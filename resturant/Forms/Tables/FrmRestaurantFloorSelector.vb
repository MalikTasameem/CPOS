Imports System.Data
Imports System.Drawing

Public Class FrmRestaurantFloorSelector

    Private ReadOnly Repository As New RestaurantFloorLayoutRepository()
    Private CurrentFlateID As Integer = 0

    Private Sub FrmRestaurantFloorSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        FloorCanvas.IsDesignMode = False
        LoadFlates()
        RefreshLayout()
    End Sub

    Private Sub LoadFlates()
        Dim dt As DataTable = Repository.LoadFlates()
        cmbFlates.DataSource = dt
        cmbFlates.DisplayMember = "Flate_Name"
        cmbFlates.ValueMember = "Flate_ID"

        Try
            If U_Flate_ID > 0 Then cmbFlates.SelectedValue = U_Flate_ID
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RefreshLayout()
        If cmbFlates.SelectedValue Is Nothing OrElse TypeOf cmbFlates.SelectedValue Is DataRowView Then Return

        CurrentFlateID = Convert.ToInt32(cmbFlates.SelectedValue)

        Dim tablesDt As DataTable = Repository.LoadTables(CurrentFlateID)
        Dim elements As List(Of RestaurantFloorElement) = Repository.LoadLayout(CurrentFlateID)
        MergeTablesWithLayout(elements, tablesDt)

        FloorCanvas.Elements = elements
        lblStatus.Text = "اختر الطاولة المطلوبة من المخطط"
    End Sub

    Private Sub MergeTablesWithLayout(elements As List(Of RestaurantFloorElement), tablesDt As DataTable)
        Dim representedTables As New List(Of Integer)()

        For Each element As RestaurantFloorElement In elements
            If element.TB_ID.HasValue Then representedTables.Add(element.TB_ID.Value)
        Next

        Dim index As Integer = representedTables.Count
        For Each row As DataRow In tablesDt.Rows
            Dim tbId As Integer = Convert.ToInt32(row("TB_ID"))
            If representedTables.Contains(tbId) Then
                ApplyTableState(elements, row)
                Continue For
            End If

            Dim point As Point = GetDefaultTablePoint(index)
            Dim element As New RestaurantFloorElement()
            element.Flate_ID = CurrentFlateID
            element.TB_ID = tbId
            element.ElementType = "Table"
            element.ElementText = row("T_Name").ToString()
            element.X_Pos = point.X
            element.Y_Pos = point.Y
            element.WidthValue = 115
            element.HeightValue = 80
            element.SeatsCount = 4
            element.BackColorArgb = Color.WhiteSmoke.ToArgb()
            element.ForeColorArgb = Color.FromArgb(15, 23, 42).ToArgb()
            element.IsBusy = GetBool(row("isbusy"))
            element.IsCash = GetBool(row("is_Cash"))
            element.ZIndex = index
            elements.Add(element)
            index += 1
        Next
    End Sub

    Private Sub ApplyTableState(elements As List(Of RestaurantFloorElement), row As DataRow)
        Dim tbId As Integer = Convert.ToInt32(row("TB_ID"))

        For Each element As RestaurantFloorElement In elements
            If element.TB_ID.HasValue AndAlso element.TB_ID.Value = tbId Then
                element.ElementText = row("T_Name").ToString()
                element.IsBusy = GetBool(row("isbusy"))
                element.IsCash = GetBool(row("is_Cash"))
                Exit For
            End If
        Next
    End Sub

    Private Function GetDefaultTablePoint(index As Integer) As Point
        Dim columns As Integer = Math.Max(1, CInt(Math.Floor((FloorCanvas.Width - 60) / 150.0R)))
        Dim x As Integer = 30 + ((index Mod columns) * 150)
        Dim y As Integer = 30 + ((index \ columns) * 125)
        Return New Point(x, y)
    End Function

    Private Function GetBool(value As Object) As Boolean
        If value Is Nothing OrElse value Is DBNull.Value Then Return False

        Dim boolValue As Boolean
        Dim text As String = value.ToString().Trim()
        If text = "" Then Return False
        If Boolean.TryParse(text, boolValue) Then Return boolValue

        Dim numberValue As Decimal
        If Decimal.TryParse(text, numberValue) Then Return numberValue <> 0D

        Return False
    End Function

    Private Sub cmbFlates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFlates.SelectedIndexChanged
        RefreshLayout()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        RefreshLayout()
    End Sub

    Private Sub FloorCanvas_ElementSelected(element As RestaurantFloorElement) Handles FloorCanvas.ElementSelected
        If element Is Nothing Then Return
        If element.TB_ID.HasValue = False Then Return

        SelectTable(element)
    End Sub

    Private Sub SelectTable(element As RestaurantFloorElement)
        Dim tbId As Integer = element.TB_ID.Value

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Update_Table"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", F_POS.T_ID)
            .Parameters.AddWithValue("@TB_ID", tbId)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            F_POS.TB_ID = tbId
            F_POS.TB_is_Cash = If(element.IsCash, 1, 0)
            F_POS.Fill_Bill_Info()
            F_POS.Check_Table()
            Me.Close()
        End If
    End Sub

    Private Sub btnNoneTable_Click(sender As Object, e As EventArgs) Handles btnNoneTable.Click
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_NONE_Table"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", F_POS.T_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            F_POS.TB_ID = 0
            F_POS.Fill_Bill_Info()
            F_POS.Check_Table()
            Me.Close()
        End If
    End Sub

    Private Sub btnOldMenu_Click(sender As Object, e As EventArgs) Handles btnOldMenu.Click
        Me.Close()
        F_SB_TablesMenu = New SB_TablesMenu
        F_SB_TablesMenu.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class
