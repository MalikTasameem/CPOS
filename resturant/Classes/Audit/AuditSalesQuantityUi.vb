Option Strict On
Imports System.Windows.Forms

Friend NotInheritable Class AuditSalesQuantityUi
    Friend Shared Sub Run(owner As Form, billId As Integer, lineId As Integer, displayed As Decimal, delta As Integer,
                          onUpdate As Boolean, allowNegative As Boolean, refresh As Action)
        Dim connection = MY_Settings.SqlConStr
        Dim request As AuditOperationRequest = Nothing
        Dim completed = False, canRefresh = False
        Try
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            Dim snapshot = AuditSalesQuantity.ReadForEdit(connection, context, billId, lineId, onUpdate)
            canRefresh = True
            request = AuditSalesQuantity.Request(context, snapshot, delta, displayed, onUpdate, allowNegative)
            Save(connection, request)
            completed = True
        Catch ex As Exception
            Dim state = If(request Is Nothing, "NotFound", Status(connection, request))
            If state = "Completed" Then
                completed = True
            ElseIf state = "NotFound" Then
                MessageBox.Show(owner, "لم تُحفظ خطوة الكمية. " & ex.Message, "تعديل الكمية", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Using dialog As New AuditQuantityRecoveryDialog(connection, request, ex.Message)
                    completed = dialog.ShowDialog(owner) = DialogResult.OK
                End Using
            End If
        End Try
        If completed OrElse canRefresh Then
            Try
                refresh()
            Catch ex As Exception
                MessageBox.Show(owner, If(completed, "حُسمت عملية الكمية بنجاح؛ ", "لم يُحفظ التعديل؛ ") & "تعذر تحديث العرض: " & ex.Message,
                                "تحديث الفاتورة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
    End Sub

    Friend Shared Sub Save(connection As String, request As AuditOperationRequest)
        Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, request.Context.ScreenCode)
        If connection <> MY_Settings.SqlConStr OrElse current.SourceId <> request.Context.SourceId OrElse current.ActorUserId <> request.Context.ActorUserId OrElse current.SessionId <> request.Context.SessionId Then Throw New InvalidOperationException("تغير الاتصال أو المستخدم أو جلسة الدخول؛ لا يمكن إرسال المحاولة السابقة في سياق جديد.")
        AuditSalesQuantity.Save(connection, request)
    End Sub

    Friend Shared Function Status(connection As String, request As AuditOperationRequest) As String
        Try
            Return AuditRequestCoordinator.Status(connection, request).Status
        Catch
            Return "Unknown"
        End Try
    End Function

    Friend Shared Sub SelectLine(grid As DataGridView, lineColumn As String, displayColumn As String, lineId As Integer)
        If Not grid.Columns.Contains(lineColumn) OrElse Not grid.Columns.Contains(displayColumn) Then Return
        For Each row As DataGridViewRow In grid.Rows
            If Not row.IsNewRow AndAlso row.Cells(lineColumn).Value IsNot Nothing AndAlso Not IsDBNull(row.Cells(lineColumn).Value) AndAlso Convert.ToInt64(row.Cells(lineColumn).Value) = lineId Then
                grid.CurrentCell = row.Cells(displayColumn) : Exit For
            End If
        Next
    End Sub
End Class
