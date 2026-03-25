Imports System.Data.SqlClient

Public Class frmPaymentDefaultAccount

    Private ConStr As String = MY_Settings.SqlConStr
    Private CurrentID As Integer = 0

    Private Sub frmPaymentDefaultAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPAYMENT_METHOD()
        LoadAccounts()
        LoadLinksGrid()
        ' fill_forms()
    End Sub


    '------------------------------------------
    Private Sub LoadPAYMENT_METHOD()
        Dim dt As New DataTable()
        Using con As New SqlConnection(ConStr)
            Dim da As New SqlDataAdapter("SELECT [P_ID],[PAYMENT_NAME] FROM [PAYMENT_METHOD]", con)
            da.Fill(dt)
        End Using
        cmbPaymentMethod.DataSource = dt
        cmbPaymentMethod.DisplayMember = "PAYMENT_NAME"
        cmbPaymentMethod.ValueMember = "P_ID"
        cmbPaymentMethod.SelectedIndex = -1
    End Sub

    '------------------------------------------
    Private Sub LoadAccounts()
        Dim dt As New DataTable()
        Using con As New SqlConnection(ConStr)
            Dim da As New SqlDataAdapter("SELECT [Tr_ID],[Tr_Name],[Tr_AccountNumber] FROM [TreasuryCard]", con)
            da.Fill(dt)
        End Using

        cmbAccount.DataSource = dt
        cmbAccount.DisplayMember = "Tr_Name"
        cmbAccount.ValueMember = "Tr_ID"
        cmbAccount.SelectedIndex = -1
    End Sub

    '------------------------------------------
    Private Sub LoadLinksGrid()
        Dim sql As String = "
            SELECT 
                p.ID,
                m.PAYMENT_NAME AS [طريقة الدفع],
                a.Tr_Name AS [الحساب الافتراضي],                            
                p.Percent_Disc as [عمولة الخصم],
                isnull(p.is_Lock,0) as [منع تبديل الخزينة فالإيصالات],
                isnull(p.IsActive,0) as [نشط],
                p.Notes AS [ملاحظات]
            FROM PaymentMethodDefaultAccounts p
            INNER JOIN PAYMENT_METHOD m ON p.PaymentMethodID = m.P_ID
            INNER JOIN TreasuryCard a ON p.AccountID = a.Tr_ID
            --WHERE p.IsActive = 1 "

        Using con As New SqlConnection(ConStr)
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(sql, con)
            da.Fill(dt)
            dgvLinks.DataSource = dt

            dgvLinks.Columns("ID").Visible = False
        End Using
    End Sub

    '------------------------------------------

    Private Function PaymentMethodExists(methodId As Integer, Optional excludeId As Integer = 0) As Boolean
        Dim sql As String = "
        SELECT COUNT(*) 
        FROM PaymentMethodDefaultAccounts 
        WHERE PaymentMethodID = @m AND IsActive = 1 AND ID <> @id
    "
        Using con As New SqlConnection(ConStr)
            con.Open()
            Dim cmd As New SqlCommand(sql, con)
            cmd.Parameters.AddWithValue("@m", methodId)
            cmd.Parameters.AddWithValue("@id", excludeId)

            Dim count As Integer = CInt(cmd.ExecuteScalar())
            Return (count > 0)
        End Using
    End Function


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If cmbPaymentMethod.SelectedIndex = -1 Or cmbAccount.SelectedIndex = -1 Then
            MessageBox.Show("يرجى اختيار طريقة الدفع والحساب")
            Exit Sub
        End If

        Dim methodId = CInt(cmbPaymentMethod.SelectedValue)
        Dim accountId = CInt(cmbAccount.SelectedValue)
        Dim notes = txtNotes.Text.Trim()


        '==================== منع التكرار ====================
        If PaymentMethodExists(methodId, CurrentID) Then
            MessageBox.Show("هذه الطريقة مرتبطة مسبقًا بحساب افتراضي، لا يمكن تكرارها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        '=====================================================


        If String.IsNullOrWhiteSpace(Percent_Disc_txt.Text) Then Percent_Disc_txt.Text = "0"

        Using con As New SqlConnection(ConStr)
            con.Open()

            If CurrentID = 0 Then
                Dim cmd As New SqlCommand("
                    INSERT INTO PaymentMethodDefaultAccounts(ID,PaymentMethodID, AccountID, Notes, IsActive,Percent_Disc,is_Lock)
                    VALUES(@m,@m, @a, @n, 1,@Percent_Disc,@is_Lock)
                ", con)
                cmd.Parameters.AddWithValue("@m", cmbPaymentMethod.SelectedValue)
                cmd.Parameters.AddWithValue("@a", accountId)
                cmd.Parameters.AddWithValue("@n", notes)
                cmd.Parameters.AddWithValue("@Percent_Disc", Percent_Disc_txt.Text)
                cmd.Parameters.AddWithValue("@is_Lock", isLockCB.Checked)
                cmd.ExecuteNonQuery()
            Else
                Dim cmd As New SqlCommand("
                    UPDATE PaymentMethodDefaultAccounts
                    SET PaymentMethodID=@m, AccountID=@a, Notes=@n, Percent_Disc = @Percent_Disc, is_Lock = @is_Lock ,IsActive = @IsActive
                    WHERE ID=@id
                ", con)
                cmd.Parameters.AddWithValue("@id", CurrentID)
                cmd.Parameters.AddWithValue("@m", methodId)
                cmd.Parameters.AddWithValue("@a", accountId)
                cmd.Parameters.AddWithValue("@n", notes)
                cmd.Parameters.AddWithValue("@Percent_Disc", Percent_Disc_txt.Text)
                cmd.Parameters.AddWithValue("@is_Lock", isLockCB.Checked)
                cmd.Parameters.AddWithValue("@IsActive", IsActive_CB.Checked)
                cmd.ExecuteNonQuery()
            End If
        End Using

        ClearForm()
        LoadLinksGrid()
        MessageBox.Show("تم الحفظ بنجاح")

    End Sub

    '------------------------------------------
    Private Sub dgvLinks_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLinks.CellClick
        If e.RowIndex < 0 Then Exit Sub

        CurrentID = CInt(dgvLinks.Rows(e.RowIndex).Cells("ID").Value)
        LoadRecord(CurrentID)
    End Sub

    Private Sub LoadRecord(id As Integer)
        Using con As New SqlConnection(ConStr)
            con.Open()

            Dim cmd As New SqlCommand("SELECT * FROM PaymentMethodDefaultAccounts WHERE ID=@id", con)
            cmd.Parameters.AddWithValue("@id", id)

            Dim rd = cmd.ExecuteReader()
            If rd.Read() Then
                cmbPaymentMethod.SelectedValue = rd("PaymentMethodID")
                cmbAccount.SelectedValue = rd("AccountID")
                txtNotes.Text = rd("Notes").ToString()


                If Not IsDBNull(rd("Percent_Disc")) Then
                    Percent_Disc_txt.Text = rd("Percent_Disc")
                Else
                    Percent_Disc_txt.Text = 0
                End If


                If Not IsDBNull(rd("is_Lock")) Then
                    isLockCB.Checked = rd("is_Lock")
                Else
                    isLockCB.Checked = False
                End If

                If Not IsDBNull(rd("IsActive")) Then
                    IsActive_CB.Checked = rd("IsActive")
                Else
                    IsActive_CB.Checked = False
                End If

            End If
        End Using
    End Sub

    '------------------------------------------
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If CurrentID = 0 Then Exit Sub

        Using con As New SqlConnection(ConStr)
            con.Open()
            'Dim cmd As New SqlCommand("UPDATE PaymentMethodDefaultAccounts SET IsActive=0 WHERE ID=@id", con)
            Dim cmd As New SqlCommand("DELETE FROM PaymentMethodDefaultAccounts WHERE ID=@id", con)
            cmd.Parameters.AddWithValue("@id", CurrentID)
            cmd.ExecuteNonQuery()
        End Using

        ClearForm()
        LoadLinksGrid()
        MessageBox.Show("تم الحذف")
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearForm()
    End Sub

    Private Sub ClearForm()
        CurrentID = 0
        cmbPaymentMethod.SelectedIndex = -1
        cmbAccount.SelectedIndex = -1
        txtNotes.Clear()
        Percent_Disc_txt.Clear()
        isLockCB.Checked = False
    End Sub

    Private Sub Show_AG_Projects_btn_Click(sender As Object, e As EventArgs) Handles Show_AG_Projects_btn.Click
        Dim F As New Normal_Form
        F.Form_Name = "PAYMENT_METHOD"
        F.Form_Name_Arabic = "أنواع طرق الدفع"
        F.F_ID = "P_ID"
        F.F_Name = "PAYMENT_NAME"
        F.F_DETAILS = "PAYMENT_METHOD"
        F.Checked_Table = "Agents_Balance_MV_RCT"
        F.Checked_Table_ID = "Pay_ID"
        F.Hidden_numbers.Add(1)
        F.ShowDialog()
    End Sub
End Class

Public Class ComboItem
    Public Property Text As String
    Public Property Value As Object

    Public Sub New(t As String, v As Object)
        Text = t
        Value = v
    End Sub
End Class

