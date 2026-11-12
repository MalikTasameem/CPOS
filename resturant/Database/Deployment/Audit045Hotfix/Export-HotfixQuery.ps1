param([Parameter(Mandatory=$true)][string]$OutputPath)
$ErrorActionPreference='Stop'
# This is the explicitly requested 044->045 + Agent-card incremental query only.
# It neither builds an application/package nor changes/bypasses the full release gate.
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..\..')).Path
$audit=Join-Path $projectRoot 'Database\Audit\045_Audit_Safe_Document_Type.sql'
$security=Join-Path $projectRoot 'Database\Security\002_User_Validation_Agent_Card.sql'
$base=Join-Path $projectRoot 'Database\Audit\044_Audit_Safe_Datetime_Validation.sql'
$template=[IO.File]::ReadAllText((Join-Path $PSScriptRoot 'Upgrade.Template.sql'))
$values=@{
 '__AUDIT_SHA__'=(Get-FileHash -LiteralPath $audit -Algorithm SHA256).Hash
 '__SECURITY_SHA__'=(Get-FileHash -LiteralPath $security -Algorithm SHA256).Hash
 '__BASE044_SHA__'=(Get-FileHash -LiteralPath $base -Algorithm SHA256).Hash
}
foreach ($entry in @(@{Path=$audit;Token='__AUDIT_SQL__'},@{Path=$security;Token='__SECURITY_SQL__'})) {
    $sql=[IO.File]::ReadAllText($entry.Path)
    $guard="DB_NAME()<>N'CPOS_ACCOUNTING'"
    if ([regex]::Matches($sql,[regex]::Escape($guard)).Count -ne 1) { throw 'Canonical database guard changed.' }
    $values[$entry.Token]=$sql.Replace($guard,'DB_NAME()<>@ExpectedDatabase').Replace("'","''")
}
foreach ($key in $values.Keys) {
    if ([regex]::Matches($template,[regex]::Escape($key)).Count -ne 1) { throw "Invalid template token $key" }
    $template=$template.Replace($key,$values[$key])
}
if ($template -match '__[A-Z0-9_]+__') { throw 'Unresolved template token.' }
$parent=Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $parent)) { [void](New-Item -ItemType Directory -Path $parent) }
[IO.File]::WriteAllText($OutputPath,$template,(New-Object Text.UTF8Encoding($true)))
Write-Output ('QUERY='+$OutputPath)
Write-Output ('SHA256='+(Get-FileHash -LiteralPath $OutputPath -Algorithm SHA256).Hash)
