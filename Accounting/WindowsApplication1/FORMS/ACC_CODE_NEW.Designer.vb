<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ACC_CODE_NEW
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
        Me.components = New System.ComponentModel.Container()
        Me.ACC_PARENT_Txt = New System.Windows.Forms.TextBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.is_Auto_Code_CB = New System.Windows.Forms.CheckBox()
        Me.Label_info = New System.Windows.Forms.Label()
        Me.Label_CODE = New System.Windows.Forms.Label()
        Me.ACC_CODE = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ACC_NAME = New System.Windows.Forms.TextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.ACC_CODE_ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ACC_NAME_ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.ACC_CODE_ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ACC_NAME_ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ACC_PARENT_Txt
        '
        Me.ACC_PARENT_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ACC_PARENT_Txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ACC_PARENT_Txt.Location = New System.Drawing.Point(79, 121)
        Me.ACC_PARENT_Txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ACC_PARENT_Txt.Name = "ACC_PARENT_Txt"
        Me.ACC_PARENT_Txt.ReadOnly = True
        Me.ACC_PARENT_Txt.Size = New System.Drawing.Size(117, 29)
        Me.ACC_PARENT_Txt.TabIndex = 92
        Me.ACC_PARENT_Txt.WordWrap = False
        '
        'Button6
        '
        Me.Button6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button6.Location = New System.Drawing.Point(1, 198)
        Me.Button6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(581, 50)
        Me.Button6.TabIndex = 91
        Me.Button6.Text = "أضــف الحســاب   ✔️     Enter"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'is_Auto_Code_CB
        '
        Me.is_Auto_Code_CB.AutoSize = True
        Me.is_Auto_Code_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_Auto_Code_CB.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.is_Auto_Code_CB.Location = New System.Drawing.Point(4, 125)
        Me.is_Auto_Code_CB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.is_Auto_Code_CB.Name = "is_Auto_Code_CB"
        Me.is_Auto_Code_CB.Size = New System.Drawing.Size(70, 23)
        Me.is_Auto_Code_CB.TabIndex = 90
        Me.is_Auto_Code_CB.Text = "كود آلـي"
        Me.is_Auto_Code_CB.UseVisualStyleBackColor = True
        '
        'Label_info
        '
        Me.Label_info.AutoSize = True
        Me.Label_info.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label_info.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label_info.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_info.Location = New System.Drawing.Point(457, 0)
        Me.Label_info.Name = "Label_info"
        Me.Label_info.Size = New System.Drawing.Size(128, 18)
        Me.Label_info.TabIndex = 89
        Me.Label_info.Text = "معلومات الحساب"
        '
        'Label_CODE
        '
        Me.Label_CODE.AutoSize = True
        Me.Label_CODE.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label_CODE.Location = New System.Drawing.Point(323, 128)
        Me.Label_CODE.Name = "Label_CODE"
        Me.Label_CODE.Size = New System.Drawing.Size(84, 17)
        Me.Label_CODE.TabIndex = 88
        Me.Label_CODE.Text = "كود الحساب:"
        '
        'ACC_CODE
        '
        Me.ACC_CODE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ACC_CODE.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ACC_CODE.Location = New System.Drawing.Point(197, 121)
        Me.ACC_CODE.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ACC_CODE.Name = "ACC_CODE"
        Me.ACC_CODE.Size = New System.Drawing.Size(123, 29)
        Me.ACC_CODE.TabIndex = 87
        Me.ACC_CODE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(442, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 18)
        Me.Label1.TabIndex = 86
        Me.Label1.Text = "اسم الحساب:"
        '
        'ACC_NAME
        '
        Me.ACC_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ACC_NAME.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ACC_NAME.Location = New System.Drawing.Point(32, 88)
        Me.ACC_NAME.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ACC_NAME.Name = "ACC_NAME"
        Me.ACC_NAME.Size = New System.Drawing.Size(406, 27)
        Me.ACC_NAME.TabIndex = 85
        '
        'Button4
        '
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.Location = New System.Drawing.Point(1, 249)
        Me.Button4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(581, 49)
        Me.Button4.TabIndex = 84
        Me.Button4.Text = "عــودة   ↩         Esc"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'ACC_CODE_ErrorProvider
        '
        Me.ACC_CODE_ErrorProvider.ContainerControl = Me
        Me.ACC_CODE_ErrorProvider.RightToLeft = True
        '
        'ACC_NAME_ErrorProvider
        '
        Me.ACC_NAME_ErrorProvider.ContainerControl = Me
        Me.ACC_NAME_ErrorProvider.RightToLeft = True
        '
        'ACC_CODE_NEW
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(585, 299)
        Me.ControlBox = False
        Me.Controls.Add(Me.ACC_PARENT_Txt)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.is_Auto_Code_CB)
        Me.Controls.Add(Me.Label_info)
        Me.Controls.Add(Me.Label_CODE)
        Me.Controls.Add(Me.ACC_CODE)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ACC_NAME)
        Me.Controls.Add(Me.Button4)
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Name = "ACC_CODE_NEW"
        Me.Text = "فتــح حســاب جديـــد"
        CType(Me.ACC_CODE_ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ACC_NAME_ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button4 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ACC_NAME As TextBox
    Friend WithEvents Label_CODE As Label
    Friend WithEvents ACC_CODE As TextBox
    Friend WithEvents Label_info As Label
    Friend WithEvents is_Auto_Code_CB As CheckBox
    Friend WithEvents Button6 As Button
    Friend WithEvents ACC_CODE_ErrorProvider As ErrorProvider
    Friend WithEvents ACC_NAME_ErrorProvider As ErrorProvider
    Friend WithEvents ACC_PARENT_Txt As TextBox
End Class
