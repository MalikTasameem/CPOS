Public Class ScheduledBackupForm

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ScheduledBackupForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadScheduledBackupSettings()
    End Sub

    Private Sub LoadScheduledBackupSettings()
        Try
            Dim settings As ScheduledBackupSettings = ScheduledBackupManager.LoadSettings()
            ScheduledPathTextBox.Text = settings.BackupPath
            KeepCountNumeric.Value = Math.Max(KeepCountNumeric.Minimum, Math.Min(KeepCountNumeric.Maximum, settings.KeepCount))
            CleanupModeComboBox.SelectedIndex = If(settings.CleanupMode = "Days", 1, 0)
            CleanupEnabledCheckBox.Checked = settings.CleanupEnabled
            ScheduledCompressionCheckBox.Checked = settings.UseCompression
            ScheduledTimePicker.Value = Date.Today.Add(settings.RunTime.TimeOfDay)
            RefreshScheduledTaskStatus()
        Catch ex As Exception
            ScheduledStatusLabel.Text = "تعذر قراءة الإعدادات: " & ex.Message
            ScheduledStatusLabel.ForeColor = Color.DarkRed
        End Try
    End Sub

    Private Function ReadScheduledSettings() As ScheduledBackupSettings
        Return New ScheduledBackupSettings With {
            .BackupPath = ScheduledPathTextBox.Text.Trim(),
            .KeepCount = Decimal.ToInt32(KeepCountNumeric.Value),
            .CleanupMode = If(CleanupModeComboBox.SelectedIndex = 1, "Days", "Count"),
            .CleanupEnabled = CleanupEnabledCheckBox.Checked,
            .UseCompression = ScheduledCompressionCheckBox.Checked,
            .RunTime = ScheduledTimePicker.Value
        }
    End Function

    Private Sub ScheduledBrowseButton_Click(sender As Object, e As EventArgs) Handles ScheduledBrowseButton.Click
        Using dialog As New FolderBrowserDialog()
            dialog.Description = "اختر مجلد النسخ الاحتياطي الموجود على جهاز SQL Server"
            dialog.SelectedPath = ScheduledPathTextBox.Text
            If dialog.ShowDialog() = DialogResult.OK Then ScheduledPathTextBox.Text = dialog.SelectedPath
        End Using
    End Sub

    Private Sub ScheduledTestPathButton_Click(sender As Object, e As EventArgs) Handles ScheduledTestPathButton.Click
        Try
            ScheduledBackupManager.TestBackupPath(ScheduledPathTextBox.Text.Trim())
            MessageBox.Show("تم اختبار الكتابة في المسار بنجاح." & vbNewLine & "ملاحظة: يجب أيضًا أن يملك حساب خدمة SQL Server صلاحية الكتابة في هذا المسار.", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("فشل اختبار المسار:" & vbNewLine & ex.Message, "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ScheduledSaveButton_Click(sender As Object, e As EventArgs) Handles ScheduledSaveButton.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ScheduledBackupManager.SaveAndInstall(ReadScheduledSettings(), MY_Settings.SqlConStr)
            RefreshScheduledTaskStatus()
            MessageBox.Show("تم حفظ الإعدادات وتسجيل مهمة النسخ اليومية بنجاح." & vbNewLine & "ستعمل المهمة حتى عند إغلاق CPOS.", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As ComponentModel.Win32Exception When ex.NativeErrorCode = 1223
            MessageBox.Show("تم إلغاء طلب صلاحية Administrator، لذلك لم تُسجل المهمة.", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show("تعذر حفظ الجدولة:" & vbNewLine & ex.Message, "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ScheduledRunNowButton_Click(sender As Object, e As EventArgs) Handles ScheduledRunNowButton.Click
        Try
            ScheduledBackupManager.RunNow()
            MessageBox.Show("تم بدء النسخ في الخلفية. يمكن مراجعة سجل التنفيذ داخل مجلد إعدادات CPOS في ProgramData.", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("تعذر بدء النسخ:" & vbNewLine & ex.Message, "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ScheduledRemoveButton_Click(sender As Object, e As EventArgs) Handles ScheduledRemoveButton.Click
        If MessageBox.Show("هل تريد إلغاء مهمة النسخ المجدولة؟ لن يتم حذف ملفات النسخ الموجودة.", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            ScheduledBackupManager.RemoveTask()
            RefreshScheduledTaskStatus()
        Catch ex As ComponentModel.Win32Exception When ex.NativeErrorCode = 1223
            MessageBox.Show("تم إلغاء طلب صلاحية Administrator.", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show("تعذر إلغاء المهمة:" & vbNewLine & ex.Message, "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ScheduledRefreshButton_Click(sender As Object, e As EventArgs) Handles ScheduledRefreshButton.Click
        RefreshScheduledTaskStatus()
    End Sub

    Private Sub CleanupEnabledCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CleanupEnabledCheckBox.CheckedChanged
        KeepCountNumeric.Enabled = CleanupEnabledCheckBox.Checked
        CleanupModeComboBox.Enabled = CleanupEnabledCheckBox.Checked
    End Sub

    Private Sub CleanupModeComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CleanupModeComboBox.SelectedIndexChanged
        CleanupValueLabel.Text = If(CleanupModeComboBox.SelectedIndex = 1, "حذف النسخ الأقدم من (يوم)", "عدد النسخ المحتفظ بها")
    End Sub

    Private Sub RefreshScheduledTaskStatus()
        Try
            ScheduledStatusLabel.Text = "حالة المهمة: " & ScheduledBackupManager.GetTaskStatus()
            ScheduledStatusLabel.ForeColor = Color.DarkGreen
        Catch ex As Exception
            ScheduledStatusLabel.Text = "تعذر قراءة الحالة: " & ex.Message
            ScheduledStatusLabel.ForeColor = Color.DarkRed
        End Try
    End Sub
End Class
