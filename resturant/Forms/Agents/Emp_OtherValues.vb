Imports System.Data.SqlClient
Public Class Emp_OtherValues

    Dim AG_NAME As String
    Dim T_ID As Integer = 0
    Dim EditState As String = ""
    Dim Emp_Cse_Dt As New DataTable

    Private Sub Sign_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub


    Private Sub Sign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        ThemeManager.ApplyThemeToForm(Me)
        For i = 0 To DataGridView1.Columns.Count - 1
            DataGridView1.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i
        AG_Cm.Focus()
        EditState = UPDATE_btn.Text
        StateComboBox.SelectedIndex = 0
    End Sub
    Public Sub fetch_EMP_Sign()
        Try
            Emp_Cse_Dt.Clear()
            Dim C As New C
            Dim s As String = "select T_ID,DateOF_Order,UserName,Type_Name,Value,isVoid,ValType  from Emp_OtherVal_V  WHERE AG_ID = '" & AG_Cm.TXT_ID.Text & "' AND isVoid = 0  ORDER BY DateOF_Order DESC"
            C.Da = New SqlDataAdapter(s, C.Con)
            C.Da.Fill(Emp_Cse_Dt)
            DataGridView1.DataSource = Emp_Cse_Dt
            Index_GV()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        If T_ID = 0 Then
            If AG_Cm.TXT_ID.Text = 0 Then
                MsgBox("حدد الموظف", MsgBoxStyle.Exclamation, "خطأ فالإدراج")
                AG_Cm.Focus()
            Else
                Sign_Insert()
            End If
        End If
    End Sub

    Sub Sign_Insert()

   Dim C As New C

        With C.Com
            .CommandText = "[Others_Values_Insert]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@DateOF_Order", DateTimePickerDATE.Value)
            .Parameters.AddWithValue("@YEAR", DateTimePickerDATE.Value.Year)
            .Parameters.AddWithValue("@MONTH", DateTimePickerDATE.Value.Month)
            .Parameters.AddWithValue("@AG_ID", AG_Cm.TXT_ID.Text)
            .Parameters.AddWithValue("@ValType", StateComboBox.SelectedIndex)
            .Parameters.AddWithValue("@Value", Val_txt.Text)
            .Parameters.AddWithValue("@About", NOTES_TXT.Text)
            .Parameters.AddWithValue("@User_ID", USER_ID)
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            Network_Edit_Tracker_insert(" الموظف:" & AG_Cm.Textt & " التاريخ:" & DateTimePickerDATE.Text & " النوع:" & StateComboBox.Text _
                                        & " القيمة:" & Val_txt.Text & " ملاحظات:" & NOTES_TXT.Text, 0, 32, 1)

            Disble_Fields()
            fetch_EMP_Sign()
            SaveButton.Enabled = False
        End If

    End Sub


    Sub Others_Values_Void()
        Dim C As New C

        With C.Com
            .CommandText = "[Others_Values_Void]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Network_Edit_Tracker_insert(" الموظف:" & AG_NAME & " التاريخ:" & DateTimePickerDATE.Text & " النوع:" & StateComboBox.Text _
                                        & " القيمة:" & Val_txt.Text & " ملاحظات:" & NOTES_TXT.Text, 0, 32, 2)

            'Network_Edit_Tracker_insert(" إلغاء حالة  " + DataGridView1.CurrentRow.Cells("Stuts_Name_CL").Value + " للموظف : " + IM_SH_txt.Text + " / " + Start_txt.Text, DataGridView1.CurrentRow.Cells("Val_CL").Value, 0, 0)
            Disble_Fields()
            fetch_EMP_Sign()
        End If
    End Sub

    Private Sub New_Btn_Click(sender As Object, e As EventArgs) Handles New_Btn.Click
        clearing()
        Enable_Fields()
        AG_Cm.Focus()
        SaveButton.Enabled = True
    End Sub

    Private Sub Enable_Fields()
        Panel1.Enabled = True
        AG_Cm.Enabled = True
    End Sub

    Private Sub Disble_Fields()
        Panel1.Enabled = False
        AG_Cm.Enabled = False
    End Sub

    Public Sub clearing()
        For Each a As Control In Me.Panel1.Controls
            If TypeOf a Is TextBox Then
                a.Text = Nothing
            End If
        Next
        DateTimePickerDATE.Value = Date.Now
        T_ID = 0
        AG_Cm.Textt = ""
        VoidLb.Visible = False
        Emp_Cse_Dt.Clear()
    End Sub

    Private Sub UPDATE_btn_Click(sender As Object, e As EventArgs) Handles UPDATE_btn.Click
        If UPDATE_btn.Text = EditState Then
            UPDATE_btn.Text = "تأكيد التعديلات"
            Enable_Fields()
            'Network_Edit_Tracker_insert(" تعديل حالة  " + StateComboBox.Text + " للموظف : " + IM_SH_txt.Text + " / " + Start_txt.Text, Val_txt.Text, 0, 0)
        Else
            Sign_UPDATE()
        End If
    End Sub



    Sub Sign_UPDATE()

        Dim C As New C

        With C.Com
            .CommandText = "[Others_Values_Update]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
            .Parameters.AddWithValue("@DateOF_Order", DateTimePickerDATE.Value)
            .Parameters.AddWithValue("@YEAR", DateTimePickerDATE.Value.Year)
            .Parameters.AddWithValue("@MONTH", DateTimePickerDATE.Value.Month)
            .Parameters.AddWithValue("@AG_ID", AG_Cm.TXT_ID.Text)
            .Parameters.AddWithValue("@ValType", StateComboBox.SelectedIndex)
            .Parameters.AddWithValue("@Value", Val_txt.Text)
            .Parameters.AddWithValue("@About", NOTES_TXT.Text)
            .Parameters.AddWithValue("@User_ID", USER_ID)
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            Network_Edit_Tracker_insert(" الإسم السابق:" & AG_NAME & " الموظف:" & AG_Cm.Textt & " التاريخ:" & DateTimePickerDATE.Text & " النوع:" & StateComboBox.Text _
                                       & " القيمة:" & Val_txt.Text & " ملاحظات:" & NOTES_TXT.Text, 0, 32, 3)
            Panel1.Enabled = False
            fetch_EMP_Sign()
            UPDATE_btn.Text = EditState
        End If

    End Sub

    Private Sub DataGridView1_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseClick
        Select_Emp()
    End Sub

    Public Sub Select_Emp()
        If DataGridView1.SelectedRows.Count < 1 Then
            Exit Sub
        End If
        Dim c As New C
        Dim s As String = "select * from Emp_OtherVal_V where T_ID = '" & DataGridView1.CurrentRow.Cells("T_ID_CL").Value & "'"

        Dim com As New SqlCommand(s, c.Con)

        c.Con.Open()
        Try
            c.Dr = com.ExecuteReader
            If c.Dr.HasRows Then

                c.Dr.Read()
                T_ID = c.Dr("T_ID")

                AG_Cm.Set_IM_By_ID(c.Dr("AG_ID"))

                DateTimePickerDATE.Text = c.Dr("DateOF_Order")
                StateComboBox.SelectedIndex = c.Dr("ValType")
                Val_txt.Text = c.Dr("Value")

                NOTES_TXT.Text = c.Dr("About")

                Start_txt.Text = "إدخال : " & c.Dr("UserName") + " -- " & c.Dr("Date").ToString

                If DataGridView1.CurrentRow.Cells("is_Void_CL").Value = 0 Then
                    VoidLb.Visible = False
                    Panel1.Enabled = False
                    UPDATE_btn.Enabled = True
                    Cancel_Btn.Enabled = True
                    SaveButton.Enabled = False
                Else
                    UPDATE_btn.Enabled = False
                    Cancel_Btn.Enabled = False
                    VoidLb.Visible = True
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()
    End Sub


    Private Sub Index_GV()
        For i = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Cells("DX").Value = i + 1
            If DataGridView1.Rows(i).Cells("ValType_CL").Value = 0 Then
                DataGridView1.Rows(i).Cells("Stuts_Name_CL").Style.ForeColor = Color.LightSeaGreen
            Else
                DataGridView1.Rows(i).Cells("Stuts_Name_CL").Style.ForeColor = Color.IndianRed
            End If
        Next
    End Sub


    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub Cancel_Btn_Click(sender As Object, e As EventArgs) Handles Cancel_Btn.Click
        If DataGridView1.Rows.Count > 0 Then
            If MessageBox.Show("إلغــاء الحالة بشكل نهائــي ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) _
                = Windows.Forms.DialogResult.Yes Then Others_Values_Void()
        End If
    End Sub

    Private Sub Val_txt_TextChanged(sender As Object, e As EventArgs) Handles Val_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Val_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Val_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        fetch_EMP_Sign()
    End Sub
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    ' ==========================================
    ' 🌟 أكواد تحريك الفورم باستخدام الماوس 🌟
    ' ==========================================
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    ' 1. عند الضغط على زر الماوس فوق الشريط العلوي أو العنوان
    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, TopTitle_LB.MouseDown
        If e.Button = MouseButtons.Left Then
            drag = True
            mouseX = Cursor.Position.X - Me.Left
            mouseY = Cursor.Position.Y - Me.Top
        End If
    End Sub

    ' 2. أثناء سحب الماوس
    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, TopTitle_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    ' 3. عند إفلات زر الماوس
    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, TopTitle_LB.MouseUp
        drag = False
    End Sub

    Private Sub HeaderCloseBtn_Click(sender As Object, e As EventArgs) Handles HeaderCloseBtn.Click
        Me.Close()
    End Sub
End Class