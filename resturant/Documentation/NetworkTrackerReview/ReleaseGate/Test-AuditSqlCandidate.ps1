function Test-AuditSqlCandidate {
    param([string]$ConnectionEnvironmentVariable, [object[]]$Operations, [switch]$RequireSandbox, [string]$OutputDirectory)
    $connectionString = [Environment]::GetEnvironmentVariable($ConnectionEnvironmentVariable)
    if (-not $connectionString) { throw 'SQL connection environment variable is empty.' }
    $builder = New-Object Data.SqlClient.SqlConnectionStringBuilder($connectionString)
    $builder['Pooling'] = $false
    $builder['Application Name'] = 'CPOS Audit Release Gate - Read Only'
    if ($RequireSandbox -and $builder['Initial Catalog'] -notmatch '^CPOS_AUDIT_GATE_[A-Za-z0-9_]+$') { throw 'Live/release checks require an isolated CPOS_AUDIT_GATE_* database. Production databases are refused.' }
    $dom = 'C:\Program Files (x86)\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.TransactSql.ScriptDom.dll'
    if (-not ('AuditSqlSafetyVisitor' -as [type])) {
        Add-Type -Path $dom
        Add-Type -Path (Join-Path $PSScriptRoot 'AuditSqlSafetyVisitor.cs') -ReferencedAssemblies $dom,System.Core
    }
    $problems = New-Object 'Collections.Generic.List[string]'
    $definitions = @{}
    $cn = New-Object Data.SqlClient.SqlConnection($builder.ConnectionString)
    try {
        $cn.Open()
        $cmd = $cn.CreateCommand()
        $cmd.CommandText = "SELECT p.name,m.definition FROM sys.procedures p JOIN sys.sql_modules m ON m.object_id=p.object_id WHERE p.name IN ('Network_Audit_Request_Begin','Network_Audit_Event_Write','Network_Audit_Request_Complete','Network_Audit_Request_Status');"
        $reader = $cmd.ExecuteReader()
        try { while ($reader.Read()) { $definitions[$reader.GetString(0)] = $reader.GetString(1) } } finally { $reader.Dispose(); $cmd.Dispose() }
        if ($definitions.Count -ne 4) { throw 'The four audit procedures are required and must not be encrypted.' }
        foreach ($name in $definitions.Keys) {
            [IO.File]::WriteAllText((Join-Path $OutputDirectory ($name + '.sql')), $definitions[$name], (New-Object Text.UTF8Encoding($true)))
            $scan = [AuditSqlSafetyVisitor]::Inspect($definitions[$name])
            foreach ($finding in $scan.Findings) { $problems.Add($name + ': ' + $finding) }
        }
        $begin = [AuditSqlSafetyVisitor]::Inspect($definitions['Network_Audit_Request_Begin'])
        $writer = [AuditSqlSafetyVisitor]::Inspect($definitions['Network_Audit_Event_Write'])
        foreach ($operation in $Operations) {
            $operation.SqlRegistered = $begin.DeclaredOperations.Contains($operation.Code) -and $writer.DeclaredOperations.Contains($operation.Code)
            if (-not $operation.SqlRegistered) { $problems.Add('SQL operation registration missing in Begin/Write: ' + $operation.Code) }
        }
        foreach ($code in @($begin.DeclaredOperations) + @($writer.DeclaredOperations) | Sort-Object -Unique) {
            if ($code -ne 'AuditTest.Changed' -and $code -notin $Operations.Code) { $problems.Add('SQL operation absent from VB contract: ' + $code) }
        }
        $cmd = $cn.CreateCommand()
        # Mixed raw values reproduce the optimizer hazard without changing any rows.
        $cmd.CommandText = @"
DECLARE @v TABLE (v nvarchar(100), expectedDate bit);
INSERT @v VALUES(N'1000',0),(N'0001-01-01T00:00:00',0),(N'1700-01-01T00:00:00',0),
 (N'1753-01-01T00:00:00',1),(N'2026-11-10T16:30:01',1),(N'9999-12-31T23:59:59.997',1),
 (N'2026-11-07T19:40:40.2930000',1),(N'2026-11-10T16:30:01.0000000',1),
 (N'1753-01-01T00:00:00.0000000',1),(N'9999-12-31T23:59:59.9970000',1),
 (N'9999-12-31T23:59:59.9999999',0),(N'6.528',0),(N'عميل عربي',0),(NULL,0);
SELECT COUNT(*) FROM @v WHERE
 (expectedDate=1 AND TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),v,126)) IS NULL) OR
 (expectedDate=0 AND (CASE WHEN TRY_CONVERT(datetime2(7),v,126)<=CONVERT(datetime2(7),'9999-12-31T23:59:59.9970000',126)
 THEN TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),v,126)) END) IS NOT NULL);
"@
        if ([int]$cmd.ExecuteScalar() -ne 0) { $problems.Add('SQL datetime boundary regression failed.') }
        $cmd.Dispose()
        $cmd = $cn.CreateCommand()
        $cmd.CommandText = 'SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1;'
        if ([long]$cmd.ExecuteScalar() -ne 0) { $problems.Add('Candidate contains incomplete audit requests.') }
        $cmd.Dispose()
    } finally { $cn.Dispose() }
    return [pscustomobject]@{ Database=$builder['Initial Catalog']; Failures=@($problems.ToArray()) }
}
