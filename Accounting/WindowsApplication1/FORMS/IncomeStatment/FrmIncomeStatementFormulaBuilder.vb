Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Partial Public Class FrmIncomeStatementFormulaBuilder

    Public Property TemplateID As Integer = 0
    Public Property FormulaLineID As Integer = 0
    Public Property FormulaLineCode As String = ""
    Public Property FormulaLineName As String = ""
    Public Property CurrentUserID As Integer = 1

    Public Property SavedSuccessfully As Boolean = False

    Private _details As DataTable

#Region "Load"

    Private Sub FrmIncomeStatementFormulaBuilder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            InitGrids()
            InitDetailsTable()

            lblFormulaLine.Text = "بند المعادلة: " & FormulaLineCode & " - " & FormulaLineName

            LoadSourceLines()
            LoadCurrentFormulaDetails()

            BuildFormulaText()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub InitGrids()
        ConfigureGrid(dgvSourceLines)
        ConfigureGrid(dgvFormulaDetails)
    End Sub

    Private Sub ConfigureGrid(dgv As DataGridView)
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.ReadOnly = True
        dgv.MultiSelect = False
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.BackgroundColor = Color.White
    End Sub

    Private Sub InitDetailsTable()
        _details = New DataTable()
        _details.Columns.Add("SourceLineID", GetType(Integer))
        _details.Columns.Add("SourceLineCode", GetType(String))
        _details.Columns.Add("SourceLineName", GetType(String))
        _details.Columns.Add("OperationType", GetType(Integer))
        _details.Columns.Add("OperationTypeName", GetType(String))
        _details.Columns.Add("SortOrder", GetType(Integer))

        dgvFormulaDetails.DataSource = _details
        FormatFormulaDetailsGrid()
    End Sub

#End Region

#Region "Connection"

    Private Function GetConnectionString() As String
        ' عدّلها حسب مشروعك
        Return MY_Settings.SqlConStr
    End Function

    Private Function GetConnection() As SqlConnection
        Return New SqlConnection(GetConnectionString())
    End Function

#End Region

#Region "Database"

    Private Function ExecuteDataTable(procedureName As String,
                                      Optional parameters As List(Of SqlParameter) = Nothing) As DataTable

        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand(procedureName, con)
                cmd.CommandTimeout = 120
                cmd.CommandType = CommandType.StoredProcedure

                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters.ToArray())
                End If

                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    Return dt
                End Using
            End Using
        End Using
    End Function

#End Region

#Region "Load Data"

    Private Sub LoadSourceLines()
        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_GetFormulaSourceLines",
            New List(Of SqlParameter) From {
                New SqlParameter("@TemplateID", TemplateID),
                New SqlParameter("@FormulaLineID", FormulaLineID)
            })

        dgvSourceLines.DataSource = dt
        FormatSourceLinesGrid()

        lblStatus.Text = "عدد البنود المتاحة: " & dt.Rows.Count.ToString()
    End Sub

    Private Sub LoadCurrentFormulaDetails()
        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_GetFormulaDetails",
            New List(Of SqlParameter) From {
                New SqlParameter("@FormulaLineID", FormulaLineID)
            })

        _details.Rows.Clear()

        For Each r As DataRow In dt.Rows
            Dim newRow As DataRow = _details.NewRow()
            newRow("SourceLineID") = SafeInt(r("SourceLineID"))
            newRow("SourceLineCode") = SafeString(r("SourceLineCode"))
            newRow("SourceLineName") = SafeString(r("SourceLineName"))
            newRow("OperationType") = SafeInt(r("OperationType"))
            newRow("OperationTypeName") = SafeString(r("OperationTypeName"))
            newRow("SortOrder") = SafeInt(r("SortOrder"))
            _details.Rows.Add(newRow)
        Next

        ReNumberDetails()
    End Sub

#End Region

