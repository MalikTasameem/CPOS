Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Module Module1

    Public T_ID_Search As Integer = 0
    Public F_ACC_B As ACC_B
    Public F_ACC_MV As ACC_MV
    Public F_Daily_B_Form As Daily_B_Form
    Public F_Tree As Tree
    Public F_Receipt As Receipt
    Public F_Fixed_Assets As Fixed_Assets
    Public F_Current_Balances_By_Family As Current_Balances_By_Family
    Public F_MONTHS_CALENDR As MONTHS_CALENDR
    Public F_Cash_Flow_Report As Cash_Flow_Report
    Public F_Cheques_Form As Cheques_Form
    Public F_Accounting_settlement As Accounting_settlement
    Public T_DEBIT, T_CREDIT, T_BALANCE
    Public F_users As users


    Public Function query(ByVal query_Str As String)
        '  On Error Resume Next
        Dim C As New C
        C.Com = New SqlCommand(query_Str, C.Con)
        C.Con.Open()
        Try
            C.Com.ExecuteNonQuery()
            C.Con.Close()
            Return 1
        Catch ex As Exception
            MsgBox(ex.Message)
            C.Con.Close()
            Return 0
        End Try

        Return 0
    End Function

    Public Sub EnsureAccountSourceTypeColumn()
        query("IF COL_LENGTH('dbo.ACCOUNTS_TREE','AccountSourceType') IS NULL " &
              "ALTER TABLE dbo.ACCOUNTS_TREE ADD AccountSourceType TINYINT NOT NULL CONSTRAINT DF_ACCOUNTS_TREE_AccountSourceType DEFAULT(0) WITH VALUES;")
    End Sub

    Public Sub EnsureUserAccountAccessTables()
        query("
IF OBJECT_ID('dbo.User_Account_Access_Settings', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.User_Account_Access_Settings
    (
        UserId INT NOT NULL PRIMARY KEY,
        AllowAllAccounts BIT NOT NULL CONSTRAINT DF_UserAccountAccessSettings_AllowAllAccounts DEFAULT(1),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_UserAccountAccessSettings_UpdatedAt DEFAULT(GETDATE())
    )
END

IF OBJECT_ID('dbo.User_Account_Allowed', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.User_Account_Allowed
    (
        UserId INT NOT NULL,
        AccountCode NVARCHAR(40) NOT NULL,
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_UserAccountAllowed_CreatedAt DEFAULT(GETDATE()),
        CONSTRAINT PK_User_Account_Allowed PRIMARY KEY (UserId, AccountCode)
    )
END")
    End Sub

    Public Function GetSalesSystemAccountBlockMessage(accountCodeValue As Object, Optional accountRole As String = "الحساب") As String
        If accountCodeValue Is Nothing OrElse accountCodeValue Is DBNull.Value Then Return ""

        Dim accountCode As String = accountCodeValue.ToString().Trim()
        If String.IsNullOrWhiteSpace(accountCode) Then Return ""

        EnsureAccountSourceTypeColumn()

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
SELECT TOP 1
    ACC_CODE,
    ACC_NAME,
    ISNULL(AccountSourceType, 0) AS AccountSourceType
FROM dbo.ACCOUNTS_TREE
WHERE LTRIM(RTRIM(CONVERT(NVARCHAR(40), ACC_CODE))) = LTRIM(RTRIM(@ACC_CODE));", cn)

                cmd.Parameters.Add("@ACC_CODE", SqlDbType.NVarChar, 40).Value = accountCode
                cn.Open()

                Using rd As SqlDataReader = cmd.ExecuteReader()
                    If Not rd.Read() Then Return ""

                    If Convert.ToInt32(rd("AccountSourceType")) = 1 Then
                        Return "لا يمكن إدخال قيد يدوي على " & accountRole & " لأنه معرف من نظام المبيعات/العملاء." & vbCrLf &
                               "رقم الحساب: " & rd("ACC_CODE").ToString() & vbCrLf &
                               "اسم الحساب: " & rd("ACC_NAME").ToString()
                    End If
                End Using
            End Using
        End Using

        Return ""
    End Function

    Public Function ValidateManualJournalAccount(accountCodeValue As Object, Optional accountRole As String = "الحساب") As Boolean
        Dim msg As String = GetSalesSystemAccountBlockMessage(accountCodeValue, accountRole)
        If String.IsNullOrWhiteSpace(msg) Then Return True

        MsgBox(msg, MsgBoxStyle.Critical, "منع قيد يدوي")
        Return False
    End Function

    Public Function GetUserJournalAccountPermissionMessage(accountCodeValue As Object, Optional accountRole As String = "الحساب") As String
        If User_isAdmin Then Return ""
        If accountCodeValue Is Nothing OrElse accountCodeValue Is DBNull.Value Then Return ""

        Dim accountCode As String = accountCodeValue.ToString().Trim()
        If String.IsNullOrWhiteSpace(accountCode) Then Return ""

        EnsureUserAccountAccessTables()

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            cn.Open()

            Using settingsCmd As New SqlCommand("
SELECT TOP 1 AllowAllAccounts
FROM dbo.User_Account_Access_Settings
WHERE UserId = @UserId;", cn)

                settingsCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = USER_ID
                Dim settingValue As Object = settingsCmd.ExecuteScalar()

                If settingValue Is Nothing OrElse settingValue Is DBNull.Value Then Return ""
                If Convert.ToBoolean(settingValue) Then Return ""
            End Using

            Using allowedCmd As New SqlCommand("
SELECT TOP 1 1
FROM dbo.User_Account_Allowed
WHERE UserId = @UserId
  AND LTRIM(RTRIM(AccountCode)) = LTRIM(RTRIM(@AccountCode));", cn)

                allowedCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = USER_ID
                allowedCmd.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 40).Value = accountCode

                Dim allowedValue As Object = allowedCmd.ExecuteScalar()
                If allowedValue IsNot Nothing AndAlso allowedValue IsNot DBNull.Value Then Return ""
            End Using

            Using accountCmd As New SqlCommand("
SELECT TOP 1 ACC_CODE, ACC_NAME
FROM dbo.ACCOUNTS_TREE
WHERE LTRIM(RTRIM(CONVERT(NVARCHAR(40), ACC_CODE))) = LTRIM(RTRIM(@AccountCode));", cn)

                accountCmd.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 40).Value = accountCode

                Using rd As SqlDataReader = accountCmd.ExecuteReader()
                    If rd.Read() Then
                        Return "لا يملك المستخدم صلاحية التعامل مع " & accountRole & "." & vbCrLf &
                               "رقم الحساب: " & rd("ACC_CODE").ToString() & vbCrLf &
                               "اسم الحساب: " & rd("ACC_NAME").ToString()
                    End If
                End Using
            End Using
        End Using

        Return "لا يملك المستخدم صلاحية التعامل مع " & accountRole & "." & vbCrLf &
               "رقم الحساب: " & accountCode
    End Function

    Public Function ValidateUserJournalAccountPermission(accountCodeValue As Object, Optional accountRole As String = "الحساب") As Boolean
        Dim msg As String = GetUserJournalAccountPermissionMessage(accountCodeValue, accountRole)
        If String.IsNullOrWhiteSpace(msg) Then Return True

        MsgBox(msg, MsgBoxStyle.Critical, "صلاحية حسابات المستخدم")
        Return False
    End Function


    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As Int32
    End Function

    Public Function IM_Serach(IM_SH As String, _SearchColumn As String) As String
        Dim words As String() = IM_SH.Split(New Char() {" "c})
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and " & _SearchColumn & " Like "

        If words.Length() = 1 Then
            Str = _SearchColumn & "  = '" & IM_SH & "'  or " & _SearchColumn & "  like '%" & words(0) & "%' or " & _SearchColumn & "  like '%" & IM_SH & "' or " & _SearchColumn & "  like '" & IM_SH & "%'"
        Else
            IM_Str = "'%" & words(0) & "%'" & S_and

            For i = 1 To words.Length - 1

                If i = words.Length - 1 Then
                    IM_Str += "'%" & words(i) & "%'"
                Else
                    IM_Str += "'%" & words(i) & "%'" & S_and
                End If
            Next


            Str = _SearchColumn & "  like " & IM_Str

        End If
        Return Str
    End Function

    Public TOTAL_C_N, TOTAL_D_N
    Public Sub Compute_Balance(DT As DataTable)
        Dim rows As Integer = 0
        T_DEBIT = 0
        T_CREDIT = 0
        T_BALANCE = 0
        TOTAL_C_N = 0
        TOTAL_D_N = 0
        'Dim total_tax_withheld_map As Double

        Try
            Do Until rows = DT.Rows.Count

                '(Not IsNothing(DT(rows)("Credit"))) Or
                If (Not IsDBNull(DT(rows)("Credit"))) Then
                    Dim Tax_Withheld As Double = DT(rows)("Credit")
                    '  total_tax_withheld_map = total_tax_withheld_map + Tax_Withheld
                    T_CREDIT += Tax_Withheld
                    TOTAL_C_N += 1
                End If

                '(Not IsNothing(DT(rows)("Debit"))) Or
                If (Not IsDBNull(DT(rows)("Debit"))) Then
                    Dim Tax_Withheld As Double = DT(rows)("Debit")
                    '  total_tax_withheld_map = total_tax_withheld_map + Tax_Withheld
                    T_DEBIT += Tax_Withheld
                    TOTAL_D_N += 1
                End If


                rows = rows + 1
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'lblamountmap.Text = total_tax_withheld_map
        'Dim dblValuemap As Double = total_tax_withheld_map
        'lblamountmap.Text = (dblValuemap.ToString("N", CultureInfo.InvariantCulture))

    End Sub

    Public Sub CB_CHecked(sender As Object)
        If sender.Checked = True Then
            sender.ForeColor = Color.DarkGreen
        Else
            sender.ForeColor = Color.Black
        End If
    End Sub

    Public Sub Build_Connection()
        If MY_Settings.IsAttachDB = False Then
            If MY_Settings.DB_Authentication = 0 Then
                MY_Settings.SqlConStr = "Data Source= " & MY_Settings.S_SERVER & " ;initial catalog=" & MY_Settings.DataBase & ";Integrated Security=True;"
            Else
                MY_Settings.SqlConStr = "Data Source= " & MY_Settings.S_SERVER & " ;initial catalog=" & MY_Settings.DataBase & ";User Id=" & MY_Settings.DB_UName & ";Password=" & MY_Settings.DB_Pass & ""
            End If
        Else
            MY_Settings.AttachDbFilename = Application.StartupPath & "\DB\" & MY_Settings.DataBase & ".mdf"
            MY_Settings.SqlConStr = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & MY_Settings.AttachDbFilename & ";Integrated Security=True;"
        End If

        'Save_AppSetting()

    End Sub


    Public Sub GET_TREE_BALANCE(ACC_CODE As Integer, ByRef D_F As Date, D_T As Date, is_Balance_View As Integer)
        'query("EXEC [dbo].[PREPARE_ACC_BALANCE] 0 ")

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[PREPARE_ACC_BALANCE]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@ACC_CODE", ACC_CODE)
            .Parameters.AddWithValue("@DATE_F", D_F)
            .Parameters.AddWithValue("@DATE_T", D_T)
            .Parameters.AddWithValue("@is_Balance_View", is_Balance_View)

        End With
        SQL_SP_EXEC(C.Com)

    End Sub


    Public Function SQL_SP_EXEC(sqlComm As SqlClient.SqlCommand)
        Dim isDone = True
        Dim T
        Dim sqlCon = New SqlConnection(MY_Settings.SqlConStr)
        Using (sqlCon)
            sqlComm.Connection = sqlCon
            sqlCon.Open()
            T = sqlComm.ExecuteNonQuery()
            If T = 0 Then isDone = False
            sqlCon.Close()
        End Using
        Return isDone
    End Function

    Public Sub Kill_All_Processes()
        For Each prog As Process In Process.GetProcesses
            If prog.ProcessName = "CPOS_Balances" Then
                prog.Kill()
            End If
        Next
    End Sub

    Public Function GET_FAINANCIAL_YEAR()
        Dim C As New C

        Dim S As String = " SELECT ISNULL(YEAR_ID,0) AS YEAR_ID from YEARS WHERE is_Close = 0 "
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                Identifiers.F_YEAR = C.Dr("YEAR_ID")
            Else
                Identifiers.F_YEAR = 0
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return 0
    End Function

    Public Sub LOAD_ALL_TABLES()
        LOAD_Currencies_Datatable()
        LOAD_CostCenter_Datatable()
        LOAD_Accounts_Datatable()
        LOAD_Accounts_Agents(1)
        LOAD_Accounts_Agents(2)
        GET_SYS_Features()
    End Sub

    Private Sub GET_SYS_Features()
        Dim c As New C
        Dim N As Double = 0
        Try
            Dim s As String
            EnsureAccountSourceTypeColumn()
            query("IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Use_State_Budget') IS NULL " &
                  "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Use_State_Budget BIT NOT NULL CONSTRAINT DF_SYS_Features_ACOUNTING_Use_State_Budget DEFAULT(0); " &
                  "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Allow_Budget_OverSpend') IS NULL " &
                  "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Allow_Budget_OverSpend BIT NOT NULL CONSTRAINT DF_SYS_Features_ACOUNTING_Allow_Budget_OverSpend DEFAULT(0); " &
                  "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Default_Stamp_Percent') IS NULL " &
                  "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Default_Stamp_Percent DECIMAL(18,3) NULL; " &
                  "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Default_Stamp_Account_Code') IS NULL " &
                  "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Default_Stamp_Account_Code NVARCHAR(40) NULL;")
            s = "SELECT TOP 1 ISNULL(Pure_Income_ACC_CODE,551) AS Pure_Income_ACC_CODE, ISNULL(Use_State_Budget, 0) AS Use_State_Budget, ISNULL(Allow_Budget_OverSpend, 0) AS Allow_Budget_OverSpend, ISNULL(Default_Stamp_Percent, 0) AS Default_Stamp_Percent, ISNULL(Default_Stamp_Account_Code, N'') AS Default_Stamp_Account_Code FROM [SYS_Features_ACOUNTING] "
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Identifiers.Pure_Income_ACC_CODE = c.Dr("Pure_Income_ACC_CODE")
                MY_Settings.Use_State_Budget = Convert.ToBoolean(c.Dr("Use_State_Budget"))
                MY_Settings.Allow_Budget_OverSpend = Convert.ToBoolean(c.Dr("Allow_Budget_OverSpend"))
                MY_Settings.Default_Stamp_Percent = Convert.ToDecimal(c.Dr("Default_Stamp_Percent"))
                MY_Settings.Default_Stamp_Account_Code = c.Dr("Default_Stamp_Account_Code").ToString()
            Else
                Identifiers.Pure_Income_ACC_CODE = 551
                MY_Settings.Use_State_Budget = False
                MY_Settings.Allow_Budget_OverSpend = False
                MY_Settings.Default_Stamp_Percent = 0D
                MY_Settings.Default_Stamp_Account_Code = ""
                SYS_Features_INSERT()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub SYS_Features_INSERT()
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[SYS_Features_INSERT]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Pure_Income_ACC_CODE", Identifiers.Pure_Income_ACC_CODE)
            SQL_SP_EXEC(C.Com)
        End With

    End Sub


    Public Sub LOAD_Currencies_Datatable()
        Currencies_Datatable = New DataTable
        Dim C As New C
        C.Da = New SqlClient.SqlDataAdapter(" select Cr_ID , Cr_Name  from Currency  ", C.Con)
        C.Da.Fill(Currencies_Datatable)
    End Sub

    Public Sub LOAD_CostCenter_Datatable()
        CostCenter_Datatable = New DataTable
        Dim C As New C
        C.Da = New SqlClient.SqlDataAdapter(" select COST_ID , COST_NAME  from COST_CENTER  ", C.Con)
        C.Da.Fill(CostCenter_Datatable)
    End Sub

    Public Sub LOAD_Accounts_Datatable()
        Accounts_Datatable = New DataTable
        Dim C As New C
        EnsureAccountSourceTypeColumn()
        C.Da = New SqlClient.SqlDataAdapter("SELECT T_ID, ACC_CODE, ACC_NAME, ACC_PARENT, ACC_LEVEL, ISNULL(AccountSourceType, 0) AS AccountSourceType, ISNULL(is_Lock_Trans, 0) AS is_Lock_Trans FROM ACCOUNTS_TREE", C.Con)
        'C.Da = New SqlClient.SqlDataAdapter("SELECT T_ID, ACC_CODE, CONCAT(ACC_NAME,' (',ACC_CODE,')') AS ACC_NAME , ACC_PARENT, ACC_LEVEL FROM ACCOUNTS_TREE", C.Con)
        C.Da.Fill(Accounts_Datatable)
    End Sub



    Public Sub LOAD_Accounts_Agents(Type_ID As Integer)

        Try
            If Type_ID = 1 Then
                ' ================== Agents ==================
                Agents_Datatable = New DataTable
                agentRoots = LoadRootAccounts(Type_ID)
                Agents_Datatable =
                GetAccountsTreeDataTable_IN(Accounts_Datatable, agentRoots)



            ElseIf Type_ID = 2 Then
                ' ================== Treasury ==================
                Treasury_Datatable = New DataTable
                treasuryRoots = LoadRootAccounts(Type_ID)
                Treasury_Datatable =
                GetAccountsTreeDataTable_IN(Accounts_Datatable, treasuryRoots)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Function LoadRootAccounts(accType As Integer) As List(Of String)

        Dim list As New List(Of String)
        Dim dt As New DataTable
        Dim C As New C

        Dim sql As String = "
        SELECT ACC_CODE
        FROM dbo.Rct_Mang_V
        WHERE ACC_Type = @Type
    "

        C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
        C.Da.SelectCommand.Parameters.AddWithValue("@Type", accType)
        C.Da.Fill(dt)

        For Each r As DataRow In dt.Rows
            list.Add(r("ACC_CODE").ToString())
        Next

        Return list
    End Function





    Public Sub Check_Only_Int(Sender As Object, e As KeyPressEventArgs)
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub


    '------------------------------------------------------------------------------------------------------------------- <for all balances>
    Function GetAccountTreeDataTable(dtAccounts As DataTable, accountCode As String) As DataTable
        ' إنشاء DataTable جديد للاحتفاظ بالنتائج
        Dim dtResult As DataTable = dtAccounts.Clone() ' نسخ نفس بنية الجدول الأصلي

        ' البحث عن الحساب الأساسي
        Dim rows As DataRow() = dtAccounts.Select($"ACC_CODE = '{accountCode}'")
        If rows.Length > 0 Then
            dtResult.ImportRow(rows(0)) ' إضافة الحساب الرئيسي
            GetChildAccountsDataTable(dtAccounts, rows(0)("ACC_CODE").ToString(), dtResult)
        End If

        ' ترتيب النتيجة حسب المستوى والكود
        dtResult.DefaultView.Sort = "ACC_LEVEL ASC, ACC_CODE ASC"
        Return dtResult.DefaultView.ToTable()
    End Function

    ' دالة البحث التكرارية لجلب الأبناء وإضافتهم إلى DataTable
    Sub GetChildAccountsDataTable(dtAccounts As DataTable, parentCode As String, ByRef dtResult As DataTable)
        Dim childRows As DataRow() = dtAccounts.Select($"ACC_PARENT = '{parentCode}'")

        For Each row In childRows
            dtResult.ImportRow(row) ' إضافة الصف للنتائج
            GetChildAccountsDataTable(dtAccounts, row("ACC_CODE").ToString(), dtResult) ' استدعاء تكراري
        Next
    End Sub
    '-------------------------------------------------------------------------------------------------------------------</for all balances>



    '------------------------------------------------------------------------------------------------------------------- <for agentsa and treasury balances>
    Function GetAccountsTreeDataTable_IN(
    dtAccounts As DataTable,
    rootAccounts As List(Of String)
) As DataTable

        ' جدول النتيجة (نفس بنية جدول الحسابات)
        Dim dtResult As DataTable = dtAccounts.Clone()

        ' لمنع التكرار
        Dim addedAccounts As New HashSet(Of String)()

        For Each accCode In rootAccounts
            If Not String.IsNullOrWhiteSpace(accCode) Then
                AddAccountWithChildren(dtAccounts, accCode, dtResult, addedAccounts)
            End If
        Next

        ' ترتيب النتيجة (اختياري)
        dtResult.DefaultView.Sort = "ACC_LEVEL ASC, ACC_CODE ASC"
        Return dtResult.DefaultView.ToTable()

    End Function


    Sub AddAccountWithChildren(
    dtAccounts As DataTable,
    parentCode As String,
    ByRef dtResult As DataTable,
    ByRef addedAccounts As HashSet(Of String)
)

        ' جلب الحساب الحالي
        Dim rows As DataRow() =
        dtAccounts.Select($"ACC_CODE = '{parentCode}'")

        If rows.Length > 0 Then
            Dim accCode As String = rows(0)("ACC_CODE").ToString()

            ' منع التكرار
            If Not addedAccounts.Contains(accCode) Then
                dtResult.ImportRow(rows(0))
                addedAccounts.Add(accCode)
            End If
        End If

        ' جلب الأبناء
        Dim childRows As DataRow() =
        dtAccounts.Select($"ACC_PARENT = '{parentCode}'")

        For Each row In childRows
            Dim childCode As String = row("ACC_CODE").ToString()
            AddAccountWithChildren(dtAccounts, childCode, dtResult, addedAccounts)
        Next

    End Sub


    '-------------------------------------------------------------------------------------------------------------------</for agentsa and treasury balances>




    Public Sub ExportReportToExcel(ByVal report As ReportDocument, ByVal exportPath As String)
        Try
            Dim exportOptions As ExportOptions
            Dim excelFormatOptions As New ExcelFormatOptions()
            Dim diskFileDestinationOptions As New DiskFileDestinationOptions()

            ' تحديد مسار الحفظ
            diskFileDestinationOptions.DiskFileName = exportPath

            ' ضبط إعدادات التصدير
            excelFormatOptions.ExcelUseConstantColumnWidth = False ' يحافظ على التنسيق الأصلي

            exportOptions = report.ExportOptions
            exportOptions.ExportFormatType = ExportFormatType.Excel
            exportOptions.FormatOptions = excelFormatOptions
            exportOptions.ExportDestinationType = ExportDestinationType.DiskFile
            exportOptions.DestinationOptions = diskFileDestinationOptions

            ' تنفيذ التصدير
            report.Export()
            MessageBox.Show("تم تصدير التقرير بنجاح إلى: " & exportPath, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء التصدير: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Check_Only_Float(Sender As Object, e As KeyPressEventArgs)
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", vbBack
                If (e.KeyChar.ToString = ".") And (Sender.Text.Contains(e.KeyChar.ToString)) Then
                    e.Handled = True
                    Exit Sub
                End If
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Public Function Show_Balance(ACC_CODE As String)
        Dim c As New C
        Dim N As Double = 0
        Try
            Dim s As String
            s = "SELECT isnull(BALANCE,0) as T_Balance FROM [ACCOUNTS_TREE_V] WHERE ACC_CODE = '" & ACC_CODE & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                N = c.Dr("T_Balance")
                Return N.ToString("N")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return 0
    End Function

    Public Sub Check_Point_in_FloatNum(Sender As Object, e As EventArgs)
        If Sender.Text = "." Then Sender.Text = "0."
    End Sub

    Dim isDarkMode As Boolean '= MY_Settings.is_Dark_mode

    Public Sub ToggleTheme(ByRef F As Form)

        isDarkMode = MY_Settings.is_Dark_mode

        Dim bgForm = If(isDarkMode, Color.FromArgb(30, 30, 30), Color.WhiteSmoke)
        Dim bgPanel = If(isDarkMode, Color.FromArgb(37, 37, 38), SystemColors.Control)
        Dim fgColor = If(isDarkMode, Color.FromArgb(212, 212, 212), Color.Black)
        Dim btnBackColor = If(isDarkMode, Color.FromArgb(60, 60, 60), Color.LightGray)
        Dim dgvHeader = If(isDarkMode, Color.FromArgb(63, 63, 70), SystemColors.Control)

        F.BackColor = bgForm
        If isDarkMode Then ApplyThemeToControls(F.Controls, bgPanel, fgColor, btnBackColor, dgvHeader, isDarkMode)
        'ApplyThemeToControls(F.Controls, bgPanel, fgColor, btnBackColor, dgvHeader, isDarkMode)
    End Sub

    Private Sub ApplyThemeToControls(
    ctrls As Control.ControlCollection,
    bgPanel As Color,
    fgColor As Color,
    btnColor As Color,
    dgvHeader As Color,
    isDark As Boolean
)
        For Each ctrl As Control In ctrls

            ' حاويات: Panel, GroupBox, TabPage
            If TypeOf ctrl Is Panel OrElse TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is TabPage Then
                ctrl.BackColor = bgPanel
                ApplyThemeToControls(ctrl.Controls, bgPanel, fgColor, btnColor, dgvHeader, isDark)

            ElseIf TypeOf ctrl Is Label Then
                ctrl.ForeColor = fgColor

            ElseIf TypeOf ctrl Is Button Then
                ctrl.BackColor = btnColor
                ctrl.ForeColor = fgColor

            ElseIf TypeOf ctrl Is CheckBox OrElse TypeOf ctrl Is RadioButton Then
                ctrl.BackColor = bgPanel
                ctrl.ForeColor = fgColor

            ElseIf TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox OrElse TypeOf ctrl Is F2FloatField Then
                ctrl.BackColor = If(isDark, Color.FromArgb(50, 50, 50), SystemColors.Window)
                ctrl.ForeColor = fgColor

            ElseIf TypeOf ctrl Is ListBox OrElse TypeOf ctrl Is TreeView Then
                ctrl.BackColor = If(isDark, Color.FromArgb(50, 50, 50), SystemColors.Window)
                ctrl.ForeColor = fgColor

            ElseIf TypeOf ctrl Is DataGridView Then
                Dim dgv = CType(ctrl, DataGridView)
                dgv.BackgroundColor = bgPanel
                dgv.DefaultCellStyle.BackColor = bgPanel
                dgv.DefaultCellStyle.ForeColor = fgColor
                dgv.ColumnHeadersDefaultCellStyle.BackColor = dgvHeader
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = fgColor
                dgv.EnableHeadersVisualStyles = False

            ElseIf TypeOf ctrl Is ToolStrip Then
                Dim ts = CType(ctrl, ToolStrip)
                ts.BackColor = bgPanel


                ts.ForeColor = fgColor
                ' تغيير الـ Renderer حسب الوضع
                If isDark Then
                    ts.Renderer = New DarkRenderer() ' تحتاج تعريف هذا الـ Renderer
                Else
                    ts.Renderer = New ToolStripProfessionalRenderer() ' الوضع العادي
                End If

                '  ts.Renderer = New DarkRenderer() ' إن كنت تستخدم Renderer مخصص


                For Each item As ToolStripItem In ts.Items
                    item.BackColor = bgPanel
                    item.ForeColor = fgColor
                    item.Font = ts.Font
                Next
            End If

            ' أي عنصر يحتوي عناصر داخلية
            If ctrl.HasChildren Then
                ApplyThemeToControls(ctrl.Controls, bgPanel, fgColor, btnColor, dgvHeader, isDark)
            End If
        Next
    End Sub


    Public Sub ApplyFontToControls(ctrls As Control.ControlCollection, font As Font)
        For Each ctrl As Control In ctrls
            ' إنشاء خط جديد بنفس الحجم والستايل الحاليين لكن بنوع الخط الجديد فقط
            ctrl.Font = New Font(font.FontFamily, ctrl.Font.Size, ctrl.Font.Style)

            ' دعم DataGridView
            If TypeOf ctrl Is DataGridView Then
                Dim dgv = CType(ctrl, DataGridView)
                dgv.ColumnHeadersDefaultCellStyle.Font = New Font(font.FontFamily, dgv.ColumnHeadersDefaultCellStyle.Font.Size, dgv.ColumnHeadersDefaultCellStyle.Font.Style)
                dgv.DefaultCellStyle.Font = New Font(font.FontFamily, dgv.DefaultCellStyle.Font.Size, dgv.DefaultCellStyle.Font.Style)
            End If

            ' دعم ToolStrip
            If TypeOf ctrl Is ToolStrip Then
                Dim ts = CType(ctrl, ToolStrip)
                ts.Font = New Font(font.FontFamily, ts.Font.Size, ts.Font.Style)
                For Each item As ToolStripItem In ts.Items
                    item.Font = New Font(font.FontFamily, item.Font.Size, item.Font.Style)
                Next
            End If

            ' تطبيق على العناصر المتداخلة
            If ctrl.HasChildren Then
                ApplyFontToControls(ctrl.Controls, font)
            End If
        Next
    End Sub


    'Public Sub ApplyFontToControls(ctrls As Control.ControlCollection, font As Font)
    '    For Each ctrl As Control In ctrls
    '        ctrl.Font = font

    '        ' دعم DataGridView
    '        If TypeOf ctrl Is DataGridView Then
    '            Dim dgv = CType(ctrl, DataGridView)
    '            dgv.ColumnHeadersDefaultCellStyle.Font = font
    '            dgv.DefaultCellStyle.Font = font
    '        End If

    '        ' دعم ToolStrip
    '        If TypeOf ctrl Is ToolStrip Then
    '            Dim ts = CType(ctrl, ToolStrip)
    '            ts.Font = font
    '            For Each item As ToolStripItem In ts.Items
    '                item.Font = font
    '            Next
    '        End If

    '        ' تطبيق على العناصر المتداخلة
    '        If ctrl.HasChildren Then
    '            ApplyFontToControls(ctrl.Controls, font)
    '        End If
    '    Next
    'End Sub



    Public Sub Filter_B(ByRef B_Name_Cm As ComboBox, ByRef B_Num_TXT As TextBox, ByRef ACC_CODE_DT As DataTable)

        ACC_CODE_DT = GetAccountTreeDataTable(Accounts_Datatable, B_Num_TXT.Text)

        B_Name_Cm.DataSource = ACC_CODE_DT
        B_Name_Cm.DisplayMember = "ACC_NAME"
        B_Name_Cm.ValueMember = "ACC_CODE"
        B_Name_Cm.DroppedDown = True
        If ACC_CODE_DT.Rows.Count = 0 Then B_Name_Cm.Text = ""

    End Sub

    Public Function ConvertImage(ByVal myImage As Image) As Byte()
        Try
            Dim mstream As New MemoryStream
            myImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Png)
            Dim myBytes(mstream.Length - 1) As Byte
            mstream.Position = 0
            mstream.Read(myBytes, 0, mstream.Length)
            Return myBytes
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function Get_Equal(Currency_ID As Integer, Date_ As DateTimePicker, Optional Type_id As Integer = 0)

        Dim C As New C
        Dim EQUAL As Double = 1
        With C.Com
            .Connection = C.Con
            .CommandText = "[GET_CR_EQUAL]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@DATE", Date_.Value)
            .Parameters.AddWithValue("@Cr_ID", Currency_ID)
            .Parameters.Add("@Currency_Equal", SqlDbType.Float)
            .Parameters.AddWithValue("@Type_id", Type_id)

            .Parameters("@Currency_Equal").Direction = ParameterDirection.Output


            If SQL_SP_EXEC(C.Com) = True Then
                EQUAL = C.Com.Parameters("@Currency_Equal").Value.ToString()
            End If

        End With

        Return EQUAL
    End Function


    Public Function Get_Currency_Tag(Cr_ID As Integer)
        Dim Cr_Tage As String = "LYD"
        Dim C As New C
        Dim S As String = "Select Cr_Tage  From Currency WHERE Cr_ID = " & Cr_ID
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Cr_Tage = C.Dr("Cr_Tage")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return Cr_Tage
    End Function



    Public Sub System_Startup()

        Recover_File_Setting()

        MY_Settings.Cpu_ID = CpuId()
        Save_AppSetting()

        Build_Connection()

        If CheckConnection(MY_Settings.SqlConStr) Then
            'Test_Computer_Setting()
            Test_Product_Code()
        Else
            MsgBox("تحقق من إعدادات الإتصال", MsgBoxStyle.Exclamation)
        End If

        GET_FAINANCIAL_YEAR()
        LOAD_ALL_TABLES()
    End Sub


    Public Function CpuId() As String
        Dim computer As String = "."
        Dim wmi As Object = GetObject("winmgmts:" &
            "{impersonationLevel=impersonate}!\\" &
            computer & "\root\cimv2")
        Dim processors As Object = wmi.ExecQuery("Select * from " &
            "Win32_Processor")

        Dim cpu_ids As String = ""
        For Each cpu As Object In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu
        If cpu_ids.Length > 0 Then cpu_ids =
            cpu_ids.Substring(2)
        Return (cpu_ids)
    End Function


    'Public Sub Test_Computer_Setting()
    '    Try
    '        Dim C As New C
    '        C.Str = "Select CP_NAME FROM SysSetting WHERE CP_NAME ='" & My.Computer.Name & "'"
    '        'C.Str = "Select CP_NAME FROM SysSetting WHERE Cpu_ID ='" & MY_Settings.Hard_Serial_NUM & "'"
    '        C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
    '        C.Con.Open()

    '        C.Dr = C.Com.ExecuteReader
    '        If C.Dr.HasRows = True Then
    '            Get_Computer_Setting()
    '        Else
    '            Computer_Setting_InsertDefult()
    '        End If
    '        C.Con.Close()
    '    Catch ex As Exception
    '        MsgBox(ex.Message + " (Test_Computer_Setting)")
    '    End Try

    'End Sub


    Public Sub Test_Product_Code()
        Try
            Dim C As New C
            C.Str = "Select ISNULL(ProductCode,'') AS ProductCode From Activation_Details WHERE CP_NAME = '" & My.Computer.Name & "'"
            'C.Str = "Select ISNULL(ProductCode,'') AS ProductCode From Activation_Details WHERE [Cpu_ID] = '" & MY_Settings.Hard_Serial_NUM & "'"
            C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
            C.Con.Open()

            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                If C.Dr("ProductCode") <> "" Then
                    S_ProductCode = C.Dr("ProductCode")
                Else
                    Build_Product_Code()
                End If

            Else
                Build_Product_Code()
            End If
            C.Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message + "  (Test_Product_Code)")
        End Try

    End Sub

    Private Sub Build_Product_Code()
        Dim Day As String = Date.Now.Day.ToString
        If Day.Count = 1 Then Day = "0" + Day
        Dim Month As String = Date.Now.Month.ToString
        If Month.Count = 1 Then Month = "0" + Month
        Date_Serial = Day + Month + Date.Now.Year.ToString
        Save_AppSetting()
        ''-------------------------------------------------------------------------
        Dim Tmp As String = ""
        Dim TmpSerialArray(8) As Char
        For i = 0 To 7
            Randomize()
            TmpSerialArray(i) = (CInt(Math.Ceiling(Rnd() * 9)) + 1).ToString
            Tmp = Tmp + TmpSerialArray(i)
        Next
        Random_Serial = Tmp

        Dim Serial As String = ""
        Serial = Serial + Date_Serial + MY_Settings.Cpu_ID + Random_Serial 'Get_first8Digits(MY_Settings.Hard_Serial_NUM)

        S_ProductCode = Serial
        Save_AppSetting()

        Parsing_Code(Serial)

        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(MY_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "Activation_Details_insert"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@CP_NAME", My.Computer.Name)
            sqlComm.Parameters.AddWithValue("@Cpu_ID", MY_Settings.Cpu_ID) 'MY_Settings.Hard_Serial_NUM 
            sqlComm.Parameters.AddWithValue("@ProductCode", Serial)
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Public Sub Parsing_Code(ByRef Product_Code As String)
        Dim i As Integer
        Dim length As Integer
        Dim value As String

        length = Product_Code.Length
        value = Product_Code

        For i = 0 To Product_Code.Length Step 5
            If i = 0 Then
                value = value.Insert(i + 4, "-")
            Else
                value = value.Insert(i + 4, "-")
            End If
        Next
        'length - 4
        Product_Code = value
    End Sub


    Public Function CheckConnection(Cs As String)
        Dim bb As New SqlConnection
        Try

            bb.ConnectionString = Cs
            'MsgBox(bb.ConnectionTimeout)
            bb.Open()
            bb.Close()

            Return True
        Catch ex As Exception
            bb.Close()
            Return False
        End Try
    End Function



    Public Function check_FOUND_YEAR(YEAR_ID)
        Dim C As New C

        Dim S As String = "select YEAR_ID AS S  from YEARS WHERE YEAR_ID = " & YEAR_ID
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                Return True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return False
    End Function


    Public Function GET_FIRST_DAY_OF_YEAR(YEAR_ID)
        Dim C As New C
        'Dim D As New Date

        Dim S As String = "SELECT TOP 1 M_FROM FROM MONTHS_CALENDR WHERE YEAR = " & YEAR_ID
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                Return C.Dr("M_FROM")
            Else
                Return 0
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return 0
    End Function


    Public Function SELECT_ARCHIVE_COUNTER(YEAR_ID As Integer)
        Dim C As New C

        Dim S As String = "select COUNT(T_ID) AS S  from  ACC_BALANCE_MASTER_ARCHIVE WHERE YEAR = " & YEAR_ID
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                Return C.Dr("S")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return 0
    End Function


    Public JournalCount As Integer
    Public JournalCount_DAY As Integer
    Public AccountsCount As Integer
    Public BalanceTotal_C As Decimal
    Public BalanceTotal_D As Decimal
    Public Sub GET_summary()

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("dbo.Get_FinancialYear_Summary", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@FinancialYear", F_YEAR)

                con.Open()
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        JournalCount = Convert.ToInt32(dr("JournalCount"))
                        JournalCount_DAY = Convert.ToInt32(dr("JournalCount_DAY"))
                        AccountsCount = Convert.ToInt32(dr("AccountsCount"))
                        BalanceTotal_C = Convert.ToDecimal(dr("BalanceTotal_C"))
                        BalanceTotal_D = Convert.ToDecimal(dr("BalanceTotal_D"))
                    End If
                End Using
            End Using
        End Using

        Tree_MainForm.UpdateStatistics(AccountsCount, ("إجمالي القيود: " & JournalCount & vbNewLine & "قيود اليوم: " & JournalCount_DAY), ("الرصيد المدين: " & BalanceTotal_D.ToString("N0") & vbNewLine & "الرصيد الدائن: " & BalanceTotal_C.ToString("N0")), F_YEAR)
    End Sub

End Module
