Public Class TB_BillIM
    Public SB_Bill_DayNum As Integer
    Public SB_ID As Integer
    Public Pure As Double
    Private Sub BillIM_Grid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BillIM_Grid.CellContentClick
        Dim Def As Integer
        Dim Desc As String = ""
        If BillIM_Grid.SelectedRows.Count > 0 Then
            Select Case e.ColumnIndex
                Case 0
                    Def = 1
                    Desc = "زيادة عدد 1 :" & vbNewLine & BillIM_Grid.CurrentRow.Cells("IM_Name_CL").Value & vbNewLine & F_TablesBalance.TB_Info.Text
                    Network_Edit_Tracker_insert(Desc, SB_ID.ToString, 1, 3)

                    Change_IM_Qty(Def)

                Case 1

                    If BillIM_Grid.CurrentRow.Cells("TB_IM_QTY_CL").Value >= 2 Then
                        Def = -1
                        Desc = "نقص عدد 1 :" & vbNewLine & BillIM_Grid.CurrentRow.Cells("IM_Name_CL").Value & vbNewLine & F_TablesBalance.TB_Info.Text
                        Network_Edit_Tracker_insert("نقص عدد 1 للصنف " + BillIM_Grid.CurrentRow.Cells("IM_Name_CL").Value + " - " + F_TablesBalance.TB_Info.Text, SB_ID.ToString, 1, 3)

                        Change_IM_Qty(Def)

                    End If

                Case 2
                    Desc = "مسح :" & vbNewLine & BillIM_Grid.CurrentRow.Cells("IM_Name_CL").Value & vbNewLine & "العدد :" + BillIM_Grid.CurrentRow.Cells("TB_IM_QTY_CL").Value.ToString & vbNewLine & F_TablesBalance.TB_Info.Text
                    Network_Edit_Tracker_insert(Desc, SB_ID.ToString, 1, 3)
                    SB_Contents_Delete_IM()

            End Select
        End If
    End Sub

    Public Sub Select_IM()
        BillIM_Grid.Visible = True
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Contents_TableItems_SELECT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", F_TablesBalance.Bill_T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        BillIM_Grid.DataSource = C.Dt
    End Sub

    Private Sub Change_IM_Qty(def As Integer)
        Dim Row_Index As Integer = BillIM_Grid.CurrentCell.RowIndex
        Dim ITEM_NAME As String = BillIM_Grid.CurrentRow.Cells("IM_Name_CL").Value
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Contents_Change_TB_IM_Qty"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", BillIM_Grid.CurrentRow.Cells("Bill_T_ID_CL").Value)
            .Parameters.AddWithValue("@T_ID", BillIM_Grid.CurrentRow.Cells("T_ID_3_CL").Value)
            .Parameters.AddWithValue("@Def", def)
            '.Parameters.AddWithValue("@Desc", Desc)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            F_TablesBalance.Refresh_TB_Balance()
            BillIM_Grid.CurrentCell = BillIM_Grid.Rows(Row_Index).Cells(1)

            If (KshPrint = True Or S_TB_Auto_Print = True) And BillIM_Grid.CurrentRow.Cells("Ptr_ID_CL").Value > 1 Then
                Dim pp As New ReportConnection
                pp.rp.Load(Application.StartupPath & "\reports\SB_Update_TB.rpt")
                pp.LoadTables()
                With pp
                    If def > 0 Then
                        .rp.SetParameterValue(0, "إضافــة" + vbNewLine + F_TablesBalance.TB_Info.Text +
                       vbNewLine + ITEM_NAME + vbNewLine _
                     + " العدد : 1")
                    ElseIf def < 0 Then
                        .rp.SetParameterValue(0, "إلغـــاء" + vbNewLine + F_TablesBalance.TB_Info.Text +
                       vbNewLine + ITEM_NAME + vbNewLine _
                     + " العدد : 1")

                    End If

                End With

                If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", BillIM_Grid.CurrentRow.Cells("Printer_Name_CL").Value))
                pp.rp.PrintOptions.PrinterName = BillIM_Grid.CurrentRow.Cells("Printer_Name_CL").Value
                pp.rp.PrintToPrinter(1, False, 0, 0)
                pp.rp.Dispose()
            End If

        End If
    End Sub

    Private Sub SB_Contents_Delete_IM()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Contents_Delete_TB_IM"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", BillIM_Grid.CurrentRow.Cells("Bill_T_ID_CL").Value)
            .Parameters.AddWithValue("@T_ID", BillIM_Grid.CurrentRow.Cells("T_ID_3_CL").Value)
            '.Parameters.AddWithValue("@Desc", Desc)
        End With
        If SQL_SP_EXEC(c.Com) = True Then

            If KshPrint = True And BillIM_Grid.CurrentRow.Cells("Ptr_ID_CL").Value > 1 Then
                Dim pp As New ReportConnection
                pp.rp.Load(Application.StartupPath & "\reports\SB_Cancel.rpt")
                pp.LoadTables()
                With pp
                    .rp.SetParameterValue(0, F_TablesBalance.TB_Info.Text +
                                          vbNewLine + BillIM_Grid.CurrentRow.Cells("IM_Name_CL").Value + vbNewLine +
                                        " العدد : " + BillIM_Grid.CurrentRow.Cells("TB_IM_QTY_CL").Value.ToString)
                End With

                If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", BillIM_Grid.CurrentRow.Cells("Printer_Name_CL").Value.ToString))
                pp.rp.PrintOptions.PrinterName = BillIM_Grid.CurrentRow.Cells("Printer_Name_CL").Value.ToString
                pp.rp.PrintToPrinter(1, False, 0, 0)
                pp.rp.Dispose()
            End If

            F_TablesBalance.Refresh_TB_Balance()
        End If
    End Sub


End Class