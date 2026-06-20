Imports System.Data
Imports System.Data.SqlClient

Public Class FrmSystemAccountTypeEdit

    Private ReadOnly ConStr As String = MY_Settings.SqlConStr

    Public Property SystemAccountTypeID As Integer
    Public Property AccountNameAr As String
    Public Property RequiredValue As Boolean
    Public Property AllowSameAccountValue As Boolean
    Public Property MustBeLeafValue As Boolean
    Public Property IsActiveValue As Boolean
    Public Property ExpectedNaturalValue As String
    Public Property NotesValue As String

    Private Sub FrmSystemAccountTypeEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' تطبيق الثيم الإجباري
        ThemeManager.ApplyThemeToForm(Me)

        lblAccountName.Text = AccountNameAr

        chkRequired.Checked = RequiredValue
        chkAllowSameAccount.Checked = AllowSameAccountValue
        chkMustBeLeaf.Checked = MustBeLeafValue
        chkIsActive.Checked = IsActiveValue

        cmbNatural.Items.Clear()
        cmbNatural.Items.Add("بدون")
        cmbNatural.Items.Add("مدين")
        cmbNatural.Items.Add("دائن")

        If ExpectedNaturalValue = "D" Then
            cmbNatural.SelectedItem = "مدين"
        ElseIf ExpectedNaturalValue = "C" Then
            cmbNatural.SelectedItem = "دائن"
        Else
            cmbNatural.SelectedItem = "بدون"
        End If

        txtNotes.Text = If(NotesValue, "")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveType()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub SaveType()
        Try
            Dim naturalValue As Object = DBNull.Value

            If cmbNatural.SelectedItem IsNot Nothing Then
                Select Case cmbNatural.SelectedItem.ToString()
                    Case "مدين"
                        naturalValue = "D"
                    Case "دائن"
                        naturalValue = "C"
                    Case Else
                        naturalValue = DBNull.Value
                End Select
            End If

            Using cn As New SqlConnection(ConStr)
                Using cmd As New SqlCommand("dbo.ACC_SYSTEM_ACCOUNT_TYPE_UPDATE_SAFE", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 60

                    cmd.Parameters.Add("@SystemAccountTypeID", SqlDbType.Int).Value = SystemAccountTypeID
                    cmd.Parameters.Add("@Required", SqlDbType.Bit).Value = chkRequired.Checked
                    cmd.Parameters.Add("@AllowSameAccount", SqlDbType.Bit).Value = chkAllowSameAccount.Checked
                    cmd.Parameters.Add("@MustBeLeaf", SqlDbType.Bit).Value = chkMustBeLeaf.Checked

                    If naturalValue Is DBNull.Value Then
                        cmd.Parameters.Add("@Expected_ACC_NATURAL", SqlDbType.Char, 1).Value = DBNull.Value
                    Else
                        cmd.Parameters.Add("@Expected_ACC_NATURAL", SqlDbType.Char, 1).Value = naturalValue
                    End If

                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = chkIsActive.Checked

                    If String.IsNullOrWhiteSpace(txtNotes.Text) Then
                        cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = DBNull.Value
                    Else
                        cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = txtNotes.Text.Trim()
                    End If

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("تم حفظ تعديل النمط بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في حفظ النمط", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class