Public Class ACC_CODE_NEW
    Private Sub is_Auto_Code_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Auto_Code_CB.CheckedChanged
        CB_CHecked(sender)
        ACC_CODE.Enabled = Not is_Auto_Code_CB.Checked
        ACC_PARENT_Txt.Visible = Not is_Auto_Code_CB.Checked
        If is_Auto_Code_CB.Checked = True Then
            Label_CODE.Text = "كود الحساب:"
            'Load_F_Tree_MAX_CODE()
            Generate_CODE()
        Else
            Label_CODE.Text = "كود الحساب فقط (بدون رقم الحساب الأب):"
            ACC_CODE.Clear()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click


        If is_Auto_Code_CB.Checked = False Then ACC_CODE.Text = ACC_PARENT_Txt.Text & ACC_CODE.Text

        If ValidateChildren() = True And CHECK_F_Tree_CODE() = False Then


            F_Tree.ACC_NAME.Text = ACC_NAME.Text
            F_Tree.T_ID_txt.Text = 0
            F_Tree.ACC_CODE.Text = ACC_CODE.Text

            F_Tree.SAVE_Button_Click(sender, e)


            'Load_F_Tree_MAX_CODE()
            Generate_CODE()

            ACC_NAME.Clear()
            ACC_NAME.Select()

        End If


    End Sub

    Private Sub ACC_CODE_NEW_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'F_Tree.ACC_PARENT_NEW = F_Tree.ACC_CODE.Text
        'ACC_PARENT_Txt.Text = F_Tree.ACC_PARENT_NEW

        'is_Auto_Code_CB.Checked = True
        'ACC_CODE.Enabled = False
        'Label_info.Text = "فتح حساب جديد تابع لـ / " & F_Tree.ACC_NAME.Text
        Generate_CODE()

        ACC_NAME.Select()
    End Sub

    Private Sub Generate_CODE()
        Dim parentAccount As String = ACC_PARENT_NEW     ' الحساب الأب
        Dim nextNumber As Integer = SELECT_COUNT_ACC_CODE() + 1       ' رقم التسلسل الفرعي الجديد
        Dim subLength As Integer = SELECT_ACC_DIGIT()      ' الطول المطلوب لكل مستوى
        Dim newAccount As String = GenerateSubAccountNumber(parentAccount, nextNumber, subLength)
        ACC_CODE.Text = newAccount
    End Sub


    Private Function SELECT_ACC_DIGIT()
        Dim C = New C
        Try
            Dim S As String = "SELECT  ISNULL(ACC_DIGIT,ACC_LEVEL) AS  ACC_DIGIT FROM  ACCOUNTS_Tree WHERE ACC_CODE = " & ACC_PARENT_NEW
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Return C.Dr("ACC_DIGIT")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 0
    End Function

    Private Function SELECT_COUNT_ACC_CODE()
        Dim C = New C
        Try
            Dim S As String = "SELECT  COUNT(T_ID) AS MX  FROM  ACCOUNTS_Tree WHERE ACC_PARENT = " & ACC_PARENT_NEW
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Return C.Dr("MX")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 0
    End Function


    Function GenerateSubAccountNumber(parentAccount As String, nextNumber As Integer, subLevelLength As Integer) As String
        ' توليد رقم الفرع بإضافة الرقم الجديد بعد إضافة أصفار على اليسار
        Dim nextSegment As String = nextNumber.ToString().PadLeft(subLevelLength, "0"c)
        Return parentAccount & nextSegment
    End Function


    Private Sub Load_F_Tree_MAX_CODE()
        Dim C = New C
        Try
            Dim S As String = "SELECT  CONCAT( " & ACC_PARENT_NEW & " , (SELECT COUNT(T_ID)+1))AS NEW_ACC_CODE  FROM  ACCOUNTS_Tree WHERE ACC_PARENT = " & ACC_PARENT_NEW
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ACC_CODE.Text = C.Dr("NEW_ACC_CODE")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Function CHECK_F_Tree_CODE()
        Dim C = New C
        Try
            Dim S As String = "SELECT  T_ID  FROM  ACCOUNTS_Tree WHERE ACC_CODE = " & ACC_CODE.Text
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                MsgBox(" كود الحساب تم مكرر ", MsgBoxStyle.Exclamation, "")
                Return True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 0
    End Function


    Private Sub ACC_CODE_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ACC_CODE.Validating

        If String.IsNullOrWhiteSpace(ACC_CODE.Text) = True Then
            ACC_CODE_ErrorProvider.SetError(ACC_CODE, " أدخل رقم الحساب ")
            ACC_CODE.Select()
            e.Cancel = True
        Else
            e.Cancel = False
            ACC_CODE_ErrorProvider.Clear()
        End If

    End Sub


    Private Sub ACC_NAME_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ACC_NAME.Validating

        If String.IsNullOrWhiteSpace(ACC_NAME.Text) = True Then
            ACC_NAME_ErrorProvider.SetError(ACC_NAME, " أدخل إسم الحساب ")
            ACC_NAME.Select()
            e.Cancel = True
        Else
            e.Cancel = False
            ACC_NAME_ErrorProvider.Clear()
        End If

    End Sub

    Private Sub ACC_CODE_TextChanged(sender As Object, e As EventArgs) Handles ACC_CODE.TextChanged
        ACC_CODE_ErrorProvider.Clear()
    End Sub

    Private Sub ACC_NAME_TextChanged(sender As Object, e As EventArgs) Handles ACC_NAME.TextChanged
        ACC_NAME_ErrorProvider.Clear()
    End Sub

    'Private Sub ACC_NAME_KeyDown(sender As Object, e As KeyEventArgs) Handles ACC_NAME.KeyDown
    '    If e.KeyCode = Keys.Return Then Button6_Click(sender, e)
    'End Sub

    Private Sub ACC_CODE_NEW_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Return Then Button6_Click(sender, e)
        If e.KeyCode = Keys.Escape Then Button4_Click(sender, e)
    End Sub
End Class