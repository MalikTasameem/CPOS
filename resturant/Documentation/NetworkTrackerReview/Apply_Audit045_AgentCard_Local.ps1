param([Parameter(Mandatory=$true)][string]$SettingsPath,
      [Parameter(Mandatory=$true)][string]$OutputDirectory,
      [switch]$Apply)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$settings=@{}
foreach ($line in [IO.File]::ReadAllLines((Resolve-Path -LiteralPath $SettingsPath))) {
    $split=$line.IndexOf(':')
    if ($split -gt 0) { $settings[$line.Substring(0,$split)]=$line.Substring($split+1) }
}
$builder=New-Object Data.SqlClient.SqlConnectionStringBuilder
$builder['Data Source']=$settings['S_SERVER']; $builder['Initial Catalog']=$settings['DataBase']
$builder['User ID']=$settings['DB_UName']; $builder['Password']=$settings['DB_Pass']
$builder['Pooling']=$false; $builder['Connect Timeout']=8
$builder['Application Name']='CPOS Audit045 AgentCard Local Deployment'
if ($builder['Initial Catalog'] -ne 'CPOS_ACCOUNTING' -or $builder['Data Source'] -ne 'CLASS-PC\SYS') { throw 'Only the approved local server/database is permitted.' }
$cn=New-Object Data.SqlClient.SqlConnection($builder.ConnectionString)
function Scalar([string]$Sql) {
    $command=$cn.CreateCommand(); $command.CommandTimeout=120; $command.CommandText=$Sql
    try { return $command.ExecuteScalar() } finally { $command.Dispose() }
}
function Assert-Idle {
    if ([int](Scalar 'SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()') -ne 0) {
        throw 'Other database clients are active. No session will be terminated automatically.'
    }
}
try {
    $cn.Open()
    if ([int](Scalar "SELECT ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)") -ne 1) { throw 'Cannot verify sessions.' }
    Assert-Idle
    if ([int](Scalar "SELECT COUNT(*) FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SchemaVersion=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c'") -ne 1) { throw 'Local database identity differs.' }
    if ([int](Scalar 'SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1') -ne 0) { throw 'Incomplete audit requests exist.' }
    $before=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")
    if (-not $before.Contains('-- Audit044: safe datetime validation across audit domains.')) { throw 'Audit044 is required.' }
    Write-Output 'PASS local identity, baseline044 and no other database clients'
    if (-not $Apply) { Write-Output 'Read-only preflight complete; use -Apply only with authorization.'; return }
    if (Test-Path -LiteralPath $OutputDirectory) { throw 'Use a new evidence directory.' }
    [void](New-Item -ItemType Directory -Path $OutputDirectory)
    [IO.File]::WriteAllText((Join-Path $OutputDirectory 'Network_Audit_Event_Write.before.sql'),$before,(New-Object Text.UTF8Encoding($true)))
    $dataPath=[string](Scalar 'SELECT TOP(1) physical_name FROM sys.database_files WHERE type=0 ORDER BY file_id')
    $directory=[IO.Path]::GetDirectoryName($dataPath)
    $backup=Join-Path $directory ('CPOS_ACCOUNTING_Before_Audit045_AgentCard_'+(Get-Date -Format 'yyyyMMdd_HHmmss')+'_'+[guid]::NewGuid().ToString('N').Substring(0,8)+'.bak')
    $cmd=$cn.CreateCommand(); $cmd.CommandTimeout=600
    $cmd.CommandText='BACKUP DATABASE [CPOS_ACCOUNTING] TO DISK=@Path WITH COPY_ONLY,CHECKSUM; RESTORE VERIFYONLY FROM DISK=@Path WITH CHECKSUM;'
    [void]$cmd.Parameters.Add('@Path',[Data.SqlDbType]::NVarChar,4000); $cmd.Parameters['@Path'].Value=$backup
    try { [void]$cmd.ExecuteNonQuery() } finally { $cmd.Dispose() }
    Write-Output ('BACKUP_VERIFIED='+$backup)
    Assert-Idle
    $tx=$cn.BeginTransaction([Data.IsolationLevel]::Serializable)
    try {
        foreach ($file in @('Database\Audit\045_Audit_Safe_Document_Type.sql','Database\Security\002_User_Validation_Agent_Card.sql')) {
            $cmd=$cn.CreateCommand(); $cmd.Transaction=$tx; $cmd.CommandTimeout=120
            $cmd.CommandText=[IO.File]::ReadAllText((Join-Path $projectRoot $file))
            try { [void]$cmd.ExecuteNonQuery() } finally { $cmd.Dispose() }
            Write-Output ('APPLIED_IN_TRANSACTION='+$file)
        }
        $tx.Commit()
    } catch { if ($tx.Connection) { $tx.Rollback() }; throw } finally { $tx.Dispose() }
    $after=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")
    if (-not $after.Contains('-- Audit045: safe document type validation.')) { throw 'Final marker missing.' }
    if ([int](Scalar "SELECT COUNT(*) FROM sys.columns WHERE object_id=OBJECT_ID('dbo.Users_Validations') AND name='Open_Agent_Card' AND system_type_id=104 AND is_nullable=0") -ne 1) { throw 'Final permission column missing.' }
    [IO.File]::WriteAllText((Join-Path $OutputDirectory 'Network_Audit_Event_Write.after.sql'),$after,(New-Object Text.UTF8Encoding($true)))
    [pscustomobject]@{Database='CPOS_ACCOUNTING';Server='CLASS-PC\SYS';Backup=$backup;AppliedUtc=[DateTime]::UtcNow.ToString('o');Audit045=$true;AgentCardPermission=$true} |
        ConvertTo-Json | Set-Content -LiteralPath (Join-Path $OutputDirectory 'Applied.json') -Encoding UTF8
    Write-Output 'SUCCESS original database: Audit045 and Agent card permission applied; no business rows changed.'
} finally { $cn.Dispose() }
