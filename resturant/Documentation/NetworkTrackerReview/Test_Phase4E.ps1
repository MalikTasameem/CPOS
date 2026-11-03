param([switch]$InstallPrerequisites,[switch]$LiveRollbackTests)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditPhase4E'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSalesEdit.vb',
 'Classes\Audit\AuditSalesLineEdit.vb','Classes\Audit\AuditSalesQuantity.vb','Classes\Audit\AuditSalesDelete.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources += @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | ForEach-Object { $_.FullName })
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditSalesDeleteTests.vb'
$exe=Join-Path $output 'AuditSalesDeleteTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditSalesDeleteTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Delete tests did not compile'}
$settings=Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings'
# An idle Visual Studio hosting process may remain after Stop Debugging; live SQL checks reject all actual sessions.
if($InstallPrerequisites -or $LiveRollbackTests){if(Get-Process -Name CPOS -ErrorAction SilentlyContinue){throw 'Close CPOS before live tests or deployment; no process is killed.'}}
if($InstallPrerequisites){
 $setting=([IO.File]::ReadAllLines($settings) | Where-Object {$_.StartsWith('SqlConStr:')}).Substring(10)
 $builder=[System.Data.SqlClient.SqlConnectionStringBuilder]::new($setting)
 if($builder.InitialCatalog -ne 'CPOS_ACCOUNTING'){throw 'Unexpected database'}
 $builder['Application Name']='CPOS Audit4E Upgrade';$builder['Pooling']=$false
 $cn=[System.Data.SqlClient.SqlConnection]::new($builder.ConnectionString)
 try{
  $cn.Open();$cmd=$cn.CreateCommand()
  $cmd.CommandText="SELECT name,OBJECT_DEFINITION(object_id) AS Definition FROM sys.procedures WHERE name IN('Network_Audit_Request_Begin','Network_Audit_Event_Write','SB_Contents_Delete_IM','SB_Contents_Structers_Remove_ONE','AG_Balance_Update_Pied_On_Update','AG_Balance_Update_Total','Recalculate_Agent_Bill_Total','Inc_IM_QTY')"
  $data=[System.Data.DataTable]::new('Definitions');$data.Load($cmd.ExecuteReader());$cmd.Dispose()
  if($data.Rows.Count -ne 8){throw 'Unexpected procedure baseline'}
  $data.WriteXml((Join-Path $output ('Before_'+[guid]::NewGuid().ToString('N')+'.xml')),[System.Data.XmlWriteMode]::WriteSchema)
  $cmd=$cn.CreateCommand();$cmd.CommandTimeout=60;$cmd.CommandText=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\014_Sales_Line_Deletion.sql'))
  [void]$cmd.ExecuteNonQuery();$cmd.Dispose()
  'Installed 014 atomically; business definitions unchanged.'
 }finally{$cn.Dispose()}
}
$arguments=@();if($LiveRollbackTests){$arguments=@('--settings',$settings)}
& $exe $arguments
if($LASTEXITCODE -ne 0){throw 'Delete tests failed; fixtures use rollback'}
