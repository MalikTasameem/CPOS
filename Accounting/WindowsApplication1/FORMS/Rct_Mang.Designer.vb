<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rct_Mang
    Inherits Base_Form

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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.AcC_INFO1 = New Accounting.ACC_INFO()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.From_Grid = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_CODE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.REMOVE_BTN_from = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.To_Grid = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.REMOVE_BTN_to = New System.Windows.Forms.Button()
        Me.To_RadioBtn = New System.Windows.Forms.RadioButton()
        Me.From_RadioBtn = New System.Windows.Forms.RadioButton()
        Me.ADD_Btn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        CType(Me.From_Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.To_Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AcC_INFO1
        '
        Me.AcC_INFO1.Location = New System.Drawing.Point(203, 6)
        Me.AcC_INFO1.Name = "AcC_INFO1"
        Me.AcC_INFO1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AcC_INFO1.Size = New System.Drawing.Size(459, 33)
        Me.AcC_INFO1.TabIndex = 82
        '
        'Panel2
        '
        Me.Panel2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Panel2.Controls.Add(Me.From_Grid)
        Me.Panel2.Controls.Add(Me.REMOVE_BTN_from)
        Me.Panel2.Location = New System.Drawing.Point(1, 52)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(695, 185)
        Me.Panel2.TabIndex = 81
        '
        'From_Grid
        '
        Me.From_Grid.AllowUserToAddRows = False
        Me.From_Grid.AllowUserToDeleteRows = False
        Me.From_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.From_Grid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.From_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.From_Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.ACC_CODE_CL, Me.ACC_NAME_CL})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.From_Grid.DefaultCellStyle = DataGridViewCellStyle3
        Me.From_Grid.Dock = System.Windows.Forms.DockStyle.Right
        Me.From_Grid.Location = New System.Drawing.Point(157, 0)
        Me.From_Grid.Margin = New System.Windows.Forms.Padding(4)
        Me.From_Grid.MultiSelect = False
        Me.From_Grid.Name = "From_Grid"
        Me.From_Grid.ReadOnly = True
        Me.From_Grid.RowTemplate.Height = 30
        Me.From_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.From_Grid.Size = New System.Drawing.Size(538, 185)
        Me.From_Grid.TabIndex = 40
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'ACC_CODE_CL
        '
        Me.ACC_CODE_CL.DataPropertyName = "ACC_CODE"
        Me.ACC_CODE_CL.HeaderText = "رقم الحساب"
        Me.ACC_CODE_CL.Name = "ACC_CODE_CL"
        Me.ACC_CODE_CL.ReadOnly = True
        '
        'ACC_NAME_CL
        '
        Me.ACC_NAME_CL.DataPropertyName = "ACC_NAME"
        Me.ACC_NAME_CL.HeaderText = "إسم الحساب"
        Me.ACC_NAME_CL.Name = "ACC_NAME_CL"
        Me.ACC_NAME_CL.ReadOnly = True
        '
        'REMOVE_BTN_from
        '
        Me.REMOVE_BTN_from.BackColor = System.Drawing.Color.White
        Me.REMOVE_BTN_from.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.REMOVE_BTN_from.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.REMOVE_BTN_from.ForeColor = System.Drawing.Color.DarkRed
        Me.REMOVE_BTN_from.Location = New System.Drawing.Point(3, 4)
        Me.REMOVE_BTN_from.Margin = New System.Windows.Forms.Padding(4)
        Me.REMOVE_BTN_from.Name = "REMOVE_BTN_from"
        Me.REMOVE_BTN_from.Size = New System.Drawing.Size(53, 176)
        Me.REMOVE_BTN_from.TabIndex = 78
        Me.REMOVE_BTN_from.Text = "حذف  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "🗑️"
        Me.REMOVE_BTN_from.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.REMOVE_BTN_from.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Panel1.Controls.Add(Me.To_Grid)
        Me.Panel1.Controls.Add(Me.REMOVE_BTN_to)
        Me.Panel1.Location = New System.Drawing.Point(2, 239)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(694, 179)
        Me.Panel1.TabIndex = 80
        '
        'To_Grid
        '
        Me.To_Grid.AllowUserToAddRows = False
        Me.To_Grid.AllowUserToDeleteRows = False
        Me.To_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.To_Grid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.To_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.To_Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 9.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.To_Grid.DefaultCellStyle = DataGridViewCellStyle4
        Me.To_Grid.Dock = System.Windows.Forms.DockStyle.Right
        Me.To_Grid.Location = New System.Drawing.Point(156, 0)
        Me.To_Grid.Margin = New System.Windows.Forms.Padding(4)
        Me.To_Grid.MultiSelect = False
        Me.To_Grid.Name = "To_Grid"
        Me.To_Grid.ReadOnly = True
        Me.To_Grid.RowTemplate.Height = 30
        Me.To_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.To_Grid.Size = New System.Drawing.Size(538, 179)
        Me.To_Grid.TabIndex = 41
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "T_ID"
        Me.DataGridViewTextBoxColumn1.HeaderText = "T_ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "ACC_CODE"
        Me.DataGridViewTextBoxColumn2.HeaderText = "رقم الحساب"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "ACC_NAME"
        Me.DataGridViewTextBoxColumn3.HeaderText = "إسم الحساب"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'REMOVE_BTN_to
        '
        Me.REMOVE_BTN_to.BackColor = System.Drawing.Color.White
        Me.REMOVE_BTN_to.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.REMOVE_BTN_to.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.REMOVE_BTN_to.ForeColor = System.Drawing.Color.DarkRed
        Me.REMOVE_BTN_to.Location = New System.Drawing.Point(3, 5)
        Me.REMOVE_BTN_to.Margin = New System.Windows.Forms.Padding(4)
        Me.REMOVE_BTN_to.Name = "REMOVE_BTN_to"
        Me.REMOVE_BTN_to.Size = New System.Drawing.Size(53, 173)
        Me.REMOVE_BTN_to.TabIndex = 79
        Me.REMOVE_BTN_to.Text = "حذف  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "🗑️"
        Me.REMOVE_BTN_to.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.REMOVE_BTN_to.UseVisualStyleBackColor = False
        '
        'To_RadioBtn
        '
        Me.To_RadioBtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.To_RadioBtn.AutoSize = True
        Me.To_RadioBtn.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.To_RadioBtn.Location = New System.Drawing.Point(700, 242)
        Me.To_RadioBtn.Name = "To_RadioBtn"
        Me.To_RadioBtn.Size = New System.Drawing.Size(112, 21)
        Me.To_RadioBtn.TabIndex = 43
        Me.To_RadioBtn.Text = "حساب خزينة"
        Me.To_RadioBtn.UseVisualStyleBackColor = True
        '
        'From_RadioBtn
        '
        Me.From_RadioBtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.From_RadioBtn.AutoSize = True
        Me.From_RadioBtn.Checked = True
        Me.From_RadioBtn.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.From_RadioBtn.Location = New System.Drawing.Point(700, 55)
        Me.From_RadioBtn.Name = "From_RadioBtn"
        Me.From_RadioBtn.Size = New System.Drawing.Size(110, 21)
        Me.From_RadioBtn.TabIndex = 42
        Me.From_RadioBtn.TabStop = True
        Me.From_RadioBtn.Text = "حساب عملاء"
        Me.From_RadioBtn.UseVisualStyleBackColor = True
        '
        'ADD_Btn
        '
        Me.ADD_Btn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ADD_Btn.BackColor = System.Drawing.Color.White
        Me.ADD_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADD_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADD_Btn.ForeColor = System.Drawing.Color.Blue
        Me.ADD_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ADD_Btn.Location = New System.Drawing.Point(24, 6)
        Me.ADD_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.ADD_Btn.Name = "ADD_Btn"
        Me.ADD_Btn.Size = New System.Drawing.Size(85, 33)
        Me.ADD_Btn.TabIndex = 77
        Me.ADD_Btn.Text = "إضافـة   ➕"
        Me.ADD_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ADD_Btn.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(0, 424)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(824, 40)
        Me.Button1.TabIndex = 76
        Me.Button1.Text = "عـــودة   ↩️"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Rct_Mang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 464)
        Me.Controls.Add(Me.AcC_INFO1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.To_RadioBtn)
        Me.Controls.Add(Me.From_RadioBtn)
        Me.Controls.Add(Me.ADD_Btn)
        Me.Controls.Add(Me.Button1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.25!)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Rct_Mang"
        Me.Text = "إدراة السندات"
        Me.Panel2.ResumeLayout(False)
        CType(Me.From_Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.To_Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents From_Grid As DataGridView
    Friend WithEvents T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_CODE_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_NAME_CL As DataGridViewTextBoxColumn
    Friend WithEvents To_Grid As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents From_RadioBtn As RadioButton
    Friend WithEvents To_RadioBtn As RadioButton
    Friend WithEvents Button1 As Button
    Friend WithEvents ADD_Btn As Button
    Friend WithEvents REMOVE_BTN_from As Button
    Friend WithEvents REMOVE_BTN_to As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents AcC_INFO1 As ACC_INFO
End Class
