Imports System.Data
Imports System.Drawing

Public Class FrmRestaurantFloorDesigner

    Private ReadOnly Repository As New RestaurantFloorLayoutRepository()
    Private CurrentFlateID As Integer = 0
    Public Property StartFlateID As Integer = 0

    Private Sub FrmRestaurantFloorDesigner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        LoadFlates()
        RefreshLayout()
    End Sub

    Private Sub LoadFlates()
        Dim dt As DataTable = Repository.LoadFlates()
        cmbFlates.DataSource = dt
        cmbFlates.DisplayMember = "Flate_Name"
        cmbFlates.ValueMember = "Flate_ID"

        If StartFlateID > 0 Then cmbFlates.SelectedValue = StartFlateID
    End Sub

    Private Sub RefreshLayout()
        If cmbFlates.SelectedValue Is Nothing OrElse TypeOf cmbFlates.SelectedValue Is DataRowView Then Return

        CurrentFlateID = Convert.ToInt32(cmbFlates.SelectedValue)

        Dim tablesDt As DataTable = Repository.LoadTables(CurrentFlateID)
        Dim elements As List(Of RestaurantFloorElement) = Repository.LoadLayout(CurrentFlateID)
        MergeTablesWithLayout(elements, tablesDt)

        FloorCanvas.Elements = elements
        lblStatus.Text = "عدد العناصر: " & elements.Count.ToString()
    End Sub

    Private Sub MergeTablesWithLayout(elements As List(Of RestaurantFloorElement), tablesDt As DataTable)
        Dim representedTables As New List(Of Integer)()

        For Each element As RestaurantFloorElement In elements
            If element.TB_ID.HasValue Then representedTables.Add(element.TB_ID.Value)
        Next

        Dim index As Integer = representedTables.Count
        For Each row As DataRow In tablesDt.Rows
            Dim tbId As Integer = Convert.ToInt32(row("TB_ID"))
            If representedTables.Contains(tbId) Then
                ApplyTableState(elements, row)
                Continue For
            End If

            Dim point As Point = GetDefaultTablePoint(index)
            Dim element As New RestaurantFloorElement()
            element.Flate_ID = CurrentFlateID
            element.TB_ID = tbId
            element.ElementType = "Table"
            element.ElementText = row("T_Name").ToString()
            element.X_Pos = point.X
            element.Y_Pos = point.Y
            element.WidthValue = 115
            element.HeightValue = 80
            element.SeatsCount = 4
            element.BackColorArgb = Color.WhiteSmoke.ToArgb()
            element.ForeColorArgb = Color.FromArgb(15, 23, 42).ToArgb()
            element.IsBusy = GetBool(row("isbusy"))
            element.IsCash = GetBool(row("is_Cash"))
            element.ZIndex = index
            elements.Add(element)
            index += 1
        Next
    End Sub

    Private Sub ApplyTableState(elements As List(Of RestaurantFloorElement), row As DataRow)
        Dim tbId As Integer = Convert.ToInt32(row("TB_ID"))
        For Each element As RestaurantFloorElement In elements
            If element.TB_ID.HasValue AndAlso element.TB_ID.Value = tbId Then
                element.ElementText = row("T_Name").ToString()
                element.IsBusy = GetBool(row("isbusy"))
                element.IsCash = GetBool(row("is_Cash"))
                Exit For
            End If
        Next
    End Sub

    Private Function GetDefaultTablePoint(index As Integer) As Point
        Dim columns As Integer = Math.Max(1, CInt(Math.Floor((FloorCanvas.Width - 60) / 150.0R)))
        Dim x As Integer = 30 + ((index Mod columns) * 150)
        Dim y As Integer = 30 + ((index \ columns) * 125)
        Return New Point(x, y)
    End Function

    Private Function GetBool(value As Object) As Boolean
        If value Is Nothing OrElse value Is DBNull.Value Then Return False

        Dim boolValue As Boolean
        Dim text As String = value.ToString().Trim()
        If text = "" Then Return False
        If Boolean.TryParse(text, boolValue) Then Return boolValue

        Dim numberValue As Decimal
        If Decimal.TryParse(text, numberValue) Then Return numberValue <> 0D

        Return False
    End Function

    Private Sub cmbFlates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFlates.SelectedIndexChanged
        RefreshLayout()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        RefreshLayout()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Repository.SaveLayout(CurrentFlateID, FloorCanvas.Elements)
            MsgBox("تم حفظ مخطط الدور بنجاح.", MsgBoxStyle.Information, "مخطط الطاولات")
            RefreshLayout()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "مخطط الطاولات")
        End Try
    End Sub

    Private Sub btnAutoArrange_Click(sender As Object, e As EventArgs) Handles btnAutoArrange.Click
        Dim index As Integer = 0
        For Each element As RestaurantFloorElement In FloorCanvas.Elements
            If element.TB_ID.HasValue = False Then Continue For
            Dim point As Point = GetDefaultTablePoint(index)
            element.X_Pos = point.X
            element.Y_Pos = point.Y
            index += 1
        Next
        FloorCanvas.Invalidate()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If MessageBox.Show("سيتم حذف تخطيط هذا الدور فقط وإعادة ترتيبه افتراضياً. هل تريد المتابعة؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            Repository.ResetLayout(CurrentFlateID)
            RefreshLayout()
        End If
    End Sub

    Private Sub btnWall_Click(sender As Object, e As EventArgs) Handles btnWall.Click
        AddDesignElement("Wall", "جدار", 520, 20, Color.FromArgb(51, 65, 85), Color.White)
    End Sub

    Private Sub btnDoor_Click(sender As Object, e As EventArgs) Handles btnDoor.Click
        AddDesignElement("Door", "باب", 90, 28, Color.FromArgb(250, 204, 21), Color.FromArgb(15, 23, 42))
    End Sub

    Private Sub btnCounter_Click(sender As Object, e As EventArgs) Handles btnCounter.Click
        AddDesignElement("Counter", "كاونتر", 180, 70, Color.FromArgb(45, 212, 191), Color.FromArgb(15, 23, 42))
    End Sub

    Private Sub AddDesignElement(elementType As String, elementText As String, widthValue As Integer, heightValue As Integer, backColor As Color, foreColor As Color)
        Dim element As New RestaurantFloorElement()
        element.Flate_ID = CurrentFlateID
        element.ElementType = elementType
        element.ElementText = elementText
        element.X_Pos = 40
        element.Y_Pos = 40
        element.WidthValue = widthValue
        element.HeightValue = heightValue
        element.SeatsCount = 0
        element.BackColorArgb = backColor.ToArgb()
        element.ForeColorArgb = foreColor.ToArgb()
        element.ZIndex = FloorCanvas.Elements.Count + 1
        FloorCanvas.AddElement(element)
    End Sub

    Private Sub FloorCanvas_ElementSelected(element As RestaurantFloorElement) Handles FloorCanvas.ElementSelected
        If element Is Nothing Then
            lblSelected.Text = "العنصر المحدد: لا يوجد"
            numSeats.Enabled = False
            numWidth.Enabled = False
            numHeight.Enabled = False
            txtElementText.Enabled = False
            btnDeleteSelected.Enabled = False
            Return
        End If

        lblSelected.Text = "العنصر المحدد: " & If(String.IsNullOrWhiteSpace(element.ElementText), element.ElementType, element.ElementText)
        numSeats.Enabled = element.TB_ID.HasValue
        numWidth.Enabled = True
        numHeight.Enabled = True
        txtElementText.Enabled = element.TB_ID.HasValue = False
        btnDeleteSelected.Enabled = element.TB_ID.HasValue = False

        numSeats.Value = Math.Max(numSeats.Minimum, Math.Min(numSeats.Maximum, element.SeatsCount))
        numWidth.Value = Math.Max(numWidth.Minimum, Math.Min(numWidth.Maximum, element.WidthValue))
        numHeight.Value = Math.Max(numHeight.Minimum, Math.Min(numHeight.Maximum, element.HeightValue))
        txtElementText.Text = element.ElementText
    End Sub

    Private Sub numSeats_ValueChanged(sender As Object, e As EventArgs) Handles numSeats.ValueChanged
        If FloorCanvas.SelectedElement Is Nothing Then Return
        FloorCanvas.SelectedElement.SeatsCount = Convert.ToInt32(numSeats.Value)
        FloorCanvas.Invalidate()
    End Sub

    Private Sub numWidth_ValueChanged(sender As Object, e As EventArgs) Handles numWidth.ValueChanged
        If FloorCanvas.SelectedElement Is Nothing Then Return
        FloorCanvas.SelectedElement.WidthValue = Convert.ToInt32(numWidth.Value)
        FloorCanvas.Invalidate()
    End Sub

    Private Sub numHeight_ValueChanged(sender As Object, e As EventArgs) Handles numHeight.ValueChanged
        If FloorCanvas.SelectedElement Is Nothing Then Return
        FloorCanvas.SelectedElement.HeightValue = Convert.ToInt32(numHeight.Value)
        FloorCanvas.Invalidate()
    End Sub

    Private Sub txtElementText_TextChanged(sender As Object, e As EventArgs) Handles txtElementText.TextChanged
        If FloorCanvas.SelectedElement Is Nothing Then Return
        If FloorCanvas.SelectedElement.TB_ID.HasValue Then Return
        FloorCanvas.SelectedElement.ElementText = txtElementText.Text
        FloorCanvas.Invalidate()
    End Sub

    Private Sub btnDeleteSelected_Click(sender As Object, e As EventArgs) Handles btnDeleteSelected.Click
        If FloorCanvas.SelectedElement Is Nothing Then Return
        If FloorCanvas.SelectedElement.TB_ID.HasValue Then Return

        FloorCanvas.Elements.Remove(FloorCanvas.SelectedElement)
        FloorCanvas.SelectElement(Nothing)
        FloorCanvas.Invalidate()
    End Sub

    Private Sub FloorCanvas_ElementMoved(element As RestaurantFloorElement) Handles FloorCanvas.ElementMoved
        If element Is Nothing Then Return
        lblStatus.Text = "الموقع: " & element.X_Pos.ToString() & ", " & element.Y_Pos.ToString()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class
