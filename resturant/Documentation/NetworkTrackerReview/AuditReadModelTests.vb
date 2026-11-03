Option Strict On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic

' Test-only application globals. This file must NOT be included in resturant.vbproj.
Friend Module AuditTestGlobals
    Public User_isAdmin As Boolean = True
    Public USER_ID As Integer = 1
    Public MY_Settings As New AuditTestSettings()
End Module
Friend Class AuditTestSettings
    Public SqlConStr As String
End Class

' Suppress application Shown handlers (and therefore database loading) during bitmap rendering.
Friend Class AuditBrowserPreview
    Inherits Network_Audit_Browser
    Protected Overrides Sub OnShown(e As EventArgs)
    End Sub
    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean
        Get
            Return True
        End Get
    End Property
End Class
Friend Class AuditDetailsPreview
    Inherits Network_Audit_Details
    Protected Overrides Sub OnShown(e As EventArgs)
    End Sub
    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean
        Get
            Return True
        End Get
    End Property
End Class

Friend Module AuditReadModelTests
    Private passed As Integer

    <STAThread>
    Public Function Main(args As String()) As Integer
        Try
            Dim settings As String = Nothing
            Dim output As String = "."
            For i As Integer = 0 To args.Length - 2
                If args(i) = "--settings" Then settings = args(i + 1)
                If args(i) = "--output" Then output = args(i + 1)
            Next
            UnitTests()
            PrintLayoutTest(output, Array.IndexOf(args, "--render") >= 0)
            FailedRefreshTest()
            If settings IsNot Nothing Then LiveTests(ReadConnection(settings), Array.IndexOf(args, "--rollback-write-tests") >= 0)
            If Array.IndexOf(args, "--render") >= 0 Then RenderScreens(output)
            Console.WriteLine("PASS: " & passed & " assertions. No business tables were written.")
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.GetType().Name & ": " & ex.Message & Environment.NewLine & ex.StackTrace)
            Return 1
        End Try
    End Function

    Private Sub Check(condition As Boolean, name As String)
        If Not condition Then Throw New Exception("FAILED: " & name)
        passed += 1
        Console.WriteLine("PASS " & name)
    End Sub

    Private Sub PrintLayoutTest(output As String, savePreview As Boolean)
        Using form As New AuditBrowserPreview(), bitmap As New Bitmap(1200, 800), drawingGraphics As Graphics = Graphics.FromImage(bitmap)
            drawingGraphics.Clear(Color.White)
            Dim table As New DataTable()
            table.Columns.Add("EventTime", GetType(DateTime))
            For Each name In {"ScreenLabel", "ActionLabel", "OutcomeLabel", "ActorName", "DocumentNumber", "Notes"}
                table.Columns.Add(name, GetType(String))
            Next
            table.Rows.Add(New DateTime(2026, 10, 28, 10, 0, 0), "Receipt", "Receipt.Created", "نجاح", "المدير", "15", "إنشاء إيصال")
            table.Rows.Add(New DateTime(2026, 10, 28, 11, 0, 0), "Sales", "Line.Added", "نجاح", "المدير", "16", "إضافة صنف")
            Dim type = GetType(Network_Audit_Browser)
            type.GetField("printRows", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).SetValue(form, table)
            type.GetField("printFilterDescription", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).SetValue(form, "اختبار معاينة الطباعة")
            Dim args As New Drawing.Printing.PrintPageEventArgs(drawingGraphics, New Rectangle(40, 40, 1120, 720),
                                                                 New Rectangle(0, 0, 1200, 800), New Drawing.Printing.PageSettings())
            type.GetMethod("ResultsPrintDocument_PrintPage", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).
                Invoke(form, New Object() {form.ResultsPrintDocument, args})
            Dim rowIndex = CInt(type.GetField("printRowIndex", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(form))
            Check(rowIndex = 2 AndAlso Not args.HasMorePages, "print layout renders all displayed rows without a printer")
            If savePreview Then
                drawingGraphics.Flush()
                Dim path = IO.Path.Combine(output, "AuditPrintPage.png")
                bitmap.Save(path, Imaging.ImageFormat.Png)
                Check(File.Exists(path), "rendered AuditPrintPage.png")
            End If
        End Using
    End Sub

    Private Sub Reject(Of T As Exception)(work As Action, name As String)
        Try
            work()
        Catch ex As T
            Check(True, name)
            Return
        End Try
        Throw New Exception("Expected rejection: " & name)
    End Sub

    Private Function Caps() As AuditReadCapabilities
        Return New AuditReadCapabilities With {.HasV2 = True, .TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Libya Standard Time")}
    End Function

    Private Sub UnitTests()
        Using cmd = AuditCompatibilityReader.BuildCommand(New AuditSearchFilter With {.RecordKind = "V2"}, Caps(), New AuditReadBounds(), 0)
            Check(Not cmd.CommandText.Contains("Network_Edit_Tracker") AndAlso Not cmd.CommandText.Contains("Operation_Types"), "V2 search has no legacy table dependencies")
            Check(cmd.CommandText.Contains("Network_Audit_Change") AndAlso cmd.CommandText.Contains("dbo.IM_Menu") AndAlso
                  cmd.CommandText.Contains("dbo.Agents") AndAlso cmd.CommandText.Contains("dbo.Stores") AndAlso
                  cmd.CommandText.Contains("dbo.TreasuryCard"), "V2 text search includes saved and current human entity names")
            Check(CBool(cmd.Parameters("@SearchEmpty").Value), "empty text search bypasses entity-name joins")
        End Using
        Dim strictReader As New AuditCompatibilityReader("", True)
        Reject(Of InvalidOperationException)(Sub() strictReader.ReadPage(New AuditSearchFilter(), Caps(), New AuditReadBounds(), 0), "primary reader refuses mixed search before connecting")
        Reject(Of InvalidOperationException)(Sub() strictReader.ReadDetails("Legacy", 1, Guid.Empty), "primary reader refuses old details before connecting")
        Using form As New AuditBrowserPreview()
            Check(form.KindCombo.Items.Count = 1 AndAlso Not form.KindCombo.Enabled, "primary screen exposes only the new log")
            Check(form.ScreenCombo.Items.Count = 1 AndAlso CStr(form.ScreenCombo.Items(0)) = "كل الشاشات", "screen combo starts with all screens")
            Check(form.UserCombo.Items.Count = 1 AndAlso CStr(form.UserCombo.Items(0)) = "كل المستخدمين", "user combo starts with all users")
            Check(form.ResultsPrintPreviewDialog.Document Is form.ResultsPrintDocument AndAlso form.PrintButton.Text.Contains("طباعة"), "print preview is wired to PrintDocument")
        End Using
        Reject(Of UnauthorizedAccessException)(Sub()
                                                  Dim denied As New AuditCompatibilityReader("", False)
                                              End Sub, "reader requires permission")
        Check(AuditCompatibilityReader.EscapeLike("x%_[~") = "x~%~_~[~~", "LIKE metacharacters escaped")
        Dim f As New AuditSearchFilter With {.FromDate = New DateTime(2026, 10, 1), .ToDate = New DateTime(2026, 10, 24), .UserId = 7, .ScreenText = "POS", .SearchText = "'; DROP TABLE x; -- %"}
        Using cmd = AuditCompatibilityReader.BuildCommand(f, Caps(), New AuditReadBounds(), 2)
            Check(Not cmd.CommandText.Contains(f.SearchText), "search text is never SQL code")
            Check(CInt(cmd.Parameters("@Skip").Value) = 200, "page offset typed")
            Check(cmd.Parameters("@Notes").SqlDbType = SqlDbType.NVarChar, "Unicode search typed")
            Check(Not CBool(cmd.Parameters("@SearchEmpty").Value), "nonempty text search enables entity-name matching")
            Check(CStr(cmd.Parameters("@Screen").Value) = "POS" AndAlso cmd.CommandText.Contains("e.ScreenCode=@Screen"), "screen combo uses exact typed ScreenCode")
            Check(CInt(cmd.Parameters("@UserId").Value) = 7 AndAlso cmd.CommandText.Contains("e.ActorUserId=@UserId"), "user combo uses exact typed ActorUserId")
            Check(cmd.CommandText.Contains("ORDER BY EventTime ASC,SequenceId ASC") AndAlso Not cmd.CommandText.Contains("EventTime DESC"), "events ordered oldest to newest")
            Check(CDate(cmd.Parameters("@ToUtc").Value) = New DateTime(2026, 10, 24, 22, 0, 0), "exclusive day end converted to UTC")
        End Using
        Using cmd = AuditCompatibilityReader.BuildScreenCodesCommand(New AuditReadBounds With {.V2Sequence = 45})
            Check(cmd.CommandText.Contains("Network_Audit_Event") AndAlso Not cmd.CommandText.Contains("Network_Edit_Tracker"), "screen list comes from V2 events only")
            Check(CLng(cmd.Parameters("@V2Bound").Value) = 45, "screen list respects captured high-water mark")
        End Using
        Using cmd = AuditCompatibilityReader.BuildUsersCommand(New AuditReadBounds With {.V2Sequence = 45})
            Check(cmd.CommandText.Contains("dbo.Users") AndAlso cmd.CommandText.Contains("Network_Audit_Event"), "user list includes current and historical event users")
            Check(cmd.CommandText.Contains("ROW_NUMBER()") AndAlso CLng(cmd.Parameters("@V2Bound").Value) = 45, "user list keeps the latest captured name within the high-water mark")
        End Using
        Reject(Of ArgumentException)(Sub() AuditCompatibilityReader.Validate(New AuditSearchFilter With {.FromDate = Date.Today, .ToDate = Date.Today.AddDays(-1)}, 0), "reversed dates rejected")
        Reject(Of ArgumentException)(Sub() AuditCompatibilityReader.Validate(New AuditSearchFilter With {.RecordKind = "bad"}, 0), "unknown record kind rejected")
        Reject(Of ArgumentException)(Sub() AuditCompatibilityReader.Validate(New AuditSearchFilter With {.SearchText = New String("x"c, 201)}, 0), "oversized text rejected")
        Reject(Of ArgumentOutOfRangeException)(Sub() AuditCompatibilityReader.Validate(New AuditSearchFilter(), -1), "negative page rejected")
        Reject(Of InvalidOperationException)(Sub()
                                                Using cmd = AuditCompatibilityReader.BuildCommand(New AuditSearchFilter With {.RecordKind = "V2"}, New AuditReadCapabilities(), New AuditReadBounds(), 0)
                                                End Using
                                            End Sub, "V2 unavailable is explicit")
        Dim states As New HashSet(Of String)()
        For i As Integer = 0 To 3
            states.Add(AuditCompatibilityReader.StateValue(i, If(i = 3, CType("0", Object), DBNull.Value)))
        Next
        Check(states.Count = 4, "uncaptured/absent/NULL/zero remain distinct")
        Check(AuditCompatibilityReader.OutcomeLabel(DBNull.Value) <> AuditCompatibilityReader.OutcomeLabel(1), "legacy outcome not invented as success")
        Using cmd = AuditCompatibilityReader.BuildDetailsCommand("V2", 0, Guid.NewGuid())
            Check(Not cmd.CommandText.Contains("CanonicalEvent"), "canonical payload omitted from UI details")
        End Using
    End Sub

    Private Function ReadConnection(path As String) As String
        Dim connection As String = Nothing
        For Each line As String In File.ReadAllLines(path)
            If line.StartsWith("SqlConStr:", StringComparison.Ordinal) Then connection = line.Substring("SqlConStr:".Length)
        Next
        If String.IsNullOrWhiteSpace(connection) Then Throw New InvalidOperationException("Settings connection not found.")
        Dim builder As New SqlConnectionStringBuilder(connection)
        builder.InitialCatalog = "CPOS_ACCOUNTING"
        builder.ApplicationName = "CPOS Audit Phase2A Tests"
        builder.ConnectTimeout = 10
        Return builder.ConnectionString
    End Function

    Private Sub FailedRefreshTest()
        ' An empty connection fails immediately: no external server or network is contacted.
        MY_Settings.SqlConStr = ""
        Using form As New AuditBrowserPreview()
            Dim previous As New DataTable()
            previous.Columns.Add("Notes")
            previous.Rows.Add("Previous successful result")
            form.EventsGrid.DataSource = previous
            Dim method = GetType(Network_Audit_Browser).GetMethod("LoadPageAsync", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
            Dim task = DirectCast(method.Invoke(form, New Object() {True, 0}), Threading.Tasks.Task)
            Dim elapsed = Diagnostics.Stopwatch.StartNew()
            While Not task.IsCompleted AndAlso elapsed.Elapsed.TotalSeconds < 10
                Application.DoEvents()
                Threading.Thread.Sleep(10)
            End While
            Check(task.IsCompleted, "failed refresh finishes without a retry loop")
            task.GetAwaiter().GetResult()
            Check(Object.ReferenceEquals(form.EventsGrid.DataSource, previous), "failed refresh preserves previous results")
            Check(form.FiltersLayout.Enabled AndAlso form.CloseButton.Enabled, "failed refresh restores UI controls")
        End Using
    End Sub

    Private Function Scalar(cn As SqlConnection, sql As String, Optional tx As SqlTransaction = Nothing) As Long
        Using cmd As New SqlCommand(sql, cn, tx)
            Return Convert.ToInt64(cmd.ExecuteScalar())
        End Using
    End Function

    Private Sub LiveTests(connection As String, writeTests As Boolean)
        Dim reader As New AuditCompatibilityReader(connection, True, True)
        Dim cap = reader.GetCapabilities()
        Check(cap.HasV2 AndAlso cap.SourceId <> Guid.Empty, "installed schema/source detected")
        Dim bound = reader.CaptureBounds(cap)
        Dim f As New AuditSearchFilter With {.FromDate = New DateTime(1753, 1, 2), .ToDate = New DateTime(9998, 12, 31), .RecordKind = "Legacy"}
        Dim page = reader.ReadPage(f, cap, bound, 0)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(cn.Database = "CPOS_ACCOUNTING", "approved test database only")
            Dim domainsBefore = Scalar(cn, "SELECT EnabledDomainsXml.value('count(/Domains/*)','int') FROM dbo.Network_Audit_Source WHERE ConfigId=1")
            Dim strictReader As New AuditCompatibilityReader(connection, True, False)
            Dim strictBounds = strictReader.CaptureBounds(cap)
            Check(strictBounds.LegacyId = 0, "primary reader captures only V2 bounds")
            Dim userValues = strictReader.ReadUsers(cap, strictBounds)
            Check(userValues.Rows.Count > 0 AndAlso userValues.Rows.Cast(Of DataRow)().All(Function(x) CInt(x("UserId")) > 0), "user combo loads current users even when the audit log is empty")
            Check(userValues.Rows.Cast(Of DataRow)().Select(Function(x) CInt(x("UserId"))).Distinct().Count() = userValues.Rows.Count, "user combo contains one row per user id")
            Dim strictFilter As New AuditSearchFilter With {.FromDate = f.FromDate, .ToDate = f.ToDate, .RecordKind = "V2"}
            Check(strictReader.ReadPage(strictFilter, cap, strictBounds, 0).Total = Scalar(cn, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Event"), "V2-only live query returns actual new events")
            Dim savedName As String
            Using nameCommand As New SqlCommand("SELECT TOP(1) LTRIM(RTRIM(CASE WHEN c.AfterState=3 THEN c.AfterValue WHEN c.BeforeState=3 THEN c.BeforeValue END)) FROM dbo.Network_Audit_Change c WHERE (c.FieldCode LIKE N'%Name%' OR REPLACE(c.FieldCode,N'_',N'') LIKE N'%ItemName%') AND NULLIF(LTRIM(RTRIM(CASE WHEN c.AfterState=3 THEN c.AfterValue WHEN c.BeforeState=3 THEN c.BeforeValue END)),N'') IS NOT NULL ORDER BY c.EventId,c.ChangeNo", cn)
                savedName = Convert.ToString(nameCommand.ExecuteScalar()).Trim()
            End Using
            If savedName.Length > 0 Then
                Check(True, "installed audit data contains at least one saved display name")
                Dim nameFilter As New AuditSearchFilter With {.FromDate = f.FromDate, .ToDate = f.ToDate, .RecordKind = "V2", .SearchText = savedName.Substring(0, Math.Min(100, savedName.Length))}
                Dim namePage = strictReader.ReadPage(nameFilter, cap, strictBounds, 0)
                Dim displayedName As Boolean
                For Each nameRow As DataRow In namePage.Rows.Rows
                    If Convert.ToString(nameRow("Notes")).IndexOf(savedName, StringComparison.OrdinalIgnoreCase) >= 0 Then displayedName = True
                Next
                Check(namePage.Total > 0 AndAlso displayedName, "saved item/customer name is searchable and displayed in the human description")
            Else
                Check(strictBounds.V2Sequence = 0, "clean audit log does not require historical display-name fixtures")
            End If
            Dim legacyBefore As Long = Scalar(cn, "SELECT COUNT_BIG(*) FROM dbo.Network_Edit_Tracker")
            Check(page.Total = Scalar(cn, "SELECT COUNT_BIG(*) FROM dbo.Network_Edit_Tracker WHERE [Date] >= '17530102' AND [Date] < '99990101'"), "legacy count includes unclassified records")
            Dim keys As New HashSet(Of Long)()
            For i As Integer = 0 To CInt(Math.Ceiling(page.Total / 100.0)) - 1
                Dim p = reader.ReadPage(f, cap, bound, i)
                For Each row As DataRow In p.Rows.Rows
                    If Not keys.Add(CLng(row("SequenceId"))) Then Throw New Exception("Duplicate legacy event across pages")
                Next
            Next
            Check(keys.Count = page.Total, "all legacy pages are reachable")
            If page.Rows.Rows.Count > 0 Then
                Dim details = reader.ReadDetails("Legacy", CLng(page.Rows.Rows(0)("SequenceId")), Guid.Empty)
                Check(details.Tables(0).Rows.Count = 1, "legacy details load")
            End If
            f.Outcome = 1
            Check(reader.ReadPage(f, cap, bound, 0).Total = 0, "outcome filter excludes unknown legacy outcome")
            f.Outcome = 0
            f.RecordKind = "All"
            Check(reader.ReadPage(f, cap, bound, 0).Total >= page.Total, "combined read succeeds")
            If writeTests Then RollbackFixture(cn, cap, bound)
            Check(Scalar(cn, "SELECT COUNT_BIG(*) FROM dbo.Network_Edit_Tracker") = legacyBefore, "legacy count unchanged")
            Check(Scalar(cn, "SELECT EnabledDomainsXml.value('count(/Domains/*)','int') FROM dbo.Network_Audit_Source WHERE ConfigId=1") = domainsBefore, "reader leaves enabled domains unchanged")
        End Using
    End Sub

    Private Function Query(cmd As SqlCommand, cn As SqlConnection, tx As SqlTransaction) As DataSet
        Using cmd
            cmd.Connection = cn
            cmd.Transaction = tx
            Using adapter As New SqlDataAdapter(cmd)
                Dim ds As New DataSet()
                adapter.Fill(ds)
                Return ds
            End Using
        End Using
    End Function

    Private Sub RollbackFixture(cn As SqlConnection, cap As AuditReadCapabilities, beforeBounds As AuditReadBounds)
        Dim countsBefore As Long = Scalar(cn, "SELECT (SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Request)+(SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Event)+(SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Change)")
        Dim request As Guid = Guid.NewGuid()
        Dim eventId As Guid = Guid.NewGuid()
        Dim marker As String = "Test_" & request.ToString("N")
        Using tx As SqlTransaction = cn.BeginTransaction()
            Try
                Using cmd As New SqlCommand("SET XACT_ABORT OFF;
INSERT dbo.Network_Audit_Request(SourceId,RequestId,ContractVersion,OperationCode,ActorKind,ActorServiceCode,RequestFingerprint,CanonicalRequest,State)
VALUES(@Source,@Request,1,'Test.Reader',3,'AuditTests',CONVERT(binary(32),0x01),0x01,0);
DECLARE @i int=1;
WHILE @i<=205 BEGIN
 INSERT dbo.Network_Audit_Event(EventId,SourceId,RequestId,EventOrdinal,ContractVersion,EventCode,DomainCode,ScreenCode,EventKind,Outcome,ActorKind,ActorServiceCode,MachineName,ApplicationVersion,OccurredAtUtc,TemplateCode,TemplateVersion,Notes,EventFingerprint,CanonicalEvent,DocumentNumber)
 VALUES(CASE WHEN @i=1 THEN @Event ELSE NEWID() END,@Source,@Request,@i,1,'Test.Reader','Test','Test',1,CASE WHEN @i%2=0 THEN 2 ELSE 1 END,3,'AuditTests','TestOnly','2A',@Utc,'Test',1,@Marker+N' %_[~',CONVERT(binary(32),0x01),0x01,@Marker);
 SET @i=@i+1;
END;
 INSERT dbo.Network_Audit_Change(EventId,ChangeNo,EntityType,EntityId,FieldCode,ValueType,SourceSqlType,BeforeState,BeforeValue,AfterState,AfterValue,ValueOrigin)
 VALUES(@Event,1,'Test','1','Amount','Decimal','decimal(18,3)',2,NULL,3,N'0.000',1),
 (@Event,2,'Test','1','Name','String','nvarchar(50)',1,NULL,3,N'اسم صنف محفوظ للاختبار',2),
 (@Event,3,'Test','1','EmptyName','String','nvarchar(50)',1,NULL,3,N'',2),
 (@Event,4,'Test','1','Previous','String','nvarchar(50)',0,NULL,2,NULL,1);
UPDATE dbo.Network_Audit_Request SET State=1,ResultCode=1,ResultXml=N'<Result />',CompletedAtUtc=SYSUTCDATETIME() WHERE SourceId=@Source AND RequestId=@Request;", cn, tx)
                    cmd.Parameters.Add("@Source", SqlDbType.UniqueIdentifier).Value = cap.SourceId
                    cmd.Parameters.Add("@Request", SqlDbType.UniqueIdentifier).Value = request
                    cmd.Parameters.Add("@Event", SqlDbType.UniqueIdentifier).Value = eventId
                    cmd.Parameters.Add("@Marker", SqlDbType.NVarChar, 50).Value = marker
                    cmd.Parameters.Add("@Utc", SqlDbType.DateTime2).Value = DateTime.UtcNow
                    cmd.ExecuteNonQuery()
                End Using
                Dim localToday As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cap.TimeZone).Date
                Dim f As New AuditSearchFilter With {.FromDate = localToday.AddDays(-1), .ToDate = localToday.AddDays(1), .RecordKind = "V2", .DocumentNumber = marker, .SearchText = "%_[~"}
                Dim bound As New AuditReadBounds With {.LegacyId = beforeBounds.LegacyId, .V2Sequence = Long.MaxValue}
                Dim ids As New HashSet(Of Guid)()
                For i As Integer = 0 To 2
                    Dim ds = Query(AuditCompatibilityReader.BuildCommand(f, cap, bound, i), cn, tx)
                    Check(CLng(ds.Tables(0).Rows(0)(0)) = 205, "fixture filtered count page " & i)
                    Check(ds.Tables(1).Rows.Count = If(i < 2, 100, 5), "fixture page size " & i)
                    For Each row As DataRow In ds.Tables(1).Rows
                        If Not ids.Add(CType(row("EventId"), Guid)) Then Throw New Exception("Duplicate fixture event across pages")
                    Next
                Next
                Check(ids.Count = 205, "V2 stable order covers every fixture event")
                Check(CLng(Query(AuditCompatibilityReader.BuildCommand(f, cap, beforeBounds, 0), cn, tx).Tables(0).Rows(0)(0)) = 0, "captured high-water excludes later events")
                f.Outcome = 2
                Check(CLng(Query(AuditCompatibilityReader.BuildCommand(f, cap, bound, 0), cn, tx).Tables(0).Rows(0)(0)) = 102, "V2 outcome filter")
                f.Outcome = 0
                f.SearchText = "اسم صنف محفوظ للاختبار"
                Check(CLng(Query(AuditCompatibilityReader.BuildCommand(f, cap, bound, 0), cn, tx).Tables(0).Rows(0)(0)) = 1, "saved descriptive change is searchable even when Notes contains only identifiers")
                f.SearchText = "not found " & marker
                Check(CLng(Query(AuditCompatibilityReader.BuildCommand(f, cap, bound, 0), cn, tx).Tables(0).Rows(0)(0)) = 0, "no match returns zero")
                Dim details = Query(AuditCompatibilityReader.BuildDetailsCommand("V2", 0, eventId), cn, tx)
                Check(details.Tables(0).Rows.Count = 1 AndAlso details.Tables(1).Rows.Count = 4, "V2 event and changes load")
                Check(CStr(details.Tables(1).Rows(0)("AfterValue")) = "0.000", "zero value preserved")
                Check(CStr(details.Tables(1).Rows(2)("AfterValue")) = "", "empty string preserved separately from NULL")
                Reject(Of SqlException)(Sub()
                                            Using cmd As New SqlCommand("UPDATE dbo.Network_Audit_Change SET BeforeValue=N'bad' WHERE EventId=@Event AND ChangeNo=1", cn, tx)
                                                cmd.Parameters.Add("@Event", SqlDbType.UniqueIdentifier).Value = eventId
                                                cmd.ExecuteNonQuery()
                                            End Using
                                        End Sub, "invalid state/value pair rejected by schema")
                Reject(Of SqlException)(Sub()
                                            Using cmd As New SqlCommand("UPDATE dbo.Network_Audit_Event SET EventOrdinal=1 WHERE RequestId=@Request AND EventOrdinal=2", cn, tx)
                                                cmd.Parameters.Add("@Request", SqlDbType.UniqueIdentifier).Value = request
                                                cmd.ExecuteNonQuery()
                                            End Using
                                        End Sub, "duplicate request event ordinal rejected")
            Finally
                tx.Rollback()
            End Try
        End Using
        Check(Scalar(cn, "SELECT (SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Request)+(SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Event)+(SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Change)") = countsBefore, "all fixture writes rolled back")
    End Sub

    Private Sub RenderScreens(output As String)
        Application.EnableVisualStyles()
        Using form As New AuditBrowserPreview()
            form.BasisLabel.Text = "V2 بتوقيت Libya Standard Time. الترتيب من الأقدم إلى الأحدث."
            form.PageLabel.Text = "الصفحة 1 / 5 | 462 حدث"
            Dim table As New DataTable()
            For Each col As DataGridViewColumn In form.EventsGrid.Columns
                table.Columns.Add(col.DataPropertyName, If(col.DataPropertyName = "EventTime", GetType(DateTime), GetType(String)))
            Next
            table.Rows.Add("قديم", Date.Today, "المدير", "المبيعات", "تعديل", "غير موثقة (قديم)", "125", "POS-01", "تعديل كمية الصنف في الفاتورة", "قديم: المنطقة الزمنية غير موثقة")
            form.EventsGrid.DataSource = table
            SaveForm(form, Path.Combine(output, "AuditBrowser_1024.png"), New Size(1024, 650))
            SaveForm(form, Path.Combine(output, "AuditBrowser_Min.png"), New Size(980, 600))
        End Using
        Using form As New AuditDetailsPreview()
            Dim data As New DataSet()
            Dim meta = data.Tables.Add()
            meta.Columns.Add("Notes")
            meta.Columns.Add("EventId", GetType(Guid))
            meta.Columns.Add("ActorName")
            meta.Columns.Add("OccurredAtUtc", GetType(DateTime))
            meta.Columns.Add("RelatedEntitiesXml")
            meta.Rows.Add("مثال تصميم فقط: تعديل الكمية من 2 إلى 3.", Guid.NewGuid(), "المدير", DateTime.UtcNow, "<Entities />")
            Dim changes = data.Tables.Add()
            For Each name As String In {"EntityType", "EntityId", "FieldCode", "ValueType", "SourceSqlType", "BeforeState", "BeforeValue", "AfterState", "AfterValue", "ValueOrigin"}
                changes.Columns.Add(name)
            Next
            changes.Rows.Add("InvoiceLine", "123", "Quantity", "Decimal", "decimal(18,3)", "3", "2.000", "3", "3.000", "1")
            form.SetDetails("V2", data, Caps())
            SaveForm(form, Path.Combine(output, "AuditDetails.png"), New Size(960, 620))
            form.DetailsTabs.SelectedTab = form.ChangesTab
            SaveForm(form, Path.Combine(output, "AuditChanges.png"), New Size(960, 620))
        End Using
    End Sub

    Private Sub SaveForm(form As Form, path As String, size As Size)
        form.ClientSize = size
        form.StartPosition = FormStartPosition.Manual
        form.Location = New Point(-30000, -30000)
        form.ShowInTaskbar = False
        form.Show()
        Application.DoEvents()
        form.PerformLayout()
        Using bitmap As New Bitmap(form.Width, form.Height)
            form.DrawToBitmap(bitmap, New Rectangle(Point.Empty, bitmap.Size))
            bitmap.Save(path, Imaging.ImageFormat.Png)
        End Using
        form.Hide()
        Check(File.Exists(path), "rendered " & System.IO.Path.GetFileName(path))
    End Sub
End Module
