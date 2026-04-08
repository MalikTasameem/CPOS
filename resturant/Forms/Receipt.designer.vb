<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Receipt
    Inherits System.Windows.Forms.Form

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
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.AG_Show_Balance_CB = New System.Windows.Forms.CheckBox()
        Me.Show_Bill_CB = New System.Windows.Forms.CheckBox()
        Me.Title_Lb = New System.Windows.Forms.Label()
        Me.Edit_butt = New System.Windows.Forms.Button()
        Me.Rct_Move_Panel = New System.Windows.Forms.Panel()
        Me.ReceiptNum_Txt = New System.Windows.Forms.TextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.إضافةالإيصالإلىقائمةالعهدToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Up_Bill_btn = New System.Windows.Forms.Button()
        Me.Down_Bill_btn = New System.Windows.Forms.Button()
        Me.Void_Lb = New System.Windows.Forms.Label()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.Fields_Panel = New System.Windows.Forms.Panel()
        Me.CR_Phone_Txt = New System.Windows.Forms.TextBox()
        Me.Treasury_Balance = New System.Windows.Forms.TextBox()
        Me.NULLContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Receipt_Title_combobox = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
        Me.BankPanel = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.bankName_Combo = New System.Windows.Forms.ComboBox()
        Me.CheckNum_txtb = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ReceiptTypeComboBox = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.payment_Type_combo = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Treasury_ComboBox = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Current_QTY = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DateTimeReceipt = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.money_num_txtb = New System.Windows.Forms.TextBox()
        Me.Notes_txtb = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.money_char_txtb = New System.Windows.Forms.TextBox()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.new_butt = New System.Windows.Forms.Button()
        Me.print_butt = New System.Windows.Forms.Button()
        Me.save_butt = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AG_Cm = New resturant.FSearch_Filter()
        Me.TitleBar_Panel.SuspendLayout()
        Me.Rct_Move_Panel.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Fields_Panel.SuspendLayout()
        Me.BankPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.Void_Lb)
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(702, 40)
        Me.TitleBar_Panel.TabIndex = 999
        Me.TitleBar_Panel.Tag = "HEADER"
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.ExitFormButton.FlatAppearance.BorderSize = 0
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.Color.White
        Me.ExitFormButton.Location = New System.Drawing.Point(0, 0)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(45, 40)
        Me.ExitFormButton.TabIndex = 3
        Me.ExitFormButton.Tag = "APP_CONTROL"
        Me.ExitFormButton.Text = "X"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'TopTitle_LB
        '
        Me.TopTitle_LB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TopTitle_LB.AutoSize = True
        Me.TopTitle_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TopTitle_LB.ForeColor = System.Drawing.Color.White
        Me.TopTitle_LB.Location = New System.Drawing.Point(589, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(90, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "الإيصــــــالات"
        '
        'AG_Show_Balance_CB
        '
        Me.AG_Show_Balance_CB.AutoSize = True
        Me.AG_Show_Balance_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AG_Show_Balance_CB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Show_Balance_CB.Location = New System.Drawing.Point(8, 458)
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
        Me.Show_Bill_CB.Location = New System.Drawing.Point(144, 458)
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
        Me.Title_Lb.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title_Lb.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Title_Lb.Location = New System.Drawing.Point(4, 44)
        Me.Title_Lb.Name = "Title_Lb"
        Me.Title_Lb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Title_Lb.Size = New System.Drawing.Size(426, 40)
        Me.Title_Lb.TabIndex = 685
        Me.Title_Lb.Text = "إيصــــــال"
        Me.Title_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Edit_butt
        '
        Me.Edit_butt.BackColor = System.Drawing.Color.White
        Me.Edit_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Edit_butt.Enabled = False
        Me.Edit_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Edit_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Edit_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Edit_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Edit_butt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Edit_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Edit_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Edit_butt.Location = New System.Drawing.Point(300, 483)
        Me.Edit_butt.Name = "Edit_butt"
        Me.Edit_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Edit_butt.Size = New System.Drawing.Size(109, 38)
        Me.Edit_butt.TabIndex = 648
        Me.Edit_butt.TabStop = False
        Me.Edit_butt.Tag = "GENERAL"
        Me.Edit_butt.Text = "تعديـل F3"
        Me.Edit_butt.UseVisualStyleBackColor = False
        '
        'Rct_Move_Panel
        '
        Me.Rct_Move_Panel.Controls.Add(Me.ReceiptNum_Txt)
        Me.Rct_Move_Panel.Controls.Add(Me.Up_Bill_btn)
        Me.Rct_Move_Panel.Controls.Add(Me.Down_Bill_btn)
        Me.Rct_Move_Panel.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rct_Move_Panel.Location = New System.Drawing.Point(439, 45)
        Me.Rct_Move_Panel.Name = "Rct_Move_Panel"
        Me.Rct_Move_Panel.Size = New System.Drawing.Size(262, 39)
        Me.Rct_Move_Panel.TabIndex = 377
        '
        'ReceiptNum_Txt
        '
        Me.ReceiptNum_Txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ReceiptNum_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ReceiptNum_Txt.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ReceiptNum_Txt.Font = New System.Drawing.Font("Times New Roman", 15.25!, System.Drawing.FontStyle.Bold)
        Me.ReceiptNum_Txt.ForeColor = System.Drawing.Color.Black
        Me.ReceiptNum_Txt.Location = New System.Drawing.Point(51, 5)
        Me.ReceiptNum_Txt.MaxLength = 100
        Me.ReceiptNum_Txt.Name = "ReceiptNum_Txt"
        Me.ReceiptNum_Txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ReceiptNum_Txt.Size = New System.Drawing.Size(160, 31)
        Me.ReceiptNum_Txt.TabIndex = 686
        Me.ReceiptNum_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.إضافةالإيصالإلىقائمةالعهدToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(192, 26)
        '
        'إضافةالإيصالإلىقائمةالعهدToolStripMenuItem
        '
        Me.إضافةالإيصالإلىقائمةالعهدToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.إضافةالإيصالإلىقائمةالعهدToolStripMenuItem.Name = "إضافةالإيصالإلىقائمةالعهدToolStripMenuItem"
        Me.إضافةالإيصالإلىقائمةالعهدToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.إضافةالإيصالإلىقائمةالعهدToolStripMenuItem.Text = "إعتماد الإيصال كعهدة"
        '
        'Up_Bill_btn
        '
        Me.Up_Bill_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Up_Bill_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Up_Bill_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Up_Bill_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Up_Bill_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Up_Bill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Up_Bill_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Up_Bill_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Up_Bill_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Up_Bill_btn.Location = New System.Drawing.Point(221, 3)
        Me.Up_Bill_btn.Name = "Up_Bill_btn"
        Me.Up_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Up_Bill_btn.Size = New System.Drawing.Size(37, 34)
        Me.Up_Bill_btn.TabIndex = 625
        Me.Up_Bill_btn.TabStop = False
        Me.Up_Bill_btn.Text = "▲"
        Me.Up_Bill_btn.TextAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.Down_Bill_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Down_Bill_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Down_Bill_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Down_Bill_btn.Location = New System.Drawing.Point(3, 3)
        Me.Down_Bill_btn.Name = "Down_Bill_btn"
        Me.Down_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Down_Bill_btn.Size = New System.Drawing.Size(39, 34)
        Me.Down_Bill_btn.TabIndex = 626
        Me.Down_Bill_btn.TabStop = False
        Me.Down_Bill_btn.Text = "▼"
        Me.Down_Bill_btn.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Down_Bill_btn.UseVisualStyleBackColor = False
        '
        'Void_Lb
        '
        Me.Void_Lb.BackColor = System.Drawing.Color.Red
        Me.Void_Lb.Dock = System.Windows.Forms.DockStyle.Left
        Me.Void_Lb.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Void_Lb.ForeColor = System.Drawing.Color.White
        Me.Void_Lb.Location = New System.Drawing.Point(45, 0)
        Me.Void_Lb.Name = "Void_Lb"
        Me.Void_Lb.Size = New System.Drawing.Size(520, 40)
        Me.Void_Lb.TabIndex = 382
        Me.Void_Lb.Tag = "DELETE"
        Me.Void_Lb.Text = "إيصال ملغي"
        Me.Void_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Void_Lb.Visible = False
        '
        'DeleteButton
        '
        Me.DeleteButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteButton.Enabled = False
        Me.DeleteButton.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.DeleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.DeleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteButton.Location = New System.Drawing.Point(212, 483)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DeleteButton.Size = New System.Drawing.Size(85, 38)
        Me.DeleteButton.TabIndex = 380
        Me.DeleteButton.TabStop = False
        Me.DeleteButton.Tag = "DELETE"
        Me.DeleteButton.Text = "إلغاء F3"
        Me.DeleteButton.UseVisualStyleBackColor = False
        '
        'Fields_Panel
        '
        Me.Fields_Panel.BackColor = System.Drawing.Color.Transparent
        Me.Fields_Panel.Controls.Add(Me.CR_Phone_Txt)
        Me.Fields_Panel.Controls.Add(Me.Treasury_Balance)
        Me.Fields_Panel.Controls.Add(Me.AG_Cm)
        Me.Fields_Panel.Controls.Add(Me.Receipt_Title_combobox)
        Me.Fields_Panel.Controls.Add(Me.Label11)
        Me.Fields_Panel.Controls.Add(Me.Barcode_SH_txt)
        Me.Fields_Panel.Controls.Add(Me.BankPanel)
        Me.Fields_Panel.Controls.Add(Me.ReceiptTypeComboBox)
        Me.Fields_Panel.Controls.Add(Me.Label8)
        Me.Fields_Panel.Controls.Add(Me.Label13)
        Me.Fields_Panel.Controls.Add(Me.payment_Type_combo)
        Me.Fields_Panel.Controls.Add(Me.Label19)
        Me.Fields_Panel.Controls.Add(Me.Label18)
        Me.Fields_Panel.Controls.Add(Me.Treasury_ComboBox)
        Me.Fields_Panel.Controls.Add(Me.Label3)
        Me.Fields_Panel.Controls.Add(Me.Label2)
        Me.Fields_Panel.Controls.Add(Me.Current_QTY)
        Me.Fields_Panel.Controls.Add(Me.Label12)
        Me.Fields_Panel.Controls.Add(Me.DateTimeReceipt)
        Me.Fields_Panel.Controls.Add(Me.Label1)
        Me.Fields_Panel.Controls.Add(Me.Label4)
        Me.Fields_Panel.Controls.Add(Me.money_num_txtb)
        Me.Fields_Panel.Controls.Add(Me.Notes_txtb)
        Me.Fields_Panel.Controls.Add(Me.Label6)
        Me.Fields_Panel.Controls.Add(Me.Label9)
        Me.Fields_Panel.Controls.Add(Me.money_char_txtb)
        Me.Fields_Panel.Enabled = False
        Me.Fields_Panel.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fields_Panel.Location = New System.Drawing.Point(4, 87)
        Me.Fields_Panel.Name = "Fields_Panel"
        Me.Fields_Panel.Size = New System.Drawing.Size(697, 367)
        Me.Fields_Panel.TabIndex = 282
        '
        'CR_Phone_Txt
        '
        Me.CR_Phone_Txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CR_Phone_Txt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.CR_Phone_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CR_Phone_Txt.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CR_Phone_Txt.ForeColor = System.Drawing.Color.Blue
        Me.CR_Phone_Txt.Location = New System.Drawing.Point(141, 49)
        Me.CR_Phone_Txt.Name = "CR_Phone_Txt"
        Me.CR_Phone_Txt.ReadOnly = True
        Me.CR_Phone_Txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CR_Phone_Txt.Size = New System.Drawing.Size(357, 23)
        Me.CR_Phone_Txt.TabIndex = 661
        '
        'Treasury_Balance
        '
        Me.Treasury_Balance.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Treasury_Balance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Treasury_Balance.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Treasury_Balance.Font = New System.Drawing.Font("Stencil", 10.25!)
        Me.Treasury_Balance.ForeColor = System.Drawing.Color.Black
        Me.Treasury_Balance.Location = New System.Drawing.Point(6, 226)
        Me.Treasury_Balance.Name = "Treasury_Balance"
        Me.Treasury_Balance.ReadOnly = True
        Me.Treasury_Balance.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Treasury_Balance.Size = New System.Drawing.Size(166, 24)
        Me.Treasury_Balance.TabIndex = 660
        Me.Treasury_Balance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NULLContextMenuStrip
        '
        Me.NULLContextMenuStrip.Name = "NULLContextMenuStrip"
        Me.NULLContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NULLContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'Receipt_Title_combobox
        '
        Me.Receipt_Title_combobox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Receipt_Title_combobox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Receipt_Title_combobox.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.Receipt_Title_combobox.ForeColor = System.Drawing.Color.Navy
        Me.Receipt_Title_combobox.Location = New System.Drawing.Point(3, 157)
        Me.Receipt_Title_combobox.Name = "Receipt_Title_combobox"
        Me.Receipt_Title_combobox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Receipt_Title_combobox.Size = New System.Drawing.Size(594, 31)
        Me.Receipt_Title_combobox.TabIndex = 3
        Me.Receipt_Title_combobox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(615, 50)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label11.Size = New System.Drawing.Size(77, 20)
        Me.Label11.TabIndex = 617
        Me.Label11.Text = "رقم العميل"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Barcode_SH_txt
        '
        Me.Barcode_SH_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Barcode_SH_txt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Barcode_SH_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_SH_txt.Location = New System.Drawing.Point(504, 49)
        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
        Me.Barcode_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Barcode_SH_txt.Size = New System.Drawing.Size(107, 23)
        Me.Barcode_SH_txt.TabIndex = 616
        Me.Barcode_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BankPanel
        '
        Me.BankPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BankPanel.Controls.Add(Me.Label5)
        Me.BankPanel.Controls.Add(Me.bankName_Combo)
        Me.BankPanel.Controls.Add(Me.CheckNum_txtb)
        Me.BankPanel.Controls.Add(Me.Label7)
        Me.BankPanel.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BankPanel.Location = New System.Drawing.Point(5, 264)
        Me.BankPanel.Name = "BankPanel"
        Me.BankPanel.Size = New System.Drawing.Size(621, 35)
        Me.BankPanel.TabIndex = 376
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label5.Location = New System.Drawing.Point(552, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 19)
        Me.Label5.TabIndex = 273
        Me.Label5.Text = " المصرف"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'bankName_Combo
        '
        Me.bankName_Combo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bankName_Combo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.bankName_Combo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.bankName_Combo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.bankName_Combo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bankName_Combo.DropDownHeight = 150
        Me.bankName_Combo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bankName_Combo.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.bankName_Combo.ForeColor = System.Drawing.Color.Black
        Me.bankName_Combo.FormattingEnabled = True
        Me.bankName_Combo.IntegralHeight = False
        Me.bankName_Combo.Location = New System.Drawing.Point(295, 3)
        Me.bankName_Combo.Name = "bankName_Combo"
        Me.bankName_Combo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.bankName_Combo.Size = New System.Drawing.Size(250, 27)
        Me.bankName_Combo.TabIndex = 7
        '
        'CheckNum_txtb
        '
        Me.CheckNum_txtb.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckNum_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CheckNum_txtb.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckNum_txtb.Location = New System.Drawing.Point(3, 3)
        Me.CheckNum_txtb.MaxLength = 100
        Me.CheckNum_txtb.Name = "CheckNum_txtb"
        Me.CheckNum_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckNum_txtb.Size = New System.Drawing.Size(163, 26)
        Me.CheckNum_txtb.TabIndex = 20
        Me.CheckNum_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label7.Location = New System.Drawing.Point(171, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 19)
        Me.Label7.TabIndex = 277
        Me.Label7.Text = "رقم الشيك"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ReceiptTypeComboBox
        '
        Me.ReceiptTypeComboBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ReceiptTypeComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ReceiptTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ReceiptTypeComboBox.Enabled = False
        Me.ReceiptTypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ReceiptTypeComboBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReceiptTypeComboBox.ForeColor = System.Drawing.Color.Black
        Me.ReceiptTypeComboBox.FormattingEnabled = True
        Me.ReceiptTypeComboBox.Location = New System.Drawing.Point(383, 4)
        Me.ReceiptTypeComboBox.Name = "ReceiptTypeComboBox"
        Me.ReceiptTypeComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ReceiptTypeComboBox.Size = New System.Drawing.Size(228, 33)
        Me.ReceiptTypeComboBox.TabIndex = 374
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label8.Location = New System.Drawing.Point(613, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 19)
        Me.Label8.TabIndex = 373
        Me.Label8.Text = "نوع الإيصال"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label13.Location = New System.Drawing.Point(291, 9)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(84, 19)
        Me.Label13.TabIndex = 270
        Me.Label13.Text = "طريـقة الدفع"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'payment_Type_combo
        '
        Me.payment_Type_combo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.payment_Type_combo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.payment_Type_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.payment_Type_combo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.payment_Type_combo.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold)
        Me.payment_Type_combo.ForeColor = System.Drawing.Color.Black
        Me.payment_Type_combo.FormattingEnabled = True
        Me.payment_Type_combo.Items.AddRange(New Object() {"نقـــــــداً", "شيـــــــك"})
        Me.payment_Type_combo.Location = New System.Drawing.Point(3, 4)
        Me.payment_Type_combo.Name = "payment_Type_combo"
        Me.payment_Type_combo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.payment_Type_combo.Size = New System.Drawing.Size(284, 33)
        Me.payment_Type_combo.TabIndex = 6
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label19.Location = New System.Drawing.Point(176, 230)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(85, 19)
        Me.Label19.TabIndex = 372
        Me.Label19.Text = "رصيد الخزينة"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label18.Location = New System.Drawing.Point(632, 230)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(50, 19)
        Me.Label18.TabIndex = 324
        Me.Label18.Text = "الخزينة"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Treasury_ComboBox
        '
        Me.Treasury_ComboBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Treasury_ComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Treasury_ComboBox.DropDownHeight = 200
        Me.Treasury_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Treasury_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Treasury_ComboBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Treasury_ComboBox.ForeColor = System.Drawing.Color.Black
        Me.Treasury_ComboBox.FormattingEnabled = True
        Me.Treasury_ComboBox.IntegralHeight = False
        Me.Treasury_ComboBox.Location = New System.Drawing.Point(300, 224)
        Me.Treasury_ComboBox.Name = "Treasury_ComboBox"
        Me.Treasury_ComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Treasury_ComboBox.Size = New System.Drawing.Size(297, 33)
        Me.Treasury_ComboBox.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label3.Location = New System.Drawing.Point(626, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 19)
        Me.Label3.TabIndex = 271
        Me.Label3.Text = "التاريـخ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label2.Location = New System.Drawing.Point(603, 161)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 19)
        Me.Label2.TabIndex = 269
        Me.Label2.Text = "عنوان الإيصال"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Current_QTY
        '
        Me.Current_QTY.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Current_QTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Current_QTY.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Current_QTY.Font = New System.Drawing.Font("Stencil", 13.75!)
        Me.Current_QTY.ForeColor = System.Drawing.Color.Black
        Me.Current_QTY.Location = New System.Drawing.Point(141, 116)
        Me.Current_QTY.Name = "Current_QTY"
        Me.Current_QTY.Size = New System.Drawing.Size(165, 30)
        Me.Current_QTY.TabIndex = 249
        Me.Current_QTY.Text = "0"
        Me.Current_QTY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.Current_QTY, "رصيد العميل")
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label12.Location = New System.Drawing.Point(317, 121)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(49, 19)
        Me.Label12.TabIndex = 248
        Me.Label12.Text = "الرصيد"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DateTimeReceipt
        '
        Me.DateTimeReceipt.CalendarFont = New System.Drawing.Font("Segoe UI", 15.25!)
        Me.DateTimeReceipt.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.DateTimeReceipt.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimeReceipt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimeReceipt.Location = New System.Drawing.Point(414, 116)
        Me.DateTimeReceipt.Name = "DateTimeReceipt"
        Me.DateTimeReceipt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTimeReceipt.RightToLeftLayout = True
        Me.DateTimeReceipt.Size = New System.Drawing.Size(197, 27)
        Me.DateTimeReceipt.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label1.Location = New System.Drawing.Point(614, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 19)
        Me.Label1.TabIndex = 231
        Me.Label1.Text = "اسم العميـل"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label4.Location = New System.Drawing.Point(602, 195)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 19)
        Me.Label4.TabIndex = 236
        Me.Label4.Text = "المبلغ بالأرقام"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'money_num_txtb
        '
        Me.money_num_txtb.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.money_num_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.money_num_txtb.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.money_num_txtb.Font = New System.Drawing.Font("Stencil", 14.75!)
        Me.money_num_txtb.ForeColor = System.Drawing.Color.DarkGreen
        Me.money_num_txtb.Location = New System.Drawing.Point(300, 190)
        Me.money_num_txtb.Name = "money_num_txtb"
        Me.money_num_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.money_num_txtb.Size = New System.Drawing.Size(297, 31)
        Me.money_num_txtb.TabIndex = 4
        Me.money_num_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Notes_txtb
        '
        Me.Notes_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Notes_txtb.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.5!)
        Me.Notes_txtb.Location = New System.Drawing.Point(5, 305)
        Me.Notes_txtb.Name = "Notes_txtb"
        Me.Notes_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Notes_txtb.Size = New System.Drawing.Size(621, 23)
        Me.Notes_txtb.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label6.Location = New System.Drawing.Point(629, 338)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 19)
        Me.Label6.TabIndex = 240
        Me.Label6.Text = "بالحروف"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Label9.Location = New System.Drawing.Point(631, 308)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label9.Size = New System.Drawing.Size(53, 19)
        Me.Label9.TabIndex = 243
        Me.Label9.Text = "ملاحظة"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'money_char_txtb
        '
        Me.money_char_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.money_char_txtb.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.money_char_txtb.ForeColor = System.Drawing.Color.SeaGreen
        Me.money_char_txtb.Location = New System.Drawing.Point(5, 335)
        Me.money_char_txtb.Name = "money_char_txtb"
        Me.money_char_txtb.ReadOnly = True
        Me.money_char_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.money_char_txtb.Size = New System.Drawing.Size(621, 23)
        Me.money_char_txtb.TabIndex = 241
        Me.money_char_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Back_Btn
        '
        Me.Back_Btn.BackColor = System.Drawing.Color.IndianRed
        Me.Back_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Back_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_Btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.Back_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Back_Btn.Location = New System.Drawing.Point(10, 483)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Back_Btn.Size = New System.Drawing.Size(109, 37)
        Me.Back_Btn.TabIndex = 377
        Me.Back_Btn.TabStop = False
        Me.Back_Btn.Tag = "DELETE"
        Me.Back_Btn.Text = "رجوع Esc"
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'new_butt
        '
        Me.new_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.new_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.new_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.new_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.new_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.new_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.new_butt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.new_butt.ForeColor = System.Drawing.Color.Black
        Me.new_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.new_butt.Location = New System.Drawing.Point(559, 483)
        Me.new_butt.Name = "new_butt"
        Me.new_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.new_butt.Size = New System.Drawing.Size(118, 38)
        Me.new_butt.TabIndex = 280
        Me.new_butt.Tag = "GENERAL"
        Me.new_butt.Text = " جديد F1"
        Me.new_butt.UseVisualStyleBackColor = False
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
        Me.print_butt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.print_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.print_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.print_butt.Location = New System.Drawing.Point(124, 483)
        Me.print_butt.Name = "print_butt"
        Me.print_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.print_butt.Size = New System.Drawing.Size(85, 38)
        Me.print_butt.TabIndex = 281
        Me.print_butt.TabStop = False
        Me.print_butt.Tag = "PRINT"
        Me.print_butt.Text = "طباعة"
        Me.print_butt.UseVisualStyleBackColor = False
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
        Me.save_butt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.save_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.save_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.save_butt.Location = New System.Drawing.Point(412, 483)
        Me.save_butt.Name = "save_butt"
        Me.save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.save_butt.Size = New System.Drawing.Size(143, 38)
        Me.save_butt.TabIndex = 279
        Me.save_butt.TabStop = False
        Me.save_butt.Tag = "SAVE"
        Me.save_butt.Text = "حفظ F12"
        Me.save_butt.UseVisualStyleBackColor = False
        '
        'AG_Cm
        '
        Me.AG_Cm.CancelSearchImage = CType(resources.GetObject("AG_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.AG_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 13.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Cm.Location = New System.Drawing.Point(142, 78)
        Me.AG_Cm.Name = "AG_Cm"
        Me.AG_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Cm.Size = New System.Drawing.Size(469, 33)
        Me.AG_Cm.SQL_Column = "AG_NAME"
        Me.AG_Cm.SQL_ID = "AG_ID"
        Me.AG_Cm.SQL_IsNumericSearchField = False
        Me.AG_Cm.SQL_ListSize = 200
        Me.AG_Cm.SQL_NumberOfRows = 200
        Me.AG_Cm.SQL_OrderByField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField_WHERE = ""
        Me.AG_Cm.SQL_Table = "AGENTS_MENU_V"
        Me.AG_Cm.TabIndex = 1
        Me.AG_Cm.TextMaxLength = 250
        Me.AG_Cm.Textt = ""
        '
        'Receipt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 526)
        Me.ControlBox = False
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.AG_Show_Balance_CB)
        Me.Controls.Add(Me.Show_Bill_CB)
        Me.Controls.Add(Me.Title_Lb)
        Me.Controls.Add(Me.Edit_butt)
        Me.Controls.Add(Me.Rct_Move_Panel)
        Me.Controls.Add(Me.save_butt)
        Me.Controls.Add(Me.print_butt)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.new_butt)
        Me.Controls.Add(Me.Fields_Panel)
        Me.Controls.Add(Me.Back_Btn)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.Name = "Receipt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "شاشة الإيصالات"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.Rct_Move_Panel.ResumeLayout(False)
        Me.Rct_Move_Panel.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Fields_Panel.ResumeLayout(False)
        Me.Fields_Panel.PerformLayout()
        Me.BankPanel.ResumeLayout(False)
        Me.BankPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents Fields_Panel As System.Windows.Forms.Panel
    Friend WithEvents BankPanel As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents bankName_Combo As System.Windows.Forms.ComboBox
    Friend WithEvents CheckNum_txtb As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ReceiptTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents payment_Type_combo As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Treasury_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Current_QTY As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DateTimeReceipt As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents money_num_txtb As System.Windows.Forms.TextBox
    Friend WithEvents Notes_txtb As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents money_char_txtb As System.Windows.Forms.TextBox
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents new_butt As System.Windows.Forms.Button
    Friend WithEvents print_butt As System.Windows.Forms.Button
    Friend WithEvents save_butt As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Void_Lb As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Barcode_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents Receipt_Title_combobox As System.Windows.Forms.TextBox
    Friend WithEvents Up_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Down_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Rct_Move_Panel As System.Windows.Forms.Panel
    Friend WithEvents Edit_butt As System.Windows.Forms.Button
    Friend WithEvents Title_Lb As System.Windows.Forms.Label
    Friend WithEvents ReceiptNum_Txt As System.Windows.Forms.TextBox
    Friend WithEvents AG_Show_Balance_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Show_Bill_CB As System.Windows.Forms.CheckBox
    Friend WithEvents NULLContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents إضافةالإيصالإلىقائمةالعهدToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AG_Cm As resturant.FSearch_Filter
    Friend WithEvents Treasury_Balance As TextBox
    Friend WithEvents CR_Phone_Txt As TextBox
    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
End Class