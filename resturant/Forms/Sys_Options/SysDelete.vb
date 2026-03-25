Imports System.Data.SqlClient

Public Class SysDelete
    Public Jrd_T_ID As Integer
    Private Sub DeleteAllBuyAndSellCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles DeleteAllBuyAndSellCheckBox.CheckedChanged
        CB_CHecked(sender)
        check_enabled_ConfirmButtom()
    End Sub

    Private Sub DeleteAllItemsCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles DeleteAllItemsCheckBox.CheckedChanged
       CB_CHecked(sender)
        check_enabled_ConfirmButtom()
    End Sub

    Public Sub check_enabled_ConfirmButtom()
        If DeleteAllBuyAndSellCheckBox.Checked = True Then
            ConfirmDeleteButtonX.Enabled = True
            Delete_ALL_Qty_CB.Enabled = True
        ElseIf DeleteAllItemsCheckBox.Checked = True Then
            ConfirmDeleteButtonX.Enabled = True
        ElseIf Delete_AGBalance_CB.Checked = True Then
            ConfirmDeleteButtonX.Enabled = True

        ElseIf Delete_ST_CB.Checked = True Then
            ConfirmDeleteButtonX.Enabled = True

        Else
            ConfirmDeleteButtonX.Enabled = False
        End If

    End Sub

    Private Sub ConfirmDeleteButtonX_Click(sender As Object, e As EventArgs) Handles ConfirmDeleteButtonX.Click

        If String.IsNullOrWhiteSpace(Jrd_ID_Txt.Text) = False Then
            If Cjeck_Jrd_ID(Jrd_ID_Txt.Text) = False Then
                MsgBox("لم يتم التعرف على فاتورة الجرد .. تأكد من إعتماد الفاتورة أو انها غير ملغية", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If
        'Beep()

        If MessageBox.Show("تعتبر هذه العملية حساسة للغاية للنظام ... هل أنت متأكد من هذا الإجراء ؟", "تحذيــر 1", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.Yes Then
            If MessageBox.Show("أنت على وشك القيام بمسح بيانات من المنظومة بشكل نهائي في حال لم يكن لديك نسخة احتياطية للبيانات ... هل أنت متأكد من هذا الإجراء ؟", "تحذيــر 2", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.Yes Then
                CheckOwnerPass.ShowDialog()
            End If
        End If

        'If MsgBox("تعتبر هذه العملية حساسة للغاية للنظام ... هل أنت متأكد من هذا الإجراء ؟", MsgBoxStyle.YesNo, "تحذيــر 1") = MsgBoxResult.Yes Then
        '    If MsgBox("أنت على وشك القيام بمسح بيانات من المنظومة بشكل نهائي في حال لم يكن لديك نسخة احتياطية للبيانات ... هل أنت متأكد من هذا الإجراء ؟", MsgBoxStyle.YesNo, "تحذيــر 2") = MsgBoxResult.Yes Then
        '        CheckOwnerPass.ShowDialog()
        '    End If
        'End If

    End Sub

    Private Function Cjeck_Jrd_ID(Jr_ID As Integer)
        Dim C = New C
        Dim isFound As Boolean = False
        Dim S As String = "Select Jrd_ID,T_ID From Agents_Balance_MV Where Jrd_ID = '" & Jr_ID & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                isFound = True
                C.Dr.Read()
                Jrd_T_ID = C.Dr("T_ID")
            Else
                isFound = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
        Return isFound
    End Function

    Private Sub InsertDBButton_Click(sender As Object, e As EventArgs) Handles InsertDBButton.Click
        Me.Cursor = Cursors.AppStarting

        Dim c As New C
        Dim FLpath As String = Application.StartupPath + "\Fainincial Year"

        '  FLpath.ShowDialog()

        'If FLpath.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

        Dim strCreate As String

        strCreate = "CREATE DATABASE " & NewDBTextBox.Text & " ON PRIMARY " & _
           "(NAME =  " & NewDBTextBox.Text & " , " & _
            " FILENAME = '" & FLpath & "\" & NewDBTextBox.Text & ".mdf', " & _
           " SIZE = 10MB, " & _
           " FILEGROWTH = 10%) " & _
           " LOG ON " & _
           "(NAME = MyDatabase_Log, " & _
           " FILENAME = '" & FLpath & "\" & NewDBTextBox.Text & ".ldf', " & _
           " SIZE = 3MB, " & _
           " FILEGROWTH = 10%)"

        '-------------------------------------------------------------------------------------
        Dim commCreate As SqlCommand = New SqlCommand(strCreate, c.Con)

        Try
            c.Con.Open()
            '  connCreate.Open()
            commCreate.ExecuteNonQuery()
            MsgBox("Database has been created  successfully", MsgBoxStyle.Information, "")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        c.Con.Close()

        '--------------------------------------------------------------------------------------------

        Dim BackupPath = FLpath + "\" + My_Settings.DataBase + ".bak"
        If query("backup database " & MY_Settings.DataBase + " to disk='" & BackupPath & "'") = 1 Then MsgBox("تــم أخد النسخــة الإحتيـاطية مرفقة بملف نصي يحتوي على بيانات المستخدم الرئيسي وبنفس إسم النسخة الإحتياطية", MsgBoxStyle.Information)

        '********************************************************************************************
        c = New C
        Dim s As String = "SELECT [UserName],[UserPass]FROM [Users] where user_id = 1"
        c.Com = New SqlClient.SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()

                Dim Writer As System.IO.StreamWriter
                Writer = New System.IO.StreamWriter(BackupPath + "UserAdmin.txt") '<-- Where to create/write to
                Writer.Write("الرقم السري لمدير " + My.Application.Info.AssemblyName.ToString + ", النسخة الإحتياطية لتاريخ " + Date.Now + vbNewLine _
                             + "المستخــدم : " + c.Dr("UserName") + vbNewLine + "الــرقم الســري : " + c.Dr("UserPass"))
                Writer.Close()

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

        Dim strAlter As String

        strAlter = "RESTORE DATABASE " & NewDBTextBox.Text & " FROM  DISK = '" & BackupPath & "' WITH  FILE = 1, " &
            " MOVE '" & My_Settings.DataBase + "' TO '" & FLpath & "\" & NewDBTextBox.Text & ".mdf', " &
            "  MOVE '" & My_Settings.DataBase + "_log' TO '" & FLpath & "\" & NewDBTextBox.Text & ".ldf', NOUNLOAD,  REPLACE,  STATS = 5"


        Dim commAlter As SqlCommand = New SqlCommand(strAlter, c.Con)

        Try
            c.Con.Open()
            commAlter.ExecuteNonQuery()
            MsgBox("Database has been altered successfully", MsgBoxStyle.Information, "")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        c.Con.Close()


        'End If


        If is_ReadOnly_CB.Checked = True Then
            Try
                c = New C
                s = "ALTER DATABASE [" & NewDBTextBox.Text & "] SET READ_ONLY "
                c.Com = New SqlClient.SqlCommand(s, c.Con)

                If SQL_SP_EXEC(c.Com) = True Then
                    MsgBox("تـم  تحديد القاعدة للقراءة فقط ", MsgBoxStyle.Information)
                    Application.Restart()
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Delete_AGBalance_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Delete_AGBalance_CB.CheckedChanged
        CB_CHecked(sender)
        check_enabled_ConfirmButtom()
    End Sub

    Private Sub BackupButtonX_Click(sender As Object, e As EventArgs) Handles BackupButtonX.Click
        Dim Path As String
        SaveFileDialog1.FileName = My_Settings.DataBase

        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            Path = SaveFileDialog1.FileName + ".bak"
            If query("backup database " & MY_Settings.DataBase & " to disk='" & Path & "'") = 1 Then MsgBox("تــم أخد النسخــة الإحتيـاطية مرفقة بملف نصي يحتوي على بيانات المستخدم الرئيسي وبنفس إسم النسخة الإحتياطية", MsgBoxStyle.Information)

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

    Private Sub Keep_QTY_Rd_CheckedChanged(sender As Object, e As EventArgs) Handles Keep_QTY_Rd.CheckedChanged
        CB_CHecked(sender)
        If sender.Checked = True Then Jrd_ID_Txt.Enabled = False
    End Sub

    Private Sub Delete_ALL_Qty_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Delete_ALL_Qty_CB.CheckedChanged
        CB_CHecked(sender)
        If sender.Checked = True Then Jrd_ID_Txt.Enabled = False
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub SysDelete_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Delete_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Delete_ST_CB.CheckedChanged
        CB_CHecked(sender)
        check_enabled_ConfirmButtom()
    End Sub

    Private Sub is_ReadOnly_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_ReadOnly_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub


    Private Sub Keep_Specific_Rd_CheckedChanged(sender As Object, e As EventArgs) Handles Keep_Specific_Rd.CheckedChanged
        CB_CHecked(sender)
        If Delete_ALL_Qty_CB.Checked = True Then
            Jrd_ID_Txt.Enabled = False
            Jrd_ID_Txt.Clear()
        Else
            Jrd_ID_Txt.Enabled = True
        End If
    End Sub
End Class