Public Class SysOptions
    Dim rs As New Resizer


    Private Sub SysOptions_Test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        Check_Sys_Featurs()


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

        ServerOptions.CmbSeverName.Text = My_Settings.S_SERVER
        ServerOptions.CmbAuth.SelectedItem = My_Settings.DB_Authentication

        If ServerOptions.CmbAuth.SelectedIndex = 1 Then
            ServerOptions.TxtUserName.Text = My_Settings.DB_UName
            ServerOptions.TxtPassWord.Text = My_Settings.DB_Pass

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

    Private Sub Tree_AG_Button_Click(sender As Object, e As EventArgs) Handles Tree_AG_Button.Click
        Dim f As New Agent_Balance_For_Tree
        f.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim f As New frmPaymentDefaultAccount
        f.ShowDialog()
    End Sub
End Class