Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

' Search-only compatibility layer. Does not write audit/business data or include draft logs.
Friend NotInheritable Class NetworkTrackerSearch
    Private Sub New()
    End Sub

    Friend Shared Function BuildCommand(fromDate As DateTime, toDate As DateTime,
                                        screenId As Integer, operationId As Integer,
                                        userId As Integer, documentNumber As String) As SqlCommand
        If fromDate.Date > toDate.Date Then
            Throw New ArgumentException("تاريخ البداية يجب ألا يتجاوز تاريخ النهاية.")
        End If
        If toDate.Date = DateTime.MaxValue.Date Then
            Throw New ArgumentException("تاريخ النهاية خارج النطاق المدعوم للبحث.")
        End If
        Dim reference As String = If(documentNumber, String.Empty).Trim()
        If reference.Length > 50 Then
            Throw New ArgumentException("رقم المستند يجب ألا يتجاوز 50 حرفًا.")
        End If
        ' LEFT JOIN keeps unclassified records visible before the view migration is deployed.
        Dim sql As New StringBuilder()
        sql.AppendLine("SELECT ROW_NUMBER() OVER(ORDER BY n.[Date],n.T_ID) AS IDX,")
        sql.AppendLine("n.T_ID,n.CP_Name,")
        sql.AppendLine("COALESCE(NULLIF(u.UserName,N''),N'مستخدم غير معرّف ('+CONVERT(nvarchar(20),n.User_ID)+N')') AS UserName,")
        sql.AppendLine("n.[Date],")
        sql.AppendLine("COALESCE(NULLIF(s.Type_Name,N''),N'تصنيف غير معرّف ('+ISNULL(CONVERT(nvarchar(20),n.Screen_Type),N'NULL')+N')') AS Type_Name,")
        sql.AppendLine("COALESCE(NULLIF(o.Operation_Name,N''),N'عملية غير معرّفة ('+ISNULL(CONVERT(nvarchar(20),n.Operation_ID),N'NULL')+N')') AS Operation_Name,")
        sql.AppendLine("n.Notes,n.Bill_ID,n.Screen_Type,n.Operation_ID,n.User_ID")
        sql.AppendLine("FROM dbo.Network_Edit_Tracker n")
        sql.AppendLine("LEFT JOIN dbo.Users u ON u.User_ID=n.User_ID")
        sql.AppendLine("LEFT JOIN dbo.AgentBalance_Type s ON s.id=n.Screen_Type")
        sql.AppendLine("LEFT JOIN dbo.Operation_Types o ON o.T_ID=n.Operation_ID")
        sql.AppendLine("WHERE n.[Date]>=@FromDate AND n.[Date]<@ToExclusive")
        Dim command As New SqlCommand()
        command.CommandTimeout = 120
        command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate.Date
        command.Parameters.Add("@ToExclusive", SqlDbType.DateTime).Value = toDate.Date.AddDays(1)
        If screenId > 0 Then
            sql.AppendLine("AND n.Screen_Type=@ScreenId")
            command.Parameters.Add("@ScreenId", SqlDbType.Int).Value = screenId
        End If
        If operationId > 0 Then
            sql.AppendLine("AND n.Operation_ID=@OperationId")
            command.Parameters.Add("@OperationId", SqlDbType.Int).Value = operationId
        End If
        If userId > 0 Then
            sql.AppendLine("AND n.User_ID=@UserId")
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
        End If
        If reference.Length > 0 Then
            sql.AppendLine("AND n.Bill_ID=@DocumentNumber")
            command.Parameters.Add("@DocumentNumber", SqlDbType.NVarChar, 50).Value = reference
        End If
        sql.AppendLine("ORDER BY n.[Date],n.T_ID;")
        command.CommandText = sql.ToString()
        Return command
    End Function

    Friend Shared Function BuildNotesFilter(searchText As String) As String
        If String.IsNullOrEmpty(searchText) Then Return String.Empty
        Dim escaped As New StringBuilder()
        For Each character As Char In searchText
            Select Case character
                Case "'"c
                    escaped.Append("''")
                Case "["c
                    escaped.Append("[[]")
                Case "]"c
                    escaped.Append("[]]")
                Case "%"c
                    escaped.Append("[%]")
                Case "*"c
                    escaped.Append("[*]")
                Case Else
                    escaped.Append(character)
            End Select
        Next
        Return "ISNULL(Notes, '') LIKE '%" & escaped.ToString() & "%'"
    End Function
End Class
