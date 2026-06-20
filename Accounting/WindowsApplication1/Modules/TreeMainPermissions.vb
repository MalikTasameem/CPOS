Imports System.Data
Imports System.Data.SqlClient

Public Module TreeMainPermissions

    Public Structure PermissionDef
        Public Key As String
        Public Title As String
        Public GroupName As String

        Public Sub New(permissionKey As String, permissionTitle As String, permissionGroup As String)
            Key = permissionKey
            Title = permissionTitle
            GroupName = permissionGroup
        End Sub
    End Structure

    Public ReadOnly Property AllPermissions As List(Of PermissionDef)
        Get
            Return New List(Of PermissionDef) From {
                New PermissionDef("TREE.ACCOUNTS", "الدليل الحسابي", "البيانات الأساسية"),
                New PermissionDef("TREE.FIXED_ASSETS", "الأصول الثابتة", "البيانات الأساسية"),
                New PermissionDef("TREE.COST_CENTERS", "مراكز التكلفة", "البيانات الأساسية"),
                New PermissionDef("TREE.CURRENCIES", "العملات", "البيانات الأساسية"),
                New PermissionDef("TREE.FISCAL_YEAR", "السنة المالية", "البيانات الأساسية"),
                New PermissionDef("TREE.JOURNAL", "القيود اليومية", "الإدخال اليومي"),
                New PermissionDef("TREE.RECEIPT_IN", "سند قبض", "الإدخال اليومي"),
                New PermissionDef("TREE.RECEIPT_OUT", "سند صرف", "الإدخال اليومي"),
                New PermissionDef("TREE.CHEQUES", "الشيكات", "الإدخال اليومي"),
                New PermissionDef("TREE.SETTLEMENT", "التسويات", "الإدخال اليومي"),
                New PermissionDef("TREE.JOURNAL_LIST", "قائمة القيود اليومية", "الإدخال اليومي"),
                New PermissionDef("TREE.BUDGET_DOORS", "إدارة أبواب الموازنة", "بيانات الموازنة الأساسية"),
                New PermissionDef("TREE.BUDGET_CHAPTERS", "إدارة فصول الموازنة", "بيانات الموازنة الأساسية"),
                New PermissionDef("TREE.BUDGET_ITEMS", "إدارة بنود الموازنة", "بيانات الموازنة الأساسية"),
                New PermissionDef("TREE.BUDGET_ACCOUNT_MAPPING", "ربط الحسابات ببنود الموازنة", "بيانات الموازنة الأساسية"),
                New PermissionDef("TREE.BUDGET_ALLOCATIONS", "اعتمادات الموازنة", "بيانات الموازنة الأساسية"),
                New PermissionDef("TREE.BUDGET_RESERVE", "حجز موازنة", "الإدخال اليومي للموازنة"),
                New PermissionDef("TREE.BUDGET_SPEND", "صرف موازنة", "الإدخال اليومي للموازنة"),
                New PermissionDef("TREE.BUDGET_RELEASE_TO_SPEND", "تحويل الحجز إلى صرف", "الإدخال اليومي للموازنة"),
                New PermissionDef("TREE.BUDGET_TRANSFER", "تحويل بين بنود الموازنة", "الإدخال اليومي للموازنة"),
                New PermissionDef("TREE.BUDGET_DASHBOARD", "لوحة موقف الموازنة", "الإدخال اليومي للموازنة"),
                New PermissionDef("TREE.BUDGET_RESERVATIONS_REPORT", "تقرير الحجوزات", "الإدخال اليومي للموازنة"),
                New PermissionDef("ACC_B.APPROVE", "إمكانية اعتماد قيد", "صلاحيات شاشة القيود"),
                New PermissionDef("ACC_B.PRINT", "طباعة قيد", "صلاحيات شاشة القيود"),
                New PermissionDef("ACC_B.EDIT_APPROVAL", "تحرير قيد", "صلاحيات شاشة القيود"),
                New PermissionDef("ACC_B.REVERSE", "قيد عكسي", "صلاحيات شاشة القيود"),
                New PermissionDef("TREE.BALANCES_REVIEW", "مراجعة الأرصدة", "التقارير المالية"),
                New PermissionDef("TREE.ACC_LEDGER", "كشف الأستاذ", "التقارير المالية"),
                New PermissionDef("TREE.BALANCE_SHEET", "الميزانية العمومية", "التقارير المالية"),
                New PermissionDef("TREE.CASH_FLOW", "التدفقات النقدية", "التقارير المالية"),
                New PermissionDef("TREE.INCOME_STATEMENT", "قائمة الدخل", "التقارير المالية"),
                New PermissionDef("TREE.CURRENT_BALANCES", "الأرصدة الحالية", "التقارير المالية"),
                New PermissionDef("TREE.DAILY_REPORT", "التقرير اليومي", "التقارير المالية"),
                New PermissionDef("TREE.FINANCIAL_REPORTS", "تقارير مالية إضافية", "التقارير المالية"),
                New PermissionDef("TREE.COST_CENTER_BALANCES", "أرصدة مراكز التكلفة", "التقارير المالية"),
                New PermissionDef("TREE.SYSTEM_SETTINGS", "إدارة النظام", "إدارة النظام"),
                New PermissionDef("TREE.RECEIPT_SETTINGS", "إدارة السندات", "إدارة النظام"),
                New PermissionDef("TREE.INCOME_DESIGNER", "إدارة قوائم الدخل", "إدارة النظام"),
                New PermissionDef("TREE.CURRENCY_RATES", "إدارة أسعار العملات", "إدارة النظام"),
                New PermissionDef("TREE.ACCOUNT_PERMISSIONS", "إدارة صلاحيات الحسابات", "إدارة النظام"),
                New PermissionDef("TREE.USERS", "المستخدمين", "إدارة النظام")
            }
        End Get
    End Property

    Public Sub EnsureTreePermissionTable()
        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
