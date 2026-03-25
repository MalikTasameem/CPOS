Imports System.IO
Imports System.Data.SqlClient
Imports System.IO.Ports
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Drawing.Printing


Public Class POS

    Public rs As New Resizer
    Dim Data As Byte()

    Public isDierct_Reseve As Boolean

    Public FormActive As Boolean = True
    Public TB_Item_ID As Integer

    Public AG_ID As Integer
    Public AG_TypeID As Integer
    Public AG_Name As String
    Public T_ID As Integer
    Public BillNum As Integer
    Public Barcode As String
    Public TB_ID As Integer
    Public isNewBill As Integer

    Public Point As Double = 0

    Dim isPied As Integer = 0
    Dim isDepended As Integer = 0
    Dim isPre_Print As Boolean = False
    Public isPriceCH As Boolean = False
    Public GM_ID As Integer
    Public IM_ID As Integer
    Dim IM_Name As String

    Public IM_SH_Dt As New DataTable

    Public D_Valid As String = ""
    Public is_Valid As Boolean = False

    Public On_Update As Boolean
    Dim SB_ID As Integer
    Public U_IM_ID As Integer

    Dim Pied As Double
    Public TB_isOrderd As Integer = 0
    Public Prev_Type As Integer
    Public is_Form_Start As Boolean = False
    Public Barcode_IM As String = ""
    Public TB_is_Cash As Integer
    Dim IM_Price As Double = 0

    Public is_Show_Bill As Boolean = False


    Dim Bill_Date_Str As String
    Public IM_Dt As New DataTable

    Public GM_Dt As New DataTable
    Public IM_POS_Dt As New DataTable

    Public Cr_Phone As String
    Public Bill_Note As String

    Public TOTAL As Double = 0
    Public DISCOUNT As Double = 0
    Public PURE As Double = 0
    Public PIED_OK As Boolean = False
    'Public Pay_ID As Integer = 1

    Private Sub SB_Delete_Last_Bill()
        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "SB_Delete_Last_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        SQL_SP_EXEC(C.Com)
    End Sub


    Private Sub POS_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If On_Update = True Then Edit_butt_Click(sender, e)
        FormType = 0
        Home_Panel = ""
        isOrder = False
        POS_ENTER_AS_ORDER = False
        'F_MainForm.Fill_ALL_IM()
        F_MainForm.Check_Sys_Featurs()
        MY_Settings.SALES_TYPES_CMB = SALES_TYPES_CMB.SelectedValue
        MY_Settings.Save_AppSetting()
    End Sub

    Private Sub Sales_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.F12
                If PrintBillButton.Enabled = True Then If PrintBillButton.Visible = True Then PrintBillButton_Click(sender, e)

            Case Keys.F1
                If SaveButton.Enabled = True Then If SaveButton.Visible = True Then SaveButton_Click(sender, e)

            Case Keys.Return
                If Barcode_txt.Enabled = True Then
                    Barcode_SH_txt_KeyDown(sender, e)
                Else
                    e.Handled = True
                End If

            Case Keys.F4
                If Barcode_txt.Enabled = True Then
                    IM_Keyboard.ShowDialog()
                Else
                    e.Handled = True
                End If

            Case Keys.F9
                If سدادرقمطاولةToolStripMenuItem.Enabled = True Then
                    سدادرقمطاولةToolStripMenuItem.PerformClick()
                Else
                    e.Handled = True
                End If


            Case 107 'Add
                If On_Update = False Then Add()
            Case 109 'Subtrac
                If On_Update = False Then Subtrac()
        End Select

    End Sub

    Private Sub Add()
        If MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
            If MetroGrid.Rows.Count > 0 Then
                Dim Def As Integer = 1

                If IM_min_QTY = False Then
                    If IM_Check_Neg_QTY_2() = 1 Then
                        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    Else
                        Change_IM_Qty(Def)
                    End If
                Else
                    Change_IM_Qty(Def)
                End If
            End If
        End If
        Barcode_txt.Clear()
    End Sub

    Private Sub Subtrac()
        If MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
            If MetroGrid.Rows.Count > 0 Then
                If MetroGrid.CurrentRow.Cells("QTY_CL").Value > 1 Then
                    Dim Def As Integer = -1
                    Change_IM_Qty(Def)
                End If
            End If
        End If
        Barcode_txt.Clear()
    End Sub

    Public Sub Sales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        FormType = 1
        Load_ST()

        Load_ALL_IM

        Mange_FormOptions()
        If POS_ENTER_AS_ORDER = True Then BillTypeCmb.SelectedValue = 3

        If isShowing_Trans = True Then
            T_ID = T_ID_Trans
            SB_Contents_SELECT_Bill()
            Fill_Bill_Info()
            SBPauseBtn.Enabled = False
        Else
            If Open_NewBill_When_OpenSale = True Then ResetNewBill()
        End If
        is_Form_Start = True
        USER_NAME_LB.Text = "المستخدم/" & USER_NAME
        'Check_Sys_Featurs()

        '-------------------------------------------------------------------


        Dim mailItems = New List(Of MailItem)
        mailItems.Add(New MailItem(0, "مبيعات قطاعي"))

        If S_Allow_MinSP Then
            If U_Sell_Under_Min_SP = True Then mailItems.Add(New MailItem(1, "مبيعات جملة"))
            If U_Sell_Under_Min_SP_2 = True Then mailItems.Add(New MailItem(2, "مبيعات جملة الجملة"))
        End If


        SALES_TYPES_CMB.DataSource = mailItems
        SALES_TYPES_CMB.DisplayMember = "name"
        SALES_TYPES_CMB.ValueMember = "ID"

        If U_Sell_Under_Min_SP = False And U_Sell_Under_Min_SP_2 = False Then SALES_TYPES_CMB.SelectedValue = 0

        Try
            SALES_TYPES_CMB.SelectedValue = MY_Settings.SALES_TYPES_CMB
        Catch ex As Exception
            SALES_TYPES_CMB.SelectedValue = 0
        End Try

        If SALES_TYPES_CMB.Items.Count = 1 Then SALES_TYPES_CMB.Visible = False
        Show_Cash_LB.Visible = U_SB_Show_Cash


    End Sub

    Public Sub Load_ALL_IM()
        Dim c As New C
        Dim s As String
        GM_Dt = New DataTable
        Try
            GM_Dt.Clear()
            s = "select * from General_menu Where POS_isShow = 1 Order By Rank_Num ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(GM_Dt)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------
        c = New C
        IM_POS_Dt = New DataTable
        Try
            IM_POS_Dt.Clear()
            s = "select *  from IM_ActiveList_V ORDER BY IM_ID ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_POS_Dt)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Public Sub Load_ST()
        Dim c As New C
        Try
            Dim s As String
            s = "select ST_ID,ST_name from STORES ORDER By ST_ID ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            ST_cm.DataSource = c.Dt
            ST_cm.DisplayMember = "ST_name"
            ST_cm.ValueMember = "ST_ID"
            ST_cm.SelectedValue = SB_ST_ID
            If SB_ST_Can_change = False Then ST_cm.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Load_PauseBills()
        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "SB_PauseBill_SelectList"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
            ' .Parameters.AddWithValue("@SB_TypeOf_Inc", SB_TypeOf_Inc)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Me.PauseCmb.DataSource = C.Dt
        Me.PauseCmb.ValueMember = "T_ID"
        Me.PauseCmb.DisplayMember = "Bill_Num"
        PauseCmb.SelectedIndex = -1

        If C.Dt.Rows.Count > 0 Then
            MoveToBill_Btn.Enabled = True
        Else
            MoveToBill_Btn.Enabled = False
        End If

    End Sub


    Public Sub Load_Bill_Types()
        Dim c As New C
        'If POS_ENTER_AS_ORDER = True Then
        '    c.Str = "select B_Type_ID,B_Name from Sales_Bills_Types WHERE B_Type_ID = 3"
        'Else
        '    c.Str = "select B_Type_ID,B_Name from Sales_Bills_Types WHERE B_Type_ID <> 3"
        'End If
        c.Str = "select B_Type_ID,B_Name from Sales_Bills_Types"
        c.Da = New SqlDataAdapter(c.Str, c.Con)
        c.Da.Fill(c.Dt)
        BillTypeCmb.DataSource = c.Dt
        BillTypeCmb.DisplayMember = "B_Name"
        BillTypeCmb.ValueMember = "B_Type_ID"
    End Sub

    Private Sub Sales_Load_GM()



        'Dim GM_Name As String
        'Dim x = 0
        'Dim y = 0

        'Dim C As New C
        'C.Con.Open()
        'With C.Com
        '    .Connection = C.Con
        '    .CommandText = "GM_SelectList"
        '    .CommandType = CommandType.StoredProcedure
        'End With

        'row = C.Com.ExecuteReader
        'If row.HasRows = True Then
        '    While row.Read()

        '        Dim SMbtn As New Button
        '        SMbtn.Name = ("GMbtn" + row("GM_ID").ToString)
        '        GM_Name = row("GM_Name")

        '        SMbtn.AutoSize = False
        '        SMbtn.Cursor = Cursors.Hand
        '        SMbtn.FlatStyle = FlatStyle.Flat
        '        SMbtn.Location = New System.Drawing.Point(x, y)
        '        SMbtn.Size = New System.Drawing.Size(SMPanel.Size.Width - 15, SMPanel.Size.Height / 10)
        '        SMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
        '        SMbtn.Font = New System.Drawing.Font("JF Flat", 9, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        '        SMbtn.Text = GM_Name
        '        SMbtn.TextAlign = ContentAlignment.MiddleCenter
        '        SMbtn.Tag = row("GM_ID")
        '        Me.Controls.Add(SMbtn)
        '        SMbtn.Parent = Me.SMPanel
        '        AddHandler SMbtn.Click, AddressOf SMbtn_Click
        '        SMbtn.BackColor = Color.White

        '        If IsDBNull(row("BK_R")) Then
        '            SMbtn.BackColor = System.Drawing.SystemColors.Info
        '        Else
        '            SMbtn.BackColor = Color.FromArgb(row("BK_R"), row("BK_G"), row("BK_B"))
        '        End If

        '        If IsDBNull(row("FK_R")) = False Then
        '            SMbtn.ForeColor = Color.FromArgb(row("FK_R"), row("FK_G"), row("FK_B"))
        '        End If

        '        y += (SMPanel.Size.Height / 10)

        '    End While
        'End If
        'C.Con.Close()
        '------------------------------------------------------------------------------------------------------------------------------------


        Dim GM_Name As String
        Dim x = 0
        Dim y = 0


        For Each row As DataRow In GM_Dt.Rows

            Dim SMbtn As New Button
            SMbtn.Name = ("GMbtn" + row("GM_ID").ToString())
            GM_Name = row("GM_Name").ToString()

            SMbtn.AutoSize = False
            SMbtn.Cursor = Cursors.Hand
            SMbtn.FlatStyle = FlatStyle.Flat
            SMbtn.Location = New System.Drawing.Point(x, y)
            SMbtn.Size = New System.Drawing.Size(SMPanel.Size.Width - 15, SMPanel.Size.Height / 10)
            SMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
            SMbtn.Font = New System.Drawing.Font("JF Flat", 9, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
            SMbtn.Text = GM_Name
            SMbtn.TextAlign = ContentAlignment.MiddleCenter
            SMbtn.Tag = row("GM_ID").ToString()
            Me.Controls.Add(SMbtn)
            SMbtn.Parent = Me.SMPanel
            AddHandler SMbtn.Click, AddressOf SMbtn_Click
            SMbtn.BackColor = Color.White

            If IsDBNull(row("BK_R")) Then
                SMbtn.BackColor = System.Drawing.SystemColors.Info
            Else
                SMbtn.BackColor = Color.FromArgb(Convert.ToInt32(row("BK_R")), Convert.ToInt32(row("BK_G")), Convert.ToInt32(row("BK_B")))
            End If

            If IsDBNull(row("FK_R")) = False Then
                SMbtn.ForeColor = Color.FromArgb(Convert.ToInt32(row("FK_R")), Convert.ToInt32(row("FK_G")), Convert.ToInt32(row("FK_B")))
            End If

            y += (SMPanel.Size.Height / 10)

        Next



    End Sub

    Sub SMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        IMPanel.Tag = ""
        GM_ID = sender.tag
        Fetch_IM()
    End Sub

    Public Sub Shortcuts_Load_IM(GM_ID As String)

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

                FontSize = ((h / 100) * (w / 100)) / (IMName.Count * IMName.Count)
                StandarMeasure = (w + h) / 190
                Try
                    IMbtn.Font = New System.Drawing.Font("JF Flat", FontSize + StandarMeasure, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                Catch ex As Exception
                    IMbtn.Font = New System.Drawing.Font("JF Flat", 12, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                End Try

                'FontSize = ((h / w) * 100) / IMName.Count
                'StandarMeasure = (w + h) / 190
                'Try
                '    IMbtn.Font = New System.Drawing.Font("JF Flat", (FontSize + StandarMeasure) - 2.5, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                'Catch ex As Exception
                '    IMbtn.Font = New System.Drawing.Font("JF Flat", 12, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                'End Try

                IMbtn.Text = C.Dr("item_name")
                IMbtn.Tag = C.Dr("IM_ID")
                IMbtn.TabStop = C.Dr("isValid")
                Controls.Add(IMbtn)
                IMbtn.Parent = IMPanel

                If IsDBNull(C.Dr("photo")) = False Then
                    IMbtn.BackColor = System.Drawing.SystemColors.Info
                    FontSize = (w + h) / 180
                    IMbtn.Font = New System.Drawing.Font("JF Flat", FontSize, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
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
                IMbtn.Enabled = False
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

    Public Sub Sales_Load_IM(GM_ID As String)

        'IMPanel.Controls.Clear()
        'Dim IMName As String
        'Dim FontSize
        'Dim StandarMeasure As Double
        'Dim counter = 1
        'Dim x = 5
        'Dim y = 10
        'Dim C As New C
        'With C.Com
        '    .Connection = C.Con
        '    .CommandText = "IM_SelectActiveList"
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.AddWithValue("@GM_ID", GM_ID)
        'End With

        'C.Con.Open()
        'row = C.Com.ExecuteReader
        'If row.HasRows Then
        '    While row.Read()
        '        counter += 1
        '        Dim IMbtn As New Button
        '        IMbtn.Name = ("IMbtn" + row("IM_ID").ToString)
        '        IMbtn.AutoSize = False
        '        IMbtn.Cursor = Cursors.Hand
        '        IMbtn.FlatStyle = FlatStyle.Popup
        '        IMbtn.Location = New System.Drawing.Point(x, y)

        '        IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
        '        IMName = row("item_nameSales")

        '        'FontSize = ((h / w) * 100) / IMName.Count
        '        'StandarMeasure = (w + h) / 190

        '        ''''''''''''''''''''''''''''''''''''''''
        '        FontSize = ((h / 100) * (w / 100)) / (IMName.Count * IMName.Count)
        '        StandarMeasure = ((w + h) / 190)

        '        Select Case POS_IM_COUNT
        '            Case 20
        '                Try
        '                    IMbtn.Size = New System.Drawing.Size(IMPanel.Size.Width / 5.2, IMPanel.Size.Height / 4.25)
        '                    IMbtn.Font = New System.Drawing.Font("JF Flat", FontSize + StandarMeasure, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        '                Catch ex As Exception
        '                    IMbtn.Font = New System.Drawing.Font("JF Flat", 12, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        '                End Try
        '            Case 36
        '                IMbtn.Size = New System.Drawing.Size(IMPanel.Size.Width / 6.2, IMPanel.Size.Height / 6.2)
        '                Try
        '                    IMbtn.Font = New System.Drawing.Font("JF Flat", FontSize + StandarMeasure, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        '                Catch ex As Exception
        '                    IMbtn.Font = New System.Drawing.Font("JF Flat", 12, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        '                End Try

        '        End Select


        '        ''''''''''''''''''''''''''''''''''''''''''

        '        'Try
        '        '    IMbtn.Font = New System.Drawing.Font("JF Flat", (FontSize + StandarMeasure) - 2.5, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        '        'Catch ex As Exception
        '        '    IMbtn.Font = New System.Drawing.Font("JF Flat", 12, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        '        'End Try

        '        IMbtn.Text = row("item_nameSales")
        '        IMbtn.Tag = row("IM_ID")
        '        IMbtn.TabStop = row("isValid")
        '        Controls.Add(IMbtn)
        '        IMbtn.Parent = IMPanel
        '        AddHandler IMbtn.Click, AddressOf IMbtn_Click
        '        If IsDBNull(row("photo")) = False Then
        '            IMbtn.BackColor = System.Drawing.SystemColors.Info
        '            FontSize = ((w + h) / 180) + 1.5
        '            IMbtn.Font = New System.Drawing.Font("JF Flat", FontSize, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        '            Data = DirectCast(row("photo"), Byte())
        '            Dim MS As New MemoryStream(Data)

        '            Dim ImageH As Integer = (IMbtn.Size.Height / 2)
        '            Dim ImageW As Integer = IMbtn.Size.Width
        '            IMbtn.Image = ResizeImage(Image.FromStream(MS), ImageH, ImageW)
        '            IMbtn.TextAlign = ContentAlignment.BottomCenter
        '            IMbtn.ImageAlign = ContentAlignment.BottomCenter
        '        Else
        '            IMbtn.Image = Nothing
        '            IMbtn.TextAlign = ContentAlignment.MiddleCenter
        '        End If

        '        If IsDBNull(row("BK_R")) Then
        '            IMbtn.BackColor = System.Drawing.SystemColors.Info
        '        Else
        '            IMbtn.BackColor = Color.FromArgb(row("BK_R"), row("BK_G"), row("BK_B"))
        '        End If

        '        If IsDBNull(row("FK_R")) = False Then
        '            IMbtn.ForeColor = Color.FromArgb(row("FK_R"), row("FK_G"), row("FK_B"))
        '        End If


        '        Select Case POS_IM_COUNT
        '            Case 20
        '                If counter = 6 Then
        '                    counter = 1
        '                    x = 5
        '                    y += IMPanel.Size.Height / 4.25
        '                Else
        '                    x += IMPanel.Size.Width / 5.2
        '                End If

        '            Case 36
        '                If counter = 7 Then
        '                    counter = 1
        '                    x = 5
        '                    y += IMPanel.Size.Height / 6.2
        '                Else
        '                    x += IMPanel.Size.Width / 6.2
        '                End If


        '        End Select



        '        rs.Find_One(IMbtn)

        '    End While
        'End If

        'C.Con.Close()
        '------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim filteredRows() As DataRow = IM_POS_Dt.Select("GM_ID = " & GM_ID)

        IMPanel.Controls.Clear()

        If filteredRows.Count > 0 Then



            Dim IMName As String
            Dim FontSize
            Dim StandarMeasure As Double
            Dim counter = 1
            Dim x = 5
            Dim y = 10

            For Each row As DataRow In filteredRows


                counter += 1
                Dim IMbtn As New Button
                IMbtn.Name = ("IMbtn" + row("IM_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)

                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMName = row("item_nameSales").ToString

                FontSize = ((h / 100) * (w / 100)) / (IMName.Count * IMName.Count)
                StandarMeasure = ((w + h) / 190)

                Select Case POS_IM_COUNT
                    Case 20
                        Try
                            IMbtn.Size = New System.Drawing.Size(IMPanel.Size.Width / 5.2, IMPanel.Size.Height / 4.25)
                            IMbtn.Font = New System.Drawing.Font("JF Flat", FontSize + StandarMeasure, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                        Catch ex As Exception
                            IMbtn.Font = New System.Drawing.Font("JF Flat", 12, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                        End Try
                    Case 36
                        IMbtn.Size = New System.Drawing.Size(IMPanel.Size.Width / 6.2, IMPanel.Size.Height / 6.2)
                        Try
                            IMbtn.Font = New System.Drawing.Font("JF Flat", FontSize + StandarMeasure, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                        Catch ex As Exception
                            IMbtn.Font = New System.Drawing.Font("JF Flat", 12, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                        End Try

                End Select
                IMbtn.Text = row("item_nameSales").ToString
                IMbtn.Tag = row("IM_ID").ToString
                IMbtn.TabStop = Convert.ToBoolean(row("isValid"))
                Controls.Add(IMbtn)
                IMbtn.Parent = IMPanel
                AddHandler IMbtn.Click, AddressOf IMbtn_Click
                If IsDBNull(row("photo")) = False Then
                    IMbtn.BackColor = System.Drawing.SystemColors.Info
                    FontSize = ((w + h) / 180) + 1.5
                    IMbtn.Font = New System.Drawing.Font("JF Flat", FontSize, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                    Data = DirectCast(row("photo"), Byte())
                    Dim MS As New MemoryStream(Data)

                    Dim ImageH As Integer = (IMbtn.Size.Height / 2)
                    Dim ImageW As Integer = IMbtn.Size.Width
                    IMbtn.Image = ResizeImage(Image.FromStream(MS), ImageH, ImageW)
                    IMbtn.TextAlign = ContentAlignment.BottomCenter
                    IMbtn.ImageAlign = ContentAlignment.BottomCenter

                    '  IMbtn.ItemImage = ResizeImage(Image.FromStream(MS), ImageH, ImageW)

                Else
                    IMbtn.Image = Nothing
                    IMbtn.TextAlign = ContentAlignment.MiddleCenter

                    ' IMbtn.ItemImage = Nothing
                End If

                If IsDBNull(row("BK_R")) Then
                    IMbtn.BackColor = System.Drawing.SystemColors.Info
                Else
                    IMbtn.BackColor = Color.FromArgb(Convert.ToInt32(row("BK_R")), Convert.ToInt32(row("BK_G")), Convert.ToInt32(row("BK_B")))
                End If

                If IsDBNull(row("FK_R")) = False Then
                    IMbtn.ForeColor = Color.FromArgb(Convert.ToInt32(row("FK_R")), Convert.ToInt32(row("FK_G")), Convert.ToInt32(row("FK_B")))
                End If

                Select Case POS_IM_COUNT
                    Case 20
                        If counter = 6 Then
                            counter = 1
                            x = 5
                            y += IMPanel.Size.Height / 4.25
                        Else
                            x += IMPanel.Size.Width / 5.2
                        End If

                    Case 36
                        If counter = 7 Then
                            counter = 1
                            x = 5
                            y += IMPanel.Size.Height / 6.2
                        Else
                            x += IMPanel.Size.Width / 6.2
                        End If


                End Select

                rs.Find_One(IMbtn)

            Next

        End If


    End Sub


    Sub IMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)

        IM_ID = sender.tag.ToString
        IM_Name = sender.Text
        is_Valid = sender.TabStop
        U_IM_ID = 0

        If SB_IM_Alert_When_Repetition = True Then
            For i = 0 To MetroGrid.Rows.Count - 1
                If MetroGrid.Rows(i).Cells("IM_ID_CL").Value = IM_ID Then
                    My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                    Dim MSG As New YesNo
                    MSG.Msg_Lb.Text = "هذا الصنف تم إدراجه بالفاتورة ... هل تريد الإستمرار"
                    MSG.ShowDialog()
                    If MSG.Result = True Then
                        Fetch_ItemToList(IM_ID)
                        Exit Sub
                    Else
                        CLEAR_BARCODE_Fildes()
                        Exit Sub
                    End If
                    '----------------------
                End If
            Next
        End If

        Fetch_ItemToList(IM_ID)
    End Sub

    Private Function Check_is_Get_Price()
        Dim c As New C
        Try
            Dim s As String
            s = "select Price,Min_SP,Min_SP_2,isStore from IM_Menu_Units_V WHERE IM_ID = '" & IM_ID & "' AND is_Default = 1"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()

                If c.Dr("isStore") = 1 Then
                    If SALES_TYPES_CMB.SelectedValue = 1 Then If c.Dr("Min_SP") = 0 Then Return 0
                    If SALES_TYPES_CMB.SelectedValue = 2 Then If c.Dr("Min_SP_2") = 0 Then Return 0
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 1
    End Function

    Public Sub Fetch_ItemToList(IM_ID As String)
        If is_Valid = True Then
            POS_D_Valid.ST_ID = SB_ST_ID
            POS_D_Valid.IM_ID = IM_ID
            POS_D_Valid.ShowDialog()
            If Valid_Allow_IM = False Then Exit Sub
        End If

        Dim Qty_St As String = ""

        If IM_min_QTY = False Then
            If BillTypeCmb.SelectedValue <> 3 Then
                If IM_Check_Neg_QTY_() = 1 Then
                    My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                    Dim MSG As New OK
                    MSG.Msg_Lb.Text = "لا يمكنك إدراج صنف بكمية سالبة"
                    MSG.ShowDialog()
                    Exit Sub
                End If
            End If
        End If

        If Check_is_Get_Price() = 0 Then
            My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
            Dim MSG As New OK
            MSG.Msg_Lb.Text = " يجب إدراج سعر للصنف قبل البيع "
            MSG.ShowDialog()
            Exit Sub
        End If

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Contents_INSERT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@U_IM_ID", U_IM_ID)
            .Parameters.AddWithValue("@Barcode", Barcode_IM)
            If is_Valid = True Then .Parameters.AddWithValue("@D_Vaild", D_Valid)
            If String.IsNullOrWhiteSpace(NumTextBox.Text) = False Then .Parameters.AddWithValue("@QYT", Convert.ToDouble(NumTextBox.Text))
            If IM_Price <> 0 Then .Parameters.AddWithValue("@IM_Price", IM_Price)
            .Parameters.AddWithValue("@On_Update", On_Update)
            .Parameters.AddWithValue("@SALES_TYPES", SALES_TYPES_CMB.SelectedValue)
        End With
        If SQL_SP_EXEC(C.Com) = True Then

            If On_Update = True Then
                DependingUpdatedBill(T_ID)
            End If

            Network_Edit_Tracker_insert(" الصنف:" & IM_Name & " العدد:" & Qty_St & " السعر المسجل:" & IM_Price & " الباركود المدرج:" + Barcode_IM, SB_ID_Txt.Text, 1, 1)

            If String.IsNullOrWhiteSpace(NumTextBox.Text) = False Then
                Qty_St = NumTextBox.Text
            Else
                Qty_St = 1
            End If


            CLEAR_BARCODE_Fildes()
            SB_Contents_SELECT_Bill()
            ' NumTextBox.Clear()

            If CHECK_IF_IM_RSV(IM_ID) = 1 Then Prepare_For_Rsv()

            'If Row_Index > 0 Then MetroGrid.CurrentCell = MetroGrid.Rows(Row_Index).Cells(3)
            IM_ID = 0
            D_Valid = ""
            IM_Price = 0
        End If

    End Sub

    Private Function IM_Check_Neg_QTY_()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_POS"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@D_Vaild", D_Valid)
            If String.IsNullOrWhiteSpace(NumTextBox.Text) = False Then .Parameters.AddWithValue("@Enterd_Qty", Convert.ToDouble(NumTextBox.Text))

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function

    Public Sub Mange_FormOptions()

        POS_CHECK_USER_Valid()
        'BillTypeCmb.Enabled = True
        Select_Categories()
        rs.FindAllControls(Me)
        Me.Size = New Size(w, h)

        Load_PauseBills()
        Load_Bill_Types()
        If Open_NewBill_When_OpenSale = True Then
            ResetNewBill()
        Else
            Me.Barcode_txt.Enabled = False
            IMPanel.Enabled = False
        End If


        If isPrintAfterCash = True And String.IsNullOrWhiteSpace(Default_Printer_80) And PrintBillButton.Visible = True Then MsgBox("لم يتم تحديد طابعة البيع السريع الإفتراضية", MsgBoxStyle.Exclamation, "تحديــد طابعة الكاشير")

    End Sub

    Private Sub Select_Categories()
        If POS_By_Shortcut = 0 Then
            Sales_Load_GM()
        Else
            Sales_Load_Shortcuts()
        End If
    End Sub

    Private Sub Sales_Load_Shortcuts()

        'Dim FontSize, NameSize
        Dim GM_Name As String
        Dim x = 0
        Dim y = 0

        Dim C As New C
        Dim s As String
        s = "select Short_ID,Short_Name from Shortcut_Group Order By Short_ID ASC"
        C.Com = New SqlClient.SqlCommand(s, C.Con)

        C.Con.Open()

        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows = True Then
            While C.Dr.Read()

                Dim SMbtn As New Button
                SMbtn.Name = ("GMbtn" + C.Dr("Short_ID").ToString)
                GM_Name = C.Dr("Short_Name")
                'NameSize = GM_Name.Count
                'If NameSize < 3 Then NameSize += 2
                SMbtn.AutoSize = False
                SMbtn.Cursor = Cursors.Hand
                SMbtn.FlatStyle = FlatStyle.Flat
                SMbtn.Location = New System.Drawing.Point(x, y)
                SMbtn.Size = New System.Drawing.Size(SMPanel.Size.Width - 15, SMPanel.Size.Height / 9)
                SMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes

                SMbtn.Font = New System.Drawing.Font("JF Flat", 10, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))


                'FontSize = ((h / w) * 100) / GM_Name.Count
                'SMbtn.Font = New System.Drawing.Font("JF Flat", (FontSize + 1.5), Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))

                SMbtn.Text = GM_Name
                SMbtn.TextAlign = ContentAlignment.MiddleCenter
                SMbtn.Tag = C.Dr("Short_ID")
                Me.Controls.Add(SMbtn)
                SMbtn.Parent = Me.SMPanel
                AddHandler SMbtn.Click, AddressOf SMbtn_Click
                SMbtn.BackColor = Color.White


                y += (SMPanel.Size.Height / 9)

            End While
        End If
        C.Con.Close()
    End Sub

    Public Sub POS_CHECK_USER_Valid()

        Edit_butt.Visible = U_SB_Update
            VoidBillBtn.Visible = U_SalesVoid

            If U_SalesDis = True And isDiscount = True Then
                DiscountBillBtn.Visible = True
            Else
                Cancel_Discount()
                DiscountBillBtn.Visible = False
            End If

            PrintBillButton.Visible = isShowPrintBtn

            ChangeTbButton.Visible = S_Tables
            TB_Name_Lb.Visible = S_Tables
    
            DiscountPanel.Visible = U_SB_Show_Price_Info
            MetroGrid.Columns("Unit_Price_CL").Visible = U_SB_Show_Price_Info
            ChangeAGButton.Visible = U_SB_Show_Price_Info
            Finish_Order_Btn.Visible = Use_Inside_Barcode

    End Sub

    Public Sub Make_Discount()
        DiscountBillBtn.Visible = True
        DiscountPanel.Visible = True
        DiscountPanel.BringToFront()
        MetroGrid.Size = New Size(MetroGrid.Size.Width, IM_Option_Panel.Size.Height)
        Dim l1 = PureTextBox.Font.Size
        PureTextBox.Font = New System.Drawing.Font("Stencil", l1, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub

    Private Sub Cancel_Discount()
        DiscountPanel.Visible = False
        DiscountPanel.SendToBack()
    End Sub

    Private Sub Sales_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls_POS(Me)
        '  TabControl1.Region = New Region(New RectangleF(IMTabPage.Left, IMTabPage.Top, IMTabPage.Width, IMTabPage.Height))
    End Sub

    Public Sub Fetch_IM()
        If POS_By_Shortcut = 0 Then
            Sales_Load_IM(GM_ID)
        Else
            Shortcuts_Load_IM(GM_ID)
        End If
    End Sub

    Public Sub Reset_Fields()


        Fetch_IM()

        Switch_To_Cash = False
        If TB_ID > 0 Then AG_Balance_Update_Pure()
        Me.T_ID = 0
        TB_ID = Default_TB_ID
        AG_ID = SB_Default_AG
        AG_TypeID = 1
        Point = 0
        BillNum = 0
        Load_PauseBills()
        If POS_ENTER_AS_ORDER = False Then BillTypeCmb.SelectedValue = SB_DefaultStatus
        Manage_Reseve()
        NumTextBox.Clear()
        TotalTextBox.Clear()
        DiscountTextBox.Clear()
        isNewBill = 1
        AG_Desc_lb.Text = " الزبون : "
        TB_Name_Lb.Text = "الطاولة : "
        isPied = 0
        isDepended = 0
        SaveButton.Enabled = True
        Finish_Order_Btn.Enabled = True
        IMPanel.Enabled = True
        IM_Option_Panel.Enabled = True
        BillPage_Panel.Enabled = True
        isPre_Print = False
        VoidLb.Visible = False
        Barcode = ""
        AG_Name = ""
        BillNotesButton.Enabled = True
        ChangeAGButton.Enabled = True
        ChangeTbButton.Enabled = True
        DiscountTextBox.Enabled = True
        DiscountBillBtn.Enabled = True
        VoidBillBtn.Enabled = True
        AG_Desc_lb.Enabled = True
        TB_Name_Lb.Enabled = True
        If DiscountPanel.Visible = True Then Cancel_Discount()

        IMDicreaseButton.Enabled = True
        IMIncreaseButton.Enabled = True
        ChangePriceButton.Enabled = True
        Units_btn.Enabled = True
        Edit_butt.BackColor = Color.WhiteSmoke
        PrintBillButton.Enabled = False
        BillTypeCmb.Enabled = False
        PureTextBox.Text = "0"
        TB_isOrderd = 0
        TB_is_Cash = 0
        CLEAR_BARCODE_Fildes()
        PIED_OK = False
        ' Pay_ID = 1
    End Sub

    Public Sub ResetNewBill()
        Reset_Fields()
        Call_New_Bill()
    End Sub
    Private Sub AG_Balance_Update_Pure()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "AG_Balance_Update_Pure"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
            .Parameters.AddWithValue("@Pure", Me.PureTextBox.Text)
        End With
        SQL_SP_EXEC(C.Com)
    End Sub
    Private Sub Call_New_Bill()
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "Call_New_SalesBill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", 0)
            .Parameters.AddWithValue("@AG_ID", SB_Default_AG)
            .Parameters.AddWithValue("@Bill_Num", 0)
            .Parameters.AddWithValue("@SB_ID", 0)
            .Parameters.AddWithValue("@isNew", 0)
            If POS_ENTER_AS_ORDER = False Then
                .Parameters.AddWithValue("@SB_Type", Me.BillTypeCmb.SelectedValue)
            Else
                .Parameters.AddWithValue("@SB_Type", 3)
            End If
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
            .Parameters.AddWithValue("@isPied", 0)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            If TB_ID > 0 Then .Parameters.AddWithValue("@TB_ID", TB_ID)
            .Parameters("@T_ID").Direction = ParameterDirection.Output
            .Parameters("@Bill_Num").Direction = ParameterDirection.Output
            .Parameters("@isNew").Direction = ParameterDirection.Output
            .Parameters("@SB_ID").Direction = ParameterDirection.Output
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Me.T_ID = C.Com.Parameters("@T_ID").Value
            BillNumTxt.Text = C.Com.Parameters("@Bill_Num").Value.ToString()
            isNewBill = C.Com.Parameters("@isNew").Value

            Fill_Bill_Info()
            SB_Contents_SELECT_Bill()

        End If
    End Sub


    Public Sub Fill_Bill_Info()
        AG_Desc_lb.Text = " الزبون : "
        TB_Name_Lb.Text = "الطاولة : "
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Info_V_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()
            AG_ID = C.Dr("AG_ID")
            AG_TypeID = C.Dr("Type_ID")
            'If AG_TypeID = 3 Then 'Calc_Bill()
            AG_Desc_lb.Text = AG_Desc_lb.Text + C.Dr("Ag_name")
            AG_Name = C.Dr("Ag_name")

            BillNumTxt.Text = C.Dr("S_Bill_Pr_ID")

            If C.Dr("TB_ID") = 0 Then
                TB_Name_Lb.Text = ""
                TB_ID = 0
            Else
                TB_Name_Lb.Text = TB_Name_Lb.Text + C.Dr("T_Name").ToString
                TB_ID = C.Dr("TB_ID")
            End If

            Barcode = C.Dr("Barcode")
            TB_isOrderd = C.Dr("TB_isOrderd")

            Bill_Date_Str = C.Dr("date")
            Cr_Phone = C.Dr("Cr_Phone")

            If C.Dr("isDepended") = 1 Then
                IMPanel.Enabled = False
                IM_Option_Panel.Enabled = False
                SaveButton.Enabled = False
                Finish_Order_Btn.Enabled = False
                BillNotesButton.Enabled = False
                ChangeAGButton.Enabled = False
                ChangeTbButton.Enabled = False
                DiscountBillBtn.Enabled = False
                AG_Desc_lb.Enabled = False
                TB_Name_Lb.Enabled = False

                Edit_butt.Enabled = True

                MetroGrid.BackgroundColor = Color.LightGreen
                MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
                TableLayoutPanel1.Enabled = False

                Barcode_txt.Enabled = False
                BillTypeCmb.Enabled = False

            Else

                MetroGrid.BackgroundColor = Color.LightYellow
                MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                TableLayoutPanel1.Enabled = True

                Barcode_txt.Enabled = True
                BillTypeCmb.Enabled = True
                Edit_butt.Enabled = False
            End If

            If C.Dr("Discount") > 0 Then
                Make_Discount()
                DiscountTextBox.Text = C.Dr("Discount")
                DISCOUNT = C.Dr("Discount")
                PureTextBox.Text = C.Dr("Total") - C.Dr("Discount")
            End If

            If C.Dr("isVoid") = 1 Then
                VoidLb.Visible = True
                BillPage_Panel.Enabled = False
                IMPanel.Enabled = False
                IM_Option_Panel.Enabled = False
                SaveButton.Enabled = False
                Finish_Order_Btn.Enabled = False
                Edit_butt.Enabled = False
                MetroGrid.BackgroundColor = Color.IndianRed
                MetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed

            Else
                VoidBillBtn.Enabled = True
                VoidLb.Visible = False
            End If

            isPied = C.Dr("isPied")
            Prev_Type = C.Dr("S_Bills_Type")

            is_Show_Bill = True
            BillTypeCmb.SelectedValue = C.Dr("S_Bills_Type")
            is_Show_Bill = False

            Check_Table()

            If C.Dr("TB_isOrderd") = 1 And C.Dr("isVoid") = 0 And C.Dr("isDepended") = 1 Then

                ChangeTbButton.Enabled = False
                ChangeAGButton.Enabled = False
                BillNotesButton.Enabled = False
                DiscountBillBtn.Enabled = False

                IMPanel.Enabled = False
                IM_Option_Panel.Enabled = False
                SaveButton.Enabled = False
                Finish_Order_Btn.Enabled = False
            End If

            If C.Dr("isDepended") = 1 And C.Dr("isVoid") = 0 Then PrintBillButton.Enabled = True
            SB_ID = C.Dr("SB_ID")
            SB_ID_Txt.Text = SB_ID

            If U_Save_otherBill = False And C.Dr("USER_ID") <> USER_ID Then
                If BillPage_Panel.Enabled = True Then BillPage_Panel.Enabled = False
                If SaveButton.Enabled = True Then SaveButton.Enabled = False
                If IMPanel.Enabled = True Then IMPanel.Enabled = False
                If IM_Option_Panel.Enabled = True Then IM_Option_Panel.Enabled = False
            End If

            If AGBillPage_ID = 3 Then Select_Sales_Receipt()
            If TB_ID > 0 Then Check_TB_is_Cash()

        Else
            AG_ID = Default_AG_ID
            AG_Desc_lb.Text = AG_Desc_lb.Text + " نقدي "
            TB_Name_Lb.Text = ""
            VoidBillBtn.Enabled = False
            Edit_butt.Enabled = False
        End If
        C.Con.Close()
    End Sub

    Private Sub Check_TB_is_Cash()
        Dim C = New C

        Dim S As String = " select is_Cash from TABLES WHERE TB_ID = " & TB_ID
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                TB_is_Cash = C.Dr("is_Cash")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub

    Private Sub Select_Sales_Receipt()
        Dim C = New C

        Dim S As String = " select ISNULL(SUM(Value),0) AS Value from SB_Receipts_V WHERE Receipt_Tran_ID = '" & T_ID & "' AND isVoid = 0"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Pied = C.Dr("Value")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub


    Public Sub SB_Contents_SELECT_Bill()
        Dim C As New C
        IM_Dt.Clear()
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Contents_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        C.Da.Fill(IM_Dt)
        MetroGrid.DataSource = C.Dt
        If MetroGrid.Rows.Count > 0 Then MetroGrid.CurrentCell = MetroGrid.Rows(MetroGrid.Rows.Count - 1).Cells("IM_NameCL")
        'Calc_Bill()
    End Sub

    Private Sub IMIncreaseButton_Click_1(sender As Object, e As EventArgs) Handles IMIncreaseButton.Click
        If MetroGrid.Rows.Count > 0 Then
            Dim Def As Integer = 1
            If IM_min_QTY = False Then
                If BillTypeCmb.SelectedValue <> 3 Then
                    If IM_Check_Neg_QTY_2() = 1 Then
                        My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                        Dim MSG As New OK
                        MSG.Msg_Lb.Text = "لا يمكنك إدراج صنف بكمية سالبة"
                        MSG.ShowDialog()
                        Exit Sub
                    End If
                End If
            End If
            Change_IM_Qty(Def)
        End If
    End Sub

    Private Function IM_Check_Neg_QTY_2()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_POS_2"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@T_ID", MetroGrid.CurrentRow.Cells("IM_T_ID").Value)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function

    Private Sub Change_IM_Qty(def As Integer)
        Dim SB_T_ID As Integer = MetroGrid.CurrentRow.Cells("IM_T_ID").Value
        Dim Row_Index As Integer = MetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Contents_Change_IM_Qty"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", SB_T_ID)
            .Parameters.AddWithValue("@Def", def)
            .Parameters.AddWithValue("@On_Update", On_Update)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            If On_Update Then
                Network_Edit_Tracker_insert(" الصنف:" + MetroGrid.CurrentRow.Cells("IM_NameCL").Value.ToString + " العدد:" + (MetroGrid.CurrentRow.Cells("QTY_CL").Value + def).ToString, _
             SB_ID_Txt.Text, 1, 3)
            End If
            SB_Contents_SELECT_Bill()

            MetroGrid.CurrentCell = MetroGrid.Rows(Row_Index).Cells(3)
        End If
    End Sub

    Private Sub RefreashButton_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If On_Update = True Then Edit_butt_Click(sender, e)
        ResetNewBill()
    End Sub

    Private Sub B1_Click(sender As Object, e As EventArgs) Handles B1.Click
        Test_Num(sender)
    End Sub

    Private Sub Test_Num(Btn As Button)
        Select Case IMPanel.Tag
            Case "SBChangePrice"
                F_SBChangePrice.NumTextBox.Text += Btn.Tag
            Case Else
                NumTextBox.Text += Btn.Tag
        End Select
    End Sub

    Private Sub B2_Click(sender As Object, e As EventArgs) Handles B2.Click
        Test_Num(sender)
    End Sub

    Private Sub B3_Click(sender As Object, e As EventArgs) Handles B3.Click
        Test_Num(sender)
    End Sub

    Private Sub B4_Click(sender As Object, e As EventArgs) Handles B4.Click
        Test_Num(sender)
    End Sub

    Private Sub B5_Click(sender As Object, e As EventArgs) Handles B5.Click
        Test_Num(sender)
    End Sub

    Private Sub B6_Click(sender As Object, e As EventArgs) Handles B6.Click
        Test_Num(sender)
    End Sub

    Private Sub B7_Click(sender As Object, e As EventArgs) Handles B7.Click
        Test_Num(sender)
    End Sub

    Private Sub B8_Click(sender As Object, e As EventArgs) Handles B8.Click
        Test_Num(sender)
    End Sub

    Private Sub B9_Click(sender As Object, e As EventArgs) Handles B9.Click
        Test_Num(sender)
    End Sub

    Private Sub B0_Click(sender As Object, e As EventArgs) Handles B0.Click
        Test_Num(sender)
    End Sub

    Private Sub BDot_Click(sender As Object, e As EventArgs) Handles BDot.Click

        Select Case IMPanel.Tag
            'Case "SB_TablesMenu"
            '    If F_SB_TablesMenu.NumTextBox.Text.Contains(".") = False Then
            '        F_SB_TablesMenu.NumTextBox.Text += "."
            '    End If
            'Case "OrderOptions"
            '    If AG_ID > 1 Then
            '        If F_OrderOptions.Piedmoney_txt.Text.Contains(".") = False Then
            '            F_OrderOptions.Piedmoney_txt.Text += "."
            '        End If
            '    End If
            Case "SBChangePrice"
                If F_SBChangePrice.NumTextBox.Text.Contains(".") = False Then
                    F_SBChangePrice.NumTextBox.Text += "."
                End If
            Case Else
                If NumTextBox.Text.Contains(".") = False Then
                    NumTextBox.Text += "."
                End If
        End Select

    End Sub

    Private Sub ClearNumBtn_Click(sender As Object, e As EventArgs) Handles ClearNumBtn.Click

        Select Case IMPanel.Tag
            'Case "SB_TablesMenu"
            '    F_SB_TablesMenu.NumTextBox.Clear()
            'Case "OrderOptions"
            '    If AG_ID > 1 Then
            '        F_OrderOptions.Piedmoney_txt.Clear()
            '    End If
            Case "SBChangePrice"
                F_SBChangePrice.NumTextBox.Clear()
            Case Else
                NumTextBox.Clear()
        End Select

    End Sub

    Private Sub IMDicreaseButton_Click(sender As Object, e As EventArgs) Handles IMDicreaseButton.Click
        If MetroGrid.Rows.Count > 0 Then
            If MetroGrid.CurrentRow.Cells("QTY_CL").Value > 1 Then
                Dim Def As Integer = -1
                Change_IM_Qty(Def)
            End If
        End If
    End Sub

    Private Sub DeleteIMButton_Click(sender As Object, e As EventArgs) Handles DeleteIMButton.Click
        If MetroGrid.SelectedRows.Count > 0 Then SB_Contents_Delete_IM()
    End Sub

    Private Sub SB_Contents_Delete_IM()
        Dim Row_Index As Integer = MetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Contents_Delete_IM"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", MetroGrid.CurrentRow.Cells("IM_T_ID").Value)
            .Parameters.AddWithValue("@On_Update", On_Update)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            If On_Update = True Then Network_Edit_Tracker_insert(" الصنف:" + MetroGrid.CurrentRow.Cells("IM_NameCL").Value.ToString + " العدد:" + MetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString, SB_ID_Txt.Text, 1, 2)
            SB_Contents_SELECT_Bill()
            If Row_Index > 0 Then MetroGrid.CurrentCell = MetroGrid.Rows(Row_Index - 1).Cells(3)
        End If
    End Sub

    Public Sub Calc_Bill(HOLE_DISCOUNT As Boolean)
        TOTAL = 0
        DISCOUNT = 0
        PURE = 0
        For i = 0 To Me.MetroGrid.Rows.Count - 1
            TOTAL = TOTAL + MetroGrid.Rows(i).Cells("Total_CL").Value
        Next
        Me.TotalTextBox.Text = TOTAL

        If String.IsNullOrWhiteSpace(Me.DiscountTextBox.Text) Then Me.DiscountTextBox.Text = "0.00"

        DISCOUNT = Me.DiscountTextBox.Text

        Me.PureTextBox.Text = (TOTAL - Convert.ToDouble(Me.DiscountTextBox.Text)).ToString("n")
        Me.TotalTextBox.Text = TOTAL.ToString("n")
        Me.MetroGrid.Columns("QTY_CL").HeaderText = Me.MetroGrid.Rows.Count.ToString & "مادة "

        If TOTAL > 0 Then
            If AG_TypeID = 5 Then
                DISCOUNT = (StdDisPercent / 100) * TOTAL
                Me.DiscountTextBox.Text = DISCOUNT
                Me.PureTextBox.Text = (TOTAL - Convert.ToDouble(Me.DiscountTextBox.Text)).ToString("n")
                If DiscountPanel.Visible = False Then Make_Discount()
            ElseIf AG_TypeID = 7 Then
                DISCOUNT = (PrevDisPercent / 100) * TOTAL
                Me.DiscountTextBox.Text = DISCOUNT
                Me.PureTextBox.Text = (TOTAL - Convert.ToDouble(Me.DiscountTextBox.Text)).ToString("n")
                If DiscountPanel.Visible = False Then Make_Discount()
            End If
        End If

        PURE = TOTAL - DISCOUNT


        If AG_TypeID <> 5 Then
            If HOLE_DISCOUNT = False Then

                If SB_Remove_Dec = True Then
                    Dim TOTAL_NO_DEC As Double = 0
                    TOTAL_NO_DEC = Math.Floor(TOTAL)

                    If TOTAL_NO_DEC <> 0 Then
                        DISCOUNT = TOTAL - TOTAL_NO_DEC
                        PURE = TOTAL - DISCOUNT
                        Make_Discount()
                        TotalTextBox.Text = TOTAL
                        DiscountTextBox.Text = DISCOUNT
                        PureTextBox.Text = PURE
                    End If
                End If

            End If
        End If




    End Sub

    'Private Sub MetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles MetroGrid.RowsAdded
    '    ' 'Calc_Bill()
    'End Sub

    'Private Sub MetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles MetroGrid.RowsRemoved
    '    'If MetroGrid.Rows.Count = 0 Then DiscountTextBox.Clear()
    '    ' 'Calc_Bill()
    'End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click


        Print_Form.Show()
        Try

            Me.Enabled = False

            If MetroGrid.Rows.Count > 0 Then
                If U_SB_Show_Price_Info = True Then

                    If isDierct_Reseve = True Then
                        If (AG_ID = Default_AG_ID And AG_TypeID = 1) Or (AG_TypeID = 3 Or AG_TypeID = 5) Then


                            If TB_isOrderd = 1 Then switch_Bill_To_Current_User()

                            ConfermBill()
                            'Print_Bill()
                            'ResetNewBill()


                        ElseIf AG_ID <> Default_AG_ID Or AG_TypeID = 4 Then
                                F_OrderOptions = New OrderOptions
                            F_OrderOptions.ShowDialog()
                        End If
                    Else
                        If BillTypeCmb.SelectedValue = 3 Then
                            F_OrderOptions = New OrderOptions
                            F_OrderOptions.ShowDialog()
                        Else
                            If TB_is_Cash = 0 Then
                                AG_Balance_Update_TB_Ordered()
                            Else
                                ConfermBill()
                                'Print_Bill()
                                'ResetNewBill()
                            End If
                        End If
                    End If

                Else

                    If S_Tables = True Then
                        If TB_ID = 0 Then
                            Beep()
                            Dim MSG As New OK
                            MSG.Msg_Lb.Text = "!! حدد الطاولة "
                            MSG.ShowDialog()

                            ChangeTbButton_Click(sender, e)
                        Else
                            If TB_is_Cash = 0 Then
                                AG_Balance_Update_TB_Ordered()
                            Else
                                ConfermBill()
                                'Print_Bill()
                                'ResetNewBill()
                            End If

                        End If
                    End If

                End If

            End If

            Me.Enabled = True

        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Enabled = True
            ResetNewBill()
            'Me.Enabled = True
        End Try

        Print_Form.Hide()
    End Sub

    Private Sub switch_Bill_To_Current_User()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Switch_Bill_To_Current_User"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
        End With
        SQL_SP_EXEC(c.Com)
    End Sub


    Private Sub AG_Balance_Update_TB_Ordered()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Update_TB_Ordered"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
            .Parameters.AddWithValue("@TB_ID", Me.TB_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            If PrintTBKsh = True Then CashPrint_SB_Ksh()
            If CP_Bill_Screen = True Then Send_To_Bill_Screen()
            If isPhone_User = True Then
                Beep()
                Dim MSG As New OK
                MSG.Msg_Lb.Text = "تــم إرسال الطلب"
                MSG.ShowDialog()
            End If
            ResetNewBill()
        End If
    End Sub

    Private Sub AG_Balance_Update_Barcode_Ordered()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Update_Barcode_Order"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
            .Parameters.AddWithValue("@TOTAL", TOTAL)
            .Parameters.AddWithValue("@Discount", DISCOUNT)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            If SB_AutoPrint = True Then PrinBarcode()
            ResetNewBill()
        End If
    End Sub

    Private Sub PrinBarcode()


        Dim barc As New ZXing.BarcodeWriter
        barc.Format = ZXing.BarcodeFormat.CODE_128

        '  PictureBox1.Image = barc.Write(txtbarcode.Text)

        Dim by = DirectCast(New ImageConverter().ConvertTo(barc.Write("*" & Barcode), GetType(Byte())), Byte())

        Dim Class1 As New C
        Class1.Com = New SqlCommand
        Class1.Com.Connection = Class1.Con
        Class1.Com.CommandType = CommandType.StoredProcedure
        Class1.Com.CommandText = "[Barcode_insert]"
        Class1.Com.Parameters.AddWithValue("BarCode_Image", by)
        Class1.Con.Open()
        Class1.Com.ExecuteNonQuery()
        Class1.Con.Close()


        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\barcode.rpt")
        pp.CrTables = pp.rp.Database.Tables
        For Each CrTable In pp.CrTables
            pp.crtableLogoninfo = CrTable.LogOnInfo
            pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
            CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
        Next
        With pp
            .rp.SetParameterValue(0, SBill_Title_1)
            .rp.SetParameterValue(1, "*" & SBill_Title_1 & "*")
            .rp.SetParameterValue(2, " رقم / " + BillNumTxt.Text)
            .rp.SetParameterValue(3, "")
            .rp.SetParameterValue(4, " السعر :  " & PureTextBox.Text)
        End With

        'Dim p As New print
        'p.CrystalReportViewer1.ReportSource = pp.rp
        'p.Show()


        Dim prs As New PrinterSettings
        '---------------------------------------------
        Dim pageSettings As New PageSettings(prs)
        Dim paperSize As New PaperSize("Custom", 150, 100)
        pageSettings.PaperSize = paperSize
        'PrintDoc.PrinterSettings = prs
        'PrintDoc.DefaultPageSettings = pageSettings

        If PrinterState(Default_Barcode_Printer) Then
            'Dim prs As New PrinterSettings
            If Def_Befor_Print = 1 Then Set_Default_Printer(Default_Barcode_Printer) ' Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Barcode_Printer))
            prs.PrinterName = Default_Barcode_Printer
            prs.Copies = 1
            pp.rp.PrintToPrinter(prs, pageSettings, False)
            pp.rp.Clone()
            pp.rp.Dispose()
        Else
            MsgBox("الطابعة الافتراضية لورق الباركود" & vbNewLine & "***" & Default_Barcode_Printer & "***" & vbNewLine & " غير متصلة")
            Dim pd_PrintDialog As New PrintDialog
            pd_PrintDialog.ShowDialog()
            pp.rp.PrintToPrinter(pd_PrintDialog.PrinterSettings, pageSettings, False)
            pp.rp.Clone()
            pp.rp.Dispose()
        End If

    End Sub

    Public Sub Send_To_Bill_Screen()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Send_To_Bill_Screen"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
            .Parameters.AddWithValue("@CP_Name", My.Computer.Name)
        End With
        SQL_SP_EXEC(c.Com)
    End Sub

    Public Sub ConfermBill()

        'If MY_Settings.is_Use_Multi_Pay = True Then
        '    POS_Pied_Types.ShowDialog()
        'Else
        '    PIED_OK = True
        'End If

        'If PIED_OK = False Then Exit Sub

        Dim F As New Pay_Main_Form
        F.MONEY_VALUE = PURE
        F.Temp_Tr_ID = SB_TR_ID
        F.AG_ID = AG_ID
        F.ShowDialog()

        If F.is_OK = True Then
            Dim Tr_ID, Pay_ID As Integer
            Tr_ID = F.Tr_ID
            Pay_ID = F.Pay_ID


            Dim c As New C
            With c.Com
                .Connection = c.Con
                .CommandText = "SB_ConfermBill"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@T_ID", Me.T_ID)
                .Parameters.AddWithValue("@TOTAL", TOTAL)
                .Parameters.AddWithValue("@Discount", DISCOUNT)
                .Parameters.AddWithValue("@Pure", PURE)
                If AG_ID <> Default_AG_ID And isDierct_Reseve = False Then .Parameters.AddWithValue("@Pied", F_OrderOptions.Piedmoney_txt.Text)
                .Parameters.AddWithValue("@AGType_ID", AG_TypeID)
                .Parameters.AddWithValue("@Point_Inc", Point_Inc)
                .Parameters.AddWithValue("@Points_Sale", Points_Sale)
                If BillTypeCmb.SelectedValue = 3 Then
                    If F_OrderOptions.NoneDate_CB.Checked = False Then .Parameters.AddWithValue("@Deliver_date", F_OrderOptions.Delever_Date.Value)
                    .Parameters.AddWithValue("@Order_isDeleverd", 0)
                End If
                .Parameters.AddWithValue("@isCostmerScreen", isCostmerScreen)
                .Parameters.AddWithValue("@Tr_ID", Tr_ID)
                .Parameters.AddWithValue("@Pr_ID", Pr_ID)
                If TB_ID > 0 Then .Parameters.AddWithValue("@TB_ID", TB_ID)
                .Parameters.AddWithValue("@User_ID", USER_ID)
                .Parameters.AddWithValue("@Pay_ID", Pay_ID)
            End With
            If SQL_SP_EXEC(c.Com) Then
                If CP_Bill_Screen = True Then Send_To_Bill_Screen()
                Print_Bill()
                ResetNewBill()
            End If

        End If

    End Sub

    Private Sub PrintBillButton_Click(sender As Object, e As EventArgs) Handles PrintBillButton.Click
        Try
            PrintBillButton.Enabled = False

            If MetroGrid.Rows.Count > 0 Then
                Select Case BillTypeCmb.SelectedValue
                    Case 3
                        OrderCashPrint()
                        If KshPrint = True Then Order_SB_Ksh()
                    Case Else
                        CashPrint()
                End Select
            End If

            PrintBillButton.Enabled = True
        Catch ex As Exception
            PrintBillButton.Enabled = True
        End Try


    End Sub

    Public Sub Print_Bill()
        Select Case BillTypeCmb.SelectedValue
            Case 3
                If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
                If SB_AutoPrint = True Then OrderCashPrint()
                If KshPrint = True Then Order_SB_Ksh()
            Case Else
                If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
                If SB_AutoPrint = True Then CashPrint()
                If KshPrint = True Then CashPrint_SB_Ksh()
                If SB_is_Copy_Print = True Then CashPrint_Copy_Print()
        End Select
    End Sub

    Public Sub CashPrint_Copy_Print()
        Dim C As New C
        C.Con.Open()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\SB_Copy.rpt")
        pp.LoadTables()
        With pp
            If TB_ID > 0 Then
                .rp.SetParameterValue(0, TB_Name_Lb.Text)
            Else
                .rp.SetParameterValue(0, BillNumTxt.Text)
            End If

            .rp.SetParameterValue(1, Me.T_ID)
            .rp.SetParameterValue(2, Me.T_ID)
        End With

        If Def_Befor_Print = 1 Then Set_Default_Printer(Copy_Printer) 'Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", Copy_Printer))
        pp.rp.PrintOptions.PrinterName = Copy_Printer
        pp.rp.PrintToPrinter(1, False, 0, 0)
        pp.rp.Dispose()


        If MY_Settings.is_POS_Copy_2 = True Then
            If Def_Befor_Print = 1 Then Set_Default_Printer(POS_Copy_2_Path) 'Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", POS_Copy_2_Path))
            pp.rp.PrintOptions.PrinterName = POS_Copy_2_Path
            pp.rp.PrintToPrinter(1, False, 0, 0)
            ' pp.rp.Dispose()
        End If

        pp.rp.Dispose()


        'Dim p As New print
        'p.CrystalReportViewer1.ReportSource = pp.rp
        'p.Show()

        C.Con.Close()
    End Sub

    Public Sub Order_SB_Ksh()

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & OrderBill_IN_Bill_Track)
        pp.LoadTables()
        With pp

            .rp.SetParameterValue(0, Me.T_ID)
            .rp.SetParameterValue(1, "*" + Barcode + "*")
        End With


        If OrderPage_ID = 1 Then
            If Def_Befor_Print = 1 Then Set_Default_Printer(Default_Printer_80) 'Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", Default_Printer_80))
            pp.rp.PrintOptions.PrinterName = Default_Printer_80
        Else
            If Def_Befor_Print = 1 Then Set_Default_Printer(Default_Printer_A4) ' Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", Default_Printer_A4))
            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        End If
        pp.rp.PrintToPrinter(1, False, 0, 0)
        pp.rp.Dispose()


        'Dim p As New print
        'p.CrystalReportViewer1.ReportSource = pp.rp
        'p.Show()
    End Sub

    Public Sub CashPrint()

        If AGBillPage_ID <> 7 Then


            Dim pp As New ReportConnection
            pp.rp.Load(Application.StartupPath & AGBillPage_Bill_Track)
            pp.LoadTables()
            With pp
                Select Case AGBillPage_ID
                    Case 1, 5, 6
                        .rp.SetParameterValue(0, BillNumTxt.Text)
                        .rp.SetParameterValue(1, USER_NAME)
                        .rp.SetParameterValue(2, PureTextBox.Text)
                        .rp.SetParameterValue(3, BillTypeCmb.Text)
                        .rp.SetParameterValue(4, Me.T_ID)
                        .rp.SetParameterValue(5, SBill_Title_1)
                        .rp.SetParameterValue(6, SBill_Title_2)
                        .rp.SetParameterValue(7, SBill_Footer)
                        .rp.SetParameterValue(8, "*" + Barcode + "*")
                    Case 2
                        .rp.SetParameterValue(0, BillNumTxt.Text)
                        .rp.SetParameterValue(1, USER_NAME)
                        .rp.SetParameterValue(2, PureTextBox.Text)
                        .rp.SetParameterValue(3, BillTypeCmb.Text)
                        .rp.SetParameterValue(4, SBill_Title_1)
                    Case 3, 4
                        .rp.SetParameterValue(0, Me.T_ID)
                        .rp.SetParameterValue(1, SBill_Title_1)
                        .rp.SetParameterValue(2, SBill_Title_2)
                        .rp.SetParameterValue(3, SBill_Footer)
                        .rp.SetParameterValue(4, MetroGrid.Columns("QTY_CL").HeaderText)
                        .rp.SetParameterValue(5, "")
                        .rp.SetParameterValue(6, "*" + Barcode + "*")
                        .rp.SetParameterValue(7, SB_ID)
                        .rp.SetParameterValue(8, PURE - Pied)
                End Select

            End With




            If Def_Befor_Print = 1 Then Set_Default_Printer(Default_Printer_80) ' Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", Default_Printer_80))


            pp.rp.PrintOptions.PrinterName = Default_Printer_80
            pp.rp.PrintToPrinter(1, False, 0, 0)
            pp.rp.Dispose()

            'Dim p As New print
            'p.CrystalReportViewer1.ReportSource = pp.rp
            'p.Show()
        Else
            Fast_Report()
        End If

    End Sub

    Private Sub Fast_Report()
        'Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", Default_Printer_80))


        Set_Default_Printer(Default_Printer_80)


        query("UPDATE Agents_Balance_MV Set Prt_Counter  = Prt_Counter + 1 WHERE T_ID = " & T_ID)
        Dim RTP_C = GET_RTP_COUNT()

        Data_Load(IM_Dt, Bill_Date_Str, SB_ID, BillNumTxt.Text, BillTypeCmb.Text, "", Cr_Phone, RTP_C, TB_Name_Lb.Text, Barcode)

        PRINT_REPORT()

    End Sub

    Private Sub FAST_REPORT_SB_Ksh()
        'Dim C2 As New C
        'C2.Str = " Select IM_Name, QTY, Printer_ID  From GM_PRINTER_IM_V WHERE B_T_ID = " & T_ID
        ''C2.Com = New SqlClient.SqlCommand(C2.Str, C2.Con)
        'C2.Da = New SqlClient.SqlDataAdapter(C2.Str, C2.Con)
        'C2.Da.Fill(C2.Dt)

        '------------------------------------------------------------------------------------------------------------------
        'Dim watch = Stopwatch.StartNew()

        Dim C, C2 As New C
        Dim Tmp_Dt As New DataTable
        Dim ItemRow As DataRow

        With Tmp_Dt.Columns
            .Add("IM_Name", Type.GetType("System.String"))
            .Add("qty", Type.GetType("System.String"))
        End With

        Data_Load(Tmp_Dt, Bill_Date_Str, SB_ID, BillNumTxt.Text, BillTypeCmb.Text, Bill_Note, Cr_Phone, 0, TB_Name_Lb.Text, Barcode)

        '---------------------------------------------------------------------------------

        C.Str = " Select DISTINCT Printer_ID, Printer_Path  From GM_PRINTER_IM_V WHERE B_T_ID = " & T_ID
        C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
        C.Con.Open()

        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows = True Then
            While C.Dr.Read()
                Tmp_Dt.Clear()

                '-------------------------------------------------------------------
                C2.Str = " Select IM_Name, QTY  From GM_PRINTER_IM_V WHERE B_T_ID = " & T_ID & " And Printer_ID = " & C.Dr("Printer_ID")
                C2.Com = New SqlClient.SqlCommand(C2.Str, C2.Con)
                C2.Con.Open()
                C2.Dr = C2.Com.ExecuteReader
                If C2.Dr.HasRows = True Then
                    While C2.Dr.Read()

                        ItemRow = Tmp_Dt.NewRow()
                        ItemRow("IM_Name") = C2.Dr("IM_Name")
                        ItemRow("qty") = C2.Dr("QTY")
                        Tmp_Dt.Rows.Add(ItemRow)

                    End While
                End If
                C2.Con.Close()

                Set_Default_Printer(C.Dr("Printer_Path"))  'Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", C.Dr("Printer_Path")))

                PRINT_REPORT_KSH()
                    End While

                End If
                C.Con.Close()
                ' ----------------------------------------------------------------------------------

                'watch.Stop()
                'MsgBox(watch.Elapsed.ToString)

    End Sub



    Public Function GET_RTP_COUNT()
        Dim c As New C
        Dim N As Integer = 0
        Try
            Dim s As String
            s = "Select Prt_Counter FROM  Agents_Balance_MV WHERE T_ID = " & T_ID
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                N = c.Dr("Prt_Counter")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return N
    End Function

    Public Sub OrderCashPrint()

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & OrderBill_AG_Bill_Track)
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, BillNumTxt.Text)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, PureTextBox.Text)
            .rp.SetParameterValue(3, BillTypeCmb.Text)
            .rp.SetParameterValue(4, Me.T_ID)
            .rp.SetParameterValue(5, "*" + Barcode + "*")
            .rp.SetParameterValue(6, SBill_Title_1)
            .rp.SetParameterValue(7, SBill_Footer)
            .rp.SetParameterValue(8, Barcode)

            '.rp.SetParameterValue(9, "0")
            '.rp.SetParameterValue(10, "0")
        End With

        If OrderPage_ID = 1 Then
            If Def_Befor_Print = 1 Then Set_Default_Printer(Default_Printer_80) 'Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", Default_Printer_80))
            pp.rp.PrintOptions.PrinterName = Default_Printer_80
        Else
            If Def_Befor_Print = 1 Then Set_Default_Printer(Default_Printer_A4) 'Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", Default_Printer_A4))
            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        End If
        pp.rp.PrintToPrinter(1, False, 0, 0)
        pp.rp.Dispose()

        'Dim p As New print
        'p.CrystalReportViewer1.ReportSource = pp.rp
        'p.Show()
    End Sub






    Public Sub CashPrint_SB_Ksh()
        If AGBillPage_ID <> 7 Then



            Dim C As New C
            Dim Prt_Path As String
            Dim Prt_ID As Integer
            C.Con.Open()
            With C.Com
                .Connection = C.Con
                .CommandText = "SB_C_By_Printers_SELECT"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@B_T_ID", Me.T_ID)
            End With
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                While C.Dr.Read()
                    Prt_ID = C.Dr("Prt_ID")
                    Prt_Path = C.Dr("Prt_Path")
                    Dim pp As New ReportConnection
                    pp.rp.Load(Application.StartupPath & "\reports\SB_Ksh.rpt")
                    pp.LoadTables()
                    With pp
                        If TB_ID > 0 Then
                            .rp.SetParameterValue(0, TB_Name_Lb.Text)
                        Else
                            .rp.SetParameterValue(0, BillNumTxt.Text)
                        End If

                        .rp.SetParameterValue(1, Me.T_ID)
                        .rp.SetParameterValue(2, Me.T_ID)
                        .rp.SetParameterValue(3, Prt_ID)
                    End With

                    If Def_Befor_Print = 1 Then Set_Default_Printer(C.Dr("Prt_Path"))  'Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", C.Dr("Prt_Path")))

                    pp.rp.PrintOptions.PrinterName = C.Dr("Prt_Path")
                    pp.rp.PrintToPrinter(1, False, 0, 0)
                    pp.rp.Dispose()

                    'Dim p As New print
                    'p.CrystalReportViewer1.ReportSource = pp.rp
                    'p.Show()

                End While
            End If
            C.Con.Close()


        Else
            FAST_REPORT_SB_Ksh()
        End If
    End Sub


    Private Sub SMButtonUP_Click(sender As Object, e As EventArgs) Handles SMButtonUP.Click
        Dim newRight As Integer = -SMPanel.AutoScrollPosition.X
        Dim newDown As Integer = -SMPanel.AutoScrollPosition.Y
        newDown = -SMPanel.AutoScrollPosition.Y - 525
        SMPanel.AutoScrollPosition = New Point(newRight, newDown)
    End Sub


    Private Sub SMButtonDown_Click(sender As Object, e As EventArgs) Handles SMButtonDown.Click
        Dim newRight As Integer = -SMPanel.AutoScrollPosition.X
        Dim newTop As Integer = -SMPanel.AutoScrollPosition.Y
        newTop = -SMPanel.AutoScrollPosition.Y + 525
        SMPanel.AutoScrollPosition = New Point(newRight, newTop)
    End Sub

    Private Sub SB_Update_Bill_Type()
        On Error Resume Next
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Update_Bill_Type"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
            .Parameters.AddWithValue("@Type_ID", Me.BillTypeCmb.SelectedValue)
            Prev_Type = BillTypeCmb.SelectedValue
        End With
        SQL_SP_EXEC(c.Com)
    End Sub

    Private Sub NumTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NumTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub BillsMenu_Btn_Click(sender As Object, e As EventArgs) Handles BillsMenu_Btn.Click
        F_SalesMenu = New SalesTypes
        F_SalesMenu.TabNum = 3
        F_SalesMenu.ShowDialog()
    End Sub

    Private Sub Compont_Btn_Click(sender As Object, e As EventArgs) Handles Compont_Btn.Click
        If MetroGrid.Rows.Count > 0 Then ComponentsManager.ShowDialog()
    End Sub

    Private Sub ChangePriceButton_Click(sender As Object, e As EventArgs) Handles ChangePriceButton.Click
        If MetroGrid.Rows.Count > 0 Then IM_Check_PriceChanging()
    End Sub

    Private Sub IM_Check_PriceChanging()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_PriceChanging"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_T_ID", Me.MetroGrid.CurrentRow.Cells("IM_T_ID").Value)
            .Parameters.AddWithValue("@isOk", 0)
            .Parameters("@isOk").Direction = ParameterDirection.Output
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            If C.Com.Parameters("@isOk").Value = 1 Then
                isPriceCH = True
                F_SBChangePrice = New SBChangePrice
                Set_Form(F_SBChangePrice, IMPanel)
            Else
                '    MsgBox("لا يمكن التعديل في سعر القطعة لهذ الصنف", MsgBoxStyle.Exclamation)
                Beep()
                Dim MSG As New OK
                MSG.Msg_Lb.Text = "لا يمكن التعديل في سعر القطعة لهذ الصنف"
                MSG.ShowDialog()
            End If
        End If
    End Sub


    Private Sub ChangeAGButton_Click(sender As Object, e As EventArgs) Handles ChangeAGButton.Click
        F_AgentsMenu = New AgentsMenu
        ' Set_Form(F_AgentsMenu, IMPanel)
        F_AgentsMenu.ShowDialog()
        F_AgentsMenu.Barcode_SH_txt.Select()
    End Sub

    Private Sub ChangeTbButton_Click(sender As Object, e As EventArgs) Handles ChangeTbButton.Click
        F_SB_TablesMenu = New SB_TablesMenu
        F_SB_TablesMenu.ShowDialog()
    End Sub

    Private Sub VoidBillBtn_Click(sender As Object, e As EventArgs) Handles VoidBillBtn.Click
        Beep()
        Dim MSG As New YesNo
        MSG.Msg_Lb.Text = " إلغــاء الفاتورة " + BillNumTxt.Text + " بشكل نهــائي  "
        MSG.ShowDialog()
        If MSG.Result = True Then SB_VoidBill()
    End Sub

    Private Sub SB_VoidBill()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Void_Row"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert("إلغاء الفاتورة", SB_ID_Txt.Text, 1, 3)
            If SB_AutoPrint = True Then SB_Report_Cacnel()
            ResetNewBill()
        End If
    End Sub

    Public Sub SB_Report_Cacnel()

        If AGBillPage_ID <> 7 Then


            Dim C As New C
            Dim Prt_Path As String
            Dim Prt_ID As Integer
            C.Con.Open()
            With C.Com
                .Connection = C.Con
                .CommandText = "SB_C_By_Printers_SELECT"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@B_T_ID", Me.T_ID)
            End With
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                While C.Dr.Read()
                    Prt_ID = C.Dr("Prt_ID")
                    Prt_Path = C.Dr("Prt_Path")
                    Dim pp As New ReportConnection
                    pp.rp.Load(Application.StartupPath & "\reports\SB_Cancel.rpt")
                    pp.LoadTables()
                    With pp
                        .rp.SetParameterValue(0, BillNumTxt.Text)
                    End With

                    If Def_Befor_Print = 1 Then Set_Default_Printer(C.Dr("Prt_Path").ToString) 'Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", C.Dr("Prt_Path").ToString))
                    pp.rp.PrintOptions.PrinterName = C.Dr("Prt_Path").ToString
                    pp.rp.PrintToPrinter(1, False, 0, 0)
                    pp.rp.Dispose()
                End While
            End If
            C.Con.Close()


        Else
            FAST_Report_Cancel_KSH()
        End If

    End Sub


    Private Sub FAST_Report_Cancel_KSH()
        Dim C As New C
        C.Str = " Select DISTINCT Printer_ID, Printer_Path  From GM_PRINTER_IM_V WHERE B_T_ID = " & T_ID
        C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
        C.Con.Open()

        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows = True Then
            While C.Dr.Read()

                'Shell(String.Format("rundll32 printui.dll, PrintUIEntry / y / n ""{0}""", C.Dr("Printer_Path")))
                Set_Default_Printer(C.Dr("Printer_Path"))
                Cancel_REPORT_KSH(" إلغــاء الطلب " & vbNewLine & BillNumTxt.Text)
            End While

        End If
        C.Con.Close()
    End Sub

    Private Sub DiscountBillBtn_Click(sender As Object, e As EventArgs) Handles DiscountBillBtn.Click
        F_SBChangePrice = New SBChangePrice
        Set_Form(F_SBChangePrice, IMPanel)
    End Sub

    Private Sub SBPauseBtn_Click(sender As Object, e As EventArgs) Handles SBPauseBtn.Click
        Beep()
        '   If MessageBox.Show(" تعليق الفاتورة " + BillNumTxt.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) _
        ' = Windows.Forms.DialogResult.OK Then
        Dim MSG As New YesNo
        MSG.Msg_Lb.Text = " تعليق الفاتورة " + BillNumTxt.Text
        MSG.ShowDialog()
        If MSG.Result = True Then SB_PauseBill()
        '   End If
    End Sub

    Private Sub SB_PauseBill()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_PauseBill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then ResetNewBill()
    End Sub

    Private Sub MoveToBill_Btn_Click(sender As Object, e As EventArgs) Handles MoveToBill_Btn.Click
        If PauseCmb.SelectedIndex > -1 Then
            Dim TmpT_ID As Integer = PauseCmb.SelectedValue
            Dim BillNum As Integer = PauseCmb.Text
            Reset_Fields()
            isNewBill = 0
            Me.T_ID = TmpT_ID
            BillNumTxt.Text = BillNum
            Fill_Bill_Info()
            SB_Contents_SELECT_Bill()
        End If
    End Sub

    Private Sub UnLockButton_Click(sender As Object, e As EventArgs) Handles UnLockButton.Click
        If FormActive = True Then
            Beep()
            '  If MessageBox.Show("تأكيد قفل الواجهة ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = MsgBoxResult.Ok Then
            Dim MSG1 As New YesNo
            MSG1.Msg_Lb.Text = "تأكيد قفل الواجهة ؟"
            MSG1.ShowDialog()
            If MSG1.Result = True Then
                UnLockButton.BackgroundImage = My.Resources.ResourceManager.GetObject("Lock")
                Me.Cursor = Cursors.AppStarting
                FormActive = False
                Me.Cursor = Cursors.Default
                Saleslogin.ShowDialog()
            End If
        End If
    End Sub


    'Private Sub BillTypeCmb_SelectedValueChanged(sender As Object, e As EventArgs) Handles BillTypeCmb.SelectedValueChanged


    '    If is_Form_Start = True Then

    '        If BillTypeCmb.Items.Count > 0 And Me.T_ID > 0 Then

    '            If is_Show_Bill = False Then
    '                If (Prev_Type = 3 Or BillTypeCmb.SelectedValue = 3) And (MetroGrid.Rows.Count > 0) Then
    '                    Dim MSG As New OK
    '                    MSG.Msg_Lb.Text = " لتحويل الفاتورة إلى طلبية أو من طلبية يجب عليك حذف كل الأصناف الموجودة بها أولا "
    '                    MSG.ShowDialog()
    '                    BillTypeCmb.SelectedValue = Prev_Type
    '                    Exit Sub
    '                End If
    '            End If

    '            SB_Update_Bill_Type()
    '            Manage_Reseve()
    '        End If

    '    End If

    'End Sub

    Private Sub BillTypeCmb_Click(sender As Object, e As EventArgs) Handles BillTypeCmb.Click
        '  If MetroGrid.Rows.Count > 0 Then
        ' IMPanel.Controls.Clear()
        Dim F_ChangeBill_Type = New ChangeBill_Type
        Set_Form(F_ChangeBill_Type, IMPanel)
        'End If
    End Sub
    '----------------------------------------------------------------------------------------------------------------------

    Public Sub Manage_Reseve()
        If TB_ID > 0 Then
            isDierct_Reseve = False
        Else
            Select Case BillTypeCmb.SelectedValue
                Case 3
                    isDierct_Reseve = False
                    SaveButton.Text = "إنتقال إلى التسليم"
                Case Else
                    isDierct_Reseve = True
                    SaveButton.Text = "إستلام مبلغ F1"
            End Select
        End If
    End Sub

    Public Sub Check_Table()
        If TB_ID > 0 Then

            If Switch_To_Cash = False Then
                isDierct_Reseve = False
                SaveButton.Text = "إرسال الطلب"
            Else
                isDierct_Reseve = True
                SaveButton.Text = "إستلام مبلغ F1"
            End If
        Else
            Manage_Reseve()
        End If
    End Sub

    Private Sub BillNotesButton_Click(sender As Object, e As EventArgs) Handles BillNotesButton.Click
        F_BillNotes = New BillNotes
        F_BillNotes.T_ID = T_ID
        '  Set_Form(F_BillNotes, IMPanel)
        F_BillNotes.ShowDialog()
    End Sub


    Private Sub Units_btn_Click(sender As Object, e As EventArgs) Handles Units_btn.Click
        If MetroGrid.Rows.Count > 0 Then
            IMPanel.Controls.Clear()
            F_ChangeUnit = New ChangeUnit
            Set_Form(F_ChangeUnit, IMPanel)
        End If
    End Sub

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_txt.KeyDown

        Select Case e.KeyCode
            Case Keys.Return
                If String.IsNullOrWhiteSpace(Barcode_txt.Text) = False Then
                    Dim Tmp_Bar As String = ""
                    If Barcode_txt.Text(0) = "*" Then
                        Tmp_Bar = Barcode_txt.Text.Replace("*", "")
                        'Load_Transform_Bill(Tmp_Bar)
                        Transform_Bill_Pros(Tmp_Bar)
                    Else
                        Load_IM_Barcode()
                    End If
                End If
            Case Keys.Delete
                Barcode_txt.Clear()
        End Select

    End Sub


    Private Sub Transform_Bill_Pros(Tmp_Bar As String)
        Dim C As New C
        IM_Dt.Clear()
        With (C.Com)
            .Connection = C.Con
            .CommandText = "[Transform_Bill_Pros]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
            .Parameters.AddWithValue("@TMP_BARCODE", Tmp_Bar)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        C.Da.Fill(IM_Dt)
        MetroGrid.DataSource = C.Dt
        If MetroGrid.Rows.Count > 0 Then MetroGrid.CurrentCell = MetroGrid.Rows(MetroGrid.Rows.Count - 1).Cells("IM_NameCL")
        CLEAR_BARCODE_Fildes()
        ''-------------------------------------------------------------------

    End Sub

    'Public Sub Load_Transform_Bill(Tmp_Bar As String)
    '    Dim c As New C
    '    Dim OLD_T_ID As Integer
    '    Try
    '        Dim s As String

    '        s = "select T_ID from SB_Info_V WHERE Barcode = '" & Tmp_Bar & "'"

    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            OLD_T_ID = c.Dr("T_ID")
    '            CLEAR_BARCODE_Fildes()
    '            TB_Contents_Transfer_From_Bills(OLD_T_ID)
    '        End If
    '        Barcode_txt.Clear()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    'Private Sub TB_Contents_Transfer_From_Bills(OLD_T_ID As Integer)
    '    Dim c As New C
    '    With c.Com
    '        .Connection = c.Con
    '        .CommandText = "[TB_Contents_Transfer_From_Bills]"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", Me.T_ID)
    '        .Parameters.AddWithValue("@T_ID_OLD", OLD_T_ID)
    '    End With
    '    If SQL_SP_EXEC(c.Com) = True Then SB_Contents_SELECT_Bill()
    'End Sub


    Public Sub Load_IM_Barcode()
        Dim c As New C
        IM_SH_Dt.Clear()
        Try
            Dim s As String

            s = "select U_IM_ID,IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_txt.Text & "'"

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_ID = c.Dr("IM_ID")
                is_Valid = c.Dr("isValid")
                U_IM_ID = c.Dr("U_IM_ID")
                Barcode_IM = Barcode_txt.Text
                If SB_IM_Alert_When_Repetition = True Then
                    For i = 0 To MetroGrid.Rows.Count - 1
                        If MetroGrid.Rows(i).Cells("IM_ID_CL").Value = IM_ID Then
                            My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                            Dim MSG1 As New YesNo
                            MSG1.Msg_Lb.Text = "هذا الصنف تم إدراجه بالفاتورة ... هل تريد الإستمرار ؟"
                            MSG1.ShowDialog()
                            If MSG1.Result = True Then
                                Fetch_ItemToList(IM_ID)
                                Exit Sub
                            Else
                                CLEAR_BARCODE_Fildes()
                                Exit Sub
                            End If
                        End If
                    Next
                    CLEAR_BARCODE_Fildes()
                    Barcode_txt.Clear()
                End If

                Fetch_ItemToList(IM_ID)
                Barcode_txt.Clear()
            Else
                If Barcode_txt.Text.Count >= 8 Then
                    Check_If_Mizan()
                Else
                    My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                    Dim MSG2 As New OK
                    MSG2.Msg_Lb.Text = "!! لم يتم التعرف على الباركود"
                    MSG2.ShowDialog()
                    CLEAR_BARCODE_Fildes()

                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Check_If_Mizan()
        Dim c As New C

        Dim New_Barcode As String = ""
        Dim Qty As String = ""

        Dim Qty_Dot As String = ""
        Dim T_Price As Double = 0
        Dim T_Price_Dot As String = ""
        Dim default_price As Double = 0

        For i = Mizan_BarcodeFrom - 1 To Mizan_BarcodeTo - 1
            New_Barcode += Barcode_txt.Text(i)
        Next

        Dim S As String = "Select U_IM_ID,IM_ID,item_name,isValid,price from IM_units_Search_V WHERE Barcode = '" & New_Barcode & "'"
        c.Com = New SqlClient.SqlCommand(S, c.Con)
        c.Con.Open()

        c.Dr = c.Com.ExecuteReader
        If c.Dr.HasRows Then
            c.Dr.Read()
            IM_Name = c.Dr("item_name")
            Barcode_IM = New_Barcode
            '---------------------------------------------------------------------------------------------------------------------------

            Try
                If Second_Part_isPrice = 0 Then
                    For i = Mizan_QtyFrom - 1 To Mizan_QtyTo - 1
                        Qty += Barcode_txt.Text(i)
                    Next
                    NumTextBox.Text = Convert.ToDouble(Qty) / 1000
                Else
                    Dim j = 0
                    For i = Mizan_QtyFrom - 1 To Mizan_QtyTo - 1
                        j += 1
                        T_Price_Dot += Barcode_txt.Text(i)
                        If j = 3 Then
                            T_Price_Dot = T_Price_Dot & "."
                        End If
                    Next
                    T_Price = T_Price_Dot

                    IM_Price = c.Dr("price")

                    NumTextBox.Text = T_Price / IM_Price
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
                CLEAR_BARCODE_Fildes()
                Exit Sub
            End Try


            '--------------------------------------------------------------------------------------------------------------------------
            IM_ID = c.Dr("IM_ID")
            is_Valid = c.Dr("isValid")
            U_IM_ID = c.Dr("U_IM_ID")

            For i = 0 To MetroGrid.Rows.Count - 1
                If MetroGrid.Rows(i).Cells("IM_ID_CL").Value = IM_ID Then
                    My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")

                    '---------------
                    Dim MSG1 As New YesNo
                    MSG1.Msg_Lb.Text = "هذا الصنف تم إدراجه بالفاتورة ... هل تريد الإستمرار ؟"
                    MSG1.ShowDialog()
                    If MSG1.Result = True Then
                        Fetch_ItemToList(IM_ID)
                        CLEAR_BARCODE_Fildes()
                        Exit Sub
                    Else
                        CLEAR_BARCODE_Fildes()
                        Exit Sub
                    End If
                    '---------------
                End If
            Next
            Fetch_ItemToList(IM_ID)
            CLEAR_BARCODE_Fildes()
        Else
            My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
            Dim MSG2 As New OK
            MSG2.Msg_Lb.Text = "!! لم يتم التعرف على الباركود"
            MSG2.ShowDialog()

            CLEAR_BARCODE_Fildes()
        End If
        c.Con.Close()
    End Sub

    Private Sub CLEAR_BARCODE_Fildes()
        'Barcode = ""
        Barcode_IM = ""
        Barcode_txt.Clear()
        IM_Price = 0
        IM_Name = ""
        NumTextBox.Clear()
    End Sub
    Public Sub Get_T_ID()
        Dim C As New C
        Dim S As String
        If is_Up = True Then
            S = "SELECT TOP 1 T_ID,S_Bill_Pr_ID from Agents_Balance_MV where Pr_ID = '" & Pr_ID & "' AND T_ID > '" & T_ID & "' ORDER BY T_ID ASC"
        Else
            S = "SELECT TOP 1 T_ID,S_Bill_Pr_ID from Agents_Balance_MV where Pr_ID = '" & Pr_ID & "' AND T_ID < '" & T_ID & "' ORDER BY T_ID DESC"
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

                MOVE_TO_SELECT()
            Else
                '  MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                Dim MSG As New OK
                MSG.Msg_Lb.Text = "!! لم يتم التعرف على الفاتورة"
                MSG.ShowDialog()
                BillNumTxt.Text = Tmp_Bill_ID
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Public Sub MOVE_TO_SELECT()
        SB_Contents_SELECT_Bill()
        F_POS.Fill_Bill_Info()
    End Sub

    Dim is_Up As Boolean = False
    Dim Tmp_Bill_ID As Integer
    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        Tmp_Bill_ID = BillNumTxt.Text
        is_Up = True
        Get_T_ID()
    End Sub

    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = BillNumTxt.Text
        is_Up = False
        Get_T_ID()
    End Sub

    Private Sub IMPanel_EnabledChanged(sender As Object, e As EventArgs) Handles IMPanel.EnabledChanged
        If IMPanel.Enabled = True Then
            Barcode_txt.Enabled = True
        Else
            Barcode_txt.Enabled = False
        End If
    End Sub



    Private Sub POS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress


        Select Case e.KeyChar
            Case "+", "-"
                e.Handled = True
                Exit Sub
        End Select


        If Me.Barcode_txt.Focused = False Then
            Barcode_txt.Focus()
            If Me.Barcode_txt.Enabled = True Then
                Barcode_txt.Text = e.KeyChar.ToString
                Barcode_txt.SelectionStart = Barcode_txt.Text.Length
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Clear_Barcode_btn_Click(sender As Object, e As EventArgs) Handles Clear_Barcode_btn.Click
        Barcode_txt.Clear()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub SearchBy_Barcode_btn_Click(sender As Object, e As EventArgs) Handles SearchBy_Barcode_btn.Click
        POS_Search = True
        SearchByBarcode.ShowDialog()
        POS_Search = False
    End Sub

    Private Sub OpenCahDR_Btn_Click(sender As Object, e As EventArgs) Handles OpenCahDR_Btn.Click
        Open_Cash_Drawer()
    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If On_Update = False Then

            Beep()
            '  If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟ ", "تعديل فاتورة", MessageBoxButtons.YesNo, _
            '    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then

            Dim MSG As New YesNo
            MSG.Msg_Lb.Text = "سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟"
            MSG.ShowDialog()
            If MSG.Result = True Then

                Edit_butt.BackColor = Color.GreenYellow
                On_Update = True
                MetroGrid.BackgroundColor = Color.LightYellow
                MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                Barcode_txt.Enabled = True
                IMPanel.Enabled = True
                BillNotesButton.Enabled = True
                DiscountBillBtn.Enabled = True
                IM_Option_Panel.Enabled = True
                ChangeAGButton.Enabled = True
                'IMDicreaseButton.Enabled = False
                'IMIncreaseButton.Enabled = False
                'ChangePriceButton.Enabled = False
                Units_btn.Enabled = True


                Network_Edit_Tracker_insert("تعديل فاتورة مبيعات / رقم آلي : " + SB_ID.ToString + " / رقم يومي :  " + BillNumTxt.Text + " / المدخل :  " + USER_NAME, PureTextBox.Text, 0, 0)
            End If

        Else
            On_Update = False
            Edit_butt.BackColor = Color.WhiteSmoke
            Fill_Bill_Info()
            Save_Total(T_ID, TOTAL, DISCOUNT)
            'SB_Contents_Structers_insert()
            'MetroGrid.BackgroundColor = Color.LightGoldenrodYellow
            'MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGoldenrodYellow
            'Barcode_txt.Enabled = False
            'IMPanel.Enabled = False
            'IM_Option_Panel.Enabled = False
        End If
    End Sub

    Private Sub IM_Search_btn_Click(sender As Object, e As EventArgs) Handles IM_Search_btn.Click
        IM_Keyboard.ShowDialog()
    End Sub


    Private Sub TB_Name_Lb_MouseClick(sender As Object, e As MouseEventArgs) Handles TB_Name_Lb.MouseClick
        If TB_ID > 0 Then Show_Table_IM.ShowDialog()
    End Sub


    Private Sub Finish_Order_Btn_Click(sender As Object, e As EventArgs) Handles Finish_Order_Btn.Click

        If MetroGrid.Rows.Count > 0 Then
            Try
                Finish_Order_Btn.Enabled = False
                AG_Balance_Update_Barcode_Ordered()
                Finish_Order_Btn.Enabled = True
            Catch ex As Exception
                Finish_Order_Btn.Enabled = True
            End Try
        End If

    End Sub

    Private Sub Show_Open_Bill_btn_Click(sender As Object, e As EventArgs) Handles Show_Open_Bill_btn.Click
        POS_Abcent_Bills.ShowDialog()
        RefreashButton_Click(sender, e)
    End Sub

    'Private Sub VoidLb_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles VoidLb.MouseDoubleClick

    '    If U_SalesVoid = True Then

    '        Beep()
    '        If MessageBox.Show(" سيتم تراجع عن إلغاء الفاتورة رقم " + SB_ID.ToString + " وكل المعاملات الخاصة بها ... متأكد ", "تاكيد العملية", MessageBoxButtons.OKCancel,
    '               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
    '            AG_Balance_UN_Void_Row(T_ID, SB_ID, 1)
    '            Get_T_ID()
    '        End If

    '    End If
    'End Sub

    'Private Sub SALES_TYPES_CMB_SelectedValueChanged(sender As Object, e As EventArgs) Handles SALES_TYPES_CMB.SelectedValueChanged

    '    'If TypeName(SALES_TYPES_CMB.SelectedValue) = "Integer" Then

    '    'End If

    'End Sub

    Private Sub PureTextBox_TextChanged(sender As Object, e As EventArgs) Handles PureTextBox.TextChanged
        If is_Use_Total_Port = True Then Show_Total_Port(PureTextBox.Text)
    End Sub


    Private Sub MetroGrid_DataSourceChanged(sender As Object, e As EventArgs) Handles MetroGrid.DataSourceChanged
        Calc_Bill(False)
    End Sub

    Private Sub Show_Cash_btn_Click(sender As Object, e As EventArgs) Handles Show_Cash_btn.Click
        Fetch_Pr_Details_()
    End Sub


    Private Sub IM_Search_LB_Click(sender As Object, e As EventArgs) Handles IM_Search_LB.Click
        IM_Keyboard.ShowDialog()
    End Sub

    Private Sub Show_Open_Bill_LB_Click(sender As Object, e As EventArgs) Handles Show_Open_Bill_LB.Click
        POS_Abcent_Bills.ShowDialog()
        RefreashButton_Click(sender, e)
    End Sub

    Private Sub Show_Cash_LB_Click(sender As Object, e As EventArgs) Handles Show_Cash_LB.Click
        Fetch_Pr_Details_()
    End Sub

    Private Sub حجـــزالخدمـــةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles حجـــزالخدمـــةToolStripMenuItem.Click
        Prepare_For_Rsv()
    End Sub

    Private Sub Prepare_For_Rsv()
        If MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And MetroGrid.Rows.Count > 0 Then
            'Dim F As New Rsv_IM_2
            'F.IM_T_ID = F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value
            'F.IM_NAME = F_POS.MetroGrid.CurrentRow.Cells("IM_NameCL").Value

            'F.ShowDialog()

            CalendarForm.IM_T_ID = F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value
            CalendarForm.IM_NAME = F_POS.MetroGrid.CurrentRow.Cells("IM_NameCL").Value
            CalendarForm.IM_ID = F_POS.MetroGrid.CurrentRow.Cells("IM_ID_CL").Value

            If Not String.IsNullOrWhiteSpace(Cr_Phone) Then CalendarForm.Rsv_Info &= Cr_Phone
            If Not String.IsNullOrWhiteSpace(AG_Name) Then CalendarForm.Rsv_Info &= " \ " & AG_Name

            'CalendarForm.Cr_Phone = Cr_Phone
            'CalendarForm.AG_NAME = AG_Name

            CalendarForm.ShowDialog()
        End If
    End Sub

    Private Sub إدراجموظفللخدمةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إدراجموظفللخدمةToolStripMenuItem.Click
        If MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And MetroGrid.Rows.Count > 0 Then
            Dim F As New SB_Contents_AGENTS

            F.SB_T_ID = F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value
            F.IM_NAME = F_POS.MetroGrid.CurrentRow.Cells("IM_NameCL").Value

            F.ShowDialog()
        End If
    End Sub

    Private Sub سدادرقمطاولةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles سدادرقمطاولةToolStripMenuItem.Click
        Dim inp = InputBox("ادخل الرقم ", "سداد رقم طاولة")
        If inp <> "" Then
            F_TablesBalance = New TablesBalance

            F_TablesBalance.FormBorderStyle = FormBorderStyle.Fixed3D
            'frm.TopLevel = False
            F_TablesBalance.TB_ID = inp
            F_TablesBalance.IS_SHOW_NUMBER = True
            F_TablesBalance.TB_Num = inp
            'frm.TopLevel = False
            'frm.FormBorderStyle = FormBorderStyle.None
            'frm.Dock = DockStyle.Fill

            'IMPanel.Controls.Clear()
            'IMPanel.Controls.Add(frm)
            'F_TablesBalance.ToggleRightPanel()

            F_TablesBalance.PanelRight.Visible = False
            F_TablesBalance.Show()

        End If
    End Sub


    'Private Sub payment_Type_combo_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If TypeName(payment_Type_combo.SelectedValue) = "Integer" Then
    '        PaymentMethodDefaultAccounts_SELECT()
    '    End If
    'End Sub


End Class