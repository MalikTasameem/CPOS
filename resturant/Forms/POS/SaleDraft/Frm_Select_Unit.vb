Public Class Frm_Select_Unit

    Public SelectedRow As DataRow = Nothing

    Private _dt As DataTable
    Private _imId As Integer
    Private _currentUId As Integer
    Private _unitRows As New List(Of DataRow)

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

        _unitRows.Clear()
        UnitsFlowPanel.Controls.Clear()

        Dim rows = _dt.Select("IM_ID = " & _imId & " AND U_ID <> " & _currentUId)

        For Each r As DataRow In rows
            _unitRows.Add(r)
            UnitsFlowPanel.Controls.Add(CreateUnitButton(r))
        Next

        If _unitRows.Count = 0 Then
            EmptyUnits_LB.Visible = True
            UnitsFlowPanel.Controls.Add(EmptyUnits_LB)
        End If

        btnSelect.Enabled = False

    End Sub

    Private Function CreateUnitButton(row As DataRow) As Button

        Dim unitName As String = SafeText(row, "U_Name")
        Dim price As String = SafeDecimalText(row, "Price")
        'Dim cargo As String = SafeDecimalText(row, "U_Cargo")
        Dim barcode As String = SafeText(row, "Barcode")

        Dim unitButton As New Button()
        unitButton.AutoSize = False
        unitButton.BackColor = Color.White
        unitButton.Cursor = Cursors.Hand
        unitButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225)
        unitButton.FlatAppearance.BorderSize = 1
        unitButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(219, 234, 254)
        unitButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 246, 255)
        unitButton.FlatStyle = FlatStyle.Flat
        unitButton.Font = New Font("Segoe UI Semibold", 11.0!, FontStyle.Bold)
        unitButton.ForeColor = Color.FromArgb(15, 23, 42)
        unitButton.Margin = New Padding(10)
        unitButton.RightToLeft = RightToLeft.Yes
        unitButton.Size = New Size(220, 116)
        unitButton.Tag = row
        unitButton.Text =
            unitName & Environment.NewLine &
            "السعر: " & price & Environment.NewLine &
            "باركود: " & If(barcode = "", "-", barcode)

        '  "العبوة: " & cargo & Environment.NewLine &
        unitButton.TextAlign = ContentAlignment.MiddleCenter

        AddHandler unitButton.Click, AddressOf UnitButton_Click
        AddHandler unitButton.DoubleClick, AddressOf UnitButton_DoubleClick

        Return unitButton

    End Function

    Private Sub UnitButton_Click(sender As Object, e As EventArgs)

        For Each control As Control In UnitsFlowPanel.Controls
            Dim button = TryCast(control, Button)
            If button IsNot Nothing Then
                button.BackColor = Color.White
                button.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225)
                button.ForeColor = Color.FromArgb(15, 23, 42)
            End If
        Next

        Dim selectedButton = TryCast(sender, Button)

        If selectedButton Is Nothing Then Exit Sub

        selectedButton.BackColor = Color.FromArgb(219, 234, 254)
        selectedButton.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235)
        selectedButton.ForeColor = Color.FromArgb(30, 64, 175)
        SelectedRow = TryCast(selectedButton.Tag, DataRow)
        btnSelect.Enabled = (SelectedRow IsNot Nothing)

    End Sub

    Private Sub UnitButton_DoubleClick(sender As Object, e As EventArgs)

        UnitButton_Click(sender, e)
        SelectCurrentUnit()

    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        SelectCurrentUnit()
    End Sub

    Private Sub SelectCurrentUnit()

        If SelectedRow IsNot Nothing Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Function SafeText(row As DataRow, columnName As String) As String

        If row.Table.Columns.Contains(columnName) AndAlso row(columnName) IsNot DBNull.Value Then
            Return row(columnName).ToString()
        End If

        Return ""

    End Function

    Private Function SafeDecimalText(row As DataRow, columnName As String) As String

        Dim value As Decimal

        If Decimal.TryParse(SafeText(row, columnName), value) Then
            Return value.ToString("N3")
        End If

        Return "0.000"

    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
