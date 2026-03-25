Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Management
Imports CrystalDecisions.CrystalReports.Engine
Imports ZXing


Public Class printbarcode
    Public barcode As String = ""
    Public product As String = ""
    Dim PrintDoc As Printing.PrintDocument = New Printing.PrintDocument()
    Dim pd_PrintDialog As New PrintDialog



    Dim IM_ID As Integer
    Dim IM_DT As New DataTable
    Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False
    Dim U_IM_ID As Integer
    Dim Search As Boolean = True
    Public Auto_Print As Boolean = False




    Public Sub printbarcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Print_CPName_CB.Checked = MY_Settings.ShowCname
            Print_Price_CB.Checked = MY_Settings.ShowIM_Price_On_Barcode
            txtbarcode.Text = barcode
            IM_SH_txt.Text = product
            width.Value = MY_Settings.BarcodeWidth
            hieght.Value = MY_Settings.BarcodeHieght
            nud_Count.Text = MY_Settings.BarcodeNumber
            MarginPUP.Value = MY_Settings.BarcodePUp
            MarginPLeft.Value = MY_Settings.BarcodePLeft
            MarginBUp.Value = MY_Settings.BarcodeBUp
            cbA4.Checked = MY_Settings.BarcodeCheckA4
            txtcol.Value = MY_Settings.BarcodeColumn
            txtrow.Value = MY_Settings.BarcodeRows

            Print_IM_NAME_CB.Checked = MY_Settings.ShowIM_IM_NAME_On_Barcode
            PRINT_IM_NUM_CB.Checked = MY_Settings.ShowIM_IM_NUM_On_Barcode



            'SM_LoadPrinters_ToOptions()
            txtbarcode.Focus()


            If Auto_Print = True Then
                If FormType = 2 Then
                    Fetch_Item_and_Print_Pch()
                ElseIf FormType = 4 Then
                    Fetch_Item_and_Print_Invoice()
                End If
            End If

            LoadPrinters(Barcode_DefPrinter_Cm)
            If Not String.IsNullOrWhiteSpace(Default_Barcode_Printer) Then Barcode_DefPrinter_Cm.Text = Default_Barcode_Printer

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Fetch_Item_and_Print_FRM()
        Search = False
        If F_Format_Items_Manual.AGMetroGrid.Rows.Count > 0 Then
            IM_ID = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("prc_IM_ID_CL").Value
            IM_SH_txt.Text = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("FRM_IM_NAME_CL").Value
            txtbarcode.Text = IM_Get_Barcode_FROM(F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("prc_U_ID_CL").Value)
            Load_IM_Barcode()

            nud_Count.Text = Convert.ToInt32(F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("prc_QTY_CL").Value)
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
            Fetch_IM_Units()
            IMDataGridViewX.Visible = False
            IM_SH_txt.Select()

            If Not IsDBNull(F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("SALE_Price_CL").Value) Then _
                If Not String.IsNullOrEmpty(F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("SALE_Price_CL").Value) Then txtprice.Text = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("SALE_Price_CL").Value
        End If
        Search = True


    End Sub


    Private Sub Fetch_Item_and_Print_Invoice()
        Search = False
        If F_invoice.AGMetroGrid.Rows.Count > 0 Then
            IM_ID = F_invoice.AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value
            IM_SH_txt.Text = F_invoice.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
            txtbarcode.Text = IM_Get_Barcode_FROM(F_Invoice.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value)
            Load_IM_Barcode()

            nud_Count.Text = Convert.ToInt32(F_Invoice.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value)
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
            Fetch_IM_Units()
            IMDataGridViewX.Visible = False
            IM_SH_txt.Select()

            If Not IsDBNull(F_Invoice.AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then _
                If Not String.IsNullOrEmpty(F_Invoice.AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then txtprice.Text = F_Invoice.AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value
        End If
        Search = True


    End Sub


    Public Function IM_Get_Barcode_FROM(U_ID As Integer) 'F_Invoice.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value
        Dim c As New C
        Dim Bar As String = ""
        Try
            Dim s As String = "select Barcode from IM_Menu_Units_V WHERE IM_ID = '" & IM_ID & "' AND U_Cargo =  (select U_Cargo From Units Where U_ID = '" & U_ID & "') AND Barcode <> ''"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Bar = c.Dr("Barcode")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return Bar

    End Function

    Private Sub Fetch_Item_and_Print_Pch()
        Search = False
        If F_Pch.AGMetroGrid.Rows.Count > 0 Then
            IM_ID = F_Pch.AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value
            IM_SH_txt.Text = F_Pch.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
            txtbarcode.Text = IM_Get_Barcode_FROM(F_Pch.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value)
            Load_IM_Barcode()
            nud_Count.Text = Convert.ToInt32(F_Pch.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value)
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
            Fetch_IM_Units()
            IMDataGridViewX.Visible = False
            IM_SH_txt.Select()

            If Not IsDBNull(F_Pch.AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then _
                If Not String.IsNullOrEmpty(F_Pch.AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then txtprice.Text = F_Pch.AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value
        End If
        Search = True

    End Sub

    Public Sub Load_IM()
        Dim c As New C
        If Search = True Then
            Try
                IM_DT.Clear()
                c.Str = IM_Serach_barcode(IM_SH_txt.Text)
                c.Da = New SqlClient.SqlDataAdapter(c.Str, c.Con)
                c.Da.Fill(IM_DT)
                IMDataGridViewX.DataSource = IM_DT
                If IM_DT.Rows.Count > 0 Then
                    IMDataGridViewX.Visible = True
                    IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
                Else
                    IMDataGridViewX.Visible = False
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

    '/======================================================================================================================
    Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged

        If IM_SH_txt.Text.Count > 0 Then
            Load_IM()
        Else
            IMDataGridViewX.Visible = False
            IM_ID = 0
            U_Dt.Clear()
        End If
        If IM_ID = 0 Then
            IM_SH_txt.BackColor = Color.LightGray
        Else
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub IMDataGridViewX_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles IMDataGridViewX.CellClick
        Fetch_ItemToList()
    End Sub

    Private Sub IMDataGridViewX_KeyDown(sender As Object, e As KeyEventArgs) Handles IMDataGridViewX.KeyDown
        If e.KeyCode = Keys.Return Then Fetch_ItemToList()
        If e.KeyCode = Keys.Up Then If IMDataGridViewX.CurrentRow.Index = 0 Then IM_SH_txt.Select()
    End Sub

    Private Sub IM_Fetch_QTY()
        Dim c As New C
        Try
            Dim s As String
            s = "select CONVERT(NUMERIC(18,2),Price) AS Price,Barcode,IM_NUM from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                txtprice.Text = c.Dr("Price")
                IM_Num_txt.Text = c.Dr("IM_NUM")
                'txtbarcode.Text = c.Dr("Barcode")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Fetch_ItemToList()
        Search = False
        If IMDataGridViewX.Rows.Count > 0 Then
            IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
            IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
            txtbarcode.Text = IMDataGridViewX.CurrentRow.Cells("Barcode_CL").Value
            U_IM_ID = IMDataGridViewX.CurrentRow.Cells("U_IM_ID_CL").Value
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
            Fetch_IM_Units()
            IMDataGridViewX.Visible = False
            IM_SH_txt.Select()
        End If
        Search = True
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
            '  IM_Unit_cm.SelectedValue = U_IM_ID        
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Get_Unit = True
        IM_Fetch_QTY()
    End Sub

    Private Sub Show_IM_btn_Click(sender As Object, e As EventArgs) Handles Show_IM_btn.Click
        If IMDataGridViewX.Visible = True Then
            IMDataGridViewX.Visible = False
        Else
            Load_ALL_IM()
        End If
    End Sub

    Public Sub Load_ALL_IM()
        Dim c As New C
        Try
            IM_DT.Clear()
            Dim s As String
            s = "select U_IM_ID,IM_ID,item_name,U_Name,Barcode from IM_units_Search_V WHERE Barcode <>''  Order by item_name ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_DT)
            IMDataGridViewX.DataSource = IM_DT
            If IM_DT.Rows.Count > 0 Then
                IMDataGridViewX.Visible = True
                IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
            Else
                IMDataGridViewX.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbarcode.KeyDown

        Select Case e.KeyCode
            Case Keys.Return : If String.IsNullOrWhiteSpace(txtbarcode.Text) = False Then Load_IM_Barcode()
            Case Keys.Delete : txtbarcode.Clear()
        End Select
    End Sub

    Public Sub Load_IM_Barcode()
        Dim c As New C
        IM_DT.Clear()
        Try
            Dim s As String
            s = "select U_IM_ID,IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & txtbarcode.Text & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_ID = c.Dr("IM_ID")
                IM_SH_txt.Text = c.Dr("item_name")
                IMDataGridViewX.Visible = False
                Fetch_IM_Units()
                IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
            Else
                MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    '/======================================================================================================================


    Public Sub PrintDocHandler(ByVal sender As Object, ByVal ev As Printing.PrintPageEventArgs)
        Try
            ev.PageSettings.Margins = New Printing.Margins(ConvertToPixel(MarginPLeft.Value), ConvertToPixel(MarginPLeft.Value), ConvertToPixel(MarginPUP.Value), ConvertToPixel(MarginPUP.Value))
            If cbA4.Checked Then
                Dim i As Integer = 0
                Dim j As Integer = 0
                Dim x As Integer = 0
                Dim Y As Integer = 0
                For i = 0 To txtrow.Value - 1
                    For j = 0 To txtcol.Value - 1
                        ev.Graphics.DrawImage(PictureBox1.Image, New Rectangle(x, Y, PictureBox1.Image.Width, PictureBox1.Image.Height))
                        '   ev.Graphics.DrawRectangle(New Pen(Color.Black), x, Y, PictureBox1.Image.Width, PictureBox1.Image.Height)
                        '     MsgBox("X= " & x & "    Y= " & Y & "     Width= " & PictureBox1.Image.VerticalResolution & "   hieght=" & PictureBox1.Image.HorizontalResolution)
                        x = x + ConvertToPixel(width.Value)
                    Next
                    x = 0
                    Y = Y + ConvertToPixel(hieght.Value)
                Next
            Else
                ev.Graphics.DrawImage(PictureBox1.Image, 0, 0)
                '      PictureBox1.Image.Save("ddss.jpg")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub
    Public Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            PageSetupDialog1.Document = PrintDoc
            PageSetupDialog1.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If String.IsNullOrWhiteSpace(nud_Count.Text) Then nud_Count.Text = "1"
            Dim Class1 As New C
            If cbA4.Checked Then
                If CheckValue() Then
                    'Dim ps As New PaperSize
                    'ps.Height = hieght.Value
                    'ps.Width = width.Value
                    If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
                    AddHandler PrintDoc.PrintPage, AddressOf PrintDocHandler
                    'تحديد عدد النسخ المراد طباعتها
                    pd_PrintDialog.PrinterSettings.Copies = nud_Count.Text
                    'PrintDoc.DefaultPageSettings.PaperSize = ps
                    PrintDoc.DefaultPageSettings.Margins = New Printing.Margins(ConvertToPixel(MarginPLeft.Value), ConvertToPixel(MarginPLeft.Value), ConvertToPixel(MarginPUP.Value), ConvertToPixel(MarginPUP.Value))
                    PrintDoc.OriginAtMargins = True
                    PrintPreviewDialog1.Document = PrintDoc
                    PrintPreviewDialog1.ShowDialog()
                End If
            Else


                Dim barc As New BarcodeWriter
                barc.Format = BarcodeFormat.CODE_128
                PictureBox1.Image = barc.Write(txtbarcode.Text)

                Class1.Com = New SqlCommand
                Class1.Com.Connection = Class1.Con
                Class1.Com.CommandType = CommandType.StoredProcedure
                Class1.Com.CommandText = "[Barcode_insert]"
                Class1.Com.Parameters.AddWithValue("@Barcode_Image", ConvertImage(PictureBox1.Image))
                Class1.Con.Open()
                Class1.Com.ExecuteNonQuery()
                Class1.Con.Close()

                Dim pp As New ReportConnection
                pp.rp.Load(Application.StartupPath & "\reports\barcode.rpt")
                pp.CrTables = pp.rp.Database.Tables
                For Each CrTable In pp.CrTables
                    pp.crtableLogoninfo = CrTable.LogOnInfo
                    pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                    CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
                Next
                'Dim by = DirectCast(New ImageConverter().ConvertTo(Code128_2(txtbarcode.Text, "A", "", 0, 0, 0, 0, 3.65, 0.66), GetType(Byte())), Byte())
                With pp
                    If MY_Settings.ShowCname Then
                        .rp.SetParameterValue(0, SBill_Title_1)
                    Else
                        .rp.SetParameterValue(0, "")
                    End If
                    .rp.SetParameterValue(1, "*" & txtbarcode.Text & "*")

                    If Print_IM_NAME_CB.Checked = True Then
                        .rp.SetParameterValue(2, IM_SH_txt.Text)
                    Else
                        .rp.SetParameterValue(2, "")
                    End If


                    'If PRINT_IM_NUM_CB.Checked = True And Print_IM_NAME_CB.Checked = False Then
                    '    .rp.SetParameterValue(2, IM_Num_txt.Text)
                    'Else
                    '    .rp.SetParameterValue(2, "")
                    'End If


                    .rp.SetParameterValue(3, txtbarcode.Text)

                    If Print_Price_CB.Checked = True Then
                        .rp.SetParameterValue(4, " السعر :  " & txtprice.Text)
                    Else
                        .rp.SetParameterValue(4, "")
                    End If
                End With
                Dim p As New print
                p.CrystalReportViewer1.ReportSource = pp.rp
                p.Show()

            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If String.IsNullOrWhiteSpace(nud_Count.Text) Then nud_Count.Text = "1"
            Dim Class1 As New C
            If cbA4.Checked Then
                If CheckValue() Then
                    'If cbA4.Checked Then
                    '    PrintDoc = New PrintDocument
                    'End If
                    pd_PrintDialog.UseEXDialog = True
                    pd_PrintDialog.AllowPrintToFile = False
                    'تحديد عدد النسخ المراد طباعتها.
                    pd_PrintDialog.PrinterSettings.Copies = nud_Count.Text
                    If pd_PrintDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                        pd_PrintDialog.PrinterSettings.DefaultPageSettings.Margins = New Printing.Margins(ConvertToPixel(MarginPLeft.Value), ConvertToPixel(MarginPLeft.Value), ConvertToPixel(MarginPUP.Value), ConvertToPixel(MarginPUP.Value))
                        PrintDoc.PrinterSettings = pd_PrintDialog.PrinterSettings
                        PrintDoc.OriginAtMargins = True
                        AddHandler PrintDoc.PrintPage, AddressOf PrintDocHandler
                        'امر الطباعة
                        If cbA4.Checked = False Then
                            Dim pkCustomSize1 As New System.Drawing.Printing.PaperSize("Custom Paper Size", ConvertToPixel(width.Value) + (ConvertToPixel(MarginPLeft.Value) * 2), ConvertToPixel(hieght.Value) + (2 * ConvertToPixel(MarginPLeft.Value)))
                            PrintDoc.DefaultPageSettings.PaperSize = pkCustomSize1
                        End If
                        PrintDoc.Print()
                    End If
                End If
            Else
                Dim barc As New BarcodeWriter
                barc.Format = BarcodeFormat.CODE_128
                PictureBox1.Image = barc.Write(txtbarcode.Text)

                Class1.Com = New SqlCommand
                Class1.Com.Connection = Class1.Con
                Class1.Com.CommandType = CommandType.StoredProcedure
                Class1.Com.CommandText = "[Barcode_insert]"
                Class1.Com.Parameters.AddWithValue("@Barcode_Image", ConvertImage(PictureBox1.Image))
                Class1.Con.Open()
                Class1.Com.ExecuteNonQuery()
                Class1.Con.Close()

                Dim pp As New ReportConnection
                pp.rp.Load(Application.StartupPath & "\reports\barcode.rpt")
                pp.CrTables = pp.rp.Database.Tables
                For Each CrTable In pp.CrTables
                    pp.crtableLogoninfo = CrTable.LogOnInfo
                    pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                    CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
                Next
                'Dim by = DirectCast(New ImageConverter().ConvertTo(Code128_2(txtbarcode.Text, "A", "", 0, 0, 0, 0, 3.65, 0.66), GetType(Byte())), Byte())
                With pp
                    If MY_Settings.ShowCname Then
                        .rp.SetParameterValue(0, SBill_Title_1)
                    Else
                        .rp.SetParameterValue(0, "")
                    End If
                    .rp.SetParameterValue(1, "*" & txtbarcode.Text & "*")

                    If Print_IM_NAME_CB.Checked = True Then
                        .rp.SetParameterValue(2, IM_SH_txt.Text)
                    Else
                        .rp.SetParameterValue(2, "")
                    End If


                    'If PRINT_IM_NUM_CB.Checked = True And Print_IM_NAME_CB.Checked = False Then
                    '    .rp.SetParameterValue(2, IM_Num_txt.Text)
                    'Else
                    '    .rp.SetParameterValue(2, "")
                    'End If


                    .rp.SetParameterValue(3, txtbarcode.Text)

                    If Print_Price_CB.Checked = True Then
                        .rp.SetParameterValue(4, " السعر :  " & txtprice.Text)
                    Else
                        .rp.SetParameterValue(4, "")
                    End If
                End With


                Dim prs As New PrinterSettings
                '---------------------------------------------
                Dim pageSettings As New PageSettings(prs)
                Dim paperSize As New PaperSize("Custom", 150, 100)
                pageSettings.PaperSize = paperSize
                PrintDoc.PrinterSettings = prs
                PrintDoc.DefaultPageSettings = pageSettings


                '---------------------------------------------
                If PrinterState(Barcode_DefPrinter_Cm.Text) Then
                    'Dim prs As New PrinterSettings
                    If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Barcode_DefPrinter_Cm.Text))
                    prs.PrinterName = Barcode_DefPrinter_Cm.Text
                    prs.Copies = nud_Count.Text
                    pp.rp.PrintToPrinter(prs, pageSettings, False)
                    pp.rp.Clone()
                    pp.rp.Dispose()
                Else
                    MsgBox("الطابعة الافتراضية لورق الباركود" & vbNewLine & "***" & Barcode_DefPrinter_Cm.Text & "***" & vbNewLine & " غير متصلة")
                    Dim pd_PrintDialog As New PrintDialog
                    pd_PrintDialog.ShowDialog()
                    pp.rp.PrintToPrinter(pd_PrintDialog.PrinterSettings, pageSettings, False)
                    pp.rp.Clone()
                    pp.rp.Dispose()
                End If
            End If

            Save_AppSetting()

            nud_Count.Text = 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub


    'Public Sub printbarcode_KeyDown_1(sender As Object, e As KeyEventArgs) Handles txtbarcode.KeyDown
    'If e.KeyCode = Keys.Enter Then
    '    If IsBarcode(txtbarcode.Text) Then
    '        Dim item As New items
    '        If IsRepeatedItem(txtbarcode.Text) Then

    '            Dim si As New selectitemRepeatedBarcode
    '            si.Location = New Point((txtbarcode.PointToScreen(Point.Empty).X + (txtbarcode.Size.Width / 2)) - (si.Size.Width / 2), (txtbarcode.PointToScreen(Point.Empty).Y + txtbarcode.Size.Height + 1))
    '            si.refreshgrid()
    '            si.txtsearch.Text = txtbarcode.Text
    '            si.ShowDialog()
    '            If si.select_flag Then
    '                item = si.Getitem
    '            Else
    '                Exit Sub
    '            End If
    '        Else
    '            item = GetItemBybarcodesp(txtbarcode.Text)
    '        End If
    '        TextBox1.Text = item.product & " - " & item.sell
    '    End If
    'End If
    'End Sub

    Public Sub Button2_Click_1(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Function CheckValue() As Boolean
        Try
            If txtbarcode.Text = "" Then
                MessageBox.Show("الباركو فارغ", "تنويه", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtbarcode.Focus()
                PictureBox1.Image = Nothing
                Return False
            End If
            If hieght.Value < 1 Then
                MessageBox.Show("الارتفاع يجب ان يكون اكبر من واحد", "تنويه", MessageBoxButtons.OK, MessageBoxIcon.Error)
                hieght.Focus()
                PictureBox1.Image = Nothing
                Return False
            End If
            If width.Value < 1 Then
                MessageBox.Show("العرض يجب ان يكون اكبر من واحد", "تنويه", MessageBoxButtons.OK, MessageBoxIcon.Error)
                width.Focus()
                PictureBox1.Image = Nothing
                Return False
            End If
            If MarginPUP.Value < 0.1 Then
                MessageBox.Show("محاذاة الورقة من اعلى لايمكن ان تكون اقل من 0.1 سنتيمتر", "تنويه", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MarginPUP.Focus()
                PictureBox1.Image = Nothing
                Return False
            End If
            If MarginPLeft.Value < 0.1 Then
                MessageBox.Show("محاذاة الورقة من اليسار لايمكن ان تكون اقل من 0.1 سنتيمتر", "تنويه", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MarginPLeft.Focus()
                PictureBox1.Image = Nothing
                Return False
            End If
            If MarginBUp.Value >= (hieght.Value / 2) Then
                MessageBox.Show("محاذاة ورقة الباركو د من اعلى لا يمكن ان تكون اكبر من نصف ورقة الباركود", "تنويه", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MarginBUp.Focus()
                PictureBox1.Image = Nothing
                Return False
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 0
    End Function

    Private Sub Showing_Click(sender As Object, e As EventArgs) Handles Showing.Click
        Try
            If CheckValue() Then

                If txtbarcode.Text = "" Then 'الباركود فارغ 
                    PictureBox1.Image = Nothing
                    PictureBox1.Invalidate()
                Else
                    Try
                        PictureBox1.Image = Code128(txtbarcode.Text, "A", IM_SH_txt.Text, MarginPUP.Value, MarginPLeft.Value, MarginBUp.Value, 0, width.Value, hieght.Value)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub



    Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If Get_Unit = True Then IM_Fetch_QTY()
    End Sub


    Private Sub cbA4_CheckedChanged(sender As Object, e As EventArgs) Handles cbA4.CheckedChanged
        CB_CHecked(sender)
        If cbA4.Checked = True Then
            A4GroupBox.ForeColor = Color.DarkGreen
        Else
            A4GroupBox.ForeColor = Color.Black
        End If
        txtcol.Enabled = cbA4.Checked
        txtrow.Enabled = cbA4.Checked
        Label15.Enabled = cbA4.Checked
        Label8.Enabled = cbA4.Checked
        GroupBox4.Enabled = cbA4.Checked
        My_Settings.BarcodeCheckA4 = cbA4.Checked
        Save_AppSetting()

    End Sub

    Private Sub select_item_Click(sender As Object, e As EventArgs)
        'Try
        '    Try
        '        Dim sc As New selectitem
        '        '  MsgBox(txtitem.PointToScreen(Point.Empty).X & "--" & txtitem.PointToScreen(Point.Empty).Y)
        '        sc.Location = New Point((txtbarcode.PointToScreen(Point.Empty).X + (txtbarcode.Size.Width / 2)) - (sc.Size.Width / 2), (txtbarcode.PointToScreen(Point.Empty).Y + txtbarcode.Size.Height + 1))
        '        sc.ShowDialog()
        '        If sc.select_flag Then
        '            Dim item As New items
        '            item = sc.Getitem
        '            txtbarcode.Text = item.barcode
        '            TextBox1.Text = item.product
        '            txtprice.Text = item.sell
        '            txtbarcode.Focus()
        '        Else

        '        End If
        '    Catch ex As Exception
        '        Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        '    End Try

        'Catch ex As Exception
        '    Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        'End Try
    End Sub

    Private Sub Saveformat_Click(sender As Object, e As EventArgs) Handles Saveformat.Click
        Try
            My_Settings.BarcodeWidth = width.Value
            My_Settings.BarcodeHieght = hieght.Value
            My_Settings.BarcodeNumber = nud_Count.Text
            My_Settings.BarcodePUp = MarginPUP.Value
            My_Settings.BarcodePLeft = MarginPLeft.Value
            My_Settings.BarcodeBUp = MarginBUp.Value
            My_Settings.BarcodeCheckA4 = cbA4.Checked
            My_Settings.BarcodeColumn = txtcol.Value
            My_Settings.BarcodeRows = txtrow.Value
            Save_AppSetting()
            MsgBox("تم الحفظ", MsgBoxStyle.Information, "")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Print_CPName_cb_CheckedChanged(sender As Object, e As EventArgs) Handles Print_CPName_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.ShowCname = Print_CPName_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub nud_Count_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nud_Count.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub printbarcode_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub


    Private Sub IM_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_SH_txt.KeyDown
        If e.KeyCode = Keys.Return Then Fetch_ItemToList()
        If e.KeyCode = Keys.Delete Then IM_SH_txt.Clear()
    End Sub

    Private Sub Print_Price_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Print_Price_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.ShowIM_Price_On_Barcode = Print_Price_CB.Checked
        Save_AppSetting()
    End Sub


    Private Sub Print_IM_NAME_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Print_IM_NAME_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.ShowIM_IM_NAME_On_Barcode = Print_IM_NAME_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub PRINT_IM_NUM_CB_CheckedChanged(sender As Object, e As EventArgs) Handles PRINT_IM_NUM_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.ShowIM_IM_NUM_On_Barcode = PRINT_IM_NUM_CB.Checked
        Save_AppSetting()
    End Sub
End Class