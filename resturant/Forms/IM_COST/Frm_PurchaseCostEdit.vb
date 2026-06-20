Imports System.Data.SqlClient

Public Class Frm_PurchaseCostEdit

    Public Property IM_ID As Integer
    Public Property PurchaseDate As DateTime

    Private ConnectionString As String = MY_Settings.SqlConStr

    Private CurrentBatchId As Guid

    Private Sub Frm_PurchaseCostEdit_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load
        ' تطبيق الثيم الإجباري
        ThemeManager.ApplyThemeToForm(Me)
        Txt_Diff.Text = "0.000000"

    End Sub

    Private Sub Txt_NewPrice_TextChanged(
        sender As Object,
        e As EventArgs
    ) Handles Txt_NewPrice.TextChanged

        Dim oldPrice As Decimal = 0
        Dim newPrice As Decimal = 0

        Decimal.TryParse(Txt_OldPrice.Text, oldPrice)
        Decimal.TryParse(Txt_NewPrice.Text, newPrice)

        Txt_Diff.Text =
            (newPrice - oldPrice).ToString("N6")

    End Sub

    Private Function CheckNeedRecount() As Boolean

        Using cn As New SqlConnection(ConnectionString)

            Using cmd As New SqlCommand(
                "dbo.InventoryCostRecount_CheckNeed",
                cn
            )

                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@IM_ID", IM_ID)
                cmd.Parameters.AddWithValue("@FromDate", PurchaseDate)

                cn.Open()

                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.Read() Then

                    Return Convert.ToBoolean(rdr("NeedRecount"))

                End If

            End Using

        End Using

        Return False

    End Function

    Private Function RunPreview() As Guid

        Dim batchId As Guid = Guid.Empty

        Using cn As New SqlConnection(ConnectionString)

            Using cmd As New SqlCommand(
                "dbo.InventoryCostRecount_Preview",
                cn
            )

                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@IM_ID", IM_ID)
                cmd.Parameters.AddWithValue("@FromDate", PurchaseDate)
                cmd.Parameters.AddWithValue("@CreatedBy", 1)

                cn.Open()

                Dim da As New SqlDataAdapter(cmd)

                Dim ds As New DataSet()

                da.Fill(ds)

                If ds.Tables.Count > 0 Then

                    If ds.Tables(0).Rows.Count > 0 Then

                        batchId =
                            Guid.Parse(
                                ds.Tables(0).Rows(0)("BatchId").ToString()
                            )

                    End If

                End If

            End Using

        End Using

        Return batchId

    End Function

    Private Sub SavePriceOnly()

        Dim newPrice As Decimal = 0
        Dim purchaseDetailId As Long = 0

        Decimal.TryParse(Txt_NewPrice.Text, newPrice)
        Long.TryParse(Txt_PurchaseDetailId.Text, purchaseDetailId)

        Using cn As New SqlConnection(ConnectionString)

            cn.Open()

            Dim tr As SqlTransaction = cn.BeginTransaction()

            Try

                Dim cmd As New SqlCommand("
                    UPDATE dbo.Pch_Details
                    SET Price = @Price
                    WHERE T_ID = @T_ID
                ", cn, tr)

                cmd.Parameters.AddWithValue("@Price", newPrice)
                cmd.Parameters.AddWithValue("@T_ID", purchaseDetailId)

                cmd.ExecuteNonQuery()

                tr.Commit()

                MessageBox.Show(
                    "تم حفظ السعر بنجاح.",
                    "نجاح",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

            Catch ex As Exception

                tr.Rollback()

                MessageBox.Show(
                    ex.Message,
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                )

            End Try

        End Using

    End Sub

    Private Sub Btn_SaveOnly_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_SaveOnly.Click

        SavePriceOnly()

    End Sub

    Private Sub Btn_CheckImpact_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_CheckImpact.Click

        Try

            If String.IsNullOrWhiteSpace(Txt_NewPrice.Text) Then

                MessageBox.Show(
                    "أدخل السعر الجديد.",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                Return

            End If

            If CheckNeedRecount() = False Then

                SavePriceOnly()

                MessageBox.Show(
                    "لا توجد حركات لاحقة تحتاج إعادة احتساب.",
                    "معلومة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

                Return

            End If

            CurrentBatchId = RunPreview()

            If CurrentBatchId = Guid.Empty Then

                MessageBox.Show(
                    "فشل إنشاء مستند إعادة الاحتساب.",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                )

                Return

            End If

            Dim frm As New Frm_InventoryCostRecountPreview

            frm.BatchId = CurrentBatchId

            frm.ShowDialog()

        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    Private Sub Btn_Close_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Close.Click

        Me.Close()

    End Sub

End Class