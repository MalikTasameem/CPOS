param(
    [Parameter(Mandatory=$true)][string]$ServerConnectionEnvironmentVariable,
    [Parameter(Mandatory=$true)][string]$BackupPath
)
$ErrorActionPreference = 'Stop'
# Only create a new uniquely named database. Never REPLACE, DROP or disconnect users.
$connectionString = [Environment]::GetEnvironmentVariable($ServerConnectionEnvironmentVariable)
if (-not $connectionString) { throw 'Server connection environment variable is empty.' }
$builder = New-Object Data.SqlClient.SqlConnectionStringBuilder($connectionString)
$builder['Initial Catalog'] = 'master'
$builder['Pooling'] = $false
$builder['Application Name'] = 'CPOS Audit Gate - New Sandbox'
$name = 'CPOS_AUDIT_GATE_' + [Guid]::NewGuid().ToString('N')
$quotedBackup = "N'" + $BackupPath.Replace("'","''") + "'"
$cn = New-Object Data.SqlClient.SqlConnection($builder.ConnectionString)
try {
    $cn.Open()
    $cmd = $cn.CreateCommand()
    $cmd.CommandTimeout = 300
    $cmd.CommandText = "SELECT LEFT(physical_name,LEN(physical_name)-CHARINDEX('\',REVERSE(physical_name))+1) FROM sys.master_files WHERE database_id=1 AND file_id=1"
    $dataPath = [string]$cmd.ExecuteScalar()
    if (-not $dataPath) { throw 'SQL data directory unavailable.' }
    $cmd.CommandText = "RESTORE VERIFYONLY FROM DISK=$quotedBackup"
    [void]$cmd.ExecuteNonQuery()
    $cmd.CommandText = "RESTORE FILELISTONLY FROM DISK=$quotedBackup"
    $files = New-Object Data.DataTable
    $reader = $cmd.ExecuteReader()
    try { $files.Load($reader) } finally { $reader.Dispose() }
    $moves = @()
    foreach ($row in $files.Rows) {
        if ($row.Type -notin 'D','L') { throw 'Only ordinary data/log backups are supported by this sandbox helper.' }
        $extension = if ($row.Type -eq 'L') { '.ldf' } else { '.mdf' }
        $physical = $dataPath + $name + '_' + [int]$row.FileId + $extension
        $moves += "MOVE N'" + ([string]$row.LogicalName).Replace("'","''") + "' TO N'" + $physical.Replace("'","''") + "'"
    }
    if ($moves.Count -lt 2) { throw 'Backup data/log files are missing.' }
    $cmd.CommandText = "IF DB_ID(N'$name') IS NOT NULL THROW 51000,'Sandbox already exists',1; RESTORE DATABASE [$name] FROM DISK=$quotedBackup WITH " + ($moves -join ',') + ',RECOVERY;'
    [void]$cmd.ExecuteNonQuery()
    $cmd.Dispose()
    $cn.ChangeDatabase($name)
    $cmd = $cn.CreateCommand()
    $cmd.CommandText = "EXEC sys.sp_addextendedproperty @name=N'CPOS.AuditReleaseSandbox',@value=N'$name';"
    [void]$cmd.ExecuteNonQuery()
    $cmd.Dispose()
    # Copying SourceId here is intentional: this is isolated test data, never a deployable branch.
    Write-Output ('SANDBOX=' + $name)
    Write-Output 'No production database was changed. Apply/test candidate migrations on this sandbox only; do not open it in CPOS.'
} finally { $cn.Dispose() }
