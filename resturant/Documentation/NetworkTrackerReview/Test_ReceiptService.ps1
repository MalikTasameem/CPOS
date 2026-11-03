param([string]$ConnectionString,[switch]$LiveRollback)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditReceiptService041'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditReceipt.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources+=Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | Select-Object -ExpandProperty FullName
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditReceiptServiceTests.vb'
$exe=Join-Path $output 'AuditReceiptServiceTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditReceiptServiceTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Receipt service tests did not compile'}
& $exe
if($LASTEXITCODE -ne 0){throw 'Receipt service tests failed'}
if($LiveRollback){
 if(-not $ConnectionString){throw 'LiveRollback requires ConnectionString'}
 & $exe '--live-void-rollback' $ConnectionString
 if($LASTEXITCODE -ne 0){throw 'Receipt administrator void rollback test failed'}
}
$service=Get-Content -LiteralPath (Join-Path $projectRoot 'Classes\Audit\AuditReceipt.vb') -Raw
if($service -notmatch 'parameter.Value = If\(sqlType = SqlDbType.Decimal, Money\(r, p\(0\)\), Input\(r, p\(0\)\)\)'){throw 'Money rounding must precede SqlDecimal binding'}
if($service -notmatch 'RequireTransaction\(cn, tx\)\s+If create AndAlso'){throw 'Require live transaction after swallowed SQL rollback'}
if($service -notmatch 'Dim after = Capture\(cn, tx, r, before\), eventsList = Events'){throw 'Validate all events before first writer'}
if($service -notmatch 'LEFT JOIN dbo\.Users_Validations_V' -or $service -notmatch 'u\.isAdmin=1 OR ISNULL\(v\.\['){throw 'Receipt service must preserve the administrator permission override'}
$sql=Get-Content -LiteralPath (Join-Path $projectRoot 'Database\Audit\041_Receipts.sql') -Raw
$match=[regex]::Matches($sql,"EXEC sys\.sp_executesql N'((?:[^']|'')*)';") | Where-Object {$_.Groups[1].Value.Contains('CREATE PROCEDURE [dbo].[Agents_BalanceMV_insert_RCT_Audited]')}
if(@($match).Count -ne 1){throw 'Expected one isolated receipt procedure'}
$body=$match.Groups[1].Value.Replace("''","'").Replace(([string][char]13+[char]10),[string][char]10)
$hasher=[Security.Cryptography.SHA256]::Create()
try{$digest=[BitConverter]::ToString($hasher.ComputeHash([Text.Encoding]::Unicode.GetBytes($body))).Replace('-','')}finally{$hasher.Dispose()}
if(!$service.Contains('{"Agents_BalanceMV_insert_RCT_Audited", "'+$digest+'"}')){throw 'Isolated receipt normalized hash mismatch'}
if($service -notmatch 'definition = definition.Replace\(vbCrLf, vbLf\)'){throw 'Isolated procedure line-ending normalization missing'}
Write-Output 'PASS normalized procedure hash, explicit SQL rounding, transaction survival, validate-before-write. Offline only.'
