Set-StrictMode -Version 2

function Get-AuditReleaseFingerprint([string]$ProjectRoot) {
    # Include callers as well as contracts: a change in a form invalidates the certificate.
    $files = @()
    foreach ($folder in @('Classes','Models','Forms','Tools','Database\Audit','Database\Security')) {
        $path = Join-Path $ProjectRoot $folder
        if (Test-Path -LiteralPath $path) {
            $files += Get-ChildItem -LiteralPath $path -File -Recurse | Where-Object { $_.Extension -in '.vb','.sql','.resx' }
        }
    }
    $files += Get-ChildItem -LiteralPath $ProjectRoot -File | Where-Object { $_.Extension -in '.vbproj','.config' }
    $review = Join-Path $ProjectRoot 'Documentation\NetworkTrackerReview'
    $files += Get-ChildItem -LiteralPath $review -File | Where-Object { $_.Name -like 'Test_*.ps1' -or $_.Name -like '*Tests.vb' }
    $files += Get-ChildItem -LiteralPath (Join-Path $review 'ReleaseGate') -File -Recurse | Where-Object { $_.Extension -in '.ps1','.vb','.cs','.json','.sql' }
    $deployment = Join-Path $ProjectRoot 'Database\Deployment\NetworkAuditUpgrade'
    foreach ($name in @('Build-NetworkAuditUpgrade.ps1','CPOS_Network_Audit_Upgrade.Template.ps1','Run_CPOS_Network_Audit_Upgrade.cmd')) {
        $files += Get-Item -LiteralPath (Join-Path $deployment $name)
    }
    $records = @($files | Sort-Object FullName -Unique | ForEach-Object {
        $_.FullName.Substring($ProjectRoot.TrimEnd('\').Length + 1).Replace('\','/') + ':' + (Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash
    })
    $sha = [Security.Cryptography.SHA256]::Create()
    try { return ([BitConverter]::ToString($sha.ComputeHash([Text.Encoding]::UTF8.GetBytes(($records -join "`n"))))).Replace('-','') }
    finally { $sha.Dispose() }
}

function Invoke-AuditGateChild([string]$Command, [string]$LogPath, [int]$TimeoutSeconds = 180) {
    # EncodedCommand avoids broken quoting in paths containing spaces and nested PowerShell.
    $encoded = [Convert]::ToBase64String([Text.Encoding]::Unicode.GetBytes($Command))
    $start = New-Object Diagnostics.ProcessStartInfo
    $start.FileName = (Join-Path $env:SystemRoot 'System32\WindowsPowerShell\v1.0\powershell.exe')
    $start.Arguments = '-NoProfile -NonInteractive -ExecutionPolicy Bypass -EncodedCommand ' + $encoded
    $start.UseShellExecute = $false
    $start.CreateNoWindow = $true
    $start.RedirectStandardOutput = $true
    $start.RedirectStandardError = $true
    $process = New-Object Diagnostics.Process
    $process.StartInfo = $start
    try {
        [void]$process.Start()
        $stdout = $process.StandardOutput.ReadToEndAsync()
        $stderr = $process.StandardError.ReadToEndAsync()
        $finished = $process.WaitForExit($TimeoutSeconds * 1000)
        if (-not $finished -and -not $process.HasExited) {
            # A legacy runner may have a compiler/test EXE child holding SQL transactions.
            # Kill only this owned test process tree, never CPOS/SSMS or a database session.
            $stopInfo = New-Object Diagnostics.ProcessStartInfo
            $stopInfo.FileName = Join-Path $env:SystemRoot 'System32\taskkill.exe'
            $stopInfo.Arguments = '/PID ' + $process.Id + ' /T /F'
            $stopInfo.UseShellExecute = $false
            $stopInfo.CreateNoWindow = $true
            $stopInfo.RedirectStandardOutput = $true
            $stopInfo.RedirectStandardError = $true
            $stopper = [Diagnostics.Process]::Start($stopInfo)
            try { $stopper.WaitForExit(); if ($stopper.ExitCode -ne 0 -and -not $process.HasExited) { throw 'Could not stop the timed-out test process tree.' } }
            finally { $stopper.Dispose() }
            $process.WaitForExit()
        }
        $output = $stdout.Result + "`r`n" + $stderr.Result
        [IO.File]::WriteAllText($LogPath, $output, (New-Object Text.UTF8Encoding($true)))
        if (-not $finished) { return [pscustomobject]@{ Passed=$false; ExitCode=-1; Reason='Timed out' } }
        return [pscustomobject]@{ Passed=($process.ExitCode -eq 0); ExitCode=$process.ExitCode; Reason='' }
    } finally { $process.Dispose() }
}

function Copy-AuditTestWorkspace([string]$ProjectRoot, [string]$Destination) {
    # Legacy runners write fixed .tmp paths; sandbox their filesystem as well as their SQL.
    [void](New-Item -ItemType Directory -Path $Destination -Force)
    foreach ($folder in @('Classes','Models','Forms','Tools','Database\Audit','Database\Security')) {
        $source = Join-Path $ProjectRoot $folder
        if (-not (Test-Path -LiteralPath $source)) { continue }
        foreach ($file in Get-ChildItem -LiteralPath $source -File -Recurse) {
            if ($file.Extension -notin '.vb','.sql','.resx') { continue }
            $target = Join-Path $Destination $file.FullName.Substring($ProjectRoot.Length+1)
            [void](New-Item -ItemType Directory -Path (Split-Path -Parent $target) -Force)
            Copy-Item -LiteralPath $file.FullName -Destination $target
        }
    }
    $review = Join-Path $ProjectRoot 'Documentation\NetworkTrackerReview'
    $targetReview = Join-Path $Destination 'Documentation\NetworkTrackerReview'
    [void](New-Item -ItemType Directory -Path $targetReview -Force)
    foreach ($file in Get-ChildItem -LiteralPath $review -File) {
        if ($file.Extension -in '.ps1','.vb') { Copy-Item -LiteralPath $file.FullName -Destination $targetReview }
    }
    Copy-Item -LiteralPath (Join-Path $review 'ReleaseGate') -Destination (Join-Path $targetReview 'ReleaseGate') -Recurse
    foreach ($file in Get-ChildItem -LiteralPath $ProjectRoot -File | Where-Object { $_.Extension -in '.vbproj','.config' }) {
        Copy-Item -LiteralPath $file.FullName -Destination $Destination
    }
}

function Assert-AuditReleaseEvidence([string]$ProjectRoot, [string]$ReportPath) {
    if (-not $ReportPath -or -not (Test-Path -LiteralPath $ReportPath -PathType Leaf)) {
        throw 'Release blocked: run ReleaseGate\Invoke-AuditReleaseGate.ps1 -Mode Release and supply -ReleaseReport. Offline checks alone do not certify a release.'
    }
    $report = Get-Content -LiteralPath $ReportPath -Raw -Encoding UTF8 | ConvertFrom-Json
    if ($report.Schema -ne 1 -or $report.Mode -ne 'Release' -or -not $report.Passed -or @($report.Failures).Count -ne 0) {
        throw 'Release blocked: the audit gate did not pass all checks.'
    }
    if ($report.SourceFingerprint -ne (Get-AuditReleaseFingerprint $ProjectRoot)) { throw 'Release blocked: source/SQL/tests changed since the gate run.' }
    $age = [DateTime]::UtcNow - [DateTime]::Parse($report.FinishedUtc).ToUniversalTime()
    if ($age.TotalHours -gt 24 -or $age.TotalSeconds -lt -60) { throw 'Release blocked: gate evidence is expired or has a future timestamp.' }
    if (-not $report.SqlChecked -or $report.Database -notmatch '^CPOS_AUDIT_GATE_[A-Za-z0-9_]+$') { throw 'Release blocked: isolated SQL verification is missing.' }
    if (@($report.Operations).Count -lt 1 -or @($report.Operations | Where-Object { -not $_.SqlRegistered -or -not $_.LivePassed }).Count -gt 0) {
        throw 'Release blocked: at least one registered operation has no successful SQL integration scenario.'
    }
    if (@($report.Suites).Count -lt 40 -or @($report.Suites | Where-Object { -not $_.Passed }).Count -gt 0) { throw 'Release blocked: offline regression suites are incomplete.' }
    foreach ($suite in $report.Suites) {
        if (-not (Test-Path -LiteralPath $suite.Log -PathType Leaf) -or (Get-FileHash -LiteralPath $suite.Log -Algorithm SHA256).Hash -ne $suite.LogHash) {
            throw 'Release blocked: test log is missing or changed.'
        }
    }
}
