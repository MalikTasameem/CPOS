Imports System.Data.SqlClient
Public Class EMP_Add_Periods

    Dim AG_NAME As String
    'Dim T_ID As Integer = 0
    Dim EditState As String = ""
    Dim Emp_Cse_Dt As New DataTable

    Private Sub Sign_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub


    Private Sub Sign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i = 0 To DataGridView1.Columns.Count - 1
            DataGridView1.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i
        'AG_Cm.Focus()
        Fill_Currencies()

        SendMessage(IM_SH_txt.Handle, &H1501, 0, "إبحث عن اسم عملة")
    End Sub


    Private Sub Fill_Currencies()
        Dim C As New C
        'Dim DT_3 As New DataTable
        C.Da = New SqlClient.SqlDataAdapter("select Cr_ID , Cr_Name  from Currency WHERE CR_ID > 1  ", C.Con)
        C.Da.Fill(C.Dt)
        Currency_Cm.DataSource = C.Dt
        Currency_Cm.DisplayMember = "Cr_Name"
        Currency_Cm.ValueMember = "Cr_ID"
    End Sub


    Public Sub fetch_EMP_Sign()
        Try
            Emp_Cse_Dt.Clear()
            Dim C As New C
            Dim s As String = "select T_ID,CR_NAME,Price,BuyPrice,D_F,D_T from Currency_Schedule_V WHERE CR_ID = " & Currency_Cm.SelectedValue & "  ORDER BY  D_F ASC"
            C.Da = New SqlDataAdapter(s, C.Con)
            C.Da.Fill(Emp_Cse_Dt)
            DataGridView1.DataSource = Emp_Cse_Dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        If Currency_Cm.SelectedValue > 0 Then

            If String.IsNullOrWhiteSpace(Currency_Equal_txt.Text) Or Currency_Equal_txt.Text = "0" Then
                MsgBox("حدد سعر الصرف", MsgBoxStyle.Exclamation, "")
                Currency_Equal_txt.Focus()
                Exit Sub
            End If

            EMP_Periods_Schedule_pros(0, "")
        End If
    End Sub

    Sub EMP_Periods_Schedule_pros(T_ID As Integer, Process As String)

        Dim C As New C

        With C.Com
            .CommandText = "[Currency_Schedule_pros]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
            .Parameters.AddWithValue("@Price", Currency_Equal_txt.Text)
            .Parameters.AddWithValue("@BuyPrice", Currency_Buy_txt.Text)
            .Parameters.AddWithValue("@CR_ID", Currency_Cm.SelectedValue)
            .Parameters.AddWithValue("@D_F", Date_From.Value)
            .Parameters.AddWithValue("@Process", Process)
        End With

        If SQL_SP_EXEC(C.Com) = True Then

            fetch_EMP_Sign()

        End If

    End Sub



    Private Sub New_Btn_Click(sender As Object, e As EventArgs)
        clearing()
        ' Enable_Fields()
        'AG_Cm.Focus()
        SaveButton.Enabled = True
    End Sub




    Public Sub clearing()
        For Each a As Control In Me.Panel1.Controls
            If TypeOf a Is TextBox Then
                a.Text = Nothing
            End If
        Next

        '  AG_Cm.Textt = ""
        'VoidLb.Visible = False
        Emp_Cse_Dt.Clear()
    End Sub




    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()

    End Sub

    Private Sub Cancel_Btn_Click(sender As Object, e As EventArgs) Handles Cancel_Btn.Click
        If DataGridView1.Rows.Count > 0 Then
            If MessageBox.Show("إلغــاء الحالة بشكل نهائــي ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) _
                = Windows.Forms.DialogResult.Yes Then
                Currency_Equal_txt.Text = "0"
                EMP_Periods_Schedule_pros(DataGridView1.CurrentRow.Cells("T_ID_CL").Value, "DELETE")
            End If
        End If
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button.Click
        fetch_EMP_ALL()
    End Sub

    Public Sub fetch_EMP_ALL()
        Try
            Emp_Cse_Dt.Clear()
            Dim C As New C
            Dim s As String = "select T_ID,CR_NAME,PRICE,BuyPrice,D_F,D_T from Currency_Schedule_V WHERE CR_ID = " & Currency_Cm.SelectedValue & " ORDER BY CR_ID,D_F DESC"
            C.Da = New SqlDataAdapter(s, C.Con)
            C.Da.Fill(Emp_Cse_Dt)
            DataGridView1.DataSource = Emp_Cse_Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
        Try
            Dim Dv As DataView
            Dv = Emp_Cse_Dt.AsDataView
            Dv.RowFilter = IM_Serach(IM_SH_txt.Text, "[CR_NAME]")
            DataGridView1.DataSource = Dv
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class