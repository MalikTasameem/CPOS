Public Class IM_ORDERS_COMING

    Dim Open_Dt As New DataTable
    Dim U_Dt As New DataTable

    Private Sub OPEN_Bills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Fill_Data()

        ExpandablePanel1.Visible = U_ADD_Pch


        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن صنف")
    End Sub

    Private Sub Fill_Data()
        Dim C = New C
        'Dim dt As New DataTable
        With (C.Com)
            '.Connection = C.Con
            '.CommandText = "Open_AGMV_SELECT"
            '.CommandType = CommandType.StoredProcedure
            '.Parameters.AddWithValue("@USER_ID", USER_ID)
            'If U_Save_otherBill = True Then
            '    .Parameters.AddWithValue("@IS_ADMIN", True)
            'Else
            '    .Parameters.AddWithValue("@IS_ADMIN", User_isAdmin)
            'End If

            Open_Dt.Clear()
            Dim s As String = " select T_ID,USER_ID,St_Name,Barcode,item_name,U_Name,QTY,DATE_OF_ARRIV,DAYS,MONTHS,NOTES,USERNAME  from IM_ORDERS_COMING_V ORDER BY (DAYS*-1) ASC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(Open_Dt)
            AGMetroGrid.DataSource = Open_Dt

        End With


        'Open_MV_DV.Columns(0).Visible = False
        'Open_MV_DV.Columns(3).Visible = False
        'Open_MV_DV.Columns(4).Visible = False
        'Open_MV_DV.Columns(6).Visible = False
    End Sub

    Private Sub OPEN_Bills_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Public Sub Load_IM_Barcode()

        Dim c As New C
        Try
            Dim s As String
            s = "select IM_ID from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_Cm.Set_IM_By_ID(c.Dr("IM_ID"))
            Else
                MsgBox("هذا الصنف غير موجود ضمن قائمة الأصناف", MsgBoxStyle.Exclamation, "")
                Barcode_SH_txt.SelectAll()
                Barcode_SH_txt.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IM_Cm_ID_Changed(sender As Object, e As EventArgs) Handles IM_Cm.ID_Changed
        If IM_Cm.TXT_ID.Text > 0 Then
            Fetch_IM_Units()
        Else
            U_Dt.Clear()
        End If
    End Sub

    Public Sub Fetch_IM_Units()
        ' Get_Unit = False
        Dim c As New C
        U_Dt.Clear()
        Try
            Dim s As String
            s = "select U_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & IM_Cm.TXT_ID.Text & "'"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(U_Dt)
            IM_Unit_cm.DataSource = U_Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        ' Get_Unit = True
        '   IM_Fetch_QTY()
    End Sub

    Private Sub MoveToBill_Btn_Click(sender As Object, e As EventArgs) Handles MoveToBill_Btn.Click
        IM_ORDERS_COMING_pros(0, "")
    End Sub

    Private Sub IM_ORDERS_COMING_pros(ID As Integer, Process As String)
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_ORDERS_COMING_pros"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", ID)
            .Parameters.AddWithValue("@DATE", Details_Date.Value)
            .Parameters.AddWithValue("@ST_ID", ST_Cm.TXT_ID.Text)
            .Parameters.AddWithValue("@IM_ID", IM_Cm.TXT_ID.Text)
            .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
            .Parameters.AddWithValue("@QTY", QTY_txt.Text)
            .Parameters.AddWithValue("@DATE_OF_ARRIV", Details_Date.Value)
            .Parameters.AddWithValue("@Notes", Details_Notes_txt.Text)
            .Parameters.AddWithValue("@USER_ID", USER_ID)
            .Parameters.AddWithValue("@Process", Process)

            If SQL_SP_EXEC(C.Com) Then
                MsgBox("تم التطبيق", MsgBoxStyle.Information, "")
                Fill_Data()
                Clear_Fields()
            End If
        End With
    End Sub

    Private Sub Clear_Fields()
        IM_Cm.Textt = ""
        U_Dt.Clear()
        QTY_txt.Clear()
        Details_Notes_txt.Clear()
    End Sub

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        If e.KeyCode = Keys.Return Then Load_IM_Barcode()
    End Sub

    Private Sub حذفالصفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles حذفالصفToolStripMenuItem.Click

        If U_ADD_Pch = False Then
            MsgBox("خارج صلاحياتك", MsgBoxStyle.Critical, "")
            Exit Sub
        End If

        If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("item_name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                                  MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then

            IM_ORDERS_COMING_pros(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value, "DELETE")

        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Fill_Data()
    End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged

        Dim Dv As DataView
        Dv = Open_Dt.AsDataView
        Dv.RowFilter = " item_name LIKE '%" + sender.Text + "%'"
        AGMetroGrid.DataSource = Dv


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim P As New Print_PDF
        P.PRINT_PDF(AGMetroGrid, 1, "طلبيات قادمة")
    End Sub


End Class