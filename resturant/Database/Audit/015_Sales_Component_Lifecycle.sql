/* Approved component lifecycle correction. SQL Server 2014; no table/schema or payment changes.
   Install only after snapshot and rollback tests. Does not recalculate historical invoices. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51115,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51115,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@definition nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51115,'Deployment busy.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c') THROW 51115,'Unexpected source.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51115,'Close other database clients.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51115,'Incomplete audit requests.',1;
 IF CHARINDEX('Sales.LineDeleted',OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write')))=0 THROW 51115,'Install audit deletion contract first.',1;
 SET @definition=OBJECT_DEFINITION(OBJECT_ID('dbo.SB_Contents_Delete_IM'));
 IF @definition IS NULL OR DATALENGTH(@definition)>8000 OR CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(8000),@definition)),2)<>'7511A02A0B518DC903AFF1BA7D5F9E1774DC645C679646D8CEB4DAC40558586B'
  THROW 51115,'Unexpected baseline for SB_Contents_Delete_IM; stop and review.',1;
 SET @definition=OBJECT_DEFINITION(OBJECT_ID('dbo.SB_Contents_INSERT'));
 IF @definition IS NULL OR DATALENGTH(@definition)>8000 OR CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(8000),@definition)),2)<>'A124D77FF2CEF871A32D047C971ED0F65CF5D7568289DBA21C3EEAFE2AE9A3E9'
  THROW 51115,'Unexpected baseline for SB_Contents_INSERT; stop and review.',1;
 SET @definition=OBJECT_DEFINITION(OBJECT_ID('dbo.SB_Contents_INSERT_2'));
 IF @definition IS NULL OR DATALENGTH(@definition)>8000 OR CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(8000),@definition)),2)<>'F60BB9D4A39B04A26B707BC5A74425F9675551370DFE01651CEA33E18BD1A84C'
  THROW 51115,'Unexpected baseline for SB_Contents_INSERT_2; stop and review.',1;
 SET @definition=OBJECT_DEFINITION(OBJECT_ID('dbo.SB_Contents_Structers_insert'));
 IF @definition IS NULL OR DATALENGTH(@definition)>8000 OR CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(8000),@definition)),2)<>'F5B0562E70BFB8F411C3CDA54F48F553D2A74DF89DDB44CA6BA5575AF54E396C'
  THROW 51115,'Unexpected baseline for SB_Contents_Structers_insert; stop and review.',1;
 SET @definition=OBJECT_DEFINITION(OBJECT_ID('dbo.SB_Contents_Structers_insert_ONE'));
 IF @definition IS NULL OR DATALENGTH(@definition)>8000 OR CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(8000),@definition)),2)<>'322E90B1FC02E1AFA538996BDF71849BD52B7BE8E04DD2F43F7ADB5D78E7B051'
  THROW 51115,'Unexpected baseline for SB_Contents_Structers_insert_ONE; stop and review.',1;
 SET @definition=N'ALTER PROCEDURE [dbo].[SB_Contents_Structers_insert_ONE]
 @T_ID INT
AS
BEGIN
 SET NOCOUNT ON;
 -- AuditComponents015: replace, never append; follow the actual stock state.
 DECLARE @OwnTransaction bit=CASE WHEN @@TRANCOUNT=0 THEN 1 ELSE 0 END;
 DECLARE @Bill int,@LockedBill int,@IM_ID int,@U_ID int,@ST_ID int,@QYT numeric(18,2),
         @LineDep bit,@Void bit,@BillType int,@Delivered bit,
         @i int,@Component int,@ComponentStore int,@StockQty numeric(18,3);
 BEGIN TRY
  IF @OwnTransaction=1 BEGIN TRANSACTION;
  SELECT @Bill=Bill_T_ID FROM dbo.SB_Contents WHERE T_ID=@T_ID;
  SELECT @LockedBill=T_ID,@Void=isVoid,@BillType=S_Bills_Type,@Delivered=Order_isDeleverd
  FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Bill;
  SELECT @IM_ID=IM_ID,@U_ID=U_ID,@ST_ID=ST_ID,@QYT=QTY,@LineDep=isDepended
  FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@T_ID AND Bill_T_ID=@LockedBill;
  IF @IM_ID IS NULL THROW 51115,''Sales line or parent was not found for component rebuild.'',1;

  EXEC dbo.SB_Contents_Structers_Remove_ONE @T_ID;
  IF ISNULL(@LineDep,0)=1 AND ISNULL(@Void,0)=0
     AND (@BillType IS NULL OR @BillType<>3 OR ISNULL(@Delivered,0)=1)
  BEGIN
   -- Keep the existing quantity precision, recipe/unit mapping and NULL Cost.
   INSERT dbo.SB_Contents_Structers(SB_Cont_T_ID,[DATE],Struct_ID,U_ID,ST_ID,QTY,U_Cargo,ST_QTY,isDepended)
   SELECT @T_ID,GETDATE(),Struct_IM_ID,U_ID,@ST_ID,(Qty*@QYT),U_Cargo,
          (U_Cargo*(Qty*@QYT)),1
   FROM dbo.IM_Struct_Menu_V WHERE IM_ID=@IM_ID AND IM_U_ID=@U_ID;
   SELECT @i=MIN(T_ID) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=@T_ID;
   WHILE @i IS NOT NULL
   BEGIN
    SELECT @Component=Struct_ID,@ComponentStore=ST_ID,@StockQty=ST_QTY
    FROM dbo.SB_Contents_Structers WHERE T_ID=@i;
    EXEC dbo.Dec_IM_QTY @Component,@ComponentStore,N'''',@StockQty;
    SELECT @i=MIN(T_ID) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=@T_ID AND T_ID>@i;
   END
  END
  IF @OwnTransaction=1 COMMIT;
 END TRY
 BEGIN CATCH
  IF @OwnTransaction=1 AND @@TRANCOUNT>0 ROLLBACK;
  THROW;
 END CATCH
END';
 EXEC sys.sp_executesql @definition;
 SET @definition=N'ALTER PROCEDURE [dbo].[SB_Contents_Structers_insert]
 @Bill_T_ID INT
AS
BEGIN
 SET NOCOUNT ON;
 -- AuditComponents015: confirmation/delivery uses the same replacement rule.
 DECLARE @OwnTransaction bit=CASE WHEN @@TRANCOUNT=0 THEN 1 ELSE 0 END;
 DECLARE @LockedBill int,@Line int;
 BEGIN TRY
  IF @OwnTransaction=1 BEGIN TRANSACTION;
  SELECT @LockedBill=T_ID FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Bill_T_ID;
  IF @LockedBill IS NULL THROW 51115,''Sales invoice was not found for component rebuild.'',1;
  SELECT @Line=MIN(T_ID) FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE Bill_T_ID=@Bill_T_ID;
  WHILE @Line IS NOT NULL
  BEGIN
   EXEC dbo.SB_Contents_Structers_insert_ONE @Line;
   SELECT @Line=MIN(T_ID) FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE Bill_T_ID=@Bill_T_ID AND T_ID>@Line;
  END
  IF @OwnTransaction=1 COMMIT;
 END TRY
 BEGIN CATCH
  IF @OwnTransaction=1 AND @@TRANCOUNT>0 ROLLBACK;
  THROW;
 END CATCH
END';
 EXEC sys.sp_executesql @definition;
 SET @definition=N'ALTER PROCEDURE [dbo].[SB_Contents_INSERT]
	@SB_T_ID int,
	@IM_ID int,
	@D_Vaild DATE = NULL,
	@QYT float = 1,
	@ST_ID int,
	@U_IM_ID int,
	@On_Update Bit,
	@Barcode NVARCHAR(50),
	@IM_Price FLOAT = NULL,
	@SALES_TYPES INT = 0
AS
DECLARE @T_ID int,@Price FLOAT ,@T_Price FLOAT,@U_ID int, @ST_QTY NUMERIC(18,2),@U_Cargo NUMERIC(18,2),@Current_QTY Float
,@Cost Float,@Valid_ST NVARCHAR(50),@Total Numeric(18,3),@Bercent NUMERIC(18,2),@NOTES NVARCHAR(200),@AG_ID int,@OFFER_PRICE Float ,@Price_2 FLOAT ,@Price_3 FLOAT,	@is_By_Min_SP	int = 0	,
    @is_By_Min_SP_2	int	= 0


BEGIN TRY
BEGIN TRANSACTION

-- AuditComponents015: serialize changes to this invoice before changing its lines.
SELECT @AG_ID=AG_ID FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@SB_T_ID;
SET @Valid_ST = @D_Vaild
IF @Valid_ST IS NULL 
BEGIN 
SET @Valid_ST ='''' 
END
ELSE
BEGIN
SELECT @Current_QTY = ISNULL(SUM(QTY),0) FROM ST_Balance_V WHERE IM_ID = @IM_ID
IF @Current_QTY <= 0 BEGIN SET @Valid_ST = (SELECT  Top 1 D_Vaild FROM SB_Contents WHERE ST_ID = @ST_ID  AND IM_ID = @IM_ID ORDER BY T_ID DESC) END
--IF @Valid_ST IS NULL BEGIN SET @Valid_ST = ''01-01-1900'' END
END

IF @U_IM_ID = 0 BEGIN SELECT TOP 1 @U_IM_ID = U_IM_ID FROM IM_Menu_Units_V WHERE IM_ID = @IM_ID AND is_Default = 1 END
SELECT @Price = PRICE,@Price_2 = Min_SP,@Price_3 = Min_SP_2,@OFFER_PRICE = OFFER_PRICE  ,@U_ID = U_ID,@U_Cargo = U_Cargo , @Cost = Cost FROM IM_Menu_Units_V WHERE U_IM_ID = @U_IM_ID 
SELECT @ST_QTY = (U_Cargo * @QYT) FROM Units WHERE U_ID = @U_ID


IF @SALES_TYPES = 1 
BEGIN 
SET @Price = @Price_2 
SET @is_By_Min_SP = 1
END
IF @SALES_TYPES = 2 
BEGIN 
SET @Price = @Price_3 
SET @is_By_Min_SP_2  = 1
END


IF @IM_Price IS NOT NULL BEGIN SET @Price = @IM_Price  END

select @Bercent = Percent_Price from Change_Price WHERE GM_ID = (SELECT GM_ID FROM IM_Menu WHERE IM_ID = @IM_ID ) 
IF @Bercent IS NOT NULL
BEGIN
SET @NOTES = @Bercent
SET @Price += @Price * (@Bercent / 100)
END

 IF @QYT = 0 BEGIN SET @QYT = 1 END
 EXEC [Check_IM_OFFER] @IM_ID,@Price OUTPUT,@OFFER_PRICE

EXEC @T_ID = [AA_GET_MAX_SB_Contents]

INSERT INTO SB_Contents (T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,Date,Compons,Cost,Price,D_Vaild,QTY,T_Price,U_Cargo,ST_QTY,isDepended,Notes,Barcode,is_By_Min_SP,is_By_Min_SP_2)
 VALUES(@T_ID,@SB_T_ID,@IM_ID,@U_ID,@ST_ID,GETDATE(),NULL,(@Cost * @U_Cargo),@Price,@Valid_ST,@QYT,(@Price * @QYT),@U_Cargo,@ST_QTY,1,@NOTES,@Barcode,@is_By_Min_SP,@is_By_Min_SP_2)


 IF @On_Update = 1 
 BEGIN 
 -- Components are rebuilt after the order-state check below (AuditComponents015).
 --UPDATE SB_Contents SET isDepended = 1 WHERE T_ID = @T_ID
 EXEC AG_Balance_Update_Pied_On_Update @SB_T_ID,1
 END
 

 IF EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WHERE T_ID=@SB_T_ID AND S_Bills_Type=3 AND ISNULL(Order_isDeleverd,0)=0)
 BEGIN
 UPDATE SB_Contents SET isDepended = 0 WHERE T_ID = @T_ID
 --EXEC Dec_IM_QTY @IM_ID,@ST_ID,@Valid_ST,@ST_QTY
 END


 --EXEC SB_Contents_UPDATE_ST_PERCENT @T_ID,@ST_ID


--COMMIT
--END TRY

--BEGIN CATCH
--IF @@TRANCOUNT > 0
--raiserror(''[SB_Contents_INSERT]'',16,1,''خطأ'')
--ROLLBACK
--END CATCH

EXEC dbo.SB_Contents_Structers_insert_ONE @T_ID;

exec Recalculate_Agent_Bill_Total @SB_T_ID

COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
DECLARE @ERR_STR NVARCHAR(MAX)
       SET @ERR_STR  = ''[SB_Contents_INSERT] '' + ERROR_MESSAGE()
	   raiserror(@ERR_STR,16,1,''خطأ'')
ROLLBACK
END CATCH';
 EXEC sys.sp_executesql @definition;
 SET @definition=N'ALTER PROCEDURE [dbo].[SB_Contents_INSERT_2]
	@SB_T_ID int,
	@IM_ID int,
	@D_Vaild DATE = NULL,
	@QYT float = 1,
	@U_ID int,
	@ST_ID int,
	@Price FLOAT,
	@On_Update Bit,
	@Current_QTY Float = NULL,
	@Notes NVARCHAR(500) = NULL,
	@Barcode NVARCHAR(100) = NULL,
	@Serial_Code NVARCHAR(100) = '''',
	@is_By_Min_SP	int = 0	,
    @is_By_Min_SP_2	int	= 0,
	@SB_IM_NEW_ROW bit = 1

AS

DECLARE  @T_ID int, @U_Cargo NUMERIC(18,2),@Cost Float,@ST_QTY NUMERIC(18,2),@Valid_ST NVARCHAR(50)
,@Total Numeric(18,3),@AG_ID int,@Markter_Val Numeric(18,3),@OFFER_PRICE Numeric(18,3),@LAST_T_ID INT

BEGIN TRY
BEGIN TRANSACTION

-- AuditComponents015: serialize changes to this invoice before changing its lines.
SELECT @AG_ID=AG_ID FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@SB_T_ID;
SET @Valid_ST = @D_Vaild

IF @Valid_ST IS NULL 
BEGIN 
SET @Valid_ST ='''' 
END
ELSE
BEGIN
SELECT @Current_QTY = ISNULL(SUM(QTY),0) FROM ST_Balance_V WHERE IM_ID = @IM_ID
IF @Current_QTY <= 0 BEGIN SET @Valid_ST = (SELECT  Top 1 D_Vaild FROM SB_Contents WHERE ST_ID = @ST_ID  AND IM_ID = @IM_ID ORDER BY T_ID DESC) END
--IF @Valid_ST IS NULL BEGIN SET @Valid_ST = ''01-01-1900'' END
END


SELECT @U_Cargo = U_Cargo , @Cost = Cost , @Markter_Val = Markter_Val , @OFFER_PRICE = OFFER_PRICE FROM IM_Menu_Units_V WHERE IM_ID = @IM_ID AND U_ID = @U_ID

IF (SELECT Markter_ID  FROM SB_Info_V WHERE T_ID = @SB_T_ID) IN( 1 , NULL)  BEGIN SET @Markter_Val = 0 END

SELECT @ST_QTY = (@U_Cargo * @QYT)

IF @QYT = 0 BEGIN SET @QYT = 1 END

EXEC @T_ID = [AA_GET_MAX_SB_Contents]

 EXEC [Check_IM_OFFER] @IM_ID,@Price OUTPUT,@OFFER_PRICE


 IF @SB_IM_NEW_ROW = 1 
 BEGIN

INSERT INTO SB_Contents (T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,Date,Compons,Cost,Price,D_Vaild,QTY,T_Price,U_Cargo,ST_QTY,isDepended,Notes,Markter_Val,Barcode,Serial_Code,is_By_Min_SP,is_By_Min_SP_2,is_Check)
 VALUES(@T_ID,@SB_T_ID,@IM_ID,@U_ID,@ST_ID,GETDATE(),NULL,(@Cost * @U_Cargo),@Price,@Valid_ST,@QYT,(@Price * @QYT),@U_Cargo,@ST_QTY,1,@Notes,@Markter_Val,@Barcode,@Serial_Code,@is_By_Min_SP,@is_By_Min_SP_2,1)

 END
 ELSE
 IF (SELECT  T_ID FROM SB_Contents WHERE Bill_T_ID = @SB_T_ID AND IM_ID = @IM_ID AND U_ID = @U_ID AND Barcode = @Barcode AND Cost = @Cost AND D_Vaild = @Valid_ST AND Price = @Price AND U_Cargo = @U_Cargo ) IS NOT NULL
 BEGIN

 SELECT @LAST_T_ID = T_ID FROM SB_Contents WHERE
 Bill_T_ID = @SB_T_ID AND IM_ID = @IM_ID AND U_ID = @U_ID AND Barcode = @Barcode AND Cost = @Cost AND D_Vaild = @Valid_ST AND Price = @Price AND U_Cargo = @U_Cargo

 UPDATE SB_Contents SET QTY += @QYT , ST_QTY = ST_QTY + (@QYT * @U_Cargo) , T_Price = ((QTY+@QYT) * Price) WHERE T_ID = @LAST_T_ID
 END
 ELSE 
 BEGIN
 INSERT INTO SB_Contents (T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,Date,Compons,Cost,Price,D_Vaild,QTY,T_Price,U_Cargo,ST_QTY,isDepended,Notes,Markter_Val,Barcode,Serial_Code,is_By_Min_SP,is_By_Min_SP_2,is_Check)
 VALUES(@T_ID,@SB_T_ID,@IM_ID,@U_ID,@ST_ID,GETDATE(),NULL,(@Cost * @U_Cargo),@Price,@Valid_ST,@QYT,(@Price * @QYT),@U_Cargo,@ST_QTY,1,@Notes,@Markter_Val,@Barcode,@Serial_Code,@is_By_Min_SP,@is_By_Min_SP_2,1)

 END











 -- AuditComponents015: a merge changes LAST_T_ID, not the newly allocated ID.
 SET @T_ID=ISNULL(@LAST_T_ID,@T_ID);
 IF EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WHERE T_ID=@SB_T_ID AND S_Bills_Type=3 AND ISNULL(Order_isDeleverd,0)=0)
  UPDATE dbo.SB_Contents SET isDepended=0 WHERE T_ID=@T_ID;
 EXEC dbo.SB_Contents_Structers_insert_ONE @T_ID;

 IF @On_Update = 1
 BEGIN 
 --UPDATE SB_Contents SET isDepended = 1 WHERE T_ID = @T_ID
 EXEC AG_Balance_Update_Pied_On_Update @SB_T_ID,1
 END


 --EXEC Dec_IM_QTY @IM_ID,@ST_ID,@Valid_ST,@ST_QTY
 --EXEC SB_Contents_UPDATE_ST_PERCENT @T_ID,@ST_ID

 exec Recalculate_Agent_Bill_Total @SB_T_ID
 
COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
DECLARE @ERR_STR NVARCHAR(MAX)
       SET @ERR_STR  = '' [SB_Contents_INSERT_2] '' + ERROR_MESSAGE()
	   raiserror(@ERR_STR,16,1,''خطأ'')
ROLLBACK
END CATCH';
 EXEC sys.sp_executesql @definition;
 SET @definition=N'ALTER PROCEDURE [dbo].[SB_Contents_Delete_IM]
	@T_ID int,
	@On_Update bit
AS
BEGIN TRY
BEGIN TRANSACTION


DECLARE @ST_QTY NUMERIC(18,2),@U_Cargo NUMERIC(18,2),@U_ID INT,@IM_ID INT,@ST_ID INT,@D_Vaild NVARCHAR(50), @SB_T_ID INT
,@B_T_ID INT,@AG_ID INT,@Valid_ST NVARCHAR(20),@i INT

IF @On_Update = 1 BEGIN SELECT @B_T_ID = Bill_T_ID FROM SB_Contents WHERE T_ID = @T_ID END

SELECT @U_Cargo = U_Cargo ,@IM_ID=IM_ID, @ST_ID=ST_ID,@D_Vaild=D_Vaild,@ST_QTY =ST_QTY , @SB_T_ID = Bill_T_ID FROM SB_Contents where T_ID = @T_ID

-- AuditComponents015: remove children before the parent in every invoice state.
EXEC [SB_Contents_Structers_Remove_ONE] @T_ID

DELETE FROM SB_Contents  where T_ID = @T_ID
DELETE FROM SB_IM_Components WHERE IM_T_ID = @T_ID



DELETE FROM SB_Rsv where SB_T_ID = @T_ID
DELETE FROM SB_Rsv_2 where SB_T_ID = @T_ID
DELETE FROM SB_VALUE_Bercent_Details where SB_T_ID = @T_ID



IF @On_Update = 1 BEGIN EXEC AG_Balance_Update_Pied_On_Update @B_T_ID,1 END 

--IF (SELECT ISNULL(S_Bills_Type,1) FROM Agents_Balance_MV WHERE T_ID = @SB_T_ID) <> 3
--BEGIN
--EXEC Inc_IM_QTY @IM_ID,@ST_ID,@D_Vaild,@ST_QTY 
--END
 
COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
raiserror(''[SB_Contents_Delete_IM]'',16,1,''خطأ'')
ROLLBACK
END CATCH';
 EXEC sys.sp_executesql @definition;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
