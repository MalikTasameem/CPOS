<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Costmers_Bills_Report
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Costmers_Bills_Report))
        Me.Costmers_DG = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.AGENT_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bills_QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn49 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Agent_Bills_Btn = New System.Windows.Forms.Button()
        Me.Agents_Print_Btn = New System.Windows.Forms.Button()
        CType(Me.Costmers_DG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Costmers_DG
        '
        Me.Costmers_DG.AllowUserToAddRows = False
        Me.Costmers_DG.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info
        Me.Costmers_DG.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Costmers_DG.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Costmers_DG.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Costmers_DG.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Costmers_DG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Costmers_DG.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AGENT_NAME, Me.Bills_QTY, Me.DataGridViewTextBoxColumn49})
        Me.Costmers_DG.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Costmers_DG.DefaultCellStyle = DataGridViewCellStyle5
        Me.Costmers_DG.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Costmers_DG.Location = New System.Drawing.Point(2, 40)
        Me.Costmers_DG.MultiSelect = False
        Me.Costmers_DG.Name = "Costmers_DG"
        Me.Costmers_DG.ReadOnly = True
        Me.Costmers_DG.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Costmers_DG.RowHeadersVisible = False
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Costmers_DG.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.Costmers_DG.RowTemplate.Height = 35
        Me.Costmers_DG.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Costmers_DG.Size = New System.Drawing.Size(891, 615)
        Me.Costmers_DG.TabIndex = 638
        '
        'AGENT_NAME
        '
        Me.AGENT_NAME.DataPropertyName = "AGENT_NAME"
        Me.AGENT_NAME.HeaderText = "الزبون"
        Me.AGENT_NAME.Name = "AGENT_NAME"
        Me.AGENT_NAME.ReadOnly = True
        '
        'Bills_QTY
        '
        Me.Bills_QTY.DataPropertyName = "Bills_QTY"
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Bills_QTY.DefaultCellStyle = DataGridViewCellStyle3
        Me.Bills_QTY.HeaderText = "عدد الفواتير"
        Me.Bills_QTY.Name = "Bills_QTY"
        Me.Bills_QTY.ReadOnly = True
        '
        'DataGridViewTextBoxColumn49
        '
        Me.DataGridViewTextBoxColumn49.DataPropertyName = "Bills_Total"
        DataGridViewCellStyle4.Format = "N2"
        Me.DataGridViewTextBoxColumn49.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn49.HeaderText = "الإجمالي"
        Me.DataGridViewTextBoxColumn49.Name = "DataGridViewTextBoxColumn49"
        Me.DataGridViewTextBoxColumn49.ReadOnly = True
        '
        'Agent_Bills_Btn
        '
        Me.Agent_Bills_Btn.BackColor = System.Drawing.Color.White
        Me.Agent_Bills_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Agent_Bills_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Agent_Bills_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Agent_Bills_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Agent_Bills_Btn.ForeColor = System.Drawing.Color.Black
        Me.Agent_Bills_Btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Agent_Bills_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Agent_Bills_Btn.Location = New System.Drawing.Point(760, 1)
        Me.Agent_Bills_Btn.Name = "Agent_Bills_Btn"
        Me.Agent_Bills_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Agent_Bills_Btn.Size = New System.Drawing.Size(133, 38)
        Me.Agent_Bills_Btn.TabIndex = 636
        Me.Agent_Bills_Btn.Text = "بحث"
        Me.Agent_Bills_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Agent_Bills_Btn.UseVisualStyleBackColor = False
        '
        'Agents_Print_Btn
        '
        Me.Agents_Print_Btn.BackColor = System.Drawing.Color.White
        Me.Agents_Print_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Agents_Print_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Agents_Print_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Agents_Print_Btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.Agents_Print_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Agents_Print_Btn.Location = New System.Drawing.Point(626, 1)
        Me.Agents_Print_Btn.Name = "Agents_Print_Btn"
        Me.Agents_Print_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Agents_Print_Btn.Size = New System.Drawing.Size(133, 38)
        Me.Agents_Print_Btn.TabIndex = 637
        Me.Agents_Print_Btn.Text = "طباعة"
        Me.Agents_Print_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Agents_Print_Btn.UseVisualStyleBackColor = False
        '
        'Costmers_Bills_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Costmers_DG)
        Me.Controls.Add(Me.Agent_Bills_Btn)
        Me.Controls.Add(Me.Agents_Print_Btn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Costmers_Bills_Report"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.Costmers_DG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Costmers_DG As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents AGENT_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bills_QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn49 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Agent_Bills_Btn As System.Windows.Forms.Button
    Friend WithEvents Agents_Print_Btn As System.Windows.Forms.Button
End Class
