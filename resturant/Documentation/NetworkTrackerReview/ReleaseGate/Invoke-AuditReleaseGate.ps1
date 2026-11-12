param(
    [ValidateSet('Check','Release')][string]$Mode = 'Check',
    [string]$SqlConnectionEnvironmentVariable = '',
    [string]$OutputDirectory = ''
)
$ErrorActionPreference = 'Stop'
if ($PSVersionTable.PSEdition -ne 'Desktop') { throw 'Use Windows PowerShell 5.1 (powershell.exe), required by the SQL 2014 ScriptDom tests.' }
. (Join-Path $PSScriptRoot 'AuditReleaseGate.Common.ps1')
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..')).Path
$review = Join-Path $projectRoot 'Documentation\NetworkTrackerReview'
if (-not $OutputDirectory) { $OutputDirectory = Join-Path $projectRoot ('..\.tmp\AuditReleaseGate\' + [DateTime]::UtcNow.ToString('yyyyMMdd_HHmmss_fff')) }
[void](New-Item -ItemType Directory -Path $OutputDirectory -Force)
$OutputDirectory = (Resolve-Path -LiteralPath $OutputDirectory).Path
$fingerprint = Get-AuditReleaseFingerprint $projectRoot
$stageProject = Join-Path $OutputDirectory 'workspace\resturant'
if (Test-Path -LiteralPath $stageProject) { throw 'Use a new output directory for every run; previous evidence must not be overwritten.' }
Copy-AuditTestWorkspace $projectRoot $stageProject
$stageReview = Join-Path $stageProject 'Documentation\NetworkTrackerReview'
$failures = New-Object 'Collections.Generic.List[string]'
$results = New-Object 'Collections.Generic.List[object]'
$operations = @()
$sqlChecked = $false
$database = ''
$suites = @('Phase1','Phase2A','Phase2B','Phase3A','Phase3B','Phase3C','Phase3D','Phase4A','Phase4B','Phase4C','Phase4D','Phase4E','Phase4F',
 'AdministrativeRestore','DocumentCancellation','Units','Groups','ItemUnits','PricingRules','ItemMaster','Tables','Finance',
 'StockDocuments','EmployeeValues','ReturnLines','ExpenseLines','AccountEntries','PurchaseAddition','DocumentFields','QuoteLines',
 'PeopleCards','InventoryOperations','DocumentWorkflows','ItemLifecycle','SalaryBatch','TableSplit','SettlementV2Only',
 'Receipts','ReceiptService','ReceiptUi_Offline','ReceiptSql_Offline')
try {
    $sources = @('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb','Classes\Audit\AuditContract.vb') | ForEach-Object { Join-Path $projectRoot $_ }
    $sources += Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | Select-Object -ExpandProperty FullName
    $sources += Join-Path $PSScriptRoot 'AuditGateCatalogue.vb'
    $exe = Join-Path $OutputDirectory 'AuditGateCatalogue.exe'
    & 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditGateCatalogue /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
    if ($LASTEXITCODE -ne 0) { throw 'Catalogue/culture test compilation failed.' }
    $catalogue = @(& $exe (Join-Path $projectRoot 'Classes\Audit'))
    if ($LASTEXITCODE -ne 0) { throw 'Catalogue/culture tests failed.' }
    $operations = @($catalogue | ForEach-Object {
        $parts = $_ -split "`t"
        if ($parts.Length -ne 2) { throw 'Invalid catalogue output.' }
        [pscustomobject]@{ Code=$parts[0]; Domain=$parts[1]; SqlRegistered=$false; LivePassed=$false }
    })
    if ($operations.Count -lt 1) { throw 'Empty operation catalogue.' }
    # Resolve literals used by production audit services, not a hand-maintained list of screens.
    $domains = @($operations.Domain | Sort-Object -Unique)
    # Explicit non-event literals: snapshot keys, legacy command-intent labels and COM ProgID.
    $nonOperations = @('AuditTest.Changed','References.Companies','References.Groups','References.Units',
        'Returns.Purchase','Returns.Sales','StockReceipt.QuantityStep','Return.QuantityStep','Excel.Application')
    $unknown = @()
    $productionFiles = @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter '*.vb') + @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Forms') -Filter '*.vb' -Recurse)
    foreach ($file in $productionFiles) {
        if ($file.Name -like 'AuditContract*' -or $file.Name -like '*.Designer.vb') { continue }
        $source = [IO.File]::ReadAllText($file.FullName)
        foreach ($match in [regex]::Matches($source, '"([A-Z][A-Za-z0-9]+\.[A-Z][A-Za-z0-9]+)"')) {
            $code = $match.Groups[1].Value
            # StartsWith accepts an operation family prefix, not a complete operation code.
            $before = $source.Substring(0,$match.Index)
            if ($before -match 'StartsWith\(\s*$') { continue }
            if ($code -notin $operations.Code -and $code -notin $domains -and $code -notin $nonOperations) { $unknown += ($file.Name + ': ' + $code) }
        }
    }
    if ($unknown.Count) { $failures.Add('Unregistered service operation literals: ' + (($unknown | Sort-Object -Unique) -join ', ')) }
} catch { $failures.Add('Catalogue: ' + $_.Exception.Message) }

foreach ($suite in $suites) {
    $path = Join-Path $stageReview ('Test_' + $suite + '.ps1')
    $log = Join-Path $OutputDirectory ($suite + '.log')
    # Deliberately no live, install or candidate switch. These scripts are reviewed offline entry points.
    $command = '$ErrorActionPreference="Stop"; $ProgressPreference="SilentlyContinue"; $PSDefaultParameterValues["Get-Content:Encoding"]="UTF8"; try { & ''' + $path.Replace("'","''") + '''; if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }; exit 0 } catch { Write-Output $_; exit 1 }'
    $result = Invoke-AuditGateChild $command $log
    $results.Add([pscustomobject]@{ Name=$suite; Passed=$result.Passed; ExitCode=$result.ExitCode; Log=$log; LogHash=(Get-FileHash -LiteralPath $log -Algorithm SHA256).Hash })
    if (-not $result.Passed) { $failures.Add('Offline suite failed: ' + $suite + ' ' + $result.Reason) }
    Write-Output ('{0} {1}' -f $(if ($result.Passed) {'PASS'} else {'FAIL'}),$suite)
}

$log = Join-Path $OutputDirectory 'ReleaseGateSelfTest.log'
$path = Join-Path $PSScriptRoot 'Test-AuditReleaseGate.ps1'
$command = '$ErrorActionPreference="Stop"; $ProgressPreference="SilentlyContinue"; try { & ''' + $path.Replace("'","''") + '''; exit 0 } catch { Write-Output $_; exit 1 }'
$selfTest = Invoke-AuditGateChild $command $log
$results.Add([pscustomobject]@{ Name='ReleaseGateSelfTest'; Passed=$selfTest.Passed; ExitCode=$selfTest.ExitCode; Log=$log; LogHash=(Get-FileHash -LiteralPath $log -Algorithm SHA256).Hash })
if (-not $selfTest.Passed) { $failures.Add('Release gate self-tests failed.') }

if ($SqlConnectionEnvironmentVariable) {
    try {
        . (Join-Path $PSScriptRoot 'Test-AuditSqlCandidate.ps1')
        $sql = Test-AuditSqlCandidate -ConnectionEnvironmentVariable $SqlConnectionEnvironmentVariable -Operations $operations -RequireSandbox:($Mode -eq 'Release') -OutputDirectory $OutputDirectory
        $sqlChecked = $true
        $database = $sql.Database
        foreach ($problem in $sql.Failures) { $failures.Add($problem) }
        if ($Mode -eq 'Release') {
            & (Join-Path $PSScriptRoot 'Invoke-AuditLiveScenarios.ps1') -ConnectionEnvironmentVariable $SqlConnectionEnvironmentVariable -OutputDirectory $OutputDirectory
            $live = Get-Content -LiteralPath (Join-Path $OutputDirectory 'LiveScenarios.json') -Raw -Encoding UTF8 | ConvertFrom-Json
            foreach ($operation in $operations) { $operation.LivePassed = $operation.Code -in @($live.CoveredOperations) }
            foreach ($problem in $live.Failures) { $failures.Add($problem) }
        }
    } catch { $failures.Add('SQL/live verification: ' + $_.Exception.Message) }
}
if ($Mode -eq 'Release') {
    if (-not $sqlChecked) { $failures.Add('Release requires a SQL candidate connection on an isolated CPOS_AUDIT_GATE_* database.') }
    $missing = @($operations | Where-Object { -not $_.LivePassed } | ForEach-Object Code)
    if ($missing.Count) { $failures.Add(('Missing successful SQL integration scenarios ({0}): {1}' -f $missing.Count, ($missing -join ', '))) }
}
if ((Get-AuditReleaseFingerprint $projectRoot) -ne $fingerprint) { $failures.Add('Inputs changed while the gate was running; rerun against a stable checkout.') }
$report = [pscustomobject]@{
    Schema=1; Mode=$Mode; Passed=($failures.Count -eq 0); SourceFingerprint=$fingerprint
    FinishedUtc=[DateTime]::UtcNow.ToString('o'); SqlChecked=$sqlChecked; Database=$database
    Operations=$operations; Suites=@($results.ToArray()); Failures=@($failures.ToArray())
}
$reportPath = Join-Path $OutputDirectory 'AuditReleaseReport.json'
$report | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath $reportPath -Encoding UTF8
$lines = @('# Audit release gate', '', ('Mode: ' + $Mode), ('Result: ' + $(if ($report.Passed) {'PASS'} else {'BLOCKED'})),
    ('Operations: ' + $operations.Count), ('Offline suites: ' + @($results | Where-Object Passed).Count + '/' + $results.Count),
    ('Successful live operations: ' + @($operations | Where-Object LivePassed).Count + '/' + $operations.Count),
    '', 'A Check run is diagnostic only and NEVER authorizes packaging.', '', '## Blockers', '')
$lines += @($failures | ForEach-Object { '- ' + $_ })
$lines += @('', '## Operation coverage', '', '| Operation | SQL registration | Live scenario |', '|---|---|---|')
$lines += @($operations | ForEach-Object { '| ' + $_.Code + ' | ' + $_.SqlRegistered + ' | ' + $_.LivePassed + ' |' })
$lines | Set-Content -LiteralPath (Join-Path $OutputDirectory 'AuditReleaseReport.md') -Encoding UTF8
Write-Output ('REPORT=' + $reportPath)
if (-not $report.Passed) { throw 'Audit gate blocked. Read AuditReleaseReport.md and suite logs.' }
