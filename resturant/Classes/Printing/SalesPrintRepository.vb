Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq

Public Class SalesPrintRepository

    Public Const UsageSales As String = "SALES"
    Private ReadOnly ConnectionString As String

    Public Sub New(connectionString As String)
        Me.ConnectionString = connectionString
    End Sub

    Public Sub EnsureSchema()
        Using cn As New SqlConnection(ConnectionString)
            cn.Open()

            Using cmd As New SqlCommand(GetCreateProfileTableSql(), cn)
                cmd.ExecuteNonQuery()
            End Using

            Using cmd As New SqlCommand(GetCreateComponentTableSql(), cn)
                cmd.ExecuteNonQuery()
            End Using

            EnsureProfileStyleColumns(cn)
        End Using
    End Sub

    Public Function LoadProfilesTable(Optional usageKey As String = UsageSales) As DataTable
        EnsureSchema()

        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConnectionString)
            Using da As New SqlDataAdapter("SELECT ProfileID, ProfileName, PaperKind, PrinterName, IsDefault FROM dbo.Sales_Print_Profile WHERE UsageKey = @UsageKey ORDER BY PaperKind, IsDefault DESC, ProfileName", cn)
                da.SelectCommand.Parameters.Add("@UsageKey", SqlDbType.NVarChar, 50).Value = NormalizeUsageKey(usageKey)
                da.Fill(dt)
            End Using
        End Using

        Return dt
    End Function

    Public Function LoadDefaultProfile(Optional usageKey As String = UsageSales, Optional paperKind As String = "") As SalesPrintProfile
        EnsureSchema()

        usageKey = NormalizeUsageKey(usageKey)
        paperKind = If(paperKind, "").Trim()

        Dim profileId As Integer = 0
        Using cn As New SqlConnection(ConnectionString)
            cn.Open()
            Using cmd As New SqlCommand("SELECT TOP 1 ProfileID FROM dbo.Sales_Print_Profile WHERE UsageKey = @UsageKey AND (@PaperKind = N'' OR PaperKind = @PaperKind) ORDER BY IsDefault DESC, ProfileID", cn)
                cmd.Parameters.Add("@UsageKey", SqlDbType.NVarChar, 50).Value = usageKey
                cmd.Parameters.Add("@PaperKind", SqlDbType.NVarChar, 20).Value = paperKind
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso result IsNot DBNull.Value Then profileId = Convert.ToInt32(result)
            End Using
        End Using

        If profileId = 0 Then
            Dim defaultProfile As SalesPrintProfile = CreateDefaultProfile(usageKey, If(String.IsNullOrWhiteSpace(paperKind), "A4", paperKind))
            SaveProfile(defaultProfile)
            Return defaultProfile
        End If

        Return LoadProfile(profileId)
    End Function

    Public Function LoadProfile(profileId As Integer) As SalesPrintProfile
        EnsureSchema()

        Dim profile As SalesPrintProfile = Nothing

        Using cn As New SqlConnection(ConnectionString)
            cn.Open()

            Using cmd As New SqlCommand("SELECT TOP 1 * FROM dbo.Sales_Print_Profile WHERE ProfileID = @ProfileID", cn)
                cmd.Parameters.Add("@ProfileID", SqlDbType.Int).Value = profileId

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        profile = New SalesPrintProfile()
                        profile.ProfileID = Convert.ToInt32(dr("ProfileID"))
                        profile.ProfileName = dr("ProfileName").ToString()
                        profile.UsageKey = dr("UsageKey").ToString()
                        profile.PaperKind = dr("PaperKind").ToString()
                        profile.PrinterName = dr("PrinterName").ToString()
                        profile.IsDefault = Convert.ToBoolean(dr("IsDefault"))
                        profile.Landscape = Convert.ToBoolean(dr("Landscape"))
                        profile.MarginLeft = Convert.ToInt32(dr("MarginLeft"))
                        profile.MarginRight = Convert.ToInt32(dr("MarginRight"))
                        profile.MarginTop = Convert.ToInt32(dr("MarginTop"))
                        profile.MarginBottom = Convert.ToInt32(dr("MarginBottom"))
                        profile.FontFamily = dr("FontFamily").ToString()
                        profile.TitleFontSize = Convert.ToDecimal(dr("TitleFontSize"))
                        profile.SubTitleFontSize = Convert.ToDecimal(dr("SubTitleFontSize"))
                        profile.InfoFontSize = Convert.ToDecimal(dr("InfoFontSize"))
                        profile.HeaderFontSize = Convert.ToDecimal(dr("HeaderFontSize"))
                        profile.RowFontSize = Convert.ToDecimal(dr("RowFontSize"))
                        profile.TotalFontSize = Convert.ToDecimal(dr("TotalFontSize"))
                        profile.FooterFontSize = Convert.ToDecimal(dr("FooterFontSize"))
                        profile.TitleForeColorArgb = Convert.ToInt32(dr("TitleForeColorArgb"))
                        profile.TextForeColorArgb = Convert.ToInt32(dr("TextForeColorArgb"))
                        profile.HeaderBackColorArgb = Convert.ToInt32(dr("HeaderBackColorArgb"))
                        profile.HeaderForeColorArgb = Convert.ToInt32(dr("HeaderForeColorArgb"))
                        profile.RowBackColorArgb = Convert.ToInt32(dr("RowBackColorArgb"))
                        profile.AlternateRowBackColorArgb = Convert.ToInt32(dr("AlternateRowBackColorArgb"))
                        profile.BorderColorArgb = Convert.ToInt32(dr("BorderColorArgb"))
                        profile.TotalBackColorArgb = Convert.ToInt32(dr("TotalBackColorArgb"))
                        profile.TotalForeColorArgb = Convert.ToInt32(dr("TotalForeColorArgb"))
                        profile.FooterForeColorArgb = Convert.ToInt32(dr("FooterForeColorArgb"))
                        profile.UseAlternatingRows = Convert.ToBoolean(dr("UseAlternatingRows"))
                        profile.DrawGridLines = Convert.ToBoolean(dr("DrawGridLines"))
                    End If
                End Using
            End Using

            If profile Is Nothing Then Return LoadDefaultProfile()

            Using cmd As New SqlCommand("SELECT * FROM dbo.Sales_Print_Profile_Component WHERE ProfileID = @ProfileID ORDER BY ComponentScope, SortOrder, ComponentID", cn)
                cmd.Parameters.Add("@ProfileID", SqlDbType.Int).Value = profile.ProfileID

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim component As New SalesPrintComponent()
                        component.ComponentID = Convert.ToInt32(dr("ComponentID"))
                        component.ProfileID = Convert.ToInt32(dr("ProfileID"))
                        component.ComponentScope = dr("ComponentScope").ToString()
                        component.ComponentCode = dr("ComponentCode").ToString()
                        component.DisplayName = dr("DisplayName").ToString()
                        component.IsVisible = Convert.ToBoolean(dr("IsVisible"))
                        component.SortOrder = Convert.ToInt32(dr("SortOrder"))
                        component.WidthValue = Convert.ToInt32(dr("WidthValue"))
                        component.AlignmentValue = dr("AlignmentValue").ToString()
                        profile.Components.Add(component)
                    End While
                End Using
            End Using
        End Using

        MergeMissingDefaults(profile)
        Return profile
    End Function

    Public Function SaveProfile(profile As SalesPrintProfile) As Integer
        EnsureSchema()

        If profile Is Nothing Then Return 0
        profile.UsageKey = NormalizeUsageKey(profile.UsageKey)
        If String.IsNullOrWhiteSpace(profile.PaperKind) Then profile.PaperKind = "A4"
        MergeMissingDefaults(profile)

        Using cn As New SqlConnection(ConnectionString)
            cn.Open()
            Using tr As SqlTransaction = cn.BeginTransaction()
                Try
                    If profile.IsDefault Then
                        Using cmd As New SqlCommand("UPDATE dbo.Sales_Print_Profile SET IsDefault = 0 WHERE UsageKey = @UsageKey AND PaperKind = @PaperKind", cn, tr)
                            cmd.Parameters.Add("@UsageKey", SqlDbType.NVarChar, 50).Value = profile.UsageKey
                            cmd.Parameters.Add("@PaperKind", SqlDbType.NVarChar, 20).Value = profile.PaperKind
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    If profile.ProfileID <= 0 Then
                        Using cmd As New SqlCommand("INSERT INTO dbo.Sales_Print_Profile(ProfileName, UsageKey, PaperKind, PrinterName, IsDefault, Landscape, MarginLeft, MarginRight, MarginTop, MarginBottom, FontFamily, TitleFontSize, SubTitleFontSize, InfoFontSize, HeaderFontSize, RowFontSize, TotalFontSize, FooterFontSize, TitleForeColorArgb, TextForeColorArgb, HeaderBackColorArgb, HeaderForeColorArgb, RowBackColorArgb, AlternateRowBackColorArgb, BorderColorArgb, TotalBackColorArgb, TotalForeColorArgb, FooterForeColorArgb, UseAlternatingRows, DrawGridLines, CreatedAt, UpdatedAt) OUTPUT INSERTED.ProfileID VALUES(@ProfileName, @UsageKey, @PaperKind, @PrinterName, @IsDefault, @Landscape, @MarginLeft, @MarginRight, @MarginTop, @MarginBottom, @FontFamily, @TitleFontSize, @SubTitleFontSize, @InfoFontSize, @HeaderFontSize, @RowFontSize, @TotalFontSize, @FooterFontSize, @TitleForeColorArgb, @TextForeColorArgb, @HeaderBackColorArgb, @HeaderForeColorArgb, @RowBackColorArgb, @AlternateRowBackColorArgb, @BorderColorArgb, @TotalBackColorArgb, @TotalForeColorArgb, @FooterForeColorArgb, @UseAlternatingRows, @DrawGridLines, GETDATE(), GETDATE())", cn, tr)
                            AddProfileParameters(cmd, profile)
                            profile.ProfileID = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using
                    Else
                        Using cmd As New SqlCommand("UPDATE dbo.Sales_Print_Profile SET ProfileName=@ProfileName, UsageKey=@UsageKey, PaperKind=@PaperKind, PrinterName=@PrinterName, IsDefault=@IsDefault, Landscape=@Landscape, MarginLeft=@MarginLeft, MarginRight=@MarginRight, MarginTop=@MarginTop, MarginBottom=@MarginBottom, FontFamily=@FontFamily, TitleFontSize=@TitleFontSize, SubTitleFontSize=@SubTitleFontSize, InfoFontSize=@InfoFontSize, HeaderFontSize=@HeaderFontSize, RowFontSize=@RowFontSize, TotalFontSize=@TotalFontSize, FooterFontSize=@FooterFontSize, TitleForeColorArgb=@TitleForeColorArgb, TextForeColorArgb=@TextForeColorArgb, HeaderBackColorArgb=@HeaderBackColorArgb, HeaderForeColorArgb=@HeaderForeColorArgb, RowBackColorArgb=@RowBackColorArgb, AlternateRowBackColorArgb=@AlternateRowBackColorArgb, BorderColorArgb=@BorderColorArgb, TotalBackColorArgb=@TotalBackColorArgb, TotalForeColorArgb=@TotalForeColorArgb, FooterForeColorArgb=@FooterForeColorArgb, UseAlternatingRows=@UseAlternatingRows, DrawGridLines=@DrawGridLines, UpdatedAt=GETDATE() WHERE ProfileID=@ProfileID", cn, tr)
                            AddProfileParameters(cmd, profile)
                            cmd.Parameters.Add("@ProfileID", SqlDbType.Int).Value = profile.ProfileID
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    Using cmd As New SqlCommand("DELETE FROM dbo.Sales_Print_Profile_Component WHERE ProfileID=@ProfileID", cn, tr)
                        cmd.Parameters.Add("@ProfileID", SqlDbType.Int).Value = profile.ProfileID
                        cmd.ExecuteNonQuery()
                    End Using

                    For Each component As SalesPrintComponent In profile.Components
                        Using cmd As New SqlCommand("INSERT INTO dbo.Sales_Print_Profile_Component(ProfileID, ComponentScope, ComponentCode, DisplayName, IsVisible, SortOrder, WidthValue, AlignmentValue) VALUES(@ProfileID, @ComponentScope, @ComponentCode, @DisplayName, @IsVisible, @SortOrder, @WidthValue, @AlignmentValue)", cn, tr)
                            cmd.Parameters.Add("@ProfileID", SqlDbType.Int).Value = profile.ProfileID
                            cmd.Parameters.Add("@ComponentScope", SqlDbType.NVarChar, 20).Value = component.ComponentScope
                            cmd.Parameters.Add("@ComponentCode", SqlDbType.NVarChar, 100).Value = component.ComponentCode
                            cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar, 200).Value = component.DisplayName
                            cmd.Parameters.Add("@IsVisible", SqlDbType.Bit).Value = component.IsVisible
                            cmd.Parameters.Add("@SortOrder", SqlDbType.Int).Value = component.SortOrder
                            cmd.Parameters.Add("@WidthValue", SqlDbType.Int).Value = component.WidthValue
                            cmd.Parameters.Add("@AlignmentValue", SqlDbType.NVarChar, 20).Value = component.AlignmentValue
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                    tr.Commit()
                Catch
                    tr.Rollback()
                    Throw
                End Try
            End Using
        End Using

        Return profile.ProfileID
    End Function

    Public Function CreateDefaultProfile(Optional usageKey As String = UsageSales, Optional paperKind As String = "A4") As SalesPrintProfile
        Dim p As New SalesPrintProfile()
        p.ProfileName = "تصميم فاتورة المبيعات الافتراضي"
        p.UsageKey = NormalizeUsageKey(usageKey)
        p.PaperKind = If(String.IsNullOrWhiteSpace(paperKind), "A4", paperKind)
        p.PrinterName = If(String.IsNullOrWhiteSpace(Default_Printer_A4), "", Default_Printer_A4)
        p.IsDefault = True
        p.Landscape = False
        p.MarginLeft = 35
        p.MarginRight = 35
        p.MarginTop = 40
        p.MarginBottom = 45
        p.Components = GetDefaultComponents()
        Return p
    End Function

    Private Sub AddProfileParameters(cmd As SqlCommand, profile As SalesPrintProfile)
        cmd.Parameters.Add("@ProfileName", SqlDbType.NVarChar, 150).Value = profile.ProfileName
        cmd.Parameters.Add("@UsageKey", SqlDbType.NVarChar, 50).Value = NormalizeUsageKey(profile.UsageKey)
        cmd.Parameters.Add("@PaperKind", SqlDbType.NVarChar, 20).Value = profile.PaperKind
        cmd.Parameters.Add("@PrinterName", SqlDbType.NVarChar, 250).Value = If(profile.PrinterName, "")
        cmd.Parameters.Add("@IsDefault", SqlDbType.Bit).Value = profile.IsDefault
        cmd.Parameters.Add("@Landscape", SqlDbType.Bit).Value = profile.Landscape
        cmd.Parameters.Add("@MarginLeft", SqlDbType.Int).Value = profile.MarginLeft
        cmd.Parameters.Add("@MarginRight", SqlDbType.Int).Value = profile.MarginRight
        cmd.Parameters.Add("@MarginTop", SqlDbType.Int).Value = profile.MarginTop
        cmd.Parameters.Add("@MarginBottom", SqlDbType.Int).Value = profile.MarginBottom
        cmd.Parameters.Add("@FontFamily", SqlDbType.NVarChar, 100).Value = profile.FontFamily
        cmd.Parameters.Add("@TitleFontSize", SqlDbType.Decimal).Value = profile.TitleFontSize
        cmd.Parameters.Add("@SubTitleFontSize", SqlDbType.Decimal).Value = profile.SubTitleFontSize
        cmd.Parameters.Add("@InfoFontSize", SqlDbType.Decimal).Value = profile.InfoFontSize
        cmd.Parameters.Add("@HeaderFontSize", SqlDbType.Decimal).Value = profile.HeaderFontSize
        cmd.Parameters.Add("@RowFontSize", SqlDbType.Decimal).Value = profile.RowFontSize
        cmd.Parameters.Add("@TotalFontSize", SqlDbType.Decimal).Value = profile.TotalFontSize
        cmd.Parameters.Add("@FooterFontSize", SqlDbType.Decimal).Value = profile.FooterFontSize
        cmd.Parameters.Add("@TitleForeColorArgb", SqlDbType.Int).Value = profile.TitleForeColorArgb
        cmd.Parameters.Add("@TextForeColorArgb", SqlDbType.Int).Value = profile.TextForeColorArgb
        cmd.Parameters.Add("@HeaderBackColorArgb", SqlDbType.Int).Value = profile.HeaderBackColorArgb
        cmd.Parameters.Add("@HeaderForeColorArgb", SqlDbType.Int).Value = profile.HeaderForeColorArgb
        cmd.Parameters.Add("@RowBackColorArgb", SqlDbType.Int).Value = profile.RowBackColorArgb
        cmd.Parameters.Add("@AlternateRowBackColorArgb", SqlDbType.Int).Value = profile.AlternateRowBackColorArgb
        cmd.Parameters.Add("@BorderColorArgb", SqlDbType.Int).Value = profile.BorderColorArgb
        cmd.Parameters.Add("@TotalBackColorArgb", SqlDbType.Int).Value = profile.TotalBackColorArgb
        cmd.Parameters.Add("@TotalForeColorArgb", SqlDbType.Int).Value = profile.TotalForeColorArgb
        cmd.Parameters.Add("@FooterForeColorArgb", SqlDbType.Int).Value = profile.FooterForeColorArgb
        cmd.Parameters.Add("@UseAlternatingRows", SqlDbType.Bit).Value = profile.UseAlternatingRows
        cmd.Parameters.Add("@DrawGridLines", SqlDbType.Bit).Value = profile.DrawGridLines
    End Sub

    Public Sub MergeMissingDefaults(profile As SalesPrintProfile)
        If profile Is Nothing Then Return

        Dim defaults As List(Of SalesPrintComponent) = GetDefaultComponents()
        For Each def As SalesPrintComponent In defaults
            Dim exists As Boolean = profile.Components.Any(Function(c) c.ComponentScope = def.ComponentScope AndAlso c.ComponentCode = def.ComponentCode)
            If exists = False Then profile.Components.Add(def.CloneComponent())
        Next
    End Sub

    Public Function GetDefaultComponents() As List(Of SalesPrintComponent)
        Dim list As New List(Of SalesPrintComponent)()

        AddComponent(list, "SECTION", "Logo", "الشعار", True, 10, 70, "Center")
        AddComponent(list, "SECTION", "StoreTitle", "اسم المحل", True, 20, 100, "Center")
        AddComponent(list, "SECTION", "StoreAddress", "العنوان", True, 30, 100, "Center")
        AddComponent(list, "SECTION", "BillInfo", "بيانات الفاتورة", True, 40, 100, "Right")
        AddComponent(list, "SECTION", "Customer", "العميل", True, 50, 100, "Right")
        AddComponent(list, "SECTION", "Project", "المشروع", True, 60, 100, "Right")
        AddComponent(list, "SECTION", "UserName", "المستخدم", True, 70, 100, "Right")
        AddComponent(list, "SECTION", "ItemsTable", "جدول الأصناف", True, 80, 100, "Center")
        AddComponent(list, "SECTION", "Totals", "الإجماليات", True, 90, 100, "Right")
        AddComponent(list, "SECTION", "Notes", "ملاحظات الفاتورة", True, 100, 100, "Right")
        AddComponent(list, "SECTION", "Barcode", "الباركود", True, 110, 100, "Center")
        AddComponent(list, "SECTION", "Footer", "التذييل", True, 120, 100, "Center")

        AddComponent(list, "COLUMN", "IMNUM_CL", "رقم", False, 10, 45, "Center")
        AddComponent(list, "COLUMN", "Barcode_CL", "باركود", False, 20, 80, "Center")
        AddComponent(list, "COLUMN", "EX_Name_CL", "الصنف", True, 30, 220, "Right")
        AddComponent(list, "COLUMN", "IMUnit_CL", "الوحدة", True, 40, 70, "Center")
        AddComponent(list, "COLUMN", "QTY_CL", "كمية", True, 50, 65, "Center")
        AddComponent(list, "COLUMN", "Price_CL", "السعر", True, 60, 80, "Center")
        AddComponent(list, "COLUMN", "IM_Discount_CL", "خصم", False, 70, 65, "Center")
        AddComponent(list, "COLUMN", "Total_CL", "إجمالي", True, 80, 90, "Center")
        AddComponent(list, "COLUMN", "Notes_CL", "ملاحظة", False, 90, 130, "Right")
        AddComponent(list, "COLUMN", "ST_Name_CL", "المخزن", False, 100, 90, "Center")
        AddComponent(list, "COLUMN", "D_Valid_CL", "صلاحية", False, 110, 90, "Center")

        Return list
    End Function

    Private Sub AddComponent(list As List(Of SalesPrintComponent), scope As String, code As String, displayName As String, visible As Boolean, sortOrder As Integer, widthValue As Integer, alignmentValue As String)
        Dim c As New SalesPrintComponent()
        c.ComponentScope = scope
        c.ComponentCode = code
        c.DisplayName = displayName
        c.IsVisible = visible
        c.SortOrder = sortOrder
        c.WidthValue = widthValue
        c.AlignmentValue = alignmentValue
        list.Add(c)
    End Sub

    Private Function NormalizeUsageKey(usageKey As String) As String
        If String.IsNullOrWhiteSpace(usageKey) Then Return UsageSales
        Return usageKey.Trim().ToUpperInvariant()
    End Function

    Private Sub EnsureProfileStyleColumns(cn As SqlConnection)
        Dim scripts As String() = {
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'UsageKey') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD UsageKey NVARCHAR(50) NOT NULL CONSTRAINT DF_Sales_Print_Profile_UsageKey DEFAULT(N'SALES')",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'FontFamily') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD FontFamily NVARCHAR(100) NOT NULL CONSTRAINT DF_Sales_Print_Profile_FontFamily DEFAULT(N'Segoe UI')",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'TitleFontSize') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD TitleFontSize DECIMAL(6,2) NOT NULL CONSTRAINT DF_Sales_Print_Profile_TitleFontSize DEFAULT(15)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'SubTitleFontSize') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD SubTitleFontSize DECIMAL(6,2) NOT NULL CONSTRAINT DF_Sales_Print_Profile_SubTitleFontSize DEFAULT(10)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'InfoFontSize') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD InfoFontSize DECIMAL(6,2) NOT NULL CONSTRAINT DF_Sales_Print_Profile_InfoFontSize DEFAULT(9)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'HeaderFontSize') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD HeaderFontSize DECIMAL(6,2) NOT NULL CONSTRAINT DF_Sales_Print_Profile_HeaderFontSize DEFAULT(8)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'RowFontSize') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD RowFontSize DECIMAL(6,2) NOT NULL CONSTRAINT DF_Sales_Print_Profile_RowFontSize DEFAULT(8)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'TotalFontSize') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD TotalFontSize DECIMAL(6,2) NOT NULL CONSTRAINT DF_Sales_Print_Profile_TotalFontSize DEFAULT(9)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'FooterFontSize') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD FooterFontSize DECIMAL(6,2) NOT NULL CONSTRAINT DF_Sales_Print_Profile_FooterFontSize DEFAULT(8)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'TitleForeColorArgb') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD TitleForeColorArgb INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_TitleForeColorArgb DEFAULT(-16777216)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'TextForeColorArgb') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD TextForeColorArgb INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_TextForeColorArgb DEFAULT(-16777216)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'HeaderBackColorArgb') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD HeaderBackColorArgb INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_HeaderBackColorArgb DEFAULT(-13812144)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'HeaderForeColorArgb') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD HeaderForeColorArgb INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_HeaderForeColorArgb DEFAULT(-1)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'RowBackColorArgb') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD RowBackColorArgb INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_RowBackColorArgb DEFAULT(-1)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'AlternateRowBackColorArgb') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD AlternateRowBackColorArgb INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_AlternateRowBackColorArgb DEFAULT(-525828)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'BorderColorArgb') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD BorderColorArgb INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_BorderColorArgb DEFAULT(-2894893)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'TotalBackColorArgb') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD TotalBackColorArgb INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_TotalBackColorArgb DEFAULT(-1253131)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'TotalForeColorArgb') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD TotalForeColorArgb INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_TotalForeColorArgb DEFAULT(-16777216)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'FooterForeColorArgb') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD FooterForeColorArgb INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_FooterForeColorArgb DEFAULT(-8355712)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'UseAlternatingRows') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD UseAlternatingRows BIT NOT NULL CONSTRAINT DF_Sales_Print_Profile_UseAlternatingRows DEFAULT(1)",
            "IF COL_LENGTH('dbo.Sales_Print_Profile', 'DrawGridLines') IS NULL ALTER TABLE dbo.Sales_Print_Profile ADD DrawGridLines BIT NOT NULL CONSTRAINT DF_Sales_Print_Profile_DrawGridLines DEFAULT(1)"
        }

        For Each script As String In scripts
            Using cmd As New SqlCommand(script, cn)
                cmd.ExecuteNonQuery()
            End Using
        Next
    End Sub

    Private Function GetCreateProfileTableSql() As String
        Return "
