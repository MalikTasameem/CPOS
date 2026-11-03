Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Globalization
Imports System.Linq
Imports System.Text

Friend NotInheritable Class AuditReceiptSnapshot
    Friend Header As DataTable
    Friend Treasury As DataTable
    Friend ReadOnly References As New SortedDictionary(Of String, DataTable)(StringComparer.Ordinal)
End Class

' Ordinary receipts only: atomic business/audit, no legacy fallback, custody mutation, or reposting.
Friend NotInheritable Class AuditReceipt
    Private Shared ReadOnly Inv As CultureInfo = CultureInfo.InvariantCulture
    Friend Shared Function Operation(command As SqlCommand) As String
        If command Is Nothing Then Throw New ArgumentNullException("command")
        Dim name = command.CommandText.Replace("[", "").Replace("]", "").Trim()
        If name.StartsWith("dbo.", StringComparison.OrdinalIgnoreCase) Then name = name.Substring(4)
        Select Case name.ToLowerInvariant()
            Case "agents_balancemv_insert_rct" : Return "Receipt.Created"
            Case "agents_balancemv_update_rct" : Return "Receipt.Updated"
            Case "agents_balance_mv_rct_openforedit" : Return "Receipt.EditOpened"
            Case "agents_balance_mv_rct_void" : Return "Receipt.Voided"
            Case Else : Throw New ArgumentException("إجراء إيصال غير معتمد.")
        End Select
    End Function
    Private Shared Function Parameter(command As SqlCommand, name As String) As Object
        Dim p = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) x.ParameterName.TrimStart("@"c).Equals(name, StringComparison.OrdinalIgnoreCase))
        Return If(p Is Nothing OrElse p.Value Is Nothing, DBNull.Value, p.Value)
    End Function
    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand) As AuditOperationRequest
        Dim op = Operation(command), create = op = "Receipt.Created"
        Dim r As New AuditOperationRequest With {.Context = context.Copy(), .RequestId = Guid.NewGuid(), .OperationCode = op, .EntityType = "Receipt",
            .EntityId = If(create, "New", Convert.ToInt32(Parameter(command, "T_ID"), Inv).ToString(Inv))}
        Dim allowed = AuditContract.ReceiptInputs(op).Select(Function(d) d.Split("|"c)(1)).Where(Function(n) n <> "").Concat({"T_ID", "Receipt_Num", "User_ID"}).ToArray()
        For Each p As SqlParameter In command.Parameters
            If Not allowed.Contains(p.ParameterName.TrimStart("@"c), StringComparer.OrdinalIgnoreCase) Then Throw New ArgumentException("معامل إيصال خارج العقد: " & p.ParameterName)
        Next
        Dim user = Parameter(command, "User_ID")
        If Not IsDBNull(user) AndAlso Convert.ToInt64(user, Inv) <> context.ActorUserId.Value Then Throw New ArgumentException("مستخدم الإيصال لا يطابق جلسة المراقبة.")
        For Each definition In AuditContract.ReceiptInputs(op)
            Dim p = definition.Split("|"c), value As Object
            If p(0) = "ExpectedReceipt" Then
                value = New String("0"c, 64)
            ElseIf p(0).StartsWith("Planned", StringComparison.Ordinal) Then
                value = 1
            Else
                value = Parameter(command, p(1))
                If IsDBNull(value) Then
                    If p(3) = "R" Then Throw New ArgumentException("معامل إيصال مطلوب: " & p(1))
                Else
                    Select Case p(2)
                        Case "Integer" : value = Convert.ToInt32(value, CultureInfo.CurrentCulture)
                        Case "Decimal" : value = Convert.ToDecimal(value, CultureInfo.CurrentCulture)
                        Case "String" : value = Convert.ToString(value, CultureInfo.CurrentCulture)
                        Case "DateTime"
                            ' Freeze SQL datetime's 1/300-second precision before hashing.
                            value = New SqlDateTime(DateTime.SpecifyKind(Convert.ToDateTime(value, CultureInfo.CurrentCulture), DateTimeKind.Unspecified)).Value
                    End Select
                End If
            End If
            r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = p(2), .Value = AuditValue.Capture(p(2), value)})
        Next
        AuditContract.Request(r) : Return r
    End Function
    Private Shared Function Input(r As AuditOperationRequest, name As String) As Object
        Dim x = r.Inputs.Single(Function(i) i.Name = name)
        If x.Value.State = AuditValueState.SqlNull Then Return DBNull.Value
        Select Case x.ValueType
            Case "Integer" : Return Integer.Parse(x.Value.Text, Inv)
            Case "Decimal" : Return Decimal.Parse(x.Value.Text, Inv)
            Case "DateTime" : Return DateTime.SpecifyKind(DateTime.Parse(x.Value.Text, Inv, DateTimeStyles.RoundtripKind), DateTimeKind.Unspecified)
            Case Else : Return x.Value.Text
        End Select
    End Function
    Private Shared Function InputId(r As AuditOperationRequest, name As String) As Integer
        Dim value = Input(r, name) : Return If(IsDBNull(value), 0, CInt(value))
    End Function
    Private Shared Function Owner(r As AuditOperationRequest) As Integer
        Return If(r.OperationCode = "Receipt.Created", InputId(r, "PlannedReceiptId"), Integer.Parse(r.EntityId, Inv))
    End Function
    Private Shared Function Query(cn As SqlConnection, tx As SqlTransaction, sql As String, ParamArray values As Object()) As DataTable
        Using cmd As New SqlCommand(sql, cn, tx)
            cmd.CommandTimeout = 60
            For i = 0 To values.Length - 1
                cmd.Parameters.AddWithValue("@P" & i, If(values(i), DBNull.Value))
            Next
            Using reader = cmd.ExecuteReader()
                Dim table As New DataTable : table.Load(reader) : Return table
            End Using
        End Using
    End Function
    Private Shared Function Hex(bytes As Byte()) As String
        Return BitConverter.ToString(AuditCanonical.Hash(bytes)).Replace("-", "")
    End Function
    Friend Shared Function Fingerprint(table As DataTable) As String
        Dim tokens As New List(Of String) From {table.Rows.Count.ToString(Inv), table.Columns.Count.ToString(Inv)}
        For Each col As DataColumn In table.Columns
            tokens.Add(col.ColumnName) : tokens.Add(col.DataType.FullName)
        Next
        For Each row As DataRow In table.Rows
            For Each col As DataColumn In table.Columns
                Dim value = row(col)
                If IsDBNull(value) Then
                    tokens.Add(Nothing)
                ElseIf TypeOf value Is Byte() Then
                    tokens.Add(Hex(DirectCast(value, Byte())))
                ElseIf TypeOf value Is DateTime Then
                    tokens.Add(CDate(value).ToString("o", Inv))
                ElseIf TypeOf value Is Double Then
                    tokens.Add(CDbl(value).ToString("R", Inv))
                Else
                    tokens.Add(Convert.ToString(value, Inv))
                End If
            Next
        Next
        Return Hex(AuditCanonical.Encode(tokens))
    End Function
    Friend Shared Function Fingerprint(s As AuditReceiptSnapshot) As String
        Return Hex(AuditCanonical.Encode({Fingerprint(s.Header), Fingerprint(s.Treasury)}.Concat(s.References.SelectMany(Function(x) New String() {x.Key, Fingerprint(x.Value)}))))
    End Function
    Private Shared Sub Guard(cn As SqlConnection, tx As SqlTransaction)
        Dim expected As New Dictionary(Of String, String) From {
            {"AA_GET_MAX_ID", "71826097F08C19B6EC806AC2B253D5E97E088D6E3998608C65BE2CB43FDD8BC6"},
            {"AA_GET_MAX_Treasury_Balance_MV", "D24ABE96D14C602551217C53DBCCEBEB222551CB5E6E2DA2FA43260189EBE746"},
            {"AG_BalanceMV_GetMaxRecepitNum", "50178CF3B6BD341B2679927654FA9900A8D7707D155E14E016D5140F31C914E8"},
            {"Agents_BalanceMV_Update_RCT", "27FFA6EC9B43ABF9C8CE7CC2E55CA79A9DB6ABC2F20F671123D64803FDCD664E"},
            {"Agents_Balance_MV_RCT_OpenForEdit", "C36AF734D35422F4B6D22972265DBC7FA6A97B29B699C066BEFC1F4933EF7F1C"},
            {"Agents_Balance_MV_RCT_VOID", "38A55C3C816F3F73742C3B58D5AB5C8EFF5B4EE3A9918319E693F3D984AB409F"},
            {"Agents_Balance_MV_RCT_Cancel", "D8BC0954E2FB163784B85C37E51BE1C6A45BE95C7C55216D4025A9E47DA1B210"},
            {"Agents_BalanceMV_insert_RCT_Audited", "8A15D516C4ACEFB075A3EC6F62748FFE53D77FC0119CEF67ED3B4E37E957509B"}}
        For Each pair In expected
            Dim definition = Convert.ToString(Query(cn, tx, "SELECT OBJECT_DEFINITION(OBJECT_ID(@P0))", "dbo." & pair.Key).Rows(0)(0), Inv)
            If pair.Key = "Agents_BalanceMV_insert_RCT_Audited" Then definition = definition.Replace(vbCrLf, vbLf)
            If Hex(Encoding.Unicode.GetBytes(definition)) <> pair.Value Then Throw New InvalidOperationException("تغير إجراء من تبعيات الإيصالات؛ يلزم تثبيت/مراجعة SQL041: " & pair.Key)
        Next
        For Each entity In {"ReceiptHeader", "ReceiptTreasury"}
            Dim table = If(entity = "ReceiptHeader", "Agents_Balance_MV_RCT", "Treasury_Balance_MV")
            If CInt(Query(cn, tx, "SELECT COUNT(*) FROM sys.triggers WHERE parent_id=OBJECT_ID(@P0) AND is_disabled=0", "dbo." & table).Rows(0)(0)) <> 0 Then Throw New InvalidOperationException("يوجد Trigger غير مراجع على " & table)
            Dim columns = Query(cn, tx, "SELECT name FROM sys.columns WHERE object_id=OBJECT_ID(@P0) ORDER BY column_id", "dbo." & table).Rows.Cast(Of DataRow)().Select(Function(x) CStr(x(0))).ToArray()
            Dim fields = AuditContract.ReceiptFields(entity).Select(Function(x) x.Split("|"c)(1)).Where(Function(x) Not x.StartsWith("AuditImage", StringComparison.Ordinal)).ToList()
            If entity = "ReceiptHeader" Then fields.Add("Img")
            If columns.Length <> fields.Count OrElse columns.Except(fields, StringComparer.Ordinal).Any() Then Throw New InvalidOperationException("تغيرت أعمدة جدول الإيصال؛ يلزم تحديث عقد المراقبة.")
        Next
    End Sub
    Private Shared Function Allocation(cn As SqlConnection, tx As SqlTransaction) As Integer()
        Dim range = Query(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If range.Rows.Count <> 1 OrElse IsDBNull(range.Rows(0)(0)) OrElse IsDBNull(range.Rows(0)(1)) Then Throw New InvalidOperationException("يلزم نطاق ترقيم فرع واحد.")
        Dim first = CInt(range.Rows(0)(0)), last = CInt(range.Rows(0)(1))
        If first < 0 OrElse last <= first Then Throw New InvalidOperationException("نطاق ترقيم الفرع غير صالح.")
        Dim result As New List(Of Integer)
        For Each pair In {"Agents_Balance_MV_RCT|T_ID", "Agents_Balance_MV_RCT|Receipt_Num", "Treasury_Balance_MV|T_ID"}
            Dim p = pair.Split("|"c)
            Dim maximum = CLng(Query(cn, tx, "SELECT ISNULL(MAX([" & p(1) & "]),@P0) FROM dbo.[" & p(0) & "] WITH(UPDLOCK,HOLDLOCK) WHERE [" & p(1) & "] BETWEEN @P0 AND @P1", first, last).Rows(0)(0))
            If maximum >= last Then Throw New InvalidOperationException("انتهى نطاق ترقيم الإيصال/الخزينة.")
            result.Add(CInt(maximum + 1))
        Next
        Return result.ToArray()
    End Function
    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, Optional previous As AuditReceiptSnapshot = Nothing) As AuditReceiptSnapshot
        Dim s As New AuditReceiptSnapshot, id = Owner(r)
        Dim columns = AuditContract.ReceiptFields("ReceiptHeader").Select(Function(d) d.Split("|"c)(1)).Where(Function(x) Not x.StartsWith("AuditImage", StringComparison.Ordinal))
        s.Header = Query(cn, tx, "SELECT " & String.Join(",", columns.Select(Function(x) "[" & x & "]")) & ",CONVERT(varbinary(max),Img) AS AuditImage FROM dbo.Agents_Balance_MV_RCT WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@P0", id)
        s.Header.Columns.Add("AuditImageLength", GetType(Long)) : s.Header.Columns.Add("AuditImageHash", GetType(String))
        For Each row As DataRow In s.Header.Rows
            If Not IsDBNull(row("AuditImage")) Then
                Dim bytes = DirectCast(row("AuditImage"), Byte())
                row("AuditImageLength") = bytes.LongLength : row("AuditImageHash") = Hex(bytes)
            End If
        Next
        s.Header.Columns.Remove("AuditImage")
        s.Treasury = Query(cn, tx, "SELECT * FROM dbo.Treasury_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE AGBalance_T_ID=@P0 ORDER BY T_ID", id)
        If s.Treasury.Rows.Count > 99 Then Throw New InvalidOperationException("الإيصال يتجاوز 99 حركة خزينة؛ لم يتم إسقاط أي صف أو تنفيذ العملية.")
        Dim linked As New HashSet(Of Integer) From {id}
        If r.Inputs.Any(Function(x) x.Name = "Receipt_Tran_ID") AndAlso InputId(r, "Receipt_Tran_ID") > 0 Then linked.Add(InputId(r, "Receipt_Tran_ID"))
        For Each snapshotValue As AuditReceiptSnapshot In New AuditReceiptSnapshot() {s, previous}
            If snapshotValue Is Nothing Then Continue For
            For Each row As DataRow In snapshotValue.Header.Rows
                If Not IsDBNull(row("Receipt_Tran_ID")) AndAlso CInt(row("Receipt_Tran_ID")) > 0 Then linked.Add(CInt(row("Receipt_Tran_ID")))
            Next
        Next
        s.References("Invoices") = Query(cn, tx, "SELECT * FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID IN(" & String.Join(",", linked.OrderBy(Function(x) x).Select(Function(x) x.ToString(Inv))) & ") ORDER BY T_ID")
        s.References("Range") = Query(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(HOLDLOCK)")
        Dim number = If(r.OperationCode = "Receipt.Created", InputId(r, "PlannedReceiptNumber"), If(s.Header.Rows.Count = 1 AndAlso Not IsDBNull(s.Header.Rows(0)("Receipt_Num")), CInt(s.Header.Rows(0)("Receipt_Num")), 0))
        s.References("Custody") = Query(cn, tx, "SELECT T_ID,C_Rcpt_T_ID FROM dbo.Custody WITH(UPDLOCK,HOLDLOCK) WHERE C_Rcpt_T_ID=@P0 ORDER BY T_ID", number)
        Return s
    End Function
    Friend Shared Function CreationPermission(receiptType As Integer, linkedType As Integer) As String
        If Not {3, 4}.Contains(receiptType) Then Throw New ArgumentException("نوع الإيصال غير معتمد.")
        If linkedType = 0 Then Return If(receiptType = 3, "Sales", "Pch")
        Select Case linkedType
            Case 1 : Return "Sales"
            Case 7 : Return "Pch"
            Case 2 : Return "Exp"
            Case 10 : Return "SB_Rtn"
            Case 11 : Return "Pch_Rtn"
            Case Else : Throw New InvalidOperationException("نوع المستند المرتبط بالإيصال خارج نطاق النقل الحالي.")
        End Select
    End Function
    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, s As AuditReceiptSnapshot)
        Dim create = r.OperationCode = "Receipt.Created", flag As String
        If s.References("Custody").Rows.Count <> 0 Then Throw New InvalidOperationException("إيصالات العهد مؤجلة؛ لا يمكن تمريرها من مسار الإيصالات العادية.")
        If create Then
            If s.Header.Rows.Count <> 0 OrElse s.Treasury.Rows.Count <> 0 Then Throw New InvalidOperationException("رقم الإيصال المخطط مستخدم مسبقًا.")
            Dim linked = InputId(r, "Receipt_Tran_ID"), kind = 0
            If linked > 0 Then
                Dim invoice = s.References("Invoices").Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = linked)
                If invoice Is Nothing OrElse CInt(invoice("isVoid")) <> 0 Then Throw New InvalidOperationException("فاتورة الإيصال غير موجودة أو ملغاة.")
                kind = CInt(invoice("BsType_ID"))
            End If
            flag = CreationPermission(InputId(r, "BsType_ID"), kind)
        Else
            If s.Header.Rows.Count <> 1 OrElse s.Treasury.Rows.Count = 0 Then Throw New InvalidOperationException("الإيصال أو حركات خزينته غير مكتملة.")
            Dim row = s.Header.Rows(0)
            If Not {3, 4}.Contains(CInt(row("BsType_ID"))) OrElse CInt(row("isVoid")) <> 0 OrElse IsDBNull(row("Receipt_Num")) OrElse CInt(row("Receipt_Num")) <= 0 Then Throw New InvalidOperationException("الإيصال ملغى أو نوعه/رقمه غير معتمد.")
            If Not IsDBNull(row("Custody_ID")) AndAlso CInt(row("Custody_ID")) <> 0 Then Throw New InvalidOperationException("الإيصال مرتبط بعهدة مؤجلة.")
            If r.OperationCode = "Receipt.Updated" Then
                If InputId(r, "BsType_ID") <> CInt(row("BsType_ID")) Then Throw New InvalidOperationException("لا يمكن تغيير نوع الإيصال أثناء التعديل.")
                If IsDBNull(row("EditOpenedAt")) OrElse IsDBNull(row("EditOpenedBy")) OrElse CInt(row("EditOpenedBy")) <> r.Context.ActorUserId.Value Then Throw New InvalidOperationException("افتح تعديل الإيصال بالمستخدم الحالي أولًا.")
            End If
            flag = If(r.OperationCode = "Receipt.Voided", "Cancel_Receipt", "Update_Receipt")
        End If
        ' Match the login/UI authorization contract: an enabled administrator owns every
        ' receipt permission; other users must own the operation-specific role flag.
        If CInt(Query(cn, tx, "SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) LEFT JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON u.User_ID=v.User_ID WHERE u.User_ID=@P0 AND u.is_Allow=1 AND (u.isAdmin=1 OR ISNULL(v.[" & flag & "],0)=1)", r.Context.ActorUserId.Value).Rows(0)(0)) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية الإيصال: " & flag)
        If create OrElse r.OperationCode = "Receipt.Updated" Then
            For Each pair In {"Agents|AG_ID", "TreasuryCard|Tr_ID"}
                Dim p = pair.Split("|"c)
                If Query(cn, tx, "SELECT [" & p(1) & "] FROM dbo.[" & p(0) & "] WITH(HOLDLOCK) WHERE [" & p(1) & "]=@P0", InputId(r, p(1))).Rows.Count <> 1 Then Throw New InvalidOperationException("حساب الإيصال أو الخزينة غير موجود.")
            Next
            Dim payments = Query(cn, tx, "SELECT P_ID FROM dbo.PAYMENT_METHOD WITH(HOLDLOCK) ORDER BY P_ID")
            ' The existing screen offers cash ID 1 when the payment-method table is empty.
            If Not (payments.Rows.Count = 0 AndAlso InputId(r, "Pay_ID") = 1) AndAlso payments.Rows.Cast(Of DataRow)().Count(Function(x) Not IsDBNull(x(0)) AndAlso CInt(x(0)) = InputId(r, "Pay_ID")) <> 1 Then Throw New InvalidOperationException("طريقة دفع الإيصال غير موجودة أو مكررة.")
        End If
    End Sub
    Friend Shared Sub Prepare(connection As String, r As AuditOperationRequest)
        AuditContract.Request(r)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Guard(cn, tx)
                If r.OperationCode = "Receipt.Created" Then
                    Dim ids = Allocation(cn, tx), names = {"PlannedReceiptId", "PlannedReceiptNumber", "PlannedTreasuryId"}
                    For i = 0 To ids.Length - 1
                        Dim name = names(i)
                        r.Inputs.Single(Function(x) x.Name = name).Value = AuditValue.Capture("Integer", ids(i))
                    Next
                End If
                Dim before = Capture(cn, tx, r) : Permission(cn, tx, r, before)
                r.Inputs.Single(Function(x) x.Name = "ExpectedReceipt").Value = AuditValue.Capture("String", Fingerprint(before))
                AuditContract.Request(r) : tx.Commit()
            End Using
        End Using
    End Sub
    Private Shared Sub Mutate(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest)
        Dim create = r.OperationCode = "Receipt.Created", proc As String
        Select Case r.OperationCode
            Case "Receipt.Created" : proc = "Agents_BalanceMV_insert_RCT_Audited"
            Case "Receipt.Updated" : proc = "Agents_BalanceMV_Update_RCT"
            Case "Receipt.EditOpened" : proc = "Agents_Balance_MV_RCT_OpenForEdit"
            Case "Receipt.Voided" : proc = "Agents_Balance_MV_RCT_VOID"
            Case Else : Throw New ArgumentException("عملية إيصال غير معتمدة.")
        End Select
        Using cmd = AuditRequestCoordinator.Command("dbo." & proc, cn, tx)
            If create Then
                cmd.Parameters.Add("@T_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@Receipt_Num", SqlDbType.Int).Direction = ParameterDirection.Output
            Else
                cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = Owner(r)
            End If
            If r.OperationCode <> "Receipt.Voided" Then cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
            For Each definition In AuditContract.ReceiptInputs(r.OperationCode)
                Dim p = definition.Split("|"c)
                If p(1) = "" Then Continue For
                Dim sqlType = If(p(2) = "Integer", SqlDbType.Int, If(p(2) = "Decimal", SqlDbType.Decimal, If(p(2) = "DateTime", SqlDbType.DateTime, SqlDbType.NVarChar)))
                Dim parameter = cmd.Parameters.Add("@" & p(1), sqlType)
                If sqlType = SqlDbType.Decimal Then parameter.Precision = 18 : parameter.Scale = 3
                If sqlType = SqlDbType.NVarChar Then parameter.Size = If({"Bank_Name", "CheckNum"}.Contains(p(1)), 100, -1)
                parameter.Value = If(sqlType = SqlDbType.Decimal, Money(r, p(0)), Input(r, p(0)))
            Next
            cmd.ExecuteNonQuery()
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            If create AndAlso (Convert.ToInt32(cmd.Parameters("@T_ID").Value, Inv) <> Owner(r) OrElse Convert.ToInt32(cmd.Parameters("@Receipt_Num").Value, Inv) <> InputId(r, "PlannedReceiptNumber")) Then Throw New InvalidOperationException("تغير ترقيم الإيصال أثناء التنفيذ.")
        End Using
    End Sub
    Private Shared Function Same(a As Object, b As Object) As Boolean
        If IsDBNull(a) OrElse IsDBNull(b) Then Return IsDBNull(a) AndAlso IsDBNull(b)
        Return Object.Equals(a, b)
    End Function
    Private Shared Sub RequireValue(row As DataRow, column As String, expected As Object)
        If Not Same(row(column), expected) Then Throw New InvalidOperationException("أثر الإيصال غير مطابق: " & column)
    End Sub
    Private Shared Function Money(r As AuditOperationRequest, name As String) As Object
        Dim value = Input(r, name)
        Return If(IsDBNull(value), value, CObj(Decimal.Round(CDec(value), 3, MidpointRounding.AwayFromZero)))
    End Function
    Friend Shared Sub Verify(r As AuditOperationRequest, before As AuditReceiptSnapshot, after As AuditReceiptSnapshot)
        Dim create = r.OperationCode = "Receipt.Created"
        For Each pair In before.References
            If Not after.References.ContainsKey(pair.Key) OrElse Fingerprint(pair.Value) <> Fingerprint(after.References(pair.Key)) Then Throw New InvalidOperationException("تغير مستند/مرجع خارج نطاق الإيصال: " & pair.Key)
        Next
        If after.Header.Rows.Count <> 1 OrElse after.Treasury.Rows.Count <> If(create, 1, before.Treasury.Rows.Count) Then Throw New InvalidOperationException("نتيجة الإيصال أو حركات خزينته غير مكتملة.")
        Dim h = after.Header.Rows(0), kind = CInt(h("BsType_ID"))
        RequireValue(h, "T_ID", Owner(r))
        If create Then
            RequireValue(h, "Receipt_Num", InputId(r, "PlannedReceiptNumber"))
            RequireValue(after.Treasury.Rows(0), "T_ID", InputId(r, "PlannedTreasuryId"))
        ElseIf Not before.Treasury.Rows.Cast(Of DataRow)().Select(Function(x) CInt(x("T_ID"))).SequenceEqual(after.Treasury.Rows.Cast(Of DataRow)().Select(Function(x) CInt(x("T_ID")))) Then
            Throw New InvalidOperationException("تبدلت هوية حركة خزينة الإيصال.")
        End If
        If create OrElse r.OperationCode = "Receipt.Updated" Then
            For Each name In {"AG_ID", "Date", "Receipt_Title", "About", "BsType_ID", "Pay_ID"}
                RequireValue(h, name, Input(r, name))
            Next
            RequireValue(h, "Pure", Money(r, "Pure")) : RequireValue(h, "Discount", Money(r, "Discount"))
            If create OrElse kind = 3 Then RequireValue(h, "Tr_ID", Input(r, "Tr_ID"))
            If Not create Then RequireValue(h, If(kind = 3, "Debit", "Credit"), Money(r, "Pure"))
            For Each tr As DataRow In after.Treasury.Rows
                RequireValue(tr, "AGBalance_T_ID", Owner(r)) : RequireValue(tr, "Tr_ID", Input(r, "Tr_ID"))
                RequireValue(tr, "date", Input(r, "Date")) : RequireValue(tr, "notice_move", Input(r, "Receipt_Title"))
                RequireValue(tr, "Pure", Money(r, "Pure")) : RequireValue(tr, If(kind = 3, "Credit", "Debit"), Money(r, "Pure"))
            Next
        End If
        If create Then
            Dim expected As New Dictionary(Of String, Object)(StringComparer.Ordinal)
            For Each col As DataColumn In after.Header.Columns
                expected(col.ColumnName) = DBNull.Value
            Next
            For Each name In {"T_ID", "Receipt_Num", "Receipt_Tran_ID", "AG_ID", "Date", "Receipt_Title", "About", "BsType_ID", "Pure", "Pay_ID", "Tr_ID", "Discount"}
                expected(name) = h(name)
            Next
            expected("Pr_ID") = If(InputId(r, "Pr_ID") = 0, CObj(DBNull.Value), Input(r, "Pr_ID"))
            expected("Receipt_Tran_ID") = Input(r, "Receipt_Tran_ID")
            expected("User_ID") = CInt(r.Context.ActorUserId.Value)
            expected("isDepended") = 1 : expected("isVoid") = 0 : expected("isPied") = 1 : expected("Prt_Counter") = 0
            expected("NeedRepost") = False : expected("NeedCancelReverse") = False : expected("EditVersion") = 0
            For Each col As DataColumn In after.Header.Columns
                RequireValue(h, col.ColumnName, expected(col.ColumnName))
            Next
            Dim tr = after.Treasury.Rows(0)
            Dim expectedTr As New Dictionary(Of String, Object)(StringComparer.Ordinal)
            For Each col As DataColumn In after.Treasury.Columns
                expectedTr(col.ColumnName) = DBNull.Value
            Next
            For Each name In {"T_ID", "AGBalance_T_ID", "Tr_ID", "date", "notice_move", "Pure", If(kind = 3, "Credit", "Debit")}
                expectedTr(name) = tr(name)
            Next
            expectedTr("Bank_Name") = Input(r, "Bank_Name") : expectedTr("CheckNum") = Input(r, "CheckNum")
            expectedTr("Tr_Type_ID") = If(kind = 3, 2, 1) : expectedTr("User_ID") = CInt(r.Context.ActorUserId.Value)
            expectedTr("isVoid") = False : expectedTr("isFirst_Time") = 0 : expectedTr("NeedRepost") = False : expectedTr("NeedCancelReverse") = False : expectedTr("EditVersion") = 0
            For Each col As DataColumn In after.Treasury.Columns
                RequireValue(tr, col.ColumnName, expectedTr(col.ColumnName))
            Next
        Else
            Dim old = before.Header.Rows(0)
            For Each entity In {"ReceiptHeader", "ReceiptTreasury"}
                Dim olds = If(entity = "ReceiptHeader", before.Header, before.Treasury), news = If(entity = "ReceiptHeader", after.Header, after.Treasury)
                For Each row As DataRow In news.Rows
                    Dim prior = olds.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(row("T_ID")))
                    For Each definition In AuditContract.ReceiptFields(entity)
                        Dim p = definition.Split("|"c)
                        If Not AuditContract.ReceiptAllowed(r.OperationCode, entity, p(0)) Then RequireValue(row, p(1), prior(p(1)))
                    Next
                Next
            Next
            RequireValue(h, "AuditImageLength", old("AuditImageLength")) : RequireValue(h, "AuditImageHash", old("AuditImageHash"))
            Select Case r.OperationCode
                Case "Receipt.EditOpened"
                    RequireValue(h, "NeedRepost", Not IsDBNull(old("JournalId")))
                    RequireValue(h, "OriginalJournalId", If(IsDBNull(old("JournalId")), old("OriginalJournalId"), If(IsDBNull(old("OriginalJournalId")), old("JournalId"), old("OriginalJournalId"))))
                    RequireValue(h, "EditOpenedBy", CInt(r.Context.ActorUserId.Value))
                    If IsDBNull(h("EditOpenedAt")) Then Throw New InvalidOperationException("لم يفتح تعديل الإيصال.")
                Case "Receipt.Updated"
                    RequireValue(h, If(kind = 3, "Credit", "Debit"), old(If(kind = 3, "Credit", "Debit")))
                    If kind = 4 Then RequireValue(h, "Tr_ID", old("Tr_ID"))
                    For Each tr As DataRow In after.Treasury.Rows
                        Dim prior = before.Treasury.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(tr("T_ID")))
                        RequireValue(tr, If(kind = 3, "Debit", "Credit"), prior(If(kind = 3, "Debit", "Credit")))
                    Next
                Case "Receipt.Voided"
                    RequireValue(h, "isVoid", 1) : RequireValue(h, "VoidBy", 1) : RequireValue(h, "VoidReason", "إلغاء الإيصال")
                    RequireValue(h, "NeedCancelReverse", Not IsDBNull(old("JournalId"))) : RequireValue(h, "NeedRepost", False)
                    If IsDBNull(h("VoidAt")) Then Throw New InvalidOperationException("لم يسجل تاريخ الإلغاء.")
                    For Each tr As DataRow In after.Treasury.Rows
                        RequireValue(tr, "isVoid", True)
                    Next
            End Select
        End If
    End Sub
    Friend Shared Function Events(r As AuditOperationRequest, before As AuditReceiptSnapshot, after As AuditReceiptSnapshot, utc As DateTime) As List(Of AuditEventRequest)
        Verify(r, before, after)
        Dim eventsList As New List(Of AuditEventRequest), create = r.OperationCode = "Receipt.Created", header = after.Header.Rows(0)
        For Each entity In {"ReceiptHeader", "ReceiptTreasury"}
            Dim news = If(entity = "ReceiptHeader", after.Header, after.Treasury), olds = If(entity = "ReceiptHeader", before.Header, before.Treasury)
            For Each row As DataRow In news.Rows
                Dim e As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = eventsList.Count + 1, .EventCode = r.OperationCode, .Outcome = 1,
                    .EntityType = "Receipt", .EntityId = Owner(r).ToString(Inv), .DocumentNumber = CInt(header("Receipt_Num")).ToString(Inv), .BusinessDate = CDate(header("Date")), .OccurredAtUtc = utc}
                Dim prior = If(create, Nothing, olds.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(row("T_ID"))))
                For Each definition In AuditContract.ReceiptFields(entity)
                    Dim p = definition.Split("|"c)
                    e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = CInt(row("T_ID")).ToString(Inv), .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                        .Before = If(create, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), prior(p(1)))), .After = AuditValue.Capture(p(2), row(p(1)))})
                Next
                eventsList.Add(e)
            Next
        Next
        If eventsList.Count > 100 Then Throw New InvalidOperationException("تفاصيل الإيصال تتجاوز سعة المراقبة؛ تم رفض العملية كاملة.")
        For Each e In eventsList
            AuditContract.EventData(r, e)
        Next
        Return eventsList
    End Function
    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable Then Throw New InvalidOperationException("الإيصال يتطلب معاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, r)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Guard(cn, tx)
            If r.OperationCode = "Receipt.Created" Then
                Dim ids = Allocation(cn, tx)
                If Not ids.SequenceEqual({InputId(r, "PlannedReceiptId"), InputId(r, "PlannedReceiptNumber"), InputId(r, "PlannedTreasuryId")}) Then Throw New InvalidOperationException("تغير ترقيم الإيصال؛ أعد التحضير قبل الحفظ.")
            End If
            Dim before = Capture(cn, tx, r) : Permission(cn, tx, r, before)
            If Fingerprint(before) <> CStr(Input(r, "ExpectedReceipt")) Then Throw New InvalidOperationException("تغير الإيصال أو مرجعه منذ التحضير؛ أعد تحميله.")
            Mutate(cn, tx, r)
            Dim after = Capture(cn, tx, r, before), eventsList = Events(r, before, after, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            For Each e In eventsList
                AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1,
                New Dictionary(Of String, String) From {{"EntityId", Owner(r).ToString(Inv)}, {"DocumentNumber", CInt(after.Header.Rows(0)("Receipt_Num")).ToString(Inv)}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function
    Friend Shared Function Save(connection As String, request As AuditOperationRequest) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim result = ExecuteInTransaction(cn, tx, request)
                    tx.Commit() : Return result
                Catch
                    Try
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    Catch
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Function
End Class
