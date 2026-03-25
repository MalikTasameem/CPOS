<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetSystem
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Sys_Type_cm = New System.Windows.Forms.ComboBox()
        Me.Pr_CB = New System.Windows.Forms.CheckBox()
        Me.Tables_CB = New System.Windows.Forms.CheckBox()
        Me.SubPrints_CB = New System.Windows.Forms.CheckBox()
        Me.Notes_CB = New System.Windows.Forms.CheckBox()
        Me.Orders_CB = New System.Windows.Forms.CheckBox()
        Me.Stores_CB = New System.Windows.Forms.CheckBox()
        Me.inside_Bill_CB = New System.Windows.Forms.CheckBox()
        Me.Frm_CB = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.SScreenDefault_Cmb = New System.Windows.Forms.ComboBox()
        Me.Agent_Cards_CB = New System.Windows.Forms.CheckBox()
        Me.GroupBox14 = New System.Windows.Forms.GroupBox()
        Me.IM_Type_cm = New System.Windows.Forms.ComboBox()
        Me.Marketers_CB = New System.Windows.Forms.CheckBox()
        Me.Out_Travel_CB = New System.Windows.Forms.CheckBox()
        Me.SerialCode_CB = New System.Windows.Forms.CheckBox()
        Me.IMPH_Btn = New System.Windows.Forms.Button()
        Me.IM_Photo = New System.Windows.Forms.PictureBox()
        Me.Save_butt = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.App_Suuply_cm = New System.Windows.Forms.ComboBox()
        Me.Fonts_Path_Lb = New System.Windows.Forms.LinkLabel()
        Me.Allow_MinSP_CB = New System.Windows.Forms.CheckBox()
        Me.Exp_Pch_CB = New System.Windows.Forms.CheckBox()
        Me.Phone_For_Tables_CB = New System.Windows.Forms.CheckBox()
        Me.is_Pch_Discount_Distribute_CB = New System.Windows.Forms.CheckBox()
        Me.is_Auto_Recive_ST_Tran_CB = New System.Windows.Forms.CheckBox()
        Me.IM_Valid_CB = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.NumOfBillsTest_txt = New resturant.F3NumericTextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TB_Auto_Print_CB = New System.Windows.Forms.CheckBox()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox14.SuspendLayout()
        CType(Me.IM_Photo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Sys_Type_cm)
        Me.GroupBox3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(1, 109)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(392, 58)
        Me.GroupBox3.TabIndex = 298
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "النشـــــاط"
        '
        'Sys_Type_cm
        '
        Me.Sys_Type_cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
        Me.Sys_Type_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sys_Type_cm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Sys_Type_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Sys_Type_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Sys_Type_cm.FormattingEnabled = True
        Me.Sys_Type_cm.Location = New System.Drawing.Point(3, 25)
        Me.Sys_Type_cm.Name = "Sys_Type_cm"
        Me.Sys_Type_cm.Size = New System.Drawing.Size(386, 29)
        Me.Sys_Type_cm.TabIndex = 262
        '
        'Pr_CB
        '
        Me.Pr_CB.AutoSize = True
        Me.Pr_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Pr_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Pr_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pr_CB.Location = New System.Drawing.Point(429, 30)
        Me.Pr_CB.Name = "Pr_CB"
        Me.Pr_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Pr_CB.Size = New System.Drawing.Size(73, 25)
        Me.Pr_CB.TabIndex = 299
        Me.Pr_CB.Text = "ورديات"
        Me.Pr_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Pr_CB.UseVisualStyleBackColor = True
        '
        'Tables_CB
        '
        Me.Tables_CB.AutoSize = True
        Me.Tables_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Tables_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Tables_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tables_CB.Location = New System.Drawing.Point(603, 30)
        Me.Tables_CB.Name = "Tables_CB"
        Me.Tables_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Tables_CB.Size = New System.Drawing.Size(76, 25)
        Me.Tables_CB.TabIndex = 300
        Me.Tables_CB.Text = "طاولات"
        Me.Tables_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Tables_CB.UseVisualStyleBackColor = True
        '
        'SubPrints_CB
        '
        Me.SubPrints_CB.AutoSize = True
        Me.SubPrints_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SubPrints_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SubPrints_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubPrints_CB.Location = New System.Drawing.Point(765, 35)
        Me.SubPrints_CB.Name = "SubPrints_CB"
        Me.SubPrints_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SubPrints_CB.Size = New System.Drawing.Size(115, 25)
        Me.SubPrints_CB.TabIndex = 301
        Me.SubPrints_CB.Text = "طابعات تجهيز"
        Me.SubPrints_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.SubPrints_CB.UseVisualStyleBackColor = True
        '
        'Notes_CB
        '
        Me.Notes_CB.AutoSize = True
        Me.Notes_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Notes_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Notes_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Notes_CB.Location = New System.Drawing.Point(429, 81)
        Me.Notes_CB.Name = "Notes_CB"
        Me.Notes_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Notes_CB.Size = New System.Drawing.Size(86, 25)
        Me.Notes_CB.TabIndex = 302
        Me.Notes_CB.Text = "ملاحظات"
        Me.Notes_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Notes_CB.UseVisualStyleBackColor = True
        '
        'Orders_CB
        '
        Me.Orders_CB.AutoSize = True
        Me.Orders_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Orders_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Orders_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Orders_CB.Location = New System.Drawing.Point(603, 86)
        Me.Orders_CB.Name = "Orders_CB"
        Me.Orders_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Orders_CB.Size = New System.Drawing.Size(74, 25)
        Me.Orders_CB.TabIndex = 303
        Me.Orders_CB.Text = "طلبيات"
        Me.Orders_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Orders_CB.UseVisualStyleBackColor = True
        '
        'Stores_CB
        '
        Me.Stores_CB.AutoSize = True
        Me.Stores_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Stores_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Stores_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Stores_CB.Location = New System.Drawing.Point(765, 86)
        Me.Stores_CB.Name = "Stores_CB"
        Me.Stores_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Stores_CB.Size = New System.Drawing.Size(224, 25)
        Me.Stores_CB.TabIndex = 304
        Me.Stores_CB.Text = "نظام مخزن(إذن صرف وإستلام)"
        Me.Stores_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Stores_CB.UseVisualStyleBackColor = True
        Me.Stores_CB.Visible = False
        '
        'inside_Bill_CB
        '
        Me.inside_Bill_CB.AutoSize = True
        Me.inside_Bill_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.inside_Bill_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.inside_Bill_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inside_Bill_CB.Location = New System.Drawing.Point(603, 159)
        Me.inside_Bill_CB.Name = "inside_Bill_CB"
        Me.inside_Bill_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.inside_Bill_CB.Size = New System.Drawing.Size(181, 25)
        Me.inside_Bill_CB.TabIndex = 306
        Me.inside_Bill_CB.Text = "أذونات (صرف + إستلام)"
        Me.inside_Bill_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.inside_Bill_CB.UseVisualStyleBackColor = True
        '
        'Frm_CB
        '
        Me.Frm_CB.AutoSize = True
        Me.Frm_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Frm_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Frm_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frm_CB.Location = New System.Drawing.Point(429, 159)
        Me.Frm_CB.Name = "Frm_CB"
        Me.Frm_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Frm_CB.Size = New System.Drawing.Size(67, 25)
        Me.Frm_CB.TabIndex = 305
        Me.Frm_CB.Text = "تصنيع"
        Me.Frm_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Frm_CB.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.SScreenDefault_Cmb)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(800, 134)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(182, 58)
        Me.GroupBox2.TabIndex = 318
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "شاشة البيع الإفتراضية"
        '
        'SScreenDefault_Cmb
        '
        Me.SScreenDefault_Cmb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SScreenDefault_Cmb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SScreenDefault_Cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SScreenDefault_Cmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SScreenDefault_Cmb.FormattingEnabled = True
        Me.SScreenDefault_Cmb.Items.AddRange(New Object() {"شاشة عادية", "شاشة لمس", "الشاشة السريعة"})
        Me.SScreenDefault_Cmb.Location = New System.Drawing.Point(3, 25)
        Me.SScreenDefault_Cmb.Name = "SScreenDefault_Cmb"
        Me.SScreenDefault_Cmb.Size = New System.Drawing.Size(176, 29)
        Me.SScreenDefault_Cmb.TabIndex = 262
        Me.SScreenDefault_Cmb.Tag = ""
        '
        'Agent_Cards_CB
        '
        Me.Agent_Cards_CB.AutoSize = True
        Me.Agent_Cards_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Agent_Cards_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Agent_Cards_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Agent_Cards_CB.Location = New System.Drawing.Point(429, 220)
        Me.Agent_Cards_CB.Name = "Agent_Cards_CB"
        Me.Agent_Cards_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Agent_Cards_CB.Size = New System.Drawing.Size(114, 25)
        Me.Agent_Cards_CB.TabIndex = 319
        Me.Agent_Cards_CB.Text = "عملاء بطاقات"
        Me.Agent_Cards_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Agent_Cards_CB.UseVisualStyleBackColor = True
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.IM_Type_cm)
        Me.GroupBox14.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox14.Location = New System.Drawing.Point(803, 198)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(179, 58)
        Me.GroupBox14.TabIndex = 320
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "نوع الصنف الإفتراضي"
        '
        'IM_Type_cm
        '
        Me.IM_Type_cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.IM_Type_cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.IM_Type_cm.BackColor = System.Drawing.SystemColors.Window
        Me.IM_Type_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Type_cm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IM_Type_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IM_Type_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Type_cm.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Type_cm.FormattingEnabled = True
        Me.IM_Type_cm.IntegralHeight = False
        Me.IM_Type_cm.Items.AddRange(New Object() {"خدمة", "بضاعة", "تصنيع"})
        Me.IM_Type_cm.Location = New System.Drawing.Point(3, 25)
        Me.IM_Type_cm.Name = "IM_Type_cm"
        Me.IM_Type_cm.Size = New System.Drawing.Size(173, 29)
        Me.IM_Type_cm.TabIndex = 465
        '
        'Marketers_CB
        '
        Me.Marketers_CB.AutoSize = True
        Me.Marketers_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Marketers_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Marketers_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Marketers_CB.Location = New System.Drawing.Point(603, 220)
        Me.Marketers_CB.Name = "Marketers_CB"
        Me.Marketers_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Marketers_CB.Size = New System.Drawing.Size(81, 25)
        Me.Marketers_CB.TabIndex = 321
        Me.Marketers_CB.Text = "مسوقين"
        Me.Marketers_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Marketers_CB.UseVisualStyleBackColor = True
        '
        'Out_Travel_CB
        '
        Me.Out_Travel_CB.AutoSize = True
        Me.Out_Travel_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Out_Travel_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Out_Travel_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Out_Travel_CB.Location = New System.Drawing.Point(676, 274)
        Me.Out_Travel_CB.Name = "Out_Travel_CB"
        Me.Out_Travel_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Out_Travel_CB.Size = New System.Drawing.Size(174, 25)
        Me.Out_Travel_CB.TabIndex = 323
        Me.Out_Travel_CB.Text = "رحلات خارجية للمبيعات"
        Me.Out_Travel_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Out_Travel_CB.UseVisualStyleBackColor = True
        Me.Out_Travel_CB.Visible = False
        '
        'SerialCode_CB
        '
        Me.SerialCode_CB.AutoSize = True
        Me.SerialCode_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SerialCode_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SerialCode_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SerialCode_CB.Location = New System.Drawing.Point(429, 274)
        Me.SerialCode_CB.Name = "SerialCode_CB"
        Me.SerialCode_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SerialCode_CB.Size = New System.Drawing.Size(227, 25)
        Me.SerialCode_CB.TabIndex = 322
        Me.SerialCode_CB.Text = "رقم تسلسلي للصنف فالمبيعات"
        Me.SerialCode_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.SerialCode_CB.UseVisualStyleBackColor = True
        '
        'IMPH_Btn
        '
        Me.IMPH_Btn.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.IMPH_Btn.BackColor = System.Drawing.Color.Linen
        Me.IMPH_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMPH_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.IMPH_Btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMPH_Btn.Location = New System.Drawing.Point(1, 463)
        Me.IMPH_Btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.IMPH_Btn.Name = "IMPH_Btn"
        Me.IMPH_Btn.Size = New System.Drawing.Size(77, 30)
        Me.IMPH_Btn.TabIndex = 612
        Me.IMPH_Btn.Text = "....."
        Me.IMPH_Btn.UseVisualStyleBackColor = False
        '
        'IM_Photo
        '
        Me.IM_Photo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.IM_Photo.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.IM_Photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Photo.Location = New System.Drawing.Point(1, 170)
        Me.IM_Photo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.IM_Photo.Name = "IM_Photo"
        Me.IM_Photo.Size = New System.Drawing.Size(329, 292)
        Me.IM_Photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.IM_Photo.TabIndex = 611
        Me.IM_Photo.TabStop = False
        '
        'Save_butt
        '
        Me.Save_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Save_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Save_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_butt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Save_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Save_butt.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.Save_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Save_butt.Location = New System.Drawing.Point(12, 521)
        Me.Save_butt.Name = "Save_butt"
        Me.Save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Save_butt.Size = New System.Drawing.Size(193, 54)
        Me.Save_butt.TabIndex = 317
        Me.Save_butt.TabStop = False
        Me.Save_butt.Text = "حفظ التعديلات"
        Me.Save_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Save_butt.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button1.BackColor = System.Drawing.Color.Linen
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(82, 463)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(77, 30)
        Me.Button1.TabIndex = 613
        Me.Button1.Text = "بلا خلفية"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.App_Suuply_cm)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(392, 58)
        Me.GroupBox1.TabIndex = 614
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "المزود"
        '
        'App_Suuply_cm
        '
        Me.App_Suuply_cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
        Me.App_Suuply_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.App_Suuply_cm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.App_Suuply_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.App_Suuply_cm.Enabled = False
        Me.App_Suuply_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.App_Suuply_cm.FormattingEnabled = True
        Me.App_Suuply_cm.Items.AddRange(New Object() {"CLASS", "RESAL"})
        Me.App_Suuply_cm.Location = New System.Drawing.Point(3, 25)
        Me.App_Suuply_cm.Name = "App_Suuply_cm"
        Me.App_Suuply_cm.Size = New System.Drawing.Size(386, 29)
        Me.App_Suuply_cm.TabIndex = 262
        '
        'Fonts_Path_Lb
        '
        Me.Fonts_Path_Lb.AutoSize = True
        Me.Fonts_Path_Lb.Location = New System.Drawing.Point(289, 540)
        Me.Fonts_Path_Lb.Name = "Fonts_Path_Lb"
        Me.Fonts_Path_Lb.Size = New System.Drawing.Size(140, 21)
        Me.Fonts_Path_Lb.TabIndex = 615
        Me.Fonts_Path_Lb.TabStop = True
        Me.Fonts_Path_Lb.Text = "تحميل خطوط النظام"
        '
        'Allow_MinSP_CB
        '
        Me.Allow_MinSP_CB.AutoSize = True
        Me.Allow_MinSP_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Allow_MinSP_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Allow_MinSP_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Allow_MinSP_CB.Location = New System.Drawing.Point(429, 320)
        Me.Allow_MinSP_CB.Name = "Allow_MinSP_CB"
        Me.Allow_MinSP_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Allow_MinSP_CB.Size = New System.Drawing.Size(182, 25)
        Me.Allow_MinSP_CB.TabIndex = 616
        Me.Allow_MinSP_CB.Text = "بيع بالجملة وجملة الجملة"
        Me.Allow_MinSP_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Allow_MinSP_CB.UseVisualStyleBackColor = True
        '
        'Exp_Pch_CB
        '
        Me.Exp_Pch_CB.AutoSize = True
        Me.Exp_Pch_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Exp_Pch_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Exp_Pch_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Exp_Pch_CB.Location = New System.Drawing.Point(676, 320)
        Me.Exp_Pch_CB.Name = "Exp_Pch_CB"
        Me.Exp_Pch_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Exp_Pch_CB.Size = New System.Drawing.Size(148, 25)
        Me.Exp_Pch_CB.TabIndex = 617
        Me.Exp_Pch_CB.Text = "مصاريف مشتريات"
        Me.Exp_Pch_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Exp_Pch_CB.UseVisualStyleBackColor = True
        '
        'Phone_For_Tables_CB
        '
        Me.Phone_For_Tables_CB.AutoSize = True
        Me.Phone_For_Tables_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Phone_For_Tables_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Phone_For_Tables_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Phone_For_Tables_CB.Location = New System.Drawing.Point(429, 363)
        Me.Phone_For_Tables_CB.Name = "Phone_For_Tables_CB"
        Me.Phone_For_Tables_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Phone_For_Tables_CB.Size = New System.Drawing.Size(263, 25)
        Me.Phone_For_Tables_CB.TabIndex = 618
        Me.Phone_For_Tables_CB.Text = "إستخدام الهاتف للإدراج على الطاولات"
        Me.Phone_For_Tables_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Phone_For_Tables_CB.UseVisualStyleBackColor = True
        Me.Phone_For_Tables_CB.Visible = False
        '
        'is_Pch_Discount_Distribute_CB
        '
        Me.is_Pch_Discount_Distribute_CB.AutoSize = True
        Me.is_Pch_Discount_Distribute_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_Pch_Discount_Distribute_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.is_Pch_Discount_Distribute_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.is_Pch_Discount_Distribute_CB.Location = New System.Drawing.Point(708, 363)
        Me.is_Pch_Discount_Distribute_CB.Name = "is_Pch_Discount_Distribute_CB"
        Me.is_Pch_Discount_Distribute_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.is_Pch_Discount_Distribute_CB.Size = New System.Drawing.Size(275, 25)
        Me.is_Pch_Discount_Distribute_CB.TabIndex = 619
        Me.is_Pch_Discount_Distribute_CB.Text = "توزيع تخفيض المشتريات على الأصناف"
        Me.is_Pch_Discount_Distribute_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.is_Pch_Discount_Distribute_CB.UseVisualStyleBackColor = True
        Me.is_Pch_Discount_Distribute_CB.Visible = False
        '
        'is_Auto_Recive_ST_Tran_CB
        '
        Me.is_Auto_Recive_ST_Tran_CB.AutoSize = True
        Me.is_Auto_Recive_ST_Tran_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_Auto_Recive_ST_Tran_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.is_Auto_Recive_ST_Tran_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.is_Auto_Recive_ST_Tran_CB.Location = New System.Drawing.Point(429, 404)
        Me.is_Auto_Recive_ST_Tran_CB.Name = "is_Auto_Recive_ST_Tran_CB"
        Me.is_Auto_Recive_ST_Tran_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.is_Auto_Recive_ST_Tran_CB.Size = New System.Drawing.Size(244, 25)
        Me.is_Auto_Recive_ST_Tran_CB.TabIndex = 620
        Me.is_Auto_Recive_ST_Tran_CB.Text = "إستلام الأصناف المحولة بشكل ألي"
        Me.is_Auto_Recive_ST_Tran_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.is_Auto_Recive_ST_Tran_CB.UseVisualStyleBackColor = True
        Me.is_Auto_Recive_ST_Tran_CB.Visible = False
        '
        'IM_Valid_CB
        '
        Me.IM_Valid_CB.AutoSize = True
        Me.IM_Valid_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Valid_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.IM_Valid_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Valid_CB.Location = New System.Drawing.Point(708, 404)
        Me.IM_Valid_CB.Name = "IM_Valid_CB"
        Me.IM_Valid_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_Valid_CB.Size = New System.Drawing.Size(160, 25)
        Me.IM_Valid_CB.TabIndex = 621
        Me.IM_Valid_CB.Text = "صلاحية تابعة للصنف"
        Me.IM_Valid_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IM_Valid_CB.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(140, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 21)
        Me.Label1.TabIndex = 623
        Me.Label1.Text = "Num Of Demo Bills"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.NumOfBillsTest_txt)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(704, 530)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(289, 56)
        Me.Panel1.TabIndex = 624
        Me.Panel1.Visible = False
        '
        'NumOfBillsTest_txt
        '
        Me.NumOfBillsTest_txt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.NumOfBillsTest_txt.BackColor = System.Drawing.Color.Lavender
        Me.NumOfBillsTest_txt.Location = New System.Drawing.Point(3, 24)
        Me.NumOfBillsTest_txt.MaxLength = 18
        Me.NumOfBillsTest_txt.Name = "NumOfBillsTest_txt"
        Me.NumOfBillsTest_txt.Size = New System.Drawing.Size(134, 29)
        Me.NumOfBillsTest_txt.TabIndex = 622
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(507, 556)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBox1.Size = New System.Drawing.Size(191, 29)
        Me.TextBox1.TabIndex = 625
        '
        'TB_Auto_Print_CB
        '
        Me.TB_Auto_Print_CB.AutoSize = True
        Me.TB_Auto_Print_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TB_Auto_Print_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.TB_Auto_Print_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_Auto_Print_CB.Location = New System.Drawing.Point(429, 453)
        Me.TB_Auto_Print_CB.Name = "TB_Auto_Print_CB"
        Me.TB_Auto_Print_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TB_Auto_Print_CB.Size = New System.Drawing.Size(289, 25)
        Me.TB_Auto_Print_CB.TabIndex = 626
        Me.TB_Auto_Print_CB.Text = "طباعة التجهيز بشكل ألي من خلال السيرفر"
        Me.TB_Auto_Print_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.TB_Auto_Print_CB.UseVisualStyleBackColor = True
        Me.TB_Auto_Print_CB.Visible = False
        '
        'SetSystem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 587)
        Me.Controls.Add(Me.TB_Auto_Print_CB)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.IM_Valid_CB)
        Me.Controls.Add(Me.is_Auto_Recive_ST_Tran_CB)
        Me.Controls.Add(Me.is_Pch_Discount_Distribute_CB)
        Me.Controls.Add(Me.Phone_For_Tables_CB)
        Me.Controls.Add(Me.Exp_Pch_CB)
        Me.Controls.Add(Me.Allow_MinSP_CB)
        Me.Controls.Add(Me.Fonts_Path_Lb)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.IMPH_Btn)
        Me.Controls.Add(Me.IM_Photo)
        Me.Controls.Add(Me.Out_Travel_CB)
        Me.Controls.Add(Me.SerialCode_CB)
        Me.Controls.Add(Me.Marketers_CB)
        Me.Controls.Add(Me.GroupBox14)
        Me.Controls.Add(Me.Agent_Cards_CB)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Save_butt)
        Me.Controls.Add(Me.inside_Bill_CB)
        Me.Controls.Add(Me.Frm_CB)
        Me.Controls.Add(Me.Stores_CB)
        Me.Controls.Add(Me.Orders_CB)
        Me.Controls.Add(Me.Notes_CB)
        Me.Controls.Add(Me.SubPrints_CB)
        Me.Controls.Add(Me.Tables_CB)
        Me.Controls.Add(Me.Pr_CB)
        Me.Controls.Add(Me.GroupBox3)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "SetSystem"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الضبط الأساسي للنظام"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox14.ResumeLayout(False)
        CType(Me.IM_Photo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Sys_Type_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Pr_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Tables_CB As System.Windows.Forms.CheckBox
    Friend WithEvents SubPrints_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Notes_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Orders_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Stores_CB As System.Windows.Forms.CheckBox
    Friend WithEvents inside_Bill_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Frm_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Save_butt As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents SScreenDefault_Cmb As System.Windows.Forms.ComboBox
    Friend WithEvents Agent_Cards_CB As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents IM_Type_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Marketers_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Out_Travel_CB As System.Windows.Forms.CheckBox
    Friend WithEvents SerialCode_CB As System.Windows.Forms.CheckBox
    Friend WithEvents IMPH_Btn As System.Windows.Forms.Button
    Friend WithEvents IM_Photo As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents App_Suuply_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Fonts_Path_Lb As System.Windows.Forms.LinkLabel
    Friend WithEvents Allow_MinSP_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Exp_Pch_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Phone_For_Tables_CB As System.Windows.Forms.CheckBox
    Friend WithEvents is_Pch_Discount_Distribute_CB As System.Windows.Forms.CheckBox
    Friend WithEvents is_Auto_Recive_ST_Tran_CB As System.Windows.Forms.CheckBox
    Friend WithEvents IM_Valid_CB As System.Windows.Forms.CheckBox
    Friend WithEvents NumOfBillsTest_txt As F3NumericTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TB_Auto_Print_CB As CheckBox
End Class
