Public Class Income_Statement_QUART
    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click

        'If All_TimeCB.Checked = True Then
        '    GET_TREE_BALANCE(0, "1900-01-01", "2900-01-01")
        'Else
        '    GET_TREE_BALANCE(0, Date_F.Value, Date_T.Value)
        'End If


        Income_Statement_BY_QUART_YEAR()
    End Sub


    Private Async Sub Income_Statement_BY_QUART_YEAR()

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[Income_Statement_BY_QUART_YEAR]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@YEAR", B_Name_Cm.Text)
            .Parameters.AddWithValue("@ORG_ST_ACC_CODE", MY_Settings.ORG_ST_ACC_CODE)
            .Parameters.AddWithValue("@Income_ST_ACC_CODE", MY_Settings.Income_ST_ACC_CODE)
        End With
        'SQL_SP_EXEC(C.Com)


        CircularPanel.Visible = True
        CircularProgressControl1.Start()
        Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr))
        SELECT_Balance()
        Coloring()

        CircularPanel.Visible = False
        CircularProgressControl1.Stop()


        'SELECT_Balance()



    End Sub


    Public Sub SELECT_Balance()
        Dim C As New C
        C.Da = New SqlClient.SqlDataAdapter("SELECT [T_ID] --AS 'ت'
      ,[ACC_NAME] --AS ' إسم الحساب '
      ,[ACC_CODE] --AS ' كود الحساب '
      ,[FIRST_QUART_INCOME] --AS ' الربــع الأول '
      ,[SECOND_QUART_INCOME]  --AS ' الربــع الثاني '
      ,[FIRST_QUART_CHANGE_PERCENT_BETWEEN_FIRST_AND_SECOND] --AS 'نسبة التغيــر'
      ,[THIRD_QUART_INCOME]  --AS ' الربــع الثالــث '
      ,[FOURTH_QUART_INCOME]  --AS ' الربــع الرابــع '
      ,[FIRST_QUART_CHANGE_PERCENT_BETWEEN_THIRD_AND_FOURTH]  --AS 'نسبة التغيــر'
  FROM [dbo].[Balance_sheet_OVER_YEAR]", C.Con)

        C.Da.Fill(C.Dt)
        DataGridView1.DataSource = C.Dt
        Coloring()
    End Sub




    Private Sub Coloring()

        For i = 0 To DataGridView1.Rows.Count - 1


            If Not IsDBNull(DataGridView1.Rows(i).Cells("CHANGE_1_2_CL").Value) Then

                Dim S = Replace(DataGridView1.Rows(i).Cells("CHANGE_1_2_CL").Value, "%", "")
                If S > 0 Then
                    DataGridView1.Rows(i).Cells("CHANGE_1_2_CL").Style.ForeColor = Drawing.Color.DarkGreen
                ElseIf S < 0 Then
                    DataGridView1.Rows(i).Cells("CHANGE_1_2_CL").Style.ForeColor = Drawing.Color.DarkRed
                End If
            End If


            If Not IsDBNull(DataGridView1.Rows(i).Cells("CHANGE_3_4_CL").Value) Then

                Dim S = Replace(DataGridView1.Rows(i).Cells("CHANGE_3_4_CL").Value, "%", "")
                If S > 0 Then
                    DataGridView1.Rows(i).Cells("CHANGE_3_4_CL").Style.ForeColor = Drawing.Color.DarkGreen
                ElseIf S < 0 Then
                    DataGridView1.Rows(i).Cells("CHANGE_3_4_CL").Style.ForeColor = Drawing.Color.DarkRed
                End If
            End If

        Next
    End Sub

    Private Sub Balance_sheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        DataGridView1.DefaultCellStyle.SelectionBackColor = DataGridView1.DefaultCellStyle.BackColor
        DataGridView1.DefaultCellStyle.SelectionForeColor = DataGridView1.DefaultCellStyle.ForeColor

        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None
        DataGridView1.BorderStyle = BorderStyle.None

        Load_YEAR()
    End Sub



    Public Sub Load_YEAR()

        Dim C As New C
        C.Da = New SqlClient.SqlDataAdapter(" SELECT DISTINCT YEAR([DATE]) AS 'Y' FROM [AGENTS_Billing_TMP_0_V] ", C.Con)
        C.Da.Fill(C.Dt)

        B_Name_Cm.DataSource = C.Dt
        B_Name_Cm.DisplayMember = "Y"

    End Sub

    Private Sub Print_Btn_Click(sender As Object, e As EventArgs) Handles Print_Btn.Click

        Dim f As New Print_PDF
        f.PRINT_PDF(DataGridView1, 1, TITLE_txt.Text & " - لسنــة " & "(" & B_Name_Cm.Text & ")")

    End Sub



End Class