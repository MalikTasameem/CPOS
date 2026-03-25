<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SB_Reports
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SB_Reports))
        Me.Bill_Berfet_DGV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn32 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn34 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bill_Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bill_Berfet_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_berfet_btn = New System.Windows.Forms.Button()
        Me.B_Berfet_Print_btn = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.B_Berfet_T_txt = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.B_BerfetCounter_txt = New System.Windows.Forms.TextBox()
        Me.B_SB_T_txt = New System.Windows.Forms.TextBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        CType(Me.Bill_Berfet_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Bill_Berfet_DGV
        '
        Me.Bill_Berfet_DGV.AllowUserToAddRows = False
        Me.Bill_Berfet_DGV.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info
        Me.Bill_Berfet_DGV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Bill_Berfet_DGV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Bill_Berfet_DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Bill_Berfet_DGV.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Bill_Berfet_DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Bill_Berfet_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Bill_Berfet_DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn32, Me.Column3, Me.DataGridViewTextBoxColumn34, Me.DataGridViewTextBoxColumn35, Me.Bill_Total_CL, Me.Bill_Berfet_CL})
        Me.Bill_Berfet_DGV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Bill_Berfet_DGV.DefaultCellStyle = DataGridViewCellStyle7
        Me.Bill_Berfet_DGV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Bill_Berfet_DGV.Location = New System.Drawing.Point(1, 43)
        Me.Bill_Berfet_DGV.MultiSelect = False
        Me.Bill_Berfet_DGV.Name = "Bill_Berfet_DGV"
        Me.Bill_Berfet_DGV.ReadOnly = True
        Me.Bill_Berfet_DGV.RowHeadersVisible = False
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bill_Berfet_DGV.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.Bill_Berfet_DGV.RowTemplate.Height = 35
        Me.Bill_Berfet_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Bill_Berfet_DGV.Size = New System.Drawing.Size(893, 575)
        Me.Bill_Berfet_DGV.TabIndex = 635
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Date"
        Me.DataGridViewTextBoxColumn1.HeaderText = "التاريخ"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 100
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn32
        '
        Me.DataGridViewTextBoxColumn32.DataPropertyName = "SB_ID"
        Me.DataGridViewTextBoxColumn32.HeaderText = "رقم الفاتـــورة"
        Me.DataGridViewTextBoxColumn32.Name = "DataGridViewTextBoxColumn32"
        Me.DataGridViewTextBoxColumn32.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "Ag_name"
        Me.Column3.HeaderText = "العميل"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn34
        '
        Me.DataGridViewTextBoxColumn34.DataPropertyName = "Total"
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.DataGridViewTextBoxColumn34.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn34.HeaderText = "الإجمالـــي"
        Me.DataGridViewTextBoxColumn34.Name = "DataGridViewTextBoxColumn34"
        Me.DataGridViewTextBoxColumn34.ReadOnly = True
        '
        'DataGridViewTextBoxColumn35
        '
        Me.DataGridViewTextBoxColumn35.DataPropertyName = "Discount"
        DataGridViewCellStyle4.Format = "N2"
        Me.DataGridViewTextBoxColumn35.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn35.HeaderText = "التخفيـــض"
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        Me.DataGridViewTextBoxColumn35.ReadOnly = True
        '
        'Bill_Total_CL
        '
        Me.Bill_Total_CL.DataPropertyName = "Pure"
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Bill_Total_CL.DefaultCellStyle = DataGridViewCellStyle5
        Me.Bill_Total_CL.HeaderText = "الصافــي"
        Me.Bill_Total_CL.Name = "Bill_Total_CL"
        Me.Bill_Total_CL.ReadOnly = True
        '
        'Bill_Berfet_CL
        '
        Me.Bill_Berfet_CL.DataPropertyName = "Bill_Berfet"
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.DarkGreen
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.DarkGreen
        Me.Bill_Berfet_CL.DefaultCellStyle = DataGridViewCellStyle6
        Me.Bill_Berfet_CL.HeaderText = "ربح الفاتورة"
        Me.Bill_Berfet_CL.Name = "Bill_Berfet_CL"
        Me.Bill_Berfet_CL.ReadOnly = True
        '
        'B_berfet_btn
        '
        Me.B_berfet_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.B_berfet_btn.BackColor = System.Drawing.Color.White
        Me.B_berfet_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.B_berfet_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B_berfet_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.B_berfet_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_berfet_btn.ForeColor = System.Drawing.Color.Black
        Me.B_berfet_btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.B_berfet_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.B_berfet_btn.Location = New System.Drawing.Point(2, 3)
        Me.B_berfet_btn.Name = "B_berfet_btn"
        Me.B_berfet_btn.Size = New System.Drawing.Size(133, 38)
        Me.B_berfet_btn.TabIndex = 633
        Me.B_berfet_btn.Text = "بحث"
        Me.B_berfet_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.B_berfet_btn.UseVisualStyleBackColor = False
        '
        'B_Berfet_Print_btn
        '
        Me.B_Berfet_Print_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.B_Berfet_Print_btn.BackColor = System.Drawing.Color.White
        Me.B_Berfet_Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B_Berfet_Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.B_Berfet_Print_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_Berfet_Print_btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.B_Berfet_Print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.B_Berfet_Print_btn.Location = New System.Drawing.Point(136, 3)
        Me.B_Berfet_Print_btn.Name = "B_Berfet_Print_btn"
        Me.B_Berfet_Print_btn.Size = New System.Drawing.Size(133, 38)
        Me.B_Berfet_Print_btn.TabIndex = 634
        Me.B_Berfet_Print_btn.Text = "طباعة"
        Me.B_Berfet_Print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.B_Berfet_Print_btn.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.B_Berfet_T_txt)
        Me.Panel4.Controls.Add(Me.Label64)
        Me.Panel4.Controls.Add(Me.B_BerfetCounter_txt)
        Me.Panel4.Controls.Add(Me.B_SB_T_txt)
        Me.Panel4.Controls.Add(Me.Label76)
        Me.Panel4.Controls.Add(Me.Label52)
        Me.Panel4.Location = New System.Drawing.Point(1, 620)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(893, 38)
        Me.Panel4.TabIndex = 661
        '
        'B_Berfet_T_txt
        '
        Me.B_Berfet_T_txt.BackColor = System.Drawing.SystemColors.Info
        Me.B_Berfet_T_txt.Font = New System.Drawing.Font("Stencil", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_Berfet_T_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.B_Berfet_T_txt.Location = New System.Drawing.Point(3, 3)
        Me.B_Berfet_T_txt.Name = "B_Berfet_T_txt"
        Me.B_Berfet_T_txt.ReadOnly = True
        Me.B_Berfet_T_txt.Size = New System.Drawing.Size(154, 32)
        Me.B_Berfet_T_txt.TabIndex = 653
        Me.B_Berfet_T_txt.Text = "0.00"
        Me.B_Berfet_T_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label64.Location = New System.Drawing.Point(505, 8)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(114, 21)
        Me.Label64.TabIndex = 659
        Me.Label64.Text = "إجمالي المبيعات"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'B_BerfetCounter_txt
        '
        Me.B_BerfetCounter_txt.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_BerfetCounter_txt.ForeColor = System.Drawing.Color.Black
        Me.B_BerfetCounter_txt.Location = New System.Drawing.Point(678, 4)
        Me.B_BerfetCounter_txt.Name = "B_BerfetCounter_txt"
        Me.B_BerfetCounter_txt.ReadOnly = True
        Me.B_BerfetCounter_txt.Size = New System.Drawing.Size(101, 32)
        Me.B_BerfetCounter_txt.TabIndex = 651
        Me.B_BerfetCounter_txt.Text = "0.00"
        Me.B_BerfetCounter_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'B_SB_T_txt
        '
        Me.B_SB_T_txt.BackColor = System.Drawing.SystemColors.Info
        Me.B_SB_T_txt.Font = New System.Drawing.Font("Stencil", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_SB_T_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.B_SB_T_txt.Location = New System.Drawing.Point(338, 3)
        Me.B_SB_T_txt.Name = "B_SB_T_txt"
        Me.B_SB_T_txt.ReadOnly = True
        Me.B_SB_T_txt.Size = New System.Drawing.Size(163, 32)
        Me.B_SB_T_txt.TabIndex = 658
        Me.B_SB_T_txt.Text = "0.00"
        Me.B_SB_T_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label76.Location = New System.Drawing.Point(784, 10)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(85, 21)
        Me.Label76.TabIndex = 652
        Me.Label76.Text = "عدد الفواتير"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label52.Location = New System.Drawing.Point(161, 8)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(88, 21)
        Me.Label52.TabIndex = 654
        Me.Label52.Text = "إجمالي الربح"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SB_Reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Bill_Berfet_DGV)
        Me.Controls.Add(Me.B_berfet_btn)
        Me.Controls.Add(Me.B_Berfet_Print_btn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SB_Reports"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الهيكل التنظيمي"
        CType(Me.Bill_Berfet_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Bill_Berfet_DGV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bill_Total_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bill_Berfet_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents B_berfet_btn As System.Windows.Forms.Button
    Friend WithEvents B_Berfet_Print_btn As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents B_Berfet_T_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents B_BerfetCounter_txt As System.Windows.Forms.TextBox
    Friend WithEvents B_SB_T_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
End Class
