Public Class BillNotes
    Dim rs As New Resizer
    Public T_ID As Integer
    Private Sub BillNotes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        rs.FindAllControls(Me)
        Select_About()
        Phone_Or_Name_txt.Select()
        'Make_Hints()
    End Sub

    'Private Sub Make_Hints()
    '    SendMessage(Phone_Or_Name_txt.Handle, &H1501, 0, "الهاتف أو اسم الزبون")
    '    SendMessage(Notes_txt.Handle, &H1501, 0, "ملاحظات")
    'End Sub
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, TopTitle_LB.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, TopTitle_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, TopTitle_LB.MouseUp
        drag = False
    End Sub
    Public Sub Select_About()
        Dim C As New C
        Dim S As String = "Select ISNULL(About,'') AS About , ISNULL(Cr_Phone,'') AS Cr_Phone  From Agents_Balance_MV Where T_ID = '" & T_ID & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Notes_txt.Text = C.Dr("About")
                Phone_Or_Name_txt.Text = C.Dr("Cr_Phone")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Private Sub BillNotes_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Select_AG_btn_Click(sender As Object, e As EventArgs) Handles Select_AG_btn.Click
        Save_About(T_ID, Notes_txt.Text)
        AG_Balance_Update_Cr_Phone(T_ID, Phone_Or_Name_txt.Text)

        If Application.OpenForms().OfType(Of POS).Any Then
            F_POS.Cr_Phone = Phone_Or_Name_txt.Text
            F_POS.Bill_Note = Notes_txt.Text
        End If


        If Application.OpenForms().OfType(Of Sales_Fast).Any Then Sales_Fast.Notes_txt.Text = Notes_txt.Text
        Me.Close()
    End Sub

    Private Sub KeyBoard_Btn_Click(sender As Object, e As EventArgs) Handles KeyBoard_Btn.Click
        Call_KeyBoard()
    End Sub
End Class