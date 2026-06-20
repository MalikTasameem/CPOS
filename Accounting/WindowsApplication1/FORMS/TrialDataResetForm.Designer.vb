<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TrialDataResetForm
    Inherits Base_Form

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
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.DescriptionLabel = New System.Windows.Forms.Label()
        Me.CountsPanel = New System.Windows.Forms.Panel()
        Me.MasterCountLabel = New System.Windows.Forms.Label()
        Me.DetailCountLabel = New System.Windows.Forms.Label()
        Me.WarningLabel = New System.Windows.Forms.Label()
        Me.ConfirmLabel = New System.Windows.Forms.Label()
        Me.ConfirmTextBox = New System.Windows.Forms.TextBox()
        Me.ResetButton = New System.Windows.Forms.Button()
        Me.CancelButtonEx = New System.Windows.Forms.Button()
        Me.HeaderPanel.SuspendLayout()
        Me.CountsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.HeaderPanel.Controls.Add(Me.TitleLabel)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(680, 72)
        Me.HeaderPanel.TabIndex = 0
        '
        'TitleLabel
        '
        Me.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 15.0!, System.Drawing.FontStyle.Bold)
        Me.TitleLabel.ForeColor = System.Drawing.Color.White
        Me.TitleLabel.Location = New System.Drawing.Point(0, 0)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Padding = New System.Windows.Forms.Padding(16, 0, 16, 0)
        Me.TitleLabel.Size = New System.Drawing.Size(680, 72)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "تهيئة بيانات القيود التجريبية"
        Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DescriptionLabel
        '
        Me.DescriptionLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DescriptionLabel.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.DescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.DescriptionLabel.Location = New System.Drawing.Point(24, 92)
        Me.DescriptionLabel.Name = "DescriptionLabel"
        Me.DescriptionLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DescriptionLabel.Size = New System.Drawing.Size(632, 84)
        Me.DescriptionLabel.TabIndex = 1
        Me.DescriptionLabel.Text = "هذه الشاشة مخصصة عند اكتمال حد النسخة التجريبية. وظيفتها تفريغ قيود التجربة فقط " &
    "حتى يمكن استخدام مساحة جديدة للتجربة. لن يتم تفعيل النظام من هنا، ولن يتم حذف دليل الحسابات أو الإعدادات أو المستخدمين."
        Me.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'CountsPanel
        '
        Me.CountsPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CountsPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.CountsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CountsPanel.Controls.Add(Me.MasterCountLabel)
        Me.CountsPanel.Controls.Add(Me.DetailCountLabel)
        Me.CountsPanel.Location = New System.Drawing.Point(24, 190)
        Me.CountsPanel.Name = "CountsPanel"
        Me.CountsPanel.Size = New System.Drawing.Size(632, 74)
        Me.CountsPanel.TabIndex = 2
        '
        'MasterCountLabel
        '
        Me.MasterCountLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MasterCountLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MasterCountLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.MasterCountLabel.Location = New System.Drawing.Point(316, 14)
        Me.MasterCountLabel.Name = "MasterCountLabel"
        Me.MasterCountLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MasterCountLabel.Size = New System.Drawing.Size(296, 42)
        Me.MasterCountLabel.TabIndex = 0
        Me.MasterCountLabel.Text = "عدد القيود الرئيسية: 0"
        Me.MasterCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DetailCountLabel
        '
        Me.DetailCountLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DetailCountLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.DetailCountLabel.Location = New System.Drawing.Point(18, 14)
        Me.DetailCountLabel.Name = "DetailCountLabel"
        Me.DetailCountLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DetailCountLabel.Size = New System.Drawing.Size(292, 42)
        Me.DetailCountLabel.TabIndex = 1
        Me.DetailCountLabel.Text = "عدد تفاصيل القيود: 0"
        Me.DetailCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'WarningLabel
        '
        Me.WarningLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarningLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.WarningLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.WarningLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.WarningLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.WarningLabel.Location = New System.Drawing.Point(24, 282)
        Me.WarningLabel.Name = "WarningLabel"
        Me.WarningLabel.Padding = New System.Windows.Forms.Padding(10)
        Me.WarningLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.WarningLabel.Size = New System.Drawing.Size(632, 58)
        Me.WarningLabel.TabIndex = 3
        Me.WarningLabel.Text = "تنبيه: هذه العملية ستحذف بيانات ACC_BALANCE و ACC_BALANCE_MASTER فقط، ولا يمكن التراجع عنها من هذه الشاشة."
        Me.WarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ConfirmLabel
        '
        Me.ConfirmLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfirmLabel.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.ConfirmLabel.Location = New System.Drawing.Point(322, 362)
        Me.ConfirmLabel.Name = "ConfirmLabel"
        Me.ConfirmLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ConfirmLabel.Size = New System.Drawing.Size(334, 25)
        Me.ConfirmLabel.TabIndex = 4
        Me.ConfirmLabel.Text = "للتأكيد اكتب كلمة: مسح"
        Me.ConfirmLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ConfirmTextBox
        '
        Me.ConfirmTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfirmTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ConfirmTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ConfirmTextBox.Location = New System.Drawing.Point(24, 392)
        Me.ConfirmTextBox.Name = "ConfirmTextBox"
        Me.ConfirmTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ConfirmTextBox.Size = New System.Drawing.Size(632, 27)
        Me.ConfirmTextBox.TabIndex = 5
        Me.ConfirmTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ResetButton
        '
        Me.ResetButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResetButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ResetButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ResetButton.ForeColor = System.Drawing.Color.White
        Me.ResetButton.Location = New System.Drawing.Point(447, 451)
        Me.ResetButton.Name = "ResetButton"
        Me.ResetButton.Size = New System.Drawing.Size(209, 42)
        Me.ResetButton.TabIndex = 6
        Me.ResetButton.Text = "مسح قيود التجربة"
        Me.ResetButton.UseVisualStyleBackColor = False
        '
        'CancelButtonEx
        '
        Me.CancelButtonEx.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CancelButtonEx.BackColor = System.Drawing.Color.White
        Me.CancelButtonEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CancelButtonEx.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CancelButtonEx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.CancelButtonEx.Location = New System.Drawing.Point(24, 451)
        Me.CancelButtonEx.Name = "CancelButtonEx"
        Me.CancelButtonEx.Size = New System.Drawing.Size(135, 42)
        Me.CancelButtonEx.TabIndex = 7
        Me.CancelButtonEx.Text = "إغلاق"
        Me.CancelButtonEx.UseVisualStyleBackColor = False
        '
        'TrialDataResetForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(680, 518)
        Me.Controls.Add(Me.CancelButtonEx)
        Me.Controls.Add(Me.ResetButton)
        Me.Controls.Add(Me.ConfirmTextBox)
        Me.Controls.Add(Me.ConfirmLabel)
        Me.Controls.Add(Me.WarningLabel)
        Me.Controls.Add(Me.CountsPanel)
        Me.Controls.Add(Me.DescriptionLabel)
        Me.Controls.Add(Me.HeaderPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TrialDataResetForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "تهيئة بيانات القيود التجريبية"
        Me.HeaderPanel.ResumeLayout(False)
        Me.CountsPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents HeaderPanel As Panel
    Friend WithEvents TitleLabel As Label
    Friend WithEvents DescriptionLabel As Label
    Friend WithEvents CountsPanel As Panel
    Friend WithEvents MasterCountLabel As Label
    Friend WithEvents DetailCountLabel As Label
    Friend WithEvents WarningLabel As Label
    Friend WithEvents ConfirmLabel As Label
    Friend WithEvents ConfirmTextBox As TextBox
    Friend WithEvents ResetButton As Button
    Friend WithEvents CancelButtonEx As Button
End Class
