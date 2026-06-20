Public Class Cost_Center_Control
    Private Sub Cost_Center_Control_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CostCenter_Datatable.Rows.Count > 0 Then
            COST_CM.DataSource = CostCenter_Datatable
            COST_CM.DisplayMember = "COST_NAME"
            COST_CM.ValueMember = "COST_ID"
        End If
    End Sub

    Public Sub Set_CHECK_ALL_VISIBLE(Optional VISIBLE As Boolean = True)
        ALL_CheckBox.Visible = VISIBLE
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles ALL_CheckBox.CheckedChanged
        CB_CHecked(sender)
        If ALL_CheckBox.Checked = True Then
            COST_CM.SelectedIndex = -1
            COST_CM.Enabled = False
        Else
            COST_CM.SelectedIndex = False
            COST_CM.Enabled = True
        End If
    End Sub

End Class
