Imports System.Data.SqlClient

Public Class IM_Negitave_QTY
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
        'DataGridViewX.Columns("Barcode_CL").Visible = MY_Settings.IMPR_BAR
        'DataGridViewX.Columns("Price_2_CL").Visible = My_Settings.IMPR_MINSP
        'DataGridViewX.Columns("Price_3_CL").Visible = My_Settings.IMPR_MINSP_2


        'DataGridViewX.Columns("Cost_CL").Visible = U_SB_Show_IM_COST
    End Sub


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
            .CommandText = "Items_Prices_V_SELECT_ALL_Negitave_QTY"
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(IM_DT)
        DataGridViewX.DataSource = IM_DT

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Fill_ALL_IM()
    End Sub

    Private Sub CMSearchTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles CMSearchTextBox.KeyDown
        If e.KeyCode = Keys.Delete Then CMSearchTextBox.Clear()
    End Sub


    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

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