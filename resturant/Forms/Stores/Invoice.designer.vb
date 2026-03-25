<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Invoice
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Invoice))
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.Up_Bill_btn = New System.Windows.Forms.Button()
        Me.Down_Bill_btn = New System.Windows.Forms.Button()
        Me.DGV_Control_btn = New System.Windows.Forms.Button()
        Me.ADDCatButton = New System.Windows.Forms.Button()
        Me.RemoveCatButton = New System.Windows.Forms.Button()
        Me.NULLContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeletedBillLabel = New System.Windows.Forms.Label()
        Me.IM_Show_CxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Invoice_Type_cm = New System.Windows.Forms.ComboBox()
        Me.Bill_ID_Txt = New System.Windows.Forms.TextBox()
        Me.User_Name_lb = New System.Windows.Forms.Label()
        Me.Title_txt = New System.Windows.Forms.TextBox()
        Me.DateTimeEx = New System.Windows.Forms.DateTimePicker()
        Me.IM_Count_LB = New System.Windows.Forms.Label()
        Me.IM_Qty_LB = New System.Windows.Forms.Label()
        Me.Total_TextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Notes_txt = New System.Windows.Forms.TextBox()
        Me.Search_txt = New System.Windows.Forms.TextBox()
        Me.ClearSearch_btn = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Barcode_Search_txt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.IM_btn = New System.Windows.Forms.Button()
        Me.Print_btn = New System.Windows.Forms.Button()
        Me.MakeBarcode_btn = New System.Windows.Forms.Button()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.New_butt = New System.Windows.Forms.Button()
        Me.Save_butt = New System.Windows.Forms.Button()
        Me.Delete_butt = New System.Windows.Forms.Button()
        Me.Edit_butt = New System.Windows.Forms.Button()
        Me.AGMetroGrid = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EX_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMNUM_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EX_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Valid_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMUnit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewSale_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewSaleByOne_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Notes_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_ContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.تعديلToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.عرضالتكلفةToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.عرضربحالفاتورةToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.تعديلصلاحياتالصنفToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.علاضبطاقةالصنفToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IM_Show_CxtMStrip.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel13.SuspendLayout()
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.IM_ContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MetroToolTip1
        '
        Me.MetroToolTip1.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip1.StyleManager = Nothing
        Me.MetroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light
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
        Me.Up_Bill_btn.Location = New System.Drawing.Point(120, 4)
        Me.Up_Bill_btn.Name = "Up_Bill_btn"
        Me.Up_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Up_Bill_btn.Size = New System.Drawing.Size(26, 29)
        Me.Up_Bill_btn.TabIndex = 658
        Me.Up_Bill_btn.TabStop = False
        Me.MetroToolTip1.SetToolTip(Me.Up_Bill_btn, "الفاتورة التالية")
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
        Me.Down_Bill_btn.Location = New System.Drawing.Point(2, 4)
        Me.Down_Bill_btn.Name = "Down_Bill_btn"
        Me.Down_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Down_Bill_btn.Size = New System.Drawing.Size(26, 29)
        Me.Down_Bill_btn.TabIndex = 659
        Me.Down_Bill_btn.TabStop = False
        Me.MetroToolTip1.SetToolTip(Me.Down_Bill_btn, "الفاتورة السابقة")
        Me.Down_Bill_btn.UseVisualStyleBackColor = False
        '
        'DGV_Control_btn
        '
        Me.DGV_Control_btn.BackColor = System.Drawing.Color.White
        Me.DGV_Control_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DGV_Control_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DGV_Control_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.DGV_Control_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DGV_Control_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DGV_Control_btn.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.DGV_Control_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DGV_Control_btn.Image = Global.resturant.My.Resources.Resources.iconfinder_menu_1814109
        Me.DGV_Control_btn.Location = New System.Drawing.Point(836, 122)
        Me.DGV_Control_btn.Name = "DGV_Control_btn"
        Me.DGV_Control_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DGV_Control_btn.Size = New System.Drawing.Size(43, 29)
        Me.DGV_Control_btn.TabIndex = 659
        Me.DGV_Control_btn.TabStop = False
        Me.DGV_Control_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MetroToolTip1.SetToolTip(Me.DGV_Control_btn, "عرض بيانات الجدول")
        Me.DGV_Control_btn.UseVisualStyleBackColor = False
        '
        'ADDCatButton
        '
        Me.ADDCatButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ADDCatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ADDCatButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADDCatButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ADDCatButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ADDCatButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADDCatButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ADDCatButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ADDCatButton.Image = Global.resturant.My.Resources.Resources.IM_ADD
        Me.ADDCatButton.Location = New System.Drawing.Point(836, 152)
        Me.ADDCatButton.Name = "ADDCatButton"
        Me.ADDCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ADDCatButton.Size = New System.Drawing.Size(43, 226)
        Me.ADDCatButton.TabIndex = 396
        Me.ADDCatButton.TabStop = False
        Me.ADDCatButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MetroToolTip1.SetToolTip(Me.ADDCatButton, "إضافة الصنف للفاتورة")
        Me.ADDCatButton.UseVisualStyleBackColor = False
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
        Me.RemoveCatButton.Location = New System.Drawing.Point(836, 380)
        Me.RemoveCatButton.Name = "RemoveCatButton"
        Me.RemoveCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RemoveCatButton.Size = New System.Drawing.Size(43, 117)
        Me.RemoveCatButton.TabIndex = 395
        Me.RemoveCatButton.TabStop = False
        Me.RemoveCatButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MetroToolTip1.SetToolTip(Me.RemoveCatButton, "حذف الصنف المختار")
        Me.RemoveCatButton.UseVisualStyleBackColor = False
        '
        'NULLContextMenuStrip
        '
        Me.NULLContextMenuStrip.Name = "NULLContextMenuStrip"
        Me.NULLContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NULLContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'DeletedBillLabel
        '
        Me.DeletedBillLabel.BackColor = System.Drawing.Color.IndianRed
        Me.DeletedBillLabel.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.DeletedBillLabel.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.DeletedBillLabel.Location = New System.Drawing.Point(1, 42)
        Me.DeletedBillLabel.Name = "DeletedBillLabel"
        Me.DeletedBillLabel.Size = New System.Drawing.Size(343, 36)
        Me.DeletedBillLabel.TabIndex = 402
        Me.DeletedBillLabel.Text = "فاتورة ملغية"
        Me.DeletedBillLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DeletedBillLabel.Visible = False
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
        'Invoice_Type_cm
        '
        Me.Invoice_Type_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Invoice_Type_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Invoice_Type_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Invoice_Type_cm.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Invoice_Type_cm.FormattingEnabled = True
        Me.Invoice_Type_cm.Items.AddRange(New Object() {"جرد اول مدة", "جرد أخر مدة"})
        Me.Invoice_Type_cm.Location = New System.Drawing.Point(3, 2)
        Me.Invoice_Type_cm.Name = "Invoice_Type_cm"
        Me.Invoice_Type_cm.Size = New System.Drawing.Size(138, 31)
        Me.Invoice_Type_cm.TabIndex = 665
        '
        'Bill_ID_Txt
        '
        Me.Bill_ID_Txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Bill_ID_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Bill_ID_Txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Bill_ID_Txt.Font = New System.Drawing.Font("Times New Roman", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bill_ID_Txt.ForeColor = System.Drawing.Color.Black
        Me.Bill_ID_Txt.Location = New System.Drawing.Point(29, 4)
        Me.Bill_ID_Txt.MaxLength = 250
        Me.Bill_ID_Txt.Name = "Bill_ID_Txt"
        Me.Bill_ID_Txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Bill_ID_Txt.Size = New System.Drawing.Size(90, 29)
        Me.Bill_ID_Txt.TabIndex = 664
        Me.Bill_ID_Txt.Text = "1234567"
        Me.Bill_ID_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'User_Name_lb
        '
        Me.User_Name_lb.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.User_Name_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.User_Name_lb.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.User_Name_lb.ForeColor = System.Drawing.Color.Blue
        Me.User_Name_lb.Location = New System.Drawing.Point(173, 670)
        Me.User_Name_lb.Name = "User_Name_lb"
        Me.User_Name_lb.Size = New System.Drawing.Size(618, 25)
        Me.User_Name_lb.TabIndex = 663
        Me.User_Name_lb.Text = "المستخدم"
        Me.User_Name_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Title_txt
        '
        Me.Title_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Title_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Title_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Title_txt.Font = New System.Drawing.Font("JF Flat", 10.25!)
        Me.Title_txt.ForeColor = System.Drawing.Color.Black
        Me.Title_txt.Location = New System.Drawing.Point(2, 4)
        Me.Title_txt.MaxLength = 250
        Me.Title_txt.Name = "Title_txt"
        Me.Title_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Title_txt.Size = New System.Drawing.Size(268, 31)
        Me.Title_txt.TabIndex = 624
        Me.Title_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateTimeEx
        '
        Me.DateTimeEx.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimeEx.CustomFormat = "dd/MM/yyyy"
        Me.DateTimeEx.Font = New System.Drawing.Font("Times New Roman", 13.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimeEx.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimeEx.Location = New System.Drawing.Point(1, 5)
        Me.DateTimeEx.Name = "DateTimeEx"
        Me.DateTimeEx.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTimeEx.RightToLeftLayout = True
        Me.DateTimeEx.Size = New System.Drawing.Size(180, 28)
        Me.DateTimeEx.TabIndex = 383
        '
        'IM_Count_LB
        '
        Me.IM_Count_LB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.IM_Count_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Count_LB.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.IM_Count_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.IM_Count_LB.Location = New System.Drawing.Point(1, 670)
        Me.IM_Count_LB.Name = "IM_Count_LB"
        Me.IM_Count_LB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IM_Count_LB.Size = New System.Drawing.Size(86, 25)
        Me.IM_Count_LB.TabIndex = 650
        Me.IM_Count_LB.Text = "المواد : 0"
        Me.IM_Count_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IM_Qty_LB
        '
        Me.IM_Qty_LB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.IM_Qty_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Qty_LB.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.IM_Qty_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.IM_Qty_LB.Location = New System.Drawing.Point(87, 670)
        Me.IM_Qty_LB.Name = "IM_Qty_LB"
        Me.IM_Qty_LB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IM_Qty_LB.Size = New System.Drawing.Size(86, 25)
        Me.IM_Qty_LB.TabIndex = 651
        Me.IM_Qty_LB.Text = "الكميات : 0"
        Me.IM_Qty_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Total_TextBox
        '
        Me.Total_TextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Total_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_TextBox.Font = New System.Drawing.Font("Stencil", 19.0!)
        Me.Total_TextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Total_TextBox.Location = New System.Drawing.Point(1, 631)
        Me.Total_TextBox.MaxLength = 200
        Me.Total_TextBox.Name = "Total_TextBox"
        Me.Total_TextBox.ReadOnly = True
        Me.Total_TextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Total_TextBox.Size = New System.Drawing.Size(208, 38)
        Me.Total_TextBox.TabIndex = 289
        Me.Total_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("JF Flat", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(212, 634)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 33)
        Me.Label6.TabIndex = 387
        Me.Label6.Text = "الإجمالي"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Notes_txt
        '
        Me.Notes_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Notes_txt.Font = New System.Drawing.Font("JF Flat", 11.0!)
        Me.Notes_txt.ForeColor = System.Drawing.Color.Black
        Me.Notes_txt.Location = New System.Drawing.Point(1, 595)
        Me.Notes_txt.MaxLength = 250
        Me.Notes_txt.Name = "Notes_txt"
        Me.Notes_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Notes_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Notes_txt.Size = New System.Drawing.Size(878, 33)
        Me.Notes_txt.TabIndex = 608
        '
        'Search_txt
        '
        Me.Search_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Search_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Search_txt.Font = New System.Drawing.Font("JF Flat", 10.0!)
        Me.Search_txt.ForeColor = System.Drawing.Color.Black
        Me.Search_txt.Location = New System.Drawing.Point(393, 2)
        Me.Search_txt.MaxLength = 250
        Me.Search_txt.Name = "Search_txt"
        Me.Search_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Search_txt.Size = New System.Drawing.Size(436, 31)
        Me.Search_txt.TabIndex = 670
        Me.Search_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ClearSearch_btn
        '
        Me.ClearSearch_btn.BackColor = System.Drawing.Color.White
        Me.ClearSearch_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClearSearch_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ClearSearch_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ClearSearch_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClearSearch_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ClearSearch_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClearSearch_btn.ForeColor = System.Drawing.Color.DarkRed
        Me.ClearSearch_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ClearSearch_btn.Location = New System.Drawing.Point(2, 2)
        Me.ClearSearch_btn.Name = "ClearSearch_btn"
        Me.ClearSearch_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ClearSearch_btn.Size = New System.Drawing.Size(29, 31)
        Me.ClearSearch_btn.TabIndex = 672
        Me.ClearSearch_btn.TabStop = False
        Me.ClearSearch_btn.Text = "C"
        Me.ClearSearch_btn.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("JF Flat", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(1, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label16.Size = New System.Drawing.Size(256, 40)
        Me.Label16.TabIndex = 685
        Me.Label16.Text = "فاتورة جــــرد"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel9
        '
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.Bill_ID_Txt)
        Me.Panel9.Controls.Add(Me.Up_Bill_btn)
        Me.Panel9.Controls.Add(Me.Down_Bill_btn)
        Me.Panel9.Location = New System.Drawing.Point(852, 1)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(152, 40)
        Me.Panel9.TabIndex = 689
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Title_txt)
        Me.Panel1.Location = New System.Drawing.Point(259, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(343, 40)
        Me.Panel1.TabIndex = 690
        '
        'Label18
        '
        Me.Label18.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label18.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(274, 10)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(60, 21)
        Me.Label18.TabIndex = 690
        Me.Label18.Text = "العنوان :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Controls.Add(Me.DateTimeEx)
        Me.Panel3.Location = New System.Drawing.Point(605, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(245, 40)
        Me.Panel3.TabIndex = 691
        '
        'Label22
        '
        Me.Label22.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label22.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(185, 9)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(54, 21)
        Me.Label22.TabIndex = 690
        Me.Label22.Text = "التاريخ :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Barcode_Search_txt)
        Me.Panel2.Controls.Add(Me.Search_txt)
        Me.Panel2.Controls.Add(Me.ClearSearch_btn)
        Me.Panel2.Location = New System.Drawing.Point(1, 82)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(834, 38)
        Me.Panel2.TabIndex = 708
        '
        'Barcode_Search_txt
        '
        Me.Barcode_Search_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Barcode_Search_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_Search_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Barcode_Search_txt.Font = New System.Drawing.Font("JF Flat", 10.0!)
        Me.Barcode_Search_txt.ForeColor = System.Drawing.Color.Black
        Me.Barcode_Search_txt.Location = New System.Drawing.Point(33, 2)
        Me.Barcode_Search_txt.MaxLength = 250
        Me.Barcode_Search_txt.Name = "Barcode_Search_txt"
        Me.Barcode_Search_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Barcode_Search_txt.Size = New System.Drawing.Size(358, 31)
        Me.Barcode_Search_txt.TabIndex = 673
        Me.Barcode_Search_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(881, 601)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 21)
        Me.Label9.TabIndex = 709
        Me.Label9.Text = "ملاحظة :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel13
        '
        Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel13.Controls.Add(Me.Label13)
        Me.Panel13.Controls.Add(Me.Invoice_Type_cm)
        Me.Panel13.Location = New System.Drawing.Point(346, 42)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(256, 38)
        Me.Panel13.TabIndex = 710
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label13.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(145, 7)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(91, 21)
        Me.Label13.TabIndex = 690
        Me.Label13.Text = "نوع الفاتورة :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(907, 657)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(93, 38)
        Me.ExitFormButton.TabIndex = 673
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'IM_btn
        '
        Me.IM_btn.BackColor = System.Drawing.Color.White
        Me.IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.IM_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.IM_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_btn.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.IM_btn.ForeColor = System.Drawing.Color.Black
        Me.IM_btn.Image = Global.resturant.My.Resources.Resources.if_category_item_select_92565
        Me.IM_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IM_btn.Location = New System.Drawing.Point(881, 484)
        Me.IM_btn.Name = "IM_btn"
        Me.IM_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_btn.Size = New System.Drawing.Size(123, 39)
        Me.IM_btn.TabIndex = 669
        Me.IM_btn.Text = "الأصناف"
        Me.IM_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IM_btn.UseVisualStyleBackColor = False
        '
        'Print_btn
        '
        Me.Print_btn.BackColor = System.Drawing.Color.White
        Me.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_btn.Enabled = False
        Me.Print_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_btn.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Print_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Print_btn.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__2_
        Me.Print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_btn.Location = New System.Drawing.Point(881, 359)
        Me.Print_btn.Name = "Print_btn"
        Me.Print_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_btn.Size = New System.Drawing.Size(123, 39)
        Me.Print_btn.TabIndex = 660
        Me.Print_btn.TabStop = False
        Me.Print_btn.Text = "طباعة F2"
        Me.Print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_btn.UseVisualStyleBackColor = False
        '
        'MakeBarcode_btn
        '
        Me.MakeBarcode_btn.BackColor = System.Drawing.Color.White
        Me.MakeBarcode_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MakeBarcode_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MakeBarcode_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.MakeBarcode_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.MakeBarcode_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MakeBarcode_btn.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.MakeBarcode_btn.ForeColor = System.Drawing.Color.Black
        Me.MakeBarcode_btn.Image = Global.resturant.My.Resources.Resources.iconfinder_barcode_2_62744
        Me.MakeBarcode_btn.Location = New System.Drawing.Point(836, 500)
        Me.MakeBarcode_btn.Name = "MakeBarcode_btn"
        Me.MakeBarcode_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MakeBarcode_btn.Size = New System.Drawing.Size(43, 93)
        Me.MakeBarcode_btn.TabIndex = 436
        Me.MakeBarcode_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MakeBarcode_btn.UseVisualStyleBackColor = False
        '
        'SearchButton
        '
        Me.SearchButton.BackColor = System.Drawing.Color.White
        Me.SearchButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SearchButton.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.SearchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.SearchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchButton.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SearchButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SearchButton.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.SearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SearchButton.Location = New System.Drawing.Point(881, 442)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SearchButton.Size = New System.Drawing.Size(123, 39)
        Me.SearchButton.TabIndex = 306
        Me.SearchButton.TabStop = False
        Me.SearchButton.Text = "بحث"
        Me.SearchButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SearchButton.UseVisualStyleBackColor = False
        '
        'New_butt
        '
        Me.New_butt.BackColor = System.Drawing.Color.White
        Me.New_butt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.New_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.New_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.New_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.New_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.New_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.New_butt.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.New_butt.ForeColor = System.Drawing.Color.Black
        Me.New_butt.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools
        Me.New_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.New_butt.Location = New System.Drawing.Point(881, 236)
        Me.New_butt.Name = "New_butt"
        Me.New_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.New_butt.Size = New System.Drawing.Size(123, 39)
        Me.New_butt.TabIndex = 294
        Me.New_butt.Text = " فاتورة جديدة F1"
        Me.New_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.New_butt.UseVisualStyleBackColor = False
        '
        'Save_butt
        '
        Me.Save_butt.BackColor = System.Drawing.Color.White
        Me.Save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Save_butt.Enabled = False
        Me.Save_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Save_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Save_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_butt.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Save_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Save_butt.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__1_
        Me.Save_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Save_butt.Location = New System.Drawing.Point(881, 277)
        Me.Save_butt.Name = "Save_butt"
        Me.Save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Save_butt.Size = New System.Drawing.Size(123, 39)
        Me.Save_butt.TabIndex = 293
        Me.Save_butt.TabStop = False
        Me.Save_butt.Text = "حفظ F12"
        Me.Save_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Save_butt.UseVisualStyleBackColor = False
        '
        'Delete_butt
        '
        Me.Delete_butt.BackColor = System.Drawing.Color.White
        Me.Delete_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Delete_butt.Enabled = False
        Me.Delete_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Delete_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Delete_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Delete_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Delete_butt.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Delete_butt.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__4_
        Me.Delete_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Delete_butt.Location = New System.Drawing.Point(881, 400)
        Me.Delete_butt.Name = "Delete_butt"
        Me.Delete_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Delete_butt.Size = New System.Drawing.Size(123, 39)
        Me.Delete_butt.TabIndex = 296
        Me.Delete_butt.Text = " إلغاء F4"
        Me.Delete_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Delete_butt.UseVisualStyleBackColor = False
        '
        'Edit_butt
        '
        Me.Edit_butt.BackColor = System.Drawing.Color.White
        Me.Edit_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Edit_butt.Enabled = False
        Me.Edit_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Edit_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Edit_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Edit_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Edit_butt.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Edit_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Edit_butt.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__3_
        Me.Edit_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Edit_butt.Location = New System.Drawing.Point(881, 318)
        Me.Edit_butt.Name = "Edit_butt"
        Me.Edit_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Edit_butt.Size = New System.Drawing.Size(123, 39)
        Me.Edit_butt.TabIndex = 712
        Me.Edit_butt.TabStop = False
        Me.Edit_butt.Text = "تعديل F3"
        Me.Edit_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Edit_butt.UseVisualStyleBackColor = False
        '
        'AGMetroGrid
        '
        Me.AGMetroGrid.AllowUserToAddRows = False
        Me.AGMetroGrid.AllowUserToDeleteRows = False
        Me.AGMetroGrid.AllowUserToResizeRows = False
        Me.AGMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.AGMetroGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.AGMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AGMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.ST_ID_CL, Me.EX_ID_CL, Me.U_ID_CL, Me.Barcode_CL, Me.ST_Name_CL, Me.IMNUM_CL, Me.EX_Name_CL, Me.D_Valid_CL, Me.IMUnit_CL, Me.QTY_CL, Me.Price_CL, Me.NewSale_CL, Me.NewSaleByOne_CL, Me.Total_CL, Me.Notes_CL})
        Me.AGMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AGMetroGrid.Location = New System.Drawing.Point(1, 122)
        Me.AGMetroGrid.MultiSelect = False
        Me.AGMetroGrid.Name = "AGMetroGrid"
        Me.AGMetroGrid.ReadOnly = True
        Me.AGMetroGrid.RowHeadersVisible = False
        Me.AGMetroGrid.RowTemplate.ContextMenuStrip = Me.IM_ContextMenuStrip
        Me.AGMetroGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.AGMetroGrid.RowTemplate.Height = 30
        Me.AGMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGMetroGrid.Size = New System.Drawing.Size(834, 471)
        Me.AGMetroGrid.TabIndex = 713
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'ST_ID_CL
        '
        Me.ST_ID_CL.DataPropertyName = "ST_ID"
        Me.ST_ID_CL.HeaderText = "ST_ID"
        Me.ST_ID_CL.Name = "ST_ID_CL"
        Me.ST_ID_CL.ReadOnly = True
        Me.ST_ID_CL.Visible = False
        '
        'EX_ID_CL
        '
        Me.EX_ID_CL.DataPropertyName = "IM_ID"
        Me.EX_ID_CL.HeaderText = "IM_ID"
        Me.EX_ID_CL.Name = "EX_ID_CL"
        Me.EX_ID_CL.ReadOnly = True
        Me.EX_ID_CL.Visible = False
        '
        'U_ID_CL
        '
        Me.U_ID_CL.DataPropertyName = "U_ID"
        Me.U_ID_CL.HeaderText = "U_ID"
        Me.U_ID_CL.Name = "U_ID_CL"
        Me.U_ID_CL.ReadOnly = True
        Me.U_ID_CL.Visible = False
        '
        'Barcode_CL
        '
        Me.Barcode_CL.DataPropertyName = "Barcode"
        Me.Barcode_CL.FillWeight = 91.83587!
        Me.Barcode_CL.HeaderText = "باركود"
        Me.Barcode_CL.Name = "Barcode_CL"
        Me.Barcode_CL.ReadOnly = True
        Me.Barcode_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'ST_Name_CL
        '
        Me.ST_Name_CL.DataPropertyName = "St_Name"
        Me.ST_Name_CL.FillWeight = 91.83587!
        Me.ST_Name_CL.HeaderText = "مخزن"
        Me.ST_Name_CL.Name = "ST_Name_CL"
        Me.ST_Name_CL.ReadOnly = True
        '
        'IMNUM_CL
        '
        Me.IMNUM_CL.DataPropertyName = "IM_Num"
        Me.IMNUM_CL.FillWeight = 91.83587!
        Me.IMNUM_CL.HeaderText = "رقم"
        Me.IMNUM_CL.Name = "IMNUM_CL"
        Me.IMNUM_CL.ReadOnly = True
        '
        'EX_Name_CL
        '
        Me.EX_Name_CL.DataPropertyName = "item_name"
        Me.EX_Name_CL.FillWeight = 91.83587!
        Me.EX_Name_CL.HeaderText = "الصنف"
        Me.EX_Name_CL.Name = "EX_Name_CL"
        Me.EX_Name_CL.ReadOnly = True
        '
        'D_Valid_CL
        '
        Me.D_Valid_CL.DataPropertyName = "D_Vaild"
        Me.D_Valid_CL.FillWeight = 91.83587!
        Me.D_Valid_CL.HeaderText = "صلاحية"
        Me.D_Valid_CL.Name = "D_Valid_CL"
        Me.D_Valid_CL.ReadOnly = True
        '
        'IMUnit_CL
        '
        Me.IMUnit_CL.DataPropertyName = "U_Name"
        Me.IMUnit_CL.FillWeight = 91.83587!
        Me.IMUnit_CL.HeaderText = "الوحدة"
        Me.IMUnit_CL.Name = "IMUnit_CL"
        Me.IMUnit_CL.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QYT"
        Me.QTY_CL.FillWeight = 91.83587!
        Me.QTY_CL.HeaderText = "كمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'Price_CL
        '
        Me.Price_CL.DataPropertyName = "Price"
        DataGridViewCellStyle1.Format = "N2"
        Me.Price_CL.DefaultCellStyle = DataGridViewCellStyle1
        Me.Price_CL.FillWeight = 91.83587!
        Me.Price_CL.HeaderText = "السعر"
        Me.Price_CL.Name = "Price_CL"
        Me.Price_CL.ReadOnly = True
        '
        'NewSale_CL
        '
        Me.NewSale_CL.DataPropertyName = "NewSale"
        DataGridViewCellStyle2.Format = "N2"
        Me.NewSale_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.NewSale_CL.HeaderText = "البيع"
        Me.NewSale_CL.Name = "NewSale_CL"
        Me.NewSale_CL.ReadOnly = True
        '
        'NewSaleByOne_CL
        '
        Me.NewSaleByOne_CL.DataPropertyName = "NewSaleByOne"
        DataGridViewCellStyle3.Format = "N2"
        Me.NewSaleByOne_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.NewSaleByOne_CL.HeaderText = "بيع القطعة"
        Me.NewSaleByOne_CL.Name = "NewSaleByOne_CL"
        Me.NewSaleByOne_CL.ReadOnly = True
        Me.NewSaleByOne_CL.Visible = False
        '
        'Total_CL
        '
        Me.Total_CL.DataPropertyName = "Total"
        DataGridViewCellStyle4.Format = "N2"
        Me.Total_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.Total_CL.FillWeight = 91.83587!
        Me.Total_CL.HeaderText = "إجمالي"
        Me.Total_CL.Name = "Total_CL"
        Me.Total_CL.ReadOnly = True
        '
        'Notes_CL
        '
        Me.Notes_CL.DataPropertyName = "Notes"
        Me.Notes_CL.FillWeight = 91.83587!
        Me.Notes_CL.HeaderText = "ملاحظة"
        Me.Notes_CL.Name = "Notes_CL"
        Me.Notes_CL.ReadOnly = True
        '
        'IM_ContextMenuStrip
        '
        Me.IM_ContextMenuStrip.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IM_ContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.تعديلToolStripMenuItem, Me.عرضالتكلفةToolStripMenuItem, Me.عرضربحالفاتورةToolStripMenuItem, Me.تعديلصلاحياتالصنفToolStripMenuItem, Me.علاضبطاقةالصنفToolStripMenuItem})
        Me.IM_ContextMenuStrip.Name = "IM_ContextMenuStrip"
        Me.IM_ContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_ContextMenuStrip.Size = New System.Drawing.Size(227, 144)
        '
        'تعديلToolStripMenuItem
        '
        Me.تعديلToolStripMenuItem.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.تعديلToolStripMenuItem.Name = "تعديلToolStripMenuItem"
        Me.تعديلToolStripMenuItem.Size = New System.Drawing.Size(226, 28)
        Me.تعديلToolStripMenuItem.Text = "تعديل بيانات الصنف "
        '
        'عرضالتكلفةToolStripMenuItem
        '
        Me.عرضالتكلفةToolStripMenuItem.Image = Global.resturant.My.Resources.Resources.if_hand_cursor_2639827
        Me.عرضالتكلفةToolStripMenuItem.Name = "عرضالتكلفةToolStripMenuItem"
        Me.عرضالتكلفةToolStripMenuItem.Size = New System.Drawing.Size(226, 28)
        Me.عرضالتكلفةToolStripMenuItem.Text = "عرض التكلفة"
        '
        'عرضربحالفاتورةToolStripMenuItem
        '
        Me.عرضربحالفاتورةToolStripMenuItem.Image = Global.resturant.My.Resources.Resources.iconfinder_money_299107
        Me.عرضربحالفاتورةToolStripMenuItem.Name = "عرضربحالفاتورةToolStripMenuItem"
        Me.عرضربحالفاتورةToolStripMenuItem.Size = New System.Drawing.Size(226, 28)
        Me.عرضربحالفاتورةToolStripMenuItem.Text = "عرض ربح الفاتورة"
        Me.عرضربحالفاتورةToolStripMenuItem.Visible = False
        '
        'تعديلصلاحياتالصنفToolStripMenuItem
        '
        Me.تعديلصلاحياتالصنفToolStripMenuItem.Name = "تعديلصلاحياتالصنفToolStripMenuItem"
        Me.تعديلصلاحياتالصنفToolStripMenuItem.Size = New System.Drawing.Size(226, 28)
        Me.تعديلصلاحياتالصنفToolStripMenuItem.Text = "تعديل صلاحيات الصنف"
        '
        'علاضبطاقةالصنفToolStripMenuItem
        '
        Me.علاضبطاقةالصنفToolStripMenuItem.Name = "علاضبطاقةالصنفToolStripMenuItem"
        Me.علاضبطاقةالصنفToolStripMenuItem.Size = New System.Drawing.Size(226, 28)
        Me.علاضبطاقةالصنفToolStripMenuItem.Text = "عرض بطاقة الصنف"
        '
        'Invoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(1005, 695)
        Me.Controls.Add(Me.AGMetroGrid)
        Me.Controls.Add(Me.Edit_butt)
        Me.Controls.Add(Me.Panel13)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.User_Name_lb)
        Me.Controls.Add(Me.IM_btn)
        Me.Controls.Add(Me.IM_Count_LB)
        Me.Controls.Add(Me.IM_Qty_LB)
        Me.Controls.Add(Me.Print_btn)
        Me.Controls.Add(Me.DGV_Control_btn)
        Me.Controls.Add(Me.Total_TextBox)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DeletedBillLabel)
        Me.Controls.Add(Me.MakeBarcode_btn)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.New_butt)
        Me.Controls.Add(Me.ADDCatButton)
        Me.Controls.Add(Me.Save_butt)
        Me.Controls.Add(Me.Delete_butt)
        Me.Controls.Add(Me.RemoveCatButton)
        Me.Controls.Add(Me.Notes_txt)
        Me.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Invoice"
        Me.Padding = New System.Windows.Forms.Padding(27, 97, 27, 32)
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "فاتورة جرد"
        Me.IM_Show_CxtMStrip.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.IM_ContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
    Friend WithEvents New_butt As System.Windows.Forms.Button
    Friend WithEvents Save_butt As System.Windows.Forms.Button
    Friend WithEvents Delete_butt As System.Windows.Forms.Button
    Friend WithEvents Total_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents DateTimeEx As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ADDCatButton As System.Windows.Forms.Button
    Friend WithEvents RemoveCatButton As System.Windows.Forms.Button
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents NULLContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeletedBillLabel As System.Windows.Forms.Label
    Friend WithEvents Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents Title_txt As System.Windows.Forms.TextBox
    Friend WithEvents MakeBarcode_btn As System.Windows.Forms.Button
    Friend WithEvents IM_Qty_LB As System.Windows.Forms.Label
    Friend WithEvents IM_Count_LB As System.Windows.Forms.Label
    Friend WithEvents User_Name_lb As System.Windows.Forms.Label
    Friend WithEvents Down_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Up_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents DGV_Control_btn As System.Windows.Forms.Button
    Friend WithEvents Print_btn As System.Windows.Forms.Button
    Friend WithEvents IM_btn As System.Windows.Forms.Button
    Friend WithEvents Search_txt As System.Windows.Forms.TextBox
    Friend WithEvents ClearSearch_btn As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Bill_ID_Txt As System.Windows.Forms.TextBox
    Public WithEvents Invoice_Type_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Edit_butt As System.Windows.Forms.Button
    Friend WithEvents AGMetroGrid As System.Windows.Forms.DataGridView
    Friend WithEvents Barcode_Search_txt As System.Windows.Forms.TextBox
    Friend WithEvents IM_Show_CxtMStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ST_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EX_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ST_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMNUM_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EX_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Valid_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMUnit_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NewSale_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NewSaleByOne_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Notes_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_ContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents تعديلToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents عرضالتكلفةToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents عرضربحالفاتورةToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents تعديلصلاحياتالصنفToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents علاضبطاقةالصنفToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
