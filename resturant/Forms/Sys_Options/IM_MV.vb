Imports System.Data.SqlClient

Public Class IM_MV
    Dim rs As New Resizer
    Dim IM_DT As New DataTable
    Dim ST As Boolean = False

    Private Sub STORES_Explorer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub STORES_Explorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        Label3.Text = "كشف حركة: " + F_ItemsMenu.IM_Name_ToolStrip.Text
        fetch_ST()
        ST = True
        Fill_ST_IM()
        For i = 0 To DataGridViewX.Columns.Count - 1
            DataGridViewX.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i
    End Sub


    Public Sub fetch_ST()
        ST_cm.DataSource = GetMailItems()
        ST_cm.DisplayMember = "name"
        ST_cm.ValueMember = "ID"
        ST_cm.SelectedValue = SB_ST_ID
    End Sub

    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        '   mailItems.Add(New MailItem(0, "------- كل المخازن -------"))

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

    Public Sub Fill_ST_IM()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_MV_SELECT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@ALL_TIME", 0)
            .Parameters.AddWithValue("@D_F", DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", DateRange_Flate.D_T.Value)

            .Parameters.AddWithValue("@IM_ID", F_ItemsMenu.IM_ID)

        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        DataGridViewX.DataSource = C.Dt
        Calc_Balance()
        '   Get_Total_QYT()
    End Sub

    Private Sub STORES_Explorer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

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

    Public Sub STORE_R_Print()

        'Dim pp As New ReportConnection
        'pp.rp.Load(Application.StartupPath & "\reports\STORES_QYT_A4.rpt")
        'pp.LoadTables()
        ''-------------------------
        'pp.rp.SetParameterValue(0, USER_NAME)
        'pp.rp.SetParameterValue(1, My_Settings.Server_Desc)
        'pp.rp.SetParameterValue(2, TotalCost_txt.Text)
        'pp.rp.SetParameterValue(3, TotalQYT_txt.Text)

        'pp.rp.SetParameterValue(4, "  المخزن : ( " + ST_cm.Text + " ) " + "  ---  " + "  التصنيف : ( " + GM_Serach.Text + " ) ")
        'pp.rp.SetParameterValue(5, ST_cm.SelectedValue)
        'pp.rp.SetParameterValue(6, GM_Serach.SelectedValue)

        'print.CrystalReportViewer1.ReportSource = pp.rp
        'print.Show()

    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        If DataGridViewX.Rows.Count > 0 Then STORE_R_Print()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Fill_ST_IM()
    End Sub

    Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
        If ST = True Then Fill_ST_IM()
    End Sub



    Private Sub DataGridViewX_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridViewX.MouseDoubleClick
        Me.Cursor = Cursors.AppStarting
        If DataGridViewX.Rows.Count > 0 Then
            If DataGridViewX.CurrentRow.Cells("Type_ID_CL").Value <> 6 Then
                Select Case DataGridViewX.CurrentRow.Cells("Type_ID_CL").Value
                    'Case 3, 4, 5
                    '    isShowing_Trans = True
                    '    F_Receipt = New Receipt
                    '    F_Receipt.ShowDialog()
                    '    isShowing_Trans = False
                    '    FormType = 0
                    Case 1
                        FormType = 1
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        'F_Balances.AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        F_Sales = New Sales
                        F_Sales.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0
                    Case 7
                        FormType = 2
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        F_Pch = New Pch
                        F_Pch.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                    Case 8
                        FormType = 3
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        F_IM_Execute = New IM_Execute
                        F_IM_Execute.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                    Case 9
                        FormType = 4
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        F_Invoice = New Invoice
                        F_Invoice.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0
                    Case 10
                        FormType = 5
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        Returns.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                    Case 11
                        FormType = 6
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        Returns.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                    Case 12
                        FormType = 7
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        F_Stores_ImmediateOrder = New Stores_ImmediateOrder
                        F_Stores_ImmediateOrder.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                    Case 2
                        FormType = 7
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        F_EXP_Details = New EXP_Details
                        F_EXP_Details.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                    Case 13
                        FormType = 9
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        F_Format_Items_Auto = New Format_Items_Auto
                        F_Format_Items_Auto.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                    Case 16
                        FormType = 10
                        isShowing_Trans = True
                        F_ViewBill = New ViewBill
                        F_ViewBill.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                    Case 17
                        FormType = 11
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        F_Inside_Sales = New Inside_Sales
                        F_Inside_Sales.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                    Case 18
                        FormType = 9
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        F_Format_Items_Manual = New Format_Items_Manual
                        F_Format_Items_Manual.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                    Case 35
                        FormType = 13
                        T_ID_Trans = DataGridViewX.CurrentRow.Cells("T_ID_CL").Value
                        isShowing_Trans = True
                        F_Outside_Sales = New Outside_Sales
                        F_Outside_Sales.ShowDialog()
                        isShowing_Trans = False
                        FormType = 0

                End Select
            End If
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub Calc_Balance()
        Dim Sum_D As Double = 0
        Dim Sum_C As Double = 0
        Dim Sum_B As Double = 0

        For i = 0 To DataGridViewX.Rows.Count - 1
            If Not IsDBNull(DataGridViewX.Rows(i).Cells("Pluse_CL").Value) Then Sum_D = Sum_D + DataGridViewX.Rows(i).Cells("Pluse_CL").Value
            If Not IsDBNull(DataGridViewX.Rows(i).Cells("Negitave_CL").Value) Then Sum_C = Sum_C + DataGridViewX.Rows(i).Cells("Negitave_CL").Value
        Next

        Sum_B = Sum_D - Sum_C
        T_PluseTextBox.Text = Sum_D.ToString("n")
        T_NegitaveTextBox.Text = Sum_C.ToString("n")
        T_BalanceTextBox.Text = Sum_B.ToString("n")
    End Sub


End Class