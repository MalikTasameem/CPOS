Public Class PchSearch : Inherits System.Windows.Forms.Form
    Dim rs As New Resizer
    Dim Bills_DT As New DataTable
    Dim Dv As New DataView
    Dim BalanceType As String = ""

    Private Sub ExpSearch_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub ExpSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.ControlKey Then SearchName_txtb.Select()
    End Sub

    Private Sub ExpSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        AGMetroGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing 'or even better .DisableResizing. Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders


        For i = 0 To AGMetroGrid.Columns.Count - 1
            AGMetroGrid.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i
        'If FormType = 1 Then
        '    If F_Sales.AG_ID > 1 Then LastLabel.Visible = False
        '    AGMetroGrid.Columns("Proj_NAME_CL").Visible = True
        'Else
        '    AGMetroGrid.Columns("Proj_NAME_CL").Visible = False
        'End If

        Load_Data(0)
        Make_Hints()
        Me.WindowState = FormWindowState.Maximized
        '   SearchName_txtb.Select()
    End Sub

    Private Sub Make_Hints()
        SendMessage(SearchName_txtb.Handle, &H1501, 0, "إبحث عن فاتورة")
    End Sub

    Private Sub Load_Data(AG_ID As Integer)
        Bills_DT.Clear()
        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "Balance_MV_V_SELECT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Form_Type", FormType)
            .Parameters.AddWithValue("@isVoid", isDeletedCheckBox.Checked)
            .Parameters.AddWithValue("@AG_ID", AG_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Bills_DT)
        AGMetroGrid.DataSource = Bills_DT
    End Sub

    Private Sub SearchName_txtb_Enter(sender As Object, e As EventArgs) Handles SearchName_txtb.Enter
        Arabic_Lang()
    End Sub

    Private Sub SearchName_txtb_KeyDown(sender As Object, e As KeyEventArgs) Handles SearchName_txtb.KeyDown
        If e.KeyCode = Keys.Down Then AGMetroGrid.Select()
    End Sub

    Private Sub SearchName_txtb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SearchName_txtb.KeyPress
        If e.KeyChar = "'" Then e.Handled = True
    End Sub

    Private Sub SearchName_txtb_TextChanged(sender As Object, e As EventArgs) Handles SearchName_txtb.TextChanged
        Dv = Bills_DT.DefaultView
        Dv.RowFilter = " Search Like '%" & sender.Text & "%'"
        AGMetroGrid.DataSource = Dv
    End Sub

    Private Sub ExpSearch_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub ClearSearchButton_Click(sender As Object, e As EventArgs) Handles ClearSearchButton.Click
        SearchName_txtb.Clear()
    End Sub

    Private Sub AGMetroGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles AGMetroGrid.KeyDown
        If e.KeyCode = Keys.Return Then Move_To_Select()
    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        Move_To_Select()
    End Sub

    Private Sub Move_To_Select()
        If AGMetroGrid.Rows.Count > 0 Then Select_ExpBill(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        Me.Close()
    End Sub


    Private Sub isDeletedCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles isDeletedCheckBox.CheckedChanged
        Load_Data(0)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub
End Class