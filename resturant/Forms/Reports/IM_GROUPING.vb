Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports Microsoft.Win32
Public Class IM_GROUPING
    Dim rs As New Resizer
    Dim IM_DT As New DataTable
    Dim Get_IM As Boolean = False

    Public is_Update_Valid_D As Boolean = False
    Public is_Update_QTY As Boolean = False
    Public is_Update_Unit_Cost As Boolean = False

    Private Sub STORES_Explorer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'F_MainForm.Fill_ALL_IM()
        Me.Dispose()
    End Sub
    Private Sub STORES_Explorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Zuby.ADGV.AdvancedDataGridView.SetTranslations(Zuby.ADGV.AdvancedDataGridView.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))
        Zuby.ADGV.AdvancedDataGridViewSearchToolBar.SetTranslations(Zuby.ADGV.AdvancedDataGridViewSearchToolBar.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))
        '' Me.WindowState = FormWindowState.Maximized
        fetch_ST()
        fetch_GM()
        Get_IM = True
        'Fill_ALL_IM(0)
        Make_Hints()
        ' Recover_STORES_Explorer_File_Setting(CheckedListBox1)
        'Check_View_Control()
        fetch_IM_CAT()
        Load_Data()
        GM_Serach.Select()

    End Sub

    Public Sub Load_Data()
        'Dim C As New C
        'Dim sql As String

        'Try
        '    sql = " select DISTINCT Type_ID,Type_Name from IM_FULL_MV_V Order By Type_Name ASC"
        '    C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
        '    C.Da.Fill(C.Dt)
        '    ReceiptTypeComboBox.DataSource = C.Dt
        '    ReceiptTypeComboBox.DisplayMember = "Type_Name"
        '    ReceiptTypeComboBox.ValueMember = "Type_ID"

        '    If C.Dt.Rows.Count > 0 Then ReceiptTypeComboBox.SelectedIndex = 0
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

    End Sub


    Private Sub Make_Hints()
        'SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن صنف بالباركود")
    End Sub


    Public Sub fetch_IM_CAT()
        IM_CAT_CM.DataSource = GetMailItems_IM_CAT()
        IM_CAT_CM.DisplayMember = "NAME"
        IM_CAT_CM.ValueMember = "ID"
        IM_CAT_CM.SelectedIndex = 0
    End Sub

    Function GetMailItems_IM_CAT() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "------- كل الفئات -------"))

        Dim c1 As New C
        Dim s As String = "select ID,NAME from IM_CAT ORDER By ID ASC"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("NAME")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

    End Function


    Public Sub fetch_GM()
        GM_Serach.DataSource = GetMailItems_GM()
        GM_Serach.DisplayMember = "name"
        GM_Serach.ValueMember = "ID"
        GM_Serach.SelectedIndex = 0
    End Sub

    Function GetMailItems_GM() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "------- كل التصنيفات -------"))

        Dim c1 As New C
        Dim s As String = "select GM_ID AS ID,GM_NAME AS name from General_menu ORDER By GM_ID ASC"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

    End Function

    Public Sub fetch_ST()
        ST_cm.DataSource = GetMailItems()
        ST_cm.DisplayMember = "name"
        ST_cm.ValueMember = "ID"
        ST_cm.SelectedValue = SB_ST_ID
    End Sub

    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "------- كل المخازن -------"))

        Dim c1 As New C
        Dim s As String = "select ST_ID AS ID,ST_name AS name from STORES ORDER By ST_ID ASC"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

    End Function

    Public Sub Fill_ALL_IM()


        DataB.Dispose()
        DataB = New BindingSource
        gridv.DataSource = Nothing
        IM_DT = New DataTable


        'Dim Main_Query As String
        'Dim C As New C

        'Main_Query = " select '' AS ' ت ' ,IM_CAT AS ' الفئــة ',GM_ANME AS ' المجموعة ',St_Name AS ' المخزن ',Type_Name AS ' المعاملة ',Ag_name AS ' الحساب ',Date AS ' التاريخ ',Bill_ID AS ' رقم المعاملة ',BARCODE AS ' باركود ',IM_NAME AS ' إسم الصنف ',U_NAME AS ' الوحدة ',U_Cargo AS ' التعادل ',D_Vaild AS ' الصلاحية ',Q_OUT AS ' الوارد ',Q_IN AS ' الصادر ',Price AS ' السعر ',UserName AS ' المدخل ' from [IM_FULL_MV_V]  "

        'Main_Query = Main_Query & " WHERE 1 = 1 "

        'If ST_cm.SelectedIndex > 0 Then Main_Query = Main_Query & " AND ST_ID = " & ST_cm.SelectedValue

        'If GM_Serach.SelectedIndex > 0 Then Main_Query = Main_Query & " AND GM_ID = " & GM_Serach.SelectedValue

        'If IM_CAT_CM.SelectedIndex > 0 Then Main_Query = Main_Query & " AND IM_CAT_ID = " & IM_CAT_CM.SelectedValue



        'Main_Query = Main_Query & " and CONVERT(Date,Date) BETWEEN '" & DateTimePicker_From.Text & "' AND '" & DateTimePicker_To.Text & "' "


        'Main_Query = Main_Query & " ORDER BY DATE ASC "



        'C.Da = New SqlClient.SqlDataAdapter(Main_Query, C.Con)
        'C.Da.SelectCommand.CommandTimeout = 120
        'C.Da.Fill(IM_DT)
        'DataB.DataSource = IM_DT
        'gridv.DataSource = DataB

        'advancedDataGridViewSearchToolBar_main.SetColumns(gridv.Columns)



        'If IM_DT.Rows.Count > 0 Then

        '    gridv.Columns(13).Tag = 1
        '    gridv.Columns(14).Tag = 1
        '    gridv.Columns(15).Tag = 1

        '    gridv.Columns(15).DefaultCellStyle.Format = "N3"

        '    Index_GV()

        '    If CheckedListBox1.Items.Count = 0 Then
        '        CheckedListBox1.Items.Clear()
        '        For i As Integer = 0 To gridv.ColumnCount - 1
        '            Dim CL = gridv.Columns(i).Name
        '            CheckedListBox1.Items.Add(CL)
        '        Next
        '    End If

        '    If U_SB_Show_IM_COST = False Then
        '        gridv.Columns(11).Visible = False
        '        CheckedListBox1.SetItemCheckState(11, False)
        '    End If

        'End If

        'Get_Total_QYT()


        IM_GROUPING_BY_IM_CAT()

    End Sub

    Private Sub IM_GROUPING_BY_IM_CAT()
        Me.Cursor = Cursors.AppStarting
        Dim c As New C
        With c.Com
            .Connection = c.Con



            If By_IM_CAT_RD.Checked = True Then
                .CommandText = "[IM_GROUPING_BY_IM_CAT]"
            End If


            If By_GM_RD.Checked = True Then
                .CommandText = "[IM_GROUPING_BY_GM]"
            End If


            If By_IM_RD.Checked = True Then
                .CommandText = "[IM_GROUPING_BY_IM]"
            End If



            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
        End With
        c.Da = New SqlDataAdapter(c.Com)
        c.Da.SelectCommand.CommandTimeout = 120
        c.Da.Fill(IM_DT)
        DataB.DataSource = IM_DT
        gridv.DataSource = DataB



        If IM_DT.Rows.Count > 0 Then

            gridv.Columns(1).Tag = 1
            gridv.Columns(2).Tag = 1
            gridv.Columns(3).Tag = 1

            gridv.Columns(4).Tag = 1
            gridv.Columns(5).Tag = 1
            gridv.Columns(6).Tag = 1

            gridv.Columns(7).Tag = 1
            gridv.Columns(8).Tag = 1
            gridv.Columns(9).Tag = 1

            gridv.Columns(10).Tag = 1
            gridv.Columns(11).Tag = 1
            gridv.Columns(12).Tag = 1
            gridv.Columns(13).Tag = 1


            gridv.Columns(1).DefaultCellStyle.Format = "N3"
            '  gridv.Columns(2).DefaultCellStyle.Format = "N3"
            gridv.Columns(3).DefaultCellStyle.Format = "N3"

            ' gridv.Columns(4).DefaultCellStyle.Format = "N3"
            gridv.Columns(5).DefaultCellStyle.Format = "N3"
            ' gridv.Columns(6).DefaultCellStyle.Format = "N3"

            gridv.Columns(7).DefaultCellStyle.Format = "N3"
            ' gridv.Columns(8).DefaultCellStyle.Format = "N3"
            gridv.Columns(9).DefaultCellStyle.Format = "N3"

            gridv.Columns(10).DefaultCellStyle.Format = "N3"
            'gridv.Columns(11).DefaultCellStyle.Format = "N3"
            ' gridv.Columns(12).DefaultCellStyle.Format = "N3"
            gridv.Columns(13).DefaultCellStyle.Format = "N3"


            Index_GV()

            If CheckedListBox1.Items.Count = 0 Then
                CheckedListBox1.Items.Clear()
                For i As Integer = 0 To gridv.ColumnCount - 1
                    Dim CL = gridv.Columns(i).Name
                    CheckedListBox1.Items.Add(CL)
                Next
            End If



        End If

        Me.Cursor = Cursors.Default

    End Sub


    Private Sub Index_GV()
        'Dim Cost As Double = 0
        'Dim Qty_IN As Double = 0
        'Dim Qty_OUT As Double = 0

        'For i = 0 To gridv.Rows.Count - 1
        '    gridv.Rows(i).Cells(0).Value = i + 1
        '    Qty_OUT += gridv.Rows(i).Cells(13).Value
        '    Qty_IN += gridv.Rows(i).Cells(14).Value
        '    Cost += gridv.Rows(i).Cells(15).Value
        'Next
        'textBox_total.Text = gridv.Rows.Count.ToString

        'T_OUT_Q_txt.Text = Qty_OUT.ToString("N")
        'T_IN_Q_txt.Text = Qty_IN.ToString("N")
        'TotalCost_txt.Text = Cost.ToString("N")
    End Sub
    Private Sub STORES_Explorer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    'Private Sub CMSearchTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If e.KeyChar = "'" Then e.Handled = True
    'End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs)
        'If BarcodeSearch_CB.Checked = False And IMNUM_CB.Checked = False Then
        '    Dim Dv As DataView
        '    Dv = IM_DT.AsDataView
        '    Dv.RowFilter = "item_name LIKE '%" + sender.Text + "%'"
        '    DataGridViewX.DataSource = Dv
        '    Get_Total_QYT()
        'End If
    End Sub


    Public Sub Get_Total_QYT()

        'Dim Cost As Double = 0
        'Dim Qty_IN As Double = 0
        'Dim Qty_OUT As Double = 0

        'For i = 0 To gridv.Rows.Count - 1
        '    Qty_OUT += gridv.Rows(i).Cells(13).Value
        '    Qty_IN += gridv.Rows(i).Cells(14).Value
        '    Cost += gridv.Rows(i).Cells(15).Value
        'Next

        'T_OUT_Q_txt.Text = Qty_OUT.ToString("N")
        'T_IN_Q_txt.Text = Qty_IN.ToString("N")
        'TotalCost_txt.Text = Cost.ToString("N")


    End Sub



    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click


        'If U_SB_Show_IM_COST = False Then
        '    gridv.Columns(11).Visible = False
        '    CheckedListBox1.SetItemCheckState(11, False)
        'End If

        Dim F As New Print_PDF
        F.PRINT_PDF_List(gridv, CheckedListBox1, " تقريــر الإحصائيـــات من " & HOME.DateRange_Flate.D_F.Value & " إلــى " & HOME.DateRange_Flate.D_T.Value, 1)

    End Sub

    Private Sub MakeBarcode_btn_Click(sender As Object, e As EventArgs)
        printbarcode.ShowDialog()
    End Sub

    'Private Sub IM_btn_Click(sender As Object, e As EventArgs)
    '    Me.Cursor = Cursors.AppStarting
    '    F_ItemsMenu = New ItemsMenu
    '    F_ItemsMenu.ShowDialog()
    '    Me.Cursor = Cursors.Default
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '  Fetch_QTY()
        Fill_ALL_IM()
    End Sub

    Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
        'If Get_IM = True Then Fill_ALL_IM(0)
    End Sub

    'Private Sub BarcodeSearch_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    CMSearchTextBox.Select()
    '    If BarcodeSearch_CB.Checked = True Then IMNUM_CB.Checked = False
    'End Sub

    'Private Sub CMSearchTextBox_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then Search_By_Barcode() 'And (BarcodeSearch_CB.Checked = True Or IMNUM_CB.Checked = True)
    '    ' If e.KeyCode = Keys.Delete Then CMSearchTextBox.Clear()
    'End Sub

    'Public Sub Search_By_Barcode()

    '    Dim IM_ID As Integer
    '    Dim c As New C
    '    IM_DT.Clear()
    '    Try
    '        Dim s As String = ""

    '        'If IMNUM_CB.Checked Then s = "select IM_ID from IM_Menu WHERE IM_NUM = '" & CMSearchTextBox.Text & "'"
    '        'If BarcodeSearch_CB.Checked Then s = "select IM_ID from IM_units_Search_V WHERE Barcode = '" & CMSearchTextBox.Text & "'"

    '        '  s = "select IM_ID from IM_units_Search_V WHERE Barcode = '" & CMSearchTextBox.Text & "'"

    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()

    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            IM_ID = c.Dr("IM_ID")
    '            Fill_ALL_IM(IM_ID)
    '        Else
    '            MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub



    'Private Sub Up_Update_btn_Click(sender As Object, e As EventArgs) Handles Up_Update_btn.Click
    '    If U_Update_IM_QTY = True Then
    '        If SHhow_Zero_Qty_btn.BackColor = Color.WhiteSmoke Then
    '            If DataGridViewX.Rows.Count > 0 Then
    '                is_Update_Valid_D = False
    '                is_Update_QTY = True
    '                is_Update_Unit_Cost = False
    '                IM_Update_Qty.ShowDialog()
    '            End If
    '        Else
    '            If DataGridViewX.Rows.Count > 0 Then
    '                MsgBox(" يمكنك تعديل بيانات الصنف الذي كميته 0 عن طريق فاتورة جرد ", MsgBoxStyle.Information)
    '            End If
    '        End If
    '    End If
    'End Sub


    Private Sub GM_Serach_SelectedValueChanged(sender As Object, e As EventArgs) Handles GM_Serach.SelectedValueChanged
        '   Fetch_QTY()
        'If Get_IM = True Then Fill_ALL_IM(0)
    End Sub


    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    'Private Sub DataGridViewX_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles gridv.MouseDoubleClick
    '    If U_Update_IM_QTY = True Then
    '        If gridv.Rows.Count > 0 Then
    '            If SHhow_Zero_Qty_btn.BackColor = Color.WhiteSmoke Then
    '                If gridv.Rows.Count > 0 Then
    '                    is_Update_Valid_D = False
    '                    is_Update_QTY = True
    '                    is_Update_Unit_Cost = False
    '                    IM_Update_Qty.ShowDialog()
    '                End If
    '            Else
    '                If gridv.Rows.Count > 0 Then
    '                    MsgBox(" يمكنك تعديل بيانات الصنف الذي كميته 0 عن طريق فاتورة جرد ", MsgBoxStyle.Information)
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs)
        Dim F As New DGV_Control
        F.TabControl1.TabPages.Remove(F.OtherTabPage)
        F.TabControl1.TabPages.Remove(F.ItemPriceTabPage)
        F.Show()
    End Sub

    Private Sub Recount_Qty_btn_Click(sender As Object, e As EventArgs)
        If ST_cm.SelectedValue = 0 Then
            MsgBox("حدد المخزن المراد تدويره", MsgBoxStyle.Information, "")
            ST_cm.DroppedDown = True
            ST_cm.Select()
        Else
            If MessageBox.Show(" تدوير كميات الأصناف ؟؟ ... قد تستغرق العملية بعض الوقت ", "تأكيد", MessageBoxButtons.OKCancel, _
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then RECOUNT_IM_ALL_QTY()
        End If

    End Sub

    Public Sub RECOUNT_IM_ALL_QTY()
        'Me.Cursor = Cursors.AppStarting
        'Dim c As New C
        'With c.Com
        '    .Connection = c.Con
        '    .CommandText = "RECOUNT_IM_ALL_QTY"
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
        'End With

        'If SQL_SP_EXEC(c.Com) = True Then
        '    Network_Edit_Tracker_insert(" تدوير كميات الأصناف بمخزن : " & ST_cm.Text & "  من شاشة كشف المخازن ", 0, 0, 0)
        '    Me.Cursor = Cursors.Default
        '    Fill_ALL_IM(0)
        '    Make_Hints()
        '    MsgBox("تم التدوير", MsgBoxStyle.Information, "")
        '    Network_Edit_Tracker_insert(" تدوير كمية المخزن:" & ST_cm.Text, 0, 23, 3)
        'End If

    End Sub

    'Private Sub Recount_Cost_btn_Click(sender As Object, e As EventArgs)

    '    If MessageBox.Show(" تدوير متوسط تكلفةالأصناف ؟؟ ... قد تستغرق العملية بعض الوقت ", "تأكيد", MessageBoxButtons.OKCancel, _
    '                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then RECOUNT_IM_ALL_COST()

    'End Sub

    'Public Sub RECOUNT_IM_ALL_COST()
    '    Me.Cursor = Cursors.AppStarting
    '    Dim c As New C
    '    With c.Com
    '        .Connection = c.Con
    '        .CommandText = "[RECOUNT_IM_ALL_MainCost]"
    '        .CommandType = CommandType.StoredProcedure
    '    End With

    '    If SQL_SP_EXEC(c.Com) = True Then
    '        Network_Edit_Tracker_insert(" تدوير متوسط تكلفة الأصناف من شاشة كشف المخازن   : ", 0, 0, 0)
    '        Me.Cursor = Cursors.Default
    '        Fill_ALL_IM(0)
    '        Make_Hints()
    '        MsgBox("تم التدوير", MsgBoxStyle.Information, "")
    '        Network_Edit_Tracker_insert("تدوير تكلفة الخازن", 0, 23, 3)

    '    End If

    'End Sub

    Private Sub gridv_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles gridv.FilterStringChanged
        DataB.Filter = gridv.FilterString
        Index_GV()
        'Get_Total_QYT()
    End Sub

    Private Sub gridv_SortStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.SortEventArgs) Handles gridv.SortStringChanged
        DataB.Sort = gridv.SortString
        Index_GV()
        'Get_Total_QYT()
    End Sub

    Private Sub EXCEL_BTN_Click(sender As Object, e As EventArgs) Handles EXCEL_BTN.Click
        EXCEL_EXPORT(gridv)
    End Sub

    Private Sub EXCEL_EXPORT(gridv As DataGridView)
        Try
            Const stXL_SUBKEY As String = "\Excel.Application\CurVer"
            Dim rkVersionKey As Microsoft.Win32.RegistryKey = Nothing
            rkVersionKey = Registry.ClassesRoot.OpenSubKey(name:=stXL_SUBKEY, writable:=False)
            If rkVersionKey Is Nothing Then
                'not installed
                MessageBox.Show("الرجاء تثبيت حزمة Microsoft Office أولا", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If gridv.RowCount > 0 Then
                    Dim xlApp As Excel.Application
                    Dim xlWorkBook As Excel.Workbook
                    Dim xlWorkSheet As Excel.Worksheet
                    Dim misValue As Object = System.Reflection.Missing.Value
                    Dim i As Integer
                    Dim j As Integer
                    ' xlApp = New Excel.Application
                    xlApp = CreateObject("Excel.Application")
                    xlWorkBook = xlApp.Workbooks.Add(misValue)
                    xlWorkSheet = xlWorkBook.Sheets(1)
                    For Each col As DataGridViewColumn In gridv.Columns
                        If col.Visible Then
                            xlWorkSheet.Cells(1, col.Index + 2) = col.HeaderText.ToString
                        End If
                    Next
                    For i = 0 To gridv.Rows.Count - 1
                        For j = 0 To gridv.ColumnCount - 1
                            If gridv.Columns(j).Visible Then
                                xlWorkSheet.Cells(i + 2, j + 2) = gridv(j, i).Value.ToString()
                            End If
                        Next
                    Next
                    xlApp.Visible = True
                    xlWorkBook.Activate()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            'Class1.con.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        'If CheckedListBox1.Items.Count > 0 Then
        '    ExportButton_STORES_Explorer_Setting_ToFile(CheckedListBox1)
        'End If

    End Sub


    Private Sub advancedDataGridViewSearchToolBar_main_Search(sender As Object, e As Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs) Handles advancedDataGridViewSearchToolBar_main.Search
        Dim restartsearch = True
        Dim startColumn = 0
        Dim startRow = 0
        If Not e.FromBegin Then
            Dim endcol As Boolean = gridv.CurrentCell.ColumnIndex + 1 >= gridv.ColumnCount
            Dim endrow As Boolean = gridv.CurrentCell.RowIndex + 1 >= gridv.RowCount

            If endcol AndAlso endrow Then
                startColumn = gridv.CurrentCell.ColumnIndex
                startRow = gridv.CurrentCell.RowIndex
            Else
                startColumn = If(endcol, 0, gridv.CurrentCell.ColumnIndex + 1)
                startRow = gridv.CurrentCell.RowIndex + If(endcol, 1, 0)
            End If
        End If
        Dim c As DataGridViewCell = gridv.FindCell(e.ValueToSearch, If(e.ColumnToSearch IsNot Nothing, e.ColumnToSearch.Name, Nothing), startRow, startColumn, e.WholeWord, e.CaseSensitive)
        If c Is Nothing AndAlso restartsearch Then c = gridv.FindCell(e.ValueToSearch, If(e.ColumnToSearch IsNot Nothing, e.ColumnToSearch.Name, Nothing), 0, 0, e.WholeWord, e.CaseSensitive)
        If c IsNot Nothing Then gridv.CurrentCell = c
    End Sub



    'Private Sub AllRecieptsCheckBox_CheckedChanged(sender As Object, e As EventArgs)
    '    If AllRecieptsCheckBox.Checked = True Then
    '        ReceiptTypeComboBox.Enabled = False
    '        ReceiptTypeComboBox.SelectedIndex = -1
    '    Else
    '        ReceiptTypeComboBox.Enabled = True
    '        ReceiptTypeComboBox.SelectedIndex = 0
    '    End If
    'End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then


            If CheckedListBox1.Items.Count > 0 Then

                For i As Integer = 0 To CheckedListBox1.Items.Count - 1
                    CheckedListBox1.SetItemChecked(i, True)
                    'Dim CL = gridv.Columns(i).Name
                    'CheckedListBox1.Items.Add(CL)
                Next
            End If

        End If
    End Sub
End Class