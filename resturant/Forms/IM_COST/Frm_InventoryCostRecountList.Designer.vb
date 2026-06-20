<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_InventoryCostRecountList

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

    Friend WithEvents Txt_IM_ID As TextBox
    Friend WithEvents Cmb_Status As ComboBox
    Friend WithEvents Dtp_From As DateTimePicker
    Friend WithEvents Dtp_To As DateTimePicker

    Friend WithEvents Btn_Search As Button
    Friend WithEvents Btn_Open As Button
    Friend WithEvents Btn_Close As Button

    Friend WithEvents GridBatches As DataGridView
    Friend WithEvents TitleBar_Panel As Panel
    Friend WithEvents TopTitle_LB As Label
    Friend WithEvents Help_LB As Label
    Friend WithEvents FiltersTitle_LB As Label
    Friend WithEvents ResultsTitle_LB As Label

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.Help_LB = New System.Windows.Forms.Label()
        Me.FiltersTitle_LB = New System.Windows.Forms.Label()
        Me.ResultsTitle_LB = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_IM_ID = New System.Windows.Forms.TextBox()
        Me.Cmb_Status = New System.Windows.Forms.ComboBox()
        Me.Dtp_From = New System.Windows.Forms.DateTimePicker()
        Me.Dtp_To = New System.Windows.Forms.DateTimePicker()
        Me.Btn_Search = New System.Windows.Forms.Button()
        Me.Btn_Open = New System.Windows.Forms.Button()
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.GridBatches = New System.Windows.Forms.DataGridView()
        Me.TitleBar_Panel.SuspendLayout()
        CType(Me.GridBatches, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(1209, 44)
        Me.TitleBar_Panel.TabIndex = 12
        '
        'TopTitle_LB
        '
        Me.TopTitle_LB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TopTitle_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TopTitle_LB.ForeColor = System.Drawing.Color.White
        Me.TopTitle_LB.Location = New System.Drawing.Point(0, 0)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Padding = New System.Windows.Forms.Padding(16, 0, 16, 0)
        Me.TopTitle_LB.Size = New System.Drawing.Size(1209, 44)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Text = "مستندات إعادة احتساب تكلفة المخزون"
        Me.TopTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Help_LB
        '
        Me.Help_LB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Help_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Help_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.Help_LB.Location = New System.Drawing.Point(32, 47)
        Me.Help_LB.Name = "Help_LB"
        Me.Help_LB.Size = New System.Drawing.Size(1177, 24)
        Me.Help_LB.TabIndex = 13
        Me.Help_LB.Text = "استخدم عوامل التصفية للوصول إلى مستند إعادة الاحتساب، ثم افتح المستند لمراجعة الت" &
    "فاصيل."
        Me.Help_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FiltersTitle_LB
        '
        Me.FiltersTitle_LB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FiltersTitle_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.FiltersTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.FiltersTitle_LB.Location = New System.Drawing.Point(4, 77)
        Me.FiltersTitle_LB.Name = "FiltersTitle_LB"
        Me.FiltersTitle_LB.Size = New System.Drawing.Size(1205, 22)
        Me.FiltersTitle_LB.TabIndex = 14
        Me.FiltersTitle_LB.Text = "عوامل التصفية"
        Me.FiltersTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ResultsTitle_LB
        '
        Me.ResultsTitle_LB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResultsTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.ResultsTitle_LB.Location = New System.Drawing.Point(8, 167)
        Me.ResultsTitle_LB.Name = "ResultsTitle_LB"
        Me.ResultsTitle_LB.Size = New System.Drawing.Size(80, 22)
        Me.ResultsTitle_LB.TabIndex = 15
        Me.ResultsTitle_LB.Text = "المستندات"
        Me.ResultsTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(747, 115)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "رقم الصنف"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(520, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 24)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "الحالة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(7, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 24)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "من تاريخ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(237, 118)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 24)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "إلى تاريخ"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_IM_ID
        '
        Me.Txt_IM_ID.BackColor = System.Drawing.Color.White
        Me.Txt_IM_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_IM_ID.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_IM_ID.Location = New System.Drawing.Point(834, 115)
        Me.Txt_IM_ID.Name = "Txt_IM_ID"
        Me.Txt_IM_ID.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_IM_ID.Size = New System.Drawing.Size(178, 23)
        Me.Txt_IM_ID.TabIndex = 4
        '
        'Cmb_Status
        '
        Me.Cmb_Status.BackColor = System.Drawing.Color.White
        Me.Cmb_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Status.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cmb_Status.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Cmb_Status.Location = New System.Drawing.Point(585, 113)
        Me.Cmb_Status.Name = "Cmb_Status"
        Me.Cmb_Status.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Cmb_Status.Size = New System.Drawing.Size(150, 23)
        Me.Cmb_Status.TabIndex = 5
        '
        'Dtp_From
        '
        Me.Dtp_From.CustomFormat = "yyyy/MM/dd"
        Me.Dtp_From.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Dtp_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_From.Location = New System.Drawing.Point(80, 115)
        Me.Dtp_From.Name = "Dtp_From"
        Me.Dtp_From.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Dtp_From.Size = New System.Drawing.Size(153, 27)
        Me.Dtp_From.TabIndex = 6
        '
        'Dtp_To
        '
        Me.Dtp_To.CustomFormat = "yyyy/MM/dd"
        Me.Dtp_To.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Dtp_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_To.Location = New System.Drawing.Point(309, 117)
        Me.Dtp_To.Name = "Dtp_To"
        Me.Dtp_To.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Dtp_To.Size = New System.Drawing.Size(153, 27)
        Me.Dtp_To.TabIndex = 7
        '
        'Btn_Search
        '
        Me.Btn_Search.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Btn_Search.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Search.FlatAppearance.BorderSize = 0
        Me.Btn_Search.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(29, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.Btn_Search.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Search.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Search.ForeColor = System.Drawing.Color.White
        Me.Btn_Search.Location = New System.Drawing.Point(517, 153)
        Me.Btn_Search.Name = "Btn_Search"
        Me.Btn_Search.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Btn_Search.Size = New System.Drawing.Size(130, 32)
        Me.Btn_Search.TabIndex = 8
        Me.Btn_Search.Text = "⌕  بحث"
        Me.Btn_Search.UseVisualStyleBackColor = False
        '
        'Btn_Open
        '
        Me.Btn_Open.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.Btn_Open.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Open.FlatAppearance.BorderSize = 0
        Me.Btn_Open.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.Btn_Open.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.Btn_Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Open.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Open.ForeColor = System.Drawing.Color.White
        Me.Btn_Open.Location = New System.Drawing.Point(649, 153)
        Me.Btn_Open.Name = "Btn_Open"
        Me.Btn_Open.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Btn_Open.Size = New System.Drawing.Size(130, 32)
        Me.Btn_Open.TabIndex = 9
        Me.Btn_Open.Text = "↗  فتح المستند"
        Me.Btn_Open.UseVisualStyleBackColor = False
        '
        'Btn_Close
        '
        Me.Btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Close.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.FlatAppearance.BorderSize = 0
        Me.Btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.Btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Close.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Close.ForeColor = System.Drawing.Color.White
        Me.Btn_Close.Location = New System.Drawing.Point(1075, 155)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Btn_Close.Size = New System.Drawing.Size(118, 30)
        Me.Btn_Close.TabIndex = 10
        Me.Btn_Close.Text = "✕  إغلاق"
        Me.Btn_Close.UseVisualStyleBackColor = False
        '
        'GridBatches
        '
        Me.GridBatches.AllowUserToAddRows = False
        Me.GridBatches.AllowUserToDeleteRows = False
        Me.GridBatches.AllowUserToResizeColumns = False
        Me.GridBatches.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridBatches.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GridBatches.BackgroundColor = System.Drawing.Color.White
        Me.GridBatches.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridBatches.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridBatches.ColumnHeadersHeight = 34
        Me.GridBatches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GridBatches.EnableHeadersVisualStyles = False
        Me.GridBatches.GridColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.GridBatches.Location = New System.Drawing.Point(4, 189)
        Me.GridBatches.MultiSelect = False
        Me.GridBatches.Name = "GridBatches"
        Me.GridBatches.ReadOnly = True
        Me.GridBatches.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GridBatches.RowHeadersVisible = False
        Me.GridBatches.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GridBatches.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GridBatches.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.GridBatches.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.GridBatches.RowTemplate.Height = 32
        Me.GridBatches.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridBatches.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridBatches.Size = New System.Drawing.Size(1205, 515)
        Me.GridBatches.TabIndex = 11
        '
        'Frm_InventoryCostRecountList
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1209, 716)
        Me.Controls.Add(Me.ResultsTitle_LB)
        Me.Controls.Add(Me.FiltersTitle_LB)
        Me.Controls.Add(Me.Help_LB)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Txt_IM_ID)
        Me.Controls.Add(Me.Cmb_Status)
        Me.Controls.Add(Me.Dtp_From)
        Me.Controls.Add(Me.Dtp_To)
        Me.Controls.Add(Me.Btn_Search)
        Me.Controls.Add(Me.Btn_Open)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.GridBatches)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(1044, 755)
        Me.Name = "Frm_InventoryCostRecountList"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "مستندات إعادة احتساب تكلفة المخزون"
        Me.TitleBar_Panel.ResumeLayout(False)
        CType(Me.GridBatches, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class
