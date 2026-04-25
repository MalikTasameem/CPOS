<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Custody
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Custody))
        Me.NULLContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Notes_txt = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTime = New System.Windows.Forms.DateTimePicker()
        Me.C_Reciept_Num_txt = New System.Windows.Forms.TextBox()
        Me.BillsGrid = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IDX_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Receipt_Title_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bill_Num_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pure_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.About_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.money_num_txtb = New System.Windows.Forms.TextBox()
        Me.Total_Rest_txt = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Total_Rcpt_txt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.C_Title_txt = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Check_Reciept_btn = New System.Windows.Forms.Button()
        Me.C_Agent_cm = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Rct_Move_Panel = New System.Windows.Forms.Panel()
        Me.ReceiptNum_Txt = New System.Windows.Forms.TextBox()
        Me.Up_Bill_btn = New System.Windows.Forms.Button()
        Me.Down_Bill_btn = New System.Windows.Forms.Button()
        Me.Title_Lb = New System.Windows.Forms.Label()
        Me.Move_To_SB_btn = New System.Windows.Forms.Button()
        Me.Edit_butt = New System.Windows.Forms.Button()
        Me.New_butt = New System.Windows.Forms.Button()
        Me.Save_butt = New System.Windows.Forms.Button()
        Me.Delete_butt = New System.Windows.Forms.Button()
        Me.ADDCatButton = New System.Windows.Forms.Button()
        Me.RemoveCatButton = New System.Windows.Forms.Button()
        Me.Details_Date = New System.Windows.Forms.DateTimePicker()
        Me.Details_Bill_Num_txt = New System.Windows.Forms.TextBox()
        Me.Details_Name_cm = New System.Windows.Forms.ComboBox()
        Me.Details_Value_txt = New System.Windows.Forms.TextBox()
        Me.Details_Notes_txt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.money_char_txtb = New System.Windows.Forms.TextBox()
        Me.ExpandablePanel1 = New DevComponents.DotNetBar.ExpandablePanel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Print_btn = New System.Windows.Forms.Button()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.VoidLb = New System.Windows.Forms.Label()
        Me.HeaderCloseBtn = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        CType(Me.BillsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Rct_Move_Panel.SuspendLayout()
        Me.ExpandablePanel1.SuspendLayout()
        Me.TitleBar_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'NULLContextMenuStrip
        '
        Me.NULLContextMenuStrip.Name = "NULLContextMenuStrip"
        Me.NULLContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NULLContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'Notes_txt
        '
        Me.Notes_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Notes_txt.Enabled = False
        Me.Notes_txt.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Notes_txt.ForeColor = System.Drawing.Color.Black
        Me.Notes_txt.Location = New System.Drawing.Point(54, 176)
        Me.Notes_txt.MaxLength = 250
        Me.Notes_txt.Multiline = True
        Me.Notes_txt.Name = "Notes_txt"
        Me.Notes_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Notes_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Notes_txt.Size = New System.Drawing.Size(414, 45)
        Me.Notes_txt.TabIndex = 633
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(471, 180)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(49, 17)
        Me.Label14.TabIndex = 634
        Me.Label14.Text = "ملاحظة"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(844, 154)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 19)
        Me.Label4.TabIndex = 643
        Me.Label4.Text = "التاريـخ"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DateTime
        '
        Me.DateTime.CalendarFont = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTime.CustomFormat = "dd/MM/yyyy"
        Me.DateTime.Enabled = False
        Me.DateTime.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTime.Location = New System.Drawing.Point(571, 152)
        Me.DateTime.Name = "DateTime"
        Me.DateTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTime.RightToLeftLayout = True
        Me.DateTime.Size = New System.Drawing.Size(269, 33)
        Me.DateTime.TabIndex = 642
        '
        'C_Reciept_Num_txt
        '
        Me.C_Reciept_Num_txt.BackColor = System.Drawing.SystemColors.HighlightText
        Me.C_Reciept_Num_txt.Enabled = False
        Me.C_Reciept_Num_txt.Font = New System.Drawing.Font("Arial", 14.0!)
        Me.C_Reciept_Num_txt.ForeColor = System.Drawing.Color.Black
        Me.C_Reciept_Num_txt.Location = New System.Drawing.Point(688, 85)
        Me.C_Reciept_Num_txt.Name = "C_Reciept_Num_txt"
        Me.C_Reciept_Num_txt.Size = New System.Drawing.Size(152, 29)
        Me.C_Reciept_Num_txt.TabIndex = 694
        Me.C_Reciept_Num_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BillsGrid
        '
        Me.BillsGrid.AllowUserToAddRows = False
        Me.BillsGrid.AllowUserToDeleteRows = False
        Me.BillsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.BillsGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.BillsGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.BillsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.BillsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.IDX_CL, Me.Date_CL, Me.Receipt_Title_CL, Me.Bill_Num_CL, Me.Pure_CL, Me.About_CL})
        Me.BillsGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BillsGrid.Location = New System.Drawing.Point(1, 258)
        Me.BillsGrid.MultiSelect = False
        Me.BillsGrid.Name = "BillsGrid"
        Me.BillsGrid.ReadOnly = True
        Me.BillsGrid.RowHeadersVisible = False
        Me.BillsGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BillsGrid.RowTemplate.Height = 25
        Me.BillsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.BillsGrid.Size = New System.Drawing.Size(897, 344)
        Me.BillsGrid.TabIndex = 701
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'IDX_CL
        '
        Me.IDX_CL.DataPropertyName = "IDX"
        Me.IDX_CL.FillWeight = 15.0!
        Me.IDX_CL.HeaderText = "ت"
        Me.IDX_CL.Name = "IDX_CL"
        Me.IDX_CL.ReadOnly = True
        '
        'Date_CL
        '
        Me.Date_CL.DataPropertyName = "Date"
        Me.Date_CL.FillWeight = 110.3892!
        Me.Date_CL.HeaderText = "تاريخ"
        Me.Date_CL.Name = "Date_CL"
        Me.Date_CL.ReadOnly = True
        '
        'Receipt_Title_CL
        '
        Me.Receipt_Title_CL.DataPropertyName = "Receipt_Title"
        Me.Receipt_Title_CL.FillWeight = 110.3892!
        Me.Receipt_Title_CL.HeaderText = "البيان"
        Me.Receipt_Title_CL.Name = "Receipt_Title_CL"
        Me.Receipt_Title_CL.ReadOnly = True
        '
        'Bill_Num_CL
        '
        Me.Bill_Num_CL.DataPropertyName = "Bill_Num"
        Me.Bill_Num_CL.FillWeight = 110.3892!
        Me.Bill_Num_CL.HeaderText = "رقم الفاتورة"
        Me.Bill_Num_CL.Name = "Bill_Num_CL"
        Me.Bill_Num_CL.ReadOnly = True
        Me.Bill_Num_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Pure_CL
        '
        Me.Pure_CL.DataPropertyName = "Pure"
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Pure_CL.DefaultCellStyle = DataGridViewCellStyle1
        Me.Pure_CL.FillWeight = 120.2027!
        Me.Pure_CL.HeaderText = "القيمة"
        Me.Pure_CL.Name = "Pure_CL"
        Me.Pure_CL.ReadOnly = True
        '
        'About_CL
        '
        Me.About_CL.DataPropertyName = "About"
        Me.About_CL.HeaderText = "ملاحظة"
        Me.About_CL.Name = "About_CL"
        Me.About_CL.ReadOnly = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(472, 118)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 19)
        Me.Label6.TabIndex = 709
        Me.Label6.Text = "المبلغ"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'money_num_txtb
        '
        Me.money_num_txtb.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.money_num_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.money_num_txtb.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.money_num_txtb.Font = New System.Drawing.Font("Stencil", 14.75!)
        Me.money_num_txtb.ForeColor = System.Drawing.Color.DarkGreen
        Me.money_num_txtb.Location = New System.Drawing.Point(292, 115)
        Me.money_num_txtb.Name = "money_num_txtb"
        Me.money_num_txtb.ReadOnly = True
        Me.money_num_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.money_num_txtb.Size = New System.Drawing.Size(176, 31)
        Me.money_num_txtb.TabIndex = 710
        Me.money_num_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Total_Rest_txt
        '
        Me.Total_Rest_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Total_Rest_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_Rest_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Total_Rest_txt.Font = New System.Drawing.Font("Stencil", 15.0!)
        Me.Total_Rest_txt.ForeColor = System.Drawing.Color.Firebrick
        Me.Total_Rest_txt.Location = New System.Drawing.Point(2, 609)
        Me.Total_Rest_txt.MaxLength = 250
        Me.Total_Rest_txt.Name = "Total_Rest_txt"
        Me.Total_Rest_txt.ReadOnly = True
        Me.Total_Rest_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Total_Rest_txt.Size = New System.Drawing.Size(142, 31)
        Me.Total_Rest_txt.TabIndex = 712
        Me.Total_Rest_txt.Text = "000"
        Me.Total_Rest_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(145, 613)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 19)
        Me.Label8.TabIndex = 713
        Me.Label8.Text = "باقي من العهدة"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Total_Rcpt_txt
        '
        Me.Total_Rcpt_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Total_Rcpt_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_Rcpt_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Total_Rcpt_txt.Font = New System.Drawing.Font("Stencil", 15.0!)
        Me.Total_Rcpt_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Total_Rcpt_txt.Location = New System.Drawing.Point(270, 610)
        Me.Total_Rcpt_txt.MaxLength = 250
        Me.Total_Rcpt_txt.Name = "Total_Rcpt_txt"
        Me.Total_Rcpt_txt.ReadOnly = True
        Me.Total_Rcpt_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Total_Rcpt_txt.Size = New System.Drawing.Size(141, 31)
        Me.Total_Rcpt_txt.TabIndex = 714
        Me.Total_Rcpt_txt.Text = "000"
        Me.Total_Rcpt_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(414, 617)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(163, 19)
        Me.Label9.TabIndex = 715
        Me.Label9.Text = "إجمالي التسوية من العهدة"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'C_Title_txt
        '
        Me.C_Title_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.C_Title_txt.Enabled = False
        Me.C_Title_txt.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.C_Title_txt.ForeColor = System.Drawing.Color.Black
        Me.C_Title_txt.Location = New System.Drawing.Point(571, 191)
        Me.C_Title_txt.MaxLength = 250
        Me.C_Title_txt.Name = "C_Title_txt"
        Me.C_Title_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.C_Title_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.C_Title_txt.Size = New System.Drawing.Size(269, 27)
        Me.C_Title_txt.TabIndex = 717
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(844, 194)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 19)
        Me.Label10.TabIndex = 718
        Me.Label10.Text = "عنوان العهدة"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(843, 92)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 19)
        Me.Label11.TabIndex = 719
        Me.Label11.Text = "رقم الإيصال"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Check_Reciept_btn
        '
        Me.Check_Reciept_btn.BackColor = System.Drawing.Color.White
        Me.Check_Reciept_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Check_Reciept_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Check_Reciept_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Check_Reciept_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Check_Reciept_btn.Location = New System.Drawing.Point(647, 85)
        Me.Check_Reciept_btn.Name = "Check_Reciept_btn"
        Me.Check_Reciept_btn.Size = New System.Drawing.Size(35, 29)
        Me.Check_Reciept_btn.TabIndex = 720
        Me.Check_Reciept_btn.UseVisualStyleBackColor = False
        Me.Check_Reciept_btn.Visible = False
        '
        'C_Agent_cm
        '
        Me.C_Agent_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.C_Agent_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.C_Agent_cm.Enabled = False
        Me.C_Agent_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.C_Agent_cm.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.C_Agent_cm.FormattingEnabled = True
        Me.C_Agent_cm.Location = New System.Drawing.Point(571, 118)
        Me.C_Agent_cm.Name = "C_Agent_cm"
        Me.C_Agent_cm.Size = New System.Drawing.Size(269, 28)
        Me.C_Agent_cm.TabIndex = 721
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(845, 124)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 19)
        Me.Label1.TabIndex = 722
        Me.Label1.Text = "الحساب"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Rct_Move_Panel
        '
        Me.Rct_Move_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Rct_Move_Panel.Controls.Add(Me.ReceiptNum_Txt)
        Me.Rct_Move_Panel.Controls.Add(Me.Up_Bill_btn)
        Me.Rct_Move_Panel.Controls.Add(Me.Down_Bill_btn)
        Me.Rct_Move_Panel.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rct_Move_Panel.Location = New System.Drawing.Point(693, 42)
        Me.Rct_Move_Panel.Name = "Rct_Move_Panel"
        Me.Rct_Move_Panel.Size = New System.Drawing.Size(248, 38)
        Me.Rct_Move_Panel.TabIndex = 728
        '
        'ReceiptNum_Txt
        '
        Me.ReceiptNum_Txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ReceiptNum_Txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReceiptNum_Txt.ForeColor = System.Drawing.Color.Black
        Me.ReceiptNum_Txt.Location = New System.Drawing.Point(43, 3)
        Me.ReceiptNum_Txt.MaxLength = 100
        Me.ReceiptNum_Txt.Name = "ReceiptNum_Txt"
        Me.ReceiptNum_Txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ReceiptNum_Txt.Size = New System.Drawing.Size(164, 29)
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
        Me.Up_Bill_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Up_Bill_btn.Location = New System.Drawing.Point(211, 2)
        Me.Up_Bill_btn.Name = "Up_Bill_btn"
        Me.Up_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Up_Bill_btn.Size = New System.Drawing.Size(32, 32)
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
        Me.Down_Bill_btn.Font = New System.Drawing.Font("Arial Narrow", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Down_Bill_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Down_Bill_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Down_Bill_btn.Location = New System.Drawing.Point(3, 2)
        Me.Down_Bill_btn.Name = "Down_Bill_btn"
        Me.Down_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Down_Bill_btn.Size = New System.Drawing.Size(33, 32)
        Me.Down_Bill_btn.TabIndex = 626
        Me.Down_Bill_btn.TabStop = False
        Me.Down_Bill_btn.Text = "▼"
        Me.Down_Bill_btn.UseVisualStyleBackColor = False
        '
        'Title_Lb
        '
        Me.Title_Lb.BackColor = System.Drawing.SystemColors.Control
        Me.Title_Lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Title_Lb.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title_Lb.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Title_Lb.Location = New System.Drawing.Point(1, 42)
        Me.Title_Lb.Name = "Title_Lb"
        Me.Title_Lb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Title_Lb.Size = New System.Drawing.Size(691, 38)
        Me.Title_Lb.TabIndex = 729
        Me.Title_Lb.Text = "شاشة العهد"
        Me.Title_Lb.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Move_To_SB_btn
        '
        Me.Move_To_SB_btn.BackColor = System.Drawing.Color.White
        Me.Move_To_SB_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Move_To_SB_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Move_To_SB_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Move_To_SB_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Move_To_SB_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Move_To_SB_btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Move_To_SB_btn.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Move_To_SB_btn.Location = New System.Drawing.Point(2, 652)
        Me.Move_To_SB_btn.Name = "Move_To_SB_btn"
        Me.Move_To_SB_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Move_To_SB_btn.Size = New System.Drawing.Size(188, 40)
        Me.Move_To_SB_btn.TabIndex = 727
        Me.Move_To_SB_btn.Text = "إقفال"
        Me.Move_To_SB_btn.UseVisualStyleBackColor = False
        '
        'Edit_butt
        '
        Me.Edit_butt.BackColor = System.Drawing.Color.White
        Me.Edit_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Edit_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Edit_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Edit_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Edit_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Edit_butt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Edit_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Edit_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Edit_butt.Location = New System.Drawing.Point(475, 652)
        Me.Edit_butt.Name = "Edit_butt"
        Me.Edit_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Edit_butt.Size = New System.Drawing.Size(113, 40)
        Me.Edit_butt.TabIndex = 726
        Me.Edit_butt.TabStop = False
        Me.Edit_butt.Text = "تعديل"
        Me.Edit_butt.UseVisualStyleBackColor = False
        Me.Edit_butt.Visible = False
        '
        'New_butt
        '
        Me.New_butt.BackColor = System.Drawing.Color.White
        Me.New_butt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.New_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.New_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.New_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.New_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.New_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.New_butt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.New_butt.ForeColor = System.Drawing.Color.Black
        Me.New_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.New_butt.Location = New System.Drawing.Point(784, 652)
        Me.New_butt.Name = "New_butt"
        Me.New_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.New_butt.Size = New System.Drawing.Size(113, 40)
        Me.New_butt.TabIndex = 724
        Me.New_butt.Text = "جديد"
        Me.New_butt.UseVisualStyleBackColor = False
        Me.New_butt.Visible = False
        '
        'Save_butt
        '
        Me.Save_butt.BackColor = System.Drawing.Color.White
        Me.Save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Save_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Save_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Save_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_butt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Save_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Save_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Save_butt.Location = New System.Drawing.Point(591, 652)
        Me.Save_butt.Name = "Save_butt"
        Me.Save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Save_butt.Size = New System.Drawing.Size(190, 40)
        Me.Save_butt.TabIndex = 723
        Me.Save_butt.TabStop = False
        Me.Save_butt.Tag = "SAVE"
        Me.Save_butt.Text = "حفظ"
        Me.Save_butt.UseVisualStyleBackColor = False
        Me.Save_butt.Visible = False
        '
        'Delete_butt
        '
        Me.Delete_butt.BackColor = System.Drawing.Color.White
        Me.Delete_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Delete_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Delete_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Delete_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Delete_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Delete_butt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Delete_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Delete_butt.Location = New System.Drawing.Point(361, 652)
        Me.Delete_butt.Name = "Delete_butt"
        Me.Delete_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Delete_butt.Size = New System.Drawing.Size(113, 40)
        Me.Delete_butt.TabIndex = 725
        Me.Delete_butt.Tag = "DELETE"
        Me.Delete_butt.Text = "إلغاء"
        Me.Delete_butt.UseVisualStyleBackColor = False
        Me.Delete_butt.Visible = False
        '
        'ADDCatButton
        '
        Me.ADDCatButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ADDCatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ADDCatButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADDCatButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ADDCatButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ADDCatButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADDCatButton.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
        Me.ADDCatButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ADDCatButton.Location = New System.Drawing.Point(899, 258)
        Me.ADDCatButton.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ADDCatButton.Name = "ADDCatButton"
        Me.ADDCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ADDCatButton.Size = New System.Drawing.Size(48, 211)
        Me.ADDCatButton.TabIndex = 731
        Me.ADDCatButton.TabStop = False
        Me.ADDCatButton.Text = "➕"
        Me.ADDCatButton.UseVisualStyleBackColor = False
        '
        'RemoveCatButton
        '
        Me.RemoveCatButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.RemoveCatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.RemoveCatButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RemoveCatButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.RemoveCatButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.RemoveCatButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.RemoveCatButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.RemoveCatButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.RemoveCatButton.Location = New System.Drawing.Point(899, 472)
        Me.RemoveCatButton.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.RemoveCatButton.Name = "RemoveCatButton"
        Me.RemoveCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RemoveCatButton.Size = New System.Drawing.Size(48, 130)
        Me.RemoveCatButton.TabIndex = 730
        Me.RemoveCatButton.TabStop = False
        Me.RemoveCatButton.Text = "❌"
        Me.RemoveCatButton.UseVisualStyleBackColor = False
        '
        'Details_Date
        '
        Me.Details_Date.CalendarFont = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Details_Date.CalendarMonthBackground = System.Drawing.SystemColors.Info
        Me.Details_Date.CalendarTitleBackColor = System.Drawing.SystemColors.Info
        Me.Details_Date.CalendarTrailingForeColor = System.Drawing.SystemColors.Info
        Me.Details_Date.CustomFormat = "dd/MM/yyyy"
        Me.Details_Date.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Details_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Details_Date.Location = New System.Drawing.Point(708, 56)
        Me.Details_Date.Name = "Details_Date"
        Me.Details_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Details_Date.RightToLeftLayout = True
        Me.Details_Date.Size = New System.Drawing.Size(185, 33)
        Me.Details_Date.TabIndex = 732
        '
        'Details_Bill_Num_txt
        '
        Me.Details_Bill_Num_txt.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Details_Bill_Num_txt.Font = New System.Drawing.Font("Arial", 14.0!)
        Me.Details_Bill_Num_txt.ForeColor = System.Drawing.Color.Black
        Me.Details_Bill_Num_txt.Location = New System.Drawing.Point(349, 56)
        Me.Details_Bill_Num_txt.MaxLength = 250
        Me.Details_Bill_Num_txt.Name = "Details_Bill_Num_txt"
        Me.Details_Bill_Num_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Details_Bill_Num_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Details_Bill_Num_txt.Size = New System.Drawing.Size(169, 29)
        Me.Details_Bill_Num_txt.TabIndex = 733
        '
        'Details_Name_cm
        '
        Me.Details_Name_cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Details_Name_cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Details_Name_cm.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Details_Name_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Details_Name_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Details_Name_cm.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Details_Name_cm.FormattingEnabled = True
        Me.Details_Name_cm.Location = New System.Drawing.Point(522, 56)
        Me.Details_Name_cm.Name = "Details_Name_cm"
        Me.Details_Name_cm.Size = New System.Drawing.Size(182, 29)
        Me.Details_Name_cm.TabIndex = 734
        '
        'Details_Value_txt
        '
        Me.Details_Value_txt.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Details_Value_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Details_Value_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Details_Value_txt.Font = New System.Drawing.Font("Stencil", 13.5!)
        Me.Details_Value_txt.ForeColor = System.Drawing.Color.Black
        Me.Details_Value_txt.Location = New System.Drawing.Point(241, 56)
        Me.Details_Value_txt.MaxLength = 250
        Me.Details_Value_txt.Name = "Details_Value_txt"
        Me.Details_Value_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Details_Value_txt.Size = New System.Drawing.Size(106, 29)
        Me.Details_Value_txt.TabIndex = 735
        Me.Details_Value_txt.Text = "000"
        '
        'Details_Notes_txt
        '
        Me.Details_Notes_txt.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Details_Notes_txt.Font = New System.Drawing.Font("Arial", 14.0!)
        Me.Details_Notes_txt.ForeColor = System.Drawing.Color.Black
        Me.Details_Notes_txt.Location = New System.Drawing.Point(0, 56)
        Me.Details_Notes_txt.MaxLength = 250
        Me.Details_Notes_txt.Name = "Details_Notes_txt"
        Me.Details_Notes_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Details_Notes_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Details_Notes_txt.Size = New System.Drawing.Size(239, 29)
        Me.Details_Notes_txt.TabIndex = 736
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(470, 150)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 17)
        Me.Label2.TabIndex = 737
        Me.Label2.Text = "بالحروف"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'money_char_txtb
        '
        Me.money_char_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.money_char_txtb.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.money_char_txtb.ForeColor = System.Drawing.Color.SeaGreen
        Me.money_char_txtb.Location = New System.Drawing.Point(54, 149)
        Me.money_char_txtb.Name = "money_char_txtb"
        Me.money_char_txtb.ReadOnly = True
        Me.money_char_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.money_char_txtb.Size = New System.Drawing.Size(414, 23)
        Me.money_char_txtb.TabIndex = 738
        Me.money_char_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ExpandablePanel1
        '
        Me.ExpandablePanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel1.Controls.Add(Me.Label13)
        Me.ExpandablePanel1.Controls.Add(Me.Label12)
        Me.ExpandablePanel1.Controls.Add(Me.Label7)
        Me.ExpandablePanel1.Controls.Add(Me.Label5)
        Me.ExpandablePanel1.Controls.Add(Me.Label3)
        Me.ExpandablePanel1.Controls.Add(Me.Details_Notes_txt)
        Me.ExpandablePanel1.Controls.Add(Me.Details_Date)
        Me.ExpandablePanel1.Controls.Add(Me.Details_Bill_Num_txt)
        Me.ExpandablePanel1.Controls.Add(Me.Details_Name_cm)
        Me.ExpandablePanel1.Controls.Add(Me.Details_Value_txt)
        Me.ExpandablePanel1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExpandablePanel1.Expanded = False
        Me.ExpandablePanel1.ExpandedBounds = New System.Drawing.Rectangle(1, 205, 896, 90)
        Me.ExpandablePanel1.ExpandOnTitleClick = True
        Me.ExpandablePanel1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExpandablePanel1.Location = New System.Drawing.Point(1, 224)
        Me.ExpandablePanel1.Name = "ExpandablePanel1"
        Me.ExpandablePanel1.Size = New System.Drawing.Size(896, 30)
        Me.ExpandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel1.Style.GradientAngle = 90
        Me.ExpandablePanel1.TabIndex = 739
        Me.ExpandablePanel1.ThemeAware = True
        Me.ExpandablePanel1.TitleHeight = 30
        Me.ExpandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.TitleStyle.BackColor1.Color = System.Drawing.Color.White
        Me.ExpandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner
        Me.ExpandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel1.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel1.TitleText = "إضافة مصاريف للعهدة"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(181, 34)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(54, 19)
        Me.Label13.TabIndex = 741
        Me.Label13.Text = "ملاحظــة"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(304, 34)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(40, 19)
        Me.Label12.TabIndex = 740
        Me.Label12.Text = "القيمة"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(444, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 19)
        Me.Label7.TabIndex = 739
        Me.Label7.Text = "رقم الفاتورة"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(662, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 19)
        Me.Label5.TabIndex = 738
        Me.Label5.Text = "البيان"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(845, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 19)
        Me.Label3.TabIndex = 737
        Me.Label3.Text = "التاريخ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Print_btn
        '
        Me.Print_btn.BackColor = System.Drawing.Color.White
        Me.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Print_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_btn.Location = New System.Drawing.Point(192, 652)
        Me.Print_btn.Name = "Print_btn"
        Me.Print_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_btn.Size = New System.Drawing.Size(148, 40)
        Me.Print_btn.TabIndex = 740
        Me.Print_btn.TabStop = False
        Me.Print_btn.Tag = "PRINT"
        Me.Print_btn.Text = "طباعــة"
        Me.Print_btn.UseVisualStyleBackColor = False
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.VoidLb)
        Me.TitleBar_Panel.Controls.Add(Me.HeaderCloseBtn)
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(948, 40)
        Me.TitleBar_Panel.TabIndex = 1000
        Me.TitleBar_Panel.Tag = "HEADER"
        '
        'VoidLb
        '
        Me.VoidLb.BackColor = System.Drawing.Color.Red
        Me.VoidLb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VoidLb.Dock = System.Windows.Forms.DockStyle.Left
        Me.VoidLb.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VoidLb.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.VoidLb.Location = New System.Drawing.Point(53, 0)
        Me.VoidLb.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.VoidLb.Name = "VoidLb"
        Me.VoidLb.Size = New System.Drawing.Size(555, 40)
        Me.VoidLb.TabIndex = 671
        Me.VoidLb.Text = "حالــة ملغية"
        Me.VoidLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.VoidLb.Visible = False
        '
        'HeaderCloseBtn
        '
        Me.HeaderCloseBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HeaderCloseBtn.Dock = System.Windows.Forms.DockStyle.Left
        Me.HeaderCloseBtn.FlatAppearance.BorderSize = 0
        Me.HeaderCloseBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.HeaderCloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.HeaderCloseBtn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.HeaderCloseBtn.ForeColor = System.Drawing.Color.White
        Me.HeaderCloseBtn.Location = New System.Drawing.Point(0, 0)
        Me.HeaderCloseBtn.Name = "HeaderCloseBtn"
        Me.HeaderCloseBtn.Size = New System.Drawing.Size(53, 40)
        Me.HeaderCloseBtn.TabIndex = 3
        Me.HeaderCloseBtn.Text = "X"
        Me.HeaderCloseBtn.UseVisualStyleBackColor = False
        '
        'TopTitle_LB
        '
        Me.TopTitle_LB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TopTitle_LB.AutoSize = True
        Me.TopTitle_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TopTitle_LB.ForeColor = System.Drawing.Color.White
        Me.TopTitle_LB.Location = New System.Drawing.Point(892, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(44, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "العهد"
        '
        'Custody
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(948, 694)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Print_btn)
        Me.Controls.Add(Me.ExpandablePanel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.money_char_txtb)
        Me.Controls.Add(Me.ADDCatButton)
        Me.Controls.Add(Me.RemoveCatButton)
        Me.Controls.Add(Me.Title_Lb)
        Me.Controls.Add(Me.Rct_Move_Panel)
        Me.Controls.Add(Me.Move_To_SB_btn)
        Me.Controls.Add(Me.Edit_butt)
        Me.Controls.Add(Me.New_butt)
        Me.Controls.Add(Me.Save_butt)
        Me.Controls.Add(Me.Delete_butt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.C_Agent_cm)
        Me.Controls.Add(Me.Check_Reciept_btn)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.C_Title_txt)
        Me.Controls.Add(Me.DateTime)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Total_Rcpt_txt)
        Me.Controls.Add(Me.money_num_txtb)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Notes_txt)
        Me.Controls.Add(Me.Total_Rest_txt)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.BillsGrid)
        Me.Controls.Add(Me.C_Reciept_Num_txt)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Custody"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.BillsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Rct_Move_Panel.ResumeLayout(False)
        Me.Rct_Move_Panel.PerformLayout()
        Me.ExpandablePanel1.ResumeLayout(False)
        Me.ExpandablePanel1.PerformLayout()
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NULLContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents C_Reciept_Num_txt As System.Windows.Forms.TextBox
    Friend WithEvents BillsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents money_num_txtb As System.Windows.Forms.TextBox
    Friend WithEvents Total_Rest_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Total_Rcpt_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents C_Title_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents C_Agent_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Check_Reciept_btn As System.Windows.Forms.Button
    Friend WithEvents Move_To_SB_btn As System.Windows.Forms.Button
    Friend WithEvents Edit_butt As System.Windows.Forms.Button
    Friend WithEvents New_butt As System.Windows.Forms.Button
    Friend WithEvents Save_butt As System.Windows.Forms.Button
    Friend WithEvents Delete_butt As System.Windows.Forms.Button
    Friend WithEvents Rct_Move_Panel As System.Windows.Forms.Panel
    Friend WithEvents ReceiptNum_Txt As System.Windows.Forms.TextBox
    Friend WithEvents Up_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Down_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Title_Lb As System.Windows.Forms.Label
    Friend WithEvents ADDCatButton As System.Windows.Forms.Button
    Friend WithEvents RemoveCatButton As System.Windows.Forms.Button
    Friend WithEvents Details_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Details_Bill_Num_txt As System.Windows.Forms.TextBox
    Friend WithEvents Details_Name_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Details_Value_txt As System.Windows.Forms.TextBox
    Friend WithEvents Details_Notes_txt As System.Windows.Forms.TextBox

    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents money_char_txtb As System.Windows.Forms.TextBox
    Friend WithEvents ExpandablePanel1 As DevComponents.DotNetBar.ExpandablePanel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Print_btn As System.Windows.Forms.Button
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDX_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Receipt_Title_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bill_Num_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pure_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents About_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TitleBar_Panel As Panel
    Friend WithEvents VoidLb As Label
    Friend WithEvents HeaderCloseBtn As Button
    Friend WithEvents TopTitle_LB As Label
End Class
