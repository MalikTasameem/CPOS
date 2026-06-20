<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BALANCE_SEARCH
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Search_By_Acc_Name_txt = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ACC_LEVEL_txt = New System.Windows.Forms.DomainUpDown()
        Me.Search_By_Acc_Code_txt = New System.Windows.Forms.TextBox()
        Me.By_level_CB = New System.Windows.Forms.CheckBox()
        Me.LEVEL_Panel = New System.Windows.Forms.Panel()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LEVEL_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(2, 589)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(828, 36)
        Me.Button1.TabIndex = 43
        Me.Button1.Text = "عـــودة   ↩️"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Search_By_Acc_Name_txt
        '
        Me.Search_By_Acc_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Name_txt.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Search_By_Acc_Name_txt.Location = New System.Drawing.Point(3, 2)
        Me.Search_By_Acc_Name_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Search_By_Acc_Name_txt.Name = "Search_By_Acc_Name_txt"
        Me.Search_By_Acc_Name_txt.Size = New System.Drawing.Size(262, 23)
        Me.Search_By_Acc_Name_txt.TabIndex = 42
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Location = New System.Drawing.Point(2, 36)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 30
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(828, 552)
        Me.DataGridView1.TabIndex = 41
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(65, 9)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 16)
        Me.Label3.TabIndex = 97
        Me.Label3.Text = "مستوى الحساب:"
        '
        'ACC_LEVEL_txt
        '
        Me.ACC_LEVEL_txt.Font = New System.Drawing.Font("Arial", 10.25!, System.Drawing.FontStyle.Bold)
        Me.ACC_LEVEL_txt.Items.Add("9")
        Me.ACC_LEVEL_txt.Items.Add("8")
        Me.ACC_LEVEL_txt.Items.Add("7")
        Me.ACC_LEVEL_txt.Items.Add("6")
        Me.ACC_LEVEL_txt.Items.Add("5")
        Me.ACC_LEVEL_txt.Items.Add("4")
        Me.ACC_LEVEL_txt.Items.Add("3")
        Me.ACC_LEVEL_txt.Items.Add("2")
        Me.ACC_LEVEL_txt.Items.Add("1")
        Me.ACC_LEVEL_txt.Items.Add("0")
        Me.ACC_LEVEL_txt.Location = New System.Drawing.Point(1, 5)
        Me.ACC_LEVEL_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ACC_LEVEL_txt.Name = "ACC_LEVEL_txt"
        Me.ACC_LEVEL_txt.ReadOnly = True
        Me.ACC_LEVEL_txt.Size = New System.Drawing.Size(60, 23)
        Me.ACC_LEVEL_txt.TabIndex = 96
        Me.ACC_LEVEL_txt.Text = "1"
        '
        'Search_By_Acc_Code_txt
        '
        Me.Search_By_Acc_Code_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Code_txt.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Search_By_Acc_Code_txt.Location = New System.Drawing.Point(266, 2)
        Me.Search_By_Acc_Code_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Search_By_Acc_Code_txt.Name = "Search_By_Acc_Code_txt"
        Me.Search_By_Acc_Code_txt.Size = New System.Drawing.Size(239, 23)
        Me.Search_By_Acc_Code_txt.TabIndex = 98
        '
        'By_level_CB
        '
        Me.By_level_CB.AutoSize = True
        Me.By_level_CB.Font = New System.Drawing.Font("Arial", 9.25!, System.Drawing.FontStyle.Bold)
        Me.By_level_CB.Location = New System.Drawing.Point(663, 5)
        Me.By_level_CB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.By_level_CB.Name = "By_level_CB"
        Me.By_level_CB.Size = New System.Drawing.Size(122, 20)
        Me.By_level_CB.TabIndex = 99
        Me.By_level_CB.Text = "عرض حسب المستوى"
        Me.By_level_CB.UseVisualStyleBackColor = True
        '
        'LEVEL_Panel
        '
        Me.LEVEL_Panel.Controls.Add(Me.ACC_LEVEL_txt)
        Me.LEVEL_Panel.Controls.Add(Me.Label3)
        Me.LEVEL_Panel.Location = New System.Drawing.Point(507, 0)
        Me.LEVEL_Panel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LEVEL_Panel.Name = "LEVEL_Panel"
        Me.LEVEL_Panel.Size = New System.Drawing.Size(152, 33)
        Me.LEVEL_Panel.TabIndex = 116
        Me.LEVEL_Panel.Visible = False
        '
        'BALANCE_SEARCH
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 625)
        Me.ControlBox = False
        Me.Controls.Add(Me.LEVEL_Panel)
        Me.Controls.Add(Me.By_level_CB)
        Me.Controls.Add(Me.Search_By_Acc_Code_txt)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Search_By_Acc_Name_txt)
        Me.Controls.Add(Me.DataGridView1)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "BALANCE_SEARCH"
        Me.Text = "بحـــث"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LEVEL_Panel.ResumeLayout(False)
        Me.LEVEL_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Search_By_Acc_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ACC_LEVEL_txt As DomainUpDown
    Friend WithEvents Search_By_Acc_Code_txt As TextBox
    Friend WithEvents By_level_CB As CheckBox
    Friend WithEvents LEVEL_Panel As Panel
End Class
