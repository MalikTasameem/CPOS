Imports System.Data.SqlClient

Public Class TrialDataResetForm
    Private Sub TrialDataResetForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCounts()
    End Sub

    Private Sub LoadCounts()
        MasterCountLabel.Text = "عدد القيود الرئيسية: " & GetTableCount("dbo.ACC_BALANCE_MASTER").ToString()
        DetailCountLabel.Text = "عدد تفاصيل القيود: " & GetTableCount("dbo.ACC_BALANCE").ToString()
    End Sub

    Private Function GetTableCount(tableName As String) As Integer
        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM " & tableName, cn)
                cn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        If ConfirmTextBox.Text.Trim() <> "مسح" Then
            MsgBox("اكتب كلمة مسح في مربع التأكيد قبل تنفيذ العملية.", MsgBoxStyle.Exclamation, "تأكيد مطلوب")
            ConfirmTextBox.Focus()
            Exit Sub
        End If

        If MessageBox.Show("سيتم حذف بيانات القيود التجريبية من ACC_BALANCE و ACC_BALANCE_MASTER. هل تريد المتابعة؟",
                           "تأكيد نهائي",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning,
                           MessageBoxDefaultButton.Button2,
                           MessageBoxOptions.RightAlign) <> DialogResult.Yes Then
            Exit Sub
        End If

        Try
            ResetTrialJournalData()
            MsgBox("تم تفريغ بيانات القيود التجريبية بنجاح. يمكنك الآن استخدام مساحة تجربة جديدة.", MsgBoxStyle.Information, "تمت العملية")
            DialogResult = DialogResult.OK
            Close()
        Catch ex As Exception
            MsgBox("تعذر تفريغ بيانات القيود التجريبية:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "خطأ")
        End Try
    End Sub

    Private Sub ResetTrialJournalData()
        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            cn.Open()
            Using tr As SqlTransaction = cn.BeginTransaction()
                Try
                    Using cmd As New SqlCommand("", cn, tr)
                        cmd.CommandText = "DELETE FROM dbo.ACC_BALANCE;"
                        cmd.ExecuteNonQuery()

                        cmd.CommandText = "DELETE FROM dbo.ACC_BALANCE_MASTER;"
                        cmd.ExecuteNonQuery()
                    End Using

                    tr.Commit()
                Catch
                    tr.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Private Sub CancelButtonEx_Click(sender As Object, e As EventArgs) Handles CancelButtonEx.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class
