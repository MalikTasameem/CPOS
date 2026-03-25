<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Exp_Static_Add
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Exp_Static_Add))
        Me.PriceTextBox = New System.Windows.Forms.TextBox()
        Me.NULLContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.QtyTextBox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.is_EXP_DATE_CB = New System.Windows.Forms.CheckBox()
        Me.Exp_Name_txt = New System.Windows.Forms.TextBox()
        Me.DateTimeEx = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Age_Txt = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DeleteSButton = New System.Windows.Forms.Button()
        Me.EditSButton = New System.Windows.Forms.Button()
        Me.SaveSButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'PriceTextBox
        '
        Me.PriceTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.PriceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PriceTextBox.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.PriceTextBox.Font = New System.Drawing.Font("Stencil", 15.7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PriceTextBox.ForeColor = System.Drawing.Color.DarkGreen
        Me.PriceTextBox.Location = New System.Drawing.Point(282, 147)
        Me.PriceTextBox.MaxLength = 250
        Me.PriceTextBox.Name = "PriceTextBox"
        Me.PriceTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PriceTextBox.Size = New System.Drawing.Size(136, 32)
        Me.PriceTextBox.TabIndex = 617
        '
        'NULLContextMenuStrip
        '
        Me.NULLContextMenuStrip.Name = "NULLContextMenuStrip"
        Me.NULLContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NULLContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'QtyTextBox
        '
        Me.QtyTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.QtyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.QtyTextBox.Font = New System.Drawing.Font("Times New Roman", 14.0!, System.Drawing.FontStyle.Bold)
        Me.QtyTextBox.ForeColor = System.Drawing.Color.Black
        Me.QtyTextBox.Location = New System.Drawing.Point(282, 113)
        Me.QtyTextBox.MaxLength = 250
        Me.QtyTextBox.Name = "QtyTextBox"
        Me.QtyTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.QtyTextBox.Size = New System.Drawing.Size(136, 29)
        Me.QtyTextBox.TabIndex = 618
        Me.QtyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(421, 150)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(143, 26)
        Me.Label7.TabIndex = 620
        Me.Label7.Text = "سعر شراء القطعة"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(99, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(244, 26)
        Me.Label2.TabIndex = 619
        Me.Label2.Text = "العمر الإفتراضي بالسنة للقطعة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(518, 208)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(98, 44)
        Me.ExitFormButton.TabIndex = 636
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "رجوع"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'is_EXP_DATE_CB
        '
        Me.is_EXP_DATE_CB.AutoSize = True
        Me.is_EXP_DATE_CB.BackColor = System.Drawing.Color.Transparent
        Me.is_EXP_DATE_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_EXP_DATE_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.is_EXP_DATE_CB.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.is_EXP_DATE_CB.ForeColor = System.Drawing.Color.Black
        Me.is_EXP_DATE_CB.Location = New System.Drawing.Point(386, 37)
        Me.is_EXP_DATE_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.is_EXP_DATE_CB.Name = "is_EXP_DATE_CB"
        Me.is_EXP_DATE_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.is_EXP_DATE_CB.Size = New System.Drawing.Size(104, 30)
        Me.is_EXP_DATE_CB.TabIndex = 685
        Me.is_EXP_DATE_CB.Text = "إستهلاكي"
        Me.is_EXP_DATE_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.is_EXP_DATE_CB.UseVisualStyleBackColor = False
        '
        'Exp_Name_txt
        '
        Me.Exp_Name_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Exp_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Exp_Name_txt.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Exp_Name_txt.ForeColor = System.Drawing.Color.Black
        Me.Exp_Name_txt.Location = New System.Drawing.Point(2, 1)
        Me.Exp_Name_txt.MaxLength = 250
        Me.Exp_Name_txt.Name = "Exp_Name_txt"
        Me.Exp_Name_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Exp_Name_txt.Size = New System.Drawing.Size(491, 32)
        Me.Exp_Name_txt.TabIndex = 684
        Me.Exp_Name_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DateTimeEx
        '
        Me.DateTimeEx.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimeEx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DateTimeEx.CustomFormat = "dd/MM/yyyy"
        Me.DateTimeEx.Font = New System.Drawing.Font("Times New Roman", 14.0!, System.Drawing.FontStyle.Bold)
        Me.DateTimeEx.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimeEx.Location = New System.Drawing.Point(282, 79)
        Me.DateTimeEx.Name = "DateTimeEx"
        Me.DateTimeEx.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTimeEx.RightToLeftLayout = True
        Me.DateTimeEx.Size = New System.Drawing.Size(136, 29)
        Me.DateTimeEx.TabIndex = 683
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(497, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 26)
        Me.Label1.TabIndex = 686
        Me.Label1.Text = "إسم المصروف"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(421, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 26)
        Me.Label3.TabIndex = 687
        Me.Label3.Text = "التاريخ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Age_Txt
        '
        Me.Age_Txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Age_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Age_Txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Age_Txt.Font = New System.Drawing.Font("Times New Roman", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Age_Txt.ForeColor = System.Drawing.Color.Black
        Me.Age_Txt.Location = New System.Drawing.Point(2, 36)
        Me.Age_Txt.MaxLength = 250
        Me.Age_Txt.Name = "Age_Txt"
        Me.Age_Txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Age_Txt.Size = New System.Drawing.Size(94, 29)
        Me.Age_Txt.TabIndex = 688
        Me.Age_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(421, 114)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 26)
        Me.Label4.TabIndex = 689
        Me.Label4.Text = "الكمية"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'DeleteSButton
        '
        Me.DeleteSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteSButton.Enabled = False
        Me.DeleteSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DeleteSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteSButton.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.DeleteSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteSButton.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.DeleteSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteSButton.Location = New System.Drawing.Point(2, 208)
        Me.DeleteSButton.Name = "DeleteSButton"
        Me.DeleteSButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DeleteSButton.Size = New System.Drawing.Size(149, 44)
        Me.DeleteSButton.TabIndex = 692
        Me.DeleteSButton.Text = "حذف"
        Me.DeleteSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteSButton.UseVisualStyleBackColor = False
        '
        'EditSButton
        '
        Me.EditSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EditSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditSButton.Enabled = False
        Me.EditSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.EditSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EditSButton.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.EditSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.EditSButton.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.EditSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditSButton.Location = New System.Drawing.Point(154, 208)
        Me.EditSButton.Name = "EditSButton"
        Me.EditSButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.EditSButton.Size = New System.Drawing.Size(149, 44)
        Me.EditSButton.TabIndex = 691
        Me.EditSButton.Text = "تأكيد التعديل"
        Me.EditSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EditSButton.UseVisualStyleBackColor = False
        '
        'SaveSButton
        '
        Me.SaveSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveSButton.Enabled = False
        Me.SaveSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SaveSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveSButton.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.SaveSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveSButton.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.SaveSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveSButton.Location = New System.Drawing.Point(306, 208)
        Me.SaveSButton.Name = "SaveSButton"
        Me.SaveSButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SaveSButton.Size = New System.Drawing.Size(149, 44)
        Me.SaveSButton.TabIndex = 690
        Me.SaveSButton.Text = "حفظ"
        Me.SaveSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveSButton.UseVisualStyleBackColor = False
        '
        'Exp_Static_Add
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(618, 252)
        Me.Controls.Add(Me.DeleteSButton)
        Me.Controls.Add(Me.EditSButton)
        Me.Controls.Add(Me.SaveSButton)
        Me.Controls.Add(Me.Age_Txt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.is_EXP_DATE_CB)
        Me.Controls.Add(Me.Exp_Name_txt)
        Me.Controls.Add(Me.DateTimeEx)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.PriceTextBox)
        Me.Controls.Add(Me.QtyTextBox)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Exp_Static_Add"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " + إضافة مصروف "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PriceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents QtyTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NULLContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents is_EXP_DATE_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Exp_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents DateTimeEx As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Age_Txt As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DeleteSButton As System.Windows.Forms.Button
    Friend WithEvents EditSButton As System.Windows.Forms.Button
    Friend WithEvents SaveSButton As System.Windows.Forms.Button
End Class
