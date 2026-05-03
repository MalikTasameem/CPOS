Public Class SB_IM_card : Inherits System.Windows.Forms.Form

    Public T_ID As Integer
    Public IM_ID As Integer = 0
    Public Barcode_IM As String = ""
    Dim IM_QTY As Double = 0
    Public AG_ID As Integer = 0
    Dim U_Dt As New DataTable
    Public Get_Unit As Boolean = False
        Dim U_Cargo As Double = 0
        Dim ALL_QTY As Double = 0
    Dim Valid_Dt As New DataTable
    Public On_Update As Boolean
        Dim U_ID As Integer
        Dim Min_SP As Double
    Dim Min_SP_2 As Double
    ' --- إغلاق الفورم ---
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    ' --- حركة الفورم (السحب والإفلات) ---
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, Title_LB.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, Title_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, Title_LB.MouseUp
        drag = False
    End Sub


    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        '    If On_Update = True Then Edit_butt_Click(sender, e)
        Me.Dispose()
        End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape : Me.Close()
        End Select
    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        If St_Count() = 1 Then All_St_Panel.Visible = False
        'FormType = 1
        ThemeManager.ApplyThemeToForm(Me)
        Check_View_Control()

        Load_ST()

        Min_SP_CB.Visible = U_Sell_Under_Min_SP
        Min_SP_2_CB.Visible = U_Sell_Under_Min_SP_2

        If U_SB_IM_Update = True Then
            PriceTextBox.ReadOnly = False
            Bercent_TXT.ReadOnly = False
        Else
            PriceTextBox.ReadOnly = True
            Bercent_TXT.ReadOnly = True
        End If


        FunModule.Load_ALL_IM()
        ' تحميل البيانات
        mySearchControl.ItemsTable = IM_Dt
        mySearchControl.itemsTable_Barcode = IM_Dt_Barcodes
        mySearchControl.MaxGridHeight = 400

        'mySearchControl.DefaultSearchField = "اسم الصنف"
        ' إضافة الكنترول للفورم
        'Me.Controls.Add(mySearchControl)
        ' استقبال الاختيار
        AddHandler mySearchControl.ItemSelected, AddressOf HandleItemSelected

        mySearchControl.txtSearch.Select()

    End Sub

    Private Sub HandleItemSelected(itemId As Integer, isValid As String)
        PriceTextBox.Clear()
        IM_ID = itemId
        Get_Unit = False
        Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
        Fetch_IM_Units()
        Load_IM_Change_Price()
        QtyTextBox.Select()

        If isValid = 1 Then
            Valid_Panel.Visible = True
            Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
            IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
        Else
            Valid_Panel.Visible = False
        End If

    End Sub


    Public Sub Check_View_Control()

        Min_SP_Panel.Visible = S_Allow_MinSP
        Serial_Code_Panel.Visible = S_SerialCode
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


    Private Sub Enable_Fields()
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        QtyTextBox.Enabled = False
        PriceTextBox.Enabled = False
        ADDCatButton.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        QtyTextBox.Enabled = True
        PriceTextBox.Enabled = True
        ADDCatButton.Enabled = True
    End Sub


    'Private Sub ClearFields()

    '    T_ID = 0
    '    PriceTextBox.Clear()
    '    ClearCatFields()
    '    Disc = 0
    '    On_Update = False
    '    SB_ID = 0
    '    ST_cm.SelectedValue = SB_ST_ID
    '    End Sub


    Public Sub ADD_IM()


            If On_Update = True Then
                If AG_ID <> Default_AG_ID Then
                    If U_AG_Skip_Max = False Then
                        If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then
                            MsgBox("عذرا ... هذا الزبون قد تخطى سقف الدين الخاص به ولا يمكنك إضافة المزيد من الأصناف عليه", MsgBoxStyle.Critical, "خطأ فالإدراج")
                        Exit Sub
                    End If
                    Else
                        If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then MsgBox("هذا الزبون قد تخطى سقف الدين الخاص به", MsgBoxStyle.Exclamation, "تنويه ")
                    End If
                End If
            End If


            If IM_ID = 0 Then
            MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
        ElseIf String.IsNullOrWhiteSpace(PriceTextBox.Text) Then
                MsgBox("حدد سعر القطعة", MsgBoxStyle.Exclamation)
                PriceTextBox.Select()
            Else
                If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"


                If S_Allow_MinSP = True Then
                    If User_isAdmin = False Then

                        If U_Sell_Under_Min_SP = True And Min_SP_CB.Checked = True Then
                            If Convert.ToDouble(PriceTextBox.Text) < Min_SP And Min_SP > 0 Then
                                MsgBox(" ( " + Min_SP.ToString + " ) لا يمكنك البيع بأقل من  سعر الجملة", MsgBoxStyle.Exclamation)
                                Exit Sub
                            End If

                        End If

                        If U_Sell_Under_Min_SP_2 = True And Min_SP_2_CB.Checked = True Then
                            If Convert.ToDouble(PriceTextBox.Text) < Min_SP_2 And Min_SP_2 > 0 Then
                                MsgBox(" ( " + Min_SP_2.ToString + " ) لا يمكنك البيع بأقل من  سعر جملة الجملة", MsgBoxStyle.Exclamation)
                                Exit Sub
                            End If

                        End If

                    Else

                        If Convert.ToDouble(PriceTextBox.Text) < Min_SP And Min_SP > 0 Then
                            If MessageBox.Show(" ( " + Min_SP.ToString + " ) سوف يتم البيع بأقل من  سعر الجملة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                                           MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub

                            If Convert.ToDouble(PriceTextBox.Text) < Min_SP_2 And Min_SP_2 > 0 Then
                                If MessageBox.Show(" ( " + Min_SP_2.ToString + " ) سوف يتم البيع بأقل من  سعر جملة الجملة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                                               MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
                            End If

                        End If
                    End If
                End If


                If U_SB_Sell_Under_Cost = False Then
                    If Show_IM_Cost(False, IM_ID, U_ID) > PriceTextBox.Text Then
                        MsgBox("لا يمكنك البيع بأقل من سعر التكلفة", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                Else
                    If Show_IM_Cost(False, IM_ID, U_ID) > PriceTextBox.Text Then
                        If MessageBox.Show(" سوف يتم البيع بأقل من سعر التكلفة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                                                  MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
                    End If
                End If

            If IM_min_QTY = False Then

                If IM_Check_Neg_QTY_() = 1 Then
                    If QTY_ALERT_SOUND = True Then My.Computer.Audio.Play(Application.StartupPath & "\QTY ALERT.wav")
                    If IM_min_QTY = False Then
                        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If

            End If


            If Valid_Panel.Visible = True And Valid_cm.Items.Count > 0 Then
                    If Ban_Expierd_IM_MV = True Then

                        If Convert.ToDateTime(Valid_cm.Text) <= Date.Now.Date Then
                            MsgBox("صنف منتهية صلاحيته لا يمكن بيعه", MsgBoxStyle.Critical, "خطأ")
                            Exit Sub
                        End If
                    End If
                End If


                If SB_IM_Alert_When_Repetition = True Then
                For i = 0 To F_Sales.AGMetroGrid.Rows.Count - 1
                    If F_Sales.AGMetroGrid.Rows(i).Cells("Bill_IMID_CL").Value = IM_ID Then
                        Beep()
                        If MessageBox.Show(" هذا الصنف تم إدراجه بالفاتورة ... هل تريد الإستمرار ؟ ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                            Exit Sub
                        Else
                            Add_ItemToBill(IM_ID)
                            Exit Sub
                        End If
                    End If
                Next
            End If


                If Valid_Panel.Visible = True And Valid_cm.Items.Count > 1 Then
                    Beep()
                    If MessageBox.Show(" يوجد من هذا الصنف أكثر من صلاحية وانت اخترت صلاحية  " + vbNewLine + Valid_cm.Text + vbNewLine + " هل تريد الإستمرار ؟ ", "",
                                   MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.Cancel Then
                        Exit Sub
                    End If
                End If

                Add_ItemToBill(IM_ID)
            End If
        End Sub

    Private Function IM_Check_Neg_QTY_()
            Dim C As New C
            Dim F As Integer = 0
            With C.Com
                .Connection = C.Con
                .CommandText = "IM_Check_Neg_QTY_"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@F", 0)
                .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
                .Parameters.AddWithValue("@IM_ID", IM_ID)
                .Parameters.AddWithValue("@D_Vaild", Valid_cm.Text)
                .Parameters.AddWithValue("@Enterd_Qty", QtyTextBox.Text)
                .Parameters.AddWithValue("@Cargo", U_Cargo)

                .Parameters("@F").Direction = ParameterDirection.Output
                If SQL_SP_EXEC(C.Com) Then F = .Parameters("@F").Value
            End With

            Return F
        End Function


    Private Sub ClearCatFields()
        IM_ID = 0
        Current_QTY.Clear()
        PriceTextBox.Clear()
        QtyTextBox.Clear()
        U_Dt.Clear()

        ALL_QTY_txt.Clear()
        Valid_QTY_txt.Clear()
        Valid_Dt.Clear()
        If S_Stores = False Then ST_Bercent_Panel.Visible = False
        Barcode_IM = ""
        Serial_Code_txt.Clear()
        LAST_SELL_Lb.Text = "أخر بيع للزبون: "
    End Sub


    Public Sub Add_ItemToBill(IM_ID As String)
            Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)
        If Not String.IsNullOrWhiteSpace(Bercent_TXT.Text) Then PriceTextBox.Text =
    (Convert.ToDouble(PriceTextBox.Text) + Convert.ToDouble(PriceTextBox.Text) * (Convert.ToDouble(Bercent_TXT.Text) / 100)).ToString("00.00")

        Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "SB_Contents_INSERT_2"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
                .Parameters.AddWithValue("@IM_ID", IM_ID)
                .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
                If Valid_Panel.Visible = True Then
                    .Parameters.AddWithValue("@D_Vaild", Valid_cm.Text)
                    .Parameters.AddWithValue("@Current_QTY", Convert.ToDouble(Current_QTY.Text))
                End If

                If String.IsNullOrWhiteSpace(QtyTextBox.Text) = False Then .Parameters.AddWithValue("@QYT", Convert.ToDouble(QtyTextBox.Text))
                .Parameters.AddWithValue("@U_ID", U_ID)
                .Parameters.AddWithValue("@Price", PriceTextBox.Text)
                .Parameters.AddWithValue("@On_Update", On_Update)
                If Not String.IsNullOrWhiteSpace(Bercent_TXT.Text) Then .Parameters.AddWithValue("@Notes", Bercent_TXT.Text & " % ")

                .Parameters.AddWithValue("@Barcode", Barcode_IM)
                .Parameters.AddWithValue("@Serial_Code", Serial_Code_txt.Text)


                .Parameters.AddWithValue("@is_By_Min_SP", Min_SP_CB.Checked)
                .Parameters.AddWithValue("@is_By_Min_SP_2", Min_SP_2_CB.Checked)
                .Parameters.AddWithValue("@SB_IM_NEW_ROW", SB_IM_NEW_ROW)
            End With

            If SQL_SP_EXEC(C.Com) = True Then
                If On_Update = True Then
                    DependingUpdatedBill(T_ID)

            End If

            Network_Edit_Tracker_insert(" الصنف:" + mySearchControl.txtSearch.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " السعر:" + PriceTextBox.Text, F_Sales.Bill_ID_Txt.Text, 1, 1)

            F_Sales.SB_Contents_SELECT_Bill()
            ClearCatFields()


            Me.Close()

        End If

        End Sub

    Private Sub PriceTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles PriceTextBox.KeyDown
            Select Case e.KeyCode
            Case Keys.Up : mySearchControl.txtSearch.Select()
              '  Case Keys.Down : If AGMetroGrid.Rows.Count > 0 = True Then AGMetroGrid.Select()
            Case Keys.Right
                    IM_Unit_cm.Select()
                    IM_Unit_cm.DroppedDown = True
                Case Keys.Return : QtyTextBox.Select()
            End Select
        End Sub

        Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PriceTextBox.KeyPress
            Check_Only_Float(sender, e)
        End Sub

        Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs) Handles PriceTextBox.TextChanged
            Check_Point_in_FloatNum(sender, e)
        End Sub

        Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown

            Select Case e.KeyCode
            Case Keys.Return : ADD_IM()
            Case Keys.Right : PriceTextBox.Select()
                Case Keys.Left
                    If Valid_Panel.Visible = True Then
                        Valid_cm.DroppedDown = True
                        Valid_cm.Select()
                    End If
            End Select

        End Sub

        Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyTextBox.KeyPress
            Check_Only_Float(sender, e)
        End Sub

    Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles QtyTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Public Sub Load_IM_Change_Price()
        Dim c As New C
        Try
            Dim s As String
            s = "select Percent_Price from Change_Price WHERE GM_ID = (SELECT GM_ID FROM IM_Menu WHERE IM_ID = '" & IM_ID & "') "
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                ST_Bercent_Panel.Visible = True
                Bercent_TXT.Text = c.Dr("Percent_Price")
            Else
                ST_Bercent_Panel.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Fetch_IM_Units()
        Get_Unit = False
        Dim c As New C
        U_Dt.Clear()
        Try
            Dim s As String
            s = "select U_IM_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Asc"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(U_Dt)
            IM_Unit_cm.DataSource = U_Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_IM_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Get_Unit = True
        IM_Fetch_QTY()
    End Sub

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return : If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode()
            Case Keys.Down : QtyTextBox.Select()
            Case Keys.Delete
                Barcode_SH_txt.Clear()
                Barcode_IM = ""
        End Select
    End Sub

    Public Sub Load_IM_Barcode()


        Dim c As New C
        IM_Dt.Clear()
        Try
            Dim s As String

            s = "select U_IM_ID,IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_ID = c.Dr("IM_ID")
                mySearchControl.SelectItemById(IM_ID)

                Get_Unit = False
                Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
                Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
                Load_IM_Change_Price()

                If c.Dr("isValid") = 1 Then
                    Valid_Panel.Visible = True
                    Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
                Else
                    Valid_Panel.Visible = False
                End If

                QtyTextBox.Select()
                Fetch_IM_Units()

                Barcode_SH_txt.Clear()
                If MY_Settings.S_CodeAdd_1 = True Then ADD_IM()
            Else
                If Barcode_SH_txt.Text.Count >= 8 Then
                    Check_If_Mizan()
                Else
                    MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
                    Barcode_SH_txt.Clear()
                    Barcode_IM = ""
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
        Dim Price As Double = 0
        Dim Price_Dot As String = ""

        Dim Qty_Dot As String = ""
        Dim T_Price As Double = 0
        Dim T_Price_Dot As String = ""

        Try

            For i = Mizan_BarcodeFrom - 1 To Mizan_BarcodeTo - 1
                New_Barcode += Barcode_SH_txt.Text(i)
            Next

            Dim S As String = "Select U_IM_ID,IM_ID,item_name,isValid,Price from IM_units_Search_V WHERE Barcode = '" & New_Barcode & "'"
            c.Com = New SqlClient.SqlCommand(S, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()


                If Second_Part_isPrice = 0 Then
                    For i = Mizan_QtyFrom - 1 To Mizan_QtyTo - 1
                        Qty += Barcode_SH_txt.Text(i)
                    Next
                    QtyTextBox.Text = Convert.ToDouble(Qty) / 1000
                Else


                    For i = Mizan_QtyFrom - 1 To Mizan_QtyTo - 1
                        Qty_Dot += Barcode_SH_txt.Text(i)
                    Next
                    Qty = Qty_Dot(0) & Qty_Dot(1) '& Qty_Dot(2)
                    Qty_Dot = "0" & "." & Qty_Dot(2) & Qty_Dot(3) & Qty_Dot(4)
                    Qty = Qty + Convert.ToDouble(Qty_Dot)
                    QtyTextBox.Text = Qty


                    For j = Mizan_BarcodeTo To Mizan_QtyFrom - 1
                        T_Price_Dot += Barcode_SH_txt.Text(j)
                    Next
                    T_Price = T_Price_Dot(0) & T_Price_Dot(1) & T_Price_Dot(2)
                    T_Price_Dot = "0" & "." & T_Price_Dot(3) & T_Price_Dot(4)
                    T_Price = T_Price + Convert.ToDouble(T_Price_Dot)
                    '-------------------------------------------------------------------------------

                    PriceTextBox.Text = Convert.ToDouble(T_Price) / Qty


                End If

                IM_ID = c.Dr("IM_ID")
                mySearchControl.SelectItemById(IM_ID)
                Get_Unit = False
                Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
                Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)

                If c.Dr("isValid") = 1 Then
                    Valid_Panel.Visible = True
                    Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
                Else
                    Valid_Panel.Visible = False
                End If

                QtyTextBox.Select()
                Fetch_IM_Units()
                IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
                Barcode_SH_txt.Clear()

                If MY_Settings.S_CodeAdd_1 = True Then ADD_IM()

            Else
                MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
                Barcode_IM = ""
                Barcode_SH_txt.Clear()
            End If

        Catch ex As Exception
        End Try
    End Sub


    Private Sub IM_Fetch_QTY()
        Dim c As New C
        Try
            Dim s As String
            s = "select U_ID,U_Cargo,Price,Min_SP,Min_SP_2 from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                U_Cargo = c.Dr("U_Cargo")
                Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
                Current_QTY.Text = N.ToString("N")
                PriceTextBox.Text = c.Dr("Price")
                ALL_QTY_txt.Text = ALL_QTY / U_Cargo
                U_ID = c.Dr("U_ID")


                If Min_SP_CB.Checked = True Then
                    PriceTextBox.Text = c.Dr("Min_SP")
                    If c.Dr("Min_SP") = 0 Then PriceTextBox.Clear()
                ElseIf Min_SP_2_CB.Checked = True Then
                    PriceTextBox.Text = c.Dr("Min_SP_2")
                    If c.Dr("Min_SP_2") = 0 Then PriceTextBox.Clear()
                End If

                Min_SP = c.Dr("Min_SP")
                Min_SP_2 = c.Dr("Min_SP_2")

                Load_IM_LAST_SELL_AG()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Load_IM_LAST_SELL_AG()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT TOP 1 CONVERT(NUMERIC(18,2),Price) AS Price FROM Agent_SB_IM_MV_V WHERE AG_ID = '" & AG_ID & "' AND IM_ID = '" & IM_ID & "' ORDER BY T_ID DESC"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                LAST_SELL_Lb.Text = "أخر بيع للزبون: " & c.Dr("Price")
            Else
                LAST_SELL_Lb.Text = "أخر بيع للزبون: " & " لا يوجد "
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If String.IsNullOrWhiteSpace(Current_QTY.Text) = False And Get_Unit = True Then
            IM_Fetch_QTY()
            If Valid_Panel.Visible = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
        End If
    End Sub


    Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Unit_cm.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Left : QtyTextBox.Select()
        End Select
    End Sub


    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        ADD_IM()
    End Sub


    Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
        If Get_Unit = True Then
            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
            IM_Fetch_QTY()
            If Valid_Panel.Visible = True Then Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
            If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
        End If
    End Sub

    Private Sub Valid_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Valid_cm.SelectedValueChanged
        If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    End Sub


    Private Sub Valid_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles Valid_cm.KeyDown
        Select Case e.KeyCode
            Case Keys.Return
                QtyTextBox.Select()
                ADD_IM()
            Case Keys.Right : QtyTextBox.Select()
        End Select
    End Sub


    Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs) Handles ALL_QTY_txt.TextChanged
        If Not String.IsNullOrWhiteSpace(ALL_QTY_txt.Text) Then
            Dim N As Double = ALL_QTY_txt.Text
            ALL_QTY_txt.Text = N.ToString("N")
        End If
    End Sub


    Private Sub Min_SP_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Min_SP_CB.CheckedChanged
        CB_CHecked(sender)
        If Min_SP_CB.Checked = True Then Min_SP_2_CB.Checked = False
        Price_Text_Check_Read()
        IM_Fetch_QTY()
    End Sub

    Private Sub Min_SP_2_CBCheckedChanged(sender As Object, e As EventArgs) Handles Min_SP_2_CB.CheckedChanged
        CB_CHecked(sender)
        If Min_SP_2_CB.Checked = True Then Min_SP_CB.Checked = False
        Price_Text_Check_Read()
        IM_Fetch_QTY()
    End Sub

    Private Sub Price_Text_Check_Read()
        If Min_SP_CB.Checked = True Or Min_SP_2_CB.Checked = True Then
            PriceTextBox.ReadOnly = True
        Else
            If U_SB_IM_Update = True Then
                PriceTextBox.ReadOnly = False
                Bercent_TXT.ReadOnly = False
            Else
                PriceTextBox.ReadOnly = True
                Bercent_TXT.ReadOnly = True
            End If
        End If
    End Sub

    Private Sub Exit_Btn_Click(sender As Object, e As EventArgs) Handles Exit_Btn.Click
        Me.Close()
    End Sub

End Class