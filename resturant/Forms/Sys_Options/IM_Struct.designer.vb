<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_Struct
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_Struct))
        Me.Label = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.FRM_GDX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.IM_ID_CL2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Sh_ByNum_CB = New System.Windows.Forms.CheckBox()
        Me.IM_FRM_txt = New System.Windows.Forms.TextBox()
        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.QtyTextBox = New System.Windows.Forms.TextBox()
        Me.IM_Unit_cm = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.IM_FRM_MENU_DV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INDX_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.USER_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.REMOVE_FRM_Btn = New System.Windows.Forms.Button()
        Me.ADD_FRM_Btn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.FRM_GDX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IM_FRM_MENU_DV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label
        '
        Me.Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label.Font = New System.Drawing.Font("JF Flat", 15.75!, System.Drawing.FontStyle.Bold)
        Me.Label.Location = New System.Drawing.Point(2, 2)
        Me.Label.Name = "Label"
        Me.Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label.Size = New System.Drawing.Size(814, 41)
        Me.Label.TabIndex = 387
        Me.Label.Text = "تركيبة الصنف :"
        Me.Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(695, 632)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(127, 40)
        Me.ExitFormButton.TabIndex = 655
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label)
        Me.Panel1.Location = New System.Drawing.Point(2, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(817, 45)
        Me.Panel1.TabIndex = 657
        '
        'FRM_GDX
        '
        Me.FRM_GDX.AllowUserToAddRows = False
        Me.FRM_GDX.AllowUserToDeleteRows = False
        Me.FRM_GDX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.FRM_GDX.BackgroundColor = System.Drawing.Color.White
        Me.FRM_GDX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FRM_GDX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.FRM_GDX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FRM_GDX.ColumnHeadersVisible = False
        Me.FRM_GDX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_ID_CL2, Me.item_name_CL2, Me.DataGridViewTextBoxColumn8})
        Me.FRM_GDX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.FRM_GDX.DefaultCellStyle = DataGridViewCellStyle4
        Me.FRM_GDX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.FRM_GDX.Location = New System.Drawing.Point(265, 107)
        Me.FRM_GDX.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FRM_GDX.MultiSelect = False
        Me.FRM_GDX.Name = "FRM_GDX"
        Me.FRM_GDX.ReadOnly = True
        Me.FRM_GDX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.FRM_GDX.RowHeadersVisible = False
        DataGridViewCellStyle5.Font = New System.Drawing.Font("JF Flat", 11.0!)
        Me.FRM_GDX.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.FRM_GDX.RowTemplate.Height = 45
        Me.FRM_GDX.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.FRM_GDX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.FRM_GDX.Size = New System.Drawing.Size(557, 2)
        Me.FRM_GDX.TabIndex = 670
        Me.FRM_GDX.Visible = False
        '
        'IM_ID_CL2
        '
        Me.IM_ID_CL2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.IM_ID_CL2.DataPropertyName = "IM_ID"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_ID_CL2.DefaultCellStyle = DataGridViewCellStyle2
        Me.IM_ID_CL2.FillWeight = 5.0!
        Me.IM_ID_CL2.Frozen = True
        Me.IM_ID_CL2.HeaderText = "رقم الصنف"
        Me.IM_ID_CL2.Name = "IM_ID_CL2"
        Me.IM_ID_CL2.ReadOnly = True
        Me.IM_ID_CL2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IM_ID_CL2.Visible = False
        Me.IM_ID_CL2.Width = 5
        '
        'item_name_CL2
        '
        Me.item_name_CL2.DataPropertyName = "item_name"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.item_name_CL2.DefaultCellStyle = DataGridViewCellStyle3
        Me.item_name_CL2.FillWeight = 69.61895!
        Me.item_name_CL2.HeaderText = "الصنف"
        Me.item_name_CL2.Name = "item_name_CL2"
        Me.item_name_CL2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "isValid"
        Me.DataGridViewTextBoxColumn8.HeaderText = "صلاحية"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(724, 48)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label22.Size = New System.Drawing.Size(95, 24)
        Me.Label22.TabIndex = 677
        Me.Label22.Text = "إسم الصنف :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Sh_ByNum_CB
        '
        Me.Sh_ByNum_CB.AutoSize = True
        Me.Sh_ByNum_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sh_ByNum_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Sh_ByNum_CB.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Sh_ByNum_CB.Location = New System.Drawing.Point(268, 50)
        Me.Sh_ByNum_CB.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Sh_ByNum_CB.Name = "Sh_ByNum_CB"
        Me.Sh_ByNum_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Sh_ByNum_CB.Size = New System.Drawing.Size(93, 25)
        Me.Sh_ByNum_CB.TabIndex = 676
        Me.Sh_ByNum_CB.Text = "رقم الصنف"
        Me.Sh_ByNum_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Sh_ByNum_CB.UseVisualStyleBackColor = True
        '
        'IM_FRM_txt
        '
        Me.IM_FRM_txt.BackColor = System.Drawing.Color.LightGray
        Me.IM_FRM_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_FRM_txt.Font = New System.Drawing.Font("JF Flat", 10.0!)
        Me.IM_FRM_txt.Location = New System.Drawing.Point(520, 75)
        Me.IM_FRM_txt.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.IM_FRM_txt.Name = "IM_FRM_txt"
        Me.IM_FRM_txt.Size = New System.Drawing.Size(302, 31)
        Me.IM_FRM_txt.TabIndex = 669
        '
        'Barcode_SH_txt
        '
        Me.Barcode_SH_txt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Barcode_SH_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_SH_txt.Location = New System.Drawing.Point(268, 76)
        Me.Barcode_SH_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
        Me.Barcode_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Barcode_SH_txt.Size = New System.Drawing.Size(204, 29)
        Me.Barcode_SH_txt.TabIndex = 671
        Me.Barcode_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(406, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(62, 24)
        Me.Label1.TabIndex = 673
        Me.Label1.Text = "باركود :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'QtyTextBox
        '
        Me.QtyTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.QtyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.QtyTextBox.Font = New System.Drawing.Font("Times New Roman", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QtyTextBox.ForeColor = System.Drawing.Color.Black
        Me.QtyTextBox.Location = New System.Drawing.Point(488, 117)
        Me.QtyTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.QtyTextBox.MaxLength = 250
        Me.QtyTextBox.Name = "QtyTextBox"
        Me.QtyTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.QtyTextBox.Size = New System.Drawing.Size(88, 27)
        Me.QtyTextBox.TabIndex = 667
        Me.QtyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IM_Unit_cm
        '
        Me.IM_Unit_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Unit_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IM_Unit_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Unit_cm.Font = New System.Drawing.Font("JF Flat", 9.5!, System.Drawing.FontStyle.Bold)
        Me.IM_Unit_cm.FormattingEnabled = True
        Me.IM_Unit_cm.Location = New System.Drawing.Point(646, 115)
        Me.IM_Unit_cm.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IM_Unit_cm.Name = "IM_Unit_cm"
        Me.IM_Unit_cm.Size = New System.Drawing.Size(112, 31)
        Me.IM_Unit_cm.TabIndex = 674
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label20.Location = New System.Drawing.Point(762, 120)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(57, 24)
        Me.Label20.TabIndex = 675
        Me.Label20.Text = "الوحدة"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label21.Location = New System.Drawing.Point(578, 118)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(56, 24)
        Me.Label21.TabIndex = 668
        Me.Label21.Text = "الكمية"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'IM_FRM_MENU_DV
        '
        Me.IM_FRM_MENU_DV.AllowUserToAddRows = False
        Me.IM_FRM_MENU_DV.AllowUserToDeleteRows = False
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro
        Me.IM_FRM_MENU_DV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.IM_FRM_MENU_DV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IM_FRM_MENU_DV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.IM_FRM_MENU_DV.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IM_FRM_MENU_DV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.IM_FRM_MENU_DV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IM_FRM_MENU_DV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.INDX_CL, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.USER_CL})
        Me.IM_FRM_MENU_DV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IM_FRM_MENU_DV.DefaultCellStyle = DataGridViewCellStyle9
        Me.IM_FRM_MENU_DV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IM_FRM_MENU_DV.Location = New System.Drawing.Point(4, 149)
        Me.IM_FRM_MENU_DV.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IM_FRM_MENU_DV.MultiSelect = False
        Me.IM_FRM_MENU_DV.Name = "IM_FRM_MENU_DV"
        Me.IM_FRM_MENU_DV.ReadOnly = True
        Me.IM_FRM_MENU_DV.RowHeadersVisible = False
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle10.Font = New System.Drawing.Font("JF Flat", 11.0!)
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IM_FRM_MENU_DV.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.IM_FRM_MENU_DV.RowTemplate.Height = 32
        Me.IM_FRM_MENU_DV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IM_FRM_MENU_DV.Size = New System.Drawing.Size(818, 481)
        Me.IM_FRM_MENU_DV.TabIndex = 666
        '
        'T_ID_CL
        '
        Me.T_ID_CL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.FillWeight = 5.0!
        Me.T_ID_CL.Frozen = True
        Me.T_ID_CL.HeaderText = "ر.الصنف"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        Me.T_ID_CL.Width = 5
        '
        'INDX_CL
        '
        Me.INDX_CL.FillWeight = 28.02963!
        Me.INDX_CL.HeaderText = "ت"
        Me.INDX_CL.Name = "INDX_CL"
        Me.INDX_CL.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "item_name"
        Me.DataGridViewTextBoxColumn3.FillWeight = 350.0!
        Me.DataGridViewTextBoxColumn3.HeaderText = "التركيبات"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "U_Name"
        Me.DataGridViewTextBoxColumn4.HeaderText = "الوحدة"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Qty"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn5.FillWeight = 119.9922!
        Me.DataGridViewTextBoxColumn5.HeaderText = "العدد"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'USER_CL
        '
        Me.USER_CL.DataPropertyName = "UserName"
        Me.USER_CL.HeaderText = "المدخل"
        Me.USER_CL.Name = "USER_CL"
        Me.USER_CL.ReadOnly = True
        '
        'REMOVE_FRM_Btn
        '
        Me.REMOVE_FRM_Btn.BackColor = System.Drawing.Color.White
        Me.REMOVE_FRM_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_minus_173055
        Me.REMOVE_FRM_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.REMOVE_FRM_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.REMOVE_FRM_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.REMOVE_FRM_Btn.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.REMOVE_FRM_Btn.Location = New System.Drawing.Point(4, 119)
        Me.REMOVE_FRM_Btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.REMOVE_FRM_Btn.Name = "REMOVE_FRM_Btn"
        Me.REMOVE_FRM_Btn.Size = New System.Drawing.Size(38, 28)
        Me.REMOVE_FRM_Btn.TabIndex = 679
        Me.REMOVE_FRM_Btn.UseVisualStyleBackColor = False
        '
        'ADD_FRM_Btn
        '
        Me.ADD_FRM_Btn.BackColor = System.Drawing.Color.White
        Me.ADD_FRM_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.ADD_FRM_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ADD_FRM_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADD_FRM_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADD_FRM_Btn.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADD_FRM_Btn.Location = New System.Drawing.Point(45, 119)
        Me.ADD_FRM_Btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ADD_FRM_Btn.Name = "ADD_FRM_Btn"
        Me.ADD_FRM_Btn.Size = New System.Drawing.Size(38, 28)
        Me.ADD_FRM_Btn.TabIndex = 678
        Me.ADD_FRM_Btn.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.BackgroundImage = Global.resturant.My.Resources.Resources.if_download_173000
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(473, 75)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(47, 31)
        Me.Button1.TabIndex = 672
        Me.Button1.UseVisualStyleBackColor = False
        '
        'IM_Struct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(825, 675)
        Me.Controls.Add(Me.FRM_GDX)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Sh_ByNum_CB)
        Me.Controls.Add(Me.IM_FRM_txt)
        Me.Controls.Add(Me.Barcode_SH_txt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.QtyTextBox)
        Me.Controls.Add(Me.IM_Unit_cm)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.IM_FRM_MENU_DV)
        Me.Controls.Add(Me.REMOVE_FRM_Btn)
        Me.Controls.Add(Me.ADD_FRM_Btn)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.MinimizeBox = False
        Me.Name = "IM_Struct"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "كشف تركيبه صنف"
        Me.Panel1.ResumeLayout(False)
        CType(Me.FRM_GDX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IM_FRM_MENU_DV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents FRM_GDX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Sh_ByNum_CB As System.Windows.Forms.CheckBox
    Friend WithEvents IM_FRM_txt As System.Windows.Forms.TextBox
    Friend WithEvents Barcode_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents QtyTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IM_Unit_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents IM_FRM_MENU_DV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents REMOVE_FRM_Btn As System.Windows.Forms.Button
    Friend WithEvents ADD_FRM_Btn As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INDX_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents USER_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_ID_CL2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
