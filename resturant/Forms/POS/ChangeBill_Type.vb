Imports System.Data.SqlClient

Public Class ChangeBill_Type
    Dim rs As New Resizer
    Dim SB_T_ID As Integer
    Dim IM_ID As Integer
    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        Me.Close()
    End Sub

    Private Sub ChangeUnit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        F_POS.Fetch_IM()
        Me.Dispose()
    End Sub

    Private Sub ChangeUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Sales_Load_IM()
        'IM_NameLb.Text = F_POS.MetroGrid.CurrentRow.Cells("IM_NameCL").Value
        'SB_T_ID = F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value
        'IM_ID = F_POS.MetroGrid.CurrentRow.Cells("IM_ID_CL").Value
    End Sub

    Public Sub Sales_Load_IM()

        IMPanel.Controls.Clear()
        Dim IMName As String
        Dim FontSize
        Dim StandarMeasure As Double
        Dim counter = 1
        Dim x = 5
        Dim y = 10
        Dim C As New C
        'With C.Com
        '    .Connection = C.Con
        '    .CommandText = "SB_IM_Select_Units"
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.AddWithValue("@IM_T_ID", F_POS.MetroGrid.CurrentRow.Cells("IM_T_ID").Value)
        'End With


        C.Str = "select B_Type_ID,B_Name from Sales_Bills_Types"
        C.Com = New SqlCommand(C.Str, C.Con)

        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            While C.Dr.Read()
                counter += 1
                Dim IMbtn As New Button
                IMbtn.Name = ("IMbtn" + C.Dr("B_Type_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(IMPanel.Size.Width / 2.4, IMPanel.Size.Height / 2.3)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMName = C.Dr("B_Name")
                FontSize = ((h / 100) * (w / 100)) / (IMName.Count * IMName.Count)
                StandarMeasure = (w + h) / 190
                IMbtn.Font = New System.Drawing.Font("Segoe UI", FontSize + (StandarMeasure + 2), Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(1, Byte))
                IMbtn.Text = IMName
                IMbtn.BackgroundImageLayout = ImageLayout.Stretch
                IMbtn.Tag = C.Dr("B_Type_ID")
                AddHandler IMbtn.Click, AddressOf IMbtn_Click
                IMbtn.TextAlign = ContentAlignment.MiddleCenter
                IMbtn.ForeColor = Color.Black
                IMbtn.BackColor = Color.LightGray

                Controls.Add(IMbtn)
                IMbtn.Parent = IMPanel

                If counter = 3 Then
                    counter = 1
                    x = 4
                    y += IMPanel.Size.Height / 2.3
                Else
                    x += IMPanel.Size.Width / 2.4
                End If

            End While
        End If

        C.Con.Close()

    End Sub

    Sub IMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SB_Contents_Confirm_Unit(sender.tag)
    End Sub

    'Private Sub UnitsGridViewX1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
    '    If e.ColumnIndex = 0 Then SB_Contents_Confirm_Unit(sender.tag)
    'End Sub

    Public Sub SB_Contents_Confirm_Unit(Type_ID As Integer)


        If F_POS.is_Form_Start = True Then

            If F_POS.BillTypeCmb.Items.Count > 0 And F_POS.T_ID > 0 Then

                If F_POS.is_Show_Bill = False Then
                    If (F_POS.Prev_Type = 3 Or Type_ID = 3) And (F_POS.MetroGrid.Rows.Count > 0) Then
                        Dim MSG As New OK
                        MSG.Msg_Lb.Text = " لتحويل الفاتورة إلى طلبية أو من طلبية يجب عليك حذف كل الأصناف الموجودة بها أولا "
                        MSG.ShowDialog()
                        F_POS.BillTypeCmb.SelectedValue = F_POS.Prev_Type
                        Exit Sub
                    End If
                End If

                SB_Update_Bill_Type(Type_ID)
                F_POS.Manage_Reseve()
            End If
        End If

        Me.Close()
    End Sub

    Private Sub SB_Update_Bill_Type(Type_ID As Integer)
        On Error Resume Next
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Update_Bill_Type"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", F_POS.T_ID)
            .Parameters.AddWithValue("@Type_ID", Type_ID)
            F_POS.BillTypeCmb.SelectedValue = Type_ID

            F_POS.Prev_Type = F_POS.BillTypeCmb.SelectedValue
        End With
        SQL_SP_EXEC(c.Com)
    End Sub


    Private Sub ChangeUnit_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub


End Class