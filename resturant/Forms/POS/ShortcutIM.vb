Imports System.IO

Public Class ShortcutIM
    Public h = My.Computer.Screen.Bounds.Size.Height
    Public w = My.Computer.Screen.Bounds.Size.Width
    Dim Data As Byte()

    Private Sub ShothcutIM_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub ShothcutIM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadShortCut_IM()
    End Sub
    Dim IM_Name As String = ""
    Dim is_Valid As String

    Private Sub loadShortCut_IM()
        IMPanel.Controls.Clear()


        Dim c As New C
        Dim x = 2.5
        Dim y = 2.5
        Dim counter = 1
        Dim IMName As String
        '   Dim StandarMeasure As Double
        '   Dim FontSize

        Dim s As String = ""

        s = "select IM_ID,item_name,Photo,BK_R,BK_G,BK_B,FK_R,FK_G,FK_B from IM_Menu WHERE is_Shortcut = 1  order by item_name ASC"
        c.Com = New SqlClient.SqlCommand(s, c.Con)
        c.Con.Open()
        c.Dr = c.Com.ExecuteReader
        If c.Dr.HasRows Then
            While c.Dr.Read()
                counter += 1
                Dim IMbtn As New Button
                IMbtn.Name = ("IMbtn" + c.Dr("IM_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(IMPanel.Size.Width / 6.5, IMPanel.Size.Height / 6)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMName = c.Dr("item_name")
                '    StandarMeasure = (w + h) / 190
                '  FontSize = (IMName.Count +)
                IMbtn.Font = New System.Drawing.Font("Segoe UI", 15, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                IMbtn.Text = c.Dr("item_name")
                Controls.Add(IMbtn)
                IMbtn.Parent = IMPanel
                AddHandler IMbtn.Click, AddressOf IMbtn_Click
                If IsDBNull(c.Dr("photo")) = False Then
                    IMbtn.BackColor = System.Drawing.SystemColors.Info
                    IMbtn.Font = New System.Drawing.Font("Segoe UI", 12, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                    Data = DirectCast(c.Dr("photo"), Byte())
                    Dim MS As New MemoryStream(Data)
                    Dim ImageH As Integer = (IMbtn.Size.Height / 1.75)
                    Dim ImageW As Integer = IMbtn.Size.Width
                    IMbtn.Image = ResizeImage(Image.FromStream(MS), ImageH, ImageW)
                    IMbtn.TextAlign = ContentAlignment.BottomCenter
                    IMbtn.ImageAlign = ContentAlignment.BottomCenter
                Else
                    IMbtn.Image = Nothing
                    IMbtn.TextAlign = ContentAlignment.MiddleCenter
                End If

                If IsDBNull(c.Dr("BK_R")) Then
                    IMbtn.BackColor = System.Drawing.SystemColors.Info
                Else
                    IMbtn.BackColor = Color.FromArgb(c.Dr("BK_R"), c.Dr("BK_G"), c.Dr("BK_B"))
                End If

                If IsDBNull(c.Dr("FK_R")) = False Then
                    IMbtn.ForeColor = Color.FromArgb(c.Dr("FK_R"), c.Dr("FK_G"), c.Dr("FK_B"))
                End If

                If counter = 7 Then
                    counter = 1
                    x = 2.5
                    y += IMPanel.Size.Height / 6
                Else
                    x += IMPanel.Size.Width / 6.5
                End If

            End While
        End If

        c.Con.Close()

    End Sub

    Sub IMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        'F_Sales.IM_SH_txt.Text = sender.Text.ToString
        'F_Sales.Search_From_Grid()
        'F_Sales.ADD_IM()
        'Me.Close()
    End Sub


    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        Me.Close()
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        Dim newLeft As Integer = -IMPanel.AutoScrollPosition.X
        Dim newTop As Integer = -IMPanel.AutoScrollPosition.Y
        newTop = -IMPanel.AutoScrollPosition.Y - 450
        IMPanel.AutoScrollPosition = New Point(newLeft, newTop)
    End Sub

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Dim newLeft As Integer = -IMPanel.AutoScrollPosition.X
        Dim newTop As Integer = -IMPanel.AutoScrollPosition.Y
        newTop = -IMPanel.AutoScrollPosition.Y + 450
        IMPanel.AutoScrollPosition = New Point(newLeft, newTop)
    End Sub
End Class