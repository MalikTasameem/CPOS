Public Class OFFERS
    'Dim rs As New Resizer
    Private Sub IM_MENU_FS_ID_Changed(sender As Object, e As EventArgs) Handles IM_MENU_FS.ID_Changed
        IM_Units_Select()
    End Sub



    Public Sub OFFERS_MASTER_V_Select()
        Try

            Dim C As New C
            C.Str = "SELECT [IM_ID],[TITLE],[item_name],[DATE_F],[DATE_T],[TIME_F],[TIME_T] FROM [dbo].[OFFERS_MASTER_V] ORDER BY DATE DESC "
            '.CommandText = "IM_Units_Select"
            '.CommandType = CommandType.StoredProcedure
            '.Parameters.AddWithValue("@IM_ID", IM_MENU_FS.TXT_ID.Text)
            C.Da = New SqlClient.SqlDataAdapter(C.Str, C.Con)
            C.Da.Fill(C.Dt)
            AGMetroGrid.DataSource = C.Dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub IM_Units_Select()
        Try

            Dim C As New C

            C.Str = "SELECT U_ID,U_IM_ID,U_Name,Barcode,Price,Min_SP,Min_SP_2,OFFER_Price,is_Default,UserName,U_Cargo from IM_Menu_Units_V where IM_ID = " & IM_MENU_FS.TXT_ID.Text
            '.CommandText = "IM_Units_Select"
            '.CommandType = CommandType.StoredProcedure
            '.Parameters.AddWithValue("@IM_ID", IM_MENU_FS.TXT_ID.Text)
            C.Da = New SqlClient.SqlDataAdapter(C.Str, C.Con)
            C.Da.Fill(C.Dt)
            Unit_DataGridView.DataSource = C.Dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridViewX1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Unit_DataGridView.CellEndEdit
        If String.IsNullOrWhiteSpace(Unit_DataGridView.CurrentRow.Cells("OFFER_Price_CL").Value) Then Unit_DataGridView.CurrentRow.Cells("OFFER_Price_CL").Value = 0
        IM_Units_UpdatePrice()
    End Sub

    Private Sub IM_Units_UpdatePrice()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "[IM_Units_Update_OFFER_Price]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@U_IM_ID", Unit_DataGridView.CurrentRow.Cells("U_IM_ID_CL").Value)
            .Parameters.AddWithValue("@Price", Unit_DataGridView.CurrentRow.Cells("OFFER_Price_CL").Value)
        End With
        SQL_SP_EXEC(c.Com)
    End Sub

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles Unit_DataGridView.EditingControlShowing
        If Me.Unit_DataGridView.CurrentCell.ColumnIndex = 7 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            'AddHandler tb.KeyDown, AddressOf TextBox_KeyDown
            AddHandler tb.KeyPress, AddressOf TextBox_KeyPress
        End If
    End Sub

    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Check_Only_Float(sender, e)
    End Sub


    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        OFFERS_MASTER_INSERT()
    End Sub


    Private Sub OFFERS_MASTER_INSERT()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "[OFFERS_MASTER_INSERT]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", IM_MENU_FS.TXT_ID.Text)
            .Parameters.AddWithValue("@TITLE", TITLE_TXT.Text)
            .Parameters.AddWithValue("@DATE_F", D_F.Value)
            .Parameters.AddWithValue("@DATE_T", D_T.Value)
            .Parameters.AddWithValue("@TIME_F", T_F.Value)
            .Parameters.AddWithValue("@TIME_T", T_T.Value)
            .Parameters.AddWithValue("@USER_ID", USER_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم الحفظ", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" إسم الصنف:" & IM_MENU_FS.Textt & " العنوان:" & TITLE_TXT.Text & " من:" & D_F.Text & " إلى:" & D_T.Text & " من ساعة:" _
                       & T_F.Text & " إلى ساعة:" & T_T.Text, 0, 36, 1)
            OFFERS_MASTER_V_Select()
        End If

    End Sub

    Private Sub OFFERS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OFFERS_MASTER_V_Select()
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        If AGMetroGrid.Rows.Count > 0 Then
            If MessageBox.Show(" تـأكيــد حــذف العرض الخاص بالصنف " + AGMetroGrid.CurrentRow.Cells("item_name_CL").Value, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, _
                      MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then OFFERS_MASTER_DELETE()
        End If

    End Sub

    Private Sub OFFERS_MASTER_DELETE()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "[OFFERS_MASTER_DELETE]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", AGMetroGrid.CurrentRow.Cells(0).Value)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم الحذف", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" إسم الصنف:" & AGMetroGrid.CurrentRow.Cells("item_name_CL").Value & " العنوان:" & AGMetroGrid.CurrentRow.Cells("TITLE_CL").Value & " من:" & AGMetroGrid.CurrentRow.Cells("DATE_F_CL").Value & " إلى:" & AGMetroGrid.CurrentRow.Cells("DATE_T_CL").Value & " من ساعة:" _
                   & AGMetroGrid.CurrentRow.Cells("TIME_F_CL").Value.ToString & " إلى ساعة:" & AGMetroGrid.CurrentRow.Cells("TIME_T_CL").Value.ToString, 0, 36, 2)

            OFFERS_MASTER_V_Select()
        End If


    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        If e.KeyCode = Keys.Return Then Load_IM_Barcode()
    End Sub
    Public Sub Load_IM_Barcode()


        'If S_is_Multi_BAR = True Then
        '    If Check_IF_Multi_BAR() > 1 Then
        '        SELECT_Multi_Bar()
        '        Exit Sub
        '    End If
        'End If


        Dim c As New C
        ' IM_Dt.Clear()
        Try
            Dim s As String
            'If Sh_ByNum_CB.Checked = True Then
            '    s = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_NUM = '" & Barcode_SH_txt.Text & "'"
            'Else
            s = "select U_IM_ID,IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
            '    End If

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_MENU_FS.Set_IM_By_ID(c.Dr("IM_ID"))
                'IM_ID = c.Dr("IM_ID")
                'IM_SH_txt.Text = c.Dr("item_name")
                'If Sh_ByNum_CB.Checked = False Then Barcode_IM = Barcode_SH_txt.Text
                'Get_Unit = False
                'Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
                'Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
                'Load_IM_Change_Price()
                'IMDataGridViewX.Visible = False

                'If c.Dr("isValid") = 1 Then
                '    Valid_Panel.Visible = True
                '    Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
                'Else
                '    Valid_Panel.Visible = False
                'End If

                'QtyTextBox.Select()
                'Fetch_IM_Units()
                'If Sh_ByNum_CB.Checked = False Then IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
                Barcode_SH_txt.Clear()
                'If MY_Settings.S_CodeAdd_1 = True Then ADD_IM()

            Else
                MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
                Barcode_SH_txt.Clear()
            End If



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OFFERS_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F8 Then Barcode_SH_txt.Select()
    End Sub



    'Private Sub OFFERS_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
    '    rs.ResizeAllControls(Me)
    'End Sub


End Class