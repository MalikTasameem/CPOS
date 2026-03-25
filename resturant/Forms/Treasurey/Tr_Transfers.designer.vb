<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tr_Transfers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tr_Transfers))
        Me.FormPanel = New System.Windows.Forms.Panel()
        Me.Void_Lb = New System.Windows.Forms.Label()
        Me.Cancel_Btn = New System.Windows.Forms.Button()
        Me.ReceiptNum_Txt = New System.Windows.Forms.TextBox()
        Me.NULLContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Down_Bill_btn = New System.Windows.Forms.Button()
        Me.Up_Bill_btn = New System.Windows.Forms.Button()
        Me.Fields_Panel = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.T_Treasury_Balance = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.T_Treasury_ComboBox = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.F_Treasury_Balance = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.F_Treasury_ComboBox = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimeReceipt = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.money_num_txtb = New System.Windows.Forms.TextBox()
        Me.ReDescription_txtb = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.money_char_txtb = New System.Windows.Forms.TextBox()
        Me.new_butt = New System.Windows.Forms.Button()
        Me.print_butt = New System.Windows.Forms.Button()
        Me.save_butt = New System.Windows.Forms.Button()
        Me.FormPanel.SuspendLayout()
        Me.Fields_Panel.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FormPanel
        '
        Me.FormPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FormPanel.Controls.Add(Me.Void_Lb)
        Me.FormPanel.Controls.Add(Me.Cancel_Btn)
        Me.FormPanel.Controls.Add(Me.ReceiptNum_Txt)
        Me.FormPanel.Controls.Add(Me.ExitFormButton)
        Me.FormPanel.Controls.Add(Me.Down_Bill_btn)
        Me.FormPanel.Controls.Add(Me.Up_Bill_btn)
        Me.FormPanel.Controls.Add(Me.Fields_Panel)
        Me.FormPanel.Controls.Add(Me.new_butt)
        Me.FormPanel.Controls.Add(Me.print_butt)
        Me.FormPanel.Controls.Add(Me.save_butt)
        Me.FormPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FormPanel.Location = New System.Drawing.Point(0, 0)
        Me.FormPanel.Name = "FormPanel"
        Me.FormPanel.Size = New System.Drawing.Size(747, 403)
        Me.FormPanel.TabIndex = 380
        '
        'Void_Lb
        '
        Me.Void_Lb.BackColor = System.Drawing.Color.IndianRed
        Me.Void_Lb.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Void_Lb.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Void_Lb.Location = New System.Drawing.Point(6, 354)
        Me.Void_Lb.Name = "Void_Lb"
        Me.Void_Lb.Size = New System.Drawing.Size(154, 45)
        Me.Void_Lb.TabIndex = 660
        Me.Void_Lb.Text = "ملغــية"
        Me.Void_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Void_Lb.Visible = False
        '
        'Cancel_Btn
        '
        Me.Cancel_Btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Cancel_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cancel_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Cancel_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Cancel_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Cancel_Btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Cancel_Btn.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.Cancel_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Cancel_Btn.Location = New System.Drawing.Point(163, 354)
        Me.Cancel_Btn.Name = "Cancel_Btn"
        Me.Cancel_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Cancel_Btn.Size = New System.Drawing.Size(112, 45)
        Me.Cancel_Btn.TabIndex = 659
        Me.Cancel_Btn.TabStop = False
        Me.Cancel_Btn.Text = "إلغاء"
        Me.Cancel_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Cancel_Btn.UseVisualStyleBackColor = False
        '
        'ReceiptNum_Txt
        '
        Me.ReceiptNum_Txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ReceiptNum_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ReceiptNum_Txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.ReceiptNum_Txt.Font = New System.Drawing.Font("Times New Roman", 16.75!, System.Drawing.FontStyle.Bold)
        Me.ReceiptNum_Txt.ForeColor = System.Drawing.Color.Black
        Me.ReceiptNum_Txt.Location = New System.Drawing.Point(541, 2)
        Me.ReceiptNum_Txt.MaxLength = 250
        Me.ReceiptNum_Txt.Name = "ReceiptNum_Txt"
        Me.ReceiptNum_Txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ReceiptNum_Txt.Size = New System.Drawing.Size(164, 33)
        Me.ReceiptNum_Txt.TabIndex = 628
        Me.ReceiptNum_Txt.Text = "0"
        Me.ReceiptNum_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NULLContextMenuStrip
        '
        Me.NULLContextMenuStrip.Name = "NULLContextMenuStrip"
        Me.NULLContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NULLContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(619, 354)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(124, 45)
        Me.ExitFormButton.TabIndex = 658
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Down_Bill_btn
        '
        Me.Down_Bill_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Down_Bill_btn.BackgroundImage = Global.resturant.My.Resources.Resources.iconfinder_Down
        Me.Down_Bill_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Down_Bill_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Down_Bill_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Down_Bill_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Down_Bill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Down_Bill_btn.Font = New System.Drawing.Font("Arial Narrow", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Down_Bill_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Down_Bill_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Down_Bill_btn.Location = New System.Drawing.Point(504, 2)
        Me.Down_Bill_btn.Name = "Down_Bill_btn"
        Me.Down_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Down_Bill_btn.Size = New System.Drawing.Size(36, 33)
        Me.Down_Bill_btn.TabIndex = 627
        Me.Down_Bill_btn.TabStop = False
        Me.Down_Bill_btn.UseVisualStyleBackColor = False
        '
        'Up_Bill_btn
        '
        Me.Up_Bill_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Up_Bill_btn.BackgroundImage = Global.resturant.My.Resources.Resources.iconfinder_up_3017922
        Me.Up_Bill_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Up_Bill_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Up_Bill_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Up_Bill_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Up_Bill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Up_Bill_btn.Font = New System.Drawing.Font("Arial Narrow", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Up_Bill_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Up_Bill_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Up_Bill_btn.Location = New System.Drawing.Point(706, 2)
        Me.Up_Bill_btn.Name = "Up_Bill_btn"
        Me.Up_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Up_Bill_btn.Size = New System.Drawing.Size(36, 33)
        Me.Up_Bill_btn.TabIndex = 626
        Me.Up_Bill_btn.TabStop = False
        Me.Up_Bill_btn.UseVisualStyleBackColor = False
        '
        'Fields_Panel
        '
        Me.Fields_Panel.BackColor = System.Drawing.Color.Transparent
        Me.Fields_Panel.Controls.Add(Me.GroupBox2)
        Me.Fields_Panel.Controls.Add(Me.GroupBox1)
        Me.Fields_Panel.Controls.Add(Me.Label3)
        Me.Fields_Panel.Controls.Add(Me.DateTimeReceipt)
        Me.Fields_Panel.Controls.Add(Me.Label4)
        Me.Fields_Panel.Controls.Add(Me.money_num_txtb)
        Me.Fields_Panel.Controls.Add(Me.ReDescription_txtb)
        Me.Fields_Panel.Controls.Add(Me.Label6)
        Me.Fields_Panel.Controls.Add(Me.Label9)
        Me.Fields_Panel.Controls.Add(Me.money_char_txtb)
        Me.Fields_Panel.Enabled = False
        Me.Fields_Panel.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fields_Panel.Location = New System.Drawing.Point(3, 36)
        Me.Fields_Panel.Name = "Fields_Panel"
        Me.Fields_Panel.Size = New System.Drawing.Size(740, 316)
        Me.Fields_Panel.TabIndex = 282
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.T_Treasury_Balance)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.T_Treasury_ComboBox)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Green
        Me.GroupBox2.Location = New System.Drawing.Point(3, 107)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(731, 70)
        Me.GroupBox2.TabIndex = 374
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "إلى حســـــاب"
        '
        'T_Treasury_Balance
        '
        Me.T_Treasury_Balance.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.T_Treasury_Balance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.T_Treasury_Balance.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.T_Treasury_Balance.Font = New System.Drawing.Font("Stencil", 15.0!)
        Me.T_Treasury_Balance.ForeColor = System.Drawing.Color.Black
        Me.T_Treasury_Balance.Location = New System.Drawing.Point(4, 28)
        Me.T_Treasury_Balance.Name = "T_Treasury_Balance"
        Me.T_Treasury_Balance.ReadOnly = True
        Me.T_Treasury_Balance.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.T_Treasury_Balance.Size = New System.Drawing.Size(181, 31)
        Me.T_Treasury_Balance.TabIndex = 376
        Me.T_Treasury_Balance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(187, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 21)
        Me.Label2.TabIndex = 372
        Me.Label2.Text = "رصيد الخزينة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'T_Treasury_ComboBox
        '
        Me.T_Treasury_ComboBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.T_Treasury_ComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.T_Treasury_ComboBox.DropDownHeight = 200
        Me.T_Treasury_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.T_Treasury_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.T_Treasury_ComboBox.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.T_Treasury_ComboBox.ForeColor = System.Drawing.Color.Black
        Me.T_Treasury_ComboBox.FormattingEnabled = True
        Me.T_Treasury_ComboBox.IntegralHeight = False
        Me.T_Treasury_ComboBox.Location = New System.Drawing.Point(288, 29)
        Me.T_Treasury_ComboBox.Name = "T_Treasury_ComboBox"
        Me.T_Treasury_ComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.T_Treasury_ComboBox.Size = New System.Drawing.Size(388, 31)
        Me.T_Treasury_ComboBox.TabIndex = 323
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(678, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 21)
        Me.Label5.TabIndex = 324
        Me.Label5.Text = "الخزينة"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.F_Treasury_Balance)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.F_Treasury_ComboBox)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.DarkRed
        Me.GroupBox1.Location = New System.Drawing.Point(3, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(735, 70)
        Me.GroupBox1.TabIndex = 373
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "من حســـــاب"
        '
        'F_Treasury_Balance
        '
        Me.F_Treasury_Balance.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.F_Treasury_Balance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.F_Treasury_Balance.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.F_Treasury_Balance.Font = New System.Drawing.Font("Stencil", 15.0!)
        Me.F_Treasury_Balance.ForeColor = System.Drawing.Color.Black
        Me.F_Treasury_Balance.Location = New System.Drawing.Point(4, 28)
        Me.F_Treasury_Balance.Name = "F_Treasury_Balance"
        Me.F_Treasury_Balance.ReadOnly = True
        Me.F_Treasury_Balance.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.F_Treasury_Balance.Size = New System.Drawing.Size(181, 31)
        Me.F_Treasury_Balance.TabIndex = 375
        Me.F_Treasury_Balance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label19.Location = New System.Drawing.Point(189, 34)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(93, 21)
        Me.Label19.TabIndex = 372
        Me.Label19.Text = "رصيد الخزينة"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'F_Treasury_ComboBox
        '
        Me.F_Treasury_ComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.F_Treasury_ComboBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.F_Treasury_ComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.F_Treasury_ComboBox.DropDownHeight = 200
        Me.F_Treasury_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.F_Treasury_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.F_Treasury_ComboBox.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.F_Treasury_ComboBox.ForeColor = System.Drawing.Color.Black
        Me.F_Treasury_ComboBox.FormattingEnabled = True
        Me.F_Treasury_ComboBox.IntegralHeight = False
        Me.F_Treasury_ComboBox.Location = New System.Drawing.Point(291, 29)
        Me.F_Treasury_ComboBox.Name = "F_Treasury_ComboBox"
        Me.F_Treasury_ComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.F_Treasury_ComboBox.Size = New System.Drawing.Size(388, 31)
        Me.F_Treasury_ComboBox.TabIndex = 323
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label18.Location = New System.Drawing.Point(681, 34)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(54, 21)
        Me.Label18.TabIndex = 324
        Me.Label18.Text = "الخزينة"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(665, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 25)
        Me.Label3.TabIndex = 271
        Me.Label3.Text = "التــاريـخ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DateTimeReceipt
        '
        Me.DateTimeReceipt.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimeReceipt.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.DateTimeReceipt.Font = New System.Drawing.Font("Tahoma", 13.75!)
        Me.DateTimeReceipt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimeReceipt.Location = New System.Drawing.Point(416, 6)
        Me.DateTimeReceipt.Name = "DateTimeReceipt"
        Me.DateTimeReceipt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTimeReceipt.RightToLeftLayout = True
        Me.DateTimeReceipt.Size = New System.Drawing.Size(245, 30)
        Me.DateTimeReceipt.TabIndex = 242
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(614, 188)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 25)
        Me.Label4.TabIndex = 236
        Me.Label4.Text = "المبلغ بالأرقام"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'money_num_txtb
        '
        Me.money_num_txtb.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.money_num_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.money_num_txtb.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.money_num_txtb.Font = New System.Drawing.Font("Stencil", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.money_num_txtb.ForeColor = System.Drawing.Color.DarkGreen
        Me.money_num_txtb.Location = New System.Drawing.Point(426, 181)
        Me.money_num_txtb.Name = "money_num_txtb"
        Me.money_num_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.money_num_txtb.Size = New System.Drawing.Size(185, 36)
        Me.money_num_txtb.TabIndex = 237
        Me.money_num_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ReDescription_txtb
        '
        Me.ReDescription_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ReDescription_txtb.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.ReDescription_txtb.Location = New System.Drawing.Point(7, 220)
        Me.ReDescription_txtb.Multiline = True
        Me.ReDescription_txtb.Name = "ReDescription_txtb"
        Me.ReDescription_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ReDescription_txtb.Size = New System.Drawing.Size(604, 57)
        Me.ReDescription_txtb.TabIndex = 244
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(614, 282)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 25)
        Me.Label6.TabIndex = 240
        Me.Label6.Text = "المبلغ بالحروف"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(614, 225)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 25)
        Me.Label9.TabIndex = 243
        Me.Label9.Text = "البيـــان"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'money_char_txtb
        '
        Me.money_char_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.money_char_txtb.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.money_char_txtb.ForeColor = System.Drawing.Color.SeaGreen
        Me.money_char_txtb.Location = New System.Drawing.Point(6, 280)
        Me.money_char_txtb.Name = "money_char_txtb"
        Me.money_char_txtb.ReadOnly = True
        Me.money_char_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.money_char_txtb.Size = New System.Drawing.Size(605, 31)
        Me.money_char_txtb.TabIndex = 241
        Me.money_char_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'new_butt
        '
        Me.new_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.new_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.new_butt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.new_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.new_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.new_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.new_butt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.new_butt.ForeColor = System.Drawing.Color.Black
        Me.new_butt.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.new_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.new_butt.Location = New System.Drawing.Point(504, 354)
        Me.new_butt.Name = "new_butt"
        Me.new_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.new_butt.Size = New System.Drawing.Size(112, 45)
        Me.new_butt.TabIndex = 280
        Me.new_butt.Text = " جديـد F1"
        Me.new_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.new_butt.UseVisualStyleBackColor = False
        '
        'print_butt
        '
        Me.print_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.print_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.print_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.print_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.print_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.print_butt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.print_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.print_butt.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.print_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.print_butt.Location = New System.Drawing.Point(276, 354)
        Me.print_butt.Name = "print_butt"
        Me.print_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.print_butt.Size = New System.Drawing.Size(112, 45)
        Me.print_butt.TabIndex = 281
        Me.print_butt.TabStop = False
        Me.print_butt.Text = "طباعة"
        Me.print_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.print_butt.UseVisualStyleBackColor = False
        '
        'save_butt
        '
        Me.save_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.save_butt.Enabled = False
        Me.save_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.save_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.save_butt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.save_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.save_butt.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.save_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.save_butt.Location = New System.Drawing.Point(390, 354)
        Me.save_butt.Name = "save_butt"
        Me.save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.save_butt.Size = New System.Drawing.Size(112, 45)
        Me.save_butt.TabIndex = 279
        Me.save_butt.TabStop = False
        Me.save_butt.Text = "حفظ F2"
        Me.save_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.save_butt.UseVisualStyleBackColor = False
        '
        'Tr_Transfers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(747, 403)
        Me.Controls.Add(Me.FormPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Tr_Transfers"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تحويل بين حسابات الخزائن"
        Me.FormPanel.ResumeLayout(False)
        Me.FormPanel.PerformLayout()
        Me.Fields_Panel.ResumeLayout(False)
        Me.Fields_Panel.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FormPanel As System.Windows.Forms.Panel
    Friend WithEvents Fields_Panel As System.Windows.Forms.Panel
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents F_Treasury_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimeReceipt As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents money_num_txtb As System.Windows.Forms.TextBox
    Friend WithEvents ReDescription_txtb As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents money_char_txtb As System.Windows.Forms.TextBox
    Friend WithEvents new_butt As System.Windows.Forms.Button
    Friend WithEvents print_butt As System.Windows.Forms.Button
    Friend WithEvents save_butt As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents T_Treasury_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents NULLContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents ReceiptNum_Txt As System.Windows.Forms.TextBox
    Friend WithEvents Down_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Up_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Cancel_Btn As System.Windows.Forms.Button
    Friend WithEvents Void_Lb As System.Windows.Forms.Label
    Friend WithEvents T_Treasury_Balance As TextBox
    Friend WithEvents F_Treasury_Balance As TextBox
End Class
