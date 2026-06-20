<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMP_Add_Periods
    Inherits Base_Form
    'Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EMP_Add_Periods))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Currency_Buy_txt = New Accounting.F2FloatField()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Currency_Equal_txt = New Accounting.F2FloatField()
        Me.Currency_Cm = New System.Windows.Forms.ComboBox()
        Me.IM_SH_txt = New System.Windows.Forms.TextBox()
        Me.Button = New System.Windows.Forms.Button()
        Me.Cancel_Btn = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Date_From = New System.Windows.Forms.DateTimePicker()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sys_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BuyPrice_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_T_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Currency_Buy_txt)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Currency_Equal_txt)
        Me.Panel1.Controls.Add(Me.Currency_Cm)
        Me.Panel1.Controls.Add(Me.IM_SH_txt)
        Me.Panel1.Controls.Add(Me.Button)
        Me.Panel1.Controls.Add(Me.Cancel_Btn)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Date_From)
        Me.Panel1.Controls.Add(Me.SaveButton)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Panel1.Location = New System.Drawing.Point(2, 1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(956, 73)
        Me.Panel1.TabIndex = 669
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Label2.Location = New System.Drawing.Point(549, 11)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(71, 17)
        Me.Label2.TabIndex = 900
        Me.Label2.Text = " سعر الشراء:"
        '
        'Currency_Buy_txt
        '
        Me.Currency_Buy_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Currency_Buy_txt.BackColor = System.Drawing.Color.White
        Me.Currency_Buy_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Currency_Buy_txt.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Currency_Buy_txt.Location = New System.Drawing.Point(470, 6)
        Me.Currency_Buy_txt.MaxLength = 0
        Me.Currency_Buy_txt.Name = "Currency_Buy_txt"
        Me.Currency_Buy_txt.Size = New System.Drawing.Size(75, 26)
        Me.Currency_Buy_txt.TabIndex = 899
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Label16.Location = New System.Drawing.Point(703, 11)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label16.Size = New System.Drawing.Size(62, 17)
        Me.Label16.TabIndex = 898
        Me.Label16.Text = " سعر البيع:"
        '
        'Currency_Equal_txt
        '
        Me.Currency_Equal_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Currency_Equal_txt.BackColor = System.Drawing.Color.White
        Me.Currency_Equal_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Currency_Equal_txt.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Currency_Equal_txt.Location = New System.Drawing.Point(624, 6)
        Me.Currency_Equal_txt.MaxLength = 0
        Me.Currency_Equal_txt.Name = "Currency_Equal_txt"
        Me.Currency_Equal_txt.Size = New System.Drawing.Size(75, 26)
        Me.Currency_Equal_txt.TabIndex = 897
        '
        'Currency_Cm
        '
        Me.Currency_Cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Currency_Cm.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Currency_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Currency_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Currency_Cm.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Currency_Cm.FormattingEnabled = True
        Me.Currency_Cm.Location = New System.Drawing.Point(770, 7)
        Me.Currency_Cm.Margin = New System.Windows.Forms.Padding(4)
        Me.Currency_Cm.Name = "Currency_Cm"
        Me.Currency_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Currency_Cm.Size = New System.Drawing.Size(127, 24)
        Me.Currency_Cm.TabIndex = 896
        '
        'IM_SH_txt
        '
        Me.IM_SH_txt.BackColor = System.Drawing.SystemColors.Window
        Me.IM_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_SH_txt.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_SH_txt.Location = New System.Drawing.Point(373, 39)
        Me.IM_SH_txt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.IM_SH_txt.Name = "IM_SH_txt"
        Me.IM_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_SH_txt.Size = New System.Drawing.Size(525, 26)
        Me.IM_SH_txt.TabIndex = 895
        '
        'Button
        '
        Me.Button.BackColor = System.Drawing.Color.White
        Me.Button.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Button.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button.Location = New System.Drawing.Point(11, 33)
        Me.Button.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.Button.Name = "Button"
        Me.Button.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button.Size = New System.Drawing.Size(139, 37)
        Me.Button.TabIndex = 686
        Me.Button.Text = "🔍 عرض قائمة الأسعار"
        Me.Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button.UseVisualStyleBackColor = False
        '
        'Cancel_Btn
        '
        Me.Cancel_Btn.BackColor = System.Drawing.Color.White
        Me.Cancel_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancel_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cancel_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Cancel_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Cancel_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Cancel_Btn.Location = New System.Drawing.Point(151, 33)
        Me.Cancel_Btn.Margin = New System.Windows.Forms.Padding(4, 8, 4, 8)
        Me.Cancel_Btn.Name = "Cancel_Btn"
        Me.Cancel_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Cancel_Btn.Size = New System.Drawing.Size(86, 37)
        Me.Cancel_Btn.TabIndex = 670
        Me.Cancel_Btn.Text = "✖️ إلغاء"
        Me.Cancel_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Cancel_Btn.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Label4.Location = New System.Drawing.Point(369, 10)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 17)
        Me.Label4.TabIndex = 683
        Me.Label4.Text = "بداية الإحتساب"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Date_From
        '
        Me.Date_From.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Date_From.CalendarFont = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Date_From.CustomFormat = "dd/MM/yyyy"
        Me.Date_From.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Date_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Date_From.Location = New System.Drawing.Point(246, 6)
        Me.Date_From.Name = "Date_From"
        Me.Date_From.RightToLeftLayout = True
        Me.Date_From.Size = New System.Drawing.Size(119, 24)
        Me.Date_From.TabIndex = 679
        '
        'SaveButton
        '
        Me.SaveButton.BackColor = System.Drawing.Color.White
        Me.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveButton.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveButton.Location = New System.Drawing.Point(239, 33)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SaveButton.Size = New System.Drawing.Size(126, 37)
        Me.SaveButton.TabIndex = 40
        Me.SaveButton.Text = "➕  إضافــة"
        Me.SaveButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(901, 10)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(43, 17)
        Me.Label1.TabIndex = 117
        Me.Label1.Text = " العملة:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.White
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(2, 529)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(956, 42)
        Me.ExitFormButton.TabIndex = 668
        Me.ExitFormButton.Text = "↩️  عــودة"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 13.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.Sys_Name_CL, Me.Price_CL, Me.BuyPrice_CL, Me.Column2, Me.D_T_CL})
        Me.DataGridView1.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 13.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkGray
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DataGridView1.RowHeadersVisible = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.RowTemplate.Height = 25
        Me.DataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(956, 444)
        Me.DataGridView1.TabIndex = 641
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DataGridView1)
        Me.Panel2.Location = New System.Drawing.Point(2, 83)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(956, 444)
        Me.Panel2.TabIndex = 670
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.FillWeight = 0.6769074!
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        '
        'Sys_Name_CL
        '
        Me.Sys_Name_CL.DataPropertyName = "CR_Name"
        Me.Sys_Name_CL.HeaderText = "العملــة"
        Me.Sys_Name_CL.Name = "Sys_Name_CL"
        Me.Sys_Name_CL.ReadOnly = True
        '
        'Price_CL
        '
        Me.Price_CL.DataPropertyName = "Price"
        Me.Price_CL.HeaderText = "سعر البيع"
        Me.Price_CL.Name = "Price_CL"
        Me.Price_CL.ReadOnly = True
        '
        'BuyPrice_CL
        '
        Me.BuyPrice_CL.DataPropertyName = "BuyPrice"
        Me.BuyPrice_CL.HeaderText = "سعر الشراء"
        Me.BuyPrice_CL.Name = "BuyPrice_CL"
        Me.BuyPrice_CL.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "D_F"
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column2.FillWeight = 180.0!
        Me.Column2.HeaderText = "بداية تاريخ الإحتساب"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'D_T_CL
        '
        Me.D_T_CL.DataPropertyName = "D_T"
        Me.D_T_CL.FillWeight = 150.0!
        Me.D_T_CL.HeaderText = "نهاية تاريخ الإحتساب"
        Me.D_T_CL.Name = "D_T_CL"
        Me.D_T_CL.ReadOnly = True
        '
        'EMP_Add_Periods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(959, 573)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Arial", 13.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EMP_Add_Periods"
        Me.Text = "جـــدول إدارة العملات"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SaveButton As System.Windows.Forms.Button

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Cancel_Btn As System.Windows.Forms.Button
    Friend WithEvents Label4 As Label

    Friend WithEvents Date_From As DateTimePicker
    Friend WithEvents Button As Button
    Friend WithEvents IM_SH_txt As TextBox
    Friend WithEvents Currency_Cm As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Currency_Equal_txt As F2FloatField
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Currency_Buy_txt As F2FloatField
    Friend WithEvents T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents Sys_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents Price_CL As DataGridViewTextBoxColumn
    Friend WithEvents BuyPrice_CL As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents D_T_CL As DataGridViewTextBoxColumn
End Class
