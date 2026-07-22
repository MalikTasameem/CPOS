Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports System.Security.Cryptography
Imports System.Security.Principal
Imports System.Text

Public Class ScheduledBackupSettings
    Public Property BackupPath As String
    Public Property KeepCount As Integer
    Public Property CleanupMode As String
    Public Property CleanupEnabled As Boolean
    Public Property UseCompression As Boolean
    Public Property RunTime As DateTime
End Class

Public NotInheritable Class ScheduledBackupManager
    Private Const TaskName As String = "CPOS Database Backup"
    Private Shared ReadOnly RootPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "CPOS", "DatabaseBackup")
    Private Shared ReadOnly RunnerPath As String = Path.Combine(RootPath, "RunDatabaseBackup.ps1")
    Private Shared ReadOnly ConfigPath As String = Path.Combine(RootPath, "Backup.config")

    Private Sub New()
    End Sub

    Public Shared Function LoadSettings() As ScheduledBackupSettings
        Dim result As New ScheduledBackupSettings With {
            .BackupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "CPOS", "SqlBackups"),
            .KeepCount = 30,
            .CleanupMode = "Count",
            .CleanupEnabled = True,
            .UseCompression = True,
            .RunTime = Date.Today.AddHours(23)
        }

        If Not File.Exists(ConfigPath) Then Return result

        For Each line As String In File.ReadAllLines(ConfigPath, Encoding.UTF8)
            Dim separatorIndex As Integer = line.IndexOf("="c)
            If separatorIndex <= 0 Then Continue For
            Dim key As String = line.Substring(0, separatorIndex).Trim()
            Dim value As String = line.Substring(separatorIndex + 1)
            Select Case key
                Case "BackupPath"
                    result.BackupPath = DecodeValue(value)
                Case "KeepCount"
                    Integer.TryParse(value, result.KeepCount)
                Case "CleanupEnabled"
                    Boolean.TryParse(value, result.CleanupEnabled)
                Case "CleanupMode"
                    result.CleanupMode = If(value = "Days", "Days", "Count")
                Case "UseCompression"
                    Boolean.TryParse(value, result.UseCompression)
                Case "RunTime"
                    Dim parsedTime As TimeSpan
                    If TimeSpan.TryParse(value, parsedTime) Then result.RunTime = Date.Today.Add(parsedTime)
            End Select
        Next
        Return result
    End Function

    Public Shared Sub SaveAndInstall(settings As ScheduledBackupSettings, connectionString As String)
        ValidateSettings(settings)
        Directory.CreateDirectory(RootPath)
        Directory.CreateDirectory(settings.BackupPath)

        Dim builder As New SqlConnectionStringBuilder(connectionString)
        If String.IsNullOrWhiteSpace(builder.DataSource) OrElse String.IsNullOrWhiteSpace(builder.InitialCatalog) Then
            Throw New InvalidOperationException("بيانات خادم SQL أو اسم قاعدة البيانات غير مكتملة.")
        End If

        Dim encryptedPassword As String = String.Empty
        If Not builder.IntegratedSecurity Then encryptedPassword = ProtectValue(builder.Password)

        Dim lines As String() = {
            "BackupPath=" & EncodeValue(settings.BackupPath),
            "KeepCount=" & settings.KeepCount.ToString(),
            "CleanupEnabled=" & settings.CleanupEnabled.ToString(),
            "CleanupMode=" & settings.CleanupMode,
            "UseCompression=" & settings.UseCompression.ToString(),
            "RunTime=" & settings.RunTime.ToString("HH\:mm"),
            "Server=" & EncodeValue(builder.DataSource),
            "Database=" & EncodeValue(builder.InitialCatalog),
            "IntegratedSecurity=" & builder.IntegratedSecurity.ToString(),
            "UserName=" & EncodeValue(builder.UserID),
            "EncryptedPassword=" & encryptedPassword
        }
        File.WriteAllLines(ConfigPath, lines, New UTF8Encoding(False))
        File.WriteAllText(RunnerPath, BuildRunnerScript(), New UTF8Encoding(False))
        RestrictFolderPermissions()
        RegisterTask(settings.RunTime)
    End Sub

    Public Shared Sub RunNow()
        If Not File.Exists(RunnerPath) OrElse Not File.Exists(ConfigPath) Then
            Throw New InvalidOperationException("احفظ إعدادات الجدولة أولًا.")
        End If
        StartPowerShell(RunnerPath, False)
    End Sub

    Public Shared Sub RemoveTask()
        RunElevated("schtasks.exe", "/Delete /TN """ & TaskName & """ /F")
    End Sub

    Public Shared Function GetTaskStatus() As String
        Try
            Dim serviceType As Type = Type.GetTypeFromProgID("Schedule.Service")
            If serviceType Is Nothing Then Return "تعذر الاتصال بخدمة Task Scheduler"
            Dim service As Object = Activator.CreateInstance(serviceType)
            service.Connect()
            Dim rootFolder As Object = service.GetFolder("\")
            Dim task As Object = rootFolder.GetTask(TaskName)
            Dim stateText As String = GetTaskStateText(Convert.ToInt32(task.State))
            Dim enabledText As String = If(Convert.ToBoolean(task.Enabled), "مفعلة", "معطلة")
            Dim lastRun As DateTime = Convert.ToDateTime(task.LastRunTime)
            Dim nextRun As DateTime = Convert.ToDateTime(task.NextRunTime)
            Dim lastResult As Integer = Convert.ToInt32(task.LastTaskResult)
            Dim lastRunText As String = If(lastRun.Year <= 1900, "لم تعمل بعد", lastRun.ToString("yyyy-MM-dd HH:mm"))
            Dim nextRunText As String = If(nextRun.Year <= 1900, "غير محدد", nextRun.ToString("yyyy-MM-dd HH:mm"))
            Dim resultText As String = If(lastResult = 0, "ناجح (0)", "رمز " & lastResult.ToString())
            Return enabledText & " - " & stateText & Environment.NewLine &
                   "آخر تشغيل: " & lastRunText & " | النتيجة: " & resultText & Environment.NewLine &
                   "التشغيل القادم: " & nextRunText
        Catch ex As UnauthorizedAccessException
            Return GetRestrictedTaskStatus()
        Catch ex As Runtime.InteropServices.COMException
            If ex.ErrorCode = -2147024891 Then Return GetRestrictedTaskStatus()
            Return "غير مسجلة"
        Catch ex As Exception
            Return "تعذر قراءة الحالة: " & ex.Message
        End Try
    End Function

    Private Shared Function GetRestrictedTaskStatus() As String
        Try
            Dim startInfo As New ProcessStartInfo("schtasks.exe", "/Query /TN """ & TaskName & """") With {
                .UseShellExecute = False,
                .CreateNoWindow = True,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True
            }
            Using process As Process = Process.Start(startInfo)
                process.WaitForExit()
                If process.ExitCode = 0 Then Return "مسجلة في Task Scheduler" & Environment.NewLine & "تفاصيلها محمية وتتطلب صلاحية Administrator"
            End Using
        Catch
        End Try
        If File.Exists(ConfigPath) Then
            Return "تم حفظ إعدادات الجدولة سابقًا" & Environment.NewLine & "التحقق من حالة المهمة يتطلب صلاحية Administrator"
        End If
        Return "لا توجد إعدادات محفوظة، أو أن قراءة المهمة تتطلب صلاحية Administrator"
    End Function

    Private Shared Function GetTaskStateText(state As Integer) As String
        Select Case state
            Case 1 : Return "معطلة"
            Case 2 : Return "في قائمة الانتظار"
            Case 3 : Return "جاهزة"
            Case 4 : Return "تعمل الآن"
            Case Else : Return "حالة غير معروفة"
        End Select
    End Function

    Public Shared Sub TestBackupPath(pathValue As String)
        If String.IsNullOrWhiteSpace(pathValue) Then Throw New InvalidOperationException("حدد مسار النسخ أولًا.")
        Directory.CreateDirectory(pathValue)
        Dim testFile As String = Path.Combine(pathValue, "CPOS_WriteTest_" & Guid.NewGuid().ToString("N") & ".tmp")
        File.WriteAllText(testFile, "test", Encoding.UTF8)
        File.Delete(testFile)
    End Sub

    Private Shared Sub ValidateSettings(settings As ScheduledBackupSettings)
        If settings Is Nothing Then Throw New ArgumentNullException(NameOf(settings))
        If String.IsNullOrWhiteSpace(settings.BackupPath) Then Throw New InvalidOperationException("حدد مسار حفظ النسخ.")
        If settings.KeepCount < 1 OrElse settings.KeepCount > 10000 Then Throw New InvalidOperationException("عدد النسخ يجب أن يكون بين 1 و10000.")
    End Sub

    Private Shared Sub RegisterTask(runTime As DateTime)
        Dim taskCommand As String = "powershell.exe -NoProfile -ExecutionPolicy Bypass -File """ & RunnerPath & """"
        Dim escapedTaskCommand As String = taskCommand.Replace(ChrW(34), "\" & ChrW(34))
        Dim arguments As String = "/Create /TN """ & TaskName & """ /SC DAILY /ST " & runTime.ToString("HH:mm") &
                                  " /TR """ & escapedTaskCommand & """ /RU SYSTEM /RL HIGHEST /F"
        RunElevated("schtasks.exe", arguments)
    End Sub

    Private Shared Sub RunElevated(fileName As String, arguments As String)
        Dim startInfo As New ProcessStartInfo(fileName, arguments) With {
            .UseShellExecute = True,
            .Verb = "runas",
            .WindowStyle = ProcessWindowStyle.Hidden
        }
        Using process As Process = Process.Start(startInfo)
            process.WaitForExit()
            If process.ExitCode <> 0 Then Throw New InvalidOperationException("تعذر تنفيذ العملية بصلاحية Administrator. رمز الخطأ: " & process.ExitCode.ToString())
        End Using
    End Sub

    Private Shared Sub StartPowerShell(scriptPath As String, waitForExit As Boolean)
        Dim startInfo As New ProcessStartInfo("powershell.exe", "-NoProfile -ExecutionPolicy Bypass -File """ & scriptPath & """") With {
            .UseShellExecute = True,
            .WindowStyle = ProcessWindowStyle.Hidden
        }
        Dim process As Process = Process.Start(startInfo)
        If waitForExit Then process.WaitForExit()
    End Sub

    Private Shared Sub RestrictFolderPermissions()
        Dim currentSid As String = WindowsIdentity.GetCurrent().User.Value
        Dim args As String = ChrW(34) & RootPath & ChrW(34) & " /inheritance:r /grant:r ""SYSTEM:(OI)(CI)F"" ""Administrators:(OI)(CI)F"" ""*" & currentSid & ":(OI)(CI)F"""
        Dim startInfo As New ProcessStartInfo("icacls.exe", args) With {.UseShellExecute = False, .CreateNoWindow = True}
        Using process As Process = Process.Start(startInfo)
            process.WaitForExit()
        End Using
    End Sub

    Private Shared Function ProtectValue(value As String) As String
        If String.IsNullOrEmpty(value) Then Return String.Empty
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(value)
        Return Convert.ToBase64String(ProtectedData.Protect(bytes, Nothing, DataProtectionScope.LocalMachine))
    End Function

    Private Shared Function EncodeValue(value As String) As String
        Return Convert.ToBase64String(Encoding.UTF8.GetBytes(If(value, String.Empty)))
    End Function

    Private Shared Function DecodeValue(value As String) As String
        Try
            Return Encoding.UTF8.GetString(Convert.FromBase64String(value))
        Catch
            Return String.Empty
        End Try
    End Function

    Private Shared Function BuildRunnerScript() As String
        Return String.Join(Environment.NewLine, {
            "$ErrorActionPreference = 'Stop'",
            "Add-Type -AssemblyName System.Security",
            "$root = Split-Path -Parent $MyInvocation.MyCommand.Path",
            "$configFile = Join-Path $root 'Backup.config'",
            "$logDir = Join-Path $root 'Logs'",
            "New-Item -ItemType Directory -Path $logDir -Force | Out-Null",
            "$values = @{}",
            "Get-Content -LiteralPath $configFile -Encoding UTF8 | ForEach-Object { $i=$_.IndexOf('='); if($i -gt 0){$values[$_.Substring(0,$i)]=$_.Substring($i+1)}}",
            "function Decode([string]$v){ if([string]::IsNullOrEmpty($v)){return ''}; [Text.Encoding]::UTF8.GetString([Convert]::FromBase64String($v)) }",
            "$backupPath = Decode $values.BackupPath",
            "$server = Decode $values.Server",
            "$database = Decode $values.Database",
            "$userName = Decode $values.UserName",
            "$keepCount = [Math]::Max(1, [int]$values.KeepCount)",
            "$cleanupEnabled = [bool]::Parse($values.CleanupEnabled)",
            "$cleanupMode = $values.CleanupMode",
            "$useCompression = [bool]::Parse($values.UseCompression)",
            "$integrated = [bool]::Parse($values.IntegratedSecurity)",
            "$safeDb = $database -replace '[\\/:*?""<>|]', '_'",
            "$stamp = Get-Date -Format 'yyyyMMdd_HHmmss_fff'",
            "$backupFile = Join-Path $backupPath ($safeDb + '_FULL_' + $stamp + '.bak')",
            "$logFile = Join-Path $logDir ('DatabaseBackup_' + $stamp + '.log')",
            "try {",
            "  New-Item -ItemType Directory -Path $backupPath -Force | Out-Null",
            "  if($integrated){ $cs = 'Server=' + $server + ';Database=master;Integrated Security=True;Application Name=CPOS Scheduled Backup;' } else {",
            "    $protected=[Convert]::FromBase64String($values.EncryptedPassword)",
            "    $password=[Text.Encoding]::UTF8.GetString([System.Security.Cryptography.ProtectedData]::Unprotect($protected,$null,[System.Security.Cryptography.DataProtectionScope]::LocalMachine))",
            "    $cs='Server=' + $server + ';Database=master;User ID=' + $userName + ';Password=' + $password + ';Application Name=CPOS Scheduled Backup;'",
            "  }",
            "  $cn=New-Object Data.SqlClient.SqlConnection($cs); $cn.Open()",
            "  $escapedDb=$database.Replace(']',']]'); $escapedFile=$backupFile.Replace('''','''''')",
            "  $sql='BACKUP DATABASE ['+$escapedDb+'] TO DISK=N'''+$escapedFile+''' WITH INIT, CHECKSUM, STATS=10'",
            "  if($useCompression){$sql += ', COMPRESSION'}; $sql += ';'",
            "  $cmd=$cn.CreateCommand(); $cmd.CommandTimeout=0; $cmd.CommandText=$sql; $cmd.ExecuteNonQuery() | Out-Null",
            "  $cmd.CommandText='RESTORE VERIFYONLY FROM DISK=N'''+$escapedFile+''' WITH CHECKSUM;'; $cmd.ExecuteNonQuery() | Out-Null; $cn.Close()",
            "  if($cleanupEnabled){",
            "    $backupFiles = Get-ChildItem -LiteralPath $backupPath -Filter ($safeDb + '_FULL_*.bak') -File",
            "    if($cleanupMode -eq 'Days'){ $cutoff=(Get-Date).AddDays(-$keepCount); $backupFiles | Where-Object { $_.LastWriteTime -lt $cutoff } | Remove-Item -Force }",
            "    else { $backupFiles | Sort-Object LastWriteTime -Descending | Select-Object -Skip $keepCount | Remove-Item -Force }",
            "  }",
            "  ('SUCCESS | '+(Get-Date)+' | '+$backupFile) | Out-File -LiteralPath $logFile -Encoding UTF8",
            "  exit 0",
            "} catch { ('FAILED | '+(Get-Date)+' | '+$_.Exception.ToString()) | Out-File -LiteralPath $logFile -Encoding UTF8; exit 1 }"
        })
    End Function
End Class
