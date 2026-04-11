Public Class SB_TablesMenu
    Dim rs As New Resizer
    Dim TB_DT As New DataTable
    Dim dv As DataView

    Dim h_f = My.Computer.Screen.Bounds.Size.Height
    Dim w_f = My.Computer.Screen.Bounds.Size.Width

    Dim TB_Num As Integer

    Private Sub SB_TablesMenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    Private Sub SB_TablesMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        rs.FindAllControls(Me)
        loadtables()
    End Sub
    ' ==========================================
    ' 🌟 أكواد الشريط العلوي والتحكم بالنافذة 🌟
    ' ==========================================

    ' --- زر الخروج من الشريط العلوي ---
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    ' --- التكبير واستعادة الحجم ---
    Private Sub MaxFormButton_Click(sender As Object, e As EventArgs) Handles MaxFormButton.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.MaximumSize = Screen.FromHandle(Me.Handle).WorkingArea.Size
            Me.WindowState = FormWindowState.Maximized
            MaxFormButton.Text = "❐"
        Else
            Me.WindowState = FormWindowState.Normal
            MaxFormButton.Text = "⬜"
        End If
    End Sub

    ' --- التصغير لشريط المهام ---
    Private Sub MinFormButton_Click(sender As Object, e As EventArgs) Handles MinFormButton.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    ' --- حركة الفورم والسحب بالماوس ---
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, TopTitle_LB.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, TopTitle_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, TopTitle_LB.MouseUp
        drag = False
    End Sub


    Public Sub loadtables()
        TBPanel.Controls.Clear()

        Dim c As New C
        Dim x = 2.5
        Dim y = 2.5

        Dim b As Integer = 0
        Dim s As String = ""
        If U_Flate_ID = 0 Then
            s = "select * from Tables_Balances_V order by TB_ID ASC"
        Else
            s = "select * from Tables_Balances_V WHERE Flate_ID = '" & U_Flate_ID & "' order by TB_ID ASC"
        End If

        c.Com = New SqlClient.SqlCommand(s, c.Con)

        c.Con.Open()
        Try

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                Dim conuter = 1
                If c.Dr.HasRows Then
                    While c.Dr.Read()
                        conuter += 1
                        Dim bt As New Button
                        bt.Tag = c.Dr("TB_ID")
                        bt.Name = "bt" + c.Dr("TB_ID").ToString
                        bt.Cursor = Cursors.Hand
                        bt.Location = New System.Drawing.Point(x, y)
                        bt.Size = New System.Drawing.Size(TBPanel.Size.Width / 6.3, TBPanel.Size.Height / 6.1)

                        bt.RightToLeft = Windows.Forms.RightToLeft.Yes
                        If c.Dr("isbusy") = True Then
                            bt.BackColor = Color.IndianRed
                        Else
                            bt.BackColor = Color.WhiteSmoke
                        End If
                        bt.ForeColor = Color.Black
                        bt.Text = c.Dr("T_Name")
                        '(h_f + h_f) / 70
                        bt.Font = New System.Drawing.Font("Segoe UI", 14, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                        bt.FlatStyle = FlatStyle.Popup
                        bt.TextAlign = ContentAlignment.MiddleCenter
                        Me.Controls.Add(bt)
                        bt.Parent = TBPanel
                        AddHandler bt.Click, AddressOf bt_Click

                        If conuter = 7 Then
                            conuter = 1
                            y += (TBPanel.Size.Height / 6.1)
                            x = 2.5
                        Else
                            x += (TBPanel.Size.Width / 6.3)
                        End If

                    End While

                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        c.Con.Close()

    End Sub

    Public Sub bt_Click(ByVal sender As Object, ByVal e As EventArgs)
        TB_Num = sender.tag
        AG_Balance_Update_AG(TB_Num)
    End Sub

    Private Sub SB_TablesMenu_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub AG_Balance_Update_AG(TB_ID As Integer)

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Update_Table"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", F_POS.T_ID)
            .Parameters.AddWithValue("@TB_ID", TB_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            Check_Cash()
            F_POS.TB_ID = TB_ID
            F_POS.Fill_Bill_Info()
            F_POS.Check_Table()
            Me.Close()
        End If
    End Sub


    Public Sub Check_Cash()

        Dim C As New C
        Dim s As String = ""
        s = "select is_Cash from Tables WHERE TB_ID = " & TB_Num
        c.Com = New SqlClient.SqlCommand(s, c.Con)
        c.Con.Open()
        Try

            c.Dr = c.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                F_POS.TB_is_Cash = C.Dr("is_Cash")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

    End Sub

    Private Sub NONE_TB_btn_Click(sender As Object, e As EventArgs) Handles NONE_TB_btn.Click
        AG_Balance_NONE_Table()
    End Sub

    Private Sub AG_Balance_NONE_Table()

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_NONE_Table"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", F_POS.T_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            F_POS.TB_ID = 0
            F_POS.Fill_Bill_Info()
            F_POS.Check_Table()
            Me.Close()
        End If
    End Sub


    Private Sub Back_Btn_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        Dim newLeft As Integer = -TBPanel.AutoScrollPosition.X
        Dim newTop As Integer = -TBPanel.AutoScrollPosition.Y
        newTop = -TBPanel.AutoScrollPosition.Y - 450
        TBPanel.AutoScrollPosition = New Point(newLeft, newTop)
    End Sub

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Dim newLeft As Integer = -TBPanel.AutoScrollPosition.X
        Dim newTop As Integer = -TBPanel.AutoScrollPosition.Y
        newTop = -TBPanel.AutoScrollPosition.Y + 450
        TBPanel.AutoScrollPosition = New Point(newLeft, newTop)
    End Sub
End Class