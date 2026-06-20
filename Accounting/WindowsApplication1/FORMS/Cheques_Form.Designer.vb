<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Cheques_Form
    Inherits Base_Form

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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Print_Btn = New Accounting.SplitButton()
        Me.Print_CntxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.إستخراجالتقريرExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Search_By_Acc_Name_txt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SEARCH_CM = New System.Windows.Forms.ComboBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.DATE_TYPE_CM = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Cheque_Type_CM = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Search_btn = New System.Windows.Forms.Button()
        Me.DateRange_Flate1 = New Accounting.DateRange_Flate()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.CircularPanel = New System.Windows.Forms.Panel()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.DataB = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Print_CntxtMStrip.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CircularPanel.SuspendLayout()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button1, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CircularPanel, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.62063!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.37936!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1004, 825)
        Me.TableLayoutPanel1.TabIndex = 108
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Print_Btn)
        Me.Panel1.Controls.Add(Me.Search_By_Acc_Name_txt)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.SEARCH_CM)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Search_btn)
        Me.Panel1.Controls.Add(Me.DateRange_Flate1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 4)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(998, 189)
        Me.Panel1.TabIndex = 0
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Print_Btn.ButtonImage = Nothing
        Me.Print_Btn.ButtonText = "طباعــة  🖨️"
        Me.Print_Btn.ContextMenuStrip = Me.Print_CntxtMStrip
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.Location = New System.Drawing.Point(3, 97)
        Me.Print_Btn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Padding = New System.Windows.Forms.Padding(1)
        Me.Print_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Btn.Size = New System.Drawing.Size(535, 47)
        Me.Print_Btn.TabIndex = 907
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
        'Search_By_Acc_Name_txt
        '
        Me.Search_By_Acc_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Name_txt.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.Search_By_Acc_Name_txt.Location = New System.Drawing.Point(3, 147)
        Me.Search_By_Acc_Name_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Search_By_Acc_Name_txt.Name = "Search_By_Acc_Name_txt"
        Me.Search_By_Acc_Name_txt.Size = New System.Drawing.Size(681, 26)
        Me.Search_By_Acc_Name_txt.TabIndex = 118
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(914, 152)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 18)
        Me.Label2.TabIndex = 117
        Me.Label2.Text = " بحث حسب:"
        '
        'SEARCH_CM
        '
        Me.SEARCH_CM.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SEARCH_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SEARCH_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SEARCH_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SEARCH_CM.Font = New System.Drawing.Font("Arial", 10.25!, System.Drawing.FontStyle.Bold)
        Me.SEARCH_CM.FormattingEnabled = True
        Me.SEARCH_CM.Location = New System.Drawing.Point(687, 148)
        Me.SEARCH_CM.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SEARCH_CM.Name = "SEARCH_CM"
        Me.SEARCH_CM.Size = New System.Drawing.Size(222, 24)
        Me.SEARCH_CM.TabIndex = 116
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.DATE_TYPE_CM)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Cheque_Type_CM)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Location = New System.Drawing.Point(540, 5)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(455, 91)
        Me.Panel3.TabIndex = 115
        '
        'DATE_TYPE_CM
        '
        Me.DATE_TYPE_CM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DATE_TYPE_CM.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DATE_TYPE_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DATE_TYPE_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DATE_TYPE_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DATE_TYPE_CM.Font = New System.Drawing.Font("Arial", 10.25!, System.Drawing.FontStyle.Bold)
        Me.DATE_TYPE_CM.FormattingEnabled = True
        Me.DATE_TYPE_CM.Items.AddRange(New Object() {"---بدون---", "تاريخ قيد الإيصال", "تاريخ الإصدار", "تاريخ الاستحقاق", "تاريخ المطابقة"})
        Me.DATE_TYPE_CM.Location = New System.Drawing.Point(4, 4)
        Me.DATE_TYPE_CM.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DATE_TYPE_CM.Name = "DATE_TYPE_CM"
        Me.DATE_TYPE_CM.Size = New System.Drawing.Size(205, 24)
        Me.DATE_TYPE_CM.TabIndex = 39
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(212, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 18)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "التاريخ حسب:"
        '
        'Cheque_Type_CM
        '
        Me.Cheque_Type_CM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cheque_Type_CM.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Cheque_Type_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cheque_Type_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cheque_Type_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cheque_Type_CM.Font = New System.Drawing.Font("Arial", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Cheque_Type_CM.FormattingEnabled = True
        Me.Cheque_Type_CM.Location = New System.Drawing.Point(4, 32)
        Me.Cheque_Type_CM.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Cheque_Type_CM.Name = "Cheque_Type_CM"
        Me.Cheque_Type_CM.Size = New System.Drawing.Size(205, 24)
        Me.Cheque_Type_CM.TabIndex = 37
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(213, 35)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 18)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = " حالة الشيك:"
        '
        'Search_btn
        '
        Me.Search_btn.BackColor = System.Drawing.Color.White
        Me.Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Search_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Search_btn.Location = New System.Drawing.Point(540, 97)
        Me.Search_btn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Search_btn.Name = "Search_btn"
        Me.Search_btn.Size = New System.Drawing.Size(454, 48)
        Me.Search_btn.TabIndex = 80
        Me.Search_btn.Text = "🔍 بحـث"
        Me.Search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Search_btn.UseVisualStyleBackColor = False
        '
        'DateRange_Flate1
        '
        Me.DateRange_Flate1.AutoSize = True
        Me.DateRange_Flate1.BackColor = System.Drawing.Color.Transparent
        Me.DateRange_Flate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DateRange_Flate1.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.DateRange_Flate1.Location = New System.Drawing.Point(3, 5)
        Me.DateRange_Flate1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateRange_Flate1.Name = "DateRange_Flate1"
        Me.DateRange_Flate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateRange_Flate1.Size = New System.Drawing.Size(535, 91)
        Me.DateRange_Flate1.TabIndex = 106
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(3, 775)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(998, 46)
        Me.Button1.TabIndex = 75
        Me.Button1.Text = "عـــودة   ↩️"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(4, 202)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 40
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(996, 506)
        Me.DataGridView1.TabIndex = 86
        '
        'CircularPanel
        '
        Me.CircularPanel.Controls.Add(Me.CircularProgressControl1)
        Me.CircularPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularPanel.Location = New System.Drawing.Point(3, 717)
        Me.CircularPanel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CircularPanel.Name = "CircularPanel"
        Me.CircularPanel.Size = New System.Drawing.Size(998, 40)
        Me.CircularPanel.TabIndex = 87
        '
        'CircularProgressControl1
        '
        Me.CircularProgressControl1.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularProgressControl1.Interval = 60
        Me.CircularProgressControl1.Location = New System.Drawing.Point(0, 0)
        Me.CircularProgressControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CircularProgressControl1.MinimumSize = New System.Drawing.Size(28, 33)
        Me.CircularProgressControl1.Name = "CircularProgressControl1"
        Me.CircularProgressControl1.Rotation = Accounting.CircularProgressControl.Direction.CLOCKWISE
        Me.CircularProgressControl1.Size = New System.Drawing.Size(998, 40)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 0
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.CircularProgressControl1.Visible = False
        '
        'Cheques_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 825)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "Cheques_Form"
        Me.Text = "شاشة مطابقة الصكوك"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Print_CntxtMStrip.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CircularPanel.ResumeLayout(False)
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataB As BindingSource
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Search_btn As Button
    Friend WithEvents DateRange_Flate1 As DateRange_Flate
    Friend WithEvents Button1 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents CircularPanel As Panel
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Cheque_Type_CM As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents DATE_TYPE_CM As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents SEARCH_CM As ComboBox
    Friend WithEvents Search_By_Acc_Name_txt As TextBox
    Friend WithEvents Print_Btn As SplitButton
    Friend WithEvents Print_CntxtMStrip As ContextMenuStrip
    Friend WithEvents إستخراجالتقريرExcelToolStripMenuItem As ToolStripMenuItem
End Class
