Imports System.Data

Partial Public Class FrmIncomeStatementAccountLinkEdit

    Public Property AccountCode As String = ""
    Public Property AccountName As String = ""

    Public Property IncludeChildren As Boolean = True
    Public Property AccountSignMode As Integer = 1

    Private Sub FrmIncomeStatementAccountLinkEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitAccountSignModeCombo()

        lblAccountInfo.Text =
            "الحساب: " & AccountCode & Environment.NewLine &
            AccountName

        chkIncludeChildren.Checked = IncludeChildren
        cboAccountSignMode.SelectedValue = AccountSignMode
    End Sub

    Private Sub InitAccountSignModeCombo()
        Dim dt As New DataTable()
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add(1, "حسب طبيعة البند")
        dt.Rows.Add(2, "عكس")
        dt.Rows.Add(3, "موجب دائمًا")
        dt.Rows.Add(4, "سالب دائمًا")
        dt.Rows.Add(5, "حسب الرصيد كما هو")

        cboAccountSignMode.DataSource = dt
        cboAccountSignMode.DisplayMember = "Name"
        cboAccountSignMode.ValueMember = "ID"
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        IncludeChildren = chkIncludeChildren.Checked
        AccountSignMode = CInt(cboAccountSignMode.SelectedValue)

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class