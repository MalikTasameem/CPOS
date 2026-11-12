param(
    [string]$AuditDirectory = (Join-Path $PSScriptRoot '..\..\Audit'),
    [string]$OutputDirectory = $PSScriptRoot,
    [string]$ReleaseReport
)

$ErrorActionPreference = 'Stop'
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..')).Path
. (Join-Path $projectRoot 'Documentation\NetworkTrackerReview\ReleaseGate\AuditReleaseGate.Common.ps1')
Assert-AuditReleaseEvidence -ProjectRoot $projectRoot -ReportPath $ReleaseReport
# A certificate is valid only for the checked-in migration directory, never an alternate payload.
if ((Resolve-Path -LiteralPath $AuditDirectory).Path -ne (Resolve-Path -LiteralPath (Join-Path $projectRoot 'Database\Audit')).Path) {
    throw 'Release blocked: AuditDirectory differs from the verified input.'
}
$migrationNames = @(
    '002_Create_Audit_V2_ReadModel.sql',
    '003_Create_Audit_V2_Writer.sql',
    '005_Settlement_Line_Prerequisites.sql',
    '006_Settlement_Header_And_Stock.sql',
    '007_Audit_Pilot_Gate.sql',
    '008_Settlement_Line_Safety.sql',
    '009_Sales_Edit_Opening.sql',
    '010_Document_Edit_Opening.sql',
    '011_Sales_Line_Editing.sql',
    '012_Sales_Line_Quantity.sql',
    '013_Fix_DependingUpdatedBill_Target.sql',
    '014_Sales_Line_Deletion.sql',
    '015_Sales_Component_Lifecycle.sql',
    '016_Sales_Detail_Components.sql',
    '017_Sales_Line_Addition.sql',
    '018_Document_Cancellation.sql',
    '019_Unit_Catalogue.sql',
    '020_Group_Catalogue.sql',
    '021_Item_Unit_Catalogue.sql',
    '022_Pricing_Rules.sql',
    '023_Item_Master.sql',
    '024_Tables.sql',
    '025_Finance_Transfers.sql',
    '026_Stock_Document_Lines.sql',
    '027_Employee_Values.sql',
    '028_Return_Lines.sql',
    '029_Expense_Lines.sql',
    '030_Account_Entries.sql',
    '031_Purchase_Addition.sql',
    '032_Document_Direct_Fields.sql',
    '033_Quote_Delete_Isolation.sql',
    '034_Quote_Lines.sql',
    '035_People_Cards.sql',
    '036_Inventory_Operations.sql',
    '037_Document_Workflows.sql',
    '038_Item_Lifecycle.sql',
    '039_Employee_Salary_Batch.sql',
    '040_Table_Split.sql',
    '041_Receipts.sql',
    '042_Receipt_Admin_Permissions.sql',
    '043_Receipt_Safe_Datetime_Validation.sql',
    '044_Audit_Safe_Datetime_Validation.sql',
    '045_Audit_Safe_Document_Type.sql',
    '046_Audit_Canonical_Datetime.sql'
)

function Get-Sha256Hex([byte[]]$Bytes) {
    $sha = [Security.Cryptography.SHA256]::Create()
    try { return (($sha.ComputeHash($Bytes) | ForEach-Object { $_.ToString('x2') }) -join '').ToUpperInvariant() }
    finally { $sha.Dispose() }
}

$utf8Strict = New-Object System.Text.UTF8Encoding($false, $true)
$entries = foreach ($name in $migrationNames) {
    $path = Join-Path $AuditDirectory $name
    if (-not (Test-Path -LiteralPath $path -PathType Leaf)) { throw "Missing migration: $name" }
    $bytes = [IO.File]::ReadAllBytes((Resolve-Path -LiteralPath $path))
    [void]$utf8Strict.GetString($bytes)
    [pscustomobject]@{
        Id = $name.Substring(0,3)
        Name = $name
        Sha256 = Get-Sha256Hex $bytes
        SqlBase64 = [Convert]::ToBase64String($bytes)
    }
}

$json = $entries | ConvertTo-Json -Depth 4 -Compress
$jsonBytes = $utf8Strict.GetBytes($json)
$compressedStream = New-Object IO.MemoryStream
try {
    $gzip = New-Object IO.Compression.GZipStream($compressedStream,[IO.Compression.CompressionMode]::Compress,$true)
    try { $gzip.Write($jsonBytes,0,$jsonBytes.Length) } finally { $gzip.Dispose() }
    $compressed = $compressedStream.ToArray()
}
finally { $compressedStream.Dispose() }

$payloadHash = Get-Sha256Hex $compressed
$payloadBase64 = [Convert]::ToBase64String($compressed)
$templatePath = Join-Path $PSScriptRoot 'CPOS_Network_Audit_Upgrade.Template.ps1'
$template = [IO.File]::ReadAllText($templatePath,$utf8Strict)
if (($template.Split(@('__PAYLOAD_SHA256__'),[StringSplitOptions]::None).Count-1) -ne 1 -or
    ($template.Split(@('__PAYLOAD_BASE64__'),[StringSplitOptions]::None).Count-1) -ne 1) {
    throw 'Template payload placeholders are invalid.'
}
$result = $template.Replace('__PAYLOAD_SHA256__',$payloadHash).Replace('__PAYLOAD_BASE64__',$payloadBase64)

if (-not (Test-Path -LiteralPath $OutputDirectory)) { [void](New-Item -ItemType Directory -Path $OutputDirectory) }
$outputPath = Join-Path $OutputDirectory 'CPOS_Network_Audit_Upgrade.ps1'
$utf8Bom = New-Object System.Text.UTF8Encoding($true)
[IO.File]::WriteAllText($outputPath,$result,$utf8Bom)
$fileHash = (Get-FileHash -LiteralPath $outputPath -Algorithm SHA256).Hash
[IO.File]::WriteAllText((Join-Path $OutputDirectory 'CPOS_Network_Audit_Upgrade.sha256'),("$fileHash  CPOS_Network_Audit_Upgrade.ps1`r`n"),(New-Object Text.UTF8Encoding($false)))
Write-Host "Generated $outputPath"
Write-Host "SHA256 $fileHash"
