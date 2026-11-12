param([Parameter(Mandatory=$true)][string]$AssemblyPath)
$ErrorActionPreference='Stop'
Add-Type -AssemblyName System.Windows.Forms
$assembly=[Reflection.Assembly]::LoadFrom((Resolve-Path -LiteralPath $AssemblyPath))
$type=$assembly.GetType('resturant.IM_ADD_New',$true)
$form=[Activator]::CreateInstance($type)
$flags=[Reflection.BindingFlags]'Instance,NonPublic'
try {
 # Do not show the form or run Load: these cases must return before any SQL/settings call.
 $id=$type.GetField('New_IM_ID',$flags)
 $save=$form.Controls.Find('ConfermButton',$true)[0]
 foreach($mode in @('zero-enabled','zero-disabled','valid-disabled')){
  $id.SetValue($form,[int]$(if($mode -eq 'valid-disabled'){20}else{0}))
  $save.Enabled=$mode -eq 'zero-enabled'
  $key=New-Object Windows.Forms.KeyEventArgs([Windows.Forms.Keys]::F12)
  $type.GetMethod('IM_ADD_New_KeyDown',$flags).Invoke($form,@($form,$key.PSObject.BaseObject))
  if(-not $key.Handled -or -not $key.SuppressKeyPress){throw ('F12 was not suppressed: '+$mode)}
  $type.GetMethod('ConfermButton_Click',$flags).Invoke($form,@($save,[EventArgs]::Empty))
  $type.GetMethod('Confirm_IM',$flags).Invoke($form,$null)
  Write-Output ('PASS F12/click/internal save refuse unready state: '+$mode)
 }
}finally{$form.Dispose()}
Write-Output 'PASS item form guards; no Load event, database connection or message dialog opened.'
