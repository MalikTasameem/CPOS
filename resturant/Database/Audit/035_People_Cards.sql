/* SQL Server 2014. People cards + V2 user activity guard. CODE ONLY: not deployed or live-tested. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51135,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51135,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51135,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51135,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.QuoteLines"]')=1) THROW 51135,'Apply stage 034 first; source mismatch.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51135,'Incomplete audit requests.',1;
 -- Reviewed existing triggers are accounting-integrity guards; preserve them unchanged.
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Agents'),OBJECT_ID('Users'),OBJECT_ID('ACC_ENTITY_LINKS'),OBJECT_ID('ACCOUNTS_TREE'))
  AND NOT(
   (name=N'TR_Agents_Prevent_Change_TypeID_WithMovement' AND parent_id=OBJECT_ID('Agents')
    AND ISNULL(CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(max),OBJECT_DEFINITION(object_id))),2),'')='32C5475463AA1F4743E0BE1044619CA3B06289A89AF7BB67BD45C6C28F50C0E9')
   OR (name=N'TR_ACCOUNTS_TREE_Prevent_Delete_Linked_Account' AND parent_id=OBJECT_ID('ACCOUNTS_TREE')
    AND ISNULL(CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(max),OBJECT_DEFINITION(object_id))),2),'')='C4CB887786E5509F7CC6C4B33EB1D55302A5AD58E29605E527F57CE63967699A')
   OR (name=N'TR_ACCOUNTS_TREE_Prevent_Update_ACC_CODE_Linked' AND parent_id=OBJECT_ID('ACCOUNTS_TREE')
    AND ISNULL(CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(max),OBJECT_DEFINITION(object_id))),2),'')='5BFD1142C414C1609BEDE0EA261C57E34E2E3980D857F0F949A88ABFB20A8C57')
   OR (name=N'TR_ACCOUNTS_TREE_Prevent_Update_Parent_WithMovement' AND parent_id=OBJECT_ID('ACCOUNTS_TREE')
    AND ISNULL(CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(max),OBJECT_DEFINITION(object_id))),2),'')='8C895EC953E5CD1408FCD1F992471D5309D045182C3297D8042288F2E9F1D93C')
  )) THROW 51135,'Review people/accounting triggers.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Agent.Created',@sql)>0 OR CHARINDEX('Quote.LineAdded',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51135,'Unexpected begin baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Agent.Created'',''Agent.Updated'',''Agent.Deleted'',''User.Created'',''User.Updated'',''User.Deleted'',''User.CredentialsChanged'') AND @DomainCode=''People.Cards'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Agent.Created',@sql)>0 OR CHARINDEX('Quote.LineAdded',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51135,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Agent.Created'',''Agent.Updated'',''Agent.Deleted'',''User.Created'',''User.Updated'',''User.Deleted'',''User.CredentialsChanged'') AND @domain=''People.Cards'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51135,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Agent.Created'',''Agent.Updated'',''Agent.Deleted'',''User.Created'',''User.Updated'',''User.Deleted'',''User.CredentialsChanged'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51135,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditPeopleCards035: closed fields; no credential values, tokens or hashes.
 IF @operation IN(''Agent.Created'',''Agent.Updated'',''Agent.Deleted'',''User.Created'',''User.Updated'',''User.Deleted'',''User.CredentialsChanged'')
 BEGIN
  DECLARE @pId nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),@pEntity varchar(64)=@EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)''),@pScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),@pCreate bit=CASE WHEN @operation IN(''Agent.Created'',''User.Created'') THEN 1 ELSE 0 END,@pDelete bit=CASE WHEN @operation IN(''Agent.Deleted'',''User.Deleted'') THEN 1 ELSE 0 END,@pAgent bit=CASE WHEN @operation LIKE ''Agent.%'' THEN 1 ELSE 0 END;
  IF ISNULL(TRY_CONVERT(int,@pId),0)<=0 OR @pEntity<>CASE WHEN @pAgent=1 THEN ''PeopleAgent'' ELSE ''PeopleUser'' END
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 OR @EventXml.value(''count(/Event/Document)'',''int'')<>1
   OR @EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')<>@pId THROW 51135,''Invalid people card identity.'',1;
  IF NOT((@operation=''Agent.Created'' AND @pScreen IN(''Agent'',''AddFastAgent'',''Sales'',''Pch'',''ViewBill'')) OR (@operation IN(''Agent.Updated'',''Agent.Deleted'') AND @pScreen=''Agent'') OR (@operation IN(''User.Created'',''User.Updated'',''User.Deleted'') AND @pScreen=''users'') OR (@operation=''User.CredentialsChanged'' AND @pScreen=''UpdateUserPass'')) THROW 51135,''Unexpected people screen.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@pEntity AND EntityId=CASE WHEN @pCreate=1 THEN ''New'' ELSE @pId END) THROW 51135,''People request identity mismatch.'',1;
  DECLARE @pFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40),O tinyint);
  INSERT @pFields VALUES
   (''PeopleAgent'',''AgentName'',''String'',''nvarchar(500)'',1),
   (''PeopleAgent'',''AgentBarcode'',''String'',''nvarchar(50)'',1),
   (''PeopleAgent'',''AgentPhone'',''String'',''nvarchar(60)'',1),
   (''PeopleAgent'',''AgentAddress'',''String'',''nvarchar(max)'',1),
   (''PeopleAgent'',''AgentDebit'',''Decimal'',''numeric(18,2)'',1),
   (''PeopleAgent'',''AgentCredit'',''Decimal'',''numeric(18,2)'',1),
   (''PeopleAgent'',''AgentBalance'',''Decimal'',''numeric(18,2)'',1),
   (''PeopleAgent'',''AgentDefault'',''Boolean'',''bit'',1),
   (''PeopleAgent'',''AgentMonthly'',''Boolean'',''bit'',1),
   (''PeopleAgent'',''AgentSalary'',''Decimal'',''money'',1),
   (''PeopleAgent'',''AgentType'',''Integer'',''int'',1),
   (''PeopleAgent'',''AgentPhotoDigest'',''String'',''image'',3),
   (''PeopleAgent'',''AgentPhotoBytes'',''Integer'',''image'',3),
   (''PeopleAgent'',''AgentMaxDebit'',''Decimal'',''numeric(18,2)'',1),
   (''PeopleAgent'',''AgentEmail'',''String'',''nvarchar(500)'',1),
   (''PeopleAgent'',''AgentPaused'',''Boolean'',''bit'',1),
   (''PeopleAgent'',''AgentCurrency'',''Integer'',''int'',1),
   (''PeopleAgent'',''AgentTemporaryBalance'',''Decimal'',''numeric(18,2)'',1),
   (''PeopleAgent'',''AgentAutoPaid'',''Boolean'',''bit'',1),
   (''PeopleAgent'',''AgentStarted'',''Date'',''date'',1),
   (''PeopleAgent'',''AgentTreeCode'',''String'',''varchar(50)'',1),
   (''PeopleAgent'',''AgentLinkId'',''Integer'',''int'',1),
   (''PeopleUser'',''UserName'',''String'',''nvarchar(200)'',1),
   (''PeopleUser'',''UserAdmin'',''Boolean'',''bit'',1),
   (''PeopleUser'',''UserTable'',''Integer'',''int'',1),
   (''PeopleUser'',''UserCustomerScreen'',''Boolean'',''bit'',1),
   (''PeopleUser'',''UserAgent'',''Integer'',''int'',1),
   (''PeopleUser'',''UserAllowed'',''Integer'',''int'',1),
   (''PeopleUser'',''UserValidation'',''Integer'',''int'',1),
   (''PeopleUser'',''UserTreasury'',''Integer'',''int'',1),
   (''PeopleUser'',''UserPhoneEnabled'',''Integer'',''int'',1),
   (''PeopleAccountLink'',''LinkEntityType'',''Integer'',''tinyint'',1),
   (''PeopleAccountLink'',''LinkSourceTable'',''String'',''sysname'',1),
   (''PeopleAccountLink'',''LinkSourceId'',''Integer'',''bigint'',1),
   (''PeopleAccountLink'',''LinkAccountCode'',''String'',''varchar(50)'',1),
   (''PeopleAccountLink'',''LinkParentCode'',''String'',''varchar(50)'',1),
   (''PeopleAccountLink'',''LinkName'',''String'',''nvarchar(1000)'',1),
   (''PeopleAccountLink'',''LinkAutomatic'',''Boolean'',''bit'',1),
   (''PeopleAccountLink'',''LinkActive'',''Boolean'',''bit'',1),
   (''PeopleAccountLink'',''LinkLocked'',''Boolean'',''bit'',1),
   (''PeopleAccountLink'',''LinkCreated'',''DateTime'',''datetime'',1),
   (''PeopleAccountLink'',''LinkCreatedBy'',''Integer'',''int'',1),
   (''PeopleAccountLink'',''LinkUpdated'',''DateTime'',''datetime'',1),
   (''PeopleAccountLink'',''LinkUpdatedBy'',''Integer'',''int'',1),
   (''PeopleChartAccount'',''ChartCode'',''String'',''varchar(40)'',1),
   (''PeopleChartAccount'',''ChartLevel'',''Integer'',''int'',1),
   (''PeopleChartAccount'',''ChartParent'',''String'',''varchar(40)'',1),
   (''PeopleChartAccount'',''ChartName'',''String'',''nvarchar(2500)'',1),
   (''PeopleChartAccount'',''ChartType'',''String'',''nvarchar(50)'',1),
   (''PeopleChartAccount'',''ChartCategory'',''String'',''nvarchar(500)'',1),
   (''PeopleChartAccount'',''ChartClosing'',''Boolean'',''bit'',1),
   (''PeopleChartAccount'',''ChartHint'',''String'',''nvarchar(500)'',1),
   (''PeopleChartAccount'',''ChartDebit'',''Float'',''float(53)'',1),
   (''PeopleChartAccount'',''ChartCredit'',''Float'',''float(53)'',1),
   (''PeopleChartAccount'',''ChartNature'',''String'',''char(1)'',1),
   (''PeopleChartAccount'',''ChartBalanceView'',''Boolean'',''bit'',1),
   (''PeopleChartAccount'',''ChartCashflow'',''Integer'',''int'',1),
   (''PeopleChartAccount'',''ChartSide'',''String'',''varchar(500)'',1),
   (''PeopleChartAccount'',''ChartLocked'',''Boolean'',''bit'',1),
   (''PeopleChartAccount'',''ChartDigits'',''Integer'',''int'',1),
   (''PeopleChartAccount'',''ChartOpeningDebit'',''Float'',''float(53)'',1),
   (''PeopleChartAccount'',''ChartOpeningCredit'',''Float'',''float(53)'',1),
   (''PeopleChartAccount'',''ChartEndingDebit'',''Float'',''float(53)'',1),
   (''PeopleChartAccount'',''ChartEndingCredit'',''Float'',''float(53)'',1),
   (''PeopleChartAccount'',''ChartSourceType'',''Integer'',''tinyint'',1),
   (''PeopleCredentialAction'',''CredentialAction'',''String'',''nvarchar(50)'',3);
  IF EXISTS(SELECT 1 FROM @changes c WHERE NOT EXISTS(SELECT 1 FROM @pFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType AND f.O=c.ValueOrigin)
    OR ISNULL(TRY_CONVERT(int,c.EntityId),0)<=0
    OR (@pAgent=1 AND c.EntityType NOT IN(''PeopleAgent'',''PeopleAccountLink'',''PeopleChartAccount''))
    OR (@pAgent=0 AND c.EntityType NOT IN(''PeopleUser'',''PeopleCredentialAction''))
    OR (@operation=''User.Deleted'' AND c.EntityType=''PeopleCredentialAction'')) THROW 51135,''Unexpected people field or sensitive field.'',1;
  IF EXISTS(SELECT EntityType,EntityId,FieldCode FROM @changes GROUP BY EntityType,EntityId,FieldCode HAVING COUNT(*)<>1)
   OR EXISTS(SELECT c.EntityType,c.EntityId FROM @changes c GROUP BY c.EntityType,c.EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @pFields f WHERE f.E=c.EntityType)) THROW 51135,''Incomplete people row snapshot.'',1;
  IF @EventOrdinal=1 AND ((SELECT COUNT(*) FROM @changes)<>(SELECT COUNT(*) FROM @pFields WHERE E=@pEntity) OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>@pEntity OR EntityId<>@pId)) THROW 51135,''First event requires full card row.'',1;
  IF @EventOrdinal>1 AND EXISTS(SELECT 1 FROM @changes WHERE EntityType=@pEntity) THROW 51135,''Card header repeated.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=@pEntity AND (EntityId<>@pId OR
   (@pCreate=1 AND (BeforeState<>1 OR AfterState NOT IN(2,3))) OR
   (@pDelete=1 AND (BeforeState NOT IN(2,3) OR AfterState<>1)) OR
   (@pCreate=0 AND @pDelete=0 AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))))) THROW 51135,''Invalid people row states.'',1;
  IF @operation=''User.CredentialsChanged'' AND EXISTS(SELECT 1 FROM @changes WHERE EntityType=''PeopleUser'' AND (BeforeState<>AfterState OR ISNULL(DATALENGTH(BeforeValue),-1)<>ISNULL(DATALENGTH(AfterValue),-1) OR ISNULL(BeforeValue,N'''') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'''') COLLATE Latin1_General_100_BIN2)) THROW 51135,''Credential operation changed public fields.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''PeopleCredentialAction'' AND (EntityId<>@pId OR BeforeState<>1 OR AfterState<>3 OR AfterValue<>CASE WHEN @operation=''User.Created'' THEN ''Created'' WHEN @operation=''User.Updated'' THEN ''WebCredentialAndTokenRefreshed'' WHEN @operation=''User.CredentialsChanged'' THEN ''PasswordChanged'' ELSE '''' END)) THROW 51135,''Invalid credential action metadata.'',1;
  IF @pAgent=1 AND ((@pDelete=1 AND EXISTS(SELECT 1 FROM dbo.Agents WITH(HOLDLOCK) WHERE AG_ID=TRY_CONVERT(int,@pId))) OR (@pDelete=0 AND NOT EXISTS(SELECT 1 FROM dbo.Agents WITH(HOLDLOCK) WHERE AG_ID=TRY_CONVERT(int,@pId)))) THROW 51135,''Agent save outcome mismatch.'',1;
  IF @pAgent=0 AND ((@pDelete=1 AND EXISTS(SELECT 1 FROM dbo.Users WITH(HOLDLOCK) WHERE User_ID=TRY_CONVERT(int,@pId))) OR (@pDelete=0 AND NOT EXISTS(SELECT 1 FROM dbo.Users WITH(HOLDLOCK) WHERE User_ID=TRY_CONVERT(int,@pId)))) THROW 51135,''User save outcome mismatch.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType IN(''PeopleAccountLink'',''PeopleChartAccount'') AND (BeforeState NOT IN(1,2,3) OR AfterState NOT IN(1,2,3) OR (BeforeState=1 AND AfterState=1))) THROW 51135,''Invalid accounting side-effect states.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''PeopleAccountLink'' AND
   ((FieldCode=''LinkSourceId'' AND CASE WHEN AfterState=1 THEN BeforeValue ELSE AfterValue END<>@pId)
    OR (FieldCode=''LinkSourceTable'' AND CASE WHEN AfterState=1 THEN BeforeValue ELSE AfterValue END<>N''Agents'')
    OR (FieldCode=''LinkEntityType'' AND ISNULL(TRY_CONVERT(int,CASE WHEN AfterState=1 THEN BeforeValue ELSE AfterValue END),0) NOT IN(1,2,3,4)))) THROW 51135,''Link belongs to another entity.'',1;
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType=''PeopleChartAccount'' AND c.FieldCode=''ChartCode'' AND NOT EXISTS(
     SELECT 1 FROM dbo.Network_Audit_Event e JOIN dbo.Network_Audit_Change d ON d.EventId=e.EventId
     WHERE e.SourceId=@SourceId AND e.RequestId=@RequestId AND e.EntityId=@pId AND d.FieldCode IN(''AgentTreeCode'',''LinkAccountCode'')
      AND (LTRIM(RTRIM(d.BeforeValue))=LTRIM(RTRIM(CASE WHEN c.AfterState=1 THEN c.BeforeValue ELSE c.AfterValue END)) OR LTRIM(RTRIM(d.AfterValue))=LTRIM(RTRIM(CASE WHEN c.AfterState=1 THEN c.BeforeValue ELSE c.AfterValue END))))) THROW 51135,''Chart effect not linked to this card.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[Agents] p WITH(HOLDLOCK) ON p.[AG_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''PeopleAgent'' AND ((c.AfterState=1 AND p.[AG_ID] IS NOT NULL) OR (c.AfterState IN(2,3) AND (p.[AG_ID] IS NULL OR
    (c.FieldCode=''AgentName'' AND ((c.AfterState=2 AND p.[Ag_name] IS NOT NULL) OR (c.AfterState=3 AND (p.[Ag_name] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[Ag_name])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Ag_name]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''AgentBarcode'' AND ((c.AfterState=2 AND p.[Barcode] IS NOT NULL) OR (c.AfterState=3 AND (p.[Barcode] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[Barcode])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Barcode]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''AgentPhone'' AND ((c.AfterState=2 AND p.[Ag_phone] IS NOT NULL) OR (c.AfterState=3 AND (p.[Ag_phone] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[Ag_phone])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Ag_phone]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''AgentAddress'' AND ((c.AfterState=2 AND p.[Address] IS NOT NULL) OR (c.AfterState=3 AND (p.[Address] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[Address])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Address]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''AgentDebit'' AND ((c.AfterState=2 AND p.[T_Debit] IS NOT NULL) OR (c.AfterState=3 AND (p.[T_Debit] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[T_Debit])))) OR
    (c.FieldCode=''AgentCredit'' AND ((c.AfterState=2 AND p.[T_Credit] IS NOT NULL) OR (c.AfterState=3 AND (p.[T_Credit] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[T_Credit])))) OR
    (c.FieldCode=''AgentBalance'' AND ((c.AfterState=2 AND p.[T_Balance] IS NOT NULL) OR (c.AfterState=3 AND (p.[T_Balance] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[T_Balance])))) OR
    (c.FieldCode=''AgentDefault'' AND ((c.AfterState=2 AND p.[isDefaultAG] IS NOT NULL) OR (c.AfterState=3 AND (p.[isDefaultAG] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[isDefaultAG])))) OR
    (c.FieldCode=''AgentMonthly'' AND ((c.AfterState=2 AND p.[isMonthEmp] IS NOT NULL) OR (c.AfterState=3 AND (p.[isMonthEmp] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[isMonthEmp])))) OR
    (c.FieldCode=''AgentSalary'' AND ((c.AfterState=2 AND p.[AG_Salary] IS NOT NULL) OR (c.AfterState=3 AND (p.[AG_Salary] IS NULL OR TRY_CONVERT(money,c.AfterValue) IS NULL OR TRY_CONVERT(money,c.AfterValue)<>p.[AG_Salary])))) OR
    (c.FieldCode=''AgentType'' AND ((c.AfterState=2 AND p.[Type_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Type_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Type_ID])))) OR
    (c.FieldCode=''AgentPhotoBytes'' AND ((c.AfterState=2 AND DATALENGTH(p.[AG_img]) IS NOT NULL) OR (c.AfterState=3 AND (DATALENGTH(p.[AG_img]) IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>DATALENGTH(p.[AG_img]))))) OR
    (c.FieldCode=''AgentMaxDebit'' AND ((c.AfterState=2 AND p.[Max_Debit] IS NOT NULL) OR (c.AfterState=3 AND (p.[Max_Debit] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[Max_Debit])))) OR
    (c.FieldCode=''AgentEmail'' AND ((c.AfterState=2 AND p.[E_mail] IS NOT NULL) OR (c.AfterState=3 AND (p.[E_mail] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[E_mail])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[E_mail]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''AgentPaused'' AND ((c.AfterState=2 AND p.[is_Emp_Pause] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Emp_Pause] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[is_Emp_Pause])))) OR
    (c.FieldCode=''AgentCurrency'' AND ((c.AfterState=2 AND p.[Cr_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Cr_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Cr_ID])))) OR
    (c.FieldCode=''AgentTemporaryBalance'' AND ((c.AfterState=2 AND p.[T_Balance_TMP] IS NOT NULL) OR (c.AfterState=3 AND (p.[T_Balance_TMP] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[T_Balance_TMP])))) OR
    (c.FieldCode=''AgentAutoPaid'' AND ((c.AfterState=2 AND p.[is_Auto_Pied] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Auto_Pied] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[is_Auto_Pied])))) OR
    (c.FieldCode=''AgentStarted'' AND ((c.AfterState=2 AND p.[DATE_START] IS NOT NULL) OR (c.AfterState=3 AND (p.[DATE_START] IS NULL OR TRY_CONVERT(date,c.AfterValue) IS NULL OR TRY_CONVERT(date,c.AfterValue)<>p.[DATE_START])))) OR
    (c.FieldCode=''AgentTreeCode'' AND ((c.AfterState=2 AND p.[Tree_Code] IS NOT NULL) OR (c.AfterState=3 AND (p.[Tree_Code] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[Tree_Code])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Tree_Code]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''AgentLinkId'' AND ((c.AfterState=2 AND p.[ACC_LINK_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_LINK_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[ACC_LINK_ID])))))))) THROW 51135,''PeopleAgent snapshot differs from stored row.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[Users] p WITH(HOLDLOCK) ON p.[User_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''PeopleUser'' AND ((c.AfterState=1 AND p.[User_ID] IS NOT NULL) OR (c.AfterState IN(2,3) AND (p.[User_ID] IS NULL OR
    (c.FieldCode=''UserName'' AND ((c.AfterState=2 AND p.[UserName] IS NOT NULL) OR (c.AfterState=3 AND (p.[UserName] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[UserName])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[UserName]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''UserAdmin'' AND ((c.AfterState=2 AND p.[isAdmin] IS NOT NULL) OR (c.AfterState=3 AND (p.[isAdmin] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[isAdmin])))) OR
    (c.FieldCode=''UserTable'' AND ((c.AfterState=2 AND p.[TB_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[TB_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[TB_ID])))) OR
    (c.FieldCode=''UserCustomerScreen'' AND ((c.AfterState=2 AND p.[isCostmerScreen] IS NOT NULL) OR (c.AfterState=3 AND (p.[isCostmerScreen] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[isCostmerScreen])))) OR
    (c.FieldCode=''UserAgent'' AND ((c.AfterState=2 AND p.[AG_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[AG_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[AG_ID])))) OR
    (c.FieldCode=''UserAllowed'' AND ((c.AfterState=2 AND p.[is_Allow] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Allow] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[is_Allow])))) OR
    (c.FieldCode=''UserValidation'' AND ((c.AfterState=2 AND p.[Valid_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Valid_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Valid_ID])))) OR
    (c.FieldCode=''UserTreasury'' AND ((c.AfterState=2 AND p.[Tr_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Tr_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Tr_ID])))) OR
    (c.FieldCode=''UserPhoneEnabled'' AND ((c.AfterState=2 AND p.[isPone_Users] IS NOT NULL) OR (c.AfterState=3 AND (p.[isPone_Users] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isPone_Users])))))))) THROW 51135,''PeopleUser snapshot differs from stored row.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[ACC_ENTITY_LINKS] p WITH(HOLDLOCK) ON p.[LINK_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''PeopleAccountLink'' AND ((c.AfterState=1 AND p.[LINK_ID] IS NOT NULL) OR (c.AfterState IN(2,3) AND (p.[LINK_ID] IS NULL OR
    (c.FieldCode=''LinkEntityType'' AND ((c.AfterState=2 AND p.[ENTITY_TYPE] IS NOT NULL) OR (c.AfterState=3 AND (p.[ENTITY_TYPE] IS NULL OR TRY_CONVERT(tinyint,c.AfterValue) IS NULL OR TRY_CONVERT(tinyint,c.AfterValue)<>p.[ENTITY_TYPE])))) OR
    (c.FieldCode=''LinkSourceTable'' AND ((c.AfterState=2 AND p.[SOURCE_TABLE] IS NOT NULL) OR (c.AfterState=3 AND (p.[SOURCE_TABLE] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[SOURCE_TABLE])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[SOURCE_TABLE]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''LinkSourceId'' AND ((c.AfterState=2 AND p.[SOURCE_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[SOURCE_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[SOURCE_ID])))) OR
    (c.FieldCode=''LinkAccountCode'' AND ((c.AfterState=2 AND p.[ACC_CODE] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_CODE] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[ACC_CODE])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ACC_CODE]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''LinkParentCode'' AND ((c.AfterState=2 AND p.[PARENT_ACC_CODE] IS NOT NULL) OR (c.AfterState=3 AND (p.[PARENT_ACC_CODE] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[PARENT_ACC_CODE])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[PARENT_ACC_CODE]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''LinkName'' AND ((c.AfterState=2 AND p.[ENTITY_NAME] IS NOT NULL) OR (c.AfterState=3 AND (p.[ENTITY_NAME] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[ENTITY_NAME])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ENTITY_NAME]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''LinkAutomatic'' AND ((c.AfterState=2 AND p.[IS_AUTO_CREATED] IS NOT NULL) OR (c.AfterState=3 AND (p.[IS_AUTO_CREATED] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[IS_AUTO_CREATED])))) OR
    (c.FieldCode=''LinkActive'' AND ((c.AfterState=2 AND p.[IS_ACTIVE] IS NOT NULL) OR (c.AfterState=3 AND (p.[IS_ACTIVE] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[IS_ACTIVE])))) OR
    (c.FieldCode=''LinkLocked'' AND ((c.AfterState=2 AND p.[IS_LOCKED] IS NOT NULL) OR (c.AfterState=3 AND (p.[IS_LOCKED] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[IS_LOCKED])))) OR
    (c.FieldCode=''LinkCreated'' AND ((c.AfterState=2 AND p.[CREATED_AT] IS NOT NULL) OR (c.AfterState=3 AND (p.[CREATED_AT] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[CREATED_AT])))) OR
    (c.FieldCode=''LinkCreatedBy'' AND ((c.AfterState=2 AND p.[CREATED_BY] IS NOT NULL) OR (c.AfterState=3 AND (p.[CREATED_BY] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[CREATED_BY])))) OR
    (c.FieldCode=''LinkUpdated'' AND ((c.AfterState=2 AND p.[UPDATED_AT] IS NOT NULL) OR (c.AfterState=3 AND (p.[UPDATED_AT] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[UPDATED_AT])))) OR
    (c.FieldCode=''LinkUpdatedBy'' AND ((c.AfterState=2 AND p.[UPDATED_BY] IS NOT NULL) OR (c.AfterState=3 AND (p.[UPDATED_BY] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[UPDATED_BY])))))))) THROW 51135,''PeopleAccountLink snapshot differs from stored row.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[ACCOUNTS_TREE] p WITH(HOLDLOCK) ON p.[T_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''PeopleChartAccount'' AND ((c.AfterState=1 AND p.[T_ID] IS NOT NULL) OR (c.AfterState IN(2,3) AND (p.[T_ID] IS NULL OR
    (c.FieldCode=''ChartCode'' AND ((c.AfterState=2 AND p.[ACC_CODE] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_CODE] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[ACC_CODE])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ACC_CODE]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ChartLevel'' AND ((c.AfterState=2 AND p.[ACC_LEVEL] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_LEVEL] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[ACC_LEVEL])))) OR
    (c.FieldCode=''ChartParent'' AND ((c.AfterState=2 AND p.[ACC_PARENT] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_PARENT] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[ACC_PARENT])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ACC_PARENT]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ChartName'' AND ((c.AfterState=2 AND p.[ACC_NAME] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_NAME] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[ACC_NAME])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ACC_NAME]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ChartType'' AND ((c.AfterState=2 AND p.[ACC_TYPE] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_TYPE] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[ACC_TYPE])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ACC_TYPE]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ChartCategory'' AND ((c.AfterState=2 AND p.[ACC_CAT] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_CAT] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[ACC_CAT])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ACC_CAT]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ChartClosing'' AND ((c.AfterState=2 AND p.[ACC_CLOSING] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_CLOSING] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[ACC_CLOSING])))) OR
    (c.FieldCode=''ChartHint'' AND ((c.AfterState=2 AND p.[ACC_HINT] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_HINT] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[ACC_HINT])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ACC_HINT]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ChartDebit'' AND ((c.AfterState=2 AND p.[DEBIT] IS NOT NULL) OR (c.AfterState=3 AND (p.[DEBIT] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[DEBIT])))) OR
    (c.FieldCode=''ChartCredit'' AND ((c.AfterState=2 AND p.[CREDIT] IS NOT NULL) OR (c.AfterState=3 AND (p.[CREDIT] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[CREDIT])))) OR
    (c.FieldCode=''ChartNature'' AND ((c.AfterState=2 AND p.[ACC_NATURAL] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_NATURAL] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[ACC_NATURAL])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ACC_NATURAL]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ChartBalanceView'' AND ((c.AfterState=2 AND p.[is_Balance_View] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Balance_View] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[is_Balance_View])))) OR
    (c.FieldCode=''ChartCashflow'' AND ((c.AfterState=2 AND p.[cash_flow_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[cash_flow_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[cash_flow_ID])))) OR
    (c.FieldCode=''ChartSide'' AND ((c.AfterState=2 AND p.[SIDE] IS NOT NULL) OR (c.AfterState=3 AND (p.[SIDE] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[SIDE])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[SIDE]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ChartLocked'' AND ((c.AfterState=2 AND p.[is_Lock_Trans] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Lock_Trans] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[is_Lock_Trans])))) OR
    (c.FieldCode=''ChartDigits'' AND ((c.AfterState=2 AND p.[ACC_DIGIT] IS NOT NULL) OR (c.AfterState=3 AND (p.[ACC_DIGIT] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[ACC_DIGIT])))) OR
    (c.FieldCode=''ChartOpeningDebit'' AND ((c.AfterState=2 AND p.[Start_DEBIT] IS NOT NULL) OR (c.AfterState=3 AND (p.[Start_DEBIT] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[Start_DEBIT])))) OR
    (c.FieldCode=''ChartOpeningCredit'' AND ((c.AfterState=2 AND p.[Start_CREDIT] IS NOT NULL) OR (c.AfterState=3 AND (p.[Start_CREDIT] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[Start_CREDIT])))) OR
    (c.FieldCode=''ChartEndingDebit'' AND ((c.AfterState=2 AND p.[End_DEBIT] IS NOT NULL) OR (c.AfterState=3 AND (p.[End_DEBIT] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[End_DEBIT])))) OR
    (c.FieldCode=''ChartEndingCredit'' AND ((c.AfterState=2 AND p.[End_CREDIT] IS NOT NULL) OR (c.AfterState=3 AND (p.[End_CREDIT] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[End_CREDIT])))) OR
    (c.FieldCode=''ChartSourceType'' AND ((c.AfterState=2 AND p.[AccountSourceType] IS NOT NULL) OR (c.AfterState=3 AND (p.[AccountSourceType] IS NULL OR TRY_CONVERT(tinyint,c.AfterValue) IS NULL OR TRY_CONVERT(tinyint,c.AfterValue)<>p.[AccountSourceType])))))))) THROW 51135,''PeopleChartAccount snapshot differs from stored row.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 -- Replace only the old-audit dependency. All other existing deletion guards remain unchanged.
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.User_Delete'));
 SET @old=N'AND ( SELECT COUNT(user_id) FROM Network_Edit_Tracker WHERE user_id = @user_id )  = 0';
 IF @sql IS NULL OR CHARINDEX('PeopleCards035:',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51135,'Unexpected User_Delete baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,N'-- PeopleCards035: immutable V2 activity prevents deleting its actor.
AND NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WITH(HOLDLOCK) WHERE ActorUserId=@user_id)');
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="People.Cards"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="People.Cards"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
