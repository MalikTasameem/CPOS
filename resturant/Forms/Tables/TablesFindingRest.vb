Public Class TablesFindingRest
    Dim aa As String
    Public is_Back As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        aa = 1
        PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
        Count_Rest()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        aa = 2
        PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
        Count_Rest()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        aa = 3
        PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
        Count_Rest()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        aa = 4
        PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
        Count_Rest()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        aa = 5
        PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        aa = 6
        PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
        Count_Rest()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        aa = 7
        PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
        Count_Rest()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        aa = 8
        PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
        Count_Rest()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        aa = 9
        PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
        Count_Rest()
    End Sub

    Private Sub Button0_Click(sender As Object, e As EventArgs) Handles Button0.Click
        aa = 0
        PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
        Count_Rest()
    End Sub

    'Private Sub TablesFindingRest_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    '    Me.Dispose()
    'End Sub

    Private Sub DotButton_Click(sender As Object, e As EventArgs) Handles DotButton.Click
        If PiedTextBox.Text.Contains(".") Then
            Beep()
        Else
            aa = "."
            PiedTextBox.Text = PiedTextBox.Text + aa.ToString()
        End If
        Count_Rest()
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        PiedTextBox.Clear()

    End Sub

    Public Sub Count_Rest()
        '    If PiedTextBox .Text <> "."
        Dim Pied As Double = Convert.ToDouble(PiedTextBox.Text)
        Dim Pure As Double = Convert.ToDouble(PureTextBox.Text)
        RestTextBox.Text = Pied - Pure
    End Sub

    Private Sub PiedTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PiedTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub PiedTextBox_TextChanged(sender As Object, e As EventArgs) Handles PiedTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
        If String.IsNullOrWhiteSpace(sender.text) = False Then Count_Rest()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        is_Back = True
        Me.Close()
    End Sub

    Private Sub TablesFindingRest_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Pied_btn_Click(sender, e)
        End If
    End Sub

    Private Sub TablesFindingRest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        is_Back = False
        PiedTextBox.Clear()
        RestTextBox.Clear()
        If TB_Part_Pied = False Then
            PureTextBox.Text = F_TablesBalance.PureTextBox.Text
        Else
            PureTextBox.Text = TablePiedApart.PureTextBox.Text
        End If

    End Sub

    Private Sub Pied_btn_Click(sender As Object, e As EventArgs) Handles Pied_btn.Click
        If TB_Part_Pied = False Then
            If String.IsNullOrWhiteSpace(PiedTextBox.Text) Then
                F_TablesBalance.Pied_Money = 0
                Me.Close()
            Else
                F_TablesBalance.Pied_Money = PiedTextBox.Text
                Me.Close()
            End If
        Else
            If String.IsNullOrWhiteSpace(PiedTextBox.Text) Then
                TablePiedApart.Pied_Money = 0
                Me.Close()
            Else
                TablePiedApart.Pied_Money = PiedTextBox.Text
                Me.Close()
            End If
        End If


    End Sub
End Class