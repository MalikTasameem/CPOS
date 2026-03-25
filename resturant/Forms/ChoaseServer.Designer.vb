<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChoaseServer
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChoaseServer))
        Me.ServersGrid = New MetroFramework.Controls.MetroGrid()
        Me.PercD_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Trend_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Set_Btn = New DevComponents.DotNetBar.ButtonX()
        CType(Me.ServersGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ServersGrid
        '
        Me.ServersGrid.AllowUserToAddRows = False
        Me.ServersGrid.AllowUserToDeleteRows = False
        Me.ServersGrid.AllowUserToResizeRows = False
        Me.ServersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.ServersGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.ServersGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ServersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ServersGrid.CausesValidation = False
        Me.ServersGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.ServersGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ServersGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.ServersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ServersGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PercD_ID_CL, Me.Trend_ID_CL})
        Me.ServersGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ServersGrid.DefaultCellStyle = DataGridViewCellStyle2
        Me.ServersGrid.EnableHeadersVisualStyles = False
        Me.ServersGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.ServersGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ServersGrid.Location = New System.Drawing.Point(3, 49)
        Me.ServersGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.ServersGrid.MultiSelect = False
        Me.ServersGrid.Name = "ServersGrid"
        Me.ServersGrid.ReadOnly = True
        Me.ServersGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ServersGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.ServersGrid.RowHeadersVisible = False
        Me.ServersGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ServersGrid.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.ServersGrid.RowTemplate.Height = 100
        Me.ServersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ServersGrid.Size = New System.Drawing.Size(504, 327)
        Me.ServersGrid.TabIndex = 411
        Me.ServersGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'PercD_ID_CL
        '
        Me.PercD_ID_CL.DataPropertyName = "Serv_ID"
        Me.PercD_ID_CL.HeaderText = "رقم الخادم"
        Me.PercD_ID_CL.Name = "PercD_ID_CL"
        Me.PercD_ID_CL.ReadOnly = True
        Me.PercD_ID_CL.Visible = False
        '
        'Trend_ID_CL
        '
        Me.Trend_ID_CL.DataPropertyName = "Serv_Desc"
        Me.Trend_ID_CL.HeaderText = "الخوادم"
        Me.Trend_ID_CL.Name = "Trend_ID_CL"
        Me.Trend_ID_CL.ReadOnly = True
        '
        'Set_Btn
        '
        Me.Set_Btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.Set_Btn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.Set_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Set_Btn.Image = Global.resturant.My.Resources.Resources.if_hand_cursor_2639827
        Me.Set_Btn.Location = New System.Drawing.Point(3, 2)
        Me.Set_Btn.Name = "Set_Btn"
        Me.Set_Btn.Size = New System.Drawing.Size(504, 46)
        Me.Set_Btn.TabIndex = 412
        Me.Set_Btn.Text = "إختيار الخادم"
        '
        'ChoaseServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 380)
        Me.Controls.Add(Me.Set_Btn)
        Me.Controls.Add(Me.ServersGrid)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ChoaseServer"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "قائمة الخــوادم"
        CType(Me.ServersGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ServersGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents Set_Btn As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PercD_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Trend_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
