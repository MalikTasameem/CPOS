Imports System.Data.SqlClient

Public Class Show_IM_Details

    Dim rs As New Resizer
    Public IM_ID As Integer
    Private Sub Yes_Btn_Click(sender As Object, e As EventArgs) Handles Yes_Btn.Click
        Me.Close()
    End Sub

    Private Sub Show_IM_Details_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Show_IM_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Fetch_IM()
    End Sub

    Private Sub Show_IM_Details_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Fetch_IM()
        Dim c As New C
        Dim S As String

        S = "select * from IM_All_With_No_Enable_V where IM_ID ='" & IM_ID & "'"

        c.Com = New SqlCommand(S, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                Me.IM_SH_txt.Text = c.Dr("item_name")
                Me.GM_Name_txt.Text = c.Dr("GM_Name")
                Me.IM_Num_txt.Text = c.Dr("IM_NUM")
                If IsDBNull(c.Dr("IM_Full_Photo")) = False Then
                    Try
                        Me.IM_Photo.Image = Image.FromFile(System.IO.Path.GetFullPath(c.Dr("IM_Full_Photo")))
                    Catch ex As Exception
                        MsgBox("تأكد من مسار الصورة" + vbNewLine + ex.Message, MsgBoxStyle.Exclamation, "")
                    End Try
                Else
                    Me.IM_Photo.Image = Nothing
                    Me.IM_Photo.BackColor = System.Drawing.SystemColors.ButtonFace
                End If

                Me.Notes_txt.Text = c.Dr("Notes")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

    End Sub

End Class