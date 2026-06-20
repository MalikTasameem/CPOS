Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing

Public Module BudgetReports


    Private ReadOnly OrgName As String = MY_Settings.SBill_Title_1
    Private ReadOnly OrgDept As String = MY_Settings.SBill_Title_2
    Private ReadOnly LogoPath As String = Application.StartupPath & "\logo\logo.jpg"

    Private CurrentPage As Integer = 0
    Private TotalPages As Integer = 0


    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr



    Public Function CreateBudgetReservationReport(fiscalYear As Integer) As PrintDocument
        Dim dt As DataTable = GetDoorsStatusData(fiscalYear)

        Dim doc As New PrintDocument()

        ' A4 Landscape
        doc.DefaultPageSettings.Landscape = True
        doc.DefaultPageSettings.Margins = New Margins(30, 30, 25, 25)

        AddHandler doc.PrintPage,
            Sub(sender, e)
                PrintDoorsStatus(e, dt, fiscalYear)
            End Sub

        Return doc
    End Function


    Public Function CreateDoorsStatusReport(fiscalYear As Integer) As PrintDocument
        Dim dt As DataTable = GetDoorsStatusData(fiscalYear)

        Dim doc As New PrintDocument()

        ' A4 Landscape
        doc.DefaultPageSettings.Landscape = True
        doc.DefaultPageSettings.Margins = New Margins(30, 30, 25, 25)

        AddHandler doc.PrintPage,
            Sub(sender, e)
                PrintDoorsStatus(e, dt, fiscalYear)
            End Sub

        Return doc
    End Function


    '    Private Function GetDoorsStatusData(y As Integer) As DataTable
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

    '        dt.Columns.Add("Available", GetType(Decimal))
    '        For Each r As DataRow In dt.Rows
    '            Dim alloc As Decimal = Convert.ToDecimal(r("Allocated"))
    '            Dim spent As Decimal = Convert.ToDecimal(r("Spent"))
    '            Dim resv As Decimal = Convert.ToDecimal(r("Reserved"))
    '            r("Available") = alloc - spent - resv
    '        Next

    '        Return dt
    '    End Function


    Private Function GetDoorsStatusData(y As Integer) As DataTable
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

        Return dt
    End Function

    Private Sub PrintDoorsStatus(e As PrintPageEventArgs, dt As DataTable, y As Integer)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Dim fontTitle As New Font("Segoe UI", 14, FontStyle.Bold)
        Dim fontHeader As New Font("Segoe UI", 10, FontStyle.Bold)
        Dim fontBody As New Font("Segoe UI", 10, FontStyle.Regular)
        Dim fontSmall As New Font("Segoe UI", 9, FontStyle.Regular)

        Dim pageW As Integer = e.MarginBounds.Width
        Dim leftX As Integer = e.MarginBounds.Left
        Dim topY As Integer = e.MarginBounds.Top

        Dim yPos As Integer = topY

        ' Header
        Dim title As String = "تقرير موقف الأبواب "
        yPos = DrawReportHeader(
    g,
    e,
   title,
    y
)

        'g.DrawString(title, fontTitle, Brushes.Black, leftX, yPos)
        'yPos += 35

        Dim meta As String = "تاريخ الطباعة: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm") & "    المستخدم: " & USER_ID.ToString()
        g.DrawString(meta, fontSmall, Brushes.DimGray, leftX, yPos)
        yPos += 25

        ' Table settings
        Dim rowH As Integer = 28
        Dim headerH As Integer = 30

        ' Columns (A4 Landscape)
        Dim colDoorCode As Integer = 80
        Dim colDoorName As Integer = 350
        Dim colMoney As Integer = 150

        Dim tableW As Integer = colDoorCode + colDoorName + (colMoney * 4)
        If tableW > pageW Then
            ' لو العرض ضاق: نقلل اسم الباب
            colDoorName = Math.Max(250, pageW - (colDoorCode + colMoney * 4))
            tableW = colDoorCode + colDoorName + (colMoney * 4)
        End If

        Dim x0 As Integer = leftX

        ' Draw header background
        Dim headerRect As New Rectangle(x0, yPos, tableW, headerH)
        g.FillRectangle(New SolidBrush(Color.FromArgb(240, 242, 245)), headerRect)
        g.DrawRectangle(Pens.DarkGray, headerRect)

        ' Header texts
        Dim headers() As String = {"كود", "اسم الباب", "الاعتماد", "المصروف", "المحجوز", "المتاح"}
        Dim widths() As Integer = {colDoorCode, colDoorName, colMoney, colMoney, colMoney, colMoney}

        Dim x As Integer = x0
        For i As Integer = headers.Length - 1 To 0 Step -1
            Dim r As New Rectangle(x, yPos, widths(i), headerH)
            DrawCellText(g, headers(i), fontHeader, r, HorizontalAlignment.Center)
            g.DrawRectangle(Pens.DarkGray, r)
            x += widths(i)
        Next

        yPos += headerH

        ' Rows
        Dim totalAlloc As Decimal = 0D, totalSpent As Decimal = 0D, totalRes As Decimal = 0D, totalAvail As Decimal = 0D

        For Each dr As DataRow In dt.Rows
            If yPos + rowH > e.MarginBounds.Bottom - 40 Then
                e.HasMorePages = True
                Return
            End If

            Dim doorCode As String = dr("DoorCode").ToString()
            Dim doorName As String = dr("DoorName").ToString()
            Dim alloc As Decimal = Convert.ToDecimal(dr("Allocated"))
            Dim spent As Decimal = Convert.ToDecimal(dr("Spent"))
            Dim resv As Decimal = Convert.ToDecimal(dr("Reserved"))
            Dim avail As Decimal = Convert.ToDecimal(dr("Available"))

            totalAlloc += alloc : totalSpent += spent : totalRes += resv : totalAvail += avail

            x = x0
            Dim cells() As String = {
                doorCode,
                doorName,
                alloc.ToString("N3"),
                spent.ToString("N3"),
                resv.ToString("N3"),
                avail.ToString("N3")
            }

            For i As Integer = cells.Length - 1 To 0 Step -1
                Dim r As New Rectangle(x, yPos, widths(i), rowH)
                DrawCellText(g, cells(i), fontBody, r, If(i >= 2, HorizontalAlignment.Right, HorizontalAlignment.Center))
                g.DrawRectangle(Pens.Gainsboro, r)
                x += widths(i)
            Next

            yPos += rowH
        Next

        ' Totals row
        yPos += 5
        Dim totRect As New Rectangle(x0, yPos, tableW, rowH)
        g.FillRectangle(New SolidBrush(Color.FromArgb(250, 250, 250)), totRect)
        g.DrawRectangle(Pens.DarkGray, totRect)

        x = x0
        Dim totCells() As String = {
            "",
            "الإجمالي",
            totalAlloc.ToString("N3"),
            totalSpent.ToString("N3"),
            totalRes.ToString("N3"),
            totalAvail.ToString("N3")
        }

        For i As Integer = totCells.Length - 1 To 0 Step -1
            Dim r As New Rectangle(x, yPos, widths(i), rowH)
            DrawCellText(g, totCells(i), fontHeader, r, If(i >= 2, HorizontalAlignment.Right, HorizontalAlignment.Center))
            g.DrawRectangle(Pens.DarkGray, r)
            x += widths(i)
        Next

        ' Footer
        Dim footerY As Integer = e.MarginBounds.Bottom + 5
        g.DrawString("توقيع المختص: ____________________      اعتماد: ____________________", fontSmall, Brushes.Black, leftX, footerY)

        e.HasMorePages = False
    End Sub


    Public Sub DrawCellText(g As Graphics,
                         txt As String,
                         f As Font,
                         r As Rectangle,
                         align As HorizontalAlignment,
                         Optional br As Brush = Nothing)

        If br Is Nothing Then br = Brushes.Black

        Dim sf As New StringFormat()
        sf.LineAlignment = StringAlignment.Center
        sf.Trimming = StringTrimming.EllipsisCharacter
        sf.FormatFlags = StringFormatFlags.NoWrap

        Select Case align
            Case HorizontalAlignment.Right
                sf.Alignment = StringAlignment.Far
            Case HorizontalAlignment.Center
                sf.Alignment = StringAlignment.Center
            Case Else
                sf.Alignment = StringAlignment.Near
        End Select

        g.DrawString(txt, f, br, r, sf)
    End Sub

    '===============================
    ' R2 - Chapters Status Report
    '===============================
    Public Function CreateChaptersStatusReport(fiscalYear As Integer, doorId As Integer) As Printing.PrintDocument
        Dim doorInfo = GetDoorInfo(doorId)
        Dim dt As DataTable = GetChaptersStatusData(fiscalYear, doorId)

        Dim doc As New Printing.PrintDocument()
        doc.DefaultPageSettings.Landscape = True
        doc.DefaultPageSettings.Margins = New Printing.Margins(30, 30, 25, 25)

        AddHandler doc.PrintPage,
        Sub(sender, e)
            PrintChaptersStatus(e, dt, fiscalYear, doorInfo)
        End Sub

        Return doc
    End Function

    Private Function GetDoorInfo(doorId As Integer) As DataRow
        Dim dt As New DataTable()

        Using cn As New SqlClient.SqlConnection(ConnStr)
            Using cmd As New SqlClient.SqlCommand("
SELECT DoorCode, DoorName
FROM Budget_Doors
WHERE DoorId = @Id;", cn)

                cmd.Parameters.AddWithValue("@Id", doorId)
                Using da As New SqlClient.SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        If dt.Rows.Count = 0 Then Throw New Exception("الباب غير موجود")
        Return dt.Rows(0)
    End Function


    Private Function GetChaptersStatusData(y As Integer, doorId As Integer) As DataTable
        Dim dt As New DataTable()

        Using cn As New SqlClient.SqlConnection(ConnStr)
            Using cmd As New SqlClient.SqlCommand("
SELECT
    c.ChapterCode,
    c.ChapterName,

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
                cmd.Parameters.AddWithValue("@DoorId", doorId)

                Using da As New SqlClient.SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    '    Private Function GetChaptersStatusData(y As Integer, doorId As Integer) As DataTable
    '        Dim dt As New DataTable()

    '        Using cn As New SqlClient.SqlConnection(ConnStr)
    '            Using cmd As New SqlClient.SqlCommand("
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
    '                cmd.Parameters.AddWithValue("@DoorId", doorId)

    '                Using da As New SqlClient.SqlDataAdapter(cmd)
    '                    da.Fill(dt)
    '                End Using
    '            End Using
    '        End Using

    '        dt.Columns.Add("Available", GetType(Decimal))
    '        For Each r As DataRow In dt.Rows
    '            Dim a As Decimal = CDec(r("Allocated"))
    '            Dim s As Decimal = CDec(r("Spent"))
    '            Dim rsv As Decimal = CDec(r("Reserved"))
    '            r("Available") = a - s - rsv
    '        Next

    '        Return dt
    '    End Function

    Private Sub PrintChaptersStatus(e As Printing.PrintPageEventArgs,
                               dt As DataTable,
                               fiscalYear As Integer,
                               doorInfo As DataRow)

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Dim fontTitle As New Font("Segoe UI", 14, FontStyle.Bold)
        Dim fontHeader As New Font("Segoe UI", 10, FontStyle.Bold)
        Dim fontBody As New Font("Segoe UI", 10)
        Dim fontSmall As New Font("Segoe UI", 9)

        Dim leftX As Integer = e.MarginBounds.Left
        Dim yPos As Integer = e.MarginBounds.Top

        ' ===== Header =====
        Dim title As String = "تقرير موقف الفصول "

        yPos = DrawReportHeader(
    g,
    e,
   title,
    fiscalYear
)

        'g.DrawString(title, fontTitle, Brushes.Black, leftX, yPos)
        'yPos += 30

        Dim doorText As String = "الباب: " & doorInfo("DoorCode").ToString() & " - " & doorInfo("DoorName").ToString()
        g.DrawString(doorText, fontHeader, Brushes.Black, leftX, yPos)
        yPos += 22

        Dim meta As String = "تاريخ الطباعة: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm") &
                         "    المستخدم: " & USER_ID.ToString()
        g.DrawString(meta, fontSmall, Brushes.DimGray, leftX, yPos)
        yPos += 25

        ' ===== Table =====
        Dim rowH As Integer = 28
        Dim headerH As Integer = 30

        Dim colCode As Integer = 90
        Dim colName As Integer = 360
        Dim colMoney As Integer = 150

        Dim widths() As Integer = {colCode, colName, colMoney, colMoney, colMoney, colMoney}
        Dim headers() As String = {"كود الفصل", "اسم الفصل", "الاعتماد", "المصروف", "المحجوز", "المتاح"}

        Dim x0 As Integer = leftX
        Dim tableW As Integer = widths.Sum()

        ' Header row
        Dim headerRect As New Rectangle(x0, yPos, tableW, headerH)
        g.FillRectangle(New SolidBrush(Color.FromArgb(240, 242, 245)), headerRect)
        g.DrawRectangle(Pens.DarkGray, headerRect)

        Dim x As Integer = x0
        For i As Integer = headers.Length - 1 To 0 Step -1
            Dim r As New Rectangle(x, yPos, widths(i), headerH)
            DrawCellText(g, headers(i), fontHeader, r, HorizontalAlignment.Center)
            g.DrawRectangle(Pens.DarkGray, r)
            x += widths(i)
        Next
        yPos += headerH

        ' Data rows
        Dim tAlloc, tSpent, tRes, tAvail As Decimal

        For Each dr As DataRow In dt.Rows
            If yPos + rowH > e.MarginBounds.Bottom - 40 Then
                e.HasMorePages = True
                Return
            End If

            Dim cells() As String = {
            dr("ChapterCode").ToString(),
            dr("ChapterName").ToString(),
            CDec(dr("Allocated")).ToString("N3"),
            CDec(dr("Spent")).ToString("N3"),
            CDec(dr("Reserved")).ToString("N3"),
            CDec(dr("Available")).ToString("N3")
        }

            tAlloc += CDec(dr("Allocated"))
            tSpent += CDec(dr("Spent"))
            tRes += CDec(dr("Reserved"))
            tAvail += CDec(dr("Available"))

            x = x0
            For i As Integer = cells.Length - 1 To 0 Step -1
                Dim r As New Rectangle(x, yPos, widths(i), rowH)
                DrawCellText(g, cells(i), fontBody, r, If(i >= 2, HorizontalAlignment.Right, HorizontalAlignment.Center))
                g.DrawRectangle(Pens.Gainsboro, r)
                x += widths(i)
            Next
            yPos += rowH
        Next

        ' Totals
        yPos += 5
        x = x0
        Dim totals() As String = {"", "الإجمالي",
        tAlloc.ToString("N3"), tSpent.ToString("N3"),
        tRes.ToString("N3"), tAvail.ToString("N3")}

        'For i As Integer = 0 To totals.Length - 1
        For i As Integer = totals.Length - 1 To 0 Step -1
            Dim r As New Rectangle(x, yPos, widths(i), rowH)
            DrawCellText(g, totals(i), fontHeader, r, If(i >= 2, HorizontalAlignment.Right, HorizontalAlignment.Center))
            g.DrawRectangle(Pens.DarkGray, r)
            x += widths(i)
        Next

        ' Footer
        Dim footerY As Integer = e.MarginBounds.Bottom + 5
        g.DrawString("توقيع المختص: ____________________      اعتماد: ____________________",
                 fontSmall, Brushes.Black, leftX, footerY)

        e.HasMorePages = False
    End Sub



    '===============================
    ' R3 - Items Status Report
    '===============================
    Public Function CreateItemsStatusReport(fiscalYear As Integer, doorId As Integer, chapterId As Integer) As Printing.PrintDocument
        Dim doorInfo = GetDoorInfo(doorId)
        Dim chapterInfo = GetChapterInfo(chapterId)
        Dim dt As DataTable = GetItemsStatusData(fiscalYear, chapterId)

        Dim doc As New Printing.PrintDocument()
        doc.DefaultPageSettings.Landscape = True
        doc.DefaultPageSettings.Margins = New Printing.Margins(30, 30, 25, 25)

        AddHandler doc.PrintPage,
        Sub(sender, e)
            PrintItemsStatus(e, dt, fiscalYear, doorInfo, chapterInfo)
        End Sub

        Return doc
    End Function

    Private Function GetChapterInfo(chapterId As Integer) As DataRow
        Dim dt As New DataTable()

        Using cn As New SqlClient.SqlConnection(ConnStr)
            Using cmd As New SqlClient.SqlCommand("
SELECT ChapterCode, ChapterName
FROM Budget_Chapters
WHERE ChapterId = @Id;", cn)

                cmd.Parameters.AddWithValue("@Id", chapterId)
                Using da As New SqlClient.SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        If dt.Rows.Count = 0 Then Throw New Exception("الفصل غير موجود")
        Return dt.Rows(0)
    End Function

    Private Function GetItemsStatusData(y As Integer, chapterId As Integer) As DataTable
        Dim dt As New DataTable()

        Using cn As New SqlClient.SqlConnection(ConnStr)
            Using cmd As New SqlClient.SqlCommand("
SELECT
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
LEFT JOIN dbo.Vw_BudgetItemSummary s
    ON s.BudgetItemId = i.BudgetItemId
   AND s.FiscalYear = @Y

WHERE i.ChapterId = @ChapterId
  AND i.IsActive = 1

ORDER BY 
    i.ItemCode;", cn)

                cmd.Parameters.AddWithValue("@Y", y)
                cmd.Parameters.AddWithValue("@ChapterId", chapterId)

                Using da As New SqlClient.SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    '    Private Function GetItemsStatusData(y As Integer, chapterId As Integer) As DataTable
    '        Dim dt As New DataTable()

    '        Using cn As New SqlClient.SqlConnection(ConnStr)
    '            Using cmd As New SqlClient.SqlCommand("
    'SELECT
    '    i.ItemCode,
    '    i.ItemName,
    '    ISNULL(a.AllocatedAmount,0) AS Allocated,
    '    ISNULL(SUM(CASE WHEN e.EntryType=1 THEN e.Amount ELSE 0 END),0) AS Spent,
    '    ISNULL(SUM(CASE WHEN e.EntryType=2 THEN e.Amount ELSE 0 END),0) AS Reserved
    'FROM Budget_Items i
    'LEFT JOIN Budget_Allocations a
    '    ON a.BudgetItemId = i.BudgetItemId AND a.FiscalYear = @Y
    'LEFT JOIN Budget_Entries e
    '    ON e.BudgetItemId = i.BudgetItemId AND e.FiscalYear = @Y
    'WHERE i.ChapterId = @ChapterId
    'GROUP BY i.ItemCode, i.ItemName, a.AllocatedAmount
    'ORDER BY i.ItemCode;", cn)

    '                cmd.Parameters.AddWithValue("@Y", y)
    '                cmd.Parameters.AddWithValue("@ChapterId", chapterId)

    '                Using da As New SqlClient.SqlDataAdapter(cmd)
    '                    da.Fill(dt)
    '                End Using
    '            End Using
    '        End Using

    '        dt.Columns.Add("Available", GetType(Decimal))
    '        For Each r As DataRow In dt.Rows
    '            Dim a As Decimal = CDec(r("Allocated"))
    '            Dim s As Decimal = CDec(r("Spent"))
    '            Dim rsv As Decimal = CDec(r("Reserved"))
    '            r("Available") = a - s - rsv
    '        Next

    '        Return dt
    '    End Function

    Private Sub PrintItemsStatus(e As Printing.PrintPageEventArgs,
                            dt As DataTable,
                            fiscalYear As Integer,
                            doorInfo As DataRow,
                            chapterInfo As DataRow)

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Dim fontTitle As New Font("Segoe UI", 14, FontStyle.Bold)
        Dim fontHeader As New Font("Segoe UI", 10, FontStyle.Bold)
        Dim fontBody As New Font("Segoe UI", 10)
        Dim fontSmall As New Font("Segoe UI", 9)

        Dim leftX As Integer = e.MarginBounds.Left
        Dim yPos As Integer = e.MarginBounds.Top

        ' ===== Header =====

        yPos = DrawReportHeader(
    g,
    e,
   "تقرير موقف البنود ",
    fiscalYear
)

        'g.DrawString("تقرير موقف البنود - السنة " & fiscalYear, fontTitle, Brushes.Black, leftX, yPos)
        'yPos += 30

        g.DrawString(
        "الباب: " & doorInfo("DoorCode") & " - " & doorInfo("DoorName"),
        fontHeader, Brushes.Black, leftX, yPos)
        yPos += 22

        g.DrawString(
        "الفصل: " & chapterInfo("ChapterCode") & " - " & chapterInfo("ChapterName"),
        fontHeader, Brushes.Black, leftX, yPos)
        yPos += 22

        g.DrawString(
        "تاريخ الطباعة: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm") &
        "    المستخدم: " & USER_ID.ToString(),
        fontSmall, Brushes.DimGray, leftX, yPos)
        yPos += 25

        ' ===== Table =====
        Dim rowH As Integer = 28
        Dim headerH As Integer = 30

        Dim colCode As Integer = 100
        Dim colName As Integer = 380
        Dim colMoney As Integer = 140

        Dim widths() As Integer = {colCode, colName, colMoney, colMoney, colMoney, colMoney}
        Dim headers() As String = {"كود البند", "اسم البند", "الاعتماد", "المصروف", "المحجوز", "المتاح"}

        Dim x0 As Integer = leftX
        Dim tableW As Integer = widths.Sum()

        ' Header row
        Dim headerRect As New Rectangle(x0, yPos, tableW, headerH)
        g.FillRectangle(New SolidBrush(Color.FromArgb(240, 242, 245)), headerRect)
        g.DrawRectangle(Pens.DarkGray, headerRect)

        Dim x As Integer = x0
        For i As Integer = headers.Length - 1 To 0 Step -1
            Dim r As New Rectangle(x, yPos, widths(i), headerH)
            DrawCellText(g, headers(i), fontHeader, r, HorizontalAlignment.Center)
            g.DrawRectangle(Pens.DarkGray, r)
            x += widths(i)
        Next
        yPos += headerH

        ' Data rows
        Dim tAlloc, tSpent, tRes, tAvail As Decimal

        For Each dr As DataRow In dt.Rows
            If yPos + rowH > e.MarginBounds.Bottom - 40 Then
                e.HasMorePages = True
                Return
            End If

            Dim cells() As String = {
            dr("ItemCode").ToString(),
            dr("ItemName").ToString(),
            CDec(dr("Allocated")).ToString("N3"),
            CDec(dr("Spent")).ToString("N3"),
            CDec(dr("Reserved")).ToString("N3"),
            CDec(dr("Available")).ToString("N3")
        }

            tAlloc += CDec(dr("Allocated"))
            tSpent += CDec(dr("Spent"))
            tRes += CDec(dr("Reserved"))
            tAvail += CDec(dr("Available"))

            x = x0
            For i As Integer = cells.Length - 1 To 0 Step -1
                Dim r As New Rectangle(x, yPos, widths(i), rowH)
                DrawCellText(g, cells(i), fontBody, r, If(i >= 2, HorizontalAlignment.Right, HorizontalAlignment.Center))
                g.DrawRectangle(Pens.Gainsboro, r)
                x += widths(i)
            Next
            yPos += rowH
        Next

        ' Totals row
        yPos += 5
        x = x0
        Dim totals() As String = {
        "", "الإجمالي",
        tAlloc.ToString("N3"),
        tSpent.ToString("N3"),
        tRes.ToString("N3"),
        tAvail.ToString("N3")
    }

        'For i As Integer = 0 To totals.Length - 1
        For i As Integer = totals.Length - 1 To 0 Step -1
            Dim r As New Rectangle(x, yPos, widths(i), rowH)
            DrawCellText(g, totals(i), fontHeader, r, If(i >= 2, HorizontalAlignment.Right, HorizontalAlignment.Center))
            g.DrawRectangle(Pens.DarkGray, r)
            x += widths(i)
        Next

        ' Footer
        Dim footerY As Integer = e.MarginBounds.Bottom + 5
        g.DrawString("توقيع المختص: ____________________      اعتماد: ____________________",
                 fontSmall, Brushes.Black, leftX, footerY)

        e.HasMorePages = False
    End Sub


    '===============================
    ' R4 - Item Ledger Report
    '===============================
    Public Function CreateItemLedgerReport(fiscalYear As Integer,
                                       doorId As Integer,
                                       chapterId As Integer,
                                       itemId As Integer) As Printing.PrintDocument

        CurrentPage = 0

        Dim doorInfo = GetDoorInfo(doorId)
        Dim chapterInfo = GetChapterInfo(chapterId)
        Dim itemInfo = GetItemInfo(itemId)

        Dim dt As DataTable = GetItemLedgerData(fiscalYear, itemId)
        Dim ledgerRowIndex As Integer = 0

        Dim doc As New Printing.PrintDocument()
        doc.DefaultPageSettings.Landscape = True
        doc.DefaultPageSettings.Margins = New Printing.Margins(30, 30, 25, 25)
        Dim ledgerTotalPages As Integer = CalculateItemLedgerTotalPages(dt.Rows.Count + 4, doc.DefaultPageSettings)
        Dim ledgerPageNumber As Integer = 0
        Dim printedAt As DateTime = DateTime.Now

        '' نحسب عدد الصفحات أولًا
        'CalculateTotalPages(doc)

        'AddHandler doc.PrintPage,
        '    Sub(sender, e)
        '        CurrentPage += 1
        '        PrintItemLedger(e, dt, fiscalYear, doorInfo, chapterInfo, itemInfo)
        '        DrawPageFooter(e)
        '    End Sub

        AddHandler doc.BeginPrint,
            Sub(sender, e)
                ledgerRowIndex = 0
                ledgerPageNumber = 0
            End Sub

        AddHandler doc.PrintPage,
            Sub(sender, e)
                ledgerPageNumber += 1
                PrintItemLedger(e, dt, fiscalYear, doorInfo, chapterInfo, itemInfo, ledgerRowIndex, ledgerPageNumber, ledgerTotalPages, printedAt)
            End Sub

        Return doc
    End Function

    Private Function CalculateItemLedgerTotalPages(rowCount As Integer, pageSettings As Printing.PageSettings) As Integer
        Dim rowsPerPage As Integer = CalculateItemLedgerRowsPerPage(pageSettings)
        If rowCount <= 0 Then Return 1
        Return Math.Max(1, CInt(Math.Ceiling(rowCount / CDbl(rowsPerPage))))
    End Function

    Private Function CalculateItemLedgerRowsPerPage(pageSettings As Printing.PageSettings) As Integer
        Dim pageHeight As Integer = pageSettings.Bounds.Height
        If pageSettings.Landscape Then
            pageHeight = Math.Min(pageSettings.Bounds.Width, pageSettings.Bounds.Height)
        Else
            pageHeight = Math.Max(pageSettings.Bounds.Width, pageSettings.Bounds.Height)
        End If

        Dim yPos As Integer = pageSettings.Margins.Top
        yPos += 25 + 22 + 30 + 10   ' DrawReportHeader
        yPos += 20 + 20 + 25        ' Door, chapter, item
        yPos += 30                  ' Table header

        Dim usableBottom As Integer = pageHeight - pageSettings.Margins.Bottom - 40
        Dim rowH As Integer = 28
        Dim availableHeight As Integer = usableBottom - yPos

        Return Math.Max(1, CInt(Math.Floor(availableHeight / CDbl(rowH))))
    End Function

    Private Sub DrawCellTextRtl(g As Graphics,
                                txt As String,
                                f As Font,
                                r As Rectangle,
                                align As HorizontalAlignment,
                                Optional br As Brush = Nothing)
        If br Is Nothing Then br = Brushes.Black

        Dim sf As New StringFormat()
        sf.LineAlignment = StringAlignment.Center
        sf.Trimming = StringTrimming.EllipsisCharacter
        sf.FormatFlags = StringFormatFlags.NoWrap

        Select Case align
            Case HorizontalAlignment.Right
                sf.Alignment = StringAlignment.Far
            Case HorizontalAlignment.Center
                sf.Alignment = StringAlignment.Center
            Case Else
                sf.Alignment = StringAlignment.Near
        End Select

        g.DrawString(txt, f, br, r, sf)
    End Sub



    Private Function GetItemInfo(itemId As Integer) As DataRow
        Dim dt As New DataTable()

        Using cn As New SqlClient.SqlConnection(ConnStr)
            Using cmd As New SqlClient.SqlCommand("
SELECT ItemCode, ItemName
FROM Budget_Items
WHERE BudgetItemId = @Id;", cn)

                cmd.Parameters.AddWithValue("@Id", itemId)
                Using da As New SqlClient.SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        If dt.Rows.Count = 0 Then Throw New Exception("البند غير موجود")
        Return dt.Rows(0)
    End Function


    Private Function GetItemLedgerData(y As Integer, itemId As Integer) As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("EntryDate", GetType(Date))
        dt.Columns.Add("MovementType", GetType(String))
        dt.Columns.Add("Notes", GetType(String))
        dt.Columns.Add("SignedAmount", GetType(Decimal))
        dt.Columns.Add("Balance", GetType(Decimal))

        Dim runningBalance As Decimal = 0D

        ' رصيد بداية السنة قبل حركات الاعتماد
        Dim openRow = dt.NewRow()
        openRow("EntryDate") = DateSerial(y, 1, 1)
        openRow("MovementType") = "بداية السنة"
        openRow("Notes") = "الرصيد قبل تسجيل حركات الاعتماد والصرف"
        openRow("SignedAmount") = 0D
        openRow("Balance") = runningBalance
        dt.Rows.Add(openRow)

        Using cn As New SqlClient.SqlConnection(ConnStr)
            Using cmd As New SqlClient.SqlCommand("
SELECT
    EntryDate,
    MovementType,
    Notes,
    SignedAmount,
    SortOrder
FROM
(
    /* حركات الاعتماد */
    SELECT
        EntryDate = m.MovementDate,
        MovementType = t.TypeName,
        Notes =
            ISNULL(m.DecisionNo, N'') +
            CASE WHEN ISNULL(m.Reason, N'') <> N'' THEN N' - ' + m.Reason ELSE N'' END,
        SignedAmount =
            CASE
                WHEN t.Direction = 1 THEN m.Amount
                WHEN t.Direction = -1 THEN -m.Amount
                ELSE 0
            END,
        SortOrder = 1
    FROM dbo.Budget_AllocationMovements m
    INNER JOIN dbo.Budget_AllocationTypes t
        ON t.AllocationTypeId = m.AllocationTypeId
    WHERE m.FiscalYear = @Y
      AND m.BudgetItemId = @ItemId
      AND m.StatusId = 1

    UNION ALL

    /* الصرف */
    SELECT
        EntryDate = e.EntryDate,
        MovementType = N'صرف',
        Notes = ISNULL(e.SpendStatement, ISNULL(e.Notes, N'')),
        SignedAmount = -e.Amount,
        SortOrder = 2
    FROM dbo.Budget_Entries e
    WHERE e.FiscalYear = @Y
      AND e.BudgetItemId = @ItemId
      AND e.EntryType = 1
      AND e.StatusId = 1

    UNION ALL

    /* الحجز: نعرض الحجز النشط والمغلق لكي تظهر الحركة تاريخيًا */
    SELECT
        EntryDate = e.EntryDate,
        MovementType =
            CASE 
                WHEN e.StatusId = 3 THEN N'حجز مغلق'
                ELSE N'حجز'
            END,
        Notes = ISNULL(e.Notes, N''),
        SignedAmount = -e.Amount,
        SortOrder = 3
    FROM dbo.Budget_Entries e
    WHERE e.FiscalYear = @Y
      AND e.BudgetItemId = @ItemId
      AND e.EntryType = 2
      AND e.StatusId IN (1, 3)

    UNION ALL

    /* فك الحجز */
    SELECT
        EntryDate = e.EntryDate,
        MovementType = N'فك حجز',
        Notes = ISNULL(e.Notes, N''),
        SignedAmount = e.Amount,
        SortOrder = 4
    FROM dbo.Budget_Entries e
    WHERE e.FiscalYear = @Y
      AND e.BudgetItemId = @ItemId
      AND e.EntryType = 3
      AND e.StatusId = 1
) x
ORDER BY
    EntryDate,
    SortOrder;", cn)

                cmd.Parameters.AddWithValue("@Y", y)
                cmd.Parameters.AddWithValue("@ItemId", itemId)

                Using da As New SqlClient.SqlDataAdapter(cmd)
                    Dim temp As New DataTable()
                    da.Fill(temp)

                    For Each r As DataRow In temp.Rows
                        Dim row = dt.NewRow()

                        Dim signedAmount As Decimal = Convert.ToDecimal(r("SignedAmount"))
                        runningBalance += signedAmount

                        row("EntryDate") = Convert.ToDateTime(r("EntryDate"))
                        row("MovementType") = Convert.ToString(r("MovementType"))
                        row("Notes") = Convert.ToString(r("Notes"))
                        row("SignedAmount") = signedAmount
                        row("Balance") = runningBalance

                        dt.Rows.Add(row)
                    Next
                End Using
            End Using
        End Using

        Return dt
    End Function

    '    Private Function GetItemLedgerData(y As Integer, itemId As Integer) As DataTable
    '        Dim dt As New DataTable()

    '        ' الأعمدة الأساسية
    '        dt.Columns.Add("EntryDate", GetType(Date))
    '        dt.Columns.Add("MovementType", GetType(String))
    '        dt.Columns.Add("Notes", GetType(String))
    '        dt.Columns.Add("SignedAmount", GetType(Decimal))
    '        dt.Columns.Add("Balance", GetType(Decimal))

    '        ' ===== الرصيد الافتتاحي =====
    '        Dim openingBalance As Decimal = GetOpeningBalance(y, itemId)

    '        Dim openRow = dt.NewRow()
    '        openRow("EntryDate") = DateSerial(y, 1, 1)
    '        openRow("MovementType") = "رصيد افتتاحي"
    '        openRow("Notes") = ""
    '        openRow("SignedAmount") = 0D
    '        openRow("Balance") = openingBalance
    '        dt.Rows.Add(openRow)

    '        Dim runningBalance As Decimal = openingBalance


    '        Using cn As New SqlClient.SqlConnection(ConnStr)
    '            Using cmd As New SqlClient.SqlCommand("
    'SELECT EntryDate, EntryType, Amount, Notes
    'FROM Budget_Entries
    'WHERE FiscalYear = @Y AND BudgetItemId = @ItemId
    'ORDER BY EntryDate;", cn)

    '                cmd.Parameters.AddWithValue("@Y", y)
    '                cmd.Parameters.AddWithValue("@ItemId", itemId)

    '                Using da As New SqlClient.SqlDataAdapter(cmd)
    '                    Dim temp As New DataTable()
    '                    da.Fill(temp)

    '                    For Each r As DataRow In temp.Rows
    '                        Dim row = dt.NewRow()
    '                        Dim amt As Decimal = CDec(r("Amount"))
    '                        Dim entryType As Integer = CInt(r("EntryType"))

    '                        row("EntryDate") = CDate(r("EntryDate"))

    '                        Select Case entryType
    '                            Case 1
    '                                row("MovementType") = "صرف"
    '                                row("SignedAmount") = -amt
    '                                runningBalance -= amt

    '                            Case 2
    '                                row("MovementType") = "حجز"
    '                                row("SignedAmount") = -amt
    '                                runningBalance -= amt

    '                            Case 3
    '                                row("MovementType") = "فك حجز"
    '                                row("SignedAmount") = amt
    '                                runningBalance += amt
    '                        End Select

    '                        row("Notes") = r("Notes").ToString()
    '                        row("Balance") = runningBalance

    '                        dt.Rows.Add(row)
    '                    Next
    '                End Using
    '            End Using
    '        End Using

    '        Return dt
    '    End Function




    Private Sub PrintItemLedger(e As Printing.PrintPageEventArgs,
                            dt As DataTable,
                            fiscalYear As Integer,
                            doorInfo As DataRow,
                            chapterInfo As DataRow,
                            itemInfo As DataRow,
                            ByRef ledgerRowIndex As Integer,
                            pageNumber As Integer,
                            totalPages As Integer,
                            printedAt As DateTime)

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Dim fontTitle As New Font("Segoe UI", 14, FontStyle.Bold)
        Dim fontHeader As New Font("Segoe UI", 10, FontStyle.Bold)
        Dim fontBody As New Font("Segoe UI", 10)
        Dim fontSmall As New Font("Segoe UI", 9)

        Dim leftX As Integer = e.MarginBounds.Left
        Dim yPos As Integer = e.MarginBounds.Top

        ' ===== Header =====
        'g.DrawString("تقرير حركة بند - السنة " & fiscalYear, fontTitle, Brushes.Black, leftX, yPos)
        'yPos += 30

        yPos = DrawReportHeader(
    g,
    e,
    "تقرير حركة بند",
    fiscalYear
)

        Dim sfRight As New StringFormat()
        sfRight.Alignment = StringAlignment.Far
        sfRight.LineAlignment = StringAlignment.Center

        g.DrawString("الباب: " & doorInfo("DoorCode") & " - " & doorInfo("DoorName"),
                 fontHeader, Brushes.Black, New RectangleF(leftX, yPos, e.MarginBounds.Width, 20), sfRight)
        yPos += 20

        g.DrawString("الفصل: " & chapterInfo("ChapterCode") & " - " & chapterInfo("ChapterName"),
                 fontHeader, Brushes.Black, New RectangleF(leftX, yPos, e.MarginBounds.Width, 20), sfRight)
        yPos += 20

        g.DrawString("البند: " & itemInfo("ItemCode") & " - " & itemInfo("ItemName"),
                 fontHeader, Brushes.Black, New RectangleF(leftX, yPos, e.MarginBounds.Width, 22), sfRight)
        yPos += 25

        ' ===== Table =====
        Dim rowH As Integer = 28
        Dim headerH As Integer = 30

        Dim widths() As Integer = {55, 110, 110, 365, 140, 140}
        Dim headers() As String = {"#", "التاريخ", "النوع", "البيان", "المبلغ", "الرصيد"}

        Dim x0 As Integer = leftX
        Dim tableW As Integer = widths.Sum()

        ' Header row
        Dim headerRect As New Rectangle(x0, yPos, tableW, headerH)
        g.FillRectangle(New SolidBrush(Color.FromArgb(240, 242, 245)), headerRect)
        g.DrawRectangle(Pens.DarkGray, headerRect)

        Dim x As Integer = x0
        For i As Integer = headers.Length - 1 To 0 Step -1
            Dim r As New Rectangle(x, yPos, widths(i), headerH)
            DrawCellTextRtl(g, headers(i), fontHeader, r, HorizontalAlignment.Center)
            g.DrawRectangle(Pens.DarkGray, r)
            x += widths(i)
        Next
        yPos += headerH

        ' Rows
        While ledgerRowIndex < dt.Rows.Count
            If yPos + rowH > e.MarginBounds.Bottom - 40 Then
                DrawItemLedgerFooter(e, fontSmall, leftX, pageNumber, totalPages, printedAt)
                e.HasMorePages = True
                Return
            End If

            Dim dr As DataRow = dt.Rows(ledgerRowIndex)
            Dim cells() As String = {
            (ledgerRowIndex + 1).ToString(),
            CDate(dr("EntryDate")).ToString("yyyy-MM-dd"),
            dr("MovementType").ToString(),
            dr("Notes").ToString(),
            CDec(dr("SignedAmount")).ToString("N3"),
            CDec(dr("Balance")).ToString("N3")
        }

            x = x0
            'For i As Integer = cells.Length - 1 To 0 Step -1
            '    Dim r As New Rectangle(x, yPos, widths(i), rowH)
            '    DrawCellText(g, cells(i), fontBody, r,
            '             If(i >= 3, HorizontalAlignment.Right, HorizontalAlignment.Center))
            '    g.DrawRectangle(Pens.Gainsboro, r)
            '    x += widths(i)
            'Next

            Dim movementType As String = dr("MovementType").ToString()
            Dim rowBrush As Brush = GetMovementBrush(movementType)

            For i As Integer = cells.Length - 1 To 0 Step -1
                Dim r As New Rectangle(x, yPos, widths(i), rowH)

                ' نلوّن كل الصف بلون الحركة
                DrawCellTextRtl(
        g,
        cells(i),
        fontBody,
        r,
        If(i = 3, HorizontalAlignment.Right, HorizontalAlignment.Center),
        rowBrush
    )

                g.DrawRectangle(Pens.Gainsboro, r)
                x += widths(i)
            Next


            yPos += rowH
            ledgerRowIndex += 1
        End While

        If yPos + 112 > e.MarginBounds.Bottom - 40 Then
            DrawItemLedgerFooter(e, fontSmall, leftX, pageNumber, totalPages, printedAt)
            e.HasMorePages = True
            Return
        End If

        DrawItemLedgerTotals(g, dt, fontHeader, fontBody, leftX, yPos, tableW)

        DrawItemLedgerFooter(e, fontSmall, leftX, pageNumber, totalPages, printedAt)

        e.HasMorePages = False
    End Sub

    Private Sub DrawItemLedgerFooter(e As Printing.PrintPageEventArgs,
                                     fontSmall As Font,
                                     leftX As Integer,
                                     pageNumber As Integer,
                                     totalPages As Integer,
                                     printedAt As DateTime)
        Dim g As Graphics = e.Graphics
        Dim footerY As Integer = e.MarginBounds.Bottom + 5

        g.DrawString("توقيع المختص: ____________________      اعتماد: ____________________",
                     fontSmall, Brushes.Black, leftX, footerY)

        Dim sfCenter As New StringFormat()
        sfCenter.Alignment = StringAlignment.Center
        sfCenter.LineAlignment = StringAlignment.Center

        Dim pageText As String = "صفحة " & pageNumber.ToString() & " من " & totalPages.ToString()
        g.DrawString(pageText, fontSmall, Brushes.DimGray,
                     New RectangleF(e.MarginBounds.Left, footerY, e.MarginBounds.Width, 20), sfCenter)

        Dim sfRight As New StringFormat()
        sfRight.Alignment = StringAlignment.Far
        sfRight.LineAlignment = StringAlignment.Center

        Dim printInfo As String = "تاريخ الطباعة: " & printedAt.ToString("yyyy-MM-dd HH:mm") &
                                  "    المستخدم: " & USER_ID.ToString()
        g.DrawString(printInfo, fontSmall, Brushes.DimGray,
                     New RectangleF(e.MarginBounds.Left, footerY + 18, e.MarginBounds.Width, 20), sfRight)
    End Sub

    Private Sub DrawItemLedgerTotals(g As Graphics,
                                     dt As DataTable,
                                     fontHeader As Font,
                                     fontBody As Font,
                                     leftX As Integer,
                                     yPos As Integer,
                                     tableW As Integer)
        Dim totalPositive As Decimal = 0D
        Dim totalNegative As Decimal = 0D
        Dim netAmount As Decimal = 0D
        Dim finalBalance As Decimal = 0D

        For Each dr As DataRow In dt.Rows
            Dim signedAmount As Decimal = Convert.ToDecimal(dr("SignedAmount"))
            netAmount += signedAmount

            If signedAmount > 0D Then
                totalPositive += signedAmount
            ElseIf signedAmount < 0D Then
                totalNegative += Math.Abs(signedAmount)
            End If

            finalBalance = Convert.ToDecimal(dr("Balance"))
        Next

        yPos += 10
        Dim titleH As Integer = 28
        Dim rowH As Integer = 26
        Dim boxH As Integer = titleH + (rowH * 4)
        Dim boxRect As New Rectangle(leftX, yPos, tableW, boxH)

        g.FillRectangle(New SolidBrush(Color.FromArgb(248, 250, 252)), boxRect)
        g.DrawRectangle(Pens.DarkGray, boxRect)

        Dim titleRect As New Rectangle(leftX, yPos, tableW, titleH)
        g.FillRectangle(New SolidBrush(Color.FromArgb(226, 232, 240)), titleRect)
        DrawCellTextRtl(g, "إجماليات حركة البند", fontHeader, titleRect, HorizontalAlignment.Center)
        g.DrawRectangle(Pens.DarkGray, titleRect)

        yPos += titleH

        Dim labels() As String = {
            "عدد الصفوف: " & dt.Rows.Count.ToString(),
            "إجمالي الإضافات: " & totalPositive.ToString("N3"),
            "إجمالي الخصومات / الصرف: " & totalNegative.ToString("N3"),
            "صافي الحركة: " & netAmount.ToString("N3") & "    الرصيد النهائي: " & finalBalance.ToString("N3")
        }

        For Each line As String In labels
            Dim r As New Rectangle(leftX, yPos, tableW, rowH)
            DrawCellTextRtl(g, line, fontBody, r, HorizontalAlignment.Right)
            g.DrawRectangle(Pens.Gainsboro, r)
            yPos += rowH
        Next
    End Sub


    '    Private Function GetOpeningBalance(fiscalYear As Integer, itemId As Integer) As Decimal
    '        Dim available As Decimal = 0D

    '        Using cn As New SqlClient.SqlConnection(ConnStr)
    '            cn.Open()

    '            Using cmd As New SqlClient.SqlCommand("
    'SELECT ISNULL(Available, 0)
    'FROM dbo.Vw_BudgetItemSummary
    'WHERE FiscalYear = @Y
    '  AND BudgetItemId = @ItemId;", cn)

    '                cmd.Parameters.AddWithValue("@Y", fiscalYear)
    '                cmd.Parameters.AddWithValue("@ItemId", itemId)

    '                Dim obj = cmd.ExecuteScalar()

    '                If obj IsNot Nothing AndAlso obj IsNot DBNull.Value Then
    '                    available = Convert.ToDecimal(obj)
    '                End If
    '            End Using
    '        End Using

    '        Return available
    '    End Function

    '    Private Function GetOpeningBalance(fiscalYear As Integer, itemId As Integer) As Decimal
    '        Dim allocated As Decimal = 0D
    '        Dim spent As Decimal = 0D
    '        Dim reserved As Decimal = 0D

    '        Using cn As New SqlClient.SqlConnection(ConnStr)
    '            cn.Open()

    '            ' الاعتماد
    '            Using cmd As New SqlClient.SqlCommand("
    'SELECT ISNULL(AllocatedAmount,0)
    'FROM Budget_Allocations
    'WHERE FiscalYear = @Y AND BudgetItemId = @ItemId;", cn)

    '                cmd.Parameters.AddWithValue("@Y", fiscalYear)
    '                cmd.Parameters.AddWithValue("@ItemId", itemId)
    '                allocated = CDec(cmd.ExecuteScalar())
    '            End Using

    '            ' المصروف السابق
    '            Using cmd As New SqlClient.SqlCommand("
    'SELECT ISNULL(SUM(Amount),0)
    'FROM Budget_Entries
    'WHERE FiscalYear = @Y AND BudgetItemId = @ItemId AND EntryType = 1;", cn)

    '                cmd.Parameters.AddWithValue("@Y", fiscalYear)
    '                cmd.Parameters.AddWithValue("@ItemId", itemId)
    '                spent = CDec(cmd.ExecuteScalar())
    '            End Using

    '            ' المحجوز السابق
    '            Using cmd As New SqlClient.SqlCommand("
    'SELECT ISNULL(SUM(Amount),0)
    'FROM Budget_Entries
    'WHERE FiscalYear = @Y AND BudgetItemId = @ItemId AND EntryType = 2;", cn)

    '                cmd.Parameters.AddWithValue("@Y", fiscalYear)
    '                cmd.Parameters.AddWithValue("@ItemId", itemId)
    '                reserved = CDec(cmd.ExecuteScalar())
    '            End Using
    '        End Using

    '        Return allocated - spent - reserved
    '    End Function


    'Private Function GetMovementBrush(movementType As String) As Brush
    '    Select Case movementType
    '        Case "صرف"
    '            Return Brushes.DarkRed
    '        Case "فك حجز"
    '            Return Brushes.DarkGreen
    '        Case "رصيد افتتاحي"
    '            Return Brushes.DarkBlue
    '        Case "حجز"
    '            Return Brushes.DarkOrange
    '        Case Else
    '            Return Brushes.Black
    '    End Select
    'End Function

    Private Function GetMovementBrush(movementType As String) As Brush
        Select Case movementType
            Case "اعتماد أصلي", "اعتماد إضافي", "اعتماد مرحل", "اعتماد احتياطي / طارئ", "مناقلة واردة", "فك حجز"
                Return Brushes.DarkGreen

            Case "تخفيض اعتماد", "مناقلة صادرة", "صرف"
                Return Brushes.DarkRed

            Case "حجز", "حجز مغلق"
                Return Brushes.DarkOrange

            Case "بداية السنة", "رصيد افتتاحي"
                Return Brushes.DarkBlue

            Case Else
                Return Brushes.Black
        End Select
    End Function

    Public Function DrawReportHeader(g As Graphics,
                                  e As Printing.PrintPageEventArgs,
                                  reportTitle As String,
                                  fiscalYear As Integer) As Integer
        Dim marginLeft = e.MarginBounds.Left
        Dim marginRight = e.MarginBounds.Right
        Dim y As Integer = e.MarginBounds.Top

        Dim fontOrg As New Font("Segoe UI", 12, FontStyle.Bold)
        Dim fontDept As New Font("Segoe UI", 10, FontStyle.Regular)
        Dim fontTitle As New Font("Segoe UI", 11, FontStyle.Bold)

        ' ===== Logo (يسار الصفحة) =====
        If IO.File.Exists(LogoPath) Then
            Using img As Image = Image.FromFile(LogoPath)
                g.DrawImage(img, marginLeft, y, 90, 70)
            End Using
        End If

        ' ===== نص الجهة (يمين الصفحة) =====
        Dim textRightX As Integer = marginRight

        Dim sfRight As New StringFormat()
        sfRight.Alignment = StringAlignment.Far
        sfRight.LineAlignment = StringAlignment.Near

        g.DrawString(OrgName, fontOrg, Brushes.Black,
                 New RectangleF(marginLeft, y, e.MarginBounds.Width, 25), sfRight)
        y += 25

        g.DrawString(OrgDept, fontDept, Brushes.Black,
                 New RectangleF(marginLeft, y, e.MarginBounds.Width, 22), sfRight)
        y += 22

        g.DrawString(reportTitle & " - السنة " & fiscalYear,
                 fontTitle, Brushes.Black,
                 New RectangleF(marginLeft, y, e.MarginBounds.Width, 24), sfRight)
        y += 30

        ' ===== خط فاصل =====
        g.DrawLine(Pens.DarkGray, marginLeft, y, marginRight, y)
        y += 10

        Return y   ' نُرجع Y لبداية محتوى التقرير
    End Function



    Private Sub CalculateTotalPages(doc As Printing.PrintDocument)
        TotalPages = 0

        AddHandler doc.PrintPage,
        Sub(sender, e)
            TotalPages += 1
            e.HasMorePages = False
        End Sub

        ' طباعة وهمية على MemoryStream
        Using bmp As New Bitmap(1, 1)
            Using g As Graphics = Graphics.FromImage(bmp)
                Dim args As New Printing.PrintPageEventArgs(
                g,
                New Rectangle(0, 0, 1, 1),
                doc.DefaultPageSettings.Bounds,
                doc.DefaultPageSettings)

                doc.PrintController = New Printing.StandardPrintController()
                doc.Print()
            End Using
        End Using

        RemoveHandler doc.PrintPage, Nothing
    End Sub


    Private Sub DrawPageFooter(e As Printing.PrintPageEventArgs)
        Dim g As Graphics = e.Graphics

        Dim fontFooter As New Font("Segoe UI", 9, FontStyle.Regular)

        Dim pageText As String =
        $"Page {CurrentPage} of {TotalPages}"

        Dim sf As New StringFormat()
        sf.Alignment = StringAlignment.Center

        Dim footerRect As New RectangleF(
        e.MarginBounds.Left,
        e.MarginBounds.Bottom + 15,
        e.MarginBounds.Width,
        20)

        g.DrawString(pageText, fontFooter, Brushes.DimGray, footerRect, sf)
    End Sub


End Module

