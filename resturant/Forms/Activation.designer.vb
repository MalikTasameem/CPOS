<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Activation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Activation))
        Me.ConfirmActivButton = New System.Windows.Forms.Button()
        Me.ProductCodeTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.InsertedActivationCodeTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.KayBoardButton = New System.Windows.Forms.Button()
        Me.SerialNumTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ConfirmActivButton
        '
        Me.ConfirmActivButton.Enabled = False
        Me.ConfirmActivButton.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfirmActivButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ConfirmActivButton.Location = New System.Drawing.Point(204, 174)
        Me.ConfirmActivButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ConfirmActivButton.Name = "ConfirmActivButton"
        Me.ConfirmActivButton.Size = New System.Drawing.Size(203, 45)
        Me.ConfirmActivButton.TabIndex = 3
        Me.ConfirmActivButton.Text = "تفعيل الرمز"
        Me.ConfirmActivButton.UseVisualStyleBackColor = True
        '
        'ProductCodeTextBox
        '
        Me.ProductCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ProductCodeTextBox.Font = New System.Drawing.Font("Times New Roman", 13.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.ProductCodeTextBox.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.ProductCodeTextBox.Location = New System.Drawing.Point(4, 60)
        Me.ProductCodeTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ProductCodeTextBox.MaxLength = 39
        Me.ProductCodeTextBox.Name = "ProductCodeTextBox"
        Me.ProductCodeTextBox.ReadOnly = True
        Me.ProductCodeTextBox.Size = New System.Drawing.Size(635, 29)
        Me.ProductCodeTextBox.TabIndex = 4
        Me.ProductCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(641, 138)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(104, 23)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "رمز التفعيل : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'InsertedActivationCodeTextBox
        '
        Me.InsertedActivationCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.InsertedActivationCodeTextBox.Font = New System.Drawing.Font("Times New Roman", 13.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.InsertedActivationCodeTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.InsertedActivationCodeTextBox.Location = New System.Drawing.Point(3, 134)
        Me.InsertedActivationCodeTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.InsertedActivationCodeTextBox.MaxLength = 39
        Me.InsertedActivationCodeTextBox.Name = "InsertedActivationCodeTextBox"
        Me.InsertedActivationCodeTextBox.Size = New System.Drawing.Size(635, 29)
        Me.InsertedActivationCodeTextBox.TabIndex = 6
        Me.InsertedActivationCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(320, 9)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(138, 25)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "نسخة غير مفعلة"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(2, 1)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(111, 57)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(271, 3)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(46, 35)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 22
        Me.PictureBox2.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(642, 63)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(99, 23)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "رمز المنتج :  "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'KayBoardButton
        '
        Me.KayBoardButton.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.KayBoardButton.BackColor = System.Drawing.Color.White
        Me.KayBoardButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.KayBoardButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.KayBoardButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.KayBoardButton.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.KayBoardButton.Location = New System.Drawing.Point(620, 194)
        Me.KayBoardButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.KayBoardButton.Name = "KayBoardButton"
        Me.KayBoardButton.Size = New System.Drawing.Size(128, 30)
        Me.KayBoardButton.TabIndex = 483
        Me.KayBoardButton.Text = "لوحة المفاتيح"
        Me.KayBoardButton.UseVisualStyleBackColor = False
        '
        'SerialNumTextBox
        '
        Me.SerialNumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SerialNumTextBox.Font = New System.Drawing.Font("Times New Roman", 13.75!, System.Drawing.FontStyle.Bold)
        Me.SerialNumTextBox.Location = New System.Drawing.Point(173, 97)
        Me.SerialNumTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SerialNumTextBox.MaxLength = 8
        Me.SerialNumTextBox.Name = "SerialNumTextBox"
        Me.SerialNumTextBox.Size = New System.Drawing.Size(264, 29)
        Me.SerialNumTextBox.TabIndex = 484
        Me.SerialNumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(440, 100)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(111, 23)
        Me.Label1.TabIndex = 485
        Me.Label1.Text = "الرقم السري :  "
        '
        'Activation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 225)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SerialNumTextBox)
        Me.Controls.Add(Me.KayBoardButton)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.InsertedActivationCodeTextBox)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ConfirmActivButton)
        Me.Controls.Add(Me.ProductCodeTextBox)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label3)
        Me.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Activation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تفعيل النظام"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ConfirmActivButton As System.Windows.Forms.Button
    Friend WithEvents ProductCodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents InsertedActivationCodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents KayBoardButton As System.Windows.Forms.Button
    Friend WithEvents SerialNumTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
