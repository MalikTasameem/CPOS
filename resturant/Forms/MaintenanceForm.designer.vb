<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MaintenanceForm
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.M_1_btn = New System.Windows.Forms.Button()
        Me.M_2_btn = New System.Windows.Forms.Button()
        Me.M_3_btn = New System.Windows.Forms.Button()
        Me.M_4_btn = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Rank_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(763, 380)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(104, 38)
        Me.ExitFormButton.TabIndex = 670
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'M_1_btn
        '
        Me.M_1_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.M_1_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.M_1_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.M_1_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.M_1_btn.ForeColor = System.Drawing.Color.Black
        Me.M_1_btn.Location = New System.Drawing.Point(2, 1)
        Me.M_1_btn.Name = "M_1_btn"
        Me.M_1_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.M_1_btn.Size = New System.Drawing.Size(357, 55)
        Me.M_1_btn.TabIndex = 671
        Me.M_1_btn.Text = "صيانة النظــــام 1 - DataBase Shrink"
        Me.M_1_btn.UseVisualStyleBackColor = False
        '
        'M_2_btn
        '
        Me.M_2_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.M_2_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.M_2_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.M_2_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.M_2_btn.ForeColor = System.Drawing.Color.Black
        Me.M_2_btn.Location = New System.Drawing.Point(2, 61)
        Me.M_2_btn.Name = "M_2_btn"
        Me.M_2_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.M_2_btn.Size = New System.Drawing.Size(357, 55)
        Me.M_2_btn.TabIndex = 672
        Me.M_2_btn.Text = "صيانة النظــــام 2 - DataBase Suspend"
        Me.M_2_btn.UseVisualStyleBackColor = False
        '
        'M_3_btn
        '
        Me.M_3_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.M_3_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.M_3_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.M_3_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.M_3_btn.ForeColor = System.Drawing.Color.Black
        Me.M_3_btn.Location = New System.Drawing.Point(2, 121)
        Me.M_3_btn.Name = "M_3_btn"
        Me.M_3_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.M_3_btn.Size = New System.Drawing.Size(357, 55)
        Me.M_3_btn.TabIndex = 673
        Me.M_3_btn.Text = "صيانة النظــــام 3 - Recover App Settings"
        Me.M_3_btn.UseVisualStyleBackColor = False
        '
        'M_4_btn
        '
        Me.M_4_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.M_4_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.M_4_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.M_4_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.M_4_btn.ForeColor = System.Drawing.Color.Black
        Me.M_4_btn.Location = New System.Drawing.Point(2, 181)
        Me.M_4_btn.Name = "M_4_btn"
        Me.M_4_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.M_4_btn.Size = New System.Drawing.Size(357, 55)
        Me.M_4_btn.TabIndex = 674
        Me.M_4_btn.Text = "صيانة النظــــام 4 - Start SQL SERVER Instance"
        Me.M_4_btn.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(4, 262)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(180, 27)
        Me.TextBox1.TabIndex = 675
        Me.TextBox1.Text = "net start MSSQLSERVER"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(12, 240)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(345, 19)
        Me.Label1.TabIndex = 676
        Me.Label1.Text = "قم بنسخ الجملة في شاشة الأوامر واضغط ENTER"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(187, 267)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(65, 19)
        Me.Label2.TabIndex = 677
        Me.Label2.Text = "الجملة :"
        '
        'DataGridViewX
        '
        Me.DataGridViewX.AllowUserToAddRows = False
        Me.DataGridViewX.AllowUserToDeleteRows = False
        Me.DataGridViewX.BackgroundColor = System.Drawing.Color.White
        Me.DataGridViewX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.Rank_CL, Me.DataGridViewTextBoxColumn4, Me.Column1, Me.Column2})
        Me.DataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX.Location = New System.Drawing.Point(361, 1)
        Me.DataGridViewX.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridViewX.MultiSelect = False
        Me.DataGridViewX.Name = "DataGridViewX"
        Me.DataGridViewX.ReadOnly = True
        Me.DataGridViewX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DataGridViewX.RowHeadersVisible = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewX.RowTemplate.Height = 35
        Me.DataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewX.Size = New System.Drawing.Size(506, 258)
        Me.DataGridViewX.TabIndex = 678
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "NAME"
        Me.DataGridViewTextBoxColumn3.HeaderText = "db_name"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'Rank_CL
        '
        Me.Rank_CL.DataPropertyName = "state"
        Me.Rank_CL.HeaderText = "state"
        Me.Rank_CL.Name = "Rank_CL"
        Me.Rank_CL.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "state_desc"
        Me.DataGridViewTextBoxColumn4.HeaderText = "state_desc"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "user_access_desc"
        Me.Column1.HeaderText = "user_access_desc"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "user_access"
        Me.Column2.HeaderText = "user_access"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(2, 295)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(357, 55)
        Me.Button1.TabIndex = 679
        Me.Button1.Text = "صيانة النظــــام 5 - SQL Query"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'MaintenanceForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 418)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridViewX)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.M_4_btn)
        Me.Controls.Add(Me.M_3_btn)
        Me.Controls.Add(Me.M_2_btn)
        Me.Controls.Add(Me.M_1_btn)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MaintenanceForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "مركز الصيانة"
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents M_1_btn As System.Windows.Forms.Button
    Friend WithEvents M_2_btn As System.Windows.Forms.Button
    Friend WithEvents M_3_btn As System.Windows.Forms.Button
    Friend WithEvents M_4_btn As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents DataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rank_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
