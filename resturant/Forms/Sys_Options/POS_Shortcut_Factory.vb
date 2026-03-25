Imports System.IO
Imports System.Data.SqlClient
Imports System.IO.Ports

Public Class POS_Shortcut_Factory

    Public rs As New Resizer
    Dim Data As Byte()

    Public GM_ID As Integer
    Public IM_ID As Integer

    Dim S_ID As Integer
    Dim S_Name As String
    Dim IM_Dt As New DataTable

    Dim Btn_TabIndex As Integer



    Public Sub Sales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_StoreData()
        Mange_FormOptions()
        If S_listBox.Items.Count > 0 Then S_listBox_Click(sender, e)
    End Sub


    'Private Sub Sales_Load_Shortcuts()

    '    Dim FontSize, NameSize
    '    Dim StandarMeasure As Double
    '    Dim GM_Name As String
    '    Dim x = 0
    '    Dim y = 0

    '    Dim C As New C
    '    Dim s As String
    '    s = "select Short_ID,Short_Name from Shortcut_Group "
    '    C.Com = New SqlClient.SqlCommand(s, C.Con)

    '    C.Con.Open()

    '    C.Dr = C.Com.ExecuteReader
    '    If C.Dr.HasRows = True Then
    '        While C.Dr.Read()

    '            Dim SMbtn As New Button
    '            SMbtn.Name = ("GMbtn" + C.Dr("Short_ID").ToString)
    '            GM_Name = C.Dr("Short_Name")
    '            NameSize = GM_Name.Count
    '            If NameSize < 3 Then NameSize += 2
    '            SMbtn.AutoSize = False
    '            SMbtn.Cursor = Cursors.Hand
    '            SMbtn.FlatStyle = FlatStyle.Flat
    '            SMbtn.Location = New System.Drawing.Point(x, y)
    '            SMbtn.Size = New System.Drawing.Size(SMPanel.Size.Width - 15, SMPanel.Size.Height / 9)
    '            SMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
    '            FontSize = ((h / 100) * (w / 100)) / (NameSize * NameSize)
    '            StandarMeasure = (w + h) / 190
    '            SMbtn.Font = New System.Drawing.Font("Segoe UI", (FontSize + StandarMeasure) - 4.5, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
    '            SMbtn.Text = GM_Name
    '            SMbtn.TextAlign = ContentAlignment.MiddleCenter
    '            SMbtn.Tag = C.Dr("Short_ID")
    '            Me.Controls.Add(SMbtn)
    '            SMbtn.Parent = Me.SMPanel
    '            AddHandler SMbtn.Click, AddressOf SMbtn_Click
    '            SMbtn.BackColor = Color.White

    '            'If IsDBNull(C.Dr("BK_R")) Then
    '            '    SMbtn.BackColor = System.Drawing.SystemColors.Info
    '            'Else
    '            '    SMbtn.BackColor = Color.FromArgb(C.Dr("BK_R"), C.Dr("BK_G"), C.Dr("BK_B"))
    '            'End If

    '            'If IsDBNull(C.Dr("FK_R")) = False Then
    '            '    SMbtn.ForeColor = Color.FromArgb(C.Dr("FK_R"), C.Dr("FK_G"), C.Dr("FK_B"))
    '            'End If

    '            y += (SMPanel.Size.Height / 9)

    '        End While
    '    End If
    '    C.Con.Close()
    'End Sub

    Sub SMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        IMPanel.Tag = ""
        GM_ID = sender.tag
        Sales_Load_IM(GM_ID)
    End Sub


    Private Sub Sales_Load_IM(GM_ID As String)

        IMPanel.Controls.Clear()
        Dim IMName As String
        Dim FontSize
        Dim StandarMeasure As Double
        Dim counter = 1
        Dim x = 5
        Dim y = 10
        Dim C As New C
        Dim s As String
        For i = 1 To 20
            counter += 1
            C = New C
            s = "select IM_ID,item_name,isValid,photo,BK_R,BK_G,BK_B,FK_R,FK_G,FK_B from ShortcutMenu_V WHERE ShortID = '" & GM_ID & "' AND Loc_ID = '" & i & "'"
            C.Com = New SqlClient.SqlCommand(s, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader

            Dim IMbtn As New Button
            IMbtn.TabIndex = i
            AddHandler IMbtn.Click, AddressOf IMbtn_Click



            If C.Dr.HasRows Then
                C.Dr.Read()

                IMbtn.Name = ("IMbtn" + C.Dr("IM_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(IMPanel.Size.Width / 5.2, IMPanel.Size.Height / 4.25)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMName = C.Dr("item_name")

                FontSize = ((h / w) * 100) / IMName.Count
                StandarMeasure = (w + h) / 190

                Try
                    IMbtn.Font = New System.Drawing.Font("Segoe UI", (FontSize + StandarMeasure) - 2.5, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                Catch ex As Exception
                    IMbtn.Font = New System.Drawing.Font("Segoe UI", 12, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                End Try

                IMbtn.Text = C.Dr("item_name")
                IMbtn.Tag = C.Dr("IM_ID")
                IMbtn.TabStop = C.Dr("isValid")
                Controls.Add(IMbtn)
                IMbtn.Parent = IMPanel

                If IsDBNull(C.Dr("photo")) = False Then
                    IMbtn.BackColor = System.Drawing.SystemColors.Info
                    FontSize = (w + h) / 180
                    IMbtn.Font = New System.Drawing.Font("Segoe UI", FontSize, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                    Data = DirectCast(C.Dr("photo"), Byte())
                    Dim MS As New MemoryStream(Data)

                    Dim ImageH As Integer = (IMbtn.Size.Height / 2)
                    Dim ImageW As Integer = IMbtn.Size.Width
                    IMbtn.Image = ResizeImage(Image.FromStream(MS), ImageH, ImageW)
                    IMbtn.TextAlign = ContentAlignment.BottomCenter
                    IMbtn.ImageAlign = ContentAlignment.BottomCenter
                Else
                    IMbtn.Image = Nothing
                    IMbtn.TextAlign = ContentAlignment.MiddleCenter
                End If

                If IsDBNull(C.Dr("BK_R")) Then
                    IMbtn.BackColor = System.Drawing.SystemColors.Info
                Else
                    IMbtn.BackColor = Color.FromArgb(C.Dr("BK_R"), C.Dr("BK_G"), C.Dr("BK_B"))
                End If

                If IsDBNull(C.Dr("FK_R")) = False Then
                    IMbtn.ForeColor = Color.FromArgb(C.Dr("FK_R"), C.Dr("FK_G"), C.Dr("FK_B"))
                End If

                rs.Find_One(IMbtn)

            Else

                '---------------------------------------------
                IMbtn.Name = ("IMbtn" + i.ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(IMPanel.Size.Width / 5.2, IMPanel.Size.Height / 4.25)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                Controls.Add(IMbtn)
                IMbtn.Parent = IMPanel
                '--------------------------------------------
            End If

            If counter = 6 Then
                counter = 1
                x = 5
                y += IMPanel.Size.Height / 4.25
            Else
                x += IMPanel.Size.Width / 5.2
            End If
            C.Con.Close()
        Next

        C.Con.Close()

    End Sub


    Sub IMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Btn_TabIndex = sender.TabIndex
        If S_ID = 0 Then
            MsgBox("حدد الإختصار", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Delete_IM_CB.Checked = True Then

            IM_ID = sender.Tag
            Beep()
            If MessageBox.Show(" حذف الإختصار ؟ ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Shortcut_Menu_Delete_Loc_ID()
            End If

        Else

            Beep()
            If IM_ID = 0 Then
                MsgBox("حدد الصنف", MsgBoxStyle.Exclamation)
                IM_SH_txt.Select()
                Exit Sub
            End If

            If MessageBox.Show(" إدراج الصنف فالمربع ؟ ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Shortcut_Menu_ADD_IM()
            End If


        End If

        

    End Sub

    Public Sub Shortcut_Menu_ADD_IM()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Shortcut_Menu_ADD_IM"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Short_ID", S_ID)
            .Parameters.AddWithValue("@Loc_ID", Btn_TabIndex)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Sales_Load_IM(S_ID)
            IM_SH_txt.Clear()
        End If

    End Sub

    Public Sub Shortcut_Menu_Delete_Loc_ID()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Shortcut_Menu_Delete_Loc_ID"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Short_ID", S_ID)
            .Parameters.AddWithValue("@Loc_ID", Btn_TabIndex)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Delete_IM_CB.Checked = False
            Sales_Load_IM(S_ID)
        End If

    End Sub

    Public Function Get_GM_ST_ID()
        Dim c As New C
        Try
            Dim s As String
            s = "select ISNULL(POS_ST_ID,1) AS POS_ST_ID from IM_GM_ST_ID_V WHERE IM_ID = '" & IM_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Return c.Dr("POS_ST_ID")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 1
    End Function


    Public Sub Mange_FormOptions()

        '   Sales_Load_GM()
        rs.FindAllControls(Me)
        Me.Size = New Size(w, h)
        Reset_Fields()

    End Sub

    Private Sub Sales_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
        '  TabControl1.Region = New Region(New RectangleF(IMTabPage.Left, IMTabPage.Top, IMTabPage.Width, IMTabPage.Height))
    End Sub


    Public Sub Reset_Fields()
        Sales_Load_IM(S_ID)
    End Sub


    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub NewSButton_Click(sender As Object, e As EventArgs) Handles NewSButton.Click
        SaveSButton.Enabled = True
        EditSButton.Enabled = False
        DeleteSButton.Enabled = False
        SNameTextBox.Clear()
        SNameTextBox.Enabled = True
        SNameTextBox.Select()
        EditSButton.Text = "تعديل"
    End Sub


    Private Sub SaveSButton_Click(sender As Object, e As EventArgs) Handles SaveSButton.Click
        If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then
            Store_Insert()
            SNameTextBox.Clear()
            Load_StoreData()
        End If
    End Sub

    Public Sub Store_Insert()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Shortcut_Group_Insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Short_Name", SNameTextBox.Text)
        End With
        SQL_SP_EXEC(C.Com)

    End Sub
    Private Sub EditSButton_Click(sender As Object, e As EventArgs) Handles EditSButton.Click
        If EditSButton.Text = "تعديل" Then
            SNameTextBox.Enabled = True
            DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            EditSButton.Text = "تأكيد التعديل"
        Else
            If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then
                Store_Update()
                Me.Load_StoreData()
                SNameTextBox.Clear()
                SNameTextBox.Enabled = False
                EditSButton.Text = "تعديل"
            End If
        End If
    End Sub

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click
        Beep()
        If MessageBox.Show(" سيتم حذف الإختصار " + S_Name + " وكل الأصناف التي تم وضعها عليه ", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            S_Delete()
        End If
    End Sub

    Public Sub Store_Update()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Shortcut_Group_Update"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Short_ID", S_ID)
            .Parameters.AddWithValue("@Short_Name", SNameTextBox.Text)
        End With
        SQL_SP_EXEC(C.Com)
    End Sub


    Public Sub S_Delete()
        Dim C = New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Shortcut_Group_Delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Short_ID", S_ID)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            S_ID = 0
            Load_StoreData()
            SNameTextBox.Enabled = False
            SNameTextBox.Clear()
            DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            EditSButton.Enabled = False
            For Each A As Control In IMPanel.Controls
                If TypeOf A Is Button Then
                    A.BackColor = SystemColors.Control
                    A.Text = ""
                    A.BackgroundImage = Nothing
                End If
            Next
        End If
    End Sub

    Private Sub SNameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SNameTextBox.KeyDown
        If e.KeyCode = Keys.Return Then
            SaveSButton_Click(sender, e)
        End If
    End Sub

    Public Sub Load_StoreData()
        Dim c As New C
        Dim s As String = "select Short_ID,Short_Name from Shortcut_Group ORDER BY Short_ID Desc"
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        S_listBox.DataSource = dt
        S_listBox.DisplayMember = "Short_Name"
        S_listBox.ValueMember = "Short_ID"

    End Sub

    Private Sub S_listBox_Click(sender As Object, e As EventArgs) Handles S_listBox.Click
        Select_Store()
        Sales_Load_IM(S_ID)
        SNameTextBox.Enabled = False
        DeleteSButton.Enabled = True
        EditSButton.Enabled = True
        SaveSButton.Enabled = False
    End Sub

    Public Sub Select_Store()
        Dim c1 As New C

        Dim sql As String = "select * from Shortcut_Group where Short_ID ='" & S_listBox.SelectedValue & "'"
        Dim com As New SqlClient.SqlCommand(sql, c1.Con)

        c1.Con.Open()
        Try

            c1.Dr = com.ExecuteReader
            If c1.Dr.HasRows Then
                c1.Dr.Read()
                SNameTextBox.Text = c1.Dr("Short_Name")
                S_Name = c1.Dr("Short_Name")
                S_ID = c1.Dr("Short_ID")

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
    End Sub

    '----------------------------------
    Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
        If IM_SH_txt.Text.Count > 0 Then
            Load_IM()
        Else
            IMDataGridViewX.Visible = False
            IM_ID = 0
        End If
        If IM_ID = 0 Then
            IM_SH_txt.BackColor = Color.LightGray
        Else
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub IM_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_SH_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return
                If IMDataGridViewX.Visible = True Then Fetch_ItemToList()
            Case Keys.Down
                IMDataGridViewX.Select()
            Case Keys.Delete : IM_SH_txt.Clear()
        End Select
    End Sub

    Public Sub Load_IM()
        Dim c As New C

        Try
            IM_Dt.Clear()
            c.Str = IM_Serach(IM_SH_txt.Text)
            c.Da = New SqlClient.SqlDataAdapter(c.Str, c.Con)
            c.Da.Fill(IM_Dt)
            IMDataGridViewX.DataSource = IM_Dt
            If IM_Dt.Rows.Count > 0 Then
                IMDataGridViewX.Visible = True
                IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
            Else
                IMDataGridViewX.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IMDataGridViewX_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles IMDataGridViewX.CellClick
        Fetch_ItemToList()
    End Sub

    Private Sub IMDataGridViewX_KeyDown(sender As Object, e As KeyEventArgs) Handles IMDataGridViewX.KeyDown
        If e.KeyCode = Keys.Return Then
            Fetch_ItemToList()
        End If

        If e.KeyCode = Keys.Up Then
            If IMDataGridViewX.CurrentRow.Index = 0 Then
                IM_SH_txt.Select()
            End If
        End If
    End Sub

    Private Sub Fetch_ItemToList()
        If IMDataGridViewX.Rows.Count > 0 Then
            IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
            IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
            IMDataGridViewX.Visible = False
            IM_SH_txt.Select()
        End If
    End Sub



    Private Sub Show_IM_btn_Click(sender As Object, e As EventArgs) Handles Show_IM_btn.Click
        If IMDataGridViewX.Visible = True Then
            IMDataGridViewX.Visible = False
        Else
            Load_ALL_IM()
        End If
    End Sub

    Public Sub Load_ALL_IM()
        Dim c As New C

        Try
            IM_Dt.Clear()
            Dim s As String
            s = "select IM_ID,item_name,isValid from IM_All_V  Order by item_name ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Dt)
            IMDataGridViewX.DataSource = IM_Dt
            If IM_Dt.Rows.Count > 0 Then
                IMDataGridViewX.Visible = True
                IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
            Else
                IMDataGridViewX.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Delete_IM_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Delete_IM_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub KeyBoard_Btn_Click(sender As Object, e As EventArgs) Handles KeyBoard_Btn.Click
        Call_KeyBoard()
    End Sub
End Class