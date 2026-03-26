Public Class IMEX_IM_card : Inherits System.Windows.Forms.Form


    Public T_ID As Integer


    Dim IM_ID As Integer = 0

    Dim IM_QTY As Double = 0

    Dim U_Dt As New DataTable
        Dim Get_Unit As Boolean = False
        Dim ALL_QTY As Double = 0
        Dim U_Cargo As Double = 0
        Dim Valid_Dt As New DataTable
    Dim U_ID As Integer
    Dim On_Update As Boolean
    Public Barcode_IM As String = ""

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then mySearchControl.txtSearch.Select()

    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If St_Count() = 1 Then All_St_Panel.Visible = False


        Load_ST()

        FunModule.Load_ALL_IM()
        ' تحميل البيانات
        mySearchControl.ItemsTable = IM_Dt
            mySearchControl.itemsTable_Barcode = IM_Dt_Barcodes
            mySearchControl.MaxGridHeight = 400
            'mySearchControl.DefaultSearchField = "اسم الصنف"
            ' إضافة الكنترول للفورم
            'Me.Controls.Add(mySearchControl)
            ' استقبال الاختيار
            AddHandler mySearchControl.ItemSelected, AddressOf HandleItemSelected

            mySearchControl.txtSearch.Select()

        End Sub


    Private Sub HandleItemSelected(itemId As Integer, isValid As String)

        IM_ID = itemId
        Get_Unit = False
        Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
        Fetch_IM_Units()
        QtyTextBox.Select()

        If isValid = 1 Then
            Valid_Panel.Visible = True
            Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
        Else
            Valid_Panel.Visible = False
        End If
    End Sub


    Public Sub Load_ST()
            Dim c As New C
            Try
                Dim s As String
                s = "select ST_ID,ST_name from STORES ORDER By ST_ID ASC"
                c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
                c.Da.Fill(c.Dt)
                ST_cm.DataSource = c.Dt
                ST_cm.DisplayMember = "ST_name"
                ST_cm.ValueMember = "ST_ID"
                ST_cm.SelectedValue = PCH_ST_ID
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub

    Private Sub ClearFields()
        ClearCatFields()
    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
            If IM_ID = 0 Then
                MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
                mySearchControl.txtSearch.Select()
            Else
                If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"

                If IM_min_QTY = False Then
                    If IM_Check_Neg_QTY_() = 1 Then
                        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If

                Insert_Cat()
            End If

        End Sub

        Private Function IM_Check_Neg_QTY_()
            Dim C As New C
            Dim F As Integer = 0
            With C.Com
                .Connection = C.Con
                .CommandText = "IM_Check_Neg_QTY_"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@F", 0)
                .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
                .Parameters.AddWithValue("@IM_ID", IM_ID)
                .Parameters.AddWithValue("@D_Vaild", Valid_cm.Text)
                .Parameters.AddWithValue("@Enterd_Qty", QtyTextBox.Text)
                .Parameters.AddWithValue("@Cargo", U_Cargo)

                .Parameters("@F").Direction = ParameterDirection.Output
                If SQL_SP_EXEC(C.Com) Then
                    F = .Parameters("@F").Value
                End If
            End With

            Return F
        End Function


    Private Sub ClearCatFields()
            mySearchControl.Clear_txt()
            Current_QTY.Clear()
            QtyTextBox.Clear()
            IM_Cost_txt.Clear()
            U_Dt.Clear()
            Barcode_IM = ""
        End Sub



    Private Sub Insert_Cat()
        If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)
        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "IMEX_Details_Insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@IMEX_T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@IM_ID", IM_ID)
        sqlComm.Parameters.AddWithValue("@U_ID", U_ID)
        sqlComm.Parameters.AddWithValue("@Price", IM_Cost_txt.Text)
        sqlComm.Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
        sqlComm.Parameters.AddWithValue("@Barcode", Barcode_IM)
        sqlComm.Parameters.AddWithValue("@Total", Convert.ToDouble(QtyTextBox.Text) * Convert.ToDouble(IM_Cost_txt.Text))
        If Valid_Panel.Visible = True Then sqlComm.Parameters.AddWithValue("@D_Vaild", Valid_cm.Text)
        If String.IsNullOrWhiteSpace(QtyTextBox.Text) = False Then sqlComm.Parameters.AddWithValue("@QYT", Convert.ToDouble(QtyTextBox.Text))
        sqlComm.Parameters.AddWithValue("@On_Update", On_Update)
        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" الصنف:" & mySearchControl.txtSearch.Text & " الوحدة:" & IM_Unit_cm.Text & " العدد:" & QtyTextBox.Text & " التكلفة:" & IM_Cost_txt.Text, F_IM_Execute.Bill_ID_Txt.Text, 8, 1)
            ClearCatFields()
            F_IM_Execute.SELECT_EXP_Cats(T_ID)
            Me.Close()
        End If

    End Sub

    Private Sub IM_Cost_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Cost_txt.KeyDown
            Select Case e.KeyCode
                Case Keys.Up : mySearchControl.txtSearch.Select()
                Case Keys.Right : QtyTextBox.Select()
            End Select
        End Sub


        Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown

            Select Case e.KeyCode
                Case Keys.Return : If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
                Case Keys.Left : If Valid_Panel.Visible = True Then
                        Valid_cm.Select()
                        Valid_cm.DroppedDown = True
                    End If
                Case Keys.Up : mySearchControl.txtSearch.Select()
                Case Keys.Right
                    IM_Unit_cm.Select()
                    IM_Unit_cm.DroppedDown = True
            End Select

        End Sub

        Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyTextBox.KeyPress
            Check_Only_Float(sender, e)
        End Sub

        Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles QtyTextBox.TextChanged
            Check_Point_in_FloatNum(sender, e)
        End Sub


    Private Sub Fetch_IM_Units()
            Get_Unit = False
            Dim c As New C
            U_Dt.Clear()
            Try
                Dim s As String
                s = "select U_IM_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Desc"
                c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
                c.Da.Fill(U_Dt)
                IM_Unit_cm.DataSource = U_Dt
                IM_Unit_cm.DisplayMember = "U_Name"
                IM_Unit_cm.ValueMember = "U_IM_ID"
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Get_Unit = True
            IM_Fetch_QTY()
        End Sub

        Private Sub IM_Fetch_QTY()
            Dim c As New C
            Try
                Dim s As String
                s = "select U_ID,U_Cargo,Price from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "'"
                c.Com = New SqlClient.SqlCommand(s, c.Con)
                c.Con.Open()
                c.Dr = c.Com.ExecuteReader
                If c.Dr.HasRows Then
                    c.Dr.Read()
                    U_Cargo = c.Dr("U_Cargo")
                    Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
                    Current_QTY.Text = N.ToString("N")
                    ALL_QTY_txt.Text = ALL_QTY / U_Cargo
                    U_ID = c.Dr("U_ID")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


            c = New C
            Try
                Dim s As String
                s = "select Cost from IM_All_V WHERE IM_ID = '" & IM_ID & "'"
                c.Com = New SqlClient.SqlCommand(s, c.Con)
                c.Con.Open()
                c.Dr = c.Com.ExecuteReader
                If c.Dr.HasRows Then
                    c.Dr.Read()
                    IM_Cost_txt.Text = c.Dr("Cost") * U_Cargo
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub

        Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If String.IsNullOrWhiteSpace(Current_QTY.Text) = False And Get_Unit = True Then
            IM_Fetch_QTY()
            If Valid_Panel.Visible = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)

        End If
    End Sub

        Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Unit_cm.KeyDown
            Select Case e.KeyCode
                Case Keys.Return, Keys.Left : QtyTextBox.Select()
            End Select
        End Sub


        Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
            If Get_Unit = True Then
                Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
                IM_Fetch_QTY()
                If Valid_Panel.Visible = True Then Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
            End If
        End Sub

        Private Sub Valid_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Valid_cm.SelectedValueChanged
            If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
        End Sub

    Private Sub Exit_Btn_Click(sender As Object, e As EventArgs) Handles Exit_Btn.Click
        Me.Close()
    End Sub
End Class