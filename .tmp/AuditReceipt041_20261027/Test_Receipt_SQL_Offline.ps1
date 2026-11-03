$ErrorActionPreference='Stop'
# Offline source/AST checks only. No SQL connection, deployment, or application startup.
$repo=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$project=Join-Path $repo 'resturant'
$script=[IO.File]::ReadAllText((Join-Path $project 'Database\Audit\041_Receipts.sql'))
$oldRoot=Join-Path $repo '.tmp\AuditCompletion038_20261027'
$ops="'Receipt.Created','Receipt.EditOpened','Receipt.Updated','Receipt.Voided'"
$begin=[IO.File]::ReadAllText((Join-Path $oldRoot 'Network_Audit_Request_Begin_040_Candidate.sql'))
$writer=[IO.File]::ReadAllText((Join-Path $oldRoot 'Network_Audit_Event_Write_040_Candidate.sql'))
$ba="(@OperationCode='AuditTest.Changed' AND @DomainCode='AuditTest')"
$wa="(@operation='AuditTest.Changed' AND @domain='AuditTest' AND @kind=1 AND @actor IN(1,3))"
$fa="(@operation='AuditTest.Changed' AND ((FieldCode"
$ia='INSERT dbo.Network_Audit_Event(EventId'
foreach($pair in @(@($begin,$ba),@($writer,$wa),@($writer,$fa),@($writer,$ia))) {
 if(([regex]::Matches($pair[0],[regex]::Escape($pair[1]))).Count -ne 1){throw 'Nonunique 040 transformation anchor'}
}
$bodyMatch=[regex]::Match($script,"(?s)SET @sql=REPLACE\(@sql,@old,N'(-- AuditReceipts041:(?:[^']|'')*)'\+@old\);")
if(-not $bodyMatch.Success){throw 'Receipt body not found'}
$body=$bodyMatch.Groups[1].Value.Replace("''","'")
$begin=$begin.Replace('CREATE PROCEDURE','ALTER PROCEDURE').Replace($ba,$ba+" OR (@OperationCode IN("+$ops+") AND @DomainCode='Finance.Receipts')")
$writer=$writer.Replace('CREATE PROCEDURE','ALTER PROCEDURE').Replace($wa,$wa+" OR (@operation IN("+$ops+") AND @domain='Finance.Receipts' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)")
$writer=$writer.Replace($fa,"(@operation IN("+$ops+")) OR "+$fa).Replace($ia,$body+$ia)
foreach($pair in @(@('Network_Audit_Request_Begin',$begin),@('Network_Audit_Event_Write',$writer))) {
 $actual=[IO.File]::ReadAllText((Join-Path $PSScriptRoot ($pair[0]+'_041_Candidate.sql')))
 if($actual.Replace("`r`n","`n").TrimEnd() -cne $pair[1].Replace("`r`n","`n").TrimEnd()){throw ('Candidate differs from migration REPLACE: '+$pair[0])}
}
$clone=[regex]::Match($script,"(?s)EXEC sys\.sp_executesql N'((?:[^']|'')*)';").Groups[1].Value.Replace("''","'")
$hash=[Security.Cryptography.SHA256]::Create()
try{$actualHash=([BitConverter]::ToString($hash.ComputeHash([Text.Encoding]::Unicode.GetBytes($clone)))).Replace('-','')}finally{$hash.Dispose()}
if($actualHash -cne '8A15D516C4ACEFB075A3EC6F62748FFE53D77FC0119CEF67ED3B4E37E957509B'){throw 'Isolated insert hash changed'}
# No executable statement in the isolated insert can write the invoice header table.
$withoutComments=[regex]::Replace($clone,'(?m)--[^\r\n]*','')
if($withoutComments -match '(?i)\b(?:UPDATE|INSERT\s+(?:INTO\s+)?)\s+(?:dbo\.)?Agents_Balance_MV\b'){throw 'Insert writes invoice table'}
Add-Type -Path 'C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
$helper=[IO.File]::ReadAllText((Join-Path $project 'Documentation\NetworkTrackerReview\Test_Completion040_SQL_Offline.ps1'))
$start=$helper.IndexOf('function Get-AuditDeclaredVariables')
$end=$helper.IndexOf('$parser =',$start)
Invoke-Expression $helper.Substring($start,$end-$start)
$parser=[Microsoft.SqlServer.TransactSql.ScriptDom.TSql120Parser]::new($true)
$files=@(Get-Item (Join-Path $project 'Database\Audit\041_Receipts.sql'))+@(Get-ChildItem -LiteralPath $PSScriptRoot -Filter '*_041_Candidate.sql')
foreach($file in $files) {
 $errors=$null
 $ast=$parser.Parse([IO.StringReader]::new([IO.File]::ReadAllText($file.FullName)),[ref]$errors)
 if($errors.Count){throw ($file.Name+': '+($errors.Message -join '; '))}
 foreach($batch in $ast.Batches) {
  $duplicates=@(Get-AuditDeclaredVariables $batch | Group-Object | Where-Object Count -gt 1)
  if($duplicates.Count){throw ($file.Name+': duplicate variables '+($duplicates.Name -join ', '))}
 }
}
Write-Output 'PASS 4 SQL2014 ASTs; unique variables; exact 040-to-041 transforms; isolated insert hash and invoice-write exclusion. No database access.'

