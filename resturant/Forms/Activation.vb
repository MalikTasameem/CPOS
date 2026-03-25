Imports System.Management
Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Globalization

Public Class Activation
    Dim c1 As New C

    Public isFor_Check_Sys_Active As Boolean = False
    Public isActive As Boolean = False

    Dim Date_Part1 As String = ""
    Dim Date_Part2 As String = ""
    Dim Mac_Part3 As String = ""
    Dim Mac_Part4 As String = ""
    Dim Mac_Part5 As String = ""
    Dim Mac_Part6 As String = ""
    ' Dim Mac_Part7 As String = ""

    Dim TmpNum_Part7 As String = ""
    Dim TmpNum_Part8 As String = ""


    Dim SerialNum1 As String = ""
    Dim SerialNum2 As String = ""

    Dim ActiveCode As New TextBox

    Private Sub Activation_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        '  Me.Dispose()
    End Sub

    Private Sub Activation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Get_ProductCode()
    End Sub

    Private Sub Get_ProductCode()
        Date_Part1 = ""
        Date_Part2 = ""
        Mac_Part3 = ""
        Mac_Part4 = ""
        Mac_Part5 = ""
        Mac_Part6 = ""
        TmpNum_Part7 = ""
        TmpNum_Part8 = ""

        Dim C As New C
        'C.Str = " Select TOP 1 ISNULL(ProductCode,'') AS ProductCode  , ISNULL(ActiveCode,'') AS ActiveCode , ISNULL(SerialNum,'') AS SerialNum  From Activation_Details "
        C.Str = "Select ISNULL(ProductCode,'') AS ProductCode  , ISNULL(ActiveCode,'') AS ActiveCode , ISNULL(SerialNum,'') AS SerialNum  From Activation_Details  WHERE CP_NAME = '" & My.Computer.Name & "'"
        C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
        C.Con.Open()

        C.Dr = C.Com.ExecuteReader

        If C.Dr.HasRows = True Then
            C.Dr.Read()
            ProductCodeTextBox.Text = C.Dr("ProductCode")
            Parsing_Product_To_Pieces()
            SerialNumTextBox.Text = C.Dr("SerialNum")
            InsertedActivationCodeTextBox.Text = C.Dr("ActiveCode")
            If isFor_Check_Sys_Active = True Then Test_Code()

        End If
        C.Con.Close()
    End Sub

    Public Sub Parsing_Product_To_Pieces()

        For i = 0 To ProductCodeTextBox.Text.Count - 1

            Select Case i

                Case 0, 1, 2, 3
                    Date_Part1 = Date_Part1 + ProductCodeTextBox.Text(i)
                Case 5, 6, 7, 8
                    Date_Part2 = Date_Part2 + ProductCodeTextBox.Text(i)

                Case 10, 11, 12, 13
                    Mac_Part3 = Mac_Part3 + ProductCodeTextBox.Text(i)

                Case 15, 16, 17, 18
                    Mac_Part4 = Mac_Part4 + ProductCodeTextBox.Text(i)

                Case 20, 21, 22, 23
                    Mac_Part5 = Mac_Part5 + ProductCodeTextBox.Text(i)

                Case 25, 26, 27, 28
                    Mac_Part6 = Mac_Part6 + ProductCodeTextBox.Text(i)

                Case 30, 31, 32, 33
                    TmpNum_Part7 = TmpNum_Part7 + ProductCodeTextBox.Text(i)

                Case 35, 36, 37, 38
                    TmpNum_Part8 = TmpNum_Part8 + ProductCodeTextBox.Text(i)

            End Select

        Next

    End Sub

    Public Sub Parsing_Code(TextBox1 As TextBox)
        Dim i As Integer
        Dim length As Integer
        Dim value As String

        length = TextBox1.Text.Length
        value = TextBox1.Text

        For i = 0 To TextBox1.Text.Length Step 5
            If i = 0 Then
                value = value.Insert(i + 4, "-")
            Else
                value = value.Insert(i + 4, "-")
            End If
        Next
        'length - 4
        TextBox1.Text = value
    End Sub

    Public Sub Encryption_Generate_ActiveCode()
        SerialNum1 = ""
        SerialNum2 = ""

        For i = 0 To SerialNumTextBox.Text.Count - 1
            Select Case i
                Case 0, 1, 2, 3
                    SerialNum1 = SerialNum1 + SerialNumTextBox.Text(i)
                Case Else
                    SerialNum2 = SerialNum2 + SerialNumTextBox.Text(i)
            End Select
        Next

        ActiveCode.Text = SerialNum1 + Date_Part1 + Mac_Part3 + Mac_Part5 + Mac_Part4 + Mac_Part6 + Date_Part2 + SerialNum2
        '---------------------------------------------
        Dim Data As String = ActiveCode.Text
        For i = 0 To Data.Count - 1
            If Data(i) = "9" Then
                Data = Data.Remove(i, 1).Insert(i, "8")
            End If

            If Data(i) = "Z" Then
                Data = Data.Remove(i, 1).Insert(i, "Y")
            End If
        Next
        ActiveCode.Text = Data
        '---------------------------------------------
        ActiveCode.Text = Encryption_Data_Shifting(ActiveCode.Text)
        Parsing_Code(ActiveCode)

    End Sub

    Public Function Encryption_Data_Shifting(Data As String) As String

        Dim asciis As Byte() = System.Text.Encoding.ASCII.GetBytes(Data)
        For i As Int32 = 0 To asciis.Length - 1
            asciis(i) = CByte(asciis(i) + 1)
        Next
        Dim result As String = System.Text.Encoding.ASCII.GetString(asciis)

        Return result
    End Function

    Public Function Decryption_Data_Shifting(Data As String) As String
        Dim asciis As Byte() = System.Text.Encoding.ASCII.GetBytes(Data)
        For i As Int32 = 0 To asciis.Length - 1
            asciis(i) = CByte(asciis(i) - 1)
        Next
        Dim result As String = System.Text.Encoding.ASCII.GetString(asciis)
        Return result
    End Function

    Private Sub ConfirmActivButton_Click(sender As Object, e As EventArgs) Handles ConfirmActivButton.Click
        Test_Code()
    End Sub

    Private Sub Test_Code()
        ActiveCode.Clear()
        Encryption_Generate_ActiveCode()
        If isFor_Check_Sys_Active = False Then
            If Check_Serial_Number() = 1 Then
                If ActiveCode.Text = InsertedActivationCodeTextBox.Text Then
                    isActive = True
                    MsgBox("تم تفعيل النسخة ... شكرا لإستخدامكم منتجات كلاس ... سيتم إعادة تشيل النظام", MsgBoxStyle.Information, "تفعيل النظام")
                    Activation_Details_Update()
                    Application.Restart()
                Else
                    MsgBox("تــأكد من صحة رمز التفعيل", MsgBoxStyle.Critical, "1 خطا فالتعفيل")
                End If
            Else
                MsgBox("تأكد من صحة الرقم السري ", MsgBoxStyle.Critical, "2 خطأ فالتفعيل")
            End If

        Else

            If (Mac_Part3 + Mac_Part4 + Mac_Part5 + Mac_Part6).ToString = My_Settings.Cpu_ID Then
                If Check_Serial_Number() = 1 Then
                    If ActiveCode.Text = InsertedActivationCodeTextBox.Text Then
                        isActive = True
                    Else
                        isActive = False
                    End If

                Else
                    isActive = False
                End If
                Me.Close()
            Else
                MsgBox("تأكد من رقم المنتج للنظام", MsgBoxStyle.Critical, "2 خطأ فالتفعيل")
                isActive = False
                Me.Close()
            End If

        End If


    End Sub

    Private Sub Activation_Details_Update()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "Activation_Details_Update"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@CP_NAME", My.Computer.Name)
            sqlComm.Parameters.AddWithValue("@ProductCode", ProductCodeTextBox.Text)
            sqlComm.Parameters.AddWithValue("@SerialNum", SerialNumTextBox.Text)
            sqlComm.Parameters.AddWithValue("@ActiveCode", ActiveCode.Text)

            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub


    Public Function Check_Serial_Number()

        TmpNum_Part7 = ""
        TmpNum_Part8 = ""
        SerialNum1 = ""
        SerialNum2 = ""

        For i = 0 To InsertedActivationCodeTextBox.Text.Count - 1

            Select Case i
                Case 0, 1, 2, 3
                    TmpNum_Part7 = TmpNum_Part7 + InsertedActivationCodeTextBox.Text(i)

                Case 35, 36, 37, 38
                    TmpNum_Part8 = TmpNum_Part8 + InsertedActivationCodeTextBox.Text(i)

            End Select
        Next

        TmpNum_Part7 = Decryption_Data_Shifting(TmpNum_Part7)
        TmpNum_Part8 = Decryption_Data_Shifting(TmpNum_Part8)

        For i = 0 To SerialNumTextBox.Text.Count - 1
            Select Case i
                Case 0, 1, 2, 3
                    SerialNum1 = SerialNum1 + SerialNumTextBox.Text(i)

                Case Else
                    SerialNum2 = SerialNum2 + SerialNumTextBox.Text(i)
            End Select
        Next

        '---------------------------------------------
        For i = 0 To SerialNum1.Count - 1
            If SerialNum1(i) = "9" Then
                SerialNum1 = SerialNum1.Remove(i, 1).Insert(i, "8")
            End If
        Next

        For i = 0 To SerialNum2.Count - 1
            If SerialNum2(i) = "9" Then
                SerialNum2 = SerialNum2.Remove(i, 1).Insert(i, "8")
            End If
        Next
        '---------------------------------------------

        If (SerialNum1 = TmpNum_Part7) And (SerialNum2 = TmpNum_Part8) Then
            Return 1
        Else
            Return 0
        End If


    End Function

    Private Sub InsertedActivationCodeTextBox_Enter(sender As Object, e As EventArgs) Handles InsertedActivationCodeTextBox.Enter
        Set_Eng_Language()
    End Sub

    Private Sub ActivationCodeTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles InsertedActivationCodeTextBox.KeyPress
        If e.KeyChar = " " Then
            e.Handled = True
        End If

        'Test only
        If Char.IsLetterOrDigit(e.KeyChar) Then
            Dim test As String = ""
            Select Case e.KeyChar
                Case "0" To "9"
                    ' Do nothing
                Case "a" To "z"
                    e.KeyChar = Char.ToUpper(e.KeyChar)
                Case Else
                    e.Handled = True
            End Select
        ElseIf Not e.KeyChar = Convert.ToChar(8) Then

            e.KeyChar = ""
        End If

        Dim strKeyTextField As String = InsertedActivationCodeTextBox.Text
        Dim n As Integer = 4
        Dim intlength As Integer = InsertedActivationCodeTextBox.TextLength

        If Not e.KeyChar = Convert.ToChar(8) Then

            While intlength > 3 And intlength < InsertedActivationCodeTextBox.MaxLength

                If InsertedActivationCodeTextBox.Text.Length = 4 Then
                    strKeyTextField = strKeyTextField.Insert(4, "-")
                End If

                Dim singleChar As Char
                singleChar = strKeyTextField.Chars(n)

                While (n + 4) < intlength

                    If singleChar = "-" Then
                        n = n + 5

                        If n = intlength Then
                            strKeyTextField = strKeyTextField.Insert(n, "-")
                        End If
                    End If

                End While

                intlength = intlength - 4
            End While
            InsertedActivationCodeTextBox.Text = strKeyTextField

            'sets focus at the end of the string
            InsertedActivationCodeTextBox.Select(InsertedActivationCodeTextBox.Text.Length, 0)

        Else
            InsertedActivationCodeTextBox.Text = strKeyTextField
            InsertedActivationCodeTextBox.Select(InsertedActivationCodeTextBox.Text.Length, 0)

        End If
    End Sub

    Private Sub KayBoardButton_Click(sender As Object, e As EventArgs) Handles KayBoardButton.Click
        Call_KeyBoard()
    End Sub

    Private Sub ActivationCodeTextBox_TextChanged(sender As Object, e As EventArgs) Handles InsertedActivationCodeTextBox.TextChanged
        If String.IsNullOrWhiteSpace(sender.text) = False Then
            ConfirmActivButton.Enabled = True
        Else
            ConfirmActivButton.Enabled = False
        End If
    End Sub

    Private Sub SerialNumTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SerialNumTextBox.KeyPress
        Check_Only_Int(sender, e)
    End Sub

End Class