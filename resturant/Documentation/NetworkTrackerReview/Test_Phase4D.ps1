param([switch]$InstallPrerequisites,[switch]$LiveRollbackTests)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditPhase4D'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$settingsPath=Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings'
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSalesEdit.vb',
 'Classes\Audit\AuditSalesLineEdit.vb','Classes\Audit\AuditSalesQuantity.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources+=Join-Path $PSScriptRoot 'AuditSalesQuantityTests.vb'
$exe=Join-Path $output 'AuditSalesQuantityTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditSalesQuantityTests `
 /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Quantity tests did not compile.'}
if($InstallPrerequisites){
 if(Get-Process -Name CPOS -ErrorAction SilentlyContinue){throw 'Close CPOS before deployment; no process is killed.'}
 $setting=([IO.File]::ReadAllLines($settingsPath) | Where-Object {$_.StartsWith('SqlConStr:')}).Substring(10)
 $b=[System.Data.SqlClient.SqlConnectionStringBuilder]::new($setting)
 if($b.InitialCatalog -ne 'CPOS_ACCOUNTING'){throw 'Unexpected database.'}
 $b['Application Name']='CPOS Audit4D Upgrade';$b['Pooling']=$false
 $cn=[System.Data.SqlClient.SqlConnection]::new($b.ConnectionString)
 try{
  $cn.Open();$cmd=$cn.CreateCommand()
  $cmd.CommandText="SELECT o.name,m.definition FROM sys.objects o JOIN sys.sql_modules m ON m.object_id=o.object_id WHERE o.schema_id=SCHEMA_ID('dbo') AND o.name IN('Network_Audit_Request_Begin','Network_Audit_Event_Write','SB_Contents_Change_IM_Qty','SB_Contents_Structers_Recount_One','SB_Contents_Structers_Remove_ONE','SB_Contents_Structers_insert_ONE','AG_Balance_Update_Total','AG_Balance_Update_Pied_On_Update','Recalculate_Agent_Bill_Total','Inc_IM_QTY','Dec_IM_QTY')"
  $snapshot=[System.Data.DataTable]::new('Definitions');$snapshot.Load($cmd.ExecuteReader())
  if($snapshot.Rows.Count -ne 11){throw 'Expected eleven baseline definitions.'}
  $snapshot.WriteXml((Join-Path $output ('Before_'+[guid]::NewGuid().ToString('N')+'.xml')),[System.Data.XmlWriteMode]::WriteSchema)
  $cmd=$cn.CreateCommand();$cmd.CommandTimeout=60
  $cmd.CommandText=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\012_Sales_Line_Quantity.sql'))
  [void]$cmd.ExecuteNonQuery()
  'Installed 012 atomically; original business procedures unchanged.'
 }finally{$cn.Dispose()}
}
$arguments=@();if($LiveRollbackTests){$arguments=@('--settings',$settingsPath)}
& $exe $arguments
if($LASTEXITCODE -ne 0){throw 'Quantity tests failed; live fixtures use rollback.'}
