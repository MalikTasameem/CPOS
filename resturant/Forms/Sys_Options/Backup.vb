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
End Class