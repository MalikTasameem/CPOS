<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Network_Audit_Details
    Inherits System.Windows.Forms.Form
    Private components As System.ComponentModel.IContainer
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.RootLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.NoticeLabel = New System.Windows.Forms.Label()
        Me.DetailsTabs = New System.Windows.Forms.TabControl()
        Me.SummaryTab = New System.Windows.Forms.TabPage()
        Me.ChangesTab = New System.Windows.Forms.TabPage()
        Me.RelationsTab = New System.Windows.Forms.TabPage()
        Me.SummaryLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.NotesText = New System.Windows.Forms.TextBox()
        Me.MetadataGrid = New System.Windows.Forms.DataGridView()
        Me.NameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ValueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChangesGrid = New System.Windows.Forms.DataGridView()
        Me.EntityColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EntityIdColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FieldColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TypeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BeforeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AfterColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OriginColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RelationsText = New System.Windows.Forms.TextBox()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.RootLayout.SuspendLayout()
        Me.DetailsTabs.SuspendLayout()
        Me.SummaryLayout.SuspendLayout()
        CType(Me.MetadataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChangesGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.RootLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RootLayout.Padding = New System.Windows.Forms.Padding(10)
        Me.RootLayout.ColumnCount = 1
        Me.RootLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.RootLayout.RowCount = 3
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.RootLayout.Controls.Add(Me.NoticeLabel, 0, 0)
        Me.RootLayout.Controls.Add(Me.DetailsTabs, 0, 1)
        Me.RootLayout.Controls.Add(Me.CloseButton, 0, 2)
        Me.NoticeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NoticeLabel.BackColor = System.Drawing.Color.FromArgb(233, 241, 247)
        Me.NoticeLabel.Padding = New System.Windows.Forms.Padding(8)
        Me.NoticeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NoticeLabel.Text = "تفاصيل للقراءة فقط"
        Me.DetailsTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DetailsTabs.Controls.Add(Me.SummaryTab)
        Me.DetailsTabs.Controls.Add(Me.ChangesTab)
        Me.DetailsTabs.Controls.Add(Me.RelationsTab)
        Me.SummaryTab.Text = "ملخص وهوية الحدث"
        Me.SummaryTab.Controls.Add(Me.SummaryLayout)
        Me.ChangesTab.Text = "القيم السابقة واللاحقة"
        Me.ChangesTab.Controls.Add(Me.ChangesGrid)
        Me.RelationsTab.Text = "المراجع المرتبطة"
        Me.RelationsTab.Controls.Add(Me.RelationsText)
        Me.SummaryLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SummaryLayout.ColumnCount = 1
        Me.SummaryLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.SummaryLayout.RowCount = 2
        Me.SummaryLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.SummaryLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.SummaryLayout.Controls.Add(Me.NotesText, 0, 0)
        Me.SummaryLayout.Controls.Add(Me.MetadataGrid, 0, 1)
        Me.NotesText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NotesText.Multiline = True
        Me.NotesText.ReadOnly = True
        Me.NotesText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.NotesText.BackColor = System.Drawing.Color.White
        Me.NotesText.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.MetadataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetadataGrid.AutoGenerateColumns = False
        Me.MetadataGrid.ReadOnly = True
        Me.MetadataGrid.AllowUserToAddRows = False
        Me.MetadataGrid.AllowUserToDeleteRows = False
        Me.MetadataGrid.RowHeadersVisible = False
        Me.MetadataGrid.BackgroundColor = System.Drawing.Color.White
        Me.MetadataGrid.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.MetadataGrid.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MetadataGrid.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True
        Me.MetadataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MetadataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameColumn, Me.ValueColumn})
        Me.NameColumn.HeaderText = "الحقل"
        Me.NameColumn.DataPropertyName = "Name"
        Me.NameColumn.Width = 240
        Me.ValueColumn.HeaderText = "القيمة المحفوظة"
        Me.ValueColumn.DataPropertyName = "Value"
        Me.ValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ChangesGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChangesGrid.AutoGenerateColumns = False
        Me.ChangesGrid.ReadOnly = True
        Me.ChangesGrid.AllowUserToAddRows = False
        Me.ChangesGrid.AllowUserToDeleteRows = False
        Me.ChangesGrid.RowHeadersVisible = False
        Me.ChangesGrid.BackgroundColor = System.Drawing.Color.White
        Me.ChangesGrid.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.ChangesGrid.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ChangesGrid.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True
        Me.ChangesGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.ChangesGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EntityColumn, Me.EntityIdColumn, Me.FieldColumn, Me.TypeColumn, Me.BeforeColumn, Me.AfterColumn, Me.OriginColumn})
        Me.EntityColumn.DataPropertyName = "EntityType"
        Me.EntityColumn.HeaderText = "الكيان"
        Me.EntityColumn.Width = 115
        Me.EntityIdColumn.DataPropertyName = "EntityId"
        Me.EntityIdColumn.HeaderText = "معرف السطر / الكيان"
        Me.EntityIdColumn.Width = 110
        Me.FieldColumn.DataPropertyName = "FieldCode"
        Me.FieldColumn.HeaderText = "الحقل"
        Me.FieldColumn.Width = 125
        Me.TypeColumn.DataPropertyName = "ValueType"
        Me.TypeColumn.HeaderText = "نوع القيمة"
        Me.TypeColumn.Width = 80
        Me.BeforeColumn.DataPropertyName = "BeforeDisplay"
        Me.BeforeColumn.HeaderText = "قبل"
        Me.BeforeColumn.Width = 160
        Me.AfterColumn.DataPropertyName = "AfterDisplay"
        Me.AfterColumn.HeaderText = "بعد"
        Me.AfterColumn.Width = 160
        Me.OriginColumn.DataPropertyName = "OriginDisplay"
        Me.OriginColumn.HeaderText = "مصدر القيمة / نوع SQL"
        Me.OriginColumn.Width = 180
        Me.RelationsText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RelationsText.Multiline = True
        Me.RelationsText.ReadOnly = True
        Me.RelationsText.BackColor = System.Drawing.Color.White
        Me.RelationsText.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.RelationsText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.RelationsText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RelationsText.WordWrap = False
        Me.CloseButton.Text = "إغلاق التفاصيل"
        Me.CloseButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.CloseButton.Width = 150
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelButton = Me.CloseButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(960, 620)
        Me.MinimumSize = New System.Drawing.Size(820, 540)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Controls.Add(Me.RootLayout)
        Me.Name = "Network_Audit_Details"
        Me.Text = "تفاصيل حدث المراقبة"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.RootLayout.ResumeLayout(False)
        Me.DetailsTabs.ResumeLayout(False)
        Me.SummaryLayout.ResumeLayout(False)
        Me.SummaryLayout.PerformLayout()
        CType(Me.MetadataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChangesGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents RootLayout As TableLayoutPanel
    Friend WithEvents NoticeLabel As Label
    Friend WithEvents DetailsTabs As TabControl
    Friend WithEvents SummaryTab As TabPage
    Friend WithEvents ChangesTab As TabPage
    Friend WithEvents RelationsTab As TabPage
    Friend WithEvents SummaryLayout As TableLayoutPanel
    Friend WithEvents NotesText As TextBox
    Friend WithEvents MetadataGrid As DataGridView
    Friend WithEvents NameColumn As DataGridViewTextBoxColumn
    Friend WithEvents ValueColumn As DataGridViewTextBoxColumn
    Friend WithEvents ChangesGrid As DataGridView
    Friend WithEvents EntityColumn As DataGridViewTextBoxColumn
    Friend WithEvents EntityIdColumn As DataGridViewTextBoxColumn
    Friend WithEvents FieldColumn As DataGridViewTextBoxColumn
    Friend WithEvents TypeColumn As DataGridViewTextBoxColumn
    Friend WithEvents BeforeColumn As DataGridViewTextBoxColumn
    Friend WithEvents AfterColumn As DataGridViewTextBoxColumn
    Friend WithEvents OriginColumn As DataGridViewTextBoxColumn
    Friend WithEvents RelationsText As TextBox
    Friend WithEvents CloseButton As Button
End Class
