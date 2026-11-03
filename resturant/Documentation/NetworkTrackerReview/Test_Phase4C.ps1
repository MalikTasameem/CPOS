param([switch]$InstallPrerequisites,[switch]$LiveRollbackTests,[switch]$CandidateDetailComponents)
$ErrorActionPreference='Stop'
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditPhase4C'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$settingsPath=Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings'
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSalesEdit.vb','Classes\Audit\AuditSalesLineEdit.vb','Classes\Audit\AuditSalesQuantity.vb') | ForEach-Object {Join-Path $projectRoot $_}
$sources += @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | ForEach-Object { $_.FullName })
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditSalesLineEditTests.vb'
$exe=Join-Path $output 'AuditSalesLineEditTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditSalesLineEditTests `
 /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Sales line tests did not compile.'}
if($InstallPrerequisites){
 $settings=@{}
 foreach($line in [IO.File]::ReadLines($settingsPath)){$p=$line.IndexOf(':');if($p -gt 0){$settings[$line.Substring(0,$p)]=$line.Substring($p+1)}}
 $b=[System.Data.SqlClient.SqlConnectionStringBuilder]::new($settings['SqlConStr'])
 if($b.InitialCatalog -ne 'CPOS_ACCOUNTING'){throw 'Unexpected database.'}
 $b['Application Name']='CPOS Audit4C Upgrade';$b.Pooling=$false
 $cn=[System.Data.SqlClient.SqlConnection]::new($b.ConnectionString)
 try{
  $cn.Open();$cmd=$cn.CreateCommand()
  $cmd.CommandText="SELECT o.name,m.definition FROM sys.objects o JOIN sys.sql_modules m ON m.object_id=o.object_id WHERE o.schema_id=SCHEMA_ID('dbo') AND o.name IN('Network_Audit_Request_Begin','Network_Audit_Event_Write','SB_IM_Unit_Change','IM_Conferm_NewPrice','AG_Balance_Update_Total','AG_Balance_Update_Pied_On_Update','Recalculate_Agent_Bill_Total','Check_IM_OFFER')"
  $snapshot=[System.Data.DataTable]::new('Definitions');$snapshot.Load($cmd.ExecuteReader())
  if($snapshot.Rows.Count -ne 8){throw 'Expected eight baseline definitions.'}
  $snapshot.WriteXml((Join-Path $output ('Before_'+[guid]::NewGuid().ToString('N')+'.xml')),[System.Data.XmlWriteMode]::WriteSchema)
  $cmd=$cn.CreateCommand();$cmd.CommandTimeout=60
  $cmd.CommandText=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\011_Sales_Line_Editing.sql'))
  [void]$cmd.ExecuteNonQuery()
  'Installed 011 atomically; original business procedures unchanged.'
 }finally{$cn.Dispose()}
}
$arguments=@();if($LiveRollbackTests){$arguments=@('--settings',$settingsPath)}
if($CandidateDetailComponents){
 if(-not $LiveRollbackTests -or $InstallPrerequisites){throw 'Candidate rehearsal requires live rollback only.'}
 $arguments+=@('--candidate',(Join-Path $projectRoot 'Database\Audit\016_Sales_Detail_Components.sql'))
}
& $exe $arguments
if($LASTEXITCODE -ne 0){throw 'Sales line tests failed; live fixtures use rollback.'}
