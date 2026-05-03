<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ADD_Pch_Exp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ADD_Pch_Exp))
        Me.CD_Money_txt = New System.Windows.Forms.TextBox()
        Me.NoneContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.OrderDeliver_btn = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Notes_cm = New System.Windows.Forms.ComboBox()
        Me.isWithBill_CB = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CD_Money_txt
        '
        Me.CD_Money_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CD_Money_txt.ContextMenuStrip = Me.NoneContextMenuStrip1
        Me.CD_Money_txt.Font = New System.Drawing.Font("Stencil", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CD_Money_txt.Location = New System.Drawing.Point(89, 3)
        Me.CD_Money_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CD_Money_txt.Name = "CD_Money_txt"
        Me.CD_Money_txt.Size = New System.Drawing.Size(149, 30)
        Me.CD_Money_txt.TabIndex = 386
        Me.CD_Money_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NoneContextMenuStrip1
        '
        Me.NoneContextMenuStrip1.Name = "GVContextMenuStrip"
        Me.NoneContextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NoneContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 26)
        Me.Label3.TabIndex = 387
        Me.Label3.Text = "القيمة"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(17, 42)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 26)
        Me.Label9.TabIndex = 391
        Me.Label9.Text = "ذلك عن"
        '
        'OrderDeliver_btn
        '
        Me.OrderDeliver_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.OrderDeliver_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.OrderDeliver_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.OrderDeliver_btn.Enabled = False
        Me.OrderDeliver_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.OrderDeliver_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.OrderDeliver_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.OrderDeliver_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OrderDeliver_btn.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OrderDeliver_btn.ForeColor = System.Drawing.Color.Black
        Me.OrderDeliver_btn.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.OrderDeliver_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OrderDeliver_btn.Location = New System.Drawing.Point(2, 212)
        Me.OrderDeliver_btn.Name = "OrderDeliver_btn"
        Me.OrderDeliver_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.OrderDeliver_btn.Size = New System.Drawing.Size(348, 45)
        Me.OrderDeliver_btn.TabIndex = 577
        Me.OrderDeliver_btn.Text = "حفـــظ"
        Me.OrderDeliver_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OrderDeliver_btn.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.ContextMenuStrip = Me.NoneContextMenuStrip1
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(240, 3)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(110, 29)
        Me.TextBox1.TabIndex = 578
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Notes_cm
        '
        Me.Notes_cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Notes_cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Notes_cm.BackColor = System.Drawing.SystemColors.Info
        Me.Notes_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Notes_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Notes_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Notes_cm.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Notes_cm.FormattingEnabled = True
        Me.Notes_cm.IntegralHeight = False
        Me.Notes_cm.Items.AddRange(New Object() {"خدمة", "بضاعة", "تصنيع"})
        Me.Notes_cm.Location = New System.Drawing.Point(89, 37)
        Me.Notes_cm.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Notes_cm.Name = "Notes_cm"
        Me.Notes_cm.Size = New System.Drawing.Size(252, 34)
        Me.Notes_cm.TabIndex = 579
        '
        'isWithBill_CB
        '
        Me.isWithBill_CB.AutoSize = True
        Me.isWithBill_CB.BackColor = System.Drawing.Color.Transparent
        Me.isWithBill_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.isWithBill_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.isWithBill_CB.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isWithBill_CB.ForeColor = System.Drawing.Color.Black
        Me.isWithBill_CB.Location = New System.Drawing.Point(89, 78)
        Me.isWithBill_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.isWithBill_CB.Name = "isWithBill_CB"
        Me.isWithBill_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.isWithBill_CB.Size = New System.Drawing.Size(165, 30)
        Me.isWithBill_CB.TabIndex = 580
        Me.isWithBill_CB.Text = "ضمن فاتورة المورد"
        Me.isWithBill_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.isWithBill_CB.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(2, 107)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(348, 45)
        Me.Button1.TabIndex = 581
        Me.Button1.Text = "إضافة"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = False
        '
        'ADD_Pch_Exp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(353, 258)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.isWithBill_CB)
        Me.Controls.Add(Me.Notes_cm)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.OrderDeliver_btn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CD_Money_txt)
        Me.Controls.Add(Me.Label3)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ADD_Pch_Exp"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إضافة مصروفات موزعة"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CD_Money_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents OrderDeliver_btn As System.Windows.Forms.Button
    Friend WithEvents NoneContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Notes_cm As System.Windows.Forms.ComboBox
    Friend WithEvents isWithBill_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As Button
End Class
