param(
    [string]$SettingsPath = (Join-Path $PSScriptRoot '..\..\bin\Debug\Settings Files\BackUpSettings.AppSettings'),
    [string]$OutputPath = (Join-Path $PSScriptRoot 'Phase2_Live_Metadata.json')
)
$ErrorActionPreference = 'Stop'
# Metadata and counts only. Never execute business procedures or print credentials.
$settings = @{}
foreach ($line in [IO.File]::ReadLines((Resolve-Path $SettingsPath))) {
    $separator = $line.IndexOf(':')
    if ($separator -gt 0) { $settings[$line.Substring(0, $separator)] = $line.Substring($separator + 1) }
}
if ($settings['IsAttachDB'] -eq 'True') { throw 'Database attachment is not allowed by this read-only inspection.' }
$builder = [System.Data.SqlClient.SqlConnectionStringBuilder]::new()
$builder['Data Source'] = $settings['S_SERVER'].Trim()
$builder['Initial Catalog'] = $settings['DataBase'].Trim()
$builder['Connect Timeout'] = 10
$builder['Application Name'] = 'CPOS Network Tracker Phase2 ReadOnly'
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
        Scope = 'Phase 2 design verification only; no writes, no draft/offline changes.'
        Database = @(Read-Rows "SELECT DB_NAME() AS DatabaseName,CONVERT(nvarchar(128),SERVERPROPERTY('ProductVersion')) AS ProductVersion;")
        SourceRows = @(Read-Rows 'SELECT Serv_ID,Sub_Code,START_ID,END_ID FROM dbo.SERVERS ORDER BY Serv_ID,Sub_Code;')
        Definitions = @(Read-Rows @'
SELECT o.name,o.type_desc,o.modify_date,m.definition
FROM sys.objects o JOIN sys.sql_modules m ON m.object_id=o.object_id
WHERE o.name IN (N'ST_Balance_Update',N'ST_QTY_Deferences_Details_Delete',N'AG_Balance_Void_Row') ORDER BY o.name;
'@)
        SettlementColumns = @(Read-Rows @'
SELECT c.column_id,c.name,TYPE_NAME(c.user_type_id) AS DataType,c.max_length,c.precision,c.scale,c.is_nullable,c.is_identity
FROM sys.columns c WHERE c.object_id=OBJECT_ID(N'dbo.ST_QTY_Deferences') ORDER BY c.column_id;
'@)
        ExistingProposedObjects = @(Read-Rows @'
SELECT SCHEMA_NAME(schema_id) AS SchemaName,name,type_desc FROM sys.objects
WHERE name IN (N'Network_Audit_Source',N'Network_Audit_Request',N'Network_Audit_Event',N'Network_Audit_Change',
N'Network_Audit_Request_Begin',N'Network_Audit_Request_Complete',N'Network_Audit_Event_Write',N'Network_Audit_Request_Status') ORDER BY name;
'@)
        TrackerSynchronization = @(Read-Rows "SELECT TABLE_NAME,is_Push,is_Pull,PRIMARY_KEY_NAME FROM dbo.SYNCRO_TABLE WHERE TABLE_NAME=N'Network_Edit_Tracker';")
    }
    try {
        $capture['MatchingSqlAgentSteps'] = @(Read-Rows @'
SELECT j.name AS JobName,j.enabled AS IsEnabled,s.step_id,s.step_name,s.subsystem,s.database_name
FROM msdb.dbo.sysjobs j JOIN msdb.dbo.sysjobsteps s ON s.job_id=j.job_id
WHERE s.command LIKE N'%Network_Edit_Tracker%' OR s.command LIKE N'%REDUCE_NETWORK_TRACKER%' OR s.command LIKE N'%DeleteAll_BuysAndSales%'
ORDER BY j.name,s.step_id;
'@)
        $capture['SqlAgentInspection'] = 'Read succeeded; only literal references in job steps were searched. Indirect calls and external schedulers are not covered.'
    } catch [System.Data.SqlClient.SqlException] {
        $capture['MatchingSqlAgentSteps'] = @()
        $capture['SqlAgentInspection'] = 'Unavailable; SQL error number ' + $_.Exception.Number.ToString() + '. No permission change attempted.'
    }
    $capture | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath $OutputPath -Encoding UTF8
    $capture.Database | Format-Table -AutoSize
    'Source configuration rows: ' + $capture.SourceRows.Count
    'Verified procedure definitions: ' + $capture.Definitions.Count
    'Existing objects using proposed names: ' + $capture.ExistingProposedObjects.Count
    'Matching SQL Agent steps: ' + $capture.MatchingSqlAgentSteps.Count
    $capture.SqlAgentInspection
    'Read-only phase 2 metadata saved. No database changes.'
} finally { $connection.Dispose() }
