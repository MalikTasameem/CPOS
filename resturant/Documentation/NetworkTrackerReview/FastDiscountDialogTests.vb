Option Strict On
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Globalization
Imports resturant

Friend Module FastDiscountDialogTests
    Private checks As Integer
    Private Sub Check(ok As Boolean, label As String)
        If Not ok Then Throw New Exception("FAIL " & label)
        checks += 1
    End Sub
    Private Sub Invoke(f As Fast_SB_Discount, name As String, args As Object())
        Dim m = GetType(Fast_SB_Discount).GetMethod(name, BindingFlags.NonPublic Or BindingFlags.Instance)
        If m Is Nothing Then Throw New Exception("Missing method " & name)
        m.Invoke(f, args)
    End Sub
    Private Function Field(f As Fast_SB_Discount, name As String) As Control
        Return DirectCast(GetType(Fast_SB_Discount).GetProperty(name, BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public).GetValue(f, Nothing), Control)
    End Function
    Private Function GlobalValue(name As String) As Double
        Return Convert.ToDouble(GetType(Fast_SB_Discount).Assembly.GetType("resturant.Identifiers").GetField(name, BindingFlags.Static Or BindingFlags.NonPublic Or BindingFlags.Public).GetValue(Nothing))
    End Function
    Private Sub SetGlobal(name As String, value As Object)
        Dim f = GetType(Fast_SB_Discount).Assembly.GetType("resturant.Identifiers").GetField(name, BindingFlags.Static Or BindingFlags.NonPublic Or BindingFlags.Public)
        f.SetValue(Nothing, Convert.ChangeType(value, f.FieldType, CultureInfo.InvariantCulture))
    End Sub
    Private Function NewDialog(draft As Boolean) As Fast_SB_Discount
        SetGlobal("T_ID", 50) : SetGlobal("SB_ID", 6) : SetGlobal("TOTAL", 10.26R) : SetGlobal("Disc", 1R) : SetGlobal("Pure", 9.26R)
        Dim f As New Fast_SB_Discount With {.is_By_Draft = draft}
        Invoke(f, "Fast_SB_Discount_Load", {f, EventArgs.Empty})
        Return f
    End Function
    <STAThread>
    Public Function Main() As Integer
        Try
            Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US")
            Check(GetType(Fast_SB_Discount).GetMethod("UpdateGB_FormClosed", BindingFlags.NonPublic Or BindingFlags.Instance) Is Nothing, "no save-on-close handler")
            For Each mode In {"exit", "escape", "close", "closed"}
                Using f = NewDialog(False)
                    Field(f, "Discount_txt").Text = "5"
                    Select Case mode
                        Case "exit" : Invoke(f, "ExitButton_Click", {f, EventArgs.Empty})
                        Case "escape" : Invoke(f, "Fast_SB_Discount_KeyDown", {f, New KeyEventArgs(Keys.Escape)})
                        Case "close" : f.Close()
                        Case "closed" : GetType(Form).GetMethod("OnFormClosed", BindingFlags.NonPublic Or BindingFlags.Instance).Invoke(f, {New FormClosedEventArgs(CloseReason.UserClosing)})
                    End Select
                    Check(Not f.is_OK, "cancel not confirmed " & mode)
                    Check(GlobalValue("Disc") = 1R AndAlso GlobalValue("Pure") = 9.26R AndAlso GlobalValue("T_ID") = 50, "cancel globals unchanged " & mode)
                End Using
            Next
            Using f = NewDialog(True)
                Field(f, "Discount_txt").Text = "0"
                Invoke(f, "UpdateGBButton_Click", {f, EventArgs.Empty})
                Check(f.is_OK AndAlso f.DialogResult = DialogResult.OK, "draft confirms")
                Check(GlobalValue("Disc") = 0R AndAlso GlobalValue("Pure") = 10.26R, "zero discount applies")
                Invoke(f, "UpdateGBButton_Click", {f, EventArgs.Empty})
                Check(GlobalValue("Disc") = 0R, "duplicate confirm ignored")
            End Using
            Using f = NewDialog(True)
                Field(f, "Percent_txt").Text = "20"
                Check(Math.Abs(Convert.ToDouble(Field(f, "Discount_txt").Text) - 2.052R) < 0.000001R, "percentage uses captured total")
                Invoke(f, "UpdateGBButton_Click", {f, EventArgs.Empty})
                Check(f.is_OK AndAlso Math.Abs(GlobalValue("Disc") - 2.052R) < 0.000001R, "draft percentage")
            End Using
            Using f = NewDialog(True)
                DirectCast(Field(f, "CheckBox3"), CheckBox).Checked = True
                Invoke(f, "UpdateGBButton_Click", {f, EventArgs.Empty})
                Check(f.is_OK AndAlso Math.Abs(GlobalValue("Disc") - 0.26R) < 0.000001R, "rounding option")
            End Using
            Console.WriteLine("PASS " & checks & " isolated discount dialog checks; no SQL path executed.")
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function
End Module
