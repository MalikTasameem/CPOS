Imports System.Data.SqlClient

Public Class FrmExchangeDetails

    Private _row As DataRowView

    Public Sub New(row As DataRowView)
        InitializeComponent()
        _row = row
    End Sub

    Private Sub FrmExchangeDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtExchangeId.Text = _row("ExchangeId").ToString()
        txtCustomerName.Text = _row("CustomerName").ToString()
        txtCustomerIdentity.Text = _row("CustomerIdentityNumber").ToString()
        txtReferenceNo.Text = _row("ReferenceNo").ToString()
        txtCreatedAt.Text = _row("CreatedAt").ToString()

        txtOperationType.Text = _row("OperationType").ToString()
        txtVault.Text = _row("VaultName").ToString()
        txtCurrency.Text = _row("CurrencyName").ToString()
        txtStatus.Text = _row("StatusName").ToString()

        txtForeignAmount.Text = FormatNumber(_row("ForeignAmount"), 3)
        txtRate.Text = FormatNumber(_row("RateSnapshot"), 3)
        txtCommissionPercent.Text = _row("CommissionPercentSnapshot").ToString()
        txtCommissionLYD.Text = FormatNumber(_row("CommissionLYD"), 3)
        txtTotalLYD.Text = FormatNumber(_row("TotalLYD"), 3)
        txtNetLYD.Text = FormatNumber(_row("NetLYD"), 3)

        LoadDocs(txtExchangeId.Text)

    End Sub

    Private Sub LoadDocs(exchangeId As Long)

        Dim dtDocs = New DataTable()

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
            SELECT 
                T_ID,
                B_T_ID,
                DOC,
                [DATE],
                Extended,
                USER_ID,
                NOTES
            FROM ExchangeTransactions_DOCS
            WHERE B_T_ID = @ExchangeId
            ORDER BY T_ID DESC", con)

                cmd.Parameters.AddWithValue("@ExchangeId", exchangeId)

                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dtDocs)

            End Using
        End Using

        DocGridView.DataSource = dtDocs

        ' إخفاء عمود البايتات
        If DocGridView.Columns.Contains("DOC") Then
            DocGridView.Columns("DOC").Visible = False
        End If

        If DocGridView.Columns.Contains("B_T_ID") Then
            DocGridView.Columns("B_T_ID").Visible = False
        End If

        If DocGridView.Columns.Contains("USER_ID") Then
            DocGridView.Columns("USER_ID").Visible = False
        End If

        ' تنسيق الأعمدة
        DocGridView.Columns("T_ID").HeaderText = "رقم"
        DocGridView.Columns("DATE").HeaderText = "التاريخ"
        DocGridView.Columns("Extended").HeaderText = "النوع"
        DocGridView.Columns("NOTES").HeaderText = "ملاحظات"


        SetupGridButtons()

    End Sub


    Private Sub SetupGridButtons()

        ' زر معاينة
        Dim btnPreview As New DataGridViewButtonColumn()
        btnPreview.Name = "btnPreview"
        btnPreview.HeaderText = "عرض"
        btnPreview.Text = "🔍"
        btnPreview.UseColumnTextForButtonValue = True
        DocGridView.Columns.Add(btnPreview)

        '' زر حذف
        'Dim btnDelete As New DataGridViewButtonColumn()
        'btnDelete.Name = "btnDelete"
        'btnDelete.HeaderText = "حذف"
        'btnDelete.Text = "❌"
        'btnDelete.UseColumnTextForButtonValue = True
        'DocGridView.Columns.Add(btnDelete)

    End Sub

    Private Sub gridDocs_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) _
Handles DocGridView.CellContentClick

        If e.RowIndex < 0 Then Exit Sub

        ' زر المعاينة
        If DocGridView.Columns(e.ColumnIndex).Name = "btnPreview" Then

            Dim fileBytes As Byte() =
            CType(DocGridView.Rows(e.RowIndex).Cells("DOC").Value, Byte())

            If fileBytes Is Nothing Then Exit Sub

            Dim tempPath As String =
            IO.Path.Combine(IO.Path.GetTempPath(), Guid.NewGuid().ToString() & ".tmp")

            IO.File.WriteAllBytes(tempPath, fileBytes)

            Process.Start(tempPath)

        End If

        ' زر الحذف
        If DocGridView.Columns(e.ColumnIndex).Name = "btnDelete" Then

            If MessageBox.Show("هل تريد حذف هذا الملف؟",
                           "تأكيد",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then

                DocGridView.Rows.RemoveAt(e.RowIndex)

            End If

        End If

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class