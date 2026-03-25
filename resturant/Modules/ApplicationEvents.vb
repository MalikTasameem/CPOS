
Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Delegate Sub SafeApplicationThreadException(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)

        'Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
        '    GetLastShow()
        'End Sub



        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            Try
                AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf AppDomain_UnhandledException
                AddHandler System.Windows.Forms.Application.ThreadException, AddressOf app_ThreadException
                Recover_File_Setting()
                Build_Connection()
                If CheckConnection(MY_Settings.SqlConStr) Then
                    prepare_Sys()
                Else
                    MsgBox("تحقق من إعدادات الإتصال", MsgBoxStyle.Exclamation)
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Application Startup")
                Recover_Setting()
            End Try

            'There are three places to catch all global unhandled exceptions:
            'AppDomain.CurrentDomain.UnhandledException event.
            'System.Windows.Forms.Application.ThreadException event.
            'MyApplication.UnhandledException event.

        End Sub

        Private Sub ShowDebugOutput(ByVal ex As Exception)

            Dim Line As String = "*******************************************************************************************************************************"
            Dim Writer As System.IO.StreamWriter
            Writer = New System.IO.StreamWriter("Errors_Exeption.txt", OpenMode.Input)
            Writer.Write(vbNewLine + Line + vbNewLine + Date.Now.ToString + vbNewLine + ex.ToString())
            Writer.Close()

            MsgBox(ex.Message)
            '-----------------------------------------------------------

            'Perform application cleanup
            'TODO: Add your application cleanup code here.

            'Exit the application - Or try to recover from the exception:
            'Environment.Exit(0)
        End Sub


        Private Sub app_ThreadException(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)

            'This is not thread safe, so make it thread safe.
            If F_MainForm.InvokeRequired Then
                ' Invoke back to the main thread
                F_MainForm.Invoke(New SafeApplicationThreadException(AddressOf app_ThreadException), New Object() {sender, e})
            Else
                ShowDebugOutput(e.Exception)
            End If

        End Sub

        Private Sub AppDomain_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
            ShowDebugOutput(DirectCast(e.ExceptionObject, Exception))
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            ShowDebugOutput(e.Exception)
        End Sub
    End Class

End Namespace

