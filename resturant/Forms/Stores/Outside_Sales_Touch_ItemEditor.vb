Imports System.Data.SqlClient
Imports System.Globalization

Public Class Outside_Sales_Touch_ItemEditor
    Private ReadOnly _itemId As Integer
    Private ReadOnly _defaultStoreId As Integer
    Private ReadOnly _canChangeStore As Boolean
    Private _loading As Boolean
    Private _requiresExpiry As Boolean
    Private _unitId As Integer
    Private _unitCargo As Decimal = 1D
    Private _activeTextBox As TextBox
    Private _replaceOnNextKey As Boolean = True

    Public ReadOnly Property SelectedStoreId As Integer
        Get
            If StoreComboBox.SelectedValue Is Nothing Then Return 0
            Return Convert.ToInt32(StoreComboBox.SelectedValue)
        End Get
    End Property

    Public ReadOnly Property SelectedUnitId As Integer
        Get
            Return _unitId
        End Get
    End Property

    Public ReadOnly Property EnteredQuantity As Decimal
        Get
            Dim value As Decimal
            Decimal.TryParse(QuantityTextBox.Text, value)
            Return value
        End Get
    End Property

    Public ReadOnly Property EnteredPrice As Decimal
        Get
            Dim value As Decimal
            Decimal.TryParse(PriceTextBox.Text, value)
            Return value
        End Get
    End Property

    Public ReadOnly Property RequiresExpiry As Boolean
        Get
            Return _requiresExpiry
        End Get
    End Property

    Public ReadOnly Property SelectedExpiryDate As Date
        Get
            Return ExpiryDatePicker.Value.Date
        End Get
    End Property

    Public Sub New(itemId As Integer, itemName As String, defaultStoreId As Integer, canChangeStore As Boolean)
        InitializeComponent()
        _itemId = itemId
        _defaultStoreId = defaultStoreId
        _canChangeStore = canChangeStore
        ItemNameLabel.Text = itemName
    End Sub

    Private Sub ItemEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        ItemNameLabel.BackColor = Color.FromArgb(39, 55, 70)
        ItemNameLabel.ForeColor = Color.White
        ConfirmButton.BackColor = Color.SeaGreen
        ConfirmButton.ForeColor = Color.White
        ConfigureKeypadAppearance()
        LoadItemData()
        ActivateNumericField(QuantityTextBox, True)
    End Sub

    Private Sub ConfigureKeypadAppearance()
        Dim buttons() As Button = {Key0, Key1, Key2, Key3, Key4, Key5, Key6, Key7, Key8, Key9, DecimalButton, BackspaceButton, ClearButton}
        For Each keypadButton As Button In buttons
            keypadButton.Font = New Font("Segoe UI Semibold", 14.0!, FontStyle.Bold)
            keypadButton.Margin = New Padding(3)
            keypadButton.FlatStyle = FlatStyle.Flat
        Next
        ClearButton.BackColor = Color.IndianRed
        ClearButton.ForeColor = Color.White
        BackspaceButton.BackColor = Color.SlateGray
        BackspaceButton.ForeColor = Color.White
    End Sub

    Private Sub LoadItemData()
        Try
            _loading = True
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                connection.Open()

                Dim stores As New DataTable()
                Using adapter As New SqlDataAdapter("SELECT ST_ID,ST_Name FROM dbo.Stores ORDER BY ST_Name", connection)
                    adapter.Fill(stores)
                End Using
                StoreComboBox.DataSource = stores
                StoreComboBox.DisplayMember = "ST_Name"
                StoreComboBox.ValueMember = "ST_ID"
                If _defaultStoreId > 0 Then StoreComboBox.SelectedValue = _defaultStoreId
                StoreComboBox.Enabled = _canChangeStore

                Dim units As New DataTable()
                Using adapter As New SqlDataAdapter("SELECT U_IM_ID,U_Name,U_ID,U_Cargo FROM dbo.IM_Menu_Units_V WHERE IM_ID=@IM_ID ORDER BY U_Cargo DESC", connection)
                    adapter.SelectCommand.Parameters.Add("@IM_ID", SqlDbType.Int).Value = _itemId
                    adapter.Fill(units)
                End Using
                UnitComboBox.DataSource = units
                UnitComboBox.DisplayMember = "U_Name"
                UnitComboBox.ValueMember = "U_IM_ID"
                UnitComboBox.Enabled = units.Rows.Count > 1

                Using command As New SqlCommand("SELECT TOP(1) isValid FROM dbo.IM_Menu_V WHERE IM_ID=@IM_ID", connection)
                    command.Parameters.Add("@IM_ID", SqlDbType.Int).Value = _itemId
                    Dim result As Object = command.ExecuteScalar()
                    _requiresExpiry = result IsNot Nothing AndAlso result IsNot DBNull.Value AndAlso Convert.ToBoolean(result)
                End Using
                ExpiryLabel.Visible = _requiresExpiry
                ExpiryDatePicker.Visible = _requiresExpiry
                LoadSelectedUnit(connection)
            End Using
        Catch ex As Exception
            StatusLabel.Text = ex.Message
            ConfirmButton.Enabled = False
        Finally
            _loading = False
        End Try
    End Sub

    Private Sub LoadSelectedUnit(connection As SqlConnection)
        If UnitComboBox.SelectedValue Is Nothing Then Return
        Using command As New SqlCommand("SELECT TOP(1) U_ID,U_Cargo FROM dbo.IM_Menu_Units_V WHERE U_IM_ID=@U_IM_ID AND IM_ID=@IM_ID", connection)
            command.Parameters.Add("@U_IM_ID", SqlDbType.Int).Value = Convert.ToInt32(UnitComboBox.SelectedValue)
            command.Parameters.Add("@IM_ID", SqlDbType.Int).Value = _itemId
            Using reader As SqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    _unitId = Convert.ToInt32(reader("U_ID"))
                    _unitCargo = Convert.ToDecimal(reader("U_Cargo"))
                End If
            End Using
        End Using

        Using command As New SqlCommand("SELECT ISNULL(SUM(QTY),0) FROM dbo.ST_Balance_V WHERE IM_ID=@IM_ID AND ST_ID=@ST_ID", connection)
            command.Parameters.Add("@IM_ID", SqlDbType.Int).Value = _itemId
            command.Parameters.Add("@ST_ID", SqlDbType.Int).Value = SelectedStoreId
            Dim baseQuantity As Decimal = Convert.ToDecimal(command.ExecuteScalar())
            StockLabel.Text = "المتوفر في المخزن: " & If(_unitCargo = 0D, 0D, baseQuantity / _unitCargo).ToString("N3")
        End Using

        PriceTextBox.Text = "0"
        Using command As New SqlCommand("SELECT TOP(1) Price FROM dbo.Pch_Details WHERE IM_ID=@IM_ID AND U_ID=@U_ID AND isDepended=1 ORDER BY Date DESC", connection)
            command.Parameters.Add("@IM_ID", SqlDbType.Int).Value = _itemId
            command.Parameters.Add("@U_ID", SqlDbType.Int).Value = _unitId
            Dim result As Object = command.ExecuteScalar()
            If result IsNot Nothing AndAlso result IsNot DBNull.Value Then PriceTextBox.Text = Convert.ToDecimal(result).ToString("0.###")
        End Using
    End Sub

    Private Sub SelectionChanged(sender As Object, e As EventArgs) Handles UnitComboBox.SelectedValueChanged, StoreComboBox.SelectedValueChanged
        If _loading OrElse UnitComboBox.SelectedValue Is Nothing OrElse StoreComboBox.SelectedValue Is Nothing Then Return
        Try
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                connection.Open()
                LoadSelectedUnit(connection)
            End Using
        Catch ex As Exception
            StatusLabel.Text = ex.Message
        End Try
    End Sub

    Private Sub QuantityTextBox_Enter(sender As Object, e As EventArgs) Handles QuantityTextBox.Enter, QuantityTextBox.Click
        ActivateNumericField(QuantityTextBox, False)
    End Sub

    Private Sub PriceTextBox_Enter(sender As Object, e As EventArgs) Handles PriceTextBox.Enter, PriceTextBox.Click
        ActivateNumericField(PriceTextBox, False)
    End Sub

    Private Sub ActivateNumericField(target As TextBox, replaceValue As Boolean)
        _activeTextBox = target
        _replaceOnNextKey = replaceValue
        QuantityTextBox.BackColor = If(target Is QuantityTextBox, Color.LightYellow, Color.White)
        PriceTextBox.BackColor = If(target Is PriceTextBox, Color.LightYellow, Color.White)
        target.Focus()
        target.SelectAll()
    End Sub

    Private Sub NumberButton_Click(sender As Object, e As EventArgs) Handles Key0.Click, Key1.Click, Key2.Click, Key3.Click, Key4.Click, Key5.Click, Key6.Click, Key7.Click, Key8.Click, Key9.Click
        If _activeTextBox Is Nothing Then ActivateNumericField(QuantityTextBox, True)
        Dim digit As String = Convert.ToString(DirectCast(sender, Button).Tag)
        If _replaceOnNextKey OrElse _activeTextBox.Text = "0" Then
            _activeTextBox.Text = digit
            _replaceOnNextKey = False
        ElseIf _activeTextBox.TextLength < 12 Then
            _activeTextBox.Text &= digit
        End If
        _activeTextBox.SelectionStart = _activeTextBox.TextLength
    End Sub

    Private Sub DecimalButton_Click(sender As Object, e As EventArgs) Handles DecimalButton.Click
        If _activeTextBox Is Nothing Then ActivateNumericField(QuantityTextBox, True)
        Dim separator As String = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        If _replaceOnNextKey Then
            _activeTextBox.Text = "0" & separator
            _replaceOnNextKey = False
        ElseIf Not _activeTextBox.Text.Contains(".") AndAlso Not _activeTextBox.Text.Contains(",") Then
            _activeTextBox.Text &= separator
        End If
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        If _activeTextBox Is Nothing Then ActivateNumericField(QuantityTextBox, True)
        _activeTextBox.Text = "0"
        _replaceOnNextKey = True
        _activeTextBox.SelectAll()
    End Sub

    Private Sub BackspaceButton_Click(sender As Object, e As EventArgs) Handles BackspaceButton.Click
        If _activeTextBox Is Nothing Then Return
        If _activeTextBox.TextLength > 1 AndAlso Not _replaceOnNextKey Then
            _activeTextBox.Text = _activeTextBox.Text.Substring(0, _activeTextBox.TextLength - 1)
        Else
            _activeTextBox.Text = "0"
            _replaceOnNextKey = True
        End If
    End Sub

    Private Sub NumericTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QuantityTextBox.KeyPress, PriceTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub ConfirmButton_Click(sender As Object, e As EventArgs) Handles ConfirmButton.Click
        Dim quantity As Decimal
        Dim price As Decimal
        If SelectedStoreId <= 0 OrElse _unitId <= 0 Then
            StatusLabel.Text = "تعذر تحديد المخزن أو الوحدة"
            Return
        End If
        If Not Decimal.TryParse(QuantityTextBox.Text, quantity) OrElse quantity <= 0D Then
            StatusLabel.Text = "أدخل كمية صحيحة أكبر من صفر"
            ActivateNumericField(QuantityTextBox, True)
            Return
        End If
        If String.IsNullOrWhiteSpace(PriceTextBox.Text) Then
            PriceTextBox.Text = "0"
        ElseIf Not Decimal.TryParse(PriceTextBox.Text, price) OrElse price < 0D Then
            StatusLabel.Text = "أدخل تكلفة صحيحة أو صفرًا"
            ActivateNumericField(PriceTextBox, True)
            Return
        End If
        If _requiresExpiry AndAlso Ban_Expierd_IM_MV AndAlso ExpiryDatePicker.Value.Date <= Date.Today Then
            StatusLabel.Text = "لا يمكن إدخال صنف منتهي الصلاحية"
            Return
        End If
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CancelButtonControl_Click(sender As Object, e As EventArgs) Handles CancelButtonControl.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
