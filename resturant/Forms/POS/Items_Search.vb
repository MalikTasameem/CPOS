Imports System.Data.SqlClient

Public Class Items_Search

    Dim IM_DT As New DataTable
    Private ReadOnly ItemsSearchCacheFolder As String = IO.Path.Combine(Application.StartupPath, "DraftSales\Cache")
    Private ReadOnly ItemsSearchCacheFilePath As String = IO.Path.Combine(Application.StartupPath, "DraftSales\Cache\ItemsSearch.xml")
    Private LastItemsLoadError As String = ""
    Private CurrentSearchColumn As String = "item_name"
    Private KeyboardInputHandlerAttached As Boolean = False


    Private Shared _instance As Items_Search = Nothing

    Public Shared Function GetInstance() As Items_Search
        If _instance Is Nothing OrElse _instance.IsDisposed Then
            _instance = New Items_Search()
        End If

        Return _instance
    End Function

    Public Sub New()
        InitializeComponent()
        WireKeyboardInputHandler()
    End Sub

    Private Sub WireKeyboardInputHandler()
        If KB Is Nothing OrElse KeyboardInputHandlerAttached Then Return

        RemoveHandler KB.UC_Button1Click, AddressOf HandleKeyboardInput
        AddHandler KB.UC_Button1Click, AddressOf HandleKeyboardInput
        KeyboardInputHandlerAttached = True
    End Sub

    Private Sub HandleKeyboardInput(sender As Object, e As EventArgs)
        Try
            Dim bt As Button = DirectCast(sender, Button)

            txtSearch.Focus()

            Select Case bt.Name.ToUpper()

                Case "BCLEAR"
                    txtSearch.Clear()

                Case "BSPACE"
                    txtSearch.SelectedText = " "

                Case "BBACK", "BDELETE", "BBACKSPACE"
                    If txtSearch.TextLength > 0 Then
                        txtSearch.Text = txtSearch.Text.Substring(0, txtSearch.TextLength - 1)
                        txtSearch.SelectionStart = txtSearch.TextLength
                    End If

                Case "BENTER"
                    Fetch_ItemToList()

                Case "BLANG"
                ' لا نفعل شيئًا هنا لأن الفورم نفسه يتولى تبديل اللغة

                Case "BCAPS"
                    ' لاحقًا إذا أردت نضيف منطق الحروف الكبيرة

                Case Else
                    txtSearch.SelectedText = bt.Text
            End Select

            txtSearch.SelectionStart = txtSearch.TextLength

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub STORES_Explorer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        IM_Search_GM_ID = GM_Serach.SelectedValue
        Save_AppSetting()
        'Me.Dispose()
    End Sub


    Private Sub STORES_Explorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadItemsSearchCache()
        fetch_GM()
        GM_Serach.SelectedValue = IM_Search_GM_ID
        GLOBAL_IM_ID = 0
        Load_ALL_IM()

        ADD_NewGM_Btn.Visible = U_ADD_Pch

        SetSearchMode("item_name", btnSearchName)

        'InitKeyboard()
    End Sub
    Private Sub Items_Search_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If Char.IsControl(e.KeyChar) Then Exit Sub

        If txtSearch.Focused Then Exit Sub

        If TypeOf Me.ActiveControl Is TextBox Then Exit Sub
        If TypeOf Me.ActiveControl Is ComboBox AndAlso CType(Me.ActiveControl, ComboBox).DroppedDown Then Exit Sub

        txtSearch.Focus()
        txtSearch.SelectionStart = txtSearch.TextLength
        txtSearch.SelectedText = e.KeyChar
        e.Handled = True
    End Sub

    Private Sub Items_Search_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Return Then Fetch_ItemToList()
    End Sub



    Public Sub fetch_GM()
        Dim groups As List(Of MailItem) = Nothing

        Try
            groups = GetMailItems()
        Catch
            groups = BuildGroupsFromCachedItems()
        End Try

        If groups Is Nothing OrElse groups.Count <= 1 Then groups = BuildGroupsFromCachedItems()

        GM_Serach.DataSource = groups
        GM_Serach.DisplayMember = "name"
        GM_Serach.ValueMember = "ID"
    End Sub

    Private Function BuildGroupsFromCachedItems() As List(Of MailItem)
        Dim groups As New List(Of MailItem)()
        groups.Add(New MailItem(0, "-----كل التصنيفات-----"))

        If IM_DT Is Nothing OrElse
           IM_DT.Columns.Contains("GM_ID") = False OrElse
           IM_DT.Columns.Contains("GM_NAME") = False Then Return groups

        Dim groupsTable As DataTable = IM_DT.DefaultView.ToTable(True, "GM_ID", "GM_NAME")
        Dim groupsView As New DataView(groupsTable)
        groupsView.Sort = "GM_NAME ASC"

        For Each rowView As DataRowView In groupsView
            Dim groupId As Integer = 0
            Integer.TryParse(rowView("GM_ID").ToString(), groupId)
            If groupId > 0 Then groups.Add(New MailItem(groupId, rowView("GM_NAME").ToString()))
        Next

        Return groups
    End Function

    Public Sub Load_IM(Search_Column As String, SearchText As String)
        Try
            Dim Dv As DataView = IM_DT.AsDataView
            Dim filters As New List(Of String)()

            If GM_Serach.SelectedValue IsNot Nothing AndAlso
               Val(GM_Serach.SelectedValue) > 0 AndAlso
               IM_DT.Columns.Contains("GM_ID") Then
                filters.Add("GM_ID = " & Convert.ToInt32(GM_Serach.SelectedValue).ToString())
            End If

            If String.IsNullOrWhiteSpace(SearchText) Then
                Dv.RowFilter = String.Join(" AND ", filters.ToArray())
                IMDataGridViewX.DataSource = Dv
                Exit Sub
            End If

            filters.Add("(" & IM_Serach(SearchText.Trim(), "[" & Search_Column & "]") & ")")
            Dv.RowFilter = String.Join(" AND ", filters.ToArray())
            IMDataGridViewX.DataSource = Dv

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function IM_Serach(IM_SH As String, _SearchColumn As String) As String
        Dim words As String() = IM_SH.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and " & _SearchColumn & " Like "

        ' حماية من العلامات الخاصة
        IM_SH = IM_SH.Replace("'", "''")

        If words.Length = 0 Then
            Return ""
        End If

        If words.Length = 1 Then
            Dim w As String = words(0).Replace("'", "''")
            Str = _SearchColumn & " = '" & IM_SH & "' OR " &
                  _SearchColumn & " LIKE '%" & w & "%' OR " &
                  _SearchColumn & " LIKE '%" & IM_SH & "' OR " &
                  _SearchColumn & " LIKE '" & IM_SH & "%'"
        Else
            IM_Str = "'%" & words(0).Replace("'", "''") & "%'" & S_and

            For i As Integer = 1 To words.Length - 1
                Dim wordSafe As String = words(i).Replace("'", "''")

                If i = words.Length - 1 Then
                    IM_Str += "'%" & wordSafe & "%'"
                Else
                    IM_Str += "'%" & wordSafe & "%'" & S_and
                End If
            Next

            Str = _SearchColumn & " LIKE " & IM_Str
        End If

        Return Str
    End Function

    Private Sub Search_From_Grid()
        If IMDataGridViewX.RowCount > 0 Then
            Fetch_ItemToList()
        End If
    End Sub

    Private Sub ExecuteSearch()
        Load_IM(CurrentSearchColumn, txtSearch.Text)
        txtSearch.Focus()
    End Sub

    Private Sub Fetch_ItemToList()
        If IMDataGridViewX.Rows.Count > 0 Then
            GLOBAL_IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
            'Me.Close()
            Me.Hide()
        End If
    End Sub

    Private Sub Load_ALL_IM()
        Dim c As New C
        Dim loadedItems As New DataTable()
        LastItemsLoadError = ""

        Try
            Dim s As String
            If SB_Sch_With_QTY = False Then
                s = "select IM_ID,ISNULL(GM_ID,0) AS GM_ID,ISNULL(GM_NAME,'') AS GM_NAME,Barcode,IM_NUM,item_name,U_Name,QTY,isValid,Price from IM_All_V Order by item_name ASC"
            Else
                s = "select IM_ID,ISNULL(GM_ID,0) AS GM_ID,ISNULL(GM_NAME,'') AS GM_NAME,Barcode,IM_NUM,item_name,U_Name,QTY,isValid,Price from IM_All_V_With_QTY Order by item_name ASC"
            End If

            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(loadedItems)

            If loadedItems.Rows.Count = 0 Then Throw New DataException("لم يُرجع تحميل الأصناف أي بيانات.")
            If HasRequiredItemsColumns(loadedItems) = False Then Throw New DataException("بيانات بحث الأصناف لا تحتوي على الأعمدة المطلوبة.")

            IM_DT = loadedItems

            Try
                SaveItemsSearchCache(IM_DT)
            Catch cacheEx As Exception
                LastItemsLoadError = "تم تحميل الأصناف، لكن تعذر تحديث الكاش المحلي: " & cacheEx.Message
            End Try

            ApplyItemsSourceFilter()

        Catch ex As Exception
            LastItemsLoadError = ex.Message

            If IM_DT Is Nothing OrElse IM_DT.Rows.Count = 0 Then
                LoadItemsSearchCache()
            End If

            ApplyItemsSourceFilter()
        End Try
    End Sub

    Private Sub ApplyItemsSourceFilter()
        If IM_DT Is Nothing Then Return
        Load_IM(CurrentSearchColumn, txtSearch.Text.Trim())
    End Sub

    Private Function HasRequiredItemsColumns(table As DataTable) As Boolean
        If table Is Nothing Then Return False

        For Each columnName As String In New String() {"IM_ID", "GM_ID", "GM_NAME", "Barcode", "IM_NUM", "item_name", "U_Name", "QTY", "isValid", "Price"}
            If table.Columns.Contains(columnName) = False Then Return False
        Next

        Return True
    End Function

    Private Function LoadItemsSearchCache() As Boolean
        Try
            If IO.File.Exists(ItemsSearchCacheFilePath) = False Then Return False

            Dim cachedItems As New DataTable()
            cachedItems.ReadXml(ItemsSearchCacheFilePath)

            If cachedItems.Rows.Count = 0 OrElse HasRequiredItemsColumns(cachedItems) = False Then Return False

            IM_DT = cachedItems
            ApplyItemsSourceFilter()
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Sub SaveItemsSearchCache(sourceTable As DataTable)
        If sourceTable Is Nothing OrElse sourceTable.Rows.Count = 0 Then Return

        Dim tempPath As String = ItemsSearchCacheFilePath & ".tmp"
        If IO.Directory.Exists(ItemsSearchCacheFolder) = False Then IO.Directory.CreateDirectory(ItemsSearchCacheFolder)
        If IO.File.Exists(tempPath) Then IO.File.Delete(tempPath)

        Try
            Dim cacheTable As DataTable = sourceTable.Copy()
            cacheTable.TableName = "ItemsSearch"

            Using fs As New IO.FileStream(tempPath,
                                         IO.FileMode.Create,
                                         IO.FileAccess.Write,
                                         IO.FileShare.None,
                                         4096,
                                         IO.FileOptions.WriteThrough)
                cacheTable.WriteXml(fs, XmlWriteMode.WriteSchema)
                fs.Flush(True)
            End Using

            Dim verifyTable As New DataTable()
            verifyTable.ReadXml(tempPath)
            If verifyTable.Rows.Count = 0 OrElse HasRequiredItemsColumns(verifyTable) = False Then Throw New DataException("فشل التحقق من كاش بحث الأصناف الجديد.")

            If IO.File.Exists(ItemsSearchCacheFilePath) Then
                IO.File.Replace(tempPath, ItemsSearchCacheFilePath, Nothing, True)
            Else
                IO.File.Move(tempPath, ItemsSearchCacheFilePath)
            End If
        Finally
            If IO.File.Exists(tempPath) Then IO.File.Delete(tempPath)
        End Try
    End Sub

    Private Sub IMDataGridViewX_KeyDown(sender As Object, e As KeyEventArgs) Handles IMDataGridViewX.KeyDown
        If e.KeyCode = Keys.Return Then
            Fetch_ItemToList()
        End If

        If e.KeyCode = Keys.Up Then
            If IMDataGridViewX.CurrentRow IsNot Nothing AndAlso IMDataGridViewX.CurrentRow.Index = 0 Then
                txtSearch.Select()
            End If
        End If
    End Sub

    Private Sub IMDataGridViewX_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles IMDataGridViewX.CellDoubleClick
        Fetch_ItemToList()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Hide()
    End Sub

    Private Sub ADD_NewGM_Btn_Click(sender As Object, e As EventArgs) Handles ADD_NewGM_Btn.Click
        IM_ADD_New.ShowDialog()
    End Sub

    Private Sub btnSearchName_Click(sender As Object, e As EventArgs) Handles btnSearchName.Click
        SetSearchMode("item_name", btnSearchName)
    End Sub

    Private Sub btnSearchBarcode_Click(sender As Object, e As EventArgs) Handles btnSearchBarcode.Click
        SetSearchMode("Barcode", btnSearchBarcode)
    End Sub

    Private Sub btnSearchItemNo_Click(sender As Object, e As EventArgs) Handles btnSearchItemNo.Click
        SetSearchMode("IM_NUM", btnSearchItemNo)
    End Sub

    Private Sub Show_IM_btn_Click(sender As Object, e As EventArgs) Handles Show_IM_btn.Click
        Load_ALL_IM()
        If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
            ExecuteSearch()
        End If
    End Sub

    Private Sub GM_Serach_SelectedValueChanged(sender As Object, e As EventArgs) Handles GM_Serach.SelectedValueChanged
        If IM_DT Is Nothing OrElse IM_DT.Columns.Count = 0 Then Return
        ApplyItemsSourceFilter()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        Select Case e.KeyCode
            Case Keys.Return
                ExecuteSearch()

            Case Keys.Down
                If IMDataGridViewX.Rows.Count > 0 Then
                    IMDataGridViewX.Select()
                End If
        End Select
    End Sub

    Private Sub ResetSearchButtonsStyle()
        btnSearchName.BackColor = Color.White
        btnSearchBarcode.BackColor = Color.White
        btnSearchItemNo.BackColor = Color.White

        btnSearchName.ForeColor = Color.Black
        btnSearchBarcode.ForeColor = Color.Black
        btnSearchItemNo.ForeColor = Color.Black
    End Sub

    Private Sub SetSearchMode(searchColumn As String, activeButton As Button)
        CurrentSearchColumn = searchColumn

        ResetSearchButtonsStyle()

        activeButton.BackColor = Color.FromArgb(52, 152, 219)
        activeButton.ForeColor = Color.White

        txtSearch.Focus()
        txtSearch.SelectionStart = txtSearch.TextLength

        ' إذا كان يوجد نص، نفذ الفلترة مباشرة
        Load_IM(CurrentSearchColumn, txtSearch.Text.Trim())
    End Sub


    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Load_IM(CurrentSearchColumn, txtSearch.Text.Trim())
    End Sub

    Private Sub KeyboardBtn_Click(sender As Object, e As EventArgs) Handles KeyboardBtn.Click
        If IMDataGridViewX.Height = 320 Then
            IMDataGridViewX.Size = New Size(999, 590)
        Else
            'kbPanel.Controls.Clear()
            IMDataGridViewX.Size = New Size(999, 320)
        End If
    End Sub

    Private Sub Items_Search_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Me.Hide()
        End If
    End Sub
End Class




