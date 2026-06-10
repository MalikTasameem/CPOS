Public Class Balances

    ' Dim rs As New Resizer
    Private AG_MV_DT As New DataTable
    Private TR_MV_DT As New DataTable
    Private _agMvPrintRowIndex As Integer = 0
    Private _trMvPrintRowIndex As Integer = 0
    Private _agBalancesPrintRowIndex As Integer = 0
    Private _salaryPrintRowIndex As Integer = 0
    Private _salaryPrintPageNumber As Integer = 1
    Private _salaryPrintTotalPages As Integer = 1
    Private _salaryPrintRowsPerPage As Integer = 0
    Private _salaryPrintTotalRows As Integer = 0
    Private _salarySummaryPrinted As Boolean = False
    Dim Balance_Type As Integer = 0

    Public AGMV_Name As String
    Public AGMV_Type As String
    Public AGMV_Date As String
    Public AGMV_User As String

    Dim TRMV_Name As String
    Dim TRMV_Type As String
    Dim TRMV_Date As String
    Dim TRMV_User As String

    Dim Trend_Date As String

    Dim IM_Dt As New DataTable
    Public AG_ID As Integer

    Dim Select_Agents_Debit As Boolean = False

    Dim colorToApply_DEBIT As Color
    Dim colorToApply_CREDIT As Color

    Private Sub MonthComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MonthComboBox.SelectedIndexChanged
        Try
            DateTimePicker_From = GetFirstDayOfMonth(DateTimePicker_From, MonthComboBox.Text)
            DateTimePicker_To = GetLastDayOfMonth(DateTimePicker_To, MonthComboBox.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Reports_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        Zuby.ADGV.AdvancedDataGridView.SetTranslations(Zuby.ADGV.AdvancedDataGridView.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))

        ConfigureResponsiveLayout()
        If U_Balance = False Then
            MsgBox("خارج صلاحياتك", MsgBoxStyle.Critical, "صلاحية المستخدم")
            Me.Close()
        End If


        colorToApply_DEBIT = ColorTranslator.FromHtml(MY_Settings.DEBIT_COLOR)
        colorToApply_CREDIT = ColorTranslator.FromHtml(MY_Settings.CREDIT_COLOR)

        Total_Debit.BackColor = colorToApply_DEBIT
        Total_Credit.BackColor = colorToApply_CREDIT

        T_DebitTextBox.BackColor = colorToApply_DEBIT
        T_CreditTextBox.BackColor = colorToApply_CREDIT

        Tr_Total_D.BackColor = colorToApply_DEBIT
        Tr_Total_C.BackColor = colorToApply_CREDIT

        ThemeManager.ApplyThemeToForm(Me)
    End Sub


    ' ==========================================
    ' 🌟 أكواد الشريط العلوي والتحكم بالنافذة 🌟
    ' ==========================================

    Private Sub HeaderCloseBtn_Click(sender As Object, e As EventArgs) Handles HeaderCloseBtn.Click
        Me.Close()
    End Sub

    Private Sub HeaderMaxBtn_Click(sender As Object, e As EventArgs) Handles HeaderMaxBtn.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.MaximumSize = Screen.FromHandle(Me.Handle).WorkingArea.Size
            Me.WindowState = FormWindowState.Maximized
            HeaderMaxBtn.Text = "❐"
        Else
            Me.WindowState = FormWindowState.Normal
            HeaderMaxBtn.Text = "⬜"
        End If
    End Sub

    Private Sub HeaderMinBtn_Click(sender As Object, e As EventArgs) Handles HeaderMinBtn.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, TopTitle_LB.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, TopTitle_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, TopTitle_LB.MouseUp
        drag = False
    End Sub


    Public Sub fetch_Agents_Types()

        AG_Type_CB.DataSource = Ge_Agents_Types()
        AG_Type_CB.DisplayMember = "name"
        AG_Type_CB.ValueMember = "ID"
        If AG_Type_CB.Visible = True Then If AG_Type_CB.Items.Count > 0 Then AG_Type_CB.SelectedIndex = 0

        Type_Cm.SelectedIndex = 0

    End Sub

    Function Ge_Agents_Types() As List(Of MailItem)
        Dim mailItems = New List(Of MailItem)
        mailItems.Add(New MailItem(0, "------- كل الأرصدة -------"))

        Dim c1 As New C
        Dim s As String = ""


        s = "Select id AS ID,Type_Name AS name from Agents_Types WHERE Visible = 1 Order By ID ASC"

        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems
    End Function


    Public Sub Load_Data()
        Dim sql As String =
            " select id,Type_Name from AgentBalance_Type WHERE ID NOT IN(14,15) AND Visible = 1 Order By RankNum ASC; " &
            " Select User_ID,UserName from Users Order By User_ID ASC; " &
            " Select Tr_ID,Tr_Name FROM TR_MENU_V; " &
            " Select id,Type_Name from TreasuryBalance_Type; " &
            " select Distinct top 10 Year from Agents_Balance_MV WHERE YEAR IS NOT NULL ORDER BY YEAR DESC "

        Try
            Using sqlCon As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
                sqlCon.Open()

                Dim loadDataSet As New DataSet()
                Using da As New SqlClient.SqlDataAdapter(sql, sqlCon)
                    da.Fill(loadDataSet)
                End Using

                If loadDataSet.Tables.Count > 0 Then
                    Dim receiptTypeDt As DataTable = loadDataSet.Tables(0)
                    ReceiptTypeComboBox.DataSource = receiptTypeDt
                    ReceiptTypeComboBox.DisplayMember = "Type_Name"
                    ReceiptTypeComboBox.ValueMember = "id"
                    If receiptTypeDt.Rows.Count > 0 Then ReceiptTypeComboBox.SelectedIndex = 0
                End If

                If loadDataSet.Tables.Count > 1 Then
                    Dim usersDt As DataTable = loadDataSet.Tables(1)
                    UsersComboBox.DataSource = usersDt
                    UsersComboBox.DisplayMember = "UserName"
                    UsersComboBox.ValueMember = "User_ID"
                    If usersDt.Rows.Count > 0 Then UsersComboBox.SelectedIndex = 0

                    TrUsersComboBox.DataSource = usersDt.Copy()
                    TrUsersComboBox.DisplayMember = "UserName"
                    TrUsersComboBox.ValueMember = "User_ID"
                    If usersDt.Rows.Count > 0 Then TrUsersComboBox.SelectedIndex = 0
                End If

                If loadDataSet.Tables.Count > 2 Then
                    Dim treasuryDt As DataTable = loadDataSet.Tables(2)
                    Treasury_ComboBox.DataSource = treasuryDt
                    Treasury_ComboBox.DisplayMember = "Tr_Name"
                    Treasury_ComboBox.ValueMember = "Tr_ID"
                    If treasuryDt.Rows.Count > 0 Then Treasury_ComboBox.SelectedIndex = 0
                End If

                If loadDataSet.Tables.Count > 3 Then
                    Dim trTypeDt As DataTable = loadDataSet.Tables(3)
                    TrTypeComboBox.DataSource = trTypeDt
                    TrTypeComboBox.DisplayMember = "Type_Name"
                    TrTypeComboBox.ValueMember = "id"
                    If trTypeDt.Rows.Count > 0 Then TrTypeComboBox.SelectedIndex = 0
                End If

                If loadDataSet.Tables.Count > 4 Then
                    Dim yearsDt As DataTable = loadDataSet.Tables(4)
                    YearComboBox.DataSource = yearsDt
                    YearComboBox.DisplayMember = "Year"
                    If yearsDt.Rows.Count > 0 Then YearComboBox.SelectedIndex = 0
                End If

                SalariesMonthComboBox.Text = Date.Now.Month

                Get_AgentsBalances(sqlCon)
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Dim AG_B_DT As New DataTable
    Private Sub Get_AgentsBalances(Optional sqlCon As SqlClient.SqlConnection = Nothing)

        Rpt_Name = ""

        Dim mainQuery As String = BuildAgentsBalancesQuery(sqlCon)

        AG_B_DT.Clear()

        If sqlCon Is Nothing Then
            Dim C = New C
            C.Da = New SqlClient.SqlDataAdapter(mainQuery, C.Con)
            C.Da.Fill(AG_B_DT)
        Else
            Using da As New SqlClient.SqlDataAdapter(mainQuery, sqlCon)
                da.Fill(AG_B_DT)
            End Using
        End If

        DebtMetroGrid.DataSource = AG_B_DT

        Calc_T_Balances()
        Coloring()

    End Sub

    Private Function BuildAgentsBalancesQuery(Optional sqlCon As SqlClient.SqlConnection = Nothing) As String

        If Type_Cm.SelectedIndex <> 0 Then
            Return " SELECT [Tr_ID] AS ID,[Tr_Name] AS 'Agent_name',[T_Debit],[T_Credit],ISNULL(T_Balance,0) AS T_Balance FROM [dbo].[TR_MENU_V]"
        End If

        If Debit_WithDate_CB.Checked = True Then Select_Debt_Till_Date(sqlCon)

        Dim baseQuery As String

        If Debit_WithDate_CB.Checked = True Then
            baseQuery = " SELECT [Ag_ID] AS ID,[Ag_name] AS Agent_name,'0' AS [T_Debit],'0' AS [T_Credit],[T_Balance_TMP] AS T_Balance FROM AGENTS_MENU_V "
        Else
            baseQuery = " SELECT [Ag_ID] AS ID,[Ag_name] AS Agent_name,[T_Debit],[T_Credit],[T_Balance] FROM AGENTS_MENU_V "
        End If

        Dim whereParts As New List(Of String)

        whereParts.Add("Type_ID IN (-1" & BuildAgentTypesFilter() & ")")

        If AlldebitsRadioButton.Checked = True Then
            whereParts.Add("T_Balance < 0")
        ElseIf AllCerditRadioButton.Checked = True Then
            whereParts.Add("T_Balance > 0")
        End If

        Return baseQuery & " WHERE " & String.Join(" AND ", whereParts) & " ORDER BY Ag_name ASC "

    End Function

    Private Function BuildAgentTypesFilter() As String

        Dim filter As String = ""

        For i As Integer = 0 To AG_Type_CB.Items.Count - 1
            If AG_Type_CB.GetItemCheckState(i) <> CheckState.Checked Then Continue For

            If i = 0 Then
                Rpt_Name = "---الكل---"
                Return Select_ALL(AG_Type_CB)
            End If

            filter &= "," & GetCheckedListItemValue(AG_Type_CB, i)
            Rpt_Name &= "," & GetCheckedListItemText(AG_Type_CB, i)
        Next

        Return filter

    End Function

    Private Function GetCheckedListItemValue(clb As CheckedListBox, index As Integer) As String

        Dim rowView = TryCast(clb.Items(index), DataRowView)

        If rowView IsNot Nothing Then Return rowView(clb.ValueMember).ToString()

        clb.SelectedIndex = index
        Return clb.SelectedValue.ToString()

    End Function

    Private Function GetCheckedListItemText(clb As CheckedListBox, index As Integer) As String

        Dim rowView = TryCast(clb.Items(index), DataRowView)

        If rowView IsNot Nothing Then Return rowView(clb.DisplayMember).ToString()

        Return clb.GetItemText(clb.Items(index))

    End Function

    Dim Rpt_Name As String = ""
    Private Function Select_ALL(ByVal CLB As CheckedListBox)
        Dim Str As String = ""
        Rpt_Name = ""
        For i As Integer = 0 To CLB.Items.Count - 1
            CLB.SelectedIndex = i
            Str += "," & CLB.SelectedValue
            'Rpt_Name += "," & CLB.Text
        Next
        Return Str
    End Function

    Private Sub Reports_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ArrangeResponsiveLayout()
    End Sub

    Private Sub ConfigureResponsiveLayout()

        Me.MinimumSize = New Size(1004, 739)
        TitleBar_Panel.Dock = DockStyle.Top
        MenuStrip1.Dock = DockStyle.Top
        'MetroTabControl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        SetAnchor(AGMVMetroGrid, AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right)
        SetAnchor(Tr_MV_MetroGrid, AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right)
        SetAnchor(DebtMetroGrid, AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right)
        SetAnchor(SalariesMetroGrid, AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right)

        SetAnchor(AG_Type_CB, AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right)
        SetAnchor(CMSearchTextBox, AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right)

        SetAnchor(Type_Cm, AnchorStyles.Top Or AnchorStyles.Right)
        SetAnchor(Label36, AnchorStyles.Top Or AnchorStyles.Right)
        ' SetAnchor(AG_Cm, AnchorStyles.Top Or AnchorStyles.Right)
        SetAnchor(Treasury_ComboBox, AnchorStyles.Top Or AnchorStyles.Right)
        SetAnchor(ReceiptTypeComboBox, AnchorStyles.Top Or AnchorStyles.Right)
        SetAnchor(UsersComboBox, AnchorStyles.Top Or AnchorStyles.Right)

        SetBottomAnchors(
            Total_Balance, Total_Credit, Total_Debit,
            NUM_CREDIT_TXT, NUM_DEBIT_TXT,
            Label33, Label34, Label35, Label37, Label38
        )

        SetBottomAnchors(
            Tr_Total_B, Tr_Total_C, Tr_Total_D,
            Label30, Label31, Label32
        )

        SetBottomAnchors(
            T_BalanceTextBox, T_CreditTextBox, T_DebitTextBox,
            Label17, Label18, Label19
        )

        SetBottomAnchors(
            TotalPureTextBox, TotalDebtTextBox,
            Label15, Label16
        )

        SetAnchor(MVSearchButton, AnchorStyles.Top Or AnchorStyles.Left)
        SetAnchor(MVPrintButton, AnchorStyles.Top Or AnchorStyles.Left)
        SetAnchor(Send_To_Email_btn, AnchorStyles.Top Or AnchorStyles.Left)
        SetAnchor(Tr_SearchButton, AnchorStyles.Top Or AnchorStyles.Left)
        SetAnchor(Tr_PrintButton, AnchorStyles.Top Or AnchorStyles.Left)
        SetAnchor(AGBalancesPrintBtn, AnchorStyles.Top Or AnchorStyles.Left)
        SetAnchor(AG_BALANCE_Sch_Btn, AnchorStyles.Top Or AnchorStyles.Left)
        SetAnchor(SalarySearchButton, AnchorStyles.Top Or AnchorStyles.Right)
        SetAnchor(PrintButton, AnchorStyles.Top Or AnchorStyles.Right)
        SetAnchor(Insert_Salary_btn, AnchorStyles.Top Or AnchorStyles.Left)
        SetAnchor(Salary_Date, AnchorStyles.Top Or AnchorStyles.Left)
        SetAnchor(Label39, AnchorStyles.Top Or AnchorStyles.Left)
        SetAnchor(YearComboBox, AnchorStyles.Top Or AnchorStyles.Right)
        SetAnchor(SalariesMonthComboBox, AnchorStyles.Top Or AnchorStyles.Right)
        SetAnchor(Label13, AnchorStyles.Top Or AnchorStyles.Right)
        SetAnchor(Label14, AnchorStyles.Top Or AnchorStyles.Right)

        SetAnchor(GroupBox1, AnchorStyles.Top)
        SetAnchor(GroupBox2, AnchorStyles.Top)


        SetGridResponsiveOptions(
            AGMVMetroGrid,
            Tr_MV_MetroGrid,
            DebtMetroGrid,
            SalariesMetroGrid
        )

        ConfigureMovementSearchDesign()
        ArrangeResponsiveLayout()

    End Sub

    Private Sub SetAnchor(control As Control, anchor As AnchorStyles)

        If control Is Nothing Then Return
        control.Anchor = anchor

    End Sub

    Private Sub SetBottomAnchors(ParamArray controls() As Control)

        For Each control As Control In controls
            SetAnchor(control, AnchorStyles.Bottom Or AnchorStyles.Left)
        Next

    End Sub

    Private Sub SetGridResponsiveOptions(ParamArray grids() As DataGridView)

        For Each grid As DataGridView In grids
            If grid Is Nothing Then Continue For
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
            grid.BackgroundColor = Color.White
            grid.BorderStyle = BorderStyle.FixedSingle
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
            grid.DefaultCellStyle.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
            grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid.GridColor = Color.Gainsboro
            grid.RowTemplate.Height = 28
            grid.ColumnHeadersHeight = 30
        Next

    End Sub

    Private Sub ConfigureMovementSearchDesign()

        StyleSearchGroupBox(GroupBox1)
        StyleSearchGroupBox(GroupBox2)

        '  StyleSearchInput(AG_Cm)
        StyleSearchInput(ReceiptTypeComboBox)
        StyleSearchInput(UsersComboBox)
        StyleSearchInput(Treasury_ComboBox)
        StyleSearchInput(TrTypeComboBox)
        StyleSearchInput(TrUsersComboBox)
        StyleSearchInput(DateTimePicker_From)
        StyleSearchInput(DateTimePicker_To)
        StyleSearchInput(MonthComboBox)
        StyleSearchInput(TrDateTimePicker_F)
        StyleSearchInput(TrDateTimePicker_T)
        StyleSearchInput(TrMonthComboBox)

        StyleSearchLabel(Label3)
        StyleSearchLabel(Label8)
        StyleSearchLabel(Label7)
        StyleSearchLabel(Label9)
        StyleSearchLabel(Label4)
        StyleSearchLabel(Label2)
        StyleSearchLabel(Label1)
        StyleSearchLabel(Label5)
        StyleSearchLabel(Label6)
        StyleSearchLabel(Label10)
        StyleSearchLabel(Label11)
        StyleSearchLabel(Label12)

        StyleSearchCheckBox(AllAgentsCheckBox)
        StyleSearchCheckBox(AllRecieptsCheckBox)
        StyleSearchCheckBox(AllUsersCheckBox)
        StyleSearchCheckBox(isVoid_CB)
        StyleSearchCheckBox(AllTimeCheckBox)
        StyleSearchCheckBox(AllTrCheckBox)
        StyleSearchCheckBox(TrAllTypeCheckBox)
        StyleSearchCheckBox(TrAllUsersCheckBox)
        StyleSearchCheckBox(TrisVoid_CB)
        StyleSearchCheckBox(TrAllTimeCheckBox)

        StyleActionButton(MVSearchButton)
        StyleActionButton(MVPrintButton)
        StyleActionButton(Send_To_Email_btn)
        StyleActionButton(Tr_SearchButton)
        StyleActionButton(Tr_PrintButton)

    End Sub

    Private Sub StyleSearchGroupBox(groupBox As GroupBox)

        If groupBox Is Nothing Then Return
        groupBox.BackColor = Color.FromArgb(248, 250, 252)
        groupBox.Font = New Font("Segoe UI", 9.0!, FontStyle.Regular)

    End Sub

    Private Sub StyleSearchInput(control As Control)

        If control Is Nothing Then Return
        control.Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold)
        control.BackColor = Color.White
        control.ForeColor = Color.FromArgb(25, 35, 45)

        Dim comboBox As ComboBox = TryCast(control, ComboBox)
        If comboBox IsNot Nothing Then comboBox.FlatStyle = FlatStyle.Flat

    End Sub

    Private Sub StyleSearchLabel(label As Label)

        If label Is Nothing Then Return
        label.AutoSize = False
        label.Font = New Font("Segoe UI", 9.0!, FontStyle.Regular)
        label.TextAlign = ContentAlignment.MiddleRight

    End Sub

    Private Sub StyleSearchCheckBox(checkBox As CheckBox)

        If checkBox Is Nothing Then Return
        checkBox.AutoSize = False
        checkBox.Font = New Font("Segoe UI Semibold", 8.75!, FontStyle.Bold)
        checkBox.TextAlign = ContentAlignment.MiddleRight
        checkBox.BackColor = Color.Transparent

    End Sub

    Private Sub StyleActionButton(button As Button)

        If button Is Nothing Then Return
        button.Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold)
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderColor = Color.FromArgb(174, 184, 196)
        button.BackColor = Color.WhiteSmoke

    End Sub

    Private Sub ArrangeMovementRangeGroupBox(groupBox As GroupBox,
                                            allTimeCheckBox As CheckBox,
                                            fromDate As DateTimePicker,
                                            toDate As DateTimePicker,
                                            monthCombo As ComboBox,
                                            fromLabel As Label,
                                            toLabel As Label,
                                            monthLabel As Label)

        If groupBox Is Nothing Then Return

        Dim gap As Integer = 6
        Dim margin As Integer = 8
        Dim rowTop1 As Integer = 24
        Dim rowTop2 As Integer = 62
        Dim rowHeight As Integer = 28
        Dim monthLabelWidth As Integer = 48
        Dim monthComboWidth As Integer = 76
        Dim centerLabelWidth As Integer = 32

        Dim monthLabelX As Integer = groupBox.Width - margin - monthLabelWidth
        Dim monthComboX As Integer = monthLabelX - gap - monthComboWidth
        Dim centerLabelX As Integer = monthComboX - gap - centerLabelWidth
        Dim dateWidth As Integer = Math.Max(110, centerLabelX - margin - gap)

        If allTimeCheckBox IsNot Nothing Then allTimeCheckBox.SetBounds(monthComboX, rowTop1, monthComboWidth + gap + monthLabelWidth, rowHeight)
        If fromLabel IsNot Nothing Then fromLabel.SetBounds(centerLabelX, rowTop1, centerLabelWidth, rowHeight)
        If toLabel IsNot Nothing Then toLabel.SetBounds(centerLabelX, rowTop2, centerLabelWidth, rowHeight)
        If monthLabel IsNot Nothing Then monthLabel.SetBounds(monthLabelX, rowTop2, monthLabelWidth, rowHeight)
        If monthCombo IsNot Nothing Then monthCombo.SetBounds(monthComboX, rowTop2, monthComboWidth, rowHeight)
        If fromDate IsNot Nothing Then fromDate.SetBounds(margin, rowTop1, dateWidth, rowHeight)
        If toDate IsNot Nothing Then toDate.SetBounds(margin, rowTop2, dateWidth, rowHeight)

    End Sub

    Private Sub ArrangeResponsiveLayout()

        If TitleBar_Panel Is Nothing OrElse MenuStrip1 Is Nothing Then 'MetroTabControl1 Is Nothing OrElse
            Return
        End If

        Dim tabTop As Integer = TitleBar_Panel.Height + MenuStrip1.Height + 7
        'MetroTabControl1.SetBounds(0, tabTop, Me.ClientSize.Width, Math.Max(200, Me.ClientSize.Height - tabTop))

        ArrangeAccountMovementPage()
        ArrangeTreasuryMovementPage()
        ArrangeSalariesPage()
        'ArrangePageFiveGroups()

    End Sub

    Private Sub ArrangeAccountMovementPage()

        If MetroTabPage1 Is Nothing Then Return

        Dim pageWidth As Integer = MetroTabPage1.ClientSize.Width
        Dim pageHeight As Integer = MetroTabPage1.ClientSize.Height
        If pageWidth <= 0 OrElse pageHeight <= 0 Then Return

        Dim margin As Integer = 8
        Dim gap As Integer = 8
        Dim rowHeight As Integer = 30
        Dim labelWidth As Integer = 84
        Dim checkWidth As Integer = 62
        Dim buttonWidth As Integer = 132
        Dim groupWidth As Integer = Math.Min(390, Math.Max(310, CInt(pageWidth * 0.22)))
        Dim topAreaHeight As Integer = 136
        Dim footerHeight As Integer = 38

        MVSearchButton.SetBounds(margin, 5, buttonWidth, 34)
        MVPrintButton.SetBounds(margin, 43, buttonWidth, 34)
        Send_To_Email_btn.SetBounds(margin, 81, buttonWidth, 34)

        GroupBox1.SetBounds(margin + buttonWidth + gap, 3, groupWidth, 104)
        ArrangeMovementRangeGroupBox(GroupBox1, AllTimeCheckBox, DateTimePicker_From, DateTimePicker_To, MonthComboBox, Label6, Label5, Label1)

        Dim labelX As Integer = pageWidth - margin - labelWidth
        Dim comboRight As Integer = labelX - gap
        Dim checkX As Integer = GroupBox1.Right + gap
        Dim comboX As Integer = checkX + checkWidth + gap
        Dim comboWidth As Integer = Math.Max(180, comboRight - comboX)

        Label3.SetBounds(labelX, 8, labelWidth, rowHeight)
        AG_Cm.SetBounds(comboX, 5, comboWidth, 34)
        AllAgentsCheckBox.SetBounds(checkX, 5, checkWidth, rowHeight)

        Label8.SetBounds(labelX, 43, labelWidth, rowHeight)
        ReceiptTypeComboBox.SetBounds(comboX, 42, comboWidth, rowHeight)
        AllRecieptsCheckBox.SetBounds(checkX, 42, checkWidth, rowHeight)

        Label7.SetBounds(labelX, 78, labelWidth, rowHeight)
        UsersComboBox.SetBounds(comboX, 77, comboWidth, rowHeight)
        AllUsersCheckBox.SetBounds(checkX, 77, checkWidth, rowHeight)
        isVoid_CB.SetBounds(checkX, 109, Math.Min(190, comboWidth + checkWidth + gap), 26)

        AGMVMetroGrid.SetBounds(
            3,
            topAreaHeight,
            Math.Max(200, pageWidth - 6),
            Math.Max(120, pageHeight - topAreaHeight - footerHeight)
        )

    End Sub

    Private Sub ArrangeTreasuryMovementPage()

        If MetroTabPage2 Is Nothing Then Return

        Dim pageWidth As Integer = MetroTabPage2.ClientSize.Width
        Dim pageHeight As Integer = MetroTabPage2.ClientSize.Height
        If pageWidth <= 0 OrElse pageHeight <= 0 Then Return

        Dim margin As Integer = 8
        Dim gap As Integer = 8
        Dim rowHeight As Integer = 30
        Dim labelWidth As Integer = 88
        Dim checkWidth As Integer = 62
        Dim buttonWidth As Integer = 132
        Dim groupWidth As Integer = Math.Min(390, Math.Max(320, CInt(pageWidth * 0.22)))
        Dim topAreaHeight As Integer = 136
        Dim footerHeight As Integer = 38

        Tr_SearchButton.SetBounds(margin, 5, buttonWidth, 52)
        Tr_PrintButton.SetBounds(margin, 61, buttonWidth, 52)

        GroupBox2.SetBounds(margin + buttonWidth + gap, 3, groupWidth, 104)
        ArrangeMovementRangeGroupBox(GroupBox2, TrAllTimeCheckBox, TrDateTimePicker_F, TrDateTimePicker_T, TrMonthComboBox, Label12, Label11, Label10)

        Dim labelX As Integer = pageWidth - margin - labelWidth
        Dim comboRight As Integer = labelX - gap
        Dim checkX As Integer = GroupBox2.Right + gap
        Dim comboX As Integer = checkX + checkWidth + gap
        Dim comboWidth As Integer = Math.Max(180, comboRight - comboX)

        Label9.SetBounds(labelX, 8, labelWidth, rowHeight)
        Treasury_ComboBox.SetBounds(comboX, 5, comboWidth, rowHeight)
        AllTrCheckBox.SetBounds(checkX, 5, checkWidth, rowHeight)

        Label4.SetBounds(labelX, 43, labelWidth, rowHeight)
        TrTypeComboBox.SetBounds(comboX, 42, comboWidth, rowHeight)
        TrAllTypeCheckBox.SetBounds(checkX, 42, checkWidth, rowHeight)

        Label2.SetBounds(labelX, 78, labelWidth, rowHeight)
        TrUsersComboBox.SetBounds(comboX, 77, comboWidth, rowHeight)
        TrAllUsersCheckBox.SetBounds(checkX, 77, checkWidth, rowHeight)
        TrisVoid_CB.SetBounds(checkX, 109, Math.Min(190, comboWidth + checkWidth + gap), 26)

        Tr_MV_MetroGrid.SetBounds(
            3,
            topAreaHeight,
            Math.Max(200, pageWidth - 6),
            Math.Max(120, pageHeight - topAreaHeight - footerHeight)
        )

    End Sub

    Private Sub ArrangeSalariesPage()

        If MetroTabPage3 Is Nothing Then Return

        Dim pageWidth As Integer = MetroTabPage3.ClientSize.Width
        Dim pageHeight As Integer = MetroTabPage3.ClientSize.Height
        If pageWidth <= 0 OrElse pageHeight <= 0 Then Return

        Dim margin As Integer = 8
        Dim gap As Integer = 8
        Dim topAreaHeight As Integer = 64
        Dim footerHeight As Integer = 42
        Dim buttonWidth As Integer = 105
        Dim insertButtonWidth As Integer = 143
        Dim labelWidth As Integer = 48

        Label13.SetBounds(pageWidth - margin - labelWidth, 23, labelWidth, 24)
        YearComboBox.SetBounds(Label13.Left - gap - 100, 15, 100, 34)

        Label14.SetBounds(YearComboBox.Left - gap - labelWidth, 23, labelWidth, 24)
        SalariesMonthComboBox.SetBounds(Label14.Left - gap - 78, 15, 78, 34)

        SalarySearchButton.SetBounds(SalariesMonthComboBox.Left - gap - buttonWidth, 8, buttonWidth, 46)
        PrintButton.SetBounds(SalarySearchButton.Left - gap - buttonWidth, 8, buttonWidth, 46)

        Insert_Salary_btn.SetBounds(margin, 8, insertButtonWidth, 46)
        Salary_Date.SetBounds(Insert_Salary_btn.Right + gap, 21, 231, 28)
        Label39.SetBounds(Salary_Date.Right + gap, 25, 96, 24)

        If Label39.Right + gap > PrintButton.Left Then
            Salary_Date.Width = Math.Max(160, PrintButton.Left - gap - Label39.Width - gap - Salary_Date.Left)
            Label39.Left = Salary_Date.Right + gap
        End If

        SalariesMetroGrid.SetBounds(
            3,
            topAreaHeight,
            Math.Max(200, pageWidth - 6),
            Math.Max(120, pageHeight - topAreaHeight - footerHeight)
        )

        Dim footerTop As Integer = Math.Max(SalariesMetroGrid.Bottom + 8, pageHeight - 36)
        Dim totalWidth As Integer = 188
        Dim firstGroupLeft As Integer = margin
        Dim secondGroupLeft As Integer = firstGroupLeft + totalWidth + 110

        TotalPureTextBox.SetBounds(firstGroupLeft, footerTop, totalWidth, 28)
        Label15.SetBounds(TotalPureTextBox.Right + gap, footerTop + 4, 120, 24)

        TotalDebtTextBox.SetBounds(secondGroupLeft, footerTop, totalWidth, 28)
        Label16.SetBounds(TotalDebtTextBox.Right + gap, footerTop + 4, 170, 24)

    End Sub

    'Private Sub ArrangePageFiveGroups()

    '    If MetroTabPage5 Is Nothing Then Return

    '    Dim pageWidth As Integer = MetroTabPage5.ClientSize.Width
    '    If pageWidth <= 0 Then Return

    '    GroupBox3.Left = Math.Max(8, (pageWidth - GroupBox3.Width) \ 2)

    '    Dim gap As Integer = 32
    '    Dim combinedWidth As Integer = GroupBox5.Width + gap + GroupBox4.Width
    '    Dim leftStart As Integer = Math.Max(8, (pageWidth - combinedWidth) \ 2)

    '    GroupBox5.Left = leftStart
    '    GroupBox4.Left = leftStart + GroupBox5.Width + gap

    'End Sub

    Private Sub AllAgentsCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AllAgentsCheckBox.CheckedChanged
        If AllAgentsCheckBox.Checked = True Then
            AG_Cm.Enabled = False
            AG_Cm.Textt = ""
            AG_ID = 0
        Else
            AG_Cm.Enabled = True
        End If
    End Sub

    Private Sub AllRecieptsCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AllRecieptsCheckBox.CheckedChanged

        If AllRecieptsCheckBox.Checked = True Then
            ReceiptTypeComboBox.Enabled = False
            ReceiptTypeComboBox.SelectedIndex = -1
        Else
            ReceiptTypeComboBox.Enabled = True
            ReceiptTypeComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub AllUsersCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AllUsersCheckBox.CheckedChanged
        If AllUsersCheckBox.Checked = True Then
            UsersComboBox.Enabled = False
            UsersComboBox.SelectedIndex = -1
        Else
            UsersComboBox.Enabled = True
            UsersComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub AllTimeCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AllTimeCheckBox.CheckedChanged
        If AllTimeCheckBox.Checked = True Then
            MonthComboBox.SelectedIndex = -1
            MonthComboBox.Enabled = False
        Else
            MonthComboBox.Enabled = True
        End If
    End Sub

    Private Sub MVSearchButton_Click(sender As Object, e As EventArgs) Handles MVSearchButton.Click
        AG_MV_Prepare_To_Search()
    End Sub


    Public Sub AG_MV_Prepare_To_Search()
        ' SetAgMvSearchBusy(True)

        Try
            PrepareAgMvReportTitles()

            If Search_AG_MV() Then
                Apply_AG_MV_View()
            End If

        Finally
            '   SetAgMvSearchBusy(False)
        End Try
    End Sub

    Private Sub PrepareAgMvReportTitles()

        If AllAgentsCheckBox.Checked = True Then
            AGMV_Name = "الكـــل"
        Else
            AGMV_Name = AG_Cm.Textt
        End If

        If AllTimeCheckBox.Checked = True Then
            AGMV_Date = "كـل الفتـــرات"
        Else
            AGMV_Date = " مــن " + DateTimePicker_From.Text + " إلــى " + DateTimePicker_To.Text
        End If

        If AllRecieptsCheckBox.Checked = True Then
            AGMV_Type = "الكـــل"
        Else
            AGMV_Type = ReceiptTypeComboBox.Text
        End If

        If AllUsersCheckBox.Checked = True Then
            AGMV_User = "الكـــل"
        Else
            AGMV_User = UsersComboBox.Text
        End If

    End Sub

    Private Sub SetAgMvSearchBusy(isBusy As Boolean)

        'Me.Cursor = If(isBusy, Cursors.AppStarting, Cursors.Default)
        'MVSearchButton.Enabled = Not isBusy
        'MVPrintButton.Enabled = Not isBusy
        'MVSearchButton.Text = If(isBusy, "جاري البحث...", "بحــــــث")
        'MVSearchButton.BackColor = If(isBusy, Color.FromArgb(255, 243, 205), Color.WhiteSmoke)
        'AGMVMetroGrid.Cursor = If(isBusy, Cursors.AppStarting, Cursors.Hand)

    End Sub

    Public Function Search_AG_MV() As Boolean
        Dim C = New C
        'If AllAgentsCheckBox.Checked = False Then
        '    If AG_ID = 0 And String.IsNullOrWhiteSpace(IM_SH_txt.Text) Then
        '        IM_SH_txt.Text = "نقدي"
        '        Fetch_ItemToList()
        '    ElseIf AG_ID = 0 And String.IsNullOrWhiteSpace(IM_SH_txt.Text) = False Then
        '        MsgBox("حدد العميل", MsgBoxStyle.Exclamation, "")
        '        Exit Sub
        '    End If
        'End If

        If AllAgentsCheckBox.Checked = False Then
            AG_ID = AG_Cm.TXT_ID.Text
            If AG_Cm.TXT_ID.Text = "0" Then
                MsgBox("حدد العميل", MsgBoxStyle.Exclamation, "")
                Return False
            End If
        End If

        Try
            With C.Com
                .Connection = C.Con
                .CommandText = "[Search_AG_MV]"

                .Parameters.AddWithValue("@AllAgentsCheckBox", AllAgentsCheckBox.Checked)
                .Parameters.AddWithValue("@AllUsersCheckBox", AllUsersCheckBox.Checked)
                .Parameters.AddWithValue("@AllTimeCheckBox", AllTimeCheckBox.Checked)
                .Parameters.AddWithValue("@AllRecieptsCheckBox", AllRecieptsCheckBox.Checked)

                .Parameters.AddWithValue("@DateTimePicker_From", DateTimePicker_From.Value)
                .Parameters.AddWithValue("@DateTimePicker_To", DateTimePicker_To.Value)

                .Parameters.AddWithValue("@AG_ID", AG_ID)
                .Parameters.AddWithValue("@ReceiptTypeComboBox", ReceiptTypeComboBox.SelectedValue)
                .Parameters.AddWithValue("@UsersComboBox", UsersComboBox.SelectedValue)
                .Parameters.AddWithValue("@Currency", Currency_CB.Checked)

                .CommandType = CommandType.StoredProcedure
            End With
            C.Da = New SqlClient.SqlDataAdapter(C.Com)
            AG_MV_DT = New DataTable()
            C.Da.Fill(AG_MV_DT)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Private Sub AllTrCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AllTrCheckBox.CheckedChanged
        If AllTrCheckBox.Checked = True Then
            Treasury_ComboBox.Enabled = False
            Treasury_ComboBox.SelectedIndex = -1
        Else
            Treasury_ComboBox.Enabled = True
            Treasury_ComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub TrAllTypeCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles TrAllTypeCheckBox.CheckedChanged
        If TrAllTypeCheckBox.Checked = True Then
            TrTypeComboBox.Enabled = False
            TrTypeComboBox.SelectedIndex = -1
        Else
            TrTypeComboBox.Enabled = True
            TrTypeComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub TrAllUsersCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles TrAllUsersCheckBox.CheckedChanged
        If TrAllUsersCheckBox.Checked = True Then
            TrUsersComboBox.Enabled = False
            TrUsersComboBox.SelectedIndex = -1
        Else
            TrUsersComboBox.Enabled = True
            TrUsersComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub TrAllTimeCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles TrAllTimeCheckBox.CheckedChanged
        If TrAllTimeCheckBox.Checked = True Then
            TrMonthComboBox.SelectedIndex = -1
            TrMonthComboBox.Enabled = False
        Else
            TrMonthComboBox.Enabled = True
        End If
    End Sub

    Private Sub Tr_SearchButton_Click(sender As Object, e As EventArgs) Handles Tr_SearchButton.Click
        Tr_MV_Prepare_To_Search()
    End Sub

    Private Sub Tr_MV_Prepare_To_Search()
        SetTrMvSearchBusy(True)

        Try
            PrepareTrMvReportTitles()

            If Search_Tr_MV() Then
                Apply_TR_MV_View()
            End If

        Finally
            SetTrMvSearchBusy(False)
        End Try
    End Sub

    Private Sub PrepareTrMvReportTitles()
        If AllTrCheckBox.Checked = True Then
            TRMV_Name = "الكـــل"
        Else
            TRMV_Name = Treasury_ComboBox.Text
        End If

        If TrAllTimeCheckBox.Checked = True Then
            TRMV_Date = "كـل الفتـــرات"
        Else
            TRMV_Date = " مــن " + TrDateTimePicker_F.Text + " إلــى " + TrDateTimePicker_T.Text
        End If

        If TrAllTypeCheckBox.Checked = True Then
            TRMV_Type = "الكـــل"
        Else
            TRMV_Type = TrTypeComboBox.Text
        End If

        If TrAllUsersCheckBox.Checked = True Then
            TRMV_User = "الكـــل"
        Else
            TRMV_User = TrUsersComboBox.Text
        End If

    End Sub

    Private Sub SetTrMvSearchBusy(isBusy As Boolean)

        Me.Cursor = If(isBusy, Cursors.AppStarting, Cursors.Default)
        Tr_SearchButton.Enabled = Not isBusy
        Tr_PrintButton.Enabled = Not isBusy
        Tr_SearchButton.Text = If(isBusy, "جاري البحث...", "بحــــــث")
        Tr_SearchButton.BackColor = If(isBusy, Color.FromArgb(255, 243, 205), Color.WhiteSmoke)
        Tr_MV_MetroGrid.Cursor = If(isBusy, Cursors.AppStarting, Cursors.Hand)

    End Sub

    Public Function Search_Tr_MV() As Boolean
        Dim C = New C
        If AllTrCheckBox.Checked = False Then
            If Treasury_ComboBox.SelectedIndex = -1 Then
                MsgBox("حدد الخزنة", MsgBoxStyle.Exclamation, "")
                Return False
            End If
        End If
        Try
            With C.Com
                .Connection = C.Con
                .CommandText = "[Search_Tr_MV]"

                .Parameters.AddWithValue("@AllAgentsCheckBox", AllTrCheckBox.Checked)
                .Parameters.AddWithValue("@AllUsersCheckBox", TrAllUsersCheckBox.Checked)
                .Parameters.AddWithValue("@AllTimeCheckBox", TrAllTimeCheckBox.Checked)
                .Parameters.AddWithValue("@AllRecieptsCheckBox", TrAllTypeCheckBox.Checked)

                .Parameters.AddWithValue("@DateTimePicker_From", TrDateTimePicker_F.Value)
                .Parameters.AddWithValue("@DateTimePicker_To", TrDateTimePicker_T.Value)

                .Parameters.AddWithValue("@Tr_ID", Treasury_ComboBox.SelectedValue)
                .Parameters.AddWithValue("@ReceiptTypeComboBox", TrTypeComboBox.SelectedValue)
                .Parameters.AddWithValue("@UsersComboBox", TrUsersComboBox.SelectedValue)

                .CommandType = CommandType.StoredProcedure
            End With
            C.Da = New SqlClient.SqlDataAdapter(C.Com)
            TR_MV_DT = New DataTable()
            C.Da.Fill(TR_MV_DT)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function


    Private Function ValidateSalarySearchInputs() As Boolean

        If String.IsNullOrWhiteSpace(SalariesMonthComboBox.Text) Then
            MsgBox("حدد الشهر", MsgBoxStyle.Exclamation)
            Return False
        End If

        If String.IsNullOrWhiteSpace(YearComboBox.Text) Then
            MsgBox("حدد السنة", MsgBoxStyle.Exclamation)
            Return False
        End If

        Return True

    End Function

    Private Sub SalarySearchButton_Click(sender As Object, e As EventArgs) Handles SalarySearchButton.Click
        If ValidateSalarySearchInputs() = False Then Exit Sub

        Me.Cursor = Cursors.AppStarting
        salary()
        Me.Cursor = Cursors.Default
    End Sub

    Public Sub salary()

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "CALC_EMP_SALARIES"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@MONTH", SalariesMonthComboBox.Text)
            .Parameters.AddWithValue("@YEAR", YearComboBox.Text)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        SalariesMetroGrid.DataSource = C.Dt

    End Sub


    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        If ValidateSalarySearchInputs() = False Then Return
        SalarySearchButton_Click(sender, e)
        Print_Salaries()
    End Sub

    Public Sub Print_Salaries()

        If GetSalaryPrintableRowsCount() = 0 Then
            MsgBox("لا توجد بيانات للطباعة.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Using printDocument As System.Drawing.Printing.PrintDocument = CreateSalariesPrintDocument()
            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة تقرير المرتبات"
                previewDialog.ShowDialog(Me)
            End Using
        End Using

    End Sub

    Private Function CreateSalariesPrintDocument() As System.Drawing.Printing.PrintDocument

        Dim printDocument As New System.Drawing.Printing.PrintDocument()
        printDocument.DocumentName = "تقرير المرتبات"
        printDocument.DefaultPageSettings.Landscape = True
        printDocument.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(35, 35, 45, 45)

        AddHandler printDocument.BeginPrint, AddressOf SalariesPrintDocument_BeginPrint
        AddHandler printDocument.PrintPage, AddressOf SalariesPrintDocument_PrintPage

        Return printDocument

    End Function

    Private Sub SalariesPrintDocument_BeginPrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs)

        _salaryPrintRowIndex = 0
        _salaryPrintPageNumber = 1
        _salaryPrintTotalPages = 1
        _salaryPrintRowsPerPage = 0
        _salaryPrintTotalRows = GetSalaryPrintableRowsCount()
        _salarySummaryPrinted = False

    End Sub

    Private Sub SalariesPrintDocument_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs)

        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top

        Using storeTitleFont As New Font("Segoe UI", 15.0!, FontStyle.Bold),
              storeSubTitleFont As New Font("Segoe UI", 10.0!, FontStyle.Bold),
              titleFont As New Font("Segoe UI", 13.0!, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 9.0!, FontStyle.Bold),
              headerFont As New Font("Segoe UI", 8.0!, FontStyle.Bold),
              rowFont As New Font("Segoe UI", 8.0!, FontStyle.Regular),
              totalsFont As New Font("Segoe UI", 9.0!, FontStyle.Bold)

            Dim rtlFormat As New StringFormat()
            rtlFormat.Alignment = StringAlignment.Far
            rtlFormat.LineAlignment = StringAlignment.Center
            rtlFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

            Dim centerFormat As New StringFormat()
            centerFormat.Alignment = StringAlignment.Center
            centerFormat.LineAlignment = StringAlignment.Center
            centerFormat.Trimming = StringTrimming.EllipsisCharacter

            DrawReportStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

            e.Graphics.DrawString(
                "تقرير المرتبات",
                titleFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 28),
                rtlFormat
            )
            y += 30

            e.Graphics.DrawString(
                "لشهر: " & SalariesMonthComboBox.Text & "    سنة: " & YearComboBox.Text,
                infoFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 24),
                rtlFormat
            )
            y += 30

            Dim columnNames As String() = {
                "DataGridViewTextBoxColumn12",
                "Salary_CL",
                "SumDebit_CL",
                "T_OtherVal_ADD_CL",
                "T_OtherVal_Neg_CL",
                "PureSalaey_CL",
                "Inserted_Salary_CL"
            }

            Dim headers As String() = {
                "الموظف",
                "المرتب الشهري",
                "دفعات على الحساب",
                "مكافئات",
                "خصومات",
                "المرتب المرصد",
                "صافي المرتب"
            }

            Dim weights As Integer() = {230, 115, 125, 95, 95, 120, 120}
            Dim totalWeight As Integer = 0
            For Each weight As Integer In weights
                totalWeight += weight
            Next
            Dim widths(weights.Length - 1) As Integer

            For i As Integer = 0 To weights.Length - 1
                widths(i) = CInt(bounds.Width * (weights(i) / CDbl(totalWeight)))
            Next

            Dim rowHeight As Integer = 28
            Dim tableBottom As Integer = bounds.Bottom - 42
            Dim x As Integer = bounds.Right

            For i As Integer = 0 To headers.Length - 1
                x -= widths(i)
                Dim rect As New Rectangle(x, y, widths(i), rowHeight)
                Using backBrush As New SolidBrush(Color.FromArgb(45, 62, 80))
                    e.Graphics.FillRectangle(backBrush, rect)
                End Using
                e.Graphics.DrawRectangle(Pens.DarkGray, rect)
                e.Graphics.DrawString(headers(i), headerFont, Brushes.White, rect, centerFormat)
            Next

            y += rowHeight

            If _salaryPrintRowsPerPage = 0 Then
                Dim printableTableHeight As Integer = Math.Max(rowHeight, tableBottom - y)
                _salaryPrintRowsPerPage = Math.Max(1, CInt(Math.Floor(printableTableHeight / CDbl(rowHeight))))
                _salaryPrintTotalPages = CalculateSalaryPrintTotalPages(_salaryPrintTotalRows, _salaryPrintRowsPerPage, printableTableHeight, rowHeight, 34)
            End If

            While _salaryPrintRowIndex < SalariesMetroGrid.Rows.Count

                Dim row As DataGridViewRow = SalariesMetroGrid.Rows(_salaryPrintRowIndex)

                If row.IsNewRow Then
                    _salaryPrintRowIndex += 1
                    Continue While
                End If

                If y + rowHeight > tableBottom Then
                    DrawReportFooter(e.Graphics, bounds, _salaryPrintPageNumber, _salaryPrintTotalPages, infoFont, centerFormat)
                    _salaryPrintPageNumber += 1
                    e.HasMorePages = True
                    Return
                End If

                x = bounds.Right

                For i As Integer = 0 To columnNames.Length - 1
                    x -= widths(i)
                    Dim rect As New Rectangle(x, y, widths(i), rowHeight)

                    If _salaryPrintRowIndex Mod 2 = 0 Then
                        e.Graphics.FillRectangle(Brushes.White, rect)
                    Else
                        Using altBrush As New SolidBrush(Color.FromArgb(248, 250, 252))
                            e.Graphics.FillRectangle(altBrush, rect)
                        End Using
                    End If

                    e.Graphics.DrawRectangle(Pens.LightGray, rect)
                    e.Graphics.DrawString(GetSalaryPrintCellText(row, columnNames(i)), rowFont, Brushes.Black, rect, centerFormat)
                Next

                y += rowHeight
                _salaryPrintRowIndex += 1

            End While

            If _salarySummaryPrinted = False Then
                If y + 34 > tableBottom Then
                    _salaryPrintTotalPages = Math.Max(_salaryPrintTotalPages, _salaryPrintPageNumber + 1)
                    DrawReportFooter(e.Graphics, bounds, _salaryPrintPageNumber, _salaryPrintTotalPages, infoFont, centerFormat)
                    _salaryPrintPageNumber += 1
                    e.HasMorePages = True
                    Return
                End If

                y += 6
                e.Graphics.DrawString(
                    "إجمالي دفعات على الحساب: " & TotalDebtTextBox.Text &
                    "    إجمالي صافي المرتبات: " & TotalPureTextBox.Text,
                    totalsFont,
                    Brushes.Black,
                    New Rectangle(bounds.Left, y, bounds.Width, 28),
                    rtlFormat
                )
                _salarySummaryPrinted = True
            End If

            DrawReportFooter(e.Graphics, bounds, _salaryPrintPageNumber, _salaryPrintTotalPages, infoFont, centerFormat)

        End Using

        e.HasMorePages = False

    End Sub

    Private Function GetSalaryPrintableRowsCount() As Integer

        Dim count As Integer = 0

        For Each row As DataGridViewRow In SalariesMetroGrid.Rows
            If row.IsNewRow = False Then count += 1
        Next

        Return count

    End Function

    Private Function CalculateSalaryPrintTotalPages(totalRows As Integer, rowsPerPage As Integer, printableTableHeight As Integer, rowHeight As Integer, summaryHeight As Integer) As Integer

        If totalRows <= 0 Then Return 1

        Dim totalPages As Integer = CalculateTotalPrintPages(totalRows, rowsPerPage)
        Dim lastPageRows As Integer = totalRows Mod rowsPerPage

        If lastPageRows = 0 Then lastPageRows = rowsPerPage

        Dim lastPageRemainingHeight As Integer = printableTableHeight - (lastPageRows * rowHeight)

        If lastPageRemainingHeight < summaryHeight Then totalPages += 1

        Return Math.Max(1, totalPages)

    End Function

    Private Function GetSalaryPrintCellText(row As DataGridViewRow, columnName As String) As String

        If Not SalariesMetroGrid.Columns.Contains(columnName) Then Return ""

        Dim value As Object = row.Cells(columnName).Value

        If value Is Nothing OrElse value Is DBNull.Value Then Return ""

        If columnName = "Salary_CL" OrElse
           columnName = "SumDebit_CL" OrElse
           columnName = "T_OtherVal_ADD_CL" OrElse
           columnName = "T_OtherVal_Neg_CL" OrElse
           columnName = "PureSalaey_CL" OrElse
           columnName = "Inserted_Salary_CL" Then

            Dim numberValue As Decimal
            If Decimal.TryParse(value.ToString(), numberValue) Then Return numberValue.ToString("N2")
        End If

        Return value.ToString()

    End Function

    Private Sub AllBalancesRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AllBalancesRadioButton.CheckedChanged
        Balance_Type = 0
        'Get_AgentsBalances()
    End Sub

    Private Sub AlldebitsRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AlldebitsRadioButton.CheckedChanged
        Balance_Type = 1
        '  Get_AgentsBalances()
    End Sub

    Private Sub AllCerditRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AllCerditRadioButton.CheckedChanged
        Balance_Type = -1
        ' Get_AgentsBalances()
    End Sub

    Private Sub AGBalancesPrintBtn_Click(sender As Object, e As EventArgs) Handles AGBalancesPrintBtn.Click
        Print_AG_Balances()
    End Sub

    Private Sub AG_Report_Copy()

        Try

            For i = 0 To DebtMetroGrid.Rows.Count - 1

                Dim sqlComm As New SqlClient.SqlCommand
                sqlComm.CommandText = "[AG_BALANCE_REPORT_INSERT]"
                sqlComm.CommandType = CommandType.StoredProcedure

                sqlComm.Parameters.AddWithValue("@AG_NAME", DebtMetroGrid.Rows(i).Cells("Agent_Report_name").Value)
                sqlComm.Parameters.AddWithValue("@T_DEBIT", DebtMetroGrid.Rows(i).Cells("B_T_Debit_CL").Value)
                sqlComm.Parameters.AddWithValue("@T_CREDIT", DebtMetroGrid.Rows(i).Cells("B_T_Credit_CL").Value)
                sqlComm.Parameters.AddWithValue("@T_Balance", DebtMetroGrid.Rows(i).Cells("AG_BALANCE_T_CL").Value)
                SQL_SP_EXEC(sqlComm)
            Next


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub AG_Report_print_Reset()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[AG_BALANCE_REPORT_DELETE]"
        sqlComm.CommandType = CommandType.StoredProcedure
        SQL_SP_EXEC(sqlComm)
    End Sub

    Public Sub Print_AG_Balances()

        If DebtMetroGrid.Rows.Count = 0 Then
            MsgBox("لا توجد بيانات للطباعة.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Using printDocument As New System.Drawing.Printing.PrintDocument()

            printDocument.DocumentName = "كشف أرصدة الحسابات"
            printDocument.DefaultPageSettings.Landscape = True
            printDocument.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(35, 35, 45, 45)

            AddHandler printDocument.BeginPrint, AddressOf AG_Balances_PrintDocument_BeginPrint
            AddHandler printDocument.PrintPage, AddressOf AG_Balances_PrintDocument_PrintPage

            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة كشف أرصدة الحسابات"
                previewDialog.ShowDialog(Me)
            End Using

        End Using

    End Sub

    Private Sub AG_Balances_PrintDocument_BeginPrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs)

        _agBalancesPrintRowIndex = 0

    End Sub

    Private Sub AG_Balances_PrintDocument_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs)

        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top

        Using storeTitleFont As New Font("Segoe UI", 15.0!, FontStyle.Bold),
              storeSubTitleFont As New Font("Segoe UI", 10.0!, FontStyle.Bold),
              titleFont As New Font("Segoe UI", 13.0!, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 9.0!, FontStyle.Bold),
              headerFont As New Font("Segoe UI", 8.0!, FontStyle.Bold),
              rowFont As New Font("Segoe UI", 8.0!, FontStyle.Regular),
              totalsFont As New Font("Segoe UI", 9.0!, FontStyle.Bold)

            Dim rtlFormat As New StringFormat()
            rtlFormat.Alignment = StringAlignment.Far
            rtlFormat.LineAlignment = StringAlignment.Center
            rtlFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

            Dim centerFormat As New StringFormat()
            centerFormat.Alignment = StringAlignment.Center
            centerFormat.LineAlignment = StringAlignment.Center
            centerFormat.Trimming = StringTrimming.EllipsisCharacter

            DrawReportStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

            e.Graphics.DrawString(
                "كشف أرصدة الحسابات",
                titleFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 28),
                rtlFormat
            )
            y += 30

            e.Graphics.DrawString(
                GetAgBalancesFilterText() & "    " & GetAgBalancesDateText() & "    الأنواع: " & If(String.IsNullOrWhiteSpace(Rpt_Name), "الكـــل", Rpt_Name),
                infoFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 24),
                rtlFormat
            )
            y += 30

            Dim columnNames As String() = {
                "__ROW_NO__",
                "Agent_Report_name",
                "B_T_Debit_CL",
                "B_T_Credit_CL",
                "AG_BALANCE_T_CL"
            }

            Dim headers As String() = {
                "ت",
                "الحساب",
                "دائن",
                "مدين",
                "الرصيد"
            }

            Dim weights As Integer() = {45, 360, 125, 125, 135}
            Dim totalWeight As Integer = 0
            For Each weight As Integer In weights
                totalWeight += weight
            Next
            Dim widths(weights.Length - 1) As Integer

            For i As Integer = 0 To weights.Length - 1
                widths(i) = CInt(bounds.Width * (weights(i) / CDbl(totalWeight)))
            Next

            Dim rowHeight As Integer = 28
            Dim x As Integer = bounds.Right

            For i As Integer = 0 To headers.Length - 1
                x -= widths(i)
                Dim rect As New Rectangle(x, y, widths(i), rowHeight)
                Using backBrush As New SolidBrush(Color.FromArgb(45, 62, 80))
                    e.Graphics.FillRectangle(backBrush, rect)
                End Using
                e.Graphics.DrawRectangle(Pens.DarkGray, rect)
                e.Graphics.DrawString(headers(i), headerFont, Brushes.White, rect, centerFormat)
            Next

            y += rowHeight
            Dim rowsPerPage As Integer = CalculatePrintableRowsPerPage(y, bounds.Bottom, rowHeight)
            Dim totalPages As Integer = CalculateTotalPrintPages(DebtMetroGrid.Rows.Count, rowsPerPage)
            Dim currentPage As Integer = CalculateCurrentPrintPage(_agBalancesPrintRowIndex, rowsPerPage)

            While _agBalancesPrintRowIndex < DebtMetroGrid.Rows.Count

                If y + rowHeight > bounds.Bottom - 42 Then
                    DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)
                    e.HasMorePages = True
                    Return
                End If

                Dim row As DataGridViewRow = DebtMetroGrid.Rows(_agBalancesPrintRowIndex)
                x = bounds.Right

                For i As Integer = 0 To columnNames.Length - 1
                    x -= widths(i)
                    Dim rect As New Rectangle(x, y, widths(i), rowHeight)

                    If _agBalancesPrintRowIndex Mod 2 = 0 Then
                        e.Graphics.FillRectangle(Brushes.White, rect)
                    Else
                        Using altBrush As New SolidBrush(Color.FromArgb(248, 250, 252))
                            e.Graphics.FillRectangle(altBrush, rect)
                        End Using
                    End If

                    e.Graphics.DrawRectangle(Pens.LightGray, rect)
                    e.Graphics.DrawString(GetAgBalancePrintCellText(row, columnNames(i), _agBalancesPrintRowIndex + 1), rowFont, Brushes.Black, rect, centerFormat)
                Next

                y += rowHeight
                _agBalancesPrintRowIndex += 1

            End While

            y += 10
            e.Graphics.DrawString(
                "إجمالي الدائن: " & T_DebitTextBox.Text &
                "    إجمالي المدين: " & T_CreditTextBox.Text &
                "    الرصيد: " & T_BalanceTextBox.Text,
                totalsFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 28),
                rtlFormat
            )

            DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)

        End Using

        e.HasMorePages = False

    End Sub

    Private Function GetAgBalancePrintCellText(row As DataGridViewRow, columnName As String, rowNumber As Integer) As String

        If columnName = "__ROW_NO__" Then Return rowNumber.ToString()

        If Not DebtMetroGrid.Columns.Contains(columnName) Then Return ""

        Dim value As Object = row.Cells(columnName).Value

        If value Is Nothing OrElse value Is DBNull.Value Then Return ""

        If columnName = "B_T_Debit_CL" OrElse columnName = "B_T_Credit_CL" OrElse columnName = "AG_BALANCE_T_CL" Then
            Dim numberValue As Decimal
            If Decimal.TryParse(value.ToString(), numberValue) Then Return numberValue.ToString("N2")
        End If

        Return value.ToString()

    End Function

    Private Function GetAgBalancesFilterText() As String

        If Type_Cm.SelectedIndex <> 0 Then Return "حسب : " & Type_Cm.Text

        If AllBalancesRadioButton.Checked = True Then Return "حسب : الـكـل"
        If AlldebitsRadioButton.Checked = True Then Return "حسب : المدينيــن"

        Return "حسب : الدائنيــن"

    End Function

    Private Function GetAgBalancesDateText() As String

        If Type_Cm.SelectedIndex <> 0 Then Return ""
        If Debit_WithDate_CB.Checked = True Then Return "الديون حتى تاريخ: " & DATE_Before.Text

        Return "الديون حتى تاريخ الطباعة"

    End Function

    Private Sub DrawReportStoreHeader(graphics As Graphics, bounds As Rectangle, ByRef y As Integer, storeTitleFont As Font, storeSubTitleFont As Font, centerFormat As StringFormat)

        If Not String.IsNullOrWhiteSpace(SBill_Title_1) Then
            graphics.DrawString(
                SBill_Title_1,
                storeTitleFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 30),
                centerFormat
            )
            y += 30
        End If

        If Not String.IsNullOrWhiteSpace(SBill_Title_2) Then
            graphics.DrawString(
                SBill_Title_2,
                storeSubTitleFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 24),
                centerFormat
            )
            y += 24
        End If

    End Sub

    Private Function CalculatePrintableRowsPerPage(firstRowY As Integer, pageBottom As Integer, rowHeight As Integer) As Integer

        Dim availableHeight As Integer = pageBottom - 42 - firstRowY

        If availableHeight <= 0 Then Return 1

        Return Math.Max(1, CInt(Math.Floor(availableHeight / CDbl(rowHeight))))

    End Function

    Private Function CalculateTotalPrintPages(totalRows As Integer, rowsPerPage As Integer) As Integer

        If totalRows <= 0 Then Return 1

        Return Math.Max(1, CInt(Math.Ceiling(totalRows / CDbl(rowsPerPage))))

    End Function

    Private Function CalculateCurrentPrintPage(rowIndex As Integer, rowsPerPage As Integer) As Integer

        Return Math.Max(1, CInt(Math.Floor(rowIndex / CDbl(rowsPerPage))) + 1)

    End Function

    Private Sub DrawReportFooter(graphics As Graphics, bounds As Rectangle, currentPage As Integer, totalPages As Integer, footerFont As Font, centerFormat As StringFormat)

        Dim footerTop As Integer = bounds.Bottom - 26
        Dim sideWidth As Integer = CInt(bounds.Width * 0.34)
        Dim centerWidth As Integer = bounds.Width - (sideWidth * 2)

        Dim rightFormat As New StringFormat(centerFormat)
        rightFormat.Alignment = StringAlignment.Far
        rightFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

        Dim leftFormat As New StringFormat(centerFormat)
        leftFormat.Alignment = StringAlignment.Near

        graphics.DrawString(
            "المعد: " & USER_NAME,
            footerFont,
            Brushes.Black,
            New Rectangle(bounds.Right - sideWidth, footerTop, sideWidth, 24),
            rightFormat
        )

        graphics.DrawString(
            currentPage & "/" & totalPages,
            footerFont,
            Brushes.Black,
            New Rectangle(bounds.Left + sideWidth, footerTop, centerWidth, 24),
            centerFormat
        )

        graphics.DrawString(
            "تاريخ الطباعة: " & Date.Now.ToString("yyyy/MM/dd HH:mm"),
            footerFont,
            Brushes.Black,
            New Rectangle(bounds.Left, footerTop, sideWidth, 24),
            leftFormat
        )

    End Sub


    Private Sub TrMonthComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TrMonthComboBox.SelectedIndexChanged
        Try
            TrDateTimePicker_F = GetFirstDayOfMonth(TrDateTimePicker_F, TrMonthComboBox.Text)
            TrDateTimePicker_T = GetLastDayOfMonth(TrDateTimePicker_T, TrMonthComboBox.Text)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub AGMVMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMVMetroGrid.MouseDoubleClick
        Me.Cursor = Cursors.AppStarting
        If AGMVMetroGrid.Rows.Count > 0 Then
            'If AGMVMetroGrid.CurrentRow.Cells("AG_MV_Type_ID_CL").Value <> 6 Then
            Select Case AGMVMetroGrid.CurrentRow.Cells("AG_MV_Type_ID_CL").Value
                Case 3, 4
                    AG_Type = AGMVMetroGrid.CurrentRow.Cells("AG_MV_Type_ID_CL").Value
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_Receipt = New Receipt
                    F_Receipt.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 5, 6, 37
                    Add_Prev_Balance.is_Select = True
                    Add_Prev_Balance.ShowDialog()

                Case 40
                    AG_Transfers.is_Select = True
                    AG_Transfers.ShowDialog()

                Case 1
                    FormType = 1
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_Sales = New Sales
                    F_Sales.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0
                Case 7
                    FormType = 2
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_Pch = New Pch
                    F_Pch.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 8
                    FormType = 3
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_IM_Execute = New IM_Execute
                    F_IM_Execute.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 9
                    FormType = 4
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_Invoice = New Invoice
                    F_Invoice.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0
                Case 10
                    FormType = 5
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    Returns.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 11
                    FormType = 6
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    Returns.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 12
                    FormType = 7
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_Stores_ImmediateOrder = New Stores_ImmediateOrder
                    F_Stores_ImmediateOrder.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 2
                    FormType = 8
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_EXP_Details = New EXP_Details
                    F_EXP_Details.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 13
                    FormType = 9
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_Format_Items_Auto = New Format_Items_Auto
                    F_Format_Items_Auto.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 16
                    FormType = 10
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_ViewBill = New ViewBill
                    F_ViewBill.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 17
                    FormType = 11
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_Inside_Sales = New Inside_Sales
                    F_Inside_Sales.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 18
                    FormType = 9
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_Format_Items_Manual = New Format_Items_Manual
                    F_Format_Items_Manual.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 35
                    FormType = 13
                    T_ID_Trans = AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    isShowing_Trans = True
                    F_Outside_Sales = New Outside_Sales
                    F_Outside_Sales.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0


            End Select
            ' End If
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub MVPrintButton_Click(sender As Object, e As EventArgs) Handles MVPrintButton.Click
        PrepareAgMvReportTitles()
        AG_MV_print()
    End Sub

    Public Sub AG_MV_print_Reset()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_MV_Reports_DELETE"
        sqlComm.CommandType = CommandType.StoredProcedure
        SQL_SP_EXEC(sqlComm)
    End Sub

    Private Sub AG_MV_print()

        If AGMVMetroGrid.Rows.Count = 0 Then
            MsgBox("لا توجد بيانات للطباعة.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Using printDocument As New System.Drawing.Printing.PrintDocument()

            printDocument.DocumentName = "كشف حساب"
            printDocument.DefaultPageSettings.Landscape = True
            printDocument.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(35, 35, 45, 45)

            AddHandler printDocument.BeginPrint, AddressOf AG_MV_PrintDocument_BeginPrint
            AddHandler printDocument.PrintPage, AddressOf AG_MV_PrintDocument_PrintPage

            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة كشف الحساب"
                previewDialog.ShowDialog(Me)
            End Using

        End Using

    End Sub

    Private Sub AG_MV_PrintDocument_BeginPrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs)

        _agMvPrintRowIndex = 0

    End Sub

    Private Sub AG_MV_PrintDocument_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs)

        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top

        Using storeTitleFont As New Font("Segoe UI", 15.0!, FontStyle.Bold),
              storeSubTitleFont As New Font("Segoe UI", 10.0!, FontStyle.Bold),
              titleFont As New Font("Segoe UI", 13.0!, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 9.0!, FontStyle.Bold),
              headerFont As New Font("Segoe UI", 8.0!, FontStyle.Bold),
              rowFont As New Font("Segoe UI", 8.0!, FontStyle.Regular),
              totalsFont As New Font("Segoe UI", 9.0!, FontStyle.Bold)

            Dim rtlFormat As New StringFormat()
            rtlFormat.Alignment = StringAlignment.Far
            rtlFormat.LineAlignment = StringAlignment.Center
            rtlFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

            Dim centerFormat As New StringFormat()
            centerFormat.Alignment = StringAlignment.Center
            centerFormat.LineAlignment = StringAlignment.Center
            centerFormat.Trimming = StringTrimming.EllipsisCharacter

            DrawReportStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

            e.Graphics.DrawString(
                "كشف حساب",
                titleFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 28),
                rtlFormat
            )
            y += 30

            e.Graphics.DrawString(
                "الحساب: " & AGMV_Name & "    النوع: " & AGMV_Type & "    الفترة: " & AGMV_Date & "    المستخدم: " & AGMV_User,
                infoFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 24),
                rtlFormat
            )
            y += 30

            Dim columnNames As String() = {
                "AGisVoid_CL",
                "Date_CL",
                "Type_Name_CL",
                "ReferNum_CL",
                "Receipt_Title_CL",
                "Debit_CL",
                "Credit_CL",
                "Balance_CL",
                "UserEntry"
            }

            Dim headers As String() = {
                "الحالة",
                "التاريخ",
                "المعاملة",
                "ر.إشاري",
                "العنوان",
                "دائن",
                "مدين",
                "الرصيد",
                "المدخل"
            }

            Dim weights As Integer() = {65, 90, 100, 75, 230, 90, 90, 95, 85}
            Dim totalWeight As Integer = 0
            For Each weight As Integer In weights
                totalWeight += weight
            Next
            Dim widths(weights.Length - 1) As Integer

            For i As Integer = 0 To weights.Length - 1
                widths(i) = CInt(bounds.Width * (weights(i) / CDbl(totalWeight)))
            Next

            Dim rowHeight As Integer = 28
            Dim x As Integer = bounds.Right

            For i As Integer = 0 To headers.Length - 1
                x -= widths(i)
                Dim rect As New Rectangle(x, y, widths(i), rowHeight)
                Using backBrush As New SolidBrush(Color.FromArgb(45, 62, 80))
                    e.Graphics.FillRectangle(backBrush, rect)
                End Using
                e.Graphics.DrawRectangle(Pens.DarkGray, rect)
                e.Graphics.DrawString(headers(i), headerFont, Brushes.White, rect, centerFormat)
            Next

            y += rowHeight
            Dim rowsPerPage As Integer = CalculatePrintableRowsPerPage(y, bounds.Bottom, rowHeight)
            Dim totalPages As Integer = CalculateTotalPrintPages(AGMVMetroGrid.Rows.Count, rowsPerPage)
            Dim currentPage As Integer = CalculateCurrentPrintPage(_agMvPrintRowIndex, rowsPerPage)

            While _agMvPrintRowIndex < AGMVMetroGrid.Rows.Count

                If y + rowHeight > bounds.Bottom - 42 Then
                    DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)
                    e.HasMorePages = True
                    Return
                End If

                Dim row As DataGridViewRow = AGMVMetroGrid.Rows(_agMvPrintRowIndex)
                Dim isVoidRow As Boolean = GetBooleanCellValue(row, "AGisVoid_CL")
                x = bounds.Right

                For i As Integer = 0 To columnNames.Length - 1
                    x -= widths(i)
                    Dim rect As New Rectangle(x, y, widths(i), rowHeight)
                    Dim isVoidStatusCell As Boolean = isVoidRow AndAlso columnNames(i) = "AGisVoid_CL"
                    Dim cellBackColor As Color

                    If isVoidStatusCell Then
                        cellBackColor = Color.FromArgb(185, 28, 28)
                    ElseIf isVoidRow Then
                        cellBackColor = Color.FromArgb(255, 241, 242)
                    ElseIf _agMvPrintRowIndex Mod 2 = 0 Then
                        cellBackColor = Color.White
                    Else
                        cellBackColor = Color.FromArgb(248, 250, 252)
                    End If

                    Using cellBrush As New SolidBrush(cellBackColor)
                        e.Graphics.FillRectangle(cellBrush, rect)
                    End Using

                    Dim cellBrushText As Brush = If(isVoidStatusCell, Brushes.White, If(isVoidRow, Brushes.Firebrick, Brushes.Black))
                    Dim cellFont As Font = If(isVoidStatusCell, headerFont, rowFont)

                    If isVoidRow Then
                        Using borderPen As New Pen(Color.FromArgb(248, 113, 113))
                            e.Graphics.DrawRectangle(borderPen, rect)
                        End Using
                    Else
                        e.Graphics.DrawRectangle(Pens.LightGray, rect)
                    End If

                    e.Graphics.DrawString(GetAgMvPrintCellText(row, columnNames(i)), cellFont, cellBrushText, rect, centerFormat)
                Next

                y += rowHeight
                _agMvPrintRowIndex += 1

            End While

            y += 10
            e.Graphics.DrawString(
                "إجمالي الدائن: " & Total_Debit.Text &
                "    إجمالي المدين: " & Total_Credit.Text &
                "    الرصيد: " & Total_Balance.Text &
                "    عدد الدائن: " & NUM_DEBIT_TXT.Text &
                "    عدد المدين: " & NUM_CREDIT_TXT.Text,
                totalsFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 28),
                rtlFormat
            )

            DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)

        End Using

        e.HasMorePages = False

    End Sub

    Private Function GetAgMvPrintCellText(row As DataGridViewRow, columnName As String) As String

        If Not AGMVMetroGrid.Columns.Contains(columnName) Then Return ""

        Dim value As Object = row.Cells(columnName).Value

        If value Is Nothing OrElse value Is DBNull.Value Then Return ""

        If columnName = "AGisVoid_CL" Then
            Return If(GetBooleanCellValue(row, columnName), "ملغية", "")
        End If

        If columnName = "Date_CL" Then
            Dim dateValue As Date
            If Date.TryParse(value.ToString(), dateValue) Then Return dateValue.ToString("yyyy/MM/dd")
        End If

        If columnName = "Debit_CL" OrElse columnName = "Credit_CL" OrElse columnName = "Balance_CL" Then
            Dim numberValue As Decimal
            If Decimal.TryParse(value.ToString(), numberValue) Then
                If (columnName = "Debit_CL" OrElse columnName = "Credit_CL") AndAlso numberValue = 0D Then Return ""
                Return numberValue.ToString("N3")
            End If
        End If

        Return value.ToString()

    End Function

    Private Sub Tr_PrintButton_Click(sender As Object, e As EventArgs) Handles Tr_PrintButton.Click
        PrepareTrMvReportTitles()
        TR_MV_print()
    End Sub

    Private Sub TR_MV_print()

        If Tr_MV_MetroGrid.Rows.Count = 0 Then
            MsgBox("لا توجد بيانات للطباعة.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Using printDocument As New System.Drawing.Printing.PrintDocument()

            printDocument.DocumentName = "كشف حساب خزينة"
            printDocument.DefaultPageSettings.Landscape = True
            printDocument.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(35, 35, 45, 45)

            AddHandler printDocument.BeginPrint, AddressOf TR_MV_PrintDocument_BeginPrint
            AddHandler printDocument.PrintPage, AddressOf TR_MV_PrintDocument_PrintPage

            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة كشف حساب الخزينة"
                previewDialog.ShowDialog(Me)
            End Using

        End Using

    End Sub

    Private Sub TR_MV_PrintDocument_BeginPrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs)

        _trMvPrintRowIndex = 0

    End Sub

    Private Sub TR_MV_PrintDocument_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs)

        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top

        Using storeTitleFont As New Font("Segoe UI", 15.0!, FontStyle.Bold),
              storeSubTitleFont As New Font("Segoe UI", 10.0!, FontStyle.Bold),
              titleFont As New Font("Segoe UI", 13.0!, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 9.0!, FontStyle.Bold),
              headerFont As New Font("Segoe UI", 8.0!, FontStyle.Bold),
              rowFont As New Font("Segoe UI", 8.0!, FontStyle.Regular),
              totalsFont As New Font("Segoe UI", 9.0!, FontStyle.Bold)

            Dim rtlFormat As New StringFormat()
            rtlFormat.Alignment = StringAlignment.Far
            rtlFormat.LineAlignment = StringAlignment.Center
            rtlFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

            Dim centerFormat As New StringFormat()
            centerFormat.Alignment = StringAlignment.Center
            centerFormat.LineAlignment = StringAlignment.Center
            centerFormat.Trimming = StringTrimming.EllipsisCharacter

            DrawReportStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

            e.Graphics.DrawString(
                "كشف حساب خزينة",
                titleFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 28),
                rtlFormat
            )
            y += 30

            e.Graphics.DrawString(
                "الخزينة: " & TRMV_Name & "    النوع: " & TRMV_Type & "    الفترة: " & TRMV_Date & "    المستخدم: " & TRMV_User,
                infoFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 24),
                rtlFormat
            )
            y += 30

            Dim columnNames As String() = {
                "TrisVoid_CL",
                "TRMV_Date_CL",
                "TRMV_Type_CL",
                "TR_MV_Title_move_CL",
                "Ag_name_CL_2",
                "Tr_Credit_CL",
                "Tr_Debit_CL",
                "Tr_Balance_CL",
                "TRMV_UserName_CL"
            }

            Dim headers As String() = {
                "الحالة",
                "التاريخ",
                "المعاملة",
                "العنوان",
                "الحساب",
                "مدين",
                "دائن",
                "الرصيد",
                "المدخل"
            }

            Dim weights As Integer() = {65, 90, 100, 250, 120, 90, 90, 95, 85}
            Dim totalWeight As Integer = 0
            For Each weight As Integer In weights
                totalWeight += weight
            Next
            Dim widths(weights.Length - 1) As Integer

            For i As Integer = 0 To weights.Length - 1
                widths(i) = CInt(bounds.Width * (weights(i) / CDbl(totalWeight)))
            Next

            Dim rowHeight As Integer = 28
            Dim x As Integer = bounds.Right

            For i As Integer = 0 To headers.Length - 1
                x -= widths(i)
                Dim rect As New Rectangle(x, y, widths(i), rowHeight)
                Using backBrush As New SolidBrush(Color.FromArgb(45, 62, 80))
                    e.Graphics.FillRectangle(backBrush, rect)
                End Using
                e.Graphics.DrawRectangle(Pens.DarkGray, rect)
                e.Graphics.DrawString(headers(i), headerFont, Brushes.White, rect, centerFormat)
            Next

            y += rowHeight
            Dim rowsPerPage As Integer = CalculatePrintableRowsPerPage(y, bounds.Bottom, rowHeight)
            Dim totalPages As Integer = CalculateTotalPrintPages(Tr_MV_MetroGrid.Rows.Count, rowsPerPage)
            Dim currentPage As Integer = CalculateCurrentPrintPage(_trMvPrintRowIndex, rowsPerPage)

            While _trMvPrintRowIndex < Tr_MV_MetroGrid.Rows.Count

                If y + rowHeight > bounds.Bottom - 42 Then
                    DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)
                    e.HasMorePages = True
                    Return
                End If

                Dim row As DataGridViewRow = Tr_MV_MetroGrid.Rows(_trMvPrintRowIndex)
                Dim isVoidRow As Boolean = GetBooleanCellValue(row, "TrisVoid_CL")
                x = bounds.Right

                For i As Integer = 0 To columnNames.Length - 1
                    x -= widths(i)
                    Dim rect As New Rectangle(x, y, widths(i), rowHeight)
                    Dim isVoidStatusCell As Boolean = isVoidRow AndAlso columnNames(i) = "TrisVoid_CL"
                    Dim cellBackColor As Color

                    If isVoidStatusCell Then
                        cellBackColor = Color.FromArgb(185, 28, 28)
                    ElseIf isVoidRow Then
                        cellBackColor = Color.FromArgb(255, 241, 242)
                    ElseIf _trMvPrintRowIndex Mod 2 = 0 Then
                        cellBackColor = Color.White
                    Else
                        cellBackColor = Color.FromArgb(248, 250, 252)
                    End If

                    Using cellBrush As New SolidBrush(cellBackColor)
                        e.Graphics.FillRectangle(cellBrush, rect)
                    End Using

                    Dim cellBrushText As Brush = If(isVoidStatusCell, Brushes.White, If(isVoidRow, Brushes.Firebrick, Brushes.Black))
                    Dim cellFont As Font = If(isVoidStatusCell, headerFont, rowFont)

                    If isVoidRow Then
                        Using borderPen As New Pen(Color.FromArgb(248, 113, 113))
                            e.Graphics.DrawRectangle(borderPen, rect)
                        End Using
                    Else
                        e.Graphics.DrawRectangle(Pens.LightGray, rect)
                    End If

                    e.Graphics.DrawString(GetTrMvPrintCellText(row, columnNames(i)), cellFont, cellBrushText, rect, centerFormat)
                Next

                y += rowHeight
                _trMvPrintRowIndex += 1

            End While

            y += 10
            e.Graphics.DrawString(
                "إجمالي الدائن: " & Tr_Total_D.Text &
                "    إجمالي المدين: " & Tr_Total_C.Text &
                "    الرصيد: " & Tr_Total_B.Text,
                totalsFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 28),
                rtlFormat
            )

            DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)

        End Using

        e.HasMorePages = False

    End Sub

    Private Function GetTrMvPrintCellText(row As DataGridViewRow, columnName As String) As String

        If Not Tr_MV_MetroGrid.Columns.Contains(columnName) Then Return ""

        Dim value As Object = row.Cells(columnName).Value

        If value Is Nothing OrElse value Is DBNull.Value Then Return ""

        If columnName = "TrisVoid_CL" Then
            Return If(GetBooleanCellValue(row, columnName), "ملغية", "")
        End If

        If columnName = "TRMV_Date_CL" Then
            Dim dateValue As Date
            If Date.TryParse(value.ToString(), dateValue) Then Return dateValue.ToString("yyyy/MM/dd")
        End If

        If columnName = "Tr_Debit_CL" OrElse columnName = "Tr_Credit_CL" OrElse columnName = "Tr_Balance_CL" Then
            Dim numberValue As Decimal
            If Decimal.TryParse(value.ToString(), numberValue) Then
                If (columnName = "Tr_Debit_CL" OrElse columnName = "Tr_Credit_CL") AndAlso numberValue = 0D Then Return ""
                Return numberValue.ToString("N3")
            End If
        End If

        Return value.ToString()

    End Function

    Private Function GetBooleanCellValue(row As DataGridViewRow, columnName As String) As Boolean

        If row Is Nothing OrElse row.DataGridView Is Nothing Then Return False
        If Not row.DataGridView.Columns.Contains(columnName) Then Return False

        Dim value As Object = row.Cells(columnName).Value

        If value Is Nothing OrElse value Is DBNull.Value Then Return False

        If TypeOf value Is Boolean Then Return CBool(value)

        Dim textValue As String = value.ToString().Trim()

        If textValue = "1" Then Return True
        If textValue = "0" Then Return False

        Dim boolValue As Boolean
        If Boolean.TryParse(textValue, boolValue) Then Return boolValue

        Dim intValue As Integer
        If Integer.TryParse(textValue, intValue) Then Return intValue <> 0

        Return False

    End Function

    Private Function GetDecimalCellValue(row As DataGridViewRow, columnName As String, Optional replaceEmptyWithZero As Boolean = False) As Decimal

        If row Is Nothing OrElse row.DataGridView Is Nothing Then Return 0D
        If Not row.DataGridView.Columns.Contains(columnName) Then Return 0D

        Dim value As Object = row.Cells(columnName).Value

        If value Is Nothing OrElse value Is DBNull.Value OrElse String.IsNullOrWhiteSpace(value.ToString()) Then
            If replaceEmptyWithZero Then row.Cells(columnName).Value = 0D
            Return 0D
        End If

        Dim numberValue As Decimal
        If Decimal.TryParse(value.ToString(), numberValue) Then Return numberValue

        If replaceEmptyWithZero Then row.Cells(columnName).Value = 0D
        Return 0D

    End Function


    Private Sub Tr_MV_MetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Tr_MV_MetroGrid.MouseDoubleClick
        Me.Cursor = Cursors.AppStarting
        If Tr_MV_MetroGrid.Rows.Count > 0 Then
            isShowing_Trans = True
            F_Tr_Deposit_Withdraw = New Tr_Deposit_Withdraw
            F_Tr_Deposit_Withdraw.ShowDialog()
            isShowing_Trans = False
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub AGMVMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles AGMVMetroGrid.RowsAdded
        Calc_Balance()
    End Sub

    Private Sub AGMVMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles AGMVMetroGrid.RowsRemoved
        Calc_Balance()
    End Sub

    Private Sub Calc_Balance()
        Dim Sum_D As Decimal = 0D
        Dim Sum_C As Decimal = 0D
        Dim Sum_B As Decimal = 0D
        Dim N_D = 0, N_C = 0

        For i = 0 To AGMVMetroGrid.Rows.Count - 1
            Dim debitValue As Decimal = GetDecimalCellValue(AGMVMetroGrid.Rows(i), "Debit_CL", True)
            Dim creditValue As Decimal = GetDecimalCellValue(AGMVMetroGrid.Rows(i), "Credit_CL", True)

            Sum_D += debitValue
            Sum_C += creditValue

            If debitValue <> 0D Then N_D += 1
            If creditValue <> 0D Then N_C += 1

            Sum_B = GetDecimalCellValue(AGMVMetroGrid.Rows(i), "Balance_CL", True)
        Next

        ' Sum_B = Sum_D - Sum_C


        Total_Debit.Text = Sum_D.ToString("n")
        Total_Credit.Text = Sum_C.ToString("n")
        Total_Balance.Text = Sum_B.ToString("n")

        NUM_CREDIT_TXT.Text = N_C
        NUM_DEBIT_TXT.Text = N_D

        If Sum_B < 0 Then
            Total_Balance.BackColor = colorToApply_DEBIT 'Color.IndianRed
        ElseIf Sum_B > 0 Then
            Total_Balance.BackColor = colorToApply_CREDIT  'Color.LightGreen
        ElseIf Sum_B = 0 Then
            Total_Balance.BackColor = SystemColors.ButtonHighlight
        End If
    End Sub


    Private Sub Tr_MV_MetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles Tr_MV_MetroGrid.RowsAdded
        Tr_Calc_Balance()
    End Sub

    Private Sub Tr_MV_MetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles Tr_MV_MetroGrid.RowsRemoved
        Tr_Calc_Balance()
    End Sub


    Private Sub Tr_Calc_Balance()
        Dim Sum_D As Decimal = 0D
        Dim Sum_C As Decimal = 0D
        Dim Sum_B As Decimal = 0D

        For i = 0 To Tr_MV_MetroGrid.Rows.Count - 1
            Sum_D += GetDecimalCellValue(Tr_MV_MetroGrid.Rows(i), "Tr_Debit_CL", True)
            Sum_C += GetDecimalCellValue(Tr_MV_MetroGrid.Rows(i), "Tr_Credit_CL", True)
        Next

        Sum_B = Sum_C - Sum_D
        Tr_Total_D.Text = Sum_D.ToString("n")
        Tr_Total_C.Text = Sum_C.ToString("n")
        Tr_Total_B.Text = Sum_B.ToString("n")

        If Sum_B > 0 Then
            Tr_Total_B.BackColor = colorToApply_CREDIT   'Color.IndianRed
        ElseIf Sum_B < 0 Then
            Tr_Total_B.BackColor = colorToApply_DEBIT  'Color.LightGreen
        End If
    End Sub


    'Private Sub Total_Fetch_Btn_Click(sender As Object, e As EventArgs)
    '    Me.Cursor = Cursors.AppStarting
    '    Count_Global_Balance()
    '    Me.Cursor = Cursors.Default
    'End Sub

    'Private Sub Count_Global_Balance()
    '    Dim C As New C

    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "Count_Global_Balance"
    '        .Parameters.AddWithValue("@D_F", Total_D_F.Value)
    '        .Parameters.AddWithValue("@D_T", Total_D_T.Value)
    '        .CommandType = CommandType.StoredProcedure

    '        .Parameters.Add("@Total_Recived", SqlDbType.Float, "0")
    '        .Parameters.Add("@Cash_Recived", SqlDbType.Float, "0")
    '        .Parameters.Add("@CHECK_Recived", SqlDbType.Float, "0")
    '        .Parameters.Add("@Total_Expens", SqlDbType.Float, "0")
    '        .Parameters.Add("@Cash_Expens", SqlDbType.Float, "0")
    '        .Parameters.Add("@CHECK_Expens", SqlDbType.Float, "0")

    '        .Parameters("@Total_Recived").Direction = ParameterDirection.Output
    '        .Parameters("@Cash_Recived").Direction = ParameterDirection.Output
    '        .Parameters("@CHECK_Recived").Direction = ParameterDirection.Output

    '        .Parameters("@Total_Expens").Direction = ParameterDirection.Output
    '        .Parameters("@Cash_Expens").Direction = ParameterDirection.Output
    '        .Parameters("@CHECK_Expens").Direction = ParameterDirection.Output

    '    End With
    '    If SQL_SP_EXEC(C.Com) = True Then

    '        With C.Com
    '            Dim N As Double = .Parameters("@Total_Recived").Value
    '            Total_Recived_txt.Text = N.ToString("n")
    '            Cash_Recived_txt.Text = .Parameters("@Cash_Recived").Value
    '            CHECK_Recived_txt.Text = .Parameters("@CHECK_Recived").Value


    '            N = .Parameters("@Total_Expens").Value
    '            Total_Expens_txt.Text = N.ToString("n")
    '            Cash_Expens_txt.Text = .Parameters("@Cash_Expens").Value
    '            CHECK_Expens_txt.Text = .Parameters("@CHECK_Expens").Value


    '            Dim Total As Double = Convert.ToDouble(Total_Recived_txt.Text) - Convert.ToDouble(Total_Expens_txt.Text)

    '            Select Case Total
    '                Case 0
    '                    Total_Balance_txt.ForeColor = Color.Black
    '                Case Is < 0
    '                    Total_Balance_txt.ForeColor = Color.DarkRed
    '                Case Is > 0
    '                    Total_Balance_txt.ForeColor = Color.DarkGreen
    '            End Select

    '            Total_Balance_txt.Text = Total.ToString("n")

    '        End With


    '    End If
    'End Sub

    'Private Sub TotalMonth_cmb_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        Total_D_F = GetFirstDayOfMonth(Total_D_F, TotalMonth_cmb.Text)
    '        Total_D_T = GetLastDayOfMonth(Total_D_T, TotalMonth_cmb.Text)
    '    Catch ex As Exception
    '    End Try
    'End Sub



    Public Sub Fetch_AG_Currency()
        Dim C As New C
        Dim S As String = "Select Cr_ID,Cr_Equal From AGENTS_MENU_V Where AG_ID = '" & AG_ID & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()

                If C.Dr("Cr_ID") > 1 Then
                    Currency_CB.Visible = True
                    Currency_CB.Checked = True
                Else
                    Currency_CB.Visible = False
                    Currency_CB.Checked = False
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub

    Private Sub isVoid_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isVoid_CB.CheckedChanged
        Apply_AG_MV_View()
    End Sub

    Private Sub Apply_AG_MV_View()

        If AG_MV_DT Is Nothing Then Return

        Dim view As New DataView(AG_MV_DT)

        If isVoid_CB.Checked = False AndAlso AG_MV_DT.Columns.Contains("isVoid") Then
            view.RowFilter = "isVoid = false OR isVoid IS NULL"
        End If

        AGMVMetroGrid.DataSource = view

        If AGMVMetroGrid.Columns.Contains("AGisVoid_CL") Then
            AGMVMetroGrid.Columns("AGisVoid_CL").Visible = isVoid_CB.Checked
        End If

        Calc_Balance()

    End Sub

    Private Sub TrisVoid_CB_CheckedChanged(sender As Object, e As EventArgs) Handles TrisVoid_CB.CheckedChanged
        Apply_TR_MV_View()
    End Sub

    Private Sub Apply_TR_MV_View()

        If TR_MV_DT Is Nothing Then Return

        Dim view As New DataView(TR_MV_DT)

        If TrisVoid_CB.Checked = False AndAlso TR_MV_DT.Columns.Contains("isVoid") Then
            view.RowFilter = "isVoid = false OR isVoid IS NULL"
        End If

        Tr_MV_MetroGrid.DataSource = view

        If Tr_MV_MetroGrid.Columns.Contains("TrisVoid_CL") Then
            Tr_MV_MetroGrid.Columns("TrisVoid_CL").Visible = TrisVoid_CB.Checked
        End If

        Tr_Calc_Balance()

    End Sub

    Private Sub IM_SH_txt_Enter(sender As Object, e As EventArgs)
        Set_Ar_Language()
    End Sub

    Private Sub SalariesMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles SalariesMetroGrid.RowsAdded
        Calc_T_Salaries()
    End Sub

    Private Sub SalariesMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles SalariesMetroGrid.RowsRemoved
        Calc_T_Salaries()
    End Sub

    Private Sub Calc_T_Salaries()
        Dim Sum_D As Double = 0
        Dim Sum_C As Double = 0

        For i = 0 To SalariesMetroGrid.Rows.Count - 1
            Sum_D = Sum_D + SalariesMetroGrid.Rows(i).Cells("SumDebit_CL").Value
            Sum_C = Sum_C + SalariesMetroGrid.Rows(i).Cells("Inserted_Salary_CL").Value
        Next
        TotalDebtTextBox.Text = Sum_D.ToString("n")
        TotalPureTextBox.Text = Sum_C.ToString("n")
    End Sub

    Private Sub إيصالقبضToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إيصالقبضToolStripMenuItem.Click
        If Application.OpenForms().OfType(Of Sales).Any Or Application.OpenForms().OfType(Of Sales_Fast).Any Or Application.OpenForms().OfType(Of POS).Any Then
            MsgBox("يجب عليك إغلاق كل شاشات البيع المفتوحة أولا", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        AG_Type = 3
        F_Receipt = New Receipt
        Receipt_Tran_ID = 0
        F_Receipt.ShowDialog()
    End Sub

    Private Sub إيصالصرفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إيصالصرفToolStripMenuItem.Click
        If Application.OpenForms().OfType(Of Pch).Any Then
            MsgBox("يجب عليك إغلاق كل شاشات المشتريات المفتوحة أولا", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        AG_Type = 4
        F_Receipt = New Receipt
        Receipt_Tran_ID = 0
        F_Receipt.ShowDialog()
    End Sub

    Private Sub سحـــبToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles سحـــبToolStripMenuItem.Click
        F_Tr_Deposit_Withdraw = New Tr_Deposit_Withdraw
        TR_Type = 1
        F_Tr_Deposit_Withdraw.ShowDialog()
    End Sub

    Private Sub إيــداعToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إيــداعToolStripMenuItem.Click
        F_Tr_Deposit_Withdraw = New Tr_Deposit_Withdraw
        TR_Type = 2
        F_Tr_Deposit_Withdraw.ShowDialog()
    End Sub

    Private Sub تحويـــلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تحويـــلToolStripMenuItem.Click
        F_Tr_Transfers = New Tr_Transfers
        TR_Type = 3
        F_Tr_Transfers.ShowDialog()
    End Sub

    Private Sub خروجToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles خروجToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub Send_To_Email_btn_Click(sender As Object, e As EventArgs) Handles Send_To_Email_btn.Click
        Type_Of_E_mail = 0
        Me.Cursor = Cursors.AppStarting
        If AGMVMetroGrid.Rows.Count > 0 Then SendingOptions.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Insert_Salary_btn_Click(sender As Object, e As EventArgs) Handles Insert_Salary_btn.Click

        If String.IsNullOrWhiteSpace(YearComboBox.Text) Then
            MsgBox("حدد السنة", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(SalariesMonthComboBox.Text) Then
            MsgBox("حدد الشهر", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If SalariesMetroGrid.Rows.Count > 0 Then
            Beep()
            If MessageBox.Show("هل تريد ترصيد مرتبات عدد " + SalariesMetroGrid.Rows.Count.ToString + " موظف بإجمالي قيمة مرتبات  " + TotalPureTextBox.Text,
                               "تأكيد العملية", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Me.Cursor = Cursors.AppStarting
                Insert_salary()
                Me.Cursor = Cursors.Default
            End If
        End If

    End Sub



    Public Sub Insert_salary()

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "EMP_Insert_Salaries"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@MONTH", SalariesMonthComboBox.Text)
            .Parameters.AddWithValue("@YEAR", YearComboBox.Text)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters.AddWithValue("@Salary_Date", Salary_Date.Value)
        End With
        If SQL_SP_EXEC(C.Com) Then
            MsgBox("تم الترصيد", MsgBoxStyle.Information, "")
            Network_Edit_Tracker_insert("ترصيد مرتبات عدد " & SalariesMetroGrid.Rows.Count.ToString & " موظف بإجمالي قيمة مرتبات  " & TotalPureTextBox.Text, 0, 5, 1)
        End If


    End Sub

    Private Sub مكافئاتوخصوماتللموظفينToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles مكافئاتوخصوماتللموظفينToolStripMenuItem.Click
        Emp_OtherValues.ShowDialog()
    End Sub


    Private Sub Coloring()
        For i = 0 To DebtMetroGrid.Rows.Count - 1



            Select Case DebtMetroGrid.Rows(i).Cells("AG_BALANCE_T_CL").Value
                Case Is > 0
                    DebtMetroGrid.Rows(i).Cells("AG_BALANCE_T_CL").Style.BackColor = colorToApply_DEBIT
                Case Is < 0
                    DebtMetroGrid.Rows(i).Cells("AG_BALANCE_T_CL").Style.BackColor = colorToApply_CREDIT
            End Select

        Next


        'For i = 0 To DebtMetroGrid.Rows.Count - 1

        '    Select Case DebtMetroGrid.Rows(i).Cells("AG_BALANCE_T_CL").Value
        '        Case Is < 0
        '            DebtMetroGrid.Rows(i).Cells("AG_BALANCE_T_CL").Style.BackColor = Color.LightGreen
        '        Case Is > 0
        '            DebtMetroGrid.Rows(i).Cells("AG_BALANCE_T_CL").Style.BackColor = Color.IndianRed
        '    End Select

        'Next
    End Sub


    Public Sub Select_Debt_Till_Date(Optional sqlCon As SqlClient.SqlConnection = Nothing)

        If sqlCon IsNot Nothing Then
            Using sqlComm As New SqlClient.SqlCommand("[Select_Debt_Till_Date]", sqlCon)
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.AddWithValue("@Date", DATE_Before.Value)

                Dim closeConnection As Boolean = sqlCon.State <> ConnectionState.Open

                Try
                    If closeConnection Then sqlCon.Open()
                    sqlComm.ExecuteNonQuery()
                Finally
                    If closeConnection AndAlso sqlCon.State = ConnectionState.Open Then sqlCon.Close()
                End Try
            End Using

            Return
        End If

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[Select_Debt_Till_Date]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Date", DATE_Before.Value)
        End With
        SQL_SP_EXEC(C.Com)
    End Sub


    Private Sub Calc_T_Balances()
        Dim Sum_D As Double = 0
        Dim Sum_C As Double = 0
        Dim Sum_B As Double = 0

        For i = 0 To DebtMetroGrid.Rows.Count - 1
            Sum_D = Sum_D + DebtMetroGrid.Rows(i).Cells("B_T_Debit_CL").Value
            Sum_C = Sum_C + DebtMetroGrid.Rows(i).Cells("B_T_Credit_CL").Value
            Sum_B = Sum_B + DebtMetroGrid.Rows(i).Cells("AG_BALANCE_T_CL").Value
        Next
        T_DebitTextBox.Text = Sum_D.ToString("n")
        T_CreditTextBox.Text = Sum_C.ToString("n")
        T_BalanceTextBox.Text = Sum_B.ToString("n")

        If Sum_B < 0 Then
            T_BalanceTextBox.BackColor = colorToApply_CREDIT 'Color.IndianRed
        ElseIf Sum_B > 0 Then
            T_BalanceTextBox.BackColor = colorToApply_DEBIT  'Color.LightGreen
        Else
            T_BalanceTextBox.BackColor = SystemColors.Window
        End If

        '-------------------------------------------------------------
        'If Sum_B > 0 Then
        '    T_BalanceTextBox.BackColor = Color.IndianRed
        'ElseIf Sum_B < 0 Then
        '    T_BalanceTextBox.BackColor = Color.LightGreen
        'Else
        '    T_BalanceTextBox.BackColor = SystemColors.Window
        'End If
        '-------------------------------------------------------------

    End Sub


    Private Sub AG_BALANCE_Sch_Btn_Click(sender As Object, e As EventArgs) Handles AG_BALANCE_Sch_Btn.Click
        Get_AgentsBalances()
    End Sub

    Private Sub Debit_WithDate_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Debit_WithDate_CB.CheckedChanged
        DATE_Before.Enabled = Debit_WithDate_CB.Checked
    End Sub

    Private Sub AG_Type_CB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AG_Type_CB.SelectedIndexChanged
        'If AG_Type_CB.SelectedIndex = 0 Then
        '    For i = 1 To AG_Type_CB.Items.Count - 1
        '        AG_Type_CB.SelectedIndex = i
        '        AG_Type_CB.SetItemChecked(0, True)
        '    Next
        'End If
    End Sub

    Private Sub Type_Cm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Type_Cm.SelectedIndexChanged
        If Type_Cm.SelectedIndex = 0 Then
            Search_Panel.Enabled = True
            AG_Type_CB.Enabled = True
        ElseIf Type_Cm.SelectedIndex = 1 Then
            Search_Panel.Enabled = False
            AG_Type_CB.Enabled = False
        End If
    End Sub

    Private Sub العهدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles العهدToolStripMenuItem.Click
        F_Custody = New Custody
        F_Custody.ShowDialog()
    End Sub


    Private Sub DebtMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DebtMetroGrid.MouseDoubleClick
        Me.Cursor = Cursors.AppStarting
        If DebtMetroGrid.Rows.Count > 0 Then

            Select Case Type_Cm.SelectedIndex
                Case 0
                    AG_Cm.Set_IM_By_ID(DebtMetroGrid.CurrentRow.Cells("ID_CL").Value)
                    AllUsersCheckBox.Checked = True
                    AllRecieptsCheckBox.Checked = True
                    AllTimeCheckBox.Checked = True
                    AllAgentsCheckBox.Checked = False
                    MVSearchButton_Click(sender, e)
                    'MetroTabControl1.SelectedTab = MetroTabPage1
                Case 1
                    Treasury_ComboBox.SelectedValue = DebtMetroGrid.CurrentRow.Cells("ID_CL").Value
                    TrAllTypeCheckBox.Checked = True
                    TrAllUsersCheckBox.Checked = True
                    TrAllTimeCheckBox.Checked = True
                    AllTrCheckBox.Checked = False
                    Tr_SearchButton_Click(sender, e)
                    'MetroTabControl1.SelectedTab = MetroTabPage2
            End Select

        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged
        Dim Dv As DataView
        Dv = AG_B_DT.AsDataView
        Dv.RowFilter = "Agent_name LIKE '%" + sender.Text + "%'"
        DebtMetroGrid.DataSource = Dv

    End Sub

    Private Sub Balances_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized

        For i = 0 To AGMVMetroGrid.Columns.Count - 1
            AGMVMetroGrid.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i


        'If FormType <> 1 Or FormType <> 2 Then
        For i = 0 To Tr_MV_MetroGrid.Columns.Count - 1
            Tr_MV_MetroGrid.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i
        fetch_Agents_Types()
        MenuStrip1.Cursor = Cursors.Hand
        Me.Cursor = Cursors.AppStarting
        Load_Data()
        Me.Cursor = Cursors.Default
        'End If

        'MetroTabControl1.SelectedTab = MetroTabPage1
        Coloring()

        Try
            AG_Type_CB.SetItemChecked(0, True)
        Catch ex As Exception
        End Try

        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن حساب")
    End Sub

End Class
