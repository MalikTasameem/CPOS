<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Notes_Report
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Notes_Report))
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.NOTES_M_TEXT = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.NOTES_QTY_TXT = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.NOTES_Grid = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.Comp_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COM_QYT_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COM_TOTAL_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Notes_btn = New System.Windows.Forms.Button()
        Me.NOTES_Print_btn = New System.Windows.Forms.Button()
        Me.Panel12.SuspendLayout()
        CType(Me.NOTES_Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.NOTES_M_TEXT)
        Me.Panel12.Controls.Add(Me.Label77)
        Me.Panel12.Controls.Add(Me.NOTES_QTY_TXT)
        Me.Panel12.Controls.Add(Me.Label78)
        Me.Panel12.Location = New System.Drawing.Point(0, 622)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(894, 35)
        Me.Panel12.TabIndex = 666
        '
        'NOTES_M_TEXT
        '
        Me.NOTES_M_TEXT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NOTES_M_TEXT.Font = New System.Drawing.Font("Stencil", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NOTES_M_TEXT.ForeColor = System.Drawing.Color.Red
        Me.NOTES_M_TEXT.Location = New System.Drawing.Point(215, 3)
        Me.NOTES_M_TEXT.Name = "NOTES_M_TEXT"
        Me.NOTES_M_TEXT.ReadOnly = True
        Me.NOTES_M_TEXT.Size = New System.Drawing.Size(126, 30)
        Me.NOTES_M_TEXT.TabIndex = 417
        Me.NOTES_M_TEXT.Text = "0.00"
        Me.NOTES_M_TEXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label77
        '
        Me.Label77.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label77.AutoSize = True
        Me.Label77.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label77.Location = New System.Drawing.Point(345, 9)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(97, 21)
        Me.Label77.TabIndex = 419
        Me.Label77.Text = "إجمالي القيمة"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NOTES_QTY_TXT
        '
        Me.NOTES_QTY_TXT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NOTES_QTY_TXT.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NOTES_QTY_TXT.ForeColor = System.Drawing.Color.Red
        Me.NOTES_QTY_TXT.Location = New System.Drawing.Point(495, 3)
        Me.NOTES_QTY_TXT.Name = "NOTES_QTY_TXT"
        Me.NOTES_QTY_TXT.ReadOnly = True
        Me.NOTES_QTY_TXT.Size = New System.Drawing.Size(126, 30)
        Me.NOTES_QTY_TXT.TabIndex = 416
        Me.NOTES_QTY_TXT.Text = "0.00"
        Me.NOTES_QTY_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label78
        '
        Me.Label78.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label78.AutoSize = True
        Me.Label78.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label78.Location = New System.Drawing.Point(625, 8)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(108, 21)
        Me.Label78.TabIndex = 418
        Me.Label78.Text = "إجمالي الكميات"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NOTES_Grid
        '
        Me.NOTES_Grid.AllowUserToAddRows = False
        Me.NOTES_Grid.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
        Me.NOTES_Grid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.NOTES_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.NOTES_Grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.NOTES_Grid.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NOTES_Grid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.NOTES_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.NOTES_Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Comp_Name_CL, Me.COM_QYT_CL, Me.COM_TOTAL_CL})
        Me.NOTES_Grid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.NOTES_Grid.DefaultCellStyle = DataGridViewCellStyle4
        Me.NOTES_Grid.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.NOTES_Grid.Location = New System.Drawing.Point(1, 41)
        Me.NOTES_Grid.Name = "NOTES_Grid"
        Me.NOTES_Grid.ReadOnly = True
        Me.NOTES_Grid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NOTES_Grid.RowHeadersVisible = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NOTES_Grid.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.NOTES_Grid.RowTemplate.Height = 32
        Me.NOTES_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.NOTES_Grid.Size = New System.Drawing.Size(893, 578)
        Me.NOTES_Grid.TabIndex = 663
        '
        'Comp_Name_CL
        '
        Me.Comp_Name_CL.DataPropertyName = "Comp_Name"
        Me.Comp_Name_CL.FillWeight = 5.836273!
        Me.Comp_Name_CL.HeaderText = "الملاحظــة"
        Me.Comp_Name_CL.Name = "Comp_Name_CL"
        Me.Comp_Name_CL.ReadOnly = True
        '
        'COM_QYT_CL
        '
        Me.COM_QYT_CL.DataPropertyName = "QYT"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.COM_QYT_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.COM_QYT_CL.FillWeight = 2.918137!
        Me.COM_QYT_CL.HeaderText = "الكمية"
        Me.COM_QYT_CL.Name = "COM_QYT_CL"
        Me.COM_QYT_CL.ReadOnly = True
        '
        'COM_TOTAL_CL
        '
        Me.COM_TOTAL_CL.DataPropertyName = "Total"
        Me.COM_TOTAL_CL.FillWeight = 3.0!
        Me.COM_TOTAL_CL.HeaderText = "الإجمالي"
        Me.COM_TOTAL_CL.Name = "COM_TOTAL_CL"
        Me.COM_TOTAL_CL.ReadOnly = True
        '
        'Notes_btn
        '
        Me.Notes_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Notes_btn.BackColor = System.Drawing.Color.White
        Me.Notes_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Notes_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Notes_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Notes_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Notes_btn.ForeColor = System.Drawing.Color.Black
        Me.Notes_btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Notes_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Notes_btn.Location = New System.Drawing.Point(774, 1)
        Me.Notes_btn.Name = "Notes_btn"
        Me.Notes_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Notes_btn.Size = New System.Drawing.Size(120, 38)
        Me.Notes_btn.TabIndex = 665
        Me.Notes_btn.Text = "بحث"
        Me.Notes_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Notes_btn.UseVisualStyleBackColor = False
        '
        'NOTES_Print_btn
        '
        Me.NOTES_Print_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NOTES_Print_btn.BackColor = System.Drawing.Color.White
        Me.NOTES_Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NOTES_Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NOTES_Print_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NOTES_Print_btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.NOTES_Print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NOTES_Print_btn.Location = New System.Drawing.Point(651, 1)
        Me.NOTES_Print_btn.Name = "NOTES_Print_btn"
        Me.NOTES_Print_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NOTES_Print_btn.Size = New System.Drawing.Size(120, 38)
        Me.NOTES_Print_btn.TabIndex = 664
        Me.NOTES_Print_btn.Text = "طباعة"
        Me.NOTES_Print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NOTES_Print_btn.UseVisualStyleBackColor = False
        '
        'Empty_F
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel12)
        Me.Controls.Add(Me.NOTES_Grid)
        Me.Controls.Add(Me.Notes_btn)
        Me.Controls.Add(Me.NOTES_Print_btn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Empty_F"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        CType(Me.NOTES_Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents NOTES_M_TEXT As System.Windows.Forms.TextBox
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents NOTES_QTY_TXT As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents NOTES_Grid As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Comp_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COM_QYT_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COM_TOTAL_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Notes_btn As System.Windows.Forms.Button
    Friend WithEvents NOTES_Print_btn As System.Windows.Forms.Button
End Class
