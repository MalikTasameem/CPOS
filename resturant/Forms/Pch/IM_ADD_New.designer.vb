<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_ADD_New
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
        Me.ConfermButton = New System.Windows.Forms.Button()
        Me.isValid_CB = New System.Windows.Forms.CheckBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
        Me.IM_Unit_cm = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Random_Barcode_btn = New System.Windows.Forms.Button()
        Me.IM_Num_txt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.KeyBoard_Btn = New System.Windows.Forms.Button()
        Me.IM_SH_txt = New System.Windows.Forms.TextBox()
        Me.Call_IM_After_Insert_CB = New System.Windows.Forms.CheckBox()
        Me.GM_Serach = New System.Windows.Forms.ComboBox()
        Me.ADD_NewGM_Btn = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Unit_cargo_txt = New System.Windows.Forms.TextBox()
        Me.Barcode_By_One_txt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.By_One_Panel = New System.Windows.Forms.Panel()
        Me.By_One_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'ConfermButton
        '
        Me.ConfermButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ConfermButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ConfermButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ConfermButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ConfermButton.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfermButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ConfermButton.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.ConfermButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ConfermButton.Location = New System.Drawing.Point(343, 308)
        Me.ConfermButton.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.ConfermButton.Name = "ConfermButton"
        Me.ConfermButton.Size = New System.Drawing.Size(131, 41)
        Me.ConfermButton.TabIndex = 443
        Me.ConfermButton.Text = "حفظ F12"
        Me.ConfermButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConfermButton.UseVisualStyleBackColor = False
        '
        'isValid_CB
        '
        Me.isValid_CB.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.isValid_CB.AutoSize = True
        Me.isValid_CB.BackColor = System.Drawing.Color.Transparent
        Me.isValid_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.isValid_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.isValid_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isValid_CB.ForeColor = System.Drawing.Color.Black
        Me.isValid_CB.Location = New System.Drawing.Point(105, 200)
        Me.isValid_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.isValid_CB.Name = "isValid_CB"
        Me.isValid_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.isValid_CB.Size = New System.Drawing.Size(74, 25)
        Me.isValid_CB.TabIndex = 669
        Me.isValid_CB.Text = "صلاحية"
        Me.isValid_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.isValid_CB.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(2, 306)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(120, 41)
        Me.ExitFormButton.TabIndex = 670
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Barcode_SH_txt
        '
        Me.Barcode_SH_txt.BackColor = System.Drawing.SystemColors.Info
        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Barcode_SH_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_SH_txt.Location = New System.Drawing.Point(103, 98)
        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
        Me.Barcode_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Barcode_SH_txt.Size = New System.Drawing.Size(326, 29)
        Me.Barcode_SH_txt.TabIndex = 676
        Me.Barcode_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'IM_Unit_cm
        '
        Me.IM_Unit_cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.IM_Unit_cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.IM_Unit_cm.BackColor = System.Drawing.SystemColors.Info
        Me.IM_Unit_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Unit_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Unit_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Unit_cm.FormattingEnabled = True
        Me.IM_Unit_cm.Location = New System.Drawing.Point(103, 130)
        Me.IM_Unit_cm.Name = "IM_Unit_cm"
        Me.IM_Unit_cm.Size = New System.Drawing.Size(194, 29)
        Me.IM_Unit_cm.TabIndex = 677
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(49, 103)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(51, 19)
        Me.Label1.TabIndex = 680
        Me.Label1.Text = "باركود :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(14, 42)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label11.Size = New System.Drawing.Size(86, 19)
        Me.Label11.TabIndex = 681
        Me.Label11.Text = "إسم الصنف :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label15.Location = New System.Drawing.Point(45, 135)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(54, 19)
        Me.Label15.TabIndex = 682
        Me.Label15.Text = "الوحدة :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(31, 10)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 19)
        Me.Label4.TabIndex = 685
        Me.Label4.Text = "التصنيف :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Random_Barcode_btn
        '
        Me.Random_Barcode_btn.BackColor = System.Drawing.Color.White
        Me.Random_Barcode_btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_refresh_1608809
        Me.Random_Barcode_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Random_Barcode_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Random_Barcode_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Random_Barcode_btn.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Random_Barcode_btn.Location = New System.Drawing.Point(431, 98)
        Me.Random_Barcode_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Random_Barcode_btn.Name = "Random_Barcode_btn"
        Me.Random_Barcode_btn.Size = New System.Drawing.Size(41, 29)
        Me.Random_Barcode_btn.TabIndex = 689
        Me.Random_Barcode_btn.UseVisualStyleBackColor = False
        '
        'IM_Num_txt
        '
        Me.IM_Num_txt.BackColor = System.Drawing.SystemColors.Info
        Me.IM_Num_txt.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Num_txt.Location = New System.Drawing.Point(103, 67)
        Me.IM_Num_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.IM_Num_txt.Name = "IM_Num_txt"
        Me.IM_Num_txt.Size = New System.Drawing.Size(369, 29)
        Me.IM_Num_txt.TabIndex = 690
        Me.IM_Num_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(18, 73)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 19)
        Me.Label9.TabIndex = 691
        Me.Label9.Text = "رقم الصنف :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'KeyBoard_Btn
        '
        Me.KeyBoard_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_Keyboard_100061
        Me.KeyBoard_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.KeyBoard_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.KeyBoard_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.KeyBoard_Btn.Location = New System.Drawing.Point(125, 306)
        Me.KeyBoard_Btn.Name = "KeyBoard_Btn"
        Me.KeyBoard_Btn.Size = New System.Drawing.Size(43, 41)
        Me.KeyBoard_Btn.TabIndex = 693
        Me.KeyBoard_Btn.UseVisualStyleBackColor = True
        '
        'IM_SH_txt
        '
        Me.IM_SH_txt.BackColor = System.Drawing.SystemColors.Info
        Me.IM_SH_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.IM_SH_txt.Location = New System.Drawing.Point(103, 36)
        Me.IM_SH_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.IM_SH_txt.Name = "IM_SH_txt"
        Me.IM_SH_txt.Size = New System.Drawing.Size(369, 29)
        Me.IM_SH_txt.TabIndex = 696
        '
        'Call_IM_After_Insert_CB
        '
        Me.Call_IM_After_Insert_CB.AutoSize = True
        Me.Call_IM_After_Insert_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Call_IM_After_Insert_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Call_IM_After_Insert_CB.Location = New System.Drawing.Point(103, 275)
        Me.Call_IM_After_Insert_CB.Name = "Call_IM_After_Insert_CB"
        Me.Call_IM_After_Insert_CB.Size = New System.Drawing.Size(179, 25)
        Me.Call_IM_After_Insert_CB.TabIndex = 698
        Me.Call_IM_After_Insert_CB.Text = "إدراج الصنف بعد الحفظ"
        Me.Call_IM_After_Insert_CB.UseVisualStyleBackColor = True
        '
        'GM_Serach
        '
        Me.GM_Serach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.GM_Serach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.GM_Serach.BackColor = System.Drawing.SystemColors.Info
        Me.GM_Serach.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GM_Serach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GM_Serach.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Serach.Location = New System.Drawing.Point(103, 4)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.Size = New System.Drawing.Size(331, 29)
        Me.GM_Serach.TabIndex = 700
        '
        'ADD_NewGM_Btn
        '
        Me.ADD_NewGM_Btn.BackColor = System.Drawing.Color.White
        Me.ADD_NewGM_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.ADD_NewGM_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ADD_NewGM_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADD_NewGM_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADD_NewGM_Btn.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADD_NewGM_Btn.Location = New System.Drawing.Point(436, 4)
        Me.ADD_NewGM_Btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ADD_NewGM_Btn.Name = "ADD_NewGM_Btn"
        Me.ADD_NewGM_Btn.Size = New System.Drawing.Size(34, 29)
        Me.ADD_NewGM_Btn.TabIndex = 699
        Me.ADD_NewGM_Btn.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(303, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 21)
        Me.Label2.TabIndex = 702
        Me.Label2.Text = "الحمولة :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Unit_cargo_txt
        '
        Me.Unit_cargo_txt.BackColor = System.Drawing.SystemColors.Info
        Me.Unit_cargo_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Unit_cargo_txt.Font = New System.Drawing.Font("Times New Roman", 15.75!)
        Me.Unit_cargo_txt.ForeColor = System.Drawing.Color.Black
        Me.Unit_cargo_txt.Location = New System.Drawing.Point(373, 129)
        Me.Unit_cargo_txt.MaxLength = 250
        Me.Unit_cargo_txt.Name = "Unit_cargo_txt"
        Me.Unit_cargo_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Unit_cargo_txt.Size = New System.Drawing.Size(55, 32)
        Me.Unit_cargo_txt.TabIndex = 701
        Me.Unit_cargo_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Barcode_By_One_txt
        '
        Me.Barcode_By_One_txt.BackColor = System.Drawing.SystemColors.Info
        Me.Barcode_By_One_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_By_One_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Barcode_By_One_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_By_One_txt.Location = New System.Drawing.Point(48, 2)
        Me.Barcode_By_One_txt.Name = "Barcode_By_One_txt"
        Me.Barcode_By_One_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Barcode_By_One_txt.Size = New System.Drawing.Size(276, 29)
        Me.Barcode_By_One_txt.TabIndex = 703
        Me.Barcode_By_One_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(327, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 19)
        Me.Label3.TabIndex = 704
        Me.Label3.Text = "باركود القطعة :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'By_One_Panel
        '
        Me.By_One_Panel.Controls.Add(Me.Barcode_By_One_txt)
        Me.By_One_Panel.Controls.Add(Me.Label3)
        Me.By_One_Panel.Location = New System.Drawing.Point(0, 163)
        Me.By_One_Panel.Name = "By_One_Panel"
        Me.By_One_Panel.Size = New System.Drawing.Size(426, 33)
        Me.By_One_Panel.TabIndex = 705
        '
        'IM_ADD_New
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 30.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 349)
        Me.ControlBox = False
        Me.Controls.Add(Me.By_One_Panel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Unit_cargo_txt)
        Me.Controls.Add(Me.GM_Serach)
        Me.Controls.Add(Me.ADD_NewGM_Btn)
        Me.Controls.Add(Me.Call_IM_After_Insert_CB)
        Me.Controls.Add(Me.IM_SH_txt)
        Me.Controls.Add(Me.KeyBoard_Btn)
        Me.Controls.Add(Me.IM_Num_txt)
        Me.Controls.Add(Me.IM_Unit_cm)
        Me.Controls.Add(Me.isValid_CB)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Random_Barcode_btn)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Barcode_SH_txt)
        Me.Controls.Add(Me.ConfermButton)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.MaximizeBox = False
        Me.Name = "IM_ADD_New"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إضافة صنف +"
        Me.By_One_Panel.ResumeLayout(False)
        Me.By_One_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ConfermButton As System.Windows.Forms.Button
    Friend WithEvents isValid_CB As System.Windows.Forms.CheckBox
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Barcode_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents IM_Unit_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Random_Barcode_btn As System.Windows.Forms.Button
    Friend WithEvents IM_Num_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents KeyBoard_Btn As System.Windows.Forms.Button
    Friend WithEvents IM_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents Call_IM_After_Insert_CB As System.Windows.Forms.CheckBox
    Friend WithEvents GM_Serach As System.Windows.Forms.ComboBox
    Friend WithEvents ADD_NewGM_Btn As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Unit_cargo_txt As System.Windows.Forms.TextBox
    Friend WithEvents Barcode_By_One_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents By_One_Panel As System.Windows.Forms.Panel
End Class
