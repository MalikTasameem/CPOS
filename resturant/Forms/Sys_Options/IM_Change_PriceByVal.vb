Public Class IM_Change_PriceByVal

    Dim Get_COUNTER As Boolean = False
    Private Sub Add_Unit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Add_Unit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        fetch_GM()
        Load_GM_IM()
    End Sub


    Private Sub Load_GM_IM()
        Try
            Get_COUNTER = False
            Dim c As New C
            Dim s As String = ""
            If GM_Serach.SelectedValue = 0 Then
                s = "select U_IM_ID,item_name,U_Name,Price FROM IM_Menu_Units_V"
            Else
                s = "select U_IM_ID,item_name,U_Name,Price FROM IM_Menu_Units_V WHERE GM_ID = '" & GM_Serach.SelectedValue & "'"
            End If

            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            IM_GV.DataSource = c.Dt

            Get_COUNTER = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Get_COUNTER = False
        End Try
    End Sub


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

    Private Sub InsertU_btn_Click(sender As Object, e As EventArgs) Handles InsertU_btn.Click


        If String.IsNullOrWhiteSpace(Val_TXT.Text) Then
            MsgBox("حدد القيمة")
            Val_TXT.Select()
            Exit Sub
        End If

        Dim delta As Decimal
        If Not Decimal.TryParse(Val_TXT.Text, delta) Then
            MessageBox.Show("قيمة التعديل غير صالحة.", "تعديل الأسعار", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If delta < 0 Then
            If MessageBox.Show(" خصم قيمة " & Val_TXT.Text & " دينار من قائمة " & vbNewLine & GM_Serach.Text, "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        ElseIf delta > 0 Then
            If MessageBox.Show(" زيادة قيمة " & Val_TXT.Text & " دينار من قائمة " & vbNewLine & GM_Serach.Text, "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Dim prices As New System.Collections.Generic.List(Of System.Collections.Generic.KeyValuePair(Of Integer, Decimal))
        Try
            For Each row As DataGridViewRow In IM_GV.Rows
                If row.IsNewRow Then Continue For
                Dim price As Decimal = If(IsDBNull(row.Cells("Price_CL").Value) OrElse row.Cells("Price_CL").Value Is Nothing, 0D, Convert.ToDecimal(row.Cells("Price_CL").Value))
                prices.Add(New System.Collections.Generic.KeyValuePair(Of Integer, Decimal)(Convert.ToInt32(row.Cells("U_IM_ID_CL").Value), price + delta))
            Next
        Catch ex As Exception
            MessageBox.Show("لم تبدأ الدفعة؛ تعذر قراءة أسعار القائمة. " & ex.Message, "تعديل الأسعار", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End Try
        Dim saved As Integer = 0
        For Each price In prices
            If Not IM_Units_UpdatePrice(price.Key, price.Value) Then
                MessageBox.Show("توقفت الدفعة. عدد الصفوف المحفوظة: " & saved.ToString() & ". لم تنفذ الصفوف التالية. حدّث القائمة قبل المتابعة؛ إعادة تطبيق الزيادة على القائمة كاملة تؤثر مجددًا في الصفوف المحفوظة.", "تعديل الأسعار", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit For
            End If
            saved += 1
        Next
        Load_GM_IM()
    End Sub


    Private Sub Back_ADD_New_IM_btn_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub DataGridViewX1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles IM_GV.CellEndEdit
        If e.RowIndex < 0 OrElse IM_GV.CurrentRow Is Nothing OrElse IM_GV.CurrentRow.IsNewRow Then Return
        Dim price As Decimal
        Dim text = Convert.ToString(IM_GV.CurrentRow.Cells("Price_CL").Value)
        If Not String.IsNullOrWhiteSpace(text) AndAlso Not Decimal.TryParse(text, price) Then
            MessageBox.Show("السعر غير صالح؛ لم يتم حفظه.", "تعديل الأسعار", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Load_GM_IM() : Return
        End If
        IM_Units_UpdatePrice(Convert.ToInt32(IM_GV.CurrentRow.Cells("U_IM_ID_CL").Value), price)
        Load_GM_IM()
    End Sub

    Private Function IM_Units_UpdatePrice(U_IM_ID As Integer, Price As Decimal) As Boolean
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "[IM_Units_UpdatePriceBy_Val]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@U_IM_ID", U_IM_ID)
            .Parameters.AddWithValue("@Price", Price)
        End With
        Return AuditItemUnitsUi.Run(Me, c.Com)
    End Function

    Private Sub GM_Serach_SelectedValueChanged(sender As Object, e As EventArgs) Handles GM_Serach.SelectedValueChanged
        If Get_COUNTER = True Then Load_GM_IM()
    End Sub

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles IM_GV.EditingControlShowing

        If Me.IM_GV.CurrentCell.ColumnIndex = 3 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            'AddHandler tb.KeyDown, AddressOf TextBox_KeyDown
            AddHandler tb.KeyPress, AddressOf TextBox_KeyPress
        End If
    End Sub

    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Check_Only_Float(sender, e)
    End Sub

End Class
