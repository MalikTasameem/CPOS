Imports System.Data.SqlClient

Public Class Units
    Dim Un_ID As Integer
    Dim Un_Name As String

    Public Sub PrintersPage_Clear_Fields()
        Un_Name_txt.Clear()
        Un_Cargo_txt.Clear()
    End Sub

    Public Sub Printer_Insert()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Unit_Insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@U_ID", 0)
            .Parameters.AddWithValue("@U_Name", Me.Un_Name_txt.Text)
            If String.IsNullOrWhiteSpace(Un_Cargo_txt.Text) = False Then
                If Convert.ToDouble(Un_Cargo_txt.Text) = 0 Then
                    .Parameters.AddWithValue("@U_Cargo", 1)
                Else
                    .Parameters.AddWithValue("@U_Cargo", Un_Cargo_txt.Text)
                End If
            Else
                .Parameters.AddWithValue("@U_Cargo", 1)
            End If

        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تـم إضافة الوحدة ", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" الوحدة:" & Un_Name_txt.Text & " الحمولة:" & Un_Cargo_txt.Text, 0, 22, 1)
            PrintersPage_Clear_Fields()
            Load_PrinterNames()
            Un_Name_txt.Select()
        End If
    End Sub

    Public Sub Printer_Update()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Unit_Update"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@U_ID", Me.Un_ID)
            .Parameters.AddWithValue("@U_Name", Me.Un_Name_txt.Text)
            If String.IsNullOrWhiteSpace(Un_Cargo_txt.Text) = False Then
                If Convert.ToDouble(Un_Cargo_txt.Text) = 0 Then
                    .Parameters.AddWithValue("@U_Cargo", 1)
                Else
                    .Parameters.AddWithValue("@U_Cargo", Un_Cargo_txt.Text)
                End If
            Else
                .Parameters.AddWithValue("@U_Cargo", 1)
            End If
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تـــم تعديل الوحدة ", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" الإسم السابق:" & Un_Name & " الوحدة:" & Un_Name_txt.Text & " الحمولة:" & Un_Cargo_txt.Text, 0, 22, 3)
            Load_PrinterNames()
            PrintersPage_Clear_Fields()
            FieldsPanel.Enabled = False
            EditEmpButton.Text = "تعديل"
            EditEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 11, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub

    Public Sub Printer_Delete()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Unit_Delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@U_ID", Me.Un_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تـــم حــذف الوحدة ", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" الوحدة:" & Un_Name, 0, 22, 2)
            Load_PrinterNames()
            FieldsPanel.Enabled = False
            DeleteButton.Enabled = False
            SaveButton.Enabled = False
            EditEmpButton.Enabled = False
        End If
    End Sub

    Public Sub Load_PrinterNames()
        Dim c As New C
        Dim s As String = "select U_ID,U_Name from Units_V WHERE U_ID > 1"
        c.Da = New SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        Me.Un_listBox.DataSource = dt
        Me.Un_listBox.DisplayMember = "U_Name"
        Me.Un_listBox.ValueMember = "U_ID"
    End Sub

    Public Sub Select_Printer()
        Dim c As New C
        Dim s As String = "select * from Units where U_ID ='" & Me.Un_listBox.SelectedValue & "'"
        c.Com = New SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                Me.Un_ID = c.Dr("U_ID")
                Me.Un_Name = c.Dr("U_Name")

                Me.Un_Name_txt.Text = c.Dr("U_Name")
                Me.Un_Cargo_txt.Text = c.Dr("U_Cargo")

                DeleteButton.Enabled = True
                EditEmpButton.Enabled = True
                SaveButton.Enabled = False
                FieldsPanel.Enabled = False
                EditEmpButton.Text = "تعديل"
                If Me.Un_ID = 2 Then
                    EditEmpButton.Enabled = False
                    DeleteButton.Enabled = False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()
    End Sub



    Private Sub Un_listBox_Click(sender As Object, e As EventArgs) Handles Un_listBox.Click
        Select_Printer()
    End Sub

    Private Sub Units_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_PrinterNames()
    End Sub

    Private Sub NewEmpButton_Click(sender As Object, e As EventArgs) Handles NewEmpButton.Click
        FieldsPanel.Enabled = True
        SaveButton.Enabled = True
        EditEmpButton.Enabled = False
        DeleteButton.Enabled = False
        PrintersPage_Clear_Fields()
        Un_Name_txt.Select()
        EditEmpButton.Text = "تعديل"
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        If ValidateChildren() = True Then
            Printer_Insert()
        End If
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

            End If
        End If
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        If MessageBox.Show(" تأكيد حذف الوحدة " + Un_Name, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
            Printer_Delete()
        End If
    End Sub

    Private Sub Un_Cargo_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Un_Cargo_txt.KeyDown
        If e.KeyCode = Keys.Return And SaveButton.Enabled = True Then
            If MessageBox.Show("حفظ الوحدة ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                SaveButton_Click(sender, e)
            End If
        End If

        If e.KeyCode = Keys.Return And EditEmpButton.Enabled = True Then EditEmpButton_Click(sender, e)

    End Sub

    Private Sub Un_Cargo_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Un_Cargo_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Un_Cargo_txt_TextChanged(sender As Object, e As EventArgs) Handles Un_Cargo_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Un_Name_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Un_Name_txt.KeyDown
        If e.KeyCode = Keys.Return Then Un_Cargo_txt.Select()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Units_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
End Class