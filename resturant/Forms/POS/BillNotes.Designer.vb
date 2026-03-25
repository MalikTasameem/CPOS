<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BillNotes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BillNotes))
        Me.Notes_txt = New System.Windows.Forms.TextBox()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.Select_AG_btn = New System.Windows.Forms.Button()
        Me.KeyBoard_Btn = New System.Windows.Forms.Button()
        Me.Phone_Or_Name_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Notes_txt
        '
        Me.Notes_txt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Notes_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Notes_txt.Location = New System.Drawing.Point(1, 84)
        Me.Notes_txt.Multiline = True
        Me.Notes_txt.Name = "Notes_txt"
        Me.Notes_txt.Size = New System.Drawing.Size(424, 177)
        Me.Notes_txt.TabIndex = 0
        '
        'Back_Btn
        '
        Me.Back_Btn.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Back_Btn.BackColor = System.Drawing.Color.White
        Me.Back_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.Users_Exit_icon
        Me.Back_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.Location = New System.Drawing.Point(349, 267)
        Me.Back_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.Size = New System.Drawing.Size(76, 50)
        Me.Back_Btn.TabIndex = 557
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'Select_AG_btn
        '
        Me.Select_AG_btn.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Select_AG_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Select_AG_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Select_AG_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Select_AG_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Select_AG_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Select_AG_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Select_AG_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Select_AG_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Select_AG_btn.ForeColor = System.Drawing.Color.Black
        Me.Select_AG_btn.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.Select_AG_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Select_AG_btn.Location = New System.Drawing.Point(2, 2)
        Me.Select_AG_btn.Name = "Select_AG_btn"
        Me.Select_AG_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Select_AG_btn.Size = New System.Drawing.Size(423, 45)
        Me.Select_AG_btn.TabIndex = 556
        Me.Select_AG_btn.Text = "تطبيــــق"
        Me.Select_AG_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Select_AG_btn.UseVisualStyleBackColor = False
        '
        'KeyBoard_Btn
        '
        Me.KeyBoard_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_Keyboard_100061
        Me.KeyBoard_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.KeyBoard_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.KeyBoard_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.KeyBoard_Btn.Location = New System.Drawing.Point(1, 267)
        Me.KeyBoard_Btn.Name = "KeyBoard_Btn"
        Me.KeyBoard_Btn.Size = New System.Drawing.Size(66, 50)
        Me.KeyBoard_Btn.TabIndex = 563
        Me.KeyBoard_Btn.UseVisualStyleBackColor = True
        '
        'Phone_Or_Name_txt
        '
        Me.Phone_Or_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Phone_Or_Name_txt.Location = New System.Drawing.Point(1, 49)
        Me.Phone_Or_Name_txt.Name = "Phone_Or_Name_txt"
        Me.Phone_Or_Name_txt.Size = New System.Drawing.Size(312, 33)
        Me.Phone_Or_Name_txt.TabIndex = 564
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(317, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 20)
        Me.Label1.TabIndex = 565
        Me.Label1.Text = "الهاتف أو الإسم"
        '
        'BillNotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 318)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Phone_Or_Name_txt)
        Me.Controls.Add(Me.KeyBoard_Btn)
        Me.Controls.Add(Me.Back_Btn)
        Me.Controls.Add(Me.Select_AG_btn)
        Me.Controls.Add(Me.Notes_txt)
        Me.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "BillNotes"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ملاحظات على الفاتورة"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents Select_AG_btn As System.Windows.Forms.Button
    Friend WithEvents KeyBoard_Btn As System.Windows.Forms.Button
    Friend WithEvents Phone_Or_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
