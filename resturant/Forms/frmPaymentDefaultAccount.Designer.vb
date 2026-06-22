<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPaymentDefaultAccount
    Inherits System.Windows.Forms.Form

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.Title_Label = New System.Windows.Forms.Label()
        Me.InputPanel = New System.Windows.Forms.Panel()
        Me.Notes_Label = New System.Windows.Forms.Label()
        Me.cmbPaymentMethod = New System.Windows.Forms.ComboBox()
        Me.cmbAccount = New System.Windows.Forms.ComboBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Show_AG_Projects_btn = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.isLockCB = New System.Windows.Forms.CheckBox()
        Me.IsActive_CB = New System.Windows.Forms.CheckBox()
        Me.Percent_Disc_txt = New resturant.F2FloatField()
        Me.dgvLinks = New System.Windows.Forms.DataGridView()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GridPanel = New System.Windows.Forms.Panel()
        Me.GridTitle_Label = New System.Windows.Forms.Label()
        Me.HeaderPanel.SuspendLayout()
        Me.InputPanel.SuspendLayout()
        CType(Me.dgvLinks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GridPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'HeaderPanel
        '
        Me.HeaderPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.HeaderPanel.Controls.Add(Me.Title_Label)
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(860, 58)
        Me.HeaderPanel.TabIndex = 670
        '
        'Title_Label
        '
        Me.Title_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Title_Label.AutoSize = True
        Me.Title_Label.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title_Label.ForeColor = System.Drawing.Color.White
        Me.Title_Label.Location = New System.Drawing.Point(565, 16)
        Me.Title_Label.Name = "Title_Label"
        Me.Title_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Title_Label.Size = New System.Drawing.Size(281, 25)
        Me.Title_Label.TabIndex = 0
        Me.Title_Label.Text = "ربط طرق الدفع بالحساب الافتراضي"
        '
        'InputPanel
        '
        Me.InputPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InputPanel.BackColor = System.Drawing.Color.White
        Me.InputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.InputPanel.Controls.Add(Me.Notes_Label)
        Me.InputPanel.Controls.Add(Me.cmbPaymentMethod)
        Me.InputPanel.Controls.Add(Me.cmbAccount)
        Me.InputPanel.Controls.Add(Me.txtNotes)
        Me.InputPanel.Controls.Add(Me.btnSave)
        Me.InputPanel.Controls.Add(Me.btnNew)
        Me.InputPanel.Controls.Add(Me.btnDelete)
        Me.InputPanel.Controls.Add(Me.Label11)
        Me.InputPanel.Controls.Add(Me.Label1)
        Me.InputPanel.Controls.Add(Me.Show_AG_Projects_btn)
        Me.InputPanel.Controls.Add(Me.Label2)
        Me.InputPanel.Controls.Add(Me.isLockCB)
        Me.InputPanel.Controls.Add(Me.IsActive_CB)
        Me.InputPanel.Controls.Add(Me.Percent_Disc_txt)
        Me.InputPanel.Location = New System.Drawing.Point(16, 72)
        Me.InputPanel.Name = "InputPanel"
        Me.InputPanel.Size = New System.Drawing.Size(828, 138)
        Me.InputPanel.TabIndex = 0
        '
        'Notes_Label
        '
        Me.Notes_Label.AutoSize = True
        Me.Notes_Label.BackColor = System.Drawing.Color.Transparent
        Me.Notes_Label.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Notes_Label.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.Notes_Label.Location = New System.Drawing.Point(259, 6)
        Me.Notes_Label.Name = "Notes_Label"
        Me.Notes_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Notes_Label.Size = New System.Drawing.Size(52, 15)
        Me.Notes_Label.TabIndex = 670
        Me.Notes_Label.Text = "ملاحظات"
        Me.Notes_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPaymentMethod
        '
        Me.cmbPaymentMethod.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPaymentMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPaymentMethod.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbPaymentMethod.Location = New System.Drawing.Point(396, 8)
        Me.cmbPaymentMethod.Name = "cmbPaymentMethod"
        Me.cmbPaymentMethod.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbPaymentMethod.Size = New System.Drawing.Size(254, 25)
        Me.cmbPaymentMethod.TabIndex = 0
        '
        'cmbAccount
        '
        Me.cmbAccount.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbAccount.Location = New System.Drawing.Point(396, 35)
        Me.cmbAccount.Name = "cmbAccount"
        Me.cmbAccount.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbAccount.Size = New System.Drawing.Size(254, 25)
        Me.cmbAccount.TabIndex = 1
        '
        'txtNotes
        '
        Me.txtNotes.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotes.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotes.Location = New System.Drawing.Point(6, 24)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtNotes.Size = New System.Drawing.Size(308, 70)
        Me.txtNotes.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(92, 100)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(140, 30)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "حفظ"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.btnNew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNew.FlatAppearance.BorderSize = 0
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.Color.White
        Me.btnNew.Location = New System.Drawing.Point(234, 100)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(80, 30)
        Me.btnNew.TabIndex = 4
        Me.btnNew.Text = "جديد"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.Location = New System.Drawing.Point(6, 100)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(84, 30)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "تعطيل"
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(654, 12)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label11.Size = New System.Drawing.Size(68, 15)
        Me.Label11.TabIndex = 618
        Me.Label11.Text = "طريقة الدفع"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(654, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(153, 15)
        Me.Label1.TabIndex = 619
        Me.Label1.Text = "حساب (خزينة/مصرف) المقابل"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Show_AG_Projects_btn
        '
        Me.Show_AG_Projects_btn.BackColor = System.Drawing.Color.White
        Me.Show_AG_Projects_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_AG_Projects_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_AG_Projects_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Show_AG_Projects_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_AG_Projects_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Show_AG_Projects_btn.Location = New System.Drawing.Point(364, 8)
        Me.Show_AG_Projects_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Show_AG_Projects_btn.Name = "Show_AG_Projects_btn"
        Me.Show_AG_Projects_btn.Size = New System.Drawing.Size(30, 25)
        Me.Show_AG_Projects_btn.TabIndex = 664
        Me.Show_AG_Projects_btn.Text = "➕"
        Me.ToolTip1.SetToolTip(Me.Show_AG_Projects_btn, "إضافة نوع جديد")
        Me.Show_AG_Projects_btn.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(656, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(83, 15)
        Me.Label2.TabIndex = 666
        Me.Label2.Text = "عمولة الخصم%"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'isLockCB
        '
        Me.isLockCB.AutoSize = True
        Me.isLockCB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isLockCB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.isLockCB.Location = New System.Drawing.Point(483, 90)
        Me.isLockCB.Name = "isLockCB"
        Me.isLockCB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.isLockCB.Size = New System.Drawing.Size(166, 19)
        Me.isLockCB.TabIndex = 667
        Me.isLockCB.Text = "منع تبديل الخزينة فالإيصالات"
        Me.isLockCB.UseVisualStyleBackColor = True
        '
        'IsActive_CB
        '
        Me.IsActive_CB.AutoSize = True
        Me.IsActive_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IsActive_CB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.IsActive_CB.Location = New System.Drawing.Point(398, 65)
        Me.IsActive_CB.Name = "IsActive_CB"
        Me.IsActive_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IsActive_CB.Size = New System.Drawing.Size(50, 19)
        Me.IsActive_CB.TabIndex = 669
        Me.IsActive_CB.Text = "نشط"
        Me.IsActive_CB.UseVisualStyleBackColor = True
        '
        'Percent_Disc_txt
        '
        Me.Percent_Disc_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.Percent_Disc_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Percent_Disc_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Percent_Disc_txt.Location = New System.Drawing.Point(592, 61)
        Me.Percent_Disc_txt.MaxLength = 0
        Me.Percent_Disc_txt.Name = "Percent_Disc_txt"
        Me.Percent_Disc_txt.Size = New System.Drawing.Size(60, 25)
        Me.Percent_Disc_txt.TabIndex = 668
        Me.Percent_Disc_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'dgvLinks
        '
        Me.dgvLinks.AllowUserToAddRows = False
        Me.dgvLinks.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(219, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(254, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.dgvLinks.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvLinks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvLinks.BackgroundColor = System.Drawing.Color.White
        Me.dgvLinks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvLinks.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvLinks.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(68, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(68, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvLinks.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvLinks.ColumnHeadersHeight = 34
        Me.dgvLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(219, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(254, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(39, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvLinks.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvLinks.EnableHeadersVisualStyles = False
        Me.dgvLinks.GridColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvLinks.Location = New System.Drawing.Point(12, 44)
        Me.dgvLinks.MultiSelect = False
        Me.dgvLinks.Name = "dgvLinks"
        Me.dgvLinks.ReadOnly = True
        Me.dgvLinks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvLinks.RowHeadersVisible = False
        Me.dgvLinks.RowTemplate.Height = 30
        Me.dgvLinks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLinks.Size = New System.Drawing.Size(804, 304)
        Me.dgvLinks.TabIndex = 6
        '
        'GridPanel
        '
        Me.GridPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridPanel.BackColor = System.Drawing.Color.White
        Me.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GridPanel.Controls.Add(Me.GridTitle_Label)
        Me.GridPanel.Controls.Add(Me.dgvLinks)
        Me.GridPanel.Location = New System.Drawing.Point(16, 224)
        Me.GridPanel.Name = "GridPanel"
        Me.GridPanel.Size = New System.Drawing.Size(828, 360)
        Me.GridPanel.TabIndex = 671
        '
        'GridTitle_Label
        '
        Me.GridTitle_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridTitle_Label.AutoSize = True
        Me.GridTitle_Label.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridTitle_Label.ForeColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.GridTitle_Label.Location = New System.Drawing.Point(655, 13)
        Me.GridTitle_Label.Name = "GridTitle_Label"
        Me.GridTitle_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GridTitle_Label.Size = New System.Drawing.Size(164, 19)
        Me.GridTitle_Label.TabIndex = 0
        Me.GridTitle_Label.Text = "الحسابات الافتراضية للدفع"
        Me.GridTitle_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmPaymentDefaultAccount
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(860, 600)
        Me.Controls.Add(Me.GridPanel)
        Me.Controls.Add(Me.InputPanel)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(876, 639)
        Me.Name = "frmPaymentDefaultAccount"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ربط طرق الدفع بالحساب الافتراضي"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        Me.InputPanel.ResumeLayout(False)
        Me.InputPanel.PerformLayout()
        CType(Me.dgvLinks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GridPanel.ResumeLayout(False)
        Me.GridPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmbPaymentMethod As ComboBox
    Friend WithEvents cmbAccount As ComboBox
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents dgvLinks As DataGridView
    Friend WithEvents Label11 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Show_AG_Projects_btn As Button
    Friend WithEvents ToolTip1 As ToolTip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents Label2 As Label
    Friend WithEvents isLockCB As CheckBox
    Friend WithEvents Percent_Disc_txt As F2FloatField
    Friend WithEvents IsActive_CB As CheckBox
    Friend WithEvents HeaderPanel As Panel
    Friend WithEvents Title_Label As Label
    Friend WithEvents InputPanel As Panel
    Friend WithEvents Notes_Label As Label
    Friend WithEvents GridPanel As Panel
    Friend WithEvents GridTitle_Label As Label
End Class
