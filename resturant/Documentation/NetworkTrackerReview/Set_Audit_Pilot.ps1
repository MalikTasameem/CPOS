# Run with Windows PowerShell 5.1 (.NET Framework), not pwsh, for reflection-only inspection.
param(
 [ValidateSet('Status','Probe','Enable','Disable')][string]$Action='Status',
 [Parameter(Mandatory=$true)][string]$RuntimeDirectory,
 [Parameter(Mandatory=$true)][guid]$ExpectedSource,
 [switch]$SessionsClosed
)
$ErrorActionPreference='Stop'
if($PSVersionTable.PSEdition -ne 'Desktop'){throw 'Use Windows PowerShell 5.1 for this .NET Framework package check.'}
$runtime=(Resolve-Path -LiteralPath $RuntimeDirectory).Path
$exe=Join-Path $runtime 'CPOS.exe'
$settings=@{}
foreach($line in [IO.File]::ReadLines((Join-Path $runtime 'Settings Files\BackUpSettings.AppSettings'))){
 $p=$line.IndexOf(':');if($p -gt 0){$settings[$line.Substring(0,$p)]=$line.Substring($p+1)}
}
if($settings['DataBase'] -ne 'CPOS_ACCOUNTING' -or [Convert]::ToBoolean($settings['IsAttachDB'])){throw 'Pilot must use CPOS_ACCOUNTING, not an attached database.'}
if([Convert]::ToBoolean($settings['isCenterSys']) -or [Convert]::ToBoolean($settings['is_SubSys'])){throw 'This local pilot is not approved for a synchronized/central client.'}
# Reconstruct the same connection target as Build_Connection, without logging credentials.
$builder=New-Object System.Data.SqlClient.SqlConnectionStringBuilder
$builder['Data Source']=$settings['S_SERVER'];$builder['Initial Catalog']=$settings['DataBase']
if($settings['DB_Authentication'] -eq '0'){$builder['Integrated Security']=$true}
else{$builder['User ID']=$settings['DB_UName'];$builder['Password']=$settings['DB_Pass']}
$builder['Application Name']='CPOS Audit Pilot Control';$builder['Connect Timeout']=5
$stored=New-Object System.Data.SqlClient.SqlConnectionStringBuilder($settings['SqlConStr'])
if($stored.InitialCatalog -ne $builder.InitialCatalog -or $stored.DataSource.Trim() -ne $builder.DataSource.Trim()){throw 'Saved and rebuilt connection targets differ; review settings before enabling.'}
$assembly=[Reflection.Assembly]::ReflectionOnlyLoadFrom($exe)
$resources=$assembly.GetManifestResourceNames()
if($resources.Count -lt 100 -or $resources -notcontains 'resturant.Resources.resources' -or $resources -notcontains 'resturant.ST_settlement.resources' -or
 $null -eq $assembly.GetType('resturant.AuditSettlementHeader',$false)) {throw 'Full client/resources/header implementation are missing; do not deploy a CoreCompile-only executable.'}
