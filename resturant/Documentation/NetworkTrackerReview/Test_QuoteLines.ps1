param()
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditQuote034'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSalesEdit.vb',
 'Classes\Audit\AuditSalesLineEdit.vb','Classes\Audit\AuditSalesQuantity.vb','Classes\Audit\AuditFinance.vb',
 'Classes\Audit\AuditDocumentFields.vb','Classes\Audit\AuditQuoteLines.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources += @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | ForEach-Object { $_.FullName })
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditQuoteLinesTests.vb'
$exe=Join-Path $output 'AuditQuoteLinesTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditQuoteLinesTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Quote line tests did not compile'}
& $exe
if($LASTEXITCODE -ne 0){throw 'Quote line tests failed'}
# Offline only. Does not connect, install SQL, or run rollback fixtures.
