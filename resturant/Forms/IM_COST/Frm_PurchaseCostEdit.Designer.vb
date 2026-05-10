<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_PurchaseCostEdit

    Inherits System.Windows.Forms.Form

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label

    Friend WithEvents Txt_ItemName As TextBox
    Friend WithEvents Txt_PurchaseId As TextBox
    Friend WithEvents Txt_PurchaseDetailId As TextBox
    Friend WithEvents Txt_OldPrice As TextBox
    Friend WithEvents Txt_NewPrice As TextBox
    Friend WithEvents Txt_Diff As TextBox

    Friend WithEvents Dtp_PurchaseDate As DateTimePicker

    Friend WithEvents Btn_CheckImpact As Button
    Friend WithEvents Btn_SaveOnly As Button
    Friend WithEvents Btn_Close As Button
    Friend WithEvents TitleBar_Panel As Panel
    Friend WithEvents TopTitle_LB As Label
    Friend WithEvents Help_LB As Label
    Friend WithEvents DetailsTitle_LB As Label

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.Help_LB = New System.Windows.Forms.Label()
        Me.DetailsTitle_LB = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Txt_ItemName = New System.Windows.Forms.TextBox()
        Me.Txt_PurchaseId = New System.Windows.Forms.TextBox()
        Me.Txt_PurchaseDetailId = New System.Windows.Forms.TextBox()
        Me.Txt_OldPrice = New System.Windows.Forms.TextBox()
        Me.Txt_NewPrice = New System.Windows.Forms.TextBox()
        Me.Txt_Diff = New System.Windows.Forms.TextBox()
        Me.Dtp_PurchaseDate = New System.Windows.Forms.DateTimePicker()
        Me.Btn_CheckImpact = New System.Windows.Forms.Button()
        Me.Btn_SaveOnly = New System.Windows.Forms.Button()
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.TitleBar_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(760, 44)
        Me.TitleBar_Panel.TabIndex = 16
        '
        'TopTitle_LB
        '
        Me.TopTitle_LB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TopTitle_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TopTitle_LB.ForeColor = System.Drawing.Color.White
        Me.TopTitle_LB.Location = New System.Drawing.Point(0, 0)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Padding = New System.Windows.Forms.Padding(16, 0, 16, 0)
        Me.TopTitle_LB.Size = New System.Drawing.Size(760, 44)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Text = "تعديل سعر شراء الصنف"
        Me.TopTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Help_LB
        '
        Me.Help_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Help_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.Help_LB.Location = New System.Drawing.Point(24, 55)
        Me.Help_LB.Name = "Help_LB"
        Me.Help_LB.Size = New System.Drawing.Size(712, 24)
        Me.Help_LB.TabIndex = 17
        Me.Help_LB.Text = "راجع بيانات فاتورة الشراء، أدخل السعر الجديد، ثم احفظ أو افحص أثر إعادة الاحتساب."
        Me.Help_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DetailsTitle_LB
        '
        Me.DetailsTitle_LB.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.DetailsTitle_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.DetailsTitle_LB.Location = New System.Drawing.Point(24, 86)
        Me.DetailsTitle_LB.Name = "DetailsTitle_LB"
        Me.DetailsTitle_LB.Size = New System.Drawing.Size(712, 22)
        Me.DetailsTitle_LB.TabIndex = 18
        Me.DetailsTitle_LB.Text = "بيانات السعر"
        Me.DetailsTitle_LB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(634, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "الصنف"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(635, 157)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 23)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "رقم الفاتورة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(310, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 23)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "رقم السطر"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(635, 196)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 23)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "تاريخ الشراء"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(635, 236)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 23)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "السعر القديم"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(310, 236)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 23)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "السعر الجديد"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(635, 276)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 23)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "فرق السعر"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_ItemName
        '
        Me.Txt_ItemName.BackColor = System.Drawing.Color.White
        Me.Txt_ItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_ItemName.Location = New System.Drawing.Point(40, 118)
        Me.Txt_ItemName.Name = "Txt_ItemName"
        Me.Txt_ItemName.ReadOnly = True
        Me.Txt_ItemName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_ItemName.Size = New System.Drawing.Size(580, 23)
        Me.Txt_ItemName.TabIndex = 6
        '
        'Txt_PurchaseId
        '
        Me.Txt_PurchaseId.BackColor = System.Drawing.Color.White
        Me.Txt_PurchaseId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_PurchaseId.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_PurchaseId.Location = New System.Drawing.Point(440, 157)
        Me.Txt_PurchaseId.Name = "Txt_PurchaseId"
        Me.Txt_PurchaseId.ReadOnly = True
        Me.Txt_PurchaseId.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_PurchaseId.Size = New System.Drawing.Size(180, 23)
        Me.Txt_PurchaseId.TabIndex = 7
        Me.Txt_PurchaseId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_PurchaseDetailId
        '
        Me.Txt_PurchaseDetailId.BackColor = System.Drawing.Color.White
        Me.Txt_PurchaseDetailId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_PurchaseDetailId.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_PurchaseDetailId.Location = New System.Drawing.Point(120, 157)
        Me.Txt_PurchaseDetailId.Name = "Txt_PurchaseDetailId"
        Me.Txt_PurchaseDetailId.ReadOnly = True
        Me.Txt_PurchaseDetailId.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_PurchaseDetailId.Size = New System.Drawing.Size(180, 23)
        Me.Txt_PurchaseDetailId.TabIndex = 8
        Me.Txt_PurchaseDetailId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_OldPrice
        '
        Me.Txt_OldPrice.BackColor = System.Drawing.Color.White
        Me.Txt_OldPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_OldPrice.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_OldPrice.Location = New System.Drawing.Point(440, 236)
        Me.Txt_OldPrice.Name = "Txt_OldPrice"
        Me.Txt_OldPrice.ReadOnly = True
        Me.Txt_OldPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_OldPrice.Size = New System.Drawing.Size(180, 23)
        Me.Txt_OldPrice.TabIndex = 9
        Me.Txt_OldPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_NewPrice
        '
        Me.Txt_NewPrice.BackColor = System.Drawing.Color.White
        Me.Txt_NewPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NewPrice.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_NewPrice.Location = New System.Drawing.Point(120, 236)
        Me.Txt_NewPrice.Name = "Txt_NewPrice"
        Me.Txt_NewPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_NewPrice.Size = New System.Drawing.Size(180, 23)
        Me.Txt_NewPrice.TabIndex = 10
        Me.Txt_NewPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_Diff
        '
        Me.Txt_Diff.BackColor = System.Drawing.Color.White
        Me.Txt_Diff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Diff.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_Diff.Location = New System.Drawing.Point(440, 276)
        Me.Txt_Diff.Name = "Txt_Diff"
        Me.Txt_Diff.ReadOnly = True
        Me.Txt_Diff.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Txt_Diff.Size = New System.Drawing.Size(180, 23)
        Me.Txt_Diff.TabIndex = 11
        Me.Txt_Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Dtp_PurchaseDate
        '
        Me.Dtp_PurchaseDate.CustomFormat = "yyyy/MM/dd HH:mm"
        Me.Dtp_PurchaseDate.Enabled = False
        Me.Dtp_PurchaseDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Dtp_PurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_PurchaseDate.Location = New System.Drawing.Point(440, 196)
        Me.Dtp_PurchaseDate.Name = "Dtp_PurchaseDate"
        Me.Dtp_PurchaseDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Dtp_PurchaseDate.Size = New System.Drawing.Size(180, 23)
        Me.Dtp_PurchaseDate.TabIndex = 12
        '
        'Btn_CheckImpact
        '
        Me.Btn_CheckImpact.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Btn_CheckImpact.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_CheckImpact.FlatAppearance.BorderSize = 0
        Me.Btn_CheckImpact.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_CheckImpact.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_CheckImpact.ForeColor = System.Drawing.Color.White
        Me.Btn_CheckImpact.Location = New System.Drawing.Point(500, 340)
        Me.Btn_CheckImpact.Name = "Btn_CheckImpact"
        Me.Btn_CheckImpact.Size = New System.Drawing.Size(160, 32)
        Me.Btn_CheckImpact.TabIndex = 13
        Me.Btn_CheckImpact.Text = "فحص أثر التكلفة"
        Me.Btn_CheckImpact.UseVisualStyleBackColor = False
        '
        'Btn_SaveOnly
        '
        Me.Btn_SaveOnly.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.Btn_SaveOnly.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_SaveOnly.FlatAppearance.BorderSize = 0
        Me.Btn_SaveOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_SaveOnly.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_SaveOnly.ForeColor = System.Drawing.Color.White
        Me.Btn_SaveOnly.Location = New System.Drawing.Point(330, 340)
        Me.Btn_SaveOnly.Name = "Btn_SaveOnly"
        Me.Btn_SaveOnly.Size = New System.Drawing.Size(160, 32)
        Me.Btn_SaveOnly.TabIndex = 14
        Me.Btn_SaveOnly.Text = "حفظ فقط"
        Me.Btn_SaveOnly.UseVisualStyleBackColor = False
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.IndianRed
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.FlatAppearance.BorderSize = 0
        Me.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Close.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Close.ForeColor = System.Drawing.Color.White
        Me.Btn_Close.Location = New System.Drawing.Point(160, 340)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(160, 32)
        Me.Btn_Close.TabIndex = 15
        Me.Btn_Close.Text = "إغلاق"
        Me.Btn_Close.UseVisualStyleBackColor = False
        '
        'Frm_PurchaseCostEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(760, 420)
        Me.Controls.Add(Me.DetailsTitle_LB)
        Me.Controls.Add(Me.Help_LB)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Txt_ItemName)
        Me.Controls.Add(Me.Txt_PurchaseId)
        Me.Controls.Add(Me.Txt_PurchaseDetailId)
        Me.Controls.Add(Me.Txt_OldPrice)
        Me.Controls.Add(Me.Txt_NewPrice)
        Me.Controls.Add(Me.Txt_Diff)
        Me.Controls.Add(Me.Dtp_PurchaseDate)
        Me.Controls.Add(Me.Btn_CheckImpact)
        Me.Controls.Add(Me.Btn_SaveOnly)
        Me.Controls.Add(Me.Btn_Close)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_PurchaseCostEdit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تعديل سعر شراء الصنف"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class
