Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Threading
Imports System.Windows.Forms

' =========================================================
' 1. كلاس التحكم بالمسار المنفصل (لضمان عدم تجمد الحركة)
' =========================================================
Public Class ModernLoader
    Private Shared loadThread As Thread
    Private Shared loadForm As frmLoadingUI

    ' دالة تشغيل شاشة التحميل
    Public Shared Sub ShowLoader()
        loadThread = New Thread(AddressOf ShowForm)
        loadThread.IsBackground = True
        loadThread.Start()
    End Sub

    Private Shared Sub ShowForm()
        loadForm = New frmLoadingUI()
        Application.Run(loadForm)
    End Sub

    ' دالة إيقاف شاشة التحميل
    Public Shared Sub CloseLoader()
        If loadForm IsNot Nothing AndAlso loadForm.InvokeRequired Then
            loadForm.Invoke(New MethodInvoker(AddressOf CloseLoader))
        ElseIf loadForm IsNot Nothing Then
            Application.ExitThread()
        End If
    End Sub
End Class

' =========================================================
' 2. تصميم الواجهة برمجياً (دائرة خضراء تدور بتصميم مسطح)
' =========================================================
Public Class frmLoadingUI
    Inherits Form

    Private tmr As System.Windows.Forms.Timer
    Private angle As Integer = 0

    Public Sub New()
        ' إعدادات الفورم (بدون إطار ومسطح)
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Size = New Size(350, 120)
        Me.BackColor = Color.FromArgb(44, 62, 80) ' خلفية كحلية داكنة راقية
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.DoubleBuffered = True

        ' رسالة التحميل
        Dim lbl As New Label()
        lbl.Text = "جاري تهيئة بيئة العمل..." & vbCrLf & "الرجاء الانتظار لحظات"
        lbl.ForeColor = Color.White
        lbl.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lbl.AutoSize = False
        lbl.Size = New Size(240, 60)
        lbl.Location = New Point(80, 30)
        lbl.TextAlign = ContentAlignment.MiddleLeft
        lbl.RightToLeft = RightToLeft.Yes
        Me.Controls.Add(lbl)

        ' مؤقت حركة الدائرة
        tmr = New System.Windows.Forms.Timer()
        tmr.Interval = 15 ' سرعة الدوران (كلما قل الرقم زادت السرعة)
        AddHandler tmr.Tick, AddressOf Tmr_Tick
        tmr.Start()
    End Sub

    ' تحديث زاوية الدوران
    Private Sub Tmr_Tick(sender As Object, e As EventArgs)
        angle += 8 ' مقدار حركة القوس
        If angle >= 360 Then angle = 0
        Me.Invalidate() ' أمر بإعادة الرسم
    End Sub

    ' رسم الدائرة المتحركة
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Dim rect As New Rectangle(15, 35, 50, 50)

        ' الدائرة الخلفية الشفافة
        Dim penBack As New Pen(Color.FromArgb(52, 73, 94), 5)
        ' الخط الأخضر المتحرك
        Dim penSpin As New Pen(Color.FromArgb(39, 174, 96), 5)

        penSpin.StartCap = LineCap.Round
        penSpin.EndCap = LineCap.Round

        e.Graphics.DrawArc(penBack, rect, 0, 360)
        e.Graphics.DrawArc(penSpin, rect, angle, 120) ' 120 هو طول الخط الأخضر
    End Sub
End Class
