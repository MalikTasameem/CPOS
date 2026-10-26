param([switch]$InstallCorrection)
$ErrorActionPreference='Stop'
if(Get-Process -Name CPOS -ErrorAction SilentlyContinue){throw 'Close CPOS normally before database tests; none will be killed.'}
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$setting=([IO.File]::ReadAllLines((Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings')) | Where-Object {$_.StartsWith('SqlConStr:')}).Substring(10)
$builder=[System.Data.SqlClient.SqlConnectionStringBuilder]::new($setting)
if($builder.InitialCatalog -ne 'CPOS_ACCOUNTING'){throw 'Unexpected database'}
$builder['Application Name']='CPOS InvoiceTarget Rollback';$builder['Pooling']=$false
$cn=[System.Data.SqlClient.SqlConnection]::new($builder.ConnectionString)
function Read-Scalar([string]$sql,$tx=$null){
 $cmd=$cn.CreateCommand();$cmd.CommandText=$sql;$cmd.Transaction=$tx;$cmd.CommandTimeout=30
 try{$cmd.ExecuteScalar()}finally{$cmd.Dispose()}
}
function Assert-Check([bool]$ok,[string]$label){if(-not $ok){throw "FAILED: $label"};"PASS $label"}
try{
 $cn.Open()
 Assert-Check ((Read-Scalar 'SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()') -eq 0) 'no other database clients'
 Assert-Check ((Read-Scalar "SELECT COUNT(*) FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c'") -eq 1) 'approved source'
 $original=[string](Read-Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.AG_Balance_DependingUpdatedBill'))")
 if($InstallCorrection){
  $output=Join-Path $projectRoot '..\.tmp\AuditInvoiceTarget'
  New-Item -ItemType Directory -Path $output -Force | Out-Null
  $cmd=$cn.CreateCommand();$cmd.CommandText="SELECT name,OBJECT_DEFINITION(object_id) AS Definition FROM sys.procedures WHERE name IN('AG_Balance_DependingUpdatedBill','AG_Balance_Update_Total','Recalculate_Agent_Bill_Total')"
  $data=[System.Data.DataTable]::new('Definitions');$data.Load($cmd.ExecuteReader());$cmd.Dispose()
  $data.WriteXml((Join-Path $output ('Before_'+[guid]::NewGuid().ToString('N')+'.xml')),[System.Data.XmlWriteMode]::WriteSchema)
  $cmd=$cn.CreateCommand();$cmd.CommandText=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\013_Fix_DependingUpdatedBill_Target.sql'))
  [void]$cmd.ExecuteNonQuery();$cmd.Dispose()
  $actual=[string](Read-Scalar "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.AG_Balance_DependingUpdatedBill'))")
  $expected=$original.Replace('CREATE PROCEDURE','ALTER PROCEDURE').Replace('SELECT @T_ID = Bill_T_ID FROM SB_Contents WHERE T_ID = @T_ID','-- AuditTarget013: @T_ID is the invoice ID supplied by the caller; never reinterpret it as a sales-line ID.')
  Assert-Check ($actual.Replace('ALTER PROCEDURE','CREATE PROCEDURE') -eq $expected.Replace('ALTER PROCEDURE','CREATE PROCEDURE')) 'only approved target statement changed'
 }
 Assert-Check ((Read-Scalar "SELECT CHARINDEX('AuditTarget013',OBJECT_DEFINITION(OBJECT_ID('dbo.AG_Balance_DependingUpdatedBill')))") -gt 0) 'target correction installed'
 $baselineFinancial=[string](Read-Scalar "SELECT CONVERT(nvarchar(max),(SELECT (SELECT * FROM dbo.Agents_Balance_MV_RCT ORDER BY T_ID FOR XML PATH('Receipt'),TYPE),(SELECT * FROM dbo.Treasury_Balance_MV ORDER BY T_ID FOR XML PATH('Movement'),TYPE) FOR XML PATH('State'),TYPE))")
 $baselineCounts=[string](Read-Scalar "SELECT (SELECT COUNT_BIG(*) FROM dbo.Agents_Balance_MV) AS Headers,(SELECT COUNT_BIG(*) FROM dbo.SB_Contents) AS Lines,(SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Event) AS Events FOR XML PATH('Counts')")
 Assert-Check ((Read-Scalar 'SELECT (SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID BETWEEN 2147482200 AND 2147482202)+(SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID BETWEEN 2147482200 AND 2147482210)') -eq 0) 'fixture IDs unused'
 Assert-Check ((Read-Scalar "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('dbo.Agents_Balance_MV'),OBJECT_ID('dbo.SB_Contents'))") -eq 0) 'no unreviewed business triggers'
 $tx=$cn.BeginTransaction([System.Data.IsolationLevel]::Serializable)
 try{
  [void](Read-Scalar "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,SB_ID,NeedRepost) VALUES(2147482200,1,GETDATE(),1,0,0,1,777,5,772,2147482200,0),(2147482201,1,GETDATE(),1,0,0,1,999,0,999,2147482201,0); INSERT dbo.SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Cost,Price,QTY,T_Price,U_Cargo,ST_QTY,isDepended,Saler_Percent) VALUES(2147482200,2147482201,1,2,1,GETDATE(),2,10,2,20,1,2,1,0),(2147482202,2147482200,1,2,1,GETDATE(),2,55,2,110,1,2,1,0)" $tx)
  [void](Read-Scalar 'EXEC dbo.AG_Balance_DependingUpdatedBill @T_ID=2147482200' $tx)
  Assert-Check ((Read-Scalar 'SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=2147482200 AND Total=110 AND Pure=105 AND Discount=5' $tx) -eq 1) 'requested invoice recalculated despite colliding line ID'
  Assert-Check ((Read-Scalar 'SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=2147482201 AND Total=999 AND Pure=999' $tx) -eq 1) 'other invoice remains untouched'
  [void](Read-Scalar 'EXEC dbo.AG_Balance_DependingUpdatedBill @T_ID=2147482201' $tx)
  Assert-Check ((Read-Scalar 'SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=2147482201 AND Total=20 AND Pure=20' $tx) -eq 1) 'invoice without matching line ID recalculated'
  Assert-Check ((Read-Scalar 'SELECT COUNT(*) FROM dbo.SB_Contents WHERE (T_ID=2147482200 AND Bill_T_ID=2147482201 AND T_Price=20) OR (T_ID=2147482202 AND Bill_T_ID=2147482200 AND T_Price=110)' $tx) -eq 2) 'sales lines unchanged'
  Assert-Check (([string](Read-Scalar "SELECT CONVERT(nvarchar(max),(SELECT (SELECT * FROM dbo.Agents_Balance_MV_RCT ORDER BY T_ID FOR XML PATH('Receipt'),TYPE),(SELECT * FROM dbo.Treasury_Balance_MV ORDER BY T_ID FOR XML PATH('Movement'),TYPE) FOR XML PATH('State'),TYPE))" $tx)) -eq $baselineFinancial) 'receipt and treasury values unchanged'
 }finally{if($tx.Connection){$tx.Rollback()};$tx.Dispose()}
 Assert-Check (([string](Read-Scalar "SELECT (SELECT COUNT_BIG(*) FROM dbo.Agents_Balance_MV) AS Headers,(SELECT COUNT_BIG(*) FROM dbo.SB_Contents) AS Lines,(SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Event) AS Events FOR XML PATH('Counts')")) -eq $baselineCounts) 'fixture rollback restored counts'
 'PASS invoice-target correction tests; no historical invoices recalculated.'
}finally{$cn.Dispose()}
