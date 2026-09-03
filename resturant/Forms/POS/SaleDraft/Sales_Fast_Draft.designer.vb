<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Sales_Fast_Draft
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Sales_Fast_Draft))
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.RemoveCatButton = New System.Windows.Forms.Button()
        Me.CALC_Btn = New System.Windows.Forms.Button()
        Me.OpenCahDR_Btn = New System.Windows.Forms.Button()
        Me.Show_Cash_btn = New System.Windows.Forms.Button()
        Me.PreviousBillsButton = New System.Windows.Forms.Button()
        Me.ChangeCustomerButton = New System.Windows.Forms.Button()
        Me.IM_Search_btn = New System.Windows.Forms.Button()
        Me.ChangePriceButton = New System.Windows.Forms.Button()
        Me.ClearDraftItemsButton = New System.Windows.Forms.Button()
        Me.IM_Count_LB = New System.Windows.Forms.Label()
        Me.User_Name_lb = New System.Windows.Forms.Label()
        Me.DiscountPanel = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Discount_txt = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Total_TextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Calc_Dicount_Btn = New System.Windows.Forms.Button()
        Me.Pure_txt = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.IM_Unit_cm = New System.Windows.Forms.ComboBox()
        Me.Bill_ID_Txt = New System.Windows.Forms.TextBox()
        Me.DateTimeEx = New System.Windows.Forms.DateTimePicker()
        Me.IM_Qty_LB = New System.Windows.Forms.Label()
        Me.IMPanel = New System.Windows.Forms.Panel()
        Me.AG_SH_txt = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.AG_Panel = New System.Windows.Forms.Panel()
        Me.dgvSales = New System.Windows.Forms.DataGridView()
        Me.Bill_IMID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.is_Check_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Serial_Code_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMNUM_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Item_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Valid_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMUnit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_Discount_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Notes_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_NOTE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Valid_cm = New System.Windows.Forms.ComboBox()
        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Draft_Btn = New System.Windows.Forms.Button()
        Me.IMIncreaseButton = New System.Windows.Forms.Button()
        Me.IMDicreaseButton = New System.Windows.Forms.Button()
        Me.Units_btn = New System.Windows.Forms.Button()
        Me.Refresh_IM_Btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Print_btn = New System.Windows.Forms.Button()
        Me.New_butt = New System.Windows.Forms.Button()
        Me.Save_butt = New System.Windows.Forms.Button()
        Me.note_Btn = New System.Windows.Forms.Button()
        Me.QTY_Btn = New System.Windows.Forms.Button()
        Me.ScreenStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ScreenStatusTypeLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ScreenStatusMessageLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ScreenStatusProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.ScreenStatusTimeLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UcGridColumnsSelector1 = New resturant.UcGridColumnsSelector()
        Me.DiscountPanel.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.AG_Panel.SuspendLayout()
        CType(Me.dgvSales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.ScreenStatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MetroToolTip1
        '
        Me.MetroToolTip1.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip1.StyleManager = Nothing
        Me.MetroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'RemoveCatButton
        '
        Me.RemoveCatButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(9, Byte), Integer))
        Me.RemoveCatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.RemoveCatButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RemoveCatButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.RemoveCatButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.RemoveCatButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RemoveCatButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RemoveCatButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.RemoveCatButton.Location = New System.Drawing.Point(113, 495)
        Me.RemoveCatButton.Name = "RemoveCatButton"
        Me.RemoveCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RemoveCatButton.Size = New System.Drawing.Size(110, 55)
        Me.RemoveCatButton.TabIndex = 395
        Me.RemoveCatButton.TabStop = False
        Me.RemoveCatButton.Text = "❌" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "F8"
        Me.MetroToolTip1.SetToolTip(Me.RemoveCatButton, "حذف الصنف الحدد")
        Me.RemoveCatButton.UseVisualStyleBackColor = False
        '
        'CALC_Btn
        '
        Me.CALC_Btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.CALC_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.CALC_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CALC_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CALC_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CALC_Btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.CALC_Btn.Location = New System.Drawing.Point(802, 497)
        Me.CALC_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CALC_Btn.Name = "CALC_Btn"
        Me.CALC_Btn.Size = New System.Drawing.Size(110, 55)
        Me.CALC_Btn.TabIndex = 708
        Me.CALC_Btn.Text = "الحاسبة 🔢"
        Me.MetroToolTip1.SetToolTip(Me.CALC_Btn, "فتح الألة الحاسبة")
        Me.CALC_Btn.UseVisualStyleBackColor = False
        '
        'OpenCahDR_Btn
        '
        Me.OpenCahDR_Btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.OpenCahDR_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.OpenCahDR_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.OpenCahDR_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OpenCahDR_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.OpenCahDR_Btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.OpenCahDR_Btn.Location = New System.Drawing.Point(802, 441)
        Me.OpenCahDR_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.OpenCahDR_Btn.Name = "OpenCahDR_Btn"
        Me.OpenCahDR_Btn.Size = New System.Drawing.Size(110, 55)
        Me.OpenCahDR_Btn.TabIndex = 654
        Me.OpenCahDR_Btn.Text = "فتح الدرج 🗄️"
        Me.MetroToolTip1.SetToolTip(Me.OpenCahDR_Btn, "فتح صندوق النقود")
        Me.OpenCahDR_Btn.UseVisualStyleBackColor = False
        '
        'Show_Cash_btn
        '
        Me.Show_Cash_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Show_Cash_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Show_Cash_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_Cash_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Show_Cash_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Show_Cash_btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Show_Cash_btn.Location = New System.Drawing.Point(691, 497)
        Me.Show_Cash_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Show_Cash_btn.Name = "Show_Cash_btn"
        Me.Show_Cash_btn.Size = New System.Drawing.Size(110, 55)
        Me.Show_Cash_btn.TabIndex = 655
        Me.Show_Cash_btn.Text = "عرض المقبوض 💵"
        Me.MetroToolTip1.SetToolTip(Me.Show_Cash_btn, "عرض المقبوض")
        Me.Show_Cash_btn.UseVisualStyleBackColor = False
        '
        'PreviousBillsButton
        '
        Me.PreviousBillsButton.BackColor = System.Drawing.Color.White
        Me.PreviousBillsButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PreviousBillsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.PreviousBillsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.PreviousBillsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PreviousBillsButton.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PreviousBillsButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PreviousBillsButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.PreviousBillsButton.Location = New System.Drawing.Point(785, 4)
        Me.PreviousBillsButton.Name = "PreviousBillsButton"
        Me.PreviousBillsButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PreviousBillsButton.Size = New System.Drawing.Size(161, 40)
        Me.PreviousBillsButton.TabIndex = 718
        Me.PreviousBillsButton.TabStop = False
        Me.PreviousBillsButton.Text = "الفواتير السابقة"
        Me.MetroToolTip1.SetToolTip(Me.PreviousBillsButton, "مراجعة الفواتير السابقة")
        Me.PreviousBillsButton.UseVisualStyleBackColor = False
        '
        'ChangeCustomerButton
        '
        Me.ChangeCustomerButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.ChangeCustomerButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChangeCustomerButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.ChangeCustomerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ChangeCustomerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.ChangeCustomerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ChangeCustomerButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ChangeCustomerButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ChangeCustomerButton.Location = New System.Drawing.Point(913, 441)
        Me.ChangeCustomerButton.Name = "ChangeCustomerButton"
        Me.ChangeCustomerButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChangeCustomerButton.Size = New System.Drawing.Size(110, 55)
        Me.ChangeCustomerButton.TabIndex = 719
        Me.ChangeCustomerButton.TabStop = False
        Me.ChangeCustomerButton.Text = "تغيير الزبون 👤"
        Me.MetroToolTip1.SetToolTip(Me.ChangeCustomerButton, "تغيير العميل")
        Me.ChangeCustomerButton.UseVisualStyleBackColor = False
        '
        'IM_Search_btn
        '
        Me.IM_Search_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.IM_Search_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IM_Search_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Search_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.IM_Search_btn.FlatAppearance.BorderSize = 2
        Me.IM_Search_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.IM_Search_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(6, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.IM_Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Search_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IM_Search_btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.IM_Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IM_Search_btn.Location = New System.Drawing.Point(691, 441)
        Me.IM_Search_btn.Name = "IM_Search_btn"
        Me.IM_Search_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_Search_btn.Size = New System.Drawing.Size(110, 55)
        Me.IM_Search_btn.TabIndex = 707
        Me.IM_Search_btn.TabStop = False
        Me.IM_Search_btn.Text = "🔎" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "بحث عن صنف"
        Me.MetroToolTip1.SetToolTip(Me.IM_Search_btn, "فتح شاشة البحث عن صنف")
        Me.IM_Search_btn.UseVisualStyleBackColor = False
        '
        'ChangePriceButton
        '
        Me.ChangePriceButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.ChangePriceButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChangePriceButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.ChangePriceButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.ChangePriceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ChangePriceButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ChangePriceButton.ForeColor = System.Drawing.Color.White
        Me.ChangePriceButton.Location = New System.Drawing.Point(802, 553)
        Me.ChangePriceButton.Name = "ChangePriceButton"
        Me.ChangePriceButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChangePriceButton.Size = New System.Drawing.Size(110, 55)
        Me.ChangePriceButton.TabIndex = 918
        Me.ChangePriceButton.TabStop = False
        Me.ChangePriceButton.Text = "💵" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "تعديل السعر"
        Me.MetroToolTip1.SetToolTip(Me.ChangePriceButton, "تعديل سعر بيع الصنف المحدد")
        Me.ChangePriceButton.UseVisualStyleBackColor = False
        '
        'ClearDraftItemsButton
        '
        Me.ClearDraftItemsButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.ClearDraftItemsButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ClearDraftItemsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(29, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.ClearDraftItemsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.ClearDraftItemsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ClearDraftItemsButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ClearDraftItemsButton.ForeColor = System.Drawing.Color.White
        Me.ClearDraftItemsButton.Location = New System.Drawing.Point(691, 553)
        Me.ClearDraftItemsButton.Name = "ClearDraftItemsButton"
        Me.ClearDraftItemsButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ClearDraftItemsButton.Size = New System.Drawing.Size(110, 55)
        Me.ClearDraftItemsButton.TabIndex = 919
        Me.ClearDraftItemsButton.TabStop = False
        Me.ClearDraftItemsButton.Text = "🧹" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "مسح الأصناف"
        Me.MetroToolTip1.SetToolTip(Me.ClearDraftItemsButton, "مسح كل أصناف المسودة الحالية")
        Me.ClearDraftItemsButton.UseVisualStyleBackColor = False
        '
        'IM_Count_LB
        '
        Me.IM_Count_LB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.IM_Count_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Count_LB.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.IM_Count_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.IM_Count_LB.Location = New System.Drawing.Point(259, 665)
        Me.IM_Count_LB.Name = "IM_Count_LB"
        Me.IM_Count_LB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IM_Count_LB.Size = New System.Drawing.Size(99, 30)
        Me.IM_Count_LB.TabIndex = 614
        Me.IM_Count_LB.Text = "المواد : 0"
        Me.IM_Count_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'User_Name_lb
        '
        Me.User_Name_lb.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.User_Name_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.User_Name_lb.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.User_Name_lb.ForeColor = System.Drawing.Color.Blue
        Me.User_Name_lb.Location = New System.Drawing.Point(459, 665)
        Me.User_Name_lb.Name = "User_Name_lb"
        Me.User_Name_lb.Size = New System.Drawing.Size(466, 30)
        Me.User_Name_lb.TabIndex = 630
        Me.User_Name_lb.Text = "المستخدم"
        Me.User_Name_lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DiscountPanel
        '
        Me.DiscountPanel.Controls.Add(Me.Panel5)
        Me.DiscountPanel.Controls.Add(Me.Label13)
        Me.DiscountPanel.Controls.Add(Me.Total_TextBox)
        Me.DiscountPanel.Controls.Add(Me.Label6)
        Me.DiscountPanel.Location = New System.Drawing.Point(4, 578)
        Me.DiscountPanel.Name = "DiscountPanel"
        Me.DiscountPanel.Size = New System.Drawing.Size(250, 64)
        Me.DiscountPanel.TabIndex = 634
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Discount_txt)
        Me.Panel5.Location = New System.Drawing.Point(1, 31)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(148, 30)
        Me.Panel5.TabIndex = 719
        '
        'Discount_txt
        '
        Me.Discount_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Discount_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Discount_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Discount_txt.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Discount_txt.ForeColor = System.Drawing.Color.Black
        Me.Discount_txt.Location = New System.Drawing.Point(1, 2)
        Me.Discount_txt.MaxLength = 200
        Me.Discount_txt.Name = "Discount_txt"
        Me.Discount_txt.ReadOnly = True
        Me.Discount_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Discount_txt.Size = New System.Drawing.Size(145, 27)
        Me.Discount_txt.TabIndex = 617
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(150, 37)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label13.Size = New System.Drawing.Size(63, 17)
        Me.Label13.TabIndex = 618
        Me.Label13.Text = " التخفيــض"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Total_TextBox
        '
        Me.Total_TextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Total_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_TextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Total_TextBox.ForeColor = System.Drawing.Color.Black
        Me.Total_TextBox.Location = New System.Drawing.Point(1, 4)
        Me.Total_TextBox.MaxLength = 200
        Me.Total_TextBox.Name = "Total_TextBox"
        Me.Total_TextBox.ReadOnly = True
        Me.Total_TextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Total_TextBox.Size = New System.Drawing.Size(146, 27)
        Me.Total_TextBox.TabIndex = 289
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(151, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label6.Size = New System.Drawing.Size(58, 17)
        Me.Label6.TabIndex = 387
        Me.Label6.Text = "الإجمالـــي"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Calc_Dicount_Btn
        '
        Me.Calc_Dicount_Btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Calc_Dicount_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Calc_Dicount_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Calc_Dicount_Btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Calc_Dicount_Btn.Location = New System.Drawing.Point(913, 553)
        Me.Calc_Dicount_Btn.Name = "Calc_Dicount_Btn"
        Me.Calc_Dicount_Btn.Size = New System.Drawing.Size(110, 55)
        Me.Calc_Dicount_Btn.TabIndex = 1
        Me.Calc_Dicount_Btn.Text = "تخفيض %"
        Me.Calc_Dicount_Btn.UseVisualStyleBackColor = False
        '
        'Pure_txt
        '
        Me.Pure_txt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Pure_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pure_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pure_txt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Pure_txt.Location = New System.Drawing.Point(2, 5)
        Me.Pure_txt.MaxLength = 200
        Me.Pure_txt.Name = "Pure_txt"
        Me.Pure_txt.ReadOnly = True
        Me.Pure_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Pure_txt.Size = New System.Drawing.Size(146, 39)
        Me.Pure_txt.TabIndex = 619
        Me.Pure_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.Label17.Location = New System.Drawing.Point(151, 12)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label17.Size = New System.Drawing.Size(73, 23)
        Me.Label17.TabIndex = 620
        Me.Label17.Text = "الصـافــي"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'IM_Unit_cm
        '
        Me.IM_Unit_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Unit_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Unit_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Unit_cm.FormattingEnabled = True
        Me.IM_Unit_cm.Location = New System.Drawing.Point(223, 77)
        Me.IM_Unit_cm.Name = "IM_Unit_cm"
        Me.IM_Unit_cm.Size = New System.Drawing.Size(60, 23)
        Me.IM_Unit_cm.TabIndex = 615
        Me.IM_Unit_cm.Visible = False
        '
        'Bill_ID_Txt
        '
        Me.Bill_ID_Txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Bill_ID_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Bill_ID_Txt.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bill_ID_Txt.ForeColor = System.Drawing.Color.Black
        Me.Bill_ID_Txt.Location = New System.Drawing.Point(877, 47)
        Me.Bill_ID_Txt.MaxLength = 250
        Me.Bill_ID_Txt.Name = "Bill_ID_Txt"
        Me.Bill_ID_Txt.ReadOnly = True
        Me.Bill_ID_Txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Bill_ID_Txt.Size = New System.Drawing.Size(146, 30)
        Me.Bill_ID_Txt.TabIndex = 625
        Me.Bill_ID_Txt.Text = "---"
        Me.Bill_ID_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateTimeEx
        '
        Me.DateTimeEx.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimeEx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DateTimeEx.CustomFormat = "dd/MM/yyyy"
        Me.DateTimeEx.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.DateTimeEx.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimeEx.Location = New System.Drawing.Point(2, 32)
        Me.DateTimeEx.Name = "DateTimeEx"
        Me.DateTimeEx.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTimeEx.RightToLeftLayout = True
        Me.DateTimeEx.Size = New System.Drawing.Size(211, 27)
        Me.DateTimeEx.TabIndex = 383
        '
        'IM_Qty_LB
        '
        Me.IM_Qty_LB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.IM_Qty_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Qty_LB.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.IM_Qty_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.IM_Qty_LB.Location = New System.Drawing.Point(359, 665)
        Me.IM_Qty_LB.Name = "IM_Qty_LB"
        Me.IM_Qty_LB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IM_Qty_LB.Size = New System.Drawing.Size(99, 30)
        Me.IM_Qty_LB.TabIndex = 643
        Me.IM_Qty_LB.Text = "الكميات : 0"
        Me.IM_Qty_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IMPanel
        '
        Me.IMPanel.AutoScroll = True
        Me.IMPanel.BackColor = System.Drawing.Color.Transparent
        Me.IMPanel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMPanel.Location = New System.Drawing.Point(557, 106)
        Me.IMPanel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IMPanel.Name = "IMPanel"
        Me.IMPanel.Size = New System.Drawing.Size(466, 334)
        Me.IMPanel.TabIndex = 657
        '
        'AG_SH_txt
        '
        Me.AG_SH_txt.BackColor = System.Drawing.SystemColors.Info
        Me.AG_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AG_SH_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.AG_SH_txt.Location = New System.Drawing.Point(2, 3)
        Me.AG_SH_txt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.AG_SH_txt.Name = "AG_SH_txt"
        Me.AG_SH_txt.ReadOnly = True
        Me.AG_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AG_SH_txt.Size = New System.Drawing.Size(211, 27)
        Me.AG_SH_txt.TabIndex = 660
        Me.AG_SH_txt.Text = "نقدي"
        Me.AG_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(3, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label16.Size = New System.Drawing.Size(217, 33)
        Me.Label16.TabIndex = 661
        Me.Label16.Text = "فاتورة مبيعات جديدة"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AG_Panel
        '
        Me.AG_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AG_Panel.Controls.Add(Me.DateTimeEx)
        Me.AG_Panel.Controls.Add(Me.AG_SH_txt)
        Me.AG_Panel.Location = New System.Drawing.Point(3, 38)
        Me.AG_Panel.Name = "AG_Panel"
        Me.AG_Panel.Size = New System.Drawing.Size(217, 65)
        Me.AG_Panel.TabIndex = 689
        '
        'dgvSales
        '
        Me.dgvSales.AllowUserToAddRows = False
        Me.dgvSales.AllowUserToDeleteRows = False
        Me.dgvSales.AllowUserToResizeRows = False
        Me.dgvSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSales.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Bill_IMID_CL, Me.is_Check_CL, Me.Barcode_CL, Me.Serial_Code_CL, Me.U_ID, Me.Date_, Me.ST_Name_CL, Me.IMNUM_CL, Me.Item_Name, Me.D_Valid_CL, Me.IMUnit_CL, Me.QTY_CL, Me.Price_CL, Me.IM_Discount_CL, Me.Total_CL, Me.Notes_CL, Me.T_ID_CL, Me.ST_ID_CL, Me.IM_NOTE_CL})
        Me.dgvSales.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dgvSales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSales.Location = New System.Drawing.Point(0, 0)
        Me.dgvSales.MultiSelect = False
        Me.dgvSales.Name = "dgvSales"
        Me.dgvSales.ReadOnly = True
        Me.dgvSales.RowHeadersVisible = False
        Me.dgvSales.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.dgvSales.RowTemplate.Height = 35
        Me.dgvSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSales.Size = New System.Drawing.Size(553, 388)
        Me.dgvSales.TabIndex = 701
        '
        'Bill_IMID_CL
        '
        Me.Bill_IMID_CL.DataPropertyName = "IM_ID"
        Me.Bill_IMID_CL.HeaderText = "IM_ID"
        Me.Bill_IMID_CL.Name = "Bill_IMID_CL"
        Me.Bill_IMID_CL.ReadOnly = True
        Me.Bill_IMID_CL.Visible = False
        '
        'is_Check_CL
        '
        Me.is_Check_CL.DataPropertyName = "is_Check"
        Me.is_Check_CL.HeaderText = "is_Check"
        Me.is_Check_CL.Name = "is_Check_CL"
        Me.is_Check_CL.ReadOnly = True
        Me.is_Check_CL.Visible = False
        '
        'Barcode_CL
        '
        Me.Barcode_CL.DataPropertyName = "Barcode"
        Me.Barcode_CL.FillWeight = 91.83587!
        Me.Barcode_CL.HeaderText = "باركود"
        Me.Barcode_CL.Name = "Barcode_CL"
        Me.Barcode_CL.ReadOnly = True
        Me.Barcode_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Serial_Code_CL
        '
        Me.Serial_Code_CL.DataPropertyName = "Serial_Code"
        Me.Serial_Code_CL.FillWeight = 91.83587!
        Me.Serial_Code_CL.HeaderText = "التسلسل"
        Me.Serial_Code_CL.Name = "Serial_Code_CL"
        Me.Serial_Code_CL.ReadOnly = True
        Me.Serial_Code_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'U_ID
        '
        Me.U_ID.DataPropertyName = "U_ID"
        Me.U_ID.HeaderText = "U_ID"
        Me.U_ID.Name = "U_ID"
        Me.U_ID.ReadOnly = True
        Me.U_ID.Visible = False
        '
        'Date_
        '
        Me.Date_.DataPropertyName = "Date"
        Me.Date_.FillWeight = 91.83587!
        Me.Date_.HeaderText = "تاريخ"
        Me.Date_.Name = "Date_"
        Me.Date_.ReadOnly = True
        '
        'ST_Name_CL
        '
        Me.ST_Name_CL.DataPropertyName = "St_Name"
        Me.ST_Name_CL.FillWeight = 91.83587!
        Me.ST_Name_CL.HeaderText = "مخزن"
        Me.ST_Name_CL.Name = "ST_Name_CL"
        Me.ST_Name_CL.ReadOnly = True
        '
        'IMNUM_CL
        '
        Me.IMNUM_CL.DataPropertyName = "IM_Num"
        Me.IMNUM_CL.FillWeight = 91.83587!
        Me.IMNUM_CL.HeaderText = "رقم"
        Me.IMNUM_CL.Name = "IMNUM_CL"
        Me.IMNUM_CL.ReadOnly = True
        '
        'Item_Name
        '
        Me.Item_Name.DataPropertyName = "ItemName"
        Me.Item_Name.FillWeight = 91.83587!
        Me.Item_Name.HeaderText = "الصنف"
        Me.Item_Name.Name = "Item_Name"
        Me.Item_Name.ReadOnly = True
        '
        'D_Valid_CL
        '
        Me.D_Valid_CL.DataPropertyName = "D_Vaild"
        Me.D_Valid_CL.FillWeight = 91.83587!
        Me.D_Valid_CL.HeaderText = "صلاحية"
        Me.D_Valid_CL.Name = "D_Valid_CL"
        Me.D_Valid_CL.ReadOnly = True
        '
        'IMUnit_CL
        '
        Me.IMUnit_CL.DataPropertyName = "UnitName"
        Me.IMUnit_CL.FillWeight = 91.83587!
        Me.IMUnit_CL.HeaderText = "الوحدة"
        Me.IMUnit_CL.Name = "IMUnit_CL"
        Me.IMUnit_CL.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.Format = "N3"
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.QTY_CL.FillWeight = 91.83587!
        Me.QTY_CL.HeaderText = "كمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'Price_CL
        '
        Me.Price_CL.DataPropertyName = "Price"
        DataGridViewCellStyle3.Format = "N3"
        Me.Price_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Price_CL.FillWeight = 91.83587!
        Me.Price_CL.HeaderText = "السعر"
        Me.Price_CL.Name = "Price_CL"
        Me.Price_CL.ReadOnly = True
        '
        'IM_Discount_CL
        '
        Me.IM_Discount_CL.DataPropertyName = "IM_Discount"
        Me.IM_Discount_CL.HeaderText = "خصم"
        Me.IM_Discount_CL.Name = "IM_Discount_CL"
        Me.IM_Discount_CL.ReadOnly = True
        '
        'Total_CL
        '
        Me.Total_CL.DataPropertyName = "T_Price"
        DataGridViewCellStyle4.Format = "N3"
        Me.Total_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.Total_CL.FillWeight = 91.83587!
        Me.Total_CL.HeaderText = "إجمالي"
        Me.Total_CL.Name = "Total_CL"
        Me.Total_CL.ReadOnly = True
        '
        'Notes_CL
        '
        Me.Notes_CL.DataPropertyName = "Notes"
        Me.Notes_CL.FillWeight = 91.83587!
        Me.Notes_CL.HeaderText = "ملاحظة"
        Me.Notes_CL.Name = "Notes_CL"
        Me.Notes_CL.ReadOnly = True
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'ST_ID_CL
        '
        Me.ST_ID_CL.DataPropertyName = "ST_ID"
        Me.ST_ID_CL.HeaderText = "ST_ID"
        Me.ST_ID_CL.Name = "ST_ID_CL"
        Me.ST_ID_CL.ReadOnly = True
        Me.ST_ID_CL.Visible = False
        '
        'IM_NOTE_CL
        '
        Me.IM_NOTE_CL.DataPropertyName = "IM_NOTE"
        Me.IM_NOTE_CL.HeaderText = "IM_NOTE"
        Me.IM_NOTE_CL.Name = "IM_NOTE_CL"
        Me.IM_NOTE_CL.ReadOnly = True
        Me.IM_NOTE_CL.Visible = False
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(256, 553)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(60, 21)
        Me.Label27.TabIndex = 705
        Me.Label27.Text = "ملاحظة :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNotes
        '
        Me.txtNotes.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotes.Enabled = False
        Me.txtNotes.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtNotes.ForeColor = System.Drawing.Color.Black
        Me.txtNotes.Location = New System.Drawing.Point(4, 551)
        Me.txtNotes.MaxLength = 250
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtNotes.Size = New System.Drawing.Size(250, 25)
        Me.txtNotes.TabIndex = 704
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Pure_txt)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Location = New System.Drawing.Point(3, 644)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(220, 48)
        Me.Panel3.TabIndex = 710
        '
        'Valid_cm
        '
        Me.Valid_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Valid_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Valid_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Valid_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Valid_cm.FormattingEnabled = True
        Me.Valid_cm.Location = New System.Drawing.Point(285, 78)
        Me.Valid_cm.Name = "Valid_cm"
        Me.Valid_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Valid_cm.Size = New System.Drawing.Size(55, 23)
        Me.Valid_cm.TabIndex = 711
        Me.Valid_cm.Visible = False
        '
        'Barcode_SH_txt
        '
        Me.Barcode_SH_txt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Barcode_SH_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_SH_txt.Location = New System.Drawing.Point(223, 78)
        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
        Me.Barcode_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Barcode_SH_txt.Size = New System.Drawing.Size(800, 27)
        Me.Barcode_SH_txt.TabIndex = 602
        Me.Barcode_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.dgvSales)
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Panel4.Location = New System.Drawing.Point(3, 106)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(553, 388)
        Me.Panel4.TabIndex = 714
        '
        'Draft_Btn
        '
        Me.Draft_Btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Draft_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Draft_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Draft_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Draft_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Draft_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Draft_Btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Draft_Btn.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Draft_Btn.Location = New System.Drawing.Point(580, 441)
        Me.Draft_Btn.Name = "Draft_Btn"
        Me.Draft_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Draft_Btn.Size = New System.Drawing.Size(110, 55)
        Me.Draft_Btn.TabIndex = 715
        Me.Draft_Btn.TabStop = False
        Me.Draft_Btn.Text = "المسودة 📋"
        Me.Draft_Btn.UseVisualStyleBackColor = False
        '
        'IMIncreaseButton
        '
        Me.IMIncreaseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(9, Byte), Integer))
        Me.IMIncreaseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IMIncreaseButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMIncreaseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMIncreaseButton.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMIncreaseButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.IMIncreaseButton.Location = New System.Drawing.Point(2, 495)
        Me.IMIncreaseButton.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.IMIncreaseButton.Name = "IMIncreaseButton"
        Me.IMIncreaseButton.Size = New System.Drawing.Size(110, 55)
        Me.IMIncreaseButton.TabIndex = 716
        Me.IMIncreaseButton.Text = "➕"
        Me.IMIncreaseButton.UseVisualStyleBackColor = False
        '
        'IMDicreaseButton
        '
        Me.IMDicreaseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(9, Byte), Integer))
        Me.IMDicreaseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IMDicreaseButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMDicreaseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMDicreaseButton.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMDicreaseButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.IMDicreaseButton.Location = New System.Drawing.Point(224, 495)
        Me.IMDicreaseButton.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.IMDicreaseButton.Name = "IMDicreaseButton"
        Me.IMDicreaseButton.Size = New System.Drawing.Size(110, 55)
        Me.IMDicreaseButton.TabIndex = 717
        Me.IMDicreaseButton.Text = "➖"
        Me.IMDicreaseButton.UseVisualStyleBackColor = False
        '
        'Units_btn
        '
        Me.Units_btn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Units_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(9, Byte), Integer))
        Me.Units_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Units_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Units_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Units_btn.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Units_btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Units_btn.Location = New System.Drawing.Point(335, 495)
        Me.Units_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Units_btn.Name = "Units_btn"
        Me.Units_btn.Size = New System.Drawing.Size(110, 55)
        Me.Units_btn.TabIndex = 718
        Me.Units_btn.Text = " العبوة 📦"
        Me.Units_btn.UseVisualStyleBackColor = False
        '
        'Refresh_IM_Btn
        '
        Me.Refresh_IM_Btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Refresh_IM_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Refresh_IM_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Refresh_IM_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Refresh_IM_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Refresh_IM_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Refresh_IM_Btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Refresh_IM_Btn.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Refresh_IM_Btn.Location = New System.Drawing.Point(913, 497)
        Me.Refresh_IM_Btn.Name = "Refresh_IM_Btn"
        Me.Refresh_IM_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Refresh_IM_Btn.Size = New System.Drawing.Size(110, 55)
        Me.Refresh_IM_Btn.TabIndex = 713
        Me.Refresh_IM_Btn.TabStop = False
        Me.Refresh_IM_Btn.Text = "تحديث الأصناف"
        Me.Refresh_IM_Btn.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(926, 665)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(96, 29)
        Me.ExitFormButton.TabIndex = 656
        Me.ExitFormButton.Tag = "DELETE"
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Print_btn
        '
        Me.Print_btn.BackColor = System.Drawing.Color.White
        Me.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_btn.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Print_btn.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Print_btn.Location = New System.Drawing.Point(226, 1)
        Me.Print_btn.Name = "Print_btn"
        Me.Print_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_btn.Size = New System.Drawing.Size(75, 40)
        Me.Print_btn.TabIndex = 307
        Me.Print_btn.TabStop = False
        Me.Print_btn.Tag = "PRINT"
        Me.Print_btn.Text = "طباعة F2"
        Me.Print_btn.UseVisualStyleBackColor = False
        Me.Print_btn.Visible = False
        '
        'New_butt
        '
        Me.New_butt.BackColor = System.Drawing.Color.White
        Me.New_butt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.New_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.New_butt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.New_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.New_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.New_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.New_butt.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.New_butt.ForeColor = System.Drawing.Color.Black
        Me.New_butt.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.New_butt.Location = New System.Drawing.Point(947, 4)
        Me.New_butt.Name = "New_butt"
        Me.New_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.New_butt.Size = New System.Drawing.Size(75, 40)
        Me.New_butt.TabIndex = 294
        Me.New_butt.Text = "جديد F1"
        Me.New_butt.UseVisualStyleBackColor = False
        '
        'Save_butt
        '
        Me.Save_butt.BackColor = System.Drawing.Color.White
        Me.Save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Save_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Save_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_butt.Font = New System.Drawing.Font("Segoe UI", 13.75!, System.Drawing.FontStyle.Bold)
        Me.Save_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Save_butt.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Save_butt.Location = New System.Drawing.Point(471, 4)
        Me.Save_butt.Name = "Save_butt"
        Me.Save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Save_butt.Size = New System.Drawing.Size(313, 40)
        Me.Save_butt.TabIndex = 293
        Me.Save_butt.TabStop = False
        Me.Save_butt.Tag = "SAVE"
        Me.Save_butt.Text = "حفظ F12"
        Me.Save_butt.UseVisualStyleBackColor = False
        '
        'note_Btn
        '
        Me.note_Btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.note_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.note_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.note_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.note_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.note_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.note_Btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.note_Btn.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.note_Btn.Location = New System.Drawing.Point(580, 497)
        Me.note_Btn.Name = "note_Btn"
        Me.note_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.note_Btn.Size = New System.Drawing.Size(110, 55)
        Me.note_Btn.TabIndex = 916
        Me.note_Btn.TabStop = False
        Me.note_Btn.Text = "ملاحظة 📝"
        Me.note_Btn.UseVisualStyleBackColor = False
        '
        'QTY_Btn
        '
        Me.QTY_Btn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.QTY_Btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(9, Byte), Integer))
        Me.QTY_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.QTY_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.QTY_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.QTY_Btn.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.QTY_Btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.QTY_Btn.Location = New System.Drawing.Point(446, 495)
        Me.QTY_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.QTY_Btn.Name = "QTY_Btn"
        Me.QTY_Btn.Size = New System.Drawing.Size(110, 55)
        Me.QTY_Btn.TabIndex = 917
        Me.QTY_Btn.Text = "الكمية 🖊️"
        Me.QTY_Btn.UseVisualStyleBackColor = False
        '
        'ScreenStatusStrip
        '
        Me.ScreenStatusStrip.AutoSize = False
        Me.ScreenStatusStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ScreenStatusStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.ScreenStatusStrip.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ScreenStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScreenStatusTypeLabel, Me.ScreenStatusMessageLabel, Me.ScreenStatusProgressBar, Me.ScreenStatusTimeLabel})
        Me.ScreenStatusStrip.Location = New System.Drawing.Point(471, 47)
        Me.ScreenStatusStrip.Name = "ScreenStatusStrip"
        Me.ScreenStatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ScreenStatusStrip.ShowItemToolTips = True
        Me.ScreenStatusStrip.Size = New System.Drawing.Size(405, 30)
        Me.ScreenStatusStrip.SizingGrip = False
        Me.ScreenStatusStrip.TabIndex = 918
        '
        'ScreenStatusTypeLabel
        '
        Me.ScreenStatusTypeLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ScreenStatusTypeLabel.Name = "ScreenStatusTypeLabel"
        Me.ScreenStatusTypeLabel.Size = New System.Drawing.Size(48, 25)
        Me.ScreenStatusTypeLabel.Text = "● جاهز"
        '
        'ScreenStatusMessageLabel
        '
        Me.ScreenStatusMessageLabel.Name = "ScreenStatusMessageLabel"
        Me.ScreenStatusMessageLabel.Size = New System.Drawing.Size(298, 25)
        Me.ScreenStatusMessageLabel.Spring = True
        Me.ScreenStatusMessageLabel.Text = "الشاشة جاهزة للاستخدام"
        Me.ScreenStatusMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ScreenStatusProgressBar
        '
        Me.ScreenStatusProgressBar.MarqueeAnimationSpeed = 25
        Me.ScreenStatusProgressBar.Name = "ScreenStatusProgressBar"
        Me.ScreenStatusProgressBar.Size = New System.Drawing.Size(65, 24)
        Me.ScreenStatusProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ScreenStatusProgressBar.Visible = False
        '
        'ScreenStatusTimeLabel
        '
        Me.ScreenStatusTimeLabel.ForeColor = System.Drawing.Color.DimGray
        Me.ScreenStatusTimeLabel.Name = "ScreenStatusTimeLabel"
        Me.ScreenStatusTimeLabel.Size = New System.Drawing.Size(44, 25)
        Me.ScreenStatusTimeLabel.Text = "00:00"
        '
        'UcGridColumnsSelector1
        '
        Me.UcGridColumnsSelector1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.UcGridColumnsSelector1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.UcGridColumnsSelector1.ForeColor = System.Drawing.Color.Black
        Me.UcGridColumnsSelector1.Location = New System.Drawing.Point(913, 609)
        Me.UcGridColumnsSelector1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.UcGridColumnsSelector1.Name = "UcGridColumnsSelector1"
        Me.UcGridColumnsSelector1.PopupMaxHeight = 320
        Me.UcGridColumnsSelector1.PopupMinHeight = 120
        Me.UcGridColumnsSelector1.PopupWidth = 260
        Me.UcGridColumnsSelector1.SettingsFolder = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\Grid" &
    "ColumnsSettings"
        Me.UcGridColumnsSelector1.Size = New System.Drawing.Size(109, 54)
        Me.UcGridColumnsSelector1.TabIndex = 915
        '
        'Sales_Fast_Draft
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(1024, 695)
        Me.Controls.Add(Me.ScreenStatusStrip)
        Me.Controls.Add(Me.ClearDraftItemsButton)
        Me.Controls.Add(Me.ChangePriceButton)
        Me.Controls.Add(Me.QTY_Btn)
        Me.Controls.Add(Me.note_Btn)
        Me.Controls.Add(Me.Barcode_SH_txt)
        Me.Controls.Add(Me.Calc_Dicount_Btn)
        Me.Controls.Add(Me.ChangeCustomerButton)
        Me.Controls.Add(Me.UcGridColumnsSelector1)
        Me.Controls.Add(Me.CALC_Btn)
        Me.Controls.Add(Me.Bill_ID_Txt)
        Me.Controls.Add(Me.OpenCahDR_Btn)
        Me.Controls.Add(Me.Units_btn)
        Me.Controls.Add(Me.Show_Cash_btn)
        Me.Controls.Add(Me.IMIncreaseButton)
        Me.Controls.Add(Me.IMDicreaseButton)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Draft_Btn)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Refresh_IM_Btn)
        Me.Controls.Add(Me.Valid_cm)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.IM_Search_btn)
        Me.Controls.Add(Me.AG_Panel)
        Me.Controls.Add(Me.RemoveCatButton)
        Me.Controls.Add(Me.IMPanel)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.IM_Qty_LB)
        Me.Controls.Add(Me.IM_Count_LB)
        Me.Controls.Add(Me.User_Name_lb)
        Me.Controls.Add(Me.DiscountPanel)
        Me.Controls.Add(Me.Print_btn)
        Me.Controls.Add(Me.PreviousBillsButton)
        Me.Controls.Add(Me.New_butt)
        Me.Controls.Add(Me.Save_butt)
        Me.Controls.Add(Me.IM_Unit_cm)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.txtNotes)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Sales_Fast_Draft"
        Me.Padding = New System.Windows.Forms.Padding(27, 97, 27, 32)
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "شاشة المبيعات"
        Me.DiscountPanel.ResumeLayout(False)
        Me.DiscountPanel.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.AG_Panel.ResumeLayout(False)
        Me.AG_Panel.PerformLayout()
        CType(Me.dgvSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.ScreenStatusStrip.ResumeLayout(False)
        Me.ScreenStatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
    Friend WithEvents New_butt As System.Windows.Forms.Button
    Friend WithEvents Save_butt As System.Windows.Forms.Button
    Friend WithEvents Total_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents DateTimeEx As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents RemoveCatButton As System.Windows.Forms.Button
    Friend WithEvents IM_Count_LB As System.Windows.Forms.Label
    Friend WithEvents IM_Unit_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Pure_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Print_btn As System.Windows.Forms.Button
    Friend WithEvents Bill_ID_Txt As System.Windows.Forms.TextBox
    Friend WithEvents User_Name_lb As System.Windows.Forms.Label
    Friend WithEvents PreviousBillsButton As System.Windows.Forms.Button
    Friend WithEvents DiscountPanel As System.Windows.Forms.Panel
    Friend WithEvents Discount_txt As System.Windows.Forms.TextBox
    Friend WithEvents IM_Qty_LB As System.Windows.Forms.Label
    Friend WithEvents OpenCahDR_Btn As System.Windows.Forms.Button
    Friend WithEvents Show_Cash_btn As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents IMPanel As System.Windows.Forms.Panel
    Friend WithEvents AG_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents ChangeCustomerButton As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents AG_Panel As System.Windows.Forms.Panel
    Friend WithEvents dgvSales As System.Windows.Forms.DataGridView
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents IM_Search_btn As System.Windows.Forms.Button
    Friend WithEvents CALC_Btn As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Valid_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Barcode_SH_txt As TextBox
    Friend WithEvents Refresh_IM_Btn As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Draft_Btn As Button
    Friend WithEvents Bill_IMID_CL As DataGridViewTextBoxColumn
    Friend WithEvents is_Check_CL As DataGridViewTextBoxColumn
    Friend WithEvents Barcode_CL As DataGridViewTextBoxColumn
    Friend WithEvents Serial_Code_CL As DataGridViewTextBoxColumn
    Friend WithEvents U_ID As DataGridViewTextBoxColumn
    Friend WithEvents Date_ As DataGridViewTextBoxColumn
    Friend WithEvents ST_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents IMNUM_CL As DataGridViewTextBoxColumn
    Friend WithEvents Item_Name As DataGridViewTextBoxColumn
    Friend WithEvents D_Valid_CL As DataGridViewTextBoxColumn
    Friend WithEvents IMUnit_CL As DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As DataGridViewTextBoxColumn
    Friend WithEvents Price_CL As DataGridViewTextBoxColumn
    Friend WithEvents IM_Discount_CL As DataGridViewTextBoxColumn
    Friend WithEvents Total_CL As DataGridViewTextBoxColumn
    Friend WithEvents Notes_CL As DataGridViewTextBoxColumn
    Friend WithEvents T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents ST_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents IM_NOTE_CL As DataGridViewTextBoxColumn
    Friend WithEvents IMIncreaseButton As Button
    Friend WithEvents IMDicreaseButton As Button
    Friend WithEvents Units_btn As Button
    Public WithEvents Calc_Dicount_Btn As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents UcGridColumnsSelector1 As UcGridColumnsSelector
    Friend WithEvents note_Btn As Button
    Friend WithEvents QTY_Btn As Button
    Friend WithEvents ChangePriceButton As Button
    Friend WithEvents ClearDraftItemsButton As Button
    Friend WithEvents ScreenStatusStrip As StatusStrip
    Friend WithEvents ScreenStatusTypeLabel As ToolStripStatusLabel
    Friend WithEvents ScreenStatusMessageLabel As ToolStripStatusLabel
    Friend WithEvents ScreenStatusProgressBar As ToolStripProgressBar
    Friend WithEvents ScreenStatusTimeLabel As ToolStripStatusLabel
End Class
