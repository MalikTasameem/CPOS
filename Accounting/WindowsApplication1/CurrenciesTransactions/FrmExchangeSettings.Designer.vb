<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmExchangeSettings
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PanelTop = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.grpSettings = New System.Windows.Forms.GroupBox()
        Me.txtAccount = New System.Windows.Forms.TextBox()
        Me.lblPercent = New System.Windows.Forms.Label()
        Me.numPercent = New System.Windows.Forms.NumericUpDown()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.cmbAccount = New System.Windows.Forms.ComboBox()
        Me.lblUpdatedInfo = New System.Windows.Forms.Label()
        Me.PanelBottom = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.PanelTop.SuspendLayout()
        Me.grpSettings.SuspendLayout()
        CType(Me.numPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.White
        Me.PanelTop.Controls.Add(Me.lblTitle)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(734, 60)
        Me.PanelTop.TabIndex = 2
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(204, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "الإعدادات العامة للصرافة"
        '
        'grpSettings
        '
        Me.grpSettings.Controls.Add(Me.txtAccount)
        Me.grpSettings.Controls.Add(Me.lblPercent)
        Me.grpSettings.Controls.Add(Me.numPercent)
        Me.grpSettings.Controls.Add(Me.lblAccount)
        Me.grpSettings.Controls.Add(Me.cmbAccount)
        Me.grpSettings.Controls.Add(Me.lblUpdatedInfo)
        Me.grpSettings.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grpSettings.Location = New System.Drawing.Point(20, 80)
        Me.grpSettings.Name = "grpSettings"
        Me.grpSettings.Size = New System.Drawing.Size(690, 150)
        Me.grpSettings.TabIndex = 0
        Me.grpSettings.TabStop = False
        Me.grpSettings.Text = "إعدادات العمولة"
        '
        'txtAccount
        '
        Me.txtAccount.Location = New System.Drawing.Point(350, 88)
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(150, 25)
        Me.txtAccount.TabIndex = 5
        Me.txtAccount.Visible = False
        '
        'lblPercent
        '
        Me.lblPercent.AutoSize = True
        Me.lblPercent.Location = New System.Drawing.Point(487, 39)
        Me.lblPercent.Name = "lblPercent"
        Me.lblPercent.Size = New System.Drawing.Size(197, 19)
        Me.lblPercent.TabIndex = 0
        Me.lblPercent.Text = "نسبة العمولة البيع الافتراضية %"
        '
        'numPercent
        '
        Me.numPercent.DecimalPlaces = 4
        Me.numPercent.Location = New System.Drawing.Point(334, 36)
        Me.numPercent.Name = "numPercent"
        Me.numPercent.Size = New System.Drawing.Size(150, 25)
        Me.numPercent.TabIndex = 1
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.Location = New System.Drawing.Point(504, 91)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(123, 19)
        Me.lblAccount.TabIndex = 2
        Me.lblAccount.Text = "حساب إيراد العمولة"
        Me.lblAccount.Visible = False
        '
        'cmbAccount
        '
        Me.cmbAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAccount.Location = New System.Drawing.Point(363, 119)
        Me.cmbAccount.Name = "cmbAccount"
        Me.cmbAccount.Size = New System.Drawing.Size(137, 25)
        Me.cmbAccount.TabIndex = 3
        Me.cmbAccount.Visible = False
        '
        'lblUpdatedInfo
        '
        Me.lblUpdatedInfo.AutoSize = True
        Me.lblUpdatedInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblUpdatedInfo.Location = New System.Drawing.Point(31, 39)
        Me.lblUpdatedInfo.Name = "lblUpdatedInfo"
        Me.lblUpdatedInfo.Size = New System.Drawing.Size(68, 15)
        Me.lblUpdatedInfo.TabIndex = 4
        Me.lblUpdatedInfo.Text = "آخر تحديث: -"
        '
        'PanelBottom
        '
        Me.PanelBottom.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelBottom.Controls.Add(Me.btnSave)
        Me.PanelBottom.Controls.Add(Me.btnRefresh)
        Me.PanelBottom.Controls.Add(Me.btnClose)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottom.Location = New System.Drawing.Point(0, 251)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(734, 60)
        Me.PanelBottom.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.SeaGreen
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(580, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(120, 36)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "حفظ التعديل"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(450, 12)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 36)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.MistyRose
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(20, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 36)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'FrmExchangeSettings
        '
        Me.ClientSize = New System.Drawing.Size(734, 311)
        Me.Controls.Add(Me.grpSettings)
        Me.Controls.Add(Me.PanelBottom)
        Me.Controls.Add(Me.PanelTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "FrmExchangeSettings"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إعدادات الصرافة"
        Me.PanelTop.ResumeLayout(False)
        Me.PanelTop.PerformLayout()
        Me.grpSettings.ResumeLayout(False)
        Me.grpSettings.PerformLayout()
        CType(Me.numPercent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTop As Panel
    Friend WithEvents lblTitle As Label

    Friend WithEvents grpSettings As GroupBox
    Friend WithEvents lblPercent As Label
    Friend WithEvents numPercent As NumericUpDown
    Friend WithEvents lblAccount As Label
    Friend WithEvents cmbAccount As ComboBox
    Friend WithEvents lblUpdatedInfo As Label

    Friend WithEvents PanelBottom As Panel
    Friend WithEvents btnSave As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents txtAccount As TextBox
End Class
