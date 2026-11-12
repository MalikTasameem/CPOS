param(
    [ValidateSet('Install', 'Validate', 'PackageCheck')]
    [string]$Action = 'Install',
    [string]$ServerInstance,
    [string]$DatabaseName = 'CPOS_ACCOUNTING',
    [ValidateSet('Windows', 'Sql')]
    [string]$Authentication,
    [string]$SqlUser = 'sa',
    [Management.Automation.PSCredential]$SqlCredential,
    [string]$BackupDirectory,
    [int]$ConnectTimeout = 15
)

$ErrorActionPreference = 'Stop'
[Console]::OutputEncoding = New-Object System.Text.UTF8Encoding($false)
$OutputEncoding = [Console]::OutputEncoding

$ToolVersion = '2026.11.10.046-R5'
$ExpectedMigrationCount = 44
$DevelopmentSourceId = '3c520084-0214-43d4-a5fb-797a179c7a7c'
$EmbeddedPayloadSha256 = '__PAYLOAD_SHA256__'
$EmbeddedPayloadBase64 = '__PAYLOAD_BASE64__'
$ScriptRoot = if ($PSScriptRoot) { $PSScriptRoot } else { [Environment]::CurrentDirectory }
$LogPath = Join-Path $ScriptRoot ('CPOS_Network_Audit_Upgrade_{0}.log' -f (Get-Date -Format 'yyyyMMdd_HHmmss'))

function Write-Log {
    param([string]$Message, [ConsoleColor]$Color = [ConsoleColor]::Gray)
    $line = '[{0}] {1}' -f (Get-Date -Format 'yyyy-MM-dd HH:mm:ss'), $Message
    Add-Content -LiteralPath $LogPath -Value $line -Encoding UTF8
    Write-Host $Message -ForegroundColor $Color
}

function Get-Sha256Hex {
    param([byte[]]$Bytes)
    $sha = [Security.Cryptography.SHA256]::Create()
    try {
        return (($sha.ComputeHash($Bytes) | ForEach-Object { $_.ToString('x2') }) -join '').ToUpperInvariant()
    }
    finally {
        $sha.Dispose()
    }
}

function Get-EmbeddedMigrations {
    $compressed = [Convert]::FromBase64String($EmbeddedPayloadBase64)
    $actualPayloadHash = Get-Sha256Hex $compressed
    if ($actualPayloadHash -ne $EmbeddedPayloadSha256) {
        throw 'بصمة حزمة SQL غير صحيحة. لا تستخدم هذا الملف لأنه تالف أو تم تعديله.'
    }

    $input = New-Object System.IO.MemoryStream
    $output = New-Object System.IO.MemoryStream
    try {
        $input.Write($compressed, 0, $compressed.Length)
        $input.Position = 0
        $gzip = New-Object System.IO.Compression.GZipStream($input, [IO.Compression.CompressionMode]::Decompress, $true)
        try { $gzip.CopyTo($output) } finally { $gzip.Dispose() }
        $json = (New-Object System.Text.UTF8Encoding($false, $true)).GetString($output.ToArray())
    }
    finally {
        $input.Dispose()
        $output.Dispose()
    }

    $parsedEntries = $json | ConvertFrom-Json
    $entryList = New-Object System.Collections.ArrayList
    foreach ($parsedEntry in $parsedEntries) { [void]$entryList.Add($parsedEntry) }
    $entries = @($entryList.ToArray())
    if ($entries.Count -ne $ExpectedMigrationCount) {
        throw ('عدد ترحيلات الحزمة غير صحيح. المتوقع {0} والموجود {1}.' -f $ExpectedMigrationCount, $entries.Count)
    }

    $ids = @{}
    foreach ($entry in $entries) {
        if ($ids.ContainsKey([string]$entry.Id)) { throw ('رقم ترحيل مكرر: {0}' -f $entry.Id) }
        $ids[[string]$entry.Id] = $true
        $sqlBytes = [Convert]::FromBase64String([string]$entry.SqlBase64)
        if ((Get-Sha256Hex $sqlBytes) -ne [string]$entry.Sha256) {
            throw ('فشلت بصمة ملف الترحيل {0}.' -f $entry.Name)
        }
        $entry | Add-Member -NotePropertyName Sql -NotePropertyValue ((New-Object System.Text.UTF8Encoding($false, $true)).GetString($sqlBytes))
    }
    return $entries
}

