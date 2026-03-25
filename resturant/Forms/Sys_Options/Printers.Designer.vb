<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Printers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Printers))
        Me.Pr_listBox = New System.Windows.Forms.ListBox()
        Me.FieldsPanel = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.PrPathComboBox = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.PrNameTextBox = New System.Windows.Forms.TextBox()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.NewEmpButton = New System.Windows.Forms.Button()
        Me.EditEmpButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.PrPathErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.PrNameErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Printer_IP_Txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FieldsPanel.SuspendLayout()
        CType(Me.PrPathErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrNameErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Pr_listBox
        '
        Me.Pr_listBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Pr_listBox.FormattingEnabled = True
        Me.Pr_listBox.ItemHeight = 21
        Me.Pr_listBox.Location = New System.Drawing.Point(1, 12)
        Me.Pr_listBox.Name = "Pr_listBox"
        Me.Pr_listBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Pr_listBox.Size = New System.Drawing.Size(304, 403)
        Me.Pr_listBox.TabIndex = 431
        '
        'FieldsPanel
        '
        Me.FieldsPanel.Controls.Add(Me.Label1)
        Me.FieldsPanel.Controls.Add(Me.Printer_IP_Txt)
        Me.FieldsPanel.Controls.Add(Me.Label15)
        Me.FieldsPanel.Controls.Add(Me.PrPathComboBox)
        Me.FieldsPanel.Controls.Add(Me.Label17)
        Me.FieldsPanel.Controls.Add(Me.PrNameTextBox)
        Me.FieldsPanel.Enabled = False
        Me.FieldsPanel.Location = New System.Drawing.Point(311, 12)
        Me.FieldsPanel.Name = "FieldsPanel"
        Me.FieldsPanel.Size = New System.Drawing.Size(377, 299)
        Me.FieldsPanel.TabIndex = 432
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(253, 12)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(97, 21)
        Me.Label15.TabIndex = 403
        Me.Label15.Text = "إســـم الطابعة"
        '
        'PrPathComboBox
        '
        Me.PrPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PrPathComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PrPathComboBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrPathComboBox.FormattingEnabled = True
        Me.PrPathComboBox.Location = New System.Drawing.Point(18, 96)
        Me.PrPathComboBox.Name = "PrPathComboBox"
        Me.PrPathComboBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PrPathComboBox.Size = New System.Drawing.Size(335, 29)
        Me.PrPathComboBox.TabIndex = 404
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(249, 71)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(101, 21)
        Me.Label17.TabIndex = 405
        Me.Label17.Text = "مســار الطابعة"
        '
        'PrNameTextBox
        '
        Me.PrNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PrNameTextBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrNameTextBox.Location = New System.Drawing.Point(18, 37)
        Me.PrNameTextBox.Name = "PrNameTextBox"
        Me.PrNameTextBox.Size = New System.Drawing.Size(335, 29)
        Me.PrNameTextBox.TabIndex = 406
        Me.PrNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DeleteButton
        '
        Me.DeleteButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteButton.Enabled = False
        Me.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DeleteButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteButton.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.DeleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteButton.Location = New System.Drawing.Point(310, 317)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(90, 45)
        Me.DeleteButton.TabIndex = 430
        Me.DeleteButton.Text = "حـذف"
        Me.DeleteButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteButton.UseVisualStyleBackColor = False
        '
        'NewEmpButton
        '
        Me.NewEmpButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NewEmpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NewEmpButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewEmpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NewEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewEmpButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NewEmpButton.Image = CType(resources.GetObject("NewEmpButton.Image"), System.Drawing.Image)
        Me.NewEmpButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NewEmpButton.Location = New System.Drawing.Point(598, 317)
        Me.NewEmpButton.Name = "NewEmpButton"
        Me.NewEmpButton.Size = New System.Drawing.Size(90, 45)
        Me.NewEmpButton.TabIndex = 429
        Me.NewEmpButton.Text = "جديد "
        Me.NewEmpButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NewEmpButton.UseVisualStyleBackColor = False
        '
        'EditEmpButton
        '
        Me.EditEmpButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditEmpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EditEmpButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditEmpButton.Enabled = False
        Me.EditEmpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EditEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditEmpButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.EditEmpButton.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.EditEmpButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditEmpButton.Location = New System.Drawing.Point(406, 317)
        Me.EditEmpButton.Name = "EditEmpButton"
        Me.EditEmpButton.Size = New System.Drawing.Size(90, 45)
        Me.EditEmpButton.TabIndex = 428
        Me.EditEmpButton.Text = "تعديل"
        Me.EditEmpButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EditEmpButton.UseVisualStyleBackColor = False
        '
        'SaveButton
        '
        Me.SaveButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveButton.Enabled = False
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SaveButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveButton.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveButton.Location = New System.Drawing.Point(502, 317)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(90, 45)
        Me.SaveButton.TabIndex = 427
        Me.SaveButton.Text = "حفـظ "
        Me.SaveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'PrPathErrorProvider
        '
        Me.PrPathErrorProvider.ContainerControl = Me
        Me.PrPathErrorProvider.RightToLeft = True
        '
        'PrNameErrorProvider
        '
        Me.PrNameErrorProvider.ContainerControl = Me
        Me.PrNameErrorProvider.RightToLeft = True
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.Location = New System.Drawing.Point(555, 368)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(133, 47)
        Me.ExitFormButton.TabIndex = 452
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Printer_IP_Txt
        '
        Me.Printer_IP_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Printer_IP_Txt.Location = New System.Drawing.Point(18, 195)
        Me.Printer_IP_Txt.Name = "Printer_IP_Txt"
        Me.Printer_IP_Txt.Size = New System.Drawing.Size(332, 29)
        Me.Printer_IP_Txt.TabIndex = 407
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(282, 171)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(65, 21)
        Me.Label1.TabIndex = 408
        Me.Label1.Text = "عنوان IP"
        '
        'Printers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(690, 416)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Pr_listBox)
        Me.Controls.Add(Me.FieldsPanel)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.NewEmpButton)
        Me.Controls.Add(Me.EditEmpButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Printers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "طابعات التجهيز"
        Me.FieldsPanel.ResumeLayout(False)
        Me.FieldsPanel.PerformLayout()
        CType(Me.PrPathErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrNameErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pr_listBox As System.Windows.Forms.ListBox
    Friend WithEvents FieldsPanel As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents PrPathComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents PrNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents NewEmpButton As System.Windows.Forms.Button
    Friend WithEvents EditEmpButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents PrPathErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents PrNameErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Printer_IP_Txt As TextBox
End Class
