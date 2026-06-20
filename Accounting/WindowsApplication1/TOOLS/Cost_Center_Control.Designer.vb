<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cost_Center_Control
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ALL_CheckBox = New System.Windows.Forms.CheckBox()
        Me.COST_CM = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(313, 53)
        Me.GroupBox1.TabIndex = 111
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " مركز التكلفة:"
        '
        'ALL_CheckBox
        '
        Me.ALL_CheckBox.AutoSize = True
        Me.ALL_CheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ALL_CheckBox.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ALL_CheckBox.Location = New System.Drawing.Point(6, 3)
        Me.ALL_CheckBox.Name = "ALL_CheckBox"
        Me.ALL_CheckBox.Size = New System.Drawing.Size(47, 22)
        Me.ALL_CheckBox.TabIndex = 108
        Me.ALL_CheckBox.Text = "الكل"
        Me.ALL_CheckBox.UseVisualStyleBackColor = True
        '
        'COST_CM
        '
        Me.COST_CM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.COST_CM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.COST_CM.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.COST_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.COST_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.COST_CM.Font = New System.Drawing.Font("Arial", 10.25!, System.Drawing.FontStyle.Bold)
        Me.COST_CM.FormattingEnabled = True
        Me.COST_CM.Location = New System.Drawing.Point(60, 4)
        Me.COST_CM.Margin = New System.Windows.Forms.Padding(4)
        Me.COST_CM.Name = "COST_CM"
        Me.COST_CM.Size = New System.Drawing.Size(243, 24)
        Me.COST_CM.TabIndex = 108
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ALL_CheckBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.COST_CM, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(307, 34)
        Me.TableLayoutPanel1.TabIndex = 112
        '
        'Cost_Center_Control
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Cost_Center_Control"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Size = New System.Drawing.Size(313, 53)
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ALL_CheckBox As CheckBox
    Friend WithEvents COST_CM As ComboBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
