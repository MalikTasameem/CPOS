Public Class AddFastAgent
    'Dim rs As New Resizer
    Dim AG_ID As Integer

    Private Sub AddFastAgent_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub AddFastAgent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'rs.FindAllControls(Me)
        'Fetch_Agents_Type()
        AG_Name_txt.Select()
    End Sub

    'Private Sub Fetch_Agents_Type()
    '    Dim C As New C
    '    Try
    '        Dim sql As String = "Select id,Type_Name from Agents_Types WHERE Visible = 1 Order By ID ASC"
    '        C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
    '        C.Da.Fill(C.Dt)
    '        AG_Type_cm.DataSource = C.Dt
    '        AG_Type_cm.DisplayMember = "Type_Name"
    '        AG_Type_cm.ValueMember = "id"
    '        AG_Type_cm.SelectedValue = 1
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    Private Sub Insert_Fast_AG()

        Dim C As New C
        With C.Com
            .CommandText = "Agents_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Ag_ID", 0)
            .Parameters.AddWithValue("@Ag_name", AG_Name_txt.Text)
            .Parameters.AddWithValue("@Barcode", Barcode_txt.Text)
            .Parameters.AddWithValue("@Ag_phone", Phone_txt.Text)
            .Parameters.AddWithValue("@Address", Address_txt.Text)
            .Parameters.AddWithValue("@isDefaultAG", 1)
            .Parameters.AddWithValue("@Type_ID", Cr_Type_ID)
            .Parameters("@Ag_ID").Direction = ParameterDirection.Output
            .Parameters.AddWithValue("@E_mail", "")
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            MsgBox("تمت إضافة الزبون", MsgBoxStyle.Information)
            AG_ID = C.Com.Parameters("@Ag_ID").Value.ToString()
            Save_AG_btn.Enabled = False
            '  Select_AG_btn.Enabled = False
            Me.Close()
        End If

    End Sub

    Private Sub AddFastAgent_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'rs.ResizeAllControls(Me)
    End Sub

    Private Sub Save_AG_btn_Click(sender As Object, e As EventArgs) Handles Save_AG_btn.Click

        If ValidateChildren() = True Then
            If String.IsNullOrWhiteSpace(Barcode_txt.Text) = False Then
                AG_Check_Barcode()
            Else
                Insert_Fast_AG()
            End If
        End If


    End Sub

    Private Sub AG_Check_Barcode()
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "AG_Check_Barcode"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Barcode", Barcode_txt.Text)
            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                If .Parameters("@F").Value > 0 Then
                    BarError.SetError(Barcode_txt, "باركود متكرر")
                    Barcode_txt.Select()
                    Barcode_txt.Focus()
                Else
                    Insert_Fast_AG()
                    'If Home.Home_Panel.Tag = "POS" Then
                    '    F_AgentsMenu.AG_Balance_Update_AG(AG_ID)
                    'End If
                    Me.Close()
                End If

            End If
        End With
    End Sub

    Private Sub AG_Name_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles AG_Name_txt.KeyDown
        If e.KeyCode = Keys.Return Then Barcode_txt.Select()
    End Sub

    Private Sub AG_Name_txt_TextChanged(sender As Object, e As EventArgs) Handles AG_Name_txt.TextChanged
        If String.IsNullOrWhiteSpace(AG_Name_txt.Text) Then
            Save_AG_btn.Enabled = False
            '  Select_AG_btn.Enabled = False
        Else
            Save_AG_btn.Enabled = True
            ' Select_AG_btn.Enabled = True
        End If
    End Sub

    Private Sub Address_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Address_txt.KeyDown
        If e.KeyCode = Keys.Return Then Save_AG_btn_Click(sender, e)
    End Sub

    Private Sub Phone_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Phone_txt.KeyDown
        If e.KeyCode = Keys.Return Then Address_txt.Select()
    End Sub


    Private Sub Barcode_txt_TextChanged(sender As Object, e As EventArgs) Handles Barcode_txt.TextChanged
        BarError.Clear()
    End Sub

    Private Sub Barcode_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_txt.KeyDown
        If e.KeyCode = Keys.Return Then Phone_txt.Select()
    End Sub


    Private Sub AG_Name_txt_Enter(sender As Object, e As EventArgs) Handles AG_Name_txt.Enter
        Set_Ar_Language()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub AddFastAgent_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class