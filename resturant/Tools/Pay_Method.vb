Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D

Public Class Pay_Method

    Public TR_ID As Integer
    Public Pay_ID As Integer

    Public Event PaymentTypeChanged As EventHandler
    'Public PaymentMethodsCount As Integer = 0

    Public ReadOnly Property PaymentMethodsCount As Integer
        Get
            Return pnlPayments.Controls.Count
        End Get
    End Property

    Private SelectedButton As Button = Nothing

    Private Sub Pay_Method_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TR_ID = 1
        Pay_ID = 1
    End Sub

    '------------------------------------------------------------------------------------------------------------------
    ' إعادة تعريف خاصية Font لتطبيقها على كل الأدوات الداخلية
    '------------------------------------------------------------------------------------------------------------------
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(value As Font)
            MyBase.Font = value
            ApplyFontToAllControls(Me, value)
        End Set
    End Property

    Private Sub ApplyFontToAllControls(parent As Control, font As Font)
        For Each ctrl As Control In parent.Controls
            ctrl.Font = font
            If ctrl.HasChildren Then
                ApplyFontToAllControls(ctrl, font)
            End If
        Next
    End Sub

    '------------------------------------------------------------------------------------------------------------------
    ' هذه الدالة تبقى كما كانت عندك
    '------------------------------------------------------------------------------------------------------------------
    Public Sub Set_Tr_Form()
        Select Case FormType
            Case 1
                TR_ID = SB_TR_ID

            Case 2
                TR_ID = PCH_TR_ID

            Case 8
                TR_ID = EXP_TR_ID
                PaymentMethodDefaultAccounts_SELECT()
        End Select
    End Sub

    '------------------------------------------------------------------------------------------------------------------
    ' هذه الدالة تبقى أساسية: تحميل الخزائن وطرق الدفع
    '------------------------------------------------------------------------------------------------------------------
    Public Sub Load_Tr()

        Dim C = New C

        Try
            Dim sql As String = "Select Distinct Tr_ID,Tr_Name from TreasuryCard"
            C.Da = New SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)

            Treasury_ComboBox.DataSource = C.Dt
            Treasury_ComboBox.DisplayMember = "Tr_Name"
            Treasury_ComboBox.ValueMember = "Tr_ID"

            If TR_ID > 0 Then
                Treasury_ComboBox.SelectedValue = TR_ID
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C

        Try
            Dim sql As String = "select PaymentMethodID as P_ID , PAYMENT_NAME 
                                 from PaymentMethodDefaultAccounts
                                 join PAYMENT_METHOD ON P_ID = PaymentMethodID WHERE IsActive = 1"

            C.Da = New SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)

            If C.Dt.Rows.Count > 0 Then
                BuildPaymentButtons(C.Dt)
                SelectPaymentButtonById(Pay_ID)
            Else
                Dim dt As New DataTable
                dt.Columns.Add("P_ID", GetType(Integer))
                dt.Columns.Add("PAYMENT_NAME", GetType(String))
                dt.Rows.Add(1, "نقدا")

                BuildPaymentButtons(dt)
                SelectPaymentButtonById(1)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------------------
    ' بناء أزرار طرق الدفع
    '------------------------------------------------------------------------------------------------------------------
    Private Sub BuildPaymentButtons(dt As DataTable)

        pnlPayments.Controls.Clear()
        SelectedButton = Nothing

        For Each r As DataRow In dt.Rows

            Dim btn As New Button()

            btn.Tag = Convert.ToInt32(r("P_ID"))
            btn.Text = r("PAYMENT_NAME").ToString()

            btn.Width = 120
            btn.Height = 50
            btn.Margin = New Padding(6)
            btn.Cursor = Cursors.Hand
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 1
            btn.FlatAppearance.BorderColor = Color.FromArgb(210, 210, 210)
            btn.BackColor = Color.White
            btn.ForeColor = Color.FromArgb(45, 45, 45)
            btn.Font = New Font(Me.Font.FontFamily, 9.5!, FontStyle.Bold)

            AddHandler btn.Click, AddressOf PaymentButton_Click
            AddHandler btn.MouseEnter, AddressOf PaymentButton_MouseEnter
            AddHandler btn.MouseLeave, AddressOf PaymentButton_MouseLeave

            pnlPayments.Controls.Add(btn)

        Next

    End Sub

    '------------------------------------------------------------------------------------------------------------------
    ' عند الضغط على زر طريقة الدفع
    '------------------------------------------------------------------------------------------------------------------
    Private Sub PaymentButton_Click(sender As Object, e As EventArgs)

        Dim btn As Button = CType(sender, Button)

        Pay_ID = CInt(btn.Tag)

        HighlightSelectedButton(btn)

        PaymentMethodDefaultAccounts_SELECT()

        RaiseEvent PaymentTypeChanged(Me, EventArgs.Empty)

    End Sub

    Private Sub HighlightSelectedButton(selectedBtn As Button)

        For Each ctrl As Control In pnlPayments.Controls
            If TypeOf ctrl Is Button Then
                Dim b As Button = CType(ctrl, Button)
                b.BackColor = Color.White
                b.ForeColor = Color.FromArgb(45, 45, 45)
                b.FlatAppearance.BorderColor = Color.FromArgb(210, 210, 210)
            End If
        Next

        selectedBtn.BackColor = Color.FromArgb(0, 120, 215)
        selectedBtn.ForeColor = Color.White
        selectedBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215)

        SelectedButton = selectedBtn

    End Sub

    Private Sub SelectPaymentButtonById(paymentId As Integer)

        For Each ctrl As Control In pnlPayments.Controls
            If TypeOf ctrl Is Button Then
                Dim btn As Button = CType(ctrl, Button)
                If CInt(btn.Tag) = paymentId Then
                    Pay_ID = paymentId
                    HighlightSelectedButton(btn)
                    Exit For
                End If
            End If
        Next

    End Sub

    Private Sub PaymentButton_MouseEnter(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        If btn IsNot SelectedButton Then
            btn.BackColor = Color.FromArgb(245, 247, 250)
            btn.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 180)
        End If
    End Sub

    Private Sub PaymentButton_MouseLeave(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        If btn IsNot SelectedButton Then
            btn.BackColor = Color.White
            btn.ForeColor = Color.FromArgb(45, 45, 45)
            btn.FlatAppearance.BorderColor = Color.FromArgb(210, 210, 210)
        End If
    End Sub

    '------------------------------------------------------------------------------------------------------------------
    ' نفس دالتك الأساسية مع تحسين بسيط
    '------------------------------------------------------------------------------------------------------------------
    Public Sub PaymentMethodDefaultAccounts_SELECT()

        Dim c As New C
        Dim Tmp_Tr_ID As Integer = TR_ID

        Try
            Dim s As String = "select TOP 1 AccountID 
                               FROM PaymentMethodDefaultAccounts 
                               WHERE PaymentMethodID = @PaymentMethodID"

            c.Com = New SqlCommand(s, c.Con)
            c.Com.Parameters.AddWithValue("@PaymentMethodID", Pay_ID)

            c.Con.Open()
            c.Dr = c.Com.ExecuteReader()

            If c.Dr.HasRows Then
                c.Dr.Read()

                Treasury_ComboBox.SelectedValue = c.Dr("AccountID")
                TR_ID = CInt(Treasury_ComboBox.SelectedValue)
            Else
                Treasury_ComboBox.SelectedValue = Tmp_Tr_ID
                TR_ID = Tmp_Tr_ID
            End If

            c.Dr.Close()
            c.Con.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


End Class



















'Public Class Pay_Method

'    Public TR_ID, Pay_ID As Integer
'    Private Sub Pay_Method_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        TR_ID = 1
'        Pay_ID = 1
'        pnlPayTypeDrop.Visible = True
'    End Sub

'    Public Event PaymentTypeChanged As EventHandler


'    '-------------------------------------------------------------------
'    ' إعادة تعريف خاصية Font لتطبيقها على كل الأدوات الداخلية
'    '-------------------------------------------------------------------
'    Public Overrides Property Font As Font
'        Get
'            Return MyBase.Font
'        End Get
'        Set(value As Font)
'            MyBase.Font = value
'            ApplyFontToAllControls(Me, value)

'        End Set
'    End Property

'    '-------------------------------------------------------------------
'    ' دالة مساعدة تطبق الخط على كل العناصر الداخلية
'    '-------------------------------------------------------------------
'    Private Sub ApplyFontToAllControls(parent As Control, font As Font)
'        For Each ctrl As Control In parent.Controls
'            ctrl.Font = font
'            ' في حال وجود أدوات داخل أدوات أخرى
'            If ctrl.HasChildren Then
'                ApplyFontToAllControls(ctrl, font)
'            End If
'        Next
'    End Sub

'    Public Sub Set_Tr_Form()
'        Select Case FormType
'            Case 1 : TR_ID = SB_TR_ID
'            Case 2 : TR_ID = PCH_TR_ID
'            Case 8 : TR_ID = EXP_TR_ID
'                PaymentMethodDefaultAccounts_SELECT()
'        End Select
'    End Sub


'    Public Sub Load_Tr()

'        Dim C = New C
'        Try
'            Dim sql As String = "Select Distinct Tr_ID,Tr_Name from TreasuryCard "
'            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
'            C.Da.Fill(C.Dt)
'            Treasury_ComboBox.DataSource = C.Dt
'            Treasury_ComboBox.DisplayMember = "Tr_Name"
'            Treasury_ComboBox.ValueMember = "Tr_ID"
'            Treasury_ComboBox.SelectedValue = TR_ID
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try


'        C = New C
'        Try
'            Dim sql As String = " select PaymentMethodID as P_ID , PAYMENT_NAME from PaymentMethodDefaultAccounts
'                                join PAYMENT_METHOD ON P_ID = PaymentMethodID "
'            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
'            C.Da.Fill(C.Dt)
'            If C.Dt.Rows.Count > 0 Then
'                lbPayTypes.DataSource = C.Dt
'                lbPayTypes.DisplayMember = "PAYMENT_NAME"
'                lbPayTypes.ValueMember = "P_ID"
'                lbPayTypes.SelectedValue = Pay_ID
'            Else
'                sql = "Select '1' AS P_ID,'نقدا' AS PAYMENT_NAME "
'                C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
'                C.Da.Fill(C.Dt)
'                lbPayTypes.DataSource = C.Dt
'                lbPayTypes.DisplayMember = "PAYMENT_NAME"
'                lbPayTypes.ValueMember = "P_ID"
'                lbPayTypes.SelectedValue = Pay_ID
'            End If

'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try


'    End Sub

'    Private Sub payment_Type_combo_SelectedValueChanged(sender As Object, e As EventArgs) Handles lbPayTypes.SelectedValueChanged
'        If TypeName(lbPayTypes.SelectedValue) = "Integer" Then
'            PaymentMethodDefaultAccounts_SELECT()
'        End If
'    End Sub


'    Public Sub PaymentMethodDefaultAccounts_SELECT() 'Pay_ID As Integer, ByRef TR_ID As Integer
'        Dim c As New C
'        Dim Tmp_Tr_ID As Integer = lbPayTypes.SelectedValue
'        Pay_ID = lbPayTypes.SelectedValue

'        Try
'            Dim s As String
'            s = "select TOP 1 AccountID FROM PaymentMethodDefaultAccounts WHERE PaymentMethodID = '" & lbPayTypes.SelectedValue & "'"
'            c.Com = New SqlClient.SqlCommand(s, c.Con)
'            c.Con.Open()
'            c.Dr = c.Com.ExecuteReader
'            If c.Dr.HasRows Then
'                c.Dr.Read()
'                Treasury_ComboBox.SelectedValue = c.Dr("AccountID")
'                TR_ID = Treasury_ComboBox.SelectedValue

'            Else
'                lbPayTypes.SelectedValue = Tmp_Tr_ID
'                Treasury_ComboBox.SelectedValue = TR_ID
'            End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try

'    End Sub


'    ' كلاس بسيط ليحمل Text + Id
'    Private Class PayItem
'        Public Property Text As String
'        Public Property Id As Integer
'        Public Sub New(t As String, i As Integer)
'            Text = t : Id = i
'        End Sub
'        Public Overrides Function ToString() As String
'            Return Text
'        End Function
'    End Class

'    Public ReadOnly Property SelectedPaymentTypeId As Integer
'        Get
'            Dim it = TryCast(lbPayTypes.SelectedItem, PayItem)
'            If it Is Nothing Then Return 0
'            Return it.Id
'        End Get
'    End Property

'    Public ReadOnly Property SelectedPaymentTypeText As String
'        Get
'            Dim it = TryCast(lbPayTypes.SelectedItem, PayItem)
'            If it Is Nothing Then Return ""
'            Return it.Text
'        End Get
'    End Property


'    Private Sub lbPayTypes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbPayTypes.SelectedIndexChanged
'        If lbPayTypes.SelectedItem Is Nothing Then Exit Sub

'        lblPayTypeText.Text = SelectedPaymentTypeText
'        RaiseEvent PaymentTypeChanged(Me, EventArgs.Empty)
'    End Sub


'    ' تعبئة من DataTable (اختياري)
'    Public Sub BindPaymentTypes(dt As DataTable, displayMember As String, valueMember As String)
'        lbPayTypes.Items.Clear()
'        For Each r As DataRow In dt.Rows
'            lbPayTypes.Items.Add(New PayItem(r(displayMember).ToString(), Convert.ToInt32(r(valueMember))))
'        Next
'    End Sub

'End Class

