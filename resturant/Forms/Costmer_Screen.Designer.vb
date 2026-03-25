<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Costmer_Screen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Costmer_Screen))
        Me.ReadyPanel = New System.Windows.Forms.Panel()
        Me.WaitPanel = New System.Windows.Forms.Panel()
        Me.Bill_Num_txt = New System.Windows.Forms.TextBox()
        Me.Get_Bill_Ready_btn = New System.Windows.Forms.Button()
        Me.Cancel_Bill_btn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.OverTimeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ReadyPanel
        '
        Me.ReadyPanel.AutoScroll = True
        Me.ReadyPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ReadyPanel.Location = New System.Drawing.Point(1, 73)
        Me.ReadyPanel.Name = "ReadyPanel"
        Me.ReadyPanel.Size = New System.Drawing.Size(397, 524)
        Me.ReadyPanel.TabIndex = 0
        '
        'WaitPanel
        '
        Me.WaitPanel.AutoScroll = True
        Me.WaitPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.WaitPanel.Location = New System.Drawing.Point(398, 73)
        Me.WaitPanel.Name = "WaitPanel"
        Me.WaitPanel.Size = New System.Drawing.Size(390, 524)
        Me.WaitPanel.TabIndex = 1
        '
        'Bill_Num_txt
        '
        Me.Bill_Num_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Bill_Num_txt.Location = New System.Drawing.Point(83, 5)
        Me.Bill_Num_txt.Name = "Bill_Num_txt"
        Me.Bill_Num_txt.Size = New System.Drawing.Size(95, 29)
        Me.Bill_Num_txt.TabIndex = 2
        Me.Bill_Num_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Get_Bill_Ready_btn
        '
        Me.Get_Bill_Ready_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Get_Bill_Ready_btn.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Get_Bill_Ready_btn.ForeColor = System.Drawing.Color.DarkGreen
        Me.Get_Bill_Ready_btn.Location = New System.Drawing.Point(179, 2)
        Me.Get_Bill_Ready_btn.Name = "Get_Bill_Ready_btn"
        Me.Get_Bill_Ready_btn.Size = New System.Drawing.Size(80, 35)
        Me.Get_Bill_Ready_btn.TabIndex = 3
        Me.Get_Bill_Ready_btn.Text = "تجهيز"
        Me.Get_Bill_Ready_btn.UseVisualStyleBackColor = True
        '
        'Cancel_Bill_btn
        '
        Me.Cancel_Bill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Bill_btn.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel_Bill_btn.ForeColor = System.Drawing.Color.DarkRed
        Me.Cancel_Bill_btn.Location = New System.Drawing.Point(2, 2)
        Me.Cancel_Bill_btn.Name = "Cancel_Bill_btn"
        Me.Cancel_Bill_btn.Size = New System.Drawing.Size(80, 35)
        Me.Cancel_Bill_btn.TabIndex = 4
        Me.Cancel_Bill_btn.Text = "إلغاء"
        Me.Cancel_Bill_btn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Navy
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(279, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(235, 37)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "شاشة عرض الفواتير"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 5000
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Location = New System.Drawing.Point(394, 48)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(5, 550)
        Me.Panel1.TabIndex = 6
        '
        'Back_Btn
        '
        Me.Back_Btn.BackColor = System.Drawing.Color.White
        Me.Back_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.Users_Exit_icon
        Me.Back_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.Location = New System.Drawing.Point(719, 2)
        Me.Back_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.Size = New System.Drawing.Size(68, 40)
        Me.Back_Btn.TabIndex = 559
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'OverTimeTimer
        '
        Me.OverTimeTimer.Enabled = True
        Me.OverTimeTimer.Interval = 10000
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.DarkGreen
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label2.Location = New System.Drawing.Point(1, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(395, 33)
        Me.Label2.TabIndex = 560
        Me.Label2.Text = "جاهــــــزة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.DarkRed
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label3.Location = New System.Drawing.Point(398, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(390, 33)
        Me.Label3.TabIndex = 561
        Me.Label3.Text = "غير جاهزة"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Costmer_Screen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(789, 600)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Back_Btn)
        Me.Controls.Add(Me.Get_Bill_Ready_btn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Cancel_Bill_btn)
        Me.Controls.Add(Me.Bill_Num_txt)
        Me.Controls.Add(Me.WaitPanel)
        Me.Controls.Add(Me.ReadyPanel)
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Name = "Costmer_Screen"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Costmer_Screen"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ReadyPanel As System.Windows.Forms.Panel
    Friend WithEvents WaitPanel As System.Windows.Forms.Panel
    Friend WithEvents Bill_Num_txt As System.Windows.Forms.TextBox
    Friend WithEvents Get_Bill_Ready_btn As System.Windows.Forms.Button
    Friend WithEvents Cancel_Bill_btn As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents OverTimeTimer As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
