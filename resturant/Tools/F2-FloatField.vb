Public Class F2FloatField
    Inherits TextBox
    Dim AlphabitFlag As Boolean = False
    Public Sub New(flag As Boolean)
        AlphabitFlag = flag

    End Sub
    Public Sub New()
        Me.MaxLength = 18
        Me.MaxLength =
        Me.Text = "0"
        Me.BackColor = Color.Lavender
    End Sub
    Public alreadyFocused As Boolean
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
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
        End If
    End Sub
    Protected Overrides Sub OnLeave(ByVal e As EventArgs)
        MyBase.OnLeave(e)
        Me.alreadyFocused = False
        BackColor = Color.Lavender
        If Not AlphabitFlag Then
            Me.Text = roundationeithout0(Text)
        End If
    End Sub

    Public Function roundationeithout0(s As String) As String
        Try
            Dim value As String = ""
            If IsNumeric(s) Then
                Dim sal As Double = Math.Truncate(s * 1000) / 1000
                value = Math.Round(sal, 3)
            Else
                value = "0"
            End If
            Return value
        Catch ex As Exception
            Return "0"
        End Try

    End Function

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
    Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
            If e.KeyChar = "." Or e.KeyChar = "." Then
                If Not Me.Text.Contains(".") And Not Me.Text.Contains(".") Then
                    e.Handled = False
                Else
                    e.Handled = True
                End If
            End If
        End If

    End Sub
End Class
