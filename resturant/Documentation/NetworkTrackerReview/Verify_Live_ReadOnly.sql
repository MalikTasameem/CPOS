/*
  CPOS Network Tracker - read-only verification, SQL Server 2014 compatible.
  Select the ACTUAL application database in SSMS before running this file.
  No USE statement: the configured database may differ from the historical dump name.
  No persistent data/schema changes; no business procedures are executed.
  Counts can scan a large audit table; run at an appropriate time, without NOLOCK.
  Metadata requires VIEW DEFINITION; missing metadata may mean missing permission.
  Run results are NOT part of the static review until this script is actually executed.
*/
SET NOCOUNT ON;

SELECT DB_NAME() AS DatabaseName, @@SERVERNAME AS ServerName,
       SERVERPROPERTY('ProductVersion') AS ProductVersion, GETDATE() AS CheckedAtServerTime;

-- 1. Actual definitions, including custom triggers/other writers that a source dump misses.
SELECT SCHEMA_NAME(o.schema_id) AS SchemaName, o.name, o.type_desc, m.definition
FROM sys.objects AS o
LEFT JOIN sys.sql_modules AS m ON m.object_id = o.object_id
WHERE o.name IN (N'Network_Edit_Tracker_insert', N'Network_Edit_Tracker_V',
                 N'Network_Edit_Tracker_V_SELECT', N'AA_GET_MAX_ID',
                 N'REDUCE_NETWORK_TRACKER', N'DeleteAll_BuysAndSales',
                 N'SalesDraft_ActionLogs_InsertBatch')
ORDER BY o.name;

SELECT SCHEMA_NAME(o.schema_id) AS SchemaName, o.name, o.type_desc
FROM sys.sql_modules AS m
JOIN sys.objects AS o ON o.object_id = m.object_id
WHERE m.definition LIKE N'%Network_Edit_Tracker%'
   OR m.definition LIKE N'%SalesDraft_ActionLogs%'
ORDER BY o.type_desc, o.name;

SELECT t.name AS TriggerName, t.is_disabled, m.definition
FROM sys.triggers AS t
LEFT JOIN sys.sql_modules AS m ON m.object_id = t.object_id
WHERE t.parent_id IN (OBJECT_ID(N'dbo.Network_Edit_Tracker'),
                      OBJECT_ID(N'dbo.SalesDraft_ActionLogs'));

-- 2. Actual schema, indexes, and foreign keys.
SELECT OBJECT_NAME(c.object_id) AS TableName, c.column_id, c.name AS ColumnName,
       TYPE_NAME(c.user_type_id) AS DataType, c.max_length AS MaxLengthBytes,
       c.precision, c.scale, c.is_nullable, c.is_identity, dc.definition AS DefaultValue
FROM sys.columns AS c
LEFT JOIN sys.default_constraints AS dc ON dc.object_id = c.default_object_id
WHERE c.object_id IN (OBJECT_ID(N'dbo.Network_Edit_Tracker'),
                      OBJECT_ID(N'dbo.SalesDraft_ActionLogs'))
ORDER BY TableName, c.column_id;

SELECT OBJECT_NAME(i.object_id) AS TableName, i.name AS IndexName, i.is_unique,
       i.type_desc, ic.key_ordinal, ic.is_included_column, c.name AS ColumnName
FROM sys.indexes AS i
JOIN sys.index_columns AS ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
JOIN sys.columns AS c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
WHERE i.object_id IN (OBJECT_ID(N'dbo.Network_Edit_Tracker'),
                      OBJECT_ID(N'dbo.SalesDraft_ActionLogs'))
ORDER BY TableName, IndexName, ic.index_column_id;

SELECT fk.name, OBJECT_NAME(fk.parent_object_id) AS ReferencingTable,
       OBJECT_NAME(fk.referenced_object_id) AS ReferencedTable, fk.is_disabled
FROM sys.foreign_keys AS fk
WHERE fk.parent_object_id = OBJECT_ID(N'dbo.Network_Edit_Tracker')
   OR fk.referenced_object_id = OBJECT_ID(N'dbo.Network_Edit_Tracker');

