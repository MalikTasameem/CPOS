<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Query_Form
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
        Me.Con_txt = New System.Windows.Forms.TextBox()
        Me.is_CHECKED_LB = New System.Windows.Forms.Label()
        Me.Check_Btn = New DevComponents.DotNetBar.ButtonX()
        Me.save_enc_btn = New DevComponents.DotNetBar.ButtonX()
        Me.Query_txt = New System.Windows.Forms.TextBox()
        Me.ButtonX2 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.SuspendLayout()
        '
        'Con_txt
        '
        Me.Con_txt.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Con_txt.Location = New System.Drawing.Point(5, 2)
        Me.Con_txt.Name = "Con_txt"
        Me.Con_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Con_txt.Size = New System.Drawing.Size(907, 25)
        Me.Con_txt.TabIndex = 714
        Me.Con_txt.Text = "Data Source= localhost ;initial catalog=' ';User Id=' ';Password=' '"
        '
        'is_CHECKED_LB
        '
        Me.is_CHECKED_LB.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.is_CHECKED_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.is_CHECKED_LB.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.is_CHECKED_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.is_CHECKED_LB.Location = New System.Drawing.Point(311, 28)
        Me.is_CHECKED_LB.Name = "is_CHECKED_LB"
        Me.is_CHECKED_LB.Size = New System.Drawing.Size(492, 34)
        Me.is_CHECKED_LB.TabIndex = 716
        Me.is_CHECKED_LB.Text = "متصل"
        Me.is_CHECKED_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.is_CHECKED_LB.Visible = False
        '
        'Check_Btn
        '
        Me.Check_Btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.Check_Btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.Check_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Check_Btn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Check_Btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right
        Me.Check_Btn.Location = New System.Drawing.Point(804, 28)
        Me.Check_Btn.Name = "Check_Btn"
        Me.Check_Btn.Size = New System.Drawing.Size(108, 34)
        Me.Check_Btn.TabIndex = 717
        Me.Check_Btn.Text = "تحقق من الإتصال"
        '
        'save_enc_btn
        '
        Me.save_enc_btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.save_enc_btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.save_enc_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.save_enc_btn.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.save_enc_btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right
        Me.save_enc_btn.Location = New System.Drawing.Point(5, 670)
        Me.save_enc_btn.Name = "save_enc_btn"
        Me.save_enc_btn.Size = New System.Drawing.Size(907, 53)
        Me.save_enc_btn.TabIndex = 721
        Me.save_enc_btn.Text = "تنفيــذ الأمــر"
        '
        'Query_txt
        '
        Me.Query_txt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Query_txt.Location = New System.Drawing.Point(5, 65)
        Me.Query_txt.Multiline = True
        Me.Query_txt.Name = "Query_txt"
        Me.Query_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Query_txt.Size = New System.Drawing.Size(907, 603)
        Me.Query_txt.TabIndex = 722
        '
        'ButtonX2
        '
        Me.ButtonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonX2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonX2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right
        Me.ButtonX2.Location = New System.Drawing.Point(5, 28)
        Me.ButtonX2.Name = "ButtonX2"
        Me.ButtonX2.Size = New System.Drawing.Size(147, 34)
        Me.ButtonX2.TabIndex = 724
        Me.ButtonX2.Text = "attach query"
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonX1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonX1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right
        Me.ButtonX1.Location = New System.Drawing.Point(153, 28)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(157, 34)
        Me.ButtonX1.TabIndex = 723
        Me.ButtonX1.Text = "إتصال تكوين قاعدة بيانات"
        '
        'Query_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 725)
        Me.Controls.Add(Me.ButtonX2)
        Me.Controls.Add(Me.ButtonX1)
        Me.Controls.Add(Me.Query_txt)
        Me.Controls.Add(Me.save_enc_btn)
        Me.Controls.Add(Me.Check_Btn)
        Me.Controls.Add(Me.is_CHECKED_LB)
        Me.Controls.Add(Me.Con_txt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "Query_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "شاشة الأوامر"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Con_txt As System.Windows.Forms.TextBox
    Friend WithEvents is_CHECKED_LB As System.Windows.Forms.Label
    Friend WithEvents Check_Btn As DevComponents.DotNetBar.ButtonX
    Friend WithEvents save_enc_btn As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Query_txt As System.Windows.Forms.TextBox
    Friend WithEvents ButtonX2 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
End Class
