<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_MV
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_MV))
        Me.DataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DATE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Notes_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ag_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pluse_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Negitave_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Valid_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ST_cm = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.T_BalanceTextBox = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.T_NegitaveTextBox = New System.Windows.Forms.TextBox()
        Me.T_PluseTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DateRange_Flate = New resturant.DateRange_Flate()
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridViewX
        '
        Me.DataGridViewX.AllowUserToAddRows = False
        Me.DataGridViewX.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info
        Me.DataGridViewX.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewX.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.Type_ID_CL, Me.Type_CL, Me.DATE_CL, Me.Notes_CL, Me.Ag_name_CL, Me.U_Name_CL, Me.Pluse_CL, Me.Negitave_CL, Me.D_Valid_CL, Me.QTY_CL})
        Me.DataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX.Location = New System.Drawing.Point(3, 88)
        Me.DataGridViewX.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.DataGridViewX.MultiSelect = False
        Me.DataGridViewX.Name = "DataGridViewX"
        Me.DataGridViewX.ReadOnly = True
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewX.RowTemplate.Height = 30
        Me.DataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewX.Size = New System.Drawing.Size(1001, 563)
        Me.DataGridViewX.TabIndex = 2
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'Type_ID_CL
        '
        Me.Type_ID_CL.DataPropertyName = "Type_ID"
        Me.Type_ID_CL.HeaderText = "Type_ID"
        Me.Type_ID_CL.Name = "Type_ID_CL"
        Me.Type_ID_CL.ReadOnly = True
        Me.Type_ID_CL.Visible = False
        '
        'Type_CL
        '
        Me.Type_CL.DataPropertyName = "Type_Name"
        Me.Type_CL.HeaderText = "نوع الحركة"
        Me.Type_CL.Name = "Type_CL"
        Me.Type_CL.ReadOnly = True
        '
        'DATE_CL
        '
        Me.DATE_CL.DataPropertyName = "DATE"
        Me.DATE_CL.HeaderText = "التاريخ"
        Me.DATE_CL.Name = "DATE_CL"
        Me.DATE_CL.ReadOnly = True
        '
        'Notes_CL
        '
        Me.Notes_CL.DataPropertyName = "Notes"
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Notes_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Notes_CL.HeaderText = "ملاحظة"
        Me.Notes_CL.Name = "Notes_CL"
        Me.Notes_CL.ReadOnly = True
        '
        'Ag_name_CL
        '
        Me.Ag_name_CL.DataPropertyName = "Ag_name"
        Me.Ag_name_CL.HeaderText = "العميل"
        Me.Ag_name_CL.Name = "Ag_name_CL"
        Me.Ag_name_CL.ReadOnly = True
        Me.Ag_name_CL.Visible = False
        '
        'U_Name_CL
        '
        Me.U_Name_CL.DataPropertyName = "U_Name"
        Me.U_Name_CL.HeaderText = "الوحدة"
        Me.U_Name_CL.Name = "U_Name_CL"
        Me.U_Name_CL.ReadOnly = True
        '
        'Pluse_CL
        '
        Me.Pluse_CL.DataPropertyName = "Pluse"
        DataGridViewCellStyle4.Format = "N2"
        Me.Pluse_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.Pluse_CL.FillWeight = 119.5432!
        Me.Pluse_CL.HeaderText = "زيادة عدد"
        Me.Pluse_CL.MinimumWidth = 100
        Me.Pluse_CL.Name = "Pluse_CL"
        Me.Pluse_CL.ReadOnly = True
        Me.Pluse_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Pluse_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Negitave_CL
        '
        Me.Negitave_CL.DataPropertyName = "Negitave"
        DataGridViewCellStyle5.Format = "N2"
        Me.Negitave_CL.DefaultCellStyle = DataGridViewCellStyle5
        Me.Negitave_CL.HeaderText = "نقص عدد"
        Me.Negitave_CL.Name = "Negitave_CL"
        Me.Negitave_CL.ReadOnly = True
        '
        'D_Valid_CL
        '
        Me.D_Valid_CL.DataPropertyName = "Valid"
        DataGridViewCellStyle6.Format = "d"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.D_Valid_CL.DefaultCellStyle = DataGridViewCellStyle6
        Me.D_Valid_CL.HeaderText = "الصلاحية"
        Me.D_Valid_CL.Name = "D_Valid_CL"
        Me.D_Valid_CL.ReadOnly = True
        Me.D_Valid_CL.Visible = False
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "Balance"
        DataGridViewCellStyle7.Format = "N2"
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle7
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 10.75!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(405, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 20)
        Me.Label9.TabIndex = 648
        Me.Label9.Text = "المخزن"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ST_cm
        '
        Me.ST_cm.BackColor = System.Drawing.SystemColors.Info
        Me.ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_cm.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(191, 52)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ST_cm.Size = New System.Drawing.Size(209, 28)
        Me.ST_cm.TabIndex = 647
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(97, 49)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(91, 36)
        Me.Button1.TabIndex = 390
        Me.Button1.Text = "بحـــث"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = False
        '
        'PrintButton
        '
        Me.PrintButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PrintButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PrintButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.PrintButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrintButton.ForeColor = System.Drawing.Color.Black
        Me.PrintButton.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.PrintButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrintButton.Location = New System.Drawing.Point(4, 49)
        Me.PrintButton.Margin = New System.Windows.Forms.Padding(7, 8, 7, 8)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PrintButton.Size = New System.Drawing.Size(91, 36)
        Me.PrintButton.TabIndex = 274
        Me.PrintButton.Text = "طباعة"
        Me.PrintButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PrintButton.UseVisualStyleBackColor = False
        Me.PrintButton.Visible = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(875, 654)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(127, 40)
        Me.ExitFormButton.TabIndex = 655
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Label19
        '
        Me.Label19.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(132, 664)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(43, 20)
        Me.Label19.TabIndex = 663
        Me.Label19.Text = "العدد"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'T_BalanceTextBox
        '
        Me.T_BalanceTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.T_BalanceTextBox.Font = New System.Drawing.Font("Stencil", 13.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.T_BalanceTextBox.Location = New System.Drawing.Point(5, 659)
        Me.T_BalanceTextBox.Name = "T_BalanceTextBox"
        Me.T_BalanceTextBox.Size = New System.Drawing.Size(124, 28)
        Me.T_BalanceTextBox.TabIndex = 662
        Me.T_BalanceTextBox.Text = "0.00"
        Me.T_BalanceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label17
        '
        Me.Label17.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(330, 664)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(51, 20)
        Me.Label17.TabIndex = 661
        Me.Label17.Text = "الصادر"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(545, 664)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(43, 20)
        Me.Label18.TabIndex = 660
        Me.Label18.Text = "الوارد"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'T_NegitaveTextBox
        '
        Me.T_NegitaveTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.T_NegitaveTextBox.BackColor = System.Drawing.Color.IndianRed
        Me.T_NegitaveTextBox.Font = New System.Drawing.Font("Stencil", 13.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.T_NegitaveTextBox.Location = New System.Drawing.Point(191, 659)
        Me.T_NegitaveTextBox.Name = "T_NegitaveTextBox"
        Me.T_NegitaveTextBox.Size = New System.Drawing.Size(135, 28)
        Me.T_NegitaveTextBox.TabIndex = 659
        Me.T_NegitaveTextBox.Text = "0.00"
        Me.T_NegitaveTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'T_PluseTextBox
        '
        Me.T_PluseTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.T_PluseTextBox.BackColor = System.Drawing.Color.LightGreen
        Me.T_PluseTextBox.Font = New System.Drawing.Font("Stencil", 13.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.T_PluseTextBox.Location = New System.Drawing.Point(406, 659)
        Me.T_PluseTextBox.Name = "T_PluseTextBox"
        Me.T_PluseTextBox.Size = New System.Drawing.Size(135, 28)
        Me.T_PluseTextBox.TabIndex = 658
        Me.T_PluseTextBox.Text = "0.00"
        Me.T_PluseTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(460, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(544, 35)
        Me.Label3.TabIndex = 676
        Me.Label3.Text = "كشف حركة :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DateRange_Flate)
        Me.Panel2.Location = New System.Drawing.Point(3, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1001, 47)
        Me.Panel2.TabIndex = 677
        '
        'DateRange_Flate
        '
        Me.DateRange_Flate.AutoSize = True
        Me.DateRange_Flate.BackColor = System.Drawing.Color.White
        Me.DateRange_Flate.Location = New System.Drawing.Point(466, 3)
        Me.DateRange_Flate.Name = "DateRange_Flate"
        Me.DateRange_Flate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateRange_Flate.Size = New System.Drawing.Size(531, 41)
        Me.DateRange_Flate.TabIndex = 1
        '
        'IM_MV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.T_BalanceTextBox)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.T_NegitaveTextBox)
        Me.Controls.Add(Me.T_PluseTextBox)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ST_cm)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridViewX)
        Me.Controls.Add(Me.PrintButton)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.MinimizeBox = False
        Me.Name = "IM_MV"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "كشف حركة صنف"
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents DataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents PrintButton As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents T_BalanceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents T_NegitaveTextBox As System.Windows.Forms.TextBox
    Friend WithEvents T_PluseTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents DateRange_Flate As DateRange_Flate
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DATE_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Notes_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ag_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pluse_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Negitave_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Valid_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
