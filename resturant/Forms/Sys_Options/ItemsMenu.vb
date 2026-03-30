Imports System.Data.SqlClient
Imports System.IO

Public Class ItemsMenu

    Dim Rs As New Resizer
    Public IM_ID As Integer
    Dim IM_Count = 0
    Dim Max_Count As Integer
    Dim Select_GM_ID As Integer = 0
    Dim IM_Dt As New DataTable
    Dim IM_Dt_2 As New DataTable
    Dim dv As DataView
    Dim Data As Byte()
    Dim GM_ID As Integer = 0
    Dim IM_Cost As Double = 0

    Dim Unit_Dt As New DataTable
    Dim ALERT_Q_Dt As New DataTable
    Dim IM_Def_Unit_Cargo As Double = 1

    Dim isValid As Boolean = False

    Dim Get_COUNTER As Boolean = False
    Public IM_PH_PATH As String = ""
    Dim Valid_St As String = "لا"
    Dim isShort_St As String = "لا"



    Dim is_New_IM As Boolean

    Private Sub NonePhotoButton_Click(sender As Object, e As EventArgs) Handles NonePhotoButton.Click
        If IMPictureBox.Image IsNot Nothing Then IMPictureBox.Image = Nothing
    End Sub

    Private Sub ItemsMenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'F_MainForm.Fill_ALL_IM()
        Me.Dispose()
    End Sub


    Private Sub CHeck_IM_Default_Unit()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "CHeck_IM_Default_Unit"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            SQL_SP_EXEC(C.Com)
        End With
    End Sub

    'Private Sub item_menu_Delete_DisableRows()
    '    Dim c As New C
    '    With c.Com
    '        .Connection = c.Con
    '        .CommandText = "item_menu_Delete_DisableRows"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@USER_ID", USER_ID)
    '    End With
    '    SQL_SP_EXEC(c.Com)
    'End Sub

    Private Sub ItemsMenu_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If NewEmpButton.Enabled = True Then NewEmpButton_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If SaveButton.Enabled = True Then SaveButton_Click(sender, e)
        If e.KeyCode = Keys.F8 Then
            Barcode_Search_txt.Clear()
            Barcode_Search_txt.Select()
        End If
    End Sub

    Private Sub ItemsMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")

        Try
            ' =========================================================
            ' 🌟 1. تعيين التاجات للأدوات الأصلية لتفعيل الثيم (بدون Loop)
            ' =========================================================
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed
            TabControl1.SizeMode = TabSizeMode.Fixed ' اختياري لضبط الأحجام
            TabControl1.Appearance = TabAppearance.FlatButtons ' هذا السطر يدمر الخلفية البيضاء الإفتراضية للويندوز
            TabControl1.ItemSize = New Size(140, 35) ' تكبير حجم التابات لتصبح مثل الأزرار الأنيقة
            ' الشريط العلوي (TitleBar) والعنوان
            If TitleBar_Panel IsNot Nothing Then TitleBar_Panel.Tag = "HEADER"
            If Title_Label IsNot Nothing Then Title_Label.Tag = "TITLE_TRANSPARENT"

            ' أزرار التحكم في الفورم (إغلاق، تكبير، تصغير)
            If ExitFormButton IsNot Nothing Then ExitFormButton.Tag = "DELETE"
            If MaximizeFormButton IsNot Nothing Then MaximizeFormButton.Tag = "GENERAL"
            If MinimizeFormButton IsNot Nothing Then MinimizeFormButton.Tag = "GENERAL"
            ' داخل دالة ItemsMenu_Load تحت استدعاء تطبيق الثيم

            ' حاويات التبويب والتنظيم
            If TabControl1 IsNot Nothing Then TabControl1.Tag = "TRANSPARENT"
            If TabPage1 IsNot Nothing Then TabPage1.Tag = "TRANSPARENT"
            If TouchTabPage IsNot Nothing Then TouchTabPage.Tag = "TRANSPARENT"
            If TabPage4 IsNot Nothing Then TabPage4.Tag = "TRANSPARENT"
            If Frm_TabPage IsNot Nothing Then Frm_TabPage.Tag = "TRANSPARENT"
            If Panel1 IsNot Nothing Then Panel1.Tag = "TRANSPARENT"
            For Each tp As TabPage In TabControl1.TabPages
                tp.BackColor = Me.BackColor ' ستأخذ لون خلفية الفورم التي تلونت بالثيم
            Next

            ' أزرار الإجراءات الأساسية
            If NewEmpButton IsNot Nothing Then NewEmpButton.Tag = "GENERAL"
            If SaveButton IsNot Nothing Then SaveButton.Tag = "SAVE"
            If DeleteButton IsNot Nothing Then DeleteButton.Tag = "DELETE"
            If IM_MV_btn IsNot Nothing Then IM_MV_btn.Tag = "GENERAL"
            If MakeBarcode_btn IsNot Nothing Then MakeBarcode_btn.Tag = "GENERAL"
            If Recount_Cost_btn IsNot Nothing Then Recount_Cost_btn.Tag = "GENERAL"

            ' أزرار العمليات الفرعية
            If ADD_NewGM_Btn IsNot Nothing Then ADD_NewGM_Btn.Tag = "GENERAL"
            If Show_IM_btn IsNot Nothing Then Show_IM_btn.Tag = "GENERAL"
            If IMPH_Btn IsNot Nothing Then IMPH_Btn.Tag = "GENERAL"
            If IMPH_None_btn IsNot Nothing Then IMPH_None_btn.Tag = "GENERAL"
            If Open_Camera_btn IsNot Nothing Then Open_Camera_btn.Tag = "GENERAL"
            If InsertU_btn IsNot Nothing Then InsertU_btn.Tag = "GENERAL"
            If DeleteU_btn IsNot Nothing Then DeleteU_btn.Tag = "DELETE"
            If ADD_ST_ALERT_QTY_btn IsNot Nothing Then ADD_ST_ALERT_QTY_btn.Tag = "GENERAL"
            If REMOVE_ST_ALERT_QTY_btn IsNot Nothing Then REMOVE_ST_ALERT_QTY_btn.Tag = "DELETE"

            ' =========================================================
            ' 🌟 2. تطبيق الثيم برمجياً
            ' =========================================================
            ThemeManager.ApplyThemeToForm(Me)

        Catch ex As Exception
            ' تفادي أي خطأ إذا كانت إحدى الأدوات غير محملة
        End Try

        Rs.FindAllControls(Me)
        Load_GM()
        Load_Units(IM_Unit_cm)
        Coutnt_IM()
        Load_Sys()
        Load_GM_Groups()
        Check_Sys_Featurs()
        Make_Hints()
        NewEmpButton_Click(sender, e)
        If isShowing_Trans = True Then
            IM_SH_txt.Text = Str_
            Begin_Fetch()
        End If
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or &H20000
            Return cp
        End Get
    End Property
    Private Sub TabControl1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = TabControl1.TabPages(e.Index)

        Dim isSelected As Boolean = (e.State And DrawItemState.Selected) = DrawItemState.Selected

        ' 1. مسح الأرضية بالكامل بلون الفورم
        g.FillRectangle(New SolidBrush(ThemeManager.FormBackColor), e.Bounds)

        ' 2. إعداد الألوان
        Dim bgBrush As Brush
        Dim textBrush As Brush

        If isSelected Then
            bgBrush = New SolidBrush(ThemeManager.AccentColor) ' لون بارز للتاب المفتوح
            textBrush = New SolidBrush(Color.White)
        Else
            bgBrush = New SolidBrush(ThemeManager.CardBackColor)
            textBrush = New SolidBrush(ThemeManager.TextPrimaryColor)
        End If

        ' 3. رسم مربع التاب 
        Dim rect As Rectangle = e.Bounds
        rect.Inflate(-1, -1)
        g.FillRectangle(bgBrush, rect)

        ' 4. رسم النص في المنتصف مع دعم اتجاه اليمين لليسار (RTL)
        Dim sf As New StringFormat() With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }
        g.DrawString(tp.Text, TabControl1.Font, textBrush, rect, sf)
    End Sub
    Private Sub MaximizeFormButton_Click(sender As Object, e As EventArgs) Handles MaximizeFormButton.Click
        If Me.WindowState = FormWindowState.Normal Then
            ' تكبير الفورم ليملا الشاشة باستثناء شريط المهام
            Me.MaximumSize = Screen.FromHandle(Me.Handle).WorkingArea.Size
            Me.WindowState = FormWindowState.Maximized
            MaximizeFormButton.Text = "❐" ' تغيير الرمز لـ استعادة
        Else
            ' إرجاع الفورم لحجمه الطبيعي
            Me.WindowState = FormWindowState.Normal
            MaximizeFormButton.Text = "⬜" ' تغيير الرمز لـ تكبير
        End If
    End Sub

    ' إضافة سريعة لزر التصغير (لتنزيل الفورم لشريط المهام)
    Private Sub MinimizeFormButton_Click(sender As Object, e As EventArgs) Handles MinimizeFormButton.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Make_Hints()
        SendMessage(Barcode_Search_txt.Handle, &H1501, 0, "أدخل باركود صنف للبحث")
        SendMessage(IM_SH_txt.Handle, &H1501, 0, "إبحث عن إسم صنف أو أدخل صنف جديد")
    End Sub

    Private Sub Check_Sys_Featurs()
        If S_Frm = False Then
            TabControl1.TabPages.Remove(Frm_TabPage)
            IM_Type_cm.Items.RemoveAt(2)
        End If

        If SScreenDefault = 0 Then TabControl1.TabPages.Remove(TouchTabPage)
        Markter_Panel.Visible = S_Marketers
        isValid_CB.Visible = S_IM_Valid


        Cost_Panel.Visible = U_SB_Show_IM_COST
        GroupBox1.Visible = U_SB_Show_IM_COST
        Recount_Cost_btn.Visible = U_SB_Show_IM_COST
    End Sub

    Public Sub Load_GM_Groups()
        Dim c As New C
        Dim s As String = "select Grp_ID,Grp_Name from Comp_Groups"
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        GM_Group_CM.DataSource = dt
        GM_Group_CM.DisplayMember = "Grp_Name"
        GM_Group_CM.ValueMember = "Grp_ID"
        GM_Group_CM.SelectedIndex = 0
    End Sub

    Private Sub Load_Sys()

        Dim c As New C
        Try
            Dim s As String
            s = "select ST_ID,ST_name from STORES ORDER By ST_ID ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            ST_cm.DataSource = c.Dt
            ST_cm.DisplayMember = "ST_name"
            ST_cm.ValueMember = "ST_ID"
            ST_cm.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Unit_DataGridView.Columns("Min_SP_CL").Visible = S_Allow_MinSP
        Unit_DataGridView.Columns("Min_SP_2_CL").Visible = S_Allow_MinSP
        Min_SP_Panel.Visible = S_Allow_MinSP
    End Sub

    Public Sub Clear_Fields()
        IM_SH_txt.Clear()
        IMSaleNameTextBox.Clear()
        BarCode_txt.Clear()
        IsActiceCheckBox.Checked = True
        IMPictureBox.Image = Nothing
        IM_Type_cm.SelectedIndex = -1
        isValid_CB.Checked = False
        IM_ID = 0
        UnitError.Clear()
        BKNoneColoreCheckBox.Checked = True
        FKNoneColoreCheckBox.Checked = True
        isChangePriceCheckBox.Checked = False
        IM_ViewerButton.Text = ""
        IM_ViewerButton.Image = Nothing
        IM_ViewerButton.BackColor = System.Drawing.SystemColors.Info
        Unit_Dt.Clear()
        ALERT_Q_Dt.Clear()
        WinPrice_Lb.Text = "000"
        IM_Cost_txt.Clear()
        IM_BoxCost_txt.Clear()
        IM_Num_txt.Clear()
        IM_All_Qty_txt.Text = 0
        Qty_Unit_Lb.Text = ""
        IM_FRM_txt.Clear()
        Barcode_SH_txt.Clear()

        Me.IM_Photo.Image = Nothing
        Me.IM_Photo.BackColor = System.Drawing.SystemColors.ButtonFace
        Notes_txt.Clear()
        Markter_Val_txt.Clear()
    End Sub

    Private Sub ChoasePicureButton_Click(sender As Object, e As EventArgs) Handles ChoasePicureButton.Click
        Dim OpenFL As New OpenFileDialog With {.Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico", _
                                               .Multiselect = False, .Title = "إختر صورة"}
        If OpenFL.ShowDialog = Windows.Forms.DialogResult.OK Then
            IMPictureBox.Image = Image.FromFile(OpenFL.FileName)
        End If
    End Sub

    Private Sub NewEmpButton_Click(sender As Object, e As EventArgs) Handles NewEmpButton.Click
        TabControl1.Enabled = True
        ADD_New_IM()
    End Sub


    Private Sub ADD_New_IM()
        TabControl1.Enabled = True
        TabControl1.SelectedTab = TabPage1
        Clear_Fields()
        SaveButton.Enabled = True
        DeleteButton.Enabled = False
        Insert_IM()
    End Sub


    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        If GM_Serach.SelectedValue = 0 Then
            MsgBox("حدد مجموعة الصنف", MsgBoxStyle.Exclamation)
        Else
            If ValidateChildren() = True Then Confirm_IM()
        End If
    End Sub

    Private Sub IM_Check_Barcode()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Barcode"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Barcode", BarCode_txt.Text)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then

                If .Parameters("@F").Value > 0 Then
                    BarError.SetError(BarCode_txt, "باركود متكرر")
                    BarCode_txt.Select()
                    BarCode_txt.Focus()
                Else
                    IM_Units_insert()
                End If

            End If
        End With
    End Sub

    Private Sub IMPriceTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles BarCode_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Down : Price_txt.Select()
        End Select
    End Sub


    Public Sub Search_IM()
        Dim c As New C
        Try
            IM_Dt.Clear()

            'Dim words As String() = IM_SH_txt.Text.Split(New Char() {" "c})
            Dim Str As String = ""
            'If words.Length() = 1 Then
            Str = "select IM_ID,item_name from IM_Active_V WHERE item_name Like '%" & IM_SH_txt.Text & "%' Order by item_name ASC"
            'Else
            '    Str = "select IM_ID,item_name from IM_Active_V WHERE item_name Like '%" & words(0) & "%' AND  item_name Like '%" & words(1) & "%' Order by item_name ASC"
            'End If

            c.Da = New SqlClient.SqlDataAdapter(Str, c.Con)
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

    'Public Sub Load_Units()
    '    Dim c As New C
    '    Try

    '        Dim sql As String = " select U_ID,U_Name from Units "
    '        c.Da = New SqlClient.SqlDataAdapter(sql, c.Con)
    '        c.Da.Fill(c.Dt)
    '        IM_Unit_cm.DataSource = c.Dt
    '        IM_Unit_cm.DisplayMember = "U_Name"
    '        IM_Unit_cm.ValueMember = "U_ID"
    '        IM_Unit_cm.SelectedIndex = 0
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

    Private Sub Load_GM()
        Try
            Get_COUNTER = False
            Dim c As New C
            Dim s As String = "select GM_ID,GM_Name FROM General_Menu"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            GM_Serach.DataSource = c.Dt
            GM_Serach.DisplayMember = "GM_Name"
            GM_Serach.ValueMember = "GM_ID"
            If GM_ID > 0 Then GM_Serach.SelectedValue = GM_ID
            Get_COUNTER = True
            Get_GM_IM_COUNTER()
        Catch ex As Exception
            MsgBox(ex.Message)
            Get_COUNTER = False
        End Try
    End Sub


    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        Beep()
        If MessageBox.Show(" تـأكيــد حــذف الصــنف " + IM_Name_ToolStrip.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, _
                      MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
            Delete_IM()
        End If

    End Sub

    Private Sub IMSaleNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles IMSaleNameTextBox.TextChanged
        IM_ViewerButton.Text = IMSaleNameTextBox.Text
    End Sub

    Private Sub ChoasePicureButton_KeyDown(sender As Object, e As KeyEventArgs) Handles ChoasePicureButton.KeyDown
        If e.KeyCode = Keys.Down Then NonePhotoButton.Select()
    End Sub

    Private Sub NonePhotoButton_KeyDown(sender As Object, e As KeyEventArgs) Handles NonePhotoButton.KeyDown
        If e.KeyCode = Keys.Down Then
            GM_Serach.DroppedDown = True
            GM_Serach.Select()
        End If
    End Sub

    Private Sub IsActiceCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles IsActiceCheckBox.CheckedChanged
        CB_CHecked(sender)
    End Sub


    Private Sub isChangePriceCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles isChangePriceCheckBox.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub CopyNameButton_Click(sender As Object, e As EventArgs) Handles CopyNameButton.Click
        IMSaleNameTextBox.Text = IM_SH_txt.Text
    End Sub

    Private Sub NoneColoreCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles BKNoneColoreCheckBox.CheckedChanged
        If sender.Checked = True Then
            sender.ForeColor = Color.DarkGreen
            BKPanel.BackColor = Nothing
            BKChoaseButton.Enabled = False
            IM_ViewerButton.BackColor = System.Drawing.SystemColors.Info
        Else
            sender.ForeColor = Color.Firebrick
            BKChoaseButton.Enabled = True
        End If
    End Sub

    Private Sub FKNoneColoreCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles FKNoneColoreCheckBox.CheckedChanged
        If sender.Checked = True Then
            sender.ForeColor = Color.DarkGreen
            FKPanel.BackColor = Nothing
            FKChoaseButton.Enabled = False
            IM_ViewerButton.ForeColor = Color.Black
        Else
            sender.ForeColor = Color.Firebrick
            FKChoaseButton.Enabled = True
        End If
    End Sub



    Private Sub BKChoaseButton_Click(sender As Object, e As EventArgs) Handles BKChoaseButton.Click

        Using dlg As New ColorDialog
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                'user selected something (and clicked ok)
                BKPanel.BackColor = dlg.Color
            End If
        End Using

    End Sub

    Private Sub FKChoaseButton_Click(sender As Object, e As EventArgs) Handles FKChoaseButton.Click
        Using dlg As New ColorDialog
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                'user selected something (and clicked ok)
                FKPanel.BackColor = dlg.Color
            End If
        End Using
    End Sub


    Private Sub BKPanel_BackColorChanged(sender As Object, e As EventArgs) Handles BKPanel.BackColorChanged
        IM_ViewerButton.BackColor = BKPanel.BackColor
    End Sub

    Private Sub FKPanel_BackColorChanged(sender As Object, e As EventArgs) Handles FKPanel.BackColorChanged
        IM_ViewerButton.ForeColor = FKPanel.BackColor
    End Sub

    Public Sub Coutnt_IM()
        Dim c As New C
        Dim s As String = "select Count(IM_ID) AS Count from IM_menu WHERE Row_Enabled = 1"
        c.Com = New SqlClient.SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                TxTTotalM.Text = c.Dr("Count").ToString
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()
    End Sub


    Public Sub Insert_IM()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "item_menu_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", 0)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters.AddWithValue("@isStore", IM_Default_Stut)
            If GM_ID > 0 Then .Parameters.AddWithValue("@GM_ID", GM_ID)
            .Parameters("@IM_ID").Direction = ParameterDirection.Output
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            IM_ID = c.Com.Parameters("@IM_ID").Value.ToString()
            Fetch_IM()
            IM_SH_txt.Select()
            isValid_CB.Checked = isValid
        End If

    End Sub

    Private Sub Delete_IM()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "item_menu_delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", IM_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم حــذف الصــنف", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert("" & IM_Name_ToolStrip.Text, IM_ID, 20, 2)
            Coutnt_IM()
            Clear_Fields()
            Load_Units(IM_Unit_cm)
            Load_GM()
            ADD_New_IM()
        End If

    End Sub

    Private Sub Fetch_IM()
        Dim c As New C
        Dim S As String

        S = "select * from IM_All_With_No_Enable_V where IM_ID ='" & IM_ID & "'"  'AND IM_ID BETWEEN " & START_ID & " AND " & END_ID


        c.Com = New SqlCommand(S, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                IM_ID = c.Dr("IM_ID")
                BarCode_txt.Clear()
                IM_BoxCost_txt.Clear()
                Me.GM_Serach.SelectedValue = c.Dr("GM_ID")
                Select_GM_ID = c.Dr("GM_ID")
                Me.IM_SH_txt.Text = c.Dr("item_name")
                Me.IMSaleNameTextBox.Text = c.Dr("item_nameSales")
                Me.IsActiceCheckBox.Checked = c.Dr("isActive")
                isChangePriceCheckBox.Checked = c.Dr("isChangePrice")
                Me.IM_Type_cm.SelectedIndex = c.Dr("isStore")
                isValid_CB.Checked = c.Dr("isValid")
                Me.IM_Cost_txt.Text = c.Dr("Cost")
                IM_Cost = c.Dr("Cost")
                If IsDBNull(c.Dr("photo")) = False Then
                    Data = DirectCast(c.Dr("photo"), Byte())
                    Dim MS As New MemoryStream(Data)
                    Me.IMPictureBox.Image = Image.FromStream(MS)
                Else
                    Me.IMPictureBox.Image = Nothing
                    Me.IMPictureBox.BackColor = System.Drawing.SystemColors.Info
                End If

                If IsDBNull(c.Dr("BK_R")) Then

                    Me.BKPanel.BackColor = Nothing
                    Me.BKNoneColoreCheckBox.Checked = True
                    Me.IM_ViewerButton.BackColor = System.Drawing.SystemColors.Info
                Else
                    Me.BKNoneColoreCheckBox.Checked = False
                    Me.BKPanel.BackColor = Color.FromArgb(c.Dr("BK_R"), c.Dr("BK_G"), c.Dr("BK_B"))
                End If

                If IsDBNull(c.Dr("FK_R")) Then
                    Me.FKPanel.BackColor = Nothing
                    Me.FKNoneColoreCheckBox.Checked = True
                    Me.IM_ViewerButton.ForeColor = Color.Black
                Else
                    Me.FKNoneColoreCheckBox.Checked = False
                    Me.FKPanel.BackColor = Color.FromArgb(c.Dr("FK_R"), c.Dr("FK_G"), c.Dr("FK_B"))
                End If

                isShortcut_CB.Checked = c.Dr("is_Shortcut")
                IM_Num_txt.Text = c.Dr("IM_Num")
                IM_Name_ToolStrip.Text = c.Dr("item_name")
                ToolStripStatusLabel4.Text = c.Dr("UserName")
                ID_ToolStripLabel.Text = c.Dr("IM_ID")
                GM_Group_CM.SelectedValue = c.Dr("Grp_ID")
                Markter_Val_txt.Text = c.Dr("Markter_Val")
                ToolStripStatusLabel9.Text = c.Dr("Date")
                IMDataGridViewX.Visible = False

                If c.Dr("Row_Enabled") = False Then
                    IM_Case_Lb.Text = "إدخال جديـد"
                    IM_Case_Lb.ForeColor = Color.DarkGreen
                    is_New_IM = True
                Else
                    IM_Case_Lb.Text = "تعديـل صنف"
                    IM_Case_Lb.ForeColor = Color.DarkRed
                    DeleteButton.Enabled = True
                    is_New_IM = False
                End If

                If IsDBNull(c.Dr("IM_Full_Photo")) = False Then
                    Try
                        Me.IM_Photo.Image = Image.FromFile(System.IO.Path.GetFullPath(c.Dr("IM_Full_Photo")))
                        IM_PH_PATH = System.IO.Path.GetFullPath(c.Dr("IM_Full_Photo"))
                    Catch ex As Exception
                        MsgBox("تأكد من مسار الصورة" + vbNewLine + ex.Message, MsgBoxStyle.Exclamation, "")
                    End Try

                Else
                    Me.IM_Photo.Image = Nothing
                    Me.IM_Photo.BackColor = System.Drawing.SystemColors.ButtonFace
                End If

                Me.Notes_txt.Text = c.Dr("Notes")

                'If ColumnExists(c.Dr, "is_Rsv") = True Then

                '    If c.Dr("is_Rsv") = 1 Then
                '        is_Rsv_CB.Checked = True
                '    Else
                '        is_Rsv_CB.Checked = False
                '    End If



                IM_Units_Select()
                    IM_Select_Qty()


                    IM_Formating_Menu_Select()
                    IM_Qty_Alert_Select()

                End If
                SaveButton.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

    End Sub


    Function ColumnExists(reader As SqlDataReader, columnName As String) As Boolean
        Dim schemaTable As DataTable = reader.GetSchemaTable()
        If schemaTable IsNot Nothing Then
            For Each row As DataRow In schemaTable.Rows
                If row("ColumnName").ToString().Equals(columnName, StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function




    Public Sub IM_Select_Qty()
        Dim c As New C
        c.Str = "select ISNULL(SUM(QTY),0) AS QTY ,U_Name from IM_STORE_View WHERE IM_ID = '" & IM_ID & "' GROUP BY U_NAME"
        c.Com = New SqlClient.SqlCommand(c.Str, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                IM_All_Qty_txt.Text = c.Dr("QTY")
                Qty_Unit_Lb.Text = c.Dr("U_Name")
            Else
                IM_All_Qty_txt.Text = 0
                Qty_Unit_Lb.Text = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()
    End Sub

    Private Sub Confirm_IM()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "item_menu_update"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@item_name", Me.IM_SH_txt.Text)
            '.Parameters.AddWithValue("@item_nameSales", Me.IMSaleNameTextBox.Text + " ")
            .Parameters.AddWithValue("@item_nameSales", Me.IMSaleNameTextBox.Text)
            .Parameters.AddWithValue("@GM_ID", Me.GM_Serach.SelectedValue)
            .Parameters.AddWithValue("@isActive", Me.IsActiceCheckBox.Checked)
            If Not IMPictureBox.Image Is Nothing Then .Parameters.AddWithValue("@photo", ConvertImage(Me.IMPictureBox.Image))
            .Parameters.AddWithValue("@isStore", Me.IM_Type_cm.SelectedIndex)
            .Parameters.AddWithValue("@isValid", Me.isValid_CB.Checked)
            .Parameters.AddWithValue("@isChangePrice", Me.isChangePriceCheckBox.Checked)
            If String.IsNullOrWhiteSpace(IM_Cost_txt.Text) = False Then .Parameters.AddWithValue("@Cost", IM_Cost_txt.Text)
            If Me.BKNoneColoreCheckBox.Checked = False Then
                .Parameters.AddWithValue("@BK_R", Me.BKPanel.BackColor.R)
                .Parameters.AddWithValue("@BK_G", Me.BKPanel.BackColor.G)
                .Parameters.AddWithValue("@BK_B", Me.BKPanel.BackColor.B)
            End If

            If Me.FKNoneColoreCheckBox.Checked = False Then
                .Parameters.AddWithValue("@FK_R", Me.FKPanel.BackColor.R)
                .Parameters.AddWithValue("@FK_G", Me.FKPanel.BackColor.G)
                .Parameters.AddWithValue("@FK_B", Me.FKPanel.BackColor.B)
            End If
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters.AddWithValue("@is_Shortcut", isShortcut_CB.Checked)
            .Parameters.AddWithValue("@IM_Num", IM_Num_txt.Text)

            .Parameters.AddWithValue("@Grp_ID", GM_Group_CM.SelectedValue)

            If Me.IM_Photo.Image IsNot Nothing Then .Parameters.AddWithValue("@IM_Full_Photo", IM_PH_PATH)
            .Parameters.AddWithValue("@Notes", Me.Notes_txt.Text)
            .Parameters.AddWithValue("@Def_U_ID", Def_U_ID)
            .Parameters.AddWithValue("@Markter_Val", Markter_Val_txt.Text)

        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم الحفظ", MsgBoxStyle.Information)

            If is_New_IM = True Then
                Network_Edit_Tracker_insert(" إسم الصنف:" & IM_SH_txt.Text & " النوع:" & IM_Type_cm.Text & " التصنيف:" & GM_Serach.Text & " الرقم:" & IM_Num_txt.Text & " الصلاحية:" _
                           & Valid_St & " إضافة للإختصارات:" & isShort_St & " تكلفة القطعة:" & IM_Cost_txt.Text & " عمولة المسوق:" & Markter_Val_txt.Text, IM_ID, 20, 1)
            Else
                Network_Edit_Tracker_insert(" الإسم السابق:" & IM_Name_ToolStrip.Text & " إسم الصنف:" & IM_SH_txt.Text & " النوع:" & IM_Type_cm.Text & " التصنيف:" & GM_Serach.Text & " الرقم:" & IM_Num_txt.Text & " الصلاحية:" _
                               & Valid_St & " إضافة للإختصارات:" & isShort_St & " تكلفة القطعة:" & IM_Cost_txt.Text & " عمولة المسوق:" & Markter_Val_txt.Text, IM_ID, 20, 3)
            End If


            GM_ID = GM_Serach.SelectedValue
            isValid = isValid_CB.Checked
            Clear_Fields()
            IM_SH_txt.Clear()
            Load_GM()
            Load_Units(IM_Unit_cm)
            Coutnt_IM()
            ADD_New_IM()
        End If

    End Sub

    Private Sub IMSaleNameTextBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles IMSaleNameTextBox.Validating
        If String.IsNullOrWhiteSpace(IMSaleNameTextBox.Text) Then IMSaleNameTextBox.Text = IM_SH_txt.Text
    End Sub



    Private Sub IM_Units_insert()
        If String.IsNullOrWhiteSpace(Price_txt.Text) Then Price_txt.Text = "0"
        'If String.IsNullOrWhiteSpace(BarCode_txt.Text) Then BarCode_txt.Text = Get_Barcode_U_IM_ID()

        Dim U_ID As Integer
        If Check_Unit_Exist(IM_Unit_cm, Unit_cargo_txt.Text) = 0 Then
            Unit_Insert(IM_Unit_cm, Unit_cargo_txt.Text, 22)
            U_ID = GET_Unit_Exist(IM_Unit_cm.Text, Unit_cargo_txt.Text)
        Else
            U_ID = GET_Unit_Exist(IM_Unit_cm.Text, Unit_cargo_txt.Text)
        End If


        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "IM_Units_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@U_ID", U_ID)
            '.Parameters.AddWithValue("@Barcode", BarCode_txt.Text)
            If Not String.IsNullOrWhiteSpace(BarCode_txt.Text) Then .Parameters.AddWithValue("@Barcode", BarCode_txt.Text)
            .Parameters.AddWithValue("@Price", Price_txt.Text)
            If Not String.IsNullOrWhiteSpace(Min_SP_txt.Text) Then .Parameters.AddWithValue("@Min_SP", Min_SP_txt.Text)
            If Not String.IsNullOrWhiteSpace(Min_SP_2_txt.Text) Then .Parameters.AddWithValue("@Min_SP_2", Min_SP_2_txt.Text)
            .Parameters.AddWithValue("@User_ID", USER_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then

            Network_Edit_Tracker_insert(" إضافة وحدة للصنف " & IM_Name_ToolStrip.Text & " الوحدة:" & IM_Unit_cm.Text & " الباركود: " & BarCode_txt.Text & " السعر: " & Price_txt.Text & " الجملة: " & Min_SP_txt.Text & " جملة الجملة: " & Min_SP_2_txt.Text, 0, 20, 1)
            Unit_cargo_txt.Clear()
            IM_Units_Select()
            Price_txt.Clear()
            Min_SP_txt.Clear()
            Min_SP_2_txt.Clear()
            BarCode_txt.Clear()
            'If IM_Type_cm.SelectedIndex = 0 Then
            '    IM_Unit_cm.SelectedValue = 1
            'Else
            '    IM_Unit_cm.SelectedValue = Def_U_ID
            'End If
            If IM_Type_cm.SelectedIndex > 0 Then IM_Unit_cm.SelectedValue = Def_U_ID
        End If
    End Sub


    Public Sub IM_Units_Select()
        Try
            Unit_Dt.Clear()
            Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "IM_Units_Select"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@IM_ID", IM_ID)
                C.Da = New SqlClient.SqlDataAdapter(C.Com)
                C.Da.Fill(Unit_Dt)
                Unit_DataGridView.DataSource = Unit_Dt

                For i = 0 To Unit_DataGridView.Rows.Count - 1
                    If Unit_DataGridView.Rows(i).Cells("is_Default_CL").Value = True Then
                        IM_Def_Unit_Cargo = Unit_DataGridView.Rows(i).Cells("U_Cargo_CL").Value
                        Exit For
                    End If
                Next

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function CHeck_if_There_Other_Unit_SameCargo()
        Dim N = 0
        For i = 0 To Unit_DataGridView.Rows.Count - 1
            If Unit_DataGridView.Rows(i).Cells("U_Cargo_CL").Value = 1 Then
                N += 1
            End If
        Next

        If N > 1 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub IM_Units_Delete()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "IM_Units_Delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@U_IM_ID", Unit_DataGridView.CurrentRow.Cells("U_IM_ID_CL").Value)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert(" حذف وحدة للصنف " & IM_Name_ToolStrip.Text & " الوحدة:" & Unit_DataGridView.CurrentRow.Cells("U_Name_CL").Value.ToString _
                                        & " الباركود:" & Unit_DataGridView.CurrentRow.Cells("Barcode_CL").Value.ToString & " السعر: " & Unit_DataGridView.CurrentRow.Cells("Price_CL").Value.ToString & " الجملة: " & Unit_DataGridView.CurrentRow.Cells("Min_SP_CL").Value.ToString & " جملة الجملة: " & Unit_DataGridView.CurrentRow.Cells("Min_SP_2_CL").Value.ToString, 0, 20, 2)
            IM_Units_Select()
        End If
    End Sub

    Private Sub Unit_DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Unit_DataGridView.CellClick
        Calc_Cost()
    End Sub

    Private Sub Calc_Cost()

        If Unit_DataGridView.CurrentRow.Cells("U_Cargo_CL").Value > 1 Then
            IM_BoxCost_txt.Text = IM_Cost * Unit_DataGridView.CurrentRow.Cells("U_Cargo_CL").Value
            WinPrice_Lb.Text = Unit_DataGridView.CurrentRow.Cells("Price_CL").Value - Convert.ToDouble(IM_BoxCost_txt.Text)
        Else
            WinPrice_Lb.Text = Unit_DataGridView.CurrentRow.Cells("Price_CL").Value - Convert.ToDouble(IM_Cost_txt.Text)
        End If

    End Sub

    Private Sub BarCode_Test_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_SH_txt.KeyDown

        Select Case e.KeyCode
            Case Keys.Return
                If IMDataGridViewX.Rows.Count > 0 Then
                    Begin_Fetch()
                Else
                    IM_Num_txt.Select()
                End If

            Case Keys.Down
                If IMDataGridViewX.Visible = True Then IMDataGridViewX.Select()
            Case Keys.Delete : IM_SH_txt.Clear()
        End Select

    End Sub

    Private Sub IM_Menu_Grid_KeyDown(sender As Object, e As KeyEventArgs) Handles IMDataGridViewX.KeyDown
        If e.KeyCode = Keys.Return Then Begin_Fetch()
        If e.KeyCode = Keys.Up Then If IMDataGridViewX.CurrentRow.Index = 0 Then IM_SH_txt.Select()
    End Sub

    Private Sub IM_Menu_Grid_MouseClick(sender As Object, e As MouseEventArgs) Handles IMDataGridViewX.MouseClick
        Begin_Fetch()
    End Sub

    Private Sub Begin_Fetch()
        If IMDataGridViewX.Rows.Count > 0 Then
            MaxQtyAlert_txt.Clear()
            MinQtyAlert_txt.Clear()
            TabControl1.SelectedTab = TabPage1
            IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
            Fetch_IM()
        End If
    End Sub

    Private Sub BarCode_Test_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
        If IM_SH_txt.Text.Count > 0 Then Search_IM()
        Name_Error.Clear()
    End Sub

    Private Sub AG_Name_txtb_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles IM_SH_txt.Validating
        If String.IsNullOrWhiteSpace(IM_SH_txt.Text) = True Then
            Name_Error.SetError(IM_SH_txt, " حدد إسم الصنف ")
            IM_SH_txt.Select()
            e.Cancel = True
        Else
            e.Cancel = False
            Name_Error.Clear()
        End If

    End Sub

    Private Sub ADD_NewGM_Btn_Click(sender As Object, e As EventArgs) Handles ADD_NewGM_Btn.Click
        ' If isCatch_IM = True Then
        If GM_Serach.SelectedIndex > -1 Then
            MsgBox("هذه مجموعة موجودة بالفعل", MsgBoxStyle.Critical, "إضافة مجموعة")
        ElseIf String.IsNullOrWhiteSpace(GM_Serach.Text) Then
            MsgBox("أدخل اسم مجموعة الجديد", MsgBoxStyle.Exclamation, "إضافة مجموعة")
            GM_Serach.Select()
        Else
            If MessageBox.Show(" إضافة " + GM_Serach.Text + " إلى قائمة المجموعات ", " إضافة مجموعة ", MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                GM_Serach.SelectedValue = Insert_Fast_GM()
            End If
        End If
        ' End If
    End Sub

    Private Function Insert_Fast_GM()
        Dim GM_New_ID As Integer
        Dim C As New C

        If GM_Serach.Text.Count < 4 Then
            Do While GM_Serach.Text.Count < 4
                GM_Serach.Text = GM_Serach.Text + " "
            Loop
        End If

        With C.Com
            .Connection = C.Con
            .CommandText = "GM_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@GM_ID", 0)
            .Parameters.AddWithValue("@GM_Name", Me.GM_Serach.Text)
            .Parameters.AddWithValue("@POS_isShow", 1)
            .Parameters.AddWithValue("@Printer_ID", 1)
            .Parameters.AddWithValue("@Ksh_Screen", My.Computer.Name)
            .Parameters("@GM_ID").Direction = ParameterDirection.Output

            If SQL_SP_EXEC(C.Com) Then
                GM_New_ID = .Parameters("@GM_ID").Value.ToString()
                Load_GM()
            End If

        End With

        Return GM_New_ID
    End Function


    Private Sub Min_SP_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Min_SP_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return
                Min_SP_2_txt.Select()
        End Select
    End Sub

    Private Sub Min_SP_2_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Min_SP_2_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return
                ADD_Unit()
        End Select
    End Sub

    Private Sub Price_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Price_txt.KeyDown

        Select Case e.KeyCode
            Case Keys.Return
                If Min_SP_Panel.Visible = True Then
                    Min_SP_txt.Select()
                Else
                    ADD_Unit()
                End If
            Case Keys.Right : IM_Unit_cm.Select()
                IM_Unit_cm.DroppedDown = True
            Case Keys.Up : IMSaleNameTextBox.Select()
        End Select

    End Sub

    Private Sub Price_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Price_txt.KeyPress, Min_SP_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Price_txt_TextChanged(sender As Object, e As EventArgs) Handles Price_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Units_Menu_cmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedIndexChanged
        UnitError.Clear()
    End Sub

    Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm_2.SelectedValueChanged
        If TypeName(IM_Unit_cm_2.SelectedValue) = "Integer" Or TypeName(IM_Unit_cm_2.SelectedValue) = "Long" Then IM_Fetch_Unit_Cargo(IM_Unit_cm_2, Unit_cargo_txt)
    End Sub



    Private Sub BarCode_txt_TextChanged(sender As Object, e As EventArgs) Handles BarCode_txt.TextChanged, Barcode_SH_txt.KeyPress
        BarError.Clear()
    End Sub

    Private Sub IM_Cost_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles IM_Cost_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub IM_Cost_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_Cost_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub GM_Serach_KeyDown(sender As Object, e As KeyEventArgs) Handles GM_Serach.KeyDown
        Select Case e.KeyCode
            Case Keys.Return : BarCode_txt.Select()
        End Select
    End Sub

    Private Sub IMSaleNameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles IMSaleNameTextBox.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Down : Price_txt.Select()
            Case Keys.Up : IM_SH_txt.Select()
        End Select
    End Sub

    Private Sub Units_Menu_cmb_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Unit_cm.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Left : Unit_cargo_txt.Select()
        End Select
    End Sub

    Private Sub InsertU_btn_Click(sender As Object, e As EventArgs) Handles InsertU_btn.Click
        ADD_Unit()
    End Sub

    Private Sub ADD_Unit()

        'If Check_Unit_Exist(IM_Unit_cm, Unit_cargo_txt.Text) = 0 Then Unit_Insert(IM_Unit_cm, Unit_cargo_txt.Text, 20)
        If String.IsNullOrWhiteSpace(BarCode_txt.Text) = False Then
            If S_is_Multi_BAR = False Then
                IM_Check_Barcode()
            Else
                IM_Units_insert()
            End If
        Else
            IM_Units_insert()
        End If
    End Sub


    Private Sub DeleteU_btn_Click(sender As Object, e As EventArgs) Handles DeleteU_btn.Click
        If Unit_DataGridView.Rows.Count > 0 Then
            'And isCatch_IM = True Then
            If MessageBox.Show(" حذف وحدة الصنف ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then IM_Units_Delete()
        End If
    End Sub

    Private Sub MakeBarcode_btn_Click(sender As Object, e As EventArgs) Handles MakeBarcode_btn.Click
        printbarcode.ShowDialog()
    End Sub

    Private Sub isValid_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isValid_CB.CheckedChanged
        CB_CHecked(sender)
        If sender.Checked = True Then
            Valid_St = "نعم"
        Else
            Valid_St = "لا"
        End If
    End Sub

    Private Sub Random_Barcode_btn_Click(sender As Object, e As EventArgs) Handles Random_Barcode_btn.Click
        BarCode_txt.Text = Get_Barcode_U_IM_ID()
    End Sub

    Private Sub BarCode_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BarCode_txt.KeyPress
        If e.KeyChar = "" Then e.Handled = True
    End Sub

    Private Sub isShortcut_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isShortcut_CB.CheckedChanged
        CB_CHecked(sender)
        If sender.Checked = True Then
            isShort_St = "نعم"
        Else
            isShort_St = "لا"
        End If
    End Sub

    Private Sub Barcode_Search_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_Search_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return

                If IMNUM_Grid.Rows.Count > 0 Then
                    Bengin_Fetch_By_Num()
                Else
                    If String.IsNullOrWhiteSpace(Barcode_Search_txt.Text) = False Then Load_IM_Barcode()
                End If

            Case Keys.Delete : Barcode_Search_txt.Clear()


            Case Keys.Down
                If IMNUM_Grid.Visible = True Then IMNUM_Grid.Select()

        End Select


    End Sub

    Private Sub Bengin_Fetch_By_Num()
        If IMNUM_Grid.Rows.Count > 0 Then
            IMNUM_Grid.Visible = False
            MaxQtyAlert_txt.Clear()
            MinQtyAlert_txt.Clear()
            TabControl1.SelectedTab = TabPage1
            IM_ID = IMNUM_Grid.CurrentRow.Cells("IM_ID_CL_2").Value
            Fetch_IM()
        End If
    End Sub

    Public Sub Load_IM_Barcode()
        Dim c As New C
        Dim s As String

        If Sh_ByNum_Searh_CB.Checked = True Then
            s = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_NUM = '" & Barcode_Search_txt.Text & "'"
        Else
            s = "select IM_ID from IM_Menu_Units_V WHERE Barcode = '" & Barcode_Search_txt.Text & "'"
        End If

        c.Com = New SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                ' isCatch_IM = True
                'isEdit = True
                IM_ID = c.Dr("IM_ID")
                Fetch_IM()
            Else
                If MessageBox.Show("هذا الصنف غير موجود ضمن قائمة الأصناف ... هل تريد إضافته", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    ADD_New_IM()
                    If Sh_ByNum_Searh_CB.Checked = True Then
                        IM_Num_txt.Text = Barcode_Search_txt.Text
                    Else
                        BarCode_txt.Text = Barcode_Search_txt.Text
                    End If

                    Barcode_Search_txt.Clear()
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Unit_DataGridView_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Unit_DataGridView.MouseDoubleClick
        If Unit_DataGridView.Rows.Count > 0 Then Update_IM_Unit.ShowDialog()
    End Sub

    Private Sub Fill_All_IM()
        Try
            Dim C As New C
            IM_Dt.Clear()
            Dim s As String = "select TOP 1000 IM_ID,item_name from IM_Active_V Order by item_name ASC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(IM_Dt)
            IMDataGridViewX.DataSource = IM_Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Show_IM_btn_Click_1(sender As Object, e As EventArgs) Handles Show_IM_btn.Click
        If IMDataGridViewX.Visible = True Then
            IMDataGridViewX.Visible = False
        Else
            IMDataGridViewX.Visible = True
            Fill_All_IM()
            IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
        End If
    End Sub

    Private Sub ItemsMenu_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Rs.ResizeAllControls(Me)
    End Sub


    Private Sub IM_Num_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Num_txt.KeyDown
        If e.KeyCode = Keys.Return Then Notes_txt.Select()
    End Sub

    Private Sub MaxQtyAlert_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MaxQtyAlert_txt.KeyPress, MinQtyAlert_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub MaxQtyAlert_txt_TextChanged(sender As Object, e As EventArgs) Handles MaxQtyAlert_txt.TextChanged, MinQtyAlert_txt.TextAlignChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub IM_Type_cm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles IM_Type_cm.SelectedIndexChanged
        'If IM_Type_cm.SelectedIndex = 1 Then
        '    IM_Unit_cm.SelectedValue = Def_U_ID
        'Else
        '    IM_Unit_cm.SelectedValue = 1
        'End If
        If IM_Type_cm.SelectedIndex > 0 Then IM_Unit_cm.SelectedValue = Def_U_ID

        If IM_Type_cm.SelectedIndex = 2 Then
            Frm_TabPage.Visible = True
        Else
            Frm_TabPage.Visible = False
        End If

    End Sub


    Dim FRM_IM_ID = 0
    Dim Get_Unit = False
    Dim U_Dt As New DataTable
    Dim FRM_Dt As New DataTable

    Private Sub IM_FRM_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_FRM_txt.TextChanged
        If IM_FRM_txt.Text.Count > 0 Then
            IM_FRM_txt_Load_IM()
        Else
            FRM_GDX.Visible = False
            FRM_IM_ID = 0
            QtyTextBox.Clear()
        End If
        If FRM_IM_ID = 0 Then
            IM_FRM_txt.BackColor = Color.LightGray
        Else
            IM_FRM_txt.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Public Sub IM_FRM_txt_Load_IM()
        Dim c As New C

        Try
            IM_Dt.Clear()
            Dim s As String
            s = "select IM_ID,item_name,isValid from IM_All_V WHERE item_name Like '%" & IM_FRM_txt.Text & "%' AND isValid = 0 AND ISSTORE IN(0,1) Order by item_name ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Dt)
            FRM_GDX.DataSource = IM_Dt
            If IM_Dt.Rows.Count > 0 Then
                FRM_GDX.Visible = True
                FRM_GDX.Size = New Point(FRM_GDX.Size.Width, 530)
            Else
                FRM_GDX.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IM_FRM_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_FRM_txt.KeyDown

        Select Case e.KeyCode
            Case Keys.Return
                Search_From_Grid()
            Case Keys.Down
                If FRM_GDX.Visible = True Then
                    FRM_GDX.Select()
                Else
                    QtyTextBox.Select()
                End If
            Case Keys.Left : Barcode_SH_txt.Select()
            Case Keys.Delete : IM_FRM_txt.Clear()
        End Select


    End Sub

    Private Sub Search_From_Grid()
        If FRM_GDX.Visible = True Then
            Fetch_ItemToList()
        Else
            QtyTextBox.Select()
        End If
    End Sub

    Private Sub FRM_GDX_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles FRM_GDX.CellClick
        Fetch_ItemToList()
    End Sub

    Private Sub FRM_GDX_KeyDown(sender As Object, e As KeyEventArgs) Handles FRM_GDX.KeyDown
        If e.KeyCode = Keys.Return Then Fetch_ItemToList()
        If e.KeyCode = Keys.Up Then If FRM_GDX.CurrentRow.Index = 0 Then IM_FRM_txt.Select()
    End Sub

    Private Sub Fetch_ItemToList()

        If FRM_GDX.Rows.Count > 0 Then
            FRM_IM_ID = FRM_GDX.CurrentRow.Cells("IM_ID_CL2").Value
            IM_FRM_txt.Text = FRM_GDX.CurrentRow.Cells("item_name_CL2").Value
            IM_FRM_txt.BackColor = Color.LightGoldenrodYellow
            Get_Unit = False
            Fetch_IM_Units()
            FRM_GDX.Visible = False
            IM_FRM_txt.Select()
        End If
    End Sub

    Private Sub Fetch_IM_Units()
        Get_Unit = False
        Dim c As New C
        U_Dt.Clear()
        Try
            Dim s As String
            s = "select U_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & FRM_IM_ID & "' Order By U_Cargo Asc"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(U_Dt)
            IM_Unit_cm_2.DataSource = U_Dt
            IM_Unit_cm_2.DisplayMember = "U_Name"
            IM_Unit_cm_2.ValueMember = "U_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Get_Unit = True
    End Sub

    Private Sub IM_Formating_Menu_insert()
        If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "IM_Formating_Menu_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("FRM_IM_ID", FRM_IM_ID)
            .Parameters.AddWithValue("@U_ID", IM_Unit_cm_2.SelectedValue)
            .Parameters.AddWithValue("@Qty", QtyTextBox.Text)
            .Parameters.AddWithValue("@User_ID", USER_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            IM_Formating_Menu_Select()
            IM_FRM_txt.Clear()
        End If


    End Sub

    Public Sub IM_Formating_Menu_Select()
        Try
            FRM_Dt.Clear()
            Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "IM_Formating_Menu_SELECT"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@IM_ID", IM_ID)
                C.Da = New SqlClient.SqlDataAdapter(C.Com)
                C.Da.Fill(FRM_Dt)
                IM_FRM_MENU_DV.DataSource = FRM_Dt

                For i = 0 To IM_FRM_MENU_DV.Rows.Count - 1
                    IM_FRM_MENU_DV.Rows(i).Cells("INDX_CL").Value = i + 1
                Next

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ADD_FRM_Btn_Click(sender As Object, e As EventArgs) Handles ADD_FRM_Btn.Click
        If FRM_IM_ID > 0 Then IM_Formating_Menu_insert()
    End Sub

    Private Sub REMOVE_FRM_Btn_Click(sender As Object, e As EventArgs) Handles REMOVE_FRM_Btn.Click
        If FRM_Dt.Rows.Count > 0 Then
            'And isCatch_IM = True Then
            If MessageBox.Show(" حذف مكون الصنف ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then IM_Formating_Menu_REMOVE()
        End If
    End Sub

    Private Sub IM_Formating_Menu_REMOVE()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "IM_Formating_Menu_REMOVE"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", IM_FRM_MENU_DV.CurrentRow.Cells("T_ID_CL").Value)
        End With

        If SQL_SP_EXEC(c.Com) = True Then IM_Formating_Menu_Select()

    End Sub

    Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown
        If e.KeyCode = Keys.Return Then ADD_FRM_Btn_Click(sender, e)
    End Sub


    Public Sub Load_IM_Barcode_frm()
        Dim c As New C
        IM_Dt.Clear()
        Try
            Dim s As String
            If Sh_ByNum_CB.Checked = True Then
                s = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_NUM = '" & Barcode_SH_txt.Text & "'"
            Else
                s = "select IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
            End If

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                FRM_IM_ID = c.Dr("IM_ID")
                IM_FRM_txt.Text = c.Dr("item_name")
                Get_Unit = False
                FRM_GDX.Visible = False
                QtyTextBox.Select()
                Fetch_IM_Units()
                ' If Sh_ByNum_CB.Checked = False Then IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
                Barcode_SH_txt.Clear()
            Else
                If Barcode_SH_txt.Text.Count = 13 Then
                    Check_If_Mizan()
                Else
                    MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Check_If_Mizan()
        Dim c As New C
        Try
            Dim S As String = "Select IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text.Substring(0, 7) & "'"
            c.Com = New SqlClient.SqlCommand(S, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Barcode_SH_txt.Text = Barcode_SH_txt.Text(7) + Barcode_SH_txt.Text(8) + Barcode_SH_txt.Text(9) + Barcode_SH_txt.Text(10) + Barcode_SH_txt.Text(11)
                QtyTextBox.Text = Convert.ToDouble(Barcode_SH_txt.Text) / 1000
                FRM_IM_ID = c.Dr("IM_ID")
                IM_FRM_txt.Text = c.Dr("item_name")
                Get_Unit = False
                FRM_GDX.Visible = False
                QtyTextBox.Select()
                Fetch_IM_Units()
                ' IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
                Barcode_SH_txt.Clear()
            Else
                MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return : If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode_frm()
            Case Keys.Down : QtyTextBox.Select()
            Case Keys.Delete : Barcode_SH_txt.Clear()
        End Select
    End Sub

    Private Sub Sh_ByNum_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Sh_ByNum_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub IM_MV_btn_Click(sender As Object, e As EventArgs) Handles IM_MV_btn.Click
        If IM_ID > 0 Then IM_MV.ShowDialog()
    End Sub

    Private Sub ADD_ST_ALERT_QTY_btn_Click(sender As Object, e As EventArgs) Handles ADD_ST_ALERT_QTY_btn.Click

        For i = 0 To IM_QTY_ALERT_DGV.Rows.Count - 1
            If IM_QTY_ALERT_DGV.Rows(i).Cells("ST_ID_CL").Value = ST_cm.SelectedValue Then
                MsgBox("تم إدراج الإشعار في هذا المخزن", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Next
        IM_Qty_Alert_insert()
    End Sub

    Private Sub IM_Qty_Alert_insert()
        If String.IsNullOrWhiteSpace(MinQtyAlert_txt.Text) Then MinQtyAlert_txt.Text = "0"
        If String.IsNullOrWhiteSpace(MaxQtyAlert_txt.Text) Then MaxQtyAlert_txt.Text = "0"

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "IM_Qty_Alert_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@MIN_QTY", MinQtyAlert_txt.Text)
            .Parameters.AddWithValue("@MAX_QTY", MaxQtyAlert_txt.Text)
            .Parameters.AddWithValue("@User_ID", USER_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            IM_Qty_Alert_Select()
            MinQtyAlert_txt.Clear()
            MaxQtyAlert_txt.Clear()
        End If
    End Sub


    Private Sub IM_Qty_Alert_Select()
        Try
            ALERT_Q_Dt.Clear()
            Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "IM_Qty_Alert_Select"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@IM_ID", IM_ID)
                C.Da = New SqlClient.SqlDataAdapter(C.Com)
                C.Da.Fill(ALERT_Q_Dt)
                IM_QTY_ALERT_DGV.DataSource = ALERT_Q_Dt
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub REMOVE_ST_ALERT_QTY_btn_Click(sender As Object, e As EventArgs) Handles REMOVE_ST_ALERT_QTY_btn.Click
        If IM_QTY_ALERT_DGV.Rows.Count > 0 Then
            'And isCatch_IM = True Then
            If MessageBox.Show(" حذف إشعار الكمية ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then IM_Qty_Alert_Delete()
        End If
    End Sub

    Private Sub IM_Qty_Alert_Delete()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "IM_Qty_Alert_Delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", IM_QTY_ALERT_DGV.CurrentRow.Cells("Q_ALERT_T_ID_CL").Value)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            IM_Qty_Alert_Select()
        End If
    End Sub

    Private Sub GM_Serach_SelectedValueChanged(sender As Object, e As EventArgs) Handles GM_Serach.SelectedValueChanged
        If Get_COUNTER = True Then Get_GM_IM_COUNTER()
    End Sub

    Private Sub Get_GM_IM_COUNTER()
        Dim c As New C
        Dim s As String
        s = "select COUNT(GM_ID) AS S from IM_Menu WHERE GM_ID = '" & GM_Serach.SelectedValue & "' AND Row_Enabled = 1"
        c.Com = New SqlClient.SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                GM_IM_COUNT_Lb.Text = c.Dr("S").ToString + " مواد "
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub Sh_ByNum_Searh_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Sh_ByNum_Searh_CB.CheckedChanged
        CB_CHecked(sender)
        Barcode_Search_txt.Select()
    End Sub

    Private Sub IMPH_Btn_Click(sender As Object, e As EventArgs) Handles IMPH_Btn.Click
        Dim OpenFL As New OpenFileDialog With {.Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico", _
                                              .Multiselect = False, .Title = "إختر صورة"}
        If OpenFL.ShowDialog = Windows.Forms.DialogResult.OK Then
            IM_Photo.Image = Image.FromFile(System.IO.Path.GetFullPath(OpenFL.FileName))

            If String.IsNullOrWhiteSpace(MY_Settings.SERVER_IMG_PATH) Then
                IM_PH_PATH = System.IO.Path.GetFullPath(OpenFL.FileName) & " (" & F_ItemsMenu.IM_ID & ")"
            Else
                IM_PH_PATH = MY_Settings.SERVER_IMG_PATH & "\" & OpenFL.FileName & " (" & F_ItemsMenu.IM_ID & ")"
            End If

        End If
    End Sub

    Private Sub IMPH_None_btn_Click(sender As Object, e As EventArgs) Handles IMPH_None_btn.Click
        If IM_Photo.Image IsNot Nothing Then IM_Photo.Image = Nothing
    End Sub

    Private Sub Recount_Cost_btn_Click(sender As Object, e As EventArgs) Handles Recount_Cost_btn.Click
        Recount_IM_Cost.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If FRM_GDX.Visible = True Then
            FRM_GDX.Visible = False
        Else
            FRM_GDX.Visible = True
            Fill_All_Frm_Compnents()
            FRM_GDX.Size = New Point(FRM_GDX.Size.Width, 430)
        End If
    End Sub
    Private Sub Fill_All_Frm_Compnents()
        Try
            Dim C As New C
            C.Dt.Clear()
            Dim s As String = "select IM_ID,item_name from IM_Active_V Order by item_name ASC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(C.Dt)
            FRM_GDX.DataSource = C.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Markter_Val_txt_TextChanged(sender As Object, e As EventArgs) Handles Markter_Val_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Markter_Val_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Markter_Val_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Min_SP_txt_TextChanged(sender As Object, e As EventArgs) Handles Min_SP_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Min_SP_2_txt_TextChanged(sender As Object, e As EventArgs) Handles Min_SP_2_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Min_SP_2_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Min_SP_2_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Notes_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Notes_txt.KeyDown
        If e.KeyCode = Keys.Return Then
            IM_Cost_txt.Select()
            IM_Cost_txt.Focus()
        End If
    End Sub

    Private Sub IM_Cost_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Cost_txt.KeyDown
        If e.KeyCode = Keys.Return Then
            If IM_BoxCost_txt.Enabled = True Then
                IM_BoxCost_txt.Select()
            Else
                BarCode_txt.Select()
            End If
        End If
    End Sub

    Private Sub IM_BoxCost_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_BoxCost_txt.KeyDown
        If e.KeyCode = Keys.Return Then
            If Markter_Val_txt.Enabled = True Then
                Markter_Val_txt.Select()
            Else
                Barcode_SH_txt.Select()
            End If

        End If
    End Sub

    Private Sub Open_Camera_btn_Click(sender As Object, e As EventArgs) Handles Open_Camera_btn.Click
        Dim f As New Camera
        f.ShowDialog()
    End Sub

    Private Sub Barcode_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles Barcode_Search_txt.TextChanged
        If Sh_ByNum_Searh_CB.Checked = True And Barcode_Search_txt.Text.Count > 0 Then
            Load_IMByNum()
        Else
            IMNUM_Grid.Visible = False
        End If
    End Sub

    Public Sub Load_IMByNum()
        Dim c As New C

        Try
            IM_Dt_2.Clear()
            Dim s As String
            s = "select IM_ID,IM_NUM from IM_All_V WHERE IM_NUM Like '%" & Barcode_Search_txt.Text & "%' Order by IM_NUM ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Dt_2)
            IMNUM_Grid.DataSource = IM_Dt_2
            If IM_Dt_2.Rows.Count > 0 Then
                IMNUM_Grid.Visible = True
                IMNUM_Grid.Size = New Point(IMNUM_Grid.Size.Width, 530)
            Else
                IMNUM_Grid.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IMNUM_Grid_KeyDown(sender As Object, e As KeyEventArgs) Handles IMNUM_Grid.KeyDown
        If e.KeyCode = Keys.Return Then Bengin_Fetch_By_Num()
        If e.KeyCode = Keys.Up Then If IMNUM_Grid.CurrentRow.Index = 0 Then Barcode_Search_txt.Select()
    End Sub

    Private Sub IMNUM_Grid_MouseClick(sender As Object, e As MouseEventArgs) Handles IMNUM_Grid.MouseClick
        Bengin_Fetch_By_Num()
    End Sub

    Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles QtyTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub تركيبةالنوعToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تركيبةالنوعToolStripMenuItem.Click
        IM_Struct.ShowDialog()
    End Sub

    Private Sub تعديلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلToolStripMenuItem.Click
        If Unit_DataGridView.Rows.Count > 0 Then Update_IM_Unit.ShowDialog()
    End Sub

    Private Sub حذفالوحدةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles حذفالوحدةToolStripMenuItem.Click
        If Unit_DataGridView.Rows.Count > 0 Then
            If MessageBox.Show(" حذف وحدة الصنف ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then IM_Units_Delete()
        End If
    End Sub

    Private Sub Unit_cargo_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Unit_cargo_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Units_Menu_cmb_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If TypeName(IM_Unit_cm.SelectedValue) = "Integer" Or TypeName(IM_Unit_cm.SelectedValue) = "Long" Then IM_Fetch_Unit_Cargo(IM_Unit_cm, Unit_cargo_txt)
    End Sub

    Private Sub Unit_cargo_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Unit_cargo_txt.KeyDown
        If e.KeyCode = Keys.Return Then BarCode_txt.Select()
    End Sub

    Private Sub is_Rsv_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Rsv_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub
End Class