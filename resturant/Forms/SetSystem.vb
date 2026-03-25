Public Class SetSystem
    Private is_select As Boolean = False
    Private Sub SetSystem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_SetSystem()

    End Sub

    Public Sub Load_SetSystem()
        Dim c As New C
        Try
            Dim sql As String = " select T_ID,Type_Name from Sys_Types "
            c.Da = New SqlClient.SqlDataAdapter(sql, c.Con)
            c.Da.Fill(c.Dt)
            Sys_Type_cm.DataSource = c.Dt
            Sys_Type_cm.DisplayMember = "Type_Name"
            Sys_Type_cm.ValueMember = "T_ID"
            is_select = True

            c = New C
            c.Str = "Select T_ID From Sys_Model "
            c.Com = New SqlClient.SqlCommand(c.Str, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                Sys_Type_cm.SelectedValue = c.Dr("T_ID")
                Select_Sys()
            End If
            c.Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If Not String.IsNullOrWhiteSpace(My_Settings.MAINFORM_BK) Then
            Me.IM_Photo.Image = Image.FromFile(System.IO.Path.GetFullPath(My_Settings.MAINFORM_BK))
        Else
            Me.IM_Photo.Image = Nothing
        End If

        App_Suuply_cm.Text = MY_Settings.App_Suuply

        NumOfBillsTest_txt.Text = MY_Settings.NumOfBillsTest
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        Sys_Features_Update()
        'My_Settings.App_Suuply = App_Suuply_cm.Text
        Save_AppSetting()
    End Sub

    Private Sub Sys_Features_Update()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "Sys_Features_Update"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Sys_Type_cm.SelectedValue)
            .Parameters.AddWithValue("@Pr", Pr_CB.Checked)
            .Parameters.AddWithValue("@Tables", Tables_CB.Checked)
            .Parameters.AddWithValue("@SubPrints", SubPrints_CB.Checked)
            .Parameters.AddWithValue("@Notes", Notes_CB.Checked)
            .Parameters.AddWithValue("@Orders", Orders_CB.Checked)
            .Parameters.AddWithValue("@Stores", Stores_CB.Checked)
            .Parameters.AddWithValue("@Frm", Frm_CB.Checked)
            .Parameters.AddWithValue("@inside_Bill", inside_Bill_CB.Checked)
            .Parameters.AddWithValue("@SScreenDefault", SScreenDefault_Cmb.SelectedIndex)
            .Parameters.AddWithValue("@AgentCards", Agent_Cards_CB.Checked)
            .Parameters.AddWithValue("@IM_Default_Stut", IM_Type_cm.SelectedIndex)
            .Parameters.AddWithValue("@Marketers", Marketers_CB.Checked)
            .Parameters.AddWithValue("@SerialCode", SerialCode_CB.Checked)
            .Parameters.AddWithValue("@Out_Travel", Out_Travel_CB.Checked)
            .Parameters.AddWithValue("@Allow_MinSP", Allow_MinSP_CB.Checked)
            .Parameters.AddWithValue("@Exp_Pch", Exp_Pch_CB.Checked)
            .Parameters.AddWithValue("@Phone_For_Tables", Phone_For_Tables_CB.Checked)
            .Parameters.AddWithValue("@is_Auto_Recive_ST_Tran", is_Auto_Recive_ST_Tran_CB.Checked)
            .Parameters.AddWithValue("@IM_Valid", IM_Valid_CB.Checked)
            '.Parameters.AddWithValue("@TB_Auto_PrintCB", TB_Auto_Print_CB.Checked)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم الحفظ", MsgBoxStyle.Information)
            Sys_Switch()
        End If


    End Sub

    Private Sub Select_Sys()
        Try
            Dim C As New C
            C.Str = "Select * From Sys_Features WHERE T_ID = '" & Sys_Type_cm.SelectedValue & "'"
            C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
            C.Con.Open()

            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                Pr_CB.Checked = C.Dr("Pr")
                Tables_CB.Checked = C.Dr("Tables")
                SubPrints_CB.Checked = C.Dr("SubPrints")
                Notes_CB.Checked = C.Dr("Notes")
                Orders_CB.Checked = C.Dr("Orders")
                Stores_CB.Checked = C.Dr("Stores")
                Frm_CB.Checked = C.Dr("Frm")
                inside_Bill_CB.Checked = C.Dr("inside_Bill")
                SScreenDefault_Cmb.SelectedIndex = C.Dr("SScreenDefault")
                Agent_Cards_CB.Checked = C.Dr("AgentCards")
                IM_Type_cm.SelectedIndex = C.Dr("IM_Default_Stut")
                Marketers_CB.Checked = C.Dr("Marketers")
                SerialCode_CB.Checked = C.Dr("SerialCode")
                Out_Travel_CB.Checked = C.Dr("Out_Travel")
                Allow_MinSP_CB.Checked = C.Dr("Allow_MinSP")
                Exp_Pch_CB.Checked = C.Dr("Exp_Pch")
                Phone_For_Tables_CB.Checked = C.Dr("Phone_For_Tables")
                is_Pch_Discount_Distribute_CB.Checked = C.Dr("is_Pch_Discount_Distribute")
                is_Auto_Recive_ST_Tran_CB.Checked = C.Dr("is_Auto_Recive_ST_Tran")
                IM_Valid_CB.Checked = C.Dr("IM_Valid")
            End If
            C.Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SetSystem_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Pr_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Pr_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Tables_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Tables_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SubPrints_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SubPrints_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Notes_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Notes_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Orders_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Orders_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Stores_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Stores_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Frm_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Frm_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub inside_Bill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles inside_Bill_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Agent_Cards_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Agent_Cards_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Sys_Type_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Sys_Type_cm.SelectedValueChanged
        If is_select = True Then Select_Sys()
    End Sub

    Private Sub Marketers_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Marketers_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SerialCode_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SerialCode_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Out_Travel_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Out_Travel_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub IMPH_Btn_Click(sender As Object, e As EventArgs) Handles IMPH_Btn.Click
        Dim OpenFL As New OpenFileDialog With {.Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico",
                                              .Multiselect = False, .Title = "إختر صورة"}
        OpenFL.InitialDirectory = Application.StartupPath + "\MAINFORM IMG\"
        If OpenFL.ShowDialog = Windows.Forms.DialogResult.OK Then
            IM_Photo.Image = Image.FromFile(System.IO.Path.GetFullPath(OpenFL.FileName))
            MY_Settings.MAINFORM_BK = System.IO.Path.GetFullPath(OpenFL.FileName)
            Save_AppSetting()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        IM_Photo.Image = Nothing
        MY_Settings.MAINFORM_BK = ""
        Save_AppSetting()
    End Sub

    Private Sub Fonts_Path_Lb_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles Fonts_Path_Lb.LinkClicked
        Dim OpenFL As New OpenFileDialog With {.Multiselect = False, .Title = "حمل خطوط النظام"}
        OpenFL.InitialDirectory = Application.StartupPath + "\Fonts\"
        OpenFL.ShowDialog()
    End Sub

    Private Sub Allow_MinSP_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Allow_MinSP_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Exp_Pch_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Exp_Pch_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Phone_For_Tables_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Phone_For_Tables_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub is_Pch_Discount_Distribute_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Pch_Discount_Distribute_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub is_Auto_Recive_ST_Tran_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Auto_Recive_ST_Tran_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub IM_Valid_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IM_Valid_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.Count > 0 Then

            If TextBox1.Text = ((Convert.ToInt16(Date.Now.Day)) + (Convert.ToInt16(Date.Now.Month)) + (Convert.ToInt16(Date.Now.Year))) * ((Convert.ToInt16(Date.Now.Day)) * 2) Then
                Panel1.Visible = True
            Else
                Panel1.Visible = False
            End If

        Else
            Panel1.Visible = False

        End If
    End Sub

    Private Sub NumOfBillsTest_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles NumOfBillsTest_txt.KeyDown
        If e.KeyCode = Keys.Return Then
            MY_Settings.NumOfBillsTest = NumOfBillsTest_txt.Text
            MY_Settings.Save_AppSetting()
            MsgBox("UPDATE")
        End If
    End Sub

    Private Sub TB_Auto_PrintCB_CheckedChanged(sender As Object, e As EventArgs) Handles TB_Auto_Print_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub
End Class