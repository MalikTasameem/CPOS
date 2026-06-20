Imports System.Data
Imports System.Data.SqlClient

Public Class ChequeRepository

    ''' <summary>
    ''' تعديل بيانات الشيك المحدد.
    ''' ترجع True/False، وتعيد رسالة في المتغير ByRef message.
    ''' </summary>
    Public Shared Function UpdateCheque(
        ByVal connectionString As String,
        ByVal chequeId As Integer,
        ByVal issueDate As Nullable(Of Date),            ' تاريخ الإصدار (يقبل Null)
        ByVal dueDate As Nullable(Of Date),              ' تاريخ الاستحقاق (يقبل Null)
        ByVal reconciliationDate As Nullable(Of Date),   ' تاريخ المطابقة (يقبل Null)
        ByVal bankTransactionNumber As String,           ' رقم الحركة البنكية (قد يكون Null/Empty)
        ByVal notes As String,                           ' ملاحظات (قد يكون Null/Empty)
        ByVal statusId As Integer,                       ' رقم حالة الشيك
        ByVal autoSetReconciliationIfFinal As Boolean,   ' لو True يتم تعبئة تاريخ المطابقة تلقائيًا للحالات النهائية
        ByRef message As String                          ' نص رسالة الخرج
    ) As Boolean

        ' (1) تحقق منطقي قبل التنفيذ
        If issueDate.HasValue AndAlso dueDate.HasValue AndAlso dueDate.Value < issueDate.Value Then
            message = "خطأ: تاريخ الاستحقاق يجب أن يكون أكبر من أو يساوي تاريخ الإصدار."
            Return False
        End If

        ' الحالات النهائية: مطابق(1) / مرفوض(2) / ملغي(4)
        Dim isFinal As Boolean = (statusId = 1 OrElse statusId = 2 OrElse statusId = 4)

        If isFinal Then
            If Not reconciliationDate.HasValue Then
                If autoSetReconciliationIfFinal Then
                    reconciliationDate = Date.Today
                Else
                    message = "خطأ: يجب إدخال تاريخ المطابقة للحالات النهائية (مطابق/مرفوض/ملغي)."
                    Return False
                End If
            End If
        End If

        If statusId = 0 AndAlso reconciliationDate.HasValue Then
            message = "تنبيه: لا يجب إدخال تاريخ مطابقة والحالة قيد الانتظار."
            Return False
        End If

        ' تقليم النصوص بما يتوافق مع طول الحقول
        If Not String.IsNullOrEmpty(bankTransactionNumber) AndAlso bankTransactionNumber.Length > 50 Then
            bankTransactionNumber = bankTransactionNumber.Substring(0, 50)
        End If
        If Not String.IsNullOrEmpty(notes) AndAlso notes.Length > 400 Then
            notes = notes.Substring(0, 400)
        End If

        '        ' (2) تنفيذ التحديث
        '        Const sql As String =
        '"UPDATE ACC_BALANCE_MASTER
        '   SET IssueDate = @IssueDate,
        '       DueDate = @DueDate,
        '       ReconciliationDate = @ReconciliationDate,
        '       BankTransactionNumber = @BankTransactionNumber,
        '       Cheque_Notes = @Notes,
        '       StatusId = @StatusId
        ' WHERE T_ID = @ChequeId;
        '"

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Using cmd As New SqlCommand()
                    cmd.Connection = conn
                    cmd.CommandText = "[UPDATE_Cheque_Status]"
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add("@ChequeId", SqlDbType.Int).Value = chequeId

                    If issueDate.HasValue Then
                        cmd.Parameters.Add("@IssueDate", SqlDbType.Date).Value = issueDate.Value
                    Else
                        cmd.Parameters.Add("@IssueDate", SqlDbType.Date).Value = DBNull.Value
                    End If

                    If dueDate.HasValue Then
                        cmd.Parameters.Add("@DueDate", SqlDbType.Date).Value = dueDate.Value
                    Else
                        cmd.Parameters.Add("@DueDate", SqlDbType.Date).Value = DBNull.Value
                    End If

                    If reconciliationDate.HasValue Then
                        cmd.Parameters.Add("@ReconciliationDate", SqlDbType.Date).Value = reconciliationDate.Value
                    Else
                        cmd.Parameters.Add("@ReconciliationDate", SqlDbType.Date).Value = DBNull.Value
                    End If

                    cmd.Parameters.Add("@BankTransactionNumber", SqlDbType.NVarChar, 50).Value =
                        IIf(String.IsNullOrWhiteSpace(bankTransactionNumber), DBNull.Value, bankTransactionNumber.Trim())

                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 400).Value =
                        IIf(String.IsNullOrWhiteSpace(notes), DBNull.Value, notes.Trim())

                    cmd.Parameters.Add("@StatusId", SqlDbType.TinyInt).Value = statusId

                    cmd.Parameters.Add("@USER_ID", SqlDbType.TinyInt).Value = USER_ID


                    cmd.Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
                    cmd.Parameters("@ERROR_MSG").Direction = ParameterDirection.Output

                    Dim affected As Integer = cmd.ExecuteNonQuery()
                    If affected = 0 Then
                        message = "لم يتم العثور على الشيك المطلوب أو لم يحدث أي تغيير." & vbNewLine & cmd.Parameters("@ERROR_MSG").Value.ToString()
                        Return False
                    End If
                End Using
            End Using

            message = "تم تحديث  🔄بيانات الشيك بنجاح."
            Return True

        Catch ex As SqlException
            message = "خطأ SQL: " & ex.Message
            Return False
        Catch ex As Exception
            message = "حدث خطأ غير متوقع: " & ex.Message
            Return False
        End Try
    End Function

End Class
