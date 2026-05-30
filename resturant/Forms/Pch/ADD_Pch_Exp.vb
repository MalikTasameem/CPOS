Public Class ADD_Pch_Exp

    Private ReadOnly ExpBills_DT As New DataTable
    Private SelectedEXP_T_ID As Integer = 0
    Private SelectedEXP_Bill_ID As String = String.Empty
    Private SelectedEXP_Total As Double = 0

    Private Sub OrderDeliver_btn_Click(sender As Object, e As EventArgs) Handles OrderDeliver_btn.Click
        Pch_Details_Calc_Tax()
    End Sub
    Public Function Pch_Exp_Values_INSERT() As Boolean

        If Notes_cm.SelectedValue Is Nothing OrElse Notes_cm.SelectedValue Is DBNull.Value Then
            MsgBox("اختر بند المصروف أولا", MsgBoxStyle.Exclamation, "")
            Notes_cm.Focus()
            Return False
        End If

        Dim expenseValue As Double = 0
        If Double.TryParse(CD_Money_txt.Text, expenseValue) = False OrElse expenseValue <= 0 Then
            MsgBox("أدخل قيمة المصروف أولا", MsgBoxStyle.Exclamation, "")
            CD_Money_txt.Focus()
            Return False
        End If

        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[Pch_Exp_Values_INSERT]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Pch_T_ID", F_Pch.T_ID)
        sqlComm.Parameters.AddWithValue("@Notes_ID", Notes_cm.SelectedValue)
        If isWithBill_CB.Checked = True Then
            sqlComm.Parameters.AddWithValue("@Value", expenseValue * Convert.ToDouble(F_Pch.Cr_Equal_TXT.Text))
        Else
            sqlComm.Parameters.AddWithValue("@Value", expenseValue)
        End If
        sqlComm.Parameters.AddWithValue("@isWithBill", isWithBill_CB.Checked)

        If SQL_SP_EXEC(sqlComm) = True Then
            '    F_Pch.Pch_Contents_SELECT_Bill()
            F_Pch.Pch_Contents_SELECT_EXP()
            '    Me.Close()
            Return True
        End If

        Return False

    End Function

    Public Sub Pch_Details_Calc_Tax()

        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[Pch_Details_Calc_Tax]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Pch_T_ID", F_Pch.T_ID)
        sqlComm.Parameters.AddWithValue("@Value", 0)

        If SQL_SP_EXEC(sqlComm) = True Then
            F_Pch.Pch_Contents_SELECT_Bill()
            Me.Close()
        End If

    End Sub

    Private Sub ADD_Withraw_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub CD_Money_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CD_Money_txt.KeyPress
        If CD_Money_txt.ReadOnly = False Then Check_Only_Float(sender, e)
    End Sub

    Private Sub CD_Money_txt_TextChanged(sender As Object, e As EventArgs) Handles CD_Money_txt.TextChanged
        If CD_Money_txt.ReadOnly = False Then Check_Point_in_FloatNum(sender, e)
        OrderDeliver_btn.Enabled = True
    End Sub

    Private Sub ADD_Pch_Exp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        TextBox1.Text = "دينار ليبي"
        CD_Money_txt.Text = "0.000"
        OrderDeliver_btn.Enabled = True
        ExpBills_DGV.DataSource = ExpBills_DT
        Load_Notes()
        ApplyExpenseEntryMode()
    End Sub



    Public Sub Load_Notes()
        Dim c As New C
        Dim s As String = "select Ex_ID,Ex_Name from Expenses_Card ORDER BY Ex_Name Desc"
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        Notes_cm.DataSource = dt
        Notes_cm.DisplayMember = "Ex_Name"
        Notes_cm.ValueMember = "Ex_ID"
    End Sub

    Private Sub isWithBill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isWithBill_CB.CheckedChanged
        CB_CHecked(sender)
        If isWithBill_CB.Checked = True Then
            TextBox1.Text = F_Pch.Cr_CM.Text
        Else
            TextBox1.Text = "دينار ليبي"
        End If
        ApplyExpenseEntryMode()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If isWithBill_CB.Checked = False Then Exit Sub
        If Pch_Exp_Values_INSERT() Then
            CD_Money_txt.Text = "0.000"
            CD_Money_txt.Focus()
        End If
    End Sub

    Private Sub BrowseExpBill_btn_Click(sender As Object, e As EventArgs) Handles BrowseExpBill_btn.Click
        If isWithBill_CB.Checked Then Exit Sub
        Load_Expense_Bills()
        ApplyExpenseBillsFilter()
        ExpBills_DGV.Visible = True
        ExpBills_DGV.BringToFront()
        ExpBillNum_txt.Focus()
    End Sub

    Private Sub ImportExpBill_btn_Click(sender As Object, e As EventArgs) Handles ImportExpBill_btn.Click
        If isWithBill_CB.Checked Then Exit Sub
        If SelectedEXP_T_ID <= 0 OrElse ExpBillNum_txt.Text.Trim() <> SelectedEXP_Bill_ID Then
            If Select_Expense_Bill_ByNumber() = False Then Exit Sub
        End If

        Import_Expense_Bill_To_Pch()
    End Sub

    Private Sub ExpBillNum_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles ExpBillNum_txt.KeyDown
        If e.KeyCode = Keys.Return Then
            If Select_Expense_Bill_ByNumber() Then Import_Expense_Bill_To_Pch()
            e.SuppressKeyPress = True
        End If

        If e.KeyCode = Keys.Down AndAlso ExpBills_DGV.Visible Then ExpBills_DGV.Select()
    End Sub

    Private Sub ExpBillNum_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ExpBillNum_txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub ExpBillNum_txt_TextChanged(sender As Object, e As EventArgs) Handles ExpBillNum_txt.TextChanged
        If isWithBill_CB.Checked Then Exit Sub
        ApplyExpenseBillsFilter()
        If ExpBillNum_txt.Text.Trim() <> SelectedEXP_Bill_ID Then
            SelectedEXP_T_ID = 0
            SelectedEXP_Total = 0
            CD_Money_txt.Text = "0.000"
            ExpBillInfo_lb.Text = "أدخل رقم فاتورة المصروفات أو اخترها من الاستعراض"
        End If
    End Sub

    Private Sub ExpBills_DGV_KeyDown(sender As Object, e As KeyEventArgs) Handles ExpBills_DGV.KeyDown
        If e.KeyCode = Keys.Return Then
            Select_Expense_Bill_FromGrid(True)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub ExpBills_DGV_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ExpBills_DGV.MouseDoubleClick
        Select_Expense_Bill_FromGrid(True)
    End Sub

    Private Sub ExpBills_DGV_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles ExpBills_DGV.DataError
        e.ThrowException = False
    End Sub

    Private Sub Load_Expense_Bills()

        ExpBills_DT.Clear()

        Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlClient.SqlCommand("Balance_MV_V_SELECT", cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Form_Type", 8)
                cmd.Parameters.AddWithValue("@isVoid", False)
                cmd.Parameters.AddWithValue("@AG_ID", 0)

                Using da As New SqlClient.SqlDataAdapter(cmd)
                    da.Fill(ExpBills_DT)
                End Using
            End Using
        End Using

        ExpBills_DGV.DataSource = ExpBills_DT

    End Sub

    Private Function Select_Expense_Bill_ByNumber() As Boolean

        Dim expBillNumber As Long = 0
        If Long.TryParse(ExpBillNum_txt.Text.Trim(), expBillNumber) = False Then
            MsgBox("أدخل رقم فاتورة المصروفات أولا", MsgBoxStyle.Exclamation, "")
            ExpBillNum_txt.Focus()
            Return False
        End If

        Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlClient.SqlCommand("Select Top 1 T_ID, EXP_ID, ISNULL(isVoid, 0) As isVoid From Agents_Balance_MV Where EXP_ID = @EXP_ID And BsType_ID = 2 Order By T_ID Desc", cn)
                cmd.Parameters.AddWithValue("@EXP_ID", expBillNumber)
                cn.Open()

                Using dr As SqlClient.SqlDataReader = cmd.ExecuteReader()
                    If dr.Read() = False Then
                        MsgBox("لم يتم العثور على فاتورة المصروفات", MsgBoxStyle.Exclamation, "")
                        Return False
                    End If

                    If Convert.ToBoolean(dr("isVoid")) Then
                        MsgBox("فاتورة المصروفات المحددة ملغية", MsgBoxStyle.Exclamation, "")
                        Return False
                    End If

                    SelectedEXP_T_ID = Convert.ToInt32(dr("T_ID"))
                    SelectedEXP_Bill_ID = dr("EXP_ID").ToString()
                    ExpBillNum_txt.Text = SelectedEXP_Bill_ID
                End Using
            End Using
        End Using

        Update_Selected_Expense_Bill_Info()
        ExpBills_DGV.Visible = False
        Return True

    End Function

    Private Sub Select_Expense_Bill_FromGrid(Optional importAfterSelect As Boolean = False)

        If ExpBills_DGV.CurrentRow Is Nothing OrElse ExpBills_DGV.CurrentRow.IsNewRow Then Exit Sub
        If ExpBills_DGV.Columns.Contains("ExpBill_T_ID_CL") = False OrElse ExpBills_DGV.Columns.Contains("ExpBill_ID_CL") = False Then Exit Sub

        SelectedEXP_T_ID = GetGridCellIntegerValue(ExpBills_DGV.CurrentRow, "ExpBill_T_ID_CL")
        SelectedEXP_Bill_ID = GetGridCellTextValue(ExpBills_DGV.CurrentRow, "ExpBill_ID_CL")

        If SelectedEXP_T_ID <= 0 Then Exit Sub

        ExpBillNum_txt.Text = SelectedEXP_Bill_ID
        Update_Selected_Expense_Bill_Info()
        ExpBills_DGV.Visible = False
        If importAfterSelect Then Import_Expense_Bill_To_Pch()

    End Sub

    Private Sub Update_Selected_Expense_Bill_Info()

        If SelectedEXP_T_ID <= 0 Then
            ExpBillInfo_lb.Text = "أدخل رقم فاتورة المصروفات أو اخترها من الاستعراض"
            Return
        End If

        Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
            cn.Open()
            Dim detailsDt As DataTable = Load_Expense_Bill_Details(SelectedEXP_T_ID, cn, Nothing)
            Dim totalValue As Double = 0

            For Each row As DataRow In detailsDt.Rows
                totalValue += GetDataRowDoubleValue(row, "Total")
            Next

            SelectedEXP_Total = totalValue
            CD_Money_txt.Text = SelectedEXP_Total.ToString(N_Point_Fter)
            ExpBillInfo_lb.Text = "فاتورة مصروفات رقم " & SelectedEXP_Bill_ID & " | عدد البنود: " & detailsDt.Rows.Count.ToString() & " | الإجمالي: " & totalValue.ToString(N_Point_Fter)
        End Using

    End Sub

    Private Function Load_Expense_Bill_Details(expTId As Integer, cn As SqlClient.SqlConnection, tr As SqlClient.SqlTransaction) As DataTable

        Dim dt As New DataTable

        Using cmd As New SqlClient.SqlCommand("EXP_Details_SELECT_Bill", cn)
            cmd.CommandType = CommandType.StoredProcedure
            If tr IsNot Nothing Then cmd.Transaction = tr
            cmd.Parameters.AddWithValue("@Bill_T_ID", expTId)

            Using da As New SqlClient.SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using

        Return dt

    End Function

    Private Sub Import_Expense_Bill_To_Pch()

        If isWithBill_CB.Checked Then Exit Sub

        If F_Pch.T_ID <= 0 Then
            MsgBox("يجب فتح فاتورة المشتريات أولا", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        Dim addedCount As Integer = 0
        Dim addedTotal As Double = 0
        Dim rate As Double = GetPurchaseCurrencyRate()

        Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
            cn.Open()

            Using tr As SqlClient.SqlTransaction = cn.BeginTransaction()
                Try
                    Dim detailsDt As DataTable = Load_Expense_Bill_Details(SelectedEXP_T_ID, cn, tr)

                    If detailsDt.Rows.Count = 0 Then
                        tr.Rollback()
                        MsgBox("فاتورة المصروفات المحددة لا تحتوي على بنود", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    For Each row As DataRow In detailsDt.Rows
                        Dim notesId As Integer = GetDataRowIntegerValue(row, "EX_ID")
                        Dim value As Double = GetDataRowDoubleValue(row, "Total")

                        If notesId > 0 Then
                            If isWithBill_CB.Checked Then value *= rate
                            Insert_Pch_Expense_Value(cn, tr, notesId, value, isWithBill_CB.Checked)
                            addedCount += 1
                            addedTotal += value
                        End If
                    Next

                    If addedCount = 0 Then
                        tr.Rollback()
                        MsgBox("لم يتم العثور على بنود صالحة للإضافة", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    tr.Commit()

                Catch ex As Exception
                    tr.Rollback()
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "")
                    Exit Sub
                End Try
            End Using
        End Using

        F_Pch.Pch_Contents_SELECT_EXP()
        CD_Money_txt.Text = addedTotal.ToString(N_Point_Fter)
        ExpBillInfo_lb.Text = "تمت إضافة " & addedCount.ToString() & " بند من فاتورة رقم " & SelectedEXP_Bill_ID & " | الإجمالي: " & addedTotal.ToString(N_Point_Fter)
        MsgBox("تمت إضافة بنود فاتورة المصروفات", MsgBoxStyle.Information, "")

    End Sub

    Private Sub Insert_Pch_Expense_Value(cn As SqlClient.SqlConnection, tr As SqlClient.SqlTransaction, notesId As Integer, value As Double, isWithBill As Boolean)

        Using cmd As New SqlClient.SqlCommand("[Pch_Exp_Values_INSERT]", cn, tr)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Pch_T_ID", F_Pch.T_ID)
            cmd.Parameters.AddWithValue("@Notes_ID", notesId)
            cmd.Parameters.AddWithValue("@Value", value)
            cmd.Parameters.AddWithValue("@isWithBill", isWithBill)
            cmd.ExecuteNonQuery()
        End Using

    End Sub

    Private Function GetPurchaseCurrencyRate() As Double

        Dim rate As Double = 1

        If F_Pch.Cr_Equal_TXT IsNot Nothing AndAlso Double.TryParse(F_Pch.Cr_Equal_TXT.Text, rate) Then
            If rate <= 0 Then rate = 1
        Else
            rate = 1
        End If

        Return rate

    End Function

    Private Sub ApplyExpenseEntryMode()

        Dim withSupplierBill As Boolean = isWithBill_CB.Checked

        Notes_cm.Visible = withSupplierBill
        Label9.Visible = withSupplierBill
        Button1.Visible = withSupplierBill

        Label1.Visible = Not withSupplierBill
        ExpBillNum_txt.Visible = Not withSupplierBill
        BrowseExpBill_btn.Visible = Not withSupplierBill
        ImportExpBill_btn.Visible = Not withSupplierBill
        ExpBillInfo_lb.Visible = Not withSupplierBill
        If withSupplierBill Then ExpBills_DGV.Visible = False

        CD_Money_txt.ReadOnly = Not withSupplierBill
        CD_Money_txt.BackColor = If(withSupplierBill, Color.White, SystemColors.Control)
        Label3.Text = If(withSupplierBill, "القيمة", "الإجمالي")

        If withSupplierBill Then
            SelectedEXP_T_ID = 0
            SelectedEXP_Bill_ID = String.Empty
            SelectedEXP_Total = 0
            ExpBillNum_txt.Clear()
            ExpBillInfo_lb.Text = "أدخل رقم فاتورة المصروفات أو اخترها من الاستعراض"
            CD_Money_txt.Text = "0.000"
        Else
            CD_Money_txt.Text = SelectedEXP_Total.ToString(N_Point_Fter)
        End If

    End Sub

    Private Sub ApplyExpenseBillsFilter()

        If ExpBills_DT Is Nothing OrElse ExpBills_DT.Rows.Count = 0 Then Exit Sub

        Dim filterText As String = ExpBillNum_txt.Text.Trim().Replace("'", "''")
        If String.IsNullOrWhiteSpace(filterText) Then
            ExpBills_DT.DefaultView.RowFilter = String.Empty
        ElseIf ExpBills_DT.Columns.Contains("Search") Then
            ExpBills_DT.DefaultView.RowFilter = "Convert(Bill_ID, 'System.String') Like '%" & filterText & "%' Or Search Like '%" & filterText & "%'"
        Else
            ExpBills_DT.DefaultView.RowFilter = "Convert(Bill_ID, 'System.String') Like '%" & filterText & "%'"
        End If

    End Sub

    Private Function GetDataRowDoubleValue(row As DataRow, columnName As String) As Double

        If row Is Nothing OrElse row.Table.Columns.Contains(columnName) = False Then Return 0
        If row(columnName) Is DBNull.Value OrElse row(columnName) Is Nothing Then Return 0

        Dim value As Double = 0
        If Double.TryParse(row(columnName).ToString(), value) Then Return value
        Return 0

    End Function

    Private Function GetDataRowIntegerValue(row As DataRow, columnName As String) As Integer

        If row Is Nothing OrElse row.Table.Columns.Contains(columnName) = False Then Return 0
        If row(columnName) Is DBNull.Value OrElse row(columnName) Is Nothing Then Return 0

        Dim value As Integer = 0
        If Integer.TryParse(row(columnName).ToString(), value) Then Return value
        Return 0

    End Function

    Private Function GetGridCellIntegerValue(row As DataGridViewRow, columnName As String) As Integer

        If row Is Nothing OrElse row.DataGridView Is Nothing OrElse row.DataGridView.Columns.Contains(columnName) = False Then Return 0
        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then Return 0

        Dim value As Integer = 0
        If Integer.TryParse(row.Cells(columnName).Value.ToString(), value) Then Return value
        Return 0

    End Function

    Private Function GetGridCellTextValue(row As DataGridViewRow, columnName As String) As String

        If row Is Nothing OrElse row.DataGridView Is Nothing OrElse row.DataGridView.Columns.Contains(columnName) = False Then Return String.Empty
        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then Return String.Empty

        Return row.Cells(columnName).Value.ToString()

    End Function
End Class
