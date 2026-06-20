Imports System.Data.SqlClient

Public Class FrmExchangeManager

    Private connectionString As String = MY_Settings.SqlConStr
    Dim dt As New DataTable

    '===========================
    ' FORM LOAD
    '===========================
    Private Sub FrmExchangeManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Zuby.ADGV.AdvancedDataGridViewSearchToolBar.SetTranslations(Zuby.ADGV.AdvancedDataGridViewSearchToolBar.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))
        LoadVaults()
        LoadStatus()
        LoadData()
        LoadSearchColumns()

        dgv.Columns("ForeignAmount").Tag = "1"
        dgv.Columns("TotalLYD").Tag = "1"
        dgv.Columns("CommissionLYD").Tag = "1"
        dgv.Columns("NetLYD").Tag = "1"

    End Sub
    Private Sub LoadSearchColumns()

        cmbSearchColumn.Items.Clear()

        For Each col As DataGridViewColumn In dgv.Columns
            If col.Visible Then
                cmbSearchColumn.Items.Add(New With {
                .Text = col.HeaderText,
                .Value = col.Name
            })

                CheckedListBox1.Items.Add(col.HeaderText)
            End If
        Next

        cmbSearchColumn.DisplayMember = "Text"
        cmbSearchColumn.ValueMember = "Value"

        If cmbSearchColumn.Items.Count > 0 Then
            cmbSearchColumn.SelectedIndex = 0
        End If

    End Sub



    '===========================
    ' LOAD VAULTS
    '===========================
    Private Sub LoadVaults()

        Using con As New SqlConnection(connectionString)

            Dim da As New SqlDataAdapter("
                SELECT ACC_CODE, ACC_NAME
                FROM dbo.Rct_Mang_V
                WHERE ACC_Type = 2", con)

            da.Fill(dt)

            cmbVault.DataSource = dt
            cmbVault.DisplayMember = "ACC_NAME"
            cmbVault.ValueMember = "ACC_CODE"
        End Using

    End Sub

    '===========================
    ' LOAD STATUS
    '===========================
    Private Sub LoadStatus()

        Dim dt As New DataTable
        dt.Columns.Add("StatusId", GetType(Integer))
        dt.Columns.Add("StatusName", GetType(String))

        dt.Rows.Add(DBNull.Value, "الكل")
        dt.Rows.Add(1, "Pending")
        dt.Rows.Add(2, "Approved")
        dt.Rows.Add(3, "Rejected")

        cmbStatus.DataSource = dt
        cmbStatus.DisplayMember = "StatusName"
        cmbStatus.ValueMember = "StatusId"

    End Sub

    '===========================
    ' LOAD DATA
    '===========================
    Private Sub LoadData()

        dt = New DataTable
        Using con As New SqlConnection(connectionString)
            'Dim dt As New DataTable
            Dim cmd As New SqlCommand("GetExchangeTransactions", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@DateFrom", dtpFrom.Value.Date)
            cmd.Parameters.AddWithValue("@DateTo", dtpTo.Value.Date)

            If cmbStatus.SelectedValue IsNot DBNull.Value Then
                cmd.Parameters.AddWithValue("@StatusId", cmbStatus.SelectedValue)
            Else
                cmd.Parameters.AddWithValue("@StatusId", DBNull.Value)
            End If

            If cmbVault.SelectedValue IsNot Nothing Then
                cmd.Parameters.AddWithValue("@VaultId", cmbVault.SelectedValue)
            Else
                cmd.Parameters.AddWithValue("@VaultId", DBNull.Value)
            End If

            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)

            dgv.DataSource = dt
            CalculateTotals()
            'advancedDataGridViewSearchToolBar_main.SetColumns(dgv.Columns)

            'AddButtonColumns()



        End Using

        ColorizeRows()

    End Sub

    'Private Sub advancedDataGridViewSearchToolBar_main_Search(sender As Object, e As Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs) Handles advancedDataGridViewSearchToolBar_main.Search
    '    Dim restartsearch = True
    '    Dim startColumn = 0
    '    Dim startRow = 0
    '    If Not e.FromBegin Then
    '        Dim endcol As Boolean = dgv.CurrentCell.ColumnIndex + 1 >= dgv.ColumnCount
    '        Dim endrow As Boolean = dgv.CurrentCell.RowIndex + 1 >= dgv.RowCount

    '        If endcol AndAlso endrow Then
    '            startColumn = dgv.CurrentCell.ColumnIndex
    '            startRow = dgv.CurrentCell.RowIndex
    '        Else
    '            startColumn = If(endcol, 0, dgv.CurrentCell.ColumnIndex + 1)
    '            startRow = dgv.CurrentCell.RowIndex + If(endcol, 1, 0)
    '        End If
    '    End If
    '    Dim c As DataGridViewCell = dgv.FindCell(e.ValueToSearch, If(e.ColumnToSearch IsNot Nothing, e.ColumnToSearch.Name, Nothing), startRow, startColumn, e.WholeWord, e.CaseSensitive)
    '    If c Is Nothing AndAlso restartsearch Then c = dgv.FindCell(e.ValueToSearch, If(e.ColumnToSearch IsNot Nothing, e.ColumnToSearch.Name, Nothing), 0, 0, e.WholeWord, e.CaseSensitive)
    '    If c IsNot Nothing Then dgv.CurrentCell = c
    'End Sub

    Private Sub AddButtonColumns()

        If Not dgv.Columns.Contains("Print_CL") Then

            Dim btnEdit As New DataGridViewButtonColumn()
            btnEdit.Name = "Print_CL"
            btnEdit.HeaderText = ""
            btnEdit.Text = "🖨️"
            btnEdit.UseColumnTextForButtonValue = True
            dgv.Columns.Add(btnEdit)

        End If

        'If Not dgv.Columns.Contains("Show_CL") Then

        '    Dim btnDelete As New DataGridViewButtonColumn()
        '    btnDelete.Name = "Show_CL"
        '    btnDelete.HeaderText = "🔎"
        '    btnDelete.Text = "🔎"
        '    btnDelete.UseColumnTextForButtonValue = True
        '    dgv.Columns.Add(btnDelete)

        'End If

    End Sub

    '===========================
    ' COLORIZE ROWS
    '===========================
    Private Sub ColorizeRows()

        For Each row As DataGridViewRow In dgv.Rows

            Dim statusId As Integer = Convert.ToInt32(row.Cells("StatusId").Value)

            If statusId = 1 Then
                row.DefaultCellStyle.BackColor = Color.LightYellow
            ElseIf statusId = 2 Then
                row.DefaultCellStyle.BackColor = Color.Honeydew
            ElseIf statusId = 3 Then
                row.DefaultCellStyle.BackColor = Color.MistyRose
            End If

        Next

    End Sub

    '===========================
    ' SEARCH
    '===========================
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadData()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub

    '===========================
    ' APPROVE
    '===========================
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click


        If dgv.CurrentRow Is Nothing Then Exit Sub

        Dim accCode As String
        Dim balance As Decimal


        Dim exchangeId As Long = CLng(dgv.CurrentRow.Cells("ExchangeId").Value)
        Dim statusId As Integer = CInt(dgv.CurrentRow.Cells("StatusId").Value)

        If statusId <> 1 Then
            MessageBox.Show("يمكن اعتماد العمليات Pending فقط")
            Exit Sub
        End If

        '========================
        ' قراءة بيانات العملية
        '========================
        Dim rateSnapshot As Decimal = CDec(dgv.CurrentRow.Cells("RateSnapshot").Value)
        Dim currencyId As Integer = CInt(dgv.CurrentRow.Cells("ForeignCurrencyId").Value)
        Dim total As Decimal = CDec(dgv.CurrentRow.Cells("TotalLYD").Value)
        Dim vaultId As Integer = CInt(dgv.CurrentRow.Cells("VaultId").Value)
        Dim operationType As String = dgv.CurrentRow.Cells("OperationType").Value.ToString()

        '========================
        ' التحقق من تغير السعر
        '========================
        Dim currentRate As Decimal = GetCurrentRate(currencyId)

        If currentRate <> rateSnapshot Then
            MessageBox.Show("⚠ السعر تغير منذ إنشاء العملية!" & Environment.NewLine &
                        "السعر الحالي: " & currentRate.ToString("N6"),
                        "تحذير",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            'Exit Sub
        End If

        '========================
        ' التحقق من الرصيد
        '========================
        If operationType = "BuyCurrency" Then

            accCode = vaultId.ToString()
            balance = GetVaultBalance(accCode)

            If balance < total Then
                MessageBox.Show("رصيد الخزنة غير كافي." & Environment.NewLine &
                            "الرصيد الحالي: " & balance.ToString("N3"),
                            "خطأ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        '========================
        ' تأكيد نهائي
        '========================
        'If MessageBox.Show("هل تريد اعتماد العملية؟",
        '               "Confirm Approve",
        '               MessageBoxButtons.YesNo,
        '               MessageBoxIcon.Question) = DialogResult.No Then Exit Sub



        '========================
        ' حساب الرصيد بعد العملية
        '========================
        accCode = vaultId.ToString()
        balance = GetVaultBalance(accCode)
        Dim balanceAfter As Decimal = balance

        If operationType = "BuyCurrency" Then
            balanceAfter = balance + total
        Else
            balanceAfter = balance - total
        End If

        '========================
        ' عرض شاشة التأكيد
        '========================
        Dim frm As New FrmExchangeApprovePreview

        frm.lblInfo.Text =
    "رقم العملية: " & exchangeId & Environment.NewLine &
    "النوع: " & operationType & Environment.NewLine &
    "الإجمالي: " & total.ToString("N3") & Environment.NewLine &
    "الرصيد الحالي: " & balance.ToString("N3") & Environment.NewLine &
    "الرصيد بعد العملية: " & balanceAfter.ToString("N3")

        frm.ShowDialog()

        If Not frm.Confirmed Then Exit Sub





        '========================
        ' تنفيذ الاعتماد
        '========================
        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim cmd As New SqlCommand("ApproveExchange", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@ExchangeId", exchangeId)
            cmd.Parameters.AddWithValue("@ApprovedBy", USER_ID)

            cmd.ExecuteNonQuery()

            InsertExchangeAudit(exchangeId,
                    "Approve",
                    balance,
                    balanceAfter,
                    rateSnapshot,
                    currentRate,
                    "تم اعتماد العملية بنجاح")


        End Using

        MessageBox.Show("تم اعتماد العملية بنجاح")

        LoadData()

        '--------------------------------------------------------------------------------------------------------------------------

    End Sub


    Private Function GetCurrentRate(currencyId As Integer) As Decimal

        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim cmd As New SqlCommand("
            SELECT TOP 1 Price
            FROM Tree_Test.dbo.Currency_Schedule_V
            WHERE Cr_ID = @CrId
            AND GETDATE() BETWEEN D_F AND D_T
            ORDER BY D_F DESC", con)

            cmd.Parameters.AddWithValue("@CrId", currencyId)

            Dim result = cmd.ExecuteScalar()

            If result IsNot Nothing Then
                Return Convert.ToDecimal(result)
            End If
        End Using

        Return 0D

    End Function



    '===========================
    ' REJECT
    '===========================
    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click

        If dgv.CurrentRow Is Nothing Then Exit Sub

        Dim exchangeId As Long = CLng(dgv.CurrentRow.Cells("ExchangeId").Value)
        Dim statusId As Integer = CInt(dgv.CurrentRow.Cells("StatusId").Value)

        If statusId <> 1 Then
            MessageBox.Show("يمكن رفض العمليات Pending فقط")
            Exit Sub
        End If

        Dim reason As String = InputBox("أدخل سبب الرفض")

        If reason = "" Then Exit Sub

        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim cmd As New SqlCommand("
                UPDATE ExchangeTransactions
                SET StatusId = 3,
                    RejectedAt = SYSDATETIME(),
                    RejectedBy = @UserId,
                    RejectReason = @Reason
                WHERE ExchangeId = @Id", con)

            cmd.Parameters.AddWithValue("@UserId", USER_ID)
            cmd.Parameters.AddWithValue("@Reason", reason)
            cmd.Parameters.AddWithValue("@Id", exchangeId)

            cmd.ExecuteNonQuery()

            InsertExchangeAudit(exchangeId,
                    "Reject",
                    0,
                    0,
                    0,
                    0,
                    reason)

        End Using

        LoadData()

    End Sub

    '===========================
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub InsertExchangeAudit(exchangeId As Long,
                                 actionType As String,
                                 balanceBefore As Decimal,
                                 balanceAfter As Decimal,
                                 rateSnapshot As Decimal,
                                 currentRate As Decimal,
                                 details As String)

        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim cmd As New SqlCommand("
            INSERT INTO ExchangeAuditLog
            (
                ExchangeId,
                ActionType,
                UserId,
                MachineName,
                WindowsUser,
                BalanceBefore,
                BalanceAfter,
                RateSnapshot,
                CurrentRate,
                Details
            )
            VALUES
            (
                @ExchangeId,
                @ActionType,
                @UserId,
                @MachineName,
                @WindowsUser,
                @BalanceBefore,
                @BalanceAfter,
                @RateSnapshot,
                @CurrentRate,
                @Details
            )", con)

            cmd.Parameters.AddWithValue("@ExchangeId", exchangeId)
            cmd.Parameters.AddWithValue("@ActionType", actionType)
            cmd.Parameters.AddWithValue("@UserId", USER_ID)
            cmd.Parameters.AddWithValue("@MachineName", Environment.MachineName)
            cmd.Parameters.AddWithValue("@WindowsUser", Environment.UserName)
            cmd.Parameters.AddWithValue("@BalanceBefore", balanceBefore)
            cmd.Parameters.AddWithValue("@BalanceAfter", balanceAfter)
            cmd.Parameters.AddWithValue("@RateSnapshot", rateSnapshot)
            cmd.Parameters.AddWithValue("@CurrentRate", currentRate)
            cmd.Parameters.AddWithValue("@Details", details)

            cmd.ExecuteNonQuery()
        End Using

    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        ' تجاهل الضغط على الهيدر
        If e.RowIndex < 0 Then Exit Sub

        ' زر تعديل
        If dgv.Columns(e.ColumnIndex).Name = "Print_CL" Then
            Dim transactionId As Integer = CInt(dgv.CurrentRow.Cells("ExchangeId").Value)
            PrintPendingReceipt(transactionId)
        End If

    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgv.CellDoubleClick

        If dgv.CurrentRow Is Nothing Then Exit Sub

        ' نتأكد أن الصف مربوط فعلياً
        Dim drv As DataRowView = TryCast(dgv.CurrentRow.DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub

        Dim frm As New FrmExchangeDetails(drv)
        frm.ShowDialog()

    End Sub



    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

        If dt Is Nothing Then Exit Sub
        If cmbSearchColumn.SelectedItem Is Nothing Then Exit Sub

        Dim dv As New DataView(dt)
        Dim columnName As String = cmbSearchColumn.SelectedItem.Value

        Dim searchText As String = txtSearch.Text.Trim().Replace("'", "''")

        If searchText <> "" Then
            dv.RowFilter = $"Convert([{columnName}], 'System.String') LIKE '%{searchText}%'"
        End If

        dgv.DataSource = dv
        CalculateTotals()

    End Sub



    Private Sub CalculateTotals()

        Dim totalAmount As Decimal = 0
        Dim totalApproved As Decimal = 0
        Dim totalPending As Decimal = 0
        Dim count As Integer = dgv.Rows.Count

        For Each row As DataGridViewRow In dgv.Rows

            If row.IsNewRow Then Continue For

            Dim amount As Decimal = 0
            Decimal.TryParse(row.Cells("ForeignAmount").Value?.ToString(), amount)

            totalAmount += amount

            Dim status As String = row.Cells("StatusName").Value?.ToString()

            If status = "Approved" Then
                totalApproved += amount
            ElseIf status = "Pending" Then
                totalPending += amount
            End If

        Next

        lblCount.Text = "عدد العمليات: " & count
        lblTotalAmount.Text = "الإجمالي: " & Format(totalAmount, "N2")
        lblTotalApproved.Text = "المعتمد: " & Format(totalApproved, "N2")
        lblTotalPending.Text = "قيد الانتظار: " & Format(totalPending, "N2")

    End Sub

    Private Sub Print_Btn_Click(sender As Object, e As EventArgs) Handles Print_Btn.Click
        Dim f As New Print_PDF
        f.PRINT_PDF_List(dgv, CheckedListBox1, "تقرير حالات الصرافة" & " - (" & cmbStatus.Text & ")  - " & "للفترة من : " & dtpFrom.Text & " إلى: " & dtpTo.Text, 1)
    End Sub
End Class
