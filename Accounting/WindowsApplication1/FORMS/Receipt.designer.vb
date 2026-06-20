<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Receipt
    Inherits Base_Form
    ' Inherits System.Windows.Forms.Form
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Receipt))
        Me.NULLContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Get_Tr_Btn = New System.Windows.Forms.Button()
        Me.Get_Ag_Btn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.AG_Show_Balance_CB = New System.Windows.Forms.CheckBox()
        Me.Show_Bill_CB = New System.Windows.Forms.CheckBox()
        Me.Title_Lb = New System.Windows.Forms.Label()
        Me.Rct_Move_Panel = New System.Windows.Forms.Panel()
        Me.ReceiptNum_Txt = New System.Windows.Forms.TextBox()
        Me.Up_Bill_btn = New System.Windows.Forms.Button()
        Me.Down_Bill_btn = New System.Windows.Forms.Button()
        Me.save_butt = New System.Windows.Forms.Button()
        Me.print_butt = New System.Windows.Forms.Button()
        Me.new_butt = New System.Windows.Forms.Button()
        Me.Fields_Panel = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Tr_Panel = New System.Windows.Forms.Panel()
        Me.Treasury_Balance = New System.Windows.Forms.TextBox()
        Me.to_Label = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.BankPanel = New System.Windows.Forms.Panel()
        Me.CheckNum_txtb = New System.Windows.Forms.TextBox()
        Me.Label_check_num = New System.Windows.Forms.Label()
        Me.payment_Type_combo = New System.Windows.Forms.ComboBox()
        Me.Treasury_ComboBox = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Currency_Equal_txt = New Accounting.F2FloatField()
        Me.Currency_Cm = New System.Windows.Forms.ComboBox()
        Me.bankName_Combo = New System.Windows.Forms.ComboBox()
        Me.AG_Panel = New System.Windows.Forms.Panel()
        Me.Current_QTY = New System.Windows.Forms.TextBox()
        Me.from_Label = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.AG_Cm = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.B_T_ID_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.COST_CM = New System.Windows.Forms.ComboBox()
        Me.Receipt_Title_txt = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ReceiptTypeComboBox = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimeReceipt = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.money_num_txtb = New System.Windows.Forms.TextBox()
        Me.money_char_txtb = New System.Windows.Forms.TextBox()
        Me.Rct_Move_Panel.SuspendLayout()
        Me.Fields_Panel.SuspendLayout()
        Me.Tr_Panel.SuspendLayout()
        Me.BankPanel.SuspendLayout()
        Me.AG_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'NULLContextMenuStrip
        '
        Me.NULLContextMenuStrip.Name = "NULLContextMenuStrip"
        Me.NULLContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NULLContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'Get_Tr_Btn
        '
        Me.Get_Tr_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Get_Tr_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Get_Tr_Btn.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Get_Tr_Btn.Location = New System.Drawing.Point(664, 28)
        Me.Get_Tr_Btn.Name = "Get_Tr_Btn"
        Me.Get_Tr_Btn.Size = New System.Drawing.Size(39, 23)
        Me.Get_Tr_Btn.TabIndex = 665
        Me.Get_Tr_Btn.Text = "..."
        Me.ToolTip1.SetToolTip(Me.Get_Tr_Btn, "فتح قائمة البحث")
        Me.Get_Tr_Btn.UseVisualStyleBackColor = True
        '
        'Get_Ag_Btn
        '
        Me.Get_Ag_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Get_Ag_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Get_Ag_Btn.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Get_Ag_Btn.Location = New System.Drawing.Point(664, 28)
        Me.Get_Ag_Btn.Name = "Get_Ag_Btn"
        Me.Get_Ag_Btn.Size = New System.Drawing.Size(39, 23)
        Me.Get_Ag_Btn.TabIndex = 664
        Me.Get_Ag_Btn.Text = "..."
        Me.ToolTip1.SetToolTip(Me.Get_Ag_Btn, "فتح قائمة البحث")
        Me.Get_Ag_Btn.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(0, 515)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(739, 41)
        Me.Button1.TabIndex = 686
        Me.Button1.Text = "عـــودة   ↩️"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'AG_Show_Balance_CB
        '
        Me.AG_Show_Balance_CB.AutoSize = True
        Me.AG_Show_Balance_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AG_Show_Balance_CB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Show_Balance_CB.Location = New System.Drawing.Point(5, 442)
        Me.AG_Show_Balance_CB.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.AG_Show_Balance_CB.Name = "AG_Show_Balance_CB"
        Me.AG_Show_Balance_CB.Size = New System.Drawing.Size(131, 18)
        Me.AG_Show_Balance_CB.TabIndex = 674
        Me.AG_Show_Balance_CB.Text = "إظهار الرصيد فالإيصال"
        Me.AG_Show_Balance_CB.UseVisualStyleBackColor = True
        '
        'Show_Bill_CB
        '
        Me.Show_Bill_CB.AutoSize = True
        Me.Show_Bill_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_Bill_CB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Show_Bill_CB.Location = New System.Drawing.Point(167, 444)
        Me.Show_Bill_CB.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Show_Bill_CB.Name = "Show_Bill_CB"
        Me.Show_Bill_CB.Size = New System.Drawing.Size(94, 18)
        Me.Show_Bill_CB.TabIndex = 675
        Me.Show_Bill_CB.Text = "معاينة الإيصال"
        Me.Show_Bill_CB.UseVisualStyleBackColor = True
        '
        'Title_Lb
        '
        Me.Title_Lb.BackColor = System.Drawing.SystemColors.Control
        Me.Title_Lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Title_Lb.Font = New System.Drawing.Font("Arial", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Title_Lb.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Title_Lb.Location = New System.Drawing.Point(4, 2)
        Me.Title_Lb.Name = "Title_Lb"
        Me.Title_Lb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Title_Lb.Size = New System.Drawing.Size(470, 42)
        Me.Title_Lb.TabIndex = 685
        Me.Title_Lb.Text = "إيصــــــال"
        Me.Title_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Rct_Move_Panel
        '
        Me.Rct_Move_Panel.Controls.Add(Me.ReceiptNum_Txt)
        Me.Rct_Move_Panel.Controls.Add(Me.Up_Bill_btn)
        Me.Rct_Move_Panel.Controls.Add(Me.Down_Bill_btn)
        Me.Rct_Move_Panel.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rct_Move_Panel.Location = New System.Drawing.Point(477, 4)
        Me.Rct_Move_Panel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Rct_Move_Panel.Name = "Rct_Move_Panel"
        Me.Rct_Move_Panel.Size = New System.Drawing.Size(261, 39)
        Me.Rct_Move_Panel.TabIndex = 377
        '
        'ReceiptNum_Txt
        '
        Me.ReceiptNum_Txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ReceiptNum_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ReceiptNum_Txt.Font = New System.Drawing.Font("Times New Roman", 15.25!, System.Drawing.FontStyle.Bold)
        Me.ReceiptNum_Txt.ForeColor = System.Drawing.Color.Black
        Me.ReceiptNum_Txt.Location = New System.Drawing.Point(30, 4)
        Me.ReceiptNum_Txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ReceiptNum_Txt.MaxLength = 100
        Me.ReceiptNum_Txt.Name = "ReceiptNum_Txt"
        Me.ReceiptNum_Txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ReceiptNum_Txt.Size = New System.Drawing.Size(200, 31)
        Me.ReceiptNum_Txt.TabIndex = 686
        Me.ReceiptNum_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Up_Bill_btn
        '
        Me.Up_Bill_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Up_Bill_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Up_Bill_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Up_Bill_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Up_Bill_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Up_Bill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Up_Bill_btn.Font = New System.Drawing.Font("Arial Narrow", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Up_Bill_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Up_Bill_btn.Image = Global.Accounting.My.Resources.Resources.Next_button
        Me.Up_Bill_btn.Location = New System.Drawing.Point(231, 4)
        Me.Up_Bill_btn.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Up_Bill_btn.Name = "Up_Bill_btn"
        Me.Up_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Up_Bill_btn.Size = New System.Drawing.Size(27, 31)
        Me.Up_Bill_btn.TabIndex = 625
        Me.Up_Bill_btn.TabStop = False
        Me.Up_Bill_btn.UseVisualStyleBackColor = False
        '
        'Down_Bill_btn
        '
        Me.Down_Bill_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Down_Bill_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Down_Bill_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Down_Bill_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Down_Bill_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Down_Bill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Down_Bill_btn.Font = New System.Drawing.Font("Arial Narrow", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Down_Bill_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Down_Bill_btn.Image = Global.Accounting.My.Resources.Resources.before_button
        Me.Down_Bill_btn.Location = New System.Drawing.Point(2, 4)
        Me.Down_Bill_btn.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Down_Bill_btn.Name = "Down_Bill_btn"
        Me.Down_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Down_Bill_btn.Size = New System.Drawing.Size(27, 31)
        Me.Down_Bill_btn.TabIndex = 626
        Me.Down_Bill_btn.TabStop = False
        Me.Down_Bill_btn.UseVisualStyleBackColor = False
        '
        'save_butt
        '
        Me.save_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.save_butt.Enabled = False
        Me.save_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.save_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.save_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.save_butt.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.save_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.save_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.save_butt.Location = New System.Drawing.Point(221, 476)
        Me.save_butt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.save_butt.Name = "save_butt"
        Me.save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.save_butt.Size = New System.Drawing.Size(287, 36)
        Me.save_butt.TabIndex = 279
        Me.save_butt.TabStop = False
        Me.save_butt.Text = "💾 حفظ F12"
        Me.save_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.save_butt.UseVisualStyleBackColor = False
        '
        'print_butt
        '
        Me.print_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.print_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.print_butt.Enabled = False
        Me.print_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.print_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.print_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.print_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.print_butt.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.print_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.print_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.print_butt.Location = New System.Drawing.Point(1, 476)
        Me.print_butt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.print_butt.Name = "print_butt"
        Me.print_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.print_butt.Size = New System.Drawing.Size(219, 36)
        Me.print_butt.TabIndex = 281
        Me.print_butt.TabStop = False
        Me.print_butt.Text = "🖨️  طباعــة"
        Me.print_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.print_butt.UseVisualStyleBackColor = False
        '
        'new_butt
        '
        Me.new_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.new_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.new_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.new_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.new_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.new_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.new_butt.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.new_butt.ForeColor = System.Drawing.Color.Black
        Me.new_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.new_butt.Location = New System.Drawing.Point(509, 476)
        Me.new_butt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.new_butt.Name = "new_butt"
        Me.new_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.new_butt.Size = New System.Drawing.Size(230, 36)
        Me.new_butt.TabIndex = 280
        Me.new_butt.Text = " جديد F1  ➕"
        Me.new_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.new_butt.UseVisualStyleBackColor = False
        '
        'Fields_Panel
        '
        Me.Fields_Panel.BackColor = System.Drawing.Color.Transparent
        Me.Fields_Panel.Controls.Add(Me.Label16)
        Me.Fields_Panel.Controls.Add(Me.Label5)
        Me.Fields_Panel.Controls.Add(Me.Tr_Panel)
        Me.Fields_Panel.Controls.Add(Me.Currency_Equal_txt)
        Me.Fields_Panel.Controls.Add(Me.Currency_Cm)
        Me.Fields_Panel.Controls.Add(Me.bankName_Combo)
        Me.Fields_Panel.Controls.Add(Me.AG_Panel)
        Me.Fields_Panel.Controls.Add(Me.Label7)
        Me.Fields_Panel.Controls.Add(Me.B_T_ID_txt)
        Me.Fields_Panel.Controls.Add(Me.Label1)
        Me.Fields_Panel.Controls.Add(Me.Label2)
        Me.Fields_Panel.Controls.Add(Me.COST_CM)
        Me.Fields_Panel.Controls.Add(Me.Receipt_Title_txt)
        Me.Fields_Panel.Controls.Add(Me.Label6)
        Me.Fields_Panel.Controls.Add(Me.ReceiptTypeComboBox)
        Me.Fields_Panel.Controls.Add(Me.Label8)
        Me.Fields_Panel.Controls.Add(Me.Label3)
        Me.Fields_Panel.Controls.Add(Me.DateTimeReceipt)
        Me.Fields_Panel.Controls.Add(Me.Label4)
        Me.Fields_Panel.Controls.Add(Me.money_num_txtb)
        Me.Fields_Panel.Controls.Add(Me.money_char_txtb)
        Me.Fields_Panel.Enabled = False
        Me.Fields_Panel.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fields_Panel.Location = New System.Drawing.Point(4, 45)
        Me.Fields_Panel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Fields_Panel.Name = "Fields_Panel"
        Me.Fields_Panel.Size = New System.Drawing.Size(734, 429)
        Me.Fields_Panel.TabIndex = 282
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label16.Location = New System.Drawing.Point(382, 101)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(91, 18)
        Me.Label16.TabIndex = 673
        Me.Label16.Text = " سعر الصرف:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label5.Location = New System.Drawing.Point(433, 399)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 16)
        Me.Label5.TabIndex = 273
        Me.Label5.Text = " المصرف"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label5.Visible = False
        '
        'Tr_Panel
        '
        Me.Tr_Panel.Controls.Add(Me.Get_Tr_Btn)
        Me.Tr_Panel.Controls.Add(Me.Treasury_Balance)
        Me.Tr_Panel.Controls.Add(Me.to_Label)
        Me.Tr_Panel.Controls.Add(Me.Label19)
        Me.Tr_Panel.Controls.Add(Me.BankPanel)
        Me.Tr_Panel.Controls.Add(Me.payment_Type_combo)
        Me.Tr_Panel.Controls.Add(Me.Treasury_ComboBox)
        Me.Tr_Panel.Controls.Add(Me.Label13)
        Me.Tr_Panel.Location = New System.Drawing.Point(5, 176)
        Me.Tr_Panel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Tr_Panel.Name = "Tr_Panel"
        Me.Tr_Panel.Size = New System.Drawing.Size(726, 98)
        Me.Tr_Panel.TabIndex = 669
        '
        'Treasury_Balance
        '
        Me.Treasury_Balance.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Treasury_Balance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Treasury_Balance.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Treasury_Balance.Font = New System.Drawing.Font("Stencil", 11.75!)
        Me.Treasury_Balance.ForeColor = System.Drawing.Color.Black
        Me.Treasury_Balance.Location = New System.Drawing.Point(4, 2)
        Me.Treasury_Balance.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Treasury_Balance.Name = "Treasury_Balance"
        Me.Treasury_Balance.ReadOnly = True
        Me.Treasury_Balance.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Treasury_Balance.Size = New System.Drawing.Size(148, 26)
        Me.Treasury_Balance.TabIndex = 660
        Me.Treasury_Balance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'to_Label
        '
        Me.to_Label.AutoSize = True
        Me.to_Label.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.to_Label.Location = New System.Drawing.Point(649, 6)
        Me.to_Label.Name = "to_Label"
        Me.to_Label.Size = New System.Drawing.Size(50, 18)
        Me.to_Label.TabIndex = 324
        Me.to_Label.Text = "الخزينة"
        Me.to_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label19.Location = New System.Drawing.Point(156, 6)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(47, 18)
        Me.Label19.TabIndex = 372
        Me.Label19.Text = "الرصيد"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BankPanel
        '
        Me.BankPanel.Controls.Add(Me.CheckNum_txtb)
        Me.BankPanel.Controls.Add(Me.Label_check_num)
        Me.BankPanel.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BankPanel.Location = New System.Drawing.Point(4, 45)
        Me.BankPanel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BankPanel.Name = "BankPanel"
        Me.BankPanel.Size = New System.Drawing.Size(508, 35)
        Me.BankPanel.TabIndex = 376
        '
        'CheckNum_txtb
        '
        Me.CheckNum_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CheckNum_txtb.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CheckNum_txtb.Location = New System.Drawing.Point(2, 5)
        Me.CheckNum_txtb.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckNum_txtb.MaxLength = 100
        Me.CheckNum_txtb.Name = "CheckNum_txtb"
        Me.CheckNum_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckNum_txtb.Size = New System.Drawing.Size(418, 23)
        Me.CheckNum_txtb.TabIndex = 20
        Me.CheckNum_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label_check_num
        '
        Me.Label_check_num.AutoSize = True
        Me.Label_check_num.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label_check_num.Location = New System.Drawing.Point(424, 9)
        Me.Label_check_num.Name = "Label_check_num"
        Me.Label_check_num.Size = New System.Drawing.Size(75, 18)
        Me.Label_check_num.TabIndex = 277
        Me.Label_check_num.Text = "رقم الشيك"
        Me.Label_check_num.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'payment_Type_combo
        '
        Me.payment_Type_combo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.payment_Type_combo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.payment_Type_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.payment_Type_combo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.payment_Type_combo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.payment_Type_combo.ForeColor = System.Drawing.Color.Black
        Me.payment_Type_combo.FormattingEnabled = True
        Me.payment_Type_combo.Items.AddRange(New Object() {"نقـــــــداً", "شيـــــــك"})
        Me.payment_Type_combo.Location = New System.Drawing.Point(515, 51)
        Me.payment_Type_combo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.payment_Type_combo.Name = "payment_Type_combo"
        Me.payment_Type_combo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.payment_Type_combo.Size = New System.Drawing.Size(89, 24)
        Me.payment_Type_combo.TabIndex = 6
        '
        'Treasury_ComboBox
        '
        Me.Treasury_ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Treasury_ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Treasury_ComboBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Treasury_ComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Treasury_ComboBox.DropDownHeight = 200
        Me.Treasury_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Treasury_ComboBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Treasury_ComboBox.ForeColor = System.Drawing.Color.Black
        Me.Treasury_ComboBox.FormattingEnabled = True
        Me.Treasury_ComboBox.IntegralHeight = False
        Me.Treasury_ComboBox.Location = New System.Drawing.Point(213, 6)
        Me.Treasury_ComboBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Treasury_ComboBox.Name = "Treasury_ComboBox"
        Me.Treasury_ComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Treasury_ComboBox.Size = New System.Drawing.Size(430, 26)
        Me.Treasury_ComboBox.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label13.Location = New System.Drawing.Point(610, 54)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(85, 18)
        Me.Label13.TabIndex = 270
        Me.Label13.Text = "طريـقة الدفع"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Currency_Equal_txt
        '
        Me.Currency_Equal_txt.BackColor = System.Drawing.Color.AliceBlue
        Me.Currency_Equal_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Currency_Equal_txt.Enabled = False
        Me.Currency_Equal_txt.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Currency_Equal_txt.Location = New System.Drawing.Point(306, 98)
        Me.Currency_Equal_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Currency_Equal_txt.MaxLength = 0
        Me.Currency_Equal_txt.Name = "Currency_Equal_txt"
        Me.Currency_Equal_txt.Size = New System.Drawing.Size(73, 26)
        Me.Currency_Equal_txt.TabIndex = 671
        '
        'Currency_Cm
        '
        Me.Currency_Cm.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Currency_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Currency_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Currency_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Currency_Cm.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Currency_Cm.FormattingEnabled = True
        Me.Currency_Cm.Location = New System.Drawing.Point(478, 97)
        Me.Currency_Cm.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Currency_Cm.Name = "Currency_Cm"
        Me.Currency_Cm.Size = New System.Drawing.Size(126, 26)
        Me.Currency_Cm.TabIndex = 670
        '
        'bankName_Combo
        '
        Me.bankName_Combo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.bankName_Combo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.bankName_Combo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.bankName_Combo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bankName_Combo.DropDownHeight = 150
        Me.bankName_Combo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bankName_Combo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.bankName_Combo.ForeColor = System.Drawing.Color.Black
        Me.bankName_Combo.FormattingEnabled = True
        Me.bankName_Combo.IntegralHeight = False
        Me.bankName_Combo.Location = New System.Drawing.Point(280, 397)
        Me.bankName_Combo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bankName_Combo.Name = "bankName_Combo"
        Me.bankName_Combo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.bankName_Combo.Size = New System.Drawing.Size(148, 24)
        Me.bankName_Combo.TabIndex = 7
        Me.bankName_Combo.Visible = False
        '
        'AG_Panel
        '
        Me.AG_Panel.Controls.Add(Me.Get_Ag_Btn)
        Me.AG_Panel.Controls.Add(Me.Current_QTY)
        Me.AG_Panel.Controls.Add(Me.from_Label)
        Me.AG_Panel.Controls.Add(Me.Label12)
        Me.AG_Panel.Controls.Add(Me.AG_Cm)
        Me.AG_Panel.Location = New System.Drawing.Point(5, 307)
        Me.AG_Panel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.AG_Panel.Name = "AG_Panel"
        Me.AG_Panel.Size = New System.Drawing.Size(726, 67)
        Me.AG_Panel.TabIndex = 668
        '
        'Current_QTY
        '
        Me.Current_QTY.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Current_QTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Current_QTY.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Current_QTY.Font = New System.Drawing.Font("Stencil", 11.75!)
        Me.Current_QTY.ForeColor = System.Drawing.Color.Black
        Me.Current_QTY.Location = New System.Drawing.Point(4, 3)
        Me.Current_QTY.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Current_QTY.Name = "Current_QTY"
        Me.Current_QTY.ReadOnly = True
        Me.Current_QTY.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Current_QTY.Size = New System.Drawing.Size(148, 26)
        Me.Current_QTY.TabIndex = 663
        Me.Current_QTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'from_Label
        '
        Me.from_Label.AutoSize = True
        Me.from_Label.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.from_Label.Location = New System.Drawing.Point(648, 7)
        Me.from_Label.Name = "from_Label"
        Me.from_Label.Size = New System.Drawing.Size(58, 18)
        Me.from_Label.TabIndex = 231
        Me.from_Label.Text = "الحساب"
        Me.from_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label12.Location = New System.Drawing.Point(156, 7)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(47, 18)
        Me.Label12.TabIndex = 248
        Me.Label12.Text = "الرصيد"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'AG_Cm
        '
        Me.AG_Cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.AG_Cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.AG_Cm.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.AG_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AG_Cm.DropDownHeight = 200
        Me.AG_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AG_Cm.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Cm.ForeColor = System.Drawing.Color.Black
        Me.AG_Cm.FormattingEnabled = True
        Me.AG_Cm.IntegralHeight = False
        Me.AG_Cm.Location = New System.Drawing.Point(213, 5)
        Me.AG_Cm.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.AG_Cm.Name = "AG_Cm"
        Me.AG_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Cm.Size = New System.Drawing.Size(430, 26)
        Me.AG_Cm.TabIndex = 662
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label7.Location = New System.Drawing.Point(608, 100)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 18)
        Me.Label7.TabIndex = 672
        Me.Label7.Text = " العملة:"
        '
        'B_T_ID_txt
        '
        Me.B_T_ID_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.B_T_ID_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.B_T_ID_txt.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.B_T_ID_txt.ForeColor = System.Drawing.Color.Black
        Me.B_T_ID_txt.Location = New System.Drawing.Point(5, 3)
        Me.B_T_ID_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.B_T_ID_txt.Name = "B_T_ID_txt"
        Me.B_T_ID_txt.ReadOnly = True
        Me.B_T_ID_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.B_T_ID_txt.Size = New System.Drawing.Size(128, 23)
        Me.B_T_ID_txt.TabIndex = 666
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label1.Location = New System.Drawing.Point(136, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 18)
        Me.Label1.TabIndex = 667
        Me.Label1.Text = "رقم القيد"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label2.Location = New System.Drawing.Point(608, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 18)
        Me.Label2.TabIndex = 269
        Me.Label2.Text = "عنوان الإيصال"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'COST_CM
        '
        Me.COST_CM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.COST_CM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.COST_CM.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.COST_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.COST_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.COST_CM.Font = New System.Drawing.Font("Tahoma", 10.75!, System.Drawing.FontStyle.Bold)
        Me.COST_CM.FormattingEnabled = True
        Me.COST_CM.Location = New System.Drawing.Point(206, 4)
        Me.COST_CM.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.COST_CM.Name = "COST_CM"
        Me.COST_CM.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.COST_CM.Size = New System.Drawing.Size(398, 25)
        Me.COST_CM.TabIndex = 664
        '
        'Receipt_Title_txt
        '
        Me.Receipt_Title_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Receipt_Title_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Receipt_Title_txt.Font = New System.Drawing.Font("Tahoma", 11.75!, System.Drawing.FontStyle.Bold)
        Me.Receipt_Title_txt.ForeColor = System.Drawing.Color.Black
        Me.Receipt_Title_txt.Location = New System.Drawing.Point(5, 66)
        Me.Receipt_Title_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Receipt_Title_txt.Name = "Receipt_Title_txt"
        Me.Receipt_Title_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Receipt_Title_txt.Size = New System.Drawing.Size(599, 26)
        Me.Receipt_Title_txt.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(609, 7)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label6.Size = New System.Drawing.Size(93, 18)
        Me.Label6.TabIndex = 665
        Me.Label6.Text = " مركز التكلفة:"
        '
        'ReceiptTypeComboBox
        '
        Me.ReceiptTypeComboBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ReceiptTypeComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ReceiptTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ReceiptTypeComboBox.Enabled = False
        Me.ReceiptTypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ReceiptTypeComboBox.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ReceiptTypeComboBox.ForeColor = System.Drawing.Color.Black
        Me.ReceiptTypeComboBox.FormattingEnabled = True
        Me.ReceiptTypeComboBox.Location = New System.Drawing.Point(429, 39)
        Me.ReceiptTypeComboBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ReceiptTypeComboBox.Name = "ReceiptTypeComboBox"
        Me.ReceiptTypeComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ReceiptTypeComboBox.Size = New System.Drawing.Size(175, 24)
        Me.ReceiptTypeComboBox.TabIndex = 374
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label8.Location = New System.Drawing.Point(608, 42)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 18)
        Me.Label8.TabIndex = 373
        Me.Label8.Text = "نوع الإيصال"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label3.Location = New System.Drawing.Point(194, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 18)
        Me.Label3.TabIndex = 271
        Me.Label3.Text = "التاريـخ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DateTimeReceipt
        '
        Me.DateTimeReceipt.CalendarFont = New System.Drawing.Font("Segoe UI", 15.25!)
        Me.DateTimeReceipt.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.DateTimeReceipt.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.DateTimeReceipt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimeReceipt.Location = New System.Drawing.Point(6, 40)
        Me.DateTimeReceipt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DateTimeReceipt.Name = "DateTimeReceipt"
        Me.DateTimeReceipt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTimeReceipt.RightToLeftLayout = True
        Me.DateTimeReceipt.Size = New System.Drawing.Size(184, 23)
        Me.DateTimeReceipt.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.Label4.Location = New System.Drawing.Point(608, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 18)
        Me.Label4.TabIndex = 236
        Me.Label4.Text = "المبلغ بالأرقام"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'money_num_txtb
        '
        Me.money_num_txtb.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.money_num_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.money_num_txtb.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.money_num_txtb.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.money_num_txtb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.money_num_txtb.Location = New System.Drawing.Point(429, 129)
        Me.money_num_txtb.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.money_num_txtb.Name = "money_num_txtb"
        Me.money_num_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.money_num_txtb.Size = New System.Drawing.Size(175, 26)
        Me.money_num_txtb.TabIndex = 4
        Me.money_num_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'money_char_txtb
        '
        Me.money_char_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.money_char_txtb.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.money_char_txtb.ForeColor = System.Drawing.Color.Black
        Me.money_char_txtb.Location = New System.Drawing.Point(5, 129)
        Me.money_char_txtb.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.money_char_txtb.Multiline = True
        Me.money_char_txtb.Name = "money_char_txtb"
        Me.money_char_txtb.ReadOnly = True
        Me.money_char_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.money_char_txtb.Size = New System.Drawing.Size(423, 26)
        Me.money_char_txtb.TabIndex = 241
        '
        'Receipt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 556)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.AG_Show_Balance_CB)
        Me.Controls.Add(Me.Show_Bill_CB)
        Me.Controls.Add(Me.Title_Lb)
        Me.Controls.Add(Me.Rct_Move_Panel)
        Me.Controls.Add(Me.save_butt)
        Me.Controls.Add(Me.print_butt)
        Me.Controls.Add(Me.new_butt)
        Me.Controls.Add(Me.Fields_Panel)
        Me.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Name = "Receipt"
        Me.Text = "شاشة الإيصالات"
        Me.Rct_Move_Panel.ResumeLayout(False)
        Me.Rct_Move_Panel.PerformLayout()
        Me.Fields_Panel.ResumeLayout(False)
        Me.Fields_Panel.PerformLayout()
        Me.Tr_Panel.ResumeLayout(False)
        Me.Tr_Panel.PerformLayout()
        Me.BankPanel.ResumeLayout(False)
        Me.BankPanel.PerformLayout()
        Me.AG_Panel.ResumeLayout(False)
        Me.AG_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Fields_Panel As System.Windows.Forms.Panel
    Friend WithEvents BankPanel As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents bankName_Combo As System.Windows.Forms.ComboBox
    Friend WithEvents CheckNum_txtb As System.Windows.Forms.TextBox
    Friend WithEvents Label_check_num As System.Windows.Forms.Label
    Friend WithEvents ReceiptTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents payment_Type_combo As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents to_Label As System.Windows.Forms.Label
    Friend WithEvents Treasury_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DateTimeReceipt As System.Windows.Forms.DateTimePicker
    Friend WithEvents from_Label As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents money_num_txtb As System.Windows.Forms.TextBox
    Friend WithEvents money_char_txtb As System.Windows.Forms.TextBox
    Friend WithEvents new_butt As System.Windows.Forms.Button
    Friend WithEvents print_butt As System.Windows.Forms.Button
    Friend WithEvents save_butt As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Receipt_Title_txt As System.Windows.Forms.TextBox
    Friend WithEvents Up_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Down_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Rct_Move_Panel As System.Windows.Forms.Panel
    Friend WithEvents Title_Lb As System.Windows.Forms.Label
    Friend WithEvents ReceiptNum_Txt As System.Windows.Forms.TextBox
    Friend WithEvents AG_Show_Balance_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Show_Bill_CB As System.Windows.Forms.CheckBox
    Friend WithEvents NULLContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Treasury_Balance As TextBox
    Friend WithEvents AG_Cm As ComboBox
    Friend WithEvents Current_QTY As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents COST_CM As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents B_T_ID_txt As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Tr_Panel As Panel
    Friend WithEvents AG_Panel As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents Currency_Equal_txt As F2FloatField
    Friend WithEvents Currency_Cm As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Get_Tr_Btn As Button
    Friend WithEvents Get_Ag_Btn As Button
End Class
