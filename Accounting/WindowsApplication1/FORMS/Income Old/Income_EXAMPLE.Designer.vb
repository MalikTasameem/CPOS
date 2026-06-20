<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Income_EXAMPLE
    Inherits Base_Form

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView_Master = New System.Windows.Forms.DataGridView()
        Me.Button4 = New System.Windows.Forms.Button()
        CType(Me.DataGridView_Master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView_Master
        '
        Me.DataGridView_Master.AllowUserToAddRows = False
        Me.DataGridView_Master.AllowUserToDeleteRows = False
        Me.DataGridView_Master.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView_Master.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView_Master.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_Master.ColumnHeadersVisible = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 10.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView_Master.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView_Master.EnableHeadersVisualStyles = False
        Me.DataGridView_Master.Location = New System.Drawing.Point(0, 2)
        Me.DataGridView_Master.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView_Master.MultiSelect = False
        Me.DataGridView_Master.Name = "DataGridView_Master"
        Me.DataGridView_Master.ReadOnly = True
        Me.DataGridView_Master.RowTemplate.Height = 30
        Me.DataGridView_Master.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView_Master.Size = New System.Drawing.Size(1003, 646)
        Me.DataGridView_Master.TabIndex = 41
        '
        'Button4
        '
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)

        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.Location = New System.Drawing.Point(1, 649)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(1002, 45)
        Me.Button4.TabIndex = 84
        Me.Button4.Text = "عـــودة   ↩️"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Income_EXAMPLE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.DataGridView_Master)
        Me.Name = "Income_EXAMPLE"
        Me.Text = "معاينـــة نموذج الدخل"
        CType(Me.DataGridView_Master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView_Master As DataGridView
    Friend WithEvents Button4 As Button
End Class
