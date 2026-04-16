<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Exp_Static
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Exp_Static))
        Me.TitleBarPanel = New System.Windows.Forms.Panel()
        Me.btnMinimize = New System.Windows.Forms.Button()
        Me.btnMaximize = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.InsertU_btn = New System.Windows.Forms.Button()
        Me.Print_btn = New System.Windows.Forms.Button()
        Me.IM_GV = New System.Windows.Forms.DataGridView()
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
        Me.T_QTY_txt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TitleBarPanel.SuspendLayout()
        CType(Me.IM_GV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleBarPanel
        '
        Me.TitleBarPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBarPanel.Controls.Add(Me.btnMinimize)
        Me.TitleBarPanel.Controls.Add(Me.btnMaximize)
        Me.TitleBarPanel.Controls.Add(Me.btnClose)
        Me.TitleBarPanel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBarPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBarPanel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBarPanel.Name = "TitleBarPanel"
        Me.TitleBarPanel.Size = New System.Drawing.Size(1004, 40)
        Me.TitleBarPanel.TabIndex = 999
        Me.TitleBarPanel.Tag = "HEADER"
        '
        'btnMinimize
        '
        Me.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMinimize.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnMinimize.FlatAppearance.BorderSize = 0
        Me.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMinimize.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.btnMinimize.ForeColor = System.Drawing.Color.White
        Me.btnMinimize.Location = New System.Drawing.Point(90, 0)
        Me.btnMinimize.Name = "btnMinimize"
        Me.btnMinimize.Size = New System.Drawing.Size(45, 40)
        Me.btnMinimize.TabIndex = 1
        Me.btnMinimize.Text = "ـ"
        Me.btnMinimize.UseVisualStyleBackColor = False
        '
        'btnMaximize
        '
        Me.btnMaximize.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMaximize.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnMaximize.FlatAppearance.BorderSize = 0
        Me.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMaximize.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.btnMaximize.ForeColor = System.Drawing.Color.White
        Me.btnMaximize.Location = New System.Drawing.Point(45, 0)
        Me.btnMaximize.Name = "btnMaximize"
        Me.btnMaximize.Size = New System.Drawing.Size(45, 40)
        Me.btnMaximize.TabIndex = 2
        Me.btnMaximize.Text = "⬜"
        Me.btnMaximize.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(0, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(45, 40)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "X"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'TopTitle_LB
        '
        Me.TopTitle_LB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TopTitle_LB.AutoSize = True
        Me.TopTitle_LB.ForeColor = System.Drawing.Color.White
        Me.TopTitle_LB.Location = New System.Drawing.Point(750, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(188, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "المصروفات التابثة (الأصول)"
        '
        'InsertU_btn
        '
        Me.InsertU_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InsertU_btn.BackColor = System.Drawing.Color.White
        Me.InsertU_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InsertU_btn.FlatAppearance.BorderSize = 0
        Me.InsertU_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.InsertU_btn.Location = New System.Drawing.Point(733, 690)
        Me.InsertU_btn.Name = "InsertU_btn"
        Me.InsertU_btn.Size = New System.Drawing.Size(246, 38)
        Me.InsertU_btn.TabIndex = 457
        Me.InsertU_btn.Tag = "SAVE"
        Me.InsertU_btn.Text = "إضافــة"
        Me.InsertU_btn.UseVisualStyleBackColor = False
        '
        'Print_btn
        '
        Me.Print_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Print_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_btn.FlatAppearance.BorderSize = 0
        Me.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_btn.Location = New System.Drawing.Point(607, 690)
        Me.Print_btn.Name = "Print_btn"
        Me.Print_btn.Size = New System.Drawing.Size(120, 38)
        Me.Print_btn.TabIndex = 689
        Me.Print_btn.Tag = "PRINT"
        Me.Print_btn.Text = "طباعة"
        Me.Print_btn.UseVisualStyleBackColor = False
        '
        'IM_GV
        '
        Me.IM_GV.AllowUserToAddRows = False
        Me.IM_GV.AllowUserToDeleteRows = False
        Me.IM_GV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IM_GV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IM_GV.BackgroundColor = System.Drawing.Color.White
        Me.IM_GV.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.IM_GV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IM_GV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EX_ID_CL, Me.EX_NAME_CL, Me.is_Executed_CL, Me.DATE_CL, Me.QTY_CL, Me.COST_CL, Me.DEF_AGE_CL, Me.Current_PriceCL, Me.R_T_Cost_CL})
        Me.IM_GV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_GV.Location = New System.Drawing.Point(4, 78)
        Me.IM_GV.Name = "IM_GV"
        Me.IM_GV.ReadOnly = True
        Me.IM_GV.RowHeadersVisible = False
        Me.IM_GV.RowTemplate.Height = 35
        Me.IM_GV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IM_GV.Size = New System.Drawing.Size(996, 606)
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
        Me.EX_NAME_CL.HeaderText = "المصروف"
        Me.EX_NAME_CL.Name = "EX_NAME_CL"
        Me.EX_NAME_CL.ReadOnly = True
        '
        'is_Executed_CL
        '
        Me.is_Executed_CL.DataPropertyName = "is_Executed"
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
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'COST_CL
        '
        Me.COST_CL.DataPropertyName = "COST"
        Me.COST_CL.HeaderText = "سعر الشراء"
        Me.COST_CL.Name = "COST_CL"
        Me.COST_CL.ReadOnly = True
        '
        'DEF_AGE_CL
        '
        Me.DEF_AGE_CL.DataPropertyName = "DEF_AGE"
        Me.DEF_AGE_CL.HeaderText = "العمر بالسنة"
        Me.DEF_AGE_CL.Name = "DEF_AGE_CL"
        Me.DEF_AGE_CL.ReadOnly = True
        '
        'Current_PriceCL
        '
        Me.Current_PriceCL.DataPropertyName = "R_Cost"
        DataGridViewCellStyle1.Format = "N3"
        Me.Current_PriceCL.DefaultCellStyle = DataGridViewCellStyle1
        Me.Current_PriceCL.HeaderText = "التكلفة الحالية"
        Me.Current_PriceCL.Name = "Current_PriceCL"
        Me.Current_PriceCL.ReadOnly = True
        '
        'R_T_Cost_CL
        '
        Me.R_T_Cost_CL.DataPropertyName = "R_T_Cost"
        DataGridViewCellStyle2.Format = "N3"
        Me.R_T_Cost_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.R_T_Cost_CL.HeaderText = "إجمالي التكلفة"
        Me.R_T_Cost_CL.Name = "R_T_Cost_CL"
        Me.R_T_Cost_CL.ReadOnly = True
        '
        'T_TextBox
        '
        Me.T_TextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.T_TextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.T_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.T_TextBox.Location = New System.Drawing.Point(4, 696)
        Me.T_TextBox.Name = "T_TextBox"
        Me.T_TextBox.ReadOnly = True
        Me.T_TextBox.Size = New System.Drawing.Size(148, 29)
        Me.T_TextBox.TabIndex = 680
        Me.T_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(155, 699)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 21)
        Me.Label7.TabIndex = 681
        Me.Label7.Text = "الإجمالي"
        '
        'Exp_Name_txt
        '
        Me.Exp_Name_txt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Exp_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Exp_Name_txt.Location = New System.Drawing.Point(4, 44)
        Me.Exp_Name_txt.MaxLength = 250
        Me.Exp_Name_txt.Name = "Exp_Name_txt"
        Me.Exp_Name_txt.Size = New System.Drawing.Size(996, 29)
        Me.Exp_Name_txt.TabIndex = 687
        Me.Exp_Name_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'T_QTY_txt
        '
        Me.T_QTY_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.T_QTY_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.T_QTY_txt.Location = New System.Drawing.Point(235, 696)
        Me.T_QTY_txt.Name = "T_QTY_txt"
        Me.T_QTY_txt.ReadOnly = True
        Me.T_QTY_txt.Size = New System.Drawing.Size(148, 29)
        Me.T_QTY_txt.TabIndex = 690
        Me.T_QTY_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(389, 699)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 21)
        Me.Label2.TabIndex = 691
        Me.Label2.Text = "الكمية"
        '
        'Exp_Static
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 735)
        Me.Controls.Add(Me.TitleBarPanel)
        Me.Controls.Add(Me.T_QTY_txt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Print_btn)
        Me.Controls.Add(Me.Exp_Name_txt)
        Me.Controls.Add(Me.T_TextBox)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.IM_GV)
        Me.Controls.Add(Me.InsertU_btn)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Exp_Static"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "المصروفات التابثة (الأصول)"
        Me.TitleBarPanel.ResumeLayout(False)
        Me.TitleBarPanel.PerformLayout()
        CType(Me.IM_GV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleBarPanel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnMaximize As System.Windows.Forms.Button
    Friend WithEvents btnMinimize As System.Windows.Forms.Button

    Friend WithEvents InsertU_btn As System.Windows.Forms.Button
    Friend WithEvents Print_btn As System.Windows.Forms.Button
    Friend WithEvents IM_GV As System.Windows.Forms.DataGridView
    Friend WithEvents T_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Exp_Name_txt As System.Windows.Forms.TextBox
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