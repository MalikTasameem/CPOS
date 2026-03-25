Public Class Periods
    Dim rs As New Resizer

    Public Pr_Status As Boolean
    Dim Unpied_Num As Integer

    Private Sub Periods_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        F_MainForm.Check_Sys_Featurs()
        F_MainForm.Fill_ALL_ALERT()
        F_MainForm.Set_Data_Alert()
    End Sub
    Private Sub Periods_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        Load_Open_Periods()
        TimeTimer.Enabled = True
    End Sub

    Public Sub Load_Open_Periods()
        Dim C As New C

        With (C.Com)
            .Connection = C.Con
            .CommandText = "Pr_SelectUserList"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@USER_ID", USER_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Pr_Grid.DataSource = C.Dt
        'Pr_ListBox.DisplayMember = "Notes"
        'Pr_ListBox.ValueMember = "Pr_ID"
        '----------------------------------------------------------

        If Pr_Grid.Rows.Count > 0 Then get_Pierod_Details()


    End Sub

    Private Sub get_Pierod_Details()
        Dim C = New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Check_Cash_UnPied"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Pr_ID", Pr_Grid.CurrentRow.Cells("Pr_ID_CL").Value)
            .Parameters.AddWithValue("@Count", 0)
            .Parameters("@Count").Direction = ParameterDirection.Output
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Unpied_Num = C.Com.Parameters("@Count").Value

            If Unpied_Num > 0 Then
                Pr_NotesLabel.Visible = True
                Pr_NotesLabel.Text = " يوجد عدد " + Unpied_Num.ToString + " فاتورة نقدية لم يتم إغلاقها بعد "
                End_PrButton.Enabled = False
                Show_OpenBills_btn.Visible = True
                '  If User_isAdmin = False Then Start_PrButton.Enabled = False
                'If isPr_Open = True Then Start_PrButton.Enabled = False
            Else
                Pr_NotesLabel.Visible = False
                '  If User_isAdmin = False Then Start_PrButton.Enabled = False
                'Start_PrButton.Enabled = False
                End_PrButton.Enabled = True
                Show_OpenBills_btn.Visible = False
            End If
            If isPr_Open = True Then Start_PrButton.Enabled = False
        End If
    End Sub

    Public Sub Check_For_OpenPierod()
        Dim C As New C
        C.Con.Open()
        With C.Com
            .Connection = C.Con
            .CommandText = "Check_For_OpenPierod"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@USER_ID", USER_ID)
        End With

        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()
            isPr_Open = True
            Start_PrButton.Enabled = False
            Pr_ID = C.Dr("Pr_ID")
            U_Flate_ID = C.Dr("TB_Flate_ID")
            End_PrButton.Enabled = True
        Else
            isPr_Open = False
            Start_PrButton.Enabled = True
            End_PrButton.Enabled = False
        End If
        C.Con.Close()
    End Sub

    'Private Sub Periods_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    '    rs.ResizeAllControls(Me)
    'End Sub

    Private Sub Start_PrButton_Click(sender As Object, e As EventArgs) Handles Start_PrButton.Click
        Me.Pr_Status = True
        F_PeriodsNotes = New PeriodsNotes
        F_PeriodsNotes.ShowDialog()
        '  Set_Form(F_PeriodsNotes, Home.Home_Panel)
    End Sub

    Private Sub End_PrButton_Click(sender As Object, e As EventArgs) Handles End_PrButton.Click
        Me.Pr_Status = False
        F_PeriodsNotes = New PeriodsNotes
        ' Set_Form(F_PeriodsNotes, Home.Home_Panel)
        F_PeriodsNotes.ShowDialog()
    End Sub

    'Private Sub Pr_ListBox_Click(sender As Object, e As EventArgs)
    '    If Pr_Grid.Rows.Count > 0 Then get_Pierod_Details()
    'End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Show_OpenBills_btn_Click(sender As Object, e As EventArgs) Handles Show_OpenBills_btn.Click
        Select_Open_Bills()
    End Sub

    Public Sub Select_Open_Bills()
        Dim C As New C
        Dim Str As String = ""
        Dim S As String = "select SB_ID,S_Bill_Pr_ID from SB_Cash_UnPied_V WHERE Pr_ID = '" & Pr_ID & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                While C.Dr.Read()
                    Str = Str + " * " + " ( " + " رقم ألي : " + C.Dr("SB_ID").ToString + " , " + " رقم يومي : " + C.Dr("S_Bill_Pr_ID").ToString + " ) " + vbNewLine
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
        MessageBox.Show(Str, "عرض الفواتير المفتوحة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub

    Private Sub Pr_Grid_MouseClick(sender As Object, e As MouseEventArgs) Handles Pr_Grid.MouseClick
        If Pr_Grid.Rows.Count > 0 Then get_Pierod_Details()
    End Sub
End Class