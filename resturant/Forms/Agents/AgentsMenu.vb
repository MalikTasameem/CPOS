Imports System.IO

Public Class AgentsMenu
    Dim rs As New Resizer
    Public AG_ID As Integer = 0
    Public AG_NAME As Integer = 0
    'Dim IM_Dt As New DataTable

    Private Sub AgentsMenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub AgentsMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        'rs.FindAllControls(Me)
        If U_Balance = False Then AG_Account_btn.Visible = False
        Fetch_Agents_Type()

        If MY_Settings.is_ByBarInput = True Then
            Barcode_SH_txt.Select()
        Else
            'IM_SH_txt.Select()
            AG_Cm.Focus()
        End If

        load_Fast_ag()
    End Sub

    Public Sub load_Fast_ag()
        Agents_Panel.Controls.Clear()

        Dim c As New C
        Dim x = 0
        Dim y = 0
        Dim counter = 1
        ' Dim FontSize, StandarMeasure As Double
        Dim b As Integer = 0
        Dim s As String = ""
        Dim IMName As String = ""

        s = "select AG_ID,AG_NAME from Agents WHERE isDefaultAG = 1 ORDER BY AG_ID ASC "


        c.Com = New SqlClient.SqlCommand(s, c.Con)

        c.Con.Open()
        c.Dr = c.Com.ExecuteReader
        If c.Dr.HasRows Then
            While c.Dr.Read()
                Dim IMbtn As New Button
                IMbtn.Name = ("AG_NAME" + c.Dr("AG_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(Agents_Panel.Size.Width / 3.2, Agents_Panel.Size.Height / 5.25)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMName = c.Dr("AG_NAME")
                IMbtn.Font = New System.Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                IMbtn.Text = c.Dr("AG_NAME")
                IMbtn.Tag = c.Dr("AG_ID")


                Controls.Add(IMbtn)
                IMbtn.Parent = Agents_Panel
                AddHandler IMbtn.Click, AddressOf bt_Click

                If counter = 3 Then
                    counter = 1
                    x = 0
                    y += Agents_Panel.Size.Height / 5.25
                Else
                    counter += 1
                    x += Agents_Panel.Size.Width / 3.2
                End If
                rs.Find_One(IMbtn)

            End While
        End If

        c.Con.Close()

    End Sub

    Public Sub bt_Click(ByVal sender As Object, ByVal e As EventArgs)
        AG_Cm.Set_IM_By_ID(sender.tag)
        Select_AG_btn_Click(sender, e)

    End Sub

    Private Sub Fetch_Agents_Type()
        Dim C As New C
        Try
            Dim sql As String = "Select id,Type_Name from Agents_Types Order By Rank_Num ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            AG_Type_cm.DataSource = C.Dt
            AG_Type_cm.DisplayMember = "Type_Name"
            AG_Type_cm.ValueMember = "id"
            AG_Type_cm.SelectedValue = 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AgentsMenu_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'rs.ResizeAllControls(Me)
    End Sub

    Private Sub New_AG_Btn_Click(sender As Object, e As EventArgs) Handles New_AG_Btn.Click
        F_AddFastAgent = New AddFastAgent
        F_AddFastAgent.ShowDialog()
    End Sub

    Private Sub Select_AG_btn_Click(sender As Object, e As EventArgs) Handles Select_AG_btn.Click
        AG_ID = AG_Cm.TXT_ID.Text
        AG_NAME = AG_Cm.Textt

        If U_AG_Skip_Max = False Then
            If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then
                MsgBox("عذرا ... هذا الزبون قد تخطى سقف الدين الخاص به ولا يمكنك فتح فاتورة جديدة له", MsgBoxStyle.Critical, "خطأ فالإدراج")
            Else
                Choase_Ag()
            End If
        Else
            If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then MsgBox("هذا الزبون قد تخطى سقف الدين الخاص به", MsgBoxStyle.Exclamation, "تنويه ")
            Choase_Ag()
        End If



    End Sub
    Private Sub Choase_Ag()


        If Application.OpenForms().OfType(Of Sales).Any Then
            Switch_Sales_Agent(F_Sales.T_ID, AG_ID, F_Sales.On_Update)
            F_Sales.AG_Label.Text = "رصيد الحســاب: ( " & F_Sales.GET_AG_Balance().ToString & " ) "
            Exit Sub
        End If


        If SB_is_Fast = True Then
            Switch_Fast_Sales_Agent(Sales_Fast.T_ID, AG_ID, Sales_Fast.On_Update)
            Exit Sub
        End If

        If Home_Panel = "POS" Or TB_Part_Pied = True Then

            If AG_ID > 0 Then AG_Balance_Update_AG(AG_ID)

        Else
            If AG_ID > 0 Then TB_ButOnAG(AG_ID)
            End If

    End Sub

    Private Sub Switch_Sales_Agent(T_ID As Integer, AG_ID As Integer, On_Update As Boolean)
        Save_AG_Name(T_ID, AG_ID, On_Update)
        Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_Cm.Textt, F_Sales.SB_ID, 1, 3)
        'If AG_ID <> Default_AG_ID Then
        '    F_Sales.AG_SH_txt.Text = AG_Cm.Textt  'IM_SH_txt.Text
        'End If
        'F_Sales.AG_Grid.Visible = False
        'F_Sales.AG_ID = AG_ID
        'F_Sales.AG_SH_txt.BackColor = Color.LightGoldenrodYellow
        'F_Sales.Fill_All_AG_Proj()
        Me.Close()
    End Sub

    Private Sub Switch_Fast_Sales_Agent(T_ID As Integer, AG_ID As Integer, On_Update As Boolean)
        Save_AG_Name(T_ID, AG_ID, On_Update)
        Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_Cm.Textt, Sales_Fast.SB_ID, 1, 3)
        If AG_ID <> Default_AG_ID Then
            Sales_Fast.AG_SH_txt.Text = AG_Cm.Textt   'IM_SH_txt.Text
        Else
            Sales_Fast.AG_SH_txt.Text = "نقدي"
        End If

        Sales_Fast.AG_ID = AG_ID
        Me.Close()
    End Sub

    Public Sub TB_ButOnAG(AG_ID As Integer)

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "TB_ButOnAG"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@TB_ID", F_TablesBalance.TB_Num)
            .Parameters.AddWithValue("@AG_ID", AG_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            F_TablesBalance.TB_NotPied_V_SELECT_Bill(F_TablesBalance.TB_Num)
            Me.Close()
        End If
    End Sub

    Public Sub AG_Balance_Update_AG(AG_ID As Integer)

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Update_AG"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", F_POS.T_ID)
            .Parameters.AddWithValue("@AG_ID", AG_ID)
            .Parameters.AddWithValue("@ON_UPDATE", F_POS.On_Update)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_Cm.Textt, F_POS.SB_ID_Txt.Text, 1, 3)

            If AG_Type_cm.SelectedValue = 3 Then Make_StdPercentDis()

            If AG_Type_cm.SelectedValue = 5 Then Make_PrevDisPercent()

            If Home_Panel = "POS" Then
                F_POS.Fill_Bill_Info()
                If F_POS.AG_TypeID = 4 Then F_POS.Point = Current_QTY.Text
            End If

            If TB_Part_Pied = True Then
                TablePiedApart.AG_ID = AG_ID
                TablePiedApart.Fill_Bill_Info()
            End If
            Me.Close()
        End If
    End Sub

    Private Sub Make_StdPercentDis()
        If String.IsNullOrWhiteSpace(F_POS.TotalTextBox.Text) = False Then
            Dim Bill_Total As Double = F_POS.TotalTextBox.Text
            Dim Discount_Val As Double = (StdDisPercent / 100) * Bill_Total
            F_POS.DiscountTextBox.Text = Discount_Val
            F_POS.Calc_Bill(False)
            F_POS.Make_Discount()
        End If
    End Sub

    Private Sub Make_PrevDisPercent()
        If String.IsNullOrWhiteSpace(F_POS.TotalTextBox.Text) = False Then
            Dim Bill_Total As Double = F_POS.TotalTextBox.Text
            Dim Discount_Val As Double = (PrevDisPercent / 100) * Bill_Total
            F_POS.DiscountTextBox.Text = Discount_Val
            F_POS.Calc_Bill(False)
            F_POS.Make_Discount()
        End If
    End Sub



    Private Sub Cash_AG_btn_Click(sender As Object, e As EventArgs) Handles Cash_AG_btn.Click

        If Application.OpenForms().OfType(Of Sales).Any Then
            Switch_Sales_Agent(F_Sales.T_ID, Default_AG_ID, F_Sales.On_Update)
            Exit Sub
        End If

        If SB_is_Fast = False Then
            If Home_Panel = "POS" Then
                AG_Balance_Update_AG(Default_AG_ID)
            Else
                TB_ButOnAG(Default_AG_ID)
            End If
        Else
            Switch_Fast_Sales_Agent(Sales_Fast.T_ID, 1, Sales_Fast.On_Update)
        End If

    End Sub


    Private Sub AG_Account_btn_Click(sender As Object, e As EventArgs) Handles AG_Account_btn.Click
        If AG_ID <> Default_AG_ID Then
            Me.Cursor = Cursors.AppStarting
            Show_AG_Balance()
            Me.Cursor = Cursors.Default
        End If
    End Sub
    Private Sub Show_AG_Balance()
        F_Balances = New Balances
        With F_Balances
            .AG_ID = AG_ID
            .AG_Cm.Set_IM_By_ID(AG_ID)


            .Load_Data()
            .AllAgentsCheckBox.Enabled = False
            .AllRecieptsCheckBox.Checked = True
            .AllUsersCheckBox.Checked = True
            .AllTimeCheckBox.Checked = True
            .AG_MV_Prepare_To_Search()

            .MetroTabControl1.TabPages.Remove(.MetroTabPage2)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage3)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage4)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage5)
            .MenuStrip1.Visible = False
        End With
        F_Balances.ShowDialog()
    End Sub


    'Private Sub Deliver_btn_Click(sender As Object, e As EventArgs)
    '    Dim tmp = F_POS.T_ID
    '    F_POS.T_ID = 0
    '    FormType = 0
    '    AG_Type = 3
    '    F_Receipt = New Receipt
    '    F_Receipt.ShowDialog()
    '    F_POS.T_ID = tmp
    'End Sub

    'Private Sub Cash_btn_Click(sender As Object, e As EventArgs)
    '    Dim tmp = F_POS.T_ID
    '    F_POS.T_ID = 0
    '    FormType = 0
    '    AG_Type = 4
    '    F_Receipt = New Receipt
    '    F_Receipt.ShowDialog()
    '    F_POS.T_ID = tmp
    'End Sub

    'Public Sub Load_IM()
    '    Dim c As New C

    '    Try
    '        IM_Dt.Clear()

    '        Dim Str As String = ""

    '        If is_By_PhoneCB.Checked = False Then
    '            Str = "select AG_ID,Ag_name,isnull(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE Ag_name Like '%" & IM_SH_txt.Text & "%' AND AG_ID NOT IN('" & U_AG_ID & "') AND Type_ID <> '" & Suply_Type_ID & "' Order by Ag_name ASC"
    '        Else
    '            Str = "select AG_ID,Ag_name,Ag_phone,isnull(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE  Ag_phone Like '%" & IM_SH_txt.Text & "%' AND AG_ID NOT IN('" & U_AG_ID & "') AND Type_ID <> '" & Suply_Type_ID & "' Order by Ag_name ASC"
    '        End If

    '        c.Da = New SqlClient.SqlDataAdapter(Str, c.Con)
    '        c.Da.Fill(IM_Dt)
    '        IMDataGridViewX.DataSource = IM_Dt
    '        If IM_Dt.Rows.Count > 0 Then
    '            IMDataGridViewX.Visible = True
    '            IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
    '        Else
    '            IMDataGridViewX.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    'Private Sub IM_SH_txt_KeyDown(sender As Object, e As KeyEventArgs)

    '    If e.KeyCode = Keys.Up Then Barcode_SH_txt.Select()

    '    If e.KeyCode = Keys.Down Then If IMDataGridViewX.Visible = True Then IMDataGridViewX.Select()

    '    If e.KeyCode = Keys.Return Then
    '        If IMDataGridViewX.Visible = True Then
    '            Fetch_ItemToList(IMDataGridViewX.CurrentRow.Cells(0).Value)
    '        Else
    '            Select_AG_btn_Click(sender, e)
    '        End If
    '    End If

    'End Sub

    'Private Sub IM_SH_txt_Enter(sender As Object, e As EventArgs)
    '    Set_Ar_Language()
    'End Sub

    'Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs)
    '    If IM_SH_txt.Text.Count > 0 Then
    '        Load_IM()
    '    Else
    '        IMDataGridViewX.Visible = False
    '        AG_ID = 0
    '        Current_QTY.Clear()
    '        AG_Type_cm.SelectedIndex = -1
    '    End If

    '    If AG_ID = 0 Then
    '        IM_SH_txt.BackColor = Color.LightGray
    '    Else
    '        IM_SH_txt.BackColor = Color.LightGoldenrodYellow
    '    End If
    'End Sub

    'Private Sub IMDataGridViewX_CellClick(sender As Object, e As DataGridViewCellEventArgs)
    '    Fetch_ItemToList(IMDataGridViewX.CurrentRow.Cells(0).Value)
    'End Sub

    'Private Sub IMDataGridViewX_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then
    '        Fetch_ItemToList(IMDataGridViewX.CurrentRow.Cells(0).Value)
    '        IM_SH_txt.Select()
    '    End If

    '    If e.KeyCode = Keys.Up Then If IMDataGridViewX.CurrentRow.Index = 0 Then IM_SH_txt.Select()
    'End Sub

    Private Sub Fetch_ItemToList(AG_ID As Integer)
        ' If IMDataGridViewX.Rows.Count > 0 Then
        '-------------------------------------------------------------------
        Dim c As New C
        Dim Data As Byte()
        Try
            Dim s As String
            s = "select * from AGENTS_MENU_V WHERE AG_ID = '" & AG_ID & "'"

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_Dt.Clear()
                AG_ID = c.Dr("AG_ID")
                '   IM_SH_txt.Text = c.Dr("Ag_name")
                Current_QTY.Text = c.Dr("T_Balance")
                AG_Type_cm.SelectedValue = c.Dr("Type_ID")
                If IsDBNull(c.Dr("AG_img")) = False Then
                    Data = DirectCast(c.Dr("AG_img"), Byte())
                    Dim MS As New MemoryStream(Data)
                    BillPictureBox.Image = Image.FromStream(MS)
                Else
                    BillPictureBox.Image = My.Resources.person
                End If
                '  IMDataGridViewX.Visible = False

                MY_Settings.is_ByBarInput = False


            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '-------------------------------------------------------------------
        '   End If
    End Sub

    'Private Sub Show_IM_btn2_Click(sender As Object, e As EventArgs)
    '    If IMDataGridViewX.Visible = True Then
    '        IMDataGridViewX.Visible = False
    '    Else
    '        Fill_All_AG()
    '        IMDataGridViewX.Visible = True
    '        IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
    '    End If
    'End Sub

    'Private Sub Fill_All_AG()
    '    Try
    '        Dim C As New C

    '        C.Dt.Clear()
    '        Dim s As String = "SELECT AG_ID,Ag_name,isnull(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE Type_ID <> '" & Suply_Type_ID & "' Order by Ag_name ASC"
    '        C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
    '        C.Da.Fill(C.Dt)
    '        IMDataGridViewX.DataSource = C.Dt
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        If e.KeyCode = Keys.Return Then If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode()
        '   If e.KeyCode = Keys.Down Then IM_SH_txt.Select()
        If e.KeyCode = Keys.Delete Then Barcode_SH_txt.Clear()
    End Sub

    Public Sub Load_IM_Barcode()
        Dim c As New C
        Dim Data As Byte()
        IM_Dt.Clear()
        Try
            Dim s As String
            s = "select * from AGENTS_MENU_V WHERE Barcode = '" & Barcode_SH_txt.Text & "' AND Type_ID IN ('" & Cr_Type_ID & "','" & General_AG_Type_ID & "')"

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                'AG_ID = 
                'IM_SH_txt.Text = c.Dr("Ag_name")
                Current_QTY.Text = c.Dr("T_Balance")
                AG_Type_cm.SelectedValue = c.Dr("Type_ID")
                If IsDBNull(c.Dr("AG_img")) = False Then
                    Data = DirectCast(c.Dr("AG_img"), Byte())
                    Dim MS As New MemoryStream(Data)
                    BillPictureBox.Image = Image.FromStream(MS)
                Else
                    BillPictureBox.Image = My.Resources.person
                End If
                ' IMDataGridViewX.Visible = False
                MY_Settings.is_ByBarInput = True
                Save_AppSetting()
                ' IM_SH_txt.Select()

                AG_Cm.Set_IM_By_ID(c.Dr("AG_ID"))

            Else
                MsgBox("لم يتم التعرف على رقم العميل", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    'Private Sub Button1_Click(sender As Object, e As EventArgs)
    '    Barcode_SH_txt.Clear()
    '    IM_SH_txt.Clear()
    '    If MY_Settings.is_ByBarInput = True Then
    '        Barcode_SH_txt.Select()
    '    Else
    '        IM_SH_txt.Select()
    '    End If
    'End Sub


    Private Sub AgentsMenu_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles is_By_PhoneCB.CheckedChanged
        CB_CHecked(sender)
        ' IM_SH_txt.Select()
    End Sub

    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        Fetch_ItemToList(AG_Cm.TXT_ID.Text)
    End Sub
End Class