Public Class Add_Prev_Balance
    Public is_Select As Boolean = False
    Public AG_ID As Integer
    Public AG_NAME As String = ""

    Private Sub Debit_txt_Enter(sender As Object, e As EventArgs) Handles Debit_txt.Enter
        Credit_txt.Clear()
    End Sub

    Private Sub Debit_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Debit_txt.KeyDown
        If e.KeyCode = Keys.Up Then Credit_txt.Select()
    End Sub

    Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Debit_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs) Handles Debit_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Change_IM_Details_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        Me.Close()
    End Sub

    Private Sub SaveSButton_Click(sender As Object, e As EventArgs) Handles SaveSButton.Click
        Exp_Static_INSERT()
    End Sub

    Private Sub Exp_Static_INSERT()

        If String.IsNullOrWhiteSpace(Debit_txt.Text) Then Debit_txt.Text = "0"
        If String.IsNullOrWhiteSpace(Credit_txt.Text) Then Credit_txt.Text = "0"

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[Agents_insert_Prev_Balance]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@AG_ID", F_Agent.AG_ID)


            If ComboBox1.SelectedIndex = 0 Then
                If Convert.ToDouble(Debit_txt.Text) > 0 Then
                    .Parameters.AddWithValue("@Balance", Convert.ToDouble(Debit_txt.Text))
                    .Parameters.AddWithValue("@Type_ID", 37)
                ElseIf Convert.ToDouble(Credit_txt.Text) > 0 Then
                    .Parameters.AddWithValue("@Balance", Convert.ToDouble(Credit_txt.Text))
                    .Parameters.AddWithValue("@Type_ID", 6)
                End If
            Else
                ' If Convert.ToDouble(Debit_txt.Text) > 0 Then
                .Parameters.AddWithValue("@Balance", Convert.ToDouble(Debit_txt.Text))
                .Parameters.AddWithValue("@Type_ID", 5)
                '    ElseIf Convert.ToDouble(Credit_txt.Text) > 0 Then
                '    .Parameters.AddWithValue("@Balance", Convert.ToDouble(Credit_txt.Text))
                '    .Parameters.AddWithValue("@Type_ID", 6)
                'End If
            End If

            .Parameters.AddWithValue("@NOTES", Notes_txt.Text)
            .Parameters.AddWithValue("@Date", DateTime.Value)
            .Parameters.AddWithValue("@User_ID", USER_ID)

        End With
        If SQL_SP_EXEC(C.Com) = True Then
            MsgBox("تم الإضافة", MsgBoxStyle.Information, "")

            If Convert.ToDouble(Debit_txt.Text) > 0 Then
                Network_Edit_Tracker_insert(ComboBox1.Text & " لحساب:" & AG_NAME & " بقيمة : " & Debit_txt.Text & "دائن (عليه)", 0, 37, 1)
            ElseIf Convert.ToDouble(Credit_txt.Text) > 0 Then
                Network_Edit_Tracker_insert(ComboBox1.Text & " لحساب:" & AG_NAME & " بقيمة : " & Credit_txt.Text & "مدين (له)", 0, 36, 1)
            End If

            F_Agent.AG_BalanceTextBox.Text = Show_AG_T_Balance(F_Agent.AG_ID)
        End If



        Me.Close()
    End Sub

    'Private Sub EditSButton_Click(sender As Object, e As EventArgs) Handles EditSButton.Click
    '    Exp_Static_UPDATE()
    'End Sub

    'Private Sub Exp_Static_UPDATE()

    '    If String.IsNullOrWhiteSpace(Debit_txt.Text) Then Debit_txt.Text = "0"
    '    F_Exp_Static.Dt.Clear()
    '    Dim C As New C
    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "Exp_Static_UPDATE"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@EX_ID", EX_ID)
    '        .Parameters.AddWithValue("@COST", Debit_txt.Text)

    '    End With

    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(F_Exp_Static.Dt)
    '    F_Exp_Static.IM_GV.DataSource = F_Exp_Static.Dt

    '    Network_Edit_Tracker_insert("تعديل بيانات مصروف تابث " + EX_NAME, 0, 0, 0)

    '    Me.Close()
    'End Sub

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click
        Beep()
        If MessageBox.Show("تأكيد إلغاء المعاملة نهائيا", "", MessageBoxButtons.OKCancel, _
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Exp_Static_DELETE()
        End If

    End Sub

    Private Sub Exp_Static_DELETE()
        ' F_Exp_Static.Dt.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "AG_Balance_Void_Row"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", F_Balances.AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        End With
        If SQL_SP_EXEC(C.Com) = True Then


            If is_Select = True Then
                If F_Balances.AGMVMetroGrid.CurrentRow.Cells("AG_MV_Type_ID_CL").Value = 6 Then


                    If Convert.ToDouble(Debit_txt.Text) > 0 Then
                        Network_Edit_Tracker_insert(" لحساب:" & AG_NAME & " بقيمة : " & Debit_txt.Text & "دائن (عليه)", 0, 6, 2)
                    ElseIf Convert.ToDouble(Credit_txt.Text) > 0 Then
                        Network_Edit_Tracker_insert(" لحساب:" & AG_NAME & " بقيمة : " & Credit_txt.Text & "مدين (له)", 0, 6, 2)
                    End If


                ElseIf F_Balances.AGMVMetroGrid.CurrentRow.Cells("AG_MV_Type_ID_CL").Value = 5 Then
                    Network_Edit_Tracker_insert(" لحساب:" & F_Balances.AGMVMetroGrid.CurrentRow.Cells("AG_Name_CL").Value.ToString & " بقيمة : " & Credit_txt.Text, 0, 5, 2)
                End If

            End If

            Me.Close()
        End If


    End Sub

    Private Sub Exp_Static_Add_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")

        If is_Select = True Then

            If F_Balances.AGMVMetroGrid.CurrentRow.Cells("AG_MV_Type_ID_CL").Value = 6 Or F_Balances.AGMVMetroGrid.CurrentRow.Cells("AG_MV_Type_ID_CL").Value = 37 Then
                Me.Text = "عرض : رصيد سابق"
                ComboBox1.SelectedIndex = 0
            ElseIf F_Balances.AGMVMetroGrid.CurrentRow.Cells("AG_MV_Type_ID_CL").Value = 5 Then
                Me.Text = "عرض : إحتساب مرتب"
                ComboBox1.SelectedIndex = 1
            End If

            If F_Balances.AGMVMetroGrid.CurrentRow.Cells("AGisVoid_CL").Value = True Then
                VoidLb.Visible = True
            Else
                VoidLb.Visible = False
                DeleteSButton.Enabled = True
            End If
            Debit_txt.Text = F_Balances.AGMVMetroGrid.CurrentRow.Cells("Debit_CL").Value
            Credit_txt.Text = F_Balances.AGMVMetroGrid.CurrentRow.Cells("Credit_CL").Value
            Notes_txt.Text = F_Balances.AGMVMetroGrid.CurrentRow.Cells("Receipt_Title_CL").Value
            DateTime.Value = F_Balances.AGMVMetroGrid.CurrentRow.Cells("Date_CL").Value

            Debit_txt.Enabled = False
            Credit_txt.Enabled = False
            Notes_txt.Enabled = False
            DateTime.Enabled = False

            ' DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            ComboBox1.Enabled = False
        Else
            DeleteSButton.Enabled = False
            Debit_txt.Select()
            ComboBox1.SelectedIndex = 0
        End If

    End Sub

    Private Sub Credit_txt_Enter(sender As Object, e As EventArgs) Handles Credit_txt.Enter
        Debit_txt.Clear()
    End Sub

    Private Sub Credit_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Credit_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Credit_txt_TextChanged(sender As Object, e As EventArgs) Handles Credit_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)

    End Sub

    Private Sub Credit_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Credit_txt.KeyDown
        If e.KeyCode = Keys.Down Then Debit_txt.Select()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 1 Then
            Credit_txt.Clear()
            Credit_txt.Enabled = False
        Else
            Credit_txt.Enabled = True
        End If
    End Sub
End Class