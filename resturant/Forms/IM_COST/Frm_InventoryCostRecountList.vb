Imports System.Data.SqlClient

Public Class Frm_InventoryCostRecountList

    Private ConnectionString As String = MY_Settings.SqlConStr

    Private Sub Frm_InventoryCostRecountList_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load
        ' تطبيق الثيم الإجباري
        ThemeManager.ApplyThemeToForm(Me)
        LoadStatus()
        LoadData()

    End Sub

    Private Sub LoadStatus()

        Cmb_Status.Items.Clear()

        Cmb_Status.Items.Add("")
        Cmb_Status.Items.Add("Draft")
        Cmb_Status.Items.Add("Posted")
        Cmb_Status.Items.Add("Reversed")

        Cmb_Status.SelectedIndex = 0

    End Sub

    Private Sub LoadData()

        Try

            Using cn As New SqlConnection(ConnectionString)

                Dim sql As String = "
SELECT
    b.BatchId,
    b.IM_ID,
    ISNULL(im.item_name, '') AS ItemName,
    b.FromDate,
    b.OldPurchaseCost,
    b.NewPurchaseCost,
    b.CostDiff,
    b.TotalImpact,
    b.InventoryImpact,
    b.COGSImpact,
    b.ExpenseImpact,
    b.Status,
    b.CreatedAt,
    b.PostedAt
FROM dbo.InventoryCostRecountBatch b
LEFT JOIN dbo.IM_MENU im ON im.IM_ID = b.IM_ID
WHERE 1 = 1
"

                If Txt_IM_ID.Text.Trim() <> "" Then

                    sql &= " AND b.IM_ID = @IM_ID "

                End If

                If Cmb_Status.Text.Trim() <> "" Then

                    sql &= " AND Status = @Status "

                End If

                sql &= "
AND CAST(CreatedAt AS date)
BETWEEN @FromDate AND @ToDate
"

                sql &= "
