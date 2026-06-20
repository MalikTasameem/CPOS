Imports System.Data.SqlClient
Imports System.IO

Public Class FrmExchangeCreate

    Private connectionString As String = MY_Settings.SqlConStr
    Private CurrentExchangeId As Long = 0
    Private DefaultCommissionPercent As Decimal = 0D

    '========================================
    ' FORM LOAD
    '========================================
    Private Sub FrmExchangeCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Try
        PrepareDocsTable()

        LoadCurrencies()
        LoadOperationTypes()
        'LoadVaults()
        LoadCommissionPercent()
        CalculateValues()
        LoadOperationAccountsSettings(cmbOperationType.SelectedItem.ToString())
        LoadRate()

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    '========================================
    ' LOAD DATA
    '========================================

    Private Sub LoadOperationTypes()
        cmbOperationType.Items.Clear()
        cmbOperationType.Items.Add("SellCurrency")
        cmbOperationType.Items.Add("BuyCurrency")
        cmbOperationType.SelectedIndex = 0
    End Sub

    Private Sub LoadVaults()

        Using con As New SqlConnection(connectionString)
            Dim dt As New DataTable

            Dim da As New SqlDataAdapter(
                "SELECT MainAccountId,Main_ACC FROM ExchangeOperationAccounts_V WHERE OperationType = '" & cmbOperationType.Text & "' ", con)

            da.Fill(dt)

            cmbVault.DataSource = dt
            cmbVault.DisplayMember = "Main_ACC"
            cmbVault.ValueMember = "MainAccountId"
        End Using

    End Sub

    Private Sub LoadCurrencies()

        Using con As New SqlConnection(connectionString)
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(
                "SELECT DISTINCT Cr_ID, Cr_Name 
                 FROM dbo.Currency", con)

            da.Fill(dt)

            cmbCurrency.DataSource = dt
            cmbCurrency.DisplayMember = "Cr_Name"
            cmbCurrency.ValueMember = "Cr_ID"
        End Using

    End Sub

    Private Sub LoadCommissionPercent()

        If BASIC_RATE = 1 Then
            ' يمكنك استبداله بقراءة من ExchangeSettings
            DefaultCommissionPercent = GetDefaultCommissionPercent()
            txtCommissionPercent.Text = DefaultCommissionPercent.ToString("0.####")
        Else
            DefaultCommissionPercent = 0
            txtCommissionPercent.Text = DefaultCommissionPercent.ToString("0.####")
        End If


    End Sub

    Public Function GetDefaultCommissionPercent() As Decimal

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            con.Open()

            Dim cmd As New SqlCommand("
            SELECT TOP 1 DefaultCommissionPercent
            FROM ExchangeSettings
            ORDER BY Id DESC", con)

            Dim result = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return Convert.ToDecimal(result)
            End If
        End Using

        Return 0D

    End Function


    '========================================
    ' LOAD RATE AUTOMATICALLY
    '========================================
    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        'If TypeName(cmbCurrency.SelectedValue) <> "Integer" Then
        '    Exit Sub
        'End If
        LoadRate()
    End Sub

    Private Sub btnRefreshRate_Click(sender As Object, e As EventArgs) Handles btnRefreshRate.Click
        LoadRate()
    End Sub

    Private Sub Set_Cr()

        If TypeName(cmbVault.SelectedValue) <> "String" Then
            Exit Sub
        End If

        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand("
            SELECT TOP 1 Cr_ID
            FROM ExchangeOperationAccounts_V 
            WHERE OperationType = @OperationType AND MainAccountId = @MainAccountId", con)

                cmd.Parameters.AddWithValue("@OperationType", cmbOperationType.Text)
                cmd.Parameters.AddWithValue("@MainAccountId", cmbVault.SelectedValue)
                con.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        cmbCurrency.SelectedValue = reader("Cr_ID")
                    Else
                        cmbCurrency.SelectedValue = 1
                    End If

                End Using
            End Using
        End Using

    End Sub

    Private Sub LoadRate()

        'On Error Resume Next

        Try

            'If TypeName(cmbVault.SelectedValue) <> "integar" Then
            '    Exit Sub
            'End If

            'If TypeName(cmbCurrency.SelectedValue) <> "integar" Then
            '    Exit Sub
            'End If

            If cmbCurrency.SelectedValue Is Nothing Then Exit Sub

            Set_Cr()

            Using con As New SqlConnection(connectionString)
                con.Open()

                Dim cmd As SqlCommand

                If cmbOperationType.SelectedIndex = 0 Then
                    cmd = New SqlCommand("
                SELECT TOP 1 Price
                FROM dbo.Currency_Schedule_V
                WHERE Cr_ID = @CrId
                AND GETDATE() BETWEEN D_F AND D_T
                ORDER BY D_F DESC", con)
                Else
                    cmd = New SqlCommand("
                SELECT TOP 1 BuyPrice
                FROM dbo.Currency_Schedule_V
                WHERE Cr_ID = @CrId
                AND GETDATE() BETWEEN D_F AND D_T
                ORDER BY D_F DESC", con)

                End If


                cmd.Parameters.AddWithValue("@CrId", cmbCurrency.SelectedValue)

                Dim result = cmd.ExecuteScalar()

                If result IsNot Nothing Then
                    numRate.Value = Convert.ToDecimal(result)
                    NoRateMsg_Label.Visible = False
                Else
                    numRate.Value = 1
                    NoRateMsg_Label.Visible = True
                    'MessageBox.Show("لا يوجد سعر فعال لهذه العملة حالياً")
                End If
                Dim B As Decimal = GetVaultBalance(cmbVault.SelectedValue)
                B = B / numRate.Value

                Dim P As Decimal = GetVaultPendingBalance(cmbVault.SelectedValue)


                If BASIC_RATE = 0 Then txtCommissionPercent.Text = 0


                Tittle_balance_Label.Text = "رصيد الخزينة (" & cmbCurrency.Text & ")"
                Tr_Balance_Lb.Text = (B).ToString("N")

                Tittle_pendingbalance_Label.Text = "رصيد المحجوز (" & cmbCurrency.Text & ")"
                Tr_Balance_Pending_Lb.Text = P.ToString("N")


                Tittle_Total_balance_Label.Text = "رصيد الكلي (" & cmbCurrency.Text & ")"
                Tr_Total_Balance_Lb.Text = (B - P).ToString("N")

                numForeignAmount.Maximum = (B - P).ToString("N")

                If numForeignAmount.Maximum <= 0 Then
                    btnSavePending.Enabled = False
                    numForeignAmount.Enabled = False
                Else
                    btnSavePending.Enabled = True
                    numForeignAmount.Enabled = True
                    numForeignAmount.Value = 0
                End If


                '"رصيد الخزينة (دينار ليبي) = " & B.ToString("N") & vbNewLine & "رصيد الخزينة (" & cmbCurrency.Text & ") = " & (B / numRate.Value).ToString("N")

            End Using

            CalculateValues()

        Catch ex As Exception
            '   MsgBox(ex.Message)
        End Try

    End Sub

    '========================================
    ' CALCULATIONS
    '========================================
    Private Sub numForeignAmount_ValueChanged(sender As Object, e As EventArgs) Handles numForeignAmount.ValueChanged
        CalculateValues()
    End Sub

    Private Sub numRate_ValueChanged(sender As Object, e As EventArgs) Handles numRate.ValueChanged
        CalculateValues()
    End Sub

    Private Sub CalculateValues()

        Dim total As Decimal = numForeignAmount.Value * numRate.Value
        Dim commission As Decimal = total * (DefaultCommissionPercent / 100D)
        Dim net As Decimal = total + commission

        lblTotalLYD.Text = total.ToString("N3")
        lblCommissionLYD.Text = commission.ToString("N3")
        lblNetLYD.Text = net.ToString("N3")

    End Sub

    '========================================
    ' SAVE PENDING
    '========================================
    'Private Sub btnSavePending_Click(sender As Object, e As EventArgs) Handles btnSavePending.Click

    '    If cmbVault.SelectedValue Is Nothing Then
    '        MessageBox.Show("يجب اختيار الخزنة")
    '        Exit Sub
    '    End If

    '    If cmbCurrency.SelectedValue Is Nothing Then
    '        MessageBox.Show("يجب اختيار العملة")
    '        Exit Sub
    '    End If

    '    If numForeignAmount.Value <= 0 Then
    '        MessageBox.Show("المبلغ يجب أن يكون أكبر من صفر")
    '        Exit Sub
    '    End If


    '    Dim result As DialogResult = MessageBox.Show(
    '"هل أنت متأكد من حفظ العملية بحالة Pending ؟" & vbCrLf &
    '"سيتم تسجيل العملية ولا يمكن تعديل بعض البيانات لاحقاً.",
    '"تأكيد الحفظ",
    'MessageBoxButtons.YesNo,
    'MessageBoxIcon.Question,
    'MessageBoxDefaultButton.Button2)

    '    If result = DialogResult.No Then
    '        Exit Sub
    '    End If

    '    Try
    '        Using con As New SqlConnection(connectionString)
    '            con.Open()

    '            Dim cmd As New SqlCommand("CreateExchangePending", con)
    '            cmd.CommandType = CommandType.StoredProcedure

    '            cmd.Parameters.AddWithValue("@OperationType", cmbOperationType.Text)
    '            cmd.Parameters.AddWithValue("@VaultId", cmbVault.SelectedValue)
    '            cmd.Parameters.AddWithValue("@CurrencyId", cmbCurrency.SelectedValue)
    '            cmd.Parameters.AddWithValue("@ForeignAmount", numForeignAmount.Value)
    '            cmd.Parameters.AddWithValue("@Rate", numRate.Value)
    '            cmd.Parameters.AddWithValue("@CommissionPercent", DefaultCommissionPercent)
    '            cmd.Parameters.AddWithValue("@ReferenceNo", txtReferenceNo.Text)
    '            cmd.Parameters.AddWithValue("@Note", txtNote.Text)
    '            cmd.Parameters.AddWithValue("@CreatedBy", USER_ID)

    '            cmd.Parameters.AddWithValue("@CustomerName", txt_CustomerName.Text)
    '            cmd.Parameters.AddWithValue("@CustomerIdentityNumber", txt_CustomerIdentityNumber.Text)

    '            Dim outputId As New SqlParameter("@ExchangeId", SqlDbType.BigInt)
    '            outputId.Direction = ParameterDirection.Output
    '            cmd.Parameters.Add(outputId)

    '            cmd.ExecuteNonQuery()
    '            CurrentExchangeId = CLng(outputId.Value)

    '            tsslStatus.Text = "تم الحفظ Pending"
    '            tsslExchangeId.Text = "رقم العملية: " & CurrentExchangeId.ToString()



    '            For Each dr As DataRow In dtDocs.Rows
    '                dr("B_T_ID") = CurrentExchangeId
    '            Next
    '            DocGridView.Refresh()
    '            SaveDocsBulk()

    '            MessageBox.Show("تم حفظ العملية Pending بنجاح")
    '            btnSavePending.Enabled = False

    '            btn_Print.Enabled = True
    '        End Using
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try



    'End Sub


    Private Sub btnSavePending_Click(sender As Object, e As EventArgs) Handles btnSavePending.Click

        If cmbVault.SelectedValue Is Nothing Then
            MessageBox.Show("يجب اختيار الخزنة")
            Exit Sub
        End If

        If cmbCurrency.SelectedValue Is Nothing Then
            MessageBox.Show("يجب اختيار العملة")
            Exit Sub
        End If

        If numForeignAmount.Value <= 0 Then
            MessageBox.Show("المبلغ يجب أن يكون أكبر من صفر")
            Exit Sub
        End If

        Dim result As DialogResult = MessageBox.Show(
        "هل أنت متأكد من حفظ العملية بحالة Pending ؟" & vbCrLf &
        "سيتم تسجيل العملية ولا يمكن تعديل بعض البيانات لاحقاً.",
        "تأكيد الحفظ",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button2)

        If result = DialogResult.No Then Exit Sub

        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                Using trans As SqlTransaction = con.BeginTransaction()

                    Try
                        '=====================================
                        ' 1️⃣ إنشاء العملية Pending
                        '=====================================

                        Dim cmd As New SqlCommand("CreateExchangePending", con, trans)
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.AddWithValue("@OperationType", cmbOperationType.Text)
                        cmd.Parameters.AddWithValue("@VaultId", cmbVault.SelectedValue)
                        cmd.Parameters.AddWithValue("@CurrencyId", cmbCurrency.SelectedValue)
                        cmd.Parameters.AddWithValue("@ForeignAmount", numForeignAmount.Value)
                        cmd.Parameters.AddWithValue("@Rate", numRate.Value)
                        cmd.Parameters.AddWithValue("@CommissionPercent", DefaultCommissionPercent)
                        cmd.Parameters.AddWithValue("@ReferenceNo", txtReferenceNo.Text)
                        cmd.Parameters.AddWithValue("@Note", txtNote.Text)
                        cmd.Parameters.AddWithValue("@CreatedBy", USER_ID)
                        cmd.Parameters.AddWithValue("@CustomerName", txt_CustomerName.Text)
                        cmd.Parameters.AddWithValue("@CustomerIdentityNumber", txt_CustomerIdentityNumber.Text)

                        Dim outputId As New SqlParameter("@ExchangeId", SqlDbType.BigInt)
                        outputId.Direction = ParameterDirection.Output
                        cmd.Parameters.Add(outputId)

                        cmd.ExecuteNonQuery()

                        CurrentExchangeId = CLng(outputId.Value)

                        '=====================================
                        ' 2️⃣ إضافة ExchangeId للمرفقات
                        '=====================================

                        If dtDocs IsNot Nothing AndAlso dtDocs.Rows.Count > 0 Then

                            If Not dtDocs.Columns.Contains("B_T_ID") Then
                                dtDocs.Columns.Add("B_T_ID", GetType(Long))
                            End If

                            For Each dr As DataRow In dtDocs.Rows
                                dr("B_T_ID") = CurrentExchangeId
                            Next

                            '=====================================
                            ' 3️⃣ Bulk Insert داخل نفس Transaction
                            '=====================================

                            Using bulk As New SqlBulkCopy(con, SqlBulkCopyOptions.Default, trans)

                                bulk.DestinationTableName = "dbo.ExchangeTransactions_DOCS"

                                bulk.ColumnMappings.Add("B_T_ID", "B_T_ID")
                                bulk.ColumnMappings.Add("DOC", "DOC")
                                bulk.ColumnMappings.Add("DATE", "DATE")
                                bulk.ColumnMappings.Add("Extended", "Extended")
                                bulk.ColumnMappings.Add("USER_ID", "USER_ID")
                                bulk.ColumnMappings.Add("NOTES", "NOTES")

                                bulk.WriteToServer(dtDocs)

                            End Using
                        End If

                        '=====================================
                        ' 4️⃣ Commit
                        '=====================================
                        trans.Commit()

                        tsslStatus.Text = "تم الحفظ Pending"
                        tsslExchangeId.Text = "رقم العملية: " & CurrentExchangeId.ToString()

                        MessageBox.Show("تم حفظ العملية والمرفقات بنجاح ✔")

                        btnSavePending.Enabled = False
                        btn_Print.Enabled = True

                    Catch ex As Exception
                        trans.Rollback()
                        MessageBox.Show("حدث خطأ وتم إلغاء العملية بالكامل: " & ex.Message)
                    End Try

                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        numForeignAmount.Maximum = 0
        numForeignAmount.Value = 0
        txtReferenceNo.Clear()
        txtNote.Clear()
        CurrentExchangeId = 0
        btnOpenDetails.Visible = False
        tsslExchangeId.Text = ""
        txt_CustomerName.Clear()
        txt_CustomerIdentityNumber.Clear()
        CalculateValues()
        btnSavePending.Enabled = True
        btn_Print.Enabled = False
        dtDocs.Clear()
        LoadRate()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub cmbOperationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOperationType.SelectedIndexChanged

        If cmbOperationType.SelectedItem Is Nothing Then Exit Sub

        LoadOperationAccountsSettings(cmbOperationType.SelectedItem.ToString())
        LoadRate()
        LoadCommissionPercent()
    End Sub

    Dim BASIC_RATE As Decimal = 0

    'Private Sub LoadOperationAccountsSettings(operationType As String)
    '    BASIC_RATE = 1
    '    Try

    '        Using con As New SqlConnection(MY_Settings.SqlConStr)

    '            con.Open()

    '            Dim cmd As New SqlCommand("
    '            SELECT MainAccountId,
    '                   CommissionAccountId,
    '                   SecondAccountId
    '            FROM ExchangeOperationAccounts
    '            WHERE OperationType = @Type", con)

    '            cmd.Parameters.AddWithValue("@Type", operationType)

    '            Dim reader = cmd.ExecuteReader()

    '            If reader.Read() Then

    '                ' الخزينة الرئيسية
    '                cmbVault.SelectedValue = reader("MainAccountId")

    '                If IsDBNull(reader("CommissionAccountId")) Then BASIC_RATE = 0

    '                '' حساب العمولة (إن كان عندك Combo خاص به)
    '                'If cmbCommissionAccount IsNot Nothing Then
    '                '    cmbCommissionAccount.SelectedValue = reader("CommissionAccountId")
    '                'End If

    '                '' الحساب الثاني إن وجد
    '                'If cmbSecondAccount IsNot Nothing Then
    '                '    cmbSecondAccount.SelectedValue = reader("SecondAccountId")
    '                'End If

    '            End If

    '                reader.Close()

    '        End Using

    '    Catch ex As Exception
    '        MessageBox.Show("خطأ في تحميل إعدادات العملية: " & ex.Message)
    '    End Try

    'End Sub


    Private Sub LoadOperationAccountsSettings(operationType As String)

        BASIC_RATE = 1

        Try
            Using con As New SqlConnection(MY_Settings.SqlConStr)
                con.Open()

                Dim cmd As New SqlCommand("
                SELECT MainAccountId,Main_ACC,
                       CommissionAccountId,
                       SecondAccountId
                FROM ExchangeOperationAccounts_V
                WHERE OperationType = @Type", con)

                cmd.Parameters.AddWithValue("@Type", operationType)

                Using reader As SqlDataReader = cmd.ExecuteReader()

                    ' إنشاء DataTables لكل Combo
                    Dim dtVault As New DataTable
                    dtVault.Columns.Add("Value")
                    dtVault.Columns.Add("Text")

                    Dim dtCommission As New DataTable
                    dtCommission.Columns.Add("Value")
                    dtCommission.Columns.Add("Text")

                    Dim dtSecond As New DataTable
                    dtSecond.Columns.Add("Value")
                    dtSecond.Columns.Add("Text")

                    While reader.Read()

                        ' -------------------
                        ' MainAccount
                        ' -------------------
                        If Not IsDBNull(reader("MainAccountId")) Then
                            Dim KEY = reader("MainAccountId").ToString()
                            Dim VALUE = reader("Main_ACC").ToString()
                            dtVault.Rows.Add(KEY, VALUE)
                        End If

                        ' -------------------
                        ' CommissionAccount
                        ' -------------------
                        If Not IsDBNull(reader("CommissionAccountId")) Then
                            'Dim val = reader("CommissionAccountId").ToString()
                            'dtCommission.Rows.Add(val, val)
                            BASIC_RATE = 1
                        Else
                            BASIC_RATE = 0
                        End If

                        ' -------------------
                        ' SecondAccount
                        ' -------------------
                        'If Not IsDBNull(reader("SecondAccountId")) Then
                        '    Dim val = reader("SecondAccountId").ToString()
                        '    dtSecond.Rows.Add(val, val)
                        'End If

                    End While

                    ' ربط البيانات بالـ ComboBoxes

                    cmbVault.DataSource = dtVault
                    cmbVault.DisplayMember = "Text"
                    cmbVault.ValueMember = "Value"

                    'cmbCommissionAccount.DataSource = dtCommission
                    'cmbCommissionAccount.DisplayMember = "Text"
                    'cmbCommissionAccount.ValueMember = "Value"

                    'cmbSecondAccount.DataSource = dtSecond
                    'cmbSecondAccount.DisplayMember = "Text"
                    'cmbSecondAccount.ValueMember = "Value"

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("خطأ في تحميل إعدادات العملية: " & ex.Message)
        End Try

    End Sub
    Private Sub FillSingleValueCombo(cmb As ComboBox, KEY As String, value As String)

        Dim dt As New DataTable
        dt.Columns.Add("Value")
        dt.Columns.Add("Text")

        dt.Rows.Add(KEY, value)

        cmb.DataSource = dt
        cmb.DisplayMember = "Text"
        cmb.ValueMember = "Value"

    End Sub


    Private Sub btn_Print_Click(sender As Object, e As EventArgs) Handles btn_Print.Click
        ' تخزين رقم العملية للطباعة
        PrintTransactionId = CurrentExchangeId

        '' استدعاء الطباعة
        'pd.Print()

        PrintPendingReceipt(PrintTransactionId)

    End Sub


    Private Sub cmbVault_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVault.SelectedIndexChanged
        LoadRate()
    End Sub

    Private Sub ADD_Doc_btn_Click(sender As Object, e As EventArgs) Handles ADD_Doc_btn.Click

        Dim F As New NewArchive
        F.ShowDialog()
        If F.ValFalg = True Then
            'DocGridView.Rows.Add(DocGridView.Rows.Count + 1, F.sFileName, F.imageData)
            AddDocumentRow(0, F.imageData, F.sFileName, USER_ID, F.sFileTittle)
        End If

    End Sub


    ' 1) جهّز DataTable مرتبط بالـ Grid
    'Private dt As New DataTable()

    Private dtDocs As New DataTable()

    Private Sub PrepareDocsTable()

        dtDocs.Columns.Add("B_T_ID", GetType(Integer))
        dtDocs.Columns.Add("DOC", GetType(Byte()))
        dtDocs.Columns.Add("DATE", GetType(DateTime))
        dtDocs.Columns.Add("Extended", GetType(String))
        dtDocs.Columns.Add("USER_ID", GetType(Integer))
        dtDocs.Columns.Add("NOTES", GetType(String))

        DocGridView.DataSource = dtDocs

        ' لا تعرض الملف
        If DocGridView.Columns.Contains("DOC") Then
            DocGridView.Columns("DOC").Visible = False
        End If

        If DocGridView.Columns.Contains("B_T_ID") Then
            DocGridView.Columns("B_T_ID").Visible = False
        End If

        If DocGridView.Columns.Contains("USER_ID") Then
            DocGridView.Columns("USER_ID").Visible = False
        End If

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

        ' زر حذف
        Dim btnDelete As New DataGridViewButtonColumn()
        btnDelete.Name = "btnDelete"
        btnDelete.HeaderText = "حذف"
        btnDelete.Text = "❌"
        btnDelete.UseColumnTextForButtonValue = True
        DocGridView.Columns.Add(btnDelete)

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

    Public Sub AddDocumentRow(
    ByVal btId As Integer,
    ByVal fileBytes As Byte(),
    ByVal extension As String,
    ByVal userId As Integer,
    Optional ByVal notes As String = "بدون عنوان"
)

        If dtDocs.Columns.Count = 0 Then
            PrepareDocsTable()
        End If

        dtDocs.Rows.Add(
            btId,
            fileBytes,
            DateTime.Now,
            extension,
            userId,
            notes
        )

    End Sub

    ' 2) إضافة صف جديد مع الملف
    'Public Sub AddFileRow(fileName As String, fileBytes As Byte())
    '    If dt.Columns.Count = 0 Then PrepareDocsTable()

    '    Dim sizeKb As Integer = CInt(Math.Ceiling(fileBytes.Length / 1024.0))
    '    dt.Rows.Add(fileName, sizeKb, fileBytes)
    'End Sub


End Class
