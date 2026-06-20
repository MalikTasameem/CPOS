<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MONTHS_CALENDR
    Inherits Base_Form
    'Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MONTHS_CALENDR))
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn = New System.Windows.Forms.Button()
        Me.MOVE_YEAR_TO_ARCHIVE_Btn = New System.Windows.Forms.Button()
        Me.Select_Btn = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DataGridViewX1 = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.M_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.M_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.M_FROM_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.M_TO_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.is_Close_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Open_Btn = New System.Windows.Forms.Button()
        Me.YEAR_status_Label = New System.Windows.Forms.Label()
        Me.Close_Btn = New System.Windows.Forms.Button()
        Me.ADD_Btn = New System.Windows.Forms.Button()
        Me.YEAR_Cm = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Create_Calendar_Btn = New System.Windows.Forms.Button()
        Me.Back_btn = New System.Windows.Forms.Button()
        Me.ARCHIVE_Label = New System.Windows.Forms.Label()
        Me.NONE_ARCHIVE_Label = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RETUTN_YEAR_FROM_ARCHIVE_Btn
        '
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.BackColor = System.Drawing.Color.White
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.Image = CType(resources.GetObject("RETUTN_YEAR_FROM_ARCHIVE_Btn.Image"), System.Drawing.Image)
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.Location = New System.Drawing.Point(2, 33)
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.Name = "RETUTN_YEAR_FROM_ARCHIVE_Btn"
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.Size = New System.Drawing.Size(346, 34)
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.TabIndex = 654
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.Text = "إسترجاع من الأرشيف"
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RETUTN_YEAR_FROM_ARCHIVE_Btn.UseVisualStyleBackColor = False
        '
        'MOVE_YEAR_TO_ARCHIVE_Btn
        '
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.BackColor = System.Drawing.Color.White
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.Location = New System.Drawing.Point(349, 33)
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.Name = "MOVE_YEAR_TO_ARCHIVE_Btn"
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.Size = New System.Drawing.Size(346, 34)
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.TabIndex = 653
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.Text = "إرسال السنة إلى الأرشيف 📦 "
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MOVE_YEAR_TO_ARCHIVE_Btn.UseVisualStyleBackColor = False
        '
        'Select_Btn
        '
        Me.Select_Btn.BackColor = System.Drawing.Color.White
        Me.Select_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Select_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Select_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Select_Btn.Location = New System.Drawing.Point(303, 1)
        Me.Select_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Select_Btn.Name = "Select_Btn"
        Me.Select_Btn.Size = New System.Drawing.Size(220, 31)
        Me.Select_Btn.TabIndex = 652
        Me.Select_Btn.Text = "🖊️ تحديد السنة"
        Me.Select_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Select_Btn.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DataGridViewX1)
        Me.Panel1.Location = New System.Drawing.Point(2, 100)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(693, 327)
        Me.Panel1.TabIndex = 651
        '
        'DataGridViewX1
        '
        Me.DataGridViewX1.AllowUserToAddRows = False
        Me.DataGridViewX1.AllowUserToDeleteRows = False
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewX1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewX1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewX1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridViewX1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.M_ID_CL, Me.M_NAME_CL, Me.M_FROM_CL, Me.M_TO_CL, Me.Status_CL, Me.is_Close_CL})
        Me.DataGridViewX1.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX1.DefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewX1.EnableHeadersVisualStyles = False
        Me.DataGridViewX1.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewX1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridViewX1.MultiSelect = False
        Me.DataGridViewX1.Name = "DataGridViewX1"
        Me.DataGridViewX1.ReadOnly = True
        Me.DataGridViewX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DataGridViewX1.RowHeadersVisible = False
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewX1.RowsDefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridViewX1.RowTemplate.Height = 25
        Me.DataGridViewX1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewX1.Size = New System.Drawing.Size(693, 327)
        Me.DataGridViewX1.TabIndex = 346
        '
        'M_ID_CL
        '
        Me.M_ID_CL.DataPropertyName = "M_ID"
        Me.M_ID_CL.FillWeight = 52.73566!
        Me.M_ID_CL.HeaderText = ""
        Me.M_ID_CL.Name = "M_ID_CL"
        Me.M_ID_CL.ReadOnly = True
        Me.M_ID_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'M_NAME_CL
        '
        Me.M_NAME_CL.DataPropertyName = "M_NAME"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.M_NAME_CL.DefaultCellStyle = DataGridViewCellStyle10
        Me.M_NAME_CL.FillWeight = 69.86156!
        Me.M_NAME_CL.HeaderText = "الشهر"
        Me.M_NAME_CL.Name = "M_NAME_CL"
        Me.M_NAME_CL.ReadOnly = True
        Me.M_NAME_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'M_FROM_CL
        '
        Me.M_FROM_CL.DataPropertyName = "M_FROM"
        DataGridViewCellStyle11.Format = "d"
        DataGridViewCellStyle11.NullValue = Nothing
        Me.M_FROM_CL.DefaultCellStyle = DataGridViewCellStyle11
        Me.M_FROM_CL.FillWeight = 62.63601!
        Me.M_FROM_CL.HeaderText = "من"
        Me.M_FROM_CL.Name = "M_FROM_CL"
        Me.M_FROM_CL.ReadOnly = True
        Me.M_FROM_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'M_TO_CL
        '
        Me.M_TO_CL.DataPropertyName = "M_TO"
        DataGridViewCellStyle12.Format = "d"
        DataGridViewCellStyle12.NullValue = Nothing
        Me.M_TO_CL.DefaultCellStyle = DataGridViewCellStyle12
        Me.M_TO_CL.FillWeight = 69.86156!
        Me.M_TO_CL.HeaderText = "إلى"
        Me.M_TO_CL.Name = "M_TO_CL"
        Me.M_TO_CL.ReadOnly = True
        Me.M_TO_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Status_CL
        '
        Me.Status_CL.DataPropertyName = "Status"
        Me.Status_CL.FillWeight = 50.0!
        Me.Status_CL.HeaderText = "الحالة"
        Me.Status_CL.Name = "Status_CL"
        Me.Status_CL.ReadOnly = True
        '
        'is_Close_CL
        '
        Me.is_Close_CL.DataPropertyName = "is_Close"
        Me.is_Close_CL.HeaderText = "is_Close"
        Me.is_Close_CL.Name = "is_Close_CL"
        Me.is_Close_CL.ReadOnly = True
        Me.is_Close_CL.Visible = False
        '
        'Open_Btn
        '
        Me.Open_Btn.BackColor = System.Drawing.Color.White
        Me.Open_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Open_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Open_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Open_Btn.Location = New System.Drawing.Point(2, 1)
        Me.Open_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Open_Btn.Name = "Open_Btn"
        Me.Open_Btn.Size = New System.Drawing.Size(147, 31)
        Me.Open_Btn.TabIndex = 650
        Me.Open_Btn.Text = "فتح السنة المحدده ✳️ "
        Me.Open_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Open_Btn.UseVisualStyleBackColor = False
        '
        'YEAR_status_Label
        '
        Me.YEAR_status_Label.BackColor = System.Drawing.Color.PaleGreen
        Me.YEAR_status_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.YEAR_status_Label.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.YEAR_status_Label.ForeColor = System.Drawing.SystemColors.Desktop
        Me.YEAR_status_Label.Location = New System.Drawing.Point(1, 68)
        Me.YEAR_status_Label.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.YEAR_status_Label.Name = "YEAR_status_Label"
        Me.YEAR_status_Label.Size = New System.Drawing.Size(694, 31)
        Me.YEAR_status_Label.TabIndex = 649
        Me.YEAR_status_Label.Text = "سنـــة مفتوحــة"
        Me.YEAR_status_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Close_Btn
        '
        Me.Close_Btn.BackColor = System.Drawing.Color.White
        Me.Close_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Close_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Close_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Close_Btn.Location = New System.Drawing.Point(150, 1)
        Me.Close_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Close_Btn.Name = "Close_Btn"
        Me.Close_Btn.Size = New System.Drawing.Size(152, 31)
        Me.Close_Btn.TabIndex = 648
        Me.Close_Btn.Text = "إقفـال السنـة الحالية 🔒"
        Me.Close_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Close_Btn.UseVisualStyleBackColor = False
        '
        'ADD_Btn
        '
        Me.ADD_Btn.BackColor = System.Drawing.Color.White
        Me.ADD_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ADD_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.ADD_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ADD_Btn.Location = New System.Drawing.Point(2, 455)
        Me.ADD_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.ADD_Btn.Name = "ADD_Btn"
        Me.ADD_Btn.Size = New System.Drawing.Size(327, 37)
        Me.ADD_Btn.TabIndex = 647
        Me.ADD_Btn.Text = "📄  فتح سنة مالية جديدة"
        Me.ADD_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ADD_Btn.UseVisualStyleBackColor = False
        '
        'YEAR_Cm
        '
        Me.YEAR_Cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.YEAR_Cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.YEAR_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.YEAR_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.YEAR_Cm.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.YEAR_Cm.FormattingEnabled = True
        Me.YEAR_Cm.Location = New System.Drawing.Point(531, 3)
        Me.YEAR_Cm.Margin = New System.Windows.Forms.Padding(4)
        Me.YEAR_Cm.Name = "YEAR_Cm"
        Me.YEAR_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.YEAR_Cm.Size = New System.Drawing.Size(103, 27)
        Me.YEAR_Cm.TabIndex = 645
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(640, 7)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(51, 19)
        Me.Label4.TabIndex = 646
        Me.Label4.Text = "السنــة:"
        '
        'Create_Calendar_Btn
        '
        Me.Create_Calendar_Btn.BackColor = System.Drawing.Color.White
        Me.Create_Calendar_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Create_Calendar_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Create_Calendar_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Create_Calendar_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Create_Calendar_Btn.Location = New System.Drawing.Point(330, 455)
        Me.Create_Calendar_Btn.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Create_Calendar_Btn.Name = "Create_Calendar_Btn"
        Me.Create_Calendar_Btn.Size = New System.Drawing.Size(365, 37)
        Me.Create_Calendar_Btn.TabIndex = 644
        Me.Create_Calendar_Btn.Text = "📅 إعــادة جدولة التقويم السنوي"
        Me.Create_Calendar_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Create_Calendar_Btn.UseVisualStyleBackColor = False
        '
        'Back_btn
        '
        Me.Back_btn.BackColor = System.Drawing.Color.White
        Me.Back_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Back_btn.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Back_btn.Location = New System.Drawing.Point(2, 493)
        Me.Back_btn.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Back_btn.Name = "Back_btn"
        Me.Back_btn.Size = New System.Drawing.Size(693, 38)
        Me.Back_btn.TabIndex = 643
        Me.Back_btn.Text = "عودة  ↩️"
        Me.Back_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Back_btn.UseVisualStyleBackColor = False
        '
        'ARCHIVE_Label
        '
        Me.ARCHIVE_Label.AutoSize = True
        Me.ARCHIVE_Label.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.ARCHIVE_Label.ForeColor = System.Drawing.Color.DarkRed
        Me.ARCHIVE_Label.Location = New System.Drawing.Point(465, 431)
        Me.ARCHIVE_Label.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ARCHIVE_Label.Name = "ARCHIVE_Label"
        Me.ARCHIVE_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ARCHIVE_Label.Size = New System.Drawing.Size(33, 19)
        Me.ARCHIVE_Label.TabIndex = 655
        Me.ARCHIVE_Label.Text = "----"
        '
        'NONE_ARCHIVE_Label
        '
        Me.NONE_ARCHIVE_Label.AutoSize = True
        Me.NONE_ARCHIVE_Label.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.NONE_ARCHIVE_Label.ForeColor = System.Drawing.Color.DarkGreen
        Me.NONE_ARCHIVE_Label.Location = New System.Drawing.Point(97, 431)
        Me.NONE_ARCHIVE_Label.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.NONE_ARCHIVE_Label.Name = "NONE_ARCHIVE_Label"
        Me.NONE_ARCHIVE_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NONE_ARCHIVE_Label.Size = New System.Drawing.Size(33, 19)
        Me.NONE_ARCHIVE_Label.TabIndex = 656
        Me.NONE_ARCHIVE_Label.Text = "----"
        '
        'MONTHS_CALENDR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(695, 532)
        Me.Controls.Add(Me.NONE_ARCHIVE_Label)
        Me.Controls.Add(Me.ARCHIVE_Label)
        Me.Controls.Add(Me.RETUTN_YEAR_FROM_ARCHIVE_Btn)
        Me.Controls.Add(Me.MOVE_YEAR_TO_ARCHIVE_Btn)
        Me.Controls.Add(Me.Select_Btn)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Open_Btn)
        Me.Controls.Add(Me.YEAR_status_Label)
        Me.Controls.Add(Me.Close_Btn)
        Me.Controls.Add(Me.ADD_Btn)
        Me.Controls.Add(Me.YEAR_Cm)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Create_Calendar_Btn)
        Me.Controls.Add(Me.Back_btn)
        Me.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MONTHS_CALENDR"
        Me.Text = "السنــــة الماليـــة"
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridViewX1 As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Back_btn As System.Windows.Forms.Button
    Friend WithEvents Create_Calendar_Btn As Button
    Friend WithEvents YEAR_Cm As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ADD_Btn As Button
    Friend WithEvents Close_Btn As Button
    Friend WithEvents YEAR_status_Label As Label
    Friend WithEvents Open_Btn As Button
    Friend WithEvents M_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents M_NAME_CL As DataGridViewTextBoxColumn
    Friend WithEvents M_FROM_CL As DataGridViewTextBoxColumn
    Friend WithEvents M_TO_CL As DataGridViewTextBoxColumn
    Friend WithEvents Status_CL As DataGridViewTextBoxColumn
    Friend WithEvents is_Close_CL As DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Select_Btn As Button
    Friend WithEvents MOVE_YEAR_TO_ARCHIVE_Btn As Button
    Friend WithEvents RETUTN_YEAR_FROM_ARCHIVE_Btn As Button
    Friend WithEvents ARCHIVE_Label As Label
    Friend WithEvents NONE_ARCHIVE_Label As Label
End Class
