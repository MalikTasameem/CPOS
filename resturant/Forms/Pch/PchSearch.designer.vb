<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PchSearch
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PchSearch))
        Me.ClearSearchButton = New System.Windows.Forms.Button()
        Me.SearchName_txtb = New System.Windows.Forms.TextBox()
        Me.AGMetroGrid = New MetroFramework.Controls.MetroGrid()
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.isDeletedCheckBox = New System.Windows.Forms.CheckBox()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bill_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReferNum_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AG_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Agent_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Proj_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Receipt_Title_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cost_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isDeleted_CL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.isDepended_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Search_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.User_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.User_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.About_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Img_CL = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Pure_cl = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Discount_cl = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Invoice_Type_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cr_Equal_Value_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isPied_CL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ClearSearchButton
        '
        Me.ClearSearchButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClearSearchButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ClearSearchButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClearSearchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ClearSearchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.ClearSearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ClearSearchButton.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ClearSearchButton.ForeColor = System.Drawing.Color.DarkRed
        Me.ClearSearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ClearSearchButton.Location = New System.Drawing.Point(568, 7)
        Me.ClearSearchButton.Name = "ClearSearchButton"
        Me.ClearSearchButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ClearSearchButton.Size = New System.Drawing.Size(55, 30)
        Me.ClearSearchButton.TabIndex = 288
        Me.ClearSearchButton.Text = "C"
        Me.ClearSearchButton.UseVisualStyleBackColor = False
        '
        'SearchName_txtb
        '
        Me.SearchName_txtb.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SearchName_txtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SearchName_txtb.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchName_txtb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SearchName_txtb.Location = New System.Drawing.Point(625, 8)
        Me.SearchName_txtb.MaxLength = 200
        Me.SearchName_txtb.Name = "SearchName_txtb"
        Me.SearchName_txtb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SearchName_txtb.Size = New System.Drawing.Size(374, 29)
        Me.SearchName_txtb.TabIndex = 388
        Me.SearchName_txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'AGMetroGrid
        '
        Me.AGMetroGrid.AllowUserToAddRows = False
        Me.AGMetroGrid.AllowUserToDeleteRows = False
        Me.AGMetroGrid.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.AGMetroGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.AGMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.AGMetroGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AGMetroGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.AGMetroGrid.CausesValidation = False
        Me.AGMetroGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.AGMetroGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AGMetroGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.AGMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AGMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.Bill_ID_CL, Me.ReferNum_CL, Me.AG_ID_CL, Me.Date_CL, Me.Agent_name_CL, Me.Proj_NAME_CL, Me.Receipt_Title_CL, Me.Cost_CL, Me.Name_CL, Me.isDeleted_CL, Me.isDepended_CL, Me.Search_CL, Me.User_Name_CL, Me.User_ID_CL, Me.About_CL, Me.Img_CL, Me.Pure_cl, Me.Discount_cl, Me.Invoice_Type_CL, Me.Cr_Equal_Value_CL, Me.isPied_CL})
        Me.AGMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.AGMetroGrid.DefaultCellStyle = DataGridViewCellStyle4
        Me.AGMetroGrid.EnableHeadersVisualStyles = False
        Me.AGMetroGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.AGMetroGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AGMetroGrid.Location = New System.Drawing.Point(3, 40)
        Me.AGMetroGrid.MultiSelect = False
        Me.AGMetroGrid.Name = "AGMetroGrid"
        Me.AGMetroGrid.ReadOnly = True
        Me.AGMetroGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AGMetroGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.AGMetroGrid.RowHeadersVisible = False
        Me.AGMetroGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Empty
        Me.AGMetroGrid.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.AGMetroGrid.RowTemplate.Height = 35
        Me.AGMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGMetroGrid.Size = New System.Drawing.Size(996, 653)
        Me.AGMetroGrid.TabIndex = 291
        Me.AGMetroGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'MetroToolTip1
        '
        Me.MetroToolTip1.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip1.StyleManager = Nothing
        Me.MetroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(1020, 556)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(105, 33)
        Me.ExitFormButton.TabIndex = 665
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'isDeletedCheckBox
        '
        Me.isDeletedCheckBox.AutoSize = True
        Me.isDeletedCheckBox.Font = New System.Drawing.Font("Arial", 14.25!)
        Me.isDeletedCheckBox.Location = New System.Drawing.Point(434, 8)
        Me.isDeletedCheckBox.Name = "isDeletedCheckBox"
        Me.isDeletedCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.isDeletedCheckBox.Size = New System.Drawing.Size(100, 26)
        Me.isDeletedCheckBox.TabIndex = 666
        Me.isDeletedCheckBox.Text = "معاملة ملغية"
        Me.isDeletedCheckBox.UseVisualStyleBackColor = True
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "ت.م"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'Bill_ID_CL
        '
        Me.Bill_ID_CL.DataPropertyName = "Bill_ID"
        Me.Bill_ID_CL.FillWeight = 96.36938!
        Me.Bill_ID_CL.HeaderText = "الرقم"
        Me.Bill_ID_CL.Name = "Bill_ID_CL"
        Me.Bill_ID_CL.ReadOnly = True
        '
        'ReferNum_CL
        '
        Me.ReferNum_CL.DataPropertyName = "ReferNum"
        Me.ReferNum_CL.FillWeight = 104.3376!
        Me.ReferNum_CL.HeaderText = "الإشاري"
        Me.ReferNum_CL.Name = "ReferNum_CL"
        Me.ReferNum_CL.ReadOnly = True
        '
        'AG_ID_CL
        '
        Me.AG_ID_CL.DataPropertyName = "AG_ID"
        Me.AG_ID_CL.FillWeight = 20.0!
        Me.AG_ID_CL.HeaderText = "رقم العميل"
        Me.AG_ID_CL.Name = "AG_ID_CL"
        Me.AG_ID_CL.ReadOnly = True
        Me.AG_ID_CL.Visible = False
        '
        'Date_CL
        '
        Me.Date_CL.DataPropertyName = "Date"
        Me.Date_CL.FillWeight = 112.2184!
        Me.Date_CL.HeaderText = "التاريـــخ"
        Me.Date_CL.Name = "Date_CL"
        Me.Date_CL.ReadOnly = True
        '
        'Agent_name_CL
        '
        Me.Agent_name_CL.DataPropertyName = "Ag_name"
        Me.Agent_name_CL.FillWeight = 130.6651!
        Me.Agent_name_CL.HeaderText = "العميل"
        Me.Agent_name_CL.Name = "Agent_name_CL"
        Me.Agent_name_CL.ReadOnly = True
        '
        'Proj_NAME_CL
        '
        Me.Proj_NAME_CL.DataPropertyName = "Proj_NAME"
        Me.Proj_NAME_CL.HeaderText = "زبون 2"
        Me.Proj_NAME_CL.Name = "Proj_NAME_CL"
        Me.Proj_NAME_CL.ReadOnly = True
        Me.Proj_NAME_CL.Visible = False
        '
        'Receipt_Title_CL
        '
        Me.Receipt_Title_CL.DataPropertyName = "Receipt_Title"
        Me.Receipt_Title_CL.FillWeight = 104.3376!
        Me.Receipt_Title_CL.HeaderText = "العنــوان"
        Me.Receipt_Title_CL.Name = "Receipt_Title_CL"
        Me.Receipt_Title_CL.ReadOnly = True
        Me.Receipt_Title_CL.Visible = False
        '
        'Cost_CL
        '
        Me.Cost_CL.DataPropertyName = "Cost"
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Cost_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Cost_CL.FillWeight = 130.6651!
        Me.Cost_CL.HeaderText = "الإجمالي"
        Me.Cost_CL.Name = "Cost_CL"
        Me.Cost_CL.ReadOnly = True
        '
        'Name_CL
        '
        Me.Name_CL.DataPropertyName = "Name_"
        Me.Name_CL.FillWeight = 116.3436!
        Me.Name_CL.HeaderText = "الحالـة"
        Me.Name_CL.Name = "Name_CL"
        Me.Name_CL.ReadOnly = True
        '
        'isDeleted_CL
        '
        Me.isDeleted_CL.DataPropertyName = "isVoid"
        Me.isDeleted_CL.HeaderText = "فاتورة محذوفة"
        Me.isDeleted_CL.Name = "isDeleted_CL"
        Me.isDeleted_CL.ReadOnly = True
        Me.isDeleted_CL.Visible = False
        '
        'isDepended_CL
        '
        Me.isDepended_CL.DataPropertyName = "isDepended"
        Me.isDepended_CL.HeaderText = "رقم الحالة"
        Me.isDepended_CL.Name = "isDepended_CL"
        Me.isDepended_CL.ReadOnly = True
        Me.isDepended_CL.Visible = False
        '
        'Search_CL
        '
        Me.Search_CL.DataPropertyName = "Search"
        Me.Search_CL.HeaderText = "البحث"
        Me.Search_CL.Name = "Search_CL"
        Me.Search_CL.ReadOnly = True
        Me.Search_CL.Visible = False
        '
        'User_Name_CL
        '
        Me.User_Name_CL.DataPropertyName = "UserName"
        Me.User_Name_CL.FillWeight = 104.3376!
        Me.User_Name_CL.HeaderText = "المدخل"
        Me.User_Name_CL.Name = "User_Name_CL"
        Me.User_Name_CL.ReadOnly = True
        '
        'User_ID_CL
        '
        Me.User_ID_CL.DataPropertyName = "User_ID"
        Me.User_ID_CL.HeaderText = "رقم المدخل"
        Me.User_ID_CL.Name = "User_ID_CL"
        Me.User_ID_CL.ReadOnly = True
        Me.User_ID_CL.Visible = False
        '
        'About_CL
        '
        Me.About_CL.DataPropertyName = "About"
        Me.About_CL.HeaderText = "ملاحظات"
        Me.About_CL.Name = "About_CL"
        Me.About_CL.ReadOnly = True
        Me.About_CL.Visible = False
        '
        'Img_CL
        '
        Me.Img_CL.DataPropertyName = "Img"
        Me.Img_CL.HeaderText = "Img"
        Me.Img_CL.Name = "Img_CL"
        Me.Img_CL.ReadOnly = True
        Me.Img_CL.Visible = False
        '
        'Pure_cl
        '
        Me.Pure_cl.DataPropertyName = "Pure"
        Me.Pure_cl.HeaderText = "Pure"
        Me.Pure_cl.Name = "Pure_cl"
        Me.Pure_cl.ReadOnly = True
        Me.Pure_cl.Visible = False
        '
        'Discount_cl
        '
        Me.Discount_cl.DataPropertyName = "Discount"
        Me.Discount_cl.HeaderText = "Discount"
        Me.Discount_cl.Name = "Discount_cl"
        Me.Discount_cl.ReadOnly = True
        Me.Discount_cl.Visible = False
        '
        'Invoice_Type_CL
        '
        Me.Invoice_Type_CL.DataPropertyName = "Invoice_Type"
        Me.Invoice_Type_CL.HeaderText = "Invoice_Type"
        Me.Invoice_Type_CL.Name = "Invoice_Type_CL"
        Me.Invoice_Type_CL.ReadOnly = True
        Me.Invoice_Type_CL.Visible = False
        '
        'Cr_Equal_Value_CL
        '
        Me.Cr_Equal_Value_CL.DataPropertyName = "Cr_Equal_Value"
        Me.Cr_Equal_Value_CL.HeaderText = "Cr_Equal_Value"
        Me.Cr_Equal_Value_CL.Name = "Cr_Equal_Value_CL"
        Me.Cr_Equal_Value_CL.ReadOnly = True
        Me.Cr_Equal_Value_CL.Visible = False
        '
        'isPied_CL
        '
        Me.isPied_CL.DataPropertyName = "isPied"
        Me.isPied_CL.HeaderText = "isPied"
        Me.isPied_CL.Name = "isPied_CL"
        Me.isPied_CL.ReadOnly = True
        Me.isPied_CL.Visible = False
        '
        'PchSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.isDeletedCheckBox)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.AGMetroGrid)
        Me.Controls.Add(Me.ClearSearchButton)
        Me.Controls.Add(Me.SearchName_txtb)
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MinimizeBox = False
        Me.Name = "PchSearch"
        Me.Padding = New System.Windows.Forms.Padding(30, 84, 30, 27)
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "قائمة الفواتيــر"
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AGMetroGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents SearchName_txtb As System.Windows.Forms.TextBox
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
    Friend WithEvents ClearSearchButton As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents isDeletedCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bill_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReferNum_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AG_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Agent_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Proj_NAME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Receipt_Title_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cost_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents isDeleted_CL As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents isDepended_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Search_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents User_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents User_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents About_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Img_CL As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Pure_cl As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Discount_cl As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Invoice_Type_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cr_Equal_Value_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents isPied_CL As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
