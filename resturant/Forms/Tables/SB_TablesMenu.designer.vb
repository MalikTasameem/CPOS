<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SB_TablesMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SB_TablesMenu))
        Me.NONE_TB_btn = New System.Windows.Forms.Button()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.Button51 = New System.Windows.Forms.Button()
        Me.Button50 = New System.Windows.Forms.Button()
        Me.TBPanel = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'NONE_TB_btn
        '
        Me.NONE_TB_btn.BackColor = System.Drawing.SystemColors.Control
        Me.NONE_TB_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NONE_TB_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NONE_TB_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NONE_TB_btn.ForeColor = System.Drawing.Color.Black
        Me.NONE_TB_btn.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.NONE_TB_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NONE_TB_btn.Location = New System.Drawing.Point(2, 0)
        Me.NONE_TB_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.NONE_TB_btn.Name = "NONE_TB_btn"
        Me.NONE_TB_btn.Size = New System.Drawing.Size(221, 42)
        Me.NONE_TB_btn.TabIndex = 554
        Me.NONE_TB_btn.Text = "بدون طاولة"
        Me.NONE_TB_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NONE_TB_btn.UseVisualStyleBackColor = False
        '
        'Back_Btn
        '
        Me.Back_Btn.BackColor = System.Drawing.Color.White
        Me.Back_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.Users_Exit_icon
        Me.Back_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.Location = New System.Drawing.Point(665, 2)
        Me.Back_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.Size = New System.Drawing.Size(81, 62)
        Me.Back_Btn.TabIndex = 559
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'Button51
        '
        Me.Button51.BackColor = System.Drawing.SystemColors.Info
        Me.Button51.BackgroundImage = CType(resources.GetObject("Button51.BackgroundImage"), System.Drawing.Image)
        Me.Button51.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button51.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button51.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button51.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button51.Location = New System.Drawing.Point(654, 357)
        Me.Button51.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button51.Name = "Button51"
        Me.Button51.Size = New System.Drawing.Size(91, 66)
        Me.Button51.TabIndex = 561
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
        Me.Button50.Location = New System.Drawing.Point(654, 109)
        Me.Button50.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button50.Name = "Button50"
        Me.Button50.Size = New System.Drawing.Size(91, 66)
        Me.Button50.TabIndex = 560
        Me.Button50.UseVisualStyleBackColor = False
        '
        'TBPanel
        '
        Me.TBPanel.AutoScroll = True
        Me.TBPanel.BackColor = System.Drawing.Color.Transparent
        Me.TBPanel.Location = New System.Drawing.Point(2, 43)
        Me.TBPanel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TBPanel.Name = "TBPanel"
        Me.TBPanel.Size = New System.Drawing.Size(645, 451)
        Me.TBPanel.TabIndex = 562
        '
        'SB_TablesMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 497)
        Me.ControlBox = False
        Me.Controls.Add(Me.TBPanel)
        Me.Controls.Add(Me.Button51)
        Me.Controls.Add(Me.Button50)
        Me.Controls.Add(Me.Back_Btn)
        Me.Controls.Add(Me.NONE_TB_btn)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "SB_TablesMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تحديد طاولــة"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NONE_TB_btn As System.Windows.Forms.Button
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents Button51 As System.Windows.Forms.Button
    Friend WithEvents Button50 As System.Windows.Forms.Button
    Friend WithEvents TBPanel As System.Windows.Forms.Panel
End Class
