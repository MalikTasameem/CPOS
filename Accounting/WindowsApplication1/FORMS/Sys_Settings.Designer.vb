<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Sys_Settings
    Inherits Base_Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Email = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Phone_Number = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Address = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ResetType_CM = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumberLength = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Prefix = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Pure_Income_ACC_CODE_TXT = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SBill_Title_2_Txt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SBill_Title_1_Txt = New System.Windows.Forms.TextBox()
        Me.GroupBox_LOGO = New System.Windows.Forms.GroupBox()
        Me.NoPictureButton = New System.Windows.Forms.Button()
        Me.ChoasePicureButton = New System.Windows.Forms.Button()
        Me.IMPictureBox = New System.Windows.Forms.PictureBox()
        Me.is_Dark_mode_CB = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.is_Link_With_SB_CB = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SALES_DB_TXT = New System.Windows.Forms.TextBox()
        Me.Font_Cm = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Use_State_Budget_CB = New System.Windows.Forms.CheckBox()
        Me.Allow_Budget_OverSpend_CB = New System.Windows.Forms.CheckBox()
        Me.LabelDefaultStampTitle = New System.Windows.Forms.Label()
        Me.LabelDefaultStampPercent = New System.Windows.Forms.Label()
        Me.Default_Stamp_Percent_TXT = New System.Windows.Forms.TextBox()
        Me.LabelDefaultStampAccount = New System.Windows.Forms.Label()
        Me.Default_Stamp_Account_Code_TXT = New System.Windows.Forms.TextBox()
        Me.Pick_Default_Stamp_Account_BTN = New System.Windows.Forms.Button()
        Me.Default_Stamp_Account_Name_TXT = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox_LOGO.SuspendLayout()
        CType(Me.IMPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(627, 140)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 18)
        Me.Label9.TabIndex = 267
        Me.Label9.Text = "بريد إلكتروني"
        '
        'Email
        '
        Me.Email.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Email.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Email.Location = New System.Drawing.Point(5, 137)
        Me.Email.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Email.Name = "Email"
        Me.Email.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Email.Size = New System.Drawing.Size(618, 26)
        Me.Email.TabIndex = 266
        Me.Email.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(627, 107)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 18)
        Me.Label8.TabIndex = 265
        Me.Label8.Text = "هاتف"
        '
        'Phone_Number
        '
        Me.Phone_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Phone_Number.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Phone_Number.Location = New System.Drawing.Point(5, 103)
        Me.Phone_Number.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Phone_Number.Name = "Phone_Number"
        Me.Phone_Number.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Phone_Number.Size = New System.Drawing.Size(618, 26)
        Me.Phone_Number.TabIndex = 264
        Me.Phone_Number.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(627, 74)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 18)
        Me.Label7.TabIndex = 263
        Me.Label7.Text = "العنوان"
        '
        'Address
        '
        Me.Address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Address.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Address.Location = New System.Drawing.Point(5, 70)
        Me.Address.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Address.Name = "Address"
        Me.Address.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Address.Size = New System.Drawing.Size(618, 26)
        Me.Address.TabIndex = 262
        Me.Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ResetType_CM)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.NumberLength)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Prefix)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 254)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(199, 129)
        Me.GroupBox1.TabIndex = 261
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "بيانات الرقم الإشاري للقيد"
        '
        'ResetType_CM
        '
        Me.ResetType_CM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResetType_CM.BackColor = System.Drawing.SystemColors.Info
        Me.ResetType_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ResetType_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ResetType_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ResetType_CM.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResetType_CM.FormattingEnabled = True
        Me.ResetType_CM.Items.AddRange(New Object() {"YEAR", "MONTH"})
        Me.ResetType_CM.Location = New System.Drawing.Point(6, 94)
        Me.ResetType_CM.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ResetType_CM.Name = "ResetType_CM"
        Me.ResetType_CM.Size = New System.Drawing.Size(70, 24)
        Me.ResetType_CM.TabIndex = 268
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(82, 100)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 16)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "تهئية كل"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label5.Location = New System.Drawing.Point(79, 66)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 16)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "عدد خانات الترقيم"
        '
        'NumberLength
        '
        Me.NumberLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumberLength.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.NumberLength.Location = New System.Drawing.Point(6, 59)
        Me.NumberLength.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.NumberLength.Name = "NumberLength"
        Me.NumberLength.Size = New System.Drawing.Size(70, 26)
        Me.NumberLength.TabIndex = 4
        Me.NumberLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label4.Location = New System.Drawing.Point(80, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 16)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "الرمز"
        '
        'Prefix
        '
        Me.Prefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Prefix.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Prefix.Location = New System.Drawing.Point(6, 25)
        Me.Prefix.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Prefix.Name = "Prefix"
        Me.Prefix.Size = New System.Drawing.Size(70, 26)
        Me.Prefix.TabIndex = 2
        Me.Prefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(150, 219)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(243, 16)
        Me.Label13.TabIndex = 260
        Me.Label13.Text = "حساب قائمة الدخل يرحل إلى حساب رقم :"
        '
        'Pure_Income_ACC_CODE_TXT
        '
        Me.Pure_Income_ACC_CODE_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pure_Income_ACC_CODE_TXT.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Pure_Income_ACC_CODE_TXT.Location = New System.Drawing.Point(5, 214)
        Me.Pure_Income_ACC_CODE_TXT.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Pure_Income_ACC_CODE_TXT.Name = "Pure_Income_ACC_CODE_TXT"
        Me.Pure_Income_ACC_CODE_TXT.Size = New System.Drawing.Size(141, 26)
        Me.Pure_Income_ACC_CODE_TXT.TabIndex = 259
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(627, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 18)
        Me.Label3.TabIndex = 258
        Me.Label3.Text = "إسم النشاط 2"
        '
        'SBill_Title_2_Txt
        '
        Me.SBill_Title_2_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SBill_Title_2_Txt.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.SBill_Title_2_Txt.Location = New System.Drawing.Point(5, 37)
        Me.SBill_Title_2_Txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SBill_Title_2_Txt.Name = "SBill_Title_2_Txt"
        Me.SBill_Title_2_Txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SBill_Title_2_Txt.Size = New System.Drawing.Size(618, 26)
        Me.SBill_Title_2_Txt.TabIndex = 257
        Me.SBill_Title_2_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(625, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 18)
        Me.Label2.TabIndex = 256
        Me.Label2.Text = "إسم النشاط 1"
        '
        'SBill_Title_1_Txt
        '
        Me.SBill_Title_1_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SBill_Title_1_Txt.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.SBill_Title_1_Txt.Location = New System.Drawing.Point(5, 4)
        Me.SBill_Title_1_Txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SBill_Title_1_Txt.Name = "SBill_Title_1_Txt"
        Me.SBill_Title_1_Txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SBill_Title_1_Txt.Size = New System.Drawing.Size(618, 26)
        Me.SBill_Title_1_Txt.TabIndex = 255
        Me.SBill_Title_1_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox_LOGO
        '
        Me.GroupBox_LOGO.Controls.Add(Me.NoPictureButton)
        Me.GroupBox_LOGO.Controls.Add(Me.ChoasePicureButton)
        Me.GroupBox_LOGO.Controls.Add(Me.IMPictureBox)
        Me.GroupBox_LOGO.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox_LOGO.Location = New System.Drawing.Point(489, 404)
        Me.GroupBox_LOGO.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox_LOGO.Name = "GroupBox_LOGO"
        Me.GroupBox_LOGO.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox_LOGO.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox_LOGO.Size = New System.Drawing.Size(237, 228)
        Me.GroupBox_LOGO.TabIndex = 254
        Me.GroupBox_LOGO.TabStop = False
        Me.GroupBox_LOGO.Text = "شعار "
        '
        'NoPictureButton
        '
        Me.NoPictureButton.BackColor = System.Drawing.Color.SeaShell
        Me.NoPictureButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NoPictureButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NoPictureButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoPictureButton.Location = New System.Drawing.Point(59, 177)
        Me.NoPictureButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.NoPictureButton.Name = "NoPictureButton"
        Me.NoPictureButton.Size = New System.Drawing.Size(174, 36)
        Me.NoPictureButton.TabIndex = 239
        Me.NoPictureButton.Text = "بدون شعار"
        Me.NoPictureButton.UseVisualStyleBackColor = False
        '
        'ChoasePicureButton
        '
        Me.ChoasePicureButton.BackColor = System.Drawing.Color.SeaShell
        Me.ChoasePicureButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChoasePicureButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ChoasePicureButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChoasePicureButton.Location = New System.Drawing.Point(5, 177)
        Me.ChoasePicureButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ChoasePicureButton.Name = "ChoasePicureButton"
        Me.ChoasePicureButton.Size = New System.Drawing.Size(52, 36)
        Me.ChoasePicureButton.TabIndex = 236
        Me.ChoasePicureButton.Text = "......"
        Me.ChoasePicureButton.UseVisualStyleBackColor = False
        '
        'IMPictureBox
        '
        Me.IMPictureBox.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.IMPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IMPictureBox.Location = New System.Drawing.Point(5, 32)
        Me.IMPictureBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IMPictureBox.Name = "IMPictureBox"
        Me.IMPictureBox.Size = New System.Drawing.Size(228, 142)
        Me.IMPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.IMPictureBox.TabIndex = 234
        Me.IMPictureBox.TabStop = False
        '
        'is_Dark_mode_CB
        '
        Me.is_Dark_mode_CB.AutoSize = True
        Me.is_Dark_mode_CB.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.is_Dark_mode_CB.Location = New System.Drawing.Point(565, 218)
        Me.is_Dark_mode_CB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.is_Dark_mode_CB.Name = "is_Dark_mode_CB"
        Me.is_Dark_mode_CB.Size = New System.Drawing.Size(161, 22)
        Me.is_Dark_mode_CB.TabIndex = 4
        Me.is_Dark_mode_CB.Text = "تفعيل الوضع الليلي"
        Me.is_Dark_mode_CB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.is_Dark_mode_CB.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(5, 639)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(721, 71)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "📝 حفـظ التعديـلات"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'is_Link_With_SB_CB
        '
        Me.is_Link_With_SB_CB.AutoSize = True
        Me.is_Link_With_SB_CB.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.is_Link_With_SB_CB.Location = New System.Drawing.Point(496, 175)
        Me.is_Link_With_SB_CB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.is_Link_With_SB_CB.Name = "is_Link_With_SB_CB"
        Me.is_Link_With_SB_CB.Size = New System.Drawing.Size(230, 22)
        Me.is_Link_With_SB_CB.TabIndex = 2
        Me.is_Link_With_SB_CB.Text = "تفعيل الربط مع نظام المبيعات"
        Me.is_Link_With_SB_CB.UseVisualStyleBackColor = True
        Me.is_Link_With_SB_CB.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(370, 177)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 18)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "إسم قاعدة اليانات"
        Me.Label1.Visible = False
        '
        'SALES_DB_TXT
        '
        Me.SALES_DB_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SALES_DB_TXT.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.SALES_DB_TXT.Location = New System.Drawing.Point(225, 171)
        Me.SALES_DB_TXT.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SALES_DB_TXT.Name = "SALES_DB_TXT"
        Me.SALES_DB_TXT.Size = New System.Drawing.Size(141, 26)
        Me.SALES_DB_TXT.TabIndex = 0
        Me.SALES_DB_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.SALES_DB_TXT.Visible = False
        '
        'Font_Cm
        '
        Me.Font_Cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Font_Cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Font_Cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Font_Cm.BackColor = System.Drawing.SystemColors.Info
        Me.Font_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Font_Cm.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Font_Cm.FormattingEnabled = True
        Me.Font_Cm.Location = New System.Drawing.Point(418, 253)
        Me.Font_Cm.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Font_Cm.Name = "Font_Cm"
        Me.Font_Cm.Size = New System.Drawing.Size(233, 24)
        Me.Font_Cm.TabIndex = 269
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(656, 256)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 18)
        Me.Label10.TabIndex = 270
        Me.Label10.Text = "خط النظام"
        '
        'Use_State_Budget_CB
        '
        Me.Use_State_Budget_CB.AutoSize = True
        Me.Use_State_Budget_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Use_State_Budget_CB.Location = New System.Drawing.Point(486, 312)
        Me.Use_State_Budget_CB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Use_State_Budget_CB.Name = "Use_State_Budget_CB"
        Me.Use_State_Budget_CB.Size = New System.Drawing.Size(165, 24)
        Me.Use_State_Budget_CB.TabIndex = 271
        Me.Use_State_Budget_CB.Text = "استخدام موازنة الدولة"
        Me.Use_State_Budget_CB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Use_State_Budget_CB.UseVisualStyleBackColor = True
        '
        'Allow_Budget_OverSpend_CB
        '
        Me.Allow_Budget_OverSpend_CB.AutoSize = True
        Me.Allow_Budget_OverSpend_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Allow_Budget_OverSpend_CB.Location = New System.Drawing.Point(231, 342)
        Me.Allow_Budget_OverSpend_CB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Allow_Budget_OverSpend_CB.Name = "Allow_Budget_OverSpend_CB"
        Me.Allow_Budget_OverSpend_CB.Size = New System.Drawing.Size(420, 23)
        Me.Allow_Budget_OverSpend_CB.TabIndex = 272
        Me.Allow_Budget_OverSpend_CB.Text = "السماح بتنفيذ عمليات الموازنة عند عدم كفاية الاعتماد"
        Me.Allow_Budget_OverSpend_CB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Allow_Budget_OverSpend_CB.UseVisualStyleBackColor = True
        '
        'LabelDefaultStampTitle
        '
        Me.LabelDefaultStampTitle.AutoSize = True
        Me.LabelDefaultStampTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDefaultStampTitle.Location = New System.Drawing.Point(293, 372)
        Me.LabelDefaultStampTitle.Name = "LabelDefaultStampTitle"
        Me.LabelDefaultStampTitle.Size = New System.Drawing.Size(151, 20)
        Me.LabelDefaultStampTitle.TabIndex = 273
        Me.LabelDefaultStampTitle.Text = "إعدادات الدمغة الافتراضية"
        '
        'LabelDefaultStampPercent
        '
        Me.LabelDefaultStampPercent.AutoSize = True
        Me.LabelDefaultStampPercent.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDefaultStampPercent.Location = New System.Drawing.Point(393, 406)
        Me.LabelDefaultStampPercent.Name = "LabelDefaultStampPercent"
        Me.LabelDefaultStampPercent.Size = New System.Drawing.Size(89, 17)
        Me.LabelDefaultStampPercent.TabIndex = 274
        Me.LabelDefaultStampPercent.Text = "نسبة الدمغة %"
        '
        'Default_Stamp_Percent_TXT
        '
        Me.Default_Stamp_Percent_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Default_Stamp_Percent_TXT.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Default_Stamp_Percent_TXT.Location = New System.Drawing.Point(313, 403)
        Me.Default_Stamp_Percent_TXT.Name = "Default_Stamp_Percent_TXT"
        Me.Default_Stamp_Percent_TXT.Size = New System.Drawing.Size(76, 25)
        Me.Default_Stamp_Percent_TXT.TabIndex = 275
        Me.Default_Stamp_Percent_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelDefaultStampAccount
        '
        Me.LabelDefaultStampAccount.AutoSize = True
        Me.LabelDefaultStampAccount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDefaultStampAccount.Location = New System.Drawing.Point(399, 438)
        Me.LabelDefaultStampAccount.Name = "LabelDefaultStampAccount"
        Me.LabelDefaultStampAccount.Size = New System.Drawing.Size(83, 17)
        Me.LabelDefaultStampAccount.TabIndex = 276
        Me.LabelDefaultStampAccount.Text = "حساب الدمغة"
        '
        'Default_Stamp_Account_Code_TXT
        '
        Me.Default_Stamp_Account_Code_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Default_Stamp_Account_Code_TXT.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Default_Stamp_Account_Code_TXT.Location = New System.Drawing.Point(313, 435)
        Me.Default_Stamp_Account_Code_TXT.Name = "Default_Stamp_Account_Code_TXT"
        Me.Default_Stamp_Account_Code_TXT.Size = New System.Drawing.Size(76, 25)
        Me.Default_Stamp_Account_Code_TXT.TabIndex = 277
        Me.Default_Stamp_Account_Code_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Pick_Default_Stamp_Account_BTN
        '
        Me.Pick_Default_Stamp_Account_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Pick_Default_Stamp_Account_BTN.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Pick_Default_Stamp_Account_BTN.Location = New System.Drawing.Point(280, 435)
        Me.Pick_Default_Stamp_Account_BTN.Name = "Pick_Default_Stamp_Account_BTN"
        Me.Pick_Default_Stamp_Account_BTN.Size = New System.Drawing.Size(29, 25)
        Me.Pick_Default_Stamp_Account_BTN.TabIndex = 278
        Me.Pick_Default_Stamp_Account_BTN.Text = "..."
        Me.Pick_Default_Stamp_Account_BTN.UseVisualStyleBackColor = True
        '
        'Default_Stamp_Account_Name_TXT
        '
        Me.Default_Stamp_Account_Name_TXT.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Default_Stamp_Account_Name_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Default_Stamp_Account_Name_TXT.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Default_Stamp_Account_Name_TXT.Location = New System.Drawing.Point(5, 435)
        Me.Default_Stamp_Account_Name_TXT.Name = "Default_Stamp_Account_Name_TXT"
        Me.Default_Stamp_Account_Name_TXT.ReadOnly = True
        Me.Default_Stamp_Account_Name_TXT.Size = New System.Drawing.Size(271, 25)
        Me.Default_Stamp_Account_Name_TXT.TabIndex = 279
        Me.Default_Stamp_Account_Name_TXT.TabStop = False
        '
        'Sys_Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(730, 712)
        Me.Controls.Add(Me.Default_Stamp_Account_Name_TXT)
        Me.Controls.Add(Me.Pick_Default_Stamp_Account_BTN)
        Me.Controls.Add(Me.Default_Stamp_Account_Code_TXT)
        Me.Controls.Add(Me.LabelDefaultStampAccount)
        Me.Controls.Add(Me.Default_Stamp_Percent_TXT)
        Me.Controls.Add(Me.LabelDefaultStampPercent)
        Me.Controls.Add(Me.LabelDefaultStampTitle)
        Me.Controls.Add(Me.Allow_Budget_OverSpend_CB)
        Me.Controls.Add(Me.Use_State_Budget_CB)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Font_Cm)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Email)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Phone_Number)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Address)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Pure_Income_ACC_CODE_TXT)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.SBill_Title_2_Txt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.SBill_Title_1_Txt)
        Me.Controls.Add(Me.GroupBox_LOGO)
        Me.Controls.Add(Me.is_Dark_mode_CB)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.is_Link_With_SB_CB)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SALES_DB_TXT)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "Sys_Settings"
        Me.Text = "Sys_Settings"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox_LOGO.ResumeLayout(False)
        CType(Me.IMPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SALES_DB_TXT As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents is_Link_With_SB_CB As CheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents is_Dark_mode_CB As CheckBox
    Friend WithEvents GroupBox_LOGO As GroupBox
    Friend WithEvents NoPictureButton As Button
    Friend WithEvents ChoasePicureButton As Button
    Friend WithEvents IMPictureBox As PictureBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SBill_Title_1_Txt As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents SBill_Title_2_Txt As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Pure_Income_ACC_CODE_TXT As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents NumberLength As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Prefix As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Address As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Phone_Number As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Email As TextBox
    Friend WithEvents ResetType_CM As ComboBox
    Friend WithEvents Font_Cm As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Use_State_Budget_CB As CheckBox
    Friend WithEvents Allow_Budget_OverSpend_CB As CheckBox
    Friend WithEvents LabelDefaultStampTitle As Label
    Friend WithEvents LabelDefaultStampPercent As Label
    Friend WithEvents Default_Stamp_Percent_TXT As TextBox
    Friend WithEvents LabelDefaultStampAccount As Label
    Friend WithEvents Default_Stamp_Account_Code_TXT As TextBox
    Friend WithEvents Pick_Default_Stamp_Account_BTN As Button
    Friend WithEvents Default_Stamp_Account_Name_TXT As TextBox
End Class
