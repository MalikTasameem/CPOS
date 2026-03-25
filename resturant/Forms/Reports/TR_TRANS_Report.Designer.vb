<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TR_TRANS_Report
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
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.gridv = New Zuby.ADGV.AdvancedDataGridView()
        Me.InSale_Search_btn = New System.Windows.Forms.Button()
        Me.InSale_print_btn = New System.Windows.Forms.Button()
        Me.DataB = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.gridv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.HorizontalScrollbar = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(2, 68)
        Me.CheckedListBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckedListBox1.Size = New System.Drawing.Size(891, 49)
        Me.CheckedListBox1.TabIndex = 894
        Me.CheckedListBox1.Visible = False
        '
        'gridv
        '
        Me.gridv.AllowUserToAddRows = False
        Me.gridv.AllowUserToDeleteRows = False
        Me.gridv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.gridv.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.gridv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridv.FilterAndSortEnabled = True
        Me.gridv.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.gridv.Location = New System.Drawing.Point(1, 118)
        Me.gridv.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.gridv.Name = "gridv"
        Me.gridv.ReadOnly = True
        Me.gridv.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.gridv.RowTemplate.Height = 35
        Me.gridv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridv.Size = New System.Drawing.Size(892, 538)
        Me.gridv.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.gridv.TabIndex = 893
        '
        'InSale_Search_btn
        '
        Me.InSale_Search_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InSale_Search_btn.BackColor = System.Drawing.Color.White
        Me.InSale_Search_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.InSale_Search_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InSale_Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.InSale_Search_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 16.0!, System.Drawing.FontStyle.Bold)
        Me.InSale_Search_btn.ForeColor = System.Drawing.Color.Black
        Me.InSale_Search_btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.InSale_Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InSale_Search_btn.Location = New System.Drawing.Point(801, 7)
        Me.InSale_Search_btn.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.InSale_Search_btn.Name = "InSale_Search_btn"
        Me.InSale_Search_btn.Size = New System.Drawing.Size(92, 60)
        Me.InSale_Search_btn.TabIndex = 892
        Me.InSale_Search_btn.Text = "بحث"
        Me.InSale_Search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InSale_Search_btn.UseVisualStyleBackColor = False
        '
        'InSale_print_btn
        '
        Me.InSale_print_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InSale_print_btn.BackColor = System.Drawing.Color.White
        Me.InSale_print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InSale_print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.InSale_print_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 16.0!, System.Drawing.FontStyle.Bold)
        Me.InSale_print_btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.InSale_print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InSale_print_btn.Location = New System.Drawing.Point(692, 7)
        Me.InSale_print_btn.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.InSale_print_btn.Name = "InSale_print_btn"
        Me.InSale_print_btn.Size = New System.Drawing.Size(107, 60)
        Me.InSale_print_btn.TabIndex = 891
        Me.InSale_print_btn.Text = "طباعة"
        Me.InSale_print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InSale_print_btn.UseVisualStyleBackColor = False
        '
        'TR_TRANS_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.gridv)
        Me.Controls.Add(Me.InSale_Search_btn)
        Me.Controls.Add(Me.InSale_print_btn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "TR_TRANS_Report"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TR_TRANS_Report"
        CType(Me.gridv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents gridv As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents InSale_Search_btn As Button
    Friend WithEvents InSale_print_btn As Button
    Friend WithEvents DataB As BindingSource
End Class
