Public Class IM_Update_Qty

    Private Sub POS_D_Valid_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub POS_D_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        'ST_Name_txt.Text = F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("ST_Name_CL").Value
        'IM_Name_txt.Text = F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("item_name_CL").Value
        'Unit_txt.Text = F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("Unit_CL").Value
        'Cost_txt.Text = F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("Cost_CL").Value
        'QTY_txt.Text = F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("QTY_CL").Value
        'New_Qty_txt.Text = F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("QTY_CL").Value

        ST_Name_txt.Text = F_STORES_Explorer.gridv.CurrentRow.Cells(3).Value
        IM_Name_txt.Text = F_STORES_Explorer.gridv.CurrentRow.Cells(7).Value
        Unit_txt.Text = F_STORES_Explorer.gridv.CurrentRow.Cells(8).Value
        Cost_txt.Text = F_STORES_Explorer.gridv.CurrentRow.Cells(11).Value
        QTY_txt.Text = F_STORES_Explorer.gridv.CurrentRow.Cells(10).Value
        New_Qty_txt.Text = F_STORES_Explorer.gridv.CurrentRow.Cells(10).Value

        Check_IF_IM_Valid()

        Cost_txt.Select()
    End Sub

    Public Sub Check_IF_IM_Valid()
        'Dim c As New C
        'Try
        '    Dim s As String
        '    s = "select isValid from IM_Menu WHERE IM_ID = '" & F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("item_id_CL").Value & "'"
        '    c.Com = New SqlClient.SqlCommand(s, c.Con)
        '    c.Con.Open()
        '    c.Dr = c.Com.ExecuteReader
        '    If c.Dr.HasRows Then
        '        c.Dr.Read()
        '        If c.Dr("isValid") = 1 Then
        '            If F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("D_Valid_CL").Value <> "" Then
        '                Valid_Date.Text = F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("D_Valid_CL").Value
        '            End If
        '        Else
        '            Valid_Panel.Visible = False
        '        End If

        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        '-----------------------------------------------------------------------------------
        Dim c As New C
        Try
            Dim s As String
            s = "select isValid from IM_Menu WHERE IM_ID = '" & F_STORES_Explorer.gridv.CurrentRow.Cells(1).Value & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                If c.Dr("isValid") = 1 Then
                    If F_STORES_Explorer.gridv.CurrentRow.Cells(9).Value <> "" Then
                        Valid_Date.Text = F_STORES_Explorer.gridv.CurrentRow.Cells(9).Value
                    End If
                Else
                    Valid_Panel.Visible = False
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub ConfermButton_Click(sender As Object, e As EventArgs) Handles ConfermButton.Click

        If IM_min_QTY = False Then
            If Convert.ToDouble(New_Qty_txt.Text) < 0 Then
                MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        'Network_Edit_Tracker_insert(" تعديل بيانات الصنف : " + IM_Name_txt.Text + " / المخزن : " + ST_Name_txt.Text + " / الوحدة : " + Unit_txt.Text + " / تكلفة الوحدة : " + Cost_txt.Text + " / الكمية : " + New_Qty_txt.Text, 0, _
        '                            F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("item_id_CL").Value, F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("ST_ID_CL").Value)

        ST_Balance_Update()
    End Sub

    Private Sub ST_Balance_Update()
        'Dim c As New C

        'With c.Com
        '    .Connection = c.Con
        '    .CommandText = "ST_Balance_Update"
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.AddWithValue("@T_ID", F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("T_ID_CL").Value)
        '    .Parameters.AddWithValue("@IM_ID", F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("item_id_CL").Value)
        '    .Parameters.AddWithValue("@ST_ID", F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("ST_ID_CL").Value)

        '    If Valid_Panel.Visible = True Then .Parameters.AddWithValue("@D_Valid", Valid_Date.Value.Date)
        '    .Parameters.AddWithValue("@ST_QTY", New_Qty_txt.Text)
        '    .Parameters.AddWithValue("@Cost", Cost_txt.Text)
        '    .Parameters.AddWithValue("@Qty_Deference", Qty_Deference_Txt.Text)
        '    .Parameters.AddWithValue("@Tag", Tag_txt.Text)
        'End With

        'If SQL_SP_EXEC(c.Com) Then
        '    MsgBox("تم حفظ التعديلات", MsgBoxStyle.Information)
        '    Network_Edit_Tracker_insert(" الصنف:" + IM_Name_txt.Text + " المخزن:" + ST_Name_txt.Text + " الوحدة:" + Unit_txt.Text + " التكلفة:" + Cost_txt.Text + " الكمية:" + New_Qty_txt.Text, 0, 23, 3)
        '    F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("Cost_CL").Value = Cost_txt.Text
        '    F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("QTY_CL").Value = New_Qty_txt.Text
        '    F_STORES_Explorer.DataGridViewX.CurrentRow.Cells("T_Cost_CL").Value = Convert.ToDouble(Cost_txt.Text) * Convert.ToDouble(New_Qty_txt.Text)
        '    F_STORES_Explorer.Get_Total_QYT()
        '    Me.Close()
        'End If
        '---------------------------------------------------------------------------------------------------------------------
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "ST_Balance_Update"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", 0)
            .Parameters.AddWithValue("@IM_ID", F_STORES_Explorer.gridv.CurrentRow.Cells(1).Value)
            .Parameters.AddWithValue("@ST_ID", F_STORES_Explorer.gridv.CurrentRow.Cells(2).Value)

            If Valid_Panel.Visible = True Then .Parameters.AddWithValue("@D_Valid", Valid_Date.Value.Date)
            .Parameters.AddWithValue("@ST_QTY", New_Qty_txt.Text)
            .Parameters.AddWithValue("@Cost", Cost_txt.Text)
            .Parameters.AddWithValue("@Qty_Deference", Qty_Deference_Txt.Text)
            .Parameters.AddWithValue("@Tag", Tag_txt.Text)
        End With

        If SQL_SP_EXEC(c.Com) Then
            MsgBox("تم حفظ التعديلات", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" الصنف:" + IM_Name_txt.Text + " المخزن:" + ST_Name_txt.Text + " الوحدة:" + Unit_txt.Text + " التكلفة:" + Cost_txt.Text + " الكمية:" + New_Qty_txt.Text, 0, 23, 3)
            F_STORES_Explorer.gridv.CurrentRow.Cells(11).Value = Cost_txt.Text
            F_STORES_Explorer.gridv.CurrentRow.Cells(10).Value = New_Qty_txt.Text
            F_STORES_Explorer.gridv.CurrentRow.Cells(13).Value = Convert.ToDouble(Cost_txt.Text) * Convert.ToDouble(New_Qty_txt.Text)
            F_STORES_Explorer.Get_Total_QYT()
            Me.Close()
        End If

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
        Check_Only_Float(sender, e)
    End Sub

    Private Sub New_Qty_txt_TextChanged(sender As Object, e As EventArgs) Handles New_Qty_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
        If String.IsNullOrWhiteSpace(New_Qty_txt.Text) Then New_Qty_txt.Text = 0
        Qty_Deference_Txt.Text = (Convert.ToDouble(QTY_txt.Text) - Convert.ToDouble(New_Qty_txt.Text))
        If Convert.ToDouble(Qty_Deference_Txt.Text) < 0 Then Qty_Deference_Txt.Text = (Convert.ToDouble(Qty_Deference_Txt.Text) * -1)


        If (Convert.ToDouble(QTY_txt.Text) - Convert.ToDouble(New_Qty_txt.Text)) = 0 Then
            Qty_Deference_Txt.ForeColor = Color.Black
            Tag_txt.Text = " "
        ElseIf (Convert.ToDouble(QTY_txt.Text) - Convert.ToDouble(New_Qty_txt.Text)) > 0 Then
            Qty_Deference_Txt.ForeColor = Color.DarkRed
            Tag_txt.Text = "-"
        Else
            Qty_Deference_Txt.ForeColor = Color.DarkGreen
            Tag_txt.Text = "+"
        End If
    End Sub
End Class