<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CalendarForm
    Inherits System.Windows.Forms.Form

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
        Me.head_Panel = New System.Windows.Forms.Panel()
        Me.days_Panel = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'head_Panel
        '
        Me.head_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.head_Panel.Location = New System.Drawing.Point(0, 0)
        Me.head_Panel.Name = "head_Panel"
        Me.head_Panel.Size = New System.Drawing.Size(821, 65)
        Me.head_Panel.TabIndex = 0
        '
        'days_Panel
        '
        Me.days_Panel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.days_Panel.Location = New System.Drawing.Point(0, 65)
        Me.days_Panel.Name = "days_Panel"
        Me.days_Panel.Size = New System.Drawing.Size(821, 570)
        Me.days_Panel.TabIndex = 1
        '
        'CalendarForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(821, 635)
        Me.Controls.Add(Me.days_Panel)
        Me.Controls.Add(Me.head_Panel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CalendarForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تقويـــم الحجــز"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents head_Panel As Panel
    Friend WithEvents days_Panel As Panel
End Class
