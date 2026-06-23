Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq

Public Class RestaurantFloorDesignerControl
    Inherits UserControl

    Public Event ElementSelected(element As RestaurantFloorElement)
    Public Event ElementMoved(element As RestaurantFloorElement)
    Public Event ElementDoubleClicked(element As RestaurantFloorElement)

    Private _elements As New List(Of RestaurantFloorElement)()
    Private _selectedElement As RestaurantFloorElement = Nothing
    Private _dragStart As Point
    Private _dragOriginal As Point
    Private _isDragging As Boolean = False

    Public Property IsDesignMode As Boolean = True
    Public Property ShowGrid As Boolean = True

    Public Property Elements As List(Of RestaurantFloorElement)
        Get
            Return _elements
        End Get
        Set(value As List(Of RestaurantFloorElement))
            If value Is Nothing Then
                _elements = New List(Of RestaurantFloorElement)()
            Else
                _elements = value
            End If
            _selectedElement = Nothing
            Invalidate()
        End Set
    End Property

    Public ReadOnly Property SelectedElement As RestaurantFloorElement
        Get
            Return _selectedElement
        End Get
    End Property

    Public Sub New()
        Me.DoubleBuffered = True
        Me.BackColor = Color.FromArgb(248, 250, 252)
        Me.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
    End Sub

    Public Sub SelectElement(element As RestaurantFloorElement)
        _selectedElement = element
        RaiseEvent ElementSelected(_selectedElement)
        Invalidate()
    End Sub

    Public Sub AddElement(element As RestaurantFloorElement)
        If element Is Nothing Then Return
        _elements.Add(element)
        SelectElement(element)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        If ShowGrid Then DrawGrid(e.Graphics)

        For Each element As RestaurantFloorElement In _elements.OrderBy(Function(x) x.ZIndex).ToList()
            DrawElement(e.Graphics, element)
        Next
    End Sub

    Private Sub DrawGrid(g As Graphics)
        Using gridPen As New Pen(Color.FromArgb(230, 236, 244), 1)
            For x As Integer = 0 To Me.Width Step 20
                g.DrawLine(gridPen, x, 0, x, Me.Height)
            Next

            For y As Integer = 0 To Me.Height Step 20
                g.DrawLine(gridPen, 0, y, Me.Width, y)
            Next
        End Using
    End Sub

    Private Sub DrawElement(g As Graphics, element As RestaurantFloorElement)
        Select Case element.ElementType
            Case "FloorRect", "FloorSquare", "FloorOval", "FloorCustom"
                DrawFloor(g, element)
            Case "Wall"
                DrawWall(g, element)
            Case "Door"
                DrawDoor(g, element)
            Case "Counter"
                DrawCounter(g, element)
            Case Else
                DrawTable(g, element)
        End Select
    End Sub

    Private Sub DrawFloor(g As Graphics, element As RestaurantFloorElement)
        Dim rect As Rectangle = element.Bounds

        Using borderPen As New Pen(Color.FromArgb(59, 130, 246), 2)
            If element.ElementType = "FloorCustom" Then borderPen.DashStyle = DashStyle.Dash

            Select Case element.ElementType
                Case "FloorOval"
                    g.DrawEllipse(borderPen, rect)
                Case Else
                    g.DrawRectangle(borderPen, rect)
            End Select
        End Using

        DrawSelection(g, element)
    End Sub

    Private Sub DrawTable(g As Graphics, element As RestaurantFloorElement)
        Dim rect As Rectangle = element.Bounds
        Dim fillColor As Color = If(element.IsBusy, Color.FromArgb(248, 113, 113), If(element.IsCash, Color.FromArgb(45, 212, 191), Color.FromArgb(element.BackColorArgb)))
        Dim borderColor As Color = If(element Is _selectedElement, Color.FromArgb(37, 99, 235), Color.FromArgb(71, 85, 105))
        Dim textColor As Color = Color.FromArgb(element.ForeColorArgb)

        DrawSeats(g, element)

        Using fillBrush As New SolidBrush(fillColor),
              borderPen As New Pen(borderColor, If(element Is _selectedElement, 3.0!, 1.4!))

            Select Case GetShapeName(element)
                Case "Round"
                    g.FillEllipse(fillBrush, rect)
                    g.DrawEllipse(borderPen, rect)
                Case "Square"
                    g.FillRectangle(fillBrush, rect)
                    g.DrawRectangle(borderPen, rect)
                Case Else
                    Using path As GraphicsPath = CreateRoundRectangle(rect, 10)
                        g.FillPath(fillBrush, path)
                        g.DrawPath(borderPen, path)
                    End Using
            End Select
        End Using

        Dim text As String = If(String.IsNullOrWhiteSpace(element.ElementText), "طاولة", element.ElementText)
        If element.IsBusy Then text &= Environment.NewLine & "مشغولة"

        Using textBrush As New SolidBrush(textColor),
              sf As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
            g.DrawString(text, Me.Font, textBrush, rect, sf)
        End Using
    End Sub

    Private Sub DrawSeats(g As Graphics, element As RestaurantFloorElement)
        Dim seats As Integer = Math.Max(0, element.SeatsCount)
        If seats = 0 Then Return

        Dim rect As Rectangle = element.Bounds
        Dim seatSize As Integer = 14
        Dim cx As Single = rect.Left + (rect.Width / 2.0!)
        Dim cy As Single = rect.Top + (rect.Height / 2.0!)
        Dim rx As Single = (rect.Width / 2.0!) + 15
        Dim ry As Single = (rect.Height / 2.0!) + 15

        Using seatBrush As New SolidBrush(Color.FromArgb(226, 232, 240)),
              seatPen As New Pen(Color.FromArgb(100, 116, 139), 1)
            For i As Integer = 0 To seats - 1
                Dim angle As Double = ((Math.PI * 2.0R) / seats) * i
                Dim x As Integer = CInt(cx + Math.Cos(angle) * rx) - (seatSize \ 2)
                Dim y As Integer = CInt(cy + Math.Sin(angle) * ry) - (seatSize \ 2)
                Dim seatRect As New Rectangle(x, y, seatSize, seatSize)
                g.FillEllipse(seatBrush, seatRect)
                g.DrawEllipse(seatPen, seatRect)
            Next
        End Using
    End Sub

    Private Sub DrawWall(g As Graphics, element As RestaurantFloorElement)
        Using wallBrush As New SolidBrush(Color.FromArgb(51, 65, 85)),
              wallPen As New Pen(Color.FromArgb(15, 23, 42), 2)
            g.FillRectangle(wallBrush, element.Bounds)
            g.DrawRectangle(wallPen, element.Bounds)
        End Using

        DrawSelection(g, element)
    End Sub

    Private Sub DrawDoor(g As Graphics, element As RestaurantFloorElement)
        Using doorBrush As New SolidBrush(Color.FromArgb(250, 204, 21)),
              doorPen As New Pen(Color.FromArgb(161, 98, 7), 2)
            g.FillRectangle(doorBrush, element.Bounds)
            g.DrawRectangle(doorPen, element.Bounds)
        End Using

        DrawCenteredText(g, element, "باب")
        DrawSelection(g, element)
    End Sub

    Private Sub DrawCounter(g As Graphics, element As RestaurantFloorElement)
        Using counterBrush As New SolidBrush(Color.FromArgb(45, 212, 191)),
              counterPen As New Pen(Color.FromArgb(15, 118, 110), 2)
            Using path As GraphicsPath = CreateRoundRectangle(element.Bounds, 8)
                g.FillPath(counterBrush, path)
                g.DrawPath(counterPen, path)
            End Using
        End Using

        DrawCenteredText(g, element, If(String.IsNullOrWhiteSpace(element.ElementText), "كاونتر", element.ElementText))
        DrawSelection(g, element)
    End Sub

    Private Sub DrawCenteredText(g As Graphics, element As RestaurantFloorElement, text As String)
        Using textBrush As New SolidBrush(Color.FromArgb(15, 23, 42)),
              sf As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
            g.DrawString(text, Me.Font, textBrush, element.Bounds, sf)
        End Using
    End Sub

    Private Sub DrawSelection(g As Graphics, element As RestaurantFloorElement)
        If element IsNot _selectedElement Then Return

        Using pen As New Pen(Color.FromArgb(37, 99, 235), 2)
            pen.DashStyle = DashStyle.Dash
            g.DrawRectangle(pen, element.Bounds)
        End Using
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)

        Dim found As RestaurantFloorElement = FindElementAt(e.Location)
        SelectElement(found)

        If found IsNot Nothing AndAlso IsDesignMode AndAlso e.Button = MouseButtons.Left Then
            _isDragging = True
            _dragStart = e.Location
            _dragOriginal = New Point(found.X_Pos, found.Y_Pos)
            Me.Cursor = Cursors.SizeAll
        End If
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        If _isDragging AndAlso _selectedElement IsNot Nothing Then
            Dim dx As Integer = e.X - _dragStart.X
            Dim dy As Integer = e.Y - _dragStart.Y
            _selectedElement.X_Pos = SnapToGrid(Math.Max(0, _dragOriginal.X + dx))
            _selectedElement.Y_Pos = SnapToGrid(Math.Max(0, _dragOriginal.Y + dy))
            RaiseEvent ElementMoved(_selectedElement)
            Invalidate()
        Else
            Me.Cursor = If(FindElementAt(e.Location) Is Nothing, Cursors.Default, Cursors.Hand)
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        _isDragging = False
        Me.Cursor = Cursors.Default
    End Sub

    Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
        MyBase.OnMouseDoubleClick(e)
        Dim found As RestaurantFloorElement = FindElementAt(e.Location)
        If found IsNot Nothing Then RaiseEvent ElementDoubleClicked(found)
    End Sub

    Private Function FindElementAt(point As Point) As RestaurantFloorElement
        For i As Integer = _elements.Count - 1 To 0 Step -1
            If _elements(i).ContainsPoint(point) Then Return _elements(i)
        Next
        Return Nothing
    End Function

    Private Function SnapToGrid(value As Integer) As Integer
        Return CInt(Math.Round(value / 10.0R) * 10)
    End Function

    Private Function GetShapeName(element As RestaurantFloorElement) As String
        If element Is Nothing OrElse String.IsNullOrWhiteSpace(element.ElementType) Then Return "Rectangle"
        If element.ElementType = "RoundTable" Then Return "Round"
        If element.ElementType = "SquareTable" Then Return "Square"
        Return "Rectangle"
    End Function

    Private Function CreateRoundRectangle(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim diameter As Integer = radius * 2
        Dim arc As New Rectangle(rect.Location, New Size(diameter, diameter))

        path.AddArc(arc, 180, 90)
        arc.X = rect.Right - diameter
        path.AddArc(arc, 270, 90)
        arc.Y = rect.Bottom - diameter
        path.AddArc(arc, 0, 90)
        arc.X = rect.Left
        path.AddArc(arc, 90, 90)
        path.CloseFigure()

        Return path
    End Function

End Class
