<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class users
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(users))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PassUserTextBox = New System.Windows.Forms.TextBox()
        Me.NameUserListBox = New System.Windows.Forms.ListBox()
        Me.NameUserTextBox = New System.Windows.Forms.TextBox()
        Me.ErrorProvideName = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ErrorProvidePass = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Valid_Cm = New System.Windows.Forms.ComboBox()
        Me.FieldsPanel = New System.Windows.Forms.Panel()
        Me.Treasury_ComboBox = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ShowPassButton = New System.Windows.Forms.Button()
        Me.Is_Allow_CB = New System.Windows.Forms.CheckBox()
        Me.TablePanel = New System.Windows.Forms.Panel()
        Me.Default_AG_cm = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CostmerScreen_CB = New System.Windows.Forms.CheckBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.ChangePassButton = New System.Windows.Forms.Button()
        Me.DeleteUserButton = New System.Windows.Forms.Button()
        Me.EditUserButton = New System.Windows.Forms.Button()
        Me.AddUserButton = New System.Windows.Forms.Button()
        Me.NewUserButton = New System.Windows.Forms.Button()
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        CType(Me.ErrorProvideName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvidePass, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FieldsPanel.SuspendLayout()
        Me.TablePanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(233, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 19)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "اسم المستخــدم"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(233, 31)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 19)
        Me.Label2.TabIndex = 71
        Me.Label2.Text = "الرقم السري"
        '
        'PassUserTextBox
        '
        Me.PassUserTextBox.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.PassUserTextBox.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.PassUserTextBox.ForeColor = System.Drawing.Color.Navy
        Me.PassUserTextBox.Location = New System.Drawing.Point(70, 28)
        Me.PassUserTextBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PassUserTextBox.MaxLength = 20
        Me.PassUserTextBox.Name = "PassUserTextBox"
        Me.PassUserTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PassUserTextBox.Size = New System.Drawing.Size(160, 25)
        Me.PassUserTextBox.TabIndex = 69
        Me.PassUserTextBox.UseSystemPasswordChar = True
        '
        'NameUserListBox
        '
        Me.NameUserListBox.BackColor = System.Drawing.Color.White
        Me.NameUserListBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NameUserListBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameUserListBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.NameUserListBox.FormattingEnabled = True
        Me.NameUserListBox.ItemHeight = 21
        Me.NameUserListBox.Location = New System.Drawing.Point(1, 30)
        Me.NameUserListBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.NameUserListBox.Name = "NameUserListBox"
        Me.NameUserListBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NameUserListBox.Size = New System.Drawing.Size(302, 571)
        Me.NameUserListBox.TabIndex = 75
        '
        'NameUserTextBox
        '
        Me.NameUserTextBox.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.NameUserTextBox.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.NameUserTextBox.ForeColor = System.Drawing.Color.Navy
        Me.NameUserTextBox.Location = New System.Drawing.Point(31, 1)
        Me.NameUserTextBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.NameUserTextBox.MaxLength = 200
        Me.NameUserTextBox.Name = "NameUserTextBox"
        Me.NameUserTextBox.Size = New System.Drawing.Size(199, 25)
        Me.NameUserTextBox.TabIndex = 68
        Me.NameUserTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ErrorProvideName
        '
        Me.ErrorProvideName.ContainerControl = Me
        Me.ErrorProvideName.RightToLeft = True
        '
        'ErrorProvidePass
        '
        Me.ErrorProvidePass.ContainerControl = Me
        Me.ErrorProvidePass.RightToLeft = True
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(235, 60)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 19)
        Me.Label4.TabIndex = 82
        Me.Label4.Text = "الصلاحيات"
        '
        'Valid_Cm
        '
        Me.Valid_Cm.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Valid_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Valid_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Valid_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Valid_Cm.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Valid_Cm.FormattingEnabled = True
        Me.Valid_Cm.Location = New System.Drawing.Point(31, 57)
        Me.Valid_Cm.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Valid_Cm.Name = "Valid_Cm"
        Me.Valid_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Valid_Cm.Size = New System.Drawing.Size(199, 25)
        Me.Valid_Cm.TabIndex = 399
        '
        'FieldsPanel
        '
        Me.FieldsPanel.Controls.Add(Me.Treasury_ComboBox)
        Me.FieldsPanel.Controls.Add(Me.Label6)
        Me.FieldsPanel.Controls.Add(Me.ShowPassButton)
        Me.FieldsPanel.Controls.Add(Me.Is_Allow_CB)
        Me.FieldsPanel.Controls.Add(Me.TablePanel)
        Me.FieldsPanel.Controls.Add(Me.Label1)
        Me.FieldsPanel.Controls.Add(Me.Valid_Cm)
        Me.FieldsPanel.Controls.Add(Me.NameUserTextBox)
        Me.FieldsPanel.Controls.Add(Me.Label2)
        Me.FieldsPanel.Controls.Add(Me.PassUserTextBox)
        Me.FieldsPanel.Controls.Add(Me.Label4)
        Me.FieldsPanel.Enabled = False
        Me.FieldsPanel.Location = New System.Drawing.Point(306, 1)
        Me.FieldsPanel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.FieldsPanel.Name = "FieldsPanel"
        Me.FieldsPanel.Size = New System.Drawing.Size(349, 279)
        Me.FieldsPanel.TabIndex = 400
        '
        'Treasury_ComboBox
        '
        Me.Treasury_ComboBox.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Treasury_ComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Treasury_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Treasury_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Treasury_ComboBox.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Treasury_ComboBox.FormattingEnabled = True
        Me.Treasury_ComboBox.Location = New System.Drawing.Point(31, 88)
        Me.Treasury_ComboBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Treasury_ComboBox.Name = "Treasury_ComboBox"
        Me.Treasury_ComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Treasury_ComboBox.Size = New System.Drawing.Size(199, 25)
        Me.Treasury_ComboBox.TabIndex = 406
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(235, 91)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 19)
        Me.Label6.TabIndex = 405
        Me.Label6.Text = "الخزينة"
        '
        'ShowPassButton
        '
        Me.ShowPassButton.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ShowPassButton.BackColor = System.Drawing.SystemColors.Window
        Me.ShowPassButton.BackgroundImage = Global.resturant.My.Resources.Resources.show_hide_password
        Me.ShowPassButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ShowPassButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose
        Me.ShowPassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ShowPassButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!)
        Me.ShowPassButton.ForeColor = System.Drawing.Color.DarkRed
        Me.ShowPassButton.Location = New System.Drawing.Point(31, 28)
        Me.ShowPassButton.Name = "ShowPassButton"
        Me.ShowPassButton.Size = New System.Drawing.Size(38, 25)
        Me.ShowPassButton.TabIndex = 404
        Me.ShowPassButton.UseVisualStyleBackColor = False
        '
        'Is_Allow_CB
        '
        Me.Is_Allow_CB.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Is_Allow_CB.AutoSize = True
        Me.Is_Allow_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Is_Allow_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Is_Allow_CB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Is_Allow_CB.Location = New System.Drawing.Point(191, 251)
        Me.Is_Allow_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Is_Allow_CB.Name = "Is_Allow_CB"
        Me.Is_Allow_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Is_Allow_CB.Size = New System.Drawing.Size(152, 23)
        Me.Is_Allow_CB.TabIndex = 403
        Me.Is_Allow_CB.Text = "إمكانية الدخول للنظام"
        Me.Is_Allow_CB.UseVisualStyleBackColor = True
        '
        'TablePanel
        '
        Me.TablePanel.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TablePanel.Controls.Add(Me.Default_AG_cm)
        Me.TablePanel.Controls.Add(Me.Label5)
        Me.TablePanel.Controls.Add(Me.CostmerScreen_CB)
        Me.TablePanel.Location = New System.Drawing.Point(32, 126)
        Me.TablePanel.Name = "TablePanel"
        Me.TablePanel.Size = New System.Drawing.Size(314, 117)
        Me.TablePanel.TabIndex = 402
        '
        'Default_AG_cm
        '
        Me.Default_AG_cm.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Default_AG_cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Default_AG_cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Default_AG_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Default_AG_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Default_AG_cm.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Default_AG_cm.FormattingEnabled = True
        Me.Default_AG_cm.Location = New System.Drawing.Point(4, 4)
        Me.Default_AG_cm.Name = "Default_AG_cm"
        Me.Default_AG_cm.Size = New System.Drawing.Size(196, 25)
        Me.Default_AG_cm.TabIndex = 263
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(205, 6)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 19)
        Me.Label5.TabIndex = 405
        Me.Label5.Text = "ربط بحساب"
        '
        'CostmerScreen_CB
        '
        Me.CostmerScreen_CB.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.CostmerScreen_CB.AutoSize = True
        Me.CostmerScreen_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CostmerScreen_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CostmerScreen_CB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CostmerScreen_CB.Location = New System.Drawing.Point(6, 32)
        Me.CostmerScreen_CB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CostmerScreen_CB.Name = "CostmerScreen_CB"
        Me.CostmerScreen_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CostmerScreen_CB.Size = New System.Drawing.Size(301, 23)
        Me.CostmerScreen_CB.TabIndex = 403
        Me.CostmerScreen_CB.Text = "إستخدام المستخدم للدخول لشاشة عرض الزبائن"
        Me.CostmerScreen_CB.UseVisualStyleBackColor = True
        Me.CostmerScreen_CB.Visible = False
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
        Me.ExitFormButton.Location = New System.Drawing.Point(659, 561)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(136, 40)
        Me.ExitFormButton.TabIndex = 655
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'ChangePassButton
        '
        Me.ChangePassButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ChangePassButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChangePassButton.Enabled = False
        Me.ChangePassButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ChangePassButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ChangePassButton.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.ChangePassButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChangePassButton.Location = New System.Drawing.Point(658, 131)
        Me.ChangePassButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ChangePassButton.Name = "ChangePassButton"
        Me.ChangePassButton.Size = New System.Drawing.Size(137, 40)
        Me.ChangePassButton.TabIndex = 409
        Me.ChangePassButton.Text = "تغيير كلمة المرور"
        Me.ChangePassButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ChangePassButton.UseVisualStyleBackColor = False
        '
        'DeleteUserButton
        '
        Me.DeleteUserButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteUserButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteUserButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteUserButton.Enabled = False
        Me.DeleteUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DeleteUserButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DeleteUserButton.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.DeleteUserButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteUserButton.Location = New System.Drawing.Point(658, 173)
        Me.DeleteUserButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DeleteUserButton.Name = "DeleteUserButton"
        Me.DeleteUserButton.Size = New System.Drawing.Size(137, 40)
        Me.DeleteUserButton.TabIndex = 79
        Me.DeleteUserButton.Text = "حــذف"
        Me.DeleteUserButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteUserButton.UseVisualStyleBackColor = False
        '
        'EditUserButton
        '
        Me.EditUserButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditUserButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditUserButton.Enabled = False
        Me.EditUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EditUserButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.EditUserButton.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.EditUserButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EditUserButton.Location = New System.Drawing.Point(658, 89)
        Me.EditUserButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EditUserButton.Name = "EditUserButton"
        Me.EditUserButton.Size = New System.Drawing.Size(137, 40)
        Me.EditUserButton.TabIndex = 78
        Me.EditUserButton.Text = "تعديل"
        Me.EditUserButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditUserButton.UseVisualStyleBackColor = False
        '
        'AddUserButton
        '
        Me.AddUserButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.AddUserButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AddUserButton.Enabled = False
        Me.AddUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddUserButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.AddUserButton.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.AddUserButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddUserButton.Location = New System.Drawing.Point(658, 46)
        Me.AddUserButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.AddUserButton.Name = "AddUserButton"
        Me.AddUserButton.Size = New System.Drawing.Size(137, 40)
        Me.AddUserButton.TabIndex = 77
        Me.AddUserButton.Text = "حـفظ "
        Me.AddUserButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AddUserButton.UseVisualStyleBackColor = False
        '
        'NewUserButton
        '
        Me.NewUserButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NewUserButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NewUserButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.NewUserButton.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.NewUserButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NewUserButton.Location = New System.Drawing.Point(658, 3)
        Me.NewUserButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.NewUserButton.Name = "NewUserButton"
        Me.NewUserButton.Size = New System.Drawing.Size(137, 40)
        Me.NewUserButton.TabIndex = 76
        Me.NewUserButton.Text = "جديــد"
        Me.NewUserButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NewUserButton.UseVisualStyleBackColor = False
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 11.5!, System.Drawing.FontStyle.Bold)
        Me.CMSearchTextBox.Location = New System.Drawing.Point(1, 1)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.Size = New System.Drawing.Size(302, 28)
        Me.CMSearchTextBox.TabIndex = 656
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'users
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(797, 601)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.ChangePassButton)
        Me.Controls.Add(Me.FieldsPanel)
        Me.Controls.Add(Me.DeleteUserButton)
        Me.Controls.Add(Me.EditUserButton)
        Me.Controls.Add(Me.AddUserButton)
        Me.Controls.Add(Me.NewUserButton)
        Me.Controls.Add(Me.NameUserListBox)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "users"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "المستخدمين"
        CType(Me.ErrorProvideName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvidePass, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FieldsPanel.ResumeLayout(False)
        Me.FieldsPanel.PerformLayout()
        Me.TablePanel.ResumeLayout(False)
        Me.TablePanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DeleteUserButton As System.Windows.Forms.Button
    Friend WithEvents EditUserButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AddUserButton As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NewUserButton As System.Windows.Forms.Button
    Friend WithEvents PassUserTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NameUserListBox As System.Windows.Forms.ListBox
    Friend WithEvents NameUserTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ErrorProvideName As System.Windows.Forms.ErrorProvider
    Friend WithEvents ErrorProvidePass As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Valid_Cm As System.Windows.Forms.ComboBox
    Friend WithEvents FieldsPanel As System.Windows.Forms.Panel
    Friend WithEvents ChangePassButton As System.Windows.Forms.Button
    Friend WithEvents TablePanel As System.Windows.Forms.Panel
    Friend WithEvents CostmerScreen_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Default_AG_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Is_Allow_CB As System.Windows.Forms.CheckBox
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents ShowPassButton As System.Windows.Forms.Button
    Friend WithEvents Treasury_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
End Class
