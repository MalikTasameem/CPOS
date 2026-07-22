Partial Class Backup
    Private ScheduledGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ScheduledPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ScheduledBrowseButton As System.Windows.Forms.Button
    Friend WithEvents KeepCountNumeric As System.Windows.Forms.NumericUpDown
    Friend WithEvents CleanupModeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CleanupValueLabel As System.Windows.Forms.Label
    Friend WithEvents CleanupEnabledCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ScheduledCompressionCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ScheduledTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents ScheduledSaveButton As System.Windows.Forms.Button
    Friend WithEvents ScheduledRunNowButton As System.Windows.Forms.Button
    Friend WithEvents ScheduledRemoveButton As System.Windows.Forms.Button
    Friend WithEvents ScheduledTestPathButton As System.Windows.Forms.Button
    Friend WithEvents ScheduledRefreshButton As System.Windows.Forms.Button
    Friend WithEvents ScheduledStatusLabel As System.Windows.Forms.Label

    Private Sub InitializeScheduledBackupControls()
        Me.ScheduledGroupBox = New System.Windows.Forms.GroupBox()
        Me.ScheduledPathTextBox = New System.Windows.Forms.TextBox()
        Me.ScheduledBrowseButton = New System.Windows.Forms.Button()
        Me.KeepCountNumeric = New System.Windows.Forms.NumericUpDown()
        Me.CleanupModeComboBox = New System.Windows.Forms.ComboBox()
        Me.CleanupValueLabel = New System.Windows.Forms.Label()
        Me.CleanupEnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.ScheduledCompressionCheckBox = New System.Windows.Forms.CheckBox()
        Me.ScheduledTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.ScheduledSaveButton = New System.Windows.Forms.Button()
        Me.ScheduledRunNowButton = New System.Windows.Forms.Button()
        Me.ScheduledRemoveButton = New System.Windows.Forms.Button()
        Me.ScheduledTestPathButton = New System.Windows.Forms.Button()
        Me.ScheduledRefreshButton = New System.Windows.Forms.Button()
        Me.ScheduledStatusLabel = New System.Windows.Forms.Label()
        Dim scheduledPathLabel As New System.Windows.Forms.Label()
        Dim cleanupModeLabel As New System.Windows.Forms.Label()
        Dim runTimeLabel As New System.Windows.Forms.Label()
        Dim descriptionLabel As New System.Windows.Forms.Label()

        CType(Me.KeepCountNumeric, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ScheduledGroupBox.SuspendLayout()

        Me.ClientSize = New System.Drawing.Size(1170, 674)
        Me.ScheduledGroupBox.Location = New System.Drawing.Point(752, 12)
        Me.ScheduledGroupBox.Name = "ScheduledGroupBox"
        Me.ScheduledGroupBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ScheduledGroupBox.Size = New System.Drawing.Size(406, 650)
        Me.ScheduledGroupBox.TabIndex = 400
        Me.ScheduledGroupBox.TabStop = False
        Me.ScheduledGroupBox.Text = "النسخ الاحتياطي المجدول المستقل"
        Me.ScheduledGroupBox.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)

        descriptionLabel.Location = New System.Drawing.Point(20, 32)
        descriptionLabel.Size = New System.Drawing.Size(366, 55)
        descriptionLabel.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        descriptionLabel.ForeColor = System.Drawing.Color.DimGray
        descriptionLabel.Text = "تعمل المهمة من Windows Task Scheduler حتى عند إغلاق النظام، طالما أن السيرفر وSQL Server يعملان."

        scheduledPathLabel.Location = New System.Drawing.Point(20, 94)
        scheduledPathLabel.Size = New System.Drawing.Size(366, 25)
        scheduledPathLabel.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        scheduledPathLabel.Text = "مسار حفظ ملفات النسخ على السيرفر"

        Me.ScheduledPathTextBox.Location = New System.Drawing.Point(62, 122)
        Me.ScheduledPathTextBox.Name = "ScheduledPathTextBox"
        Me.ScheduledPathTextBox.Size = New System.Drawing.Size(324, 29)
        Me.ScheduledPathTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold)

        Me.ScheduledBrowseButton.Location = New System.Drawing.Point(20, 121)
        Me.ScheduledBrowseButton.Name = "ScheduledBrowseButton"
        Me.ScheduledBrowseButton.Size = New System.Drawing.Size(38, 31)
        Me.ScheduledBrowseButton.Text = "..."
        Me.ScheduledBrowseButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)

        Me.ScheduledTestPathButton.Location = New System.Drawing.Point(20, 159)
        Me.ScheduledTestPathButton.Name = "ScheduledTestPathButton"
        Me.ScheduledTestPathButton.Size = New System.Drawing.Size(366, 34)
        Me.ScheduledTestPathButton.Text = "اختبار إمكانية الكتابة في المسار"
        Me.ScheduledTestPathButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)

        cleanupModeLabel.Location = New System.Drawing.Point(210, 207)
        cleanupModeLabel.Size = New System.Drawing.Size(176, 28)
        cleanupModeLabel.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        cleanupModeLabel.Text = "طريقة حذف النسخ القديمة"

        Me.CleanupModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CleanupModeComboBox.Location = New System.Drawing.Point(20, 205)
        Me.CleanupModeComboBox.Name = "CleanupModeComboBox"
        Me.CleanupModeComboBox.Size = New System.Drawing.Size(170, 29)
        Me.CleanupModeComboBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CleanupModeComboBox.Items.AddRange(New Object() {"حسب عدد النسخ", "حسب العمر بالأيام"})

        Me.CleanupValueLabel.Location = New System.Drawing.Point(210, 249)
        Me.CleanupValueLabel.Size = New System.Drawing.Size(176, 28)
        Me.CleanupValueLabel.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.CleanupValueLabel.Text = "عدد النسخ المحتفظ بها"

        Me.KeepCountNumeric.Location = New System.Drawing.Point(20, 247)
        Me.KeepCountNumeric.Name = "KeepCountNumeric"
        Me.KeepCountNumeric.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.KeepCountNumeric.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.KeepCountNumeric.Value = New Decimal(New Integer() {30, 0, 0, 0})
        Me.KeepCountNumeric.Size = New System.Drawing.Size(170, 29)
        Me.KeepCountNumeric.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold)

        runTimeLabel.Location = New System.Drawing.Point(210, 291)
        runTimeLabel.Size = New System.Drawing.Size(176, 28)
        runTimeLabel.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        runTimeLabel.Text = "وقت النسخ اليومي"

        Me.ScheduledTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.ScheduledTimePicker.ShowUpDown = True
        Me.ScheduledTimePicker.Location = New System.Drawing.Point(20, 289)
        Me.ScheduledTimePicker.Name = "ScheduledTimePicker"
        Me.ScheduledTimePicker.Size = New System.Drawing.Size(170, 29)
        Me.ScheduledTimePicker.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold)

        Me.CleanupEnabledCheckBox.Location = New System.Drawing.Point(20, 328)
        Me.CleanupEnabledCheckBox.Name = "CleanupEnabledCheckBox"
        Me.CleanupEnabledCheckBox.Size = New System.Drawing.Size(366, 31)
        Me.CleanupEnabledCheckBox.Text = "حذف النسخ القديمة تلقائيًا بعد نجاح النسخة الجديدة"
        Me.CleanupEnabledCheckBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)

        Me.ScheduledCompressionCheckBox.Location = New System.Drawing.Point(20, 363)
        Me.ScheduledCompressionCheckBox.Name = "ScheduledCompressionCheckBox"
        Me.ScheduledCompressionCheckBox.Size = New System.Drawing.Size(366, 31)
        Me.ScheduledCompressionCheckBox.Text = "ضغط النسخة الاحتياطية (COMPRESSION)"
        Me.ScheduledCompressionCheckBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold)

        Me.ScheduledSaveButton.Location = New System.Drawing.Point(200, 405)
        Me.ScheduledSaveButton.Name = "ScheduledSaveButton"
        Me.ScheduledSaveButton.Size = New System.Drawing.Size(186, 42)
        Me.ScheduledSaveButton.Text = "حفظ وتسجيل الجدولة"
        Me.ScheduledSaveButton.BackColor = System.Drawing.Color.FromArgb(36, 117, 72)
        Me.ScheduledSaveButton.ForeColor = System.Drawing.Color.White
        Me.ScheduledSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ScheduledSaveButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold)

        Me.ScheduledRunNowButton.Location = New System.Drawing.Point(20, 405)
        Me.ScheduledRunNowButton.Name = "ScheduledRunNowButton"
        Me.ScheduledRunNowButton.Size = New System.Drawing.Size(170, 42)
        Me.ScheduledRunNowButton.Text = "تشغيل نسخة الآن"
        Me.ScheduledRunNowButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold)

        Me.ScheduledRemoveButton.Location = New System.Drawing.Point(200, 456)
        Me.ScheduledRemoveButton.Name = "ScheduledRemoveButton"
        Me.ScheduledRemoveButton.Size = New System.Drawing.Size(186, 38)
        Me.ScheduledRemoveButton.Text = "إلغاء المهمة المجدولة"
        Me.ScheduledRemoveButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)

        Me.ScheduledRefreshButton.Location = New System.Drawing.Point(20, 456)
        Me.ScheduledRefreshButton.Name = "ScheduledRefreshButton"
        Me.ScheduledRefreshButton.Size = New System.Drawing.Size(170, 38)
        Me.ScheduledRefreshButton.Text = "تحديث الحالة"
        Me.ScheduledRefreshButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)

        Me.ScheduledStatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ScheduledStatusLabel.Location = New System.Drawing.Point(20, 507)
        Me.ScheduledStatusLabel.Name = "ScheduledStatusLabel"
        Me.ScheduledStatusLabel.Size = New System.Drawing.Size(366, 119)
        Me.ScheduledStatusLabel.Text = "حالة المهمة: جاري القراءة..."
        Me.ScheduledStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ScheduledStatusLabel.Font = New System.Drawing.Font("Segoe UI", 10.5!)

        Me.ScheduledGroupBox.Controls.Add(descriptionLabel)
        Me.ScheduledGroupBox.Controls.Add(scheduledPathLabel)
        Me.ScheduledGroupBox.Controls.Add(Me.ScheduledPathTextBox)
        Me.ScheduledGroupBox.Controls.Add(Me.ScheduledBrowseButton)
        Me.ScheduledGroupBox.Controls.Add(Me.ScheduledTestPathButton)
        Me.ScheduledGroupBox.Controls.Add(cleanupModeLabel)
        Me.ScheduledGroupBox.Controls.Add(Me.CleanupModeComboBox)
        Me.ScheduledGroupBox.Controls.Add(Me.CleanupValueLabel)
        Me.ScheduledGroupBox.Controls.Add(Me.KeepCountNumeric)
        Me.ScheduledGroupBox.Controls.Add(Me.CleanupEnabledCheckBox)
        Me.ScheduledGroupBox.Controls.Add(runTimeLabel)
        Me.ScheduledGroupBox.Controls.Add(Me.ScheduledTimePicker)
        Me.ScheduledGroupBox.Controls.Add(Me.ScheduledCompressionCheckBox)
        Me.ScheduledGroupBox.Controls.Add(Me.ScheduledSaveButton)
        Me.ScheduledGroupBox.Controls.Add(Me.ScheduledRunNowButton)
        Me.ScheduledGroupBox.Controls.Add(Me.ScheduledRemoveButton)
        Me.ScheduledGroupBox.Controls.Add(Me.ScheduledRefreshButton)
        Me.ScheduledGroupBox.Controls.Add(Me.ScheduledStatusLabel)
        Me.Controls.Add(Me.ScheduledGroupBox)

        CType(Me.KeepCountNumeric, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ScheduledGroupBox.ResumeLayout(False)
    End Sub
End Class
