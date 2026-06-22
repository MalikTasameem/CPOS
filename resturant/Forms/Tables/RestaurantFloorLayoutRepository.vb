Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Public Class RestaurantFloorLayoutRepository

    Public Sub EnsureSchema()
        Dim sql As String =
"IF OBJECT_ID(N'dbo.Restaurant_Floor_Layout', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Restaurant_Floor_Layout
    (
        Layout_ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Restaurant_Floor_Layout PRIMARY KEY,
        Flate_ID INT NOT NULL,
        TB_ID INT NULL,
        ElementType NVARCHAR(30) NOT NULL,
        ElementText NVARCHAR(100) NULL,
        X_Pos INT NOT NULL CONSTRAINT DF_Restaurant_Floor_Layout_X DEFAULT(0),
        Y_Pos INT NOT NULL CONSTRAINT DF_Restaurant_Floor_Layout_Y DEFAULT(0),
        WidthValue INT NOT NULL CONSTRAINT DF_Restaurant_Floor_Layout_W DEFAULT(110),
        HeightValue INT NOT NULL CONSTRAINT DF_Restaurant_Floor_Layout_H DEFAULT(80),
        RotationValue INT NOT NULL CONSTRAINT DF_Restaurant_Floor_Layout_R DEFAULT(0),
        SeatsCount INT NOT NULL CONSTRAINT DF_Restaurant_Floor_Layout_Seats DEFAULT(4),
        BackColorArgb INT NULL,
        ForeColorArgb INT NULL,
        ZIndex INT NOT NULL CONSTRAINT DF_Restaurant_Floor_Layout_Z DEFAULT(0),
        IsActive BIT NOT NULL CONSTRAINT DF_Restaurant_Floor_Layout_Active DEFAULT(1),
        CreatedAt DATETIME NOT NULL CONSTRAINT DF_Restaurant_Floor_Layout_Created DEFAULT(GETDATE()),
        UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Restaurant_Floor_Layout_Updated DEFAULT(GETDATE())
    );
