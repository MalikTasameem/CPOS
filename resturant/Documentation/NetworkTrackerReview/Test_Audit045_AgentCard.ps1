param([Parameter(Mandatory=$true)][string]$ConnectionEnvironmentVariable,
      [Parameter(Mandatory=$true)][string]$OutputDirectory)
$ErrorActionPreference='Stop'
if ($PSVersionTable.PSEdition -ne 'Desktop') { throw 'Use Windows PowerShell 5.1.' }
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$builder=New-Object Data.SqlClient.SqlConnectionStringBuilder([Environment]::GetEnvironmentVariable($ConnectionEnvironmentVariable))
$name=$builder['Initial Catalog']
if ($name -notmatch '^CPOS_AUDIT_GATE_[a-f0-9]{32}$') { throw 'Production database refused.' }
$builder['Pooling']=$false
if (Test-Path -LiteralPath $OutputDirectory) { throw 'Use a new output directory.' }
[void](New-Item -ItemType Directory -Path $OutputDirectory)
$cn=New-Object Data.SqlClient.SqlConnection($builder.ConnectionString)
function Scalar([string]$Sql) {
    $cmd=$cn.CreateCommand(); $cmd.CommandTimeout=120; $cmd.CommandText=$Sql
    try { return $cmd.ExecuteScalar() } finally { $cmd.Dispose() }
}
try {
    $cn.Open()
    if ([int](Scalar "SELECT COUNT(*) FROM sys.extended_properties WHERE class=0 AND name=N'CPOS.AuditReleaseSandbox' AND CONVERT(nvarchar(100),value)=DB_NAME()") -ne 1) { throw 'Sandbox ownership marker required.' }
    $migrations=@('Database\Audit\045_Audit_Safe_Document_Type.sql','Database\Security\002_User_Validation_Agent_Card.sql')
    foreach ($migration in $migrations) {
        $sql=[IO.File]::ReadAllText((Join-Path $projectRoot $migration)).Replace("DB_NAME()<>N'CPOS_ACCOUNTING'","DB_NAME()<>N'$name'")
        [void](Scalar $sql)
        $before=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")
        [void](Scalar $sql)
        if ($before -cne [string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")) { throw 'Migration rerun changed writer.' }
        Write-Output "PASS apply/idempotence $migration"
    }
    $dom='C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
    Add-Type -Path $dom
    Add-Type -Path (Join-Path $PSScriptRoot 'ReleaseGate\AuditSqlSafetyVisitor.cs') -ReferencedAssemblies $dom,System.Core
    foreach ($procedure in @('Network_Audit_Request_Begin','Network_Audit_Event_Write','Network_Audit_Request_Complete','Network_Audit_Request_Status')) {
        $definition=[string](Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.$procedure'))")
        [IO.File]::WriteAllText((Join-Path $OutputDirectory "$procedure.sql"),$definition,(New-Object Text.UTF8Encoding($true)))
        $scan=[AuditSqlSafetyVisitor]::Inspect($definition)
        if ($scan.Findings.Count -ne 0) { throw ($scan.Findings -join "`n") }
        Write-Output "PASS SQL2014 safety $procedure"
    }
    if ([int](Scalar @'
DECLARE @changes TABLE(FieldCode varchar(64),AfterValue nvarchar(100));
INSERT @changes VALUES('DocumentType','7'),('Other','عميل'),('Other','2026-11-10'),('Other','2147483648'),('Other','6.528');
DECLARE @docType int=(SELECT TRY_CONVERT(int,AfterValue) FROM @changes WHERE FieldCode='DocumentType');
IF @docType<>7 THROW 51145,'Valid document type changed.',1;
DECLARE @invalid TABLE(v nvarchar(100));
INSERT @invalid VALUES(N'عميل'),(N'2147483648'),(N'1.5'),(N'2026-11-10'),(NULL);
IF EXISTS(SELECT 1 FROM @invalid WHERE TRY_CONVERT(int,v) IS NOT NULL) THROW 51145,'Invalid integer conversion accepted.',1;
SELECT 1;
'@) -ne 1) { throw 'Mixed integer value probe failed.' }
    Write-Output 'PASS mixed text/overflow/null integer probe'
} finally { $cn.Dispose() }
$exe=Join-Path $OutputDirectory 'AgentCardAccessTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AgentCardAccessTests /reference:System.dll,System.Core.dll,System.Data.dll "/out:$exe" (Join-Path $projectRoot 'Classes\Security\AgentCardAccessPolicy.vb') (Join-Path $PSScriptRoot 'AgentCardAccessTests.vb')
if ($LASTEXITCODE -ne 0) { throw 'Permission test compilation failed.' }
& $exe $ConnectionEnvironmentVariable (Join-Path $projectRoot 'Database\Security\002_User_Validation_Agent_Card.sql')
if ($LASTEXITCODE -ne 0) { throw 'Permission integration tests failed.' }
& (Join-Path $PSScriptRoot 'ReleaseGate\Invoke-LegacyAuditScenarios.ps1') -ConnectionEnvironmentVariable $ConnectionEnvironmentVariable -OutputDirectory $OutputDirectory -OnlySuites Phase4B,DocumentCancellation
$results=Get-Content -LiteralPath (Join-Path $OutputDirectory 'LegacyScenarios.json') -Raw | ConvertFrom-Json
if (@($results.Failures).Count -ne 0) { throw ($results.Failures -join '; ') }
Write-Output 'PASS focused audit045 and Agent screen tests. This is not a full release certificate.'
