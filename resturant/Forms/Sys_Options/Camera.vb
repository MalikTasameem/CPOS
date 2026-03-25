Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.IO
Public Class Camera
    Dim CAMARA As VideoCaptureDevice
    Dim BMP As Bitmap
    Dim Cap As String = "Capture"
    Public Cach_Pic As Boolean = False
    Dim rs As New Resizer
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Try
        'CAMARA.Stop()
        'F_ItemsMenu.IM_Photo.Image = pbcaptureimage.Image
        Me.Dispose()
        'Catch ex As Exception
        '    Me.Dispose()
        'End Try
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        cmdno.Visible = False
        cmdok.Visible = False
        Dim CAMARAS As VideoCaptureDeviceForm = New VideoCaptureDeviceForm()
        If CAMARAS.ShowDialog() = DialogResult.OK Then
            CAMARA = CAMARAS.VideoDevice
            AddHandler CAMARA.NewFrame, New NewFrameEventHandler(AddressOf CAPTURAR)
            CAMARA.Start()
        Else
            Me.Close()
        End If
    End Sub
    Private Sub CAPTURAR(sender As Object, eventArgs As NewFrameEventArgs)

        BMP = DirectCast(eventArgs.Frame.Clone(), Bitmap)
        pbcaptureimage.Image = DirectCast(eventArgs.Frame.Clone(), Bitmap)

    End Sub
    Private Sub pbcapture_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmdno_Click(sender As Object, e As EventArgs) Handles cmdno.Click
        Dim CAMARAS As VideoCaptureDeviceForm = New VideoCaptureDeviceForm()
        CAMARA.Start()
        cmdno.Visible = False
        cmdok.Visible = False
        pbcapture.Visible = True
        Cap = "Capture"
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Cach_Pic = True
        Try
            Dim PATH As String
            If String.IsNullOrWhiteSpace(MY_Settings.SERVER_IMG_PATH) Then
                PATH = Application.StartupPath & "\IM IMG\" & F_ItemsMenu.IM_Name_ToolStrip.Text & " (" & F_ItemsMenu.IM_ID & ").jpg"
                'IM_PH_PATH = System.IO.Path.GetFullPath(OpenFL.FileName)
            Else
                '    PATH = Application.StartupPath & "\IM IMG\" & F_ItemsMenu.IM_Name_ToolStrip.Text & ".jpg"
                PATH = MY_Settings.SERVER_IMG_PATH & "\" & F_ItemsMenu.IM_Name_ToolStrip.Text & " (" & F_ItemsMenu.IM_ID & ") .jpg"
            End If

            'Dim PATH As String = Application.StartupPath & "\IM IMG\" & F_ItemsMenu.IM_Name_ToolStrip.Text & ".jpg"

            'Dim lastSlash As Integer = matchPath.LastIndexOf("\"c)
            'Dim path As String = matchPath.Substring(0, lastSlash)
            'Dim match As String = matchPath.Substring(lastSlash + 1)
            'Dim files As String() = Directory.GetFiles(path, match)
            'For Each file As String In files
            '    file.Delete(file)
            'Next

            File.Delete(PATH)


            F_ItemsMenu.IM_PH_PATH = PATH
            pbcaptureimage.Image.Save(PATH)

            CAMARA.Stop()
            F_ItemsMenu.IM_Photo.Image = pbcaptureimage.Image
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles pbcapture.Click
        If Cap = "Capture" Then
            cmdno.Visible = True
            cmdok.Visible = True
            pbcapture.Visible = False
            CAMARA.Stop()
            Cap = "Start"
        ElseIf Cap = "Start" Then
            Dim CAMARAS As VideoCaptureDeviceForm = New VideoCaptureDeviceForm()
            CAMARA.Start()
            cmdno.Visible = False
            cmdok.Visible = False
            Cap = "Capture"
        End If
    End Sub

    Private Sub Camera_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub
End Class
