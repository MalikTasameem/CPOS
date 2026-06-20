Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices

<DefaultEvent("ButtonClick")>
Public Class SplitButton
    Inherits UserControl

    Private mainButton As New Button()
    Private dropButton As New Button()
    Private _menu As New ContextMenuStrip()
    Private _buttonImage As Image = Nothing



    '------------------------------------------------------------------------------------------------------------------
    '-------------------------------------------------------------------
    ' إعادة تعريف خاصية Font لتطبيقها على كل الأدوات الداخلية
    '-------------------------------------------------------------------
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(value As Font)
            MyBase.Font = value
            ApplyFontToAllControls(Me, value)
        End Set
    End Property


    '-------------------------------------------------------------------
    ' دالة مساعدة تطبق الخط على كل العناصر الداخلية
    '-------------------------------------------------------------------
    Private Sub ApplyFontToAllControls(parent As Control, font As Font)
        For Each ctrl As Control In parent.Controls
            ctrl.Font = font
            ' في حال وجود أدوات داخل أدوات أخرى
            If ctrl.HasChildren Then
                ApplyFontToAllControls(ctrl, font)
            End If
        Next
    End Sub


    '------------------------------------------------------------------------------------------------------------------



    ' 🔹 API لإظهار القائمة بتأثير انزلاقي (Animation)
    <DllImport("user32.dll")>
    Private Shared Function TrackPopupMenuEx(hMenu As IntPtr, uFlags As Integer, x As Integer, y As Integer, hWnd As IntPtr, ByVal lpTpm As IntPtr) As Boolean
    End Function

    Private Const TPM_RIGHTALIGN As Integer = &H8
    Private Const TPM_LEFTALIGN As Integer = &H0
    Private Const TPM_VERTICAL As Integer = &H40
    Private Const TPM_RETURNCMD As Integer = &H100
    Private Const TPM_RIGHTBUTTON As Integer = &H2
    Private Const TPM_BOTTOMALIGN As Integer = &H20
    Private Const TPM_TOPALIGN As Integer = &H0
    Private Const TPM_NOANIMATION As Integer = &H4000

    ' الحدث الرئيسي عند الضغط على الزر الأساسي
    Public Event ButtonClick As EventHandler

    ' خاصية لتحديد القائمة المنسدلة
    <Browsable(True)>
    Public Property DropDownMenu As ContextMenuStrip
        Get
            Return _menu
        End Get
        Set(value As ContextMenuStrip)
            _menu = value
        End Set
    End Property

    ' خاصية لتغيير النص
    <Browsable(True)>
    Public Property ButtonText As String
        Get
            Return mainButton.Text
        End Get
        Set(value As String)
            mainButton.Text = value
        End Set
    End Property

    ' خاصية لإضافة صورة للزر الرئيسي
    <Browsable(True)>
    Public Property ButtonImage As Image
        Get
            Return _buttonImage
        End Get
        Set(value As Image)
            _buttonImage = value
            mainButton.Image = _buttonImage
            mainButton.ImageAlign = If(Me.RightToLeft = RightToLeft.Yes, ContentAlignment.MiddleRight, ContentAlignment.MiddleLeft)
            mainButton.TextAlign = If(Me.RightToLeft = RightToLeft.Yes, ContentAlignment.MiddleLeft, ContentAlignment.MiddleRight)
        End Set
    End Property

    Public Sub New()
        Me.Height = 36
        Me.Width = 150
        Me.BackColor = SystemColors.Control

        ' إعداد الزر الرئيسي
        With mainButton
            .Dock = DockStyle.Fill
            .Text = "زر رئيسي"
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .BackColor = Color.WhiteSmoke
            .ForeColor = Color.Black
            .Font = New Font("Segoe UI", 9, FontStyle.Regular)
            .TextAlign = ContentAlignment.MiddleRight
            .RightToLeft = RightToLeft.Yes
        End With
        AddHandler mainButton.Click, AddressOf MainButton_Click
        AddHandler mainButton.MouseEnter, AddressOf Button_MouseEnter
        AddHandler mainButton.MouseLeave, AddressOf Button_MouseLeave

        ' إعداد زر السهم ▼
        With dropButton
            .Dock = DockStyle.Left
            .Width = 28
            .Text = "▼"
            .FlatStyle = FlatStyle.Flat
            .FlatAppearance.BorderSize = 0
            .BackColor = Color.WhiteSmoke
            .ForeColor = Color.Black
            .Font = New Font("Segoe UI", 8, FontStyle.Bold)
        End With
        AddHandler dropButton.Click, AddressOf DropButton_Click
        AddHandler dropButton.MouseEnter, AddressOf Button_MouseEnter
        AddHandler dropButton.MouseLeave, AddressOf Button_MouseLeave

        ' إضافة الزرين للأداة
        Me.Controls.Add(mainButton)
        Me.Controls.Add(dropButton)

        ' إعداد القائمة الافتراضية
        _menu.RightToLeft = RightToLeft.Yes
        _menu.Font = New Font("Segoe UI", 9)

        ' محاذاة من اليمين لليسار للأداة ككل
        Me.RightToLeft = RightToLeft.Yes
        Me.Padding = New Padding(1)
    End Sub

    Private Sub MainButton_Click(sender As Object, e As EventArgs)
        RaiseEvent ButtonClick(Me, e)
    End Sub

    ' 🔹 عرض القائمة المنسدلة مع تأثير انزلاقي خفيف
    Private Sub DropButton_Click(sender As Object, e As EventArgs)
        If _menu IsNot Nothing AndAlso _menu.Items.Count > 0 Then
            Dim screenPoint As Point = Me.PointToScreen(New Point(0, Me.Height))
            _menu.Show(screenPoint, ToolStripDropDownDirection.BelowRight)
        End If
    End Sub

    ' تغيير اللون عند المرور بالماوس
    Private Sub Button_MouseEnter(sender As Object, e As EventArgs)
        'Dim btn As Button = DirectCast(sender, Button)
        'btn.BackColor = Color.LightGray
    End Sub

    Private Sub Button_MouseLeave(sender As Object, e As EventArgs)
        'Dim btn As Button = DirectCast(sender, Button)
        'btn.BackColor = Color.WhiteSmoke
    End Sub

    ' 🔹 إضافة عنصر جديد للقائمة برمجيًا
    Public Sub AddMenuItem(text As String, Optional icon As Image = Nothing, Optional onClick As EventHandler = Nothing)
        Dim item As New ToolStripMenuItem(text)
        If icon IsNot Nothing Then item.Image = icon
        If onClick IsNot Nothing Then AddHandler item.Click, onClick
        _menu.Items.Add(item)
    End Sub

End Class
