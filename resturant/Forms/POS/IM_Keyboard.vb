Imports System.Data.SqlClient

Public Class IM_Keyboard
    Dim Lang As String

    Dim IM_ID As Integer = 0
    Dim IM_Dt As New DataTable
    Dim IM_QTY As Double = 0
    Dim U_Dt As New DataTable
    Dim Valid_Dt As New DataTable
    Dim Get_Unit As Boolean = False
    Dim U_Cargo As Double = 0
    Dim ALL_QTY As Double = 0
    Dim U_ID As Integer
    Dim Min_SP As Double = 0
    Dim is_Valid As Boolean = False
    Dim Barcode_IM As String = ""

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If IM_SH_txt.Text < " " Then
            IM_SH_txt.Text = Mid(IM_SH_txt.Text, 1, Len(IM_SH_txt.Text) - 1 + 1)
        Else
            IM_SH_txt.Text = Mid(IM_SH_txt.Text, 1, Len(IM_SH_txt.Text) - 1)
        End If
    End Sub

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click

        If Application.OpenForms().OfType(Of POS).Any Then
            Fetch_ItemToList_POS()
        ElseIf Application.OpenForms().OfType(Of Sales_Fast).Any Then
            Fetch_ItemToList_TO_FAST_SALES()
        End If

    End Sub

    Private Sub Fetch_ItemToList_POS()
        If F_POS.MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
            If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then
                F_POS.NumTextBox.Text = "1"
            Else
                F_POS.NumTextBox.Text = QtyTextBox.Text
            End If

            If IMDataGridViewX.Visible = True Then
                Fetch_ItemToList()
            Else
                If IM_ID > 0 Then

                    If IM_min_QTY = False Then
                        If F_POS.BillTypeCmb.SelectedValue <> 3 Then
                            If IM_Check_Neg_QTY_() = 1 Then
                                Beep()
                                Dim MSG As New OK
                                MSG.Msg_Lb.Text = "لا يمكنك إدراج صنف بكمية سالبة"
                                MSG.ShowDialog()
                                Exit Sub
                            End If
                        End If
                    End If

                    If U_SB_Sell_Under_Cost = False Then
                        If Show_IM_Cost(False, IM_ID, U_ID) > PriceTextBox.Text Then
                            MsgBox("لا يمكنك البيع بأقل من سعر التكلفة", MsgBoxStyle.Critical)
                            Exit Sub
                        End If
                    End If

                    If Valid_Panel.Visible = True Then
                        F_POS.is_Valid = True
                        F_POS.D_Valid = Valid_cm.Text
                    End If
                    F_POS.U_IM_ID = IM_Unit_cm.SelectedValue

                    For i = 0 To F_POS.MetroGrid.Rows.Count - 1
                        If F_POS.MetroGrid.Rows(i).Cells("IM_ID_CL").Value = IM_ID Then
                            Beep()
                            Dim MSG1 As New YesNo
                            MSG1.Msg_Lb.Text = "هذا الصنف تم إدراجه بالفاتورة ... هل تريد الإستمرار ؟"
                            MSG1.ShowDialog()
                            If MSG1.Result = True Then
                                F_POS.Fetch_ItemToList(IM_ID)
                                Me.Close()
                                Exit Sub
                            Else
                                Exit Sub
                            End If
                        End If
                    Next
                    F_POS.Fetch_ItemToList(IM_ID)
                    Me.Close()
                End If
            End If
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
            .Parameters.AddWithValue("@D_Vaild", Valid_cm.Text)
            If String.IsNullOrWhiteSpace(QtyTextBox.Text) = False Then .Parameters.AddWithValue("@Enterd_Qty", Convert.ToDouble(QtyTextBox.Text))

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function

    Private Sub Fetch_ItemToList_TO_FAST_SALES()

        If IMDataGridViewX.Rows.Count > 0 Then
            Sales_Fast.IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
            IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
            Sales_Fast.IM_Name = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value

            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
            Get_Unit = False
            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
            Fetch_IM_Units()
            IMDataGridViewX.Visible = False
            'Sales_Fast.Load_IM_Change_Price()
            Sales_Fast.IM_Unit_cm.Text = IM_Unit_cm.Text

            If IMDataGridViewX.CurrentRow.Cells("isValid_CL").Value = 1 Then
                is_Valid = True
                Sales_Fast.is_Valid = True
            End If


            ADD_IM()
        End If
    End Sub



    Public Sub ADD_IM()
        If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"


        Sales_Fast.IM_ID = IM_ID
        Sales_Fast.U_ID = U_ID
        Sales_Fast.IM_Price = PriceTextBox.Text
        Sales_Fast.Barcode_IM = Barcode_IM
        Sales_Fast.QtyTextBox = QtyTextBox.Text
        Sales_Fast.U_Cargo = U_Cargo
        Sales_Fast.Valid_TXT = Valid_cm.Text

        If S_Allow_MinSP = True Then
            If User_isAdmin = False Then
                If U_Sell_Under_Min_SP = True Then
                    If Convert.ToDouble(PriceTextBox.Text) < Min_SP And Min_SP > 0 Then
                        MsgBox(" ( " + Min_SP.ToString + " ) لا يمكنك البيع بأقل من أدنى سعر بيع", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                End If

            Else
                If Convert.ToDouble(PriceTextBox.Text) < Min_SP And Min_SP > 0 Then
                    If MessageBox.Show(" ( " + Min_SP.ToString + " ) سوف يتم البيع بأقل من أدنى سعر بيع", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
                                       MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
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
                If MessageBox.Show(" سوف يتم البيع بأقل من سعر التكلفة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
                                              MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            End If
        End If

        If IM_min_QTY = False Then
            If Sales_Fast.IM_Check_Neg_QTY_() = 1 Then
                MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If


        If SB_IM_Alert_When_Repetition = True Then
            For i = 0 To Sales_Fast.AGMetroGrid.Rows.Count - 1
                If Sales_Fast.AGMetroGrid.Rows(i).Cells("Bill_IMID_CL").Value = IM_ID Then
                    Beep()
                    If MessageBox.Show(" هذا الصنف تم إدراجه بالفاتورة ... هل تريد الإستمرار ؟ ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    Else
                        Sales_Fast.Add_ItemToBill(IM_ID)
                        Exit Sub
                    End If
                End If
            Next
        End If

        Beep()
        If Notif_If_SB_Has_No_SB_Price = True Then
            If Convert.ToDouble(PriceTextBox.Text) = 0 Then
                If MessageBox.Show(" لم يتم تحديد سعر بيع للصنف أوسعره = 0 ... هل تريد الإستمرار فالبيع ", "", _
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
        End If

        Sales_Fast.Add_ItemToBill(IM_ID)
        Me.Close()
    End Sub






    Private Sub btnShiftR_Click(sender As Object, e As EventArgs) Handles btnShiftR.Click, btnShiftL.Click
        If btnShiftR.FlatStyle = FlatStyle.Flat Then
            btnShiftR.FlatStyle = FlatStyle.Standard
            btnShiftL.FlatStyle = FlatStyle.Standard
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Text = bttn.Text.ToLower
                    btn01.Text = "1"
                    btn02.Text = "2"
                    btn03.Text = "3"
                    btn04.Text = "4"
                    btn05.Text = "5"
                    btn06.Text = "6"
                    btn07.Text = "7"
                    btn08.Text = "8"
                    btn09.Text = "9"
                    btn00.Text = "0"
                    'btn11.Text = "-"
                    'btn12.Text = "="
                    'btn13.Text = "`"
                    'btn14.Text = "\"
                    'btn15.Text = "]"
                    'btn16.Text = "["
                    'btn29.Text = "'"
                    'btn30.Text = ";"
                    'btn28.Text = "/"
                    'btn40.Text = "."
                    'btn41.Text = ","
                End If
            Next
        ElseIf btnShiftR.FlatStyle = FlatStyle.Standard Then
            btnShiftL.FlatStyle = FlatStyle.Flat
            btnShiftR.FlatStyle = FlatStyle.Flat
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Text = bttn.Text.ToUpper
                    btn01.Text = "!"
                    btn02.Text = "@"
                    btn03.Text = "#"
                    btn04.Text = "$"
                    btn05.Text = "%"
                    btn06.Text = "^"
                    btn07.Text = "&"
                    btn08.Text = "*"
                    btn09.Text = "("
                    btn00.Text = ")"
                    'btn11.Text = " _"
                    'btn12.Text = "+"
                    'btn13.Text = "~"
                    btn14.Text = "|"
                    btn15.Text = "}"
                    btn16.Text = "{"
                    btn29.Text = """"
                    btn30.Text = ":"
                    btn28.Text = "?"
                    btn40.Text = ">"
                    btn41.Text = "<"
                End If
            Next
        End If
    End Sub

    Private Sub btnCaps_Click(sender As Object, e As EventArgs) Handles btnCaps.Click
        If btnCaps.FlatStyle = FlatStyle.Flat Then
            btnCaps.FlatStyle = FlatStyle.Standard
            btnCaps.BackColor = Color.FromKnownColor(KnownColor.Control)
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Text = bttn.Text.ToLower
                    'btn1.Text = "1"
                    'btn2.Text = "2"
                    'btn3.Text = "3"
                    'btn4.Text = "4"
                    'btn5.Text = "5"
                    'btn6.Text = "6"
                    'btn7.Text = "7"
                    'btn8.Text = "8"
                    'btn9.Text = "9"
                    'btn10.Text = "0"
                    ''btn11.Text = "-"
                    ''btn12.Text = "="
                    ''btn13.Text = "`"
                    'btn14.Text = "\"
                    'btn15.Text = "]"
                    'btn16.Text = "["
                    'btn29.Text = "'"
                    'btn30.Text = ";"
                    'btn28.Text = "/"
                    'btn40.Text = "."
                    'btn41.Text = ","
                End If
            Next
        ElseIf btnCaps.FlatStyle = FlatStyle.Standard Then
            btnCaps.FlatStyle = FlatStyle.Flat
            btnCaps.BackColor = Color.LightGreen
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Text = bttn.Text.ToUpper
                    'btn01.Text = "!"
                    'btn02.Text = "@"
                    'btn03.Text = "#"
                    'btn04.Text = "$"
                    'btn05.Text = "%"
                    'btn06.Text = "^"
                    'btn07.Text = "&"
                    'btn08.Text = "*"
                    'btn09.Text = "("
                    'btn00.Text = ")"
                    ''btn11.Text = " _"
                    ''btn12.Text = "+"
                    ''btn13.Text = "~"
                    'btn14.Text = "|"
                    'btn15.Text = "}"
                    'btn16.Text = "{"
                    'btn29.Text = """"
                    'btn30.Text = ":"
                    'btn28.Text = "?"
                    'btn40.Text = ">"
                    'btn41.Text = "<"
                End If
            Next
        End If
    End Sub

    Private Sub btnTab_Click(sender As Object, e As EventArgs)
        IM_SH_txt.Text = IM_SH_txt.Text & "    "
    End Sub

    Private Sub btnSpace_Click(sender As Object, e As EventArgs) Handles btnSpace.Click
        IM_SH_txt.Text = IM_SH_txt.Text & " "
    End Sub

    Private Sub btn28_Click(sender As Object, e As EventArgs) Handles btn01.Click, _
        btn00.Click, btn14.Click, _
        btn15.Click, btn16.Click, btn02.Click, btn29.Click, btn03.Click, _
        btn30.Click, btn04.Click, btn40.Click, btn41.Click, btn05.Click, _
        btn06.Click, btn07.Click, btn08.Click, btn09.Click, btnA.Click, btnB.Click, _
        btnC.Click, btnD.Click, btnE.Click, btnF.Click, btnG.Click, btnH.Click, _
        btnI.Click, btnJ.Click, btnK.Click, btnL.Click, btnM.Click, btnN.Click, _
        btnO.Click, btnP.Click, btnQ.Click, btnR.Click, btnS.Click, btnT.Click, _
        btnV.Click, btnU.Click, btnW.Click, btnX.Click, btnY.Click, btnZ.Click
        If btnShiftR.FlatStyle = FlatStyle.Flat Then
            IM_SH_txt.Text = IM_SH_txt.Text + sender.text
            btnShiftR.PerformClick()
        Else
            IM_SH_txt.Text = IM_SH_txt.Text + sender.text
        End If
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub IM_Keyboard_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If IM_Use_Out_KB_CB.Checked = False Then
            If Me.Barcode_SH_txt.Focused = False Then
                Barcode_SH_txt.Focus()
                If Me.Barcode_SH_txt.Enabled = True Then
                    Barcode_SH_txt.Text = e.KeyChar.ToString
                    Barcode_SH_txt.SelectionStart = Barcode_SH_txt.Text.Length
                    e.Handled = True
                End If
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If U_SB_IM_Update = True Then
            PriceTextBox.ReadOnly = False
        Else
            PriceTextBox.ReadOnly = True
        End If
        Min_SP_Panel.Visible = S_Allow_MinSP
        If St_Count() = 1 Then All_St_Panel.Visible = False
        Lang = "EN"
        Load_ST()
        Swich_Lang_btn_Click(sender, e)
        IM_SH_txt.Select()
        IM_Use_Out_KB_CB.Checked = IM_Use_Out_KB
        Show_IM_Cost_btn.Visible = U_SB_Show_IM_COST
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

    Private Sub Swich_Lang_btn_Click(sender As Object, e As EventArgs) Handles Swich_Lang_btn.Click
        If Lang = "EN" Then
            btnQ.Text = "ض"
            btnW.Text = "ص"
            btnE.Text = "ث"
            btnR.Text = "ق"
            btnT.Text = "ف"
            btnY.Text = "غ"
            btnU.Text = "ع"
            btnI.Text = "ه"
            btnO.Text = "خ"
            btnP.Text = "ح"
            btn16.Text = "ج"
            btn15.Text = "د"
            btn14.Text = "ذ"
            '----------------------------------------------
            btnA.Text = "ش"
            btnS.Text = "س"
            btnD.Text = "ي"
            btnF.Text = "ب"
            btnG.Text = "ل"
            btnH.Text = "ا"
            btnJ.Text = "ت"
            btnK.Text = "ن"
            btnL.Text = "م"
            btn30.Text = "ك"
            btn29.Text = "ط"
            '----------------------------------------------
            btnZ.Text = "ئ"
            btnX.Text = "ء"
            btnC.Text = "ؤ"
            btnV.Text = "ر"
            btnB.Text = "لا"
            btnN.Text = "ى"
            btnM.Text = "ة"
            btn41.Text = "و"
            btn40.Text = "ز"
            btn28.Text = "ظ"
            Delete_Btn.Text = "مسح"
            btnBack.Text = "تراجع"
            Swich_Lang_btn.Text = "English Lang"
            btnEnter.Text = "إختيار"
            btnSpace.Text = "مساحة"
            IM_SH_txt.RightToLeft = Windows.Forms.RightToLeft.Yes
            Lang = "AR"
        Else
            btnQ.Text = "q"
            btnW.Text = "w"
            btnE.Text = "e"
            btnR.Text = "r"
            btnT.Text = "t"
            btnY.Text = "y"
            btnU.Text = "u"
            btnI.Text = "i"
            btnO.Text = "o"
            btnP.Text = "p"
            btn16.Text = ""
            btn15.Text = ""
            btn14.Text = ""
            '----------------------------------------------
            btnA.Text = "a"
            btnS.Text = "s"
            btnD.Text = "d"
            btnF.Text = "f"
            btnG.Text = "g"
            btnH.Text = "h"
            btnJ.Text = "j"
            btnK.Text = "k"
            btnL.Text = "l"
            btn30.Text = ""
            btn29.Text = ""
            '----------------------------------------------
            btnZ.Text = "z"
            btnX.Text = "x"
            btnC.Text = "c"
            btnV.Text = "v"
            btnB.Text = "b"
            btnN.Text = "n"
            btnM.Text = "m"
            btn41.Text = ""
            btn40.Text = ""
            btn28.Text = ""

            Delete_Btn.Text = "Delete"
            btnBack.Text = "Back"
            Swich_Lang_btn.Text = "لغة عربية"
            btnEnter.Text = "ENTER"
            btnSpace.Text = "Space"
            IM_SH_txt.RightToLeft = Windows.Forms.RightToLeft.No
            Lang = "EN"
        End If

    End Sub

    Private Sub Delete_Btn_Click(sender As Object, e As EventArgs) Handles Delete_Btn.Click
        IM_SH_txt.Clear()
    End Sub


    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        If e.KeyCode = Keys.Return Then Load_IM_Barcode()
    End Sub

    Public Sub Load_IM_Barcode()

        Dim c As New C
        IM_Dt.Clear()
        Try
            Dim s As String
            If By_IMNum_Rb.Checked = True Then
                s = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_NUM = '" & Barcode_SH_txt.Text & "'"
            Else
                s = "select U_IM_ID,IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
            End If

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_ID = c.Dr("IM_ID")
                IM_SH_txt.Text = c.Dr("item_name")

                Barcode_IM = Barcode_SH_txt.Text

                Get_Unit = False
                Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
                Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)

                IMDataGridViewX.Visible = False

                If c.Dr("isValid") = 1 Then
                    Valid_Panel.Visible = True
                    Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
                Else
                    Valid_Panel.Visible = False
                End If

                '  QtyTextBox.Select()
                Fetch_IM_Units()
                If By_IMNum_Rb.Checked = False Then IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")

                Barcode_SH_txt.Clear()
                Fetch_IM_Full_Photo()
            Else

                '   MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
                Beep()
                Dim MSG2 As New OK
                MSG2.Msg_Lb.Text = "!! لم يتم التعرف على الباركود"
                MSG2.ShowDialog()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
        IMNAME_Serach()
    End Sub

    Private Sub IMNAME_Serach()
        If IM_SH_txt.Text.Count > 0 Then
            Load_IM()
        Else
            IMDataGridViewX.Visible = False
            IM_ID = 0
            U_Dt.Clear()
            Current_QTY.Clear()
            ALL_QTY_txt.Clear()
            PriceTextBox.Clear()
            Valid_QTY_txt.Clear()
            Valid_Dt.Clear()
            QtyTextBox.Clear()
            Min_SP_txt.Clear()
        End If
        If IM_ID = 0 Then
            IM_SH_txt.BackColor = Color.LightGray
        Else
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Public Sub Load_IM()
        Dim c As New C
        Try
            IM_Dt.Clear()
            If SB_Sch_With_QTY = False Then
                c.Str = IM_Serach(IM_SH_txt.Text)
            Else
                c.Str = IM_Serach_With_QTY(IM_SH_txt.Text)
            End If
            c.Da = New SqlClient.SqlDataAdapter(c.Str, c.Con)
            c.Da.Fill(IM_Dt)
            IMDataGridViewX.DataSource = IM_Dt
            If IM_Dt.Rows.Count > 0 Then
                IMDataGridViewX.Visible = True
                IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 335)
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
        If e.KeyCode = Keys.Return Then Fetch_ItemToList()
        If e.KeyCode = Keys.Up Then If IMDataGridViewX.CurrentRow.Index = 0 Then IM_SH_txt.Select()
    End Sub

    Private Sub Fetch_ItemToList()

        If IMDataGridViewX.Rows.Count > 0 Then
            IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
            IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
            Get_Unit = False
            Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
            Fetch_IM_Units()
            IMDataGridViewX.Visible = False
            QtyTextBox.Select()

            If IMDataGridViewX.CurrentRow.Cells("isValid_CL").Value = 1 Then
                Valid_Panel.Visible = True
                Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
            Else
                Valid_Panel.Visible = False
            End If

            Fetch_IM_Full_Photo()
        End If
    End Sub

    Private Sub Fetch_IM_Full_Photo()
        Dim c As New C
        Dim S As String

        S = "select * from IM_All_With_No_Enable_V where IM_ID ='" & IM_ID & "'"

        c.Com = New SqlCommand(S, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()

                If IsDBNull(c.Dr("IM_Full_Photo")) = False Then
                    Try
                        Me.IM_Photo.Image = Image.FromFile(System.IO.Path.GetFullPath(c.Dr("IM_Full_Photo")))
                    Catch ex As Exception
                        MsgBox("تأكد من مسار الصورة" + vbNewLine + ex.Message, MsgBoxStyle.Exclamation, "")
                    End Try
                Else
                    Me.IM_Photo.Image = Nothing
                    Me.IM_Photo.BackColor = System.Drawing.SystemColors.ButtonFace
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

    End Sub


    Private Sub Fetch_IM_Units()
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

    Private Sub IM_Fetch_QTY()
        Dim c As New C
        Try
            Dim s As String
            s = "select U_ID,U_Cargo,Price,Min_SP from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "'"
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
                Min_SP_txt.Text = c.Dr("Min_SP")
                Min_SP = c.Dr("Min_SP")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
        If Get_Unit = True Then
            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
            IM_Fetch_QTY()
            If Valid_Panel.Visible = True Then Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
        End If
    End Sub

    Private Sub Valid_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Valid_cm.SelectedValueChanged
        If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PriceTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles QtyTextBox.TextChanged, PriceTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click, _
            btn2.Click, btn3.Click, _
            btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, _
            btn9.Click, btn10.Click, btn_Dot.Click
        QtyTextBox.Text = QtyTextBox.Text + sender.text
    End Sub

    Private Sub QTY_Clear_btn_Click(sender As Object, e As EventArgs) Handles QTY_Clear_btn.Click
        QtyTextBox.Clear()
    End Sub

    Private Sub Clear_Barcode_btn_Click(sender As Object, e As EventArgs) Handles Clear_Barcode_btn.Click
        Barcode_SH_txt.Clear()
    End Sub

    Private Sub By_IMNum_Rb_CheckedChanged(sender As Object, e As EventArgs) Handles By_IMNum_Rb.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If String.IsNullOrWhiteSpace(Current_QTY.Text) = False And Get_Unit = True Then
            IM_Fetch_QTY()
            If Valid_Panel.Visible = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
        End If
    End Sub

    Private Sub Use_Out_Keyboard_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IM_Use_Out_KB_CB.CheckedChanged
        CB_CHecked(sender)
        IM_SH_txt.Select()

        IM_Use_Out_KB = IM_Use_Out_KB_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub IM_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_SH_txt.KeyDown
        If e.KeyCode = Keys.Return Then Fetch_ItemToList()
        If e.KeyCode = Keys.Down Then IMDataGridViewX.Select()
    End Sub

    Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown
        If e.KeyCode = Keys.Return Then btnEnter_Click(sender, e)
    End Sub

    Private Sub Show_IM_Cost_btn_Click(sender As Object, e As EventArgs) Handles Show_IM_Cost_btn.Click
        Show_IM_Cost(True, IM_ID, U_ID)
    End Sub
End Class
