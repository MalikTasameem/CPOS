Imports System.Data.SqlClient

Public Class Query_Form

    Private Sub Encrypt_Pass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Con_txt.Text = MY_Settings.SqlConStr
    End Sub

    Private Sub CHECK_CONNECTION_ON_REAL_TIME()
        Me.Cursor = Cursors.AppStarting
        'check_cmb()
        is_CHECKED_LB.Visible = True
        If CheckConnection(Con_txt.Text) Then
            is_CHECKED_LB.BackColor = Color.DarkSeaGreen
            is_CHECKED_LB.Text = "متصـــل"
        Else
            is_CHECKED_LB.BackColor = Color.LightGray
            is_CHECKED_LB.Text = "غير متصــل"
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Public Function CheckConnection(Cs As String)
        Dim bb As New SqlConnection
        Try

            bb.ConnectionString = Cs
            'MsgBox(bb.ConnectionTimeout)
            bb.Open()
            bb.Close()

            Return True
        Catch ex As Exception
            bb.Close()
            Return False
        End Try
    End Function

    'Private Sub fetch_normal_btn_Click(sender As Object, e As EventArgs) Handles fetch_normal_btn.Click
    '    fill_List()
    'End Sub

    'Public Sub fill_List()
    '    Dim C As New C
    '    Dim sqlCon = New SqlConnection(Con_txt.Text)
    '    Dim sql As String = "select user_id,UserPass from Users ORDER BY UserName ASC"
    '    Dim com As New SqlDataAdapter(sql, sqlCon)
    '    com.Fill(C.Dt)
    '    normal_grid.DataSource = C.Dt

    'End Sub

  

    'Private Sub enc_btn_Click(sender As Object, e As EventArgs) Handles enc_btn.Click
    '    'enc_grid.DataSource = Nothing
    '    For i = 0 To normal_grid.Rows.Count - 1
    '        enc_grid.Rows.Add(normal_grid.Rows(i).Cells(0).Value, EncryptionHelper.Encrypt(normal_grid.Rows(i).Cells(1).Value))
    '    Next
    'End Sub

    Private Sub save_enc_btn_Click(sender As Object, e As EventArgs) Handles save_enc_btn.Click

        Dim sqlCon = New SqlConnection(Con_txt.Text)

        '  On Error Resume Next
        Dim C As New C
        C.Com = New SqlCommand(Query_txt.Text, sqlCon)
        sqlCon.Open()
        Try
            C.Com.ExecuteNonQuery()
            MsgBox("تم التنفيذ بنجاح", MsgBoxStyle.Information, "")
            sqlCon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            sqlCon.Close()

        End Try

    End Sub

    Private Sub Check_Btn_Click(sender As Object, e As EventArgs) Handles Check_Btn.Click
        CHECK_CONNECTION_ON_REAL_TIME()
    End Sub

    Private Sub Query_Form_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Con_txt.Text = "Data Source= localhost ;initial catalog='MASTER';User Id='sa';Password='123'"
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        Query_txt.Text = "CREATE DATABASE [" & MY_Settings.DataBase & "] ON " & _
" ( FILENAME = N'" & Application.StartupPath & "\DB\" & MY_Settings.DataBase & ".mdf' ), " & _
" ( FILENAME = N'" & Application.StartupPath & "\DB\" & MY_Settings.DataBase & "_log.ldf' ) " & _
 " FOR ATTACH"
    End Sub
End Class