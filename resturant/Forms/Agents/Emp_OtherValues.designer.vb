<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Emp_OtherValues
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Emp_OtherValues))
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.VoidLb = New System.Windows.Forms.Label()
        Me.HeaderCloseBtn = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.StateComboBox = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerDATE = New System.Windows.Forms.DateTimePicker()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UserName_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Stuts_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VAL_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NOTES_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.is_Void_CL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ValType_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NOTES_TXT = New System.Windows.Forms.TextBox()
        Me.Start_txt = New System.Windows.Forms.TextBox()
        Me.New_Btn = New System.Windows.Forms.Button()
        Me.UPDATE_btn = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Val_txt = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.AG_Cm = New resturant.FSearch_Filter()
        Me.Cancel_Btn = New System.Windows.Forms.Button()
        Me.TitleBar_Panel.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.TitleBar_Panel.Size = New System.Drawing.Size(959, 40)
        Me.TitleBar_Panel.TabIndex = 999
        Me.TitleBar_Panel.Tag = "HEADER"
        '
        'VoidLb
        '
        Me.VoidLb.BackColor = System.Drawing.Color.Red
        Me.VoidLb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VoidLb.Dock = System.Windows.Forms.DockStyle.Left
        Me.VoidLb.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.VoidLb.Location = New System.Drawing.Point(45, 0)
        Me.VoidLb.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.VoidLb.Name = "VoidLb"
        Me.VoidLb.Size = New System.Drawing.Size(519, 40)
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
        Me.HeaderCloseBtn.Size = New System.Drawing.Size(45, 40)
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
        Me.TopTitle_LB.Location = New System.Drawing.Point(790, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(154, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "المكافئات والخصومات"
        '
        'StateComboBox
        '
        Me.StateComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StateComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.StateComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.StateComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.StateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.StateComboBox.FormattingEnabled = True
        Me.StateComboBox.Items.AddRange(New Object() {"مكافئة", "خصم"})
        Me.StateComboBox.Location = New System.Drawing.Point(639, 88)
        Me.StateComboBox.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.StateComboBox.Name = "StateComboBox"
        Me.StateComboBox.Size = New System.Drawing.Size(206, 29)
        Me.StateComboBox.TabIndex = 44
        '
        'DateTimePickerDATE
        '
        Me.DateTimePickerDATE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerDATE.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePickerDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerDATE.Location = New System.Drawing.Point(639, 56)
        Me.DateTimePickerDATE.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.DateTimePickerDATE.Name = "DateTimePickerDATE"
        Me.DateTimePickerDATE.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTimePickerDATE.RightToLeftLayout = True
        Me.DateTimePickerDATE.Size = New System.Drawing.Size(206, 29)
        Me.DateTimePickerDATE.TabIndex = 43
        '
        'SaveButton
        '
        Me.SaveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SaveButton.BackColor = System.Drawing.Color.White
        Me.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveButton.Enabled = False
        Me.SaveButton.FlatAppearance.BorderSize = 0
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveButton.Location = New System.Drawing.Point(257, 533)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(191, 38)
        Me.SaveButton.TabIndex = 40
        Me.SaveButton.Tag = "SAVE"
        Me.SaveButton.Text = "حـفــظ"
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(848, 57)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 21)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "التاريخ"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(850, 91)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 21)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "النــوع"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(848, 15)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 21)
        Me.Label1.TabIndex = 117
        Me.Label1.Text = "الموظف"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.DeepSkyBlue
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.DX, Me.Column2, Me.UserName_CL, Me.Stuts_Name_CL, Me.VAL_CL, Me.NOTES_CL, Me.is_Void_CL, Me.ValType_CL})
        Me.DataGridView1.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.DarkGray
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.Location = New System.Drawing.Point(2, 231)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DataGridView1.RowHeadersVisible = False
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle15
        Me.DataGridView1.RowTemplate.Height = 30
        Me.DataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(956, 290)
        Me.DataGridView1.TabIndex = 641
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.FillWeight = 0.6769074!
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'DX
        '
        Me.DX.FillWeight = 25.0!
        Me.DX.HeaderText = ""
        Me.DX.Name = "DX"
        Me.DX.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "DateOF_Order"
        DataGridViewCellStyle13.Format = "d"
        DataGridViewCellStyle13.NullValue = Nothing
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle13
        Me.Column2.FillWeight = 180.0!
        Me.Column2.HeaderText = "التاريخ"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'UserName_CL
        '
        Me.UserName_CL.DataPropertyName = "UserName"
        Me.UserName_CL.FillWeight = 150.0!
        Me.UserName_CL.HeaderText = "المدخل"
        Me.UserName_CL.Name = "UserName_CL"
        Me.UserName_CL.ReadOnly = True
        '
        'Stuts_Name_CL
        '
        Me.Stuts_Name_CL.DataPropertyName = "Type_Name"
        Me.Stuts_Name_CL.HeaderText = "النوع"
        Me.Stuts_Name_CL.Name = "Stuts_Name_CL"
        Me.Stuts_Name_CL.ReadOnly = True
        '
        'VAL_CL
        '
        Me.VAL_CL.DataPropertyName = "Value"
        Me.VAL_CL.HeaderText = "القيمة"
        Me.VAL_CL.Name = "VAL_CL"
        Me.VAL_CL.ReadOnly = True
        '
        'NOTES_CL
        '
        Me.NOTES_CL.DataPropertyName = "About"
        Me.NOTES_CL.HeaderText = "ذلك عن"
        Me.NOTES_CL.Name = "NOTES_CL"
        Me.NOTES_CL.ReadOnly = True
        Me.NOTES_CL.Visible = False
        '
        'is_Void_CL
        '
        Me.is_Void_CL.DataPropertyName = "isVoid"
        Me.is_Void_CL.FillWeight = 40.0!
        Me.is_Void_CL.HeaderText = "ملغية"
        Me.is_Void_CL.Name = "is_Void_CL"
        Me.is_Void_CL.ReadOnly = True
        Me.is_Void_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.is_Void_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ValType_CL
        '
        Me.ValType_CL.DataPropertyName = "ValType"
        Me.ValType_CL.HeaderText = "ValType"
        Me.ValType_CL.Name = "ValType_CL"
        Me.ValType_CL.ReadOnly = True
        Me.ValType_CL.Visible = False
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(848, 158)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 21)
        Me.Label6.TabIndex = 643
        Me.Label6.Text = "ذلك عن"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NOTES_TXT
        '
        Me.NOTES_TXT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NOTES_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NOTES_TXT.Location = New System.Drawing.Point(500, 155)
        Me.NOTES_TXT.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.NOTES_TXT.Name = "NOTES_TXT"
        Me.NOTES_TXT.Size = New System.Drawing.Size(345, 29)
        Me.NOTES_TXT.TabIndex = 642
        '
        'Start_txt
        '
        Me.Start_txt.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Start_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Start_txt.ForeColor = System.Drawing.Color.Blue
        Me.Start_txt.Location = New System.Drawing.Point(235, 155)
        Me.Start_txt.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.Start_txt.MaxLength = 10
        Me.Start_txt.Name = "Start_txt"
        Me.Start_txt.ReadOnly = True
        Me.Start_txt.Size = New System.Drawing.Size(264, 29)
        Me.Start_txt.TabIndex = 647
        Me.Start_txt.Text = "---"
        Me.Start_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'New_Btn
        '
        Me.New_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.New_Btn.BackColor = System.Drawing.Color.White
        Me.New_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.New_Btn.FlatAppearance.BorderSize = 0
        Me.New_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.New_Btn.Location = New System.Drawing.Point(452, 533)
        Me.New_Btn.Margin = New System.Windows.Forms.Padding(4, 8, 4, 8)
        Me.New_Btn.Name = "New_Btn"
        Me.New_Btn.Size = New System.Drawing.Size(126, 38)
        Me.New_Btn.TabIndex = 650
        Me.New_Btn.Text = "جديـــد"
        Me.New_Btn.UseVisualStyleBackColor = False
        '
        'UPDATE_btn
        '
        Me.UPDATE_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UPDATE_btn.BackColor = System.Drawing.Color.White
        Me.UPDATE_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.UPDATE_btn.Enabled = False
        Me.UPDATE_btn.FlatAppearance.BorderSize = 0
        Me.UPDATE_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.UPDATE_btn.Location = New System.Drawing.Point(130, 533)
        Me.UPDATE_btn.Margin = New System.Windows.Forms.Padding(4, 8, 4, 8)
        Me.UPDATE_btn.Name = "UPDATE_btn"
        Me.UPDATE_btn.Size = New System.Drawing.Size(126, 38)
        Me.UPDATE_btn.TabIndex = 649
        Me.UPDATE_btn.Text = "تعديل"
        Me.UPDATE_btn.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(848, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 21)
        Me.Label2.TabIndex = 652
        Me.Label2.Text = "القيمة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Val_txt
        '
        Me.Val_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Val_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Val_txt.Location = New System.Drawing.Point(639, 121)
        Me.Val_txt.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.Val_txt.Name = "Val_txt"
        Me.Val_txt.Size = New System.Drawing.Size(206, 29)
        Me.Val_txt.TabIndex = 651
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.AG_Cm)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Val_txt)
        Me.Panel1.Controls.Add(Me.DateTimePickerDATE)
        Me.Panel1.Controls.Add(Me.StateComboBox)
        Me.Panel1.Controls.Add(Me.NOTES_TXT)
        Me.Panel1.Controls.Add(Me.Start_txt)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Enabled = False
        Me.Panel1.Location = New System.Drawing.Point(2, 40)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(956, 191)
        Me.Panel1.TabIndex = 669
        '
        'AG_Cm
        '
        Me.AG_Cm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AG_Cm.CancelSearchImage = CType(resources.GetObject("AG_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.AG_Cm.Location = New System.Drawing.Point(483, 10)
        Me.AG_Cm.Name = "AG_Cm"
        Me.AG_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Cm.Size = New System.Drawing.Size(362, 34)
        Me.AG_Cm.SQL_Column = "AG_NAME"
        Me.AG_Cm.SQL_ID = "AG_ID"
        Me.AG_Cm.SQL_IsNumericSearchField = False
        Me.AG_Cm.SQL_ListSize = 200
        Me.AG_Cm.SQL_NumberOfRows = 200
        Me.AG_Cm.SQL_OrderByField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField_WHERE = ""
        Me.AG_Cm.SQL_Table = "Agents_Emp_V"
        Me.AG_Cm.TabIndex = 672
        Me.AG_Cm.TextMaxLength = 250
        Me.AG_Cm.Textt = ""
        '
        'Cancel_Btn
        '
        Me.Cancel_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Cancel_Btn.BackColor = System.Drawing.Color.White
        Me.Cancel_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cancel_Btn.Enabled = False
        Me.Cancel_Btn.FlatAppearance.BorderSize = 0
        Me.Cancel_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Btn.Location = New System.Drawing.Point(2, 533)
        Me.Cancel_Btn.Margin = New System.Windows.Forms.Padding(4, 8, 4, 8)
        Me.Cancel_Btn.Name = "Cancel_Btn"
        Me.Cancel_Btn.Size = New System.Drawing.Size(126, 38)
        Me.Cancel_Btn.TabIndex = 670
        Me.Cancel_Btn.Tag = "DELETE"
        Me.Cancel_Btn.Text = "إلغاء"
        Me.Cancel_Btn.UseVisualStyleBackColor = False
        '
        'Emp_OtherValues
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(959, 578)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Cancel_Btn)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.New_Btn)
        Me.Controls.Add(Me.UPDATE_btn)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.SaveButton)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.Name = "Emp_OtherValues"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "المكافئات والخصومات"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents HeaderCloseBtn As System.Windows.Forms.Button

    Friend WithEvents StateComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePickerDATE As System.Windows.Forms.DateTimePicker
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents NOTES_TXT As System.Windows.Forms.TextBox
    Friend WithEvents Start_txt As System.Windows.Forms.TextBox
    Friend WithEvents New_Btn As System.Windows.Forms.Button
    Friend WithEvents UPDATE_btn As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Val_txt As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Cancel_Btn As System.Windows.Forms.Button
    Friend WithEvents VoidLb As System.Windows.Forms.Label
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UserName_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Stuts_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VAL_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NOTES_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents is_Void_CL As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ValType_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AG_Cm As resturant.FSearch_Filter
End Class