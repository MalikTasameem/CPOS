Option Strict On

Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AgentTypeAccessPolicy
    Private Const MappingTable As String = "dbo.User_Validation_AgentTypes"

    Private Sub New()
    End Sub

    Private Shared Function MappingTableExists(cn As SqlConnection, tx As SqlTransaction) As Boolean
        Using cmd As New SqlCommand("SELECT CASE WHEN OBJECT_ID(N'dbo.User_Validation_AgentTypes', N'U') IS NULL THEN 0 ELSE 1 END", cn, tx)
            Return Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.InvariantCulture) = 1
        End Using
    End Function

    Friend Shared Function IsInstalled(connectionString As String) As Boolean
        Using cn As New SqlConnection(connectionString)
            cn.Open()
            Return MappingTableExists(cn, Nothing)
        End Using
    End Function

    ' agentTypeExpression is supplied only by the application from known SQL aliases/columns.
    Friend Shared Function UserWhereClause(connectionString As String, userId As Integer, agentTypeExpression As String) As String
        If String.IsNullOrWhiteSpace(agentTypeExpression) Then Throw New ArgumentException("يلزم تحديد عمود نوع الحساب.", NameOf(agentTypeExpression))
        If Not IsInstalled(connectionString) Then Return String.Empty

        Dim id = userId.ToString(CultureInfo.InvariantCulture)
        Return " AND EXISTS (SELECT 1 FROM dbo.Users AS AccessUser " &
               "WHERE AccessUser.User_ID=" & id & " AND AccessUser.is_Allow=1 " &
               "AND (AccessUser.isAdmin=1 " &
               "OR NOT EXISTS (SELECT 1 FROM " & MappingTable & " AS AccessAll WHERE AccessAll.Validation_ID=AccessUser.Valid_ID) " &
               "OR EXISTS (SELECT 1 FROM " & MappingTable & " AS AccessType WHERE AccessType.Validation_ID=AccessUser.Valid_ID AND AccessType.Agent_Type_ID=" & agentTypeExpression & ")))"
    End Function

    Friend Shared Function IsAgentAllowed(connectionString As String, userId As Integer, agentId As Integer) As Boolean
        Using cn As New SqlConnection(connectionString)
            cn.Open()
            Return IsAgentAllowed(cn, Nothing, userId, agentId)
        End Using
    End Function

    Private Shared Function IsAgentAllowed(cn As SqlConnection, tx As SqlTransaction, userId As Integer, agentId As Integer) As Boolean
        If Not MappingTableExists(cn, tx) Then Return True
        Const sql As String =
            "SELECT COUNT(*) FROM dbo.Users AS u WITH(HOLDLOCK) " &
            "JOIN dbo.Agents AS a WITH(HOLDLOCK) ON a.AG_ID=@AgentId " &
            "WHERE u.User_ID=@UserId AND u.is_Allow=1 " &
            "AND (u.isAdmin=1 " &
            "OR NOT EXISTS (SELECT 1 FROM dbo.User_Validation_AgentTypes AS p WITH(HOLDLOCK) WHERE p.Validation_ID=u.Valid_ID) " &
            "OR EXISTS (SELECT 1 FROM dbo.User_Validation_AgentTypes AS p WITH(HOLDLOCK) WHERE p.Validation_ID=u.Valid_ID AND p.Agent_Type_ID=a.Type_ID))"
        Using cmd As New SqlCommand(sql, cn, tx)
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
            cmd.Parameters.Add("@AgentId", SqlDbType.Int).Value = agentId
            Return Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.InvariantCulture) = 1
        End Using
    End Function

    Friend Shared Function IsTypeAllowed(connectionString As String, userId As Integer, agentTypeId As Integer) As Boolean
        Using cn As New SqlConnection(connectionString)
            cn.Open()
            If Not MappingTableExists(cn, Nothing) Then Return True
            Const sql As String =
                "SELECT COUNT(*) FROM dbo.Users AS u WITH(HOLDLOCK) " &
                "WHERE u.User_ID=@UserId AND u.is_Allow=1 " &
                "AND (u.isAdmin=1 " &
                "OR NOT EXISTS (SELECT 1 FROM dbo.User_Validation_AgentTypes AS p WITH(HOLDLOCK) WHERE p.Validation_ID=u.Valid_ID) " &
                "OR EXISTS (SELECT 1 FROM dbo.User_Validation_AgentTypes AS p WITH(HOLDLOCK) WHERE p.Validation_ID=u.Valid_ID AND p.Agent_Type_ID=@AgentTypeId))"
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
                cmd.Parameters.Add("@AgentTypeId", SqlDbType.Int).Value = agentTypeId
                Return Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.InvariantCulture) = 1
            End Using
        End Using
    End Function

    Friend Shared Sub EnsureAgentAllowed(cn As SqlConnection, tx As SqlTransaction, userId As Integer, agentId As Integer)
        If agentId <= 0 OrElse Not IsAgentAllowed(cn, tx, userId, agentId) Then
            Throw New UnauthorizedAccessException("نوع العميل أو الحساب غير مسموح للمستخدم الحالي.")
        End If
    End Sub

    Friend Shared Function LoadValidationTypeIds(connectionString As String, validationId As Integer) As HashSet(Of Integer)
        Dim result As New HashSet(Of Integer)()
        Using cn As New SqlConnection(connectionString)
            cn.Open()
            If Not MappingTableExists(cn, Nothing) Then Return result
            Using cmd As New SqlCommand("SELECT Agent_Type_ID FROM " & MappingTable & " WHERE Validation_ID=@ValidationId ORDER BY Agent_Type_ID", cn)
                cmd.Parameters.Add("@ValidationId", SqlDbType.Int).Value = validationId
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        result.Add(reader.GetInt32(0))
                    End While
                End Using
            End Using
        End Using
        Return result
    End Function

    Friend Shared Sub SaveValidationTypes(cn As SqlConnection, tx As SqlTransaction, validationId As Integer, allowAll As Boolean, selectedTypeIds As IEnumerable(Of Integer))
        If Not MappingTableExists(cn, tx) Then
            If allowAll Then Return
            Throw New InvalidOperationException("جدول صلاحيات أنواع العملاء غير مثبت. نفّذ ترحيل قاعدة البيانات أولًا.")
        End If

        Dim ids = selectedTypeIds.Distinct().OrderBy(Function(x) x).ToArray()
        If Not allowAll AndAlso ids.Length = 0 Then Throw New InvalidOperationException("اختر نوع عميل واحدًا على الأقل أو فعّل السماح للجميع.")
        If ids.Any(Function(x) x <= 0) Then Throw New InvalidOperationException("توجد قيمة غير صالحة ضمن أنواع العملاء.")

        If ids.Length > 0 Then
            Using validate As New SqlCommand("SELECT COUNT(*) FROM dbo.Agents_Types WITH(HOLDLOCK) WHERE ID IN (" & String.Join(",", ids.Select(Function(x, i) "@Type" & i.ToString(CultureInfo.InvariantCulture))) & ")", cn, tx)
                For i = 0 To ids.Length - 1
                    validate.Parameters.Add("@Type" & i.ToString(CultureInfo.InvariantCulture), SqlDbType.Int).Value = ids(i)
                Next
                If Convert.ToInt32(validate.ExecuteScalar(), CultureInfo.InvariantCulture) <> ids.Length Then Throw New InvalidOperationException("أحد أنواع العملاء المحددة لم يعد موجودًا.")
            End Using
        End If

        Using deleteCommand As New SqlCommand("DELETE FROM " & MappingTable & " WHERE Validation_ID=@ValidationId", cn, tx)
            deleteCommand.Parameters.Add("@ValidationId", SqlDbType.Int).Value = validationId
            deleteCommand.ExecuteNonQuery()
        End Using
        If allowAll Then Return

        For Each typeId In ids
            Using insertCommand As New SqlCommand("INSERT INTO " & MappingTable & "(Validation_ID,Agent_Type_ID) VALUES(@ValidationId,@AgentTypeId)", cn, tx)
                insertCommand.Parameters.Add("@ValidationId", SqlDbType.Int).Value = validationId
                insertCommand.Parameters.Add("@AgentTypeId", SqlDbType.Int).Value = typeId
                insertCommand.ExecuteNonQuery()
            End Using
        Next
    End Sub
End Class
