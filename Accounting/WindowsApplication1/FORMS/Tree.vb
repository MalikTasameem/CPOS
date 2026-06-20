Imports System.Data.SqlClient
Imports System
Imports System.Collections
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Text
Imports System.Windows.Forms
Imports PrintControl

Imports System.Threading.Tasks
Imports System.Text.RegularExpressions

Public Class Tree
    Dim Is_SELECT As Boolean = True
    Private WithEvents AccountTreePD As New System.Drawing.Printing.PrintDocument
    Private AccountTreePPD As New PrintPreviewDialog
    Private AccountTreePrintRows As New List(Of DataRow)
    Private AccountTreeCurrentRow As Integer = 0
    Private AccountTreePageNumber As Integer = 1
    Private AccountTreeTotalPages As Integer = 1

    Private Class AccountTreeNodeInfo
        Public Property Code As String
        Public Property Level As Integer
        Public Property SourceType As Integer
        Public Property IsLocked As Boolean

        Public Overrides Function ToString() As String
            Return Code
        End Function
    End Class

    'Public ACC_PARENT_NEW As Int16
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Income_ST_ACC_CODE_TXT.Text = MY_Settings.Income_ST_ACC_CODE
        ' ORG_ST_ACC_CODE_TXT.Text = MY_Settings.ORG_ST_ACC_CODE
        Pure_Income_ACC_CODE_TXT.Text = Identifiers.Pure_Income_ACC_CODE

        ApplyTreeViewUx()

        query("EXEC [dbo].[PREPARE_ACC_BALANCE] 0,NULL,NULL,0 ")
        CREATENODE()
        Load_Balances()
        Make_Hints()
    End Sub

    Private Sub Make_Hints()
        SendMessage(Search_By_Acc_Name_txt.Handle, &H1501, 0, "إبحث عن إسم حســـاب")
        SendMessage(Search_By_Acc_Code_txt.Handle, &H1501, 0, "إبحث عن رقـــم حســاب")
    End Sub

    Private Sub ApplyTreeViewUx()
        If TreeView1 Is Nothing Then Exit Sub

        TreeView1.BorderStyle = BorderStyle.None
        TreeView1.BackColor = Color.FromArgb(248, 250, 252)
        TreeView1.ForeColor = Color.FromArgb(30, 41, 59)
        TreeView1.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
        TreeView1.ItemHeight = 30
        TreeView1.Indent = 34
        TreeView1.HideSelection = False
        TreeView1.FullRowSelect = True
        TreeView1.ShowLines = False
        TreeView1.ShowPlusMinus = True
        TreeView1.ShowRootLines = False
        TreeView1.DrawMode = TreeViewDrawMode.OwnerDrawText
    End Sub

    Private Function CreateAccountTreeNode(row As DataRow) As TreeNode
        Dim code As String = GetAccountTreeValue(row, "ACC_CODE")
        Dim name As String = GetAccountTreeValue(row, "ACC_NAME")
        Dim node As New TreeNode(code & " : " & name)
        Dim level As Integer = GetAccountTreeLevel(row)

        node.Name = GetAccountTreeValue(row, "T_ID")
        node.Tag = New AccountTreeNodeInfo With {
            .Code = code,
            .Level = level,
            .SourceType = GetAccountSourceType(row),
            .IsLocked = GetAccountLockStatus(row)
        }
        node.NodeFont = GetTreeViewNodeFont(level)
        node.ForeColor = GetTreeViewNodeForeColor(level, GetAccountSourceType(row), GetAccountLockStatus(row))

        Return node
    End Function

    Private Function GetAccountSourceType(row As DataRow) As Integer
        If row Is Nothing OrElse Not row.Table.Columns.Contains("AccountSourceType") Then Return 0
        If row("AccountSourceType") Is Nothing OrElse IsDBNull(row("AccountSourceType")) Then Return 0

        Dim sourceType As Integer = 0
        Integer.TryParse(row("AccountSourceType").ToString(), sourceType)
        Return sourceType
    End Function

    Private Function GetAccountLockStatus(row As DataRow) As Boolean
        If row Is Nothing OrElse Not row.Table.Columns.Contains("is_Lock_Trans") Then Return False
        If row("is_Lock_Trans") Is Nothing OrElse IsDBNull(row("is_Lock_Trans")) Then Return False

        If TypeOf row("is_Lock_Trans") Is Boolean Then Return Convert.ToBoolean(row("is_Lock_Trans"))

        Dim lockValue As Integer = 0
        Integer.TryParse(row("is_Lock_Trans").ToString(), lockValue)
        Return lockValue = 1
    End Function

    Private Function GetTreeNodeInfo(node As TreeNode) As AccountTreeNodeInfo
        Dim info As AccountTreeNodeInfo = TryCast(node.Tag, AccountTreeNodeInfo)
        If info IsNot Nothing Then Return info

        Return New AccountTreeNodeInfo With {
            .Code = If(node.Tag Is Nothing, "", node.Tag.ToString()),
            .Level = Math.Max(node.Level + 1, 1),
            .SourceType = 0,
            .IsLocked = False
        }
    End Function

    Private Function GetTreeViewNodeFont(level As Integer) As Font
        Select Case level
            Case 1
                Return New Font("Segoe UI Semibold", 10.75!, FontStyle.Bold)
            Case 2
                Return New Font("Segoe UI Semibold", 10.25!, FontStyle.Bold)
            Case Else
                Return New Font("Segoe UI Semibold", 9.75!, FontStyle.Bold)
        End Select
    End Function

    Private Function GetTreeViewNodeForeColor(level As Integer, sourceType As Integer, isLocked As Boolean) As Color
        If isLocked Then Return Color.FromArgb(146, 64, 14)
        If sourceType = 1 Then Return Color.FromArgb(15, 118, 110)

        Select Case level
            Case 1
                Return Color.FromArgb(15, 23, 42)
            Case 2
                Return Color.FromArgb(22, 101, 52)
            Case 3
                Return Color.FromArgb(30, 64, 175)
            Case Else
                Return Color.FromArgb(51, 65, 85)
        End Select
    End Function


    Public Sub Load_Balances()
        Dim DT As New DataTable
        'Dim Con = New SqlConnection("Data Source= localhost ;initial catalog= Tree ;Integrated Security=True;")
        Dim C As New C
        Dim da As New SqlClient.SqlDataAdapter("select T_ID , O_NAME  from cash_flows_Card_Master ORDER BY T_ID ASC ", C.Con)
        da.Fill(DT)

        Cash_flows_CM.DataSource = DT
        Cash_flows_CM.DisplayMember = "O_NAME"
        Cash_flows_CM.ValueMember = "T_ID"

    End Sub


    Sub CREATENODE()
        Dim TRV As New TreeNode
        Dim DT As New DataTable
        DT.Clear()
        Dim C As New C
        'Dim Con = New SqlConnection("Data Source= localhost ;initial catalog= Tree ;Integrated Security=True;")
        EnsureAccountSourceTypeColumn()
        Dim da As New SqlClient.SqlDataAdapter("select ACC_CODE , ACC_NAME , T_ID, ACC_LEVEL, ISNULL(AccountSourceType, 0) AS AccountSourceType, ISNULL(is_Lock_Trans, 0) AS is_Lock_Trans from ACCOUNTS_TREE WHERE ACC_LEVEL = 1 ORDER BY ACC_CODE ASC", C.Con)
        da.Fill(DT)

        For I As Integer = 0 To DT.Rows.Count - 1
            TRV = CreateAccountTreeNode(DT.Rows(I))
            TreeView1.Nodes.Add(TRV)
        Next
    End Sub


    Dim T_ID As String
    'Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick

    '    'Try

    '    '    ACC_PARENT.Text = TreeView1.SelectedNode.Tag.ToString
    '    '    T_ID = TreeView1.SelectedNode.Name.ToString
    '    '    T_ID_txt.Text = TreeView1.SelectedNode.Name.ToString

    '    '    If TreeView1.SelectedNode.Nodes.Count = 0 Then
    '    '        Dim TRV As New TreeNode
    '    '        Dim DT As New DataTable
    '    '        DT.Clear()
    '    '        Dim Con = New SqlConnection("Data Source= localhost ;initial catalog= Tree ;Integrated Security=True;")
    '    '        Dim da As New SqlClient.SqlDataAdapter("select ACC_CODE , ACC_NAME , T_ID from ACCOUNTS_TREE_V WHERE ACC_PARENT = " & ACC_PARENT.Text, Con)
    '    '        da.Fill(DT)
    '    '        For I As Integer = 0 To DT.Rows.Count - 1
    '    '            TRV = New TreeNode(DT.Rows(I)(0).ToString() + " : " + DT.Rows(I)(1).ToString())
    '    '            TRV.Tag = DT.Rows(I)(0).ToString()
    '    '            TRV.Name = DT.Rows(I)(2).ToString()
    '    '            TreeView1.SelectedNode.Nodes.Add(TRV)
    '    '        Next
    '    '    End If
    '    '    SELECT_NODE_CONTENTS()
    '    'Catch ex As Exception
    '    '    MsgBox(ex.Message)
    '    'End Try

    'End Sub

    Public ACC_NAME_str As String
    Public Sub SELECT_NODE_CONTENTS()
        Side_CM.SelectedIndex = -1
        Dim C As New C
        'Dim ACC_PARENT_tmp As String = ""
        'Dim Con = New SqlConnection("Data Source= localhost ;initial catalog= Tree ;Integrated Security=True;")
        Dim Com = New SqlCommand("select *  from ACCOUNTS_TREE_V WHERE T_ID = " & T_ID, C.Con)
        'Dim C.Dr As SqlDataReader
        C.Con.Open()
        Try
            C.Dr = Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                ACC_CODE.Text = C.Dr("ACC_CODE")
                ACC_NAME.Text = C.Dr("ACC_NAME")
                ACC_NAME_str = C.Dr("ACC_NAME")
                ACC_PARENT.Text = C.Dr("ACC_PARENT")
                'ACC_TYPE.SelectedIndex = C.Dr("ACC_TYPE")
                'ACC_CAT.SelectedIndex = C.Dr("ACC_CAT")
                is_Balance_Sheet_CB.Checked = C.Dr("ACC_CLOSING")
                is_Balance_View_CB.Checked = C.Dr("is_Balance_View")
                ACC_LEVEL.Text = C.Dr("ACC_LEVEL")
                DEBIT.Text = C.Dr("DEBIT")
                CREDIT.Text = C.Dr("CREDIT")
                BALANCE.Text = C.Dr("BALANCE")
                'ACC_CODE_2.Text = C.Dr("ACC_HINT")
                parent_Label.Text = SELECT_NODE_Parent(C.Dr("ACC_PARENT"))

                If C.Dr("ACC_NATURAL") = "D" Then
                    ACC_NATURAL.SelectedIndex = 0
                Else
                    ACC_NATURAL.SelectedIndex = 1
                End If
                Cash_flows_CM.SelectedValue = C.Dr("cash_flow_ID")
                Side_CM.SelectedItem = C.Dr("SIDE")
                Acc_Current_Status_LB.Text = C.Dr("Acc_Current_Status")
                is_Lock_Trans_CB.Checked = C.Dr("is_Lock_Trans")
                ACC_DIGIT.Text = C.Dr("ACC_DIGIT")

                'ACC_PARENT.Text = ACC_PARENT_tmp

            End If
            'query("EXEC [dbo].[PREPARE_ACC_BALANCE] " & ACC_CODE.Text)
            'SELECT_NODE_BALANCE()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub


    Public Function SELECT_NODE_Parent(ACC_PARENT As String)
        Dim C As New C

        Dim S As String = "select [ACC_NAME]  from ACCOUNTS_TREE_V WHERE ACC_CODE = '" & ACC_PARENT & "' "
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                Return C.Dr("ACC_NAME")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return ""
    End Function


    'Public Sub SELECT_NODE_BALANCE()
    '    Dim C As New C
    '    'Dim Con = New SqlConnection("Data Source= localhost ;initial catalog= Tree ;Integrated Security=True;")
    '    Dim Com = New SqlCommand("select *  from ACCOUNTS_TREE_V WHERE T_ID = " & T_ID, C.Con)
    '    Dim Dr As SqlDataReader
    '    C.Con.Open()
    '    Try
    '        Dr = Com.ExecuteReader
    '        If Dr.HasRows = True Then
    '            Dr.Read()
    '            DEBIT.Text = Dr("DEBIT")
    '            CREDIT.Text = Dr("CREDIT")
    '            BALANCE.Text = Dr("BALANCE")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    C.Con.Close()
    'End Sub


    Public Sub SAVE_Button_Click(sender As Object, e As EventArgs)
        ' Dim Con = New SqlConnection("Data Source= localhost ;initial catalog= Tree ;Integrated Security=True;")
        ' Dim com = New SqlCommand("INSERT INTO [dbo].[ACCOUNTS_TREE]([T_ID],[ACC_CODE],[ACC_LEVEL],[ACC_PARENT],[ACC_NAME],[ACC_TYPE],[ACC_CAT],[ACC_CLOSING],[ACC_HINT]) " &
        '" VALUES( (SELECT MAX(T_ID)+1 FROM  ACCOUNTS_TREE), CONCAT( '" & ACC_CODE.Text & "' , (SELECT COUNT(T_ID)+1 FROM  ACCOUNTS_TREE WHERE ACC_PARENT = '" & ACC_CODE.Text & "')) " &
        '",'" & Convert.ToInt16(ACC_LEVEL.Text) + 1 & "','" & ACC_PARENT.Text & "','" & ACC_NAME.Text & "','" & ACC_TYPE.SelectedIndex & "','" & ACC_CAT.SelectedIndex & "','" & ACC_CLOSING.SelectedIndex & "','" & ACC_CODE_2.Text & "')", Con)
        ' Con.Open()
        ' com.ExecuteNonQuery()
        ' Con.Close()
        '------------------------------------------------------------------------------------------------------------------------
        ACCOUNTS_TREE_pros(0, "INSERT")
    End Sub

    Private Sub ACCOUNTS_TREE_pros(T_ID, Process)
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[ACCOUNTS_TREE_pros]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
            .Parameters.AddWithValue("@ACC_CODE", ACC_CODE.Text)
            .Parameters.AddWithValue("@ACC_LEVEL", Convert.ToInt16(ACC_LEVEL.Text) + 1)
            .Parameters.AddWithValue("@ACC_PARENT", ACC_PARENT_NEW)
            .Parameters.AddWithValue("@ACC_NAME", ACC_NAME.Text)
            .Parameters.AddWithValue("@ACC_TYPE", ACC_NATURAL.SelectedIndex)
            .Parameters.AddWithValue("@ACC_CAT", 0)
            .Parameters.AddWithValue("@ACC_CLOSING", is_Balance_Sheet_CB.Checked)
            .Parameters.AddWithValue("@is_Balance_View", is_Balance_View_CB.Checked)
            .Parameters.AddWithValue("@cash_flow_ID", Cash_flows_CM.SelectedValue)
            .Parameters.AddWithValue("@SIDE", Side_CM.Text)
            .Parameters.AddWithValue("@ACC_DIGIT", ACC_DIGIT.Text)

            If ACC_NATURAL.SelectedIndex = 0 Then
                .Parameters.AddWithValue("@ACC_NATURAL", "D")
            Else
                .Parameters.AddWithValue("@ACC_NATURAL", "C")
            End If
            .Parameters.AddWithValue("@Process", Process)
            .Parameters.AddWithValue("@is_Lock_Trans", is_Lock_Trans_CB.Checked)

            Try
                C.Con.Open()
                'C.Com.ExecuteNonQuery()
                T_ID = C.Com.ExecuteScalar()
                C.Con.Close()
                'MsgBox("تم التطبيق", MsgBoxStyle.Information, "")


                If Process = "INSERT" And TabControl1.SelectedIndex = 0 Then
                    Dim TRV = New TreeNode(ACC_CODE.Text + " : " + ACC_NAME.Text)
                    TRV.Tag = New AccountTreeNodeInfo With {
                        .Code = ACC_CODE.Text,
                        .Level = Math.Max(Convert.ToInt16(ACC_LEVEL.Text) + 1, 1),
                        .SourceType = 0,
                        .IsLocked = is_Lock_Trans_CB.Checked
                    }
                    TRV.Name = T_ID
                    TRV.NodeFont = GetTreeViewNodeFont(Math.Max(Convert.ToInt16(ACC_LEVEL.Text) + 1, 1))
                    TRV.ForeColor = GetTreeViewNodeForeColor(Math.Max(Convert.ToInt16(ACC_LEVEL.Text) + 1, 1), 0, is_Lock_Trans_CB.Checked)
                    TreeView1.SelectedNode.Nodes.Add(TRV)

                    Dim notification3 As New NotificationForm("إشعار", " تم إضافة الحساب ", "bottom")
                    notification3.ShowNotification()

                End If

                If Process = "DELETE" Then
                    TreeView1.Nodes.Remove(TreeView1.SelectedNode)

                    Dim notification3 As New NotificationForm("إشعار", " تم حذف الحساب ", "bottom")
                    notification3.ShowNotification()

                End If

                If Process = "UPDATE" Then
                    TreeView1.SelectedNode.Text = ACC_CODE.Text + " : " + ACC_NAME.Text
                    TreeView1.SelectedNode.Tag = New AccountTreeNodeInfo With {
                        .Code = ACC_CODE.Text,
                        .Level = Math.Max(Convert.ToInt16(ACC_LEVEL.Text), 1),
                        .SourceType = GetTreeNodeInfo(TreeView1.SelectedNode).SourceType,
                        .IsLocked = is_Lock_Trans_CB.Checked
                    }
                    TreeView1.SelectedNode.ForeColor = GetTreeViewNodeForeColor(Math.Max(Convert.ToInt16(ACC_LEVEL.Text), 1), GetTreeNodeInfo(TreeView1.SelectedNode).SourceType, is_Lock_Trans_CB.Checked)

                    Dim notification3 As New NotificationForm("إشعار", " تم تعديل الحساب ", "bottom")
                    notification3.ShowNotification()

                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End With
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect

        PREPARE_TO_SHOW(TreeView1.SelectedNode.Tag.ToString, TreeView1.SelectedNode.Name.ToString)

    End Sub

    Private Sub PREPARE_TO_SHOW(ACC_PARENT_ As String, ACC_T_ID As String)
        'ACC_PARENT.Text = TreeView1.SelectedNode.Tag.ToString
        'T_ID =          TreeView1.SelectedNode.Name.ToString
        'T_ID_txt.Text = TreeView1.SelectedNode.Name.ToString

        ACC_PARENT.Text = ACC_PARENT_
        T_ID = ACC_T_ID
        T_ID_txt.Text = ACC_T_ID

        SELECT_NODE_CONTENTS()
    End Sub

    Private Sub Fill_SubNodes()
        Try

            ACC_PARENT.Text = TreeView1.SelectedNode.Tag.ToString
            T_ID = TreeView1.SelectedNode.Name.ToString
            T_ID_txt.Text = TreeView1.SelectedNode.Name.ToString

            If TreeView1.SelectedNode.Nodes.Count = 0 Then
                Dim TRV As New TreeNode
                Dim DT As New DataTable
                DT.Clear()
                Dim C As New C
                'Dim Con = New SqlConnection("Data Source= localhost ;initial catalog= Tree ;Integrated Security=True;")
                EnsureAccountSourceTypeColumn()
                Dim da As New SqlClient.SqlDataAdapter("select ACC_CODE , ACC_NAME , T_ID , ACC_TYPE, ACC_LEVEL, ISNULL(AccountSourceType, 0) AS AccountSourceType, ISNULL(is_Lock_Trans, 0) AS is_Lock_Trans from ACCOUNTS_TREE WHERE ACC_PARENT = '" & ACC_PARENT.Text & "' ", C.Con)
                da.Fill(DT)
                For I As Integer = 0 To DT.Rows.Count - 1
                    TRV = CreateAccountTreeNode(DT.Rows(I))
                    TreeView1.SelectedNode.Nodes.Add(TRV)

                Next
            End If
            SELECT_NODE_CONTENTS()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub REMOVE_BTN_Click(sender As Object, e As EventArgs) Handles REMOVE_BTN.Click

        If ACC_LEVEL.Text = 0 Then
            MsgBox(" لا يمكن حذف حساب من المستوى الأول ", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        If CHECK_IF_CODE_HAS_PARENT() = True Then
            MsgBox("لا يمكن حذف حساب لديه حساب تابع", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If


        'Dim Con = New SqlConnection("Data Source= localhost ;initial catalog= Tree ;Integrated Security=True;")
        'Dim com = New SqlCommand("DELETE FROM [dbo].[ACCOUNTS_TREE] WHERE [ACC_CODE] =" & TreeView1.SelectedNode.Tag.ToString, Con)
        'Con.Open()
        'com.ExecuteNonQuery()
        'Con.Close()

        If MessageBox.Show("حذف الحساب " & ACC_NAME.Text & " بشكل نهائي ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
            ACCOUNTS_TREE_pros(T_ID_txt.Text, "DELETE")
            SELECT_NODE_CONTENTS()
        End If


    End Sub

    Public Function CHECK_IF_CODE_HAS_PARENT()
        Dim C As New C

        Dim S As String = "select [T_ID]  from ACCOUNTS_TREE WHERE ACC_PARENT = " & ACC_CODE.Text
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                Return True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return False
    End Function

    Private Function CheckIfAccountHasTransactions(accountCode As String) As Boolean
        If String.IsNullOrWhiteSpace(accountCode) Then Return False

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
SELECT TOP 1 1
FROM dbo.ACC_BALANCE
WHERE LTRIM(RTRIM(CONVERT(NVARCHAR(40), ACC_CODE))) = LTRIM(RTRIM(@ACC_CODE));", cn)

                cmd.Parameters.Add("@ACC_CODE", SqlDbType.NVarChar, 40).Value = accountCode.Trim()
                cn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                Return result IsNot Nothing AndAlso result IsNot DBNull.Value
            End Using
        End Using
    End Function

    'Private Sub Button3_Click(sender As Object, e As EventArgs)
    '    ACC_B.Show()
    'End Sub

    'Private Sub Button4_Click(sender As Object, e As EventArgs)
    '    Balances_Form.ShowDialog()
    'End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'F_Tree.ACC_PARENT_NEW = F_Tree.ACC_CODE.Text
        'ACC_PARENT_Txt.Text = F_Tree.ACC_PARENT_NEW

        'is_Auto_Code_CB.Checked = True
        'ACC_CODE.Enabled = False
        'Label_info.Text = "فتح حساب جديد تابع لـ / " & F_Tree.ACC_NAME.Text


        If String.IsNullOrWhiteSpace(ACC_CODE.Text) Then
            MsgBox("حدد الحساب الأصلي أولاً", MsgBoxStyle.Exclamation, "فتح حساب جديد")
            Exit Sub
        End If

        If CheckIfAccountHasTransactions(ACC_CODE.Text) Then
            MsgBox("لا يمكن فتح حساب فرعي تحت هذا الحساب لأنه يحتوي على حركة في القيود." & vbCrLf &
                   "رقم الحساب: " & ACC_CODE.Text & vbCrLf &
                   "اسم الحساب: " & ACC_NAME.Text, MsgBoxStyle.Exclamation, "فتح حساب جديد")
            Exit Sub
        End If

        Dim F As New ACC_CODE_NEW
        ACC_PARENT_NEW = ACC_CODE.Text
        F.ACC_PARENT_Txt.Text = ACC_PARENT_NEW
        F.is_Auto_Code_CB.Checked = True
        F.ACC_CODE.Enabled = False
        F.Label_info.Text = "فتح حساب جديد تابع لـ / " & ACC_NAME.Text
        F.ShowDialog()



        ' ACC_CODE_NEW.ShowDialog()
        'Dim inp = InputBox("أدخل اسم الحساب ", "فتح حساب جديد تابع لـ / " & ACC_NAME.Text)
        'If inp <> "" Then
        '    ACC_NAME.Text = inp
        '    T_ID_txt.Text = 0
        '    SAVE_Button_Click(sender, e)
        'End If

    End Sub

    'Private Sub Button7_Click(sender As Object, e As EventArgs)
    '    Dim F As New Normal_Form
    '    F.Form_Name = "COST_CENTER"
    '    F.Form_Name_Arabic = "مراكز التكلفة"
    '    F.F_ID = "COST_ID"
    '    F.F_Name = "COST_NAME"
    '    F.F_DETAILS = "COST_CENTER"

    '    F.Checked_Table = "ACC_BALANCE"
    '    F.Checked_Table_ID = "COST_ID"
    '    F.ShowDialog()
    'End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If MessageBox.Show(" تعديل الحســاب  " & ACC_NAME_str, "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
            ACCOUNTS_TREE_pros(T_ID_txt.Text, "UPDATE")
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Refresh_Btn.Click

        Dim selectedIndex As Integer = TabControl1.SelectedIndex
        query("EXEC [dbo].[PREPARE_ACC_BALANCE] 0 ")

        If selectedIndex = 0 Then
            TreeView1.Nodes.Clear()
            CREATENODE()
        Else
            ACCOUNTS_TREE_SELECT_TABLE()
        End If



    End Sub

    Dim DT As New DataTable

    Public Async Sub ACCOUNTS_TREE_SELECT_TABLE()
        DT = New DataTable

        DataB.Dispose()
        DataB = New BindingSource
        DataGridView1.DataSource = Nothing

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[ACCOUNTS_TREE_SELECT_TABLE]"
            .CommandType = CommandType.StoredProcedure
        End With

        CircularPanel.Visible = True
        CircularProgressControl1.Start()
        DT = Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr))

        DataB.DataSource = DT
        DataGridView1.DataSource = DataB

        CircularPanel.Visible = False
        CircularProgressControl1.Stop()

        DataGridView1.Columns(0).Visible = False

        DataGridView1.Columns("ACC_CODE_CL").ReadOnly = True
        DataGridView1.Columns("ACC_PARENT_CL").ReadOnly = True
        DataGridView1.Columns("ACC_LEVEL_CL").ReadOnly = True
        DataGridView1.Columns("SIDE_CL").ReadOnly = True

        ' حذف العمود الأصلي
        Dim colIndex As Integer = DataGridView1.Columns("SIDE_CL").Index
        DataGridView1.Columns.Remove("SIDE_CL")

        ' إنشاء ComboBox جديد بنفس الاسم
        Dim comboCol As New DataGridViewComboBoxColumn()
        comboCol.Name = "SIDE_CL"
        comboCol.HeaderText = "أصول/إلتزامات"
        comboCol.DataPropertyName = "SIDE" ' لربطه مع نفس العمود في DataTable
        comboCol.Items.AddRange("assets", "opponents", "REVENUE", "EXPENSE", "بلا")

        ' إدراجه في نفس المكان السابق
        DataGridView1.Columns.Insert(colIndex, comboCol)


    End Sub

    Private Sub Show_ALL_BTN_Click(sender As Object, e As EventArgs) Handles Show_ALL_BTN.Click
        Dim selectedIndex As Integer = TabControl1.SelectedIndex
        If selectedIndex = 0 Then
            LoadTreeView()
            'Is_SELECT = False
            'CallRecursive(TreeView1, "", "")
            'Is_SELECT = True

        Else
            ACCOUNTS_TREE_SELECT_TABLE()
        End If


    End Sub

    '-------------------------------------------------------------------------------------------------------<new fill nodes>


    ' هذا الكود يقوم بتحميل كل البيانات في TreeView بناءً على DataTable (Accounts_Datatable)
    Private Sub LoadTreeView()
        Try
            ' تنظيف الشجرة أولاً
            TreeView1.Nodes.Clear()

            ' إضافة الجذور (العقد التي ليس لها أب)
            Dim rootNodes As DataRow() = Accounts_Datatable.Select("ACC_PARENT = 0 ")

            For Each row As DataRow In rootNodes
                Dim rootNode As TreeNode = CreateAccountTreeNode(row)
                TreeView1.Nodes.Add(rootNode)

                ' استدعاء دالة تعبئة الفروع بشكل متكرر
                AddChildNodes(rootNode)
            Next

            TreeView1.ExpandAll() ' لتوسيع جميع العقد، اختياري
        Catch ex As Exception
            MsgBox("حدث خطأ أثناء تحميل الشجرة: " & ex.Message)
        End Try
    End Sub

    ' دالة تكرارية لإضافة الفروع لكل عقدة
    Private Sub AddChildNodes(parentNode As TreeNode)
        Try
            Dim parentCode As String = parentNode.Tag.ToString()

            Dim childRows As DataRow() = Accounts_Datatable.Select("ACC_PARENT = '" & parentCode & "'")

            For Each row As DataRow In childRows
                Dim childNode As TreeNode = CreateAccountTreeNode(row)

                parentNode.Nodes.Add(childNode)

                ' استدعاء الدالة نفسها للفروع التالية
                AddChildNodes(childNode)
            Next
        Catch ex As Exception
            MsgBox("حدث خطأ أثناء إضافة الفروع: " & ex.Message)
        End Try
    End Sub



    '-------------------------------------------------------------------------------------------------------</new fill nodes>





    Private Sub PrintRecursive(ByVal n As TreeNode, oldText As String, newText As String)
        System.Diagnostics.Debug.WriteLine(n.Text)
        TreeView1.SelectedNode = n
        Fill_SubNodes()
        'LoadTreeView()

        n.Expand()

        If String.Compare(n.Text, oldText, True) = 0 Then
            n.Text = newText
        End If
        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursive(aNode, oldText, newText)
        Next
    End Sub



    ' Call the procedure Using the top nodes Of the treeview.
    Private Sub CallRecursive(ByVal aTreeView As TreeView, oldText As String, newText As String)
        Dim n As TreeNode
        For Each n In aTreeView.Nodes
            PrintRecursive(n, oldText, newText)
        Next
    End Sub


    Private Sub View_Btn_Click(sender As Object, e As EventArgs) Handles View_Btn.Click
        Dim m_print As ControlPrint = New ControlPrint()
        m_print.StretchControl = True
        m_print.SetControl(TreeView1)
        m_print.PrintWidth = m_print.CalculateSize().Width
        m_print.PrintHeight = m_print.CalculateSize().Height
        PrintPreviewDialog1.Document = CType(m_print, PrintDocument)
        PrintPreviewDialog1.ShowDialog()

    End Sub


    Private Sub Print_Btn_Click(sender As Object, e As EventArgs) Handles Print_Btn.Click
        Dim m_print As ControlPrint = New ControlPrint(TreeView1, True)
        m_print.Print()
    End Sub

    Private Sub TreeView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TreeView1.MouseDoubleClick
        Fill_SubNodes()
    End Sub

    Private Sub TreeView1_DrawNode(sender As Object, e As DrawTreeNodeEventArgs) Handles TreeView1.DrawNode
        If e.Node Is Nothing Then Return

        Dim info As AccountTreeNodeInfo = GetTreeNodeInfo(e.Node)
        Dim isSelected As Boolean = (e.State And TreeNodeStates.Selected) = TreeNodeStates.Selected
        Dim textRect As Rectangle = e.Bounds
        textRect.Height = Math.Max(textRect.Height, TreeView1.ItemHeight - 2)
        textRect.Width = Math.Max(textRect.Width, TreeView1.ClientSize.Width - textRect.Left - 8)

        Dim backColor As Color = If(isSelected, Color.FromArgb(219, 234, 254), TreeView1.BackColor)
        If info.SourceType = 1 AndAlso Not isSelected Then backColor = Color.FromArgb(236, 253, 245)
        If info.IsLocked AndAlso Not isSelected Then backColor = Color.FromArgb(255, 251, 235)

        Using backBrush As New SolidBrush(backColor)
            e.Graphics.FillRectangle(backBrush, New Rectangle(0, textRect.Top, TreeView1.ClientSize.Width, textRect.Height))
        End Using

        DrawTreeNodeConnectors(e.Graphics, e.Node, textRect)

        Dim nodeFont As Font = If(e.Node.NodeFont, TreeView1.Font)
        Dim textColor As Color = If(isSelected, Color.FromArgb(15, 23, 42), GetTreeViewNodeForeColor(info.Level, info.SourceType, info.IsLocked))

        Dim textBounds As New Rectangle(textRect.Left + 4, textRect.Top, Math.Max(40, textRect.Width - 8), textRect.Height)
        If info.SourceType = 1 Then
            DrawTreeNodeBadge(e.Graphics, textBounds, "مبيعات", Color.FromArgb(15, 118, 110))
            textBounds.X += 62
            textBounds.Width = Math.Max(40, textBounds.Width - 62)
        End If

        If info.IsLocked Then
            DrawTreeNodeBadge(e.Graphics, textBounds, "مقفل", Color.FromArgb(217, 119, 6))
            textBounds.X += 54
            textBounds.Width = Math.Max(40, textBounds.Width - 54)
        End If

        TextRenderer.DrawText(
            e.Graphics,
            e.Node.Text,
            nodeFont,
            textBounds,
            textColor,
            TextFormatFlags.RightToLeft Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.EndEllipsis
        )

        If isSelected Then
            Using borderPen As New Pen(Color.FromArgb(37, 99, 235))
                e.Graphics.DrawRectangle(borderPen, New Rectangle(0, textRect.Top, TreeView1.ClientSize.Width - 1, textRect.Height - 1))
            End Using
        End If
    End Sub

    Private Sub DrawTreeNodeBadge(g As Graphics, bounds As Rectangle, badgeText As String, badgeColor As Color)
        Dim badgeWidth As Integer = If(badgeText = "مبيعات", 54, 46)
        Dim badgeRect As New Rectangle(bounds.Left, bounds.Top + 5, badgeWidth, bounds.Height - 10)

        Using badgeBack As New SolidBrush(badgeColor)
            g.FillRectangle(badgeBack, badgeRect)
        End Using

        TextRenderer.DrawText(g, badgeText, New Font("Segoe UI Semibold", 8.0!, FontStyle.Bold), badgeRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.RightToLeft)
    End Sub

    Private Sub DrawTreeNodeConnectors(g As Graphics, node As TreeNode, rowRect As Rectangle)
        If node Is Nothing Then Return

        Dim centerY As Integer = rowRect.Top + (rowRect.Height \ 2)
        Dim connectorColor As Color = Color.FromArgb(148, 163, 184)

        Using connectorPen As New Pen(connectorColor, 1.0F)
            connectorPen.DashStyle = Drawing2D.DashStyle.Dot

            Dim currentConnectorX As Integer = Math.Max(8, node.Bounds.Left - 15)

            If node.Parent IsNot Nothing Then
                Dim verticalTop As Integer = rowRect.Top
                Dim verticalBottom As Integer = If(HasNextSibling(node), rowRect.Bottom, centerY)
                g.DrawLine(connectorPen, currentConnectorX, verticalTop, currentConnectorX, verticalBottom)
                g.DrawLine(connectorPen, currentConnectorX, centerY, Math.Max(currentConnectorX, node.Bounds.Left - 3), centerY)
            End If

            Dim ancestor As TreeNode = node.Parent
            Dim ancestorConnectorX As Integer = currentConnectorX - TreeView1.Indent

            While ancestor IsNot Nothing
                If HasNextSibling(ancestor) Then
                    g.DrawLine(connectorPen, ancestorConnectorX, rowRect.Top, ancestorConnectorX, rowRect.Bottom)
                End If

                ancestor = ancestor.Parent
                ancestorConnectorX -= TreeView1.Indent
            End While
        End Using
    End Sub

    Private Function HasNextSibling(node As TreeNode) As Boolean
        If node Is Nothing Then Return False

        If node.Parent Is Nothing Then
            Return node.Index < TreeView1.Nodes.Count - 1
        End If

        Return node.Index < node.Parent.Nodes.Count - 1
    End Function

    Private Sub is_Balance_Sheet_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Balance_Sheet_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub is_Balance_View_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Balance_View_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    'Private Sub is_Income_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    'End Sub

    'Private Sub ST_EDIT_Bton_Click(sender As Object, e As EventArgs) 
    '    query("UPDATE SYS_Features SET Pure_Income_ACC_CODE = " & Pure_Income_ACC_CODE_TXT.Text)
    '    Identifiers.Pure_Income_ACC_CODE = Pure_Income_ACC_CODE_TXT.Text
    'End Sub

    Private Sub Tree_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        LOAD_ALL_TABLES()
    End Sub

    Private Sub Print_Table_Btn_Click(sender As Object, e As EventArgs) Handles Print_Table_Btn.Click
        Print_B()
    End Sub

    Public Sub Print_B()
        Try
            PrepareAccountTreePrint()

            If AccountTreePrintRows.Count = 0 Then
                MessageBox.Show("لا توجد بيانات للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            AccountTreePPD.Document = AccountTreePD
            AccountTreePPD.WindowState = FormWindowState.Maximized
            AccountTreePPD.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء طباعة دليل الحسابات: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrepareAccountTreePrint()
        AccountTreeCurrentRow = 0
        AccountTreePageNumber = 1
        AccountTreeTotalPages = 1
        AccountTreePrintRows.Clear()

        AccountTreePD.DefaultPageSettings.Landscape = False
        AccountTreePD.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(25, 25, 30, 35)

        Dim sourceTable As DataTable = GetAccountTreePrintTable()

        For Each row As DataRow In sourceTable.Rows
            AccountTreePrintRows.Add(row)
        Next

        AccountTreeTotalPages = EstimateAccountTreeTotalPages()
    End Sub

    Private Function GetAccountTreePrintTable() As DataTable
        If DT IsNot Nothing AndAlso DT.Rows.Count > 0 AndAlso DT.Columns.Contains("ACC_CODE") AndAlso DT.Columns.Contains("ACC_NAME") Then
            Return DT.DefaultView.ToTable()
        End If

        Dim result As New DataTable

        Try
            Dim C As New C
            Using da As New SqlDataAdapter("SELECT T_ID, ACC_CODE, ACC_NAME, ACC_PARENT, ACC_LEVEL FROM ACCOUNTS_TREE ORDER BY ACC_CODE ASC", C.Con)
                da.Fill(result)
            End Using
        Catch ex As Exception
            MessageBox.Show("تعذر تحميل دليل الحسابات للطباعة: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return result
    End Function

    Private Sub AccountTreePD_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles AccountTreePD.PrintPage
        Dim g = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim marginLeft As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right
        Dim y As Integer = e.MarginBounds.Top
        Dim pageWidth As Integer = e.MarginBounds.Width
        Dim footerReserve As Integer = 46

        Dim companyFontAr As New Font("Tahoma", 12, FontStyle.Bold)
        Dim companyFontEn As New Font("Tahoma", 11, FontStyle.Bold)
        Dim titleFont As New Font("Tahoma", 14, FontStyle.Bold)
        Dim subTitleFont As New Font("Tahoma", 9.5!, FontStyle.Bold)
        Dim headerFont As New Font("Tahoma", 9.5!, FontStyle.Bold)
        Dim footerFont As New Font("Tahoma", 8.0!, FontStyle.Regular)

        Dim sfRight As New StringFormat With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        Dim sfCenter As New StringFormat With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        Dim sfLeft As New StringFormat With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center
        }

        DrawAccountTreeReportHeader(g, marginLeft, marginRight, y, pageWidth, companyFontAr, companyFontEn, titleFont, subTitleFont, sfRight, sfCenter, sfLeft)
        y += AccountTreeHeaderHeight()

        Dim colWidths = GetAccountTreeColumnWidths(pageWidth)
        DrawAccountTreeTableHeader(g, marginLeft, y, colWidths, headerFont, sfCenter)
        y += 30

        While AccountTreeCurrentRow < AccountTreePrintRows.Count
            Dim row As DataRow = AccountTreePrintRows(AccountTreeCurrentRow)
            Dim rowHeight As Integer = EstimateAccountTreeRowHeight(row)

            If y + rowHeight > e.MarginBounds.Bottom - footerReserve Then
                DrawAccountTreeFooter(g, e.MarginBounds, footerFont, sfRight, sfCenter, sfLeft)
                e.HasMorePages = True
                AccountTreePageNumber += 1
                Return
            End If

            DrawAccountTreeRow(g, row, marginLeft, y, rowHeight, colWidths, sfCenter, sfRight)
            y += rowHeight
            AccountTreeCurrentRow += 1
        End While

        DrawAccountTreeFooter(g, e.MarginBounds, footerFont, sfRight, sfCenter, sfLeft)

        e.HasMorePages = False
        AccountTreeCurrentRow = 0
        AccountTreePageNumber = 1
    End Sub

    Private Sub DrawAccountTreeReportHeader(g As Graphics, marginLeft As Integer, marginRight As Integer, y As Integer, pageWidth As Integer, companyFontAr As Font, companyFontEn As Font, titleFont As Font, subTitleFont As Font, sfRight As StringFormat, sfCenter As StringFormat, sfLeft As StringFormat)
        g.DrawString(MY_Settings.SBill_Title_1, companyFontAr, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 24), sfRight)
        y += 26
        g.DrawString(MY_Settings.SBill_Title_2, companyFontEn, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfRight)
        y += 26
        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 8

        g.DrawString("دليل الحسابات", titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 28), sfCenter)
        y += 30
        g.DrawString("تاريخ الطباعة: " & Date.Now.ToString("dd/MM/yyyy HH:mm") & "     إجمالي الحسابات: " & AccountTreePrintRows.Count.ToString(), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfCenter)
        y += 24
        g.DrawString("صفحة " & AccountTreePageNumber.ToString() & " من " & AccountTreeTotalPages.ToString(), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y - 2, pageWidth, 18), sfLeft)
    End Sub

    Private Function AccountTreeHeaderHeight() As Integer
        Return 134
    End Function

    Private Function GetAccountTreeColumnWidths(pageWidth As Integer) As Integer()
        Return {
            CInt(pageWidth * 0.23),
            pageWidth - CInt(pageWidth * 0.23)
        }
    End Function

    Private Function TotalColumnWidth(colWidths As Integer()) As Integer
        Dim total As Integer = 0

        For Each w As Integer In colWidths
            total += w
        Next

        Return total
    End Function

    Private Sub DrawAccountTreeTableHeader(g As Graphics, x As Integer, y As Integer, colWidths As Integer(), headerFont As Font, sfCenter As StringFormat)
        Dim headers() As String = {"رقم الحساب", "بيان الحساب"}
        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

        For i As Integer = 0 To headers.Length - 1
            currentX -= colWidths(i)
            Dim rect As New Rectangle(currentX, y, colWidths(i), 30)
            g.FillRectangle(New SolidBrush(Color.FromArgb(223, 238, 232)), rect)
            g.DrawRectangle(New Pen(Color.FromArgb(90, 90, 90)), rect)
            g.DrawString(headers(i), headerFont, Brushes.Black, New RectangleF(rect.X + 4, rect.Y, rect.Width - 8, rect.Height), sfCenter)
        Next
    End Sub

    Private Sub DrawAccountTreeRow(g As Graphics, row As DataRow, x As Integer, y As Integer, rowHeight As Integer, colWidths As Integer(), sfCenter As StringFormat, sfRight As StringFormat)
        Dim level As Integer = GetAccountTreeLevel(row)
        Dim styleFont As Font = GetAccountTreeLevelFont(level)
        Dim styleBrush As Brush = GetAccountTreeLevelBrush(level)
        Dim fillColor As Color = GetAccountTreeLevelBackColor(level)
        Dim values() As String = {GetAccountTreeValue(row, "ACC_CODE"), GetAccountTreeValue(row, "ACC_NAME")}
        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

        For i As Integer = 0 To values.Length - 1
            currentX -= colWidths(i)
            Dim rect As New Rectangle(currentX, y, colWidths(i), rowHeight)

            If fillColor <> Color.Transparent Then
                g.FillRectangle(New SolidBrush(fillColor), rect)
            ElseIf AccountTreeCurrentRow Mod 2 = 1 Then
                g.FillRectangle(New SolidBrush(Color.FromArgb(250, 250, 250)), rect)
            End If

            g.DrawRectangle(New Pen(Color.FromArgb(160, 160, 160)), rect)

            If i = 0 Then
                g.DrawString(values(i), styleFont, styleBrush, New RectangleF(rect.X + 4, rect.Y + 2, rect.Width - 8, rect.Height - 4), sfCenter)
            Else
                Dim rightIndent As Integer = 8 + (Math.Max(level, 1) - 1) * 14
                g.DrawString(values(i), styleFont, styleBrush, New RectangleF(rect.X + 4, rect.Y + 2, rect.Width - rightIndent - 8, rect.Height - 4), sfRight)
            End If
        Next
    End Sub

    Private Function GetAccountTreeLevelFont(level As Integer) As Font
        Select Case level
            Case 1
                Return New Font("Tahoma", 11.5!, FontStyle.Bold)
            Case 2
                Return New Font("Tahoma", 10.5!, FontStyle.Bold)
            Case 3
                Return New Font("Tahoma", 9.5!, FontStyle.Bold)
            Case 4
                Return New Font("Tahoma", 8.8!, FontStyle.Regular)
            Case Else
                Return New Font("Tahoma", 8.2!, FontStyle.Regular)
        End Select
    End Function

    Private Function GetAccountTreeLevelBrush(level As Integer) As Brush
        Select Case level
            Case 1
                Return New SolidBrush(Color.FromArgb(25, 45, 85))
            Case 2
                Return New SolidBrush(Color.FromArgb(35, 95, 70))
            Case 3
                Return Brushes.Black
            Case 4
                Return New SolidBrush(Color.FromArgb(70, 70, 70))
            Case Else
                Return New SolidBrush(Color.FromArgb(105, 105, 105))
        End Select
    End Function

    Private Function GetAccountTreeLevelBackColor(level As Integer) As Color
        Select Case level
            Case 1
                Return Color.FromArgb(232, 239, 249)
            Case 2
                Return Color.FromArgb(238, 246, 241)
            Case Else
                Return Color.Transparent
        End Select
    End Function

    Private Function EstimateAccountTreeRowHeight(row As DataRow) As Integer
        Dim level As Integer = GetAccountTreeLevel(row)
        Select Case level
            Case 1
                Return 29
            Case 2
                Return 27
            Case 3
                Return 25
            Case Else
                Return 23
        End Select
    End Function

    Private Function EstimateAccountTreeTotalPages() As Integer
        Dim pageHeight As Integer = AccountTreePD.DefaultPageSettings.Bounds.Height - AccountTreePD.DefaultPageSettings.Margins.Top - AccountTreePD.DefaultPageSettings.Margins.Bottom
        Dim usableHeight As Integer = pageHeight - AccountTreeHeaderHeight() - 30 - 46
        Dim y As Integer = 0
        Dim pages As Integer = 1

        For Each row As DataRow In AccountTreePrintRows
            Dim h As Integer = EstimateAccountTreeRowHeight(row)

            If y + h > usableHeight Then
                pages += 1
                y = 0
            End If

            y += h
        Next

        Return Math.Max(pages, 1)
    End Function

    Private Sub DrawAccountTreeFooter(g As Graphics, marginBounds As Rectangle, footerFont As Font, sfRight As StringFormat, sfCenter As StringFormat, sfLeft As StringFormat)
        Dim footerY As Integer = marginBounds.Bottom - 34
        Dim pageWidth As Integer = marginBounds.Width
        Dim boxWidth As Integer = CInt(pageWidth / 4)
        Dim titles() As String = {
            "إعداد التقرير: " & USER_NAME,
            "إجمالي الحسابات: " & AccountTreePrintRows.Count.ToString(),
            "Page " & AccountTreePageNumber.ToString() & " of " & AccountTreeTotalPages.ToString(),
            Date.Now.ToString("dd/MM/yyyy HH:mm")
        }

        g.DrawLine(New Pen(Color.FromArgb(120, 120, 120)), marginBounds.Left, footerY - 6, marginBounds.Right, footerY - 6)

        For i As Integer = 0 To titles.Length - 1
            Dim rect As New Rectangle(marginBounds.Left + (i * boxWidth), footerY, boxWidth, 26)
            Dim fmt As StringFormat = If(i = 0, sfRight, If(i = 2, sfCenter, sfLeft))
            g.DrawString(titles(i), footerFont, Brushes.DimGray, New RectangleF(rect.X + 3, rect.Y, rect.Width - 6, rect.Height), fmt)
        Next
    End Sub

    Private Function GetAccountTreeLevel(row As DataRow) As Integer
        Dim levelText As String = GetAccountTreeValue(row, "ACC_LEVEL")
        Dim level As Integer = 1
        Integer.TryParse(levelText, level)
        If level < 1 Then level = 1
        Return level
    End Function

    Private Function GetAccountTreeValue(row As DataRow, columnName As String) As String
        If row Is Nothing OrElse Not row.Table.Columns.Contains(columnName) Then Return ""
        If row(columnName) Is Nothing OrElse IsDBNull(row(columnName)) Then Return ""
        Return row(columnName).ToString()
    End Function

    Private Sub Search_By_Acc_Name_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Name_txt.TextChanged
        Dim Dv As DataView
        Dv = DT.AsDataView
        Dv.RowFilter = IM_Serach(Search_By_Acc_Name_txt.Text, "[ACC_NAME]")
        DataGridView1.DataSource = Dv
    End Sub

    Private Sub Search_By_Acc_Code_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Code_txt.TextChanged
        Dim Dv As DataView
        Dv = DT.AsDataView
        Dv.RowFilter = IM_Serach(Search_By_Acc_Code_txt.Text, "[ACC_CODE]")
        DataGridView1.DataSource = Dv
    End Sub

    Private Sub is_Lock_Trans_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Lock_Trans_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub


    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If e.RowIndex >= 0 Then

            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            ' مثال: نفترض أن أول عمود هو ID (مفتاح رئيسي) لا يتغير
            Dim id As Integer = Convert.ToInt32(row.Cells("T_ID_CL").Value)
            Dim columnName As String = DataGridView1.Columns(e.ColumnIndex).Name
            Dim newValue As Object = row.Cells(e.ColumnIndex).Value

            Text = Regex.Replace(columnName, "_CL$", "")

            Dim C As New C
            Try

                Dim sql As String = $"UPDATE [ACCOUNTS_TREE] SET {Text} = @value WHERE T_ID = @id"
                Using Com As New SqlCommand(sql, C.Con)
                    Com.Parameters.AddWithValue("@value", newValue)
                    Com.Parameters.AddWithValue("@id", id)

                    C.Con.Open()
                    Com.ExecuteNonQuery()
                    C.Con.Close()
                End Using
            Catch ex As Exception
                MessageBox.Show("خطأ في التحديث: " & ex.Message)
                C.Con.Close()
            End Try
        End If
    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        PREPARE_TO_SHOW(DataGridView1.CurrentRow.Cells("ACC_CODE_CL").Value, DataGridView1.CurrentRow.Cells("T_ID_CL").Value)


        '' التحقق من أن النقر تم على صف صالح
        'If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
        '    Dim columnName As String = DataGridView1.Columns(e.ColumnIndex).Name

        '    ' التحقق من اسم العمود
        '    If columnName = "SIDE" Then
        '        ' تمييز الخلية مثلاً بتغيير لونها
        '        'DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Yellow

        '        ' أو تنفيذ كود معين، مثلاً عرض رسالة
        '        Dim value = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        '        MessageBox.Show("تم النقر على راتب الموظف: " & value.ToString())
        '    End If
        'End If
    End Sub


    '-----------------------------------------------------------------------------------------------------------------------




End Class
