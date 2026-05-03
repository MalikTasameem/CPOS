<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SB_IM_card
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
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Title_LB = New System.Windows.Forms.Label()
        Me.All_St_Panel = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ALL_QTY_txt = New System.Windows.Forms.TextBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Current_QTY = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.ST_cm = New System.Windows.Forms.ComboBox()
        Me.ADDCatButton = New System.Windows.Forms.Button()
        Me.Exit_Btn = New System.Windows.Forms.Button()
        Me.LAST_SELL_Lb = New System.Windows.Forms.Label()
        Me.Min_SP_Panel = New System.Windows.Forms.Panel()
        Me.Min_SP_2_CB = New System.Windows.Forms.CheckBox()
        Me.Min_SP_CB = New System.Windows.Forms.CheckBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.IM_Unit_cm = New System.Windows.Forms.ComboBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PriceTextBox = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.QtyTextBox = New System.Windows.Forms.TextBox()
        Me.Valid_Panel = New System.Windows.Forms.Panel()
        Me.Valid_cm = New System.Windows.Forms.ComboBox()
        Me.Valid_QTY_txt = New System.Windows.Forms.TextBox()
        Me.Serial_Code_Panel = New System.Windows.Forms.Panel()
        Me.Serial_Code_txt = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ST_Bercent_Panel = New System.Windows.Forms.Panel()
        Me.Bercent_TXT = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mySearchControl = New resturant.SearchItemControl()
        Me.TitleBar_Panel.SuspendLayout()
        Me.All_St_Panel.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Min_SP_Panel.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Valid_Panel.SuspendLayout()
        Me.Serial_Code_Panel.SuspendLayout()
        Me.ST_Bercent_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.Title_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(728, 42)
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
        Me.ExitFormButton.Size = New System.Drawing.Size(45, 42)
        Me.ExitFormButton.TabIndex = 3
        Me.ExitFormButton.Tag = "DELETE"
        Me.ExitFormButton.Text = "X"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Title_LB
        '
        Me.Title_LB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Title_LB.AutoSize = True
        Me.Title_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Title_LB.ForeColor = System.Drawing.Color.White
        Me.Title_LB.Location = New System.Drawing.Point(594, 9)
        Me.Title_LB.Name = "Title_LB"
        Me.Title_LB.Size = New System.Drawing.Size(109, 21)
        Me.Title_LB.TabIndex = 0
        Me.Title_LB.Tag = "TITLE_TRANSPARENT"
        Me.Title_LB.Text = "تفاصيل الصنف"
        '
        'All_St_Panel
        '
        Me.All_St_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.All_St_Panel.Controls.Add(Me.Label5)
        Me.All_St_Panel.Controls.Add(Me.ALL_QTY_txt)
        Me.All_St_Panel.Location = New System.Drawing.Point(2, 185)
        Me.All_St_Panel.Name = "All_St_Panel"
        Me.All_St_Panel.Size = New System.Drawing.Size(306, 38)
        Me.All_St_Panel.TabIndex = 1027
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label5.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label5.Location = New System.Drawing.Point(177, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(119, 20)
        Me.Label5.TabIndex = 642
        Me.Label5.Text = "كمية كل المخازن :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ALL_QTY_txt
        '
        Me.ALL_QTY_txt.BackColor = System.Drawing.SystemColors.HighlightText
        Me.ALL_QTY_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ALL_QTY_txt.Font = New System.Drawing.Font("Times New Roman", 12.75!)
        Me.ALL_QTY_txt.ForeColor = System.Drawing.Color.Firebrick
        Me.ALL_QTY_txt.Location = New System.Drawing.Point(3, 3)
        Me.ALL_QTY_txt.Name = "ALL_QTY_txt"
        Me.ALL_QTY_txt.ReadOnly = True
        Me.ALL_QTY_txt.Size = New System.Drawing.Size(153, 27)
        Me.ALL_QTY_txt.TabIndex = 645
        Me.ALL_QTY_txt.Text = "00"
        Me.ALL_QTY_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel8
        '
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.Label26)
        Me.Panel8.Controls.Add(Me.Current_QTY)
        Me.Panel8.Location = New System.Drawing.Point(2, 146)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(306, 38)
        Me.Panel8.TabIndex = 1026
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label26.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label26.Location = New System.Drawing.Point(164, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label26.Size = New System.Drawing.Size(137, 20)
        Me.Label26.TabIndex = 643
        Me.Label26.Text = "كمية المخزن الحالي :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Current_QTY
        '
        Me.Current_QTY.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Current_QTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Current_QTY.Font = New System.Drawing.Font("Times New Roman", 12.75!)
        Me.Current_QTY.ForeColor = System.Drawing.Color.Firebrick
        Me.Current_QTY.Location = New System.Drawing.Point(3, 3)
        Me.Current_QTY.Name = "Current_QTY"
        Me.Current_QTY.ReadOnly = True
        Me.Current_QTY.Size = New System.Drawing.Size(153, 27)
        Me.Current_QTY.TabIndex = 643
        Me.Current_QTY.Text = "00"
        Me.Current_QTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label27)
        Me.Panel4.Controls.Add(Me.ST_cm)
        Me.Panel4.Location = New System.Drawing.Point(2, 98)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(306, 35)
        Me.Panel4.TabIndex = 1044
        '
        'Label27
        '
        Me.Label27.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label27.Location = New System.Drawing.Point(244, 6)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(59, 20)
        Me.Label27.TabIndex = 684
        Me.Label27.Text = "المخزن :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ST_cm
        '
        Me.ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(3, 3)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.Size = New System.Drawing.Size(238, 28)
        Me.ST_cm.TabIndex = 0
        '
        'ADDCatButton
        '
        Me.ADDCatButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ADDCatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ADDCatButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADDCatButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ADDCatButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ADDCatButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADDCatButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADDCatButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ADDCatButton.Location = New System.Drawing.Point(4, 373)
        Me.ADDCatButton.Name = "ADDCatButton"
        Me.ADDCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ADDCatButton.Size = New System.Drawing.Size(446, 55)
        Me.ADDCatButton.TabIndex = 1045
        Me.ADDCatButton.TabStop = False
        Me.ADDCatButton.Tag = "GENERAL"
        Me.ADDCatButton.Text = "➕ إضافة للفاتورة"
        Me.ADDCatButton.UseVisualStyleBackColor = False
        '
        'Exit_Btn
        '
        Me.Exit_Btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Exit_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Exit_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Exit_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Exit_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Exit_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Exit_Btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Exit_Btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Exit_Btn.Location = New System.Drawing.Point(456, 373)
        Me.Exit_Btn.Name = "Exit_Btn"
        Me.Exit_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Exit_Btn.Size = New System.Drawing.Size(264, 55)
        Me.Exit_Btn.TabIndex = 1046
        Me.Exit_Btn.TabStop = False
        Me.Exit_Btn.Tag = "DELETE"
        Me.Exit_Btn.Text = "رجوع ◀️"
        Me.Exit_Btn.UseVisualStyleBackColor = False
        '
        'LAST_SELL_Lb
        '
        Me.LAST_SELL_Lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LAST_SELL_Lb.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LAST_SELL_Lb.ForeColor = System.Drawing.Color.Black
        Me.LAST_SELL_Lb.Location = New System.Drawing.Point(336, 318)
        Me.LAST_SELL_Lb.Name = "LAST_SELL_Lb"
        Me.LAST_SELL_Lb.Size = New System.Drawing.Size(221, 39)
        Me.LAST_SELL_Lb.TabIndex = 1062
        Me.LAST_SELL_Lb.Text = "أخر بيع للزبون:"
        Me.LAST_SELL_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Min_SP_Panel
        '
        Me.Min_SP_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Min_SP_Panel.Controls.Add(Me.Min_SP_2_CB)
        Me.Min_SP_Panel.Controls.Add(Me.Min_SP_CB)
        Me.Min_SP_Panel.Location = New System.Drawing.Point(320, 146)
        Me.Min_SP_Panel.Name = "Min_SP_Panel"
        Me.Min_SP_Panel.Size = New System.Drawing.Size(237, 36)
        Me.Min_SP_Panel.TabIndex = 1060
        '
        'Min_SP_2_CB
        '
        Me.Min_SP_2_CB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Min_SP_2_CB.AutoSize = True
        Me.Min_SP_2_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Min_SP_2_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Min_SP_2_CB.Location = New System.Drawing.Point(7, 5)
        Me.Min_SP_2_CB.Name = "Min_SP_2_CB"
        Me.Min_SP_2_CB.Size = New System.Drawing.Size(103, 24)
        Me.Min_SP_2_CB.TabIndex = 674
        Me.Min_SP_2_CB.Text = "جملة الجملة"
        Me.Min_SP_2_CB.UseVisualStyleBackColor = True
        '
        'Min_SP_CB
        '
        Me.Min_SP_CB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Min_SP_CB.AutoSize = True
        Me.Min_SP_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Min_SP_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Min_SP_CB.Location = New System.Drawing.Point(128, 5)
        Me.Min_SP_CB.Name = "Min_SP_CB"
        Me.Min_SP_CB.Size = New System.Drawing.Size(100, 24)
        Me.Min_SP_CB.TabIndex = 675
        Me.Min_SP_CB.Text = "سعر الجملة"
        Me.Min_SP_CB.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Label10)
        Me.Panel7.Controls.Add(Me.IM_Unit_cm)
        Me.Panel7.Location = New System.Drawing.Point(336, 274)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(221, 42)
        Me.Panel7.TabIndex = 1058
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label10.Location = New System.Drawing.Point(140, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 20)
        Me.Label10.TabIndex = 616
        Me.Label10.Text = "الوحدة :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IM_Unit_cm
        '
        Me.IM_Unit_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Unit_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IM_Unit_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Unit_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Unit_cm.FormattingEnabled = True
        Me.IM_Unit_cm.Location = New System.Drawing.Point(3, 5)
        Me.IM_Unit_cm.Name = "IM_Unit_cm"
        Me.IM_Unit_cm.Size = New System.Drawing.Size(131, 28)
        Me.IM_Unit_cm.TabIndex = 615
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.Label11)
        Me.Panel6.Controls.Add(Me.PriceTextBox)
        Me.Panel6.Location = New System.Drawing.Point(177, 274)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(158, 42)
        Me.Panel6.TabIndex = 1057
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label11.Location = New System.Drawing.Point(101, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 20)
        Me.Label11.TabIndex = 617
        Me.Label11.Text = "السعر :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PriceTextBox
        '
        Me.PriceTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.PriceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PriceTextBox.Font = New System.Drawing.Font("Stencil", 14.0!)
        Me.PriceTextBox.ForeColor = System.Drawing.Color.DarkGreen
        Me.PriceTextBox.Location = New System.Drawing.Point(2, 4)
        Me.PriceTextBox.MaxLength = 250
        Me.PriceTextBox.Name = "PriceTextBox"
        Me.PriceTextBox.ReadOnly = True
        Me.PriceTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PriceTextBox.Size = New System.Drawing.Size(95, 30)
        Me.PriceTextBox.TabIndex = 292
        Me.PriceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label12)
        Me.Panel5.Controls.Add(Me.QtyTextBox)
        Me.Panel5.Location = New System.Drawing.Point(4, 274)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(171, 42)
        Me.Panel5.TabIndex = 1056
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label12.Location = New System.Drawing.Point(103, 8)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 20)
        Me.Label12.TabIndex = 604
        Me.Label12.Text = "الكمية :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'QtyTextBox
        '
        Me.QtyTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.QtyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.QtyTextBox.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QtyTextBox.ForeColor = System.Drawing.Color.Black
        Me.QtyTextBox.Location = New System.Drawing.Point(2, 3)
        Me.QtyTextBox.MaxLength = 250
        Me.QtyTextBox.Name = "QtyTextBox"
        Me.QtyTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.QtyTextBox.Size = New System.Drawing.Size(97, 32)
        Me.QtyTextBox.TabIndex = 390
        Me.QtyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Valid_Panel
        '
        Me.Valid_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Valid_Panel.Controls.Add(Me.Valid_cm)
        Me.Valid_Panel.Controls.Add(Me.Valid_QTY_txt)
        Me.Valid_Panel.Location = New System.Drawing.Point(320, 185)
        Me.Valid_Panel.Name = "Valid_Panel"
        Me.Valid_Panel.Size = New System.Drawing.Size(237, 36)
        Me.Valid_Panel.TabIndex = 1051
        '
        'Valid_cm
        '
        Me.Valid_cm.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Valid_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Valid_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Valid_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Valid_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Valid_cm.FormattingEnabled = True
        Me.Valid_cm.Location = New System.Drawing.Point(107, 4)
        Me.Valid_cm.Name = "Valid_cm"
        Me.Valid_cm.Size = New System.Drawing.Size(118, 28)
        Me.Valid_cm.TabIndex = 639
        '
        'Valid_QTY_txt
        '
        Me.Valid_QTY_txt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Valid_QTY_txt.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Valid_QTY_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Valid_QTY_txt.Enabled = False
        Me.Valid_QTY_txt.Font = New System.Drawing.Font("Times New Roman", 11.25!)
        Me.Valid_QTY_txt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Valid_QTY_txt.Location = New System.Drawing.Point(5, 5)
        Me.Valid_QTY_txt.Name = "Valid_QTY_txt"
        Me.Valid_QTY_txt.Size = New System.Drawing.Size(96, 25)
        Me.Valid_QTY_txt.TabIndex = 640
        Me.Valid_QTY_txt.Text = "00"
        Me.Valid_QTY_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Serial_Code_Panel
        '
        Me.Serial_Code_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Serial_Code_Panel.Controls.Add(Me.Serial_Code_txt)
        Me.Serial_Code_Panel.Controls.Add(Me.Label21)
        Me.Serial_Code_Panel.Location = New System.Drawing.Point(4, 318)
        Me.Serial_Code_Panel.Name = "Serial_Code_Panel"
        Me.Serial_Code_Panel.Size = New System.Drawing.Size(331, 39)
        Me.Serial_Code_Panel.TabIndex = 1053
        '
        'Serial_Code_txt
        '
        Me.Serial_Code_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Serial_Code_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Serial_Code_txt.Location = New System.Drawing.Point(2, 4)
        Me.Serial_Code_txt.Name = "Serial_Code_txt"
        Me.Serial_Code_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Serial_Code_txt.Size = New System.Drawing.Size(241, 27)
        Me.Serial_Code_txt.TabIndex = 673
        Me.Serial_Code_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label21.Location = New System.Drawing.Point(250, 8)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(75, 20)
        Me.Label21.TabIndex = 674
        Me.Label21.Text = " التسلسل :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ST_Bercent_Panel
        '
        Me.ST_Bercent_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ST_Bercent_Panel.Controls.Add(Me.Bercent_TXT)
        Me.ST_Bercent_Panel.Controls.Add(Me.Label19)
        Me.ST_Bercent_Panel.Location = New System.Drawing.Point(2, 224)
        Me.ST_Bercent_Panel.Name = "ST_Bercent_Panel"
        Me.ST_Bercent_Panel.Size = New System.Drawing.Size(306, 36)
        Me.ST_Bercent_Panel.TabIndex = 1063
        '
        'Bercent_TXT
        '
        Me.Bercent_TXT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Bercent_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Bercent_TXT.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Bercent_TXT.Location = New System.Drawing.Point(3, 4)
        Me.Bercent_TXT.Name = "Bercent_TXT"
        Me.Bercent_TXT.ReadOnly = True
        Me.Bercent_TXT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Bercent_TXT.Size = New System.Drawing.Size(153, 27)
        Me.Bercent_TXT.TabIndex = 673
        Me.Bercent_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label19.Location = New System.Drawing.Point(166, 7)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(118, 20)
        Me.Label19.TabIndex = 674
        Me.Label19.Text = "نسبة متغيرات % :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Barcode_SH_txt
        '
        Me.Barcode_SH_txt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barcode_SH_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_SH_txt.Location = New System.Drawing.Point(320, 98)
        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
        Me.Barcode_SH_txt.Size = New System.Drawing.Size(300, 29)
        Me.Barcode_SH_txt.TabIndex = 1064
        Me.Barcode_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label1.Location = New System.Drawing.Point(623, 103)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 20)
        Me.Label1.TabIndex = 1065
        Me.Label1.Text = "باركود ميزان :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'mySearchControl
        '
        Me.mySearchControl.BackColor = System.Drawing.Color.WhiteSmoke
        Me.mySearchControl.DefaultSearchField = "إسم الصنف"
        Me.mySearchControl.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.mySearchControl.ItemsTable = Nothing
        Me.mySearchControl.itemsTable_Barcode = Nothing
        Me.mySearchControl.Location = New System.Drawing.Point(5, 58)
        Me.mySearchControl.Margin = New System.Windows.Forms.Padding(0)
        Me.mySearchControl.MarginBetweenSearchAndGrid = 15
        Me.mySearchControl.MaxGridHeight = 400
        Me.mySearchControl.Name = "mySearchControl"
        Me.mySearchControl.Size = New System.Drawing.Size(716, 35)
        Me.mySearchControl.TabIndex = 1047
        '
        'SB_IM_card
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 436)
        Me.ControlBox = False
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Barcode_SH_txt)
        Me.Controls.Add(Me.ST_Bercent_Panel)
        Me.Controls.Add(Me.LAST_SELL_Lb)
        Me.Controls.Add(Me.Min_SP_Panel)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Valid_Panel)
        Me.Controls.Add(Me.Serial_Code_Panel)
        Me.Controls.Add(Me.mySearchControl)
        Me.Controls.Add(Me.Exit_Btn)
        Me.Controls.Add(Me.ADDCatButton)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.All_St_Panel)
        Me.Controls.Add(Me.Panel8)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SB_IM_card"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الصنف"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.All_St_Panel.ResumeLayout(False)
        Me.All_St_Panel.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Min_SP_Panel.ResumeLayout(False)
        Me.Min_SP_Panel.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Valid_Panel.ResumeLayout(False)
        Me.Valid_Panel.PerformLayout()
        Me.Serial_Code_Panel.ResumeLayout(False)
        Me.Serial_Code_Panel.PerformLayout()
        Me.ST_Bercent_Panel.ResumeLayout(False)
        Me.ST_Bercent_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents All_St_Panel As Panel
    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents Title_LB As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Label5 As Label
    Friend WithEvents ALL_QTY_txt As TextBox
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label26 As Label
    Friend WithEvents Current_QTY As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label27 As Label
    Friend WithEvents ST_cm As ComboBox
    Friend WithEvents ADDCatButton As Button
    Friend WithEvents Exit_Btn As Button
    Friend WithEvents mySearchControl As SearchItemControl
    Friend WithEvents LAST_SELL_Lb As Label
    Friend WithEvents Min_SP_Panel As Panel
    Friend WithEvents Min_SP_2_CB As CheckBox
    Friend WithEvents Min_SP_CB As CheckBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents IM_Unit_cm As ComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents PriceTextBox As TextBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents QtyTextBox As TextBox
    Friend WithEvents Valid_Panel As Panel
    Friend WithEvents Valid_cm As ComboBox
    Friend WithEvents Valid_QTY_txt As TextBox
    Friend WithEvents Serial_Code_Panel As Panel
    Friend WithEvents Serial_Code_txt As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents ST_Bercent_Panel As Panel
    Friend WithEvents Bercent_TXT As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Barcode_SH_txt As TextBox
    Friend WithEvents Label1 As Label
End Class
