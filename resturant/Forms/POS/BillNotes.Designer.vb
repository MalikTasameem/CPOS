<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BillNotes
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BillNotes))
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.Notes_txt = New System.Windows.Forms.TextBox()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.Select_AG_btn = New System.Windows.Forms.Button()
        Me.KeyBoard_Btn = New System.Windows.Forms.Button()
        Me.Phone_Or_Name_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TitleBar_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(427, 40)
        Me.TitleBar_Panel.TabIndex = 999
        Me.TitleBar_Panel.Tag = "HEADER"
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.ExitFormButton.FlatAppearance.BorderSize = 0
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.Color.White
        Me.ExitFormButton.Location = New System.Drawing.Point(0, 0)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(45, 40)
        Me.ExitFormButton.TabIndex = 3
        Me.ExitFormButton.Tag = "APP_CONTROL"
        Me.ExitFormButton.Text = "X"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'TopTitle_LB
        '
        Me.TopTitle_LB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TopTitle_LB.AutoSize = True
        Me.TopTitle_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TopTitle_LB.ForeColor = System.Drawing.Color.White
        Me.TopTitle_LB.Location = New System.Drawing.Point(260, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(152, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "ملاحظات على الفاتورة"
        '
        'Notes_txt
        '
        Me.Notes_txt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Notes_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Notes_txt.Location = New System.Drawing.Point(1, 124)
        Me.Notes_txt.Multiline = True
        Me.Notes_txt.Name = "Notes_txt"
        Me.Notes_txt.Size = New System.Drawing.Size(424, 177)
        Me.Notes_txt.TabIndex = 0
        '
        'Back_Btn
        '
        Me.Back_Btn.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Back_Btn.BackColor = System.Drawing.Color.IndianRed
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Back_Btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.ForeColor = System.Drawing.Color.White
        Me.Back_Btn.Location = New System.Drawing.Point(317, 307)
        Me.Back_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.Size = New System.Drawing.Size(108, 50)
        Me.Back_Btn.TabIndex = 557
        Me.Back_Btn.Text = "خروج Esc"
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'Select_AG_btn
        '
        Me.Select_AG_btn.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Select_AG_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Select_AG_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Select_AG_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Select_AG_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Select_AG_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Select_AG_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Select_AG_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Select_AG_btn.ForeColor = System.Drawing.Color.Black
        Me.Select_AG_btn.Location = New System.Drawing.Point(2, 42)
        Me.Select_AG_btn.Name = "Select_AG_btn"
        Me.Select_AG_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Select_AG_btn.Size = New System.Drawing.Size(423, 45)
        Me.Select_AG_btn.TabIndex = 556
        Me.Select_AG_btn.Text = "تطبيــــق"
        Me.Select_AG_btn.UseVisualStyleBackColor = False
        '
        'KeyBoard_Btn
        '
        Me.KeyBoard_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.KeyBoard_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.KeyBoard_Btn.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyBoard_Btn.Location = New System.Drawing.Point(1, 307)
        Me.KeyBoard_Btn.Name = "KeyBoard_Btn"
        Me.KeyBoard_Btn.Size = New System.Drawing.Size(92, 50)
        Me.KeyBoard_Btn.TabIndex = 563
        Me.KeyBoard_Btn.Text = "⌨️"
        Me.KeyBoard_Btn.UseVisualStyleBackColor = True
        '
        'Phone_Or_Name_txt
        '
        Me.Phone_Or_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Phone_Or_Name_txt.Location = New System.Drawing.Point(1, 89)
        Me.Phone_Or_Name_txt.Name = "Phone_Or_Name_txt"
        Me.Phone_Or_Name_txt.Size = New System.Drawing.Size(312, 33)
        Me.Phone_Or_Name_txt.TabIndex = 564
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(317, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 20)
        Me.Label1.TabIndex = 565
        Me.Label1.Text = "الهاتف أو الإسم"
        '
        'BillNotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 358)
        Me.ControlBox = False
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Phone_Or_Name_txt)
        Me.Controls.Add(Me.KeyBoard_Btn)
        Me.Controls.Add(Me.Back_Btn)
        Me.Controls.Add(Me.Select_AG_btn)
        Me.Controls.Add(Me.Notes_txt)
        Me.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "BillNotes"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ملاحظات على الفاتورة"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button

    Friend WithEvents Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents Select_AG_btn As System.Windows.Forms.Button
    Friend WithEvents KeyBoard_Btn As System.Windows.Forms.Button
    Friend WithEvents Phone_Or_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class