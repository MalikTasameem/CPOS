Imports System.Drawing.Printing

Imports System.Globalization

Public Class Receipt

    'Dim rs As New Resizer
    Dim Reciept_T_ID As Integer
    Dim Is_ComandSuccess As Boolean = False
    Dim EditState As String = ""
    Public AG_ID As Integer = 0
    Dim IM_Dt As New DataTable
    Dim On_Update As Boolean = False
    Private receiptOperationInProgress As Boolean
    Private receiptContextCaptured As Boolean
    Private receiptEntryFormType As Integer
    Private receiptDeferredCustodyMode As Boolean
    Private receiptLinkedTransactionId As Integer
    Private loadedReceiptType As Integer
    Private receiptResultNeedsReload As Boolean

    Public Clear_Tran_ID As Boolean = True

    Dim ReceiptNum As Integer = 0
    'Dim Tr_B As Double = 0
    Private Sub new_butt_Click(sender As Object, e As EventArgs) Handles new_butt.Click
        If receiptOperationInProgress Then Return
        Make_New_Receipt()
    End Sub

    Dim KEY_HANDLER As String
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    Private Sub Make_New_Receipt()
        If receiptOperationInProgress OrElse receiptResultNeedsReload Then Return
        Is_ComandSuccess = False
        AG_Cm.Enabled = True
        ReceiptTypeComboBox.Enabled = True
        Fields_Panel.Enabled = True
        ClearFields()
        save_butt.Enabled = True
        print_butt.Enabled = False
        Void_Lb.Visible = False
        Edit_butt.Enabled = False
        Edit_butt.Text = "تعديـل F3"
        Edit_butt.BackColor = Color.White
        DeleteButton.Enabled = False
        Me.BackColor = SystemColors.Control
        Get_MAX_T_ID()
        AG_Cm.Focus()
    End Sub

    Private Sub After_Save_Receipt()
        Fields_Panel.Enabled = False
        save_butt.Enabled = False
        print_butt.Enabled = True
        Edit_butt.Enabled = True
        DeleteButton.Enabled = True
    End Sub


    Public Sub ClearFields()
        If receiptOperationInProgress Then Return
        Reciept_T_ID = 0
        loadedReceiptType = 0
        Is_ComandSuccess = False
        receiptLinkedTransactionId = 0
        For Each a As Control In Fields_Panel.Controls
            If TypeOf a Is TextBox Then
                a.Text = Nothing
            End If
        Next

        AG_Cm.Textt = ""
        Current_QTY.Text = Nothing
        payment_Type_combo.SelectedIndex = 0
        Receipt_Title_combobox.Clear()
        Receipt_Title_combobox.Text = ""
        bankName_Combo.SelectedIndex = -1
        DateTimeReceipt.Text = Date.Now
        On_Update = False
        ReceiptTypeComboBox.SelectedValue = AG_Type
        Receipt_Tran_ID = 0
        Treasury_ComboBox.SelectedValue = Rct_Tr_ID
        Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
    End Sub

    Private Sub Load_Data()

        Dim C As New C
        Try
            Dim sql As String = "Select id , Type_Name from AgentBalance_Type Order By id ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            ReceiptTypeComboBox.DataSource = C.Dt
            ReceiptTypeComboBox.DisplayMember = "Type_Name"
            ReceiptTypeComboBox.ValueMember = "id"
            ReceiptTypeComboBox.SelectedValue = AG_Type
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            Dim sql As String = "Select Distinct Tr_ID,Tr_Name from TR_MENU_V "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            Treasury_ComboBox.DataSource = C.Dt
            Treasury_ComboBox.DisplayMember = "Tr_Name"
            Treasury_ComboBox.ValueMember = "Tr_ID"
            Treasury_ComboBox.SelectedValue = Rct_Tr_ID
            If Rct_Tr_Can_change = False Then Treasury_ComboBox.Enabled = False
            Treasury_ComboBox.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            Dim sql As String = "Select Distinct Bank_Name from Treasury_Balance_MV "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            bankName_Combo.DataSource = C.Dt
            bankName_Combo.DisplayMember = "Bank_Name"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            Dim sql As String = "Select P_ID,PAYMENT_NAME AS PAYMENT_NAME from PAYMENT_METHOD "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            If C.Dt.Rows.Count > 0 Then
                payment_Type_combo.DataSource = C.Dt
                payment_Type_combo.DisplayMember = "PAYMENT_NAME"
                payment_Type_combo.ValueMember = "P_ID"
                payment_Type_combo.SelectedIndex = 0
            Else
                sql = "Select '1' AS P_ID,'نقدا' AS PAYMENT_NAME "
                C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
                C.Da.Fill(C.Dt)
                payment_Type_combo.DataSource = C.Dt
                payment_Type_combo.DisplayMember = "PAYMENT_NAME"
                payment_Type_combo.ValueMember = "P_ID"
                payment_Type_combo.SelectedIndex = 0
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        If ReceiptContextType() = 1 AndAlso Application.OpenForms().OfType(Of Sales).Any Then
            If F_Sales.IS_Show_Rctp = True Then

                Select_Receipt(F_Sales.ReceiptsMetroGrid.CurrentRow.Cells("Receipt_T_ID_CL").Value)
                Exit Sub
            End If
        End If


        If ReceiptContextType() = 0 Then
            '--------------------------------------------------------------------------------------------
        ElseIf ReceiptContextType() = 1 Then
            If isOrder = True Then
                new_butt.Enabled = False
                save_butt.Enabled = True
                DeleteButton.Enabled = False
                Rct_Move_Panel.Enabled = False
            Else

                If Application.OpenForms().OfType(Of Sales).Any = True Then
                    If F_Sales.isDepended = True Then
                        With F_Sales
                            money_num_txtb.Select()
                            new_butt.Enabled = False
                            save_butt.Enabled = True
                            DeleteButton.Enabled = False
                            Rct_Move_Panel.Enabled = False
                        End With
                    End If

                Else
                    If Sales_Fast.isDepended = True Then
                        money_num_txtb.Select()
                        new_butt.Enabled = False
                        save_butt.Enabled = True
                        DeleteButton.Enabled = False
                        Rct_Move_Panel.Enabled = False
                    End If
                End If

            End If
            '--------------------------------------------------------------------------------------------
        ElseIf ReceiptContextType() = 2 Then
            With F_Pch
                If .isShowingDetails = True Then
                    Select_Receipt(.ReceiptsMetroGrid.CurrentRow.Cells("Receipt_T_ID_CL").Value)
                    Exit Sub
                Else
                    money_num_txtb.Select()
                    new_butt.Enabled = False
                    save_butt.Enabled = True
                    If .T_Other_Cr_TXT.Visible = True Then money_num_txtb.Clear()

                    DeleteButton.Enabled = False
                    Rct_Move_Panel.Enabled = False

                End If
            End With

        ElseIf ReceiptContextType() = 8 Then
            With F_EXP_Details
                If .isShowingDetails = True Then
                    Select_Receipt(.ReceiptsMetroGrid.CurrentRow.Cells("Receipt_T_ID_CL").Value)
                    Exit Sub
                Else
                    new_butt.Enabled = False
                    save_butt.Enabled = True
                    money_num_txtb.Select()
                    DeleteButton.Enabled = False
                    Rct_Move_Panel.Enabled = False

                End If
            End With

        ElseIf ReceiptContextType() = 12 Then
            With F_Custody
                new_butt.Enabled = False
                save_butt.Enabled = True
                money_num_txtb.Select()
                DeleteButton.Enabled = False
                Rct_Move_Panel.Enabled = False
            End With

        End If


        If isShowing_Trans = True Then Select_Receipt(T_ID_Trans)


    End Sub


    Private Sub LockReceiptUntilReload()
        ' Also protects direct action calls while controls are only partly populated.
        receiptResultNeedsReload = True
        Is_ComandSuccess = False
        On_Update = False
        Reciept_T_ID = 0
        ReceiptNum = 0
        loadedReceiptType = 0
        receiptLinkedTransactionId = 0
        Fields_Panel.Enabled = False
        save_butt.Enabled = False
        Edit_butt.Enabled = False
        DeleteButton.Enabled = False
        print_butt.Enabled = False
        new_butt.Enabled = False
    End Sub

    Public Sub Select_Receipt(T_ID As Integer)
        If receiptOperationInProgress Then Return
        LockReceiptUntilReload()
        Try
            If T_ID <= 0 Then Throw New ArgumentException("رقم الإيصال المطلوب غير صالح.")
            Dim table As New DataTable
            Using connection As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
                Using command As New SqlClient.SqlCommand("SELECT * FROM dbo.Receipts_V WHERE T_ID=@T_ID", connection)
                    command.Parameters.Add("@T_ID", SqlDbType.Int).Value = T_ID
                    connection.Open()
                    Using reader = command.ExecuteReader()
                        table.Load(reader)
                    End Using
                End Using
            End Using
            If table.Rows.Count = 0 Then Throw New InvalidOperationException("لم يتم العثور على الإيصال؛ لم تُفتح عمليات الحفظ أو التعديل.")

            ' Validate the complete row before touching the displayed receipt.
            Dim row = table.Rows(0)
            Dim number = Convert.ToInt32(row("Receipt_Num"))
            Dim linkedId = If(IsDBNull(row("Receipt_Tran_ID")), 0, Convert.ToInt32(row("Receipt_Tran_ID")))
            Dim kind = Convert.ToInt32(row("BsType_ID")), agent = Convert.ToInt32(row("AG_ID"))
            Dim payment = Convert.ToInt32(row("Pay_ID")), treasury = Convert.ToInt32(row("Tr_ID"))
            Dim receiptDate = Convert.ToDateTime(row("Date"))
            Dim amount = Convert.ToDecimal(row("Value"))
            Dim discount = If(IsDBNull(row("Discount")), 0D, Convert.ToDecimal(row("Discount")))
            Dim isVoid = Convert.ToInt32(row("isVoid")) = 1
            Dim title = Convert.ToString(row("Receipt_Title")), notes = Convert.ToString(row("About"))
            Dim bank = Convert.ToString(row("Bank_Name")), cheque = Convert.ToString(row("CheckNum"))
            If number <= 0 OrElse linkedId < 0 OrElse kind <= 0 OrElse agent <= 0 OrElse payment <= 0 OrElse treasury <= 0 Then
                Throw New InvalidOperationException("بيانات الإيصال أو مراجعه غير مكتملة.")
            End If

            ReceiptNum_Txt.Text = S_Sub_Code & number.ToString(CultureInfo.CurrentCulture)
            AG_Cm.Set_IM_By_ID(agent)
            ReceiptTypeComboBox.SelectedValue = kind
            DateTimeReceipt.Value = receiptDate
            Receipt_Title_combobox.Text = title
            Notes_txtb.Text = notes
            Discount_txt.Text = discount.ToString(CultureInfo.CurrentCulture)
            payment_Type_combo.SelectedIndex = If(String.IsNullOrWhiteSpace(bank), 0, 1)
            If Not String.IsNullOrWhiteSpace(bank) Then
                bankName_Combo.Text = bank
                CheckNum_txtb.Text = cheque
            End If
            money_num_txtb.Text = Math.Abs(amount).ToString(CultureInfo.CurrentCulture)
            payment_Type_combo.SelectedValue = payment
            Treasury_ComboBox.SelectedValue = treasury
            Dim shownId As Integer
            If Not Integer.TryParse(AG_Cm.TXT_ID.Text, shownId) OrElse shownId <> agent OrElse AG_ID <> agent OrElse
                Not TrySelectedReceiptId(ReceiptTypeComboBox, shownId) OrElse shownId <> kind OrElse
                Not TrySelectedReceiptId(payment_Type_combo, shownId) OrElse shownId <> payment OrElse
                Not TrySelectedReceiptId(Treasury_ComboBox, shownId) OrElse shownId <> treasury Then
                Throw New InvalidOperationException("تعذر عرض حساب أو نوع أو خزينة أو طريقة دفع الإيصال بصورة مكتملة.")
            End If
            Void_Lb.Visible = isVoid
            If isVoid Then Void_Lb.BackColor = Color.Red
            Edit_butt.Text = EditState
            Edit_butt.BackColor = Color.White

            ' Publish identity only after every field and selection was successfully populated.
            Reciept_T_ID = T_ID
            ReceiptNum = number
            loadedReceiptType = kind
            receiptLinkedTransactionId = linkedId
            Receipt_Tran_ID = linkedId
            print_butt.Enabled = Not isVoid
            Edit_butt.Enabled = Not isVoid
            DeleteButton.Enabled = Not isVoid
            receiptResultNeedsReload = False
        Catch ex As Exception
            LockReceiptUntilReload()
            MsgBox("تعذر تحميل الإيصال بالكامل. أعد البحث عنه أو أعد فتح الشاشة؛ عمليات الحفظ والتعديل والإلغاء مقفلة. " & ex.Message, MsgBoxStyle.Exclamation, "لم يكتمل تحميل الإيصال")
        End Try
    End Sub

    Private Sub money_num_txtb_KeyDown(sender As Object, e As KeyEventArgs) Handles money_num_txtb.KeyDown
        If e.KeyCode = Keys.Return Then
            Treasury_ComboBox.Select()
            Treasury_ComboBox.DroppedDown = True
        End If
    End Sub

    Private Sub money_num_txtb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles money_num_txtb.KeyPress
        Check_Only_Float(sender, e)
    End Sub


    Private Sub money_num_txtb_TextChanged(sender As Object, e As EventArgs) Handles money_num_txtb.TextChanged
        Check_Point_in_FloatNum(sender, e)
        Me.money_char_txtb.Text = HANY(Val(money_num_txtb.Text), "EGYPT")
    End Sub

    Private Sub payment_Type_combo_KeyDown(sender As Object, e As KeyEventArgs) Handles payment_Type_combo.KeyDown
        If e.KeyCode = Keys.Return Then
            If payment_Type_combo.SelectedIndex = 1 Then
                bankName_Combo.Select()
                bankName_Combo.DroppedDown = True
            Else
                Notes_txtb.Select()
            End If
        End If
    End Sub

    Private Sub payment_method_combo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles payment_Type_combo.SelectedIndexChanged
        If payment_Type_combo.SelectedIndex = 0 Then
            BankPanel.Enabled = False
            CheckNum_txtb.Clear()
            bankName_Combo.SelectedIndex = -1

        Else
            BankPanel.Enabled = True
        End If
    End Sub

    Private Sub save_butt_Click(sender As Object, e As EventArgs) Handles save_butt.Click
        If receiptOperationInProgress OrElse Not save_butt.Enabled OrElse On_Update Then Return
        Receipt_Insert()
    End Sub

    Friend Sub ConfigureEntryContext(kind As Integer, linkedId As Integer)
        If receiptContextCaptured Then Throw New InvalidOperationException("لا يمكن تغيير مصدر الإيصال بعد فتحه.")
        If kind <> 0 AndAlso kind <> 1 AndAlso kind <> 2 AndAlso kind <> 8 Then Throw New ArgumentException("مصدر الإيصال العادي غير معتمد.")
        If linkedId < 0 Then Throw New ArgumentException("رقم المستند المرتبط غير صالح.")
        receiptEntryFormType = kind
        receiptLinkedTransactionId = linkedId
        receiptDeferredCustodyMode = False
    End Sub

    Friend Sub ConfigureDeferredCustodyReceipt(owner As Custody)
        If receiptContextCaptured Then Throw New InvalidOperationException("لا يمكن تغيير مصدر الإيصال بعد فتحه.")
        If owner Is Nothing OrElse owner.IsDisposed OrElse Not Application.OpenForms.Cast(Of Form)().Any(Function(form) Object.ReferenceEquals(form, owner)) Then
            Throw New InvalidOperationException("مسار العهد المؤجل يتطلب شاشة العهد المفتوحة نفسها.")
        End If
        receiptEntryFormType = 12
        receiptDeferredCustodyMode = True
        ' Keep the deferred custody path's existing link and closing behavior unchanged.
        receiptLinkedTransactionId = Receipt_Tran_ID
    End Sub

    Private Function ReceiptContextType() As Integer
        ' An unconfigured instance is an ordinary receipt; never infer custody from global FormType.
        If receiptEntryFormType = 12 AndAlso Not receiptDeferredCustodyMode Then Throw New InvalidOperationException("مصدر إيصال العهد غير مثبت.")
        receiptContextCaptured = True
        Return receiptEntryFormType
    End Function

    Private Function TryBeginReceiptOperation() As Boolean
        If receiptOperationInProgress OrElse receiptResultNeedsReload Then Return False
        receiptOperationInProgress = True
        Is_ComandSuccess = False
        UseWaitCursor = True
        Return True
    End Function

    Private Sub EndReceiptOperation()
        receiptOperationInProgress = False
        UseWaitCursor = False
    End Sub

    Private Shared Function TrySelectedReceiptId(combo As ComboBox, ByRef value As Integer) As Boolean
        value = 0
        If combo Is Nothing OrElse combo.SelectedValue Is Nothing OrElse IsDBNull(combo.SelectedValue) OrElse TypeOf combo.SelectedValue Is DataRowView Then Return False
        Return Integer.TryParse(Convert.ToString(combo.SelectedValue, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, value) AndAlso value > 0
    End Function

    Private Function Preper_To_Insert() As Integer
        Dim selectedAgent As Integer, amount As Decimal, discount As Decimal
        Dim kind As Integer, treasury As Integer, payment As Integer
        If AG_Cm.TXT_ID Is Nothing OrElse Not Integer.TryParse(AG_Cm.TXT_ID.Text, selectedAgent) OrElse selectedAgent <= 0 OrElse selectedAgent <> AG_ID Then
            MsgBox("الرجاء التأكد من اختيار الحساب من القائمة", MsgBoxStyle.Exclamation, "خطأ في الحفظ")
            AG_Cm.Focus() : Return 0
        End If
        If Not Decimal.TryParse(money_num_txtb.Text, NumberStyles.Number, CultureInfo.CurrentCulture, amount) OrElse amount = 0D Then
            MsgBox("أدخل قيمة صحيحة وغير صفرية للمعاملة", MsgBoxStyle.Exclamation, "خطأ في الحفظ")
            money_num_txtb.Focus() : Return 0
        End If
        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Discount_txt.Text = "0"
        If Not Decimal.TryParse(Discount_txt.Text, NumberStyles.Number, CultureInfo.CurrentCulture, discount) Then
            MsgBox("أدخل قيمة صحيحة للخصم", MsgBoxStyle.Exclamation, "خطأ في الحفظ")
            Discount_txt.Focus() : Return 0
        End If
        If Not TrySelectedReceiptId(ReceiptTypeComboBox, kind) OrElse (ReceiptContextType() <> 12 AndAlso kind <> 3 AndAlso kind <> 4) Then
            MsgBox("حدد نوع الإيصال: قبض أو صرف", MsgBoxStyle.Exclamation, "خطأ في الحفظ") : Return 0
        End If
        If On_Update AndAlso ReceiptContextType() <> 12 AndAlso (loadedReceiptType = 0 OrElse loadedReceiptType <> kind) Then
            MsgBox("لا يمكن تغيير نوع إيصال محفوظ أثناء تعديله؛ أعد تحميل الإيصال", MsgBoxStyle.Exclamation, "خطأ في التعديل") : Return 0
        End If
        If Not TrySelectedReceiptId(Treasury_ComboBox, treasury) OrElse Not TrySelectedReceiptId(payment_Type_combo, payment) Then
            MsgBox("حدد خزينة وطريقة دفع صحيحتين من القائمة", MsgBoxStyle.Exclamation, "خطأ في الحفظ") : Return 0
        End If
        Return 1
    End Function

    Private Function BuildReceiptSaveCommand(updating As Boolean) As SqlClient.SqlCommand
        Dim amount As Decimal, discount As Decimal, kind As Integer, treasury As Integer, payment As Integer
        If Not Decimal.TryParse(money_num_txtb.Text, NumberStyles.Number, CultureInfo.CurrentCulture, amount) OrElse
            Not Decimal.TryParse(Discount_txt.Text, NumberStyles.Number, CultureInfo.CurrentCulture, discount) OrElse
            Not TrySelectedReceiptId(ReceiptTypeComboBox, kind) OrElse Not TrySelectedReceiptId(Treasury_ComboBox, treasury) OrElse
            Not TrySelectedReceiptId(payment_Type_combo, payment) Then Throw New InvalidOperationException("تغيرت قيم الإيصال بعد التحقق منها.")
        Dim command As New SqlClient.SqlCommand(If(updating, "Agents_BalanceMV_Update_RCT", "Agents_BalanceMV_insert_RCT"))
        command.CommandType = CommandType.StoredProcedure
        command.Parameters.Add("@T_ID", SqlDbType.Int).Value = If(updating, Reciept_T_ID, 0)
        If Not updating Then
            command.Parameters("@T_ID").Direction = ParameterDirection.Output
            command.Parameters.Add("@Receipt_Num", SqlDbType.Int).Value = 0
            command.Parameters("@Receipt_Num").Direction = ParameterDirection.Output
        End If
        If S_Pr AndAlso Pr_ID > 0 Then command.Parameters.Add("@Pr_ID", SqlDbType.Int).Value = Pr_ID
        command.Parameters.Add("@Receipt_Tran_ID", SqlDbType.Int).Value = receiptLinkedTransactionId
        command.Parameters.Add("@AG_ID", SqlDbType.Int).Value = AG_ID
        command.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTimeReceipt.Value
        command.Parameters.AddWithValue("@Receipt_Title", Receipt_Title_combobox.Text)
        command.Parameters.AddWithValue("@Pure", amount)
        command.Parameters.AddWithValue("@Discount", discount)
        command.Parameters.AddWithValue("@About", Notes_txtb.Text)
        command.Parameters.Add("@BsType_ID", SqlDbType.Int).Value = If(updating, loadedReceiptType, kind)
        If payment_Type_combo.SelectedIndex = 1 Then
            command.Parameters.AddWithValue("@Bank_Name", bankName_Combo.Text)
            command.Parameters.AddWithValue("@CheckNum", CheckNum_txtb.Text)
        End If
        command.Parameters.Add("@User_ID", SqlDbType.Int).Value = USER_ID
        command.Parameters.Add("@Tr_ID", SqlDbType.Int).Value = treasury
        command.Parameters.Add("@Pay_ID", SqlDbType.Int).Value = payment
        Return command
    End Function

    Private Function BuildReceiptStateCommand(procedureName As String, id As Integer) As SqlClient.SqlCommand
        If id <= 0 Then Throw New InvalidOperationException("حدد إيصالًا محفوظًا أولًا.")
        Dim command As New SqlClient.SqlCommand(procedureName)
        command.CommandType = CommandType.StoredProcedure
        command.Parameters.Add("@T_ID", SqlDbType.Int).Value = id
        If procedureName = "Agents_Balance_MV_RCT_OpenForEdit" Then command.Parameters.Add("@User_ID", SqlDbType.Int).Value = USER_ID
        Return command
    End Function

    Private Function RecordAuditedReceiptSuccess(savedId As Integer, savedNumber As Integer) As Boolean
        ' Run=True means committed: record identity/result before any control event or database refresh.
        Reciept_T_ID = savedId
        ReceiptNum = savedNumber
        Is_ComandSuccess = True
        receiptResultNeedsReload = savedId <= 0 OrElse savedNumber <= 0
        If receiptResultNeedsReload Then
            On_Update = False
            Me.save_butt.Enabled = False
            Me.new_butt.Enabled = False
            Me.Edit_butt.Enabled = False
            Me.DeleteButton.Enabled = False
            Me.print_butt.Enabled = False
            Me.Fields_Panel.Enabled = False
            MsgBox("تمت العملية وتأكد حفظها، لكن تعذر استرجاع رقم الإيصال. لا تكرر الحفظ. ابحث عن الإيصال أو أعد فتح الشاشة للتحقق منه.", MsgBoxStyle.Exclamation, "تم التنفيذ — يلزم إعادة التحميل")
            Return False
        End If
        Return True
    End Function

    Private Sub RefreshReceiptBalancesAndParent()
        Current_QTY.Text = Show_AG_T_Balance(AG_ID)
        Dim treasury As Integer
        If TrySelectedReceiptId(Treasury_ComboBox, treasury) Then Treasury_Balance.Text = Show_TR_T_Balance(treasury)
        If ReceiptContextType() = 1 AndAlso Application.OpenForms().OfType(Of Sales).Any Then Select_Sales_Receipt(F_Sales.T_ID)
        If ReceiptContextType() = 2 AndAlso Application.OpenForms().OfType(Of Pch).Any Then Select_Pch_Receipt(F_Pch.T_ID)
        If ReceiptContextType() = 8 AndAlso Application.OpenForms().OfType(Of EXP_Details).Any Then Select_EX_Receipt(F_EXP_Details.T_ID)
    End Sub

    Private Sub FinishAuditedReceiptSave()
        On_Update = False
        After_Save_Receipt()
        ReceiptTypeComboBox.Enabled = False
        Edit_butt.Text = EditState
        Edit_butt.BackColor = Color.White
        AuditReceiptUi.Refresh(Me, Sub()
                                     ReceiptNum_Txt.Text = ReceiptNum.ToString(CultureInfo.CurrentCulture)
                                     RefreshReceiptBalancesAndParent()
                                 End Sub)
        MsgBox("تم حفظ الإيصال", MsgBoxStyle.Information)
    End Sub

    Private Sub ReportReceiptOperationError(ex As Exception)
        If Is_ComandSuccess Then
            MsgBox("تمت العملية، لكن تعذر تحديث الواجهة. لا تكرر الحفظ؛ أعد تحميل الإيصال. " & ex.Message, MsgBoxStyle.Exclamation, "تم التنفيذ")
        Else
            MsgBox(ex.Message, MsgBoxStyle.Critical, "لم تتم عملية الإيصال")
        End If
    End Sub

    Public Sub Receipt_Insert()
        If receiptOperationInProgress OrElse receiptResultNeedsReload OrElse On_Update OrElse Is_ComandSuccess OrElse Not save_butt.Enabled Then Return
        If Not TryBeginReceiptOperation() Then Return
        Try
            If Preper_To_Insert() <> 1 Then Return
            If ReceiptContextType() = 12 Then
                LegacyCustodyReceiptInsert() : Return
            End If
            Dim savedId As Integer, savedNumber As Integer, kind As Integer
            Using command = BuildReceiptSaveCommand(False)
                kind = CInt(command.Parameters("@BsType_ID").Value)
                If Not AuditReceiptUi.Run(Me, command, savedId, savedNumber) Then Return
            End Using
            If Not RecordAuditedReceiptSuccess(savedId, savedNumber) Then Return
            loadedReceiptType = kind
            FinishAuditedReceiptSave()
        Catch ex As Exception
            ReportReceiptOperationError(ex)
        Finally
            EndReceiptOperation()
        End Try
    End Sub

    Public Function Receipt_UPDATE() As Boolean
        If receiptOperationInProgress OrElse Not On_Update OrElse Reciept_T_ID <= 0 Then Return False
        If Not TryBeginReceiptOperation() Then Return False
        Try
            If Preper_To_Insert() <> 1 Then Return False
            If ReceiptContextType() = 12 Then Return LegacyCustodyReceiptUpdate()
            Dim savedId As Integer, savedNumber As Integer
            Using command = BuildReceiptSaveCommand(True)
                If Not AuditReceiptUi.Run(Me, command, savedId, savedNumber) Then Return False
            End Using
            If Not RecordAuditedReceiptSuccess(savedId, savedNumber) Then Return True
            ' Existing update semantics: do not call ConfirmEditAndRepost here.
            FinishAuditedReceiptSave()
            Return True
        Catch ex As Exception
            ReportReceiptOperationError(ex)
            Return Is_ComandSuccess
        Finally
            EndReceiptOperation()
        End Try
    End Function

    Private Function OpenReceiptForEdit() As Boolean
        If receiptOperationInProgress OrElse Reciept_T_ID <= 0 Then Return False
        If Not TryBeginReceiptOperation() Then Return False
        Try
            If ReceiptContextType() = 12 Then Return LegacyCustodyOpenReceiptForEdit()
            If loadedReceiptType <> 3 AndAlso loadedReceiptType <> 4 Then Throw New InvalidOperationException("نوع الإيصال المحمّل غير صالح؛ أعد تحميله.")
            Dim savedId As Integer, savedNumber As Integer
            Using command = BuildReceiptStateCommand("Agents_Balance_MV_RCT_OpenForEdit", Reciept_T_ID)
                If Not AuditReceiptUi.Run(Me, command, savedId, savedNumber) Then Return False
            End Using
            If Not RecordAuditedReceiptSuccess(savedId, savedNumber) Then Return False
            Return True
        Catch ex As Exception
            ReportReceiptOperationError(ex)
            Return Is_ComandSuccess AndAlso Not receiptResultNeedsReload
        Finally
            EndReceiptOperation()
        End Try
    End Function

    Public Sub Agents_Balance_MV_RCT_VOID(T_ID As Integer)
        If receiptOperationInProgress OrElse receiptResultNeedsReload OrElse Void_Lb.Visible OrElse T_ID <= 0 OrElse T_ID <> Reciept_T_ID Then Return
        If Not TryBeginReceiptOperation() Then Return
        Try
            If ReceiptContextType() = 12 Then
                LegacyCustodyReceiptVoid(T_ID) : Return
            End If
            Dim savedId As Integer, savedNumber As Integer
            Using command = BuildReceiptStateCommand("Agents_Balance_MV_RCT_VOID", T_ID)
                If Not AuditReceiptUi.Run(Me, command, savedId, savedNumber) Then Return
            End Using
            If Not RecordAuditedReceiptSuccess(savedId, savedNumber) Then Return
            On_Update = False
            Me.Fields_Panel.Enabled = False
            Me.save_butt.Enabled = False
            Me.print_butt.Enabled = False
            Me.Edit_butt.Enabled = False
            Me.DeleteButton.Enabled = False
            Me.Void_Lb.Visible = True
            Me.Void_Lb.BackColor = Color.Red
            AuditReceiptUi.Refresh(Me, Sub()
                                         Me.ReceiptNum_Txt.Text = ReceiptNum.ToString(CultureInfo.CurrentCulture)
                                         RefreshReceiptBalancesAndParent()
                                     End Sub)
            MsgBox("تم إلغاء المعاملة", MsgBoxStyle.Information)
        Catch ex As Exception
            ReportReceiptOperationError(ex)
        Finally
            EndReceiptOperation()
        End Try
    End Sub


    Private Sub LegacyCustodyReceiptInsert()
        If ReceiptContextType() <> 12 Then Throw New InvalidOperationException("مسار الإيصال القديم مخصص للعهد المؤجلة فقط.")

        Dim sqlComm As New SqlClient.SqlCommand()

        sqlComm.CommandText = "Agents_BalanceMV_insert_RCT"
        sqlComm.Parameters.AddWithValue("@Receipt_Num", 0)

        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", 0)

        If S_Pr = True And Pr_ID > 0 Then sqlComm.Parameters.AddWithValue("@Pr_ID", Pr_ID)
        sqlComm.Parameters.AddWithValue("@Receipt_Tran_ID", Receipt_Tran_ID)

        sqlComm.Parameters.AddWithValue("@AG_ID", Me.AG_ID)
        sqlComm.Parameters.AddWithValue("Date", Me.DateTimeReceipt.Value)
        sqlComm.Parameters.AddWithValue("@Receipt_Title", Me.Receipt_Title_combobox.Text)

        sqlComm.Parameters.AddWithValue("@Pure", Convert.ToDouble(Me.money_num_txtb.Text))

        sqlComm.Parameters.AddWithValue("@Discount", Convert.ToDouble(Me.Discount_txt.Text))

        sqlComm.Parameters.AddWithValue("@About", Notes_txtb.Text)
        sqlComm.Parameters.AddWithValue("@BsType_ID", Me.ReceiptTypeComboBox.SelectedValue)

        If payment_Type_combo.SelectedIndex = 1 Then
            sqlComm.Parameters.AddWithValue("@Bank_Name", bankName_Combo.Text)
            sqlComm.Parameters.AddWithValue("@CheckNum", CheckNum_txtb.Text)
        End If
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters.AddWithValue("@Tr_ID", Treasury_ComboBox.SelectedValue)
        sqlComm.Parameters.AddWithValue("@Pay_ID", payment_Type_combo.SelectedValue)

        sqlComm.Parameters("@Receipt_Num").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then

            MsgBox("تم حفظ الإيصــال", MsgBoxStyle.Information)
            Reciept_T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
            ReceiptNum = sqlComm.Parameters("@Receipt_Num").Value.ToString()

            'Reload_Balance()
            Current_QTY.Text = Show_AG_T_Balance(AG_ID)

            Is_ComandSuccess = True

            '  If On_Update = False Then
            ReceiptNum_Txt.Text = sqlComm.Parameters("@Receipt_Num").Value.ToString()
            Network_Edit_Tracker_insert(" إيصال للحساب " & AG_Cm.Textt & " بقيمة : " & money_num_txtb.Text, ReceiptNum_Txt.Text, AG_Type, 1)
            'Else
            '    ReceiptNum_Txt.Text = ReceiptNum
            '    Network_Edit_Tracker_insert(" إيصال للحساب " & AG_Cm.Textt & " بقيمة : " & money_num_txtb.Text, ReceiptNum_Txt.Text, AG_Type, 3)
            'End If

            If ReceiptContextType() = 12 Then If Application.OpenForms().OfType(Of Custody).Any Then F_Custody.Custody_CLOSE()
            If ReceiptContextType() = 1 Then If Application.OpenForms().OfType(Of Sales).Any Then Select_Sales_Receipt(F_Sales.T_ID)
            If ReceiptContextType() = 2 Then If Application.OpenForms().OfType(Of Pch).Any Then Select_Pch_Receipt(F_Pch.T_ID)
            If ReceiptContextType() = 8 Then If Application.OpenForms().OfType(Of EXP_Details).Any Then Select_EX_Receipt(F_EXP_Details.T_ID)

            Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
            After_Save_Receipt()

            'If Application.OpenForms().OfType(Of SearchAgentBill).Any Then SearchAgentBill.Load_Data()
        End If

    End Sub


    Private Function LegacyCustodyReceiptUpdate() As Boolean
        If ReceiptContextType() <> 12 Then Throw New InvalidOperationException("مسار الإيصال القديم مخصص للعهد المؤجلة فقط.")

        Try

            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)

                cn.Open()

                Using tr As SqlClient.SqlTransaction = cn.BeginTransaction()

                    Try

                        Using sqlComm As New SqlClient.SqlCommand("[Agents_BalanceMV_Update_RCT]", cn, tr)

                            sqlComm.CommandType = CommandType.StoredProcedure
                            sqlComm.Parameters.AddWithValue("@T_ID", Reciept_T_ID)

                            If S_Pr = True And Pr_ID > 0 Then sqlComm.Parameters.AddWithValue("@Pr_ID", Pr_ID)
                            sqlComm.Parameters.AddWithValue("@Receipt_Tran_ID", Receipt_Tran_ID)

                            sqlComm.Parameters.AddWithValue("@AG_ID", Me.AG_ID)
                            sqlComm.Parameters.AddWithValue("Date", Me.DateTimeReceipt.Value)
                            sqlComm.Parameters.AddWithValue("@Receipt_Title", Me.Receipt_Title_combobox.Text)

                            sqlComm.Parameters.AddWithValue("@Pure", Convert.ToDouble(Me.money_num_txtb.Text))

                            sqlComm.Parameters.AddWithValue("@About", Notes_txtb.Text)
                            sqlComm.Parameters.AddWithValue("@BsType_ID", Me.ReceiptTypeComboBox.SelectedValue)
                            If payment_Type_combo.SelectedIndex = 1 Then
                                sqlComm.Parameters.AddWithValue("@Bank_Name", bankName_Combo.Text)
                                sqlComm.Parameters.AddWithValue("@CheckNum", CheckNum_txtb.Text)
                            End If
                            sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
                            sqlComm.Parameters.AddWithValue("@Tr_ID", Treasury_ComboBox.SelectedValue)
                            sqlComm.Parameters.AddWithValue("@Pay_ID", payment_Type_combo.SelectedValue)

                            sqlComm.Parameters.AddWithValue("@Discount", Convert.ToDouble(Me.Discount_txt.Text))


                            sqlComm.ExecuteNonQuery()

                        End Using

                        'ExecuteReceiptStoredProcedure(cn, tr, "dbo.Agents_Balance_MV_RCT_ConfirmEditAndRepost",
                        '    Sub(cmd)
                        '        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = Reciept_T_ID
                        '        cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = USER_ID
                        '        cmd.Parameters.Add("@Reason", SqlDbType.NVarChar, 500).Value = "تعديل إيصال بعد التقييد"
                        '    End Sub)

                        LegacyCustodyExecuteStoredProcedure(cn, tr, "Network_Edit_Tracker_insert",
                            Sub(cmd)
                                cmd.Parameters.AddWithValue("@User_ID", USER_ID)
                                cmd.Parameters.AddWithValue("@Notes", " إيصال للحساب " & AG_Cm.Textt & " بقيمة : " & money_num_txtb.Text)
                                cmd.Parameters.AddWithValue("@Bill_ID", ReceiptNum_Txt.Text)
                                cmd.Parameters.AddWithValue("@Screen_Type", AG_Type)
                                cmd.Parameters.AddWithValue("@Operation_ID", 3)
                                cmd.Parameters.AddWithValue("@CP_Name", My.Computer.Name)
                            End Sub)

                        tr.Commit()

                    Catch

                        tr.Rollback()
                        Throw

                    End Try

                End Using

            End Using

            MsgBox("تم حفظ الإيصــال", MsgBoxStyle.Information)

            Current_QTY.Text = Show_AG_T_Balance(AG_ID)
            Is_ComandSuccess = True
            ReceiptNum_Txt.Text = ReceiptNum

            If ReceiptContextType() = 12 Then If Application.OpenForms().OfType(Of Custody).Any Then F_Custody.Custody_CLOSE()
            If ReceiptContextType() = 1 Then If Application.OpenForms().OfType(Of Sales).Any Then Select_Sales_Receipt(F_Sales.T_ID)
            If ReceiptContextType() = 2 Then If Application.OpenForms().OfType(Of Pch).Any Then Select_Pch_Receipt(F_Pch.T_ID)
            If ReceiptContextType() = 8 Then If Application.OpenForms().OfType(Of EXP_Details).Any Then Select_EX_Receipt(F_EXP_Details.T_ID)

            Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
            After_Save_Receipt()

            Edit_butt.Text = EditState
            Edit_butt.BackColor = Color.White
            On_Update = False

            Return True

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "خطأ في حفظ تعديل الإيصال")
            Return False

        End Try

    End Function

    Private Sub LegacyCustodyExecuteStoredProcedure(cn As SqlClient.SqlConnection, tr As SqlClient.SqlTransaction, procedureName As String, addParameters As Action(Of SqlClient.SqlCommand))
        If ReceiptContextType() <> 12 Then Throw New InvalidOperationException("هذا التنفيذ القديم مخصص للعهد المؤجلة فقط.")

        Using cmd As New SqlClient.SqlCommand(procedureName, cn, tr)

            cmd.CommandType = CommandType.StoredProcedure
            addParameters(cmd)
            cmd.ExecuteNonQuery()

        End Using

    End Sub


    Private Sub print_butt_Click(sender As Object, e As EventArgs) Handles print_butt.Click
        ValidateChildren()
        Print_RECIEPT()
        '    My.Computer.Audio.Play("D:\Music & Images Disk\Music\إيطاليا.wav")
        '    ', AudioPlayMode.WaitToComplete)

    End Sub

    Public Sub Print_RECIEPT()

        Try
            Dim data As ReceiptPrintData = ReceiptPrintData.FromReceiptForm(Me, Show_Treasury_CB.Checked)
            Dim renderer As New ReceiptPrintDocumentRenderer(data, ReceiptPage_ID)

            Using doc As PrintDocument = renderer.CreatePrintDocument()
                AddHandler doc.PrintPage, AddressOf renderer.PrintPage

                Dim printerName As String = If(ReceiptPage_ID = 1, Default_Printer_80, Default_Printer_A4)
                If String.IsNullOrWhiteSpace(printerName) = False Then
                    If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", printerName))
                    doc.PrinterSettings.PrinterName = printerName
                End If

                If Show_Bill_CB.Checked = True Then
                    Using previewDialog As New PrintPreviewDialog()
                        previewDialog.Document = doc
                        previewDialog.WindowState = FormWindowState.Maximized
                        previewDialog.Text = "معاينة الإيصال"
                        previewDialog.ShowDialog(Me)
                    End Using
                Else
                    doc.PrintController = New StandardPrintController()
                    doc.Print()
                End If
            End Using


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub Receipt_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'F_MainForm.Fill_ALL_IM()
        Me.Dispose()
    End Sub

    Private Sub Receipt_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If receiptOperationInProgress Then Return
        If e.KeyCode = Keys.F1 Then If new_butt.Enabled = True Then new_butt_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If save_butt.Enabled = True Then save_butt_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If DeleteButton.Enabled = True Then DeleteButton_Click(sender, e)
        If e.KeyCode = Keys.Escape Then Me.Close()

        If e.KeyCode = Keys.Return Then
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End If
    End Sub

    Private Sub Receipt_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If receiptOperationInProgress Then e.Cancel = True
    End Sub

    Private Sub Receipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReceiptContextType()
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        ThemeManager.ApplyThemeToForm(Me)
        EditState = Edit_butt.Text
        Edit_butt.Visible = U_Update_Receipt
        DeleteButton.Visible = U_Cancel_Receipt
        Get_MAX_T_ID()
      
        Load_Data()
        Title_Lb.Text = "شاشة إدراج : " + ReceiptTypeComboBox.Text
        Title_Lb.BackColor = ReceiptTypeComboBox.BackColor
        If Reciept_T_ID = 0 Then Integer.TryParse(ReceiptNum_Txt.Text, ReceiptNum)

        If AG_ID > 0 Then AG_Cm.Set_IM_By_ID(AG_ID)

        AG_Show_Balance_CB.Checked = MY_Settings.AG_Show_Balance_in_Receipt
        Show_Treasury_CB.Checked = MY_Settings.Show_Treasury_in_Receipt
    End Sub


    Public Sub Get_MAX_T_ID()

        Dim C As New C
        Dim S As String = "Select ISNULL(MAX(Receipt_Num),0) + 1 AS MX From Agents_Balance_MV_RCT"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                If ReceiptContextType() = 0 Then ClearFields()
                ReceiptNum_Txt.Text = C.Dr("MX")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub


    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        If receiptOperationInProgress Then Return
        Tmp_Bill_ID = ReceiptNum
        ReceiptNum_Txt.Text = ReceiptNum - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()
        If receiptOperationInProgress Then Return
        Dim requestedNumber As Integer
        If Not Integer.TryParse(ReceiptNum_Txt.Text, requestedNumber) Then
            If ReceiptNum > 0 AndAlso ReceiptNum_Txt.Text = S_Sub_Code & ReceiptNum.ToString(CultureInfo.CurrentCulture) Then
                requestedNumber = ReceiptNum
            Else
                LockReceiptUntilReload()
                MsgBox("أدخل رقم إيصال صحيحًا للبحث", MsgBoxStyle.Exclamation) : Return
            End If
        End If
        LockReceiptUntilReload()
        If requestedNumber <= 0 Then Return
            Dim C As New C
            Dim S As String = ""
        S = "Select T_ID From Agents_Balance_MV_RCT Where Receipt_Num = '" & requestedNumber.ToString(CultureInfo.InvariantCulture) & "'"

            C.Com = New SqlClient.SqlCommand(S, C.Con)
            Try
                C.Con.Open()
                C.Dr = C.Com.ExecuteReader
                If C.Dr.HasRows Then
                    C.Dr.Read()
                    ClearFields()
                    Select_Receipt(C.Dr("T_ID"))
                    new_butt.Enabled = Not receiptResultNeedsReload
                Else
                    MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                    ReceiptNum_Txt.Text = Tmp_Bill_ID
                End If

            Catch ex As Exception
                LockReceiptUntilReload()
                MsgBox(ex.Message)
            End Try
            C.Con.Close()
    End Sub

    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        If receiptOperationInProgress Then Return
        If Not String.IsNullOrWhiteSpace(ReceiptNum_Txt.Text) Then
            Tmp_Bill_ID = ReceiptNum
            ReceiptNum_Txt.Text = ReceiptNum + 1
            Get_T_ID()
        End If
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    'Private Sub DateTimeReceipt_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeReceipt.KeyDown
    '    If e.KeyCode = Keys.Return Then Receipt_Title_combobox.Select()
    'End Sub

    'Private Sub bankName_Combo_KeyDown(sender As Object, e As KeyEventArgs) Handles bankName_Combo.KeyDown
    '    If e.KeyCode = Keys.Return Then CheckNum_txtb.Select()
    'End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        If receiptOperationInProgress OrElse Not DeleteButton.Enabled OrElse Reciept_T_ID <= 0 Then Return
        Beep()
        If MessageBox.Show("سيتم إلغاء الإيصال والمعاملات المتعلقة به. هل تريد الاستمرار؟", "إلغاء الإيصال", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                       MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then Agents_Balance_MV_RCT_VOID(Reciept_T_ID)
    End Sub

    'Private Sub CheckNum_txtb_KeyDown(sender As Object, e As KeyEventArgs) Handles CheckNum_txtb.KeyDown
    '    If e.KeyCode = Keys.Return Then Notes_txtb.Select()
    'End Sub




    Private Sub LegacyCustodyReceiptVoid(T_ID As Integer)
        If ReceiptContextType() <> 12 Then Throw New InvalidOperationException("مسار الإلغاء القديم مخصص للعهد المؤجلة فقط.")
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "[Agents_Balance_MV_RCT_VOID]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم إلغــاء المعاملة ", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert("إلغاء الإيصال", Me.ReceiptNum_Txt.Text, AG_Type, 3)

            If ReceiptContextType() = 1 Then Select_Sales_Receipt(F_Sales.T_ID)
            If ReceiptContextType() = 2 Then Select_Pch_Receipt(F_Pch.T_ID)
            If ReceiptContextType() = 8 Then Select_EX_Receipt(F_EXP_Details.T_ID)
            Me.Void_Lb.Visible = True
            Me.Void_Lb.BackColor = Color.Red
            Me.Treasury_Balance.Text = Show_TR_T_Balance(Me.Treasury_ComboBox.SelectedValue)
            Me.print_butt.Enabled = False
            Me.Edit_butt.Enabled = False
            Me.DeleteButton.Enabled = False
        End If

    End Sub
    Public Sub Fetch_ItemToList()
        Dim selectedAgent As Integer
        If Not Integer.TryParse(AG_Cm.TXT_ID.Text, selectedAgent) OrElse selectedAgent <= 0 Then Return
        AG_ID = selectedAgent
        Current_QTY.Text = Show_AG_T_Balance(AG_ID)
        Fetch_AG_Currency()
    End Sub


    Public Sub Fetch_AG_Currency()
        'Dim C As New C
        'Dim S As String = "Select Cr_ID,Cr_Equal From AGENTS_MENU_V Where AG_ID = '" & AG_ID & "'"
        'C.Com = New SqlClient.SqlCommand(S, C.Con)
        'C.Con.Open()
        'Try
        '    C.Dr = C.Com.ExecuteReader
        '    If C.Dr.HasRows Then
        '        C.Dr.Read()
        '        Cr_CM.SelectedValue = C.Dr("Cr_ID")
        '        Cr_Equal_TXT.Text = C.Dr("Cr_Equal")

        '        If Cr_CM.SelectedValue = 1 Then
        '            CurrencyPanel.Visible = False
        '        Else
        '            CurrencyPanel.Visible = True
        '        End If

        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        'C.Con.Close()
    End Sub

    'Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
    '    If e.KeyCode = Keys.Return Then If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode()
    '    If e.KeyCode = Keys.Delete Then Barcode_SH_txt.Clear()
    'End Sub

    Public Sub Load_IM_Barcode()
        Dim c As New C
        IM_Dt.Clear()
        Try
            Dim s As String
            s = "select AG_ID,Ag_name,isnull(T_Balance,0) AS T_Balance from Agents WHERE Barcode = '" & Barcode_SH_txt.Text & "'"

            C.Com = New SqlClient.SqlCommand(S, C.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                'AG_ID = c.Dr("AG_ID")
                AG_Cm.Set_IM_By_ID(c.Dr("AG_ID"))
                'IM_SH_txt.Text = c.Dr("Ag_name")
                Current_QTY.Text = c.Dr("T_Balance")
                'IMDataGridViewX.Visible = False
                Fetch_AG_Currency()
            Else
                MsgBox("لم يتم التعرف على الرقم ", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Current_QTY_DoubleClick(sender As Object, e As EventArgs) Handles Current_QTY.DoubleClick
        Dim Num As Double = Convert.ToDouble(Current_QTY.Text)
        If Num < 0 Then Num = Num * -1
        money_num_txtb.Text = Num
    End Sub

    'Private Sub Receipt_Title_combobox_KeyDown_1(sender As Object, e As KeyEventArgs) Handles Receipt_Title_combobox.KeyDown
    '    If e.KeyCode = Keys.Return Then money_num_txtb.Select()
    'End Sub


    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If receiptOperationInProgress OrElse Not Edit_butt.Enabled Then Return
        If Reciept_T_ID > 0 Then
            If On_Update = False Then

                Beep()
                If MessageBox.Show(" تعديل الإيصال ؟ ", "تعديل", MessageBoxButtons.YesNo, _
                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    If OpenReceiptForEdit() = False Then Return

                    Edit_butt.BackColor = Color.Green
                    On_Update = True
                    Fields_Panel.Enabled = True
                    If ReceiptContextType() <> 12 Then ReceiptTypeComboBox.Enabled = False
                    'Show_IM_btn.Enabled = True

                    Edit_butt.Text = "حفظ التعديل"
                    'Network_Edit_Tracker_insert("تعديل " + ReceiptTypeComboBox.Text + " / رقم  : " + ReceiptNum_Txt.Text, money_num_txtb.Text, 0, 0)
                End If
            Else
                If Preper_To_Insert() = 1 Then
                    If Receipt_UPDATE() = False Then Return
                End If

            End If
        End If
    End Sub

    Private Function LegacyCustodyOpenReceiptForEdit() As Boolean
        If ReceiptContextType() <> 12 Then Throw New InvalidOperationException("فتح التعديل القديم مخصص للعهد المؤجلة فقط.")

        Try

            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
                Using cmd As New SqlClient.SqlCommand("dbo.Agents_Balance_MV_RCT_OpenForEdit", cn)

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = Reciept_T_ID
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = USER_ID

                    cn.Open()
                    cmd.ExecuteNonQuery()

                End Using
            End Using

            Return True

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "خطأ في فتح تعديل الإيصال")
            Return False

        End Try

    End Function

   

    Private Sub ReceiptNum_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ReceiptNum_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub ReceiptNum_Txt_Enter(sender As Object, e As EventArgs) Handles ReceiptNum_Txt.Enter
        Tmp_Bill_ID = ReceiptNum
    End Sub

    Private Sub ReceiptNum_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles ReceiptNum_Txt.KeyDown
        If e.KeyCode = Keys.Return Then
            Tmp_Bill_ID = ReceiptNum
            Get_T_ID()
        End If
    End Sub

    Private Sub Show_Bill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Bill_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Show_Treasury_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Treasury_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.Show_Treasury_in_Receipt = Show_Treasury_CB.Checked
        MY_Settings.Save_AppSetting()
    End Sub


    Private Sub إضافةالإيصالإلىقائمةالعهدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إضافةالإيصالإلىقائمةالعهدToolStripMenuItem.Click
        If ReceiptTypeComboBox.SelectedValue = 3 Then
            MsgBox("لا يمكن إجراء عهدة لإيصال قبض", MsgBoxStyle.Exclamation, "")
        Else
            Custody_SELECT()
        End If

    End Sub

    Public Sub Custody_SELECT()
        Dim c As New C
        Try
            Dim s As String
            s = "select *  FROM Custody_V WHERE T_ID = '" & ReceiptNum & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                MsgBox("تم إعتماد الواصل كهعدة", MsgBoxStyle.Information)
            Else
                Custody_INSERT()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Custody_INSERT()

        Dim C As New C
        With C.Com
            .CommandText = "Custody_INSERT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", ReceiptNum)
            .Parameters.AddWithValue("@C_Rcpt_T_ID", ReceiptNum)
            '.Parameters("@T_ID").Direction = ParameterDirection.Output
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            MsgBox("تمت إضافة العهدة", MsgBoxStyle.Information)
            'Network_Edit_Tracker_insert(" العميل:" & IM_SH_txt.Text & " الرقم:" & Barcode_txt.Text & " لهاتف:" & AG_Phone_TextBox.Text & " العنوان:" & AG_AddressTextBox.Text & " البريد الإلكتروني:" _
            '                & Email_txt.Text & " النوع:" & AG_Type_cm.Text & " العملة:" & Cr_CM.Text & " المرتب:" & SalaryTextBox.Text & " إشعار الدين:" & Max_Debit_txt.Text, 0, 27, 1)
        End If

    End Sub

    Private Sub Treasury_ComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles Treasury_ComboBox.KeyDown
        If e.KeyCode = Keys.Return Then
            payment_Type_combo.Select()
            payment_Type_combo.DroppedDown = True
        End If
    End Sub

    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        Dim selectedAgent As Integer
        If Integer.TryParse(AG_Cm.TXT_ID.Text, selectedAgent) AndAlso selectedAgent > 0 Then Fetch_ItemToList()
    End Sub



    Private Sub Treasury_ComboBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles Treasury_ComboBox.SelectedValueChanged
        If TypeName(Treasury_ComboBox.SelectedValue) = "Integer" Then
            Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
            check_hide_tr(Treasury_ComboBox, Treasury_Balance)
        End If
    End Sub

    'Private Sub OnKeyBoard1_UC_Button1Click(sender As Object, e As EventArgs)
    '    Dim BT As New Button
    '    BT = sender

    '    For Each a As Control In Fields_Panel.Controls
    '        If a.Name = KEY_HANDLER Then
    '            a.Focus()
    '            a.Select()

    '            If BT.Name <> "BLANG" And BT.Name <> "BCLEAR" And BT.Name <> "BSPACE" Then

    '                If TypeOf a Is FSearch_Filter Then
    '                    Dim a2 As New FSearch_Filter
    '                    a2.Textt = a.Text
    '                    'a2.Textt += BT.Text
    '                Else
    '                    SendKeys.Send("{" & BT.Text & "}")
    '                End If

    '            ElseIf BT.Name = "BCLEAR" Then
    '                If TypeOf a Is FSearch_Filter Then
    '                    Dim a2 As New FSearch_Filter
    '                    a2.Textt = ""
    '                Else
    '                    a.Text = ""
    '                End If

    '            ElseIf BT.Name = "BSPACE" Then
    '                SendKeys.Send(" ")
    '            End If


    '        End If
    '        'a.Text += BT.Text
    '    Next
    'End Sub

    Private Sub AG_Cm_Enter(sender As Object, e As EventArgs) Handles AG_Cm.Enter, money_num_txtb.Enter, Receipt_Title_combobox.Enter, Notes_txtb.Enter
        KEY_HANDLER = sender.name
    End Sub


    Private Sub AG_Show_Balance_CB_CheckedChanged(sender As Object, e As EventArgs) Handles AG_Show_Balance_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.AG_Show_Balance_in_Receipt = AG_Show_Balance_CB.Checked
        MY_Settings.Save_AppSetting()
    End Sub

    Private Sub payment_Type_combo_SelectedValueChanged(sender As Object, e As EventArgs) Handles payment_Type_combo.SelectedValueChanged
        If TypeName(payment_Type_combo.SelectedValue) = "Integer" Then PaymentMethodDefaultAccounts_SELECT()
    End Sub


    Public Sub PaymentMethodDefaultAccounts_SELECT()
        Treasury_ComboBox.Enabled = True
        Dim c As New C
        Dim Tmp_Tr_ID As Integer = payment_Type_combo.SelectedValue
        Try
            Dim s As String
            s = "select TOP 1 AccountID , isnull(is_Lock,0) as is_Lock FROM PaymentMethodDefaultAccounts WHERE PaymentMethodID = '" & payment_Type_combo.SelectedValue & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Treasury_ComboBox.SelectedValue = c.Dr("AccountID")
                If c.Dr("is_Lock") = True Then Treasury_ComboBox.Enabled = False
            Else
                payment_Type_combo.SelectedValue = Tmp_Tr_ID
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        Me.Close()
    End Sub
End Class
