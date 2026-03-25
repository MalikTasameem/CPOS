<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Pay_Method
    Inherits System.Windows.Forms.UserControl

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
    Friend WithEvents MainLayout As TableLayoutPanel
    Friend WithEvents lblPayment As Label
    Friend WithEvents pnlPayments As FlowLayoutPanel
    Friend WithEvents lblTreasury As Label
    Friend WithEvents Treasury_ComboBox As ComboBox

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MainLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.lblPayment = New System.Windows.Forms.Label()
        Me.pnlPayments = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblTreasury = New System.Windows.Forms.Label()
        Me.Treasury_ComboBox = New System.Windows.Forms.ComboBox()
        Me.MainLayout.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainLayout
        '
        Me.MainLayout.ColumnCount = 1
        Me.MainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.MainLayout.Controls.Add(Me.lblPayment, 0, 0)
        Me.MainLayout.Controls.Add(Me.pnlPayments, 0, 1)
        Me.MainLayout.Controls.Add(Me.lblTreasury, 0, 2)
        Me.MainLayout.Controls.Add(Me.Treasury_ComboBox, 0, 3)
        Me.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainLayout.Location = New System.Drawing.Point(0, 0)
        Me.MainLayout.Name = "MainLayout"
        Me.MainLayout.RowCount = 4
        Me.MainLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.MainLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.MainLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.MainLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.MainLayout.Size = New System.Drawing.Size(500, 170)
        Me.MainLayout.TabIndex = 0
        '
        'lblPayment
        '
        Me.lblPayment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPayment.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayment.Location = New System.Drawing.Point(3, 0)
        Me.lblPayment.Name = "lblPayment"
        Me.lblPayment.Size = New System.Drawing.Size(494, 28)
        Me.lblPayment.TabIndex = 0
        Me.lblPayment.Text = "طريقة الدفع"
        Me.lblPayment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlPayments
        '
        Me.pnlPayments.AutoScroll = True
        Me.pnlPayments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPayments.Location = New System.Drawing.Point(3, 31)
        Me.pnlPayments.Name = "pnlPayments"
        Me.pnlPayments.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlPayments.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.pnlPayments.Size = New System.Drawing.Size(494, 64)
        Me.pnlPayments.TabIndex = 1
        '
        'lblTreasury
        '
        Me.lblTreasury.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTreasury.Font = New System.Drawing.Font("Segoe UI Semibold", 10.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTreasury.Location = New System.Drawing.Point(3, 98)
        Me.lblTreasury.Name = "lblTreasury"
        Me.lblTreasury.Size = New System.Drawing.Size(494, 28)
        Me.lblTreasury.TabIndex = 2
        Me.lblTreasury.Text = "الخزينة"
        Me.lblTreasury.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Treasury_ComboBox
        '
        Me.Treasury_ComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Treasury_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Treasury_ComboBox.Enabled = False
        Me.Treasury_ComboBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Treasury_ComboBox.FormattingEnabled = True
        Me.Treasury_ComboBox.Location = New System.Drawing.Point(3, 129)
        Me.Treasury_ComboBox.Name = "Treasury_ComboBox"
        Me.Treasury_ComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Treasury_ComboBox.Size = New System.Drawing.Size(494, 23)
        Me.Treasury_ComboBox.TabIndex = 3
        '
        'Pay_Method
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainLayout)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "Pay_Method"
        Me.Size = New System.Drawing.Size(500, 170)
        Me.MainLayout.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

End Class






















'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
'Partial Class Pay_Method
'    Inherits System.Windows.Forms.UserControl

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

