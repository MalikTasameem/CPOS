$ErrorActionPreference = 'Stop'
$repo = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
$source = [IO.File]::ReadAllText((Join-Path $repo 'resturant\Forms\Stores\Invoice_IM_card.vb'))
$methods = @('Load_SelectedItemData', 'ApplySelectedUnitData', 'RefreshCostFields',
    'PriceTextBox_TextChanged', 'IM_Fetch_QTY', 'IM_Unit_cm_SelectedValueChanged',
    'ST_cm_SelectedValueChanged')
$methodSource = foreach ($name in $methods) {
    $match = [regex]::Match($source, '(?ms)^    Private Sub ' + $name + '\(.*?^    End Sub')
    if (-not $match.Success) { throw "Production method not found: $name" }
    $match.Value
}
$harness = @'
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports SqlClient = System.Data.SqlClient

Public Class MY_Settings
    Public Shared SqlConStr As String
End Class

Public Class ItemCostHarness
    Implements IDisposable
    Public IM_ID As Integer
    Public U_ID As Integer
    Public U_Cargo As Double = 1
    Public IM_QTY As Double
    Public ALL_QTY As Double
    Public U_Dt As New DataTable
    Public Get_Unit As Boolean
    Public S_Stores As Boolean
    Public CalcCalls As Integer
    Public LastCalculatedCost As String
    Public WithEvents IM_Unit_cm As New ComboBox
    Public WithEvents ST_cm As New ComboBox
    Public WithEvents PriceTextBox As New TextBox
    Public Prev_Sale_Unit_txt As New TextBox
    Public Current_QTY As New TextBox
    Public ALL_QTY_txt As New TextBox
    Public CostByOne As New TextBox
    Public NewSalePrice_txt As New TextBox
    Public NewSaleByOne As New TextBox
    Public Min_SP_By_One_txt As New TextBox
    Public Min_SP_2_By_One_txt As New TextBox
    Public Min_SP_By_One_Lb As New Label
    Public Min_SP_2_By_One_Lb As New Label
    Public One_Panel As New Panel
    Public Two_Panel As New Panel

    Public Sub LoadItem(itemId As Integer)
        IM_ID = itemId
        Load_SelectedItemData()
    End Sub

    Public Sub ReloadStore()
        ST_cm_SelectedValueChanged(ST_cm, EventArgs.Empty)
    End Sub

    Public Sub ReloadUnit()
        IM_Fetch_QTY()
    End Sub

    Private Sub CalcAvgCost()
        CalcCalls += 1
        LastCalculatedCost = PriceTextBox.Text
    End Sub

    Private Sub Check_Point_in_FloatNum(sender As Object, e As EventArgs)
        If sender.Text = "." Then sender.Text = "0."
    End Sub

    Private Sub Load_IM_ST_QTY(itemId As Integer, store As ComboBox, ByRef qty As Double)
        Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
            cn.Open()
            Using cmd As New SqlClient.SqlCommand("SELECT ISNULL(SUM(QTY),0) FROM ST_Balance_V WHERE IM_ID=@IM_ID AND ST_ID=@ST_ID", cn)
                cmd.Parameters.AddWithValue("@IM_ID", itemId)
                cmd.Parameters.AddWithValue("@ST_ID", store.SelectedValue)
                qty = Convert.ToDouble(cmd.ExecuteScalar())
            End Using
        End Using
    End Sub

    Private Sub MsgBox(message As String)
        Throw New InvalidOperationException(message)
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        For Each field As System.Reflection.FieldInfo In Me.GetType().GetFields()
            Dim disposable As IDisposable = TryCast(field.GetValue(Me), IDisposable)
            If disposable IsNot Nothing Then disposable.Dispose()
        Next
    End Sub
__METHODS__
End Class
'@
$harness = $harness.Replace('__METHODS__', ($methodSource -join "`r`n"))
Add-Type -AssemblyName System.Windows.Forms
$provider = New-Object Microsoft.VisualBasic.VBCodeProvider
$parameters = New-Object System.CodeDom.Compiler.CompilerParameters
$parameters.GenerateInMemory = $true
$parameters.CompilerOptions = '/optionexplicit+ /optionstrict-'
foreach ($assembly in @('System.dll', 'System.Data.dll', 'System.Drawing.dll', 'System.Windows.Forms.dll', 'System.Xml.dll')) {
    [void]$parameters.ReferencedAssemblies.Add($assembly)
}
$compiled = $provider.CompileAssemblyFromSource($parameters, $harness)
if ($compiled.Errors.HasErrors) { throw ($compiled.Errors | Out-String) }