IF OBJECT_ID('dbo.User_TreeMainForm_Permissions', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.User_TreeMainForm_Permissions
    (
        UserId INT NOT NULL,
        PermissionKey NVARCHAR(100) NOT NULL,
        IsAllowed BIT NOT NULL CONSTRAINT DF_User_TreeMainForm_Permissions_IsAllowed DEFAULT (0),
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_User_TreeMainForm_Permissions_CreatedAt DEFAULT (GETDATE()),
        CONSTRAINT PK_User_TreeMainForm_Permissions PRIMARY KEY (UserId, PermissionKey)
    );
END;", con)

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function LoadAllowedPermissions(userId As Integer, isAdmin As Boolean) As HashSet(Of String)
        Dim result As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        If isAdmin Then
            For Each p In AllPermissions
                result.Add(p.Key)
            Next
            Return result
        End If

        EnsureTreePermissionTable()

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
SELECT PermissionKey
FROM dbo.User_TreeMainForm_Permissions
WHERE UserId = @UserId
  AND IsAllowed = 1;", con)

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
                con.Open()

                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        result.Add(rd("PermissionKey").ToString())
                    End While
                End Using
            End Using
        End Using

        Return result
    End Function

    Public Sub SaveUserPermissions(userId As Integer, allowedKeys As IEnumerable(Of String))
        EnsureTreePermissionTable()

        Dim allowed As New HashSet(Of String)(allowedKeys, StringComparer.OrdinalIgnoreCase)

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            con.Open()

            Using tr = con.BeginTransaction()
                Using cmdDelete As New SqlCommand("
DELETE FROM dbo.User_TreeMainForm_Permissions
WHERE UserId = @UserId;", con, tr)
                    cmdDelete.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
                    cmdDelete.ExecuteNonQuery()
                End Using

                For Each p In AllPermissions
                    Using cmdInsert As New SqlCommand("
INSERT INTO dbo.User_TreeMainForm_Permissions (UserId, PermissionKey, IsAllowed)
VALUES (@UserId, @PermissionKey, @IsAllowed);", con, tr)
                        cmdInsert.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
                        cmdInsert.Parameters.Add("@PermissionKey", SqlDbType.NVarChar, 100).Value = p.Key
                        cmdInsert.Parameters.Add("@IsAllowed", SqlDbType.Bit).Value = allowed.Contains(p.Key)
                        cmdInsert.ExecuteNonQuery()
                    End Using
                Next

                tr.Commit()
            End Using
        End Using
    End Sub
End Module
