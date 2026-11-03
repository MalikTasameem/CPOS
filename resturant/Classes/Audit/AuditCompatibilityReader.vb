Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text

Friend Class AuditSearchFilter
    Public Property FromDate As DateTime = Date.Today.AddDays(-7)
    Public Property ToDate As DateTime = Date.Today
    Public Property RecordKind As String = "All"
    Public Property Outcome As Integer
    Public Property UserId As Integer
    Public Property DocumentNumber As String = ""
    Public Property ScreenText As String = ""
    Public Property SearchText As String = ""
End Class

Friend Class AuditReadCapabilities
    Public Property HasV2 As Boolean
    Public Property TimeZone As TimeZoneInfo
    Public Property SourceId As Guid
End Class

Friend Class AuditReadBounds
    Public Property LegacyId As Long
    Public Property V2Sequence As Long
End Class

Friend Class AuditReadPage
    Public Property Rows As DataTable
    Public Property Total As Long
End Class

' Read-only. No business writes, no legacy backfill, no draft/offline integration.
Friend NotInheritable Class AuditCompatibilityReader
    Friend Const PageSize As Integer = 100
    Private ReadOnly connectionString As String
    Private ReadOnly allowLegacy As Boolean

    Friend Sub New(sqlConnectionString As String, canRead As Boolean, Optional includeLegacy As Boolean = False)
        If Not canRead Then Throw New UnauthorizedAccessException("عرض المراقبة الجديدة متاح للمدير فقط في هذه الدفعة.")
        connectionString = sqlConnectionString
        allowLegacy = includeLegacy
    End Sub

    Friend Function GetCapabilities() As AuditReadCapabilities
        Using cn As New SqlConnection(connectionString)
            cn.Open()
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM sys.tables WHERE schema_id=SCHEMA_ID(N'dbo') AND name IN (N'Network_Audit_Source',N'Network_Audit_Request',N'Network_Audit_Event',N'Network_Audit_Change')", cn)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count = 0 Then
                    If Not allowLegacy Then Throw New InvalidOperationException("بنية المراقبة الجديدة غير مثبتة؛ لا يوجد رجوع إلى السجل القديم.")
                    Return New AuditReadCapabilities With {.HasV2 = False}
                End If
                If count <> 4 Then Throw New InvalidOperationException("بنية المراقبة الجديدة غير مكتملة؛ لا يمكن عرض نتيجة موحدة موثوقة.")
            End Using
            Using cmd As New SqlCommand("SELECT SourceId,TimeZoneId,SchemaVersion FROM dbo.Network_Audit_Source WHERE ConfigId=1", cn)
                Using rd As SqlDataReader = cmd.ExecuteReader()
                    If Not rd.Read() Then Throw New InvalidOperationException("إعداد مصدر المراقبة الجديدة غير موجود.")
                    If Convert.ToInt32(rd("SchemaVersion")) <> 1 Then Throw New InvalidOperationException("إصدار بنية المراقبة غير مدعوم.")
                    Return New AuditReadCapabilities With {
                        .HasV2 = True, .SourceId = CType(rd("SourceId"), Guid),
                        .TimeZone = TimeZoneInfo.FindSystemTimeZoneById(Convert.ToString(rd("TimeZoneId")))}
                End Using
            End Using
        End Using
    End Function

    Friend Function CaptureBounds(capabilities As AuditReadCapabilities) As AuditReadBounds
        If Not allowLegacy Then
            If Not capabilities.HasV2 Then Throw New InvalidOperationException("بنية المراقبة الجديدة مطلوبة.")
            Using cn As New SqlConnection(connectionString), cmd As New SqlCommand("SELECT ISNULL(MAX(LocalSequence),CONVERT(bigint,0)) FROM dbo.Network_Audit_Event", cn)
                cn.Open()
                Return New AuditReadBounds With {.V2Sequence = CLng(cmd.ExecuteScalar())}
            End Using
        End If
        Dim query As String = "SELECT ISNULL(MAX(CONVERT(bigint,T_ID)),0) FROM dbo.Network_Edit_Tracker;"
        If capabilities.HasV2 Then query &= " SELECT ISNULL(MAX(LocalSequence),0) FROM dbo.Network_Audit_Event;"
        Using cn As New SqlConnection(connectionString), cmd As New SqlCommand(query, cn)
            cn.Open()
            Using rd As SqlDataReader = cmd.ExecuteReader()
                rd.Read()
                Dim bounds As New AuditReadBounds With {.LegacyId = rd.GetInt64(0)}
                If capabilities.HasV2 Then
                    rd.NextResult()
                    rd.Read()
                    bounds.V2Sequence = rd.GetInt64(0)
                End If
                Return bounds
            End Using
        End Using
    End Function

    Friend Shared Function EscapeLike(value As String) As String
        Return value.Replace("~", "~~").Replace("%", "~%").Replace("_", "~_").Replace("[", "~[")
    End Function

    Friend Shared Sub Validate(filter As AuditSearchFilter, pageIndex As Integer)
        If filter Is Nothing Then Throw New ArgumentNullException("filter")
        If filter.FromDate.Date > filter.ToDate.Date Then Throw New ArgumentException("تاريخ البداية يجب ألا يتجاوز تاريخ النهاية.")
        If filter.FromDate.Year < 1753 OrElse filter.ToDate.Date >= DateTime.MaxValue.Date Then Throw New ArgumentException("الفترة خارج نطاق التاريخ المدعوم.")
        If pageIndex < 0 OrElse pageIndex > Integer.MaxValue \ PageSize Then Throw New ArgumentOutOfRangeException("pageIndex")
        If filter.RecordKind <> "All" AndAlso filter.RecordKind <> "Legacy" AndAlso filter.RecordKind <> "V2" Then Throw New ArgumentException("نوع سجل غير معروف.")
        If filter.Outcome < 0 OrElse filter.Outcome > 4 OrElse filter.UserId < 0 Then Throw New ArgumentException("قيمة مرشح غير صحيحة.")
        If If(filter.DocumentNumber, "").Length > 50 OrElse If(filter.ScreenText, "").Length > 64 OrElse If(filter.SearchText, "").Length > 200 Then
            Throw New ArgumentException("تجاوز نص البحث الطول المسموح.")
        End If
    End Sub

    Friend Shared Function GetUtcBoundary(localDate As DateTime, zone As TimeZoneInfo) As DateTime
        Dim unspecified As DateTime = DateTime.SpecifyKind(localDate.Date, DateTimeKind.Unspecified)
        If zone.IsInvalidTime(unspecified) OrElse zone.IsAmbiguousTime(unspecified) Then
            Throw New ArgumentException("حد اليوم غير محدد زمنيًا في المنطقة المختارة؛ اختر فترة لا تتضمن هذا الحد.")
        End If
        Return TimeZoneInfo.ConvertTimeToUtc(unspecified, zone)
    End Function

    Friend Shared Function BuildCommand(filter As AuditSearchFilter, capabilities As AuditReadCapabilities,
                                       bounds As AuditReadBounds, pageIndex As Integer) As SqlCommand
        Validate(filter, pageIndex)
        If filter.RecordKind = "V2" AndAlso Not capabilities.HasV2 Then Throw New InvalidOperationException("بنية V2 لم تثبت على هذه القاعدة بعد.")
        Dim cmd As New SqlCommand With {.CommandTimeout = 60}
        cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = filter.FromDate.Date
        cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = filter.ToDate.Date.AddDays(1)
        cmd.Parameters.Add("@LegacyBound", SqlDbType.BigInt).Value = bounds.LegacyId
        cmd.Parameters.Add("@Kind", SqlDbType.VarChar, 6).Value = filter.RecordKind
        cmd.Parameters.Add("@Outcome", SqlDbType.Int).Value = filter.Outcome
        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = filter.UserId
        cmd.Parameters.Add("@Document", SqlDbType.NVarChar, 50).Value = If(filter.DocumentNumber, "").Trim()
        cmd.Parameters.Add("@Screen", SqlDbType.NVarChar, 64).Value = If(filter.ScreenText, "").Trim()
        Dim searchText = If(filter.SearchText, "").Trim()
        cmd.Parameters.Add("@SearchEmpty", SqlDbType.Bit).Value = searchText.Length = 0
        cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 402).Value = "%" & EscapeLike(searchText) & "%"
        cmd.Parameters.Add("@Skip", SqlDbType.Int).Value = pageIndex * PageSize
        cmd.Parameters.Add("@Take", SqlDbType.Int).Value = PageSize
        Dim sql As New StringBuilder()
        ' The two clocks are deliberately NOT merged into a claimed global chronology.
        sql.AppendLine("SELECT * INTO #AuditMatches FROM (")
        If filter.RecordKind <> "V2" Then
        sql.AppendLine("SELECT CAST('Legacy' AS varchar(6)) RecordKind, CONVERT(bigint,n.T_ID) SequenceId,")
        sql.AppendLine("CAST(NULL AS uniqueidentifier) EventId, CAST(NULL AS uniqueidentifier) SourceId,")
        sql.AppendLine("CONVERT(datetime2(3),n.[Date]) EventTime, n.User_ID ActorUserId,")
        sql.AppendLine("COALESCE(NULLIF(u.UserName,N''),N'مستخدم غير معرّف') ActorName,")
        sql.AppendLine("COALESCE(NULLIF(s.Type_Name,N''),N'تصنيف غير معرّف ('+ISNULL(CONVERT(nvarchar(20),n.Screen_Type),N'NULL')+N')') ScreenLabel,")
        sql.AppendLine("COALESCE(NULLIF(o.Operation_Name,N''),N'عملية غير معرّفة') ActionLabel,")
        sql.AppendLine("CAST(NULL AS tinyint) Outcome, n.Bill_ID DocumentNumber,n.CP_Name MachineName,n.Notes")
        sql.AppendLine("FROM dbo.Network_Edit_Tracker n LEFT JOIN dbo.Users u ON u.User_ID=n.User_ID")
        sql.AppendLine("LEFT JOIN dbo.AgentBalance_Type s ON s.ID=n.Screen_Type LEFT JOIN dbo.Operation_Types o ON o.T_ID=n.Operation_ID")
        sql.AppendLine("WHERE @Kind IN ('All','Legacy') AND @Outcome=0 AND n.T_ID<=@LegacyBound AND n.[Date]>=@From AND n.[Date]<@To")
        sql.AppendLine("AND (@UserId=0 OR n.User_ID=@UserId) AND (@Document=N'' OR n.Bill_ID=@Document)")
        sql.AppendLine("AND (@Screen=N'' OR ISNULL(s.Type_Name,N'')=@Screen) AND (@SearchEmpty=1 OR ISNULL(n.Notes,N'') LIKE @Notes ESCAPE '~')")
        End If
        If capabilities.HasV2 Then
            If capabilities.TimeZone Is Nothing Then Throw New InvalidOperationException("منطقة وقت العرض غير معرفة.")
            cmd.Parameters.Add("@FromUtc", SqlDbType.DateTime2).Value = GetUtcBoundary(filter.FromDate, capabilities.TimeZone)
            cmd.Parameters.Add("@ToUtc", SqlDbType.DateTime2).Value = GetUtcBoundary(filter.ToDate.AddDays(1), capabilities.TimeZone)
            cmd.Parameters.Add("@V2Bound", SqlDbType.BigInt).Value = bounds.V2Sequence
            If filter.RecordKind <> "V2" Then sql.AppendLine("UNION ALL")
            sql.AppendLine("SELECT CAST('V2' AS varchar(6)) RecordKind,e.LocalSequence SequenceId,e.EventId,e.SourceId,e.OccurredAtUtc EventTime,e.ActorUserId,")
            sql.AppendLine("COALESCE(e.ActorName,e.ActorServiceCode,N'غير معروف') ActorName,CONVERT(nvarchar(256),e.ScreenCode) ScreenLabel,CONVERT(nvarchar(256),e.EventCode) ActionLabel,")
            sql.AppendLine("e.Outcome,e.DocumentNumber,e.MachineName,e.Notes FROM dbo.Network_Audit_Event e")
            sql.AppendLine("WHERE @Kind IN ('All','V2') AND e.LocalSequence<=@V2Bound AND e.OccurredAtUtc>=@FromUtc AND e.OccurredAtUtc<@ToUtc")
            sql.AppendLine("AND (@Outcome=0 OR e.Outcome=@Outcome) AND (@UserId=0 OR e.ActorUserId=@UserId)")
            sql.AppendLine("AND (@Document=N'' OR e.DocumentNumber=@Document) AND (@Screen=N'' OR e.ScreenCode=@Screen)")
            sql.AppendLine("AND (@SearchEmpty=1 OR e.Notes LIKE @Notes ESCAPE '~'")
            sql.AppendLine(" OR EXISTS(SELECT 1 FROM dbo.Network_Audit_Change c WHERE c.EventId=e.EventId")
            sql.AppendLine("   AND (c.FieldCode LIKE N'%Name%' OR REPLACE(c.FieldCode,N'_',N'') LIKE N'%ItemName%')")
            sql.AppendLine("   AND (ISNULL(c.BeforeValue,N'') LIKE @Notes ESCAPE '~' OR ISNULL(c.AfterValue,N'') LIKE @Notes ESCAPE '~'))")
            sql.AppendLine(" OR EXISTS(SELECT 1 FROM dbo.Network_Audit_Change c JOIN dbo.IM_Menu m ON m.IM_ID=TRY_CONVERT(int,CASE WHEN c.AfterState=3 THEN c.AfterValue WHEN c.BeforeState=3 THEN c.BeforeValue END)")
            sql.AppendLine("   WHERE c.EventId=e.EventId AND ((RIGHT(c.FieldCode,6)=N'ItemId') OR (CHARINDEX(N'_IM_ID',c.FieldCode)>0 AND CHARINDEX(N'U_IM_ID',c.FieldCode)=0) OR c.FieldCode IN(N'ComponentId',N'RecipeComponentId',N'FormatComponentId',N'SplitBalanceItem')) AND ISNULL(m.item_name,N'') LIKE @Notes ESCAPE '~')")
            sql.AppendLine(" OR EXISTS(SELECT 1 FROM dbo.Network_Audit_Change c JOIN dbo.Agents a ON a.AG_ID=TRY_CONVERT(int,CASE WHEN c.AfterState=3 THEN c.AfterValue WHEN c.BeforeState=3 THEN c.BeforeValue END)")
            sql.AppendLine("   WHERE c.EventId=e.EventId AND (RIGHT(c.FieldCode,9)=N'AccountId' OR RIGHT(c.FieldCode,10)=N'EmployeeId' OR c.FieldCode IN(N'Rct_AG_ID',N'AccountAGIDFROM',N'AccountAGIDTO',N'SalaryAGID',N'SalaryAdjEmployeeId',N'InvDocument_AG_ID',N'UserAgent',N'CommissionAccountId')) AND ISNULL(a.Ag_name,N'') LIKE @Notes ESCAPE '~')")
            sql.AppendLine(" OR EXISTS(SELECT 1 FROM dbo.Network_Audit_Change c JOIN dbo.Stores s ON s.ST_ID=TRY_CONVERT(int,CASE WHEN c.AfterState=3 THEN c.AfterValue WHEN c.BeforeState=3 THEN c.BeforeValue END)")
            sql.AppendLine("   WHERE c.EventId=e.EventId AND (RIGHT(c.FieldCode,7)=N'StoreId' OR CHARINDEX(N'_ST_ID',c.FieldCode)>0 OR c.FieldCode=N'SplitBalanceStore') AND ISNULL(s.ST_Name,N'') LIKE @Notes ESCAPE '~')")
            sql.AppendLine(" OR EXISTS(SELECT 1 FROM dbo.Network_Audit_Change c JOIN dbo.TreasuryCard t ON t.Tr_ID=TRY_CONVERT(int,CASE WHEN c.AfterState=3 THEN c.AfterValue WHEN c.BeforeState=3 THEN c.BeforeValue END)")
            sql.AppendLine("   WHERE c.EventId=e.EventId AND (RIGHT(c.FieldCode,10)=N'TreasuryId' OR c.FieldCode IN(N'Rct_Tr_ID',N'RctTr_Tr_ID',N'CashTrID',N'UserTreasury')) AND ISNULL(t.Tr_Name,N'') LIKE @Notes ESCAPE '~'))")
        End If
        sql.AppendLine(") matches;")
        sql.AppendLine("SELECT COUNT_BIG(*) FROM #AuditMatches;")
        sql.AppendLine("SELECT * FROM #AuditMatches ORDER BY EventTime ASC,SequenceId ASC,RecordKind ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;")
        sql.AppendLine("DROP TABLE #AuditMatches;")
        cmd.CommandText = sql.ToString()
        Return cmd
    End Function

    Friend Shared Function BuildScreenCodesCommand(bounds As AuditReadBounds) As SqlCommand
        If bounds Is Nothing Then Throw New ArgumentNullException("bounds")
        Dim cmd As New SqlCommand With {
            .CommandTimeout = 60,
            .CommandText = "SELECT DISTINCT e.ScreenCode FROM dbo.Network_Audit_Event e " &
                           "WHERE e.LocalSequence<=@V2Bound AND e.ScreenCode IS NOT NULL " &
                           "AND LTRIM(RTRIM(e.ScreenCode))<>N'' ORDER BY e.ScreenCode;"}
        cmd.Parameters.Add("@V2Bound", SqlDbType.BigInt).Value = bounds.V2Sequence
        Return cmd
    End Function

    Friend Function ReadScreenCodes(capabilities As AuditReadCapabilities, bounds As AuditReadBounds) As List(Of String)
        If capabilities Is Nothing OrElse Not capabilities.HasV2 Then Throw New InvalidOperationException("بنية V2 مطلوبة لقراءة قائمة الشاشات.")
        Using cn As New SqlConnection(connectionString), cmd = BuildScreenCodesCommand(bounds)
            cmd.Connection = cn
            cn.Open()
            Using rd = cmd.ExecuteReader()
                Dim result As New List(Of String)()
                While rd.Read()
                    Dim value = Convert.ToString(rd(0), CultureInfo.InvariantCulture).Trim()
                    If value.Length > 0 AndAlso value.Length <= 64 Then result.Add(value)
                End While
                Return result
            End Using
        End Using
    End Function

    Friend Shared Function BuildUsersCommand(bounds As AuditReadBounds) As SqlCommand
        If bounds Is Nothing Then Throw New ArgumentNullException("bounds")
        Dim sql = ";WITH EventActors AS (" &
                  " SELECT e.ActorUserId UserId,e.ActorName,ROW_NUMBER() OVER(PARTITION BY e.ActorUserId ORDER BY e.LocalSequence DESC) rn" &
                  " FROM dbo.Network_Audit_Event e WHERE e.LocalSequence<=@V2Bound AND e.ActorUserId IS NOT NULL)," &
                  " ActorIds AS (SELECT u.User_ID UserId FROM dbo.Users u WHERE u.User_ID>0 UNION SELECT a.UserId FROM EventActors a)" &
                  " SELECT i.UserId,COALESCE(NULLIF(LTRIM(RTRIM(u.UserName)),N''),NULLIF(LTRIM(RTRIM(a.ActorName)),N''),N'مستخدم رقم '+CONVERT(nvarchar(20),i.UserId))" &
                  " +N' — '+CONVERT(nvarchar(20),i.UserId) DisplayName" &
                  " FROM ActorIds i LEFT JOIN dbo.Users u ON u.User_ID=i.UserId LEFT JOIN EventActors a ON a.UserId=i.UserId AND a.rn=1" &
                  " ORDER BY DisplayName,i.UserId;"
        Dim cmd As New SqlCommand(sql) With {.CommandTimeout = 60}
        cmd.Parameters.Add("@V2Bound", SqlDbType.BigInt).Value = bounds.V2Sequence
        Return cmd
    End Function

    Friend Function ReadUsers(capabilities As AuditReadCapabilities, bounds As AuditReadBounds) As DataTable
        If capabilities Is Nothing OrElse Not capabilities.HasV2 Then Throw New InvalidOperationException("بنية V2 مطلوبة لقراءة قائمة المستخدمين.")
        Using cn As New SqlConnection(connectionString), cmd = BuildUsersCommand(bounds)
            cmd.Connection = cn
            cn.Open()
            Dim result As New DataTable()
            Using reader = cmd.ExecuteReader()
                result.Load(reader)
            End Using
            Return result
        End Using
    End Function

    Friend Function ReadPage(filter As AuditSearchFilter, capabilities As AuditReadCapabilities,
                             bounds As AuditReadBounds, pageIndex As Integer) As AuditReadPage
        If Not allowLegacy AndAlso filter.RecordKind <> "V2" Then Throw New InvalidOperationException("هذه الشاشة تعرض السجل الجديد فقط.")
        Using cn As New SqlConnection(connectionString), cmd As SqlCommand = BuildCommand(filter, capabilities, bounds, pageIndex)
            cmd.Connection = cn
            cn.Open()
            Dim total As Long
            Dim table As New DataTable()
            Using rd As SqlDataReader = cmd.ExecuteReader()
                rd.Read()
                total = rd.GetInt64(0)
                rd.NextResult()
                table.Load(rd)
            End Using
            EnrichDescriptions(cn, table)
            table.Columns.Add("TimeBasis", GetType(String))
            table.Columns.Add("OutcomeLabel", GetType(String))
            table.Columns.Add("RecordLabel", GetType(String))
            For Each row As DataRow In table.Rows
                If CStr(row("RecordKind")) = "V2" Then
                    row("EventTime") = TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(CDate(row("EventTime")), DateTimeKind.Utc), capabilities.TimeZone)
                    row("TimeBasis") = capabilities.TimeZone.Id
                    row("RecordLabel") = "جديد V2"
                Else
                    row("TimeBasis") = "قديم: المنطقة الزمنية غير موثقة"
                    row("RecordLabel") = "قديم"
                End If
                row("OutcomeLabel") = OutcomeLabel(row("Outcome"))
            Next
            Return New AuditReadPage With {.Rows = table, .Total = total}
        End Using
    End Function

    Private Shared Sub EnrichDescriptions(cn As SqlConnection, table As DataTable)
        Dim rows As New Dictionary(Of Guid, DataRow)()
        For Each row As DataRow In table.Rows
            If Convert.ToString(row("RecordKind"), CultureInfo.InvariantCulture) = "V2" AndAlso Not IsDBNull(row("EventId")) Then
                rows(CType(row("EventId"), Guid)) = row
            End If
        Next
        If rows.Count = 0 Then Return

        Dim sql As New StringBuilder("SELECT ids.EventId,ci.DisplayName CapturedItemName,im.item_name CurrentItemName,")
        sql.AppendLine("ca.DisplayName CapturedAccountName,a.Ag_name CurrentAccountName,cs.DisplayName CapturedStoreName,s.ST_Name CurrentStoreName,ct.DisplayName CapturedTreasuryName,t.Tr_Name CurrentTreasuryName")
        sql.Append("FROM (VALUES ")
        Using cmd As New SqlCommand()
            cmd.Connection = cn
            Dim index As Integer
            For Each eventId As Guid In rows.Keys
                If index > 0 Then sql.Append(",")
                Dim parameterName = "@E" & index.ToString(CultureInfo.InvariantCulture)
                sql.Append("(").Append(parameterName).Append(")")
                cmd.Parameters.Add(parameterName, SqlDbType.UniqueIdentifier).Value = eventId
                index += 1
            Next
            sql.AppendLine(") ids(EventId)")
            AppendCapturedNameApply(sql, "ci", "(c.FieldCode LIKE N'%ItemName%' OR REPLACE(c.FieldCode,N'_',N'') LIKE N'%ItemName%')")
            AppendIdentifierApply(sql, "ii", "((RIGHT(c.FieldCode,6)=N'ItemId') OR (CHARINDEX(N'_IM_ID',c.FieldCode)>0 AND CHARINDEX(N'U_IM_ID',c.FieldCode)=0) OR c.FieldCode IN(N'ComponentId',N'RecipeComponentId',N'FormatComponentId',N'SplitBalanceItem'))")
            sql.AppendLine("LEFT JOIN dbo.IM_Menu im ON im.IM_ID=ii.Identifier")
            AppendCapturedNameApply(sql, "ca", "c.FieldCode IN(N'AgentName',N'AccountName',N'SalaryEmployeeName')")
            AppendIdentifierApply(sql, "ai", "(RIGHT(c.FieldCode,9)=N'AccountId' OR RIGHT(c.FieldCode,10)=N'EmployeeId' OR c.FieldCode IN(N'Rct_AG_ID',N'AccountAGIDFROM',N'AccountAGIDTO',N'SalaryAGID',N'SalaryAdjEmployeeId',N'InvDocument_AG_ID',N'UserAgent',N'CommissionAccountId'))")
            sql.AppendLine("LEFT JOIN dbo.Agents a ON a.AG_ID=ai.Identifier")
            AppendCapturedNameApply(sql, "cs", "c.FieldCode=N'StoreName'")
            AppendIdentifierApply(sql, "si", "(RIGHT(c.FieldCode,7)=N'StoreId' OR CHARINDEX(N'_ST_ID',c.FieldCode)>0 OR c.FieldCode=N'SplitBalanceStore')")
            sql.AppendLine("LEFT JOIN dbo.Stores s ON s.ST_ID=si.Identifier")
            AppendCapturedNameApply(sql, "ct", "c.FieldCode IN(N'TreasuryName',N'TrName')")
            AppendIdentifierApply(sql, "ti", "(RIGHT(c.FieldCode,10)=N'TreasuryId' OR c.FieldCode IN(N'Rct_Tr_ID',N'RctTr_Tr_ID',N'CashTrID',N'UserTreasury'))")
            sql.AppendLine("LEFT JOIN dbo.TreasuryCard t ON t.Tr_ID=ti.Identifier;")
            cmd.CommandText = sql.ToString()
            cmd.CommandTimeout = 60
            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim eventId = reader.GetGuid(0), row As DataRow = Nothing
                    If Not rows.TryGetValue(eventId, row) Then Continue While
                    Dim notes = Convert.ToString(row("Notes"), CultureInfo.InvariantCulture)
                    Dim capturedItem = ReaderText(reader, 1), currentItem = ReaderText(reader, 2)
                    Dim capturedAccount = ReaderText(reader, 3), currentAccount = ReaderText(reader, 4)
                    Dim capturedStore = ReaderText(reader, 5), currentStore = ReaderText(reader, 6)
                    Dim capturedTreasury = ReaderText(reader, 7), currentTreasury = ReaderText(reader, 8)
                    notes = AppendReferenceNames(notes, "التعريف المحفوظ", capturedItem, capturedAccount, capturedStore, capturedTreasury)
                    notes = AppendReferenceNames(notes, "تعريف حالي مساعد (قد يختلف عن الاسم وقت العملية)",
                                                 If(capturedItem.Length = 0, currentItem, ""),
                                                 If(capturedAccount.Length = 0, currentAccount, ""),
                                                 If(capturedStore.Length = 0, currentStore, ""),
                                                 If(capturedTreasury.Length = 0, currentTreasury, ""))
                    row("Notes") = notes
                End While
            End Using
        End Using
    End Sub

    Private Shared Sub AppendCapturedNameApply(sql As StringBuilder, aliasName As String, predicate As String)
        Dim value = "CASE WHEN c.AfterState=3 THEN c.AfterValue WHEN c.BeforeState=3 THEN c.BeforeValue END"
        sql.Append("OUTER APPLY(SELECT TOP(1) ").Append(value).Append(" DisplayName FROM dbo.Network_Audit_Change c WHERE c.EventId=ids.EventId AND ").Append(predicate)
        sql.Append(" AND NULLIF(LTRIM(RTRIM(").Append(value).Append(")),N'') IS NOT NULL ORDER BY c.ChangeNo DESC) ").AppendLine(aliasName)
    End Sub

    Private Shared Sub AppendIdentifierApply(sql As StringBuilder, aliasName As String, predicate As String)
        Dim value = "TRY_CONVERT(int,CASE WHEN c.AfterState=3 THEN c.AfterValue WHEN c.BeforeState=3 THEN c.BeforeValue END)"
        sql.Append("OUTER APPLY(SELECT TOP(1) ").Append(value).Append(" Identifier FROM dbo.Network_Audit_Change c WHERE c.EventId=ids.EventId AND ").Append(predicate)
        sql.Append(" AND ").Append(value).Append(" IS NOT NULL ORDER BY c.ChangeNo) ").AppendLine(aliasName)
    End Sub

    Private Shared Function ReaderText(reader As SqlDataReader, ordinal As Integer) As String
        Return If(reader.IsDBNull(ordinal), "", Convert.ToString(reader.GetValue(ordinal), CultureInfo.InvariantCulture).Trim())
    End Function

    Private Shared Function AppendReferenceNames(notes As String, label As String, itemName As String,
                                                 accountName As String, storeName As String, treasuryName As String) As String
        Dim parts As New List(Of String)()
        AddReferencePart(parts, notes, "الصنف", itemName)
        AddReferencePart(parts, notes, "الحساب/العميل/المورد", accountName)
        AddReferencePart(parts, notes, "المخزن", storeName)
        AddReferencePart(parts, notes, "الخزينة/المصرف", treasuryName)
        If parts.Count = 0 Then Return notes
        Return notes.TrimEnd() & " | " & label & ": " & String.Join("؛ ", parts.ToArray()) & "."
    End Function

    Private Shared Sub AddReferencePart(parts As List(Of String), notes As String, title As String, value As String)
        If String.IsNullOrWhiteSpace(value) OrElse notes.IndexOf(value.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 Then Return
        parts.Add(title & " «" & value.Trim().Replace("«", "‹").Replace("»", "›") & "»")
    End Sub

    Friend Shared Function OutcomeLabel(value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then Return "غير موثقة (قديم)"
        Select Case Convert.ToInt32(value)
            Case 1 : Return "نجاح"
            Case 2 : Return "رفض"
            Case 3 : Return "فشل مؤكد"
            Case 4 : Return "إلغاء المستخدم"
            Case Else : Return "غير معروف"
        End Select
    End Function

    Friend Shared Function StateValue(state As Object, value As Object) As String
        Select Case Convert.ToInt32(state)
            Case 0 : Return "[لم تُلتقط]"
            Case 1 : Return "[غير موجود]"
            Case 2 : Return "[NULL]"
            Case 3 : Return If(IsDBNull(value), "[قيمة مفقودة: خلل بالعقد]", Convert.ToString(value, CultureInfo.InvariantCulture))
            Case Else : Return "[حالة غير معروفة]"
        End Select
    End Function

    Friend Shared Function BuildDetailsCommand(recordKind As String, sequenceId As Long, eventId As Guid) As SqlCommand
        Dim query As String
        If recordKind = "Legacy" Then
            query = "SELECT T_ID,User_ID,[Date],Screen_Type,Operation_ID,Bill_ID,CP_Name,Notes FROM dbo.Network_Edit_Tracker WHERE T_ID=@Id;"
        ElseIf recordKind = "V2" Then
            query = "SELECT EventId,LocalSequence,SourceId,RequestId,EventOrdinal,CorrelationId,ContractVersion,EventCode,DomainCode,ScreenCode,EventKind,Outcome,ActorKind,ActorUserId,ActorServiceCode,ActorName,SessionId,MachineName,ApplicationVersion,EntityType,EntityId,DocumentNumber,OccurredAtUtc,RecordedAtUtc,BusinessDate,LegacyScreenType,LegacyOperationId,ReasonCode,ReasonText,TemplateCode,TemplateVersion,Notes,CONVERT(nvarchar(max),RelatedEntitiesXml) RelatedEntitiesXml FROM dbo.Network_Audit_Event WHERE EventId=@EventId;" &
                    "SELECT ChangeNo,EntityType,EntityId,FieldCode,ValueType,SourceSqlType,BeforeState,BeforeValue,AfterState,AfterValue,ValueOrigin FROM dbo.Network_Audit_Change WHERE EventId=@EventId ORDER BY ChangeNo;"
        Else
            Throw New ArgumentException("نوع سجل غير معروف.")
        End If
        Dim cmd As New SqlCommand(query) With {.CommandTimeout = 60}
        If recordKind = "Legacy" Then
            cmd.Parameters.Add("@Id", SqlDbType.BigInt).Value = sequenceId
        Else
            cmd.Parameters.Add("@EventId", SqlDbType.UniqueIdentifier).Value = eventId
        End If
        Return cmd
    End Function

    Friend Function ReadDetails(recordKind As String, sequenceId As Long, eventId As Guid) As DataSet
        If Not allowLegacy AndAlso recordKind <> "V2" Then Throw New InvalidOperationException("تفاصيل السجل القديم خارج هذه الشاشة.")
        Using cn As New SqlConnection(connectionString), cmd As SqlCommand = BuildDetailsCommand(recordKind, sequenceId, eventId)
            cmd.Connection = cn
            Using adapter As New SqlDataAdapter(cmd)
                Dim result As New DataSet()
                adapter.Fill(result)
                If result.Tables(0).Rows.Count <> 1 Then Throw New InvalidOperationException("لم يعد السجل موجودًا؛ أعد البحث.")
                Return result
            End Using
        End Using
    End Function
End Class
