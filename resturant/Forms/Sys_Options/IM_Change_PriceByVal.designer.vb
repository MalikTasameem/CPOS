<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_Change_PriceByVal
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_Change_PriceByVal))
        Me.InsertU_btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Val_TXT = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GM_Serach = New System.Windows.Forms.ComboBox()
        Me.IM_GV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.U_IM_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.IM_GV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'InsertU_btn
        '
        Me.InsertU_btn.BackColor = System.Drawing.Color.White
        Me.InsertU_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.InsertU_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InsertU_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.InsertU_btn.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold)
        Me.InsertU_btn.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.InsertU_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InsertU_btn.Location = New System.Drawing.Point(1, 2)
        Me.InsertU_btn.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.InsertU_btn.Name = "InsertU_btn"
        Me.InsertU_btn.Size = New System.Drawing.Size(158, 39)
        Me.InsertU_btn.TabIndex = 457
        Me.InsertU_btn.Text = "تعديل القائمة"
        Me.InsertU_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InsertU_btn.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(569, 605)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(123, 49)
        Me.ExitFormButton.TabIndex = 672
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Val_TXT
        '
        Me.Val_TXT.Font = New System.Drawing.Font("Times New Roman", 13.25!)
        Me.Val_TXT.Location = New System.Drawing.Point(167, 9)
        Me.Val_TXT.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Val_TXT.Name = "Val_TXT"
        Me.Val_TXT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Val_TXT.Size = New System.Drawing.Size(99, 28)
        Me.Val_TXT.TabIndex = 675
        Me.Val_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label19.Location = New System.Drawing.Point(271, 10)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(56, 24)
        Me.Label19.TabIndex = 676
        Me.Label19.Text = "القيمة"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(613, 9)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 28)
        Me.Label2.TabIndex = 680
        Me.Label2.Text = "التصنيف"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GM_Serach
        '
        Me.GM_Serach.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.GM_Serach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.GM_Serach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.GM_Serach.BackColor = System.Drawing.SystemColors.Info
        Me.GM_Serach.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GM_Serach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GM_Serach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GM_Serach.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Serach.Location = New System.Drawing.Point(334, 8)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.Size = New System.Drawing.Size(276, 29)
        Me.GM_Serach.TabIndex = 681
        '
        'IM_GV
        '
        Me.IM_GV.AllowUserToAddRows = False
        Me.IM_GV.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray
        Me.IM_GV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.IM_GV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IM_GV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.IM_GV.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IM_GV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.IM_GV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IM_GV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.U_IM_ID_CL, Me.item_name_CL, Me.U_Name_CL, Me.Price_CL})
        Me.IM_GV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IM_GV.DefaultCellStyle = DataGridViewCellStyle3
        Me.IM_GV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IM_GV.Location = New System.Drawing.Point(1, 45)
        Me.IM_GV.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IM_GV.Name = "IM_GV"
        Me.IM_GV.RowHeadersVisible = False
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_GV.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.IM_GV.RowTemplate.Height = 35
        Me.IM_GV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IM_GV.Size = New System.Drawing.Size(691, 558)
        Me.IM_GV.TabIndex = 679
        '
        'U_IM_ID_CL
        '
        Me.U_IM_ID_CL.DataPropertyName = "U_IM_ID"
        Me.U_IM_ID_CL.HeaderText = "U_IM_ID"
        Me.U_IM_ID_CL.Name = "U_IM_ID_CL"
        Me.U_IM_ID_CL.Visible = False
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.FillWeight = 97.0213!
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.Name = "item_name_CL"
        '
        'U_Name_CL
        '
        Me.U_Name_CL.DataPropertyName = "U_Name"
        Me.U_Name_CL.FillWeight = 57.75078!
        Me.U_Name_CL.HeaderText = "الوحدة"
        Me.U_Name_CL.Name = "U_Name_CL"
        '
        'Price_CL
        '
        Me.Price_CL.DataPropertyName = "Price"
        Me.Price_CL.FillWeight = 72.76598!
        Me.Price_CL.HeaderText = "السعر"
        Me.Price_CL.Name = "Price_CL"
        '
        'IM_Change_PriceByVal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 28.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 654)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GM_Serach)
        Me.Controls.Add(Me.IM_GV)
        Me.Controls.Add(Me.Val_TXT)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.InsertU_btn)
        Me.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IM_Change_PriceByVal"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تعديل الأسعار بقيمة"
        CType(Me.IM_GV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents InsertU_btn As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Val_TXT As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GM_Serach As System.Windows.Forms.ComboBox
    Friend WithEvents IM_GV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents U_IM_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
