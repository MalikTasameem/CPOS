$ErrorActionPreference='Stop'
$root=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$receipt=Get-Content -LiteralPath (Join-Path $root 'resturant\Forms\Receipt.vb') -Raw
$sales=Get-Content -LiteralPath (Join-Path $root 'resturant\Forms\POS\Sales_Fast.vb') -Raw
$script:checks=0
function Check([bool]$condition,[string]$label){if(-not $condition){throw "FAIL $label"};$script:checks++}
function Method([string]$source,[string]$name){
 $match=[regex]::Match($source,'(?ms)^    (?:Public|Private|Friend)(?: Shared)? (?<kind>Sub|Function) '+[regex]::Escape($name)+'\b.*?^    End \k<kind>')
 if(-not $match.Success){throw "Method not found: $name"};return $match.Value
}
foreach($name in @('Receipt_Insert','Receipt_UPDATE','OpenReceiptForEdit','Agents_Balance_MV_RCT_VOID')){
 $body=Method $receipt $name
 Check ($body.Contains('AuditReceiptUi.Run(Me, command, savedId, savedNumber)')) "$name uses V2"
 Check ($body.Contains('ReceiptContextType() = 12')) "$name explicit deferred custody branch"
 Check ($body.Contains('TryBeginReceiptOperation()') -and $body.Contains('EndReceiptOperation()')) "$name reentry guard and finally release"
 Check (-not [regex]::IsMatch($body,'SQL_SP_EXEC|ExecuteNonQuery|Network_Edit_Tracker_insert')) "$name no ordinary legacy execution"
 Check ($body.IndexOf('RecordAuditedReceiptSuccess') -gt $body.IndexOf('AuditReceiptUi.Run')) "$name result recorded only after confirmed Run"
}
foreach($name in @('LegacyCustodyReceiptInsert','LegacyCustodyReceiptUpdate','LegacyCustodyOpenReceiptForEdit','LegacyCustodyReceiptVoid','LegacyCustodyExecuteStoredProcedure')){
 $body=Method $receipt $name
 Check ($body.StartsWith('    Private') -and $body.Contains('If ReceiptContextType() <> 12 Then Throw')) "$name is private custody-only"
}
$record=Method $receipt 'RecordAuditedReceiptSuccess'
Check ($record.IndexOf('Reciept_T_ID = savedId') -lt $record.IndexOf('Is_ComandSuccess = True')) 'receipt id recorded before successful flag'
Check ($record.Contains('receiptResultNeedsReload = savedId <= 0 OrElse savedNumber <= 0') -and $record.Contains('Me.new_butt.Enabled = False') -and $record.Contains('Me.save_butt.Enabled = False')) 'unknown committed result locks create and save'
Check ((Method $receipt 'TryBeginReceiptOperation').Contains('receiptResultNeedsReload')) 'unknown committed result blocks subsequent writes'
Check ((Method $receipt 'Make_New_Receipt').Contains('receiptResultNeedsReload')) 'unknown committed result cannot reset to new receipt'
Check ((Method $receipt 'Preper_To_Insert').Contains('Decimal.TryParse') -and (Method $receipt 'Preper_To_Insert').Contains('loadedReceiptType <> kind')) 'decimal validation and immutable transaction type'
Check ((Method $receipt 'TrySelectedReceiptId').Contains('DataRowView') -and (Method $receipt 'TrySelectedReceiptId').Contains('IsDBNull')) 'selection guards cover null and binding rows'
Check ((Method $receipt 'BuildReceiptSaveCommand').Contains('If(updating, loadedReceiptType, kind)')) 'update uses loaded receipt type'
Check (-not (Method $receipt 'Receipt_UPDATE').Contains('ExecuteReceiptStoredProcedure')) 'ordinary update does not request repost'
Check (-not [regex]::IsMatch((Method $receipt 'Agents_Balance_MV_RCT_VOID'),'F_Receipt\.')) 'ordinary cancel updates this instance'
Check (-not [regex]::IsMatch($receipt,'(?m)^\s*(?:Else)?If FormType\b')) 'screen no longer branches on mutable global form type'
$delivery=Method $sales 'DeliveryingButton_Click'
Check ($delivery.IndexOf('Receipt_Tran_ID = T_ID') -gt $delivery.IndexOf('.ClearFields()')) 'Sales_Fast linked bill set after ClearFields'
Check ($delivery.IndexOf('.ConfigureEntryContext(1, T_ID)') -gt $delivery.IndexOf('.ClearFields()')) 'Sales_Fast instance context set after ClearFields'
$context=Method $receipt 'ReceiptContextType'
Check (-not [regex]::IsMatch($context,'(?m)^\s*[^\x27\r\n]*=\s*FormType\b|receiptLinkedTransactionId\s*=\s*Receipt_Tran_ID')) 'stale global FormType12 cannot select legacy custody'
Check ($context.Contains('receiptEntryFormType = 12 AndAlso Not receiptDeferredCustodyMode')) 'custody context requires explicit marker'
$ordinary=Method $receipt 'ConfigureEntryContext'
Check ($ordinary.Contains('kind <> 0 AndAlso kind <> 1 AndAlso kind <> 2 AndAlso kind <> 8') -and $ordinary.Contains('receiptDeferredCustodyMode = False')) 'ordinary entry excludes type12 and clears custody marker'
Check ($ordinary.Contains('If receiptContextCaptured Then Throw') -and $ordinary.Contains('receiptLinkedTransactionId = linkedId')) 'entry context immutable after opening and link explicit'
$deferred=Method $receipt 'ConfigureDeferredCustodyReceipt'
Check ($deferred.Contains('owner As Custody') -and $deferred.Contains('Object.ReferenceEquals(form, owner)') -and $deferred.Contains('receiptDeferredCustodyMode = True')) 'deferred custody requires exact open custody instance'
Check (-not (Method $receipt 'Receipt_Load').Contains('receiptLinkedTransactionId = Receipt_Tran_ID')) 'Load cannot overwrite instance link from global state'
Check (-not [regex]::IsMatch($receipt,'(?m)^\s*receiptEntryFormType\s*=\s*FormType\b')) 'no global context capture anywhere'
$callers=@('MainForm.vb','Balances.vb','OPEN_Bills.vb','POS\Sales.vb','Pch\Pch.vb','Exp\EXP_Details.vb','POS\Sales_Fast.vb','POS\OrdersMenu.vb','Custody.vb')
$callCount=0
foreach($path in $callers){
 $caller=Get-Content -LiteralPath (Join-Path $root ('resturant\Forms\'+$path)) -Raw
 foreach($match in [regex]::Matches($caller,'(?m)^\s*F_Receipt = New Receipt\s*$')){
  $tail=$caller.Substring($match.Index)
  $show=[regex]::Match($tail,'F_Receipt\.Show(?:Dialog)?\(')
  Check ($show.Success) "$path receipt entry reaches Show"
  $setup=$tail.Substring(0,$show.Index)
  if($path -eq 'Custody.vb'){
   Check ($setup.Contains('.ConfigureDeferredCustodyReceipt(Me)') -and $setup.IndexOf('.ConfigureDeferredCustodyReceipt(Me)') -gt $setup.IndexOf('.ClearFields()')) 'Custody explicit setup after ClearFields only'
  }else{
   Check ($setup.Contains('ConfigureEntryContext(') -and -not $setup.Contains('ConfigureDeferredCustodyReceipt')) "$path ordinary entry explicit and not custody"
  }
  $callCount++
 }
}
Check ($callCount -eq 16) 'all16 live receipt instantiation sites checked'
$load=Method $receipt 'Select_Receipt'
$lock=Method $receipt 'LockReceiptUntilReload'
Check ($lock.Contains('receiptResultNeedsReload = True') -and $lock.Contains('Reciept_T_ID = 0') -and $lock.Contains('On_Update = False')) 'load lock invalidates actionable receipt identity'
foreach($button in @('save_butt','Edit_butt','DeleteButton','print_butt','new_butt','Fields_Panel')){
 Check ($lock.Contains($button+'.Enabled = False')) "load lock disables $button"
}
Check ($load.IndexOf('LockReceiptUntilReload()') -lt $load.IndexOf('connection.Open()')) 'load lock precedes connection attempt'
Check ($load.Contains('If table.Rows.Count = 0 Then Throw') -and $load.Contains('Catch ex As Exception'+"`n"+'            LockReceiptUntilReload()')) 'missing or failed row stays locked'
Check ($load.IndexOf('Dim amount = Convert.ToDecimal(row("Value"))') -lt $load.IndexOf('ReceiptNum_Txt.Text =')) 'nullable amount validated before controls are changed'
Check ($load.IndexOf('Reciept_T_ID = T_ID') -gt $load.IndexOf('TrySelectedReceiptId(Treasury_ComboBox')) 'identity published only after displayed selections validate'
Check ([regex]::Matches($load,'receiptResultNeedsReload = False').Count -eq 1 -and $load.IndexOf('receiptResultNeedsReload = False') -gt $load.IndexOf('DeleteButton.Enabled = Not isVoid')) 'reload lock cleared only at final successful statement'
$lookup=Method $receipt 'Get_T_ID'
Check ($lookup.Contains('new_butt.Enabled = Not receiptResultNeedsReload')) 'lookup cannot unlock failed receipt load'
Check ($lookup.IndexOf('LockReceiptUntilReload()') -lt $lookup.IndexOf('C.Con.Open()')) 'receipt-number lookup failure is also locked'
$original=(& git -c "safe.directory=$($root.Replace('\','/'))" show 'HEAD:resturant/Forms/Receipt.vb') -join "`n"
if($LASTEXITCODE -ne 0){throw 'Could not read baseline Receipt.vb'}
foreach($name in @('Custody_SELECT','Custody_INSERT')){
 Check (((Method $receipt $name) -replace "`r",'') -ceq ((Method $original $name) -replace "`r",'')) "$name unchanged"
}
"PASS $script:checks Receipt UI source invariants; no database access or forms opened."
