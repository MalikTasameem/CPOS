Public Class SB_Contents_AGENTS

    Public SB_T_ID
    Public IM_NAME As String = ""

    Private Sub Rsv_IM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IM_LB.Text = IM_NAME
        SB_Rsv_SELECT()
        Load_comisions()
    End Sub

    Public Sub Load_comisions()
        Dim c As New C
        Try
            Dim s As String
            s = "select ID,VALUE_Bercent from SB_VALUE_Bercent_Card "
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            If c.Dt.Rows.Count > 0 Then
                Comision_cm.DataSource = c.Dt
                Comision_cm.DisplayMember = "VALUE_Bercent"
                Comision_cm.ValueMember = "ID"
                Comision_cm.SelectedIndex = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        SB_Rsv_Pros(SB_T_ID, "")
        'query("INSERT INTO [dbo].[SB_Rsv]([T_ID],[DATE_F]) VALUES (" & F_Sales.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value & ",'" & Date_F.Value & "')")
    End Sub


    Private Sub SB_Rsv_Pros(ID As Integer, Process As String)
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_VALUE_Bercent_Details_pros"
            .CommandType = CommandType.StoredProcedure
            '.Parameters.AddWithValue("@T_ID", F_Sales.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            .Parameters.AddWithValue("@SB_T_ID", SB_T_ID)
            .Parameters.AddWithValue("@AG_ID", AG_Cm.TXT_ID.Text)
            .Parameters.AddWithValue("@VALUE_Bercent", Comision_cm.Text)
            .Parameters.AddWithValue("@Process", Process)
            If SQL_SP_EXEC(C.Com) Then SB_Rsv_SELECT()
        End With
    End Sub


    Public Sub SB_Rsv_SELECT()

        Try
            Dim C As New C
            Dim s As String
            s = "select SB_T_ID,Ag_name,VALUE_Bercent  from SB_VALUE_Bercent_Details_V  WHERE SB_T_ID = " & SB_T_ID
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(C.Dt)
            AGMetroGrid.DataSource = C.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        If AGMetroGrid.Rows.Count > 0 Then SB_Rsv_Pros(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value, "DELETE")
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub
End Class