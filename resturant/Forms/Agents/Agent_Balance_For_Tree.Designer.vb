<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Agent_Balance_For_Tree
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Agent_Balance_For_Tree))
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.EXP_DataGridView = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TREE_CODE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AGENTS_DataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TR_DataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_DataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_TXT = New System.Windows.Forms.TextBox()
        Me.TR_TXT = New System.Windows.Forms.TextBox()
        Me.AGENTS_TXT = New System.Windows.Forms.TextBox()
        Me.GENERAL_TXT = New System.Windows.Forms.TextBox()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.GeneralTitle_LB = New System.Windows.Forms.Label()
        Me.AgentsTitle_LB = New System.Windows.Forms.Label()
        Me.TreasuryTitle_LB = New System.Windows.Forms.Label()
        Me.StoresTitle_LB = New System.Windows.Forms.Label()
        Me.Help_LB = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PCH_EXP_DataGridView = New System.Windows.Forms.DataGridView()
        Me.PCH_EX_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pch_Ex_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PCH_EX_TREE_CODE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.EXP_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AGENTS_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TR_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ST_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TitleBar_Panel.SuspendLayout()
        CType(Me.PCH_EXP_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ExitFormButton.FlatAppearance.BorderSize = 0
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(0, 685)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(1004, 50)
        Me.ExitFormButton.TabIndex = 673
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "رجوع"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'EXP_DataGridView
        '
        Me.EXP_DataGridView.AllowUserToAddRows = False
        Me.EXP_DataGridView.AllowUserToDeleteRows = False
        Me.EXP_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.EXP_DataGridView.BackgroundColor = System.Drawing.Color.White
        Me.EXP_DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EXP_DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.EXP_DataGridView.ColumnHeadersHeight = 34
        Me.EXP_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.EXP_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.B_NAME_CL, Me.TREE_CODE_CL})
        Me.EXP_DataGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EXP_DataGridView.EnableHeadersVisualStyles = False
        Me.EXP_DataGridView.GridColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.EXP_DataGridView.Location = New System.Drawing.Point(15, 152)
        Me.EXP_DataGridView.MultiSelect = False
        Me.EXP_DataGridView.Name = "EXP_DataGridView"
        Me.EXP_DataGridView.ReadOnly = True
        Me.EXP_DataGridView.RowHeadersVisible = False
        Me.EXP_DataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.EXP_DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.EXP_DataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.EXP_DataGridView.RowTemplate.Height = 32
        Me.EXP_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.EXP_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.EXP_DataGridView.Size = New System.Drawing.Size(322, 236)
        Me.EXP_DataGridView.TabIndex = 701
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "EX_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'B_NAME_CL
        '
        Me.B_NAME_CL.DataPropertyName = "Ex_Name"
        Me.B_NAME_CL.HeaderText = "الحســـاب"
        Me.B_NAME_CL.Name = "B_NAME_CL"
        Me.B_NAME_CL.ReadOnly = True
        '
        'TREE_CODE_CL
        '
        Me.TREE_CODE_CL.DataPropertyName = "TREE_CODE"
        Me.TREE_CODE_CL.HeaderText = "كود الحساب"
        Me.TREE_CODE_CL.Name = "TREE_CODE_CL"
        Me.TREE_CODE_CL.ReadOnly = True
        '
        'AGENTS_DataGridView
        '
        Me.AGENTS_DataGridView.AllowUserToAddRows = False
        Me.AGENTS_DataGridView.AllowUserToDeleteRows = False
        Me.AGENTS_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.AGENTS_DataGridView.BackgroundColor = System.Drawing.Color.White
        Me.AGENTS_DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AGENTS_DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.AGENTS_DataGridView.ColumnHeadersHeight = 34
        Me.AGENTS_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.AGENTS_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.AGENTS_DataGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AGENTS_DataGridView.EnableHeadersVisualStyles = False
        Me.AGENTS_DataGridView.GridColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.AGENTS_DataGridView.Location = New System.Drawing.Point(350, 152)
        Me.AGENTS_DataGridView.MultiSelect = False
        Me.AGENTS_DataGridView.Name = "AGENTS_DataGridView"
        Me.AGENTS_DataGridView.ReadOnly = True
        Me.AGENTS_DataGridView.RowHeadersVisible = False
        Me.AGENTS_DataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.AGENTS_DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.AGENTS_DataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.AGENTS_DataGridView.RowTemplate.Height = 32
        Me.AGENTS_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.AGENTS_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGENTS_DataGridView.Size = New System.Drawing.Size(372, 520)
        Me.AGENTS_DataGridView.TabIndex = 702
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "AG_ID"
        Me.DataGridViewTextBoxColumn1.HeaderText = "T_ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Ag_name"
        Me.DataGridViewTextBoxColumn2.HeaderText = "الحســـاب"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "TREE_CODE"
        Me.DataGridViewTextBoxColumn3.HeaderText = "كود الحساب"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'TR_DataGridView
        '
        Me.TR_DataGridView.AllowUserToAddRows = False
        Me.TR_DataGridView.AllowUserToDeleteRows = False
        Me.TR_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.TR_DataGridView.BackgroundColor = System.Drawing.Color.White
        Me.TR_DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TR_DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.TR_DataGridView.ColumnHeadersHeight = 34
        Me.TR_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.TR_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6})
        Me.TR_DataGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TR_DataGridView.EnableHeadersVisualStyles = False
        Me.TR_DataGridView.GridColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.TR_DataGridView.Location = New System.Drawing.Point(735, 152)
        Me.TR_DataGridView.MultiSelect = False
        Me.TR_DataGridView.Name = "TR_DataGridView"
        Me.TR_DataGridView.ReadOnly = True
        Me.TR_DataGridView.RowHeadersVisible = False
        Me.TR_DataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.TR_DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TR_DataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.TR_DataGridView.RowTemplate.Height = 32
        Me.TR_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TR_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.TR_DataGridView.Size = New System.Drawing.Size(254, 225)
        Me.TR_DataGridView.TabIndex = 703
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Tr_ID"
        Me.DataGridViewTextBoxColumn4.HeaderText = "T_ID"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Tr_Name"
        Me.DataGridViewTextBoxColumn5.HeaderText = "الحســـاب"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "TREE_CODE"
        Me.DataGridViewTextBoxColumn6.HeaderText = "كود الحساب"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'ST_DataGridView
        '
        Me.ST_DataGridView.AllowUserToAddRows = False
        Me.ST_DataGridView.AllowUserToDeleteRows = False
        Me.ST_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.ST_DataGridView.BackgroundColor = System.Drawing.Color.White
        Me.ST_DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ST_DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.ST_DataGridView.ColumnHeadersHeight = 34
        Me.ST_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.ST_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9})
        Me.ST_DataGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_DataGridView.EnableHeadersVisualStyles = False
        Me.ST_DataGridView.GridColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ST_DataGridView.Location = New System.Drawing.Point(735, 436)
        Me.ST_DataGridView.MultiSelect = False
        Me.ST_DataGridView.Name = "ST_DataGridView"
        Me.ST_DataGridView.ReadOnly = True
        Me.ST_DataGridView.RowHeadersVisible = False
        Me.ST_DataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.ST_DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ST_DataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.ST_DataGridView.RowTemplate.Height = 32
        Me.ST_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ST_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ST_DataGridView.Size = New System.Drawing.Size(254, 236)
        Me.ST_DataGridView.TabIndex = 704
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "ST_ID"
        Me.DataGridViewTextBoxColumn7.HeaderText = "T_ID"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "ST_Name"
        Me.DataGridViewTextBoxColumn8.HeaderText = "الحســـاب"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "TREE_CODE"
        Me.DataGridViewTextBoxColumn9.HeaderText = "كود الحساب"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'ST_TXT
        '
        Me.ST_TXT.BackColor = System.Drawing.Color.White
        Me.ST_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ST_TXT.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.ST_TXT.Location = New System.Drawing.Point(735, 407)
        Me.ST_TXT.Name = "ST_TXT"
        Me.ST_TXT.Size = New System.Drawing.Size(254, 25)
        Me.ST_TXT.TabIndex = 705
        '
        'TR_TXT
        '
        Me.TR_TXT.BackColor = System.Drawing.Color.White
        Me.TR_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TR_TXT.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.TR_TXT.Location = New System.Drawing.Point(735, 123)
        Me.TR_TXT.Name = "TR_TXT"
        Me.TR_TXT.Size = New System.Drawing.Size(254, 25)
        Me.TR_TXT.TabIndex = 706
        '
        'AGENTS_TXT
        '
        Me.AGENTS_TXT.BackColor = System.Drawing.Color.White
        Me.AGENTS_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AGENTS_TXT.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.AGENTS_TXT.Location = New System.Drawing.Point(350, 123)
        Me.AGENTS_TXT.Name = "AGENTS_TXT"
        Me.AGENTS_TXT.Size = New System.Drawing.Size(372, 25)
        Me.AGENTS_TXT.TabIndex = 707
        '
        'GENERAL_TXT
        '
        Me.GENERAL_TXT.BackColor = System.Drawing.Color.White
        Me.GENERAL_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GENERAL_TXT.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GENERAL_TXT.Location = New System.Drawing.Point(15, 123)
        Me.GENERAL_TXT.Name = "GENERAL_TXT"
        Me.GENERAL_TXT.Size = New System.Drawing.Size(322, 25)
        Me.GENERAL_TXT.TabIndex = 708
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(1004, 48)
        Me.TitleBar_Panel.TabIndex = 709
        '
        'TopTitle_LB
        '
        Me.TopTitle_LB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TopTitle_LB.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.TopTitle_LB.ForeColor = System.Drawing.Color.White
        Me.TopTitle_LB.Location = New System.Drawing.Point(0, 0)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Padding = New System.Windows.Forms.Padding(16, 0, 16, 0)
        Me.TopTitle_LB.Size = New System.Drawing.Size(1004, 48)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Text = "الحسابات الافتراضية العامة"
        Me.TopTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GeneralTitle_LB
        '
        Me.GeneralTitle_LB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GeneralTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.GeneralTitle_LB.Location = New System.Drawing.Point(15, 96)
        Me.GeneralTitle_LB.Name = "GeneralTitle_LB"
        Me.GeneralTitle_LB.Size = New System.Drawing.Size(322, 24)
        Me.GeneralTitle_LB.TabIndex = 710
        Me.GeneralTitle_LB.Text = "المصروفات"
        Me.GeneralTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AgentsTitle_LB
        '
        Me.AgentsTitle_LB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.AgentsTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.AgentsTitle_LB.Location = New System.Drawing.Point(350, 96)
        Me.AgentsTitle_LB.Name = "AgentsTitle_LB"
        Me.AgentsTitle_LB.Size = New System.Drawing.Size(372, 24)
        Me.AgentsTitle_LB.TabIndex = 711
        Me.AgentsTitle_LB.Text = "العملاء"
        Me.AgentsTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TreasuryTitle_LB
        '
        Me.TreasuryTitle_LB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.TreasuryTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.TreasuryTitle_LB.Location = New System.Drawing.Point(735, 96)
        Me.TreasuryTitle_LB.Name = "TreasuryTitle_LB"
        Me.TreasuryTitle_LB.Size = New System.Drawing.Size(254, 24)
        Me.TreasuryTitle_LB.TabIndex = 712
        Me.TreasuryTitle_LB.Text = "الخزائن"
        Me.TreasuryTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'StoresTitle_LB
        '
        Me.StoresTitle_LB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.StoresTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.StoresTitle_LB.Location = New System.Drawing.Point(735, 380)
        Me.StoresTitle_LB.Name = "StoresTitle_LB"
        Me.StoresTitle_LB.Size = New System.Drawing.Size(254, 24)
        Me.StoresTitle_LB.TabIndex = 713
        Me.StoresTitle_LB.Text = "المخازن"
        Me.StoresTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Help_LB
        '
        Me.Help_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Help_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.Help_LB.Location = New System.Drawing.Point(15, 57)
        Me.Help_LB.Name = "Help_LB"
        Me.Help_LB.Size = New System.Drawing.Size(974, 26)
        Me.Help_LB.TabIndex = 714
        Me.Help_LB.Text = "اكتب في مربع البحث لتصفية النتائج، وانقر نقرًا مزدوجًا على أي حساب لتعديل كود الح" &
    "ساب."
        Me.Help_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(15, 391)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(322, 24)
        Me.Label1.TabIndex = 717
        Me.Label1.Text = "مصروفات المشتريات"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.TextBox1.Location = New System.Drawing.Point(15, 418)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(322, 25)
        Me.TextBox1.TabIndex = 716
        '
        'PCH_EXP_DataGridView
        '
        Me.PCH_EXP_DataGridView.AllowUserToAddRows = False
        Me.PCH_EXP_DataGridView.AllowUserToDeleteRows = False
        Me.PCH_EXP_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.PCH_EXP_DataGridView.BackgroundColor = System.Drawing.Color.White
        Me.PCH_EXP_DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PCH_EXP_DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.PCH_EXP_DataGridView.ColumnHeadersHeight = 34
        Me.PCH_EXP_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.PCH_EXP_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PCH_EX_ID_CL, Me.Pch_Ex_Name_CL, Me.PCH_EX_TREE_CODE_CL})
        Me.PCH_EXP_DataGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PCH_EXP_DataGridView.EnableHeadersVisualStyles = False
        Me.PCH_EXP_DataGridView.GridColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.PCH_EXP_DataGridView.Location = New System.Drawing.Point(15, 447)
        Me.PCH_EXP_DataGridView.MultiSelect = False
        Me.PCH_EXP_DataGridView.Name = "PCH_EXP_DataGridView"
        Me.PCH_EXP_DataGridView.ReadOnly = True
        Me.PCH_EXP_DataGridView.RowHeadersVisible = False
        Me.PCH_EXP_DataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.PCH_EXP_DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.PCH_EXP_DataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.PCH_EXP_DataGridView.RowTemplate.Height = 32
        Me.PCH_EXP_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.PCH_EXP_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PCH_EXP_DataGridView.Size = New System.Drawing.Size(322, 236)
        Me.PCH_EXP_DataGridView.TabIndex = 715
        '
        'PCH_EX_ID_CL
        '
        Me.PCH_EX_ID_CL.DataPropertyName = "EX_ID"
        Me.PCH_EX_ID_CL.HeaderText = "T_ID"
        Me.PCH_EX_ID_CL.Name = "PCH_EX_ID_CL"
        Me.PCH_EX_ID_CL.ReadOnly = True
        Me.PCH_EX_ID_CL.Visible = False
        '
        'Pch_Ex_Name_CL
        '
        Me.Pch_Ex_Name_CL.DataPropertyName = "Ex_Name"
        Me.Pch_Ex_Name_CL.HeaderText = "الحســـاب"
        Me.Pch_Ex_Name_CL.Name = "Pch_Ex_Name_CL"
        Me.Pch_Ex_Name_CL.ReadOnly = True
        '
        'PCH_EX_TREE_CODE_CL
        '
        Me.PCH_EX_TREE_CODE_CL.DataPropertyName = "TREE_CODE"
        Me.PCH_EX_TREE_CODE_CL.HeaderText = "كود الحساب"
        Me.PCH_EX_TREE_CODE_CL.Name = "PCH_EX_TREE_CODE_CL"
        Me.PCH_EX_TREE_CODE_CL.ReadOnly = True
        '
        'Agent_Balance_For_Tree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1004, 735)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.PCH_EXP_DataGridView)
        Me.Controls.Add(Me.Help_LB)
        Me.Controls.Add(Me.StoresTitle_LB)
        Me.Controls.Add(Me.TreasuryTitle_LB)
        Me.Controls.Add(Me.AgentsTitle_LB)
        Me.Controls.Add(Me.GeneralTitle_LB)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.GENERAL_TXT)
        Me.Controls.Add(Me.AGENTS_TXT)
        Me.Controls.Add(Me.TR_TXT)
        Me.Controls.Add(Me.ST_TXT)
        Me.Controls.Add(Me.ST_DataGridView)
        Me.Controls.Add(Me.TR_DataGridView)
        Me.Controls.Add(Me.AGENTS_DataGridView)
        Me.Controls.Add(Me.EXP_DataGridView)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "Agent_Balance_For_Tree"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الحسابات الإقتراضية العامة"
        CType(Me.EXP_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AGENTS_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TR_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ST_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TitleBar_Panel.ResumeLayout(False)
        CType(Me.PCH_EXP_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ExitFormButton As Button
    Friend WithEvents EXP_DataGridView As DataGridView
    Friend WithEvents AGENTS_DataGridView As DataGridView
    Friend WithEvents TR_DataGridView As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents ST_DataGridView As DataGridView
    Friend WithEvents ST_TXT As TextBox
    Friend WithEvents TR_TXT As TextBox
    Friend WithEvents AGENTS_TXT As TextBox
    Friend WithEvents GENERAL_TXT As TextBox
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents B_NAME_CL As DataGridViewTextBoxColumn
    Friend WithEvents TREE_CODE_CL As DataGridViewTextBoxColumn
    Friend WithEvents TitleBar_Panel As Panel
    Friend WithEvents TopTitle_LB As Label
    Friend WithEvents GeneralTitle_LB As Label
    Friend WithEvents AgentsTitle_LB As Label
    Friend WithEvents TreasuryTitle_LB As Label
    Friend WithEvents StoresTitle_LB As Label
    Friend WithEvents Help_LB As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents PCH_EXP_DataGridView As DataGridView
    Friend WithEvents PCH_EX_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents Pch_Ex_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents PCH_EX_TREE_CODE_CL As DataGridViewTextBoxColumn
End Class
