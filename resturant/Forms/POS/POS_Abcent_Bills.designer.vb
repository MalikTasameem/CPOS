<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class POS_Abcent_Bills
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Cancel_ALL_Bills_btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Open_MV_DV = New System.Windows.Forms.DataGridView()
        Me.check_cl = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Confirm_ALL_Bills_btn = New System.Windows.Forms.Button()
        Me.CHECK_ALL_CB = New System.Windows.Forms.CheckBox()
        CType(Me.Open_MV_DV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Cancel_ALL_Bills_btn
        '
        Me.Cancel_ALL_Bills_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel_ALL_Bills_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Cancel_ALL_Bills_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancel_ALL_Bills_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cancel_ALL_Bills_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Cancel_ALL_Bills_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel_ALL_Bills_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Cancel_ALL_Bills_btn.Image = Global.resturant.My.Resources.Resources.if_f_cross_256_282471
        Me.Cancel_ALL_Bills_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Cancel_ALL_Bills_btn.Location = New System.Drawing.Point(418, 1)
        Me.Cancel_ALL_Bills_btn.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.Cancel_ALL_Bills_btn.Name = "Cancel_ALL_Bills_btn"
        Me.Cancel_ALL_Bills_btn.Size = New System.Drawing.Size(165, 52)
        Me.Cancel_ALL_Bills_btn.TabIndex = 443
        Me.Cancel_ALL_Bills_btn.Text = "إلغــاء المحدد"
        Me.Cancel_ALL_Bills_btn.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 12.25!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.Location = New System.Drawing.Point(2, 446)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ExitFormButton.Size = New System.Drawing.Size(581, 44)
        Me.ExitFormButton.TabIndex = 456
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Open_MV_DV
        '
        Me.Open_MV_DV.AllowUserToAddRows = False
        Me.Open_MV_DV.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Open_MV_DV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.Open_MV_DV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Open_MV_DV.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Open_MV_DV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.DeepSkyBlue
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI Semibold", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Open_MV_DV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.Open_MV_DV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Open_MV_DV.ColumnHeadersVisible = False
        Me.Open_MV_DV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.check_cl})
        Me.Open_MV_DV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Segoe UI Semibold", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.DarkGray
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Open_MV_DV.DefaultCellStyle = DataGridViewCellStyle7
        Me.Open_MV_DV.EnableHeadersVisualStyles = False
        Me.Open_MV_DV.Location = New System.Drawing.Point(2, 57)
        Me.Open_MV_DV.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Open_MV_DV.MultiSelect = False
        Me.Open_MV_DV.Name = "Open_MV_DV"
        Me.Open_MV_DV.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Open_MV_DV.RowHeadersVisible = False
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Sakkal Majalla", 16.25!, System.Drawing.FontStyle.Bold)
        Me.Open_MV_DV.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.Open_MV_DV.RowTemplate.Height = 35
        Me.Open_MV_DV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Open_MV_DV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Open_MV_DV.Size = New System.Drawing.Size(581, 388)
        Me.Open_MV_DV.TabIndex = 643
        '
        'check_cl
        '
        Me.check_cl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.check_cl.HeaderText = "تحديد"
        Me.check_cl.Name = "check_cl"
        '
        'Confirm_ALL_Bills_btn
        '
        Me.Confirm_ALL_Bills_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Confirm_ALL_Bills_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Confirm_ALL_Bills_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Confirm_ALL_Bills_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Confirm_ALL_Bills_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Confirm_ALL_Bills_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Confirm_ALL_Bills_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Confirm_ALL_Bills_btn.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.Confirm_ALL_Bills_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Confirm_ALL_Bills_btn.Location = New System.Drawing.Point(2, 1)
        Me.Confirm_ALL_Bills_btn.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.Confirm_ALL_Bills_btn.Name = "Confirm_ALL_Bills_btn"
        Me.Confirm_ALL_Bills_btn.Size = New System.Drawing.Size(170, 52)
        Me.Confirm_ALL_Bills_btn.TabIndex = 644
        Me.Confirm_ALL_Bills_btn.Text = "إعتماد المحدد"
        Me.Confirm_ALL_Bills_btn.UseVisualStyleBackColor = False
        '
        'CHECK_ALL_CB
        '
        Me.CHECK_ALL_CB.AutoSize = True
        Me.CHECK_ALL_CB.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.CHECK_ALL_CB.Location = New System.Drawing.Point(252, 19)
        Me.CHECK_ALL_CB.Name = "CHECK_ALL_CB"
        Me.CHECK_ALL_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CHECK_ALL_CB.Size = New System.Drawing.Size(86, 23)
        Me.CHECK_ALL_CB.TabIndex = 6553
        Me.CHECK_ALL_CB.Text = "تحديد الكل"
        Me.CHECK_ALL_CB.UseVisualStyleBackColor = True
        '
        'POS_Abcent_Bills
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 30.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 491)
        Me.ControlBox = False
        Me.Controls.Add(Me.CHECK_ALL_CB)
        Me.Controls.Add(Me.Confirm_ALL_Bills_btn)
        Me.Controls.Add(Me.Open_MV_DV)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Cancel_ALL_Bills_btn)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.MaximizeBox = False
        Me.Name = "POS_Abcent_Bills"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "فواتير غير معتمدة"
        CType(Me.Open_MV_DV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel_ALL_Bills_btn As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Open_MV_DV As DataGridView
    Friend WithEvents check_cl As DataGridViewCheckBoxColumn
    Friend WithEvents Confirm_ALL_Bills_btn As Button
    Friend WithEvents CHECK_ALL_CB As CheckBox
End Class
