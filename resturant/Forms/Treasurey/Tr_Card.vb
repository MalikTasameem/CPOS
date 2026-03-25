


Public Class Tr_Card

    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Dim TR_ID As Integer
    Dim TR_Name As String = ""
    Dim TR_DT As New DataTable

    Private Sub TreasuryCard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub TreasuryCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        'rs.FindAllControls(Me)
        EditState = Edit_butt.Text
        DefaultFormState = Me.Text
        Load_AG()
    End Sub

    Private Sub NewStateBt()
        FieldsPanel.Enabled = True
        Save_butt.Enabled = True
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = "خزينة جديــــدة"
        Tr_Name_txtb.Select()
    End Sub
    Private Sub DeleteOrUpdateStateBt()
        FieldsPanel.Enabled = False
        Save_butt.Enabled = False
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Private Sub SavedStateBt()
        FieldsPanel.Enabled = False
        Save_butt.Enabled = False
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Private Sub SelectStateBt()
        FieldsPanel.Enabled = False
        Save_butt.Enabled = False
        Edit_butt.Enabled = True
        Delete_butt.Enabled = True
        Me.Text = "بيانات الخزينــة : " + TR_Name

    End Sub

    Private Sub ClearFields()
        Tr_Name_txtb.Clear()
        Tr_BalanceTextBox.Clear()
        Tr_BankNum_TextBox.Clear()
        Edit_butt.Text = EditState
        ACC_CODE_TXT.Clear()
        ' Me.Text = FormState
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        ClearFields()
        NewStateBt()
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        If ValidateChildren() = True Then
            Insert_New_TR()
            Load_AG()
            ClearFields()
            SavedStateBt()
        End If
    End Sub

    Private Sub Insert_New_TR()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "TreasuryCard_Insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Tr_Name", Tr_Name_txtb.Text)
        sqlComm.Parameters.AddWithValue("@Tr_AccountNumber", Tr_BankNum_TextBox.Text)
        '  sqlComm.Parameters.AddWithValue("@Tree_Code", ACC_CODE_TXT.Text)
        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تمت إضافة الخزينـــة", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Update_AG()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "TreasuryCard_Update"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Tr_ID", TR_ID)
        sqlComm.Parameters.AddWithValue("@Tr_Name", Tr_Name_txtb.Text)
        sqlComm.Parameters.AddWithValue("@Tr_AccountNumber", Tr_BankNum_TextBox.Text)
        sqlComm.Parameters.AddWithValue("@Tree_Code", ACC_CODE_TXT.Text)
        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم تعديل بيانات الخزينـــة", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Delete_AG()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "TreasuryCard_Delete"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Tr_ID", TR_ID)
        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم حـــذف بيانات الخزينـــة", MsgBoxStyle.Information)
            ClearFields()
            DeleteOrUpdateStateBt()
            Load_AG()
        End If

    End Sub

    Private Sub Load_AG()
        Try
            Dim C As New C
            TR_DT.Clear()
            Dim sql As String = " select Tr_ID,Tr_name,T_Balance from TR_MENU_V Order By Tr_ID ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(TR_DT)
            S_listBox.DataSource = TR_DT
            S_listBox.DisplayMember = "Tr_name"
            S_listBox.ValueMember = "Tr_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Calc_BS()

    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If Edit_butt.Text = EditState Then
            Edit_butt.Text = "تأكيد التعديلات"
            FieldsPanel.Enabled = True
        Else
            If ValidateChildren() = True Then
                Edit_butt.Text = EditState
                Update_AG()
                ClearFields()
                DeleteOrUpdateStateBt()
                Load_AG()
            End If

        End If
    End Sub

    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click

        If TR_ID = 1 Then
            MsgBox("لا يمكن حذف الخزينة اللإفتراضية للنظام", MsgBoxStyle.Exclamation, "حذف خزينة")
        Else
            Beep()
            If MessageBox.Show(" سيتم حذف الخزينـــة " + TR_Name + " نهائيا من النظام ... متأكد ", "حذف خزينة", MessageBoxButtons.OKCancel, _
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                Delete_AG()
            End If
        End If
    End Sub



    Public Sub Select_AG()
        Tr_BalanceTextBox.Clear()
        Dim C As New C
        Dim BS As Double
        Dim S As String = "Select * FROM TR_MENU_V Where TR_ID = '" & S_listBox.SelectedValue & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                TR_ID = C.Dr("TR_ID")
                Tr_Name_txtb.Text = C.Dr("TR_Name")
                TR_Name = C.Dr("TR_Name")
                Tr_BankNum_TextBox.Text = C.Dr("Tr_AccountNumber")
                If Not IsDBNull(C.Dr("T_Balance")) Then
                    BS = C.Dr("T_Balance")
                    Tr_BalanceTextBox.Text = BS.ToString("n")
                End If
                If Not IsDBNull(C.Dr("Tree_Code")) Then
                    ACC_CODE_TXT.Text = C.Dr("Tree_Code")
                End If



                SelectStateBt()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Private Sub Tr_Name_txtb_TextChanged(sender As Object, e As EventArgs) Handles Tr_Name_txtb.TextChanged
        TR_NAMEErrorProvider.Clear()
    End Sub

    Private Sub Tr_Name_txtb_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Tr_Name_txtb.Validating
        If String.IsNullOrWhiteSpace(sender.Text) = True Then
            TR_NAMEErrorProvider.SetError(sender, " حدد إسم الخزينة ")
            e.Cancel = True
        Else
            e.Cancel = False
            TR_NAMEErrorProvider.Clear()
        End If
    End Sub


    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tr_BankNum_TextBox.KeyPress
        Check_Only_Int(sender, e)
    End Sub


    Private Sub Calc_BS()

        Dim C As New C
        Dim Sum As Double = 0
        Dim S As String = "Select ISNULL(SUM(T_BALANCE),0) AS S FROM TR_MENU_V"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Sum = C.Dr("S")
                If Sum < 0 Then
                    Total_BS_Txt.ForeColor = Color.DarkRed
                ElseIf Sum = 0 Then
                    Total_BS_Txt.ForeColor = Color.Black
                Else
                    Total_BS_Txt.ForeColor = Color.DarkGreen
                End If
                Total_BS_Txt.Text = Sum.ToString("n")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub



    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub S_listBox_MouseClick(sender As Object, e As MouseEventArgs) Handles S_listBox.MouseClick
        '   ClearFields()
        Select_AG()
    End Sub

End Class