<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class POS_Shortcut_Factory
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(POS_Shortcut_Factory))
        Me.NoneContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.IMPanel = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.KeyBoard_Btn = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.FormPanel = New System.Windows.Forms.Panel()
        Me.IMDataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.IM_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isValid_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Delete_IM_CB = New System.Windows.Forms.CheckBox()
        Me.Show_IM_btn = New System.Windows.Forms.Button()
        Me.IM_SH_txt = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DeleteSButton = New System.Windows.Forms.Button()
        Me.NewSButton = New System.Windows.Forms.Button()
        Me.EditSButton = New System.Windows.Forms.Button()
        Me.SaveSButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.S_listBox = New System.Windows.Forms.ListBox()
        Me.SNameTextBox = New System.Windows.Forms.TextBox()
        Me.ExitButton = New System.Windows.Forms.Button()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FormPanel.SuspendLayout()
        CType(Me.IMDataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NoneContextMenuStrip
        '
        Me.NoneContextMenuStrip.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoneContextMenuStrip.Name = "ContextMenuStrip1"
        Me.NoneContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'IMPanel
        '
        Me.IMPanel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.IMPanel.AutoScroll = True
        Me.IMPanel.BackColor = System.Drawing.SystemColors.Control
        Me.IMPanel.Cursor = System.Windows.Forms.Cursors.Default
        Me.IMPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.IMPanel.Location = New System.Drawing.Point(440, 0)
        Me.IMPanel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.IMPanel.Name = "IMPanel"
        Me.IMPanel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IMPanel.Size = New System.Drawing.Size(458, 389)
        Me.IMPanel.TabIndex = 546
        '
        'KeyBoard_Btn
        '
        Me.KeyBoard_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_Keyboard_100061
        Me.KeyBoard_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.KeyBoard_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.KeyBoard_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.KeyBoard_Btn.Location = New System.Drawing.Point(2, 531)
        Me.KeyBoard_Btn.Name = "KeyBoard_Btn"
        Me.KeyBoard_Btn.Size = New System.Drawing.Size(63, 47)
        Me.KeyBoard_Btn.TabIndex = 655
        Me.ToolTip1.SetToolTip(Me.KeyBoard_Btn, "شاشة لوحة المفاتيح")
        Me.KeyBoard_Btn.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        Me.ErrorProvider1.RightToLeft = True
        '
        'FormPanel
        '
        Me.FormPanel.BackColor = System.Drawing.SystemColors.Control
        Me.FormPanel.Controls.Add(Me.IMDataGridViewX)
        Me.FormPanel.Controls.Add(Me.KeyBoard_Btn)
        Me.FormPanel.Controls.Add(Me.Delete_IM_CB)
        Me.FormPanel.Controls.Add(Me.Show_IM_btn)
        Me.FormPanel.Controls.Add(Me.IM_SH_txt)
        Me.FormPanel.Controls.Add(Me.Label11)
        Me.FormPanel.Controls.Add(Me.Label1)
        Me.FormPanel.Controls.Add(Me.DeleteSButton)
        Me.FormPanel.Controls.Add(Me.NewSButton)
        Me.FormPanel.Controls.Add(Me.EditSButton)
        Me.FormPanel.Controls.Add(Me.SaveSButton)
        Me.FormPanel.Controls.Add(Me.GroupBox1)
        Me.FormPanel.Controls.Add(Me.SNameTextBox)
        Me.FormPanel.Controls.Add(Me.ExitButton)
        Me.FormPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FormPanel.Location = New System.Drawing.Point(0, 0)
        Me.FormPanel.Name = "FormPanel"
        Me.FormPanel.Size = New System.Drawing.Size(1016, 581)
        Me.FormPanel.TabIndex = 0
        '
        'IMDataGridViewX
        '
        Me.IMDataGridViewX.AllowUserToAddRows = False
        Me.IMDataGridViewX.AllowUserToDeleteRows = False
        Me.IMDataGridViewX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IMDataGridViewX.BackgroundColor = System.Drawing.Color.White
        Me.IMDataGridViewX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMDataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.IMDataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IMDataGridViewX.ColumnHeadersVisible = False
        Me.IMDataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_ID_CL, Me.item_name_CL, Me.isValid_CL})
        Me.IMDataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IMDataGridViewX.DefaultCellStyle = DataGridViewCellStyle4
        Me.IMDataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IMDataGridViewX.Location = New System.Drawing.Point(5, 310)
        Me.IMDataGridViewX.MultiSelect = False
        Me.IMDataGridViewX.Name = "IMDataGridViewX"
        Me.IMDataGridViewX.ReadOnly = True
        Me.IMDataGridViewX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMDataGridViewX.RowHeadersVisible = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.IMDataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.IMDataGridViewX.RowTemplate.Height = 40
        Me.IMDataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IMDataGridViewX.Size = New System.Drawing.Size(428, 2)
        Me.IMDataGridViewX.TabIndex = 607
        Me.IMDataGridViewX.Visible = False
        '
        'IM_ID_CL
        '
        Me.IM_ID_CL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.IM_ID_CL.DataPropertyName = "IM_ID"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_ID_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.IM_ID_CL.FillWeight = 5.0!
        Me.IM_ID_CL.Frozen = True
        Me.IM_ID_CL.HeaderText = "رقم الصنف"
        Me.IM_ID_CL.Name = "IM_ID_CL"
        Me.IM_ID_CL.ReadOnly = True
        Me.IM_ID_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IM_ID_CL.Visible = False
        Me.IM_ID_CL.Width = 5
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.item_name_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.item_name_CL.FillWeight = 69.61895!
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        '
        'isValid_CL
        '
        Me.isValid_CL.DataPropertyName = "isValid"
        Me.isValid_CL.HeaderText = "صلاحية"
        Me.isValid_CL.Name = "isValid_CL"
        Me.isValid_CL.ReadOnly = True
        Me.isValid_CL.Visible = False
        '
        'Delete_IM_CB
        '
        Me.Delete_IM_CB.AutoSize = True
        Me.Delete_IM_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Delete_IM_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Delete_IM_CB.Location = New System.Drawing.Point(272, 357)
        Me.Delete_IM_CB.Name = "Delete_IM_CB"
        Me.Delete_IM_CB.Size = New System.Drawing.Size(163, 29)
        Me.Delete_IM_CB.TabIndex = 654
        Me.Delete_IM_CB.Text = "وضع مسح صنف"
        Me.Delete_IM_CB.UseVisualStyleBackColor = True
        '
        'Show_IM_btn
        '
        Me.Show_IM_btn.BackColor = System.Drawing.Color.White
        Me.Show_IM_btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_download_173000
        Me.Show_IM_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_IM_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Show_IM_btn.Location = New System.Drawing.Point(6, 280)
        Me.Show_IM_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Show_IM_btn.Name = "Show_IM_btn"
        Me.Show_IM_btn.Size = New System.Drawing.Size(47, 29)
        Me.Show_IM_btn.TabIndex = 608
        Me.Show_IM_btn.UseVisualStyleBackColor = False
        '
        'IM_SH_txt
        '
        Me.IM_SH_txt.BackColor = System.Drawing.Color.LightGray
        Me.IM_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_SH_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_SH_txt.Location = New System.Drawing.Point(54, 280)
        Me.IM_SH_txt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.IM_SH_txt.Name = "IM_SH_txt"
        Me.IM_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_SH_txt.Size = New System.Drawing.Size(379, 29)
        Me.IM_SH_txt.TabIndex = 606
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(336, 256)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label11.Size = New System.Drawing.Size(94, 21)
        Me.Label11.TabIndex = 609
        Me.Label11.Text = "إسم الصنف :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(317, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 20)
        Me.Label1.TabIndex = 558
        Me.Label1.Text = "الإختصار"
        '
        'DeleteSButton
        '
        Me.DeleteSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteSButton.Enabled = False
        Me.DeleteSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DeleteSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DeleteSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DeleteSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteSButton.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.DeleteSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteSButton.Location = New System.Drawing.Point(314, 200)
        Me.DeleteSButton.Name = "DeleteSButton"
        Me.DeleteSButton.Size = New System.Drawing.Size(112, 40)
        Me.DeleteSButton.TabIndex = 557
        Me.DeleteSButton.Text = "حذف"
        Me.DeleteSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteSButton.UseVisualStyleBackColor = False
        '
        'NewSButton
        '
        Me.NewSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NewSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NewSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.NewSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.NewSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NewSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NewSButton.Image = CType(resources.GetObject("NewSButton.Image"), System.Drawing.Image)
        Me.NewSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NewSButton.Location = New System.Drawing.Point(314, 68)
        Me.NewSButton.Name = "NewSButton"
        Me.NewSButton.Size = New System.Drawing.Size(112, 40)
        Me.NewSButton.TabIndex = 556
        Me.NewSButton.Text = "جديد"
        Me.NewSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NewSButton.UseVisualStyleBackColor = False
        '
        'EditSButton
        '
        Me.EditSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EditSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditSButton.Enabled = False
        Me.EditSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.EditSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.EditSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EditSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.EditSButton.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.EditSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditSButton.Location = New System.Drawing.Point(314, 156)
        Me.EditSButton.Name = "EditSButton"
        Me.EditSButton.Size = New System.Drawing.Size(112, 40)
        Me.EditSButton.TabIndex = 555
        Me.EditSButton.Text = "تعديل"
        Me.EditSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EditSButton.UseVisualStyleBackColor = False
        '
        'SaveSButton
        '
        Me.SaveSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveSButton.Enabled = False
        Me.SaveSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SaveSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SaveSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SaveSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveSButton.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.SaveSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveSButton.Location = New System.Drawing.Point(314, 112)
        Me.SaveSButton.Name = "SaveSButton"
        Me.SaveSButton.Size = New System.Drawing.Size(112, 40)
        Me.SaveSButton.TabIndex = 554
        Me.SaveSButton.Text = "حفظ"
        Me.SaveSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveSButton.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.S_listBox)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 34)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(310, 222)
        Me.GroupBox1.TabIndex = 553
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "القائمة"
        '
        'S_listBox
        '
        Me.S_listBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.S_listBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.S_listBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.S_listBox.FormattingEnabled = True
        Me.S_listBox.ItemHeight = 21
        Me.S_listBox.Location = New System.Drawing.Point(3, 23)
        Me.S_listBox.Name = "S_listBox"
        Me.S_listBox.Size = New System.Drawing.Size(304, 196)
        Me.S_listBox.TabIndex = 430
        '
        'SNameTextBox
        '
        Me.SNameTextBox.Enabled = False
        Me.SNameTextBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SNameTextBox.Location = New System.Drawing.Point(6, 2)
        Me.SNameTextBox.MaxLength = 500
        Me.SNameTextBox.Name = "SNameTextBox"
        Me.SNameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SNameTextBox.Size = New System.Drawing.Size(307, 29)
        Me.SNameTextBox.TabIndex = 552
        '
        'ExitButton
        '
        Me.ExitButton.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ExitButton.BackColor = System.Drawing.SystemColors.Menu
        Me.ExitButton.BackgroundImage = Global.resturant.My.Resources.Resources.Users_Exit_icon
        Me.ExitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ExitButton.Location = New System.Drawing.Point(947, 531)
        Me.ExitButton.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(65, 47)
        Me.ExitButton.TabIndex = 551
        Me.ExitButton.UseVisualStyleBackColor = False
        '
        'POS_Shortcut_Factory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1016, 581)
        Me.Controls.Add(Me.IMPanel)
        Me.Controls.Add(Me.FormPanel)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "POS_Shortcut_Factory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "POS"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FormPanel.ResumeLayout(False)
        Me.FormPanel.PerformLayout()
        CType(Me.IMDataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents NoneContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents IMPanel As System.Windows.Forms.Panel
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents FormPanel As System.Windows.Forms.Panel
    Friend WithEvents ExitButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents S_listBox As System.Windows.Forms.ListBox
    Friend WithEvents SNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DeleteSButton As System.Windows.Forms.Button
    Friend WithEvents NewSButton As System.Windows.Forms.Button
    Friend WithEvents EditSButton As System.Windows.Forms.Button
    Friend WithEvents SaveSButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents IMDataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents IM_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents isValid_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Show_IM_btn As System.Windows.Forms.Button
    Friend WithEvents IM_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Delete_IM_CB As System.Windows.Forms.CheckBox
    Friend WithEvents KeyBoard_Btn As System.Windows.Forms.Button
End Class
