Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient

Public Class ThemeManager

    Public Shared IsDarkMode As Boolean = False
    Public Shared FormBackColor As Color, PanelBackColor As Color
    Public Shared TextPrimaryColor As Color, TextSecondaryColor As Color

    Public Shared BtnSaveBackColor As Color, BtnSaveForeColor As Color
    Public Shared BtnDeleteBackColor As Color, BtnDeleteForeColor As Color
    Public Shared BtnPrintBackColor As Color, BtnPrintForeColor As Color
    Public Shared BtnGeneralBackColor As Color, BtnGeneralForeColor As Color

    Public Shared GridHeaderColor As Color, GridRowColor As Color
    Public Shared GridAltRowColor As Color, GridSelectionColor As Color

    Public Shared SidebarBackColor As Color, HeaderBackColor As Color, CardBackColor As Color
    Public Shared POSCatBack As Color, POSCatFore As Color
    Public Shared POSItemBack As Color, POSItemFore As Color
    Public Shared NumpadBack As Color, NumpadFore As Color
    Public Shared TotalsBack As Color, TotalsFore As Color
    Public Shared AccentColor As Color

    Public Shared Function GetContrastColor(bg As Color) As Color
        Dim brightness As Integer = CInt((CInt(bg.R) * 299 + CInt(bg.G) * 587 + CInt(bg.B) * 114) / 1000)
        Return If(brightness > 128, Color.FromArgb(30, 30, 30), Color.White)
    End Function

    Private Shared Function GetColorFromRGB(rgbString As String, fallbackColor As Color) As Color
        Try
            If String.IsNullOrWhiteSpace(rgbString) Then Return fallbackColor
            Dim parts() As String = rgbString.Split(","c)
            If parts.Length = 3 Then Return Color.FromArgb(CInt(parts(0).Trim()), CInt(parts(1).Trim()), CInt(parts(2).Trim()))
            Return fallbackColor
        Catch ex As Exception
            Return fallbackColor
        End Try
    End Function

    Public Shared Sub SetSafeDefaultColors()
        IsDarkMode = False
        FormBackColor = Color.FromArgb(243, 244, 246) : PanelBackColor = Color.White
        TextPrimaryColor = Color.FromArgb(31, 41, 55) : TextSecondaryColor = Color.FromArgb(107, 114, 128)
        SidebarBackColor = Color.FromArgb(31, 41, 55) : HeaderBackColor = Color.White : CardBackColor = Color.White
        BtnSaveBackColor = Color.FromArgb(16, 185, 129) : BtnSaveForeColor = Color.White
        BtnDeleteBackColor = Color.FromArgb(239, 68, 68) : BtnDeleteForeColor = Color.White
        BtnPrintBackColor = Color.FromArgb(245, 158, 11) : BtnPrintForeColor = Color.White
        BtnGeneralBackColor = Color.FromArgb(59, 130, 246) : BtnGeneralForeColor = Color.White
        POSCatBack = Color.FromArgb(31, 41, 55) : POSCatFore = Color.White
        POSItemBack = Color.FromArgb(243, 244, 246) : POSItemFore = Color.FromArgb(31, 41, 55)
        NumpadBack = Color.FromArgb(229, 231, 235) : NumpadFore = Color.FromArgb(31, 41, 55)
        TotalsBack = Color.FromArgb(31, 41, 55) : TotalsFore = Color.FromArgb(16, 185, 129)
        AccentColor = Color.FromArgb(59, 130, 246)
        GridHeaderColor = Color.FromArgb(243, 244, 246) : GridRowColor = Color.White
        GridAltRowColor = Color.FromArgb(249, 250, 251) : GridSelectionColor = Color.FromArgb(59, 130, 246)
    End Sub

    Public Shared Sub LoadDefaultSystemTheme()
        SetSafeDefaultColors()
        Dim DefaultThemeID As Integer = 0
        Try
            Dim db As New C()
            db.Str = "IF EXISTS (SELECT * FROM sysobjects WHERE name='Sys_Active_Theme') SELECT TOP 1 Theme_ID FROM Sys_Active_Theme"
            db.Com = New SqlClient.SqlCommand(db.Str, db.Con) : db.Con.Open()
            Dim result = db.Com.ExecuteScalar()
            If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then DefaultThemeID = Convert.ToInt32(result)
            db.Con.Close()
        Catch ex As Exception
        End Try
        If DefaultThemeID > 0 Then LoadThemeFromDB(DefaultThemeID)
    End Sub

    Public Shared Sub LoadThemeFromDB(ThemeID As Integer)
        Dim db As New C()
        Try
            db.Str = "SELECT * FROM Sys_Themes WHERE Theme_ID = " & ThemeID
            db.Com = New SqlCommand(db.Str, db.Con) : db.Con.Open() : db.Dr = db.Com.ExecuteReader()
            If db.Dr.HasRows Then
                db.Dr.Read()
                IsDarkMode = Convert.ToBoolean(db.Dr("Is_Dark_Mode"))
                FormBackColor = GetColorFromRGB(db.Dr("Form_BackColor").ToString(), FormBackColor)
                PanelBackColor = GetColorFromRGB(db.Dr("Panel_BackColor").ToString(), PanelBackColor)
                TextPrimaryColor = GetColorFromRGB(db.Dr("Text_Primary_Color").ToString(), TextPrimaryColor)
                TextSecondaryColor = GetColorFromRGB(db.Dr("Text_Secondary_Color").ToString(), TextSecondaryColor)
                SidebarBackColor = GetColorFromRGB(db.Dr("Sidebar_BackColor").ToString(), SidebarBackColor)
                HeaderBackColor = GetColorFromRGB(db.Dr("Header_BackColor").ToString(), HeaderBackColor)
                CardBackColor = GetColorFromRGB(db.Dr("Card_BackColor").ToString(), CardBackColor)
                BtnSaveBackColor = GetColorFromRGB(db.Dr("Btn_Save_BackColor").ToString(), BtnSaveBackColor)
                BtnSaveForeColor = GetColorFromRGB(db.Dr("Btn_Save_ForeColor").ToString(), BtnSaveForeColor)
                BtnDeleteBackColor = GetColorFromRGB(db.Dr("Btn_Delete_BackColor").ToString(), BtnDeleteBackColor)
                BtnDeleteForeColor = GetColorFromRGB(db.Dr("Btn_Delete_ForeColor").ToString(), BtnDeleteForeColor)
                BtnPrintBackColor = GetColorFromRGB(db.Dr("Btn_Print_BackColor").ToString(), BtnPrintBackColor)
                BtnPrintForeColor = GetColorFromRGB(db.Dr("Btn_Print_ForeColor").ToString(), BtnPrintForeColor)
                BtnGeneralBackColor = GetColorFromRGB(db.Dr("Btn_General_BackColor").ToString(), BtnGeneralBackColor)
                BtnGeneralForeColor = GetColorFromRGB(db.Dr("Btn_General_ForeColor").ToString(), BtnGeneralForeColor)
                POSCatBack = GetColorFromRGB(db.Dr("POS_Category_BackColor").ToString(), POSCatBack)
                POSCatFore = GetColorFromRGB(db.Dr("POS_Category_ForeColor").ToString(), POSCatFore)
                POSItemBack = GetColorFromRGB(db.Dr("POS_Item_BackColor").ToString(), POSItemBack)
                POSItemFore = GetColorFromRGB(db.Dr("POS_Item_ForeColor").ToString(), POSItemFore)
                NumpadBack = GetColorFromRGB(db.Dr("Numpad_BackColor").ToString(), NumpadBack)
                NumpadFore = GetColorFromRGB(db.Dr("Numpad_ForeColor").ToString(), NumpadFore)
                TotalsBack = GetColorFromRGB(db.Dr("Totals_Panel_BackColor").ToString(), TotalsBack)
                TotalsFore = GetColorFromRGB(db.Dr("Totals_Panel_ForeColor").ToString(), TotalsFore)
                AccentColor = GetColorFromRGB(db.Dr("Accent_Color").ToString(), AccentColor)
                GridHeaderColor = GetColorFromRGB(db.Dr("Grid_Header_BackColor").ToString(), GridHeaderColor)
                GridRowColor = GetColorFromRGB(db.Dr("Grid_Row_BackColor").ToString(), GridRowColor)
                GridAltRowColor = GetColorFromRGB(db.Dr("Grid_AltRow_BackColor").ToString(), GridAltRowColor)
                GridSelectionColor = GetColorFromRGB(db.Dr("Grid_Selection_Color").ToString(), GridSelectionColor)
            End If
            db.Con.Close()
        Catch ex As Exception
        Finally
            If db.Con.State = ConnectionState.Open Then db.Con.Close()
        End Try
    End Sub

    Public Shared Sub ApplyThemeToForm(frm As Form)
        If FormBackColor.IsEmpty Then SetSafeDefaultColors()
        frm.BackColor = FormBackColor
        frm.ForeColor = TextPrimaryColor
        ApplyThemeToControls(frm.Controls)
    End Sub

    'Private Shared Sub ApplyThemeToControls(controls As Control.ControlCollection)
    '    For Each ctrl As Control In controls

    '        ' --- 1. تلوين البانلات والكروت ---
    '        If TypeOf ctrl Is Panel Or TypeOf ctrl Is GroupBox Then
    '            If ctrl.Tag IsNot Nothing Then
    '                Select Case ctrl.Tag.ToString().ToUpper()
    '                    Case "TRANSPARENT" : ctrl.BackColor = Color.Transparent
    '                    Case "TITLEBAR", "HEADER" : ctrl.BackColor = HeaderBackColor
    '                    Case "SIDEBAR", "MENU" : ctrl.BackColor = SidebarBackColor
    '                    Case "CARD", "DASHBOARD" : ctrl.BackColor = CardBackColor
    '                    Case "TOTALS" : ctrl.BackColor = TotalsBack
    '                    Case "ACCENT" : ctrl.BackColor = AccentColor
    '                    Case Else : ctrl.BackColor = PanelBackColor
    '                End Select
    '            Else
    '                ctrl.BackColor = PanelBackColor
    '            End If
    '        End If

    '        ' --- 2. البارات ---
    '        If TypeOf ctrl Is ToolStrip Then
    '            Dim ts As ToolStrip = DirectCast(ctrl, ToolStrip)
    '            ts.RenderMode = ToolStripRenderMode.System
    '            If ctrl.Tag IsNot Nothing AndAlso (ctrl.Tag.ToString().ToUpper() = "SIDEBAR" Or ctrl.Tag.ToString().ToUpper() = "FOOTER") Then
    '                ts.BackColor = SidebarBackColor : ts.ForeColor = GetContrastColor(SidebarBackColor)
    '            Else
    '                ts.BackColor = HeaderBackColor : ts.ForeColor = GetContrastColor(HeaderBackColor)
    '            End If
    '            For Each item As ToolStripItem In ts.Items
    '                item.ForeColor = ts.ForeColor : item.BackColor = Color.Transparent
    '            Next
    '        End If

    '        ' --- 3. 💥 تلوين النصوص والتشيك بوكس 💥 ---
    '        If TypeOf ctrl Is Label Or TypeOf ctrl Is CheckBox Or TypeOf ctrl Is RadioButton Then

    '            ' 💡 الحل القاطع لمشكلة المربع الأبيض للـ CheckBox
    '            If TypeOf ctrl Is CheckBox Or TypeOf ctrl Is RadioButton Then
    '                Dim btnBase = DirectCast(ctrl, ButtonBase)
    '                btnBase.UseVisualStyleBackColor = False
    '                btnBase.FlatStyle = FlatStyle.Standard ' يوقف تأثيرات الهوفر الخاصة بالويندوز
    '            End If

    '            Dim tagStr As String = ""
    '            If ctrl.Tag IsNot Nothing Then tagStr = ctrl.Tag.ToString().ToUpper()

    '            Dim parentTag As String = ""
    '            If ctrl.Parent IsNot Nothing AndAlso ctrl.Parent.Tag IsNot Nothing Then
    '                parentTag = ctrl.Parent.Tag.ToString().ToUpper()
    '            End If

    '            ' 💡 تفادي الشفافية الوهمية التي تسبب بقات في الـ CheckBox عبر قراءة لون الأب المباشر
    '            Dim actualBackColor As Color
    '            If tagStr.Contains("TRANSPARENT") Then
    '                actualBackColor = If(ctrl.Parent IsNot Nothing, ctrl.Parent.BackColor, Color.Transparent)
    '            Else
    '                Select Case parentTag
    '                    Case "HEADER", "TITLEBAR" : actualBackColor = HeaderBackColor
    '                    Case "SIDEBAR", "MENU" : actualBackColor = SidebarBackColor
    '                    Case "CARD", "DASHBOARD" : actualBackColor = CardBackColor
    '                    Case "TOTALS" : actualBackColor = TotalsBack
    '                    Case "ACCENT" : actualBackColor = AccentColor
    '                    Case Else : actualBackColor = If(ctrl.Parent IsNot Nothing, ctrl.Parent.BackColor, Color.Transparent)
    '                End Select
    '            End If

    '            ctrl.BackColor = actualBackColor

    '            ' 💡 تحديد لون النص
    '            If tagStr.Contains("TITLE") Then
    '                ctrl.ForeColor = GetContrastColor(HeaderBackColor)
    '            ElseIf tagStr.Contains("SECONDARY") Then
    '                ctrl.ForeColor = TextSecondaryColor
    '            ElseIf tagStr.Contains("TOTALS") Then
    '                ctrl.ForeColor = TotalsFore
    '            ElseIf tagStr.Contains("ACCENT") Then
    '                ctrl.ForeColor = AccentColor
    '            ElseIf parentTag = "CARD" Then
    '                ctrl.ForeColor = GetContrastColor(CardBackColor)
    '            Else
    '                ctrl.ForeColor = TextPrimaryColor
    '            End If
    '        End If

    '        ' --- 4. حقول الإدخال والكومبو بوكس ---
    '        If TypeOf ctrl Is TextBox Or TypeOf ctrl Is ComboBox Then

    '            If ctrl.Tag IsNot Nothing AndAlso ctrl.Tag.ToString().ToUpper() = "TOTALS" Then
    '                ctrl.BackColor = TotalsBack : ctrl.ForeColor = TotalsFore
    '            Else
    '                ctrl.BackColor = If(IsDarkMode, Color.FromArgb(45, 45, 45), Color.White)
    '                ctrl.ForeColor = TextPrimaryColor
    '            End If
    '        End If

    '        ' --- 5. الجداول ---
    '        If TypeOf ctrl Is DataGridView Then
    '            Dim dgv As DataGridView = DirectCast(ctrl, DataGridView)
    '            dgv.EnableHeadersVisualStyles = False
    '            dgv.BackgroundColor = If(PanelBackColor.A = 0, Color.White, PanelBackColor)
    '            dgv.GridColor = If(IsDarkMode, Color.FromArgb(60, 60, 60), Color.Silver)
    '            dgv.ColumnHeadersDefaultCellStyle.BackColor = If(GridHeaderColor.A = 0, Color.FromArgb(236, 240, 241), GridHeaderColor)
    '            dgv.ColumnHeadersDefaultCellStyle.ForeColor = If(TextPrimaryColor.A = 0, Color.Black, TextPrimaryColor)
    '            dgv.DefaultCellStyle.BackColor = If(GridRowColor.A = 0, Color.White, GridRowColor)
    '            dgv.DefaultCellStyle.ForeColor = If(TextPrimaryColor.A = 0, Color.Black, TextPrimaryColor)
    '            dgv.DefaultCellStyle.SelectionBackColor = If(GridSelectionColor.A = 0, Color.FromArgb(59, 130, 246), GridSelectionColor)
    '            dgv.DefaultCellStyle.SelectionForeColor = Color.White
    '            dgv.AlternatingRowsDefaultCellStyle.BackColor = If(GridAltRowColor.A = 0, Color.FromArgb(249, 250, 251), GridAltRowColor)
    '        End If

    '        ' --- 6. الأزرار ---
    '        If TypeOf ctrl Is Button Then
    '            Dim btn As Button = DirectCast(ctrl, Button)
    '            If btn.Tag IsNot Nothing AndAlso btn.Tag.ToString().ToUpper() = "IGNORE" Then Continue For

    '            btn.FlatStyle = FlatStyle.Flat
    '            btn.FlatAppearance.BorderSize = 0

    '            If btn.Tag IsNot Nothing Then
    '                Select Case btn.Tag.ToString().ToUpper()
    '                    Case "SAVE", "PAY", "CONFIRM", "PRIMARY" : btn.BackColor = BtnSaveBackColor : btn.ForeColor = BtnSaveForeColor
    '                    Case "DELETE", "CANCEL", "CLOSE" : btn.BackColor = BtnDeleteBackColor : btn.ForeColor = BtnDeleteForeColor
    '                    Case "PRINT", "REPORT" : btn.BackColor = BtnPrintBackColor : btn.ForeColor = BtnPrintForeColor
    '                    Case "GENERAL", "SHORTCUT" : btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
    '                    Case "CATEGORY" : btn.BackColor = POSCatBack : btn.ForeColor = POSCatFore
    '                    Case "ITEM" : btn.BackColor = POSItemBack : btn.ForeColor = POSItemFore
    '                    Case "NUMPAD" : btn.BackColor = NumpadBack : btn.ForeColor = NumpadFore
    '                    Case "TRANSPARENT"
    '                        btn.BackColor = If(btn.Parent IsNot Nothing, btn.Parent.BackColor, Color.Transparent)
    '                        btn.ForeColor = TextPrimaryColor
    '                    Case "APP_CONTROL"
    '                        btn.BackColor = HeaderBackColor
    '                        btn.ForeColor = GetContrastColor(HeaderBackColor)
    '                        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, GetContrastColor(HeaderBackColor))
    '                    Case Else : btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
    '                End Select
    '            Else
    '                btn.BackColor = BtnGeneralBackColor
    '                btn.ForeColor = BtnGeneralForeColor
    '            End If
    '        End If

    '        If ctrl.HasChildren Then ApplyThemeToControls(ctrl.Controls)
    '    Next
    'End Sub
    'Private Shared Sub ApplyThemeToControls(controls As Control.ControlCollection)
    '    For Each ctrl As Control In controls

    '        ' --- 1. تلوين البانلات والتابات ---
    '        If TypeOf ctrl Is Panel Or TypeOf ctrl Is GroupBox Or TypeOf ctrl Is TabPage Then
    '            If ctrl.Tag IsNot Nothing Then
    '                Select Case ctrl.Tag.ToString().ToUpper()
    '                    Case "TRANSPARENT"
    '                        ' 💡 الـ TabPage لا تدعم الشفافية وتسبب المربعات السوداء، لذلك نعطيها لون الفورم
    '                        If TypeOf ctrl Is TabPage Then
    '                            ctrl.BackColor = FormBackColor
    '                        Else
    '                            ctrl.BackColor = Color.Transparent
    '                        End If
    '                    Case "TITLEBAR", "HEADER" : ctrl.BackColor = HeaderBackColor
    '                    Case "SIDEBAR", "MENU" : ctrl.BackColor = SidebarBackColor
    '                    Case "CARD", "DASHBOARD" : ctrl.BackColor = CardBackColor
    '                    Case "TOTALS" : ctrl.BackColor = TotalsBack
    '                    Case "ACCENT" : ctrl.BackColor = AccentColor
    '                    Case Else : ctrl.BackColor = PanelBackColor
    '                End Select
    '            Else
    '                ' تأمين التابات التي لا تملك تاج
    '                If TypeOf ctrl Is TabPage Then
    '                    ctrl.BackColor = FormBackColor
    '                Else
    '                    ctrl.BackColor = PanelBackColor
    '                End If
    '            End If
    '        End If

    '        ' --- 2. البارات ---
    '        If TypeOf ctrl Is ToolStrip Then
    '            Dim ts As ToolStrip = DirectCast(ctrl, ToolStrip)
    '            ts.RenderMode = ToolStripRenderMode.System
    '            If ctrl.Tag IsNot Nothing AndAlso (ctrl.Tag.ToString().ToUpper() = "SIDEBAR" Or ctrl.Tag.ToString().ToUpper() = "FOOTER") Then
    '                ts.BackColor = SidebarBackColor : ts.ForeColor = GetContrastColor(SidebarBackColor)
    '            Else
    '                ts.BackColor = HeaderBackColor : ts.ForeColor = GetContrastColor(HeaderBackColor)
    '            End If
    '            For Each item As ToolStripItem In ts.Items
    '                item.ForeColor = ts.ForeColor : item.BackColor = Color.Transparent
    '            Next
    '        End If

    '        ' --- 3. 💥 إصلاح النصوص والتشيك بوكس 💥 ---
    '        If TypeOf ctrl Is Label Or TypeOf ctrl Is CheckBox Or TypeOf ctrl Is RadioButton Then
    '            If TypeOf ctrl Is CheckBox Or TypeOf ctrl Is RadioButton Then
    '                Dim btnBase = DirectCast(ctrl, ButtonBase)
    '                btnBase.UseVisualStyleBackColor = False
    '                btnBase.FlatStyle = FlatStyle.Standard
    '            End If

    '            Dim tagStr As String = If(ctrl.Tag IsNot Nothing, ctrl.Tag.ToString().ToUpper(), "")
    '            Dim parentTag As String = If(ctrl.Parent IsNot Nothing AndAlso ctrl.Parent.Tag IsNot Nothing, ctrl.Parent.Tag.ToString().ToUpper(), "")

    '            ' إرجاع الشفافية للنصوص (الآن ستعمل بأمان لأن التاب بيج لم تعد شفافة)
    '            ctrl.BackColor = Color.Transparent

    '            ' تحديد لون النص
    '            If tagStr.Contains("TITLE") Then
    '                ctrl.ForeColor = GetContrastColor(HeaderBackColor)
    '            ElseIf tagStr.Contains("SECONDARY") Then
    '                ctrl.ForeColor = TextSecondaryColor
    '            ElseIf tagStr.Contains("TOTALS") Then
    '                ctrl.ForeColor = TotalsFore
    '            ElseIf tagStr.Contains("ACCENT") Then
    '                ctrl.ForeColor = AccentColor
    '            ElseIf parentTag = "CARD" Then
    '                ctrl.ForeColor = GetContrastColor(CardBackColor)
    '            Else
    '                ctrl.ForeColor = TextPrimaryColor
    '            End If
    '        End If

    '        ' --- 4. حقول الإدخال والكومبو بوكس ---
    '        If TypeOf ctrl Is TextBox Or TypeOf ctrl Is ComboBox Or TypeOf ctrl Is NumericUpDown Then
    '            If ctrl.Tag IsNot Nothing AndAlso ctrl.Tag.ToString().ToUpper() = "TOTALS" Then
    '                ctrl.BackColor = TotalsBack : ctrl.ForeColor = TotalsFore
    '            Else
    '                ' إصلاح الألوان لتكون بيضاء في الوضع العادي، وداكنة في الدارك مود مع نص واضح
    '                ctrl.BackColor = If(IsDarkMode, Color.FromArgb(45, 45, 45), Color.White)
    '                ctrl.ForeColor = If(IsDarkMode, Color.White, Color.Black)
    '            End If
    '        End If

    '        ' --- 5. الجداول ---
    '        If TypeOf ctrl Is DataGridView Then
    '            Dim dgv As DataGridView = DirectCast(ctrl, DataGridView)
    '            dgv.EnableHeadersVisualStyles = False
    '            dgv.BackgroundColor = If(PanelBackColor.A = 0, Color.White, PanelBackColor)
    '            dgv.GridColor = If(IsDarkMode, Color.FromArgb(60, 60, 60), Color.Silver)
    '            dgv.ColumnHeadersDefaultCellStyle.BackColor = If(GridHeaderColor.A = 0, Color.FromArgb(236, 240, 241), GridHeaderColor)
    '            dgv.ColumnHeadersDefaultCellStyle.ForeColor = If(TextPrimaryColor.A = 0, Color.Black, TextPrimaryColor)
    '            dgv.DefaultCellStyle.BackColor = If(GridRowColor.A = 0, Color.White, GridRowColor)
    '            dgv.DefaultCellStyle.ForeColor = If(TextPrimaryColor.A = 0, Color.Black, TextPrimaryColor)
    '            dgv.DefaultCellStyle.SelectionBackColor = If(GridSelectionColor.A = 0, Color.FromArgb(59, 130, 246), GridSelectionColor)
    '            dgv.DefaultCellStyle.SelectionForeColor = Color.White
    '            dgv.AlternatingRowsDefaultCellStyle.BackColor = If(GridAltRowColor.A = 0, Color.FromArgb(249, 250, 251), GridAltRowColor)
    '        End If

    '        ' --- 6. الأزرار ---
    '        If TypeOf ctrl Is Button Then
    '            Dim btn As Button = DirectCast(ctrl, Button)
    '            If btn.Tag IsNot Nothing AndAlso btn.Tag.ToString().ToUpper() = "IGNORE" Then Continue For

    '            btn.FlatStyle = FlatStyle.Flat
    '            btn.FlatAppearance.BorderSize = 0

    '            If btn.Tag IsNot Nothing Then
    '                Select Case btn.Tag.ToString().ToUpper()
    '                    Case "SAVE", "PAY", "CONFIRM", "PRIMARY" : btn.BackColor = BtnSaveBackColor : btn.ForeColor = BtnSaveForeColor
    '                    Case "DELETE", "CANCEL", "CLOSE" : btn.BackColor = BtnDeleteBackColor : btn.ForeColor = BtnDeleteForeColor
    '                    Case "PRINT", "REPORT" : btn.BackColor = BtnPrintBackColor : btn.ForeColor = BtnPrintForeColor
    '                    Case "GENERAL", "SHORTCUT" : btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
    '                    Case "CATEGORY" : btn.BackColor = POSCatBack : btn.ForeColor = POSCatFore
    '                    Case "ITEM" : btn.BackColor = POSItemBack : btn.ForeColor = POSItemFore
    '                    Case "NUMPAD" : btn.BackColor = NumpadBack : btn.ForeColor = NumpadFore
    '                    Case "TRANSPARENT"
    '                        btn.BackColor = If(btn.Parent IsNot Nothing, btn.Parent.BackColor, Color.Transparent)
    '                        btn.ForeColor = TextPrimaryColor
    '                    Case "APP_CONTROL"
    '                        btn.BackColor = HeaderBackColor
    '                        btn.ForeColor = GetContrastColor(HeaderBackColor)
    '                        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, GetContrastColor(HeaderBackColor))
    '                    Case Else : btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
    '                End Select
    '            Else
    '                btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
    '            End If
    '        End If

    '        If ctrl.HasChildren Then ApplyThemeToControls(ctrl.Controls)
    '    Next
    'End Sub
    'Private Shared Sub ApplyThemeToControls(controls As Control.ControlCollection)
    '    ' =========================================================
    '    ' 🌟 تعريف الخطوط المطلوبة وتوحيدها على مستوى النظام
    '    ' =========================================================
    '    Dim gridAndFieldsFont As New Font("Segoe UI", 12.0!, FontStyle.Bold)
    '    Dim normalTextFont As New Font("Segoe UI", 11.0!, FontStyle.Regular)

    '    For Each ctrl As Control In controls
    '        If ctrl.GetType().Name.Contains("AdvancedDataGridViewSearchToolBar") Or ctrl.GetType().Name.Contains("DateRange_Flate") Then
    '            Continue For ' فوت الأداة هذي وامشِ للي بعدها
    '        End If
    '        ' --- 1. تلوين البانلات والتابات ---
    '        If TypeOf ctrl Is Panel Or TypeOf ctrl Is GroupBox Or TypeOf ctrl Is TabPage Then
    '            If ctrl.Tag IsNot Nothing Then
    '                Select Case ctrl.Tag.ToString().ToUpper()
    '                    Case "TRANSPARENT"
    '                        If TypeOf ctrl Is TabPage Then
    '                            ctrl.BackColor = FormBackColor
    '                        Else
    '                            ctrl.BackColor = Color.Transparent
    '                        End If
    '                    Case "TITLEBAR", "HEADER" : ctrl.BackColor = HeaderBackColor
    '                    Case "SIDEBAR", "MENU" : ctrl.BackColor = SidebarBackColor
    '                    Case "CARD", "DASHBOARD" : ctrl.BackColor = CardBackColor
    '                    Case "TOTALS" : ctrl.BackColor = TotalsBack
    '                    Case "ACCENT" : ctrl.BackColor = AccentColor
    '                    Case Else : ctrl.BackColor = PanelBackColor
    '                End Select
    '            Else
    '                If TypeOf ctrl Is TabPage Then
    '                    ctrl.BackColor = FormBackColor
    '                Else
    '                    ctrl.BackColor = PanelBackColor
    '                End If
    '            End If
    '        End If

    '        ' --- 2. البارات ---
    '        If TypeOf ctrl Is ToolStrip Then
    '            Dim ts As ToolStrip = DirectCast(ctrl, ToolStrip)
    '            ts.RenderMode = ToolStripRenderMode.System
    '            If ctrl.Tag IsNot Nothing AndAlso (ctrl.Tag.ToString().ToUpper() = "SIDEBAR" Or ctrl.Tag.ToString().ToUpper() = "FOOTER") Then
    '                ts.BackColor = SidebarBackColor : ts.ForeColor = GetContrastColor(SidebarBackColor)
    '            Else
    '                ts.BackColor = HeaderBackColor : ts.ForeColor = GetContrastColor(HeaderBackColor)
    '            End If
    '            For Each item As ToolStripItem In ts.Items
    '                item.ForeColor = ts.ForeColor : item.BackColor = Color.Transparent
    '            Next
    '        End If

    '        ' --- 3. النصوص والتشيك بوكس ---
    '        If TypeOf ctrl Is Label Or TypeOf ctrl Is CheckBox Or TypeOf ctrl Is RadioButton Then
    '            ' تطبيق خط النصوص العادية
    '            ctrl.Font = normalTextFont

    '            If TypeOf ctrl Is CheckBox Or TypeOf ctrl Is RadioButton Then
    '                Dim btnBase = DirectCast(ctrl, ButtonBase)
    '                btnBase.UseVisualStyleBackColor = False
    '                btnBase.FlatStyle = FlatStyle.Standard
    '            End If

    '            Dim tagStr As String = If(ctrl.Tag IsNot Nothing, ctrl.Tag.ToString().ToUpper(), "")
    '            Dim parentTag As String = If(ctrl.Parent IsNot Nothing AndAlso ctrl.Parent.Tag IsNot Nothing, ctrl.Parent.Tag.ToString().ToUpper(), "")

    '            ctrl.BackColor = Color.Transparent

    '            If tagStr.Contains("TITLE") Then
    '                ctrl.ForeColor = GetContrastColor(HeaderBackColor)
    '            ElseIf tagStr.Contains("SECONDARY") Then
    '                ctrl.ForeColor = TextSecondaryColor
    '            ElseIf tagStr.Contains("TOTALS") Then
    '                ctrl.ForeColor = TotalsFore
    '            ElseIf tagStr.Contains("ACCENT") Then
    '                ctrl.ForeColor = AccentColor
    '            ElseIf parentTag = "CARD" Then
    '                ctrl.ForeColor = GetContrastColor(CardBackColor)
    '            Else
    '                ctrl.ForeColor = TextPrimaryColor
    '            End If
    '        End If

    '        ' --- 4. حقول الإدخال والكومبو بوكس ---
    '        If TypeOf ctrl Is TextBox Or TypeOf ctrl Is ComboBox Or TypeOf ctrl Is NumericUpDown Then
    '            ' تطبيق الخط البولد للحقول
    '            ctrl.Font = gridAndFieldsFont

    '            If ctrl.Tag IsNot Nothing AndAlso ctrl.Tag.ToString().ToUpper() = "TOTALS" Then
    '                ctrl.BackColor = TotalsBack : ctrl.ForeColor = TotalsFore
    '            Else
    '                ctrl.BackColor = If(IsDarkMode, Color.FromArgb(45, 45, 45), Color.White)
    '                ctrl.ForeColor = If(IsDarkMode, Color.White, Color.Black)
    '            End If
    '        End If

    '        ' --- 5. الجداول (الجريدات) ---
    '        If TypeOf ctrl Is DataGridView Then
    '            Dim dgv As DataGridView = DirectCast(ctrl, DataGridView)
    '            dgv.EnableHeadersVisualStyles = False

    '            ' 🌟 تطبيق الخطوط على الجريد 🌟
    '            dgv.Font = gridAndFieldsFont
    '            dgv.ColumnHeadersDefaultCellStyle.Font = gridAndFieldsFont
    '            dgv.DefaultCellStyle.Font = gridAndFieldsFont

    '            dgv.BackgroundColor = If(PanelBackColor.A = 0, Color.White, PanelBackColor)
    '            dgv.GridColor = If(IsDarkMode, Color.FromArgb(60, 60, 60), Color.Silver)
    '            dgv.ColumnHeadersDefaultCellStyle.BackColor = If(GridHeaderColor.A = 0, Color.FromArgb(236, 240, 241), GridHeaderColor)
    '            dgv.ColumnHeadersDefaultCellStyle.ForeColor = If(TextPrimaryColor.A = 0, Color.Black, TextPrimaryColor)
    '            dgv.DefaultCellStyle.BackColor = If(GridRowColor.A = 0, Color.White, GridRowColor)
    '            dgv.DefaultCellStyle.ForeColor = If(TextPrimaryColor.A = 0, Color.Black, TextPrimaryColor)

    '            ' 🌟 اللون المحدد المأخوذ من الصورة (أزرق ويندوز الصافي) 🌟
    '            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215)
    '            dgv.DefaultCellStyle.SelectionForeColor = Color.White

    '            dgv.AlternatingRowsDefaultCellStyle.BackColor = If(GridAltRowColor.A = 0, Color.FromArgb(249, 250, 251), GridAltRowColor)
    '        End If

    '        ' --- 6. الأزرار ---
    '        If TypeOf ctrl Is Button Then
    '            Dim btn As Button = DirectCast(ctrl, Button)
    '            If btn.Tag IsNot Nothing AndAlso btn.Tag.ToString().ToUpper() = "IGNORE" Then Continue For

    '            ' تطبيق الخط البولد للأزرار
    '            btn.Font = gridAndFieldsFont

    '            btn.FlatStyle = FlatStyle.Flat
    '            btn.FlatAppearance.BorderSize = 0

    '            If btn.Tag IsNot Nothing Then
    '                Select Case btn.Tag.ToString().ToUpper()
    '                    Case "SAVE", "PAY", "CONFIRM", "PRIMARY" : btn.BackColor = BtnSaveBackColor : btn.ForeColor = BtnSaveForeColor
    '                    Case "DELETE", "CANCEL", "CLOSE" : btn.BackColor = BtnDeleteBackColor : btn.ForeColor = BtnDeleteForeColor
    '                    Case "PRINT", "REPORT" : btn.BackColor = BtnPrintBackColor : btn.ForeColor = BtnPrintForeColor
    '                    Case "GENERAL", "SHORTCUT" : btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
    '                    Case "CATEGORY" : btn.BackColor = POSCatBack : btn.ForeColor = POSCatFore
    '                    Case "ITEM" : btn.BackColor = POSItemBack : btn.ForeColor = POSItemFore
    '                    Case "NUMPAD" : btn.BackColor = NumpadBack : btn.ForeColor = NumpadFore
    '                    Case "TRANSPARENT"
    '                        btn.BackColor = If(btn.Parent IsNot Nothing, btn.Parent.BackColor, Color.Transparent)
    '                        btn.ForeColor = TextPrimaryColor
    '                    Case "APP_CONTROL"
    '                        btn.BackColor = HeaderBackColor
    '                        btn.ForeColor = GetContrastColor(HeaderBackColor)
    '                        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, GetContrastColor(HeaderBackColor))
    '                    Case Else : btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
    '                End Select
    '            Else
    '                btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
    '            End If
    '        End If

    '        If ctrl.HasChildren Then ApplyThemeToControls(ctrl.Controls)
    '    Next
    'End Sub
    Private Shared Sub ApplyThemeToControls(controls As Control.ControlCollection)
        ' =========================================================
        ' 🌟 تعريف خط الجريد فقط (لأنه ثابت 12 Bold كما طلبت)
        ' =========================================================
        Dim gridFont As New Font("Segoe UI", 12.0!, FontStyle.Bold)

        For Each ctrl As Control In controls
            If ctrl.GetType().Name.Contains("AdvancedDataGridViewSearchToolBar") Or ctrl.GetType().Name.Contains("DateRange_Flate") Then
                Continue For ' فوت الأداة هذي وامشِ للي بعدها
            End If

            ' --- 1. تلوين البانلات والتابات ---
            If TypeOf ctrl Is Panel Or TypeOf ctrl Is GroupBox Or TypeOf ctrl Is TabPage Then
                If ctrl.Tag IsNot Nothing Then
                    Select Case ctrl.Tag.ToString().ToUpper()
                        Case "TRANSPARENT"
                            If TypeOf ctrl Is TabPage Then
                                ctrl.BackColor = FormBackColor
                            Else
                                ctrl.BackColor = Color.Transparent
                            End If
                        Case "TITLEBAR", "HEADER" : ctrl.BackColor = HeaderBackColor
                        Case "SIDEBAR", "MENU" : ctrl.BackColor = SidebarBackColor
                        Case "CARD", "DASHBOARD" : ctrl.BackColor = CardBackColor
                        Case "TOTALS" : ctrl.BackColor = TotalsBack
                        Case "ACCENT" : ctrl.BackColor = AccentColor
                        Case Else : ctrl.BackColor = PanelBackColor
                    End Select
                Else
                    If TypeOf ctrl Is TabPage Then
                        ctrl.BackColor = FormBackColor
                    Else
                        ctrl.BackColor = PanelBackColor
                    End If
                End If
            End If

            ' --- 2. البارات ---
            If TypeOf ctrl Is ToolStrip Then
                Dim ts As ToolStrip = DirectCast(ctrl, ToolStrip)
                ts.RenderMode = ToolStripRenderMode.System
                If ctrl.Tag IsNot Nothing AndAlso (ctrl.Tag.ToString().ToUpper() = "SIDEBAR" Or ctrl.Tag.ToString().ToUpper() = "FOOTER") Then
                    ts.BackColor = SidebarBackColor : ts.ForeColor = GetContrastColor(SidebarBackColor)
                Else
                    ts.BackColor = HeaderBackColor : ts.ForeColor = GetContrastColor(HeaderBackColor)
                End If
                For Each item As ToolStripItem In ts.Items
                    item.ForeColor = ts.ForeColor : item.BackColor = Color.Transparent
                Next
            End If

            ' --- 3. النصوص والتشيك بوكس ---
            If TypeOf ctrl Is Label Or TypeOf ctrl Is CheckBox Or TypeOf ctrl Is RadioButton Then
                ' 💡 السحر هنا: تغيير الخط إلى Segoe UI مع الحفاظ على الحجم والـ Bold الأصلي من الديزاينر!
                ctrl.Font = New Font("Segoe UI", ctrl.Font.Size, ctrl.Font.Style)

                If TypeOf ctrl Is CheckBox Or TypeOf ctrl Is RadioButton Then
                    Dim btnBase = DirectCast(ctrl, ButtonBase)
                    btnBase.UseVisualStyleBackColor = False
                    btnBase.FlatStyle = FlatStyle.Standard
                End If

                Dim tagStr As String = If(ctrl.Tag IsNot Nothing, ctrl.Tag.ToString().ToUpper(), "")
                Dim parentTag As String = If(ctrl.Parent IsNot Nothing AndAlso ctrl.Parent.Tag IsNot Nothing, ctrl.Parent.Tag.ToString().ToUpper(), "")

                ctrl.BackColor = Color.Transparent

                If tagStr.Contains("TITLE") Then
                    ctrl.ForeColor = GetContrastColor(HeaderBackColor)
                ElseIf tagStr.Contains("SECONDARY") Then
                    ctrl.ForeColor = TextSecondaryColor
                ElseIf tagStr.Contains("TOTALS") Then
                    ctrl.ForeColor = TotalsFore
                ElseIf tagStr.Contains("ACCENT") Then
                    ctrl.ForeColor = AccentColor
                ElseIf parentTag = "CARD" Then
                    ctrl.ForeColor = GetContrastColor(CardBackColor)
                Else
                    ctrl.ForeColor = TextPrimaryColor
                End If
            End If

            ' --- 4. حقول الإدخال والكومبو بوكس ---
            If TypeOf ctrl Is TextBox Or TypeOf ctrl Is ComboBox Or TypeOf ctrl Is NumericUpDown Then
                ' الحفاظ على حجم خط الحقول الأصلي
                ctrl.Font = New Font("Segoe UI", ctrl.Font.Size, ctrl.Font.Style)

                If ctrl.Tag IsNot Nothing AndAlso ctrl.Tag.ToString().ToUpper() = "TOTALS" Then
                    ctrl.BackColor = TotalsBack : ctrl.ForeColor = TotalsFore
                Else
                    ctrl.BackColor = If(IsDarkMode, Color.FromArgb(45, 45, 45), Color.White)
                    ctrl.ForeColor = If(IsDarkMode, Color.White, Color.Black)
                End If
            End If

            ' --- 5. الجداول (الجريدات) ---
            If TypeOf ctrl Is DataGridView Then
                Dim dgv As DataGridView = DirectCast(ctrl, DataGridView)
                dgv.EnableHeadersVisualStyles = False

                ' 🌟 تطبيق الخط الموحد 12 Bold على الجريد فقط 🌟
                dgv.Font = gridFont
                dgv.ColumnHeadersDefaultCellStyle.Font = gridFont
                dgv.DefaultCellStyle.Font = gridFont

                dgv.BackgroundColor = If(PanelBackColor.A = 0, Color.White, PanelBackColor)
                dgv.GridColor = If(IsDarkMode, Color.FromArgb(60, 60, 60), Color.Silver)
                dgv.ColumnHeadersDefaultCellStyle.BackColor = If(GridHeaderColor.A = 0, Color.FromArgb(236, 240, 241), GridHeaderColor)
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = If(TextPrimaryColor.A = 0, Color.Black, TextPrimaryColor)
                dgv.DefaultCellStyle.BackColor = If(GridRowColor.A = 0, Color.White, GridRowColor)
                dgv.DefaultCellStyle.ForeColor = If(TextPrimaryColor.A = 0, Color.Black, TextPrimaryColor)

                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215)
                dgv.DefaultCellStyle.SelectionForeColor = Color.White

                dgv.AlternatingRowsDefaultCellStyle.BackColor = If(GridAltRowColor.A = 0, Color.FromArgb(249, 250, 251), GridAltRowColor)
            End If

            ' --- 6. الأزرار ---
            If TypeOf ctrl Is Button Then
                Dim btn As Button = DirectCast(ctrl, Button)
                If btn.Tag IsNot Nothing AndAlso btn.Tag.ToString().ToUpper() = "IGNORE" Then Continue For

                ' الحفاظ على حجم خط الأزرار الأصلي
                btn.Font = New Font("Segoe UI", btn.Font.Size, btn.Font.Style)

                btn.FlatStyle = FlatStyle.Flat
                btn.FlatAppearance.BorderSize = 0

                If btn.Tag IsNot Nothing Then
                    Select Case btn.Tag.ToString().ToUpper()
                        Case "SAVE", "PAY", "CONFIRM", "PRIMARY" : btn.BackColor = BtnSaveBackColor : btn.ForeColor = BtnSaveForeColor
                        Case "DELETE", "CANCEL", "CLOSE" : btn.BackColor = BtnDeleteBackColor : btn.ForeColor = BtnDeleteForeColor
                        Case "PRINT", "REPORT" : btn.BackColor = BtnPrintBackColor : btn.ForeColor = BtnPrintForeColor
                        Case "GENERAL", "SHORTCUT" : btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
                        Case "CATEGORY" : btn.BackColor = POSCatBack : btn.ForeColor = POSCatFore
                        Case "ITEM" : btn.BackColor = POSItemBack : btn.ForeColor = POSItemFore
                        Case "NUMPAD" : btn.BackColor = NumpadBack : btn.ForeColor = NumpadFore
                        Case "TRANSPARENT"
                            btn.BackColor = If(btn.Parent IsNot Nothing, btn.Parent.BackColor, Color.Transparent)
                            btn.ForeColor = TextPrimaryColor
                        Case "APP_CONTROL"
                            btn.BackColor = HeaderBackColor
                            btn.ForeColor = GetContrastColor(HeaderBackColor)
                            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, GetContrastColor(HeaderBackColor))
                        Case Else : btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
                    End Select
                Else
                    btn.BackColor = BtnGeneralBackColor : btn.ForeColor = BtnGeneralForeColor
                End If
            End If

            If ctrl.HasChildren Then ApplyThemeToControls(ctrl.Controls)
        Next
    End Sub
End Class