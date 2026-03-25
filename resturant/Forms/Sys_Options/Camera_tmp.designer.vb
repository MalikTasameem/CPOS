<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Camera_tmp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Camera_tmp))
        Me.lstDevices = New System.Windows.Forms.ListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnCapture = New System.Windows.Forms.Button()
        Me.picFinalPicture = New System.Windows.Forms.PictureBox()
        Me.picCapture = New System.Windows.Forms.PictureBox()
        Me.Depend_Btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picFinalPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstDevices
        '
        Me.lstDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstDevices.FormattingEnabled = True
        Me.lstDevices.ItemHeight = 24
        Me.lstDevices.Location = New System.Drawing.Point(3, 25)
        Me.lstDevices.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstDevices.Name = "lstDevices"
        Me.lstDevices.Size = New System.Drawing.Size(409, 193)
        Me.lstDevices.TabIndex = 563
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstDevices)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(4, 373)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(415, 221)
        Me.GroupBox1.TabIndex = 569
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "الكاميرات"
        '
        'btnCapture
        '
        Me.btnCapture.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCapture.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCapture.Location = New System.Drawing.Point(469, 373)
        Me.btnCapture.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCapture.Name = "btnCapture"
        Me.btnCapture.Size = New System.Drawing.Size(200, 72)
        Me.btnCapture.TabIndex = 567
        Me.btnCapture.Text = "إلتقط صورة  F1"
        Me.btnCapture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCapture.UseVisualStyleBackColor = True
        '
        'picFinalPicture
        '
        Me.picFinalPicture.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.picFinalPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picFinalPicture.Location = New System.Drawing.Point(469, 1)
        Me.picFinalPicture.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.picFinalPicture.Name = "picFinalPicture"
        Me.picFinalPicture.Size = New System.Drawing.Size(415, 370)
        Me.picFinalPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picFinalPicture.TabIndex = 566
        Me.picFinalPicture.TabStop = False
        '
        'picCapture
        '
        Me.picCapture.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.picCapture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picCapture.Location = New System.Drawing.Point(4, 1)
        Me.picCapture.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.picCapture.Name = "picCapture"
        Me.picCapture.Size = New System.Drawing.Size(415, 370)
        Me.picCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picCapture.TabIndex = 564
        Me.picCapture.TabStop = False
        '
        'Depend_Btn
        '
        Me.Depend_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Depend_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Depend_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Depend_Btn.Location = New System.Drawing.Point(684, 373)
        Me.Depend_Btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Depend_Btn.Name = "Depend_Btn"
        Me.Depend_Btn.Size = New System.Drawing.Size(200, 72)
        Me.Depend_Btn.TabIndex = 570
        Me.Depend_Btn.Text = "إعتماد"
        Me.Depend_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Depend_Btn.UseVisualStyleBackColor = True
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
        Me.ExitFormButton.Location = New System.Drawing.Point(693, 523)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(189, 72)
        Me.ExitFormButton.TabIndex = 655
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Camera
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 596)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Depend_Btn)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCapture)
        Me.Controls.Add(Me.picFinalPicture)
        Me.Controls.Add(Me.picCapture)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Camera"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الكاميرة"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.picFinalPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCapture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstDevices As System.Windows.Forms.ListBox
    Friend WithEvents picCapture As System.Windows.Forms.PictureBox
    Friend WithEvents picFinalPicture As System.Windows.Forms.PictureBox
    Friend WithEvents btnCapture As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Depend_Btn As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
End Class
