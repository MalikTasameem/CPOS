<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MONTHS_CALENDR_Update
    Inherits Base_Form
    'Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MONTHS_CALENDR_Update))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DATETIME_F = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DATETIME_TO = New System.Windows.Forms.DateTimePicker()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.Open_Btn = New System.Windows.Forms.Button()
        Me.Close_Btn = New System.Windows.Forms.Button()
        Me.Back_btn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(223, 15)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 22)
        Me.Label2.TabIndex = 353
        Me.Label2.Text = "مــن"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DATETIME_F
        '
        Me.DATETIME_F.CustomFormat = "dd/MM/yyyy"
        Me.DATETIME_F.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DATETIME_F.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DATETIME_F.Location = New System.Drawing.Point(14, 11)
        Me.DATETIME_F.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.DATETIME_F.Name = "DATETIME_F"
        Me.DATETIME_F.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DATETIME_F.RightToLeftLayout = True
        Me.DATETIME_F.Size = New System.Drawing.Size(206, 29)
        Me.DATETIME_F.TabIndex = 352
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(223, 48)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 22)
        Me.Label1.TabIndex = 355
        Me.Label1.Text = "إلــى"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DATETIME_TO
        '
        Me.DATETIME_TO.CustomFormat = "dd/MM/yyyy"
        Me.DATETIME_TO.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DATETIME_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DATETIME_TO.Location = New System.Drawing.Point(14, 44)
        Me.DATETIME_TO.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.DATETIME_TO.Name = "DATETIME_TO"
        Me.DATETIME_TO.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DATETIME_TO.RightToLeftLayout = True
        Me.DATETIME_TO.Size = New System.Drawing.Size(206, 29)
        Me.DATETIME_TO.TabIndex = 354
        '
        'SaveButton
        '
        Me.SaveButton.BackColor = System.Drawing.SystemColors.MenuBar
        Me.SaveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.SaveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveButton.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveButton.Location = New System.Drawing.Point(14, 74)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(206, 47)
        Me.SaveButton.TabIndex = 356
        Me.SaveButton.Text = "حـفـظ  💾"
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'Open_Btn
        '
        Me.Open_Btn.BackColor = System.Drawing.Color.White
        Me.Open_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Open_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Open_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Open_Btn.Location = New System.Drawing.Point(6, 131)
        Me.Open_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Open_Btn.Name = "Open_Btn"
        Me.Open_Btn.Size = New System.Drawing.Size(251, 36)
        Me.Open_Btn.TabIndex = 652
        Me.Open_Btn.Text = "🔓 فتح الشهر الحالي"
        Me.Open_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Open_Btn.UseVisualStyleBackColor = False
        '
        'Close_Btn
        '
        Me.Close_Btn.BackColor = System.Drawing.Color.White
        Me.Close_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Close_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Close_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Close_Btn.Location = New System.Drawing.Point(6, 168)
        Me.Close_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Close_Btn.Name = "Close_Btn"
        Me.Close_Btn.Size = New System.Drawing.Size(251, 36)
        Me.Close_Btn.TabIndex = 651
        Me.Close_Btn.Text = "🔒 إقفــال الشهر الحالي"
        Me.Close_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Close_Btn.UseVisualStyleBackColor = False
        '
        'Back_btn
        '
        Me.Back_btn.BackColor = System.Drawing.Color.White
        Me.Back_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Back_btn.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Back_btn.Location = New System.Drawing.Point(3, 223)
        Me.Back_btn.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Back_btn.Name = "Back_btn"
        Me.Back_btn.Size = New System.Drawing.Size(263, 38)
        Me.Back_btn.TabIndex = 653
        Me.Back_btn.Text = "عودة  ↩️"
        Me.Back_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Back_btn.UseVisualStyleBackColor = False
        '
        'MONTHS_CALENDR_Update
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(268, 261)
        Me.Controls.Add(Me.Back_btn)
        Me.Controls.Add(Me.Open_Btn)
        Me.Controls.Add(Me.Close_Btn)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DATETIME_TO)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DATETIME_F)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MONTHS_CALENDR_Update"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DATETIME_F As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DATETIME_TO As System.Windows.Forms.DateTimePicker
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents Open_Btn As Button
    Friend WithEvents Close_Btn As Button
    Friend WithEvents Back_btn As Button
End Class
