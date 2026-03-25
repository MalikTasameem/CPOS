Public Class SearchByBarcode
    Dim aa As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        aa = 1
        Search_txt.Text = Search_txt.Text + aa.ToString()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        aa = 2
        Search_txt.Text = Search_txt.Text + aa.ToString()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        aa = 3
        Search_txt.Text = Search_txt.Text + aa.ToString()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        aa = 4
        Search_txt.Text = Search_txt.Text + aa.ToString()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        aa = 5
        Search_txt.Text = Search_txt.Text + aa.ToString()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        aa = 6
        Search_txt.Text = Search_txt.Text + aa.ToString()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        aa = 7
        Search_txt.Text = Search_txt.Text + aa.ToString()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        aa = 8
        Search_txt.Text = Search_txt.Text + aa.ToString()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        aa = 9
        Search_txt.Text = Search_txt.Text + aa.ToString()
    End Sub

    Private Sub Button0_Click(sender As Object, e As EventArgs) Handles Button0.Click
        aa = 0
        Search_txt.Text = Search_txt.Text + aa.ToString()
    End Sub

    Private Sub TablesFindingRest_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub DotButton_Click(sender As Object, e As EventArgs) Handles DotButton.Click
        If Search_txt.Text.Contains(".") Then
            Beep()
        Else
            aa = "."
            Search_txt.Text = Search_txt.Text + aa.ToString()
        End If
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        Search_txt.Clear()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub TablesFindingRest_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then Pied_btn_Click(sender, e)
    End Sub


    Private Sub Pied_btn_Click(sender As Object, e As EventArgs) Handles Pied_btn.Click
        If String.IsNullOrWhiteSpace(Search_txt.Text) Then
            MsgBox("حدد الفاتورة", MsgBoxStyle.Critical)
        Else
            If SB_is_Fast = False Then
                Get_T_ID()
            Else

                If SB_ID_Radio.Checked = True Then
                    Sales_Fast.Get_T_ID(Convert.ToInt32(Search_txt.Text), "")
                ElseIf ByBarcodeRadio.Checked = True Then
                    Sales_Fast.Get_T_ID_By_Barcode(Search_txt.Text)
                End If


                Me.Close()
            End If

        End If

    End Sub

    'Public Sub Get_T_ID_By_Fast()
    '    Try
    '        Dim C As New C
    '        Dim S As String
    '        If ByBarcodeRadio.Checked = True Then
    '            S = "SELECT  T_ID,S_Bill_Pr_ID from SB_Info_V where Pr_ID = '" & Pr_ID & "' AND Barcode = '" & Barcode_txt.Text & "'"
    '        ElseIf By_NumRadio.Checked = True Then
    '            S = "SELECT  T_ID,S_Bill_Pr_ID from SB_Info_V where Pr_ID = '" & Pr_ID & "' AND S_Bill_Pr_ID = '" & Barcode_txt.Text & "'"
    '        Else
    '            S = "SELECT  T_ID,S_Bill_Pr_ID from SB_Info_V where Pr_ID = '" & Pr_ID & "' AND SB_ID = '" & Barcode_txt.Text & "'"
    '        End If

    '        C.Com = New SqlClient.SqlCommand(S, C.Con)
    '        C.Con.Open()
    '        Try
    '            C.Dr = C.Com.ExecuteReader
    '            If C.Dr.HasRows Then
    '                C.Dr.Read()
    '                Sales_Fast.ResetNewBill()
    '                Sales_Fast.isNewBill = 0
    '                Sales_Fast.T_ID = C.Dr("T_ID")
    '                ' Sales_Fast.BillNumTxt.Text = C.Dr("S_Bill_Pr_ID")
    '                Sales_Fast.Fill_Bill_Info()
    '                Sales_Fast.SB_Contents_SELECT_Bill()
    '                Me.Close()
    '            Else
    '                MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
    '            End If

    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '        C.Con.Close()
    '    Catch ex As Exception
    '        '...
    '    End Try
    'End Sub

    Public Sub Get_T_ID()
        Try
            Dim C As New C
            Dim S As String
            If ByBarcodeRadio.Checked = True Then
                S = "SELECT  T_ID,S_Bill_Pr_ID from SB_Info_V where  Barcode = '" & Search_txt.Text & "'"
            ElseIf By_NumRadio.Checked = True Then
                S = "SELECT  T_ID,S_Bill_Pr_ID from SB_Info_V where  S_Bill_Pr_ID = '" & Search_txt.Text & "' AND Pr_ID = '" & Pr_ID & "' ORDER BY T_ID DESC "
            Else
                S = "SELECT  T_ID,S_Bill_Pr_ID from SB_Info_V where  SB_ID = '" & Search_txt.Text & "'"
            End If

            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            Try
                C.Dr = C.Com.ExecuteReader
                If C.Dr.HasRows Then
                    C.Dr.Read()
                    F_POS.Reset_Fields()
                    F_POS.isNewBill = 0
                    F_POS.T_ID = C.Dr("T_ID")
                    F_POS.BillNumTxt.Text = C.Dr("S_Bill_Pr_ID")
                    F_POS.Fill_Bill_Info()
                    F_POS.SB_Contents_SELECT_Bill()
                    Me.Close()
                Else
                    MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            C.Con.Close()
        Catch ex As Exception
            '...
        End Try
    End Sub

    Private Sub Barcode_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Search_txt.KeyDown
        If e.KeyCode = Keys.Return Then Pied_btn_Click(sender, e)
    End Sub

    Private Sub SearchByBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        If Me.Search_txt.Focused = False Then
            Search_txt.Focus()
            If Me.Search_txt.Enabled = True Then
                Search_txt.Text = e.KeyChar.ToString
                Search_txt.SelectionStart = Search_txt.Text.Length
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub By_NumRadio_CheckedChanged(sender As Object, e As EventArgs) Handles By_NumRadio.CheckedChanged
        CB_CHecked(sender)
        If By_NumRadio.Checked = True Then
            My_Settings.POS_Search_Type = 2
            Save_AppSetting
        End If
    End Sub

    Private Sub ByBarcodeRadio_CheckedChanged(sender As Object, e As EventArgs) Handles ByBarcodeRadio.CheckedChanged
        CB_CHecked(sender)
        If ByBarcodeRadio.Checked = True Then
            My_Settings.POS_Search_Type = 1
            Save_AppSetting
        End If
    End Sub

    Private Sub SearchByBarcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My_Settings.POS_Search_Type = 1 Then
            ByBarcodeRadio.Checked = True
        ElseIf My_Settings.POS_Search_Type = 2 Then
            By_NumRadio.Checked = True
        Else
            SB_ID_Radio.Checked = True
        End If

        If SB_is_Fast = True Then By_NumRadio.Visible = False
    End Sub

    Private Sub SB_ID_Radio_CheckedChanged(sender As Object, e As EventArgs) Handles SB_ID_Radio.CheckedChanged
        CB_CHecked(sender)
        If SB_ID_Radio.Checked = True Then
            My_Settings.POS_Search_Type = 3
            Save_AppSetting
        End If
    End Sub
End Class