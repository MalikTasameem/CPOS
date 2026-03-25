Public Class POS_Abcent_Bills


    Private Sub POS_D_Valid_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub POS_D_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cancel_ALL_Bills_btn.Visible = U_SalesVoid
        Load_Valid()
    End Sub

    Private Sub Load_Valid()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "[Open_AGMV_SB_SELECT]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@USER_ID", USER_ID)
            If U_Save_otherBill = True Then
                .Parameters.AddWithValue("@IS_ADMIN", True)
            Else
                .Parameters.AddWithValue("@IS_ADMIN", User_isAdmin)
            End If
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Open_MV_DV.DataSource = C.Dt

        'Open_MV_DV.Columns(0).Visible = False
        'Open_MV_DV.Columns(3).Visible = False
        'Open_MV_DV.Columns(4).Visible = False
        'Open_MV_DV.Columns(6).Visible = False

        Open_MV_DV.Columns(1).Visible = False
        Open_MV_DV.Columns(4).Visible = False
        Open_MV_DV.Columns(5).Visible = False
        Open_MV_DV.Columns(7).Visible = False

        Open_MV_DV.Columns(2).ReadOnly = True
        Open_MV_DV.Columns(3).ReadOnly = True
        Open_MV_DV.Columns(6).ReadOnly = True

    End Sub

    Private Sub Cancel_ALL_Bills_btn_Click(sender As Object, e As EventArgs) Handles Cancel_ALL_Bills_btn.Click
        If MessageBox.Show(" سيتم إلغاء كل الواتير المحددة ", " تأكيد الإلغاء ", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then

            For i = 0 To Open_MV_DV.Rows.Count - 1

                Try
                    Dim IsTicked As Boolean = CBool(Open_MV_DV.Rows(i).Cells(0).Value)
                    If IsTicked Then
                        'DependingBill(Open_MV_DV.Rows(i).Cells(1).Value)
                        'Else
                        SB_VoidBill(Open_MV_DV.Rows(i).Cells(1).Value, Open_MV_DV.Rows(i).Cells(3).Value)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Next


            Load_Valid()
        End If

    End Sub


    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub CHECK_ALL_CB_CheckedChanged(sender As Object, e As EventArgs) Handles CHECK_ALL_CB.CheckedChanged
        If CHECK_ALL_CB.Checked = True Then
            For i = 0 To Open_MV_DV.Rows.Count - 1
                Open_MV_DV.Rows(i).Cells(0).Value = True
            Next
        Else
            For i = 0 To Open_MV_DV.Rows.Count - 1
                Open_MV_DV.Rows(i).Cells(0).Value = False
            Next
        End If
    End Sub

    Private Sub Confirm_ALL_Bills_btn_Click(sender As Object, e As EventArgs) Handles Confirm_ALL_Bills_btn.Click
        If MessageBox.Show(" سيتم إعتماد كل الواتير المحددة ", " تأكيد الإعتماد ", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then

            For i = 0 To Open_MV_DV.Rows.Count - 1

                Try
                    Dim IsTicked As Boolean = CBool(Open_MV_DV.Rows(i).Cells(0).Value)
                    If IsTicked Then
                        DependingBill(Open_MV_DV.Rows(i).Cells(1).Value)
                        'Else
                        '    Void_Row(Open_MV_DV.Rows(i).Cells(1).Value)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Next

            Load_Valid()
        End If
    End Sub

    Private Sub SB_VoidBill(T_ID As Integer, Bill_Num As Integer)
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Void_Row"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert("إلغاء الفاتورة", Bill_Num, 1, 3)
        End If
    End Sub

End Class