<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Add_Unit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Add_Unit))
        Me.InsertU_btn = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Price_txt = New System.Windows.Forms.TextBox()
        Me.IM_Unit_cm = New System.Windows.Forms.ComboBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Random_Barcode_btn = New System.Windows.Forms.Button()
        Me.BarCode_txt = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Unit_cargo_txt = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'InsertU_btn
        '
        Me.InsertU_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InsertU_btn.BackColor = System.Drawing.Color.White
        Me.InsertU_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.InsertU_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InsertU_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.InsertU_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InsertU_btn.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.InsertU_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InsertU_btn.Location = New System.Drawing.Point(210, 214)
        Me.InsertU_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.InsertU_btn.Name = "InsertU_btn"
        Me.InsertU_btn.Size = New System.Drawing.Size(123, 37)
        Me.InsertU_btn.TabIndex = 457
        Me.InsertU_btn.Text = "إضافــة"
        Me.InsertU_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InsertU_btn.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 9)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 21)
        Me.Label5.TabIndex = 456
        Me.Label5.Text = "الوحدة"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 104)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 21)
        Me.Label3.TabIndex = 455
        Me.Label3.Text = "سعر البيع"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Price_txt
        '
        Me.Price_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Price_txt.Font = New System.Drawing.Font("Stencil", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Price_txt.Location = New System.Drawing.Point(78, 99)
        Me.Price_txt.Name = "Price_txt"
        Me.Price_txt.Size = New System.Drawing.Size(257, 30)
        Me.Price_txt.TabIndex = 454
        Me.Price_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IM_Unit_cm
        '
        Me.IM_Unit_cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IM_Unit_cm.BackColor = System.Drawing.SystemColors.Info
        Me.IM_Unit_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Unit_cm.DropDownHeight = 300
        Me.IM_Unit_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Unit_cm.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Unit_cm.FormattingEnabled = True
        Me.IM_Unit_cm.IntegralHeight = False
        Me.IM_Unit_cm.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.IM_Unit_cm.Location = New System.Drawing.Point(78, 5)
        Me.IM_Unit_cm.Name = "IM_Unit_cm"
        Me.IM_Unit_cm.Size = New System.Drawing.Size(257, 29)
        Me.IM_Unit_cm.TabIndex = 453
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
        Me.ExitFormButton.Location = New System.Drawing.Point(3, 214)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(123, 37)
        Me.ExitFormButton.TabIndex = 672
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "عــودة"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Random_Barcode_btn
        '
        Me.Random_Barcode_btn.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Random_Barcode_btn.BackColor = System.Drawing.Color.White
        Me.Random_Barcode_btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_refresh_1608809
        Me.Random_Barcode_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Random_Barcode_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Random_Barcode_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Random_Barcode_btn.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Random_Barcode_btn.Location = New System.Drawing.Point(296, 69)
        Me.Random_Barcode_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Random_Barcode_btn.Name = "Random_Barcode_btn"
        Me.Random_Barcode_btn.Size = New System.Drawing.Size(38, 29)
        Me.Random_Barcode_btn.TabIndex = 675
        Me.Random_Barcode_btn.UseVisualStyleBackColor = False
        '
        'BarCode_txt
        '
        Me.BarCode_txt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BarCode_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarCode_txt.Location = New System.Drawing.Point(77, 69)
        Me.BarCode_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BarCode_txt.Name = "BarCode_txt"
        Me.BarCode_txt.Size = New System.Drawing.Size(218, 29)
        Me.BarCode_txt.TabIndex = 673
        Me.BarCode_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(24, 74)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 21)
        Me.Label6.TabIndex = 674
        Me.Label6.Text = "باركود"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(8, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 21)
        Me.Label2.TabIndex = 704
        Me.Label2.Text = "الحمولة :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Unit_cargo_txt
        '
        Me.Unit_cargo_txt.BackColor = System.Drawing.SystemColors.Info
        Me.Unit_cargo_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Unit_cargo_txt.Font = New System.Drawing.Font("Times New Roman", 15.75!)
        Me.Unit_cargo_txt.ForeColor = System.Drawing.Color.Black
        Me.Unit_cargo_txt.Location = New System.Drawing.Point(77, 36)
        Me.Unit_cargo_txt.MaxLength = 250
        Me.Unit_cargo_txt.Name = "Unit_cargo_txt"
        Me.Unit_cargo_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Unit_cargo_txt.Size = New System.Drawing.Size(66, 32)
        Me.Unit_cargo_txt.TabIndex = 703
        Me.Unit_cargo_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Add_Unit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(335, 252)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Unit_cargo_txt)
        Me.Controls.Add(Me.Random_Barcode_btn)
        Me.Controls.Add(Me.BarCode_txt)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.InsertU_btn)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Price_txt)
        Me.Controls.Add(Me.IM_Unit_cm)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Add_Unit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إضافة وحدة للصنف +"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents InsertU_btn As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Price_txt As System.Windows.Forms.TextBox
    Friend WithEvents IM_Unit_cm As System.Windows.Forms.ComboBox
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Random_Barcode_btn As System.Windows.Forms.Button
    Friend WithEvents BarCode_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Unit_cargo_txt As System.Windows.Forms.TextBox
End Class
