<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Stores_ImmediateOrder
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Stores_ImmediateOrder))
        Me.ST_To_cm = New System.Windows.Forms.ComboBox()
        Me.RemoveCatButton = New System.Windows.Forms.Button()
        Me.ADDCatButton = New System.Windows.Forms.Button()
        Me.ST_cm = New System.Windows.Forms.ComboBox()
        Me.IM_Show_CxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DateTimeEx = New System.Windows.Forms.DateTimePicker()
        Me.NewButton = New System.Windows.Forms.Button()
        Me.Save_butt = New System.Windows.Forms.Button()
        Me.QTY_Error = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Name_Error = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.AGMetroGrid = New MetroFramework.Controls.MetroGrid()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bill_IMID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_TO_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMNUM_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EX_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Valid_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMUnit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SPrice_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isDepended_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_F_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_To_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.User_Name_lb = New System.Windows.Forms.Label()
        Me.IM_Count_LB = New System.Windows.Forms.Label()
        Me.IM_Qty_LB = New System.Windows.Forms.Label()
        Me.Bill_ID_Txt = New System.Windows.Forms.TextBox()
        Me.Up_Bill_btn = New System.Windows.Forms.Button()
        Me.Down_Bill_btn = New System.Windows.Forms.Button()
        Me.VoidLb = New System.Windows.Forms.Label()
        Me.Pure_txt = New System.Windows.Forms.TextBox()
        Me.Notes_txt = New System.Windows.Forms.TextBox()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.Delete_butt = New System.Windows.Forms.Button()
        Me.DGV_Control_btn = New System.Windows.Forms.Button()
        Me.Edit_butt = New System.Windows.Forms.Button()
        Me.Print_btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.MetroToolTip2 = New MetroFramework.Components.MetroToolTip()
        Me.is_Recived_LB = New System.Windows.Forms.Label()
        Me.Pure_SP_txt = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.IM_Show_CxtMStrip.SuspendLayout()
        CType(Me.QTY_Error, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Name_Error, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ST_To_cm
        '
        Me.ST_To_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_To_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_To_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_To_cm.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ST_To_cm.FormattingEnabled = True
        Me.ST_To_cm.Location = New System.Drawing.Point(3, 7)
        Me.ST_To_cm.Name = "ST_To_cm"
        Me.ST_To_cm.Size = New System.Drawing.Size(260, 27)
        Me.ST_To_cm.TabIndex = 653
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
        Me.RemoveCatButton.Image = Global.resturant.My.Resources.Resources.IM_REMOVE
        Me.RemoveCatButton.Location = New System.Drawing.Point(825, 340)
        Me.RemoveCatButton.Name = "RemoveCatButton"
        Me.RemoveCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RemoveCatButton.Size = New System.Drawing.Size(48, 196)
        Me.RemoveCatButton.TabIndex = 651
        Me.RemoveCatButton.TabStop = False
        Me.RemoveCatButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RemoveCatButton.UseVisualStyleBackColor = False
        '
        'ADDCatButton
        '
        Me.ADDCatButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ADDCatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ADDCatButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADDCatButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ADDCatButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ADDCatButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADDCatButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ADDCatButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ADDCatButton.Image = Global.resturant.My.Resources.Resources.IM_ADD
        Me.ADDCatButton.Location = New System.Drawing.Point(825, 143)
        Me.ADDCatButton.Name = "ADDCatButton"
        Me.ADDCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ADDCatButton.Size = New System.Drawing.Size(48, 196)
        Me.ADDCatButton.TabIndex = 652
        Me.ADDCatButton.TabStop = False
        Me.ADDCatButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ADDCatButton.UseVisualStyleBackColor = False
        '
        'ST_cm
        '
        Me.ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_cm.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(2, 7)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.Size = New System.Drawing.Size(260, 27)
        Me.ST_cm.TabIndex = 649
        '
        'IM_Show_CxtMStrip
        '
        Me.IM_Show_CxtMStrip.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IM_Show_CxtMStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.IM_Show_CxtMStrip.Name = "IM_ContextMenuStrip"
        Me.IM_Show_CxtMStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_Show_CxtMStrip.Size = New System.Drawing.Size(211, 32)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.resturant.My.Resources.Resources.iconfinder_search_126577
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(210, 28)
        Me.ToolStripMenuItem1.Text = "عرض تفاصيل الصنف"
        '
        'DateTimeEx
        '
        Me.DateTimeEx.CustomFormat = "dd/MM/yyyy"
        Me.DateTimeEx.Font = New System.Drawing.Font("Times New Roman", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimeEx.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimeEx.Location = New System.Drawing.Point(1, 5)
        Me.DateTimeEx.Name = "DateTimeEx"
        Me.DateTimeEx.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTimeEx.RightToLeftLayout = True
        Me.DateTimeEx.Size = New System.Drawing.Size(199, 29)
        Me.DateTimeEx.TabIndex = 574
        '
        'NewButton
        '
        Me.NewButton.BackColor = System.Drawing.Color.White
        Me.NewButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NewButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NewButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.NewButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.NewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewButton.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.NewButton.ForeColor = System.Drawing.Color.Black
        Me.NewButton.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools
        Me.NewButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NewButton.Location = New System.Drawing.Point(874, 89)
        Me.NewButton.Name = "NewButton"
        Me.NewButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NewButton.Size = New System.Drawing.Size(130, 40)
        Me.NewButton.TabIndex = 563
        Me.NewButton.Text = " فاتورة جديدة F1"
        Me.NewButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NewButton.UseVisualStyleBackColor = False
        '
        'Save_butt
        '
        Me.Save_butt.BackColor = System.Drawing.Color.White
        Me.Save_butt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Save_butt.Enabled = False
        Me.Save_butt.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Save_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Save_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_butt.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Save_butt.ForeColor = System.Drawing.Color.Black
        Me.Save_butt.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__1_
        Me.Save_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Save_butt.Location = New System.Drawing.Point(874, 132)
        Me.Save_butt.Name = "Save_butt"
        Me.Save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Save_butt.Size = New System.Drawing.Size(130, 40)
        Me.Save_butt.TabIndex = 585
        Me.Save_butt.Text = "حفظ F12"
        Me.Save_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Save_butt.UseVisualStyleBackColor = False
        '
        'QTY_Error
        '
        Me.QTY_Error.ContainerControl = Me
        Me.QTY_Error.RightToLeft = True
        '
        'Name_Error
        '
        Me.Name_Error.ContainerControl = Me
        '
        'AGMetroGrid
        '
        Me.AGMetroGrid.AllowUserToAddRows = False
        Me.AGMetroGrid.AllowUserToDeleteRows = False
        Me.AGMetroGrid.AllowUserToResizeRows = False
        Me.AGMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.AGMetroGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AGMetroGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.AGMetroGrid.CausesValidation = False
        Me.AGMetroGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.AGMetroGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AGMetroGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.AGMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AGMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.B_T_ID_CL, Me.Bill_IMID_CL, Me.Date_CL, Me.ST_Name_CL, Me.ST_TO_CL, Me.Barcode_CL, Me.IMNUM_CL, Me.EX_Name_CL, Me.D_Valid_CL, Me.IMUnit_CL, Me.QTY_CL, Me.Price_CL, Me.SPrice_CL, Me.Total_CL, Me.isDepended_CL, Me.ST_F_ID_CL, Me.ST_To_ID_CL})
        Me.AGMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.AGMetroGrid.DefaultCellStyle = DataGridViewCellStyle4
        Me.AGMetroGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.AGMetroGrid.EnableHeadersVisualStyles = False
        Me.AGMetroGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.AGMetroGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AGMetroGrid.Location = New System.Drawing.Point(2, 88)
        Me.AGMetroGrid.MultiSelect = False
        Me.AGMetroGrid.Name = "AGMetroGrid"
        Me.AGMetroGrid.ReadOnly = True
        Me.AGMetroGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AGMetroGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.AGMetroGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        Me.AGMetroGrid.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.AGMetroGrid.RowTemplate.Height = 40
        Me.AGMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGMetroGrid.Size = New System.Drawing.Size(822, 526)
        Me.AGMetroGrid.TabIndex = 588
        Me.AGMetroGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "رقم الآلي"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        Me.T_ID_CL.Width = 105
        '
        'B_T_ID_CL
        '
        Me.B_T_ID_CL.DataPropertyName = "B_T_ID"
        Me.B_T_ID_CL.HeaderText = "ت.الفاتورة"
        Me.B_T_ID_CL.Name = "B_T_ID_CL"
        Me.B_T_ID_CL.ReadOnly = True
        Me.B_T_ID_CL.Visible = False
        Me.B_T_ID_CL.Width = 111
        '
        'Bill_IMID_CL
        '
        Me.Bill_IMID_CL.DataPropertyName = "IM_ID"
        Me.Bill_IMID_CL.HeaderText = "ر.الصنف"
        Me.Bill_IMID_CL.Name = "Bill_IMID_CL"
        Me.Bill_IMID_CL.ReadOnly = True
        Me.Bill_IMID_CL.Visible = False
        Me.Bill_IMID_CL.Width = 93
        '
        'Date_CL
        '
        Me.Date_CL.DataPropertyName = "Date"
        Me.Date_CL.HeaderText = "تاريخ"
        Me.Date_CL.Name = "Date_CL"
        Me.Date_CL.ReadOnly = True
        Me.Date_CL.Visible = False
        Me.Date_CL.Width = 69
        '
        'ST_Name_CL
        '
        Me.ST_Name_CL.DataPropertyName = "ST_F"
        Me.ST_Name_CL.HeaderText = "من"
        Me.ST_Name_CL.Name = "ST_Name_CL"
        Me.ST_Name_CL.ReadOnly = True
        Me.ST_Name_CL.Width = 56
        '
        'ST_TO_CL
        '
        Me.ST_TO_CL.DataPropertyName = "ST_To"
        Me.ST_TO_CL.HeaderText = "إلى"
        Me.ST_TO_CL.Name = "ST_TO_CL"
        Me.ST_TO_CL.ReadOnly = True
        Me.ST_TO_CL.Width = 62
        '
        'Barcode_CL
        '
        Me.Barcode_CL.DataPropertyName = "Barcode"
        Me.Barcode_CL.HeaderText = "باركود"
        Me.Barcode_CL.Name = "Barcode_CL"
        Me.Barcode_CL.ReadOnly = True
        Me.Barcode_CL.Width = 81
        '
        'IMNUM_CL
        '
        Me.IMNUM_CL.DataPropertyName = "IM_NUM"
        Me.IMNUM_CL.HeaderText = "الرقم"
        Me.IMNUM_CL.Name = "IMNUM_CL"
        Me.IMNUM_CL.ReadOnly = True
        Me.IMNUM_CL.Width = 75
        '
        'EX_Name_CL
        '
        Me.EX_Name_CL.DataPropertyName = "item_name"
        Me.EX_Name_CL.FillWeight = 112.3096!
        Me.EX_Name_CL.HeaderText = "الصنــف"
        Me.EX_Name_CL.Name = "EX_Name_CL"
        Me.EX_Name_CL.ReadOnly = True
        Me.EX_Name_CL.Width = 92
        '
        'D_Valid_CL
        '
        Me.D_Valid_CL.DataPropertyName = "D_Vaild"
        Me.D_Valid_CL.HeaderText = "الصلاحية"
        Me.D_Valid_CL.Name = "D_Valid_CL"
        Me.D_Valid_CL.ReadOnly = True
        Me.D_Valid_CL.Width = 97
        '
        'IMUnit_CL
        '
        Me.IMUnit_CL.DataPropertyName = "U_Name"
        Me.IMUnit_CL.HeaderText = "الوحدة"
        Me.IMUnit_CL.Name = "IMUnit_CL"
        Me.IMUnit_CL.ReadOnly = True
        Me.IMUnit_CL.Width = 85
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        Me.QTY_CL.FillWeight = 112.3096!
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        Me.QTY_CL.Width = 86
        '
        'Price_CL
        '
        Me.Price_CL.DataPropertyName = "Price"
        DataGridViewCellStyle2.Format = "N3"
        Me.Price_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.Price_CL.FillWeight = 112.3096!
        Me.Price_CL.HeaderText = "السعر"
        Me.Price_CL.Name = "Price_CL"
        Me.Price_CL.ReadOnly = True
        Me.Price_CL.Width = 77
        '
        'SPrice_CL
        '
        Me.SPrice_CL.DataPropertyName = "SPrice"
        Me.SPrice_CL.HeaderText = "البيع"
        Me.SPrice_CL.Name = "SPrice_CL"
        Me.SPrice_CL.ReadOnly = True
        Me.SPrice_CL.Width = 70
        '
        'Total_CL
        '
        Me.Total_CL.DataPropertyName = "T_Price"
        DataGridViewCellStyle3.Format = "N3"
        Me.Total_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Total_CL.FillWeight = 112.3096!
        Me.Total_CL.HeaderText = "الإجمالي"
        Me.Total_CL.Name = "Total_CL"
        Me.Total_CL.ReadOnly = True
        Me.Total_CL.Width = 99
        '
        'isDepended_CL
        '
        Me.isDepended_CL.DataPropertyName = "isDepended"
        Me.isDepended_CL.HeaderText = "معتمده"
        Me.isDepended_CL.Name = "isDepended_CL"
        Me.isDepended_CL.ReadOnly = True
        Me.isDepended_CL.Visible = False
        Me.isDepended_CL.Width = 93
        '
        'ST_F_ID_CL
        '
        Me.ST_F_ID_CL.DataPropertyName = "ST_F_ID"
        Me.ST_F_ID_CL.HeaderText = "ST_F_ID"
        Me.ST_F_ID_CL.Name = "ST_F_ID_CL"
        Me.ST_F_ID_CL.ReadOnly = True
        Me.ST_F_ID_CL.Visible = False
        Me.ST_F_ID_CL.Width = 98
        '
        'ST_To_ID_CL
        '
        Me.ST_To_ID_CL.DataPropertyName = "ST_To_ID"
        Me.ST_To_ID_CL.HeaderText = "ST_To_ID"
        Me.ST_To_ID_CL.Name = "ST_To_ID_CL"
        Me.ST_To_ID_CL.ReadOnly = True
        Me.ST_To_ID_CL.Visible = False
        Me.ST_To_ID_CL.Width = 106
        '
        'User_Name_lb
        '
        Me.User_Name_lb.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.User_Name_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.User_Name_lb.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.User_Name_lb.ForeColor = System.Drawing.Color.Blue
        Me.User_Name_lb.Location = New System.Drawing.Point(575, 670)
        Me.User_Name_lb.Name = "User_Name_lb"
        Me.User_Name_lb.Size = New System.Drawing.Size(331, 25)
        Me.User_Name_lb.TabIndex = 666
        Me.User_Name_lb.Text = "المستخدم"
        Me.User_Name_lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'IM_Count_LB
        '
        Me.IM_Count_LB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.IM_Count_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Count_LB.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Count_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.IM_Count_LB.Location = New System.Drawing.Point(382, 670)
        Me.IM_Count_LB.Name = "IM_Count_LB"
        Me.IM_Count_LB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IM_Count_LB.Size = New System.Drawing.Size(96, 25)
        Me.IM_Count_LB.TabIndex = 664
        Me.IM_Count_LB.Text = "المواد : 0"
        Me.IM_Count_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IM_Qty_LB
        '
        Me.IM_Qty_LB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.IM_Qty_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Qty_LB.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Qty_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.IM_Qty_LB.Location = New System.Drawing.Point(478, 670)
        Me.IM_Qty_LB.Name = "IM_Qty_LB"
        Me.IM_Qty_LB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IM_Qty_LB.Size = New System.Drawing.Size(96, 25)
        Me.IM_Qty_LB.TabIndex = 665
        Me.IM_Qty_LB.Text = "الكميات : 0"
        Me.IM_Qty_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Bill_ID_Txt
        '
        Me.Bill_ID_Txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Bill_ID_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Bill_ID_Txt.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bill_ID_Txt.ForeColor = System.Drawing.Color.Black
        Me.Bill_ID_Txt.Location = New System.Drawing.Point(29, 5)
        Me.Bill_ID_Txt.MaxLength = 250
        Me.Bill_ID_Txt.Name = "Bill_ID_Txt"
        Me.Bill_ID_Txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Bill_ID_Txt.Size = New System.Drawing.Size(89, 29)
        Me.Bill_ID_Txt.TabIndex = 657
        Me.Bill_ID_Txt.Text = "1234567"
        Me.Bill_ID_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Up_Bill_btn
        '
        Me.Up_Bill_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Up_Bill_btn.BackgroundImage = Global.resturant.My.Resources.Resources.iconfinder_up_3017922
        Me.Up_Bill_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Up_Bill_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Up_Bill_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Up_Bill_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Up_Bill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Up_Bill_btn.Font = New System.Drawing.Font("Arial Narrow", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Up_Bill_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Up_Bill_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Up_Bill_btn.Location = New System.Drawing.Point(119, 5)
        Me.Up_Bill_btn.Name = "Up_Bill_btn"
        Me.Up_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Up_Bill_btn.Size = New System.Drawing.Size(26, 29)
        Me.Up_Bill_btn.TabIndex = 655
        Me.Up_Bill_btn.TabStop = False
        Me.Up_Bill_btn.UseVisualStyleBackColor = False
        '
        'Down_Bill_btn
        '
        Me.Down_Bill_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Down_Bill_btn.BackgroundImage = Global.resturant.My.Resources.Resources.iconfinder_Down
        Me.Down_Bill_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Down_Bill_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Down_Bill_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Down_Bill_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Down_Bill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Down_Bill_btn.Font = New System.Drawing.Font("Arial Narrow", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Down_Bill_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Down_Bill_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Down_Bill_btn.Location = New System.Drawing.Point(2, 5)
        Me.Down_Bill_btn.Name = "Down_Bill_btn"
        Me.Down_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Down_Bill_btn.Size = New System.Drawing.Size(26, 29)
        Me.Down_Bill_btn.TabIndex = 656
        Me.Down_Bill_btn.TabStop = False
        Me.Down_Bill_btn.UseVisualStyleBackColor = False
        '
        'VoidLb
        '
        Me.VoidLb.BackColor = System.Drawing.Color.IndianRed
        Me.VoidLb.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.VoidLb.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.VoidLb.Location = New System.Drawing.Point(147, 42)
        Me.VoidLb.Name = "VoidLb"
        Me.VoidLb.Size = New System.Drawing.Size(140, 43)
        Me.VoidLb.TabIndex = 658
        Me.VoidLb.Text = "فاتورة ملغية"
        Me.VoidLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.VoidLb.Visible = False
        '
        'Pure_txt
        '
        Me.Pure_txt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Pure_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pure_txt.Font = New System.Drawing.Font("Stencil", 19.0!)
        Me.Pure_txt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Pure_txt.Location = New System.Drawing.Point(184, 655)
        Me.Pure_txt.MaxLength = 200
        Me.Pure_txt.Name = "Pure_txt"
        Me.Pure_txt.ReadOnly = True
        Me.Pure_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Pure_txt.Size = New System.Drawing.Size(127, 38)
        Me.Pure_txt.TabIndex = 664
        Me.Pure_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Notes_txt
        '
        Me.Notes_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Notes_txt.Font = New System.Drawing.Font("JF Flat", 11.0!)
        Me.Notes_txt.ForeColor = System.Drawing.Color.Black
        Me.Notes_txt.Location = New System.Drawing.Point(1, 616)
        Me.Notes_txt.MaxLength = 250
        Me.Notes_txt.Name = "Notes_txt"
        Me.Notes_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Notes_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Notes_txt.Size = New System.Drawing.Size(872, 33)
        Me.Notes_txt.TabIndex = 665
        '
        'SearchButton
        '
        Me.SearchButton.BackColor = System.Drawing.Color.White
        Me.SearchButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SearchButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SearchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.SearchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchButton.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SearchButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SearchButton.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.SearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SearchButton.Location = New System.Drawing.Point(874, 301)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SearchButton.Size = New System.Drawing.Size(130, 40)
        Me.SearchButton.TabIndex = 667
        Me.SearchButton.TabStop = False
        Me.SearchButton.Text = "بحث"
        Me.SearchButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SearchButton.UseVisualStyleBackColor = False
        '
        'Delete_butt
        '
        Me.Delete_butt.BackColor = System.Drawing.Color.White
        Me.Delete_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Delete_butt.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Delete_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Delete_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Delete_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Delete_butt.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Delete_butt.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__4_
        Me.Delete_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Delete_butt.Location = New System.Drawing.Point(874, 258)
        Me.Delete_butt.Name = "Delete_butt"
        Me.Delete_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Delete_butt.Size = New System.Drawing.Size(130, 40)
        Me.Delete_butt.TabIndex = 668
        Me.Delete_butt.Text = " إلغاء F4"
        Me.Delete_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Delete_butt.UseVisualStyleBackColor = False
        '
        'DGV_Control_btn
        '
        Me.DGV_Control_btn.BackColor = System.Drawing.Color.White
        Me.DGV_Control_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DGV_Control_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DGV_Control_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.DGV_Control_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DGV_Control_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DGV_Control_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DGV_Control_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DGV_Control_btn.Image = Global.resturant.My.Resources.Resources.iconfinder_menu_1814109
        Me.DGV_Control_btn.Location = New System.Drawing.Point(825, 89)
        Me.DGV_Control_btn.Name = "DGV_Control_btn"
        Me.DGV_Control_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DGV_Control_btn.Size = New System.Drawing.Size(48, 53)
        Me.DGV_Control_btn.TabIndex = 669
        Me.DGV_Control_btn.TabStop = False
        Me.DGV_Control_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MetroToolTip1.SetToolTip(Me.DGV_Control_btn, "عرض بيانات الجدول")
        Me.DGV_Control_btn.UseVisualStyleBackColor = False
        '
        'Edit_butt
        '
        Me.Edit_butt.BackColor = System.Drawing.Color.White
        Me.Edit_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Edit_butt.Enabled = False
        Me.Edit_butt.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Edit_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Edit_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Edit_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Edit_butt.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Edit_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Edit_butt.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__3_
        Me.Edit_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Edit_butt.Location = New System.Drawing.Point(874, 174)
        Me.Edit_butt.Name = "Edit_butt"
        Me.Edit_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Edit_butt.Size = New System.Drawing.Size(130, 40)
        Me.Edit_butt.TabIndex = 670
        Me.Edit_butt.TabStop = False
        Me.Edit_butt.Text = "تعديل F3"
        Me.Edit_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Edit_butt.UseVisualStyleBackColor = False
        '
        'Print_btn
        '
        Me.Print_btn.BackColor = System.Drawing.Color.White
        Me.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_btn.Enabled = False
        Me.Print_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_btn.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Print_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Print_btn.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__2_
        Me.Print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_btn.Location = New System.Drawing.Point(874, 216)
        Me.Print_btn.Name = "Print_btn"
        Me.Print_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_btn.Size = New System.Drawing.Size(130, 40)
        Me.Print_btn.TabIndex = 671
        Me.Print_btn.TabStop = False
        Me.Print_btn.Text = "طباعة F3"
        Me.Print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_btn.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(908, 657)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(93, 38)
        Me.ExitFormButton.TabIndex = 674
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("JF Flat", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(2, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label16.Size = New System.Drawing.Size(583, 40)
        Me.Label16.TabIndex = 675
        Me.Label16.Text = "تحويــل أصنــاف"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel10
        '
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Controls.Add(Me.Label3)
        Me.Panel10.Controls.Add(Me.DateTimeEx)
        Me.Panel10.Location = New System.Drawing.Point(586, 1)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(267, 40)
        Me.Panel10.TabIndex = 695
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(204, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 21)
        Me.Label3.TabIndex = 384
        Me.Label3.Text = "التاريخ :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel9
        '
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.Up_Bill_btn)
        Me.Panel9.Controls.Add(Me.Down_Bill_btn)
        Me.Panel9.Controls.Add(Me.Bill_ID_Txt)
        Me.Panel9.Location = New System.Drawing.Point(855, 1)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(149, 40)
        Me.Panel9.TabIndex = 694
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Controls.Add(Me.Label1)
        Me.Panel12.Controls.Add(Me.ST_To_cm)
        Me.Panel12.Location = New System.Drawing.Point(321, 42)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(341, 43)
        Me.Panel12.TabIndex = 704
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(264, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 21)
        Me.Label1.TabIndex = 646
        Me.Label1.Text = "إلى مخزن :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.ST_cm)
        Me.Panel1.Location = New System.Drawing.Point(663, 42)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(341, 43)
        Me.Panel1.TabIndex = 705
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(267, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 21)
        Me.Label7.TabIndex = 646
        Me.Label7.Text = "من مخزن :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(876, 623)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(62, 21)
        Me.Label27.TabIndex = 706
        Me.Label27.Text = "ملاحظة :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(313, 663)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 24)
        Me.Label2.TabIndex = 707
        Me.Label2.Text = "التكلفة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'MetroToolTip1
        '
        Me.MetroToolTip1.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip1.StyleManager = Nothing
        Me.MetroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'MetroToolTip2
        '
        Me.MetroToolTip2.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip2.StyleManager = Nothing
        Me.MetroToolTip2.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'is_Recived_LB
        '
        Me.is_Recived_LB.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.is_Recived_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.is_Recived_LB.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.is_Recived_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.is_Recived_LB.Location = New System.Drawing.Point(2, 42)
        Me.is_Recived_LB.Name = "is_Recived_LB"
        Me.is_Recived_LB.Size = New System.Drawing.Size(143, 43)
        Me.is_Recived_LB.TabIndex = 708
        Me.is_Recived_LB.Text = "تم إستلام البضاعة"
        Me.is_Recived_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.is_Recived_LB.Visible = False
        '
        'Pure_SP_txt
        '
        Me.Pure_SP_txt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Pure_SP_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pure_SP_txt.Font = New System.Drawing.Font("Stencil", 19.0!)
        Me.Pure_SP_txt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Pure_SP_txt.Location = New System.Drawing.Point(1, 654)
        Me.Pure_SP_txt.MaxLength = 200
        Me.Pure_SP_txt.Name = "Pure_SP_txt"
        Me.Pure_SP_txt.ReadOnly = True
        Me.Pure_SP_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Pure_SP_txt.Size = New System.Drawing.Size(127, 38)
        Me.Pure_SP_txt.TabIndex = 709
        Me.Pure_SP_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(134, 661)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 24)
        Me.Label10.TabIndex = 710
        Me.Label10.Text = "البيع"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Stores_ImmediateOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Pure_SP_txt)
        Me.Controls.Add(Me.is_Recived_LB)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel12)
        Me.Controls.Add(Me.User_Name_lb)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.IM_Count_LB)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.IM_Qty_LB)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Print_btn)
        Me.Controls.Add(Me.Edit_butt)
        Me.Controls.Add(Me.DGV_Control_btn)
        Me.Controls.Add(Me.Delete_butt)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.Notes_txt)
        Me.Controls.Add(Me.Pure_txt)
        Me.Controls.Add(Me.VoidLb)
        Me.Controls.Add(Me.AGMetroGrid)
        Me.Controls.Add(Me.NewButton)
        Me.Controls.Add(Me.RemoveCatButton)
        Me.Controls.Add(Me.Save_butt)
        Me.Controls.Add(Me.ADDCatButton)
        Me.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Stores_ImmediateOrder"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تحويل أصنـاف"
        Me.IM_Show_CxtMStrip.ResumeLayout(False)
        CType(Me.QTY_Error, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Name_Error, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NewButton As System.Windows.Forms.Button
    Friend WithEvents DateTimeEx As System.Windows.Forms.DateTimePicker
    Friend WithEvents Save_butt As System.Windows.Forms.Button
    Friend WithEvents QTY_Error As System.Windows.Forms.ErrorProvider
    Friend WithEvents Name_Error As System.Windows.Forms.ErrorProvider
    Friend WithEvents ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents RemoveCatButton As System.Windows.Forms.Button
    Friend WithEvents ADDCatButton As System.Windows.Forms.Button
    Friend WithEvents ST_To_cm As System.Windows.Forms.ComboBox
    Friend WithEvents AGMetroGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents VoidLb As System.Windows.Forms.Label
    Friend WithEvents Pure_txt As System.Windows.Forms.TextBox
    Friend WithEvents Bill_ID_Txt As System.Windows.Forms.TextBox
    Friend WithEvents Up_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Down_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents Delete_butt As System.Windows.Forms.Button
    Friend WithEvents User_Name_lb As System.Windows.Forms.Label
    Friend WithEvents IM_Count_LB As System.Windows.Forms.Label
    Friend WithEvents IM_Qty_LB As System.Windows.Forms.Label
    Friend WithEvents DGV_Control_btn As System.Windows.Forms.Button
    Friend WithEvents Edit_butt As System.Windows.Forms.Button
    Friend WithEvents Print_btn As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents IM_Show_CxtMStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
    Friend WithEvents MetroToolTip2 As MetroFramework.Components.MetroToolTip
    Friend WithEvents is_Recived_LB As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Pure_SP_txt As System.Windows.Forms.TextBox
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents B_T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bill_IMID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ST_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ST_TO_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMNUM_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EX_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Valid_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMUnit_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SPrice_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents isDepended_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ST_F_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ST_To_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
