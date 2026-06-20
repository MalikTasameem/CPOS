Imports System.Data
Imports System.Data.SqlClient

Public Class FrmBudgetDashboard

    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr

    '=========================
    ' Load
    '=========================
    Private Sub FrmBudgetDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            BudgetUiHelper.ApplyBudgetFormStyle(Me)
            ApplyAllGridStyles()

            LoadYears()
            LoadDoors()
            LoadChapters()
            LoadItems()

            ArrangeDashboardLayout()
            RefreshDashboard()
            SetStatus("جاهز")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    '=========================
    ' Styling
    '=========================
    Private Sub ApplyAllGridStyles()
        ApplyGridStyle(dgvDoors)
        ApplyGridStyle(dgvChapters)
        ApplyGridStyle(dgvTopItems)
    End Sub

    Private Sub ApplyGridStyle(g As DataGridView)
        If g Is Nothing Then Exit Sub

        g.EnableHeadersVisualStyles = False
        g.ColumnHeadersHeight = 30
        g.RowTemplate.Height = 26
        g.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0!, FontStyle.Regular)
        g.DefaultCellStyle.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
        g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 232, 240)
        g.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42)
        g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        g.AllowUserToResizeRows = True
        g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        g.GridColor = Color.FromArgb(226, 232, 240)
        g.RowHeadersVisible = False
        g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    '=========================
    ' Filters
    '=========================
    Private Sub LoadYears()
        Dim nowY = DateTime.Now.Year
        cmbYear.Items.Clear()
        For y As Integer = nowY - 2 To nowY + 5
            cmbYear.Items.Add(y)
        Next
        cmbYear.SelectedItem = nowY
    End Sub

    Private Sub LoadDoors()
        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT DoorId,
       DoorCode + N' - ' + DoorName AS DoorText
FROM Budget_Doors
WHERE IsActive = 1
ORDER BY DoorCode;", cn)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbDoor.DataSource = dt
        cmbDoor.DisplayMember = "DoorText"
        cmbDoor.ValueMember = "DoorId"
        cmbDoor.SelectedIndex = -1
    End Sub

    Private Sub LoadChapters()
        Dim dt As New DataTable()

        If cmbDoor.SelectedIndex < 0 Then
            cmbChapter.DataSource = Nothing
            Return
        End If

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT ChapterId,
       ChapterCode + N' - ' + ChapterName AS ChapterText
FROM Budget_Chapters
WHERE DoorId = @DoorId AND IsActive = 1
ORDER BY ChapterCode;", cn)

                cmd.Parameters.AddWithValue("@DoorId", cmbDoor.SelectedValue)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbChapter.DataSource = dt
        cmbChapter.DisplayMember = "ChapterText"
        cmbChapter.ValueMember = "ChapterId"
        cmbChapter.SelectedIndex = -1

        cmbItem.DataSource = Nothing
    End Sub

    Private Sub LoadItems()
        Dim dt As New DataTable()

        If cmbChapter.SelectedIndex < 0 Then
            cmbItem.DataSource = Nothing
            Return
        End If

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT BudgetItemId,
       ItemCode + N' - ' + ItemName AS ItemText
