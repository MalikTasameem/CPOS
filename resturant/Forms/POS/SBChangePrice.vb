Public Class SBChangePrice
    Dim rs As New Resizer
    Dim Min_SP As Double = 0
    Dim Min_SP_2 As Double
    Dim U_ID As Integer

    Private Sub NewPriceTextBox_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Return Then IM_Conferm_NewPrice()
    End Sub

    Private Sub NewPriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
        Check_Only_Float(sender, e)
    End Sub

    Private Sub NewPriceTextBox_TextChanged(sender As Object, e As EventArgs)
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub ConfermButton_Click(sender As Object, e As EventArgs) Handles ConfermButton.Click

        If F_POS.isPriceCH = True Then
            If Not String.IsNullOrWhiteSpace(NumTextBox.Text) Then CALC_ChangePrice()
        Else
        If String.IsNullOrWhiteSpace(NumTextBox.Text) = False Then
            F_POS.DiscountTextBox.Text = NumTextBox.Text
                F_POS.Calc_Bill(True)
                Update_Discount(F_POS.T_ID, NumTextBox.Text)
                F_POS.Make_Discount()
                Network_Edit_Tracker_insert(" تخفيض للفاتورة بقيمة:" & NumTextBox.Text, F_POS.SB_ID_Txt.Text, 1, 3)
            Me.Close()
        Else
            Me.Close()
            End If
        End If


    End Sub

    Private Sub CALC_ChangePrice()
        If S_Allow_MinSP = True Then
            If User_isAdmin = False Then
                If U_Sell_Under_Min_SP = True Then
                    If Convert.ToDouble(NumTextBox.Text) < Min_SP And Min_SP > 0 Then
                        MsgBox(" ( " + Min_SP.ToString + " ) لا يمكنك البيع بأقل من أدنى سعر بيع", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                End If

            Else
                If Convert.ToDouble(NumTextBox.Text) < Min_SP And Min_SP > 0 Then
                    If MessageBox.Show(" ( " + Min_SP.ToString + " ) سوف يتم البيع بأقل من أدنى سعر بيع", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
                                       MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
                End If
            End If
        End If


        If U_SB_Sell_Under_Cost = False Then
            If Show_IM_Cost(False, F_POS.MetroGrid.CurrentRow.Cells("IM_ID_CL").Value, U_ID) > NumTextBox.Text Then
                MsgBox("لا يمكنك البيع بأقل من سعر التكلفة", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Else
            If Show_IM_Cost(False, F_POS.MetroGrid.CurrentRow.Cells("IM_ID_CL").Value, U_ID) > NumTextBox.Text Then
                If MessageBox.Show(" سوف يتم البيع بأقل من سعر التكلفة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
                                              MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            End If
        End If

        IM_Conferm_NewPrice()
    End Sub

    Private Function Show_IM_Cost(Show_Msg As Boolean, IM_ID As Integer, U_ID As Integer)
        Dim c As New C
        Dim Cost As Double = 0
        Dim Min_SP As Double = 0
        Dim Min_SP_Desc As String = ""
        Try
            Dim s As String
            s = "select ISNULL(Cost,0) AS Cost , U_Cargo , Min_SP  from IM_Menu_Units_V WHERE IM_ID = '" & IM_ID & "' AND U_ID = '" & U_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Cost = c.Dr("Cost") * c.Dr("U_Cargo")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Cost
    End Function

    Private Sub IM_Conferm_NewPrice()
        Dim SB_T_ID As Integer = F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value
        Dim Row_Index As Integer = F_POS.MetroGrid.CurrentCell.RowIndex
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Conferm_NewPrice"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_T_ID", F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value)
            .Parameters.AddWithValue("@New_Price", NumTextBox.Text)
            .Parameters.AddWithValue("@On_Update", F_POS.On_Update)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            F_POS.isPriceCH = False
            F_POS.SB_Contents_SELECT_Bill()
            F_POS.MetroGrid.CurrentCell = F_POS.MetroGrid.Rows(Row_Index).Cells(3)
            Me.Close()
        End If
    End Sub

    Private Sub isChangePrice_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        F_POS.IMPanel.Tag = ""
        F_POS.Fetch_IM()
        Me.Dispose()
    End Sub

    Private Sub isChangePrice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)

        If F_POS.isPriceCH = False Then
            InfoLb.Text = "قيمــة التخفيض"
            If Convert.ToDouble(F_POS.DiscountTextBox.Text) > 0 Then NumTextBox.Text = F_POS.DiscountTextBox.Text
            NumTextBox.Focus()
                NumTextBox.Select()
            Else
                Min_SP_Panel.Visible = S_Allow_MinSP
            IM_Fetch_Unit()
            NumTextBox.Focus()
            NumTextBox.Select()


        End If
    End Sub

    Private Sub IM_Fetch_Unit()
        Dim c As New C
        Try
            Dim s As String
            s = "select U_ID From SB_Contents WHERE T_ID = '" & F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                U_ID = c.Dr("U_ID")
                IM_Fetch_Min_SP(U_ID)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IM_Fetch_Min_SP(U_ID_Tmp As Integer)
        Dim c As New C
        Try
            Dim s As String
            s = "select Min_SP,Min_SP_2 From IM_Menu_Units_V WHERE U_ID = '" & U_ID_Tmp & "' AND IM_ID = '" & F_POS.MetroGrid.CurrentRow.Cells("IM_ID_CL").Value & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Min_SP = c.Dr("Min_SP")
                Min_SP_txt.Text = c.Dr("Min_SP")

                Min_SP_2 = c.Dr("Min_SP_2")
                Min_SP_2_txt.Text = c.Dr("Min_SP_2")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub ClearNumBtn_Click(sender As Object, e As EventArgs) Handles ClearNumBtn.Click
        NumTextBox.Clear()
    End Sub

    Private Sub BDot_Click(sender As Object, e As EventArgs)
        If NumTextBox.Text.Contains(".") = False Then
            NumTextBox.Text += "."
        End If
    End Sub

    Private Sub SBChangePrice_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        F_POS.IMPanel.Tag = ""
        F_POS.Fetch_IM()
    End Sub

    Private Sub Show_Cash_btn_Click(sender As Object, e As EventArgs) Handles Show_Cash_btn.Click

        If F_POS.MetroGrid.Rows.Count > 0 Then
            Dim F As New Fast_SB_Discount
            Identifiers.T_ID = T_ID
            Identifiers.TOTAL = F_POS.TotalTextBox.Text
            Identifiers.Disc = Disc
            Identifiers.Pure = F_POS.PureTextBox.Text
            Identifiers.SB_ID = SB_ID
            Fast_SB_Discount.ShowDialog()
        End If

    End Sub

    Private Sub Min1_btn_Click(sender As Object, e As EventArgs) Handles Min1_btn.Click
        NumTextBox.Text = Min_SP_txt.Text
    End Sub

    Private Sub Min2_btn_Click(sender As Object, e As EventArgs) Handles Min2_btn.Click
        NumTextBox.Text = Min_SP_2_txt.Text
    End Sub

    Private Sub PercentBtn_Click(sender As Object, e As EventArgs) Handles PercentBtn.Click
        Dim F_Percent_Disc As New Percent_Disc
        F_Percent_Disc.T_ID = F_POS.T_ID
        F_Percent_Disc.TOTAL = F_POS.TOTAL
        F_Percent_Disc.ShowDialog()
        If F_Percent_Disc.Result > 0 Then
            NumTextBox.Text = F_Percent_Disc.Result
            ConfermButton_Click(sender, e)
        End If
    End Sub
End Class