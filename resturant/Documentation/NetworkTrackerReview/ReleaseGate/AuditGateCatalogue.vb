Option Strict On
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Globalization

Friend Module AuditGateCatalogue
    Public Function Main(args As String()) As Integer
        Try
            Dim files = Directory.GetFiles(args(0), "AuditContract*.vb")
            Dim literals = files.SelectMany(Function(path) Regex.Matches(File.ReadAllText(path), """([A-Z][A-Za-z0-9]+\.[A-Z][A-Za-z0-9]+)""").Cast(Of Match)().Select(Function(m) m.Groups(1).Value)).Distinct().OrderBy(Function(x) x)
            For Each code In literals
                Try
                    Dim domain = AuditContract.Domain(code)
                    If domain <> "AuditTest" Then Console.WriteLine(code & vbTab & domain)
                Catch ex As ArgumentException
                    ' Domain names and field labels are not operation codes.
                End Try
            Next
            ' Exercise canonical values under workstation cultures used by customers.
            For Each name In {"ar-LY", "ar-SA", "en-US", "en-GB", "fr-FR"}
                Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(name)
                Check(AuditValue.Capture("Decimal", 6.528D).Text = "6.528", "decimal culture " & name)
                Check(AuditValue.Capture("Float", 6.528R).Text = "6.528", "float culture " & name)
                Check(AuditValue.Capture("String", "1000").Text = "1000", "numeric-looking name " & name)
                Check(AuditValue.Capture("String", "عميل عربي").Text = "عميل عربي", "Arabic name")
                Check(AuditValue.Capture("String", "").Text = "", "empty is not null")
                Check(AuditValue.Capture("String", DBNull.Value).State = AuditValueState.SqlNull, "SQL NULL")
                Check(AuditValue.Capture("DateTime", New DateTime(2026, 11, 10, 16, 30, 1)).Text = "2026-11-10T16:30:01.0000000", "ISO date")
                Reject(Sub() AuditValue.Capture("String", 1), "implicit integer-to-string")
                Reject(Sub() AuditValue.Capture("Decimal", 1R), "implicit float-to-decimal")
                Reject(Sub() AuditValue.Capture("Float", Double.NaN), "NaN")
                Reject(Sub() AuditValue.Capture("Float", Double.PositiveInfinity), "infinity")
            Next
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString())
            Return 1
        End Try
    End Function

    Private Sub Check(ok As Boolean, message As String)
        If Not ok Then Throw New Exception(message)
    End Sub

    Private Sub Reject(action As Action, message As String)
        Try
            action()
        Catch ex As ArgumentException
            Return
        End Try
        Throw New Exception("Expected rejection: " & message)
    End Sub
End Module
