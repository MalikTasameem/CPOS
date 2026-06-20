Imports System.Data.SqlClient
Public Class MONTHS_CALENDR_Update


    Public Sub MONTHS_CALENDR_Update()

        Dim c = New C

        Using (c.Con)
            Dim sqlComm As New SqlCommand()
            sqlComm.Connection = c.Con
            sqlComm.CommandText = "MONTHS_CALENDR_Update"
            sqlComm.CommandType = CommandType.StoredProcedure

            With sqlComm
                .Parameters.AddWithValue("@M_FROM", DATETIME_F.Value)
                .Parameters.AddWithValue("@M_TO", DATETIME_TO.Value)
                .Parameters.AddWithValue("@M_ID", M_ID) 'F_MONTHS_CALENDR.DataGridViewX1.CurrentRow.Cells("M_ID_CL").Value
                .Parameters.AddWithValue("@YEAR", YEAR_) 'F_MONTHS_CALENDR.YEAR_Cm.Text
            End With

            c.Con.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تم التعديل", MsgBoxStyle.Information)
                'F_MONTHS_CALENDR.Load_MONTHS()
                Me.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            c.Con.Close()
        End Using


    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        MONTHS_CALENDR_Update()
    End Sub

    Public is_Close As Boolean
    Public M_ID As Int16
    Public YEAR_ As Int16
    Private Sub MONTHS_CALENDR_Update_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Text = F_MONTHS_CALENDR.DataGridViewX1.CurrentRow.Cells("M_NAME_CL").Value & " سنة : " & F_MONTHS_CALENDR.YEAR_Cm.Text
        'DATETIME_F.Value = F_MONTHS_CALENDR.DataGridViewX1.CurrentRow.Cells("M_FROM_CL").Value
        'DATETIME_TO.Value = F_MONTHS_CALENDR.DataGridViewX1.CurrentRow.Cells("M_TO_CL").Value



        If is_Close = False Then
            Close_Btn.Enabled = True
            Open_Btn.Enabled = False
        Else
            Close_Btn.Enabled = False
            Open_Btn.Enabled = True
        End If

    End Sub

    Private Sub Open_Btn_Click(sender As Object, e As EventArgs) Handles Open_Btn.Click
        query("UPDATE MONTHS_CALENDR SET is_Close = 0 WHERE YEAR = " & YEAR_ & " AND M_ID = " & M_ID)
        F_MONTHS_CALENDR.Load_MONTHS()
        Me.Close()
    End Sub

    Private Sub Close_Btn_Click(sender As Object, e As EventArgs) Handles Close_Btn.Click
        query("UPDATE MONTHS_CALENDR SET is_Close = 1 WHERE YEAR = " & YEAR_ & " AND M_ID = " & M_ID)
        F_MONTHS_CALENDR.Load_MONTHS()
        Me.Close()
    End Sub

    Private Sub Back_btn_Click(sender As Object, e As EventArgs) Handles Back_btn.Click
        Me.Close()
    End Sub
End Class