Imports System.Runtime.InteropServices

Public Class frmWithdrawalDeposit

    ' ==========================================
    ' 1. كود تحريك الفورم (Drag Form)
    ' ==========================================
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Sub ReleaseCapture()
    End Sub

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Sub SendMessage(ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
    End Sub

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = &H2

    Private Sub pnlTopBar_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTopBar.MouseDown, lblTitle.MouseDown
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
        End If
    End Sub

    ' ==========================================
    ' 2. أزرار التحكم بالنافذة (إغلاق، تكبير، تصغير)
    ' ==========================================
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As EventArgs) Handles btnMaximize.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    ' ==========================================
    ' 3. التبديل بين المسبوك والمصنعية
    ' ==========================================
    Private Sub RadioButtons_CheckedChanged(sender As Object, e As EventArgs) Handles rbCast.CheckedChanged, rbManufactured.CheckedChanged
        ' هذه الأسطر تعكس حالة ظهور اللوحات بناءً على الخيار المحدد
        pnlCast.Visible = rbCast.Checked
        pnlManufactured.Visible = rbManufactured.Checked
    End Sub

End Class