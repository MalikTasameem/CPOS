<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_Update_Qty
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
        Me.Cost_txt = New System.Windows.Forms.TextBox()
        Me.NULLContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Unit_txt = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.IM_Name_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Valid_Date = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ST_Name_txt = New System.Windows.Forms.TextBox()
        Me.Valid_Panel = New System.Windows.Forms.Panel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.ConfermButton = New System.Windows.Forms.Button()
        Me.QTY_txt = New System.Windows.Forms.TextBox()
        Me.New_Qty_txt = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Qty_Deference_Txt = New System.Windows.Forms.TextBox()
        Me.Tag_txt = New System.Windows.Forms.TextBox()
        Me.Valid_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Cost_txt
        '
        Me.Cost_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cost_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Cost_txt.Font = New System.Drawing.Font("Stencil", 20.0!)
        Me.Cost_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Cost_txt.Location = New System.Drawing.Point(142, 121)
        Me.Cost_txt.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Cost_txt.Name = "Cost_txt"
        Me.Cost_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Cost_txt.Size = New System.Drawing.Size(166, 39)
        Me.Cost_txt.TabIndex = 459
        Me.Cost_txt.Visible = False
        '
        'NULLContextMenuStrip
        '
        Me.NULLContextMenuStrip.Name = "NULLContextMenuStrip"
        Me.NULLContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NULLContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(9, 170)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(129, 28)
        Me.Label8.TabIndex = 460
        Me.Label8.Text = "الكمية السابقة"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Unit_txt
        '
        Me.Unit_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Unit_txt.Enabled = False
        Me.Unit_txt.Font = New System.Drawing.Font("JF Flat", 13.0!)
        Me.Unit_txt.Location = New System.Drawing.Point(79, 80)
        Me.Unit_txt.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Unit_txt.Name = "Unit_txt"
        Me.Unit_txt.Size = New System.Drawing.Size(410, 38)
        Me.Unit_txt.TabIndex = 457
        Me.Unit_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(20, 128)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(118, 28)
        Me.Label4.TabIndex = 458
        Me.Label4.Text = "تكلفة الوحدة"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label4.Visible = False
        '
        'IM_Name_txt
        '
        Me.IM_Name_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IM_Name_txt.Enabled = False
        Me.IM_Name_txt.Font = New System.Drawing.Font("JF Flat", 13.0!)
        Me.IM_Name_txt.Location = New System.Drawing.Point(79, 41)
        Me.IM_Name_txt.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.IM_Name_txt.Name = "IM_Name_txt"
        Me.IM_Name_txt.Size = New System.Drawing.Size(410, 38)
        Me.IM_Name_txt.TabIndex = 462
        Me.IM_Name_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 13.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(172, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(86, 31)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "الصلاحية"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Valid_Date
        '
        Me.Valid_Date.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Valid_Date.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Valid_Date.CustomFormat = "dd-MM-yyyy"
        Me.Valid_Date.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Valid_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Valid_Date.Location = New System.Drawing.Point(4, 5)
        Me.Valid_Date.Name = "Valid_Date"
        Me.Valid_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Valid_Date.RightToLeftLayout = True
        Me.Valid_Date.Size = New System.Drawing.Size(166, 32)
        Me.Valid_Date.TabIndex = 667
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(2, 82)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 28)
        Me.Label2.TabIndex = 668
        Me.Label2.Text = "الوحدة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(4, 45)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 28)
        Me.Label3.TabIndex = 669
        Me.Label3.Text = "الصنف"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(5, 5)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 28)
        Me.Label5.TabIndex = 671
        Me.Label5.Text = "المخزن"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ST_Name_txt
        '
        Me.ST_Name_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ST_Name_txt.Enabled = False
        Me.ST_Name_txt.Font = New System.Drawing.Font("JF Flat", 13.0!)
        Me.ST_Name_txt.Location = New System.Drawing.Point(79, 1)
        Me.ST_Name_txt.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.ST_Name_txt.Name = "ST_Name_txt"
        Me.ST_Name_txt.Size = New System.Drawing.Size(410, 38)
        Me.ST_Name_txt.TabIndex = 670
        Me.ST_Name_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Valid_Panel
        '
        Me.Valid_Panel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Valid_Panel.Controls.Add(Me.Valid_Date)
        Me.Valid_Panel.Controls.Add(Me.Label1)
        Me.Valid_Panel.Location = New System.Drawing.Point(4, 254)
        Me.Valid_Panel.Name = "Valid_Panel"
        Me.Valid_Panel.Size = New System.Drawing.Size(307, 43)
        Me.Valid_Panel.TabIndex = 672
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 13.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(4, 358)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(112, 48)
        Me.ExitFormButton.TabIndex = 675
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'ConfermButton
        '
        Me.ConfermButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfermButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ConfermButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ConfermButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ConfermButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ConfermButton.Font = New System.Drawing.Font("JF Flat", 13.0!, System.Drawing.FontStyle.Bold)
        Me.ConfermButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ConfermButton.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.ConfermButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ConfermButton.Location = New System.Drawing.Point(329, 358)
        Me.ConfermButton.Margin = New System.Windows.Forms.Padding(5, 8, 5, 8)
        Me.ConfermButton.Name = "ConfermButton"
        Me.ConfermButton.Size = New System.Drawing.Size(162, 48)
        Me.ConfermButton.TabIndex = 443
        Me.ConfermButton.Text = "تطبيــق F12"
        Me.ConfermButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConfermButton.UseVisualStyleBackColor = False
        '
        'QTY_txt
        '
        Me.QTY_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QTY_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.QTY_txt.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QTY_txt.ForeColor = System.Drawing.Color.Black
        Me.QTY_txt.Location = New System.Drawing.Point(142, 163)
        Me.QTY_txt.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.QTY_txt.Name = "QTY_txt"
        Me.QTY_txt.ReadOnly = True
        Me.QTY_txt.Size = New System.Drawing.Size(165, 39)
        Me.QTY_txt.TabIndex = 676
        Me.QTY_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'New_Qty_txt
        '
        Me.New_Qty_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.New_Qty_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.New_Qty_txt.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.New_Qty_txt.ForeColor = System.Drawing.Color.Black
        Me.New_Qty_txt.Location = New System.Drawing.Point(142, 205)
        Me.New_Qty_txt.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.New_Qty_txt.Name = "New_Qty_txt"
        Me.New_Qty_txt.Size = New System.Drawing.Size(165, 39)
        Me.New_Qty_txt.TabIndex = 678
        Me.New_Qty_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(9, 211)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(129, 28)
        Me.Label6.TabIndex = 677
        Me.Label6.Text = "الكمية الجديده"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Qty_Deference_Txt
        '
        Me.Qty_Deference_Txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Qty_Deference_Txt.BackColor = System.Drawing.SystemColors.Menu
        Me.Qty_Deference_Txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Qty_Deference_Txt.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Qty_Deference_Txt.ForeColor = System.Drawing.Color.Black
        Me.Qty_Deference_Txt.Location = New System.Drawing.Point(311, 205)
        Me.Qty_Deference_Txt.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Qty_Deference_Txt.Name = "Qty_Deference_Txt"
        Me.Qty_Deference_Txt.ReadOnly = True
        Me.Qty_Deference_Txt.Size = New System.Drawing.Size(125, 39)
        Me.Qty_Deference_Txt.TabIndex = 680
        Me.Qty_Deference_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Tag_txt
        '
        Me.Tag_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tag_txt.BackColor = System.Drawing.SystemColors.Menu
        Me.Tag_txt.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.Tag_txt.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tag_txt.ForeColor = System.Drawing.Color.Black
        Me.Tag_txt.Location = New System.Drawing.Point(438, 205)
        Me.Tag_txt.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Tag_txt.Name = "Tag_txt"
        Me.Tag_txt.ReadOnly = True
        Me.Tag_txt.Size = New System.Drawing.Size(44, 39)
        Me.Tag_txt.TabIndex = 681
        Me.Tag_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IM_Update_Qty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 33.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(491, 406)
        Me.ControlBox = False
        Me.Controls.Add(Me.Tag_txt)
        Me.Controls.Add(Me.Qty_Deference_Txt)
        Me.Controls.Add(Me.New_Qty_txt)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.QTY_txt)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Valid_Panel)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ST_Name_txt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.IM_Name_txt)
        Me.Controls.Add(Me.Cost_txt)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Unit_txt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ConfermButton)
        Me.Font = New System.Drawing.Font("JF Flat", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(5, 8, 5, 8)
        Me.Name = "IM_Update_Qty"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تعديل بيانات صنف"
        Me.Valid_Panel.ResumeLayout(False)
        Me.Valid_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ConfermButton As System.Windows.Forms.Button
    Friend WithEvents Cost_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Unit_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents IM_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Valid_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ST_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Valid_Panel As System.Windows.Forms.Panel
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents NULLContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents QTY_txt As System.Windows.Forms.TextBox
    Friend WithEvents New_Qty_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Qty_Deference_Txt As System.Windows.Forms.TextBox
    Friend WithEvents Tag_txt As System.Windows.Forms.TextBox
End Class
