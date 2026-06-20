Imports System.Data
Imports System.Data.SqlClient

Public Class FrmBudgetChapters

    '✅ غيّر الاتصال حسب بيئتك (أو اربطه بـ My.Settings)
    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr

    Private CurrentChapterId As Integer = 0

    Private Function NewRefNo() As String
        Return "BC-" & DateTime.Now.ToString("yyyyMMdd-HHmmss")
    End Function

    'SQL Server 2014 Audit Context
    Private Sub SetUserContext(userId As Integer, refNo As String)
        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
DELETE FROM dbo.User_Context WHERE SPID = @@SPID;
INSERT INTO dbo.User_Context (SPID, UserId, RefNo)
VALUES (@@SPID, @UserId, @RefNo);", cn)

                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.Parameters.AddWithValue("@RefNo", refNo)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub FrmBudgetChapters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGridStyle()
            BudgetUiHelper.ApplyBudgetFormStyle(Me)
            LoadDoors()
            LoadChapters()
            ClearForm()
            SetStatus("جاهز")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub ApplyGridStyle()
        dgvChapters.EnableHeadersVisualStyles = False
        dgvChapters.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvChapters.ColumnHeadersHeight = 38
        dgvChapters.RowTemplate.Height = 34

        dgvChapters.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 242, 245)
        dgvChapters.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 35, 35)
        dgvChapters.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

        dgvChapters.DefaultCellStyle.Font = New Font("Segoe UI", 10.0!, FontStyle.Regular)
        dgvChapters.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 237, 255)
        dgvChapters.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30)

        dgvChapters.GridColor = Color.FromArgb(235, 235, 235)
    End Sub

    Private Sub LoadDoors()
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT DoorId,
       DoorCode + N' - ' + DoorName AS DoorText
FROM Budget_Doors
WHERE IsActive = 1
ORDER BY DoorCode;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbDoors.DataSource = dt
        cmbDoors.DisplayMember = "DoorText"
        cmbDoors.ValueMember = "DoorId"
        cmbDoors.SelectedIndex = -1
    End Sub

    Private Sub LoadChapters(Optional doorIdFilter As Integer = 0)
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT
    c.ChapterId,
    c.DoorId,
    d.DoorCode,
    d.DoorName,
    c.ChapterCode,
    c.ChapterName,
    c.IsActive
