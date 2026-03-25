Imports System.Data.SqlClient
Imports System.IO

Public Class Show_Uploaded_Files

    Private Sub Show_IM_Rtn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "قائمة اخر 10 ملفات مرفوعة"
        Select_TEMPLATE()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Show_IM_Rtn_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub



    Public Sub Select_TEMPLATE()
        Dim c As New Online_C
        Try
            Dim s As String
            s = "Select TOP 10 T_ID,DATE,DOC_NAME From Upload_Files ORDER BY DATE DESC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            MetroGrid.DataSource = c.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub PreviewPdf()
        ' Try
        Try
            Dim c As New C
            '  If MetroGrid.SelectedRows.Count > 0 Then

            c.Com = New SqlCommand("Select DOC_DATA,DOC_EXTENSION from Upload_Files WHERE T_ID=" & MetroGrid.CurrentRow.Cells("T_ID_CL").Value, c.Con)

            c.Com.CommandTimeout = 10000000
            c.Con.Open()
            Dim fileData As Byte() = DirectCast(c.Com.ExecuteScalar(), Byte())

            c.Dr = c.Com.ExecuteReader
            c.Dr.Read()
            Dim fileext As String = ""

            Dim filename As String = Format(Now, "yyyy_MM_dd_hh_mm_ss")
            Dim sFileName = filename & c.Dr("DOC_EXTENSION")
            Dim sTempFileName As String = Application.StartupPath & "\Previews\" & sFileName
            Using fs As New FileStream(sTempFileName, FileMode.OpenOrCreate, FileAccess.Write)
                fs.Write(fileData, 0, fileData.Length)
                fs.Flush()
                fs.Close()
            End Using
            If System.IO.File.Exists(sTempFileName) Then
                Dim psi As New ProcessStartInfo(sTempFileName)
                psi.WindowStyle = ProcessWindowStyle.Maximized
                Dim proc As Process = Process.Start(psi)
            Else
                MessageBox.Show("الرجاء قم باختيار ملف موجود على هذا الجهاز !!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            'Else
            '    MessageBox.Show("الرجاء تحديد كتاب من القائمة", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End If
        Catch ex As Exception
            '  Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            MsgBox(ex.Message)
            ' C.Con.Close()
        End Try

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    ' c.Con.Close()
        '    ' Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        'End Try
    End Sub

    Private Sub MetroGrid_DoubleClick(sender As Object, e As EventArgs) Handles MetroGrid.DoubleClick
        If MetroGrid.Rows.Count > 1 Then PreviewPdf()
    End Sub
End Class