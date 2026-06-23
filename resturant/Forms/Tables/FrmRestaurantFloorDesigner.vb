Imports System.Data
Imports System.Drawing

Public Class FrmRestaurantFloorDesigner

    Private ReadOnly Repository As New RestaurantFloorLayoutRepository()
    Private CurrentFlateID As Integer = 0
    Private IsUpdatingSelectedElement As Boolean = False
    Private IsUpdatingFloorControls As Boolean = False
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
        EnsureFloorElement(elements)

        FloorCanvas.Elements = elements
        LoadFloorControls()
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

    Private Sub EnsureFloorElement(elements As List(Of RestaurantFloorElement))
        If elements Is Nothing Then Return

        Dim floorElement As RestaurantFloorElement = GetFloorElement(elements)
        If floorElement Is Nothing Then
            floorElement = New RestaurantFloorElement()
            floorElement.Flate_ID = CurrentFlateID
            floorElement.ElementType = "FloorRect"
            floorElement.ElementText = "الدور"
            floorElement.X_Pos = 20
            floorElement.Y_Pos = 20
            floorElement.WidthValue = Math.Max(760, FloorCanvas.Width - 120)
            floorElement.HeightValue = Math.Max(520, FloorCanvas.Height - 100)
            floorElement.SeatsCount = 0
            floorElement.BackColorArgb = Color.FromArgb(248, 250, 252).ToArgb()
            floorElement.ForeColorArgb = Color.FromArgb(15, 23, 42).ToArgb()
            floorElement.ZIndex = -10000
            elements.Insert(0, floorElement)
        Else
            floorElement.Flate_ID = CurrentFlateID
            floorElement.ZIndex = -10000
            If floorElement.WidthValue < 160 Then floorElement.WidthValue = 760
            If floorElement.HeightValue < 120 Then floorElement.HeightValue = 520
        End If
    End Sub

    Private Function GetFloorElement(elements As List(Of RestaurantFloorElement)) As RestaurantFloorElement
        If elements Is Nothing Then Return Nothing

        For Each element As RestaurantFloorElement In elements
            If IsFloorElement(element) Then Return element
        Next

        Return Nothing
    End Function

    Private Function IsFloorElement(element As RestaurantFloorElement) As Boolean
        If element Is Nothing Then Return False

        Select Case element.ElementType
            Case "FloorRect", "FloorSquare", "FloorOval", "FloorCustom"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Sub LoadFloorControls()
        Dim floorElement As RestaurantFloorElement = GetFloorElement(FloorCanvas.Elements)
        If floorElement Is Nothing Then Return

        IsUpdatingFloorControls = True
        FloorShapeComboBox.SelectedIndex = GetFloorShapeIndex(floorElement.ElementType)
        FloorWidthNum.Value = ClampFloorValue(floorElement.WidthValue, FloorWidthNum.Minimum, FloorWidthNum.Maximum)
        FloorHeightNum.Value = ClampFloorValue(floorElement.HeightValue, FloorHeightNum.Minimum, FloorHeightNum.Maximum)
        IsUpdatingFloorControls = False
    End Sub

    Private Function ClampFloorValue(value As Integer, minValue As Decimal, maxValue As Decimal) As Decimal
        Dim result As Decimal = CDec(value)
        If result < minValue Then result = minValue
        If result > maxValue Then result = maxValue
        Return result
    End Function

    Private Function GetFloorShapeIndex(elementType As String) As Integer
        Select Case elementType
            Case "FloorSquare"
                Return 1
            Case "FloorOval"
                Return 2
            Case "FloorCustom"
                Return 3
            Case Else
                Return 0
        End Select
    End Function

    Private Function GetFloorElementType() As String
        Select Case FloorShapeComboBox.SelectedIndex
            Case 1
                Return "FloorSquare"
            Case 2
                Return "FloorOval"
            Case 3
                Return "FloorCustom"
            Case Else
                Return "FloorRect"
        End Select
    End Function

    Private Sub ApplyFloorControls(Optional sourceName As String = "")
        If IsUpdatingFloorControls Then Return

        Dim floorElement As RestaurantFloorElement = GetFloorElement(FloorCanvas.Elements)
        If floorElement Is Nothing Then
            EnsureFloorElement(FloorCanvas.Elements)
            floorElement = GetFloorElement(FloorCanvas.Elements)
        End If
        If floorElement Is Nothing Then Return

        floorElement.ElementType = GetFloorElementType()

        If floorElement.ElementType = "FloorSquare" Then
            IsUpdatingFloorControls = True
            If sourceName = "Height" Then
                FloorWidthNum.Value = FloorHeightNum.Value
            Else
                FloorHeightNum.Value = FloorWidthNum.Value
            End If
            IsUpdatingFloorControls = False
        End If

        floorElement.WidthValue = Convert.ToInt32(FloorWidthNum.Value)
        floorElement.HeightValue = Convert.ToInt32(FloorHeightNum.Value)
        floorElement.SeatsCount = 0
        floorElement.ZIndex = -10000

        FloorCanvas.Invalidate()
    End Sub

    Private Sub FloorShapeComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FloorShapeComboBox.SelectedIndexChanged
        ApplyFloorControls("Shape")
    End Sub

    Private Sub FloorWidthNum_ValueChanged(sender As Object, e As EventArgs) Handles FloorWidthNum.ValueChanged
        ApplyFloorControls("Width")
    End Sub

    Private Sub FloorHeightNum_ValueChanged(sender As Object, e As EventArgs) Handles FloorHeightNum.ValueChanged
        ApplyFloorControls("Height")
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
            IsUpdatingSelectedElement = True
            numSeats.Enabled = False
            numWidth.Enabled = False
            numHeight.Enabled = False
            TableShapeComboBox.Enabled = False
            TableShapeComboBox.SelectedIndex = -1
            txtElementText.Enabled = False
            btnDeleteSelected.Enabled = False
            IsUpdatingSelectedElement = False
            Return
        End If

        IsUpdatingSelectedElement = True
        Dim isFloor As Boolean = IsFloorElement(element)
        lblSelected.Text = "العنصر المحدد: " & If(String.IsNullOrWhiteSpace(element.ElementText), element.ElementType, element.ElementText)
        numSeats.Enabled = element.TB_ID.HasValue
        numWidth.Enabled = Not isFloor
        numHeight.Enabled = Not isFloor
        TableShapeComboBox.Enabled = element.TB_ID.HasValue
        TableShapeComboBox.SelectedIndex = If(element.TB_ID.HasValue, GetTableShapeIndex(element), -1)
        txtElementText.Enabled = element.TB_ID.HasValue = False AndAlso Not isFloor
        btnDeleteSelected.Enabled = element.TB_ID.HasValue = False AndAlso Not isFloor

        numSeats.Value = Math.Max(numSeats.Minimum, Math.Min(numSeats.Maximum, element.SeatsCount))
        numWidth.Value = Math.Max(numWidth.Minimum, Math.Min(numWidth.Maximum, element.WidthValue))
        numHeight.Value = Math.Max(numHeight.Minimum, Math.Min(numHeight.Maximum, element.HeightValue))
        txtElementText.Text = element.ElementText
        IsUpdatingSelectedElement = False
    End Sub

    Private Function GetTableShapeIndex(element As RestaurantFloorElement) As Integer
        If element Is Nothing Then Return 0

        Select Case element.ElementType
            Case "RectTable"
                Return 1
            Case "RoundTable"
                Return 2
            Case "SquareTable"
                Return 3
            Case Else
                Return 0
        End Select
    End Function

    Private Function IsEqualSizeTable(element As RestaurantFloorElement) As Boolean
        If element Is Nothing Then Return False
        Return element.ElementType = "RoundTable" OrElse element.ElementType = "SquareTable"
    End Function

    Private Sub ApplySelectedTableShape()
        If FloorCanvas.SelectedElement Is Nothing Then Return
        If FloorCanvas.SelectedElement.TB_ID.HasValue = False Then Return
        If TableShapeComboBox.SelectedIndex < 0 Then Return

        Dim element As RestaurantFloorElement = FloorCanvas.SelectedElement

        Select Case TableShapeComboBox.SelectedIndex
            Case 1
                element.ElementType = "RectTable"
                If element.WidthValue <= element.HeightValue Then element.WidthValue = Math.Max(140, element.HeightValue + 40)
            Case 2
                element.ElementType = "RoundTable"
                MakeSelectedTableEqualSize(element)
            Case 3
                element.ElementType = "SquareTable"
                MakeSelectedTableEqualSize(element)
            Case Else
                element.ElementType = "Table"
        End Select

        RefreshSelectedSizeControls(element)
        FloorCanvas.Invalidate()
    End Sub

    Private Sub MakeSelectedTableEqualSize(element As RestaurantFloorElement)
        Dim sizeValue As Integer = Math.Max(element.WidthValue, element.HeightValue)
        If sizeValue < 80 Then sizeValue = 80
        element.WidthValue = sizeValue
        element.HeightValue = sizeValue
    End Sub

    Private Sub RefreshSelectedSizeControls(element As RestaurantFloorElement)
        If element Is Nothing Then Return

        IsUpdatingSelectedElement = True
        numWidth.Value = Math.Max(numWidth.Minimum, Math.Min(numWidth.Maximum, element.WidthValue))
        numHeight.Value = Math.Max(numHeight.Minimum, Math.Min(numHeight.Maximum, element.HeightValue))
        IsUpdatingSelectedElement = False
    End Sub

    Private Sub numSeats_ValueChanged(sender As Object, e As EventArgs) Handles numSeats.ValueChanged
        If IsUpdatingSelectedElement Then Return
        If FloorCanvas.SelectedElement Is Nothing Then Return
        FloorCanvas.SelectedElement.SeatsCount = Convert.ToInt32(numSeats.Value)
        FloorCanvas.Invalidate()
    End Sub

    Private Sub numWidth_ValueChanged(sender As Object, e As EventArgs) Handles numWidth.ValueChanged
        If IsUpdatingSelectedElement Then Return
        If FloorCanvas.SelectedElement Is Nothing Then Return
        Dim element As RestaurantFloorElement = FloorCanvas.SelectedElement
        element.WidthValue = Convert.ToInt32(numWidth.Value)
        If IsEqualSizeTable(element) Then
            element.HeightValue = element.WidthValue
            RefreshSelectedSizeControls(element)
        End If
        FloorCanvas.Invalidate()
    End Sub

    Private Sub numHeight_ValueChanged(sender As Object, e As EventArgs) Handles numHeight.ValueChanged
        If IsUpdatingSelectedElement Then Return
        If FloorCanvas.SelectedElement Is Nothing Then Return
        Dim element As RestaurantFloorElement = FloorCanvas.SelectedElement
        element.HeightValue = Convert.ToInt32(numHeight.Value)
        If IsEqualSizeTable(element) Then
            element.WidthValue = element.HeightValue
            RefreshSelectedSizeControls(element)
        End If
        FloorCanvas.Invalidate()
    End Sub

    Private Sub TableShapeComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TableShapeComboBox.SelectedIndexChanged
        If IsUpdatingSelectedElement Then Return
        ApplySelectedTableShape()
        FloorCanvas.Invalidate()
    End Sub

    Private Sub txtElementText_TextChanged(sender As Object, e As EventArgs) Handles txtElementText.TextChanged
        If FloorCanvas.SelectedElement Is Nothing Then Return
        If FloorCanvas.SelectedElement.TB_ID.HasValue Then Return
        If IsFloorElement(FloorCanvas.SelectedElement) Then Return
        FloorCanvas.SelectedElement.ElementText = txtElementText.Text
        FloorCanvas.Invalidate()
    End Sub

    Private Sub btnDeleteSelected_Click(sender As Object, e As EventArgs) Handles btnDeleteSelected.Click
        If FloorCanvas.SelectedElement Is Nothing Then Return
        If FloorCanvas.SelectedElement.TB_ID.HasValue Then Return
        If IsFloorElement(FloorCanvas.SelectedElement) Then Return

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
