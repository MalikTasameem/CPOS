Imports System.Drawing
Imports System.Windows.Forms

Public Class Pch : Inherits System.Windows.Forms.Form
    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Public Receipts_DT As New DataTable
    Dim Indx_ID As Integer

    Public isShowingDetails As Boolean = False
    Public IM_ID As Integer = 0
    Dim IM_Dt As New DataTable
    Dim IM_QTY As Double = 0
    Public TOTAL As Double = 0
    Public AG_ID As Integer = 0
    Dim AG_Dt As New DataTable
    Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False
    Public Bill_DT As New DataTable
    Public Exp_DT As New DataTable
    Dim U_Cargo As Double = 1
    Dim ALL_QTY As Double = 0
    Public On_Update As Boolean
    Dim U_ID As Integer
    Public Pch_ID As Integer
    Dim AG_Balance As Double = 0
    Public Disc As Double = 0
    Public Pure As Double = 0
    Public Barcode_IM As String = ""
    'Private WithEvents PulseTimer As New System.Windows.Forms.Timer() With {.Interval = 30}
    'Private PulseFactor As Double = 0.0
    'Private PulseStep As Double = 0.05
    'Private BaseStateColor As System.Drawing.Color = System.Drawing.Color.Black
    'Private FadedColor As System.Drawing.Color = System.Drawing.Color.LightGray
    'Private Sub PulseTimer_Tick(sender As Object, e As EventArgs) Handles PulseTimer.Tick
    '    If lblFormState Is Nothing Then Return

    '    ' حساب معامل التوهج والخفوت بنعومة
    '    PulseFactor += PulseStep

    '    If PulseFactor >= 1.0 Then
    '        PulseFactor = 1.0
    '        PulseStep = -0.05 ' عكس الاتجاه للخفوت
    '    ElseIf PulseFactor <= 0.0 Then
    '        PulseFactor = 0.0
    '        PulseStep = 0.05 ' عكس الاتجاه للتوهج
    '    End If

    '    ' دمج الألوان (Color Interpolation)
    '    Dim r As Integer = CInt(BaseStateColor.R + (FadedColor.R - BaseStateColor.R) * PulseFactor)
    '    Dim g As Integer = CInt(BaseStateColor.G + (FadedColor.G - BaseStateColor.G) * PulseFactor)
    '    Dim b As Integer = CInt(BaseStateColor.B + (FadedColor.B - BaseStateColor.B) * PulseFactor)

    '    ' تطبيق اللون وإجبار الواجهة على التحديث الفوري (هنا السر في ظهور الوميض!)
    '    lblFormState.ForeColor = System.Drawing.Color.FromArgb(r, g, b)
    '    lblFormState.Refresh()
    'End Sub


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

    ' =========================================================
    ' 🌟 دالة التحكم في مؤشر حالة الفاتورة البصري (مع الوميض)
    ' =========================================================
    'Private Sub UpdateFormStateIndicator(ByVal StateText As String, ByVal StateColor As Color, Optional ByVal ShouldPulse As Boolean = False)
    '    If lblFormState IsNot Nothing Then
    '        lblFormState.Text = "⬤  " & StateText
    '        BaseStateColor = StateColor ' حفظ اللون الأصلي
    '        lblFormState.Visible = True

    '        If ShouldPulse Then
    '            PulseFactor = 0.0 ' تصفير العداد
    '            PulseStep = 0.05
    '            PulseTimer.Start() ' تشغيل تأثير التنفس
    '        Else
    '            PulseTimer.Stop() ' إيقاف التأثير
    '            lblFormState.ForeColor = BaseStateColor ' إرجاع اللون للسطوع الكامل
    '        End If
    '    End If
    'End Sub
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
            If MaxFormButton IsNot Nothing Then MaxFormButton.Tag = "GENERAL" If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Tag = "DELETE"

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
            If Show_IM_btn2 IsNot Nothing Then Show_IM_btn2.Tag = "GENERAL"
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

            If U_Cancel_Pch = False Then Delete_butt.Visible = False
            If isShowing_Trans = True Then
                Select_ExpBill(T_ID_Trans)
                SelectStateBt()
                New_butt.Enabled = False
                SearchButton.Enabled = False
            End If
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
        If AG_Panel IsNot Nothing Then AG_Panel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If Panel1 IsNot Nothing Then Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If BillNumPanel IsNot Nothing Then BillNumPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' الفواتير المعلقة

        ' بيانات المورد ورصيد الحساب (هذي اللي صايرة فيها السلاطة في الصورة)
        If AG_SH_txt IsNot Nothing Then AG_SH_txt.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' كومبو المورد الفعلي
        If Label25 IsNot Nothing Then Label25.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' رصيد الحساب
        If Label24 IsNot Nothing Then Label24.Anchor = AnchorStyles.Top Or AnchorStyles.Right ' كلمة "المورد :"

        ' أزرار التحكم بالجريد (يمين الجريد)
        If DGV_Control_btn IsNot Nothing Then DGV_Control_btn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If ADDCatButton IsNot Nothing Then ADDCatButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If RemoveCatButton IsNot Nothing Then RemoveCatButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' بيانات إضافية (أعلى اليسار)
        '  If Panel1 IsNot Nothing Then Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        ' If Panel4 IsNot Nothing Then Panel4.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        If Label1 IsNot Nothing Then Label1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub
    Public Sub UpdatePchStatusUI()
        If lblFormState Is Nothing Then Exit Sub

        If isVoid Then
            ' حالة الإلغاء
            lblFormState.Text = "فاتورة ملغيــــة"
            lblFormState.BackColor = Color.FromArgb(231, 76, 60) ' أحمر
            lblFormState.ForeColor = Color.White
        ElseIf isDepended Then
            ' حالة الإعتماد
            lblFormState.Text = "فاتورة معتمـــدة"
            lblFormState.BackColor = Color.FromArgb(46, 204, 113) ' أخضر
            lblFormState.ForeColor = Color.White
        Else
            ' حالة فاتورة جديدة أو قيد التحرير
            lblFormState.Text = "فاتورة جديــــدة"
            lblFormState.BackColor = Color.FromArgb(52, 152, 219) ' أزرق
            lblFormState.ForeColor = Color.White
        End If
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Get_Last_T_ID()
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
        AG_SH_txt.Enabled = True
        Show_IM_btn2.Enabled = True
        EX_ReferNumTextBox.Enabled = True
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        Ebable_CatFields()
        DiscountPanel.Enabled = True
        ADD_Dist_btn.Enabled = True
        Remove_Dist_btn.Enabled = True
        Cr_Equal_TXT.Enabled = True
        Aggregate_Btn.Enabled = True
        'AGMetroGrid.BackgroundColor = Color.LightYellow
        'AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
    End Sub

    Private Sub Disable_Fields()
        AG_SH_txt.Enabled = False
        Show_IM_btn2.Enabled = False
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
            'AGMetroGrid.BackgroundColor = Color.LightGreen
            'AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            AG_SH_txt.Enabled = False
            DeliveryingButton.Enabled = True
            Save_butt.Enabled = False
        Else
            isDepended = 0
            'AGMetroGrid.BackgroundColor = Color.LightYellow
            'AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            AG_SH_txt.Enabled = True
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
        AG_Grid.Visible = False
        AG_SH_txt.Enabled = True
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
            'AGMetroGrid.BackgroundColor = Color.IndianRed
            'AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed
            DiscountPanel.Enabled = False
            DeliveryingButton.Enabled = False
            Aggregate_Btn.Enabled = False
            UpdateFormStateIndicator("فاتورة ملغاة", Color.Red)
        Else
            If isDepended = False Then
                Save_butt.Enabled = True
                DiscountPanel.Enabled = True
                Print_btn.Enabled = False
                Enable_Fields()
                UpdateFormStateIndicator("محفوظة", Color.DodgerBlue)
            Else
                Print_btn.Enabled = True
                'AGMetroGrid.BackgroundColor = Color.LightGreen
                'AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
                DiscountPanel.Enabled = False
                Disable_Fields()
            End If
            Edit_butt.Enabled = True
            Edit_butt.Text = EditState
            DeletedBillLabel.Visible = False
            Delete_butt.Enabled = True
            DeliveryingButton.Enabled = True
            Aggregate_Btn.Enabled = False
            UpdateFormStateIndicator("مُرحّلة / معتمدة", Color.LimeGreen)
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
        AG_SH_txt.Clear()
        AG_Balance = 0
        Discount_txt.Clear()
        Total_txt.Clear()
        Pure_txt.Text = "0"
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        If On_Update = True Then Edit_butt_Click(sender, e)
        Call_New_Bill()
        UpdateFormStateIndicator("فاتورة جديدة", Color.Honeydew)
    End Sub

    Private Sub Call_New_Bill()
        If T_ID > 0 Then
            If MessageBox.Show("فتح فاتورة جديدة", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
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
            If String.IsNullOrWhiteSpace(AG_SH_txt.Text) = False And AG_ID = 0 Then
                MsgBox("حدد إسم العميل", MsgBoxStyle.Critical, "خطأ في الإعتماد")
                AG_SH_txt.Select()
            Else
                If String.IsNullOrWhiteSpace(AG_SH_txt.Text) Then
                    Fetch_ItemToList2()
                End If
                Beep()
                If MessageBox.Show(" حفظ الفاتــورة ؟", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.OK Then
                    Save_AG_Name(T_ID, AG_ID, On_Update)
                    Save_About(T_ID, Notes_txt.Text)
                    Save_ReferNum(T_ID, EX_ReferNumTextBox.Text)
                    Save_Date(T_ID, DateTimeEx)
                    AG_Balance_Update_Equal_Value()
                    Prepare_Discount()
                    If DependingBill(T_ID) = True Then Select_ExpBill(T_ID)
                End If
            End If
        End If
    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If isDepended = True Then
            If U_Pch_Update = True Then
                If On_Update = False Then
                    Beep()
                    If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟", "تعديل فاتورة", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        '  Edit_butt.BackColor = Color.GreenYellow
                        UpdateFormStateIndicator("قيد التعديل", Color.DarkOrange)
                        On_Update = True
                        AGMetroGrid.Enabled = True
                        'AGMetroGrid.BackgroundColor = Color.LightYellow
                        'AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                        ADDCatButton.Enabled = True
                        RemoveCatButton.Enabled = True
                        Ebable_CatFields()
                        Edit_butt.Text = "إيقاف التعديل"
                        Notes_txt.Enabled = True
                        DateTimeEx.Enabled = True
                        EX_ReferNumTextBox.Enabled = True
                        DiscountPanel.Enabled = True
                        AG_SH_txt.Enabled = True
                        Show_IM_btn2.Enabled = True
                        Aggregate_Btn.Enabled = True
                        If Cr_CM.SelectedValue > 1 And Cr_Equal_TXT.Visible = True Then Cr_Equal_TXT.Enabled = True
                    End If
                Else
                    Save_Total(T_ID, TOTAL, Disc)
                    Save_About(T_ID, Notes_txt.Text)
                    Save_ReferNum(T_ID, EX_ReferNumTextBox.Text)
                    Save_Date(T_ID, DateTimeEx)
                    Prepare_Discount()
                    On_Update = False
                    Edit_butt.Text = EditState
                    '   Edit_butt.BackColor = Color.WhiteSmoke
                    SelectStateBt()
                    Notes_txt.Enabled = False
                    DiscountPanel.Enabled = False
                    AG_SH_txt.Enabled = False
                    Show_IM_btn2.Enabled = False
                    Select_Pch_Receipt(T_ID)
                End If
            Else
                MsgBox("أنت غير مخول بتعديل فاتورة تم حفظها", MsgBoxStyle.Exclamation)
            End If
        Else
            If Edit_butt.Text = EditState Then
                Edit_butt.Text = "ح التعديل"
                Enable_Fields()
                'AGMetroGrid.BackgroundColor = Color.LightYellow
                'AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Else
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Edit_butt.Text = EditState
                Disable_Fields()
                SelectStateBt()
            End If
        End If
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
            UpdateFormStateIndicator("فاتورة ملغاة", Color.Crimson)
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

    Private Sub TreasuryCard_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Tr_Name_txtb_Enter(sender As Object, e As EventArgs)
        Arabic_Lang()
    End Sub

    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pure_txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub Calc_Total()
        TOTAL = 0
        Dim Dist_Values As Double = 0
        Dim QTY As Double = 0
        For i = 0 To AGMetroGrid.Rows.Count - 1
            TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("Total_CL").Value
            QTY += AGMetroGrid.Rows(i).Cells("QTY_CL").Value
        Next
        For j = 0 To Dist_DV.Rows.Count - 1
            If Dist_DV.Rows(j).Cells("isWithBill_CL").Value = False Then
                Dist_Values = Dist_Values + Dist_DV.Rows(j).Cells("Dist_Values_CL").Value
            End If
        Next
        Total_txt.Text = TOTAL.ToString(N_Point_Fter)
        Pure_txt.Text = (TOTAL - Disc).ToString(N_Point_Fter)
        Pure = TOTAL - Disc
        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "
        If Cr_CM.SelectedValue > 1 Then T_Other_Cr_TXT.Text = (Pure / Convert.ToDouble(Cr_Equal_TXT.Text)).ToString(N_Point_Fter)
    End Sub

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

    Public Sub Pch_Contents_SELECT_Bill()
        Bill_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Pch_Details_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Bill_DT)
        AGMetroGrid.DataSource = Bill_DT
        If AGMetroGrid.Rows.Count > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(AGMetroGrid.Rows.Count - 1).Cells("EX_Name_CL")
        Calc_Total()
    End Sub

    Public Sub Pch_Contents_SELECT_EXP()
        Exp_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[Pch_Details_SELECT_EXP_Dist]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Exp_DT)
        Dist_DV.DataSource = Exp_DT
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
        sqlComm.Parameters.AddWithValue("@Ag_name", AG_SH_txt.Text)
        sqlComm.Parameters.AddWithValue("@Barcode", "")
        sqlComm.Parameters.AddWithValue("@Type_ID", Suply_Type_ID)
        sqlComm.Parameters("@AG_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters.AddWithValue("@E_mail", "")
        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تمت إضافة العميــل", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" (من شاشة المشتريات) الزبون:" & AG_SH_txt.Text, 0, 27, 1)
            New_AG_ID = sqlComm.Parameters("@AG_ID").Value.ToString()
            Load_AG()
        End If
        Return New_AG_ID
    End Function

    Public Sub Load_AG()
        Dim c As New C
        Try
            AG_Dt.Clear()
            Dim s As String
            s = "select AG_ID,Ag_name,isnull(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE Ag_name Like '%" & AG_SH_txt.Text & "%' AND Type_ID IN ('" & Suply_Type_ID & "','" & General_AG_Type_ID & "')"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(AG_Dt)
            AG_Grid.DataSource = AG_Dt
            If AG_Dt.Rows.Count > 0 Then
                AG_Grid.Visible = True
                AG_Grid.Size = New Point(AG_Grid.Size.Width, 530)
            Else
                AG_Grid.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub GET_AG()
        Dim c As New C
        Try
            AG_Dt.Clear()
            Dim s As String
            s = "select Ag_name from Agents WHERE Ag_ID = '" & AG_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                AG_SH_txt.Text = c.Dr("Ag_name")
                '    AG_SH_txt.BackColor = Color.LightGoldenrodYellow
                AG_Grid.Visible = False
                Fetch_AG_Currency()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AG_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles AG_SH_txt.KeyDown
        If e.KeyCode = Keys.Down Then AG_Grid.Select()
        If e.KeyCode = Keys.Delete Then AG_SH_txt.Clear()
        If e.KeyCode = Keys.Return Then If AG_Grid.Visible = True Then Fetch_ItemToList2()
    End Sub

    Private Sub AG_SH_txt_Enter(sender As Object, e As EventArgs) Handles AG_SH_txt.Enter
        Set_Ar_Language()
    End Sub

    Private Sub AG_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles AG_SH_txt.TextChanged
        If AG_SH_txt.Text.Count > 0 Then
            Load_AG()
        Else
            AG_Grid.Visible = False
            AG_ID = Default_AG_ID
            Save_AG_Name(T_ID, AG_ID, On_Update)
            Fetch_AG_Currency()
        End If
        Check_AG_Pied()
    End Sub

    Private Sub Check_AG_Pied()
        If AG_ID = Default_AG_ID Then
            '  AG_SH_txt.BackColor = Color.LightGray
        Else
            '  AG_SH_txt.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub AG_Grid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles AG_Grid.CellClick
        Fetch_ItemToList2()
    End Sub

    Private Sub AG_Grid_KeyDown(sender As Object, e As KeyEventArgs) Handles AG_Grid.KeyDown
        If e.KeyCode = Keys.Return Then Fetch_ItemToList2()
        If e.KeyCode = Keys.Up Then If AG_Grid.CurrentRow.Index = 0 Then AG_SH_txt.Select()
    End Sub

    Public Sub Fetch_ItemToList2()
        If AG_Grid.Rows.Count > 0 Then
            AG_ID = AG_Grid.CurrentRow.Cells(0).Value
            AG_SH_txt.Text = AG_Grid.CurrentRow.Cells(1).Value
            AG_Balance = AG_Grid.CurrentRow.Cells(2).Value
            ' AG_SH_txt.BackColor = Color.LightGoldenrodYellow
            AG_Grid.Visible = False
            Save_AG_Name(T_ID, AG_ID, On_Update)
            Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_SH_txt.Text, Bill_ID_Txt.Text, 7, 3)
            Fetch_AG_Currency()
            AG_Balance_Update_Equal_Value()
        End If
    End Sub

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

    Private Sub Show_IM_btn2_Click(sender As Object, e As EventArgs) Handles Show_IM_btn2.Click
        If AG_Grid.Visible = True Then
            AG_Grid.Visible = False
        Else
            Fill_All_AG()
            AG_Grid.Visible = True
            AG_Grid.Size = New Point(AG_Grid.Size.Width, 530)
        End If
    End Sub

    Private Sub Fill_All_AG()
        Try
            Dim C As New C
            AG_Dt.Clear()
            Dim s As String = "SELECT top 100 AG_ID,Ag_name,ISNULL(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE Type_ID IN ('" & Suply_Type_ID & "','" & General_AG_Type_ID & "') Order by Ag_name ASC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(AG_Dt)
            AG_Grid.DataSource = AG_Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub MakeBarcode_btn_Click(sender As Object, e As EventArgs) Handles MakeBarcode_btn.Click
        printbarcode.Auto_Print = True
        printbarcode.ShowDialog()
        printbarcode.Auto_Print = False
    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        FormType = 2

        ' بدلاً من فحص اللون، نفحص المتغير البرمجي لحالة التعديل (On_Update)
        ' نستخدم AndAlso لتسريع التنفيذ وتفادي الأخطاء
        If On_Update = True AndAlso AGMetroGrid.Rows.Count > 0 Then
            Change_IM_Details.ShowDialog()
        End If
    End Sub
    Private Sub AGMetroGrid_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles AGMetroGrid.CellMouseDoubleClick
        ' التأكد من الضغط على صف حقيقي وليس الهيدر
        If e.RowIndex >= 0 Then
            ' إذا كانت الفاتورة معتمدة أو ملغية، نمنع التعديل المباشر
            If isDepended Or isVoid Then
                Beep()
                Exit Sub
            End If

            Try
                ' جلب بيانات الصنف (تأكد أن أسماء الأعمدة مطابقة للديزاينر عندك)
                Change_IM_Details.ShowDialog()

            Catch ex As Exception
                MsgBox("خطأ في جلب بيانات الصنف: " & ex.Message)
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
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
        MsgBox(Show_AG_T_Balance(AG_ID).ToString(), MsgBoxStyle.Information, "رصيد العميل : " & AG_SH_txt.Text)
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
            .MetroTabControl1.TabPages.Remove(.MetroTabPage2)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage3)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage4)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage5)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage6)
            .MenuStrip1.Visible = False
        End With
        F_Balances.ShowDialog()
    End Sub

    Private Sub إضافةكعميلجديدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إضافةكعميلجديدToolStripMenuItem.Click
        ADD_Fast_AG()
    End Sub

    Private Sub ADD_Fast_AG()
        If AG_ID <> Default_AG_ID Or GET_AG_NO_SPACES(AG_SH_txt.Text) = True Then
            MsgBox("هذا العميل موجود بالفعل", MsgBoxStyle.Critical, "إضافة عميل")
        ElseIf String.IsNullOrWhiteSpace(AG_SH_txt.Text) Then
            MsgBox("أدخل اسم العميل الجديد", MsgBoxStyle.Exclamation)
            AG_SH_txt.Select()
        Else
            Beep()
            If MessageBox.Show(" إضافة " + AG_SH_txt.Text + " إلى قائمة العملاء ", " إضافة العميل ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
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

    Private Sub AG_Grid_VisibleChanged(sender As Object, e As EventArgs) Handles AG_Grid.VisibleChanged
        If AG_Grid.Visible = True Then
            Me.Controls.Add(AG_Grid)
            AG_Grid.BringToFront()
            AG_Grid.Location = New Point(AG_Panel.Location.X, AG_Panel.Location.Y + AG_Panel.Size.Height + 1)
        Else
            AG_Panel.Controls.Add(AG_Grid)
            AG_Grid.Location = New Point(AG_SH_txt.Location.X, AG_SH_txt.Location.Y + AG_SH_txt.Size.Height + 1)
        End If
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
                .rp.SetParameterValue(9, " المـورد : " + AG_SH_txt.Text + vbNewLine)
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

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If IM_ID > 0 Then
            Show_IM_Details.IM_ID = IM_ID
            Show_IM_Details.ShowDialog()
        End If
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

    Private Sub DeletedBillLabel_Click(sender As Object, e As EventArgs) Handles DeletedBillLabel.Click

    End Sub



    Private Sub تخفيضبنسبةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تخفيضبنسبةToolStripMenuItem.Click
        Dim F_Percent_Disc As New Percent_Disc
        F_Percent_Disc.T_ID = T_ID
        F_Percent_Disc.TOTAL = TOTAL
        F_Percent_Disc.ShowDialog()
        Select_ExpBill(T_ID)
    End Sub

    Private Sub ADD_New_IM_btn_Click(sender As Object, e As EventArgs)
    End Sub

End Class