END"

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand(sql, cn)
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function LoadFlates() As DataTable
        Dim dt As New DataTable()
        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using da As New SqlDataAdapter("SELECT Flate_ID, Flate_Name FROM Tables_Flate ORDER BY Flate_ID ASC", cn)
                da.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    Public Function LoadTables(flateId As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sql As String = "SELECT TB_ID, Flate_ID, T_Name, Flate_Name, isbusy, is_Cash FROM Tables_Balances_V WHERE (@Flate_ID = 0 OR Flate_ID = @Flate_ID) ORDER BY TB_ID ASC"

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.Add("@Flate_ID", SqlDbType.Int).Value = flateId
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Function LoadLayout(flateId As Integer) As List(Of RestaurantFloorElement)
        EnsureSchema()

        Dim list As New List(Of RestaurantFloorElement)()
        Dim sql As String = "SELECT Layout_ID, Flate_ID, TB_ID, ElementType, ElementText, X_Pos, Y_Pos, WidthValue, HeightValue, RotationValue, SeatsCount, BackColorArgb, ForeColorArgb, ZIndex FROM dbo.Restaurant_Floor_Layout WHERE (@Flate_ID = 0 OR Flate_ID = @Flate_ID) AND IsActive = 1 ORDER BY Flate_ID ASC, ZIndex ASC, Layout_ID ASC"

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.Add("@Flate_ID", SqlDbType.Int).Value = flateId
                cn.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim element As New RestaurantFloorElement()
                        element.Layout_ID = GetInt(dr("Layout_ID"), 0)
                        element.Flate_ID = GetInt(dr("Flate_ID"), flateId)

                        If dr("TB_ID") Is Nothing OrElse dr("TB_ID") Is DBNull.Value Then
                            element.TB_ID = Nothing
                        Else
                            element.TB_ID = GetInt(dr("TB_ID"), 0)
                        End If

                        element.ElementType = dr("ElementType").ToString()
                        element.ElementText = If(dr("ElementText") Is DBNull.Value, "", dr("ElementText").ToString())
                        element.X_Pos = GetInt(dr("X_Pos"), 0)
                        element.Y_Pos = GetInt(dr("Y_Pos"), 0)
                        element.WidthValue = GetInt(dr("WidthValue"), 110)
                        element.HeightValue = GetInt(dr("HeightValue"), 80)
                        element.RotationValue = GetInt(dr("RotationValue"), 0)
                        element.SeatsCount = GetInt(dr("SeatsCount"), 4)
                        element.BackColorArgb = GetInt(dr("BackColorArgb"), Color.WhiteSmoke.ToArgb())
                        element.ForeColorArgb = GetInt(dr("ForeColorArgb"), Color.FromArgb(15, 23, 42).ToArgb())
                        element.ZIndex = GetInt(dr("ZIndex"), 0)
                        list.Add(element)
                    End While
                End Using
            End Using
        End Using

        Return list
    End Function

    Public Sub SaveLayout(flateId As Integer, elements As List(Of RestaurantFloorElement))
        EnsureSchema()

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            cn.Open()
            Using tr As SqlTransaction = cn.BeginTransaction()
                Try
                    Using deleteCmd As New SqlCommand("DELETE FROM dbo.Restaurant_Floor_Layout WHERE Flate_ID = @Flate_ID", cn, tr)
                        deleteCmd.Parameters.Add("@Flate_ID", SqlDbType.Int).Value = flateId
                        deleteCmd.ExecuteNonQuery()
                    End Using

                    For Each element As RestaurantFloorElement In elements
                        Using cmd As New SqlCommand("INSERT INTO dbo.Restaurant_Floor_Layout(Flate_ID, TB_ID, ElementType, ElementText, X_Pos, Y_Pos, WidthValue, HeightValue, RotationValue, SeatsCount, BackColorArgb, ForeColorArgb, ZIndex, IsActive, CreatedAt, UpdatedAt) VALUES(@Flate_ID, @TB_ID, @ElementType, @ElementText, @X_Pos, @Y_Pos, @WidthValue, @HeightValue, @RotationValue, @SeatsCount, @BackColorArgb, @ForeColorArgb, @ZIndex, 1, GETDATE(), GETDATE())", cn, tr)
                            cmd.Parameters.Add("@Flate_ID", SqlDbType.Int).Value = flateId
                            cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = If(element.TB_ID.HasValue, CType(element.TB_ID.Value, Object), DBNull.Value)
                            cmd.Parameters.Add("@ElementType", SqlDbType.NVarChar, 30).Value = element.ElementType
                            cmd.Parameters.Add("@ElementText", SqlDbType.NVarChar, 100).Value = If(String.IsNullOrWhiteSpace(element.ElementText), CType(DBNull.Value, Object), element.ElementText)
                            cmd.Parameters.Add("@X_Pos", SqlDbType.Int).Value = element.X_Pos
                            cmd.Parameters.Add("@Y_Pos", SqlDbType.Int).Value = element.Y_Pos
                            cmd.Parameters.Add("@WidthValue", SqlDbType.Int).Value = element.WidthValue
                            cmd.Parameters.Add("@HeightValue", SqlDbType.Int).Value = element.HeightValue
                            cmd.Parameters.Add("@RotationValue", SqlDbType.Int).Value = element.RotationValue
                            cmd.Parameters.Add("@SeatsCount", SqlDbType.Int).Value = element.SeatsCount
                            cmd.Parameters.Add("@BackColorArgb", SqlDbType.Int).Value = element.BackColorArgb
                            cmd.Parameters.Add("@ForeColorArgb", SqlDbType.Int).Value = element.ForeColorArgb
                            cmd.Parameters.Add("@ZIndex", SqlDbType.Int).Value = element.ZIndex
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
    End Sub

    Public Sub ResetLayout(flateId As Integer)
        EnsureSchema()

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("DELETE FROM dbo.Restaurant_Floor_Layout WHERE Flate_ID = @Flate_ID", cn)
                cmd.Parameters.Add("@Flate_ID", SqlDbType.Int).Value = flateId
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function GetInt(value As Object, defaultValue As Integer) As Integer
        If value Is Nothing OrElse value Is DBNull.Value Then Return defaultValue

        Dim result As Integer = defaultValue
        Integer.TryParse(value.ToString(), result)
        Return result
    End Function

End Class
