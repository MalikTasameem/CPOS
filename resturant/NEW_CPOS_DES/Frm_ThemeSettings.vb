Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Public Class Frm_ThemeSettings
    Dim Current_Theme_ID As Integer = 0

    Private Sub Frm_ThemeSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        Load_Themes()
        btnNew.PerformClick()
    End Sub

    ' =========================================================
    ' 1. تحريك الشاشة والإغلاق (Frameless)
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
    Private Sub CloseBtn_Click(sender As Object, e As EventArgs) Handles CloseBtn.Click
        Me.Close()
    End Sub

    ' =========================================================
    ' 2. دالة مساعدة لاختيار الألوان باحترافية
    ' =========================================================
    Private Sub PickColor(txt As TextBox, btn As Button)
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            Dim c As Color = ColorDialog1.Color
            txt.Text = c.R & ", " & c.G & ", " & c.B
            btn.BackColor = c
        End If
    End Sub

    ' ربط الأزرار بدالة اختيار الألوان (الأساسيات)
    Private Sub btnFormBack_Click(sender As Object, e As EventArgs) Handles btnFormBack.Click
        PickColor(txtFormBack, btnFormBack) : End Sub
    Private Sub btnPanelBack_Click(sender As Object, e As EventArgs) Handles btnPanelBack.Click
        PickColor(txtPanelBack, btnPanelBack) : End Sub
    Private Sub btnTextPrimary_Click(sender As Object, e As EventArgs) Handles btnTextPrimary.Click
        PickColor(txtTextPrimary, btnTextPrimary) : End Sub
    Private Sub btnTextSecondary_Click(sender As Object, e As EventArgs) Handles btnTextSecondary.Click
        PickColor(txtTextSecondary, btnTextSecondary) : End Sub

    ' ربط الأزرار بدالة اختيار الألوان (هيكل الداشبورد الجديد)
    Private Sub btnSidebarBack_Click(sender As Object, e As EventArgs) Handles btnSidebarBack.Click
        PickColor(txtSidebarBack, btnSidebarBack) : End Sub
    Private Sub btnHeaderBack_Click(sender As Object, e As EventArgs) Handles btnHeaderBack.Click
        PickColor(txtHeaderBack, btnHeaderBack) : End Sub
    Private Sub btnCardBack_Click(sender As Object, e As EventArgs) Handles btnCardBack.Click
        PickColor(txtCardBack, btnCardBack) : End Sub

    ' ربط الأزرار بدالة اختيار الألوان (الأزرار العامة)
    Private Sub btnBtnSaveBack_Click(sender As Object, e As EventArgs) Handles btnBtnSaveBack.Click
        PickColor(txtBtnSaveBack, btnBtnSaveBack) : End Sub
    Private Sub btnBtnSaveFore_Click(sender As Object, e As EventArgs) Handles btnBtnSaveFore.Click
        PickColor(txtBtnSaveFore, btnBtnSaveFore) : End Sub
    Private Sub btnBtnDeleteBack_Click(sender As Object, e As EventArgs) Handles btnBtnDeleteBack.Click
        PickColor(txtBtnDeleteBack, btnBtnDeleteBack) : End Sub
    Private Sub btnBtnDeleteFore_Click(sender As Object, e As EventArgs) Handles btnBtnDeleteFore.Click
        PickColor(txtBtnDeleteFore, btnBtnDeleteFore) : End Sub
    Private Sub btnBtnPrintBack_Click(sender As Object, e As EventArgs) Handles btnBtnPrintBack.Click
        PickColor(txtBtnPrintBack, btnBtnPrintBack) : End Sub
    Private Sub btnBtnPrintFore_Click(sender As Object, e As EventArgs) Handles btnBtnPrintFore.Click
        PickColor(txtBtnPrintFore, btnBtnPrintFore) : End Sub
    Private Sub btnBtnGeneralBack_Click(sender As Object, e As EventArgs) Handles btnBtnGeneralBack.Click
        PickColor(txtBtnGeneralBack, btnBtnGeneralBack) : End Sub
    Private Sub btnBtnGeneralFore_Click(sender As Object, e As EventArgs) Handles btnBtnGeneralFore.Click
        PickColor(txtBtnGeneralFore, btnBtnGeneralFore) : End Sub

    ' ربط الأزرار بدالة اختيار الألوان (الفواتير ونقاط البيع POS)
    Private Sub btnPOSCatBack_Click(sender As Object, e As EventArgs) Handles btnPOSCatBack.Click
        PickColor(txtPOSCatBack, btnPOSCatBack) : End Sub
    Private Sub btnPOSCatFore_Click(sender As Object, e As EventArgs) Handles btnPOSCatFore.Click
        PickColor(txtPOSCatFore, btnPOSCatFore) : End Sub
    Private Sub btnPOSItemBack_Click(sender As Object, e As EventArgs) Handles btnPOSItemBack.Click
        PickColor(txtPOSItemBack, btnPOSItemBack) : End Sub
    Private Sub btnPOSItemFore_Click(sender As Object, e As EventArgs) Handles btnPOSItemFore.Click
        PickColor(txtPOSItemFore, btnPOSItemFore) : End Sub
    Private Sub btnNumpadBack_Click(sender As Object, e As EventArgs) Handles btnNumpadBack.Click
        PickColor(txtNumpadBack, btnNumpadBack) : End Sub
    Private Sub btnNumpadFore_Click(sender As Object, e As EventArgs) Handles btnNumpadFore.Click
        PickColor(txtNumpadFore, btnNumpadFore) : End Sub
    Private Sub btnTotalsBack_Click(sender As Object, e As EventArgs) Handles btnTotalsBack.Click
        PickColor(txtTotalsBack, btnTotalsBack) : End Sub
    Private Sub btnTotalsFore_Click(sender As Object, e As EventArgs) Handles btnTotalsFore.Click
        PickColor(txtTotalsFore, btnTotalsFore) : End Sub
    Private Sub btnAccent_Click(sender As Object, e As EventArgs) Handles btnAccent.Click
        PickColor(txtAccent, btnAccent) : End Sub

    ' ربط الأزرار بدالة اختيار الألوان (الجداول)
    Private Sub btnGridHeader_Click(sender As Object, e As EventArgs) Handles btnGridHeader.Click
        PickColor(txtGridHeader, btnGridHeader) : End Sub
    Private Sub btnGridRow_Click(sender As Object, e As EventArgs) Handles btnGridRow.Click
        PickColor(txtGridRow, btnGridRow) : End Sub
    Private Sub btnGridAltRow_Click(sender As Object, e As EventArgs) Handles btnGridAltRow.Click
        PickColor(txtGridAltRow, btnGridAltRow) : End Sub
    Private Sub btnGridSel_Click(sender As Object, e As EventArgs) Handles btnGridSel.Click
        PickColor(txtGridSel, btnGridSel) : End Sub

    ' =========================================================
    ' 3. عمليات قاعدة البيانات (جلب، حفظ، حذف)
    ' =========================================================
    Private Sub Load_Themes()
        Try
            Dim db As New C()
            db.Str = "SELECT Theme_ID, Theme_Name AS [اسم الثيم] FROM Sys_Themes"
            db.Da = New SqlDataAdapter(db.Str, db.Con)
            Dim dt As New DataTable()
            db.Da.Fill(dt)
            DGV_Themes.DataSource = dt
            DGV_Themes.Columns("Theme_ID").Visible = False
        Catch ex As Exception
            MsgBox("خطأ في تحميل الثيمات: " & ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Current_Theme_ID = 0
        txtThemeName.Clear()
        chkIsDark.Checked = False

        ' ألوان افتراضية تشمل الأساسيات والداشبورد
        txtFormBack.Text = "240, 242, 245" : txtPanelBack.Text = "255, 255, 255"
        txtTextPrimary.Text = "44, 62, 80" : txtTextSecondary.Text = "127, 140, 141"
        txtSidebarBack.Text = "44, 62, 80" : txtHeaderBack.Text = "255, 255, 255" : txtCardBack.Text = "255, 255, 255"

        ' ألوان الأزرار العامة
        txtBtnSaveBack.Text = "39, 174, 96" : txtBtnSaveFore.Text = "255, 255, 255"
        txtBtnDeleteBack.Text = "231, 76, 60" : txtBtnDeleteFore.Text = "255, 255, 255"
        txtBtnPrintBack.Text = "243, 156, 18" : txtBtnPrintFore.Text = "255, 255, 255"
        txtBtnGeneralBack.Text = "52, 152, 219" : txtBtnGeneralFore.Text = "255, 255, 255"

        ' ألوان الفواتير ونقاط البيع (الجديدة)
        txtPOSCatBack.Text = "52, 73, 94" : txtPOSCatFore.Text = "255, 255, 255"
        txtPOSItemBack.Text = "236, 240, 241" : txtPOSItemFore.Text = "44, 62, 80"
        txtNumpadBack.Text = "223, 230, 233" : txtNumpadFore.Text = "45, 52, 54"
        txtTotalsBack.Text = "45, 52, 54" : txtTotalsFore.Text = "46, 204, 113"
        txtAccent.Text = "231, 76, 60"

        ' ألوان الجداول
        txtGridHeader.Text = "236, 240, 241" : txtGridRow.Text = "255, 255, 255"
        txtGridAltRow.Text = "249, 249, 249" : txtGridSel.Text = "52, 152, 219"

        SetPreviewColors()
    End Sub


    Private Sub SetPreviewColors()
        ' تم تحديث المصفوفات لتشمل أدوات الداشبورد والفواتير (POS)
        Dim txts() As TextBox = {txtFormBack, txtPanelBack, txtTextPrimary, txtTextSecondary, txtSidebarBack, txtHeaderBack, txtCardBack, txtBtnSaveBack, txtBtnSaveFore, txtBtnDeleteBack, txtBtnDeleteFore, txtBtnPrintBack, txtBtnPrintFore, txtBtnGeneralBack, txtBtnGeneralFore, txtPOSCatBack, txtPOSCatFore, txtPOSItemBack, txtPOSItemFore, txtNumpadBack, txtNumpadFore, txtTotalsBack, txtTotalsFore, txtAccent, txtGridHeader, txtGridRow, txtGridAltRow, txtGridSel}
        Dim btns() As Button = {btnFormBack, btnPanelBack, btnTextPrimary, btnTextSecondary, btnSidebarBack, btnHeaderBack, btnCardBack, btnBtnSaveBack, btnBtnSaveFore, btnBtnDeleteBack, btnBtnDeleteFore, btnBtnPrintBack, btnBtnPrintFore, btnBtnGeneralBack, btnBtnGeneralFore, btnPOSCatBack, btnPOSCatFore, btnPOSItemBack, btnPOSItemFore, btnNumpadBack, btnNumpadFore, btnTotalsBack, btnTotalsFore, btnAccent, btnGridHeader, btnGridRow, btnGridAltRow, btnGridSel}

        For i = 0 To txts.Length - 1
            Try
                If txts(i).Text.Trim() <> "" And txts(i).Name <> "Serv_Desc_lb" Then
                    Dim pts() As String = txts(i).Text.Split(","c)
                    If pts.Length = 3 Then
                        btns(i).BackColor = Color.FromArgb(CInt(pts(0).Trim()), CInt(pts(1).Trim()), CInt(pts(2).Trim()))
                    End If
                End If
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtThemeName.Text.Trim = "" Then
            MsgBox("يرجى كتابة اسم الثيم!", MsgBoxStyle.Exclamation) : Exit Sub
        End If

        Try
            Dim db As New C()
            db.Con.Open()

            ' إضافة كل الحقول في جملة الإدخال والتحديث
            If Current_Theme_ID = 0 Then
                db.Str = "INSERT INTO Sys_Themes (Theme_Name, Is_Dark_Mode, Form_BackColor, Panel_BackColor, Text_Primary_Color, Text_Secondary_Color, Sidebar_BackColor, Header_BackColor, Card_BackColor, Btn_Save_BackColor, Btn_Save_ForeColor, Btn_Delete_BackColor, Btn_Delete_ForeColor, Btn_Print_BackColor, Btn_Print_ForeColor, Btn_General_BackColor, Btn_General_ForeColor, POS_Category_BackColor, POS_Category_ForeColor, POS_Item_BackColor, POS_Item_ForeColor, Numpad_BackColor, Numpad_ForeColor, Totals_Panel_BackColor, Totals_Panel_ForeColor, Accent_Color, Grid_Header_BackColor, Grid_Row_BackColor, Grid_AltRow_BackColor, Grid_Selection_Color) VALUES (@TName, @Dark, @FB, @PB, @TP, @TS, @SB, @HB, @CB, @BSB, @BSF, @BDB, @BDF, @BPB, @BPF, @BGB, @BGF, @PCB, @PCF, @PIB, @PIF, @NB, @NF, @TB, @TF, @ACC, @GH, @GR, @GA, @GS)"
            Else
                db.Str = "UPDATE Sys_Themes SET Theme_Name=@TName, Is_Dark_Mode=@Dark, Form_BackColor=@FB, Panel_BackColor=@PB, Text_Primary_Color=@TP, Text_Secondary_Color=@TS, Sidebar_BackColor=@SB, Header_BackColor=@HB, Card_BackColor=@CB, Btn_Save_BackColor=@BSB, Btn_Save_ForeColor=@BSF, Btn_Delete_BackColor=@BDB, Btn_Delete_ForeColor=@BDF, Btn_Print_BackColor=@BPB, Btn_Print_ForeColor=@BPF, Btn_General_BackColor=@BGB, Btn_General_ForeColor=@BGF, POS_Category_BackColor=@PCB, POS_Category_ForeColor=@PCF, POS_Item_BackColor=@PIB, POS_Item_ForeColor=@PIF, Numpad_BackColor=@NB, Numpad_ForeColor=@NF, Totals_Panel_BackColor=@TB, Totals_Panel_ForeColor=@TF, Accent_Color=@ACC, Grid_Header_BackColor=@GH, Grid_Row_BackColor=@GR, Grid_AltRow_BackColor=@GA, Grid_Selection_Color=@GS WHERE Theme_ID=@ID"
            End If

            db.Com = New SqlCommand(db.Str, db.Con)
            db.Com.Parameters.AddWithValue("@TName", txtThemeName.Text)
            db.Com.Parameters.AddWithValue("@Dark", If(chkIsDark.Checked, 1, 0))
            db.Com.Parameters.AddWithValue("@FB", txtFormBack.Text)
            db.Com.Parameters.AddWithValue("@PB", txtPanelBack.Text)
            db.Com.Parameters.AddWithValue("@TP", txtTextPrimary.Text)
            db.Com.Parameters.AddWithValue("@TS", txtTextSecondary.Text)

            ' بارامترات الداشبورد
            db.Com.Parameters.AddWithValue("@SB", txtSidebarBack.Text)
            db.Com.Parameters.AddWithValue("@HB", txtHeaderBack.Text)
            db.Com.Parameters.AddWithValue("@CB", txtCardBack.Text)

            ' بارامترات الأزرار العامة
            db.Com.Parameters.AddWithValue("@BSB", txtBtnSaveBack.Text)
            db.Com.Parameters.AddWithValue("@BSF", txtBtnSaveFore.Text)
            db.Com.Parameters.AddWithValue("@BDB", txtBtnDeleteBack.Text)
            db.Com.Parameters.AddWithValue("@BDF", txtBtnDeleteFore.Text)
            db.Com.Parameters.AddWithValue("@BPB", txtBtnPrintBack.Text)
            db.Com.Parameters.AddWithValue("@BPF", txtBtnPrintFore.Text)
            db.Com.Parameters.AddWithValue("@BGB", txtBtnGeneralBack.Text)
            db.Com.Parameters.AddWithValue("@BGF", txtBtnGeneralFore.Text)

            ' بارامترات الفواتير ونقاط البيع
            db.Com.Parameters.AddWithValue("@PCB", txtPOSCatBack.Text)
            db.Com.Parameters.AddWithValue("@PCF", txtPOSCatFore.Text)
            db.Com.Parameters.AddWithValue("@PIB", txtPOSItemBack.Text)
            db.Com.Parameters.AddWithValue("@PIF", txtPOSItemFore.Text)
            db.Com.Parameters.AddWithValue("@NB", txtNumpadBack.Text)
            db.Com.Parameters.AddWithValue("@NF", txtNumpadFore.Text)
            db.Com.Parameters.AddWithValue("@TB", txtTotalsBack.Text)
            db.Com.Parameters.AddWithValue("@TF", txtTotalsFore.Text)
            db.Com.Parameters.AddWithValue("@ACC", txtAccent.Text)

            ' بارامترات الجداول
            db.Com.Parameters.AddWithValue("@GH", txtGridHeader.Text)
            db.Com.Parameters.AddWithValue("@GR", txtGridRow.Text)
            db.Com.Parameters.AddWithValue("@GA", txtGridAltRow.Text)
            db.Com.Parameters.AddWithValue("@GS", txtGridSel.Text)

            If Current_Theme_ID > 0 Then db.Com.Parameters.AddWithValue("@ID", Current_Theme_ID)

            db.Com.ExecuteNonQuery()
            db.Con.Close()
            MsgBox("تم الحفظ بنجاح!", MsgBoxStyle.Information)
            Load_Themes()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If Current_Theme_ID = 0 Then Exit Sub
        If MsgBox("هل أنت متأكد من حذف الثيم؟", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim db As New C()
                db.Con.Open()
                db.Str = "DELETE FROM Sys_Themes WHERE Theme_ID = " & Current_Theme_ID
                db.Com = New SqlCommand(db.Str, db.Con)
                db.Com.ExecuteNonQuery()
                db.Con.Close()
                MsgBox("تم الحذف بنجاح!", MsgBoxStyle.Information)
                Load_Themes()
                btnNew.PerformClick()
            Catch ex As Exception
                MsgBox("خطأ في الحذف: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub DGV_Themes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Themes.CellClick
        If e.RowIndex >= 0 Then
            Current_Theme_ID = DGV_Themes.Rows(e.RowIndex).Cells("Theme_ID").Value

            Dim db As New C()
            db.Str = "SELECT * FROM Sys_Themes WHERE Theme_ID = " & Current_Theme_ID
            db.Com = New SqlCommand(db.Str, db.Con)
            db.Con.Open()
            db.Dr = db.Com.ExecuteReader()
            If db.Dr.HasRows Then
                db.Dr.Read()
                txtThemeName.Text = db.Dr("Theme_Name").ToString()
                chkIsDark.Checked = Convert.ToBoolean(db.Dr("Is_Dark_Mode"))

                txtFormBack.Text = db.Dr("Form_BackColor").ToString()
                txtPanelBack.Text = db.Dr("Panel_BackColor").ToString()
                txtTextPrimary.Text = db.Dr("Text_Primary_Color").ToString()
                txtTextSecondary.Text = db.Dr("Text_Secondary_Color").ToString()

                ' بيانات الداشبورد
                txtSidebarBack.Text = db.Dr("Sidebar_BackColor").ToString()
                txtHeaderBack.Text = db.Dr("Header_BackColor").ToString()
                txtCardBack.Text = db.Dr("Card_BackColor").ToString()

                txtBtnSaveBack.Text = db.Dr("Btn_Save_BackColor").ToString()
                txtBtnSaveFore.Text = db.Dr("Btn_Save_ForeColor").ToString()
                txtBtnDeleteBack.Text = db.Dr("Btn_Delete_BackColor").ToString()
                txtBtnDeleteFore.Text = db.Dr("Btn_Delete_ForeColor").ToString()
                txtBtnPrintBack.Text = db.Dr("Btn_Print_BackColor").ToString()
                txtBtnPrintFore.Text = db.Dr("Btn_Print_ForeColor").ToString()
                txtBtnGeneralBack.Text = db.Dr("Btn_General_BackColor").ToString()
                txtBtnGeneralFore.Text = db.Dr("Btn_General_ForeColor").ToString()

                ' بيانات الفواتير ونقاط البيع
                txtPOSCatBack.Text = db.Dr("POS_Category_BackColor").ToString()
                txtPOSCatFore.Text = db.Dr("POS_Category_ForeColor").ToString()
                txtPOSItemBack.Text = db.Dr("POS_Item_BackColor").ToString()
                txtPOSItemFore.Text = db.Dr("POS_Item_ForeColor").ToString()
                txtNumpadBack.Text = db.Dr("Numpad_BackColor").ToString()
                txtNumpadFore.Text = db.Dr("Numpad_ForeColor").ToString()
                txtTotalsBack.Text = db.Dr("Totals_Panel_BackColor").ToString()
                txtTotalsFore.Text = db.Dr("Totals_Panel_ForeColor").ToString()
                txtAccent.Text = db.Dr("Accent_Color").ToString()

                txtGridHeader.Text = db.Dr("Grid_Header_BackColor").ToString()
                txtGridRow.Text = db.Dr("Grid_Row_BackColor").ToString()
                txtGridAltRow.Text = db.Dr("Grid_AltRow_BackColor").ToString()
                txtGridSel.Text = db.Dr("Grid_Selection_Color").ToString()

                SetPreviewColors()
            End If
            db.Con.Close()
        End If
    End Sub

    ' =========================================================
    ' 4. زر تطبيق الثيم كافتراضي للنظام بالكامل
    ' =========================================================
    Private Sub btnApplyToSystem_Click(sender As Object, e As EventArgs) Handles btnApplyToSystem.Click
        If Current_Theme_ID = 0 Then
            MsgBox("يرجى اختيار ثيم من الجدول أولاً!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim db As New C()
        Try
            db.Con.Open()
            db.Str = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Sys_Active_Theme') CREATE TABLE Sys_Active_Theme (ID INT, Theme_ID INT);"
            db.Com = New SqlClient.SqlCommand(db.Str, db.Con)
            db.Com.ExecuteNonQuery()

            db.Str = "DELETE FROM Sys_Active_Theme; INSERT INTO Sys_Active_Theme (ID, Theme_ID) VALUES (1, " & Current_Theme_ID & ");"
            db.Com = New SqlClient.SqlCommand(db.Str, db.Con)
            db.Com.ExecuteNonQuery()

            db.Con.Close()
            MsgBox("تم اعتماد الثيم بنجاح! سيتم تطبيقه تلقائياً عند فتح أي شاشة في النظام.", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("حدث خطأ أثناء تطبيق الثيم: " & ex.Message)
        Finally
            If db.Con.State = ConnectionState.Open Then db.Con.Close()
        End Try
    End Sub

    ' =========================================================
    ' 5. زر المعاينة الحية (Live Preview)
    ' =========================================================
    Private Sub btnApplyTheme_Click(sender As Object, e As EventArgs) Handles btnApplyTheme.Click
        If Current_Theme_ID = 0 Then
            MsgBox("يرجى اختيار ثيم محفوظ من الجدول أولاً لمعاينته", MsgBoxStyle.Information)
            Exit Sub
        End If
        ThemeManager.LoadThemeFromDB(Current_Theme_ID)
        ThemeManager.ApplyThemeToForm(Me)

        ' 💡 هنا يكمن السحر لحل "قلتش الأزرار": 
        ' نقوم بإجبار مربعات الألوان على استرجاع ألوانها الخاصة بعد أن طغى عليها لون الثيم العام!
        SetPreviewColors()
    End Sub

End Class