ORDER BY CreatedAt DESC
"

                Using cmd As New SqlCommand(sql, cn)

                    If Txt_IM_ID.Text.Trim() <> "" Then

                        cmd.Parameters.AddWithValue(
                            "@IM_ID",
                            Convert.ToInt32(Txt_IM_ID.Text)
                        )

                    End If

                    If Cmb_Status.Text.Trim() <> "" Then

                        cmd.Parameters.AddWithValue(
                            "@Status",
                            Cmb_Status.Text
                        )

                    End If

                    cmd.Parameters.AddWithValue(
                        "@FromDate",
                        Dtp_From.Value.Date
                    )

                    cmd.Parameters.AddWithValue(
                        "@ToDate",
                        Dtp_To.Value.Date
                    )

                    Dim da As New SqlDataAdapter(cmd)

                    Dim dt As New DataTable()

                    da.Fill(dt)

                    GridBatches.DataSource = dt

                End Using

            End Using

            FormatGrid()

        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    Public Sub RefreshGrid()

        LoadData()

    End Sub

    Private Sub FormatGrid()

        If GridBatches.Columns.Count = 0 Then
            Return
        End If

        Try

            GridBatches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            GridBatches.AllowUserToResizeColumns = True
            GridBatches.ScrollBars = ScrollBars.Both
            GridBatches.ColumnHeadersHeight = 48
            GridBatches.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            GridBatches.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter
            GridBatches.ColumnHeadersDefaultCellStyle.WrapMode =
                DataGridViewTriState.True
            GridBatches.ColumnHeadersDefaultCellStyle.Font =
                New Font("Segoe UI", 9.0!)
            GridBatches.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False
            GridBatches.DefaultCellStyle.Font =
                New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
            GridBatches.RowsDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter
            GridBatches.RowTemplate.Height = 32

            If GridBatches.Columns.Contains("BatchId") Then
                ConfigureGridColumn(
                    "BatchId",
                    "Batch ID",
                    145,
                    DataGridViewContentAlignment.MiddleCenter
                )
            End If

            If GridBatches.Columns.Contains("IM_ID") Then
                GridBatches.Columns("IM_ID").Visible = False
            End If

            If GridBatches.Columns.Contains("ItemName") Then
                ConfigureGridColumn(
                    "ItemName",
                    "الصنف",
                    220,
                    DataGridViewContentAlignment.MiddleRight
                )
            End If

            If GridBatches.Columns.Contains("FromDate") Then
                ConfigureGridColumn(
                    "FromDate",
                    "من تاريخ",
                    115,
                    DataGridViewContentAlignment.MiddleCenter,
                    "yyyy/MM/dd"
                )
            End If

            If GridBatches.Columns.Contains("OldPurchaseCost") Then
                ConfigureGridColumn(
                    "OldPurchaseCost",
                    "التكلفة القديمة",
                    110,
                    DataGridViewContentAlignment.MiddleCenter,
                    "N3"
                )
            End If

            If GridBatches.Columns.Contains("NewPurchaseCost") Then
                ConfigureGridColumn(
                    "NewPurchaseCost",
                    "التكلفة الجديدة",
                    110,
                    DataGridViewContentAlignment.MiddleCenter,
                    "N3"
                )
            End If

            If GridBatches.Columns.Contains("CostDiff") Then
                ConfigureGridColumn(
                    "CostDiff",
                    "فرق التكلفة",
                    105,
                    DataGridViewContentAlignment.MiddleCenter,
                    "N3"
                )
            End If

            If GridBatches.Columns.Contains("TotalImpact") Then
                ConfigureGridColumn(
                    "TotalImpact",
                    "إجمالي الأثر",
                    110,
                    DataGridViewContentAlignment.MiddleCenter,
                    "N3"
                )
            End If

            If GridBatches.Columns.Contains("InventoryImpact") Then
                ConfigureGridColumn(
                    "InventoryImpact",
                    "أثر المخزون",
                    110,
                    DataGridViewContentAlignment.MiddleCenter,
                    "N3"
                )
            End If

            If GridBatches.Columns.Contains("COGSImpact") Then
                ConfigureGridColumn(
                    "COGSImpact",
                    "أثر تكلفة المبيعات",
                    125,
                    DataGridViewContentAlignment.MiddleCenter,
                    "N3"
                )
            End If

            If GridBatches.Columns.Contains("ExpenseImpact") Then
                ConfigureGridColumn(
                    "ExpenseImpact",
                    "أثر المصروفات",
                    115,
                    DataGridViewContentAlignment.MiddleCenter,
                    "N3"
                )
            End If

            If GridBatches.Columns.Contains("Status") Then
                ConfigureGridColumn(
                    "Status",
                    "الحالة",
                    95,
                    DataGridViewContentAlignment.MiddleCenter
                )
            End If

            If GridBatches.Columns.Contains("CreatedAt") Then
                ConfigureGridColumn(
                    "CreatedAt",
                    "تاريخ الإنشاء",
                    145,
                    DataGridViewContentAlignment.MiddleCenter,
                    "yyyy/MM/dd HH:mm"
                )
            End If

            If GridBatches.Columns.Contains("PostedAt") Then
                ConfigureGridColumn(
                    "PostedAt",
                    "تاريخ الترحيل",
                    145,
                    DataGridViewContentAlignment.MiddleCenter,
                    "yyyy/MM/dd HH:mm"
                )
            End If

        Catch
        End Try

    End Sub

    Private Sub ConfigureGridColumn(
        columnName As String,
        headerText As String,
        width As Integer,
        alignment As DataGridViewContentAlignment,
        Optional format As String = ""
    )

        If Not GridBatches.Columns.Contains(columnName) Then
            Return
        End If

        Dim column As DataGridViewColumn = GridBatches.Columns(columnName)

        column.HeaderText = headerText
        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        column.MinimumWidth = Math.Max(70, width - 20)
        column.Width = width
        column.DefaultCellStyle.Alignment = alignment
        column.DefaultCellStyle.WrapMode = DataGridViewTriState.False

        If format.Trim() <> "" Then
            column.DefaultCellStyle.Format = format
        End If

    End Sub

    Private Sub GridBatches_CellFormatting(
        sender As Object,
        e As DataGridViewCellFormattingEventArgs
    ) Handles GridBatches.CellFormatting

        If GridBatches.Columns.Count = 0 Then
            Return
        End If

        If Not GridBatches.Columns.Contains("Status") Then
            Return
        End If

        If e.RowIndex < 0 Then
            Return
        End If

        If GridBatches.Columns(e.ColumnIndex).Name <> "Status" Then
            Return
        End If

        If e.Value Is Nothing OrElse e.Value Is DBNull.Value Then
            Return
        End If

        Select Case e.Value.ToString().Trim().ToUpperInvariant()
            Case "DRAFT"
                e.CellStyle.BackColor = Color.FromArgb(255, 243, 205)
                e.CellStyle.ForeColor = Color.FromArgb(133, 100, 4)
                e.CellStyle.SelectionBackColor = Color.FromArgb(245, 158, 11)
                e.CellStyle.SelectionForeColor = Color.White

            Case "POSTED"
                e.CellStyle.BackColor = Color.FromArgb(209, 250, 229)
                e.CellStyle.ForeColor = Color.FromArgb(6, 95, 70)
                e.CellStyle.SelectionBackColor = Color.FromArgb(16, 185, 129)
                e.CellStyle.SelectionForeColor = Color.White

            Case "REVERSED"
                e.CellStyle.BackColor = Color.FromArgb(254, 226, 226)
                e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27)
                e.CellStyle.SelectionBackColor = Color.FromArgb(239, 68, 68)
                e.CellStyle.SelectionForeColor = Color.White

            Case Else
                e.CellStyle.BackColor = Color.FromArgb(229, 231, 235)
                e.CellStyle.ForeColor = Color.FromArgb(55, 65, 81)
        End Select

        e.CellStyle.Font = New Font("Segoe UI Semibold", 9.0!)

    End Sub

    Private Sub Btn_Search_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Search.Click

        LoadData()

    End Sub

    Private Sub Btn_Open_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Open.Click

        Try

            If GridBatches.Rows.Count = 0 Then
                Return
            End If

            If GridBatches.CurrentRow Is Nothing Then
                Return
            End If

            Dim batchId As Guid =
                Guid.Parse(
                    GridBatches.CurrentRow.Cells("BatchId").Value.ToString()
                )

            Dim frm As New Frm_InventoryCostRecountPreview

            frm.BatchId = batchId

            frm.ShowDialog(Me)

            RefreshGrid()

        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    Private Sub Btn_Close_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Close.Click

        Me.Close()

    End Sub

End Class
