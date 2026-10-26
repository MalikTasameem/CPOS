param([switch]$CandidateRollback,[switch]$InstallCorrection)
$ErrorActionPreference='Stop'
if($CandidateRollback -and $InstallCorrection){throw 'Choose rehearsal OR installation.'}
if(Get-Process -Name CPOS -ErrorAction SilentlyContinue){throw 'Close CPOS normally; no process is killed.'}
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$setting=([IO.File]::ReadAllLines((Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings')) | Where-Object {$_.StartsWith('SqlConStr:')}).Substring(10)
$builder=[System.Data.SqlClient.SqlConnectionStringBuilder]::new($setting)
if($builder.InitialCatalog -ne 'CPOS_ACCOUNTING'){throw 'Unexpected database'}
$builder['Application Name']='CPOS Components015 Rollback';$builder['Pooling']=$false
$cn=[System.Data.SqlClient.SqlConnection]::new($builder.ConnectionString)
$migration=[IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\015_Sales_Component_Lifecycle.sql'))
$bill=2147482400;$recipe=2147482401
$checks=0
function Q([string]$sql,$tx=$null){
 $cmd=$cn.CreateCommand();$cmd.CommandText=$sql;$cmd.Transaction=$tx;$cmd.CommandTimeout=30
 try{$cmd.ExecuteScalar()}finally{$cmd.Dispose()}
}
function Check([bool]$ok,[string]$label){if(-not $ok){throw "FAILED: $label"};$script:checks++;"PASS $label"}
function Financial($tx=$null){[string](Q "SELECT CONVERT(nvarchar(max),(SELECT (SELECT * FROM dbo.Agents_Balance_MV_RCT ORDER BY T_ID FOR XML PATH('Receipt'),TYPE),(SELECT * FROM dbo.Treasury_Balance_MV ORDER BY T_ID FOR XML PATH('Movement'),TYPE) FOR XML PATH('State'),TYPE))" $tx)}
function Stock($tx){[decimal](Q "SELECT ISNULL(SUM(QTY),0) FROM dbo.ST_Balance_V WHERE IM_ID=2 AND ST_ID=1 AND D_Vaild=N''" $tx)}
function Add-Sale($tx,[int]$path,[int]$edit=0,[int]$qty=2,[int]$newRow=1){
 if($path -eq 1){[void](Q "EXEC dbo.SB_Contents_INSERT @SB_T_ID=$bill,@IM_ID=1,@QYT=$qty,@ST_ID=1,@U_IM_ID=1,@On_Update=$edit,@Barcode=N'component-test',@IM_Price=55" $tx)}
 else{[void](Q "EXEC dbo.SB_Contents_INSERT_2 @SB_T_ID=$bill,@IM_ID=1,@QYT=$qty,@ST_ID=1,@U_ID=2,@Price=55,@On_Update=$edit,@Barcode=N'component-test',@SB_IM_NEW_ROW=$newRow" $tx)}
}
function Line-Id($tx){[int](Q "SELECT MIN(T_ID) FROM dbo.SB_Contents WHERE Bill_T_ID=$bill" $tx)}
function Check-Components($tx,[int]$line,[decimal]$qty,[string]$label){
 Check ((Q "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=$line AND Struct_ID=2 AND ST_ID=1 AND QTY=$qty AND ST_QTY=$qty AND U_Cargo=1 AND isDepended=1 AND Cost IS NULL" $tx) -eq 1 -and (Q "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=$line" $tx) -eq 1) $label
}
function Run-Case([string]$label,[scriptblock]$body){
 $tx=$cn.BeginTransaction([System.Data.IsolationLevel]::Serializable)
 try{
  if($CandidateRollback){[void](Q $migration $tx)}
  [void](Q "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,SB_ID,NeedRepost,S_Bills_Type,Order_isDeleverd,Pr_ID,TB_isOrderd) VALUES($bill,1,GETDATE(),1,0,0,1,0,0,0,$bill,0,1,0,1,0); INSERT dbo.IM_Struct_Menu(T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES($recipe,1,1,2,2,2,1,1)" $tx)
  & $body $tx
 }finally{try{if($tx.Connection){$tx.Rollback()}}finally{$tx.Dispose()}}
 Check ((Financial) -eq $script:financialBefore) "$label financial rollback"
 Check (([string](Q $script:countsSql)) -eq $script:countsBefore) "$label fixture rollback"
}
try{
 $cn.Open()
 Check ((Q 'SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()') -eq 0) 'no other database clients'
 Check ((Q "SELECT COUNT(*) FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c'") -eq 1) 'approved source'
 Check ((Q "SELECT (SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=$bill)+(SELECT COUNT(*) FROM dbo.IM_Struct_Menu WHERE IM_ID=1 OR T_ID=$recipe)") -eq 0) 'fixture header and recipe unused'
 Check ((Q 'SELECT COUNT(*) FROM dbo.IM_Menu_Units_V WHERE IM_ID=1 AND U_ID=2 AND U_IM_ID=1 AND U_Cargo=1 AND Cost=50') -eq 1) 'known unit and merge cost'
 Check ((Q "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('dbo.SB_Contents'),OBJECT_ID('dbo.SB_Contents_Structers'),OBJECT_ID('dbo.IM_Struct_Menu'),OBJECT_ID('dbo.Agents_Balance_MV'),OBJECT_ID('dbo.Periods'))") -eq 0) 'no unreviewed business triggers'
 $changed=@('SB_Contents_INSERT','SB_Contents_INSERT_2','SB_Contents_Delete_IM','SB_Contents_Structers_insert','SB_Contents_Structers_insert_ONE')
 $unchanged=@('SB_ConfermBill','SB_ConfermBill_V2','Deliver_Order','AG_Balance_Update_isDepended','AG_Balance_Update_Total','AG_Balance_Update_Pied_On_Update','Recalculate_Agent_Bill_Total','Inc_IM_QTY','Dec_IM_QTY','SB_Contents_Change_IM_Qty')
 $definitions=@{};foreach($name in ($changed+$unchanged)){$definitions[$name]=[string](Q "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.$name'))")}
 if($InstallCorrection){
  $output=Join-Path $projectRoot '..\.tmp\AuditComponents015';New-Item -ItemType Directory -Path $output -Force | Out-Null
  $cmd=$cn.CreateCommand();$cmd.CommandText="SELECT name,OBJECT_DEFINITION(object_id) AS Definition FROM sys.procedures WHERE name IN('"+(($changed+$unchanged)-join "','")+"')"
  $data=[System.Data.DataTable]::new('Definitions');$data.Load($cmd.ExecuteReader());$cmd.Dispose()
  if($data.Rows.Count -ne 15){throw 'Unexpected snapshot count'}
  $data.WriteXml((Join-Path $output ('Before_'+[guid]::NewGuid().ToString('N')+'.xml')),[System.Data.XmlWriteMode]::WriteSchema)
  [void](Q $migration)
  'Installed 015 atomically; no historical recalculation.'
 }
 if(-not $CandidateRollback){foreach($name in $changed){Check (([string](Q "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.$name'))")).Contains('AuditComponents015')) "installed $name"}}
 $financialBefore=Financial
 $tables=@('Agents_Balance_MV','SB_Contents','SB_Contents_Structers','IM_Struct_Menu','Agents_Balance_MV_RCT','Treasury_Balance_MV','TB_AutoPrint_tmp','Costmer_Screen','Network_Edit_Tracker','Network_Audit_Request','Network_Audit_Event','Network_Audit_Change')
 $countsSql='SELECT '+(($tables | ForEach-Object {"(SELECT COUNT_BIG(*) FROM dbo.$_) AS [$_]"}) -join ',')+" FOR XML PATH('Counts')"
 $countsBefore=[string](Q $countsSql)
 $periodsBefore=[string](Q "SELECT * FROM dbo.Periods ORDER BY Pr_ID FOR XML PATH('Period')")
 foreach($path in 1,2){foreach($edit in 0,1){
  Run-Case "add path $path edit $edit" {
   param($tx)
   if($edit -eq 1){[void](Q "UPDATE dbo.Agents_Balance_MV SET isDepended=1 WHERE T_ID=$bill" $tx)}
   $stockBefore=Stock $tx;Add-Sale $tx $path $edit;$line=Line-Id $tx
   Check-Components $tx $line 4 'new line components created once'
   Check (((Stock $tx)-$stockBefore) -eq -4) 'immediate component stock delta'
   [void](Q "EXEC dbo.SB_Contents_Structers_insert_ONE $line; EXEC dbo.SB_Contents_Structers_insert $bill; EXEC dbo.SB_Contents_Structers_Recount $bill" $tx)
   Check-Components $tx $line 4 'repeated single/full/recount does not append'
   Check (((Stock $tx)-$stockBefore) -eq -4) 'rebuild does not duplicate stock effect'
   Check ((Financial $tx) -eq $financialBefore) 'add and rebuild do not alter payments'
  }
 }}
 foreach($edit in 0,1){Run-Case "merge edit $edit" {
  param($tx)
  $stockBefore=Stock $tx;Add-Sale $tx 2 $edit;$line=Line-Id $tx
  Add-Sale $tx 2 $edit 3 0
  Check ((Q "SELECT COUNT(*) FROM dbo.SB_Contents WHERE Bill_T_ID=$bill" $tx) -eq 1 -and (Q "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=$line" $tx) -eq 5) 'merge keeps the actual line ID and increases its quantity'
  Check-Components $tx $line 10 'merged quantity replaces old components'
  Check (((Stock $tx)-$stockBefore) -eq -10) 'merged stock effect is final quantity only'
  [void](Q "EXEC dbo.SB_Contents_Delete_IM @T_ID=$line,@On_Update=$edit" $tx)
  Check ((Q "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=$line" $tx) -eq 0) 'delete always removes structures'
  Check ((Stock $tx) -eq $stockBefore) 'delete restores component stock'
  Check ((Financial $tx) -eq $financialBefore) 'merge/delete preserve payments'
 }}
 foreach($version in 1,2){Run-Case "confirm V$version" {
  param($tx)
  $stockBefore=Stock $tx;Add-Sale $tx 2;$line=Line-Id $tx
  if($version -eq 1){[void](Q "EXEC dbo.SB_ConfermBill @T_ID=$bill,@TOTAL=110,@Discount=110,@Pure=0,@Pied=0,@AGType_ID=1,@Tr_ID=1,@Pr_ID=1,@User_ID=1" $tx)}
  else{[void](Q "DECLARE @p dbo.SalePaymentAllocationType; EXEC dbo.SB_ConfermBill_V2 @T_ID=$bill,@TOTAL=110,@Discount=110,@Pure=0,@Pied=0,@AGType_ID=1,@Pr_ID=1,@User_ID=1,@Payments=@p" $tx)}
  Check-Components $tx $line 4 'actual confirmation preserves one recipe set'
  Check (((Stock $tx)-$stockBefore) -eq -4) 'confirmation does not consume components twice'
  Check ((Q "SELECT isDepended FROM dbo.Agents_Balance_MV WHERE T_ID=$bill" $tx) -eq 1) 'invoice confirmed by original procedure'
  Check ((Financial $tx) -eq $financialBefore) 'zero-payment fixture creates no financial movement'
 }}
 foreach($path in 1,2){Run-Case "order path $path" {
  param($tx)
  [void](Q "UPDATE dbo.Agents_Balance_MV SET S_Bills_Type=3 WHERE T_ID=$bill" $tx)
  $stockBefore=Stock $tx;Add-Sale $tx $path;$line=Line-Id $tx
  [void](Q "EXEC dbo.SB_Contents_Structers_insert $bill" $tx)
  Check ((Q "SELECT isDepended FROM dbo.SB_Contents WHERE T_ID=$line" $tx) -eq 0) 'undelivered order line not depended'
  Check ((Q "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=$line" $tx) -eq 0 -and (Stock $tx) -eq $stockBefore) 'undelivered order creates no component stock effect'
  [void](Q "EXEC dbo.Deliver_Order @T_ID=$bill,@Order_isDeleverd=1,@User_ID=1,@Pr_ID=1,@Tr_ID=1" $tx)
  Check-Components $tx $line 4 'actual delivery creates components'
  [void](Q "EXEC dbo.SB_Contents_Structers_insert $bill" $tx)
  Check (((Stock $tx)-$stockBefore) -eq -4) 'delivery/full rebuild affects stock once'
  Add-Sale $tx $path 1 1 1
  Check ((Q "SELECT COUNT(*) FROM dbo.SB_Contents WHERE Bill_T_ID=$bill AND isDepended=1" $tx) -eq 2) 'addition to a delivered order stays stock-active'
  Check (((Stock $tx)-$stockBefore) -eq -6) 'delivered order addition adds only its own component quantity'
 }}
 Run-Case 'inactive and void' {
  param($tx)
  $stockBefore=Stock $tx;Add-Sale $tx 2;$line=Line-Id $tx
  [void](Q "UPDATE dbo.SB_Contents SET isDepended=0 WHERE T_ID=$line; EXEC dbo.SB_Contents_Structers_insert_ONE $line" $tx)
  Check ((Q "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=$line" $tx) -eq 0 -and (Stock $tx) -eq $stockBefore) 'inactive line removes stale structures'
  [void](Q "UPDATE dbo.SB_Contents SET isDepended=1 WHERE T_ID=$line; EXEC dbo.SB_Contents_Structers_insert_ONE $line; UPDATE dbo.Agents_Balance_MV SET isVoid=1 WHERE T_ID=$bill; EXEC dbo.SB_Contents_Structers_insert $bill" $tx)
  Check ((Q "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=$line" $tx) -eq 0 -and (Stock $tx) -eq $stockBefore) 'void invoice does not rebuild active structures'
 }
 foreach($path in 1,2){Run-Case "injected component failure path $path" {
  param($tx)
  [void](Q "CREATE TRIGGER dbo.AuditTest015RejectStructure ON dbo.SB_Contents_Structers AFTER INSERT AS BEGIN THROW 51116,'Injected component lifecycle failure',1; END" $tx)
  $failed=$false
  try{Add-Sale $tx $path}catch{if($_.Exception.Message -notlike '*Injected component lifecycle failure*'){throw};$failed=$true}
  Check $failed 'component failure propagates to caller, not silent success'
 }}
 foreach($name in $unchanged){Check (([string](Q "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.$name'))")) -eq $definitions[$name]) "unchanged procedure $name"}
 if($CandidateRollback){foreach($name in $changed){Check (([string](Q "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.$name'))")) -eq $definitions[$name]) "candidate DDL rolled back $name"}}
 Check (([string](Q "SELECT * FROM dbo.Periods ORDER BY Pr_ID FOR XML PATH('Period')")) -eq $periodsBefore) 'period counters restored'
 Check ((Q "SELECT COUNT(*) FROM sys.triggers WHERE name='AuditTest015RejectStructure'") -eq 0) 'failure trigger removed by rollback'
 Check ((Q 'SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1') -eq 0) 'no incomplete audit requests'
 "PASS $checks component lifecycle checks; all fixtures rolled back."
}finally{$cn.Dispose()}
