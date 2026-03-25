<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Mang_IM_Valid_Notes
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
        Me.Valid_For_List_Date = New System.Windows.Forms.DateTimePicker()
        Me.Valid_ListBox = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Remove_Valid_Btn = New System.Windows.Forms.Button()
        Me.Add_Valid_Btn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Valid_For_List_Date
        '
        Me.Valid_For_List_Date.CalendarFont = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Valid_For_List_Date.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Valid_For_List_Date.CustomFormat = "dd-MM-yyyy"
        Me.Valid_For_List_Date.Font = New System.Drawing.Font("Times New Roman", 14.0!)
        Me.Valid_For_List_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Valid_For_List_Date.Location = New System.Drawing.Point(63, 35)
        Me.Valid_For_List_Date.Name = "Valid_For_List_Date"
        Me.Valid_For_List_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Valid_For_List_Date.RightToLeftLayout = True
        Me.Valid_For_List_Date.Size = New System.Drawing.Size(320, 29)
        Me.Valid_For_List_Date.TabIndex = 712
        '
        'Valid_ListBox
        '
        Me.Valid_ListBox.Font = New System.Drawing.Font("Times New Roman", 14.0!)
        Me.Valid_ListBox.FormattingEnabled = True
        Me.Valid_ListBox.ItemHeight = 21
        Me.Valid_ListBox.Location = New System.Drawing.Point(0, 65)
        Me.Valid_ListBox.Name = "Valid_ListBox"
        Me.Valid_ListBox.Size = New System.Drawing.Size(383, 277)
        Me.Valid_ListBox.TabIndex = 711
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 12.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 28)
        Me.Label1.TabIndex = 715
        Me.Label1.Text = "---"
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(1, 343)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(383, 38)
        Me.ExitFormButton.TabIndex = 716
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Remove_Valid_Btn
        '
        Me.Remove_Valid_Btn.BackColor = System.Drawing.Color.White
        Me.Remove_Valid_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_minus_173055
        Me.Remove_Valid_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Remove_Valid_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Remove_Valid_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Remove_Valid_Btn.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Remove_Valid_Btn.Location = New System.Drawing.Point(1, 35)
        Me.Remove_Valid_Btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Remove_Valid_Btn.Name = "Remove_Valid_Btn"
        Me.Remove_Valid_Btn.Size = New System.Drawing.Size(30, 29)
        Me.Remove_Valid_Btn.TabIndex = 714
        Me.Remove_Valid_Btn.UseVisualStyleBackColor = False
        '
        'Add_Valid_Btn
        '
        Me.Add_Valid_Btn.BackColor = System.Drawing.Color.White
        Me.Add_Valid_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.Add_Valid_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Add_Valid_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Add_Valid_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Add_Valid_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Add_Valid_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Add_Valid_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Add_Valid_Btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Add_Valid_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Add_Valid_Btn.Location = New System.Drawing.Point(32, 35)
        Me.Add_Valid_Btn.Name = "Add_Valid_Btn"
        Me.Add_Valid_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Add_Valid_Btn.Size = New System.Drawing.Size(30, 29)
        Me.Add_Valid_Btn.TabIndex = 713
        Me.Add_Valid_Btn.TabStop = False
        Me.Add_Valid_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Add_Valid_Btn.UseVisualStyleBackColor = False
        '
        'Mang_IM_Notes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 26.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(385, 382)
        Me.ControlBox = False
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Remove_Valid_Btn)
        Me.Controls.Add(Me.Add_Valid_Btn)
        Me.Controls.Add(Me.Valid_For_List_Date)
        Me.Controls.Add(Me.Valid_ListBox)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Mang_IM_Notes"
        Me.Text = "إدارة صلاحيات الصنف"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Remove_Valid_Btn As System.Windows.Forms.Button
    Friend WithEvents Add_Valid_Btn As System.Windows.Forms.Button
    Friend WithEvents Valid_For_List_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Valid_ListBox As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
End Class
