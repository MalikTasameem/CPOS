param([switch]$InstallPrerequisites,[switch]$LiveRollbackTests)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditPhase3C'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$settingsPath=Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings'
if($InstallPrerequisites){
 $settings=@{}
 foreach($line in [IO.File]::ReadLines($settingsPath)){$p=$line.IndexOf(':');if($p -gt 0){$settings[$line.Substring(0,$p)]=$line.Substring($p+1)}}
 $b=[System.Data.SqlClient.SqlConnectionStringBuilder]::new($settings['SqlConStr'])
 $b['Initial Catalog']='CPOS_ACCOUNTING';$b['Application Name']='CPOS Audit3C Upgrade'
 $cn=[System.Data.SqlClient.SqlConnection]::new($b.ConnectionString)
 try{
  $cn.Open();$cmd=$cn.CreateCommand()
  $cmd.CommandText="SELECT o.name,m.definition FROM sys.objects o JOIN sys.sql_modules m ON m.object_id=o.object_id WHERE o.schema_id=SCHEMA_ID('dbo') AND o.name IN('ST_Balance_TMP1_Plus_V','ST_Balance_TMP1_Negitave_V','ST_Balance_TMP_Plus_Negitave_V','IM_full_movement_tmp_V','VW_InventoryTransactionFlow','ST_QTY_Deferences_Details_Delete','Network_Audit_Request_Begin','Network_Audit_Event_Write')"
  $snapshot=[System.Data.DataTable]::new('Definitions');$snapshot.Load($cmd.ExecuteReader())
  if($snapshot.Rows.Count -ne 8){throw 'Expected eight baseline definitions.'}
  $path=Join-Path $output ('Before_'+[guid]::NewGuid().ToString('N')+'.xml')
  $snapshot.WriteXml($path,[System.Data.XmlWriteMode]::WriteSchema)
  $cmd=$cn.CreateCommand();$cmd.CommandTimeout=90
  $cmd.CommandText=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\006_Settlement_Header_And_Stock.sql'))
  [void]$cmd.ExecuteNonQuery()
  "Installed 006 without activation. Before snapshot: $path"
 }finally{$cn.Dispose()}
}
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSecuritySession.vb',
 'Classes\Audit\AuditSettlementLines.vb','Classes\Audit\AuditSettlementHeader.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources += @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | ForEach-Object { $_.FullName })
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditSettlementHeaderTests.vb'
$exe=Join-Path $output 'AuditSettlementHeaderTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditSettlementHeaderTests `
 /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Header tests did not compile.'}
$arguments=@();if($LiveRollbackTests){$arguments=@('--settings',$settingsPath)}
& $exe $arguments
if($LASTEXITCODE -ne 0){throw 'Phase3C tests failed; fixture work is enclosed in rollback transactions.'}
