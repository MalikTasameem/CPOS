Public Class IM_ADD_New
    Dim New_IM_ID As Integer
    Dim IM_Dt As New DataTable
    Public Bar_Entred As String = ""
    Dim Valid_St As String = "لا"
    Private Sub POS_D_Valid_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub item_menu_Delete_DisableRows()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "item_menu_Delete_DisableRows"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@USER_ID", USER_ID)
        End With
        SQL_SP_EXEC(c.Com)
    End Sub

    Private Sub IM_ADD_New_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F12 Then ConfermButton_Click(sender, e)
    End Sub

    Private Sub POS_D_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        is_Add_New_IM = False
        Load_GM()
        Insert_IM()
        Load_Units(IM_Unit_cm)
        Call_IM_After_Insert_CB.Checked = My_Settings.Call_IM_After_Insert_CB
        IM_SH_txt.Select()
        isValid_CB.Visible = S_IM_Valid
        GM_Serach.SelectedValue = MY_Settings.GM_ID_Selected
    End Sub

    Private Sub Load_GM()
        Try

            Dim c As New C
            Dim s As String = "select GM_ID,GM_Name from General_Menu"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            GM_Serach.DataSource = c.Dt
            GM_Serach.DisplayMember = "GM_Name"
            GM_Serach.ValueMember = "GM_ID"
            GM_Serach.SelectedValue = 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub Insert_IM()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "item_menu_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", 0)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters.AddWithValue("@isStore", IM_Default_Stut)
            .Parameters.AddWithValue("@GM_ID", 1)
            .Parameters("@IM_ID").Direction = ParameterDirection.Output
        End With
        If SQL_SP_EXEC(c.Com) = True Then New_IM_ID = c.Com.Parameters("@IM_ID").Value.ToString()
    End Sub


    Dim Bar01 As Boolean = False
    Dim Bar02 As Boolean = False

    Private Sub ConfermButton_Click(sender As Object, e As EventArgs) Handles ConfermButton.Click
        If Search_IM() = 1 Then
            MsgBox("إسم الصنف متكرر", MsgBoxStyle.Critical, "خطأ فالإدخال")
            Exit Sub
        End If

        If Check_Unit_Exist(IM_Unit_cm, Unit_cargo_txt.Text) = 0 Then Unit_Insert(IM_Unit_cm, Unit_cargo_txt.Text, 22)
        If String.IsNullOrWhiteSpace(IM_SH_txt.Text) Then
            MsgBox("أدخل اسم الصنف", MsgBoxStyle.Exclamation, "حقل إجباري")

        Else

            If GM_Serach.SelectedValue = 0 Then
                MsgBox("حدد تصنيف الصنف", MsgBoxStyle.Exclamation, "حقل إجباري")
                Exit Sub
            End If


            Bar01 = False
            Bar02 = False

            If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then
                If S_is_Multi_BAR = False Then
                    Bar01 = IM_Check_Barcode(Barcode_SH_txt.Text)
                Else
                    Bar01 = True
                    'Confirm_IM()
                End If
            Else
                Bar01 = True
                'Confirm_IM()
            End If


            If String.IsNullOrWhiteSpace(Barcode_By_One_txt.Text) = False And By_One_Panel.Visible = True Then
                If S_is_Multi_BAR = False Then
                    Bar02 = IM_Check_Barcode(Barcode_By_One_txt.Text)
                Else
                    Bar02 = True
                    'Confirm_IM()
                End If
            Else
                Bar02 = True
                'Confirm_IM()
            End If

            If Bar01 = True And Bar02 = True Then Confirm_IM()

        End If

    End Sub

    Public Function Search_IM()
        Dim c As New C
        Try
            Dim s As String
            s = "select IM_ID,item_name from IM_Active_V WHERE item_name = '" & IM_SH_txt.Text & "' Order by item_name ASC"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then Return 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 0
    End Function




    Private Function IM_Check_Barcode(Barcode As String) As Boolean
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Barcode"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Barcode", Barcode)
            .Parameters.AddWithValue("@IM_ID", New_IM_ID)
            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then

                If .Parameters("@F").Value > 0 Then
                    MsgBox("باركود متكرر", MsgBoxStyle.Critical, " خطأ في إدخال الصنف ")
                    Return False
                Else
                    Return True
                    'Confirm_IM()
                End If

            End If
        End With


        Return False
    End Function

    Private Sub Confirm_IM()
        Dim c As New C

        'If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) Then Barcode_SH_txt.Text = Get_Barcode_U_IM_ID()
        'If String.IsNullOrWhiteSpace(Barcode_By_One_txt.Text) Then Barcode_By_One_txt.Text = Get_Barcode_U_IM_ID()

        With c.Com
            .Connection = c.Con
            .CommandText = "ADD_IM_Not_Exisesit"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", New_IM_ID)
            .Parameters.AddWithValue("@item_name", IM_SH_txt.Text)
            If Not String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) Then .Parameters.AddWithValue("@Barcode", Barcode_SH_txt.Text)
            '.Parameters.AddWithValue("@BarCode", Barcode_SH_txt.Text)
            .Parameters.AddWithValue("@isValid", Me.isValid_CB.Checked)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters.AddWithValue("@GM_ID", GM_Serach.SelectedValue)
            .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
            .Parameters.AddWithValue("@IM_NUM", IM_Num_txt.Text)
            .Parameters.AddWithValue("@Def_U_ID", Def_U_ID)
            .Parameters.AddWithValue("@is_Store", IM_Default_Stut)
            '.Parameters.AddWithValue("@Barcode_By_One", Barcode_By_One_txt.Text)
            If Not String.IsNullOrWhiteSpace(Barcode_By_One_txt.Text) Then .Parameters.AddWithValue("@Barcode_By_One", Barcode_By_One_txt.Text)

        End With

        If SQL_SP_EXEC(c.Com) = True Then
            'MsgBox("تم الحفظ", MsgBoxStyle.Information)

            Network_Edit_Tracker_insert(" (شاشة إضافة صنف) إسم الصنف:" & IM_SH_txt.Text & " النوع:" & "بضاعة" & " التصنيف:" & GM_Serach.Text & " الرقم:" & IM_Num_txt.Text & _
                                        " الصلاحية:" & Valid_St & " الوحدة:" & IM_Unit_cm.Text & " باركود:" & Barcode_SH_txt.Text & "  باركود الوحدة2:" & Barcode_By_One_txt.Text, 0, 20, 1)

            is_Add_New_IM = True
            If Call_IM_After_Insert_CB.Checked = True Then
                If Application.OpenForms().OfType(Of Invoice).Any Then
                    With F_Invoice_IM_card
                        .mySearchControl.txtSearch.Text = IM_SH_txt.Text
                        .Barcode_IM = Barcode_SH_txt.Text
                        '.Fetch_ItemToList()
                        .IM_ID = New_IM_ID

                        Me.Close()
                    End With
                End If

                If Application.OpenForms().OfType(Of Pch).Any Then
                    With F_Pch_IM_card
                        .mySearchControl.txtSearch.Text = IM_SH_txt.Text
                        .Barcode_IM = Barcode_SH_txt.Text
                        '.Fetch_ItemToList()
                        .IM_ID = New_IM_ID

                        Me.Close()
                    End With
                End If

            End If

            MY_Settings.GM_ID_Selected = GM_Serach.SelectedValue
            MY_Settings.Save_AppSetting()

            Me.Close()
        End If

    End Sub


    Private Sub Back_ADD_New_IM_btn_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        item_menu_Delete_DisableRows()
        Me.Close()
    End Sub

    Private Sub isValid_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isValid_CB.CheckedChanged
        CB_CHecked(sender)
        If sender.Checked = True Then
            Valid_St = "نعم"
        Else
            Valid_St = "لا"
        End If
    End Sub

    Private Sub Random_Barcode_btn_Click(sender As Object, e As EventArgs) Handles Random_Barcode_btn.Click
        Barcode_SH_txt.Text = Get_Barcode_U_IM_ID()
    End Sub

    Private Sub IM_Num_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Num_txt.KeyDown
        If e.KeyCode = Keys.Return Then Barcode_SH_txt.Select()
    End Sub

    Private Sub KeyBoard_Btn_Click(sender As Object, e As EventArgs) Handles KeyBoard_Btn.Click
        Dim O As New OSK_Class
        O.OSK_OPEN()
    End Sub

 
    Private Sub IM_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_SH_txt.KeyDown
      If e.KeyCode = Keys.Return Then IM_Num_txt.Select()
    End Sub

    Private Sub Call_IM_After_Insert_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Call_IM_After_Insert_CB.CheckedChanged
        My_Settings.Call_IM_After_Insert_CB = Call_IM_After_Insert_CB.Checked
        Save_AppSetting()
        CB_CHecked(sender)
    End Sub

    Private Sub ADD_NewGM_Btn_Click(sender As Object, e As EventArgs) Handles ADD_NewGM_Btn.Click
        ' If isCatch_IM = True Then
        If GM_Serach.SelectedIndex > -1 Then
            MsgBox("هذه مجموعة موجودة بالفعل", MsgBoxStyle.Critical, "إضافة مجموعة")
        ElseIf String.IsNullOrWhiteSpace(GM_Serach.Text) Then
            MsgBox("أدخل اسم مجموعة الجديد", MsgBoxStyle.Exclamation, "إضافة مجموعة")
            GM_Serach.Select()
        Else
            If MessageBox.Show(" إضافة " + GM_Serach.Text + " إلى قائمة المجموعات ", " إضافة مجموعة ", MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                GM_Serach.SelectedValue = Insert_Fast_GM()
            End If
        End If
        ' End If
    End Sub

    Private Function Insert_Fast_GM()
        Dim GM_New_ID As Integer
        Dim C As New C

        If GM_Serach.Text.Count < 4 Then
            Do While GM_Serach.Text.Count < 4
                GM_Serach.Text = GM_Serach.Text + " "
            Loop
        End If

        With C.Com
            .Connection = C.Con
            .CommandText = "GM_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@GM_ID", 0)
            .Parameters.AddWithValue("@GM_Name", Me.GM_Serach.Text)
            .Parameters.AddWithValue("@POS_isShow", 1)
            .Parameters.AddWithValue("@Printer_ID", 1)
            .Parameters.AddWithValue("@Ksh_Screen", My.Computer.Name)
            .Parameters("@GM_ID").Direction = ParameterDirection.Output

            If SQL_SP_EXEC(C.Com) Then
                GM_New_ID = .Parameters("@GM_ID").Value.ToString()
                Network_Edit_Tracker_insert(" التصنيف:" & GM_Serach.Text, 0, 21, 1)
                Load_GM()
            End If

        End With

        Return GM_New_ID
    End Function

    Private Sub Unit_cargo_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Unit_cargo_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If TypeName(IM_Unit_cm.SelectedValue) = "Integer" Or TypeName(IM_Unit_cm.SelectedValue) = "Long" Then IM_Fetch_Unit_Cargo(IM_Unit_cm, Unit_cargo_txt)
    End Sub


    Private Sub Unit_cargo_txt_TextChanged(sender As Object, e As EventArgs) Handles Unit_cargo_txt.TextChanged
        If Unit_cargo_txt.Text.Count = 0 Then Unit_cargo_txt.Text = "1"
        If Not String.IsNullOrWhiteSpace(Unit_cargo_txt.Text) Then
            If Convert.ToDouble(Unit_cargo_txt.Text) > 1 Then
                By_One_Panel.Visible = True
            Else
                By_One_Panel.Visible = False
            End If
        End If
    End Sub

    Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Unit_cm.KeyDown
        If e.KeyCode = Keys.Return Then
            If By_One_Panel.Visible = True Then
                Barcode_By_One_txt.Select()
            End If
        End If
    End Sub

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        If e.KeyCode = Keys.Return Then IM_Unit_cm.Select()
    End Sub
End Class