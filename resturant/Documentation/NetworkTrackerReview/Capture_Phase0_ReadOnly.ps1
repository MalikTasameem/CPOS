param(
    [string]$SettingsPath = (Join-Path $PSScriptRoot '..\..\bin\Debug\Settings Files\BackUpSettings.AppSettings'),
    [string]$OutputPath = (Join-Path $PSScriptRoot 'Phase0_Live_Metadata.json')
)
$ErrorActionPreference = 'Stop'
# Reads the configured application's database. Never logs the connection string or credentials.
# All database commands below are SELECTs. No business procedures or migrations are executed.
$settings = @{}
foreach ($line in [IO.File]::ReadLines((Resolve-Path $SettingsPath))) {
    $separator = $line.IndexOf(':')
    if ($separator -gt 0) { $settings[$line.Substring(0, $separator)] = $line.Substring($separator + 1) }
}
if ($settings['IsAttachDB'] -eq 'True') { throw 'Attached database mode is not supported by this read-only capture; no attachment attempted.' }
$builder = [System.Data.SqlClient.SqlConnectionStringBuilder]::new()
$builder['Data Source'] = $settings['S_SERVER'].Trim()
$builder['Initial Catalog'] = $settings['DataBase'].Trim()
$builder['Connect Timeout'] = 10
$builder['Application Name'] = 'CPOS Network Tracker Phase0 ReadOnly'
if ($settings['DB_Authentication'] -eq '0') { $builder['Integrated Security'] = $true }
else { $builder['User ID'] = $settings['DB_UName']; $builder['Password'] = $settings['DB_Pass'] }
$connection = [System.Data.SqlClient.SqlConnection]::new($builder.ConnectionString)
function Read-Rows([string]$Sql) {
    $command = $connection.CreateCommand()
    $command.CommandText = $Sql
    $command.CommandTimeout = 30
    try {
        $reader = $command.ExecuteReader()
        try {
            while ($reader.Read()) {
                $row = [ordered]@{}
                for ($i = 0; $i -lt $reader.FieldCount; $i++) {
                    $row[$reader.GetName($i)] = if ($reader.IsDBNull($i)) { $null } else { $reader.GetValue($i) }
                }
                [pscustomobject]$row
            }
        } finally { $reader.Dispose() }
    } finally { $command.Dispose() }
}
try {
    $connection.Open()
    $capture = [ordered]@{
        CapturedAt = (Get-Date -Format 'o')
        ConfiguredServer = $settings['S_SERVER']
        Scope = 'Network tracker only; draft/offline log development excluded.'
        Database = @(Read-Rows "SELECT DB_NAME() AS DatabaseName, @@SERVERNAME AS ServerName, CONVERT(nvarchar(128),SERVERPROPERTY('ProductVersion')) AS ProductVersion, GETDATE() AS ServerTime;")
        Definitions = @(Read-Rows @'
SELECT o.name, o.type_desc, o.modify_date, m.definition
FROM sys.objects o JOIN sys.sql_modules m ON m.object_id=o.object_id
WHERE o.name IN (N'Network_Edit_Tracker_insert',N'Network_Edit_Tracker_V',N'Network_Edit_Tracker_V_SELECT',N'AA_GET_MAX_ID',N'REDUCE_NETWORK_TRACKER') ORDER BY o.name;
'@)
        ReferencingModules = @(Read-Rows @'
SELECT o.name, o.type_desc FROM sys.sql_modules m JOIN sys.objects o ON o.object_id=m.object_id
WHERE m.definition LIKE N'%Network_Edit_Tracker%' ORDER BY o.name;
'@)
        Columns = @(Read-Rows @'
SELECT c.column_id,c.name,TYPE_NAME(c.user_type_id) AS DataType,c.max_length,c.precision,c.scale,c.is_nullable,c.is_identity
FROM sys.columns c WHERE c.object_id=OBJECT_ID(N'dbo.Network_Edit_Tracker') ORDER BY c.column_id;
'@)
        Indexes = @(Read-Rows @'
SELECT i.name,i.is_unique,i.type_desc,ic.key_ordinal,c.name AS ColumnName
FROM sys.indexes i JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id
JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id
WHERE i.object_id=OBJECT_ID(N'dbo.Network_Edit_Tracker') ORDER BY i.name,ic.index_column_id;
'@)
        Triggers = @(Read-Rows "SELECT name,is_disabled FROM sys.triggers WHERE parent_id=OBJECT_ID(N'dbo.Network_Edit_Tracker');")
        ForeignKeys = @(Read-Rows "SELECT name,OBJECT_NAME(parent_object_id) AS ParentTable,OBJECT_NAME(referenced_object_id) AS ReferencedTable,is_disabled FROM sys.foreign_keys WHERE parent_object_id=OBJECT_ID(N'dbo.Network_Edit_Tracker') OR referenced_object_id=OBJECT_ID(N'dbo.Network_Edit_Tracker');")
        ScreenTypes = @(Read-Rows 'SELECT id,Type_Name,RankNum FROM dbo.AgentBalance_Type ORDER BY id;')
        Operations = @(Read-Rows 'SELECT T_ID,Operation_Name FROM dbo.Operation_Types ORDER BY T_ID;')
        Counts = @(Read-Rows @'
SELECT COUNT_BIG(*) AS BaseRows,
       (SELECT COUNT_BIG(*) FROM dbo.Network_Edit_Tracker_V) AS ViewRows,
       SUM(CONVERT(bigint,CASE WHEN u.User_ID IS NULL THEN 1 ELSE 0 END)) AS MissingUser,
       SUM(CONVERT(bigint,CASE WHEN s.id IS NULL THEN 1 ELSE 0 END)) AS MissingScreen,
       SUM(CONVERT(bigint,CASE WHEN a.T_ID IS NULL THEN 1 ELSE 0 END)) AS MissingOperation,
       SUM(CONVERT(bigint,CASE WHEN u.User_ID IS NULL OR s.id IS NULL OR a.T_ID IS NULL THEN 1 ELSE 0 END)) AS HiddenByInnerJoins
FROM dbo.Network_Edit_Tracker n LEFT JOIN dbo.Users u ON u.User_ID=n.User_ID
LEFT JOIN dbo.AgentBalance_Type s ON s.id=n.Screen_Type LEFT JOIN dbo.Operation_Types a ON a.T_ID=n.Operation_ID;
'@)
        InvalidClassifications = @(Read-Rows @'
SELECT n.Screen_Type,n.Operation_ID,COUNT_BIG(*) AS EventCount
FROM dbo.Network_Edit_Tracker n LEFT JOIN dbo.AgentBalance_Type s ON s.id=n.Screen_Type
LEFT JOIN dbo.Operation_Types a ON a.T_ID=n.Operation_ID
WHERE s.id IS NULL OR a.T_ID IS NULL GROUP BY n.Screen_Type,n.Operation_ID ORDER BY EventCount DESC;
'@)
        Synchronization = @(Read-Rows "SELECT TABLE_NAME,is_Push,is_Pull,PRIMARY_KEY_NAME,ORDER_COLUMN,SELECT_SCRIPT,CREATE_SCRIPT FROM dbo.SYNCRO_TABLE WHERE TABLE_NAME=N'Network_Edit_Tracker';")
    }
    $capture | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath $OutputPath -Encoding UTF8
    $capture.Database | Format-Table -AutoSize
    $capture.Counts | Format-Table -AutoSize
    $capture.InvalidClassifications | Format-Table -AutoSize
    'Read-only capture saved; no database changes.'
} finally { $connection.Dispose() }
