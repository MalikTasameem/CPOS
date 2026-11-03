$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$checks=0
function Check([bool]$condition,[string]$label){
 if(-not $condition){throw ('FAILED '+$label)}
 $script:checks++
 Write-Output ('PASS '+$label)
}
function Source([string]$relative){
 # Full-line comments are ignored; this is a static regression guard, not a UI test.
 return (([IO.File]::ReadAllLines((Join-Path $projectRoot $relative)) | Where-Object {$_ -notmatch "^\s*'"}) -join "`n")
}
$header=Source 'Forms\Stores\ST_settlement.vb'
$line=Source 'Forms\Stores\IM_Update_Qty.vb'
foreach($entry in @(@('header',$header),@('line',$line))){
 Check ($entry[1] -notmatch 'Network_Edit_Tracker_insert') ($entry[0]+' no legacy writer')
 Check ($entry[1] -notmatch '\b(Save_Total|Save_Date|Save_Title_Name|Save_About|DependingBill|Open_Agents_Balance_MV_For_Edit)\s*\(') ($entry[0]+' no direct header writes')
}
Check ($header -match 'Private Sub TryAuditHeader\(action As String\)') 'single V2 header handler'
Check ($header -notmatch 'If TryAuditHeader|Insert_NewBill|Private Sub Update_Total') 'no fallback header branches'
foreach($action in @('Created','Approved','Voided','HeaderSaved','EditOpened','EditFinished')){
 Check ($header.Contains('TryAuditHeader("'+$action+'")')) ('header route '+$action)
}
Check (([regex]::Matches($header,'If Not AuditSettlementLines.IsEnabled\(MY_Settings.SqlConStr\) Then Throw')).Count -eq 3) 'header delete and editor fail closed'
Check ($header -match 'AuditSettlementHeader.Save\(pendingHeaderConnection, pendingHeaderAudit\)') 'header saves immutable request'
Check ($header -match 'AuditSettlementLines.Save\(pendingDeleteConnection, pendingDeleteAudit\)') 'delete saves immutable request'
Check ($line -notmatch 'SQL_SP_EXEC|ST_Balance_Update\(|auditMode') 'line has no direct-write branch'
Check ($line -match 'If pendingAudit Is Nothing AndAlso Not AuditSettlementLines.IsEnabled') 'new line attempts check gate'
Check ($line -match 'AuditSettlementLines.Save\(pendingConnection, pendingAudit\)') 'line saves immutable request'
Check ($line -match 'pendingAudit.RequestId.ToString\("D"\)') 'unknown result retains request reference'
Write-Output ("PASS "+$checks+" settlement V2-only static checks.")
