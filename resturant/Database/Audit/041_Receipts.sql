/* SQL Server 2014. CODE ONLY: install only after explicit deployment authorization.
   Sequential prerequisite: 040_Table_Split.sql. Ordinary Receipt screen types 3/4 only.
   Custody/FormType 12 and receipts linked by Custody.C_Rcpt_T_ID = Receipt_Num remain excluded.
   Creates an isolated ordinary-receipt insert; preserves the legacy insert used by custody.
   Corrects only the three currency table targets, preserving all amounts/formulas and void behavior.
   No existing business data is rewritten by this migration. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51141,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51141,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51141,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51141,'Close database clients first.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c'
  AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.TableSplit"]')=1) THROW 51141,'Unexpected source or prerequisite 040.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51141,'Incomplete requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('dbo.Agents_Balance_MV_RCT'),OBJECT_ID('dbo.Treasury_Balance_MV'),OBJECT_ID('dbo.Custody')))
  THROW 51141,'Review business triggers first.',1;
 DECLARE @schema TABLE(O sysname,C sysname,T sysname,L smallint,P tinyint,S tinyint,N bit,I bit);
 INSERT @schema VALUES
  ('Agents_Balance_MV_RCT','T_ID','int',4,10,0,0,0),
  ('Agents_Balance_MV_RCT','Receipt_Num','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','Receipt_Tran_ID','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','AG_ID','int',4,10,0,0,0),
  ('Agents_Balance_MV_RCT','Pr_ID','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','ReferNum','nvarchar',200,0,0,1,0),
  ('Agents_Balance_MV_RCT','Date','datetime',8,23,3,0,0),
  ('Agents_Balance_MV_RCT','Receipt_Title','nvarchar',1100,0,0,1,0),
  ('Agents_Balance_MV_RCT','Total','numeric',9,18,3,1,0),
  ('Agents_Balance_MV_RCT','Discount','numeric',9,18,3,1,0),
  ('Agents_Balance_MV_RCT','Pure','numeric',9,18,3,1,0),
  ('Agents_Balance_MV_RCT','Debit','numeric',9,18,3,1,0),
  ('Agents_Balance_MV_RCT','Credit','numeric',9,18,3,1,0),
  ('Agents_Balance_MV_RCT','Balance','numeric',9,18,3,1,0),
  ('Agents_Balance_MV_RCT','About','nvarchar',-1,0,0,1,0),
  ('Agents_Balance_MV_RCT','BsType_ID','int',4,10,0,0,0),
  ('Agents_Balance_MV_RCT','isDepended','int',4,10,0,0,0),
  ('Agents_Balance_MV_RCT','isVoid','int',4,10,0,0,0),
  ('Agents_Balance_MV_RCT','isPied','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','User_ID','int',4,10,0,0,0),
  ('Agents_Balance_MV_RCT','Barcode','nvarchar',100,0,0,1,0),
  ('Agents_Balance_MV_RCT','Img','image',16,0,0,1,0),
  ('Agents_Balance_MV_RCT','Cr_Equal_Value','numeric',9,18,3,1,0),
  ('Agents_Balance_MV_RCT','Debit_2','numeric',9,18,3,1,0),
  ('Agents_Balance_MV_RCT','Credit_2','numeric',9,18,3,1,0),
  ('Agents_Balance_MV_RCT','Balance_2','numeric',9,18,3,1,0),
  ('Agents_Balance_MV_RCT','Custody_ID','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','Pay_ID','int',4,10,0,0,0),
  ('Agents_Balance_MV_RCT','Prt_Counter','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','JournalId','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','PostedAt','datetime',8,23,3,1,0),
  ('Agents_Balance_MV_RCT','PostedBy','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','Tr_ID','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','OriginalJournalId','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','LastReversalJournalId','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','NeedRepost','bit',1,1,0,0,0),
  ('Agents_Balance_MV_RCT','EditVersion','int',4,10,0,0,0),
  ('Agents_Balance_MV_RCT','EditOpenedAt','datetime',8,23,3,1,0),
  ('Agents_Balance_MV_RCT','EditOpenedBy','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','LastRepostAt','datetime',8,23,3,1,0),
  ('Agents_Balance_MV_RCT','LastRepostBy','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','VoidAt','datetime',8,23,3,1,0),
  ('Agents_Balance_MV_RCT','VoidBy','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','VoidReason','nvarchar',1000,0,0,1,0),
  ('Agents_Balance_MV_RCT','VoidReversalJournalId','int',4,10,0,1,0),
  ('Agents_Balance_MV_RCT','NeedCancelReverse','bit',1,1,0,0,0),
  ('Treasury_Balance_MV','T_ID','int',4,10,0,0,0),
  ('Treasury_Balance_MV','AGBalance_T_ID','int',4,10,0,1,0),
  ('Treasury_Balance_MV','Tran_ID','int',4,10,0,1,0),
  ('Treasury_Balance_MV','date','datetime',8,23,3,1,0),
  ('Treasury_Balance_MV','Tr_ID','int',4,10,0,0,0),
  ('Treasury_Balance_MV','notice_move','nvarchar',-1,0,0,1,0),
  ('Treasury_Balance_MV','Bank_Name','nvarchar',200,0,0,1,0),
  ('Treasury_Balance_MV','CheckNum','nvarchar',200,0,0,1,0),
  ('Treasury_Balance_MV','Pure','numeric',9,18,3,1,0),
  ('Treasury_Balance_MV','Debit','numeric',9,18,3,1,0),
  ('Treasury_Balance_MV','Credit','numeric',9,18,3,1,0),
  ('Treasury_Balance_MV','Balance','numeric',9,18,3,1,0),
  ('Treasury_Balance_MV','Tr_Type_ID','int',4,10,0,0,0),
  ('Treasury_Balance_MV','isTran_From','bit',1,1,0,1,0),
  ('Treasury_Balance_MV','isTran_To','bit',1,1,0,1,0),
  ('Treasury_Balance_MV','User_ID','int',4,10,0,0,0),
  ('Treasury_Balance_MV','isVoid','bit',1,1,0,0,0),
  ('Treasury_Balance_MV','isFirst_Time','int',4,10,0,1,0),
  ('Treasury_Balance_MV','JournalId','int',4,10,0,1,0),
  ('Treasury_Balance_MV','PostedAt','datetime',8,23,3,1,0),
  ('Treasury_Balance_MV','PostedBy','int',4,10,0,1,0),
  ('Treasury_Balance_MV','OriginalJournalId','int',4,10,0,1,0),
  ('Treasury_Balance_MV','LastReversalJournalId','int',4,10,0,1,0),
  ('Treasury_Balance_MV','NeedRepost','bit',1,1,0,0,0),
  ('Treasury_Balance_MV','EditVersion','int',4,10,0,0,0),
  ('Treasury_Balance_MV','EditOpenedAt','datetime',8,23,3,1,0),
  ('Treasury_Balance_MV','EditOpenedBy','int',4,10,0,1,0),
  ('Treasury_Balance_MV','LastRepostAt','datetime',8,23,3,1,0),
  ('Treasury_Balance_MV','LastRepostBy','int',4,10,0,1,0),
  ('Treasury_Balance_MV','VoidAt','datetime',8,23,3,1,0),
  ('Treasury_Balance_MV','VoidBy','int',4,10,0,1,0),
  ('Treasury_Balance_MV','VoidReason','nvarchar',1000,0,0,1,0),
  ('Treasury_Balance_MV','VoidReversalJournalId','int',4,10,0,1,0),
  ('Treasury_Balance_MV','NeedCancelReverse','bit',1,1,0,0,0);
 IF EXISTS(SELECT 1 FROM @schema e LEFT JOIN sys.columns c ON c.object_id=OBJECT_ID(N'dbo.'+e.O) AND c.name=e.C
  LEFT JOIN sys.types t ON t.user_type_id=c.user_type_id
  WHERE c.column_id IS NULL OR t.name<>e.T OR c.max_length<>e.L OR c.precision<>e.P OR c.scale<>e.S OR c.is_nullable<>e.N OR c.is_identity<>e.I)
  OR EXISTS(SELECT 1 FROM sys.columns c WHERE c.object_id IN(OBJECT_ID('dbo.Agents_Balance_MV_RCT'),OBJECT_ID('dbo.Treasury_Balance_MV'))
   AND NOT EXISTS(SELECT 1 FROM @schema e WHERE OBJECT_ID(N'dbo.'+e.O)=c.object_id AND e.C=c.name))
  THROW 51141,'Unexpected receipt/treasury schema.',1;
 IF OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_BalanceMV_insert_RCT')) IS NULL OR DATALENGTH(OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_BalanceMV_insert_RCT')))>8000
  OR HASHBYTES('SHA2_256',OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_BalanceMV_insert_RCT')))<>0x4F2DCF7C3DC4CD3B530696461E467CB7C785A103204E642134CB1FEC969F660B THROW 51141,'Review changed Agents_BalanceMV_insert_RCT before deploying.',1;
 IF OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_BalanceMV_Update_RCT')) IS NULL OR DATALENGTH(OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_BalanceMV_Update_RCT')))>8000
  OR HASHBYTES('SHA2_256',OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_BalanceMV_Update_RCT')))<>0x27FFA6EC9B43ABF9C8CE7CC2E55CA79A9DB6ABC2F20F671123D64803FDCD664E THROW 51141,'Review changed Agents_BalanceMV_Update_RCT before deploying.',1;
 IF OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_RCT_OpenForEdit')) IS NULL OR DATALENGTH(OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_RCT_OpenForEdit')))>8000
  OR HASHBYTES('SHA2_256',OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_RCT_OpenForEdit')))<>0xC36AF734D35422F4B6D22972265DBC7FA6A97B29B699C066BEFC1F4933EF7F1C THROW 51141,'Review changed Agents_Balance_MV_RCT_OpenForEdit before deploying.',1;
 IF OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_RCT_VOID')) IS NULL OR DATALENGTH(OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_RCT_VOID')))>8000
  OR HASHBYTES('SHA2_256',OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_RCT_VOID')))<>0x38A55C3C816F3F73742C3B58D5AB5C8EFF5B4EE3A9918319E693F3D984AB409F THROW 51141,'Review changed Agents_Balance_MV_RCT_VOID before deploying.',1;
 IF OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_RCT_Cancel')) IS NULL OR DATALENGTH(OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_RCT_Cancel')))>8000
  OR HASHBYTES('SHA2_256',OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_RCT_Cancel')))<>0xD8BC0954E2FB163784B85C37E51BE1C6A45BE95C7C55216D4025A9E47DA1B210 THROW 51141,'Review changed Agents_Balance_MV_RCT_Cancel before deploying.',1;
 IF OBJECT_DEFINITION(OBJECT_ID('dbo.AA_GET_MAX_ID')) IS NULL OR DATALENGTH(OBJECT_DEFINITION(OBJECT_ID('dbo.AA_GET_MAX_ID')))>8000
  OR HASHBYTES('SHA2_256',OBJECT_DEFINITION(OBJECT_ID('dbo.AA_GET_MAX_ID')))<>0x71826097F08C19B6EC806AC2B253D5E97E088D6E3998608C65BE2CB43FDD8BC6 THROW 51141,'Review changed AA_GET_MAX_ID before deploying.',1;
 IF OBJECT_DEFINITION(OBJECT_ID('dbo.AG_BalanceMV_GetMaxRecepitNum')) IS NULL OR DATALENGTH(OBJECT_DEFINITION(OBJECT_ID('dbo.AG_BalanceMV_GetMaxRecepitNum')))>8000
  OR HASHBYTES('SHA2_256',OBJECT_DEFINITION(OBJECT_ID('dbo.AG_BalanceMV_GetMaxRecepitNum')))<>0x50178CF3B6BD341B2679927654FA9900A8D7707D155E14E016D5140F31C914E8 THROW 51141,'Review changed AG_BalanceMV_GetMaxRecepitNum before deploying.',1;
 IF OBJECT_DEFINITION(OBJECT_ID('dbo.AA_GET_MAX_Treasury_Balance_MV')) IS NULL OR DATALENGTH(OBJECT_DEFINITION(OBJECT_ID('dbo.AA_GET_MAX_Treasury_Balance_MV')))>8000
  OR HASHBYTES('SHA2_256',OBJECT_DEFINITION(OBJECT_ID('dbo.AA_GET_MAX_Treasury_Balance_MV')))<>0xD24ABE96D14C602551217C53DBCCEBEB222551CB5E6E2DA2FA43260189EBE746 THROW 51141,'Review changed AA_GET_MAX_Treasury_Balance_MV before deploying.',1;
 IF OBJECT_ID('dbo.Agents_BalanceMV_insert_RCT_Audited') IS NOT NULL THROW 51141,'Receipt isolated insert already exists.',1;
 EXEC sys.sp_executesql N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Agents_BalanceMV_insert_RCT_Audited]
@T_ID int OUTPUT,
@Date DATETIME, 
@Receipt_Num int OUTPUT,
@AG_ID	int,
@Pr_ID int = NULL,
@Receipt_Tran_ID INT = NULL,
@ReferNum nvarchar(100) = NULL,
@Receipt_Title	nvarchar(MAX) = NULL,
@Pure	numeric(18, 3)= NULL,
@About	nvarchar(MAX) = NULL,
@BsType_ID	int,
@User_ID int,
@Tr_ID	int = NULL,
@Bank_Name nvarchar(100) = NULL,
@CheckNum nvarchar(100) = NULL,
@Pay_ID	int	=1,
@Discount numeric(18, 3)= NULL

AS
-- ReceiptInsert041: isolated ordinary receipt currency target; legacy custody procedure is unchanged.

declare @AGBalance_T_ID int , @Cr_Equal_Value FLOAT,@Debit	numeric(18, 3) = NULL,@Credit	numeric(18, 3) = NULL ,@T_ID_2 INT,@Tr_Type_ID	int 

BEGIN TRY
BEGIN TRANSACTION

--begin


if @Pr_ID = 0 BEGIN SET @Pr_ID = NULL  END 

--IF @Receipt_Num = 0 BEGIN 
EXEC @Receipt_Num = AG_BalanceMV_GetMaxRecepitNum

IF @BsType_ID = 3 BEGIN SELECT @Debit  = @PURE , @Tr_Type_ID = 2  END
ELSE IF @BsType_ID = 4 BEGIN SELECT @Credit = @PURE , @Tr_Type_ID = 1 END

   EXEC @T_ID = AA_GET_MAX_ID ''Agents_Balance_MV_RCT''

insert into Agents_Balance_MV_RCT (T_ID,Receipt_Num,Receipt_Tran_ID,AG_ID,Pr_ID,Date,Receipt_Title,About,BsType_ID,isPied,User_ID,Pure,Pay_ID,Tr_ID,Discount)
	                        values(@T_ID,@Receipt_Num,@Receipt_Tran_ID,@AG_ID,@Pr_ID,@Date,@Receipt_Title,@About,@BsType_ID,1,@User_ID,@Pure,@Pay_ID,@Tr_ID,@Discount)
	 SELECT @AGBalance_T_ID = @T_ID

---------------------------------------------------------------------
	--if  @BsType_ID = 3 BEGIN UPDATE Agents_Balance_MV SET isPied = 1 WHERE T_ID = @Receipt_Tran_ID END

	EXEC @T_ID_2 = [AA_GET_MAX_Treasury_Balance_MV]
	Insert Into Treasury_Balance_MV 
	(T_ID,AGBalance_T_ID,Tran_ID,date,Tr_ID,notice_move,Bank_Name,CheckNum,Pure,Credit,Debit,Tr_Type_ID,isTran_From,isTran_To,User_ID,isVoid)
	Values
	 (@T_ID_2,@AGBalance_T_ID,NULL,@Date,@Tr_ID,@Receipt_Title,@Bank_Name,@CheckNum,@Pure,@Debit,@Credit,@Tr_Type_ID,NULL,NULL,@User_ID,0)


IF @AG_ID > 1 AND ( SELECT Type_ID FROM Agents WHERE AG_ID = @AG_ID ) <> 2
BEGIN
SELECT @Cr_Equal_Value = Cr_Equal_Value FROM Agents_Balance_MV_RCT WHERE T_ID = @T_ID
IF @BsType_ID = 4 BEGIN Update Agents_Balance_MV_RCT SET Debit_2 = (@Debit / @Cr_Equal_Value) WHERE T_ID = @T_ID END
IF @BsType_ID = 3 BEGIN Update Agents_Balance_MV_RCT SET Credit_2 = (@Credit / @Cr_Equal_Value) WHERE T_ID = @T_ID END

END


--if @Receipt_Tran_ID <> 0 and (SELECT BsType_ID FROM Agents_Balance_MV WHERE T_ID = @Receipt_Tran_ID) = 1
--BEGIN
--EXEC [AG_Balance_Update_Pied] @Receipt_Tran_ID
--END


	
	COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
	print ''DB ERROR ! __ [Agents_BalanceMV_insert_RCT]''
ROLLBACK
END CATCH

--end











';
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Receipt.Created',@sql)>0 OR CHARINDEX('TableSplit.Started',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51141,'Unexpected begin baseline 040.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Receipt.Created'',''Receipt.EditOpened'',''Receipt.Updated'',''Receipt.Voided'') AND @DomainCode=''Finance.Receipts'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Receipt.Created',@sql)>0 OR CHARINDEX('TableSplit.Started',@sql)=0 OR CHARINDEX('AuditTableSplit040',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51141,'Unexpected writer baseline 040.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Receipt.Created'',''Receipt.EditOpened'',''Receipt.Updated'',''Receipt.Voided'') AND @domain=''Finance.Receipts'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51141,'Unexpected receipt field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Receipt.Created'',''Receipt.EditOpened'',''Receipt.Updated'',''Receipt.Voided'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51141,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditReceipts041: ordinary receipts only; full persistent after-state, isolated creation procedure.
 IF @operation IN(''Receipt.Created'',''Receipt.EditOpened'',''Receipt.Updated'',''Receipt.Voided'')
 BEGIN
  DECLARE @rcOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @rcId int,@rcNumber int=TRY_CONVERT(int,@EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')),
   @rcType int,@rcLinkedType int,@rcLinked int,@rcCreated bit=CASE WHEN @operation=''Receipt.Created'' THEN 1 ELSE 0 END;
  SET @rcId=TRY_CONVERT(int,@rcOwner);
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''Receipt'' OR @EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'')<>''Receipt''
   OR ISNULL(@rcId,0)<=0 OR ISNULL(@rcNumber,0)<=0 OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0
   OR @EventXml.value(''count(/Event/Document)'',''int'')<>1 THROW 51141,''Invalid receipt identity.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId
   AND EntityType=''Receipt'' AND EntityId=CASE WHEN @rcCreated=1 THEN ''New'' ELSE @rcOwner END)
   THROW 51141,''Receipt request identity mismatch.'',1;
  SELECT @rcType=BsType_ID,@rcLinked=Receipt_Tran_ID FROM dbo.Agents_Balance_MV_RCT WITH(HOLDLOCK)
   WHERE T_ID=@rcId AND Receipt_Num=@rcNumber AND BsType_ID IN(3,4) AND ISNULL(Custody_ID,0)=0
   AND isVoid=CASE WHEN @operation=''Receipt.Voided'' THEN 1 ELSE 0 END;
  IF @rcType IS NULL OR EXISTS(SELECT 1 FROM dbo.Custody WITH(HOLDLOCK) WHERE C_Rcpt_T_ID=@rcNumber)
   THROW 51141,''Receipt absent, void state invalid, or custody receipt excluded.'',1;
  IF @operation=''Receipt.Created'' AND NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV_RCT WHERE T_ID=@rcId AND User_ID=@user)
   THROW 51141,''Receipt creator mismatch.'',1;
  IF @operation IN(''Receipt.EditOpened'',''Receipt.Updated'') AND NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV_RCT WHERE T_ID=@rcId AND EditOpenedBy=@user AND EditOpenedAt IS NOT NULL)
   THROW 51141,''Receipt edit ownership mismatch.'',1;
  IF @operation=''Receipt.Created'' AND ISNULL(@rcLinked,0)<>0
  BEGIN
   SELECT @rcLinkedType=BsType_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=@rcLinked AND isVoid=0;
   IF @rcLinkedType IS NULL OR @rcLinkedType NOT IN(1,2,7,10,11) THROW 51141,''Unsupported or missing linked invoice.'',1;
  END;
  IF NOT EXISTS(SELECT 1 FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID
   WHERE u.User_ID=@user AND u.is_Allow=1 AND
    ((@operation IN(''Receipt.EditOpened'',''Receipt.Updated'') AND v.Update_Receipt=1)
     OR (@operation=''Receipt.Voided'' AND v.Cancel_Receipt=1)
     OR (@operation=''Receipt.Created'' AND
       ((ISNULL(@rcLinked,0)=0 AND ((@rcType=3 AND v.Sales=1) OR (@rcType=4 AND v.Pch=1)))
        OR (@rcLinkedType=1 AND v.Sales=1) OR (@rcLinkedType=7 AND v.Pch=1) OR (@rcLinkedType=2 AND v.Exp=1)
        OR (@rcLinkedType=10 AND v.SB_Rtn=1) OR (@rcLinkedType=11 AND v.Pch_Rtn=1)))))
   THROW 51141,''Receipt permission denied.'',1;
  DECLARE @rcFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @rcFields VALUES
   (''ReceiptHeader'',''Rct_T_ID'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_Receipt_Num'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_Receipt_Tran_ID'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_AG_ID'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_Pr_ID'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_ReferNum'',''String'',''nvarchar(100)''),
   (''ReceiptHeader'',''Rct_Date'',''DateTime'',''datetime''),
   (''ReceiptHeader'',''Rct_Receipt_Title'',''String'',''nvarchar(550)''),
   (''ReceiptHeader'',''Rct_Total'',''Decimal'',''numeric(18,3)''),
   (''ReceiptHeader'',''Rct_Discount'',''Decimal'',''numeric(18,3)''),
   (''ReceiptHeader'',''Rct_Pure'',''Decimal'',''numeric(18,3)''),
   (''ReceiptHeader'',''Rct_Debit'',''Decimal'',''numeric(18,3)''),
   (''ReceiptHeader'',''Rct_Credit'',''Decimal'',''numeric(18,3)''),
   (''ReceiptHeader'',''Rct_Balance'',''Decimal'',''numeric(18,3)''),
   (''ReceiptHeader'',''Rct_About'',''String'',''nvarchar(max)''),
   (''ReceiptHeader'',''Rct_BsType_ID'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_isDepended'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_isVoid'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_isPied'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_User_ID'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_Barcode'',''String'',''nvarchar(50)''),
   (''ReceiptHeader'',''Rct_Cr_Equal_Value'',''Decimal'',''numeric(18,3)''),
   (''ReceiptHeader'',''Rct_Debit_2'',''Decimal'',''numeric(18,3)''),
   (''ReceiptHeader'',''Rct_Credit_2'',''Decimal'',''numeric(18,3)''),
   (''ReceiptHeader'',''Rct_Balance_2'',''Decimal'',''numeric(18,3)''),
   (''ReceiptHeader'',''Rct_Custody_ID'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_Pay_ID'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_Prt_Counter'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_JournalId'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_PostedAt'',''DateTime'',''datetime''),
   (''ReceiptHeader'',''Rct_PostedBy'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_Tr_ID'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_OriginalJournalId'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_LastReversalJournalId'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_NeedRepost'',''Boolean'',''bit''),
   (''ReceiptHeader'',''Rct_EditVersion'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_EditOpenedAt'',''DateTime'',''datetime''),
   (''ReceiptHeader'',''Rct_EditOpenedBy'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_LastRepostAt'',''DateTime'',''datetime''),
   (''ReceiptHeader'',''Rct_LastRepostBy'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_VoidAt'',''DateTime'',''datetime''),
   (''ReceiptHeader'',''Rct_VoidBy'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_VoidReason'',''String'',''nvarchar(500)''),
   (''ReceiptHeader'',''Rct_VoidReversalJournalId'',''Integer'',''int''),
   (''ReceiptHeader'',''Rct_NeedCancelReverse'',''Boolean'',''bit''),
   (''ReceiptHeader'',''Rct_ImageLength'',''Integer'',''bigint''),
   (''ReceiptHeader'',''Rct_ImageHash'',''String'',''varchar(64)''),
   (''ReceiptTreasury'',''RctTr_T_ID'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_AGBalance_T_ID'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_Tran_ID'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_date'',''DateTime'',''datetime''),
   (''ReceiptTreasury'',''RctTr_Tr_ID'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_notice_move'',''String'',''nvarchar(max)''),
   (''ReceiptTreasury'',''RctTr_Bank_Name'',''String'',''nvarchar(100)''),
   (''ReceiptTreasury'',''RctTr_CheckNum'',''String'',''nvarchar(100)''),
   (''ReceiptTreasury'',''RctTr_Pure'',''Decimal'',''numeric(18,3)''),
   (''ReceiptTreasury'',''RctTr_Debit'',''Decimal'',''numeric(18,3)''),
   (''ReceiptTreasury'',''RctTr_Credit'',''Decimal'',''numeric(18,3)''),
   (''ReceiptTreasury'',''RctTr_Balance'',''Decimal'',''numeric(18,3)''),
   (''ReceiptTreasury'',''RctTr_Tr_Type_ID'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_isTran_From'',''Boolean'',''bit''),
   (''ReceiptTreasury'',''RctTr_isTran_To'',''Boolean'',''bit''),
   (''ReceiptTreasury'',''RctTr_User_ID'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_isVoid'',''Boolean'',''bit''),
   (''ReceiptTreasury'',''RctTr_isFirst_Time'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_JournalId'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_PostedAt'',''DateTime'',''datetime''),
   (''ReceiptTreasury'',''RctTr_PostedBy'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_OriginalJournalId'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_LastReversalJournalId'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_NeedRepost'',''Boolean'',''bit''),
   (''ReceiptTreasury'',''RctTr_EditVersion'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_EditOpenedAt'',''DateTime'',''datetime''),
   (''ReceiptTreasury'',''RctTr_EditOpenedBy'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_LastRepostAt'',''DateTime'',''datetime''),
   (''ReceiptTreasury'',''RctTr_LastRepostBy'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_VoidAt'',''DateTime'',''datetime''),
   (''ReceiptTreasury'',''RctTr_VoidBy'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_VoidReason'',''String'',''nvarchar(500)''),
   (''ReceiptTreasury'',''RctTr_VoidReversalJournalId'',''Integer'',''int''),
   (''ReceiptTreasury'',''RctTr_NeedCancelReverse'',''Boolean'',''bit'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>1 OR c.AfterState NOT IN(2,3)
   OR (@rcCreated=1 AND c.BeforeState<>1) OR (@rcCreated=0 AND c.BeforeState NOT IN(2,3))
   OR NOT EXISTS(SELECT 1 FROM @rcFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType))
   THROW 51141,''Invalid receipt field/type/state.'',1;
  IF EXISTS(SELECT EntityType,EntityId,FieldCode FROM @changes GROUP BY EntityType,EntityId,FieldCode HAVING COUNT(*)<>1)
   THROW 51141,''Duplicate receipt field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN ''ReceiptHeader'' THEN 47 WHEN ''ReceiptTreasury'' THEN 34 ELSE -1 END)
   THROW 51141,''Incomplete receipt row.'',1;
  IF @EventOrdinal=1 AND ((SELECT COUNT(*) FROM @changes)<>47 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''ReceiptHeader'' OR EntityId<>@rcOwner))
   THROW 51141,''First receipt event requires its complete header.'',1;
  IF @EventOrdinal>1 AND (EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''ReceiptTreasury'')
   OR NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=1 AND EntityType=''Receipt'' AND EntityId=@rcOwner AND EventCode=@operation))
   THROW 51141,''Treasury event must follow its receipt header.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ISNULL(TRY_CONVERT(int,EntityId),0)<=0
   OR (EntityType=''ReceiptHeader'' AND EntityId<>@rcOwner)
   OR (FieldCode IN(''Rct_T_ID'',''RctTr_T_ID'') AND (AfterState<>3 OR AfterValue<>EntityId))
   OR (FieldCode=''RctTr_AGBalance_T_ID'' AND (AfterState<>3 OR AfterValue<>@rcOwner)))
   THROW 51141,''Receipt row identity mismatch.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE @rcCreated=0 AND NOT(
    (EntityType=''ReceiptHeader'' AND ((@operation=''Receipt.EditOpened'' AND FieldCode IN(''Rct_NeedRepost'',''Rct_OriginalJournalId'',''Rct_EditOpenedAt'',''Rct_EditOpenedBy''))
     OR (@operation=''Receipt.Updated'' AND (FieldCode IN(''Rct_AG_ID'',''Rct_Date'',''Rct_Receipt_Title'',''Rct_About'',''Rct_Pure'',''Rct_Pay_ID'',''Rct_Discount'')
      OR (@rcType=3 AND FieldCode IN(''Rct_Debit'',''Rct_Tr_ID'')) OR (@rcType=4 AND FieldCode=''Rct_Credit'')))
     OR (@operation=''Receipt.Voided'' AND FieldCode IN(''Rct_isVoid'',''Rct_VoidAt'',''Rct_VoidBy'',''Rct_VoidReason'',''Rct_NeedCancelReverse'',''Rct_NeedRepost''))))
    OR (EntityType=''ReceiptTreasury'' AND ((@operation=''Receipt.Updated'' AND (FieldCode IN(''RctTr_Tr_ID'',''RctTr_date'',''RctTr_notice_move'',''RctTr_Pure'')
      OR (@rcType=3 AND FieldCode=''RctTr_Credit'') OR (@rcType=4 AND FieldCode=''RctTr_Debit'')))
     OR (@operation=''Receipt.Voided'' AND FieldCode=''RctTr_isVoid''))))
   AND (BeforeState<>AfterState OR ISNULL(DATALENGTH(BeforeValue),-1)<>ISNULL(DATALENGTH(AfterValue),-1)
    OR ISNULL(BeforeValue,N'''') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'''') COLLATE Latin1_General_100_BIN2))
   THROW 51141,''Change outside closed receipt operation.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.Agents_Balance_MV_RCT p WITH(HOLDLOCK) ON p.T_ID=TRY_CONVERT(int,c.EntityId)
   WHERE c.EntityType=''ReceiptHeader'' AND (p.T_ID IS NULL OR
    (c.FieldCode=''Rct_T_ID'' AND ((c.AfterState=2 AND p.[T_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[T_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[T_ID])))) OR
    (c.FieldCode=''Rct_Receipt_Num'' AND ((c.AfterState=2 AND p.[Receipt_Num] IS NOT NULL) OR (c.AfterState=3 AND (p.[Receipt_Num] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Receipt_Num])))) OR
    (c.FieldCode=''Rct_Receipt_Tran_ID'' AND ((c.AfterState=2 AND p.[Receipt_Tran_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Receipt_Tran_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Receipt_Tran_ID])))) OR
    (c.FieldCode=''Rct_AG_ID'' AND ((c.AfterState=2 AND p.[AG_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[AG_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[AG_ID])))) OR
    (c.FieldCode=''Rct_Pr_ID'' AND ((c.AfterState=2 AND p.[Pr_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Pr_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Pr_ID])))) OR
    (c.FieldCode=''Rct_ReferNum'' AND ((c.AfterState=2 AND p.[ReferNum] IS NOT NULL) OR (c.AfterState=3 AND (p.[ReferNum] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[ReferNum]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ReferNum]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''Rct_Date'' AND ((c.AfterState=2 AND p.[Date] IS NOT NULL) OR (c.AfterState=3 AND (p.[Date] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[Date])))) OR
    (c.FieldCode=''Rct_Receipt_Title'' AND ((c.AfterState=2 AND p.[Receipt_Title] IS NOT NULL) OR (c.AfterState=3 AND (p.[Receipt_Title] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Receipt_Title]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Receipt_Title]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''Rct_Total'' AND ((c.AfterState=2 AND p.[Total] IS NOT NULL) OR (c.AfterState=3 AND (p.[Total] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Total])))) OR
    (c.FieldCode=''Rct_Discount'' AND ((c.AfterState=2 AND p.[Discount] IS NOT NULL) OR (c.AfterState=3 AND (p.[Discount] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Discount])))) OR
    (c.FieldCode=''Rct_Pure'' AND ((c.AfterState=2 AND p.[Pure] IS NOT NULL) OR (c.AfterState=3 AND (p.[Pure] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Pure])))) OR
    (c.FieldCode=''Rct_Debit'' AND ((c.AfterState=2 AND p.[Debit] IS NOT NULL) OR (c.AfterState=3 AND (p.[Debit] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Debit])))) OR
    (c.FieldCode=''Rct_Credit'' AND ((c.AfterState=2 AND p.[Credit] IS NOT NULL) OR (c.AfterState=3 AND (p.[Credit] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Credit])))) OR
    (c.FieldCode=''Rct_Balance'' AND ((c.AfterState=2 AND p.[Balance] IS NOT NULL) OR (c.AfterState=3 AND (p.[Balance] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Balance])))) OR
    (c.FieldCode=''Rct_About'' AND ((c.AfterState=2 AND p.[About] IS NOT NULL) OR (c.AfterState=3 AND (p.[About] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[About]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[About]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''Rct_BsType_ID'' AND ((c.AfterState=2 AND p.[BsType_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[BsType_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[BsType_ID])))) OR
    (c.FieldCode=''Rct_isDepended'' AND ((c.AfterState=2 AND p.[isDepended] IS NOT NULL) OR (c.AfterState=3 AND (p.[isDepended] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isDepended])))) OR
    (c.FieldCode=''Rct_isVoid'' AND ((c.AfterState=2 AND p.[isVoid] IS NOT NULL) OR (c.AfterState=3 AND (p.[isVoid] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isVoid])))) OR
    (c.FieldCode=''Rct_isPied'' AND ((c.AfterState=2 AND p.[isPied] IS NOT NULL) OR (c.AfterState=3 AND (p.[isPied] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isPied])))) OR
    (c.FieldCode=''Rct_User_ID'' AND ((c.AfterState=2 AND p.[User_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[User_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[User_ID])))) OR
    (c.FieldCode=''Rct_Barcode'' AND ((c.AfterState=2 AND p.[Barcode] IS NOT NULL) OR (c.AfterState=3 AND (p.[Barcode] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Barcode]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Barcode]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''Rct_Cr_Equal_Value'' AND ((c.AfterState=2 AND p.[Cr_Equal_Value] IS NOT NULL) OR (c.AfterState=3 AND (p.[Cr_Equal_Value] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Cr_Equal_Value])))) OR
    (c.FieldCode=''Rct_Debit_2'' AND ((c.AfterState=2 AND p.[Debit_2] IS NOT NULL) OR (c.AfterState=3 AND (p.[Debit_2] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Debit_2])))) OR
    (c.FieldCode=''Rct_Credit_2'' AND ((c.AfterState=2 AND p.[Credit_2] IS NOT NULL) OR (c.AfterState=3 AND (p.[Credit_2] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Credit_2])))) OR
    (c.FieldCode=''Rct_Balance_2'' AND ((c.AfterState=2 AND p.[Balance_2] IS NOT NULL) OR (c.AfterState=3 AND (p.[Balance_2] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Balance_2])))) OR
    (c.FieldCode=''Rct_Custody_ID'' AND ((c.AfterState=2 AND p.[Custody_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Custody_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Custody_ID])))) OR
    (c.FieldCode=''Rct_Pay_ID'' AND ((c.AfterState=2 AND p.[Pay_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Pay_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Pay_ID])))) OR
    (c.FieldCode=''Rct_Prt_Counter'' AND ((c.AfterState=2 AND p.[Prt_Counter] IS NOT NULL) OR (c.AfterState=3 AND (p.[Prt_Counter] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Prt_Counter])))) OR
    (c.FieldCode=''Rct_JournalId'' AND ((c.AfterState=2 AND p.[JournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[JournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[JournalId])))) OR
    (c.FieldCode=''Rct_PostedAt'' AND ((c.AfterState=2 AND p.[PostedAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[PostedAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[PostedAt])))) OR
    (c.FieldCode=''Rct_PostedBy'' AND ((c.AfterState=2 AND p.[PostedBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[PostedBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[PostedBy])))) OR
    (c.FieldCode=''Rct_Tr_ID'' AND ((c.AfterState=2 AND p.[Tr_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Tr_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Tr_ID])))) OR
    (c.FieldCode=''Rct_OriginalJournalId'' AND ((c.AfterState=2 AND p.[OriginalJournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[OriginalJournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[OriginalJournalId])))) OR
    (c.FieldCode=''Rct_LastReversalJournalId'' AND ((c.AfterState=2 AND p.[LastReversalJournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[LastReversalJournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[LastReversalJournalId])))) OR
    (c.FieldCode=''Rct_NeedRepost'' AND ((c.AfterState=2 AND p.[NeedRepost] IS NOT NULL) OR (c.AfterState=3 AND (p.[NeedRepost] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[NeedRepost])))) OR
    (c.FieldCode=''Rct_EditVersion'' AND ((c.AfterState=2 AND p.[EditVersion] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditVersion] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[EditVersion])))) OR
    (c.FieldCode=''Rct_EditOpenedAt'' AND ((c.AfterState=2 AND p.[EditOpenedAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditOpenedAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[EditOpenedAt])))) OR
    (c.FieldCode=''Rct_EditOpenedBy'' AND ((c.AfterState=2 AND p.[EditOpenedBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditOpenedBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[EditOpenedBy])))) OR
    (c.FieldCode=''Rct_LastRepostAt'' AND ((c.AfterState=2 AND p.[LastRepostAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[LastRepostAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[LastRepostAt])))) OR
    (c.FieldCode=''Rct_LastRepostBy'' AND ((c.AfterState=2 AND p.[LastRepostBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[LastRepostBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[LastRepostBy])))) OR
    (c.FieldCode=''Rct_VoidAt'' AND ((c.AfterState=2 AND p.[VoidAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[VoidAt])))) OR
    (c.FieldCode=''Rct_VoidBy'' AND ((c.AfterState=2 AND p.[VoidBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[VoidBy])))) OR
    (c.FieldCode=''Rct_VoidReason'' AND ((c.AfterState=2 AND p.[VoidReason] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidReason] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[VoidReason]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[VoidReason]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''Rct_VoidReversalJournalId'' AND ((c.AfterState=2 AND p.[VoidReversalJournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidReversalJournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[VoidReversalJournalId])))) OR
    (c.FieldCode=''Rct_NeedCancelReverse'' AND ((c.AfterState=2 AND p.[NeedCancelReverse] IS NOT NULL) OR (c.AfterState=3 AND (p.[NeedCancelReverse] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[NeedCancelReverse])))) OR
    (c.FieldCode=''Rct_ImageLength'' AND ((c.AfterState=2 AND CONVERT(bigint,DATALENGTH(p.[Img])) IS NOT NULL) OR (c.AfterState=3 AND (CONVERT(bigint,DATALENGTH(p.[Img])) IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>CONVERT(bigint,DATALENGTH(p.[Img]))))))))
   THROW 51141,''ReceiptHeader after-state differs from persistent receipt data.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.Treasury_Balance_MV p WITH(HOLDLOCK) ON p.T_ID=TRY_CONVERT(int,c.EntityId)
   WHERE c.EntityType=''ReceiptTreasury'' AND (p.T_ID IS NULL OR ISNULL(p.AGBalance_T_ID,0)<>@rcId OR
    (c.FieldCode=''RctTr_T_ID'' AND ((c.AfterState=2 AND p.[T_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[T_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[T_ID])))) OR
    (c.FieldCode=''RctTr_AGBalance_T_ID'' AND ((c.AfterState=2 AND p.[AGBalance_T_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[AGBalance_T_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[AGBalance_T_ID])))) OR
    (c.FieldCode=''RctTr_Tran_ID'' AND ((c.AfterState=2 AND p.[Tran_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Tran_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Tran_ID])))) OR
    (c.FieldCode=''RctTr_date'' AND ((c.AfterState=2 AND p.[date] IS NOT NULL) OR (c.AfterState=3 AND (p.[date] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[date])))) OR
    (c.FieldCode=''RctTr_Tr_ID'' AND ((c.AfterState=2 AND p.[Tr_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Tr_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Tr_ID])))) OR
    (c.FieldCode=''RctTr_notice_move'' AND ((c.AfterState=2 AND p.[notice_move] IS NOT NULL) OR (c.AfterState=3 AND (p.[notice_move] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[notice_move]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[notice_move]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''RctTr_Bank_Name'' AND ((c.AfterState=2 AND p.[Bank_Name] IS NOT NULL) OR (c.AfterState=3 AND (p.[Bank_Name] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Bank_Name]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Bank_Name]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''RctTr_CheckNum'' AND ((c.AfterState=2 AND p.[CheckNum] IS NOT NULL) OR (c.AfterState=3 AND (p.[CheckNum] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[CheckNum]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[CheckNum]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''RctTr_Pure'' AND ((c.AfterState=2 AND p.[Pure] IS NOT NULL) OR (c.AfterState=3 AND (p.[Pure] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Pure])))) OR
    (c.FieldCode=''RctTr_Debit'' AND ((c.AfterState=2 AND p.[Debit] IS NOT NULL) OR (c.AfterState=3 AND (p.[Debit] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Debit])))) OR
    (c.FieldCode=''RctTr_Credit'' AND ((c.AfterState=2 AND p.[Credit] IS NOT NULL) OR (c.AfterState=3 AND (p.[Credit] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Credit])))) OR
    (c.FieldCode=''RctTr_Balance'' AND ((c.AfterState=2 AND p.[Balance] IS NOT NULL) OR (c.AfterState=3 AND (p.[Balance] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Balance])))) OR
    (c.FieldCode=''RctTr_Tr_Type_ID'' AND ((c.AfterState=2 AND p.[Tr_Type_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Tr_Type_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Tr_Type_ID])))) OR
    (c.FieldCode=''RctTr_isTran_From'' AND ((c.AfterState=2 AND p.[isTran_From] IS NOT NULL) OR (c.AfterState=3 AND (p.[isTran_From] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isTran_From])))) OR
    (c.FieldCode=''RctTr_isTran_To'' AND ((c.AfterState=2 AND p.[isTran_To] IS NOT NULL) OR (c.AfterState=3 AND (p.[isTran_To] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isTran_To])))) OR
    (c.FieldCode=''RctTr_User_ID'' AND ((c.AfterState=2 AND p.[User_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[User_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[User_ID])))) OR
    (c.FieldCode=''RctTr_isVoid'' AND ((c.AfterState=2 AND p.[isVoid] IS NOT NULL) OR (c.AfterState=3 AND (p.[isVoid] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isVoid])))) OR
    (c.FieldCode=''RctTr_isFirst_Time'' AND ((c.AfterState=2 AND p.[isFirst_Time] IS NOT NULL) OR (c.AfterState=3 AND (p.[isFirst_Time] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isFirst_Time])))) OR
    (c.FieldCode=''RctTr_JournalId'' AND ((c.AfterState=2 AND p.[JournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[JournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[JournalId])))) OR
    (c.FieldCode=''RctTr_PostedAt'' AND ((c.AfterState=2 AND p.[PostedAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[PostedAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[PostedAt])))) OR
    (c.FieldCode=''RctTr_PostedBy'' AND ((c.AfterState=2 AND p.[PostedBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[PostedBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[PostedBy])))) OR
    (c.FieldCode=''RctTr_OriginalJournalId'' AND ((c.AfterState=2 AND p.[OriginalJournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[OriginalJournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[OriginalJournalId])))) OR
    (c.FieldCode=''RctTr_LastReversalJournalId'' AND ((c.AfterState=2 AND p.[LastReversalJournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[LastReversalJournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[LastReversalJournalId])))) OR
    (c.FieldCode=''RctTr_NeedRepost'' AND ((c.AfterState=2 AND p.[NeedRepost] IS NOT NULL) OR (c.AfterState=3 AND (p.[NeedRepost] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[NeedRepost])))) OR
    (c.FieldCode=''RctTr_EditVersion'' AND ((c.AfterState=2 AND p.[EditVersion] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditVersion] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[EditVersion])))) OR
    (c.FieldCode=''RctTr_EditOpenedAt'' AND ((c.AfterState=2 AND p.[EditOpenedAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditOpenedAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[EditOpenedAt])))) OR
    (c.FieldCode=''RctTr_EditOpenedBy'' AND ((c.AfterState=2 AND p.[EditOpenedBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditOpenedBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[EditOpenedBy])))) OR
    (c.FieldCode=''RctTr_LastRepostAt'' AND ((c.AfterState=2 AND p.[LastRepostAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[LastRepostAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[LastRepostAt])))) OR
    (c.FieldCode=''RctTr_LastRepostBy'' AND ((c.AfterState=2 AND p.[LastRepostBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[LastRepostBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[LastRepostBy])))) OR
    (c.FieldCode=''RctTr_VoidAt'' AND ((c.AfterState=2 AND p.[VoidAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[VoidAt])))) OR
    (c.FieldCode=''RctTr_VoidBy'' AND ((c.AfterState=2 AND p.[VoidBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[VoidBy])))) OR
    (c.FieldCode=''RctTr_VoidReason'' AND ((c.AfterState=2 AND p.[VoidReason] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidReason] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[VoidReason]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[VoidReason]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''RctTr_VoidReversalJournalId'' AND ((c.AfterState=2 AND p.[VoidReversalJournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidReversalJournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[VoidReversalJournalId])))) OR
    (c.FieldCode=''RctTr_NeedCancelReverse'' AND ((c.AfterState=2 AND p.[NeedCancelReverse] IS NOT NULL) OR (c.AfterState=3 AND (p.[NeedCancelReverse] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[NeedCancelReverse]))))))
   THROW 51141,''ReceiptTreasury after-state differs from persistent receipt data.'',1;
  -- SQL Server 2014 HASHBYTES accepts at most 8000 input bytes. The service additionally hashes the complete
  -- image in memory and verifies equality before/after every existing-receipt operation, including larger images.
  -- This writer validates exact persisted length, nullability and canonical SHA256 text; for small images also bytes.
  IF EXISTS(SELECT 1 FROM @changes c JOIN dbo.Agents_Balance_MV_RCT p WITH(HOLDLOCK) ON p.T_ID=TRY_CONVERT(int,c.EntityId)
   WHERE c.EntityType=''ReceiptHeader'' AND c.FieldCode=''Rct_ImageHash'' AND
    ((c.AfterState=2 AND p.Img IS NOT NULL) OR (c.AfterState=3 AND
     (p.Img IS NULL OR DATALENGTH(c.AfterValue)<>128 OR c.AfterValue COLLATE Latin1_General_100_BIN2 LIKE ''%[^A-F0-9]%''
      OR (DATALENGTH(p.Img)<=8000 AND c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(varchar(64),HASHBYTES(''SHA2_256'',CONVERT(varbinary(8000),p.Img)),2) COLLATE Latin1_General_100_BIN2)
      OR (@rcCreated=1 AND DATALENGTH(p.Img)>8000)))))
   THROW 51141,''Receipt image hash/length/null state mismatch.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Finance.Receipts"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.Receipts"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF XACT_STATE()<>0 ROLLBACK;
 THROW;
END CATCH;
