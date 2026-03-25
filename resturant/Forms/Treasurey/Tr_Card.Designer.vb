<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tr_Card
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
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Delete_butt = New System.Windows.Forms.Button()
        Me.Edit_butt = New System.Windows.Forms.Button()
        Me.Save_butt = New System.Windows.Forms.Button()
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.New_butt = New System.Windows.Forms.Button()
        Me.Tr_BalanceTextBox = New System.Windows.Forms.TextBox()
        Me.Tr_Name_txtb = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Tr_BankNum_TextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Total_BS_Txt = New System.Windows.Forms.TextBox()
        Me.S_listBox = New System.Windows.Forms.ListBox()
        Me.FieldsPanel = New System.Windows.Forms.Panel()
        Me.ACC_CODE_TXT = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TR_NAMEErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.FieldsPanel.SuspendLayout()
        CType(Me.TR_NAMEErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(488, 353)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(115, 40)
        Me.ExitFormButton.TabIndex = 667
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Delete_butt
        '
        Me.Delete_butt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Delete_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Delete_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Delete_butt.Enabled = False
        Me.Delete_butt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Delete_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Delete_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Delete_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Delete_butt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Delete_butt.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.Delete_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Delete_butt.Location = New System.Drawing.Point(4, 354)
        Me.Delete_butt.Name = "Delete_butt"
        Me.Delete_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Delete_butt.Size = New System.Drawing.Size(110, 40)
        Me.Delete_butt.TabIndex = 664
        Me.Delete_butt.Text = " حـذف"
        Me.Delete_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Delete_butt.UseVisualStyleBackColor = False
        '
        'Edit_butt
        '
        Me.Edit_butt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Edit_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Edit_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Edit_butt.Enabled = False
        Me.Edit_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Edit_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Edit_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Edit_butt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Edit_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Edit_butt.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.Edit_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Edit_butt.Location = New System.Drawing.Point(116, 354)
        Me.Edit_butt.Name = "Edit_butt"
        Me.Edit_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Edit_butt.Size = New System.Drawing.Size(110, 40)
        Me.Edit_butt.TabIndex = 663
        Me.Edit_butt.TabStop = False
        Me.Edit_butt.Text = "تعديـــل"
        Me.Edit_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Edit_butt.UseVisualStyleBackColor = False
        '
        'Save_butt
        '
        Me.Save_butt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Save_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Save_butt.Enabled = False
        Me.Save_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Save_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_butt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Save_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Save_butt.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.Save_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Save_butt.Location = New System.Drawing.Point(228, 354)
        Me.Save_butt.Name = "Save_butt"
        Me.Save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Save_butt.Size = New System.Drawing.Size(110, 40)
        Me.Save_butt.TabIndex = 661
        Me.Save_butt.TabStop = False
        Me.Save_butt.Text = "حفظ"
        Me.Save_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Save_butt.UseVisualStyleBackColor = False
        '
        'MetroToolTip1
        '
        Me.MetroToolTip1.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip1.StyleManager = Nothing
        Me.MetroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'New_butt
        '
        Me.New_butt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.New_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.New_butt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.New_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.New_butt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.New_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.New_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.New_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.New_butt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.New_butt.ForeColor = System.Drawing.Color.Black
        Me.New_butt.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.New_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.New_butt.Location = New System.Drawing.Point(340, 354)
        Me.New_butt.Name = "New_butt"
        Me.New_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.New_butt.Size = New System.Drawing.Size(110, 40)
        Me.New_butt.TabIndex = 662
        Me.New_butt.Text = " جديـد"
        Me.New_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.New_butt.UseVisualStyleBackColor = False
        '
        'Tr_BalanceTextBox
        '
        Me.Tr_BalanceTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Tr_BalanceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tr_BalanceTextBox.Font = New System.Drawing.Font("Stencil", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tr_BalanceTextBox.ForeColor = System.Drawing.Color.DarkGreen
        Me.Tr_BalanceTextBox.Location = New System.Drawing.Point(16, 176)
        Me.Tr_BalanceTextBox.MaxLength = 250
        Me.Tr_BalanceTextBox.Name = "Tr_BalanceTextBox"
        Me.Tr_BalanceTextBox.ReadOnly = True
        Me.Tr_BalanceTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Tr_BalanceTextBox.Size = New System.Drawing.Size(282, 32)
        Me.Tr_BalanceTextBox.TabIndex = 292
        Me.Tr_BalanceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Tr_Name_txtb
        '
        Me.Tr_Name_txtb.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Tr_Name_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tr_Name_txtb.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tr_Name_txtb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Tr_Name_txtb.Location = New System.Drawing.Point(19, 36)
        Me.Tr_Name_txtb.MaxLength = 200
        Me.Tr_Name_txtb.Name = "Tr_Name_txtb"
        Me.Tr_Name_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Tr_Name_txtb.Size = New System.Drawing.Size(279, 33)
        Me.Tr_Name_txtb.TabIndex = 287
        Me.Tr_Name_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(182, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(113, 23)
        Me.Label4.TabIndex = 286
        Me.Label4.Text = "إسم الخزينــــة :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tr_BankNum_TextBox
        '
        Me.Tr_BankNum_TextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Tr_BankNum_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tr_BankNum_TextBox.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tr_BankNum_TextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Tr_BankNum_TextBox.Location = New System.Drawing.Point(17, 106)
        Me.Tr_BankNum_TextBox.MaxLength = 200
        Me.Tr_BankNum_TextBox.Name = "Tr_BankNum_TextBox"
        Me.Tr_BankNum_TextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Tr_BankNum_TextBox.Size = New System.Drawing.Size(281, 32)
        Me.Tr_BankNum_TextBox.TabIndex = 289
        Me.Tr_BankNum_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(186, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(106, 23)
        Me.Label1.TabIndex = 288
        Me.Label1.Text = "رقم الحسـاب :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(219, 150)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(73, 23)
        Me.Label2.TabIndex = 291
        Me.Label2.Text = "الرصيــد :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Total_BS_Txt
        '
        Me.Total_BS_Txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Total_BS_Txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Total_BS_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_BS_Txt.Font = New System.Drawing.Font("Stencil", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Total_BS_Txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Total_BS_Txt.Location = New System.Drawing.Point(313, 307)
        Me.Total_BS_Txt.MaxLength = 250
        Me.Total_BS_Txt.Name = "Total_BS_Txt"
        Me.Total_BS_Txt.ReadOnly = True
        Me.Total_BS_Txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Total_BS_Txt.Size = New System.Drawing.Size(290, 32)
        Me.Total_BS_Txt.TabIndex = 666
        Me.Total_BS_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'S_listBox
        '
        Me.S_listBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.S_listBox.FormattingEnabled = True
        Me.S_listBox.ItemHeight = 21
        Me.S_listBox.Location = New System.Drawing.Point(314, 5)
        Me.S_listBox.Name = "S_listBox"
        Me.S_listBox.Size = New System.Drawing.Size(289, 298)
        Me.S_listBox.TabIndex = 668
        '
        'FieldsPanel
        '
        Me.FieldsPanel.Controls.Add(Me.ACC_CODE_TXT)
        Me.FieldsPanel.Controls.Add(Me.Label12)
        Me.FieldsPanel.Controls.Add(Me.Label4)
        Me.FieldsPanel.Controls.Add(Me.Tr_BalanceTextBox)
        Me.FieldsPanel.Controls.Add(Me.Label2)
        Me.FieldsPanel.Controls.Add(Me.Label1)
        Me.FieldsPanel.Controls.Add(Me.Tr_Name_txtb)
        Me.FieldsPanel.Controls.Add(Me.Tr_BankNum_TextBox)
        Me.FieldsPanel.Location = New System.Drawing.Point(4, 5)
        Me.FieldsPanel.Name = "FieldsPanel"
        Me.FieldsPanel.Size = New System.Drawing.Size(302, 334)
        Me.FieldsPanel.TabIndex = 669
        '
        'ACC_CODE_TXT
        '
        Me.ACC_CODE_TXT.BackColor = System.Drawing.SystemColors.WindowText
        Me.ACC_CODE_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ACC_CODE_TXT.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ACC_CODE_TXT.ForeColor = System.Drawing.Color.White
        Me.ACC_CODE_TXT.Location = New System.Drawing.Point(3, 288)
        Me.ACC_CODE_TXT.MaxLength = 500
        Me.ACC_CODE_TXT.Name = "ACC_CODE_TXT"
        Me.ACC_CODE_TXT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ACC_CODE_TXT.Size = New System.Drawing.Size(205, 29)
        Me.ACC_CODE_TXT.TabIndex = 618
        Me.ACC_CODE_TXT.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(211, 293)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label12.Size = New System.Drawing.Size(81, 19)
        Me.Label12.TabIndex = 617
        Me.Label12.Text = "كود الحساب :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label12.Visible = False
        '
        'TR_NAMEErrorProvider
        '
        Me.TR_NAMEErrorProvider.ContainerControl = Me
        Me.TR_NAMEErrorProvider.RightToLeft = True
        '
        'Tr_Card
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(605, 397)
        Me.ControlBox = False
        Me.Controls.Add(Me.FieldsPanel)
        Me.Controls.Add(Me.S_listBox)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Delete_butt)
        Me.Controls.Add(Me.Edit_butt)
        Me.Controls.Add(Me.Save_butt)
        Me.Controls.Add(Me.New_butt)
        Me.Controls.Add(Me.Total_BS_Txt)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "Tr_Card"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الخزينة"
        Me.FieldsPanel.ResumeLayout(False)
        Me.FieldsPanel.PerformLayout()
        CType(Me.TR_NAMEErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Delete_butt As System.Windows.Forms.Button
    Friend WithEvents Edit_butt As System.Windows.Forms.Button
    Friend WithEvents Save_butt As System.Windows.Forms.Button
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
    Friend WithEvents New_butt As System.Windows.Forms.Button
    Friend WithEvents Total_BS_Txt As System.Windows.Forms.TextBox
    Friend WithEvents Tr_BalanceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Tr_Name_txtb As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Tr_BankNum_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents S_listBox As System.Windows.Forms.ListBox
    Friend WithEvents FieldsPanel As System.Windows.Forms.Panel
    Friend WithEvents TR_NAMEErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label12 As Label
    Friend WithEvents ACC_CODE_TXT As TextBox
End Class
