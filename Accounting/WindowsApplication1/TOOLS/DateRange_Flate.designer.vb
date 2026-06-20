<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DateRange_Flate
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DateRange_Flate))
        Me.MonthCmbo = New System.Windows.Forms.ComboBox()
        Me.Down_Btn = New System.Windows.Forms.Button()
        Me.D_To = New System.Windows.Forms.DateTimePicker()
        Me.Up_Btn = New System.Windows.Forms.Button()
        Me.D_From = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ALLTime_CheckBox = New System.Windows.Forms.CheckBox()
        Me.FYear_Txt = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MonthCmbo
        '
        Me.MonthCmbo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.MonthCmbo.BackColor = System.Drawing.SystemColors.Menu
        Me.MonthCmbo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MonthCmbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MonthCmbo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.MonthCmbo.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.MonthCmbo.FormattingEnabled = True
        Me.MonthCmbo.Location = New System.Drawing.Point(292, 5)
        Me.MonthCmbo.Name = "MonthCmbo"
        Me.MonthCmbo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MonthCmbo.Size = New System.Drawing.Size(69, 26)
        Me.MonthCmbo.TabIndex = 10
        '
        'Down_Btn
        '
        Me.Down_Btn.BackgroundImage = CType(resources.GetObject("Down_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Down_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Down_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Down_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Down_Btn.Location = New System.Drawing.Point(4, 4)
        Me.Down_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Down_Btn.Name = "Down_Btn"
        Me.Down_Btn.Size = New System.Drawing.Size(27, 27)
        Me.Down_Btn.TabIndex = 9
        Me.Down_Btn.UseVisualStyleBackColor = True
        '
        'D_To
        '
        Me.D_To.CalendarFont = New System.Drawing.Font("Tahoma", 12.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.D_To.Cursor = System.Windows.Forms.Cursors.Hand
        Me.D_To.CustomFormat = "yyyy-MM-dd"
        Me.D_To.Font = New System.Drawing.Font("Tahoma", 12.25!)
        Me.D_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.D_To.Location = New System.Drawing.Point(79, 4)
        Me.D_To.Margin = New System.Windows.Forms.Padding(4)
        Me.D_To.Name = "D_To"
        Me.D_To.Size = New System.Drawing.Size(121, 27)
        Me.D_To.TabIndex = 8
        '
        'Up_Btn
        '
        Me.Up_Btn.BackgroundImage = CType(resources.GetObject("Up_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Up_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Up_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Up_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Up_Btn.Location = New System.Drawing.Point(43, 4)
        Me.Up_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Up_Btn.Name = "Up_Btn"
        Me.Up_Btn.Size = New System.Drawing.Size(27, 27)
        Me.Up_Btn.TabIndex = 7
        Me.Up_Btn.UseVisualStyleBackColor = True
        '
        'D_From
        '
        Me.D_From.Cursor = System.Windows.Forms.Cursors.Hand
        Me.D_From.CustomFormat = "yyyy-MM-dd"
        Me.D_From.Font = New System.Drawing.Font("Tahoma", 12.25!)
        Me.D_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.D_From.Location = New System.Drawing.Point(245, 4)
        Me.D_From.Margin = New System.Windows.Forms.Padding(4)
        Me.D_From.Name = "D_From"
        Me.D_From.Size = New System.Drawing.Size(121, 27)
        Me.D_From.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label1.Location = New System.Drawing.Point(365, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "الشهر"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label2.Location = New System.Drawing.Point(375, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "من"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label3.Location = New System.Drawing.Point(208, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "إلى"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.51515!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.48485!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Up_Btn, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.D_To, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.D_From, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Down_Btn, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(4, 37)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(415, 35)
        Me.TableLayoutPanel1.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ALLTime_CheckBox)
        Me.Panel1.Controls.Add(Me.FYear_Txt)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.MonthCmbo)
        Me.Panel1.Location = New System.Drawing.Point(3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(416, 33)
        Me.Panel1.TabIndex = 15
        '
        'ALLTime_CheckBox
        '
        Me.ALLTime_CheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ALLTime_CheckBox.AutoSize = True
        Me.ALLTime_CheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ALLTime_CheckBox.Font = New System.Drawing.Font("Tahoma", 7.75!)
        Me.ALLTime_CheckBox.Location = New System.Drawing.Point(178, 8)
        Me.ALLTime_CheckBox.Name = "ALLTime_CheckBox"
        Me.ALLTime_CheckBox.Size = New System.Drawing.Size(75, 17)
        Me.ALLTime_CheckBox.TabIndex = 96
        Me.ALLTime_CheckBox.Text = "كل الفترات"
        Me.ALLTime_CheckBox.UseVisualStyleBackColor = True
        '
        'FYear_Txt
        '
        Me.FYear_Txt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.FYear_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FYear_Txt.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FYear_Txt.ForeColor = System.Drawing.Color.Black
        Me.FYear_Txt.Location = New System.Drawing.Point(4, 4)
        Me.FYear_Txt.Margin = New System.Windows.Forms.Padding(4)
        Me.FYear_Txt.Name = "FYear_Txt"
        Me.FYear_Txt.ReadOnly = True
        Me.FYear_Txt.Size = New System.Drawing.Size(56, 24)
        Me.FYear_Txt.TabIndex = 95
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label4.Location = New System.Drawing.Point(64, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "السنة المالية"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DateRange_Flate
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.Name = "DateRange_Flate"
        Me.Size = New System.Drawing.Size(422, 75)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MonthCmbo As System.Windows.Forms.ComboBox
    Friend WithEvents Down_Btn As System.Windows.Forms.Button
    Friend WithEvents D_To As System.Windows.Forms.DateTimePicker
    Friend WithEvents Up_Btn As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents FYear_Txt As TextBox
    Friend WithEvents ALLTime_CheckBox As CheckBox
    Friend WithEvents D_From As DateTimePicker
End Class
