<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBulkPriceUpdate
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
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.MinFormButton = New System.Windows.Forms.Button()
        Me.MaxFormButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.dgvPrices = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkOfferPrice = New System.Windows.Forms.CheckBox()
        Me.chkMinSP2 = New System.Windows.Forms.CheckBox()
        Me.chkMinSP = New System.Windows.Forms.CheckBox()
        Me.chkPrice = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbDecrease = New System.Windows.Forms.RadioButton()
        Me.rbIncrease = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rbPercent = New System.Windows.Forms.RadioButton()
        Me.rbValue = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkOnlySelected = New System.Windows.Forms.CheckBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.lblTotalIncrease = New System.Windows.Forms.Label()
        Me.lblTotalDecrease = New System.Windows.Forms.Label()
        Me.lblNetChange = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GM_Serach = New System.Windows.Forms.ComboBox()
        Me.btnExportGridProtected = New System.Windows.Forms.Button()
        Me.btnImportFromExcel = New System.Windows.Forms.Button()
        Me.txtValue = New resturant.F2FloatField()
        Me.TitleBar_Panel.SuspendLayout()
        CType(Me.dgvPrices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.TitleBar_Panel.Size = New System.Drawing.Size(1061, 36)
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
        Me.MinFormButton.Size = New System.Drawing.Size(45, 36)
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
        Me.MaxFormButton.Size = New System.Drawing.Size(45, 36)
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
        Me.ExitFormButton.Size = New System.Drawing.Size(45, 36)
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
        Me.TopTitle_LB.Location = New System.Drawing.Point(817, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(230, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "تعديل الأسعار الجماعي مع المعاينة"
        '
        'dgvPrices
        '
        Me.dgvPrices.AllowUserToAddRows = False
        Me.dgvPrices.AllowUserToDeleteRows = False
        Me.dgvPrices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPrices.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPrices.Location = New System.Drawing.Point(3, 177)
        Me.dgvPrices.Name = "dgvPrices"
        Me.dgvPrices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPrices.Size = New System.Drawing.Size(1058, 451)
        Me.dgvPrices.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkOfferPrice)
        Me.GroupBox1.Controls.Add(Me.chkMinSP2)
        Me.GroupBox1.Controls.Add(Me.chkMinSP)
        Me.GroupBox1.Controls.Add(Me.chkPrice)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 71)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(348, 60)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "أنواع الأسعار المطلوب تعديلها"
        '
        'chkOfferPrice
        '
        Me.chkOfferPrice.AutoSize = True
        Me.chkOfferPrice.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkOfferPrice.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkOfferPrice.Location = New System.Drawing.Point(281, 25)
        Me.chkOfferPrice.Name = "chkOfferPrice"
        Me.chkOfferPrice.Size = New System.Drawing.Size(53, 19)
        Me.chkOfferPrice.TabIndex = 3
        Me.chkOfferPrice.Text = "عرض"
        Me.chkOfferPrice.UseVisualStyleBackColor = True
        '
        'chkMinSP2
        '
        Me.chkMinSP2.AutoSize = True
        Me.chkMinSP2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkMinSP2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkMinSP2.Location = New System.Drawing.Point(194, 25)
        Me.chkMinSP2.Name = "chkMinSP2"
        Me.chkMinSP2.Size = New System.Drawing.Size(77, 19)
        Me.chkMinSP2.TabIndex = 2
        Me.chkMinSP2.Text = "جملة جملة"
        Me.chkMinSP2.UseVisualStyleBackColor = True
        '
        'chkMinSP
        '
        Me.chkMinSP.AutoSize = True
        Me.chkMinSP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkMinSP.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkMinSP.Location = New System.Drawing.Point(134, 25)
        Me.chkMinSP.Name = "chkMinSP"
        Me.chkMinSP.Size = New System.Drawing.Size(50, 19)
        Me.chkMinSP.TabIndex = 1
        Me.chkMinSP.Text = "جملة"
        Me.chkMinSP.UseVisualStyleBackColor = True
        '
        'chkPrice
        '
        Me.chkPrice.AutoSize = True
        Me.chkPrice.Checked = True
        Me.chkPrice.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPrice.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkPrice.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkPrice.Location = New System.Drawing.Point(9, 25)
        Me.chkPrice.Name = "chkPrice"
        Me.chkPrice.Size = New System.Drawing.Size(108, 19)
        Me.chkPrice.TabIndex = 0
        Me.chkPrice.Text = "سعر البيع العادي"
        Me.chkPrice.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbDecrease)
        Me.GroupBox2.Controls.Add(Me.rbIncrease)
        Me.GroupBox2.Location = New System.Drawing.Point(362, 71)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox2.Size = New System.Drawing.Size(130, 60)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "نوع العملية"
        '
        'rbDecrease
        '
        Me.rbDecrease.AutoSize = True
        Me.rbDecrease.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbDecrease.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.rbDecrease.Location = New System.Drawing.Point(70, 25)
        Me.rbDecrease.Name = "rbDecrease"
        Me.rbDecrease.Size = New System.Drawing.Size(51, 19)
        Me.rbDecrease.TabIndex = 1
        Me.rbDecrease.Text = "نقص"
        Me.rbDecrease.UseVisualStyleBackColor = True
        '
        'rbIncrease
        '
        Me.rbIncrease.AutoSize = True
        Me.rbIncrease.Checked = True
        Me.rbIncrease.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbIncrease.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.rbIncrease.Location = New System.Drawing.Point(14, 25)
        Me.rbIncrease.Name = "rbIncrease"
        Me.rbIncrease.Size = New System.Drawing.Size(50, 19)
        Me.rbIncrease.TabIndex = 0
        Me.rbIncrease.TabStop = True
        Me.rbIncrease.Text = "زيادة"
        Me.rbIncrease.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbPercent)
        Me.GroupBox3.Controls.Add(Me.rbValue)
        Me.GroupBox3.Location = New System.Drawing.Point(494, 71)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox3.Size = New System.Drawing.Size(164, 60)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "طريقة التعديل"
        '
        'rbPercent
        '
        Me.rbPercent.AutoSize = True
        Me.rbPercent.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbPercent.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.rbPercent.Location = New System.Drawing.Point(66, 25)
        Me.rbPercent.Name = "rbPercent"
        Me.rbPercent.Size = New System.Drawing.Size(80, 19)
        Me.rbPercent.TabIndex = 1
        Me.rbPercent.Text = "نسبة مئوية"
        Me.rbPercent.UseVisualStyleBackColor = True
        '
        'rbValue
        '
        Me.rbValue.AutoSize = True
        Me.rbValue.Checked = True
        Me.rbValue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbValue.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.rbValue.Location = New System.Drawing.Point(7, 25)
        Me.rbValue.Name = "rbValue"
        Me.rbValue.Size = New System.Drawing.Size(48, 19)
        Me.rbValue.TabIndex = 0
        Me.rbValue.TabStop = True
        Me.rbValue.Text = "قيمة"
        Me.rbValue.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(677, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 19)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "قيمة / نسبة التعديل"
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(797, 93)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtReason.Size = New System.Drawing.Size(260, 25)
        Me.txtReason.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(863, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(142, 19)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "سبب التعديل (اختياري)"
        '
        'chkOnlySelected
        '
        Me.chkOnlySelected.AutoSize = True
        Me.chkOnlySelected.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkOnlySelected.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkOnlySelected.Location = New System.Drawing.Point(863, 124)
        Me.chkOnlySelected.Name = "chkOnlySelected"
        Me.chkOnlySelected.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkOnlySelected.Size = New System.Drawing.Size(194, 19)
        Me.chkOnlySelected.TabIndex = 8
        Me.chkOnlySelected.Text = "تطبيق فقط على الصفوف المحددة"
        Me.chkOnlySelected.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnLoad.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Location = New System.Drawing.Point(372, 39)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(120, 28)
        Me.btnLoad.TabIndex = 9
        Me.btnLoad.Text = "تحميل البيانات"
        Me.btnLoad.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Location = New System.Drawing.Point(326, 141)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(120, 30)
        Me.btnPreview.TabIndex = 10
        Me.btnPreview.Text = "معاينة"
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'btnConfirm
        '
        Me.btnConfirm.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfirm.Location = New System.Drawing.Point(452, 141)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(120, 30)
        Me.btnConfirm.TabIndex = 11
        Me.btnConfirm.Tag = "SAVE"
        Me.btnConfirm.Text = "تأكيد التعديل"
        Me.btnConfirm.UseVisualStyleBackColor = False
        '
        'lblTotalIncrease
        '
        Me.lblTotalIncrease.AutoSize = True
        Me.lblTotalIncrease.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblTotalIncrease.Location = New System.Drawing.Point(891, 154)
        Me.lblTotalIncrease.Name = "lblTotalIncrease"
        Me.lblTotalIncrease.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalIncrease.Size = New System.Drawing.Size(145, 19)
        Me.lblTotalIncrease.TabIndex = 12
        Me.lblTotalIncrease.Text = "إجمالي الزيادات: 0.000"
        '
        'lblTotalDecrease
        '
        Me.lblTotalDecrease.AutoSize = True
        Me.lblTotalDecrease.ForeColor = System.Drawing.Color.IndianRed
        Me.lblTotalDecrease.Location = New System.Drawing.Point(739, 154)
        Me.lblTotalDecrease.Name = "lblTotalDecrease"
        Me.lblTotalDecrease.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalDecrease.Size = New System.Drawing.Size(136, 19)
        Me.lblTotalDecrease.TabIndex = 13
        Me.lblTotalDecrease.Text = "إجمالي النقص: 0.000"
        '
        'lblNetChange
        '
        Me.lblNetChange.AutoSize = True
        Me.lblNetChange.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblNetChange.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblNetChange.Location = New System.Drawing.Point(589, 154)
        Me.lblNetChange.Name = "lblNetChange"
        Me.lblNetChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblNetChange.Size = New System.Drawing.Size(125, 19)
        Me.lblNetChange.TabIndex = 14
        Me.lblNetChange.Text = "صافي التغير: 0.000"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(299, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 19)
        Me.Label4.TabIndex = 655
        Me.Label4.Text = "التصنيف"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GM_Serach
        '
        Me.GM_Serach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.GM_Serach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.GM_Serach.BackColor = System.Drawing.SystemColors.Info
        Me.GM_Serach.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GM_Serach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GM_Serach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Location = New System.Drawing.Point(12, 42)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GM_Serach.Size = New System.Drawing.Size(282, 25)
        Me.GM_Serach.TabIndex = 656
        '
        'btnExportGridProtected
        '
        Me.btnExportGridProtected.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnExportGridProtected.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExportGridProtected.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportGridProtected.Location = New System.Drawing.Point(125, 141)
        Me.btnExportGridProtected.Name = "btnExportGridProtected"
        Me.btnExportGridProtected.Size = New System.Drawing.Size(120, 30)
        Me.btnExportGridProtected.TabIndex = 657
        Me.btnExportGridProtected.Text = "تصدير Excel"
        Me.btnExportGridProtected.UseVisualStyleBackColor = False
        '
        'btnImportFromExcel
        '
        Me.btnImportFromExcel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnImportFromExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImportFromExcel.Location = New System.Drawing.Point(3, 141)
        Me.btnImportFromExcel.Name = "btnImportFromExcel"
        Me.btnImportFromExcel.Size = New System.Drawing.Size(120, 30)
        Me.btnImportFromExcel.TabIndex = 658
        Me.btnImportFromExcel.Text = "استيراد من Excel"
        Me.btnImportFromExcel.UseVisualStyleBackColor = False
        '
        'txtValue
        '
        Me.txtValue.BackColor = System.Drawing.Color.Lavender
        Me.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValue.Location = New System.Drawing.Point(691, 94)
        Me.txtValue.MaxLength = 0
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(100, 25)
        Me.txtValue.TabIndex = 15
        '
        'FrmBulkPriceUpdate
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1061, 630)
        Me.ControlBox = False
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.btnImportFromExcel)
        Me.Controls.Add(Me.btnExportGridProtected)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GM_Serach)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.lblNetChange)
        Me.Controls.Add(Me.lblTotalDecrease)
        Me.Controls.Add(Me.lblTotalIncrease)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.chkOnlySelected)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtReason)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvPrices)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmBulkPriceUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تعديل الأسعار الجماعي مع المعاينة"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        CType(Me.dgvPrices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents MaxFormButton As System.Windows.Forms.Button
    Friend WithEvents MinFormButton As System.Windows.Forms.Button

    Friend WithEvents dgvPrices As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents chkOfferPrice As CheckBox
    Friend WithEvents chkMinSP2 As CheckBox
    Friend WithEvents chkMinSP As CheckBox
    Friend WithEvents chkPrice As CheckBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents rbDecrease As RadioButton
    Friend WithEvents rbIncrease As RadioButton
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents rbPercent As RadioButton
    Friend WithEvents rbValue As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents txtReason As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents chkOnlySelected As CheckBox
    Friend WithEvents btnLoad As Button
    Friend WithEvents btnPreview As Button
    Friend WithEvents btnConfirm As Button
    Friend WithEvents lblTotalIncrease As Label
    Friend WithEvents lblTotalDecrease As Label
    Friend WithEvents lblNetChange As Label
    Friend WithEvents txtValue As resturant.F2FloatField
    Friend WithEvents Label4 As Label
    Friend WithEvents GM_Serach As ComboBox
    Friend WithEvents btnExportGridProtected As Button
    Friend WithEvents btnImportFromExcel As Button
End Class