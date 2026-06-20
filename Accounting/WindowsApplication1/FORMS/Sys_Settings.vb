Imports System.IO
Imports System.Drawing.Text
Imports System.Data


Public Class Sys_Settings

    Dim ResetType_Tmp As String = ""
    Private Sub Sys_Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'is_Link_With_SB_CB.Checked = MY_Settings.is_Link_With_SB
        'SALES_DB_TXT.Text = MY_Settings.SALES_DB
        'is_Dark_mode_CB.Checked = MY_Settings.is_Dark_mode

        'SBill_Title_1_Txt.Text = MY_Settings.SBill_Title_1
        'SBill_Title_2_Txt.Text = MY_Settings.SBill_Title_2

        SHOWphto()
        Load_Setting()
    End Sub

    Dim Data As Byte()
    Private Sub SHOWphto()
        'On Error Resume Next
        Dim c As New C
        Dim s As String = "SELECT LOGO FROM SysSetting"
        c.Com = New SqlClient.SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                If IsDBNull(c.Dr("LOGO")) = False Then
                    Data = DirectCast(c.Dr("LOGO"), Byte())
                    Dim MS As New MemoryStream(Data)
                    IMPictureBox.Image = Image.FromStream(MS)
                Else
                    IMPictureBox.Image = My.Resources.white
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

    End Sub



    Private Sub Load_Setting()
        'On Error Resume Next
        EnsureBudgetSettingsColumns()

        Dim c As New C
        Dim s As String = "SELECT * FROM SYS_Features_ACOUNTING"
        c.Com = New SqlClient.SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                SBill_Title_1_Txt.Text = c.Dr("SBill_Title_1")
                SBill_Title_2_Txt.Text = c.Dr("SBill_Title_2")
                Pure_Income_ACC_CODE_TXT.Text = c.Dr("Pure_Income_ACC_CODE")
                Prefix.Text = c.Dr("Prefix")
                NumberLength.Text = c.Dr("NumberLength")
                ResetType_CM.Text = c.Dr("ResetType")
                ResetType_Tmp = c.Dr("ResetType")
                is_Link_With_SB_CB.Checked = c.Dr("is_Link_With_SB")
                SALES_DB_TXT.Text = c.Dr("SALES_DB")
                Address.Text = c.Dr("Address")
                Phone_Number.Text = c.Dr("Phone_Number")
                is_Dark_mode_CB.Checked = c.Dr("is_Dark_mode")
                Use_State_Budget_CB.Checked = c.Dr("Use_State_Budget")
                Allow_Budget_OverSpend_CB.Checked = If(IsDBNull(c.Dr("Allow_Budget_OverSpend")), False, Convert.ToBoolean(c.Dr("Allow_Budget_OverSpend")))
                Default_Stamp_Percent_TXT.Text = If(IsDBNull(c.Dr("Default_Stamp_Percent")), "", Convert.ToDecimal(c.Dr("Default_Stamp_Percent")).ToString("0.###"))
                Default_Stamp_Account_Code_TXT.Text = If(IsDBNull(c.Dr("Default_Stamp_Account_Code")), "", c.Dr("Default_Stamp_Account_Code").ToString())
                Default_Stamp_Account_Name_TXT.Text = GetAccountName(Default_Stamp_Account_Code_TXT.Text)
                Email.Text = c.Dr("Email")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

        ApplyBudgetOverSpendOptionState()

        Dim fonts As New InstalledFontCollection()
        ' حلقة لإضافة أسماء الخطوط إلى الـ ComboBox
        For Each fontFamily As FontFamily In fonts.Families
            Font_Cm.Items.Add(fontFamily.Name)
        Next
        ' اختيار أول خط تلقائيًا (اختياري)
        If Font_Cm.Items.Count > 0 Then
            Font_Cm.SelectedIndex = 0
            Font_Cm.Sorted = True
        End If


    End Sub


    Private Function Check_ACC_Master()
        'On Error Resume Next
        Dim c As New C
        Dim s As String = "SELECT top 1 T_ID  FROM ACC_BALANCE_MASTER "
        c.Com = New SqlClient.SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                Return True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

        Return False
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try



            If Check_ACC_Master() = True And ResetType_CM.Text <> ResetType_Tmp Then
                MsgBox("تم إدراج قيود ... لم يعد بإمكانك تعديل نظام تهيئة الأرقام الإشارية", MsgBoxStyle.Critical, "خطأ")
                Exit Sub
            End If


            Dim defaultStampPercent As Decimal
            If Not String.IsNullOrWhiteSpace(Default_Stamp_Percent_TXT.Text) AndAlso
               Not Decimal.TryParse(Default_Stamp_Percent_TXT.Text.Trim(), defaultStampPercent) Then
                MsgBox("نسبة الدمغة الافتراضية غير صحيحة", MsgBoxStyle.Exclamation, "تنبيه")
                Default_Stamp_Percent_TXT.Focus()
                Exit Sub
            End If

            If defaultStampPercent < 0D Then
                MsgBox("نسبة الدمغة الافتراضية لا يمكن أن تكون أقل من صفر", MsgBoxStyle.Exclamation, "تنبيه")
                Default_Stamp_Percent_TXT.Focus()
                Exit Sub
            End If

            Dim defaultStampAccountCode As String = Default_Stamp_Account_Code_TXT.Text.Trim()
            If defaultStampPercent > 0D Then
                If String.IsNullOrWhiteSpace(defaultStampAccountCode) Then
                    MsgBox("اختر حساب الدمغة الافتراضي", MsgBoxStyle.Exclamation, "تنبيه")
                    Pick_Default_Stamp_Account_BTN.Focus()
                    Exit Sub
                End If

                If String.IsNullOrWhiteSpace(GetAccountName(defaultStampAccountCode)) Then
                    MsgBox("حساب الدمغة الافتراضي غير موجود في شجرة الحسابات", MsgBoxStyle.Exclamation, "تنبيه")
                    Default_Stamp_Account_Code_TXT.Focus()
                    Exit Sub
                End If
            End If

            Dim model As New SysFeaturesModel With {
                .Pure_Income_ACC_CODE = Pure_Income_ACC_CODE_TXT.Text,
                .Prefix = Prefix.Text,
                .NumberLength = NumberLength.Text,
                .ResetType = ResetType_CM.Text,
                .is_Link_With_SB = is_Link_With_SB_CB.Checked,
                .SALES_DB = SALES_DB_TXT.Text,
                .SBill_Title_1 = SBill_Title_1_Txt.Text,
                .SBill_Title_2 = SBill_Title_2_Txt.Text,
                .Address = Address.Text,
                .Phone_Number = Phone_Number.Text,
                .is_Dark_mode = is_Dark_mode_CB.Checked,
                .Use_State_Budget = Use_State_Budget_CB.Checked,
                .Allow_Budget_OverSpend = (Use_State_Budget_CB.Checked AndAlso Allow_Budget_OverSpend_CB.Checked),
                .Default_Stamp_Percent = defaultStampPercent,
                .Default_Stamp_Account_Code = defaultStampAccountCode,
                .Email = Email.Text
            }

            Dim dal As New SysFeaturesDAL(MY_Settings.SqlConStr)
            Dim rows As Integer = dal.UpdateSysFeatures(model)

            If rows > 0 Then
                'MessageBox.Show("تم تحديث  🔄البيانات بنجاح")

                MY_Settings.is_Link_With_SB = is_Link_With_SB_CB.Checked
                MY_Settings.SALES_DB = SALES_DB_TXT.Text
                MY_Settings.is_Dark_mode = is_Dark_mode_CB.Checked
                MY_Settings.Use_State_Budget = Use_State_Budget_CB.Checked
                MY_Settings.Allow_Budget_OverSpend = (Use_State_Budget_CB.Checked AndAlso Allow_Budget_OverSpend_CB.Checked)
                MY_Settings.Default_Stamp_Percent = defaultStampPercent
                MY_Settings.Default_Stamp_Account_Code = defaultStampAccountCode
                MY_Settings.SBill_Title_1 = SBill_Title_1_Txt.Text
                MY_Settings.SBill_Title_2 = SBill_Title_2_Txt.Text
                Identifiers.Pure_Income_ACC_CODE = Pure_Income_ACC_CODE_TXT.Text
                MY_Settings.Save_AppSetting()

                Update_SB_DB()

                MsgBox("تم حفظ التعديلات", MsgBoxStyle.Information, "")
                Me.Close()

            Else
                MessageBox.Show("لم يتم العثور على سجل للتحديث")
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub



    Private Sub ChoasePicureButton_Click(sender As Object, e As EventArgs) Handles ChoasePicureButton.Click
        On Error Resume Next
        With Me.OpenFileDialog1
            .Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico"
            .FilterIndex = 1
            .Multiselect = False
            .Title = "حدد شعار الشركة"
            .ShowDialog()
            If Len(.FileName) > 0 Then
                IMPictureBox.Image = Image.FromFile(OpenFileDialog1.FileName)
                'Update_SB_DB()
            End If
        End With
    End Sub

    Private Sub Update_SB_DB()
        Dim c As New C
        Dim sql As String = "UPDATE SysSetting SET logo = @logo , CompName = @SBill_Title_1 , englishN = @SBill_Title_2, BillNotes = @BillNotes"

        Using c.Con
            Using cmd As New SqlClient.SqlCommand(sql, c.Con)
                cmd.Parameters.AddWithValue("@logo", ConvertImage(IMPictureBox.Image))
                cmd.Parameters.AddWithValue("@SBill_Title_1", SBill_Title_1_Txt.Text)
                cmd.Parameters.AddWithValue("@SBill_Title_2", SBill_Title_2_Txt.Text)
                cmd.Parameters.AddWithValue("@BillNotes", Address.Text)

                SQL_SP_EXEC(cmd)
            End Using
        End Using
    End Sub

    Private Sub NoPictureButton_Click(sender As Object, e As EventArgs) Handles NoPictureButton.Click
        IMPictureBox.Image = My.Resources.white
        Update_SB_DB()
    End Sub

    Private Sub EnsureBudgetSettingsColumns()
        Dim sql As String =
            "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Use_State_Budget') IS NULL " &
            "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Use_State_Budget BIT NOT NULL CONSTRAINT DF_SYS_Features_ACOUNTING_Use_State_Budget DEFAULT(0); " &
            "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Allow_Budget_OverSpend') IS NULL " &
            "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Allow_Budget_OverSpend BIT NOT NULL CONSTRAINT DF_SYS_Features_ACOUNTING_Allow_Budget_OverSpend DEFAULT(0); " &
            "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Default_Stamp_Percent') IS NULL " &
            "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Default_Stamp_Percent DECIMAL(18,3) NULL; " &
            "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Default_Stamp_Account_Code') IS NULL " &
            "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Default_Stamp_Account_Code NVARCHAR(40) NULL;"

        query(sql)
    End Sub

    Private Function GetAccountName(accountCode As String) As String
        If String.IsNullOrWhiteSpace(accountCode) Then Return ""

        Using con As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlClient.SqlCommand("SELECT TOP 1 ACC_NAME FROM dbo.ACCOUNTS_TREE WHERE ACC_CODE = @ACC_CODE;", con)
                cmd.Parameters.Add("@ACC_CODE", SqlDbType.NVarChar, 40).Value = accountCode.Trim()
                con.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing OrElse result Is DBNull.Value Then Return ""
                Return result.ToString()
            End Using
        End Using
    End Function

    Private Sub Pick_Default_Stamp_Account_BTN_Click(sender As Object, e As EventArgs) Handles Pick_Default_Stamp_Account_BTN.Click
        ACC_CODE_Search = ""
        ACC_NAME_Search = ""

        Dim frm As New BALANCE_SEARCH
        frm.ShowDialog()

        If String.IsNullOrWhiteSpace(ACC_CODE_Search) Then Exit Sub

        Default_Stamp_Account_Code_TXT.Text = ACC_CODE_Search.Trim()
        Default_Stamp_Account_Name_TXT.Text = If(String.IsNullOrWhiteSpace(ACC_NAME_Search), GetAccountName(ACC_CODE_Search), ACC_NAME_Search.Trim())
    End Sub

    Private Sub Default_Stamp_Account_Code_TXT_Leave(sender As Object, e As EventArgs) Handles Default_Stamp_Account_Code_TXT.Leave
        Default_Stamp_Account_Name_TXT.Text = GetAccountName(Default_Stamp_Account_Code_TXT.Text)
    End Sub

    Private Sub Use_State_Budget_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Use_State_Budget_CB.CheckedChanged
        ApplyBudgetOverSpendOptionState()
    End Sub

    Private Sub ApplyBudgetOverSpendOptionState()
        If Allow_Budget_OverSpend_CB Is Nothing Then Exit Sub

        Allow_Budget_OverSpend_CB.Visible = Use_State_Budget_CB.Checked
        Allow_Budget_OverSpend_CB.Enabled = Use_State_Budget_CB.Checked

        If Not Use_State_Budget_CB.Checked Then
            Allow_Budget_OverSpend_CB.Checked = False
        End If
    End Sub


End Class
