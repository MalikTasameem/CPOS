Imports System.Data
Imports System.Data.SqlClient

Public Class FrmBudgetDoors

    '✅ غيّر الاتصال حسب بيئتك (أو اربطه بـ My.Settings)
    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr

    'تخزين الـ DoorId الحالي للتعديل
    Private CurrentDoorId As Integer = 0

    'اختياري: رقم مرجعي للـ Audit (نولده من الوقت)
    Private Function NewRefNo() As String
        Return "BD-" & DateTime.Now.ToString("yyyyMMdd-HHmmss")
    End Function

    '⚠️ مهم لـ SQL Server 2014: ضبط سياق المستخدم للـ Audit
    Private Sub SetUserContext(userId As Integer, refNo As String)
        Using cn As New SqlConnection(ConnStr)
            cn.Open()

            'نضمن صف واحد لكل SPID
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

    Private Sub FrmBudgetDoors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGridStyle()
            BudgetUiHelper.ApplyBudgetFormStyle(Me)
            LoadDoors()
            ClearForm()
            SetStatus("جاهز")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub ApplyGridStyle()
        dgvDoors.EnableHeadersVisualStyles = False
        dgvDoors.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvDoors.ColumnHeadersHeight = 38
        dgvDoors.RowTemplate.Height = 34

        dgvDoors.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 242, 245)
        dgvDoors.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 35, 35)
        dgvDoors.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)

        dgvDoors.DefaultCellStyle.Font = New Font("Segoe UI", 10.0!, FontStyle.Regular)
        dgvDoors.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 237, 255)
        dgvDoors.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30)

        dgvDoors.GridColor = Color.FromArgb(235, 235, 235)
    End Sub

    Private Sub LoadDoors()
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT
    DoorId,
    DoorCode,
    DoorName,
    IsActive
FROM Budget_Doors
ORDER BY DoorCode;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvDoors.DataSource = dt

        'تحسين أسماء الأعمدة
        If dgvDoors.Columns.Count > 0 Then
            dgvDoors.Columns("DoorId").Visible = False
            dgvDoors.Columns("DoorCode").HeaderText = "الكود"
            dgvDoors.Columns("DoorName").HeaderText = "الاسم"
            dgvDoors.Columns("IsActive").HeaderText = "نشط"
        End If

        dgvDoors.ClearSelection()
        SetStatus($"تم تحميل {dt.Rows.Count} باب")
    End Sub

    Private Sub ClearForm()
        CurrentDoorId = 0
        txtDoorCode.Text = ""
        txtDoorName.Text = ""
        chkIsActive.Checked = True
        txtDoorCode.Focus()
    End Sub

    Private Function ValidateForm() As Boolean
        If String.IsNullOrWhiteSpace(txtDoorCode.Text) Then
            MessageBox.Show("رجاءً أدخل كود الباب", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDoorCode.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtDoorName.Text) Then
            MessageBox.Show("رجاءً أدخل اسم الباب", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDoorName.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function DoorCodeExists(code As String, excludeId As Integer) As Boolean
        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
SELECT COUNT(1)
FROM Budget_Doors
WHERE DoorCode = @Code
  AND DoorId <> @ExcludeId;", cn)

                cmd.Parameters.AddWithValue("@Code", code.Trim())
                cmd.Parameters.AddWithValue("@ExcludeId", excludeId)

                Dim c As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return c > 0
            End Using
        End Using
    End Function

    Private Sub SaveDoor()
        If Not ValidateForm() Then Exit Sub

        Dim code = txtDoorCode.Text.Trim()
        Dim name = txtDoorName.Text.Trim()
        Dim active = If(chkIsActive.Checked, 1, 0)

        'تحقق uniqueness للكود
        If DoorCodeExists(code, CurrentDoorId) Then
            MessageBox.Show("كود الباب موجود مسبقًا. رجاءً اختر كودًا آخر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDoorCode.Focus()
            Exit Sub
        End If

        '⚠️ Audit Context: ضع UserId الحقيقي من نظامك
        Dim userId As Integer = USER_ID
        Dim refNo As String = NewRefNo()

        Try
            SetUserContext(userId, refNo)

            Using cn As New SqlConnection(ConnStr)
                cn.Open()

                If CurrentDoorId = 0 Then
                    'INSERT
                    Using cmd As New SqlCommand("
INSERT INTO Budget_Doors (DoorCode, DoorName, IsActive)
VALUES (@Code, @Name, @IsActive);", cn)

                        cmd.Parameters.AddWithValue("@Code", code)
                        cmd.Parameters.AddWithValue("@Name", name)
                        cmd.Parameters.AddWithValue("@IsActive", active)
                        cmd.ExecuteNonQuery()
                    End Using

                    SetStatus("تمت إضافة الباب بنجاح")
                Else
                    'UPDATE
                    Using cmd As New SqlCommand("
UPDATE Budget_Doors
SET DoorCode = @Code,
    DoorName = @Name,
    IsActive = @IsActive
WHERE DoorId = @Id;", cn)

                        cmd.Parameters.AddWithValue("@Code", code)
                        cmd.Parameters.AddWithValue("@Name", name)
                        cmd.Parameters.AddWithValue("@IsActive", active)
                        cmd.Parameters.AddWithValue("@Id", CurrentDoorId)
                        cmd.ExecuteNonQuery()
                    End Using

                    SetStatus("تم تعديل الباب بنجاح")
                End If
            End Using

            LoadDoors()
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("خطأ أثناء الحفظ")
        End Try
    End Sub

    Private Sub SoftDeleteDoor()
        If CurrentDoorId = 0 Then
            MessageBox.Show("اختر بابًا من القائمة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("هل تريد تعطيل هذا الباب؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        '⚠️ Audit Context
        Dim userId As Integer = 1
        Dim refNo As String = NewRefNo()

        Try
            SetUserContext(userId, refNo)

            Using cn As New SqlConnection(ConnStr)
                cn.Open()
                Using cmd As New SqlCommand("
UPDATE Budget_Doors
SET IsActive = 0
WHERE DoorId = @Id;", cn)

                    cmd.Parameters.AddWithValue("@Id", CurrentDoorId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            SetStatus("تم تعطيل الباب")
            LoadDoors()
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("خطأ أثناء الحذف")
        End Try
    End Sub

    Private Sub FillFromGrid()
        If dgvDoors.CurrentRow Is Nothing Then Exit Sub

        Dim row = dgvDoors.CurrentRow
        If row Is Nothing OrElse row.DataBoundItem Is Nothing Then Exit Sub

        CurrentDoorId = Convert.ToInt32(row.Cells("DoorId").Value)
        txtDoorCode.Text = Convert.ToString(row.Cells("DoorCode").Value)
        txtDoorName.Text = Convert.ToString(row.Cells("DoorName").Value)
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
        SaveDoor()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        SoftDeleteDoor()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadDoors()
            dgvDoors.ClearSelection()
            SetStatus("تم التحديث")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvDoors_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDoors.CellClick
        FillFromGrid()
    End Sub

    Private Sub dgvDoors_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDoors.CellDoubleClick
        FillFromGrid()
    End Sub

    Private Sub exit_Btn_Click(sender As Object, e As EventArgs) Handles exit_Btn.Click
        Me.Close()
    End Sub
End Class
