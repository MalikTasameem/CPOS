<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Exp_Static
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Exp_Static))
        Me.InsertU_btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.IM_GV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.EX_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EX_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.is_Executed_CL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DATE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COST_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DEF_AGE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Current_PriceCL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.R_T_Cost_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.T_TextBox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Exp_Name_txt = New System.Windows.Forms.TextBox()
        Me.Print_btn = New System.Windows.Forms.Button()
        Me.T_QTY_txt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.IM_GV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'InsertU_btn
        '
        Me.InsertU_btn.BackColor = System.Drawing.Color.White
        Me.InsertU_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.InsertU_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InsertU_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.InsertU_btn.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.InsertU_btn.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.InsertU_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InsertU_btn.Location = New System.Drawing.Point(735, 651)
        Me.InsertU_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.InsertU_btn.Name = "InsertU_btn"
        Me.InsertU_btn.Size = New System.Drawing.Size(124, 44)
        Me.InsertU_btn.TabIndex = 457
        Me.InsertU_btn.Text = "إضافــة"
        Me.InsertU_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InsertU_btn.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(860, 651)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(140, 44)
        Me.ExitFormButton.TabIndex = 672
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "رجــــوع"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'IM_GV
        '
        Me.IM_GV.AllowUserToAddRows = False
        Me.IM_GV.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray
        Me.IM_GV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.IM_GV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IM_GV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.IM_GV.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IM_GV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.IM_GV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IM_GV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EX_ID_CL, Me.EX_NAME_CL, Me.is_Executed_CL, Me.DATE_CL, Me.QTY_CL, Me.COST_CL, Me.DEF_AGE_CL, Me.Current_PriceCL, Me.R_T_Cost_CL})
        Me.IM_GV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IM_GV.DefaultCellStyle = DataGridViewCellStyle5
        Me.IM_GV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IM_GV.Location = New System.Drawing.Point(2, 36)
        Me.IM_GV.Name = "IM_GV"
        Me.IM_GV.ReadOnly = True
        Me.IM_GV.RowHeadersVisible = False
        DataGridViewCellStyle6.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_GV.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.IM_GV.RowTemplate.Height = 35
        Me.IM_GV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IM_GV.Size = New System.Drawing.Size(999, 609)
        Me.IM_GV.TabIndex = 679
        '
        'EX_ID_CL
        '
        Me.EX_ID_CL.DataPropertyName = "EX_ID"
        Me.EX_ID_CL.HeaderText = "T_ID"
        Me.EX_ID_CL.Name = "EX_ID_CL"
        Me.EX_ID_CL.ReadOnly = True
        Me.EX_ID_CL.Visible = False
        '
        'EX_NAME_CL
        '
        Me.EX_NAME_CL.DataPropertyName = "EX_NAME"
        Me.EX_NAME_CL.FillWeight = 138.2133!
        Me.EX_NAME_CL.HeaderText = "المصروف"
        Me.EX_NAME_CL.Name = "EX_NAME_CL"
        Me.EX_NAME_CL.ReadOnly = True
        '
        'is_Executed_CL
        '
        Me.is_Executed_CL.DataPropertyName = "is_Executed"
        Me.is_Executed_CL.FillWeight = 35.67497!
        Me.is_Executed_CL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.is_Executed_CL.HeaderText = "إستهلاكي"
        Me.is_Executed_CL.Name = "is_Executed_CL"
        Me.is_Executed_CL.ReadOnly = True
        Me.is_Executed_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.is_Executed_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DATE_CL
        '
        Me.DATE_CL.DataPropertyName = "DATE"
        Me.DATE_CL.HeaderText = "تاريخ الإدخال"
        Me.DATE_CL.Name = "DATE_CL"
        Me.DATE_CL.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        Me.QTY_CL.FillWeight = 40.0!
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'COST_CL
        '
        Me.COST_CL.DataPropertyName = "COST"
        Me.COST_CL.FillWeight = 39.95293!
        Me.COST_CL.HeaderText = "سعر الشراء"
        Me.COST_CL.Name = "COST_CL"
        Me.COST_CL.ReadOnly = True
        '
        'DEF_AGE_CL
        '
        Me.DEF_AGE_CL.DataPropertyName = "DEF_AGE"
        Me.DEF_AGE_CL.FillWeight = 26.79966!
        Me.DEF_AGE_CL.HeaderText = "العمر بالسنة"
        Me.DEF_AGE_CL.Name = "DEF_AGE_CL"
        Me.DEF_AGE_CL.ReadOnly = True
        '
        'Current_PriceCL
        '
        Me.Current_PriceCL.DataPropertyName = "R_Cost"
        DataGridViewCellStyle3.Format = "N3"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Current_PriceCL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Current_PriceCL.FillWeight = 35.0!
        Me.Current_PriceCL.HeaderText = "التكلفة الحالية"
        Me.Current_PriceCL.Name = "Current_PriceCL"
        Me.Current_PriceCL.ReadOnly = True
        '
        'R_T_Cost_CL
        '
        Me.R_T_Cost_CL.DataPropertyName = "R_T_Cost"
        DataGridViewCellStyle4.Format = "N3"
        Me.R_T_Cost_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.R_T_Cost_CL.FillWeight = 40.0!
        Me.R_T_Cost_CL.HeaderText = "إجمالي التكلفة"
        Me.R_T_Cost_CL.Name = "R_T_Cost_CL"
        Me.R_T_Cost_CL.ReadOnly = True
        '
        'T_TextBox
        '
        Me.T_TextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.T_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.T_TextBox.Font = New System.Drawing.Font("Stencil", 16.7!)
        Me.T_TextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.T_TextBox.Location = New System.Drawing.Point(1, 657)
        Me.T_TextBox.MaxLength = 250
        Me.T_TextBox.Name = "T_TextBox"
        Me.T_TextBox.ReadOnly = True
        Me.T_TextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.T_TextBox.Size = New System.Drawing.Size(148, 34)
        Me.T_TextBox.TabIndex = 680
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(153, 661)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 28)
        Me.Label7.TabIndex = 681
        Me.Label7.Text = "الإجمالي"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Exp_Name_txt
        '
        Me.Exp_Name_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Exp_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Exp_Name_txt.Font = New System.Drawing.Font("JF Flat", 11.0!)
        Me.Exp_Name_txt.ForeColor = System.Drawing.Color.Black
        Me.Exp_Name_txt.Location = New System.Drawing.Point(1, 2)
        Me.Exp_Name_txt.MaxLength = 250
        Me.Exp_Name_txt.Name = "Exp_Name_txt"
        Me.Exp_Name_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Exp_Name_txt.Size = New System.Drawing.Size(1000, 33)
        Me.Exp_Name_txt.TabIndex = 687
        Me.Exp_Name_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Print_btn
        '
        Me.Print_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_btn.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Print_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Print_btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.Print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_btn.Location = New System.Drawing.Point(608, 651)
        Me.Print_btn.Name = "Print_btn"
        Me.Print_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_btn.Size = New System.Drawing.Size(124, 44)
        Me.Print_btn.TabIndex = 689
        Me.Print_btn.TabStop = False
        Me.Print_btn.Text = "طباعة"
        Me.Print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_btn.UseVisualStyleBackColor = False
        '
        'T_QTY_txt
        '
        Me.T_QTY_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.T_QTY_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.T_QTY_txt.Font = New System.Drawing.Font("Times New Roman", 17.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.T_QTY_txt.ForeColor = System.Drawing.Color.Black
        Me.T_QTY_txt.Location = New System.Drawing.Point(252, 657)
        Me.T_QTY_txt.MaxLength = 250
        Me.T_QTY_txt.Name = "T_QTY_txt"
        Me.T_QTY_txt.ReadOnly = True
        Me.T_QTY_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.T_QTY_txt.Size = New System.Drawing.Size(148, 34)
        Me.T_QTY_txt.TabIndex = 690
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(404, 660)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 28)
        Me.Label2.TabIndex = 691
        Me.Label2.Text = "الكمية"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Exp_Static
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.T_QTY_txt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Print_btn)
        Me.Controls.Add(Me.Exp_Name_txt)
        Me.Controls.Add(Me.T_TextBox)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.IM_GV)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.InsertU_btn)
        Me.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Exp_Static"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "المصروفات التابثة (الأصول)"
        CType(Me.IM_GV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents InsertU_btn As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents IM_GV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents T_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Exp_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Print_btn As System.Windows.Forms.Button
    Friend WithEvents T_QTY_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents EX_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EX_NAME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents is_Executed_CL As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DATE_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COST_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DEF_AGE_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Current_PriceCL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents R_T_Cost_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
