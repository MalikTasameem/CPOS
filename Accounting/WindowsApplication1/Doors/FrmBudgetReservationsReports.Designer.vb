<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBudgetReservationsReports
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
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.cmbFiscalYear = New System.Windows.Forms.ComboBox()
        Me.lblDoor = New System.Windows.Forms.Label()
        Me.cmbDoors = New System.Windows.Forms.ComboBox()
        Me.lblChapter = New System.Windows.Forms.Label()
        Me.cmbChapters = New System.Windows.Forms.ComboBox()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.cmbItems = New System.Windows.Forms.ComboBox()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.GroupBoxReportType = New System.Windows.Forms.GroupBox()
        Me.rbActive = New System.Windows.Forms.RadioButton()
        Me.rbPartial = New System.Windows.Forms.RadioButton()
        Me.rbCompleted = New System.Windows.Forms.RadioButton()
        Me.rbAll = New System.Windows.Forms.RadioButton()
        Me.SplitContainerMain = New System.Windows.Forms.SplitContainer()
        Me.dgvReserves = New System.Windows.Forms.DataGridView()
        Me.dgvTimeline = New System.Windows.Forms.DataGridView()
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.btnShowTimeline = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PanelHeader.SuspendLayout()
        Me.GroupBoxReportType.SuspendLayout()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        CType(Me.dgvReserves, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTimeline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFooter.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.Color.White
        Me.PanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelHeader.Controls.Add(Me.lblYear)
        Me.PanelHeader.Controls.Add(Me.cmbFiscalYear)
        Me.PanelHeader.Controls.Add(Me.lblDoor)
        Me.PanelHeader.Controls.Add(Me.cmbDoors)
        Me.PanelHeader.Controls.Add(Me.lblChapter)
        Me.PanelHeader.Controls.Add(Me.cmbChapters)
        Me.PanelHeader.Controls.Add(Me.lblItem)
        Me.PanelHeader.Controls.Add(Me.cmbItems)
        Me.PanelHeader.Controls.Add(Me.lblFrom)
        Me.PanelHeader.Controls.Add(Me.dtFrom)
        Me.PanelHeader.Controls.Add(Me.lblTo)
        Me.PanelHeader.Controls.Add(Me.dtTo)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Padding = New System.Windows.Forms.Padding(10)
        Me.PanelHeader.Size = New System.Drawing.Size(1180, 90)
        Me.PanelHeader.TabIndex = 3
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.Location = New System.Drawing.Point(1132, 14)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(42, 19)
        Me.lblYear.TabIndex = 0
        Me.lblYear.Text = "السنة"
        '
        'cmbFiscalYear
        '
        Me.cmbFiscalYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFiscalYear.Location = New System.Drawing.Point(1031, 11)
        Me.cmbFiscalYear.Name = "cmbFiscalYear"
        Me.cmbFiscalYear.Size = New System.Drawing.Size(97, 25)
        Me.cmbFiscalYear.TabIndex = 1
        '
        'lblDoor
        '
        Me.lblDoor.AutoSize = True
        Me.lblDoor.Location = New System.Drawing.Point(957, 13)
        Me.lblDoor.Name = "lblDoor"
        Me.lblDoor.Size = New System.Drawing.Size(40, 19)
        Me.lblDoor.TabIndex = 2
        Me.lblDoor.Text = "الباب"
        '
        'cmbDoors
        '
        Me.cmbDoors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoors.Location = New System.Drawing.Point(783, 10)
        Me.cmbDoors.Name = "cmbDoors"
        Me.cmbDoors.Size = New System.Drawing.Size(170, 25)
        Me.cmbDoors.TabIndex = 3
        '
        'lblChapter
        '
        Me.lblChapter.AutoSize = True
        Me.lblChapter.Location = New System.Drawing.Point(729, 13)
        Me.lblChapter.Name = "lblChapter"
        Me.lblChapter.Size = New System.Drawing.Size(48, 19)
        Me.lblChapter.TabIndex = 4
        Me.lblChapter.Text = "الفصل"
        '
        'cmbChapters
        '
        Me.cmbChapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChapters.Location = New System.Drawing.Point(537, 10)
        Me.cmbChapters.Name = "cmbChapters"
        Me.cmbChapters.Size = New System.Drawing.Size(188, 25)
        Me.cmbChapters.TabIndex = 5
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Location = New System.Drawing.Point(494, 14)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(35, 19)
        Me.lblItem.TabIndex = 6
        Me.lblItem.Text = "البند"
        '
        'cmbItems
        '
        Me.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItems.Location = New System.Drawing.Point(270, 11)
        Me.cmbItems.Name = "cmbItems"
        Me.cmbItems.Size = New System.Drawing.Size(220, 25)
        Me.cmbItems.TabIndex = 7
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(1131, 50)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(26, 19)
        Me.lblFrom.TabIndex = 8
        Me.lblFrom.Text = "من"
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(1008, 47)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(120, 25)
        Me.dtFrom.TabIndex = 9
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(962, 50)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(30, 19)
        Me.lblTo.TabIndex = 10
        Me.lblTo.Text = "إلى"
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(838, 47)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(120, 25)
        Me.dtTo.TabIndex = 11
        '
        'GroupBoxReportType
        '
        Me.GroupBoxReportType.BackColor = System.Drawing.Color.White
        Me.GroupBoxReportType.Controls.Add(Me.rbActive)
        Me.GroupBoxReportType.Controls.Add(Me.rbPartial)
        Me.GroupBoxReportType.Controls.Add(Me.rbCompleted)
        Me.GroupBoxReportType.Controls.Add(Me.rbAll)
        Me.GroupBoxReportType.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBoxReportType.Location = New System.Drawing.Point(0, 90)
        Me.GroupBoxReportType.Name = "GroupBoxReportType"
        Me.GroupBoxReportType.Size = New System.Drawing.Size(1180, 70)
        Me.GroupBoxReportType.TabIndex = 2
        Me.GroupBoxReportType.TabStop = False
        Me.GroupBoxReportType.Text = "نوع التقرير"
        '
        'rbActive
        '
        Me.rbActive.AutoSize = True
        Me.rbActive.Checked = True
        Me.rbActive.Location = New System.Drawing.Point(820, 30)
        Me.rbActive.Name = "rbActive"
        Me.rbActive.Size = New System.Drawing.Size(130, 23)
        Me.rbActive.TabIndex = 0
        Me.rbActive.TabStop = True
        Me.rbActive.Text = "الحجوزات النشطة"
        '
        'rbPartial
        '
        Me.rbPartial.AutoSize = True
        Me.rbPartial.Location = New System.Drawing.Point(650, 30)
        Me.rbPartial.Name = "rbPartial"
        Me.rbPartial.Size = New System.Drawing.Size(101, 23)
        Me.rbPartial.TabIndex = 1
        Me.rbPartial.Text = "مفكوكة جزئيًا"
        '
        'rbCompleted
        '
        Me.rbCompleted.AutoSize = True
        Me.rbCompleted.Location = New System.Drawing.Point(470, 30)
        Me.rbCompleted.Name = "rbCompleted"
        Me.rbCompleted.Size = New System.Drawing.Size(116, 23)
        Me.rbCompleted.TabIndex = 2
        Me.rbCompleted.Text = "مفكوكة بالكامل"
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Location = New System.Drawing.Point(340, 30)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(52, 23)
        Me.rbAll.TabIndex = 3
        Me.rbAll.Text = "الكل"
        '
        'SplitContainerMain
        '
        Me.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMain.Location = New System.Drawing.Point(0, 160)
        Me.SplitContainerMain.Name = "SplitContainerMain"
        Me.SplitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerMain.Panel1
        '
        Me.SplitContainerMain.Panel1.Controls.Add(Me.dgvReserves)
        Me.SplitContainerMain.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        '
        'SplitContainerMain.Panel2
        '
        Me.SplitContainerMain.Panel2.Controls.Add(Me.dgvTimeline)
        Me.SplitContainerMain.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SplitContainerMain.Size = New System.Drawing.Size(1180, 478)
        Me.SplitContainerMain.SplitterDistance = 339
        Me.SplitContainerMain.TabIndex = 0
        '
        'dgvReserves
        '
        Me.dgvReserves.AllowUserToAddRows = False
        Me.dgvReserves.AllowUserToDeleteRows = False
        Me.dgvReserves.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvReserves.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvReserves.Location = New System.Drawing.Point(0, 0)
        Me.dgvReserves.MultiSelect = False
        Me.dgvReserves.Name = "dgvReserves"
        Me.dgvReserves.ReadOnly = True
        Me.dgvReserves.RowHeadersVisible = False
        Me.dgvReserves.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvReserves.Size = New System.Drawing.Size(1180, 339)
        Me.dgvReserves.TabIndex = 0
        '
        'dgvTimeline
        '
        Me.dgvTimeline.AllowUserToAddRows = False
        Me.dgvTimeline.AllowUserToDeleteRows = False
        Me.dgvTimeline.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTimeline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTimeline.Location = New System.Drawing.Point(0, 0)
        Me.dgvTimeline.MultiSelect = False
        Me.dgvTimeline.Name = "dgvTimeline"
        Me.dgvTimeline.ReadOnly = True
        Me.dgvTimeline.RowHeadersVisible = False
        Me.dgvTimeline.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTimeline.Size = New System.Drawing.Size(1180, 135)
        Me.dgvTimeline.TabIndex = 0
        '
        'PanelFooter
        '
        Me.PanelFooter.BackColor = System.Drawing.Color.White
        Me.PanelFooter.Controls.Add(Me.btnShowTimeline)
        Me.PanelFooter.Controls.Add(Me.btnPrint)
        Me.PanelFooter.Controls.Add(Me.btnExport)
        Me.PanelFooter.Controls.Add(Me.btnClose)
        Me.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelFooter.Location = New System.Drawing.Point(0, 638)
        Me.PanelFooter.Name = "PanelFooter"
        Me.PanelFooter.Padding = New System.Windows.Forms.Padding(10)
        Me.PanelFooter.Size = New System.Drawing.Size(1180, 60)
        Me.PanelFooter.TabIndex = 1
        '
        'btnShowTimeline
        '
        Me.btnShowTimeline.Location = New System.Drawing.Point(760, 12)
        Me.btnShowTimeline.Name = "btnShowTimeline"
        Me.btnShowTimeline.Size = New System.Drawing.Size(140, 36)
        Me.btnShowTimeline.TabIndex = 0
        Me.btnShowTimeline.Text = "◷ عرض حركة الحجز"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(610, 12)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(140, 36)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "⎙ طباعة التقرير"
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(460, 12)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(140, 36)
        Me.btnExport.TabIndex = 2
        Me.btnExport.Text = "⇩ تصدير PDF"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(310, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 36)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "⟵ خروج"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 698)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1180, 22)
        Me.StatusStrip1.TabIndex = 4
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'FrmBudgetReservationsReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1180, 720)
        Me.Controls.Add(Me.SplitContainerMain)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.GroupBoxReportType)
        Me.Controls.Add(Me.PanelHeader)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FrmBudgetReservationsReports"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تقارير الحجوزات - Budget Control"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        Me.GroupBoxReportType.ResumeLayout(False)
        Me.GroupBoxReportType.PerformLayout()
        Me.SplitContainerMain.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.ResumeLayout(False)
        CType(Me.dgvReserves, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTimeline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFooter.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelHeader As Panel
    Friend WithEvents lblYear As Label
    Friend WithEvents cmbFiscalYear As ComboBox
    Friend WithEvents lblDoor As Label
    Friend WithEvents cmbDoors As ComboBox
    Friend WithEvents lblChapter As Label
    Friend WithEvents cmbChapters As ComboBox
    Friend WithEvents lblItem As Label
    Friend WithEvents cmbItems As ComboBox
    Friend WithEvents lblFrom As Label
    Friend WithEvents dtFrom As DateTimePicker
    Friend WithEvents lblTo As Label
    Friend WithEvents dtTo As DateTimePicker

    Friend WithEvents GroupBoxReportType As GroupBox
    Friend WithEvents rbActive As RadioButton
    Friend WithEvents rbPartial As RadioButton
    Friend WithEvents rbCompleted As RadioButton
    Friend WithEvents rbAll As RadioButton

    Friend WithEvents SplitContainerMain As SplitContainer
    Friend WithEvents dgvReserves As DataGridView
    Friend WithEvents dgvTimeline As DataGridView

    Friend WithEvents PanelFooter As Panel
    Friend WithEvents btnShowTimeline As Button
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnExport As Button
    Friend WithEvents btnClose As Button

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel

End Class
