param()
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditInventory036'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSalesEdit.vb',
 'Classes\Audit\AuditSalesLineEdit.vb','Classes\Audit\AuditSalesQuantity.vb','Classes\Audit\AuditFinance.vb',
 'Classes\Audit\AuditInventoryOperations.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources += @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | ForEach-Object {$_.FullName})
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources += Join-Path $PSScriptRoot 'AuditInventoryOperationsTests.vb'
$exe=Join-Path $output 'AuditInventoryOperationsTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditInventoryOperationsTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Inventory tests did not compile'}
& $exe
if($LASTEXITCODE -ne 0){throw 'Inventory tests failed'}

# Parse the deployment and embedded procedure bodies without connecting to SQL Server.
Add-Type -Path 'C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
$inventorySql=Get-Content -LiteralPath (Join-Path $projectRoot 'Database\Audit\036_Inventory_Operations.sql') -Raw
$inventoryService=Get-Content -LiteralPath (Join-Path $projectRoot 'Classes\Audit\AuditInventoryOperations.vb') -Raw
$inventoryParser=New-Object Microsoft.SqlServer.TransactSql.ScriptDom.TSql120Parser($true)
$inventoryErrors=$null
$null=$inventoryParser.Parse((New-Object IO.StringReader($inventorySql)),[ref]$inventoryErrors)
if($inventoryErrors.Count){throw ($inventoryErrors | ForEach-Object {$_.Message} | Out-String)}
$inventoryMatches=[regex]::Matches($inventorySql,"EXEC sys\.sp_executesql N'((?:[^']|'')*)';")
$inventoryProcedures=0
foreach($inventoryMatch in $inventoryMatches){
 $inventoryBody=$inventoryMatch.Groups[1].Value.Replace("''","'")
 $inventoryName=[regex]::Match($inventoryBody,'CREATE PROCEDURE \[dbo\]\.\[([^\]]+)\]').Groups[1].Value
 if(!$inventoryName){continue}
 $inventoryProcedures++
 $inventoryErrors=$null
 $null=$inventoryParser.Parse((New-Object IO.StringReader($inventoryBody)),[ref]$inventoryErrors)
 if($inventoryErrors.Count){throw ($inventoryErrors | ForEach-Object {$_.Message} | Out-String)}
 if($inventoryBody.Contains('##') -or $inventoryBody -match 'EXEC\s+\[?CREATE_PCH_TMP_TABLE'){throw 'Global temporary snapshot leaked into new procedures'}
 $inventoryHasher=[Security.Cryptography.SHA256]::Create()
 try{$inventoryHash=[BitConverter]::ToString($inventoryHasher.ComputeHash([Text.Encoding]::Unicode.GetBytes($inventoryBody))).Replace('-','')}finally{$inventoryHasher.Dispose()}
 if(!$inventoryService.Contains('{"'+$inventoryName+'", "'+$inventoryHash+'"}')){throw "Embedded procedure fingerprint differs: $inventoryName"}
}
if($inventoryProcedures -ne 2){throw 'Two isolated cost procedures expected'}
if($inventoryService -notmatch 'Dim after = Capture\(cn, tx, r, before\)'){throw 'Deletion scope is not retained'}
if($inventoryService -notmatch 'For Each e In Events\(r, before, after,'){throw 'All events must be built and validated before any writer call'}
Write-Output 'PASS SQL 2014 parse, two exact procedure fingerprints, local temp isolation, retained scope, fail-closed event construction.'
# Offline only. Does not connect, install SQL, run rollback fixtures, or replace a runtime.
