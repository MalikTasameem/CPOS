<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OPEN_Bills
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
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Open_MV_DV = New System.Windows.Forms.DataGridView()
        CType(Me.Open_MV_DV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(1, 344)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(489, 55)
        Me.ExitFormButton.TabIndex = 675
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Open_MV_DV
        '
        Me.Open_MV_DV.AllowUserToAddRows = False
        Me.Open_MV_DV.AllowUserToDeleteRows = False
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Open_MV_DV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13
        Me.Open_MV_DV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Open_MV_DV.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Open_MV_DV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.DeepSkyBlue
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Open_MV_DV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.Open_MV_DV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Open_MV_DV.ColumnHeadersVisible = False
        Me.Open_MV_DV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.DarkGray
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Open_MV_DV.DefaultCellStyle = DataGridViewCellStyle15
        Me.Open_MV_DV.EnableHeadersVisualStyles = False
        Me.Open_MV_DV.Location = New System.Drawing.Point(1, 0)
        Me.Open_MV_DV.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Open_MV_DV.MultiSelect = False
        Me.Open_MV_DV.Name = "Open_MV_DV"
        Me.Open_MV_DV.ReadOnly = True
        Me.Open_MV_DV.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Open_MV_DV.RowHeadersVisible = False
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Sakkal Majalla", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Open_MV_DV.RowsDefaultCellStyle = DataGridViewCellStyle16
        Me.Open_MV_DV.RowTemplate.Height = 25
        Me.Open_MV_DV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Open_MV_DV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Open_MV_DV.Size = New System.Drawing.Size(489, 343)
        Me.Open_MV_DV.TabIndex = 676
        '
        'OPEN_Bills
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 399)
        Me.ControlBox = False
        Me.Controls.Add(Me.Open_MV_DV)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "OPEN_Bills"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "فواتير مفتوحة"
        CType(Me.Open_MV_DV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExitFormButton As Button
    Friend WithEvents Open_MV_DV As DataGridView
End Class
