param([Parameter(Mandatory=$true)][string]$QueryPath,[Parameter(Mandatory=$true)][string]$ConnectionEnvironmentVariable,[Parameter(Mandatory=$true)][string]$ReportPath)
$ErrorActionPreference='Stop'
$b=New-Object Data.SqlClient.SqlConnectionStringBuilder([Environment]::GetEnvironmentVariable($ConnectionEnvironmentVariable))
$name=$b['Initial Catalog'];$b['Pooling']=$false
if($name -notmatch '^CPOS_AUDIT_GATE_[a-f0-9]{32}$'){throw 'Production refused.'}
$cn=New-Object Data.SqlClient.SqlConnection($b.ConnectionString)
$checks=New-Object 'Collections.Generic.List[string]';$backups=New-Object 'Collections.Generic.List[string]'
function Check([bool]$ok,[string]$label){if(-not $ok){throw ('FAIL '+$label)};$checks.Add($label);Write-Output ('PASS '+$label)}
function Scalar([string]$sql){$cmd=$cn.CreateCommand();$cmd.CommandTimeout=300;$cmd.CommandText=$sql;try{return $cmd.ExecuteScalar()}finally{$cmd.Dispose()}}
function Reject([string]$sql,[string]$message,[Data.SqlClient.SqlTransaction]$tx=$null){
 $cmd=$cn.CreateCommand();$cmd.CommandTimeout=300;$cmd.CommandText=$sql;$cmd.Transaction=$tx
 try{try{[void]$cmd.ExecuteNonQuery()}catch [Data.SqlClient.SqlException]{if($_.Exception.Message.Contains($message)){return};throw};throw ('Expected rejection: '+$message)}finally{$cmd.Dispose()}
}
function Upgrade([string]$sql){
 $cmd=$cn.CreateCommand();$cmd.CommandTimeout=300;$cmd.CommandText=$sql
 $ds=New-Object Data.DataSet;$da=New-Object Data.SqlClient.SqlDataAdapter($cmd)
 try{[void]$da.Fill($ds);$result=@($ds.Tables|Where-Object {$_.Columns.Contains('UpgradeStatus')});Check ($result.Count -eq 1 -and $result[0].Rows[0]['UpgradeStatus'] -eq 'UPGRADE SUCCESS') 'final success result';$backups.Add([string]$result[0].Rows[0]['VerifiedBackup'])}finally{$da.Dispose();$ds.Dispose();$cmd.Dispose()}
}
try{
 $cn.Open()
 Check ([int](Scalar "SELECT COUNT(*) FROM sys.extended_properties WHERE class=0 AND name=N'CPOS.AuditReleaseSandbox' AND CONVERT(nvarchar(100),value)=DB_NAME()") -eq 1) 'owned sandbox'
 $before=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")
 Check ($before.Contains('-- Audit044:') -and -not $before.Contains('-- Audit046:')) 'starts on 044/045, not 046'
 $source=[guid](Scalar 'SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1')
 $raw=[IO.File]::ReadAllText((Resolve-Path -LiteralPath $QueryPath))
 Add-Type -Path 'C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
 $parser=New-Object Microsoft.SqlServer.TransactSql.ScriptDom.TSql120Parser($true);$errors=$null;$reader=New-Object IO.StringReader($raw)
 try{[void]$parser.Parse($reader,[ref]$errors)}finally{$reader.Dispose()}
 Check ($errors.Count -eq 0) 'SQL2014 query parse'
 $guard="DECLARE @ExpectedDatabase sysname = N'CPOS_ACCOUNTING';"
 Check ([regex]::Matches($raw,[regex]::Escape($guard)).Count -eq 1) 'one database setting'
 $sql=$raw.Replace($guard,"DECLARE @ExpectedDatabase sysname = N'$name';")
 Reject $raw 'Select the customer database';Check $true 'wrong database refused'
 $tx=$cn.BeginTransaction()
 try{Reject $sql 'no open transaction' $tx;$cmd=$cn.CreateCommand();$cmd.Transaction=$tx;$cmd.CommandText='SELECT @@TRANCOUNT';Check ([int]$cmd.ExecuteScalar() -eq 1) 'caller transaction preserved';$cmd.Dispose()}finally{$tx.Rollback();$tx.Dispose()}
 $other=New-Object Data.SqlClient.SqlConnection($b.ConnectionString)
 try{$other.Open();Reject $sql 'Close other CPOS/SSMS';$cmd=$other.CreateCommand();$cmd.CommandText='SELECT 1';Check ([int]$cmd.ExecuteScalar() -eq 1) 'busy refusal does not terminate sessions';$cmd.Dispose()}finally{$other.Dispose()}
 try{
  [void](Scalar $before.Replace('CREATE PROCEDURE','ALTER PROCEDURE').Replace('-- Audit044:','-- TestNo044:'))
  Reject $sql 'requires audit version 044';Check $true 'unknown baseline refused'
 }finally{[void](Scalar $before.Replace('CREATE PROCEDURE','ALTER PROCEDURE'))}
 $badBackup=$sql.Replace('DECLARE @BackupDirectory nvarchar(2000) = NULL;',"DECLARE @BackupDirectory nvarchar(2000) = N'C:\CPOS_NO_DIRECTORY_$([guid]::NewGuid().ToString('N'))';")
 Reject $badBackup 'backup device'
 Check ($before -ceq [string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")) 'backup failure changes nothing'
 $anchor=" IF @history=1`n  EXEC sys.sp_executesql"
 Check ($sql.Contains($anchor)) 'post-migration failure injection point'
 Reject ($sql.Replace($anchor," THROW 51246,'Injected after migration',1;`n"+$anchor)) 'Injected after migration'
 Check ($before -ceq [string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")) 'failure after alteration rolls back writer'
 Check ([int](Scalar "SELECT COUNT(*) FROM dbo.CPOS_Database_Migration WHERE Component=N'NetworkAuditV2' AND MigrationId='046'") -eq 0) 'failed upgrade leaves no history'
 Upgrade $sql
 $after=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")
 Check ($after.Contains('-- Audit046:')) '046 installed'
 Upgrade $sql
 Check ($after -ceq [string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")) 'repeat leaves writer identical'
 Check ([int](Scalar "SELECT COUNT(*) FROM dbo.CPOS_Database_Migration WHERE Component=N'NetworkAuditV2' AND MigrationId='046'") -eq 1) 'one migration history row'
 Check ([guid](Scalar 'SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1') -eq $source) 'source identity preserved'
 Check ([int](Scalar 'SELECT @@TRANCOUNT') -eq 0) 'no open transaction'
}finally{$cn.Dispose()}
[pscustomobject]@{Passed=$true;Checks=$checks;Backups=$backups;QueryHash=(Get-FileHash -LiteralPath $QueryPath -Algorithm SHA256).Hash;Database=$name}|ConvertTo-Json -Depth 4|Set-Content -LiteralPath $ReportPath -Encoding UTF8
