param()
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditSalaryBatch039'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSalesEdit.vb',
 'Classes\Audit\AuditSalesLineEdit.vb','Classes\Audit\AuditSalesQuantity.vb','Classes\Audit\AuditPeopleCards.vb','Classes\Audit\AuditSalaryBatch.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources+=Get-ChildItem (Join-Path $projectRoot 'Classes\Audit\AuditContract.*.vb') | ForEach-Object FullName
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditSalaryBatchTests.vb'
$exe=Join-Path $output 'AuditSalaryBatchTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditSalaryBatchTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Salary batch tests did not compile'}
& $exe
if($LASTEXITCODE -ne 0){throw 'Salary batch tests failed'}
# Offline only. The migration and live transaction/retry/security tests require a separate deployment decision.


