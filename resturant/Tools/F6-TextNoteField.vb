Public Class F6TextNoteField
    Inherits TextBox
    Public Sub New()
        Me.BackColor = Color.Lavender
        Me.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaxLength = 250
        '
        ' Me.Multiline = True
    End Sub
    Public alreadyFocused As Boolean
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If alreadyFocused Then
            If e.Control And e.KeyCode = Keys.C Then
                Me.Copy()
            End If
            If e.Control And e.KeyCode = Keys.P Then
                Me.Paste()
            End If
            If e.Control And e.KeyCode = Keys.X Then
                Me.Cut()
            End If
            If e.Control And e.KeyCode = Keys.Z Then
                Me.Undo()
            End If
            If e.Control And e.KeyCode = Keys.A Then
                Me.SelectAll()
            ElseIf e.KeyCode = Keys.Down
                alreadyFocused = False
            ElseIf e.KeyCode = Keys.Enter
                e.SuppressKeyPress = True
            End If
        End If

    End Sub
    Protected Overrides Sub OnLeave(ByVal e As EventArgs)
        MyBase.OnLeave(e)
        Me.alreadyFocused = False
        BackColor = Color.Lavender
    End Sub

    Protected Overrides Sub OnGotFocus(ByVal e As EventArgs)
        MyBase.OnGotFocus(e)

        ' Select all text only if the mouse isn't down.
        ' This makes tabbing to the textbox give focus.
        If MouseButtons = MouseButtons.None Then

            Me.SelectAll()
            Me.alreadyFocused = True
            Me.BackColor = Color.White
        End If

    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mevent As MouseEventArgs)
        MyBase.OnMouseUp(mevent)

        ' Web browsers like Google Chrome select the text on mouse up.
        ' They only do it if the textbox isn't already focused,
        ' and if the user hasn't selected all text.
        If Not Me.alreadyFocused AndAlso Me.SelectionLength = 0 Then
            Me.alreadyFocused = True
            Me.SelectAll()
            Me.BackColor = Color.White
        End If

    End Sub
End Class
