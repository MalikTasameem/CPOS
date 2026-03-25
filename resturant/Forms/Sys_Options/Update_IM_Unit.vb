Public Class Update_IM_Unit
    Dim Prev_Price As Double
    Dim Prev_Barcode As String

    Private Sub Change_IM_Details_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F12 Then ConfermButton_Click(sender, e)
    End Sub

    Private Sub Change_IM_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IM_LB.Text = F_ItemsMenu.IM_SH_txt.Text
        U_Name_txt.Text = F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("U_Name_CL").Value
        Prev_Price = F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("Price_CL").Value
        PriceTextBox.Text = F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("Price_CL").Value
        isInvoice_CB.Checked = F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("is_Default_CL").Value
        Min_SP_txt.Text = F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("Min_SP_CL").Value
        Min_SP_2_txt.Text = F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("Min_SP_2_CL").Value
        Min_SP_Panel.Visible = S_Allow_MinSP
        BarCode_txt.Text = F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("Barcode_CL").Value
        Prev_Barcode = F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("Barcode_CL").Value
    End Sub

    Private Sub ConfermButton_Click(sender As Object, e As EventArgs) Handles ConfermButton.Click

        If Prev_Barcode <> BarCode_txt.Text Then
            If String.IsNullOrWhiteSpace(BarCode_txt.Text) = False Then
                If S_is_Multi_BAR = False Then
                    IM_Check_Barcode()
                Else
                    IM_Units_UpdatePrice()
                End If
            Else
                IM_Units_UpdatePrice()
            End If

        Else
            IM_Units_UpdatePrice()
        End If

    End Sub

    Private Sub IM_Check_Barcode()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Barcode"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Barcode", BarCode_txt.Text)
            .Parameters.AddWithValue("@IM_ID", F_ItemsMenu.IM_ID)
            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then

                If .Parameters("@F").Value > 0 Then
                    MsgBox("باركود متكرر", MsgBoxStyle.Critical, "خطأ")
                    BarCode_txt.Select()
                    BarCode_txt.Focus()
                Else
                    IM_Units_UpdatePrice()
                End If

            End If
        End With
    End Sub

    Private Sub IM_Units_UpdatePrice()

        If String.IsNullOrWhiteSpace(PriceTextBox.Text) Then PriceTextBox.Text = 0
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "IM_Units_UpdatePrice"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@U_IM_ID", F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("U_IM_ID_CL").Value)
            .Parameters.AddWithValue("@Price", PriceTextBox.Text)
            If Not String.IsNullOrWhiteSpace(Min_SP_txt.Text) Then .Parameters.AddWithValue("@Min_SP", Min_SP_txt.Text)
            If Not String.IsNullOrWhiteSpace(Min_SP_2_txt.Text) Then .Parameters.AddWithValue("@Min_SP_2", Min_SP_2_txt.Text)
            .Parameters.AddWithValue("@is_Default", isInvoice_CB.Checked)
            .Parameters.AddWithValue("@Barcode", BarCode_txt.Text)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert(" (من واجهة الأصناف) تعديل بيانات الصنف : " + IM_LB.Text + " / الوحدة : " + U_Name_txt.Text + " / سعر البيع : " + PriceTextBox.Text + " / أساسي : " _
                                        + Convert.ToInt16(isInvoice_CB.Checked).ToString + " السعر السابق : " + Prev_Price.ToString, F_ItemsMenu.IM_ID _
                                       , 20, 3)
            F_ItemsMenu.IM_Units_Select()
            If isInvoice_CB.Checked = True Then F_ItemsMenu.IM_Select_Qty()
            Me.Close()
        End If
    End Sub

    Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles U_Name_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles U_Name_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub


    Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PriceTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs) Handles PriceTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub isInvoice_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isInvoice_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Update_IM_Unit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Min_SP_txt_TextChanged(sender As Object, e As EventArgs) Handles Min_SP_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Min_SP_2_txt_TextChanged(sender As Object, e As EventArgs) Handles Min_SP_2_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Min_SP_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Min_SP_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Min_SP_2_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Min_SP_2_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub
End Class