Public Class PchAdd_img
    Dim rs As New Resizer

    Dim isDelete As Boolean = False
    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        On Error Resume Next
        With Me.OpenFileDialog1
            .Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico"
            .FilterIndex = 1
            .Multiselect = False
            .Title = "إختر صورة مرفقة للفاتورة"
            .ShowDialog()
            If Len(.FileName) > 0 Then
                BillPictureBox.Image = Image.FromFile(OpenFileDialog1.FileName)
                Update_AG_Balance_MV_Img()
            End If
        End With
    End Sub

    Public Sub Update_AG_Balance_MV_Img()

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Update_AG_Balance_MV_Img"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", F_Pch.T_ID)
        If isDelete = False Then sqlComm.Parameters.AddWithValue("@Img", ConvertImage(BillPictureBox.Image))
        If SQL_SP_EXEC(sqlComm) = True Then
            'If isDelete = True Then
            '    BillPictureBox.Image = Nothing
            '    F_Pch.BillPictureBox.Image = Nothing
            'Else
            '    If BillPictureBox.Image IsNot Nothing Then
            '        F_Pch.BillPictureBox.Image = BillPictureBox.Image
            '    End If
            'End If
        End If

    End Sub

    Private Sub PchAdd_img_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
    End Sub

    Private Sub PchAdd_img_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Scan_img_btn_Click(sender As Object, e As EventArgs) Handles Scan_img_btn.Click
        Try
            If String.IsNullOrWhiteSpace(ImageScannerPath) Then
                MsgBox(" قم بتحديد مسار صور المسح الضوئي أولا ", MsgBoxStyle.Exclamation)
            Else
                Dim FileName As String
                FileName = TwainHandler.ScanIt(ImageScannerPath)
                BillPictureBox.Image = Image.FromFile(ImageScannerPath & "\" & FileName)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click
        If BillPictureBox.Image IsNot Nothing Then
            Beep()
            If MessageBox.Show(" حذف المرفق ؟ ", "", MessageBoxButtons.YesNo, _
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                isDelete = True
                Update_AG_Balance_MV_Img()
                isDelete = False
            End If
        End If
    End Sub

    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        Me.Close()
    End Sub
End Class