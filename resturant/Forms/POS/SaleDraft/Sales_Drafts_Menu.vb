Public Class Sales_Drafts_Menu

    Private DraftsTable As DataTable

    Private Sub Sales_Drafts_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitSearchBox()
        LoadDraftsList()
    End Sub

    Private Sub InitSearchBox()
        txtSearch.Text = "اكتب للبحث..."
        txtSearch.ForeColor = Color.Gray
    End Sub

    Private Sub LoadDraftsList()

        Dim drafts = Sales_Fast_Draft.DraftManager.GetAllDrafts()

        DraftsTable = New DataTable()
        DraftsTable.Columns.Add("DraftId", GetType(String))
        DraftsTable.Columns.Add("DraftName", GetType(String))
        DraftsTable.Columns.Add("CustomerName", GetType(String))
        DraftsTable.Columns.Add("Date", GetType(DateTime))
        DraftsTable.Columns.Add("Total", GetType(Decimal))
        DraftsTable.Columns.Add("ItemsCount", GetType(Integer))
        DraftsTable.Columns.Add("UpdatedAt", GetType(DateTime))

        For Each d In drafts
            DraftsTable.Rows.Add(
                d.DraftId,
                d.DraftName,
                d.CustomerName,
                d.Date,
                d.Total,
                d.Items.Count,
                d.UpdatedAt
            )
        Next

        dgvDrafts.DataSource = DraftsTable

        FormatGrid()
        UpdateDraftsCount()

    End Sub

    Private Sub FormatGrid()

        If dgvDrafts.Columns.Count = 0 Then Return

        dgvDrafts.ClearSelection()

        If dgvDrafts.Columns.Contains("DraftId") Then
            dgvDrafts.Columns("DraftId").Visible = False
        End If

        If dgvDrafts.Columns.Contains("DraftName") Then
            dgvDrafts.Columns("DraftName").HeaderText = "اسم المسودة"
            dgvDrafts.Columns("DraftName").FillWeight = 180
        End If

        If dgvDrafts.Columns.Contains("CustomerName") Then
            dgvDrafts.Columns("CustomerName").HeaderText = "العميل"
            dgvDrafts.Columns("CustomerName").FillWeight = 160
        End If

        If dgvDrafts.Columns.Contains("Date") Then
            dgvDrafts.Columns("Date").HeaderText = "تاريخ الفاتورة"
            dgvDrafts.Columns("Date").DefaultCellStyle.Format = "yyyy/MM/dd"
            dgvDrafts.Columns("Date").FillWeight = 110
        End If

        If dgvDrafts.Columns.Contains("Total") Then
            dgvDrafts.Columns("Total").HeaderText = "الإجمالي"
            dgvDrafts.Columns("Total").DefaultCellStyle.Format = "N3"
            dgvDrafts.Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvDrafts.Columns("Total").FillWeight = 110
        End If

        If dgvDrafts.Columns.Contains("ItemsCount") Then
            dgvDrafts.Columns("ItemsCount").HeaderText = "عدد الأصناف"
            dgvDrafts.Columns("ItemsCount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvDrafts.Columns("ItemsCount").FillWeight = 90
        End If

        If dgvDrafts.Columns.Contains("UpdatedAt") Then
            dgvDrafts.Columns("UpdatedAt").HeaderText = "آخر تحديث"
            dgvDrafts.Columns("UpdatedAt").DefaultCellStyle.Format = "yyyy/MM/dd hh:mm tt"
            dgvDrafts.Columns("UpdatedAt").FillWeight = 140
        End If

    End Sub

    Private Sub UpdateDraftsCount()

        If dgvDrafts.DataSource Is Nothing Then
            lblDraftsCountValue.Text = "0"
            Return
        End If

        lblDraftsCountValue.Text = dgvDrafts.Rows.Count.ToString()

    End Sub

    Private Function GetSelectedDraftId() As String

        If dgvDrafts.CurrentRow Is Nothing Then
            MessageBox.Show("يرجى تحديد مسودة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return ""
        End If

        If dgvDrafts.CurrentRow.Cells("DraftId").Value Is Nothing Then
            MessageBox.Show("لم يتم العثور على رقم المسودة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return ""
        End If

        Return dgvDrafts.CurrentRow.Cells("DraftId").Value.ToString()

    End Function

    Private Sub OpenSelectedDraft()

        Dim draftId As String = GetSelectedDraftId()

        If String.IsNullOrWhiteSpace(draftId) Then Return

        Sales_Fast_Draft.OpenDraft(draftId)
        Me.Close()

    End Sub

    Private Sub dgvDrafts_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDrafts.CellDoubleClick

        If e.RowIndex < 0 Then Return

        dgvDrafts.CurrentCell = dgvDrafts.Rows(e.RowIndex).Cells(1)
        OpenSelectedDraft()

    End Sub

    Private Sub btnOpenDraft_Click(sender As Object, e As EventArgs) Handles btnOpenDraft.Click
        OpenSelectedDraft()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadDraftsList()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim draftId As String = GetSelectedDraftId()

        If String.IsNullOrWhiteSpace(draftId) Then Return

        Dim result = MessageBox.Show(
            "هل أنت متأكد من حذف هذه المسودة؟",
            "تأكيد الحذف",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        )

        If result <> DialogResult.Yes Then Return

        Try
            Sales_Fast_Draft.DraftManager.DeleteDraft(draftId)

            MessageBox.Show("تم حذف المسودة بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadDraftsList()

        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء حذف المسودة:" & Environment.NewLine & ex.Message,
                            "خطأ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtSearch_GotFocus(sender As Object, e As EventArgs) Handles txtSearch.GotFocus

        If txtSearch.Text = "اكتب للبحث..." Then
            txtSearch.Text = ""
            txtSearch.ForeColor = Color.Black
        End If

    End Sub

    Private Sub txtSearch_LostFocus(sender As Object, e As EventArgs) Handles txtSearch.LostFocus

        If txtSearch.Text.Trim() = "" Then
            txtSearch.Text = "اكتب للبحث..."
            txtSearch.ForeColor = Color.Gray
        End If

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

        If DraftsTable Is Nothing Then Return

        If txtSearch.Text = "اكتب للبحث..." Then Return

        Dim keyword As String = txtSearch.Text.Trim().Replace("'", "''")

        If keyword = "" Then
            DraftsTable.DefaultView.RowFilter = ""
        Else
            DraftsTable.DefaultView.RowFilter =
                "DraftName LIKE '%" & keyword & "%' OR " &
                "CustomerName LIKE '%" & keyword & "%'"
        End If

        UpdateDraftsCount()

    End Sub

End Class








'Public Class Sales_Drafts_Menu
'    Private Sub Sales_Drafts_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        LoadDraftsList()
'    End Sub

'    Private Sub LoadDraftsList()

'        Dim drafts = Sales_Fast_Draft.DraftManager.GetAllDrafts()

'        Dim dt As New DataTable()
'        dt.Columns.Add("DraftId")
'        dt.Columns.Add("DraftName")
'        dt.Columns.Add("CustomerName")
'        dt.Columns.Add("Date", GetType(DateTime))
'        dt.Columns.Add("Total", GetType(Decimal))
'        dt.Columns.Add("ItemsCount", GetType(Integer))
'        dt.Columns.Add("UpdatedAt", GetType(DateTime))

'        For Each d In drafts
'            dt.Rows.Add(
'                d.DraftId,
'                d.DraftName,
'                d.CustomerName,
'                d.Date,
'                d.Total,
'                d.Items.Count,
'                d.UpdatedAt
'            )
'        Next

'        dgvDrafts.DataSource = dt

'        If dgvDrafts.Columns.Contains("DraftId") Then
'            dgvDrafts.Columns("DraftId").Visible = False
'        End If

'    End Sub

'    Private Sub dgvDrafts_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDrafts.CellDoubleClick

'        If e.RowIndex < 0 Then Exit Sub

'        Dim draftId As String = dgvDrafts.Rows(e.RowIndex).Cells("DraftId").Value.ToString()

'        Sales_Fast_Draft.OpenDraft(draftId)
'        Me.Close()
'    End Sub


'    Private Sub btnOpenDraft_Click(sender As Object, e As EventArgs) Handles btnOpenDraft.Click

'    End Sub
'End Class