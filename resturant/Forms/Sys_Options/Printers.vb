Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Text.RegularExpressions

Public Class Printers
    Public Pr_ID As Integer
    Public Pr_Name As String = ""
    Private Sub NewEmpButton_Click(sender As Object, e As EventArgs) Handles NewEmpButton.Click
        FieldsPanel.Enabled = True
        SaveButton.Enabled = True
        EditEmpButton.Enabled = False
        DeleteButton.Enabled = False
        PrintersPage_Clear_Fields()
        PrNameTextBox.Select()
        EditEmpButton.Text = "تعديل"
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        If ValidateChildren() = True Then
            Printer_Insert()
            PrintersPage_Clear_Fields()
            Load_PrinterNames()
        End If
    End Sub

    Public Sub PrintersPage_Clear_Fields()
        PrNameTextBox.Clear()
        PrPathComboBox.SelectedIndex = -1
        Printer_IP_Txt.Clear()
    End Sub

    Private Sub EditEmpButton_Click(sender As Object, e As EventArgs) Handles EditEmpButton.Click
        If EditEmpButton.Text = "تعديل" Then
            FieldsPanel.Enabled = True
            DeleteButton.Enabled = False
            SaveButton.Enabled = False
            EditEmpButton.Text = "تأكيد التعديل"
            EditEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        Else
            If ValidateChildren() = True Then
                Printer_Update()
                Load_PrinterNames()
                PrintersPage_Clear_Fields()
                FieldsPanel.Enabled = False
                EditEmpButton.Text = "تعديل"
                EditEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 11, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If
        End If
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        If MessageBox.Show(" تأكيد حذف الطابعة " + Pr_Name, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
            Printer_Delete()
        End If
    End Sub

    Public Sub Printer_Insert()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Printers_Insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("Printer_Name", Me.PrNameTextBox.Text)
            .Parameters.AddWithValue("Printer_Path", Me.PrPathComboBox.Text)
            .Parameters.AddWithValue("Printer_IP", Me.Printer_IP_Txt.Text)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تـم إضافة الطابعة ", MsgBoxStyle.Information)
        End If
    End Sub

    Public Sub Printer_Update()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Printers_Update"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("Printer_ID", Me.Pr_ID)
            .Parameters.AddWithValue("Printer_Name", Me.PrNameTextBox.Text)
            .Parameters.AddWithValue("Printer_Path", Me.PrPathComboBox.Text)
            .Parameters.AddWithValue("Printer_IP", Me.Printer_IP_Txt.Text)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تـــم تعديل الطابعة ", MsgBoxStyle.Information)
        End If
    End Sub

    Public Sub Printer_Delete()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Printers_Delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("Printer_ID", Me.Pr_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تـــم حــذف الطابعة ", MsgBoxStyle.Information)
            Load_PrinterNames()
            FieldsPanel.Enabled = False
            DeleteButton.Enabled = False
            SaveButton.Enabled = False
            EditEmpButton.Enabled = False
        End If
    End Sub

    Public Sub Load_PrinterNames()
        Dim c As New C
        Dim s As String = "select Printer_ID,Printer_Name from Printers WHERE Printer_ID > 1"
        c.Da = New SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        Me.Pr_listBox.DataSource = dt
        Me.Pr_listBox.DisplayMember = "Printer_Name"
        Me.Pr_listBox.ValueMember = "Printer_ID"
    End Sub

    Public Sub Select_Printer()
        Dim c As New C
        Dim s As String = "select * from Printers where Printer_ID ='" & Me.Pr_listBox.SelectedValue & "'"
        c.Com = New SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()

                PrintersPage_Clear_Fields()

                Me.Pr_ID = c.Dr("Printer_ID")
                Me.Pr_Name = c.Dr("Printer_Name")

                Me.PrNameTextBox.Text = c.Dr("Printer_Name")
                Me.PrPathComboBox.Text = c.Dr("Printer_Path")
                Me.Printer_IP_Txt.Text = c.Dr("Printer_IP")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()
    End Sub

    Private Sub Pr_listBox_Click(sender As Object, e As EventArgs) Handles Pr_listBox.Click
        Select_Printer()
        DeleteButton.Enabled = True
        EditEmpButton.Enabled = True
        FieldsPanel.Enabled = False
    End Sub


    Private Sub Printers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_PrinterNames()
        LoadComputerPrinters()


        'Printer_IP_Txt.Location = New Point(20, 20)
        'Printer_IP_Txt.Width = 200
        'Me.Controls.Add(ipTextBox)
    End Sub

    Private Sub ipTextBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Printer_IP_Txt.Validating
        If Not String.IsNullOrWhiteSpace(Printer_IP_Txt.Text) Then

            Dim ipPattern As String = "^(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)){3}$"
            If Not Regex.IsMatch(Printer_IP_Txt.Text.Trim(), ipPattern) Then
                MessageBox.Show("الرجاء إدخال عنوان IP صحيح (مثال: 192.168.1.1)", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                e.Cancel = True
            End If

        End If

    End Sub

    Private Sub LoadComputerPrinters()
        Dim pkInstalledPrinters As String
        ' Find all printers installed
        For Each pkInstalledPrinters In _
            PrinterSettings.InstalledPrinters
            PrPathComboBox.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters
        ' Set the combo to the first printer in the list
        PrPathComboBox.SelectedIndex = -1
    End Sub

    Private Sub PrNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PrNameTextBox.TextChanged
        PrNameErrorProvider.Clear()
    End Sub

    Private Sub PrPathComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PrPathComboBox.SelectedIndexChanged
        PrPathErrorProvider.Clear()
    End Sub

    Private Sub PrNameTextBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PrNameTextBox.Validating
        If String.IsNullOrWhiteSpace(sender.text) Then
            PrNameErrorProvider.SetError(sender, "أدخل اسم الطابعة")
            e.Cancel = True
        Else
            PrNameErrorProvider.Clear()
        End If
    End Sub

    Private Sub PrPathComboBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PrPathComboBox.Validating
        If String.IsNullOrWhiteSpace(sender.text) Then
            PrPathErrorProvider.SetError(sender, "أدخل مسار الطابعة")
            e.Cancel = True
        Else
            PrPathErrorProvider.Clear()
        End If
    End Sub

    Private Sub Printers_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub
End Class