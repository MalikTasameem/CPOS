<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ADD_IM_TO_Valid_Notes
    Inherits Base_Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ADD_IM_TO_Valid_Notes))
        Me.Valid_For_List_Date = New System.Windows.Forms.DateTimePicker()
        Me.Add_Valid_Btn = New System.Windows.Forms.Button()
        Me.IM_Cm = New resturant.FSearch_Filter()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Valid_For_List_Date
        '
        Me.Valid_For_List_Date.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Valid_For_List_Date.CalendarFont = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Valid_For_List_Date.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Valid_For_List_Date.CustomFormat = "dd-MM-yyyy"
        Me.Valid_For_List_Date.Font = New System.Drawing.Font("Arial Narrow", 14.75!, System.Drawing.FontStyle.Bold)
        Me.Valid_For_List_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Valid_For_List_Date.Location = New System.Drawing.Point(567, 8)
        Me.Valid_For_List_Date.Name = "Valid_For_List_Date"
        Me.Valid_For_List_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Valid_For_List_Date.RightToLeftLayout = True
        Me.Valid_For_List_Date.Size = New System.Drawing.Size(157, 30)
        Me.Valid_For_List_Date.TabIndex = 712
        '
        'Add_Valid_Btn
        '
        Me.Add_Valid_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Add_Valid_Btn.BackColor = System.Drawing.Color.White
        Me.Add_Valid_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Add_Valid_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Add_Valid_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Add_Valid_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Add_Valid_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Add_Valid_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Add_Valid_Btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Add_Valid_Btn.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.Add_Valid_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Add_Valid_Btn.Location = New System.Drawing.Point(534, 243)
        Me.Add_Valid_Btn.Name = "Add_Valid_Btn"
        Me.Add_Valid_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Add_Valid_Btn.Size = New System.Drawing.Size(190, 44)
        Me.Add_Valid_Btn.TabIndex = 714
        Me.Add_Valid_Btn.TabStop = False
        Me.Add_Valid_Btn.Text = "أضف الصـــنف"
        Me.Add_Valid_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Add_Valid_Btn.UseVisualStyleBackColor = False
        '
        'IM_Cm
        '
        Me.IM_Cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IM_Cm.CancelSearchImage = CType(resources.GetObject("IM_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.IM_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Cm.Location = New System.Drawing.Point(77, 5)
        Me.IM_Cm.Name = "IM_Cm"
        Me.IM_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_Cm.Size = New System.Drawing.Size(406, 34)
        Me.IM_Cm.SQL_Column = "item_name"
        Me.IM_Cm.SQL_ID = "IM_ID"
        Me.IM_Cm.SQL_IsNumericSearchField = False
        Me.IM_Cm.SQL_ListSize = 200
        Me.IM_Cm.SQL_NumberOfRows = 200
        Me.IM_Cm.SQL_OrderByField = "item_name"
        Me.IM_Cm.SQL_SearchField = "item_name"
        Me.IM_Cm.SQL_Table = "IM_All_V"
        Me.IM_Cm.TabIndex = 713
        Me.IM_Cm.TextMaxLength = 250
        Me.IM_Cm.Textt = ""
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 28)
        Me.Label1.TabIndex = 715
        Me.Label1.Text = "الصنف"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(486, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 28)
        Me.Label2.TabIndex = 716
        Me.Label2.Text = "الصلاحية"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(2, 242)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(110, 44)
        Me.ExitFormButton.TabIndex = 717
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(1, 46)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(73, 28)
        Me.Label12.TabIndex = 719
        Me.Label12.Text = "الباركود"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Barcode_SH_txt
        '
        Me.Barcode_SH_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Barcode_SH_txt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Times New Roman", 18.0!)
        Me.Barcode_SH_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_SH_txt.Location = New System.Drawing.Point(77, 42)
        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
        Me.Barcode_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Barcode_SH_txt.Size = New System.Drawing.Size(406, 35)
        Me.Barcode_SH_txt.TabIndex = 718
        Me.Barcode_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ADD_IM_TO_Valid_Notes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 26.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(725, 287)
        Me.ControlBox = False
        Me.Controls.Add(Me.IM_Cm)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Barcode_SH_txt)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Add_Valid_Btn)
        Me.Controls.Add(Me.Valid_For_List_Date)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ADD_IM_TO_Valid_Notes"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.Text = "إضافة صنف لقائمة الإشعارات"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Add_Valid_Btn As System.Windows.Forms.Button
    Friend WithEvents IM_Cm As resturant.FSearch_Filter
    Friend WithEvents Valid_For_List_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Barcode_SH_txt As System.Windows.Forms.TextBox
End Class
