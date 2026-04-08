<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Change_IM_Details
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Change_IM_Details))
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.MinFormButton = New System.Windows.Forms.Button()
        Me.MaxFormButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.PriceTextBox = New System.Windows.Forms.TextBox()
        Me.NULLContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.QtyTextBox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Current_QTY = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.IM_Unit_cm = New System.Windows.Forms.ComboBox()
        Me.ConfermButton = New System.Windows.Forms.Button()
        Me.IM_LB = New System.Windows.Forms.TextBox()
        Me.Notes_txt = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Note_Panel = New System.Windows.Forms.Panel()
        Me.IM_DiscountPanel = New System.Windows.Forms.Panel()
        Me.is_PercentCB = New System.Windows.Forms.CheckBox()
        Me.IM_Disc_txt = New resturant.F2FloatField()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.NewSalePrice_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Pch_Panel = New System.Windows.Forms.Panel()
        Me.IM_CalcAvgCost_btn = New System.Windows.Forms.Button()
        Me.Newsale_ByOne_Panel = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NewSaleByOne = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SB_2_Btn = New System.Windows.Forms.Button()
        Me.SB_3_Btn = New System.Windows.Forms.Button()
        Me.TitleBar_Panel.SuspendLayout()
        Me.Note_Panel.SuspendLayout()
        Me.IM_DiscountPanel.SuspendLayout()
        Me.Pch_Panel.SuspendLayout()
        Me.Newsale_ByOne_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.MinFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.MaxFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(502, 40)
        Me.TitleBar_Panel.TabIndex = 999
        Me.TitleBar_Panel.Tag = "HEADER"
        '
        'MinFormButton
        '
        Me.MinFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MinFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MinFormButton.FlatAppearance.BorderSize = 0
        Me.MinFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MinFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.MinFormButton.ForeColor = System.Drawing.Color.White
        Me.MinFormButton.Location = New System.Drawing.Point(90, 0)
        Me.MinFormButton.Name = "MinFormButton"
        Me.MinFormButton.Size = New System.Drawing.Size(45, 40)
        Me.MinFormButton.TabIndex = 1
        Me.MinFormButton.Tag = "APP_CONTROL"
        Me.MinFormButton.Text = "ـ"
        Me.MinFormButton.UseVisualStyleBackColor = False
        '
        'MaxFormButton
        '
        Me.MaxFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MaxFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MaxFormButton.FlatAppearance.BorderSize = 0
        Me.MaxFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MaxFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.MaxFormButton.ForeColor = System.Drawing.Color.White
        Me.MaxFormButton.Location = New System.Drawing.Point(45, 0)
        Me.MaxFormButton.Name = "MaxFormButton"
        Me.MaxFormButton.Size = New System.Drawing.Size(45, 40)
        Me.MaxFormButton.TabIndex = 2
        Me.MaxFormButton.Tag = "APP_CONTROL"
        Me.MaxFormButton.Text = "⬜"
        Me.MaxFormButton.UseVisualStyleBackColor = False
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
        Me.ExitFormButton.Size = New System.Drawing.Size(45, 40)
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
        Me.TopTitle_LB.Location = New System.Drawing.Point(365, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(113, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "تعديل الصنـــــف"
        '
        'PriceTextBox
        '
        Me.PriceTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PriceTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.PriceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PriceTextBox.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.PriceTextBox.Font = New System.Drawing.Font("Stencil", 15.7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PriceTextBox.ForeColor = System.Drawing.Color.DarkGreen
        Me.PriceTextBox.Location = New System.Drawing.Point(129, 111)
        Me.PriceTextBox.MaxLength = 250
        Me.PriceTextBox.Name = "PriceTextBox"
        Me.PriceTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PriceTextBox.Size = New System.Drawing.Size(107, 25)
        Me.PriceTextBox.TabIndex = 617
        '
        'NULLContextMenuStrip
        '
        Me.NULLContextMenuStrip.Name = "NULLContextMenuStrip"
        Me.NULLContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NULLContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'QtyTextBox
        '
        Me.QtyTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.QtyTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.QtyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.QtyTextBox.Font = New System.Drawing.Font("Segoe UI", 16.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QtyTextBox.ForeColor = System.Drawing.Color.Black
        Me.QtyTextBox.Location = New System.Drawing.Point(9, 108)
        Me.QtyTextBox.MaxLength = 250
        Me.QtyTextBox.Name = "QtyTextBox"
        Me.QtyTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.QtyTextBox.Size = New System.Drawing.Size(113, 37)
        Me.QtyTextBox.TabIndex = 618
        Me.QtyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(160, 86)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 21)
        Me.Label7.TabIndex = 620
        Me.Label7.Text = "السعر "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(49, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 21)
        Me.Label2.TabIndex = 619
        Me.Label2.Text = "الكمية"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label12.Location = New System.Drawing.Point(389, 84)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label12.Size = New System.Drawing.Size(93, 21)
        Me.Label12.TabIndex = 622
        Me.Label12.Text = "الكمية الحالية"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Current_QTY
        '
        Me.Current_QTY.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Current_QTY.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Current_QTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Current_QTY.Enabled = False
        Me.Current_QTY.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Current_QTY.ForeColor = System.Drawing.Color.Firebrick
        Me.Current_QTY.Location = New System.Drawing.Point(372, 110)
        Me.Current_QTY.Name = "Current_QTY"
        Me.Current_QTY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Current_QTY.Size = New System.Drawing.Size(128, 36)
        Me.Current_QTY.TabIndex = 621
        '
        'Label15
        '
        Me.Label15.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(291, 85)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(52, 21)
        Me.Label15.TabIndex = 624
        Me.Label15.Text = "الوحدة"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'IM_Unit_cm
        '
        Me.IM_Unit_cm.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.IM_Unit_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Unit_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IM_Unit_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Unit_cm.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Unit_cm.FormattingEnabled = True
        Me.IM_Unit_cm.Location = New System.Drawing.Point(245, 112)
        Me.IM_Unit_cm.Name = "IM_Unit_cm"
        Me.IM_Unit_cm.Size = New System.Drawing.Size(120, 26)
        Me.IM_Unit_cm.TabIndex = 623
        '
        'ConfermButton
        '
        Me.ConfermButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ConfermButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ConfermButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ConfermButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ConfermButton.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfermButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ConfermButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ConfermButton.Location = New System.Drawing.Point(9, 438)
        Me.ConfermButton.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.ConfermButton.Name = "ConfermButton"
        Me.ConfermButton.Size = New System.Drawing.Size(362, 49)
        Me.ConfermButton.TabIndex = 630
        Me.ConfermButton.Tag = "SAVE"
        Me.ConfermButton.Text = "تطبيــق Enter"
        Me.ConfermButton.UseVisualStyleBackColor = False
        '
        'IM_LB
        '
        Me.IM_LB.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.IM_LB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.IM_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_LB.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.IM_LB.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_LB.ForeColor = System.Drawing.Color.Black
        Me.IM_LB.Location = New System.Drawing.Point(2, 42)
        Me.IM_LB.MaxLength = 250
        Me.IM_LB.Name = "IM_LB"
        Me.IM_LB.ReadOnly = True
        Me.IM_LB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_LB.Size = New System.Drawing.Size(498, 29)
        Me.IM_LB.TabIndex = 632
        Me.IM_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Notes_txt
        '
        Me.Notes_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Notes_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Notes_txt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.25!)
        Me.Notes_txt.ForeColor = System.Drawing.Color.Black
        Me.Notes_txt.Location = New System.Drawing.Point(2, 47)
        Me.Notes_txt.MaxLength = 250
        Me.Notes_txt.Multiline = True
        Me.Notes_txt.Name = "Notes_txt"
        Me.Notes_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Notes_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Notes_txt.Size = New System.Drawing.Size(380, 64)
        Me.Notes_txt.TabIndex = 633
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(385, 51)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(66, 21)
        Me.Label14.TabIndex = 634
        Me.Label14.Text = "ملاحظة :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Note_Panel
        '
        Me.Note_Panel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Note_Panel.Controls.Add(Me.IM_DiscountPanel)
        Me.Note_Panel.Controls.Add(Me.Notes_txt)
        Me.Note_Panel.Controls.Add(Me.Label14)
        Me.Note_Panel.Location = New System.Drawing.Point(9, 234)
        Me.Note_Panel.Name = "Note_Panel"
        Me.Note_Panel.Size = New System.Drawing.Size(477, 114)
        Me.Note_Panel.TabIndex = 635
        Me.Note_Panel.Visible = False
        '
        'IM_DiscountPanel
        '
        Me.IM_DiscountPanel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.IM_DiscountPanel.Controls.Add(Me.is_PercentCB)
        Me.IM_DiscountPanel.Controls.Add(Me.IM_Disc_txt)
        Me.IM_DiscountPanel.Controls.Add(Me.Label4)
        Me.IM_DiscountPanel.Location = New System.Drawing.Point(10, 10)
        Me.IM_DiscountPanel.Name = "IM_DiscountPanel"
        Me.IM_DiscountPanel.Size = New System.Drawing.Size(440, 35)
        Me.IM_DiscountPanel.TabIndex = 637
        Me.IM_DiscountPanel.Visible = False
        '
        'is_PercentCB
        '
        Me.is_PercentCB.AutoSize = True
        Me.is_PercentCB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_PercentCB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.is_PercentCB.Location = New System.Drawing.Point(122, 6)
        Me.is_PercentCB.Name = "is_PercentCB"
        Me.is_PercentCB.Size = New System.Drawing.Size(135, 25)
        Me.is_PercentCB.TabIndex = 637
        Me.is_PercentCB.Text = "تخفيض بنسبة %"
        Me.is_PercentCB.UseVisualStyleBackColor = True
        '
        'IM_Disc_txt
        '
        Me.IM_Disc_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IM_Disc_txt.BackColor = System.Drawing.Color.Lavender
        Me.IM_Disc_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Disc_txt.Font = New System.Drawing.Font("Stencil", 15.7!)
        Me.IM_Disc_txt.Location = New System.Drawing.Point(272, 2)
        Me.IM_Disc_txt.MaxLength = 0
        Me.IM_Disc_txt.Name = "IM_Disc_txt"
        Me.IM_Disc_txt.Size = New System.Drawing.Size(101, 32)
        Me.IM_Disc_txt.TabIndex = 635
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(379, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 21)
        Me.Label4.TabIndex = 636
        Me.Label4.Text = "خصم :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Back_Btn
        '
        Me.Back_Btn.BackColor = System.Drawing.Color.IndianRed
        Me.Back_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Back_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_Btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.Back_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Back_Btn.Location = New System.Drawing.Point(378, 438)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Back_Btn.Size = New System.Drawing.Size(107, 49)
        Me.Back_Btn.TabIndex = 636
        Me.Back_Btn.TabStop = False
        Me.Back_Btn.Tag = "DELETE"
        Me.Back_Btn.Text = "خروج Esc"
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'NewSalePrice_txt
        '
        Me.NewSalePrice_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.NewSalePrice_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NewSalePrice_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.NewSalePrice_txt.Font = New System.Drawing.Font("Stencil", 15.7!)
        Me.NewSalePrice_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.NewSalePrice_txt.Location = New System.Drawing.Point(42, 34)
        Me.NewSalePrice_txt.MaxLength = 250
        Me.NewSalePrice_txt.Name = "NewSalePrice_txt"
        Me.NewSalePrice_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NewSalePrice_txt.Size = New System.Drawing.Size(105, 32)
        Me.NewSalePrice_txt.TabIndex = 637
        Me.NewSalePrice_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(60, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 21)
        Me.Label1.TabIndex = 639
        Me.Label1.Text = "سعر البيع"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Pch_Panel
        '
        Me.Pch_Panel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Pch_Panel.Controls.Add(Me.IM_CalcAvgCost_btn)
        Me.Pch_Panel.Controls.Add(Me.Newsale_ByOne_Panel)
        Me.Pch_Panel.Controls.Add(Me.Label1)
        Me.Pch_Panel.Controls.Add(Me.NewSalePrice_txt)
        Me.Pch_Panel.Location = New System.Drawing.Point(9, 151)
        Me.Pch_Panel.Name = "Pch_Panel"
        Me.Pch_Panel.Size = New System.Drawing.Size(301, 77)
        Me.Pch_Panel.TabIndex = 641
        Me.Pch_Panel.Visible = False
        '
        'IM_CalcAvgCost_btn
        '
        Me.IM_CalcAvgCost_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.IM_CalcAvgCost_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IM_CalcAvgCost_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_CalcAvgCost_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.IM_CalcAvgCost_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.IM_CalcAvgCost_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.IM_CalcAvgCost_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_CalcAvgCost_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.IM_CalcAvgCost_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IM_CalcAvgCost_btn.Location = New System.Drawing.Point(4, 34)
        Me.IM_CalcAvgCost_btn.Name = "IM_CalcAvgCost_btn"
        Me.IM_CalcAvgCost_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_CalcAvgCost_btn.Size = New System.Drawing.Size(36, 32)
        Me.IM_CalcAvgCost_btn.TabIndex = 643
        Me.IM_CalcAvgCost_btn.TabStop = False
        Me.IM_CalcAvgCost_btn.Text = "a/b"
        Me.IM_CalcAvgCost_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.IM_CalcAvgCost_btn, "حساب المتوسط الحسابي لبضاعة المخزن مع البضاعة المدخلة")
        Me.IM_CalcAvgCost_btn.UseVisualStyleBackColor = False
        '
        'Newsale_ByOne_Panel
        '
        Me.Newsale_ByOne_Panel.Controls.Add(Me.Label3)
        Me.Newsale_ByOne_Panel.Controls.Add(Me.NewSaleByOne)
        Me.Newsale_ByOne_Panel.Location = New System.Drawing.Point(150, 3)
        Me.Newsale_ByOne_Panel.Name = "Newsale_ByOne_Panel"
        Me.Newsale_ByOne_Panel.Size = New System.Drawing.Size(148, 71)
        Me.Newsale_ByOne_Panel.TabIndex = 642
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 21)
        Me.Label3.TabIndex = 640
        Me.Label3.Text = "سعر بيع القطعة"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'NewSaleByOne
        '
        Me.NewSaleByOne.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NewSaleByOne.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.NewSaleByOne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NewSaleByOne.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.NewSaleByOne.Font = New System.Drawing.Font("Stencil", 15.7!)
        Me.NewSaleByOne.ForeColor = System.Drawing.Color.DarkGreen
        Me.NewSaleByOne.Location = New System.Drawing.Point(18, 33)
        Me.NewSaleByOne.MaxLength = 250
        Me.NewSaleByOne.Name = "NewSaleByOne"
        Me.NewSaleByOne.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NewSaleByOne.Size = New System.Drawing.Size(107, 32)
        Me.NewSaleByOne.TabIndex = 638
        Me.NewSaleByOne.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SB_2_Btn
        '
        Me.SB_2_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SB_2_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SB_2_Btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SB_2_Btn.Location = New System.Drawing.Point(268, 351)
        Me.SB_2_Btn.Name = "SB_2_Btn"
        Me.SB_2_Btn.Size = New System.Drawing.Size(217, 51)
        Me.SB_2_Btn.TabIndex = 642
        Me.SB_2_Btn.Tag = "0"
        Me.SB_2_Btn.Text = "سعر الجملة:"
        Me.SB_2_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SB_2_Btn.UseVisualStyleBackColor = True
        Me.SB_2_Btn.Visible = False
        '
        'SB_3_Btn
        '
        Me.SB_3_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SB_3_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SB_3_Btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SB_3_Btn.Location = New System.Drawing.Point(9, 351)
        Me.SB_3_Btn.Name = "SB_3_Btn"
        Me.SB_3_Btn.Size = New System.Drawing.Size(217, 51)
        Me.SB_3_Btn.TabIndex = 643
        Me.SB_3_Btn.Tag = "0"
        Me.SB_3_Btn.Text = "سعر جملة الجملة:"
        Me.SB_3_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SB_3_Btn.UseVisualStyleBackColor = True
        Me.SB_3_Btn.Visible = False
        '
        'Change_IM_Details
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(502, 489)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.SB_3_Btn)
        Me.Controls.Add(Me.SB_2_Btn)
        Me.Controls.Add(Me.Pch_Panel)
        Me.Controls.Add(Me.Back_Btn)
        Me.Controls.Add(Me.Note_Panel)
        Me.Controls.Add(Me.IM_LB)
        Me.Controls.Add(Me.ConfermButton)
        Me.Controls.Add(Me.PriceTextBox)
        Me.Controls.Add(Me.QtyTextBox)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Current_QTY)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.IM_Unit_cm)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Change_IM_Details"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تعديل صنف"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.Note_Panel.ResumeLayout(False)
        Me.Note_Panel.PerformLayout()
        Me.IM_DiscountPanel.ResumeLayout(False)
        Me.IM_DiscountPanel.PerformLayout()
        Me.Pch_Panel.ResumeLayout(False)
        Me.Pch_Panel.PerformLayout()
        Me.Newsale_ByOne_Panel.ResumeLayout(False)
        Me.Newsale_ByOne_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents MaxFormButton As System.Windows.Forms.Button
    Friend WithEvents MinFormButton As System.Windows.Forms.Button

    Friend WithEvents PriceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents QtyTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Current_QTY As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents IM_Unit_cm As System.Windows.Forms.ComboBox
    Friend WithEvents ConfermButton As System.Windows.Forms.Button
    Friend WithEvents NULLContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents IM_LB As System.Windows.Forms.TextBox
    Friend WithEvents Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Note_Panel As System.Windows.Forms.Panel
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents NewSalePrice_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Pch_Panel As System.Windows.Forms.Panel
    Friend WithEvents IM_CalcAvgCost_btn As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Newsale_ByOne_Panel As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NewSaleByOne As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents IM_Disc_txt As resturant.F2FloatField
    Friend WithEvents IM_DiscountPanel As System.Windows.Forms.Panel
    Friend WithEvents is_PercentCB As CheckBox
    Friend WithEvents SB_2_Btn As Button
    Friend WithEvents SB_3_Btn As Button
End Class