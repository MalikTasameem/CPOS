<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_ORDERS_COMING
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_ORDERS_COMING))
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.AGMetroGrid = New System.Windows.Forms.DataGridView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.حذفالصفToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        Me.ExpandablePanel1 = New DevComponents.DotNetBar.ExpandablePanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MoveToBill_Btn = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.IM_Unit_cm = New System.Windows.Forms.ComboBox()
        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Details_Notes_txt = New System.Windows.Forms.TextBox()
        Me.Details_Date = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.USER_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.St_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DATE_OF_ARRIV_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DAYS_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MONTHS_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NOTES_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.USER_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_Cm = New resturant.FSearch_Filter()
        Me.QTY_txt = New resturant.F2FloatField()
        Me.IM_Cm = New resturant.FSearch_Filter()
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ExpandablePanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(1, 639)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(1006, 55)
        Me.ExitFormButton.TabIndex = 675
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'AGMetroGrid
        '
        Me.AGMetroGrid.AllowUserToAddRows = False
        Me.AGMetroGrid.AllowUserToDeleteRows = False
        Me.AGMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.AGMetroGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.AGMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AGMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.USER_ID_CL, Me.St_Name_CL, Me.item_name_CL, Me.Barcode_CL, Me.U_Name_CL, Me.QTY_CL, Me.DATE_OF_ARRIV_CL, Me.DAYS_CL, Me.MONTHS_CL, Me.NOTES_CL, Me.USER_NAME_CL})
        Me.AGMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AGMetroGrid.Location = New System.Drawing.Point(0, 61)
        Me.AGMetroGrid.MultiSelect = False
        Me.AGMetroGrid.Name = "AGMetroGrid"
        Me.AGMetroGrid.ReadOnly = True
        Me.AGMetroGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AGMetroGrid.RowHeadersVisible = False
        Me.AGMetroGrid.RowTemplate.ContextMenuStrip = Me.ContextMenuStrip1
        Me.AGMetroGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.AGMetroGrid.RowTemplate.Height = 30
        Me.AGMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGMetroGrid.Size = New System.Drawing.Size(1004, 577)
        Me.AGMetroGrid.TabIndex = 702
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.حذفالصفToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(147, 28)
        '
        'حذفالصفToolStripMenuItem
        '
        Me.حذفالصفToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.حذفالصفToolStripMenuItem.Name = "حذفالصفToolStripMenuItem"
        Me.حذفالصفToolStripMenuItem.Size = New System.Drawing.Size(146, 24)
        Me.حذفالصفToolStripMenuItem.Text = "حذف الصف"
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.Font = New System.Drawing.Font("Arial", 13.25!)
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(210, 32)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CMSearchTextBox.Size = New System.Drawing.Size(794, 28)
        Me.CMSearchTextBox.TabIndex = 703
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ExpandablePanel1
        '
        Me.ExpandablePanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel1.Controls.Add(Me.Label1)
        Me.ExpandablePanel1.Controls.Add(Me.ST_Cm)
        Me.ExpandablePanel1.Controls.Add(Me.QTY_txt)
        Me.ExpandablePanel1.Controls.Add(Me.MoveToBill_Btn)
        Me.ExpandablePanel1.Controls.Add(Me.Label18)
        Me.ExpandablePanel1.Controls.Add(Me.IM_Unit_cm)
        Me.ExpandablePanel1.Controls.Add(Me.IM_Cm)
        Me.ExpandablePanel1.Controls.Add(Me.Barcode_SH_txt)
        Me.ExpandablePanel1.Controls.Add(Me.Label13)
        Me.ExpandablePanel1.Controls.Add(Me.Label12)
        Me.ExpandablePanel1.Controls.Add(Me.Label7)
        Me.ExpandablePanel1.Controls.Add(Me.Label5)
        Me.ExpandablePanel1.Controls.Add(Me.Label3)
        Me.ExpandablePanel1.Controls.Add(Me.Details_Notes_txt)
        Me.ExpandablePanel1.Controls.Add(Me.Details_Date)
        Me.ExpandablePanel1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExpandablePanel1.Expanded = False
        Me.ExpandablePanel1.ExpandedBounds = New System.Drawing.Rectangle(0, 1, 1003, 362)
        Me.ExpandablePanel1.ExpandOnTitleClick = True
        Me.ExpandablePanel1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExpandablePanel1.Location = New System.Drawing.Point(0, 1)
        Me.ExpandablePanel1.Name = "ExpandablePanel1"
        Me.ExpandablePanel1.Size = New System.Drawing.Size(1003, 30)
        Me.ExpandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel1.Style.GradientAngle = 90
        Me.ExpandablePanel1.TabIndex = 740
        Me.ExpandablePanel1.ThemeAware = True
        Me.ExpandablePanel1.TitleHeight = 30
        Me.ExpandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.TitleStyle.BackColor1.Color = System.Drawing.Color.White
        Me.ExpandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner
        Me.ExpandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel1.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel1.TitleText = "إضافة طلبية"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(488, 114)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 19)
        Me.Label1.TabIndex = 749
        Me.Label1.Text = "المخزن"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MoveToBill_Btn
        '
        Me.MoveToBill_Btn.BackColor = System.Drawing.SystemColors.Control
        Me.MoveToBill_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.MoveToBill_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MoveToBill_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.MoveToBill_Btn.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold)
        Me.MoveToBill_Btn.ForeColor = System.Drawing.Color.DarkRed
        Me.MoveToBill_Btn.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.MoveToBill_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.MoveToBill_Btn.Location = New System.Drawing.Point(359, 287)
        Me.MoveToBill_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MoveToBill_Btn.Name = "MoveToBill_Btn"
        Me.MoveToBill_Btn.Size = New System.Drawing.Size(206, 59)
        Me.MoveToBill_Btn.TabIndex = 746
        Me.MoveToBill_Btn.Text = "إضافة للقائمــة"
        Me.MoveToBill_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MoveToBill_Btn.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label18.Location = New System.Drawing.Point(817, 115)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(52, 19)
        Me.Label18.TabIndex = 745
        Me.Label18.Text = "الوحدة :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IM_Unit_cm
        '
        Me.IM_Unit_cm.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.IM_Unit_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Unit_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IM_Unit_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Unit_cm.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.IM_Unit_cm.FormattingEnabled = True
        Me.IM_Unit_cm.Location = New System.Drawing.Point(691, 108)
        Me.IM_Unit_cm.Name = "IM_Unit_cm"
        Me.IM_Unit_cm.Size = New System.Drawing.Size(120, 30)
        Me.IM_Unit_cm.TabIndex = 744
        '
        'Barcode_SH_txt
        '
        Me.Barcode_SH_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Barcode_SH_txt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Times New Roman", 18.0!)
        Me.Barcode_SH_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_SH_txt.Location = New System.Drawing.Point(83, 34)
        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
        Me.Barcode_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Barcode_SH_txt.Size = New System.Drawing.Size(402, 35)
        Me.Barcode_SH_txt.TabIndex = 743
        Me.Barcode_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(814, 184)
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
        Me.Label12.Location = New System.Drawing.Point(644, 114)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(41, 19)
        Me.Label12.TabIndex = 740
        Me.Label12.Text = "الكمية"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(488, 42)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 19)
        Me.Label7.TabIndex = 739
        Me.Label7.Text = "باركود"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(951, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 19)
        Me.Label5.TabIndex = 738
        Me.Label5.Text = "الصنف"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(814, 149)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 19)
        Me.Label3.TabIndex = 737
        Me.Label3.Text = "موعد الوصــول"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Details_Notes_txt
        '
        Me.Details_Notes_txt.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Details_Notes_txt.Font = New System.Drawing.Font("Arial", 14.0!)
        Me.Details_Notes_txt.ForeColor = System.Drawing.Color.Black
        Me.Details_Notes_txt.Location = New System.Drawing.Point(83, 175)
        Me.Details_Notes_txt.MaxLength = 250
        Me.Details_Notes_txt.Multiline = True
        Me.Details_Notes_txt.Name = "Details_Notes_txt"
        Me.Details_Notes_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Details_Notes_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Details_Notes_txt.Size = New System.Drawing.Size(728, 106)
        Me.Details_Notes_txt.TabIndex = 736
        '
        'Details_Date
        '
        Me.Details_Date.CalendarFont = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Details_Date.CalendarMonthBackground = System.Drawing.SystemColors.Info
        Me.Details_Date.CalendarTitleBackColor = System.Drawing.SystemColors.Info
        Me.Details_Date.CalendarTrailingForeColor = System.Drawing.SystemColors.Info
        Me.Details_Date.CustomFormat = "dd/MM/yyyy"
        Me.Details_Date.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Details_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Details_Date.Location = New System.Drawing.Point(83, 143)
        Me.Details_Date.Name = "Details_Date"
        Me.Details_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Details_Date.RightToLeftLayout = True
        Me.Details_Date.Size = New System.Drawing.Size(728, 29)
        Me.Details_Date.TabIndex = 732
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(105, 32)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(103, 28)
        Me.Button1.TabIndex = 742
        Me.Button1.Text = "تحديث"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(1, 32)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(103, 28)
        Me.Button2.TabIndex = 743
        Me.Button2.Text = "طباعة"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        Me.T_ID_CL.Width = 36
        '
        'USER_ID_CL
        '
        Me.USER_ID_CL.DataPropertyName = "USER_ID"
        Me.USER_ID_CL.HeaderText = "USER_ID"
        Me.USER_ID_CL.Name = "USER_ID_CL"
        Me.USER_ID_CL.ReadOnly = True
        Me.USER_ID_CL.Visible = False
        Me.USER_ID_CL.Width = 56
        '
        'St_Name_CL
        '
        Me.St_Name_CL.DataPropertyName = "St_Name"
        Me.St_Name_CL.HeaderText = "المخزن"
        Me.St_Name_CL.Name = "St_Name_CL"
        Me.St_Name_CL.ReadOnly = True
        Me.St_Name_CL.Width = 69
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        Me.item_name_CL.Width = 70
        '
        'Barcode_CL
        '
        Me.Barcode_CL.DataPropertyName = "Barcode"
        Me.Barcode_CL.HeaderText = "الباركود"
        Me.Barcode_CL.Name = "Barcode_CL"
        Me.Barcode_CL.ReadOnly = True
        Me.Barcode_CL.Width = 73
        '
        'U_Name_CL
        '
        Me.U_Name_CL.DataPropertyName = "U_Name"
        Me.U_Name_CL.HeaderText = "الوحدة"
        Me.U_Name_CL.Name = "U_Name_CL"
        Me.U_Name_CL.ReadOnly = True
        Me.U_Name_CL.Width = 64
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        Me.QTY_CL.Width = 61
        '
        'DATE_OF_ARRIV_CL
        '
        Me.DATE_OF_ARRIV_CL.DataPropertyName = "DATE_OF_ARRIV"
        Me.DATE_OF_ARRIV_CL.HeaderText = "موعد الوصول"
        Me.DATE_OF_ARRIV_CL.Name = "DATE_OF_ARRIV_CL"
        Me.DATE_OF_ARRIV_CL.ReadOnly = True
        Me.DATE_OF_ARRIV_CL.Width = 105
        '
        'DAYS_CL
        '
        Me.DAYS_CL.DataPropertyName = "DAYS"
        Me.DAYS_CL.HeaderText = "الباقي باليوم"
        Me.DAYS_CL.Name = "DAYS_CL"
        Me.DAYS_CL.ReadOnly = True
        Me.DAYS_CL.Width = 91
        '
        'MONTHS_CL
        '
        Me.MONTHS_CL.DataPropertyName = "MONTHS"
        Me.MONTHS_CL.HeaderText = "الباقي بالشهر"
        Me.MONTHS_CL.Name = "MONTHS_CL"
        Me.MONTHS_CL.ReadOnly = True
        Me.MONTHS_CL.Width = 98
        '
        'NOTES_CL
        '
        Me.NOTES_CL.DataPropertyName = "NOTES"
        Me.NOTES_CL.HeaderText = "ملاحظة"
        Me.NOTES_CL.Name = "NOTES_CL"
        Me.NOTES_CL.ReadOnly = True
        Me.NOTES_CL.Width = 72
        '
        'USER_NAME_CL
        '
        Me.USER_NAME_CL.DataPropertyName = "USERNAME"
        Me.USER_NAME_CL.HeaderText = "المدخل"
        Me.USER_NAME_CL.Name = "USER_NAME_CL"
        Me.USER_NAME_CL.ReadOnly = True
        Me.USER_NAME_CL.Width = 66
        '
        'ST_Cm
        '
        Me.ST_Cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ST_Cm.CancelSearchImage = CType(resources.GetObject("ST_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.ST_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ST_Cm.Location = New System.Drawing.Point(83, 106)
        Me.ST_Cm.Name = "ST_Cm"
        Me.ST_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ST_Cm.Size = New System.Drawing.Size(402, 34)
        Me.ST_Cm.SQL_Column = "St_Name"
        Me.ST_Cm.SQL_ID = "ST_ID"
        Me.ST_Cm.SQL_IsNumericSearchField = False
        Me.ST_Cm.SQL_ListSize = 200
        Me.ST_Cm.SQL_NumberOfRows = 200
        Me.ST_Cm.SQL_OrderByField = "St_Name"
        Me.ST_Cm.SQL_SearchField = "St_Name"
        Me.ST_Cm.SQL_Table = "Stores"
        Me.ST_Cm.TabIndex = 748
        Me.ST_Cm.TextMaxLength = 250
        Me.ST_Cm.Textt = ""
        '
        'QTY_txt
        '
        Me.QTY_txt.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.QTY_txt.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold)
        Me.QTY_txt.Location = New System.Drawing.Point(541, 108)
        Me.QTY_txt.MaxLength = 0
        Me.QTY_txt.Name = "QTY_txt"
        Me.QTY_txt.Size = New System.Drawing.Size(100, 30)
        Me.QTY_txt.TabIndex = 747
        '
        'IM_Cm
        '
        Me.IM_Cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IM_Cm.CancelSearchImage = CType(resources.GetObject("IM_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.IM_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Cm.Location = New System.Drawing.Point(542, 34)
        Me.IM_Cm.Name = "IM_Cm"
        Me.IM_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_Cm.Size = New System.Drawing.Size(406, 34)
        Me.IM_Cm.SQL_Column = "item_name"
        Me.IM_Cm.SQL_ID = "IM_ID"
        Me.IM_Cm.SQL_IsNumericSearchField = False
        Me.IM_Cm.SQL_ListSize = 200
        Me.IM_Cm.SQL_NumberOfRows = 200
        Me.IM_Cm.SQL_OrderByField = "item_name"
        Me.IM_Cm.SQL_SearchField = "item_name"
        Me.IM_Cm.SQL_Table = "IM_All_V"
        Me.IM_Cm.TabIndex = 742
        Me.IM_Cm.TextMaxLength = 250
        Me.IM_Cm.Textt = ""
        '
        'IM_ORDERS_COMING
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.ControlBox = False
        Me.Controls.Add(Me.ExpandablePanel1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Controls.Add(Me.AGMetroGrid)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "IM_ORDERS_COMING"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "طلبيات قادمــة"
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ExpandablePanel1.ResumeLayout(False)
        Me.ExpandablePanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ExitFormButton As Button
    Friend WithEvents AGMetroGrid As System.Windows.Forms.DataGridView
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ExpandablePanel1 As DevComponents.DotNetBar.ExpandablePanel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Details_Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents Details_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents IM_Cm As FSearch_Filter
    Friend WithEvents Barcode_SH_txt As TextBox
    Friend WithEvents Label18 As Label
    Public WithEvents IM_Unit_cm As ComboBox
    Friend WithEvents MoveToBill_Btn As Button
    Friend WithEvents QTY_txt As F2FloatField
    Friend WithEvents Label1 As Label
    Friend WithEvents ST_Cm As FSearch_Filter
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents حذفالصفToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents USER_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents St_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DATE_OF_ARRIV_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DAYS_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MONTHS_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NOTES_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents USER_NAME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
