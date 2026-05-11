<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_ItemLedger

    Inherits System.Windows.Forms.Form

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

    Friend WithEvents PanelFilters As Panel
    Friend WithEvents PanelTotals As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Txt_ShopName As TextBox
    Friend WithEvents Txt_ItemName As TextBox
    Friend WithEvents Cmb_Store As ComboBox

    Friend WithEvents Dtp_From As DateTimePicker
    Friend WithEvents Dtp_To As DateTimePicker

    Friend WithEvents Btn_Search As Button
    Friend WithEvents Btn_Preview As Button
    Friend WithEvents Btn_Pdf As Button
    Friend WithEvents Btn_Print As Button
    Friend WithEvents Btn_Close As Button

    Friend WithEvents GridLedger As DataGridView

    Friend WithEvents Txt_TotalIn As TextBox
    Friend WithEvents Txt_TotalOut As TextBox
    Friend WithEvents Txt_FinalBalance As TextBox
    Friend WithEvents TitleBar_Panel As Panel
    Friend WithEvents TopTitle_LB As Label
    Friend WithEvents Help_LB As Label
    Friend WithEvents ResultsTitle_LB As Label
    Friend WithEvents LedgerPrintDocument As System.Drawing.Printing.PrintDocument

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.Help_LB = New System.Windows.Forms.Label()
        Me.ResultsTitle_LB = New System.Windows.Forms.Label()
        Me.PanelFilters = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Txt_ShopName = New System.Windows.Forms.TextBox()
        Me.Txt_ItemName = New System.Windows.Forms.TextBox()
        Me.Cmb_Store = New System.Windows.Forms.ComboBox()
        Me.Dtp_From = New System.Windows.Forms.DateTimePicker()
        Me.Dtp_To = New System.Windows.Forms.DateTimePicker()
        Me.Btn_Search = New System.Windows.Forms.Button()
        Me.Btn_Preview = New System.Windows.Forms.Button()
        Me.Btn_Pdf = New System.Windows.Forms.Button()
        Me.Btn_Print = New System.Windows.Forms.Button()
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.PanelTotals = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txt_TotalIn = New System.Windows.Forms.TextBox()
        Me.Txt_TotalOut = New System.Windows.Forms.TextBox()
        Me.Txt_FinalBalance = New System.Windows.Forms.TextBox()
        Me.GridLedger = New System.Windows.Forms.DataGridView()
        Me.LedgerPrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.TitleBar_Panel.SuspendLayout()
        Me.PanelFilters.SuspendLayout()
        Me.PanelTotals.SuspendLayout()
        CType(Me.GridLedger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(1028, 44)
        Me.TitleBar_Panel.TabIndex = 4
        '
        'TopTitle_LB
        '
        Me.TopTitle_LB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TopTitle_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TopTitle_LB.ForeColor = System.Drawing.Color.White
        Me.TopTitle_LB.Location = New System.Drawing.Point(0, 0)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Padding = New System.Windows.Forms.Padding(16, 0, 16, 0)
        Me.TopTitle_LB.Size = New System.Drawing.Size(1028, 44)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Text = "كشف حركة صنف"
        Me.TopTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Help_LB
        '
        Me.Help_LB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Help_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Help_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.Help_LB.Location = New System.Drawing.Point(16, 52)
        Me.Help_LB.Name = "Help_LB"
        Me.Help_LB.Size = New System.Drawing.Size(996, 23)
        Me.Help_LB.TabIndex = 5
        Me.Help_LB.Text = "راجع حركة الصنف حسب المخزن والتاريخ، مع تمييز كميات الدخول والخروج وطباعتها عند ا" &
    "لحاجة."
        Me.Help_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ResultsTitle_LB
        '
        Me.ResultsTitle_LB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResultsTitle_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ResultsTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.ResultsTitle_LB.Location = New System.Drawing.Point(16, 176)
        Me.ResultsTitle_LB.Name = "ResultsTitle_LB"
        Me.ResultsTitle_LB.Size = New System.Drawing.Size(996, 22)
        Me.ResultsTitle_LB.TabIndex = 6
        Me.ResultsTitle_LB.Text = "الحركات"
        Me.ResultsTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PanelFilters
        '
        Me.PanelFilters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelFilters.BackColor = System.Drawing.Color.Transparent
        Me.PanelFilters.Controls.Add(Me.Label2)
        Me.PanelFilters.Controls.Add(Me.Label3)
        Me.PanelFilters.Controls.Add(Me.Label4)
        Me.PanelFilters.Controls.Add(Me.Label5)
        Me.PanelFilters.Controls.Add(Me.Label9)
        Me.PanelFilters.Controls.Add(Me.Txt_ShopName)
        Me.PanelFilters.Controls.Add(Me.Txt_ItemName)
        Me.PanelFilters.Controls.Add(Me.Cmb_Store)
        Me.PanelFilters.Controls.Add(Me.Dtp_From)
        Me.PanelFilters.Controls.Add(Me.Dtp_To)
        Me.PanelFilters.Controls.Add(Me.Btn_Search)
        Me.PanelFilters.Controls.Add(Me.Btn_Preview)
        Me.PanelFilters.Controls.Add(Me.Btn_Pdf)
        Me.PanelFilters.Controls.Add(Me.Btn_Print)
        Me.PanelFilters.Controls.Add(Me.Btn_Close)
        Me.PanelFilters.Location = New System.Drawing.Point(16, 78)
        Me.PanelFilters.Name = "PanelFilters"
        Me.PanelFilters.Size = New System.Drawing.Size(996, 96)
        Me.PanelFilters.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(890, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 23)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "اسم الصنف"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(890, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 23)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "المخزن"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(466, 34)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 23)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "من تاريخ"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(466, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 23)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "إلى تاريخ"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(890, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 23)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "اسم المحل"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_ShopName
        '
        Me.Txt_ShopName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txt_ShopName.BackColor = System.Drawing.Color.White
        Me.Txt_ShopName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ShopName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_ShopName.Location = New System.Drawing.Point(560, 4)
        Me.Txt_ShopName.Name = "Txt_ShopName"
        Me.Txt_ShopName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_ShopName.Size = New System.Drawing.Size(320, 23)
        Me.Txt_ShopName.TabIndex = 12
        '
        'Txt_ItemName
        '
        Me.Txt_ItemName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txt_ItemName.BackColor = System.Drawing.Color.White
        Me.Txt_ItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_ItemName.Location = New System.Drawing.Point(560, 34)
        Me.Txt_ItemName.Name = "Txt_ItemName"
        Me.Txt_ItemName.ReadOnly = True
        Me.Txt_ItemName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_ItemName.Size = New System.Drawing.Size(320, 23)
        Me.Txt_ItemName.TabIndex = 6
        '
        'Cmb_Store
        '
        Me.Cmb_Store.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cmb_Store.BackColor = System.Drawing.Color.White
        Me.Cmb_Store.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Store.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cmb_Store.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Cmb_Store.Location = New System.Drawing.Point(560, 65)
        Me.Cmb_Store.Name = "Cmb_Store"
        Me.Cmb_Store.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Cmb_Store.Size = New System.Drawing.Size(320, 23)
        Me.Cmb_Store.TabIndex = 7
        '
        'Dtp_From
        '
        Me.Dtp_From.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Dtp_From.CustomFormat = "yyyy/MM/dd"
        Me.Dtp_From.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Dtp_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_From.Location = New System.Drawing.Point(346, 34)
        Me.Dtp_From.Name = "Dtp_From"
        Me.Dtp_From.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Dtp_From.Size = New System.Drawing.Size(110, 23)
        Me.Dtp_From.TabIndex = 8
        '
        'Dtp_To
        '
        Me.Dtp_To.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Dtp_To.CustomFormat = "yyyy/MM/dd"
        Me.Dtp_To.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Dtp_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_To.Location = New System.Drawing.Point(346, 65)
        Me.Dtp_To.Name = "Dtp_To"
        Me.Dtp_To.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Dtp_To.Size = New System.Drawing.Size(110, 23)
        Me.Dtp_To.TabIndex = 9
        '
        'Btn_Search
        '
        Me.Btn_Search.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Btn_Search.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Search.FlatAppearance.BorderSize = 0
        Me.Btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Search.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Search.ForeColor = System.Drawing.Color.White
        Me.Btn_Search.Location = New System.Drawing.Point(224, 34)
        Me.Btn_Search.Name = "Btn_Search"
        Me.Btn_Search.Size = New System.Drawing.Size(105, 26)
        Me.Btn_Search.TabIndex = 10
        Me.Btn_Search.Text = "بحث"
        Me.Btn_Search.UseVisualStyleBackColor = False
        '
        'Btn_Preview
        '
        Me.Btn_Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.Btn_Preview.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Preview.FlatAppearance.BorderSize = 0
        Me.Btn_Preview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Preview.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Preview.ForeColor = System.Drawing.Color.White
        Me.Btn_Preview.Location = New System.Drawing.Point(113, 34)
        Me.Btn_Preview.Name = "Btn_Preview"
        Me.Btn_Preview.Size = New System.Drawing.Size(105, 26)
        Me.Btn_Preview.TabIndex = 14
        Me.Btn_Preview.Text = "معاينة"
        Me.Btn_Preview.UseVisualStyleBackColor = False
        '
        'Btn_Pdf
        '
        Me.Btn_Pdf.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.Btn_Pdf.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Pdf.FlatAppearance.BorderSize = 0
        Me.Btn_Pdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Pdf.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Pdf.ForeColor = System.Drawing.Color.White
        Me.Btn_Pdf.Location = New System.Drawing.Point(2, 34)
        Me.Btn_Pdf.Name = "Btn_Pdf"
        Me.Btn_Pdf.Size = New System.Drawing.Size(105, 26)
        Me.Btn_Pdf.TabIndex = 15
        Me.Btn_Pdf.Text = "PDF"
        Me.Btn_Pdf.UseVisualStyleBackColor = False
        '
        'Btn_Print
        '
        Me.Btn_Print.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.Btn_Print.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Print.FlatAppearance.BorderSize = 0
        Me.Btn_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Print.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Print.ForeColor = System.Drawing.Color.White
        Me.Btn_Print.Location = New System.Drawing.Point(113, 65)
        Me.Btn_Print.Name = "Btn_Print"
        Me.Btn_Print.Size = New System.Drawing.Size(105, 26)
        Me.Btn_Print.TabIndex = 12
        Me.Btn_Print.Text = "طباعة"
        Me.Btn_Print.UseVisualStyleBackColor = False
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.IndianRed
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.FlatAppearance.BorderSize = 0
        Me.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Close.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Close.ForeColor = System.Drawing.Color.White
        Me.Btn_Close.Location = New System.Drawing.Point(2, 65)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(105, 26)
        Me.Btn_Close.TabIndex = 11
        Me.Btn_Close.Text = "إغلاق"
        Me.Btn_Close.UseVisualStyleBackColor = False
        '
        'PanelTotals
        '
        Me.PanelTotals.BackColor = System.Drawing.Color.Transparent
        Me.PanelTotals.Controls.Add(Me.Label6)
        Me.PanelTotals.Controls.Add(Me.Label7)
        Me.PanelTotals.Controls.Add(Me.Label8)
        Me.PanelTotals.Controls.Add(Me.Txt_TotalIn)
        Me.PanelTotals.Controls.Add(Me.Txt_TotalOut)
        Me.PanelTotals.Controls.Add(Me.Txt_FinalBalance)
        Me.PanelTotals.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelTotals.Location = New System.Drawing.Point(0, 658)
        Me.PanelTotals.Name = "PanelTotals"
        Me.PanelTotals.Size = New System.Drawing.Size(1028, 58)
        Me.PanelTotals.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(874, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 23)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "إجمالي الدخول"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(558, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 23)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "إجمالي الخروج"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(242, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(110, 23)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "الرصيد النهائي"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_TotalIn
        '
        Me.Txt_TotalIn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txt_TotalIn.BackColor = System.Drawing.Color.White
        Me.Txt_TotalIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotalIn.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_TotalIn.Location = New System.Drawing.Point(708, 18)
        Me.Txt_TotalIn.Name = "Txt_TotalIn"
        Me.Txt_TotalIn.ReadOnly = True
        Me.Txt_TotalIn.Size = New System.Drawing.Size(160, 23)
        Me.Txt_TotalIn.TabIndex = 3
        Me.Txt_TotalIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_TotalOut
        '
        Me.Txt_TotalOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txt_TotalOut.BackColor = System.Drawing.Color.White
        Me.Txt_TotalOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotalOut.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_TotalOut.Location = New System.Drawing.Point(392, 18)
        Me.Txt_TotalOut.Name = "Txt_TotalOut"
        Me.Txt_TotalOut.ReadOnly = True
        Me.Txt_TotalOut.Size = New System.Drawing.Size(160, 23)
        Me.Txt_TotalOut.TabIndex = 4
        Me.Txt_TotalOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_FinalBalance
        '
        Me.Txt_FinalBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txt_FinalBalance.BackColor = System.Drawing.Color.White
        Me.Txt_FinalBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_FinalBalance.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_FinalBalance.Location = New System.Drawing.Point(76, 18)
        Me.Txt_FinalBalance.Name = "Txt_FinalBalance"
        Me.Txt_FinalBalance.ReadOnly = True
        Me.Txt_FinalBalance.Size = New System.Drawing.Size(160, 23)
        Me.Txt_FinalBalance.TabIndex = 5
        Me.Txt_FinalBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GridLedger
        '
        Me.GridLedger.AllowUserToAddRows = False
        Me.GridLedger.AllowUserToDeleteRows = False
        Me.GridLedger.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridLedger.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridLedger.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridLedger.ColumnHeadersHeight = 34
        Me.GridLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GridLedger.EnableHeadersVisualStyles = False
        Me.GridLedger.GridColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.GridLedger.Location = New System.Drawing.Point(16, 201)
        Me.GridLedger.MultiSelect = False
        Me.GridLedger.Name = "GridLedger"
        Me.GridLedger.ReadOnly = True
        Me.GridLedger.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GridLedger.RowHeadersVisible = False
        Me.GridLedger.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.GridLedger.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GridLedger.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.GridLedger.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.GridLedger.RowTemplate.Height = 28
        Me.GridLedger.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridLedger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridLedger.Size = New System.Drawing.Size(996, 451)
        Me.GridLedger.TabIndex = 0
        '
        'LedgerPrintDocument
        '
        '
        'Frm_ItemLedger
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 716)
        Me.Controls.Add(Me.ResultsTitle_LB)
        Me.Controls.Add(Me.Help_LB)
        Me.Controls.Add(Me.GridLedger)
        Me.Controls.Add(Me.PanelTotals)
        Me.Controls.Add(Me.PanelFilters)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MinimumSize = New System.Drawing.Size(1044, 755)
        Me.Name = "Frm_ItemLedger"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "كشف حركة صنف"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.PanelFilters.ResumeLayout(False)
        Me.PanelFilters.PerformLayout()
        Me.PanelTotals.ResumeLayout(False)
        Me.PanelTotals.PerformLayout()
        CType(Me.GridLedger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

End Class
