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
        RestoreButtonX.Enabled = False
        Try
            Me.Cursor = Cursors.WaitCursor
            Restore_DataBase()
        Finally
            Me.Cursor = Cursors.Default
            RestoreButtonX.Enabled = True
        End Try
    End Sub

    Public Sub Restore_DataBase()
        Using chooser As New OpenFileDialog With {.Filter = "Backup Files (*.bak)|*.bak", .RestoreDirectory = True, .Title = "Open backup file"}
            If chooser.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                Dim connection = MY_Settings.SqlConStr
                Dim builder As New SqlConnectionStringBuilder(connection)
                Dim database = builder.InitialCatalog
                If Not String.Equals(database, MY_Settings.DataBase, StringComparison.OrdinalIgnoreCase) Then Throw New InvalidOperationException("اسم قاعدة الاتصال لا يطابق إعداد الاستعادة؛ لم يتم التنفيذ.")
                Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, Me.Name)
                Dim backupFile = System.IO.Path.GetFullPath(chooser.FileName)
                If MessageBox.Show(Me, "ستستبدل الاستعادة قاعدة " & database & " وتفصل اتصالاتها الحالية. تأكد من حفظ العمل وإغلاق الأجهزة الأخرى. هل تريد المتابعة؟", "تأكيد استعادة قاعدة البيانات", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) <> DialogResult.OK Then Return
                Using journal As New AdministrativeRestoreAudit(AdministrativeRestoreAudit.DefaultDirectory)
                    Dim pending = journal.Unresolved()
                    If pending.Count > 0 Then
                        If MessageBox.Show(Me, "توجد محاولة استعادة سابقة غير محسومة: " & String.Join(", ", pending) & Environment.NewLine &
                            "لا تعني هذه الحالة فشل الاستعادة. هل راجعت حالة القاعدة على الخادم وتريد السماح بمحاولة جديدة مستقلة؟ لن نغيّر نتيجة المحاولة السابقة." & Environment.NewLine & AdministrativeRestoreAudit.DefaultDirectory,
                            "مراجعة إدارية مطلوبة", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then Return
                        journal.AcknowledgeUnresolved(pending, context)
                    End If
                    journal.Start(context, builder.DataSource, database, backupFile)
                    Dim completed As Boolean = False
                    Dim executionError As Exception = Nothing
                    Dim journalError As Exception = Nothing
                    Try
                        builder.InitialCatalog = "master"
                        builder.Pooling = False
                        builder.ApplicationName = "CPOS Administrative Restore"
                        Using cn As New SqlConnection(builder.ConnectionString), command = AdministrativeRestoreExecutor.CreateCommand(cn, database, backupFile)
                            cn.Open()
                            journal.MarkDispatched()
                            command.ExecuteNonQuery()
                            completed = True
                        End Using
                    Catch ex As Exception
                        executionError = ex
                    Finally
                        Try
                            Dim sqlError = TryCast(executionError, SqlException)
                            journal.Finish(If(completed, "Succeeded", If(journal.WasDispatched, "Unknown", "NotDispatched")), If(sqlError Is Nothing, CType(Nothing, Integer?), sqlError.Number))
                        Catch ex As Exception
                            journalError = ex
                        End Try
                        If journal.WasDispatched Then AuditSecuritySession.Current.InvalidateAfterAdministrativeRestore()
                    End Try
                    Dim message = If(completed, "أكد SQL Server اكتمال استعادة النسخة الاحتياطية.", If(journal.WasDispatched,
                        "نتيجة الاستعادة غير محسومة. لا تكررها قبل فحص حالة القاعدة على الخادم؛ قد يكون الأمر قد نُفذ.", "لم يُرسل أمر الاستعادة إلى الخادم."))
                    If executionError IsNot Nothing Then message &= Environment.NewLine & executionError.Message
                    If journalError IsNot Nothing Then message &= Environment.NewLine & "تعذر تثبيت النتيجة النهائية في السجل المحلي؛ بقي سجل بدء المحاولة للمراجعة. " & journalError.Message
                    If journal.WasDispatched Then message &= Environment.NewLine & "أُبطلت جلسة المراقبة القديمة. أعد تشغيل البرنامج وتسجيل الدخول بعد التحقق من جاهزية القاعدة."
                    message &= Environment.NewLine & "مرجع: " & journal.Reference & Environment.NewLine & AdministrativeRestoreAudit.DefaultDirectory
                    MessageBox.Show(Me, message, "استعادة قاعدة البيانات", MessageBoxButtons.OK, If(completed AndAlso journalError Is Nothing, MessageBoxIcon.Information, MessageBoxIcon.Warning))
                End Using
            Catch ex As Exception
                MessageBox.Show(Me, "لم يبدأ تنفيذ الاستعادة: " & ex.Message, "الاستعادة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Using
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

    Private Sub ScheduledBackupButton_Click(sender As Object, e As EventArgs) Handles ScheduledBackupButton.Click
        Using form As New ScheduledBackupForm()
            form.ShowDialog(Me)
        End Using
    End Sub
End Class
