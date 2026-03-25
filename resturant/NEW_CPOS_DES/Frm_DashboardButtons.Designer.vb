<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_DashboardButtons
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
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.Title_Label = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Main_Panel = New System.Windows.Forms.Panel()
        Me.Card_Panel = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtButtonTitle = New System.Windows.Forms.TextBox()
        Me.lblActionKey = New System.Windows.Forms.Label()
        Me.txtActionKey = New System.Windows.Forms.TextBox()
        Me.lblSymbol = New System.Windows.Forms.Label()
        Me.cmbSymbolChar = New System.Windows.Forms.ComboBox()
        Me.lblColor = New System.Windows.Forms.Label()
        Me.txtBackColor = New System.Windows.Forms.TextBox()
        Me.btnBackColor = New System.Windows.Forms.Button()
        Me.lblPermission = New System.Windows.Forms.Label()
        Me.txtReqPermission = New System.Windows.Forms.TextBox()
        Me.lblImage = New System.Windows.Forms.Label()
        Me.txtDefaultImage = New System.Windows.Forms.TextBox()
        Me.lblSortOrder = New System.Windows.Forms.Label()
        Me.numSortOrder = New System.Windows.Forms.NumericUpDown()
        Me.chkIsActive = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.DGV_Buttons = New System.Windows.Forms.DataGridView()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.TitleBar_Panel.SuspendLayout()
        Me.Main_Panel.SuspendLayout()
        Me.Card_Panel.SuspendLayout()
        CType(Me.numSortOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_Buttons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.Controls.Add(Me.Title_Label)
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(950, 40)
        Me.TitleBar_Panel.TabIndex = 0
        Me.TitleBar_Panel.Tag = "HEADER"
        '
        'Title_Label
        '
        Me.Title_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Title_Label.AutoSize = True
        Me.Title_Label.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Title_Label.Location = New System.Drawing.Point(700, 9)
        Me.Title_Label.Name = "Title_Label"
        Me.Title_Label.Size = New System.Drawing.Size(217, 21)
        Me.Title_Label.TabIndex = 1
        Me.Title_Label.Tag = "TITLE_TRANSPARENT"
        Me.Title_Label.Text = "تخصيص أزرار الداشبورد الرئيسية"
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.ExitFormButton.FlatAppearance.BorderSize = 0
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.Location = New System.Drawing.Point(0, 0)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(45, 40)
        Me.ExitFormButton.TabIndex = 0
        Me.ExitFormButton.Tag = "APP_CONTROL"
        Me.ExitFormButton.Text = "X"
        Me.ExitFormButton.UseVisualStyleBackColor = True
        '
        'Main_Panel
        '
        Me.Main_Panel.Controls.Add(Me.Card_Panel)
        Me.Main_Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Main_Panel.Location = New System.Drawing.Point(0, 40)
        Me.Main_Panel.Name = "Main_Panel"
        Me.Main_Panel.Padding = New System.Windows.Forms.Padding(15)
        Me.Main_Panel.Size = New System.Drawing.Size(950, 610)
        Me.Main_Panel.TabIndex = 1
        '
        'Card_Panel
        '
        Me.Card_Panel.Controls.Add(Me.lblTitle)
        Me.Card_Panel.Controls.Add(Me.txtButtonTitle)
        Me.Card_Panel.Controls.Add(Me.lblActionKey)
        Me.Card_Panel.Controls.Add(Me.txtActionKey)
        Me.Card_Panel.Controls.Add(Me.lblSymbol)
        Me.Card_Panel.Controls.Add(Me.cmbSymbolChar)
        Me.Card_Panel.Controls.Add(Me.lblColor)
        Me.Card_Panel.Controls.Add(Me.txtBackColor)
        Me.Card_Panel.Controls.Add(Me.btnBackColor)
        Me.Card_Panel.Controls.Add(Me.lblPermission)
        Me.Card_Panel.Controls.Add(Me.txtReqPermission)
        Me.Card_Panel.Controls.Add(Me.lblImage)
        Me.Card_Panel.Controls.Add(Me.txtDefaultImage)
        Me.Card_Panel.Controls.Add(Me.lblSortOrder)
        Me.Card_Panel.Controls.Add(Me.numSortOrder)
        Me.Card_Panel.Controls.Add(Me.chkIsActive)
        Me.Card_Panel.Controls.Add(Me.btnSave)
        Me.Card_Panel.Controls.Add(Me.DGV_Buttons)
        Me.Card_Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Card_Panel.Location = New System.Drawing.Point(15, 15)
        Me.Card_Panel.Name = "Card_Panel"
        Me.Card_Panel.Size = New System.Drawing.Size(920, 580)
        Me.Card_Panel.TabIndex = 0
        Me.Card_Panel.Tag = "CARD"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(820, 30)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(73, 20)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Tag = "TRANSPARENT"
        Me.lblTitle.Text = "عنوان الزر:"
        '
        'txtButtonTitle
        '
        Me.txtButtonTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.txtButtonTitle.Location = New System.Drawing.Point(580, 27)
        Me.txtButtonTitle.Name = "txtButtonTitle"
        Me.txtButtonTitle.Size = New System.Drawing.Size(230, 27)
        Me.txtButtonTitle.TabIndex = 2
        '
        'lblActionKey
        '
        Me.lblActionKey.AutoSize = True
        Me.lblActionKey.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblActionKey.Location = New System.Drawing.Point(820, 75)
        Me.lblActionKey.Name = "lblActionKey"
        Me.lblActionKey.Size = New System.Drawing.Size(78, 20)
        Me.lblActionKey.TabIndex = 3
        Me.lblActionKey.Tag = "TRANSPARENT"
        Me.lblActionKey.Text = "كود الإجراء:"
        '
        'txtActionKey
        '
        Me.txtActionKey.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.txtActionKey.Location = New System.Drawing.Point(580, 72)
        Me.txtActionKey.Name = "txtActionKey"
        Me.txtActionKey.ReadOnly = True
        Me.txtActionKey.Size = New System.Drawing.Size(230, 27)
        Me.txtActionKey.TabIndex = 4
        '
        'lblSymbol
        '
        Me.lblSymbol.AutoSize = True
        Me.lblSymbol.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblSymbol.Location = New System.Drawing.Point(450, 30)
        Me.lblSymbol.Name = "lblSymbol"
        Me.lblSymbol.Size = New System.Drawing.Size(42, 20)
        Me.lblSymbol.TabIndex = 5
        Me.lblSymbol.Tag = "TRANSPARENT"
        Me.lblSymbol.Text = "الرمز:"
        '
        'cmbSymbolChar
        '
        Me.cmbSymbolChar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSymbolChar.Font = New System.Drawing.Font("Segoe UI Emoji", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cmbSymbolChar.FormattingEnabled = True
        Me.cmbSymbolChar.Location = New System.Drawing.Point(280, 27)
        Me.cmbSymbolChar.Name = "cmbSymbolChar"
        Me.cmbSymbolChar.Size = New System.Drawing.Size(160, 29)
        Me.cmbSymbolChar.TabIndex = 6
        '
        'lblColor
        '
        Me.lblColor.AutoSize = True
        Me.lblColor.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblColor.Location = New System.Drawing.Point(450, 75)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(60, 20)
        Me.lblColor.TabIndex = 7
        Me.lblColor.Tag = "TRANSPARENT"
        Me.lblColor.Text = "لون الزر:"
        '
        'txtBackColor
        '
        Me.txtBackColor.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.txtBackColor.Location = New System.Drawing.Point(320, 72)
        Me.txtBackColor.Name = "txtBackColor"
        Me.txtBackColor.ReadOnly = True
        Me.txtBackColor.Size = New System.Drawing.Size(120, 27)
        Me.txtBackColor.TabIndex = 8
        '
        'btnBackColor
        '
        Me.btnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackColor.Location = New System.Drawing.Point(280, 71)
        Me.btnBackColor.Name = "btnBackColor"
        Me.btnBackColor.Size = New System.Drawing.Size(35, 29)
        Me.btnBackColor.TabIndex = 9
        Me.btnBackColor.Tag = "IGNORE"
        Me.btnBackColor.Text = "..."
        Me.btnBackColor.UseVisualStyleBackColor = True
        '
        'lblPermission
        '
        Me.lblPermission.AutoSize = True
        Me.lblPermission.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblPermission.Location = New System.Drawing.Point(820, 120)
        Me.lblPermission.Name = "lblPermission"
        Me.lblPermission.Size = New System.Drawing.Size(68, 20)
        Me.lblPermission.TabIndex = 10
        Me.lblPermission.Tag = "TRANSPARENT"
        Me.lblPermission.Text = "الصلاحية:"
        '
        'txtReqPermission
        '
        Me.txtReqPermission.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.txtReqPermission.Location = New System.Drawing.Point(580, 117)
        Me.txtReqPermission.Name = "txtReqPermission"
        Me.txtReqPermission.ReadOnly = True
        Me.txtReqPermission.Size = New System.Drawing.Size(230, 27)
        Me.txtReqPermission.TabIndex = 11
        '
        'lblImage
        '
        Me.lblImage.AutoSize = True
        Me.lblImage.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblImage.Location = New System.Drawing.Point(450, 120)
        Me.lblImage.Name = "lblImage"
        Me.lblImage.Size = New System.Drawing.Size(58, 20)
        Me.lblImage.TabIndex = 12
        Me.lblImage.Tag = "TRANSPARENT"
        Me.lblImage.Text = "الصورة:"
        '
        'txtDefaultImage
        '
        Me.txtDefaultImage.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.txtDefaultImage.Location = New System.Drawing.Point(280, 117)
        Me.txtDefaultImage.Name = "txtDefaultImage"
        Me.txtDefaultImage.Size = New System.Drawing.Size(160, 27)
        Me.txtDefaultImage.TabIndex = 13
        '
        'lblSortOrder
        '
        Me.lblSortOrder.AutoSize = True
        Me.lblSortOrder.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblSortOrder.Location = New System.Drawing.Point(820, 165)
        Me.lblSortOrder.Name = "lblSortOrder"
        Me.lblSortOrder.Size = New System.Drawing.Size(56, 20)
        Me.lblSortOrder.TabIndex = 14
        Me.lblSortOrder.Tag = "TRANSPARENT"
        Me.lblSortOrder.Text = "الترتيب:"
        '
        'numSortOrder
        '
        Me.numSortOrder.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.numSortOrder.Location = New System.Drawing.Point(680, 162)
        Me.numSortOrder.Name = "numSortOrder"
        Me.numSortOrder.Size = New System.Drawing.Size(130, 27)
        Me.numSortOrder.TabIndex = 15
        Me.numSortOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkIsActive
        '
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Checked = True
        Me.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsActive.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.chkIsActive.Location = New System.Drawing.Point(580, 165)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(63, 24)
        Me.chkIsActive.TabIndex = 16
        Me.chkIsActive.Tag = "TRANSPARENT"
        Me.chkIsActive.Text = "إظهار"
        Me.chkIsActive.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Location = New System.Drawing.Point(280, 160)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(160, 40)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Tag = "SAVE"
        Me.btnSave.Text = "حفظ التعديلات"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'DGV_Buttons
        '
        Me.DGV_Buttons.AllowUserToAddRows = False
        Me.DGV_Buttons.AllowUserToDeleteRows = False
        Me.DGV_Buttons.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_Buttons.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_Buttons.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.DGV_Buttons.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV_Buttons.DefaultCellStyle = DataGridViewCellStyle1
        Me.DGV_Buttons.Location = New System.Drawing.Point(20, 220)
        Me.DGV_Buttons.Name = "DGV_Buttons"
        Me.DGV_Buttons.ReadOnly = True
        Me.DGV_Buttons.RowHeadersVisible = False
        Me.DGV_Buttons.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.DGV_Buttons.RowTemplate.Height = 40
        Me.DGV_Buttons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_Buttons.Size = New System.Drawing.Size(880, 340)
        Me.DGV_Buttons.TabIndex = 19
        '
        'Frm_DashboardButtons
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(950, 650)
        Me.Controls.Add(Me.Main_Panel)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_DashboardButtons"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة أزرار الداشبورد"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.Main_Panel.ResumeLayout(False)
        Me.Card_Panel.ResumeLayout(False)
        Me.Card_Panel.PerformLayout()
        CType(Me.numSortOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_Buttons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents Title_Label As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Main_Panel As System.Windows.Forms.Panel
    Friend WithEvents Card_Panel As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents txtButtonTitle As System.Windows.Forms.TextBox
    Friend WithEvents lblActionKey As System.Windows.Forms.Label
    Friend WithEvents txtActionKey As System.Windows.Forms.TextBox
    Friend WithEvents lblSymbol As System.Windows.Forms.Label
    Friend WithEvents cmbSymbolChar As System.Windows.Forms.ComboBox
    Friend WithEvents lblColor As System.Windows.Forms.Label
    Friend WithEvents txtBackColor As System.Windows.Forms.TextBox
    Friend WithEvents btnBackColor As System.Windows.Forms.Button
    Friend WithEvents lblPermission As System.Windows.Forms.Label
    Friend WithEvents txtReqPermission As System.Windows.Forms.TextBox
    Friend WithEvents lblImage As System.Windows.Forms.Label
    Friend WithEvents txtDefaultImage As System.Windows.Forms.TextBox
    Friend WithEvents lblSortOrder As System.Windows.Forms.Label
    Friend WithEvents numSortOrder As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkIsActive As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents DGV_Buttons As System.Windows.Forms.DataGridView
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
End Class