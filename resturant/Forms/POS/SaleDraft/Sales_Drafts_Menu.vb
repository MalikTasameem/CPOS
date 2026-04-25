Public Class Sales_Drafts_Menu
    Private Sub Sales_Drafts_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDraftsList()
    End Sub

    Private Sub LoadDraftsList()

        Dim drafts = Sales_Fast_Draft.DraftManager.GetAllDrafts()

        Dim dt As New DataTable()
        dt.Columns.Add("DraftId")
        dt.Columns.Add("DraftName")
        dt.Columns.Add("CustomerName")
        dt.Columns.Add("Date", GetType(DateTime))
        dt.Columns.Add("Total", GetType(Decimal))
        dt.Columns.Add("ItemsCount", GetType(Integer))
        dt.Columns.Add("UpdatedAt", GetType(DateTime))

        For Each d In drafts
            dt.Rows.Add(
                d.DraftId,
                d.DraftName,
                d.CustomerName,
                d.Date,
                d.Total,
                d.Items.Count,
                d.UpdatedAt
            )
        Next

        dgvDrafts.DataSource = dt

        If dgvDrafts.Columns.Contains("DraftId") Then
            dgvDrafts.Columns("DraftId").Visible = False
        End If

    End Sub

    Private Sub dgvDrafts_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDrafts.CellDoubleClick

        If e.RowIndex < 0 Then Exit Sub

        Dim draftId As String = dgvDrafts.Rows(e.RowIndex).Cells("DraftId").Value.ToString()

        Sales_Fast_Draft.OpenDraft(draftId)

    End Sub

End Class