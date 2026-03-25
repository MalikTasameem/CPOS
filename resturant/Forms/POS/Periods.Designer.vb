<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Periods
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Periods))
        Me.Pr_NotesLabel = New System.Windows.Forms.Label()
        Me.Start_PrButton = New System.Windows.Forms.Button()
        Me.End_PrButton = New System.Windows.Forms.Button()
        Me.TimeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Show_OpenBills_btn = New System.Windows.Forms.Button()
        Me.Pr_Grid = New System.Windows.Forms.DataGridView()
        Me.Pr_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.USER_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TIME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WORK_TIME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.Pr_Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Pr_NotesLabel
        '
        Me.Pr_NotesLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pr_NotesLabel.Font = New System.Drawing.Font("JF Flat", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pr_NotesLabel.ForeColor = System.Drawing.Color.Red
        Me.Pr_NotesLabel.Location = New System.Drawing.Point(687, 1)
        Me.Pr_NotesLabel.Name = "Pr_NotesLabel"
        Me.Pr_NotesLabel.Size = New System.Drawing.Size(170, 164)
        Me.Pr_NotesLabel.TabIndex = 1
        Me.Pr_NotesLabel.Text = "---"
        '
        'Start_PrButton
        '
        Me.Start_PrButton.BackColor = System.Drawing.SystemColors.Info
        Me.Start_PrButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Start_PrButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Start_PrButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Start_PrButton.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Start_PrButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Start_PrButton.Location = New System.Drawing.Point(687, 286)
        Me.Start_PrButton.Margin = New System.Windows.Forms.Padding(5)
        Me.Start_PrButton.Name = "Start_PrButton"
        Me.Start_PrButton.Size = New System.Drawing.Size(171, 50)
        Me.Start_PrButton.TabIndex = 197
        Me.Start_PrButton.Text = "بـدء الوردية ENTER"
        Me.Start_PrButton.UseVisualStyleBackColor = False
        '
        'End_PrButton
        '
        Me.End_PrButton.BackColor = System.Drawing.SystemColors.Info
        Me.End_PrButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.End_PrButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.End_PrButton.Enabled = False
        Me.End_PrButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.End_PrButton.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.End_PrButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.End_PrButton.Location = New System.Drawing.Point(687, 346)
        Me.End_PrButton.Margin = New System.Windows.Forms.Padding(5)
        Me.End_PrButton.Name = "End_PrButton"
        Me.End_PrButton.Size = New System.Drawing.Size(171, 50)
        Me.End_PrButton.TabIndex = 196
        Me.End_PrButton.Text = "إغــــلاق الوردية "
        Me.End_PrButton.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(687, 538)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(171, 38)
        Me.ExitFormButton.TabIndex = 657
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Show_OpenBills_btn
        '
        Me.Show_OpenBills_btn.BackColor = System.Drawing.SystemColors.Info
        Me.Show_OpenBills_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_OpenBills_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_OpenBills_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Show_OpenBills_btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Show_OpenBills_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Show_OpenBills_btn.Location = New System.Drawing.Point(687, 170)
        Me.Show_OpenBills_btn.Margin = New System.Windows.Forms.Padding(5)
        Me.Show_OpenBills_btn.Name = "Show_OpenBills_btn"
        Me.Show_OpenBills_btn.Size = New System.Drawing.Size(171, 50)
        Me.Show_OpenBills_btn.TabIndex = 658
        Me.Show_OpenBills_btn.Text = "عرض الفواتير المفتوحة"
        Me.Show_OpenBills_btn.UseVisualStyleBackColor = False
        '
        'Pr_Grid
        '
        Me.Pr_Grid.AllowUserToAddRows = False
        Me.Pr_Grid.AllowUserToDeleteRows = False
        Me.Pr_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Pr_Grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Pr_Grid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.Pr_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Pr_Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Pr_ID_CL, Me.USER_NAME_CL, Me.Date_CL, Me.TIME_CL, Me.WORK_TIME_CL})
        Me.Pr_Grid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Pr_Grid.Location = New System.Drawing.Point(1, 1)
        Me.Pr_Grid.MultiSelect = False
        Me.Pr_Grid.Name = "Pr_Grid"
        Me.Pr_Grid.ReadOnly = True
        Me.Pr_Grid.RowHeadersVisible = False
        Me.Pr_Grid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Pr_Grid.RowTemplate.Height = 25
        Me.Pr_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Pr_Grid.Size = New System.Drawing.Size(680, 575)
        Me.Pr_Grid.TabIndex = 701
        '
        'Pr_ID_CL
        '
        Me.Pr_ID_CL.DataPropertyName = "Pr_ID"
        Me.Pr_ID_CL.HeaderText = "Pr_ID"
        Me.Pr_ID_CL.Name = "Pr_ID_CL"
        Me.Pr_ID_CL.ReadOnly = True
        Me.Pr_ID_CL.Visible = False
        '
        'USER_NAME_CL
        '
        Me.USER_NAME_CL.DataPropertyName = "UserName"
        Me.USER_NAME_CL.FillWeight = 91.83587!
        Me.USER_NAME_CL.HeaderText = "المستخدم"
        Me.USER_NAME_CL.Name = "USER_NAME_CL"
        Me.USER_NAME_CL.ReadOnly = True
        '
        'Date_CL
        '
        Me.Date_CL.DataPropertyName = "Date"
        Me.Date_CL.FillWeight = 91.83587!
        Me.Date_CL.HeaderText = "تاريخ البدء"
        Me.Date_CL.Name = "Date_CL"
        Me.Date_CL.ReadOnly = True
        '
        'TIME_CL
        '
        Me.TIME_CL.DataPropertyName = "TIME"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.TIME_CL.DefaultCellStyle = DataGridViewCellStyle1
        Me.TIME_CL.FillWeight = 91.83587!
        Me.TIME_CL.HeaderText = "وقت البدء"
        Me.TIME_CL.Name = "TIME_CL"
        Me.TIME_CL.ReadOnly = True
        '
        'WORK_TIME_CL
        '
        Me.WORK_TIME_CL.DataPropertyName = "WORK_TIME"
        Me.WORK_TIME_CL.HeaderText = "فترة العمل"
        Me.WORK_TIME_CL.Name = "WORK_TIME_CL"
        Me.WORK_TIME_CL.ReadOnly = True
        '
        'Periods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(859, 576)
        Me.ControlBox = False
        Me.Controls.Add(Me.Pr_Grid)
        Me.Controls.Add(Me.Show_OpenBills_btn)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Start_PrButton)
        Me.Controls.Add(Me.End_PrButton)
        Me.Controls.Add(Me.Pr_NotesLabel)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Periods"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الورديــــــات"
        CType(Me.Pr_Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pr_NotesLabel As System.Windows.Forms.Label
    Friend WithEvents Start_PrButton As System.Windows.Forms.Button
    Friend WithEvents End_PrButton As System.Windows.Forms.Button
    Friend WithEvents TimeTimer As System.Windows.Forms.Timer
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Show_OpenBills_btn As System.Windows.Forms.Button
    Friend WithEvents Pr_Grid As System.Windows.Forms.DataGridView
    Friend WithEvents Pr_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents USER_NAME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TIME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WORK_TIME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
