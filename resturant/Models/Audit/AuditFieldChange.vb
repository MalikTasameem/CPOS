Option Strict On
Imports System.Globalization

Friend Enum AuditValueState As Byte
    NotCaptured = 0
    Absent = 1
    SqlNull = 2
    Present = 3
End Enum

Friend NotInheritable Class AuditValue
    Public ReadOnly State As AuditValueState
    Public ReadOnly Text As String
    Public ReadOnly ValueType As String

    Private Sub New(stateValue As AuditValueState, textValue As String, Optional capturedType As String = Nothing)
        State = stateValue
        Text = textValue
        ValueType = capturedType
    End Sub

    Friend Shared Function Missing(state As AuditValueState) As AuditValue
        If state < AuditValueState.NotCaptured OrElse state > AuditValueState.SqlNull Then Throw New ArgumentException("حالة قيمة غير صحيحة.")
        Return New AuditValue(state, Nothing)
    End Function

    ' No display rounding, culture-dependent numbers, or implicit Float-to-Decimal conversion.
    Friend Shared Function Capture(valueType As String, value As Object) As AuditValue
        If value Is Nothing Then Throw New ArgumentException("استخدم Missing لتمييز القيمة غير الملتقطة عن NULL.")
        If IsDBNull(value) Then Return Missing(AuditValueState.SqlNull)
        Dim text As String
        Select Case valueType
            Case "String"
                If Not TypeOf value Is String Then Throw New ArgumentException("القيمة ليست نصًا.")
                text = CStr(value)
            Case "Integer"
                If Not (TypeOf value Is Integer OrElse TypeOf value Is Long OrElse TypeOf value Is Short OrElse TypeOf value Is Byte) Then Throw New ArgumentException("القيمة ليست عددًا صحيحًا.")
                text = Convert.ToInt64(value).ToString(CultureInfo.InvariantCulture)
            Case "Decimal"
                If Not TypeOf value Is Decimal Then Throw New ArgumentException("يلزم Decimal دون تحويل ضمني.")
                text = CDec(value).ToString("G29", CultureInfo.InvariantCulture)
            Case "Float"
                If Not TypeOf value Is Double Then Throw New ArgumentException("يلزم Double لتمثيل SQL float(53).")
                Dim number As Double = CDbl(value)
                If Double.IsNaN(number) OrElse Double.IsInfinity(number) Then Throw New ArgumentException("Float غير صالح.")
                text = number.ToString("R", CultureInfo.InvariantCulture)
            Case "Boolean"
                If Not TypeOf value Is Boolean Then Throw New ArgumentException("القيمة ليست Boolean.")
                text = If(CBool(value), "1", "0")
            Case "Date", "DateTime"
                If Not TypeOf value Is DateTime Then Throw New ArgumentException("القيمة ليست تاريخًا.")
                Dim dateValue As DateTime = CDate(value)
                If dateValue.Kind <> DateTimeKind.Unspecified Then Throw New ArgumentException("قيمة تاريخ SQL يجب أن تكون Unspecified؛ لا نخمن المنطقة الزمنية.")
                If valueType = "Date" AndAlso dateValue.TimeOfDay <> TimeSpan.Zero Then Throw New ArgumentException("لا تُقص الساعة ضمنيًا من Date.")
                text = dateValue.ToString(If(valueType = "Date", "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffffff"), CultureInfo.InvariantCulture)
            Case "Guid"
                If Not TypeOf value Is Guid Then Throw New ArgumentException("القيمة ليست Guid.")
                text = CType(value, Guid).ToString("D")
            Case Else
                Throw New ArgumentException("نوع قيمة مراقبة غير معتمد.")
        End Select
        If text.Length > 4096 Then Throw New ArgumentException("قيمة المراقبة تتجاوز 4096 حرفًا؛ لا يتم قصها.")
        Return New AuditValue(AuditValueState.Present, text, valueType)
    End Function
End Class

Friend Class AuditFieldChange
    Public Property ChangeNo As Integer
    Public Property EntityType As String
    Public Property EntityId As String
    Public Property FieldCode As String
    Public Property ValueType As String
    Public Property SourceSqlType As String
    Public Property Before As AuditValue
    Public Property After As AuditValue
    Public Property ValueOrigin As Byte
End Class

Friend Class AuditInput
    Public Property Name As String
    Public Property ValueType As String
    Public Property Value As AuditValue
End Class
