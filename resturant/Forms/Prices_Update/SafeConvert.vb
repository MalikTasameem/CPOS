Public Module SafeConvert

    Public Function SafeInt(val As Object, Optional defaultValue As Integer = 0) As Integer
        If val Is Nothing OrElse IsDBNull(val) Then Return defaultValue
        Dim s As String = val.ToString().Trim()
        If s = "" Then Return defaultValue
        Dim i As Integer
        If Integer.TryParse(s, i) Then Return i Else Return defaultValue
    End Function

    Public Function SafeDec(val As Object, Optional defaultValue As Decimal = 0D) As Decimal
        If val Is Nothing OrElse IsDBNull(val) Then Return defaultValue
        Dim s As String = val.ToString().Trim()
        If s = "" Then Return defaultValue
        Dim d As Decimal
        If Decimal.TryParse(s, d) Then Return d Else Return defaultValue
    End Function

    Public Function SafeDate(val As Object, Optional defaultValue As DateTime = Nothing) As DateTime
        If val Is Nothing OrElse IsDBNull(val) Then
            If defaultValue = Nothing Then Return DateTime.MinValue Else Return defaultValue
        End If
        Dim s As String = val.ToString().Trim()
        If s = "" Then Return DateTime.MinValue
        Dim d As DateTime
        If DateTime.TryParse(s, d) Then Return d Else Return DateTime.MinValue
    End Function

    Public Function SafeStr(val As Object, Optional defaultValue As String = "") As String
        If val Is Nothing OrElse IsDBNull(val) Then Return defaultValue
        Return val.ToString().Trim()
    End Function

End Module
