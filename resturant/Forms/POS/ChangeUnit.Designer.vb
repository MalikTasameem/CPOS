<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangeUnit
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
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.IM_NameLb = New System.Windows.Forms.Label()
        Me.IMPanel = New System.Windows.Forms.Panel()
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
        Me.Back_Btn.Location = New System.Drawing.Point(401, 2)
        Me.Back_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.Size = New System.Drawing.Size(52, 45)
        Me.Back_Btn.TabIndex = 440
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'IM_NameLb
        '
        Me.IM_NameLb.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_NameLb.Location = New System.Drawing.Point(6, 4)
        Me.IM_NameLb.Name = "IM_NameLb"
        Me.IM_NameLb.Size = New System.Drawing.Size(388, 40)
        Me.IM_NameLb.TabIndex = 441
        Me.IM_NameLb.Text = "الصنف"
        Me.IM_NameLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IMPanel
        '
        Me.IMPanel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.IMPanel.AutoScroll = True
        Me.IMPanel.BackColor = System.Drawing.SystemColors.Control
        Me.IMPanel.Cursor = System.Windows.Forms.Cursors.Default
        Me.IMPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.IMPanel.Location = New System.Drawing.Point(5, 49)
        Me.IMPanel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.IMPanel.Name = "IMPanel"
        Me.IMPanel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IMPanel.Size = New System.Drawing.Size(447, 339)
        Me.IMPanel.TabIndex = 547
        '
        'ChangeUnit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 388)
        Me.ControlBox = False
        Me.Controls.Add(Me.IMPanel)
        Me.Controls.Add(Me.IM_NameLb)
        Me.Controls.Add(Me.Back_Btn)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.HelpButton = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "ChangeUnit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents IM_NameLb As System.Windows.Forms.Label
    Friend WithEvents IMPanel As System.Windows.Forms.Panel
End Class
