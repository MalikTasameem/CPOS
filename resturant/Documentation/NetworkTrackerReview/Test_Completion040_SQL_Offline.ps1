param([Parameter(Mandatory=$true)][string]$CandidateDirectory)
$ErrorActionPreference = 'Stop'
# Text/AST checks only: no SQL connection, deployment, or application startup.
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$domPath = 'C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
Add-Type -Path $domPath
function Get-AuditDeclaredVariables($node) {
 if($node.GetType().Name -in @('DeclareVariableElement','ProcedureParameter')) { $node.VariableName.Value }
 if($node.GetType().Name -eq 'DeclareTableVariableStatement') { $node.Body.VariableName.Value }
 foreach($property in $node.GetType().GetProperties()) {
  if($property.Name -eq 'ScriptTokenStream' -or $property.GetIndexParameters().Count){continue}
  $value=$property.GetValue($node,$null)
  if($value -is [Microsoft.SqlServer.TransactSql.ScriptDom.TSqlFragment]) { Get-AuditDeclaredVariables $value }
  elseif($value -is [Collections.IEnumerable] -and $value -isnot [string]) {
   foreach($child in $value) { if($child -is [Microsoft.SqlServer.TransactSql.ScriptDom.TSqlFragment]) { Get-AuditDeclaredVariables $child } }
  }
 }
}
$parser = [Microsoft.SqlServer.TransactSql.ScriptDom.TSql120Parser]::new($true)
$files = @(Get-ChildItem (Join-Path $projectRoot 'Database\Audit') -Filter '*.sql' | Where-Object { $_.Name -match '^(007_|03[1-9]_|040_)' })
$candidates = @(Get-ChildItem -LiteralPath $CandidateDirectory -Filter '*_040_Candidate.sql')
if($candidates.Count -lt 6){throw 'Expected the merged writer, request coordinator, user-delete and isolated procedures.'}
$parsed = 0
foreach($file in @($files)+@($candidates)) {
 $errors = $null
 $text = [IO.File]::ReadAllText($file.FullName)
 $ast = $parser.Parse([IO.StringReader]::new($text),[ref]$errors)
 if($errors.Count){throw ($file.Name+': '+(($errors | ForEach-Object { 'Line '+$_.Line+': '+$_.Message }) -join '; '))}
 foreach($batch in $ast.Batches) {
  $duplicates = @(Get-AuditDeclaredVariables $batch | Group-Object | Where-Object Count -gt 1)
  if($duplicates.Count){throw ($file.Name+': duplicate variables '+(($duplicates.Name)-join ', '))}
 }
 $parsed++
}
foreach($name in 'Set_Audit_Pilot.ps1','Test_ItemLifecycle.ps1','Test_InventoryOperations.ps1','Test_DocumentWorkflows.ps1','Test_TableSplit.ps1') {
 $tokens=$null; $errors=$null
 [void][Management.Automation.Language.Parser]::ParseFile((Join-Path $PSScriptRoot $name),[ref]$tokens,[ref]$errors)
 if($errors.Count){throw ($name+': '+($errors.Message -join '; '))}
}
$writer = [IO.File]::ReadAllText((Join-Path $CandidateDirectory 'Network_Audit_Event_Write_040_Candidate.sql'))
$begin = [IO.File]::ReadAllText((Join-Path $CandidateDirectory 'Network_Audit_Request_Begin_040_Candidate.sql'))
foreach($operation in @('Agent.Created','Agent.Updated','Agent.Deleted','User.Created','User.Updated','User.Deleted','User.CredentialsChanged',
 'Document.EditFieldsSaved','Document.ProjectChanged','Purchase.AccountCurrencyChanged','Purchase.CurrencyChanged','Quote.Confirmed','Quote.DiscountChanged','Quote.DiscountDistributed','Document.MetadataSaved','Quote.Reserved','Quote.EditFieldsSaved',
 'Item.PlaceholderAcquired','Item.FullSaved','Item.QuickSaved','Item.DisabledRowsRemoved','Salary.BatchPosted',
 'TableSplit.Started','TableSplit.UnitMoved','TableSplit.UnitReturned','TableSplit.Confirmed')) {
 if(-not $writer.Contains($operation) -or -not $begin.Contains($operation)){throw ('Missing final operation '+$operation)}
}
$quote = [IO.File]::ReadAllText((Join-Path $CandidateDirectory 'ViewBill_Discount_Distribute_Audited_040_Candidate.sql'))
if($quote -match '(?i)\b(SB_Contents|SB_IM_Components)\b'){throw 'Quote distribution must not target sale lines/components.'}
if($writer -notmatch 'ItemStockRow after-state differs' -or $writer -match 'p\.\[AuditStock\]'){throw 'Missing/incorrect actual stock-row validation.'}
Write-Output ('PASS '+$parsed+' SQL Server 2014 files; unique variables per batch, five PowerShell parsers, final catalogues and quote target isolation. No database access.')
