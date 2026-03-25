Public Class IM_Fast_Mang

    Dim IM_Dt As New DataTable
    Dim IM_ID As Integer
    Dim rs As New Resizer
    Private Sub Add_Unit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Add_Unit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        Load_GM()
        Fill_All_IM()
        Make_Hints()
        fetch_GM()
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "-----كل التصنيفات-----"))

        Dim c1 As New C
        Dim s As String = "select GM_Name as 'name' ,GM_ID as 'ID' from General_Menu "
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read

                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()

        Return mailItems

    End Function

    Public Sub fetch_GM()
        GM_Serach.DataSource = GetMailItems()
        GM_Serach.DisplayMember = "name"
        GM_Serach.ValueMember = "ID"
        GM_Serach.SelectedIndex = 0
    End Sub

    Private Sub Make_Hints()
        SendMessage(IM_SH_txt.Handle, &H1501, 0, "إبحث عن إسم صنف")
    End Sub

    Public Sub Load_GM()
        Dim c As New C
        Try
            Dim sql As String = " select GM_ID,GM_Name from General_menu "
            c.Da = New SqlClient.SqlDataAdapter(sql, c.Con)
            c.Da.Fill(c.Dt)
            GM_CL.DataSource = c.Dt
            GM_CL.DisplayMember = "GM_Name"
            GM_CL.ValueMember = "GM_ID"
            GM_CL.Selected = 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Fill_All_IM()
        Try
            Dim C As New C
            IM_Dt.Clear()
            Dim s As String
            If GM_Serach.SelectedValue = 0 Then
                s = "select IM_ID,GM_Name,GM_ID,item_name,IM_NUM,PRICE,isActive from IM_FAST_MANG_V Order by GM_Name,item_name ASC"
            Else
                s = "select IM_ID,GM_Name,GM_ID,item_name,IM_NUM,PRICE,isActive from IM_FAST_MANG_V where GM_ID = " & GM_Serach.SelectedValue & " Order by GM_Name,item_name ASC"
            End If
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(IM_Dt)
            MetroGrid.DataSource = IM_Dt
            SelectLast_ROW()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub Back_ADD_New_IM_btn_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    'Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles MetroGrid.EditingControlShowing
    '    If Me.MetroGrid.CurrentCell.ColumnIndex = 6 And Not e.Control Is Nothing Then
    '        Dim tb As TextBox = CType(e.Control, TextBox)
    '        'AddHandler tb.KeyDown, AddressOf TextBox_KeyDown
    '        AddHandler tb.KeyPress, AddressOf TextBox_KeyPress
    '    End If
    'End Sub



    'Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub


    Private Sub MetroGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles MetroGrid.CellContentClick

        If e.ColumnIndex = 0 Then

            If IsDBNull(MetroGrid.CurrentRow.Cells("IM_ID_CL").Value) Then
                Insert_IM()
                If IsDBNull(MetroGrid.CurrentRow.Cells("IM_NAME_CL").Value) Then
                    MsgBox("أدخل إسم الصنف", MsgBoxStyle.Exclamation, "")
                Else
                    Confirm_IM()
                End If
            Else
                IM_ID = MetroGrid.CurrentRow.Cells("IM_ID_CL").Value

                If IsDBNull(MetroGrid.CurrentRow.Cells("IM_NAME_CL").Value) Then
                    MsgBox("أدخل إسم الصنف", MsgBoxStyle.Exclamation, "")
                Else
                    Confirm_IM()
                End If

            End If
        End If

        If e.ColumnIndex = 1 Then
            IM_ID = MetroGrid.CurrentRow.Cells("IM_ID_CL").Value
            If MessageBox.Show(" تـأكيــد حــذف العنصر ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, _
                    MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                Delete_IM()
            End If

        End If


    End Sub

    Private Sub Delete_IM()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "item_menu_delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", IM_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم حــذف الصــنف", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert("" & MetroGrid.CurrentRow.Cells("IM_Name_CL").Value.ToString, 0, 20, 2)
            Fill_All_IM()
        End If
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

        If SQL_SP_EXEC(c.Com) = True Then
            IM_ID = c.Com.Parameters("@IM_ID").Value.ToString()
            'Confirm_IM()
            'Fetch_IM()
            ''IM_SH_txt.Select()
            'isValid_CB.Checked = isValid
        End If

    End Sub


    Private Sub Confirm_IM()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "[item_menu_update_On_Mange_Form]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@item_name", MetroGrid.CurrentRow.Cells("IM_NAME_CL").Value)
            .Parameters.AddWithValue("@item_nameSales", MetroGrid.CurrentRow.Cells("IM_NAME_CL").Value)
            If IsDBNull(MetroGrid.CurrentRow.Cells("GM_CL").Value) Then
                .Parameters.AddWithValue("@GM_ID", 1)
            Else
                .Parameters.AddWithValue("@GM_ID", MetroGrid.CurrentRow.Cells("GM_CL").Value)
            End If

            .Parameters.AddWithValue("@isActive", MetroGrid.CurrentRow.Cells("isActive_CL").Value)
            '.Parameters.AddWithValue("@photo", ConvertImage(Me.IMPictureBox.Image))


            '.Parameters.AddWithValue("@isStore", 0)
            '.Parameters.AddWithValue("@isValid", 0)
            '.Parameters.AddWithValue("@isChangePrice", 0)
            '.Parameters.AddWithValue("@Cost", 0)

            .Parameters.AddWithValue("@User_ID", USER_ID)
            '.Parameters.AddWithValue("@is_Shortcut", 0)
            .Parameters.AddWithValue("@IM_Num", MetroGrid.CurrentRow.Cells("IM_Num_CL").Value)
            '.Parameters.AddWithValue("@Grp_ID", 1)
            '.Parameters.AddWithValue("@Notes", "")
            .Parameters.AddWithValue("@Def_U_ID", Def_U_ID)
            '.Parameters.AddWithValue("@Markter_Val", 0)

            If IsDBNull(MetroGrid.CurrentRow.Cells("Price_CL").Value) Then
                .Parameters.AddWithValue("@Price_ONE", 0)
            Else
                .Parameters.AddWithValue("@Price_ONE", MetroGrid.CurrentRow.Cells("Price_CL").Value)
            End If


        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم الحفظ", MsgBoxStyle.Information)
            'Network_Edit_Tracker_insert(" (من واجهة إدارة الأصناف) تعديل بيانات الصنف : " + MetroGrid.CurrentRow.Cells("IM_NAME_CL").Value.ToString + " / سعر البيع : " + MetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, 0, F_ItemsMenu.IM_ID, 0)


            Network_Edit_Tracker_insert(" الصنف:" & MetroGrid.CurrentRow.Cells("IM_Name_CL").Value.ToString + " سعر البيع:" + MetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, 0, 20, 3)
            'Fill_All_IM()
        End If

    End Sub

    Private Sub SelectLast_ROW()
        If MetroGrid.Rows.Count > 0 Then MetroGrid.CurrentCell = MetroGrid.Rows(MetroGrid.Rows.Count - 1).Cells("IM_NAME_CL")
    End Sub


    Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
        Dim dv As DataView
        dv = IM_Dt.DefaultView
        dv.RowFilter = "Item_Name like '%" & IM_SH_txt.Text & "%'"
    End Sub

    Private Sub IM_Fast_Mang_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub


    'Private Sub MetroGrid_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles MetroGrid.CellValidating
    '    If e.ColumnIndex = 5 Then
    '        Dim i As Integer

    '        If Not Integer.TryParse(Convert.ToString(e.FormattedValue), i) Then
    '            e.Cancel = True
    '            MsgBox("يحتوي سعر الصنف على أرقام فقط", MsgBoxStyle.Exclamation, "")
    '        Else
    '        End If
    '    End If
    'End Sub

    Private Sub GM_Serach_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GM_Serach.SelectedIndexChanged
        If TypeName(GM_Serach.SelectedValue) = "Integer" Then Fill_All_IM()
    End Sub
End Class