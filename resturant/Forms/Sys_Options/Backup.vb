Imports System.Data.SqlClient

Public Class Backup


    Private Sub BackupPathButton_Click_1(sender As Object, e As EventArgs) Handles BackupPathButton.Click
        Beep()
        MsgBox("سيتم تهئية المسار المختار عند وصوله إلي عدد معين من الملفات .. يستحسن تحديد مسار لمجلد جديد لتجنب حذف البيانات الأخرى بالكمبيوتر", MsgBoxStyle.Exclamation, "تحذيــر")

        Dim path As New FolderBrowserDialog
        path.ShowDialog()
        BackupPathTextBox.Text = path.SelectedPath
        'save to hard-disk
        BackupPath = BackupPathTextBox.Text
    End Sub

    Private Sub IsBackupCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles IsBackupCheckBox.CheckedChanged
        If sender.Checked = True Then
            sender.ForeColor = Color.DarkGreen
        Else
            sender.ForeColor = Color.Black
        End If

        If IsBackupCheckBox.Checked = True And String.IsNullOrWhiteSpace(BackupPathTextBox.Text) Then
            MsgBox("يجب تحديد مسار النسخة الإحتياطية أولا", MsgBoxStyle.Exclamation)
            IsBackupCheckBox.Checked = False
        Else
            isBackup = IsBackupCheckBox.Checked
        End If
    End Sub

    Private Sub BackupPath2Button_Click(sender As Object, e As EventArgs) Handles BackupPath2Button.Click
        Beep()
        MsgBox("سيتم تهئية المسار المختار عند وصوله إلي عدد معين من الملفات .. يستحسن تحديد مسار لمجلد جديد لتجنب حذف البيانات الأخرى بالكمبيوتر", MsgBoxStyle.Exclamation, "تحذيــر")

        Dim path As New FolderBrowserDialog
        path.ShowDialog()
        BackupPath2TextBox.Text = path.SelectedPath
        'save to hard-disk
        BackupPath_2 = BackupPath2TextBox.Text
    End Sub

    Private Sub IsBackupPath2CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles IsBackupPath2CheckBox.CheckedChanged
        If sender.Checked = True Then
            sender.ForeColor = Color.DarkGreen
        Else
            sender.ForeColor = Color.Black
        End If

        If IsBackupPath2CheckBox.Checked = True And String.IsNullOrWhiteSpace(BackupPath2TextBox.Text) Then
            MsgBox("يجب تحديد مسار النسخة الإحتياطية أولا", MsgBoxStyle.Exclamation)
            IsBackupPath2CheckBox.Checked = False
        Else
            isBackupPath2 = IsBackupPath2CheckBox.Checked
        End If
    End Sub

    Private Sub RestoreButtonX_Click(sender As Object, e As EventArgs) Handles RestoreButtonX.Click
        Me.Cursor = Cursors.WaitCursor
        Restore_DataBase()
        Me.Cursor = Cursors.Default
    End Sub

    Public Sub Restore_DataBase()
        Dim c As New C
        Dim OpenFileDialog1 As New OpenFileDialog
        OpenFileDialog1.Filter = "Backup Files (*.bak)|*.bak"
        OpenFileDialog1.RestoreDirectory = True
        OpenFileDialog1.Title = "Open backup file"
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            'getting restore file path.
            Dim path As String
            path = System.IO.Path.GetFullPath(OpenFileDialog1.FileName)

            'Code to restore database.
            Try
                c.Com = New SqlCommand("USE master ALTER DATABASE " & My_Settings.DataBase & "  SET SINGLE_USER " &
                                       " WITH ROLLBACK IMMEDIATE  RESTORE DATABASE " & My_Settings.DataBase & " FROM DISK = '" & path & "'   with REPLACE", c.Con)
                c.Con.Open()
                c.Com.ExecuteNonQuery()
                MsgBox("تــم إسترجاع النسخة الإحتياطية للنظام", MsgBoxStyle.Information)
                Network_Edit_Tracker_insert(" إسترجاع نسخة إحتياطية للمنظومة من المسار " + path, 0, 0, 0)
                c.Con.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
                c.Con.Close()
            End Try
        End If
    End Sub


    Private Sub BackupButtonX_Click(sender As Object, e As EventArgs) Handles BackupButtonX.Click
        Dim Path As String
        SaveFileDialog1.FileName = My_Settings.DataBase

        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            Path = SaveFileDialog1.FileName + ".bak"
            If is_Comprission_CB.Checked = True Then
                If query("backup database " & MY_Settings.DataBase & " to disk='" & Path & "' WITH NOFORMAT, NOINIT, SKIP, NOREWIND, NOUNLOAD, COMPRESSION,  STATS = 10") = 1 Then MsgBox("تــم أخد النسخــة الإحتيـاطية مرفقة بملف نصي يحتوي على بيانات المستخدم الرئيسي وبنفس إسم النسخة الإحتياطية", MsgBoxStyle.Information)

            Else
                If query("backup database " & MY_Settings.DataBase & " to disk='" & Path & "'") = 1 Then MsgBox("تــم أخد النسخــة الإحتيـاطية مرفقة بملف نصي يحتوي على بيانات المستخدم الرئيسي وبنفس إسم النسخة الإحتياطية", MsgBoxStyle.Information)

            End If


            '-----------------------------------------------------------------------------------------
            Dim c As New C
            Dim s As String = "SELECT [UserName],[UserPass]FROM [Users] where user_id = 1"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            Try
                c.Dr = c.Com.ExecuteReader
                If c.Dr.HasRows Then
                    c.Dr.Read()

                    Dim Writer As System.IO.StreamWriter
                    Writer = New System.IO.StreamWriter(Path + "UserAdmin.txt") '<-- Where to create/write to
                    Writer.Write("الرقم السري لمدير " + My.Application.Info.AssemblyName.ToString + ", النسخة الإحتياطية لتاريخ " + Date.Now + vbNewLine + "المستخــدم : " + c.Dr("UserName") + vbNewLine + "الــرقم الســري : " + c.Dr("UserPass"))
                    Writer.Close()

                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            c.Con.Close()

        End If
    End Sub


    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        Update_Backup_Info()
    End Sub

    Private Sub Update_Backup_Info()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "Update_Backup_Info"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@CPU_ID", My.Computer.Name)
            .Parameters.AddWithValue("@isBackup", IsBackupCheckBox.Checked)
            .Parameters.AddWithValue("@BackupPath", BackupPathTextBox.Text)
            .Parameters.AddWithValue("@isBackup2", IsBackupPath2CheckBox.Checked)
            .Parameters.AddWithValue("@BackupPath_2", BackupPath2TextBox.Text)
            .Parameters.AddWithValue("@BK_OnExit", BkPath3_txt.Text)

        End With

        If SQL_SP_EXEC(c.Com) Then
            MsgBox("تم حفظ  التعديلات", MsgBoxStyle.Information)
            Get_Computer_Setting()
        End If
    End Sub

    Private Sub Backup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IntialForm()
        LoadScheduledBackupSettings()
    End Sub

    Private Sub IntialForm()
        '''''''''''''''''''''''''''''''''''''''''''
        'load from the hard-disk
        BackupPathTextBox.Text = BackupPath
        IsBackupCheckBox.Checked = isBackup

        BackupPath2TextBox.Text = BackupPath_2
        IsBackupPath2CheckBox.Checked = isBackupPath2

        BkPath3_txt.Text = BackupPath_OnExit
    End Sub

    Private Sub BkPath3_Btn_Click(sender As Object, e As EventArgs) Handles BkPath3_Btn.Click
        Beep()
        Dim path As New FolderBrowserDialog
        path.ShowDialog()
        BkPath3_txt.Text = path.SelectedPath
        'save to hard-disk
        BackupPath_OnExit = BkPath3_txt.Text
    End Sub

    Private Sub NoBK_btn_Click(sender As Object, e As EventArgs) Handles NoBK_btn.Click
        BkPath3_txt.Clear()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Backup_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub is_Comprission_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Comprission_CB.CheckedChanged
        CB_CHecked(sender)
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
        If CleanupModeComboBox.SelectedIndex = 1 Then
            CleanupValueLabel.Text = "حذف النسخ الأقدم من (يوم)"
        Else
            CleanupValueLabel.Text = "عدد النسخ المحتفظ بها"
        End If
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
