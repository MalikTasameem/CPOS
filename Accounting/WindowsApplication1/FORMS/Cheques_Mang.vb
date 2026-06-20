Public Class Cheques_Mang

    Public T_ID As Integer

    Private Sub Cheques_Mang_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Load_Balances()

        If Not IsDBNull(F_Cheques_Form.DataGridView1.CurrentRow.Cells("تاريخ الإصدار").Value) Then
            issueDate.Checked = True
            issueDate.Text = F_Cheques_Form.DataGridView1.CurrentRow.Cells("تاريخ الإصدار").Value
        Else
            issueDate.Checked = False
        End If

        If Not IsDBNull(F_Cheques_Form.DataGridView1.CurrentRow.Cells("تاريخ الاستحقاق").Value) Then
            dueDate.Checked = True
            dueDate.Text = F_Cheques_Form.DataGridView1.CurrentRow.Cells("تاريخ الاستحقاق").Value
        Else
            dueDate.Checked = False
        End If

        If Not IsDBNull(F_Cheques_Form.DataGridView1.CurrentRow.Cells("تاريخ المطابقة").Value) Then
            reconciliationDate.Checked = True
            reconciliationDate.Text = F_Cheques_Form.DataGridView1.CurrentRow.Cells("تاريخ المطابقة").Value
        Else
            reconciliationDate.Checked = False
        End If


        If Not IsDBNull(F_Cheques_Form.DataGridView1.CurrentRow.Cells("رقم الحركة البنكية").Value) Then bankTransactionNumber.Text = F_Cheques_Form.DataGridView1.CurrentRow.Cells("رقم الحركة البنكية").Value
        If Not IsDBNull(F_Cheques_Form.DataGridView1.CurrentRow.Cells("ملاحظات").Value) Then notes.Text = F_Cheques_Form.DataGridView1.CurrentRow.Cells("ملاحظات").Value

        Cheque_Type_CM.SelectedValue = F_Cheques_Form.DataGridView1.CurrentRow.Cells("StatusId").Value


        Label_info.Text = Label_info.Text &
            vbNewLine & " نوع الصك : " & F_Cheques_Form.DataGridView1.CurrentRow.Cells("نوع الشيك").Value _
            & vbNewLine & " رقم الصك : " & F_Cheques_Form.DataGridView1.CurrentRow.Cells("رقم الشيك").Value _
        & vbNewLine & " الحساب : " & F_Cheques_Form.DataGridView1.CurrentRow.Cells("الحساب").Value _
         & vbNewLine & " قيمة الصك : " & F_Cheques_Form.DataGridView1.CurrentRow.Cells("قيمة الشيك").Value


        If F_Cheques_Form.DataGridView1.CurrentRow.Cells("IsFinal").Value = True Then
            Panel1.Enabled = False
            CONFIRM_BTN.Enabled = False
        End If




    End Sub

    Public ChequeStatuses_DT As New DataTable

    'Public Sub Load_Balances()

    '    Dim C As New C
    '    Dim da As New SqlClient.SqlDataAdapter(" select StatusId , NameAr ,BackColorHex from ChequeStatuses ", C.Con)
    '    da.Fill(ChequeStatuses_DT)

    '    Cheque_Type_CM.DataSource = ChequeStatuses_DT
    '    Cheque_Type_CM.DisplayMember = "NameAr"
    '    Cheque_Type_CM.ValueMember = "StatusId"
    'End Sub
    '-------------------------------------------------------------------------------------------------------------------------------
    Public Sub Load_Balances()
        Dim C As New C
        Dim da As New SqlClient.SqlDataAdapter("SELECT StatusId, NameAr, BackColorHex FROM ChequeStatuses", C.Con)
        da.Fill(ChequeStatuses_DT)

        Cheque_Type_CM.DataSource = ChequeStatuses_DT
        Cheque_Type_CM.DisplayMember = "NameAr"
        Cheque_Type_CM.ValueMember = "StatusId"

        ' تمكين الرسم اليدوي لكل عنصر
        Cheque_Type_CM.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler Cheque_Type_CM.DrawItem, AddressOf Cheque_Type_CM_DrawItem
    End Sub

    Private Sub Cheque_Type_CM_DrawItem(sender As Object, e As DrawItemEventArgs)
        If e.Index < 0 Then Return

        Dim combo As ComboBox = CType(sender, ComboBox)
        Dim drv As DataRowView = CType(combo.Items(e.Index), DataRowView)

        ' الحصول على اللون من العمود BackColorHex
        Dim colorHex As String = drv("BackColorHex").ToString()
        Dim backColor As Color = Color.White
        Try
            backColor = ColorTranslator.FromHtml(colorHex)
        Catch ex As Exception
            ' في حال كان اللون غير صالح
            backColor = Color.White
        End Try

        ' رسم الخلفية
        e.DrawBackground()
        Using brush As New SolidBrush(backColor)
            e.Graphics.FillRectangle(brush, e.Bounds)
        End Using

        ' رسم النص
        Dim textColor As Color = If(backColor.GetBrightness() < 0.5, Color.White, Color.Black)
        TextRenderer.DrawText(e.Graphics, drv("NameAr").ToString(), e.Font, e.Bounds, textColor, TextFormatFlags.Left)

        e.DrawFocusRectangle()
    End Sub



    '-------------------------------------------------------------------------------------------------------------------------------


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles CONFIRM_BTN.Click
        Dim msg As String = Nothing
        Dim ok As Boolean = ChequeRepository.UpdateCheque(
            connectionString:=MY_Settings.SqlConStr,
            chequeId:=T_ID,
        issueDate:=If(issueDate.Checked, New Nullable(Of Date)(issueDate.Value), Nothing),
    dueDate:=If(dueDate.Checked, New Nullable(Of Date)(dueDate.Value), Nothing),
    reconciliationDate:=If(reconciliationDate.Checked, New Nullable(Of Date)(reconciliationDate.Value), Nothing),
        bankTransactionNumber:=bankTransactionNumber.Text,
            notes:=notes.Text,
            statusId:=Cheque_Type_CM.SelectedValue,                                 ' 0=قيد الانتظار
            autoSetReconciliationIfFinal:=False,
            message:=msg
        )

        If ok Then
            MsgBox(msg, MsgBoxStyle.Information, "نجاح")
            'MessageBox.Show(msg, "نجاح")
            F_Cheques_Form.Cheque_SELECT()
            Me.Close()
        Else
            'MessageBox.Show(msg, "فشل")
            MsgBox(msg, MsgBoxStyle.Information, "فشل")
        End If



        'issueDate:=If(issueDate.Checked, issueDate.Value, Nothing),               'New Nullable(Of Date)(New Date(2025, 7, 15)),
        'dueDate:=If(dueDate.Checked, dueDate.Value, Nothing),                                                                   'New Nullable(Of Date)(New Date(2025, 8, 1)),
        'reconciliationDate:=If(reconciliationDate.Checked, reconciliationDate.Value, Nothing),                 ' أو New Nullable(Of Date)(New Date(...))

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub


End Class