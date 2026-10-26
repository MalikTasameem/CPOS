/* Phase 3B (line add/delete only), SQL Server 2014, approved test DB only.
   Targeted upgrades; no table changes, no accounting formula changes, no activation.
   Save OBJECT_DEFINITION of the four procedures before installation (test runner does this).
   Refuses already-upgraded or unexpected definitions. Not a repeatable blanket overwrite.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51102,'Select approved CPOS_ACCOUNTING test database.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max),@new nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=10000;
 IF @lock<0 THROW 51103,'Audit deployment is busy.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.Settlement"]')=1)
    THROW 51102,'Disable settlement pilot before upgrading.',1;

 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.ST_Balance_Update'));
 IF @sql IS NULL OR CHARINDEX(N'@Inserted_T_ID',@sql)>0 OR CHARINDEX(N'@Barcode nvarchar(50)',@sql)=0
    THROW 51102,'Unexpected/already upgraded ST_Balance_Update.',1;
 SET @sql=REPLACE(@sql,N'CREATE PROCEDURE',N'ALTER PROCEDURE');
 SET @sql=REPLACE(@sql,N'@Barcode nvarchar(50)',N'@Barcode nvarchar(50), @Inserted_T_ID int = NULL OUTPUT');
 SET @old=N'BEGIN TRANSACTION';
 SET @new=N'BEGIN TRANSACTION
 SET @Inserted_T_ID=NULL;
 DECLARE @LockedBill int;
 SELECT @LockedBill=T_ID FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK)
 WHERE T_ID=@Bill_T_ID AND BsType_ID=39 AND isVoid=0;
 IF @LockedBill IS NULL THROW 51106,''Settlement document is missing, void or of another type.'',1;
 IF @Qty_Deference<0 OR @Tag NOT IN(''+'',''-'','' '') OR (@Tag='' '' AND @Qty_Deference<>0)
    THROW 51106,''Invalid settlement difference/sign.'',1;
 IF @Qty_Deference=0 BEGIN COMMIT; RETURN; END;';
 IF (LEN(@sql)-LEN(REPLACE(@sql,@old,N'')))/LEN(@old)<>1 THROW 51102,'Unexpected transaction body.',1;
 SET @sql=REPLACE(@sql,@old,@new);
 SET @old=N'EXEC @T_ID = [AA_GET_MAX_ID] ''ST_QTY_Deferences''';
 SET @new=N'-- Serialize allocation in this table only; AA_GET_MAX_ID is unchanged.
 DECLARE @LockedLine int,@FirstId int,@LastId int;
 SELECT TOP(1) @LockedLine=T_ID FROM dbo.ST_QTY_Deferences WITH(TABLOCKX,HOLDLOCK);
 IF (SELECT COUNT(*) FROM dbo.SERVERS)<>1 THROW 51106,''Ambiguous local ID range.'',1;
 SELECT @FirstId=START_ID,@LastId=END_ID FROM dbo.SERVERS;
 EXEC @T_ID = [AA_GET_MAX_ID] ''ST_QTY_Deferences'';
 IF @T_ID IS NULL OR @FirstId IS NULL OR @LastId IS NULL OR @T_ID NOT BETWEEN @FirstId AND @LastId
    THROW 51106,''Settlement line ID range exhausted or invalid.'',1;';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected settlement allocator.',1;
 SET @sql=REPLACE(@sql,@old,@new);
 SET @old=N'EXEC Recalculate_Agent_Bill_Total @Bill_T_ID';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected settlement total call.',1;
 SET @sql=REPLACE(@sql,@old,N'SET @Inserted_T_ID=@T_ID;
 EXEC Recalculate_Agent_Bill_Total @Bill_T_ID');
 EXEC sys.sp_executesql @sql;

 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.ST_QTY_Deferences_Details_Delete'));
 IF @sql IS NULL OR CHARINDEX(N'@ExpectedBillId',@sql)>0 THROW 51102,'Unexpected/already upgraded settlement delete.',1;
 SET @sql=REPLACE(@sql,N'CREATE PROCEDURE',N'ALTER PROCEDURE');
 SET @old=N'@T_ID'+NCHAR(9)+N'int';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected delete parameter declaration.',1;
 SET @sql=REPLACE(@sql,@old,N'@T_ID int, @ExpectedBillId int=NULL, @Deleted bit=0 OUTPUT');
 SET @old=N'SELECT @Bill_T_ID = Bill_T_ID FROM ST_QTY_Deferences WHERE Bill_T_ID = @Bill_T_ID';
 SET @new=N'SET @Deleted=0;
 SELECT @Bill_T_ID = Bill_T_ID FROM dbo.ST_QTY_Deferences WHERE T_ID=@T_ID;
 IF @Bill_T_ID IS NULL BEGIN COMMIT; RETURN; END;
 IF @ExpectedBillId IS NOT NULL AND @ExpectedBillId<>@Bill_T_ID THROW 51106,''Line belongs to another settlement.'',1;
 DECLARE @LockedBill int;
 SELECT @LockedBill=T_ID FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK)
 WHERE T_ID=@Bill_T_ID AND BsType_ID=39 AND isVoid=0;
 IF @LockedBill IS NULL THROW 51106,''Settlement document is missing, void or of another type.'',1;';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Delete baseline differs; review before upgrade.',1;
 SET @sql=REPLACE(@sql,@old,@new);
 SET @sql=REPLACE(@sql,N'DELETE FROM ST_QTY_Deferences WHERE T_ID = @T_ID',N'DELETE FROM ST_QTY_Deferences WHERE T_ID=@T_ID AND Bill_T_ID=@Bill_T_ID;
 SET @Deleted=CASE WHEN @@ROWCOUNT=1 THEN 1 ELSE 0 END;');
 EXEC sys.sp_executesql @sql;

 -- Expand the existing closed writer catalogue. Preserve its request/identity rules.
 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Request_Begin'));
 SET @old=N'@OperationCode=''Settlement.LineAdded''';
 IF @sql IS NULL OR CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected request writer catalogue.',1;
 SET @sql=REPLACE(REPLACE(@sql,N'CREATE PROCEDURE',N'ALTER PROCEDURE'),@old,N'@OperationCode IN(''Settlement.LineAdded'',''Settlement.LineDeleted'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL OR CHARINDEX(N'@operation=''Settlement.LineAdded''',@sql)=0 THROW 51102,'Unexpected event writer catalogue.',1;
 SET @sql=REPLACE(@sql,N'CREATE PROCEDURE',N'ALTER PROCEDURE');
 SET @sql=REPLACE(@sql,N'@operation=''Settlement.LineAdded'' AND @domain',N'@operation IN(''Settlement.LineAdded'',''Settlement.LineDeleted'') AND @domain');
 SET @old=N'(@operation=''Settlement.LineAdded'' AND ((FieldCode IN(''ItemId'',''StoreId'',''UnitId'') AND ValueType=''Integer'') OR
    (FieldCode IN(''QuantityDelta'',''Cost'',''Total'') AND ValueType=''Decimal'') OR (FieldCode=''StoredQuantity'' AND ValueType=''Float'') OR (FieldCode=''ExpiryDate'' AND ValueType=''DateTime'')))';
 -- Writer was created as a single LF-based batch. Normalize CRLF only for this exact catalogue check.
 SET @sql=REPLACE(@sql,NCHAR(13)+NCHAR(10),NCHAR(10));
 SET @old=REPLACE(@old,NCHAR(13)+NCHAR(10),NCHAR(10));
 SET @new=N'(@operation IN(''Settlement.LineAdded'',''Settlement.LineDeleted'') AND (
    (FieldCode IN(''ItemId'',''StoreId'',''UnitId'',''LineId'') AND ValueType=''Integer'') OR
    (FieldCode IN(''QuantityDelta'',''Cost'',''Total'',''UnitCargo'',''RequestedQuantity'',''DocumentTotal'',''DocumentPure'') AND ValueType=''Decimal'') OR
    (FieldCode IN(''StoredQuantity'',''DisplayedQuantity'') AND ValueType=''Float'') OR
    (FieldCode IN(''ExpiryDate'',''CreatedAt'') AND ValueType=''DateTime'') OR
    (FieldCode IN(''ExpiryText'',''Barcode'') AND ValueType=''String'')))';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected event field catalogue.',1;
 SET @sql=REPLACE(@sql,@old,@new);
 SET @old=N'IF @operation=''Settlement.LineAdded'' AND EXISTS(SELECT 1 FROM @changes WHERE BeforeState<>1 OR AfterState NOT IN(2,3))';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected line value-state validation.',1;
 SET @sql=REPLACE(@sql,@old,N'IF @operation=''Settlement.LineAdded'' AND EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SettlementLine'' AND (BeforeState<>1 OR AfterState NOT IN(2,3)))');
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 SET @sql=REPLACE(@sql,@old,N'IF @operation=''Settlement.LineDeleted'' AND EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SettlementLine'' AND (BeforeState NOT IN(2,3) OR AfterState<>1))
    THROW 51101,''Line deletion must capture before and absent-after.'',1;
 INSERT dbo.Network_Audit_Event(EventId');
 EXEC sys.sp_executesql @sql;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
