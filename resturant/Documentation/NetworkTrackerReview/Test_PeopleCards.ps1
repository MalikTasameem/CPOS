param([string]$ConnectionString,[switch]$LiveRollback)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditPeopleCards035'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSalesEdit.vb',
 'Classes\Audit\AuditSalesLineEdit.vb','Classes\Audit\AuditSalesQuantity.vb','Classes\Audit\AuditPeopleCards.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources+=Get-ChildItem (Join-Path $projectRoot 'Classes\Audit\AuditContract.*.vb') | ForEach-Object FullName
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditPeopleCardsTests.vb'
$exe=Join-Path $output 'AuditPeopleCardsTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditPeopleCardsTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'People card tests did not compile'}
& $exe
if($LASTEXITCODE -ne 0){throw 'People card tests failed'}
if($ConnectionString){
 & $exe '--live-read' $ConnectionString
 if($LASTEXITCODE -ne 0){throw 'People card live read tests failed'}
}
if($LiveRollback){
 if(-not $ConnectionString){throw 'LiveRollback requires ConnectionString'}
 & $exe '--live-rollback' $ConnectionString
 if($LASTEXITCODE -ne 0){throw 'People card rolled-back save tests failed'}
}
# Default run is offline. -ConnectionString adds read-only request construction against the deployed schema.
