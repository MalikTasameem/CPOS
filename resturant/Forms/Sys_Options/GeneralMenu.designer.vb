<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GeneralMenu
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GeneralMenu))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NewEmpButton = New System.Windows.Forms.Button()
        Me.EditEmpButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.SMPrinterComboBox = New System.Windows.Forms.ComboBox()
        Me.PrinterLabel = New System.Windows.Forms.Label()
        Me.SMDataGridViewX = New DataGridView()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Rank_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SMNamerrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.SMNameTextBox = New System.Windows.Forms.TextBox()
        Me.PrinterErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.IM_ViewerButton = New System.Windows.Forms.Button()
        Me.FKPanel = New System.Windows.Forms.Panel()
        Me.BKPanel = New System.Windows.Forms.Panel()
        Me.FKChoaseButton = New System.Windows.Forms.Button()
        Me.BKChoaseButton = New System.Windows.Forms.Button()
        Me.FKNoneColoreCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.BKNoneColoreCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.IM_CAT_SF = New resturant.FSearch_Filter()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Touch_Screen_Panel = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Rank_Num_txt = New System.Windows.Forms.TextBox()
        Me.is_Show_cb = New System.Windows.Forms.CheckBox()
        Me.SubPrint_Panel = New System.Windows.Forms.Panel()
        Me.DeleteU_btn = New System.Windows.Forms.Button()
        Me.InsertU_btn = New System.Windows.Forms.Button()
        Me.PRINTER_GRID = New System.Windows.Forms.DataGridView()
        Me.T_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PTR_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CP_Bill_Screen_GroupBox = New System.Windows.Forms.GroupBox()
        Me.Ksh_Screen_cmb = New System.Windows.Forms.ComboBox()
        Me.TitleBar_Panel.SuspendLayout()
        CType(Me.SMDataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SMNamerrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrinterErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Touch_Screen_Panel.SuspendLayout()
        Me.SubPrint_Panel.SuspendLayout()
        CType(Me.PRINTER_GRID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CP_Bill_Screen_GroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(759, 40)
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
        Me.ExitFormButton.Size = New System.Drawing.Size(48, 40)
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
        Me.TopTitle_LB.Location = New System.Drawing.Point(666, 10)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(77, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "التصنيفات"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(340, 8)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 21)
        Me.Label1.TabIndex = 181
        Me.Label1.Text = "إسم القائمة"
        '
        'NewEmpButton
        '
        Me.NewEmpButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NewEmpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NewEmpButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewEmpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NewEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewEmpButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NewEmpButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NewEmpButton.Location = New System.Drawing.Point(661, 581)
        Me.NewEmpButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.NewEmpButton.Name = "NewEmpButton"
        Me.NewEmpButton.Size = New System.Drawing.Size(94, 42)
        Me.NewEmpButton.TabIndex = 195
        Me.NewEmpButton.Text = "جديد"
        Me.NewEmpButton.UseVisualStyleBackColor = False
        '
        'EditEmpButton
        '
        Me.EditEmpButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditEmpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EditEmpButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditEmpButton.Enabled = False
        Me.EditEmpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EditEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditEmpButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.EditEmpButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditEmpButton.Location = New System.Drawing.Point(420, 581)
        Me.EditEmpButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EditEmpButton.Name = "EditEmpButton"
        Me.EditEmpButton.Size = New System.Drawing.Size(111, 42)
        Me.EditEmpButton.TabIndex = 194
        Me.EditEmpButton.Text = "تعديل"
        Me.EditEmpButton.UseVisualStyleBackColor = False
        '
        'SaveButton
        '
        Me.SaveButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveButton.Enabled = False
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SaveButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveButton.Location = New System.Drawing.Point(533, 581)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(125, 42)
        Me.SaveButton.TabIndex = 193
        Me.SaveButton.Tag = "SAVE"
        Me.SaveButton.Text = "حفظ"
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'SMPrinterComboBox
        '
        Me.SMPrinterComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SMPrinterComboBox.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.SMPrinterComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SMPrinterComboBox.DropDownHeight = 120
        Me.SMPrinterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SMPrinterComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SMPrinterComboBox.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.SMPrinterComboBox.FormattingEnabled = True
        Me.SMPrinterComboBox.IntegralHeight = False
        Me.SMPrinterComboBox.ItemHeight = 20
        Me.SMPrinterComboBox.Location = New System.Drawing.Point(84, 6)
        Me.SMPrinterComboBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SMPrinterComboBox.Name = "SMPrinterComboBox"
        Me.SMPrinterComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SMPrinterComboBox.Size = New System.Drawing.Size(238, 28)
        Me.SMPrinterComboBox.TabIndex = 205
        '
        'PrinterLabel
        '
        Me.PrinterLabel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PrinterLabel.AutoSize = True
        Me.PrinterLabel.BackColor = System.Drawing.Color.Transparent
        Me.PrinterLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrinterLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PrinterLabel.Location = New System.Drawing.Point(327, 10)
        Me.PrinterLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PrinterLabel.Name = "PrinterLabel"
        Me.PrinterLabel.Size = New System.Drawing.Size(96, 21)
        Me.PrinterLabel.TabIndex = 206
        Me.PrinterLabel.Text = "طابعة التجهيز"
        '
        'SMDataGridViewX
        '
        Me.SMDataGridViewX.AllowUserToAddRows = False
        Me.SMDataGridViewX.AllowUserToDeleteRows = False
        Me.SMDataGridViewX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.SMDataGridViewX.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.SMDataGridViewX.BackgroundColor = System.Drawing.Color.White
        Me.SMDataGridViewX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SMDataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.SMDataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.SMDataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.Rank_CL, Me.DataGridViewTextBoxColumn4})
        Me.SMDataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SMDataGridViewX.DefaultCellStyle = DataGridViewCellStyle2
        Me.SMDataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.SMDataGridViewX.Location = New System.Drawing.Point(2, 42)
        Me.SMDataGridViewX.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SMDataGridViewX.MultiSelect = False
        Me.SMDataGridViewX.Name = "SMDataGridViewX"
        Me.SMDataGridViewX.ReadOnly = True
        Me.SMDataGridViewX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SMDataGridViewX.RowHeadersVisible = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SMDataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.SMDataGridViewX.RowTemplate.Height = 35
        Me.SMDataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.SMDataGridViewX.Size = New System.Drawing.Size(315, 524)
        Me.SMDataGridViewX.TabIndex = 226
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "GM_ID"
        Me.DataGridViewTextBoxColumn3.FillWeight = 5.076142!
        Me.DataGridViewTextBoxColumn3.HeaderText = "الرقم"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Visible = False
        '
        'Rank_CL
        '
        Me.Rank_CL.DataPropertyName = "Rank_Num"
        Me.Rank_CL.FillWeight = 29.94151!
        Me.Rank_CL.HeaderText = "ت"
        Me.Rank_CL.Name = "Rank_CL"
        Me.Rank_CL.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "GM_Name"
        Me.DataGridViewTextBoxColumn4.FillWeight = 264.9824!
        Me.DataGridViewTextBoxColumn4.HeaderText = "القوائــــم"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'SMNamerrorProvider
        '
        Me.SMNamerrorProvider.ContainerControl = Me
        Me.SMNamerrorProvider.RightToLeft = True
        '
        'DeleteButton
        '
        Me.DeleteButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteButton.Enabled = False
        Me.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DeleteButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteButton.Location = New System.Drawing.Point(324, 581)
        Me.DeleteButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(94, 42)
        Me.DeleteButton.TabIndex = 399
        Me.DeleteButton.Tag = "DELETE"
        Me.DeleteButton.Text = "حذف"
        Me.DeleteButton.UseVisualStyleBackColor = False
        '
        'SMNameTextBox
        '
        Me.SMNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SMNameTextBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMNameTextBox.Location = New System.Drawing.Point(33, 3)
        Me.SMNameTextBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SMNameTextBox.MaxLength = 200
        Me.SMNameTextBox.Name = "SMNameTextBox"
        Me.SMNameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SMNameTextBox.Size = New System.Drawing.Size(291, 29)
        Me.SMNameTextBox.TabIndex = 402
        '
        'PrinterErrorProvider
        '
        Me.PrinterErrorProvider.ContainerControl = Me
        Me.PrinterErrorProvider.RightToLeft = True
        '
        'IM_ViewerButton
        '
        Me.IM_ViewerButton.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.IM_ViewerButton.BackColor = System.Drawing.SystemColors.Info
        Me.IM_ViewerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IM_ViewerButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_ViewerButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.IM_ViewerButton.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_ViewerButton.Location = New System.Drawing.Point(6, 4)
        Me.IM_ViewerButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.IM_ViewerButton.Name = "IM_ViewerButton"
        Me.IM_ViewerButton.Size = New System.Drawing.Size(124, 115)
        Me.IM_ViewerButton.TabIndex = 485
        Me.IM_ViewerButton.UseVisualStyleBackColor = False
        '
        'FKPanel
        '
        Me.FKPanel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.FKPanel.BackColor = System.Drawing.Color.Transparent
        Me.FKPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FKPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FKPanel.Location = New System.Drawing.Point(243, 63)
        Me.FKPanel.Name = "FKPanel"
        Me.FKPanel.Size = New System.Drawing.Size(89, 21)
        Me.FKPanel.TabIndex = 493
        '
        'BKPanel
        '
        Me.BKPanel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BKPanel.BackColor = System.Drawing.Color.Transparent
        Me.BKPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BKPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BKPanel.Location = New System.Drawing.Point(243, 36)
        Me.BKPanel.Name = "BKPanel"
        Me.BKPanel.Size = New System.Drawing.Size(89, 21)
        Me.BKPanel.TabIndex = 492
        '
        'FKChoaseButton
        '
        Me.FKChoaseButton.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.FKChoaseButton.BackColor = System.Drawing.Color.Linen
        Me.FKChoaseButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.FKChoaseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.FKChoaseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FKChoaseButton.Location = New System.Drawing.Point(204, 63)
        Me.FKChoaseButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.FKChoaseButton.Name = "FKChoaseButton"
        Me.FKChoaseButton.Size = New System.Drawing.Size(38, 21)
        Me.FKChoaseButton.TabIndex = 491
        Me.FKChoaseButton.Text = "....."
        Me.FKChoaseButton.UseVisualStyleBackColor = False
        '
        'BKChoaseButton
        '
        Me.BKChoaseButton.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BKChoaseButton.BackColor = System.Drawing.Color.Linen
        Me.BKChoaseButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BKChoaseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BKChoaseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BKChoaseButton.Location = New System.Drawing.Point(204, 36)
        Me.BKChoaseButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BKChoaseButton.Name = "BKChoaseButton"
        Me.BKChoaseButton.Size = New System.Drawing.Size(38, 21)
        Me.BKChoaseButton.TabIndex = 490
        Me.BKChoaseButton.Text = "....."
        Me.BKChoaseButton.UseVisualStyleBackColor = False
        '
        'FKNoneColoreCheckBox
        '
        Me.FKNoneColoreCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.FKNoneColoreCheckBox.AutoSize = True
        Me.FKNoneColoreCheckBox.BackColor = System.Drawing.Color.Transparent
        Me.FKNoneColoreCheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.FKNoneColoreCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.FKNoneColoreCheckBox.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FKNoneColoreCheckBox.ForeColor = System.Drawing.Color.Maroon
        Me.FKNoneColoreCheckBox.Location = New System.Drawing.Point(136, 62)
        Me.FKNoneColoreCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.FKNoneColoreCheckBox.Name = "FKNoneColoreCheckBox"
        Me.FKNoneColoreCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.FKNoneColoreCheckBox.Size = New System.Drawing.Size(65, 23)
        Me.FKNoneColoreCheckBox.TabIndex = 489
        Me.FKNoneColoreCheckBox.Text = "بلا لون"
        Me.FKNoneColoreCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.FKNoneColoreCheckBox.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label13.Location = New System.Drawing.Point(335, 64)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(85, 19)
        Me.Label13.TabIndex = 488
        Me.Label13.Text = "لون خط البيع"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BKNoneColoreCheckBox
        '
        Me.BKNoneColoreCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BKNoneColoreCheckBox.AutoSize = True
        Me.BKNoneColoreCheckBox.BackColor = System.Drawing.Color.Transparent
        Me.BKNoneColoreCheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BKNoneColoreCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BKNoneColoreCheckBox.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.BKNoneColoreCheckBox.ForeColor = System.Drawing.Color.Maroon
        Me.BKNoneColoreCheckBox.Location = New System.Drawing.Point(136, 34)
        Me.BKNoneColoreCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BKNoneColoreCheckBox.Name = "BKNoneColoreCheckBox"
        Me.BKNoneColoreCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BKNoneColoreCheckBox.Size = New System.Drawing.Size(65, 23)
        Me.BKNoneColoreCheckBox.TabIndex = 487
        Me.BKNoneColoreCheckBox.Text = "بلا لون"
        Me.BKNoneColoreCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.BKNoneColoreCheckBox.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(335, 37)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 19)
        Me.Label7.TabIndex = 486
        Me.Label7.Text = "لون مربع البيع"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.IM_CAT_SF)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.Touch_Screen_Panel)
        Me.Panel1.Controls.Add(Me.SubPrint_Panel)
        Me.Panel1.Controls.Add(Me.CP_Bill_Screen_GroupBox)
        Me.Panel1.Controls.Add(Me.SMNameTextBox)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(324, 42)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(433, 524)
        Me.Panel1.TabIndex = 494
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(33, 42)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(38, 32)
        Me.Button2.TabIndex = 713
        Me.Button2.Text = "..."
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'IM_CAT_SF
        '
        Me.IM_CAT_SF.CancelSearchImage = CType(resources.GetObject("IM_CAT_SF.CancelSearchImage"), System.Drawing.Image)
        Me.IM_CAT_SF.Location = New System.Drawing.Point(77, 40)
        Me.IM_CAT_SF.Name = "IM_CAT_SF"
        Me.IM_CAT_SF.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_CAT_SF.Size = New System.Drawing.Size(247, 34)
        Me.IM_CAT_SF.SQL_Column = "NAME"
        Me.IM_CAT_SF.SQL_ID = "ID"
        Me.IM_CAT_SF.SQL_IsNumericSearchField = False
        Me.IM_CAT_SF.SQL_ListSize = 200
        Me.IM_CAT_SF.SQL_NumberOfRows = 0
        Me.IM_CAT_SF.SQL_OrderByField = "NAME"
        Me.IM_CAT_SF.SQL_SearchField = "NAME"
        Me.IM_CAT_SF.SQL_SearchField_WHERE = ""
        Me.IM_CAT_SF.SQL_Table = "IM_CAT"
        Me.IM_CAT_SF.TabIndex = 712
        Me.IM_CAT_SF.TextMaxLength = 250
        Me.IM_CAT_SF.Textt = ""
        Me.IM_CAT_SF.Visible = False
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label29.Location = New System.Drawing.Point(339, 48)
        Me.Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(40, 21)
        Me.Label29.TabIndex = 711
        Me.Label29.Text = "الفئه"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label29.Visible = False
        '
        'Touch_Screen_Panel
        '
        Me.Touch_Screen_Panel.Controls.Add(Me.IM_ViewerButton)
        Me.Touch_Screen_Panel.Controls.Add(Me.FKNoneColoreCheckBox)
        Me.Touch_Screen_Panel.Controls.Add(Me.BKChoaseButton)
        Me.Touch_Screen_Panel.Controls.Add(Me.Label2)
        Me.Touch_Screen_Panel.Controls.Add(Me.Label13)
        Me.Touch_Screen_Panel.Controls.Add(Me.Rank_Num_txt)
        Me.Touch_Screen_Panel.Controls.Add(Me.FKChoaseButton)
        Me.Touch_Screen_Panel.Controls.Add(Me.is_Show_cb)
        Me.Touch_Screen_Panel.Controls.Add(Me.BKNoneColoreCheckBox)
        Me.Touch_Screen_Panel.Controls.Add(Me.Label7)
        Me.Touch_Screen_Panel.Controls.Add(Me.BKPanel)
        Me.Touch_Screen_Panel.Controls.Add(Me.FKPanel)
        Me.Touch_Screen_Panel.Location = New System.Drawing.Point(1, 320)
        Me.Touch_Screen_Panel.Name = "Touch_Screen_Panel"
        Me.Touch_Screen_Panel.Size = New System.Drawing.Size(431, 124)
        Me.Touch_Screen_Panel.TabIndex = 499
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(334, 91)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 19)
        Me.Label2.TabIndex = 496
        Me.Label2.Text = "ترتيب العرض"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Rank_Num_txt
        '
        Me.Rank_Num_txt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Rank_Num_txt.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rank_Num_txt.Location = New System.Drawing.Point(204, 87)
        Me.Rank_Num_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Rank_Num_txt.MaxLength = 200
        Me.Rank_Num_txt.Name = "Rank_Num_txt"
        Me.Rank_Num_txt.Size = New System.Drawing.Size(128, 27)
        Me.Rank_Num_txt.TabIndex = 495
        '
        'is_Show_cb
        '
        Me.is_Show_cb.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.is_Show_cb.AutoSize = True
        Me.is_Show_cb.BackColor = System.Drawing.Color.Transparent
        Me.is_Show_cb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_Show_cb.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.is_Show_cb.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.is_Show_cb.ForeColor = System.Drawing.Color.Black
        Me.is_Show_cb.Location = New System.Drawing.Point(207, 8)
        Me.is_Show_cb.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.is_Show_cb.Name = "is_Show_cb"
        Me.is_Show_cb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.is_Show_cb.Size = New System.Drawing.Size(172, 24)
        Me.is_Show_cb.TabIndex = 494
        Me.is_Show_cb.Text = "عرض في شاشة اللمس"
        Me.is_Show_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.is_Show_cb.UseVisualStyleBackColor = False
        '
        'SubPrint_Panel
        '
        Me.SubPrint_Panel.Controls.Add(Me.DeleteU_btn)
        Me.SubPrint_Panel.Controls.Add(Me.InsertU_btn)
        Me.SubPrint_Panel.Controls.Add(Me.PRINTER_GRID)
        Me.SubPrint_Panel.Controls.Add(Me.SMPrinterComboBox)
        Me.SubPrint_Panel.Controls.Add(Me.PrinterLabel)
        Me.SubPrint_Panel.Location = New System.Drawing.Point(2, 81)
        Me.SubPrint_Panel.Name = "SubPrint_Panel"
        Me.SubPrint_Panel.Size = New System.Drawing.Size(430, 233)
        Me.SubPrint_Panel.TabIndex = 498
        '
        'DeleteU_btn
        '
        Me.DeleteU_btn.BackColor = System.Drawing.Color.White
        Me.DeleteU_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteU_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteU_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DeleteU_btn.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteU_btn.Location = New System.Drawing.Point(5, 5)
        Me.DeleteU_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DeleteU_btn.Name = "DeleteU_btn"
        Me.DeleteU_btn.Size = New System.Drawing.Size(30, 31)
        Me.DeleteU_btn.TabIndex = 706
        Me.DeleteU_btn.Text = "➖"
        Me.DeleteU_btn.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.DeleteU_btn.UseVisualStyleBackColor = False
        '
        'InsertU_btn
        '
        Me.InsertU_btn.BackColor = System.Drawing.Color.White
        Me.InsertU_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.InsertU_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InsertU_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.InsertU_btn.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InsertU_btn.Location = New System.Drawing.Point(39, 5)
        Me.InsertU_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.InsertU_btn.Name = "InsertU_btn"
        Me.InsertU_btn.Size = New System.Drawing.Size(42, 31)
        Me.InsertU_btn.TabIndex = 705
        Me.InsertU_btn.Text = "➕"
        Me.InsertU_btn.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.InsertU_btn.UseVisualStyleBackColor = False
        '
        'PRINTER_GRID
        '
        Me.PRINTER_GRID.AllowUserToAddRows = False
        Me.PRINTER_GRID.AllowUserToDeleteRows = False
        Me.PRINTER_GRID.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.PRINTER_GRID.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PRINTER_GRID.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.PRINTER_GRID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PRINTER_GRID.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID, Me.PTR_NAME_CL})
        Me.PRINTER_GRID.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PRINTER_GRID.Location = New System.Drawing.Point(4, 37)
        Me.PRINTER_GRID.MultiSelect = False
        Me.PRINTER_GRID.Name = "PRINTER_GRID"
        Me.PRINTER_GRID.ReadOnly = True
        Me.PRINTER_GRID.RowHeadersVisible = False
        Me.PRINTER_GRID.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.PRINTER_GRID.RowTemplate.Height = 30
        Me.PRINTER_GRID.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PRINTER_GRID.Size = New System.Drawing.Size(423, 190)
        Me.PRINTER_GRID.TabIndex = 704
        '
        'T_ID
        '
        Me.T_ID.DataPropertyName = "T_ID"
        Me.T_ID.HeaderText = "T_ID"
        Me.T_ID.Name = "T_ID"
        Me.T_ID.ReadOnly = True
        Me.T_ID.Visible = False
        '
        'PTR_NAME_CL
        '
        Me.PTR_NAME_CL.DataPropertyName = "Printer_Name"
        Me.PTR_NAME_CL.HeaderText = "الطابعة"
        Me.PTR_NAME_CL.Name = "PTR_NAME_CL"
        Me.PTR_NAME_CL.ReadOnly = True
        '
        'CP_Bill_Screen_GroupBox
        '
        Me.CP_Bill_Screen_GroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CP_Bill_Screen_GroupBox.Controls.Add(Me.Ksh_Screen_cmb)
        Me.CP_Bill_Screen_GroupBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CP_Bill_Screen_GroupBox.Location = New System.Drawing.Point(6, 453)
        Me.CP_Bill_Screen_GroupBox.Name = "CP_Bill_Screen_GroupBox"
        Me.CP_Bill_Screen_GroupBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CP_Bill_Screen_GroupBox.Size = New System.Drawing.Size(421, 65)
        Me.CP_Bill_Screen_GroupBox.TabIndex = 497
        Me.CP_Bill_Screen_GroupBox.TabStop = False
        Me.CP_Bill_Screen_GroupBox.Text = "كمبيوتر عرض شاشة داخلية"
        '
        'Ksh_Screen_cmb
        '
        Me.Ksh_Screen_cmb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Ksh_Screen_cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Ksh_Screen_cmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Ksh_Screen_cmb.FormattingEnabled = True
        Me.Ksh_Screen_cmb.Location = New System.Drawing.Point(10, 27)
        Me.Ksh_Screen_cmb.Name = "Ksh_Screen_cmb"
        Me.Ksh_Screen_cmb.Size = New System.Drawing.Size(404, 29)
        Me.Ksh_Screen_cmb.TabIndex = 0
        '
        'GeneralMenu
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(759, 636)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.SMDataGridViewX)
        Me.Controls.Add(Me.NewEmpButton)
        Me.Controls.Add(Me.EditEmpButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GeneralMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "التصنيفات"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        CType(Me.SMDataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SMNamerrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrinterErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Touch_Screen_Panel.ResumeLayout(False)
        Me.Touch_Screen_Panel.PerformLayout()
        Me.SubPrint_Panel.ResumeLayout(False)
        Me.SubPrint_Panel.PerformLayout()
        CType(Me.PRINTER_GRID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CP_Bill_Screen_GroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NewEmpButton As System.Windows.Forms.Button
    Friend WithEvents EditEmpButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents SMPrinterComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents PrinterLabel As System.Windows.Forms.Label
    Public WithEvents SMDataGridViewX As DataGridView
    Friend WithEvents SMNamerrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents SMNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PrinterErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents IM_ViewerButton As System.Windows.Forms.Button
    Friend WithEvents FKPanel As System.Windows.Forms.Panel
    Friend WithEvents BKPanel As System.Windows.Forms.Panel
    Friend WithEvents FKChoaseButton As System.Windows.Forms.Button
    Friend WithEvents BKChoaseButton As System.Windows.Forms.Button
    Friend WithEvents FKNoneColoreCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents BKNoneColoreCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents is_Show_cb As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Rank_Num_txt As System.Windows.Forms.TextBox
    Friend WithEvents CP_Bill_Screen_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Ksh_Screen_cmb As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rank_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubPrint_Panel As System.Windows.Forms.Panel
    Friend WithEvents Touch_Screen_Panel As System.Windows.Forms.Panel
    Friend WithEvents DeleteU_btn As Button
    Friend WithEvents InsertU_btn As Button
    Friend WithEvents PRINTER_GRID As DataGridView
    Friend WithEvents T_ID As DataGridViewTextBoxColumn
    Friend WithEvents PTR_NAME_CL As DataGridViewTextBoxColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents IM_CAT_SF As resturant.FSearch_Filter
    Friend WithEvents Label29 As System.Windows.Forms.Label
End Class