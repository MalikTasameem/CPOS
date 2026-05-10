<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_InventoryCostRecountPreview

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

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label

    Friend WithEvents Txt_BatchId As TextBox
    Friend WithEvents Txt_TotalImpact As TextBox
    Friend WithEvents Txt_InventoryImpact As TextBox
    Friend WithEvents Txt_COGSImpact As TextBox
    Friend WithEvents Txt_ExpenseImpact As TextBox

    Friend WithEvents GridImpact As DataGridView
    Friend WithEvents GridJournal As DataGridView

    Friend WithEvents Btn_Post As Button
    Friend WithEvents Btn_Rollback As Button
    Friend WithEvents Btn_Close As Button
    Friend WithEvents TitleBar_Panel As Panel
    Friend WithEvents TopTitle_LB As Label
    Friend WithEvents Help_LB As Label
    Friend WithEvents SummaryTitle_LB As Label
    Friend WithEvents ImpactTitle_LB As Label
    Friend WithEvents JournalTitle_LB As Label

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.Help_LB = New System.Windows.Forms.Label()
        Me.SummaryTitle_LB = New System.Windows.Forms.Label()
        Me.ImpactTitle_LB = New System.Windows.Forms.Label()
        Me.JournalTitle_LB = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_BatchId = New System.Windows.Forms.TextBox()
        Me.Txt_TotalImpact = New System.Windows.Forms.TextBox()
        Me.Txt_InventoryImpact = New System.Windows.Forms.TextBox()
        Me.Txt_COGSImpact = New System.Windows.Forms.TextBox()
        Me.Txt_ExpenseImpact = New System.Windows.Forms.TextBox()
        Me.GridImpact = New System.Windows.Forms.DataGridView()
        Me.GridJournal = New System.Windows.Forms.DataGridView()
        Me.Btn_Post = New System.Windows.Forms.Button()
        Me.Btn_Rollback = New System.Windows.Forms.Button()
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.TitleBar_Panel.SuspendLayout()
        CType(Me.GridImpact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridJournal, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TitleBar_Panel.TabIndex = 15
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
        Me.TopTitle_LB.Text = "معاينة إعادة احتساب التكلفة"
        Me.TopTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Help_LB
        '
        Me.Help_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Help_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.Help_LB.Location = New System.Drawing.Point(16, 51)
        Me.Help_LB.Name = "Help_LB"
        Me.Help_LB.Size = New System.Drawing.Size(996, 24)
        Me.Help_LB.TabIndex = 16
        Me.Help_LB.Text = "راجع ملخص الأثر وتفاصيل الحركات والقيد المحاسبي قبل اعتماد التعديل أو التراجع عنه" &
    "."
        Me.Help_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SummaryTitle_LB
        '
        Me.SummaryTitle_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SummaryTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.SummaryTitle_LB.Location = New System.Drawing.Point(16, 79)
        Me.SummaryTitle_LB.Name = "SummaryTitle_LB"
        Me.SummaryTitle_LB.Size = New System.Drawing.Size(996, 22)
        Me.SummaryTitle_LB.TabIndex = 17
        Me.SummaryTitle_LB.Text = "ملخص المستند"
        Me.SummaryTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ImpactTitle_LB
        '
        Me.ImpactTitle_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ImpactTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.ImpactTitle_LB.Location = New System.Drawing.Point(16, 170)
        Me.ImpactTitle_LB.Name = "ImpactTitle_LB"
        Me.ImpactTitle_LB.Size = New System.Drawing.Size(996, 22)
        Me.ImpactTitle_LB.TabIndex = 18
        Me.ImpactTitle_LB.Text = "تفاصيل الأثر على الحركات"
        Me.ImpactTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'JournalTitle_LB
        '
        Me.JournalTitle_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.JournalTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.JournalTitle_LB.Location = New System.Drawing.Point(16, 526)
        Me.JournalTitle_LB.Name = "JournalTitle_LB"
        Me.JournalTitle_LB.Size = New System.Drawing.Size(996, 22)
        Me.JournalTitle_LB.TabIndex = 19
        Me.JournalTitle_LB.Text = "القيد المحاسبي المتوقع"
        Me.JournalTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(912, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Batch ID"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(810, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 23)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "إجمالي الأثر"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(576, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 23)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "أثر المخزون"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(308, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 23)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "أثر تكلفة المبيعات"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(26, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 23)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "أثر المصروفات"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_BatchId
        '
        Me.Txt_BatchId.BackColor = System.Drawing.Color.White
        Me.Txt_BatchId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_BatchId.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Txt_BatchId.Location = New System.Drawing.Point(650, 105)
        Me.Txt_BatchId.Name = "Txt_BatchId"
        Me.Txt_BatchId.ReadOnly = True
        Me.Txt_BatchId.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_BatchId.Size = New System.Drawing.Size(256, 23)
        Me.Txt_BatchId.TabIndex = 5
        '
        'Txt_TotalImpact
        '
        Me.Txt_TotalImpact.BackColor = System.Drawing.Color.White
        Me.Txt_TotalImpact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotalImpact.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_TotalImpact.Location = New System.Drawing.Point(886, 144)
        Me.Txt_TotalImpact.Name = "Txt_TotalImpact"
        Me.Txt_TotalImpact.ReadOnly = True
        Me.Txt_TotalImpact.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_TotalImpact.Size = New System.Drawing.Size(126, 23)
        Me.Txt_TotalImpact.TabIndex = 6
        Me.Txt_TotalImpact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_InventoryImpact
        '
        Me.Txt_InventoryImpact.BackColor = System.Drawing.Color.White
        Me.Txt_InventoryImpact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_InventoryImpact.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_InventoryImpact.Location = New System.Drawing.Point(650, 144)
        Me.Txt_InventoryImpact.Name = "Txt_InventoryImpact"
        Me.Txt_InventoryImpact.ReadOnly = True
        Me.Txt_InventoryImpact.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_InventoryImpact.Size = New System.Drawing.Size(126, 23)
        Me.Txt_InventoryImpact.TabIndex = 7
        Me.Txt_InventoryImpact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_COGSImpact
        '
        Me.Txt_COGSImpact.BackColor = System.Drawing.Color.White
        Me.Txt_COGSImpact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_COGSImpact.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_COGSImpact.Location = New System.Drawing.Point(406, 144)
        Me.Txt_COGSImpact.Name = "Txt_COGSImpact"
        Me.Txt_COGSImpact.ReadOnly = True
        Me.Txt_COGSImpact.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_COGSImpact.Size = New System.Drawing.Size(126, 23)
        Me.Txt_COGSImpact.TabIndex = 8
        Me.Txt_COGSImpact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_ExpenseImpact
        '
        Me.Txt_ExpenseImpact.BackColor = System.Drawing.Color.White
        Me.Txt_ExpenseImpact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ExpenseImpact.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_ExpenseImpact.Location = New System.Drawing.Point(115, 144)
        Me.Txt_ExpenseImpact.Name = "Txt_ExpenseImpact"
        Me.Txt_ExpenseImpact.ReadOnly = True
        Me.Txt_ExpenseImpact.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_ExpenseImpact.Size = New System.Drawing.Size(100, 23)
        Me.Txt_ExpenseImpact.TabIndex = 9
        Me.Txt_ExpenseImpact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GridImpact
        '
        Me.GridImpact.AllowUserToAddRows = False
        Me.GridImpact.AllowUserToDeleteRows = False
        Me.GridImpact.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GridImpact.BackgroundColor = System.Drawing.Color.White
        Me.GridImpact.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridImpact.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridImpact.ColumnHeadersHeight = 34
        Me.GridImpact.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GridImpact.EnableHeadersVisualStyles = False
        Me.GridImpact.GridColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.GridImpact.Location = New System.Drawing.Point(16, 194)
        Me.GridImpact.Name = "GridImpact"
        Me.GridImpact.ReadOnly = True
        Me.GridImpact.RowHeadersVisible = False
        Me.GridImpact.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.GridImpact.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.GridImpact.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.GridImpact.RowTemplate.Height = 32
        Me.GridImpact.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridImpact.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridImpact.Size = New System.Drawing.Size(996, 324)
        Me.GridImpact.TabIndex = 10
        '
        'GridJournal
        '
        Me.GridJournal.AllowUserToAddRows = False
        Me.GridJournal.AllowUserToDeleteRows = False
        Me.GridJournal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GridJournal.BackgroundColor = System.Drawing.Color.White
        Me.GridJournal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridJournal.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GridJournal.ColumnHeadersHeight = 34
        Me.GridJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GridJournal.EnableHeadersVisualStyles = False
        Me.GridJournal.GridColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.GridJournal.Location = New System.Drawing.Point(16, 550)
        Me.GridJournal.Name = "GridJournal"
        Me.GridJournal.ReadOnly = True
        Me.GridJournal.RowHeadersVisible = False
        Me.GridJournal.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.GridJournal.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.GridJournal.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.GridJournal.RowTemplate.Height = 32
        Me.GridJournal.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridJournal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridJournal.Size = New System.Drawing.Size(996, 98)
        Me.GridJournal.TabIndex = 11
        '
        'Btn_Post
        '
        Me.Btn_Post.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.Btn_Post.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Post.FlatAppearance.BorderSize = 0
        Me.Btn_Post.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Post.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Post.ForeColor = System.Drawing.Color.White
        Me.Btn_Post.Location = New System.Drawing.Point(872, 662)
        Me.Btn_Post.Name = "Btn_Post"
        Me.Btn_Post.Size = New System.Drawing.Size(140, 36)
        Me.Btn_Post.TabIndex = 12
        Me.Btn_Post.Text = "اعتماد التعديل"
        Me.Btn_Post.UseVisualStyleBackColor = False
        '
        'Btn_Rollback
        '
        Me.Btn_Rollback.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.Btn_Rollback.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Rollback.FlatAppearance.BorderSize = 0
        Me.Btn_Rollback.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Rollback.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Rollback.ForeColor = System.Drawing.Color.White
        Me.Btn_Rollback.Location = New System.Drawing.Point(724, 662)
        Me.Btn_Rollback.Name = "Btn_Rollback"
        Me.Btn_Rollback.Size = New System.Drawing.Size(140, 36)
        Me.Btn_Rollback.TabIndex = 13
        Me.Btn_Rollback.Text = "تراجع"
        Me.Btn_Rollback.UseVisualStyleBackColor = False
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.IndianRed
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.FlatAppearance.BorderSize = 0
        Me.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Close.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Close.ForeColor = System.Drawing.Color.White
        Me.Btn_Close.Location = New System.Drawing.Point(576, 662)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(140, 36)
        Me.Btn_Close.TabIndex = 14
        Me.Btn_Close.Text = "إغلاق"
        Me.Btn_Close.UseVisualStyleBackColor = False
        '
        'Frm_InventoryCostRecountPreview
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 716)
        Me.Controls.Add(Me.JournalTitle_LB)
        Me.Controls.Add(Me.ImpactTitle_LB)
        Me.Controls.Add(Me.SummaryTitle_LB)
        Me.Controls.Add(Me.Help_LB)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Txt_BatchId)
        Me.Controls.Add(Me.Txt_TotalImpact)
        Me.Controls.Add(Me.Txt_InventoryImpact)
        Me.Controls.Add(Me.Txt_COGSImpact)
        Me.Controls.Add(Me.Txt_ExpenseImpact)
        Me.Controls.Add(Me.GridImpact)
        Me.Controls.Add(Me.GridJournal)
        Me.Controls.Add(Me.Btn_Post)
        Me.Controls.Add(Me.Btn_Rollback)
        Me.Controls.Add(Me.Btn_Close)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Frm_InventoryCostRecountPreview"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "معاينة إعادة احتساب التكلفة"
        Me.TitleBar_Panel.ResumeLayout(False)
        CType(Me.GridImpact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridJournal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class
