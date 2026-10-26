Option Strict On

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AuditQuantityRecoveryDialog
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.InfoLabel = New System.Windows.Forms.Label()
        Me.ActionsPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.RetryButton = New System.Windows.Forms.Button()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.ActionsPanel.SuspendLayout()
        Me.SuspendLayout()
        Me.InfoLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InfoLabel.Padding = New System.Windows.Forms.Padding(16)
        Me.InfoLabel.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.InfoLabel.Name = "InfoLabel"
        Me.ActionsPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ActionsPanel.Height = 62
        Me.ActionsPanel.Padding = New System.Windows.Forms.Padding(8)
        Me.ActionsPanel.Name = "ActionsPanel"
        Me.ActionsPanel.Controls.Add(Me.RetryButton)
        Me.ActionsPanel.Controls.Add(Me.CloseButton)
        Me.RetryButton.Text = "إعادة المحاولة نفسها"
        Me.RetryButton.Size = New System.Drawing.Size(240, 42)
        Me.RetryButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.RetryButton.Name = "RetryButton"
        Me.CloseButton.Text = "فحص النتيجة / رجوع"
        Me.CloseButton.Size = New System.Drawing.Size(240, 42)
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.CloseButton.Name = "CloseButton"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 300)
        Me.MinimumSize = New System.Drawing.Size(540, 280)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.Text = "حسم نتيجة تعديل الكمية"
        Me.Name = "AuditQuantityRecoveryDialog"
        Me.Controls.Add(Me.InfoLabel)
        Me.Controls.Add(Me.ActionsPanel)
        Me.ActionsPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents InfoLabel As System.Windows.Forms.Label
    Friend WithEvents ActionsPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents RetryButton As System.Windows.Forms.Button
    Friend WithEvents CloseButton As System.Windows.Forms.Button
End Class
