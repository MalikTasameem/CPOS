param(
    [string]$ProjectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path,
    [string]$OutputDirectory = $PSScriptRoot
)
$ErrorActionPreference = 'Stop'
New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null
$root = [IO.Path]::GetFullPath($ProjectRoot)
[xml]$project = [IO.File]::ReadAllText((Join-Path $root 'resturant.vbproj'))
$compiled = @{}
foreach ($node in $project.SelectNodes('//*[local-name()="Compile"]')) { $compiled[$node.Include.ToLowerInvariant()] = $true }
$rows = [Collections.Generic.List[object]]::new()
$files = & rg --files $root -g '*.vb' -g '!**/obj/**' -g '!**/bin/**'
foreach ($file in $files) {
    $lines = [IO.File]::ReadAllText($file) -split "`n"
    $method = ''
    for ($i=0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i].TrimEnd("`r")
        if ($line -match '^\s*(?:(?:Public|Private|Protected|Friend|Shared|Async|Overrides|Overloads|Partial)\s+)*(?:Sub|Function)\s+(\w+)') { $method = $Matches[1] }
        if ($line -notmatch '(?i)Network_Edit_Tracker_insert') { continue }
        $relative = $file.Substring($root.Length+1)
        $kind = 'Call'
        if ($line.TrimStart().StartsWith("'")) { $kind = 'Comment' }
        elseif ($line -match '(?i)(Sub|Function)\s+Network_Edit_Tracker_insert') { $kind = 'Definition' }
        elseif ($line -match '(?i)CommandText\s*=') { $kind = 'CommandText' }
        elseif ($line -match '(?i)Execute\w+StoredProcedure') { $kind = 'TransactionalCall' }
        $statement = $line.Trim()
        $j=$i
        while ($j+1 -lt $lines.Count -and ($lines[$j].TrimEnd() -match '(?:_|,|\+|&)\s*$')) {
            $j++
            $statement += ' ' + $lines[$j].Trim()
        }
        if ($kind -eq 'TransactionalCall') {
            while ($j+1 -lt $lines.Count -and $lines[$j] -notmatch '(?i)End Sub\)') {
                $j++
                $statement += ' ' + $lines[$j].Trim()
            }
        }
        $rows.Add([pscustomobject]@{File=$relative;Line=$i+1;Method=$method;Kind=$kind;Compiled=$compiled.ContainsKey($relative.ToLowerInvariant());Statement=$statement})
    }
}
$rows = @($rows | Sort-Object File,Line)
$rows | ConvertTo-Json -Depth 4 | Set-Content -LiteralPath (Join-Path $OutputDirectory 'CallSites.json') -Encoding UTF8
$md = [Collections.Generic.List[string]]::new()
$md.Add('# حصر مواضع شبكة المراقبة')
$md.Add('')
$md.Add('حصر نصي آلي من كود المشروع؛ ليس تحليل وصول تنفيذيًا ولا يثبت أن كل موضع يُستدعى وقت التشغيل. عدد الاستدعاءات لا يساوي عدد العمليات المغطاة. Comment يعني كودًا معلقًا لا ينفذ. Compiled=False يعني ملفًا غير مدرج في مشروع VB الحالي. مسارات الجدول والإجراءات SQL تراجع في التقرير الرئيسي.')
$md.Add('')
$md.Add('تاريخ توليد الحصر: ' + (Get-Date -Format 'yyyy-MM-dd') + '. المسارات نسبةً إلى مجلد resturant. الاستدعاء داخل الشرط محسوب موضعًا واحدًا، ولا تحسب الاستدعاءات غير المباشرة مرة ثانية.')
$md.Add('')
$md.Add('| الملف | السطر | الإجراء الحاوي | نوع الموضع | ضمن البناء | النص |')
$md.Add('|---|---:|---|---|---|---|')
foreach($r in $rows) {
    $statement = $r.Statement.Replace('|','\|').Replace('`','\`')
    $md.Add('| ' + $r.File + ' | ' + $r.Line + ' | ' + $r.Method + ' | ' + $r.Kind + ' | ' + $r.Compiled + ' | ' + $statement + ' |')
}
$md | Set-Content -LiteralPath (Join-Path $OutputDirectory 'CallSites.md') -Encoding UTF8
$rows | Group-Object Kind | Select-Object Name,Count | Format-Table -AutoSize
$rows | Where-Object {$_.Kind -in @('Call','TransactionalCall')} | Group-Object File | Select-Object Name,Count | Format-Table -AutoSize
