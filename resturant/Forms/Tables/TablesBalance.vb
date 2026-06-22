Imports System.IO.Ports

Public Class TablesBalance
    Dim rs As New Resizer
    Private ReadOnly FloorRepository As New RestaurantFloorLayoutRepository()
    Private FloorCanvas As RestaurantFloorDesignerControl
    Public tran_F, tran_T
    Public Bill_T_ID, AG_ID, TB_Num As Integer
    Public Pied_Money As Double
    Public IS_SHOW_NUMBER As Boolean = False
    Public TB_ID As Integer
    Private ReadOnly BillsGridCellFont As New Font("Segoe UI Semibold", 8.0!, FontStyle.Bold)
    Private ReadOnly BillsGridHeaderFont As New Font("Segoe UI", 8.0!, FontStyle.Regular)
    Private TableBillTotal As Decimal = 0D
    Private TableBillDiscount As Decimal = 0D
    Private TableBillPure As Decimal = 0D

    'Dim RightPanelWidth As Integer
    'Public IsPanelHidden As Boolean = False

    Private Sub Refrech_btn_Click(sender As Object, e As EventArgs) Handles Refrech_btn.Click
        '   F_TablesMenu = New TablesMenu
        '    Set_Form(F_TablesMenu, F_Panel)
        loadtables()
        ' TablePiedApart.TB_Num = TB_Num
        LoadTableBalanceData(TB_Num)
        Me.TB_Info.Text = " الطاولة : " + TB_Num.ToString
        Me.Items_btn_Click(sender, e)
    End Sub

    Public Sub loadtables()
        If FloorCanvas IsNot Nothing Then
            RemoveHandler FloorCanvas.ElementSelected, AddressOf FloorCanvas_ElementSelected
            FloorCanvas.Dispose()
            FloorCanvas = Nothing
        End If

        F_Panel.Controls.Clear()

        If MY_Settings.TableDisplayMode = 0 Then
            LoadTablesTraditional()
            Return
        End If

        Try
            Dim flateId As Integer = GetActiveTablesFlateID()
            Dim tablesDt As DataTable = FloorRepository.LoadTables(flateId)
            Dim elements As List(Of RestaurantFloorElement) = FloorRepository.LoadLayout(flateId)

            MergeTablesWithLayout(elements, tablesDt, flateId)
            ShowTablesLayout(elements)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadTablesTraditional()
        Try
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim counter As Integer = 1
            Dim tablesDt As DataTable = FloorRepository.LoadTables(GetActiveTablesFlateID())

            For Each row As DataRow In tablesDt.Rows
                Dim IMbtn As New Button
                IMbtn.Name = ("T_Name" + row("TB_ID").ToString())
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(F_Panel.Size.Width / 6.2, F_Panel.Size.Height / 5.25)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMbtn.Font = New System.Drawing.Font("Segoe UI", (h + w) / 125, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                IMbtn.Text = row("T_Name").ToString()
                IMbtn.Tag = row("TB_ID")
                IMbtn.BackColor = If(GetBool(row("isbusy")), Color.IndianRed, Color.WhiteSmoke)
                Controls.Add(IMbtn)
                IMbtn.Parent = F_Panel
                AddHandler IMbtn.Click, AddressOf bt_Click

                If counter = 6 Then
                    counter = 1
                    x = 0
                    y += F_Panel.Size.Height / 5.25
                Else
                    counter += 1
                    x += F_Panel.Size.Width / 6.2
                End If

                rs.Find_One(IMbtn)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetActiveTablesFlateID() As Integer
        If U_Flate_ID > 0 Then Return U_Flate_ID
        If MY_Settings.Tables_Flate_ID > 0 Then Return MY_Settings.Tables_Flate_ID
        Return 0
    End Function

    Public Sub bt_Click(ByVal sender As Object, ByVal e As EventArgs)
        SelectTable(Convert.ToInt32(sender.tag), sender.Text)
    End Sub

    Private Sub FloorCanvas_ElementSelected(element As RestaurantFloorElement)
        If element Is Nothing OrElse element.TB_ID.HasValue = False Then Return
        SelectTable(element.TB_ID.Value, If(String.IsNullOrWhiteSpace(element.ElementText), element.TB_ID.Value.ToString(), element.ElementText))
    End Sub

    Private Sub SelectTable(tbId As Integer, tableText As String)
        TB_Num = tbId

        If Me.TB_Types_CMB.SelectedIndex <> 0 Then

            If Me.tran_F = 0 Then
                Me.tran_F = tbId
                Me.TB_F_txt.Text = tableText
                Exit Sub
            End If

            If Me.tran_T = 0 Then
                Me.tran_T = tbId
                Me.TB_T_txt.Text = tableText
            End If

        Else
            ' TablePiedApart.TB_Num = TB_Num
            LoadTableBalanceData(TB_Num)
            Me.TB_Info.Text = " الطاولة : " + tableText
            Me.Items_btn_Click(Items_btn, EventArgs.Empty)


        End If
    End Sub

    Private Sub MergeTablesWithLayout(elements As List(Of RestaurantFloorElement), tablesDt As DataTable, flateId As Integer)
        Dim representedTables As New List(Of Integer)()

        For Each element As RestaurantFloorElement In elements
            If element.TB_ID.HasValue Then representedTables.Add(element.TB_ID.Value)
        Next

        Dim index As Integer = representedTables.Count
        For Each row As DataRow In tablesDt.Rows
            Dim tbId As Integer = Convert.ToInt32(row("TB_ID"))
            If representedTables.Contains(tbId) Then
                ApplyTableState(elements, row)
                Continue For
            End If

            Dim point As Point = GetDefaultTablePoint(index)
            Dim element As New RestaurantFloorElement()
            element.Flate_ID = flateId
            element.TB_ID = tbId
            element.ElementType = "Table"
            element.ElementText = row("T_Name").ToString()
            element.X_Pos = point.X
            element.Y_Pos = point.Y
            element.WidthValue = 115
            element.HeightValue = 80
            element.SeatsCount = 4
            element.BackColorArgb = Color.WhiteSmoke.ToArgb()
            element.ForeColorArgb = Color.FromArgb(15, 23, 42).ToArgb()
            element.IsBusy = GetBool(row("isbusy"))
            element.IsCash = GetBool(row("is_Cash"))
            element.ZIndex = index
            elements.Add(element)
            index += 1
        Next
    End Sub

    Private Sub ApplyTableState(elements As List(Of RestaurantFloorElement), row As DataRow)
        Dim tbId As Integer = Convert.ToInt32(row("TB_ID"))

        For Each element As RestaurantFloorElement In elements
            If element.TB_ID.HasValue AndAlso element.TB_ID.Value = tbId Then
                element.ElementText = row("T_Name").ToString()
                element.IsBusy = GetBool(row("isbusy"))
                element.IsCash = GetBool(row("is_Cash"))
                Exit For
            End If
        Next
    End Sub

    Private Sub ShowTablesLayout(elements As List(Of RestaurantFloorElement))
        FloorCanvas = New RestaurantFloorDesignerControl()
        FloorCanvas.IsDesignMode = False
        FloorCanvas.ShowGrid = False
        FloorCanvas.Location = New Point(0, 0)
        FloorCanvas.Size = GetFloorCanvasSize(elements)
        FloorCanvas.Elements = elements
        AddHandler FloorCanvas.ElementSelected, AddressOf FloorCanvas_ElementSelected

        F_Panel.AutoScroll = True
        F_Panel.Controls.Add(FloorCanvas)
    End Sub

    Private Function GetFloorCanvasSize(elements As List(Of RestaurantFloorElement)) As Size
        Dim maxRight As Integer = F_Panel.ClientSize.Width
        Dim maxBottom As Integer = F_Panel.ClientSize.Height

        For Each element As RestaurantFloorElement In elements
            maxRight = Math.Max(maxRight, element.X_Pos + element.WidthValue + 80)
            maxBottom = Math.Max(maxBottom, element.Y_Pos + element.HeightValue + 80)
        Next

        Return New Size(Math.Max(1, maxRight), Math.Max(1, maxBottom))
    End Function

    Private Function GetDefaultTablePoint(index As Integer) As Point
        Dim columns As Integer = Math.Max(1, CInt(Math.Floor((F_Panel.ClientSize.Width - 30) / 145.0R)))
        Dim x As Integer = 20 + ((index Mod columns) * 145)
        Dim y As Integer = 20 + ((index \ columns) * 120)
        Return New Point(x, y)
    End Function

    Private Function GetBool(value As Object) As Boolean
        If value Is Nothing OrElse value Is DBNull.Value Then Return False

        Dim boolValue As Boolean
        Dim text As String = value.ToString().Trim()
        If text = "" Then Return False
        If Boolean.TryParse(text, boolValue) Then Return boolValue

        Dim numberValue As Decimal
        If Decimal.TryParse(text, numberValue) Then Return numberValue <> 0D

        Return False
    End Function




    Public Sub LoadTableBalanceData(TB_ID As Integer)
        Try
            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
                cn.Open()
                FillTableItems(cn, TB_ID)
                FillTableBills(cn, TB_ID)
                LoadTableApartCount(cn, TB_ID)
                LoadTableTimeInfo(cn, TB_ID)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub SB_Contents_SELECT_TB(TB_ID As Integer)
        Try
            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
                cn.Open()
                FillTableItems(cn, TB_ID)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub TB_NotPied_V_SELECT_Bill(TB_ID As Integer)
        Try
            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
                cn.Open()
                FillTableBills(cn, TB_ID)
                LoadTableApartCount(cn, TB_ID)
                LoadTableTimeInfo(cn, TB_ID)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillTableItems(cn As SqlClient.SqlConnection, TB_ID As Integer)
        Dim dt As New DataTable
        Using cmd As New SqlClient.SqlCommand("TB_NotPied_V_SELECT_TB", cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = TB_ID
            Using da As New SqlClient.SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using
        Me.IMGrid.DataSource = dt
        UpdateTableItemsSummary()
    End Sub

    Private Sub FillTableBills(cn As SqlClient.SqlConnection, TB_ID As Integer)
        Dim dt As New DataTable
        Using cmd As New SqlClient.SqlCommand("TB_NotPied_V_SELECT_Bill", cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = TB_ID
            Using da As New SqlClient.SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using
        Me.BillsMetroGrid.DataSource = dt
        ApplyBillsGridCompactStyle()
    End Sub

    Private Sub LoadTableApartCount(cn As SqlClient.SqlConnection, TB_ID As Integer)
        Dim s As String = "SELECT CONVERT(INT,ISNULL(SUM(QTY),0)) AS C FROM TABLES_PREV_APARTS_V WHERE TB_ORDER_CODE = (SELECT TB_ORDER_CODE FROM TABLES WHERE TB_ID = @TB_ID)"
        Using cmd As New SqlClient.SqlCommand(s, cn)
            cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = TB_ID
            Dim result As Object = cmd.ExecuteScalar()
            If result Is Nothing OrElse result Is DBNull.Value Then
                Apart_List_btn.Text = "(0)"
            Else
                Apart_List_btn.Text = "(" & Convert.ToInt32(result).ToString & ")"
            End If
        End Using
    End Sub

    Private Sub LoadTableTimeInfo(cn As SqlClient.SqlConnection, TB_ID As Integer)
        Time_Table_Label.Text = ""
        Dim s As String = "SELECT TOP 1 ISNULL(Start_Open,'') AS Start_Open,ISNULL([dbo].[get_TimeDuration](Start_Open),'') AS Time_Duration FROM Tables_Bills_NotPied_V WHERE Table_ID = @TB_ID"
        Using cmd As New SqlClient.SqlCommand(s, cn)
            cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = TB_ID
            Using dr As SqlClient.SqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    Time_Table_Label.Text = " بداية الدخول:- " & dr("Start_Open").ToString & vbNewLine & " الفترة:- " & dr("Time_Duration").ToString
                End If
            End Using
        End Using
    End Sub

    Private Sub UpdateTableItemsSummary()
        Dim qty As Decimal = 0D
        For Each row As DataGridViewRow In Me.IMGrid.Rows
            If row.IsNewRow Then Continue For
            qty += GetTableDecimalValue(row.Cells("QTY_CL").Value)
        Next
        TB_Info_LB.Text = " عدد المواد : " & IMGrid.RowCount.ToString & "  /    عدد الكميات :  " & FormatTableNumber(qty)
    End Sub

    Private Sub TablesBalance_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Home_Panel = "POS"
        Me.Dispose()
    End Sub

    Private Sub TablesBalance_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If SaveButton.Enabled = True Then SaveButton_Click(sender, e)
    End Sub

    Private Sub TablesBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Home_Panel = ""

        'RightPanelWidth = PanelRight.Width

        If IS_SHOW_NUMBER = False Then
            Me.BeginInvoke(New MethodInvoker(Sub() loadtables()))
        Else
            LoadTableBalanceData(TB_ID)
            TB_Info.Text = " الطاولة : " + TB_ID.ToString
            Items_btn.PerformClick()
        End If



        BillsMetroGrid.Columns("Edit_TB_IMQty_CL").Visible = U_SB_Update
        Tables_Option_GB.Visible = U_Transfer_Table
        TB_Transfer_Panel.Visible = U_Transfer_Table

        SaveButton.Visible = U_End_Table
        PiedApart_btn.Visible = U_End_Table
        Debit_Table_btn.Visible = U_End_Table

        TB_Types_CMB.SelectedIndex = 0
        isPrintBeforeEndBill_CB.Checked = Print_TB_Before_End

        Show_AllBill_Clmns_CB.Checked = Show_AllBill_Clmns
        show_bill_tb_columns()
        ApplyBillsGridCompactStyle()

        'If IsPanelHidden = False Then

        '    ' اخفاء الجزء
        '    PanelRight.Visible = False

        '    ' تصغير عرض الفورم
        '    Me.Width = Me.Width - RightPanelWidth

        '    IsPanelHidden = True

        'End If


    End Sub


    'Public Sub ToggleRightPanel()

    '    If PanelRight.Visible = True Then
    '        PanelRight.Visible = False
    '    Else
    '        PanelRight.Visible = True
    '    End If

    'End Sub

    Private Sub TablesBalance_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls_POS(Me)
        ApplyBillsGridCompactStyle()
    End Sub

    Private Sub ApplyBillsGridCompactStyle()
        If BillsMetroGrid Is Nothing Then Return

        BillsMetroGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        BillsMetroGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        BillsMetroGrid.ColumnHeadersHeight = 28
        BillsMetroGrid.RowTemplate.Height = 30

        BillsMetroGrid.Font = BillsGridCellFont
        BillsMetroGrid.DefaultCellStyle.Font = BillsGridCellFont
        BillsMetroGrid.RowsDefaultCellStyle.Font = BillsGridCellFont
        BillsMetroGrid.ColumnHeadersDefaultCellStyle.Font = BillsGridHeaderFont
        BillsMetroGrid.RowHeadersDefaultCellStyle.Font = BillsGridHeaderFont
        BillsMetroGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.False
        BillsMetroGrid.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.False

        For Each col As DataGridViewColumn In BillsMetroGrid.Columns
            col.DefaultCellStyle.Font = BillsGridCellFont
        Next

        For Each row As DataGridViewRow In BillsMetroGrid.Rows
            row.Height = 30
        Next
    End Sub

    'Private Sub MetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles BillsMetroGrid.RowsAdded
    '    Calc_Bill()
    'End Sub

    'Private Sub MetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles BillsMetroGrid.RowsRemoved
    '    Calc_Bill()
    'End Sub

    Public Sub Calc_Bill()
        CalculateTableBillTotals(True)
    End Sub

    Private Sub CalculateTableBillTotals(Optional ShowTotalPortValue As Boolean = True)
        Dim pure As Decimal = 0D
        Dim discount As Decimal = 0D
        Dim total As Decimal = 0D

        For i = 0 To Me.BillsMetroGrid.Rows.Count - 1
            total += GetTableDecimalValue(BillsMetroGrid.Rows(i).Cells("Total_TB_CL").Value)
            discount += GetTableDecimalValue(BillsMetroGrid.Rows(i).Cells("Discount_CL").Value)
            pure += GetTableDecimalValue(BillsMetroGrid.Rows(i).Cells("Pure_CL").Value)
        Next
        TableBillTotal = total
        TableBillDiscount = discount
        TableBillPure = pure
        Total_Label.Text = " الإجمالي: " & FormatTableNumber(total) & " \ التخفيض: " & FormatTableNumber(discount)
        Me.PureTextBox.Text = FormatTableNumber(pure)
        If ShowTotalPortValue = True AndAlso is_Use_Total_Port = True Then Show_Total_Port(Convert.ToDouble(pure))
    End Sub

    'Private Sub Show_Total_Port(Pure As Double)
    '    Dim sp As SerialPort = New SerialPort(Total_Port, 9600, Parity.None, 8, StopBits.One)
    '    sp.Open()
    '    sp.Write(Convert.ToString(ChrW(12)))
    '    sp.WriteLine(" TABLE : " & TB_Num.ToString)
    '    sp.WriteLine(ChrW(13) & "TOTAL : " & Pure.ToString)
    '    sp.Close()
    '    sp.Dispose()
    '    sp = Nothing
    'End Sub

    Public Sub Show_Total_Port(Pure As Double)
        Try
            Dim sp As SerialPort = New SerialPort(Total_Port, 2400, Parity.None, 8, StopBits.One)
            sp.Open()
            sp.Write(Convert.ToString(ChrW(12)))
            sp.WriteLine(Pure)
            sp.Close()
            sp.Dispose()
            sp = Nothing
        Catch ex As Exception

        End Try

        '-------------------------------------------------------------------------------
    End Sub

    Public Sub Items_btn_Click(sender As Object, e As EventArgs) Handles Items_btn.Click
        BillsMetroGrid.Visible = False
        IMGrid.Visible = True
    End Sub

    Private Sub Bills_btn_Click(sender As Object, e As EventArgs) Handles Bills_btn.Click
        BillsMetroGrid.Visible = True
        IMGrid.Visible = False
    End Sub


    Private Sub BillsMetroGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BillsMetroGrid.CellContentClick

        If e.ColumnIndex = 0 Then
            Me.Cursor = Cursors.AppStarting
            'F_POS.SMPanel.Controls.Clear()
            F_POS.Reset_Fields()
            F_POS.isNewBill = 0
            F_POS.T_ID = BillsMetroGrid.CurrentRow.Cells("Bills_T_ID_CL").Value
            F_POS.BillNumTxt.Text = BillsMetroGrid.CurrentRow.Cells("B_Pr_ID_CL").Value
            Switch_To_Cash = True
            F_POS.Fill_Bill_Info()
            F_POS.SB_Contents_SELECT_Bill()
            F_POS.BillTypeCmb.SelectedValue = BillsMetroGrid.CurrentRow.Cells("B_Type_CL").Value
            F_SalesMenu.Hide()
            Me.Cursor = Cursors.Default
        End If

        If e.ColumnIndex = 1 Then
            If BillsMetroGrid.Rows.Count > 0 Then
                Me.Cursor = Cursors.AppStarting
                F_TB_BillIM = New TB_BillIM
                F_TB_BillIM.SB_ID = BillsMetroGrid.CurrentRow.Cells("B_D_ID_CL").Value
                F_TB_BillIM.SB_Bill_DayNum = BillsMetroGrid.CurrentRow.Cells("B_Pr_ID_CL").Value
                F_TB_BillIM.Pure = BillsMetroGrid.CurrentRow.Cells("Pure_CL").Value
                Set_Form(F_TB_BillIM, F_Panel)
                Bill_T_ID = BillsMetroGrid.CurrentRow.Cells("Bills_T_ID_CL").Value
                F_TB_BillIM.Select_IM()
                Me.Cursor = Cursors.Default
            End If
        End If

    End Sub


    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Pied_Money = 0
        If BillsMetroGrid.Rows.Count > 0 Then
            If Check_AG() = 0 Then
                MsgBox("يجب توحيد الزبون على الطاولة", MsgBoxStyle.Critical, "")
            Else
                Beep()
                If MessageBox.Show(" إغلاق حساب " + TB_Info.Text.ToString, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    If AG_ID = Default_AG_ID Then
                        If String.IsNullOrWhiteSpace(PureTextBox.Text) = False Then Pied_Money = PureTextBox.Text
                        If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
                        If isPrintBeforeEndBill_CB.Checked = True Then Print_Table_Bill()
                        PiedUp_Table(0)
                    Else
                        TablesFindingRest.ShowDialog()
                        If TablesFindingRest.is_Back = False Then
                            If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
                            If isPrintBeforeEndBill_CB.Checked = True Then Print_Table_Bill()
                            PiedUp_Table(0)
                        End If

                    End If

                End If
            End If
        End If
    End Sub

    Private Function Check_AG()
        AG_ID = BillsMetroGrid.Rows(0).Cells("AG_ID_CL").Value
        For i = 1 To BillsMetroGrid.Rows.Count - 1

            If AG_ID <> BillsMetroGrid.Rows(i).Cells("AG_ID_CL").Value Then
                Return 0
            End If
        Next
        Return 1
    End Function


    Private Sub PiedUp_Table(is_Debit As Boolean)
        Dim F As New Pay_Main_Form
        F.MONEY_VALUE = Pied_Money
        F.Temp_Tr_ID = SB_TR_ID
        F.AG_ID = AG_ID
        F.ShowDialog()

        If F.is_OK = True Then
            Dim Tr_ID, Pay_ID As Integer
            Tr_ID = F.Tr_ID
            Pay_ID = F.Pay_ID

            Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "PiedUp_Table"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@Pr_ID", Pr_ID)
                .Parameters.AddWithValue("@TB_ID", Me.TB_Num)
                If is_Debit = True Then
                    .Parameters.AddWithValue("@AG_ID", U_AG_ID)
                Else
                    .Parameters.AddWithValue("@AG_ID", AG_ID)
                End If
                .Parameters.AddWithValue("@Tr_ID", Tr_ID)
                .Parameters.AddWithValue("@USER_ID", USER_ID)
                .Parameters.AddWithValue("@Total", Pied_Money)
                .Parameters.AddWithValue("@TB_Info", TB_Info.Text)
                .Parameters.AddWithValue("@is_Debit", is_Debit)
                .Parameters.AddWithValue("@Pay_ID", Pay_ID)

            End With
            If SQL_SP_EXEC(C.Com) Then

                'If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
                'If isPrintBeforeEndBill_CB.Checked = True Then Print_Table_Bill()

                Me.LoadTableBalanceData(Me.TB_Num)
                Refresh_Table()

                If IS_SHOW_NUMBER = True Then Me.Close()
            End If


        End If
    End Sub

    Public Sub Refresh_Table()
        loadtables()
    End Sub


    Private Sub TB_ButOnAG_btn_Click(sender As Object, e As EventArgs) Handles TB_ButOnAG_btn.Click
        F_AgentsMenu = New AgentsMenu
        F_AgentsMenu.New_AG_Btn.Visible = False
        F_AgentsMenu.ShowDialog()
    End Sub

    Private Sub PrintBillButton_Click(sender As Object, e As EventArgs) Handles PrintBillButton.Click
        Print_Table_Bill()
    End Sub

    Private Sub Print_Table_Bill()
        If IMGrid.Rows.Count > 0 Then
            Try
                Me.Cursor = Cursors.AppStarting
                TBPrint()
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End If
    End Sub

    Public Sub TBPrint()
        CalculateTableBillTotals(False)

        Using doc As New System.Drawing.Printing.PrintDocument()
            If String.IsNullOrWhiteSpace(Default_Printer_80) = False Then doc.PrinterSettings.PrinterName = Default_Printer_80
            doc.PrintController = New System.Drawing.Printing.StandardPrintController()
            doc.DefaultPageSettings = CreateTableDetailsPageSettings(Me.IMGrid.Rows.Count)

            Dim RowIndex As Integer = 0
            AddHandler doc.PrintPage,
                Sub(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs)
                    DrawTableDetailsPage(e, RowIndex)
                End Sub

            If String.IsNullOrWhiteSpace(Default_Printer_80) = False AndAlso Def_Befor_Print = 1 Then Set_Default_Printer(Default_Printer_80)
            doc.Print()
        End Using
    End Sub

    Private Function CreateTableDetailsPageSettings(itemCount As Integer) As System.Drawing.Printing.PageSettings
        Dim settings As New System.Drawing.Printing.PageSettings()
        settings.Margins = New System.Drawing.Printing.Margins(0, 0, 0, 0)

        Dim paperHeight As Integer = Math.Max(520, 430 + (Math.Max(itemCount, 1) * 30))
        settings.PaperSize = New System.Drawing.Printing.PaperSize("Thermal80mm", 280, paperHeight)
        Return settings
    End Function

    Private Sub DrawTableDetailsPage(e As System.Drawing.Printing.PrintPageEventArgs, ByRef RowIndex As Integer)
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

        Dim g As Graphics = e.Graphics
        Dim printY As Integer = 10
        Dim paperWidth As Integer = 280

        Using fontTitle As New Font("Segoe UI", 12, FontStyle.Bold),
              fontSmallBold As New Font("Segoe UI", 8, FontStyle.Bold),
              fontBody As New Font("Segoe UI", 9, FontStyle.Regular),
              fontBodyBold As New Font("Segoe UI", 9, FontStyle.Bold),
              fontItem As New Font("Segoe UI", 8, FontStyle.Regular),
              fontItemBold As New Font("Segoe UI", 8, FontStyle.Bold)

            Dim fmtCenter As New StringFormat() With {.Alignment = StringAlignment.Center}
            Dim fmtArabic As New StringFormat() With {
                .Alignment = StringAlignment.Near,
                .FormatFlags = StringFormatFlags.DirectionRightToLeft
            }

            If String.IsNullOrWhiteSpace(SBill_Title_1) = False Then
                g.DrawString(SBill_Title_1, fontTitle, Brushes.Black, New Rectangle(5, printY, paperWidth, 30), fmtCenter)
                printY += 30
            End If

            If String.IsNullOrWhiteSpace(SBill_Title_2) = False Then
                g.DrawString(SBill_Title_2, fontSmallBold, Brushes.Black, New Rectangle(5, printY, paperWidth, 18), fmtCenter)
                printY += 22
            End If

            g.DrawString("تفاصيل الطاولة", fontTitle, Brushes.Black, New Rectangle(5, printY, paperWidth, 28), fmtCenter)
            printY += 32
            DrawTableThreeParts(g, "No", TB_Info.Text.Replace("الطاولة", "").Replace(":", "").Trim(), "رقم الطاولة", printY, fontBodyBold)
            printY += 22
            DrawTableThreeParts(g, "Date", Date.Now.ToString("yyyy/MM/dd HH:mm"), "التاريخ", printY, fontBodyBold)
            printY += 22
            DrawTableThreeParts(g, "User", USER_NAME, "المستخدم", printY, fontBody)
            printY += 25

            DrawTableDashedLine(g, printY, paperWidth)
            printY += 10

            g.DrawString("ت", fontSmallBold, Brushes.Black, New Rectangle(260, printY, 20, 30), fmtCenter)
            g.DrawString("Item" & vbCrLf & "الصنف", fontSmallBold, Brushes.Black, New Rectangle(150, printY, 110, 30), fmtArabic)
            g.DrawString("Qty" & vbCrLf & "كمية", fontSmallBold, Brushes.Black, New Rectangle(115, printY, 35, 30), fmtCenter)
            g.DrawString("Price" & vbCrLf & "السعر", fontSmallBold, Brushes.Black, New Rectangle(65, printY, 50, 30), fmtCenter)
            g.DrawString("Total" & vbCrLf & "الإجمالي", fontSmallBold, Brushes.Black, New Rectangle(5, printY, 60, 30), fmtCenter)
            printY += 30

            DrawTableDashedLine(g, printY, paperWidth)
            printY += 6

            Dim rowCounter As Integer = RowIndex + 1
            While RowIndex < Me.IMGrid.Rows.Count
                Dim row As DataGridViewRow = Me.IMGrid.Rows(RowIndex)
                If row.IsNewRow Then
                    RowIndex += 1
                    Continue While
                End If

                Dim itemName As String = GetTableCellText(row, "IM_NameCL")
                Dim qty As String = FormatTableNumber(GetTableCellValue(row, "QTY_CL"))
                Dim price As String = FormatTableNumber(GetTableCellValue(row, "Unit_Price_CL"))
                Dim total As String = FormatTableNumber(GetTableCellValue(row, "Total_CL"))

                Dim itemSizeF As SizeF = g.MeasureString(itemName, fontItem, 110, fmtArabic)
                Dim rowHeight As Integer = Math.Max(16, CInt(itemSizeF.Height) + 2)

                If printY + rowHeight + 120 > e.PageBounds.Bottom Then
                    e.HasMorePages = True
                    Return
                End If

                g.DrawString(rowCounter.ToString(), fontItem, Brushes.Black, New Rectangle(260, printY, 20, rowHeight), fmtCenter)
                g.DrawString(itemName, fontItem, Brushes.Black, New Rectangle(150, printY, 110, rowHeight), fmtArabic)
                g.DrawString(qty, fontItem, Brushes.Black, New Rectangle(115, printY, 35, rowHeight), fmtCenter)
                g.DrawString(price, fontItem, Brushes.Black, New Rectangle(65, printY, 50, rowHeight), fmtCenter)
                g.DrawString(total, fontItemBold, Brushes.Black, New Rectangle(5, printY, 60, rowHeight), fmtCenter)

                printY += rowHeight
                RowIndex += 1
                rowCounter += 1
            End While

            printY += 4
            DrawTableDashedLine(g, printY, paperWidth)
            printY += 10

            DrawTableThreeParts(g, "Gross Total", FormatTableNumber(TableBillTotal), "الإجمالي", printY, fontBodyBold)
            printY += 22
            DrawTableThreeParts(g, "Discount", FormatTableNumber(TableBillDiscount), "التخفيض", printY, fontBody)
            printY += 20
            DrawTableThreeParts(g, "Net Total", FormatTableNumber(TableBillPure), "الصافي", printY, fontTitle)
            printY += 40

            DrawTableDashedLine(g, printY, paperWidth)
            printY += 12

            DrawTableThreeParts(g, "User", USER_NAME, "المستخدم", printY, fontBody)
            printY += 30

            g.DrawString("طُبعت في: " & Now.ToString("yyyy-MM-dd HH:mm:ss"), fontSmallBold, Brushes.Black, New Rectangle(5, printY, paperWidth, 15), fmtCenter)
            printY += 25

            If String.IsNullOrWhiteSpace(SBill_Footer) = False Then
                g.DrawString(SBill_Footer, fontBodyBold, Brushes.Black, New Rectangle(5, printY, paperWidth, 40), fmtCenter)
            End If
        End Using

        e.HasMorePages = False
    End Sub

    Private Sub DrawTableThreeParts(g As Graphics, engText As String, value As String, araText As String, y As Integer, font As Font)
        Dim fLeft As New StringFormat() With {.Alignment = StringAlignment.Near}
        Dim fCenter As New StringFormat() With {.Alignment = StringAlignment.Center}
        Dim fRight As New StringFormat() With {.Alignment = StringAlignment.Far}

        g.DrawString(engText, font, Brushes.Black, New Rectangle(5, y, 80, 25), fLeft)
        g.DrawString(value, font, Brushes.Black, New Rectangle(85, y, 95, 25), fCenter)
        g.DrawString(araText, font, Brushes.Black, New Rectangle(180, y, 85, 25), fRight)
    End Sub

    Private Sub DrawTableDashedLine(g As Graphics, y As Integer, width As Integer)
        Using p As New Pen(Color.Black, 1)
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            g.DrawLine(p, 5, y, width, y)
        End Using
    End Sub

    Private Sub DrawReceiptLine(g As Graphics, text As String, font As Font, brush As Brush, rect As Rectangle, flags As TextFormatFlags)
        Dim lineFlags As TextFormatFlags = flags Or TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPadding Or TextFormatFlags.NoPrefix
        Dim solidBrush As SolidBrush = TryCast(brush, SolidBrush)
        Dim textColor As Color = If(solidBrush Is Nothing, Color.Black, solidBrush.Color)
        TextRenderer.DrawText(g, If(text, ""), font, rect, textColor, lineFlags)
    End Sub

    Private Sub DrawReceiptInfoLine(g As Graphics, caption As String, value As String, font As Font, left As Integer, ByRef y As Integer, width As Integer)
        Dim labelWidth As Integer = 82
        Dim rowHeight As Integer = 22
        Dim valueRect As New Rectangle(left, y, width - labelWidth, rowHeight)
        Dim captionRect As New Rectangle(left + width - labelWidth, y, labelWidth, rowHeight)

        DrawReceiptLine(g, caption & " :", font, Brushes.Black, captionRect, TextFormatFlags.Right Or TextFormatFlags.RightToLeft Or TextFormatFlags.VerticalCenter)
        DrawReceiptLine(g, If(value, ""), font, Brushes.Black, valueRect, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
        y += rowHeight
    End Sub

    Private Sub DrawReceiptSummaryLine(g As Graphics, caption As String, value As String, font As Font, left As Integer, ByRef y As Integer, width As Integer)
        Dim valueWidth As Integer = 110
        Dim rowHeight As Integer = 25
        Dim valueRect As New Rectangle(left, y, valueWidth, rowHeight)
        Dim captionRect As New Rectangle(left + valueWidth, y, width - valueWidth, rowHeight)

        DrawReceiptLine(g, caption & " :", font, Brushes.Black, captionRect, TextFormatFlags.Right Or TextFormatFlags.RightToLeft Or TextFormatFlags.VerticalCenter)
        DrawReceiptLine(g, If(value, "0"), font, Brushes.Black, valueRect, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
        y += rowHeight
    End Sub

    Private Sub DrawTableMetaLine(g As Graphics, caption As String, value As String, font As Font, left As Integer, ByRef y As Integer, width As Integer, rightFormat As StringFormat, leftFormat As StringFormat)
        Dim labelWidth As Integer = 70
        Dim rowHeight As Integer = 17
        Dim valueRect As New Rectangle(left, y, width - labelWidth, rowHeight)
        Dim captionRect As New Rectangle(left + width - labelWidth, y, labelWidth, rowHeight)

        DrawReceiptLine(g, caption & " :", font, Brushes.Black, captionRect, TextFormatFlags.Right Or TextFormatFlags.RightToLeft Or TextFormatFlags.VerticalCenter)
        DrawReceiptLine(g, If(value, ""), font, Brushes.Black, valueRect, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
        y += rowHeight
    End Sub

    Private Sub DrawTableWrappedBlock(g As Graphics, text As String, font As Font, brush As Brush, left As Integer, ByRef y As Integer, width As Integer, lineHeight As Integer, format As StringFormat)
        Dim lines As System.Collections.Generic.List(Of String) = WrapTablePrintText(g, text, font, width - 4)
        For Each line As String In lines
            g.DrawString(line, font, brush, New Rectangle(left + 2, y, width - 4, lineHeight), format)
            y += lineHeight
        Next
    End Sub

    Private Sub DrawTableSingleLine(g As Graphics, text As String, font As Font, brush As Brush, left As Integer, ByRef y As Integer, width As Integer, lineHeight As Integer, format As StringFormat)
        Dim printText As String = FitTablePrintText(g, text, font, width - 4)
        g.DrawString(printText, font, brush, New Rectangle(left + 2, y, width - 4, lineHeight), format)
        y += lineHeight
    End Sub

    Private Function FitTablePrintText(g As Graphics, text As String, font As Font, maxWidth As Integer) As String
        text = If(text, "").Trim()
        If String.IsNullOrWhiteSpace(text) Then Return ""
        If g.MeasureString(text, font).Width <= maxWidth Then Return text

        Dim result As String = text
        While result.Length > 0 AndAlso g.MeasureString(result & "...", font).Width > maxWidth
            result = result.Substring(0, result.Length - 1)
        End While

        If String.IsNullOrWhiteSpace(result) Then Return ""
        Return result & "..."
    End Function

    Private Sub DrawTableSummaryLine(g As Graphics, caption As String, value As String, font As Font, left As Integer, ByRef y As Integer, width As Integer, borderPen As Pen, rightFormat As StringFormat, leftFormat As StringFormat)
        Dim rowHeight As Integer = 21
        Dim valueWidth As Integer = 80
        Dim captionRect As New Rectangle(left + valueWidth, y, width - valueWidth, rowHeight)
        Dim valueRect As New Rectangle(left, y, valueWidth, rowHeight)

        DrawReceiptLine(g, caption & " :", font, Brushes.Black, captionRect, TextFormatFlags.Right Or TextFormatFlags.RightToLeft Or TextFormatFlags.VerticalCenter)
        DrawReceiptLine(g, If(value, "0"), font, Brushes.Black, valueRect, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
        g.DrawLine(borderPen, left, y + rowHeight, left + width, y + rowHeight)
        y += rowHeight
    End Sub

    Private Sub DrawTableHeader(g As Graphics, left As Integer, y As Integer, nameWidth As Integer, qtyWidth As Integer, priceWidth As Integer, totalWidth As Integer, headerFont As Font, headerBrush As Brush, centerFormat As StringFormat)
        Dim headerHeight As Integer = 24
        Dim totalRect As New Rectangle(left, y, totalWidth, headerHeight)
        Dim priceRect As New Rectangle(left + totalWidth, y, priceWidth, headerHeight)
        Dim qtyRect As New Rectangle(left + totalWidth + priceWidth, y, qtyWidth, headerHeight)
        Dim nameRect As New Rectangle(left + totalWidth + priceWidth + qtyWidth, y, nameWidth, headerHeight)

        g.FillRectangle(headerBrush, New Rectangle(left, y, nameWidth + qtyWidth + priceWidth + totalWidth, headerHeight))
        g.DrawString("الإجمالي", headerFont, Brushes.White, totalRect, centerFormat)
        g.DrawString("السعر", headerFont, Brushes.White, priceRect, centerFormat)
        g.DrawString("كمية", headerFont, Brushes.White, qtyRect, centerFormat)
        g.DrawString("الصنف", headerFont, Brushes.White, nameRect, centerFormat)
    End Sub

    Private Function GetTableCellValue(row As DataGridViewRow, columnName As String) As Object
        If row Is Nothing OrElse row.DataGridView Is Nothing OrElse row.DataGridView.Columns.Contains(columnName) = False Then Return Nothing
        Return row.Cells(columnName).Value
    End Function

    Private Function GetTableCellText(row As DataGridViewRow, columnName As String) As String
        Dim value As Object = GetTableCellValue(row, columnName)
        If value Is Nothing OrElse value Is DBNull.Value Then Return ""
        Return value.ToString()
    End Function

    Private Function GetTableDecimalValue(value As Object) As Decimal
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0D

        Dim number As Decimal
        If Decimal.TryParse(value.ToString(), number) Then Return number
        Return 0D
    End Function

    Private Function FormatTableNumber(value As Object) As String
        If value Is Nothing OrElse value Is DBNull.Value Then Return "0"

        Dim number As Decimal
        If Decimal.TryParse(value.ToString(), number) Then
            If number = Decimal.Truncate(number) Then Return number.ToString("0")
            Return number.ToString("0.###")
        End If

        Return value.ToString()
    End Function

    Private Function WrapTablePrintText(g As Graphics, text As String, font As Font, maxWidth As Integer) As System.Collections.Generic.List(Of String)
        Dim lines As New System.Collections.Generic.List(Of String)()
        text = If(text, "").Trim()
        If String.IsNullOrWhiteSpace(text) Then
            lines.Add("")
            Return lines
        End If

        Dim current As String = ""
        Dim parts As String() = text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        For Each part As String In parts
            Dim candidate As String = If(String.IsNullOrWhiteSpace(current), part, current & " " & part)
            If FitsTablePrintLine(g, candidate, font, maxWidth) Then
                current = candidate
            Else
                If String.IsNullOrWhiteSpace(current) = False Then lines.Add(current)

                If FitsTablePrintLine(g, part, font, maxWidth) Then
                    current = part
                Else
                    Dim tokenLines As System.Collections.Generic.List(Of String) = SplitTablePrintLongToken(g, part, font, maxWidth)
                    For i As Integer = 0 To tokenLines.Count - 2
                        lines.Add(tokenLines(i))
                    Next
                    current = If(tokenLines.Count = 0, "", tokenLines(tokenLines.Count - 1))
                End If
            End If
        Next

        If String.IsNullOrWhiteSpace(current) = False Then lines.Add(current)
        If lines.Count = 0 Then lines.Add(text)
        Return lines
    End Function

    Private Function FitsTablePrintLine(g As Graphics, text As String, font As Font, maxWidth As Integer) As Boolean
        Return g.MeasureString(text, font).Width <= maxWidth
    End Function

    Private Function SplitTablePrintLongToken(g As Graphics, token As String, font As Font, maxWidth As Integer) As System.Collections.Generic.List(Of String)
        Dim lines As New System.Collections.Generic.List(Of String)()
        Dim current As String = ""

        For Each ch As Char In token
            Dim candidate As String = current & ch
            If FitsTablePrintLine(g, candidate, font, maxWidth) Then
                current = candidate
            Else
                If String.IsNullOrWhiteSpace(current) = False Then lines.Add(current)
                current = ch.ToString()
            End If
        Next

        If String.IsNullOrWhiteSpace(current) = False Then lines.Add(current)
        Return lines
    End Function

    Private Sub DrawTablePrintLines(g As Graphics, lines As System.Collections.Generic.List(Of String), font As Font, brush As Brush, rect As Rectangle, lineHeight As Integer, format As StringFormat)
        Dim currentY As Integer = rect.Top + 4
        For Each line As String In lines
            Dim lineRect As New Rectangle(rect.Left + 4, currentY, rect.Width - 8, lineHeight)
            g.DrawString(line, font, brush, lineRect, format)
            currentY += lineHeight
        Next
    End Sub
    Private Sub TranConfirm_btn_Click(sender As Object, e As EventArgs) Handles TranConfirm_btn.Click
        If TB_Types_CMB.SelectedIndex = 1 Then
            If MessageBox.Show(" نقل كل الأصناف من طاولة " + TB_F_txt.Text + " إلى  " + TB_T_txt.Text, "نقل", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then Transform_Tables()
        ElseIf TB_Types_CMB.SelectedIndex = 2 Then
            Tables_Trans_IM.TB_Num = Me.tran_F
            Tables_Trans_IM.TB_TO_Num = Me.tran_T
            Tables_Trans_IM.ShowDialog()
        End If
        Clear_TB_Types_Fields()
    End Sub

    Private Sub Transform_Tables()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Transform_Tables"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Table_From", tran_F)
            .Parameters.AddWithValue("@Table_To", tran_T)
        End With
        If SQL_SP_EXEC(C.Com) Then
            Network_Edit_Tracker_insert("نقل من طاولة  : " + TB_F_txt.Text + " إلى :  " + TB_T_txt.Text, 0, 28, 3)
            'TB_Tran_CB.Checked = False
            loadtables()
            'TB_F_txt.Clear()
            'TB_T_txt.Clear()
            Clear_TB_Types_Fields()
        End If
    End Sub

    'Private Sub TB_Tran_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    'If TB_Tran_CB.Checked = True Then
    '    '    TB_Tran_CB.ForeColor = Color.DarkGreen
    '    'Else
    '    '    TB_Tran_CB.ForeColor = Color.Black
    '    'End If
    '    tran_F = 0
    '    tran_T = 0
    '    TB_F_txt.Clear()
    '    TB_T_txt.Clear()
    'End Sub


    Public Sub Refresh_TB_Balance()
        Me.LoadTableBalanceData(Me.TB_Num)
        F_TB_BillIM.Select_IM()
    End Sub

    Private Sub PiedApart_btn_Click(sender As Object, e As EventArgs) Handles PiedApart_btn.Click
        TB_Part_Pied = True
        TablePiedApart.TB_Num = Me.TB_Num
        TablePiedApart.ShowDialog()
        TB_Part_Pied = False
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        Dim newLeft As Integer = -F_Panel.AutoScrollPosition.X
        Dim newTop As Integer = -F_Panel.AutoScrollPosition.Y
        newTop = -F_Panel.AutoScrollPosition.Y - 450
        F_Panel.AutoScrollPosition = New Point(newLeft, newTop)
    End Sub

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Dim newLeft As Integer = -F_Panel.AutoScrollPosition.X
        Dim newTop As Integer = -F_Panel.AutoScrollPosition.Y
        newTop = -F_Panel.AutoScrollPosition.Y + 450
        F_Panel.AutoScrollPosition = New Point(newLeft, newTop)
    End Sub


    Private Sub Debit_Table_btn_Click(sender As Object, e As EventArgs) Handles Debit_Table_btn.Click
        If U_AG_ID = 0 Then
            MsgBox("تأكد من بيانات المستخدم", MsgBoxStyle.Critical, " خطأ ")
        Else
            Beep()
            If MessageBox.Show(" تحويل حساب  " + TB_Info.Text.ToString + " كمعاملة دين ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Pied_Money = PureTextBox.Text
                PiedUp_Table(1)
            End If
        End If


    End Sub

    Private Sub Apart_List_btn_Click(sender As Object, e As EventArgs) Handles Apart_List_btn.Click
        Show_Tables_Aparts.ShowDialog()
    End Sub

    Private Sub Show_AllBill_Clmns_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_AllBill_Clmns_CB.CheckedChanged
        CB_CHecked(sender)
        Show_AllBill_Clmns = Show_AllBill_Clmns_CB.Checked

        show_bill_tb_columns()
        MY_Settings.Save_AppSetting()
    End Sub

    Private Sub show_bill_tb_columns()
        BillsMetroGrid.Columns("Date_CL").Visible = Show_AllBill_Clmns
        BillsMetroGrid.Columns("Total_TB_CL").Visible = Show_AllBill_Clmns
        BillsMetroGrid.Columns("Discount_CL").Visible = Show_AllBill_Clmns
        BillsMetroGrid.Columns("User_Name_CL").Visible = Show_AllBill_Clmns
    End Sub

    Private Sub BillsMetroGrid_DataSourceChanged(sender As Object, e As EventArgs) Handles BillsMetroGrid.DataSourceChanged
        Calc_Bill()
        ApplyBillsGridCompactStyle()
    End Sub

    Private Sub ClearNumBtn_Click(sender As Object, e As EventArgs) Handles ClearNumBtn.Click
        Clear_TB_Types_Fields()
    End Sub

    Private Sub Clear_TB_Types_Fields()
        TB_F_txt.Clear()
        TB_T_txt.Clear()
        tran_F = 0
        tran_T = 0
        TB_Types_CMB.SelectedIndex = 0
    End Sub

    Private Sub isPrintBeforeEndBill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isPrintBeforeEndBill_CB.CheckedChanged
        CB_CHecked(sender)
        Print_TB_Before_End = isPrintBeforeEndBill_CB.Checked
        MY_Settings.Save_AppSetting()
    End Sub
End Class
