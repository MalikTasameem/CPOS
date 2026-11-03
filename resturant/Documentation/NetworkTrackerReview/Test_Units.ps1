param([switch]$LiveRollbackTests,[switch]$CandidateRollback,[switch]$InstallPrerequisites)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditUnits019'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSalesEdit.vb',
 'Classes\Audit\AuditSalesLineEdit.vb','Classes\Audit\AuditSalesQuantity.vb','Classes\Audit\AuditUnits.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources += @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | ForEach-Object { $_.FullName })
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditUnitTests.vb'
$exe=Join-Path $output 'AuditUnitTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditUnitTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Unit catalogue tests did not compile'}
$settings=Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings'
if($LiveRollbackTests -or $CandidateRollback -or $InstallPrerequisites){if(Get-Process -Name CPOS -ErrorAction SilentlyContinue){throw 'Close CPOS normally; no process is killed.'}}
if($InstallPrerequisites){
 if($CandidateRollback){throw 'Choose candidate or installation, not both'}
 $setting=([IO.File]::ReadAllLines($settings) | Where-Object {$_.StartsWith('SqlConStr:')}).Substring(10)
 $builder=[System.Data.SqlClient.SqlConnectionStringBuilder]::new($setting)
 if($builder.InitialCatalog -ne 'CPOS_ACCOUNTING'){throw 'Unexpected database'}
 $builder['Application Name']='CPOS Units019 Upgrade';$builder['Pooling']=$false
 $cn=[System.Data.SqlClient.SqlConnection]::new($builder.ConnectionString)
 try{
  $cn.Open();$cmd=$cn.CreateCommand()
  $cmd.CommandText='SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()'
  if($cmd.ExecuteScalar() -ne 0){throw 'Close other database clients before installation.'}
  $cmd.CommandText="SELECT name,OBJECT_DEFINITION(object_id) AS Definition FROM sys.procedures WHERE name IN('Network_Audit_Request_Begin','Network_Audit_Event_Write','Unit_Insert','Unit_Update','Unit_Delete','AA_GET_MAX_UNITS')"
  $data=[System.Data.DataTable]::new('Definitions');$data.Load($cmd.ExecuteReader())
  if($data.Rows.Count -ne 6){throw 'Unexpected procedure baseline'}
  $data.WriteXml((Join-Path $output ('Before_'+[guid]::NewGuid().ToString('N')+'.xml')),[System.Data.XmlWriteMode]::WriteSchema)
  $cmd.CommandText=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\019_Unit_Catalogue.sql'));$cmd.CommandTimeout=60
  [void]$cmd.ExecuteNonQuery();$cmd.Dispose()
  'Installed 019 atomically; business procedures unchanged.'
 }finally{$cn.Dispose()}
}
$arguments=@()
if($LiveRollbackTests -or $CandidateRollback -or $InstallPrerequisites){$arguments=@('--settings',$settings)}
if($CandidateRollback){$arguments+=@('--candidate',(Join-Path $projectRoot 'Database\Audit\019_Unit_Catalogue.sql'))}
& $exe $arguments
if($LASTEXITCODE -ne 0){throw 'Unit catalogue tests failed; fixtures use rollback'}
