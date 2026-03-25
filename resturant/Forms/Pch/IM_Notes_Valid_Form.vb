Imports System.Data.SqlClient

Public Class IM_Notes_Valid_Form
    Dim IM_DT As New DataTable
    Dim rs As New Resizer
    Private Sub IM_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized

        TypeComboBox.Items.Add("عرض حسب الفترة المحددة" & "(" & IM_Day_Valid & ") يوم ")
        TypeComboBox.Items.Add("عرض القائمة كاملة")

        TypeComboBox.SelectedIndex = 0
        DateTimePicker_From.Value = DateTimePicker_From.Value.AddDays(IM_Day_Valid)
        Fill_ALL_IM()
        '  Check_View_Control()
        Make_Hints()
    End Sub

    Private Sub Check_View_Control()
        ' Me.DataGridViewX.Columns("GM_Name_CL").Visible = My_Settings.ST_GM_Name
        Me.DataGridViewX.Columns("ST_Name_CL").Visible = My_Settings.ST_STNAME
        '  Me.DataGridViewX.Columns("Last_Pch_Price").Visible = My_Settings.ST_Last_Pch_Price
        '   Me.DataGridViewX.Columns("D_Valid_CL").Visible = My_Settings.ST_Valid
        Me.DataGridViewX.Columns("IM_Num_CL").Visible = My_Settings.ST_IM_Num
    End Sub

    Private Sub Make_Hints()
        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن صنف")
    End Sub

    Public Sub Fill_ALL_IM()
        Dim C As New C
        IM_DT.Clear()
        With (C.Com)
            .Connection = C.Con
            .CommandText = "[IM_Notes_Valid_V_SELECT]"
            .CommandType = CommandType.StoredProcedure
            If TypeComboBox.SelectedIndex = 0 Then
                .Parameters.AddWithValue("@END_Date_Valid", IM_Day_Valid)
            Else
                .Parameters.AddWithValue("@END_Date_Valid", 9999999)
            End If

        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(IM_DT)
        DataGridViewX.DataSource = IM_DT
        Get_Total_QYT()
    End Sub



    Private Sub Get_Total_QYT()
        ' Dim Cost As Double = 0
        ' Dim Qty As Double = 0
        'Dim DATE1 As Date
        For i = 0 To DataGridViewX.Rows.Count - 1


            Select Case DataGridViewX.Rows(i).Cells("Days_Left_CL").Value

                Case Is <= 0
                    DataGridViewX.Rows(i).DefaultCellStyle.BackColor = Color.IndianRed
                    DataGridViewX.Rows(i).Cells("Case_CL").Value = "صلاحية منتهية"
                Case Is <= 30
                    DataGridViewX.Rows(i).DefaultCellStyle.BackColor = Color.Orange
                    DataGridViewX.Rows(i).Cells("Case_CL").Value = " ينتهي بعد " + DataGridViewX.Rows(i).Cells("Days_Left_CL").Value.ToString + " يوم "
                Case Is <= 45
                    DataGridViewX.Rows(i).DefaultCellStyle.BackColor = Color.Yellow
                    DataGridViewX.Rows(i).Cells("Case_CL").Value = " ينتهي بعد " + DataGridViewX.Rows(i).Cells("Days_Left_CL").Value.ToString + " يوم "

                Case Else
                    DataGridViewX.Rows(i).Cells("Case_CL").Value = " ينتهي بعد " + DataGridViewX.Rows(i).Cells("Days_Left_CL").Value.ToString + " يوم "
            End Select

            '   Cost += DataGridViewX.Rows(i).Cells("Cost_CL").Value
            '   Qty += DataGridViewX.Rows(i).Cells("QTY_CL").Value
            ' DATE1 = DataGridViewX.Rows(i).Cells("D_Valid_CL").Value
            'If Date.Now.Date >= DATE1 Then
            '    DataGridViewX.Rows(i).DefaultCellStyle.BackColor = Color.IndianRed
            '    DataGridViewX.Rows(i).Cells("Case_CL").Value = "صلاحية منتهية"
            'Else
            '    Dim DaysStayed As Int32 = DATE1.Subtract(Date.Now.Date).Days

            '    If DaysStayed < 10 Then
            '        DataGridViewX.Rows(i).DefaultCellStyle.BackColor = Color.Orange
            '    Else
            '        DataGridViewX.Rows(i).DefaultCellStyle.BackColor = Color.Yellow
            '    End If


            ' End If
        Next
        'TotalQYT_txt.Text = Qty.ToString("N")
        'TotalCost_txt.Text = Cost.ToString("N")
    End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged
        Dim Dv As DataView
        Dv = IM_DT.AsDataView
        Dv.RowFilter = "item_name LIKE '%" + sender.Text + "%'"
        DataGridViewX.DataSource = Dv

        Get_Total_QYT()
    End Sub

    Private Sub IM_Valid_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub



    Private Sub IM_Notes_Valid_Form_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'F_MainForm.Fill_ALL_IM()
        Me.Dispose()
    End Sub

    Private Sub DataGridViewX_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewX.CellContentClick
        If e.ColumnIndex = 1 Then
            Beep()
            If MessageBox.Show(" حذف إشعار الصنف " & DataGridViewX.CurrentRow.Cells("item_name_CL").Value, "إلغــاء فاتورة", MessageBoxButtons.OKCancel, _
                   MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                Valid_Notes_DELETE()
            End If

        End If
    End Sub


    Public Sub Valid_Notes_DELETE()
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[Valid_Notes_DELETE]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", DataGridViewX.CurrentRow.Cells("T_ID_CL").Value)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Dim Row_Index As Integer = DataGridViewX.CurrentCell.RowIndex
            Fill_ALL_IM()
            If Row_Index > 0 Then DataGridViewX.CurrentCell = DataGridViewX.Rows(Row_Index - 1).Cells("item_name_CL")
        End If
    End Sub

    Private Sub TypeComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TypeComboBox.SelectedIndexChanged
        Fill_ALL_IM()
    End Sub


    Private Sub Add_Valid_Btn_Click_1(sender As Object, e As EventArgs) Handles Add_Valid_Btn.Click
        ADD_IM_TO_Valid_Notes.ShowDialog()
    End Sub

End Class