<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Inside_Sales_IM_card
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Inside_Sales_IM_card))
        Me.All_St_Panel = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ALL_QTY_txt = New System.Windows.Forms.TextBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Current_QTY = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.ST_cm = New System.Windows.Forms.ComboBox()
        Me.ADDCatButton = New System.Windows.Forms.Button()
        Me.Exit_Btn = New System.Windows.Forms.Button()
        Me.mySearchControl = New resturant.SearchItemControl()
        Me.Valid_Panel = New System.Windows.Forms.Panel()
        Me.Valid_cm = New System.Windows.Forms.ComboBox()
        Me.Valid_QTY_txt = New System.Windows.Forms.TextBox()
        Me.IM_Cost_Panel = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.IM_Cost_txt = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.QtyTextBox = New System.Windows.Forms.TextBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.IM_Unit_cm = New System.Windows.Forms.ComboBox()
        Me.InSale_All_ST_Btn = New System.Windows.Forms.Button()
        Me.All_St_Panel.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Valid_Panel.SuspendLayout()
        Me.IM_Cost_Panel.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'All_St_Panel
        '
        Me.All_St_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.All_St_Panel.Controls.Add(Me.Label5)
        Me.All_St_Panel.Controls.Add(Me.ALL_QTY_txt)
        Me.All_St_Panel.Location = New System.Drawing.Point(2, 117)
        Me.All_St_Panel.Name = "All_St_Panel"
        Me.All_St_Panel.Size = New System.Drawing.Size(245, 31)
        Me.All_St_Panel.TabIndex = 1027
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label5.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label5.Location = New System.Drawing.Point(105, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(119, 20)
        Me.Label5.TabIndex = 642
        Me.Label5.Text = "كمية كل المخازن :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ALL_QTY_txt
        '
        Me.ALL_QTY_txt.BackColor = System.Drawing.SystemColors.HighlightText
        Me.ALL_QTY_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ALL_QTY_txt.Font = New System.Drawing.Font("Times New Roman", 12.75!)
        Me.ALL_QTY_txt.ForeColor = System.Drawing.Color.Firebrick
        Me.ALL_QTY_txt.Location = New System.Drawing.Point(3, 1)
        Me.ALL_QTY_txt.Name = "ALL_QTY_txt"
        Me.ALL_QTY_txt.ReadOnly = True
        Me.ALL_QTY_txt.Size = New System.Drawing.Size(99, 27)
        Me.ALL_QTY_txt.TabIndex = 645
        Me.ALL_QTY_txt.Text = "00"
        Me.ALL_QTY_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel8
        '
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.Label26)
        Me.Panel8.Controls.Add(Me.Current_QTY)
        Me.Panel8.Location = New System.Drawing.Point(2, 85)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(245, 31)
        Me.Panel8.TabIndex = 1026
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label26.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label26.Location = New System.Drawing.Point(105, 4)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label26.Size = New System.Drawing.Size(137, 20)
        Me.Label26.TabIndex = 643
        Me.Label26.Text = "كمية المخزن الحالي :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Current_QTY
        '
        Me.Current_QTY.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Current_QTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Current_QTY.Font = New System.Drawing.Font("Times New Roman", 12.75!)
        Me.Current_QTY.ForeColor = System.Drawing.Color.Firebrick
        Me.Current_QTY.Location = New System.Drawing.Point(3, 1)
        Me.Current_QTY.Name = "Current_QTY"
        Me.Current_QTY.ReadOnly = True
        Me.Current_QTY.Size = New System.Drawing.Size(99, 27)
        Me.Current_QTY.TabIndex = 643
        Me.Current_QTY.Text = "00"
        Me.Current_QTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label27)
        Me.Panel4.Controls.Add(Me.ST_cm)
        Me.Panel4.Location = New System.Drawing.Point(2, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(306, 35)
        Me.Panel4.TabIndex = 1044
        '
        'Label27
        '
        Me.Label27.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label27.Location = New System.Drawing.Point(244, 6)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(59, 20)
        Me.Label27.TabIndex = 684
        Me.Label27.Text = "المخزن :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ST_cm
        '
        Me.ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(3, 3)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.Size = New System.Drawing.Size(238, 28)
        Me.ST_cm.TabIndex = 0
        '
        'ADDCatButton
        '
        Me.ADDCatButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ADDCatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ADDCatButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADDCatButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ADDCatButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ADDCatButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADDCatButton.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ADDCatButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ADDCatButton.Image = CType(resources.GetObject("ADDCatButton.Image"), System.Drawing.Image)
        Me.ADDCatButton.Location = New System.Drawing.Point(4, 382)
        Me.ADDCatButton.Name = "ADDCatButton"
        Me.ADDCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ADDCatButton.Size = New System.Drawing.Size(241, 89)
        Me.ADDCatButton.TabIndex = 1045
        Me.ADDCatButton.TabStop = False
        Me.ADDCatButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ADDCatButton.UseVisualStyleBackColor = False
        '
        'Exit_Btn
        '
        Me.Exit_Btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Exit_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Exit_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Exit_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Exit_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Exit_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Exit_Btn.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Exit_Btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Exit_Btn.Image = Global.resturant.My.Resources.Resources.Arrow_doodle_128
        Me.Exit_Btn.Location = New System.Drawing.Point(808, 382)
        Me.Exit_Btn.Name = "Exit_Btn"
        Me.Exit_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Exit_Btn.Size = New System.Drawing.Size(241, 89)
        Me.Exit_Btn.TabIndex = 1046
        Me.Exit_Btn.TabStop = False
        Me.Exit_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Exit_Btn.UseVisualStyleBackColor = False
        '
        'mySearchControl
        '
        Me.mySearchControl.BackColor = System.Drawing.Color.WhiteSmoke
        Me.mySearchControl.DefaultSearchField = "إسم الصنف"
        Me.mySearchControl.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.mySearchControl.ItemsTable = Nothing
        Me.mySearchControl.itemsTable_Barcode = Nothing
        Me.mySearchControl.Location = New System.Drawing.Point(311, 5)
        Me.mySearchControl.Margin = New System.Windows.Forms.Padding(0)
        Me.mySearchControl.MarginBetweenSearchAndGrid = 15
        Me.mySearchControl.MaxGridHeight = 400
        Me.mySearchControl.Name = "mySearchControl"
        Me.mySearchControl.Size = New System.Drawing.Size(738, 35)
        Me.mySearchControl.TabIndex = 1047
        '
        'Valid_Panel
        '
        Me.Valid_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Valid_Panel.Controls.Add(Me.Valid_cm)
        Me.Valid_Panel.Controls.Add(Me.Valid_QTY_txt)
        Me.Valid_Panel.Location = New System.Drawing.Point(326, 85)
        Me.Valid_Panel.Name = "Valid_Panel"
        Me.Valid_Panel.Size = New System.Drawing.Size(226, 36)
        Me.Valid_Panel.TabIndex = 1056
        Me.Valid_Panel.Visible = False
        '
        'Valid_cm
        '
        Me.Valid_cm.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Valid_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Valid_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Valid_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Valid_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Valid_cm.FormattingEnabled = True
        Me.Valid_cm.Location = New System.Drawing.Point(102, 4)
        Me.Valid_cm.Name = "Valid_cm"
        Me.Valid_cm.Size = New System.Drawing.Size(118, 28)
        Me.Valid_cm.TabIndex = 639
        '
        'Valid_QTY_txt
        '
        Me.Valid_QTY_txt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Valid_QTY_txt.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Valid_QTY_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Valid_QTY_txt.Enabled = False
        Me.Valid_QTY_txt.Font = New System.Drawing.Font("Times New Roman", 13.25!)
        Me.Valid_QTY_txt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Valid_QTY_txt.Location = New System.Drawing.Point(2, 4)
        Me.Valid_QTY_txt.Name = "Valid_QTY_txt"
        Me.Valid_QTY_txt.Size = New System.Drawing.Size(96, 28)
        Me.Valid_QTY_txt.TabIndex = 640
        Me.Valid_QTY_txt.Text = "00"
        Me.Valid_QTY_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IM_Cost_Panel
        '
        Me.IM_Cost_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Cost_Panel.Controls.Add(Me.Label2)
        Me.IM_Cost_Panel.Controls.Add(Me.IM_Cost_txt)
        Me.IM_Cost_Panel.Location = New System.Drawing.Point(679, 85)
        Me.IM_Cost_Panel.Name = "IM_Cost_Panel"
        Me.IM_Cost_Panel.Size = New System.Drawing.Size(156, 36)
        Me.IM_Cost_Panel.TabIndex = 1055
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(92, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 20)
        Me.Label2.TabIndex = 604
        Me.Label2.Text = "التكلفة :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IM_Cost_txt
        '
        Me.IM_Cost_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.IM_Cost_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_Cost_txt.Font = New System.Drawing.Font("Stencil", 13.0!)
        Me.IM_Cost_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.IM_Cost_txt.Location = New System.Drawing.Point(2, 3)
        Me.IM_Cost_txt.MaxLength = 250
        Me.IM_Cost_txt.Name = "IM_Cost_txt"
        Me.IM_Cost_txt.ReadOnly = True
        Me.IM_Cost_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IM_Cost_txt.Size = New System.Drawing.Size(84, 28)
        Me.IM_Cost_txt.TabIndex = 1002
        Me.IM_Cost_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.Label15)
        Me.Panel6.Controls.Add(Me.QtyTextBox)
        Me.Panel6.Location = New System.Drawing.Point(554, 85)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(123, 36)
        Me.Panel6.TabIndex = 1054
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(62, 7)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 20)
        Me.Label15.TabIndex = 604
        Me.Label15.Text = "الكمية :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'QtyTextBox
        '
        Me.QtyTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QtyTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.QtyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.QtyTextBox.Font = New System.Drawing.Font("Times New Roman", 14.75!)
        Me.QtyTextBox.ForeColor = System.Drawing.Color.Black
        Me.QtyTextBox.Location = New System.Drawing.Point(2, 2)
        Me.QtyTextBox.MaxLength = 250
        Me.QtyTextBox.Name = "QtyTextBox"
        Me.QtyTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.QtyTextBox.Size = New System.Drawing.Size(58, 30)
        Me.QtyTextBox.TabIndex = 1001
        Me.QtyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Label18)
        Me.Panel7.Controls.Add(Me.IM_Unit_cm)
        Me.Panel7.Location = New System.Drawing.Point(837, 85)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(214, 36)
        Me.Panel7.TabIndex = 1053
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(148, 7)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(58, 20)
        Me.Label18.TabIndex = 616
        Me.Label18.Text = "الوحدة :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IM_Unit_cm
        '
        Me.IM_Unit_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Unit_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IM_Unit_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Unit_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Unit_cm.FormattingEnabled = True
        Me.IM_Unit_cm.Location = New System.Drawing.Point(3, 3)
        Me.IM_Unit_cm.Name = "IM_Unit_cm"
        Me.IM_Unit_cm.Size = New System.Drawing.Size(139, 28)
        Me.IM_Unit_cm.TabIndex = 500
        '
        'InSale_All_ST_Btn
        '
        Me.InSale_All_ST_Btn.BackColor = System.Drawing.Color.White
        Me.InSale_All_ST_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InSale_All_ST_Btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.InSale_All_ST_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.InSale_All_ST_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.InSale_All_ST_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.InSale_All_ST_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InSale_All_ST_Btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.InSale_All_ST_Btn.Image = Global.resturant.My.Resources.Resources.iconfinder_icon_refresh_2867936
        Me.InSale_All_ST_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InSale_All_ST_Btn.Location = New System.Drawing.Point(578, 382)
        Me.InSale_All_ST_Btn.Name = "InSale_All_ST_Btn"
        Me.InSale_All_ST_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.InSale_All_ST_Btn.Size = New System.Drawing.Size(202, 89)
        Me.InSale_All_ST_Btn.TabIndex = 1057
        Me.InSale_All_ST_Btn.TabStop = False
        Me.InSale_All_ST_Btn.Text = "تصفية المخزن كاملا"
        Me.InSale_All_ST_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InSale_All_ST_Btn.UseVisualStyleBackColor = False
        '
        'Inside_Sales_IM_card
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1052, 472)
        Me.ControlBox = False
        Me.Controls.Add(Me.InSale_All_ST_Btn)
        Me.Controls.Add(Me.Valid_Panel)
        Me.Controls.Add(Me.IM_Cost_Panel)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.mySearchControl)
        Me.Controls.Add(Me.Exit_Btn)
        Me.Controls.Add(Me.ADDCatButton)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.All_St_Panel)
        Me.Controls.Add(Me.Panel8)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Inside_Sales_IM_card"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الصنف"
        Me.All_St_Panel.ResumeLayout(False)
        Me.All_St_Panel.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Valid_Panel.ResumeLayout(False)
        Me.Valid_Panel.PerformLayout()
        Me.IM_Cost_Panel.ResumeLayout(False)
        Me.IM_Cost_Panel.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents All_St_Panel As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents ALL_QTY_txt As TextBox
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label26 As Label
    Friend WithEvents Current_QTY As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label27 As Label
    Friend WithEvents ST_cm As ComboBox
    Friend WithEvents ADDCatButton As Button
    Friend WithEvents Exit_Btn As Button
    Friend WithEvents mySearchControl As SearchItemControl
    Friend WithEvents Valid_Panel As Panel
    Friend WithEvents Valid_cm As ComboBox
    Friend WithEvents Valid_QTY_txt As TextBox
    Friend WithEvents IM_Cost_Panel As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents IM_Cost_txt As TextBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents QtyTextBox As TextBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label18 As Label
    Public WithEvents IM_Unit_cm As ComboBox
    Friend WithEvents InSale_All_ST_Btn As Button
End Class
