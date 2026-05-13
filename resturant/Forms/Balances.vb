Imports CrystalDecisions.Shared

Public Class Balances

    Dim rs As New Resizer
    Private AG_MV_DT As New DataTable
    Private TR_MV_DT As New DataTable
    Private _agMvPrintRowIndex As Integer = 0
    Private _trMvPrintRowIndex As Integer = 0
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
        ThemeManager.ApplyThemeToForm(Me)
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
    End Sub


    Private Sub ALL_BALANCES_Grid_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles ALL_BALANCES_Grid.FilterStringChanged
        DataB.Filter = ALL_BALANCES_Grid.FilterString

    End Sub

    Private Sub ALL_BALANCES_Grid_SortStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.SortEventArgs) Handles ALL_BALANCES_Grid.SortStringChanged
        DataB.Sort = ALL_BALANCES_Grid.SortString

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
    Private Sub SELECT_ALL_BALANCES_V()


        DataB.Dispose()
        DataB = New BindingSource
        ALL_BALANCES_Grid.DataSource = Nothing

        Dim C = New C
        Dim Tr_Query As String = " SELECT  ROW_NUMBER() OVER(ORDER BY B_NAME ASC) AS [ت],[B_NAME] AS [الحساب],[T_CREDIT] AS [مدين-المجاميع],[T_DEBIT] AS [دائن-المجاميع],[T_CREDIT_1] AS [مدين-الأرصدة],[T_DEBIT_1] AS [دائن-الأرصدة] FROM [dbo].[ALL_BALANCES_V]  "
        C.Da = New SqlClient.SqlDataAdapter(Tr_Query, C.Con)
        C.Da.Fill(C.Dt)

        DataB.DataSource = C.Dt
        ALL_BALANCES_Grid.DataSource = DataB

        ALL_BALANCES_Grid.DataSource = C.Dt
        ALL_BALANCES_Grid.Columns(2).Tag = 1
        ALL_BALANCES_Grid.Columns(3).Tag = 1
        ALL_BALANCES_Grid.Columns(4).Tag = 1
        ALL_BALANCES_Grid.Columns(5).Tag = 1

        ALL_BALANCES_Grid.Columns(2).DefaultCellStyle.Format = "N3"
        ALL_BALANCES_Grid.Columns(3).DefaultCellStyle.Format = "N3"
        ALL_BALANCES_Grid.Columns(4).DefaultCellStyle.Format = "N3"
        ALL_BALANCES_Grid.Columns(5).DefaultCellStyle.Format = "N3"

        '' Calculate sums for each numeric column
        'Dim totalCredit As Decimal = 0
        'Dim totalDebit As Decimal = 0
        'Dim totalCredit1 As Decimal = 0
        'Dim totalDebit1 As Decimal = 0

        'For Each row As DataRow In C.Dt.Rows
        '    If Not IsDBNull(row("T_CREDIT")) Then totalCredit += Convert.ToDecimal(row("T_CREDIT"))
        '    If Not IsDBNull(row("T_DEBIT")) Then totalDebit += Convert.ToDecimal(row("T_DEBIT"))
        '    If Not IsDBNull(row("T_CREDIT_1")) Then totalCredit1 += Convert.ToDecimal(row("T_CREDIT_1"))
        '    If Not IsDBNull(row("T_DEBIT_1")) Then totalDebit1 += Convert.ToDecimal(row("T_DEBIT_1"))
        'Next

        '' Add a new row for the summary
        'Dim summaryRow As DataRow = C.Dt.NewRow()
        'summaryRow("IDX") = DBNull.Value ' Leave IDX blank for summary row
        'summaryRow("B_NAME") = "Total"
        'summaryRow("T_CREDIT") = totalCredit
        'summaryRow("T_DEBIT") = totalDebit
        'summaryRow("T_CREDIT_1") = totalCredit1
        'summaryRow("T_DEBIT_1") = totalDebit1

        '' Add the summary row to the DataTable
        'C.Dt.Rows.Add(summaryRow)


        'ALL_BALANCES_Grid.DataSource = C.Dt

        '' Make the summary row bold in the DataGridView
        'Dim summaryRowIndex As Integer = ALL_BALANCES_Grid.Rows.Count - 1
        'For Each cell As DataGridViewCell In ALL_BALANCES_Grid.Rows(summaryRowIndex).Cells
        '    cell.Style.Font = New Font(ALL_BALANCES_Grid.Font, FontStyle.Bold)
        'Next

        '-----------------------------------------------------------------------------------------------------------------------------

        C = New C
        Tr_Query = " SELECT '--' IDX,'--' AS [B_NAME],SUM([T_CREDIT]) AS S1,SUM([T_DEBIT]) AS S2,SUM([T_CREDIT_1]) AS S3,SUM([T_DEBIT_1]) AS S4 FROM [dbo].[ALL_BALANCES_V]  "
        C.Da = New SqlClient.SqlDataAdapter(Tr_Query, C.Con)
        C.Da.Fill(C.Dt)
        ALL_B_TOTAL_Grid.DataSource = C.Dt
        ALL_B_TOTAL_Grid.ClearSelection()

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

    'Private Sub Fetch_Agents_Type()
    '    Dim C As New C
    '    Try
    '        Dim sql As String = "Select id,Type_Name from Agents_Types WHERE Visible = 1 Order By ID ASC"
    '        C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
    '        C.Da.Fill(C.Dt)
    '        AG_Type_cm.DataSource = C.Dt
    '        AG_Type_cm.DisplayMember = "Type_Name"
    '        AG_Type_cm.ValueMember = "id"
    '        AG_Type_cm.SelectedValue = 1
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Public Sub Load_Data()
        Dim C As New C
        Dim sql As String

        Try
            sql = " select id,Type_Name from AgentBalance_Type WHERE ID NOT IN(14,15) AND Visible = 1 Order By RankNum ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            ReceiptTypeComboBox.DataSource = C.Dt
            ReceiptTypeComboBox.DisplayMember = "Type_Name"
            ReceiptTypeComboBox.ValueMember = "id"
            ReceiptTypeComboBox.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            sql = "Select User_ID,UserName from Users Order By User_ID ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            UsersComboBox.DataSource = C.Dt
            UsersComboBox.DisplayMember = "UserName"
            UsersComboBox.ValueMember = "User_ID"
            UsersComboBox.SelectedIndex = 0

            TrUsersComboBox.DataSource = C.Dt
            TrUsersComboBox.DisplayMember = "UserName"
            TrUsersComboBox.ValueMember = "User_ID"
            TrUsersComboBox.SelectedIndex = 0

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            sql = "Select Tr_ID,Tr_Name FROM TR_MENU_V "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)

            Treasury_ComboBox.DataSource = C.Dt
            Treasury_ComboBox.DisplayMember = "Tr_Name"
            Treasury_ComboBox.ValueMember = "Tr_ID"
            Treasury_ComboBox.SelectedIndex = 0

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            sql = "Select id,Type_Name from TreasuryBalance_Type "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)

            TrTypeComboBox.DataSource = C.Dt
            TrTypeComboBox.DisplayMember = "Type_Name"
            TrTypeComboBox.ValueMember = "id"
            TrTypeComboBox.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            sql = "select Distinct top 10 Year from Agents_Balance_MV WHERE YEAR IS NOT NULL ORDER BY YEAR DESC "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            YearComboBox.DataSource = C.Dt
            YearComboBox.DisplayMember = "Year"
            If C.Dt.Rows.Count > 0 Then YearComboBox.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        SalariesMonthComboBox.Text = Date.Now.Month

        Get_AgentsBalances()

    End Sub


    Dim AG_B_DT As New DataTable
    Private Sub Get_AgentsBalances()


        Rpt_Name = ""
        Dim C = New C

        Dim Main_Query As String = ""
        Dim Tr_Query As String = " SELECT [Tr_ID] AS ID,[Tr_Name] AS 'Agent_name',[T_Debit] , [T_Credit] , ISNULL(T_Balance,0) AS T_Balance FROM [dbo].[TR_MENU_V]"
        'Dim Union_Str As String = " UNION "
        '------------------------------------------------------------------------

        If Type_Cm.SelectedIndex = 0 Then

            If Debit_WithDate_CB.Checked = True Then
                Select_Debt_Till_Date()
                Main_Query = " select [Ag_ID] AS ID,[Ag_name] AS Agent_name,'0' AS  [T_Debit],'0' AS  [T_Credit],[T_Balance_TMP] AS T_Balance from AGENTS_MENU_V "
            Else
                Main_Query = " select [Ag_ID] AS ID,[Ag_name] AS Agent_name,[T_Debit],[T_Credit],[T_Balance] from AGENTS_MENU_V "
            End If


            Dim middle As String = " where 1=1 "
            Dim last As String = " order by Ag_name ASC "
            middle += " AND Type_ID IN (-1"


            For i As Integer = 0 To AG_Type_CB.Items.Count - 1
                AG_Type_CB.SelectedIndex = i
                If AG_Type_CB.GetItemCheckState(i) = CheckState.Checked Then
                    If i = 0 Then
                        middle += Select_ALL(AG_Type_CB)
                        Rpt_Name += "---الكل---"
                        Exit For
                    End If
                    middle += "," & AG_Type_CB.SelectedValue

                    Select Case i
                        Case 1
                            Rpt_Name += ",حسابات عامة"
                        Case 2
                            Rpt_Name += ",زبائن"
                        Case 3
                            Rpt_Name += ",موردين"
                        Case 4
                            Rpt_Name += ",موظفين"
                        Case 5
                            Rpt_Name += ",الأصول الثابته"
                    End Select

                End If
            Next

            middle += ")"

            If AlldebitsRadioButton.Checked = True Then
                middle = middle & " AND T_Balance < 0 "
            ElseIf AllCerditRadioButton.Checked = True Then
                middle = middle & " AND T_Balance > 0 "
            End If

            Main_Query = Main_Query & middle & last

        Else
            Main_Query = Tr_Query

        End If
        AG_B_DT.Clear()
        C.Da = New SqlClient.SqlDataAdapter(Main_Query, C.Con)
        C.Da.Fill(AG_B_DT)
        DebtMetroGrid.DataSource = AG_B_DT

        Calc_T_Balances()
        Coloring()


    End Sub
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
        rs.ResizeAllControls(Me)
    End Sub

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
        SetAgMvSearchBusy(True)

        Try
            PrepareAgMvReportTitles()

            If Search_AG_MV() Then
                Apply_AG_MV_View()
            End If

        Finally
            SetAgMvSearchBusy(False)
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

        Me.Cursor = If(isBusy, Cursors.AppStarting, Cursors.Default)
        MVSearchButton.Enabled = Not isBusy
        MVPrintButton.Enabled = Not isBusy
        MVSearchButton.Text = If(isBusy, "جاري البحث...", "بحــــــث")
        MVSearchButton.BackColor = If(isBusy, Color.FromArgb(255, 243, 205), Color.WhiteSmoke)
        AGMVMetroGrid.Cursor = If(isBusy, Cursors.AppStarting, Cursors.Hand)

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


    Private Sub SalarySearchButton_Click(sender As Object, e As EventArgs) Handles SalarySearchButton.Click
        If String.IsNullOrWhiteSpace(SalariesMonthComboBox.Text) Then
            MsgBox("حدد الشهر", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(YearComboBox.Text) Then
            MsgBox("حدد السنة", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
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
        SalarySearchButton_Click(sender, e)
        Print_Salaries()
    End Sub

    Public Sub Print_Salaries()

        Try
            Dim p As New print
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & "\reports\EMP_Salaries.rpt")

            pp.CrTables = pp.rp.Database.Tables
            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next


            With pp
                .rp.SetParameterValue(0, USER_NAME)
                .rp.SetParameterValue(1, " لشهــر " + SalariesMonthComboBox.Text + " سنــة " + YearComboBox.Text)
                .rp.SetParameterValue(2, TotalDebtTextBox.Text)
                .rp.SetParameterValue(3, TotalPureTextBox.Text)
                .rp.SetParameterValue(4, F_MainForm.Serv_Label.Text)

                p.CrystalReportViewer1.ReportSource = pp.rp
                p.ShowDialog()

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

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
        If Debit_WithDate_CB.Checked = True Then Select_Debt_Till_Date()

        AG_Report_print_Reset()
        AG_Report_Copy()
        Print_AG_Balances()
        AG_Report_print_Reset()
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

        Try
            Dim p As New print
            Dim pp As New ReportConnection


            If Debit_WithDate_CB.Checked = False Then
                pp.rp.Load(Application.StartupPath & "\reports\AG_Balances.rpt")
            Else
                pp.rp.Load(Application.StartupPath & "\reports\AG_Balances_With_Date.rpt")
            End If


            pp.CrTables = pp.rp.Database.Tables
            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next


            With pp
                .rp.SetParameterValue(0, USER_NAME)

                If Type_Cm.SelectedIndex = 0 Then

                    If AllBalancesRadioButton.Checked = True Then
                        .rp.SetParameterValue(1, "حسب : الـكـل")
                    ElseIf AlldebitsRadioButton.Checked = True Then
                        .rp.SetParameterValue(1, "حسب : المدينيــن ")
                    Else
                        .rp.SetParameterValue(1, "حسب : الدائنيــن")
                    End If

                    If Debit_WithDate_CB.Checked = False Then
                        .rp.SetParameterValue(6, " الديون حتى تاريخ : " & " تاريخ الطباعة ")
                    Else
                        .rp.SetParameterValue(6, " الديون :  حتى تاريخ " & DATE_Before.Text)
                    End If

                Else

                    .rp.SetParameterValue(1, Type_Cm.Text)
                    .rp.SetParameterValue(6, " ")

                End If


                .rp.SetParameterValue(2, T_DebitTextBox.Text)
                .rp.SetParameterValue(3, T_CreditTextBox.Text)
                .rp.SetParameterValue(4, F_MainForm.Serv_Label.Text)
                .rp.SetParameterValue(5, T_BalanceTextBox.Text)


                '.rp.SetParameterValue(6, Balance_Type)
                .rp.SetParameterValue(7, Rpt_Name)

                '.rp.SetParameterValue(9, " نوع الحساب : " & Rpt_Name)
                ''Else
                '.rp.SetParameterValue(2, F_MainForm.Serv_Desc_lb.Text)
                '.rp.SetParameterValue(3, T_BalanceTextBox.Text)
                '.rp.SetParameterValue(4, Balance_Type)
                '.rp.SetParameterValue(5, Rpt_Name)

                '.rp.SetParameterValue(7, " نوع الحساب : " & Rpt_Name)

                'End If

                '.rp.PrintOptions.PrinterName = My_Settings.Default_Printer
                '.rp.PrintToPrinter(1, False, 0, 0)
                '.rp.Dispose()


                p.CrystalReportViewer1.ReportSource = pp.rp
                p.ShowDialog()

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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

        Using titleFont As New Font("Segoe UI", 13.0!, FontStyle.Bold),
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
                "التاريخ",
                "المعاملة",
                "ر.إشاري",
                "العنوان",
                "دائن",
                "مدين",
                "الرصيد",
                "المدخل"
            }

            Dim weights As Integer() = {90, 100, 75, 245, 90, 90, 95, 90}
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

            While _agMvPrintRowIndex < AGMVMetroGrid.Rows.Count

                If y + rowHeight > bounds.Bottom - 42 Then
                    e.HasMorePages = True
                    Return
                End If

                Dim row As DataGridViewRow = AGMVMetroGrid.Rows(_agMvPrintRowIndex)
                x = bounds.Right

                For i As Integer = 0 To columnNames.Length - 1
                    x -= widths(i)
                    Dim rect As New Rectangle(x, y, widths(i), rowHeight)

                    If _agMvPrintRowIndex Mod 2 = 0 Then
                        e.Graphics.FillRectangle(Brushes.White, rect)
                    Else
                        Using altBrush As New SolidBrush(Color.FromArgb(248, 250, 252))
                            e.Graphics.FillRectangle(altBrush, rect)
                        End Using
                    End If

                    e.Graphics.DrawRectangle(Pens.LightGray, rect)
                    e.Graphics.DrawString(GetAgMvPrintCellText(row, columnNames(i)), rowFont, Brushes.Black, rect, centerFormat)
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

        End Using

        e.HasMorePages = False

    End Sub

    Private Function GetAgMvPrintCellText(row As DataGridViewRow, columnName As String) As String

        If Not AGMVMetroGrid.Columns.Contains(columnName) Then Return ""

        Dim value As Object = row.Cells(columnName).Value

        If value Is Nothing OrElse value Is DBNull.Value Then Return ""

        If columnName = "Date_CL" Then
            Dim dateValue As Date
            If Date.TryParse(value.ToString(), dateValue) Then Return dateValue.ToString("yyyy/MM/dd")
        End If

        If columnName = "Debit_CL" OrElse columnName = "Credit_CL" OrElse columnName = "Balance_CL" Then
            Dim numberValue As Decimal
            If Decimal.TryParse(value.ToString(), numberValue) Then Return numberValue.ToString("N3")
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

        Using titleFont As New Font("Segoe UI", 13.0!, FontStyle.Bold),
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
                "التاريخ",
                "المعاملة",
                "العنوان",
                "الحساب",
                "مدين",
                "دائن",
                "الرصيد",
                "المدخل"
            }

            Dim weights As Integer() = {90, 100, 260, 130, 90, 90, 95, 90}
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

            While _trMvPrintRowIndex < Tr_MV_MetroGrid.Rows.Count

                If y + rowHeight > bounds.Bottom - 42 Then
                    e.HasMorePages = True
                    Return
                End If

                Dim row As DataGridViewRow = Tr_MV_MetroGrid.Rows(_trMvPrintRowIndex)
                x = bounds.Right

                For i As Integer = 0 To columnNames.Length - 1
                    x -= widths(i)
                    Dim rect As New Rectangle(x, y, widths(i), rowHeight)

                    If _trMvPrintRowIndex Mod 2 = 0 Then
                        e.Graphics.FillRectangle(Brushes.White, rect)
                    Else
                        Using altBrush As New SolidBrush(Color.FromArgb(248, 250, 252))
                            e.Graphics.FillRectangle(altBrush, rect)
                        End Using
                    End If

                    e.Graphics.DrawRectangle(Pens.LightGray, rect)
                    e.Graphics.DrawString(GetTrMvPrintCellText(row, columnNames(i)), rowFont, Brushes.Black, rect, centerFormat)
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

        End Using

        e.HasMorePages = False

    End Sub

    Private Function GetTrMvPrintCellText(row As DataGridViewRow, columnName As String) As String

        If Not Tr_MV_MetroGrid.Columns.Contains(columnName) Then Return ""

        Dim value As Object = row.Cells(columnName).Value

        If value Is Nothing OrElse value Is DBNull.Value Then Return ""

        If columnName = "TRMV_Date_CL" Then
            Dim dateValue As Date
            If Date.TryParse(value.ToString(), dateValue) Then Return dateValue.ToString("yyyy/MM/dd")
        End If

        If columnName = "Tr_Debit_CL" OrElse columnName = "Tr_Credit_CL" OrElse columnName = "Tr_Balance_CL" Then
            Dim numberValue As Decimal
            If Decimal.TryParse(value.ToString(), numberValue) Then Return numberValue.ToString("N3")
        End If

        Return value.ToString()

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
        Dim Sum_D As Double = 0
        Dim Sum_C As Double = 0
        Dim Sum_B As Double = 0
        Dim N_D = 0, N_C = 0

        For i = 0 To AGMVMetroGrid.Rows.Count - 1
            Sum_D = Sum_D + AGMVMetroGrid.Rows(i).Cells("Debit_CL").Value
            Sum_C = Sum_C + AGMVMetroGrid.Rows(i).Cells("Credit_CL").Value
            If AGMVMetroGrid.Rows(i).Cells("Debit_CL").Value <> 0 Then N_D += 1
            If AGMVMetroGrid.Rows(i).Cells("Credit_CL").Value <> 0 Then N_C += 1

            Sum_B = AGMVMetroGrid.Rows(i).Cells("Balance_CL").Value
        Next

        ' Sum_B = Sum_D - Sum_C


        Total_Debit.Text = Sum_D.ToString("n")
        Total_Credit.Text = Sum_C.ToString("n")
        Total_Balance.Text = Sum_B.ToString("n")

        NUM_CREDIT_TXT.Text = N_C
        NUM_DEBIT_TXT.Text = N_D

        If Sum_B > 0 Then
            Total_Balance.BackColor = colorToApply_DEBIT 'Color.IndianRed
        ElseIf Sum_B < 0 Then
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
        Dim Sum_D As Double = 0
        Dim Sum_C As Double = 0
        Dim Sum_B As Double = 0

        For i = 0 To Tr_MV_MetroGrid.Rows.Count - 1
            Sum_D = Sum_D + Tr_MV_MetroGrid.Rows(i).Cells("Tr_Debit_CL").Value
            Sum_C = Sum_C + Tr_MV_MetroGrid.Rows(i).Cells("Tr_Credit_CL").Value
        Next

        Sum_B = Sum_C - Sum_D
        Tr_Total_D.Text = Sum_D.ToString("n")
        Tr_Total_C.Text = Sum_C.ToString("n")
        Tr_Total_B.Text = Sum_B.ToString("n")

        If Sum_B < 0 Then
            Tr_Total_B.BackColor = colorToApply_CREDIT   'Color.IndianRed
        ElseIf Sum_B > 0 Then
            Tr_Total_B.BackColor = colorToApply_DEBIT  'Color.LightGreen
        End If
    End Sub


    Private Sub Total_Fetch_Btn_Click(sender As Object, e As EventArgs) Handles Total_Fetch_Btn.Click
        Me.Cursor = Cursors.AppStarting
        Count_Global_Balance()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Count_Global_Balance()
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "Count_Global_Balance"
            .Parameters.AddWithValue("@D_F", Total_D_F.Value)
            .Parameters.AddWithValue("@D_T", Total_D_T.Value)
            .CommandType = CommandType.StoredProcedure

            .Parameters.Add("@Total_Recived", SqlDbType.Float, "0")
            .Parameters.Add("@Cash_Recived", SqlDbType.Float, "0")
            .Parameters.Add("@CHECK_Recived", SqlDbType.Float, "0")
            .Parameters.Add("@Total_Expens", SqlDbType.Float, "0")
            .Parameters.Add("@Cash_Expens", SqlDbType.Float, "0")
            .Parameters.Add("@CHECK_Expens", SqlDbType.Float, "0")

            .Parameters("@Total_Recived").Direction = ParameterDirection.Output
            .Parameters("@Cash_Recived").Direction = ParameterDirection.Output
            .Parameters("@CHECK_Recived").Direction = ParameterDirection.Output

            .Parameters("@Total_Expens").Direction = ParameterDirection.Output
            .Parameters("@Cash_Expens").Direction = ParameterDirection.Output
            .Parameters("@CHECK_Expens").Direction = ParameterDirection.Output

        End With
        If SQL_SP_EXEC(C.Com) = True Then

            With C.Com
                Dim N As Double = .Parameters("@Total_Recived").Value
                Total_Recived_txt.Text = N.ToString("n")
                Cash_Recived_txt.Text = .Parameters("@Cash_Recived").Value
                CHECK_Recived_txt.Text = .Parameters("@CHECK_Recived").Value


                N = .Parameters("@Total_Expens").Value
                Total_Expens_txt.Text = N.ToString("n")
                Cash_Expens_txt.Text = .Parameters("@Cash_Expens").Value
                CHECK_Expens_txt.Text = .Parameters("@CHECK_Expens").Value


                Dim Total As Double = Convert.ToDouble(Total_Recived_txt.Text) - Convert.ToDouble(Total_Expens_txt.Text)

                Select Case Total
                    Case 0
                        Total_Balance_txt.ForeColor = Color.Black
                    Case Is < 0
                        Total_Balance_txt.ForeColor = Color.DarkRed
                    Case Is > 0
                        Total_Balance_txt.ForeColor = Color.DarkGreen
                End Select

                Total_Balance_txt.Text = Total.ToString("n")

            End With


        End If
    End Sub

    Private Sub TotalMonth_cmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TotalMonth_cmb.SelectedIndexChanged
        Try
            Total_D_F = GetFirstDayOfMonth(Total_D_F, TotalMonth_cmb.Text)
            Total_D_T = GetLastDayOfMonth(Total_D_T, TotalMonth_cmb.Text)
        Catch ex As Exception
        End Try
    End Sub



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


    Public Sub Select_Debt_Till_Date()

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

    Private Sub Refrech_Btn_Click(sender As Object, e As EventArgs) Handles Refrech_Btn.Click
        SELECT_ALL_BALANCES_V()
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
                    MetroTabControl1.SelectedTab = MetroTabPage1
                Case 1
                    Treasury_ComboBox.SelectedValue = DebtMetroGrid.CurrentRow.Cells("ID_CL").Value
                    TrAllTypeCheckBox.Checked = True
                    TrAllUsersCheckBox.Checked = True
                    TrAllTimeCheckBox.Checked = True
                    AllTrCheckBox.Checked = False
                    Tr_SearchButton_Click(sender, e)
                    MetroTabControl1.SelectedTab = MetroTabPage2
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
        rs.FindAllControls(Me)
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

        MetroTabControl1.SelectedTab = MetroTabPage1
        Coloring()

        Try
            AG_Type_CB.SetItemChecked(0, True)
        Catch ex As Exception
        End Try

        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن حساب")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim p As New Print_PDF
        p.PRINT_PDF(ALL_BALANCES_Grid, 1, "ميزان المراجعة")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        EXCEL_EXPORT(ALL_BALANCES_Grid)
    End Sub


End Class
