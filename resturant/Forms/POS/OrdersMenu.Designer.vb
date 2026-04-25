<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrdersMenu
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
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle36 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle37 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle38 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle39 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle40 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Search_txt = New System.Windows.Forms.TextBox()
        Me.TypeCmb = New System.Windows.Forms.ComboBox()
        Me.Bar_CB = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Bill_Panel = New System.Windows.Forms.Panel()
        Me.Cr_Phone_Txt = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Open_butt = New System.Windows.Forms.Button()
        Me.DiscountPanel = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TotalTextBox = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.DiscountTextBox = New System.Windows.Forms.TextBox()
        Me.OpenCahDR_Btn = New System.Windows.Forms.Button()
        Me.Cancel_Recive_btn = New System.Windows.Forms.Button()
        Me.SB_day_Bill_txt = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Ag_phone_txt = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Print_Costmer_Bill_btn = New System.Windows.Forms.Button()
        Me.isVoid_LB = New System.Windows.Forms.Label()
        Me.Pure_txt = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.VoidBill_Btn = New System.Windows.Forms.Button()
        Me.Print_IN_btn = New System.Windows.Forms.Button()
        Me.MetroGrid = New MetroFramework.Controls.MetroGrid()
        Me.IM_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_T_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_NameCL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UserName_txt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Barcode_txt = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.AG_Name_txt = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.OrdersGrid = New Zuby.ADGV.AdvancedDataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Details_Panel = New System.Windows.Forms.Panel()
        Me.Deliver_LB = New System.Windows.Forms.Label()
        Me.Edit_Date_btn = New System.Windows.Forms.Button()
        Me.Deliver_Date = New System.Windows.Forms.DateTimePicker()
        Me.Bill_Date_txt = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Notes_txt = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Cash_btn = New System.Windows.Forms.Button()
        Me.OrderDeliver_btn = New System.Windows.Forms.Button()
        Me.Rest_txt = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Pied_txt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Show_IM_btn = New System.Windows.Forms.Button()
        Me.DataB = New System.Windows.Forms.BindingSource(Me.components)
        Me.Only_IM_View_CB = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Bill_Panel.SuspendLayout()
        Me.DiscountPanel.SuspendLayout()
        CType(Me.MetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OrdersGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Details_Panel.SuspendLayout()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Search_txt
        '
        Me.Search_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Search_txt.Font = New System.Drawing.Font("Segoe UI", 14.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Search_txt.Location = New System.Drawing.Point(501, 2)
        Me.Search_txt.Margin = New System.Windows.Forms.Padding(5)
        Me.Search_txt.Name = "Search_txt"
        Me.Search_txt.Size = New System.Drawing.Size(377, 34)
        Me.Search_txt.TabIndex = 549
        '
        'TypeCmb
        '
        Me.TypeCmb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TypeCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypeCmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TypeCmb.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.TypeCmb.FormattingEnabled = True
        Me.TypeCmb.Items.AddRange(New Object() {"المعلقة", "تم الإستلام", "كل الطلبيات"})
        Me.TypeCmb.Location = New System.Drawing.Point(80, 4)
        Me.TypeCmb.Name = "TypeCmb"
        Me.TypeCmb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TypeCmb.Size = New System.Drawing.Size(142, 31)
        Me.TypeCmb.TabIndex = 551
        '
        'Bar_CB
        '
        Me.Bar_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Bar_CB.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Bar_CB.Location = New System.Drawing.Point(379, 6)
        Me.Bar_CB.Name = "Bar_CB"
        Me.Bar_CB.Size = New System.Drawing.Size(79, 27)
        Me.Bar_CB.TabIndex = 565
        Me.Bar_CB.Text = "باركود"
        Me.Bar_CB.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(881, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 34)
        Me.Label1.TabIndex = 567
        Me.Label1.Text = "بحــث"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Bill_Panel
        '
        Me.Bill_Panel.Controls.Add(Me.Button1)
        Me.Bill_Panel.Controls.Add(Me.Cr_Phone_Txt)
        Me.Bill_Panel.Controls.Add(Me.Label15)
        Me.Bill_Panel.Controls.Add(Me.Open_butt)
        Me.Bill_Panel.Controls.Add(Me.DiscountPanel)
        Me.Bill_Panel.Controls.Add(Me.OpenCahDR_Btn)
        Me.Bill_Panel.Controls.Add(Me.Cancel_Recive_btn)
        Me.Bill_Panel.Controls.Add(Me.SB_day_Bill_txt)
        Me.Bill_Panel.Controls.Add(Me.Label12)
        Me.Bill_Panel.Controls.Add(Me.Ag_phone_txt)
        Me.Bill_Panel.Controls.Add(Me.Label11)
        Me.Bill_Panel.Controls.Add(Me.Print_Costmer_Bill_btn)
        Me.Bill_Panel.Controls.Add(Me.isVoid_LB)
        Me.Bill_Panel.Controls.Add(Me.Pure_txt)
        Me.Bill_Panel.Controls.Add(Me.Label10)
        Me.Bill_Panel.Controls.Add(Me.VoidBill_Btn)
        Me.Bill_Panel.Controls.Add(Me.Print_IN_btn)
        Me.Bill_Panel.Controls.Add(Me.MetroGrid)
        Me.Bill_Panel.Controls.Add(Me.UserName_txt)
        Me.Bill_Panel.Controls.Add(Me.Label9)
        Me.Bill_Panel.Controls.Add(Me.Barcode_txt)
        Me.Bill_Panel.Controls.Add(Me.Label8)
        Me.Bill_Panel.Controls.Add(Me.AG_Name_txt)
        Me.Bill_Panel.Controls.Add(Me.Label7)
        Me.Bill_Panel.Location = New System.Drawing.Point(1, 44)
        Me.Bill_Panel.Name = "Bill_Panel"
        Me.Bill_Panel.Size = New System.Drawing.Size(724, 472)
        Me.Bill_Panel.TabIndex = 569
        '
        'Cr_Phone_Txt
        '
        Me.Cr_Phone_Txt.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Cr_Phone_Txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Cr_Phone_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Cr_Phone_Txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Cr_Phone_Txt.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.Cr_Phone_Txt.Location = New System.Drawing.Point(3, 14)
        Me.Cr_Phone_Txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Cr_Phone_Txt.Name = "Cr_Phone_Txt"
        Me.Cr_Phone_Txt.ReadOnly = True
        Me.Cr_Phone_Txt.Size = New System.Drawing.Size(337, 29)
        Me.Cr_Phone_Txt.TabIndex = 645
        Me.Cr_Phone_Txt.Tag = "3"
        Me.Cr_Phone_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(343, 14)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(79, 27)
        Me.Label15.TabIndex = 644
        Me.Label15.Text = "إسم/هاتف"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Open_butt
        '
        Me.Open_butt.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Open_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Open_butt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Open_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Open_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Open_butt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Open_butt.ForeColor = System.Drawing.Color.Black
        Me.Open_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Open_butt.Location = New System.Drawing.Point(226, 429)
        Me.Open_butt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Open_butt.Name = "Open_butt"
        Me.Open_butt.Size = New System.Drawing.Size(100, 40)
        Me.Open_butt.TabIndex = 643
        Me.Open_butt.Text = "فتح"
        Me.Open_butt.UseVisualStyleBackColor = False
        '
        'DiscountPanel
        '
        Me.DiscountPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.DiscountPanel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.DiscountPanel.Controls.Add(Me.Label13)
        Me.DiscountPanel.Controls.Add(Me.TotalTextBox)
        Me.DiscountPanel.Controls.Add(Me.Label14)
        Me.DiscountPanel.Controls.Add(Me.DiscountTextBox)
        Me.DiscountPanel.Location = New System.Drawing.Point(219, 391)
        Me.DiscountPanel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DiscountPanel.Name = "DiscountPanel"
        Me.DiscountPanel.Size = New System.Drawing.Size(491, 31)
        Me.DiscountPanel.TabIndex = 642
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(415, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label13.Size = New System.Drawing.Size(59, 19)
        Me.Label13.TabIndex = 454
        Me.Label13.Text = "الإجمالي"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TotalTextBox
        '
        Me.TotalTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.TotalTextBox.BackColor = System.Drawing.SystemColors.Window
        Me.TotalTextBox.Font = New System.Drawing.Font("Stencil", 12.0!)
        Me.TotalTextBox.Location = New System.Drawing.Point(266, 3)
        Me.TotalTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TotalTextBox.Name = "TotalTextBox"
        Me.TotalTextBox.ReadOnly = True
        Me.TotalTextBox.Size = New System.Drawing.Size(140, 26)
        Me.TotalTextBox.TabIndex = 451
        Me.TotalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(164, 6)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label14.Size = New System.Drawing.Size(62, 19)
        Me.Label14.TabIndex = 452
        Me.Label14.Text = "التخفيض"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DiscountTextBox
        '
        Me.DiscountTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.DiscountTextBox.Font = New System.Drawing.Font("Stencil", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DiscountTextBox.Location = New System.Drawing.Point(13, 2)
        Me.DiscountTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DiscountTextBox.Name = "DiscountTextBox"
        Me.DiscountTextBox.ReadOnly = True
        Me.DiscountTextBox.Size = New System.Drawing.Size(148, 26)
        Me.DiscountTextBox.TabIndex = 453
        Me.DiscountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OpenCahDR_Btn
        '
        Me.OpenCahDR_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.OpenCahDR_Btn.BackColor = System.Drawing.SystemColors.Menu
        Me.OpenCahDR_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OpenCahDR_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.OpenCahDR_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OpenCahDR_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpenCahDR_Btn.ForeColor = System.Drawing.Color.Black
        Me.OpenCahDR_Btn.Location = New System.Drawing.Point(51, 429)
        Me.OpenCahDR_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.OpenCahDR_Btn.Name = "OpenCahDR_Btn"
        Me.OpenCahDR_Btn.Size = New System.Drawing.Size(66, 40)
        Me.OpenCahDR_Btn.TabIndex = 641
        Me.OpenCahDR_Btn.Text = "🗄️"
        Me.OpenCahDR_Btn.UseVisualStyleBackColor = False
        '
        'Cancel_Recive_btn
        '
        Me.Cancel_Recive_btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Cancel_Recive_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Cancel_Recive_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancel_Recive_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cancel_Recive_btn.Enabled = False
        Me.Cancel_Recive_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Cancel_Recive_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Cancel_Recive_btn.ForeColor = System.Drawing.Color.Black
        Me.Cancel_Recive_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Cancel_Recive_btn.Location = New System.Drawing.Point(118, 429)
        Me.Cancel_Recive_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Cancel_Recive_btn.Name = "Cancel_Recive_btn"
        Me.Cancel_Recive_btn.Size = New System.Drawing.Size(107, 40)
        Me.Cancel_Recive_btn.TabIndex = 599
        Me.Cancel_Recive_btn.Text = "إستعاده    "
        Me.Cancel_Recive_btn.UseVisualStyleBackColor = False
        '
        'SB_day_Bill_txt
        '
        Me.SB_day_Bill_txt.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.SB_day_Bill_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SB_day_Bill_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SB_day_Bill_txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.SB_day_Bill_txt.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.SB_day_Bill_txt.Location = New System.Drawing.Point(428, 79)
        Me.SB_day_Bill_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SB_day_Bill_txt.Name = "SB_day_Bill_txt"
        Me.SB_day_Bill_txt.ReadOnly = True
        Me.SB_day_Bill_txt.Size = New System.Drawing.Size(203, 29)
        Me.SB_day_Bill_txt.TabIndex = 598
        Me.SB_day_Bill_txt.Tag = "3"
        Me.SB_day_Bill_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(634, 78)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(74, 30)
        Me.Label12.TabIndex = 597
        Me.Label12.Text = "رقم الفاتورة"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Ag_phone_txt
        '
        Me.Ag_phone_txt.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Ag_phone_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Ag_phone_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Ag_phone_txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Ag_phone_txt.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.Ag_phone_txt.Location = New System.Drawing.Point(171, 79)
        Me.Ag_phone_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Ag_phone_txt.Name = "Ag_phone_txt"
        Me.Ag_phone_txt.ReadOnly = True
        Me.Ag_phone_txt.Size = New System.Drawing.Size(169, 29)
        Me.Ag_phone_txt.TabIndex = 596
        Me.Ag_phone_txt.Tag = "3"
        Me.Ag_phone_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(342, 78)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(43, 29)
        Me.Label11.TabIndex = 595
        Me.Label11.Text = "هاتف"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Print_Costmer_Bill_btn
        '
        Me.Print_Costmer_Bill_btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Print_Costmer_Bill_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Print_Costmer_Bill_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Print_Costmer_Bill_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_Costmer_Bill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Print_Costmer_Bill_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Print_Costmer_Bill_btn.ForeColor = System.Drawing.Color.Black
        Me.Print_Costmer_Bill_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_Costmer_Bill_btn.Location = New System.Drawing.Point(437, 429)
        Me.Print_Costmer_Bill_btn.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Print_Costmer_Bill_btn.Name = "Print_Costmer_Bill_btn"
        Me.Print_Costmer_Bill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Costmer_Bill_btn.Size = New System.Drawing.Size(136, 40)
        Me.Print_Costmer_Bill_btn.TabIndex = 594
        Me.Print_Costmer_Bill_btn.Tag = "PRINT"
        Me.Print_Costmer_Bill_btn.Text = "طباعة فاتورة زبون"
        Me.Print_Costmer_Bill_btn.UseVisualStyleBackColor = False
        '
        'isVoid_LB
        '
        Me.isVoid_LB.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.isVoid_LB.BackColor = System.Drawing.Color.IndianRed
        Me.isVoid_LB.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isVoid_LB.ForeColor = System.Drawing.Color.Snow
        Me.isVoid_LB.Location = New System.Drawing.Point(3, 78)
        Me.isVoid_LB.Name = "isVoid_LB"
        Me.isVoid_LB.Size = New System.Drawing.Size(165, 30)
        Me.isVoid_LB.TabIndex = 593
        Me.isVoid_LB.Text = "فاتــورة ملغيــة"
        Me.isVoid_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.isVoid_LB.Visible = False
        '
        'Pure_txt
        '
        Me.Pure_txt.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Pure_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Pure_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pure_txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Pure_txt.Font = New System.Drawing.Font("Stencil", 18.75!)
        Me.Pure_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Pure_txt.Location = New System.Drawing.Point(1, 388)
        Me.Pure_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Pure_txt.Name = "Pure_txt"
        Me.Pure_txt.ReadOnly = True
        Me.Pure_txt.Size = New System.Drawing.Size(146, 37)
        Me.Pure_txt.TabIndex = 592
        Me.Pure_txt.Tag = "3"
        Me.Pure_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(150, 396)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 20)
        Me.Label10.TabIndex = 591
        Me.Label10.Text = "الصافي"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'VoidBill_Btn
        '
        Me.VoidBill_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.VoidBill_Btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.VoidBill_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.VoidBill_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.VoidBill_Btn.Enabled = False
        Me.VoidBill_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.VoidBill_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.VoidBill_Btn.ForeColor = System.Drawing.Color.Black
        Me.VoidBill_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.VoidBill_Btn.Location = New System.Drawing.Point(327, 429)
        Me.VoidBill_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.VoidBill_Btn.Name = "VoidBill_Btn"
        Me.VoidBill_Btn.Size = New System.Drawing.Size(109, 40)
        Me.VoidBill_Btn.TabIndex = 589
        Me.VoidBill_Btn.Text = "إلغاء الفاتورة"
        Me.VoidBill_Btn.UseVisualStyleBackColor = False
        '
        'Print_IN_btn
        '
        Me.Print_IN_btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Print_IN_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Print_IN_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Print_IN_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_IN_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Print_IN_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Print_IN_btn.ForeColor = System.Drawing.Color.Black
        Me.Print_IN_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_IN_btn.Location = New System.Drawing.Point(574, 429)
        Me.Print_IN_btn.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Print_IN_btn.Name = "Print_IN_btn"
        Me.Print_IN_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_IN_btn.Size = New System.Drawing.Size(146, 40)
        Me.Print_IN_btn.TabIndex = 590
        Me.Print_IN_btn.Text = "طباعة فاتورة داخلية"
        Me.Print_IN_btn.UseVisualStyleBackColor = False
        '
        'MetroGrid
        '
        Me.MetroGrid.AllowUserToAddRows = False
        Me.MetroGrid.AllowUserToDeleteRows = False
        Me.MetroGrid.AllowUserToResizeRows = False
        Me.MetroGrid.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.MetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MetroGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MetroGrid.CausesValidation = False
        Me.MetroGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.MetroGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle31.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle31.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle31.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle31
        Me.MetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_ID, Me.IM_T_ID, Me.IM_NameCL, Me.QTY_CL, Me.Unit_CL, Me.Unit_Price_CL, Me.Total_CL})
        Me.MetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle34.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle34.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle34.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle34.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle34.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid.DefaultCellStyle = DataGridViewCellStyle34
        Me.MetroGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.MetroGrid.EnableHeadersVisualStyles = False
        Me.MetroGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.MetroGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroGrid.Location = New System.Drawing.Point(3, 110)
        Me.MetroGrid.MultiSelect = False
        Me.MetroGrid.Name = "MetroGrid"
        Me.MetroGrid.ReadOnly = True
        Me.MetroGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MetroGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle35.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle35.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle35.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle35.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle35.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle35
        Me.MetroGrid.RowHeadersVisible = False
        Me.MetroGrid.RowHeadersWidth = 100
        Me.MetroGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle36.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MetroGrid.RowsDefaultCellStyle = DataGridViewCellStyle36
        Me.MetroGrid.RowTemplate.Height = 35
        Me.MetroGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.MetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MetroGrid.Size = New System.Drawing.Size(718, 276)
        Me.MetroGrid.TabIndex = 588
        Me.MetroGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'IM_ID
        '
        Me.IM_ID.DataPropertyName = "IM_ID"
        Me.IM_ID.HeaderText = "IM_ID"
        Me.IM_ID.Name = "IM_ID"
        Me.IM_ID.ReadOnly = True
        Me.IM_ID.Visible = False
        '
        'IM_T_ID
        '
        Me.IM_T_ID.DataPropertyName = "T_ID"
        Me.IM_T_ID.HeaderText = "ر.الآلي"
        Me.IM_T_ID.Name = "IM_T_ID"
        Me.IM_T_ID.ReadOnly = True
        Me.IM_T_ID.Visible = False
        '
        'IM_NameCL
        '
        Me.IM_NameCL.DataPropertyName = "IM_Name"
        Me.IM_NameCL.FillWeight = 170.7813!
        Me.IM_NameCL.HeaderText = "الصنف"
        Me.IM_NameCL.Name = "IM_NameCL"
        Me.IM_NameCL.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        Me.QTY_CL.FillWeight = 79.91894!
        Me.QTY_CL.HeaderText = "العدد"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'Unit_CL
        '
        Me.Unit_CL.DataPropertyName = "Unit_Name"
        DataGridViewCellStyle32.ForeColor = System.Drawing.Color.Gray
        Me.Unit_CL.DefaultCellStyle = DataGridViewCellStyle32
        Me.Unit_CL.HeaderText = "الوحدة"
        Me.Unit_CL.Name = "Unit_CL"
        Me.Unit_CL.ReadOnly = True
        Me.Unit_CL.Visible = False
        '
        'Unit_Price_CL
        '
        Me.Unit_Price_CL.DataPropertyName = "Price"
        Me.Unit_Price_CL.HeaderText = "السعر"
        Me.Unit_Price_CL.Name = "Unit_Price_CL"
        Me.Unit_Price_CL.ReadOnly = True
        Me.Unit_Price_CL.ToolTipText = "السعر"
        '
        'Total_CL
        '
        Me.Total_CL.DataPropertyName = "T_Price"
        DataGridViewCellStyle33.Format = "N2"
        DataGridViewCellStyle33.NullValue = Nothing
        Me.Total_CL.DefaultCellStyle = DataGridViewCellStyle33
        Me.Total_CL.FillWeight = 79.91894!
        Me.Total_CL.HeaderText = "الإجمالي"
        Me.Total_CL.Name = "Total_CL"
        Me.Total_CL.ReadOnly = True
        '
        'UserName_txt
        '
        Me.UserName_txt.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.UserName_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.UserName_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UserName_txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.UserName_txt.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.UserName_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.UserName_txt.Location = New System.Drawing.Point(171, 48)
        Me.UserName_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.UserName_txt.Name = "UserName_txt"
        Me.UserName_txt.ReadOnly = True
        Me.UserName_txt.Size = New System.Drawing.Size(169, 29)
        Me.UserName_txt.TabIndex = 587
        Me.UserName_txt.Tag = "3"
        Me.UserName_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(343, 47)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 33)
        Me.Label9.TabIndex = 586
        Me.Label9.Text = "المدخل"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Barcode_txt
        '
        Me.Barcode_txt.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Barcode_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Barcode_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Barcode_txt.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.Barcode_txt.Location = New System.Drawing.Point(428, 47)
        Me.Barcode_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Barcode_txt.Name = "Barcode_txt"
        Me.Barcode_txt.ReadOnly = True
        Me.Barcode_txt.Size = New System.Drawing.Size(203, 29)
        Me.Barcode_txt.TabIndex = 585
        Me.Barcode_txt.Tag = "3"
        Me.Barcode_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(633, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 29)
        Me.Label8.TabIndex = 584
        Me.Label8.Text = "باركود"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AG_Name_txt
        '
        Me.AG_Name_txt.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.AG_Name_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.AG_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AG_Name_txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.AG_Name_txt.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.AG_Name_txt.Location = New System.Drawing.Point(428, 15)
        Me.AG_Name_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.AG_Name_txt.Name = "AG_Name_txt"
        Me.AG_Name_txt.ReadOnly = True
        Me.AG_Name_txt.Size = New System.Drawing.Size(203, 29)
        Me.AG_Name_txt.TabIndex = 583
        Me.AG_Name_txt.Tag = "3"
        Me.AG_Name_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(634, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 27)
        Me.Label7.TabIndex = 582
        Me.Label7.Text = "الزبون"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'OrdersGrid
        '
        Me.OrdersGrid.AllowUserToAddRows = False
        Me.OrdersGrid.AllowUserToDeleteRows = False
        Me.OrdersGrid.AllowUserToResizeRows = False
        Me.OrdersGrid.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.OrdersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.OrdersGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.OrdersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.OrdersGrid.CausesValidation = False
        Me.OrdersGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.OrdersGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle37.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle37.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle37.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle37.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle37.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.OrdersGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle37
        Me.OrdersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.OrdersGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle38.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle38.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle38.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle38.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle38.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.OrdersGrid.DefaultCellStyle = DataGridViewCellStyle38
        Me.OrdersGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.OrdersGrid.EnableHeadersVisualStyles = False
        Me.OrdersGrid.FilterAndSortEnabled = True
        Me.OrdersGrid.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.OrdersGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.OrdersGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.OrdersGrid.Location = New System.Drawing.Point(4, 39)
        Me.OrdersGrid.MultiSelect = False
        Me.OrdersGrid.Name = "OrdersGrid"
        Me.OrdersGrid.ReadOnly = True
        Me.OrdersGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.OrdersGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle39.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle39.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle39.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle39.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle39.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.OrdersGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle39
        Me.OrdersGrid.RowHeadersVisible = False
        Me.OrdersGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle40.Font = New System.Drawing.Font("Segoe UI", 13.75!)
        DataGridViewCellStyle40.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.OrdersGrid.RowsDefaultCellStyle = DataGridViewCellStyle40
        Me.OrdersGrid.RowTemplate.Height = 35
        Me.OrdersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.OrdersGrid.Size = New System.Drawing.Size(942, 10)
        Me.OrdersGrid.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.OrdersGrid.TabIndex = 644
        Me.OrdersGrid.Visible = False
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(4, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(208, 20)
        Me.Label2.TabIndex = 570
        Me.Label2.Text = "موعد التسليـــم"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Details_Panel
        '
        Me.Details_Panel.Controls.Add(Me.Deliver_LB)
        Me.Details_Panel.Controls.Add(Me.Edit_Date_btn)
        Me.Details_Panel.Controls.Add(Me.Deliver_Date)
        Me.Details_Panel.Controls.Add(Me.Bill_Date_txt)
        Me.Details_Panel.Controls.Add(Me.Label6)
        Me.Details_Panel.Controls.Add(Me.Notes_txt)
        Me.Details_Panel.Controls.Add(Me.Label5)
        Me.Details_Panel.Controls.Add(Me.Cash_btn)
        Me.Details_Panel.Controls.Add(Me.OrderDeliver_btn)
        Me.Details_Panel.Controls.Add(Me.Rest_txt)
        Me.Details_Panel.Controls.Add(Me.Label4)
        Me.Details_Panel.Controls.Add(Me.Pied_txt)
        Me.Details_Panel.Controls.Add(Me.Label3)
        Me.Details_Panel.Controls.Add(Me.Label2)
        Me.Details_Panel.Location = New System.Drawing.Point(728, 44)
        Me.Details_Panel.Name = "Details_Panel"
        Me.Details_Panel.Size = New System.Drawing.Size(218, 472)
        Me.Details_Panel.TabIndex = 572
        '
        'Deliver_LB
        '
        Me.Deliver_LB.BackColor = System.Drawing.Color.Transparent
        Me.Deliver_LB.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Deliver_LB.ForeColor = System.Drawing.Color.DarkRed
        Me.Deliver_LB.Location = New System.Drawing.Point(20, 78)
        Me.Deliver_LB.Name = "Deliver_LB"
        Me.Deliver_LB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Deliver_LB.Size = New System.Drawing.Size(75, 17)
        Me.Deliver_LB.TabIndex = 705
        Me.Deliver_LB.Text = "(غير مدرج)"
        Me.Deliver_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Deliver_LB.Visible = False
        '
        'Edit_Date_btn
        '
        Me.Edit_Date_btn.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Edit_Date_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Edit_Date_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Edit_Date_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Edit_Date_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Edit_Date_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Edit_Date_btn.ForeColor = System.Drawing.Color.Black
        Me.Edit_Date_btn.Location = New System.Drawing.Point(6, 99)
        Me.Edit_Date_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Edit_Date_btn.Name = "Edit_Date_btn"
        Me.Edit_Date_btn.Size = New System.Drawing.Size(33, 31)
        Me.Edit_Date_btn.TabIndex = 703
        Me.Edit_Date_btn.Text = "⚙️"
        Me.Edit_Date_btn.UseVisualStyleBackColor = False
        '
        'Deliver_Date
        '
        Me.Deliver_Date.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Deliver_Date.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Deliver_Date.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.Deliver_Date.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Deliver_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Deliver_Date.Location = New System.Drawing.Point(41, 102)
        Me.Deliver_Date.Name = "Deliver_Date"
        Me.Deliver_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Deliver_Date.RightToLeftLayout = True
        Me.Deliver_Date.ShowCheckBox = True
        Me.Deliver_Date.Size = New System.Drawing.Size(173, 24)
        Me.Deliver_Date.TabIndex = 704
        '
        'Bill_Date_txt
        '
        Me.Bill_Date_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Bill_Date_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Bill_Date_txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Bill_Date_txt.Font = New System.Drawing.Font("Segoe UI", 13.25!)
        Me.Bill_Date_txt.Location = New System.Drawing.Point(5, 39)
        Me.Bill_Date_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Bill_Date_txt.Name = "Bill_Date_txt"
        Me.Bill_Date_txt.ReadOnly = True
        Me.Bill_Date_txt.Size = New System.Drawing.Size(210, 31)
        Me.Bill_Date_txt.TabIndex = 581
        Me.Bill_Date_txt.Tag = "3"
        Me.Bill_Date_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(6, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(208, 20)
        Me.Label6.TabIndex = 580
        Me.Label6.Text = "تاريخ الفاتورة"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Notes_txt
        '
        Me.Notes_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Notes_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Notes_txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Notes_txt.Font = New System.Drawing.Font("Segoe UI", 11.5!)
        Me.Notes_txt.ForeColor = System.Drawing.Color.Blue
        Me.Notes_txt.Location = New System.Drawing.Point(4, 278)
        Me.Notes_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Notes_txt.Multiline = True
        Me.Notes_txt.Name = "Notes_txt"
        Me.Notes_txt.ReadOnly = True
        Me.Notes_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Notes_txt.Size = New System.Drawing.Size(210, 93)
        Me.Notes_txt.TabIndex = 579
        Me.Notes_txt.Tag = "3"
        Me.Notes_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(8, 252)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(206, 26)
        Me.Label5.TabIndex = 578
        Me.Label5.Text = "ملاحظـــات"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Cash_btn
        '
        Me.Cash_btn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Cash_btn.BackColor = System.Drawing.Color.White
        Me.Cash_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Cash_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cash_btn.Enabled = False
        Me.Cash_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Cash_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Cash_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Cash_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cash_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cash_btn.ForeColor = System.Drawing.Color.Black
        Me.Cash_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Cash_btn.Location = New System.Drawing.Point(3, 378)
        Me.Cash_btn.Name = "Cash_btn"
        Me.Cash_btn.Size = New System.Drawing.Size(213, 45)
        Me.Cash_btn.TabIndex = 577
        Me.Cash_btn.Text = "إيصــال دفع           💵"
        Me.Cash_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Cash_btn.UseVisualStyleBackColor = False
        '
        'OrderDeliver_btn
        '
        Me.OrderDeliver_btn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.OrderDeliver_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.OrderDeliver_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.OrderDeliver_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.OrderDeliver_btn.Enabled = False
        Me.OrderDeliver_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.OrderDeliver_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.OrderDeliver_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.OrderDeliver_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OrderDeliver_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OrderDeliver_btn.ForeColor = System.Drawing.Color.Black
        Me.OrderDeliver_btn.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.OrderDeliver_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OrderDeliver_btn.Location = New System.Drawing.Point(3, 425)
        Me.OrderDeliver_btn.Name = "OrderDeliver_btn"
        Me.OrderDeliver_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.OrderDeliver_btn.Size = New System.Drawing.Size(213, 45)
        Me.OrderDeliver_btn.TabIndex = 576
        Me.OrderDeliver_btn.Text = "تسليم الطلبية F12"
        Me.OrderDeliver_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OrderDeliver_btn.UseVisualStyleBackColor = False
        '
        'Rest_txt
        '
        Me.Rest_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Rest_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Rest_txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Rest_txt.Font = New System.Drawing.Font("Stencil", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rest_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Rest_txt.Location = New System.Drawing.Point(4, 215)
        Me.Rest_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Rest_txt.Name = "Rest_txt"
        Me.Rest_txt.ReadOnly = True
        Me.Rest_txt.Size = New System.Drawing.Size(210, 32)
        Me.Rest_txt.TabIndex = 575
        Me.Rest_txt.Tag = "3"
        Me.Rest_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(8, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(206, 22)
        Me.Label4.TabIndex = 574
        Me.Label4.Text = "المتبقي"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Pied_txt
        '
        Me.Pied_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Pied_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pied_txt.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Pied_txt.Font = New System.Drawing.Font("Stencil", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pied_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Pied_txt.Location = New System.Drawing.Point(4, 156)
        Me.Pied_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Pied_txt.Name = "Pied_txt"
        Me.Pied_txt.ReadOnly = True
        Me.Pied_txt.Size = New System.Drawing.Size(210, 32)
        Me.Pied_txt.TabIndex = 573
        Me.Pied_txt.Tag = "3"
        Me.Pied_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(29, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(184, 24)
        Me.Label3.TabIndex = 572
        Me.Label3.Text = "المدفوع"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Show_IM_btn
        '
        Me.Show_IM_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Show_IM_btn.BackColor = System.Drawing.Color.White
        Me.Show_IM_btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_download_173000
        Me.Show_IM_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_IM_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Show_IM_btn.Location = New System.Drawing.Point(461, 2)
        Me.Show_IM_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Show_IM_btn.Name = "Show_IM_btn"
        Me.Show_IM_btn.Size = New System.Drawing.Size(39, 34)
        Me.Show_IM_btn.TabIndex = 591
        Me.Show_IM_btn.UseVisualStyleBackColor = False
        '
        'Only_IM_View_CB
        '
        Me.Only_IM_View_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Only_IM_View_CB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Only_IM_View_CB.Location = New System.Drawing.Point(228, 4)
        Me.Only_IM_View_CB.Name = "Only_IM_View_CB"
        Me.Only_IM_View_CB.Size = New System.Drawing.Size(145, 32)
        Me.Only_IM_View_CB.TabIndex = 593
        Me.Only_IM_View_CB.Text = "عرض أصناف فقط"
        Me.Only_IM_View_CB.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button1.BackColor = System.Drawing.SystemColors.Menu
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(1, 429)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(49, 40)
        Me.Button1.TabIndex = 646
        Me.Button1.Tag = "DELETE"
        Me.Button1.Text = "X"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'OrdersMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(946, 516)
        Me.Controls.Add(Me.OrdersGrid)
        Me.Controls.Add(Me.Only_IM_View_CB)
        Me.Controls.Add(Me.Show_IM_btn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Bar_CB)
        Me.Controls.Add(Me.TypeCmb)
        Me.Controls.Add(Me.Search_txt)
        Me.Controls.Add(Me.Details_Panel)
        Me.Controls.Add(Me.Bill_Panel)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "OrdersMenu"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OrdersMenu"
        Me.Bill_Panel.ResumeLayout(False)
        Me.Bill_Panel.PerformLayout()
        Me.DiscountPanel.ResumeLayout(False)
        Me.DiscountPanel.PerformLayout()
        CType(Me.MetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OrdersGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Details_Panel.ResumeLayout(False)
        Me.Details_Panel.PerformLayout()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Search_txt As System.Windows.Forms.TextBox
    Friend WithEvents TypeCmb As System.Windows.Forms.ComboBox
    Friend WithEvents Bar_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bill_Panel As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Details_Panel As System.Windows.Forms.Panel
    Friend WithEvents Rest_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Pied_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Cash_btn As System.Windows.Forms.Button
    Friend WithEvents OrderDeliver_btn As System.Windows.Forms.Button
    Friend WithEvents Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Bill_Date_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents UserName_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Barcode_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents AG_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents MetroGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents VoidBill_Btn As System.Windows.Forms.Button
    Friend WithEvents Print_IN_btn As System.Windows.Forms.Button
    Friend WithEvents Pure_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents isVoid_LB As System.Windows.Forms.Label
    Friend WithEvents Print_Costmer_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Ag_phone_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Show_IM_btn As System.Windows.Forms.Button
    Friend WithEvents IM_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_T_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_NameCL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SB_day_Bill_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Cancel_Recive_btn As System.Windows.Forms.Button
    Friend WithEvents OpenCahDR_Btn As System.Windows.Forms.Button
    Friend WithEvents DiscountPanel As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TotalTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents DiscountTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Open_butt As System.Windows.Forms.Button
    Friend WithEvents OrdersGrid As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents Deliver_LB As System.Windows.Forms.Label
    Friend WithEvents Edit_Date_btn As System.Windows.Forms.Button
    Friend WithEvents Deliver_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents DataB As System.Windows.Forms.BindingSource
    Friend WithEvents Only_IM_View_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Cr_Phone_Txt As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Button1 As Button
End Class
