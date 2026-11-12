param([Parameter(Mandatory=$true)][string]$OutputPath)
$ErrorActionPreference='Stop'
# Bounded correction of the canonical date validator, not a full release certificate.
$projectRoot=(Resolve-Path (Join-Path $PSScriptRoot '..\..\..')).Path
$path=Join-Path $projectRoot 'Database\Audit\046_Audit_Canonical_Datetime.sql'
$sql=[IO.File]::ReadAllText($path)
$guard="DB_NAME()<>N'CPOS_ACCOUNTING'"
if([regex]::Matches($sql,[regex]::Escape($guard)).Count -ne 1){throw 'Canonical database guard changed.'}
$template=[IO.File]::ReadAllText((Join-Path $PSScriptRoot 'Upgrade.Template.sql'))
$values=@{'__AUDIT_SHA__'=(Get-FileHash -LiteralPath $path -Algorithm SHA256).Hash;'__AUDIT_SQL__'=$sql.Replace($guard,'DB_NAME()<>@ExpectedDatabase').Replace("'","''")}
foreach($key in $values.Keys){
 if([regex]::Matches($template,[regex]::Escape($key)).Count -ne 1){throw 'Invalid template token.'}
 $template=$template.Replace($key,$values[$key])
}
if($template -match '__[A-Z0-9_]+__'){throw 'Unresolved token.'}
[IO.File]::WriteAllText($OutputPath,$template,(New-Object Text.UTF8Encoding($true)))
Write-Output ('QUERY='+$OutputPath)
Write-Output ('SHA256='+(Get-FileHash -LiteralPath $OutputPath -Algorithm SHA256).Hash)
