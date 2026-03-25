Imports System.Data.SqlClient

Public Class IM_Struct
    Dim rs As New Resizer
    Dim IM_DT As New DataTable
    Dim ST As Boolean = False

    Private Sub STORES_Explorer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    


    Private Sub STORES_Explorer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

  
    Dim FRM_IM_ID = 0
    Dim Get_Unit = False
    Dim U_Dt As New DataTable
    Dim FRM_Dt As New DataTable

    Private Sub IM_FRM_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_FRM_txt.TextChanged
        If IM_FRM_txt.Text.Count > 0 Then
            IM_FRM_txt_Load_IM()
        Else
            FRM_GDX.Visible = False
            FRM_IM_ID = 0
            QtyTextBox.Clear()
        End If
        If FRM_IM_ID = 0 Then
            IM_FRM_txt.BackColor = Color.LightGray
        Else
            IM_FRM_txt.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Public Sub IM_FRM_txt_Load_IM()
        Dim c As New C

        Try
            IM_Dt.Clear()
            Dim s As String
            s = "select IM_ID,item_name,isValid from IM_All_V WHERE item_name Like '%" & IM_FRM_txt.Text & "%' AND isValid = 0 AND ISSTORE IN(0,1) Order by item_name ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Dt)
            FRM_GDX.DataSource = IM_Dt
            If IM_Dt.Rows.Count > 0 Then
                FRM_GDX.Visible = True
                FRM_GDX.Size = New Point(FRM_GDX.Size.Width, 530)
            Else
                FRM_GDX.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IM_FRM_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_FRM_txt.KeyDown

        Select Case e.KeyCode
            Case Keys.Return
                Search_From_Grid()
            Case Keys.Down
                If FRM_GDX.Visible = True Then
                    FRM_GDX.Select()
                Else
                    QtyTextBox.Select()
                End If
            Case Keys.Left : Barcode_SH_txt.Select()
            Case Keys.Delete : IM_FRM_txt.Clear()
        End Select


    End Sub

    Private Sub Search_From_Grid()
        If FRM_GDX.Visible = True Then
            Fetch_ItemToList()
        Else
            QtyTextBox.Select()
        End If
    End Sub

    Private Sub FRM_GDX_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles FRM_GDX.CellClick
        Fetch_ItemToList()
    End Sub

    Private Sub FRM_GDX_KeyDown(sender As Object, e As KeyEventArgs) Handles FRM_GDX.KeyDown
        If e.KeyCode = Keys.Return Then Fetch_ItemToList()
        If e.KeyCode = Keys.Up Then If FRM_GDX.CurrentRow.Index = 0 Then IM_FRM_txt.Select()
    End Sub

    Private Sub Fetch_ItemToList()

        If FRM_GDX.Rows.Count > 0 Then
            FRM_IM_ID = FRM_GDX.CurrentRow.Cells("IM_ID_CL2").Value
            IM_FRM_txt.Text = FRM_GDX.CurrentRow.Cells("item_name_CL2").Value
            IM_FRM_txt.BackColor = Color.LightGoldenrodYellow
            Get_Unit = False
            Fetch_IM_Units()
            FRM_GDX.Visible = False
            IM_FRM_txt.Select()
        End If
    End Sub

    Private Sub Fetch_IM_Units()
        Get_Unit = False
        Dim c As New C
        U_Dt.Clear()
        Try
            Dim s As String
            s = "select U_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & FRM_IM_ID & "'"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(U_Dt)
            IM_Unit_cm.DataSource = U_Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Get_Unit = True
    End Sub

    Private Sub IM_Formating_Menu_insert()
        If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "IM_Struct_Menu_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", F_ItemsMenu.IM_ID)
            .Parameters.AddWithValue("@IM_U_ID", F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("U_IM_ID_CL").Value)
            .Parameters.AddWithValue("@Struct_IM_ID", FRM_IM_ID)
            .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
            .Parameters.AddWithValue("@Qty", QtyTextBox.Text)
            .Parameters.AddWithValue("@User_ID", USER_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            IM_Formating_Menu_Select()
            IM_FRM_txt.Clear()
        End If
    End Sub

    Public Sub IM_Formating_Menu_Select()
        Try
            FRM_Dt.Clear()
            Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "IM_Struct_Menu_SELECT"   
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@U_IM_ID", F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("U_IM_ID_CL").Value)
                C.Da = New SqlClient.SqlDataAdapter(C.Com)
                C.Da.Fill(FRM_Dt)
                IM_FRM_MENU_DV.DataSource = FRM_Dt

                For i = 0 To IM_FRM_MENU_DV.Rows.Count - 1
                    IM_FRM_MENU_DV.Rows(i).Cells("INDX_CL").Value = i + 1
                Next

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ADD_FRM_Btn_Click(sender As Object, e As EventArgs) Handles ADD_FRM_Btn.Click
        If FRM_IM_ID > 0 Then IM_Formating_Menu_insert()
    End Sub

    Private Sub REMOVE_FRM_Btn_Click(sender As Object, e As EventArgs) Handles REMOVE_FRM_Btn.Click
        If FRM_Dt.Rows.Count > 0 Then
            'And isCatch_IM = True Then
            If MessageBox.Show(" حذف تركيبة الصنف ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then IM_Formating_Menu_REMOVE()
        End If
    End Sub

    Private Sub IM_Formating_Menu_REMOVE()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "IM_Struct_Menu_REMOVE"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", IM_FRM_MENU_DV.CurrentRow.Cells("T_ID_CL").Value)
        End With

        If SQL_SP_EXEC(c.Com) = True Then IM_Formating_Menu_Select()

    End Sub

    Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown
        If e.KeyCode = Keys.Return Then ADD_FRM_Btn_Click(sender, e)
    End Sub


    Public Sub Load_IM_Barcode_frm()
        Dim c As New C
        IM_Dt.Clear()
        Try
            Dim s As String
            If Sh_ByNum_CB.Checked = True Then
                s = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_NUM = '" & Barcode_SH_txt.Text & "'"
            Else
                s = "select IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
            End If

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                FRM_IM_ID = c.Dr("IM_ID")
                IM_FRM_txt.Text = c.Dr("item_name")
                Get_Unit = False
                FRM_GDX.Visible = False
                QtyTextBox.Select()
                Fetch_IM_Units()
                ' If Sh_ByNum_CB.Checked = False Then IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
                Barcode_SH_txt.Clear()
            Else
                'If Barcode_SH_txt.Text.Count = 13 Then
                '    Check_If_Mizan()
                'Else
                MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
                'End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub IM_Struct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label.Text = " تركيبة الصنف : " & F_ItemsMenu.IM_Name_ToolStrip.Text & " - النوع : " & F_ItemsMenu.Unit_DataGridView.CurrentRow.Cells("U_Name_CL").Value
        IM_Formating_Menu_Select()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If FRM_GDX.Visible = True Then
            FRM_GDX.Visible = False
        Else
            FRM_GDX.Visible = True
            Fill_All_Frm_Compnents()
            FRM_GDX.Size = New Point(FRM_GDX.Size.Width, 430)
        End If
    End Sub

    Private Sub Fill_All_Frm_Compnents()
        Try
            Dim C As New C
            C.Dt.Clear()
            Dim s As String = "select IM_ID,item_name from IM_Active_V Order by item_name ASC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(C.Dt)
            FRM_GDX.DataSource = C.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class