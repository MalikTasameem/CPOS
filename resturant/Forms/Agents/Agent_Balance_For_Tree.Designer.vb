<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Agent_Balance_For_Tree
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Agent_Balance_For_Tree))
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.GENERAL_DataGridView = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TREE_CODE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AGENTS_DataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TR_DataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_DataGridView = New System.Windows.Forms.DataGridView()
        Me.ST_TXT = New System.Windows.Forms.TextBox()
        Me.TR_TXT = New System.Windows.Forms.TextBox()
        Me.AGENTS_TXT = New System.Windows.Forms.TextBox()
        Me.GENERAL_TXT = New System.Windows.Forms.TextBox()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.GENERAL_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AGENTS_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TR_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ST_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(2, 648)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(1001, 45)
        Me.ExitFormButton.TabIndex = 673
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "رجوع"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'GENERAL_DataGridView
        '
        Me.GENERAL_DataGridView.AllowUserToAddRows = False
        Me.GENERAL_DataGridView.AllowUserToDeleteRows = False
        Me.GENERAL_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GENERAL_DataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.GENERAL_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GENERAL_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.B_NAME_CL, Me.TREE_CODE_CL})
        Me.GENERAL_DataGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GENERAL_DataGridView.Location = New System.Drawing.Point(2, 29)
        Me.GENERAL_DataGridView.MultiSelect = False
        Me.GENERAL_DataGridView.Name = "GENERAL_DataGridView"
        Me.GENERAL_DataGridView.ReadOnly = True
        Me.GENERAL_DataGridView.RowHeadersVisible = False
        Me.GENERAL_DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.GENERAL_DataGridView.RowTemplate.Height = 30
        Me.GENERAL_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GENERAL_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GENERAL_DataGridView.Size = New System.Drawing.Size(337, 613)
        Me.GENERAL_DataGridView.TabIndex = 701
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'B_NAME_CL
        '
        Me.B_NAME_CL.DataPropertyName = "B_NAME"
        Me.B_NAME_CL.HeaderText = "الحســـاب"
        Me.B_NAME_CL.Name = "B_NAME_CL"
        Me.B_NAME_CL.ReadOnly = True
        '
        'TREE_CODE_CL
        '
        Me.TREE_CODE_CL.DataPropertyName = "TREE_CODE"
        Me.TREE_CODE_CL.HeaderText = "كود الحساب"
        Me.TREE_CODE_CL.Name = "TREE_CODE_CL"
        Me.TREE_CODE_CL.ReadOnly = True
        '
        'AGENTS_DataGridView
        '
        Me.AGENTS_DataGridView.AllowUserToAddRows = False
        Me.AGENTS_DataGridView.AllowUserToDeleteRows = False
        Me.AGENTS_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.AGENTS_DataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.AGENTS_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AGENTS_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.AGENTS_DataGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AGENTS_DataGridView.Location = New System.Drawing.Point(340, 29)
        Me.AGENTS_DataGridView.MultiSelect = False
        Me.AGENTS_DataGridView.Name = "AGENTS_DataGridView"
        Me.AGENTS_DataGridView.ReadOnly = True
        Me.AGENTS_DataGridView.RowHeadersVisible = False
        Me.AGENTS_DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.AGENTS_DataGridView.RowTemplate.Height = 30
        Me.AGENTS_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.AGENTS_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGENTS_DataGridView.Size = New System.Drawing.Size(386, 613)
        Me.AGENTS_DataGridView.TabIndex = 702
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "AG_ID"
        Me.DataGridViewTextBoxColumn1.HeaderText = "T_ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Ag_name"
        Me.DataGridViewTextBoxColumn2.HeaderText = "الحســـاب"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "TREE_CODE"
        Me.DataGridViewTextBoxColumn3.HeaderText = "كود الحساب"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'TR_DataGridView
        '
        Me.TR_DataGridView.AllowUserToAddRows = False
        Me.TR_DataGridView.AllowUserToDeleteRows = False
        Me.TR_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.TR_DataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.TR_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TR_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6})
        Me.TR_DataGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TR_DataGridView.Location = New System.Drawing.Point(728, 29)
        Me.TR_DataGridView.MultiSelect = False
        Me.TR_DataGridView.Name = "TR_DataGridView"
        Me.TR_DataGridView.ReadOnly = True
        Me.TR_DataGridView.RowHeadersVisible = False
        Me.TR_DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.TR_DataGridView.RowTemplate.Height = 30
        Me.TR_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TR_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.TR_DataGridView.Size = New System.Drawing.Size(275, 311)
        Me.TR_DataGridView.TabIndex = 703
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Tr_ID"
        Me.DataGridViewTextBoxColumn4.HeaderText = "T_ID"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Tr_Name"
        Me.DataGridViewTextBoxColumn5.HeaderText = "الحســـاب"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "TREE_CODE"
        Me.DataGridViewTextBoxColumn6.HeaderText = "كود الحساب"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'ST_DataGridView
        '
        Me.ST_DataGridView.AllowUserToAddRows = False
        Me.ST_DataGridView.AllowUserToDeleteRows = False
        Me.ST_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.ST_DataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.ST_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ST_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9})
        Me.ST_DataGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_DataGridView.Location = New System.Drawing.Point(728, 366)
        Me.ST_DataGridView.MultiSelect = False
        Me.ST_DataGridView.Name = "ST_DataGridView"
        Me.ST_DataGridView.ReadOnly = True
        Me.ST_DataGridView.RowHeadersVisible = False
        Me.ST_DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.ST_DataGridView.RowTemplate.Height = 30
        Me.ST_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ST_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ST_DataGridView.Size = New System.Drawing.Size(275, 276)
        Me.ST_DataGridView.TabIndex = 704
        '
        'ST_TXT
        '
        Me.ST_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ST_TXT.Location = New System.Drawing.Point(728, 342)
        Me.ST_TXT.Name = "ST_TXT"
        Me.ST_TXT.Size = New System.Drawing.Size(275, 23)
        Me.ST_TXT.TabIndex = 705
        '
        'TR_TXT
        '
        Me.TR_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TR_TXT.Location = New System.Drawing.Point(728, 5)
        Me.TR_TXT.Name = "TR_TXT"
        Me.TR_TXT.Size = New System.Drawing.Size(275, 23)
        Me.TR_TXT.TabIndex = 706
        '
        'AGENTS_TXT
        '
        Me.AGENTS_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AGENTS_TXT.Location = New System.Drawing.Point(340, 5)
        Me.AGENTS_TXT.Name = "AGENTS_TXT"
        Me.AGENTS_TXT.Size = New System.Drawing.Size(386, 23)
        Me.AGENTS_TXT.TabIndex = 707
        '
        'GENERAL_TXT
        '
        Me.GENERAL_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GENERAL_TXT.Location = New System.Drawing.Point(2, 5)
        Me.GENERAL_TXT.Name = "GENERAL_TXT"
        Me.GENERAL_TXT.Size = New System.Drawing.Size(337, 23)
        Me.GENERAL_TXT.TabIndex = 708
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "ST_ID"
        Me.DataGridViewTextBoxColumn7.HeaderText = "T_ID"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "ST_Name"
        Me.DataGridViewTextBoxColumn8.HeaderText = "الحســـاب"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "TREE_CODE"
        Me.DataGridViewTextBoxColumn9.HeaderText = "كود الحساب"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'Agent_Balance_For_Tree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.GENERAL_TXT)
        Me.Controls.Add(Me.AGENTS_TXT)
        Me.Controls.Add(Me.TR_TXT)
        Me.Controls.Add(Me.ST_TXT)
        Me.Controls.Add(Me.ST_DataGridView)
        Me.Controls.Add(Me.TR_DataGridView)
        Me.Controls.Add(Me.AGENTS_DataGridView)
        Me.Controls.Add(Me.GENERAL_DataGridView)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Agent_Balance_For_Tree"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الحسابات الإقتراضية العامة"
        CType(Me.GENERAL_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AGENTS_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TR_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ST_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ExitFormButton As Button
    Friend WithEvents GENERAL_DataGridView As DataGridView
    Friend WithEvents T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents B_NAME_CL As DataGridViewTextBoxColumn
    Friend WithEvents TREE_CODE_CL As DataGridViewTextBoxColumn
    Friend WithEvents AGENTS_DataGridView As DataGridView
    Friend WithEvents TR_DataGridView As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents ST_DataGridView As DataGridView
    Friend WithEvents ST_TXT As TextBox
    Friend WithEvents TR_TXT As TextBox
    Friend WithEvents AGENTS_TXT As TextBox
    Friend WithEvents GENERAL_TXT As TextBox
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
End Class
