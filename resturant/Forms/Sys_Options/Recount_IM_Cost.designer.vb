<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Recount_IM_Cost
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
        Me.UpdateGBButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Cost_txt = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Pr_DateRange = New resturant.DateRange_Flate()
        Me.Discount_Lb = New System.Windows.Forms.Label()
        Me.Edit_OnCard_CB = New System.Windows.Forms.CheckBox()
        Me.Markter_Panel = New System.Windows.Forms.Panel()
        Me.Markval_CB = New System.Windows.Forms.CheckBox()
        Me.Edit_Markval_OnCard_CB = New System.Windows.Forms.CheckBox()
        Me.Markter_Val_txt = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Cost_CB = New System.Windows.Forms.CheckBox()
        Me.Markter_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'UpdateGBButton
        '
        Me.UpdateGBButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.UpdateGBButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.UpdateGBButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UpdateGBButton.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.UpdateGBButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.UpdateGBButton.Location = New System.Drawing.Point(1, 317)
        Me.UpdateGBButton.Name = "UpdateGBButton"
        Me.UpdateGBButton.Size = New System.Drawing.Size(150, 42)
        Me.UpdateGBButton.TabIndex = 155
        Me.UpdateGBButton.Text = "تدويــر"
        Me.UpdateGBButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.UpdateGBButton.UseVisualStyleBackColor = True
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(408, 321)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(108, 38)
        Me.ExitFormButton.TabIndex = 156
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Cost_txt
        '
        Me.Cost_txt.BackColor = System.Drawing.SystemColors.Window
        Me.Cost_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Cost_txt.Enabled = False
        Me.Cost_txt.Font = New System.Drawing.Font("Stencil", 15.0!)
        Me.Cost_txt.ForeColor = System.Drawing.Color.Black
        Me.Cost_txt.Location = New System.Drawing.Point(3, 131)
        Me.Cost_txt.MaxLength = 200
        Me.Cost_txt.Name = "Cost_txt"
        Me.Cost_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Cost_txt.Size = New System.Drawing.Size(162, 31)
        Me.Cost_txt.TabIndex = 619
        Me.Cost_txt.Text = "0"
        Me.Cost_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(169, 137)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(95, 21)
        Me.Label13.TabIndex = 620
        Me.Label13.Text = "تكلفة القطعة"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pr_DateRange
        '
        Me.Pr_DateRange.AutoSize = True
        Me.Pr_DateRange.BackColor = System.Drawing.Color.Transparent
        Me.Pr_DateRange.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.Pr_DateRange.Location = New System.Drawing.Point(6, 30)
        Me.Pr_DateRange.Margin = New System.Windows.Forms.Padding(4)
        Me.Pr_DateRange.Name = "Pr_DateRange"
        Me.Pr_DateRange.Size = New System.Drawing.Size(531, 41)
        Me.Pr_DateRange.TabIndex = 621
        '
        'Discount_Lb
        '
        Me.Discount_Lb.AutoSize = True
        Me.Discount_Lb.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Discount_Lb.ForeColor = System.Drawing.Color.Black
        Me.Discount_Lb.Location = New System.Drawing.Point(284, 5)
        Me.Discount_Lb.Name = "Discount_Lb"
        Me.Discount_Lb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Discount_Lb.Size = New System.Drawing.Size(236, 21)
        Me.Discount_Lb.TabIndex = 658
        Me.Discount_Lb.Text = "قم تحديد فترة التدوير المراد تعديلها:"
        Me.Discount_Lb.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Edit_OnCard_CB
        '
        Me.Edit_OnCard_CB.AutoSize = True
        Me.Edit_OnCard_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Edit_OnCard_CB.Enabled = False
        Me.Edit_OnCard_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Edit_OnCard_CB.Location = New System.Drawing.Point(145, 165)
        Me.Edit_OnCard_CB.Name = "Edit_OnCard_CB"
        Me.Edit_OnCard_CB.Size = New System.Drawing.Size(227, 24)
        Me.Edit_OnCard_CB.TabIndex = 659
        Me.Edit_OnCard_CB.Text = "تعديل التكلفة في بطاقة الصنف"
        Me.Edit_OnCard_CB.UseVisualStyleBackColor = True
        '
        'Markter_Panel
        '
        Me.Markter_Panel.Controls.Add(Me.Markval_CB)
        Me.Markter_Panel.Controls.Add(Me.Edit_Markval_OnCard_CB)
        Me.Markter_Panel.Controls.Add(Me.Markter_Val_txt)
        Me.Markter_Panel.Controls.Add(Me.Label12)
        Me.Markter_Panel.Location = New System.Drawing.Point(1, 217)
        Me.Markter_Panel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Markter_Panel.Name = "Markter_Panel"
        Me.Markter_Panel.Size = New System.Drawing.Size(372, 94)
        Me.Markter_Panel.TabIndex = 679
        '
        'Markval_CB
        '
        Me.Markval_CB.AutoSize = True
        Me.Markval_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Markval_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Markval_CB.Location = New System.Drawing.Point(241, 4)
        Me.Markval_CB.Name = "Markval_CB"
        Me.Markval_CB.Size = New System.Drawing.Size(127, 19)
        Me.Markval_CB.TabIndex = 681
        Me.Markval_CB.Text = "تدوير عمولة المسوق"
        Me.Markval_CB.UseVisualStyleBackColor = True
        '
        'Edit_Markval_OnCard_CB
        '
        Me.Edit_Markval_OnCard_CB.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Edit_Markval_OnCard_CB.AutoSize = True
        Me.Edit_Markval_OnCard_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Edit_Markval_OnCard_CB.Enabled = False
        Me.Edit_Markval_OnCard_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Edit_Markval_OnCard_CB.Location = New System.Drawing.Point(135, 66)
        Me.Edit_Markval_OnCard_CB.Name = "Edit_Markval_OnCard_CB"
        Me.Edit_Markval_OnCard_CB.Size = New System.Drawing.Size(232, 24)
        Me.Edit_Markval_OnCard_CB.TabIndex = 680
        Me.Edit_Markval_OnCard_CB.Text = "تعديل العمولة في بطاقة الصنف"
        Me.Edit_Markval_OnCard_CB.UseVisualStyleBackColor = True
        '
        'Markter_Val_txt
        '
        Me.Markter_Val_txt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Markter_Val_txt.Enabled = False
        Me.Markter_Val_txt.Font = New System.Drawing.Font("Stencil", 15.0!)
        Me.Markter_Val_txt.Location = New System.Drawing.Point(4, 27)
        Me.Markter_Val_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Markter_Val_txt.Name = "Markter_Val_txt"
        Me.Markter_Val_txt.Size = New System.Drawing.Size(162, 31)
        Me.Markter_Val_txt.TabIndex = 466
        Me.Markter_Val_txt.Text = "0"
        Me.Markter_Val_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(170, 33)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(156, 21)
        Me.Label12.TabIndex = 467
        Me.Label12.Text = "عمولة المسوق للقطعة"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Cost_CB
        '
        Me.Cost_CB.AutoSize = True
        Me.Cost_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cost_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cost_CB.Location = New System.Drawing.Point(284, 139)
        Me.Cost_CB.Name = "Cost_CB"
        Me.Cost_CB.Size = New System.Drawing.Size(88, 19)
        Me.Cost_CB.TabIndex = 680
        Me.Cost_CB.Text = "تدوير التكلفة"
        Me.Cost_CB.UseVisualStyleBackColor = True
        '
        'Recount_IM_Cost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(518, 360)
        Me.ControlBox = False
        Me.Controls.Add(Me.Cost_CB)
        Me.Controls.Add(Me.Markter_Panel)
        Me.Controls.Add(Me.Edit_OnCard_CB)
        Me.Controls.Add(Me.Discount_Lb)
        Me.Controls.Add(Me.Pr_DateRange)
        Me.Controls.Add(Me.Cost_txt)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.UpdateGBButton)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Recount_IM_Cost"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = ""
        Me.Text = "تدوير تكلفة الصنف"
        Me.Markter_Panel.ResumeLayout(False)
        Me.Markter_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UpdateGBButton As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Cost_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Pr_DateRange As DateRange_Flate
    Friend WithEvents Discount_Lb As System.Windows.Forms.Label
    Friend WithEvents Edit_OnCard_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Markter_Panel As System.Windows.Forms.Panel
    Friend WithEvents Edit_Markval_OnCard_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Markter_Val_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Markval_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Cost_CB As System.Windows.Forms.CheckBox
End Class
