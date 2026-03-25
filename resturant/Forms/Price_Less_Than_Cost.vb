Imports System.Data.SqlClient

Public Class Price_Less_Than_Cost
    Dim rs As New Resizer
    Dim IM_DT As New DataTable
    Dim Get_IM As Boolean = False


    Public is_Update_Valid_D As Boolean = False
    Public is_Update_QTY As Boolean = False
    Public is_Update_Unit_Cost As Boolean = False

    Private Sub STORES_Explorer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'F_MainForm.Fill_ALL_IM()
        Me.Dispose()
    End Sub
    Private Sub STORES_Explorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        'fetch_ST()
        'fetch_GM()
        Get_IM = True
        Check_View_Control()
        Fill_ALL_IM()
        Make_Hints()
    End Sub

    Private Sub Make_Hints()
        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن صنف")
    End Sub

    Private Sub Check_View_Control()

        DataGridViewX.Columns("IM_Num_CL").Visible = My_Settings.IMPR_IMNUM
        DataGridViewX.Columns("Barcode_CL").Visible = My_Settings.IMPR_BAR
        DataGridViewX.Columns("Price_2_CL").Visible = My_Settings.IMPR_MINSP
        DataGridViewX.Columns("Price_3_CL").Visible = My_Settings.IMPR_MINSP_2


        DataGridViewX.Columns("Cost_CL").Visible = U_SB_Show_IM_COST
    End Sub

    'Public Sub fetch_GM()
    '    GM_Serach.DataSource = GetMailItems_GM()
    '    GM_Serach.DisplayMember = "name"
    '    GM_Serach.ValueMember = "ID"
    '    GM_Serach.SelectedIndex = 0
    'End Sub

    Function GetMailItems_GM() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "------- كل التصنيفات -------"))

        Dim c1 As New C
        Dim s As String = "select GM_ID AS ID,GM_NAME AS name from General_menu ORDER By GM_ID ASC"
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

    'Public Sub fetch_ST()
    '    ST_cm.DataSource = GetMailItems()
    '    ST_cm.DisplayMember = "name"
    '    ST_cm.ValueMember = "ID"
    '    ST_cm.SelectedIndex = 0
    'End Sub

    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "------- كل المخازن -------"))

        Dim c1 As New C
        Dim s As String = "select ST_ID AS ID,ST_name AS name from STORES ORDER By ST_ID ASC"
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

    Public Sub Fill_ALL_IM()

        IM_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[Items_Prices_V_SELECT_ALL_Less_Then_Cost]"
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(IM_DT)
        DataGridViewX.DataSource = IM_DT

        '  Get_Total_QYT()
    End Sub


    Private Sub STORES_Explorer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub CMSearchTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CMSearchTextBox.KeyPress
        If e.KeyChar = "'" Then e.Handled = True
    End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged
        Dim Dv As DataView
        Dv = IM_DT.AsDataView
        Dv.RowFilter = "item_name LIKE '%" + sender.Text + "%'"
        DataGridViewX.DataSource = Dv
        '   Get_Total_QYT()
    End Sub

    'Public Sub Get_Total_QYT()
    '    If DataGridViewX.BackgroundColor <> Color.IndianRed Then
    '        Dim Cost As Double = 0
    '        Dim Qty As Double = 0
    '        For i = 0 To DataGridViewX.Rows.Count - 1
    '            Cost += DataGridViewX.Rows(i).Cells("T_Cost_CL").Value
    '            Qty += DataGridViewX.Rows(i).Cells("QTY_CL").Value
    '        Next
    '        TotalQYT_txt.Text = Qty.ToString("N")
    '        TotalCost_txt.Text = Cost.ToString("N")
    '    End If

    'End Sub

    Private Sub Store_R_Insert()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            c.Com = New SqlClient.SqlCommand
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "STORES_R_DELETE"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        sqlCon.Close()
        '***********************************************************************

        For i = 0 To DataGridViewX.Rows.Count - 1
            c = New C
            sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
            Using (sqlCon)
                Dim sqlComm As New SqlClient.SqlCommand()
                c.Com = New SqlClient.SqlCommand
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "STORES_R_INSERT"
                sqlComm.CommandType = CommandType.StoredProcedure

                sqlComm.Parameters.AddWithValue("@CM_NAME", DataGridViewX.Rows(i).Cells("item_name_CL").Value)
                sqlComm.Parameters.AddWithValue("@QYT", DataGridViewX.Rows(i).Cells("QTY_CL").Value)
                sqlComm.Parameters.AddWithValue("@Unit", DataGridViewX.Rows(i).Cells("Unit_CL").Value)
                sqlComm.Parameters.AddWithValue("@Cost", DataGridViewX.Rows(i).Cells("Cost_CL").Value)
                sqlCon.Open()
                Try
                    sqlComm.ExecuteNonQuery()
                    ' MsgBox("تـــم الحذف ", MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            sqlCon.Close()
        Next


    End Sub

    'Public Sub STORE_R_Print()

    '    Dim pp As New ReportConnection
    '    pp.rp.Load(Application.StartupPath & "\reports\STORES_QYT_A4.rpt")
    '    pp.LoadTables()
    '    '-------------------------
    '    pp.rp.SetParameterValue(0, USER_NAME)
    '    pp.rp.SetParameterValue(1, My_Settings.Server_Desc)
    '    '  pp.rp.SetParameterValue(2, TotalCost_txt.Text)
    '    pp.rp.SetParameterValue(3, TotalQYT_txt.Text)

    '    pp.rp.SetParameterValue(4, "  المخزن : ( " + ST_cm.Text + " ) " + "  ---  " + "  التصنيف : ( " + GM_Serach.Text + " ) ")
    '    pp.rp.SetParameterValue(5, ST_cm.SelectedValue)
    '    pp.rp.SetParameterValue(6, GM_Serach.SelectedValue)

    '    print.CrystalReportViewer1.ReportSource = pp.rp
    '    print.Show()

    '    'pp.rp.PrintOptions.PrinterName = Default_Printer_A4
    '    'pp.rp.PrintToPrinter(1, False, 0, 0)
    '    'pp.rp.Dispose()

    'End Sub




    Private Sub IM_btn_Click(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.AppStarting
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '  Fetch_QTY()
        Fill_ALL_IM()
    End Sub

    'Private Sub BarcodeSearch_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    CMSearchTextBox.Select()
    '    If BarcodeSearch_CB.Checked = True Then IMNUM_CB.Checked = False
    'End Sub

    Private Sub CMSearchTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles CMSearchTextBox.KeyDown
        'If e.KeyCode = Keys.Return And (BarcodeSearch_CB.Checked = True Or IMNUM_CB.Checked = True) Then Search_By_Barcode()
        If e.KeyCode = Keys.Delete Then CMSearchTextBox.Clear()
    End Sub

    'Public Sub Search_By_Barcode()

    '    Dim IM_ID As Integer
    '    Dim c As New C
    '    IM_DT.Clear()
    '    Try
    '        Dim s As String = ""

    '        If IMNUM_CB.Checked Then s = "select IM_ID from IM_Menu WHERE IM_NUM = '" & CMSearchTextBox.Text & "'"
    '        If BarcodeSearch_CB.Checked Then s = "select IM_ID from IM_units_Search_V WHERE Barcode = '" & CMSearchTextBox.Text & "'"

    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()

    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            IM_ID = c.Dr("IM_ID")
    '            Fill_ALL_IM()
    '        Else
    '            MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub



    'Private Sub Up_Update_btn_Click(sender As Object, e As EventArgs)
    '    If SHhow_Zero_Qty_btn.BackColor = Color.WhiteSmoke Then
    '        If DataGridViewX.Rows.Count > 0 Then
    '            is_Update_Valid_D = False
    '            is_Update_QTY = True
    '            is_Update_Unit_Cost = False
    '            IM_Update_Qty.ShowDialog()
    '        End If
    '    Else
    '        If DataGridViewX.Rows.Count > 0 Then
    '            MsgBox(" يمكنك تعديل بيانات الصنف الذي كميته 0 عن طريق فاتورة جرد ", MsgBoxStyle.Information)
    '        End If
    '    End If
    'End Sub


    Private Sub GM_Serach_SelectedValueChanged(sender As Object, e As EventArgs)
        '   Fetch_QTY()
        If Get_IM = True Then Fill_ALL_IM()
    End Sub

    'Private Sub SHhow_Zero_Qty_btn_Click(sender As Object, e As EventArgs)

    '    If SHhow_Zero_Qty_btn.BackColor = Color.GreenYellow Then
    '        SHhow_Zero_Qty_btn.BackColor = Color.WhiteSmoke
    '        DataGridViewX.BackgroundColor = Color.SeaShell
    '        DataGridViewX.RowsDefaultCellStyle.BackColor = Color.SeaShell
    '        DataGridViewX.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.Info

    '        Fill_ALL_IM(0)
    '    Else
    '        SHhow_Zero_Qty_btn.BackColor = Color.GreenYellow
    '        GM_Serach.SelectedValue = 0
    '        ST_cm.SelectedValue = 0
    '        Me.Cursor = Cursors.AppStarting
    '        Fill_ALL_ZERO()
    '        Me.Cursor = Cursors.Default
    '    End If

    'End Sub

    Public Sub Fill_ALL_ZERO()
        Try
            Dim C As New C
            IM_DT.Clear()
            C.Str = "select IM_ID,item_name,'0' AS QTY,T_Cost=0 from IM_Menu WHERE IM_ID NOT IN (SELECT IM_ID FROM ST_Balance ) AND isStore = 1 And Row_Enabled = 1"
            C.Da = New SqlDataAdapter(C.Str, C.Con)
            C.Da.Fill(IM_DT)
            If IM_DT.Rows.Count > 0 Then
                DataGridViewX.BackgroundColor = Color.LightGray
                DataGridViewX.RowsDefaultCellStyle.BackColor = Color.LightGray
                DataGridViewX.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray
                DataGridViewX.DataSource = IM_DT
            Else
                MsgBox(" لا توجد كميات = 0 ", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '  Get_Total_QYT()

    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    'Private Sub DataGridViewX_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridViewX.MouseDoubleClick
    '    If DataGridViewX.Rows.Count > 0 Then
    '        If SHhow_Zero_Qty_btn.BackColor = Color.WhiteSmoke Then
    '            If DataGridViewX.Rows.Count > 0 Then
    '                is_Update_Valid_D = False
    '                is_Update_QTY = True
    '                is_Update_Unit_Cost = False
    '                IM_Update_Qty.ShowDialog()
    '            End If
    '        Else
    '            If DataGridViewX.Rows.Count > 0 Then
    '                MsgBox(" يمكنك تعديل بيانات الصنف الذي كميته 0 عن طريق فاتورة جرد ", MsgBoxStyle.Information)
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        Dim F As New DGV_Control
        F.TabControl1.TabPages.Remove(F.OtherTabPage)
        F.TabControl1.TabPages.Remove(F.StTabPage)
        F.Show()
    End Sub

    Private Sub DataGridViewX_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridViewX.MouseDoubleClick
        If DataGridViewX.Rows.Count > 0 Then
            Show_IM_Details.IM_ID = DataGridViewX.CurrentRow.Cells("item_id_CL").Value
            Show_IM_Details.ShowDialog()
        End If
    End Sub
End Class