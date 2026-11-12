Option Strict On

Imports System.Data
Imports System.Data.SqlClient

' Separate from Agents (account operations) and from customer-type filtering.
Friend NotInheritable Class AgentCardAccessPolicy
    Private Sub New()
    End Sub

    Private Shared Function IsInstalled(cn As SqlConnection, tx As SqlTransaction) As Boolean
        Using cmd As New SqlCommand("SELECT CASE WHEN COL_LENGTH(N'dbo.Users_Validations',N'Open_Agent_Card') IS NULL THEN 0 ELSE 1 END", cn, tx)
            Return Convert.ToInt32(cmd.ExecuteScalar()) = 1
        End Using
    End Function

    Friend Shared Sub EnsureAllowed(connectionString As String, userId As Integer)
        Using cn As New SqlConnection(connectionString)
            cn.Open()
            EnsureAllowed(cn, Nothing, userId)
        End Using
    End Sub

    Friend Shared Sub EnsureAllowed(cn As SqlConnection, tx As SqlTransaction, userId As Integer)
        If userId <= 0 Then Throw New UnauthorizedAccessException("المستخدم الحالي غير صالح.")
        ' A pre-upgrade database retains its old access behavior, but inactive/unknown users never pass.
        Dim flag = If(IsInstalled(cn, tx), "v.Open_Agent_Card=1", "1=1")
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) LEFT JOIN dbo.Users_Validations v WITH(HOLDLOCK) ON v.T_ID=u.Valid_ID WHERE u.User_ID=@UserId AND u.is_Allow=1 AND (u.isAdmin=1 OR (v.T_ID IS NOT NULL AND " & flag & "))", cn, tx)
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
            If Convert.ToInt32(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية فتح بطاقة العملاء والموردين.")
        End Using
    End Sub

    Friend Shared Function LoadValidation(connectionString As String, validationId As Integer) As Boolean
        Using cn As New SqlConnection(connectionString)
            cn.Open()
            Dim column = If(IsInstalled(cn, Nothing), "Open_Agent_Card", "CAST(1 AS bit)")
            Using cmd As New SqlCommand("SELECT " & column & " FROM dbo.Users_Validations WHERE T_ID=@Id", cn)
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = validationId
                Dim value = cmd.ExecuteScalar()
                If value Is Nothing OrElse IsDBNull(value) Then Throw New InvalidOperationException("الصلاحية المحددة غير موجودة.")
                Return Convert.ToBoolean(value)
            End Using
        End Using
    End Function

    Friend Shared Sub SaveValidation(cn As SqlConnection, tx As SqlTransaction, validationId As Integer, allowed As Boolean)
        If tx Is Nothing OrElse tx.Connection IsNot cn Then Throw New InvalidOperationException("حفظ صلاحية البطاقة يتطلب معاملة الحفظ نفسها.")
        If Not IsInstalled(cn, tx) Then
            If allowed Then Return
            Throw New InvalidOperationException("صلاحية فتح بطاقة العملاء غير مثبتة. نفّذ ترقية الصلاحيات 002 أولًا.")
        End If
        ' Legacy permission procedures leave NOCOUNT ON on the connection; ExecuteNonQuery
        ' can return -1 after a successful UPDATE. Read SQL's row count explicitly instead.
        Using cmd As New SqlCommand("UPDATE dbo.Users_Validations SET Open_Agent_Card=@Allowed WHERE T_ID=@Id; SELECT @@ROWCOUNT;", cn, tx)
            cmd.Parameters.Add("@Allowed", SqlDbType.Bit).Value = allowed
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = validationId
            If Convert.ToInt32(cmd.ExecuteScalar()) <> 1 Then Throw New InvalidOperationException("تعذر حفظ صلاحية فتح بطاقة العملاء.")
        End Using
    End Sub
End Class
