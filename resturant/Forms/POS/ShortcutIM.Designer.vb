<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShortcutIM
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ShortcutIM))
        Me.IMPanel = New System.Windows.Forms.Panel()
        Me.Button51 = New System.Windows.Forms.Button()
        Me.Button50 = New System.Windows.Forms.Button()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'IMPanel
        '
        Me.IMPanel.AutoScroll = True
        Me.IMPanel.BackColor = System.Drawing.Color.Transparent
        Me.IMPanel.Location = New System.Drawing.Point(2, 2)
        Me.IMPanel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IMPanel.Name = "IMPanel"
        Me.IMPanel.Size = New System.Drawing.Size(832, 613)
        Me.IMPanel.TabIndex = 566
        '
        'Button51
        '
        Me.Button51.BackColor = System.Drawing.SystemColors.Info
        Me.Button51.BackgroundImage = CType(resources.GetObject("Button51.BackgroundImage"), System.Drawing.Image)
        Me.Button51.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button51.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button51.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button51.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button51.Location = New System.Drawing.Point(841, 433)
        Me.Button51.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button51.Name = "Button51"
        Me.Button51.Size = New System.Drawing.Size(91, 93)
        Me.Button51.TabIndex = 565
        Me.Button51.UseVisualStyleBackColor = False
        '
        'Button50
        '
        Me.Button50.BackColor = System.Drawing.SystemColors.Info
        Me.Button50.BackgroundImage = CType(resources.GetObject("Button50.BackgroundImage"), System.Drawing.Image)
        Me.Button50.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button50.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button50.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button50.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button50.Location = New System.Drawing.Point(841, 146)
        Me.Button50.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button50.Name = "Button50"
        Me.Button50.Size = New System.Drawing.Size(91, 93)
        Me.Button50.TabIndex = 564
        Me.Button50.UseVisualStyleBackColor = False
        '
        'Back_Btn
        '
        Me.Back_Btn.BackColor = System.Drawing.Color.White
        Me.Back_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.Users_Exit_icon
        Me.Back_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.Location = New System.Drawing.Point(841, 2)
        Me.Back_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.Size = New System.Drawing.Size(91, 62)
        Me.Back_Btn.TabIndex = 563
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'ShortcutIM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(933, 617)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button51)
        Me.Controls.Add(Me.IMPanel)
        Me.Controls.Add(Me.Button50)
        Me.Controls.Add(Me.Back_Btn)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "ShortcutIM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "قائمة الأصناف السريعة"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IMPanel As System.Windows.Forms.Panel
    Friend WithEvents Button51 As System.Windows.Forms.Button
    Friend WithEvents Button50 As System.Windows.Forms.Button
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
End Class
