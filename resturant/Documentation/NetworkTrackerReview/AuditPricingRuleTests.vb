Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditPricingRuleTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const GroupId As Integer = 2147481900
    Private Const ItemId As Integer = 2147481901
    Private ReadOnly Tables As String() = {"IM_Menu", "General_menu", "OFFERS_MASTER", "Change_Price", "IM_Units", "SB_Contents", "Agents_Balance_MV", "Users", "Users_Validations", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.PricingFields("OfferSnapshotRow").Length = 8, "8 actual offer fields without invented key")
            Check(AuditContract.PricingFields("PriceRule").Length = 3, "3 actual percentage fields")
            Check(AuditContract.Domain("Offer.ItemOffersDeleted") = "Catalog.PricingRules", "closed pricing domain")
            Check(AuditContract.PricingInputs("Offer.Created").Length = 6, "offer input catalogue")
            Reject(Sub() AuditContract.PricingInputs("Offer.Arbitrary"), "unknown operation denied", "غير معتمدة")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " pricing-rule checks.") : Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function

    Private Sub Check(value As Boolean, label As String)
        If Not value Then Throw New Exception("FAILED " & label)
        checks += 1 : Console.WriteLine("PASS " & label)
    End Sub

    Private Sub Reject(work As Action, label As String, expected As String)
        Try
            work()
        Catch ex As Exception
            If expected.Length > 0 AndAlso Not ex.ToString().Contains(expected) Then Throw New Exception("Unexpected rejection: " & label, ex)
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection: " & label)
    End Sub

    Private Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx) With {.CommandTimeout = 30}
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Function SavedId(result As AuditRequestResult) As Integer
        Return Integer.Parse(AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
    End Function

    Private Function OfferCommand(Optional delete As Boolean = False) As SqlCommand
        Dim cmd As New SqlCommand(If(delete, "OFFERS_MASTER_DELETE", "OFFERS_MASTER_INSERT"))
        cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = ItemId
        If Not delete Then
            cmd.Parameters.Add("@TITLE", SqlDbType.NVarChar, -1).Value = "عرض اختبار عربي"
            cmd.Parameters.Add("@DATE_F", SqlDbType.DateTime).Value = New DateTime(2026, 10, 1, 12, 1, 2)
            cmd.Parameters.Add("@DATE_T", SqlDbType.DateTime).Value = New DateTime(2026, 10, 31, 23, 1, 2)
            cmd.Parameters.Add("@TIME_F", SqlDbType.DateTime).Value = New DateTime(2026, 10, 1, 8, 30, 15, 750)
            cmd.Parameters.Add("@TIME_T", SqlDbType.DateTime).Value = New DateTime(2026, 10, 1, 21, 45, 1, 250)
            cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = 999999
        End If
        Return cmd
    End Function

    Private Function RuleCommand(Optional target As Integer = 0, Optional percent As Decimal = 12.345D) As SqlCommand
        Dim cmd As New SqlCommand(If(target = 0, "Change_Price_Insert", "Change_Price_DELETE"))
        cmd.Parameters.Add(If(target = 0, "@GM_ID", "@T_ID"), SqlDbType.Int).Value = If(target = 0, GroupId, target)
        If target = 0 Then cmd.Parameters.AddWithValue("@Percent_Price", percent)
        Return cmd
    End Function

    Private Function Request(cn As SqlConnection, tx As SqlTransaction, cmd As SqlCommand) As AuditOperationRequest
        Dim offer = AuditPricingRules.Operation(cmd).StartsWith("Offer.", StringComparison.Ordinal)
        context.ScreenCode = If(offer, "OFFERS", "IM_Change_Price_ByPercent")
        Return AuditPricingRules.Request(context, cmd, AuditPricingRules.Capture(cn, tx, offer, If(offer, ItemId, GroupId)))
    End Function

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "INSERT dbo.General_menu(GM_ID,GM_Name,Printer_ID,POS_isShow,Rank_Num,POS_ST_ID,Ksh_Screen,Cat_ID) VALUES(" & GroupId & ",N'Audit pricing group',1,1,2,1,NULL,NULL)")
                    Q(cn, tx, "INSERT dbo.IM_Menu(IM_ID,item_name,item_nameSales,GM_ID,isValid,Row_Enabled) VALUES(" & ItemId & ",N'صنف عرض اختبار',N'صنف عرض اختبار بيع'," & GroupId & ",0,1)")
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Function OfferHash(cn As SqlConnection, tx As SqlTransaction) As String
        Dim rows = AuditSalesQuantity.Rows(cn, tx, "SELECT IM_ID,[DATE],TITLE,DATE_F,DATE_T,TIME_F,TIME_T,USER_ID FROM dbo.OFFERS_MASTER")
        Return String.Join("|", rows.Rows.Cast(Of DataRow)().Select(Function(x) AuditPricingRules.RowKey(x, True)).OrderBy(Function(x) x, StringComparer.Ordinal))
    End Function

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Pricing022 rollback", .ConnectTimeout = 5, .Pooling = False}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "022", .ScreenCode = "OFFERS"}
        Dim counts As Long(), definitions As String(), domains As String, unitsHash As String, offersHash As String, rulesHash As String, linesHash As String, billsHash As String
        Dim objects = {"OFFERS_MASTER_INSERT", "OFFERS_MASTER_DELETE", "Change_Price_Insert", "Change_Price_DELETE", "Check_IM_OFFER", "IM_Menu_Units_V", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(" & String.Join(",", Tables.Select(Function(x) "OBJECT_ID('dbo." & x & "')")) & ")")) = 0, "no unreviewed triggers")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Menu WHERE IM_ID=" & ItemId)) = 0, "unused item fixture")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.General_menu WHERE GM_ID=" & GroupId)) = 0, "unused group fixture")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.OFFERS_MASTER WHERE IM_ID=" & ItemId)) = 0, "no existing fixture offers")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Change_Price WHERE GM_ID=" & GroupId)) = 0, "no existing fixture rules")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditPricingFail022')") Is DBNull.Value, "failure trigger name unused")
            counts = Tables.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & x & "]"))).ToArray()
            definitions = objects.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
            unitsHash = AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.IM_Units ORDER BY U_IM_ID"))
            offersHash = OfferHash(cn, Nothing)
            rulesHash = AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.Change_Price ORDER BY T_ID"))
            linesHash = AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.SB_Contents ORDER BY T_ID"))
            billsHash = AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.Agents_Balance_MV ORDER BY T_ID"))
        End Using
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, OfferCommand()), result = AuditPricingRules.ExecuteInTransaction(cn, tx, r)
                    Dim row = AuditPricingRules.Capture(cn, tx, True, ItemId).Rows.Rows(0)
                    Check(SavedId(result) = ItemId AndAlso CStr(row("TITLE")) = "عرض اختبار عربي", "offer created for actual item")
                    Check(CDate(row("DATE_F")) = New DateTime(2026, 10, 1) AndAlso CDate(row("DATE_T")) = New DateTime(2026, 10, 31), "dates retain original SQL date conversion")
                    Check(CType(row("TIME_F"), TimeSpan) = New TimeSpan(8, 30, 16) AndAlso CType(row("TIME_T"), TimeSpan) = New TimeSpan(21, 45, 1), "time(0) retains SQL rounding")
                    Check(CInt(row("USER_ID")) = 1, "confirmed actor overrides untrusted command user")
                    Check(CDate(row("DATE")).Kind = DateTimeKind.Unspecified, "business timestamp is not claimed as UTC")
                    Check(AuditPricingRules.ExecuteInTransaction(cn, tx, r).Status = "Completed", "offer retry returns completed")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.OFFERS_MASTER WHERE IM_ID=" & ItemId)) = 1, "offer retry creates no duplicate")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.BeforeState=1")) = 8, "all new offer fields captured")
                    r.Inputs.Single(Function(x) x.Name = "OfferTitle").Value = AuditValue.Capture("String", "changed payload")
                    Reject(Sub() AuditPricingRules.ExecuteInTransaction(cn, tx, r), "request id cannot change offer payload", "")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim cmd = OfferCommand()
                    cmd.Parameters("@TITLE").Value = New String("ع"c, 4001)
                    cmd.Parameters("@TIME_F").Value = New DateTime(2026, 10, 2, 23, 59, 59, 900)
                    cmd.Parameters("@TIME_T").Value = New DateTime(2026, 10, 1, 1, 0, 0)
                    cmd.Parameters("@DATE_F").Value = New DateTime(2026, 11, 5)
                    AuditPricingRules.ExecuteInTransaction(cn, tx, Request(cn, tx, cmd))
                    Dim row = AuditPricingRules.Capture(cn, tx, True, ItemId).Rows.Rows(0)
                    Check(CStr(row("TITLE")).Length = 4000, "actual stored title length")
                    Check(CType(row("TIME_F"), TimeSpan) = CType(Q(cn, tx, "SELECT CAST(CAST('2026-10-02T23:59:59.900' AS datetime) AS time(0))"), TimeSpan), "end-of-day SQL conversion retained")
                    Check(CDate(row("DATE_F")) > CDate(row("DATE_T")), "no hidden new date-order rule")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "DECLARE @i int=0; WHILE @i<25 BEGIN INSERT dbo.OFFERS_MASTER(IM_ID,[DATE],TITLE,DATE_F,DATE_T,TIME_F,TIME_T,USER_ID) VALUES(" & ItemId & ",NULL,N'identical offer '+CONVERT(nvarchar(10),@i%5),NULL,NULL,NULL,NULL,NULL); SET @i+=1; END")
                    Dim before = AuditPricingRules.Capture(cn, tx, True, ItemId)
                    Dim originalHash = AuditPricingRules.Fingerprint(before)
                    Dim reverse = before.Rows.Clone()
                    For Each row In before.Rows.Rows.Cast(Of DataRow)().Reverse()
                        reverse.ImportRow(row)
                    Next
                    before.Rows = reverse
                    Check(AuditPricingRules.Fingerprint(before) = originalHash, "snapshot insensitive to row enumeration order")
                    Dim addition = Request(cn, tx, OfferCommand())
                    AuditPricingRules.ExecuteInTransaction(cn, tx, addition)
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.OFFERS_MASTER WHERE IM_ID=" & ItemId)) = 26, "adding preserves all duplicate occurrences")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & addition.RequestId.ToString() & "' AND c.BeforeState=1")) = 8, "only new occurrence reported created")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & addition.RequestId.ToString() & "'")) = 4, "large offer snapshots paged without loss")
                    Dim removal = Request(cn, tx, OfferCommand(True))
                    AuditPricingRules.ExecuteInTransaction(cn, tx, removal)
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.OFFERS_MASTER WHERE IM_ID=" & ItemId)) = 0, "delete all offers by item preserved")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & removal.RequestId.ToString() & "' AND c.AfterState=1")) = 208, "all 26 deleted rows and nulls captured")
                    Check(AuditPricingRules.ExecuteInTransaction(cn, tx, removal).Status = "Completed", "delete-all replay")
                    Reject(Sub() AuditPricingRules.ExecuteInTransaction(cn, tx, Request(cn, tx, OfferCommand(True))), "new delete of missing offers is rejected", "غير موجود")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim cmd = RuleCommand(), r = Request(cn, tx, cmd), concurrent = Request(cn, tx, cmd)
                    Dim result = AuditPricingRules.ExecuteInTransaction(cn, tx, r), id = SavedId(result)
                    Check(id > 0 AndAlso CDec(Q(cn, tx, "SELECT Percent_Price FROM dbo.Change_Price WHERE T_ID=" & id)) = 12.35D, "actual percentage rounding and identity")
                    Check(SavedId(AuditPricingRules.ExecuteInTransaction(cn, tx, r)) = id, "percentage replay returns original identity")
                    Reject(Sub() AuditPricingRules.ExecuteInTransaction(cn, tx, concurrent), "second pre-read request detects changed group", "تغيرت")
                    Reject(Sub() AuditPricingRules.ExecuteInTransaction(cn, tx, Request(cn, tx, cmd)), "duplicate rule checked inside transaction", "مسبقًا")
                    Dim deletion = Request(cn, tx, RuleCommand(id))
                    AuditPricingRules.ExecuteInTransaction(cn, tx, deletion)
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Change_Price WHERE GM_ID=" & GroupId)) = 0, "selected percentage deleted")
                    Check(AuditPricingRules.ExecuteInTransaction(cn, tx, deletion).Status = "Completed", "percentage deletion replay")
                End Sub)
        For Each value In {0D, -10.25D, 150.25D, 12.345D, 12.355D, 99.995D, -12.345D}
            Dim percent = value
            Fixture(Sub(cn, tx)
                        AuditPricingRules.ExecuteInTransaction(cn, tx, Request(cn, tx, RuleCommand(0, percent)))
                        Using original As New SqlCommand("SELECT CAST(@value AS numeric(18,2))", cn, tx)
                            original.Parameters.AddWithValue("@value", Convert.ToDouble(percent))
                            Check(CDec(Q(cn, tx, "SELECT Percent_Price FROM dbo.Change_Price WHERE GM_ID=" & GroupId)) = CDec(original.ExecuteScalar()), "original percentage range and rounding " & percent.ToString(CultureInfo.InvariantCulture))
                        End Using
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "INSERT dbo.Change_Price([DATE],GM_ID,Percent_Price) VALUES(GETDATE()," & GroupId & ",1),(GETDATE()," & GroupId & ",2)")
                    Dim target = CInt(Q(cn, tx, "SELECT MIN(T_ID) FROM dbo.Change_Price WHERE GM_ID=" & GroupId))
                    AuditPricingRules.ExecuteInTransaction(cn, tx, Request(cn, tx, RuleCommand(target)))
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Change_Price WHERE GM_ID=" & GroupId)) = 1, "delete selected existing duplicate rule only")
                    Check(CDec(Q(cn, tx, "SELECT Percent_Price FROM dbo.Change_Price WHERE GM_ID=" & GroupId)) = 2D, "other percentage unchanged")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, OfferCommand())
                    Q(cn, tx, "INSERT dbo.OFFERS_MASTER(IM_ID,TITLE) VALUES(" & ItemId & ",N'concurrent')")
                    Reject(Sub() AuditPricingRules.ExecuteInTransaction(cn, tx, r), "stale offer multiset rejected", "تغيرت")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, OfferCommand())
                    Q(cn, tx, "UPDATE dbo.IM_Menu SET item_name=N'changed' WHERE IM_ID=" & ItemId)
                    Reject(Sub() AuditPricingRules.ExecuteInTransaction(cn, tx, r), "stale item context rejected", "تغيرت")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, RuleCommand())
                    r.Context.ScreenCode = "OFFERS"
                    Reject(Sub() AuditContract.Request(r), "wrong pricing screen denied", "غير معتمدة")
                    r.Context.ScreenCode = "IM_Change_Price_ByPercent"
                    r.Inputs.Add(New AuditInput With {.Name = "Arbitrary", .ValueType = "String", .Value = AuditValue.Capture("String", "x")})
                    Reject(Sub() AuditContract.Request(r), "arbitrary field denied", "")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, OfferCommand())
                    Q(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                    Reject(Sub() AuditPricingRules.ExecuteInTransaction(cn, tx, r), "disabled user denied", "غير مسموح")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, RuleCommand())
                    Q(cn, tx, "UPDATE dbo.Users_Validations SET IM=0 WHERE T_ID=1")
                    Reject(Sub() AuditPricingRules.ExecuteInTransaction(cn, tx, r), "revoked permission denied", "صلاحية")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, OfferCommand())
                    Q(cn, tx, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Catalog.PricingRules""]') WHERE ConfigId=1")
                    Reject(Sub() AuditPricingRules.ExecuteInTransaction(cn, tx, r), "disabled pricing gate blocks save", "")
                End Sub)
        For Each operation In {"Offer.Created", "Offer.ItemOffersDeleted", "PriceRule.Created", "PriceRule.Deleted"}
            Dim op = operation
            Fixture(Sub(cn, tx)
                        Dim cmd As SqlCommand
                        If op.StartsWith("Offer.", StringComparison.Ordinal) Then
                            If op = "Offer.ItemOffersDeleted" Then Q(cn, tx, "INSERT dbo.OFFERS_MASTER(IM_ID,TITLE) VALUES(" & ItemId & ",N'fixture')")
                            cmd = OfferCommand(op = "Offer.ItemOffersDeleted")
                        ElseIf op = "PriceRule.Deleted" Then
                            Q(cn, tx, "INSERT dbo.Change_Price([DATE],GM_ID,Percent_Price) VALUES(GETDATE()," & GroupId & ",1)")
                            cmd = RuleCommand(CInt(Q(cn, tx, "SELECT T_ID FROM dbo.Change_Price WHERE GM_ID=" & GroupId)))
                        Else
                            cmd = RuleCommand()
                        End If
                        Dim r = Request(cn, tx, cmd)
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditPricingFail022 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventCode IN('Offer.Created','Offer.ItemOffersDeleted','PriceRule.Created','PriceRule.Deleted') AND EventOrdinal=2) THROW 51122,'forced pricing detail failure',1; END")
                        Reject(Sub() AuditPricingRules.ExecuteInTransaction(cn, tx, r), "detail audit failure rolls back " & op, "forced pricing detail failure")
                    End Sub)
        Next
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To Tables.Length - 1
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & Tables(i) & "]")) = counts(i), "rollback count " & Tables(i))
            Next
            For i = 0 To objects.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & objects(i) & "'))")) = definitions(i), "definition unchanged " & objects(i))
            Next
            Check(AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.IM_Units ORDER BY U_IM_ID")) = unitsHash, "all stored unit prices unchanged")
            Check(OfferHash(cn, Nothing) = offersHash, "all original offer occurrences unchanged")
            Check(AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.Change_Price ORDER BY T_ID")) = rulesHash, "all original percentage rows unchanged")
            Check(AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.SB_Contents ORDER BY T_ID")) = linesHash, "all invoice lines unchanged")
            Check(AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.Agents_Balance_MV ORDER BY T_ID")) = billsHash, "all invoice headers unchanged")
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "domain rollback")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1")) = 0, "no incomplete requests")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditPricingFail022')") Is DBNull.Value, "failure trigger removed")
        End Using
        Console.WriteLine("Rollback leaves no fixture rows. Change_Price identity values may retain ordinary gaps; no reseed.")
    End Sub
End Module
