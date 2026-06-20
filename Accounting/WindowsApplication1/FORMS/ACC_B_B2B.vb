Public Class ACC_B_B2B

    Dim ACC_CODE_DT As New DataTable
    Public ACC_CODE As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub ACC_B_B2B_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ACC_CODE_DT.Clear()
        ACC_CODE_DT = Accounts_Datatable


        SWICH_DEBIT_CREDIT()

        Cost_Center_Control1.Set_CHECK_ALL_VISIBLE(False)

        'DEBIT_B_Cm.DrawMode = DrawMode.OwnerDrawFixed
    End Sub



    'Private Sub ComboBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles DEBIT_B_Cm.DrawItem
    '    If e.Index < 0 Then Return

    '    ' إذا كان العنصر محدد
    '    If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
    '        ' هنا تتحكم بلون الخلفية عند التحديد
    '        e.Graphics.FillRectangle(New SolidBrush(Color.LightGreen), e.Bounds)
    '    Else
    '        ' لون الخلفية العادي
    '        e.Graphics.FillRectangle(New SolidBrush(Color.White), e.Bounds)
    '    End If

    '    ' لون النص
    '    e.Graphics.DrawString(DEBIT_B_Cm.Items(e.Index).ToString(), e.Font, Brushes.Black, e.Bounds)
    '    e.DrawFocusRectangle()
    'End Sub





    Private Sub ORG_B_NUM_txt_TextChanged(sender As Object, e As EventArgs) Handles DEBIT_ACC_CODE_TXT.TextChanged
        If DEBIT_ACC_CODE_TXT.Text.Count > 0 Then
            Filter_B(DEBIT_B_Cm, DEBIT_ACC_CODE_TXT, ACC_CODE_DT)
        Else
            ACC_CODE_DT.Clear()
            ACC_CODE_DT = Accounts_Datatable
        End If

    End Sub


    Private Sub ORG_B_Cm_KeyDown(sender As Object, e As KeyEventArgs) Handles DEBIT_B_Cm.KeyDown
        If e.KeyCode = Keys.Return Then GET_B_DATA(DEBIT_B_Cm, DEBIT_ACC_CODE_TXT)
    End Sub

    Private Sub EXP_B_Cm_KeyDown(sender As Object, e As KeyEventArgs) Handles CREDIT_B_Cm.KeyDown
        If e.KeyCode = Keys.Return Then GET_B_DATA(CREDIT_B_Cm, CREDIT_ACC_CODE_TXT)
    End Sub

    Private Sub ORG_B_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles DEBIT_B_Cm.SelectedValueChanged
        GET_B_DATA(DEBIT_B_Cm, DEBIT_ACC_CODE_TXT)
    End Sub

    Private Sub EXP_B_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles CREDIT_B_Cm.SelectedValueChanged
        GET_B_DATA(CREDIT_B_Cm, CREDIT_ACC_CODE_TXT)
    End Sub

    Private Sub GET_B_DATA(ByRef CM As ComboBox, ByRef TXT As TextBox)
        If TypeName(CM.SelectedValue) = "String" Then
            TXT.Text = CM.SelectedValue
            CM.DroppedDown = False
        End If
    End Sub

    Private Sub EXP_B_NUM_txt_TextChanged(sender As Object, e As EventArgs) Handles CREDIT_ACC_CODE_TXT.TextChanged
        If CREDIT_ACC_CODE_TXT.Text.Count > 0 Then
            Filter_B(CREDIT_B_Cm, CREDIT_ACC_CODE_TXT, ACC_CODE_DT)
        Else
            ACC_CODE_DT.Clear()
            ACC_CODE_DT = Accounts_Datatable
        End If
    End Sub


    Private Sub save_butt_Click(sender As Object, e As EventArgs) Handles save_butt.Click

        If DEBIT_B_Cm.SelectedValue = 0 Then
            Dim notification3 As New NotificationForm("تنويه", " حدد حساب المدين ", "bottom", True)
            notification3.ShowNotification()
            Exit Sub
        End If

        If CREDIT_B_Cm.SelectedValue = 0 Then
            Dim notification3 As New NotificationForm("تنويه", " حدد حساب الدائن ", "bottom", True)
            notification3.ShowNotification()
            Exit Sub
        End If

        If CREDIT_ACC_CODE_TXT.Text <> ACC_CODE And DEBIT_ACC_CODE_TXT.Text <> ACC_CODE Then
            Dim notification3 As New NotificationForm("تنويه", " بجب ان يحتوي القيد على الحساب المعني ", "bottom", True)
            notification3.ShowNotification()
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Amount_txt.Text) Or Convert.ToDouble(Amount_txt.Text) = 0 Then
            Dim notification3 As New NotificationForm("تنويه", " حدد قيمة القيد ", "bottom", True)
            notification3.ShowNotification()
            Exit Sub
        End If

        If CREDIT_ACC_CODE_TXT.Text = DEBIT_ACC_CODE_TXT.Text Then
            Dim notification3 As New NotificationForm("تنويه", " القيد يجب ان يكون مختلف", "bottom", True)
            notification3.ShowNotification()
            Exit Sub
        End If

        If Not ValidateManualJournalAccount(DEBIT_ACC_CODE_TXT.Text, "الحساب المدين") Then Exit Sub
        If Not ValidateManualJournalAccount(CREDIT_ACC_CODE_TXT.Text, "الحساب الدائن") Then Exit Sub
        If Not ValidateUserJournalAccountPermission(DEBIT_ACC_CODE_TXT.Text, "الحساب المدين") Then Exit Sub
        If Not ValidateUserJournalAccountPermission(CREDIT_ACC_CODE_TXT.Text, "الحساب الدائن") Then Exit Sub

        Receipt_Insert()
    End Sub

    Public Sub Receipt_Insert()

        Dim C As New C


        With C.Com
            .Connection = C.Con
            .CommandText = "[ACC_BALANCE_proc_Receipt]"
            .CommandType = CommandType.StoredProcedure

            .Parameters.AddWithValue("@DATE", DateTimeReceipt.Value)
            .Parameters.AddWithValue("@ACC_CODE_FROM", DEBIT_ACC_CODE_TXT.Text)
            .Parameters.AddWithValue("@ACC_CODE_TO", CREDIT_ACC_CODE_TXT.Text)

            .Parameters.AddWithValue("@DEBIT", Amount_txt.Text)
            .Parameters.AddWithValue("@CREDIT", Amount_txt.Text)
            .Parameters.AddWithValue("@USER_ID", USER_ID)
            .Parameters.AddWithValue("@Notes_MASTER", M_Notes_txt.Text)

            .Parameters.AddWithValue("@COST_ID", Cost_Center_Control1.COST_CM.SelectedValue)
            .Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
            .Parameters.AddWithValue("@OP_Status", 1)

            .Parameters.AddWithValue("@Receipt_Type", DBNull.Value)
            .Parameters.AddWithValue("@Receipt_Num", DBNull.Value)
            .Parameters.AddWithValue("@Bank_Name", DBNull.Value)
            .Parameters.AddWithValue("@Check_Number", DBNull.Value)

            .Parameters.AddWithValue("@Cr_ID", 1)
            .Parameters.AddWithValue("@Currency_Equal", 1)


            .Parameters("@OP_Status").Direction = ParameterDirection.Output
            .Parameters("@ERROR_MSG").Direction = ParameterDirection.Output

            C.Con.Open()
            C.Com.ExecuteScalar()
            C.Con.Close()


            If C.Com.Parameters("@OP_Status").Value.ToString() = "0" Then
                MsgBox(C.Com.Parameters("@ERROR_MSG").Value.ToString(), MsgBoxStyle.Critical, "خطــأ")
            Else
                MsgBox(" تم حفظ القيد بنجاح ", MsgBoxStyle.Information, "")

            End If

        End With


    End Sub

    Private Sub Debit_Rd_CheckedChanged(sender As Object, e As EventArgs) Handles Debit_Rd.CheckedChanged
        SWICH_DEBIT_CREDIT()
    End Sub

    Private Sub SWICH_DEBIT_CREDIT()
        If Debit_Rd.Checked Then
            DEBIT_ACC_CODE_TXT.Text = ACC_CODE
            CREDIT_ACC_CODE_TXT.Clear()
            CREDIT_B_Cm.SelectedIndex = -1


            Filter_B(DEBIT_B_Cm, DEBIT_ACC_CODE_TXT, ACC_CODE_DT)

            'DEBIT_GroupBox.Enabled = False
            DEBIT_ACC_CODE_TXT.Enabled = False
            DEBIT_SEARCH_ACC_BTN.Enabled = False


            'CREDIT_GroupBox.Enabled = True
            CREDIT_SEARCH_ACC_BTN.Enabled = True
            CREDIT_ACC_CODE_TXT.Enabled = True

            DEBIT_B_Cm.DroppedDown = False

        Else
            CREDIT_ACC_CODE_TXT.Text = ACC_CODE
            DEBIT_ACC_CODE_TXT.Clear()
            DEBIT_B_Cm.SelectedIndex = -1

            Filter_B(CREDIT_B_Cm, CREDIT_ACC_CODE_TXT, ACC_CODE_DT)


            'CREDIT_GroupBox.Enabled = False
            CREDIT_SEARCH_ACC_BTN.Enabled = False
            CREDIT_ACC_CODE_TXT.Enabled = False

            'DEBIT_GroupBox.Enabled = True
            DEBIT_ACC_CODE_TXT.Enabled = True
            DEBIT_SEARCH_ACC_BTN.Enabled = True


            CREDIT_B_Cm.DroppedDown = False

        End If
    End Sub

    Private Sub Credit_Rd_CheckedChanged(sender As Object, e As EventArgs) Handles Credit_Rd.CheckedChanged
        SWICH_DEBIT_CREDIT()
    End Sub

    Private Sub SEARCH_ACC_BTN_Click(sender As Object, e As EventArgs) Handles DEBIT_SEARCH_ACC_BTN.Click
        ACC_CODE_Search = ""
        BALANCE_SEARCH.ShowDialog()
        DEBIT_ACC_CODE_TXT.Text = ACC_CODE_Search
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles CREDIT_SEARCH_ACC_BTN.Click
        ACC_CODE_Search = ""
        BALANCE_SEARCH.ShowDialog()
        CREDIT_ACC_CODE_TXT.Text = ACC_CODE_Search
    End Sub
End Class
