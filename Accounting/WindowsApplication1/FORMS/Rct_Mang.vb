Public Class Rct_Mang
    Dim DT_from As New DataTable
    Dim DT_to As New DataTable
    Dim ACC_CODE_DT As New DataTable

    Private Sub Rct_Mang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SELECT_Balance_From()
        SELECT_Balance_To()
        LOAD_ALL_BALANCES()
    End Sub


    Private Sub LOAD_ALL_BALANCES()

        ACC_CODE_DT.Clear()
        ACC_CODE_DT = Accounts_Datatable

    End Sub


    Public Sub SELECT_Balance_From()
        DT_from = New DataTable
        Dim C As New C

        Dim da As New SqlClient.SqlDataAdapter("SELECT [T_ID],[ACC_CODE],[ACC_NAME]  FROM [dbo].[Rct_Mang_V] WHERE  ACC_Type = 1 ", C.Con)
        da.Fill(DT_from)
        From_Grid.DataSource = DT_from
    End Sub


    Public Sub SELECT_Balance_To()
        DT_to = New DataTable
        Dim C As New C

        Dim da As New SqlClient.SqlDataAdapter("SELECT [T_ID],[ACC_CODE],[ACC_NAME]  FROM [dbo].[Rct_Mang_V] WHERE  ACC_Type = 2 ", C.Con)
        da.Fill(DT_to)
        To_Grid.DataSource = DT_to

    End Sub

    Private Sub ADD_Btn_Click(sender As Object, e As EventArgs) Handles ADD_Btn.Click

        Dim TYPE As Integer = 1
        If To_RadioBtn.Checked = True Then TYPE = 2
        query(" DECLARE @T_ID INT ; EXEC  @T_ID = AA_GET_MAX_ID 'Rct_Mang' ; INSERT INTO [dbo].[Rct_Mang]([T_ID],[ACC_CODE],[ACC_Type]) VALUES (@T_ID," & AcC_INFO1.ACC_CODE_TXT.Text & "," & TYPE & ")  ")
        SELECT_Balance_From()
        SELECT_Balance_To()
    End Sub

    Private Sub REMOVE_BTN_from_Click(sender As Object, e As EventArgs) Handles REMOVE_BTN_from.Click

        If From_Grid.Rows.Count > 0 Then
            query(" DELETE FROM Rct_Mang WHERE T_ID = " & From_Grid.CurrentRow.Cells(0).Value)
            SELECT_Balance_From()
            SELECT_Balance_To()
        End If

    End Sub

    Private Sub REMOVE_BTN_to_Click(sender As Object, e As EventArgs) Handles REMOVE_BTN_to.Click
        If To_Grid.Rows.Count > 0 Then
            query(" DELETE FROM Rct_Mang WHERE T_ID = " & To_Grid.CurrentRow.Cells(0).Value)
            SELECT_Balance_From()
            SELECT_Balance_To()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Rct_Mang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        LOAD_Accounts_Agents(1)
        LOAD_Accounts_Agents(2)
    End Sub
End Class