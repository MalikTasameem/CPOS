Option Strict On
Imports System.Windows.Forms

' Only shown after an ambiguous save. No automatic retry or new request identity.
Friend NotInheritable Class AuditQuantityRecoveryDialog
    Private ReadOnly connection As String
    Private ReadOnly request As AuditOperationRequest
    Private ReadOnly execute As Action(Of String, AuditOperationRequest)
    Private resolved As Boolean

    Friend Sub New(connectionValue As String, requestValue As AuditOperationRequest, errorText As String, Optional executeValue As Action(Of String, AuditOperationRequest) = Nothing)
        InitializeComponent()
        connection = connectionValue : request = requestValue
        execute = If(executeValue, AddressOf AuditSalesQuantityUi.Save)
        If request.OperationCode = "Sales.LineDeleted" Then Text = "حسم نتيجة حذف الصنف"
        If request.OperationCode = "Sales.LineAdded" Then Text = "حسم نتيجة إضافة الصنف"
        If request.OperationCode = "Document.Voided" Then Text = "حسم نتيجة إلغاء المستند"
        If AuditContract.IsUnit(request.OperationCode) Then Text = "حسم نتيجة عملية الوحدة"
        If AuditContract.IsGroup(request.OperationCode) Then Text = "حسم نتيجة عملية التصنيف"
        If AuditContract.IsItemUnit(request.OperationCode) Then Text = "حسم نتيجة عملية رابط وحدة الصنف"
        If AuditContract.IsPricingRule(request.OperationCode) Then Text = "حسم نتيجة عملية العرض أو نسبة التسعير"
        If AuditContract.IsItemMaster(request.OperationCode) Then Text = "حسم نتيجة عملية بطاقة الصنف"
        If AuditContract.IsTableOperation(request.OperationCode) Then Text = "حسم نتيجة عملية الطاولة"
        If AuditContract.IsFinance(request.OperationCode) Then Text = "حسم نتيجة الحركة المالية"
        If AuditContract.IsReceipt(request.OperationCode) Then Text = "حسم نتيجة عملية الإيصال"
        If AuditContract.IsStockDocumentLine(request.OperationCode) Then Text = "حسم نتيجة عملية الصنف المخزني"
        If AuditContract.IsEmployeeValue(request.OperationCode) Then Text = "حسم نتيجة قيمة الموظف"
        If AuditContract.IsReturnLine(request.OperationCode) Then Text = "حسم نتيجة عملية صنف المرتجع"
        If AuditContract.IsExpenseLine(request.OperationCode) Then Text = "حسم نتيجة بند المصروف"
        If AuditContract.IsDocumentField(request.OperationCode) Then Text = "حسم نتيجة تغيير حساب/خصم الفاتورة"
        If AuditContract.IsQuoteLine(request.OperationCode) Then Text = "حسم نتيجة عملية صنف عرض السعر"
        If AuditContract.IsPeopleCard(request.OperationCode) Then Text = "حسم نتيجة بطاقة العميل/المستخدم"
        If AuditContract.IsTableSplit(request.OperationCode) Then Text = "حسم نتيجة نقل صنف الطاولة"
        If request.OperationCode = "Salary.BatchPosted" Then Text = "حسم نتيجة ترصيد المرتبات"
        If AuditContract.IsItemLifecycle(request.OperationCode) Then Text = "حسم نتيجة حفظ/إنشاء بطاقة الصنف"
        If AuditContract.IsWorkflow(request.OperationCode) Then Text = "حسم نتيجة حفظ بيانات الفاتورة"
        If AuditContract.IsInventoryOperation(request.OperationCode) Then Text = "حسم نتيجة عملية المخزون/التصنيع"
        If request.OperationCode = "Purchase.LineAdded" Then Text = "حسم نتيجة إضافة صنف المشتريات/الجرد"
        If request.OperationCode = "AccountEntry.Created" Then Text = "حسم نتيجة حركة الحساب"
        ShowMessage(errorText)
    End Sub

    Private Sub Retry_Click(sender As Object, e As EventArgs) Handles RetryButton.Click
        RetryButton.Enabled = False
        Try
            execute(connection, request)
            resolved = True : DialogResult = DialogResult.OK : Close()
        Catch ex As Exception
            If AuditSalesQuantityUi.Status(connection, request) = "Completed" Then
                resolved = True : DialogResult = DialogResult.OK : Close()
            Else
                ShowMessage(ex.Message)
            End If
        Finally
            RetryButton.Enabled = True
        End Try
    End Sub

    Private Sub Close_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Close()
    End Sub

    Private Sub ShowMessage(errorText As String)
        InfoLabel.Text = "نتيجة العملية غير محسومة. استعد الاتصال ثم أعد المحاولة بالطلب نفسه؛ لا تنفذ عملية إضافية من الشاشة الأساسية." & Environment.NewLine &
            "لن يسمح بالرجوع حتى التأكد من نجاح الطلب أو عدم حفظه." & Environment.NewLine & "المرجع: " & request.RequestId.ToString() & Environment.NewLine & errorText
    End Sub

    Private Sub CheckBeforeClose(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If resolved Then Return
        Select Case AuditSalesQuantityUi.Status(connection, request)
            Case "Completed" : resolved = True : DialogResult = DialogResult.OK
            Case "NotFound" : resolved = True : DialogResult = DialogResult.Cancel
            Case Else
                e.Cancel = True : ShowMessage("الاتصال غير متاح أو حالة الطلب تحتاج المراجعة؛ لم تُرسل عملية إضافية.")
        End Select
    End Sub
End Class
