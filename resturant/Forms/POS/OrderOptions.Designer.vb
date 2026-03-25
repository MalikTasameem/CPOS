<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderOptions
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
        Me.Delever_Date = New System.Windows.Forms.DateTimePicker()
        Me.NoneDate_CB = New System.Windows.Forms.CheckBox()
        Me.Piedmoney_txt = New System.Windows.Forms.TextBox()
        Me.Rest_txt = New System.Windows.Forms.TextBox()
        Me.Barcode_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Down_Btn = New System.Windows.Forms.Button()
        Me.Up_Btn = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.Barcode_CB = New System.Windows.Forms.CheckBox()
        Me.UsePoint_btn = New System.Windows.Forms.Button()
        Me.Point_LB = New System.Windows.Forms.Label()
        Me.Point_Panel = New System.Windows.Forms.Panel()
        Me.DeliverDate_Panel = New System.Windows.Forms.Panel()
        Me.B1 = New System.Windows.Forms.Button()
        Me.B5 = New System.Windows.Forms.Button()
        Me.ClearNumBtn = New System.Windows.Forms.Button()
        Me.BDot = New System.Windows.Forms.Button()
        Me.B0 = New System.Windows.Forms.Button()
        Me.B8 = New System.Windows.Forms.Button()
        Me.B7 = New System.Windows.Forms.Button()
        Me.B4 = New System.Windows.Forms.Button()
        Me.B6 = New System.Windows.Forms.Button()
        Me.B2 = New System.Windows.Forms.Button()
        Me.B9 = New System.Windows.Forms.Button()
        Me.B3 = New System.Windows.Forms.Button()
        Me.Point_Panel.SuspendLayout()
        Me.DeliverDate_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Delever_Date
        '
        Me.Delever_Date.CalendarFont = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Delever_Date.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Delever_Date.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.Delever_Date.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Delever_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Delever_Date.Location = New System.Drawing.Point(4, 30)
        Me.Delever_Date.Name = "Delever_Date"
        Me.Delever_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Delever_Date.RightToLeftLayout = True
        Me.Delever_Date.Size = New System.Drawing.Size(280, 30)
        Me.Delever_Date.TabIndex = 243
        '
        'NoneDate_CB
        '
        Me.NoneDate_CB.AutoSize = True
        Me.NoneDate_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NoneDate_CB.Location = New System.Drawing.Point(200, 63)
        Me.NoneDate_CB.Name = "NoneDate_CB"
        Me.NoneDate_CB.Size = New System.Drawing.Size(81, 25)
        Me.NoneDate_CB.TabIndex = 244
        Me.NoneDate_CB.Text = "بلا موعد"
        Me.NoneDate_CB.UseVisualStyleBackColor = True
        '
        'Piedmoney_txt
        '
        Me.Piedmoney_txt.Font = New System.Drawing.Font("Stencil", 17.25!)
        Me.Piedmoney_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Piedmoney_txt.Location = New System.Drawing.Point(7, 63)
        Me.Piedmoney_txt.Name = "Piedmoney_txt"
        Me.Piedmoney_txt.Size = New System.Drawing.Size(190, 36)
        Me.Piedmoney_txt.TabIndex = 560
        Me.Piedmoney_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Rest_txt
        '
        Me.Rest_txt.Font = New System.Drawing.Font("Stencil", 17.25!)
        Me.Rest_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Rest_txt.Location = New System.Drawing.Point(7, 105)
        Me.Rest_txt.Name = "Rest_txt"
        Me.Rest_txt.ReadOnly = True
        Me.Rest_txt.Size = New System.Drawing.Size(190, 36)
        Me.Rest_txt.TabIndex = 562
        Me.Rest_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Barcode_txt
        '
        Me.Barcode_txt.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barcode_txt.Location = New System.Drawing.Point(3, 2)
        Me.Barcode_txt.Name = "Barcode_txt"
        Me.Barcode_txt.ReadOnly = True
        Me.Barcode_txt.Size = New System.Drawing.Size(284, 29)
        Me.Barcode_txt.TabIndex = 563
        Me.Barcode_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(291, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 21)
        Me.Label1.TabIndex = 564
        Me.Label1.Text = "باركود الفاتورة"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(200, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 21)
        Me.Label2.TabIndex = 565
        Me.Label2.Text = "المدفوع"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(200, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 21)
        Me.Label3.TabIndex = 566
        Me.Label3.Text = "الباقي"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(187, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 21)
        Me.Label4.TabIndex = 567
        Me.Label4.Text = "موعد التسليم"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Down_Btn
        '
        Me.Down_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Down_Btn.Image = Global.resturant.My.Resources.Resources.if_icon_arrow_down_b_211614
        Me.Down_Btn.Location = New System.Drawing.Point(302, 48)
        Me.Down_Btn.Name = "Down_Btn"
        Me.Down_Btn.Size = New System.Drawing.Size(84, 39)
        Me.Down_Btn.TabIndex = 569
        Me.Down_Btn.UseVisualStyleBackColor = True
        '
        'Up_Btn
        '
        Me.Up_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Up_Btn.Image = Global.resturant.My.Resources.Resources.if_icon_arrow_up_b_211623
        Me.Up_Btn.Location = New System.Drawing.Point(302, 3)
        Me.Up_Btn.Name = "Up_Btn"
        Me.Up_Btn.Size = New System.Drawing.Size(84, 39)
        Me.Up_Btn.TabIndex = 568
        Me.Up_Btn.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.BackColor = System.Drawing.SystemColors.Info
        Me.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveButton.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveButton.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveButton.Location = New System.Drawing.Point(3, 488)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(194, 53)
        Me.SaveButton.TabIndex = 561
        Me.SaveButton.Text = "حفظ وإنهاء "
        Me.SaveButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'Back_Btn
        '
        Me.Back_Btn.BackColor = System.Drawing.Color.White
        Me.Back_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.Users_Exit_icon
        Me.Back_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.Location = New System.Drawing.Point(326, 489)
        Me.Back_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.Size = New System.Drawing.Size(68, 53)
        Me.Back_Btn.TabIndex = 559
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'Barcode_CB
        '
        Me.Barcode_CB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Barcode_CB.AutoSize = True
        Me.Barcode_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Barcode_CB.Location = New System.Drawing.Point(9, 36)
        Me.Barcode_CB.Name = "Barcode_CB"
        Me.Barcode_CB.Size = New System.Drawing.Size(12, 11)
        Me.Barcode_CB.TabIndex = 572
        Me.Barcode_CB.UseVisualStyleBackColor = True
        '
        'UsePoint_btn
        '
        Me.UsePoint_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsePoint_btn.BackColor = System.Drawing.Color.White
        Me.UsePoint_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.UsePoint_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.UsePoint_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.UsePoint_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsePoint_btn.Location = New System.Drawing.Point(5, 33)
        Me.UsePoint_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.UsePoint_btn.Name = "UsePoint_btn"
        Me.UsePoint_btn.Size = New System.Drawing.Size(121, 82)
        Me.UsePoint_btn.TabIndex = 571
        Me.UsePoint_btn.Text = "إستعمال نقاط البطاقة للدفع"
        Me.UsePoint_btn.UseVisualStyleBackColor = False
        '
        'Point_LB
        '
        Me.Point_LB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Point_LB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Point_LB.ForeColor = System.Drawing.Color.DarkGreen
        Me.Point_LB.Location = New System.Drawing.Point(5, 4)
        Me.Point_LB.Name = "Point_LB"
        Me.Point_LB.Size = New System.Drawing.Size(120, 26)
        Me.Point_LB.TabIndex = 573
        Me.Point_LB.Text = "النقاط"
        Me.Point_LB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Point_Panel
        '
        Me.Point_Panel.Controls.Add(Me.Barcode_CB)
        Me.Point_Panel.Controls.Add(Me.UsePoint_btn)
        Me.Point_Panel.Controls.Add(Me.Point_LB)
        Me.Point_Panel.Enabled = False
        Me.Point_Panel.Location = New System.Drawing.Point(266, 37)
        Me.Point_Panel.Name = "Point_Panel"
        Me.Point_Panel.Size = New System.Drawing.Size(128, 119)
        Me.Point_Panel.TabIndex = 574
        Me.Point_Panel.Visible = False
        '
        'DeliverDate_Panel
        '
        Me.DeliverDate_Panel.Controls.Add(Me.Down_Btn)
        Me.DeliverDate_Panel.Controls.Add(Me.Delever_Date)
        Me.DeliverDate_Panel.Controls.Add(Me.NoneDate_CB)
        Me.DeliverDate_Panel.Controls.Add(Me.Up_Btn)
        Me.DeliverDate_Panel.Controls.Add(Me.Label4)
        Me.DeliverDate_Panel.Location = New System.Drawing.Point(3, 159)
        Me.DeliverDate_Panel.Name = "DeliverDate_Panel"
        Me.DeliverDate_Panel.Size = New System.Drawing.Size(391, 93)
        Me.DeliverDate_Panel.TabIndex = 575
        Me.DeliverDate_Panel.Visible = False
        '
        'B1
        '
        Me.B1.BackColor = System.Drawing.SystemColors.Control
        Me.B1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B1.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B1.ForeColor = System.Drawing.Color.Firebrick
        Me.B1.Location = New System.Drawing.Point(4, 258)
        Me.B1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B1.Name = "B1"
        Me.B1.Size = New System.Drawing.Size(75, 70)
        Me.B1.TabIndex = 576
        Me.B1.Tag = "1"
        Me.B1.Text = "1"
        Me.B1.UseVisualStyleBackColor = False
        '
        'B5
        '
        Me.B5.BackColor = System.Drawing.SystemColors.Control
        Me.B5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B5.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B5.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B5.ForeColor = System.Drawing.Color.Firebrick
        Me.B5.Location = New System.Drawing.Point(87, 334)
        Me.B5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B5.Name = "B5"
        Me.B5.Size = New System.Drawing.Size(75, 70)
        Me.B5.TabIndex = 580
        Me.B5.Tag = "5"
        Me.B5.Text = "5"
        Me.B5.UseVisualStyleBackColor = False
        '
        'ClearNumBtn
        '
        Me.ClearNumBtn.BackColor = System.Drawing.Color.IndianRed
        Me.ClearNumBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ClearNumBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ClearNumBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClearNumBtn.ForeColor = System.Drawing.SystemColors.Control
        Me.ClearNumBtn.Location = New System.Drawing.Point(252, 258)
        Me.ClearNumBtn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ClearNumBtn.Name = "ClearNumBtn"
        Me.ClearNumBtn.Size = New System.Drawing.Size(75, 70)
        Me.ClearNumBtn.TabIndex = 587
        Me.ClearNumBtn.Text = "C"
        Me.ClearNumBtn.UseVisualStyleBackColor = False
        '
        'BDot
        '
        Me.BDot.BackColor = System.Drawing.SystemColors.Control
        Me.BDot.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BDot.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BDot.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BDot.ForeColor = System.Drawing.Color.DarkRed
        Me.BDot.Location = New System.Drawing.Point(252, 334)
        Me.BDot.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.BDot.Name = "BDot"
        Me.BDot.Size = New System.Drawing.Size(75, 70)
        Me.BDot.TabIndex = 586
        Me.BDot.Tag = "."
        Me.BDot.Text = "."
        Me.BDot.UseVisualStyleBackColor = False
        '
        'B0
        '
        Me.B0.BackColor = System.Drawing.SystemColors.Control
        Me.B0.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B0.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B0.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B0.ForeColor = System.Drawing.Color.Firebrick
        Me.B0.Location = New System.Drawing.Point(252, 409)
        Me.B0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B0.Name = "B0"
        Me.B0.Size = New System.Drawing.Size(75, 70)
        Me.B0.TabIndex = 585
        Me.B0.Tag = "0"
        Me.B0.Text = "0"
        Me.B0.UseVisualStyleBackColor = False
        '
        'B8
        '
        Me.B8.BackColor = System.Drawing.SystemColors.Control
        Me.B8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B8.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B8.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B8.ForeColor = System.Drawing.Color.Firebrick
        Me.B8.Location = New System.Drawing.Point(87, 409)
        Me.B8.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B8.Name = "B8"
        Me.B8.Size = New System.Drawing.Size(75, 70)
        Me.B8.TabIndex = 583
        Me.B8.Tag = "8"
        Me.B8.Text = "8"
        Me.B8.UseVisualStyleBackColor = False
        '
        'B7
        '
        Me.B7.BackColor = System.Drawing.SystemColors.Control
        Me.B7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B7.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B7.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B7.ForeColor = System.Drawing.Color.Firebrick
        Me.B7.Location = New System.Drawing.Point(4, 409)
        Me.B7.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B7.Name = "B7"
        Me.B7.Size = New System.Drawing.Size(75, 70)
        Me.B7.TabIndex = 582
        Me.B7.Tag = "7"
        Me.B7.Text = "7"
        Me.B7.UseVisualStyleBackColor = False
        '
        'B4
        '
        Me.B4.BackColor = System.Drawing.SystemColors.Control
        Me.B4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B4.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B4.ForeColor = System.Drawing.Color.Firebrick
        Me.B4.Location = New System.Drawing.Point(4, 334)
        Me.B4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B4.Name = "B4"
        Me.B4.Size = New System.Drawing.Size(75, 70)
        Me.B4.TabIndex = 579
        Me.B4.Tag = "4"
        Me.B4.Text = "4"
        Me.B4.UseVisualStyleBackColor = False
        '
        'B6
        '
        Me.B6.BackColor = System.Drawing.SystemColors.Control
        Me.B6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B6.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B6.ForeColor = System.Drawing.Color.Firebrick
        Me.B6.Location = New System.Drawing.Point(170, 334)
        Me.B6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B6.Name = "B6"
        Me.B6.Size = New System.Drawing.Size(75, 70)
        Me.B6.TabIndex = 581
        Me.B6.Tag = "6"
        Me.B6.Text = "6"
        Me.B6.UseVisualStyleBackColor = False
        '
        'B2
        '
        Me.B2.BackColor = System.Drawing.SystemColors.Control
        Me.B2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B2.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B2.ForeColor = System.Drawing.Color.Firebrick
        Me.B2.Location = New System.Drawing.Point(87, 258)
        Me.B2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B2.Name = "B2"
        Me.B2.Size = New System.Drawing.Size(75, 70)
        Me.B2.TabIndex = 577
        Me.B2.Tag = "2"
        Me.B2.Text = "2"
        Me.B2.UseVisualStyleBackColor = False
        '
        'B9
        '
        Me.B9.BackColor = System.Drawing.SystemColors.Control
        Me.B9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B9.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B9.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B9.ForeColor = System.Drawing.Color.Firebrick
        Me.B9.Location = New System.Drawing.Point(170, 409)
        Me.B9.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B9.Name = "B9"
        Me.B9.Size = New System.Drawing.Size(75, 70)
        Me.B9.TabIndex = 584
        Me.B9.Tag = "9"
        Me.B9.Text = "9"
        Me.B9.UseVisualStyleBackColor = False
        '
        'B3
        '
        Me.B3.BackColor = System.Drawing.SystemColors.Control
        Me.B3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.B3.Font = New System.Drawing.Font("Tahoma", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B3.ForeColor = System.Drawing.Color.Firebrick
        Me.B3.Location = New System.Drawing.Point(170, 258)
        Me.B3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.B3.Name = "B3"
        Me.B3.Size = New System.Drawing.Size(75, 70)
        Me.B3.TabIndex = 578
        Me.B3.Tag = "3"
        Me.B3.Text = "3"
        Me.B3.UseVisualStyleBackColor = False
        '
        'OrderOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 542)
        Me.ControlBox = False
        Me.Controls.Add(Me.B1)
        Me.Controls.Add(Me.B5)
        Me.Controls.Add(Me.ClearNumBtn)
        Me.Controls.Add(Me.BDot)
        Me.Controls.Add(Me.B0)
        Me.Controls.Add(Me.B8)
        Me.Controls.Add(Me.B7)
        Me.Controls.Add(Me.B4)
        Me.Controls.Add(Me.B6)
        Me.Controls.Add(Me.B2)
        Me.Controls.Add(Me.B9)
        Me.Controls.Add(Me.B3)
        Me.Controls.Add(Me.DeliverDate_Panel)
        Me.Controls.Add(Me.Point_Panel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Barcode_txt)
        Me.Controls.Add(Me.Rest_txt)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.Piedmoney_txt)
        Me.Controls.Add(Me.Back_Btn)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "OrderOptions"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الدفـــع"
        Me.Point_Panel.ResumeLayout(False)
        Me.Point_Panel.PerformLayout()
        Me.DeliverDate_Panel.ResumeLayout(False)
        Me.DeliverDate_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Delever_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents NoneDate_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents Piedmoney_txt As System.Windows.Forms.TextBox
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents Rest_txt As System.Windows.Forms.TextBox
    Friend WithEvents Barcode_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Up_Btn As System.Windows.Forms.Button
    Friend WithEvents Down_Btn As System.Windows.Forms.Button
    Friend WithEvents Barcode_CB As System.Windows.Forms.CheckBox
    Friend WithEvents UsePoint_btn As System.Windows.Forms.Button
    Friend WithEvents Point_LB As System.Windows.Forms.Label
    Friend WithEvents Point_Panel As System.Windows.Forms.Panel
    Friend WithEvents DeliverDate_Panel As System.Windows.Forms.Panel
    Friend WithEvents B1 As System.Windows.Forms.Button
    Friend WithEvents B5 As System.Windows.Forms.Button
    Friend WithEvents ClearNumBtn As System.Windows.Forms.Button
    Friend WithEvents BDot As System.Windows.Forms.Button
    Friend WithEvents B0 As System.Windows.Forms.Button
    Friend WithEvents B8 As System.Windows.Forms.Button
    Friend WithEvents B7 As System.Windows.Forms.Button
    Friend WithEvents B4 As System.Windows.Forms.Button
    Friend WithEvents B6 As System.Windows.Forms.Button
    Friend WithEvents B2 As System.Windows.Forms.Button
    Friend WithEvents B9 As System.Windows.Forms.Button
    Friend WithEvents B3 As System.Windows.Forms.Button
End Class
