<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BillsMenu
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Show_Unpied_CH = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.B1 = New System.Windows.Forms.Button()
        Me.ClearNumBtn = New System.Windows.Forms.Button()
        Me.B0 = New System.Windows.Forms.Button()
        Me.B6 = New System.Windows.Forms.Button()
        Me.B5 = New System.Windows.Forms.Button()
        Me.B9 = New System.Windows.Forms.Button()
        Me.B7 = New System.Windows.Forms.Button()
        Me.B3 = New System.Windows.Forms.Button()
        Me.B4 = New System.Windows.Forms.Button()
        Me.B8 = New System.Windows.Forms.Button()
        Me.B2 = New System.Windows.Forms.Button()
        Me.SB_SearchTextBox = New System.Windows.Forms.TextBox()
        Me.BillsGrid = New MetroFramework.Controls.MetroGrid()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_Type_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_Pr_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AG_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.T_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_Status_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isCancel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Search_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Show_Bill = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.BillsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Show_Unpied_CH
        '
        Me.Show_Unpied_CH.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Show_Unpied_CH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_Unpied_CH.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_Unpied_CH.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Show_Unpied_CH.Location = New System.Drawing.Point(494, 6)
        Me.Show_Unpied_CH.Name = "Show_Unpied_CH"
        Me.Show_Unpied_CH.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Show_Unpied_CH.Size = New System.Drawing.Size(141, 25)
        Me.Show_Unpied_CH.TabIndex = 547
        Me.Show_Unpied_CH.Text = "الغير مدفوعة"
        Me.Show_Unpied_CH.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.B1)
        Me.Panel1.Controls.Add(Me.ClearNumBtn)
        Me.Panel1.Controls.Add(Me.B0)
        Me.Panel1.Controls.Add(Me.B6)
        Me.Panel1.Controls.Add(Me.B5)
        Me.Panel1.Controls.Add(Me.B9)
        Me.Panel1.Controls.Add(Me.B7)
        Me.Panel1.Controls.Add(Me.B3)
        Me.Panel1.Controls.Add(Me.B4)
        Me.Panel1.Controls.Add(Me.B8)
        Me.Panel1.Controls.Add(Me.B2)
        Me.Panel1.Location = New System.Drawing.Point(3, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(470, 38)
        Me.Panel1.TabIndex = 544
        '
        'B1
        '
        Me.B1.BackColor = System.Drawing.SystemColors.Control
        Me.B1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B1.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B1.ForeColor = System.Drawing.Color.Firebrick
        Me.B1.Location = New System.Drawing.Point(388, 2)
        Me.B1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B1.Name = "B1"
        Me.B1.Size = New System.Drawing.Size(40, 33)
        Me.B1.TabIndex = 543
        Me.B1.Text = "1"
        Me.B1.UseVisualStyleBackColor = False
        '
        'ClearNumBtn
        '
        Me.ClearNumBtn.BackColor = System.Drawing.SystemColors.Control
        Me.ClearNumBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ClearNumBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ClearNumBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClearNumBtn.ForeColor = System.Drawing.Color.Firebrick
        Me.ClearNumBtn.Location = New System.Drawing.Point(429, 2)
        Me.ClearNumBtn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ClearNumBtn.Name = "ClearNumBtn"
        Me.ClearNumBtn.Size = New System.Drawing.Size(40, 33)
        Me.ClearNumBtn.TabIndex = 537
        Me.ClearNumBtn.Text = "C"
        Me.ClearNumBtn.UseVisualStyleBackColor = False
        '
        'B0
        '
        Me.B0.BackColor = System.Drawing.SystemColors.Control
        Me.B0.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B0.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B0.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B0.ForeColor = System.Drawing.Color.Firebrick
        Me.B0.Location = New System.Drawing.Point(2, 2)
        Me.B0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B0.Name = "B0"
        Me.B0.Size = New System.Drawing.Size(40, 33)
        Me.B0.TabIndex = 552
        Me.B0.Text = "0"
        Me.B0.UseVisualStyleBackColor = False
        '
        'B6
        '
        Me.B6.BackColor = System.Drawing.SystemColors.Control
        Me.B6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B6.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B6.ForeColor = System.Drawing.Color.Firebrick
        Me.B6.Location = New System.Drawing.Point(174, 2)
        Me.B6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B6.Name = "B6"
        Me.B6.Size = New System.Drawing.Size(40, 33)
        Me.B6.TabIndex = 548
        Me.B6.Text = "6"
        Me.B6.UseVisualStyleBackColor = False
        '
        'B5
        '
        Me.B5.BackColor = System.Drawing.SystemColors.Control
        Me.B5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B5.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B5.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B5.ForeColor = System.Drawing.Color.Firebrick
        Me.B5.Location = New System.Drawing.Point(217, 2)
        Me.B5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B5.Name = "B5"
        Me.B5.Size = New System.Drawing.Size(40, 33)
        Me.B5.TabIndex = 547
        Me.B5.Text = "5"
        Me.B5.UseVisualStyleBackColor = False
        '
        'B9
        '
        Me.B9.BackColor = System.Drawing.SystemColors.Control
        Me.B9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B9.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B9.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B9.ForeColor = System.Drawing.Color.Firebrick
        Me.B9.Location = New System.Drawing.Point(45, 2)
        Me.B9.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B9.Name = "B9"
        Me.B9.Size = New System.Drawing.Size(40, 33)
        Me.B9.TabIndex = 551
        Me.B9.Text = "9"
        Me.B9.UseVisualStyleBackColor = False
        '
        'B7
        '
        Me.B7.BackColor = System.Drawing.SystemColors.Control
        Me.B7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B7.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B7.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B7.ForeColor = System.Drawing.Color.Firebrick
        Me.B7.Location = New System.Drawing.Point(131, 2)
        Me.B7.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B7.Name = "B7"
        Me.B7.Size = New System.Drawing.Size(40, 33)
        Me.B7.TabIndex = 549
        Me.B7.Text = "7"
        Me.B7.UseVisualStyleBackColor = False
        '
        'B3
        '
        Me.B3.BackColor = System.Drawing.SystemColors.Control
        Me.B3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B3.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B3.ForeColor = System.Drawing.Color.Firebrick
        Me.B3.Location = New System.Drawing.Point(303, 2)
        Me.B3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B3.Name = "B3"
        Me.B3.Size = New System.Drawing.Size(40, 33)
        Me.B3.TabIndex = 545
        Me.B3.Text = "3"
        Me.B3.UseVisualStyleBackColor = False
        '
        'B4
        '
        Me.B4.BackColor = System.Drawing.SystemColors.Control
        Me.B4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B4.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B4.ForeColor = System.Drawing.Color.Firebrick
        Me.B4.Location = New System.Drawing.Point(260, 2)
        Me.B4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B4.Name = "B4"
        Me.B4.Size = New System.Drawing.Size(40, 33)
        Me.B4.TabIndex = 546
        Me.B4.Text = "4"
        Me.B4.UseVisualStyleBackColor = False
        '
        'B8
        '
        Me.B8.BackColor = System.Drawing.SystemColors.Control
        Me.B8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B8.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B8.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B8.ForeColor = System.Drawing.Color.Firebrick
        Me.B8.Location = New System.Drawing.Point(88, 2)
        Me.B8.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B8.Name = "B8"
        Me.B8.Size = New System.Drawing.Size(40, 33)
        Me.B8.TabIndex = 550
        Me.B8.Text = "8"
        Me.B8.UseVisualStyleBackColor = False
        '
        'B2
        '
        Me.B2.BackColor = System.Drawing.SystemColors.Control
        Me.B2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B2.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B2.ForeColor = System.Drawing.Color.Firebrick
        Me.B2.Location = New System.Drawing.Point(345, 2)
        Me.B2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B2.Name = "B2"
        Me.B2.Size = New System.Drawing.Size(40, 33)
        Me.B2.TabIndex = 544
        Me.B2.Text = "2"
        Me.B2.UseVisualStyleBackColor = False
        '
        'SB_SearchTextBox
        '
        Me.SB_SearchTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SB_SearchTextBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SB_SearchTextBox.Location = New System.Drawing.Point(638, 3)
        Me.SB_SearchTextBox.Margin = New System.Windows.Forms.Padding(5)
        Me.SB_SearchTextBox.Name = "SB_SearchTextBox"
        Me.SB_SearchTextBox.Size = New System.Drawing.Size(277, 33)
        Me.SB_SearchTextBox.TabIndex = 545
        Me.SB_SearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BillsGrid
        '
        Me.BillsGrid.AllowUserToAddRows = False
        Me.BillsGrid.AllowUserToDeleteRows = False
        Me.BillsGrid.AllowUserToResizeRows = False
        Me.BillsGrid.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BillsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.BillsGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BillsGrid.CausesValidation = False
        Me.BillsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.BillsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillsGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.BillsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.BillsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.B_Type_CL, Me.B_Pr_ID_CL, Me.AG_Name_CL, Me.T_Name_CL, Me.Date_CL, Me.B_Status_CL, Me.isCancel, Me.Total_CL, Me.Search_CL, Me.Show_Bill, Me.Column1})
        Me.BillsGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.BillsGrid.DefaultCellStyle = DataGridViewCellStyle3
        Me.BillsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.BillsGrid.EnableHeadersVisualStyles = False
        Me.BillsGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BillsGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillsGrid.Location = New System.Drawing.Point(5, 41)
        Me.BillsGrid.MultiSelect = False
        Me.BillsGrid.Name = "BillsGrid"
        Me.BillsGrid.ReadOnly = True
        Me.BillsGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BillsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillsGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.BillsGrid.RowHeadersVisible = False
        Me.BillsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillsGrid.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.BillsGrid.RowTemplate.Height = 35
        Me.BillsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.BillsGrid.Size = New System.Drawing.Size(910, 385)
        Me.BillsGrid.TabIndex = 546
        Me.BillsGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "ر.الآلي"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'B_Type_CL
        '
        Me.B_Type_CL.DataPropertyName = "S_Bills_Type"
        Me.B_Type_CL.HeaderText = "نوع الفاتورة"
        Me.B_Type_CL.Name = "B_Type_CL"
        Me.B_Type_CL.ReadOnly = True
        Me.B_Type_CL.Visible = False
        '
        'B_Pr_ID_CL
        '
        Me.B_Pr_ID_CL.DataPropertyName = "S_Bill_Pr_ID"
        Me.B_Pr_ID_CL.FillWeight = 63.22473!
        Me.B_Pr_ID_CL.HeaderText = "الرقم"
        Me.B_Pr_ID_CL.Name = "B_Pr_ID_CL"
        Me.B_Pr_ID_CL.ReadOnly = True
        '
        'AG_Name_CL
        '
        Me.AG_Name_CL.DataPropertyName = "AG_Name"
        Me.AG_Name_CL.FillWeight = 135.1069!
        Me.AG_Name_CL.HeaderText = "الزبون"
        Me.AG_Name_CL.Name = "AG_Name_CL"
        Me.AG_Name_CL.ReadOnly = True
        '
        'T_Name_CL
        '
        Me.T_Name_CL.DataPropertyName = "T_Name"
        Me.T_Name_CL.FillWeight = 79.11108!
        Me.T_Name_CL.HeaderText = "الطاولـة"
        Me.T_Name_CL.Name = "T_Name_CL"
        Me.T_Name_CL.ReadOnly = True
        '
        'Date_CL
        '
        Me.Date_CL.DataPropertyName = "Date"
        Me.Date_CL.FillWeight = 79.11108!
        Me.Date_CL.HeaderText = "التاريخ"
        Me.Date_CL.Name = "Date_CL"
        Me.Date_CL.ReadOnly = True
        '
        'B_Status_CL
        '
        Me.B_Status_CL.DataPropertyName = "PIED_Name"
        Me.B_Status_CL.FillWeight = 79.11108!
        Me.B_Status_CL.HeaderText = "الحالـة"
        Me.B_Status_CL.Name = "B_Status_CL"
        Me.B_Status_CL.ReadOnly = True
        '
        'isCancel
        '
        Me.isCancel.DataPropertyName = "isVoid"
        Me.isCancel.FillWeight = 79.11108!
        Me.isCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.isCancel.HeaderText = "ملغية"
        Me.isCancel.Name = "isCancel"
        Me.isCancel.ReadOnly = True
        '
        'Total_CL
        '
        Me.Total_CL.DataPropertyName = "Pure"
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.Total_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.Total_CL.FillWeight = 63.22473!
        Me.Total_CL.HeaderText = "الإجمالي"
        Me.Total_CL.Name = "Total_CL"
        Me.Total_CL.ReadOnly = True
        '
        'Search_CL
        '
        Me.Search_CL.DataPropertyName = "Search"
        Me.Search_CL.HeaderText = "Search"
        Me.Search_CL.Name = "Search_CL"
        Me.Search_CL.ReadOnly = True
        Me.Search_CL.Visible = False
        '
        'Show_Bill
        '
        Me.Show_Bill.FillWeight = 50.0!
        Me.Show_Bill.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_Bill.HeaderText = ""
        Me.Show_Bill.Name = "Show_Bill"
        Me.Show_Bill.ReadOnly = True
        Me.Show_Bill.Text = "فتح"
        Me.Show_Bill.UseColumnTextForButtonValue = True
        '
        'Column1
        '
        Me.Column1.FillWeight = 50.0!
        Me.Column1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Column1.HeaderText = ""
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Text = "الأصناف"
        Me.Column1.UseColumnTextForButtonValue = True
        '
        'BillsMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(920, 430)
        Me.Controls.Add(Me.Show_Unpied_CH)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.SB_SearchTextBox)
        Me.Controls.Add(Me.BillsGrid)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "BillsMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BillsMenu"
        Me.Panel1.ResumeLayout(False)
        CType(Me.BillsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Show_Unpied_CH As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents B1 As System.Windows.Forms.Button
    Friend WithEvents ClearNumBtn As System.Windows.Forms.Button
    Friend WithEvents B0 As System.Windows.Forms.Button
    Friend WithEvents B6 As System.Windows.Forms.Button
    Friend WithEvents B5 As System.Windows.Forms.Button
    Friend WithEvents B9 As System.Windows.Forms.Button
    Friend WithEvents B7 As System.Windows.Forms.Button
    Friend WithEvents B3 As System.Windows.Forms.Button
    Friend WithEvents B4 As System.Windows.Forms.Button
    Friend WithEvents B8 As System.Windows.Forms.Button
    Friend WithEvents B2 As System.Windows.Forms.Button
    Friend WithEvents SB_SearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents BillsGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents B_Type_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents B_Pr_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AG_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents T_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents B_Status_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents isCancel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Total_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Search_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Show_Bill As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewButtonColumn
End Class
