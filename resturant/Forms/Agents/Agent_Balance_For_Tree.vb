Public Class Agent_Balance_For_Tree


    Dim IM_Dt As New DataTable
    Dim AG_Dt As New DataTable
    Dim TR_Dt As New DataTable
    Dim ST_Dt As New DataTable
    Dim Rs As New Resizer
    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles GENERAL_DataGridView.MouseDoubleClick
        Dim inp = InputBox(" : أدخل كود الحساب للحساب  " & vbNewLine & GENERAL_DataGridView.CurrentRow.Cells("B_NAME_CL").Value)
        If inp <> "" Then
            query("UPDATE Agent_Balance_Tree SET TREE_CODE = '" & inp & "' WHERE T_ID = " & GENERAL_DataGridView.CurrentRow.Cells("T_ID_CL").Value)
            GENERAL_DataGridView.CurrentRow.Cells(2).Value = inp
            'Load_MAIN_BALANCES()
        End If
    End Sub

    Private Sub AGENTS_DataGridView_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGENTS_DataGridView.MouseDoubleClick
        Dim inp = InputBox(" : أدخل كود الحساب للحساب  " & vbNewLine & AGENTS_DataGridView.CurrentRow.Cells(1).Value)
        If inp <> "" Then
            query("UPDATE Agents SET TREE_CODE = '" & inp & "' WHERE AG_ID = " & AGENTS_DataGridView.CurrentRow.Cells(0).Value)
            AGENTS_DataGridView.CurrentRow.Cells(2).Value = inp
            'Load_AG_BALANCES()
        End If
    End Sub


    Private Sub TR_DataGridView_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TR_DataGridView.MouseDoubleClick
        Dim inp = InputBox(" : أدخل كود الحساب للحساب  " & vbNewLine & TR_DataGridView.CurrentRow.Cells(1).Value)
        If inp <> "" Then
            query("UPDATE TreasuryCard SET TREE_CODE = '" & inp & "' WHERE TR_ID = " & TR_DataGridView.CurrentRow.Cells(0).Value)
            'Load_TR_BALANCES()
            TR_DataGridView.CurrentRow.Cells(2).Value = inp
        End If
    End Sub

    Private Sub ST_DataGridView_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ST_DataGridView.MouseDoubleClick
        Dim inp = InputBox(" : أدخل كود الحساب للحساب  " & vbNewLine & ST_DataGridView.CurrentRow.Cells(1).Value)
        If inp <> "" Then
            query("UPDATE STORES SET TREE_CODE = '" & inp & "' WHERE ST_ID = " & ST_DataGridView.CurrentRow.Cells(0).Value)
            'Load_TR_BALANCES()
            ST_DataGridView.CurrentRow.Cells(2).Value = inp
        End If
    End Sub




    Private Sub Agent_Balance_For_Tree_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Rs.FindAllControls(Me)
        Load_MAIN_BALANCES()
        Load_AG_BALANCES()
        Load_TR_BALANCES()
        Load_ST_BALANCES()
    End Sub

    Private Sub Load_ST_BALANCES()
        Dim c As New C

        Try
            ST_Dt.Clear()
            Dim s As String
            s = "select ST_ID,ST_Name,TREE_CODE from STORES "
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(ST_Dt)
            ST_DataGridView.DataSource = ST_Dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Load_TR_BALANCES()
        Dim c As New C

        Try
            TR_Dt.Clear()
            Dim s As String
            s = "select Tr_ID,Tr_Name,TREE_CODE from TreasuryCard "
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(TR_Dt)
            TR_DataGridView.DataSource = TR_Dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Load_AG_BALANCES()
        Dim c As New C

        Try
            AG_Dt.Clear()
            Dim s As String
            s = "select AG_ID,AG_NAME,TREE_CODE from AgentS "
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(AG_Dt)
            AGENTS_DataGridView.DataSource = AG_Dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Load_MAIN_BALANCES()
        Dim c As New C

        Try
            IM_Dt.Clear()
            Dim s As String
            s = "select T_ID,B_NAME,TREE_CODE from Agent_Balance_Tree "
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Dt)
            GENERAL_DataGridView.DataSource = IM_Dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Agent_Balance_For_Tree_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Rs.ResizeAllControls(Me)
    End Sub

    Private Sub Agent_Balance_For_Tree_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub GENERAL_TXT_TextChanged(sender As Object, e As EventArgs) Handles GENERAL_TXT.TextChanged
        Dim Dv As DataView
        Dv = IM_Dt.AsDataView
        Dv.RowFilter = " B_NAME LIKE '%" + sender.Text + "%'"
        GENERAL_DataGridView.DataSource = Dv

    End Sub

    Private Sub AGENTS_TXT_TextChanged(sender As Object, e As EventArgs) Handles AGENTS_TXT.TextChanged
        Dim Dv As DataView
        Dv = AG_Dt.AsDataView
        Dv.RowFilter = " AG_NAME LIKE '%" + sender.Text + "%'"
        AGENTS_DataGridView.DataSource = Dv
    End Sub

    Private Sub TR_TXT_TextChanged(sender As Object, e As EventArgs) Handles TR_TXT.TextChanged
        Dim Dv As DataView
        Dv = TR_Dt.AsDataView
        Dv.RowFilter = " TR_NAME LIKE '%" + sender.Text + "%'"
        TR_DataGridView.DataSource = Dv
    End Sub

    Private Sub ST_TXT_TextChanged(sender As Object, e As EventArgs) Handles ST_TXT.TextChanged
        Dim Dv As DataView
        Dv = ST_Dt.AsDataView
        Dv.RowFilter = " ST_NAME LIKE '%" + sender.Text + "%'"
        ST_DataGridView.DataSource = Dv
    End Sub
End Class