foreach($required in @('CPOS.exe.config','ACCOUNTING.exe','MetroFramework.dll','DevComponents.DotNetBar2.dll','CrystalDecisions.Windows.Forms.dll','reports')){
 if(-not(Test-Path -LiteralPath (Join-Path $runtime $required))){throw "Missing runtime dependency: $required"}
}
[pscustomobject]@{Executable=$exe;ResourceCount=$resources.Count;SHA256=(Get-FileHash -LiteralPath $exe -Algorithm SHA256).Hash;Action=$Action}|ConvertTo-Json -Compress
if($Action -ne 'Status'){
 if(-not $SessionsClosed){throw 'Close all clients and explicitly pass -SessionsClosed; none will be killed automatically.'}
 if(Get-Process -Name CPOS -ErrorAction SilentlyContinue){throw 'A CPOS process is still running; close it normally.'}
}
$cn=New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)
function Read-PilotStatus {
 $cmd=$cn.CreateCommand();$cmd.CommandTimeout=10
 $cmd.CommandText="SELECT DB_NAME() AS DatabaseName,SourceId,CONVERT(nvarchar(max),EnabledDomainsXml) AS Domains,(SELECT COUNT(*) FROM dbo.Network_Audit_Request) AS Requests,(SELECT COUNT(*) FROM dbo.Network_Audit_Event) AS Events,(SELECT COUNT(*) FROM dbo.Network_Audit_Change) AS Changes FROM dbo.Network_Audit_Source WHERE ConfigId=1"
 $table=New-Object System.Data.DataTable;$reader=$cmd.ExecuteReader()
 try{$table.Load($reader)}finally{$reader.Dispose();$cmd.Dispose()}
 if($table.Rows.Count -ne 1 -or [guid]$table.Rows[0]['SourceId'] -ne $ExpectedSource){throw 'Unexpected audit source.'}
 $row=$table.Rows[0]
 [pscustomobject]@{Database=$row['DatabaseName'];SourceId=$row['SourceId'];Domains=$row['Domains'];Requests=$row['Requests'];Events=$row['Events'];Changes=$row['Changes']}
}
try{
 $cn.Open();$before=Read-PilotStatus;$before|ConvertTo-Json -Compress
 if($Action -eq 'Status'){return}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Sales.EditOpened%'"
 $salesCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($salesCatalogCount -eq 1){throw 'Sales edit audit catalogue is partially installed; inspect before toggling.'}
 $hasSalesCatalog=$salesCatalogCount -eq 2
 if($Action -in @('Enable','Probe') -and $hasSalesCatalog -and $null -eq $assembly.GetType('resturant.AuditSalesEdit',$false)){throw 'Database includes 009; build the matching sales-edit client before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Document.EditOpened%'"
 $documentCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($documentCatalogCount -eq 1){throw 'Document edit audit catalogue is partially installed; inspect before toggling.'}
 $hasDocumentCatalog=$documentCatalogCount -eq 2
 if($hasDocumentCatalog -and -not $hasSalesCatalog){throw '010 requires the preceding 009 catalogue.'}
 if($Action -in @('Enable','Probe') -and $hasDocumentCatalog -and $null -eq $assembly.GetType('resturant.AuditDocumentEdit',$false)){throw 'Database includes 010; build the matching document-edit client before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Sales.LineDetailsChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Sales.LinePriceChanged%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $lineCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($lineCatalogCount -notin @(0,4)){throw 'Sales line audit catalogue is partially installed; inspect before toggling.'}
 $hasLineCatalog=$lineCatalogCount -eq 4
 if($hasLineCatalog -and -not $hasDocumentCatalog){throw '011 requires the preceding 010 catalogue.'}
 if($Action -in @('Enable','Probe') -and $hasLineCatalog -and $null -eq $assembly.GetType('resturant.AuditSalesLineEdit',$false)){throw 'Database includes 011; build the matching sales-line client before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Sales.LineQuantityStepped%'"
 $quantityCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($quantityCatalogCount -eq 1){throw 'Quantity audit catalogue is partially installed.'}
 $hasQuantityCatalog=$quantityCatalogCount -eq 2
 if($hasQuantityCatalog -and -not $hasLineCatalog){throw '012 requires the preceding 011 catalogue.'}
 if($Action -in @('Enable','Probe') -and $hasQuantityCatalog -and $null -eq $assembly.GetType('resturant.AuditSalesQuantity',$false)){throw 'Database includes 012; build the matching quantity client before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Sales.LineDeleted%'"
 $deleteCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($deleteCatalogCount -eq 1){throw 'Deletion catalogue is partially installed.'}
 $hasDeleteCatalog=$deleteCatalogCount -eq 2
 if($hasDeleteCatalog -and -not $hasQuantityCatalog){throw '014 requires the preceding 012 catalogue.'}
 if($Action -in @('Enable','Probe') -and $hasDeleteCatalog -and $null -eq $assembly.GetType('resturant.AuditSalesDelete',$false)){throw 'Database includes 014; build the matching sales-deletion client before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Sales.LineAdded%'"
 $addCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($addCatalogCount -eq 1){throw 'Addition catalogue is partially installed.'}
 $hasAddCatalog=$addCatalogCount -eq 2
 if($hasAddCatalog -and -not $hasDeleteCatalog){throw '017 requires the preceding lifecycle catalogue.'}
 if($Action -in @('Enable','Probe') -and $hasAddCatalog -and $null -eq $assembly.GetType('resturant.AuditSalesAdd',$false)){throw 'Database includes 017; build the matching sales-addition client before enabling.'}
 if($Action -in @('Enable','Probe') -and -not $hasAddCatalog -and $null -ne $assembly.GetType('resturant.AuditSalesAdd',$false)){throw 'This client requires the 017 addition catalogue before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Document.Voided%'"
 $voidCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($voidCatalogCount -eq 1){throw 'Cancellation catalogue is partially installed.'}
 $hasVoidCatalog=$voidCatalogCount -eq 2
 if($hasVoidCatalog -and -not $hasAddCatalog){throw '018 requires 017.'}
 if($Action -in @('Enable','Probe') -and $hasVoidCatalog -and $null -eq $assembly.GetType('resturant.AuditDocumentVoid',$false)){throw 'Database includes 018; build the matching cancellation client.'}
 if($Action -in @('Enable','Probe') -and -not $hasVoidCatalog -and $null -ne $assembly.GetType('resturant.AuditDocumentVoid',$false)){throw 'This client requires 018 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Unit.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Unit.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Unit.Deleted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $unitCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($unitCatalogCount -notin @(0,6)){throw 'Unit catalogue is partially installed.'}
 $hasUnitCatalog=$unitCatalogCount -eq 6
 if($hasUnitCatalog -and -not $hasVoidCatalog){throw '019 requires 018.'}
 if($Action -in @('Enable','Probe') -and $hasUnitCatalog -and $null -eq $assembly.GetType('resturant.AuditUnits',$false)){throw 'Database includes 019; build the matching units client.'}
 if($Action -in @('Enable','Probe') -and -not $hasUnitCatalog -and $null -ne $assembly.GetType('resturant.AuditUnits',$false)){throw 'This client requires 019 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Group.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Group.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Group.Deleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Group.PrinterLinked%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Group.PrinterUnlinked%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $groupCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($groupCatalogCount -notin @(0,10)){throw 'Group catalogue is partially installed.'}
 $hasGroupCatalog=$groupCatalogCount -eq 10
 if($hasGroupCatalog -and -not $hasUnitCatalog){throw '020 requires 019.'}
 if($Action -in @('Enable','Probe') -and $hasGroupCatalog -and $null -eq $assembly.GetType('resturant.AuditGroups',$false)){throw 'Database includes 020; build the matching groups client.'}
 if($Action -in @('Enable','Probe') -and -not $hasGroupCatalog -and $null -ne $assembly.GetType('resturant.AuditGroups',$false)){throw 'This client requires 020 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%ItemUnit.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%ItemUnit.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%ItemUnit.Deleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%ItemUnit.PriceChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%ItemUnit.OfferPriceChanged%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $itemUnitCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($itemUnitCatalogCount -notin @(0,10)){throw 'Item-unit catalogue is partially installed.'}
 $hasItemUnitCatalog=$itemUnitCatalogCount -eq 10
 if($hasItemUnitCatalog -and -not $hasGroupCatalog){throw '021 requires 020.'}
 if($Action -in @('Enable','Probe') -and $hasItemUnitCatalog -and $null -eq $assembly.GetType('resturant.AuditItemUnits',$false)){throw 'Database includes 021; build the matching item-unit client.'}
 if($Action -in @('Enable','Probe') -and -not $hasItemUnitCatalog -and $null -ne $assembly.GetType('resturant.AuditItemUnits',$false)){throw 'This client requires 021 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Offer.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Offer.ItemOffersDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%PriceRule.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%PriceRule.Deleted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $pricingCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($pricingCatalogCount -notin @(0,8)){throw 'Pricing catalogue is partially installed.'}
 $hasPricingCatalog=$pricingCatalogCount -eq 8
 if($hasPricingCatalog -and -not $hasItemUnitCatalog){throw '022 requires 021.'}
 if($Action -in @('Enable','Probe') -and $hasPricingCatalog -and $null -eq $assembly.GetType('resturant.AuditPricingRules',$false)){throw 'Database includes 022; build the matching pricing client.'}
 if($Action -in @('Enable','Probe') -and -not $hasPricingCatalog -and $null -ne $assembly.GetType('resturant.AuditPricingRules',$false)){throw 'This client requires 022 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Item.FastUpdated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Item.Deleted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $itemMasterCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($itemMasterCatalogCount -notin @(0,4)){throw 'Item catalogue is partially installed.'}
 $hasItemMasterCatalog=$itemMasterCatalogCount -eq 4
 if($hasItemMasterCatalog -and -not $hasPricingCatalog){throw '023 requires 022.'}
 if($Action -in @('Enable','Probe') -and $hasItemMasterCatalog -and $null -eq $assembly.GetType('resturant.AuditItemMaster',$false)){throw 'Database includes 023; build the matching item client.'}
 if($Action -in @('Enable','Probe') -and -not $hasItemMasterCatalog -and $null -ne $assembly.GetType('resturant.AuditItemMaster',$false)){throw 'This client requires 023 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Table.Transferred%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Table.LineQuantityChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Table.LineDeleted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $tablesCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($tablesCatalogCount -notin @(0,6)){throw 'Table catalogue is partially installed.'}
 $hasTablesCatalog=$tablesCatalogCount -eq 6
 if($hasTablesCatalog -and -not $hasItemMasterCatalog){throw '024 requires 023.'}
 if($Action -in @('Enable','Probe') -and $hasTablesCatalog -and $null -eq $assembly.GetType('resturant.AuditTables',$false)){throw 'Database includes 024; build the matching table client.'}
 if($Action -in @('Enable','Probe') -and -not $hasTablesCatalog -and $null -ne $assembly.GetType('resturant.AuditTables',$false)){throw 'This client requires 024 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%TreasuryMovement.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TreasuryMovement.Voided%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TreasuryTransfer.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TreasuryTransfer.Voided%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%AccountTransfer.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%AccountTransfer.Voided%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $financeCatalogCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($financeCatalogCount -notin @(0,12)){throw 'Finance catalogue is partially installed.'}
 $hasFinanceCatalog=$financeCatalogCount -eq 12
 if($hasFinanceCatalog -and -not $hasTablesCatalog){throw '025 requires 024.'}
 if($Action -in @('Enable','Probe') -and $hasFinanceCatalog -and $null -eq $assembly.GetType('resturant.AuditFinance',$false)){throw 'Database includes 025; build the matching finance client.'}
 if($Action -in @('Enable','Probe') -and -not $hasFinanceCatalog -and $null -ne $assembly.GetType('resturant.AuditFinance',$false)){throw 'This client requires 025 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%StockDocument.LineAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%StockDocument.LineDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%StockDocument.LineQuantityChanged%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $stockLinesCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($stockLinesCount -notin @(0,6)){throw 'Stock lines catalogue is partially installed.'}
 $hasStockLines=$stockLinesCount -eq 6
 if($hasStockLines -and -not $hasFinanceCatalog){throw '026 requires 025.'}
 if($Action -in @('Enable','Probe') -and $hasStockLines -and $null -eq $assembly.GetType('resturant.AuditStockDocumentLines',$false)){throw 'Database includes 026; build the matching stock lines client.'}
 if($Action -in @('Enable','Probe') -and -not $hasStockLines -and $null -ne $assembly.GetType('resturant.AuditStockDocumentLines',$false)){throw 'This client requires 026 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%EmployeeValue.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%EmployeeValue.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%EmployeeValue.Voided%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $employeeValuesCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($employeeValuesCount -notin @(0,6)){throw 'Employee value catalogue is partially installed.'}
 $hasEmployeeValues=$employeeValuesCount -eq 6
 if($hasEmployeeValues -and -not $hasStockLines){throw '027 requires 026.'}
 if($Action -in @('Enable','Probe') -and $hasEmployeeValues -and $null -eq $assembly.GetType('resturant.AuditEmployeeValues',$false)){throw 'Database includes 027; build the matching employee value client.'}
 if($Action -in @('Enable','Probe') -and -not $hasEmployeeValues -and $null -ne $assembly.GetType('resturant.AuditEmployeeValues',$false)){throw 'This client requires 027 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Return.LineAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Return.LineDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Return.LineQuantityChanged%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $returnLinesCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($returnLinesCount -notin @(0,6)){throw 'Return line catalogue is partially installed.'}
 $hasReturnLines=$returnLinesCount -eq 6
 if($hasReturnLines -and -not $hasEmployeeValues){throw '028 requires 027.'}
 if($Action -in @('Enable','Probe') -and $hasReturnLines -and $null -eq $assembly.GetType('resturant.AuditReturnLines',$false)){throw 'Database includes 028; build the matching return line client.'}
 if($Action -in @('Enable','Probe') -and -not $hasReturnLines -and $null -ne $assembly.GetType('resturant.AuditReturnLines',$false)){throw 'This client requires 028 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Expense.LineAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Expense.LineDeleted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $expenseLinesCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($expenseLinesCount -notin @(0,4)){throw 'Expense line catalogue is partially installed.'}
 $hasExpenseLines=$expenseLinesCount -eq 4
 if($hasExpenseLines -and -not $hasReturnLines){throw '029 requires 028.'}
 if($Action -in @('Enable','Probe') -and $hasExpenseLines -and $null -eq $assembly.GetType('resturant.AuditExpenseLines',$false)){throw 'Database includes 029; build the matching expense line client.'}
 if($Action -in @('Enable','Probe') -and -not $hasExpenseLines -and $null -ne $assembly.GetType('resturant.AuditExpenseLines',$false)){throw 'This client requires 029 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%AccountEntry.Created%'"
 $entryCount=[int]$catalog.ExecuteScalar();$catalog.Dispose()
 if($entryCount -notin @(0,2)){throw 'Account entry catalogue is partially installed.'}
 $hasAccountEntries=$entryCount -eq 2
 if($hasAccountEntries -and -not $hasExpenseLines){throw '030 requires 029.'}
 if($Action -in @('Enable','Probe') -and $hasAccountEntries -and $null -eq $assembly.GetType('resturant.AuditAccountEntry',$false)){throw 'Database includes 030; build the matching account entry client.'}
 if($Action -in @('Enable','Probe') -and -not $hasAccountEntries -and $null -ne $assembly.GetType('resturant.AuditAccountEntry',$false)){throw 'This client requires 030 before enabling.'}
 $catalog=$cn.CreateCommand()
 $catalog.CommandText="SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Purchase.LineAdded%'"
 $purchaseCount=[int]$catalog.ExecuteScalar()
 if($purchaseCount -notin @(0,2)){throw 'Purchase addition catalogue is partially installed.'}
 $hasPurchaseAddition=$purchaseCount -eq 2
 if($hasPurchaseAddition -and -not $hasAccountEntries){throw '031 requires 030.'}
 if($Action -in @('Enable','Probe') -and $hasPurchaseAddition -and $null -eq $assembly.GetType('resturant.AuditPurchaseAdd',$false)){throw 'Database includes 031; build the matching purchase addition client.'}
 if($Action -in @('Enable','Probe') -and -not $hasPurchaseAddition -and $null -ne $assembly.GetType('resturant.AuditPurchaseAdd',$false)){throw 'This client requires 031 before enabling.'}
 $catalog.CommandText="SELECT SUM(CASE WHEN definition LIKE '%Document.AccountChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Document.DiscountChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Document.FastDiscountChanged%' THEN 1 ELSE 0 END) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $directFieldsCount=[int]$catalog.ExecuteScalar()
 if($directFieldsCount -notin @(0,6)){throw 'Direct header catalogue is partially installed.'}
 $hasDirectFields=$directFieldsCount -eq 6
 if($hasDirectFields -and -not $hasPurchaseAddition){throw '032 requires 031.'}
 if($Action -in @('Enable','Probe') -and $hasDirectFields -and $null -eq $assembly.GetType('resturant.AuditDocumentFields',$false)){throw 'Database includes 032; build the matching direct header client.'}
 if($Action -in @('Enable','Probe') -and -not $hasDirectFields -and $null -ne $assembly.GetType('resturant.AuditDocumentFields',$false)){throw 'This client requires 032 before enabling.'}
 $catalog.CommandText="SELECT SUM(CASE WHEN definition LIKE '%Quote.LineAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.LineDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.LineChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.LineQuantityStepped%' THEN 1 ELSE 0 END) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $quoteCount=[int]$catalog.ExecuteScalar()
 if($quoteCount -notin @(0,8)){throw 'Quote catalogue is partially installed.'}
 $hasQuotes=$quoteCount -eq 8
 if($hasQuotes -and -not $hasDirectFields){throw '034 requires revised 032.'}
 if($Action -in @('Enable','Probe') -and $hasQuotes -and $null -eq $assembly.GetType('resturant.AuditQuoteLines',$false)){throw 'Database includes 034; build the matching quote client.'}
 if($Action -in @('Enable','Probe') -and -not $hasQuotes -and $null -ne $assembly.GetType('resturant.AuditQuoteLines',$false)){throw 'This client requires 034 before enabling.'}
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Agent.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Agent.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Agent.Deleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%User.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%User.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%User.Deleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%User.CredentialsChanged%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $count_people=[int]$catalog.ExecuteScalar()
 if($count_people -notin @(0,14)){throw 'Stage 035 catalogue is partially installed.'}
 $has_people=$count_people -eq 14
 if($has_people -and -not $hasQuotes){throw 'Stage 035 requires the preceding catalogue.'}
 if($Action -in @('Enable','Probe') -and ($has_people -ne ($null -ne $assembly.GetType('resturant.AuditPeopleCards',$false)))){throw 'Stage 035 database/client mismatch; do not enable this package.'}
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Inventory.PurchaseLineDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Inventory.PurchaseLineChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.AutoProductAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.ManualLineAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.ProductDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.ProductChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.IngredientDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.CostRecalculated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.PurchaseCopied%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.SalesCopied%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Inventory.QuantityRecounted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Inventory.ItemCostRecounted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Inventory.AllCostsRecounted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $count_inventory=[int]$catalog.ExecuteScalar()
 if($count_inventory -notin @(0,26)){throw 'Stage 036 catalogue is partially installed.'}
 $has_inventory=$count_inventory -eq 26
 if($has_inventory -and -not $has_people){throw 'Stage 036 requires the preceding catalogue.'}
 if($Action -in @('Enable','Probe') -and ($has_inventory -ne ($null -ne $assembly.GetType('resturant.AuditInventoryOperations',$false)))){throw 'Stage 036 database/client mismatch; do not enable this package.'}
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Document.EditFieldsSaved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Document.ProjectChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Purchase.AccountCurrencyChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Purchase.CurrencyChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.Confirmed%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.DiscountChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.DiscountDistributed%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Document.MetadataSaved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.Reserved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.EditFieldsSaved%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $count_workflows=[int]$catalog.ExecuteScalar()
 if($count_workflows -notin @(0,20)){throw 'Stage 037 catalogue is partially installed.'}
 $has_workflows=$count_workflows -eq 20
 if($has_workflows -and -not $has_inventory){throw 'Stage 037 requires the preceding catalogue.'}
 if($Action -in @('Enable','Probe') -and ($has_workflows -ne ($null -ne $assembly.GetType('resturant.AuditDocumentWorkflows',$false)))){throw 'Stage 037 database/client mismatch; do not enable this package.'}
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Item.PlaceholderAcquired%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Item.FullSaved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Item.QuickSaved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Item.DisabledRowsRemoved%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $count_itemLife=[int]$catalog.ExecuteScalar()
 if($count_itemLife -notin @(0,8)){throw 'Stage 038 catalogue is partially installed.'}
 $has_itemLife=$count_itemLife -eq 8
 if($has_itemLife -and -not $has_workflows){throw 'Stage 038 requires the preceding catalogue.'}
 if($Action -in @('Enable','Probe') -and ($has_itemLife -ne ($null -ne $assembly.GetType('resturant.AuditItemLifecycle',$false)))){throw 'Stage 038 database/client mismatch; do not enable this package.'}
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Salary.BatchPosted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $count_salary=[int]$catalog.ExecuteScalar()
 if($count_salary -notin @(0,2)){throw 'Stage 039 catalogue is partially installed.'}
 $has_salary=$count_salary -eq 2
 if($has_salary -and -not $has_itemLife){throw 'Stage 039 requires the preceding catalogue.'}
 if($Action -in @('Enable','Probe') -and ($has_salary -ne ($null -ne $assembly.GetType('resturant.AuditSalaryBatch',$false)))){throw 'Stage 039 database/client mismatch; do not enable this package.'}
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%TableSplit.Started%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TableSplit.UnitMoved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TableSplit.UnitReturned%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TableSplit.Confirmed%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $count_tableSplit=[int]$catalog.ExecuteScalar()
 if($count_tableSplit -notin @(0,8)){throw 'Stage 040 catalogue is partially installed.'}
 $has_tableSplit=$count_tableSplit -eq 8
 if($has_tableSplit -and -not $has_salary){throw 'Stage 040 requires the preceding catalogue.'}
 if($Action -in @('Enable','Probe') -and ($has_tableSplit -ne ($null -ne $assembly.GetType('resturant.AuditTableSplit',$false)))){throw 'Stage 040 database/client mismatch; do not enable this package.'}
 $catalog.CommandText="SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Receipt.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Receipt.EditOpened%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Receipt.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Receipt.Voided%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write'))"
 $count_receipts=[int]$catalog.ExecuteScalar()
 if($count_receipts -notin @(0,8)){throw 'Stage 041 catalogue is partially installed.'}
 $has_receipts=$count_receipts -eq 8
 if($has_receipts -and -not $has_tableSplit){throw 'Stage 041 requires the preceding catalogue.'}
 if($Action -in @('Enable','Probe') -and ($has_receipts -ne ($null -ne $assembly.GetType('resturant.AuditReceipt',$false)))){throw 'Stage 041 database/client mismatch; do not enable this package.'}
 $catalog.Dispose()
 $expectedDomains=2+[int]$has_receipts+[int]$has_people+[int]$has_inventory+[int]$has_workflows+[int]$has_itemLife+[int]$has_salary+[int]$has_tableSplit+[int]$hasQuotes+[int]$hasDirectFields+[int]$hasSalesCatalog+[int]$hasDocumentCatalog+[int]$hasLineCatalog+[int]$hasQuantityCatalog+[int]$hasDeleteCatalog+[int]$hasVoidCatalog+[int]$hasUnitCatalog+[int]$hasGroupCatalog+[int]$hasItemUnitCatalog+[int]$hasPricingCatalog+[int]$hasItemMasterCatalog+[int]$hasTablesCatalog+[int]$hasFinanceCatalog+[int]$hasStockLines+[int]$hasEmployeeValues+[int]$hasReturnLines+[int]$hasExpenseLines+[int]$hasAccountEntries+[int]$hasPurchaseAddition
 $projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
 $template=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\007_Audit_Pilot_Gate.sql'))
 $template=$template.Replace('DECLARE @ExpectedSource uniqueidentifier = NULL;',"DECLARE @ExpectedSource uniqueidentifier = '$($ExpectedSource.ToString('D'))';")
 if($Action -eq 'Probe'){
  $tx=$cn.BeginTransaction()
  try{
   foreach($flag in @(1,1,0,0)){
    $cmd=$cn.CreateCommand();$cmd.Transaction=$tx;$cmd.CommandTimeout=15
    $cmd.CommandText=$template.Replace('DECLARE @Enable bit = NULL;',"DECLARE @Enable bit = $flag;")
    [void]$cmd.ExecuteNonQuery();$cmd.Dispose()
    $cmd=$cn.CreateCommand();$cmd.Transaction=$tx
    $cmd.CommandText="SELECT EnabledDomainsXml.value('count(/Domains/Domain[@code=""Security"" or @code=""Inventory.Settlement"" or @code=""Sales.EditOpening"" or @code=""Documents.EditOpening"" or @code=""Sales.LineEditing"" or @code=""Sales.LineQuantity"" or @code=""Sales.LineLifecycle"" or @code=""Documents.Cancellation"" or @code=""Catalog.Units"" or @code=""Catalog.Groups"" or @code=""Catalog.ItemUnits"" or @code=""Catalog.PricingRules"" or @code=""Catalog.Items"" or @code=""Sales.Tables"" or @code=""Finance.Transfers"" or @code=""Inventory.DocumentLines"" or @code=""People.EmployeeValues"" or @code=""Sales.ReturnLines"" or @code=""Expenses.Lines"" or @code=""Finance.AccountEntries"" or @code=""Inventory.PurchaseAddition"" or @code=""Documents.DirectFields"" or @code=""Sales.QuoteLines"" or @code=""People.Cards"" or @code=""Inventory.Operations"" or @code=""Documents.Workflows"" or @code=""Catalog.ItemLifecycle"" or @code=""People.Salaries"" or @code=""Sales.TableSplit"" or @code=""Finance.Receipts""])','int') FROM dbo.Network_Audit_Source WHERE ConfigId=1"
    $count=[int]$cmd.ExecuteScalar();$cmd.Dispose()
    if($count -ne ($expectedDomains*$flag)){throw 'Pilot toggle verification failed.'}
   }
   $tx.Rollback()
  }finally{if($tx.Connection){$tx.Rollback()};$tx.Dispose()}
  $after=Read-PilotStatus
  if($after.Domains -ne $before.Domains -or $after.Requests -ne $before.Requests -or $after.Events -ne $before.Events -or $after.Changes -ne $before.Changes){throw 'Probe did not restore the exact baseline.'}
  'PASS: enable/enable/disable/disable rolled back; audit history unchanged.'
  $after|ConvertTo-Json -Compress
 }else{
  $flag=if($Action -eq 'Enable'){1}else{0}
  $cmd=$cn.CreateCommand();$cmd.CommandTimeout=15
  $cmd.CommandText=$template.Replace('DECLARE @Enable bit = NULL;',"DECLARE @Enable bit = $flag;")
  [void]$cmd.ExecuteNonQuery();$cmd.Dispose()
  $after=Read-PilotStatus
  $xml=[xml]$after.Domains
  $count=$xml.SelectNodes('/Domains/Domain[@code="Security" or @code="Inventory.Settlement" or @code="Sales.EditOpening" or @code="Documents.EditOpening" or @code="Sales.LineEditing" or @code="Sales.LineQuantity" or @code="Sales.LineLifecycle" or @code="Documents.Cancellation" or @code="Catalog.Units" or @code="Catalog.Groups" or @code="Catalog.ItemUnits" or @code="Catalog.PricingRules" or @code="Catalog.Items" or @code="Sales.Tables" or @code="Finance.Transfers" or @code="Inventory.DocumentLines" or @code="People.EmployeeValues" or @code="Sales.ReturnLines" or @code="Expenses.Lines" or @code="Finance.AccountEntries" or @code="Inventory.PurchaseAddition" or @code="Documents.DirectFields" or @code="Sales.QuoteLines" or @code="People.Cards" or @code="Inventory.Operations" or @code="Documents.Workflows" or @code="Catalog.ItemLifecycle" or @code="People.Salaries" or @code="Sales.TableSplit" or @code="Finance.Receipts"]').Count
  if($count -ne ($expectedDomains*$flag)){throw 'Pilot state did not match the requested action; inspect before retrying.'}
  $after|ConvertTo-Json -Compress
 }
}finally{$cn.Dispose()}
