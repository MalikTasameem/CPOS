Imports System.Data.SqlClient

Public Class TablesCard
    Dim rs As New Resizer

    Private Sub control_tables_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub TablesCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        Load_Flates()
        Load_Tables()
    End Sub

    Public Sub Load_Tables()
        Dim c As New C
        Dim s As String = "select TB_ID,Flate_ID,T_Name,Flate_Name,is_Cash from Tables_Balances_V Order By TB_ID ASC "
        c.Da = New SqlDataAdapter(s, c.Con)
        c.Da.Fill(c.Dt)
        TBGrid.DataSource = c.Dt
    End Sub

    Private Sub control_tables_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls_POS(Me)
    End Sub

    Public Sub Load_Flates()
        Dim c As New C
        Dim s As String = "select Flate_ID,Flate_Name from Tables_Flate"
        c.Da = New SqlDataAdapter(s, c.Con)
        c.Da.Fill(c.Dt)
        Flate_cmb.DataSource = c.Dt
        Flate_cmb.DisplayMember = "Flate_Name"
        Flate_cmb.ValueMember = "Flate_ID"
        Flate_cmb.SelectedIndex = 0
    End Sub

    Private Sub TablesNumButton_Click(sender As Object, e As EventArgs) Handles TablesNumButton.Click

        Me.Cursor = Cursors.AppStarting
        Dim Num As String = TableNameTxt.Text

        If Total_CB.Checked = True Then
            If Check_isbusy() = 1 Then
                MsgBox("توجد طاولات مشغولة ... قم بإغلاقها أولا", MsgBoxStyle.Critical)
            Else
                '  Tables_DeleteALL()

                For i = 1 To Convert.ToInt16(Num)
                    TableNameTxt.Text = i
                    Tables_insert(Get_Max_Table_Name)
                Next


                Total_CB.Checked = False
                'Catch ex As Exception
                '    MsgBox("يجب ان يحتوي الحقل على ارقام فقط")
                'End Try
            End If
        Else
            Tables_insert(Num)
        End If

        'F_TablesBalance.loadtables()
        TableNameTxt.Clear()
        Me.Cursor = Cursors.Default
    End Sub

    Private Function Get_Max_Table_Name()
        Dim C As New C
        Dim S As String = "Select ISNULL(MAX(TB_ID),0) + 1 As MX From Tables"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Return C.Dr("MX")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return 1
    End Function


    Public Function Check_isbusy()
        Dim C As New C
        Dim S As String = "Select COUNT(*) As Num From Tables_Balances_V Where isbusy = 1 "
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                If C.Dr("Num") > 0 Then

                    Return 1
                Else
                    Return 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return 0
    End Function

    Private Sub Tables_insert(TB_Name As String)
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Tables_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_Name", TB_Name)
            .Parameters.AddWithValue("@Flate_ID", Flate_cmb.SelectedValue)
            .Parameters.AddWithValue("@is_Cash", is_Auto_Pied_CB.Checked)
        End With
        If SQL_SP_EXEC(c.Com) = True Then Load_Tables()
    End Sub

    Private Sub Tables_DeleteALL()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Tables_DeleteALL"
            .CommandType = CommandType.StoredProcedure
        End With
        SQL_SP_EXEC(c.Com)
    End Sub

    Private Sub Tables_delete()
        Dim c As New C
        Dim sqlCon = New SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlCommand()
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "Tables_delete"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@TB_ID", TBGrid.CurrentRow.Cells("TB_ID_CL").Value)
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
                Load_Tables()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Private Sub TableNameTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TableNameTxt.KeyPress
        'Check_Only_Int(sender, e)
    End Sub

    Private Sub TablesNumTextBox_TextChanged(sender As Object, e As EventArgs) Handles TableNameTxt.TextChanged
        If String.IsNullOrWhiteSpace(sender.text) Then
            TablesNumButton.Enabled = False
        Else
            TablesNumButton.Enabled = True
        End If
    End Sub



    Private Sub Total_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Total_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Tables_Flates.ShowDialog()
    End Sub

    Private Sub TBGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles TBGrid.CellContentClick
        If e.ColumnIndex = 0 Then
            Beep()
            If MessageBox.Show(" حذف الطاولة " + TBGrid.CurrentRow.Cells("T_Name_CL").Value.ToString, "", MessageBoxButtons.YesNo, _
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                Tables_delete()
            End If

        End If
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub is_Auto_Pied_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Auto_Pied_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub
End Class