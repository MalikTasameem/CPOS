param([switch]$InstallCorrection)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
if($InstallCorrection){
 if(Get-Process -Name CPOS -ErrorAction SilentlyContinue){throw 'Close CPOS normally; no process is killed.'}
 $setting=([IO.File]::ReadAllLines((Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings')) | Where-Object {$_.StartsWith('SqlConStr:')}).Substring(10)
 $builder=[System.Data.SqlClient.SqlConnectionStringBuilder]::new($setting)
 if($builder.InitialCatalog -ne 'CPOS_ACCOUNTING'){throw 'Unexpected database'}
 $builder['Application Name']='CPOS DetailComponents016 Upgrade';$builder['Pooling']=$false
 $cn=[System.Data.SqlClient.SqlConnection]::new($builder.ConnectionString)
 try{
  $cn.Open();$cmd=$cn.CreateCommand()
  $cmd.CommandText='SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()'
  if($cmd.ExecuteScalar() -ne 0){throw 'Close other database clients before installation.'}
  $cmd.CommandText="SELECT name,OBJECT_DEFINITION(object_id) AS Definition FROM sys.procedures WHERE name IN('SB_IM_Unit_Change','Network_Audit_Event_Write')"
  $data=[System.Data.DataTable]::new('Definitions');$data.Load($cmd.ExecuteReader())
  if($data.Rows.Count -ne 2){throw 'Unexpected snapshot count'}
  $output=Join-Path $projectRoot '..\.tmp\AuditComponents016';New-Item -ItemType Directory -Path $output -Force | Out-Null
  $data.WriteXml((Join-Path $output ('Before_'+[guid]::NewGuid().ToString('N')+'.xml')),[System.Data.XmlWriteMode]::WriteSchema)
  $cmd.CommandText=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\016_Sales_Detail_Components.sql'))
  $cmd.CommandTimeout=60;[void]$cmd.ExecuteNonQuery();$cmd.Dispose()
  'Installed 016 atomically; existing domains and financial calculations preserved.'
 }finally{$cn.Dispose()}
}
& (Join-Path $PSScriptRoot 'Test_Phase4C.ps1') -LiveRollbackTests
if(-not $?){throw 'Detail component regression failed'}
