<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Add_Prev_Balance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Add_Prev_Balance))
        Me.Debit_txt = New System.Windows.Forms.TextBox()
        Me.NULLContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label7 = New System.Windows.Forms.Label()
        Me.BackButton = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DeleteSButton = New System.Windows.Forms.Button()
        Me.SaveSButton = New System.Windows.Forms.Button()
        Me.Credit_txt = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Notes_txt = New System.Windows.Forms.TextBox()
        Me.VoidLb = New System.Windows.Forms.Label()
        Me.DateTime = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Debit_txt
        '
        Me.Debit_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Debit_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Debit_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Debit_txt.Font = New System.Drawing.Font("Stencil", 15.7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Debit_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Debit_txt.Location = New System.Drawing.Point(2, 80)
        Me.Debit_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Debit_txt.MaxLength = 250
        Me.Debit_txt.Name = "Debit_txt"
        Me.Debit_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Debit_txt.Size = New System.Drawing.Size(157, 32)
        Me.Debit_txt.TabIndex = 617
        '
        'NULLContextMenuStrip
        '
        Me.NULLContextMenuStrip.Name = "NULLContextMenuStrip"
        Me.NULLContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NULLContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(162, 82)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 28)
        Me.Label7.TabIndex = 620
        Me.Label7.Text = "(دائن)له"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'BackButton
        '
        Me.BackButton.BackColor = System.Drawing.Color.IndianRed
        Me.BackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BackButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BackButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.BackButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BackButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BackButton.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.BackButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.BackButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BackButton.Location = New System.Drawing.Point(410, 292)
        Me.BackButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BackButton.Name = "BackButton"
        Me.BackButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BackButton.Size = New System.Drawing.Size(130, 43)
        Me.BackButton.TabIndex = 636
        Me.BackButton.TabStop = False
        Me.BackButton.Text = "رجوع"
        Me.BackButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BackButton.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(162, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 28)
        Me.Label4.TabIndex = 689
        Me.Label4.Text = "(مدين)عليه"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'DeleteSButton
        '
        Me.DeleteSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteSButton.Enabled = False
        Me.DeleteSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DeleteSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DeleteSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DeleteSButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.DeleteSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteSButton.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.DeleteSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteSButton.Location = New System.Drawing.Point(2, 292)
        Me.DeleteSButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DeleteSButton.Name = "DeleteSButton"
        Me.DeleteSButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DeleteSButton.Size = New System.Drawing.Size(111, 43)
        Me.DeleteSButton.TabIndex = 692
        Me.DeleteSButton.Text = "إلغــاء"
        Me.DeleteSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteSButton.UseVisualStyleBackColor = False
        '
        'SaveSButton
        '
        Me.SaveSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SaveSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SaveSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SaveSButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.SaveSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveSButton.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.SaveSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveSButton.Location = New System.Drawing.Point(116, 292)
        Me.SaveSButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SaveSButton.Name = "SaveSButton"
        Me.SaveSButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SaveSButton.Size = New System.Drawing.Size(111, 43)
        Me.SaveSButton.TabIndex = 690
        Me.SaveSButton.Text = "حفظ"
        Me.SaveSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveSButton.UseVisualStyleBackColor = False
        '
        'Credit_txt
        '
        Me.Credit_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Credit_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Credit_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Credit_txt.Font = New System.Drawing.Font("Stencil", 15.7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Credit_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Credit_txt.Location = New System.Drawing.Point(3, 120)
        Me.Credit_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Credit_txt.MaxLength = 250
        Me.Credit_txt.Name = "Credit_txt"
        Me.Credit_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Credit_txt.Size = New System.Drawing.Size(156, 32)
        Me.Credit_txt.TabIndex = 693
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label15.Location = New System.Drawing.Point(333, 166)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(67, 28)
        Me.Label15.TabIndex = 694
        Me.Label15.Text = "العنوان"
        '
        'Notes_txt
        '
        Me.Notes_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Notes_txt.Location = New System.Drawing.Point(3, 167)
        Me.Notes_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Notes_txt.MaxLength = 350
        Me.Notes_txt.Name = "Notes_txt"
        Me.Notes_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Notes_txt.Size = New System.Drawing.Size(327, 29)
        Me.Notes_txt.TabIndex = 695
        '
        'VoidLb
        '
        Me.VoidLb.BackColor = System.Drawing.Color.IndianRed
        Me.VoidLb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VoidLb.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.VoidLb.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.VoidLb.Location = New System.Drawing.Point(399, 2)
        Me.VoidLb.Name = "VoidLb"
        Me.VoidLb.Size = New System.Drawing.Size(140, 64)
        Me.VoidLb.TabIndex = 696
        Me.VoidLb.Text = "معاملة ملغية"
        Me.VoidLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.VoidLb.Visible = False
        '
        'DateTime
        '
        Me.DateTime.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DateTime.CustomFormat = "dd/MM/yyyy"
        Me.DateTime.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.DateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTime.Location = New System.Drawing.Point(149, 200)
        Me.DateTime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateTime.Name = "DateTime"
        Me.DateTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTime.RightToLeftLayout = True
        Me.DateTime.Size = New System.Drawing.Size(181, 28)
        Me.DateTime.TabIndex = 697
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(334, 199)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 28)
        Me.Label5.TabIndex = 698
        Me.Label5.Text = "التاريخ"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox1.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"رصيد سابق", "إثبــات مرتب"})
        Me.ComboBox1.Location = New System.Drawing.Point(3, 2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(305, 29)
        Me.ComboBox1.TabIndex = 699
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(313, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 28)
        Me.Label1.TabIndex = 700
        Me.Label1.Text = "النــوع"
        '
        'Add_Prev_Balance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 28.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 335)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.DateTime)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.VoidLb)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Notes_txt)
        Me.Controls.Add(Me.Credit_txt)
        Me.Controls.Add(Me.DeleteSButton)
        Me.Controls.Add(Me.SaveSButton)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.BackButton)
        Me.Controls.Add(Me.Debit_txt)
        Me.Controls.Add(Me.Label7)
        Me.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Add_Prev_Balance"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "رصيد سابق"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Debit_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents NULLContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BackButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DeleteSButton As System.Windows.Forms.Button
    Friend WithEvents SaveSButton As System.Windows.Forms.Button
    Friend WithEvents Credit_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents VoidLb As System.Windows.Forms.Label
    Friend WithEvents DateTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
