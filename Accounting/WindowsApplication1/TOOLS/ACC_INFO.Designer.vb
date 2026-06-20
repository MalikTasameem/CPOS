<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ACC_INFO
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.SEARCH_ACC_BTN = New System.Windows.Forms.Button()
        Me.ACC_CODE_TXT = New System.Windows.Forms.TextBox()
        Me.ACC_CODE_Cm = New System.Windows.Forms.ComboBox()
        Me.ACC_CODE_NUM_ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.ACC_CODE_NUM_ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SEARCH_ACC_BTN
        '
        Me.SEARCH_ACC_BTN.BackColor = System.Drawing.Color.White
        Me.SEARCH_ACC_BTN.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SEARCH_ACC_BTN.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SEARCH_ACC_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SEARCH_ACC_BTN.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SEARCH_ACC_BTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SEARCH_ACC_BTN.Location = New System.Drawing.Point(3, 4)
        Me.SEARCH_ACC_BTN.Margin = New System.Windows.Forms.Padding(2)
        Me.SEARCH_ACC_BTN.Name = "SEARCH_ACC_BTN"
        Me.SEARCH_ACC_BTN.Size = New System.Drawing.Size(29, 25)
        Me.SEARCH_ACC_BTN.TabIndex = 419
        Me.SEARCH_ACC_BTN.Text = "..."
        Me.ToolTip1.SetToolTip(Me.SEARCH_ACC_BTN, "قائمة الحسابات")
        Me.SEARCH_ACC_BTN.UseVisualStyleBackColor = False
        '
        'ACC_CODE_TXT
        '
        Me.ACC_CODE_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ACC_CODE_TXT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ACC_CODE_TXT.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ACC_CODE_TXT.Location = New System.Drawing.Point(281, 3)
        Me.ACC_CODE_TXT.Margin = New System.Windows.Forms.Padding(2)
        Me.ACC_CODE_TXT.Name = "ACC_CODE_TXT"
        Me.ACC_CODE_TXT.Size = New System.Drawing.Size(101, 25)
        Me.ACC_CODE_TXT.TabIndex = 104
        Me.ACC_CODE_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.ACC_CODE_TXT, "أدخل رقم الحساب")
        '
        'ACC_CODE_Cm
        '
        Me.ACC_CODE_Cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ACC_CODE_Cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ACC_CODE_Cm.BackColor = System.Drawing.Color.White
        Me.ACC_CODE_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ACC_CODE_Cm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ACC_CODE_Cm.DropDownHeight = 500
        Me.ACC_CODE_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ACC_CODE_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ACC_CODE_Cm.FormattingEnabled = True
        Me.ACC_CODE_Cm.IntegralHeight = False
        Me.ACC_CODE_Cm.Location = New System.Drawing.Point(36, 3)
        Me.ACC_CODE_Cm.Margin = New System.Windows.Forms.Padding(2)
        Me.ACC_CODE_Cm.Name = "ACC_CODE_Cm"
        Me.ACC_CODE_Cm.Size = New System.Drawing.Size(241, 25)
        Me.ACC_CODE_Cm.TabIndex = 105
        Me.ToolTip1.SetToolTip(Me.ACC_CODE_Cm, "إسم الحساب")
        '
        'ACC_CODE_NUM_ErrorProvider
        '
        Me.ACC_CODE_NUM_ErrorProvider.ContainerControl = Me
        Me.ACC_CODE_NUM_ErrorProvider.RightToLeft = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ACC_CODE_TXT, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SEARCH_ACC_BTN, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ACC_CODE_Cm, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.Padding = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(386, 33)
        Me.TableLayoutPanel1.TabIndex = 420
        '
        'ACC_INFO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MinimumSize = New System.Drawing.Size(260, 31)
        Me.Name = "ACC_INFO"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Size = New System.Drawing.Size(386, 33)
        CType(Me.ACC_CODE_NUM_ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SEARCH_ACC_BTN As Button
    Friend WithEvents ACC_CODE_TXT As TextBox
    Friend WithEvents ACC_CODE_Cm As ComboBox
    Friend WithEvents ACC_CODE_NUM_ErrorProvider As ErrorProvider
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
