$ErrorActionPreference = 'Stop'
# Source regression checks only: no application startup or database connection.
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$checks = 0
function Check([bool]$condition, [string]$label) {
 if(-not $condition){throw ('FAILED '+$label)}
 $script:checks++
}
function Source([string]$path) { [IO.File]::ReadAllText((Join-Path $projectRoot $path)) }
function Method([string]$source, [string]$name) {
 $pattern = '(?ms)^\s*(?:Public|Private|Friend)\s+(?:Shared\s+)?(?:Sub|Function)\s+'+[regex]::Escape($name)+'\([^\r\n]*\).*?^\s*End (?:Sub|Function)\s*$'
 $match = [regex]::Match($source,$pattern)
 if(-not $match.Success){throw ('Method not found: '+$name)}
 $match.Value
}
$form = Source 'Forms\Receipt.vb'
$ui = Source 'Classes\Audit\AuditReceiptUi.vb'
foreach($name in @('Receipt_Insert','Receipt_UPDATE','OpenReceiptForEdit','Agents_Balance_MV_RCT_VOID')) {
 $body = Method $form $name
 Check ($body.Contains('AuditReceiptUi.Run(Me, command, savedId, savedNumber)')) ($name+' V2 route')
 Check ($body -notmatch 'Network_Edit_Tracker_insert|SQL_SP_EXEC|ExecuteNonQuery|BeginTransaction') ($name+' no direct ordinary write')
 Check ($body.Contains('TryBeginReceiptOperation()') -and $body.Contains('EndReceiptOperation()')) ($name+' reentry protection')
 Check ($body.Contains('ReceiptContextType() = 12')) ($name+' explicit deferred custody branch')
}
foreach($name in @('LegacyCustodyReceiptInsert','LegacyCustodyReceiptUpdate','LegacyCustodyReceiptVoid','LegacyCustodyOpenReceiptForEdit')) {
 Check ((Method $form $name).Contains('If ReceiptContextType() <> 12 Then Throw')) ($name+' cannot serve ordinary receipts')
}
$capture = Method $form 'ReceiptContextType'
Check ($capture.Contains('receiptContextCaptured = True') -and $capture.Contains('Not receiptDeferredCustodyMode')) 'entry context frozen and deferred marker guarded'
Check ($form -notmatch 'receiptEntryFormType\s*=\s*FormType') 'stale global FormType cannot select custody'
Check ((Method $form 'Load_Data').Contains('If ReceiptContextType() = 1 AndAlso Application.OpenForms().OfType(Of Sales).Any Then')) 'other entry sources cannot inherit Sales receipt preview'
$ordinaryContext = Method $form 'ConfigureEntryContext'
Check ($ordinaryContext.Contains('kind <> 0 AndAlso kind <> 1 AndAlso kind <> 2 AndAlso kind <> 8')) 'only ordinary entry kinds accepted'
Check ($ordinaryContext.Contains('If receiptContextCaptured Then Throw') -and $ordinaryContext.Contains('receiptDeferredCustodyMode = False')) 'ordinary context immutable after load'
$custodyContext = Method $form 'ConfigureDeferredCustodyReceipt'
Check ($custodyContext.Contains('Object.ReferenceEquals(form, owner)') -and $custodyContext.Contains('receiptDeferredCustodyMode = True')) 'deferred custody requires actual open owner'
foreach($path in @('Forms\MainForm.vb','Forms\Balances.vb','Forms\OPEN_Bills.vb','Forms\POS\Sales.vb','Forms\POS\Sales_Fast.vb','Forms\Pch\Pch.vb','Forms\Exp\EXP_Details.vb','Forms\POS\OrdersMenu.vb','Forms\Custody.vb')) {
 $caller = Source $path
 $creationCount = [regex]::Matches($caller,'(?i)New\s+Receipt\b').Count
 $contextCount = [regex]::Matches($caller,'\.Configure(?:EntryContext|DeferredCustodyReceipt)\(').Count
 Check ($creationCount -gt 0 -and $creationCount -eq $contextCount) ($path+' explicit context at every Receipt entry')
 if($path -ne 'Forms\Custody.vb') {Check (-not $caller.Contains('.ConfigureDeferredCustodyReceipt(')) ($path+' cannot infer deferred custody')}
}
$build = Method $form 'BuildReceiptSaveCommand'
Check ($build.Contains('receiptLinkedTransactionId') -and $build.Contains('loadedReceiptType')) 'stable link and edit kind'
Check ($build.Contains('Decimal.TryParse') -and $build -notmatch 'Convert.ToDouble|As Double') 'decimal amount parsing'
$success = Method $form 'RecordAuditedReceiptSuccess'
Check ($success.IndexOf('Is_ComandSuccess = True') -lt $success.IndexOf('If receiptResultNeedsReload Then')) 'commit recorded before UI refresh'
foreach($control in @('save_butt','new_butt','Edit_butt','DeleteButton')) {
 Check ($success.Contains('Me.'+$control+'.Enabled = False')) ('unknown result blocks '+$control)
}
Check ($ui.Contains('submitted = True') -and $ui.Contains('If(Not submitted, "NotFound"')) 'prepare failure cannot retry an unprepared request'
Check ($ui.Contains('current.SourceId <> r.Context.SourceId') -and $ui.Contains('current.SessionId <> r.Context.SessionId') -and $ui.Contains('current.ActorUserId <> r.Context.ActorUserId')) 'retry context guarded'
Check ($ui.Contains('AuditQuantityRecoveryDialog(connection, request, ex.Message, save)')) 'same request recovery'
Check ($ui.Contains('DocumentNumber') -and $ui.Contains('EntityId')) 'result identity recovered'
Check ((Method $form 'FinishAuditedReceiptSave').Contains('AuditReceiptUi.Refresh(Me')) 'refresh errors separated from commit'
Check ((Method $form 'Receipt_FormClosing').Contains('If receiptOperationInProgress Then e.Cancel = True')) 'cannot close unresolved save'
$load = Method $form 'Select_Receipt'
$lock = Method $form 'LockReceiptUntilReload'
Check ($load.IndexOf('LockReceiptUntilReload()') -lt $load.IndexOf('connection.Open()')) 'lock before loading receipt'
Check ($load.Contains('table.Load(reader)') -and $load.Contains('If table.Rows.Count = 0 Then Throw')) 'stage receipt row and reject missing row'
Check ($load.IndexOf('Reciept_T_ID = T_ID') -gt $load.IndexOf('TrySelectedReceiptId(Treasury_ComboBox')) 'publish identity after controls verified'
Check ($load.IndexOf('receiptResultNeedsReload = False') -gt $load.IndexOf('DeleteButton.Enabled = Not isVoid')) 'unlock only after display complete'
Check ($load -match '(?s)Catch ex As Exception\s+LockReceiptUntilReload\(\)') 'failed load stays locked'
foreach($control in @('save_butt','new_butt','Edit_butt','DeleteButton','print_butt','Fields_Panel')) {
 Check ($lock.Contains($control+'.Enabled = False')) ('partial load blocks '+$control)
}
Check ((Method $form 'Get_T_ID').Contains('new_butt.Enabled = Not receiptResultNeedsReload')) 'search cannot unlock failed load'
$fast = Source 'Forms\POS\Sales_Fast.vb'
Check ($fast -match '(?s)\.ClearFields\(\)\s*\r?\n\s*Receipt_Tran_ID = T_ID') 'Sales_Fast link assigned after reset'
$gate = Source 'Database\Audit\007_Audit_Pilot_Gate.sql'
$domains = @([regex]::Matches($gate,'insert <Domain code="([^"]+)"') | ForEach-Object {$_.Groups[1].Value} | Select-Object -Unique)
foreach($domain in $domains) {
 Check ($gate.Contains('delete /Domains/Domain[@code="'+$domain+'"]')) ('gate can disable '+$domain)
}
Write-Output ('PASS '+$checks+' ordinary Receipt UI and pilot-gate source checks. No database access.')
