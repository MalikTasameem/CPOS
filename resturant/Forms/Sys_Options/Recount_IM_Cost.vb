Imports System.Data.SqlClient

Public Class Recount_IM_Cost

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub UpdateGB_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Fast_SB_Discount_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Discount_txt_TextChanged(sender As Object, e As EventArgs)
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Fast_SB_Discount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Markter_Panel.Visible = S_Marketers
        Me.Text += " : " & F_ItemsMenu.IM_SH_txt.Text

        Cost_txt.Text = F_ItemsMenu.IM_Cost_txt.Text
        Markter_Val_txt.Text = F_ItemsMenu.Markter_Val_txt.Text

    End Sub

    Private Sub Discount_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cost_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub UpdateGBButton_Click(sender As Object, e As EventArgs) Handles UpdateGBButton.Click

        If MessageBox.Show(" تدوير الصنف " & F_ItemsMenu.IM_SH_txt.Text & " من تاريخ " & Pr_DateRange.D_F.Value.ToString & " إلى " & Pr_DateRange.D_T.Value.ToString, "تأكيد", MessageBoxButtons.OKCancel, _
                              MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then Recount_IM_Cost()
    End Sub

    Public Sub Recount_IM_Cost()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "Recount_IM_Cost"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", F_ItemsMenu.IM_ID)


            .Parameters.AddWithValue("@NewCOST", Cost_txt.Text)
            .Parameters.AddWithValue("@Edit_On_Card", Edit_OnCard_CB.Checked)

            .Parameters.AddWithValue("@NewMarkVal", Markter_Val_txt.Text)
            .Parameters.AddWithValue("@Edit_MarkVal_On_Card", Edit_Markval_OnCard_CB.Checked)


            .Parameters.AddWithValue("@D_F", Pr_DateRange.D_F.Value)
            .Parameters.AddWithValue("@D_T", Pr_DateRange.D_T.Value)

            .Parameters.AddWithValue("@Calc_COST", Cost_CB.Checked)
            .Parameters.AddWithValue("@Calc_MarkVal", Markval_CB.Checked)

        End With
        If SQL_SP_EXEC(c.Com) = True Then

            Network_Edit_Tracker_insert(" تدوير تكلفة الصنف : " & F_ItemsMenu.IM_SH_txt.Text + " من تاريخ " & Pr_DateRange.D_F.Value.ToString & " إلى " & Pr_DateRange.D_T.Value.ToString & " بتكلفة جديده : " & Cost_txt.Text, 0, F_ItemsMenu.IM_ID, 0)
            MsgBox("تم التدوير", MsgBoxStyle.Information, "")

            If Edit_OnCard_CB.Checked = True Then F_ItemsMenu.IM_Cost_txt.Text = Cost_txt.Text
            If Edit_Markval_OnCard_CB.Checked = True Then F_ItemsMenu.Markter_Val_txt.Text = Markter_Val_txt.Text
        End If

    End Sub

    Private Sub Edit_OnCard_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Edit_OnCard_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Cost_txt_TextChanged(sender As Object, e As EventArgs) Handles Cost_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Markter_Val_txt_TextChanged(sender As Object, e As EventArgs) Handles Markter_Val_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Markter_Val_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Markter_Val_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Edit_Markval_OnCard_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Edit_Markval_OnCard_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Cost_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Cost_CB.CheckedChanged
        CB_CHecked(sender)
        If Cost_CB.Checked Then
            Cost_txt.Enabled = True
            Edit_OnCard_CB.Enabled = True
        Else
            Cost_txt.Enabled = False
            Edit_OnCard_CB.Enabled = False
        End If
    End Sub

    Private Sub Markval_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Markval_CB.CheckedChanged
        CB_CHecked(sender)
        If Markval_CB.Checked Then
            Markter_Val_txt.Enabled = True
            Edit_Markval_OnCard_CB.Enabled = True
        Else
            Markter_Val_txt.Enabled = False
            Edit_Markval_OnCard_CB.Enabled = False
        End If
    End Sub
End Class