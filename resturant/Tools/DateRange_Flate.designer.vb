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
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MonthCmbo
        '
        Me.MonthCmbo.BackColor = System.Drawing.SystemColors.Menu
        Me.MonthCmbo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MonthCmbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MonthCmbo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.MonthCmbo.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.MonthCmbo.FormattingEnabled = True
        Me.MonthCmbo.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})
        Me.MonthCmbo.Location = New System.Drawing.Point(404, 3)
        Me.MonthCmbo.Name = "MonthCmbo"
        Me.MonthCmbo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MonthCmbo.Size = New System.Drawing.Size(58, 25)
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
        Me.Down_Btn.Size = New System.Drawing.Size(31, 27)
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
        Me.D_To.Location = New System.Drawing.Point(84, 4)
        Me.D_To.Margin = New System.Windows.Forms.Padding(4)
        Me.D_To.Name = "D_To"
        Me.D_To.Size = New System.Drawing.Size(110, 27)
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
        Me.Up_Btn.Size = New System.Drawing.Size(33, 27)
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
        Me.D_From.Size = New System.Drawing.Size(119, 27)
        Me.D_From.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label1.Location = New System.Drawing.Point(468, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 17)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "الشهر"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label2.Location = New System.Drawing.Point(371, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 17)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "من"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label3.Location = New System.Drawing.Point(201, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 17)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "إلى"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 8
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.61111!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.38889!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Up_Btn, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.D_To, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.MonthCmbo, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.D_From, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Down_Btn, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(528, 35)
        Me.TableLayoutPanel1.TabIndex = 14
        '
        'DateRange_Flate
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "DateRange_Flate"
        Me.Size = New System.Drawing.Size(531, 41)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MonthCmbo As System.Windows.Forms.ComboBox
    Friend WithEvents Down_Btn As System.Windows.Forms.Button
    Friend WithEvents D_To As System.Windows.Forms.DateTimePicker
    Friend WithEvents Up_Btn As System.Windows.Forms.Button
    Friend WithEvents D_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel

End Class
