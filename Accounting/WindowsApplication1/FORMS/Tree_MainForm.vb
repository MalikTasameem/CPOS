Imports DevExpress.Utils

Public Class Tree_MainForm
    Private Const TrialEntriesLimit As Integer = 200
    Private TrialDataReset_Btn As Button
    Private IsTrialLimitExceeded As Boolean = False
    Public Property OpenedFromCPOS As Boolean = False

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        F_ACC_B = New ACC_B
        F_ACC_B.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        F_Tree = New Tree
        F_Tree.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        BALANCES_REVIEW.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim F As New Normal_Form
        F.Form_Name = "COST_CENTER"
        F.Form_Name_Arabic = "مراكز التكلفة"
        F.F_ID = "COST_ID"
        F.F_Name = "COST_NAME"
        F.F_DETAILS = "COST_CENTER"

        F.Checked_Table = "ACC_BALANCE"
        F.Checked_Table_ID = "COST_ID"
        F.Show()
    End Sub


    Private StatsRowPanel As TableLayoutPanel
    Private lblAccountsCount As New Label
    Private lblTodayEntries As New Label
    Private lblTotalBalance As New Label
    Private lblFiscalYear As New Label


    Private Sub Tree_MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        CHECK_YAER()

        If String.IsNullOrWhiteSpace(MY_Settings.SALES_DB) Then
            Watcher_Timer.Enabled = False
            Watcher_Timer.Stop()
        End If

        Me.BackColor = Color.FromArgb(246, 247, 251)
        BuildDashboard_FromDesigner()


        StatsRowPanel = CreateStatsRow()
        StatsRowPanel.Dock = DockStyle.Top
        stats_Panel.Controls.Add(StatsRowPanel)
        StatsRowPanel.BringToFront()
        ToolStrip1.BringToFront()

        'ToggleTheme(Me)

        ' ✅ هنا فقط
        AddStatsRow_Runtime()

        ToggleTheme(Me)
        RefreshButtonColors(Me)


        GET_summary()


        Load_Form()

    End Sub


    Public Sub Load_Form()

        'Setting_GroupBox.Enabled = is_Home_Mange_Printers


        Activation.isFor_Check_Sys_Active = True
        'Dim hiddenHandle As IntPtr = Activation.Handle
        Activation.ShowDialog()
        Activation.isFor_Check_Sys_Active = False

        If Activation.isActive = False Then
            IsTrialLimitExceeded = Count_AG_BalanceRows()
            ActiveLinkLa.Visible = True
        Else
            IsTrialLimitExceeded = False
            ActiveLinkLa.Visible = False
            Bill_Num_LB.Visible = False
        End If

        ApplyTrialLimitState()
        LayoutActivationLabels()
        ' =====================================================================



    End Sub


    Private Function Count_AG_BalanceRows() As Boolean
        Dim rowsCount As Integer = 0

        Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlClient.SqlCommand("SELECT COUNT(T_ID) AS Num FROM dbo.ACC_BALANCE_MASTER", cn)
                cn.Open()
                rowsCount = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using

        If rowsCount >= TrialEntriesLimit Then
            Bill_Num_LB.Visible = True
            Bill_Num_LB.Text = "انتهت صلاحية النسخة التجريبية: " & rowsCount.ToString() & " قيد من أصل " & TrialEntriesLimit.ToString()
            Return True
        End If

        Bill_Num_LB.Visible = True
        Bill_Num_LB.Text = " لديك " & (TrialEntriesLimit - rowsCount).ToString() & " قيد من أصل " & TrialEntriesLimit.ToString()
        LayoutActivationLabels()
        Return False
    End Function

    Private Sub ApplyTrialLimitState()
        EnsureTrialDataResetButton()

        If IsTrialLimitExceeded Then
            MsgBox("انتهت صلاحية النسخة التجريبية للنظام. يرجى تفعيل النظام أو تفريغ قيود التجربة للبدء من جديد.", MsgBoxStyle.Exclamation)
            SetMainActionsEnabled(False)
            TrialDataReset_Btn.Visible = True
            TrialDataReset_Btn.Enabled = True
            TrialDataReset_Btn.BringToFront()
        Else
            SetMainActionsEnabled(True)
            If TrialDataReset_Btn IsNot Nothing Then TrialDataReset_Btn.Visible = False
            ApplyStateBudgetVisibility()
        End If
    End Sub

    Private Sub EnsureTrialDataResetButton()
        If TrialDataReset_Btn IsNot Nothing Then Return

        TrialDataReset_Btn = New Button With {
            .Name = "TrialDataReset_Btn",
            .Text = "تهيئة بيانات القيود التجريبية",
            .Font = New Font("Segoe UI Semibold", 11.0!, FontStyle.Bold),
            .BackColor = Color.FromArgb(217, 119, 6),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Visible = False,
            .Height = 48,
            .Width = 260,
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        }
        TrialDataReset_Btn.FlatAppearance.BorderSize = 0
        AddHandler TrialDataReset_Btn.Click, AddressOf TrialDataReset_Btn_Click

        Me.Controls.Add(TrialDataReset_Btn)
        PositionTrialDataResetButton()
    End Sub

    Private Sub PositionTrialDataResetButton()
        If TrialDataReset_Btn Is Nothing Then Return

        Dim bottomMargin As Integer = If(ToolStrip1 Is Nothing, 60, ToolStrip1.Height + 58)
        TrialDataReset_Btn.Left = Math.Max(8, Me.ClientSize.Width - TrialDataReset_Btn.Width - 12)
        TrialDataReset_Btn.Top = Math.Max(8, Me.ClientSize.Height - bottomMargin - TrialDataReset_Btn.Height)
    End Sub

    Private Sub SetMainActionsEnabled(enabled As Boolean)
        SetMainActionsEnabled(Me, enabled)
        MenuStrip1.Enabled = enabled
        Sync_Btn.Enabled = enabled
        ActiveLinkLa.Enabled = True

        If TrialDataReset_Btn IsNot Nothing Then
            TrialDataReset_Btn.Enabled = True
            TrialDataReset_Btn.Visible = Not enabled
        End If
    End Sub

    Private Sub SetMainActionsEnabled(parent As Control, enabled As Boolean)
        For Each c As Control In parent.Controls
            If TypeOf c Is Button AndAlso Not Object.ReferenceEquals(c, TrialDataReset_Btn) Then
                c.Enabled = enabled
            End If

            If c.HasChildren Then SetMainActionsEnabled(c, enabled)
        Next
    End Sub

    Private Sub TrialDataReset_Btn_Click(sender As Object, e As EventArgs)
        Using f As New TrialDataResetForm()
            If f.ShowDialog(Me) = DialogResult.OK Then
                Load_Form()
            End If
        End Using
    End Sub

    Private Sub LayoutActivationLabels()
        If ActiveLinkLa Is Nothing OrElse Bill_Num_LB Is Nothing Then Return

        Dim bottomMargin As Integer = If(ToolStrip1 Is Nothing, 4, ToolStrip1.Height + 4)
        Dim labelHeight As Integer = 45
        Dim labelTop As Integer = Math.Max(0, Me.ClientSize.Height - bottomMargin - labelHeight)
        Dim gap As Integer = 6
        Dim labelWidth As Integer = Math.Max(160, (Me.ClientSize.Width - gap) \ 2)

        Bill_Num_LB.SetBounds(0, labelTop, labelWidth, labelHeight)
        ActiveLinkLa.SetBounds(labelWidth + gap, labelTop, Math.Max(160, Me.ClientSize.Width - labelWidth - gap), labelHeight)

        Bill_Num_LB.BringToFront()
        ActiveLinkLa.BringToFront()
        If ToolStrip1 IsNot Nothing Then ToolStrip1.BringToFront()
        PositionTrialDataResetButton()
    End Sub


    Public Sub AddStatsRow_Runtime()

        stats_Panel.Controls.Clear()

        ' حماية إضافية (اختياري)
        If Me.DesignMode Then Exit Sub

        Dim stats = CreateStatsRow()
        stats.Dock = DockStyle.Top

        stats_Panel.Controls.Add(stats)
        stats.BringToFront()
        ToolStrip1.BringToFront()

        'RefreshButtonColors(Me)
    End Sub


    Private IsDarkTheme As Boolean = MY_Settings.is_Dark_mode

    'Private Sub ToggleTheme()
    '    'IsDarkTheme = Not IsDarkTheme

    '    If IsDarkTheme Then
    '        ApplyDarkTheme(Me)
    '    Else
    '        ApplyLightTheme(Me)
    '    End If
    'End Sub

    Private Sub ApplyLightTheme(ctrl As Control)
        ctrl.BackColor = Color.FromArgb(246, 247, 251)
        For Each c As Control In ctrl.Controls
            ApplyLightTheme(c)
        Next
    End Sub

    Private Sub ApplyDarkTheme(ctrl As Control)
        ctrl.BackColor = Color.FromArgb(17, 24, 39)
        ctrl.ForeColor = Color.White
        For Each c As Control In ctrl.Controls
            ApplyDarkTheme(c)
        Next
    End Sub



    Private Sub BuildDashboard_FromDesigner()
        ' إخفاء القروبات القديمة
        BasicGroupBox.Visible = False
        GroupBox1.Visible = False
        GroupBox2.Visible = False
        GroupBox3.Visible = False
        AssignExistingDashboardPermissionKeys()


        '.RightToLeft = RightToLeft.Yes,
        Dim includeBudget As Boolean = MY_Settings.Use_State_Budget
        Dim dashboardColumns As Integer = If(includeBudget, 6, 4)

        Dim layout As New TableLayoutPanel With {
        .Dock = DockStyle.Fill,
        .BackColor = Me.BackColor,
        .Padding = New Padding(12),
        .ColumnCount = dashboardColumns,
        .RowCount = 1
    }

        For i = 1 To dashboardColumns
            layout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, CSng(100 / dashboardColumns)))
        Next
        layout.RowStyles.Add(New RowStyle(SizeType.Percent, 100))

        Dim colIndex As Integer = 0

        ' Cards (بنفس تقسيمك)
        layout.Controls.Add(CreateCard("البيانات الأساسية",
        {Button1, Button19, Button3, Currency_Btn, Button10}), colIndex, 0)
        colIndex += 1

        layout.Controls.Add(CreateCard("الإدخال اليومي",
        {Button5, Button16, Button17, Button22, Button24, Button13}), colIndex, 0)
        colIndex += 1

        If includeBudget Then
            layout.Controls.Add(CreateCard("بيانات الموازنة الأساسية",
            CreateBudgetBasicButtons()), colIndex, 0)
            colIndex += 1

            layout.Controls.Add(CreateCard("الإدخال اليومي للموازنة",
            CreateBudgetDailyButtons()), colIndex, 0)
            colIndex += 1
        End If

        layout.Controls.Add(CreateCard("التقارير المالية",
        {Button2, Button23, Button4, Button20, Button7, Button9, Button8, Button21, Button25}), colIndex, 0)
        colIndex += 1

        layout.Controls.Add(CreateCard("إدارة النظام",
        {Button18, Button15, Button11, Button12, Button14, Button6}), colIndex, 0)

        ApplyTreeMainPermissions(layout)
        ApplyStateBudgetVisibility()
        Me.Controls.Add(layout)
        layout.BringToFront()
        ToolStrip1.BringToFront()
        LayoutActivationLabels()
    End Sub

    Private Function CreateBudgetBasicButtons() As IEnumerable(Of Button)
        Return New Button() {
            CreateDashboardActionButton("TREE.BUDGET_DOORS", "إدارة الأبواب", Sub() FrmBudgetDoors.Show()),
            CreateDashboardActionButton("TREE.BUDGET_CHAPTERS", "إدارة الفصول", Sub() FrmBudgetChapters.Show()),
            CreateDashboardActionButton("TREE.BUDGET_ITEMS", "إدارة البنود", Sub() FrmBudgetItems.Show()),
            CreateDashboardActionButton("TREE.BUDGET_ACCOUNT_MAPPING", "ربط الحسابات ببنود الموازنة", Sub() FrmAccountBudgetMapping.Show()),
            CreateDashboardActionButton("TREE.BUDGET_ALLOCATIONS", "اعتمادات الموازنة", Sub() FrmBudgetAllocations.Show())
        }
    End Function

    Private Function CreateBudgetDailyButtons() As IEnumerable(Of Button)
        Return New Button() {
            CreateDashboardActionButton("TREE.BUDGET_RESERVE", "حجز موازنة", Sub()
                                                                                 Dim f As New FrmBudgetEntries()
                                                                                 f.EntryMode = 2
                                                                                 f.ShowDialog()
                                                                             End Sub),
            CreateDashboardActionButton("TREE.BUDGET_SPEND", "صرف موازنة", Sub()
                                                                               Dim f As New FrmBudgetEntries()
                                                                               f.EntryMode = 1
                                                                               f.ShowDialog()
                                                                           End Sub),
            CreateDashboardActionButton("TREE.BUDGET_RELEASE_TO_SPEND", "تحويل الحجز إلى صرف", Sub() FrmBudgetReleaseToSpend.Show()),
            CreateDashboardActionButton("TREE.BUDGET_TRANSFER", "تحويل بين بنود الموازنة", Sub() FrmBudgetTransfer.Show()),
            CreateDashboardActionButton("TREE.BUDGET_DASHBOARD", "لوحة موقف الموازنة", Sub() FrmBudgetDashboard.Show()),
            CreateDashboardActionButton("TREE.BUDGET_RESERVATIONS_REPORT", "تقرير الحجوزات", Sub() FrmBudgetReservationsReports.Show())
        }
    End Function

    Private Function CreateDashboardActionButton(permissionKey As String, text As String, action As Action) As Button
        Dim btn As New Button With {
            .Text = text,
            .UseVisualStyleBackColor = False,
            .AccessibleName = permissionKey
        }

        AddHandler btn.Click,
            Sub()
                If action IsNot Nothing Then action.Invoke()
            End Sub

        Return btn
    End Function

    Private Sub AssignExistingDashboardPermissionKeys()
        Button1.AccessibleName = "TREE.ACCOUNTS"
        Button19.AccessibleName = "TREE.FIXED_ASSETS"
        Button3.AccessibleName = "TREE.COST_CENTERS"
        Currency_Btn.AccessibleName = "TREE.CURRENCIES"
        Button10.AccessibleName = "TREE.FISCAL_YEAR"

        Button5.AccessibleName = "TREE.JOURNAL"
        Button16.AccessibleName = "TREE.RECEIPT_IN"
        Button17.AccessibleName = "TREE.RECEIPT_OUT"
        Button22.AccessibleName = "TREE.CHEQUES"
        Button24.AccessibleName = "TREE.SETTLEMENT"
        Button13.AccessibleName = "TREE.JOURNAL_LIST"

        Button2.AccessibleName = "TREE.BALANCES_REVIEW"
        Button23.AccessibleName = "TREE.ACC_LEDGER"
        Button4.AccessibleName = "TREE.BALANCE_SHEET"
        Button20.AccessibleName = "TREE.CURRENT_BALANCES"
        Button7.AccessibleName = "TREE.INCOME_STATEMENT"
        Button9.AccessibleName = "TREE.DAILY_REPORT"
        Button8.AccessibleName = "TREE.FINANCIAL_REPORTS"
        Button21.AccessibleName = "TREE.CASH_FLOW"
        Button25.AccessibleName = "TREE.COST_CENTER_BALANCES"

        Button18.AccessibleName = "TREE.SYSTEM_SETTINGS"
        Button15.AccessibleName = "TREE.RECEIPT_SETTINGS"
        Button11.AccessibleName = "TREE.INCOME_DESIGNER"
        Button12.AccessibleName = "TREE.CURRENCY_RATES"
        Button14.AccessibleName = "TREE.ACCOUNT_PERMISSIONS"
        Button6.AccessibleName = "TREE.USERS"

        إدارةالأبوابToolStripMenuItem.Tag = "TREE.BUDGET_DOORS"
        إدارةالفصولToolStripMenuItem.Tag = "TREE.BUDGET_CHAPTERS"
        إدارةالبنودToolStripMenuItem.Tag = "TREE.BUDGET_ITEMS"
        ربطالحساباتببنودالموازنةToolStripMenuItem.Tag = "TREE.BUDGET_ACCOUNT_MAPPING"
        اعتماداتالموازنةToolStripMenuItem.Tag = "TREE.BUDGET_ALLOCATIONS"
        حجـــزToolStripMenuItem.Tag = "TREE.BUDGET_RESERVE"
        صـــرفToolStripMenuItem.Tag = "TREE.BUDGET_SPEND"
        تحويلالحجزإلىصرفToolStripMenuItem.Tag = "TREE.BUDGET_RELEASE_TO_SPEND"
        تحويلبينبنودالموازنةToolStripMenuItem.Tag = "TREE.BUDGET_TRANSFER"
        لوحةموقفالموازنةToolStripMenuItem.Tag = "TREE.BUDGET_DASHBOARD"
        الحجوزاتToolStripMenuItem.Tag = "TREE.BUDGET_RESERVATIONS_REPORT"
    End Sub

    Private Sub ApplyTreeMainPermissions(root As Control)
        TreeMainPermissions.EnsureTreePermissionTable()
        Dim allowed = TreeMainPermissions.LoadAllowedPermissions(USER_ID, User_isAdmin)
        ApplyTreeMainPermissionsToControls(root, allowed)
        ApplyTreeMainPermissionsToMenuItems(allowed)
    End Sub

    Private Sub ApplyTreeMainPermissionsToControls(parent As Control, allowed As HashSet(Of String))
        For Each c As Control In parent.Controls
            If TypeOf c Is Button AndAlso Not String.IsNullOrWhiteSpace(c.AccessibleName) Then
                c.Visible = (User_isAdmin OrElse allowed.Contains(c.AccessibleName))
                c.Enabled = c.Visible
            End If

            If c.HasChildren Then ApplyTreeMainPermissionsToControls(c, allowed)
        Next
    End Sub

    Private Sub ApplyTreeMainPermissionsToMenuItems(allowed As HashSet(Of String))
        For Each item As ToolStripItem In ToolStrip1.Items
            ApplyTreeMainPermissionsToMenuItem(item, allowed)
        Next
    End Sub

    Private Sub ApplyTreeMainPermissionsToMenuItem(item As ToolStripItem, allowed As HashSet(Of String))
        Dim key As String = TryCast(item.Tag, String)

        If Not String.IsNullOrWhiteSpace(key) Then
            item.Visible = (User_isAdmin OrElse allowed.Contains(key))
            item.Enabled = item.Visible
        End If

        Dim menu = TryCast(item, ToolStripMenuItem)
        If menu Is Nothing Then Exit Sub

        For Each child As ToolStripItem In menu.DropDownItems
            ApplyTreeMainPermissionsToMenuItem(child, allowed)
        Next

        If String.IsNullOrWhiteSpace(key) AndAlso menu.DropDownItems.Count > 0 Then
            Dim anyVisible As Boolean = False
            For Each child As ToolStripItem In menu.DropDownItems
                If child.Visible Then
                    anyVisible = True
                    Exit For
                End If
            Next

            menu.Visible = anyVisible
            menu.Enabled = anyVisible
        End If
    End Sub

    Private Sub ApplyStateBudgetVisibility()
        If الأبوابToolStripMenuItem IsNot Nothing Then
            الأبوابToolStripMenuItem.Visible = MY_Settings.Use_State_Budget
            الأبوابToolStripMenuItem.Enabled = MY_Settings.Use_State_Budget
        End If
    End Sub

    Private Function CreateCard(title As String, buttons As IEnumerable(Of Button)) As Panel

        Dim card As New Panel With {
        .BackColor = Color.White,
        .Dock = DockStyle.Fill,
        .Margin = New Padding(8),
        .Padding = New Padding(12)
    }

        Dim lbl As New Label With {
    .Text = title,
    .Dock = DockStyle.Top,
    .Height = 40,
    .TextAlign = ContentAlignment.MiddleRight,
    .Font = New Font("Segoe UI", 12.5!, FontStyle.Bold),
    .ForeColor = Color.FromArgb(37, 99, 235) ' Primary
}

        Dim flp As New FlowLayoutPanel With {
        .Dock = DockStyle.Fill,
        .FlowDirection = FlowDirection.TopDown,
        .WrapContents = False,
        .AutoScroll = True,
        .RightToLeft = RightToLeft.Yes
    }

        'Dim primaryBtn = CreatePrimaryButton("فتح " & title)
        'flp.Controls.Add(primaryBtn)

        card.Controls.Add(flp)
        card.Controls.Add(lbl)

        For Each b In buttons
            PrepareDashboardButton(b, flp)
            flp.Controls.Add(b)
        Next

        ' تمدد تلقائي عند تغيير الحجم
        AddHandler flp.SizeChanged,
        Sub()
            For Each c As Control In flp.Controls
                If TypeOf c Is Button Then
                    c.Width = flp.ClientSize.Width - 6
                End If
            Next
        End Sub

        Return card
    End Function

    Private Function CreatePrimaryButton(text As String) As Button
        Return New Button With {
        .Text = "➕  " & text,
        .Height = 46,
        .Dock = DockStyle.Top,
        .FlatStyle = FlatStyle.Flat,
        .BackColor = Color.FromArgb(37, 99, 235),
        .ForeColor = Color.White,
        .Font = New Font("Segoe UI", 10.5!, FontStyle.Bold)
    }
    End Function


    Private Sub PrepareDashboardButton(btn As Button, host As FlowLayoutPanel)
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.Height = 44
        btn.Margin = New Padding(0, 0, 0, 8)
        'btn.TextAlign = ContentAlignment.MiddleRight
        btn.Padding = New Padding(12, 0, 12, 0)


        btn.BackColor = Color.FromArgb(238, 242, 255)
        btn.ForeColor = Color.FromArgb(17, 24, 39)
        btn.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        ' نحفظ اللون الأصلي
        btn.Tag = btn.BackColor

        btn.Text = AddUnicodeSymbol(btn.Text)
        btn.Width = host.ClientSize.Width - 6

        ' Hover آمن
        AddHandler btn.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btn.MouseLeave, AddressOf Button_MouseLeave

    End Sub

    Private Sub Button_MouseEnter(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        btn.BackColor = GetHoverColor()
    End Sub

    Private Sub Button_MouseLeave(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        btn.BackColor = CType(btn.Tag, Color)
    End Sub

    Private Function GetButtonBaseColor() As Color
        If IsDarkTheme Then
            Return Color.FromArgb(31, 41, 55)   ' Dark card button
        Else
            Return Color.FromArgb(238, 242, 255) ' Light
        End If
    End Function

    Private Function GetHoverColor() As Color
        If IsDarkTheme Then
            Return Color.FromArgb(55, 65, 81)
        Else
            Return Color.FromArgb(224, 231, 255)
        End If
    End Function

    Private Function GetButtonTextColor() As Color
        If IsDarkTheme Then
            Return Color.White
        Else
            Return Color.FromArgb(17, 24, 39)
        End If
    End Function


    Private Sub RefreshButtonColors(parent As Control)
        For Each c As Control In parent.Controls
            If TypeOf c Is Button Then
                Dim b = CType(c, Button)
                b.BackColor = GetButtonBaseColor()
                b.ForeColor = GetButtonTextColor()
                b.Tag = b.BackColor   ' 🔑 تحديث اللون الأصلي
            ElseIf c.HasChildren Then
                RefreshButtonColors(c)
            End If
        Next
    End Sub



    Private Function AddUnicodeSymbol(text As String) As String
        If text.Contains("الدليل") Then Return "📁  " & text
        If text.Contains("قيود") Or text.Contains("قيد") Then Return "🧾  " & text
        If text.Contains("ميزان") Then Return "📊  " & text
        If text.Contains("سند قبض") Then Return "💰  " & text
        If text.Contains("سند صرف") Then Return "💸  " & text
        If text.Contains("المستخدم") Then Return "👤  " & text
        If text.Contains("العملات") Then Return "💱  " & text
        If text.Contains("السنة") Then Return "📅  " & text
        If text.Contains("إدارة") Or text.Contains("النظام") Then Return "⚙  " & text
        If text.Contains("أصول") Then Return "🏦  " & text
        If text.Contains("تقارير") Or text.Contains("قائمة") Then Return "📄  " & text
        If text.Contains("تزامن") Then Return "🔄  " & text

        Return "▶  " & text
    End Function


    Private Function CreateStatCard(title As String, ByRef valueLabel As Label) As Panel
        Dim p As New Panel With {
        .BackColor = Color.White,
        .Margin = New Padding(6),
        .Padding = New Padding(12),
        .Height = 90,
        .Dock = DockStyle.Fill
    }

        Dim lblTitle As New Label With {
        .Text = title,
        .Dock = DockStyle.Top,
        .Font = New Font("Segoe UI", 9.5!, FontStyle.Bold),
        .ForeColor = Color.Gray
    }

        valueLabel = New Label With {
        .Dock = DockStyle.Fill,
        .Font = New Font("Segoe UI", 14, FontStyle.Bold),
        .ForeColor = Color.FromArgb(37, 99, 235),
        .TextAlign = ContentAlignment.MiddleRight
    }

        p.Controls.Add(valueLabel)
        p.Controls.Add(lblTitle)

        Return p
    End Function


    Private Function CreateStatsRow() As TableLayoutPanel
        Dim tlp As New TableLayoutPanel With {
        .Dock = DockStyle.Top,
        .Height = 110,
        .ColumnCount = 4
    }

        For i = 1 To 4
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25))
        Next

        tlp.Controls.Add(CreateStatCard("عدد الحسابات", lblAccountsCount), 3, 0)
        tlp.Controls.Add(CreateStatCard("القيود", lblTodayEntries), 2, 0)
        tlp.Controls.Add(CreateStatCard("إجمالي الرصيد", lblTotalBalance), 1, 0)
        tlp.Controls.Add(CreateStatCard("السنة المالية", lblFiscalYear), 0, 0)

        Return tlp
    End Function


    Public Sub UpdateStatistics(accounts As String, todayEntries As String, totalBalance As String, yearName As String)
        lblAccountsCount.Text = accounts.ToString()
        lblTodayEntries.Text = todayEntries.ToString()
        lblTotalBalance.Text = totalBalance '.ToString("N0")
        lblFiscalYear.Text = yearName
    End Sub



    'Private Function CreateStatCard(title As String, value As String) As Panel
    '    Dim p As New Panel With {
    '    .BackColor = Color.White,
    '    .Margin = New Padding(6),
    '    .Padding = New Padding(12),
    '    .Height = 90,
    '    .Dock = DockStyle.Fill
    '}

    '    Dim lblTitle As New Label With {
    '    .Text = title,
    '    .Dock = DockStyle.Top,
    '    .Font = New Font("Segoe UI", 9.5!, FontStyle.Bold),
    '    .ForeColor = Color.Gray
    '}

    '    Dim lblValue As New Label With {
    '    .Text = value,
    '    .Dock = DockStyle.Fill,
    '    .Font = New Font("Segoe UI", 18, FontStyle.Bold),
    '    .ForeColor = Color.FromArgb(37, 99, 235),
    '    .TextAlign = ContentAlignment.MiddleRight
    '}

    '    p.Controls.Add(lblValue)
    '    p.Controls.Add(lblTitle)
    '    Return p
    'End Function


    'Private Function CreateStatsRow() As TableLayoutPanel

    '    GET_summary()

    '    Dim tlp As New TableLayoutPanel With {
    '    .Dock = DockStyle.Top,
    '    .Height = 110,
    '    .ColumnCount = 4
    '}

    '    For i = 1 To 4
    '        tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25))
    '    Next

    '    tlp.Controls.Add(CreateStatCard("عدد الحسابات", AccountsCount), 3, 0)
    '    tlp.Controls.Add(CreateStatCard("عدد القيود", JournalCount), 2, 0)
    '    tlp.Controls.Add(CreateStatCard("إجمالي الرصيد", BalanceTotal), 1, 0)
    '    tlp.Controls.Add(CreateStatCard("سنة مالية", F_YEAR.ToString), 0, 0)

    '    Return tlp
    'End Function


    '------------------------------------------------------------------------------------------


    ' دالة لإعطاء ستايل للأزرار فقط
    'Private Sub StyleControls(parent As Control)
    '    For Each ctrl As Control In parent.Controls
    '        If TypeOf ctrl Is Button Then
    '            Dim btn As Button = DirectCast(ctrl, Button)

    '            ' تحسين الشكل
    '            btn.FlatStyle = FlatStyle.Flat
    '            btn.FlatAppearance.BorderSize = 0
    '            btn.BackColor = Color.FromArgb(0, 123, 255) ' لون أساسي أزرق
    '            btn.ForeColor = Color.White
    '            btn.Font = New Font("Segoe UI", 10, FontStyle.Bold)
    '            btn.Cursor = Cursors.Hand
    '            btn.Height = 45

    '            ' Hover Animation بسيط
    '            AddHandler btn.MouseEnter, Sub(s, e) btn.BackColor = ControlPaint.Light(Color.FromArgb(0, 123, 255))
    '            AddHandler btn.MouseLeave, Sub(s, e) btn.BackColor = Color.FromArgb(0, 123, 255)
    '        End If

    '        ' استدعاء نفسه لو فيه Panels أو GroupBoxes
    '        If ctrl.HasChildren Then
    '            StyleControls(ctrl)
    '        End If
    '    Next
    'End Sub



    Private Sub CHECK_YAER()
        If Identifiers.F_YEAR = 0 Then
            YEAR_Txt_Tool.Text = " غير محدد "
        Else
            YEAR_Txt_Tool.Text = F_YEAR.ToString
        End If


        If MY_Settings.is_Link_With_SB = True Then
            IS_SALES_DB_Tool_TXT.Text = "نعم"
            Watcher_Timer.Start()
        Else
            IS_SALES_DB_Tool_TXT.Text = "لا"
            Watcher_Timer.Stop()
        End If

        SALES_DB_Tool_TXT.Text = MY_Settings.SALES_DB



    End Sub

    Private Sub login_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If OpenedFromCPOS Then
            OpenedFromCPOS = False
            Exit Sub
        End If

        Application.ExitThread()
        Application.Exit()
        Kill_All_Processes()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Balances_Form.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Balance_sheet_01.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Income_Statement.Show()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Income_Statement_QUART.Show()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        F_MONTHS_CALENDR = New MONTHS_CALENDR
        F_MONTHS_CALENDR.Show()
        GET_FAINANCIAL_YEAR()
        CHECK_YAER()
    End Sub

    Private Sub Currency_Btn_Click(sender As Object, e As EventArgs) Handles Currency_Btn.Click
        Dim F As New Currencies
        F.Form_Name = "Currency"
        F.Form_Name_Arabic = "العمـــلات"
        F.F_ID = "Cr_ID"
        F.F_Name = "Cr_Name"
        F.F_DETAILS = "Currency"

        F.Checked_Table = "ACC_BALANCE_MASTER"
        F.Checked_Table_ID = "Currency_ID"
        F.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        FrmIncomeStatementDesigner.Show()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim F As New EMP_Add_Periods
        F.Show()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim f As New Daily_B_Form
        f.Show()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim f As New USER_VALID_ACCOUNT
        f.Show()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Rct_Mang.Show()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        F_Receipt = New Receipt
        F_Receipt.AG_Type = 3
        F_Receipt.Show()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        F_Receipt = New Receipt
        F_Receipt.AG_Type = 4
        F_Receipt.Show()
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Sys_Settings.Show()
    End Sub

    Private Sub Sync_Btn_Click(sender As Object, e As EventArgs) Handles Sync_Btn.Click

        If MessageBox.Show(" إجراء تزامن بشكل يدوي مع المبيعات ", "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then

            Try
                Me.Cursor = Cursors.AppStarting

                query("
        DELETE B
        FROM [" & MY_Settings.DataBase & "].[dbo].[SALES_SYSTEM_BALANCES] B
        WHERE NOT EXISTS (
            SELECT 1
            FROM [" & MY_Settings.SALES_DB & "].[dbo].[BALANCES_VIEW] A
            WHERE 
                A.T_ID = B.Tran_ID AND
                A.DATE = B.DATE AND
                A.Bill_Num = B.Bill_Num AND
                A.DEBIT = B.DEBIT AND
                A.CREDIT = B.CREDIT AND
                A.AMOUNT = B.AMOUNT
        );


        ")

                query("
        INSERT INTO [" & MY_Settings.DataBase & "].[dbo].[SALES_SYSTEM_BALANCES] ([Tran_ID], [DATE], [Bill_Num], [DEBIT], [CREDIT], [AMOUNT])
        SELECT [T_ID], [DATE], [Bill_Num], [DEBIT], [CREDIT], [AMOUNT]
        FROM [" & MY_Settings.SALES_DB & "].[dbo].[BALANCES_VIEW] A
        WHERE NOT EXISTS (
            SELECT 1
            FROM [" & MY_Settings.DataBase & "].[dbo].[SALES_SYSTEM_BALANCES] B
            WHERE 
                A.T_ID = B.Tran_ID AND
                A.DATE = B.DATE AND
                A.Bill_Num = B.Bill_Num AND
                A.DEBIT = B.DEBIT AND
                A.CREDIT = B.CREDIT AND
                A.AMOUNT = B.AMOUNT
        );


        ")
                Dim notification3 As New NotificationForm("إشعار", " تمت المزامنة يدويا ", "bottom")
                notification3.ShowNotification()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.Message)
            End Try

        End If
    End Sub


    Private Sub SYNC_FROM_SALES()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[SYNC_FROM_SALES]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@TREE_DB", MY_Settings.DataBase)
            .Parameters.AddWithValue("@SALES_DB", MY_Settings.SALES_DB)
        End With
        SQL_SP_EXEC(C.Com)
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Watcher_Timer.Enabled = False
        Check_Connect()

    End Sub

    Private Sub Watcher_Timer_Tick(sender As Object, e As EventArgs) Handles Watcher_Timer.Tick

        Try
            If Not BackgroundWorker1.IsBusy Then
                BackgroundWorker1.RunWorkerAsync()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Watcher_Timer.Enabled = True
    End Sub


    Private Async Sub Check_Connect()

        Try
            Connection_ToolStrip.Text = "جارٍ الاتصال بالخادم..."

            ' تشغيل المزامنة في الخلفية
            Await Task.Run(Sub()
                               SYNC_FROM_SALES()
                           End Sub)

            ' بعد الانتهاء تحديث  🔄التفاصيل
            Connection_ToolStrip.Text = " ✅ يعمل بالتزامن مع المبيعات"

        Catch ex As Exception
            Connection_ToolStrip.Text = "حدث خطأ: " & ex.Message

        End Try

    End Sub



    Private Function Get_Watcher_Details()
        Dim C = New C
        Try
            Dim S As String = "SELECT TOP 1 CONCAT( ISNULL(Details,''),ActionTime) AS NOTES  FROM BalanceSyncLogs ORDER BY LogID DESC "
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Return C.Dr("NOTES")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 0
    End Function


    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        F_Fixed_Assets = New Fixed_Assets
        F_Fixed_Assets.Show()
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        F_Current_Balances_By_Family = New Current_Balances_By_Family
        F_Current_Balances_By_Family.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        F_users = New users
        F_users.ShowDialog()
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        F_Cash_Flow_Report = New Cash_Flow_Report
        F_Cash_Flow_Report.Show()
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click

        F_Cheques_Form = New Cheques_Form
        F_Cheques_Form.Show()

    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        BALANCES_REVIEW_NO_LEVELS.Show()
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        F_Accounting_settlement = New Accounting_settlement
        F_Accounting_settlement.Show()
    End Sub

    Private Sub Tree_MainForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.WindowState = FormWindowState.Maximized
        LayoutActivationLabels()
    End Sub

    Private Sub Tree_MainForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        LayoutActivationLabels()
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Cost_Center_Balances.Show()
    End Sub

    Private Sub إدارةالأبوابToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إدارةالأبوابToolStripMenuItem.Click
        FrmBudgetDoors.Show()
    End Sub

    Private Sub إدارةالفصولToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إدارةالفصولToolStripMenuItem.Click
        FrmBudgetChapters.Show()
    End Sub

    Private Sub إدارةالبنودToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إدارةالبنودToolStripMenuItem.Click
        FrmBudgetItems.Show()
    End Sub

    Private Sub ربطالحساباتببنودالموازنةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ربطالحساباتببنودالموازنةToolStripMenuItem.Click
        FrmAccountBudgetMapping.Show()
    End Sub

    Private Sub اعتماداتالموازنةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles اعتماداتالموازنةToolStripMenuItem.Click
        FrmBudgetAllocations.Show()
    End Sub

    Private Sub عملياتالموازنةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عملياتالموازنةToolStripMenuItem.Click

    End Sub

    Private Sub حجـــزToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles حجـــزToolStripMenuItem.Click
        'حجز
        Dim f As New FrmBudgetEntries()
        f.EntryMode = 2
        f.ShowDialog()
    End Sub

    Private Sub صـــرفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles صـــرفToolStripMenuItem.Click
        'صرف
        Dim s As New FrmBudgetEntries()
        s.EntryMode = 1
        s.ShowDialog()
    End Sub

    Private Sub تحويلالحجزإلىصرفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تحويلالحجزإلىصرفToolStripMenuItem.Click
        FrmBudgetReleaseToSpend.Show()
    End Sub

    Private Sub تحويلبينبنودالموازنةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تحويلبينبنودالموازنةToolStripMenuItem.Click
        FrmBudgetTransfer.Show()
    End Sub

    Private Sub لوحةموقفالموازنةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles لوحةموقفالموازنةToolStripMenuItem.Click
        FrmBudgetDashboard.Show()
    End Sub

    Private Sub الحجوزاتToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles الحجوزاتToolStripMenuItem.Click
        FrmBudgetReservationsReports.Show()
    End Sub

    Private Sub إنشاءعمليةصرافةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إنشاءعمليةصرافةToolStripMenuItem.Click
        Dim f As New FrmExchangeCreate
        f.ShowDialog()
    End Sub

    Private Sub إدارةعملياتالصرافةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إدارةعملياتالصرافةToolStripMenuItem.Click
        Dim f As New FrmExchangeManager
        f.ShowDialog()
    End Sub

    Private Sub سجلمراجعةعملياتالصرافةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles سجلمراجعةعملياتالصرافةToolStripMenuItem.Click
        Dim f As New FrmExchangeAuditViewer
        f.ShowDialog()
    End Sub



    Private Sub إدارةنسبةالصرافةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إدارةنسبةالصرافةToolStripMenuItem.Click
        Dim f As New FrmExchangeSettings
        f.ShowDialog()
    End Sub

    Private Sub إعدادربطالحساباتToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles إعدادربطالحساباتToolStripMenuItem1.Click
        Dim f As New FrmExchangeOperationAccounts
        f.ShowDialog()
    End Sub

    Private Sub ActiveLinkLa_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ActiveLinkLa.LinkClicked
        Activation.ShowDialog()
        Load_Form()
    End Sub
End Class
