Imports System.IO
Imports System.Security.Cryptography


Public Class NewArchive
    Public new_flag As Boolean = True
    'Public update_flag As Boolean = False
    'Dim imagefilename As String = ""
    'Public Id As Integer
    'Public DocType As Integer = 0
    'Public PID As Integer = 0

    Public ValFalg As Boolean = False
    Public imageData As Byte()
    Public sFileName As String
    Public sFileTittle As String
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles FromScanner.Click
        Try
            Try
                Dim flag = MessageBox.Show("هل انت متاكد من اخذ المسح الضوئي", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If flag = DialogResult.Yes Then

                    AxScanner1.SelectImageSource()
                    'If My.Settings.allsalwatch Then
                    '    AxScanner1.AutoFeedEnabled = True
                    '    AxScanner1.FeederEnabled = True
                    'Else
                    AxScanner1.AutoFeedEnabled = False
                        AxScanner1.FeederEnabled = False
                    'End If
                    AxScanner1.DPI = 150
                    AxScanner1.PixelType = 0
                    AxScanner1.SetCaptureArea(0, 0, 0, 0)
                    Me.Enabled = False
                    AxScanner1.Scan()
                    Me.Enabled = True

                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub

    Public Function UploadPdfFile(Filename As String) As Boolean
        Try
            Dim FileToUpload As String = Filename
            Dim sFileToUpload As String = ""
            sFileToUpload = LTrim(RTrim(FileToUpload))
            Dim Extension As String = System.IO.Path.GetExtension(sFileToUpload)
            'upLoadImageOrFile(sFileToUpload, "Image")

            If Not upLoadImageOrFile(sFileToUpload, Extension) Then
                MessageBox.Show("حدث خطأ في تحميل الملف", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
                Return False
            End If
            Return True
        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            Return False
        End Try


    End Function
    Public Function Addrow() As Boolean

        Try
            AxScanner1 = New AxSCANNERLib.AxScanner
            MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ValFalg = True
            'NameFile = TXT_Dsc.Text.Text-----------------------------------------
            Me.Close()
            Return True
        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            Return False
        End Try
    End Function
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles FromFile.Click
        Try
            If new_flag Then

                'Dim flag = MessageBox.Show("هل انت متاكد من اختيار ملف", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                'If flag = DialogResult.Yes Then
                With OpenFileDialog1
                        .Filter = "PDF files (*.Pdf) | *.Pdf;"
                        .Title = "اختر ملف Pdf "
                        .FileName = ""
                        ' .Multiselect = False

                        If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                            If .FileNames.Count = 1 Then
                                Dim fi As New IO.FileInfo(.FileName)
                                Dim MB = (fi.Length / 1024) / 1024
                                If MB < 10 Then
                                    filename = .FileName
                                    Panel1.Visible = True
                                    Panel1.Parent = Me
                                    Panel1.BringToFront()
                                    CircularProgressControl1.Start()
                                    MainBack.RunWorkerAsync()
                                    Me.pnlMain.Enabled = False
                                    Panel1.Enabled = True
                                Else
                                    MessageBox.Show("حجم الملف اكبر من الحجم المصرح به", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            ElseIf .FileNames.Count() >= 1 Then
                                filename = ""
                                filenames = .FileNames
                                Panel1.Visible = True
                                Panel1.Parent = Me
                                Panel1.BringToFront()
                                CircularProgressControl1.Start()
                                MainBack.RunWorkerAsync()
                                Me.pnlMain.Enabled = False
                                Panel1.Enabled = True
                            End If
                        End If
                    End With
                Else
                    Exit Sub
                End If
            'End If
            Dim directoryName As String = Application.StartupPath & "\scan"
            Try
                For Each deleteFile In Directory.GetFiles(directoryName, "*.*", SearchOption.TopDirectoryOnly)
                    File.Delete(deleteFile)
                Next
            Catch ex As Exception

            End Try
        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub


    'Dim c As New C
    Private Function upLoadImageOrFile(ByVal sFilePath As String, ByVal sFileType As String) As Boolean
        Try
            'Dim SqlCom As SqlCommand
            'Dim imageData As Byte()
            'Dim sFileName As String
            'Dim qry As String
            'If c.Con.State = ConnectionState.Closed Then
            '    c.Con.Open()
            'End If
            imageData = readfile(sFilePath)
            sFileName = System.IO.Path.GetFileName(sFilePath)
            sFileTittle = fsDocType.Textt
            '    qry = "INSERT INTO [dbo].[DocumentArchive]
            '   ([filename]
            '   ,[ImageData]
            '   ,[Bill_id]
            '   ,[doc_type]
            '   ,[uploaded_by]
            '   ,[uploaded_at])
            '    values(@FileName, @ImageData," &
            '    "@Bill_id,@doc_type,@uploaded_by,@uploaded_at)"
            '    SqlCom = New SqlCommand(qry, c.Con)
            '    SqlCom.CommandTimeout = 0
            '    SqlCom.Parameters.Add(New SqlParameter("@FileName", sFileName))
            '    SqlCom.Parameters.Add(New SqlParameter("@ImageData", DirectCast(imageData, Object)))
            '    SqlCom.Parameters.Add(New SqlParameter("@Bill_id", Id))
            '    SqlCom.Parameters.Add(New SqlParameter("@doc_type", fsDocType.TXT_ID.Text))
            '    SqlCom.Parameters.Add(New SqlParameter("@uploaded_by", USER_ID))
            '    SqlCom.Parameters.Add(New SqlParameter("@uploaded_at", Now()))
            '    SqlCom.ExecuteNonQuery()
            '    c.Con.Close()
            Return True
        Catch ex As Exception
            '    'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            Return False
        End Try
    End Function
    Public Function tryEncryptBytes(ByRef B() As Byte, ByVal Pass As String) As Boolean
        Try
            Dim PassMD5Bytes() As Byte
            Dim MD5 As New MD5CryptoServiceProvider
            PassMD5Bytes = MD5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(Pass))
            Dim Rij As New RijndaelManaged
            Rij.Mode = CipherMode.ECB
            Rij.Key = PassMD5Bytes
            Dim Encryptor As ICryptoTransform = Rij.CreateEncryptor
            B = Encryptor.TransformFinalBlock(B, 0, B.Length)
            Return True
        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
    End Function

    Public Function tryDecryptBytes(ByRef B() As Byte, ByVal Pass As String) As Boolean
        Try
            Dim PassMD5Bytes() As Byte
            Dim MD5 As New MD5CryptoServiceProvider
            PassMD5Bytes = MD5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(Pass))
            Dim Rij As New RijndaelManaged
            Rij.Mode = CipherMode.ECB
            Rij.Key = PassMD5Bytes
            Dim Decryptor As ICryptoTransform = Rij.CreateDecryptor
            B = Decryptor.TransformFinalBlock(B, 0, B.Length)
            Return True
        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try

        Return False
    End Function



    Public Function readfile(sPath As String) As Byte()
        Try
            Dim data As Byte() = Nothing

            'Use FileInfo object to get file size.
            Dim fInfo As New FileInfo(sPath)
            Dim numBytes As Long = fInfo.Length

            'Open FileStream to read file
            Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)

            'Use BinaryReader to read file stream into byte array.
            Dim br As New BinaryReader(fStream)

            'When you use BinaryReader, you need to supply number of bytes to read from file.
            'In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes(CInt(numBytes))
            Return data
        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
    End Function


    Dim filename As String = ""
    Dim filenames As String()
    Private Sub AxScanner1_EndAllScan(sender As Object, e As EventArgs) Handles AxScanner1.EndAllScan
        Try
            Try
                filename = Application.StartupPath & "\Scan\" & Format(Now, "yyyy_MM_dd_hh_mm_ss") & ".pdf"
                AxScanner1.View = 5
                'BackgroundWorker1.RunWorkerAsync()
                Dim bresult = AxScanner1.SaveAllPage2PDF(filename, True, 1)
                If File.Exists(filename) Then
                    Panel1.Visible = True
                    Panel1.Parent = Me
                    Panel1.BringToFront()
                    CircularProgressControl1.Start()
                    MainBack.RunWorkerAsync()
                    Me.pnlMain.Enabled = False
                    Panel1.Enabled = True
                Else
                    MessageBox.Show("الملف الذي تم مسحه ضوئيا غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            End Try


            'AxScanner1.Save(Application.StartupPath & "\a111.jpg", "jpeg")
        Catch ex As Exception

        End Try
    End Sub


    Private Sub StopDownload_Click(sender As Object, e As EventArgs)
        Try
            If MainBack.IsBusy Then
                MainBack.CancelAsync()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Dim CompleteFlag = 0
    Private Sub InnerComplete(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
        If Not e.Cancelled Then
            CompleteFlag = 1
        Else
            CompleteFlag = 0
        End If

    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles MainBack.RunWorkerCompleted
        Try
            Panel1.Visible = False
            Me.pnlMain.Enabled = True
            If Not e.Cancelled Then
                If CompleteFlag And new_flag Then
                    Addrow()
                End If
            End If
        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub
    Private Sub DoWorkPreviewPdf(ByVal sender As Object, e As System.ComponentModel.DoWorkEventArgs)
        Try
            If filename <> "" Then
                If UploadPdfFile(filename) Then

                Else
                    e.Cancel = True
                    MessageBox.Show("حدث خطأ في تحميل الملف", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                For Each filename In filenames
                    Dim fi As New IO.FileInfo(filename)
                    Dim MB = (fi.Length / 1024) / 1024
                    If MB < 10 Then
                        If UploadPdfFile(filename) Then

                        Else
                            e.Cancel = True
                            MessageBox.Show("حدث خطأ في تحميل الملف", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Else
                        '   e.Cancel = True
                        MessageBox.Show("حجم الملف " & filename & "اكبر من المصرح به", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Next
            End If

        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub
    Private Sub BackgddroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles MainBack.DoWork
        Try
            Dim InnerBack As New System.ComponentModel.BackgroundWorker()
            AddHandler InnerBack.DoWork, AddressOf DoWorkPreviewPdf
            AddHandler InnerBack.RunWorkerCompleted, AddressOf InnerComplete
            InnerBack.WorkerSupportsCancellation = True
            InnerBack.RunWorkerAsync()
            While InnerBack.IsBusy
                If MainBack.CancellationPending Then
                    e.Cancel = True
                    InnerBack.CancelAsync()
                    InnerBack = Nothing
                    'c.Con.Close()
                    Exit While
                End If
            End While

            ' PreviewPdf()
        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub


    Private Sub AddDoc_Click(sender As Object, e As EventArgs) Handles btnAddDocType.Click
        Dim F As New Cities
        F.Form_Name = sender.Tag
        F.Form_Name_Arabic = "نوع المستند"
        F.F_ID = "ID"
        F.F_Name = "Name"

        F.F_DETAILS = "[doc_type]"
        F.F_DETAILS_TABLE = "[DocumentArchive]"
        F.WithOne = True
        F.ShowDialog()
    End Sub

    Private Sub NewArchive_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class