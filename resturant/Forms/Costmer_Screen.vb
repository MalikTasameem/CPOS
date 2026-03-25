Public Class Costmer_Screen
    Public h = My.Computer.Screen.Bounds.Size.Height
    Public w = My.Computer.Screen.Bounds.Size.Width
    Dim T_ID As Integer
    Dim rs As New Resizer
    Private Sub Costmer_Screen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Me.Size = New Size(w, h)
    End Sub

    Public Sub SB_Select_Wait()

        WaitPanel.Controls.Clear()
        Dim StandarMeasure As Double
        Dim counter = 1
        Dim x = 5
        Dim y = 10
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Select_Wait"
            .CommandType = CommandType.StoredProcedure
        End With

        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            While C.Dr.Read()
                counter += 1
                Dim IMbtn As New Button
                IMbtn.Name = ("IMbtn" + C.Dr("T_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(WaitPanel.Size.Width / 7.1, WaitPanel.Size.Height / 7.1)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                StandarMeasure = (w + h) / 190
                IMbtn.Font = New System.Drawing.Font("Arial", StandarMeasure + 22, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(1, Byte))
                AddHandler IMbtn.Click, AddressOf IMbtn_Click
                IMbtn.Text = C.Dr("Bill_Num")
                IMbtn.BackgroundImageLayout = ImageLayout.Stretch
                IMbtn.Tag = C.Dr("T_ID")
                Controls.Add(IMbtn)
                IMbtn.Parent = WaitPanel
                IMbtn.BackColor = Color.DarkRed
                IMbtn.ForeColor = Color.White

                If counter = 8 Then
                    counter = 1
                    x = 5
                    y += WaitPanel.Size.Height / 7.1
                Else
                    x += WaitPanel.Size.Width / 7.1
                End If


            End While
        End If

        C.Con.Close()

    End Sub

    Public Sub SB_Select_Ready()

        ReadyPanel.Controls.Clear()
        Dim StandarMeasure As Double
        Dim counter = 1
        Dim x = 5
        Dim y = 10
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Select_Ready"
            .CommandType = CommandType.StoredProcedure
        End With

        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            While C.Dr.Read()
                counter += 1
                Dim IMbtn As New Button
                IMbtn.Name = ("IMbtn" + C.Dr("T_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(ReadyPanel.Size.Width / 7.1, ReadyPanel.Size.Height / 7.1)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                StandarMeasure = (w + h) / 190
                IMbtn.Font = New System.Drawing.Font("Arial", StandarMeasure + 22, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(1, Byte))
                AddHandler IMbtn.Click, AddressOf IMbtn_Click
                IMbtn.Text = C.Dr("Bill_Num")
                IMbtn.BackgroundImageLayout = ImageLayout.Stretch
                IMbtn.Tag = C.Dr("T_ID")
                Controls.Add(IMbtn)
                IMbtn.Parent = ReadyPanel
                IMbtn.BackColor = Color.DarkGreen
                IMbtn.ForeColor = Color.White

                If counter = 8 Then
                    counter = 1
                    x = 5
                    y += ReadyPanel.Size.Height / 7.1
                Else
                    x += ReadyPanel.Size.Width / 7.1
                End If


            End While
        End If

        C.Con.Close()

    End Sub

    Sub IMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        T_ID = sender.tag.ToString
        Bill_Num_txt.Text = sender.Text
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        SB_Select_Wait()
        SB_Select_Ready()
    End Sub

    Private Sub Costmer_Screen_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Get_Bill_Ready_btn_Click(sender As Object, e As EventArgs) Handles Get_Bill_Ready_btn.Click
        Change_SB_Case(1)
    End Sub

    Private Sub Change_SB_Case(S As Integer)
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Change_SB_Case"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
            .Parameters.AddWithValue("@CASE", S)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Bill_Num_txt.Clear()
            T_ID = 0
            SB_Select_Wait()
            SB_Select_Ready()
        End If
    End Sub

    Private Sub Bill_Num_txt_TextChanged(sender As Object, e As EventArgs) Handles Bill_Num_txt.TextChanged
        If Bill_Num_txt.Text.Count = 0 Then
            T_ID = 0
        End If
    End Sub

    Private Sub Cancel_Bill_btn_Click(sender As Object, e As EventArgs) Handles Cancel_Bill_btn.Click
        Change_SB_Case(0)
    End Sub

    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        Me.Close()
        login.Show()
    End Sub

    Private Sub OverTimeTimer_Tick(sender As Object, e As EventArgs) Handles OverTimeTimer.Tick
        Change_SB_Case()
    End Sub

    Private Sub Change_SB_Case()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_DeleteOverTime"
            .CommandType = CommandType.StoredProcedure
            '  .Parameters.AddWithValue("@T_ID", T_ID)
            '  .Parameters.AddWithValue("@CASE", S)
        End With
        SQL_SP_EXEC(c.Com)
    End Sub

End Class