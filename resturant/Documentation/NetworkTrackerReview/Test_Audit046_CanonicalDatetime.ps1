param([Parameter(Mandatory=$true)][string]$ConnectionEnvironmentVariable,
      [Parameter(Mandatory=$true)][string]$OutputDirectory)
$ErrorActionPreference='Stop'
if($PSVersionTable.PSEdition -ne 'Desktop'){throw 'Use Windows PowerShell 5.1.'}
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$b=New-Object Data.SqlClient.SqlConnectionStringBuilder([Environment]::GetEnvironmentVariable($ConnectionEnvironmentVariable))
$name=$b['Initial Catalog']; $b['Pooling']=$false
if($name -notmatch '^CPOS_AUDIT_GATE_[a-f0-9]{32}$'){throw 'Production databases refused.'}
if(Test-Path -LiteralPath $OutputDirectory){throw 'Use a fresh output directory.'}
[void](New-Item -ItemType Directory -Path $OutputDirectory)
$checks=New-Object 'Collections.Generic.List[string]'
function Check([bool]$ok,[string]$label){if(-not $ok){throw ('FAIL '+$label)};$checks.Add($label);Write-Output ('PASS '+$label)}
$cn=New-Object Data.SqlClient.SqlConnection($b.ConnectionString)
function Scalar([string]$sql){$cmd=$cn.CreateCommand();$cmd.CommandTimeout=120;$cmd.CommandText=$sql;try{return $cmd.ExecuteScalar()}finally{$cmd.Dispose()}}
try {
 $cn.Open()
 Check ([int](Scalar "SELECT COUNT(*) FROM sys.extended_properties WHERE class=0 AND name=N'CPOS.AuditReleaseSandbox' AND CONVERT(nvarchar(100),value)=DB_NAME()") -eq 1) 'owned sandbox'
 $before=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")
 Check ($before.Contains('-- Audit044:')) '044 baseline present'
 Check ([int](Scalar "SELECT CASE WHEN TRY_CONVERT(datetime,N'2026-11-07T19:40:40.2930000',126) IS NULL AND TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),N'2026-11-07T19:40:40.2930000',126))=CONVERT(datetime,N'2026-11-07T19:40:40.293',126) THEN 1 ELSE 0 END") -eq 1) 'reproduced direct-parse failure and safe round-trip on SQL2014'
 $dom='C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
 Add-Type -Path $dom
 Add-Type -Path (Join-Path $PSScriptRoot 'ReleaseGate\AuditSqlSafetyVisitor.cs') -ReferencedAssemblies $dom,System.Core
 Check ([AuditSqlSafetyVisitor]::Inspect('SELECT TRY_CONVERT(datetime,c.AfterValue,126) FROM t c').Findings.Count -gt 0) 'scanner rejects direct datetime'
 Check ([AuditSqlSafetyVisitor]::Inspect('SELECT CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) FROM t c').Findings.Count -gt 0) 'scanner rejects unsafe outer CONVERT'
 Check ([AuditSqlSafetyVisitor]::Inspect('SELECT TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) FROM t c').Findings.Count -eq 0) 'scanner accepts safe two-stage parsing'
 Check ([AuditSqlSafetyVisitor]::Inspect("DECLARE @s nvarchar(max)=N'SELECT TRY_CONVERT(datetime,@value,126)'").Findings.Count -gt 0) 'scanner rejects direct dynamic datetime'
 $migration=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\046_Audit_Canonical_Datetime.sql'))
 $parser=New-Object Microsoft.SqlServer.TransactSql.ScriptDom.TSql120Parser($true)
 $errors=$null; $reader=New-Object IO.StringReader($migration)
 try{[void]$parser.Parse($reader,[ref]$errors)}finally{$reader.Dispose()}
 Check ($errors.Count -eq 0) 'migration parses as SQL2014'
 $migration=$migration.Replace("DB_NAME()<>N'CPOS_ACCOUNTING'","DB_NAME()<>N'$name'")
 [void](Scalar $migration)
 $after=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")
 Check ($after.Contains('-- Audit046:')) '046 applied to sandbox'
 [void](Scalar $migration)
 Check ($after -ceq [string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")) 'idempotent rerun'
 foreach($spec in @(@('c.AfterValue',60),@('q.[QueueDateValue]',2),@('@value',1))){
   $safe='TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),'+$spec[0]+',126))'
   Check ([regex]::Matches($after,[regex]::Escape($safe)).Count -eq $spec[1]) ('exact conversion count '+$spec[0])
 }
 Check ([int](Scalar @'
DECLARE @values TABLE(v nvarchar(100),expectedDate bit);
INSERT @values VALUES(N'2026-11-07T19:40:40.2930000',1),(N'2026-11-10T12:00:00.0000000',1),
 (N'1753-01-01T00:00:00.0000000',1),(N'9999-12-31T23:59:59.9970000',1),
 (N'2026-11-10T12:00:00.0030000',1),(N'2026-11-10T12:00:00.0070000',1),
 (N'0001-01-01T00:00:00.0000000',0),(N'1700-01-01T00:00:00.0000000',0),
 (N'9999-12-31T23:59:59.9999999',0),(N'1000',0),(N'6.528',0),(N'عميل',0),(NULL,0);
SELECT COUNT(*) FROM @values WHERE (expectedDate=1 AND TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),v,126)) IS NULL)
 OR (expectedDate=0 AND (CASE WHEN TRY_CONVERT(datetime2(7),v,126)<=CONVERT(datetime2(7),'9999-12-31T23:59:59.9970000',126)
 THEN TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),v,126)) END) IS NOT NULL);
