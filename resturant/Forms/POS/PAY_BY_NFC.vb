Imports System.IO

Public Class PAY_BY_NFC

    Public Bill_num, Total As String

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Clear_Fields()

        Dim proc As New System.Diagnostics.Process()
        proc = Process.Start(Application.StartupPath & "\NFC\EXE\acr122-demo.exe", "")

        CHECK_NFC_Card_Info()

    End Sub

    Private Sub Clear_Fields()
        NFC_NUM_txt.Clear()
        NFC_Balance_txt.Clear()
        NFC_Status_txt.Clear()

        File.WriteAllText(Application.StartupPath + "\NFC\EXE\CardID.txt", "")
        'For Each a As Control In Panel1.Controls
        '    If TypeOf a Is TextBox Then a.Text = ""
        'Next
    End Sub


    Dim CardUID As String
    Public Sub CHECK_NFC_Card_Info()



        If String.IsNullOrWhiteSpace(CardUID) Then
            Exit Sub
        Else
            NFC_NUM_txt.Text = CardUID
        End If


        'If NFC_NUM_txt.Text.Count > 0 Then
        '    NFC_Balance_txt.Text = "2500"
        '    NFC_Status_txt.Text = "1"

        'End If


        Dim C As New Online_C
        With C.Com
            .Connection = C.Con
            .CommandText = "CheckSCardBalancePROC"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@CardUID", CardUID)
            .Parameters.Add("@Result_Check", SqlDbType.Int, "0")
            .Parameters.Add("@SCard_Balance", SqlDbType.Float, (18.3), "0")
            .Parameters("@Result_Check").Direction = ParameterDirection.Output
            .Parameters("@SCard_Balance").Direction = ParameterDirection.Output

        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Try
                With C.Com
                    NFC_NUM_txt.Text = CardUID
                    NFC_Balance_txt.Text = .Parameters("@SCard_Balance").Value
                    NFC_Status_txt.Text = .Parameters("@Result_Check").Value
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If


    End Sub

    ' Dim is_Get As Boolean = False
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


        'If NFC_Panel.BackColor = Color.White Then
        '    NFC_Panel.BackColor = Color.Black
        'Else
        '    NFC_Panel.BackColor = Color.White
        'End If

        CardUID = File.ReadAllText(Application.StartupPath + "\NFC\EXE\CardID.txt")
        CardUID = CardUID.Replace("00000", "")
        NFC_NUM_txt.Text = CardUID

        If Not String.IsNullOrWhiteSpace(NFC_NUM_txt.Text) Then
            CHECK_NFC_Card_Info()
        Else
            Clear_Fields()
        End If


    End Sub

    Private Sub NFC_NUM_txt_TextChanged(sender As Object, e As EventArgs) Handles NFC_NUM_txt.TextChanged
        'If NFC_NUM_txt.Text.Count > 0 Then Timer1.Enabled = False
    End Sub

    Private Sub NFC_Status_txt_TextChanged(sender As Object, e As EventArgs) Handles NFC_Status_txt.TextChanged

        If NFC_Status_txt.Text.Count > 0 Then
            NFC_Label.Visible = True
        Else
            NFC_Label.Visible = False
        End If

        If NFC_Status_txt.Text = "1" Then
            If Convert.ToDouble(NFC_Balance_txt.Text) >= Convert.ToDouble(Pure_txt.Text) Then
                Pay_Btn.Enabled = True
                NFC_Label.Text = "السوار  مفعـــل"
                NFC_Label.BackColor = Color.PaleGreen
            Else
                NFC_Label.Text = "!!! الرصيـــد لا يكفــي"
                NFC_Label.BackColor = Color.IndianRed
                Pay_Btn.Enabled = False
            End If

        Else
            NFC_Label.Text = "!!! السوار غير مفعــل"
            NFC_Label.BackColor = Color.IndianRed
            Pay_Btn.Enabled = False
        End If

    End Sub

    Private Sub Pay_Btn_Click(sender As Object, e As EventArgs) Handles Pay_Btn.Click
        PAY_BY_NFC()
        'F_POS.PIED_OK = True
        'MsgBox(" تم السداد ... رصيد السوار بعد السداد : " & NFC_Balance_txt.Text, MsgBoxStyle.Information, "")
        'Me.Close()
    End Sub

    Private Sub PAY_BY_NFC()
        Dim C As New Online_C
        With C.Com
            .Connection = C.Con
            .CommandText = "SaveSpendCofeePROC"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SCard_UID", CardUID)
            .Parameters.AddWithValue("@SCard_UID_Det", NFC_NUM_txt.Text)
            .Parameters.AddWithValue("@Invo", Bill_Num_txt.Text)
            .Parameters.AddWithValue("@Total_Invo", Pure_txt.Text)
            .Parameters.AddWithValue("@Resturant_name", Title_txt.Text)
            .Parameters.Add("@SCard_Balance", SqlDbType.Float, (18.3), "0")
            .Parameters("@SCard_Balance").Direction = ParameterDirection.Output
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            F_POS.PIED_OK = True
            Try
                With C.Com
                    NFC_Balance_txt.Text = .Parameters("@SCard_Balance").Value
                    MsgBox(" تم السداد ... رصيد السوار بعد السداد : " & NFC_Balance_txt.Text)
                    Me.Close()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Private Sub PAY_BY_NFC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Title_txt.Text = SBill_Title_1
        Bill_Num_txt.Text = Bill_num
        Pure_txt.Text = Total

        File.WriteAllText(Application.StartupPath + "\NFC\EXE\CardID.txt", "")
        Timer1.Enabled = True
    End Sub
End Class