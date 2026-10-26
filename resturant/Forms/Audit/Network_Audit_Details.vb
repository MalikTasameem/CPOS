Imports System.Globalization

Public Class Network_Audit_Details
    Public Sub New()
        InitializeComponent()
    End Sub

    Friend Sub SetDetails(kind As String, data As DataSet, capabilities As AuditReadCapabilities)
        Dim row As DataRow = data.Tables(0).Rows(0)
        Dim metadata As New DataTable()
        metadata.Columns.Add("Name", GetType(String))
        metadata.Columns.Add("Value", GetType(String))
        NotesText.Text = Convert.ToString(row("Notes"))
        For Each column As DataColumn In data.Tables(0).Columns
            If column.ColumnName = "Notes" OrElse column.ColumnName = "RelatedEntitiesXml" Then Continue For
            Dim value As String = If(IsDBNull(row(column)), "[NULL]", Convert.ToString(row(column), CultureInfo.InvariantCulture))
            If TypeOf row(column) Is DateTime Then
                value = CDate(row(column)).ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)
                If column.ColumnName.EndsWith("Utc", StringComparison.Ordinal) Then value &= " UTC"
            End If
            metadata.Rows.Add(FieldLabel(column.ColumnName), value)
        Next
        MetadataGrid.DataSource = metadata
        If kind = "Legacy" Then
            NoticeLabel.Text = "سجل قديم: النتيجة وقيم قبل/بعد وهوية المصدر والمنطقة الزمنية غير موثقة. أسماء التعريفات في قائمة البحث ليست لقطات تاريخية. لا نستنتج بيانات مفقودة من المستند الحالي."
            ChangesTab.Text = "قبل/بعد: غير متوفر للقديم"
            ChangesGrid.DataSource = Nothing
            RelationsText.Text = "السجل القديم لا يحتوي مراجع هيكلية محفوظة. راجع رقم المستند والوصف دون افتراض روابط إضافية."
        Else
            NoticeLabel.Text = "حدث V2 محفوظ — الأوقات الموسومة UTC أصلية. وقت المستند BusinessDate ليس UTC. قائمة البحث تعرض وقت الحدث بمنطقة " & capabilities.TimeZone.Id & "."
            Dim changes As DataTable = data.Tables(1).Copy()
            changes.Columns.Add("BeforeDisplay", GetType(String))
            changes.Columns.Add("AfterDisplay", GetType(String))
            changes.Columns.Add("OriginDisplay", GetType(String))
            For Each change As DataRow In changes.Rows
                change("BeforeDisplay") = AuditCompatibilityReader.StateValue(change("BeforeState"), change("BeforeValue"))
                change("AfterDisplay") = AuditCompatibilityReader.StateValue(change("AfterState"), change("AfterValue"))
                Dim origin As String = "غير معروف"
                Select Case CInt(change("ValueOrigin"))
                    Case 1 : origin = "قاعدة البيانات"
                    Case 2 : origin = "مدخل المستخدم"
                    Case 3 : origin = "قيمة مشتقة"
                End Select
                change("OriginDisplay") = origin & " / " & Convert.ToString(change("SourceSqlType"))
            Next
            ChangesGrid.DataSource = changes
            ChangesTab.Text = "القيم السابقة واللاحقة (" & changes.Rows.Count & ")"
            RelationsText.Text = If(IsDBNull(row("RelatedEntitiesXml")), "لا توجد مراجع مرتبطة محفوظة لهذا الحدث.", Convert.ToString(row("RelatedEntitiesXml")))
        End If
    End Sub

    Private Shared Function FieldLabel(name As String) As String
        Select Case name
            Case "EventId" : Return "هوية الحدث — EventId"
            Case "SourceId" : Return "هوية المصدر — SourceId"
            Case "RequestId" : Return "هوية الطلب — RequestId"
            Case "CorrelationId" : Return "ربط دورة العمل — CorrelationId"
            Case "SessionId" : Return "الجلسة — SessionId"
            Case "ActorName" : Return "اسم الفاعل وقت الحدث"
            Case "ActorUserId", "User_ID" : Return "رقم المستخدم"
            Case "DocumentNumber", "Bill_ID" : Return "رقم المستند المعروض"
            Case "EntityId" : Return "المعرف الفعلي للكيان"
            Case "EntityType" : Return "نوع الكيان"
            Case "MachineName", "CP_Name" : Return "الجهاز"
            Case "OccurredAtUtc" : Return "وقت وقوع الحدث — UTC"
            Case "RecordedAtUtc" : Return "وقت تسجيله — UTC"
            Case "BusinessDate" : Return "تاريخ المستند كما حُفظ"
            Case "Date" : Return "التاريخ القديم كما حُفظ"
            Case "ReasonText" : Return "شرح السبب"
            Case "ScreenCode" : Return "رمز الشاشة"
            Case "DomainCode" : Return "المجال"
            Case "EventCode" : Return "رمز الفعل"
            Case "ApplicationVersion" : Return "إصدار التطبيق"
            Case Else : Return name
        End Select
    End Function

    Private Sub Details_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If Not User_isAdmin OrElse USER_ID <= 0 Then
            MessageBox.Show(Me, "لا توجد صلاحية لعرض تفاصيل المراقبة.", "صلاحية العرض", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Close()
        End If
    End Sub
End Class
