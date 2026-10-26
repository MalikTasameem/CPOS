/* Phase 3C, SQL Server 2014. Immediate stock effect is intentionally retained.
   One-time upgrade after 005. Save definitions before running. No table changes,
   no activation, no change to cost formulas, no reactivation of voided documents. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51102,'Approved test database only.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max),@new nvarchar(max),@name sysname,@alias sysname;
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=10000;
 IF @lock<0 THROW 51103,'Deployment lock unavailable.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.Settlement"]')=1)
    THROW 51102,'Disable settlement auditing before upgrade.',1;
 IF OBJECT_ID('dbo.ST_Settlement_Header_Apply') IS NOT NULL OR OBJECT_ID('dbo.ST_Settlement_CanRemove') IS NOT NULL
    THROW 51102,'Phase 3C already installed or object-name conflict.',1;

 -- Only the settlement branches change. Orphan historical adjustments retain their prior effect.
 DECLARE views_cursor CURSOR LOCAL FAST_FORWARD FOR SELECT n,a FROM (VALUES
  ('ST_Balance_TMP1_Plus_V','F'),('ST_Balance_TMP1_Negitave_V','A8'),
  ('ST_Balance_TMP_Plus_Negitave_V','F'),('IM_full_movement_tmp_V','AuditSettlement'),
  ('VW_InventoryTransactionFlow','AuditSettlement')) v(n,a);
 OPEN views_cursor; FETCH NEXT FROM views_cursor INTO @name,@alias;
 WHILE @@FETCH_STATUS=0
 BEGIN
  SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.'+@name));
  IF @sql IS NULL OR CHARINDEX(N'/*Audit3C*/',@sql)>0 THROW 51102,'Unexpected/already patched stock view.',1;
  SET @sql=REPLACE(@sql,N'CREATE VIEW',N'ALTER VIEW');
  SET @new=N'NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV AS AuditHeader WHERE AuditHeader.T_ID='+@alias+N'.Bill_T_ID AND AuditHeader.isVoid=1) /*Audit3C*/';
  IF @name IN('ST_Balance_TMP1_Plus_V','ST_Balance_TMP_Plus_Negitave_V')
  BEGIN
   SET @old=N'F.ST_QTY > 0';
   IF CHARINDEX(@old,@sql)=0 THROW 51102,'Positive settlement branch differs.',1;
   SET @sql=REPLACE(@sql,@old,@old+N' AND '+@new);
  END
  ELSE IF @name='ST_Balance_TMP1_Negitave_V'
  BEGIN
   SET @old=N'A8.ST_QTY < 0';
   IF CHARINDEX(@old,@sql)=0 THROW 51102,'Negative settlement branch differs.',1;
   SET @sql=REPLACE(@sql,@old,@old+N' AND '+@new);
  END
  ELSE IF @name='IM_full_movement_tmp_V'
  BEGIN
   SET @old=N'FROM   ST_QTY_Deferences WHERE';
   IF (LEN(@sql)-LEN(REPLACE(@sql,@old,N'')))/LEN(@old)<>2 THROW 51102,'Movement settlement branches differ.',1;
   SET @sql=REPLACE(@sql,@old,N'FROM dbo.ST_QTY_Deferences AS AuditSettlement WHERE '+@new+N' AND');
  END
  ELSE
  BEGIN
   SET @old=N'FROM dbo.ST_QTY_Deferences';
   IF (LEN(@sql)-LEN(REPLACE(@sql,@old,N'')))/LEN(@old)<>1 THROW 51102,'Inventory-flow settlement branch differs.',1;
   SET @sql=REPLACE(@sql,@old,@old+N' AS AuditSettlement WHERE '+@new);
  END;
  EXEC sys.sp_executesql @sql;
  FETCH NEXT FROM views_cursor INTO @name,@alias;
 END;
 CLOSE views_cursor; DEALLOCATE views_cursor;

 EXEC(N'CREATE PROCEDURE dbo.ST_Settlement_CanRemove
 @Bill_T_ID int,@LineId int=NULL,@F int OUTPUT,@Str_Name nvarchar(1500) OUTPUT
 AS
 BEGIN
  SET NOCOUNT ON;
  SET @F=0; SET @Str_Name=N'''';
  IF NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WHERE T_ID=@Bill_T_ID AND BsType_ID=39 AND isVoid=0) RETURN;
  -- Group the whole document by the actual stock key; do not test rows independently.
  ;WITH Removed AS (
   SELECT d.IM_ID,d.ST_ID,d.Valid_D,SUM(d.ST_QTY) Qty
   FROM dbo.ST_QTY_Deferences d WITH(HOLDLOCK)
   JOIN dbo.IM_Menu i ON i.IM_ID=d.IM_ID AND i.isStore IN(1,2)
   WHERE d.Bill_T_ID=@Bill_T_ID AND (@LineId IS NULL OR d.T_ID=@LineId)
   GROUP BY d.IM_ID,d.ST_ID,d.Valid_D
  )
  SELECT TOP(1) @F=1,@Str_Name=CONCAT(N''الصنف '',r.IM_ID,N''؛ المخزن '',r.ST_ID,N''؛ الصلاحية '',r.Valid_D)
  FROM Removed r OUTER APPLY (
    SELECT SUM(v.QTY) Qty FROM dbo.ST_Balance_V v WITH(HOLDLOCK)
    WHERE v.IM_ID=r.IM_ID AND v.ST_ID=r.ST_ID AND v.D_Vaild=r.Valid_D
  ) s
  WHERE r.Qty>0 AND ISNULL(s.Qty,0)-r.Qty < -0.0000001
  ORDER BY r.IM_ID,r.ST_ID,r.Valid_D;
 END');

 EXEC(N'CREATE PROCEDURE dbo.ST_Settlement_Header_Apply
 @Action varchar(24),@T_ID int OUTPUT,@User_ID int,@Date datetime=NULL,
 @Title nvarchar(350)=NULL,@About nvarchar(max)=NULL,@AllowNegative bit=0,
 @DocumentNumber int=NULL OUTPUT,@Changed bit=0 OUTPUT
 AS
 BEGIN
  SET NOCOUNT ON; SET XACT_ABORT ON;
  BEGIN TRY
   BEGIN TRANSACTION;
   SET @Changed=0; SET @DocumentNumber=NULL;
   IF @Action NOT IN(''Created'',''Approved'',''Voided'',''EditOpened'',''HeaderSaved'',''EditFinished'')
     THROW 51106,''Unknown settlement action.'',1;
   IF NOT EXISTS(SELECT 1 FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID
     WHERE u.User_ID=@User_ID AND u.is_Allow=1 AND
      ((@Action=''Voided'' AND v.PchVoid=1) OR (@Action IN(''EditOpened'',''EditFinished'') AND v.Pch_Update=1)
       OR (@Action=''HeaderSaved'' AND (v.Pch_Update=1 OR v.Update_IM_QTY=1))
       OR (@Action IN(''Created'',''Approved'') AND v.Update_IM_QTY=1)))
     THROW 51107,N''لا تملك صلاحية عملية التسوية أو المستخدم غير مسموح له.'',1;
   IF @Action IN(''Created'',''Approved'',''HeaderSaved'',''EditFinished'') AND (@Date IS NULL OR LEN(@About)>4096)
     THROW 51106,N''تاريخ أو ملاحظات التسوية غير صالحة.'',1;
   DECLARE @dep int,@void int,@first int,@last int,@locked int,@F int,@info nvarchar(1500);
   IF @Action=''Created''
   BEGIN
    IF ISNULL(@T_ID,0)<>0 THROW 51106,''Creation requires a new document.'',1;
    SELECT TOP(1) @locked=T_ID FROM dbo.Agents_Balance_MV WITH(TABLOCKX,HOLDLOCK);
    IF (SELECT COUNT(*) FROM dbo.SERVERS)<>1 THROW 51106,''Ambiguous local ID range.'',1;
    SELECT @first=START_ID,@last=END_ID FROM dbo.SERVERS;
    EXEC dbo.Agents_BalanceMV_insert @T_ID=@T_ID OUTPUT,@Receipt_Num=0,@AG_ID=1,@Pch_ID=0,@IMEX_ID=0,@Jrd_ID=0,
     @SRtn_ID=0,@PRtn_ID=0,@ST_Tran_ID=0,@EXP_ID=0,@Frm_ID=0,@ViewSB_ID=0,@InSale_ID=0,@OutSale_ID=0,@Frm_ID_M=0,
     @ST_Sett_ID=@DocumentNumber OUTPUT,@BsType_ID=39,@User_ID=@User_ID,@Date=@Date;
    IF XACT_STATE()<>1 OR @@TRANCOUNT=0 THROW 51106,''Document insertion rolled back.'',1;
    IF @T_ID IS NULL OR @DocumentNumber IS NULL OR @T_ID NOT BETWEEN @first AND @last OR @DocumentNumber NOT BETWEEN @first AND @last
       OR NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WHERE T_ID=@T_ID AND BsType_ID=39 AND User_ID=@User_ID AND isVoid=0 AND isDepended=0)
       THROW 51106,''Document insertion did not return a valid identity.'',1;
    SET @Changed=1;
   END
   ELSE
   BEGIN
    SELECT @dep=isDepended,@void=isVoid,@DocumentNumber=ST_Sett_ID FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK)
     WHERE T_ID=@T_ID AND BsType_ID=39;
    IF @dep IS NULL THROW 51106,N''مستند التسوية غير موجود.'',1;
    IF @Action=''HeaderSaved'' AND NOT EXISTS(SELECT 1 FROM dbo.Users_Validations_V
      WHERE User_ID=@User_ID AND ((@dep=0 AND Update_IM_QTY=1) OR (@dep=1 AND Pch_Update=1)))
      THROW 51107,N''لا تملك صلاحية حفظ بيانات التسوية في حالتها الحالية.'',1;
    IF @Action=''Voided'' AND @void=1 BEGIN COMMIT; RETURN; END;
    IF @void<>0 THROW 51106,N''المستند ملغى؛ لا يمكن تغييره.'',1;
    IF @Action=''Approved'' AND @dep=1 BEGIN COMMIT; RETURN; END;
    IF @Action=''Approved'' AND NOT EXISTS(SELECT 1 FROM dbo.ST_QTY_Deferences WITH(UPDLOCK,HOLDLOCK) WHERE Bill_T_ID=@T_ID)
      THROW 51106,N''لا يمكن اعتماد تسوية بلا أصناف.'',1;
    IF @Action IN(''EditOpened'',''EditFinished'') AND @dep<>1 THROW 51106,N''فتح أو إنهاء التعديل يتطلب مستندًا معتمدًا.'',1;
    IF @Action IN(''HeaderSaved'',''EditFinished'') AND @dep=1 AND NOT EXISTS(
      SELECT 1 FROM dbo.Agents_Balance_MV WHERE T_ID=@T_ID AND EditOpenedBy=@User_ID AND EditOpenedAt IS NOT NULL)
      THROW 51106,N''افتح تعديل المستند بهذا المستخدم أولًا.'',1;
   END;
   IF @Action IN(''Created'',''Approved'',''HeaderSaved'',''EditFinished'')
   BEGIN
    IF EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WHERE T_ID=@T_ID AND ([Date]<>@Date
       OR ISNULL(Receipt_Title,N'''')<>ISNULL(@Title,N'''') OR ISNULL(About,N'''')<>ISNULL(@About,N''''))) SET @Changed=1;
    UPDATE dbo.Agents_Balance_MV SET [Date]=@Date,Receipt_Title=@Title,About=@About WHERE T_ID=@T_ID;
    -- Never use a UI total; the existing recalculation preserves the saved discount.
    EXEC dbo.Recalculate_Agent_Bill_Total @T_ID;
    IF @Action=''Approved'' BEGIN UPDATE dbo.Agents_Balance_MV SET isDepended=1 WHERE T_ID=@T_ID; SET @Changed=1; END;
    IF @Action=''EditFinished'' SET @Changed=1; -- Explicit end of the UI edit session, not an inventory reversal.
   END
   ELSE IF @Action=''EditOpened''
   BEGIN
    EXEC dbo.Agents_Balance_MV_OpenForEdit @T_ID,@User_ID;
    SET @Changed=1;
   END
   ELSE IF @Action=''Voided''
   BEGIN
    IF @dep<>1 THROW 51106,N''اعتمد التسوية قبل إلغائها.'',1;
    IF EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV_RCT WHERE Receipt_Tran_ID=@T_ID)
      OR EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WHERE T_ID=@T_ID AND ISNULL(Table_ID,0)>0)
      THROW 51106,N''للتسوية ارتباطات غير متوقعة؛ تتطلب مراجعة قبل الإلغاء.'',1;
    IF @AllowNegative=0
    BEGIN
     EXEC dbo.ST_Settlement_CanRemove @T_ID,NULL,@F OUTPUT,@info OUTPUT;
     IF @F=1 BEGIN SET @info=N''الإلغاء ينتج كمية سالبة: ''+@info; THROW 51106,@info,1; END;
    END;
    EXEC dbo.Agents_Balance_MV_Cancel @T_ID,@User_ID,N''إلغاء مستند تسوية المخزون'';
    UPDATE dbo.Agents_Balance_MV SET SB_isOpen=0,isPause=NULL,Order_isDeleverd=1,Balance=NULL WHERE T_ID=@T_ID;
    SET @Changed=1;
   END;
   COMMIT;
  END TRY
  BEGIN CATCH
   IF @@TRANCOUNT>0 ROLLBACK;
   THROW;
  END CATCH;
 END');

 -- Atomic removal guard for both V2 and legacy callers; machine policy remains an explicit input.
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.ST_QTY_Deferences_Details_Delete'));
 IF @sql IS NULL OR CHARINDEX('@AllowNegative',@sql)>0 OR CHARINDEX('@ExpectedBillId',@sql)=0 THROW 51102,'Unexpected delete baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),'@Deleted bit=0 OUTPUT','@Deleted bit=0 OUTPUT, @AllowNegative bit=0');
 SET @old=N'DELETE FROM ST_QTY_Deferences WHERE T_ID=@T_ID AND Bill_T_ID=@Bill_T_ID;';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Delete statement differs.',1;
 SET @sql=REPLACE(@sql,@old,N'IF @AllowNegative=0
 BEGIN
  DECLARE @blocked int,@description nvarchar(1500);
  EXEC dbo.ST_Settlement_CanRemove @Bill_T_ID,@T_ID,@blocked OUTPUT,@description OUTPUT;
  IF @blocked=1 BEGIN SET @description=N''الحذف ينتج كمية سالبة: ''+@description; THROW 51106,@description,1; END;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;

 -- Extend only the closed audit catalogue, not the existing identity/transaction rules.
 DECLARE @ops nvarchar(max)=N'''Settlement.LineAdded'',''Settlement.LineDeleted'',''Settlement.Created'',''Settlement.Approved'',''Settlement.Voided'',''Settlement.EditOpened'',''Settlement.HeaderSaved'',''Settlement.EditFinished''';
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'@OperationCode IN(''Settlement.LineAdded'',''Settlement.LineDeleted'')';
 IF @sql IS NULL OR CHARINDEX(@old,@sql)=0 THROW 51102,'Request catalogue differs.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,N'@OperationCode IN('+@ops+N')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'@operation IN(''Settlement.LineAdded'',''Settlement.LineDeleted'') AND @domain';
 IF @sql IS NULL OR CHARINDEX(@old,@sql)=0 THROW 51102,'Event catalogue differs.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,N'@operation IN('+@ops+N') AND @domain');
 SET @old=N'IF EXISTS(SELECT 1 FROM @changes WHERE NOT (';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Field catalogue differs.',1;
 SET @new=@old+N'
 (@operation IN(''Settlement.Created'',''Settlement.Approved'',''Settlement.Voided'',''Settlement.EditOpened'',''Settlement.HeaderSaved'',''Settlement.EditFinished'') AND (
  (FieldCode IN(''DocumentNumber'',''OwnerUserId'',''AccountId'',''Approved'',''Voided'',''EditOpenedBy'',''VoidBy'',''JournalId'',''OriginalJournalId'') AND ValueType=''Integer'') OR
  (FieldCode IN(''DocumentTotal'',''DocumentPure'',''Discount'',''Balance'') AND ValueType=''Decimal'') OR
  (FieldCode IN(''DocumentDate'',''EditOpenedAt'',''VoidAt'') AND ValueType=''DateTime'') OR
  (FieldCode IN(''Title'',''About'',''VoidReason'') AND ValueType=''String'') OR
  (FieldCode IN(''NeedRepost'',''NeedCancelReverse'',''SalesOpen'',''Paused'',''OrderDelivered'') AND ValueType=''Boolean''))) OR';
 SET @sql=REPLACE(@sql,@old,@new);
 EXEC sys.sp_executesql @sql;
 COMMIT;
END TRY
BEGIN CATCH
 IF CURSOR_STATUS('local','views_cursor')>=0 CLOSE views_cursor;
 IF CURSOR_STATUS('local','views_cursor')>=-1 DEALLOCATE views_cursor;
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