#Region "Buttons"

    Private Sub btnAddPlus_Click(sender As Object, e As EventArgs) Handles btnAddPlus.Click
        AddSelectedSource(1)
    End Sub

    Private Sub btnAddSubtract_Click(sender As Object, e As EventArgs) Handles btnAddSubtract.Click
        AddSelectedSource(2)
    End Sub

    Private Sub AddSelectedSource(operationType As Integer)
        Try
            If dgvSourceLines.CurrentRow Is Nothing Then
                MessageBox.Show("اختر بندًا من البنود المتاحة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim row As DataRowView = TryCast(dgvSourceLines.CurrentRow.DataBoundItem, DataRowView)
            If row Is Nothing Then Return

            Dim sourceLineID As Integer = SafeInt(row("LineID"))
            Dim sourceLineCode As String = SafeString(row("LineCode"))
            Dim sourceLineName As String = SafeString(row("LineName"))

            If sourceLineID <= 0 Then Return

            For Each r As DataRow In _details.Rows
                If SafeInt(r("SourceLineID")) = sourceLineID Then
                    MessageBox.Show("هذا البند مضاف مسبقًا في المعادلة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            Next

            Dim newRow As DataRow = _details.NewRow()
            newRow("SourceLineID") = sourceLineID
            newRow("SourceLineCode") = sourceLineCode
            newRow("SourceLineName") = sourceLineName
            newRow("OperationType") = operationType
            newRow("OperationTypeName") = If(operationType = 1, "إضافة", "خصم")
            newRow("SortOrder") = _details.Rows.Count + 1

            _details.Rows.Add(newRow)

            ReNumberDetails()
            BuildFormulaText()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnRemoveDetail_Click(sender As Object, e As EventArgs) Handles btnRemoveDetail.Click
        Try
            If dgvFormulaDetails.CurrentRow Is Nothing Then Return

            Dim rowView As DataRowView = TryCast(dgvFormulaDetails.CurrentRow.DataBoundItem, DataRowView)
            If rowView Is Nothing Then Return

            rowView.Row.Delete()

            ReNumberDetails()
            BuildFormulaText()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnMoveUp_Click(sender As Object, e As EventArgs) Handles btnMoveUp.Click
        MoveSelectedDetail(-1)
    End Sub

    Private Sub btnMoveDown_Click(sender As Object, e As EventArgs) Handles btnMoveDown.Click
        MoveSelectedDetail(1)
    End Sub

    Private Sub MoveSelectedDetail(direction As Integer)
        Try
            If dgvFormulaDetails.CurrentRow Is Nothing Then Return

            Dim index As Integer = dgvFormulaDetails.CurrentRow.Index
            Dim newIndex As Integer = index + direction

            If newIndex < 0 OrElse newIndex >= _details.Rows.Count Then Return

            Dim current As DataRow = _details.Rows(index)
            Dim target As DataRow = _details.Rows(newIndex)

            Dim tmpSourceLineID As Object = current("SourceLineID")
            Dim tmpSourceLineCode As Object = current("SourceLineCode")
            Dim tmpSourceLineName As Object = current("SourceLineName")
            Dim tmpOperationType As Object = current("OperationType")
            Dim tmpOperationTypeName As Object = current("OperationTypeName")

            current("SourceLineID") = target("SourceLineID")
            current("SourceLineCode") = target("SourceLineCode")
            current("SourceLineName") = target("SourceLineName")
            current("OperationType") = target("OperationType")
            current("OperationTypeName") = target("OperationTypeName")

            target("SourceLineID") = tmpSourceLineID
            target("SourceLineCode") = tmpSourceLineCode
            target("SourceLineName") = tmpSourceLineName
            target("OperationType") = tmpOperationType
            target("OperationTypeName") = tmpOperationTypeName

            ReNumberDetails()
            BuildFormulaText()

            dgvFormulaDetails.ClearSelection()
            dgvFormulaDetails.Rows(newIndex).Selected = True
            dgvFormulaDetails.CurrentCell = dgvFormulaDetails.Rows(newIndex).Cells(0)

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If _details.Rows.Count = 0 Then
                MessageBox.Show("لا توجد تفاصيل للمعادلة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim formulaDetails As String = BuildFormulaDetailsParameter()
            Dim formulaText As String = txtFormulaText.Text.Trim()

            Dim dt As DataTable = ExecuteDataTable(
                "dbo.ACC_IncomeStatement_RebuildFormula",
                New List(Of SqlParameter) From {
                    New SqlParameter("@FormulaLineID", FormulaLineID),
                    New SqlParameter("@FormulaDetails", formulaDetails),
                    New SqlParameter("@FormulaText", formulaText),
                    New SqlParameter("@UserID", CurrentUserID)
                })

            If dt.Rows.Count > 0 AndAlso dt.Columns.Contains("IsSuccess") Then
                Dim isSuccess As Boolean = False
                Boolean.TryParse(dt.Rows(0)("IsSuccess").ToString(), isSuccess)

                If isSuccess Then
                    SavedSuccessfully = True
                    MessageBox.Show("تم حفظ المعادلة بنجاح.", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show(SafeString(dt.Rows(0)("MessageText")), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                SavedSuccessfully = True
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

#Region "Formula Build"

    Private Sub ReNumberDetails()
        For i As Integer = 0 To _details.Rows.Count - 1
            If _details.Rows(i).RowState <> DataRowState.Deleted Then
                _details.Rows(i)("SortOrder") = i + 1
            End If
        Next

        _details.AcceptChanges()
    End Sub

    Private Sub BuildFormulaText()
        Dim sb As New StringBuilder()

        For i As Integer = 0 To _details.Rows.Count - 1
            Dim r As DataRow = _details.Rows(i)

            Dim op As Integer = SafeInt(r("OperationType"))
            Dim code As String = SafeString(r("SourceLineCode"))
            Dim name As String = SafeString(r("SourceLineName"))

            Dim caption As String = If(code <> "", code, name)

            If i = 0 Then
                If op = 2 Then
                    sb.Append("- ")
                End If
                sb.Append(caption)
            Else
                If op = 1 Then
                    sb.Append(" + ")
                Else
                    sb.Append(" - ")
                End If

                sb.Append(caption)
            End If
        Next

        txtFormulaText.Text = sb.ToString()
    End Sub

    Private Function BuildFormulaDetailsParameter() As String
        Dim sb As New StringBuilder()

        For Each r As DataRow In _details.Rows
            If r.RowState = DataRowState.Deleted Then Continue For

            If sb.Length > 0 Then
                sb.Append(";")
            End If

            sb.Append(SafeInt(r("SourceLineID")).ToString())
            sb.Append(":")
            sb.Append(SafeInt(r("OperationType")).ToString())
        Next

        Return sb.ToString()
    End Function

#End Region

#Region "Formatting"

    Private Sub FormatSourceLinesGrid()
        If dgvSourceLines.DataSource Is Nothing Then Return

        HideColumnIfExists(dgvSourceLines, "LineID")
        HideColumnIfExists(dgvSourceLines, "LineType")

        SetHeader(dgvSourceLines, "LineCode", "الكود")
        SetHeader(dgvSourceLines, "LineName", "البند")
        SetHeader(dgvSourceLines, "LineTypeName", "النوع")
        SetHeader(dgvSourceLines, "SortOrder", "الترتيب")
        SetHeader(dgvSourceLines, "LevelNo", "المستوى")
    End Sub

    Private Sub FormatFormulaDetailsGrid()
        If dgvFormulaDetails.DataSource Is Nothing Then Return

        HideColumnIfExists(dgvFormulaDetails, "SourceLineID")
        HideColumnIfExists(dgvFormulaDetails, "OperationType")

        SetHeader(dgvFormulaDetails, "SourceLineCode", "الكود")
        SetHeader(dgvFormulaDetails, "SourceLineName", "البند")
        SetHeader(dgvFormulaDetails, "OperationTypeName", "العملية")
        SetHeader(dgvFormulaDetails, "SortOrder", "الترتيب")
    End Sub

    Private Sub SetHeader(dgv As DataGridView, columnName As String, headerText As String)
        If dgv.Columns.Contains(columnName) Then
            dgv.Columns(columnName).HeaderText = headerText
        End If
    End Sub

    Private Sub HideColumnIfExists(dgv As DataGridView, columnName As String)
        If dgv.Columns.Contains(columnName) Then
            dgv.Columns(columnName).Visible = False
        End If
    End Sub

#End Region

#Region "Helpers"

    Private Function SafeInt(value As Object) As Integer
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0

        Dim result As Integer
        If Integer.TryParse(value.ToString(), result) Then Return result

        Return 0
    End Function

    Private Function SafeString(value As Object) As String
        If value Is Nothing OrElse value Is DBNull.Value Then Return ""
        Return value.ToString()
    End Function

    Private Sub ShowError(ex As Exception)
        lblStatus.Text = "خطأ: " & ex.Message
        MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

#End Region

End Class