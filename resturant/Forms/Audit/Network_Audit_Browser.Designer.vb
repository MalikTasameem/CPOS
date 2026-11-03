<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Network_Audit_Browser
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
        Me.components = New System.ComponentModel.Container()
        Me.RootLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.FiltersLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.FromLabel = New System.Windows.Forms.Label()
        Me.ToLabel = New System.Windows.Forms.Label()
        Me.KindLabel = New System.Windows.Forms.Label()
        Me.OutcomeLabel = New System.Windows.Forms.Label()
        Me.DocumentLabel = New System.Windows.Forms.Label()
        Me.UserLabel = New System.Windows.Forms.Label()
        Me.ScreenLabel = New System.Windows.Forms.Label()
        Me.NotesLabel = New System.Windows.Forms.Label()
        Me.FromPicker = New System.Windows.Forms.DateTimePicker()
        Me.ToPicker = New System.Windows.Forms.DateTimePicker()
        Me.KindCombo = New System.Windows.Forms.ComboBox()
        Me.OutcomeCombo = New System.Windows.Forms.ComboBox()
        Me.DocumentText = New System.Windows.Forms.TextBox()
        Me.UserCombo = New System.Windows.Forms.ComboBox()
        Me.ScreenCombo = New System.Windows.Forms.ComboBox()
        Me.NotesText = New System.Windows.Forms.TextBox()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.ResetButton = New System.Windows.Forms.Button()
        Me.FilterHint = New System.Windows.Forms.Label()
        Me.BasisLabel = New System.Windows.Forms.Label()
        Me.EventsGrid = New System.Windows.Forms.DataGridView()
        Me.KindColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ActorColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ScreenColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ActionColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResultColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DocumentColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachineColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NotesColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BasisColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FooterLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.PreviousButton = New System.Windows.Forms.Button()
        Me.NextButton = New System.Windows.Forms.Button()
        Me.PageLabel = New System.Windows.Forms.Label()
        Me.DetailsButton = New System.Windows.Forms.Button()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.ResultsPrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.ResultsPrintPreviewDialog = New System.Windows.Forms.PrintPreviewDialog()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.RootLayout.SuspendLayout()
        Me.FiltersLayout.SuspendLayout()
        Me.FooterLayout.SuspendLayout()
        CType(Me.EventsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.RootLayout.ColumnCount = 1
        Me.RootLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.RootLayout.RowCount = 6
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.RootLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RootLayout.Padding = New System.Windows.Forms.Padding(10, 6, 10, 4)
        Me.RootLayout.Controls.Add(Me.TitleLabel, 0, 0)
        Me.RootLayout.Controls.Add(Me.FiltersLayout, 0, 1)
        Me.RootLayout.Controls.Add(Me.BasisLabel, 0, 2)
        Me.RootLayout.Controls.Add(Me.EventsGrid, 0, 3)
        Me.RootLayout.Controls.Add(Me.FooterLayout, 0, 4)
        Me.RootLayout.Controls.Add(Me.StatusStrip, 0, 5)
        Me.TitleLabel.Text = "شبكة المراقبة | مراجعة الأحداث والتفاصيل"
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!)
        Me.TitleLabel.BackColor = System.Drawing.Color.FromArgb(38, 55, 70)
        Me.TitleLabel.ForeColor = System.Drawing.Color.White
        Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TitleLabel.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FiltersLayout.ColumnCount = 8
        Me.FiltersLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.FiltersLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.0!))
        Me.FiltersLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.FiltersLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.0!))
        Me.FiltersLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.FiltersLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.0!))
        Me.FiltersLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.FiltersLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.0!))
        Me.FiltersLayout.RowCount = 3
        Me.FiltersLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.FiltersLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.FiltersLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.FiltersLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FiltersLayout.Controls.Add(Me.FromLabel, 0, 0)
        Me.FiltersLayout.Controls.Add(Me.FromPicker, 1, 0)
        Me.FiltersLayout.Controls.Add(Me.ToLabel, 2, 0)
        Me.FiltersLayout.Controls.Add(Me.ToPicker, 3, 0)
        Me.FiltersLayout.Controls.Add(Me.KindLabel, 4, 0)
        Me.FiltersLayout.Controls.Add(Me.KindCombo, 5, 0)
        Me.FiltersLayout.Controls.Add(Me.OutcomeLabel, 6, 0)
        Me.FiltersLayout.Controls.Add(Me.OutcomeCombo, 7, 0)
        Me.FiltersLayout.Controls.Add(Me.DocumentLabel, 0, 1)
        Me.FiltersLayout.Controls.Add(Me.DocumentText, 1, 1)
        Me.FiltersLayout.Controls.Add(Me.UserLabel, 2, 1)
        Me.FiltersLayout.Controls.Add(Me.UserCombo, 3, 1)
        Me.FiltersLayout.Controls.Add(Me.ScreenLabel, 4, 1)
        Me.FiltersLayout.Controls.Add(Me.ScreenCombo, 5, 1)
        Me.FiltersLayout.Controls.Add(Me.NotesLabel, 6, 1)
        Me.FiltersLayout.Controls.Add(Me.NotesText, 7, 1)
        Me.FiltersLayout.Controls.Add(Me.SearchButton, 0, 2)
        Me.FiltersLayout.SetColumnSpan(Me.SearchButton, 2)
        Me.FiltersLayout.Controls.Add(Me.ResetButton, 2, 2)
        Me.FiltersLayout.SetColumnSpan(Me.ResetButton, 2)
        Me.FiltersLayout.Controls.Add(Me.FilterHint, 4, 2)
        Me.FiltersLayout.SetColumnSpan(Me.FilterHint, 4)
        Me.FromLabel.Text = "من"
        Me.FromLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToLabel.Text = "إلى"
        Me.ToLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.KindLabel.Text = "السجل"
        Me.KindLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KindLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OutcomeLabel.Text = "النتيجة"
        Me.OutcomeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DocumentLabel.Text = "المستند"
        Me.DocumentLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DocumentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.UserLabel.Text = "المستخدم"
        Me.UserLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ScreenLabel.Text = "الشاشة"
        Me.ScreenLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScreenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NotesLabel.Text = "الوصف"
        Me.NotesLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NotesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.FromPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FromPicker.CustomFormat = "yyyy/MM/dd"
        Me.FromPicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FromPicker.Margin = New System.Windows.Forms.Padding(3, 8, 3, 5)
        Me.FromPicker.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.ToPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToPicker.CustomFormat = "yyyy/MM/dd"
        Me.ToPicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToPicker.Margin = New System.Windows.Forms.Padding(3, 8, 3, 5)
        Me.ToPicker.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.KindCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.KindCombo.Items.AddRange(New Object() {"السجل الجديد"})
        Me.KindCombo.Enabled = False
        Me.KindCombo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KindCombo.Margin = New System.Windows.Forms.Padding(3, 8, 3, 5)
        Me.KindCombo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.OutcomeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OutcomeCombo.Items.AddRange(New Object() {"كل النتائج", "نجاح", "رفض", "فشل مؤكد", "إلغاء المستخدم"})
        Me.OutcomeCombo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutcomeCombo.Margin = New System.Windows.Forms.Padding(3, 8, 3, 5)
        Me.OutcomeCombo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.DocumentText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DocumentText.Margin = New System.Windows.Forms.Padding(3, 8, 3, 5)
        Me.DocumentText.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.DocumentText.MaxLength = 50
        Me.UserCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UserCombo.Items.AddRange(New Object() {"كل المستخدمين"})
        Me.UserCombo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserCombo.Margin = New System.Windows.Forms.Padding(3, 8, 3, 5)
        Me.UserCombo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.ScreenCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ScreenCombo.Items.AddRange(New Object() {"كل الشاشات"})
        Me.ScreenCombo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScreenCombo.Margin = New System.Windows.Forms.Padding(3, 8, 3, 5)
        Me.ScreenCombo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.NotesText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NotesText.Margin = New System.Windows.Forms.Padding(3, 8, 3, 5)
        Me.NotesText.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.NotesText.MaxLength = 200
        Me.SearchButton.Text = "بحث / تحديث النتائج"
        Me.SearchButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SearchButton.BackColor = System.Drawing.Color.FromArgb(38, 88, 120)
        Me.SearchButton.ForeColor = System.Drawing.Color.White
        Me.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.ResetButton.Text = "إعادة ضبط المرشحات"
        Me.ResetButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResetButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.FilterHint.Text = "المرشحات اختيارية. تعرض الشاشة أحداث الآلية الجديدة فقط."
        Me.FilterHint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FilterHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BasisLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BasisLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BasisLabel.BackColor = System.Drawing.Color.FromArgb(233, 241, 247)
        Me.BasisLabel.Padding = New System.Windows.Forms.Padding(6)
        Me.BasisLabel.Text = "عرض للقراءة فقط للسجل الجديد؛ يشمل المسارات المنقولة، ولا يمثل اكتمال ربط جميع الشاشات."
        Me.EventsGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EventsGrid.AllowUserToAddRows = False
        Me.EventsGrid.AllowUserToDeleteRows = False
        Me.EventsGrid.AllowUserToOrderColumns = True
        Me.EventsGrid.ReadOnly = True
        Me.EventsGrid.MultiSelect = False
        Me.EventsGrid.RowHeadersVisible = False
        Me.EventsGrid.AutoGenerateColumns = False
        Me.EventsGrid.BackgroundColor = System.Drawing.Color.White
        Me.EventsGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.EventsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.EventsGrid.RowTemplate.Height = 30
        Me.EventsGrid.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.EventsGrid.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.EventsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.KindColumn, Me.TimeColumn, Me.ActorColumn, Me.ScreenColumn, Me.ActionColumn, Me.ResultColumn, Me.DocumentColumn, Me.MachineColumn, Me.NotesColumn, Me.BasisColumn})
        Me.KindColumn.DataPropertyName = "RecordLabel"
        Me.KindColumn.HeaderText = "السجل"
        Me.KindColumn.Width = 65
        Me.TimeColumn.DataPropertyName = "EventTime"
        Me.TimeColumn.HeaderText = "وقت الحدث"
        Me.TimeColumn.Width = 145
        Me.TimeColumn.DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss"
        Me.ActorColumn.DataPropertyName = "ActorName"
        Me.ActorColumn.HeaderText = "المستخدم / الخدمة"
        Me.ActorColumn.Width = 115
        Me.ScreenColumn.DataPropertyName = "ScreenLabel"
        Me.ScreenColumn.HeaderText = "الشاشة"
        Me.ScreenColumn.Width = 120
        Me.ActionColumn.DataPropertyName = "ActionLabel"
        Me.ActionColumn.HeaderText = "العملية"
        Me.ActionColumn.Width = 140
        Me.ResultColumn.DataPropertyName = "OutcomeLabel"
        Me.ResultColumn.HeaderText = "النتيجة"
        Me.ResultColumn.Width = 115
        Me.DocumentColumn.DataPropertyName = "DocumentNumber"
        Me.DocumentColumn.HeaderText = "المستند"
        Me.DocumentColumn.Width = 85
        Me.MachineColumn.DataPropertyName = "MachineName"
        Me.MachineColumn.HeaderText = "الجهاز"
        Me.MachineColumn.Width = 110
        Me.NotesColumn.DataPropertyName = "Notes"
        Me.NotesColumn.HeaderText = "وصف الحدث"
        Me.NotesColumn.Width = 250
        Me.BasisColumn.DataPropertyName = "TimeBasis"
        Me.BasisColumn.HeaderText = "أساس الوقت"
        Me.BasisColumn.Width = 210
        Me.FooterLayout.ColumnCount = 6
        Me.FooterLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.FooterLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.FooterLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.FooterLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.FooterLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.FooterLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.FooterLayout.RowCount = 1
        Me.FooterLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.FooterLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FooterLayout.Controls.Add(Me.PreviousButton, 0, 0)
        Me.FooterLayout.Controls.Add(Me.NextButton, 1, 0)
        Me.FooterLayout.Controls.Add(Me.PageLabel, 2, 0)
        Me.FooterLayout.Controls.Add(Me.PrintButton, 3, 0)
        Me.FooterLayout.Controls.Add(Me.DetailsButton, 4, 0)
        Me.FooterLayout.Controls.Add(Me.CloseButton, 5, 0)
        Me.PreviousButton.Text = "السابق"
        Me.PreviousButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PreviousButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.PreviousButton.Enabled = False
        Me.NextButton.Text = "التالي"
        Me.NextButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NextButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.NextButton.Enabled = False
        Me.PageLabel.Text = "لم يبدأ البحث"
        Me.PageLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DetailsButton.Text = "تفاصيل الحدث"
        Me.DetailsButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DetailsButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.DetailsButton.Enabled = False
        Me.PrintButton.Text = "معاينة الطباعة"
        Me.PrintButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrintButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.PrintButton.Enabled = False
        Me.CloseButton.Text = "إغلاق"
        Me.CloseButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.ResultsPrintDocument.DocumentName = "CPOS Audit Events"
        Me.ResultsPrintPreviewDialog.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.ResultsPrintPreviewDialog.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.ResultsPrintPreviewDialog.ClientSize = New System.Drawing.Size(1000, 700)
        Me.ResultsPrintPreviewDialog.Document = Me.ResultsPrintDocument
        Me.ResultsPrintPreviewDialog.Enabled = True
        Me.ResultsPrintPreviewDialog.Name = "ResultsPrintPreviewDialog"
        Me.ResultsPrintPreviewDialog.Text = "معاينة طباعة سجل المراقبة"
        Me.ResultsPrintPreviewDialog.UseAntiAlias = True
        Me.ResultsPrintPreviewDialog.Visible = False
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel})
        Me.StatusStrip.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StatusStrip.SizingGrip = False
        Me.StatusLabel.Spring = True
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StatusLabel.Text = "جاهز"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1024, 650)
        Me.MinimumSize = New System.Drawing.Size(980, 600)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Controls.Add(Me.RootLayout)
        Me.Name = "Network_Audit_Browser"
        Me.Text = "شبكة المراقبة الجديدة — مراجعة الأحداث"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.RootLayout.ResumeLayout(False)
        Me.RootLayout.PerformLayout()
        Me.FiltersLayout.ResumeLayout(False)
        Me.FiltersLayout.PerformLayout()
        Me.FooterLayout.ResumeLayout(False)
        CType(Me.EventsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents RootLayout As TableLayoutPanel
    Friend WithEvents TitleLabel As Label
    Friend WithEvents FiltersLayout As TableLayoutPanel
    Friend WithEvents FromLabel As Label
    Friend WithEvents ToLabel As Label
    Friend WithEvents KindLabel As Label
    Friend WithEvents OutcomeLabel As Label
    Friend WithEvents DocumentLabel As Label
    Friend WithEvents UserLabel As Label
    Friend WithEvents ScreenLabel As Label
    Friend WithEvents NotesLabel As Label
    Friend WithEvents FromPicker As DateTimePicker
    Friend WithEvents ToPicker As DateTimePicker
    Friend WithEvents KindCombo As ComboBox
    Friend WithEvents OutcomeCombo As ComboBox
    Friend WithEvents DocumentText As TextBox
    Friend WithEvents UserCombo As ComboBox
    Friend WithEvents ScreenCombo As ComboBox
    Friend WithEvents NotesText As TextBox
    Friend WithEvents SearchButton As Button
    Friend WithEvents ResetButton As Button
    Friend WithEvents FilterHint As Label
    Friend WithEvents BasisLabel As Label
    Friend WithEvents EventsGrid As DataGridView
    Friend WithEvents KindColumn As DataGridViewTextBoxColumn
    Friend WithEvents TimeColumn As DataGridViewTextBoxColumn
    Friend WithEvents ActorColumn As DataGridViewTextBoxColumn
    Friend WithEvents ScreenColumn As DataGridViewTextBoxColumn
    Friend WithEvents ActionColumn As DataGridViewTextBoxColumn
    Friend WithEvents ResultColumn As DataGridViewTextBoxColumn
    Friend WithEvents DocumentColumn As DataGridViewTextBoxColumn
    Friend WithEvents MachineColumn As DataGridViewTextBoxColumn
    Friend WithEvents NotesColumn As DataGridViewTextBoxColumn
    Friend WithEvents BasisColumn As DataGridViewTextBoxColumn
    Friend WithEvents FooterLayout As TableLayoutPanel
    Friend WithEvents PreviousButton As Button
    Friend WithEvents NextButton As Button
    Friend WithEvents PageLabel As Label
    Friend WithEvents PrintButton As Button
    Friend WithEvents DetailsButton As Button
    Friend WithEvents CloseButton As Button
    Friend WithEvents ResultsPrintDocument As System.Drawing.Printing.PrintDocument
    Friend WithEvents ResultsPrintPreviewDialog As PrintPreviewDialog
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents StatusLabel As ToolStripStatusLabel
End Class
