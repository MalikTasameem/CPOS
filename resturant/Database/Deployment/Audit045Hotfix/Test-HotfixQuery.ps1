param([Parameter(Mandatory=$true)][string]$QueryPath,
      [Parameter(Mandatory=$true)][string]$ConnectionEnvironmentVariable,
      [Parameter(Mandatory=$true)][string]$ReportPath)
$ErrorActionPreference='Stop'
if ($PSVersionTable.PSEdition -ne 'Desktop') { throw 'Use Windows PowerShell 5.1 for SQL2014 parser.' }
$builder=New-Object Data.SqlClient.SqlConnectionStringBuilder([Environment]::GetEnvironmentVariable($ConnectionEnvironmentVariable))
$name=$builder['Initial Catalog']
if ($name -notmatch '^CPOS_AUDIT_GATE_[a-f0-9]{32}$') { throw 'Production databases refused.' }
$builder['Pooling']=$false
$cn=New-Object Data.SqlClient.SqlConnection($builder.ConnectionString)
$checks=New-Object 'Collections.Generic.List[string]'
$backups=New-Object 'Collections.Generic.List[string]'
function Check([bool]$Condition,[string]$Label) {
 if (-not $Condition) { throw ('FAILED '+$Label) }
 $checks.Add($Label); Write-Output ('PASS '+$Label)
}
function Scalar([string]$Sql) {
 $cmd=$cn.CreateCommand(); $cmd.CommandTimeout=120; $cmd.CommandText=$Sql
 try { return $cmd.ExecuteScalar() } finally { $cmd.Dispose() }
}
function Reject([string]$Sql,[string]$Expected,[Data.SqlClient.SqlTransaction]$Transaction=$null) {
 $cmd=$cn.CreateCommand(); $cmd.CommandTimeout=120; $cmd.CommandText=$Sql; $cmd.Transaction=$Transaction
 try {
  try { [void]$cmd.ExecuteNonQuery() }
  catch [Data.SqlClient.SqlException] {
   if ($_.Exception.Message.Contains($Expected)) { return }
   throw
  }
  throw ('Expected rejection: '+$Expected)
 } finally { $cmd.Dispose() }
}
function Execute-Upgrade([string]$Sql) {
 $cmd=$cn.CreateCommand(); $cmd.CommandTimeout=300; $cmd.CommandText=$Sql
 $set=New-Object Data.DataSet; $adapter=New-Object Data.SqlClient.SqlDataAdapter($cmd)
 try {
  [void]$adapter.Fill($set)
  $success=@($set.Tables | Where-Object {$_.Columns.Contains('UpgradeStatus')})
  if ($success.Count -ne 1 -or $success[0].Rows.Count -ne 1 -or $success[0].Rows[0]['UpgradeStatus'] -ne 'UPGRADE SUCCESS') { throw 'No final success result.' }
  $backups.Add([string]$success[0].Rows[0]['VerifiedBackup'])
 } finally { $adapter.Dispose(); $cmd.Dispose(); $set.Dispose() }
}
try {
 $cn.Open()
 Check ([int](Scalar "SELECT COUNT(*) FROM sys.extended_properties WHERE class=0 AND name=N'CPOS.AuditReleaseSandbox' AND CONVERT(nvarchar(100),value)=DB_NAME()") -eq 1) 'owned isolated database'
 $original=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")
 Check ($original.Contains('-- Audit044:') -and -not $original.Contains('-- Audit045:')) 'starts from actual 044 writer'
 Check ((Scalar "SELECT COL_LENGTH('dbo.Users_Validations','Open_Agent_Card')") -is [DBNull]) 'starts without card permission'
 $source=[guid](Scalar 'SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1')
 $raw=[IO.File]::ReadAllText((Resolve-Path -LiteralPath $QueryPath))
 Add-Type -Path 'C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
 $parser=New-Object Microsoft.SqlServer.TransactSql.ScriptDom.TSql120Parser($true)
 $errors=$null
 $reader=New-Object IO.StringReader($raw)
 try { [void]$parser.Parse($reader,[ref]$errors) } finally { $reader.Dispose() }
 Check ($errors.Count -eq 0) ('SQL2014 parse: '+(($errors | ForEach-Object Message) -join '; '))
 $guard="DECLARE @ExpectedDatabase sysname = N'CPOS_ACCOUNTING';"
 Check ([regex]::Matches($raw,[regex]::Escape($guard)).Count -eq 1) 'one customer database setting only'
 $sql=$raw.Replace($guard,"DECLARE @ExpectedDatabase sysname = N'$name';")
 Reject $raw 'Select the customer database'
 Check $true 'wrong database refused'
 $tx=$cn.BeginTransaction()
 try {
  Reject $sql 'no open transaction' $tx
  $cmd=$cn.CreateCommand(); $cmd.Transaction=$tx; $cmd.CommandText='SELECT @@TRANCOUNT'
  try { Check ([int]$cmd.ExecuteScalar() -eq 1) 'caller transaction is not rolled back' } finally {$cmd.Dispose()}
 } finally { $tx.Rollback(); $tx.Dispose() }
 $other=New-Object Data.SqlClient.SqlConnection($builder.ConnectionString)
 try {
  $other.Open(); Reject $sql 'Close other CPOS/SSMS'
  $ping=$other.CreateCommand(); $ping.CommandText='SELECT 1'
  try { Check ([int]$ping.ExecuteScalar() -eq 1) 'busy database refused without terminating other session' } finally {$ping.Dispose()}
 } finally { $other.Dispose() }
 # Missing prerequisite, on the owned copy only, with exact restoration afterwards.
 try {
  [void](Scalar $original.Replace('CREATE PROCEDURE','ALTER PROCEDURE').Replace('-- Audit044:','-- TestMissing044:'))
  Reject $sql 'requires audit version 044'
  Check $true 'unrecognized baseline refused'
 } finally { [void](Scalar $original.Replace('CREATE PROCEDURE','ALTER PROCEDURE')) }
 $badBackup=$sql.Replace('DECLARE @BackupDirectory nvarchar(2000) = NULL;',"DECLARE @BackupDirectory nvarchar(2000) = N'C:\CPOS_HOTFIX_NONEXISTENT_$([guid]::NewGuid().ToString('N'))';")
 Reject $badBackup 'backup device'
 Check ([string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))") -ceq $original) 'backup failure leaves writer unchanged'
 Check ((Scalar "SELECT COL_LENGTH('dbo.Users_Validations','Open_Agent_Card')") -is [DBNull]) 'backup failure leaves permission absent'

 # Recreate the customer upgrade history shape if this development backup lacks it.
 if ([int](Scalar "SELECT CASE WHEN OBJECT_ID('dbo.CPOS_Database_Migration','U') IS NULL THEN 0 ELSE 1 END") -eq 0) {
  [void](Scalar @'
CREATE TABLE dbo.CPOS_Database_Migration(
 MigrationId varchar(10) NOT NULL,Component nvarchar(100) NOT NULL,MigrationName nvarchar(260) NOT NULL,
 Sha256 varchar(64) NOT NULL,ToolVersion varchar(30) NOT NULL,SourceId uniqueidentifier NULL,
 AppliedAtUtc datetime2(3) NOT NULL DEFAULT SYSUTCDATETIME(),AppliedBy sysname NOT NULL,HostName sysname NOT NULL,
 PRIMARY KEY(Component,MigrationId));
'@)
 }
 $basePath=(Resolve-Path (Join-Path $PSScriptRoot '..\..\Audit\044_Audit_Safe_Datetime_Validation.sql')).Path
 $baseHash=(Get-FileHash -LiteralPath $basePath -Algorithm SHA256).Hash
 [void](Scalar "IF NOT EXISTS(SELECT 1 FROM dbo.CPOS_Database_Migration WHERE Component=N'NetworkAuditV2' AND MigrationId='044') INSERT dbo.CPOS_Database_Migration(MigrationId,Component,MigrationName,Sha256,ToolVersion,SourceId,AppliedBy,HostName) VALUES('044',N'NetworkAuditV2',N'044_Audit_Safe_Datetime_Validation.sql','$baseHash','Test044','$source',SUSER_SNAME(),HOST_NAME());")
 $baseline=@{}
 foreach ($table in @('Agents','Users','Agents_Balance_MV','SB_Contents','Pch_Details','Rtn_Contents','ST_Balance','Network_Audit_Event','Network_Audit_Request','Network_Audit_Change')) {
  $baseline[$table]=[string](Scalar "SELECT CONCAT(COUNT_BIG(*),':',CHECKSUM_AGG(BINARY_CHECKSUM(*))) FROM dbo.[$table]")
 }
 $hook='-- Independent Agent card permission: keep existing restrictions on rerun.'
 Check ([regex]::Matches($sql,[regex]::Escape($hook)).Count -eq 1) 'test-only failure injection anchor'
 Reject ($sql.Replace($hook,"THROW 51999,'test forced failure after audit stage',1;`r`n"+$hook)) 'test forced failure after audit stage'
 Check ([string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))") -ceq $original) 'second-stage failure rolls back writer to 044'
 Check ((Scalar "SELECT COL_LENGTH('dbo.Users_Validations','Open_Agent_Card')") -is [DBNull]) 'second-stage failure leaves no partial permission'
 Check ([int](Scalar "SELECT COUNT(*) FROM dbo.CPOS_Database_Migration WHERE Component=N'NetworkAuditV2' AND MigrationId='045'") -eq 0) 'failed upgrade has no success history'
 Execute-Upgrade $sql
 $patched=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")
 Check ($patched.Contains('-- Audit045: safe document type validation.')) 'successful 044 to 045 upgrade'
 Check ([int](Scalar 'SELECT COUNT(*) FROM dbo.Users_Validations WHERE Open_Agent_Card<>1 OR Open_Agent_Card IS NULL') -eq 0) 'existing roles default to allowed'
 [void](Scalar 'UPDATE dbo.Users_Validations SET Open_Agent_Card=0 WHERE T_ID=(SELECT MIN(T_ID) FROM dbo.Users_Validations)')
 Execute-Upgrade $sql
 Check ([string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))") -ceq $patched) 'repeat does not rewrite writer'
 Check ([int](Scalar 'SELECT COUNT(*) FROM dbo.Users_Validations WHERE Open_Agent_Card=0') -eq 1) 'repeat preserves denied card access'
 Check ([int](Scalar "SELECT COUNT(*) FROM dbo.CPOS_Database_Migration WHERE (Component=N'NetworkAuditV2' AND MigrationId='045') OR (Component=N'Security.AgentCard' AND MigrationId='002')") -eq 2) 'incremental history recorded exactly once'
 Check ([guid](Scalar 'SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1') -eq $source) 'source identity preserved'
 foreach ($table in $baseline.Keys) {
  Check ([string](Scalar "SELECT CONCAT(COUNT_BIG(*),':',CHECKSUM_AGG(BINARY_CHECKSUM(*))) FROM dbo.[$table]") -ceq $baseline[$table]) ('data unchanged '+$table)
 }
 Check ([int](Scalar 'SELECT @@TRANCOUNT') -eq 0) 'no open transaction left'
 [pscustomobject]@{Passed=$true;QuerySha256=(Get-FileHash -LiteralPath $QueryPath -Algorithm SHA256).Hash;Database=$name;Checks=$checks.ToArray();Backups=$backups.ToArray();CompletedUtc=[DateTime]::UtcNow.ToString('o')} |
  ConvertTo-Json -Depth 5 | Set-Content -LiteralPath $ReportPath -Encoding UTF8
 Write-Output ('PASSED_CHECKS='+$checks.Count)
} finally { $cn.Dispose() }
