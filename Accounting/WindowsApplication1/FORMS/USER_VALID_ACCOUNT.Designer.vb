<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class USER_VALID_ACCOUNT
    'Inherits System.Windows.Forms.Form
    Inherits Base_Form

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
        Me.CircularPanel = New System.Windows.Forms.Panel()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.ALL_DataGridView = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.REMOVE_BTN = New System.Windows.Forms.Button()
        Me.ADD_Btn = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.USER_Search_By_Acc_Name_txt = New System.Windows.Forms.TextBox()
        Me.USER_Search_By_Acc_Code_txt = New System.Windows.Forms.TextBox()
        Me.USER_DataGridView = New Zuby.ADGV.AdvancedDataGridView()
        Me.ALL_Search_By_Acc_Name_txt = New System.Windows.Forms.TextBox()
        Me.ALL_Search_By_Acc_Code_txt = New System.Windows.Forms.TextBox()
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        Me.NameUserListBox = New System.Windows.Forms.ListBox()
        Me.chkAllowAllAccounts = New System.Windows.Forms.CheckBox()
        Me.CircularPanel.SuspendLayout()
        CType(Me.ALL_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.USER_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CircularPanel
        '
        Me.CircularPanel.Controls.Add(Me.CircularProgressControl1)
        Me.CircularPanel.Location = New System.Drawing.Point(1, 704)
        Me.CircularPanel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CircularPanel.Name = "CircularPanel"
        Me.CircularPanel.Size = New System.Drawing.Size(876, 39)
        Me.CircularPanel.TabIndex = 673
        '
        'CircularProgressControl1
        '
        Me.CircularProgressControl1.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularProgressControl1.Interval = 60
        Me.CircularProgressControl1.Location = New System.Drawing.Point(0, 0)
        Me.CircularProgressControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CircularProgressControl1.MinimumSize = New System.Drawing.Size(28, 33)
        Me.CircularProgressControl1.Name = "CircularProgressControl1"
        Me.CircularProgressControl1.Rotation = Accounting.CircularProgressControl.Direction.CLOCKWISE
        Me.CircularProgressControl1.Size = New System.Drawing.Size(876, 39)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 672
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        '
        'ALL_DataGridView
        '
        Me.ALL_DataGridView.AllowUserToAddRows = False
        Me.ALL_DataGridView.AllowUserToDeleteRows = False
        Me.ALL_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.ALL_DataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.ALL_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ALL_DataGridView.DefaultCellStyle = DataGridViewCellStyle1
        Me.ALL_DataGridView.Location = New System.Drawing.Point(490, 72)
        Me.ALL_DataGridView.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ALL_DataGridView.MultiSelect = False
        Me.ALL_DataGridView.Name = "ALL_DataGridView"
        Me.ALL_DataGridView.ReadOnly = True
        Me.ALL_DataGridView.RowHeadersVisible = False
        Me.ALL_DataGridView.RowTemplate.Height = 30
        Me.ALL_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ALL_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ALL_DataGridView.Size = New System.Drawing.Size(427, 600)
        Me.ALL_DataGridView.TabIndex = 671
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(2, 636)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(431, 37)
        Me.Button1.TabIndex = 670
        Me.Button1.Text = "➕  إضافـة كل الحسابات"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'REMOVE_BTN
        '
        Me.REMOVE_BTN.BackColor = System.Drawing.Color.White
        Me.REMOVE_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.REMOVE_BTN.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold)
        Me.REMOVE_BTN.Location = New System.Drawing.Point(435, 373)
        Me.REMOVE_BTN.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.REMOVE_BTN.Name = "REMOVE_BTN"
        Me.REMOVE_BTN.Size = New System.Drawing.Size(53, 300)
        Me.REMOVE_BTN.TabIndex = 669
        Me.REMOVE_BTN.Text = "❌"
        Me.REMOVE_BTN.UseVisualStyleBackColor = False
        '
        'ADD_Btn
        '
        Me.ADD_Btn.BackColor = System.Drawing.Color.White
        Me.ADD_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADD_Btn.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold)
        Me.ADD_Btn.Location = New System.Drawing.Point(435, 73)
        Me.ADD_Btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ADD_Btn.Name = "ADD_Btn"
        Me.ADD_Btn.Size = New System.Drawing.Size(53, 300)
        Me.ADD_Btn.TabIndex = 668
        Me.ADD_Btn.Text = "➕"
        Me.ADD_Btn.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(432, 44)
        Me.Label2.TabIndex = 667
        Me.Label2.Text = "قائمة الحسابات للمستخدم"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(490, 1)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(427, 44)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "قائمة كل الحسابات"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TITLE_txt
        '
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TITLE_txt.Location = New System.Drawing.Point(921, 1)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(257, 44)
        Me.TITLE_txt.TabIndex = 665
        Me.TITLE_txt.Text = "قائمة المستخدمين"
        Me.TITLE_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'USER_Search_By_Acc_Name_txt
        '
        Me.USER_Search_By_Acc_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.USER_Search_By_Acc_Name_txt.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.USER_Search_By_Acc_Name_txt.Location = New System.Drawing.Point(141, 46)
        Me.USER_Search_By_Acc_Name_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.USER_Search_By_Acc_Name_txt.Name = "USER_Search_By_Acc_Name_txt"
        Me.USER_Search_By_Acc_Name_txt.Size = New System.Drawing.Size(292, 24)
        Me.USER_Search_By_Acc_Name_txt.TabIndex = 663
        '
        'USER_Search_By_Acc_Code_txt
        '
        Me.USER_Search_By_Acc_Code_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.USER_Search_By_Acc_Code_txt.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.USER_Search_By_Acc_Code_txt.Location = New System.Drawing.Point(1, 46)
        Me.USER_Search_By_Acc_Code_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.USER_Search_By_Acc_Code_txt.Name = "USER_Search_By_Acc_Code_txt"
        Me.USER_Search_By_Acc_Code_txt.Size = New System.Drawing.Size(139, 24)
        Me.USER_Search_By_Acc_Code_txt.TabIndex = 664
        '
        'USER_DataGridView
        '
        Me.USER_DataGridView.AllowUserToAddRows = False
        Me.USER_DataGridView.AllowUserToDeleteRows = False
        Me.USER_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.USER_DataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.USER_DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.USER_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.BlanchedAlmond
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.USER_DataGridView.DefaultCellStyle = DataGridViewCellStyle3
        Me.USER_DataGridView.FilterAndSortEnabled = True
        Me.USER_DataGridView.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.USER_DataGridView.Location = New System.Drawing.Point(1, 71)
        Me.USER_DataGridView.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.USER_DataGridView.MultiSelect = False
        Me.USER_DataGridView.Name = "USER_DataGridView"
        Me.USER_DataGridView.ReadOnly = True
        Me.USER_DataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.USER_DataGridView.RowTemplate.Height = 35
        Me.USER_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.USER_DataGridView.Size = New System.Drawing.Size(432, 564)
        Me.USER_DataGridView.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.USER_DataGridView.TabIndex = 662
        '
        'ALL_Search_By_Acc_Name_txt
        '
        Me.ALL_Search_By_Acc_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ALL_Search_By_Acc_Name_txt.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.ALL_Search_By_Acc_Name_txt.Location = New System.Drawing.Point(664, 46)
        Me.ALL_Search_By_Acc_Name_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ALL_Search_By_Acc_Name_txt.Name = "ALL_Search_By_Acc_Name_txt"
        Me.ALL_Search_By_Acc_Name_txt.Size = New System.Drawing.Size(253, 24)
        Me.ALL_Search_By_Acc_Name_txt.TabIndex = 660
        '
        'ALL_Search_By_Acc_Code_txt
        '
        Me.ALL_Search_By_Acc_Code_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ALL_Search_By_Acc_Code_txt.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.ALL_Search_By_Acc_Code_txt.Location = New System.Drawing.Point(490, 46)
        Me.ALL_Search_By_Acc_Code_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ALL_Search_By_Acc_Code_txt.Name = "ALL_Search_By_Acc_Code_txt"
        Me.ALL_Search_By_Acc_Code_txt.Size = New System.Drawing.Size(173, 24)
        Me.ALL_Search_By_Acc_Code_txt.TabIndex = 661
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CMSearchTextBox.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.CMSearchTextBox.Location = New System.Drawing.Point(921, 46)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.Size = New System.Drawing.Size(257, 24)
        Me.CMSearchTextBox.TabIndex = 658
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NameUserListBox
        '
        Me.NameUserListBox.BackColor = System.Drawing.Color.White
        Me.NameUserListBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NameUserListBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameUserListBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.NameUserListBox.FormattingEnabled = True
        Me.NameUserListBox.ItemHeight = 21
        Me.NameUserListBox.Location = New System.Drawing.Point(921, 71)
        Me.NameUserListBox.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.NameUserListBox.Name = "NameUserListBox"
        Me.NameUserListBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NameUserListBox.Size = New System.Drawing.Size(257, 592)
        Me.NameUserListBox.TabIndex = 657
        '
        'chkAllowAllAccounts
        '
        Me.chkAllowAllAccounts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAllowAllAccounts.AutoSize = True
        Me.chkAllowAllAccounts.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.chkAllowAllAccounts.Location = New System.Drawing.Point(553, 681)
        Me.chkAllowAllAccounts.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkAllowAllAccounts.Name = "chkAllowAllAccounts"
        Me.chkAllowAllAccounts.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkAllowAllAccounts.Size = New System.Drawing.Size(153, 23)
        Me.chkAllowAllAccounts.TabIndex = 674
        Me.chkAllowAllAccounts.Text = "السماح بكل الحسابات"
        Me.chkAllowAllAccounts.UseVisualStyleBackColor = True
        '
        'USER_VALID_ACCOUNT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1180, 743)
        Me.Controls.Add(Me.chkAllowAllAccounts)
        Me.Controls.Add(Me.CircularPanel)
        Me.Controls.Add(Me.ALL_DataGridView)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.REMOVE_BTN)
        Me.Controls.Add(Me.ADD_Btn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TITLE_txt)
        Me.Controls.Add(Me.USER_Search_By_Acc_Name_txt)
        Me.Controls.Add(Me.USER_Search_By_Acc_Code_txt)
        Me.Controls.Add(Me.USER_DataGridView)
        Me.Controls.Add(Me.ALL_Search_By_Acc_Name_txt)
        Me.Controls.Add(Me.ALL_Search_By_Acc_Code_txt)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Controls.Add(Me.NameUserListBox)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "USER_VALID_ACCOUNT"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "صلاحية الحسابات للمستخدمين"
        Me.CircularPanel.ResumeLayout(False)
        CType(Me.ALL_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.USER_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CMSearchTextBox As TextBox
    Friend WithEvents NameUserListBox As ListBox
    Friend WithEvents ALL_Search_By_Acc_Name_txt As TextBox
    Friend WithEvents ALL_Search_By_Acc_Code_txt As TextBox
    Friend WithEvents USER_DataGridView As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents USER_Search_By_Acc_Name_txt As TextBox
    Friend WithEvents USER_Search_By_Acc_Code_txt As TextBox
    Friend WithEvents TITLE_txt As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents REMOVE_BTN As Button
    Friend WithEvents ADD_Btn As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ALL_DataGridView As DataGridView
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents CircularPanel As Panel
    Friend WithEvents chkAllowAllAccounts As CheckBox
End Class
