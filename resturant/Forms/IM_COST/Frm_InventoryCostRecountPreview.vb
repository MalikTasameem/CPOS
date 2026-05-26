Imports System.Data.SqlClient

Public Class Frm_InventoryCostRecountPreview

    Public Property BatchId As Guid

    Private ConnectionString As String = MY_Settings.SqlConStr

    Private Sub Frm_InventoryCostRecountPreview_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        LoadBatch()

        FormatImpactGrid()
        FormatJournalGrid()

        GridImpact.RowHeadersVisible = False
        GridJournal.RowHeadersVisible = False
        GridImpact.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
        GridJournal.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
    End Sub

    Private Sub LoadBatch()

        Try

            Using cn As New SqlConnection(ConnectionString)

                Using cmd As New SqlCommand(
                    "dbo.InventoryCostRecount_GetBatch",
                    cn
                )

                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@BatchId", BatchId)

                    Dim da As New SqlDataAdapter(cmd)

                    Dim ds As New DataSet()

                    da.Fill(ds)

                    If ds.Tables.Count = 0 Then
                        Return
                    End If

                    '========================================
                    ' Batch Header
                    '========================================
                    If ds.Tables(0).Rows.Count > 0 Then

                        Dim row = ds.Tables(0).Rows(0)

                        Txt_BatchId.Text =
                            row("BatchId").ToString()

                        Txt_TotalImpact.Text =
                            Convert.ToDecimal(
                                row("TotalImpact")
                            ).ToString("N6")

                        Txt_InventoryImpact.Text =
                            Convert.ToDecimal(
                                row("InventoryImpact")
                            ).ToString("N6")

                        Txt_COGSImpact.Text =
                            Convert.ToDecimal(
                                row("COGSImpact")
                            ).ToString("N6")

                        Txt_ExpenseImpact.Text =
                            Convert.ToDecimal(
                                row("ExpenseImpact")
                            ).ToString("N6")

                    End If

                    '========================================
                    ' Impact Grid
                    '========================================
                    If ds.Tables.Count > 1 Then

                        AddStoreNameColumn(ds.Tables(1))

                        GridImpact.DataSource =
                            ds.Tables(1)

                    End If

                    '========================================
                    ' Journal Grid
                    '========================================
                    If ds.Tables.Count > 2 Then

                        GridJournal.DataSource =
                            ds.Tables(2)

                    End If

                End Using

            End Using

            FormatImpactGrid()
            FormatJournalGrid()

        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub


    Private Sub FormatImpactGrid()

        If GridImpact.Columns.Count = 0 Then Return

        For Each col As DataGridViewColumn In GridImpact.Columns
            col.Visible = False
        Next

        HideColumn(GridImpact, "IM_ID")

        ShowColumn(GridImpact, "SourceDate", "التاريخ", 120)
        ShowColumn(GridImpact, "MovementType", "نوع الحركة", 120)
        ShowColumn(GridImpact, "SourceParentId", "رقم المستند", 100)
        ShowColumn(GridImpact, "StoreName", "المخزن", 130)
        ShowColumn(GridImpact, "Qty", "الكمية", 90)
        ShowColumn(GridImpact, "OldCost", "التكلفة القديمة", 120)
        ShowColumn(GridImpact, "NewCost", "التكلفة الجديدة", 120)
        ShowColumn(GridImpact, "OldTotalCost", "القيمة القديمة", 120)
        ShowColumn(GridImpact, "NewTotalCost", "القيمة الجديدة", 120)
        ShowColumn(GridImpact, "DiffAmount", "الفرق", 110)
        ShowColumn(GridImpact, "Notes", "ملاحظة", 220)

        GridImpact.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        GridImpact.AllowUserToResizeColumns = True

    End Sub

    Private Sub AddStoreNameColumn(dt As DataTable)

        If dt Is Nothing Then Return

        Dim storeIdColumnName As String = ""

        If dt.Columns.Contains("StoreId") Then
            storeIdColumnName = "StoreId"
        ElseIf dt.Columns.Contains("ST_ID") Then
            storeIdColumnName = "ST_ID"
        End If

        If storeIdColumnName = "" Then Return

        If Not dt.Columns.Contains("StoreName") Then
            dt.Columns.Add("StoreName", GetType(String))
        End If

        Dim storeNames As New Dictionary(Of Long, String)()

        Using cn As New SqlConnection(ConnectionString)

            Dim sql As String = "
SELECT
    CAST(ST_ID AS bigint) AS ST_ID,
    CAST(ST_Name AS nvarchar(2500)) AS ST_Name
