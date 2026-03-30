<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Items_Search
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Items_Search))
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GM_Serach = New System.Windows.Forms.ComboBox()
        Me.Show_IM_btn = New System.Windows.Forms.Button()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnSearchName = New System.Windows.Forms.Button()
        Me.btnSearchBarcode = New System.Windows.Forms.Button()
        Me.btnSearchItemNo = New System.Windows.Forms.Button()
        Me.lblHint = New System.Windows.Forms.Label()
        Me.IMDataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.IM_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GM_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_NUM_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isValid_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UNIT_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ADD_NewGM_Btn = New System.Windows.Forms.Button()
        Me.kbPanel = New System.Windows.Forms.Panel()
        Me.KeyboardBtn = New System.Windows.Forms.Button()
        Me.KB = New resturant.OnKeyBoard()
        CType(Me.IMDataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.kbPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(886, 660)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(116, 34)
        Me.ExitFormButton.TabIndex = 20
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label2.Location = New System.Drawing.Point(937, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "التصنيف :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GM_Serach
        '
        Me.GM_Serach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GM_Serach.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.Location = New System.Drawing.Point(670, 2)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.Size = New System.Drawing.Size(261, 25)
        Me.GM_Serach.TabIndex = 2
        '
        'Show_IM_btn
        '
        Me.Show_IM_btn.BackColor = System.Drawing.Color.White
        Me.Show_IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_IM_btn.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Show_IM_btn.Location = New System.Drawing.Point(643, 3)
        Me.Show_IM_btn.Name = "Show_IM_btn"
        Me.Show_IM_btn.Size = New System.Drawing.Size(25, 25)
        Me.Show_IM_btn.TabIndex = 3
        Me.Show_IM_btn.Text = "📌"
        Me.Show_IM_btn.UseVisualStyleBackColor = False
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.lblSearch.Location = New System.Drawing.Point(945, 42)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(52, 17)
        Me.lblSearch.TabIndex = 4
        Me.lblSearch.Text = "البحث :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(601, 36)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(341, 26)
        Me.txtSearch.TabIndex = 5
        '
        'btnSearchName
        '
        Me.btnSearchName.BackColor = System.Drawing.Color.White
        Me.btnSearchName.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearchName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchName.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearchName.Location = New System.Drawing.Point(281, 2)
        Me.btnSearchName.Name = "btnSearchName"
        Me.btnSearchName.Size = New System.Drawing.Size(133, 33)
        Me.btnSearchName.TabIndex = 6
        Me.btnSearchName.Text = "بحث بالاسم"
        Me.btnSearchName.UseVisualStyleBackColor = False
        '
        'btnSearchBarcode
        '
        Me.btnSearchBarcode.BackColor = System.Drawing.Color.White
        Me.btnSearchBarcode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearchBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchBarcode.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearchBarcode.Location = New System.Drawing.Point(142, 2)
        Me.btnSearchBarcode.Name = "btnSearchBarcode"
        Me.btnSearchBarcode.Size = New System.Drawing.Size(133, 33)
        Me.btnSearchBarcode.TabIndex = 7
        Me.btnSearchBarcode.Text = "بحث بالباركود"
        Me.btnSearchBarcode.UseVisualStyleBackColor = False
        '
        'btnSearchItemNo
        '
        Me.btnSearchItemNo.BackColor = System.Drawing.Color.White
        Me.btnSearchItemNo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearchItemNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchItemNo.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearchItemNo.Location = New System.Drawing.Point(3, 2)
        Me.btnSearchItemNo.Name = "btnSearchItemNo"
        Me.btnSearchItemNo.Size = New System.Drawing.Size(133, 33)
        Me.btnSearchItemNo.TabIndex = 8
        Me.btnSearchItemNo.Text = "بحث برقم الصنف"
        Me.btnSearchItemNo.UseVisualStyleBackColor = False
        '
        'lblHint
        '
        Me.lblHint.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.lblHint.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblHint.ForeColor = System.Drawing.Color.DimGray
        Me.lblHint.Location = New System.Drawing.Point(3, 37)
        Me.lblHint.Name = "lblHint"
        Me.lblHint.Size = New System.Drawing.Size(595, 26)
        Me.lblHint.TabIndex = 10
        Me.lblHint.Text = "اختر نوع البحث من الأزرار بالأعلى تم اكتب قيمة البحث و إضغط Enter"
        Me.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IMDataGridViewX
        '
        Me.IMDataGridViewX.AllowUserToAddRows = False
        Me.IMDataGridViewX.AllowUserToDeleteRows = False
        Me.IMDataGridViewX.BackgroundColor = System.Drawing.Color.White
        Me.IMDataGridViewX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMDataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.IMDataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IMDataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_ID_CL, Me.GM_NAME_CL, Me.Barcode_CL, Me.IM_NUM_CL, Me.item_name_CL, Me.isValid_CL, Me.UNIT_CL, Me.QTY_CL, Me.Price_CL})
        Me.IMDataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IMDataGridViewX.DefaultCellStyle = DataGridViewCellStyle5
        Me.IMDataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IMDataGridViewX.Location = New System.Drawing.Point(3, 64)
        Me.IMDataGridViewX.MultiSelect = False
        Me.IMDataGridViewX.Name = "IMDataGridViewX"
        Me.IMDataGridViewX.ReadOnly = True
        Me.IMDataGridViewX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMDataGridViewX.RowHeadersVisible = False
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 10.25!, System.Drawing.FontStyle.Bold)
        Me.IMDataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.IMDataGridViewX.RowTemplate.Height = 30
        Me.IMDataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IMDataGridViewX.Size = New System.Drawing.Size(999, 590)
        Me.IMDataGridViewX.TabIndex = 11
        '
        'IM_ID_CL
        '
        Me.IM_ID_CL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.IM_ID_CL.DataPropertyName = "IM_ID"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.IM_ID_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.IM_ID_CL.FillWeight = 5.0!
        Me.IM_ID_CL.Frozen = True
        Me.IM_ID_CL.HeaderText = "رقم الصنف"
        Me.IM_ID_CL.Name = "IM_ID_CL"
        Me.IM_ID_CL.ReadOnly = True
        Me.IM_ID_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IM_ID_CL.Visible = False
        Me.IM_ID_CL.Width = 5
        '
        'GM_NAME_CL
        '
        Me.GM_NAME_CL.DataPropertyName = "GM_NAME"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GM_NAME_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.GM_NAME_CL.HeaderText = "التصنيف"
        Me.GM_NAME_CL.Name = "GM_NAME_CL"
        Me.GM_NAME_CL.ReadOnly = True
        Me.GM_NAME_CL.Width = 200
        '
        'Barcode_CL
        '
        Me.Barcode_CL.DataPropertyName = "Barcode"
        Me.Barcode_CL.HeaderText = "باركود"
        Me.Barcode_CL.Name = "Barcode_CL"
        Me.Barcode_CL.ReadOnly = True
        Me.Barcode_CL.Width = 130
        '
        'IM_NUM_CL
        '
        Me.IM_NUM_CL.DataPropertyName = "IM_NUM"
        Me.IM_NUM_CL.HeaderText = "رقم الصنف"
        Me.IM_NUM_CL.Name = "IM_NUM_CL"
        Me.IM_NUM_CL.ReadOnly = True
        Me.IM_NUM_CL.Width = 120
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.FillWeight = 150.0!
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        Me.item_name_CL.Width = 320
        '
        'isValid_CL
        '
        Me.isValid_CL.DataPropertyName = "isValid"
        Me.isValid_CL.HeaderText = "صلاحية"
        Me.isValid_CL.Name = "isValid_CL"
        Me.isValid_CL.ReadOnly = True
        Me.isValid_CL.Visible = False
        '
        'UNIT_CL
        '
        Me.UNIT_CL.DataPropertyName = "U_NAME"
        Me.UNIT_CL.FillWeight = 40.0!
        Me.UNIT_CL.HeaderText = "الوحدة"
        Me.UNIT_CL.Name = "UNIT_CL"
        Me.UNIT_CL.ReadOnly = True
        Me.UNIT_CL.Width = 70
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Green
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.QTY_CL.FillWeight = 60.0!
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        Me.QTY_CL.Width = 75
        '
        'Price_CL
        '
        Me.Price_CL.DataPropertyName = "Price"
        Me.Price_CL.HeaderText = "السعر"
        Me.Price_CL.Name = "Price_CL"
        Me.Price_CL.ReadOnly = True
        Me.Price_CL.Width = 90
        '
        'ADD_NewGM_Btn
        '
        Me.ADD_NewGM_Btn.BackColor = System.Drawing.Color.White
        Me.ADD_NewGM_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADD_NewGM_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADD_NewGM_Btn.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.ADD_NewGM_Btn.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.ADD_NewGM_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ADD_NewGM_Btn.Location = New System.Drawing.Point(3, 660)
        Me.ADD_NewGM_Btn.Name = "ADD_NewGM_Btn"
        Me.ADD_NewGM_Btn.Size = New System.Drawing.Size(149, 34)
        Me.ADD_NewGM_Btn.TabIndex = 12
        Me.ADD_NewGM_Btn.Text = "إضافة صنف جديد"
        Me.ADD_NewGM_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ADD_NewGM_Btn.UseVisualStyleBackColor = False
        '
        'kbPanel
        '
        Me.kbPanel.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.kbPanel.Controls.Add(Me.KB)
        Me.kbPanel.Location = New System.Drawing.Point(3, 390)
        Me.kbPanel.Name = "kbPanel"
        Me.kbPanel.Size = New System.Drawing.Size(999, 267)
        Me.kbPanel.TabIndex = 21
        '
        'KeyboardBtn
        '
        Me.KeyboardBtn.BackColor = System.Drawing.Color.White
        Me.KeyboardBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.KeyboardBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.KeyboardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.KeyboardBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyboardBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.KeyboardBtn.Location = New System.Drawing.Point(415, 660)
        Me.KeyboardBtn.Name = "KeyboardBtn"
        Me.KeyboardBtn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.KeyboardBtn.Size = New System.Drawing.Size(149, 34)
        Me.KeyboardBtn.TabIndex = 22
        Me.KeyboardBtn.Text = "⌨ لوحة المفاتيح"
        Me.KeyboardBtn.UseVisualStyleBackColor = False
        '
        'KB
        '
        Me.KB.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.KB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KB.Location = New System.Drawing.Point(0, 0)
        Me.KB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.KB.Name = "KB"
        Me.KB.Size = New System.Drawing.Size(999, 267)
        Me.KB.TabIndex = 0
        '
        'Items_Search
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1003, 695)
        Me.Controls.Add(Me.IMDataGridViewX)
        Me.Controls.Add(Me.KeyboardBtn)
        Me.Controls.Add(Me.kbPanel)
        Me.Controls.Add(Me.ADD_NewGM_Btn)
        Me.Controls.Add(Me.lblHint)
        Me.Controls.Add(Me.btnSearchItemNo)
        Me.Controls.Add(Me.btnSearchBarcode)
        Me.Controls.Add(Me.btnSearchName)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.Show_IM_btn)
        Me.Controls.Add(Me.GM_Serach)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.MinimizeBox = False
        Me.Name = "Items_Search"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "بحث عن صنف"
        CType(Me.IMDataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.kbPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GM_Serach As System.Windows.Forms.ComboBox
    Friend WithEvents Show_IM_btn As System.Windows.Forms.Button
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchName As System.Windows.Forms.Button
    Friend WithEvents btnSearchBarcode As System.Windows.Forms.Button
    Friend WithEvents btnSearchItemNo As System.Windows.Forms.Button
    Friend WithEvents lblHint As System.Windows.Forms.Label
    Public WithEvents IMDataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents IM_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GM_NAME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_NUM_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents isValid_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UNIT_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ADD_NewGM_Btn As System.Windows.Forms.Button
    Friend WithEvents kbPanel As Panel
    Friend WithEvents KeyboardBtn As Button
    Friend WithEvents KB As OnKeyBoard
End Class
'------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
'Partial Class Items_Search
'    Inherits System.Windows.Forms.Form

'    'Form overrides dispose to clean up the component list.
'    <System.Diagnostics.DebuggerNonUserCode()> _
'    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
'        Try
'            If disposing AndAlso components IsNot Nothing Then
'                components.Dispose()
'            End If
'        Finally
'            MyBase.Dispose(disposing)
'        End Try
'    End Sub

'    'Required by the Windows Form Designer
'    Private components As System.ComponentModel.IContainer

'    'NOTE: The following procedure is required by the Windows Form Designer
'    'It can be modified using the Windows Form Designer.  
'    'Do not modify it using the code editor.
'    <System.Diagnostics.DebuggerStepThrough()> _
'    Private Sub InitializeComponent()
'        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
'        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
'        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
'        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
'        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
'        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
'        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Items_Search))
'        Me.Label3 = New System.Windows.Forms.Label()
'        Me.ExitFormButton = New System.Windows.Forms.Button()
'        Me.Label1 = New System.Windows.Forms.Label()
'        Me.IM_SH_txt = New System.Windows.Forms.TextBox()
'        Me.IMDataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
'        Me.GM_Serach = New System.Windows.Forms.ComboBox()
'        Me.Label2 = New System.Windows.Forms.Label()
'        Me.Show_IM_btn = New System.Windows.Forms.Button()
'        Me.ADD_NewGM_Btn = New System.Windows.Forms.Button()
'        Me.Label4 = New System.Windows.Forms.Label()
'        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
'        Me.IM_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
'        Me.GM_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
'        Me.Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
'        Me.IM_NUM_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
'        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
'        Me.isValid_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
'        Me.UNIT_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
'        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
'        Me.Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
'        Me.Label5 = New System.Windows.Forms.Label()
'        Me.IM_NUM_SH_txt = New System.Windows.Forms.TextBox()
'        CType(Me.IMDataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.SuspendLayout()
'        '
'        'Label3
'        '
'        Me.Label3.BackColor = System.Drawing.SystemColors.Control
'        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
'        Me.Label3.Location = New System.Drawing.Point(2, 2)
'        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
'        Me.Label3.Name = "Label3"
'        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.Label3.Size = New System.Drawing.Size(212, 52)
'        Me.Label3.TabIndex = 685
'        Me.Label3.Text = "بحث عن صنف"
'        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'        '
'        'ExitFormButton
'        '
'        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
'        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
'        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
'        Me.ExitFormButton.Font = New System.Drawing.Font("Tahoma", 10.25!)
'        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
'        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
'        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
'        Me.ExitFormButton.Location = New System.Drawing.Point(875, 585)
'        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
'        Me.ExitFormButton.Name = "ExitFormButton"
'        Me.ExitFormButton.Size = New System.Drawing.Size(100, 28)
'        Me.ExitFormButton.TabIndex = 674
'        Me.ExitFormButton.Text = "خروج"
'        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'        Me.ExitFormButton.UseVisualStyleBackColor = False
'        '
'        'Label1
'        '
'        Me.Label1.AutoSize = True
'        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.25!)
'        Me.Label1.Location = New System.Drawing.Point(919, 36)
'        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
'        Me.Label1.Name = "Label1"
'        Me.Label1.Size = New System.Drawing.Size(53, 17)
'        Me.Label1.TabIndex = 604
'        Me.Label1.Text = "الصنف :"
'        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
'        '
'        'IM_SH_txt
'        '
'        Me.IM_SH_txt.BackColor = System.Drawing.Color.White
'        Me.IM_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.IM_SH_txt.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.IM_SH_txt.Location = New System.Drawing.Point(639, 56)
'        Me.IM_SH_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
'        Me.IM_SH_txt.Name = "IM_SH_txt"
'        Me.IM_SH_txt.Size = New System.Drawing.Size(336, 26)
'        Me.IM_SH_txt.TabIndex = 600
'        '
'        'IMDataGridViewX
'        '
'        Me.IMDataGridViewX.AllowUserToAddRows = False
'        Me.IMDataGridViewX.AllowUserToDeleteRows = False
'        Me.IMDataGridViewX.BackgroundColor = System.Drawing.Color.White
'        Me.IMDataGridViewX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
'        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
'        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
'        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
'        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
'        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
'        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
'        Me.IMDataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
'        Me.IMDataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
'        Me.IMDataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_ID_CL, Me.GM_NAME_CL, Me.Barcode_CL, Me.IM_NUM_CL, Me.item_name_CL, Me.isValid_CL, Me.UNIT_CL, Me.QTY_CL, Me.Price_CL})
'        Me.IMDataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
'        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
'        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
'        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
'        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
'        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText
'        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
'        Me.IMDataGridViewX.DefaultCellStyle = DataGridViewCellStyle5
'        Me.IMDataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
'        Me.IMDataGridViewX.Location = New System.Drawing.Point(2, 83)
'        Me.IMDataGridViewX.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
'        Me.IMDataGridViewX.MultiSelect = False
'        Me.IMDataGridViewX.Name = "IMDataGridViewX"
'        Me.IMDataGridViewX.ReadOnly = True
'        Me.IMDataGridViewX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.IMDataGridViewX.RowHeadersVisible = False
'        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
'        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 10.25!, System.Drawing.FontStyle.Bold)
'        Me.IMDataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle6
'        Me.IMDataGridViewX.RowTemplate.Height = 30
'        Me.IMDataGridViewX.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
'        Me.IMDataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
'        Me.IMDataGridViewX.Size = New System.Drawing.Size(973, 498)
'        Me.IMDataGridViewX.TabIndex = 688
'        '
'        'GM_Serach
'        '
'        Me.GM_Serach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
'        Me.GM_Serach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
'        Me.GM_Serach.BackColor = System.Drawing.SystemColors.Info
'        Me.GM_Serach.Cursor = System.Windows.Forms.Cursors.Hand
'        Me.GM_Serach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.GM_Serach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.GM_Serach.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.GM_Serach.FormattingEnabled = True
'        Me.GM_Serach.IntegralHeight = False
'        Me.GM_Serach.Location = New System.Drawing.Point(643, 3)
'        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
'        Me.GM_Serach.Name = "GM_Serach"
'        Me.GM_Serach.Size = New System.Drawing.Size(261, 26)
'        Me.GM_Serach.TabIndex = 701
'        '
'        'Label2
'        '
'        Me.Label2.AutoSize = True
'        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.25!)
'        Me.Label2.Location = New System.Drawing.Point(909, 6)
'        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
'        Me.Label2.Name = "Label2"
'        Me.Label2.Size = New System.Drawing.Size(63, 17)
'        Me.Label2.TabIndex = 702
'        Me.Label2.Text = "التصنيف :"
'        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
'        '
'        'Show_IM_btn
'        '
'        Me.Show_IM_btn.BackColor = System.Drawing.Color.White
'        Me.Show_IM_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
'        Me.Show_IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
'        Me.Show_IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
'        Me.Show_IM_btn.Font = New System.Drawing.Font("Tahoma", 10.25!)
'        Me.Show_IM_btn.Location = New System.Drawing.Point(536, 2)
'        Me.Show_IM_btn.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
'        Me.Show_IM_btn.Name = "Show_IM_btn"
'        Me.Show_IM_btn.Size = New System.Drawing.Size(105, 25)
'        Me.Show_IM_btn.TabIndex = 703
'        Me.Show_IM_btn.Text = "فلترة التصنيف"
'        Me.Show_IM_btn.UseVisualStyleBackColor = False
'        '
'        'ADD_NewGM_Btn
'        '
'        Me.ADD_NewGM_Btn.BackColor = System.Drawing.Color.White
'        Me.ADD_NewGM_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
'        Me.ADD_NewGM_Btn.Cursor = System.Windows.Forms.Cursors.Hand
'        Me.ADD_NewGM_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
'        Me.ADD_NewGM_Btn.Font = New System.Drawing.Font("Tahoma", 10.25!)
'        Me.ADD_NewGM_Btn.Image = Global.resturant.My.Resources.Resources.if_Add_27831
'        Me.ADD_NewGM_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
'        Me.ADD_NewGM_Btn.Location = New System.Drawing.Point(2, 584)
'        Me.ADD_NewGM_Btn.Name = "ADD_NewGM_Btn"
'        Me.ADD_NewGM_Btn.Size = New System.Drawing.Size(149, 30)
'        Me.ADD_NewGM_Btn.TabIndex = 704
'        Me.ADD_NewGM_Btn.Text = "إضافة صنف جديد"
'        Me.ADD_NewGM_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'        Me.ADD_NewGM_Btn.UseVisualStyleBackColor = False
'        '
'        'Label4
'        '
'        Me.Label4.AutoSize = True
'        Me.Label4.Font = New System.Drawing.Font("Tahoma", 10.25!)
'        Me.Label4.Location = New System.Drawing.Point(582, 36)
'        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
'        Me.Label4.Name = "Label4"
'        Me.Label4.Size = New System.Drawing.Size(50, 17)
'        Me.Label4.TabIndex = 706
'        Me.Label4.Text = "باركود :"
'        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
'        '
'        'Barcode_SH_txt
'        '
'        Me.Barcode_SH_txt.BackColor = System.Drawing.Color.White
'        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Barcode_SH_txt.Location = New System.Drawing.Point(302, 56)
'        Me.Barcode_SH_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
'        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
'        Me.Barcode_SH_txt.Size = New System.Drawing.Size(336, 26)
'        Me.Barcode_SH_txt.TabIndex = 705
'        '
'        'IM_ID_CL
'        '
'        Me.IM_ID_CL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
'        Me.IM_ID_CL.DataPropertyName = "IM_ID"
'        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.IM_ID_CL.DefaultCellStyle = DataGridViewCellStyle2
'        Me.IM_ID_CL.FillWeight = 5.0!
'        Me.IM_ID_CL.Frozen = True
'        Me.IM_ID_CL.HeaderText = "رقم الصنف"
'        Me.IM_ID_CL.Name = "IM_ID_CL"
'        Me.IM_ID_CL.ReadOnly = True
'        Me.IM_ID_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
'        Me.IM_ID_CL.Visible = False
'        Me.IM_ID_CL.Width = 5
'        '
'        'GM_NAME_CL
'        '
'        Me.GM_NAME_CL.DataPropertyName = "GM_NAME"
'        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
'        Me.GM_NAME_CL.DefaultCellStyle = DataGridViewCellStyle3
'        Me.GM_NAME_CL.FillWeight = 101.0!
'        Me.GM_NAME_CL.HeaderText = "التصنيف"
'        Me.GM_NAME_CL.Name = "GM_NAME_CL"
'        Me.GM_NAME_CL.ReadOnly = True
'        Me.GM_NAME_CL.Width = 200
'        '
'        'Barcode_CL
'        '
'        Me.Barcode_CL.DataPropertyName = "Barcode"
'        Me.Barcode_CL.HeaderText = "باركود"
'        Me.Barcode_CL.Name = "Barcode_CL"
'        Me.Barcode_CL.ReadOnly = True
'        '
'        'IM_NUM_CL
'        '
'        Me.IM_NUM_CL.DataPropertyName = "IM_NUM"
'        Me.IM_NUM_CL.HeaderText = "رقم الصنف"
'        Me.IM_NUM_CL.Name = "IM_NUM_CL"
'        Me.IM_NUM_CL.ReadOnly = True
'        '
'        'item_name_CL
'        '
'        Me.item_name_CL.DataPropertyName = "item_name"
'        Me.item_name_CL.FillWeight = 150.0!
'        Me.item_name_CL.HeaderText = "الصنف"
'        Me.item_name_CL.Name = "item_name_CL"
'        Me.item_name_CL.ReadOnly = True
'        Me.item_name_CL.Width = 350
'        '
'        'isValid_CL
'        '
'        Me.isValid_CL.DataPropertyName = "isValid"
'        Me.isValid_CL.HeaderText = "صلاحية"
'        Me.isValid_CL.Name = "isValid_CL"
'        Me.isValid_CL.ReadOnly = True
'        Me.isValid_CL.Visible = False
'        '
'        'UNIT_CL
'        '
'        Me.UNIT_CL.DataPropertyName = "U_NAME"
'        Me.UNIT_CL.FillWeight = 40.0!
'        Me.UNIT_CL.HeaderText = "الوحدة"
'        Me.UNIT_CL.Name = "UNIT_CL"
'        Me.UNIT_CL.ReadOnly = True
'        Me.UNIT_CL.Width = 60
'        '
'        'QTY_CL
'        '
'        Me.QTY_CL.DataPropertyName = "QTY"
'        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Green
'        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle4
'        Me.QTY_CL.FillWeight = 60.0!
'        Me.QTY_CL.HeaderText = "الكمية"
'        Me.QTY_CL.Name = "QTY_CL"
'        Me.QTY_CL.ReadOnly = True
'        Me.QTY_CL.Width = 70
'        '
'        'Price_CL
'        '
'        Me.Price_CL.DataPropertyName = "Price"
'        Me.Price_CL.HeaderText = "السعر"
'        Me.Price_CL.Name = "Price_CL"
'        Me.Price_CL.ReadOnly = True
'        '
'        'Label5
'        '
'        Me.Label5.AutoSize = True
'        Me.Label5.Font = New System.Drawing.Font("Tahoma", 10.25!)
'        Me.Label5.Location = New System.Drawing.Point(220, 36)
'        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
'        Me.Label5.Name = "Label5"
'        Me.Label5.Size = New System.Drawing.Size(78, 17)
'        Me.Label5.TabIndex = 708
'        Me.Label5.Text = "رقم الصنف :"
'        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
'        '
'        'IM_NUM_SH_txt
'        '
'        Me.IM_NUM_SH_txt.BackColor = System.Drawing.Color.White
'        Me.IM_NUM_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.IM_NUM_SH_txt.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.IM_NUM_SH_txt.Location = New System.Drawing.Point(2, 56)
'        Me.IM_NUM_SH_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
'        Me.IM_NUM_SH_txt.Name = "IM_NUM_SH_txt"
'        Me.IM_NUM_SH_txt.Size = New System.Drawing.Size(299, 26)
'        Me.IM_NUM_SH_txt.TabIndex = 707
'        '
'        'Items_Search
'        '
'        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
'        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
'        Me.ClientSize = New System.Drawing.Size(976, 614)
'        Me.ControlBox = False
'        Me.Controls.Add(Me.Label5)
'        Me.Controls.Add(Me.IM_NUM_SH_txt)
'        Me.Controls.Add(Me.Label4)
'        Me.Controls.Add(Me.Barcode_SH_txt)
'        Me.Controls.Add(Me.ADD_NewGM_Btn)
'        Me.Controls.Add(Me.Show_IM_btn)
'        Me.Controls.Add(Me.Label2)
'        Me.Controls.Add(Me.GM_Serach)
'        Me.Controls.Add(Me.IMDataGridViewX)
'        Me.Controls.Add(Me.Label1)
'        Me.Controls.Add(Me.IM_SH_txt)
'        Me.Controls.Add(Me.Label3)
'        Me.Controls.Add(Me.ExitFormButton)
'        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
'        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
'        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
'        Me.Name = "Items_Search"
'        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
'        Me.Text = "يحث عن صنف"
'        CType(Me.IMDataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.ResumeLayout(False)
'        Me.PerformLayout()

'    End Sub
'    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
'    Friend WithEvents Label3 As System.Windows.Forms.Label
'    Friend WithEvents Label1 As System.Windows.Forms.Label
'    Friend WithEvents IM_SH_txt As System.Windows.Forms.TextBox
'    Public WithEvents IMDataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
'    Friend WithEvents GM_Serach As System.Windows.Forms.ComboBox
'    Friend WithEvents Label2 As System.Windows.Forms.Label
'    Friend WithEvents Show_IM_btn As System.Windows.Forms.Button
'    Friend WithEvents ADD_NewGM_Btn As System.Windows.Forms.Button
'    Friend WithEvents Label4 As Label
'    Friend WithEvents Barcode_SH_txt As TextBox
'    Friend WithEvents IM_ID_CL As DataGridViewTextBoxColumn
'    Friend WithEvents GM_NAME_CL As DataGridViewTextBoxColumn
'    Friend WithEvents Barcode_CL As DataGridViewTextBoxColumn
'    Friend WithEvents IM_NUM_CL As DataGridViewTextBoxColumn
'    Friend WithEvents item_name_CL As DataGridViewTextBoxColumn
'    Friend WithEvents isValid_CL As DataGridViewTextBoxColumn
'    Friend WithEvents UNIT_CL As DataGridViewTextBoxColumn
'    Friend WithEvents QTY_CL As DataGridViewTextBoxColumn
'    Friend WithEvents Price_CL As DataGridViewTextBoxColumn
'    Friend WithEvents Label5 As Label
'    Friend WithEvents IM_NUM_SH_txt As TextBox
'End Class
