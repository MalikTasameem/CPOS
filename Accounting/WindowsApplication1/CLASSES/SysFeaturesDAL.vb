Imports System.Data
Imports System.Data.SqlClient

Public Class SysFeaturesDAL
    Private _connectionString As String

    ' Constructor لتخزين connection string
    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub

    Public Function UpdateSysFeatures(m As SysFeaturesModel) As Integer
        Using conn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand("[dbo].[SYS_Features_ACCOUNTING_UPDATE]", conn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim ToDb As Func(Of Object, Object) = Function(v) If(v Is Nothing, DBNull.Value, v)

                cmd.Parameters.Add("@Pure_Income_ACC_CODE", SqlDbType.VarChar, 40).Value = ToDb(m.Pure_Income_ACC_CODE)
                cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 20).Value = ToDb(m.Prefix)
                cmd.Parameters.Add("@NumberLength", SqlDbType.Int).Value = m.NumberLength
                cmd.Parameters.Add("@ResetType", SqlDbType.NVarChar, 10).Value = ToDb(m.ResetType)
                cmd.Parameters.Add("@is_Link_With_SB", SqlDbType.Bit).Value = m.is_Link_With_SB
                cmd.Parameters.Add("@SALES_DB", SqlDbType.VarChar, 500).Value = ToDb(m.SALES_DB)
                cmd.Parameters.Add("@SBill_Title_1", SqlDbType.NVarChar, -1).Value = ToDb(m.SBill_Title_1)
                cmd.Parameters.Add("@SBill_Title_2", SqlDbType.NVarChar, -1).Value = ToDb(m.SBill_Title_2)
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, -1).Value = ToDb(m.Address)
                cmd.Parameters.Add("@Phone_Number", SqlDbType.NVarChar, 500).Value = ToDb(m.Phone_Number)
                cmd.Parameters.Add("@is_Dark_mode", SqlDbType.Bit).Value = m.is_Dark_mode
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 500).Value = ToDb(m.Email)

                conn.Open()

                If SQL_SP_EXEC(cmd) Then
                    UpdateBudgetFeatureSettings(m)
                    Return 1
                Else
                    Return 0
                End If


            End Using
        End Using
    End Function

    Private Sub UpdateBudgetFeatureSettings(m As SysFeaturesModel)
        Using conn As New SqlConnection(_connectionString)
            Dim sql As String =
                "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Use_State_Budget') IS NULL " &
                "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Use_State_Budget BIT NOT NULL CONSTRAINT DF_SYS_Features_ACOUNTING_Use_State_Budget DEFAULT(0); " &
                "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Allow_Budget_OverSpend') IS NULL " &
                "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Allow_Budget_OverSpend BIT NOT NULL CONSTRAINT DF_SYS_Features_ACOUNTING_Allow_Budget_OverSpend DEFAULT(0); " &
                "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Default_Stamp_Percent') IS NULL " &
                "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Default_Stamp_Percent DECIMAL(18,3) NULL; " &
                "IF COL_LENGTH('dbo.SYS_Features_ACOUNTING','Default_Stamp_Account_Code') IS NULL " &
                "ALTER TABLE dbo.SYS_Features_ACOUNTING ADD Default_Stamp_Account_Code NVARCHAR(40) NULL; " &
                "UPDATE dbo.SYS_Features_ACOUNTING SET Use_State_Budget = @Use_State_Budget, Allow_Budget_OverSpend = @Allow_Budget_OverSpend, Default_Stamp_Percent = @Default_Stamp_Percent, Default_Stamp_Account_Code = @Default_Stamp_Account_Code;"

            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.Add("@Use_State_Budget", SqlDbType.Bit).Value = m.Use_State_Budget
                cmd.Parameters.Add("@Allow_Budget_OverSpend", SqlDbType.Bit).Value = (m.Use_State_Budget AndAlso m.Allow_Budget_OverSpend)
                cmd.Parameters.Add("@Default_Stamp_Percent", SqlDbType.Decimal).Value = m.Default_Stamp_Percent
                cmd.Parameters("@Default_Stamp_Percent").Precision = 18
                cmd.Parameters("@Default_Stamp_Percent").Scale = 3
                cmd.Parameters.Add("@Default_Stamp_Account_Code", SqlDbType.NVarChar, 40).Value =
                    If(String.IsNullOrWhiteSpace(m.Default_Stamp_Account_Code), CType(DBNull.Value, Object), m.Default_Stamp_Account_Code.Trim())
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class