'@) -eq 0) 'canonical valid dates, range overflow, malformed/mixed values and null'
 Check ([int](Scalar @'
;WITH n AS (SELECT 0 i UNION ALL SELECT i+1 FROM n WHERE i<999),
 dates AS (SELECT DATEADD(millisecond,i,CONVERT(datetime,'20261110',112)) d FROM n),
 texts AS (SELECT d,REPLACE(CONVERT(nvarchar(23),d,121),N' ',N'T')+N'0000' v FROM dates)
SELECT COUNT(*) FROM texts WHERE TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),v,126)) IS NULL
 OR TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),v,126))<>d OPTION(MAXRECURSION 1000);
'@) -eq 0) '1000 millisecond inputs round-trip to exact SQL datetime'
 foreach($proc in @('Network_Audit_Request_Begin','Network_Audit_Event_Write','Network_Audit_Request_Complete','Network_Audit_Request_Status')){
   $sql=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.$proc'))")
   $scan=[AuditSqlSafetyVisitor]::Inspect($sql)
   Check ($scan.Findings.Count -eq 0) ('SQL2014 safety '+$proc+': '+($scan.Findings -join '; '))
 }
}finally{$cn.Dispose()}
& (Join-Path $PSScriptRoot 'Test_ItemLifecycle.ps1') -OutputDirectory (Join-Path $OutputDirectory 'lifecycle') -ConnectionEnvironmentVariable $ConnectionEnvironmentVariable
if($LASTEXITCODE -ne 0){throw 'Lifecycle tests failed.'}
Check $true 'real item allocation/reuse/quick/full/cleanup/tamper tests with rollback'
& (Join-Path $PSScriptRoot 'Test_ReceiptCanonicalDatetime.ps1') -OutputDirectory (Join-Path $OutputDirectory 'receipts') -ConnectionEnvironmentVariable $ConnectionEnvironmentVariable
if($LASTEXITCODE -ne 0){throw 'Receipt datetime tests failed.'}
Check $true 'real receipt pre-046 reproduction and create/edit/update/void/replay tests with rollback'
[pscustomobject]@{Passed=$true;Checks=$checks;Database=$name;MigrationHash=(Get-FileHash (Join-Path $projectRoot 'Database\Audit\046_Audit_Canonical_Datetime.sql') -Algorithm SHA256).Hash} | ConvertTo-Json -Depth 4 | Set-Content -LiteralPath (Join-Path $OutputDirectory 'Report.json') -Encoding UTF8
