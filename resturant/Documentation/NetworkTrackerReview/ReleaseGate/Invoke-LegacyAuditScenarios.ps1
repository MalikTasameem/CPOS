param([string]$ConnectionEnvironmentVariable, [string]$OutputDirectory,
    [ValidateSet('Phase3B','Phase3C','Phase3D','Phase4A','Phase4B','Phase4C','Phase4D','Phase4E','Phase4F',
        'DocumentCancellation','Units','Groups','ItemUnits','PricingRules','ItemMaster','Tables','Finance',
        'StockDocuments','EmployeeValues','ReturnLines','ExpenseLines','AccountEntries','PurchaseAddition')]
    [string[]]$OnlySuites = @())
$ErrorActionPreference='Stop'
. (Join-Path $PSScriptRoot 'AuditReleaseGate.Common.ps1')
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..')).Path
$builder = New-Object Data.SqlClient.SqlConnectionStringBuilder([Environment]::GetEnvironmentVariable($ConnectionEnvironmentVariable))
$name = $builder['Initial Catalog']
if ($name -notmatch '^CPOS_AUDIT_GATE_[a-f0-9]{32}$') { throw 'Production database refused.' }
$builder['Pooling']=$false
$cn = New-Object Data.SqlClient.SqlConnection($builder.ConnectionString)
try {
    $cn.Open(); $cmd=$cn.CreateCommand()
    $cmd.CommandText="SELECT COUNT(*) FROM sys.extended_properties WHERE class=0 AND name=N'CPOS.AuditReleaseSandbox' AND CONVERT(nvarchar(100),value)=DB_NAME()"
    if ([int]$cmd.ExecuteScalar() -ne 1) { throw 'Sandbox ownership marker required.' }
    $cmd.CommandText='SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1'
    $sourceId=([guid]$cmd.ExecuteScalar()).ToString()
    $cmd.Dispose()
} finally { $cn.Dispose() }
$stage = Join-Path $OutputDirectory 'legacy-workspace\resturant'
if (Test-Path -LiteralPath $stage) { throw 'Use a fresh legacy test workspace.' }
Copy-AuditTestWorkspace $projectRoot $stage
$review=Join-Path $stage 'Documentation\NetworkTrackerReview'
$utf8=New-Object Text.UTF8Encoding($true)
# Existing tests explicitly guard the development database/source. Adapt ONLY their isolated
# copies to our marked restore; the actual business services and production guards stay untouched.
foreach ($file in Get-ChildItem -LiteralPath $review -Filter '*Tests.vb') {
    $text=[IO.File]::ReadAllText($file.FullName).Replace('CPOS_ACCOUNTING',$name).Replace('3c520084-0214-43d4-a5fb-797a179c7a7c',$sourceId)
    $text=[regex]::Replace($text,'File\.ReadAllLines\((settings|settingsPath|path)\)','AuditGateFixture.Settings()')
    [IO.File]::WriteAllText($file.FullName,$text,$utf8)
}
$coordinator=Join-Path $stage 'Classes\Audit\AuditRequestCoordinator.vb'
$text=[IO.File]::ReadAllText($coordinator)
$anchor='lease.Finished = True'
if ([regex]::Matches($text,[regex]::Escape($anchor)).Count -ne 1) { throw 'Coordinator instrumentation anchor changed.' }
$text=$text.Replace($anchor,'AuditGateFixture.Record(cn, tx, lease, resultCode)' + "`r`n            " + $anchor)
$text+="`r`n" + [IO.File]::ReadAllText((Join-Path $PSScriptRoot 'AuditGateFixture.vb'))
[IO.File]::WriteAllText($coordinator,$text,$utf8)
$suites=@('Phase3B','Phase3C','Phase3D','Phase4A','Phase4B','Phase4C','Phase4D','Phase4E','Phase4F',
    'DocumentCancellation','Units','Groups','ItemUnits','PricingRules','ItemMaster','Tables','Finance',
    'StockDocuments','EmployeeValues','ReturnLines','ExpenseLines','AccountEntries','PurchaseAddition')
if ($OnlySuites.Count -gt 0) { $suites = @($suites | Where-Object { $_ -in $OnlySuites }) }
$oldConnection=$env:CPOS_AUDIT_GATE_FIXTURE_CONNECTION
$oldCoverage=$env:CPOS_AUDIT_GATE_FIXTURE_COVERAGE
$covered=@(); $failures=@(); $results=@()
try {
    $env:CPOS_AUDIT_GATE_FIXTURE_CONNECTION=$builder.ConnectionString
    foreach ($suite in $suites) {
        $path=Join-Path $review ('Test_'+$suite+'.ps1')
        $log=Join-Path $OutputDirectory ('Live_'+$suite+'.log')
        $coverage=Join-Path $OutputDirectory ('Live_'+$suite+'.coverage')
        $env:CPOS_AUDIT_GATE_FIXTURE_COVERAGE=$coverage
        # Keep the original process/session guards. A busy machine yields a blocked test, not a false pass.
        $command='$ErrorActionPreference="Stop"; $ProgressPreference="SilentlyContinue"; $PSDefaultParameterValues["Get-Content:Encoding"]="UTF8"; try { & ''' + $path.Replace("'","''") + ''' -LiveRollbackTests; if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }; exit 0 } catch { Write-Output $_; exit 1 }'
        $result=Invoke-AuditGateChild $command $log 180
        $results += [pscustomobject]@{Name=$suite;Passed=$result.Passed;Log=$log;LogHash=(Get-FileHash -LiteralPath $log -Algorithm SHA256).Hash}
        if ($result.Passed) {
            if (Test-Path -LiteralPath $coverage) { $covered += [IO.File]::ReadAllLines($coverage) }
        } else { $failures+=('Live suite failed: '+$suite) }
        Write-Output ('LIVE {0} {1}' -f $(if($result.Passed){'PASS'}else{'FAIL'}),$suite)
    }
} finally {
    $env:CPOS_AUDIT_GATE_FIXTURE_CONNECTION=$oldConnection
    $env:CPOS_AUDIT_GATE_FIXTURE_COVERAGE=$oldCoverage
}
[pscustomobject]@{CoveredOperations=@($covered | Sort-Object -Unique);Failures=$failures;Suites=$results} | ConvertTo-Json -Depth 6 | Set-Content -LiteralPath (Join-Path $OutputDirectory 'LegacyScenarios.json') -Encoding UTF8
