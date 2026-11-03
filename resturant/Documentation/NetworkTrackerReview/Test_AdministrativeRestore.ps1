$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditAdministrativeRestore'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Classes\Audit\AdministrativeRestoreAudit.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources+=Join-Path $PSScriptRoot 'AdministrativeRestoreTests.vb'
$exe=Join-Path $output 'AdministrativeRestoreTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AdministrativeRestoreTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Administrative restore tests did not compile'}
& $exe $output
if($LASTEXITCODE -ne 0){throw 'Administrative restore tests failed'}
