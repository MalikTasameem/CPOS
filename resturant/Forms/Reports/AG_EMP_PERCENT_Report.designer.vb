<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AG_EMP_PERCENT_Report
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AG_EMP_PERCENT_Report))
        Me.Frm_ST_cm = New System.Windows.Forms.ComboBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Frm_Cn_DGV_2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Frm_Dt_DGV_2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.Print_Frm_Cn_Btn = New System.Windows.Forms.Button()
        Me.Frm_Search_Btn = New System.Windows.Forms.Button()
        Me.Print_Frm_Dt_Btn = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DataB_1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataB_2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.AG_Cm = New resturant.FSearch_Filter()
        Me.GroupBox4.SuspendLayout()
        CType(Me.Frm_Cn_DGV_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.Frm_Dt_DGV_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataB_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataB_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frm_ST_cm
        '
        Me.Frm_ST_cm.BackColor = System.Drawing.SystemColors.Info
        Me.Frm_ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Frm_ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Frm_ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Frm_ST_cm.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frm_ST_cm.FormattingEnabled = True
        Me.Frm_ST_cm.Location = New System.Drawing.Point(323, 4)
        Me.Frm_ST_cm.Name = "Frm_ST_cm"
        Me.Frm_ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Frm_ST_cm.Size = New System.Drawing.Size(216, 26)
        Me.Frm_ST_cm.TabIndex = 698
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Frm_Cn_DGV_2)
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 381)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox4.Size = New System.Drawing.Size(891, 275)
        Me.GroupBox4.TabIndex = 696
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "تقرير الإحصائيات"
        '
        'Frm_Cn_DGV_2
        '
        Me.Frm_Cn_DGV_2.AllowUserToAddRows = False
        Me.Frm_Cn_DGV_2.AllowUserToDeleteRows = False
        Me.Frm_Cn_DGV_2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Frm_Cn_DGV_2.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Frm_Cn_DGV_2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Frm_Cn_DGV_2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Frm_Cn_DGV_2.FilterAndSortEnabled = True
        Me.Frm_Cn_DGV_2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.Frm_Cn_DGV_2.Location = New System.Drawing.Point(5, 25)
        Me.Frm_Cn_DGV_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Frm_Cn_DGV_2.MultiSelect = False
        Me.Frm_Cn_DGV_2.Name = "Frm_Cn_DGV_2"
        Me.Frm_Cn_DGV_2.ReadOnly = True
        Me.Frm_Cn_DGV_2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Frm_Cn_DGV_2.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frm_Cn_DGV_2.RowTemplate.Height = 25
        Me.Frm_Cn_DGV_2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Frm_Cn_DGV_2.Size = New System.Drawing.Size(881, 326)
        Me.Frm_Cn_DGV_2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.Frm_Cn_DGV_2.TabIndex = 904
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Frm_Dt_DGV_2)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(3, 42)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox2.Size = New System.Drawing.Size(891, 333)
        Me.GroupBox2.TabIndex = 693
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "التقرير التفصيلي"
        '
        'Frm_Dt_DGV_2
        '
        Me.Frm_Dt_DGV_2.AllowUserToAddRows = False
        Me.Frm_Dt_DGV_2.AllowUserToDeleteRows = False
        Me.Frm_Dt_DGV_2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Frm_Dt_DGV_2.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Frm_Dt_DGV_2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Frm_Dt_DGV_2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Frm_Dt_DGV_2.FilterAndSortEnabled = True
        Me.Frm_Dt_DGV_2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.Frm_Dt_DGV_2.Location = New System.Drawing.Point(8, 24)
        Me.Frm_Dt_DGV_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Frm_Dt_DGV_2.MultiSelect = False
        Me.Frm_Dt_DGV_2.Name = "Frm_Dt_DGV_2"
        Me.Frm_Dt_DGV_2.ReadOnly = True
        Me.Frm_Dt_DGV_2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Frm_Dt_DGV_2.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frm_Dt_DGV_2.RowTemplate.Height = 25
        Me.Frm_Dt_DGV_2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Frm_Dt_DGV_2.Size = New System.Drawing.Size(878, 303)
        Me.Frm_Dt_DGV_2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.Frm_Dt_DGV_2.TabIndex = 903
        '
        'Print_Frm_Cn_Btn
        '
        Me.Print_Frm_Cn_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Frm_Cn_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_Frm_Cn_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Print_Frm_Cn_Btn.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_Frm_Cn_Btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.Print_Frm_Cn_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_Frm_Cn_Btn.Location = New System.Drawing.Point(595, 2)
        Me.Print_Frm_Cn_Btn.Name = "Print_Frm_Cn_Btn"
        Me.Print_Frm_Cn_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Frm_Cn_Btn.Size = New System.Drawing.Size(105, 38)
        Me.Print_Frm_Cn_Btn.TabIndex = 695
        Me.Print_Frm_Cn_Btn.Text = "طباعة الإجماليات"
        Me.Print_Frm_Cn_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_Frm_Cn_Btn.UseVisualStyleBackColor = False
        '
        'Frm_Search_Btn
        '
        Me.Frm_Search_Btn.BackColor = System.Drawing.Color.White
        Me.Frm_Search_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Frm_Search_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Frm_Search_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Frm_Search_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frm_Search_Btn.ForeColor = System.Drawing.Color.Black
        Me.Frm_Search_Btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Frm_Search_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Frm_Search_Btn.Location = New System.Drawing.Point(810, 2)
        Me.Frm_Search_Btn.Name = "Frm_Search_Btn"
        Me.Frm_Search_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Frm_Search_Btn.Size = New System.Drawing.Size(84, 38)
        Me.Frm_Search_Btn.TabIndex = 697
        Me.Frm_Search_Btn.Text = "بحث"
        Me.Frm_Search_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Frm_Search_Btn.UseVisualStyleBackColor = False
        '
        'Print_Frm_Dt_Btn
        '
        Me.Print_Frm_Dt_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Frm_Dt_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_Frm_Dt_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Print_Frm_Dt_Btn.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_Frm_Dt_Btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.Print_Frm_Dt_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_Frm_Dt_Btn.Location = New System.Drawing.Point(702, 2)
        Me.Print_Frm_Dt_Btn.Name = "Print_Frm_Dt_Btn"
        Me.Print_Frm_Dt_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Frm_Dt_Btn.Size = New System.Drawing.Size(106, 38)
        Me.Print_Frm_Dt_Btn.TabIndex = 694
        Me.Print_Frm_Dt_Btn.Text = "طباعة" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "تقرير التفصيلي"
        Me.Print_Frm_Dt_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_Frm_Dt_Btn.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(264, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 19)
        Me.Label4.TabIndex = 702
        Me.Label4.Text = "الموظف"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(544, 8)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 19)
        Me.Label9.TabIndex = 701
        Me.Label9.Text = "المخزن"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AG_Cm
        '
        Me.AG_Cm.CancelSearchImage = CType(resources.GetObject("AG_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.AG_Cm.Location = New System.Drawing.Point(11, 2)
        Me.AG_Cm.Name = "AG_Cm"
        Me.AG_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Cm.Size = New System.Drawing.Size(249, 34)
        Me.AG_Cm.SQL_Column = "AG_NAME"
        Me.AG_Cm.SQL_ID = "AG_ID"
        Me.AG_Cm.SQL_IsNumericSearchField = False
        Me.AG_Cm.SQL_ListSize = 200
        Me.AG_Cm.SQL_NumberOfRows = 200
        Me.AG_Cm.SQL_OrderByField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField = "AG_NAME"
        Me.AG_Cm.SQL_Table = "Agents_Emp_V"
        Me.AG_Cm.TabIndex = 700
        Me.AG_Cm.TextMaxLength = 250
        Me.AG_Cm.Textt = ""
        '
        'AG_EMP_PERCENT_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.AG_Cm)
        Me.Controls.Add(Me.Frm_ST_cm)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Print_Frm_Cn_Btn)
        Me.Controls.Add(Me.Frm_Search_Btn)
        Me.Controls.Add(Me.Print_Frm_Dt_Btn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AG_EMP_PERCENT_Report"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.Frm_Cn_DGV_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.Frm_Dt_DGV_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataB_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataB_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Frm_ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Print_Frm_Cn_Btn As System.Windows.Forms.Button
    Friend WithEvents Frm_Search_Btn As System.Windows.Forms.Button
    Friend WithEvents Print_Frm_Dt_Btn As System.Windows.Forms.Button
    Friend WithEvents Frm_Cn_DGV_2 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents Frm_Dt_DGV_2 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents AG_Cm As resturant.FSearch_Filter
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DataB_1 As System.Windows.Forms.BindingSource
    Friend WithEvents DataB_2 As System.Windows.Forms.BindingSource
End Class
