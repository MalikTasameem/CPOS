Imports System.Windows.Forms

Public Class ReceiptPrintData
    Public Property StoreTitle As String = ""
    Public Property StoreSubTitle As String = ""
    Public Property DocumentTitle As String = ""
    Public Property ReceiptNumber As String = ""
    Public Property ReceiptDate As DateTime = DateTime.Now
    Public Property PartyCaption As String = ""
    Public Property PartyName As String = ""
    Public Property StatementText As String = ""
    Public Property AmountText As String = ""
    Public Property AmountInWords As String = ""
    Public Property PaymentMethod As String = ""
    Public Property PaymentDetails As String = ""
    Public Property TreasuryName As String = ""
    Public Property CurrencyName As String = "دينار ليبي"
    Public Property AccountBalanceText As String = ""
    Public Property UserName As String = ""
    Public Property ShowAccountBalance As Boolean = True
    Public Property ShowTreasury As Boolean = True

    Public Shared Function FromReceiptForm(form As Receipt, showTreasury As Boolean) As ReceiptPrintData
        Dim data As New ReceiptPrintData()

        data.StoreTitle = SBill_Title_1
        data.StoreSubTitle = SBill_Title_2
        data.DocumentTitle = NormalizeReceiptTitle(SafeControlText(form.ReceiptTypeComboBox))
        data.ReceiptNumber = SafeControlText(form.ReceiptNum_Txt)
        data.ReceiptDate = form.DateTimeReceipt.Value
        data.PartyCaption = GetPartyCaption(form)
        data.PartyName = GetPartyName(form)
        data.StatementText = BuildStatementText(form)
        data.AmountText = FormatReceiptAmount(SafeControlText(form.money_num_txtb))
        data.AmountInWords = SafeControlText(form.money_char_txtb)
        data.PaymentMethod = SafeControlText(form.payment_Type_combo)
        data.PaymentDetails = GetPaymentDetails(form)
        data.TreasuryName = SafeControlText(form.Treasury_ComboBox)
        data.AccountBalanceText = SafeControlText(form.Current_QTY)
        data.UserName = USER_NAME
        data.ShowAccountBalance = form.AG_Show_Balance_CB.Checked
        data.ShowTreasury = showTreasury

        Return data
    End Function

    Private Shared Function SafeControlText(ctrl As Control) As String
        If ctrl Is Nothing OrElse ctrl.Text Is Nothing Then Return ""
        Return ctrl.Text.Trim()
    End Function

    Private Shared Function NormalizeReceiptTitle(typeText As String) As String
        If String.IsNullOrWhiteSpace(typeText) Then Return "سند"
        If typeText.Contains("قبض") Then Return "سند قبض"
        If typeText.Contains("صرف") Then Return "سند صرف"
        If typeText.Contains("إيصال") Then Return typeText.Replace("إيصال", "سند").Trim()
        Return typeText
    End Function

    Private Shared Function GetPartyCaption(form As Receipt) As String
        Dim typeId As Integer = 0
        If form IsNot Nothing AndAlso form.ReceiptTypeComboBox IsNot Nothing AndAlso form.ReceiptTypeComboBox.SelectedValue IsNot Nothing Then
            Integer.TryParse(form.ReceiptTypeComboBox.SelectedValue.ToString(), typeId)
        End If

        If typeId = 2 OrElse typeId = 4 OrElse typeId = 5 Then Return "سلمت للسيد"
        If typeId = 3 OrElse typeId = 9 Then Return "استلمت من السيد"

        Return "استلمت من السيد"
    End Function

    Private Shared Function GetPartyName(form As Receipt) As String
        If form Is Nothing Then Return ""

        Dim partyName As String = If(form.AG_Cm Is Nothing, "", form.AG_Cm.Textt)
        Dim phone As String = SafeControlText(form.CR_Phone_Txt)

        If String.IsNullOrWhiteSpace(phone) Then Return partyName
        Return partyName & " \ " & phone
    End Function

    Private Shared Function BuildStatementText(form As Receipt) As String
        If form Is Nothing Then Return ""

        Dim notes As String = SafeControlText(form.Notes_txtb)
        Dim title As String = SafeControlText(form.Receipt_Title_combobox)

        If String.IsNullOrWhiteSpace(notes) AndAlso String.IsNullOrWhiteSpace(title) Then Return ""
        If String.IsNullOrWhiteSpace(notes) Then Return title
        If String.IsNullOrWhiteSpace(title) Then Return notes

        Return notes & "  * " & title & " * "
    End Function

    Private Shared Function FormatReceiptAmount(valueText As String) As String
        Dim amount As Decimal = 0D
        If Decimal.TryParse(valueText, amount) = False Then Return valueText

        Dim numberFormat As String = If(String.IsNullOrWhiteSpace(N_Point_Fter), "N3", N_Point_Fter)
        Try
            Return amount.ToString(numberFormat)
        Catch
            Return amount.ToString("N3")
        End Try
    End Function

    Private Shared Function GetPaymentDetails(form As Receipt) As String
        If form Is Nothing OrElse form.payment_Type_combo Is Nothing Then Return ""
        If form.payment_Type_combo.SelectedIndex = 0 Then Return "--------------------------------"

        Dim checkNumber As String = SafeControlText(form.CheckNum_txtb)
        Dim bankName As String = SafeControlText(form.bankName_Combo)

        Return "شيك رقم " & checkNumber & " * عن مصرف " & bankName & " * "
    End Function
End Class
