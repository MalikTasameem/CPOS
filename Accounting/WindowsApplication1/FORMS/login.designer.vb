<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(login))
        Me.passTxt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ShowPassButton = New System.Windows.Forms.Button()
        Me.ExitButton = New System.Windows.Forms.Button()
        Me.EnterButton = New System.Windows.Forms.Button()
        Me.ServersMenuBtn = New System.Windows.Forms.Button()
        Me.RestoreButton = New System.Windows.Forms.Button()
        Me.ServerButton = New System.Windows.Forms.Button()
        Me.ClearButton = New System.Windows.Forms.Button()
        Me.Button0 = New System.Windows.Forms.Button()
        Me.BackButton = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.ServeConnect_LB = New System.Windows.Forms.Label()
        Me.IM_PHONE_1_LB = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.IM_PHONE_2_LB = New System.Windows.Forms.Label()
        Me.SYS_DEVELOPER_LB = New System.Windows.Forms.Label()
        Me.Sys_Maintains_btn = New System.Windows.Forms.Button()
        Me.Button23 = New System.Windows.Forms.Button()
        Me.AnyDesk_Btn = New System.Windows.Forms.Button()
        Me.IM_PHONE_3_LB = New System.Windows.Forms.Label()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'passTxt
        '
        Me.passTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.passTxt.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.passTxt.Location = New System.Drawing.Point(59, 7)
        Me.passTxt.Name = "passTxt"
        Me.passTxt.Size = New System.Drawing.Size(288, 35)
        Me.passTxt.TabIndex = 0
        Me.passTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.passTxt.UseSystemPasswordChar = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(351, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 19)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "كلمة المرور"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.ShowPassButton)
        Me.Panel2.Controls.Add(Me.passTxt)
        Me.Panel2.Controls.Add(Me.ExitButton)
        Me.Panel2.Controls.Add(Me.EnterButton)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(3, 38)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(434, 102)
        Me.Panel2.TabIndex = 14
        '
        'ShowPassButton
        '
        Me.ShowPassButton.BackColor = System.Drawing.SystemColors.Window
        Me.ShowPassButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ShowPassButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose
        Me.ShowPassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ShowPassButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!)
        Me.ShowPassButton.ForeColor = System.Drawing.Color.DarkRed
        Me.ShowPassButton.Location = New System.Drawing.Point(11, 7)
        Me.ShowPassButton.Name = "ShowPassButton"
        Me.ShowPassButton.Size = New System.Drawing.Size(47, 35)
        Me.ShowPassButton.TabIndex = 10
        Me.ShowPassButton.UseVisualStyleBackColor = False
        Me.ShowPassButton.Visible = False
        '
        'ExitButton
        '
        Me.ExitButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose
        Me.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitButton.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitButton.ForeColor = System.Drawing.Color.DarkRed
        Me.ExitButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitButton.Location = New System.Drawing.Point(220, 46)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(210, 52)
        Me.ExitButton.TabIndex = 7
        Me.ExitButton.Text = "خروج 🚪"
        Me.ExitButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitButton.UseVisualStyleBackColor = True
        '
        'EnterButton
        '
        Me.EnterButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EnterButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGreen
        Me.EnterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EnterButton.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnterButton.ForeColor = System.Drawing.Color.DarkGreen
        Me.EnterButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EnterButton.Location = New System.Drawing.Point(3, 46)
        Me.EnterButton.Name = "EnterButton"
        Me.EnterButton.Size = New System.Drawing.Size(210, 52)
        Me.EnterButton.TabIndex = 9
        Me.EnterButton.Text = "دخـــــــول ENTER 🗝️"
        Me.EnterButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EnterButton.UseVisualStyleBackColor = True
        '
        'ServersMenuBtn
        '
        Me.ServersMenuBtn.BackColor = System.Drawing.SystemColors.Control
        Me.ServersMenuBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ServersMenuBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ServersMenuBtn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServersMenuBtn.ForeColor = System.Drawing.Color.DarkRed
        Me.ServersMenuBtn.Location = New System.Drawing.Point(178, 257)
        Me.ServersMenuBtn.Name = "ServersMenuBtn"
        Me.ServersMenuBtn.Size = New System.Drawing.Size(259, 55)
        Me.ServersMenuBtn.TabIndex = 35
        Me.ServersMenuBtn.Text = "الفروع"
        Me.ServersMenuBtn.UseVisualStyleBackColor = False
        Me.ServersMenuBtn.Visible = False
        '
        'RestoreButton
        '
        Me.RestoreButton.BackColor = System.Drawing.SystemColors.Control
        Me.RestoreButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RestoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.RestoreButton.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RestoreButton.ForeColor = System.Drawing.Color.DarkRed
        Me.RestoreButton.Location = New System.Drawing.Point(427, 375)
        Me.RestoreButton.Name = "RestoreButton"
        Me.RestoreButton.Size = New System.Drawing.Size(10, 48)
        Me.RestoreButton.TabIndex = 34
        Me.RestoreButton.Text = "إستعادة نسخة للبيانات"
        Me.RestoreButton.UseVisualStyleBackColor = False
        Me.RestoreButton.Visible = False
        '
        'ServerButton
        '
        Me.ServerButton.BackColor = System.Drawing.Color.SeaGreen
        Me.ServerButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ServerButton.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.ServerButton.ForeColor = System.Drawing.SystemColors.Info
        Me.ServerButton.Location = New System.Drawing.Point(178, 200)
        Me.ServerButton.Name = "ServerButton"
        Me.ServerButton.Size = New System.Drawing.Size(259, 55)
        Me.ServerButton.TabIndex = 33
        Me.ServerButton.Text = "ضبط الخادم"
        Me.ServerButton.UseVisualStyleBackColor = False
        '
        'ClearButton
        '
        Me.ClearButton.BackColor = System.Drawing.Color.IndianRed
        Me.ClearButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ClearButton.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClearButton.ForeColor = System.Drawing.Color.White
        Me.ClearButton.Location = New System.Drawing.Point(120, 314)
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.Size = New System.Drawing.Size(55, 55)
        Me.ClearButton.TabIndex = 32
        Me.ClearButton.Text = "امسح"
        Me.ClearButton.UseVisualStyleBackColor = False
        '
        'Button0
        '
        Me.Button0.BackColor = System.Drawing.SystemColors.Control
        Me.Button0.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button0.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button0.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button0.ForeColor = System.Drawing.Color.DarkRed
        Me.Button0.Location = New System.Drawing.Point(62, 314)
        Me.Button0.Name = "Button0"
        Me.Button0.Size = New System.Drawing.Size(55, 55)
        Me.Button0.TabIndex = 31
        Me.Button0.Text = "0"
        Me.Button0.UseVisualStyleBackColor = False
        '
        'BackButton
        '
        Me.BackButton.BackColor = System.Drawing.SystemColors.Control
        Me.BackButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BackButton.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackButton.ForeColor = System.Drawing.Color.Black
        Me.BackButton.Location = New System.Drawing.Point(178, 144)
        Me.BackButton.Name = "BackButton"
        Me.BackButton.Size = New System.Drawing.Size(259, 55)
        Me.BackButton.TabIndex = 30
        Me.BackButton.Text = "تراجع"
        Me.BackButton.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.SystemColors.Control
        Me.Button8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button8.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.ForeColor = System.Drawing.Color.DarkRed
        Me.Button8.Location = New System.Drawing.Point(62, 257)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(55, 55)
        Me.Button8.TabIndex = 29
        Me.Button8.Text = "8"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.SystemColors.Control
        Me.Button9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button9.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button9.ForeColor = System.Drawing.Color.DarkRed
        Me.Button9.Location = New System.Drawing.Point(120, 257)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(55, 55)
        Me.Button9.TabIndex = 28
        Me.Button9.Text = "9"
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.SystemColors.Control
        Me.Button7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button7.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.ForeColor = System.Drawing.Color.DarkRed
        Me.Button7.Location = New System.Drawing.Point(4, 257)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(55, 55)
        Me.Button7.TabIndex = 27
        Me.Button7.Text = "7"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.DarkRed
        Me.Button1.Location = New System.Drawing.Point(4, 144)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(55, 55)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "1"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.Control
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.DarkRed
        Me.Button2.Location = New System.Drawing.Point(62, 144)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(55, 55)
        Me.Button2.TabIndex = 22
        Me.Button2.Text = "2"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.Control
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button3.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.DarkRed
        Me.Button3.Location = New System.Drawing.Point(120, 144)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(55, 55)
        Me.Button3.TabIndex = 23
        Me.Button3.Text = "3"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.SystemColors.Control
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button4.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.DarkRed
        Me.Button4.Location = New System.Drawing.Point(4, 200)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(55, 55)
        Me.Button4.TabIndex = 24
        Me.Button4.Text = "4"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.SystemColors.Control
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button5.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.Color.DarkRed
        Me.Button5.Location = New System.Drawing.Point(62, 200)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(55, 55)
        Me.Button5.TabIndex = 25
        Me.Button5.Text = "5"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.SystemColors.Control
        Me.Button6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button6.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.DarkRed
        Me.Button6.Location = New System.Drawing.Point(120, 200)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(55, 55)
        Me.Button6.TabIndex = 26
        Me.Button6.Text = "6"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'ServeConnect_LB
        '
        Me.ServeConnect_LB.BackColor = System.Drawing.Color.DarkRed
        Me.ServeConnect_LB.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold)
        Me.ServeConnect_LB.ForeColor = System.Drawing.SystemColors.Control
        Me.ServeConnect_LB.Location = New System.Drawing.Point(49, 1)
        Me.ServeConnect_LB.Name = "ServeConnect_LB"
        Me.ServeConnect_LB.Size = New System.Drawing.Size(390, 35)
        Me.ServeConnect_LB.TabIndex = 11
        Me.ServeConnect_LB.Text = "لا يوجد إتصال بالخادم الرئيسي"
        Me.ServeConnect_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IM_PHONE_1_LB
        '
        Me.IM_PHONE_1_LB.AutoSize = True
        Me.IM_PHONE_1_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_PHONE_1_LB.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.IM_PHONE_1_LB.ForeColor = System.Drawing.Color.Black
        Me.IM_PHONE_1_LB.Location = New System.Drawing.Point(135, 372)
        Me.IM_PHONE_1_LB.Name = "IM_PHONE_1_LB"
        Me.IM_PHONE_1_LB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IM_PHONE_1_LB.Size = New System.Drawing.Size(111, 19)
        Me.IM_PHONE_1_LB.TabIndex = 576
        Me.IM_PHONE_1_LB.Text = "092 - 794 26 00"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(250, 380)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label9.Size = New System.Drawing.Size(140, 21)
        Me.Label9.TabIndex = 575
        Me.Label9.Text = "مركز الدعــم الفنـــي :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'IM_PHONE_2_LB
        '
        Me.IM_PHONE_2_LB.AutoSize = True
        Me.IM_PHONE_2_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_PHONE_2_LB.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.IM_PHONE_2_LB.ForeColor = System.Drawing.Color.Black
        Me.IM_PHONE_2_LB.Location = New System.Drawing.Point(23, 372)
        Me.IM_PHONE_2_LB.Name = "IM_PHONE_2_LB"
        Me.IM_PHONE_2_LB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IM_PHONE_2_LB.Size = New System.Drawing.Size(111, 19)
        Me.IM_PHONE_2_LB.TabIndex = 574
        Me.IM_PHONE_2_LB.Text = "091 - 794 26 00"
        '
        'SYS_DEVELOPER_LB
        '
        Me.SYS_DEVELOPER_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SYS_DEVELOPER_LB.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SYS_DEVELOPER_LB.ForeColor = System.Drawing.Color.Black
        Me.SYS_DEVELOPER_LB.Location = New System.Drawing.Point(23, 417)
        Me.SYS_DEVELOPER_LB.Name = "SYS_DEVELOPER_LB"
        Me.SYS_DEVELOPER_LB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SYS_DEVELOPER_LB.Size = New System.Drawing.Size(223, 21)
        Me.SYS_DEVELOPER_LB.TabIndex = 577
        Me.SYS_DEVELOPER_LB.Text = "مصمــم النظام : م.سراج فكرون"
        '
        'Sys_Maintains_btn
        '
        Me.Sys_Maintains_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Sys_Maintains_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sys_Maintains_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Sys_Maintains_btn.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Sys_Maintains_btn.ForeColor = System.Drawing.Color.Black
        Me.Sys_Maintains_btn.Location = New System.Drawing.Point(235, 314)
        Me.Sys_Maintains_btn.Name = "Sys_Maintains_btn"
        Me.Sys_Maintains_btn.Size = New System.Drawing.Size(202, 55)
        Me.Sys_Maintains_btn.TabIndex = 578
        Me.Sys_Maintains_btn.Text = "مركز صيانــــة النظــــام"
        Me.Sys_Maintains_btn.UseVisualStyleBackColor = False
        '
        'Button23
        '
        Me.Button23.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button23.BackColor = System.Drawing.Color.White
        Me.Button23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button23.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button23.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Button23.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button23.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button23.ForeColor = System.Drawing.Color.DarkRed
        Me.Button23.Location = New System.Drawing.Point(3, 1)
        Me.Button23.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button23.Name = "Button23"
        Me.Button23.Size = New System.Drawing.Size(44, 35)
        Me.Button23.TabIndex = 516
        Me.Button23.UseVisualStyleBackColor = False
        '
        'AnyDesk_Btn
        '
        Me.AnyDesk_Btn.BackColor = System.Drawing.SystemColors.Control
        Me.AnyDesk_Btn.BackgroundImage = CType(resources.GetObject("AnyDesk_Btn.BackgroundImage"), System.Drawing.Image)
        Me.AnyDesk_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.AnyDesk_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AnyDesk_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AnyDesk_Btn.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AnyDesk_Btn.ForeColor = System.Drawing.Color.DarkRed
        Me.AnyDesk_Btn.Location = New System.Drawing.Point(178, 314)
        Me.AnyDesk_Btn.Name = "AnyDesk_Btn"
        Me.AnyDesk_Btn.Size = New System.Drawing.Size(55, 55)
        Me.AnyDesk_Btn.TabIndex = 579
        Me.AnyDesk_Btn.UseVisualStyleBackColor = False
        '
        'IM_PHONE_3_LB
        '
        Me.IM_PHONE_3_LB.AutoSize = True
        Me.IM_PHONE_3_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_PHONE_3_LB.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.IM_PHONE_3_LB.ForeColor = System.Drawing.Color.Black
        Me.IM_PHONE_3_LB.Location = New System.Drawing.Point(135, 393)
        Me.IM_PHONE_3_LB.Name = "IM_PHONE_3_LB"
        Me.IM_PHONE_3_LB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IM_PHONE_3_LB.Size = New System.Drawing.Size(111, 19)
        Me.IM_PHONE_3_LB.TabIndex = 580
        Me.IM_PHONE_3_LB.Text = "092 - 794 26 66"
        '
        'Button15
        '
        Me.Button15.BackColor = System.Drawing.Color.White
        Me.Button15.BackgroundImage = CType(resources.GetObject("Button15.BackgroundImage"), System.Drawing.Image)
        Me.Button15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button15.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button15.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button15.Location = New System.Drawing.Point(4, 314)
        Me.Button15.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(55, 55)
        Me.Button15.TabIndex = 581
        Me.Button15.UseVisualStyleBackColor = False
        '
        'login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(439, 438)
        Me.Controls.Add(Me.Button15)
        Me.Controls.Add(Me.IM_PHONE_3_LB)
        Me.Controls.Add(Me.AnyDesk_Btn)
        Me.Controls.Add(Me.Sys_Maintains_btn)
        Me.Controls.Add(Me.SYS_DEVELOPER_LB)
        Me.Controls.Add(Me.IM_PHONE_1_LB)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.IM_PHONE_2_LB)
        Me.Controls.Add(Me.Button23)
        Me.Controls.Add(Me.ServeConnect_LB)
        Me.Controls.Add(Me.ServersMenuBtn)
        Me.Controls.Add(Me.RestoreButton)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.ServerButton)
        Me.Controls.Add(Me.ClearButton)
        Me.Controls.Add(Me.BackButton)
        Me.Controls.Add(Me.Button0)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تسجيــــل الدخـــــول"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents passTxt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ExitButton As System.Windows.Forms.Button
    Friend WithEvents EnterButton As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ShowPassButton As System.Windows.Forms.Button
    Friend WithEvents ClearButton As System.Windows.Forms.Button
    Friend WithEvents Button0 As System.Windows.Forms.Button
    Friend WithEvents BackButton As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents RestoreButton As System.Windows.Forms.Button
    Friend WithEvents ServerButton As System.Windows.Forms.Button
    Friend WithEvents ServersMenuBtn As System.Windows.Forms.Button
    Friend WithEvents ServeConnect_LB As System.Windows.Forms.Label
    Friend WithEvents Button23 As System.Windows.Forms.Button
    Friend WithEvents IM_PHONE_1_LB As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents IM_PHONE_2_LB As System.Windows.Forms.Label
    Friend WithEvents SYS_DEVELOPER_LB As System.Windows.Forms.Label
    Friend WithEvents Sys_Maintains_btn As System.Windows.Forms.Button
    Friend WithEvents AnyDesk_Btn As System.Windows.Forms.Button
    Friend WithEvents IM_PHONE_3_LB As System.Windows.Forms.Label
    Friend WithEvents Button15 As System.Windows.Forms.Button
End Class
