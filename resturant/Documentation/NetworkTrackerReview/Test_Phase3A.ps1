param([switch]$LiveWriteTests)
$ErrorActionPreference = 'Stop'
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output = Join-Path $projectRoot '..\.tmp\AuditPhase3A'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources = @(
 'Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSecuritySession.vb'
) | ForEach-Object { Join-Path $projectRoot $_ }
$sources += Join-Path $PSScriptRoot 'AuditSecurityTests.vb'
$exe = Join-Path $output 'AuditSecurityTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditSecurityTests `
    /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if ($LASTEXITCODE -ne 0) { throw 'Security/test compilation failed.' }
$arguments = @('--output', $output)
if ($LiveWriteTests) { $arguments += @('--settings', (Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings')) }
& $exe $arguments
if ($LASTEXITCODE -ne 0) { throw 'Security tests failed; inspect the recovery manifest if cleanup failed.' }