# Reuse application settings in memory; all commands in this harness are SELECT only.
$settings = @{}
foreach ($line in [IO.File]::ReadLines((Join-Path $repo 'resturant\bin\Debug\Settings Files\BackUpSettings.AppSettings'))) {
    $sep = $line.IndexOf(':')
    if ($sep -gt 0) { $settings[$line.Substring(0, $sep)] = $line.Substring($sep + 1) }
}
if ($settings['IsAttachDB'] -eq 'True') { throw 'This test never attaches a database.' }
$builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder
$builder['Data Source'] = $settings['S_SERVER'].Trim()
$builder['Initial Catalog'] = $settings['DataBase'].Trim()
$builder['Connect Timeout'] = 10
$builder['Application Name'] = 'CPOS Item Cost ReadOnly Test'
if ($settings['DB_Authentication'] -eq '0') { $builder['Integrated Security'] = $true }
else { $builder['User ID'] = $settings['DB_UName']; $builder['Password'] = $settings['DB_Pass'] }
$compiled.CompiledAssembly.GetType('MY_Settings').GetField('SqlConStr').SetValue($null, $builder.ConnectionString)
$connection = New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)
$form = $null
$checks = 0
function Assert-Equal($actual, $expected, [string]$name) {
    if ($actual -ne $expected) { throw "FAIL $name : expected $expected, actual $actual" }
    $script:checks++
    Write-Output "PASS $name"
}
try {
    $connection.Open()
    $cmd = $connection.CreateCommand()
    $cmd.CommandText = 'SELECT TOP (1) ST_ID FROM STORES ORDER BY ST_ID; SELECT TOP (1) IM_ID,U_IM_ID,Cost,Price,U_Cargo FROM IM_Menu_Units_V v WHERE Cost>0 AND NOT EXISTS (SELECT 1 FROM Pch_Details p WHERE p.IM_ID=v.IM_ID AND p.U_ID=v.U_ID AND p.isDepended=1) ORDER BY IM_ID; SELECT IM_ID,U_IM_ID,Cost,Price,U_Cargo FROM IM_Menu_Units_V WHERE IM_ID IN (SELECT IM_ID FROM IM_Menu_Units_V GROUP BY IM_ID HAVING MIN(U_Cargo)=1 AND MAX(U_Cargo)>1) ORDER BY IM_ID,U_Cargo'
    $adapter = New-Object System.Data.SqlClient.SqlDataAdapter($cmd)
    $data = New-Object System.Data.DataSet
    [void]$adapter.Fill($data)
    $adapter.Dispose()
    $cmd.Dispose()
    if ($data.Tables[1].Rows.Count -eq 0) { throw 'Need an item with cost but no approved purchase for this unit.' }
    $form = [Activator]::CreateInstance($compiled.CompiledAssembly.GetType('ItemCostHarness'))
    $form.ST_cm.BindingContext = New-Object System.Windows.Forms.BindingContext
    $form.IM_Unit_cm.BindingContext = New-Object System.Windows.Forms.BindingContext
    $form.ST_cm.DisplayMember = 'ST_ID'
    $form.ST_cm.ValueMember = 'ST_ID'
    $form.ST_cm.DataSource = $data.Tables[0]
    $row = $data.Tables[1].Rows[0]
    $form.LoadItem([int]$row.IM_ID)
    Assert-Equal ([double]$form.PriceTextBox.Text) ([double]$row.Cost * $form.U_Cargo) 'Initial selection loads card cost'
    $form.IM_Unit_cm.SelectedValue = $row.U_IM_ID
    Assert-Equal ([double]$form.PriceTextBox.Text) ([double]$row.Cost * [double]$row.U_Cargo) 'Unit without purchase history still loads cost'
    Assert-Equal ([double]$form.Prev_Sale_Unit_txt.Text) ([double]$row.Price) 'Previous sale price remains independent'
    $form.PriceTextBox.Text = '999'
    $form.ReloadStore()
    Assert-Equal ([double]$form.PriceTextBox.Text) ([double]$row.Cost * [double]$row.U_Cargo) 'Store-change handler reloads card cost'
    Assert-Equal $form.LastCalculatedCost $form.PriceTextBox.Text 'Calculations see new cost, not previous item cost'
    $before = $form.CalcCalls
    $form.ReloadUnit()
    Assert-Equal ($form.CalcCalls - $before) 1 'Unchanged cost still refreshes dependent fields once'

    if ($data.Tables[2].Rows.Count -gt 0) {
        $itemId = [int]$data.Tables[2].Rows[0].IM_ID
        $form.LoadItem($itemId)
        foreach ($unit in $data.Tables[2].Rows | Where-Object { [int]$_.IM_ID -eq $itemId }) {
            $form.IM_Unit_cm.SelectedValue = $unit.U_IM_ID
            Assert-Equal ([double]$form.PriceTextBox.Text) ([double]$unit.Cost * [double]$unit.U_Cargo) "Cost conversion for cargo $($unit.U_Cargo)"
            if ([double]$unit.U_Cargo -gt 1) {
                Assert-Equal ([double]$form.CostByOne.Text) ([double]$unit.Cost) 'Per-piece cost after unit change'
            } else { Assert-Equal $form.CostByOne.Text '' 'Per-piece field cleared for base unit' }
        }
    }
    $form.S_Stores = $true
    $form.IM_QTY = 0
    $form.ALL_QTY = 0
    $form.ReloadUnit()
    Assert-Equal $form.NewSalePrice_txt.Text $form.PriceTextBox.Text 'Existing zero-stock behavior preserved when stores enabled'
    $form.IM_Unit_cm.SelectedIndex = -1
    $before = $form.CalcCalls
    $form.ReloadUnit()
    Assert-Equal $form.CalcCalls $before 'Empty unit selection safely ignored'
    Write-Output "Completed $checks checks. No application window shown; no data writes performed. Average-cost calculation is stubbed, not tested."
} finally {
    if ($form) { $form.Dispose() }
    $connection.Dispose()
    $provider.Dispose()
}
