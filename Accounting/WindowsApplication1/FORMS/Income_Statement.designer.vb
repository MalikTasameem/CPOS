<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Income_Statement
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Income_Statement))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CircularPanel = New System.Windows.Forms.Panel()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Template_Cm = New System.Windows.Forms.ComboBox()
        Me.Template_Lbl = New System.Windows.Forms.Label()
        Me.Print_Btn = New Accounting.SplitButton()
        Me.Hide_Zeros_CB = New System.Windows.Forms.CheckBox()
        Me.DateRange_Flate1 = New Accounting.DateRange_Flate()
        Me.Search_btn = New System.Windows.Forms.Button()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.Print_CntxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.إستخراجالتقريرExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.CircularPanel.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Print_CntxtMStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.CircularPanel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.47619!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.52381!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1004, 695)
        Me.TableLayoutPanel1.TabIndex = 87
        '
        'CircularPanel
        '
        Me.CircularPanel.BackColor = System.Drawing.Color.Transparent
        Me.CircularPanel.Controls.Add(Me.CircularProgressControl1)
        Me.CircularPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularPanel.Location = New System.Drawing.Point(3, 633)
        Me.CircularPanel.Name = "CircularPanel"
        Me.CircularPanel.Size = New System.Drawing.Size(998, 59)
        Me.CircularPanel.TabIndex = 898
        Me.CircularPanel.Visible = False
        '
        'CircularProgressControl1
        '
        Me.CircularProgressControl1.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularProgressControl1.Interval = 60
        Me.CircularProgressControl1.Location = New System.Drawing.Point(0, 0)
        Me.CircularProgressControl1.MinimumSize = New System.Drawing.Size(28, 28)
        Me.CircularProgressControl1.Name = "CircularProgressControl1"
        Me.CircularProgressControl1.Rotation = Accounting.CircularProgressControl.Direction.CLOCKWISE
        Me.CircularProgressControl1.Size = New System.Drawing.Size(998, 59)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 87
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DataGridView1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 132)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(998, 495)
        Me.Panel2.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.ColumnHeadersVisible = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(6)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 30
        Me.DataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(998, 495)
        Me.DataGridView1.TabIndex = 86
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Template_Cm)
        Me.Panel1.Controls.Add(Me.Template_Lbl)
        Me.Panel1.Controls.Add(Me.Print_Btn)
        Me.Panel1.Controls.Add(Me.Hide_Zeros_CB)
        Me.Panel1.Controls.Add(Me.DateRange_Flate1)
        Me.Panel1.Controls.Add(Me.Search_btn)
        Me.Panel1.Controls.Add(Me.TITLE_txt)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(998, 123)
        Me.Panel1.TabIndex = 0
        '
        'Template_Cm
        '
        Me.Template_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Template_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Template_Cm.FormattingEnabled = True
        Me.Template_Cm.Location = New System.Drawing.Point(539, 84)
        Me.Template_Cm.Name = "Template_Cm"
        Me.Template_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Template_Cm.Size = New System.Drawing.Size(250, 25)
        Me.Template_Cm.TabIndex = 913
        '
        'Template_Lbl
        '
        Me.Template_Lbl.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.Template_Lbl.Location = New System.Drawing.Point(793, 84)
        Me.Template_Lbl.Name = "Template_Lbl"
        Me.Template_Lbl.Size = New System.Drawing.Size(65, 32)
        Me.Template_Lbl.TabIndex = 912
        Me.Template_Lbl.Text = "القالب:"
        Me.Template_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Print_Btn.ButtonImage = Nothing
        Me.Print_Btn.ButtonText = "🖨️  طباعــة"
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.Location = New System.Drawing.Point(279, 80)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Padding = New System.Windows.Forms.Padding(1)
        Me.Print_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Btn.Size = New System.Drawing.Size(258, 40)
        Me.Print_Btn.TabIndex = 911
        '
        'Hide_Zeros_CB
        '
        Me.Hide_Zeros_CB.AutoSize = True
        Me.Hide_Zeros_CB.Checked = True
        Me.Hide_Zeros_CB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Hide_Zeros_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Hide_Zeros_CB.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Hide_Zeros_CB.Location = New System.Drawing.Point(9, 97)
        Me.Hide_Zeros_CB.Name = "Hide_Zeros_CB"
        Me.Hide_Zeros_CB.Size = New System.Drawing.Size(146, 19)
        Me.Hide_Zeros_CB.TabIndex = 108
        Me.Hide_Zeros_CB.Text = "إخفــــــاء الحسابــات الصفريــــة"
        Me.Hide_Zeros_CB.UseVisualStyleBackColor = True
        '
        'DateRange_Flate1
        '
        Me.DateRange_Flate1.AutoSize = True
        Me.DateRange_Flate1.BackColor = System.Drawing.Color.Transparent
        Me.DateRange_Flate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DateRange_Flate1.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.DateRange_Flate1.Location = New System.Drawing.Point(2, 2)
        Me.DateRange_Flate1.Name = "DateRange_Flate1"
        Me.DateRange_Flate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateRange_Flate1.Size = New System.Drawing.Size(535, 77)
        Me.DateRange_Flate1.TabIndex = 107
        '
        'Search_btn
        '
        Me.Search_btn.BackColor = System.Drawing.Color.White
        Me.Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Search_btn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Search_btn.Image = CType(resources.GetObject("Search_btn.Image"), System.Drawing.Image)
        Me.Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Search_btn.Location = New System.Drawing.Point(859, 80)
        Me.Search_btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Search_btn.Name = "Search_btn"
        Me.Search_btn.Size = New System.Drawing.Size(139, 40)
        Me.Search_btn.TabIndex = 84
        Me.Search_btn.Text = "بحـــث"
        Me.Search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Search_btn.UseVisualStyleBackColor = False
        '
        'TITLE_txt
        '
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.TITLE_txt.Location = New System.Drawing.Point(539, 2)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(459, 77)
        Me.TITLE_txt.TabIndex = 82
        Me.TITLE_txt.Text = "إعـــداد قائمـــــة الدخــــل"
        Me.TITLE_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Print_CntxtMStrip
        '
        Me.Print_CntxtMStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.إستخراجالتقريرExcelToolStripMenuItem})
        Me.Print_CntxtMStrip.Name = "ContextMenuStrip1"
        Me.Print_CntxtMStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_CntxtMStrip.Size = New System.Drawing.Size(177, 26)
        '
        'إستخراجالتقريرExcelToolStripMenuItem
        '
        Me.إستخراجالتقريرExcelToolStripMenuItem.Name = "إستخراجالتقريرExcelToolStripMenuItem"
        Me.إستخراجالتقريرExcelToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.إستخراجالتقريرExcelToolStripMenuItem.Text = "إستخراج التقرير Excel"
        '
        'Income_Statement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "Income_Statement"
        Me.Text = "قائمة الميزانية"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.CircularPanel.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Print_CntxtMStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Search_btn As Button
    Friend WithEvents TITLE_txt As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DateRange_Flate1 As DateRange_Flate
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents CircularPanel As Panel
    Friend WithEvents Hide_Zeros_CB As CheckBox
    Friend WithEvents Print_Btn As SplitButton
    Friend WithEvents Print_CntxtMStrip As ContextMenuStrip
    Friend WithEvents إستخراجالتقريرExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Template_Cm As ComboBox
    Friend WithEvents Template_Lbl As Label
End Class
