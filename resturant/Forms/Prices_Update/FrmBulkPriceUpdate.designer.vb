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
        CType(Me.dgvPrices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
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
        Me.dgvPrices.Location = New System.Drawing.Point(3, 137)
        Me.dgvPrices.Name = "dgvPrices"
        Me.dgvPrices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPrices.Size = New System.Drawing.Size(1001, 451)
        Me.dgvPrices.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkOfferPrice)
        Me.GroupBox1.Controls.Add(Me.chkMinSP2)
        Me.GroupBox1.Controls.Add(Me.chkMinSP)
        Me.GroupBox1.Controls.Add(Me.chkPrice)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 31)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(330, 60)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "أنواع الأسعار المطلوب تعديلها"
        '
        'chkOfferPrice
        '
        Me.chkOfferPrice.AutoSize = True
        Me.chkOfferPrice.Location = New System.Drawing.Point(270, 25)
        Me.chkOfferPrice.Name = "chkOfferPrice"
        Me.chkOfferPrice.Size = New System.Drawing.Size(51, 17)
        Me.chkOfferPrice.TabIndex = 3
        Me.chkOfferPrice.Text = "عرض"
        Me.chkOfferPrice.UseVisualStyleBackColor = True
        '
        'chkMinSP2
        '
        Me.chkMinSP2.AutoSize = True
        Me.chkMinSP2.Location = New System.Drawing.Point(190, 25)
        Me.chkMinSP2.Name = "chkMinSP2"
        Me.chkMinSP2.Size = New System.Drawing.Size(77, 17)
        Me.chkMinSP2.TabIndex = 2
        Me.chkMinSP2.Text = "جملة جملة"
        Me.chkMinSP2.UseVisualStyleBackColor = True
        '
        'chkMinSP
        '
        Me.chkMinSP.AutoSize = True
        Me.chkMinSP.Location = New System.Drawing.Point(130, 25)
        Me.chkMinSP.Name = "chkMinSP"
        Me.chkMinSP.Size = New System.Drawing.Size(50, 17)
        Me.chkMinSP.TabIndex = 1
        Me.chkMinSP.Text = "جملة"
        Me.chkMinSP.UseVisualStyleBackColor = True
        '
        'chkPrice
        '
        Me.chkPrice.AutoSize = True
        Me.chkPrice.Checked = True
        Me.chkPrice.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPrice.Location = New System.Drawing.Point(20, 25)
        Me.chkPrice.Name = "chkPrice"
        Me.chkPrice.Size = New System.Drawing.Size(104, 17)
        Me.chkPrice.TabIndex = 0
        Me.chkPrice.Text = "سعر البيع العادي"
        Me.chkPrice.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbDecrease)
        Me.GroupBox2.Controls.Add(Me.rbIncrease)
        Me.GroupBox2.Location = New System.Drawing.Point(346, 31)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(130, 60)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "نوع العملية"
        '
        'rbDecrease
        '
        Me.rbDecrease.AutoSize = True
        Me.rbDecrease.Location = New System.Drawing.Point(79, 25)
        Me.rbDecrease.Name = "rbDecrease"
        Me.rbDecrease.Size = New System.Drawing.Size(47, 17)
        Me.rbDecrease.TabIndex = 1
        Me.rbDecrease.Text = "نقص"
        Me.rbDecrease.UseVisualStyleBackColor = True
        '
        'rbIncrease
        '
        Me.rbIncrease.AutoSize = True
        Me.rbIncrease.Checked = True
        Me.rbIncrease.Location = New System.Drawing.Point(20, 25)
        Me.rbIncrease.Name = "rbIncrease"
        Me.rbIncrease.Size = New System.Drawing.Size(45, 17)
        Me.rbIncrease.TabIndex = 0
        Me.rbIncrease.TabStop = True
        Me.rbIncrease.Text = "زيادة"
        Me.rbIncrease.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbPercent)
        Me.GroupBox3.Controls.Add(Me.rbValue)
        Me.GroupBox3.Location = New System.Drawing.Point(479, 31)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(148, 60)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "طريقة التعديل"
        '
        'rbPercent
        '
        Me.rbPercent.AutoSize = True
        Me.rbPercent.Location = New System.Drawing.Point(66, 25)
        Me.rbPercent.Name = "rbPercent"
        Me.rbPercent.Size = New System.Drawing.Size(77, 17)
        Me.rbPercent.TabIndex = 1
        Me.rbPercent.Text = "نسبة مئوية"
        Me.rbPercent.UseVisualStyleBackColor = True
        '
        'rbValue
        '
        Me.rbValue.AutoSize = True
        Me.rbValue.Checked = True
        Me.rbValue.Location = New System.Drawing.Point(7, 25)
        Me.rbValue.Name = "rbValue"
        Me.rbValue.Size = New System.Drawing.Size(47, 17)
        Me.rbValue.TabIndex = 0
        Me.rbValue.TabStop = True
        Me.rbValue.Text = "قيمة"
        Me.rbValue.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(630, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "قيمة / نسبة التعديل"
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(734, 61)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(260, 20)
        Me.txtReason.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(789, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "سبب التعديل (اختياري)"
        '
        'chkOnlySelected
        '
        Me.chkOnlySelected.AutoSize = True
        Me.chkOnlySelected.Location = New System.Drawing.Point(814, 84)
        Me.chkOnlySelected.Name = "chkOnlySelected"
        Me.chkOnlySelected.Size = New System.Drawing.Size(180, 17)
        Me.chkOnlySelected.TabIndex = 8
        Me.chkOnlySelected.Text = "تطبيق فقط على الصفوف المحددة"
        Me.chkOnlySelected.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(291, 2)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(120, 30)
        Me.btnLoad.TabIndex = 9
        Me.btnLoad.Text = "تحميل البيانات"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(329, 105)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(120, 30)
        Me.btnPreview.TabIndex = 10
        Me.btnPreview.Text = "معاينة"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnConfirm
        '
        Me.btnConfirm.Location = New System.Drawing.Point(452, 105)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(120, 30)
        Me.btnConfirm.TabIndex = 11
        Me.btnConfirm.Text = "تأكيد التعديل"
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'lblTotalIncrease
        '
        Me.lblTotalIncrease.AutoSize = True
        Me.lblTotalIncrease.Location = New System.Drawing.Point(838, 121)
        Me.lblTotalIncrease.Name = "lblTotalIncrease"
        Me.lblTotalIncrease.Size = New System.Drawing.Size(113, 13)
        Me.lblTotalIncrease.TabIndex = 12
        Me.lblTotalIncrease.Text = "إجمالي الزيادات: 0.000"
        '
        'lblTotalDecrease
        '
        Me.lblTotalDecrease.AutoSize = True
        Me.lblTotalDecrease.Location = New System.Drawing.Point(707, 121)
        Me.lblTotalDecrease.Name = "lblTotalDecrease"
        Me.lblTotalDecrease.Size = New System.Drawing.Size(108, 13)
        Me.lblTotalDecrease.TabIndex = 13
        Me.lblTotalDecrease.Text = "إجمالي النقص: 0.000"
        '
        'lblNetChange
        '
        Me.lblNetChange.AutoSize = True
        Me.lblNetChange.Location = New System.Drawing.Point(589, 121)
        Me.lblNetChange.Name = "lblNetChange"
        Me.lblNetChange.Size = New System.Drawing.Size(98, 13)
        Me.lblNetChange.TabIndex = 14
        Me.lblNetChange.Text = "صافي التغير: 0.000"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Label4.Location = New System.Drawing.Point(237, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
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
        Me.GM_Serach.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Serach.Location = New System.Drawing.Point(3, 3)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GM_Serach.Size = New System.Drawing.Size(229, 24)
        Me.GM_Serach.TabIndex = 656
        '
        'btnExportGridProtected
        '
        Me.btnExportGridProtected.Location = New System.Drawing.Point(125, 101)
        Me.btnExportGridProtected.Name = "btnExportGridProtected"
        Me.btnExportGridProtected.Size = New System.Drawing.Size(120, 30)
        Me.btnExportGridProtected.TabIndex = 657
        Me.btnExportGridProtected.Text = "تصدير Excel"
        Me.btnExportGridProtected.UseVisualStyleBackColor = True
        '
        'btnImportFromExcel
        '
        Me.btnImportFromExcel.Location = New System.Drawing.Point(3, 101)
        Me.btnImportFromExcel.Name = "btnImportFromExcel"
        Me.btnImportFromExcel.Size = New System.Drawing.Size(120, 30)
        Me.btnImportFromExcel.TabIndex = 658
        Me.btnImportFromExcel.Text = "استيراد من Excel"
        Me.btnImportFromExcel.UseVisualStyleBackColor = True
        '
        'txtValue
        '
        Me.txtValue.BackColor = System.Drawing.Color.Lavender
        Me.txtValue.Location = New System.Drawing.Point(631, 61)
        Me.txtValue.MaxLength = 0
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(100, 20)
        Me.txtValue.TabIndex = 15
        '
        'FrmBulkPriceUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 590)
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
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmBulkPriceUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تعديل الأسعار الجماعي مع المعاينة"
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
    Friend WithEvents txtValue As F2FloatField
    Friend WithEvents Label4 As Label
    Friend WithEvents GM_Serach As ComboBox
    Friend WithEvents btnExportGridProtected As Button
    Friend WithEvents btnImportFromExcel As Button
End Class
