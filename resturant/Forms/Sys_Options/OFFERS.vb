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
        If e.RowIndex < 0 OrElse Unit_DataGridView.CurrentRow Is Nothing OrElse Unit_DataGridView.CurrentRow.IsNewRow Then Return
        If String.IsNullOrWhiteSpace(Convert.ToString(Unit_DataGridView.CurrentRow.Cells("OFFER_Price_CL").Value)) Then Unit_DataGridView.CurrentRow.Cells("OFFER_Price_CL").Value = 0D
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
        If AuditItemUnitsUi.Run(Me, c.Com) Then
            AuditItemUnitsUi.Refresh(Me, AddressOf IM_Units_Select)
        Else
            IM_Units_Select()
        End If
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

        If AuditPricingRulesUi.Run(Me, c.Com) Then
            MsgBox("تم الحفظ", MsgBoxStyle.Information)
            AuditPricingRulesUi.Refresh(Me, AddressOf OFFERS_MASTER_V_Select)
        End If

    End Sub

    Private Sub OFFERS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        OFFERS_MASTER_V_Select()
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        If AGMetroGrid.CurrentRow IsNot Nothing AndAlso Not AGMetroGrid.CurrentRow.IsNewRow Then
            If MessageBox.Show(" تـأكيــد حــذف جميع فترات عروض الصنف " & Convert.ToString(AGMetroGrid.CurrentRow.Cells("item_name_CL").Value), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, _
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

        If AuditPricingRulesUi.Run(Me, c.Com) Then
            MsgBox("تم الحذف", MsgBoxStyle.Information)
            AuditPricingRulesUi.Refresh(Me, AddressOf OFFERS_MASTER_V_Select)
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
