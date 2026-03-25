<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SELECT_GM_FIRST_TIME
    Inherits Base_Form

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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GM_Serach = New System.Windows.Forms.ComboBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.UpdateGBButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(333, 10)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 26)
        Me.Label2.TabIndex = 442
        Me.Label2.Text = "التصنيف"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GM_Serach
        '
        Me.GM_Serach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.GM_Serach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.GM_Serach.BackColor = System.Drawing.SystemColors.Info
        Me.GM_Serach.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GM_Serach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GM_Serach.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Serach.Location = New System.Drawing.Point(3, 6)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GM_Serach.Size = New System.Drawing.Size(325, 34)
        Me.GM_Serach.TabIndex = 443
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(285, 163)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(125, 40)
        Me.ExitFormButton.TabIndex = 655
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'UpdateGBButton
        '
        Me.UpdateGBButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.UpdateGBButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.UpdateGBButton.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.UpdateGBButton.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.UpdateGBButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.UpdateGBButton.Location = New System.Drawing.Point(3, 163)
        Me.UpdateGBButton.Name = "UpdateGBButton"
        Me.UpdateGBButton.Size = New System.Drawing.Size(150, 39)
        Me.UpdateGBButton.TabIndex = 656
        Me.UpdateGBButton.Text = "موافــق"
        Me.UpdateGBButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.UpdateGBButton.UseVisualStyleBackColor = True
        '
        'SELECT_GM_FIRST_TIME
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 26.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 203)
        Me.ControlBox = False
        Me.Controls.Add(Me.UpdateGBButton)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GM_Serach)
        Me.Name = "SELECT_GM_FIRST_TIME"
        Me.Text = "حدد التصنيف"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GM_Serach As System.Windows.Forms.ComboBox
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents UpdateGBButton As System.Windows.Forms.Button
End Class
