Public Class Exp_Static_Add
    Public is_Select As Boolean = False
    Public EX_ID As Integer
    Public EX_NAME As String = ""
    Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown
        Select Case e.KeyCode
            Case Keys.Return : PriceTextBox.Select()
        End Select
    End Sub

    Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles QtyTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub


    Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PriceTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs) Handles PriceTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Change_IM_Details_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub isInvoice_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_EXP_DATE_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Age_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Age_Txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Age_Txt_TextChanged(sender As Object, e As EventArgs) Handles Age_Txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub SaveSButton_Click(sender As Object, e As EventArgs) Handles SaveSButton.Click
        Exp_Static_INSERT()
    End Sub

    Private Sub Exp_Static_INSERT()

        If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
        If String.IsNullOrWhiteSpace(PriceTextBox.Text) Then PriceTextBox.Text = "0"
        F_Exp_Static.Dt.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Exp_Static_INSERT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@DATE", DateTimeEx.Value)
            .Parameters.AddWithValue("@EX_NAME", Exp_Name_txt.Text)
            .Parameters.AddWithValue("@COST", PriceTextBox.Text)
            .Parameters.AddWithValue("@QTY", QtyTextBox.Text)

            If is_EXP_DATE_CB.Checked = True Then .Parameters.AddWithValue("@DEF_AGE", Age_Txt.Text)
            .Parameters.AddWithValue("@is_Executed", is_EXP_DATE_CB.Checked)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(F_Exp_Static.Dt)
        F_Exp_Static.IM_GV.DataSource = F_Exp_Static.Dt


        'If PrevPrice <> NewPrice Then
        'Network_Edit_Tracker_insert("تعديل سعر بيع صنف '" + IM_LB.Text + "' / رقم آلي '" + F_Sales.Bill_ID_Txt.Text + " / رقم يومي : " + _
        '                 F_Sales.BillNumTxt.Text + "' من '" + PrevPrice.ToString + "' إلى '" + NewPrice.ToString + "' / المدخل :  " + F_Sales.User_Name_lb.Text, F_Sales.Pure_txt.Text, _
        '                 F_Sales.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, 0)
        'End If

        Me.Close()
    End Sub

    Private Sub EditSButton_Click(sender As Object, e As EventArgs) Handles EditSButton.Click
        Exp_Static_UPDATE()
    End Sub

    Private Sub Exp_Static_UPDATE()
        If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
        If String.IsNullOrWhiteSpace(PriceTextBox.Text) Then PriceTextBox.Text = "0"
        F_Exp_Static.Dt.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Exp_Static_UPDATE"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@EX_ID", EX_ID)
            .Parameters.AddWithValue("@DATE", DateTimeEx.Value)
            .Parameters.AddWithValue("@EX_NAME", Exp_Name_txt.Text)
            .Parameters.AddWithValue("@COST", PriceTextBox.Text)
            .Parameters.AddWithValue("@QTY", QtyTextBox.Text)
            If is_EXP_DATE_CB.Checked = True Then .Parameters.AddWithValue("@DEF_AGE", Age_Txt.Text)
            .Parameters.AddWithValue("@is_Executed", is_EXP_DATE_CB.Checked)
        End With

        '   SQL_SP_EXEC(C.Com)

        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(F_Exp_Static.Dt)
        F_Exp_Static.IM_GV.DataSource = F_Exp_Static.Dt


        'If is_EXP_DATE_CB.Checked = True Then
        '    F_Exp_Static.IM_GV.CurrentRow.Cells("is_Executed_CL").Value = True
        'Else
        '    F_Exp_Static.IM_GV.CurrentRow.Cells("is_Executed_CL").Value = False
        'End If
        'F_Exp_Static.IM_GV.CurrentRow.Cells("EX_NAME_CL").Value = Exp_Name_txt.Text
        'F_Exp_Static.IM_GV.CurrentRow.Cells("DATE_CL").Value = DateTimeEx.Value
        'F_Exp_Static.IM_GV.CurrentRow.Cells("DEF_AGE_CL").Value = Age_Txt.Text
        'F_Exp_Static.IM_GV.CurrentRow.Cells("QTY_CL").Value = QtyTextBox.Text
        'F_Exp_Static.IM_GV.CurrentRow.Cells("COST_CL").Value = PriceTextBox.Text

        'If PrevPrice <> NewPrice Then
        Network_Edit_Tracker_insert("تعديل بيانات مصروف تابث " + EX_NAME, 0, 0, 0)
        'End If

        Me.Close()
    End Sub

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click
        Beep()
        If MessageBox.Show("تأكيد حذف المصروف نهائيا", "", MessageBoxButtons.OKCancel, _
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Exp_Static_DELETE()
        End If


    End Sub

    Private Sub Exp_Static_DELETE()
        F_Exp_Static.Dt.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Exp_Static_DELETE"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@EX_ID", EX_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(F_Exp_Static.Dt)
        F_Exp_Static.IM_GV.DataSource = F_Exp_Static.Dt

        Network_Edit_Tracker_insert("حذف  المصروف تابث " + EX_NAME, 0, 0, 0)

        'If PrevPrice <> NewPrice Then
        '    Network_Edit_Tracker_insert("تعديل سعر بيع صنف '" + IM_LB.Text + "' / رقم آلي '" + F_Sales.Bill_ID_Txt.Text + " / رقم يومي : " + _
        '                     F_Sales.BillNumTxt.Text + "' من '" + PrevPrice.ToString + "' إلى '" + NewPrice.ToString + "' / المدخل :  " + F_Sales.User_Name_lb.Text, F_Sales.Pure_txt.Text, _
        '                     F_Sales.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, 0)
        'End If

        Me.Close()
    End Sub

    Private Sub Exp_Static_Add_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        If is_Select = True Then SELECT_EXP_STATIC()
        Exp_Name_txt.Select()
    End Sub



    Private Sub SELECT_EXP_STATIC()
        EX_ID = F_Exp_Static.IM_GV.CurrentRow.Cells("EX_ID_CL").Value
        is_EXP_DATE_CB.Checked = F_Exp_Static.IM_GV.CurrentRow.Cells("is_Executed_CL").Value
        Exp_Name_txt.Text = F_Exp_Static.IM_GV.CurrentRow.Cells("EX_NAME_CL").Value
        DateTimeEx.Value = F_Exp_Static.IM_GV.CurrentRow.Cells("DATE_CL").Value
        If Not IsDBNull(F_Exp_Static.IM_GV.CurrentRow.Cells("DEF_AGE_CL").Value) Then Age_Txt.Text = F_Exp_Static.IM_GV.CurrentRow.Cells("DEF_AGE_CL").Value
        QtyTextBox.Text = F_Exp_Static.IM_GV.CurrentRow.Cells("QTY_CL").Value
        PriceTextBox.Text = F_Exp_Static.IM_GV.CurrentRow.Cells("COST_CL").Value
        DeleteSButton.Enabled = True
        EditSButton.Enabled = True
    End Sub

End Class