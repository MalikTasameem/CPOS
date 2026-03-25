Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Public Class Frm_DashboardButtons
    Dim Current_Button_ID As Integer = 0

    Private Sub Frm_DashboardButtons_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        'تعبئة قائمة الرموز بأيقونات رائعة
        Dim Symbols() As String = {"🛒", "📦", "🏷️", "🍽️", "💰", "📊", "↩️", "📤", "🏢", "📈", "⚙️", "👥", "📝", "🚚", "🔐", "⭐"}
        cmbSymbolChar.Items.AddRange(Symbols)
        Load_Buttons()
    End Sub
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property
    ' =========================================================
    ' تحريك الشاشة والإغلاق (Frameless)
    ' =========================================================
    Dim drag As Boolean, mouseX As Integer, mouseY As Integer
    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, Title_Label.MouseDown
        drag = True : mouseX = Cursor.Position.X - Me.Left : mouseY = Cursor.Position.Y - Me.Top
    End Sub
    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, Title_Label.MouseMove
        If drag Then Me.Location = New Point(Cursor.Position.X - mouseX, Cursor.Position.Y - mouseY)
    End Sub
    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, Title_Label.MouseUp
        drag = False
    End Sub
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    ' =========================================================
    ' اختيار اللون للزر
    ' =========================================================
    Private Sub btnBackColor_Click(sender As Object, e As EventArgs) Handles btnBackColor.Click
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            Dim c As Color = ColorDialog1.Color
            txtBackColor.Text = c.R & ", " & c.G & ", " & c.B
            btnBackColor.BackColor = c
        End If
    End Sub

    ' =========================================================
    ' جلب البيانات
    ' =========================================================
    Private Sub Load_Buttons()
        Try
            Dim db As New C()
            db.Str = "SELECT Button_ID, Button_Title AS [عنوان الزر], Action_Key AS [كود الإجراء], Symbol_Char AS [الرمز], Is_Global_Active AS [ظاهر؟], Default_Sort_Order AS [الترتيب] FROM Sys_Dashboard_Buttons_List ORDER BY Default_Sort_Order ASC"
            db.Da = New SqlDataAdapter(db.Str, db.Con)
            Dim dt As New DataTable()
            db.Da.Fill(dt)
            DGV_Buttons.DataSource = dt
            DGV_Buttons.Columns("Button_ID").Visible = False
        Catch ex As Exception
            MsgBox("خطأ في تحميل الأزرار: " & ex.Message)
        End Try
    End Sub

    ' =========================================================
    ' عند النقر على الجدول لعرض التفاصيل
    ' =========================================================
    Private Sub DGV_Buttons_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Buttons.CellClick
        If e.RowIndex >= 0 Then
            Current_Button_ID = DGV_Buttons.Rows(e.RowIndex).Cells("Button_ID").Value

            Dim db As New C()
            db.Str = "SELECT * FROM Sys_Dashboard_Buttons_List WHERE Button_ID = " & Current_Button_ID
            db.Com = New SqlCommand(db.Str, db.Con)
            db.Con.Open()
            db.Dr = db.Com.ExecuteReader()

            If db.Dr.HasRows Then
                db.Dr.Read()
                txtButtonTitle.Text = db.Dr("Button_Title").ToString()
                txtActionKey.Text = db.Dr("Action_Key").ToString()
                cmbSymbolChar.Text = db.Dr("Symbol_Char").ToString()
                txtReqPermission.Text = db.Dr("Req_Permission").ToString()
                txtDefaultImage.Text = db.Dr("Default_Image").ToString()

                If Not IsDBNull(db.Dr("Default_Sort_Order")) Then numSortOrder.Value = Convert.ToInt32(db.Dr("Default_Sort_Order"))
                chkIsActive.Checked = Convert.ToBoolean(db.Dr("Is_Global_Active"))

                ' جلب لون الزر
                Dim bg As String = db.Dr("Button_BackColor").ToString()
                txtBackColor.Text = bg
                If Not String.IsNullOrWhiteSpace(bg) Then
                    Dim pts() As String = bg.Split(","c)
                    If pts.Length = 3 Then btnBackColor.BackColor = Color.FromArgb(CInt(pts(0).Trim()), CInt(pts(1).Trim()), CInt(pts(2).Trim()))
                End If
            End If
            db.Con.Close()
        End If
    End Sub

    ' =========================================================
    ' حفظ (تحديث) التعديلات
    ' =========================================================
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Current_Button_ID = 0 Then
            MsgBox("يرجى اختيار زر من الجدول لتعديله!", MsgBoxStyle.Exclamation) : Exit Sub
        End If
        If txtButtonTitle.Text.Trim = "" Then
            MsgBox("يرجى كتابة عنوان للزر!", MsgBoxStyle.Exclamation) : Exit Sub
        End If

        Try
            Dim db As New C()
            db.Con.Open()
            db.Str = "UPDATE Sys_Dashboard_Buttons_List SET Button_Title=@Title, Symbol_Char=@Sym, Is_Global_Active=@Active, Default_Sort_Order=@Sort, Default_Image=@Img, Button_BackColor=@BgColor WHERE Button_ID=@ID"

            db.Com = New SqlCommand(db.Str, db.Con)
            db.Com.Parameters.AddWithValue("@Title", txtButtonTitle.Text)
            db.Com.Parameters.AddWithValue("@Sym", cmbSymbolChar.Text)
            db.Com.Parameters.AddWithValue("@Active", If(chkIsActive.Checked, 1, 0))
            db.Com.Parameters.AddWithValue("@Sort", numSortOrder.Value)
            db.Com.Parameters.AddWithValue("@Img", txtDefaultImage.Text)
            db.Com.Parameters.AddWithValue("@BgColor", txtBackColor.Text)
            db.Com.Parameters.AddWithValue("@ID", Current_Button_ID)

            db.Com.ExecuteNonQuery()
            db.Con.Close()

            MsgBox("تم تحديث وتخصيص الزر بنجاح!", MsgBoxStyle.Information)
            Load_Buttons()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class