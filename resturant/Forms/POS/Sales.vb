Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class Sales : Inherits System.Windows.Forms.Form

    'Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public Pied_T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Public isPause As Boolean
    Public Receipts_DT As New DataTable

    Public isCashReceipt_Success As Boolean = False
    Public isShowingDetails As Boolean = False


    Public IM_ID As Integer = 0
    Public Barcode As String = ""
    ' Public Barcode_IM As String = ""
    ' Dim IM_Dt As New DataTable
    ' Dim IM_QTY As Double = 0
    Public TOTAL As Double = 0
    Public TOTAL_Check As Double = 0
    Public Disc As Double = 0
    Public Pure As Decimal = 0
    Public AG_ID As Integer = 0
    Dim AG_Dt As New DataTable
    ' Dim U_Dt As New DataTable
    ' Public Get_Unit As Boolean = False
    ' Dim U_Cargo As Double = 0
    ' Dim ALL_QTY As Double = 0
    ' Dim Valid_Dt As New DataTable
    Public isNewBill As Integer
    Dim isPied As Integer = 0
    Dim BillUser_ID As Integer

    Public On_Update As Boolean
    Dim U_ID As Integer
    '  Dim Min_SP As Double
    '  Dim Min_SP_2 As Double
    Public IS_Show_Rctp As Boolean = False

    Public SB_ID As Integer

    Dim PREV_AG_ID As Integer
    Public is_Order_IM_Print As Boolean = False

    Dim Old_Disc As Double = 0
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    ' تفعيل الظل الاحترافي للفورم
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or &H20000
            Return cp
        End Get
    End Property
    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp
        drag = False
    End Sub



    Private Sub MaxFormButton_Click(sender As Object, e As EventArgs) Handles MaxFormButton.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
            MaxFormButton.Text = "🗗" ' تغيير الرمز ليدل على التصغير
        Else
            Me.WindowState = FormWindowState.Normal
            MaxFormButton.Text = "⬜" ' إرجاع الرمز للمربع
        End If
    End Sub
    ' =========================================================
    Private Sub UpdateFormStateIndicator(ByVal StateText As String, ByVal StateColor As System.Drawing.Color)
        If lblFormState IsNot Nothing Then
            lblFormState.Text = "⬤  " & StateText
            lblFormState.BackColor = StateColor
            lblFormState.Visible = True
            lblFormState.Refresh() ' لإجبار الشاشة على إظهار اللون الجديد فوراً
        End If
    End Sub

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If On_Update = True Then Edit_butt_Click(sender, e)
        FormType = 0
        Me.Dispose()
    End Sub


    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.Escape : Me.Close()
            Case Keys.F1
                If New_butt.Enabled = True Then ResetNewBill()
            Case Keys.F12
                If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
            Case Keys.F2
                If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
            Case Keys.F3
                If Edit_butt.Enabled = True And Edit_butt.Visible = True Then Edit_butt_Click(sender, e)
            Case Keys.F4
                If Delete_butt.Enabled = True And Delete_butt.Visible = True Then Delete_butt_Click(sender, e)
            Case Keys.F7
                SBPauseBtn_Click(sender, e)

            Case Keys.PageUp
                Up_Bill_btn_Click(sender, e)
            Case Keys.PageDown
                Down_Bill_btn_Click(sender, e)

            Case Keys.F10
                If IM_Check_Panel.Visible = True Then Clear_Check_btn_Click(sender, e)

            Case 107 'Add

                If On_Update = False Then
                    If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                        If AGMetroGrid.Rows.Count > 0 Then
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
                End If


            Case 109 'Subtrac
                If On_Update = False Then
                    If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                        If AGMetroGrid.Rows.Count > 0 Then
                            If AGMetroGrid.CurrentRow.Cells("QTY_CL").Value > 1 Then
                                Dim Def As Integer = -1
                                Change_IM_Qty(Def)
                            End If
                        End If
                    End If
                End If

            Case Keys.F9
                If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 And On_Update = False Then Change_IM_Details.ShowDialog()

        End Select
    End Sub

    Private Function IM_Check_Neg_QTY_2()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_POS_2"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function

    Private Sub Change_IM_Qty(def As Integer)
        Dim SB_T_ID As Integer = AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
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
            SB_Contents_SELECT_Bill()
            AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
            Network_Edit_Tracker_insert(" الصنف:" + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString + " العدد:" + AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString + " السعر:" + AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString,
             Bill_ID_Txt.Text, 1, 3)
        End If
    End Sub

    Private Sub Sales_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

        Select Case e.KeyChar
            Case "+" 'Add
                e.Handled = True

            Case "-" 'Subtrac
                e.Handled = True
        End Select

    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' If St_Count() = 1 Then All_St_Panel.Visible = False
        SetupAnchors()
        Try
            If TitleBar_Panel IsNot Nothing Then TitleBar_Panel.Tag = "HEADER"

            If Title_Label IsNot Nothing Then Title_Label.Tag = "TITLE_TRANSPARENT"
            If ExitFormButton IsNot Nothing Then ExitFormButton.Tag = "DELETE"
            If MaxFormButton IsNot Nothing Then MaxFormButton.Tag = "GENERAL"

            ' 2. أزرار التحكم الرئيسية
            If New_butt IsNot Nothing Then New_butt.Tag = "GENERAL"
            If Save_butt IsNot Nothing Then Save_butt.Tag = "SAVE"
            If Edit_butt IsNot Nothing Then Edit_butt.Tag = "GENERAL"
            If Delete_butt IsNot Nothing Then Delete_butt.Tag = "DELETE"
            If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Tag = "DELETE"
            If Print_btn IsNot Nothing Then Print_btn.Tag = "PRINT"
            If SBPauseBtn IsNot Nothing Then SBPauseBtn.Tag = "GENERAL"
            If DeliveryingButton IsNot Nothing Then DeliveryingButton.Tag = "SAVE"

            ' 3. أزرار التحكم في الجدول (الأصناف)
            If ADDCatButton IsNot Nothing Then ADDCatButton.Tag = "GENERAL"
            If RemoveCatButton IsNot Nothing Then RemoveCatButton.Tag = "DELETE"
            If DGV_Control_btn IsNot Nothing Then DGV_Control_btn.Tag = "GENERAL"
            If Clear_Check_btn IsNot Nothing Then Clear_Check_btn.Tag = "GENERAL"

            ' 4. أزرار التنقل والعمليات الجانبية
            If Up_Bill_btn IsNot Nothing Then Up_Bill_btn.Tag = "GENERAL"
            If Down_Bill_btn IsNot Nothing Then Down_Bill_btn.Tag = "GENERAL"
            '  If Show_IM_btn2 IsNot Nothing Then Show_IM_btn2.Tag = "GENERAL"
            If Calc_Dicount_Btn IsNot Nothing Then Calc_Dicount_Btn.Tag = "GENERAL"
            If Show_AG_Projects_btn IsNot Nothing Then Show_AG_Projects_btn.Tag = "GENERAL"
            If Show_IM_Rtn_btn IsNot Nothing Then Show_IM_Rtn_btn.Tag = "GENERAL"
            If OpenCahDR_Btn IsNot Nothing Then OpenCahDR_Btn.Tag = "GENERAL"
            If Show_Cash_btn IsNot Nothing Then Show_Cash_btn.Tag = "GENERAL"
            If MoveToBill_Btn IsNot Nothing Then MoveToBill_Btn.Tag = "GENERAL"

            '''    تطبيق الثيم الإجباري باستخدام محرك الثيمات
            ThemeManager.ApplyThemeToForm(Me)
            ModernLoader.ShowLoader()

            FormType = 1

            'rs.FindAllControls(Me)
            NewStateBt()
            Check_View_Control()

            Me.WindowState = FormWindowState.Maximized
            EditState = Edit_butt.Text
            'Load_ST()

            Delete_butt.Visible = U_SalesVoid

            If U_SalesDis = True And isDiscount = True Then
                DiscountPanel.Visible = True
            Else
                DiscountPanel.Visible = False
            End If

            Edit_butt.Visible = U_SB_Update

            Show_Cash_btn.Visible = U_SB_Show_Cash

            AG_Show_Balance_CB.Checked = MY_Settings.SB_AG_Show_Balance
            Show_Bill_CB.Checked = MY_Settings.SB_Show_Bill
            Show_Bill_Rest_CB.Checked = MY_Settings.SB_Show_Bill_Rest
            Show_SumPied_CB.Checked = MY_Settings.SB_Show_SumPied


            عرضالتكلفةToolStripMenuItem.Visible = U_SB_Show_IM_COST

            Show_Cash_btn.Visible = S_Pr

            If isShowing_Trans = True Then
                T_ID = T_ID_Trans
                Fill_Bill_Info()
                SB_Contents_SELECT_Bill()
                SelectStateBt()
                New_butt.Enabled = False
                SBPauseBtn.Enabled = False
            Else
                If Open_NewBill_When_OpenSale = True Then ResetNewBill()
            End If
            ModernLoader.CloseLoader()
        Catch ex As Exception
            ModernLoader.CloseLoader()
        End Try


    End Sub

    Public Sub Check_View_Control()
        AGMetroGrid.Columns("Date_CL").Visible = MY_Settings.S_Date_CL
        AGMetroGrid.Columns("ST_Name_CL").Visible = MY_Settings.S_ST_Name_CL
        AGMetroGrid.Columns("D_Valid_CL").Visible = MY_Settings.S_D_Valid_CL
        AGMetroGrid.Columns("IMUnit_CL").Visible = MY_Settings.S_IMUnit_CL
        AGMetroGrid.Columns("Price_CL").Visible = MY_Settings.S_Price_CL
        AGMetroGrid.Columns("Total_CL").Visible = MY_Settings.S_Total_CL
        AGMetroGrid.Columns("Notes_CL").Visible = MY_Settings.SP_Notes_CL
        AGMetroGrid.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL
        AGMetroGrid.Columns("Barcode_CL").Visible = MY_Settings.S_Barcode_CL
        AGMetroGrid.Columns("Serial_Code_CL").Visible = MY_Settings.S_Serial_Code_CL
        AGMetroGrid.Columns("IM_NOTE_CL").Visible = MY_Settings.S_IM_NOTE_CL
        AGMetroGrid.Columns("IM_Discount_CL").Visible = MY_Settings.S_IM_Discount_CL
        Markter_Cm.Visible = S_Marketers
        Marketer_Lb.Visible = S_Marketers

        IM_Check_Panel.Visible = SB_is_Check_IM
        AGMetroGrid.Columns("Check_CL").Visible = SB_is_Check_IM
        عرضربحالفاتورةToolStripMenuItem.Visible = U_Show_Bill_Profet
    End Sub

    Public Sub SB_Contents_SELECT_Bill()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Contents_SELECT_Bill_2"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        AGMetroGrid.DataSource = C.Dt
        If AGMetroGrid.Rows.Count > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(AGMetroGrid.Rows.Count - 1).Cells("EX_Name_CL")
    End Sub

    Public Sub Fill_Bill_Info()

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
            AG_Cm.Set_IM_By_ID(AG_ID)
            'GET_AG()
            AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "
            Fill_All_AG_Proj()
            Project_cm.SelectedValue = C.Dr("Proj_ID")

            DateTimeEx.Text = C.Dr("Date")
            BillNumTxt.Text = C.Dr("S_Bill_Pr_ID")
            Barcode = C.Dr("Barcode")

            If C.Dr("isPied") = 1 Then
                Save_butt.Enabled = False
            Else
                Save_butt.Enabled = True
            End If
            Bill_ID_Txt.Text = C.Dr("SB_ID")
            SB_ID = C.Dr("SB_ID")


            If C.Dr("Discount") > 0 Then
                Discount_txt1.Text = C.Dr("Discount")
                Disc = C.Dr("Discount")
                'If Discount_Distribute = False Then Pure_txt.Text = C.Dr("Total") - C.Dr("Discount")
                Old_Disc = Disc
            End If

            isVoid = C.Dr("isVoid")
            isDepended = C.Dr("isDepended")

            isPied = C.Dr("isPied")

            User_Name_lb.Text = C.Dr("U_Name") + " - " + C.Dr("Date").ToString
            BillUser_ID = C.Dr("User_ID")
            Notes_txt.Text = C.Dr("About")

            isPause = C.Dr("isPause")
            If isPause = False Then
                SBPauseBtn.Text = "تعليق F7"
            Else
                SBPauseBtn.Text = "إلغاء التعليق"
            End If
            Markter_Cm.Set_IM_By_ID(C.Dr("Markter_ID"))
            Pure = C.Dr("Total") - C.Dr("Discount")

            'OUT_SALE_Bill_TXT.Text = C.Dr("Travel_ID")

            Select_Sales_Receipt(T_ID)
        Else
            AG_ID = Default_AG_ID
            'AG_SH_txt.Text = "نقدي"
            Fetch_ItemToList2()
            VoidLb.Enabled = False
        End If
        C.Con.Close()
    End Sub

    Private Sub Enable_Fields()
        AG_Cm.Enabled = True
        ' Show_IM_btn2.Enabled = True
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        Project_cm.Enabled = True
        Show_AG_Projects_btn.Enabled = True
        Markter_Cm.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        AG_Cm.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        Project_cm.Enabled = False
        Show_AG_Projects_btn.Enabled = False
        Markter_Cm.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1
            AG_Cm.Enabled = False
            Save_butt.Enabled = False
        Else
            isDepended = 0
            AG_Cm.Enabled = True
            Save_butt.Enabled = True
        End If

    End Sub

    Public Sub NewStateBt()


        ' 4. تحديث حالة الثيم البصرية
        UpdateFormStateIndicator("فاتورة جديدة", System.Drawing.Color.Honeydew)
        ' 5. تفعيل الأزرار
        Save_butt.Enabled = True
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Enable_Fields()
    End Sub
    Private Sub DeleteOrUpdateStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Private Sub SavedStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub


    Public Sub SelectStateBt()
        Try
            ' 1. إعدادات الواجهة الأساسية
            Edit_butt.Text = EditState
            Me.Text = "فاتورة مبيعات "
            VoidLb.Visible = False

            ' 2. توجيه حالة الفاتورة (ملغاة - معلقة - مرحلة - قيد التعديل)
            If isVoid Then
                ' 🔴 --- حالة: فاتورة ملغاة ---
                DeletedBillLabel.Visible = True
                DeletedBillLabel.BackColor = Color.Red
                UpdateFormStateIndicator("فاتورة ملغاة", System.Drawing.Color.Red)
                Disable_Fields()
                Save_butt.Enabled = False
                Edit_butt.Enabled = False
                Delete_butt.Enabled = False
                Print_btn.Enabled = False
                DiscountPanel.Enabled = False
                DeliveryingButton.Enabled = False
                AGMetroGrid.Enabled = True

            ElseIf isDepended = False Then
                ' 🟡 --- حالة: فاتورة معلقة / غير مرحلة ---
                UpdateFormStateIndicator("فاتورة معلقة", System.Drawing.Color.Goldenrod)
                Enable_Fields()
                Save_butt.Enabled = True
                Edit_butt.Enabled = False
                Delete_butt.Enabled = True
                Print_btn.Enabled = False
                DiscountPanel.Enabled = True

            ElseIf isDepended = True AndAlso On_Update = False Then
                ' 🟣 --- حالة: فاتورة مُرحّلة / معتمدة ---
                UpdateFormStateIndicator("مُرحّلة / معتمدة", System.Drawing.Color.LimeGreen)
                Disable_Fields()
                Save_butt.Enabled = False
                Edit_butt.Enabled = True
                Delete_butt.Enabled = True
                Print_btn.Enabled = True
                DiscountPanel.Enabled = False
                DeliveryingButton.Enabled = True

            ElseIf isDepended = True AndAlso On_Update = True Then
                ' 🟠 --- حالة: فاتورة قيد التعديل ---
                UpdateFormStateIndicator("قيد التعديل", System.Drawing.Color.DarkOrange)
                Enable_Fields()
                ADDCatButton.Enabled = True
                RemoveCatButton.Enabled = True
                Notes_txt.Enabled = True
                DateTimeEx.Enabled = True
                Project_cm.Enabled = True
                Show_AG_Projects_btn.Enabled = True
                DiscountPanel.Enabled = True
                AG_Cm.Enabled = True
                Markter_Cm.Enabled = True
                Ebable_CatFields()
                Save_butt.Enabled = False
                Edit_butt.Enabled = True
                Delete_butt.Enabled = False
                Print_btn.Enabled = False
                DeliveryingButton.Enabled = False
                Edit_butt.Text = "إيقاف التعديل"
            End If

            ' 3. فحص صلاحيات المستخدم (تطبق فقط إذا لم تكن الفاتورة ملغاة)
            If Not isVoid AndAlso (U_Save_otherBill = False AndAlso BillUser_ID <> USER_ID) Then
                Save_butt.Enabled = False
                Edit_butt.Enabled = False
                Delete_butt.Enabled = False
                AGMetroGrid.Enabled = False
                Disable_CatFields()
                Disable_Fields()
            End If

            ' 4. تحديث البيانات النهائية للمكونات
            If AGMetroGrid.Rows.Count = 0 Then
                DateTimeEx.Value = Date.Now
            End If

            Pied_T_ID = Get_Reciept_ID()
            Get_Rtn_Count()

        Catch ex As Exception
            ' تفادي أي خطأ مفاجئ أثناء تحميل حالة الواجهة
        End Try
    End Sub

    Public Function Get_Reciept_ID()
        Dim c As New C
        Try
            Dim s As String
            s = "select T_ID from Receipts_V WHERE Receipt_Tran_ID = '" & T_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Return c.Dr("T_ID")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return 0
    End Function

    Public Sub Get_Rtn_Count()
        Dim c As New C
        Dim N As Double = 0
        Try
            Dim s As String
            s = "select ISNULL(SUM(T_PRICE),0) AS S from Rtn_Contents WHERE Orginal_Bill_T_ID = '" & T_ID & "' AND isDepended=1"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                N = c.Dr("S")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Rtn_Count_txt.Text = N
    End Sub

    Private Sub ClearFields()
        isCashReceipt_Success = False
        T_ID = 0
        Pied_T_ID = 0
        Notes_txt.Clear()
        Total_TextBox1.Clear()
        Receipts_DT.Clear()
        DateTimeEx.Text = Date.Now
        VoidLb.Visible = False
        isVoid = False
        isDepended = False
        Discount_txt1.Clear()
        Disc = 0
        Me.Text = FormState
        Edit_butt.Text = EditState
        On_Update = False
        SB_ID = 0
        Project_cm.SelectedIndex = -1
        Markter_Cm.Set_IM_By_ID(1)
    End Sub

    Private Sub ResetNewBill()
        Load_PauseBills()
        ClearFields()
        Call_New_Bill()
        NewStateBt()
    End Sub


    Public Sub Call_New_Bill()
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
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
            .Parameters.AddWithValue("@isPied", 0)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters("@T_ID").Direction = ParameterDirection.Output
            .Parameters("@Bill_Num").Direction = ParameterDirection.Output
            .Parameters("@isNew").Direction = ParameterDirection.Output
            .Parameters("@SB_ID").Direction = ParameterDirection.Output
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Me.T_ID = C.Com.Parameters("@T_ID").Value
            Bill_ID_Txt.Text = C.Com.Parameters("@SB_ID").Value.ToString()
            BillNumTxt.Text = C.Com.Parameters("@Bill_Num").Value.ToString()
            isNewBill = C.Com.Parameters("@isNew").Value
            SB_Contents_SELECT_Bill()
            Fill_Bill_Info()
            SelectStateBt()
        End If
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click

        If AGMetroGrid.Rows.Count > 0 Then

            If AG_ID = 0 Then
                Beep()
                MsgBox("حدد إسم العميل", MsgBoxStyle.Critical, "خطأ في الإعتماد")
                AG_Cm.Focus()
            Else
                If String.IsNullOrWhiteSpace(AG_Cm.Textt) Then
                    AG_ID = Default_AG_ID
                    AG_Cm.Set_IM_By_ID(AG_ID)
                End If
                Beep()
                Save_AG_Name(T_ID, AG_ID, On_Update)
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Save_Pro()
                ConfermBill()
                AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "
            End If
            isCashReceipt_Success = False
        End If

    End Sub

    Public Sub ConfermBill()

        Dim F As New Pay_Main_Form
        F.Temp_Tr_ID = SB_TR_ID
        F.AG_ID = AG_ID
        F.MONEY_VALUE = Pure
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
                .Parameters.AddWithValue("@Discount", Disc)
                .Parameters.AddWithValue("@Pure", Pure)
                If AG_ID <> Default_AG_ID Then .Parameters.AddWithValue("@Pied", Piedmoney_txt.Text)
                .Parameters.AddWithValue("@AGType_ID", 1)
                .Parameters.AddWithValue("@Tr_ID", Tr_ID) 'SB_TR_ID
                .Parameters.AddWithValue("@Pr_ID", Pr_ID)
                .Parameters.AddWithValue("@User_ID", USER_ID)
                .Parameters.AddWithValue("@Pay_ID", Pay_ID)
            End With
            If SQL_SP_EXEC(c.Com) = True Then
                Switch_Dependcy(1)
                If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
                If SB_AutoPrint = True Then
                    Me.Cursor = Cursors.AppStarting
                    CashPrint(Sales_BillPage_Bill_Track, Sales_Page_ID)
                    Me.Cursor = Cursors.Default
                End If
                SelectStateBt()
                Select_Sales_Receipt(T_ID)
                If MY_Settings.S_OpenNextBill = True Then Call_New_Bill()
                ' SELECT_IM()
            End If


        End If

    End Sub


    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click

        Beep()
        If MessageBox.Show(" سيتم إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "إلغــاء فاتورة", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Cancel_Bill()
        End If
    End Sub

    Private Sub Cancel_Bill()

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Void_Row"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)

        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم إلغاء الفاتورة", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert("إلغاء الفاتورة", Bill_ID_Txt.Text, 1, 3)
            isVoid = True
            SelectStateBt()
        End If

    End Sub

    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Total_TextBox1.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Public Sub Calc_Total()
        TOTAL = 0
        TOTAL_Check = 0

        If String.IsNullOrWhiteSpace(Discount_txt1.Text) Then Disc = 0
        Dim QTY As Double = 0
        For i = 0 To AGMetroGrid.Rows.Count - 1
            TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("Total_CL").Value
            QTY += AGMetroGrid.Rows(i).Cells("QTY_CL").Value

            If AGMetroGrid.Rows(i).Cells("Check_CL").Value = True Then TOTAL_Check += AGMetroGrid.Rows(i).Cells("Total_CL").Value
        Next

        Total_TextBox1.Text = TOTAL.ToString(N_Point_Fter)

        If Discount_Distribute = False Then
            Pure = (TOTAL - Disc)
            TOTAL_Check = (TOTAL_Check - Disc)
        Else
            Pure = TOTAL
        End If

        Pure_txt.Text = Pure.ToString(N_Point_Fter)
        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "
        Check_Compute_txt.Text = TOTAL_Check.ToString(N_Point_Fter)

    End Sub


    Private Sub SB_Contents_Delete_IM(T_ID_CL As Integer)
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Contents_Delete_IM"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID_CL)
            .Parameters.AddWithValue("@On_Update", On_Update)
        End With
        If SQL_SP_EXEC(c.Com) = True Then

            Network_Edit_Tracker_insert(" الصنف:" + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString + " الوحدة:" + AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value.ToString + " العدد:" + AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString _
                                        + " السعر:" + AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 1, 2)

            SB_Contents_SELECT_Bill()
            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")

        End If
    End Sub


    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        If AGMetroGrid.Rows.Count > 0 Then
            If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                SB_Contents_Delete_IM(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            End If
        End If
    End Sub

    Private Sub ADD_Fast_AG()
        If GET_AG_NO_SPACES(AG_Cm.Textt) = True Then 'AG_Cm.TXT_ID.Text <> 0 Or
            MsgBox("هذا العميل موجود بالفعل", MsgBoxStyle.Critical, "إضافة عميل")
        ElseIf String.IsNullOrWhiteSpace(AG_Cm.Textt) Then
            MsgBox("أدخل اسم العميل الجديد", MsgBoxStyle.Exclamation)
            AG_Cm.Focus()
        Else
            Beep()
            If MessageBox.Show(" إضافة " + AG_Cm.Textt + " إلى قائمة العملاء ", " إضافة العميل ", MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Insert_Fast_AG()
            End If
        End If
    End Sub

    Private Function Insert_Fast_AG()
        Dim New_AG_ID As Integer = 0
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Agents_insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@AG_ID", 0)
        sqlComm.Parameters.AddWithValue("@Ag_name", AG_Cm.Textt)
        sqlComm.Parameters.AddWithValue("@Barcode", "")
        sqlComm.Parameters.AddWithValue("@Type_ID", Cr_Type_ID)
        sqlComm.Parameters.AddWithValue("@E_mail", "")
        sqlComm.Parameters("@AG_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تمت إضافة العميــل", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" (من شاشة المبيعات) الزبون:" & AG_Cm.Textt, 0, 27, 1)
            New_AG_ID = sqlComm.Parameters("@AG_ID").Value.ToString()

        End If
        Return New_AG_ID
    End Function


    Public Sub Fetch_ItemToList2()
        If AG_Cm.TXT_ID.Text > 0 Then

            PREV_AG_ID = AG_ID
            AG_ID = AG_Cm.TXT_ID.Text

            If U_AG_Skip_Max = False Then
                If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then
                    MsgBox("عذرا ... هذا الزبون قد تخطى سقف الدين الخاص به ولا يمكنك فتح فاتورة جديدة له", MsgBoxStyle.Critical, "خطأ فالإدراج")
                    AG_ID = PREV_AG_ID
                Else
                    Save_AG_Name(T_ID, AG_ID, On_Update)
                    Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_Cm.Textt, SB_ID, 1, 3)
                    Fill_All_AG_Proj()
                End If
            Else
                If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then MsgBox("هذا الزبون قد تخطى سقف الدين الخاص به", MsgBoxStyle.Exclamation, "تنويه ")
                Save_AG_Name(T_ID, AG_ID, On_Update)
                Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_Cm.Textt, SB_ID, 1, 3)

                Fill_All_AG_Proj()
            End If

        End If
    End Sub

    Public Sub Fill_All_AG_Proj()
        Try
            Dim C As New C
            Dim s As String = "SELECT Proj_ID,Proj_NAME from AG_Projects_V WHERE AG_ID = '" & F_Sales.AG_ID & "' ORDER BY Proj_ID DESC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(C.Dt)
            Project_cm.DataSource = C.Dt
            Project_cm.ValueMember = "Proj_ID"
            Project_cm.DisplayMember = "Proj_NAME"
            Project_cm.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Save_Pro()
        ' If Project_cm.SelectedValue <= 0 Then Project_cm.SelectedValue = 1

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_AG_Project"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Proj_ID", Project_cm.SelectedValue)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Private Sub Discount_txt1_KeyDown(sender As Object, e As KeyEventArgs) Handles Discount_txt1.KeyDown

        If Not String.IsNullOrWhiteSpace(Discount_txt1.Text) Then
            Discount_calc()
        End If
    End Sub


    Private Sub Discount_txt1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Discount_txt1.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Discount_txt1_KeyUp(sender As Object, e As KeyEventArgs) Handles Discount_txt1.KeyUp
        If String.IsNullOrWhiteSpace(Discount_txt1.Text) Then
            Disc = 0
            Pure_txt.Text = TOTAL
        Else
            Discount_calc()
        End If
    End Sub

    Private Sub Discount_txt1_TextChanged(sender As Object, e As EventArgs) Handles Discount_txt1.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Bill_ID_Txt.Text = Convert.ToInt64(Bill_ID_Txt.Text) - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()


        Dim C As New C
        Dim S As String = ""
        If Search_By_Bar_CB.Checked = True Then
            S = "Select T_ID From Agents_Balance_MV Where Barcode = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
        Else
            S = "Select T_ID From Agents_Balance_MV Where SB_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
        End If

        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Fill_Bill_Info()
                SB_Contents_SELECT_Bill()
                SelectStateBt()
            Else
                MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                If Search_By_Bar_CB.Checked = True Then
                    Bill_ID_Txt.Clear()
                Else
                    Bill_ID_Txt.Text = SB_ID
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        If Not String.IsNullOrWhiteSpace(Bill_ID_Txt.Text) Then
            '  Tmp_Bill_ID = Convert.ToInt64(Bill_ID_Txt.Text)
            Bill_ID_Txt.Text = Convert.ToInt64(Bill_ID_Txt.Text) + 1
            Get_T_ID()
        End If
    End Sub

    Private Sub Bill_ID_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Bill_ID_Txt.KeyDown
        If e.KeyCode = Keys.Return Then Get_T_ID()
        If e.KeyCode = Keys.Up Then Up_Bill_btn_Click(sender, e)
        If e.KeyCode = Keys.Down Then Down_Bill_btn_Click(sender, e)
    End Sub

    Private Sub Bill_ID_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_ID_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub


    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        'ADD_IM()

        F_SB_IM_card = New SB_IM_card
        F_SB_IM_card.T_ID = T_ID
        F_SB_IM_card.AG_ID = AG_ID
        F_SB_IM_card.ShowDialog()

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
            '.AG_Type_cm.Visible = False
            .MetroTabControl1.TabPages.Remove(.MetroTabPage4)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage5)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage6)
            .MenuStrip1.Visible = False
        End With
        F_Balances.ShowDialog()
    End Sub


    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        If AGMetroGrid.Rows.Count > 0 Then
            Me.Cursor = Cursors.AppStarting
            CashPrint(Sales_BillPage_Bill_Track, Sales_Page_ID)
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Public Function CashPrint(Sales_BillPage_Bill_Track As String, Sales_Page_ID As Integer)
        Dim Balance_Notes As String = ""
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & Sales_BillPage_Bill_Track)
        pp.LoadTables()
        With pp

            Select Case Sales_Page_ID
                Case 9

                    .rp.SetParameterValue(0, BillNumTxt.Text)
                    .rp.SetParameterValue(1, USER_NAME)
                    .rp.SetParameterValue(2, Pure_txt.Text)
                    .rp.SetParameterValue(3, "")
                    .rp.SetParameterValue(4, Me.T_ID)
                    .rp.SetParameterValue(5, SBill_Title_1)
                    .rp.SetParameterValue(6, SBill_Title_2)
                    .rp.SetParameterValue(7, SBill_Footer)
                    .rp.SetParameterValue(8, "*" + Barcode + "*")

                Case Else

                    .rp.SetParameterValue(0, Me.T_ID)
                    .rp.SetParameterValue(1, SBill_Title_1)
                    .rp.SetParameterValue(2, SBill_Title_2)
                    .rp.SetParameterValue(3, SBill_Footer)
                    .rp.SetParameterValue(4, IM_Qty_LB.Text)
                    .rp.SetParameterValue(5, IM_Count_LB.Text)



                    CALCE_REST_AG_BALANCE(Balance_Notes, "N")

                    If Sales_Page_ID = 2 Or Sales_Page_ID = 8 Then

                        .rp.SetParameterValue(6, "*" + Barcode + "*")
                        .rp.SetParameterValue(7, SB_ID)
                        .rp.SetParameterValue(8, Pure - Convert.ToDouble(Piedmoney_txt.Text))

                        .rp.SetParameterValue(9, Balance_Notes)

                    Else

                        If Sales_Page_ID = 5 Then
                            Balance_Notes = ""
                            CALCE_REST_AG_BALANCE(Balance_Notes, " , ")
                            .rp.SetParameterValue(8, Balance_Notes & vbNewLine & Notes_txt.Text)
                        Else
                            .rp.SetParameterValue(8, Notes_txt.Text)
                        End If

                        .rp.SetParameterValue(6, HANY(Val(Pure), "EGYPT"))
                        .rp.SetParameterValue(7, "*" + Barcode + "*")


                        If SB_is_Check_Thankes = True And (Convert.ToDouble(Piedmoney_txt.Text) = Convert.ToDouble(Pure_txt.Text)) Then
                            .rp.SetParameterValue(9, "خالــــص مع الشكر")
                        Else
                            .rp.SetParameterValue(9, "")
                        End If

                        If Project_cm.SelectedValue <= 1 Then
                            .rp.SetParameterValue(10, "")
                        Else
                            .rp.SetParameterValue(10, " / " + Project_cm.Text)
                        End If

                        .rp.SetParameterValue(11, Pure - Convert.ToDouble(Piedmoney_txt.Text))
                        .rp.SetParameterValue(12, Piedmoney_txt.Text)

                        If is_Order_IM_Print = False Then

                            .rp.SetParameterValue(13, Balance_Notes)
                        End If

                    End If

            End Select


        End With

        If Sales_Page_ID <> 2 Then
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        Else
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_80))
            pp.rp.PrintOptions.PrinterName = Default_Printer_80
        End If

        If Show_Bill_CB.Checked = False Then
            pp.rp.PrintToPrinter(1, False, 0, 0)
            pp.rp.Dispose()
        Else
            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.ShowDialog()
        End If

        Return pp
    End Function

    Private Sub CALCE_REST_AG_BALANCE(ByRef Balance_Notes As String, Dot As String)

        Dim BALANCE As Double = GET_AG_Balance()


        If Dot = "N" Then
            If Show_SumPied_CB.Checked = True Then Balance_Notes += " المدفوع : " & Piedmoney_txt.Text & vbNewLine
            If Show_Bill_Rest_CB.Checked = True And AG_ID <> Default_AG_ID Then Balance_Notes += " باقي للفاتورة : " & (Pure - Convert.ToDouble(Piedmoney_txt.Text)).ToString() & vbNewLine
            If AG_Show_Balance_CB.Checked = True And AG_ID <> Default_AG_ID Then
                If Convert.ToDouble(BALANCE) < 0 Then
                    Balance_Notes += " باقي للحساب : " & (BALANCE.ToString) & vbNewLine
                ElseIf Convert.ToDouble(BALANCE) > 0 Then
                    Balance_Notes += " باقي على الحساب : " & (BALANCE.ToString) & vbNewLine
                Else
                    Balance_Notes += " الرصيد : " & Convert.ToDouble(BALANCE.ToString) & vbNewLine
                End If
            End If
        Else

            If Show_SumPied_CB.Checked = True Then Balance_Notes += " المدفوع : " & Piedmoney_txt.Text & " , "
            If Show_Bill_Rest_CB.Checked = True And AG_ID <> Default_AG_ID Then Balance_Notes += " باقي للفاتورة : " & (Pure - Convert.ToDouble(Piedmoney_txt.Text)).ToString() & " , "
            If AG_Show_Balance_CB.Checked = True And AG_ID <> Default_AG_ID Then
                If Convert.ToDouble(BALANCE) < 0 Then
                    Balance_Notes += " باقي للحساب : " & (BALANCE.ToString) & " , "
                ElseIf Convert.ToDouble(BALANCE) > 0 Then
                    Balance_Notes += " باقي على الحساب : " & (BALANCE.ToString) & " , "
                Else
                    Balance_Notes += " الرصيد : " & Convert.ToDouble(BALANCE.ToString) & " , "
                End If
            End If
        End If

    End Sub

    Public Function GET_AG_Balance()
        Return Show_AG_T_Balance(AG_ID)
    End Function


    Private Sub SBPauseBtn_Click(sender As Object, e As EventArgs) Handles SBPauseBtn.Click

        If isPause = True Then
            If isDepended = True Then
                Beep()
                If MessageBox.Show(" إلغاء تعليق الفاتورة " + Bill_ID_Txt.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) _
                 = Windows.Forms.DialogResult.OK Then
                    SB_Cancel_PauseBill()
                End If
            Else
                MsgBox("لا يمكن إلغاء تعليق فاتورة غير محفوظة", MsgBoxStyle.Exclamation, "")
            End If
        Else
            Beep()
            If MessageBox.Show(" تعليق الفاتورة " + Bill_ID_Txt.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) _
                 = Windows.Forms.DialogResult.OK Then
                SB_PauseBill()
            End If
        End If

    End Sub

    Private Sub SB_Cancel_PauseBill()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Cancel_PauseBill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            ResetNewBill()
        End If
    End Sub

    Private Sub SB_PauseBill()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_PauseBill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            ResetNewBill()
        End If
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        If On_Update = True Then Edit_butt_Click(sender, e)
        ResetNewBill()
        UpdateFormStateIndicator("فاتورة جديدة", System.Drawing.Color.Honeydew)
    End Sub

    Private Sub Load_PauseBills()
        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "SB_PauseBill_SelectList_2"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
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


    Private Sub MoveToBill_Btn_Click(sender As Object, e As EventArgs) Handles MoveToBill_Btn.Click
        If PauseCmb.SelectedIndex > -1 Then
            Me.Enabled = False
            ClearFields()
            T_ID = PauseCmb.SelectedValue
            SB_Contents_SELECT_Bill()
            Fill_Bill_Info()
            SelectStateBt()
            Me.Enabled = True
        End If
    End Sub

    Private Sub DeliveryingButton_Click(sender As Object, e As EventArgs) Handles DeliveryingButton.Click
        If isDepended = True Then
            FormType = 1
            AG_Type = 3
            F_Receipt = New Receipt
            Receipt_Tran_ID = T_ID

            With F_Receipt
                Rct_Tr_ID = SB_TR_ID
                .Fields_Panel.Enabled = True
                .AG_Cm.Enabled = False
                .Barcode_SH_txt.Enabled = False
                .Receipt_Title_combobox.Text = "فاتورة مبيعات : " + Bill_ID_Txt.Text
                .AG_ID = AG_ID
                .money_num_txtb.Text = Pure - Convert.ToDouble(Piedmoney_txt.Text)
            End With

            isShowing_Trans = False
            F_Receipt.ShowDialog()
            AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "
        Else
            MsgBox("يجب إعتماد الفاتورة أولا", MsgBoxStyle.Exclamation, "")
        End If

    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If T_ID > 0 Then
            If On_Update = False Then
                Beep()
                If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟", "تعديل فاتورة", MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then

                    On_Update = True
                    SelectStateBt() ' استدعاء دالة الحالات لتجهيز الفورم للتعديل
                End If

            Else
                Save_Total(T_ID, TOTAL, Disc)
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Save_Pro()
                Begin_Discount()
                AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "

                On_Update = False
                SelectStateBt() ' استدعاء دالة الحالات لترجيع الفورم لوضع القفل
                Select_Sales_Receipt(T_ID)
            End If
        End If
    End Sub

    Sub Close_Sale()
        Update_Discount(T_ID, Discount_txt1.Text)
        Network_Edit_Tracker_insert(" تخفيض للفاتورة بقيمة:" & Disc.ToString, Bill_ID_Txt.Text, 1, 3)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        FormType = 1
        Switch_To_DV_Show()
    End Sub

    Private Sub تعديلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلToolStripMenuItem.Click
        Mov_To_Edit_IM()
    End Sub

    Private Sub عرضالتكلفةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضالتكلفةToolStripMenuItem.Click
        Show_IM_Cost(True, F_Sales.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, F_Sales.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value)
    End Sub


    Private Sub Search_By_Bar_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Search_By_Bar_CB.CheckedChanged
        CB_CHecked(sender)
        Bill_ID_Txt.Clear()
        Bill_ID_Txt.Select()
    End Sub

    Private Sub OpenCahDR_Btn_Click(sender As Object, e As EventArgs) Handles OpenCahDR_Btn.Click
        Open_Cash_Drawer()
    End Sub

    Private Sub Show_Cash_btn_Click(sender As Object, e As EventArgs) Handles Show_Cash_btn.Click
        Fetch_Pr_Details_()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub إضافةكعميلجديدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إضافةكعميلجديدToolStripMenuItem.Click
        ADD_Fast_AG()
    End Sub

    Private Sub كشفحسابالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles كشفحسابالعميلToolStripMenuItem.Click
        Show_AG_Balance()
    End Sub

    Private Sub عرضرصيدالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضرصيدالعميلToolStripMenuItem.Click
        MsgBox(Show_AG_T_Balance(AG_ID).ToString(), MsgBoxStyle.Information, "رصيد العميل : " & AG_Cm.Textt)
    End Sub


    Private Sub إرسالالفاتورةللبريدالإلكترونيToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إرسالالفاتورةللبريدالإلكترونيToolStripMenuItem.Click
        Type_Of_E_mail = 1
        Me.Cursor = Cursors.AppStarting
        SendingOptions.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Show_IM_Rtn_btn_Click(sender As Object, e As EventArgs) Handles Show_IM_Rtn_btn.Click
        Show_IM_Rtn.ShowDialog()
    End Sub


    Private Sub Calc_Dicount_Btn_Click(sender As Object, e As EventArgs) Handles Calc_Dicount_Btn.Click
        Begin_Discount()
    End Sub

    Public Sub Discount_calc()
        Disc = Convert.ToDouble(Discount_txt1.Text)
        Pure_txt.Text = TOTAL - Disc
        Pure = TOTAL - Disc
    End Sub

    Private Sub Begin_Discount()
        If String.IsNullOrWhiteSpace(Discount_txt1.Text) Then Discount_txt1.Text = "0"
        Discount_calc()
        Close_Sale()
    End Sub

    Private Sub طباعةورقةA5ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةورقةA5ToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A5.rpt", 3)
    End Sub

    Private Sub طباعةورقA4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةورقA4ToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A4.rpt", 1)
    End Sub

    Private Sub طباعةورقةA4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةورقةA4ToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A4_2.rpt", 4)
    End Sub

    Private Sub ورقةA4تصميمجاهزToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ورقةA4تصميمجاهزToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A4_3.rpt", 5)
    End Sub


    Private Sub طباعةإذنصـــرفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةإذنصـــرفToolStripMenuItem.Click
        is_Order_IM_Print = True
        CashPrint("\reports\Costmer_SB_Resive_Order_A5.rpt", 5)
        is_Order_IM_Print = False
    End Sub

    Private Sub Project_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles Project_cm.KeyDown
        If e.KeyCode = Keys.Return Then Save_Pro()
    End Sub

    Private Sub Show_AG_Projects_btn_Click(sender As Object, e As EventArgs) Handles Show_AG_Projects_btn.Click
        AG_Projects.ShowDialog()
    End Sub

    Private Sub ReceiptsMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles ReceiptsMetroGrid.RowsAdded
        Calc_Credit()
    End Sub

    Private Sub Calc_Credit()
        Dim Sum As Double = 0
        For i = 0 To ReceiptsMetroGrid.Rows.Count - 1
            Sum = Sum + ReceiptsMetroGrid.Rows(i).Cells("Value_CL").Value
        Next
        Piedmoney_txt.Text = Sum.ToString("n")
    End Sub

    Private Sub ReceiptsMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles ReceiptsMetroGrid.RowsRemoved
        Calc_Credit()
    End Sub

    Private Sub ReceiptsMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ReceiptsMetroGrid.MouseDoubleClick
        If ReceiptsMetroGrid.Rows.Count > 0 Then
            AG_Type = 3
            IS_Show_Rctp = True
            F_Receipt = New Receipt
            F_Receipt.ShowDialog()
            IS_Show_Rctp = False
        End If
    End Sub


    Private Sub عرضفواتيرالزبونToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضفواتيرالزبونToolStripMenuItem.Click
        PchSearch.ShowDialog()
    End Sub

    Private Sub AG_Show_Balance_CB_CheckedChanged(sender As Object, e As EventArgs) Handles AG_Show_Balance_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.SB_AG_Show_Balance = AG_Show_Balance_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If IM_ID > 0 Then
            Show_IM_Details.IM_ID = IM_ID
            Show_IM_Details.ShowDialog()
        End If
    End Sub

    Private Sub طباعةورقةA4تصميمجاهز2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةورقةA4تصميمجاهز2ToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A4_4.rpt", 5)
    End Sub


    Private Sub DateTimeEx_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeEx.KeyDown
        If e.KeyCode = Keys.Return Then Save_Date(T_ID, DateTimeEx)
    End Sub

    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        If AG_Cm.TXT_ID.Text > 0 Then
            Fetch_ItemToList2()
            AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "
        End If
    End Sub

    Private Sub Show_Bill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Bill_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.SB_Show_Bill = Show_Bill_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub طباعةورقA4ملاحظاتالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةورقA4ملاحظاتالصنفToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_With_Notes_A4.rpt", 7)
    End Sub

    Private Sub Show_Billl_Rest_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Bill_Rest_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.SB_Show_Bill_Rest = Show_Bill_Rest_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub Clear_Check_btn_Click(sender As Object, e As EventArgs) Handles Clear_Check_btn.Click
        SB_Contents_Uncheck_IM()
    End Sub


    Private Sub SB_Contents_Uncheck_IM()

        Dim C As New C

        For Each row As DataGridViewRow In AGMetroGrid.Rows
            If row.Cells("Check_CL").Value = True Then
                C = New C
                With (C.Com)
                    .Connection = C.Con
                    .CommandText = "SB_Contents_Uncheck_IM"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.AddWithValue("@T_ID", row.Cells("T_ID_CL").Value)
                End With
                SQL_SP_EXEC(C.Com)

            End If
        Next

        SB_Contents_SELECT_Bill()

    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        ' استدعاء الدالة البرمجية مباشرة
        Mov_To_Edit_IM()
    End Sub

    Private Sub Mov_To_Edit_IM()
        ' تحديد نوع النموذج للمبيعات (1)
        FormType = 1

        ' التحقق برمجياً: السماح بالتعديل إذا كانت الفاتورة (جديدة/معلقة) أو (قيد التعديل) وليست (ملغاة)
        If (isDepended = False OrElse On_Update = True) AndAlso isVoid = False AndAlso AGMetroGrid.Rows.Count > 0 Then
            Change_IM_Details.ShowDialog()
        End If
    End Sub
    Private Sub AGMetroGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles AGMetroGrid.KeyDown

        If e.KeyCode = Keys.Delete Then
            ' التأكد من حالة الفاتورة بدلاً من الاعتماد على لون الجريد
            If AGMetroGrid.Rows.Count > 0 AndAlso (isDepended = False OrElse On_Update = True) AndAlso isVoid = False Then
                If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    SB_Contents_Delete_IM(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
                End If
            End If
        End If

    End Sub

    Private Sub عرضربحالفاتورةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضربحالفاتورةToolStripMenuItem.Click
        Bill_Perfet_Select_For_Bill(T_ID)
    End Sub

    Private Sub Show_SumPied_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_SumPied_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.SB_Show_SumPied = Show_SumPied_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub Markter_Cm_ID_Changed(sender As Object, e As EventArgs) Handles Markter_Cm.ID_Changed
        AG_Balance_Update_Marketer(T_ID, Markter_Cm.TXT_ID.Text)
    End Sub

    Private Sub تخفيضبنسبةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تخفيضبنسبةToolStripMenuItem.Click
        Dim F_Percent_Disc As New Percent_Disc
        F_Percent_Disc.T_ID = T_ID
        F_Percent_Disc.TOTAL = TOTAL
        F_Percent_Disc.ShowDialog()
        Fill_Bill_Info()
    End Sub

    Private Sub عرضأخرمبيعاتللصنفبالنسبةللزبونToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضأخرمبيعاتللصنفبالنسبةللزبونToolStripMenuItem.Click
        Show_AG_IM_SALES.ShowDialog()
    End Sub

    Private Sub VoidLb_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DeletedBillLabel.MouseClick
        If U_SalesVoid = True Then

            Beep()
            If MessageBox.Show(" سيتم تراجع عن إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "تاكيد العملية", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                AG_Balance_UN_Void_Row(T_ID, SB_ID, 1)
                Get_T_ID()
            End If

        End If
    End Sub

    Private Sub Pure_txt_TextChanged(sender As Object, e As EventArgs) Handles Pure_txt.TextChanged
        If is_Use_Total_Port = True Then Show_Total_Port(Pure)
    End Sub

    Private Sub AGMetroGrid_DataSourceChanged(sender As Object, e As EventArgs) Handles AGMetroGrid.DataSourceChanged
        Calc_Total()
    End Sub

    Private Sub إيجارالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إيجارالصنفToolStripMenuItem.Click
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then
            Dim F As New Rsv_IM
            F.ShowDialog()
        End If
       
    End Sub

    Private Sub طباعةإذنصـــرفA4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةإذنصـــرفA4ToolStripMenuItem.Click
        is_Order_IM_Print = True
        CashPrint("\reports\Costmer_SB_Resive_Order_A4.rpt", 5)
        is_Order_IM_Print = False
    End Sub


    Private Sub طباعةالتسليموالإستلامToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةالتسليموالإستلامToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A5_IM_IN_OUT_QTY.rpt", 18)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        CashPrint("\reports\Costmer_SB_A5_IM_IN_OUT_QTY.rpt", 18)
    End Sub

    Private Sub تحديدنوعالطباعـــةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تحديدنوعالطباعـــةToolStripMenuItem.Click
        Dim dialog As New SingleChoiceDialog()
        dialog.Text = "حدد نوع الطباعة"
        ' أضف الخيارات إلى ListBox
        'dialog.ListBox1.Items.AddRange(New String() {"Option 1", "Option 2", "Option 3"})

        Dim c2 As New C
        c2.Str = "select ID,Type from Sales_Bill_Page"
        c2.Da = New SqlClient.SqlDataAdapter(c2.Str, c2.Con)
        c2.Da.Fill(c2.Dt)
        dialog.ListBox1.DataSource = c2.Dt
        dialog.ListBox1.DisplayMember = "Type"
        dialog.ListBox1.ValueMember = "ID"
        dialog.ListBox1.SelectedValue = Sales_Page_ID


        ' عرض مربع الحوار
        If dialog.ShowDialog() = DialogResult.OK Then
            CashPrint(get_prt_path(dialog.ListBox1.SelectedValue), dialog.ListBox1.SelectedValue)
        End If
    End Sub


    Public Function get_prt_path(Sales_Page_ID As Integer)

        Dim C As New C
        Dim S As String = ""
        Dim result As String = ""

        S = "Select AG_Bill From Sales_Bill_Page Where ID = " & Sales_Page_ID
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                result = C.Dr("AG_Bill")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return result
    End Function

    Private Sub جدولحجزالخدمةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles جدولحجزالخدمةToolStripMenuItem.Click
        CalendarForm.IM_T_ID = Me.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
        CalendarForm.IM_NAME = Me.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
        CalendarForm.IM_ID = Me.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value
        CalendarForm.ShowDialog()
    End Sub

    Private Sub إدراجموظفللخدمةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إدراجموظفللخدمةToolStripMenuItem.Click
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then
            Dim F As New SB_Contents_AGENTS

            F.SB_T_ID = Me.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
            F.IM_NAME = Me.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value

            F.ShowDialog()
        End If
    End Sub

    Private Sub SetupAnchors()
        ' 1. إيقاف التحجيم التلقائي وتفعيل الرسم المزدوج لمنع الوميض
        Me.AutoScaleMode = AutoScaleMode.None
        Me.DoubleBuffered = True

        ' ==========================================
        ' 🌟 2. الجريد الرئيسي والملاحظات (تمدد ديناميكي)
        ' ==========================================
        If AGMetroGrid IsNot Nothing Then AGMetroGrid.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        If Notes_txt IsNot Nothing Then Notes_txt.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        If Label27 IsNot Nothing Then Label27.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right ' كلمة "ملاحظة :"

        ' ==========================================
        ' 🌟 3. الأجزاء السفلية (الإجماليات، الخصم، الدفع، الإيصالات)
        ' ==========================================
        ' جهة اليسار (الإجماليات والخصم)
        If DiscountPanel IsNot Nothing Then DiscountPanel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        If Panel16 IsNot Nothing Then Panel16.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left ' خيارات العرض (مدفوع، باقي، ديون)

        ' جهة اليمين (الدفع، الإيصالات، والأزرار السفلية)
        If ReceiptsMetroGrid IsNot Nothing Then ReceiptsMetroGrid.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        If Panel13 IsNot Nothing Then Panel13.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right ' المدفوع
        If Show_Cash_btn IsNot Nothing Then Show_Cash_btn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        If OpenCahDR_Btn IsNot Nothing Then OpenCahDR_Btn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        If DeliveryingButton IsNot Nothing Then DeliveryingButton.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right

        ' شريط المعلومات السفلي (اسم المستخدم، عدد الأصناف، الخ)
        If User_Name_lb IsNot Nothing Then User_Name_lb.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        If IM_Count_LB IsNot Nothing Then IM_Count_LB.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        If IM_Qty_LB IsNot Nothing Then IM_Qty_LB.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        If Label16 IsNot Nothing Then Label16.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right

        ' ==========================================
        ' 🌟 4. الأجزاء العلوية (أزرار التحكم، بيانات الفاتورة والعميل)
        ' ==========================================
        ' شريط الأزرار الأساسي (حفظ، طباعة، إلغاء، جديد) - يتمدد بالعرض أو يبقى أعلى يسار
        If Panel1 IsNot Nothing Then Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        ' بيانات العميل والفاتورة (أعلى اليمين)
        If BillNumPanel IsNot Nothing Then BillNumPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If Panel10 IsNot Nothing Then Panel10.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' التاريخ والرقم اليومي
        If Panel2 IsNot Nothing Then Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' الزبون 2 والمشروع
        If Panel14 IsNot Nothing Then Panel14.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' الفواتير المعلقة
        If AG_Cm IsNot Nothing Then AG_Cm.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' كومبو بوكس الزبون
        If AG_Label IsNot Nothing Then AG_Label.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' رصيد الحساب
        If Label24 IsNot Nothing Then Label24.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' كلمة "الزبون :"

        ' أزرار التحكم بالجريد (يمين الجريد)
        If DGV_Control_btn IsNot Nothing Then DGV_Control_btn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If ADDCatButton IsNot Nothing Then ADDCatButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If RemoveCatButton IsNot Nothing Then RemoveCatButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' بيانات إضافية (أعلى اليسار)
        If IM_Check_Panel IsNot Nothing Then IM_Check_Panel.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        If Markter_Cm IsNot Nothing Then Markter_Cm.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        If Marketer_Lb IsNot Nothing Then Marketer_Lb.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub
End Class