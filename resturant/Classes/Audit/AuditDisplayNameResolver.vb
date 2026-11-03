Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text

' Adds immutable, human-readable entity names to the text that is fingerprinted and
' stored with a new event. The numeric identifiers remain in the original message.
Friend NotInheritable Class AuditDisplayNameResolver
    Private NotInheritable Class NameBucket
        Friend ReadOnly Ids As New List(Of Integer)()
        Friend ReadOnly Names As New List(Of String)()
    End Class

    Private Sub New()
    End Sub

    Friend Shared Function Decorate(cn As SqlConnection, tx As SqlTransaction,
                                    item As AuditEventRequest, notes As String) As String
        If cn Is Nothing Then Throw New ArgumentNullException("cn")
        If tx Is Nothing Then Throw New ArgumentNullException("tx")
        If item Is Nothing Then Throw New ArgumentNullException("item")

        Dim itemNames As New NameBucket(), accountNames As New NameBucket()
        Dim storeNames As New NameBucket(), treasuryNames As New NameBucket()
        Collect(item, itemNames, accountNames, storeNames, treasuryNames)

        ResolveNames(cn, tx, "dbo.IM_Menu", "IM_ID", "item_name", itemNames)
        ResolveNames(cn, tx, "dbo.Agents", "AG_ID", "Ag_name", accountNames)
        ResolveNames(cn, tx, "dbo.Stores", "ST_ID", "ST_Name", storeNames)
        ResolveNames(cn, tx, "dbo.TreasuryCard", "Tr_ID", "Tr_Name", treasuryNames)

        Return AppendNames(notes, itemNames.Names, accountNames.Names, storeNames.Names, treasuryNames.Names,
                           "التعريف المحفوظ")
    End Function

    ' Testable path for names already captured by an operation contract.
    Friend Shared Function DecorateCaptured(item As AuditEventRequest, notes As String) As String
        If item Is Nothing Then Throw New ArgumentNullException("item")
        Dim itemNames As New NameBucket(), accountNames As New NameBucket()
        Dim storeNames As New NameBucket(), treasuryNames As New NameBucket()
        Collect(item, itemNames, accountNames, storeNames, treasuryNames)
        Return AppendNames(notes, itemNames.Names, accountNames.Names, storeNames.Names, treasuryNames.Names,
                           "التعريف المحفوظ")
    End Function

    Private Shared Sub Collect(item As AuditEventRequest, itemNames As NameBucket, accountNames As NameBucket,
                               storeNames As NameBucket, treasuryNames As NameBucket)
        For Each change As AuditFieldChange In item.Changes
            Dim code = If(change.FieldCode, "")
            If IsItemName(code) Then
                AddValues(itemNames.Names, change)
            ElseIf IsAccountName(code) Then
                AddValues(accountNames.Names, change)
            ElseIf IsStoreName(code) Then
                AddValues(storeNames.Names, change)
            ElseIf IsTreasuryName(code) Then
                AddValues(treasuryNames.Names, change)
            End If

            If IsItemId(code) Then
                AddIds(itemNames.Ids, change)
            ElseIf IsAccountId(code) Then
                AddIds(accountNames.Ids, change)
            ElseIf IsStoreId(code) Then
                AddIds(storeNames.Ids, change)
            ElseIf IsTreasuryId(code) Then
                AddIds(treasuryNames.Ids, change)
            End If
        Next
    End Sub

    Private Shared Sub AddValues(target As List(Of String), change As AuditFieldChange)
        AddName(target, If(change.After Is Nothing, Nothing, If(change.After.State = AuditValueState.Present, change.After.Text, Nothing)))
        AddName(target, If(change.Before Is Nothing, Nothing, If(change.Before.State = AuditValueState.Present, change.Before.Text, Nothing)))
    End Sub

    Private Shared Sub AddIds(target As List(Of Integer), change As AuditFieldChange)
        AddId(target, change.After)
        AddId(target, change.Before)
    End Sub

    Private Shared Sub AddId(target As List(Of Integer), value As AuditValue)
        If value Is Nothing OrElse value.State <> AuditValueState.Present Then Return
        Dim parsed As Integer
        If Integer.TryParse(value.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, parsed) AndAlso parsed > 0 AndAlso Not target.Contains(parsed) Then target.Add(parsed)
    End Sub

    Private Shared Sub AddName(target As List(Of String), value As String)
        Dim normalized = NormalizeName(value)
        If normalized.Length = 0 Then Return
        For Each current As String In target
            If String.Equals(current, normalized, StringComparison.OrdinalIgnoreCase) Then Return
        Next
        target.Add(normalized)
    End Sub

    Private Shared Function NormalizeName(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return ""
        Dim result = value.Replace(ControlChars.Cr, " ").Replace(ControlChars.Lf, " ").Replace(ControlChars.Tab, " ").Trim()
        While result.Contains("  ")
            result = result.Replace("  ", " ")
        End While
        result = result.Replace("«", "‹").Replace("»", "›")
        If result.Length > 250 Then result = result.Substring(0, 247) & "..."
        Return result
    End Function

    Private Shared Sub ResolveNames(cn As SqlConnection, tx As SqlTransaction, tableName As String,
                                    idColumn As String, nameColumn As String, bucket As NameBucket)
        If bucket.Ids.Count = 0 Then Return
        Dim sql As New StringBuilder("SELECT CONVERT(nvarchar(500),[")
        sql.Append(nameColumn).Append("]) FROM ").Append(tableName).Append(" WHERE [").Append(idColumn).Append("] IN (")
        Using cmd As New SqlCommand()
            cmd.Connection = cn
            cmd.Transaction = tx
            For i As Integer = 0 To bucket.Ids.Count - 1
                If i > 0 Then sql.Append(",")
                Dim parameterName = "@P" & i.ToString(CultureInfo.InvariantCulture)
                sql.Append(parameterName)
                cmd.Parameters.Add(parameterName, SqlDbType.Int).Value = bucket.Ids(i)
            Next
            sql.Append(") ORDER BY [").Append(idColumn).Append("]")
            cmd.CommandText = sql.ToString()
            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    If Not reader.IsDBNull(0) Then AddName(bucket.Names, reader.GetString(0))
                End While
            End Using
        End Using
    End Sub

    Friend Shared Function AppendNames(notes As String, itemNames As IEnumerable(Of String),
                                       accountNames As IEnumerable(Of String), storeNames As IEnumerable(Of String),
                                       treasuryNames As IEnumerable(Of String), label As String) As String
        Dim original = If(notes, "")
        Dim parts As New List(Of String)()
        AddDisplayPart(parts, original, "الصنف", itemNames)
        AddDisplayPart(parts, original, "الحساب/العميل/المورد", accountNames)
        AddDisplayPart(parts, original, "المخزن", storeNames)
        AddDisplayPart(parts, original, "الخزينة/المصرف", treasuryNames)
        If parts.Count = 0 Then Return original
        Return original.TrimEnd() & " | " & label & ": " & String.Join("؛ ", parts.ToArray()) & "."
    End Function

    Private Shared Sub AddDisplayPart(parts As List(Of String), original As String, title As String,
                                      values As IEnumerable(Of String))
        Dim display As New List(Of String)()
        For Each value As String In values
            Dim normalized = NormalizeName(value)
            If normalized.Length > 0 AndAlso original.IndexOf(normalized, StringComparison.OrdinalIgnoreCase) < 0 AndAlso Not display.Contains(normalized) Then
                display.Add(normalized)
            End If
            If display.Count = 3 Then Exit For
        Next
        If display.Count = 0 Then Return
        parts.Add(title & " «" & String.Join("»، «", display.ToArray()) & "»")
    End Sub

    Private Shared Function IsItemName(code As String) As Boolean
        Dim key = code.Replace("_", "")
        Return key.IndexOf("ItemName", StringComparison.OrdinalIgnoreCase) >= 0
    End Function

    Private Shared Function IsAccountName(code As String) As Boolean
        Return code.Equals("AgentName", StringComparison.OrdinalIgnoreCase) OrElse
               code.Equals("AccountName", StringComparison.OrdinalIgnoreCase) OrElse
               code.Equals("SalaryEmployeeName", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function IsStoreName(code As String) As Boolean
        Return code.Equals("StoreName", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function IsTreasuryName(code As String) As Boolean
        Return code.Equals("TreasuryName", StringComparison.OrdinalIgnoreCase) OrElse code.Equals("TrName", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function IsItemId(code As String) As Boolean
        If code.IndexOf("U_IM_ID", StringComparison.OrdinalIgnoreCase) >= 0 Then Return False
        If code.EndsWith("ItemId", StringComparison.OrdinalIgnoreCase) Then Return True
        If code.IndexOf("_IM_ID", StringComparison.OrdinalIgnoreCase) >= 0 Then Return True
        Return {"ItemId", "ComponentId", "RecipeComponentId", "FormatComponentId", "SplitBalanceItem"}.
            Contains(code, StringComparer.OrdinalIgnoreCase)
    End Function

    Private Shared Function IsAccountId(code As String) As Boolean
        If code.EndsWith("AccountId", StringComparison.OrdinalIgnoreCase) OrElse code.EndsWith("EmployeeId", StringComparison.OrdinalIgnoreCase) Then Return True
        Return {"Rct_AG_ID", "AccountAGIDFROM", "AccountAGIDTO", "SalaryAGID", "SalaryAdjEmployeeId", "InvDocument_AG_ID", "UserAgent", "CommissionAccountId"}.
            Contains(code, StringComparer.OrdinalIgnoreCase)
    End Function

    Private Shared Function IsStoreId(code As String) As Boolean
        Return code.EndsWith("StoreId", StringComparison.OrdinalIgnoreCase) OrElse
               code.IndexOf("_ST_ID", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               code.Equals("SplitBalanceStore", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function IsTreasuryId(code As String) As Boolean
        Return code.EndsWith("TreasuryId", StringComparison.OrdinalIgnoreCase) OrElse
               {"Rct_Tr_ID", "RctTr_Tr_ID", "CashTrID", "UserTreasury"}.Contains(code, StringComparer.OrdinalIgnoreCase)
    End Function
End Class
