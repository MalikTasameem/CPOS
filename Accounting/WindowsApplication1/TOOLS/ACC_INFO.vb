Public Class ACC_INFO

    Dim ACC_CODE_DT As New DataTable
    Public Type_Of_Data As Integer = 0 '0 = all / 1 = Agents / 2 = Treasury

    Private Sub ApplyCompactLayout()
        If TableLayoutPanel1 Is Nothing Then Exit Sub

        TableLayoutPanel1.SuspendLayout()
        Try
            TableLayoutPanel1.Padding = New Padding(1)
            TableLayoutPanel1.ColumnStyles.Clear()
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 105.0!))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 34.0!))
            TableLayoutPanel1.RowStyles.Clear()
            TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))

            ACC_CODE_TXT.Dock = DockStyle.Fill
            ACC_CODE_Cm.Dock = DockStyle.Fill
            SEARCH_ACC_BTN.Dock = DockStyle.None
            SEARCH_ACC_BTN.Anchor = AnchorStyles.None
            SEARCH_ACC_BTN.Size = New Size(29, 25)

            ACC_CODE_TXT.Margin = New Padding(2)
            ACC_CODE_Cm.Margin = New Padding(2)
            SEARCH_ACC_BTN.Margin = New Padding(2)

            ACC_CODE_TXT.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
            ACC_CODE_Cm.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
            SEARCH_ACC_BTN.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)

            ACC_CODE_TXT.TextAlign = HorizontalAlignment.Right
            ACC_CODE_Cm.BackColor = Color.White
            SEARCH_ACC_BTN.FlatStyle = FlatStyle.Flat
            SEARCH_ACC_BTN.FlatAppearance.BorderSize = 1
            Me.MinimumSize = New Size(260, 31)
        Finally
            TableLayoutPanel1.ResumeLayout()
        End Try
    End Sub

    Private Sub SEARCH_ACC_BTN_Click(sender As Object, e As EventArgs) Handles SEARCH_ACC_BTN.Click
        ACC_CODE_Search = ""
        BALANCE_SEARCH.ShowDialog()
        ACC_CODE_TXT.Text = ACC_CODE_Search
    End Sub

    Private Sub ACC_CODE_TXT_TextChanged(sender As Object, e As EventArgs) Handles ACC_CODE_TXT.TextChanged
        If ACC_CODE_TXT.Text.Count > 0 Then
            Filter_B()
        Else
            LOAD_ALL_BALANCES()
        End If
        ACC_CODE_NUM_ErrorProvider.Clear()
    End Sub

    Private Sub Filter_B()

        ACC_CODE_DT = GetAccountTreeDataTable(Accounts_Datatable, ACC_CODE_TXT.Text)

        Select Case Type_Of_Data
            Case 0 : ACC_CODE_DT = GetAccountTreeDataTable(Accounts_Datatable, ACC_CODE_TXT.Text)
            Case 1 : ACC_CODE_DT = GetAccountsTreeDataTable_IN(Agents_Datatable, agentRoots)
            Case 2 : ACC_CODE_DT = GetAccountsTreeDataTable_IN(Treasury_Datatable, treasuryRoots)
        End Select

        ACC_CODE_Cm.DataSource = ACC_CODE_DT
        ACC_CODE_Cm.DisplayMember = "ACC_NAME"
        ACC_CODE_Cm.ValueMember = "ACC_CODE"
        ACC_CODE_Cm.DroppedDown = True
        If ACC_CODE_DT.Rows.Count = 0 Then ACC_CODE_Cm.Text = ""

    End Sub

    Public Sub LOAD_ALL_BALANCES()
        ACC_CODE_DT.Clear()

        ACC_CODE_DT = Accounts_Datatable

        Select Case Type_Of_Data
            Case 0 : ACC_CODE_DT = Accounts_Datatable
            Case 1 : ACC_CODE_DT = Agents_Datatable
            Case 2 : ACC_CODE_DT = Treasury_Datatable
        End Select

    End Sub

    Private Sub ORG_B_Cm_KeyDown(sender As Object, e As KeyEventArgs) Handles ACC_CODE_Cm.KeyDown
        If e.KeyCode = Keys.Return Then GET_B_DATA(ACC_CODE_Cm, ACC_CODE_TXT)
    End Sub

    'Private Sub ORG_B_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ACC_CODE_Cm.SelectedValueChanged
    '    'GET_B_DATA(ACC_CODE_Cm, ACC_CODE_TXT)
    'End Sub

    Private Sub GET_B_DATA(ByRef CM As ComboBox, ByRef TXT As TextBox)
        If TypeName(CM.SelectedValue) = "String" Then
            TXT.Text = CM.SelectedValue
            CM.DroppedDown = False
        End If
    End Sub

    Private Sub ACC_INFO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyCompactLayout()
        LOAD_ALL_BALANCES()
        SendMessage(ACC_CODE_TXT.Handle, &H1501, 0, "رقم الحساب")
    End Sub

    Private Sub ACC_CODE_TXT_KeyDown(sender As Object, e As KeyEventArgs) Handles ACC_CODE_TXT.KeyDown
        If e.KeyCode = Keys.Return Then GET_B_DATA(ACC_CODE_Cm, ACC_CODE_TXT)

        If e.KeyCode = Keys.Down Then If ACC_CODE_DT.Rows.Count > 0 Then ACC_CODE_Cm.Select()

    End Sub

    Private Sub ACC_CODE_Cm_MouseClick(sender As Object, e As MouseEventArgs) Handles ACC_CODE_Cm.MouseClick
        GET_B_DATA(ACC_CODE_Cm, ACC_CODE_TXT)
    End Sub
End Class
