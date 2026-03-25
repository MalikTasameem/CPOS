<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPaymentDefaultAccount
    Inherits System.Windows.Forms.Form

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmbPaymentMethod = New System.Windows.Forms.ComboBox()
        Me.cmbAccount = New System.Windows.Forms.ComboBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.dgvLinks = New System.Windows.Forms.DataGridView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Show_AG_Projects_btn = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.isLockCB = New System.Windows.Forms.CheckBox()
        Me.IsActive_CB = New System.Windows.Forms.CheckBox()
        Me.Percent_Disc_txt = New resturant.F2FloatField()
        CType(Me.dgvLinks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbPaymentMethod
        '
        Me.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPaymentMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPaymentMethod.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPaymentMethod.Location = New System.Drawing.Point(332, 3)
        Me.cmbPaymentMethod.Name = "cmbPaymentMethod"
        Me.cmbPaymentMethod.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbPaymentMethod.Size = New System.Drawing.Size(250, 24)
        Me.cmbPaymentMethod.TabIndex = 0
        '
        'cmbAccount
        '
        Me.cmbAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbAccount.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAccount.Location = New System.Drawing.Point(332, 33)
        Me.cmbAccount.Name = "cmbAccount"
        Me.cmbAccount.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbAccount.Size = New System.Drawing.Size(250, 24)
        Me.cmbAccount.TabIndex = 1
        '
        'txtNotes
        '
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotes.Location = New System.Drawing.Point(2, 3)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(296, 65)
        Me.txtNotes.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(2, 69)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(169, 31)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "حفظ"
        '
        'btnNew
        '
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(172, 69)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(126, 31)
        Me.btnNew.TabIndex = 4
        Me.btnNew.Text = "جديد"
        '
        'btnDelete
        '
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(698, 94)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(96, 31)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "تعطيل"
        Me.btnDelete.Visible = False
        '
        'dgvLinks
        '
        Me.dgvLinks.AllowUserToAddRows = False
        Me.dgvLinks.AllowUserToDeleteRows = False
        Me.dgvLinks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvLinks.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 12.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvLinks.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvLinks.Location = New System.Drawing.Point(2, 128)
        Me.dgvLinks.Name = "dgvLinks"
        Me.dgvLinks.ReadOnly = True
        Me.dgvLinks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvLinks.RowTemplate.Height = 30
        Me.dgvLinks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLinks.Size = New System.Drawing.Size(792, 389)
        Me.dgvLinks.TabIndex = 6
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(587, 5)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label11.Size = New System.Drawing.Size(75, 17)
        Me.Label11.TabIndex = 618
        Me.Label11.Text = "طريقة الدفع"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(587, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(179, 17)
        Me.Label1.TabIndex = 619
        Me.Label1.Text = "حساب (خزينة/مصرف) المقابل"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Show_AG_Projects_btn
        '
        Me.Show_AG_Projects_btn.BackColor = System.Drawing.Color.White
        Me.Show_AG_Projects_btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.Show_AG_Projects_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_AG_Projects_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_AG_Projects_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_AG_Projects_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Show_AG_Projects_btn.Location = New System.Drawing.Point(306, 4)
        Me.Show_AG_Projects_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Show_AG_Projects_btn.Name = "Show_AG_Projects_btn"
        Me.Show_AG_Projects_btn.Size = New System.Drawing.Size(24, 23)
        Me.Show_AG_Projects_btn.TabIndex = 664
        Me.ToolTip1.SetToolTip(Me.Show_AG_Projects_btn, "إضافة نوع جديد")
        Me.Show_AG_Projects_btn.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(586, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(99, 17)
        Me.Label2.TabIndex = 666
        Me.Label2.Text = "عمولة الخصم%"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'isLockCB
        '
        Me.isLockCB.AutoSize = True
        Me.isLockCB.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.isLockCB.Location = New System.Drawing.Point(306, 101)
        Me.isLockCB.Name = "isLockCB"
        Me.isLockCB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.isLockCB.Size = New System.Drawing.Size(188, 21)
        Me.isLockCB.TabIndex = 667
        Me.isLockCB.Text = "منع تبديل الخزينة فالإيصالات"
        Me.isLockCB.UseVisualStyleBackColor = True
        '
        'IsActive_CB
        '
        Me.IsActive_CB.AutoSize = True
        Me.IsActive_CB.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.IsActive_CB.Location = New System.Drawing.Point(526, 101)
        Me.IsActive_CB.Name = "IsActive_CB"
        Me.IsActive_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IsActive_CB.Size = New System.Drawing.Size(56, 21)
        Me.IsActive_CB.TabIndex = 669
        Me.IsActive_CB.Text = "نشط"
        Me.IsActive_CB.UseVisualStyleBackColor = True
        '
        'Percent_Disc_txt
        '
        Me.Percent_Disc_txt.BackColor = System.Drawing.Color.Lavender
        Me.Percent_Disc_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Percent_Disc_txt.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Percent_Disc_txt.Location = New System.Drawing.Point(482, 66)
        Me.Percent_Disc_txt.MaxLength = 0
        Me.Percent_Disc_txt.Name = "Percent_Disc_txt"
        Me.Percent_Disc_txt.Size = New System.Drawing.Size(100, 24)
        Me.Percent_Disc_txt.TabIndex = 668
        '
        'frmPaymentDefaultAccount
        '
        Me.ClientSize = New System.Drawing.Size(795, 520)
        Me.Controls.Add(Me.IsActive_CB)
        Me.Controls.Add(Me.Percent_Disc_txt)
        Me.Controls.Add(Me.isLockCB)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Show_AG_Projects_btn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmbPaymentMethod)
        Me.Controls.Add(Me.cmbAccount)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.dgvLinks)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmPaymentDefaultAccount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ربط طرق الدفع بالحساب الافتراضي"
        CType(Me.dgvLinks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
End Class
