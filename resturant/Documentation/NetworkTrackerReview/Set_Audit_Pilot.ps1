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
 $expectedDomains=2+[int]$hasSalesCatalog+[int]$hasDocumentCatalog+[int]$hasLineCatalog+[int]$hasQuantityCatalog+[int]$hasDeleteCatalog
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
    $cmd.CommandText="SELECT EnabledDomainsXml.value('count(/Domains/Domain[@code=""Security"" or @code=""Inventory.Settlement"" or @code=""Sales.EditOpening"" or @code=""Documents.EditOpening"" or @code=""Sales.LineEditing"" or @code=""Sales.LineQuantity"" or @code=""Sales.LineLifecycle""])','int') FROM dbo.Network_Audit_Source WHERE ConfigId=1"
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
  $count=$xml.SelectNodes('/Domains/Domain[@code="Security" or @code="Inventory.Settlement" or @code="Sales.EditOpening" or @code="Documents.EditOpening" or @code="Sales.LineEditing" or @code="Sales.LineQuantity" or @code="Sales.LineLifecycle"]').Count
  if($count -ne ($expectedDomains*$flag)){throw 'Pilot state did not match the requested action; inspect before retrying.'}
  $after|ConvertTo-Json -Compress
 }
}finally{$cn.Dispose()}
