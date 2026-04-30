Imports System.Drawing
Imports System.Windows.Forms

Public Class SysOptions
    Dim rs As New Resizer

    ' =========================================================
    ' 🌟 إضافة ظل احترافي (Drop Shadow) للفورم الفريملس 🌟
    ' =========================================================
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    Private Sub SysOptions_Test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' 1. تحديد التاجات لكل زر وكل عنصر بشكل فردي ويدوي (بدون استخدام For Each نهائياً)
            If TitleBar_Panel IsNot Nothing Then TitleBar_Panel.Tag = "HEADER"
            If Title_Label IsNot Nothing Then Title_Label.Tag = "TITLE_TRANSPARENT"
            If ExitFormButton IsNot Nothing Then ExitFormButton.Tag = "DELETE"
            If MaxFormButton IsNot Nothing Then MaxFormButton.Tag = "GENERAL"
            If F_Panel IsNot Nothing Then F_Panel.Tag = "TRANSPARENT"

            Button12.Tag = "GENERAL"
            Button13.Tag = "GENERAL"
            Button14.Tag = "GENERAL"
            Button15.Tag = "GENERAL"
            Button17.Tag = "GENERAL"
            Button18.Tag = "GENERAL"
            Tree_AG_Button.Tag = "GENERAL"
            ST_Button.Tag = "GENERAL"
            Pch_Exp_Btn.Tag = "GENERAL"
            Cities_btn.Tag = "GENERAL"
            MK_Btn.Tag = "GENERAL"
            Network_Wacher_btn.Tag = "GENERAL"
            Button4.Tag = "GENERAL"
            SubPrinterButton.Tag = "GENERAL"
            CardAgent_Btn.Tag = "GENERAL"
            Button8.Tag = "GENERAL"
            Shurtcut_Btn.Tag = "GENERAL"
            TableButton.Tag = "GENERAL"
            Button1.Tag = "GENERAL"

            ' 2. تطبيق الثيم
            ThemeManager.ApplyThemeToForm(Me)

            ' 3. أكوادك الأصلية كما هي
            'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
            rs.FindAllControls(Me)
            Me.WindowState = FormWindowState.Maximized
            Check_Sys_Featurs()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Check_Sys_Featurs()
        TableButton.Visible = S_Tables
        SubPrinterButton.Visible = S_SubPrints
        If SScreenDefault = 1 Then CardAgent_Btn.Visible = False
        If SScreenDefault <> 1 Then
            Shurtcut_Btn.Visible = False
            CardAgent_Btn.Visible = False
        End If
        MK_Btn.Visible = S_Marketers
        Cities_btn.Visible = S_Out_Travel
        Pch_Exp_Btn.Visible = S_Exp_Pch
        CardAgent_Btn.Visible = S_AgentCard
    End Sub

    Private Sub SysOptions_Test_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    ' =========================================================
    ' 🌟 أكواد السحب وتكبير الشاشة للشريط العلوي 🌟
    ' =========================================================
    Dim drag As Boolean, mouseX As Integer, mouseY As Integer
    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, Title_Label.MouseDown
        drag = True : mouseX = Cursor.Position.X - Me.Left : mouseY = Cursor.Position.Y - Me.Top
    End Sub
    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, Title_Label.MouseMove
        If drag Then Me.Location = New Point(Cursor.Position.X - mouseX, Cursor.Position.Y - mouseY)
    End Sub
    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, Title_Label.MouseUp
        drag = False
    End Sub

    Private Sub MaxFormButton_Click(sender As Object, e As EventArgs) Handles MaxFormButton.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
            MaxFormButton.Text = "⬜"
        Else
            Me.WindowState = FormWindowState.Maximized
            MaxFormButton.Text = "🗗"
        End If
    End Sub

    ' =========================================================
    ' 🚀 أحداث الأزرار الأصلية بالضبط (تطابق الكود الذي رفعته لي) 🚀
    ' =========================================================
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub SysOptions_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        F_MainForm.Check_Sys_Featurs()
        'F_MainForm.Fill_ALL_IM()
        'F_MainForm.F_Load()
        Me.Dispose()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles SubPrinterButton.Click
        F_Printers = New Printers
        F_Printers.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        F_SB_Edit = New SB_Edit
        F_SB_Edit.ShowDialog()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Shurtcut_Btn.Click
        Dim F_POS_Shortcut_Factory = New POS_Shortcut_Factory
        F_POS_Shortcut_Factory.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        F_POS_Shortcut_Factory.ShowDialog()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        F_users = New users
        F_users.ShowDialog()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles CardAgent_Btn.Click
        Dim F_AgentsTypesOptions = New AgentsTypesOptions
        F_AgentsTypesOptions.ShowDialog()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles TableButton.Click
        F_TablesCard = New TablesCard
        F_TablesCard.ShowDialog()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        F_ExpencesCard = New EXP_Card
        ' Set_Form(F_ExpencesCard, F_Panel)
        F_ExpencesCard.ShowDialog()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        F_TreasuryCard = New Tr_Card
        ' Set_Form(F_TreasuryCard, F_Panel)
        ' F_TreasuryCard.ShowDialog()
        Tr_Card.ShowDialog()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        F_Backup = New Backup
        '  Set_Form(F_Backup, Me.F_Panel)
        F_Backup.ShowDialog()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        ServerOptions.UserPassGroupBox.Visible = False
        ServerOptions.UserNameLabel.Visible = False
        ServerOptions.PassWordLabel.Visible = False
        ' ServerOptions.Serv_Desc_txt.Enabled = False

        ServerOptions.CmbSeverName.Text = MY_Settings.S_SERVER
        ServerOptions.CmbAuth.SelectedItem = MY_Settings.DB_Authentication

        If ServerOptions.CmbAuth.SelectedIndex = 1 Then
            ServerOptions.TxtUserName.Text = MY_Settings.DB_UName
            ServerOptions.TxtPassWord.Text = MY_Settings.DB_Pass
            ServerOptions.CmbDataBase_DropDown(sender, e)
        End If

        ServerOptions.CmbSeverName.Enabled = False
        ServerOptions.CmbAuth.Enabled = False
        ServerOptions.ShowDialog()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        F_SysDelete = New SysDelete
        F_SysDelete.ShowDialog()
    End Sub

    Private Sub Network_Wacher_btn_Click(sender As Object, e As EventArgs) Handles Network_Wacher_btn.Click
        Network_Tracker.ShowDialog()
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        User_Valids.ShowDialog()
    End Sub

    Private Sub MK_Btn_Click(sender As Object, e As EventArgs) Handles MK_Btn.Click
        Marketers.ShowDialog()
    End Sub

    Private Sub Cities_btn_Click(sender As Object, e As EventArgs) Handles Cities_btn.Click
        Cities_Card.ShowDialog()
    End Sub

    Private Sub Pch_Exp_Btn_Click(sender As Object, e As EventArgs) Handles Pch_Exp_Btn.Click
        PCH_EXP_Card.ShowDialog()
    End Sub

    Private Sub ST_Button_Click(sender As Object, e As EventArgs) Handles ST_Button.Click
        F_STORES = New STORES
        F_STORES.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frm As New FrmAccountingPostingSettings(MY_Settings.SqlConStr, USER_ID)
        frm.ShowDialog()
    End Sub

    Private Sub Tree_AG_Button_Click(sender As Object, e As EventArgs) Handles Tree_AG_Button.Click
        Dim f As New Agent_Balance_For_Tree
        f.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim f As New frmPaymentDefaultAccount
        f.ShowDialog()
    End Sub
End Class