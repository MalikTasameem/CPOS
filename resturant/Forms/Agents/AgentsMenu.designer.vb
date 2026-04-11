<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AgentsMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AgentsMenu))
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.MaxFormButton = New System.Windows.Forms.Button()
        Me.MinFormButton = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.New_AG_Btn = New System.Windows.Forms.Button()
        Me.Select_AG_btn = New System.Windows.Forms.Button()
        Me.Cash_AG_btn = New System.Windows.Forms.Button()
        Me.AG_Account_btn = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Current_QTY = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
        Me.BillPictureBox = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.AG_Type_cm = New System.Windows.Forms.ComboBox()
        Me.Note_Lb = New System.Windows.Forms.Label()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.is_By_PhoneCB = New System.Windows.Forms.CheckBox()
        Me.Agents_Panel = New System.Windows.Forms.Panel()
        Me.AG_Cm = New resturant.FSearch_Filter()
        Me.TitleBar_Panel.SuspendLayout()
        CType(Me.BillPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.MinFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.MaxFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(894, 40)
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
        'MaxFormButton
        '
        Me.MaxFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MaxFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MaxFormButton.FlatAppearance.BorderSize = 0
        Me.MaxFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MaxFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.MaxFormButton.ForeColor = System.Drawing.Color.White
        Me.MaxFormButton.Location = New System.Drawing.Point(45, 0)
        Me.MaxFormButton.Name = "MaxFormButton"
        Me.MaxFormButton.Size = New System.Drawing.Size(45, 40)
        Me.MaxFormButton.TabIndex = 2
        Me.MaxFormButton.Tag = "APP_CONTROL"
        Me.MaxFormButton.Text = "⬜"
        Me.MaxFormButton.UseVisualStyleBackColor = False
        '
        'MinFormButton
        '
        Me.MinFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MinFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MinFormButton.FlatAppearance.BorderSize = 0
        Me.MinFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MinFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.MinFormButton.ForeColor = System.Drawing.Color.White
        Me.MinFormButton.Location = New System.Drawing.Point(90, 0)
        Me.MinFormButton.Name = "MinFormButton"
        Me.MinFormButton.Size = New System.Drawing.Size(45, 40)
        Me.MinFormButton.TabIndex = 1
        Me.MinFormButton.Tag = "APP_CONTROL"
        Me.MinFormButton.Text = "ـ"
        Me.MinFormButton.UseVisualStyleBackColor = False
        '
        'TopTitle_LB
        '
        Me.TopTitle_LB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TopTitle_LB.AutoSize = True
        Me.TopTitle_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TopTitle_LB.ForeColor = System.Drawing.Color.White
        Me.TopTitle_LB.Location = New System.Drawing.Point(740, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(132, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "إدراج عميل للفاتورة"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(461, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(77, 17)
        Me.Label1.TabIndex = 549
        Me.Label1.Text = "إسم الزبون :"
        '
        'New_AG_Btn
        '
        Me.New_AG_Btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.New_AG_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.New_AG_Btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.New_AG_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.New_AG_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.New_AG_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.New_AG_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.New_AG_Btn.ForeColor = System.Drawing.Color.Black
        Me.New_AG_Btn.Location = New System.Drawing.Point(405, 281)
        Me.New_AG_Btn.Name = "New_AG_Btn"
        Me.New_AG_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.New_AG_Btn.Size = New System.Drawing.Size(150, 45)
        Me.New_AG_Btn.TabIndex = 550
        Me.New_AG_Btn.Text = "زبون جديد"
        Me.New_AG_Btn.UseVisualStyleBackColor = False
        '
        'Select_AG_btn
        '
        Me.Select_AG_btn.BackColor = System.Drawing.Color.White
        Me.Select_AG_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Select_AG_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Select_AG_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Select_AG_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Select_AG_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Select_AG_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Select_AG_btn.ForeColor = System.Drawing.Color.Black
        Me.Select_AG_btn.Location = New System.Drawing.Point(405, 186)
        Me.Select_AG_btn.Name = "Select_AG_btn"
        Me.Select_AG_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Select_AG_btn.Size = New System.Drawing.Size(150, 45)
        Me.Select_AG_btn.TabIndex = 551
        Me.Select_AG_btn.Text = "إختيار الزبون"
        Me.Select_AG_btn.UseVisualStyleBackColor = False
        '
        'Cash_AG_btn
        '
        Me.Cash_AG_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Cash_AG_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cash_AG_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Cash_AG_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Cash_AG_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Cash_AG_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cash_AG_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cash_AG_btn.ForeColor = System.Drawing.Color.Black
        Me.Cash_AG_btn.Location = New System.Drawing.Point(405, 233)
        Me.Cash_AG_btn.Name = "Cash_AG_btn"
        Me.Cash_AG_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Cash_AG_btn.Size = New System.Drawing.Size(150, 45)
        Me.Cash_AG_btn.TabIndex = 552
        Me.Cash_AG_btn.Text = "زبون نقدي"
        Me.Cash_AG_btn.UseVisualStyleBackColor = False
        '
        'AG_Account_btn
        '
        Me.AG_Account_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.AG_Account_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AG_Account_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.AG_Account_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.AG_Account_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.AG_Account_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AG_Account_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Account_btn.ForeColor = System.Drawing.Color.Black
        Me.AG_Account_btn.Location = New System.Drawing.Point(405, 330)
        Me.AG_Account_btn.Name = "AG_Account_btn"
        Me.AG_Account_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Account_btn.Size = New System.Drawing.Size(150, 45)
        Me.AG_Account_btn.TabIndex = 554
        Me.AG_Account_btn.Text = "كشف حساب"
        Me.AG_Account_btn.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label12.Location = New System.Drawing.Point(308, 157)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label12.Size = New System.Drawing.Size(51, 20)
        Me.Label12.TabIndex = 615
        Me.Label12.Text = "الرصيد"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Current_QTY
        '
        Me.Current_QTY.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Current_QTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Current_QTY.Enabled = False
        Me.Current_QTY.Font = New System.Drawing.Font("Stencil", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Current_QTY.ForeColor = System.Drawing.Color.Black
        Me.Current_QTY.Location = New System.Drawing.Point(167, 151)
        Me.Current_QTY.Name = "Current_QTY"
        Me.Current_QTY.Size = New System.Drawing.Size(137, 32)
        Me.Current_QTY.TabIndex = 614
        Me.Current_QTY.Text = "00"
        Me.Current_QTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(460, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(84, 20)
        Me.Label2.TabIndex = 612
        Me.Label2.Text = "رقم الزبون :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Barcode_SH_txt
        '
        Me.Barcode_SH_txt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barcode_SH_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_SH_txt.Location = New System.Drawing.Point(302, 50)
        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
        Me.Barcode_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Barcode_SH_txt.Size = New System.Drawing.Size(156, 29)
        Me.Barcode_SH_txt.TabIndex = 610
        Me.Barcode_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BillPictureBox
        '
        Me.BillPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BillPictureBox.Location = New System.Drawing.Point(4, 134)
        Me.BillPictureBox.Name = "BillPictureBox"
        Me.BillPictureBox.Size = New System.Drawing.Size(148, 143)
        Me.BillPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BillPictureBox.TabIndex = 616
        Me.BillPictureBox.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(374, 122)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label7.Size = New System.Drawing.Size(85, 20)
        Me.Label7.TabIndex = 618
        Me.Label7.Text = "نوع العميل :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AG_Type_cm
        '
        Me.AG_Type_cm.BackColor = System.Drawing.SystemColors.Info
        Me.AG_Type_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AG_Type_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AG_Type_cm.Enabled = False
        Me.AG_Type_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AG_Type_cm.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Type_cm.FormattingEnabled = True
        Me.AG_Type_cm.Location = New System.Drawing.Point(167, 118)
        Me.AG_Type_cm.Name = "AG_Type_cm"
        Me.AG_Type_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Type_cm.Size = New System.Drawing.Size(201, 30)
        Me.AG_Type_cm.TabIndex = 617
        '
        'Note_Lb
        '
        Me.Note_Lb.AutoSize = True
        Me.Note_Lb.BackColor = System.Drawing.Color.Transparent
        Me.Note_Lb.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Note_Lb.ForeColor = System.Drawing.Color.MediumBlue
        Me.Note_Lb.Location = New System.Drawing.Point(73, 318)
        Me.Note_Lb.Name = "Note_Lb"
        Me.Note_Lb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Note_Lb.Size = New System.Drawing.Size(62, 20)
        Me.Note_Lb.TabIndex = 619
        Me.Note_Lb.Text = "ملاحظات"
        Me.Note_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Note_Lb.Visible = False
        '
        'Back_Btn
        '
        Me.Back_Btn.BackColor = System.Drawing.Color.IndianRed
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Back_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Back_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Back_Btn.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.Back_Btn.Location = New System.Drawing.Point(406, 480)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Back_Btn.Size = New System.Drawing.Size(150, 45)
        Me.Back_Btn.TabIndex = 637
        Me.Back_Btn.TabStop = False
        Me.Back_Btn.Text = "خروج Esc"
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'is_By_PhoneCB
        '
        Me.is_By_PhoneCB.AutoSize = True
        Me.is_By_PhoneCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.is_By_PhoneCB.Location = New System.Drawing.Point(152, 48)
        Me.is_By_PhoneCB.Name = "is_By_PhoneCB"
        Me.is_By_PhoneCB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.is_By_PhoneCB.Size = New System.Drawing.Size(118, 21)
        Me.is_By_PhoneCB.TabIndex = 640
        Me.is_By_PhoneCB.Text = "بحث برقم الهاتف"
        Me.is_By_PhoneCB.UseVisualStyleBackColor = True
        '
        'Agents_Panel
        '
        Me.Agents_Panel.Location = New System.Drawing.Point(557, 42)
        Me.Agents_Panel.Name = "Agents_Panel"
        Me.Agents_Panel.Size = New System.Drawing.Size(335, 483)
        Me.Agents_Panel.TabIndex = 641
        '
        'AG_Cm
        '
        Me.AG_Cm.CancelSearchImage = CType(resources.GetObject("AG_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.AG_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Cm.Location = New System.Drawing.Point(4, 81)
        Me.AG_Cm.Name = "AG_Cm"
        Me.AG_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Cm.Size = New System.Drawing.Size(454, 35)
        Me.AG_Cm.SQL_Column = "AG_NAME"
        Me.AG_Cm.SQL_ID = "AG_ID"
        Me.AG_Cm.SQL_IsNumericSearchField = False
        Me.AG_Cm.SQL_ListSize = 200
        Me.AG_Cm.SQL_NumberOfRows = 200
        Me.AG_Cm.SQL_OrderByField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField_WHERE = ""
        Me.AG_Cm.SQL_Table = "AGENTS_MENU_V"
        Me.AG_Cm.TabIndex = 642
        Me.AG_Cm.TextMaxLength = 250
        Me.AG_Cm.Textt = ""
        '
        'AgentsMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(894, 526)
        Me.ControlBox = False
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.AG_Cm)
        Me.Controls.Add(Me.Agents_Panel)
        Me.Controls.Add(Me.is_By_PhoneCB)
        Me.Controls.Add(Me.Back_Btn)
        Me.Controls.Add(Me.Note_Lb)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Barcode_SH_txt)
        Me.Controls.Add(Me.AG_Account_btn)
        Me.Controls.Add(Me.Cash_AG_btn)
        Me.Controls.Add(Me.Select_AG_btn)
        Me.Controls.Add(Me.New_AG_Btn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.AG_Type_cm)
        Me.Controls.Add(Me.BillPictureBox)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Current_QTY)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "AgentsMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدراج عميل للفاتورة"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        CType(Me.BillPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents MaxFormButton As System.Windows.Forms.Button
    Friend WithEvents MinFormButton As System.Windows.Forms.Button

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents New_AG_Btn As System.Windows.Forms.Button
    Friend WithEvents Select_AG_btn As System.Windows.Forms.Button
    Friend WithEvents Cash_AG_btn As System.Windows.Forms.Button
    Friend WithEvents AG_Account_btn As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Current_QTY As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Barcode_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents BillPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents AG_Type_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Note_Lb As System.Windows.Forms.Label
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents is_By_PhoneCB As CheckBox
    Friend WithEvents Agents_Panel As Panel
    Friend WithEvents AG_Cm As FSearch_Filter
End Class