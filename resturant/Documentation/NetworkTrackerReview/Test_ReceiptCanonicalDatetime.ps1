param(
 [Parameter(Mandatory=$true)][string]$ConnectionEnvironmentVariable,
 [Parameter(Mandatory=$true)][string]$OutputDirectory
)
$ErrorActionPreference='Stop'
if(-not [Environment]::GetEnvironmentVariable($ConnectionEnvironmentVariable)){throw 'Connection environment variable is empty'}
if(Test-Path -LiteralPath $OutputDirectory){throw 'Use a new output directory'}
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
New-Item -ItemType Directory -Path $OutputDirectory | Out-Null
$output=(Resolve-Path -LiteralPath $OutputDirectory).Path
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditReceipt.vb',
 'Classes\Audit\AuditDisplayNameResolver.vb','Classes\Security\AgentTypeAccessPolicy.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources+=Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | Select-Object -ExpandProperty FullName
$sources+=Join-Path $PSScriptRoot 'AuditReceiptServiceTests.vb'
$sources+=Join-Path $PSScriptRoot 'AuditReceiptCanonicalDatetimeTests.vb'
$exe=Join-Path $output 'AuditReceiptCanonicalDatetimeTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System,System.Collections.Generic /main:AuditReceiptCanonicalDatetimeTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Receipt canonical datetime tests did not compile'}
& $exe $ConnectionEnvironmentVariable | Tee-Object -FilePath (Join-Path $output 'Results.txt')
if($LASTEXITCODE -ne 0){throw 'Receipt canonical datetime tests failed'}
