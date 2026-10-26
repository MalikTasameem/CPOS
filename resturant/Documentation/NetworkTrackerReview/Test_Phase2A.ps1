param([switch]$LiveReadOnly, [switch]$RollbackWriteTests, [switch]$RenderScreens)
$ErrorActionPreference = 'Stop'
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$testDirectory = Join-Path $projectRoot '..\.tmp\AuditPhase2A\tests'
New-Item -ItemType Directory -Path $testDirectory -Force | Out-Null
$compiler = 'C:\Program Files (x86)\MSBuild\14.0\Bin\vbc.exe'
if (-not (Test-Path -LiteralPath $compiler)) { throw 'VB 14 compiler is required.' }
$testExe = Join-Path $testDirectory 'AuditReadModelTests.exe'
$sources = @(
    (Join-Path $projectRoot 'Classes\Audit\AuditCompatibilityReader.vb'),
    (Join-Path $projectRoot 'Forms\Audit\Network_Audit_Browser.vb'),
    (Join-Path $projectRoot 'Forms\Audit\Network_Audit_Browser.Designer.vb'),
    (Join-Path $projectRoot 'Forms\Audit\Network_Audit_Details.vb'),
    (Join-Path $projectRoot 'Forms\Audit\Network_Audit_Details.Designer.vb'),
    (Join-Path $PSScriptRoot 'AuditReadModelTests.vb')
)
& $compiler /nologo /target:exe /optionstrict+ /imports:System,System.Data,System.Drawing,System.Windows.Forms `
    /main:AuditReadModelTests /reference:System.dll,System.Core.dll,System.Data.dll,System.Xml.dll,System.Drawing.dll,System.Windows.Forms.dll `
    "/out:$testExe" $sources
if ($LASTEXITCODE -ne 0) { throw 'Audit reader/forms test compilation failed.' }
$testArgs = @('--output', $testDirectory)
if ($LiveReadOnly -or $RollbackWriteTests) {
    # Connection is read in memory by the harness; never logged or passed on the command line.
    $testArgs += @('--settings', (Join-Path $projectRoot 'bin\Debug\Settings Files\BackUpSettings.AppSettings'))
}
if ($RollbackWriteTests) { $testArgs += '--rollback-write-tests' }
if ($RenderScreens) { $testArgs += '--render' }
& $testExe $testArgs
if ($LASTEXITCODE -ne 0) { throw 'Audit Phase 2A tests failed.' }
