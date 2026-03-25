<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Update_IM_Unit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Update_IM_Unit))
        Me.PriceTextBox = New System.Windows.Forms.TextBox()
        Me.NULLContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.U_Name_txt = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.IM_LB = New System.Windows.Forms.Label()
        Me.ConfermButton = New System.Windows.Forms.Button()
        Me.isInvoice_CB = New System.Windows.Forms.CheckBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Min_SP_Panel = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Min_SP_2_txt = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Min_SP_txt = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BarCode_txt = New System.Windows.Forms.TextBox()
        Me.Min_SP_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'PriceTextBox
        '
        Me.PriceTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.PriceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PriceTextBox.ContextMenuStrip = Me.NULLContextMenuStrip
        Me.PriceTextBox.Font = New System.Drawing.Font("Stencil", 15.0!)
        Me.PriceTextBox.ForeColor = System.Drawing.Color.DarkGreen
        Me.PriceTextBox.Location = New System.Drawing.Point(491, 78)
        Me.PriceTextBox.MaxLength = 250
        Me.PriceTextBox.Name = "PriceTextBox"
        Me.PriceTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PriceTextBox.Size = New System.Drawing.Size(93, 31)
        Me.PriceTextBox.TabIndex = 617
        Me.PriceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NULLContextMenuStrip
        '
        Me.NULLContextMenuStrip.Name = "NULLContextMenuStrip"
        Me.NULLContextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NULLContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'U_Name_txt
        '
        Me.U_Name_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.U_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.U_Name_txt.Enabled = False
        Me.U_Name_txt.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.U_Name_txt.ForeColor = System.Drawing.Color.Black
        Me.U_Name_txt.Location = New System.Drawing.Point(589, 77)
        Me.U_Name_txt.MaxLength = 250
        Me.U_Name_txt.Name = "U_Name_txt"
        Me.U_Name_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.U_Name_txt.Size = New System.Drawing.Size(104, 33)
        Me.U_Name_txt.TabIndex = 618
        Me.U_Name_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(507, 51)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 24)
        Me.Label7.TabIndex = 620
        Me.Label7.Text = "سعر البيع"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label15.Location = New System.Drawing.Point(633, 50)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(57, 24)
        Me.Label15.TabIndex = 624
        Me.Label15.Text = "الوحدة"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'IM_LB
        '
        Me.IM_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_LB.Font = New System.Drawing.Font("JF Flat", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_LB.ForeColor = System.Drawing.Color.DarkRed
        Me.IM_LB.Location = New System.Drawing.Point(1, 0)
        Me.IM_LB.Name = "IM_LB"
        Me.IM_LB.Size = New System.Drawing.Size(692, 42)
        Me.IM_LB.TabIndex = 629
        Me.IM_LB.Text = "اسم الصنف"
        Me.IM_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ConfermButton
        '
        Me.ConfermButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ConfermButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ConfermButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ConfermButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ConfermButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ConfermButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ConfermButton.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.ConfermButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ConfermButton.Location = New System.Drawing.Point(1, 146)
        Me.ConfermButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ConfermButton.Name = "ConfermButton"
        Me.ConfermButton.Size = New System.Drawing.Size(214, 41)
        Me.ConfermButton.TabIndex = 630
        Me.ConfermButton.Text = "تطبيــق F12"
        Me.ConfermButton.UseVisualStyleBackColor = False
        '
        'isInvoice_CB
        '
        Me.isInvoice_CB.AutoSize = True
        Me.isInvoice_CB.BackColor = System.Drawing.Color.Transparent
        Me.isInvoice_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.isInvoice_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.isInvoice_CB.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isInvoice_CB.ForeColor = System.Drawing.Color.Black
        Me.isInvoice_CB.Location = New System.Drawing.Point(4, 75)
        Me.isInvoice_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.isInvoice_CB.Name = "isInvoice_CB"
        Me.isInvoice_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.isInvoice_CB.Size = New System.Drawing.Size(85, 32)
        Me.isInvoice_CB.TabIndex = 670
        Me.isInvoice_CB.Text = "أساسي"
        Me.isInvoice_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.isInvoice_CB.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(578, 146)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(115, 41)
        Me.ExitFormButton.TabIndex = 673
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Min_SP_Panel
        '
        Me.Min_SP_Panel.Controls.Add(Me.Label26)
        Me.Min_SP_Panel.Controls.Add(Me.Min_SP_2_txt)
        Me.Min_SP_Panel.Controls.Add(Me.Label23)
        Me.Min_SP_Panel.Controls.Add(Me.Min_SP_txt)
        Me.Min_SP_Panel.Location = New System.Drawing.Point(93, 49)
        Me.Min_SP_Panel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Min_SP_Panel.Name = "Min_SP_Panel"
        Me.Min_SP_Panel.Size = New System.Drawing.Size(201, 61)
        Me.Min_SP_Panel.TabIndex = 674
        '
        'Label26
        '
        Me.Label26.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label26.Location = New System.Drawing.Point(4, 2)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(96, 24)
        Me.Label26.TabIndex = 466
        Me.Label26.Text = "جملة الجملة"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Min_SP_2_txt
        '
        Me.Min_SP_2_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Min_SP_2_txt.Font = New System.Drawing.Font("Stencil", 15.0!)
        Me.Min_SP_2_txt.Location = New System.Drawing.Point(10, 28)
        Me.Min_SP_2_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Min_SP_2_txt.Name = "Min_SP_2_txt"
        Me.Min_SP_2_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Min_SP_2_txt.Size = New System.Drawing.Size(84, 31)
        Me.Min_SP_2_txt.TabIndex = 465
        Me.Min_SP_2_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label23
        '
        Me.Label23.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label23.Location = New System.Drawing.Point(105, 2)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(88, 24)
        Me.Label23.TabIndex = 464
        Me.Label23.Text = "سعر الجملة"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Min_SP_txt
        '
        Me.Min_SP_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Min_SP_txt.Font = New System.Drawing.Font("Stencil", 15.0!)
        Me.Min_SP_txt.Location = New System.Drawing.Point(112, 28)
        Me.Min_SP_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Min_SP_txt.Name = "Min_SP_txt"
        Me.Min_SP_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Min_SP_txt.Size = New System.Drawing.Size(84, 31)
        Me.Min_SP_txt.TabIndex = 463
        Me.Min_SP_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(427, 50)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 24)
        Me.Label6.TabIndex = 676
        Me.Label6.Text = "باركود"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BarCode_txt
        '
        Me.BarCode_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 13.5!, System.Drawing.FontStyle.Bold)
        Me.BarCode_txt.Location = New System.Drawing.Point(301, 77)
        Me.BarCode_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BarCode_txt.Name = "BarCode_txt"
        Me.BarCode_txt.Size = New System.Drawing.Size(183, 31)
        Me.BarCode_txt.TabIndex = 675
        Me.BarCode_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Update_IM_Unit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(694, 188)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BarCode_txt)
        Me.Controls.Add(Me.Min_SP_Panel)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.isInvoice_CB)
        Me.Controls.Add(Me.ConfermButton)
        Me.Controls.Add(Me.IM_LB)
        Me.Controls.Add(Me.PriceTextBox)
        Me.Controls.Add(Me.U_Name_txt)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label15)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Update_IM_Unit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تعديل وحدة"
        Me.Min_SP_Panel.ResumeLayout(False)
        Me.Min_SP_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PriceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents U_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents IM_LB As System.Windows.Forms.Label
    Friend WithEvents ConfermButton As System.Windows.Forms.Button
    Friend WithEvents NULLContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents isInvoice_CB As System.Windows.Forms.CheckBox
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Min_SP_Panel As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Min_SP_2_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Min_SP_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents BarCode_txt As TextBox
End Class
