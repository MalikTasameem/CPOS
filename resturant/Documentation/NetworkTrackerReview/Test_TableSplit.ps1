param([switch]$Catalog)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditTableSplit040'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSalesEdit.vb',
 'Classes\Audit\AuditSalesLineEdit.vb','Classes\Audit\AuditSalesQuantity.vb','Classes\Audit\AuditFinance.vb',
 'Classes\Audit\AuditTables.vb','Classes\Audit\AuditTableSplit.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources+=Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | Select-Object -ExpandProperty FullName
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditTableSplitTests.vb'
$exe=Join-Path $output 'AuditTableSplitTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditTableSplitTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Table split tests did not compile'}
if($Catalog){& $exe catalog}else{& $exe}
if($LASTEXITCODE -ne 0){throw 'Table split tests failed'}
if(-not $Catalog){
 $sql=Get-Content -LiteralPath (Join-Path $projectRoot 'Database\Audit\040_Table_Split.sql') -Raw
 $match=[regex]::Match($sql,"N'(-- AuditTableSplit040:(?:''|[^'])*)'\+@old")
 if(-not $match.Success){throw 'Table split writer body not found'}
 $body=$match.Groups[1].Value.Replace("''", "'")
 foreach($entity in @('SplitTable','SplitHeader','SplitLine','SplitPeriod','SplitBalance')){
  if(-not $body.Contains("$entity after-state does not match storage.")){throw "Actual after-state check missing: $entity"}
 }
 foreach($guard in @('DATALENGTH','TRY_CONVERT(datetime2(7),c.AfterValue,126)','FROM dbo.ST_Balance_V','one actual queue row','People.Salaries','Salary.BatchPosted')){
  if(-not $sql.Contains($guard)){throw "SQL040 required guard missing: $guard"}
 }
 Add-Type -Path 'C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
 foreach($text in @($sql,('CREATE PROCEDURE dbo.SyntaxOnly AS BEGIN '+$body+' END;'))){
  $parser=[Microsoft.SqlServer.TransactSql.ScriptDom.TSql120Parser]::new($true)
  $reader=[IO.StringReader]::new($text)
  $errors=$null
  $null=$parser.Parse($reader,[ref]$errors)
  $reader.Dispose()
  if($errors.Count){$errors|ForEach-Object {Write-Host $_.Message};throw 'SQL040 SQL Server 2014 syntax failed'}
 }
 Write-Host 'PASS SQL040 outer script and inner writer body syntax (no database execution)'
}
# Offline only: no SQL connection, fixture, deployment, or runtime replacement.
