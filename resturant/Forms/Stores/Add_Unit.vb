Public Class Add_Unit
    Public IM_ID As Integer
    Private Sub Add_Unit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Add_Unit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_IM()
    End Sub

    Public Sub Load_IM()
        Dim c As New C
        Try
            Dim sql As String = " select U_ID,U_Name from Units "
            c.Da = New SqlClient.SqlDataAdapter(sql, c.Con)
            c.Da.Fill(c.Dt)
            IM_Unit_cm.DataSource = c.Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_ID"
            IM_Unit_cm.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    'Private Function Check_Unit_exsist()
    '    If FormType = 2 Then
    '        Dim isFound As Boolean = False
    '        For i = 0 To F_Pch.IM_Unit_cm.Items.Count - 1
    '            F_Pch.IM_Unit_cm.SelectedIndex = i
    '            If F_Pch.IM_Unit_cm.SelectedValue = Units_Menu_cmb.SelectedValue Then
    '                isFound = True
    '                MsgBox("الوحدة موجودة بالفعل", MsgBoxStyle.Exclamation)
    '                Exit For
    '            End If
    '        Next
    '        Return isFound

    '    ElseIf FormType = 4 Then
    '        Dim isFound As Boolean = False
    '        For i = 0 To F_Invoice.IM_Unit_cm.Items.Count - 1
    '            If F_Invoice.IM_Unit_cm.SelectedValue = Units_Menu_cmb.SelectedValue Then
    '                isFound = True
    '                MsgBox("الوحدة موجودة بالفعل", MsgBoxStyle.Exclamation)
    '                Exit For
    '            End If
    '        Next
    '        Return isFound

    '    End If
    '    Return 0
    'End Function

    Private Sub InsertU_btn_Click(sender As Object, e As EventArgs) Handles InsertU_btn.Click
        If String.IsNullOrWhiteSpace(BarCode_txt.Text) = False Then
            If S_is_Multi_BAR = False Then
                IM_Check_Barcode()
            Else
                IM_Units_insert()
            End If
        Else
            IM_Units_insert()
        End If

        ' If Check_Unit_exsist() = False Then IM_Units_insert()
    End Sub

    Private Sub IM_Check_Barcode()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Barcode"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Barcode", BarCode_txt.Text)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then

                If .Parameters("@F").Value > 0 Then
                    MsgBox("باركود متكرر", MsgBoxStyle.Exclamation, "")
                    BarCode_txt.Select()
                    BarCode_txt.Focus()
                Else
                    IM_Units_insert()
                End If

            End If
        End With
    End Sub

    Private Sub IM_Units_insert()
        Dim U_ID As Integer

        If Check_Unit_Exist(IM_Unit_cm, Unit_cargo_txt.Text) = 0 Then
            Unit_Insert(IM_Unit_cm, Unit_cargo_txt.Text, 22)
            U_ID = GET_Unit_Exist(IM_Unit_cm.Text, Unit_cargo_txt.Text)
        Else
            U_ID = GET_Unit_Exist(IM_Unit_cm.Text, Unit_cargo_txt.Text)
        End If

        If String.IsNullOrWhiteSpace(Price_txt.Text) Then Price_txt.Text = "0"

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "IM_Units_insert"
            .CommandType = CommandType.StoredProcedure
            'If FormType = 2 Then
            '    .Parameters.AddWithValue("@IM_ID", F_Pch.IM_ID)
            'ElseIf FormType = 4 Then
            '.Parameters.AddWithValue("@IM_ID", F_Invoice.IM_ID)
            'End If

            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@U_ID", U_ID)
            .Parameters.AddWithValue("@Price", Price_txt.Text)
            If Not String.IsNullOrWhiteSpace(BarCode_txt.Text) Then .Parameters.AddWithValue("@Barcode", BarCode_txt.Text)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters.AddWithValue("@Row_Enabled", 1)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            If FormType = 2 Then
                F_Pch_IM_card.Fetch_IM_Units()
            ElseIf FormType = 4 Then
                F_Invoice_IM_card.Fetch_IM_Units()
            End If
            MsgBox("تم إضافة الوحدة", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub


    Private Sub Back_ADD_New_IM_btn_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Random_Barcode_btn_Click(sender As Object, e As EventArgs) Handles Random_Barcode_btn.Click
        BarCode_txt.Text = Get_Barcode_U_IM_ID()
    End Sub
  Private Sub Unit_cargo_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Unit_cargo_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub
 Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If TypeName(IM_Unit_cm.SelectedValue) = "Integer" Or TypeName(IM_Unit_cm.SelectedValue) = "Long" Then IM_Fetch_Unit_Cargo(IM_Unit_cm, Unit_cargo_txt)
    End Sub

    Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Unit_cm.KeyDown
        If e.KeyCode = Keys.Return Then Unit_cargo_txt.Select()
    End Sub

    Private Sub Unit_cargo_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Unit_cargo_txt.KeyDown
        If e.KeyCode = Keys.Return Then BarCode_txt.Select()
    End Sub
End Class