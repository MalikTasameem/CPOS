Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient

Public Class Sales_Fast_Draft : Inherits System.Windows.Forms.Form

    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Public isPause As Boolean
    Public IM_ID As Integer = 0

    Public TOTAL As Double = 0
    Public Disc As Double = 0
    Public Pure As Double = 0
    Public AG_ID As Integer = 0
    Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False
    Public U_Cargo As Double = 0
    Public Barcode As String = ""
    Dim BillUser_ID As Integer
    Public On_Update As Boolean
    Public IM_U_ID As Integer
    Dim Min_SP As Double
    Public SB_ID As Integer
    Dim U_IM_ID As Integer
    Public Barcode_IM As String = ""
    Public is_Valid As Boolean = False
    Public IM_Name As String
    Public IM_Unit_Name As String
    Public Valid_TXT As String = ""
    Dim Sales_BillPage_Bill_Track_FAST As String
    Dim Sales_Page_ID_FAST As Integer
    Public IM_Dt_Barcodes As New DataTable
    Public IM_Dt As New DataTable
    Public IM_Units_Dt As New DataTable

    Dim Bercent_Price As Double
    Public QtyTextBox As Double = 0
    Public IM_Price As Double
    Public IM_Cost As Double

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        FormType = 0
        'F_MainForm.Fill_ALL_IM()
        'If AGMetroGrid.Rows.Count = 0 And isDepended = False Then Delete_Last_Empty_Bill(T_ID)
        Me.Dispose()
    End Sub


    Public DraftManager As New DraftSalesManager()
    Private CurrentDraft As SaleDraftHeader
    Private ItemsCache As New List(Of CachedSaleItem)
    Dim C As New C


    Public Sub OpenDraft(draftId As String)

        Dim draft As SaleDraftHeader = DraftManager.LoadDraft(draftId)

        If draft Is Nothing Then
            MessageBox.Show("تعذر فتح المسودة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        CurrentDraft = draft

        BindDraftHeaderToForm()
        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()

    End Sub

    Private Sub BindDraftHeaderToForm()

        If CurrentDraft Is Nothing Then Exit Sub

        ' العميل
        AG_SH_txt.Text = CurrentDraft.CustomerName
        AG_ID = CurrentDraft.AG_ID.ToString()

        ' التاريخ
        DateTimeEx.Value = CurrentDraft.Date

        ' الملاحظات
        txtNotes.Text = CurrentDraft.About

        ' الخصم
        Discount_txt.Text = CurrentDraft.Discount.ToString("0.000")

        ' الطاولة إن وجدت
        'If CurrentDraft.Table_ID.HasValue Then
        '    txtTableId.Text = CurrentDraft.Table_ID.Value.ToString()
        'Else
        '    txtTableId.Text = ""
        'End If

        ' اسم المسودة أو عنوانها
        'lblDraftName.Text = CurrentDraft.DraftName

    End Sub

    Private Sub UpdateDraftTotalsOnScreen()

        If CurrentDraft Is Nothing Then Exit Sub

        Total_TextBox.Text = CurrentDraft.Total.ToString("0.000")
        Discount_txt.Text = CurrentDraft.Discount.ToString("0.000")
        Pure_txt.Text = CurrentDraft.Pure.ToString("0.000")
        IM_Count_LB.Text = " المواد: " & CurrentDraft.Items.Count.ToString()

    End Sub


    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                If New_butt.Enabled = True Then ResetNewBill()
            Case Keys.F12
                If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
            Case Keys.F2
                If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
            Case Keys.F3
                If Edit_butt.Enabled = True And Edit_butt.Visible = True Then Edit_butt_Click(sender, e)
            'Case Keys.F4
            '    If Delete_butt.Enabled = True And Delete_butt.Visible = True Then Delete_butt_Click(sender, e)

            Case Keys.F7
                SBPauseBtn_Click(sender, e)

            'Case Keys.PageUp
            '    Up_Bill_btn_Click(sender, e)
            'Case Keys.PageDown
            '    Down_Bill_btn_Click(sender, e)


            Case 107 'Add

                'If On_Update = False Then
                'If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                If dgvSales.Rows.Count > 0 Then


                    'Dim Def As Double = 1

                    'If IM_min_QTY = False Then
                    '    If IM_Check_Neg_QTY_2(Def) = 1 Then
                    '        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Exclamation, "")
                    '        Exit Sub
                    '    Else
                    '        Change_IM_Qty(Def)
                    '    End If
                    'Else
                    '    Change_IM_Qty(Def)
                    'End If


                    Dim Def As Double = 1
                    ChangeQtyByInput(Def)

                    'Dim inp = InputBox("ادخل رقم", "مقدار زيادة العدد")
                    'If inp <> "" Then Def = inp

                    'If IM_min_QTY = False Then
                    '    If IM_Check_Neg_QTY_2(Def) = 1 Then
                    '        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Exclamation, "")
                    '        Exit Sub
                    '    Else
                    '        Change_IM_Qty(Def)
                    '    End If
                    'Else
                    '    Change_IM_Qty(Def)
                    'End If

                End If
                    'End If
                'End If


            Case 109 'Subtrac
                'If On_Update = False Then
                '    If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                If dgvSales.Rows.Count > 0 Then

                    Dim Def As Double = -1
                    ChangeQtyByInput(Def)

                    'If dgvSales.CurrentRow.Cells("QTY_CL").Value > 1 Then
                    '    Dim Def As Double = -1

                    '    Dim inp = InputBox("ادخل رقم", "مقدار زيادة العدد")
                    '    If inp <> "" Then Def = inp * -1

                    '    'Change_IM_Qty(Def)
                    'End If
                End If
                '    End If
                'End If

            Case Keys.F9
                If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow And dgvSales.Rows.Count > 0 And On_Update = False Then Change_IM_Details.ShowDialog()


            Case Keys.Return
                If Barcode_SH_txt.Enabled = True Then
                    Barcode_SH_txt_KeyDown(sender, e)
                Else
                    e.Handled = True
                End If

            'Case Keys.F8
            '    If RemoveCatButton.Enabled = True Then
            '        If dgvSales.Rows.Count > 0 Then
            '            If MessageBox.Show(" حذف الصنف " + dgvSales.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
            '                               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
            '                SB_Contents_Delete_IM(dgvSales.CurrentRow.Cells("T_ID_CL").Value)
            '            End If
            '        End If
            '    End If

            Case Keys.F11
                If U_SalesDis = True Then Make_Discount()

            Case Keys.ControlKey
                Barcode_SH_txt.Clear()
                Barcode_IM = ""
        End Select
    End Sub


    'Private Sub Change_IM_Qty(def As Double)

    '    If CurrentDraft Is Nothing Then Exit Sub
    '    If dgvSales.CurrentRow Is Nothing Then Exit Sub

    '    Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()

    '    Dim item As SaleDraftItem =
    '    CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

    '    If item Is Nothing Then Exit Sub

    '    Dim newQty As Decimal = item.QTY + CDec(def)

    '    If newQty < 0 Then
    '        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Exclamation, "")
    '        Exit Sub
    '    End If


    '    If newQty = 0 Then
    '        CurrentDraft.Items.Remove(item)
    '    Else
    '        item.QTY = newQty
    '    End If

    '    DraftCalculator.RecalculateDraft(CurrentDraft)
    '    DraftManager.SaveDraft(CurrentDraft)
    '    LoadDraftToGrid()
    '    UpdateDraftTotalsOnScreen()

    'End Sub

    Private Sub ChangeSelectedDraftItemQty(deltaQty As Decimal)

        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.CurrentRow Is Nothing Then Exit Sub

        Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()

        Dim item As SaleDraftItem =
        CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

        If item Is Nothing Then Exit Sub

        Dim newQty As Decimal = item.QTY + deltaQty

        If newQty < 0 Then
            MessageBox.Show("لا يمكن أن تصبح الكمية سالبة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        item.QTY = newQty

        DraftCalculator.RecalculateDraft(CurrentDraft)
        DraftManager.SaveDraft(CurrentDraft)
        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()

    End Sub

    Private Sub ChangeQtyByInput(def_type As Integer, Optional choice As Boolean = True)

        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.CurrentRow Is Nothing Then Exit Sub

        Dim str As String = ""

        If def_type = 1 Then
            str = "مقدار زيادة العدد"
        Else
            str = "مقدار نقص العدد"
        End If

        Dim def As Decimal = 0D


        Dim inp As String = ""

        If choice = True Then
            inp = InputBox("ادخل رقم", str)
        End If


        If inp.Trim() = "" Then
            If def_type = 1 Then
                def = 1
            Else
                def = -1
            End If
        Else
            def = inp
            If def_type = -1 Then def = def * -1
        End If




        'If Not Decimal.TryParse(inp, def) Then
        '    MsgBox("القيمة المدخلة غير صحيحة", MsgBoxStyle.Exclamation, "")
        '    Exit Sub
        'End If

        ChangeSelectedDraftItemQty(def)

    End Sub

    'Private Function CheckNegativeQtyForSelectedLine(deltaQty As Decimal) As Boolean

    '    If CurrentDraft Is Nothing Then Return True
    '    If dgvSales.CurrentRow Is Nothing Then Return True

    '    Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()

    '    Dim item As SaleDraftItem =
    '    CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

    '    If item Is Nothing Then Return True

    '    Dim newQty As Decimal = item.QTY + deltaQty

    '    If newQty < 0 Then
    '        Return True
    '    End If

    '    Return False

    'End Function

    'Private Sub ChangeQtyByInput()

    '    If CurrentDraft Is Nothing Then Exit Sub
    '    If dgvSales.CurrentRow Is Nothing Then Exit Sub

    '    Dim def As Decimal = 0D
    '    Dim inp As String = InputBox("ادخل رقم", "مقدار زيادة العدد")

    '    If inp.Trim() = "" Then Exit Sub

    '    If Not Decimal.TryParse(inp, def) Then
    '        MsgBox("القيمة المدخلة غير صحيحة", MsgBoxStyle.Exclamation, "")
    '        Exit Sub
    '    End If

    '    If IM_min_QTY = False Then
    '        If CheckNegativeQtyForSelectedLine(def) Then
    '            MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Exclamation, "")
    '            Exit Sub
    '        Else
    '            ChangeSelectedDraftItemQty(def)
    '        End If
    '    Else
    '        ChangeSelectedDraftItemQty(def)
    '    End If

    'End Sub

    Public Function IM_Check_Neg_QTY_2(qty)

        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@ST_ID", dgvSales.CurrentRow.Cells("ST_ID_CL").Value)
            .Parameters.AddWithValue("@IM_ID", dgvSales.CurrentRow.Cells("Bill_IMID_CL").Value)
            .Parameters.AddWithValue("@D_Vaild", dgvSales.CurrentRow.Cells("D_Valid_CL").Value)
            .Parameters.AddWithValue("@Enterd_Qty", qty)
            '.Parameters.AddWithValue("@Cargo", )
            .Parameters.AddWithValue("@U_ID", dgvSales.CurrentRow.Cells("U_ID_CL").Value)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then F = .Parameters("@F").Value
        End With

        Return F

    End Function

    'Private Sub Change_IM_Qty(def As Integer)
    '    Dim SB_T_ID As Integer = dgvSales.CurrentRow.Cells("T_ID_CL").Value
    '    Dim Row_Index As Integer = dgvSales.CurrentCell.RowIndex
    '    Dim c As New C
    '    With c.Com
    '        .Connection = c.Con
    '        .CommandText = "SB_Contents_Change_IM_Qty"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", SB_T_ID)
    '        .Parameters.AddWithValue("@Def", def)
    '        .Parameters.AddWithValue("@On_Update", On_Update)
    '    End With

    '    If SQL_SP_EXEC(c.Com) = True Then
    '        SB_Contents_SELECT_Bill()
    '        dgvSales.CurrentCell = dgvSales.Rows(Row_Index).Cells("EX_Name_CL")
    '        Network_Edit_Tracker_insert(" الصنف:" + dgvSales.CurrentRow.Cells("EX_Name_CL").Value.ToString + " العدد:" + dgvSales.CurrentRow.Cells("QTY_CL").Value.ToString + " السعر:" + dgvSales.CurrentRow.Cells("Price_CL").Value.ToString,
    '            Bill_ID_Txt.Text, 1, 3)
    '    End If
    'End Sub

    Private Sub loadShortCut_IM()
        IMPanel.Controls.Clear()


        Dim c As New C
        Dim x = 2.5
        Dim y = 2.5
        Dim counter = 1
        Dim IMName As String

        Dim s As String = ""

        s = "select IM_ID,item_name,Photo,BK_R,BK_G,BK_B,FK_R,FK_G,FK_B from IM_Menu WHERE is_Shortcut = 1  order by item_name ASC"
        c.Com = New SqlClient.SqlCommand(s, c.Con)
        c.Con.Open()
        c.Dr = c.Com.ExecuteReader
        If c.Dr.HasRows Then
            While c.Dr.Read()
                counter += 1
                Dim IMbtn As New Button
                IMbtn.Name = ("IMbtn" + c.Dr("IM_ID").ToString)
                IMbtn.Tag = c.Dr("IM_ID")
                IMbtn.TextAlign = ContentAlignment.MiddleLeft
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(IMPanel.Size.Width / 8.3, IMPanel.Size.Height / 5.5)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMName = c.Dr("item_name")
                IMbtn.Font = New System.Drawing.Font("Arial", 11, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))
                IMbtn.Text = c.Dr("item_name")
                Controls.Add(IMbtn)
                IMbtn.Parent = IMPanel
                AddHandler IMbtn.Click, AddressOf IMbtn_Click
                IMbtn.BackColor = System.Drawing.SystemColors.Window
                If IsDBNull(c.Dr("BK_R")) Then
                    IMbtn.BackColor = System.Drawing.SystemColors.Window
                Else
                    IMbtn.BackColor = Color.FromArgb(c.Dr("BK_R"), c.Dr("BK_G"), c.Dr("BK_B"))
                End If

                If IsDBNull(c.Dr("FK_R")) = False Then
                    IMbtn.ForeColor = Color.FromArgb(c.Dr("FK_R"), c.Dr("FK_G"), c.Dr("FK_B"))
                End If

                If counter = 9 Then
                    counter = 1
                    x = 2.5
                    y += IMPanel.Size.Height / 5.5
                Else
                    x += IMPanel.Size.Width / 8.3
                End If

            End While
        End If

        c.Con.Close()

    End Sub

    Sub IMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        IM_Name = sender.Text.ToString
        IM_ID = sender.Tag
        Load_IM_By_ID()
    End Sub

    Private Sub POS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

        Select Case e.KeyChar
            Case "+", "-"
                e.Handled = True
                Exit Sub
        End Select

        If Me.Barcode_SH_txt.Focused = False Then
            Barcode_SH_txt.Focus()
            If Me.Barcode_SH_txt.Enabled = True Then
                Barcode_SH_txt.Text = e.KeyChar.ToString
                Barcode_SH_txt.SelectionStart = Barcode_SH_txt.Text.Length
                e.Handled = True
            End If
        End If

    End Sub

    Private Async Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        FormType = 1
        Check_View_Control()
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        EditState = Edit_butt.Text
        loadShortCut_IM()
        GET_Printer_Type()

        AG_ID = Default_AG_ID

        Await Load_ALL_IM()

        'If isShowing_Trans = True Then
        '    T_ID = T_ID_Trans
        '    SB_Contents_SELECT_Bill()
        '    Fill_Bill_Info()
        '    SelectStateBt()
        '    New_butt.Enabled = False
        '    SBPauseBtn.Enabled = False
        'Else
        '    If Open_NewBill_When_OpenSale = True Then ResetNewBill()
        'End If

    End Sub


    Public Async Function Load_ALL_IM() As Task
        Dim c As New C
        Dim s As String
        Try
            IM_Units_Dt = New DataTable()
            s = "SELECT U_IM_ID, IM_ID, item_name, U_Name, U_ID, U_Cargo, Price, Min_SP, Min_SP_2,Percent_Price,Barcode FROM IM_Menu_Units_V ORDER BY IM_ID, U_ID ASC"

            Using cmd As New SqlCommand(s, c.Con)
                Await c.Con.OpenAsync()
                Using reader = Await cmd.ExecuteReaderAsync()
                    IM_Units_Dt.Load(reader)
                End Using
                c.Con.Close()
            End Using
        Catch ex As Exception
            MsgBox("IM_Units_Dt: " & ex.Message)
            If c.Con.State = ConnectionState.Open Then c.Con.Close()
        End Try
    End Function






    Private Sub GET_Printer_Type()
        Dim c2 As New C
        c2.Str = "select ID,Type from Sales_Bill_Page"
        c2.Da = New SqlClient.SqlDataAdapter(c2.Str, c2.Con)
        c2.Da.Fill(c2.Dt)
        Sales_Bill_Page_cm.DataSource = c2.Dt
        Sales_Bill_Page_cm.DisplayMember = "Type"
        Sales_Bill_Page_cm.ValueMember = "ID"
        Sales_Bill_Page_cm.SelectedValue = Sales_Page_ID
    End Sub


    Public Sub Check_View_Control()
        dgvSales.Columns("Date_").Visible = MY_Settings.S_Date_CL
        dgvSales.Columns("ST_Name_CL").Visible = MY_Settings.S_ST_Name_CL
        dgvSales.Columns("D_Valid_CL").Visible = MY_Settings.S_D_Valid_CL
        dgvSales.Columns("IMUnit_CL").Visible = MY_Settings.S_IMUnit_CL
        dgvSales.Columns("Price_CL").Visible = MY_Settings.S_Price_CL
        dgvSales.Columns("Total_CL").Visible = MY_Settings.S_Total_CL
        dgvSales.Columns("Notes_CL").Visible = MY_Settings.SP_Notes_CL
        dgvSales.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL
        dgvSales.Columns("Barcode_CL").Visible = MY_Settings.S_Barcode_CL
        dgvSales.Columns("Serial_Code_CL").Visible = MY_Settings.S_Serial_Code_CL
        dgvSales.Columns("IM_Discount_CL").Visible = MY_Settings.S_IM_Discount_CL


        Delete_butt.Visible = U_SalesVoid
        If U_SalesDis = True And isDiscount = True Then
            DiscountPanel.Visible = True
        Else
            DiscountPanel.Visible = False
        End If
        Edit_butt.Visible = U_SB_Update
        Show_Cash_btn.Visible = U_SB_Show_Cash
        'If U_SB_IM_Update = True Then
        '    IM_Price.ReadOnly = False
        'Else
        '    IM_Price.ReadOnly = True
        'End If
        Show_Cash_btn.Visible = S_Pr
        IM_Profet_btn.Visible = U_Show_Bill_Profet
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
        dgvSales.DataSource = C.Dt
        If dgvSales.Rows.Count > 0 Then dgvSales.CurrentCell = dgvSales.Rows(dgvSales.Rows.Count - 1).Cells("EX_Name_CL")
        'Calc_Total()
    End Sub

    'Public Sub Fill_Bill_Info()

    '    Dim C As New C

    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "SB_Info_V_SELECT_Bill"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", Me.T_ID)
    '    End With
    '    C.Con.Open()
    '    C.Dr = C.Com.ExecuteReader
    '    If C.Dr.HasRows Then
    '        C.Dr.Read()

    '        AG_ID = C.Dr("AG_ID")
    '        AG_SH_txt.Text = C.Dr("AG_Name")

    '        DateTimeEx.Text = C.Dr("Date")
    '        Barcode = C.Dr("Barcode")
    '        If C.Dr("isPied") = 1 Then
    '            Save_butt.Enabled = False
    '        Else
    '            Save_butt.Enabled = True
    '        End If
    '        Bill_ID_Txt.Text = C.Dr("SB_ID")
    '        SB_ID = C.Dr("SB_ID")
    '        If C.Dr("Discount") > 0 Then
    '            Discount_txt.Text = C.Dr("Discount")
    '            Disc = C.Dr("Discount")
    '            If Discount_Distribute = False Then Pure_txt.Text = C.Dr("Total") - C.Dr("Discount")
    '        End If
    '        isVoid = C.Dr("isVoid")
    '        isDepended = C.Dr("isDepended")

    '        'isPied = C.Dr("isPied")

    '        User_Name_lb.Text = C.Dr("U_Name") + " - " + C.Dr("Date").ToString
    '        BillUser_ID = C.Dr("User_ID")
    '        isPause = C.Dr("isPause")
    '        If isPause = False Then
    '            SBPauseBtn.Text = "تعليق F7"
    '        Else
    '            SBPauseBtn.Text = "إلغاء التعليق"
    '        End If

    '        txtNotes.Text = C.Dr("About")

    '    Else
    '        AG_ID = Default_AG_ID
    '        VoidLb.Enabled = False
    '    End If
    '    C.Con.Close()
    'End Sub


    Private Sub Enable_Fields()
        DateTimeEx.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        DateTimeEx.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        Barcode_SH_txt.Enabled = False
        '  QtyTextBox.Enabled = False
        ' IM_Price = 0
        RemoveCatButton.Enabled = False
        IMPanel.Enabled = False
        txtNotes.Enabled = False
        IM_Search_btn.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        Barcode_SH_txt.Enabled = True
        ' QtyTextBox.Enabled = True
        '  IM_Price.Enabled = True
        RemoveCatButton.Enabled = True
        IMPanel.Enabled = True
        txtNotes.Enabled = True
        IM_Search_btn.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1
            dgvSales.BackgroundColor = Color.LightGreen
            dgvSales.RowsDefaultCellStyle.BackColor = Color.LightGreen
            Save_butt.Enabled = False
        Else
            isDepended = 0
            dgvSales.BackgroundColor = Color.LightYellow
            dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Save_butt.Enabled = True
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        Me.Text = "فاتورة مبيعات جديدة"
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
        Edit_butt.Text = EditState
        If isVoid = True Then
            VoidLb.Visible = True
            Disable_Fields()
            Save_butt.Enabled = False
            Edit_butt.Enabled = False
            Delete_butt.Enabled = False
            dgvSales.Enabled = True
            dgvSales.BackgroundColor = Color.IndianRed
            dgvSales.RowsDefaultCellStyle.BackColor = Color.IndianRed
            Print_btn.Enabled = False
            DiscountPanel.Enabled = False
            DeliveryingButton.Enabled = False

        Else

            If isDepended = False Then
                Save_butt.Enabled = True
                Edit_butt.Enabled = False
                Print_btn.Enabled = False
                dgvSales.BackgroundColor = Color.LightYellow
                dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow
                Enable_Fields()
                DiscountPanel.Enabled = True

            Else
                Save_butt.Enabled = False
                Edit_butt.Enabled = True
                Print_btn.Enabled = True
                dgvSales.BackgroundColor = Color.LightGreen
                dgvSales.RowsDefaultCellStyle.BackColor = Color.LightGreen
                Disable_Fields()
                DiscountPanel.Enabled = False
                DeliveryingButton.Enabled = True
            End If

            VoidLb.Visible = False
            Delete_butt.Enabled = True

            If U_Save_otherBill = False And BillUser_ID <> USER_ID Then

                If Edit_butt.Enabled = True Then Edit_butt.Enabled = False
                If Save_butt.Enabled = True Then Save_butt.Enabled = False
                If Delete_butt.Enabled = True Then Delete_butt.Enabled = False
                'Barcode_SH_txt.Enabled = False
                'RemoveCatButton.Enabled = False
                'If IM_Search_btn.Enabled = True Then IM_Search_btn.Enabled = False
                If dgvSales.Enabled = True Then dgvSales.Enabled = False
                Disable_CatFields()
                Disable_Fields()
            End If

        End If

        Me.Text = "عرض بيانات فاتورة"
    End Sub


    Private Sub ClearFields()
        'T_ID = 0
        IM_Price = 0
        Total_TextBox.Clear()
        DateTimeEx.Text = Date.Now
        VoidLb.Visible = False
        isVoid = False
        isDepended = False
        ClearCatFields()
        Discount_txt.Clear()
        Disc = 0
        Me.Text = FormState
        Edit_butt.BackColor = Color.WhiteSmoke
        Edit_butt.Text = EditState
        On_Update = False
        SB_ID = 0
    End Sub


    Public Sub ResetNewBill()
        Dim Insert_New As Integer = 0
        If dgvSales.Rows.Count > 0 And isDepended = False Then Insert_New = 1
        Load_PauseBills()
        ClearFields()
        'Call_New_Bill(Insert_New)
        NewStateBt()
    End Sub


    'Private Sub Call_New_Bill(Insert_New)
    '    Dim C As New C

    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "Call_New_SalesBill"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", 0)
    '        .Parameters.AddWithValue("@AG_ID", SB_Default_AG)
    '        .Parameters.AddWithValue("@Bill_Num", 0)
    '        .Parameters.AddWithValue("@SB_ID", 0)
    '        .Parameters.AddWithValue("@isNew", 0)
    '        '.Parameters.AddWithValue("@SB_Type", SB_DefaultStatus)
    '        .Parameters.AddWithValue("@Pr_ID", Pr_ID)
    '        .Parameters.AddWithValue("@isPied", 0)
    '        .Parameters.AddWithValue("@User_ID", USER_ID)
    '        .Parameters.AddWithValue("@Insert_New", Insert_New)

    '        .Parameters("@T_ID").Direction = ParameterDirection.Output
    '        .Parameters("@Bill_Num").Direction = ParameterDirection.Output
    '        .Parameters("@isNew").Direction = ParameterDirection.Output
    '        .Parameters("@SB_ID").Direction = ParameterDirection.Output
    '    End With
    '    If SQL_SP_EXEC(C.Com) = True Then
    '        Me.T_ID = C.Com.Parameters("@T_ID").Value
    '        Bill_ID_Txt.Text = C.Com.Parameters("@SB_ID").Value.ToString()
    '        SB_Contents_SELECT_Bill()
    '        Fill_Bill_Info()
    '        SelectStateBt()
    '        Refresh_IM_MENU()
    '    End If
    'End Sub

    Private Async Sub Refresh_IM_MENU()
        Await Load_ALL_IM()
    End Sub


    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        'If dgvSales.Rows.Count > 0 Then
        '    'Save_Date(T_ID, DateTimeEx)
        '    'ConfermBill()
        '    PushCurrentDraftToDatabase()
        'End If


        If CurrentDraft Is Nothing Then
            MessageBox.Show("لا توجد فاتورة حالية.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim result = MessageBox.Show(
            "هل تريد اعتماد الفاتورة وحفظها نهائيًا؟",
            "تأكيد",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If result <> DialogResult.Yes Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        'BtnPushFinal.Enabled = False

        Try
            Dim ok As Boolean = PushCurrentDraftToDatabase()

            If ok Then
                DraftManager.ArchiveDraft(CurrentDraft)

                'MessageBox.Show(
                '    "تم حفظ الفاتورة بنجاح." & Environment.NewLine &
                '    "T_ID = " & If(CurrentDraft.Final_T_ID.HasValue, CurrentDraft.Final_T_ID.Value.ToString(), "") & Environment.NewLine &
                '    "SB_ID = " & If(CurrentDraft.Final_SB_ID.HasValue, CurrentDraft.Final_SB_ID.Value.ToString(), ""),
                '    "نجاح",
                '    MessageBoxButtons.OK,
                '    MessageBoxIcon.Information)


                Disable_Fields()
                ClearCatFields()
            End If

        Finally
            'BtnPushFinal.Enabled = True
            Me.Cursor = Cursors.Default
        End Try

    End Sub




    Private Function ValidateDraftBeforePush() As Boolean

        If CurrentDraft Is Nothing Then
            MessageBox.Show("لا توجد فاتورة حالية.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If CurrentDraft.AG_ID <= 0 Then
            MessageBox.Show("يجب اختيار العميل أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If CurrentDraft.Items Is Nothing OrElse CurrentDraft.Items.Count = 0 Then
            MessageBox.Show("لا يمكن حفظ فاتورة بدون أصناف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        For Each item As SaleDraftItem In CurrentDraft.Items
            If item.IM_ID <= 0 Then
                MessageBox.Show("يوجد صنف غير صحيح داخل الفاتورة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If item.U_ID <= 0 Then
                MessageBox.Show("يوجد سطر بدون وحدة صحيحة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If item.ST_ID <= 0 Then
                MessageBox.Show("يوجد سطر بدون مخزن صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If item.QTY <= 0 Then
                MessageBox.Show("يوجد سطر كميته أقل من أو تساوي صفر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If item.Price < 0 Then
                MessageBox.Show("يوجد سطر سعره غير صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
        Next

        Return True

    End Function

    Private Function PushCurrentDraftToDatabase() As Boolean

        If Not ValidateDraftBeforePush() Then Return False

        DraftCalculator.RecalculateDraft(CurrentDraft)

        Dim detailsTable As DataTable = BuildDetailsTable(CurrentDraft.Items)

        Try
            Using con As New SqlConnection(MY_Settings.SqlConStr) ' عدّل اسم الاتصال عندك
                Using cmd As New SqlCommand("dbo.PushSalesDraft", con)

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 120

                    cmd.Parameters.Add("@AG_ID", SqlDbType.Int).Value = CurrentDraft.AG_ID

                    If CurrentDraft.S_Bill_Pr_ID.HasValue Then
                        cmd.Parameters.Add("@S_Bill_Pr_ID", SqlDbType.Int).Value = CurrentDraft.S_Bill_Pr_ID.Value
                    Else
                        cmd.Parameters.Add("@S_Bill_Pr_ID", SqlDbType.Int).Value = DBNull.Value
                    End If

                    If CurrentDraft.Table_ID.HasValue Then
                        cmd.Parameters.Add("@Table_ID", SqlDbType.Int).Value = CurrentDraft.Table_ID.Value
                    Else
                        cmd.Parameters.Add("@Table_ID", SqlDbType.Int).Value = DBNull.Value
                    End If

                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = CurrentDraft.Date
                    cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = CurrentDraft.Discount
                    cmd.Parameters("@Discount").Precision = 18
                    cmd.Parameters("@Discount").Scale = 3

                    cmd.Parameters.Add("@About", SqlDbType.NVarChar).Value =
                    If(String.IsNullOrWhiteSpace(CurrentDraft.About), CType(DBNull.Value, Object), CurrentDraft.About)

                    cmd.Parameters.Add("@BsType_ID", SqlDbType.Int).Value = CurrentDraft.BsType_ID
                    cmd.Parameters.Add("@isVoid", SqlDbType.Int).Value = CurrentDraft.isVoid

                    If CurrentDraft.isPied.HasValue Then
                        cmd.Parameters.Add("@isPied", SqlDbType.Int).Value = CurrentDraft.isPied.Value
                    Else
                        cmd.Parameters.Add("@isPied", SqlDbType.Int).Value = DBNull.Value
                    End If

                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = CurrentDraft.User_ID

                    If CurrentDraft.Markter_ID.HasValue Then
                        cmd.Parameters.Add("@Markter_ID", SqlDbType.Int).Value = CurrentDraft.Markter_ID.Value
                    Else
                        cmd.Parameters.Add("@Markter_ID", SqlDbType.Int).Value = DBNull.Value
                    End If

                    Dim pDetails As New SqlParameter("@Details", SqlDbType.Structured)
                    pDetails.TypeName = "dbo.SB_Contents_DraftType"
                    pDetails.Value = detailsTable
                    cmd.Parameters.Add(pDetails)

                    con.Open()

                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        If dr.Read() Then

                            Dim isSuccess As Boolean = False

                            If Not IsDBNull(dr("IsSuccess")) Then
                                isSuccess = Convert.ToBoolean(dr("IsSuccess"))
                            End If

                            If isSuccess Then
                                If Not IsDBNull(dr("Header_T_ID")) Then
                                    CurrentDraft.Final_T_ID = Convert.ToInt32(dr("Header_T_ID"))
                                    T_ID = CurrentDraft.Final_T_ID
                                End If

                                If Not IsDBNull(dr("SB_ID")) Then
                                    CurrentDraft.Final_SB_ID = Convert.ToInt32(dr("SB_ID"))
                                    Bill_ID_Txt.Text = CurrentDraft.Final_SB_ID
                                End If



                                CurrentDraft.PushedAt = DateTime.Now
                                Return True
                            Else
                                Dim errMsg As String = "فشل ترحيل الفاتورة."
                                If HasColumn(dr, "ErrorMessage") AndAlso Not IsDBNull(dr("ErrorMessage")) Then
                                    errMsg = dr("ErrorMessage").ToString()
                                End If

                                MessageBox.Show(errMsg, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Return False
                            End If
                        Else
                            MessageBox.Show("لم يتم استلام نتيجة من الإجراء.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ أثناء الحفظ النهائي", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

    End Function

    Private Function HasColumn(reader As SqlDataReader, columnName As String) As Boolean
        For i As Integer = 0 To reader.FieldCount - 1
            If reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If
        Next
        Return False
    End Function


    'Public Function PushDraftToDatabase(draft As SaleDraftHeader, con As SqlConnection) As Boolean
    '    Try
    '        Using cmd As New SqlCommand("dbo.PushSalesDraft", con)
    '            cmd.CommandType = CommandType.StoredProcedure

    '            cmd.Parameters.AddWithValue("@AG_ID", draft.AG_ID)

    '            If draft.S_Bill_Pr_ID.HasValue Then
    '                cmd.Parameters.AddWithValue("@S_Bill_Pr_ID", draft.S_Bill_Pr_ID.Value)
    '            Else
    '                cmd.Parameters.AddWithValue("@S_Bill_Pr_ID", DBNull.Value)
    '            End If

    '            If draft.Table_ID.HasValue Then
    '                cmd.Parameters.AddWithValue("@Table_ID", draft.Table_ID.Value)
    '            Else
    '                cmd.Parameters.AddWithValue("@Table_ID", DBNull.Value)
    '            End If

    '            cmd.Parameters.AddWithValue("@Date", draft.Date)
    '            cmd.Parameters.AddWithValue("@Discount", draft.Discount)
    '            cmd.Parameters.AddWithValue("@About", If(draft.About, CType(DBNull.Value, Object)))
    '            cmd.Parameters.AddWithValue("@BsType_ID", draft.BsType_ID)
    '            cmd.Parameters.AddWithValue("@isVoid", draft.isVoid)

    '            If draft.isPied.HasValue Then
    '                cmd.Parameters.AddWithValue("@isPied", draft.isPied.Value)
    '            Else
    '                cmd.Parameters.AddWithValue("@isPied", DBNull.Value)
    '            End If

    '            cmd.Parameters.AddWithValue("@User_ID", draft.User_ID)

    '            If draft.Markter_ID.HasValue Then
    '                cmd.Parameters.AddWithValue("@Markter_ID", draft.Markter_ID.Value)
    '            Else
    '                cmd.Parameters.AddWithValue("@Markter_ID", DBNull.Value)
    '            End If

    '            Dim detailsTable As DataTable = BuildDetailsTable(draft.Items)

    '            Dim p As New SqlParameter("@Details", SqlDbType.Structured)
    '            p.TypeName = "dbo.SB_Contents_DraftType"
    '            p.Value = detailsTable
    '            cmd.Parameters.Add(p)

    '            If con.State <> ConnectionState.Open Then
    '                con.Open()
    '            End If

    '            Using dr = cmd.ExecuteReader()
    '                If dr.Read() Then
    '                    Dim isSuccess As Boolean = Convert.ToBoolean(dr("IsSuccess"))

    '                    If isSuccess Then
    '                        If Not IsDBNull(dr("Header_T_ID")) Then
    '                            draft.Final_T_ID = Convert.ToInt32(dr("Header_T_ID"))
    '                        End If

    '                        If Not IsDBNull(dr("SB_ID")) Then
    '                            draft.Final_SB_ID = Convert.ToInt32(dr("SB_ID"))
    '                        End If

    '                        draft.PushedAt = DateTime.Now
    '                        Return True
    '                    Else
    '                        Dim msg As String = dr("ErrorMessage").ToString()
    '                        MessageBox.Show(msg, "خطأ في ترحيل الفاتورة", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                        Return False
    '                    End If
    '                End If
    '            End Using
    '        End Using

    '        Return False

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    End Try
    'End Function



    'Public Sub ConfermBill()

    '    Dim F As New Pay_Main_Form
    '    F.MONEY_VALUE = Pure
    '    F.Temp_Tr_ID = SB_TR_ID
    '    F.AG_ID = AG_ID
    '    F.ShowDialog()

    '    If F.is_OK = True Then
    '        Dim Tr_ID, Pay_ID As Integer
    '        Tr_ID = F.Tr_ID
    '        Pay_ID = F.Pay_ID

    '        Dim c As New C
    '        With c.Com
    '            .Connection = c.Con
    '            .CommandText = "SB_ConfermBill"
    '            .CommandType = CommandType.StoredProcedure
    '            .Parameters.AddWithValue("@T_ID", Me.T_ID)

    '            '.Parameters.AddWithValue("@TOTAL", TOTAL)
    '            '.Parameters.AddWithValue("@Discount", Disc)
    '            '.Parameters.AddWithValue("@Pure", Pure)
    '            .Parameters.AddWithValue("@TOTAL", Total_TextBox.Text)
    '            .Parameters.AddWithValue("@Discount", Discount_txt.Text)
    '            .Parameters.AddWithValue("@Pure", Pure_txt.Text)

    '            .Parameters.AddWithValue("@AGType_ID", 1)
    '            .Parameters.AddWithValue("@Tr_ID", Tr_ID) 'SB_TR_ID
    '            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
    '            .Parameters.AddWithValue("@User_ID", USER_ID)
    '            .Parameters.AddWithValue("@Pay_ID", Pay_ID)

    '        End With
    '        If SQL_SP_EXEC(c.Com) = True Then
    '            Switch_Dependcy(1)
    '            If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
    '            If SB_AutoPrint = True Then
    '                Me.Cursor = Cursors.AppStarting
    '                CashPrint(Sales_BillPage_Bill_Track, Sales_Page_ID)
    '                Me.Cursor = Cursors.Default
    '            End If
    '            SelectStateBt()

    '            If MY_Settings.S_OpenNextBill = True Then
    '                ClearFields()
    '                Call_New_Bill(0)
    '            End If

    '        End If

    '    End If

    'End Sub

    'Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click

    '    Beep()
    '    If MessageBox.Show(" سيتم إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "إلغــاء فاتورة", MessageBoxButtons.OKCancel,
    '           MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
    '        Cancel_Bill()
    '    End If
    'End Sub

    'Private Sub Cancel_Bill()

    '    Dim sqlComm As New SqlClient.SqlCommand
    '    sqlComm.CommandText = "AG_Balance_Void_Row"
    '    sqlComm.CommandType = CommandType.StoredProcedure
    '    sqlComm.Parameters.AddWithValue("@T_ID", T_ID)

    '    If SQL_SP_EXEC(sqlComm) = True Then
    '        MsgBox("تم إلغاء الفاتورة", MsgBoxStyle.Information)
    '        Network_Edit_Tracker_insert("إلغاء الفاتورة", Bill_ID_Txt.Text, 1, 3)
    '        isVoid = True
    '        SelectStateBt()
    '    End If

    'End Sub

    Private Sub TreasuryCard_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Total_TextBox.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgvSales.MouseDoubleClick
        FormType = 1
        If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow And dgvSales.Rows.Count > 0 Then Change_IM_Details.ShowDialog()
    End Sub

    Public Sub Calc_Total()
        TOTAL = 0
        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then
            Disc = 0
            Discount_txt.Text = "0"
        End If

        Dim QTY As Double = 0
        For i = 0 To dgvSales.Rows.Count - 1
            TOTAL = TOTAL + dgvSales.Rows(i).Cells("Total_CL").Value
            QTY += dgvSales.Rows(i).Cells("QTY_CL").Value
        Next

        Total_TextBox.Text = TOTAL '.ToString("N")

        Pure = (TOTAL - Disc)

        Pure_txt.Text = Pure '.ToString("N")
        IM_Count_LB.Text = dgvSales.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "

    End Sub

    Public Sub ADD_IM()

        '  If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
        QtyTextBox = 1

        If S_Allow_MinSP = True Then
            If User_isAdmin = False Then
                If U_Sell_Under_Min_SP = True Then
                    If IM_Price < Min_SP And Min_SP > 0 Then
                        '  My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                        System.Media.SystemSounds.Hand.Play()
                        MsgBox(" ( " + Min_SP.ToString + " ) لا يمكنك البيع بأقل من أدنى سعر بيع", MsgBoxStyle.Exclamation)
                        ClearCatFields()
                        Exit Sub
                    End If
                End If

            Else
                If IM_Price < Min_SP And Min_SP > 0 Then
                    System.Media.SystemSounds.Hand.Play()
                    '   My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                    If MessageBox.Show(" ( " + Min_SP.ToString + " ) سوف يتم البيع بأقل من أدنى سعر بيع", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                                       MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
                        ClearCatFields()
                        Exit Sub
                    End If

                End If
            End If
        End If


        'If U_SB_Sell_Under_Cost = False Then
        '    If Show_IM_Cost(False, IM_ID, U_ID) > IM_Price.Text Then
        'System.Media.SystemSounds.Hand.Play()
        '        My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
        '        MsgBox("لا يمكنك البيع بأقل من سعر التكلفة", MsgBoxStyle.Critical)
        '        ClearCatFields()
        '        Exit Sub
        '    End If
        'Else

        '    If Show_IM_Cost(False, IM_ID, U_ID) > IM_Price.Text Then
        'System.Media.SystemSounds.Hand.Play()
        '        My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
        '        If MessageBox.Show(" سوف يتم البيع بأقل من سعر التكلفة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
        '                                      MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
        '            ClearCatFields()
        '            Exit Sub
        '        End If

        '    End If
        'End If

        If IM_min_QTY = False Then

            If IM_Check_Neg_QTY_() = 1 Then
                ' If QTY_ALERT_SOUND = True Then My.Computer.Audio.Play(Application.StartupPath & "\QTY ALERT.wav")
                If IM_min_QTY = False Then
                    System.Media.SystemSounds.Hand.Play()
                    '    My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                    MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                    ClearCatFields()
                    Exit Sub
                End If
            End If

        End If

        If SB_IM_Alert_When_Repetition = True Then
            For i = 0 To dgvSales.Rows.Count - 1
                If dgvSales.Rows(i).Cells("Bill_IMID_CL").Value = IM_ID Then
                    System.Media.SystemSounds.Hand.Play()
                    '  My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                    If MessageBox.Show(" هذا الصنف تم إدراجه بالفاتورة ... هل تريد الإستمرار ؟ ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                        ClearCatFields()
                        Exit Sub
                    Else
                        Add_ItemToBill(IM_ID)
                        Exit Sub
                    End If
                End If
            Next
        End If

        Beep()
        If Notif_If_SB_Has_No_SB_Price = True Then
            If IM_Price = 0 Then
                System.Media.SystemSounds.Hand.Play()
                '  My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                If MessageBox.Show(" لم يتم تحديد سعر بيع للصنف أوسعره = 0 ... هل تريد الإستمرار فالبيع ", "",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                    ClearCatFields()
                    Exit Sub
                End If
            End If
        End If

        Dim cachedItem As New CachedSaleItem With {
        .ItemId = Convert.ToInt32(IM_ID),
        .ItemName = IM_Name.ToString(),
        .Barcode = Barcode_IM,
        .SellPrice = Convert.ToDecimal(IM_Price),
        .UnitId = Convert.ToInt64(IM_U_ID),
        .UnitName = IM_Unit_Name.ToString(),
        .StoreId = Convert.ToInt64(SB_ST_ID),
        .Equal = Convert.ToDouble(U_Cargo),
        .Cost = Convert.ToDouble(IM_Cost)
    }

        'Add_ItemToBill(IM_ID)


        AddItemFromCache(cachedItem)

    End Sub


    Private Sub AddItemFromCache(cachedItem As CachedSaleItem)
        EnsureDraftExists()

        If cachedItem Is Nothing Then Exit Sub

        Dim item As New SaleDraftItem With {
        .IM_ID = cachedItem.ItemId,
        .ItemName = cachedItem.ItemName,
        .Barcode = cachedItem.Barcode,
        .Price = cachedItem.SellPrice,
        .QTY = 1D,
        .U_ID = cachedItem.UnitId,
        .UnitName = cachedItem.UnitName,
        .ST_ID = cachedItem.StoreId,
        .U_Cargo = cachedItem.Equal,
        .Cost = cachedItem.Cost,
        .Compons = "",
        .D_Vaild = "",
        .Date_ = DateTime.Now
    }

        DraftItemService.AddItem(CurrentDraft, item)

        DraftManager.SaveDraft(CurrentDraft)
        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()

    End Sub

    Private Function BuildDetailsTable(items As List(Of SaleDraftItem)) As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("IM_ID", GetType(Integer))
        dt.Columns.Add("U_ID", GetType(Long))
        dt.Columns.Add("ST_ID", GetType(Long))
        dt.Columns.Add("Date", GetType(DateTime))
        dt.Columns.Add("Compons", GetType(String))
        dt.Columns.Add("Cost", GetType(Double))
        dt.Columns.Add("Price", GetType(Decimal))
        dt.Columns.Add("D_Vaild", GetType(String))
        dt.Columns.Add("QTY", GetType(Decimal))
        dt.Columns.Add("T_Price", GetType(Decimal))
        dt.Columns.Add("U_Cargo", GetType(Double))
        dt.Columns.Add("ST_QTY", GetType(Decimal))
        dt.Columns.Add("isDepended", GetType(Integer))
        dt.Columns.Add("Barcode", GetType(String))

        For Each item In items
            dt.Rows.Add(
            item.IM_ID,
            item.U_ID,
            item.ST_ID,
            item.Date_,
            If(item.Compons, ""),
            If(item.Cost.HasValue, item.Cost.Value, CType(DBNull.Value, Object)),
            item.Price,
            If(item.D_Vaild, ""),
            item.QTY,
            item.T_Price,
            item.U_Cargo,
            item.ST_QTY,
            0,
            item.Barcode
        )
        Next

        Return dt
    End Function

    Public Function IM_Check_Neg_QTY_()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@ST_ID", SB_ST_ID)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@D_Vaild", Valid_TXT)
            .Parameters.AddWithValue("@Enterd_Qty", QtyTextBox)
            .Parameters.AddWithValue("@Cargo", U_Cargo)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then F = .Parameters("@F").Value
        End With

        Return F
    End Function


    Private Sub ClearCatFields()
        QtyTextBox = 1
        IM_ID = 0
        ' Current_QTY.Clear()
        IM_Price = 0
        'QtyTextBox.Clear()
        U_Dt.Clear()
        '  Valid_QTY_txt.Clear()
        '   Valid_Dt.Clear()
        Barcode_SH_txt.Clear()
        Barcode_SH_txt.Select()
        Barcode_IM = ""
        is_Valid = False
        Bercent_Price = 0
        Valid_TXT = ""
    End Sub


    Public Sub Add_ItemToBill(IM_ID As String)
        If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, IM_U_ID)

        If Bercent_Price > 0 Then IM_Price =
    IM_Price + IM_Price * (IM_Price / 100).ToString("00.00")

        'Dim Row_Index As Integer = 0
        'If AGMetroGrid.Rows.Count > 0 Then Row_Index = AGMetroGrid.CurrentCell.RowIndex + 1
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Contents_INSERT_2"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@ST_ID", SB_ST_ID)
            .Parameters.AddWithValue("@QYT", Convert.ToDouble(QtyTextBox))
            .Parameters.AddWithValue("@U_ID", IM_U_ID)
            .Parameters.AddWithValue("@Price", IM_Price)
            .Parameters.AddWithValue("@On_Update", On_Update)
            .Parameters.AddWithValue("@Barcode", Barcode_IM)
            If is_Valid = True Then .Parameters.AddWithValue("@D_Vaild", Valid_TXT)
            If Bercent_Price > 0 Then .Parameters.AddWithValue("@Notes", Bercent_Price.ToString & " % ")
            .Parameters.AddWithValue("@SB_IM_NEW_ROW", SB_IM_NEW_ROW)
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            If On_Update = True Then DependingUpdatedBill(T_ID)

            Network_Edit_Tracker_insert(" الصنف:" + IM_Name + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.ToString() + " السعر:" + IM_Price.ToString(), Bill_ID_Txt.Text, 1, 1)

            SB_Contents_SELECT_Bill()
            ClearCatFields()
            'If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
        End If

    End Sub

    'Private Sub SB_Contents_Delete_IM(T_ID_CL As Integer)
    '    Dim Row_Index As Integer = dgvSales.CurrentCell.RowIndex
    '    Dim c As New C
    '    With c.Com
    '        .Connection = c.Con
    '        .CommandText = "SB_Contents_Delete_IM"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", T_ID_CL)
    '        .Parameters.AddWithValue("@On_Update", On_Update)
    '    End With
    '    If SQL_SP_EXEC(c.Com) = True Then
    '        'Network_Edit_Tracker_insert("حذف صنف لفاتورة مبيعات/ رقم آلي : " + Bill_ID_Txt.Text + " / الصنف :  " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString + " /العدد :  " + AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString + " /السعر :  " + AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Pure_txt.Text, 0, 0)

    '        Network_Edit_Tracker_insert(" الصنف:" + dgvSales.CurrentRow.Cells("EX_Name_CL").Value.ToString + " الوحدة:" + dgvSales.CurrentRow.Cells("IMUnit_CL").Value.ToString + " العدد:" + dgvSales.CurrentRow.Cells("QTY_CL").Value.ToString _
    '                                    + " السعر:" + dgvSales.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 1, 2)

    '        SB_Contents_SELECT_Bill()
    '        If Row_Index > 0 Then dgvSales.CurrentCell = dgvSales.Rows(Row_Index - 1).Cells("EX_Name_CL")
    '    End If
    'End Sub


    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        'If dgvSales.Rows.Count > 0 Then
        '    SB_Contents_Delete_IM(dgvSales.CurrentRow.Cells("T_ID_CL").Value)
        'End If


        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.CurrentRow Is Nothing Then Exit Sub

        Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()

        DraftItemService.RemoveItem(CurrentDraft, draftLineId)

        DraftManager.SaveDraft(CurrentDraft)
        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()
    End Sub

    Private Sub EnsureDraftExists()

        If CurrentDraft Is Nothing Then
            CurrentDraft = DraftManager.CreateNewDraft(USER_ID)
            BindDraftHeaderToForm()
            LoadDraftToGrid()
            UpdateDraftTotalsOnScreen()
        End If

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

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return
                If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode()
                Clear_Barcode()
         '   Case Keys.Down : QtyTextBox.Select()
            Case Keys.Delete
                Clear_Barcode()
        End Select
    End Sub

    Private Sub Clear_Barcode()
        Barcode_SH_txt.Clear()
        Barcode_IM = ""
    End Sub

    Public Sub Load_IM_Barcode()

        IM_Dt.Clear()
        Dim rows As DataRow() = IM_Units_Dt.Select("Barcode = '" & Barcode_SH_txt.Text & "' ")
        If rows.Length > 0 Then

            Dim row As DataRow = rows(0)

            IM_ID = Convert.ToInt32(row("IM_ID"))
            IM_Name = row("item_name").ToString
            Barcode_IM = Barcode_SH_txt.Text
            U_IM_ID = Convert.ToInt32(row("U_IM_ID"))
            IM_Unit_Name = row("U_name").ToString
            Get_Unit = False

            Fetch_IM_Units_By_Bar()
            Barcode_SH_txt.Clear()
            ADD_IM()
        Else

            If Barcode_SH_txt.Text.Count >= 8 Then
                Check_If_Mizan()
            Else
                System.Media.SystemSounds.Hand.Play()

                '   My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                MessageBox.Show("لم يتم التعرف على الإدخال")
                Clear_Barcode()
            End If


        End If

    End Sub


    Public Sub Load_IM_By_ID()

        IM_Dt.Clear()
        Dim rows As DataRow() = IM_Units_Dt.Select("IM_ID = '" & IM_ID & "' ")
        If rows.Length > 0 Then

            Dim row As DataRow = rows(0)

            'IM_ID = Convert.ToInt32(row("IM_ID"))
            IM_Name = row("item_name").ToString
            Barcode_IM = row("Barcode").ToString
            U_IM_ID = Convert.ToInt32(row("U_IM_ID"))
            IM_Unit_Name = row("U_name").ToString
            Get_Unit = False

            Fetch_IM_Units_By_Bar()
            Barcode_SH_txt.Clear()
            ADD_IM()
        Else

            If Barcode_SH_txt.Text.Count >= 8 Then
                Check_If_Mizan()
            Else
                '   My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                System.Media.SystemSounds.Hand.Play()
                MessageBox.Show("لم يتم التعرف على الإدخال")
                Clear_Barcode()
            End If


        End If

        'End Sub

        '--------------------------------------------------------------------------
        'Dim c As New C
        'IM_Dt.Clear()
        'Try
        '    Dim s As String = "select U_IM_ID,U_ID,IM_ID,item_name,Barcode,isValid from IM_units_Search_V WHERE IM_ID = '" & IM_ID & "' AND is_Default = 1"

        '    c.Com = New SqlClient.SqlCommand(s, c.Con)
        '    c.Con.Open()

        '    c.Dr = c.Com.ExecuteReader
        '    If c.Dr.HasRows Then
        '        c.Dr.Read()
        '        IM_ID = c.Dr("IM_ID")
        '        IM_Name = c.Dr("item_name")
        '        Barcode_IM = c.Dr("Barcode")
        '        Barcode_SH_txt.Text = c.Dr("Barcode")
        '        IM_U_ID = c.Dr("U_ID")
        '        U_IM_ID = c.Dr("U_IM_ID")
        '        Get_Unit = False
        '        '  Load_IM_ST_QTY_ST_INT(IM_ID, SB_ST_ID, IM_QTY)
        '        'IMDataGridViewX.Visible = False
        '        'Load_IM_Change_Price()

        '        If c.Dr("isValid") = 1 Then
        '            is_Valid = True
        '            'Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
        '            POS_D_Valid.ST_ID = SB_ST_ID
        '            POS_D_Valid.IM_ID = IM_ID
        '            POS_D_Valid.ShowDialog()
        '            If Valid_Allow_IM = False Then Exit Sub
        '        End If

        '        Fetch_IM_Units_By_Bar()
        '        Barcode_SH_txt.Clear()
        '        ADD_IM()
        '    Else

        '        '   My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
        '        MsgBox("لم يتم التعرف على الإدخال")
        '        Barcode_SH_txt.Clear()
        '        Barcode_IM = ""
        '    End If

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub Fetch_IM_Units_By_Bar()
        Get_Unit = False
        'Dim c As New C
        U_Dt.Clear()
        Try

            Dim rootRows() As DataRow = IM_Units_Dt.Select("Barcode = '" & Barcode_IM & "'")
            If rootRows.Length > 0 Then
                U_Dt = rootRows.CopyToDataTable()
            End If
            IM_Unit_cm.DataSource = U_Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_IM_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Get_Unit = True
        IM_Fetch_QTY()

    End Sub


    Private Sub Check_If_Mizan()
        Dim c As New C
        Dim New_Barcode As String = ""
        Dim Qty As Double = 0
        Dim Qty_Dot As String = ""
        Dim Price As Double = 0
        Dim Price_Dot As String = ""
        Dim T_Price As Double = 0
        Dim T_Price_Dot As String = ""
        'QtyTextBox = 0

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


                IM_ID = c.Dr("IM_ID")
                IM_Name = c.Dr("item_name")
                Barcode_IM = New_Barcode
                Get_Unit = False
                '  Load_IM_ST_QTY_ST_INT(IM_ID, SB_ST_ID, IM_QTY)


                If Second_Part_isPrice = 0 Then
                    For i = Mizan_QtyFrom - 1 To Mizan_QtyTo - 1
                        Qty_Dot += Barcode_SH_txt.Text(i)
                    Next
                    QtyTextBox = Convert.ToDouble(Qty_Dot) / 1000
                Else

                    For i = Mizan_QtyFrom - 1 To Mizan_QtyTo - 1
                        Qty_Dot += Barcode_SH_txt.Text(i)
                    Next
                    Qty = Qty_Dot(0) & Qty_Dot(1)
                    Qty_Dot = "0" & "." & Qty_Dot(2) & Qty_Dot(3) & Qty_Dot(4)
                    Qty = Qty + Convert.ToDouble(Qty_Dot)
                    QtyTextBox = Qty

                    '----------------------------------------------------------------------------

                    For j = Mizan_BarcodeTo To Mizan_QtyFrom - 1
                        T_Price_Dot += Barcode_SH_txt.Text(j)
                    Next
                    T_Price = T_Price_Dot(0) & T_Price_Dot(1) & T_Price_Dot(2)
                    T_Price_Dot = "0" & "." & T_Price_Dot(3) & T_Price_Dot(4)
                    T_Price = T_Price + Convert.ToDouble(T_Price_Dot)
                    '-------------------------------------------------------------------------------
                    IM_Price = Convert.ToDouble(T_Price) / Qty
                End If

                Fetch_IM_Units()
                IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
                Barcode_SH_txt.Clear()
                'Load_IM_Change_Price()

                If c.Dr("isValid") = 1 Then
                    is_Valid = True
                    POS_D_Valid.ST_ID = SB_ST_ID
                    POS_D_Valid.IM_ID = IM_ID
                    POS_D_Valid.ShowDialog()
                    If Valid_Allow_IM = False Then Exit Sub
                End If

                ADD_IM()
            Else
                ' My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                System.Media.SystemSounds.Hand.Play()
                MsgBox("لم يتم التعرف على الإدخال")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub IM_Fetch_QTY()

        'Dim c As New C
        Try

            '------------------------------------------------------------------------------------------------------------------------------------
            Dim rows As DataRow() = IM_Units_Dt.Select("U_IM_ID = " & IM_Unit_cm.SelectedValue)
            If rows.Length > 0 Then

                Dim row As DataRow = rows(0)

                U_Cargo = Convert.ToDouble(row("U_Cargo"))
                ' Dim N As Double = (Convert.ToDouble(IM_QTY) / U_Cargo)
                '   Current_QTY.Text = N.ToString("N")
                IM_Price = row("Price").ToString
                '  ALL_QTY_txt.Text = ALL_QTY / U_Cargo
                IM_U_ID = row("U_ID").ToString

                Min_SP = row("Min_SP").ToString

                Bercent_Price = row("Percent_Price").ToString

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    'Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
    '    Bill_ID_Txt.Text = Convert.ToInt64(Bill_ID_Txt.Text)
    '    Get_T_ID(Convert.ToInt64(Bill_ID_Txt.Text), "-")
    'End Sub

    Public Sub Get_T_ID_By_Barcode(Num As String)
        Dim C As New C
        Dim S As String = "Select T_ID From Agents_Balance_MV Where Barcode = '" & Num & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Move_To_SELECT_Bill()
            Else
                MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                Bill_ID_Txt.Text = SB_ID
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub

    Public Sub Move_To_SELECT_Bill()
        'Fill_Bill_Info()
        SB_Contents_SELECT_Bill()
        SelectStateBt()
    End Sub

    'Public Sub Get_T_ID(S_T_ID As Integer, F As Char)

    '    Dim C As New C
    '    Dim S As String = ""
    '    Select Case F
    '        Case ""
    '            S = "Select TOP 1 T_ID From Agents_Balance_MV Where SB_ID = '" & S_T_ID & "'"
    '        Case "+"
    '            S = "Select TOP 1 T_ID From Agents_Balance_MV Where SB_ID > '" & S_T_ID & "' AND USER_ID = " & USER_ID & " ORDER BY SB_ID ASC "
    '        Case "-"
    '            S = "Select TOP 1 T_ID From Agents_Balance_MV Where SB_ID < '" & S_T_ID & "' AND USER_ID = " & USER_ID & " ORDER BY SB_ID DESC "
    '    End Select


    '    C.Com = New SqlClient.SqlCommand(S, C.Con)
    '    C.Con.Open()
    '    Try
    '        C.Dr = C.Com.ExecuteReader
    '        If C.Dr.HasRows Then
    '            C.Dr.Read()
    '            ClearFields()
    '            T_ID = C.Dr("T_ID")
    '            Fill_Bill_Info()
    '            SB_Contents_SELECT_Bill()
    '            SelectStateBt()
    '        Else
    '            MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
    '            'Bill_ID_Txt.Text = SB_ID
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    C.Con.Close()

    'End Sub

    'Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
    '    If Not String.IsNullOrWhiteSpace(Bill_ID_Txt.Text) Then
    '        Bill_ID_Txt.Text = Convert.ToInt64(Bill_ID_Txt.Text)
    '        Get_T_ID(Convert.ToInt64(Bill_ID_Txt.Text), "+")
    '    End If
    'End Sub


    'Private Sub Bill_ID_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Bill_ID_Txt.KeyDown
    '    If e.KeyCode = Keys.Return Then Get_T_ID(Convert.ToInt64(Bill_ID_Txt.Text), "")
    '    If e.KeyCode = Keys.Up Then Up_Bill_btn_Click(sender, e)
    '    If e.KeyCode = Keys.Down Then Down_Bill_btn_Click(sender, e)
    'End Sub

    Private Sub Bill_ID_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_ID_Txt.KeyPress
        Check_Only_Int(sender, e)
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


    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        If dgvSales.Rows.Count > 0 Then
            Me.Cursor = Cursors.AppStarting
            CashPrint(Sales_BillPage_Bill_Track, Sales_Page_ID)
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Public Sub CashPrint(Sales_BillPage_Bill_Track As String, Sales_Page_ID As Integer)

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & Sales_BillPage_Bill_Track_FAST)
        pp.LoadTables()
        With pp

            Select Case Sales_Page_ID_FAST
                Case 9

                    .rp.SetParameterValue(0, "")
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
                    If Sales_Page_ID_FAST = 2 Or Sales_Page_ID_FAST = 8 Then

                        .rp.SetParameterValue(6, "*" + Barcode + "*")
                        .rp.SetParameterValue(7, SB_ID)
                        .rp.SetParameterValue(8, Pure)
                        .rp.SetParameterValue(9, "")

                    Else

                        .rp.SetParameterValue(6, HANY(Val(Pure), "EGYPT"))
                        .rp.SetParameterValue(7, "*" + Barcode + "*")
                        .rp.SetParameterValue(8, txtNotes.Text)
                        .rp.SetParameterValue(9, "")
                        .rp.SetParameterValue(10, "")
                        .rp.SetParameterValue(11, "0")
                        .rp.SetParameterValue(12, Pure)
                        .rp.SetParameterValue(13, "")

                    End If

            End Select


        End With

        If Sales_Page_ID_FAST <> 2 And Sales_Page_ID_FAST <> 8 And Sales_Page_ID_FAST <> 6 Then
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        Else
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_80))
            pp.rp.PrintOptions.PrinterName = Default_Printer_80
        End If
        pp.rp.PrintToPrinter(1, False, 0, 0)
        pp.rp.Dispose()

        'Dim p As New print
        'p.CrystalReportViewer1.ReportSource = pp.rp
        'p.Show()
        '------------------------------------------------------------------------
    End Sub

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
        'ResetNewBill()

        CurrentDraft = DraftManager.CreateNewDraft(USER_ID)

        BindDraftHeaderToForm()
        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()
        Bill_ID_Txt.Clear()
        Enable_Fields()
    End Sub

    Private Sub LoadDraftToGrid()



        If CurrentDraft Is Nothing Then Exit Sub

        Dim dt As New DataTable()

        dt.Columns.Add("DraftLineId", GetType(String))
        dt.Columns.Add("IM_ID", GetType(Integer))
        dt.Columns.Add("Barcode", GetType(String))
        dt.Columns.Add("ItemName", GetType(String))
        dt.Columns.Add("UnitName", GetType(String))
        dt.Columns.Add("QTY", GetType(Decimal))
        dt.Columns.Add("Price", GetType(Decimal))
        dt.Columns.Add("T_Price", GetType(Decimal))
        dt.Columns.Add("U_ID", GetType(Long))
        dt.Columns.Add("ST_ID", GetType(Long))
        dt.Columns.Add("Cost", GetType(Double))
        dt.Columns.Add("U_Cargo", GetType(Double))
        dt.Columns.Add("ST_QTY", GetType(Decimal))
        dt.Columns.Add("Compons", GetType(String))
        dt.Columns.Add("D_Vaild", GetType(String))

        For Each item As SaleDraftItem In CurrentDraft.Items
            dt.Rows.Add(
            item.DraftLineId,
            item.IM_ID,
            item.Barcode,
            item.ItemName,
             item.UnitName,
            item.QTY,
             item.Price,
            item.T_Price,
            item.U_ID,
            item.ST_ID,
            If(item.Cost.HasValue, item.Cost.Value, 0),
            item.U_Cargo,
            item.ST_QTY,
            item.Compons,
            item.D_Vaild
        )
        Next

        dgvSales.DataSource = dt

        If dgvSales.Columns.Contains("DraftLineId") Then
            dgvSales.Columns("DraftLineId").Visible = False
        End If

        FormatSalesGrid()

        If dgvSales.Rows.Count > 0 Then
            Dim lastRowIndex As Integer
            lastRowIndex = dgvSales.Rows.Count - 1
            dgvSales.CurrentCell = dgvSales.Rows(lastRowIndex).Cells("Item_Name")
        End If


        '-----------------------------------------------------
        'Dim dt As New DataTable()

        'dt.Columns.Add("ItemName")
        'dt.Columns.Add("QTY", GetType(Decimal))
        'dt.Columns.Add("Price", GetType(Decimal))
        'dt.Columns.Add("Total", GetType(Decimal))
        'dt.Columns.Add("DraftLineId")

        'For Each item In CurrentDraft.Items
        '    dt.Rows.Add(
        '    item.ItemName,
        '    item.QTY,
        '    item.Price,
        '    item.T_Price,
        '    item.DraftLineId
        ')
        'Next

        'AGMetroGrid.DataSource = dt

    End Sub

    Private Sub FormatSalesGrid()

        If dgvSales.DataSource Is Nothing Then Exit Sub

        With dgvSales

            ' إخفاء الأعمدة التقنية
            If .Columns.Contains("DraftLineId") Then .Columns("DraftLineId").Visible = False
            If .Columns.Contains("IM_ID") Then .Columns("IM_ID").Visible = False
            If .Columns.Contains("U_ID") Then .Columns("U_ID").Visible = False
            If .Columns.Contains("ST_ID") Then .Columns("ST_ID").Visible = False
            If .Columns.Contains("Date_") Then .Columns("Date_").Visible = False
            If .Columns.Contains("D_Valid_CL") Then .Columns("D_Valid_CL").Visible = False
            If .Columns.Contains("Serial_Code_CL") Then .Columns("Serial_Code_CL").Visible = False
            If .Columns.Contains("Notes_CL") Then .Columns("Notes_CL").Visible = False


            ' أعمدة تريد إخفاءها مؤقتًا
            If .Columns.Contains("Cost") Then .Columns("Cost").Visible = False
            If .Columns.Contains("U_Cargo") Then .Columns("U_Cargo").Visible = False
            If .Columns.Contains("ST_QTY") Then .Columns("ST_QTY").Visible = False
            If .Columns.Contains("D_Vaild") Then .Columns("D_Vaild").Visible = False
            If .Columns.Contains("Compons") Then .Columns("Compons").Visible = False

            ' أعمدة العرض الأساسية
            If .Columns.Contains("ItemName") Then
                .Columns("ItemName").HeaderText = "الصنف"
                .Columns("ItemName").Width = 220
            End If

            If .Columns.Contains("Barcode") Then
                .Columns("Barcode").HeaderText = "الباركود"
                .Columns("Barcode").Width = 120
            End If

            If .Columns.Contains("UnitName") Then
                .Columns("UnitName").HeaderText = "الوحدة"
                .Columns("UnitName").Width = 90
            End If

            If .Columns.Contains("QTY") Then
                .Columns("QTY").HeaderText = "الكمية"
                .Columns("QTY").Width = 80
                .Columns("QTY").DefaultCellStyle.Format = "0.###"
            End If

            If .Columns.Contains("Price") Then
                .Columns("Price").HeaderText = "السعر"
                .Columns("Price").Width = 90
                .Columns("Price").DefaultCellStyle.Format = "0.000"
            End If

            If .Columns.Contains("T_Price") Then
                .Columns("T_Price").HeaderText = "الإجمالي"
                .Columns("T_Price").Width = 100
                .Columns("T_Price").DefaultCellStyle.Format = "0.000"
                .Columns("T_Price").ReadOnly = True
            End If

        End With

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
            'Fill_Bill_Info()
            SelectStateBt()
            Me.Enabled = True
        End If
    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If T_ID > 0 Then
            If On_Update = False Then

                Beep()
                If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟ ", "تعديل فاتورة", MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    Edit_butt.BackColor = Color.GreenYellow
                    On_Update = True
                    dgvSales.BackgroundColor = Color.LightYellow
                    dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow
                    RemoveCatButton.Enabled = True
                    DateTimeEx.Enabled = True
                    DiscountPanel.Enabled = True
                    Ebable_CatFields()
                    Edit_butt.Text = "إيقاف التعديل"
                    'Network_Edit_Tracker_insert("تعديل فاتورة مبيعات (الشاشة السريعة) / رقم آلي : " + Bill_ID_Txt.Text + "  / المدخل :  " + User_Name_lb.Text, Pure_txt.Text, 0, 0)
                End If

            Else
                Save_Date(T_ID, DateTimeEx)
                Save_Total(T_ID, TOTAL, Disc)
                On_Update = False
                Edit_butt.Text = EditState
                Edit_butt.BackColor = Color.White
                DateTimeEx.Enabled = False
                DiscountPanel.Enabled = False
                SelectStateBt()

            End If
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        FormType = 1
        Switch_To_DV_Show()
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

    Private Sub Bill_ID_Txt_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Bill_ID_Txt.MouseDoubleClick
        SearchByBarcode.ShowDialog()
    End Sub

    Private Sub Discount_txt_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Discount_txt.MouseDoubleClick
        Make_Discount()
    End Sub

    Private Sub Make_Discount()

        If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow And dgvSales.Rows.Count > 0 Then
            Dim F As New Fast_SB_Discount
            Identifiers.T_ID = T_ID
            Identifiers.TOTAL = Total_TextBox.Text
            Identifiers.Disc = Disc
            Identifiers.Pure = Pure_txt.Text
            Identifiers.SB_ID = SB_ID

            Fast_SB_Discount.ShowDialog()
        End If

    End Sub

    Private Sub AG_SH_txt_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AG_SH_txt.MouseDoubleClick
        'If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow Then

        Dim f As New AgentsMenu
        f.is_By_Draft = True
        f.ShowDialog()
        If f.is_OK = True Then ChangeDraftCustomer(f.AG_ID, f.AG_NAME)
        'End If
    End Sub

    Private Sub AGMetroGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvSales.KeyDown
        If e.KeyCode = Keys.Delete Then
            If dgvSales.Rows.Count > 0 And dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                If MessageBox.Show(" حذف الصنف " + dgvSales.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    'SB_Contents_Delete_IM(dgvSales.CurrentRow.Cells("T_ID_CL").Value)

                    RemoveCatButton.PerformClick()
                End If
            End If
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
                .ClearFields()
                .Fields_Panel.Enabled = True
                .AG_Cm.Enabled = False
                .Barcode_SH_txt.Enabled = False
                .Receipt_Title_combobox.Text = "فاتورة مبيعات : " + Bill_ID_Txt.Text
                .AG_Cm.Set_IM_By_ID(AG_ID)
                '.IM_SH_txt.Text = AG_SH_txt.Text
                '.AG_ID = AG_ID
                '.GET_AG()
                '.IM_SH_txt.BackColor = Color.LightGoldenrodYellow
                '.Current_QTY.Text = Show_AG_T_Balance(AG_ID)
                '.Fetch_AG_Currency()
                .money_num_txtb.Text = Pure
            End With

            F_Receipt.ShowDialog()

        Else
            MsgBox("يجب إعتماد الفاتورة أولا", MsgBoxStyle.Exclamation, "")
        End If
    End Sub

    Private Sub Notes_txt_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles txtNotes.MouseDoubleClick
        F_BillNotes = New BillNotes
        F_BillNotes.T_ID = T_ID
        F_BillNotes.ShowDialog()

        'Notes_txt.Text = F_BillNotes.Notes_txt.Text
    End Sub


    Private Sub IM_Profet_btn_Click(sender As Object, e As EventArgs) Handles IM_Profet_btn.Click
        Bill_Perfet_Select_For_Bill(T_ID)
    End Sub

    Private Sub IM_Search_btn_Click(sender As Object, e As EventArgs) Handles IM_Search_btn.Click
        'IM_Keyboard.ShowDialog()

        'Items_Search.ShowDialog()
        'If GLOBAL_IM_ID > 0 Then
        '    IM_ID = GLOBAL_IM_ID
        '    Load_IM_By_ID()
        '    'SelectItemById(GLOBAL_IM_ID)
        'End If

        Dim f As Items_Search = Items_Search.GetInstance()

        f.ShowDialog()
        f.BringToFront()
        f.WindowState = FormWindowState.Normal

        If GLOBAL_IM_ID > 0 Then
            IM_ID = GLOBAL_IM_ID
            Load_IM_By_ID()
            'SelectItemById(GLOBAL_IM_ID) 
        End If

    End Sub


    Private Sub CALC_Btn_Click(sender As Object, e As EventArgs) Handles CALC_Btn.Click
        Shell("calc.exe")
    End Sub

    'Private Sub VoidLb_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles VoidLb.MouseDoubleClick

    '    If U_SalesVoid = True Then

    '        Beep()
    '        If MessageBox.Show(" سيتم تراجع عن إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "تاكيد العملية", MessageBoxButtons.OKCancel,
    '               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
    '            AG_Balance_UN_Void_Row(T_ID, SB_ID, 1)
    '            Get_T_ID(SB_ID, "")
    '        End If

    '    End If
    'End Sub


    Private Sub Pure_txt_TextChanged(sender As Object, e As EventArgs) Handles Pure_txt.TextChanged
        If is_Use_Total_Port = True Then Show_Total_Port(Pure)
    End Sub

    Private Sub AGMetroGrid_DataSourceChanged(sender As Object, e As EventArgs) Handles dgvSales.DataSourceChanged
        Calc_Total()
    End Sub


    Private Sub Sales_Bill_Page_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Sales_Bill_Page_cm.SelectedValueChanged
        If TypeName(Sales_Bill_Page_cm.SelectedValue) = "Integer" Then SELECT_Rpt_Path()
    End Sub

    Private Sub SELECT_Rpt_Path()

        Dim c As New C

        Try
            Dim s As String
            s = "select AG_Bill from Sales_Bill_Page  WHERE ID = " & Sales_Bill_Page_cm.SelectedValue
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Sales_BillPage_Bill_Track_FAST = c.Dr("AG_Bill")
                Sales_Page_ID_FAST = Sales_Bill_Page_cm.SelectedValue
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Async Sub Refresh_IM_Btn_Click(sender As Object, e As EventArgs) Handles Refresh_IM_Btn.Click
        Await Load_ALL_IM()
    End Sub

    Private Sub Notes_txt_TextChanged(sender As Object, e As EventArgs) Handles txtNotes.TextChanged
        If CurrentDraft Is Nothing Then Exit Sub

        CurrentDraft.About = txtNotes.Text
        DraftManager.SaveDraft(CurrentDraft)
    End Sub


    Private Sub txtDiscount_Leave(sender As Object, e As EventArgs) Handles Discount_txt.Leave
        If CurrentDraft Is Nothing Then Exit Sub

        Dim disc As Decimal = 0D
        Decimal.TryParse(Discount_txt.Text, disc)

        CurrentDraft.Discount = disc
        DraftCalculator.RecalculateDraft(CurrentDraft)

        DraftManager.SaveDraft(CurrentDraft)
        UpdateDraftTotalsOnScreen()
    End Sub

    Private Sub AG_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles AG_SH_txt.TextChanged
        If CurrentDraft Is Nothing Then Exit Sub

        CurrentDraft.AG_ID = AG_ID
        CurrentDraft.CustomerName = AG_SH_txt.Text

        'AG_ID = customerId.ToString()
        'txtCustomerName.Text = customerName

        DraftManager.SaveDraft(CurrentDraft)
    End Sub


    Private Sub dgvSales_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSales.CellEndEdit

        If CurrentDraft Is Nothing Then Exit Sub
        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = dgvSales.Rows(e.RowIndex)
        Dim draftLineId As String = row.Cells("DraftLineId").Value.ToString()

        Dim item = CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)
        If item Is Nothing Then Exit Sub

        item.QTY = Convert.ToDecimal(row.Cells("QTY").Value)
        item.Price = Convert.ToDecimal(row.Cells("Price").Value)
        item.Compons = If(row.Cells("Compons").Value, "").ToString()
        item.D_Vaild = If(row.Cells("D_Vaild").Value, "").ToString()

        DraftCalculator.RecalculateDraft(CurrentDraft)
        DraftManager.SaveDraft(CurrentDraft)

        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Draft_Btn.Click
        Sales_Drafts_Menu.ShowDialog()
    End Sub

    Private Sub ChangeDraftCustomer(agId As Integer, agName As String)

        EnsureDraftExists()

        CurrentDraft.AG_ID = agId
        CurrentDraft.AG_NAME = agName
        CurrentDraft.UpdatedAt = DateTime.Now

        ' العرض على الشاشة
        AG_ID = agId.ToString()
        AG_SH_txt.Text = agName

        DraftManager.SaveDraft(CurrentDraft)

    End Sub

    Private Sub IMIncreaseButton_Click(sender As Object, e As EventArgs) Handles IMIncreaseButton.Click
        Dim Def As Double = 1
        ChangeQtyByInput(Def, False)
    End Sub

    Private Sub IMDicreaseButton_Click(sender As Object, e As EventArgs) Handles IMDicreaseButton.Click
        Dim Def As Double = -1
        ChangeQtyByInput(Def, False)
    End Sub
End Class