FROM dbo.STORES
"

            Using cmd As New SqlCommand(sql, cn)

                Dim da As New SqlDataAdapter(cmd)
                Dim storesTable As New DataTable()

                da.Fill(storesTable)

                For Each row As DataRow In storesTable.Rows

                    If row("ST_ID") Is DBNull.Value Then Continue For

                    Dim storeId As Long = CLng(row("ST_ID"))

                    If storeId > 0 AndAlso Not storeNames.ContainsKey(storeId) Then
                        storeNames.Add(storeId, row("ST_Name").ToString())
                    End If

                Next

            End Using

        End Using

        For Each row As DataRow In dt.Rows

            If row(storeIdColumnName) Is DBNull.Value Then Continue For

            Dim storeId As Long = CLng(row(storeIdColumnName))

            If storeNames.ContainsKey(storeId) Then
                row("StoreName") = storeNames(storeId)
            End If

        Next

    End Sub

    Private Sub FormatJournalGrid()

        If GridJournal.Columns.Count = 0 Then Return

        For Each col As DataGridViewColumn In GridJournal.Columns
            col.Visible = False
        Next

        HideColumn(GridJournal, "IM_ID")

        ShowColumn(GridJournal, "AccountEffect", "الحساب المتأثر", 180)
        ShowColumn(GridJournal, "Debit", "مدين", 120)
        ShowColumn(GridJournal, "Credit", "دائن", 120)
        ShowColumn(GridJournal, "MovementType", "نوع الأثر", 120)
        ShowColumn(GridJournal, "LineNote", "البيان", 350)

        GridJournal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

    End Sub

    Private Sub ShowColumn(
    grid As DataGridView,
    columnName As String,
    headerText As String,
    width As Integer
)

        If grid.Columns.Contains(columnName) Then
            grid.Columns(columnName).Visible = True
            grid.Columns(columnName).HeaderText = headerText
            grid.Columns(columnName).Width = width
            grid.Columns(columnName).DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter
            grid.Columns(columnName).HeaderCell.Style.Alignment =
            DataGridViewContentAlignment.MiddleCenter
        End If

    End Sub

    Private Sub HideColumn(grid As DataGridView, columnName As String)

        If grid.Columns.Contains(columnName) Then
            grid.Columns(columnName).Visible = False
        End If

    End Sub


    'Private Sub FormatImpactGrid()

    '    If GridImpact.Columns.Count = 0 Then
    '        Return
    '    End If

    '    Try

    '        If GridImpact.Columns.Contains("ImpactId") Then
    '            GridImpact.Columns("ImpactId").HeaderText = "رقم"
    '        End If

    '        If GridImpact.Columns.Contains("MovementType") Then
    '            GridImpact.Columns("MovementType").HeaderText = "نوع الحركة"
    '        End If

    '        If GridImpact.Columns.Contains("SourceDate") Then
    '            GridImpact.Columns("SourceDate").HeaderText = "التاريخ"
    '        End If

    '        If GridImpact.Columns.Contains("Qty") Then
    '            GridImpact.Columns("Qty").HeaderText = "الكمية"
    '        End If

    '        If GridImpact.Columns.Contains("OldCost") Then
    '            GridImpact.Columns("OldCost").HeaderText = "التكلفة القديمة"
    '        End If

    '        If GridImpact.Columns.Contains("NewCost") Then
    '            GridImpact.Columns("NewCost").HeaderText = "التكلفة الجديدة"
    '        End If

    '        If GridImpact.Columns.Contains("DiffAmount") Then
    '            GridImpact.Columns("DiffAmount").HeaderText = "فرق القيمة"
    '        End If

    '    Catch
    '    End Try

    'End Sub

    'Private Sub FormatJournalGrid()

    '    If GridJournal.Columns.Count = 0 Then
    '        Return
    '    End If

    '    Try

    '        If GridJournal.Columns.Contains("AccountEffect") Then
    '            GridJournal.Columns("AccountEffect").HeaderText = "الحساب"
    '        End If

    '        If GridJournal.Columns.Contains("Debit") Then
    '            GridJournal.Columns("Debit").HeaderText = "مدين"
    '        End If

    '        If GridJournal.Columns.Contains("Credit") Then
    '            GridJournal.Columns("Credit").HeaderText = "دائن"
    '        End If

    '        If GridJournal.Columns.Contains("LineNote") Then
    '            GridJournal.Columns("LineNote").HeaderText = "ملاحظة"
    '        End If

    '    Catch
    '    End Try

    'End Sub

    Private Sub Btn_Post_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Post.Click

        Try

            If MessageBox.Show(
                "هل تريد اعتماد إعادة الاحتساب؟",
                "تأكيد",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) <> DialogResult.Yes Then

                Return

            End If

            Using cn As New SqlConnection(ConnectionString)

                Using cmd As New SqlCommand(
                    "dbo.InventoryCostRecount_Post",
                    cn
                )

                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@BatchId", BatchId)
                    cmd.Parameters.AddWithValue("@PostedBy", USER_ID)

                    cn.Open()

                    cmd.ExecuteNonQuery()

                End Using

            End Using

            MessageBox.Show(
                "تم اعتماد إعادة الاحتساب بنجاح.",
                "نجاح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

            LoadBatch()
            NotifyOwnerListChanged()

        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    Private Sub Btn_Rollback_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Rollback.Click

        Try

            If MessageBox.Show(
                "هل تريد التراجع عن إعادة الاحتساب؟",
                "تأكيد",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            ) <> DialogResult.Yes Then

                Return

            End If

            Using cn As New SqlConnection(ConnectionString)

                Using cmd As New SqlCommand(
                    "dbo.InventoryCostRecount_Rollback",
                    cn
                )

                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@BatchId", BatchId)
                    cmd.Parameters.AddWithValue("@ReversedBy", 1)

                    cn.Open()

                    cmd.ExecuteNonQuery()

                End Using

            End Using

            MessageBox.Show(
                "تم التراجع بنجاح.",
                "نجاح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

            LoadBatch()
            NotifyOwnerListChanged()

        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    Private Sub NotifyOwnerListChanged()

        Dim listForm As Frm_InventoryCostRecountList =
            TryCast(Me.Owner, Frm_InventoryCostRecountList)

        If listForm IsNot Nothing Then
            listForm.RefreshGrid()
        End If

    End Sub

    Private Sub Btn_Close_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Close.Click

        Me.Close()

    End Sub

End Class
