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
    Private ItemsCacheDt As DataTable = Nothing
    Private ItemBarcodesCacheDt As DataTable = Nothing
    Private IsItemsCacheLoaded As Boolean = False


    Private Sub LoadItemsToCache()
        If IsItemsCacheLoaded AndAlso ItemsCacheDt IsNot Nothing AndAlso ItemBarcodesCacheDt IsNot Nothing Then Return
        Try
            Dim db As New C()
            ItemsCacheDt = New DataTable()
            db.Str = "SELECT IM_ID,item_name,isValid,IM_Num FROM IM_All_V Order by item_name ASC"
            db.Da = New SqlClient.SqlDataAdapter(db.Str, db.Con)
            db.Da.Fill(ItemsCacheDt)

            ItemBarcodesCacheDt = New DataTable()
            db.Str = "SELECT IM_ID,Barcode FROM IM_All_Barcodes_V WHERE ISNULL(Barcode,'') <> '' Order by Barcode ASC"
            db.Da = New SqlClient.SqlDataAdapter(db.Str, db.Con)
            db.Da.Fill(ItemBarcodesCacheDt)

            IsItemsCacheLoaded = True
        Catch ex As Exception
            IsItemsCacheLoaded = False
            ItemsCacheDt = Nothing
            ItemBarcodesCacheDt = Nothing
        End Try
    End Sub

    Private Sub RefreshItemsCache()
        IsItemsCacheLoaded = False
        LoadItemsToCache()
    End Sub

    Private Function SafeCacheText(row As DataRow, columnName As String) As String
        If row Is Nothing OrElse row.Table Is Nothing OrElse Not row.Table.Columns.Contains(columnName) OrElse row.IsNull(columnName) Then Return ""
        Return row(columnName).ToString()
    End Function

    Private Function SortSearchResults(dt As DataTable, sortExpression As String) As DataTable
        If dt.Rows.Count = 0 Then Return dt
        Dim view As New DataView(dt)
        view.Sort = sortExpression
        Return view.ToTable()
    End Function

    Private Function BuildItemNameResults(searchText As String) As DataTable
        Dim result As New DataTable()
        result.Columns.Add("IM_ID", GetType(Integer))
        result.Columns.Add("item_name", GetType(String))
        result.Columns.Add("isValid", GetType(Integer))

        LoadItemsToCache()
        If ItemsCacheDt Is Nothing Then Return result

        For Each row As DataRow In ItemsCacheDt.Rows
            Dim itemName As String = SafeCacheText(row, "item_name")
            If String.IsNullOrWhiteSpace(searchText) OrElse itemName.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0 Then
                Dim resultRow As DataRow = result.NewRow()
                resultRow("IM_ID") = row("IM_ID")
                resultRow("item_name") = itemName
                resultRow("isValid") = If(row.IsNull("isValid"), 0, row("isValid"))
                result.Rows.Add(resultRow)
            End If
        Next

        Return SortSearchResults(result, "item_name ASC")
    End Function

    Private Function BuildItemNumberResults(searchText As String) As DataTable
        Dim result As New DataTable()
        result.Columns.Add("IM_ID", GetType(Integer))
        result.Columns.Add("IM_NUM", GetType(String))
        result.Columns.Add("isValid", GetType(Integer))

        LoadItemsToCache()
        If ItemsCacheDt Is Nothing OrElse String.IsNullOrWhiteSpace(searchText) Then Return result

        For Each row As DataRow In ItemsCacheDt.Rows
            Dim itemNumber As String = SafeCacheText(row, "IM_NUM")
            If itemNumber.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0 Then
                Dim resultRow As DataRow = result.NewRow()
                resultRow("IM_ID") = row("IM_ID")
                resultRow("IM_NUM") = itemNumber
                resultRow("isValid") = If(row.IsNull("isValid"), 0, row("isValid"))
                result.Rows.Add(resultRow)
            End If
        Next

        Return SortSearchResults(result, "IM_NUM ASC")
    End Function

    Private Function BuildBarcodeResults(searchText As String) As DataTable
        Dim result As New DataTable()
        result.Columns.Add("IM_ID", GetType(Integer))
        result.Columns.Add("Barcode", GetType(String))

        LoadItemsToCache()
        If ItemBarcodesCacheDt Is Nothing OrElse String.IsNullOrWhiteSpace(searchText) Then Return result

        For Each row As DataRow In ItemBarcodesCacheDt.Rows
            Dim barcode As String = SafeCacheText(row, "Barcode")
            If barcode.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0 Then
                Dim resultRow As DataRow = result.NewRow()
                resultRow("IM_ID") = row("IM_ID")
                resultRow("Barcode") = barcode
                result.Rows.Add(resultRow)
            End If
        Next

        Return SortSearchResults(result, "Barcode ASC")
    End Function

    Private Function FindItemByExactValue(source As DataTable, columnName As String, searchText As String) As DataRow
        LoadItemsToCache()
        If source Is Nothing OrElse Not source.Columns.Contains(columnName) Then Return Nothing

        For Each row As DataRow In source.Rows
            If String.Equals(SafeCacheText(row, columnName), searchText, StringComparison.CurrentCultureIgnoreCase) Then Return row
        Next

        Return Nothing
    End Function


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
        ' تحميل البيانات للرام فور فتح الشاشة لتكون جاهزة للبحث
        LoadItemsToCache()
        ThemeManager.ApplyThemeToForm(Me)
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

        FunModule.Load_ALL_IM()
        ' تحميل البيانات
        IM_FRM_mySearchControl.ItemsTable = IM_Dt
        IM_FRM_mySearchControl.itemsTable_Barcode = IM_Dt_Barcodes
        IM_FRM_mySearchControl.MaxGridHeight = 400

        'mySearchControl.DefaultSearchField = "اسم الصنف"
        ' إضافة الكنترول للفورم
        'Me.Controls.Add(mySearchControl)
        ' استقبال الاختيار
        AddHandler IM_FRM_mySearchControl.ItemSelected, AddressOf HandleItemSelected



        IMDataGridViewX.Visible = False
        IMDataGridViewX.Height = 0




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
        'IM_FRM_txt.Clear()
        'Barcode_SH_txt.Clear()
        IM_FRM_mySearchControl.Clear_txt()

        Me.IM_Photo.Image = Nothing
        Me.IM_Photo.BackColor = System.Drawing.SystemColors.ButtonFace
        Notes_txt.Clear()
        Markter_Val_txt.Clear()
    End Sub

    Private Sub ChoasePicureButton_Click(sender As Object, e As EventArgs) Handles ChoasePicureButton.Click
        Dim OpenFL As New OpenFileDialog With {.Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico",
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
        Try
            IM_Dt = BuildItemNameResults(IM_SH_txt.Text.Trim())
            IMDataGridViewX.DataSource = IM_Dt
            AutoResizeGridDropDown(IMDataGridViewX, 250)
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
        If MessageBox.Show(" تـأكيــد حــذف الصــنف " + IM_Name_ToolStrip.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
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
            RefreshItemsCache()
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
        If Not CanSaveItemTypeChange() Then Return

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
            RefreshItemsCache()

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

    Private Function CanSaveItemTypeChange() As Boolean

        If IM_ID <= 0 Then Return True

        Dim currentItemType As Integer = GetCurrentItemType(IM_ID)
        Dim newItemType As Integer = IM_Type_cm.SelectedIndex

        If currentItemType = newItemType Then Return True
        If Not IsServiceStockTypeChange(currentItemType, newItemType) Then Return True
        If Not HasInventoryTransactionFlow(IM_ID) Then Return True

        MsgBox(
            "لا يمكن تغيير نوع الصنف بين خدمة وبضاعة/تصنيع لأن الصنف لديه حركة مخزنية سابقة." & vbCrLf &
            "يرجى إبقاء نوع الصنف كما هو حتى لا تتأثر حركات المخزون.",
            MsgBoxStyle.Exclamation,
            "تنبيه"
        )

        IM_Type_cm.SelectedIndex = currentItemType
        IM_Type_cm.Focus()

        Return False

    End Function

    Private Function IsServiceStockTypeChange(currentItemType As Integer, newItemType As Integer) As Boolean

        Dim currentIsService As Boolean = currentItemType = 0
        Dim newIsService As Boolean = newItemType = 0
        Dim currentIsStockType As Boolean = currentItemType = 1 OrElse currentItemType = 2
        Dim newIsStockType As Boolean = newItemType = 1 OrElse newItemType = 2

        Return (currentIsStockType AndAlso newIsService) OrElse
               (currentIsService AndAlso newIsStockType)

    End Function

    Private Function GetCurrentItemType(itemId As Integer) As Integer

        Dim c As New C

        Try

            c.Str = "SELECT ISNULL(isStore, 0) AS isStore FROM IM_Menu WHERE IM_ID = @IM_ID"
            c.Com = New SqlClient.SqlCommand(c.Str, c.Con)
            c.Com.Parameters.AddWithValue("@IM_ID", itemId)

            c.Con.Open()

            Dim result As Object = c.Com.ExecuteScalar()

            If result Is Nothing OrElse result Is DBNull.Value Then Return -1

            Return Convert.ToInt32(result)

        Catch ex As Exception

            MsgBox("تعذر التحقق من نوع الصنف الحالي: " & ex.Message, MsgBoxStyle.Critical, "خطأ")
            Return -1

        Finally

            If c.Con.State = ConnectionState.Open Then c.Con.Close()

        End Try

    End Function

    Private Function HasInventoryTransactionFlow(itemId As Integer) As Boolean

        Dim c As New C

        Try

            c.Str = "SELECT TOP 1 1 FROM VW_InventoryTransactionFlow WHERE IM_ID = @IM_ID"
            c.Com = New SqlClient.SqlCommand(c.Str, c.Con)
            c.Com.Parameters.AddWithValue("@IM_ID", itemId)

            c.Con.Open()

            Dim result As Object = c.Com.ExecuteScalar()

            Return result IsNot Nothing AndAlso result IsNot DBNull.Value

        Catch ex As Exception

            MsgBox("تعذر التحقق من حركة الصنف: " & ex.Message, MsgBoxStyle.Critical, "خطأ")
            Return True

        Finally

            If c.Con.State = ConnectionState.Open Then c.Con.Close()

        End Try

    End Function

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
            RefreshItemsCache()
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
            RefreshItemsCache()
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

    'Private Sub BarCode_Test_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
    '    If IM_SH_txt.Text.Count > 0 Then Search_IM()
    '    Name_Error.Clear()
    'End Sub
    Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
        Name_Error.Clear()
        If String.IsNullOrWhiteSpace(IM_SH_txt.Text) Then
            IMDataGridViewX.Visible = False
            IMDataGridViewX.Height = 0
        Else
            IMDataGridViewX.Visible = True
            Search_IM()
        End If
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
            If MessageBox.Show(" إضافة " + GM_Serach.Text + " إلى قائمة المجموعات ", " إضافة مجموعة ", MessageBoxButtons.YesNo,
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


    Private Sub BarCode_txt_TextChanged(sender As Object, e As EventArgs) Handles BarCode_txt.TextChanged
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
        Try
            Dim foundRow As DataRow
            LoadItemsToCache()

            If Sh_ByNum_Searh_CB.Checked = True Then
                foundRow = FindItemByExactValue(ItemsCacheDt, "IM_NUM", Barcode_Search_txt.Text.Trim())
            Else
                foundRow = FindItemByExactValue(ItemBarcodesCacheDt, "Barcode", Barcode_Search_txt.Text.Trim())
            End If

            If foundRow IsNot Nothing Then
                IM_ID = foundRow("IM_ID")
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
    Private Sub AutoResizeGridDropDown(grid As DataGridView, maxHeight As Integer)
        ' 1. إذا لم يكن هناك نتائج أو مربع النص فارغ، قم بإخفاء الجريد أو تصغيرها
        If grid.Rows.Count = 0 OrElse grid.DataSource Is Nothing Then
            grid.Height = 0
            grid.Visible = False
            Return
        End If

        ' 2. إظهار الجريد وجعلها في المقدمة فوق كل الأدوات الأخرى
        grid.Visible = True
        grid.BringToFront()

        ' 3. حساب الارتفاع الإجمالي (ارتفاع الهيدر + ارتفاع كل صف)
        Dim totalHeight As Integer = grid.ColumnHeadersHeight

        ' نجمع ارتفاع الصفوف الحالية
        For Each row As DataGridViewRow In grid.Rows
            totalHeight += row.Height
        Next

        ' إضافة مسافة بسيطة للإطار الخارجي (Borders) لتجنب ظهور السكرول بشكل مزعج
        totalHeight += 5

        ' 4. تطبيق الارتفاع مع مراعاة الحد الأقصى (Max Height)
        If totalHeight > maxHeight Then
            grid.Height = maxHeight
            grid.ScrollBars = ScrollBars.Vertical ' إظهار شريط التمرير إذا تخطت الحد الأقصى
        Else
            grid.Height = totalHeight
            grid.ScrollBars = ScrollBars.None ' إخفاء شريط التمرير إذا كانت البيانات قليلة
        End If
    End Sub
    Private Sub Fill_All_IM()
        Try
            IM_Dt = BuildItemNameResults(IM_SH_txt.Text.Trim())
            IMDataGridViewX.DataSource = IM_Dt
            AutoResizeGridDropDown(IMDataGridViewX, 250)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Show_IM_btn_Click_1(sender As Object, e As EventArgs) Handles Show_IM_btn.Click
        If IMDataGridViewX.Visible Then
            IMDataGridViewX.Visible = False
            IMDataGridViewX.Height = 0
            Return
        End If

        IMDataGridViewX.Visible = True
        Fill_All_IM()
        IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
    End Sub

    'Private Sub ItemsMenu_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    '    Rs.ResizeAllControls(Me)
    'End Sub

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

    'Private Sub IM_FRM_txt_TextChanged(sender As Object, e As EventArgs)
    '    If IM_FRM_txt.Text.Count > 0 Then
    '        IM_FRM_txt_Load_IM()
    '    Else
    '        FRM_GDX.Visible = False
    '        FRM_IM_ID = 0
    '        QtyTextBox.Clear()
    '    End If
    '    If FRM_IM_ID = 0 Then
    '        IM_FRM_txt.BackColor = Color.LightGray
    '    Else
    '        IM_FRM_txt.BackColor = Color.LightGoldenrodYellow
    '    End If
    'End Sub

    'Public Sub IM_FRM_txt_Load_IM()
    '    Dim c As New C

    '    Try
    '        IM_Dt.Clear()
    '        Dim s As String
    '        s = "select IM_ID,item_name,isValid from IM_All_V WHERE item_name Like '%" & IM_FRM_txt.Text & "%' AND isValid = 0 AND ISSTORE IN(0,1) Order by item_name ASC"
    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(IM_Dt)
    '        FRM_GDX.DataSource = IM_Dt
    '        If IM_Dt.Rows.Count > 0 Then
    '            FRM_GDX.Visible = True
    '            FRM_GDX.Size = New Point(FRM_GDX.Size.Width, 530)
    '        Else
    '            FRM_GDX.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub IM_FRM_txt_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Return
    '            Search_From_Grid()
    '        Case Keys.Down
    '            If FRM_GDX.Visible = True Then
    '                FRM_GDX.Select()
    '            Else
    '                QtyTextBox.Select()
    '            End If
    '        Case Keys.Left : Barcode_SH_txt.Select()
    '        Case Keys.Delete : IM_FRM_txt.Clear()
    '    End Select


    'End Sub

    'Private Sub Search_From_Grid()
    '    If FRM_GDX.Visible = True Then
    '        Fetch_ItemToList()
    '    Else
    '        QtyTextBox.Select()
    '    End If
    'End Sub

    'Private Sub FRM_GDX_CellClick(sender As Object, e As DataGridViewCellEventArgs)
    '    Fetch_ItemToList()
    'End Sub

    'Private Sub FRM_GDX_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then Fetch_ItemToList()
    '    If e.KeyCode = Keys.Up Then If FRM_GDX.CurrentRow.Index = 0 Then IM_FRM_txt.Select()
    'End Sub


    Private Sub HandleItemSelected(itemId As Integer, isValid As String, isStore As Integer)
        FRM_IM_ID = itemId
        Get_Unit = False
        Fetch_IM_Units()
    End Sub

    'Private Sub Fetch_ItemToList()

    '    If FRM_GDX.Rows.Count > 0 Then
    '        FRM_IM_ID = FRM_GDX.CurrentRow.Cells("IM_ID_CL2").Value
    '        IM_FRM_txt.Text = FRM_GDX.CurrentRow.Cells("item_name_CL2").Value
    '        IM_FRM_txt.BackColor = Color.LightGoldenrodYellow
    '        Get_Unit = False
    '        Fetch_IM_Units()
    '        FRM_GDX.Visible = False
    '        IM_FRM_txt.Select()
    '    End If
    'End Sub

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
            IM_FRM_mySearchControl.Clear_txt()
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



    Private Sub IM_MV_btn_Click(sender As Object, e As EventArgs) Handles IM_MV_btn.Click
        'If IM_ID > 0 Then IM_MV.ShowDialog()


        Dim frm As New Frm_ItemLedger(
    IM_ID,
    IM_Name_ToolStrip.Text,
    1
)

        frm.ShowDialog()
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
        Dim OpenFL As New OpenFileDialog With {.Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico",
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

    'Private Sub Button1_Click(sender As Object, e As EventArgs)
    '    If FRM_GDX.Visible = True Then
    '        FRM_GDX.Visible = False
    '    Else
    '        FRM_GDX.Visible = True
    '        Fill_All_Frm_Compnents()
    '        FRM_GDX.Size = New Point(FRM_GDX.Size.Width, 430)
    '    End If
    'End Sub
    'Private Sub Fill_All_Frm_Compnents()
    '    Try
    '        Dim C As New C
    '        C.Dt.Clear()
    '        Dim s As String = "select IM_ID,item_name from IM_Active_V Order by item_name ASC"
    '        C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
    '        C.Da.Fill(C.Dt)
    '        FRM_GDX.DataSource = C.Dt
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

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
                'Else
                '    Barcode_SH_txt.Select()
            End If

        End If
    End Sub

    Private Sub Open_Camera_btn_Click(sender As Object, e As EventArgs) Handles Open_Camera_btn.Click
        Dim f As New Camera
        f.ShowDialog()
    End Sub

    'Private Sub Barcode_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles Barcode_Search_txt.TextChanged
    '    If Sh_ByNum_Searh_CB.Checked = True And Barcode_Search_txt.Text.Count > 0 Then
    '        Load_IMByNum()
    '    Else
    '        IMNUM_Grid.Visible = False
    '    End If
    'End Sub
    Private Sub Barcode_Search_txt_TextChanged(sender As Object, e As EventArgs) Handles Barcode_Search_txt.TextChanged
        If String.IsNullOrWhiteSpace(Barcode_Search_txt.Text) Then
            IMNUM_Grid.Visible = False
            IMNUM_Grid.Height = 0
        Else
            Load_IMByNum()
        End If
    End Sub

    Public Sub Load_IMByNum()
        Try
            If Sh_ByNum_Searh_CB.Checked = True Then
                IM_Dt_2 = BuildItemNumberResults(Barcode_Search_txt.Text.Trim())
            Else
                IM_Dt_2 = BuildBarcodeResults(Barcode_Search_txt.Text.Trim())
            End If

            IMNUM_Grid.DataSource = IM_Dt_2
            AutoResizeGridDropDown(IMNUM_Grid, 250)
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
