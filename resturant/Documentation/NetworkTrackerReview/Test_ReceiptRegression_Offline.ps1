$ErrorActionPreference='Stop'
# Explicit offline allowlist. Never forward live/install/rollback switches.
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditReceipt041_20261027'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$suites=@('Phase2A','Phase2B','Phase3A','Phase3B','Phase3C','Phase3D','Phase4A','Phase4B','Phase4C','Phase4D','Phase4E','Phase4F',
 'AdministrativeRestore','DocumentCancellation','Units','Groups','ItemUnits','PricingRules','ItemMaster','Tables','Finance',
 'StockDocuments','EmployeeValues','ReturnLines','ExpenseLines','AccountEntries','PurchaseAddition','DocumentFields','QuoteLines',
 'PeopleCards','InventoryOperations','DocumentWorkflows','ItemLifecycle','SalaryBatch','TableSplit','SettlementV2Only',
 'Receipts','ReceiptService','ReceiptUi_Offline','ReceiptSql_Offline')
foreach($suite in $suites){
 $scriptFile=Join-Path $PSScriptRoot ('Test_'+$suite+'.ps1')
 # SQL 2014 ScriptDom needs an isolated Windows PowerShell process here.
 # Existing fixtures contain BOM-less UTF-8 Arabic SQL; do not read them as the Windows ANSI code page.
 $invoke = '$ErrorActionPreference="Stop"; $PSDefaultParameterValues["Get-Content:Encoding"]="UTF8"; & ''' + $scriptFile.Replace("'","''") + '''; if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }'
 & powershell.exe -NoProfile -ExecutionPolicy Bypass -Command $invoke *>&1 | Tee-Object -FilePath (Join-Path $output ('Final_'+$suite+'.log'))
 if($LASTEXITCODE -ne 0){throw ('Offline suite failed: '+$suite)}
}
Write-Output ('PASS '+$suites.Count+' offline suites; no deployment or runtime replacement.')