FROM Budget_Items
WHERE ChapterId = @ChapterId AND IsActive = 1
ORDER BY ItemCode;", cn)

                cmd.Parameters.AddWithValue("@ChapterId", cmbChapter.SelectedValue)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbItem.DataSource = dt
        cmbItem.DisplayMember = "ItemText"
        cmbItem.ValueMember = "BudgetItemId"
        cmbItem.SelectedIndex = -1
    End Sub

    Private Function SelectedYear() As Integer
        If cmbYear.SelectedItem Is Nothing Then Return 0
        Return Convert.ToInt32(cmbYear.SelectedItem)
    End Function

    '=========================
    ' KPIs
    '=========================

    Private Sub LoadKpis()
        Dim y As Integer = SelectedYear()

        Using cn As New SqlConnection(ConnStr)
            cn.Open()

            Using cmd As New SqlCommand("
SELECT
    Allocated = ISNULL(SUM(Allocated), 0),
    Spent     = ISNULL(SUM(Spent), 0),
    Reserved  = ISNULL(SUM(Reserved), 0),
    Available = ISNULL(SUM(Available), 0)
FROM dbo.Vw_BudgetItemSummary
WHERE FiscalYear = @Y;", cn)

                cmd.Parameters.AddWithValue("@Y", y)

                Using rd = cmd.ExecuteReader()
                    If rd.Read() Then
                        lblAllocatedVal.Text = Convert.ToDecimal(rd("Allocated")).ToString("N3") & " دينار"
                        lblSpentVal.Text = Convert.ToDecimal(rd("Spent")).ToString("N3") & " دينار"
                        lblReservedVal.Text = Convert.ToDecimal(rd("Reserved")).ToString("N3") & " دينار"
                        lblAvailableVal.Text = Convert.ToDecimal(rd("Available")).ToString("N3") & " دينار"
                    End If
                End Using
            End Using
        End Using
    End Sub



    '    Private Sub LoadKpis()
    '        Dim y As Integer = SelectedYear()

    '        Using cn As New SqlConnection(ConnStr)
    '            cn.Open()

    '            'Allocated
    '            Using cmd As New SqlCommand("
    'SELECT ISNULL(SUM(AllocatedAmount),0)
    'FROM Budget_Allocations
    'WHERE FiscalYear = @Y;", cn)
    '                cmd.Parameters.AddWithValue("@Y", y)
    '                lblAllocatedVal.Text = Convert.ToDecimal(cmd.ExecuteScalar()).ToString("N3") & " دينار"
    '            End Using

    '            'Spent
    '            Using cmd As New SqlCommand("
    'SELECT ISNULL(SUM(Amount),0)
    'FROM Budget_Entries
    'WHERE FiscalYear = @Y AND EntryType = 1;", cn)
    '                cmd.Parameters.AddWithValue("@Y", y)
    '                lblSpentVal.Text = Convert.ToDecimal(cmd.ExecuteScalar()).ToString("N3") & " دينار"
    '            End Using

    '            'Reserved
    '            Using cmd As New SqlCommand("
    'SELECT ISNULL(SUM(Amount),0)
    'FROM Budget_Entries
    'WHERE FiscalYear = @Y AND EntryType = 2;", cn)
    '                cmd.Parameters.AddWithValue("@Y", y)
    '                lblReservedVal.Text = Convert.ToDecimal(cmd.ExecuteScalar()).ToString("N3") & " دينار"
    '            End Using
    '        End Using

    '        Dim allocated As Decimal = ParseMoneyLabel(lblAllocatedVal.Text)
    '        Dim spent As Decimal = ParseMoneyLabel(lblSpentVal.Text)
    '        Dim reserved As Decimal = ParseMoneyLabel(lblReservedVal.Text)

    '        lblAvailableVal.Text = (allocated - spent - reserved).ToString("N3") & " دينار"
    '    End Sub

    '=========================
    ' Grids
    '=========================

    Private Sub LoadDoorsGrid()
        Dim y As Integer = SelectedYear()
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT
    d.DoorCode,
    d.DoorName,

    OriginalAllocated   = ISNULL(SUM(s.OriginalAllocated), 0),
    AdditionalAllocated = ISNULL(SUM(s.AdditionalAllocated), 0),
    ReductionAmount     = ISNULL(SUM(s.ReductionAmount), 0),
    CarriedAmount       = ISNULL(SUM(s.CarriedAmount), 0),
    EmergencyAmount     = ISNULL(SUM(s.EmergencyAmount), 0),

    Allocated = ISNULL(SUM(s.Allocated), 0),
    Spent     = ISNULL(SUM(s.Spent), 0),
    Reserved  = ISNULL(SUM(s.Reserved), 0),
    Available = ISNULL(SUM(s.Available), 0),

    SpendPercent =
        CASE 
            WHEN ISNULL(SUM(s.Allocated), 0) = 0 THEN 0
            ELSE (ISNULL(SUM(s.Spent), 0) / ISNULL(SUM(s.Allocated), 0)) * 100
        END
FROM dbo.Budget_Doors d
LEFT JOIN dbo.Budget_Chapters c 
    ON c.DoorId = d.DoorId
LEFT JOIN dbo.Budget_Items i 
    ON i.ChapterId = c.ChapterId
LEFT JOIN dbo.Vw_BudgetItemSummary s 
    ON s.BudgetItemId = i.BudgetItemId
   AND s.FiscalYear = @Y
WHERE d.IsActive = 1
GROUP BY 
    d.DoorCode, 
    d.DoorName
ORDER BY 
    d.DoorCode;", cn)

                cmd.Parameters.AddWithValue("@Y", y)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvDoors.DataSource = dt
        FormatMoneyColumns(dgvDoors)
        ApplyDashboardGridHeaders(dgvDoors)
    End Sub




    '    Private Sub LoadDoorsGrid()
    '        Dim y As Integer = SelectedYear()
    '        Dim dt As New DataTable()

    '        Using cn As New SqlConnection(ConnStr)
    '            Using cmd As New SqlCommand("
    'SELECT
    '    d.DoorCode,
    '    d.DoorName,
    '    ISNULL(SUM(a.AllocatedAmount),0) AS Allocated,
    '    ISNULL(SUM(CASE WHEN e.EntryType=1 THEN e.Amount ELSE 0 END),0) AS Spent,
    '    ISNULL(SUM(CASE WHEN e.EntryType=2 THEN e.Amount ELSE 0 END),0) AS Reserved
    'FROM Budget_Doors d
    'LEFT JOIN Budget_Chapters c ON c.DoorId = d.DoorId
    'LEFT JOIN Budget_Items i ON i.ChapterId = c.ChapterId
    'LEFT JOIN Budget_Allocations a ON a.BudgetItemId = i.BudgetItemId AND a.FiscalYear = @Y
    'LEFT JOIN Budget_Entries e ON e.BudgetItemId = i.BudgetItemId AND e.FiscalYear = @Y
    'GROUP BY d.DoorCode, d.DoorName
    'ORDER BY d.DoorCode;", cn)

    '                cmd.Parameters.AddWithValue("@Y", y)
    '                Using da As New SqlDataAdapter(cmd)
    '                    da.Fill(dt)
    '                End Using
    '            End Using
    '        End Using

    '        dt.Columns.Add("Available", GetType(Decimal),
    '            "Allocated - Spent - Reserved")

    '        dgvDoors.DataSource = dt
    '        FormatMoneyColumns(dgvDoors)
    '        ApplyDashboardGridHeaders(dgvDoors)
    '    End Sub

    '    Private Sub LoadChaptersGrid()
    '        If cmbDoor.SelectedIndex < 0 Then
    '            dgvChapters.DataSource = Nothing
    '            Exit Sub
    '        End If

    '        Dim y As Integer = SelectedYear()
    '        Dim dt As New DataTable()

    '        Using cn As New SqlConnection(ConnStr)
    '            Using cmd As New SqlCommand("
    'SELECT
    '    c.ChapterCode,
    '    c.ChapterName,
    '    ISNULL(SUM(a.AllocatedAmount),0) AS Allocated,
    '    ISNULL(SUM(CASE WHEN e.EntryType=1 THEN e.Amount ELSE 0 END),0) AS Spent,
    '    ISNULL(SUM(CASE WHEN e.EntryType=2 THEN e.Amount ELSE 0 END),0) AS Reserved
    'FROM Budget_Chapters c
    'LEFT JOIN Budget_Items i ON i.ChapterId = c.ChapterId
    'LEFT JOIN Budget_Allocations a ON a.BudgetItemId = i.BudgetItemId AND a.FiscalYear = @Y
    'LEFT JOIN Budget_Entries e ON e.BudgetItemId = i.BudgetItemId AND e.FiscalYear = @Y
    'WHERE c.DoorId = @DoorId
    'GROUP BY c.ChapterCode, c.ChapterName
    'ORDER BY c.ChapterCode;", cn)

    '                cmd.Parameters.AddWithValue("@Y", y)
    '                cmd.Parameters.AddWithValue("@DoorId", cmbDoor.SelectedValue)

    '                Using da As New SqlDataAdapter(cmd)
    '                    da.Fill(dt)
    '                End Using
    '            End Using
    '        End Using

    '        dt.Columns.Add("Available", GetType(Decimal),
    '            "Allocated - Spent - Reserved")

    '        dgvChapters.DataSource = dt
    '        FormatMoneyColumns(dgvChapters)
    '        ApplyDashboardGridHeaders(dgvChapters)
    '    End Sub


    '    Private Sub LoadTopItemsGrid()
    '        Dim y As Integer = SelectedYear()
    '        Dim dt As New DataTable()

    '        Using cn As New SqlConnection(ConnStr)
    '            Using cmd As New SqlCommand("
    'SELECT TOP 10
    '    i.BudgetItemId,
    '    i.ItemCode,
    '    i.ItemName,
    '    SUM(e.Amount) AS Spent
    'FROM Budget_Items i
    'JOIN Budget_Entries e ON e.BudgetItemId = i.BudgetItemId
    'WHERE e.FiscalYear = @Y AND e.EntryType = 1
    'GROUP BY i.BudgetItemId, i.ItemCode, i.ItemName
    'ORDER BY Spent DESC;", cn)

    '                cmd.Parameters.AddWithValue("@Y", y)
    '                Using da As New SqlDataAdapter(cmd)
    '                    da.Fill(dt)
    '                End Using
    '            End Using
    '        End Using

    '        dgvTopItems.DataSource = dt

    '        If dgvTopItems.Columns.Count > 0 Then
    '            dgvTopItems.Columns("BudgetItemId").Visible = False
    '            dgvTopItems.Columns("ItemCode").HeaderText = "كود البند"
    '            dgvTopItems.Columns("ItemName").HeaderText = "اسم البند"
    '            dgvTopItems.Columns("Spent").HeaderText = "إجمالي المصروف"
    '            dgvTopItems.Columns("Spent").DefaultCellStyle.Format = "N3"
    '            dgvTopItems.Columns("ItemName").FillWeight = 190
    '            dgvTopItems.Columns("ItemCode").FillWeight = 80
    '            dgvTopItems.Columns("Spent").FillWeight = 95
    '        End If
    '    End Sub


    Private Sub LoadChaptersGrid()
        If cmbDoor.SelectedIndex < 0 Then
            dgvChapters.DataSource = Nothing
            Exit Sub
        End If

        Dim y As Integer = SelectedYear()
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT
    c.ChapterCode,
    c.ChapterName,

    OriginalAllocated   = ISNULL(SUM(s.OriginalAllocated), 0),
    AdditionalAllocated = ISNULL(SUM(s.AdditionalAllocated), 0),
    ReductionAmount     = ISNULL(SUM(s.ReductionAmount), 0),
    CarriedAmount       = ISNULL(SUM(s.CarriedAmount), 0),
    EmergencyAmount     = ISNULL(SUM(s.EmergencyAmount), 0),

    Allocated = ISNULL(SUM(s.Allocated), 0),
    Spent     = ISNULL(SUM(s.Spent), 0),
    Reserved  = ISNULL(SUM(s.Reserved), 0),
    Available = ISNULL(SUM(s.Available), 0),

    SpendPercent =
        CASE 
            WHEN ISNULL(SUM(s.Allocated), 0) = 0 THEN 0
            ELSE (ISNULL(SUM(s.Spent), 0) / ISNULL(SUM(s.Allocated), 0)) * 100
        END
FROM dbo.Budget_Chapters c
LEFT JOIN dbo.Budget_Items i 
    ON i.ChapterId = c.ChapterId
LEFT JOIN dbo.Vw_BudgetItemSummary s 
    ON s.BudgetItemId = i.BudgetItemId
   AND s.FiscalYear = @Y
WHERE c.DoorId = @DoorId
  AND c.IsActive = 1
GROUP BY 
    c.ChapterCode, 
    c.ChapterName
ORDER BY 
    c.ChapterCode;", cn)

                cmd.Parameters.AddWithValue("@Y", y)
                cmd.Parameters.AddWithValue("@DoorId", cmbDoor.SelectedValue)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvChapters.DataSource = dt
        FormatMoneyColumns(dgvChapters)
        ApplyDashboardGridHeaders(dgvChapters)
    End Sub


    Private Sub LoadTopItemsGrid()
        Dim y As Integer = SelectedYear()
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT TOP 10
    i.BudgetItemId,
    i.ItemCode,
    i.ItemName,
    Allocated = ISNULL(s.Allocated, 0),
    Spent     = ISNULL(s.Spent, 0),
    Reserved  = ISNULL(s.Reserved, 0),
    Available = ISNULL(s.Available, 0),
    SpendPercent =
        CASE 
            WHEN ISNULL(s.Allocated, 0) = 0 THEN 0
            ELSE (ISNULL(s.Spent, 0) / ISNULL(s.Allocated, 0)) * 100
        END
FROM dbo.Budget_Items i
INNER JOIN dbo.Vw_BudgetItemSummary s
    ON s.BudgetItemId = i.BudgetItemId
   AND s.FiscalYear = @Y
WHERE ISNULL(s.Spent, 0) > 0
ORDER BY 
    s.Spent DESC;", cn)

                cmd.Parameters.AddWithValue("@Y", y)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvTopItems.DataSource = dt
        FormatMoneyColumns(dgvTopItems)
        ApplyTopItemsGridHeaders()
    End Sub

    Private Sub ApplyTopItemsGridHeaders()
        If dgvTopItems.Columns.Count = 0 Then Exit Sub

        If dgvTopItems.Columns.Contains("BudgetItemId") Then dgvTopItems.Columns("BudgetItemId").Visible = False

        If dgvTopItems.Columns.Contains("ItemCode") Then dgvTopItems.Columns("ItemCode").HeaderText = "كود البند"
        If dgvTopItems.Columns.Contains("ItemName") Then dgvTopItems.Columns("ItemName").HeaderText = "اسم البند"
        If dgvTopItems.Columns.Contains("Allocated") Then dgvTopItems.Columns("Allocated").HeaderText = "الاعتماد"
        If dgvTopItems.Columns.Contains("Spent") Then dgvTopItems.Columns("Spent").HeaderText = "المصروف"
        If dgvTopItems.Columns.Contains("Reserved") Then dgvTopItems.Columns("Reserved").HeaderText = "المحجوز"
        If dgvTopItems.Columns.Contains("Available") Then dgvTopItems.Columns("Available").HeaderText = "المتاح"
        If dgvTopItems.Columns.Contains("SpendPercent") Then dgvTopItems.Columns("SpendPercent").HeaderText = "نسبة الصرف %"

        For Each columnName As String In {"Allocated", "Spent", "Reserved", "Available", "SpendPercent"}
            If dgvTopItems.Columns.Contains(columnName) Then
                dgvTopItems.Columns(columnName).DefaultCellStyle.Format = "N3"
                dgvTopItems.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
        Next

        If dgvTopItems.Columns.Contains("ItemName") Then dgvTopItems.Columns("ItemName").FillWeight = 180
        If dgvTopItems.Columns.Contains("ItemCode") Then dgvTopItems.Columns("ItemCode").FillWeight = 70
    End Sub

    Private Function ParseMoneyLabel(text As String) As Decimal
        Dim value As Decimal = 0D
        Decimal.TryParse(text.Replace("دينار", "").Trim(), value)
        Return value
    End Function

    Private Sub FormatMoneyColumns(g As DataGridView)
        For Each columnName As String In {
        "OriginalAllocated",
        "AdditionalAllocated",
        "ReductionAmount",
        "TransferInAmount",
        "TransferOutAmount",
        "CarriedAmount",
        "EmergencyAmount",
        "Allocated",
        "Spent",
        "Reserved",
        "Available",
        "SpendPercent"
    }
            If g.Columns.Contains(columnName) Then
                g.Columns(columnName).DefaultCellStyle.Format = "N3"
                g.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
        Next
    End Sub

    'Private Sub FormatMoneyColumns(g As DataGridView)
    '    For Each c As DataGridViewColumn In g.Columns
    '        If c.ValueType Is GetType(Decimal) Then
    '            c.DefaultCellStyle.Format = "N3"
    '        End If
    '    Next
    'End Sub

    Private Sub ApplyDashboardGridHeaders(g As DataGridView)
        If g.Columns.Count = 0 Then Exit Sub

        If g.Columns.Contains("DoorCode") Then g.Columns("DoorCode").HeaderText = "كود الباب"
        If g.Columns.Contains("DoorName") Then g.Columns("DoorName").HeaderText = "اسم الباب"

        If g.Columns.Contains("ChapterCode") Then g.Columns("ChapterCode").HeaderText = "كود الفصل"
        If g.Columns.Contains("ChapterName") Then g.Columns("ChapterName").HeaderText = "اسم الفصل"

        If g.Columns.Contains("OriginalAllocated") Then g.Columns("OriginalAllocated").HeaderText = "اعتماد أصلي"
        If g.Columns.Contains("AdditionalAllocated") Then g.Columns("AdditionalAllocated").HeaderText = "اعتماد إضافي"
        If g.Columns.Contains("ReductionAmount") Then g.Columns("ReductionAmount").HeaderText = "تخفيض"
        If g.Columns.Contains("CarriedAmount") Then g.Columns("CarriedAmount").HeaderText = "مرحل"
        If g.Columns.Contains("EmergencyAmount") Then g.Columns("EmergencyAmount").HeaderText = "طارئ"

        If g.Columns.Contains("Allocated") Then g.Columns("Allocated").HeaderText = "الاعتماد المعدل"
        If g.Columns.Contains("Spent") Then g.Columns("Spent").HeaderText = "المصروف"
        If g.Columns.Contains("Reserved") Then g.Columns("Reserved").HeaderText = "المحجوز"
        If g.Columns.Contains("Available") Then g.Columns("Available").HeaderText = "المتاح"
        If g.Columns.Contains("SpendPercent") Then g.Columns("SpendPercent").HeaderText = "نسبة الصرف %"

        For Each columnName As String In {"DoorName", "ChapterName"}
            If g.Columns.Contains(columnName) Then
                g.Columns(columnName).FillWeight = 160
            End If
        Next

        For Each columnName As String In {"DoorCode", "ChapterCode"}
            If g.Columns.Contains(columnName) Then
                g.Columns(columnName).FillWeight = 65
            End If
        Next

        For Each columnName As String In {
        "OriginalAllocated",
        "AdditionalAllocated",
        "ReductionAmount",
        "CarriedAmount",
        "EmergencyAmount",
        "Allocated",
        "Spent",
        "Reserved",
        "Available",
        "SpendPercent"
    }
            If g.Columns.Contains(columnName) Then
                g.Columns(columnName).FillWeight = 90
                g.Columns(columnName).DefaultCellStyle.Format = "N3"
                g.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
        Next
    End Sub


    'Private Sub ApplyDashboardGridHeaders(g As DataGridView)
    '    If g.Columns.Count = 0 Then Exit Sub

    '    If g.Columns.Contains("DoorCode") Then g.Columns("DoorCode").HeaderText = "كود الباب"
    '    If g.Columns.Contains("DoorName") Then g.Columns("DoorName").HeaderText = "اسم الباب"
    '    If g.Columns.Contains("ChapterCode") Then g.Columns("ChapterCode").HeaderText = "كود الفصل"
    '    If g.Columns.Contains("ChapterName") Then g.Columns("ChapterName").HeaderText = "اسم الفصل"
    '    If g.Columns.Contains("Allocated") Then g.Columns("Allocated").HeaderText = "إجمالي الاعتماد"
    '    If g.Columns.Contains("Spent") Then g.Columns("Spent").HeaderText = "إجمالي المصروف"
    '    If g.Columns.Contains("Reserved") Then g.Columns("Reserved").HeaderText = "إجمالي الحجوزات"
    '    If g.Columns.Contains("Available") Then g.Columns("Available").HeaderText = "الرصيد المتاح"

    '    For Each columnName As String In {"DoorName", "ChapterName"}
    '        If g.Columns.Contains(columnName) Then
    '            g.Columns(columnName).FillWeight = 170
    '        End If
    '    Next

    '    For Each columnName As String In {"DoorCode", "ChapterCode"}
    '        If g.Columns.Contains(columnName) Then
    '            g.Columns(columnName).FillWeight = 70
    '        End If
    '    Next

    '    For Each columnName As String In {"Allocated", "Spent", "Reserved", "Available"}
    '        If g.Columns.Contains(columnName) Then
    '            g.Columns(columnName).FillWeight = 95
    '            g.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '        End If
    '    Next
    'End Sub

    '=========================
    ' Refresh
    '=========================
    Private Sub RefreshDashboard()
        ArrangeDashboardLayout()
        LoadKpis()
        LoadDoorsGrid()
        LoadChaptersGrid()
        LoadTopItemsGrid()
    End Sub

    Private Sub ArrangeDashboardLayout()
        If pnlHeader Is Nothing OrElse pnlFilters Is Nothing OrElse pnlKpis Is Nothing OrElse pnlContent Is Nothing OrElse pnlActions Is Nothing Then Exit Sub

        Dim margin As Integer = 15
        Dim cardGap As Integer = 10
        Dim contentWidth As Integer = Math.Max(900, Me.ClientSize.Width - (margin * 2))
        Dim topY As Integer = pnlHeader.Bottom + 8
        Dim topHeight As Integer = 96
        Dim filtersWidth As Integer = Math.Max(420, CInt(contentWidth * 0.38))
        Dim kpisWidth As Integer = Math.Max(520, contentWidth - filtersWidth - cardGap)

        If filtersWidth + kpisWidth + cardGap > contentWidth Then
            filtersWidth = Math.Max(380, CInt(contentWidth * 0.36))
            kpisWidth = contentWidth - filtersWidth - cardGap
        End If

        pnlFilters.SetBounds(margin, topY, filtersWidth, topHeight)
        pnlKpis.SetBounds(margin + filtersWidth + cardGap, topY, kpisWidth, topHeight)

        ArrangeFiltersPanel()
        ArrangeKpiCards(cardGap)

        pnlContent.SetBounds(0, pnlFilters.Bottom + 8, Me.ClientSize.Width, Math.Max(300, pnlActions.Top - pnlFilters.Bottom - 13))

        Dim cardWidth As Integer = contentWidth
        Dim baseCardHeight As Integer = Math.Max(145, CInt((pnlContent.Height - (cardGap * 2)) / 3))

        cardDoors.SetBounds(margin, 0, cardWidth, baseCardHeight)
        cardChapters.SetBounds(margin, cardDoors.Bottom + cardGap, cardWidth, baseCardHeight)
        cardTopItems.SetBounds(margin, cardChapters.Bottom + cardGap, cardWidth, Math.Max(baseCardHeight, pnlContent.Height - cardChapters.Bottom - cardGap))

        SizeGridCard(cardDoors, dgvDoors, lblDoorsTitle)
        SizeGridCard(cardChapters, dgvChapters, lblChaptersTitle)
        SizeGridCard(cardTopItems, dgvTopItems, lblTopItemsTitle)
    End Sub

    Private Sub ArrangeFiltersPanel()
        Dim pad As Integer = 8
        Dim labelW As Integer = 52
        Dim yearW As Integer = 105
        Dim rightComboW As Integer = Math.Max(150, CInt((pnlFilters.Width - (pad * 4) - (labelW * 2)) / 2))

        lblYear.SetBounds(pnlFilters.Width - pad - labelW, 12, labelW, 22)
        cmbYear.SetBounds(lblYear.Left - pad - yearW, 10, yearW, 25)

        lblDoor.SetBounds(Math.Max(pad, cmbYear.Left - pad - labelW), 12, labelW, 22)
        cmbDoor.SetBounds(pad, 10, Math.Max(120, lblDoor.Left - (pad * 2)), 25)

        lblChapter.SetBounds(pnlFilters.Width - pad - labelW, 54, labelW, 22)
        cmbChapter.SetBounds(lblChapter.Left - pad - rightComboW, 52, rightComboW, 25)

        lblItem.SetBounds(Math.Max(pad, cmbChapter.Left - pad - labelW), 54, labelW, 22)
        cmbItem.SetBounds(pad, 52, Math.Max(120, lblItem.Left - (pad * 2)), 25)
    End Sub

    Private Sub ArrangeKpiCards(cardGap As Integer)
        Dim kpiWidth As Integer = Math.Max(120, CInt((pnlKpis.Width - (cardGap * 3)) / 4))
        Dim kpiHeight As Integer = pnlKpis.Height - 10

        cardAllocated.SetBounds(pnlKpis.Width - kpiWidth, 5, kpiWidth, kpiHeight)
        cardSpent.SetBounds(cardAllocated.Left - cardGap - kpiWidth, 5, kpiWidth, kpiHeight)
        cardReserved.SetBounds(cardSpent.Left - cardGap - kpiWidth, 5, kpiWidth, kpiHeight)
        cardAvailable.SetBounds(Math.Max(0, cardReserved.Left - cardGap - kpiWidth), 5, Math.Max(120, cardReserved.Left - cardGap), kpiHeight)
    End Sub

    Private Sub SizeGridCard(card As Panel, grid As DataGridView, title As Label)
        title.Left = Math.Max(8, card.Width - title.Width - 16)
        grid.SetBounds(3, 40, card.Width - 6, card.Height - 43)
    End Sub

    '=========================
    ' Events
    '=========================
    Private Sub cmbYear_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbYear.SelectionChangeCommitted
        RefreshDashboard()
    End Sub

    Private Sub cmbDoor_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDoor.SelectionChangeCommitted
        LoadChapters()
        LoadItems()
        LoadChaptersGrid()
    End Sub

    Private Sub cmbChapter_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbChapter.SelectionChangeCommitted
        LoadItems()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        RefreshDashboard()
        SetStatus("تم التحديث")
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    '=========================
    ' Status
    '=========================
    Private Sub SetStatus(msg As String)
        lblStatus.Text = msg
    End Sub

    Private Sub Door_print_Btn_Click(sender As Object, e As EventArgs) Handles Door_print_Btn.Click
        Dim doc = BudgetReports.CreateDoorsStatusReport(CInt(cmbYear.SelectedItem))
        Dim frm As New FrmPrintPreview()
        frm.SetDocument(doc, "تقرير موقف الأبواب")
        frm.ShowDialog()

    End Sub

    Private Sub Chapters_Print_btn_Click(sender As Object, e As EventArgs) Handles Chapters_Print_btn.Click
        If cmbDoor.SelectedIndex < 0 Then
            MessageBox.Show("يرجى اختيار الباب")
            Exit Sub
        End If

        Dim year As Integer = CInt(cmbYear.SelectedItem)
        Dim doorId As Integer = CInt(cmbDoor.SelectedValue)

        Dim doc = BudgetReports.CreateChaptersStatusReport(year, doorId)
        Dim frm As New FrmPrintPreview()
        frm.SetDocument(doc, "تقرير موقف الفصول")
        frm.ShowDialog()

    End Sub

    Private Sub Items_Print_btn_Click(sender As Object, e As EventArgs) Handles Items_Print_btn.Click
        If cmbDoor.SelectedIndex < 0 Or cmbChapter.SelectedIndex < 0 Then
            MessageBox.Show("يرجى اختيار الباب والفصل")
            Exit Sub
        End If

        Dim year As Integer = CInt(cmbYear.SelectedItem)
        Dim doorId As Integer = CInt(cmbDoor.SelectedValue)
        Dim chapterId As Integer = CInt(cmbChapter.SelectedValue)

        Dim doc = BudgetReports.CreateItemsStatusReport(year, doorId, chapterId)
        Dim frm As New FrmPrintPreview()
        frm.SetDocument(doc, "تقرير موقف البنود")
        frm.ShowDialog()

    End Sub

    Private Sub ItemsMV_Print_btn_Click(sender As Object, e As EventArgs) Handles ItemsMV_Print_btn.Click
        If cmbDoor.SelectedIndex < 0 OrElse cmbChapter.SelectedIndex < 0 OrElse (cmbItem.SelectedIndex < 0 AndAlso dgvTopItems.CurrentRow Is Nothing) Then
            MessageBox.Show("يرجى اختيار الباب والفصل والبند أو تحديد بند من قائمة أعلى البنود صرفًا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim year As Integer = CInt(cmbYear.SelectedItem)
        Dim doorId As Integer = CInt(cmbDoor.SelectedValue)
        Dim chapterId As Integer = CInt(cmbChapter.SelectedValue)
        Dim itemId As Integer

        If cmbItem.SelectedIndex >= 0 Then
            itemId = CInt(cmbItem.SelectedValue)
        Else
            itemId = CInt(dgvTopItems.CurrentRow.Cells("BudgetItemId").Value)
        End If

        Dim doc = BudgetReports.CreateItemLedgerReport(year, doorId, chapterId, itemId)
        Dim frm As New FrmPrintPreview()
        frm.SetDocument(doc, "تقرير حركة بند")
        frm.ShowDialog()

    End Sub

    Private Sub Min_Btn_Click(sender As Object, e As EventArgs) 
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Max_Btn_Click(sender As Object, e As EventArgs) 
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.WindowState = FormWindowState.Maximized
        End If
        ArrangeDashboardLayout()
    End Sub

    Private Sub FrmBudgetDashboard_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ArrangeDashboardLayout()
    End Sub
End Class
