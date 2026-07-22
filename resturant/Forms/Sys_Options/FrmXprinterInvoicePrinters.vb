Imports System.Diagnostics
Imports System.Net
Imports System.Threading.Tasks

Public Class FrmXprinterInvoicePrinters

    Private PrintersList As New List(Of PrinterDeviceInfo)()

    Private Async Sub FrmXprinterInvoicePrinters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ThemeManager.ApplyThemeToForm(Me)
        Catch
        End Try

        If PrinterProvisioningService.IsRunningAsAdministrator() = False Then
            WriteLog("تنبيه: تعريف أو تحديث طابعة Windows قد يحتاج تشغيل البرنامج كمسؤول.")
        End If

        Await RefreshAllAsync()
    End Sub

    Private Async Function RefreshAllAsync() As Task
        SetBusy(True, "جاري تحميل الطابعات والتعريفات...")

        Try
            Await LoadDriversAsync()
            Await LoadPrintersAsync()
            lblStatus.Text = "تم تحميل بيانات الطابعات"
        Catch ex As Exception
            lblStatus.Text = "تعذر تحميل بيانات الطابعات"
            WriteLog(ex.Message)
            MessageBox.Show(ex.Message, "طابعات XPRINTER", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            SetBusy(False)
        End Try
    End Function

    Private Async Function LoadDriversAsync() As Task
        cmbDriverName.Items.Clear()

        Dim drivers As List(Of String) = Await PrinterProvisioningService.GetInstalledDriverNamesAsync()

        For Each driverName As String In drivers
            cmbDriverName.Items.Add(driverName)
        Next

        If cmbDriverName.Items.Count > 0 AndAlso cmbDriverName.SelectedIndex < 0 Then cmbDriverName.SelectedIndex = 0
    End Function

    Private Async Function LoadPrintersAsync() As Task
        PrintersList = Await PrinterProvisioningService.GetInstalledPrintersAsync()
        dgvPrinters.DataSource = Nothing
        dgvPrinters.DataSource = PrintersList
        WriteLog("تم تحميل عدد الطابعات: " & PrintersList.Count.ToString())
    End Function

    Private Sub dgvPrinters_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPrinters.SelectionChanged
        Dim info As PrinterDeviceInfo = GetSelectedPrinter()
        If info Is Nothing Then Exit Sub

        txtPrinterName.Text = info.Name
        cmbDriverName.Text = info.DriverName
        txtPrinterIp.Text = info.PrinterIp

        Dim portValue As Integer = 9100
        If Integer.TryParse(info.PortNumber, portValue) AndAlso portValue > 0 AndAlso portValue <= 65535 Then
            nudPort.Value = portValue
        Else
            nudPort.Value = 9100
        End If

        SuggestNetworkFieldsFromCurrentIp()
    End Sub

    Private Async Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Await RefreshAllAsync()
    End Sub

    Private Async Sub btnCheckConnection_Click(sender As Object, e As EventArgs) Handles btnCheckConnection.Click
        If ValidatePrinterInputs(False) = False Then Exit Sub

        SetBusy(True, "جاري فحص الاتصال...")

        Try
            Dim reachable As Boolean = Await PrinterProvisioningService.IsPrinterPortOpenAsync(txtPrinterIp.Text.Trim(), CInt(nudPort.Value))

            If reachable Then
                lblStatus.Text = "الطابعة تستجيب على المنفذ المحدد"
                WriteLog("نجح فحص الاتصال: " & txtPrinterIp.Text.Trim() & ":" & CInt(nudPort.Value).ToString())
                MessageBox.Show("الطابعة تستجيب على هذا العنوان والمنفذ.", "فحص الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                lblStatus.Text = "لم يستجب منفذ الطابعة"
                WriteLog("فشل فحص الاتصال: " & txtPrinterIp.Text.Trim() & ":" & CInt(nudPort.Value).ToString())
                MessageBox.Show("لم يستجب منفذ الطابعة. تحقق من الشبكة أو IP أو تشغيل الطابعة.", "فحص الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            lblStatus.Text = "فشل فحص الاتصال"
            WriteLog(ex.Message)
            MessageBox.Show(ex.Message, "فحص الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetBusy(False)
        End Try
    End Sub

    Private Async Sub btnInstallPrinter_Click(sender As Object, e As EventArgs) Handles btnInstallPrinter.Click
        If ValidatePrinterInputs(True) = False Then Exit Sub

        If PrinterProvisioningService.IsRunningAsAdministrator() = False Then
            Dim result As DialogResult = MessageBox.Show(
                "عملية تعريف أو تحديث طابعة Windows قد تحتاج صلاحية Administrator." & vbNewLine &
                "إذا فشلت العملية شغل البرنامج كمسؤول ثم أعد المحاولة." & vbNewLine &
                "هل تريد المتابعة الآن؟",
                "صلاحيات Windows",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If result <> DialogResult.Yes Then Exit Sub
        End If

        SetBusy(True, "جاري فحص الاتصال قبل التعريف...")

        Try
            Dim ipAddress As String = txtPrinterIp.Text.Trim()
            Dim portNumber As Integer = CInt(nudPort.Value)
            Dim reachable As Boolean = Await PrinterProvisioningService.IsPrinterPortOpenAsync(ipAddress, portNumber)

            If reachable = False Then
                Dim result As DialogResult = MessageBox.Show(
                    "لم يستجب منفذ الطابعة. هل تريد متابعة التعريف في Windows رغم ذلك؟",
                    "فحص الاتصال",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)

                If result <> DialogResult.Yes Then
                    lblStatus.Text = "تم إلغاء التعريف"
                    WriteLog("تم إلغاء تعريف الطابعة بسبب فشل فحص الاتصال.")
                    Return
                End If
            End If

            lblStatus.Text = "جاري تعريف أو تحديث الطابعة في Windows..."
            WriteLog("بدء تعريف الطابعة: " & txtPrinterName.Text.Trim() & " / " & ipAddress & ":" & portNumber.ToString())

            Dim output As String = Await PrinterProvisioningService.InstallOrUpdateNetworkPrinterAsync(
                txtPrinterName.Text.Trim(),
                cmbDriverName.Text.Trim(),
                ipAddress,
                portNumber)

            WriteLog(output)
            lblStatus.Text = "تم تعريف أو تحديث الطابعة بنجاح"
            MessageBox.Show("تم تعريف الطابعة وربطها بعنوان IP في Windows.", "طابعات XPRINTER", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Await LoadPrintersAsync()
        Catch ex As Exception
            lblStatus.Text = "فشلت عملية تعريف الطابعة"
            WriteLog(ex.ToString())
            MessageBox.Show(ex.Message, "طابعات XPRINTER", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetBusy(False)
        End Try
    End Sub

    Private Async Sub btnPrintTest_Click(sender As Object, e As EventArgs) Handles btnPrintTest.Click
        Dim printerName As String = txtPrinterName.Text.Trim()

        If String.IsNullOrWhiteSpace(printerName) Then
            MessageBox.Show("اختر أو أدخل اسم الطابعة أولاً.", "طباعة اختبار", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        SetBusy(True, "جاري إرسال طباعة الاختبار...")

        Try
            Dim result As String = Await PrinterProvisioningService.PrintTestPageAsync(printerName)
            lblStatus.Text = "تم إرسال طباعة الاختبار"
            WriteLog(result)
            MessageBox.Show(result, "طباعة اختبار", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            lblStatus.Text = "فشلت طباعة الاختبار"
            WriteLog(ex.Message)
            MessageBox.Show(ex.Message, "طباعة اختبار", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetBusy(False)
        End Try
    End Sub

    Private Sub btnOpenPrinterWebPage_Click(sender As Object, e As EventArgs) Handles btnOpenPrinterWebPage.Click
        Dim address As IPAddress = Nothing

        If PrinterProvisioningService.TryParseIPv4(txtPrinterIp.Text.Trim(), address) = False Then
            MessageBox.Show("أدخل IP الطابعة الحالي أولاً.", "فتح صفحة الطابعة", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try
            Dim url As String = "http://" & address.ToString()
            Process.Start(New ProcessStartInfo(url) With {.UseShellExecute = True})
            WriteLog("تم فتح صفحة إعداد الطابعة: " & url)
        Catch ex As Exception
            WriteLog(ex.Message)
            MessageBox.Show(ex.Message, "فتح صفحة الطابعة", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnCheckStaticIp_Click(sender As Object, e As EventArgs) Handles btnCheckStaticIp.Click
        If ValidatePrinterInputs(False) = False Then Exit Sub
        If ValidateStaticIpInputs() = False Then Exit Sub

        Dim currentIp As String = txtPrinterIp.Text.Trim()
        Dim newIp As String = txtNewPrinterIp.Text.Trim()
        Dim portNumber As Integer = CInt(nudPort.Value)

        SetBusy(True, "جاري فحص Static IP...")

        Try
            If String.Equals(currentIp, newIp, StringComparison.OrdinalIgnoreCase) Then
                lblStatus.Text = "العنوان الجديد هو نفس عنوان الطابعة الحالي"
                MessageBox.Show("IP الجديد هو نفس IP الطابعة الحالي. اختر عنواناً ثابتاً جديداً إذا كنت تريد تغيير العنوان.", "فحص Static IP", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim currentReachable As Boolean = Await PrinterProvisioningService.IsPrinterPortOpenAsync(currentIp, portNumber)
            Dim newHostReachable As Boolean = Await PrinterProvisioningService.IsHostReachableAsync(newIp)
            Dim newPortOpen As Boolean = Await PrinterProvisioningService.IsPrinterPortOpenAsync(newIp, portNumber)

            WriteLog("فحص IP الحالي: " & currentIp & ":" & portNumber.ToString() & " = " & If(currentReachable, "OK", "No Response"))
            WriteLog("فحص IP الجديد: " & newIp & " = " & If(newHostReachable OrElse newPortOpen, "Used", "Available"))

            If newHostReachable OrElse newPortOpen Then
                lblStatus.Text = "Static IP الجديد مستخدم أو يستجيب على الشبكة"
                MessageBox.Show("العنوان الجديد يستجيب على الشبكة أو المنفذ مفتوح، وهذا يعني أنه قد يكون مستخدماً من جهاز آخر. اختر IP مختلفاً لتجنب التعارض.", "فحص Static IP", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If currentReachable = False Then
                lblStatus.Text = "IP الحالي لا يستجيب، لكن العنوان الجديد غير مستخدم"
                MessageBox.Show("IP الجديد لا يظهر مستخدماً، لكن الطابعة الحالية لا تستجيب على IP الحالي. تحقق من توصيل الطابعة قبل تغيير إعداداتها.", "فحص Static IP", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            lblStatus.Text = "Static IP مناسب مبدئياً"
            MessageBox.Show("IP الجديد مناسب مبدئياً وغير مستخدم حالياً. افتح صفحة الطابعة وغيّر العنوان داخلها، ثم اضغط تحديث Windows.", "فحص Static IP", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            lblStatus.Text = "فشل فحص Static IP"
            WriteLog(ex.Message)
            MessageBox.Show(ex.Message, "فحص Static IP", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetBusy(False)
        End Try
    End Sub

    Private Async Sub btnApplyStaticIpToWindows_Click(sender As Object, e As EventArgs) Handles btnApplyStaticIpToWindows.Click
        If ValidatePrinterInputs(True) = False Then Exit Sub
        If ValidateStaticIpInputs() = False Then Exit Sub

        If PrinterProvisioningService.IsRunningAsAdministrator() = False Then
            Dim adminResult As DialogResult = MessageBox.Show(
                "تحديث منفذ الطابعة في Windows قد يحتاج صلاحية Administrator." & vbNewLine &
                "إذا فشلت العملية شغّل البرنامج كمسؤول ثم أعد المحاولة." & vbNewLine &
                "هل تريد المتابعة الآن؟",
                "صلاحيات Windows",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If adminResult <> DialogResult.Yes Then Exit Sub
        End If

        Dim newIp As String = txtNewPrinterIp.Text.Trim()
        Dim portNumber As Integer = CInt(nudPort.Value)

        SetBusy(True, "جاري تحديث منفذ Windows...")

        Try
            Dim reachable As Boolean = Await PrinterProvisioningService.IsPrinterPortOpenAsync(newIp, portNumber)

            If reachable = False Then
                Dim continueResult As DialogResult = MessageBox.Show(
                    "الطابعة لا تستجيب على IP الجديد. غالباً لم يتم تطبيق Static IP داخل الطابعة بعد." & vbNewLine &
                    "هل تريد تحديث Windows إلى هذا العنوان رغم ذلك؟",
                    "تحديث Windows",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)

                If continueResult <> DialogResult.Yes Then
                    lblStatus.Text = "تم إلغاء تحديث Windows"
                    WriteLog("تم إلغاء تحديث Windows لأن IP الجديد لا يستجيب: " & newIp)
                    Return
                End If
            End If

            WriteLog("تحديث تعريف Windows للطابعة: " & txtPrinterName.Text.Trim() & " / " & newIp & ":" & portNumber.ToString())

            Dim output As String = Await PrinterProvisioningService.InstallOrUpdateNetworkPrinterAsync(
                txtPrinterName.Text.Trim(),
                cmbDriverName.Text.Trim(),
                newIp,
                portNumber)

            txtPrinterIp.Text = newIp
            WriteLog(output)
            lblStatus.Text = "تم تحديث منفذ Windows إلى Static IP"
            MessageBox.Show("تم تحديث تعريف الطابعة في Windows وربطه بالـ Static IP الجديد.", "تحديث Windows", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Await LoadPrintersAsync()
        Catch ex As Exception
            lblStatus.Text = "فشل تحديث Windows"
            WriteLog(ex.ToString())
            MessageBox.Show(ex.Message, "تحديث Windows", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetBusy(False)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Function GetSelectedPrinter() As PrinterDeviceInfo
        If dgvPrinters.CurrentRow Is Nothing Then Return Nothing
        Return TryCast(dgvPrinters.CurrentRow.DataBoundItem, PrinterDeviceInfo)
    End Function

    Private Function ValidatePrinterInputs(requireDriver As Boolean) As Boolean
        If String.IsNullOrWhiteSpace(txtPrinterName.Text) Then
            MessageBox.Show("أدخل اسم الطابعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPrinterName.Focus()
            Return False
        End If

        If requireDriver AndAlso String.IsNullOrWhiteSpace(cmbDriverName.Text) Then
            MessageBox.Show("اختر تعريف الطابعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbDriverName.Focus()
            Return False
        End If

        Dim address As IPAddress = Nothing
        If PrinterProvisioningService.TryParseIPv4(txtPrinterIp.Text.Trim(), address) = False Then
            MessageBox.Show("أدخل عنوان IPv4 صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPrinterIp.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function ValidateStaticIpInputs() As Boolean
        Dim address As IPAddress = Nothing

        If PrinterProvisioningService.TryParseIPv4(txtNewPrinterIp.Text.Trim(), address) = False Then
            MessageBox.Show("أدخل Static IP صحيح للطابعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNewPrinterIp.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtSubnetMask.Text) = False AndAlso
            PrinterProvisioningService.TryParseIPv4(txtSubnetMask.Text.Trim(), address) = False Then

            MessageBox.Show("أدخل Subnet Mask صحيح أو اتركه فارغاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtSubnetMask.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtGateway.Text) = False AndAlso
            PrinterProvisioningService.TryParseIPv4(txtGateway.Text.Trim(), address) = False Then

            MessageBox.Show("أدخل Gateway صحيح أو اتركه فارغاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtGateway.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub SuggestNetworkFieldsFromCurrentIp()
        Dim address As IPAddress = Nothing

        If PrinterProvisioningService.TryParseIPv4(txtPrinterIp.Text.Trim(), address) = False Then Exit Sub

        If String.IsNullOrWhiteSpace(txtNewPrinterIp.Text) Then txtNewPrinterIp.Text = address.ToString()
        If String.IsNullOrWhiteSpace(txtSubnetMask.Text) Then txtSubnetMask.Text = "255.255.255.0"

        If String.IsNullOrWhiteSpace(txtGateway.Text) Then
            Dim parts As String() = address.ToString().Split("."c)

            If parts.Length = 4 Then
                txtGateway.Text = parts(0) & "." & parts(1) & "." & parts(2) & ".1"
            End If
        End If
    End Sub

    Private Sub SetBusy(isBusy As Boolean, Optional statusText As String = "")
        UseWaitCursor = isBusy
        btnRefresh.Enabled = Not isBusy
        btnCheckConnection.Enabled = Not isBusy
        btnInstallPrinter.Enabled = Not isBusy
        btnPrintTest.Enabled = Not isBusy
        btnOpenPrinterWebPage.Enabled = Not isBusy
        btnCheckStaticIp.Enabled = Not isBusy
        btnApplyStaticIpToWindows.Enabled = Not isBusy

        If String.IsNullOrWhiteSpace(statusText) = False Then lblStatus.Text = statusText
    End Sub

    Private Sub WriteLog(message As String)
        If txtLog Is Nothing Then Exit Sub

        txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") & " - " & message & Environment.NewLine)
    End Sub

End Class
