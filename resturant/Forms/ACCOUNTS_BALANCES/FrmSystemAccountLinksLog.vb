Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Public Class FrmSystemAccountLinksLog

    Private ReadOnly ConStr As String = MY_Settings.SqlConStr

    Public Property SystemAccountTypeID As Integer = 0
    Public Property AccountNameAr As String = ""

    Private _dtLog As DataTable

    Private Sub FrmSystemAccountLinksLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' تطبيق الثيم الإجباري
            ThemeManager.ApplyThemeToForm(Me)

            lblAccountName.Text = If(String.IsNullOrWhiteSpace(AccountNameAr), "كل الحسابات الأساسية", AccountNameAr)

            SetupGrid()
            LoadLog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadLog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkShowAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowAll.CheckedChanged
        LoadLog()
    End Sub

    Private Sub SetupGrid()
        With dgvLog
            .AutoGenerateColumns = False
            .Columns.Clear()
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .RowHeadersVisible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .EnableHeadersVisualStyles = False
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 55, 72)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 9.0!, FontStyle.Bold)
            .DefaultCellStyle.Font = New Font("Tahoma", 9.0!)
            .ColumnHeadersHeight = 36
            .RowTemplate.Height = 32
        End With

        AddTextColumn("ActionDate", "التاريخ", 120)
        AddTextColumn("ActionTypeAr", "نوع العملية", 120)
        AddTextColumn("AccountNameAr", "الحساب الأساسي", 180)
        AddTextColumn("Old_ACC_CODE", "الحساب القديم", 110)
        AddTextColumn("New_ACC_CODE", "الحساب الجديد", 110)
        AddTextColumn("OldNaturalText", "الطبيعة القديمة", 90)
        AddTextColumn("NewNaturalText", "الطبيعة الجديدة", 90)
        AddTextColumn("ActionBy", "المستخدم", 70)
        AddTextColumn("SummaryText", "الملخص", 320)

        AddHiddenColumn("LogID")
        AddHiddenColumn("AccountKey")
        AddHiddenColumn("Old_ACC_NAME")
        AddHiddenColumn("New_ACC_NAME")
        AddHiddenColumn("OldRequired")
        AddHiddenColumn("NewRequired")
        AddHiddenColumn("OldAllowSameAccount")
        AddHiddenColumn("NewAllowSameAccount")
        AddHiddenColumn("OldMustBeLeaf")
        AddHiddenColumn("NewMustBeLeaf")
        AddHiddenColumn("OldIsActive")
        AddHiddenColumn("NewIsActive")
        AddHiddenColumn("OldNotes")
        AddHiddenColumn("NewNotes")
        AddHiddenColumn("ActionNote")
    End Sub

    Private Sub AddTextColumn(dataPropertyName As String, headerText As String, fillWeight As Single)
        Dim col As New DataGridViewTextBoxColumn()
        col.DataPropertyName = dataPropertyName
        col.HeaderText = headerText
        col.Name = dataPropertyName
        col.FillWeight = fillWeight
        col.SortMode = DataGridViewColumnSortMode.Automatic
        dgvLog.Columns.Add(col)
    End Sub

    Private Sub AddHiddenColumn(dataPropertyName As String)
        Dim col As New DataGridViewTextBoxColumn()
        col.DataPropertyName = dataPropertyName
        col.Name = dataPropertyName
        col.Visible = False
        dgvLog.Columns.Add(col)
    End Sub

    Private Sub LoadLog()
        Try
            lblStatus.Text = "جاري تحميل سجل التعديلات..."

            Using cn As New SqlConnection(ConStr)
                Using cmd As New SqlCommand("dbo.ACC_SYSTEM_ACCOUNT_LINKS_LOG_LOAD", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 60

                    If chkShowAll.Checked OrElse SystemAccountTypeID <= 0 Then
                        cmd.Parameters.Add("@SystemAccountTypeID", SqlDbType.Int).Value = DBNull.Value
                    Else
                        cmd.Parameters.Add("@SystemAccountTypeID", SqlDbType.Int).Value = SystemAccountTypeID
                    End If

                    Using da As New SqlDataAdapter(cmd)
                        _dtLog = New DataTable()
                        da.Fill(_dtLog)
                    End Using
                End Using
            End Using

            dgvLog.DataSource = _dtLog
            ApplyRowsStyle()

            lblStatus.Text = "عدد السجلات: " & _dtLog.Rows.Count.ToString()

        Catch ex As Exception
            lblStatus.Text = "فشل تحميل السجل"
            MessageBox.Show(ex.Message, "خطأ في تحميل السجل", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplyRowsStyle()
        For Each gridRow As DataGridViewRow In dgvLog.Rows
            Dim actionType As String = ""

            If dgvLog.Columns.Contains("ActionTypeAr") AndAlso
               gridRow.Cells("ActionTypeAr").Value IsNot Nothing AndAlso
               gridRow.Cells("ActionTypeAr").Value IsNot DBNull.Value Then
                actionType = Convert.ToString(gridRow.Cells("ActionTypeAr").Value)
            End If

            Select Case actionType
                Case "ربط / تغيير حساب"
                    gridRow.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233)
                    gridRow.DefaultCellStyle.ForeColor = Color.FromArgb(30, 80, 40)

                Case "إلغاء ربط"
                    gridRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238)
                    gridRow.DefaultCellStyle.ForeColor = Color.FromArgb(120, 30, 30)

                Case "تعديل نمط"
                    gridRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225)
                    gridRow.DefaultCellStyle.ForeColor = Color.FromArgb(120, 90, 20)

                Case Else
                    gridRow.DefaultCellStyle.BackColor = Color.White
                    gridRow.DefaultCellStyle.ForeColor = Color.Black
            End Select
        Next
    End Sub

End Class