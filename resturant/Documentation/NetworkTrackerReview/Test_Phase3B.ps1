param([switch]$InstallPrerequisites, [switch]$LiveRollbackTests, [switch]$ReadOnlyPolicyTests)
$ErrorActionPreference='Stop'
if($ReadOnlyPolicyTests -and ($InstallPrerequisites -or $LiveRollbackTests)){throw 'Read-only policy checks cannot be combined with database writes.'}
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output=Join-Path $projectRoot '..\.tmp\AuditPhase3B'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$sources=@('Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb','Classes\Audit\AuditSecuritySession.vb','Classes\Audit\AuditSettlementLines.vb') |
 ForEach-Object {Join-Path $projectRoot $_}
$sources += @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | ForEach-Object { $_.FullName })
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources+=Join-Path $PSScriptRoot 'AuditSettlementTests.vb'
$exe=Join-Path $output 'AuditSettlementTests.exe'
& 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe' /nologo /target:exe /optionstrict+ /imports:System /main:AuditSettlementTests `
 /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if($LASTEXITCODE -ne 0){throw 'Settlement tests did not compile.'}
if($InstallPrerequisites){
 $settings=@{}
 foreach($line in [IO.File]::ReadLines((Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings'))){
  $p=$line.IndexOf(':');if($p -gt 0){$settings[$line.Substring(0,$p)]=$line.Substring($p+1)}
 }
 $builder=[System.Data.SqlClient.SqlConnectionStringBuilder]::new($settings['SqlConStr'])
 $builder['Initial Catalog']='CPOS_ACCOUNTING';$builder['Application Name']='CPOS Audit3B Upgrade'
 $connection=[System.Data.SqlClient.SqlConnection]::new($builder.ConnectionString)
 try{
  $connection.Open()
  $snapshot=[System.Data.DataTable]::new('ProcedureDefinitions')
  $command=$connection.CreateCommand()
  $command.CommandText="SELECT o.name, m.definition FROM sys.sql_modules m JOIN sys.objects o ON o.object_id=m.object_id WHERE o.schema_id=SCHEMA_ID(N'dbo') AND o.name IN('ST_Balance_Update','ST_QTY_Deferences_Details_Delete','Network_Audit_Request_Begin','Network_Audit_Event_Write')"
  $snapshot.Load($command.ExecuteReader())
  if($snapshot.Rows.Count -ne 4){throw 'Expected four existing procedure definitions.'}
  $backupPath=Join-Path $output ('Before_'+[guid]::NewGuid().ToString('N')+'.xml')
  $snapshot.WriteXml($backupPath,[System.Data.XmlWriteMode]::WriteSchema)
  $command=$connection.CreateCommand();$command.CommandTimeout=60
  $command.CommandText=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\005_Settlement_Line_Prerequisites.sql'))
  [void]$command.ExecuteNonQuery()
  Write-Output "Upgraded four procedures without enabling settlement auditing. Before snapshot: $backupPath"
 }finally{$connection.Dispose()}
}
$arguments=@()
if($LiveRollbackTests){$arguments=@('--settings',(Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings'))}
if($ReadOnlyPolicyTests){$arguments=@('--read-only-policy',(Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings'))}
& $exe $arguments
if($LASTEXITCODE -ne 0){throw 'Settlement checks failed. Read-only policy checks do not write to the database; live business fixtures use rollback transactions.'}
