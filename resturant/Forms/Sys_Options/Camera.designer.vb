<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Camera
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Camera))
        Me.cmdno = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.pbcapture = New System.Windows.Forms.Button()
        Me.pbcaptureimage = New System.Windows.Forms.PictureBox()
        CType(Me.pbcaptureimage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdno
        '
        Me.cmdno.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdno.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdno.Font = New System.Drawing.Font("Sakkal Majalla", 14.0!, System.Drawing.FontStyle.Bold)
        Me.cmdno.Image = Global.resturant.My.Resources.Resources.iconfinder_icon_refresh_2867936
        Me.cmdno.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdno.Location = New System.Drawing.Point(359, 361)
        Me.cmdno.Name = "cmdno"
        Me.cmdno.Size = New System.Drawing.Size(166, 71)
        Me.cmdno.TabIndex = 3
        Me.cmdno.Text = "التقاط اخرى"
        Me.cmdno.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdno.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.cmdno.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.Font = New System.Drawing.Font("Sakkal Majalla", 14.0!, System.Drawing.FontStyle.Bold)
        Me.cmdok.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.cmdok.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdok.Location = New System.Drawing.Point(24, 361)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(166, 71)
        Me.cmdok.TabIndex = 2
        Me.cmdok.Text = "اختيار"
        Me.cmdok.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdok.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'pbcapture
        '
        Me.pbcapture.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pbcapture.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbcapture.Font = New System.Drawing.Font("Sakkal Majalla", 14.0!, System.Drawing.FontStyle.Bold)
        Me.pbcapture.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.pbcapture.Location = New System.Drawing.Point(192, 361)
        Me.pbcapture.Name = "pbcapture"
        Me.pbcapture.Size = New System.Drawing.Size(166, 70)
        Me.pbcapture.TabIndex = 4
        Me.pbcapture.Text = "التقاط"
        Me.pbcapture.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.pbcapture.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.pbcapture.UseVisualStyleBackColor = False
        '
        'pbcaptureimage
        '
        Me.pbcaptureimage.BackColor = System.Drawing.Color.White
        Me.pbcaptureimage.Location = New System.Drawing.Point(3, 1)
        Me.pbcaptureimage.Name = "pbcaptureimage"
        Me.pbcaptureimage.Size = New System.Drawing.Size(555, 359)
        Me.pbcaptureimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbcaptureimage.TabIndex = 0
        Me.pbcaptureimage.TabStop = False
        '
        'Camera
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(560, 434)
        Me.Controls.Add(Me.pbcapture)
        Me.Controls.Add(Me.cmdno)
        Me.Controls.Add(Me.cmdok)
        Me.Controls.Add(Me.pbcaptureimage)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Camera"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "كاميرا"
        CType(Me.pbcaptureimage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbcaptureimage As System.Windows.Forms.PictureBox
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents cmdno As System.Windows.Forms.Button
    Friend WithEvents pbcapture As Button
End Class
