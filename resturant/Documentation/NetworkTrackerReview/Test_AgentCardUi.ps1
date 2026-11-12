param([Parameter(Mandatory=$true)][string]$AssemblyPath,
      [Parameter(Mandatory=$true)][string]$OutputImage)
$ErrorActionPreference='Stop'
Add-Type -AssemblyName System.Windows.Forms,System.Drawing
$assembly=[Reflection.Assembly]::LoadFrom((Resolve-Path -LiteralPath $AssemblyPath))
$type=$assembly.GetType('resturant.User_Valids',$true)
$form=[Activator]::CreateInstance($type)
try {
    # Designer-only inspection: never run Load, queries, or the application startup.
    $method=$type.GetMethod('STORES_Load',[Reflection.BindingFlags]'Instance,NonPublic')
    $handler=[Delegate]::CreateDelegate([EventHandler],$form,$method)
    $form.remove_Load($handler)
    $tabs=$form.Controls.Find('TabControl1',$true)[0]
    $tabs.Enabled=$true; $tabs.SelectedIndex=4
    $check=$form.Controls.Find('Open_Agent_Card_CB',$true)[0]
    if (-not $check -or $check.Parent.Name -ne 'TabPage5' -or -not $check.Checked) { throw 'Permission control/default missing.' }
    foreach ($other in $check.Parent.Controls) {
        if ($other -ne $check -and $check.Bounds.IntersectsWith($other.Bounds)) { throw ('Permission overlaps '+$other.Name) }
    }
    if (-not $check.Parent.ClientRectangle.Contains($check.Bounds)) { throw 'Permission outside tab bounds.' }
    # Render the tab on an unattached panel: a hidden Form otherwise produces an empty bitmap.
    $panel=New-Object Windows.Forms.Panel
    $panel.Size=$check.Parent.ClientSize
    $panel.BackColor=$check.Parent.BackColor
    foreach ($control in @($check.Parent.Controls)) { $panel.Controls.Add($control) }
    $panel.CreateControl()
    foreach ($control in $panel.Controls) { $control.CreateControl() }
    $bitmap=New-Object Drawing.Bitmap($panel.Width,$panel.Height)
    try { $panel.DrawToBitmap($bitmap,(New-Object Drawing.Rectangle(0,0,$panel.Width,$panel.Height))); $bitmap.Save($OutputImage,[Drawing.Imaging.ImageFormat]::Png) }
    finally { $bitmap.Dispose(); $panel.Dispose() }
    Write-Output 'PASS permission designer: default allowed, correct tab, no overlapping controls; no SQL connection opened.'
} finally { $form.Dispose() }
