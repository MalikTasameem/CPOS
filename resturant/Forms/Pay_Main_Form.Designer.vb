<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pay_Main_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.OK_Btn = New System.Windows.Forms.Button()
        Me.MONEY_VALUE_Txt = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Pay_Method1 = New resturant.Pay_Method()
        Me.PaymentsPanel = New System.Windows.Forms.Panel()
        Me.PaymentsGrid = New System.Windows.Forms.DataGridView()
        Me.PaymentMethodColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TreasuryColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AmountColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaymentAmountTxt = New System.Windows.Forms.TextBox()
        Me.AddPaymentBtn = New System.Windows.Forms.Button()
        Me.RemovePaymentBtn = New System.Windows.Forms.Button()
        Me.PaymentAmountLbl = New System.Windows.Forms.Label()
        Me.PaidTitleLbl = New System.Windows.Forms.Label()
        Me.PaidValueLbl = New System.Windows.Forms.Label()
        Me.RemainingTitleLbl = New System.Windows.Forms.Label()
        Me.RemainingValueLbl = New System.Windows.Forms.Label()
        Me.PaymentsPanel.SuspendLayout()
        CType(Me.PaymentsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(3, 613)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(4)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(1008, 66)
        Me.ExitFormButton.TabIndex = 673
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "رجـــوع Esc"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'OK_Btn
        '
        Me.OK_Btn.BackColor = System.Drawing.Color.CornflowerBlue
        Me.OK_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OK_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.OK_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.OK_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.OK_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.OK_Btn.Font = New System.Drawing.Font("Arial", 24.25!, System.Drawing.FontStyle.Bold)
        Me.OK_Btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.OK_Btn.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.OK_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OK_Btn.Location = New System.Drawing.Point(3, 519)
        Me.OK_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.OK_Btn.Name = "OK_Btn"
        Me.OK_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.OK_Btn.Size = New System.Drawing.Size(1008, 93)
        Me.OK_Btn.TabIndex = 674
        Me.OK_Btn.TabStop = False
        Me.OK_Btn.Text = "تأكيـــد F12"
        Me.OK_Btn.UseVisualStyleBackColor = False
        '
        'MONEY_VALUE_Txt
        '
        Me.MONEY_VALUE_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MONEY_VALUE_Txt.Font = New System.Drawing.Font("Times New Roman", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MONEY_VALUE_Txt.Location = New System.Drawing.Point(135, 3)
        Me.MONEY_VALUE_Txt.Name = "MONEY_VALUE_Txt"
        Me.MONEY_VALUE_Txt.ReadOnly = True
        Me.MONEY_VALUE_Txt.Size = New System.Drawing.Size(294, 44)
        Me.MONEY_VALUE_Txt.TabIndex = 675
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(433, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label11.Size = New System.Drawing.Size(80, 33)
        Me.Label11.TabIndex = 676
        Me.Label11.Text = "المبلغ"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pay_Method1
        '
        Me.Pay_Method1.BackColor = System.Drawing.Color.Transparent
        Me.Pay_Method1.Font = New System.Drawing.Font("Tahoma", 14.75!, System.Drawing.FontStyle.Bold)
        Me.Pay_Method1.Location = New System.Drawing.Point(3, 54)
        Me.Pay_Method1.Margin = New System.Windows.Forms.Padding(4)
        Me.Pay_Method1.Name = "Pay_Method1"
        Me.Pay_Method1.Size = New System.Drawing.Size(627, 457)
        Me.Pay_Method1.TabIndex = 463
        '
        'PaymentsPanel
        '
        Me.PaymentsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PaymentsPanel.Controls.Add(Me.PaymentsGrid)
        Me.PaymentsPanel.Controls.Add(Me.PaymentAmountTxt)
        Me.PaymentsPanel.Controls.Add(Me.AddPaymentBtn)
        Me.PaymentsPanel.Controls.Add(Me.RemovePaymentBtn)
        Me.PaymentsPanel.Controls.Add(Me.PaymentAmountLbl)
        Me.PaymentsPanel.Controls.Add(Me.PaidTitleLbl)
        Me.PaymentsPanel.Controls.Add(Me.PaidValueLbl)
        Me.PaymentsPanel.Controls.Add(Me.RemainingTitleLbl)
        Me.PaymentsPanel.Controls.Add(Me.RemainingValueLbl)
        Me.PaymentsPanel.Location = New System.Drawing.Point(638, 54)
        Me.PaymentsPanel.Name = "PaymentsPanel"
        Me.PaymentsPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PaymentsPanel.Size = New System.Drawing.Size(373, 457)
        Me.PaymentsPanel.TabIndex = 677
        '
        'PaymentsGrid
        '
        Me.PaymentsGrid.AllowUserToAddRows = False
        Me.PaymentsGrid.AllowUserToDeleteRows = False
        Me.PaymentsGrid.AllowUserToResizeRows = False
        Me.PaymentsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.PaymentsGrid.BackgroundColor = System.Drawing.Color.White
        Me.PaymentsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PaymentsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PaymentMethodColumn, Me.TreasuryColumn, Me.AmountColumn})
        Me.PaymentsGrid.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.PaymentsGrid.Location = New System.Drawing.Point(8, 92)
        Me.PaymentsGrid.MultiSelect = False
        Me.PaymentsGrid.Name = "PaymentsGrid"
        Me.PaymentsGrid.ReadOnly = True
        Me.PaymentsGrid.RowHeadersVisible = False
        Me.PaymentsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PaymentsGrid.Size = New System.Drawing.Size(355, 252)
        Me.PaymentsGrid.TabIndex = 0
        '
        'PaymentMethodColumn
        '
        Me.PaymentMethodColumn.HeaderText = "طريقة الدفع"
        Me.PaymentMethodColumn.Name = "PaymentMethodColumn"
        Me.PaymentMethodColumn.ReadOnly = True
        '
        'TreasuryColumn
        '
        Me.TreasuryColumn.HeaderText = "الخزينة"
        Me.TreasuryColumn.Name = "TreasuryColumn"
        Me.TreasuryColumn.ReadOnly = True
        '
        'AmountColumn
        '
        Me.AmountColumn.HeaderText = "المبلغ"
        Me.AmountColumn.Name = "AmountColumn"
        Me.AmountColumn.ReadOnly = True
        '
        'PaymentAmountTxt
        '
        Me.PaymentAmountTxt.Font = New System.Drawing.Font("Segoe UI Semibold", 15.0!)
        Me.PaymentAmountTxt.Location = New System.Drawing.Point(8, 39)
        Me.PaymentAmountTxt.Name = "PaymentAmountTxt"
        Me.PaymentAmountTxt.Size = New System.Drawing.Size(210, 34)
        Me.PaymentAmountTxt.TabIndex = 1
        '
        'AddPaymentBtn
        '
        Me.AddPaymentBtn.BackColor = System.Drawing.Color.ForestGreen
        Me.AddPaymentBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddPaymentBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!)
        Me.AddPaymentBtn.ForeColor = System.Drawing.Color.White
        Me.AddPaymentBtn.Location = New System.Drawing.Point(224, 37)
        Me.AddPaymentBtn.Name = "AddPaymentBtn"
        Me.AddPaymentBtn.Size = New System.Drawing.Size(139, 39)
        Me.AddPaymentBtn.TabIndex = 2
        Me.AddPaymentBtn.Text = "إضافة الدفعة"
        Me.AddPaymentBtn.UseVisualStyleBackColor = False
        '
        'RemovePaymentBtn
        '
        Me.RemovePaymentBtn.BackColor = System.Drawing.Color.IndianRed
        Me.RemovePaymentBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RemovePaymentBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.RemovePaymentBtn.ForeColor = System.Drawing.Color.White
        Me.RemovePaymentBtn.Location = New System.Drawing.Point(8, 350)
        Me.RemovePaymentBtn.Name = "RemovePaymentBtn"
        Me.RemovePaymentBtn.Size = New System.Drawing.Size(355, 36)
        Me.RemovePaymentBtn.TabIndex = 3
        Me.RemovePaymentBtn.Text = "حذف الدفعة المحددة"
        Me.RemovePaymentBtn.UseVisualStyleBackColor = False
        '
        'PaymentAmountLbl
        '
        Me.PaymentAmountLbl.AutoSize = True
        Me.PaymentAmountLbl.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.PaymentAmountLbl.Location = New System.Drawing.Point(8, 9)
        Me.PaymentAmountLbl.Name = "PaymentAmountLbl"
        Me.PaymentAmountLbl.Size = New System.Drawing.Size(78, 20)
        Me.PaymentAmountLbl.TabIndex = 4
        Me.PaymentAmountLbl.Text = "مبلغ الدفعة"
        '
        'PaidTitleLbl
        '
        Me.PaidTitleLbl.AutoSize = True
        Me.PaidTitleLbl.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.PaidTitleLbl.Location = New System.Drawing.Point(278, 399)
        Me.PaidTitleLbl.Name = "PaidTitleLbl"
        Me.PaidTitleLbl.Size = New System.Drawing.Size(85, 20)
        Me.PaidTitleLbl.TabIndex = 5
        Me.PaidTitleLbl.Text = "المدفوع:"
        '
        'PaidValueLbl
        '
        Me.PaidValueLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.PaidValueLbl.Location = New System.Drawing.Point(8, 396)
        Me.PaidValueLbl.Name = "PaidValueLbl"
        Me.PaidValueLbl.Size = New System.Drawing.Size(255, 25)
        Me.PaidValueLbl.TabIndex = 6
        Me.PaidValueLbl.Text = "0.000"
        Me.PaidValueLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RemainingTitleLbl
        '
        Me.RemainingTitleLbl.AutoSize = True
        Me.RemainingTitleLbl.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RemainingTitleLbl.Location = New System.Drawing.Point(278, 426)
        Me.RemainingTitleLbl.Name = "RemainingTitleLbl"
        Me.RemainingTitleLbl.Size = New System.Drawing.Size(85, 20)
        Me.RemainingTitleLbl.TabIndex = 7
        Me.RemainingTitleLbl.Text = "المتبقي:"
        '
        'RemainingValueLbl
        '
        Me.RemainingValueLbl.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.RemainingValueLbl.Location = New System.Drawing.Point(8, 423)
        Me.RemainingValueLbl.Name = "RemainingValueLbl"
        Me.RemainingValueLbl.Size = New System.Drawing.Size(255, 25)
        Me.RemainingValueLbl.TabIndex = 8
        Me.RemainingValueLbl.Text = "0.000"
        Me.RemainingValueLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Pay_Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1012, 680)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.MONEY_VALUE_Txt)
        Me.Controls.Add(Me.OK_Btn)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Pay_Method1)
        Me.Controls.Add(Me.PaymentsPanel)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Pay_Main_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تحديد طريقة الدفع"
        Me.PaymentsPanel.ResumeLayout(False)
        Me.PaymentsPanel.PerformLayout()
        CType(Me.PaymentsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Pay_Method1 As Pay_Method
    Friend WithEvents ExitFormButton As Button
    Friend WithEvents OK_Btn As Button
    Friend WithEvents MONEY_VALUE_Txt As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents PaymentsPanel As Panel
    Friend WithEvents PaymentsGrid As DataGridView
    Friend WithEvents PaymentMethodColumn As DataGridViewTextBoxColumn
    Friend WithEvents TreasuryColumn As DataGridViewTextBoxColumn
    Friend WithEvents AmountColumn As DataGridViewTextBoxColumn
    Friend WithEvents PaymentAmountTxt As TextBox
    Friend WithEvents AddPaymentBtn As Button
    Friend WithEvents RemovePaymentBtn As Button
    Friend WithEvents PaymentAmountLbl As Label
    Friend WithEvents PaidTitleLbl As Label
    Friend WithEvents PaidValueLbl As Label
    Friend WithEvents RemainingTitleLbl As Label
    Friend WithEvents RemainingValueLbl As Label
End Class
