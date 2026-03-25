<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerOptions
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerOptions))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmbSeverName = New System.Windows.Forms.ComboBox()
        Me.CmbAuth = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbDataBase = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtUserName = New System.Windows.Forms.TextBox()
        Me.UserPassGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtPassWord = New System.Windows.Forms.TextBox()
        Me.Serv_Desc_txt = New System.Windows.Forms.TextBox()
        Me.ServerNameErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.AuthErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.DBNameErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.UnameErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.UpassErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.SaveEdit_Btn = New DevComponents.DotNetBar.ButtonX()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DataBaseNameLabel = New System.Windows.Forms.Label()
        Me.AuthenticationLabel = New System.Windows.Forms.Label()
        Me.ServerNameLabel = New System.Windows.Forms.Label()
        Me.PassWordLabel = New System.Windows.Forms.Label()
        Me.UserNameLabel = New System.Windows.Forms.Label()
        Me.TrailCheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Serv_Desc_Lb = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Server_Choese_Server_txt = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.DB_Choese_Server_txt = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.isCenterSys_CB = New System.Windows.Forms.CheckBox()
        Me.SERVER_IP_txt = New System.Windows.Forms.TextBox()
        Me.isSubSys_CB = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Switch_PC_Activation_btn = New DevComponents.DotNetBar.ButtonX()
        Me.UseServer_Btn = New DevComponents.DotNetBar.ButtonX()
        Me.ServMenu_Btn = New DevComponents.DotNetBar.ButtonX()
        Me.Attach_Database_btn = New DevComponents.DotNetBar.ButtonX()
        Me.NewDBTextBox = New System.Windows.Forms.TextBox()
        Me.is_CHECKED_LB = New System.Windows.Forms.Label()
        Me.Check_Btn = New DevComponents.DotNetBar.ButtonX()
        Me.Button23 = New System.Windows.Forms.Button()
        Me.Online_Con_txt = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Refresh_btn = New DevComponents.DotNetBar.ButtonX()
        Me.UserPassGroupBox.SuspendLayout()
        CType(Me.ServerNameErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AuthErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DBNameErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UnameErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UpassErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "إسم الشركة \ الفرع :"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 21)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "الخـــادم :"
        '
        'CmbSeverName
        '
        Me.CmbSeverName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmbSeverName.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbSeverName.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CmbSeverName.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.CmbSeverName.FormattingEnabled = True
        Me.CmbSeverName.Location = New System.Drawing.Point(96, 36)
        Me.CmbSeverName.Name = "CmbSeverName"
        Me.CmbSeverName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmbSeverName.Size = New System.Drawing.Size(267, 29)
        Me.CmbSeverName.TabIndex = 3
        '
        'CmbAuth
        '
        Me.CmbAuth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmbAuth.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbAuth.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CmbAuth.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.CmbAuth.FormattingEnabled = True
        Me.CmbAuth.Items.AddRange(New Object() {"Windows Authentication", "SQL Server Authentication"})
        Me.CmbAuth.Location = New System.Drawing.Point(96, 69)
        Me.CmbAuth.Name = "CmbAuth"
        Me.CmbAuth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmbAuth.Size = New System.Drawing.Size(304, 29)
        Me.CmbAuth.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(-1, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 21)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = " المصادقـــة :"
        '
        'CmbDataBase
        '
        Me.CmbDataBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmbDataBase.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbDataBase.DropDownHeight = 180
        Me.CmbDataBase.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CmbDataBase.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.CmbDataBase.FormattingEnabled = True
        Me.CmbDataBase.IntegralHeight = False
        Me.CmbDataBase.Location = New System.Drawing.Point(174, 196)
        Me.CmbDataBase.Name = "CmbDataBase"
        Me.CmbDataBase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmbDataBase.Size = New System.Drawing.Size(224, 29)
        Me.CmbDataBase.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(64, 200)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 21)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "قاعدة البيانات :"
        '
        'TxtUserName
        '
        Me.TxtUserName.Location = New System.Drawing.Point(8, 20)
        Me.TxtUserName.Name = "TxtUserName"
        Me.TxtUserName.Size = New System.Drawing.Size(257, 29)
        Me.TxtUserName.TabIndex = 8
        Me.TxtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'UserPassGroupBox
        '
        Me.UserPassGroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserPassGroupBox.Controls.Add(Me.Label6)
        Me.UserPassGroupBox.Controls.Add(Me.Label5)
        Me.UserPassGroupBox.Controls.Add(Me.TxtPassWord)
        Me.UserPassGroupBox.Controls.Add(Me.TxtUserName)
        Me.UserPassGroupBox.Enabled = False
        Me.UserPassGroupBox.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserPassGroupBox.Location = New System.Drawing.Point(2, 103)
        Me.UserPassGroupBox.Name = "UserPassGroupBox"
        Me.UserPassGroupBox.Size = New System.Drawing.Size(398, 89)
        Me.UserPassGroupBox.TabIndex = 9
        Me.UserPassGroupBox.TabStop = False
        Me.UserPassGroupBox.Text = "تفاصيل الإتصال :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(268, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 20)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "كلمة المرور :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(268, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 20)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "اسم المستخدم :"
        '
        'TxtPassWord
        '
        Me.TxtPassWord.Location = New System.Drawing.Point(8, 50)
        Me.TxtPassWord.Name = "TxtPassWord"
        Me.TxtPassWord.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassWord.Size = New System.Drawing.Size(257, 29)
        Me.TxtPassWord.TabIndex = 9
        Me.TxtPassWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtPassWord.UseSystemPasswordChar = True
        '
        'Serv_Desc_txt
        '
        Me.Serv_Desc_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Serv_Desc_txt.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.Serv_Desc_txt.Location = New System.Drawing.Point(134, 3)
        Me.Serv_Desc_txt.Name = "Serv_Desc_txt"
        Me.Serv_Desc_txt.Size = New System.Drawing.Size(546, 29)
        Me.Serv_Desc_txt.TabIndex = 1
        '
        'ServerNameErrorProvider
        '
        Me.ServerNameErrorProvider.ContainerControl = Me
        Me.ServerNameErrorProvider.RightToLeft = True
        '
        'AuthErrorProvider
        '
        Me.AuthErrorProvider.ContainerControl = Me
        Me.AuthErrorProvider.RightToLeft = True
        '
        'DBNameErrorProvider
        '
        Me.DBNameErrorProvider.ContainerControl = Me
        Me.DBNameErrorProvider.RightToLeft = True
        '
        'UnameErrorProvider
        '
        Me.UnameErrorProvider.ContainerControl = Me
        Me.UnameErrorProvider.RightToLeft = True
        '
        'UpassErrorProvider
        '
        Me.UpassErrorProvider.ContainerControl = Me
        Me.UpassErrorProvider.RightToLeft = True
        '
        'SaveEdit_Btn
        '
        Me.SaveEdit_Btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.SaveEdit_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveEdit_Btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.SaveEdit_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveEdit_Btn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SaveEdit_Btn.Location = New System.Drawing.Point(581, 401)
        Me.SaveEdit_Btn.Name = "SaveEdit_Btn"
        Me.SaveEdit_Btn.Size = New System.Drawing.Size(98, 40)
        Me.SaveEdit_Btn.TabIndex = 12
        Me.SaveEdit_Btn.Text = "إدراج الخادم"
        Me.SaveEdit_Btn.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(188, 120)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 15)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "قاعدة البيانات :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(208, 95)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 15)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "المصادقة :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(225, 66)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(43, 15)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "الخادم :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(200, 178)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 15)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "كلمة المرور :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(182, 150)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(84, 15)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "اسم المستخدم :"
        '
        'DataBaseNameLabel
        '
        Me.DataBaseNameLabel.AutoSize = True
        Me.DataBaseNameLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataBaseNameLabel.ForeColor = System.Drawing.Color.Blue
        Me.DataBaseNameLabel.Location = New System.Drawing.Point(7, 119)
        Me.DataBaseNameLabel.Name = "DataBaseNameLabel"
        Me.DataBaseNameLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DataBaseNameLabel.Size = New System.Drawing.Size(18, 17)
        Me.DataBaseNameLabel.TabIndex = 20
        Me.DataBaseNameLabel.Text = "--"
        '
        'AuthenticationLabel
        '
        Me.AuthenticationLabel.AutoSize = True
        Me.AuthenticationLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AuthenticationLabel.ForeColor = System.Drawing.Color.Blue
        Me.AuthenticationLabel.Location = New System.Drawing.Point(7, 94)
        Me.AuthenticationLabel.Name = "AuthenticationLabel"
        Me.AuthenticationLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AuthenticationLabel.Size = New System.Drawing.Size(18, 17)
        Me.AuthenticationLabel.TabIndex = 19
        Me.AuthenticationLabel.Text = "--"
        '
        'ServerNameLabel
        '
        Me.ServerNameLabel.AutoSize = True
        Me.ServerNameLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServerNameLabel.ForeColor = System.Drawing.Color.Blue
        Me.ServerNameLabel.Location = New System.Drawing.Point(6, 65)
        Me.ServerNameLabel.Name = "ServerNameLabel"
        Me.ServerNameLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ServerNameLabel.Size = New System.Drawing.Size(18, 17)
        Me.ServerNameLabel.TabIndex = 18
        Me.ServerNameLabel.Text = "--"
        '
        'PassWordLabel
        '
        Me.PassWordLabel.AutoSize = True
        Me.PassWordLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassWordLabel.ForeColor = System.Drawing.Color.Blue
        Me.PassWordLabel.Location = New System.Drawing.Point(6, 178)
        Me.PassWordLabel.Name = "PassWordLabel"
        Me.PassWordLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PassWordLabel.Size = New System.Drawing.Size(18, 17)
        Me.PassWordLabel.TabIndex = 22
        Me.PassWordLabel.Text = "--"
        Me.PassWordLabel.Visible = False
        '
        'UserNameLabel
        '
        Me.UserNameLabel.AutoSize = True
        Me.UserNameLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserNameLabel.ForeColor = System.Drawing.Color.Blue
        Me.UserNameLabel.Location = New System.Drawing.Point(6, 148)
        Me.UserNameLabel.Name = "UserNameLabel"
        Me.UserNameLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.UserNameLabel.Size = New System.Drawing.Size(18, 17)
        Me.UserNameLabel.TabIndex = 21
        Me.UserNameLabel.Text = "--"
        '
        'TrailCheckBox
        '
        Me.TrailCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrailCheckBox.AutoSize = True
        Me.TrailCheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TrailCheckBox.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TrailCheckBox.Location = New System.Drawing.Point(275, 329)
        Me.TrailCheckBox.Name = "TrailCheckBox"
        Me.TrailCheckBox.Size = New System.Drawing.Size(159, 25)
        Me.TrailCheckBox.TabIndex = 23
        Me.TrailCheckBox.Text = "ضبط كنسخة تجريبية"
        Me.TrailCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Serv_Desc_Lb)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.DataBaseNameLabel)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.AuthenticationLabel)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.ServerNameLabel)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.PassWordLabel)
        Me.GroupBox1.Controls.Add(Me.UserNameLabel)
        Me.GroupBox1.Location = New System.Drawing.Point(406, 69)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(273, 201)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "الخـــادم الحالــي"
        '
        'Serv_Desc_Lb
        '
        Me.Serv_Desc_Lb.AutoSize = True
        Me.Serv_Desc_Lb.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Serv_Desc_Lb.ForeColor = System.Drawing.Color.Blue
        Me.Serv_Desc_Lb.Location = New System.Drawing.Point(7, 38)
        Me.Serv_Desc_Lb.Name = "Serv_Desc_Lb"
        Me.Serv_Desc_Lb.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Serv_Desc_Lb.Size = New System.Drawing.Size(17, 15)
        Me.Serv_Desc_Lb.TabIndex = 24
        Me.Serv_Desc_Lb.Text = "--"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(200, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 15)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "إسم الشركة :"
        '
        'Server_Choese_Server_txt
        '
        Me.Server_Choese_Server_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Server_Choese_Server_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Server_Choese_Server_txt.Location = New System.Drawing.Point(174, 261)
        Me.Server_Choese_Server_txt.Name = "Server_Choese_Server_txt"
        Me.Server_Choese_Server_txt.Size = New System.Drawing.Size(223, 29)
        Me.Server_Choese_Server_txt.TabIndex = 28
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(53, 266)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(118, 21)
        Me.Label13.TabIndex = 27
        Me.Label13.Text = "خادم السيرفرات :"
        '
        'DB_Choese_Server_txt
        '
        Me.DB_Choese_Server_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DB_Choese_Server_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DB_Choese_Server_txt.Location = New System.Drawing.Point(174, 293)
        Me.DB_Choese_Server_txt.Name = "DB_Choese_Server_txt"
        Me.DB_Choese_Server_txt.Size = New System.Drawing.Size(223, 29)
        Me.DB_Choese_Server_txt.TabIndex = 30
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(4, 298)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(167, 21)
        Me.Label14.TabIndex = 29
        Me.Label14.Text = "قاعدة بيانات السيرفرات :"
        '
        'isCenterSys_CB
        '
        Me.isCenterSys_CB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.isCenterSys_CB.AutoSize = True
        Me.isCenterSys_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.isCenterSys_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isCenterSys_CB.Location = New System.Drawing.Point(11, 329)
        Me.isCenterSys_CB.Name = "isCenterSys_CB"
        Me.isCenterSys_CB.Size = New System.Drawing.Size(151, 25)
        Me.isCenterSys_CB.TabIndex = 31
        Me.isCenterSys_CB.Text = "ربط النظام بالفروع "
        Me.isCenterSys_CB.UseVisualStyleBackColor = True
        '
        'SERVER_IP_txt
        '
        Me.SERVER_IP_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SERVER_IP_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SERVER_IP_txt.Location = New System.Drawing.Point(174, 229)
        Me.SERVER_IP_txt.Name = "SERVER_IP_txt"
        Me.SERVER_IP_txt.Size = New System.Drawing.Size(223, 29)
        Me.SERVER_IP_txt.TabIndex = 32
        '
        'isSubSys_CB
        '
        Me.isSubSys_CB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.isSubSys_CB.AutoSize = True
        Me.isSubSys_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.isSubSys_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isSubSys_CB.Location = New System.Drawing.Point(168, 329)
        Me.isSubSys_CB.Name = "isSubSys_CB"
        Me.isSubSys_CB.Size = New System.Drawing.Size(100, 25)
        Me.isSubSys_CB.TabIndex = 33
        Me.isSubSys_CB.Text = "نظام فرعي"
        Me.isSubSys_CB.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 234)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(168, 21)
        Me.Label15.TabIndex = 34
        Me.Label15.Text = "عنوان IP الخادم الرئيسي :"
        '
        'Switch_PC_Activation_btn
        '
        Me.Switch_PC_Activation_btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.Switch_PC_Activation_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Switch_PC_Activation_btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.Switch_PC_Activation_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Switch_PC_Activation_btn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Switch_PC_Activation_btn.Location = New System.Drawing.Point(472, 401)
        Me.Switch_PC_Activation_btn.Name = "Switch_PC_Activation_btn"
        Me.Switch_PC_Activation_btn.Size = New System.Drawing.Size(107, 40)
        Me.Switch_PC_Activation_btn.TabIndex = 37
        Me.Switch_PC_Activation_btn.Text = "تبديل التفعيل"
        '
        'UseServer_Btn
        '
        Me.UseServer_Btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.UseServer_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UseServer_Btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.UseServer_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.UseServer_Btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.UseServer_Btn.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.UseServer_Btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right
        Me.UseServer_Btn.Location = New System.Drawing.Point(1, 401)
        Me.UseServer_Btn.Name = "UseServer_Btn"
        Me.UseServer_Btn.Size = New System.Drawing.Size(160, 40)
        Me.UseServer_Btn.TabIndex = 26
        Me.UseServer_Btn.Text = "  حفظ الخادم"
        '
        'ServMenu_Btn
        '
        Me.ServMenu_Btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ServMenu_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServMenu_Btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ServMenu_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ServMenu_Btn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ServMenu_Btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right
        Me.ServMenu_Btn.Location = New System.Drawing.Point(362, 401)
        Me.ServMenu_Btn.Name = "ServMenu_Btn"
        Me.ServMenu_Btn.Size = New System.Drawing.Size(108, 40)
        Me.ServMenu_Btn.TabIndex = 25
        Me.ServMenu_Btn.Text = "  قائمة الخوادم"
        '
        'Attach_Database_btn
        '
        Me.Attach_Database_btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.Attach_Database_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Attach_Database_btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.Attach_Database_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Attach_Database_btn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Attach_Database_btn.Location = New System.Drawing.Point(517, 329)
        Me.Attach_Database_btn.Name = "Attach_Database_btn"
        Me.Attach_Database_btn.Size = New System.Drawing.Size(162, 38)
        Me.Attach_Database_btn.TabIndex = 38
        Me.Attach_Database_btn.Text = "تكوين قاعدة البيانات"
        '
        'NewDBTextBox
        '
        Me.NewDBTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NewDBTextBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewDBTextBox.Location = New System.Drawing.Point(517, 298)
        Me.NewDBTextBox.Name = "NewDBTextBox"
        Me.NewDBTextBox.Size = New System.Drawing.Size(163, 29)
        Me.NewDBTextBox.TabIndex = 39
        Me.NewDBTextBox.Text = "CPOS"
        '
        'is_CHECKED_LB
        '
        Me.is_CHECKED_LB.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.is_CHECKED_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.is_CHECKED_LB.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.is_CHECKED_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.is_CHECKED_LB.Location = New System.Drawing.Point(403, 35)
        Me.is_CHECKED_LB.Name = "is_CHECKED_LB"
        Me.is_CHECKED_LB.Size = New System.Drawing.Size(276, 30)
        Me.is_CHECKED_LB.TabIndex = 709
        Me.is_CHECKED_LB.Text = "متصل"
        Me.is_CHECKED_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.is_CHECKED_LB.Visible = False
        '
        'Check_Btn
        '
        Me.Check_Btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.Check_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Check_Btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.Check_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Check_Btn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Check_Btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right
        Me.Check_Btn.Location = New System.Drawing.Point(237, 401)
        Me.Check_Btn.Name = "Check_Btn"
        Me.Check_Btn.Size = New System.Drawing.Size(119, 40)
        Me.Check_Btn.TabIndex = 710
        Me.Check_Btn.Text = "تحقق من الإتصال"
        '
        'Button23
        '
        Me.Button23.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button23.BackColor = System.Drawing.Color.White
        Me.Button23.BackgroundImage = Global.resturant.My.Resources.Resources.if_refresh_1608809
        Me.Button23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button23.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button23.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button23.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button23.ForeColor = System.Drawing.Color.DarkRed
        Me.Button23.Location = New System.Drawing.Point(365, 36)
        Me.Button23.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button23.Name = "Button23"
        Me.Button23.Size = New System.Drawing.Size(37, 29)
        Me.Button23.TabIndex = 711
        Me.Button23.UseVisualStyleBackColor = False
        '
        'Online_Con_txt
        '
        Me.Online_Con_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Online_Con_txt.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Online_Con_txt.Location = New System.Drawing.Point(174, 373)
        Me.Online_Con_txt.Name = "Online_Con_txt"
        Me.Online_Con_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Online_Con_txt.Size = New System.Drawing.Size(504, 25)
        Me.Online_Con_txt.TabIndex = 713
        Me.Online_Con_txt.Text = "Data Source= 65.21.80.196;initial catalog='';User Id='';Password=''"
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label16.Location = New System.Drawing.Point(46, 376)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(93, 19)
        Me.Label16.TabIndex = 712
        Me.Label16.Text = "خادم الإنترنت :"
        '
        'Refresh_btn
        '
        Me.Refresh_btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.Refresh_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Refresh_btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.Refresh_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Refresh_btn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Refresh_btn.Location = New System.Drawing.Point(143, 373)
        Me.Refresh_btn.Name = "Refresh_btn"
        Me.Refresh_btn.Size = New System.Drawing.Size(30, 24)
        Me.Refresh_btn.TabIndex = 714
        Me.Refresh_btn.Text = ".."
        '
        'ServerOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(681, 441)
        Me.Controls.Add(Me.Refresh_btn)
        Me.Controls.Add(Me.Online_Con_txt)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Button23)
        Me.Controls.Add(Me.Check_Btn)
        Me.Controls.Add(Me.is_CHECKED_LB)
        Me.Controls.Add(Me.NewDBTextBox)
        Me.Controls.Add(Me.Attach_Database_btn)
        Me.Controls.Add(Me.Switch_PC_Activation_btn)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.isSubSys_CB)
        Me.Controls.Add(Me.SERVER_IP_txt)
        Me.Controls.Add(Me.isCenterSys_CB)
        Me.Controls.Add(Me.DB_Choese_Server_txt)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Server_Choese_Server_txt)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.UseServer_Btn)
        Me.Controls.Add(Me.ServMenu_Btn)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TrailCheckBox)
        Me.Controls.Add(Me.SaveEdit_Btn)
        Me.Controls.Add(Me.UserPassGroupBox)
        Me.Controls.Add(Me.CmbDataBase)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CmbAuth)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CmbSeverName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Serv_Desc_txt)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ServerOptions"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ضبط الخـــادم"
        Me.UserPassGroupBox.ResumeLayout(False)
        Me.UserPassGroupBox.PerformLayout()
        CType(Me.ServerNameErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AuthErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DBNameErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UnameErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UpassErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CmbSeverName As System.Windows.Forms.ComboBox
    Friend WithEvents CmbAuth As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CmbDataBase As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtUserName As System.Windows.Forms.TextBox
    Friend WithEvents UserPassGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtPassWord As System.Windows.Forms.TextBox
    Friend WithEvents Serv_Desc_txt As System.Windows.Forms.TextBox
    Friend WithEvents ServerNameErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents AuthErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents DBNameErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents UnameErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents UpassErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents SaveEdit_Btn As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DataBaseNameLabel As System.Windows.Forms.Label
    Friend WithEvents AuthenticationLabel As System.Windows.Forms.Label
    Friend WithEvents ServerNameLabel As System.Windows.Forms.Label
    Friend WithEvents PassWordLabel As System.Windows.Forms.Label
    Friend WithEvents UserNameLabel As System.Windows.Forms.Label
    Friend WithEvents TrailCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ServMenu_Btn As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Serv_Desc_Lb As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents UseServer_Btn As DevComponents.DotNetBar.ButtonX
    Friend WithEvents DB_Choese_Server_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Server_Choese_Server_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents isCenterSys_CB As System.Windows.Forms.CheckBox
    Friend WithEvents isSubSys_CB As System.Windows.Forms.CheckBox
    Friend WithEvents SERVER_IP_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Switch_PC_Activation_btn As DevComponents.DotNetBar.ButtonX
    Friend WithEvents NewDBTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Attach_Database_btn As DevComponents.DotNetBar.ButtonX
    Friend WithEvents is_CHECKED_LB As System.Windows.Forms.Label
    Friend WithEvents Check_Btn As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Button23 As System.Windows.Forms.Button
    Friend WithEvents Online_Con_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Refresh_btn As DevComponents.DotNetBar.ButtonX

End Class
