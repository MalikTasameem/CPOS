Public Class OSK_Class
    Declare Function Wow64DisableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Declare Function Wow64EnableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Private osk As String = "C:\Windows\System32\osk.exe"
    Public Sub OSK_OPEN()
        Try

            Dim old As Long
            If Environment.Is64BitOperatingSystem Then
                If Not Wow64DisableWow64FsRedirection(old) Then
                    Process.Start(osk)
                    Wow64EnableWow64FsRedirection(old)
                End If
            Else
                Process.Start(osk)
            End If

            '  Shell("cmd.exe /c start osk")
        Catch ex As Exception
        End Try
    End Sub

End Class
