
Imports System.Data.SqlClient
Public Class Fixed_Assets

    Dim ACC_CODE_DT As New DataTable
    Dim DT As New DataTable
    Dim id As Integer = 0
    Private Sub ORG_B_NUM_txt_TextChanged(sender As Object, e As EventArgs) Handles DepreciationExpenseAccount.TextChanged
        If DepreciationExpenseAccount.Text.Count > 0 Then
            Filter_B(ORG_B_Cm, DepreciationExpenseAccount, ACC_CODE_DT)
        Else
            ACC_CODE_DT.Clear()
            ACC_CODE_DT = Accounts_Datatable
        End If

    End Sub




    Private Sub Fixed_Assets_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ACC_CODE_DT.Clear()
        ACC_CODE_DT = Accounts_Datatable

        cmbDepreciationFrequency.SelectedIndex = 0
        DepreciationMethod.SelectedIndex = 0
        Fill_FixedAssets_List()
        Fill_AssetGroups_List()
        Fill_AssetLocations_List()

        SendMessage(Search_Txt.Handle, &H1501, 0, "إبحث عن إسم الأصل")
    End Sub


    Public Sub Fill_AssetLocations_List()
        Dim C As New C
        C.Da = New SqlClient.SqlDataAdapter("SELECT [Id],[LocName] FROM [AssetLocations] ORDER BY Id ASC ", C.Con)
        C.Da.Fill(C.Dt)
        Location.DataSource = C.Dt
        Location.DisplayMember = "LocName"
        Location.ValueMember = "Id"
    End Sub

    Public Sub Fill_AssetGroups_List()
        Dim C As New C
        C.Da = New SqlClient.SqlDataAdapter("SELECT [Id],[GroupName] FROM [AssetGroups] ORDER BY Id ASC ", C.Con)
        C.Da.Fill(C.Dt)
        AssetGroupId.DataSource = C.Dt
        AssetGroupId.DisplayMember = "GroupName"
        AssetGroupId.ValueMember = "Id"
    End Sub

    Public Sub Fill_FixedAssets_List()
        DT = New DataTable
        Dim C As New C
        Dim da As New SqlClient.SqlDataAdapter("SELECT [Id],[AssetDescription] FROM [FixedAssets] ORDER BY Id ASC ", C.Con)
        da.Fill(DT)
        DataGridView1.DataSource = DT
        DataGridView1.Columns(0).Visible = False
    End Sub

    Private Sub EXP_B_NUM_txt_TextChanged(sender As Object, e As EventArgs) Handles AccumulatedDepreciationAccount.TextChanged
        If AccumulatedDepreciationAccount.Text.Count > 0 Then
            Filter_B(EXP_B_Cm, AccumulatedDepreciationAccount, ACC_CODE_DT)
        Else
            ACC_CODE_DT.Clear()
            ACC_CODE_DT = Accounts_Datatable
        End If
    End Sub


    Private Sub UpdateExpectedEntries()

        If cmbDepreciationFrequency.SelectedIndex = 0 Then
            Month_Panel.Visible = False
            'YEAR_Panel.Visible = True
        Else
            Month_Panel.Visible = True
            YEAR_Panel.Visible = False
        End If

        Try
            Dim purchaseDate As Date = Me.PurchaseDate.Value
            Dim usefulLife As Integer = Integer.Parse(UsefulLifeYears.Text)
            Dim frequency As String = cmbDepreciationFrequency.Text.Trim()

            Dim expectedEntries As Integer = 0

            If frequency = "سنوي" Then
                ' حساب تاريخ النهاية
                Dim endDate As Date = purchaseDate.AddYears(usefulLife)

                Dim currentStart As Date = purchaseDate
                Dim currentEnd As Date

                While currentStart < endDate
                    Dim yearEnd As Date = New Date(currentStart.Year, 12, 31)
                    currentEnd = If(yearEnd < endDate, yearEnd, endDate)

                    expectedEntries += 1

                    currentStart = currentEnd.AddDays(1)
                End While

            ElseIf frequency = "شهري" Then
                expectedEntries = usefulLife * 12
            Else
                lblExpectedEntries.Text = "❌ اختر نوع الإهلاك"
                Return
            End If

            lblExpectedEntries.Text = "عدد القيود المحاسبية المتوقع: " & expectedEntries.ToString()
        Catch ex As Exception
            lblExpectedEntries.Text = "❌ تأكد من إدخال البيانات بشكل صحيح"
        End Try



        '-----------------------------------------------------------------------------------------------------------------------
        'Dim usefulLife As Integer
        'Dim frequency As String = cmbDepreciationFrequency.Text.Trim()

        '' التأكد من أن العمر الافتراضي رقم صحيح
        'If Not Integer.TryParse(UsefulLifeYears.Text, usefulLife) Then
        '    lblExpectedEntries.Text = "❌ أدخل عدد سنوات صالح"
        '    Return
        'End If

        '' تحديد عدد القيود حسب نوع التكرار
        'Dim expectedEntries As Integer = 0
        'If frequency = "سنوي" Then
        '    expectedEntries = usefulLife
        'ElseIf frequency = "شهري" Then
        '    expectedEntries = usefulLife * 12
        'Else
        '    lblExpectedEntries.Text = "❌ اختر نوع الإهلاك"
        '    Return
        'End If

        '' عرض النتيجة في Label
        'lblExpectedEntries.Text = "عدد القيود المحاسبية المتوقعة: " & expectedEntries.ToString()
    End Sub


    Private Sub Depend_Btn_Click(sender As Object, e As EventArgs) Handles Depend_Btn.Click
        SaveAsset()
    End Sub


    Private Sub SaveAsset()

        Dim C As New C

        Using C.Con
            Using C.Com
                C.Com.CommandType = CommandType.StoredProcedure
                C.Com.CommandText = "AddFixedAsset"
                ' إضافة القيم من النموذج

                C.Com.Parameters.AddWithValue("@id", id)
                C.Com.Parameters.AddWithValue("@AssetDescription", AssetDescription.Text)
                C.Com.Parameters.AddWithValue("@AssetGroupId", CInt(AssetGroupId.SelectedValue))
                C.Com.Parameters.AddWithValue("@PurchaseDate", PurchaseDate.Value.Date)
                C.Com.Parameters.AddWithValue("@PurchaseAmount", Decimal.Parse(PurchaseAmount.Text))
                C.Com.Parameters.AddWithValue("@SerialNumber", SerialNumber.Text)
                C.Com.Parameters.AddWithValue("@LocationId", Location.SelectedValue)
                C.Com.Parameters.AddWithValue("@DepreciationExpenseAccount", DepreciationExpenseAccount.Text)
                ' C.Com.Parameters.AddWithValue("@AssetType", AssetType.Text)
                C.Com.Parameters.AddWithValue("@AccumulatedDepreciationAccount", AccumulatedDepreciationAccount.Text)
                C.Com.Parameters.AddWithValue("@UsefulLifeYears", CInt(UsefulLifeYears.Text))
                C.Com.Parameters.AddWithValue("@DepreciationMethod", DepreciationMethod.Text)
                C.Com.Parameters.AddWithValue("@SalvageValue", Decimal.Parse(SalvageValue.Text))
                C.Com.Parameters.AddWithValue("@DepreciationStartDate", DepreciationStartDate.Value.Date)
                C.Com.Parameters.AddWithValue("@DepreciationFrequency", cmbDepreciationFrequency.Text)

                If cmbDepreciationFrequency.Text = "شهري" Then
                    C.Com.Parameters.AddWithValue("@DepreciationDayOfMonth", DATE_OF_MONTH.Value.Day)
                Else
                    C.Com.Parameters.AddWithValue("@DepreciationDayOfMonth", DBNull.Value)
                End If

                If cmbDepreciationFrequency.Text = "سنوي" Then
                    C.Com.Parameters.AddWithValue("@DepreciationMonthOfYear", DATE_OF_YEAR.Value.Month)
                    C.Com.Parameters.AddWithValue("@DepreciationDayOfYear", DATE_OF_YEAR.Value.Day)
                Else
                    C.Com.Parameters.AddWithValue("@DepreciationMonthOfYear", DBNull.Value)
                    C.Com.Parameters.AddWithValue("@DepreciationDayOfYear", DBNull.Value)
                End If

                If DepreciationMethod.Text = "القسط المتناقص" Then
                    C.Com.Parameters.AddWithValue("@DepreciationRate", DepreciationRate.Text)
                End If



                '    C.Com.Parameters.AddWithValue("@DepreciationDayOfMonth", If(cmbDepreciationFrequency.Text, "شهري", DATE_OF_MONTH.Value.Day))

                ' MsgBox(If(cmbDepreciationFrequency.Text, "شهري", DATE_OF_MONTH.Value.Day).ToString)


                If id > 0 Then
                    If MessageBox.Show(" حفظ تعديلات الأصل ", "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.Cancel Then
                        Exit Sub
                    End If
                End If



                If SQL_SP_EXEC(C.Com) Then
                    Dim notification3 As New NotificationForm("تنويه", "تم حفظ الأصل/" & AssetDescription.Text, "bottom")
                    notification3.ShowNotification()
                    ' MsgBox("تم حفظ الأصل بنجاح", MsgBoxStyle.Information, "")
                    Fill_FixedAssets_List()
                End If

            End Using
        End Using
    End Sub

    Private Sub ORG_B_Cm_KeyDown(sender As Object, e As KeyEventArgs) Handles ORG_B_Cm.KeyDown

        If e.KeyCode = Keys.Return Then GET_B_DATA(ORG_B_Cm, DepreciationExpenseAccount)
    End Sub

    Private Sub EXP_B_Cm_KeyDown(sender As Object, e As KeyEventArgs) Handles EXP_B_Cm.KeyDown
        If e.KeyCode = Keys.Return Then GET_B_DATA(EXP_B_Cm, AccumulatedDepreciationAccount)
    End Sub

    Private Sub GET_B_DATA(ByRef CM As ComboBox, ByRef TXT As TextBox)
        If TypeName(CM.SelectedValue) = "String" Then
            TXT.Text = CM.SelectedValue
            CM.DroppedDown = False
        End If
    End Sub

    Private Sub ORG_B_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ORG_B_Cm.SelectedValueChanged
        GET_B_DATA(ORG_B_Cm, DepreciationExpenseAccount)
    End Sub

    Private Sub EXP_B_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles EXP_B_Cm.SelectedValueChanged
        GET_B_DATA(EXP_B_Cm, AccumulatedDepreciationAccount)
    End Sub

    Private Sub cmbDepreciationFrequency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepreciationFrequency.SelectedIndexChanged
        UpdateExpectedEntries()
    End Sub

    Private Sub UsefulLifeYears_TextChanged(sender As Object, e As EventArgs) Handles UsefulLifeYears.TextChanged
        UpdateExpectedEntries()
    End Sub

    Private Sub LoadAssetData(assetId As Integer)
        Dim C As New C
        Dim query As String = "SELECT * FROM FixedAssets WHERE Id = @AssetId"
        C.Com = New SqlCommand(query, C.Con)


        Using C.Con
            Using C.Com
                C.Com.Parameters.AddWithValue("@AssetId", assetId)

                C.Con.Open()
                C.Dr = C.Com.ExecuteReader()

                If C.Dr.Read() Then
                    id = assetId
                    ' تعيين البيانات إلى عناصر النموذج
                    AssetDescription.Text = C.Dr("AssetDescription").ToString()
                    AssetGroupId.SelectedValue = CInt(C.Dr("AssetGroupId"))
                    PurchaseDate.Value = CDate(C.Dr("PurchaseDate"))
                    PurchaseAmount.Text = C.Dr("PurchaseAmount").ToString()
                    SerialNumber.Text = C.Dr("SerialNumber").ToString()
                    Location.SelectedValue = C.Dr("LocationId").ToString()
                    DepreciationExpenseAccount.Text = C.Dr("DepreciationExpenseAccount").ToString()
                    ORG_B_Cm.DroppedDown = False
                    '  AssetType.Text = C.Dr("AssetType").ToString()
                    AccumulatedDepreciationAccount.Text = C.Dr("AccumulatedDepreciationAccount").ToString()
                    EXP_B_Cm.DroppedDown = False
                    UsefulLifeYears.Text = C.Dr("UsefulLifeYears").ToString()
                    DepreciationMethod.Text = C.Dr("DepreciationMethod").ToString()
                    SalvageValue.Text = C.Dr("SalvageValue").ToString()
                    DepreciationStartDate.Value = CDate(C.Dr("DepreciationStartDate"))
                    cmbDepreciationFrequency.Text = C.Dr("DepreciationFrequency").ToString()


                    If Not IsDBNull(C.Dr("DepreciationDayOfMonth")) Then
                        '----------------------------------------------------------------------------------------------------
                        ' DATE_OF_MONTH.Text = C.Dr("DepreciationDayOfMonth")
                        ' التاريخ الأساسي (مثال: تاريخ محفوظ أو مسترجع من قاعدة البيانات)
                        Dim originalDate As DateTime = New DateTime(2025, 5, 1)
                        ' اليوم الجديد من قاعدة البيانات (نوع Int)
                        Dim newDay As Integer = C.Dr("DepreciationDayOfMonth")

                        ' التأكد أن اليوم صالح في نفس الشهر والسنة
                        If newDay >= 1 AndAlso newDay <= DateTime.DaysInMonth(originalDate.Year, originalDate.Month) Then
                            ' إنشاء تاريخ جديد بنفس السنة والشهر لكن مع اليوم الجديد
                            Dim updatedDate As New DateTime(originalDate.Year, originalDate.Month, newDay)
                            ' تعيينه في DateTimePicker
                            DATE_OF_MONTH.Value = updatedDate

                            '' تخصيص التنسيق إن رغبت في عرض اليوم فقط مع فاصل
                            'DATE_OF_MONTH.Format = DateTimePickerFormat.Custom
                            'DATE_OF_MONTH.CustomFormat = "dd-"
                        Else
                            MessageBox.Show("اليوم غير صالح لهذا الشهر.")
                        End If
                        '-----------------------------------------------------------------------------------------------------
                    End If
                    '  DATE_OF_MONTH.Text = If(IsDBNull(C.Dr("DepreciationDayOfMonth")), "", C.Dr("DepreciationDayOfMonth").ToString())


                    If Not IsDBNull(C.Dr("DepreciationDayOfYear")) Then

                        '---------------------------------------------------------------------------------------------------------------------------------

                        Dim dayValue As Integer = C.Dr("DepreciationDayOfYear")
                        Dim monthValue As Integer = C.Dr("DepreciationMonthOfYear")
                        Dim yearValue As Integer = Now.Year ' أو حدد سنة معينة مثل: 2025

                        ' التحقق من صلاحية التاريخ
                        If dayValue >= 1 AndAlso monthValue >= 1 AndAlso monthValue <= 12 AndAlso
                     dayValue <= DateTime.DaysInMonth(yearValue, monthValue) Then

                            ' إنشاء التاريخ الجديد
                            Dim selectedDate As New DateTime(yearValue, monthValue, dayValue)

                            ' تعيينه في DateTimePicker
                            DATE_OF_YEAR.Value = selectedDate

                            '' تنسيق العرض المطلوب
                            'DateTimePicker1.Format = DateTimePickerFormat.Custom
                            'DateTimePicker1.CustomFormat = "dd-MM"
                        Else
                            MessageBox.Show("القيم غير صالحة لتكوين تاريخ.")
                        End If

                        '---------------------------------------------------------------------------------------------------------------------------------
                    End If



                    ' حساب عدد القيود المتوقع
                    Dim usefulLife As Integer = Convert.ToInt32(C.Dr("UsefulLifeYears"))
                        Dim frequency As String = C.Dr("DepreciationFrequency").ToString()
                        Dim expected As Integer = If(frequency = "سنوي", usefulLife, usefulLife * 12)

                        lblExpectedEntries.Text = "عدد القيود المحاسبية المتوقع: " & expected.ToString()

                        TITLE_txt.Text = "عرض الأصل : " & C.Dr("AssetDescription").ToString()


                        SELECT_Trans(assetId)
                    Else
                        MessageBox.Show("لم يتم العثور على الأصل.")
                End If
            End Using
        End Using
    End Sub

    Public Sub SELECT_Trans(AssetId As Integer)
        Dim C As New C
        C.Da = New SqlClient.SqlDataAdapter("SELECT * FROM [dbo].[AssetJournalEntries] WHERE AssetId = " & AssetId & " ORDER BY EntryDate ASC ", C.Con)
        C.Da.Fill(C.Dt)
        Trans_DataGridView.DataSource = C.Dt

    End Sub

    Private Sub DataGridView1_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseClick
        If DataGridView1.Rows.Count > 0 Then LoadAssetData(DataGridView1.CurrentRow.Cells("ID").Value)

    End Sub

    Private Sub NEW_Btn_Click(sender As Object, e As EventArgs) Handles NEW_Btn.Click
        Clear_Fields()
    End Sub

    Private Sub Clear_Fields()
        For Each a As Control In GroupBox1.Controls
            If TypeOf a Is TextBox Then
                a.Text = ""
            End If
        Next
        id = 0
        Trans_DataGridView.DataSource = Nothing
        TITLE_txt.Text = "إضافة أصل جديــــد"
        AssetDescription.Select()

        'For Each a As ComboBox In GroupBox1.Controls
        '    a.SelectedIndex = 0
        'Next


    End Sub

    Private Sub SEARCH_ACC_BTN_Click(sender As Object, e As EventArgs) Handles SEARCH_ACC_BTN.Click
        Dim F As New Normal_Form
        F.Form_Name = "AssetGroups"
        F.Form_Name_Arabic = "مجموعات الأصول"
        F.F_ID = "Id"
        F.F_Name = "GroupName"
        F.F_DETAILS = "AssetGroups"

        F.Checked_Table = "FixedAssets"
        F.Checked_Table_ID = "AssetGroupId"
        F.ShowDialog()
        Fill_AssetGroups_List()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim F As New Normal_Form
        F.Form_Name = "AssetLocations"
        F.Form_Name_Arabic = "مواقع الأصول"
        F.F_ID = "Id"
        F.F_Name = "LocName"
        F.F_DETAILS = "AssetLocations"

        F.Checked_Table = "FixedAssets"
        F.Checked_Table_ID = "LocationId"
        F.ShowDialog()
        Fill_AssetLocations_List()
    End Sub

    Private Sub Search_Txt_TextChanged(sender As Object, e As EventArgs) Handles Search_Txt.TextChanged
        Dim Dv As DataView
        Dv = DT.AsDataView
        Dv.RowFilter = IM_Serach(Search_Txt.Text, "[AssetDescription]")
        DataGridView1.DataSource = Dv
    End Sub

    Private Sub DELETE_Btn_Click(sender As Object, e As EventArgs) Handles DELETE_Btn.Click

        If id > 0 Then

            Try

                If MessageBox.Show(" حذف الأصل ", "تاكيــد العملية", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.Yes Then

                    query("DELETE FROM AssetJournalEntries WHERE AssetId = " & id)
                    query("DELETE FROM FixedAssets WHERE Id = " & id)
                    Dim notification3 As New NotificationForm("تنويه", "تم حذف الأصل/" & AssetDescription.Text, "bottom")
                    notification3.ShowNotification()

                    Fill_FixedAssets_List()

                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

    End Sub

    Private Sub DepreciationMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DepreciationMethod.SelectedIndexChanged
        If DepreciationMethod.SelectedIndex = 0 Then
            SalvageValue_Panel.Visible = True
            DepreciationRate_Panel.Visible = False
        Else
            SalvageValue_Panel.Visible = False
            DepreciationRate_Panel.Visible = True
        End If
    End Sub
End Class