Public Class ADD_Pch_Exp

    Private Sub OrderDeliver_btn_Click(sender As Object, e As EventArgs) Handles OrderDeliver_btn.Click
        Pch_Exp_Values_INSERT()
    End Sub
    Public Sub Pch_Exp_Values_INSERT()

        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[Pch_Exp_Values_INSERT]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Pch_T_ID", F_Pch.T_ID)
        sqlComm.Parameters.AddWithValue("@Notes_ID", Notes_cm.SelectedValue)
        If isWithBill_CB.Checked = True Then
            sqlComm.Parameters.AddWithValue("@Value", Convert.ToDouble(CD_Money_txt.Text) * Convert.ToDouble(F_Pch.Cr_Equal_TXT.Text))
        Else
            sqlComm.Parameters.AddWithValue("@Value", Convert.ToDouble(CD_Money_txt.Text))
        End If
        sqlComm.Parameters.AddWithValue("@isWithBill", isWithBill_CB.Checked)

        If SQL_SP_EXEC(sqlComm) = True Then
            F_Pch.Pch_Contents_SELECT_Bill()
            F_Pch.Pch_Contents_SELECT_EXP()
            Me.Close()
        End If

    End Sub

    Private Sub ADD_Withraw_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub CD_Money_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CD_Money_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub CD_Money_txt_TextChanged(sender As Object, e As EventArgs) Handles CD_Money_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
        If CD_Money_txt.Text.Count > 0 Then
            OrderDeliver_btn.Enabled = True
        Else
            OrderDeliver_btn.Enabled = False
        End If
    End Sub

    Private Sub ADD_Pch_Exp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        TextBox1.Text = "دينار ليبي"
        Load_Notes()
    End Sub



    Public Sub Load_Notes()
        Dim c As New C
        Dim s As String = "select Ex_ID,Ex_Name from Pch_Exp_Card ORDER BY Ex_Name Desc"
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        Notes_cm.DataSource = dt
        Notes_cm.DisplayMember = "Ex_Name"
        Notes_cm.ValueMember = "Ex_ID"
    End Sub

    Private Sub isWithBill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isWithBill_CB.CheckedChanged
        CB_CHecked(sender)
        If isWithBill_CB.Checked = True Then
            TextBox1.Text = F_Pch.Cr_CM.Text
        Else
            TextBox1.Text = "دينار ليبي"
        End If
    End Sub
End Class