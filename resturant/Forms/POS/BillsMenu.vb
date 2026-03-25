Public Class BillsMenu
    Dim rs As New Resizer
    Dim Pr_Bill_C As New C
    Dim isPied As Boolean = True
    Private Sub BillsMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Load_Pr_Bils()
        If U_SB_Show_Price_Info = False Then BillsGrid.Columns("Total_CL").Visible = False
        If S_Tables = False Then BillsGrid.Columns("T_Name_CL").Visible = False
    End Sub

    Private Sub BillsMenu_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls_POS(Me)
    End Sub


    Private Sub Load_Pr_Bils()

        Pr_Bill_C = New C
        With (Pr_Bill_C.Com)
            .Connection = Pr_Bill_C.Con
            .CommandText = "SB_Info_V_SELECT_Bill_To_Menu"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
            .Parameters.AddWithValue("@Show_Unpied_CH", isPied)
        End With
        Pr_Bill_C.Da = New SqlClient.SqlDataAdapter(Pr_Bill_C.Com)
        Pr_Bill_C.Da.Fill(Pr_Bill_C.Dt)
        BillsGrid.DataSource = Pr_Bill_C.Dt

    End Sub

    Private Sub BillsGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BillsGrid.CellContentClick
        If e.ColumnIndex = 0 Then
            F_POS.Reset_Fields()
            F_POS.isNewBill = 0
            F_POS.T_ID = BillsGrid.CurrentRow.Cells("T_ID_CL").Value
            F_POS.BillNumTxt.Text = BillsGrid.CurrentRow.Cells("B_Pr_ID_CL").Value
            F_POS.SB_Contents_SELECT_Bill()
            F_POS.Fill_Bill_Info()
            F_POS.BillTypeCmb.SelectedValue = BillsGrid.CurrentRow.Cells("B_Type_CL").Value
            F_SalesMenu.Hide()
        End If

        If e.ColumnIndex = 1 Then
            Dim F As New ShowBill_IM
            F.T_ID = BillsGrid.CurrentRow.Cells("T_ID_CL").Value
            F.BillNumTxt.Text = BillsGrid.CurrentRow.Cells("B_Pr_ID_CL").Value
            F.BillTypeCmb.SelectedValue = BillsGrid.CurrentRow.Cells("B_Type_CL").Value
            F.ShowDialog()
        End If
    End Sub

    Private Sub SB_SearchTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SB_SearchTextBox.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub SB_SearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles SB_SearchTextBox.TextChanged
        On Error Resume Next
        Pr_Bill_C.DV = Pr_Bill_C.Dt.AsDataView
        Pr_Bill_C.DV.RowFilter = "Search LIKE '%" + SB_SearchTextBox.Text + "%'"
        BillsGrid.DataSource = Pr_Bill_C.DV
    End Sub

    Private Sub Show_Unpied_CH_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Unpied_CH.CheckedChanged
        If sender.Checked = True Then
            sender.ForeColor = Color.DarkGreen
            isPied = False
        Else
            sender.ForeColor = Color.Black
            isPied = True
        End If
        Load_Pr_Bils()
    End Sub

    Private Sub B0_Click(sender As Object, e As EventArgs) Handles B0.Click
        SB_SearchTextBox.Text += "0"
    End Sub

    Private Sub B9_Click(sender As Object, e As EventArgs) Handles B9.Click
        SB_SearchTextBox.Text += "9"
    End Sub

    Private Sub B8_Click(sender As Object, e As EventArgs) Handles B8.Click
        SB_SearchTextBox.Text += "8"
    End Sub

    Private Sub B7_Click(sender As Object, e As EventArgs) Handles B7.Click
        SB_SearchTextBox.Text += "7"
    End Sub

    Private Sub B6_Click(sender As Object, e As EventArgs) Handles B6.Click
        SB_SearchTextBox.Text += "6"
    End Sub

    Private Sub B5_Click(sender As Object, e As EventArgs) Handles B5.Click
        SB_SearchTextBox.Text += "5"
    End Sub

    Private Sub B4_Click(sender As Object, e As EventArgs) Handles B4.Click
        SB_SearchTextBox.Text += "4"
    End Sub

    Private Sub B3_Click(sender As Object, e As EventArgs) Handles B3.Click
        SB_SearchTextBox.Text += "3"
    End Sub

    Private Sub B2_Click(sender As Object, e As EventArgs) Handles B2.Click
        SB_SearchTextBox.Text += "2"
    End Sub

    Private Sub B1_Click(sender As Object, e As EventArgs) Handles B1.Click
        SB_SearchTextBox.Text += "1"
    End Sub

    Private Sub ClearNumBtn_Click(sender As Object, e As EventArgs) Handles ClearNumBtn.Click
        SB_SearchTextBox.Clear()
    End Sub
End Class