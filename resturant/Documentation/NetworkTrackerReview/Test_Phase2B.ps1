param([switch]$InstallProcedures, [switch]$LiveWriteTests)
$ErrorActionPreference = 'Stop'
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$output = Join-Path $projectRoot '..\.tmp\AuditPhase2B'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$compiler = 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe'
$sources = @(
 'Models\Audit\AuditEventContext.vb','Models\Audit\AuditEventRequest.vb','Models\Audit\AuditFieldChange.vb',
 'Classes\Audit\AuditContract.vb','Classes\Audit\AuditCanonical.vb','Classes\Audit\AuditMessageFormatter.vb',
 'Classes\Audit\AuditRequestCoordinator.vb','Classes\Audit\AuditWriter.vb'
) | ForEach-Object { Join-Path $projectRoot $_ }
$sources += @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Classes\Audit') -Filter 'AuditContract.*.vb' | ForEach-Object { $_.FullName })
$sources += Join-Path $projectRoot 'Classes\Audit\AuditDisplayNameResolver.vb'
$sources += Join-Path $PSScriptRoot 'AuditWriterTests.vb'
$exe = Join-Path $output 'AuditWriterTests.exe'
& $compiler /nologo /target:exe /optionstrict+ /imports:System /main:AuditWriterTests `
    /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Xml.Linq.dll "/out:$exe" $sources
if ($LASTEXITCODE -ne 0) { throw 'Writer/test compilation failed.' }
if ($InstallProcedures) {
    $settings = @{}
    foreach ($line in [IO.File]::ReadLines((Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings'))) {
        $p = $line.IndexOf(':')
        if ($p -gt 0) { $settings[$line.Substring(0,$p)] = $line.Substring($p+1) }
    }
    $builder = [System.Data.SqlClient.SqlConnectionStringBuilder]::new($settings['SqlConStr'])
    $builder['Initial Catalog'] = 'CPOS_ACCOUNTING'
    $builder['Application Name'] = 'CPOS Audit Phase2B Install'
    $connection = [System.Data.SqlClient.SqlConnection]::new($builder.ConnectionString)
    try {
        $connection.Open()
        $command = $connection.CreateCommand()
        $command.CommandTimeout = 60
        $command.CommandText = [IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Audit\003_Create_Audit_V2_Writer.sql'))
        [void]$command.ExecuteNonQuery()
        Write-Output 'Installed four audit procedures. No domains enabled.'
    } finally { $connection.Dispose() }
}
$arguments = @('--output', $output)
if ($LiveWriteTests) { $arguments += @('--settings', (Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings')) }
& $exe $arguments
if ($LASTEXITCODE -ne 0) { throw 'Writer tests failed; inspect the recovery manifest if cleanup failed.' }