-- 3. Registry values: do not assume the live system has the same IDs as the dump.
IF OBJECT_ID(N'dbo.AgentBalance_Type', N'U') IS NOT NULL
    EXEC(N'SELECT id, Type_Name, RankNum FROM dbo.AgentBalance_Type ORDER BY id;');
IF OBJECT_ID(N'dbo.Operation_Types', N'U') IS NOT NULL
    EXEC(N'SELECT T_ID, Operation_Name FROM dbo.Operation_Types ORDER BY T_ID;');

-- 4. Detect records hidden by the historical INNER JOIN view.
-- Read counts, not personal notes. Individual reason counts may overlap.
IF OBJECT_ID(N'dbo.Network_Edit_Tracker', N'U') IS NOT NULL
   AND OBJECT_ID(N'dbo.Users', N'U') IS NOT NULL
   AND OBJECT_ID(N'dbo.AgentBalance_Type', N'U') IS NOT NULL
   AND OBJECT_ID(N'dbo.Operation_Types', N'U') IS NOT NULL
BEGIN
    EXEC(N'
    SELECT COUNT_BIG(*) AS BaseRows, MIN([Date]) AS FirstEventDate, MAX([Date]) AS LastEventDate
    FROM dbo.Network_Edit_Tracker;

    SELECT SUM(CONVERT(bigint, CASE WHEN u.User_ID IS NULL THEN 1 ELSE 0 END)) AS MissingUser,
           SUM(CONVERT(bigint, CASE WHEN s.id IS NULL THEN 1 ELSE 0 END)) AS MissingScreenType,
           SUM(CONVERT(bigint, CASE WHEN a.T_ID IS NULL THEN 1 ELSE 0 END)) AS MissingOperation,
           SUM(CONVERT(bigint, CASE WHEN u.User_ID IS NULL OR s.id IS NULL OR a.T_ID IS NULL THEN 1 ELSE 0 END)) AS HiddenByHistoricalJoins
    FROM dbo.Network_Edit_Tracker AS n
    LEFT JOIN dbo.Users AS u ON u.User_ID = n.User_ID
    LEFT JOIN dbo.AgentBalance_Type AS s ON s.id = n.Screen_Type
    LEFT JOIN dbo.Operation_Types AS a ON a.T_ID = n.Operation_ID;

    SELECT n.Screen_Type, n.Operation_ID, COUNT_BIG(*) AS EventCount,
           MIN(n.[Date]) AS FirstDate, MAX(n.[Date]) AS LastDate
    FROM dbo.Network_Edit_Tracker AS n
    LEFT JOIN dbo.AgentBalance_Type AS s ON s.id = n.Screen_Type
    LEFT JOIN dbo.Operation_Types AS a ON a.T_ID = n.Operation_ID
    WHERE s.id IS NULL OR a.T_ID IS NULL
    GROUP BY n.Screen_Type, n.Operation_ID ORDER BY EventCount DESC;

    SELECT COUNT_BIG(*) AS MissingOrZeroDocumentReference
    FROM dbo.Network_Edit_Tracker WHERE Bill_ID IS NULL OR LTRIM(RTRIM(Bill_ID)) IN (N'''', N''0'');

    SELECT TOP (20) [Date], COUNT_BIG(*) AS SameTimestampRows
    FROM dbo.Network_Edit_Tracker GROUP BY [Date] HAVING COUNT_BIG(*) > 1
    ORDER BY SameTimestampRows DESC, [Date];');
END;

IF OBJECT_ID(N'dbo.Network_Edit_Tracker_V', N'V') IS NOT NULL
    EXEC(N'SELECT COUNT_BIG(*) AS CurrentViewRows FROM dbo.Network_Edit_Tracker_V;');

-- 5. Synchronization metadata must be included in any future migration.
IF OBJECT_ID(N'dbo.SYNCRO_TABLE', N'U') IS NOT NULL
    EXEC(N'SELECT * FROM dbo.SYNCRO_TABLE WHERE TABLE_NAME IN (N''Network_Edit_Tracker'', N''SalesDraft_ActionLogs'');');

-- No password, connection string, or unmasked audit notes are selected by this file.
-- Procedure definitions may contain environment-specific details: review before sharing.
