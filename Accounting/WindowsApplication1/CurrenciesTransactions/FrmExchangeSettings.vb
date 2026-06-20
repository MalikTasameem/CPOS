Imports System.Data.SqlClient

Public Class FrmExchangeSettings

    Private connectionString As String = MY_Settings.SqlConStr
    Private currentSettingId As Integer = 0

    '=========================================
    ' FORM LOAD
    '=========================================
    Private Sub FrmExchangeSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            LoadAccounts()
            LoadSettings()
        Catch ex As Exception
            MessageBox.Show("خطأ أثناء تحميل البيانات: " & ex.Message)
        End Try

    End Sub

    '=========================================
    ' تحميل حسابات الإيرادات
    '=========================================
    Private Sub LoadAccounts()

        'Using con As New SqlConnection(connectionString)

        '    Dim dt As New DataTable

        '    ' عدل ACC_Type حسب نظامك إن لزم
        '    'Dim da As New SqlDataAdapter("
        '    '    SELECT CommissionAccountId
        '    '    FROM dbo.[ExchangeOperationAccounts]", con)

        '    'da.Fill(dt)

        '    'cmbAccount.DataSource = dt
        '    'cmbAccount.DisplayMember = "ACC_NAME"
        '    'cmbAccount.ValueMember = "T_ID"

        'End Using

        Using con As New SqlConnection(connectionString)

            con.Open()

            Dim cmd As New SqlCommand(" SELECT top 1 ISNULL(CommissionAccountId,0) AS CommissionAccountId FROM dbo.[ExchangeOperationAccounts] WHERE OperationType = 'SellCurrency'  ", con)

            Dim reader = cmd.ExecuteReader()

            If reader.Read() Then
                txtAccount.Text = CDec(reader("CommissionAccountId"))
            Else

                lblUpdatedInfo.Text = "لا توجد إعدادات محفوظة خاصة بجدول الصرافة"

            End If

            reader.Close()

        End Using

    End Sub

    '=========================================
    ' تحميل الإعداد الحالي
    '=========================================
    Private Sub LoadSettings()

        Using con As New SqlConnection(connectionString)

            con.Open()

            Dim cmd As New SqlCommand("
                SELECT TOP 1 *
                FROM ExchangeSettings
                ORDER BY Id DESC", con)

            Dim reader = cmd.ExecuteReader()

            If reader.Read() Then

                currentSettingId = CInt(reader("Id"))

                numPercent.Value = CDec(reader("DefaultCommissionPercent"))

                cmbAccount.SelectedValue = CInt(reader("CommissionAccountId"))

                lblUpdatedInfo.Text =
                    "آخر تحديث: " &
                    Convert.ToDateTime(reader("UpdatedAt")).ToString("yyyy-MM-dd HH:mm")

            Else

                currentSettingId = 0
                numPercent.Value = 0
                lblUpdatedInfo.Text = "لا توجد إعدادات محفوظة"

            End If

            reader.Close()

        End Using

    End Sub

    '=========================================
    ' زر تحديث
    '=========================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        Try
            LoadSettings()
        Catch ex As Exception
            MessageBox.Show("خطأ أثناء التحديث: " & ex.Message)
        End Try

    End Sub

    '=========================================
    ' زر حفظ
    '=========================================
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try

            'If cmbAccount.SelectedValue Is Nothing Then
            '    MessageBox.Show("يجب اختيار حساب العمولة")
            '    Exit Sub
            'End If


            'If txtAccount.Text Is Nothing Then
            '    MessageBox.Show("يجب اختيار حساب العمولة")
            '    Exit Sub
            'End If


            If MessageBox.Show("هل تريد حفظ التعديلات؟",
                               "تأكيد",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

            Using con As New SqlConnection(connectionString)

                con.Open()

                If currentSettingId = 0 Then

                    ' أول مرة → INSERT
                    Dim cmdInsert As New SqlCommand("
                        INSERT INTO ExchangeSettings
                        (DefaultCommissionPercent, CommissionAccountId, UpdatedBy)
                        VALUES
                        (@Percent, @AccountId, @UserId)", con)

                    cmdInsert.Parameters.AddWithValue("@Percent", numPercent.Value)
                    cmdInsert.Parameters.AddWithValue("@AccountId", txtAccount.Text)
                    cmdInsert.Parameters.AddWithValue("@UserId", USER_ID)

                    cmdInsert.ExecuteNonQuery()

                Else

                    ' UPDATE
                    Dim cmdUpdate As New SqlCommand("
                        UPDATE ExchangeSettings
                        SET DefaultCommissionPercent = @Percent,
                            CommissionAccountId = @AccountId,
                            UpdatedAt = SYSDATETIME(),
                            UpdatedBy = @UserId
                        WHERE Id = @Id", con)

                    cmdUpdate.Parameters.AddWithValue("@Percent", numPercent.Value)
                    cmdUpdate.Parameters.AddWithValue("@AccountId", txtAccount.Text)
                    cmdUpdate.Parameters.AddWithValue("@UserId", USER_ID)
                    cmdUpdate.Parameters.AddWithValue("@Id", currentSettingId)

                    cmdUpdate.ExecuteNonQuery()

                End If

            End Using

            MessageBox.Show("تم حفظ الإعدادات بنجاح")

            LoadSettings()

        Catch ex As Exception
            MessageBox.Show("خطأ أثناء الحفظ: " & ex.Message)
        End Try

    End Sub

    '=========================================
    ' زر إغلاق
    '=========================================
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class
