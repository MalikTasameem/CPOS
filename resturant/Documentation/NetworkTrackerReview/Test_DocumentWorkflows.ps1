param()
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditWorkflows037'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSalesEdit.vb',
 'Classes\Audit\AuditSalesLineEdit.vb','Classes\Audit\AuditSalesQuantity.vb','Classes\Audit\AuditFinance.vb',
 'Classes\Audit\AuditDocumentFields.vb','Classes\Audit\AuditDocumentWorkflows.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources+=Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | Select-Object -ExpandProperty FullName
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditDocumentWorkflowsTests.vb'
$exe=Join-Path $output 'AuditDocumentWorkflowsTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditDocumentWorkflowsTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Workflow tests did not compile'}
& $exe
if($LASTEXITCODE -ne 0){throw 'Workflow tests failed'}
$sql=Get-Content -LiteralPath (Join-Path $projectRoot 'Database\Audit\037_Document_Workflows.sql') -Raw
$match=[regex]::Match($sql,"EXEC sys\.sp_executesql N'((?:''|[^'])*)';")
if(-not $match.Success){throw 'Isolated quote procedure not found'}
$procedure=$match.Groups[1].Value.Replace("''", "'")
if($procedure -match '\bSB_Contents\b|\bSB_IM_Components\b' -or $procedure -notmatch 'UPDATE ViewBill_Contents' -or $procedure -notmatch 'BsType_ID=16'){throw 'Quote distribution isolation failed'}
$writerMatch=[regex]::Match($sql,"SET @sql=REPLACE\(@sql,@old,N'(-- AuditDocumentWorkflows037:(?:''|[^'])*)'\+@old\);")
if(-not $writerMatch.Success){throw 'Workflow writer body not found'}
$writer=$writerMatch.Groups[1].Value.Replace("''", "'")
foreach($entity in @('WorkflowHeader','WorkflowReference','WorkflowCurrency','WorkflowQuoteLine')){
 if(-not $writer.Contains("$entity after-state does not match storage.")){throw "Actual after-state check missing: $entity"}
}
if(-not $writer.Contains('DATALENGTH') -or -not $writer.Contains('TRY_CONVERT(datetime2(7),c.AfterValue,126)')){throw 'Canonical string/date comparisons missing'}
Add-Type -Path 'C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
foreach($text in @($sql,$procedure,$writer)){
 $parser=[Microsoft.SqlServer.TransactSql.ScriptDom.TSql120Parser]::new($true)
 $reader=[IO.StringReader]::new($text)
 $errors=$null
 $null=$parser.Parse($reader,[ref]$errors)
 $reader.Dispose()
 if($errors.Count){$errors|ForEach-Object {Write-Host $_.Message};throw 'SQL Server 2014 syntax failed'}
}
Write-Host 'PASS SQL037 and isolated quote distribution syntax/isolation (no database execution)'
# Offline only; no connection, SQL deployment, or runtime replacement.
