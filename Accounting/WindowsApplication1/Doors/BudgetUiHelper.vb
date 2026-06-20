Imports System.Drawing
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Forms

Public Module BudgetUiHelper

    Public Sub ApplyBudgetFormStyle(frm As Form)
        If frm Is Nothing Then Exit Sub

        frm.FormBorderStyle = FormBorderStyle.Sizable
        frm.MaximizeBox = True
        frm.MinimizeBox = True
        frm.MinimumSize = New Size(Math.Min(Math.Max(frm.ClientSize.Width, 1050), 1300), Math.Min(Math.Max(frm.ClientSize.Height, 650), 760))

        StyleControls(frm.Controls)
    End Sub

    Public Function TryReadMoneyInput(input As TextBox, fieldCaption As String, ByRef value As Decimal, Optional allowZero As Boolean = False) As Boolean
        value = 0D

        If input Is Nothing Then
            MessageBox.Show("حقل " & fieldCaption & " غير موجود.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If Not TryParseMoneyText(input.Text, value) Then
            MessageBox.Show("قيمة " & fieldCaption & " غير صحيحة. الرجاء إدخال رقم مالي صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            input.Focus()
            input.SelectAll()
            Return False
        End If

        If Not allowZero AndAlso value <= 0D Then
            MessageBox.Show("قيمة " & fieldCaption & " يجب أن تكون أكبر من صفر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            input.Focus()
            input.SelectAll()
            Return False
        End If

        Return True
    End Function

    Public Function TryParseMoneyText(text As String, ByRef value As Decimal) As Boolean
        value = 0D
        If String.IsNullOrWhiteSpace(text) Then Return False

        Dim normalized As String = NormalizeMoneyText(text)

        If Decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.CurrentCulture, value) Then Return True
        If Decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, value) Then Return True

        normalized = normalized.Replace(",", "")
        Return Decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, value)
    End Function

    Private Function NormalizeMoneyText(text As String) As String
        Dim normalized As String = text.Trim()
        normalized = normalized.Replace("دينار", "")
        normalized = normalized.Replace("د.ل", "")
        normalized = normalized.Replace(" ", "")

        Dim arabicDigits As String = "٠١٢٣٤٥٦٧٨٩"
        Dim persianDigits As String = "۰۱۲۳۴۵۶۷۸۹"

        For i As Integer = 0 To 9
            normalized = normalized.Replace(arabicDigits(i), ChrW(AscW("0"c) + i))
            normalized = normalized.Replace(persianDigits(i), ChrW(AscW("0"c) + i))
        Next

        normalized = normalized.Replace("٫", ".")
        normalized = normalized.Replace("٬", ",")

        Return normalized
    End Function

    Private Sub StyleControls(controls As Control.ControlCollection)
        For Each ctrl As Control In controls
            If TypeOf ctrl Is Button Then
                StyleButton(DirectCast(ctrl, Button))
            ElseIf TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Font = New Font("Segoe UI Semibold", 9.75!, FontStyle.Bold)
            ElseIf TypeOf ctrl Is ComboBox Then
                DirectCast(ctrl, ComboBox).Font = New Font("Segoe UI Semibold", 9.75!, FontStyle.Bold)
            ElseIf TypeOf ctrl Is DateTimePicker Then
                DirectCast(ctrl, DateTimePicker).Font = New Font("Segoe UI Semibold", 9.75!, FontStyle.Bold)
            ElseIf TypeOf ctrl Is DataGridView Then
                StyleGrid(DirectCast(ctrl, DataGridView))
            ElseIf TypeOf ctrl Is Panel Then
                StylePanel(DirectCast(ctrl, Panel))
            End If

            If ctrl.HasChildren Then
                StyleControls(ctrl.Controls)
            End If

            If TypeOf ctrl Is Panel Then
                ArrangeActionPanel(DirectCast(ctrl, Panel))
            End If
        Next
    End Sub

    Private Sub StyleButton(btn As Button)
        btn.Font = New Font("Segoe UI Semibold", 9.75!, FontStyle.Bold)
        btn.FlatStyle = FlatStyle.Flat
        If btn.Height < 36 Then btn.Height = 36

        Select Case btn.Name
            Case "btnNew"
                btn.Text = "+ عملية جديدة"
            Case "btnSave"
                btn.Text = "✓ حفظ البيانات"
            Case "btnDelete"
                btn.Text = "× حذف / تعطيل"
            Case "btnRefresh"
                btn.Text = "↻ تحديث البيانات"
            Case "btnExit", "exit_Btn", "btnClose"
                btn.Text = "⟵ خروج"
            Case "btnConvert"
                btn.Text = "✓ تحويل الحجز إلى صرف"
            Case "btnTransfer"
                btn.Text = "⇄ تنفيذ التحويل"
            Case "btnPrint"
                btn.Text = "⎙ طباعة التقرير"
            Case "btnExport", "btnExportPdf"
                btn.Text = "⇩ تصدير PDF"
            Case "Door_print_Btn"
                btn.Text = "⎙ موقف الأبواب"
            Case "Chapters_Print_btn"
                btn.Text = "⎙ موقف الفصول"
            Case "Items_Print_btn"
                btn.Text = "⎙ موقف البنود"
            Case "ItemsMV_Print_btn"
                btn.Text = "⎙ حركة بند"
            Case "btnShowTimeline"
                btn.Text = "◷ عرض حركة الحجز"
        End Select

        If btn.Name = "btnExit" OrElse btn.Name = "exit_Btn" OrElse btn.Name = "btnClose" Then
            btn.BackColor = Color.FromArgb(220, 53, 69)
            btn.ForeColor = Color.White
            btn.UseVisualStyleBackColor = False
        End If

        If btn.Text.Length > 10 AndAlso btn.Width < 130 Then
            btn.Width = 130
        End If
    End Sub

    Private Sub ArrangeActionPanel(panel As Panel)
        If panel.Name <> "pnlActions" AndAlso panel.Name <> "PanelFooter" Then Exit Sub

        Dim buttons As New List(Of Button)
        For Each ctrl As Control In panel.Controls
            If TypeOf ctrl Is Button Then
                buttons.Add(DirectCast(ctrl, Button))
            End If
        Next

        buttons.Sort(Function(a, b) b.Left.CompareTo(a.Left))

        Dim x As Integer = panel.Width - 16
        For Each btn As Button In buttons
            If Not btn.Visible Then Continue For
            x -= btn.Width
            btn.Left = Math.Max(8, x)
            btn.Top = Math.Max(4, CInt((panel.Height - btn.Height) / 2))
            x -= 10
        Next
    End Sub

    Private Sub StyleGrid(grid As DataGridView)
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersHeight = Math.Max(grid.ColumnHeadersHeight, 38)
        grid.RowTemplate.Height = Math.Max(grid.RowTemplate.Height, 34)
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)
        grid.DefaultCellStyle.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 242, 245)
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 35, 35)
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 237, 255)
        grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30)
        grid.GridColor = Color.FromArgb(235, 235, 235)
    End Sub

    Private Sub StylePanel(panel As Panel)
        If panel.Name = "pnlSummary" OrElse panel.Name = "cardSummary" Then
            panel.BackColor = Color.FromArgb(248, 250, 252)
            For Each ctrl As Control In panel.Controls
                If TypeOf ctrl Is Label Then
                    Dim lbl = DirectCast(ctrl, Label)
                    lbl.Font = New Font("Segoe UI Semibold", 9.25!, FontStyle.Bold)
                    lbl.TextAlign = ContentAlignment.MiddleCenter

                    Select Case lbl.Name
                        Case "Label4", "lblFromAllocatedCap"
                            lbl.Text = "إجمالي الاعتماد"
                        Case "Label3", "lblFromSpentCap"
                            lbl.Text = "إجمالي المصروف"
                        Case "Label2", "lblFromReservedCap"
                            lbl.Text = "إجمالي الحجوزات"
                        Case "Label1", "lblFromAvailableCap"
                            lbl.Text = "الرصيد المتاح للصرف"
                    End Select
                End If
            Next
        End If
    End Sub

End Module
