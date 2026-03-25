Public Class SingleChoiceDialog
    ' خاصية لحفظ الخيار الذي يختاره المستخدم
    Public SelectedItem As String = String.Empty

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles OK_btn.Click
        ' التحقق من أن هناك عنصرًا محددًا
        If ListBox1.SelectedItem IsNot Nothing Then
            SelectedItem = ListBox1.SelectedItem.ToString()
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("يرجى اختيار خيار قبل المتابعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles Cancel_btn.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class