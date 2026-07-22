Imports System.Diagnostics
Imports System.Drawing.Printing
Imports System.Linq
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Security.Principal
Imports System.Text
Imports System.Threading.Tasks

Public Class PrinterDeviceInfo
    Public Property Name As String = ""
    Public Property DriverName As String = ""
    Public Property PortName As String = ""
    Public Property PrinterIp As String = ""
    Public Property PortNumber As String = ""
    Public Property Status As String = ""
    Public Property IsDefault As Boolean = False
End Class

Public NotInheritable Class PrinterProvisioningService

    Private Const ServiceSeparator As Char = ChrW(31)

    Private Sub New()
    End Sub

    Public Shared Function GetInstalledPrinterNames() As List(Of String)
        Return PrinterSettings.InstalledPrinters.Cast(Of String)().OrderBy(Function(name) name).ToList()
    End Function

    Public Shared Async Function GetInstalledDriverNamesAsync() As Task(Of List(Of String))
        Dim script As New StringBuilder()
        script.AppendLine("$ErrorActionPreference = 'Stop'")
        script.AppendLine("Import-Module PrintManagement")
        script.AppendLine("Get-PrinterDriver | Sort-Object Name | ForEach-Object { $_.Name }")

        Dim output As String = Await RunPowerShellAsync(script.ToString())
        Return output.Split({ControlChars.Cr, ControlChars.Lf}, StringSplitOptions.RemoveEmptyEntries).
            Select(Function(line) line.Trim()).
            Where(Function(line) String.IsNullOrWhiteSpace(line) = False).
            Distinct().
            ToList()
    End Function

    Public Shared Async Function GetInstalledPrintersAsync() As Task(Of List(Of PrinterDeviceInfo))
        Dim script As New StringBuilder()
        script.AppendLine("$ErrorActionPreference = 'Stop'")
        script.AppendLine("Import-Module PrintManagement")
        script.AppendLine("$sep = [char]31")
        script.AppendLine("$defaultName = (Get-CimInstance Win32_Printer | Where-Object { $_.Default -eq $true } | Select-Object -First 1 -ExpandProperty Name)")
        script.AppendLine("Get-Printer | Sort-Object Name | ForEach-Object {")
        script.AppendLine("    $printer = $_")
        script.AppendLine("    $port = Get-PrinterPort -Name $printer.PortName -ErrorAction SilentlyContinue")
        script.AppendLine("    $ip = ''")
        script.AppendLine("    $portNumber = ''")
        script.AppendLine("    if ($null -ne $port) {")
        script.AppendLine("        if ($port.PSObject.Properties.Name -contains 'PrinterHostAddress') { $ip = [string]$port.PrinterHostAddress }")
        script.AppendLine("        if ($port.PSObject.Properties.Name -contains 'PortNumber') { $portNumber = [string]$port.PortNumber }")
        script.AppendLine("    }")
        script.AppendLine("    $isDefault = if ($printer.Name -eq $defaultName) { 'True' } else { 'False' }")
        script.AppendLine("    Write-Output (($printer.Name, $printer.DriverName, $printer.PortName, $ip, $portNumber, $printer.PrinterStatus, $isDefault) -join $sep)")
        script.AppendLine("}")

        Dim output As String = Await RunPowerShellAsync(script.ToString())
        Dim printers As New List(Of PrinterDeviceInfo)()

        For Each line As String In output.Split({ControlChars.Cr, ControlChars.Lf}, StringSplitOptions.RemoveEmptyEntries)
            Dim parts As String() = line.Split(ServiceSeparator)
            If parts.Length < 7 Then Continue For

            printers.Add(New PrinterDeviceInfo With {
                .Name = parts(0).Trim(),
                .DriverName = parts(1).Trim(),
                .PortName = parts(2).Trim(),
                .PrinterIp = parts(3).Trim(),
                .PortNumber = parts(4).Trim(),
                .Status = parts(5).Trim(),
                .IsDefault = String.Equals(parts(6).Trim(), "True", StringComparison.OrdinalIgnoreCase)
            })
        Next

        Return printers
    End Function

    Public Shared Async Function IsPrinterPortOpenAsync(ipText As String, portNumber As Integer, Optional timeoutMilliseconds As Integer = 1500) As Task(Of Boolean)
        Dim address As IPAddress = Nothing

        If TryParseIPv4(ipText, address) = False Then
            Throw New ArgumentException("عنوان IPv4 غير صحيح.", NameOf(ipText))
        End If

        If portNumber < 1 OrElse portNumber > 65535 Then
            Throw New ArgumentOutOfRangeException(NameOf(portNumber), "رقم المنفذ غير صحيح.")
        End If

        Try
            Using client As New TcpClient()
                Dim connectTask As Task = client.ConnectAsync(address.ToString(), portNumber)
                Dim completedTask As Task = Await Task.WhenAny(connectTask, Task.Delay(timeoutMilliseconds))

                If Not Object.ReferenceEquals(completedTask, connectTask) Then Return False

                Await connectTask
                Return client.Connected
            End Using
        Catch ex As SocketException
            Return False
        Catch ex As ObjectDisposedException
            Return False
        End Try
    End Function

    Public Shared Async Function IsHostReachableAsync(ipText As String, Optional timeoutMilliseconds As Integer = 1200) As Task(Of Boolean)
        Dim address As IPAddress = Nothing

        If TryParseIPv4(ipText, address) = False Then
            Throw New ArgumentException("عنوان IPv4 غير صحيح.", NameOf(ipText))
        End If

        Try
            Using ping As New Ping()
                Dim reply As PingReply = Await ping.SendPingAsync(address, timeoutMilliseconds)
                Return reply IsNot Nothing AndAlso reply.Status = IPStatus.Success
            End Using
        Catch ex As PingException
            Return False
        Catch ex As InvalidOperationException
            Return False
        End Try
    End Function

    Public Shared Function TryParseIPv4(ipText As String, ByRef address As IPAddress) As Boolean
        address = Nothing

        If String.IsNullOrWhiteSpace(ipText) Then Return False
        If IPAddress.TryParse(ipText.Trim(), address) = False Then Return False

        Return address.AddressFamily = AddressFamily.InterNetwork
    End Function

    Public Shared Async Function InstallOrUpdateNetworkPrinterAsync(printerName As String, driverName As String, ipText As String, Optional portNumber As Integer = 9100) As Task(Of String)
        If String.IsNullOrWhiteSpace(printerName) Then
            Throw New ArgumentException("يجب إدخال اسم الطابعة.", NameOf(printerName))
        End If

        If String.IsNullOrWhiteSpace(driverName) Then
            Throw New ArgumentException("يجب اختيار تعريف الطابعة.", NameOf(driverName))
        End If

        Dim address As IPAddress = Nothing

        If TryParseIPv4(ipText, address) = False Then
            Throw New ArgumentException("عنوان IPv4 غير صحيح.", NameOf(ipText))
        End If

        If portNumber < 1 OrElse portNumber > 65535 Then
            Throw New ArgumentOutOfRangeException(NameOf(portNumber), "رقم المنفذ غير صحيح.")
        End If

        Dim normalizedIp As String = address.ToString()
        Dim windowsPortName As String = String.Format("IP_{0}_{1}", normalizedIp, portNumber)
        Dim script As New StringBuilder()

        script.AppendLine("$ErrorActionPreference = 'Stop'")
        script.AppendLine("Import-Module PrintManagement")
        script.AppendLine("$printerName = " & ToPowerShellLiteral(printerName))
        script.AppendLine("$driverName = " & ToPowerShellLiteral(driverName))
        script.AppendLine("$printerIp = " & ToPowerShellLiteral(normalizedIp))
        script.AppendLine("$portName = " & ToPowerShellLiteral(windowsPortName))
        script.AppendLine("$portNumber = " & portNumber.ToString(Globalization.CultureInfo.InvariantCulture))
        script.AppendLine("if (-not (Get-PrinterDriver -Name $driverName -ErrorAction SilentlyContinue)) {")
        script.AppendLine("    throw ""Printer driver is not installed: $driverName""")
        script.AppendLine("}")
        script.AppendLine("if (-not (Get-PrinterPort -Name $portName -ErrorAction SilentlyContinue)) {")
        script.AppendLine("    Add-PrinterPort -Name $portName -PrinterHostAddress $printerIp -PortNumber $portNumber")
        script.AppendLine("}")
        script.AppendLine("$existingPrinter = Get-Printer -Name $printerName -ErrorAction SilentlyContinue")
        script.AppendLine("if ($null -eq $existingPrinter) {")
        script.AppendLine("    Add-Printer -Name $printerName -DriverName $driverName -PortName $portName")
        script.AppendLine("} else {")
        script.AppendLine("    Set-Printer -Name $printerName -DriverName $driverName -PortName $portName")
        script.AppendLine("}")
        script.AppendLine("Get-Printer -Name $printerName | Select-Object Name, DriverName, PortName | Format-List | Out-String")

        Return Await RunPowerShellAsync(script.ToString())
    End Function

    Public Shared Async Function PrintTestPageAsync(printerName As String) As Task(Of String)
        If String.IsNullOrWhiteSpace(printerName) Then Throw New ArgumentException("يجب اختيار الطابعة.", NameOf(printerName))

        Await Task.Run(
            Sub()
                Using doc As New PrintDocument()
                    doc.DocumentName = "CPOS XPRINTER Test"
                    doc.PrinterSettings.PrinterName = printerName

                    AddHandler doc.PrintPage,
                        Sub(sender As Object, e As PrintPageEventArgs)
                            Using titleFont As New Font("Segoe UI", 14.0!, FontStyle.Bold),
                                  bodyFont As New Font("Segoe UI", 10.0!, FontStyle.Regular)

                                Dim y As Integer = 40
                                e.Graphics.DrawString("CPOS - XPRINTER Test", titleFont, Brushes.Black, 40, y)
                                y += 34
                                e.Graphics.DrawString("Printer: " & printerName, bodyFont, Brushes.Black, 40, y)
                                y += 24
                                e.Graphics.DrawString("Time: " & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), bodyFont, Brushes.Black, 40, y)
                                y += 32
                                e.Graphics.DrawString("تم إرسال صفحة اختبار من نظام CPOS.", bodyFont, Brushes.Black, 40, y)
                            End Using
                        End Sub

                    doc.Print()
                End Using
            End Sub)

        Return "تم إرسال طباعة الاختبار إلى: " & printerName
    End Function

    Public Shared Function IsRunningAsAdministrator() As Boolean
        Dim identity As WindowsIdentity = WindowsIdentity.GetCurrent()
        Dim principal As New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    Private Shared Function ToPowerShellLiteral(value As String) As String
        If value Is Nothing Then Return "$null"
        Return "'" & value.Replace("'", "''") & "'"
    End Function

    Private Shared Async Function RunPowerShellAsync(script As String) As Task(Of String)
        Dim encodedScript As String = Convert.ToBase64String(Encoding.Unicode.GetBytes(script))

        Dim startInfo As New ProcessStartInfo With {
            .FileName = GetWindowsPowerShellPath(),
            .Arguments = "-NoLogo -NoProfile -NonInteractive -ExecutionPolicy Bypass -EncodedCommand " & encodedScript,
            .UseShellExecute = False,
            .RedirectStandardOutput = True,
            .RedirectStandardError = True,
            .CreateNoWindow = True
        }

        Using process As New Process With {.StartInfo = startInfo}
            If Not process.Start() Then Throw New InvalidOperationException("تعذر تشغيل PowerShell.")

            Dim outputTask As Task(Of String) = process.StandardOutput.ReadToEndAsync()
            Dim errorTask As Task(Of String) = process.StandardError.ReadToEndAsync()

            Await Task.Run(Sub() process.WaitForExit())

            Dim output As String = Await outputTask
            Dim errors As String = Await errorTask

            If process.ExitCode <> 0 Then
                Throw New InvalidOperationException(If(String.IsNullOrWhiteSpace(errors), "فشلت عملية تنفيذ أمر الطابعة.", errors.Trim()))
            End If

            Return output.Trim()
        End Using
    End Function

    Private Shared Function GetWindowsPowerShellPath() As String
        Dim windowsDirectory As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows)
        Dim systemDirectoryName As String = If(Environment.Is64BitOperatingSystem AndAlso Not Environment.Is64BitProcess, "Sysnative", "System32")

        Return IO.Path.Combine(windowsDirectory, systemDirectoryName, "WindowsPowerShell", "v1.0", "powershell.exe")
    End Function

End Class
