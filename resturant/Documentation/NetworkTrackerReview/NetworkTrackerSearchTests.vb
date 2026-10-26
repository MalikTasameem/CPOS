Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Text.RegularExpressions

' Standalone tests compile the actual production helper. Live mode executes SELECT only.
Module NetworkTrackerSearchTests
    Private passed As Integer

    Sub Main(args As String())
        Try
            TestParameters()
            TestDescriptionFilter()
            TestInvalidInput()
            If args.Length = 2 Then TestLiveReadOnly(args(0), args(1))
            Console.WriteLine("PASS: " & passed.ToString() & " checks. No database writes.")
        Catch ex As Exception
            Console.Error.WriteLine("FAIL: " & ex.GetType().Name & ": " & ex.Message)
            Environment.ExitCode = 1
        End Try
    End Sub

    Private Sub Check(condition As Boolean, label As String)
        If Not condition Then Throw New Exception(label)
        passed += 1
        Console.WriteLine("PASS " & label)
    End Sub

    Private Sub TestParameters()
        Dim malicious As String = "A' OR 1=1--"
        Using cmd = NetworkTrackerSearch.BuildCommand(New DateTime(2026, 10, 1, 17, 0, 0), New DateTime(2026, 10, 13, 23, 0, 0), 39, 3, 1, malicious)
            Check(Not cmd.CommandText.Contains(malicious), "reference excluded from SQL text")
            Check(CStr(cmd.Parameters("@DocumentNumber").Value) = malicious, "reference preserved as data")
            Check(cmd.Parameters("@DocumentNumber").SqlDbType = SqlDbType.NVarChar AndAlso cmd.Parameters("@DocumentNumber").Size = 50, "text reference type and size")
            Check(CDate(cmd.Parameters("@FromDate").Value) = New DateTime(2026, 10, 1), "start normalized to midnight")
            Check(CDate(cmd.Parameters("@ToExclusive").Value) = New DateTime(2026, 10, 14), "inclusive final day via exclusive next day")
            Check(cmd.Parameters.Count = 6, "all optional filters parameterized")
            Check(cmd.CommandText.Contains("ORDER BY n.[Date],n.T_ID"), "stable date and ID ordering")
            Check(Not cmd.CommandText.Contains("INNER JOIN"), "legacy records not lost by inner joins")
        End Using
        Using cmd = NetworkTrackerSearch.BuildCommand(Date.Today, Date.Today, 0, 0, 0, Nothing)
            Check(cmd.Parameters.Count = 2, "all-selection adds only date parameters")
        End Using
        Dim previousCulture = CultureInfo.CurrentCulture
        Try
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ar-LY")
            Using cmd = NetworkTrackerSearch.BuildCommand(New DateTime(2026, 1, 2), New DateTime(2026, 1, 2), 0, 0, 0, "001")
                Check(TypeOf cmd.Parameters("@FromDate").Value Is DateTime, "culture does not turn dates into text")
                Check(CStr(cmd.Parameters("@DocumentNumber").Value) = "001", "leading zero reference preserved")
            End Using
        Finally
            CultureInfo.CurrentCulture = previousCulture
        End Try
    End Sub

    Private Sub TestDescriptionFilter()
        For Each needle As String In New String() {"O'Brien", "%", "*", "[", "]", "[x]", "'[]%*", "تسوية"}
            Dim table As New DataTable()
            table.Columns.Add("Notes", GetType(String))
            table.Rows.Add("prefix " & needle & " suffix")
            table.Rows.Add("ordinary unrelated text")
            table.Rows.Add(DBNull.Value)
            table.DefaultView.RowFilter = NetworkTrackerSearch.BuildNotesFilter(needle)
            Check(table.DefaultView.Count = 1, "literal description search: " & needle)
        Next
        Check(NetworkTrackerSearch.BuildNotesFilter(Nothing) = "", "null filter cleared")
        Check(NetworkTrackerSearch.BuildNotesFilter("") = "", "empty filter cleared")
    End Sub

    Private Sub TestInvalidInput()
        Dim rejected As Boolean
        Try
            Using cmd = NetworkTrackerSearch.BuildCommand(Date.Today.AddDays(1), Date.Today, 0, 0, 0, "")
            End Using
        Catch ex As ArgumentException
            rejected = True
        End Try
        Check(rejected, "reversed dates rejected")
        rejected = False
        Try
            Using cmd = NetworkTrackerSearch.BuildCommand(Date.Today, Date.Today, 0, 0, 0, New String("A"c, 51))
            End Using
        Catch ex As ArgumentException
            rejected = True
        End Try
        Check(rejected, "long reference rejected without truncation")
    End Sub

    Private Sub TestLiveReadOnly(settingsFile As String, viewScriptFile As String)
        Dim settings As New Collections.Generic.Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        For Each line In File.ReadAllLines(settingsFile)
            Dim separator = line.IndexOf(":"c)
            If separator > 0 Then settings(line.Substring(0, separator)) = line.Substring(separator + 1)
        Next
        If Boolean.Parse(settings("IsAttachDB")) Then Throw New Exception("No database attachment is permitted by these tests.")
        Dim builder As New SqlConnectionStringBuilder()
        builder.DataSource = settings("S_SERVER").Trim()
        builder.InitialCatalog = settings("DataBase").Trim()
        builder.ConnectTimeout = 10
        builder.ApplicationName = "CPOS Tracker Search ReadOnly Tests"
        If settings("DB_Authentication") = "0" Then
            builder.IntegratedSecurity = True
        Else
            builder.UserID = settings("DB_UName")
            builder.Password = settings("DB_Pass")
        End If
        Using connection As New SqlConnection(builder.ConnectionString)
            connection.Open()
            Dim rows As New DataTable()
            Using cmd = NetworkTrackerSearch.BuildCommand(New DateTime(1753, 1, 1), New DateTime(9998, 12, 31), 0, 0, 0, "")
                cmd.Connection = connection
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(rows)
                End Using
            End Using
            Dim count As Integer
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Network_Edit_Tracker WHERE [Date]>=@D1 AND [Date]<@D2", connection)
                cmd.Parameters.Add("@D1", SqlDbType.DateTime).Value = New DateTime(1753, 1, 1)
                cmd.Parameters.Add("@D2", SqlDbType.DateTime).Value = New DateTime(9999, 1, 1)
                count = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
            Check(rows.Rows.Count = count, "live helper returns all base records in range")
            Console.WriteLine("Live read-only row count: " & count.ToString())
            Dim ids As New Collections.Generic.HashSet(Of Integer)()
            Dim previousDate As DateTime = DateTime.MinValue
            Dim previousId As Integer = 0
            Dim sorted As Boolean = True
            For Each row As DataRow In rows.Rows
                Dim eventDate = CDate(row("Date"))
                Dim eventId = CInt(row("T_ID"))
                If eventDate < previousDate OrElse (eventDate = previousDate AndAlso eventId <= previousId) Then sorted = False
                ids.Add(eventId)
                previousDate = eventDate
                previousId = eventId
            Next
            Check(sorted, "live deterministic chronological order")
            Check(ids.Count = rows.Rows.Count, "live no duplicate event IDs")
            Using cmd = NetworkTrackerSearch.BuildCommand(New DateTime(1753, 1, 1), New DateTime(9998, 12, 31), 39, 3, 0, "")
                cmd.Connection = connection
                Dim filtered As New DataTable()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(filtered)
                End Using
                Check(filtered.Rows.Count = rows.Select("Screen_Type=39 AND Operation_ID=3").Length, "live screen and operation filters")
            End Using
            Using cmd = NetworkTrackerSearch.BuildCommand(New DateTime(1753, 1, 1), New DateTime(9998, 12, 31), 0, 0, 0, "A' OR 1=1--")
                cmd.Connection = connection
                Dim filtered As New DataTable()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(filtered)
                End Using
                Dim expected As Integer = 0
                For Each row As DataRow In rows.Rows
                    If Convert.ToString(row("Bill_ID")) = "A' OR 1=1--" Then expected += 1
                Next
                Check(filtered.Rows.Count = expected, "live injection-like reference treated as text")
            End Using

            ' Extract and execute ONLY the SELECT projection of the planned view, never ALTER VIEW.
            Dim script As String = File.ReadAllText(viewScriptFile)
            Dim match = Regex.Match(script, "EXEC sys\.sp_executesql N'(?<body>(?:''|[^'])*)';", RegexOptions.Singleline)
            Check(match.Success, "view migration body extractable for SELECT-only verification")
            Dim body = match.Groups("body").Value.Replace("''", "'").Replace(vbCrLf, vbLf)
            Dim start = body.IndexOf("SELECT ", StringComparison.Ordinal)
            Check(start > 0 AndAlso body.Substring(0, start).Trim() = "ALTER VIEW dbo.Network_Edit_Tracker_V" & vbLf & "AS", "migration contains expected view only")
            Dim projection As String = body.Substring(start)
            Dim viewRows As New DataTable()
            Using adapter As New SqlDataAdapter(projection, connection)
                adapter.Fill(viewRows)
            End Using
            Check(viewRows.Rows.Count = count, "planned view SELECT preserves all events")
            Dim projectionIds As New Collections.Generic.HashSet(Of Integer)()
            Dim labelsPresent As Boolean = True
            For Each row As DataRow In viewRows.Rows
                projectionIds.Add(CInt(row("T_ID")))
                If String.IsNullOrEmpty(Convert.ToString(row("Type_Name"))) OrElse String.IsNullOrEmpty(Convert.ToString(row("Operation_Name"))) Then labelsPresent = False
            Next
            Check(labelsPresent, "all projection classification labels present")
            Check(projectionIds.SetEquals(ids), "screen and view projection contain same IDs")
            ' Do not print notes, names, or credentials.
        End Using
    End Sub
End Module
