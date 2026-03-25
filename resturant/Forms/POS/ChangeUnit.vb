Public Class ChangeUnit
    Dim rs As New Resizer
    Dim SB_T_ID As Integer
    Dim IM_ID As Integer
    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        Me.Close()
    End Sub

    Private Sub ChangeUnit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        F_POS.Fetch_IM()
        Me.Dispose()
    End Sub

    Private Sub ChangeUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Sales_Load_IM()
        IM_NameLb.Text = F_POS.MetroGrid.CurrentRow.Cells("IM_NameCL").Value
        SB_T_ID = F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value
        IM_ID = F_POS.MetroGrid.CurrentRow.Cells("IM_ID_CL").Value
    End Sub

    Public Sub Sales_Load_IM()

        IMPanel.Controls.Clear()
        Dim IMName As String
        Dim FontSize
        Dim StandarMeasure As Double
        Dim counter = 1
        Dim x = 5
        Dim y = 10
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_IM_Select_Units"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_T_ID", F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value)
            .Parameters.AddWithValue("@Sales_Type", F_POS.SALES_TYPES_CMB.SelectedValue)
        End With

        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            While C.Dr.Read()
                counter += 1
                Dim IMbtn As New Button
                IMbtn.Name = ("IMbtn" + C.Dr("U_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(IMPanel.Size.Width / 3.2, IMPanel.Size.Height / 3.1)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMName = C.Dr("U_Name") + vbNewLine + C.Dr("Price").ToString
                FontSize = ((h / 100) * (w / 100)) / (IMName.Count * IMName.Count)
                StandarMeasure = (w + h) / 190
                IMbtn.Font = New System.Drawing.Font("Segoe UI", FontSize + StandarMeasure, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(1, Byte))
                IMbtn.Text = IMName
                IMbtn.BackgroundImageLayout = ImageLayout.Stretch
                IMbtn.Tag = C.Dr("U_ID")
                AddHandler IMbtn.Click, AddressOf IMbtn_Click
                IMbtn.TextAlign = ContentAlignment.MiddleCenter
                IMbtn.ForeColor = Color.Black
                IMbtn.BackColor = Color.LightGreen

                Controls.Add(IMbtn)
                IMbtn.Parent = IMPanel

                If counter = 4 Then
                    counter = 1
                    x = 5
                    y += IMPanel.Size.Height / 3.1
                Else
                    x += IMPanel.Size.Width / 3.2
                End If

            End While
        End If

        C.Con.Close()

    End Sub

    Sub IMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SB_Contents_Confirm_Unit(sender.tag)
    End Sub

    Private Sub UnitsGridViewX1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.ColumnIndex = 0 Then SB_Contents_Confirm_Unit(sender.tag)
    End Sub

    Public Sub SB_Contents_Confirm_Unit(U_ID As Integer)


        If IM_min_QTY = False Then
            If IM_Check_Pch_Def_QTY_(U_ID) = 1 Then
                Beep()
                Dim MSG As New OK
                MSG.Msg_Lb.Text = "لا يمكنك إدراج صنف بكمية سالبة"
                MSG.ShowDialog()
                Exit Sub
            End If
        End If

        If Check_is_Get_Price(U_ID) = 0 Then
            My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
            Dim MSG As New OK
            MSG.Msg_Lb.Text = " يجب إدراج سعر للصنف قبل البيع "
            MSG.ShowDialog()
            Exit Sub
        End If

        Dim Row_Index As Integer = 0
        If F_POS.MetroGrid.Rows.Count > 0 Then Row_Index = F_POS.MetroGrid.CurrentCell.RowIndex
        F_POS.IM_Dt.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_IM_Unit_Change"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", F_POS.T_ID)
            .Parameters.AddWithValue("@IM_T_ID", SB_T_ID)
            .Parameters.AddWithValue("@U_ID", U_ID)
            .Parameters.AddWithValue("@Sales_Type", F_POS.SALES_TYPES_CMB.SelectedValue)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        C.Da.Fill(F_POS.IM_Dt)

        F_POS.MetroGrid.DataSource = C.Dt
        'F_POS.Calc_Bill()
        If Row_Index > 0 Then F_POS.MetroGrid.CurrentCell = F_POS.MetroGrid.Rows(Row_Index).Cells("IM_NameCL")
        Me.Close()
    End Sub

    Private Function Check_is_Get_Price(U_ID As Integer)
        Dim c As New C
        Try
            Dim s As String
            s = "select Price,Min_SP,Min_SP_2,isStore from IM_Menu_Units_V WHERE IM_ID = '" & IM_ID & "' AND U_ID = " & U_ID
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()

                If c.Dr("isStore") = 1 Then
                    If F_POS.SALES_TYPES_CMB.SelectedValue = 1 Then If c.Dr("Min_SP") = 0 Then Return 0
                    If F_POS.SALES_TYPES_CMB.SelectedValue = 2 Then If c.Dr("Min_SP_2") = 0 Then Return 0
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 1
    End Function

    Private Function IM_Check_Pch_Def_QTY_(U_ID As Integer)
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "[IM_Check_Neg_QTY_POS_Unit_Change]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", SB_T_ID)
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@Enterd_U_ID", U_ID)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function



    Private Sub ChangeUnit_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub


End Class