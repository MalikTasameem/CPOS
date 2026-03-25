Imports System.Runtime.InteropServices

Public Class NotificationForm
    Inherits Form

    Private Shared notifications As New List(Of NotificationForm) ' قائمة لتتبع الإشعارات المفتوحة
    Private lblTitle As Label
    Private lblMessage As Label
    Private btnClose As Button
    Private animationTimer As Timer
    Private isClosing As Boolean = False
    Private position As String = "top"

    Public Sub New(title As String, message As String, position As String)
        Me.position = position
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.Manual
        Me.TopMost = True
        Me.BackColor = Color.DarkSlateGray
        Me.Opacity = 0.9
        Me.Width = 300
        Me.Height = 80
        Me.ShowInTaskbar = False

        ' إنشاء زر الإغلاق
        btnClose = New Button()
        btnClose.Text = "❌"
        btnClose.Font = New Font("Arial", 10, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.BackColor = Color.Red
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Width = 30
        btnClose.Height = 25
        btnClose.Top = 5
        btnClose.Left = Me.Width - btnClose.Width - 5
        AddHandler btnClose.Click, AddressOf CloseNotification
        Me.Controls.Add(btnClose)

        ' إنشاء العنوان
        lblTitle = New Label()
        lblTitle.Text = title
        lblTitle.ForeColor = Color.White
        lblTitle.Font = New Font("Arial", 14, FontStyle.Bold)
        lblTitle.AutoSize = False
        lblTitle.Width = Me.Width - 40
        lblTitle.Height = 25
        lblTitle.TextAlign = ContentAlignment.MiddleLeft
        lblTitle.Top = 5
        lblTitle.Left = 10
        'lblTitle.RightToLeft = True
        Me.Controls.Add(lblTitle)

        ' إنشاء المحتوى
        lblMessage = New Label()
        lblMessage.Text = message
        lblMessage.ForeColor = Color.WhiteSmoke
        lblMessage.Font = New Font("Arial", 12, FontStyle.Regular)
        lblMessage.AutoSize = False
        lblMessage.Width = Me.Width - 20
        lblMessage.Height = Me.Height - 30
        lblMessage.TextAlign = ContentAlignment.MiddleCenter
        lblMessage.Top = 30
        lblMessage.Left = 10
        'lblMessage.RightToLeft = True
        Me.Controls.Add(lblMessage)

        ' ضبط موقع الإشعار
        Dim screenBounds = Screen.PrimaryScreen.WorkingArea
        Me.Left = screenBounds.Width - Me.Width - 10

        ' تحديد موضع الإشعار الجديد بناءً على الإشعارات السابقة
        Dim offset = 10 ' المسافة بين الإشعارات
        Dim totalNotifications = notifications.Count
        If position = "top" Then
            Me.Top = -Me.Height - (totalNotifications * (Me.Height + offset)) ' وضعه فوق الشاشة
        Else
            Me.Top = screenBounds.Height + (totalNotifications * (Me.Height + offset)) ' وضعه أسفل الشاشة
        End If

        ' مؤقت الحركة
        animationTimer = New Timer()
        animationTimer.Interval = 10
        AddHandler animationTimer.Tick, AddressOf AnimateNotification
    End Sub

    ' دالة لإظهار الإشعار
    Public Sub ShowNotification()
        notifications.Add(Me)
        animationTimer.Start()
        Me.Show()
    End Sub

    ' حركة الانزلاق للإشعار عند الظهور والاختفاء
    Private Sub AnimateNotification(sender As Object, e As EventArgs)
        Dim targetPosition As Integer
        Dim screenBounds = Screen.PrimaryScreen.WorkingArea
        Dim index = notifications.IndexOf(Me)
        Dim offset = 10 ' المسافة بين الإشعارات

        If position = "top" Then
            targetPosition = 10 + (index * (Me.Height + offset))
        Else
            targetPosition = screenBounds.Height - Me.Height - 10 - (index * (Me.Height + offset))
        End If

        If Not isClosing Then
            If (position = "top" AndAlso Me.Top < targetPosition) OrElse (position = "bottom" AndAlso Me.Top > targetPosition) Then
                Me.Top += If(position = "top", 5, -5)
            Else
                animationTimer.Stop()
            End If
        Else
            If (position = "top" AndAlso Me.Top > -Me.Height) OrElse (position = "bottom" AndAlso Me.Top < screenBounds.Height) Then
                Me.Top += If(position = "top", -5, 5)
            Else
                animationTimer.Stop()
                notifications.Remove(Me)
                RepositionNotifications()
                Me.Close()
            End If
        End If
    End Sub

    ' دالة لإعادة ترتيب الإشعارات عند الإغلاق
    Private Sub RepositionNotifications()
        Dim offset = 10
        Dim screenBounds = Screen.PrimaryScreen.WorkingArea

        For i = 0 To notifications.Count - 1
            Dim targetPosition As Integer = If(position = "top",
                10 + (i * (Me.Height + offset)),
                screenBounds.Height - Me.Height - 10 - (i * (Me.Height + offset)))

            notifications(i).Top = targetPosition
        Next
    End Sub

    ' دالة لإغلاق الإشعار عند الضغط على زر الإغلاق
    Private Sub CloseNotification(sender As Object, e As EventArgs)
        isClosing = True
        animationTimer.Start()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'NotificationForm
        '
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Name = "NotificationForm"
        Me.ResumeLayout(False)

    End Sub

End Class
