<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Show_IM_Details
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Show_IM_Details))
        Me.Yes_Btn = New System.Windows.Forms.Button()
        Me.Notes_txt = New System.Windows.Forms.TextBox()
        Me.IM_Photo = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.IM_Num_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IM_SH_txt = New System.Windows.Forms.TextBox()
        Me.GM_Name_txt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.IM_Photo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Yes_Btn
        '
        Me.Yes_Btn.BackColor = System.Drawing.SystemColors.Control
        Me.Yes_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Yes_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Yes_Btn.Font = New System.Drawing.Font("JF Flat", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Yes_Btn.ForeColor = System.Drawing.Color.DarkRed
        Me.Yes_Btn.Location = New System.Drawing.Point(218, 485)
        Me.Yes_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Yes_Btn.Name = "Yes_Btn"
        Me.Yes_Btn.Size = New System.Drawing.Size(136, 44)
        Me.Yes_Btn.TabIndex = 452
        Me.Yes_Btn.Tag = ""
        Me.Yes_Btn.Text = "موافق"
        Me.Yes_Btn.UseVisualStyleBackColor = False
        '
        'Notes_txt
        '
        Me.Notes_txt.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Notes_txt.Location = New System.Drawing.Point(2, 424)
        Me.Notes_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Notes_txt.Multiline = True
        Me.Notes_txt.Name = "Notes_txt"
        Me.Notes_txt.ReadOnly = True
        Me.Notes_txt.Size = New System.Drawing.Size(470, 58)
        Me.Notes_txt.TabIndex = 616
        '
        'IM_Photo
        '
        Me.IM_Photo.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.IM_Photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Photo.Location = New System.Drawing.Point(2, 109)
        Me.IM_Photo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.IM_Photo.Name = "IM_Photo"
        Me.IM_Photo.Size = New System.Drawing.Size(470, 312)
        Me.IM_Photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.IM_Photo.TabIndex = 615
        Me.IM_Photo.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(475, 42)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 28)
        Me.Label2.TabIndex = 610
        Me.Label2.Text = "التصنيف"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(475, 77)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 28)
        Me.Label9.TabIndex = 614
        Me.Label9.Text = "رقم الصنف"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IM_Num_txt
        '
        Me.IM_Num_txt.Font = New System.Drawing.Font("Times New Roman", 15.25!, System.Drawing.FontStyle.Bold)
        Me.IM_Num_txt.Location = New System.Drawing.Point(2, 77)
        Me.IM_Num_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.IM_Num_txt.Name = "IM_Num_txt"
        Me.IM_Num_txt.ReadOnly = True
        Me.IM_Num_txt.Size = New System.Drawing.Size(470, 31)
        Me.IM_Num_txt.TabIndex = 613
        Me.IM_Num_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(475, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 28)
        Me.Label1.TabIndex = 609
        Me.Label1.Text = "بيان الصنف"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IM_SH_txt
        '
        Me.IM_SH_txt.BackColor = System.Drawing.SystemColors.Window
        Me.IM_SH_txt.Font = New System.Drawing.Font("JF Flat", 12.0!)
        Me.IM_SH_txt.Location = New System.Drawing.Point(2, 3)
        Me.IM_SH_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.IM_SH_txt.Name = "IM_SH_txt"
        Me.IM_SH_txt.ReadOnly = True
        Me.IM_SH_txt.Size = New System.Drawing.Size(470, 35)
        Me.IM_SH_txt.TabIndex = 611
        '
        'GM_Name_txt
        '
        Me.GM_Name_txt.BackColor = System.Drawing.SystemColors.Window
        Me.GM_Name_txt.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GM_Name_txt.Location = New System.Drawing.Point(2, 40)
        Me.GM_Name_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GM_Name_txt.Name = "GM_Name_txt"
        Me.GM_Name_txt.ReadOnly = True
        Me.GM_Name_txt.Size = New System.Drawing.Size(470, 35)
        Me.GM_Name_txt.TabIndex = 617
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(476, 427)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 28)
        Me.Label3.TabIndex = 618
        Me.Label3.Text = "ملاحظة"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Show_IM_Details
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 37.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(572, 533)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GM_Name_txt)
        Me.Controls.Add(Me.Notes_txt)
        Me.Controls.Add(Me.IM_Photo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.IM_Num_txt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.IM_SH_txt)
        Me.Controls.Add(Me.Yes_Btn)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.MinimizeBox = False
        Me.Name = "Show_IM_Details"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تفاصيل الصنف"
        CType(Me.IM_Photo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Yes_Btn As System.Windows.Forms.Button
    Friend WithEvents Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents IM_Photo As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents IM_Num_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents IM_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents GM_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
