<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ST_settlement_IM_card
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
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Title_LB = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.IM_Unit_cm = New System.Windows.Forms.ComboBox()
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
        Me.TitleBar_Panel.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.All_St_Panel.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.Title_LB)
        Me.TitleBar_Panel.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(930, 42)
        Me.TitleBar_Panel.TabIndex = 999
        Me.TitleBar_Panel.Tag = "HEADER"
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.Transparent
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.ExitFormButton.FlatAppearance.BorderSize = 0
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.Color.White
        Me.ExitFormButton.Location = New System.Drawing.Point(0, 0)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(45, 42)
        Me.ExitFormButton.TabIndex = 3
        Me.ExitFormButton.Tag = "DELETE"
        Me.ExitFormButton.Text = "X"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Title_LB
        '
        Me.Title_LB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Title_LB.AutoSize = True
        Me.Title_LB.BackColor = System.Drawing.Color.Transparent
        Me.Title_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Title_LB.ForeColor = System.Drawing.Color.White
        Me.Title_LB.Location = New System.Drawing.Point(760, 9)
        Me.Title_LB.Name = "Title_LB"
        Me.Title_LB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Title_LB.Size = New System.Drawing.Size(144, 21)
        Me.Title_LB.TabIndex = 1
        Me.Title_LB.Tag = "TITLE_TRANSPARENT"
        Me.Title_LB.Text = "إضافة صنف للفاتورة"
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Label18)
        Me.Panel7.Controls.Add(Me.IM_Unit_cm)
        Me.Panel7.Location = New System.Drawing.Point(382, 137)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(221, 35)
        Me.Panel7.TabIndex = 1029
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(148, 6)
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
        Me.IM_Unit_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold)
        Me.IM_Unit_cm.FormattingEnabled = True
        Me.IM_Unit_cm.Location = New System.Drawing.Point(2, 1)
        Me.IM_Unit_cm.Name = "IM_Unit_cm"
        Me.IM_Unit_cm.Size = New System.Drawing.Size(139, 27)
        Me.IM_Unit_cm.TabIndex = 500
        '
        'All_St_Panel
        '
        Me.All_St_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.All_St_Panel.Controls.Add(Me.Label5)
        Me.All_St_Panel.Controls.Add(Me.ALL_QTY_txt)
        Me.All_St_Panel.Location = New System.Drawing.Point(2, 176)
        Me.All_St_Panel.Name = "All_St_Panel"
        Me.All_St_Panel.Size = New System.Drawing.Size(306, 36)
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
        Me.Panel8.Location = New System.Drawing.Point(2, 137)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(306, 36)
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
        Me.Panel4.Location = New System.Drawing.Point(2, 98)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(306, 35)
        Me.Panel4.TabIndex = 1044
        '
        'Label27
        '
        Me.Label27.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label27.Location = New System.Drawing.Point(240, 6)
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
        Me.ADDCatButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADDCatButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ADDCatButton.Location = New System.Drawing.Point(4, 373)
        Me.ADDCatButton.Name = "ADDCatButton"
        Me.ADDCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ADDCatButton.Size = New System.Drawing.Size(570, 55)
        Me.ADDCatButton.TabIndex = 1045
        Me.ADDCatButton.TabStop = False
        Me.ADDCatButton.Tag = "GENERAL"
        Me.ADDCatButton.Text = "➕ إضافة للفاتورة"
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
        Me.Exit_Btn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Exit_Btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Exit_Btn.Location = New System.Drawing.Point(584, 373)
        Me.Exit_Btn.Name = "Exit_Btn"
        Me.Exit_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Exit_Btn.Size = New System.Drawing.Size(338, 55)
        Me.Exit_Btn.TabIndex = 1046
        Me.Exit_Btn.TabStop = False
        Me.Exit_Btn.Tag = "DELETE"
        Me.Exit_Btn.Text = "رجوع ◀️"
        Me.Exit_Btn.UseVisualStyleBackColor = False
        '
        'mySearchControl
        '
        Me.mySearchControl.BackColor = System.Drawing.Color.WhiteSmoke
        Me.mySearchControl.DefaultSearchField = "إسم الصنف"
        Me.mySearchControl.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        Me.mySearchControl.ItemsTable = Nothing
        Me.mySearchControl.itemsTable_Barcode = Nothing
        Me.mySearchControl.Location = New System.Drawing.Point(5, 58)
        Me.mySearchControl.Margin = New System.Windows.Forms.Padding(0)
        Me.mySearchControl.MarginBetweenSearchAndGrid = 15
        Me.mySearchControl.MaxGridHeight = 400
        Me.mySearchControl.Name = "mySearchControl"
        Me.mySearchControl.Size = New System.Drawing.Size(916, 35)
        Me.mySearchControl.TabIndex = 1047
        '
        'ST_settlement_IM_card
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(930, 436)
        Me.ControlBox = False
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.mySearchControl)
        Me.Controls.Add(Me.Exit_Btn)
        Me.Controls.Add(Me.ADDCatButton)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.All_St_Panel)
        Me.Controls.Add(Me.Panel8)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ST_settlement_IM_card"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الصنف"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.All_St_Panel.ResumeLayout(False)
        Me.All_St_Panel.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label18 As Label
    Public WithEvents IM_Unit_cm As ComboBox
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
    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Title_LB As System.Windows.Forms.Label
End Class