FROM Budget_Chapters c
JOIN Budget_Doors d ON c.DoorId = d.DoorId
WHERE (@DoorId = 0 OR c.DoorId = @DoorId)
ORDER BY d.DoorCode, c.ChapterCode;", cn)

                cmd.Parameters.AddWithValue("@DoorId", doorIdFilter)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvChapters.DataSource = dt

        If dgvChapters.Columns.Count > 0 Then
            dgvChapters.Columns("ChapterId").Visible = False
            dgvChapters.Columns("DoorId").Visible = False

            dgvChapters.Columns("DoorCode").HeaderText = "كود الباب"
            dgvChapters.Columns("DoorName").HeaderText = "اسم الباب"
            dgvChapters.Columns("ChapterCode").HeaderText = "كود الفصل"
            dgvChapters.Columns("ChapterName").HeaderText = "اسم الفصل"
            dgvChapters.Columns("IsActive").HeaderText = "نشط"
        End If

        dgvChapters.ClearSelection()
        SetStatus($"تم تحميل {dt.Rows.Count} فصل")
    End Sub

    Private Sub ClearForm()
        CurrentChapterId = 0
        cmbDoors.SelectedIndex = -1
        txtChapterCode.Text = ""
        txtChapterName.Text = ""
        chkIsActive.Checked = True
        cmbDoors.Focus()
    End Sub

    Private Function ValidateForm() As Boolean
        If cmbDoors.SelectedIndex < 0 Then
            MessageBox.Show("رجاءً اختر الباب أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbDoors.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtChapterCode.Text) Then
            MessageBox.Show("رجاءً أدخل كود الفصل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtChapterCode.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtChapterName.Text) Then
            MessageBox.Show("رجاءً أدخل اسم الفصل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtChapterName.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function ChapterCodeExists(doorId As Integer, chapterCode As String, excludeId As Integer) As Boolean
        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
SELECT COUNT(1)
FROM Budget_Chapters
WHERE DoorId = @DoorId
  AND ChapterCode = @Code
  AND ChapterId <> @ExcludeId;", cn)

                cmd.Parameters.AddWithValue("@DoorId", doorId)
                cmd.Parameters.AddWithValue("@Code", chapterCode.Trim())
                cmd.Parameters.AddWithValue("@ExcludeId", excludeId)

                Dim c As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return c > 0
            End Using
        End Using
    End Function

    Private Sub SaveChapter()
        If Not ValidateForm() Then Exit Sub

        Dim doorId As Integer = Convert.ToInt32(cmbDoors.SelectedValue)
        Dim code = txtChapterCode.Text.Trim()
        Dim name = txtChapterName.Text.Trim()
        Dim active = If(chkIsActive.Checked, 1, 0)

        If ChapterCodeExists(doorId, code, CurrentChapterId) Then
            MessageBox.Show("كود الفصل موجود مسبقًا داخل نفس الباب. رجاءً اختر كودًا آخر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtChapterCode.Focus()
            Exit Sub
        End If

        '⚠️ Audit Context: اربطه بمستخدمك الحقيقي
        Dim userId As Integer = 1
        Dim refNo As String = NewRefNo()

        Try
            SetUserContext(userId, refNo)

            Using cn As New SqlConnection(ConnStr)
                cn.Open()

                If CurrentChapterId = 0 Then
                    Using cmd As New SqlCommand("
INSERT INTO Budget_Chapters (DoorId, ChapterCode, ChapterName, IsActive)
VALUES (@DoorId, @Code, @Name, @IsActive);", cn)

                        cmd.Parameters.AddWithValue("@DoorId", doorId)
                        cmd.Parameters.AddWithValue("@Code", code)
                        cmd.Parameters.AddWithValue("@Name", name)
                        cmd.Parameters.AddWithValue("@IsActive", active)
                        cmd.ExecuteNonQuery()
                    End Using

                    SetStatus("تمت إضافة الفصل بنجاح")
                Else
                    Using cmd As New SqlCommand("
UPDATE Budget_Chapters
SET DoorId = @DoorId,
    ChapterCode = @Code,
    ChapterName = @Name,
    IsActive = @IsActive
WHERE ChapterId = @Id;", cn)

                        cmd.Parameters.AddWithValue("@DoorId", doorId)
                        cmd.Parameters.AddWithValue("@Code", code)
                        cmd.Parameters.AddWithValue("@Name", name)
                        cmd.Parameters.AddWithValue("@IsActive", active)
                        cmd.Parameters.AddWithValue("@Id", CurrentChapterId)
                        cmd.ExecuteNonQuery()
                    End Using

                    SetStatus("تم تعديل الفصل بنجاح")
                End If
            End Using

            LoadChapters(doorId)
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("خطأ أثناء الحفظ")
        End Try
    End Sub

    Private Sub SoftDeleteChapter()
        If CurrentChapterId = 0 Then
            MessageBox.Show("اختر فصلًا من القائمة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("هل تريد تعطيل هذا الفصل؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Dim userId As Integer = USER_ID
        Dim refNo As String = NewRefNo()

        Try
            SetUserContext(userId, refNo)

            Using cn As New SqlConnection(ConnStr)
                cn.Open()
                Using cmd As New SqlCommand("
UPDATE Budget_Chapters
SET IsActive = 0
WHERE ChapterId = @Id;", cn)

                    cmd.Parameters.AddWithValue("@Id", CurrentChapterId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            SetStatus("تم تعطيل الفصل")
            LoadChapters(If(cmbDoors.SelectedIndex >= 0, Convert.ToInt32(cmbDoors.SelectedValue), 0))
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("خطأ أثناء الحذف")
        End Try
    End Sub

    Private Sub FillFromGrid()
        If dgvChapters.CurrentRow Is Nothing Then Exit Sub

        Dim row = dgvChapters.CurrentRow
        CurrentChapterId = Convert.ToInt32(row.Cells("ChapterId").Value)

        Dim doorId As Integer = Convert.ToInt32(row.Cells("DoorId").Value)
        cmbDoors.SelectedValue = doorId

        txtChapterCode.Text = Convert.ToString(row.Cells("ChapterCode").Value)
        txtChapterName.Text = Convert.ToString(row.Cells("ChapterName").Value)
        chkIsActive.Checked = Convert.ToBoolean(row.Cells("IsActive").Value)

        SetStatus("وضع التعديل")
    End Sub

    Private Sub SetStatus(msg As String)
        lblStatus.Text = msg
    End Sub

    '=========================
    ' Events
    '=========================

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearForm()
        SetStatus("جديد")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveChapter()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        SoftDeleteChapter()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim doorIdFilter As Integer = If(cmbDoors.SelectedIndex >= 0, Convert.ToInt32(cmbDoors.SelectedValue), 0)
            LoadDoors()
            LoadChapters(doorIdFilter)
            SetStatus("تم التحديث")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvChapters_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChapters.CellClick
        FillFromGrid()
    End Sub

    Private Sub dgvChapters_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvChapters.CellDoubleClick
        FillFromGrid()
    End Sub

    'فلترة اختيارية: عند تغيير الباب اعرض فصوله فقط
    Private Sub cmbDoors_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDoors.SelectionChangeCommitted
        Try
            Dim doorId As Integer = Convert.ToInt32(cmbDoors.SelectedValue)
            LoadChapters(doorId)
        Catch
        End Try
    End Sub

    Private Sub exit_Btn_Click(sender As Object, e As EventArgs) Handles exit_Btn.Click
        Me.Close()
    End Sub
End Class
