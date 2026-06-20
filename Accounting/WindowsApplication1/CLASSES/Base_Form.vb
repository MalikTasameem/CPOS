'Imports System.Drawing.Drawing2D

'Public Class Base_Form
'    Inherits Form

'    Public Property HeaderColor As Color = Color.FromArgb(33, 150, 243) ' الأزرق الافتراضي
'    Public Property HeaderHeight As Integer = 32

'    Public Sub New()
'        Me.DoubleBuffered = True
'        Me.FormBorderStyle = FormBorderStyle.FixedSingle
'        Me.StartPosition = FormStartPosition.CenterScreen
'        Me.BackColor = Color.White
'    End Sub

'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)

'        Dim g = e.Graphics
'        g.SmoothingMode = SmoothingMode.AntiAlias

'        ' رسم شريط العنوان (العلوي فقط)
'        Using br As New SolidBrush(HeaderColor)
'            g.FillRectangle(br, 0, 0, Me.Width, HeaderHeight)
'        End Using

'        ' رسم عنوان النافذة
'        Using f As New Font("Segoe UI", 10, FontStyle.Bold)
'            TextRenderer.DrawText(g, Me.Text, f, New Rectangle(10, 0, Me.Width - 60, HeaderHeight), Color.White, TextFormatFlags.VerticalCenter)
'        End Using

'        ' رسم خط سفلي خفيف للفصل
'        Using pen As New Pen(Color.FromArgb(200, 200, 200))
'            g.DrawLine(pen, 0, HeaderHeight, Me.Width, HeaderHeight)
'        End Using
'    End Sub
'End Class


'------------------------------------------------------------------------------------------------------------------------
Public Class Base_Form
    Dim rs As New Resizer

    Private Sub Base_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        ToggleTheme(Me)

        ' الخط المطلوب، مثلاً: Segoe UI بحجم 10
        Dim myFont As New Font("Segoe UI", 7.5, FontStyle.Regular)

        ' تطبيقه على كل عناصر الفورم
        ApplyFontToControls(Me.Controls, myFont)

    End Sub


    Private Sub Base_Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Return Then
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Base_Form_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Base_Form_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        GET_summary()
        Me.Dispose()
    End Sub

End Class