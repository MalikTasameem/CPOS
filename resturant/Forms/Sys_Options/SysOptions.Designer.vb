<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SysOptions
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.Title_Label = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.F_Panel = New System.Windows.Forms.FlowLayoutPanel()

        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.Button18 = New System.Windows.Forms.Button()
        Me.Tree_AG_Button = New System.Windows.Forms.Button()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.ST_Button = New System.Windows.Forms.Button()
        Me.CardAgent_Btn = New System.Windows.Forms.Button()
        Me.Shurtcut_Btn = New System.Windows.Forms.Button()
        Me.SubPrinterButton = New System.Windows.Forms.Button()
        Me.MK_Btn = New System.Windows.Forms.Button()
        Me.Cities_btn = New System.Windows.Forms.Button()
        Me.TableButton = New System.Windows.Forms.Button()
        Me.Pch_Exp_Btn = New System.Windows.Forms.Button()
        Me.Network_Wacher_btn = New System.Windows.Forms.Button()

        Me.TitleBar_Panel.SuspendLayout()
        Me.F_Panel.SuspendLayout()
        Me.SuspendLayout()

        ' =========================================================
        ' TitleBar_Panel
        ' =========================================================
        Me.TitleBar_Panel.Controls.Add(Me.Title_Label)
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(1000, 45)
        Me.TitleBar_Panel.Tag = "HEADER"
        Me.TitleBar_Panel.Cursor = System.Windows.Forms.Cursors.SizeAll

        ' Title_Label
        Me.Title_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Title_Label.AutoSize = True
        Me.Title_Label.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Title_Label.Location = New System.Drawing.Point(760, 10)
        Me.Title_Label.Name = "Title_Label"
        Me.Title_Label.Size = New System.Drawing.Size(220, 25)
        Me.Title_Label.Tag = "TITLE_TRANSPARENT"
        Me.Title_Label.Text = "خيارات وتحديثات النظام"

        ' ExitFormButton
        Me.ExitFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.ExitFormButton.FlatAppearance.BorderSize = 0
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.Location = New System.Drawing.Point(0, 0)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(50, 45)
        Me.ExitFormButton.Tag = "APP_CONTROL"
        Me.ExitFormButton.Text = "X"
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand

        ' =========================================================
        ' F_Panel (الحاوية الذكية التي تملأ الفراغات)
        ' =========================================================
        Me.F_Panel.AutoScroll = True
        Me.F_Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.F_Panel.Location = New System.Drawing.Point(0, 45)
        Me.F_Panel.Name = "F_Panel"
        Me.F_Panel.Padding = New System.Windows.Forms.Padding(25)
        Me.F_Panel.Size = New System.Drawing.Size(1000, 655)
        Me.F_Panel.Tag = "TRANSPARENT"

        ' ترتيب الأزرار لتبدو مثل صورتك تماماً
        Me.F_Panel.Controls.Add(Me.Button12)
        Me.F_Panel.Controls.Add(Me.Button1)
        Me.F_Panel.Controls.Add(Me.Button13)
        Me.F_Panel.Controls.Add(Me.Button8)
        Me.F_Panel.Controls.Add(Me.Button4)
        Me.F_Panel.Controls.Add(Me.Button17)
        Me.F_Panel.Controls.Add(Me.Button14)
        Me.F_Panel.Controls.Add(Me.Button18)
        Me.F_Panel.Controls.Add(Me.Tree_AG_Button)
        Me.F_Panel.Controls.Add(Me.Button15)
        Me.F_Panel.Controls.Add(Me.ST_Button)
        Me.F_Panel.Controls.Add(Me.CardAgent_Btn)
        Me.F_Panel.Controls.Add(Me.Shurtcut_Btn)
        Me.F_Panel.Controls.Add(Me.SubPrinterButton)
        Me.F_Panel.Controls.Add(Me.MK_Btn)
        Me.F_Panel.Controls.Add(Me.Cities_btn)
        Me.F_Panel.Controls.Add(Me.TableButton)
        Me.F_Panel.Controls.Add(Me.Pch_Exp_Btn)
        Me.F_Panel.Controls.Add(Me.Network_Wacher_btn)

        ' =========================================================
        ' إعدادات الأزرار (بالأيقونات الجميلة)
        ' =========================================================
        Dim btnFont As New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Dim btnSize As New System.Drawing.Size(180, 120) ' حجم مميز للأزرار
        Dim btnMargin As New System.Windows.Forms.Padding(12)

        For Each ctrl As Control In Me.F_Panel.Controls
            If TypeOf ctrl Is Button Then
                Dim btn As Button = DirectCast(ctrl, Button)
                btn.Size = btnSize
                btn.Font = btnFont
                btn.Margin = btnMargin
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
                btn.FlatAppearance.BorderSize = 0
                btn.Cursor = System.Windows.Forms.Cursors.Hand
            End If
        Next

        ' وضع الأيقونات مع النصوص (كما في تصميمك الأصلي)
        Me.Button12.Text = "⚙️" & vbCrLf & "إعدادات عامة" : Me.Button12.Name = "Button12"
        Me.Button1.Text = "🏢" & vbCrLf & "بيانات الشركة" : Me.Button1.Name = "Button1"
        Me.Button13.Text = "🛒" & vbCrLf & "خيارات الكاشير" : Me.Button13.Name = "Button13"
        Me.Button8.Text = "🗄️" & vbCrLf & "قاعدة البيانات" : Me.Button8.Name = "Button8"
        Me.Button4.Text = "🖥️" & vbCrLf & "إعدادات السيرفر" : Me.Button4.Name = "Button4"
        Me.Button17.Text = "🗑️" & vbCrLf & "مسح السجلات" : Me.Button17.Name = "Button17"
        Me.Button14.Text = "👥" & vbCrLf & "إدارة المستخدمين" : Me.Button14.Name = "Button14"
        Me.Button18.Text = "🔐" & vbCrLf & "صلاحيات النظام" : Me.Button18.Name = "Button18"
        Me.Tree_AG_Button.Text = "🌳" & vbCrLf & "شجرة الحسابات" : Me.Tree_AG_Button.Name = "Tree_AG_Button"
        Me.Button15.Text = "💰" & vbCrLf & "إعداد الضرائب" : Me.Button15.Name = "Button15"
        Me.ST_Button.Text = "📦" & vbCrLf & "إدارة المخازن" : Me.ST_Button.Name = "ST_Button"
        Me.CardAgent_Btn.Text = "📇" & vbCrLf & "بطاقات العملاء" : Me.CardAgent_Btn.Name = "CardAgent_Btn"
        Me.Shurtcut_Btn.Text = "⚡" & vbCrLf & "اختصارات سريعة" : Me.Shurtcut_Btn.Name = "Shurtcut_Btn"
        Me.SubPrinterButton.Text = "🖨️" & vbCrLf & "طابعات فرعية" : Me.SubPrinterButton.Name = "SubPrinterButton"
        Me.MK_Btn.Text = "📢" & vbCrLf & "إدارة المسوقين" : Me.MK_Btn.Name = "MK_Btn"
        Me.Cities_btn.Text = "🌍" & vbCrLf & "مناطق السفر" : Me.Cities_btn.Name = "Cities_btn"
        Me.TableButton.Text = "🍽️" & vbCrLf & "صالة الطاولات" : Me.TableButton.Name = "TableButton"
        Me.Pch_Exp_Btn.Text = "💸" & vbCrLf & "مصروفات المشتريات" : Me.Pch_Exp_Btn.Name = "Pch_Exp_Btn"
        Me.Network_Wacher_btn.Text = "🌐" & vbCrLf & "مراقبة الشبكة" : Me.Network_Wacher_btn.Name = "Network_Wacher_btn"

        ' =========================================================
        ' إعدادات الفورم
        ' =========================================================
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 700)
        Me.Controls.Add(Me.F_Panel)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SysOptions"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة النظام"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.F_Panel.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents Title_Label As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents F_Panel As System.Windows.Forms.FlowLayoutPanel

    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Button18 As System.Windows.Forms.Button
    Friend WithEvents Tree_AG_Button As System.Windows.Forms.Button
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents ST_Button As System.Windows.Forms.Button
    Friend WithEvents CardAgent_Btn As System.Windows.Forms.Button
    Friend WithEvents Shurtcut_Btn As System.Windows.Forms.Button
    Friend WithEvents SubPrinterButton As System.Windows.Forms.Button
    Friend WithEvents MK_Btn As System.Windows.Forms.Button
    Friend WithEvents Cities_btn As System.Windows.Forms.Button
    Friend WithEvents TableButton As System.Windows.Forms.Button
    Friend WithEvents Pch_Exp_Btn As System.Windows.Forms.Button
    Friend WithEvents Network_Wacher_btn As System.Windows.Forms.Button
End Class