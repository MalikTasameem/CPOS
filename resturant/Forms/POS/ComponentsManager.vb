Public Class ComponentsManager
    Dim rs As New Resizer
    Private Sub ComponentsManager_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Dim Row_Index As Integer = F_POS.MetroGrid.CurrentCell.RowIndex
        F_POS.SB_Contents_SELECT_Bill()
        '  F_POS.Fetch_IM()
        F_POS.MetroGrid.CurrentCell = F_POS.MetroGrid.Rows(Row_Index).Cells(3)
        Me.Dispose()
    End Sub

    Private Sub Comp_Cancel_ALL()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_IM_Compon_Clear"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_T_ID", F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value)
        End With
        SQL_SP_EXEC(C.Com)
    End Sub


    Private Sub Comp_INSERT(Comp_ID As Integer, Price As Double)
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_IM_Compon_INSERT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_T_ID", F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value)
            .Parameters.AddWithValue("@Com_ID", Comp_ID)
            ' .Parameters.AddWithValue("@Price", Price)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            GM_Select_Compon_Details()
        End If
    End Sub

    'Private Sub Comp_Change(Del As Integer)
    '    Dim C As New C
    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "SB_IM_Compon_Change"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@IM_T_ID", F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value)
    '        .Parameters.AddWithValue("@Com_ID", DataGridViewX1.CurrentRow.Cells("Comp_ID_CL").Value)
    '        .Parameters.AddWithValue("@Del", Del)
    '        .Parameters.AddWithValue("@Price", DataGridViewX1.CurrentRow.Cells("Price_CL").Value)
    '        .Parameters.AddWithValue("@Qty", 0)
    '        .Parameters.AddWithValue("@isADD", DataGridViewX1.CurrentRow.Cells("IS_ADD_CL").Value)
    '    End With
    '    SQL_SP_EXEC(C.Com)
    'End Sub

    Private Sub ComponentsManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        Me.Text = " ملاحظات للصنف : " + F_POS.MetroGrid.CurrentRow.Cells("IM_NameCL").Value
        ' Load_Compon()

        GM_Select_Compon_Details()
        Load_IM_NOTES()
        Load_IM_ADDS()
    End Sub

    Public Sub Load_IM_NOTES()

        TabPage1.Controls.Clear()
        Dim IMName As String
        Dim FontSize
        Dim StandarMeasure As Double
        Dim counter = 1
        Dim x = 5
        Dim y = 10
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "GM_Select_Compon"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", F_POS.MetroGrid.CurrentRow.Cells("IM_ID_CL").Value)
            .Parameters.AddWithValue("@is_ADD", 0)
        End With

        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            While C.Dr.Read()
                counter += 1
                Dim IMbtn As New Button
                IMbtn.Name = ("IMbtn" + C.Dr("Comp_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(TabPage1.Size.Width / 7.2, TabPage1.Size.Height / 4.1)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMName = C.Dr("Comp_Name") + vbNewLine + C.Dr("Price").ToString
                FontSize = ((h / 100) * (w / 100)) / (IMName.Count * IMName.Count)
                StandarMeasure = (w + h) / 190
                IMbtn.Font = New System.Drawing.Font("Segoe UI", FontSize + StandarMeasure, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(1, Byte))
                IMbtn.Text = IMName
                IMbtn.BackgroundImageLayout = ImageLayout.Stretch
                IMbtn.Tag = C.Dr("Comp_ID")
                AddHandler IMbtn.Click, AddressOf IMbtn_Click
                IMbtn.TextAlign = ContentAlignment.MiddleCenter
                IMbtn.ForeColor = SystemColors.Control
                IMbtn.BackColor = Color.IndianRed

                Controls.Add(IMbtn)
                IMbtn.Parent = TabPage1

                If counter = 8 Then
                    counter = 1
                    x = 5
                    y += TabPage1.Size.Height / 4.1
                Else
                    x += TabPage1.Size.Width / 7.2
                End If

            End While
        End If

        C.Con.Close()

    End Sub

    Public Sub Load_IM_ADDS()

        TabPage2.Controls.Clear()
        Dim IMName As String
        Dim FontSize
        Dim StandarMeasure As Double
        Dim counter = 1
        Dim x = 5
        Dim y = 10
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "GM_Select_Compon"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", F_POS.MetroGrid.CurrentRow.Cells("IM_ID_CL").Value)
            .Parameters.AddWithValue("@is_ADD", 1)

        End With

        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            While C.Dr.Read()
                counter += 1
                Dim IMbtn As New Button
                IMbtn.Name = ("IMbtn" + C.Dr("Comp_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(TabPage2.Size.Width / 7.2, TabPage2.Size.Height / 4.1)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMName = C.Dr("Comp_Name") + vbNewLine + C.Dr("Price").ToString
                FontSize = ((h / 100) * (w / 100)) / (IMName.Count * IMName.Count)
                StandarMeasure = (w + h) / 190
                IMbtn.Font = New System.Drawing.Font("Segoe UI", FontSize + StandarMeasure, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(1, Byte))
                IMbtn.Text = IMName
                IMbtn.BackgroundImageLayout = ImageLayout.Stretch
                IMbtn.Tag = C.Dr("Comp_ID")
                AddHandler IMbtn.Click, AddressOf IMbtn_Click
                IMbtn.TextAlign = ContentAlignment.MiddleCenter
                IMbtn.ForeColor = Color.Black
                IMbtn.BackColor = Color.LightGreen

                Controls.Add(IMbtn)
                IMbtn.Parent = TabPage2

                If counter = 8 Then
                    counter = 1
                    x = 5
                    y += TabPage2.Size.Height / 4.1
                Else
                    x += TabPage2.Size.Width / 7.2
                End If

            End While
        End If

        C.Con.Close()

    End Sub

    Sub IMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Comp_INSERT(sender.tag, 0)
    End Sub

    'Private Sub Load_Compon()
    '    Dim C As New C
    '    With (C.Com)
    '        .Connection = C.Con
    '        .CommandText = "GM_Select_Compon"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@is_ADD", 0)
    '    End With
    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(C.Dt)
    '    DataGridViewX1.DataSource = C.Dt

    '    C = New C
    '    With (C.Com)
    '        .Connection = C.Con
    '        .CommandText = "GM_Select_Compon"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@is_ADD", 1)
    '    End With
    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(C.Dt)
    '    DataGridViewX2.DataSource = C.Dt


    'End Sub


    Private Sub GM_Select_Compon_Details()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "[GM_Select_Compon_Details]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_T_ID", F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        IM_Com_Grid.DataSource = C.Dt
    End Sub

    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        Me.Close()
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelNotesButton.Click
        Comp_Cancel_ALL()
        Me.Close()
    End Sub


    Private Sub ComponentsManager_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub IM_Com_Grid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles IM_Com_Grid.CellContentClick

        Select Case e.ColumnIndex
            Case 2
                SB_IM_Compon_DELETE()
            Case 0
                Change_NOTES_Qty(1)
            Case 1
                If IM_Com_Grid.CurrentRow.Cells("TB_IM_QTY_CL").Value > 1 Then Change_NOTES_Qty(-1)

        End Select
    End Sub

    Private Sub Change_NOTES_Qty(def As Integer)

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_IM_Compon_Update_QTY"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", IM_Com_Grid.CurrentRow.Cells("T_ID_3_CL").Value)
            .Parameters.AddWithValue("@Def", def)

        End With

        If SQL_SP_EXEC(c.Com) = True Then GM_Select_Compon_Details()

    End Sub


    Private Sub SB_IM_Compon_DELETE()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[SB_IM_Compon_DELETE]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", IM_Com_Grid.CurrentRow.Cells("T_ID_3_CL").Value)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            GM_Select_Compon_Details()
        End If
    End Sub

    Private Sub SMButtonDown_Click(sender As Object, e As EventArgs) Handles SMButtonDown.Click
     

        If TabControl1.SelectedIndex = 0 Then

            Dim newRight As Integer = -TabPage1.AutoScrollPosition.X
            Dim newTop As Integer = -TabPage1.AutoScrollPosition.Y
            newTop = -TabPage1.AutoScrollPosition.Y + 250

            TabPage1.AutoScrollPosition = New Point(newRight, newTop)
        Else

            Dim newRight As Integer = -TabPage2.AutoScrollPosition.X
            Dim newTop As Integer = -TabPage2.AutoScrollPosition.Y
            newTop = -TabPage2.AutoScrollPosition.Y + 250

            TabPage2.AutoScrollPosition = New Point(newRight, newTop)
        End If

    End Sub

    Private Sub SMButtonUP_Click(sender As Object, e As EventArgs) Handles SMButtonUP.Click


        If TabControl1.SelectedIndex = 0 Then

            Dim newRight As Integer = -TabPage1.AutoScrollPosition.X
            Dim newDown As Integer = -TabPage1.AutoScrollPosition.Y
            newDown = -TabPage1.AutoScrollPosition.Y - 250


            TabPage1.AutoScrollPosition = New Point(newRight, newDown)
        Else

            Dim newRight As Integer = -TabPage2.AutoScrollPosition.X
            Dim newDown As Integer = -TabPage2.AutoScrollPosition.Y
            newDown = -TabPage2.AutoScrollPosition.Y - 250

            TabPage2.AutoScrollPosition = New Point(newRight, newDown)
        End If

    End Sub


End Class
