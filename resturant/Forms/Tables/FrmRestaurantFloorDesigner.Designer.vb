<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRestaurantFloorDesigner
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
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnAutoArrange = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.cmbFlates = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlTools = New System.Windows.Forms.Panel()
        Me.FloorShapeComboBox = New System.Windows.Forms.ComboBox()
        Me.FloorShapeLabel = New System.Windows.Forms.Label()
        Me.FloorHeightNum = New System.Windows.Forms.NumericUpDown()
        Me.FloorHeightLabel = New System.Windows.Forms.Label()
        Me.FloorWidthNum = New System.Windows.Forms.NumericUpDown()
        Me.FloorWidthLabel = New System.Windows.Forms.Label()
        Me.btnDeleteSelected = New System.Windows.Forms.Button()
        Me.txtElementText = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.numHeight = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.numWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.numSeats = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TableShapeComboBox = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnCounter = New System.Windows.Forms.Button()
        Me.btnDoor = New System.Windows.Forms.Button()
        Me.btnWall = New System.Windows.Forms.Button()
        Me.lblSelected = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.FloorCanvas = New Global.resturant.RestaurantFloorDesignerControl()
        Me.pnlTop.SuspendLayout()
        Me.pnlTools.SuspendLayout()
        CType(Me.FloorHeightNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FloorWidthNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numSeats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.btnClose)
        Me.pnlTop.Controls.Add(Me.btnReset)
        Me.pnlTop.Controls.Add(Me.btnAutoArrange)
        Me.pnlTop.Controls.Add(Me.btnSave)
        Me.pnlTop.Controls.Add(Me.btnRefresh)
        Me.pnlTop.Controls.Add(Me.cmbFlates)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1184, 62)
        Me.pnlTop.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(12, 13)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(96, 36)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "خروج"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnReset
        '
        Me.btnReset.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.btnReset.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReset.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnReset.ForeColor = System.Drawing.Color.White
        Me.btnReset.Location = New System.Drawing.Point(114, 13)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(112, 36)
        Me.btnReset.TabIndex = 5
        Me.btnReset.Text = "إعادة افتراضي"
        Me.btnReset.UseVisualStyleBackColor = False
        '
        'btnAutoArrange
        '
        Me.btnAutoArrange.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.btnAutoArrange.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAutoArrange.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAutoArrange.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAutoArrange.ForeColor = System.Drawing.Color.White
        Me.btnAutoArrange.Location = New System.Drawing.Point(232, 13)
        Me.btnAutoArrange.Name = "btnAutoArrange"
        Me.btnAutoArrange.Size = New System.Drawing.Size(104, 36)
        Me.btnAutoArrange.TabIndex = 4
        Me.btnAutoArrange.Text = "ترتيب آلي"
        Me.btnAutoArrange.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(342, 13)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(104, 36)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "حفظ"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(452, 13)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(104, 36)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'cmbFlates
        '
        Me.cmbFlates.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFlates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFlates.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbFlates.FormattingEnabled = True
        Me.cmbFlates.Location = New System.Drawing.Point(796, 18)
        Me.cmbFlates.Name = "cmbFlates"
        Me.cmbFlates.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbFlates.Size = New System.Drawing.Size(256, 25)
        Me.cmbFlates.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1058, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "الدور"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlTools
        '
        Me.pnlTools.BackColor = System.Drawing.Color.White
        Me.pnlTools.Controls.Add(Me.FloorShapeComboBox)
        Me.pnlTools.Controls.Add(Me.FloorShapeLabel)
        Me.pnlTools.Controls.Add(Me.FloorHeightNum)
        Me.pnlTools.Controls.Add(Me.FloorHeightLabel)
        Me.pnlTools.Controls.Add(Me.FloorWidthNum)
        Me.pnlTools.Controls.Add(Me.FloorWidthLabel)
        Me.pnlTools.Controls.Add(Me.btnDeleteSelected)
        Me.pnlTools.Controls.Add(Me.txtElementText)
        Me.pnlTools.Controls.Add(Me.Label5)
        Me.pnlTools.Controls.Add(Me.numHeight)
        Me.pnlTools.Controls.Add(Me.Label4)
        Me.pnlTools.Controls.Add(Me.numWidth)
        Me.pnlTools.Controls.Add(Me.Label3)
        Me.pnlTools.Controls.Add(Me.TableShapeComboBox)
        Me.pnlTools.Controls.Add(Me.Label6)
        Me.pnlTools.Controls.Add(Me.numSeats)
        Me.pnlTools.Controls.Add(Me.Label2)
        Me.pnlTools.Controls.Add(Me.btnCounter)
        Me.pnlTools.Controls.Add(Me.btnDoor)
        Me.pnlTools.Controls.Add(Me.btnWall)
        Me.pnlTools.Controls.Add(Me.lblSelected)
        Me.pnlTools.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlTools.Location = New System.Drawing.Point(934, 62)
        Me.pnlTools.Name = "pnlTools"
        Me.pnlTools.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.pnlTools.Size = New System.Drawing.Size(250, 611)
        Me.pnlTools.TabIndex = 1
        '
        'FloorShapeComboBox
        '
        Me.FloorShapeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FloorShapeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FloorShapeComboBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FloorShapeComboBox.FormattingEnabled = True
        Me.FloorShapeComboBox.Items.AddRange(New Object() {"مستطيل", "مربع", "بيضاوي", "مخصص"})
        Me.FloorShapeComboBox.Location = New System.Drawing.Point(18, 86)
        Me.FloorShapeComboBox.Name = "FloorShapeComboBox"
        Me.FloorShapeComboBox.Size = New System.Drawing.Size(104, 25)
        Me.FloorShapeComboBox.TabIndex = 20
        '
        'FloorShapeLabel
        '
        Me.FloorShapeLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FloorShapeLabel.Location = New System.Drawing.Point(128, 86)
        Me.FloorShapeLabel.Name = "FloorShapeLabel"
        Me.FloorShapeLabel.Size = New System.Drawing.Size(104, 25)
        Me.FloorShapeLabel.TabIndex = 19
        Me.FloorShapeLabel.Text = "شكل الدور"
        Me.FloorShapeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FloorHeightNum
        '
        Me.FloorHeightNum.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FloorHeightNum.Location = New System.Drawing.Point(18, 154)
        Me.FloorHeightNum.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.FloorHeightNum.Minimum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.FloorHeightNum.Name = "FloorHeightNum"
        Me.FloorHeightNum.Size = New System.Drawing.Size(104, 25)
        Me.FloorHeightNum.TabIndex = 18
        Me.FloorHeightNum.Value = New Decimal(New Integer() {520, 0, 0, 0})
        '
        'FloorHeightLabel
        '
        Me.FloorHeightLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FloorHeightLabel.Location = New System.Drawing.Point(128, 154)
        Me.FloorHeightLabel.Name = "FloorHeightLabel"
        Me.FloorHeightLabel.Size = New System.Drawing.Size(104, 25)
        Me.FloorHeightLabel.TabIndex = 17
        Me.FloorHeightLabel.Text = "ارتفاع الدور"
        Me.FloorHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FloorWidthNum
        '
        Me.FloorWidthNum.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FloorWidthNum.Location = New System.Drawing.Point(18, 120)
        Me.FloorWidthNum.Maximum = New Decimal(New Integer() {3000, 0, 0, 0})
        Me.FloorWidthNum.Minimum = New Decimal(New Integer() {160, 0, 0, 0})
        Me.FloorWidthNum.Name = "FloorWidthNum"
        Me.FloorWidthNum.Size = New System.Drawing.Size(104, 25)
        Me.FloorWidthNum.TabIndex = 16
        Me.FloorWidthNum.Value = New Decimal(New Integer() {760, 0, 0, 0})
        '
        'FloorWidthLabel
        '
        Me.FloorWidthLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FloorWidthLabel.Location = New System.Drawing.Point(128, 120)
        Me.FloorWidthLabel.Name = "FloorWidthLabel"
        Me.FloorWidthLabel.Size = New System.Drawing.Size(104, 25)
        Me.FloorWidthLabel.TabIndex = 15
        Me.FloorWidthLabel.Text = "عرض الدور"
        Me.FloorWidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnDeleteSelected
        '
        Me.btnDeleteSelected.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnDeleteSelected.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDeleteSelected.Enabled = False
        Me.btnDeleteSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteSelected.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDeleteSelected.ForeColor = System.Drawing.Color.White
        Me.btnDeleteSelected.Location = New System.Drawing.Point(18, 457)
        Me.btnDeleteSelected.Name = "btnDeleteSelected"
        Me.btnDeleteSelected.Size = New System.Drawing.Size(214, 38)
        Me.btnDeleteSelected.TabIndex = 12
        Me.btnDeleteSelected.Text = "حذف العنصر المحدد"
        Me.btnDeleteSelected.UseVisualStyleBackColor = False
        '
        'txtElementText
        '
        Me.txtElementText.Enabled = False
        Me.txtElementText.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtElementText.Location = New System.Drawing.Point(18, 412)
        Me.txtElementText.Name = "txtElementText"
        Me.txtElementText.Size = New System.Drawing.Size(214, 25)
        Me.txtElementText.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(18, 386)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(214, 23)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "نص عنصر الديكور"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'numHeight
        '
        Me.numHeight.Enabled = False
        Me.numHeight.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.numHeight.Location = New System.Drawing.Point(18, 350)
        Me.numHeight.Maximum = New Decimal(New Integer() {800, 0, 0, 0})
        Me.numHeight.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.numHeight.Name = "numHeight"
        Me.numHeight.Size = New System.Drawing.Size(104, 25)
        Me.numHeight.TabIndex = 9
        Me.numHeight.Value = New Decimal(New Integer() {80, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(128, 350)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 25)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "الارتفاع"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'numWidth
        '
        Me.numWidth.Enabled = False
        Me.numWidth.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.numWidth.Location = New System.Drawing.Point(18, 314)
        Me.numWidth.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.numWidth.Minimum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.numWidth.Name = "numWidth"
        Me.numWidth.Size = New System.Drawing.Size(104, 25)
        Me.numWidth.TabIndex = 7
        Me.numWidth.Value = New Decimal(New Integer() {110, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(128, 314)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 25)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "العرض"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'numSeats
        '
        Me.numSeats.Enabled = False
        Me.numSeats.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.numSeats.Location = New System.Drawing.Point(18, 278)
        Me.numSeats.Maximum = New Decimal(New Integer() {16, 0, 0, 0})
        Me.numSeats.Name = "numSeats"
        Me.numSeats.Size = New System.Drawing.Size(104, 25)
        Me.numSeats.TabIndex = 5
        Me.numSeats.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(128, 278)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 25)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "عدد الكراسي"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TableShapeComboBox
        '
        Me.TableShapeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TableShapeComboBox.Enabled = False
        Me.TableShapeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TableShapeComboBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.TableShapeComboBox.FormattingEnabled = True
        Me.TableShapeComboBox.Items.AddRange(New Object() {"عادية", "مستطيلة", "دائرية", "مربعة"})
        Me.TableShapeComboBox.Location = New System.Drawing.Point(18, 242)
        Me.TableShapeComboBox.Name = "TableShapeComboBox"
        Me.TableShapeComboBox.Size = New System.Drawing.Size(104, 25)
        Me.TableShapeComboBox.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(128, 242)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 25)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "شكل الطاولة"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCounter
        '
        Me.btnCounter.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(191, Byte), Integer))
        Me.btnCounter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCounter.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCounter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.btnCounter.Location = New System.Drawing.Point(18, 188)
        Me.btnCounter.Name = "btnCounter"
        Me.btnCounter.Size = New System.Drawing.Size(66, 36)
        Me.btnCounter.TabIndex = 3
        Me.btnCounter.Text = "كاونتر"
        Me.btnCounter.UseVisualStyleBackColor = False
        '
        'btnDoor
        '
        Me.btnDoor.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.btnDoor.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDoor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDoor.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDoor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.btnDoor.Location = New System.Drawing.Point(92, 188)
        Me.btnDoor.Name = "btnDoor"
        Me.btnDoor.Size = New System.Drawing.Size(66, 36)
        Me.btnDoor.TabIndex = 2
        Me.btnDoor.Text = "باب"
        Me.btnDoor.UseVisualStyleBackColor = False
        '
        'btnWall
        '
        Me.btnWall.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnWall.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnWall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnWall.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnWall.ForeColor = System.Drawing.Color.White
        Me.btnWall.Location = New System.Drawing.Point(166, 188)
        Me.btnWall.Name = "btnWall"
        Me.btnWall.Size = New System.Drawing.Size(66, 36)
        Me.btnWall.TabIndex = 1
        Me.btnWall.Text = "جدار"
        Me.btnWall.UseVisualStyleBackColor = False
        '
        'lblSelected
        '
        Me.lblSelected.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSelected.Location = New System.Drawing.Point(18, 18)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Size = New System.Drawing.Size(214, 62)
        Me.lblSelected.TabIndex = 0
        Me.lblSelected.Text = "العنصر المحدد: لا يوجد"
        Me.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.White
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblStatus.Location = New System.Drawing.Point(0, 647)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(934, 26)
        Me.lblStatus.TabIndex = 3
        Me.lblStatus.Text = "جاهز"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FloorCanvas
        '
        Me.FloorCanvas.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.FloorCanvas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FloorCanvas.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.FloorCanvas.IsDesignMode = True
        Me.FloorCanvas.Location = New System.Drawing.Point(0, 62)
        Me.FloorCanvas.Name = "FloorCanvas"
        Me.FloorCanvas.Size = New System.Drawing.Size(934, 585)
        Me.FloorCanvas.TabIndex = 2
        '
        'FrmRestaurantFloorDesigner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 673)
        Me.Controls.Add(Me.FloorCanvas)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.pnlTools)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(1040, 650)
        Me.Name = "FrmRestaurantFloorDesigner"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "مصمم مخطط الطاولات"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTools.ResumeLayout(False)
        Me.pnlTools.PerformLayout()
        CType(Me.FloorHeightNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FloorWidthNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numSeats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTop As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents btnReset As Button
    Friend WithEvents btnAutoArrange As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents cmbFlates As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents pnlTools As Panel
    Friend WithEvents FloorShapeComboBox As ComboBox
    Friend WithEvents FloorShapeLabel As Label
    Friend WithEvents FloorHeightNum As NumericUpDown
    Friend WithEvents FloorHeightLabel As Label
    Friend WithEvents FloorWidthNum As NumericUpDown
    Friend WithEvents FloorWidthLabel As Label
    Friend WithEvents FloorCanvas As RestaurantFloorDesignerControl
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblSelected As Label
    Friend WithEvents btnCounter As Button
    Friend WithEvents btnDoor As Button
    Friend WithEvents btnWall As Button
    Friend WithEvents numSeats As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents TableShapeComboBox As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents numHeight As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents numWidth As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents txtElementText As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btnDeleteSelected As Button
End Class
