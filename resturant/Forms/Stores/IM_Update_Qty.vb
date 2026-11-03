Public Class IM_Update_Qty


    Public ST_NAME As String = ""
    Public ST_ID As Integer = 0
    Public IM_NAME As String = ""
    Public IM_ID As Integer = 0
    Public Unit_NAME As String = ""
    Public CURRENT_QTY As Double = 0
    Public New_Qty As Double = 0
    Public Cost As Double = 0
    Public D_Valid As String = ""
    Public Barcode As String = ""
    Public Bill_T_ID As Integer
    Public Unit_ID As Integer
    Public EditLineId As Integer
    Private editSnapshot As String
    Private ready As Boolean
    Private pendingAudit As AuditOperationRequest
    Private pendingConnection As String
    Private saving As Boolean

    Private Sub POS_D_Valid_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub POS_D_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ST_Name_txt.Text = ST_NAME  'F_STORES_Explorer.gridv.CurrentRow.Cells(3).Value
        IM_Name_txt.Text = IM_NAME ' F_STORES_Explorer.gridv.CurrentRow.Cells(7).Value
        Unit_txt.Text = Unit_NAME ' F_STORES_Explorer.gridv.CurrentRow.Cells(8).Value
        Cost_txt.Text = Cost  'F_STORES_Explorer.gridv.CurrentRow.Cells(11).Value
        QTY_txt.Text = CURRENT_QTY 'F_STORES_Explorer.gridv.CurrentRow.Cells(10).Value
        New_Qty_txt.Text = "0" 'F_STORES_Explorer.gridv.CurrentRow.Cells(10).Value

        QTY_txt.ReadOnly = True
        Cost_txt.ReadOnly = True ' Cost calculation is owned by the existing SQL path.
        Try
            If Not AuditSettlementLines.IsEnabled(MY_Settings.SqlConStr) Then Throw New InvalidOperationException("مراقبة التسويات الجديدة غير مفعلة؛ لا يمكن حفظ الكمية.")
            If EditLineId > 0 Then
                Dim row = AuditSettlementLines.ReadEditLine(MY_Settings.SqlConStr,
                    AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, "IM_Update_Qty"), Bill_T_ID, EditLineId)
                editSnapshot = AuditSettlementLines.LineSnapshot(row)
                Dim cargo = Convert.ToDecimal(row("U_Cargo"))
                If cargo <= 0D Then Throw New InvalidOperationException("معامل الوحدة المحفوظ غير صالح.")
                CURRENT_QTY = Convert.ToDouble(row("ST_QTY")) / Convert.ToDouble(cargo)
                QTY_txt.Text = CURRENT_QTY.ToString("G17")
                New_Qty_txt.Text = Convert.ToDecimal(CURRENT_QTY).ToString("G29")
                Cost_txt.Text = Convert.ToString(row("Cost"))
                Unit_txt.Text = "الوحدة المحفوظة؛ معامل " & cargo.ToString()
                Valid_Panel.Visible = False ' Identity, expiry, unit and cost stay unchanged on line editing.
                Label8.Text = "الفرق السابق"
                Label6.Text = "الفرق الجديد"
                Me.Text = "تعديل فرق سطر التسوية (موجب أو سالب)"
                ConfermButton.Text = "حفظ F12"
            Else
                Check_IF_IM_Valid()
                RefreshCurrentQuantity()
            End If
            ready = True
            New_Qty_txt.Select()
        Catch ex As Exception
            ConfermButton.Enabled = False
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Public Sub Check_IF_IM_Valid()

        Dim c As New C
        Try
            Dim s As String
            s = "select isValid from IM_Menu WHERE IM_ID = '" & IM_ID & "'" 'F_STORES_Explorer.gridv.CurrentRow.Cells(1).Value
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                If c.Dr("isValid") = 1 Then
                    If D_Valid <> "" Then 'F_STORES_Explorer.gridv.CurrentRow.Cells(9).Value
                        Valid_Date.Text = D_Valid
                    End If
                Else
                    Valid_Panel.Visible = False
                End If
            Else
                Throw New InvalidOperationException("الصنف غير موجود؛ أعد اختيار الصنف.")
            End If
        Finally
            c.Con.Close()
        End Try
    End Sub



    Private Sub ConfermButton_Click(sender As Object, e As EventArgs) Handles ConfermButton.Click
        If saving Then Return
        If Not ready AndAlso pendingAudit Is Nothing Then Return
        If Bill_T_ID <= 0 Then
            MsgBox("يجب فتح نافذة الكمية من مستند تسوية محدد.", MsgBoxStyle.Exclamation)
            Return
        End If
        Try
            Dim requested As Decimal
            If Not Decimal.TryParse(New_Qty_txt.Text, requested) Then Throw New ArgumentException("أدخل كمية صحيحة.")
            If IM_min_QTY = False AndAlso EditLineId = 0 Then
                If requested < 0D Then
                    MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
            saving = True
            ConfermButton.Enabled = False
            If pendingAudit Is Nothing AndAlso Not AuditSettlementLines.IsEnabled(MY_Settings.SqlConStr) Then Throw New InvalidOperationException("مراقبة التسويات الجديدة غير مفعلة؛ لم يتم حفظ الكمية.")
            If pendingAudit Is Nothing Then
                pendingConnection = MY_Settings.SqlConStr
                If EditLineId > 0 Then
                    pendingAudit = AuditSettlementLines.UpdateRequest(AuditSecuritySession.Current.ContextFor(pendingConnection, USER_ID, "IM_Update_Qty"),
                        Bill_T_ID, EditLineId, requested, editSnapshot, IM_min_QTY)
                Else
                    Dim difference = AuditSettlementLines.VerifiedDifference(requested, CURRENT_QTY, CURRENT_QTY)
                    pendingAudit = AuditSettlementLines.AddRequest(AuditSecuritySession.Current.ContextFor(pendingConnection, USER_ID, "IM_Update_Qty"),
                        Bill_T_ID, IM_ID, ST_ID, Unit_ID, Barcode, requested, difference, CURRENT_QTY,
                        If(Valid_Panel.Visible, CType(DateTime.SpecifyKind(Valid_Date.Value.Date, DateTimeKind.Unspecified), DateTime?), Nothing), IM_min_QTY)
                End If
            End If
            SetPendingInputs(True)
            Dim result = AuditSettlementLines.Save(pendingConnection, pendingAudit)
            pendingAudit = Nothing
            MsgBox(If(result.ResultCode.GetValueOrDefault() = 5, "لا يوجد تغيير؛ لم تسجل حركة إضافية.", "تم حفظ سطر التسوية وتسجيله في المراقبة."), MsgBoxStyle.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            If pendingAudit IsNot Nothing Then
                Try
                    If AuditRequestCoordinator.Status(pendingConnection, pendingAudit).Status = "NotFound" Then
                        pendingAudit = Nothing
                        SetPendingInputs(False)
                        If EditLineId = 0 Then
                            Try
                                RefreshCurrentQuantity()
                            Catch refreshError As Exception
                                ready = False
                                MsgBox("تعذر تحديث الرصيد؛ أغلق النافذة وأعد فتحها قبل الحفظ." & Environment.NewLine & refreshError.Message, MsgBoxStyle.Exclamation)
                            End Try
                        End If
                    End If
                Catch
                    ' Keep the same attempt; do not enable a different payload after an unknown result.
                End Try
            End If
        Finally
            saving = False
            If Not IsDisposed Then ConfermButton.Enabled = ready OrElse pendingAudit IsNot Nothing
        End Try
    End Sub

    Private Sub SetPendingInputs(pending As Boolean)
        New_Qty_txt.Enabled = Not pending
        QTY_txt.Enabled = Not pending
        Cost_txt.Enabled = Not pending
        Valid_Date.Enabled = Not pending AndAlso EditLineId = 0
    End Sub

    Private Sub RefreshCurrentQuantity()
        CURRENT_QTY = AuditSettlementLines.ReadCurrentQuantity(MY_Settings.SqlConStr, IM_ID, ST_ID, Unit_ID,
            If(Valid_Panel.Visible, CType(DateTime.SpecifyKind(Valid_Date.Value.Date, DateTimeKind.Unspecified), DateTime?), Nothing))
        QTY_txt.Text = CURRENT_QTY.ToString("G17")
        New_Qty_txt_TextChanged(New_Qty_txt, EventArgs.Empty)
    End Sub

    Private Sub Valid_Date_ValueChanged(sender As Object, e As EventArgs) Handles Valid_Date.ValueChanged
        If Not ready OrElse EditLineId > 0 OrElse pendingAudit IsNot Nothing Then Return
        Try
            RefreshCurrentQuantity()
        Catch ex As Exception
            ready = False
            ConfermButton.Enabled = False
            MsgBox("تعذر قراءة رصيد الصلاحية؛ أعد فتح النافذة." & Environment.NewLine & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub IM_Update_Qty_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If pendingAudit Is Nothing Then Return
        Try
            Dim status = AuditRequestCoordinator.Status(pendingConnection, pendingAudit)
            If status.Status = "Completed" Then
                pendingAudit = Nothing
                Me.DialogResult = DialogResult.OK
                Return
            End If
            If status.Status = "NotFound" Then
                pendingAudit = Nothing
                Return
            End If
        Catch
        End Try
        e.Cancel = True
        MsgBox("نتيجة التسوية غير محسومة. أعد المحاولة بنفس الزر بعد عودة الاتصال. مرجع الطلب: " & pendingAudit.RequestId.ToString("D"), MsgBoxStyle.Exclamation)
    End Sub


    Private Sub Cost_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cost_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Cost_txt_TextChanged(sender As Object, e As EventArgs) Handles Cost_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub IM_Update_Qty_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 Then ConfermButton_Click(sender, e)
    End Sub

    Private Sub Cost_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Cost_txt.KeyDown
        If e.KeyCode = Keys.Down Then QTY_txt.Select()
    End Sub

    Private Sub QTY_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QTY_txt.KeyPress
        Check_Only_Float_With_Negitave(sender, e)
    End Sub

    Private Sub QTY_txt_TextChanged(sender As Object, e As EventArgs) Handles QTY_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub



    Private Sub New_Qty_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles New_Qty_txt.KeyPress
        If EditLineId > 0 OrElse IM_min_QTY Then
            Check_Only_Float_With_Negitave(sender, e)
        Else
            Check_Only_Float(sender, e)
        End If
    End Sub

    Private Sub New_Qty_txt_TextChanged(sender As Object, e As EventArgs) Handles New_Qty_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
        If String.IsNullOrWhiteSpace(New_Qty_txt.Text) Then New_Qty_txt.Text = 0
        Dim displayed As Decimal, requested As Decimal
        If Not Decimal.TryParse(QTY_txt.Text, displayed) OrElse Not Decimal.TryParse(New_Qty_txt.Text, requested) Then Return
        Dim difference As Decimal = requested - displayed
        Qty_Deference_Txt.Text = Math.Abs(difference).ToString()
        If difference = 0D Then
            Qty_Deference_Txt.ForeColor = Color.Black
            Tag_txt.Text = " "
        ElseIf difference < 0D Then
            Qty_Deference_Txt.ForeColor = Color.DarkRed
            Tag_txt.Text = "-"
        Else
            Qty_Deference_Txt.ForeColor = Color.DarkGreen
            Tag_txt.Text = "+"
        End If
    End Sub
End Class
