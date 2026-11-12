$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'AuditReleaseGate.Common.ps1')
$dom = 'C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
Add-Type -Path $dom
Add-Type -Path (Join-Path $PSScriptRoot 'AuditSqlSafetyVisitor.cs') -ReferencedAssemblies $dom,System.Core
$cases = @(
    @{Sql='SELECT CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) FROM @c c;'; Unsafe=$true},
    @{Sql='SELECT CAST(c.BeforeValue AS decimal(18,3)) FROM @c c;'; Unsafe=$true},
    @{Sql='SELECT CONVERT(int,c.AfterValue) FROM @c c;'; Unsafe=$true},
    @{Sql="EXEC sys.sp_executesql N'SELECT CONVERT(datetime,TRY_CONVERT(datetime2(7),@value,126))';"; Unsafe=$true},
    @{Sql='SELECT TRY_CONVERT(datetime,c.AfterValue,126) FROM @c c;'; Unsafe=$true},
    @{Sql="EXEC sys.sp_executesql N'SELECT TRY_CONVERT(datetime,@value,126)';"; Unsafe=$true},
    @{Sql='SELECT TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) FROM @c c;'; Unsafe=$false},
    @{Sql="EXEC sys.sp_executesql N'SELECT TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),@value,126))';"; Unsafe=$false},
    @{Sql='SELECT CONVERT(nvarchar(100),c.AfterValue) FROM @c c;'; Unsafe=$false},
    @{Sql='-- CONVERT(datetime,c.AfterValue) is historical documentation'; Unsafe=$false}
)
foreach ($case in $cases) {
    $found = [AuditSqlSafetyVisitor]::Inspect($case.Sql)
    if (($found.Findings.Count -gt 0) -ne $case.Unsafe) { throw ('SQL safety detector regression: ' + $case.Sql) }
}
$catalogue = [AuditSqlSafetyVisitor]::Inspect("IF @OperationCode NOT IN ('Unit.Created','Unit.Deleted') THROW 51000,'unknown',1; IF @code='Unit.Updated' SELECT 1;")
if ($catalogue.DeclaredOperations.Count -ne 3 -or $catalogue.DeclaredOperations.Contains('unknown')) { throw 'SQL operation detection failed.' }
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..')).Path
$blocked = $false
try { Assert-AuditReleaseEvidence $projectRoot '' } catch { $blocked = $true }
if (-not $blocked) { throw 'Missing evidence must block packaging.' }
Write-Output 'PASS gate detector: unsafe direct/nested/dynamic conversions, safe TRY_CONVERT, comments, operation declarations, missing release evidence.'
