param([Parameter(Mandatory=$true)][string]$ConnectionEnvironmentVariable, [Parameter(Mandatory=$true)][string]$OutputDirectory)
$ErrorActionPreference = 'Stop'
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..')).Path
$covered = @()
$failures = @()
# Do not infer coverage from source text or an offline test name. Each scenario must
# call the real service and verify its business effect, persisted event and rollback.
$sources = @('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
    'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
    'Classes\Audit\AuditDisplayNameResolver.vb','Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb',
    'Classes\Audit\AuditUnits.vb') | ForEach-Object { Join-Path $projectRoot $_ }
$sources += Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | Select-Object -ExpandProperty FullName
$sources += Join-Path $PSScriptRoot 'AuditGateLiveTests.vb'
$exe = Join-Path $OutputDirectory 'AuditGateLiveTests.exe'
try {
    & 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditGateLiveTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
    if ($LASTEXITCODE -ne 0) { throw 'Live scenario compilation failed.' }
    # The secret never appears on the command line or in the report.
    $output = @(& $exe $ConnectionEnvironmentVariable)
    $output | Set-Content -LiteralPath (Join-Path $OutputDirectory 'LiveScenarios.log') -Encoding UTF8
    if ($LASTEXITCODE -ne 0) { throw 'Live scenarios failed; no coverage is granted.' }
    $covered = @($output | Where-Object { $_ -match '^COVERED=' } | ForEach-Object { $_.Substring(8) })
} catch { $failures += $_.Exception.Message }
try {
    & (Join-Path $PSScriptRoot 'Invoke-LegacyAuditScenarios.ps1') -ConnectionEnvironmentVariable $ConnectionEnvironmentVariable -OutputDirectory $OutputDirectory
    $legacy = Get-Content -LiteralPath (Join-Path $OutputDirectory 'LegacyScenarios.json') -Raw -Encoding UTF8 | ConvertFrom-Json
    $covered += @($legacy.CoveredOperations)
    $failures += @($legacy.Failures)
} catch { $failures += ('Legacy live scenarios: ' + $_.Exception.Message) }
[void]($covered = @($covered | Sort-Object -Unique))
[pscustomobject]@{ CoveredOperations=$covered; Failures=$failures } | ConvertTo-Json -Depth 4 | Set-Content -LiteralPath (Join-Path $OutputDirectory 'LiveScenarios.json') -Encoding UTF8
