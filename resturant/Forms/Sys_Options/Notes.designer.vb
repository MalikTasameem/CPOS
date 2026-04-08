<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Notes
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Notes))
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.S_listBox = New System.Windows.Forms.ListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DeleteSButton = New System.Windows.Forms.Button()
        Me.NewSButton = New System.Windows.Forms.Button()
        Me.EditSButton = New System.Windows.Forms.Button()
        Me.SaveSButton = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.SNameTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Price_txt = New System.Windows.Forms.TextBox()
        Me.Without_Rd = New System.Windows.Forms.RadioButton()
        Me.ADD_Rd = New System.Windows.Forms.RadioButton()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.GM_Group_CM = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TitleBar_Panel.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.TitleBar_Panel.Size = New System.Drawing.Size(498, 40)
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
        Me.TopTitle_LB.Location = New System.Drawing.Point(414, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(78, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "الملاحظات"
        '
        'S_listBox
        '
        Me.S_listBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.S_listBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.S_listBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.S_listBox.FormattingEnabled = True
        Me.S_listBox.ItemHeight = 21
        Me.S_listBox.Location = New System.Drawing.Point(3, 25)
        Me.S_listBox.Name = "S_listBox"
        Me.S_listBox.Size = New System.Drawing.Size(348, 412)
        Me.S_listBox.TabIndex = 430
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.GroupBox1.Controls.Add(Me.S_listBox)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 142)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(354, 440)
        Me.GroupBox1.TabIndex = 443
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "القائمة"
        '
        'DeleteSButton
        '
        Me.DeleteSButton.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DeleteSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteSButton.Enabled = False
        Me.DeleteSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.DeleteSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteSButton.Location = New System.Drawing.Point(360, 300)
        Me.DeleteSButton.Name = "DeleteSButton"
        Me.DeleteSButton.Size = New System.Drawing.Size(132, 40)
        Me.DeleteSButton.TabIndex = 442
        Me.DeleteSButton.Text = "حذف"
        Me.DeleteSButton.UseVisualStyleBackColor = False
        '
        'NewSButton
        '
        Me.NewSButton.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.NewSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NewSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.NewSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NewSButton.Location = New System.Drawing.Point(360, 168)
        Me.NewSButton.Name = "NewSButton"
        Me.NewSButton.Size = New System.Drawing.Size(132, 40)
        Me.NewSButton.TabIndex = 441
        Me.NewSButton.Text = "جديد"
        Me.NewSButton.UseVisualStyleBackColor = False
        '
        'EditSButton
        '
        Me.EditSButton.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.EditSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditSButton.Enabled = False
        Me.EditSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EditSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.EditSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.EditSButton.Location = New System.Drawing.Point(360, 256)
        Me.EditSButton.Name = "EditSButton"
        Me.EditSButton.Size = New System.Drawing.Size(132, 40)
        Me.EditSButton.TabIndex = 440
        Me.EditSButton.Text = "تعديل"
        Me.EditSButton.UseVisualStyleBackColor = False
        '
        'SaveSButton
        '
        Me.SaveSButton.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SaveSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveSButton.Enabled = False
        Me.SaveSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.SaveSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveSButton.Location = New System.Drawing.Point(360, 212)
        Me.SaveSButton.Name = "SaveSButton"
        Me.SaveSButton.Size = New System.Drawing.Size(132, 40)
        Me.SaveSButton.TabIndex = 439
        Me.SaveSButton.Text = "حفظ"
        Me.SaveSButton.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(293, 82)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(59, 21)
        Me.Label15.TabIndex = 437
        Me.Label15.Text = "الوصف"
        '
        'SNameTextBox
        '
        Me.SNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SNameTextBox.Enabled = False
        Me.SNameTextBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SNameTextBox.Location = New System.Drawing.Point(87, 78)
        Me.SNameTextBox.MaxLength = 500
        Me.SNameTextBox.Name = "SNameTextBox"
        Me.SNameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SNameTextBox.Size = New System.Drawing.Size(203, 29)
        Me.SNameTextBox.TabIndex = 438
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(293, 114)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 21)
        Me.Label3.TabIndex = 452
        Me.Label3.Text = "سعر البيع"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Price_txt
        '
        Me.Price_txt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Price_txt.Enabled = False
        Me.Price_txt.Font = New System.Drawing.Font("Stencil", 13.5!)
        Me.Price_txt.Location = New System.Drawing.Point(87, 110)
        Me.Price_txt.Name = "Price_txt"
        Me.Price_txt.Size = New System.Drawing.Size(203, 29)
        Me.Price_txt.TabIndex = 451
        Me.Price_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Without_Rd
        '
        Me.Without_Rd.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Without_Rd.AutoSize = True
        Me.Without_Rd.Checked = True
        Me.Without_Rd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Without_Rd.Location = New System.Drawing.Point(5, 81)
        Me.Without_Rd.Name = "Without_Rd"
        Me.Without_Rd.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Without_Rd.Size = New System.Drawing.Size(76, 25)
        Me.Without_Rd.TabIndex = 454
        Me.Without_Rd.TabStop = True
        Me.Without_Rd.Text = "ملاحظة"
        Me.Without_Rd.UseVisualStyleBackColor = True
        '
        'ADD_Rd
        '
        Me.ADD_Rd.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ADD_Rd.AutoSize = True
        Me.ADD_Rd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADD_Rd.Location = New System.Drawing.Point(13, 113)
        Me.ADD_Rd.Name = "ADD_Rd"
        Me.ADD_Rd.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ADD_Rd.Size = New System.Drawing.Size(68, 25)
        Me.ADD_Rd.TabIndex = 453
        Me.ADD_Rd.Text = "إضافة"
        Me.ADD_Rd.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(294, 50)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(116, 21)
        Me.Label24.TabIndex = 456
        Me.Label24.Text = "قائمة الملاحظات"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GM_Group_CM
        '
        Me.GM_Group_CM.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.GM_Group_CM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.GM_Group_CM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.GM_Group_CM.BackColor = System.Drawing.Color.White
        Me.GM_Group_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GM_Group_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GM_Group_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GM_Group_CM.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GM_Group_CM.FormattingEnabled = True
        Me.GM_Group_CM.IntegralHeight = False
        Me.GM_Group_CM.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Group_CM.Location = New System.Drawing.Point(44, 44)
        Me.GM_Group_CM.Name = "GM_Group_CM"
        Me.GM_Group_CM.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GM_Group_CM.Size = New System.Drawing.Size(246, 29)
        Me.GM_Group_CM.TabIndex = 458
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(4, 45)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(38, 29)
        Me.Button2.TabIndex = 457
        Me.Button2.Text = "+"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Notes
        '
        Me.ClientSize = New System.Drawing.Size(498, 584)
        Me.ControlBox = False
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.GM_Group_CM)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Without_Rd)
        Me.Controls.Add(Me.ADD_Rd)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Price_txt)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DeleteSButton)
        Me.Controls.Add(Me.NewSButton)
        Me.Controls.Add(Me.EditSButton)
        Me.Controls.Add(Me.SaveSButton)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.SNameTextBox)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Notes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الملاحظات"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button

    Friend WithEvents S_listBox As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DeleteSButton As System.Windows.Forms.Button
    Friend WithEvents NewSButton As System.Windows.Forms.Button
    Friend WithEvents EditSButton As System.Windows.Forms.Button
    Friend WithEvents SaveSButton As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents SNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Price_txt As System.Windows.Forms.TextBox
    Friend WithEvents Without_Rd As System.Windows.Forms.RadioButton
    Friend WithEvents ADD_Rd As System.Windows.Forms.RadioButton
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents GM_Group_CM As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class