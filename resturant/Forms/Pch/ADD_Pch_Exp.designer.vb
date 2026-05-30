<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ADD_Pch_Exp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ADD_Pch_Exp))
        Me.CD_Money_txt = New System.Windows.Forms.TextBox()
        Me.NoneContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.OrderDeliver_btn = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Notes_cm = New System.Windows.Forms.ComboBox()
        Me.isWithBill_CB = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ExpBillNum_txt = New System.Windows.Forms.TextBox()
        Me.BrowseExpBill_btn = New System.Windows.Forms.Button()
        Me.ImportExpBill_btn = New System.Windows.Forms.Button()
        Me.ExpBillInfo_lb = New System.Windows.Forms.Label()
        Me.ExpBills_DGV = New System.Windows.Forms.DataGridView()
        Me.ExpBill_T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpBill_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpBill_Date_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpBill_Agent_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpBill_Cost_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpBill_State_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.ExpBills_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CD_Money_txt
        '
        Me.CD_Money_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CD_Money_txt.ContextMenuStrip = Me.NoneContextMenuStrip1
        Me.CD_Money_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CD_Money_txt.ForeColor = System.Drawing.Color.DarkBlue
        Me.CD_Money_txt.Location = New System.Drawing.Point(89, 3)
        Me.CD_Money_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CD_Money_txt.Name = "CD_Money_txt"
        Me.CD_Money_txt.ReadOnly = True
        Me.CD_Money_txt.Size = New System.Drawing.Size(149, 29)
        Me.CD_Money_txt.TabIndex = 386
        Me.CD_Money_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NoneContextMenuStrip1
        '
        Me.NoneContextMenuStrip1.Name = "GVContextMenuStrip"
        Me.NoneContextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NoneContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 21)
        Me.Label3.TabIndex = 387
        Me.Label3.Text = "الإجمالي"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(24, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(43, 21)
        Me.Label9.TabIndex = 391
        Me.Label9.Text = "البند"
        Me.Label9.Visible = False
        '
        'OrderDeliver_btn
        '
        Me.OrderDeliver_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.OrderDeliver_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.OrderDeliver_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.OrderDeliver_btn.Enabled = False
        Me.OrderDeliver_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.OrderDeliver_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.OrderDeliver_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.OrderDeliver_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OrderDeliver_btn.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OrderDeliver_btn.ForeColor = System.Drawing.Color.Black
        Me.OrderDeliver_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OrderDeliver_btn.Location = New System.Drawing.Point(12, 407)
        Me.OrderDeliver_btn.Name = "OrderDeliver_btn"
        Me.OrderDeliver_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.OrderDeliver_btn.Size = New System.Drawing.Size(626, 45)
        Me.OrderDeliver_btn.TabIndex = 577
        Me.OrderDeliver_btn.Text = "حفـــظ"
        Me.OrderDeliver_btn.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.ContextMenuStrip = Me.NoneContextMenuStrip1
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(240, 3)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(110, 29)
        Me.TextBox1.TabIndex = 578
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Notes_cm
        '
        Me.Notes_cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Notes_cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Notes_cm.BackColor = System.Drawing.SystemColors.Info
        Me.Notes_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Notes_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Notes_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Notes_cm.FormattingEnabled = True
        Me.Notes_cm.IntegralHeight = False
        Me.Notes_cm.Items.AddRange(New Object() {"خدمة", "بضاعة", "تصنيع"})
        Me.Notes_cm.Location = New System.Drawing.Point(89, 37)
        Me.Notes_cm.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Notes_cm.Name = "Notes_cm"
        Me.Notes_cm.Size = New System.Drawing.Size(252, 26)
        Me.Notes_cm.TabIndex = 579
        Me.Notes_cm.Visible = False
        '
        'isWithBill_CB
        '
        Me.isWithBill_CB.AutoSize = True
        Me.isWithBill_CB.BackColor = System.Drawing.Color.Transparent
        Me.isWithBill_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.isWithBill_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.isWithBill_CB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isWithBill_CB.ForeColor = System.Drawing.Color.Black
        Me.isWithBill_CB.Location = New System.Drawing.Point(89, 78)
        Me.isWithBill_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.isWithBill_CB.Name = "isWithBill_CB"
        Me.isWithBill_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.isWithBill_CB.Size = New System.Drawing.Size(150, 25)
        Me.isWithBill_CB.TabIndex = 580
        Me.isWithBill_CB.Text = "ضمن فاتورة المورد"
        Me.isWithBill_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.isWithBill_CB.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(2, 107)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(348, 45)
        Me.Button1.TabIndex = 581
        Me.Button1.Text = "إضافة البند"
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(543, 164)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 29)
        Me.Label1.TabIndex = 582
        Me.Label1.Text = "فاتورة مصروفات"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ExpBillNum_txt
        '
        Me.ExpBillNum_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ExpBillNum_txt.ContextMenuStrip = Me.NoneContextMenuStrip1
        Me.ExpBillNum_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExpBillNum_txt.Location = New System.Drawing.Point(430, 166)
        Me.ExpBillNum_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ExpBillNum_txt.Name = "ExpBillNum_txt"
        Me.ExpBillNum_txt.Size = New System.Drawing.Size(107, 25)
        Me.ExpBillNum_txt.TabIndex = 583
        Me.ExpBillNum_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BrowseExpBill_btn
        '
        Me.BrowseExpBill_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BrowseExpBill_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BrowseExpBill_btn.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BrowseExpBill_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.BrowseExpBill_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.BrowseExpBill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BrowseExpBill_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrowseExpBill_btn.ForeColor = System.Drawing.Color.Black
        Me.BrowseExpBill_btn.Location = New System.Drawing.Point(313, 164)
        Me.BrowseExpBill_btn.Name = "BrowseExpBill_btn"
        Me.BrowseExpBill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BrowseExpBill_btn.Size = New System.Drawing.Size(110, 29)
        Me.BrowseExpBill_btn.TabIndex = 584
        Me.BrowseExpBill_btn.Text = "☰ استعراض"
        Me.BrowseExpBill_btn.UseVisualStyleBackColor = False
        '
        'ImportExpBill_btn
        '
        Me.ImportExpBill_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ImportExpBill_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImportExpBill_btn.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.ImportExpBill_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ImportExpBill_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.ImportExpBill_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ImportExpBill_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ImportExpBill_btn.ForeColor = System.Drawing.Color.DarkGreen
        Me.ImportExpBill_btn.Location = New System.Drawing.Point(12, 164)
        Me.ImportExpBill_btn.Name = "ImportExpBill_btn"
        Me.ImportExpBill_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ImportExpBill_btn.Size = New System.Drawing.Size(295, 29)
        Me.ImportExpBill_btn.TabIndex = 585
        Me.ImportExpBill_btn.Text = "＋ إضافة بنود فاتورة المصروفات"
        Me.ImportExpBill_btn.UseVisualStyleBackColor = False
        '
        'ExpBillInfo_lb
        '
        Me.ExpBillInfo_lb.BackColor = System.Drawing.Color.Honeydew
        Me.ExpBillInfo_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ExpBillInfo_lb.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExpBillInfo_lb.ForeColor = System.Drawing.Color.DarkGreen
        Me.ExpBillInfo_lb.Location = New System.Drawing.Point(12, 201)
        Me.ExpBillInfo_lb.Name = "ExpBillInfo_lb"
        Me.ExpBillInfo_lb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExpBillInfo_lb.Size = New System.Drawing.Size(626, 25)
        Me.ExpBillInfo_lb.TabIndex = 586
        Me.ExpBillInfo_lb.Text = "أدخل رقم فاتورة المصروفات أو اخترها من الاستعراض"
        Me.ExpBillInfo_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ExpBills_DGV
        '
        Me.ExpBills_DGV.AllowUserToAddRows = False
        Me.ExpBills_DGV.AllowUserToDeleteRows = False
        Me.ExpBills_DGV.AllowUserToResizeRows = False
        Me.ExpBills_DGV.AutoGenerateColumns = False
        Me.ExpBills_DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.ExpBills_DGV.BackgroundColor = System.Drawing.Color.White
        Me.ExpBills_DGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ExpBills_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ExpBills_DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ExpBill_T_ID_CL, Me.ExpBill_ID_CL, Me.ExpBill_Date_CL, Me.ExpBill_Agent_CL, Me.ExpBill_Cost_CL, Me.ExpBill_State_CL})
        Me.ExpBills_DGV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExpBills_DGV.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ExpBills_DGV.Location = New System.Drawing.Point(12, 229)
        Me.ExpBills_DGV.MultiSelect = False
        Me.ExpBills_DGV.Name = "ExpBills_DGV"
        Me.ExpBills_DGV.ReadOnly = True
        Me.ExpBills_DGV.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExpBills_DGV.RowHeadersVisible = False
        Me.ExpBills_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ExpBills_DGV.Size = New System.Drawing.Size(626, 169)
        Me.ExpBills_DGV.TabIndex = 587
        Me.ExpBills_DGV.Visible = False
        '
        'ExpBill_T_ID_CL
        '
        Me.ExpBill_T_ID_CL.DataPropertyName = "T_ID"
        Me.ExpBill_T_ID_CL.HeaderText = "T_ID"
        Me.ExpBill_T_ID_CL.Name = "ExpBill_T_ID_CL"
        Me.ExpBill_T_ID_CL.ReadOnly = True
        Me.ExpBill_T_ID_CL.Visible = False
        '
        'ExpBill_ID_CL
        '
        Me.ExpBill_ID_CL.DataPropertyName = "Bill_ID"
        Me.ExpBill_ID_CL.FillWeight = 70.0!
        Me.ExpBill_ID_CL.HeaderText = "الرقم"
        Me.ExpBill_ID_CL.Name = "ExpBill_ID_CL"
        Me.ExpBill_ID_CL.ReadOnly = True
        '
        'ExpBill_Date_CL
        '
        Me.ExpBill_Date_CL.DataPropertyName = "Date"
        Me.ExpBill_Date_CL.FillWeight = 95.0!
        Me.ExpBill_Date_CL.HeaderText = "التاريخ"
        Me.ExpBill_Date_CL.Name = "ExpBill_Date_CL"
        Me.ExpBill_Date_CL.ReadOnly = True
        '
        'ExpBill_Agent_CL
        '
        Me.ExpBill_Agent_CL.DataPropertyName = "Ag_name"
        Me.ExpBill_Agent_CL.HeaderText = "الحساب"
        Me.ExpBill_Agent_CL.Name = "ExpBill_Agent_CL"
        Me.ExpBill_Agent_CL.ReadOnly = True
        '
        'ExpBill_Cost_CL
        '
        Me.ExpBill_Cost_CL.DataPropertyName = "Cost"
        Me.ExpBill_Cost_CL.FillWeight = 85.0!
        Me.ExpBill_Cost_CL.HeaderText = "الإجمالي"
        Me.ExpBill_Cost_CL.Name = "ExpBill_Cost_CL"
        Me.ExpBill_Cost_CL.ReadOnly = True
        '
        'ExpBill_State_CL
        '
        Me.ExpBill_State_CL.DataPropertyName = "Name_"
        Me.ExpBill_State_CL.FillWeight = 85.0!
        Me.ExpBill_State_CL.HeaderText = "الحالة"
        Me.ExpBill_State_CL.Name = "ExpBill_State_CL"
        Me.ExpBill_State_CL.ReadOnly = True
        '
        'ADD_Pch_Exp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 464)
        Me.Controls.Add(Me.ExpBills_DGV)
        Me.Controls.Add(Me.ExpBillInfo_lb)
        Me.Controls.Add(Me.ImportExpBill_btn)
        Me.Controls.Add(Me.BrowseExpBill_btn)
        Me.Controls.Add(Me.ExpBillNum_txt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.isWithBill_CB)
        Me.Controls.Add(Me.Notes_cm)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.OrderDeliver_btn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CD_Money_txt)
        Me.Controls.Add(Me.Label3)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ADD_Pch_Exp"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إضافة مصروفات موزعة"
        CType(Me.ExpBills_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CD_Money_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents OrderDeliver_btn As System.Windows.Forms.Button
    Friend WithEvents NoneContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Notes_cm As System.Windows.Forms.ComboBox
    Friend WithEvents isWithBill_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ExpBillNum_txt As TextBox
    Friend WithEvents BrowseExpBill_btn As Button
    Friend WithEvents ImportExpBill_btn As Button
    Friend WithEvents ExpBillInfo_lb As Label
    Friend WithEvents ExpBills_DGV As DataGridView
    Friend WithEvents ExpBill_T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents ExpBill_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents ExpBill_Date_CL As DataGridViewTextBoxColumn
    Friend WithEvents ExpBill_Agent_CL As DataGridViewTextBoxColumn
    Friend WithEvents ExpBill_Cost_CL As DataGridViewTextBoxColumn
    Friend WithEvents ExpBill_State_CL As DataGridViewTextBoxColumn
End Class