function Read-DefaultValue {
    param([string]$Prompt, [string]$DefaultValue)
    $value = Read-Host ('{0} [{1}]' -f $Prompt, $DefaultValue)
    if ([string]::IsNullOrWhiteSpace($value)) { return $DefaultValue }
    return $value.Trim()
}

function New-ConnectionString {
    param([string]$Catalog, [Security.SecureString]$SecurePassword)
    $builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder
    $builder['Data Source'] = $ServerInstance
    $builder['Initial Catalog'] = $Catalog
    $builder['Application Name'] = 'CPOS Network Audit Upgrade'
    $builder['Connect Timeout'] = $ConnectTimeout
    $builder['Pooling'] = $false
    $builder['Encrypt'] = $false
    $builder['TrustServerCertificate'] = $true
    if ($Authentication -eq 'Windows') {
        $builder['Integrated Security'] = $true
    }
    else {
        $builder['User ID'] = $SqlUser
        $pointer = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($SecurePassword)
        try { $builder['Password'] = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($pointer) }
        finally { [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($pointer) }
    }
    return $builder.ConnectionString
}

function New-SqlCommand {
    param(
        [Data.SqlClient.SqlConnection]$Connection,
        [string]$CommandText,
        [Data.SqlClient.SqlTransaction]$Transaction = $null,
        [int]$Timeout = 60
    )
    $command = $Connection.CreateCommand()
    $command.CommandText = $CommandText
    $command.CommandTimeout = $Timeout
    if ($null -ne $Transaction) { $command.Transaction = $Transaction }
    return $command
}

function Add-Parameter {
    param([Data.SqlClient.SqlCommand]$Command, [string]$Name, [object]$Value)
    $parameter = $Command.Parameters.AddWithValue($Name, $(if ($null -eq $Value) { [DBNull]::Value } else { $Value }))
    return $parameter
}

function Get-Scalar {
    param(
        [Data.SqlClient.SqlConnection]$Connection,
        [string]$CommandText,
        [hashtable]$Parameters = @{},
        [Data.SqlClient.SqlTransaction]$Transaction = $null
    )
    $command = New-SqlCommand $Connection $CommandText $Transaction
    try {
        foreach ($key in $Parameters.Keys) { [void](Add-Parameter $command ('@' + $key) $Parameters[$key]) }
        return $command.ExecuteScalar()
    }
    finally { $command.Dispose() }
}

function Get-Table {
    param(
        [Data.SqlClient.SqlConnection]$Connection,
        [string]$CommandText,
        [hashtable]$Parameters = @{}
    )
    $command = New-SqlCommand $Connection $CommandText $null 60
    $table = New-Object System.Data.DataTable
    try {
        foreach ($key in $Parameters.Keys) { [void](Add-Parameter $command ('@' + $key) $Parameters[$key]) }
        $reader = $command.ExecuteReader()
        try { $table.Load($reader) } finally { $reader.Dispose() }
        return ,$table
    }
    finally { $command.Dispose() }
}

function Get-AuditStatus {
    param([Data.SqlClient.SqlConnection]$Connection)
    $tableCount = [int](Get-Scalar $Connection @"
SELECT COUNT(*) FROM sys.tables
WHERE schema_id=SCHEMA_ID(N'dbo')
  AND name IN (N'Network_Audit_Source',N'Network_Audit_Request',N'Network_Audit_Event',N'Network_Audit_Change');
"@)
    if ($tableCount -eq 0) {
        return [pscustomobject]@{ State='NotInstalled'; TableCount=0; SourceId=$null; Domains=$null; Base043=$false; Latest=$false; Incomplete=0; MigrationCount=0 }
    }
    if ($tableCount -ne 4) {
        return [pscustomobject]@{ State='Partial'; TableCount=$tableCount; SourceId=$null; Domains=$null; Base043=$false; Latest=$false; Incomplete=0; MigrationCount=0 }
    }

    $command = New-SqlCommand $Connection @"
SELECT s.SourceId,CONVERT(nvarchar(max),s.EnabledDomainsXml) Domains,
       (SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1) Incomplete,
       CASE WHEN OBJECT_ID(N'dbo.Network_Audit_Request_Begin',N'P') IS NOT NULL
                  AND OBJECT_ID(N'dbo.Network_Audit_Event_Write',N'P') IS NOT NULL
                  AND OBJECT_ID(N'dbo.Network_Audit_Request_Complete',N'P') IS NOT NULL
                  AND OBJECT_ID(N'dbo.Network_Audit_Request_Status',N'P') IS NOT NULL
                  AND s.EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.Receipts"]')=1
                  AND CHARINDEX(N'AuditReceipts042: administrator permission override.',ISNULL(OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write')),N''))>0
                  AND CHARINDEX(N'AuditReceipts043: safe datetime validation.',ISNULL(OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write')),N''))>0
             THEN 1 ELSE 0 END Base043,
       CASE WHEN CHARINDEX(N'Audit044: safe datetime validation across audit domains.',ISNULL(OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write')),N''))>0
             THEN 1 ELSE 0 END Base044,
       CASE WHEN CHARINDEX(N'Audit045: safe document type validation.',ISNULL(OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write')),N''))>0
             THEN 1 ELSE 0 END Base045,
       CASE WHEN CHARINDEX(N'Audit046: canonical seven-digit datetime validation.',ISNULL(OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write')),N''))>0
                  AND CHARINDEX(N'Audit045: safe document type validation.',ISNULL(OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write')),N''))>0
                  AND CHARINDEX(N'Audit044: safe datetime validation across audit domains.',ISNULL(OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write')),N''))>0
             THEN 1 ELSE 0 END Latest
FROM dbo.Network_Audit_Source s WHERE s.ConfigId=1 AND s.SchemaVersion=1;
"@
    $statusTable = New-Object System.Data.DataTable
    try {
        $reader = $command.ExecuteReader()
        try { $statusTable.Load($reader) } finally { $reader.Dispose() }
    }
    finally { $command.Dispose() }
    if ($statusTable.Rows.Count -ne 1) {
        return [pscustomobject]@{ State='Partial'; TableCount=4; SourceId=$null; Domains=$null; Base043=$false; Latest=$false; Incomplete=0; MigrationCount=0 }
    }
    $row = $statusTable.Rows[0]
    $migrationCount = 0
    if ([int](Get-Scalar $Connection "SELECT CASE WHEN OBJECT_ID(N'dbo.CPOS_Database_Migration',N'U') IS NULL THEN 0 ELSE 1 END;") -eq 1) {
        $migrationCount = [int](Get-Scalar $Connection "SELECT COUNT(*) FROM dbo.CPOS_Database_Migration WHERE Component=N'NetworkAuditV2';")
    }
    return [pscustomobject]@{
        State = $(if ([bool]$row['Latest'] -and [bool]$row['Base043']) { 'Current' } elseif ([bool]$row['Base043']) { 'UpgradeRequired' } else { 'Partial' })
        TableCount = 4
        SourceId = [guid]$row['SourceId']
        Domains = [string]$row['Domains']
        Base043 = [bool]$row['Base043']
        Base044 = [bool]$row['Base044']
        Base045 = [bool]$row['Base045']
        Latest = [bool]$row['Latest']
        Incomplete = [int]$row['Incomplete']
        MigrationCount = $migrationCount
    }
}

function Show-AuditStatus {
    param($Status)
    Write-Log ('حالة شبكة المراقبة: {0}' -f $Status.State) Cyan
    if ($null -ne $Status.SourceId) { Write-Log ('هوية قاعدة البيانات: {0}' -f $Status.SourceId) Cyan }
    if ($Status.Domains) { Write-Log ('النطاقات المفعلة: {0}' -f $Status.Domains) DarkGray }
    Write-Log ('طلبات غير مكتملة: {0} | ترحيلات مسجلة: {1}' -f $Status.Incomplete, $Status.MigrationCount) DarkGray
}

function Assert-ExistingMigrationHistory {
    param([Data.SqlClient.SqlConnection]$Connection, [object[]]$Migrations, [int]$ExpectedCount, [int]$NextMigration)
    if ([int](Get-Scalar $Connection "SELECT CASE WHEN OBJECT_ID(N'dbo.CPOS_Database_Migration',N'U') IS NULL THEN 0 ELSE 1 END;") -ne 1) {
        throw 'سجل ترحيلات شبكة المراقبة غير موجود؛ لا يمكن تنفيذ الترقية التزايدية بأمان.'
    }
    $actualCount = [int](Get-Scalar $Connection "SELECT COUNT(*) FROM dbo.CPOS_Database_Migration WHERE Component=N'NetworkAuditV2';")
    if ($actualCount -ne $ExpectedCount) {
        throw ('عدد الترحيلات السابقة لا يطابق الإصدار المثبت. المتوقع {0} والموجود {1}.' -f $ExpectedCount,$actualCount)
    }
    foreach ($entry in $Migrations) {
        if ([int]([string]$entry.Id) -ge $NextMigration) { continue }
        $storedHash = [string](Get-Scalar $Connection @"
SELECT Sha256 FROM dbo.CPOS_Database_Migration
WHERE Component=N'NetworkAuditV2' AND MigrationId=@MigrationId;
"@ @{ MigrationId=[string]$entry.Id })
        if ([string]::IsNullOrWhiteSpace($storedHash) -or $storedHash -ne [string]$entry.Sha256) {
            throw ('بصمة الترحيل السابق {0} لا تطابق الحزمة المعتمدة؛ أوقفت الترقية التزايدية.' -f $entry.Name)
        }
    }
}

function Assert-NoOtherSessions {
    param([Data.SqlClient.SqlConnection]$Connection)
    $sessions = Get-Table $Connection @"
SELECT session_id,login_name,host_name,program_name
FROM sys.dm_exec_sessions
WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()
ORDER BY session_id;
"@
    if ($sessions.Rows.Count -gt 0) {
        foreach ($row in $sessions.Rows) {
            Write-Log ('اتصال مفتوح: Session={0}, Login={1}, Host={2}, Program={3}' -f $row.session_id,$row.login_name,$row.host_name,$row.program_name) Yellow
        }
        throw 'أغلق CPOS وSSMS وأي اتصال آخر بقاعدة البيانات، ثم أعد تشغيل الأداة. الأداة لن تغلق الاتصالات بالقوة.'
    }
}

function Backup-Database {
    param([Data.SqlClient.SqlConnection]$MasterConnection)
    $dataFile = [string](Get-Scalar $MasterConnection @"
SELECT TOP (1) physical_name FROM master.sys.master_files
WHERE database_id=DB_ID(@DatabaseName) AND file_id=1;
"@ @{ DatabaseName=$DatabaseName })
    if ([string]::IsNullOrWhiteSpace($dataFile)) { throw 'تعذر تحديد مسار ملفات قاعدة البيانات لإنشاء النسخة الاحتياطية.' }
    $directory = $BackupDirectory
    if ([string]::IsNullOrWhiteSpace($directory)) { $directory = [IO.Path]::GetDirectoryName($dataFile) }
    $safeName = [Text.RegularExpressions.Regex]::Replace($DatabaseName, '[^A-Za-z0-9_\-]', '_')
    $backupPath = [IO.Path]::Combine($directory, ('{0}_Before_NetworkAudit_{1}.bak' -f $safeName,(Get-Date -Format 'yyyyMMdd_HHmmss')))
    $quotedDatabase = '[' + $DatabaseName.Replace(']', ']]') + ']'
    $sqlPath = $backupPath.Replace("'", "''")
    $command = New-SqlCommand $MasterConnection ("BACKUP DATABASE {0} TO DISK=N'{1}' WITH COPY_ONLY, INIT, CHECKSUM, NAME=N'CPOS before Network Audit V2';" -f $quotedDatabase,$sqlPath) $null 0
    try { [void]$command.ExecuteNonQuery() } finally { $command.Dispose() }
    $command = New-SqlCommand $MasterConnection ("RESTORE VERIFYONLY FROM DISK=N'{0}' WITH CHECKSUM;" -f $sqlPath) $null 0
    try { [void]$command.ExecuteNonQuery() } finally { $command.Dispose() }
    return $backupPath
}

function Prepare-MigrationSql {
    param($Entry, [guid]$SourceId)
    $databaseLiteral = $DatabaseName.Replace("'", "''")
    $sql = [string]$Entry.Sql
    $sql = $sql.Replace('CPOS_ACCOUNTING', $databaseLiteral)
    if ($SourceId -ne [guid]::Empty) {
        $sql = $sql.Replace($DevelopmentSourceId, $SourceId.ToString())
    }
    if ([string]$Entry.Id -eq '007') {
        $enableNeedle = 'DECLARE @Enable bit = NULL;'
        $sourceNeedle = 'DECLARE @ExpectedSource uniqueidentifier = NULL;'
        if (($sql.Split(@($enableNeedle),[StringSplitOptions]::None).Count - 1) -ne 1 -or
            ($sql.Split(@($sourceNeedle),[StringSplitOptions]::None).Count - 1) -ne 1) {
            throw 'لم يمكن تهيئة بوابة تشغيل المراقبة 007 بأمان.'
        }
        $sql = $sql.Replace($enableNeedle, 'DECLARE @Enable bit = 1;')
        $sql = $sql.Replace($sourceNeedle, ("DECLARE @ExpectedSource uniqueidentifier = '{0}';" -f $SourceId))
    }
    if ($sql -match '(?im)^\s*GO\s*$') { throw ('ملف {0} يحتوي فاصل GO غير مدعوم.' -f $Entry.Name) }
    if ($SourceId -ne [guid]::Empty -and $sql.Contains($DevelopmentSourceId)) {
        throw ('بقيت هوية التطوير داخل الترحيل {0}.' -f $Entry.Name)
    }
    return $sql
}

Write-Log ('CPOS Network Audit Upgrade Tool {0}' -f $ToolVersion) White
$migrations = Get-EmbeddedMigrations
Write-Log ('تم التحقق من سلامة {0} ترحيلًا داخل الحزمة.' -f $migrations.Count) Green
if ($Action -eq 'PackageCheck') {
    $savedDatabaseName = $DatabaseName
    $DatabaseName = 'CPOS_CUSTOMER_PACKAGE_CHECK'
    $testSource = [guid]'11111111-2222-4333-8444-555555555555'
    foreach ($entry in $migrations) {
        $sourceForEntry = if ([int]([string]$entry.Id) -ge 7) { $testSource } else { [guid]::Empty }
        $prepared = Prepare-MigrationSql $entry $sourceForEntry
        if ($prepared.Contains('CPOS_ACCOUNTING') -or $prepared.Contains($DevelopmentSourceId)) {
            throw ('فشل اختبار تخصيص الترحيل {0} لقاعدة الزبون.' -f $entry.Name)
        }
    }
    $DatabaseName = $savedDatabaseName
    Write-Log 'فحص الحزمة والتحويل لقاعدة الزبون اكتمل بنجاح، ولم يتم الاتصال بأي قاعدة بيانات.' Green
    exit 0
}

if ([string]::IsNullOrWhiteSpace($ServerInstance)) { $ServerInstance = Read-DefaultValue 'اسم SQL Server' '.\SYS' }
if (-not $PSBoundParameters.ContainsKey('DatabaseName') -or [string]::IsNullOrWhiteSpace($DatabaseName)) {
    $DatabaseName = Read-DefaultValue 'اسم قاعدة البيانات' 'CPOS_ACCOUNTING'
}
if ($null -ne $SqlCredential) {
    $Authentication = 'Sql'
    $SqlUser = $SqlCredential.UserName
}
elseif ([string]::IsNullOrWhiteSpace($Authentication)) {
    $choice = (Read-DefaultValue 'نوع الدخول: S لحساب SQL أو W لويندوز' 'S').ToUpperInvariant()
    $Authentication = if ($choice -eq 'W') { 'Windows' } else { 'Sql' }
}

$securePassword = $null
if ($Authentication -eq 'Sql') {
    if ($null -ne $SqlCredential) {
        $securePassword = $SqlCredential.Password
    }
    else {
        $SqlUser = Read-DefaultValue 'اسم مستخدم SQL' $SqlUser
        $securePassword = Read-Host 'كلمة مرور SQL' -AsSecureString
    }
}

$masterConnection = $null
$targetConnection = $null
try {
    $masterConnection = New-Object System.Data.SqlClient.SqlConnection((New-ConnectionString 'master' $securePassword))
    $masterConnection.Open()
    $major = [int](Get-Scalar $masterConnection "SELECT CONVERT(int,PARSENAME(CONVERT(varchar(128),SERVERPROPERTY('ProductVersion')),4));")
    if ($major -lt 12) { throw 'يلزم SQL Server 2014 أو أحدث.' }
    if ([int](Get-Scalar $masterConnection "SELECT ISNULL(IS_SRVROLEMEMBER('sysadmin'),0);") -ne 1) {
        throw 'يلزم الدخول بحساب SQL Server من نوع sysadmin لتنفيذ النسخة الاحتياطية والترقية.'
    }
    if ([int](Get-Scalar $masterConnection 'SELECT COUNT(*) FROM sys.databases WHERE name=@DatabaseName;' @{ DatabaseName=$DatabaseName }) -ne 1) {
        throw ('قاعدة البيانات {0} غير موجودة على الخادم المحدد.' -f $DatabaseName)
    }
    $dbState = [string](Get-Scalar $masterConnection "SELECT state_desc+'|'+user_access_desc+'|'+CONVERT(varchar(1),is_read_only) FROM sys.databases WHERE name=@DatabaseName;" @{ DatabaseName=$DatabaseName })
    if (-not $dbState.StartsWith('ONLINE|') -or $dbState.EndsWith('|1')) {
        throw ('قاعدة البيانات ليست ONLINE وقابلة للكتابة. الحالة: {0}' -f $dbState)
    }

    $targetConnection = New-Object System.Data.SqlClient.SqlConnection((New-ConnectionString $DatabaseName $securePassword))
    $targetConnection.Open()
    if ([int](Get-Scalar $targetConnection "SELECT ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0);") -ne 1) {
        throw 'الصلاحية VIEW SERVER STATE مطلوبة للتحقق من إغلاق الجلسات.'
    }
    $status = Get-AuditStatus $targetConnection
    Show-AuditStatus $status

    if ($Action -eq 'Validate') {
        if ($status.State -ne 'Current' -or $status.Incomplete -ne 0) { throw 'شبكة المراقبة ليست على الإصدار النهائي السليم.' }
        Write-Log 'التحقق اكتمل: قاعدة البيانات على الإصدار النهائي 046.' Green
        exit 0
    }
    if ($status.State -eq 'Current') {
        Write-Log 'شبكة المراقبة مثبتة بالفعل على الإصدار النهائي؛ لم يتم إجراء أي تعديل.' Green
        exit 0
    }
    Assert-NoOtherSessions $targetConnection
    if ($status.State -notin @('NotInstalled','UpgradeRequired')) {
        throw 'وُجد تثبيت جزئي أو أقدم لشبكة المراقبة. أوقفت الأداة العملية لأن ترحيلات التثبيت الأول غير قابلة لإعادة التطبيق قسرًا.'
    }
    $migrationsToApply = @($migrations)
    if ($status.State -eq 'NotInstalled') {
        if ([int](Get-Scalar $targetConnection "SELECT COUNT(*) FROM sys.tables WHERE object_id=OBJECT_ID(N'dbo.Network_Edit_Tracker',N'U');") -ne 1) {
            throw 'جدول المراقبة القديم Network_Edit_Tracker غير موجود؛ هذه ليست قاعدة CPOS الأساسية المتوقعة.'
        }
        if ([int](Get-Scalar $targetConnection "SELECT CASE WHEN OBJECT_ID(N'dbo.CPOS_Database_Migration',N'U') IS NULL THEN 0 ELSE 1 END;") -eq 1) {
            if ([int](Get-Scalar $targetConnection "SELECT COUNT(*) FROM dbo.CPOS_Database_Migration WHERE Component=N'NetworkAuditV2';") -ne 0) {
                throw 'يوجد سجل ترقية سابق دون جداول المراقبة الجديدة. يلزم فحص القاعدة يدويًا.'
            }
        }
    }
    else {
        $nextMigration = if ($status.Base045) { 46 } elseif ($status.Base044) { 45 } else { 44 }
        $previousCount = if ($status.Base045) { 43 } elseif ($status.Base044) { 42 } else { 41 }
        Assert-ExistingMigrationHistory $targetConnection $migrations $previousCount $nextMigration
        $migrationsToApply = @($migrations | Where-Object { [int]([string]$_.Id) -ge $nextMigration })
        if ($migrationsToApply.Count -ne ($ExpectedMigrationCount - $previousCount)) { throw 'عدد ترحيلات الإصلاح المتبقية غير صحيح.' }
    }

    Write-Host ''
    Write-Log ('الخادم: {0}' -f $ServerInstance) White
    Write-Log ('القاعدة: {0}' -f $DatabaseName) White
    if ($status.State -eq 'UpgradeRequired') {
        Write-Log 'تم اكتشاف إصدار 043 أو 044 أو 045. ستؤخذ نسخة احتياطية ثم تطبق الإصلاحات المتبقية حتى 046 دون إعادة إنشاء الجداول أو حذف السجلات.' Yellow
    }
    else {
        Write-Log 'سيتم أخذ نسخة احتياطية ثم تطبيق الترحيلات 002..046 في معاملة واحدة.' Yellow
    }
    Write-Log 'يجب تثبيت نسخة CPOS المطابقة بعد نجاح الأداة، وقبل إعادة فتح النظام للمستخدمين.' Yellow
    $confirmation = Read-Host 'اكتب UPGRADE للبدء'
    if ($confirmation -cne 'UPGRADE') { throw 'تم إلغاء العملية؛ لم يجر أي تعديل.' }

    Write-Log 'جاري إنشاء النسخة الاحتياطية والتحقق منها...' Cyan
    $backupPath = Backup-Database $masterConnection
    Write-Log ('اكتملت النسخة الاحتياطية: {0}' -f $backupPath) Green
    Assert-NoOtherSessions $targetConnection

    $transaction = $targetConnection.BeginTransaction([Data.IsolationLevel]::Serializable)
    $currentMigration = $null
    try {
        $historySql = @"
IF OBJECT_ID(N'dbo.CPOS_Database_Migration',N'U') IS NULL
BEGIN
 CREATE TABLE dbo.CPOS_Database_Migration(
  MigrationId varchar(10) NOT NULL,
  Component varchar(40) NOT NULL,
  MigrationName nvarchar(260) NOT NULL,
  Sha256 char(64) NOT NULL,
  ToolVersion varchar(30) NOT NULL,
  SourceId uniqueidentifier NULL,
  AppliedAtUtc datetime2(3) NOT NULL CONSTRAINT DF_CPOS_Database_Migration_Applied DEFAULT SYSUTCDATETIME(),
  AppliedBy sysname NOT NULL,
  HostName sysname NOT NULL,
  CONSTRAINT PK_CPOS_Database_Migration PRIMARY KEY(Component,MigrationId)
 );
END;
"@
        $command = New-SqlCommand $targetConnection $historySql $transaction 60
        try { [void]$command.ExecuteNonQuery() } finally { $command.Dispose() }

        $sourceId = $(if ($status.State -eq 'UpgradeRequired') { [guid]$status.SourceId } else { [guid]::Empty })
        foreach ($entry in $migrationsToApply) {
            $currentMigration = [string]$entry.Name
            Write-Log ('تطبيق {0}...' -f $currentMigration) Cyan
            $sql = Prepare-MigrationSql $entry $sourceId
            $command = New-SqlCommand $targetConnection $sql $transaction 600
            try { [void]$command.ExecuteNonQuery() } finally { $command.Dispose() }

            if ([string]$entry.Id -eq '002') {
                $sourceId = [guid](Get-Scalar $targetConnection 'SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1;' @{} $transaction)
                if ($sourceId -eq [guid]::Empty) { throw 'لم تُنشأ هوية مستقلة لقاعدة الزبون.' }
                Write-Log ('تم إنشاء هوية القاعدة: {0}' -f $sourceId) DarkCyan
            }

            $insert = New-SqlCommand $targetConnection @"
INSERT dbo.CPOS_Database_Migration(MigrationId,Component,MigrationName,Sha256,ToolVersion,SourceId,AppliedBy,HostName)
VALUES(@Id,N'NetworkAuditV2',@Name,@Hash,@ToolVersion,@SourceId,SUSER_SNAME(),HOST_NAME());
"@ $transaction 60
            try {
                [void](Add-Parameter $insert '@Id' ([string]$entry.Id))
                [void](Add-Parameter $insert '@Name' ([string]$entry.Name))
                [void](Add-Parameter $insert '@Hash' ([string]$entry.Sha256))
                [void](Add-Parameter $insert '@ToolVersion' $ToolVersion)
                [void](Add-Parameter $insert '@SourceId' $sourceId)
                [void]$insert.ExecuteNonQuery()
            }
            finally { $insert.Dispose() }
        }
        $transaction.Commit()
        Write-Log 'تم اعتماد جميع الترحيلات في قاعدة البيانات.' Green
    }
    catch {
        try { $transaction.Rollback() } catch { }
        throw ('فشلت المرحلة {0}. تم التراجع عن جميع تغييرات الترقية. السبب: {1}' -f $currentMigration,$_.Exception.Message)
    }
    finally { $transaction.Dispose() }

    $finalStatus = Get-AuditStatus $targetConnection
    Show-AuditStatus $finalStatus
    if ($finalStatus.State -ne 'Current' -or $finalStatus.Incomplete -ne 0 -or $finalStatus.MigrationCount -ne $ExpectedMigrationCount) {
        throw 'اكتمل التنفيذ لكن التحقق النهائي لم يطابق العقد المتوقع. لا تفتح CPOS قبل مراجعة السجل.'
    }
    Write-Log 'نجحت ترقية شبكة المراقبة إلى الإصدار 046. يمكنك الآن فتح CPOS واختبار العمليات.' Green
    Write-Log ('احتفظ بالنسخة الاحتياطية وبملف السجل: {0}' -f $LogPath) Green
}
catch {
    Write-Log ('فشل: {0}' -f $_.Exception.Message) Red
    if ($_.ScriptStackTrace) { Write-Log ('الموضع التقني: {0}' -f $_.ScriptStackTrace) DarkGray }
    Write-Log ('راجع ملف السجل: {0}' -f $LogPath) Yellow
    exit 1
}
finally {
    if ($null -ne $targetConnection) { $targetConnection.Dispose() }
    if ($null -ne $masterConnection) { $masterConnection.Dispose() }
    $securePassword = $null
}
