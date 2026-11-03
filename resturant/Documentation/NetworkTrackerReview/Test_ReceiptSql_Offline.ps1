param([string]$CandidateDirectory)
$ErrorActionPreference='Stop'
# Text/AST checks only. Never opens SQL connections or executes SQL batches.
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
if(-not $CandidateDirectory){$CandidateDirectory=Join-Path $projectRoot '..\.tmp\AuditReceipt041_20261027'}
$baseline=Join-Path $projectRoot '..\.tmp\AuditCompletion038_20261027'
$sql=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\041_Receipts.sql'))
$permissionFix=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\042_Receipt_Admin_Permissions.sql'))
function Decode([string]$value){$value.Replace("''", "'")}
function Normalized([string]$value){$value.Replace("`r`n","`n").TrimEnd()}
$olds=@([regex]::Matches($sql,"SET @old=N'((?:''|[^'])*)';") | ForEach-Object {Decode $_.Groups[1].Value})
$appends=@([regex]::Matches($sql,"@old\+N'((?:''|[^'])*)'\)") | ForEach-Object {Decode $_.Groups[1].Value})
$prefixes=@([regex]::Matches($sql,"REPLACE\(@sql,@old,N'((?:''|[^'])*)'\+@old\)") | ForEach-Object {Decode $_.Groups[1].Value})
if($olds.Count -ne 4 -or $appends.Count -ne 2 -or $prefixes.Count -ne 2){throw 'Unexpected SQL041 transformation structure'}
$begin=[IO.File]::ReadAllText((Join-Path $baseline 'Network_Audit_Request_Begin_040_Candidate.sql'))
$writer=[IO.File]::ReadAllText((Join-Path $baseline 'Network_Audit_Event_Write_040_Candidate.sql'))
$begin=$begin.Replace('CREATE PROCEDURE','ALTER PROCEDURE').Replace($olds[0],$olds[0]+$appends[0])
$writer=$writer.Replace('CREATE PROCEDURE','ALTER PROCEDURE').Replace($olds[1],$olds[1]+$appends[1]).Replace($olds[2],$prefixes[0]+$olds[2]).Replace($olds[3],$prefixes[1]+$olds[3])
foreach($pair in @(@('Network_Audit_Request_Begin',$begin),@('Network_Audit_Event_Write',$writer))){
 $actual=[IO.File]::ReadAllText((Join-Path $CandidateDirectory ($pair[0]+'_041_Candidate.sql')))
 if((Normalized $actual) -cne (Normalized $pair[1])){throw ('Candidate does not match migration transformations: '+$pair[0])}
}
$insertMatch=[regex]::Match($sql,"EXEC sys\.sp_executesql N'((?:''|[^'])*)';")
if(-not $insertMatch.Success){throw 'Isolated insert not found'}
$insert=Decode $insertMatch.Groups[1].Value
$active=($insert -split '\r?\n' | Where-Object {$_ -notmatch '^\s*--'}) -join "`n"
if($active -match '(?i)\b(?:UPDATE|FROM)\s+(?:dbo\.)?Agents_Balance_MV\b'){throw 'Isolated receipt insert still targets invoice currency fields'}
if(([regex]::Matches($active,'(?i)UPDATE\s+Agents_Balance_MV_RCT\s+SET (Debit_2|Credit_2)')).Count -ne 2){throw 'Currency targets incomplete'}
$hasher=[Security.Cryptography.SHA256]::Create()
try{$hash=([BitConverter]::ToString($hasher.ComputeHash([Text.Encoding]::Unicode.GetBytes($insert.Replace("`r`n","`n"))))).Replace('-','')}finally{$hasher.Dispose()}
$service=[IO.File]::ReadAllText((Join-Path $projectRoot 'Classes\Audit\AuditReceipt.vb'))
if(-not $service.Contains($hash) -or $service.Contains('PENDING_SQL041_HASH')){throw 'Service isolated-procedure fingerprint mismatch'}
foreach($guard in @('Update_Receipt','Cancel_Receipt','v.Exp=1','v.SB_Rtn=1','v.Pch_Rtn=1','ISNULL(Custody_ID,0)=0','C_Rcpt_T_ID=@rcNumber','ReceiptHeader after-state','ReceiptTreasury after-state','DATALENGTH(c.AfterValue)<>128','varbinary(8000)','TRY_CONVERT(datetime2(7),c.AfterValue,126)')){
 if(-not $sql.Contains($guard)){throw ('Missing receipt SQL guard: '+$guard)}
}
foreach($guard in @('AuditReceipts042','LEFT JOIN dbo.Users_Validations_V','u.isAdmin=1 OR','Receipt permission denied.')){
 if(-not $permissionFix.Contains($guard)){throw ('Missing receipt permission migration guard: '+$guard)}
}
Add-Type -Path 'C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
function Variables($node){
 if($node.GetType().Name -in @('DeclareVariableElement','ProcedureParameter')){$node.VariableName.Value}
 if($node.GetType().Name -eq 'DeclareTableVariableStatement'){$node.Body.VariableName.Value}
 foreach($property in $node.GetType().GetProperties()){
  if($property.Name -eq 'ScriptTokenStream' -or $property.GetIndexParameters().Count){continue}
  $value=$property.GetValue($node,$null)
  if($value -is [Microsoft.SqlServer.TransactSql.ScriptDom.TSqlFragment]){Variables $value}
  elseif($value -is [Collections.IEnumerable] -and $value -isnot [string]){
   foreach($child in $value){if($child -is [Microsoft.SqlServer.TransactSql.ScriptDom.TSqlFragment]){Variables $child}}
  }
 }
}
$parser=[Microsoft.SqlServer.TransactSql.ScriptDom.TSql120Parser]::new($true)
foreach($text in @($sql,$permissionFix,$begin,$writer,$insert,[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\007_Audit_Pilot_Gate.sql')))){
 $errors=$null
 $ast=$parser.Parse([IO.StringReader]::new($text),[ref]$errors)
 if($errors.Count){throw (($errors | ForEach-Object {'Line '+$_.Line+': '+$_.Message}) -join '; ')}
 foreach($batch in $ast.Batches){
  $duplicates=@(Variables $batch | Group-Object | Where-Object Count -gt 1)
  if($duplicates.Count){throw ('Duplicate SQL variable: '+($duplicates.Name -join ', '))}
 }
}
foreach($file in @('Test_Receipts.ps1','Test_ReceiptService.ps1','Test_ReceiptUi_Offline.ps1','Test_ReceiptRegression_Offline.ps1','Test_ReceiptSql_Offline.ps1','Set_Audit_Pilot.ps1')){
 $tokens=$null; $errors=$null
 [void][Management.Automation.Language.Parser]::ParseFile((Join-Path $PSScriptRoot $file),[ref]$tokens,[ref]$errors)
 if($errors.Count){throw ('Invalid PowerShell '+$file+': '+($errors.Message -join '; '))}
}
Write-Output 'PASS SQL041 and pilot syntax, complete merged procedures, unique variables, exact text transformations, isolated currency target and fingerprint, and six PowerShell scripts. No database access.'
