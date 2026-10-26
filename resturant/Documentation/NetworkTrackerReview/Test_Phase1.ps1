param([switch]$LiveReadOnly)
$ErrorActionPreference = 'Stop'
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$testDirectory = Join-Path $projectRoot '..\.tmp\NetworkTrackerTests'
New-Item -ItemType Directory -Path $testDirectory -Force | Out-Null
$compiler = 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe'
if (-not (Test-Path -LiteralPath $compiler)) { $compiler = 'C:\Windows\Microsoft.NET\Framework\v4.0.30319\vbc.exe' }
$testExe = Join-Path $testDirectory 'NetworkTrackerSearchTests.exe'
& $compiler /nologo /target:exe /optionstrict+ /imports:System /main:NetworkTrackerSearchTests `
    /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll `
    "/out:$testExe" (Join-Path $projectRoot 'Classes\NetworkTrackerSearch.vb') (Join-Path $PSScriptRoot 'NetworkTrackerSearchTests.vb')
if ($LASTEXITCODE -ne 0) { throw 'Search helper/test compilation failed.' }
if ($LiveReadOnly) {
    & $testExe (Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings') (Join-Path $projectRoot 'Database\Audit\001_Keep_Unclassified_Network_Events_Visible.sql')
} else { & $testExe }
if ($LASTEXITCODE -ne 0) { throw 'Search tests failed.' }
