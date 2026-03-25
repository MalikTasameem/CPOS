<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class login
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(login))
        Me.passTxt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ServeConnect_LB = New System.Windows.Forms.Label()
        Me.Sys_Maintains_btn = New System.Windows.Forms.Button()
        Me.ServerButton = New System.Windows.Forms.Button()
        Me.ClearButton = New System.Windows.Forms.Button()
        Me.ExitButton = New System.Windows.Forms.Button()
        Me.ServersMenuBtn = New System.Windows.Forms.Button()
        Me.RestoreButton = New System.Windows.Forms.Button()
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
        Me.IM_PHONE_1_LB = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.IM_PHONE_2_LB = New System.Windows.Forms.Label()
        Me.SYS_DEVELOPER_LB = New System.Windows.Forms.Label()
        Me.IM_PHONE_3_LB = New System.Windows.Forms.Label()
        Me.AnyDesk_Btn = New System.Windows.Forms.Button()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.Button23 = New System.Windows.Forms.Button()
        Me.EnterButton = New System.Windows.Forms.Button()
        Me.ShowPassButton = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'passTxt
        '
        Me.passTxt.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.passTxt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.passTxt.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.passTxt.Location = New System.Drawing.Point(30, 110)
        Me.passTxt.Name = "passTxt"
        Me.passTxt.Size = New System.Drawing.Size(300, 43)
        Me.passTxt.TabIndex = 0
        Me.passTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.passTxt.UseSystemPasswordChar = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(65, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(257, 37)
        Me.Label1.TabIndex = 1
        Me.Label1.Tag = "TitleBar"
        Me.Label1.Text = "تسجيل الدخول للنظام"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.ServeConnect_LB)
        Me.Panel2.Controls.Add(Me.Button23)
        Me.Panel2.Controls.Add(Me.Sys_Maintains_btn)
        Me.Panel2.Controls.Add(Me.ServerButton)
        Me.Panel2.Controls.Add(Me.ClearButton)
        Me.Panel2.Controls.Add(Me.ExitButton)
        Me.Panel2.Controls.Add(Me.EnterButton)
        Me.Panel2.Controls.Add(Me.ShowPassButton)
        Me.Panel2.Controls.Add(Me.passTxt)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(360, 480)
        Me.Panel2.TabIndex = 14
        '
        'ServeConnect_LB
        '
        Me.ServeConnect_LB.BackColor = System.Drawing.Color.DarkRed
        Me.ServeConnect_LB.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ServeConnect_LB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ServeConnect_LB.ForeColor = System.Drawing.Color.White
        Me.ServeConnect_LB.Location = New System.Drawing.Point(0, 447)
        Me.ServeConnect_LB.Name = "ServeConnect_LB"
        Me.ServeConnect_LB.Size = New System.Drawing.Size(360, 33)
        Me.ServeConnect_LB.TabIndex = 11
        Me.ServeConnect_LB.Text = "لا يوجد إتصال بالخادم"
        Me.ServeConnect_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Sys_Maintains_btn
        '
        Me.Sys_Maintains_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Sys_Maintains_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sys_Maintains_btn.FlatAppearance.BorderSize = 0
        Me.Sys_Maintains_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Sys_Maintains_btn.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Sys_Maintains_btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Sys_Maintains_btn.Location = New System.Drawing.Point(185, 320)
        Me.Sys_Maintains_btn.Name = "Sys_Maintains_btn"
        Me.Sys_Maintains_btn.Size = New System.Drawing.Size(145, 45)
        Me.Sys_Maintains_btn.TabIndex = 578
        Me.Sys_Maintains_btn.Text = "صيانة النظام"
        Me.Sys_Maintains_btn.UseVisualStyleBackColor = False
        '
        'ServerButton
        '
        Me.ServerButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ServerButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ServerButton.FlatAppearance.BorderSize = 0
        Me.ServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ServerButton.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ServerButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ServerButton.Location = New System.Drawing.Point(30, 320)
        Me.ServerButton.Name = "ServerButton"
        Me.ServerButton.Size = New System.Drawing.Size(145, 45)
        Me.ServerButton.TabIndex = 33
        Me.ServerButton.Text = "ضبط الخادم"
        Me.ServerButton.UseVisualStyleBackColor = False
        '
        'ClearButton
        '
        Me.ClearButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ClearButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ClearButton.FlatAppearance.BorderSize = 0
        Me.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ClearButton.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ClearButton.ForeColor = System.Drawing.Color.White
        Me.ClearButton.Location = New System.Drawing.Point(185, 255)
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.Size = New System.Drawing.Size(145, 50)
        Me.ClearButton.TabIndex = 32
        Me.ClearButton.Text = "تفريغ"
        Me.ClearButton.UseVisualStyleBackColor = False
        '
        'ExitButton
        '
        Me.ExitButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.ExitButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitButton.FlatAppearance.BorderSize = 0
        Me.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitButton.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitButton.ForeColor = System.Drawing.Color.White
        Me.ExitButton.Location = New System.Drawing.Point(30, 255)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(145, 50)
        Me.ExitButton.TabIndex = 7
        Me.ExitButton.Tag = "Close"
        Me.ExitButton.Text = "خروج"
        Me.ExitButton.UseVisualStyleBackColor = False
        '
        'ServersMenuBtn
        '
        Me.ServersMenuBtn.Location = New System.Drawing.Point(0, 0)
        Me.ServersMenuBtn.Name = "ServersMenuBtn"
        Me.ServersMenuBtn.Size = New System.Drawing.Size(0, 0)
        Me.ServersMenuBtn.TabIndex = 35
        Me.ServersMenuBtn.Visible = False
        '
        'RestoreButton
        '
        Me.RestoreButton.Location = New System.Drawing.Point(0, 0)
        Me.RestoreButton.Name = "RestoreButton"
        Me.RestoreButton.Size = New System.Drawing.Size(0, 0)
        Me.RestoreButton.TabIndex = 34
        Me.RestoreButton.Visible = False
        '
        'Button0
        '
        Me.Button0.BackColor = System.Drawing.Color.White
        Me.Button0.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button0.FlatAppearance.BorderSize = 0
        Me.Button0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button0.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Button0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Button0.Location = New System.Drawing.Point(489, 305)
        Me.Button0.Name = "Button0"
        Me.Button0.Size = New System.Drawing.Size(90, 80)
        Me.Button0.TabIndex = 31
        Me.Button0.Text = "0"
        Me.Button0.UseVisualStyleBackColor = False
        '
        'BackButton
        '
        Me.BackButton.BackColor = System.Drawing.Color.White
        Me.BackButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BackButton.FlatAppearance.BorderSize = 0
        Me.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BackButton.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.BackButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.BackButton.Location = New System.Drawing.Point(389, 305)
        Me.BackButton.Name = "BackButton"
        Me.BackButton.Size = New System.Drawing.Size(90, 80)
        Me.BackButton.TabIndex = 30
        Me.BackButton.Text = "⌫"
        Me.BackButton.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.White
        Me.Button8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button8.FlatAppearance.BorderSize = 0
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Button8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Button8.Location = New System.Drawing.Point(489, 215)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(90, 80)
        Me.Button8.TabIndex = 29
        Me.Button8.Text = "8"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.Color.White
        Me.Button9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button9.FlatAppearance.BorderSize = 0
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button9.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Button9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Button9.Location = New System.Drawing.Point(589, 215)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(90, 80)
        Me.Button9.TabIndex = 28
        Me.Button9.Text = "9"
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.White
        Me.Button7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button7.FlatAppearance.BorderSize = 0
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Button7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Button7.Location = New System.Drawing.Point(389, 215)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(90, 80)
        Me.Button7.TabIndex = 27
        Me.Button7.Text = "7"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(389, 35)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 80)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "1"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Button2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(489, 35)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(90, 80)
        Me.Button2.TabIndex = 22
        Me.Button2.Text = "2"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.White
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Button3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Button3.Location = New System.Drawing.Point(589, 35)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(90, 80)
        Me.Button3.TabIndex = 23
        Me.Button3.Text = "3"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.White
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Button4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Button4.Location = New System.Drawing.Point(389, 125)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(90, 80)
        Me.Button4.TabIndex = 24
        Me.Button4.Text = "4"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.White
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Button5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Button5.Location = New System.Drawing.Point(489, 125)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(90, 80)
        Me.Button5.TabIndex = 25
        Me.Button5.Text = "5"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.White
        Me.Button6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Button6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Button6.Location = New System.Drawing.Point(589, 125)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(90, 80)
        Me.Button6.TabIndex = 26
        Me.Button6.Text = "6"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'IM_PHONE_1_LB
        '
        Me.IM_PHONE_1_LB.AutoSize = True
        Me.IM_PHONE_1_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.IM_PHONE_1_LB.ForeColor = System.Drawing.Color.Gray
        Me.IM_PHONE_1_LB.Location = New System.Drawing.Point(595, 447)
        Me.IM_PHONE_1_LB.Name = "IM_PHONE_1_LB"
        Me.IM_PHONE_1_LB.Size = New System.Drawing.Size(84, 15)
        Me.IM_PHONE_1_LB.TabIndex = 576
        Me.IM_PHONE_1_LB.Text = "092 - 794 26 00"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.ForeColor = System.Drawing.Color.Gray
        Me.Label9.Location = New System.Drawing.Point(570, 398)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label9.Size = New System.Drawing.Size(105, 15)
        Me.Label9.TabIndex = 575
        Me.Label9.Text = "مركز الدعــم الفنـــي :"
        '
        'IM_PHONE_2_LB
        '
        Me.IM_PHONE_2_LB.AutoSize = True
        Me.IM_PHONE_2_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.IM_PHONE_2_LB.ForeColor = System.Drawing.Color.Gray
        Me.IM_PHONE_2_LB.Location = New System.Drawing.Point(595, 432)
        Me.IM_PHONE_2_LB.Name = "IM_PHONE_2_LB"
        Me.IM_PHONE_2_LB.Size = New System.Drawing.Size(84, 15)
        Me.IM_PHONE_2_LB.TabIndex = 574
        Me.IM_PHONE_2_LB.Text = "091 - 794 26 00"
        '
        'SYS_DEVELOPER_LB
        '
        Me.SYS_DEVELOPER_LB.AutoSize = True
        Me.SYS_DEVELOPER_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SYS_DEVELOPER_LB.ForeColor = System.Drawing.Color.Silver
        Me.SYS_DEVELOPER_LB.Location = New System.Drawing.Point(526, 462)
        Me.SYS_DEVELOPER_LB.Name = "SYS_DEVELOPER_LB"
        Me.SYS_DEVELOPER_LB.Size = New System.Drawing.Size(153, 15)
        Me.SYS_DEVELOPER_LB.TabIndex = 577
        Me.SYS_DEVELOPER_LB.Text = "مصمــم النظام : م.سراج فكرون"
        '
        'IM_PHONE_3_LB
        '
        Me.IM_PHONE_3_LB.AutoSize = True
        Me.IM_PHONE_3_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.IM_PHONE_3_LB.ForeColor = System.Drawing.Color.Gray
        Me.IM_PHONE_3_LB.Location = New System.Drawing.Point(595, 417)
        Me.IM_PHONE_3_LB.Name = "IM_PHONE_3_LB"
        Me.IM_PHONE_3_LB.Size = New System.Drawing.Size(84, 15)
        Me.IM_PHONE_3_LB.TabIndex = 580
        Me.IM_PHONE_3_LB.Text = "092 - 794 26 66"
        '
        'AnyDesk_Btn
        '
        Me.AnyDesk_Btn.BackColor = System.Drawing.Color.Transparent
        Me.AnyDesk_Btn.BackgroundImage = CType(resources.GetObject("AnyDesk_Btn.BackgroundImage"), System.Drawing.Image)
        Me.AnyDesk_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.AnyDesk_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AnyDesk_Btn.FlatAppearance.BorderSize = 0
        Me.AnyDesk_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AnyDesk_Btn.Location = New System.Drawing.Point(389, 394)
        Me.AnyDesk_Btn.Name = "AnyDesk_Btn"
        Me.AnyDesk_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AnyDesk_Btn.Size = New System.Drawing.Size(90, 76)
        Me.AnyDesk_Btn.TabIndex = 579
        Me.AnyDesk_Btn.UseVisualStyleBackColor = False
        '
        'Button15
        '
        Me.Button15.BackColor = System.Drawing.Color.White
        Me.Button15.BackgroundImage = CType(resources.GetObject("Button15.BackgroundImage"), System.Drawing.Image)
        Me.Button15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button15.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button15.FlatAppearance.BorderSize = 0
        Me.Button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button15.Location = New System.Drawing.Point(589, 305)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(90, 80)
        Me.Button15.TabIndex = 581
        Me.Button15.UseVisualStyleBackColor = False
        '
        'Button23
        '
        Me.Button23.BackColor = System.Drawing.Color.Transparent
        Me.Button23.BackgroundImage = Global.resturant.My.Resources.Resources.if_refresh_1608809
        Me.Button23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button23.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button23.FlatAppearance.BorderSize = 0
        Me.Button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button23.Location = New System.Drawing.Point(30, 385)
        Me.Button23.Name = "Button23"
        Me.Button23.Size = New System.Drawing.Size(35, 35)
        Me.Button23.TabIndex = 516
        Me.Button23.UseVisualStyleBackColor = False
        '
        'EnterButton
        '
        Me.EnterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.EnterButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EnterButton.FlatAppearance.BorderSize = 0
        Me.EnterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EnterButton.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.EnterButton.ForeColor = System.Drawing.Color.White
        Me.EnterButton.Image = Global.resturant.My.Resources.Resources.if_hand_cursor_2639827
        Me.EnterButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EnterButton.Location = New System.Drawing.Point(30, 180)
        Me.EnterButton.Name = "EnterButton"
        Me.EnterButton.Size = New System.Drawing.Size(300, 60)
        Me.EnterButton.TabIndex = 9
        Me.EnterButton.Tag = "Save"
        Me.EnterButton.Text = "دخـــــــول ENTER"
        Me.EnterButton.UseVisualStyleBackColor = False
        '
        'ShowPassButton
        '
        Me.ShowPassButton.BackColor = System.Drawing.Color.White
        Me.ShowPassButton.BackgroundImage = Global.resturant.My.Resources.Resources.show_hide_password
        Me.ShowPassButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ShowPassButton.FlatAppearance.BorderSize = 0
        Me.ShowPassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ShowPassButton.Location = New System.Drawing.Point(290, 114)
        Me.ShowPassButton.Name = "ShowPassButton"
        Me.ShowPassButton.Size = New System.Drawing.Size(35, 35)
        Me.ShowPassButton.TabIndex = 10
        Me.ShowPassButton.UseVisualStyleBackColor = False
        Me.ShowPassButton.Visible = False
        '
        'Button10
        '
        Me.Button10.BackColor = System.Drawing.Color.Transparent
        Me.Button10.BackgroundImage = CType(resources.GetObject("Button10.BackgroundImage"), System.Drawing.Image)
        Me.Button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button10.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button10.FlatAppearance.BorderSize = 0
        Me.Button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button10.Location = New System.Drawing.Point(489, 394)
        Me.Button10.Name = "Button10"
        Me.Button10.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button10.Size = New System.Drawing.Size(90, 76)
        Me.Button10.TabIndex = 582
        Me.Button10.UseVisualStyleBackColor = False
        '
        'login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(709, 480)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.SYS_DEVELOPER_LB)
        Me.Controls.Add(Me.IM_PHONE_3_LB)
        Me.Controls.Add(Me.IM_PHONE_1_LB)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.IM_PHONE_2_LB)
        Me.Controls.Add(Me.AnyDesk_Btn)
        Me.Controls.Add(Me.Button15)
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
        Me.Controls.Add(Me.ServersMenuBtn)
        Me.Controls.Add(Me.RestoreButton)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
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
    Friend WithEvents Button10 As Button
End Class
