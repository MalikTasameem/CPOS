<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DGV_Control
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
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Date_cb = New System.Windows.Forms.CheckBox()
        Me.St_cb = New System.Windows.Forms.CheckBox()
        Me.D_Valid_cb = New System.Windows.Forms.CheckBox()
        Me.Unit_cb = New System.Windows.Forms.CheckBox()
        Me.Price_cb = New System.Windows.Forms.CheckBox()
        Me.Total_cb = New System.Windows.Forms.CheckBox()
        Me.B_CodeAdd_1_CB = New System.Windows.Forms.CheckBox()
        Me.OpenNextBill_CB = New System.Windows.Forms.CheckBox()
        Me.S_Deafult_cm = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Notes_cb = New System.Windows.Forms.CheckBox()
        Me.IMNUM_cb = New System.Windows.Forms.CheckBox()
        Me.Proj_CB = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.OtherTabPage = New System.Windows.Forms.TabPage()
        Me.IM_NOTE_CB = New System.Windows.Forms.CheckBox()
        Me.SB_Sch_With_QTY_CB = New System.Windows.Forms.CheckBox()
        Me.Serial_Code_CB = New System.Windows.Forms.CheckBox()
        Me.Barcode_CB = New System.Windows.Forms.CheckBox()
        Me.StTabPage = New System.Windows.Forms.TabPage()
        Me.LAST_PCH_CB = New System.Windows.Forms.CheckBox()
        Me.VALID_CB = New System.Windows.Forms.CheckBox()
        Me.SHOW_IMNUM_CB = New System.Windows.Forms.CheckBox()
        Me.GM_CB = New System.Windows.Forms.CheckBox()
        Me.ST_STName_CB = New System.Windows.Forms.CheckBox()
        Me.ItemPriceTabPage = New System.Windows.Forms.TabPage()
        Me.IMPR_MINPR_2_CB = New System.Windows.Forms.CheckBox()
        Me.IMPR_MINPR_CB = New System.Windows.Forms.CheckBox()
        Me.IMPR_BAR_CB = New System.Windows.Forms.CheckBox()
        Me.IMPR_IMNUM_CB = New System.Windows.Forms.CheckBox()
        Me.IM_Discount_CB = New System.Windows.Forms.CheckBox()
        Me.TabControl1.SuspendLayout()
        Me.OtherTabPage.SuspendLayout()
        Me.StTabPage.SuspendLayout()
        Me.ItemPriceTabPage.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(3, 434)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(106, 35)
        Me.ExitFormButton.TabIndex = 442
        Me.ExitFormButton.Text = "رجوع"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Date_cb
        '
        Me.Date_cb.AutoSize = True
        Me.Date_cb.BackColor = System.Drawing.Color.Transparent
        Me.Date_cb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Date_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Date_cb.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Date_cb.ForeColor = System.Drawing.Color.Black
        Me.Date_cb.Location = New System.Drawing.Point(331, 6)
        Me.Date_cb.Name = "Date_cb"
        Me.Date_cb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Date_cb.Size = New System.Drawing.Size(149, 28)
        Me.Date_cb.TabIndex = 444
        Me.Date_cb.Text = "تاريخ إضافة الصنف"
        Me.Date_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Date_cb.UseVisualStyleBackColor = False
        '
        'St_cb
        '
        Me.St_cb.AutoSize = True
        Me.St_cb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.St_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.St_cb.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.St_cb.Location = New System.Drawing.Point(409, 42)
        Me.St_cb.Name = "St_cb"
        Me.St_cb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.St_cb.Size = New System.Drawing.Size(71, 28)
        Me.St_cb.TabIndex = 443
        Me.St_cb.Text = "المخزن"
        Me.St_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.St_cb.UseVisualStyleBackColor = True
        '
        'D_Valid_cb
        '
        Me.D_Valid_cb.AutoSize = True
        Me.D_Valid_cb.BackColor = System.Drawing.Color.Transparent
        Me.D_Valid_cb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.D_Valid_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.D_Valid_cb.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.D_Valid_cb.ForeColor = System.Drawing.Color.Black
        Me.D_Valid_cb.Location = New System.Drawing.Point(398, 78)
        Me.D_Valid_cb.Name = "D_Valid_cb"
        Me.D_Valid_cb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.D_Valid_cb.Size = New System.Drawing.Size(82, 28)
        Me.D_Valid_cb.TabIndex = 446
        Me.D_Valid_cb.Text = "الصلاحية"
        Me.D_Valid_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.D_Valid_cb.UseVisualStyleBackColor = False
        '
        'Unit_cb
        '
        Me.Unit_cb.AutoSize = True
        Me.Unit_cb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Unit_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Unit_cb.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Unit_cb.Location = New System.Drawing.Point(407, 114)
        Me.Unit_cb.Name = "Unit_cb"
        Me.Unit_cb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Unit_cb.Size = New System.Drawing.Size(73, 28)
        Me.Unit_cb.TabIndex = 445
        Me.Unit_cb.Text = "الوحدة"
        Me.Unit_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Unit_cb.UseVisualStyleBackColor = True
        '
        'Price_cb
        '
        Me.Price_cb.AutoSize = True
        Me.Price_cb.BackColor = System.Drawing.Color.Transparent
        Me.Price_cb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Price_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Price_cb.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Price_cb.ForeColor = System.Drawing.Color.Black
        Me.Price_cb.Location = New System.Drawing.Point(417, 150)
        Me.Price_cb.Name = "Price_cb"
        Me.Price_cb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Price_cb.Size = New System.Drawing.Size(63, 28)
        Me.Price_cb.TabIndex = 448
        Me.Price_cb.Text = "السعر"
        Me.Price_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Price_cb.UseVisualStyleBackColor = False
        '
        'Total_cb
        '
        Me.Total_cb.AutoSize = True
        Me.Total_cb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Total_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Total_cb.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Total_cb.Location = New System.Drawing.Point(396, 186)
        Me.Total_cb.Name = "Total_cb"
        Me.Total_cb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Total_cb.Size = New System.Drawing.Size(84, 28)
        Me.Total_cb.TabIndex = 447
        Me.Total_cb.Text = "الإجمالي"
        Me.Total_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Total_cb.UseVisualStyleBackColor = True
        '
        'B_CodeAdd_1_CB
        '
        Me.B_CodeAdd_1_CB.AutoSize = True
        Me.B_CodeAdd_1_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B_CodeAdd_1_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.B_CodeAdd_1_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_CodeAdd_1_CB.Location = New System.Drawing.Point(53, 222)
        Me.B_CodeAdd_1_CB.Name = "B_CodeAdd_1_CB"
        Me.B_CodeAdd_1_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.B_CodeAdd_1_CB.Size = New System.Drawing.Size(427, 28)
        Me.B_CodeAdd_1_CB.TabIndex = 626
        Me.B_CodeAdd_1_CB.Text = "إضافة كمية 1 بشكل مباشر في حالة إضافة عبر قارئ الباركود"
        Me.B_CodeAdd_1_CB.UseVisualStyleBackColor = True
        Me.B_CodeAdd_1_CB.Visible = False
        '
        'OpenNextBill_CB
        '
        Me.OpenNextBill_CB.AutoSize = True
        Me.OpenNextBill_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.OpenNextBill_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OpenNextBill_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpenNextBill_CB.Location = New System.Drawing.Point(257, 257)
        Me.OpenNextBill_CB.Name = "OpenNextBill_CB"
        Me.OpenNextBill_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.OpenNextBill_CB.Size = New System.Drawing.Size(223, 28)
        Me.OpenNextBill_CB.TabIndex = 627
        Me.OpenNextBill_CB.Text = "فتح فاتورة جديدة بعد الحفظ"
        Me.OpenNextBill_CB.UseVisualStyleBackColor = True
        Me.OpenNextBill_CB.Visible = False
        '
        'S_Deafult_cm
        '
        Me.S_Deafult_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.S_Deafult_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.S_Deafult_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.S_Deafult_cm.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.S_Deafult_cm.FormattingEnabled = True
        Me.S_Deafult_cm.Items.AddRange(New Object() {"بالباركود", "بإسم الصنف"})
        Me.S_Deafult_cm.Location = New System.Drawing.Point(81, 291)
        Me.S_Deafult_cm.Name = "S_Deafult_cm"
        Me.S_Deafult_cm.Size = New System.Drawing.Size(184, 29)
        Me.S_Deafult_cm.TabIndex = 628
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(270, 293)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(210, 24)
        Me.Label1.TabIndex = 629
        Me.Label1.Text = "طريقة إدخال الصنف الإفتراضية"
        '
        'Notes_cb
        '
        Me.Notes_cb.AutoSize = True
        Me.Notes_cb.BackColor = System.Drawing.Color.Transparent
        Me.Notes_cb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Notes_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Notes_cb.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Notes_cb.ForeColor = System.Drawing.Color.Black
        Me.Notes_cb.Location = New System.Drawing.Point(198, 6)
        Me.Notes_cb.Name = "Notes_cb"
        Me.Notes_cb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Notes_cb.Size = New System.Drawing.Size(77, 28)
        Me.Notes_cb.TabIndex = 631
        Me.Notes_cb.Text = "ملاحظة"
        Me.Notes_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Notes_cb.UseVisualStyleBackColor = False
        Me.Notes_cb.Visible = False
        '
        'IMNUM_cb
        '
        Me.IMNUM_cb.AutoSize = True
        Me.IMNUM_cb.BackColor = System.Drawing.Color.Transparent
        Me.IMNUM_cb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMNUM_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMNUM_cb.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMNUM_cb.ForeColor = System.Drawing.Color.Black
        Me.IMNUM_cb.Location = New System.Drawing.Point(175, 42)
        Me.IMNUM_cb.Name = "IMNUM_cb"
        Me.IMNUM_cb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMNUM_cb.Size = New System.Drawing.Size(100, 28)
        Me.IMNUM_cb.TabIndex = 632
        Me.IMNUM_cb.Text = "رقم الصنف"
        Me.IMNUM_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IMNUM_cb.UseVisualStyleBackColor = False
        '
        'Proj_CB
        '
        Me.Proj_CB.AutoSize = True
        Me.Proj_CB.BackColor = System.Drawing.Color.Transparent
        Me.Proj_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Proj_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Proj_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Proj_CB.ForeColor = System.Drawing.Color.Black
        Me.Proj_CB.Location = New System.Drawing.Point(201, 78)
        Me.Proj_CB.Name = "Proj_CB"
        Me.Proj_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Proj_CB.Size = New System.Drawing.Size(74, 28)
        Me.Proj_CB.TabIndex = 633
        Me.Proj_CB.Text = "الخدمة"
        Me.Proj_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Proj_CB.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.OtherTabPage)
        Me.TabControl1.Controls.Add(Me.StTabPage)
        Me.TabControl1.Controls.Add(Me.ItemPriceTabPage)
        Me.TabControl1.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold)
        Me.TabControl1.Location = New System.Drawing.Point(0, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.RightToLeftLayout = True
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(492, 424)
        Me.TabControl1.TabIndex = 634
        '
        'OtherTabPage
        '
        Me.OtherTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.OtherTabPage.Controls.Add(Me.IM_Discount_CB)
        Me.OtherTabPage.Controls.Add(Me.IM_NOTE_CB)
        Me.OtherTabPage.Controls.Add(Me.S_Deafult_cm)
        Me.OtherTabPage.Controls.Add(Me.SB_Sch_With_QTY_CB)
        Me.OtherTabPage.Controls.Add(Me.Label1)
        Me.OtherTabPage.Controls.Add(Me.Serial_Code_CB)
        Me.OtherTabPage.Controls.Add(Me.Barcode_CB)
        Me.OtherTabPage.Controls.Add(Me.Notes_cb)
        Me.OtherTabPage.Controls.Add(Me.Price_cb)
        Me.OtherTabPage.Controls.Add(Me.OpenNextBill_CB)
        Me.OtherTabPage.Controls.Add(Me.Total_cb)
        Me.OtherTabPage.Controls.Add(Me.D_Valid_cb)
        Me.OtherTabPage.Controls.Add(Me.Proj_CB)
        Me.OtherTabPage.Controls.Add(Me.Unit_cb)
        Me.OtherTabPage.Controls.Add(Me.IMNUM_cb)
        Me.OtherTabPage.Controls.Add(Me.Date_cb)
        Me.OtherTabPage.Controls.Add(Me.St_cb)
        Me.OtherTabPage.Controls.Add(Me.B_CodeAdd_1_CB)
        Me.OtherTabPage.Location = New System.Drawing.Point(4, 33)
        Me.OtherTabPage.Name = "OtherTabPage"
        Me.OtherTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.OtherTabPage.Size = New System.Drawing.Size(484, 387)
        Me.OtherTabPage.TabIndex = 0
        Me.OtherTabPage.Text = "عرض عام"
        '
        'IM_NOTE_CB
        '
        Me.IM_NOTE_CB.AutoSize = True
        Me.IM_NOTE_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_NOTE_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_NOTE_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_NOTE_CB.Location = New System.Drawing.Point(150, 184)
        Me.IM_NOTE_CB.Name = "IM_NOTE_CB"
        Me.IM_NOTE_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_NOTE_CB.Size = New System.Drawing.Size(125, 28)
        Me.IM_NOTE_CB.TabIndex = 637
        Me.IM_NOTE_CB.Text = "ملاحظة الصنف"
        Me.IM_NOTE_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IM_NOTE_CB.UseVisualStyleBackColor = True
        '
        'SB_Sch_With_QTY_CB
        '
        Me.SB_Sch_With_QTY_CB.AutoSize = True
        Me.SB_Sch_With_QTY_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SB_Sch_With_QTY_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SB_Sch_With_QTY_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SB_Sch_With_QTY_CB.Location = New System.Drawing.Point(132, 328)
        Me.SB_Sch_With_QTY_CB.Name = "SB_Sch_With_QTY_CB"
        Me.SB_Sch_With_QTY_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SB_Sch_With_QTY_CB.Size = New System.Drawing.Size(346, 28)
        Me.SB_Sch_With_QTY_CB.TabIndex = 636
        Me.SB_Sch_With_QTY_CB.Text = "بحث في الأصناف التي تحتوي على كميات فقط"
        Me.SB_Sch_With_QTY_CB.UseVisualStyleBackColor = True
        '
        'Serial_Code_CB
        '
        Me.Serial_Code_CB.AutoSize = True
        Me.Serial_Code_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Serial_Code_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Serial_Code_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Serial_Code_CB.Location = New System.Drawing.Point(163, 150)
        Me.Serial_Code_CB.Name = "Serial_Code_CB"
        Me.Serial_Code_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Serial_Code_CB.Size = New System.Drawing.Size(112, 28)
        Me.Serial_Code_CB.TabIndex = 635
        Me.Serial_Code_CB.Text = "رقم التسلسل"
        Me.Serial_Code_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Serial_Code_CB.UseVisualStyleBackColor = True
        '
        'Barcode_CB
        '
        Me.Barcode_CB.AutoSize = True
        Me.Barcode_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Barcode_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Barcode_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barcode_CB.Location = New System.Drawing.Point(196, 114)
        Me.Barcode_CB.Name = "Barcode_CB"
        Me.Barcode_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Barcode_CB.Size = New System.Drawing.Size(79, 28)
        Me.Barcode_CB.TabIndex = 634
        Me.Barcode_CB.Text = "الباركود"
        Me.Barcode_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Barcode_CB.UseVisualStyleBackColor = True
        '
        'StTabPage
        '
        Me.StTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.StTabPage.Controls.Add(Me.LAST_PCH_CB)
        Me.StTabPage.Controls.Add(Me.VALID_CB)
        Me.StTabPage.Controls.Add(Me.SHOW_IMNUM_CB)
        Me.StTabPage.Controls.Add(Me.GM_CB)
        Me.StTabPage.Controls.Add(Me.ST_STName_CB)
        Me.StTabPage.Location = New System.Drawing.Point(4, 33)
        Me.StTabPage.Name = "StTabPage"
        Me.StTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.StTabPage.Size = New System.Drawing.Size(484, 387)
        Me.StTabPage.TabIndex = 1
        Me.StTabPage.Text = "عرض المخزن"
        '
        'LAST_PCH_CB
        '
        Me.LAST_PCH_CB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LAST_PCH_CB.AutoSize = True
        Me.LAST_PCH_CB.BackColor = System.Drawing.Color.Transparent
        Me.LAST_PCH_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LAST_PCH_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.LAST_PCH_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold)
        Me.LAST_PCH_CB.ForeColor = System.Drawing.Color.Black
        Me.LAST_PCH_CB.Location = New System.Drawing.Point(357, 69)
        Me.LAST_PCH_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.LAST_PCH_CB.Name = "LAST_PCH_CB"
        Me.LAST_PCH_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.LAST_PCH_CB.Size = New System.Drawing.Size(115, 28)
        Me.LAST_PCH_CB.TabIndex = 665
        Me.LAST_PCH_CB.Text = "عرض أخر شراء"
        Me.LAST_PCH_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LAST_PCH_CB.UseVisualStyleBackColor = False
        '
        'VALID_CB
        '
        Me.VALID_CB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.VALID_CB.AutoSize = True
        Me.VALID_CB.BackColor = System.Drawing.Color.Transparent
        Me.VALID_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.VALID_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.VALID_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold)
        Me.VALID_CB.ForeColor = System.Drawing.Color.Black
        Me.VALID_CB.Location = New System.Drawing.Point(211, 69)
        Me.VALID_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.VALID_CB.Name = "VALID_CB"
        Me.VALID_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.VALID_CB.Size = New System.Drawing.Size(117, 28)
        Me.VALID_CB.TabIndex = 664
        Me.VALID_CB.Text = "عرض الصلاحية"
        Me.VALID_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.VALID_CB.UseVisualStyleBackColor = False
        '
        'SHOW_IMNUM_CB
        '
        Me.SHOW_IMNUM_CB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.SHOW_IMNUM_CB.AutoSize = True
        Me.SHOW_IMNUM_CB.BackColor = System.Drawing.Color.Transparent
        Me.SHOW_IMNUM_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SHOW_IMNUM_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SHOW_IMNUM_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold)
        Me.SHOW_IMNUM_CB.ForeColor = System.Drawing.Color.Black
        Me.SHOW_IMNUM_CB.Location = New System.Drawing.Point(50, 69)
        Me.SHOW_IMNUM_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SHOW_IMNUM_CB.Name = "SHOW_IMNUM_CB"
        Me.SHOW_IMNUM_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SHOW_IMNUM_CB.Size = New System.Drawing.Size(135, 28)
        Me.SHOW_IMNUM_CB.TabIndex = 663
        Me.SHOW_IMNUM_CB.Text = "عرض رقم الصنف"
        Me.SHOW_IMNUM_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.SHOW_IMNUM_CB.UseVisualStyleBackColor = False
        '
        'GM_CB
        '
        Me.GM_CB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.GM_CB.AutoSize = True
        Me.GM_CB.BackColor = System.Drawing.Color.Transparent
        Me.GM_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GM_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GM_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold)
        Me.GM_CB.ForeColor = System.Drawing.Color.Black
        Me.GM_CB.Location = New System.Drawing.Point(354, 27)
        Me.GM_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GM_CB.Name = "GM_CB"
        Me.GM_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GM_CB.Size = New System.Drawing.Size(117, 28)
        Me.GM_CB.TabIndex = 662
        Me.GM_CB.Text = "عرض التصنيف"
        Me.GM_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.GM_CB.UseVisualStyleBackColor = False
        '
        'ST_STName_CB
        '
        Me.ST_STName_CB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ST_STName_CB.AutoSize = True
        Me.ST_STName_CB.BackColor = System.Drawing.Color.Transparent
        Me.ST_STName_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_STName_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ST_STName_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold)
        Me.ST_STName_CB.ForeColor = System.Drawing.Color.Black
        Me.ST_STName_CB.Location = New System.Drawing.Point(222, 27)
        Me.ST_STName_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ST_STName_CB.Name = "ST_STName_CB"
        Me.ST_STName_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ST_STName_CB.Size = New System.Drawing.Size(106, 28)
        Me.ST_STName_CB.TabIndex = 661
        Me.ST_STName_CB.Text = "عرض المخزن"
        Me.ST_STName_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ST_STName_CB.UseVisualStyleBackColor = False
        '
        'ItemPriceTabPage
        '
        Me.ItemPriceTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.ItemPriceTabPage.Controls.Add(Me.IMPR_MINPR_2_CB)
        Me.ItemPriceTabPage.Controls.Add(Me.IMPR_MINPR_CB)
        Me.ItemPriceTabPage.Controls.Add(Me.IMPR_BAR_CB)
        Me.ItemPriceTabPage.Controls.Add(Me.IMPR_IMNUM_CB)
        Me.ItemPriceTabPage.Location = New System.Drawing.Point(4, 33)
        Me.ItemPriceTabPage.Name = "ItemPriceTabPage"
        Me.ItemPriceTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ItemPriceTabPage.Size = New System.Drawing.Size(484, 387)
        Me.ItemPriceTabPage.TabIndex = 2
        Me.ItemPriceTabPage.Text = "عرض لوائح الأسعار"
        '
        'IMPR_MINPR_2_CB
        '
        Me.IMPR_MINPR_2_CB.AutoSize = True
        Me.IMPR_MINPR_2_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMPR_MINPR_2_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMPR_MINPR_2_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMPR_MINPR_2_CB.Location = New System.Drawing.Point(342, 111)
        Me.IMPR_MINPR_2_CB.Name = "IMPR_MINPR_2_CB"
        Me.IMPR_MINPR_2_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMPR_MINPR_2_CB.Size = New System.Drawing.Size(139, 28)
        Me.IMPR_MINPR_2_CB.TabIndex = 667
        Me.IMPR_MINPR_2_CB.Text = "بيع جملة الجملة"
        Me.IMPR_MINPR_2_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IMPR_MINPR_2_CB.UseVisualStyleBackColor = True
        '
        'IMPR_MINPR_CB
        '
        Me.IMPR_MINPR_CB.AutoSize = True
        Me.IMPR_MINPR_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMPR_MINPR_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMPR_MINPR_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMPR_MINPR_CB.Location = New System.Drawing.Point(381, 77)
        Me.IMPR_MINPR_CB.Name = "IMPR_MINPR_CB"
        Me.IMPR_MINPR_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMPR_MINPR_CB.Size = New System.Drawing.Size(99, 28)
        Me.IMPR_MINPR_CB.TabIndex = 666
        Me.IMPR_MINPR_CB.Text = "بيع الجملة"
        Me.IMPR_MINPR_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IMPR_MINPR_CB.UseVisualStyleBackColor = True
        '
        'IMPR_BAR_CB
        '
        Me.IMPR_BAR_CB.AutoSize = True
        Me.IMPR_BAR_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMPR_BAR_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMPR_BAR_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMPR_BAR_CB.Location = New System.Drawing.Point(401, 43)
        Me.IMPR_BAR_CB.Name = "IMPR_BAR_CB"
        Me.IMPR_BAR_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMPR_BAR_CB.Size = New System.Drawing.Size(79, 28)
        Me.IMPR_BAR_CB.TabIndex = 665
        Me.IMPR_BAR_CB.Text = "الباركود"
        Me.IMPR_BAR_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IMPR_BAR_CB.UseVisualStyleBackColor = True
        '
        'IMPR_IMNUM_CB
        '
        Me.IMPR_IMNUM_CB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.IMPR_IMNUM_CB.AutoSize = True
        Me.IMPR_IMNUM_CB.BackColor = System.Drawing.Color.Transparent
        Me.IMPR_IMNUM_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMPR_IMNUM_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.IMPR_IMNUM_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold)
        Me.IMPR_IMNUM_CB.ForeColor = System.Drawing.Color.Black
        Me.IMPR_IMNUM_CB.Location = New System.Drawing.Point(379, 9)
        Me.IMPR_IMNUM_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.IMPR_IMNUM_CB.Name = "IMPR_IMNUM_CB"
        Me.IMPR_IMNUM_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMPR_IMNUM_CB.Size = New System.Drawing.Size(101, 28)
        Me.IMPR_IMNUM_CB.TabIndex = 664
        Me.IMPR_IMNUM_CB.Text = "رقم الصنف"
        Me.IMPR_IMNUM_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IMPR_IMNUM_CB.UseVisualStyleBackColor = False
        '
        'IM_Discount_CB
        '
        Me.IM_Discount_CB.AutoSize = True
        Me.IM_Discount_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Discount_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Discount_CB.Font = New System.Drawing.Font("JF Flat", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Discount_CB.Location = New System.Drawing.Point(29, 6)
        Me.IM_Discount_CB.Name = "IM_Discount_CB"
        Me.IM_Discount_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_Discount_CB.Size = New System.Drawing.Size(107, 28)
        Me.IM_Discount_CB.TabIndex = 638
        Me.IM_Discount_CB.Text = "خصم الصنف"
        Me.IM_Discount_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IM_Discount_CB.UseVisualStyleBackColor = True
        '
        'DGV_Control
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 30.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(491, 469)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.Name = "DGV_Control"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تحكم فالعرض"
        Me.TabControl1.ResumeLayout(False)
        Me.OtherTabPage.ResumeLayout(False)
        Me.OtherTabPage.PerformLayout()
        Me.StTabPage.ResumeLayout(False)
        Me.StTabPage.PerformLayout()
        Me.ItemPriceTabPage.ResumeLayout(False)
        Me.ItemPriceTabPage.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Date_cb As System.Windows.Forms.CheckBox
    Friend WithEvents St_cb As System.Windows.Forms.CheckBox
    Friend WithEvents D_Valid_cb As System.Windows.Forms.CheckBox
    Friend WithEvents Unit_cb As System.Windows.Forms.CheckBox
    Friend WithEvents Price_cb As System.Windows.Forms.CheckBox
    Friend WithEvents Total_cb As System.Windows.Forms.CheckBox
    Friend WithEvents B_CodeAdd_1_CB As System.Windows.Forms.CheckBox
    Friend WithEvents OpenNextBill_CB As System.Windows.Forms.CheckBox
    Friend WithEvents S_Deafult_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Notes_cb As System.Windows.Forms.CheckBox
    Friend WithEvents IMNUM_cb As System.Windows.Forms.CheckBox
    Friend WithEvents Proj_CB As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents OtherTabPage As System.Windows.Forms.TabPage
    Friend WithEvents StTabPage As System.Windows.Forms.TabPage
    Friend WithEvents GM_CB As System.Windows.Forms.CheckBox
    Friend WithEvents ST_STName_CB As System.Windows.Forms.CheckBox
    Friend WithEvents LAST_PCH_CB As System.Windows.Forms.CheckBox
    Friend WithEvents VALID_CB As System.Windows.Forms.CheckBox
    Friend WithEvents SHOW_IMNUM_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Barcode_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Serial_Code_CB As System.Windows.Forms.CheckBox
    Friend WithEvents ItemPriceTabPage As System.Windows.Forms.TabPage
    Friend WithEvents IMPR_MINPR_2_CB As System.Windows.Forms.CheckBox
    Friend WithEvents IMPR_MINPR_CB As System.Windows.Forms.CheckBox
    Friend WithEvents IMPR_BAR_CB As System.Windows.Forms.CheckBox
    Friend WithEvents IMPR_IMNUM_CB As System.Windows.Forms.CheckBox
    Friend WithEvents SB_Sch_With_QTY_CB As System.Windows.Forms.CheckBox
    Friend WithEvents IM_NOTE_CB As System.Windows.Forms.CheckBox
    Friend WithEvents IM_Discount_CB As System.Windows.Forms.CheckBox
End Class
