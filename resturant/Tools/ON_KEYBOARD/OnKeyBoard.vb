Imports System.Drawing

Public Class OnKeyBoard

    Public Event UC_Button1Click(ByVal sender As Object, ByVal e As System.EventArgs)

    Private _isScaled As Boolean = False

    Private Sub OnKeyBoard_Load(sender As Object, e As EventArgs) Handles Me.Load
        BindButtons(Me)

        If Not _isScaled Then
            ScaleKeyboard(0.74F, 0.74F)   ' غيّر النسبة إذا أردت أصغر أو أكبر
            _isScaled = True
        End If
    End Sub

    Private Sub BindButtons(parent As Control)
        For Each c As Control In parent.Controls
            If TypeOf c Is Button Then
                AddHandler c.Click, AddressOf Dynamic_DoubleclickItems
            End If

            If c.HasChildren Then
                BindButtons(c)
            End If
        Next
    End Sub

    Private Sub Dynamic_DoubleclickItems(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim bt As Button = DirectCast(sender, Button)
            RaiseEvent UC_Button1Click(bt, New EventArgs)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ScaleKeyboard(scaleX As Single, scaleY As Single)
        Me.SuspendLayout()
        ScaleAllControls(Me, scaleX, scaleY)
        Me.Size = New Size(
            CInt(Me.Width * scaleX),
            CInt(Me.Height * scaleY)
        )
        Me.ResumeLayout()
    End Sub

    Private Sub ScaleAllControls(parent As Control, scaleX As Single, scaleY As Single)
        For Each ctrl As Control In parent.Controls

            ctrl.Left = CInt(ctrl.Left * scaleX)
            ctrl.Top = CInt(ctrl.Top * scaleY)
            ctrl.Width = CInt(ctrl.Width * scaleX)
            ctrl.Height = CInt(ctrl.Height * scaleY)

            Dim newFontSize As Single = ctrl.Font.Size * Math.Min(scaleX, scaleY)

            If newFontSize < 8.0F Then newFontSize = 8.0F

            ctrl.Font = New Font(ctrl.Font.FontFamily, newFontSize, ctrl.Font.Style)

            If TypeOf ctrl Is Button Then
                Dim bt As Button = DirectCast(ctrl, Button)
                bt.FlatStyle = FlatStyle.Flat
                bt.FlatAppearance.BorderSize = 1
                bt.FlatAppearance.BorderColor = Color.Silver
                bt.Margin = New Padding(1)
                bt.Cursor = Cursors.Hand
                bt.TextAlign = ContentAlignment.MiddleCenter
            End If

            If ctrl.HasChildren Then
                ScaleAllControls(ctrl, scaleX, scaleY)
            End If
        Next
    End Sub

    Private Sub BLANG_Click(sender As Object, e As EventArgs) Handles BLANG.Click
        Try
            EN.Visible = Not EN.Visible
            AR.Visible = Not AR.Visible
        Catch ex As Exception
        End Try
    End Sub

End Class

'-------------------------------------------------------------------------------------------------------------------
'Public Class OnKeyBoard
'    Public Event UC_Button1Click(ByVal sender As Object, ByVal e As System.EventArgs)

'    Private Sub BLANG_Click(sender As Object, e As EventArgs) Handles BLANG.Click
'        Try
'            EN.Visible = Not EN.Visible
'            AR.Visible = Not AR.Visible
'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Sub OnKeyBoard_Load(sender As Object, e As EventArgs) Handles Me.Load
'        For Each c As Control In Me.Controls
'            If TypeOf c Is Button Then
'                AddHandler c.Click, AddressOf Dynamic_DoubleclickItems
'            End If
'        Next c
'        For Each c As Control In Me.EN.Controls
'            If TypeOf c Is Button Then
'                AddHandler c.Click, AddressOf Dynamic_DoubleclickItems
'            End If
'        Next c
'        For Each c As Control In Me.AR.Controls
'            If TypeOf c Is Button Then
'                AddHandler c.Click, AddressOf Dynamic_DoubleclickItems
'            End If
'        Next c
'    End Sub
'    Private Sub Dynamic_DoubleclickItems(ByVal sender As Object, ByVal e As System.EventArgs)
'        Try
'            Dim bt As Button = sender
'            RaiseEvent UC_Button1Click(bt, New EventArgs)
'        Catch ex As Exception
'            '     Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)

'        End Try

'    End Sub


'End Class
