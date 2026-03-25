<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ComponentsManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ComponentsManager))
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.CancelNotesButton = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.IM_Com_Grid = New MetroFramework.Controls.MetroGrid()
        Me.T_ID_3_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TB_IM_QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Plus_CL = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Min_CL = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Delete = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.SMButtonDown = New System.Windows.Forms.Button()
        Me.SMButtonUP = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        CType(Me.IM_Com_Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Back_Btn
        '
        Me.Back_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Back_Btn.BackColor = System.Drawing.Color.White
        Me.Back_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.Users_Exit_icon
        Me.Back_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.Location = New System.Drawing.Point(946, 538)
        Me.Back_Btn.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.Size = New System.Drawing.Size(68, 43)
        Me.Back_Btn.TabIndex = 441
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'CancelNotesButton
        '
        Me.CancelNotesButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.CancelNotesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelNotesButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CancelNotesButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CancelNotesButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.CancelNotesButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CancelNotesButton.Image = Global.resturant.My.Resources.Resources.if_f_cross_256_282471
        Me.CancelNotesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelNotesButton.Location = New System.Drawing.Point(5, 538)
        Me.CancelNotesButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CancelNotesButton.Name = "CancelNotesButton"
        Me.CancelNotesButton.Size = New System.Drawing.Size(129, 43)
        Me.CancelNotesButton.TabIndex = 443
        Me.CancelNotesButton.Text = "إلغاء الكل"
        Me.CancelNotesButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelNotesButton.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(2, 1)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.RightToLeftLayout = True
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(919, 322)
        Me.TabControl1.TabIndex = 446
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.BackColor = System.Drawing.Color.Transparent
        Me.TabPage1.Location = New System.Drawing.Point(4, 39)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage1.Size = New System.Drawing.Size(911, 279)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "ملاحظة"
        '
        'TabPage2
        '
        Me.TabPage2.AutoScroll = True
        Me.TabPage2.Location = New System.Drawing.Point(4, 39)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage2.Size = New System.Drawing.Size(911, 279)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "إضافـــة"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'IM_Com_Grid
        '
        Me.IM_Com_Grid.AllowUserToAddRows = False
        Me.IM_Com_Grid.AllowUserToDeleteRows = False
        Me.IM_Com_Grid.AllowUserToResizeRows = False
        Me.IM_Com_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IM_Com_Grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.IM_Com_Grid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.IM_Com_Grid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.IM_Com_Grid.CausesValidation = False
        Me.IM_Com_Grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.IM_Com_Grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IM_Com_Grid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.IM_Com_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IM_Com_Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_3_CL, Me.IM_Name_CL, Me.TB_IM_QTY_CL, Me.Plus_CL, Me.Min_CL, Me.Delete})
        Me.IM_Com_Grid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IM_Com_Grid.DefaultCellStyle = DataGridViewCellStyle2
        Me.IM_Com_Grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.IM_Com_Grid.EnableHeadersVisualStyles = False
        Me.IM_Com_Grid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.IM_Com_Grid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.IM_Com_Grid.Location = New System.Drawing.Point(2, 323)
        Me.IM_Com_Grid.MultiSelect = False
        Me.IM_Com_Grid.Name = "IM_Com_Grid"
        Me.IM_Com_Grid.ReadOnly = True
        Me.IM_Com_Grid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_Com_Grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IM_Com_Grid.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.IM_Com_Grid.RowHeadersVisible = False
        Me.IM_Com_Grid.RowHeadersWidth = 100
        Me.IM_Com_Grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.IM_Com_Grid.RowTemplate.Height = 150
        Me.IM_Com_Grid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IM_Com_Grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.IM_Com_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IM_Com_Grid.Size = New System.Drawing.Size(1012, 210)
        Me.IM_Com_Grid.TabIndex = 581
        Me.IM_Com_Grid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'T_ID_3_CL
        '
        Me.T_ID_3_CL.DataPropertyName = "T_ID"
        Me.T_ID_3_CL.HeaderText = "ر.الآلي"
        Me.T_ID_3_CL.Name = "T_ID_3_CL"
        Me.T_ID_3_CL.ReadOnly = True
        Me.T_ID_3_CL.Visible = False
        '
        'IM_Name_CL
        '
        Me.IM_Name_CL.DataPropertyName = "Comp_Name"
        Me.IM_Name_CL.FillWeight = 307.4976!
        Me.IM_Name_CL.HeaderText = "الإضافة"
        Me.IM_Name_CL.Name = "IM_Name_CL"
        Me.IM_Name_CL.ReadOnly = True
        '
        'TB_IM_QTY_CL
        '
        Me.TB_IM_QTY_CL.DataPropertyName = "Qty"
        Me.TB_IM_QTY_CL.FillWeight = 51.15959!
        Me.TB_IM_QTY_CL.HeaderText = "العدد"
        Me.TB_IM_QTY_CL.Name = "TB_IM_QTY_CL"
        Me.TB_IM_QTY_CL.ReadOnly = True
        '
        'Plus_CL
        '
        Me.Plus_CL.FillWeight = 64.01435!
        Me.Plus_CL.HeaderText = ""
        Me.Plus_CL.Name = "Plus_CL"
        Me.Plus_CL.ReadOnly = True
        Me.Plus_CL.Text = "+"
        Me.Plus_CL.UseColumnTextForButtonValue = True
        '
        'Min_CL
        '
        Me.Min_CL.FillWeight = 64.01435!
        Me.Min_CL.HeaderText = ""
        Me.Min_CL.Name = "Min_CL"
        Me.Min_CL.ReadOnly = True
        Me.Min_CL.Text = "-"
        Me.Min_CL.UseColumnTextForButtonValue = True
        '
        'Delete
        '
        Me.Delete.FillWeight = 64.01435!
        Me.Delete.HeaderText = ""
        Me.Delete.Name = "Delete"
        Me.Delete.ReadOnly = True
        Me.Delete.Text = "/"
        Me.Delete.UseColumnTextForButtonValue = True
        '
        'SMButtonDown
        '
        Me.SMButtonDown.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.SMButtonDown.BackColor = System.Drawing.SystemColors.Info
        Me.SMButtonDown.BackgroundImage = CType(resources.GetObject("SMButtonDown.BackgroundImage"), System.Drawing.Image)
        Me.SMButtonDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SMButtonDown.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SMButtonDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SMButtonDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMButtonDown.Location = New System.Drawing.Point(931, 244)
        Me.SMButtonDown.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.SMButtonDown.Name = "SMButtonDown"
        Me.SMButtonDown.Size = New System.Drawing.Size(75, 75)
        Me.SMButtonDown.TabIndex = 583
        Me.SMButtonDown.UseVisualStyleBackColor = False
        '
        'SMButtonUP
        '
        Me.SMButtonUP.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.SMButtonUP.BackColor = System.Drawing.SystemColors.Info
        Me.SMButtonUP.BackgroundImage = CType(resources.GetObject("SMButtonUP.BackgroundImage"), System.Drawing.Image)
        Me.SMButtonUP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SMButtonUP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SMButtonUP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SMButtonUP.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMButtonUP.Location = New System.Drawing.Point(931, 41)
        Me.SMButtonUP.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.SMButtonUP.Name = "SMButtonUP"
        Me.SMButtonUP.Size = New System.Drawing.Size(75, 75)
        Me.SMButtonUP.TabIndex = 582
        Me.SMButtonUP.UseVisualStyleBackColor = False
        '
        'ComponentsManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 30.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 581)
        Me.ControlBox = False
        Me.Controls.Add(Me.SMButtonDown)
        Me.Controls.Add(Me.SMButtonUP)
        Me.Controls.Add(Me.IM_Com_Grid)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.CancelNotesButton)
        Me.Controls.Add(Me.Back_Btn)
        Me.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(7)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ComponentsManager"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "."
        Me.TabControl1.ResumeLayout(False)
        CType(Me.IM_Com_Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents CancelNotesButton As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents IM_Com_Grid As MetroFramework.Controls.MetroGrid
    Friend WithEvents SMButtonDown As System.Windows.Forms.Button
    Friend WithEvents SMButtonUP As System.Windows.Forms.Button
    Friend WithEvents T_ID_3_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TB_IM_QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Plus_CL As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Min_CL As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Delete As System.Windows.Forms.DataGridViewButtonColumn
End Class
