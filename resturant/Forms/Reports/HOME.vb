Imports System.Data.SqlClient
Imports System.IO

Public Class HOME

    Dim rs As New Resizer


    Private Sub main_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        rs.ResizeAllControls(Me)
    End Sub



    'Public Sub Remove_Form()
    '    TabControl1.TabPages.Remove(TabControl1.SelectedTab)
    'End Sub


    Public Sub Set_Form(ByRef Ctrl As Form, F_NAME As String, F_TEXT As String)


        For Each A As TabPage In TabControl1.TabPages
            If A.Name = F_NAME Then
                TabControl1.SelectedIndex = A.TabIndex
                Exit Sub
            End If
        Next

        Dim page = New TabPage()
        page.Name = F_NAME
        page.Text = F_TEXT
        page.Controls.Add(New Form With {.TopMost = False, .TopLevel = False, .FormBorderStyle = FormBorderStyle.None, .Dock = DockStyle.Fill})


        TabControl1.TabPages.Add(page)

        page.Controls.Clear()
        Ctrl.TopLevel = False
        Ctrl.Visible = True
        Ctrl.Dock = DockStyle.Fill
        page.Controls.Add(Ctrl)

        Ctrl.Parent = page
        Ctrl.Size = New System.Drawing.Size(page.Size.Width, page.Size.Height)
        page.Tag = Ctrl.Name
        TabControl1.SelectedTab = page
    End Sub

    Public Sub Remove_Form(Tab_Name As StreamReader)
        TabControl1.TabPages.Remove(TabControl1.SelectedTab)
    End Sub

    Private Sub HOME_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        Check_Sys_Featurs()
    End Sub

    Private Sub Check_Sys_Featurs()


        Pr_Reports_CB.Visible = S_Pr
        Inside_CB.Visible = S_In_Out_side
        Notes_CB.Visible = S_Notes
        Marketers_CB.Visible = S_Marketers
        Frm_CB.Visible = S_Frm
        PCH_Exp_CB.Visible = S_Exp_Pch
        TABLES_MV_CB.Visible = S_Tables

    End Sub


    Private Sub HOME_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub


    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    'Private Sub POS_CB_Click(sender As Object, e As EventArgs) Handles POS_CB.Click

    '    Dim F As New POS_Report
    '    Set_Form(F, "POS_Report", sender.Text)
    'End Sub


    'Private Sub SB_CB_Click(sender As Object, e As EventArgs) Handles SB_CB.Click

    '    Dim F As New SB_Reports
    '    Set_Form(F, "SB_Reports", sender.Text)
    'End Sub

    Private Sub IM_CB_Click(sender As Object, e As EventArgs) Handles IM_CB.Click

        Dim F As New IM_Report
        Set_Form(F, "IM_Report", sender.Text)
    End Sub

    Private Sub PCH_CB_Click(sender As Object, e As EventArgs) Handles PCH_CB.Click

        Dim F As New Pch_Report
        Set_Form(F, "Pch_Report", sender.Text)
    End Sub

    Private Sub ST_Tran_CB_Click(sender As Object, e As EventArgs) Handles ST_Tran_CB.Click

        Dim F As New ST_Tran_Report
        Set_Form(F, "ST_Tran_Report", sender.Text)
    End Sub

    'Private Sub AGMV_CB_Click(sender As Object, e As EventArgs) Handles AGMV_CB.Click

    '    Dim F As New AG_MV
    '    Set_Form(F, "AG_MV", sender.Text)
    'End Sub

    Private Sub IM_EX_CB_Click(sender As Object, e As EventArgs) Handles IM_EX_CB.Click

        Dim F As New IMEX_Report
        Set_Form(F, "IMEX_Report", sender.Text)
    End Sub

    Private Sub Inside_CB_Click(sender As Object, e As EventArgs) Handles Inside_CB.Click

        Dim F As New Inside_Report
        Set_Form(F, "Inside_Report", sender.Text)
    End Sub

    Public F_Exp_Report As New Exp_Report
    Private Sub EXP_CB_Click(sender As Object, e As EventArgs) Handles EXP_CB.Click
        Set_Form(F_Exp_Report, "Exp_Report", sender.Text)
    End Sub

    Private Sub IM_Perfet_CB_Click(sender As Object, e As EventArgs) Handles IM_Perfet_CB.Click

        Dim F As New IMP_Perfet_Report_1
        Set_Form(F, "IMP_Perfet_Report_1", sender.Text)
    End Sub

    'Private Sub Notes_CB_Click(sender As Object, e As EventArgs) Handles Notes_CB.Click

    '    Dim F As New Notes_Report
    '    Set_Form(F, "Notes_Report", sender.Text)
    'End Sub

    'Private Sub Marketers_CB_Click(sender As Object, e As EventArgs) Handles Marketers_CB.Click
    '    Dim F As New Costmers_Bills_Report
    '    Set_Form(F, "Costmers_Bills_Report", sender.Text)

    'End Sub

    Private Sub Frm_CB_Click(sender As Object, e As EventArgs) Handles Frm_CB.Click


    End Sub

    Private Sub PCH_Exp_CB_Click(sender As Object, e As EventArgs) Handles PCH_Exp_CB.Click

        Dim F As New PCH_EXP_Report
        Set_Form(F, "PCH_EXP_Report", sender.Text)
    End Sub

    'Private Sub Costmers_Bills_Report_CB_Click(sender As Object, e As EventArgs) Handles Costmers_Bills_Report_CB.Click

    '    Dim F As New Costmers_Bills_Report
    '    Set_Form(F, "Costmers_Bills_Report", sender.Text)
    'End Sub

    Private Sub ToolStripDropDownButton1_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton1.Click
        Dim F As New IMP_Perfet_Report_2
        Set_Form(F, "IMP_Perfet_Report_2", sender.Text)
    End Sub

    Private Sub TABLES_MV_CB_Click(sender As Object, e As EventArgs) Handles TABLES_MV_CB.Click
        Dim F As New TABLES_MV_Report
        Set_Form(F, "TABLES_MV_Report", sender.Text)
    End Sub

    'Private Sub IMStruct_MV_CB_Click(sender As Object, e As EventArgs) Handles IMStruct_MV_CB.Click
    '    Dim F As New IM_Struct_MV_Report
    '    Set_Form(F, "IM_Struct_MV_Report", sender.Text)
    'End Sub

    'Private Sub ToolStripDropDownButton2_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton2.Click
    '    Dim F As New Pr_Report
    '    Set_Form(F, "Pr_Report", sender.Text)
    'End Sub

    Private Sub ToolStripDropDownButton3_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton3.Click
        Dim F As New IM_CYRCLE
        Set_Form(F, "IM_CYRCLE", sender.Text)
    End Sub

    Private Sub ToolStripDropDownButton5_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton5.Click
        Dim F As New IM_GROUPING
        Set_Form(F, "IM_GROUPING", sender.Text)
    End Sub

    Private Sub تقريرالمبيعاتToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تقريرالمبيعاتToolStripMenuItem.Click

        Dim F As New POS_Report
        Set_Form(F, "POS_Report", sender.Text)
    End Sub

    Private Sub Pr_Reports_CB_Click(sender As Object, e As EventArgs) Handles Pr_Reports_CB.Click
        Dim F As New Pr_Report
        Set_Form(F, "Pr_Report", sender.Text)
    End Sub

    Private Sub SB_CB_Click(sender As Object, e As EventArgs) Handles SB_CB.Click
        Dim F As New SB_Reports
        Set_Form(F, "SB_Reports", sender.Text)
    End Sub

    Private Sub Costmers_Bills_Report_CB_Click(sender As Object, e As EventArgs) Handles Costmers_Bills_Report_CB.Click
        Dim F As New Costmers_Bills_Report
        Set_Form(F, "Costmers_Bills_Report", sender.Text)
    End Sub

    Private Sub Marketers_CB_Click(sender As Object, e As EventArgs) Handles Marketers_CB.Click
        Dim F As New Costmers_Bills_Report
        Set_Form(F, "Costmers_Bills_Report", sender.Text)
    End Sub

    Private Sub Notes_CB_Click(sender As Object, e As EventArgs) Handles Notes_CB.Click
        Dim F As New Notes_Report
        Set_Form(F, "Notes_Report", sender.Text)
    End Sub

    Private Sub AGMV_CB_Click(sender As Object, e As EventArgs) Handles AGMV_CB.Click
        Dim F As New AG_MV
        Set_Form(F, "AG_MV", sender.Text)
    End Sub

    Private Sub IMStruct_MV_CB_Click(sender As Object, e As EventArgs) Handles IMStruct_MV_CB.Click
        Dim F As New IM_Struct_MV_Report
        Set_Form(F, "IM_Struct_MV_Report", sender.Text)
    End Sub

    Private Sub AG_EMP_Percent_CB_Click(sender As Object, e As EventArgs) Handles AG_EMP_Percent_CB.Click
        Dim F As New AG_EMP_PERCENT_Report
        Set_Form(F, "AG_EMP_PERCENT_Report", sender.Text)
    End Sub

    Private Sub تفاصيلالتصنيــعToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تفاصيلالتصنيــعToolStripMenuItem.Click
        Dim F As New Frm_Report
        Set_Form(F, "Frm_Report", sender.Text)
    End Sub

    Private Sub فواتيــرالتصنيعToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles فواتيــرالتصنيعToolStripMenuItem.Click
        Dim F As New Frm_Report_2
        Set_Form(F, "Frm_Report_2", sender.Text)
    End Sub

    Private Sub ToolStripDropDownButton2_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton2.Click
        Dim F As New TR_TRANS_Report
        Set_Form(F, "TR_TRANS_Report", sender.Text)
    End Sub
End Class