'    <System.Diagnostics.DebuggerStepThrough()>
'    Private Sub InitializeComponent()
'        Me.TableLP1 = New System.Windows.Forms.TableLayoutPanel()
'        Me.pnlPayType = New System.Windows.Forms.Panel()
'        Me.lblPayTypeText = New System.Windows.Forms.Label()
'        Me.lblPayTypeArrow = New System.Windows.Forms.Label()
'        Me.Treasury_ComboBox = New System.Windows.Forms.ComboBox()
'        Me.pnlPayTypeDrop = New System.Windows.Forms.Panel()
'        Me.lbPayTypes = New System.Windows.Forms.ListBox()
'        Me.TableLP1.SuspendLayout()
'        Me.pnlPayType.SuspendLayout()
'        Me.pnlPayTypeDrop.SuspendLayout()
'        Me.SuspendLayout()
'        '
'        'TableLP1
'        '
'        Me.TableLP1.ColumnCount = 2
'        Me.TableLP1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.0!))
'        Me.TableLP1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.0!))
'        Me.TableLP1.Controls.Add(Me.pnlPayType, 0, 0)
'        Me.TableLP1.Controls.Add(Me.Treasury_ComboBox, 1, 0)
'        Me.TableLP1.Controls.Add(Me.pnlPayTypeDrop, 0, 1)
'        Me.TableLP1.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.TableLP1.Location = New System.Drawing.Point(0, 0)
'        Me.TableLP1.Name = "TableLP1"
'        Me.TableLP1.Padding = New System.Windows.Forms.Padding(6, 5, 6, 5)
'        Me.TableLP1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.TableLP1.RowCount = 2
'        Me.TableLP1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
'        Me.TableLP1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0!))
'        Me.TableLP1.Size = New System.Drawing.Size(266, 231)
'        Me.TableLP1.TabIndex = 0
'        '
'        'pnlPayType
'        '
'        Me.pnlPayType.BackColor = System.Drawing.Color.White
'        Me.pnlPayType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.pnlPayType.Controls.Add(Me.lblPayTypeText)
'        Me.pnlPayType.Controls.Add(Me.lblPayTypeArrow)
'        Me.pnlPayType.Cursor = System.Windows.Forms.Cursors.Hand
'        Me.pnlPayType.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.pnlPayType.Location = New System.Drawing.Point(170, 8)
'        Me.pnlPayType.Margin = New System.Windows.Forms.Padding(3, 3, 6, 3)
'        Me.pnlPayType.Name = "pnlPayType"
'        Me.pnlPayType.Padding = New System.Windows.Forms.Padding(8, 3, 8, 3)
'        Me.pnlPayType.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.pnlPayType.Size = New System.Drawing.Size(87, 29)
'        Me.pnlPayType.TabIndex = 1
'        '
'        'lblPayTypeText
'        '
'        Me.lblPayTypeText.Cursor = System.Windows.Forms.Cursors.Hand
'        Me.lblPayTypeText.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.lblPayTypeText.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
'        Me.lblPayTypeText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
'        Me.lblPayTypeText.Location = New System.Drawing.Point(26, 3)
'        Me.lblPayTypeText.Name = "lblPayTypeText"
'        Me.lblPayTypeText.Size = New System.Drawing.Size(51, 21)
'        Me.lblPayTypeText.TabIndex = 1
'        Me.lblPayTypeText.Text = "طريقة الدفع"
'        Me.lblPayTypeText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
'        '
'        'lblPayTypeArrow
'        '
'        Me.lblPayTypeArrow.Cursor = System.Windows.Forms.Cursors.Hand
'        Me.lblPayTypeArrow.Dock = System.Windows.Forms.DockStyle.Left
'        Me.lblPayTypeArrow.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
'        Me.lblPayTypeArrow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
'        Me.lblPayTypeArrow.Location = New System.Drawing.Point(8, 3)
'        Me.lblPayTypeArrow.Name = "lblPayTypeArrow"
'        Me.lblPayTypeArrow.Size = New System.Drawing.Size(18, 21)
'        Me.lblPayTypeArrow.TabIndex = 0
'        Me.lblPayTypeArrow.Text = "▾"
'        Me.lblPayTypeArrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'        '
'        'Treasury_ComboBox
'        '
'        Me.Treasury_ComboBox.BackColor = System.Drawing.Color.White
'        Me.Treasury_ComboBox.Cursor = System.Windows.Forms.Cursors.Hand
'        Me.Treasury_ComboBox.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.Treasury_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.Treasury_ComboBox.Enabled = False
'        Me.Treasury_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.Treasury_ComboBox.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
'        Me.Treasury_ComboBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
'        Me.Treasury_ComboBox.Location = New System.Drawing.Point(9, 8)
'        Me.Treasury_ComboBox.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
'        Me.Treasury_ComboBox.Name = "Treasury_ComboBox"
'        Me.Treasury_ComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.Treasury_ComboBox.Size = New System.Drawing.Size(149, 27)
'        Me.Treasury_ComboBox.TabIndex = 2
'        '
'        'pnlPayTypeDrop
'        '
'        Me.pnlPayTypeDrop.BackColor = System.Drawing.Color.White
'        Me.pnlPayTypeDrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.TableLP1.SetColumnSpan(Me.pnlPayTypeDrop, 2)
'        Me.pnlPayTypeDrop.Controls.Add(Me.lbPayTypes)
'        Me.pnlPayTypeDrop.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.pnlPayTypeDrop.Location = New System.Drawing.Point(6, 43)
'        Me.pnlPayTypeDrop.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
'        Me.pnlPayTypeDrop.Name = "pnlPayTypeDrop"
'        Me.pnlPayTypeDrop.Padding = New System.Windows.Forms.Padding(6)
'        Me.pnlPayTypeDrop.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.pnlPayTypeDrop.Size = New System.Drawing.Size(254, 183)
'        Me.pnlPayTypeDrop.TabIndex = 3
'        Me.pnlPayTypeDrop.Visible = False
'        '
'        'lbPayTypes
'        '
'        Me.lbPayTypes.BorderStyle = System.Windows.Forms.BorderStyle.None
'        Me.lbPayTypes.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.lbPayTypes.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
'        Me.lbPayTypes.FormattingEnabled = True
'        Me.lbPayTypes.IntegralHeight = False
'        Me.lbPayTypes.ItemHeight = 19
'        Me.lbPayTypes.Location = New System.Drawing.Point(6, 6)
'        Me.lbPayTypes.Name = "lbPayTypes"
'        Me.lbPayTypes.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.lbPayTypes.Size = New System.Drawing.Size(240, 169)
'        Me.lbPayTypes.TabIndex = 0
'        '
'        'Pay_Method
'        '
'        Me.BackColor = System.Drawing.Color.White
'        Me.Controls.Add(Me.TableLP1)
'        Me.Name = "Pay_Method"
'        Me.Size = New System.Drawing.Size(266, 231)
'        Me.TableLP1.ResumeLayout(False)
'        Me.pnlPayType.ResumeLayout(False)
'        Me.pnlPayTypeDrop.ResumeLayout(False)
'        Me.ResumeLayout(False)

'    End Sub

'    Friend WithEvents TableLP1 As TableLayoutPanel
'    Friend WithEvents pnlPayType As Panel
'    Friend WithEvents lblPayTypeText As Label
'    Friend WithEvents lblPayTypeArrow As Label
'    Friend WithEvents Treasury_ComboBox As ComboBox
'    Friend WithEvents pnlPayTypeDrop As Panel
'    Friend WithEvents lbPayTypes As ListBox
'End Class