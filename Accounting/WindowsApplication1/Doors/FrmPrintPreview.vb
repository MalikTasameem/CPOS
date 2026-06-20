Imports System.Drawing.Printing

Public Class FrmPrintPreview

    Private CurrentPreviewPage As Integer = 0

    Public Sub SetDocument(doc As PrintDocument, title As String)
        PrintDocument1 = doc
        preview.Document = PrintDocument1
        CurrentPreviewPage = 0
        preview.StartPage = CurrentPreviewPage
        lblTitle.Text = title
        lblStatus.Text = "تم تحميل التقرير"
        UpdatePageNavigation()
    End Sub

    Private Sub FrmPrintPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BudgetUiHelper.ApplyBudgetFormStyle(Me)
        preview.Zoom = 1.0R
        preview.Rows = 1
        preview.Columns = 1
        UpdatePageNavigation()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            PrintDialog1.Document = PrintDocument1
            If PrintDialog1.ShowDialog() = DialogResult.OK Then
                PrintDocument1.Print()
            End If
        Catch ex As Exception
            lblStatus.Text = "خطأ: " & ex.Message
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExportPdf_Click(sender As Object, e As EventArgs) Handles btnExportPdf.Click
        Try
            Dim sfd As New SaveFileDialog()
            sfd.Filter = "PDF Files (*.pdf)|*.pdf"
            sfd.FileName = lblTitle.Text & ".pdf"

            If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub

            ExportToPdf(PrintDocument1, sfd.FileName)

            lblStatus.Text = "تم تصدير الملف بنجاح"
            'MessageBox.Show("تم إنشاء ملف PDF بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("خطأ أثناء التصدير: " & ex.Message)
        End Try
    End Sub

    Private Sub Min_Btn_Click(sender As Object, e As EventArgs) 
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Max_Btn_Click(sender As Object, e As EventArgs) 
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnFirstPage_Click(sender As Object, e As EventArgs) Handles btnFirstPage.Click
        CurrentPreviewPage = 0
        ShowPreviewPage()
    End Sub

    Private Sub btnPrevPage_Click(sender As Object, e As EventArgs) Handles btnPrevPage.Click
        If CurrentPreviewPage <= 0 Then Exit Sub
        CurrentPreviewPage -= 1
        ShowPreviewPage()
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        CurrentPreviewPage += 1
        ShowPreviewPage()
    End Sub

    Private Sub ShowPreviewPage()
        Try
            preview.StartPage = CurrentPreviewPage
            preview.InvalidatePreview()
            UpdatePageNavigation()
        Catch ex As Exception
            lblStatus.Text = "خطأ في الانتقال للصفحة: " & ex.Message
        End Try
    End Sub

    Private Sub UpdatePageNavigation()
        If lblPageNav Is Nothing Then Exit Sub

        lblPageNav.Text = "صفحة " & (CurrentPreviewPage + 1).ToString()
        btnFirstPage.Enabled = CurrentPreviewPage > 0
        btnPrevPage.Enabled = CurrentPreviewPage > 0
    End Sub
End Class
