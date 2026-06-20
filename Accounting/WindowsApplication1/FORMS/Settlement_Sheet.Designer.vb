<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Settlement_Sheet
    Inherits Base_Form
    'Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.dgvSystem = New System.Windows.Forms.DataGridView()
        Me.dgvBank = New System.Windows.Forms.DataGridView()
        Me.cmbMapMove = New System.Windows.Forms.ComboBox()
        Me.cmbMapDate = New System.Windows.Forms.ComboBox()
        Me.cmbMapValue = New System.Windows.Forms.ComboBox()
        Me.btnImportBank = New System.Windows.Forms.Button()
        Me.btnMatch = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabNotInSystem = New System.Windows.Forms.TabPage()
        Me.NO_DIF_Label = New System.Windows.Forms.Label()
        Me.dgvNotInSystem = New System.Windows.Forms.DataGridView()
        Me.TabNotInBank = New System.Windows.Forms.TabPage()
        Me.dgvNotInBank = New System.Windows.Forms.DataGridView()
        Me.TabDiff = New System.Windows.Forms.TabPage()
        Me.dgvDiff = New System.Windows.Forms.DataGridView()
        Me.TabJournal = New System.Windows.Forms.TabPage()
        Me.dgvJournal = New System.Windows.Forms.DataGridView()
        Me.LabelMove = New System.Windows.Forms.Label()
        Me.LabelDate = New System.Windows.Forms.Label()
        Me.LabelValue = New System.Windows.Forms.Label()
        Me.PanelSummary = New System.Windows.Forms.Panel()
        Me.lblTotalBank = New System.Windows.Forms.Label()
        Me.lblTotalSystem = New System.Windows.Forms.Label()
        Me.lblNotInSystem = New System.Windows.Forms.Label()
        Me.lblNotInBank = New System.Windows.Forms.Label()
        Me.lblDiff = New System.Windows.Forms.Label()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.Settlement_Btn = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ALL_CB = New System.Windows.Forms.CheckBox()
        CType(Me.dgvSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvBank, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabNotInSystem.SuspendLayout()
        CType(Me.dgvNotInSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabNotInBank.SuspendLayout()
        CType(Me.dgvNotInBank, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabDiff.SuspendLayout()
        CType(Me.dgvDiff, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabJournal.SuspendLayout()
        CType(Me.dgvJournal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelSummary.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvSystem
        '
        Me.dgvSystem.AllowUserToAddRows = False
        Me.dgvSystem.AllowUserToDeleteRows = False
        Me.dgvSystem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSystem.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvSystem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSystem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSystem.Location = New System.Drawing.Point(3, 19)
        Me.dgvSystem.Name = "dgvSystem"
        Me.dgvSystem.ReadOnly = True
        Me.dgvSystem.Size = New System.Drawing.Size(493, 241)
        Me.dgvSystem.TabIndex = 0
        '
        'dgvBank
        '
        Me.dgvBank.AllowUserToAddRows = False
        Me.dgvBank.AllowUserToDeleteRows = False
        Me.dgvBank.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvBank.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.dgvBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBank.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBank.Location = New System.Drawing.Point(3, 19)
        Me.dgvBank.Name = "dgvBank"
        Me.dgvBank.ReadOnly = True
        Me.dgvBank.Size = New System.Drawing.Size(498, 240)
        Me.dgvBank.TabIndex = 1
        '
        'cmbMapMove
        '
        Me.cmbMapMove.BackColor = System.Drawing.SystemColors.Info
        Me.cmbMapMove.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMapMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbMapMove.Location = New System.Drawing.Point(5, 273)
        Me.cmbMapMove.Name = "cmbMapMove"
        Me.cmbMapMove.Size = New System.Drawing.Size(154, 24)
        Me.cmbMapMove.TabIndex = 2
        '
        'cmbMapDate
        '
        Me.cmbMapDate.BackColor = System.Drawing.SystemColors.Info
        Me.cmbMapDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMapDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbMapDate.Location = New System.Drawing.Point(240, 272)
        Me.cmbMapDate.Name = "cmbMapDate"
        Me.cmbMapDate.Size = New System.Drawing.Size(121, 24)
        Me.cmbMapDate.TabIndex = 3
        '
        'cmbMapValue
        '
        Me.cmbMapValue.BackColor = System.Drawing.SystemColors.Info
        Me.cmbMapValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMapValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbMapValue.Location = New System.Drawing.Point(430, 272)
        Me.cmbMapValue.Name = "cmbMapValue"
        Me.cmbMapValue.Size = New System.Drawing.Size(121, 24)
        Me.cmbMapValue.TabIndex = 4
        '
        'btnImportBank
        '
        Me.btnImportBank.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImportBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImportBank.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportBank.Location = New System.Drawing.Point(842, 271)
        Me.btnImportBank.Name = "btnImportBank"
        Me.btnImportBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnImportBank.Size = New System.Drawing.Size(163, 30)
        Me.btnImportBank.TabIndex = 8
        Me.btnImportBank.Text = "📂 استيراد ملف المصرف"
        Me.btnImportBank.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImportBank.UseVisualStyleBackColor = True
        '
        'btnMatch
        '
        Me.btnMatch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMatch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMatch.Location = New System.Drawing.Point(718, 271)
        Me.btnMatch.Name = "btnMatch"
        Me.btnMatch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnMatch.Size = New System.Drawing.Size(123, 30)
        Me.btnMatch.TabIndex = 9
        Me.btnMatch.Text = "⚖️ تنفيذ المطابقة"
        Me.btnMatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMatch.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabNotInSystem)
        Me.TabControl1.Controls.Add(Me.TabNotInBank)
        Me.TabControl1.Controls.Add(Me.TabDiff)
        Me.TabControl1.Controls.Add(Me.TabJournal)
        Me.TabControl1.Location = New System.Drawing.Point(5, 302)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(995, 311)
        Me.TabControl1.TabIndex = 10
        '
        'TabNotInSystem
        '
        Me.TabNotInSystem.Controls.Add(Me.NO_DIF_Label)
        Me.TabNotInSystem.Controls.Add(Me.dgvNotInSystem)
        Me.TabNotInSystem.Location = New System.Drawing.Point(4, 25)
        Me.TabNotInSystem.Name = "TabNotInSystem"
        Me.TabNotInSystem.Size = New System.Drawing.Size(987, 282)
        Me.TabNotInSystem.TabIndex = 0
        Me.TabNotInSystem.Text = "🔴 غير موجود في النظام"
        '
        'NO_DIF_Label
        '
        Me.NO_DIF_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NO_DIF_Label.Font = New System.Drawing.Font("Arial", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NO_DIF_Label.Location = New System.Drawing.Point(1, 1)
        Me.NO_DIF_Label.Name = "NO_DIF_Label"
        Me.NO_DIF_Label.Size = New System.Drawing.Size(986, 281)
        Me.NO_DIF_Label.TabIndex = 1
        Me.NO_DIF_Label.Text = "لا يوجد إختلاف بين الكشف الدفتري وكشف الأستاذ"
        Me.NO_DIF_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.NO_DIF_Label.Visible = False
        '
        'dgvNotInSystem
        '
        Me.dgvNotInSystem.AllowUserToAddRows = False
        Me.dgvNotInSystem.AllowUserToDeleteRows = False
        Me.dgvNotInSystem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvNotInSystem.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvNotInSystem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNotInSystem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvNotInSystem.Location = New System.Drawing.Point(0, 0)
        Me.dgvNotInSystem.Name = "dgvNotInSystem"
        Me.dgvNotInSystem.ReadOnly = True
        Me.dgvNotInSystem.Size = New System.Drawing.Size(987, 282)
        Me.dgvNotInSystem.TabIndex = 0
        '
        'TabNotInBank
        '
        Me.TabNotInBank.Controls.Add(Me.dgvNotInBank)
        Me.TabNotInBank.Location = New System.Drawing.Point(4, 25)
        Me.TabNotInBank.Name = "TabNotInBank"
        Me.TabNotInBank.Size = New System.Drawing.Size(987, 282)
        Me.TabNotInBank.TabIndex = 1
        Me.TabNotInBank.Text = "🟠 غير موجود في المصرف"
        '
        'dgvNotInBank
        '
        Me.dgvNotInBank.AllowUserToAddRows = False
        Me.dgvNotInBank.AllowUserToDeleteRows = False
        Me.dgvNotInBank.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvNotInBank.BackgroundColor = System.Drawing.SystemColors.ControlLight
        Me.dgvNotInBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNotInBank.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvNotInBank.Location = New System.Drawing.Point(0, 0)
        Me.dgvNotInBank.Name = "dgvNotInBank"
        Me.dgvNotInBank.ReadOnly = True
        Me.dgvNotInBank.Size = New System.Drawing.Size(987, 282)
        Me.dgvNotInBank.TabIndex = 0
        '
        'TabDiff
        '
        Me.TabDiff.Controls.Add(Me.dgvDiff)
        Me.TabDiff.Location = New System.Drawing.Point(4, 25)
        Me.TabDiff.Name = "TabDiff"
        Me.TabDiff.Size = New System.Drawing.Size(987, 282)
        Me.TabDiff.TabIndex = 2
        Me.TabDiff.Text = "🟡 اختلافات"
        '
        'dgvDiff
        '
        Me.dgvDiff.AllowUserToAddRows = False
        Me.dgvDiff.AllowUserToDeleteRows = False
        Me.dgvDiff.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDiff.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.dgvDiff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDiff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDiff.Location = New System.Drawing.Point(0, 0)
        Me.dgvDiff.Name = "dgvDiff"
        Me.dgvDiff.ReadOnly = True
        Me.dgvDiff.Size = New System.Drawing.Size(987, 282)
        Me.dgvDiff.TabIndex = 0
        '
        'TabJournal
        '
        Me.TabJournal.Controls.Add(Me.dgvJournal)
        Me.TabJournal.Location = New System.Drawing.Point(4, 25)
        Me.TabJournal.Name = "TabJournal"
        Me.TabJournal.Size = New System.Drawing.Size(987, 282)
        Me.TabJournal.TabIndex = 3
        Me.TabJournal.Text = "📘 مذكرة التسوية المحاسبية"
        '
        'dgvJournal
        '
        Me.dgvJournal.AllowUserToAddRows = False
        Me.dgvJournal.AllowUserToDeleteRows = False
        Me.dgvJournal.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.dgvJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvJournal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvJournal.Location = New System.Drawing.Point(0, 0)
        Me.dgvJournal.Name = "dgvJournal"
        Me.dgvJournal.ReadOnly = True
        Me.dgvJournal.Size = New System.Drawing.Size(987, 282)
        Me.dgvJournal.TabIndex = 0
        '
        'LabelMove
        '
        Me.LabelMove.AutoSize = True
        Me.LabelMove.Location = New System.Drawing.Point(164, 277)
        Me.LabelMove.Name = "LabelMove"
        Me.LabelMove.Size = New System.Drawing.Size(63, 16)
        Me.LabelMove.TabIndex = 5
        Me.LabelMove.Text = "رقم الحركة:"
        '
        'LabelDate
        '
        Me.LabelDate.AutoSize = True
        Me.LabelDate.Location = New System.Drawing.Point(364, 277)
        Me.LabelDate.Name = "LabelDate"
        Me.LabelDate.Size = New System.Drawing.Size(41, 16)
        Me.LabelDate.TabIndex = 6
        Me.LabelDate.Text = "التاريخ:"
        '
        'LabelValue
        '
        Me.LabelValue.AutoSize = True
        Me.LabelValue.Location = New System.Drawing.Point(554, 276)
        Me.LabelValue.Name = "LabelValue"
        Me.LabelValue.Size = New System.Drawing.Size(36, 16)
        Me.LabelValue.TabIndex = 7
        Me.LabelValue.Text = "القيمة:"
        '
        'PanelSummary
        '
        Me.PanelSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelSummary.Controls.Add(Me.lblTotalBank)
        Me.PanelSummary.Controls.Add(Me.lblTotalSystem)
        Me.PanelSummary.Controls.Add(Me.lblNotInSystem)
        Me.PanelSummary.Controls.Add(Me.lblNotInBank)
        Me.PanelSummary.Controls.Add(Me.lblDiff)
        Me.PanelSummary.Controls.Add(Me.btnExportExcel)
        Me.PanelSummary.Location = New System.Drawing.Point(4, 617)
        Me.PanelSummary.Name = "PanelSummary"
        Me.PanelSummary.Size = New System.Drawing.Size(635, 80)
        Me.PanelSummary.TabIndex = 11
        '
        'lblTotalBank
        '
        Me.lblTotalBank.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalBank.Location = New System.Drawing.Point(2, 5)
        Me.lblTotalBank.Name = "lblTotalBank"
        Me.lblTotalBank.Size = New System.Drawing.Size(205, 20)
        Me.lblTotalBank.TabIndex = 0
        '
        'lblTotalSystem
        '
        Me.lblTotalSystem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalSystem.Location = New System.Drawing.Point(2, 26)
        Me.lblTotalSystem.Name = "lblTotalSystem"
        Me.lblTotalSystem.Size = New System.Drawing.Size(205, 20)
        Me.lblTotalSystem.TabIndex = 1
        '
        'lblNotInSystem
        '
        Me.lblNotInSystem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotInSystem.Location = New System.Drawing.Point(208, 5)
        Me.lblNotInSystem.Name = "lblNotInSystem"
        Me.lblNotInSystem.Size = New System.Drawing.Size(205, 20)
        Me.lblNotInSystem.TabIndex = 2
        '
        'lblNotInBank
        '
        Me.lblNotInBank.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotInBank.Location = New System.Drawing.Point(208, 26)
        Me.lblNotInBank.Name = "lblNotInBank"
        Me.lblNotInBank.Size = New System.Drawing.Size(205, 20)
        Me.lblNotInBank.TabIndex = 3
        '
        'lblDiff
        '
        Me.lblDiff.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiff.Location = New System.Drawing.Point(2, 47)
        Me.lblDiff.Name = "lblDiff"
        Me.lblDiff.Size = New System.Drawing.Size(205, 20)
        Me.lblDiff.TabIndex = 4
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportExcel.Location = New System.Drawing.Point(490, 45)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(141, 30)
        Me.btnExportExcel.TabIndex = 5
        Me.btnExportExcel.Text = "📤 تصدير إلى Excel"
        Me.btnExportExcel.UseVisualStyleBackColor = True
        '
        'Settlement_Btn
        '
        Me.Settlement_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Settlement_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Settlement_Btn.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Settlement_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Settlement_Btn.Location = New System.Drawing.Point(656, 617)
        Me.Settlement_Btn.Name = "Settlement_Btn"
        Me.Settlement_Btn.Size = New System.Drawing.Size(340, 41)
        Me.Settlement_Btn.TabIndex = 12
        Me.Settlement_Btn.Text = "☑️ تنفيذ التسوية"
        Me.Settlement_Btn.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvSystem)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(499, 263)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "كشف الأستاذ"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvBank)
        Me.GroupBox2.Location = New System.Drawing.Point(504, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(504, 262)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "الكشف الدفتري"
        '
        'ALL_CB
        '
        Me.ALL_CB.AutoSize = True
        Me.ALL_CB.Location = New System.Drawing.Point(603, 277)
        Me.ALL_CB.Name = "ALL_CB"
        Me.ALL_CB.Size = New System.Drawing.Size(90, 20)
        Me.ALL_CB.TabIndex = 15
        Me.ALL_CB.Text = "تخصيص الكل"
        Me.ALL_CB.UseVisualStyleBackColor = True
        '
        'Settlement_Sheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.ClientSize = New System.Drawing.Size(1008, 699)
        Me.Controls.Add(Me.ALL_CB)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Settlement_Btn)
        Me.Controls.Add(Me.cmbMapMove)
        Me.Controls.Add(Me.cmbMapDate)
        Me.Controls.Add(Me.cmbMapValue)
        Me.Controls.Add(Me.LabelMove)
        Me.Controls.Add(Me.LabelDate)
        Me.Controls.Add(Me.LabelValue)
        Me.Controls.Add(Me.btnImportBank)
        Me.Controls.Add(Me.btnMatch)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PanelSummary)
        Me.Name = "Settlement_Sheet"
        Me.Text = "شاشة مطابقة كشف المصرف مع كشف الأستاذ"
        CType(Me.dgvSystem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvBank, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabNotInSystem.ResumeLayout(False)
        CType(Me.dgvNotInSystem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabNotInBank.ResumeLayout(False)
        CType(Me.dgvNotInBank, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabDiff.ResumeLayout(False)
        CType(Me.dgvDiff, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabJournal.ResumeLayout(False)
        CType(Me.dgvJournal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelSummary.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvSystem As DataGridView
    Friend WithEvents dgvBank As DataGridView
    Friend WithEvents cmbMapMove As ComboBox
    Friend WithEvents cmbMapDate As ComboBox
    Friend WithEvents cmbMapValue As ComboBox
    Friend WithEvents btnImportBank As Button
    Friend WithEvents btnMatch As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabNotInSystem As TabPage
    Friend WithEvents dgvNotInSystem As DataGridView
    Friend WithEvents TabNotInBank As TabPage
    Friend WithEvents dgvNotInBank As DataGridView
    Friend WithEvents TabDiff As TabPage
    Friend WithEvents dgvDiff As DataGridView
    Friend WithEvents LabelMove As Label
    Friend WithEvents LabelDate As Label
    Friend WithEvents LabelValue As Label
    Friend WithEvents PanelSummary As Panel
    Friend WithEvents lblTotalBank As Label
    Friend WithEvents lblTotalSystem As Label
    Friend WithEvents lblNotInSystem As Label
    Friend WithEvents lblNotInBank As Label
    Friend WithEvents lblDiff As Label
    Friend WithEvents btnExportExcel As Button

    ' تعريف الأدوات الجديدة
    Friend WithEvents TabJournal As TabPage
    Friend WithEvents dgvJournal As DataGridView
    Friend WithEvents Settlement_Btn As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ALL_CB As CheckBox
    Friend WithEvents NO_DIF_Label As Label
End Class


























'------------------------------------------------------------------------------------------------------------
'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
'Partial Class Form1
'    Inherits System.Windows.Forms.Form

'    ' Dispose
'    <System.Diagnostics.DebuggerNonUserCode()>
'    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
'        Try
'            If disposing AndAlso components IsNot Nothing Then
'                components.Dispose()
'            End If
'        Finally
'            MyBase.Dispose(disposing)
'        End Try
'    End Sub

'    Private components As System.ComponentModel.IContainer

'    ' Form Design
'    <System.Diagnostics.DebuggerStepThrough()>
'    Private Sub InitializeComponent()
'        Me.dgvMaster = New System.Windows.Forms.DataGridView()
'        Me.dgvImport = New System.Windows.Forms.DataGridView()
'        Me.dgvDiff = New System.Windows.Forms.DataGridView()
'        Me.btnImport = New System.Windows.Forms.Button()
'        Me.btnMatch = New System.Windows.Forms.Button()
'        Me.lblResult = New System.Windows.Forms.Label()
'        Me.Label1 = New System.Windows.Forms.Label()
'        Me.Label2 = New System.Windows.Forms.Label()
'        Me.cmbMapMove = New System.Windows.Forms.ComboBox()
'        Me.cmbMapDate = New System.Windows.Forms.ComboBox()
'        Me.cmbMapValue = New System.Windows.Forms.ComboBox()
'        Me.Label3 = New System.Windows.Forms.Label()
'        CType(Me.dgvMaster, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.dgvImport, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.dgvDiff, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.SuspendLayout()
'        '
'        'dgvMaster
'        '
'        Me.dgvMaster.AllowUserToAddRows = False
'        Me.dgvMaster.AllowUserToDeleteRows = False
'        Me.dgvMaster.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
'        Me.dgvMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
'        Me.dgvMaster.GridColor = System.Drawing.SystemColors.GradientActiveCaption
'        Me.dgvMaster.Location = New System.Drawing.Point(7, 25)
'        Me.dgvMaster.Name = "dgvMaster"
'        Me.dgvMaster.ReadOnly = True
'        Me.dgvMaster.Size = New System.Drawing.Size(489, 323)
'        Me.dgvMaster.TabIndex = 0
'        '
'        'dgvImport
'        '
'        Me.dgvImport.AllowUserToAddRows = False
'        Me.dgvImport.AllowUserToDeleteRows = False
'        Me.dgvImport.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption
'        Me.dgvImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
'        Me.dgvImport.Location = New System.Drawing.Point(514, 25)
'        Me.dgvImport.Name = "dgvImport"
'        Me.dgvImport.ReadOnly = True
'        Me.dgvImport.Size = New System.Drawing.Size(489, 323)
'        Me.dgvImport.TabIndex = 1
'        '
'        'dgvDiff
'        '
'        Me.dgvDiff.AllowUserToAddRows = False
'        Me.dgvDiff.AllowUserToDeleteRows = False
'        Me.dgvDiff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
'        Me.dgvDiff.Location = New System.Drawing.Point(7, 390)
'        Me.dgvDiff.Name = "dgvDiff"
'        Me.dgvDiff.ReadOnly = True
'        Me.dgvDiff.Size = New System.Drawing.Size(989, 304)
'        Me.dgvDiff.TabIndex = 2
'        '
'        'btnImport
'        '
'        Me.btnImport.Location = New System.Drawing.Point(431, 354)
'        Me.btnImport.Name = "btnImport"
'        Me.btnImport.Size = New System.Drawing.Size(90, 30)
'        Me.btnImport.TabIndex = 6
'        Me.btnImport.Text = "استيراد الملف"
'        Me.btnImport.UseVisualStyleBackColor = True
'        '
'        'btnMatch
'        '
'        Me.btnMatch.Location = New System.Drawing.Point(531, 354)
'        Me.btnMatch.Name = "btnMatch"
'        Me.btnMatch.Size = New System.Drawing.Size(90, 30)
'        Me.btnMatch.TabIndex = 7
'        Me.btnMatch.Text = "تنفيذ المطابقة"
'        Me.btnMatch.UseVisualStyleBackColor = True
'        '
'        'lblResult
'        '
'        Me.lblResult.AutoSize = True
'        Me.lblResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
'        Me.lblResult.Location = New System.Drawing.Point(12, 505)
'        Me.lblResult.Name = "lblResult"
'        Me.lblResult.Size = New System.Drawing.Size(0, 14)
'        Me.lblResult.TabIndex = 8
'        '
'        'Label1
'        '
'        Me.Label1.AutoSize = True
'        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
'        Me.Label1.Location = New System.Drawing.Point(863, 7)
'        Me.Label1.Name = "Label1"
'        Me.Label1.Size = New System.Drawing.Size(140, 14)
'        Me.Label1.TabIndex = 9
'        Me.Label1.Text = "حقول المطابقة بالترتيب"
'        '
'        'Label2
'        '
'        Me.Label2.AutoSize = True
'        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
'        Me.Label2.Location = New System.Drawing.Point(7, 375)
'        Me.Label2.Name = "Label2"
'        Me.Label2.Size = New System.Drawing.Size(134, 14)
'        Me.Label2.TabIndex = 10
'        Me.Label2.Text = "السجلات غير المطابقة"
'        '
'        'cmbMapMove
'        '
'        Me.cmbMapMove.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.cmbMapMove.FormattingEnabled = True
'        Me.cmbMapMove.Location = New System.Drawing.Point(514, 2)
'        Me.cmbMapMove.Name = "cmbMapMove"
'        Me.cmbMapMove.Size = New System.Drawing.Size(107, 21)
'        Me.cmbMapMove.TabIndex = 11
'        '
'        'cmbMapDate
'        '
'        Me.cmbMapDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.cmbMapDate.FormattingEnabled = True
'        Me.cmbMapDate.Location = New System.Drawing.Point(622, 2)
'        Me.cmbMapDate.Name = "cmbMapDate"
'        Me.cmbMapDate.Size = New System.Drawing.Size(114, 21)
'        Me.cmbMapDate.TabIndex = 12
'        '
'        'cmbMapValue
'        '
'        Me.cmbMapValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.cmbMapValue.FormattingEnabled = True
'        Me.cmbMapValue.Location = New System.Drawing.Point(737, 2)
'        Me.cmbMapValue.Name = "cmbMapValue"
'        Me.cmbMapValue.Size = New System.Drawing.Size(122, 21)
'        Me.cmbMapValue.TabIndex = 13
'        '
'        'Label3
'        '
'        Me.Label3.AutoSize = True
'        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
'        Me.Label3.Location = New System.Drawing.Point(11, 8)
'        Me.Label3.Name = "Label3"
'        Me.Label3.Size = New System.Drawing.Size(146, 14)
'        Me.Label3.TabIndex = 14
'        Me.Label3.Text = "كشف الأستاذ من النظام"
'        '
'        'Form1
'        '
'        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
'        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
'        Me.ClientSize = New System.Drawing.Size(1008, 699)
'        Me.Controls.Add(Me.Label3)
'        Me.Controls.Add(Me.cmbMapValue)
'        Me.Controls.Add(Me.cmbMapDate)
'        Me.Controls.Add(Me.cmbMapMove)
'        Me.Controls.Add(Me.Label2)
'        Me.Controls.Add(Me.Label1)
'        Me.Controls.Add(Me.lblResult)
'        Me.Controls.Add(Me.btnMatch)
'        Me.Controls.Add(Me.btnImport)
'        Me.Controls.Add(Me.dgvDiff)
'        Me.Controls.Add(Me.dgvImport)
'        Me.Controls.Add(Me.dgvMaster)
'        Me.Name = "Form1"
'        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
'        Me.Text = "شاشة مطابقة مذكرة التسوية"
'        CType(Me.dgvMaster, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.dgvImport, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.dgvDiff, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.ResumeLayout(False)
'        Me.PerformLayout()

'    End Sub

'    Friend WithEvents dgvMaster As DataGridView
'    Friend WithEvents dgvImport As DataGridView
'    Friend WithEvents dgvDiff As DataGridView
'    Friend WithEvents btnImport As Button
'    Friend WithEvents btnMatch As Button
'    Friend WithEvents lblResult As Label
'    Friend WithEvents Label1 As Label
'    Friend WithEvents Label2 As Label
'    Friend WithEvents cmbMapMove As ComboBox
'    Friend WithEvents cmbMapDate As ComboBox
'    Friend WithEvents cmbMapValue As ComboBox
'    Friend WithEvents Label3 As Label
'End Class
