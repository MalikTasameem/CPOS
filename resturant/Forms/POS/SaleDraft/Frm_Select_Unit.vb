Public Class Frm_Select_Unit

    Public SelectedRow As DataRow = Nothing

    Private _dt As DataTable
    Private _imId As Integer
    Private _currentUId As Integer

    Public Sub New(dt As DataTable, imId As Integer, currentUId As Integer)
        InitializeComponent()

        _dt = dt
        _imId = imId
        _currentUId = currentUId
    End Sub

    Private Sub Frm_Select_Unit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUnits()
    End Sub

    Private Sub LoadUnits()

        Dim rows = _dt.Select("IM_ID = " & _imId & " AND U_ID <> " & _currentUId)

        Dim result As DataTable = _dt.Clone()

        For Each r As DataRow In rows
            result.ImportRow(r)
        Next

        dgvUnits.DataSource = result

        FormatGrid()

    End Sub

    Private Sub FormatGrid()

        With dgvUnits
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .RightToLeft = RightToLeft.Yes
        End With

        If dgvUnits.Columns.Contains("U_IM_ID") Then dgvUnits.Columns("U_IM_ID").Visible = False
        If dgvUnits.Columns.Contains("IM_ID") Then dgvUnits.Columns("IM_ID").Visible = False
        If dgvUnits.Columns.Contains("U_ID") Then dgvUnits.Columns("U_ID").Visible = False

        If dgvUnits.Columns.Contains("item_name") Then dgvUnits.Columns("item_name").HeaderText = "الصنف"
        If dgvUnits.Columns.Contains("U_Name") Then dgvUnits.Columns("U_Name").HeaderText = "الوحدة"
        If dgvUnits.Columns.Contains("U_Cargo") Then dgvUnits.Columns("U_Cargo").HeaderText = "العبوة"
        If dgvUnits.Columns.Contains("Price") Then dgvUnits.Columns("Price").HeaderText = "السعر"
        If dgvUnits.Columns.Contains("Min_SP") Then dgvUnits.Columns("Min_SP").HeaderText = "أقل سعر"
        If dgvUnits.Columns.Contains("Min_SP_2") Then dgvUnits.Columns("Min_SP_2").HeaderText = "أقل سعر 2"
        If dgvUnits.Columns.Contains("Percent_Price") Then dgvUnits.Columns("Percent_Price").HeaderText = "النسبة"
        If dgvUnits.Columns.Contains("Barcode") Then dgvUnits.Columns("Barcode").HeaderText = "الباركود"

    End Sub

    Private Sub dgvUnits_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUnits.CellDoubleClick
        SelectCurrentUnit()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        SelectCurrentUnit()
    End Sub

    Private Sub SelectCurrentUnit()

        If dgvUnits.CurrentRow Is Nothing Then Exit Sub

        Dim drv As DataRowView = TryCast(dgvUnits.CurrentRow.DataBoundItem, DataRowView)

        If drv IsNot Nothing Then
            SelectedRow = drv.Row
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class