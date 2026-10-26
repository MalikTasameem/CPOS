/* SQL Server 2014. Detail quantity/unit changes rebuild and audit their component effects. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51116,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51116,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51116,'Deployment busy.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c') THROW 51116,'Unexpected source.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51116,'Close other database clients.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51116,'Incomplete audit requests.',1;
 IF ISNULL(CHARINDEX('AuditComponents015',OBJECT_DEFINITION(OBJECT_ID('dbo.SB_Contents_Structers_insert_ONE'))),0)=0 THROW 51116,'Install 015 first.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.SB_IM_Unit_Change'));
 IF @sql IS NULL OR DATALENGTH(@sql)>8000 OR CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(8000),@sql)),2)<>'911AE6DCBD35A6E7BEA6F9E59438BF41CD1DCE3D110B861D7C6FF4D246178FC0'
  THROW 51116,'Unexpected detail procedure baseline.',1;
 SET @sql=N'ALTER PROCEDURE [dbo].[SB_IM_Unit_Change] 
@SB_T_ID INT,
@IM_T_ID int,
@U_ID int,
@QTY Numeric(18,3) = NULL,
@Price Numeric(18,3) = NULL,
@Notes Nvarchar(MAX)='''',
@On_Update Bit = 0,
@Sales_Type INT = 0,
@IM_Discount float = 0

AS
DECLARE @Bill_T_ID INT,@U_Cargo NUMERIC(18,2),@IM_ID INT,@Cost FLOAT , @NEW_ST_QTY FLOAT , @PREV_ST_QTY FLOAT 
, @ST_ID int ,@D_Valid NVARCHAR(20) = '''' , @ST_QTY_DEF FLOAT = 0 ,@Total Numeric(18,3),@AG_ID INT ,@OFFER_PRICE FLOAT	,@is_By_Min_SP	int = 0	,
    @is_By_Min_SP_2	int	= 0

BEGIN TRY
BEGIN TRANSACTION 


-- AuditDetailComponents016: preserve the old unit/quantity before the existing update.
DECLARE @AuditOldUnit bigint,@AuditOldQuantity numeric(18,3);
SELECT @AuditOldUnit=U_ID,@AuditOldQuantity=QTY FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@IM_T_ID;

SELECT @IM_ID  = IM_ID,@Bill_T_ID = Bill_T_ID , @ST_ID = ST_ID , @D_Valid = D_Vaild FROM SB_Contents WHERE T_ID = @IM_T_ID


IF @Price IS NULL BEGIN

IF @Sales_Type = 0
BEGIN
SELECT @Price = PRICE FROM IM_Menu_Units_V WHERE U_ID = @U_ID AND IM_ID = @IM_ID  
END
ELSE IF @Sales_Type = 1 
BEGIN 
SELECT @Price = Min_SP FROM IM_Menu_Units_V WHERE U_ID = @U_ID AND IM_ID = @IM_ID  
SET @is_By_Min_SP = 1
END
ELSE IF @Sales_Type = 2 
BEGIN 
SELECT @Price = Min_SP_2 FROM IM_Menu_Units_V WHERE U_ID = @U_ID AND IM_ID = @IM_ID  
SET @is_By_Min_SP_2  = 1
END


END
SELECT @U_Cargo = U_Cargo , @Cost = Cost, @OFFER_PRICE = OFFER_PRICE  FROM IM_Menu_Units_V WHERE IM_ID = @IM_ID AND U_ID = @U_ID
SELECT @PREV_ST_QTY = ST_QTY FROM SB_Contents WHERE T_ID = @IM_T_ID



 EXEC [Check_IM_OFFER] @IM_ID,@Price OUTPUT,@OFFER_PRICE



IF @QTY IS NULL
BEGIN
SELECT @NEW_ST_QTY = QTY FROM SB_Contents WHERE T_ID = @IM_T_ID 
SELECT @NEW_ST_QTY = (@NEW_ST_QTY * @U_Cargo)
UPDATE SB_Contents SET U_ID=@U_ID, Cost = (@Cost * @U_Cargo) ,Price=@Price, T_Price = (@Price * QTY)- @IM_Discount , U_Cargo = @U_Cargo , ST_QTY = @NEW_ST_QTY 
, Notes=@Notes , is_By_Min_SP = @is_By_Min_SP , is_By_Min_SP_2 =@is_By_Min_SP_2 , IM_Discount = @IM_Discount
 WHERE T_ID = @IM_T_ID
 END
 ELSE
 BEGIN
 SELECT @NEW_ST_QTY = (@U_Cargo * @QTY) 
 UPDATE SB_Contents SET U_ID=@U_ID , QTY=@QTY , Cost = (@Cost * @U_Cargo) , Price=@Price , T_Price = (@Price * @QTY) - @IM_Discount 
 , U_Cargo = @U_Cargo , ST_QTY = @NEW_ST_QTY , Notes=@Notes , IM_Discount = @IM_Discount
 WHERE T_ID = @IM_T_ID
 END



-- AuditDetailComponents016: price/notes-only saves do not rebuild unchanged recipes.
IF @AuditOldUnit<>@U_ID OR (@QTY IS NOT NULL AND @AuditOldQuantity<>@QTY)
 EXEC dbo.SB_Contents_Structers_insert_ONE @IM_T_ID;

IF @QTY IS NULL 
BEGIN EXEC SB_Contents_SELECT_Bill @Bill_T_ID END
ELSE  BEGIN EXEC SB_Contents_SELECT_Bill_2 @Bill_T_ID END

SELECT @ST_QTY_DEF = (@NEW_ST_QTY - @PREV_ST_QTY)

EXEC Dec_IM_QTY @IM_ID,@ST_ID,@D_Valid,@ST_QTY_DEF



--SELECT @Total = ISNULL(SUM(T_Price),0) FROM SB_Contents WHERE Bill_T_ID = @Bill_T_ID
--UPDATE Agents_Balance_MV SET Total = @Total , Pure = @Total - ISNULL(Discount,0),Credit = @Total - ISNULL(Discount,0),

UPDATE Agents_Balance_MV SET
Bill_Berfet = ((SELECT SUM(T_Price) FROM SB_Contents_V WHERE Bill_T_ID = @Bill_T_ID)  -
(SELECT SUM(SUM_Cost) FROM SB_Contents_V WHERE Bill_T_ID = @Bill_T_ID ))
WHERE T_ID = @Bill_T_ID


IF @On_Update = 1 BEGIN EXEC AG_Balance_Update_Pied_On_Update @SB_T_ID,1 END

 exec Recalculate_Agent_Bill_Total @SB_T_ID

COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
raiserror(''[SB_IM_Unit_Change]'',16,1,''خطأ'')
ROLLBACK
END CATCH';
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL OR CHARINDEX('AuditDetailComponents016',@sql)>0 THROW 51116,'Unexpected writer or already installed.',1;
 SET @sql=REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE');
 SET @old=N'(@operation=''Sales.LineQuantityStepped'' AND ((EntityType';
 IF CHARINDEX(@old,@sql)=0 OR CHARINDEX(@old,@sql,CHARINDEX(@old,@sql)+LEN(@old))>0 THROW 51116,'Unexpected writer anchor.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Sales.LineQuantityStepped'',''Sales.LineDetailsChanged'') AND ((EntityType');
 SET @old=N'IF @operation IN(''Sales.LineDetailsChanged'',''Sales.LinePriceChanged'')
 BEGIN';
 IF CHARINDEX(@old,@sql)=0 OR CHARINDEX(@old,@sql,CHARINDEX(@old,@sql)+LEN(@old))>0 THROW 51116,'Unexpected writer anchor.',1;
 SET @sql=REPLACE(@sql,@old,N'IF @operation IN(''Sales.LineDetailsChanged'',''Sales.LinePriceChanged'') AND (@EventOrdinal=1 OR @operation=''Sales.LinePriceChanged'')
 BEGIN
  -- AuditDetailComponents016: first part stays the original 25 fields.
  IF @operation=''Sales.LinePriceChanged'' AND @EventOrdinal<>1 THROW 51101,''Price-only operation has no component parts.'',1;');
 SET @old=N'IF @operation=''Sales.LineQuantityStepped''
 BEGIN';
 IF CHARINDEX(@old,@sql)=0 OR CHARINDEX(@old,@sql,CHARINDEX(@old,@sql)+LEN(@old))>0 THROW 51116,'Unexpected writer anchor.',1;
 SET @sql=REPLACE(@sql,@old,N'IF @operation=''Sales.LineQuantityStepped'' OR (@operation=''Sales.LineDetailsChanged'' AND @EventOrdinal>1)
 BEGIN');
 SET @old=N'OR @EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'') NOT IN(''Sales'',''Sales_Fast'',''POS'')
   THROW 51101,''Invalid quantity step identity or screen.'',1;';
 IF CHARINDEX(@old,@sql)=0 OR CHARINDEX(@old,@sql,CHARINDEX(@old,@sql)+LEN(@old))>0 THROW 51116,'Unexpected writer anchor.',1;
 SET @sql=REPLACE(@sql,@old,N'OR (@operation=''Sales.LineQuantityStepped'' AND @EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'') NOT IN(''Sales'',''Sales_Fast'',''POS''))
   OR (@operation=''Sales.LineDetailsChanged'' AND @EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'')<>''Change_IM_Details'')
   THROW 51101,''Invalid quantity/detail identity or screen.'',1;');
 EXEC sys.sp_executesql @sql;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
