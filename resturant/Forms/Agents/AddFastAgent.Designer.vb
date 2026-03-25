<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddFastAgent
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AG_Name_txt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Phone_txt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Address_txt = New System.Windows.Forms.TextBox()
        Me.Save_AG_btn = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Barcode_txt = New System.Windows.Forms.TextBox()
        Me.BarError = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ExitFormButton = New System.Windows.Forms.Button()
        CType(Me.BarError, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(492, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 25)
        Me.Label1.TabIndex = 551
        Me.Label1.Text = "الإســم"
        '
        'AG_Name_txt
        '
        Me.AG_Name_txt.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Name_txt.Location = New System.Drawing.Point(2, 1)
        Me.AG_Name_txt.MaxLength = 500
        Me.AG_Name_txt.Name = "AG_Name_txt"
        Me.AG_Name_txt.Size = New System.Drawing.Size(486, 33)
        Me.AG_Name_txt.TabIndex = 550
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(492, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 25)
        Me.Label2.TabIndex = 553
        Me.Label2.Text = "هــاتف"
        '
        'Phone_txt
        '
        Me.Phone_txt.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Phone_txt.Location = New System.Drawing.Point(2, 71)
        Me.Phone_txt.MaxLength = 60
        Me.Phone_txt.Name = "Phone_txt"
        Me.Phone_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Phone_txt.Size = New System.Drawing.Size(486, 33)
        Me.Phone_txt.TabIndex = 552
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(491, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 25)
        Me.Label3.TabIndex = 555
        Me.Label3.Text = "العنوان"
        '
        'Address_txt
        '
        Me.Address_txt.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Address_txt.Location = New System.Drawing.Point(2, 107)
        Me.Address_txt.Name = "Address_txt"
        Me.Address_txt.Size = New System.Drawing.Size(486, 33)
        Me.Address_txt.TabIndex = 554
        '
        'Save_AG_btn
        '
        Me.Save_AG_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Save_AG_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Save_AG_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Save_AG_btn.Enabled = False
        Me.Save_AG_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Save_AG_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Save_AG_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Save_AG_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_AG_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Save_AG_btn.ForeColor = System.Drawing.Color.Black
        Me.Save_AG_btn.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.Save_AG_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Save_AG_btn.Location = New System.Drawing.Point(2, 197)
        Me.Save_AG_btn.Name = "Save_AG_btn"
        Me.Save_AG_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Save_AG_btn.Size = New System.Drawing.Size(168, 45)
        Me.Save_AG_btn.TabIndex = 556
        Me.Save_AG_btn.Text = "حفــظ"
        Me.Save_AG_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Save_AG_btn.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(492, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 25)
        Me.Label4.TabIndex = 560
        Me.Label4.Text = "رقم"
        '
        'Barcode_txt
        '
        Me.Barcode_txt.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barcode_txt.Location = New System.Drawing.Point(270, 37)
        Me.Barcode_txt.MaxLength = 500
        Me.Barcode_txt.Name = "Barcode_txt"
        Me.Barcode_txt.Size = New System.Drawing.Size(218, 32)
        Me.Barcode_txt.TabIndex = 559
        Me.Barcode_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BarError
        '
        Me.BarError.ContainerControl = Me
        Me.BarError.RightToLeft = True
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(452, 197)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(110, 45)
        Me.ExitFormButton.TabIndex = 637
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "خروج Esc"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'AddFastAgent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 243)
        Me.ControlBox = False
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Barcode_txt)
        Me.Controls.Add(Me.Save_AG_btn)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Address_txt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Phone_txt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.AG_Name_txt)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "AddFastAgent"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "أضف عميل +"
        CType(Me.BarError, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AG_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Phone_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Address_txt As System.Windows.Forms.TextBox
    Friend WithEvents Save_AG_btn As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Barcode_txt As System.Windows.Forms.TextBox
    Friend WithEvents BarError As System.Windows.Forms.ErrorProvider
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
End Class
