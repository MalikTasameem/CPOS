<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_Notes_Valid_Form
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_Notes_Valid_Form))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_Num_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Valid_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Case_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Days_Left_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Delete_Alert = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        Me.DateTimePicker_From = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TypeComboBox = New System.Windows.Forms.ComboBox()
        Me.Add_Valid_Btn = New System.Windows.Forms.Button()
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("JF Flat", 15.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(2, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(768, 46)
        Me.Label3.TabIndex = 658
        Me.Label3.Text = "قائمة صلاحيات الأصنـــاف"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataGridViewX
        '
        Me.DataGridViewX.AllowUserToAddRows = False
        Me.DataGridViewX.AllowUserToDeleteRows = False
        Me.DataGridViewX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewX.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("JF Flat", 9.5!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.item_name_CL, Me.IM_Num_CL, Me.D_Valid_CL, Me.Case_CL, Me.Days_Left_CL, Me.Delete_Alert})
        Me.DataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("JF Flat", 9.5!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX.Location = New System.Drawing.Point(2, 78)
        Me.DataGridViewX.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.DataGridViewX.MultiSelect = False
        Me.DataGridViewX.Name = "DataGridViewX"
        Me.DataGridViewX.ReadOnly = True
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewX.RowTemplate.Height = 35
        Me.DataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewX.Size = New System.Drawing.Size(769, 520)
        Me.DataGridViewX.TabIndex = 649
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "رقم الحالة"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.FillWeight = 119.5432!
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.MinimumWidth = 100
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        Me.item_name_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.item_name_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'IM_Num_CL
        '
        Me.IM_Num_CL.DataPropertyName = "IM_Num"
        Me.IM_Num_CL.HeaderText = "الرقم"
        Me.IM_Num_CL.Name = "IM_Num_CL"
        Me.IM_Num_CL.ReadOnly = True
        Me.IM_Num_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.IM_Num_CL.Visible = False
        '
        'D_Valid_CL
        '
        Me.D_Valid_CL.DataPropertyName = "Valid_Date"
        DataGridViewCellStyle2.Format = "d"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.D_Valid_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.D_Valid_CL.HeaderText = "الصلاحية"
        Me.D_Valid_CL.Name = "D_Valid_CL"
        Me.D_Valid_CL.ReadOnly = True
        Me.D_Valid_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Case_CL
        '
        Me.Case_CL.HeaderText = "الحالة"
        Me.Case_CL.Name = "Case_CL"
        Me.Case_CL.ReadOnly = True
        Me.Case_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Days_Left_CL
        '
        Me.Days_Left_CL.DataPropertyName = "Days_Left"
        Me.Days_Left_CL.HeaderText = "Days_Left"
        Me.Days_Left_CL.Name = "Days_Left_CL"
        Me.Days_Left_CL.ReadOnly = True
        Me.Days_Left_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Days_Left_CL.Visible = False
        '
        'Delete_Alert
        '
        Me.Delete_Alert.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Delete_Alert.HeaderText = ""
        Me.Delete_Alert.Name = "Delete_Alert"
        Me.Delete_Alert.ReadOnly = True
        Me.Delete_Alert.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Delete_Alert.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Delete_Alert.Text = "حذف"
        Me.Delete_Alert.UseColumnTextForButtonValue = True
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(384, 51)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CMSearchTextBox.Size = New System.Drawing.Size(386, 25)
        Me.CMSearchTextBox.TabIndex = 650
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateTimePicker_From
        '
        Me.DateTimePicker_From.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DateTimePicker_From.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker_From.Enabled = False
        Me.DateTimePicker_From.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_From.Location = New System.Drawing.Point(101, 50)
        Me.DateTimePicker_From.Name = "DateTimePicker_From"
        Me.DateTimePicker_From.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateTimePicker_From.Size = New System.Drawing.Size(92, 26)
        Me.DateTimePicker_From.TabIndex = 664
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(2, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(93, 23)
        Me.Label4.TabIndex = 665
        Me.Label4.Text = "الصلاحية قبل"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(2, 607)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(93, 44)
        Me.ExitFormButton.TabIndex = 666
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'TypeComboBox
        '
        Me.TypeComboBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TypeComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TypeComboBox.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.TypeComboBox.ForeColor = System.Drawing.Color.Black
        Me.TypeComboBox.FormattingEnabled = True
        Me.TypeComboBox.Location = New System.Drawing.Point(196, 50)
        Me.TypeComboBox.Name = "TypeComboBox"
        Me.TypeComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TypeComboBox.Size = New System.Drawing.Size(186, 26)
        Me.TypeComboBox.TabIndex = 677
        '
        'Add_Valid_Btn
        '
        Me.Add_Valid_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Add_Valid_Btn.BackColor = System.Drawing.Color.White
        Me.Add_Valid_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Add_Valid_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Add_Valid_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Add_Valid_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Add_Valid_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Add_Valid_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Add_Valid_Btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Add_Valid_Btn.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.Add_Valid_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Add_Valid_Btn.Location = New System.Drawing.Point(511, 607)
        Me.Add_Valid_Btn.Name = "Add_Valid_Btn"
        Me.Add_Valid_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Add_Valid_Btn.Size = New System.Drawing.Size(259, 44)
        Me.Add_Valid_Btn.TabIndex = 715
        Me.Add_Valid_Btn.TabStop = False
        Me.Add_Valid_Btn.Text = "إضافة صنف للقائمة"
        Me.Add_Valid_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Add_Valid_Btn.UseVisualStyleBackColor = False
        '
        'IM_Notes_Valid_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(771, 652)
        Me.Controls.Add(Me.Add_Valid_Btn)
        Me.Controls.Add(Me.TypeComboBox)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePicker_From)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DataGridViewX)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Font = New System.Drawing.Font("JF Flat", 9.5!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Name = "IM_Notes_Valid_Form"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "صلاحية الأصناف"
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents DataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents TypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_Num_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Valid_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Case_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Days_Left_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Delete_Alert As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Add_Valid_Btn As System.Windows.Forms.Button
End Class
