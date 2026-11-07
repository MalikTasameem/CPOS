Imports System.Data.SqlClient
Imports System.IO

Public Class DraftCustomerCacheResult
    Public Property Customers As DataTable
    Public Property LoadedFromServer As Boolean
    Public Property CacheUpdatedAt As DateTime?
    Public Property ErrorMessage As String
End Class

Public NotInheritable Class DraftCustomerCacheService

    Private ReadOnly CacheFolder As String =
        Path.Combine(Application.StartupPath, "DraftSales\Cache")

    Private ReadOnly CacheFilePath As String =
        Path.Combine(Application.StartupPath, "DraftSales\Cache\Customers.xml")

    Public Function LoadCustomers(defaultCustomerId As Integer) As DraftCustomerCacheResult
        Dim result As New DraftCustomerCacheResult With {
            .Customers = CreateCustomersTable(),
            .LoadedFromServer = False,
            .CacheUpdatedAt = Nothing,
            .ErrorMessage = ""
        }

        Try
            Dim customers As DataTable = LoadCustomersFromServer(defaultCustomerId)
            result.Customers = customers
            result.LoadedFromServer = True
            result.CacheUpdatedAt = DateTime.Now

            Try
                SaveCache(customers)
            Catch cacheException As Exception
                result.ErrorMessage = "تم تحميل العملاء من الخادم، لكن تعذر تحديث الذاكرة المحلية: " &
                                      cacheException.Message
            End Try

            Return result
        Catch serverException As Exception
            result.ErrorMessage = serverException.Message
        End Try

        Dim cacheError As String = ""
        Dim cachedCustomers As DataTable = Nothing
        Dim loadedCachePath As String = ""

        For Each candidatePath As String In GetCacheCandidates()
            If TryLoadCache(candidatePath, cachedCustomers, cacheError) Then
                loadedCachePath = candidatePath
                Exit For
            End If
        Next

        If cachedCustomers IsNot Nothing Then
            result.Customers = cachedCustomers
            result.CacheUpdatedAt = File.GetLastWriteTime(loadedCachePath)
        ElseIf String.IsNullOrWhiteSpace(cacheError) = False Then
            result.ErrorMessage = (result.ErrorMessage & Environment.NewLine & cacheError).Trim()
        End If

        Return result
    End Function

    Private Function LoadCustomersFromServer(defaultCustomerId As Integer) As DataTable
        Dim customers As DataTable = CreateCustomersTable()

        Const sql As String =
            "SELECT AG_ID, Ag_name, ISNULL(Barcode, N'') AS Barcode, " &
            "ISNULL(Ag_phone, N'') AS Ag_phone, Type_ID, ISNULL(isDefaultAG, 0) AS isDefaultAG " &
            "FROM dbo.Agents " &
            "WHERE AG_ID = @DefaultCustomerId OR Type_ID IN (1, 2, 5, 6, 7) " &
            "ORDER BY CASE WHEN AG_ID = @DefaultCustomerId THEN 0 ELSE 1 END, Ag_name, AG_ID"

        Dim connectionString As New SqlConnectionStringBuilder(MY_Settings.SqlConStr)
        If connectionString.ConnectTimeout <= 0 OrElse connectionString.ConnectTimeout > 5 Then
            connectionString.ConnectTimeout = 5
        End If

        Using connection As New SqlConnection(connectionString.ConnectionString)
            Using command As New SqlCommand(sql, connection)
                command.CommandTimeout = 20
                command.Parameters.Add("@DefaultCustomerId", SqlDbType.Int).Value = defaultCustomerId

                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    customers.Load(reader)
                End Using
            End Using
        End Using

        ValidateCustomersTable(customers)
        Return customers
    End Function

    Private Sub SaveCache(customers As DataTable)
        If customers Is Nothing Then Throw New ArgumentNullException("customers")
        ValidateCustomersTable(customers)

        If Directory.Exists(CacheFolder) = False Then Directory.CreateDirectory(CacheFolder)

        Dim temporaryPath As String = CacheFilePath & ".tmp"
        Dim backupPath As String = CacheFilePath & ".bak"

        customers.TableName = "DraftCustomers"
        customers.WriteXml(temporaryPath, XmlWriteMode.WriteSchema)

        If File.Exists(CacheFilePath) Then
            File.Replace(temporaryPath, CacheFilePath, backupPath, True)
        Else
            File.Move(temporaryPath, CacheFilePath)
        End If
    End Sub

    Private Function TryLoadCache(filePath As String,
                                  ByRef customers As DataTable,
                                  ByRef errorMessage As String) As Boolean
        If File.Exists(filePath) = False Then Return False

        Try
            Dim cachedTable As New DataTable("DraftCustomers")
            cachedTable.ReadXml(filePath)
            ValidateCustomersTable(cachedTable)

            customers = cachedTable
            Return True
        Catch ex As Exception
            errorMessage = "تعذر قراءة ذاكرة العملاء المحلية: " & ex.Message
            Return False
        End Try
    End Function

    Private Function GetCacheCandidates() As IEnumerable(Of String)
        Return New String() {
            CacheFilePath & ".tmp",
            CacheFilePath,
            CacheFilePath & ".bak"
        }
    End Function

    Private Function CreateCustomersTable() As DataTable
        Dim customers As New DataTable("DraftCustomers")
        customers.Columns.Add("AG_ID", GetType(Integer))
        customers.Columns.Add("Ag_name", GetType(String))
        customers.Columns.Add("Barcode", GetType(String))
        customers.Columns.Add("Ag_phone", GetType(String))
        customers.Columns.Add("Type_ID", GetType(Integer))
        customers.Columns.Add("isDefaultAG", GetType(Boolean))
        Return customers
    End Function

    Private Sub ValidateCustomersTable(customers As DataTable)
        If customers Is Nothing Then Throw New InvalidDataException("قائمة العملاء غير متاحة.")

        Dim requiredColumns As String() = {
            "AG_ID", "Ag_name", "Barcode", "Ag_phone", "Type_ID", "isDefaultAG"
        }

        For Each columnName As String In requiredColumns
            If customers.Columns.Contains(columnName) = False Then
                Throw New InvalidDataException("ذاكرة العملاء لا تحتوي على العمود " & columnName & ".")
            End If
        Next

        For Each row As DataRow In customers.Rows
            If row.IsNull("AG_ID") OrElse Convert.ToInt32(row("AG_ID")) <= 0 Then
                Throw New InvalidDataException("ذاكرة العملاء تحتوي على رقم عميل غير صحيح.")
            End If

            If row.IsNull("Ag_name") OrElse String.IsNullOrWhiteSpace(Convert.ToString(row("Ag_name"))) Then
                Throw New InvalidDataException("ذاكرة العملاء تحتوي على اسم عميل فارغ.")
            End If
        Next
    End Sub

End Class
