Imports System.Data.SqlClient

Public Class FrmExchangeAuditViewer
    Private connectionString As String = MY_Settings.SqlConStr

    Private Sub FrmExchangeAuditViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadData()
    End Sub

    Private Sub LoadData()

        Using con As New SqlConnection(connectionString)
            Dim dt As New DataTable

            Dim sql As String = "
            SELECT 
                AuditId,
                ExchangeId,
                ActionType,
                UserId,
                MachineName,
                WindowsUser,
                ActionDate,
                BalanceBefore,
                BalanceAfter,
                RateSnapshot,
                CurrentRate,
                Details
            FROM ExchangeAuditLog
            WHERE ActionDate BETWEEN @From AND @To"

            If txtExchangeId.Text <> "" Then
                sql &= " AND ExchangeId = @ExchangeId"
            End If

            sql &= " ORDER BY AuditId DESC"

            Dim cmd As New SqlCommand(sql, con)

            cmd.Parameters.AddWithValue("@From", dtpFrom.Value.Date)
            cmd.Parameters.AddWithValue("@To", dtpTo.Value.Date.AddDays(1))

            If txtExchangeId.Text <> "" Then
                cmd.Parameters.AddWithValue("@ExchangeId", txtExchangeId.Text)
            End If

            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)

            dgv.DataSource = dt
            ColorizeRows()
        End Using

    End Sub

    Private Sub ColorizeRows()

        For Each row As DataGridViewRow In dgv.Rows

            Dim actionType As String = row.Cells("ActionType").Value.ToString()

            If actionType = "Approve" Then
                row.DefaultCellStyle.BackColor = Color.Honeydew
            ElseIf actionType = "Reject" Then
                row.DefaultCellStyle.BackColor = Color.MistyRose
            End If

        Next

    End Sub


    Private Sub txtQuickSearch_TextChanged(sender As Object, e As EventArgs) Handles txtQuickSearch.TextChanged

        If dgv.DataSource Is Nothing Then Exit Sub

        Dim dv As DataView = CType(dgv.DataSource, DataTable).DefaultView

        Dim filterText As String = txtQuickSearch.Text.Replace("'", "''")

        dv.RowFilter =
            $"Convert(ExchangeId,'System.String') LIKE '%{filterText}%' OR " &
            $"ActionType LIKE '%{filterText}%' OR " &
            $"Convert(UserId,'System.String') LIKE '%{filterText}%' OR " &
            $"MachineName LIKE '%{filterText}%'"

    End Sub

End Class