IF OBJECT_ID(N'dbo.Sales_Print_Profile', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Sales_Print_Profile
    (
        ProfileID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Sales_Print_Profile PRIMARY KEY,
        ProfileName NVARCHAR(150) NOT NULL,
        UsageKey NVARCHAR(50) NOT NULL CONSTRAINT DF_Sales_Print_Profile_UsageKey DEFAULT(N'SALES'),
        PaperKind NVARCHAR(20) NOT NULL,
        PrinterName NVARCHAR(250) NOT NULL CONSTRAINT DF_Sales_Print_Profile_PrinterName DEFAULT(N''),
        IsDefault BIT NOT NULL CONSTRAINT DF_Sales_Print_Profile_IsDefault DEFAULT(0),
        Landscape BIT NOT NULL CONSTRAINT DF_Sales_Print_Profile_Landscape DEFAULT(0),
        MarginLeft INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_MarginLeft DEFAULT(35),
        MarginRight INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_MarginRight DEFAULT(35),
        MarginTop INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_MarginTop DEFAULT(40),
        MarginBottom INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_MarginBottom DEFAULT(45),
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Sales_Print_Profile_CreatedAt DEFAULT(GETDATE()),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Sales_Print_Profile_UpdatedAt DEFAULT(GETDATE())
    )
END"
    End Function

    Private Function GetCreateComponentTableSql() As String
        Return "
IF OBJECT_ID(N'dbo.Sales_Print_Profile_Component', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Sales_Print_Profile_Component
    (
        ComponentID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Sales_Print_Profile_Component PRIMARY KEY,
        ProfileID INT NOT NULL,
        ComponentScope NVARCHAR(20) NOT NULL,
        ComponentCode NVARCHAR(100) NOT NULL,
        DisplayName NVARCHAR(200) NOT NULL,
        IsVisible BIT NOT NULL CONSTRAINT DF_Sales_Print_Profile_Component_IsVisible DEFAULT(1),
        SortOrder INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_Component_SortOrder DEFAULT(0),
        WidthValue INT NOT NULL CONSTRAINT DF_Sales_Print_Profile_Component_WidthValue DEFAULT(80),
        AlignmentValue NVARCHAR(20) NOT NULL CONSTRAINT DF_Sales_Print_Profile_Component_AlignmentValue DEFAULT(N'Center'),
        CONSTRAINT FK_Sales_Print_Profile_Component_Profile FOREIGN KEY(ProfileID) REFERENCES dbo.Sales_Print_Profile(ProfileID) ON DELETE CASCADE
    )
END"
    End Function

End Class
