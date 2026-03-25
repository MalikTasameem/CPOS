Public Class ShowBill_IM
    Public T_ID As Integer

    Private Sub ShowBill_IM_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub ShowBill_IM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Bill_Types()
        'T_ID = F_BillsMenu.BillsGrid.CurrentRow.Cells("T_ID_CL").Value
        'BillNumTxt.Text = F_BillsMenu.BillsGrid.CurrentRow.Cells("B_Pr_ID_CL").Value
        SB_Contents_SELECT_Bill()
        Fill_Bill_Info()
        'BillTypeCmb.SelectedValue = F_BillsMenu.BillsGrid.CurrentRow.Cells("B_Type_CL").Value
        If U_SalesVoid = False Then
            VoidBillBtn.Visible = False
        Else
            VoidBillBtn.Visible = True
        End If
    End Sub

    Private Sub SB_Contents_SELECT_Bill()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Contents_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        MetroGrid.DataSource = C.Dt
    End Sub

    Private Sub Load_Bill_Types()
        Dim c As New C
        c.Str = "select B_Type_ID,B_Name from Sales_Bills_Types"
        c.Da = New SqlClient.SqlDataAdapter(c.Str, c.Con)
        c.Da.Fill(c.Dt)
        BillTypeCmb.DataSource = c.Dt
        BillTypeCmb.DisplayMember = "B_Name"
        BillTypeCmb.ValueMember = "B_Type_ID"
    End Sub

    Private Sub Fill_Bill_Info()

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
            ' Barcode = C.Dr("Barcode")
            If C.Dr("isDepended") = 1 Then
                MetroGrid.BackgroundColor = Color.LightGreen
                MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            Else
                MetroGrid.BackgroundColor = Color.LightYellow
                MetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow

            End If


            If C.Dr("isVoid") = True Then
                VoidBillBtn.Enabled = False
                MetroGrid.BackgroundColor = Color.IndianRed
                MetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed
            Else
                VoidBillBtn.Enabled = True
            End If
        End If
        C.Con.Close()
    End Sub

    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        Me.Close()
    End Sub

    Private Sub VoidBillBtn_Click(sender As Object, e As EventArgs) Handles VoidBillBtn.Click
        Beep()
        If MessageBox.Show(" إلغــاء الفاتورة " + BillNumTxt.Text + " بشكل نهــائي  ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) _
             = Windows.Forms.DialogResult.OK Then
            SB_VoidBill()
        End If
    End Sub

    Private Sub SB_VoidBill()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Void_Row"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            If SB_AutoPrint = True Then SB_Report_Cacnel()
            Fill_Bill_Info()
        End If
    End Sub

    Private Sub SB_Report_Cacnel()
        Dim C As New C
        Dim Prt_Path As String
        Dim Prt_ID As Integer
        C.Con.Open()
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_C_By_Printers_SELECT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@B_T_ID", Me.T_ID)
        End With
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            While C.Dr.Read()
                Prt_ID = C.Dr("Prt_ID")
                Prt_Path = C.Dr("Prt_Path")
                Dim pp As New ReportConnection
                pp.rp.Load(Application.StartupPath & "\reports\SB_Cancel.rpt")
                pp.LoadTables()
                With pp
                    .rp.SetParameterValue(0, BillNumTxt.Text)
                End With
                If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", C.Dr("Prt_Path")))
                pp.rp.PrintOptions.PrinterName = C.Dr("Prt_Path")
                pp.rp.PrintToPrinter(1, False, 0, 0)
                pp.rp.Dispose()
            End While
        End If
        C.Con.Close()
    End Sub


End Class