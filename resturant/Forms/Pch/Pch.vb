Imports System.Drawing
Imports System.Windows.Forms

Public Class Pch : Inherits System.Windows.Forms.Form
    'Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Public Receipts_DT As New DataTable
    Dim Indx_ID As Integer
    Public isShowingDetails As Boolean = False
    Public TOTAL As Double = 0
    Dim TOTAL_NO_EXP As Double
    Public AG_ID As Integer = 0
    Public Bill_DT As New DataTable
    Public Exp_DT As New DataTable
    Public On_Update As Boolean
    Public Pch_ID As Integer
    Public Disc As Double = 0
    Public Pure As Double = 0
    Public PchExpWithBillTotal As Double = 0
    Public PchExpWithoutBillTotal As Double = 0
    Public PchTotalWithBillExpenses As Double = 0
    Public PchTotalWithoutBillExpenses As Double = 0

    Dim is_Select_Mode = False

    ' =========================================================
    ' 🌟 دالة التحكم في مؤشر حالة الفاتورة البصري (ثابت بدون وميض)
    ' =========================================================
    Private Sub UpdateFormStateIndicator(ByVal StateText As String, ByVal StateColor As System.Drawing.Color)
        If lblFormState IsNot Nothing Then
            lblFormState.Text = "⬤  " & StateText
            lblFormState.BackColor = StateColor
            lblFormState.Visible = True
            lblFormState.Refresh() ' لإجبار الشاشة على إظهار اللون الجديد فوراً
        End If
    End Sub

    ' =========================================================
    ' 🌟 إضافة الظل الاحترافي (Drop Shadow) للفورم الفريملس
    ' =========================================================
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If On_Update = True Then Edit_butt_Click(sender, e)
        Me.Dispose()
        FormType = 0
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then If New_butt.Enabled = True Then New_butt_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If Edit_butt.Enabled = True And Edit_butt.Visible = True Then If Edit_butt.Text = EditState Then Edit_butt_Click(sender, e)
        If e.KeyCode = Keys.F4 Then If Delete_butt.Enabled = True And Delete_butt.Visible = True Then Delete_butt_Click(sender, e)
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetupAnchors()
            ModernLoader.ShowLoader()
            ' =========================================================
            ' 🌟 تعيين التاجات يدوياً للثيمات (يُمنع استخدام For Each نهائياً)
            ' =========================================================
            If TitleBar_Panel IsNot Nothing Then TitleBar_Panel.Tag = "HEADER"
            If Title_Label IsNot Nothing Then Title_Label.Tag = "TITLE_TRANSPARENT"
            If ExitFormButton IsNot Nothing Then ExitFormButton.Tag = "DELETE"
            If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Tag = "DELETE"
            If MaxFormButton IsNot Nothing Then MaxFormButton.Tag = "GENERAL"
            If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Tag = "DELETE"

            If New_butt IsNot Nothing Then New_butt.Tag = "GENERAL"
            If Save_butt IsNot Nothing Then Save_butt.Tag = "SAVE"
            If Edit_butt IsNot Nothing Then Edit_butt.Tag = "GENERAL"
            If Delete_butt IsNot Nothing Then Delete_butt.Tag = "DELETE"
            If Print_btn IsNot Nothing Then Print_btn.Tag = "PRINT"
            If SearchButton IsNot Nothing Then SearchButton.Tag = "GENERAL"
            If MakeBarcode_btn IsNot Nothing Then MakeBarcode_btn.Tag = "GENERAL"
            If Aggregate_Btn IsNot Nothing Then Aggregate_Btn.Tag = "GENERAL"
            If DeliveryingButton IsNot Nothing Then DeliveryingButton.Tag = "SAVE"

            If ADDCatButton IsNot Nothing Then ADDCatButton.Tag = "GENERAL"
            If RemoveCatButton IsNot Nothing Then RemoveCatButton.Tag = "DELETE"


            If IM_btn IsNot Nothing Then IM_btn.Tag = "GENERAL"
            ' If Show_IM_btn2 IsNot Nothing Then Show_IM_btn2.Tag = "GENERAL"
            If DGV_Control_btn IsNot Nothing Then DGV_Control_btn.Tag = "GENERAL"
            If Up_Bill_btn IsNot Nothing Then Up_Bill_btn.Tag = "GENERAL"
            If Down_Bill_btn IsNot Nothing Then Down_Bill_btn.Tag = "GENERAL"
            If Calc_Dicount_Btn IsNot Nothing Then Calc_Dicount_Btn.Tag = "GENERAL"
            If ADD_Dist_btn IsNot Nothing Then ADD_Dist_btn.Tag = "GENERAL"
            If Remove_Dist_btn IsNot Nothing Then Remove_Dist_btn.Tag = "DELETE"

            ' تطبيق الثيم الإجباري
            ThemeManager.ApplyThemeToForm(Me)

            ' =========================================================
            ' 🌟 الأكواد الأصلية للتحميل
            ' =========================================================
            FormType = 2
            Check_View_Control()
            Pch_Exp_Panel.Visible = S_Exp_Pch
            '    rs.FindAllControls(Me)
            Me.WindowState = FormWindowState.Maximized

            EditState = Edit_butt.Text
            DefaultFormState = Me.Text
            Disable_Fields()
            Fetch_Currency()
            Get_Last_T_ID()
            AG_Cm.SQL_SearchField_WHERE = " AND Type_ID IN ('" & Suply_Type_ID & "','" & General_AG_Type_ID & "')"

            AGMetroGrid.Columns("Main_Price_CL").Visible = Dist_DV.Visible
            AGMetroGrid.Columns("EXP_VALUE").Visible = Dist_DV.Visible
            'EXP_TOTAL_Panel.Visible = Dist_DV.Visible


            If U_Cancel_Pch = False Then Delete_butt.Visible = False
            If isShowing_Trans = True Then
                Select_ExpBill(T_ID_Trans)
                '    SelectStateBt()
                New_butt.Enabled = False
                SearchButton.Enabled = False
            End If
            '  New_butt_Click(sender, e)
            ModernLoader.CloseLoader()
        Catch ex As Exception
            ModernLoader.CloseLoader()
        End Try
    End Sub

    ' =========================================================
    ' 🌟 أكواد السحب والتكبير للشريط العلوي 🌟
    ' =========================================================
    Dim drag As Boolean, mouseX As Integer, mouseY As Integer
    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, Title_Label.MouseDown
        drag = True : mouseX = Cursor.Position.X - Me.Left : mouseY = Cursor.Position.Y - Me.Top
    End Sub
    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, Title_Label.MouseMove
        If drag Then Me.Location = New Point(Cursor.Position.X - mouseX, Cursor.Position.Y - mouseY)
    End Sub
    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, Title_Label.MouseUp
        drag = False
    End Sub

    Private Sub MaxFormButton_Click(sender As Object, e As EventArgs) Handles MaxFormButton.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
            MaxFormButton.Text = "⬜"
        Else
            Me.WindowState = FormWindowState.Maximized
            MaxFormButton.Text = "🗗"
        End If
    End Sub
    Private Sub SetupAnchors()
        ' إيقاف التحجيم التلقائي وتفعيل الرسم المزدوج لمنع الوميض
        Me.AutoScaleMode = AutoScaleMode.None
        Me.DoubleBuffered = True

        ' ==========================================
        ' 🌟 1. الجريد الرئيسي والملاحظات (تمدد ديناميكي)
        ' ==========================================
        If AGMetroGrid IsNot Nothing Then AGMetroGrid.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        If Notes_txt IsNot Nothing Then Notes_txt.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        If Label8 IsNot Nothing Then Label8.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right ' كلمة "ملاحظة :"

        ' ==========================================
        ' 🌟 2. الأجزاء السفلية (الإجماليات والخصم)
        ' ==========================================
        ' Panel3 في المشتريات واخذه Dock = Bottom من الديزاينر وأمورها طيبة، 
        ' ومحتوياتها (Panel4, DiscountPanel, Panel5) تترتب تلقائياً بالدوك، فما فيش داعي نلعبوا بيهم بالـ Anchor!

        ' ==========================================
        ' 🌟 3. الأجزاء العلوية (أزرار التحكم، بيانات الفاتورة والمورد)
        ' ==========================================
        ' شريط الأزرار الأساسي (حفظ، طباعة، إلغاء، جديد) 
        '   If Panel1 IsNot Nothing Then Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        ' بيانات الفاتورة (أعلى اليمين)
        If BillNumPanel IsNot Nothing Then BillNumPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If Panel3 IsNot Nothing Then Panel3.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' التاريخ والرقم اليومي
        If Panel2 IsNot Nothing Then Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        '  If AG_Panel IsNot Nothing Then AG_Panel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If Panel1 IsNot Nothing Then Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If BillNumPanel IsNot Nothing Then BillNumPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' الفواتير المعلقة

        ' بيانات المورد ورصيد الحساب (هذي اللي صايرة فيها السلاطة في الصورة)
        '  If AG_SH_txt IsNot Nothing Then AG_SH_txt.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' كومبو المورد الفعلي
        If Label25 IsNot Nothing Then Label25.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' رصيد الحساب
        If Label24 IsNot Nothing Then Label24.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' كلمة "المورد :"

        ' أزرار التحكم بالجريد (يمين الجريد)
        If DGV_Control_btn IsNot Nothing Then DGV_Control_btn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If ADDCatButton IsNot Nothing Then ADDCatButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If RemoveCatButton IsNot Nothing Then RemoveCatButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' بيانات إضافية (أعلى اليسار)
        '  If Panel1 IsNot Nothing Then Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        ' If Panel4 IsNot Nothing Then Panel4.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        'If Label1 IsNot Nothing Then Label1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub
    'Public Sub UpdatePchStatusUI()
    '    If lblFormState Is Nothing Then Exit Sub

    '    If isVoid Then
    '        ' حالة الإلغاء
    '        lblFormState.Text = "فاتورة ملغيــــة"
    '        lblFormState.BackColor = Color.FromArgb(231, 76, 60) ' أحمر
    '        lblFormState.ForeColor = Color.White
    '    ElseIf isDepended Then
    '        ' حالة الإعتماد
    '        lblFormState.Text = "فاتورة معتمـــدة"
    '        lblFormState.BackColor = Color.FromArgb(46, 204, 113) ' أخضر
    '        lblFormState.ForeColor = Color.White
    '    Else
    '        ' حالة فاتورة جديدة أو قيد التحرير
    '        lblFormState.Text = "فاتورة جديــــدة"
    '        lblFormState.BackColor = Color.FromArgb(52, 152, 219) ' أزرق
    '        lblFormState.ForeColor = Color.White
    '    End If
    'End Sub
    'Public Sub UpdatePchStatusUI()
    '    If lblFormState Is Nothing Then Exit Sub

    '    If isVoid = True Then
    '        ' حالة الإلغاء (أحمر)
    '        lblFormState.Text = "فاتورة ملغيــــة"
    '        lblFormState.BackColor = Color.FromArgb(231, 76, 60)
    '        lblFormState.ForeColor = Color.White
    '    ElseIf isDepended = True Then
    '        ' حالة الإعتماد بعد الحفظ (أخضر)
    '        lblFormState.Text = "فاتورة معتمـــدة"
    '        lblFormState.BackColor = Color.FromArgb(46, 204, 113)
    '        lblFormState.ForeColor = Color.White
    '    Else
    '        ' الحالة الافتراضية عند اللود أو زر جديد (أزرق)
    '        lblFormState.Text = "فاتورة جديــــدة"
    '        lblFormState.BackColor = Color.FromArgb(52, 152, 219)
    '        lblFormState.ForeColor = Color.White
    '    End If
    'End Sub
    Public Sub CheckAccountingState()
        If lblFormState Is Nothing Then Return

        ' إذا كانت الفاتورة جديدة ولم تحفظ بعد (في اللود أو زر جديد)
        If T_ID = 0 Then
            lblFormState.Visible = False ' إخفاء الليبل بالكامل
            Return
        End If

        Try
            Dim db As New C()
            db.Str = "SELECT TOP 1 JournalId FROM Agents_Balance_MV WHERE T_ID = " & T_ID
            db.Com = New SqlClient.SqlCommand(db.Str, db.Con)

            db.Con.Open()
            Dim jId As Object = db.Com.ExecuteScalar()
            db.Con.Close()

            lblFormState.Visible = True ' إظهار الليبل لأن الفاتورة محفوظة

            ' التشييك: هل القيد فارغ أو غير موجود؟
            If IsDBNull(jId) OrElse jId Is Nothing OrElse jId.ToString().Trim() = "" Then
                lblFormState.Text = "⬤ غير مرحلة محاسبياً"
                lblFormState.BackColor = Color.DarkOrange
                lblFormState.ForeColor = Color.White
            Else
                lblFormState.Text = "⬤ مرحلة محاسبياً - قيد رقم: " & jId.ToString()
                lblFormState.BackColor = Color.ForestGreen
                lblFormState.ForeColor = Color.White
            End If

        Catch ex As Exception
            '  If db.Con.State = ConnectionState.Open Then db.Con.Close()
        End Try
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    ' =========================================================
    ' 🌟 الدوال المحاسبية الخاصة بالنظام (مُنقّحة من التعليقات) 🌟
    ' =========================================================
    Private Sub Fetch_Currency()
        Dim C As New C
        Try
            Dim sql As String = "Select Cr_ID , Cr_Name from Currency Order By Cr_ID ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            Cr_CM.DataSource = C.Dt
            Cr_CM.DisplayMember = "Cr_Name"
            Cr_CM.ValueMember = "Cr_ID"



            If Cr_CM.SelectedValue = 1 Then
                AGMetroGrid.Columns("Price_By_Foriegn_Cr_CL").Visible = False
                AGMetroGrid.Columns("Cr_NAME_CL").Visible = False
                AGMetroGrid.Columns("Price_By_Equal_CL").Visible = False
                AGMetroGrid.Columns("Price_CL").HeaderText = "السعر"
                AGMetroGrid.Columns("NewSale_CL").HeaderText = "البيع"
                AGMetroGrid.Columns("TOTAL_CL").HeaderText = "الإجمالي"
            Else
                AGMetroGrid.Columns("Price_By_Foriegn_Cr_CL").Visible = True
                AGMetroGrid.Columns("Cr_NAME_CL").Visible = True
                AGMetroGrid.Columns("Price_By_Equal_CL").Visible = True
                AGMetroGrid.Columns("Price_CL").HeaderText = "السعر بالعملة المحلية"
                AGMetroGrid.Columns("NewSale_CL").HeaderText = "البيع بالعملة المحلية"
                AGMetroGrid.Columns("TOTAL_CL").HeaderText = "الإجمالي بالعملة المحلية"
            End If



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Get_Last_T_ID()
        is_Select_Mode = True
        Dim C As New C
        Dim S As String = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 7 AND isDepended = 0 AND isVoid = 0  AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Select_ExpBill(T_ID)
            Else
                Call_New_Bill()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
        is_Select_Mode = False
    End Sub

    Public Sub Check_View_Control()
        AGMetroGrid.Columns("ST_Name_CL").Visible = MY_Settings.S_ST_Name_CL
        AGMetroGrid.Columns("D_Valid_CL").Visible = MY_Settings.S_D_Valid_CL
        AGMetroGrid.Columns("IMUnit_CL").Visible = MY_Settings.S_IMUnit_CL
        AGMetroGrid.Columns("Price_CL").Visible = MY_Settings.S_Price_CL
        AGMetroGrid.Columns("Total_CL").Visible = MY_Settings.S_Total_CL
        AGMetroGrid.Columns("Notes_CL").Visible = MY_Settings.SP_Notes_CL
        AGMetroGrid.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL
        AGMetroGrid.Columns("Barcode_CL").Visible = MY_Settings.S_Barcode_CL
        Aggregate_Btn.Visible = S_Stores
    End Sub

    Public Sub SELECT_MAX()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT ISNULL(MAX(Pch_ID),0) + 1 AS N FROM Agents_Balance_MV "
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Bill_ID_Txt.Text = c.Dr("N")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Enable_Fields()
        ' AG_SH_txt.Enabled = True
        '  Show_IM_btn2.Enabled = True
        AG_Cm.Enabled = True
        EX_ReferNumTextBox.Enabled = True
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        Ebable_CatFields()
        DiscountPanel.Enabled = True
        ADD_Dist_btn.Enabled = True
        Remove_Dist_btn.Enabled = True
        Cr_Equal_TXT.Enabled = True
        Aggregate_Btn.Enabled = True
        AGMetroGrid.BackgroundColor = Color.LightYellow
        AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
    End Sub

    Private Sub Disable_Fields()
        'AG_SH_txt.Enabled = False
        'Show_IM_btn2.Enabled = False
        AG_Cm.Enabled = False
        EX_ReferNumTextBox.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        DiscountPanel.Enabled = False
        ADD_Dist_btn.Enabled = False
        Remove_Dist_btn.Enabled = False
        Cr_Equal_TXT.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
        ADD_Dist_btn.Enabled = False
        Remove_Dist_btn.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
        ADD_Dist_btn.Enabled = True
        Remove_Dist_btn.Enabled = True
    End Sub

    Public Sub Switch_Dependcy(F As Boolean)
        If F = True Then
            isDepended = 1

            AG_Cm.Enabled = False
            'AG_SH_txt.Enabled = False
            DeliveryingButton.Enabled = True
            Save_butt.Enabled = False
        Else
            isDepended = 0
            AG_Cm.Enabled = True
            'AG_SH_txt.Enabled = True
            DeliveryingButton.Enabled = False
            Save_butt.Enabled = True
        End If
    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = "فاتورة مشتريات جديدة"
        'AG_Grid.Visible = False
        'AG_SH_txt.Enabled = True
        AG_Cm.Enabled = True
    End Sub

    Private Sub DeleteOrUpdateStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Private Sub SavedStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Public Sub SelectStateBt()
        If isVoid = True Then
            DeletedBillLabel.Visible = True
            DeletedBillLabel.BackColor = Color.Red
            Print_btn.Enabled = False
            Disable_Fields()
            Save_butt.Enabled = False
            Edit_butt.Enabled = False
            Edit_butt.Text = EditState
            Delete_butt.Enabled = False
            AGMetroGrid.Enabled = True
            AGMetroGrid.BackgroundColor = Color.IndianRed
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed
            DiscountPanel.Enabled = False
            DeliveryingButton.Enabled = False
            Aggregate_Btn.Enabled = False
            '       UpdateFormStateIndicator("فاتورة ملغاة", Color.Red)
        Else
            If isDepended = False Then
                Save_butt.Enabled = True
                DiscountPanel.Enabled = True
                Print_btn.Enabled = False
                Enable_Fields()
                '      UpdateFormStateIndicator("محفوظة", Color.DodgerBlue)
            Else
                Print_btn.Enabled = True
                AGMetroGrid.BackgroundColor = Color.LightGreen
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
                DiscountPanel.Enabled = False
                Disable_Fields()
            End If
            Edit_butt.Enabled = True
            Edit_butt.Text = EditState
            DeletedBillLabel.Visible = False
            Delete_butt.Enabled = True
            DeliveryingButton.Enabled = True
            Aggregate_Btn.Enabled = False
            '    UpdateFormStateIndicator("مُرحّلة / معتمدة", Color.LimeGreen)
        End If
        Me.Text = "فاتورة مشتريات "
    End Sub

    Private Sub ClearFields()
        T_ID = 0
        AG_ID = Default_AG_ID
        Notes_txt.Clear()
        EX_ReferNumTextBox.Clear()
        Pure_txt.Clear()
        Bill_DT.Clear()
        Exp_DT.Clear()
        Receipts_DT.Clear()
        DateTimeEx.Text = Date.Now
        Edit_butt.Text = EditState
        DeletedBillLabel.Visible = False
        isVoid = False
        CreditTextBox.ForeColor = Color.Black
        User_Name_lb.Text = "---"
        Me.Text = FormState
        On_Update = False
        '  Edit_butt.BackColor = Color.WhiteSmoke
        'AG_SH_txt.Clear()
        AG_Cm.Textt = ""
        '  AG_Balance = 0
        Discount_txt.Clear()
        Total_txt.Clear()
        PchExpWithBillTotal = 0
        PchExpWithoutBillTotal = 0
        PchTotalWithBillExpenses = 0
        PchTotalWithoutBillExpenses = 0
        SetPurchaseExpenseTotalsText()
        Pure_txt.Text = "0"
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        If On_Update = True Then Edit_butt_Click(sender, e)
        Call_New_Bill()

    End Sub

    Private Sub Call_New_Bill()
        If T_ID > 0 Then
            If MessageBox.Show("فتح فاتورة جديدة", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                '  UpdateFormStateIndicator("فاتورة جديدة", Color.Honeydew)
                ClearFields()
                Insert_NewBill()
                NewStateBt()
            End If
        Else
            ClearFields()
            Insert_NewBill()
            NewStateBt()
        End If
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        If AGMetroGrid.Rows.Count > 0 Then
            If AG_ID = 0 Then 'String.IsNullOrWhiteSpace(AG_SH_txt.Text) = False And
                MsgBox("حدد إسم العميل", MsgBoxStyle.Critical, "خطأ في الإعتماد")
                'AG_SH_txt.Select()
                AG_Cm.Select()
            Else
                'If String.IsNullOrWhiteSpace(AG_SH_txt.Text) Then
                '    Fetch_ItemToList2()
                'End If
                Beep()
                If MessageBox.Show(" حفظ الفاتــورة ؟", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.OK Then
                    Save_PchBill_WithSingleConnection()
                End If
            End If
        End If
    End Sub

    Private Sub Save_PchBill_WithSingleConnection()

        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Discount_txt.Text = "0"

        Try

            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)

                cn.Open()

                Dim isDependedSaved As Boolean = False
                Dim calculatedDisc As Double = Convert.ToDouble(Discount_txt.Text) * Convert.ToDouble(Cr_Equal_TXT.Text)

                Using tr As SqlClient.SqlTransaction = cn.BeginTransaction()

                    Try

                        'ExecuteEditStoredProcedure(cn, tr, "AG_Balance_Update_AG",
                        '    Sub(cmd)
                        '        cmd.Parameters.AddWithValue("@T_ID", T_ID)
                        '        cmd.Parameters.AddWithValue("@AG_ID", AG_ID)
                        '        cmd.Parameters.AddWithValue("@ON_UPDATE", On_Update)
                        '    End Sub)

                        ExecuteEditStoredProcedure(cn, tr, "AG_Balance_Update_About",
                            Sub(cmd)
                                cmd.Parameters.AddWithValue("@T_ID", T_ID)
                                If String.IsNullOrWhiteSpace(Notes_txt.Text) = False Then cmd.Parameters.AddWithValue("@About", Notes_txt.Text)
                            End Sub)

                        ExecuteEditStoredProcedure(cn, tr, "AG_Balance_Update_ReferNum",
                            Sub(cmd)
                                cmd.Parameters.AddWithValue("@T_ID", T_ID)
                                cmd.Parameters.AddWithValue("@ReferNum", EX_ReferNumTextBox.Text)
                            End Sub)

                        ExecuteEditStoredProcedure(cn, tr, "AG_Balance_Update_Date",
                            Sub(cmd)
                                cmd.Parameters.AddWithValue("@T_ID", T_ID)
                                cmd.Parameters.AddWithValue("@Date", DateTimeEx.Value)
                                cmd.Parameters.AddWithValue("@Month", DateTimeEx.Value.Month)
                                cmd.Parameters.AddWithValue("@YEAR", DateTimeEx.Value.Year)
                            End Sub)

                        ExecuteEditStoredProcedure(cn, tr, "AG_Balance_Update_Equal_Value",
                            Sub(cmd)
                                cmd.Parameters.AddWithValue("@T_ID", T_ID)
                                cmd.Parameters.AddWithValue("@Cr_ID", Cr_CM.SelectedValue)
                                If String.IsNullOrWhiteSpace(Cr_Equal_TXT.Text) = False Then cmd.Parameters.AddWithValue("@Cr_Equal_Value", Cr_Equal_TXT.Text)
                            End Sub)

                        ExecutePchDiscountUpdate(cn, tr, calculatedDisc)
                        isDependedSaved = ExecutePchDependingBill(cn, tr)

                        tr.Commit()

                    Catch

                        tr.Rollback()
                        Throw

                    End Try

                End Using

                ApplyPchDiscountValues(calculatedDisc)

                If isDependedSaved = True Then
                    SelectCurrentPchBill(cn)
                End If

            End Using

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub ExecutePchDiscountUpdate(cn As SqlClient.SqlConnection, tr As SqlClient.SqlTransaction, calculatedDisc As Double)

        Using discountCmd As New SqlClient.SqlCommand(
            "Update Agents_Balance_MV SET Discount = @Discount WHERE T_ID = @T_ID",
            cn,
            tr
        )

            discountCmd.Parameters.AddWithValue("@Discount", calculatedDisc)
            discountCmd.Parameters.AddWithValue("@T_ID", T_ID)
            discountCmd.ExecuteNonQuery()

        End Using

        ExecuteEditStoredProcedure(cn, tr, "Network_Edit_Tracker_insert",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@User_ID", USER_ID)
                cmd.Parameters.AddWithValue("@Notes", " تخفيض للفاتورة بقيمة:" & calculatedDisc.ToString)
                cmd.Parameters.AddWithValue("@Bill_ID", Bill_ID_Txt.Text)
                cmd.Parameters.AddWithValue("@Screen_Type", 7)
                cmd.Parameters.AddWithValue("@Operation_ID", 3)
                cmd.Parameters.AddWithValue("@CP_Name", My.Computer.Name)
            End Sub)

    End Sub

    Private Function ExecutePchDependingBill(cn As SqlClient.SqlConnection, tr As SqlClient.SqlTransaction) As Boolean

        Using cmd As New SqlClient.SqlCommand("AG_Balance_Update_isDepended", cn, tr)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@T_ID", T_ID)
            If isPr_Open Then cmd.Parameters.AddWithValue("@Pr_ID", Pr_ID)
            cmd.Parameters.AddWithValue("@Tr_ID", PCH_TR_ID)
            cmd.Parameters.AddWithValue("@Pay_ID", 1)

            Return cmd.ExecuteNonQuery() <> 0

        End Using

    End Function

    Private Sub ApplyPchDiscountValues(calculatedDisc As Double)

        Disc = calculatedDisc
        Discount_txt.Text = Disc
        If Cr_CM.SelectedValue > 1 Then T_Other_Cr_TXT.Text = (Pure / Convert.ToDouble(Cr_Equal_TXT.Text)).ToString("n")
        Pure_txt.Text = (TOTAL - Disc).ToString("n")
        Pure = TOTAL - Disc

    End Sub

    Private Sub SelectCurrentPchBill(cn As SqlClient.SqlConnection)

        Using cmd As New SqlClient.SqlCommand("Select * From Pch_Balance_MV_V Where T_ID = @T_ID", cn)

            cmd.Parameters.AddWithValue("@T_ID", T_ID)

            Using dr As SqlClient.SqlDataReader = cmd.ExecuteReader()

                If dr.HasRows Then
                    dr.Read()

                    T_ID = dr("T_ID")
                    Pch_ID = dr("Bill_ID")
                    Bill_ID_Txt.Text = S_Sub_Code & dr("Bill_ID")
                    AG_ID = dr("AG_ID")
                    AG_Cm.Set_IM_By_ID(AG_ID)
                    DateTimeEx.Text = dr("Date")
                    Notes_txt.Text = dr("About")
                    EX_ReferNumTextBox.Text = dr("ReferNum")
                    TOTAL = dr("Cost")
                    Disc = dr("Discount")
                    Total_txt.Text = TOTAL.ToString("N")
                    Pure_txt.Text = (TOTAL - Disc).ToString("N")
                    Discount_txt.Text = Disc
                    Switch_Dependcy(dr("isDepended"))
                    isVoid = dr("isVoid")
                    User_Name_lb.Text = dr("UserName") + " - " + dr("Date").ToString
                    Cr_Equal_TXT.Text = dr("Cr_Equal_Value")
                    SelectStateBt()

                End If

            End Using

        End Using

        Pch_Contents_SELECT_Bill(cn)
        SelectPchReceiptWithConnection(cn)
        Pch_Contents_SELECT_EXP(cn)
        If AGMetroGrid.Rows.Count = 0 Then DateTimeEx.Value = Date.Now

    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        '   If isDepended = True Then
        If AGMetroGrid.BackgroundColor <> Color.White AndAlso AGMetroGrid.BackgroundColor <> SystemColors.Window Then
            If U_Pch_Update = True Then
                If On_Update = False Then
                    Beep()
                    If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟", "تعديل فاتورة", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        If Open_Agents_Balance_MV_For_Edit(T_ID) = False Then Exit Sub

                        '  Edit_butt.BackColor = Color.GreenYellow
                        UpdateFormStateIndicator("قيد التعديل", Color.DarkOrange)
                        On_Update = True
                        AGMetroGrid.Enabled = True
                        AGMetroGrid.BackgroundColor = Color.LightYellow
                        AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                        ADDCatButton.Enabled = True
                        RemoveCatButton.Enabled = True
                        Ebable_CatFields()
                        Edit_butt.Text = "إيقاف التعديل"
                        Notes_txt.Enabled = True
                        DateTimeEx.Enabled = True
                        EX_ReferNumTextBox.Enabled = True
                        DiscountPanel.Enabled = True
                        ' AG_SH_txt.Enabled = True
                        'Show_IM_btn2.Enabled = True
                        AG_Cm.Enabled = True
                        Aggregate_Btn.Enabled = True
                        If Cr_CM.SelectedValue > 1 And Cr_Equal_TXT.Visible = True Then Cr_Equal_TXT.Enabled = True
                    End If
                Else
                    Save_EditChanges()
                    On_Update = False
                    Edit_butt.Text = EditState
                    SelectStateBt()
                    Notes_txt.Enabled = False
                    DiscountPanel.Enabled = False
                    'AG_SH_txt.Enabled = False
                    'Show_IM_btn2.Enabled = False
                    AG_Cm.Enabled = False
                    Select_Pch_Receipt(T_ID)
                End If
            Else
                MsgBox("أنت غير مخول بتعديل فاتورة تم حفظها", MsgBoxStyle.Exclamation)
            End If
        Else
            If Edit_butt.Text = EditState Then
                Edit_butt.Text = "ح التعديل"
                Enable_Fields()
            Else
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Edit_butt.Text = EditState
                Disable_Fields()
                SelectStateBt()
            End If
        End If
    End Sub

    Private Sub Save_EditChanges()

        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Discount_txt.Text = "0"

        Try

            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)

                cn.Open()

                Using tr As SqlClient.SqlTransaction = cn.BeginTransaction()

                    Try

                        ExecuteEditStoredProcedure(cn, tr, "AG_Balance_Update_Total",
                            Sub(cmd)
                                cmd.Parameters.AddWithValue("@T_ID", T_ID)
                                cmd.Parameters.AddWithValue("@Total", TOTAL)
                                cmd.Parameters.AddWithValue("@Disc", Disc)
                            End Sub)

                        ExecuteEditStoredProcedure(cn, tr, "AG_Balance_Update_About",
                            Sub(cmd)
                                cmd.Parameters.AddWithValue("@T_ID", T_ID)
                                If String.IsNullOrWhiteSpace(Notes_txt.Text) = False Then cmd.Parameters.AddWithValue("@About", Notes_txt.Text)
                            End Sub)

                        ExecuteEditStoredProcedure(cn, tr, "AG_Balance_Update_ReferNum",
                            Sub(cmd)
                                cmd.Parameters.AddWithValue("@T_ID", T_ID)
                                cmd.Parameters.AddWithValue("@ReferNum", EX_ReferNumTextBox.Text)
                            End Sub)

                        ExecuteEditStoredProcedure(cn, tr, "AG_Balance_Update_Date",
                            Sub(cmd)
                                cmd.Parameters.AddWithValue("@T_ID", T_ID)
                                cmd.Parameters.AddWithValue("@Date", DateTimeEx.Value)
                                cmd.Parameters.AddWithValue("@Month", DateTimeEx.Value.Month)
                                cmd.Parameters.AddWithValue("@YEAR", DateTimeEx.Value.Year)
                            End Sub)

                        Disc = Convert.ToDouble(Discount_txt.Text) * Convert.ToDouble(Cr_Equal_TXT.Text)
                        Discount_txt.Text = Disc

                        Using discountCmd As New SqlClient.SqlCommand(
                            "Update Agents_Balance_MV SET Discount = @Discount WHERE T_ID = @T_ID",
                            cn,
                            tr
                        )

                            discountCmd.Parameters.AddWithValue("@Discount", Convert.ToDouble(Discount_txt.Text))
                            discountCmd.Parameters.AddWithValue("@T_ID", T_ID)
                            discountCmd.ExecuteNonQuery()

                        End Using

                        ExecuteEditStoredProcedure(cn, tr, "Network_Edit_Tracker_insert",
                            Sub(cmd)
                                cmd.Parameters.AddWithValue("@User_ID", USER_ID)
                                cmd.Parameters.AddWithValue("@Notes", " تخفيض للفاتورة بقيمة:" & Disc.ToString)
                                cmd.Parameters.AddWithValue("@Bill_ID", Bill_ID_Txt.Text)
                                cmd.Parameters.AddWithValue("@Screen_Type", 7)
                                cmd.Parameters.AddWithValue("@Operation_ID", 3)
                                cmd.Parameters.AddWithValue("@CP_Name", My.Computer.Name)
                            End Sub)

                        tr.Commit()

                    Catch

                        tr.Rollback()
                        Throw

                    End Try

                End Using

            End Using

            If Cr_CM.SelectedValue > 1 Then T_Other_Cr_TXT.Text = (Pure / Convert.ToDouble(Cr_Equal_TXT.Text)).ToString("n")
            Pure_txt.Text = (TOTAL - Disc).ToString("n")
            Pure = TOTAL - Disc

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub ExecuteEditStoredProcedure(cn As SqlClient.SqlConnection, tr As SqlClient.SqlTransaction, procedureName As String, addParameters As Action(Of SqlClient.SqlCommand))

        Using cmd As New SqlClient.SqlCommand(procedureName, cn, tr)

            cmd.CommandType = CommandType.StoredProcedure
            addParameters(cmd)
            cmd.ExecuteNonQuery()

        End Using

    End Sub

    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click
        If AGMetroGrid.Rows.Count > 0 Then
            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_For_Cancel_Pch() = 1 Then
                    MsgBox(" لا يمكن سحب كمية بالسالب للصنف  " & Str_Name, MsgBoxStyle.Exclamation)

                    Exit Sub
                End If
            End If
        End If
        Beep()
        If MessageBox.Show(" سيتم إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "إلغــاء فاتورة", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            '  UpdateFormStateIndicator("فاتورة ملغاة", Color.Crimson)
            Cancel_Bill()
        End If
    End Sub

    Private Function IM_Check_Neg_QTY_For_Cancel_Pch()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_For_Cancel_Pch"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@T_ID", T_ID)
            .Parameters.Add("@Str_Name", SqlDbType.Char, 1500)
            .Parameters("@F").Direction = ParameterDirection.Output
            .Parameters("@Str_Name").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
                Str_Name = .Parameters("@Str_Name").Value
            End If
        End With
        Return F
    End Function

    Private Sub Cancel_Bill()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Void_Row"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم إلغاء الفاتورة", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert("إلغاء الفاتورة", Bill_ID_Txt.Text, 7, 3)
            isVoid = True
            SelectStateBt()
        End If
    End Sub

    'Private Sub TreasuryCard_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
    '    rs.ResizeAllControls(Me)
    'End Sub

    Private Sub Tr_Name_txtb_Enter(sender As Object, e As EventArgs)
        Arabic_Lang()
    End Sub

    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pure_txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub


    Public Sub Calc_Total()
        TOTAL = 0
        TOTAL_NO_EXP = 0
        PchExpWithBillTotal = 0
        PchExpWithoutBillTotal = 0
        PchTotalWithBillExpenses = 0
        PchTotalWithoutBillExpenses = 0
        Dim QTY As Double = 0
        For i = 0 To AGMetroGrid.Rows.Count - 1
            If AGMetroGrid.Rows(i).IsNewRow = False Then
                TOTAL += GetGridCellDoubleValue(AGMetroGrid.Rows(i), "Total_CL")
                QTY += GetGridCellDoubleValue(AGMetroGrid.Rows(i), "QTY_CL")
                TOTAL_NO_EXP += GetGridCellDoubleValue(AGMetroGrid.Rows(i), "Main_Price_CL") * GetGridCellDoubleValue(AGMetroGrid.Rows(i), "QTY_CL")
            End If
        Next
        For j = 0 To Dist_DV.Rows.Count - 1
            If Dist_DV.Rows(j).IsNewRow = False Then
                If GetGridCellBooleanValue(Dist_DV.Rows(j), "isWithBill_CL") Then
                    PchExpWithBillTotal += GetGridCellDoubleValue(Dist_DV.Rows(j), "Dist_Values_CL")
                Else
                    PchExpWithoutBillTotal += GetGridCellDoubleValue(Dist_DV.Rows(j), "Dist_Values_CL")
                End If
            End If
        Next
        PchTotalWithBillExpenses = TOTAL_NO_EXP + PchExpWithBillTotal
        PchTotalWithoutBillExpenses = TOTAL_NO_EXP + PchExpWithoutBillTotal
        Total_txt.Text = TOTAL.ToString(N_Point_Fter)
        SetPurchaseExpenseTotalsText()
        Pure_txt.Text = (TOTAL - Disc).ToString(N_Point_Fter)
        Pure = TOTAL - Disc
        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "
        If Cr_CM.SelectedValue > 1 Then T_Other_Cr_TXT.Text = (Pure / Convert.ToDouble(Cr_Equal_TXT.Text)).ToString(N_Point_Fter)
    End Sub

    Private Sub SetPurchaseExpenseTotalsText()

        If Dist_TotalWithoutExpenses_txt IsNot Nothing Then Dist_TotalWithoutExpenses_txt.Text = TOTAL_NO_EXP.ToString(N_Point_Fter)
        If Dist_TotalWithBill_txt IsNot Nothing Then Dist_TotalWithBill_txt.Text = PchTotalWithBillExpenses.ToString(N_Point_Fter)
        If Dist_TotalWithoutBill_txt IsNot Nothing Then Dist_TotalWithoutBill_txt.Text = PchTotalWithoutBillExpenses.ToString(N_Point_Fter)

    End Sub

    Private Function GetGridCellDoubleValue(row As DataGridViewRow, columnName As String) As Double

        If row Is Nothing OrElse row.DataGridView Is Nothing OrElse row.DataGridView.Columns.Contains(columnName) = False Then Return 0

        Dim cellValue As Object = row.Cells(columnName).Value
        If cellValue Is Nothing OrElse cellValue Is DBNull.Value OrElse String.IsNullOrWhiteSpace(cellValue.ToString()) Then Return 0

        Try
            Return Convert.ToDouble(cellValue)
        Catch
            Dim result As Double = 0
            If Double.TryParse(cellValue.ToString(), result) Then Return result
        End Try

        Return 0

    End Function

    Private Function GetGridCellBooleanValue(row As DataGridViewRow, columnName As String) As Boolean

        If row Is Nothing OrElse row.DataGridView Is Nothing OrElse row.DataGridView.Columns.Contains(columnName) = False Then Return False

        Dim cellValue As Object = row.Cells(columnName).Value
        If cellValue Is Nothing OrElse cellValue Is DBNull.Value Then Return False

        If TypeOf cellValue Is Boolean Then Return Convert.ToBoolean(cellValue)

        Try
            Return Convert.ToDouble(cellValue) <> 0
        Catch
            Dim result As Boolean = False
            If Boolean.TryParse(cellValue.ToString(), result) Then Return result
        End Try

        Return False

    End Function

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        F_Pch_IM_card = New Pch_IM_card_11
        F_Pch_IM_card.T_ID = T_ID
        F_Pch_IM_card.ShowDialog()
    End Sub

    Private Sub Insert_NewBill()
        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "Agents_BalanceMV_insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", 0)
        sqlComm.Parameters.AddWithValue("@Pch_ID", 0)
        sqlComm.Parameters.AddWithValue("@IMEX_ID", 0)
        sqlComm.Parameters.AddWithValue("@Jrd_ID", 0)
        sqlComm.Parameters.AddWithValue("@SRtn_ID", 0)
        sqlComm.Parameters.AddWithValue("@PRtn_ID", 0)
        sqlComm.Parameters.AddWithValue("@Receipt_Num", 0)
        sqlComm.Parameters.AddWithValue("@ST_Tran_ID", 0)
        sqlComm.Parameters.AddWithValue("@EXP_ID", 0)
        sqlComm.Parameters.AddWithValue("@Frm_ID", 0)
        sqlComm.Parameters.AddWithValue("@ViewSB_ID", 0)
        sqlComm.Parameters.AddWithValue("@InSale_ID", 0)
        sqlComm.Parameters.AddWithValue("@Outsale_ID", 0)
        sqlComm.Parameters.AddWithValue("@Frm_ID_M", 0)
        sqlComm.Parameters.AddWithValue("@ST_Sett_ID", 0)
        sqlComm.Parameters.AddWithValue("@AG_ID", 1)
        sqlComm.Parameters.AddWithValue("@Date", Me.DateTimeEx.Value)
        sqlComm.Parameters.AddWithValue("@BsType_ID", 7)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@Pch_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output
        If SQL_SP_EXEC(sqlComm) = True Then
            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
            Select_ExpBill(T_ID)
            Fetch_AG_Currency()
        End If
    End Sub

    Public Sub Pch_Contents_SELECT_Bill(Optional sqlCon As SqlClient.SqlConnection = Nothing)

        If sqlCon Is Nothing Then
            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
                cn.Open()
                Pch_Contents_SELECT_Bill(cn)
            End Using
            Return
        End If

        Bill_DT.Clear()

        Using cmd As New SqlClient.SqlCommand()
            cmd.Connection = sqlCon
            cmd.CommandText = "Pch_Details_SELECT_Bill"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)

            Using da As New SqlClient.SqlDataAdapter(cmd)
                da.Fill(Bill_DT)
            End Using
        End Using

        AGMetroGrid.DataSource = Bill_DT
        If AGMetroGrid.Rows.Count > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(AGMetroGrid.Rows.Count - 1).Cells("EX_Name_CL")
        Calc_Total()

    End Sub

    Public Sub Pch_Contents_SELECT_EXP(Optional sqlCon As SqlClient.SqlConnection = Nothing)

        If sqlCon Is Nothing Then
            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
                cn.Open()
                Pch_Contents_SELECT_EXP(cn)
            End Using
            Return
        End If

        Exp_DT.Clear()

        Using cmd As New SqlClient.SqlCommand()
            cmd.Connection = sqlCon
            cmd.CommandText = "[Pch_Details_SELECT_EXP_Dist]"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)

            Using da As New SqlClient.SqlDataAdapter(cmd)
                da.Fill(Exp_DT)
            End Using
        End Using

        Dist_DV.DataSource = Exp_DT
        Calc_Total()

    End Sub

    Private Sub SelectPchReceiptWithConnection(sqlCon As SqlClient.SqlConnection)

        Receipts_DT.Clear()

        Using cmd As New SqlClient.SqlCommand("select T_ID,Receipt_Num,Type_Name,Value from Pch_Receipts_V WHERE Receipt_Tran_ID = @T_ID AND isVoid = 0", sqlCon)

            cmd.Parameters.AddWithValue("@T_ID", T_ID)

            Using da As New SqlClient.SqlDataAdapter(cmd)
                da.Fill(Receipts_DT)
            End Using

        End Using

        ReceiptsMetroGrid.DataSource = Receipts_DT
        If String.IsNullOrWhiteSpace(CreditTextBox.Text) Then CreditTextBox.Text = "0.000"

    End Sub

    Private Sub Delete_Cat()
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Pch_Details_Delete"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        sqlComm.Parameters.AddWithValue("@On_Update", On_Update)
        If SQL_SP_EXEC(sqlComm) = True Then
            If Not IsDBNull(AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then MsgBox("قم بتعديل سعر البيع من شاشة الأصناف", MsgBoxStyle.Information, "")
            Network_Edit_Tracker_insert(" الصنف:" + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString + " الوحدة:" + AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value.ToString + " العدد:" + AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString _
                            + " السعر:" + AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 7, 2)
            Pch_Contents_SELECT_Bill()
            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")
        End If
    End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        If Dist_DV.Rows.Count > 0 Then
            MsgBox("لا يمكنك إضافة أو التعديل فالأصناف لوجود قيمة موزعة على البضاغة ... قم بإلغاء القيمة الموزعة أولا", MsgBoxStyle.Exclamation, "تنويه")
        Else
            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_For_Update_Pch() = 1 Then
                    MsgBox("في حالة خذف الصنف ستصبح كمية المخزون سالبة", MsgBoxStyle.Critical, "خطأ")
                    Exit Sub
                End If
            End If
            If AGMetroGrid.Rows.Count > 0 Then
                If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    Delete_Cat()
                End If
            End If
        End If
    End Sub

    Private Function IM_Check_Neg_QTY_For_Update_Pch()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_For_Update_Pch"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Pch_T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With
        Return F
    End Function

    Private Sub EX_ReferNumTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles EX_ReferNumTextBox.KeyDown
        If e.KeyCode = Keys.Return Then If Edit_butt.Text = EditState Then Save_ReferNum(T_ID, EX_ReferNumTextBox.Text)
    End Sub

    Private Sub DateTimeEx_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeEx.KeyDown
        If e.KeyCode = Keys.Return Then If Edit_butt.Text = EditState Then Save_Date(T_ID, DateTimeEx)
    End Sub

    Private Sub SerachButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Me.Cursor = Cursors.AppStarting
        FormType = 2
        PchSearch.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DeliveryingButton_Click(sender As Object, e As EventArgs) Handles DeliveryingButton.Click
        If isDepended = True Then
            FormType = 2
            AG_Type = 4
            F_Receipt = New Receipt
            Receipt_Tran_ID = T_ID
            With F_Receipt
                Rct_Tr_ID = PCH_TR_ID
                .Fields_Panel.Enabled = True
                .AG_Cm.Enabled = False
                .Barcode_SH_txt.Enabled = False
                .Receipt_Title_combobox.Text = "فاتورة مشتريات : " + Bill_ID_Txt.Text
                .AG_ID = AG_ID
                .money_num_txtb.Text = Pure - Convert.ToDouble(CreditTextBox.Text)
            End With
            isShowing_Trans = False
            F_Receipt.ShowDialog()
        Else
            MsgBox("يجب إعتماد الفاتورة أولا", MsgBoxStyle.Exclamation, "")
        End If
    End Sub

    Private Sub ReceiptsMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ReceiptsMetroGrid.MouseDoubleClick
        If ReceiptsMetroGrid.Rows.Count > 0 Then
            AG_Type = 4
            isShowingDetails = True
            F_Receipt = New Receipt
            F_Receipt.ShowDialog()
            isShowingDetails = False
        End If
    End Sub

    Private Sub ReceiptsMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles ReceiptsMetroGrid.RowsAdded
        Calc_Credit()
    End Sub

    Private Sub Calc_Credit()
        Dim Sum As Double = 0
        For i = 0 To ReceiptsMetroGrid.Rows.Count - 1
            Sum = Sum + ReceiptsMetroGrid.Rows(i).Cells("Value_CL").Value
        Next
        CreditTextBox.Text = Sum.ToString("n")
    End Sub

    Private Sub ReceiptsMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles ReceiptsMetroGrid.RowsRemoved
        Calc_Credit()
    End Sub

    Private Function Insert_Fast_AG()
        Dim New_AG_ID As Integer = 0
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Agents_insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@AG_ID", 0)
        sqlComm.Parameters.AddWithValue("@Ag_name", AG_Cm.Textt)
        sqlComm.Parameters.AddWithValue("@Barcode", "")
        sqlComm.Parameters.AddWithValue("@Type_ID", Suply_Type_ID)
        sqlComm.Parameters("@AG_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters.AddWithValue("@E_mail", "")
        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تمت إضافة العميــل", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" (من شاشة المشتريات) الزبون:" & AG_Cm.Textt, 0, 27, 1)
            New_AG_ID = sqlComm.Parameters("@AG_ID").Value.ToString()
            'Load_AG()
        End If
        Return New_AG_ID
    End Function


    Public Sub Fetch_AG_Currency()
        Dim C As New C
        Dim S As String = "Select Cr_ID,Cr_Equal From AGENTS_MENU_V Where AG_ID = '" & AG_ID & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Cr_CM.SelectedValue = C.Dr("Cr_ID")
                Cr_Equal_TXT.Text = C.Dr("Cr_Equal")
                If Cr_CM.SelectedValue > 1 Then
                    Pure_2_LB.Visible = True
                    T_Other_Cr_TXT.Visible = True
                    Cr_Equal_TXT.Visible = True
                Else
                    Pure_2_LB.Visible = False
                    T_Other_Cr_TXT.Visible = False
                    Cr_Equal_TXT.Visible = False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub


    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        FormType = 2

        ' إذا كان لون الجريد ليس أصفر فاتح (يعني الفاتورة مقفلة أو ملغية)
        If AGMetroGrid.BackgroundColor <> Color.LightYellow Then
            Beep()
            Exit Sub
        End If

        ' إذا تجاوزنا التشييك، نفتح شاشة التعديل
        If AGMetroGrid.Rows.Count > 0 Then
            Change_IM_Details.ShowDialog()
        End If
    End Sub

    Private Sub AGMetroGrid_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles AGMetroGrid.CellMouseDoubleClick
        ' التأكد من الضغط على صف حقيقي وليس الهيدر
        If e.RowIndex >= 0 Then
            ' التشييك على لون الخلفية لرفض التعديل للفواتير المقفلة
            If AGMetroGrid.BackgroundColor <> Color.LightYellow Then
                Beep()
                Exit Sub
            End If

            ' =========================================================
            ' هنا تضع باقي كودك الخاص بجلب بيانات الصنف للكمية والسعر
            ' =========================================================
            Try
                ' مثال لكودك الأصلي داخل هذا الحدث:
                ' IM_ID = AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value
                ' isShowingDetails = True
                ' ... الخ
            Catch ex As Exception
            End Try
        End If
    End Sub


    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = Pch_ID
        Bill_ID_Txt.Text = Pch_ID - 1
        Get_T_ID()
    End Sub
    ' تايمر للتحكم في وميض حالة الفورم (600 مللي ثانية تعطي وميضاً هادئاً وغير مزعج)
    Private WithEvents StateTimer As New Timer With {.Interval = 600}
    Private Sub StateTimer_Tick(sender As Object, e As EventArgs) Handles StateTimer.Tick
        If lblFormState IsNot Nothing Then
            lblFormState.Visible = Not lblFormState.Visible ' عكس الحالة (إظهار/إخفاء)
        End If
    End Sub
    Public Sub Get_T_ID()

        is_Select_Mode = True

        Dim C As New C
        Dim S As String = "Select T_ID From Agents_Balance_MV Where Pch_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Select_ExpBill(T_ID)
            Else
                MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                Bill_ID_Txt.Text = Tmp_Bill_ID
            End If
            CheckAccountingState()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        is_Select_Mode = False
    End Sub

    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        If Not String.IsNullOrWhiteSpace(Bill_ID_Txt.Text) Then
            Tmp_Bill_ID = Pch_ID
            Bill_ID_Txt.Text = Pch_ID + 1
            Get_T_ID()
        End If
    End Sub

    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        FormType = 2
        Switch_To_DV_Show()
    End Sub

    Private Sub Bill_ID_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Bill_ID_Txt.KeyDown
        If e.KeyCode = Keys.Return Then Get_T_ID()
        If e.KeyCode = Keys.Up Then Up_Bill_btn_Click(sender, e)
        If e.KeyCode = Keys.Down Then Down_Bill_btn_Click(sender, e)
    End Sub

    Private Sub Bill_ID_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_ID_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub عرضرصيدالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضرصيدالعميلToolStripMenuItem.Click
        MsgBox(Show_AG_T_Balance(AG_ID).ToString(), MsgBoxStyle.Information, "رصيد العميل : " & AG_Cm.Textt)
    End Sub

    Private Sub كشفحسابالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles كشفحسابالعميلToolStripMenuItem.Click
        Show_AG_Balance()
    End Sub

    Private Sub Show_AG_Balance()
        F_Balances = New Balances
        With F_Balances
            .AG_ID = AG_ID
            .AG_Cm.Set_IM_By_ID(AG_ID)
            .Fetch_AG_Currency()
            .Load_Data()
            .AllAgentsCheckBox.Enabled = False
            .AllRecieptsCheckBox.Checked = True
            .AllUsersCheckBox.Checked = True
            .AllTimeCheckBox.Checked = True
            .AG_MV_Prepare_To_Search()
            .ن.TabPages.Remove(.MetroTabPage2)
            .ن.TabPages.Remove(.MetroTabPage3)
            .ن.TabPages.Remove(.MetroTabPage4)
            '  .MetroTabControl1.TabPages.Remove(.MetroTabPage5)
            ' .MetroTabControl1.TabPages.Remove(.MetroTabPage6)
            .MenuStrip1.Visible = False
        End With
        F_Balances.ShowDialog()
    End Sub

    Private Sub إضافةكعميلجديدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إضافةكعميلجديدToolStripMenuItem.Click
        ADD_Fast_AG()
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

    Private Sub Calc_Dicount_Btn_Click(sender As Object, e As EventArgs) Handles Calc_Dicount_Btn.Click
        Prepare_Discount()
    End Sub

    Private Sub Prepare_Discount()
        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Discount_txt.Text = "0"
        Make_Discount()
    End Sub

    Private Sub Make_Discount()
        Disc = Convert.ToDouble(Discount_txt.Text) * Convert.ToDouble(Cr_Equal_TXT.Text)
        Discount_txt.Text = Disc
        Update_Discount(T_ID, Discount_txt.Text)
        Network_Edit_Tracker_insert(" تخفيض للفاتورة بقيمة:" & Disc.ToString, Bill_ID_Txt.Text, 7, 3)
        If Cr_CM.SelectedValue > 1 Then T_Other_Cr_TXT.Text = (Pure / Convert.ToDouble(Cr_Equal_TXT.Text)).ToString("n")
        Pure_txt.Text = (TOTAL - Disc).ToString("n")
        Pure = TOTAL - Disc
    End Sub

    Private Sub Discount_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Discount_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Discount_txt_TextChanged(sender As Object, e As EventArgs) Handles Discount_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Disc = 0
    End Sub

    Private Sub Cr_Equal_TXT_KeyDown(sender As Object, e As KeyEventArgs) Handles Cr_Equal_TXT.KeyDown
        If e.KeyCode = Keys.Return Then AG_Balance_Update_Equal_Value()
    End Sub

    Private Sub AG_Balance_Update_Equal_Value()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[AG_Balance_Update_Equal_Value]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Cr_ID", Cr_CM.SelectedValue)
        If Not String.IsNullOrWhiteSpace(Cr_Equal_TXT.Text) Then sqlComm.Parameters.AddWithValue("@Cr_Equal_Value", Cr_Equal_TXT.Text)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Private Sub Cr_Equal_TXT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cr_Equal_TXT.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Cr_Equal_TXT_TextChanged(sender As Object, e As EventArgs) Handles Cr_Equal_TXT.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub ADD_Dist_btn_Click(sender As Object, e As EventArgs) Handles ADD_Dist_btn.Click
        ADD_Pch_Exp.ShowDialog()
    End Sub

    Private Sub Remove_Dist_btn_Click(sender As Object, e As EventArgs) Handles Remove_Dist_btn.Click
        If Dist_DV.Rows.Count > 0 Then
            Beep()
            If MessageBox.Show("حذف الخدمة الموزعة", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then Pch_Exp_Values_DELETE()
        End If
    End Sub

    Public Sub Pch_Exp_Values_DELETE()
        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[Pch_Exp_Values_DELETE]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", Dist_DV.CurrentRow.Cells("Dist_T_ID_CL").Value)
        If SQL_SP_EXEC(sqlComm) = True Then
            Pch_Contents_SELECT_Bill()
            Pch_Contents_SELECT_EXP()
        End If
    End Sub

    Private Sub Cr_CM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cr_CM.SelectedIndexChanged
        If TypeName(Cr_CM.SelectedValue) = "Integer" Then
            If Cr_CM.SelectedValue = 1 Then
                Cr_Equal_TXT.Enabled = False
            Else
                Cr_Equal_TXT.Enabled = True
            End If


            If Cr_CM.SelectedValue = 1 Then
                AGMetroGrid.Columns("Price_By_Foriegn_Cr_CL").Visible = False
                AGMetroGrid.Columns("Cr_NAME_CL").Visible = False
                AGMetroGrid.Columns("Price_By_Equal_CL").Visible = False
                AGMetroGrid.Columns("Price_CL").HeaderText = "السعر"
                AGMetroGrid.Columns("NewSale_CL").HeaderText = "البيع"
                AGMetroGrid.Columns("TOTAL_CL").HeaderText = "الإجمالي"
            Else
                AGMetroGrid.Columns("Price_By_Foriegn_Cr_CL").Visible = True
                AGMetroGrid.Columns("Cr_NAME_CL").Visible = True
                AGMetroGrid.Columns("Price_By_Equal_CL").Visible = True
                AGMetroGrid.Columns("Price_CL").HeaderText = "السعر بالعملة المحلية"
                AGMetroGrid.Columns("NewSale_CL").HeaderText = "البيع بالعملة المحلية"
                AGMetroGrid.Columns("TOTAL_CL").HeaderText = "الإجمالي بالعملة المحلية"
            End If


        End If
    End Sub

    Private Sub Aggregate_Btn_Click(sender As Object, e As EventArgs) Handles Aggregate_Btn.Click
        Beep()
        If MessageBox.Show("تحديد سعر صرف البضاعة بالتساوي مع متوسط التكلفة ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then Pch_Details_Make_SP_SAME_COST()
    End Sub

    Private Sub Pch_Details_Make_SP_SAME_COST()
        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[Pch_Details_Make_SP_SAME_COST]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If SQL_SP_EXEC(sqlComm) = True Then Pch_Contents_SELECT_Bill()
    End Sub

    Public Sub IMTranPrintData()
        Try
            Dim pp As New ReportConnection
            pp.rp.Load(Application.StartupPath & "\reports\Invoice_Bill.rpt")
            pp.CrTables = pp.rp.Database.Tables
            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next
            With pp
                .rp.SetParameterValue(0, " تاريخ : " + DateTimeEx.Value)
                .rp.SetParameterValue(1, USER_NAME)
                .rp.SetParameterValue(2, SBill_Title_1 & vbNewLine & SBill_Title_2)
                .rp.SetParameterValue(3, IM_Qty_LB.Text)
                .rp.SetParameterValue(4, T_ID)
                .rp.SetParameterValue(5, Pure_txt.Text)
                .rp.SetParameterValue(6, "فاتـــورة مشتريــات")
                .rp.SetParameterValue(7, " العنوان : " + EX_ReferNumTextBox.Text)
                .rp.SetParameterValue(8, " الرقم الألي : " + Bill_ID_Txt.Text)
                .rp.SetParameterValue(9, " المـورد : " + AG_Cm.Textt + vbNewLine)
            End With
            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        IMTranPrintData()
    End Sub

    Private Sub Dist_DV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles Dist_DV.RowsAdded
        Calc_Total()
    End Sub

    Private Sub Dist_DV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles Dist_DV.RowsRemoved
        Calc_Total()
    End Sub

    Private Sub Discount_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Discount_txt.KeyDown
        If e.KeyCode = Keys.Return Then Prepare_Discount()
    End Sub

    Private Sub IM_btn_Click(sender As Object, e As EventArgs) Handles IM_btn.Click
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.ShowDialog()
    End Sub

    Private Sub تعديلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلToolStripMenuItem.Click
        FormType = 2
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then Change_IM_Details.ShowDialog()
    End Sub

    Private Sub عرضالتكلفةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضالتكلفةToolStripMenuItem.Click
        Show_IM_Cost(True, F_Pch.AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value, F_Pch.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value)
    End Sub

    Private Sub تعديلصلاحياتالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلصلاحياتالصنفToolStripMenuItem.Click
        Mang_IM_Valid_Notes.IM_ID = AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value
        Mang_IM_Valid_Notes.Bill_T_ID = AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
        Mang_IM_Valid_Notes.IM_NAME = AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
        Mang_IM_Valid_Notes.ShowDialog()
    End Sub

    Private Sub علاضبطاقةالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles علاضبطاقةالصنفToolStripMenuItem.Click
        isShowing_Trans = True
        Str_ = F_Pch.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.ShowDialog()
        isShowing_Trans = False
    End Sub

    Private Sub DeletedBillLabel_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DeletedBillLabel.MouseDoubleClick
        If U_Cancel_Pch = True Then
            Beep()
            If MessageBox.Show(" سيتم تراجع عن إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "تاكيد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                AG_Balance_UN_Void_Row(T_ID, Pch_ID, 7)
                Get_T_ID()
            End If
        End If
    End Sub

    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        If AG_Cm.TXT_ID.Text > 0 Then
            AG_ID = AG_Cm.TXT_ID.Text
            If is_Select_Mode = False Then
                Save_AG_Name(T_ID, AG_ID, On_Update)
                Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_Cm.Textt, Bill_ID_Txt.Text, 7, 3)
                AG_Balance_Update_Equal_Value()
            End If
            Fetch_AG_Currency()

        End If
    End Sub

    Private Sub تخفيضبنسبةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تخفيضبنسبةToolStripMenuItem.Click
        Dim F_Percent_Disc As New Percent_Disc
        F_Percent_Disc.T_ID = T_ID
        F_Percent_Disc.TOTAL = TOTAL
        F_Percent_Disc.ShowDialog()
        Select_ExpBill(T_ID)
    End Sub


End Class
