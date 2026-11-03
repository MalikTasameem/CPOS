param([switch]$Catalog)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditReceipt041'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources+=Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | Select-Object -ExpandProperty FullName
$sources+=Join-Path $PSScriptRoot 'AuditReceiptTests.vb'
$exe=Join-Path $output 'AuditReceiptTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditReceiptTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Receipt tests did not compile'}
if($Catalog){& $exe catalog}else{& $exe}
if($LASTEXITCODE -ne 0){throw 'Receipt tests failed'}
# Offline only: no SQL connection, fixtures, deployment, or runtime replacement.
