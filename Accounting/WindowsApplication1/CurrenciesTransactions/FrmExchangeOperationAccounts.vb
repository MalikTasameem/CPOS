Imports System.Data.SqlClient

Public Class FrmExchangeOperationAccounts

    Private connectionString As String = MY_Settings.SqlConStr
    ' Private Tr_accountsTable As DataTable
    Private accountsTable As DataTable
    Private crTable As DataTable
    '=========================================
    ' FORM LOAD
    '=========================================
    Private Sub FrmExchangeOperationAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadAccounts()
        LoadOperationAccounts()

        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255)
        dgv.DefaultCellStyle.SelectionForeColor = Color.Black

    End Sub

    Private Sub btnDeleteRow_Click(sender As Object, e As EventArgs) Handles btnDeleteRow.Click

        If dgv.CurrentRow Is Nothing Then Exit Sub

        Dim result = MessageBox.Show("هل تريد حذف هذا السطر؟",
                                 "تأكيد",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question)

        If result = DialogResult.No Then Exit Sub

        ' إذا كان السطر مرتبط بقاعدة البيانات
        If dgv.CurrentRow.Cells("Id").Value IsNot DBNull.Value Then

            Dim rowId As Integer = CInt(dgv.CurrentRow.Cells("Id").Value)

            Using con As New SqlConnection(connectionString)
                con.Open()
                Dim cmd As New SqlCommand("DELETE FROM ExchangeOperationAccounts WHERE Id=@Id", con)
                cmd.Parameters.AddWithValue("@Id", rowId)
                cmd.ExecuteNonQuery()
            End Using

        End If

        dgv.Rows.Remove(dgv.CurrentRow)

    End Sub

    Private Sub LoadAccounts()

        Using con As New SqlConnection(connectionString)

            accountsTable = New DataTable

            Dim da As New SqlDataAdapter("
                SELECT NULL AS ACC_CODE, '-- بدون تحديد --' AS  ACC_NAME
                UNION ALL
                SELECT ACC_CODE, ACC_NAME FROM dbo.ACCOUNTS_TREE ORDER BY ACC_NAME
                ", con)

            da.Fill(accountsTable)

        End Using

        Using con As New SqlConnection(connectionString)

            crTable = New DataTable

            Dim da As New SqlDataAdapter("
                SELECT Cr_ID, Cr_NAME FROM Currency ORDER BY Cr_NAME ", con)

            da.Fill(crTable)

        End Using

    End Sub

    '=========================================
    ' تحميل بيانات الجدول
    '=========================================
    Private Sub LoadOperationAccounts()

        Using con As New SqlConnection(connectionString)

            Dim dt As New DataTable

            Dim da As New SqlDataAdapter("
                SELECT *
                FROM ExchangeOperationAccounts
                ORDER BY OperationType", con)

            da.Fill(dt)

            dgv.Columns.Clear()
            dgv.DataSource = Nothing

            dgv.DataSource = dt

            ' اجعل OperationType غير قابل للتعديل للصفوف الموجودة فقط (الصف الجديد يُسمح بتعديله)
            dgv.Columns("OperationType").ReadOnly = False
            For Each r As DataGridViewRow In dgv.Rows
                If r.IsNewRow Then Continue For
                r.Cells("OperationType").ReadOnly = True
                r.Cells("ExchangeTypeId").ReadOnly = True
            Next

            ' تحويل الأعمدة إلى ComboBox
            ConvertToComboColumn("MainAccountId")
            ConvertToComboColumn("CommissionAccountId")
            ConvertToComboColumn("SecondAccountId")
            ConvertToComboColumn_Cr("Cr_ID")


            AddButtonColumn("MainAccountBtn")
            AddButtonColumn("CommissionAccountBtn")
            AddButtonColumn("SecondAccountBtn")

            MoveButtonNextTo("MainAccountId", "MainAccountBtn")
            MoveButtonNextTo("CommissionAccountId", "CommissionAccountBtn")
            MoveButtonNextTo("SecondAccountId", "SecondAccountBtn")


            For Each col As DataGridViewColumn In dgv.Columns
                If col.Name.Contains("Btn") Then
                    col.Width = 30
                End If
            Next


        End Using

    End Sub

    Private Sub AddButtonColumn(columnName As String)

        Dim btn As New DataGridViewButtonColumn
        btn.Name = columnName
        btn.HeaderText = ""
        btn.Text = "🔍"
        btn.UseColumnTextForButtonValue = True
        btn.Width = 35
        btn.FlatStyle = FlatStyle.Flat

        dgv.Columns.Add(btn)

    End Sub

    Private Sub MoveButtonNextTo(comboColumn As String, buttonColumn As String)

        dgv.Columns(buttonColumn).DisplayIndex =
        dgv.Columns(comboColumn).DisplayIndex + 1

    End Sub


    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick

        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = dgv.Columns(e.ColumnIndex).Name

        If colName = "MainAccountBtn" Then
            OpenAccountSelector(e.RowIndex, "MainAccountId")
        ElseIf colName = "CommissionAccountBtn" Then
            OpenAccountSelector(e.RowIndex, "CommissionAccountId")
        ElseIf colName = "SecondAccountBtn" Then
            OpenAccountSelector(e.RowIndex, "SecondAccountId")
        End If

    End Sub


    Private Sub OpenAccountSelector(rowIndex As Integer, targetColumn As String)

        Dim frm As New BALANCE_SEARCH
        frm.ShowDialog()
        If ACC_CODE_Search <> "" Then dgv.Rows(rowIndex).Cells(targetColumn).Value = ACC_CODE_Search


    End Sub



    '=========================================
    ' تحويل العمود إلى ComboBox
    '=========================================
    Private Sub ConvertToComboColumn(columnName As String)

        Dim combo As New DataGridViewComboBoxColumn

        combo.DataPropertyName = columnName
        combo.HeaderText = dgv.Columns(columnName).HeaderText
        combo.Name = columnName
        combo.DataSource = accountsTable
        combo.DisplayMember = "ACC_NAME"
        combo.ValueMember = "ACC_CODE"

        Dim index As Integer = dgv.Columns(columnName).Index

        dgv.Columns.Remove(columnName)
        dgv.Columns.Insert(index, combo)

    End Sub

    Private Sub ConvertToComboColumn_Cr(columnName As String)

        Dim combo As New DataGridViewComboBoxColumn

        combo.DataPropertyName = columnName
        combo.HeaderText = dgv.Columns(columnName).HeaderText
        combo.Name = columnName
        combo.DataSource = crTable
        combo.DisplayMember = "Cr_NAME"
        combo.ValueMember = "Cr_ID"

        Dim index As Integer = dgv.Columns(columnName).Index

        dgv.Columns.Remove(columnName)
        dgv.Columns.Insert(index, combo)

    End Sub


    '=========================================
    ' حفظ التعديلات
    '=========================================
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If MessageBox.Show("هل تريد حفظ التعديلات؟",
                           "تأكيد",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

         Using con As New SqlConnection(connectionString)

            con.Open()

            For Each row As DataGridViewRow In dgv.Rows

                If row.IsNewRow Then Continue For

                Dim opTypeObj = row.Cells("OperationType").Value
                If opTypeObj Is Nothing OrElse IsDBNull(opTypeObj) OrElse opTypeObj.ToString().Trim() = "" Then
                    ' تجاهل الصفوف غير المكتملة
                    Continue For
                End If

                ' لو لم يتم اختيار حساب، خزن NULL
                Dim IDObj = row.Cells("ExchangeTypeId").Value
                Dim mainObj = row.Cells("MainAccountId").Value
                Dim commObj = row.Cells("CommissionAccountId").Value
                Dim secObj = row.Cells("SecondAccountId").Value
                Dim CrObj = row.Cells("Cr_ID").Value

                ' Upsert: تحديث إذا موجود، وإدراج إذا غير موجود
                Dim existsCmd As New SqlCommand("SELECT COUNT(1) FROM ExchangeOperationAccounts WHERE OperationType = @Type AND ExchangeTypeId = @ExchangeTypeId", con)
                existsCmd.Parameters.AddWithValue("@Type", opTypeObj)
                existsCmd.Parameters.AddWithValue("@ExchangeTypeId", IDObj)

                Dim exists As Boolean = CInt(existsCmd.ExecuteScalar()) > 0

                Dim sql As String
                If exists Then
                    sql = "
                    UPDATE ExchangeOperationAccounts
                    SET MainAccountId = @Main,
                        CommissionAccountId = @Commission,
                        SecondAccountId = @Second,
                        Cr_ID = @Cr_ID
                    WHERE OperationType = @Type AND ExchangeTypeId = @ExchangeTypeId "
                Else
                    sql = "
                    INSERT INTO ExchangeOperationAccounts (OperationType, MainAccountId, CommissionAccountId, SecondAccountId,Cr_ID)
                    VALUES (@Type, @Main, @Commission, @Second,@Cr_ID)"
                End If

                Dim cmd As New SqlCommand(sql, con)

                cmd.Parameters.AddWithValue("@Type", opTypeObj)
                cmd.Parameters.AddWithValue("@Main", If(mainObj Is Nothing OrElse IsDBNull(mainObj) OrElse mainObj.ToString() = "", DBNull.Value, mainObj))
                cmd.Parameters.AddWithValue("@Commission", If(commObj Is Nothing OrElse IsDBNull(commObj) OrElse commObj.ToString() = "", DBNull.Value, commObj))
                cmd.Parameters.AddWithValue("@Second", If(secObj Is Nothing OrElse IsDBNull(secObj) OrElse secObj.ToString() = "", DBNull.Value, secObj))
                cmd.Parameters.AddWithValue("@ExchangeTypeId", If(IDObj Is Nothing OrElse IsDBNull(IDObj) OrElse IDObj.ToString() = "", DBNull.Value, IDObj))
                cmd.Parameters.AddWithValue("@Cr_ID", If(CrObj Is Nothing OrElse IsDBNull(CrObj) OrElse CrObj.ToString() = "", DBNull.Value, CrObj))
                cmd.ExecuteNonQuery()

            Next

        End Using

        MessageBox.Show("تم حفظ التعديلات بنجاح")

    End Sub



    '=========================================
    ' إضافة سطر جديد (ربط نوع عملية جديد)
    '=========================================
    Private Sub btnAddRow_Click(sender As Object, e As EventArgs) Handles btnAddRow.Click

        Dim dt As DataTable = TryCast(dgv.DataSource, DataTable)
        If dt Is Nothing Then
            MessageBox.Show("لا توجد بيانات في الجدول.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim r As DataRow = dt.NewRow()

        ' اجعل الحقول فارغة ليتم تحديدها من المستخدم
        r("OperationType") = DBNull.Value
        r("MainAccountId") = DBNull.Value
        r("CommissionAccountId") = DBNull.Value
        r("SecondAccountId") = DBNull.Value

        dt.Rows.Add(r)

        ' اجعل المستخدم يبدأ بتحديد نوع العملية مباشرة
        Dim newIndex As Integer = dgv.Rows.Count - 1
        If newIndex >= 0 Then
            dgv.ClearSelection()
            dgv.Rows(newIndex).Selected = True

            ' السماح بتعديل OperationType في الصف الجديد فقط
            dgv.Rows(newIndex).Cells("OperationType").ReadOnly = False

            dgv.CurrentCell = dgv.Rows(newIndex).Cells("OperationType")
            dgv.BeginEdit(True)
        End If

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadOperationAccounts()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


End Class




'Imports System.Data.SqlClient

'Public Class FrmExchangeOperationAccounts

'    Private connectionString As String = MY_Settings.SqlConStr
'    ' Private Tr_accountsTable As DataTable
'    Private accountsTable As DataTable
'    '=========================================
'    ' FORM LOAD
'    '=========================================
'    Private Sub FrmExchangeOperationAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

'        LoadAccounts()
'        LoadOperationAccounts()


'        dgv.EnableHeadersVisualStyles = False
'        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
'        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255)
'        dgv.DefaultCellStyle.SelectionForeColor = Color.Black


'    End Sub


'    Private Sub LoadAccounts()

'        Using con As New SqlConnection(connectionString)

'            accountsTable = New DataTable

'            Dim da As New SqlDataAdapter("
'                SELECT ACC_CODE, ACC_NAME
'                FROM dbo.ACCOUNTS_TREE
'                ORDER BY ACC_NAME", con)

'            da.Fill(accountsTable)

'        End Using

'    End Sub

'    '=========================================
'    ' تحميل بيانات الجدول
'    '=========================================
'    Private Sub LoadOperationAccounts()

'        Using con As New SqlConnection(connectionString)

'            Dim dt As New DataTable

'            Dim da As New SqlDataAdapter("
'                SELECT *
'                FROM ExchangeOperationAccounts
'                ORDER BY OperationType", con)

'            da.Fill(dt)

'            dgv.Columns.Clear()
'            dgv.DataSource = Nothing

'            dgv.DataSource = dt

'            ' اجعل OperationType غير قابل للتعديل
'            dgv.Columns("OperationType").ReadOnly = True

'            ' تحويل الأعمدة إلى ComboBox
'            ConvertToComboColumn("MainAccountId")
'            ConvertToComboColumn("CommissionAccountId")
'            ConvertToComboColumn("SecondAccountId")

'            AddButtonColumn("MainAccountBtn")
'            AddButtonColumn("CommissionAccountBtn")
'            AddButtonColumn("SecondAccountBtn")

'            MoveButtonNextTo("MainAccountId", "MainAccountBtn")
'            MoveButtonNextTo("CommissionAccountId", "CommissionAccountBtn")
'            MoveButtonNextTo("SecondAccountId", "SecondAccountBtn")



'            For Each col As DataGridViewColumn In dgv.Columns
'                If col.Name.Contains("Btn") Then
'                    col.Width = 30
'                End If
'            Next


'        End Using

'    End Sub

'    Private Sub AddButtonColumn(columnName As String)

'        Dim btn As New DataGridViewButtonColumn
'        btn.Name = columnName
'        btn.HeaderText = ""
'        btn.Text = "🔍"
'        btn.UseColumnTextForButtonValue = True
'        btn.Width = 35
'        btn.FlatStyle = FlatStyle.Flat

'        dgv.Columns.Add(btn)

'    End Sub

'    Private Sub MoveButtonNextTo(comboColumn As String, buttonColumn As String)

'        dgv.Columns(buttonColumn).DisplayIndex =
'        dgv.Columns(comboColumn).DisplayIndex + 1

'    End Sub


'    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick

'        If e.RowIndex < 0 Then Exit Sub

'        Dim colName As String = dgv.Columns(e.ColumnIndex).Name

'        If colName = "MainAccountBtn" Then
'            OpenAccountSelector(e.RowIndex, "MainAccountId")
'        ElseIf colName = "CommissionAccountBtn" Then
'            OpenAccountSelector(e.RowIndex, "CommissionAccountId")
'        ElseIf colName = "SecondAccountBtn" Then
'            OpenAccountSelector(e.RowIndex, "SecondAccountId")
'        End If

'    End Sub


'    Private Sub OpenAccountSelector(rowIndex As Integer, targetColumn As String)

'        Dim frm As New BALANCE_SEARCH
'        frm.ShowDialog()
'        If ACC_CODE_Search <> "" Then dgv.Rows(rowIndex).Cells(targetColumn).Value = ACC_CODE_Search

'    End Sub


'    '=========================================
'    ' تحويل العمود إلى ComboBox
'    '=========================================
'    Private Sub ConvertToComboColumn(columnName As String)

'        Dim combo As New DataGridViewComboBoxColumn

'        combo.DataPropertyName = columnName
'        combo.HeaderText = dgv.Columns(columnName).HeaderText
'        combo.Name = columnName
'        combo.DataSource = accountsTable
'        combo.DisplayMember = "ACC_NAME"
'        combo.ValueMember = "ACC_CODE"

'        Dim index As Integer = dgv.Columns(columnName).Index

'        dgv.Columns.Remove(columnName)
'        dgv.Columns.Insert(index, combo)

'    End Sub

'    '=========================================
'    ' حفظ التعديلات
'    '=========================================
'    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

'        If MessageBox.Show("هل تريد حفظ التعديلات؟",
'                           "تأكيد",
'                           MessageBoxButtons.YesNo,
'                           MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

'        Using con As New SqlConnection(connectionString)

'            con.Open()

'            For Each row As DataGridViewRow In dgv.Rows

'                If row.IsNewRow Then Continue For

'                Dim cmd As New SqlCommand("
'                    UPDATE ExchangeOperationAccounts
'                    SET MainAccountId = @Main,
'                        CommissionAccountId = @Commission,
'                        SecondAccountId = @Second
'                    WHERE OperationType = @Type", con)

'                cmd.Parameters.AddWithValue("@Main", row.Cells("MainAccountId").Value)
'                cmd.Parameters.AddWithValue("@Commission", row.Cells("CommissionAccountId").Value)
'                cmd.Parameters.AddWithValue("@Second", row.Cells("SecondAccountId").Value)
'                cmd.Parameters.AddWithValue("@Type", row.Cells("OperationType").Value)

'                cmd.ExecuteNonQuery()

'            Next

'        End Using

'        MessageBox.Show("تم حفظ التعديلات بنجاح")

'    End Sub

'    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
'        LoadOperationAccounts()
'    End Sub

'    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
'        Me.Close()
'    End Sub

'End Class
