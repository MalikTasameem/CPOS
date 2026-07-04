using System.Data;
using CPOS.PosApi.Data;
using CPOS.PosApi.Models.Responses;
using Microsoft.Data.SqlClient;

namespace CPOS.PosApi.Services;

public sealed class TablesService
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public TablesService(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<TablesResponse> GetTablesAsync(int flateId, string status, bool includeLayout, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        List<TableDto> tables = await LoadTablesAsync(cn, flateId, status, cancellationToken);
        List<TableLayoutElementDto> layout = includeLayout
            ? await LoadLayoutAsync(cn, flateId, tables, cancellationToken)
            : new List<TableLayoutElementDto>();

        return new TablesResponse
        {
            Tables = tables,
            Layout = layout
        };
    }

    private static async Task<List<TableDto>> LoadTablesAsync(SqlConnection cn, int flateId, string status, CancellationToken cancellationToken)
    {
        List<TableDto> tables = new();

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"
SELECT TB_ID, Flate_ID, T_Name, Flate_Name, isbusy, is_Cash
FROM dbo.Tables_Balances_V
WHERE (@Flate_ID = 0 OR Flate_ID = @Flate_ID)
ORDER BY TB_ID ASC";
        cmd.Parameters.Add("@Flate_ID", SqlDbType.Int).Value = flateId;

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await dr.ReadAsync(cancellationToken))
        {
            bool isBusy = GetBool(dr["isbusy"]);
            if (ShouldIncludeStatus(status, isBusy) == false) continue;

            tables.Add(new TableDto
            {
                TableId = GetInt(dr["TB_ID"]),
                FlateId = GetInt(dr["Flate_ID"]),
                TableName = GetString(dr["T_Name"]),
                FlateName = GetString(dr["Flate_Name"]),
                IsBusy = isBusy,
                IsCash = GetBool(dr["is_Cash"])
            });
        }

        return tables;
    }

    private static async Task<List<TableLayoutElementDto>> LoadLayoutAsync(SqlConnection cn, int flateId, IReadOnlyList<TableDto> tables, CancellationToken cancellationToken)
    {
        Dictionary<int, TableDto> tableMap = tables.ToDictionary(x => x.TableId);
        List<TableLayoutElementDto> layout = new();

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"
IF OBJECT_ID(N'dbo.Restaurant_Floor_Layout', N'U') IS NULL
BEGIN
    SELECT TOP 0
        CAST(NULL AS INT) AS Layout_ID,
        CAST(NULL AS INT) AS Flate_ID,
        CAST(NULL AS INT) AS TB_ID,
        CAST(NULL AS NVARCHAR(30)) AS ElementType,
        CAST(NULL AS NVARCHAR(100)) AS ElementText,
        CAST(NULL AS INT) AS X_Pos,
        CAST(NULL AS INT) AS Y_Pos,
        CAST(NULL AS INT) AS WidthValue,
        CAST(NULL AS INT) AS HeightValue,
        CAST(NULL AS INT) AS RotationValue,
        CAST(NULL AS INT) AS SeatsCount,
        CAST(NULL AS INT) AS BackColorArgb,
        CAST(NULL AS INT) AS ForeColorArgb,
        CAST(NULL AS INT) AS ZIndex;
END
ELSE
BEGIN
    SELECT Layout_ID, Flate_ID, TB_ID, ElementType, ElementText, X_Pos, Y_Pos, WidthValue, HeightValue, RotationValue, SeatsCount, BackColorArgb, ForeColorArgb, ZIndex
    FROM dbo.Restaurant_Floor_Layout
    WHERE (@Flate_ID = 0 OR Flate_ID = @Flate_ID)
      AND IsActive = 1
    ORDER BY Flate_ID ASC, ZIndex ASC, Layout_ID ASC;
END";
        cmd.Parameters.Add("@Flate_ID", SqlDbType.Int).Value = flateId;

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await dr.ReadAsync(cancellationToken))
        {
            int? tableId = GetNullableInt(dr["TB_ID"]);
            tableMap.TryGetValue(tableId.GetValueOrDefault(), out TableDto? table);

            layout.Add(new TableLayoutElementDto
            {
                LayoutId = GetInt(dr["Layout_ID"]),
                FlateId = GetInt(dr["Flate_ID"]),
                TableId = tableId,
                ElementType = GetString(dr["ElementType"]),
                ElementText = table?.TableName ?? GetString(dr["ElementText"]),
                X = GetInt(dr["X_Pos"]),
                Y = GetInt(dr["Y_Pos"]),
                Width = GetInt(dr["WidthValue"]),
                Height = GetInt(dr["HeightValue"]),
                Rotation = GetInt(dr["RotationValue"]),
                SeatsCount = GetInt(dr["SeatsCount"]),
                BackColorArgb = GetNullableInt(dr["BackColorArgb"]),
                ForeColorArgb = GetNullableInt(dr["ForeColorArgb"]),
                ZIndex = GetInt(dr["ZIndex"]),
                IsBusy = table?.IsBusy ?? false,
                IsCash = table?.IsCash ?? false
            });
        }

        return layout;
    }

    private static bool ShouldIncludeStatus(string status, bool isBusy)
    {
        return status.Trim().ToLowerInvariant() switch
        {
            "busy" or "open" or "opened" => isBusy,
            "free" or "empty" or "available" => isBusy == false,
            _ => true
        };
    }

    private static int GetInt(object value)
    {
        return value is DBNull ? 0 : Convert.ToInt32(value);
    }

    private static int? GetNullableInt(object value)
    {
        return value is DBNull ? null : Convert.ToInt32(value);
    }

    private static string GetString(object value)
    {
        return value is DBNull ? "" : value.ToString() ?? "";
    }

    private static bool GetBool(object value)
    {
        if (value is DBNull) return false;
        if (value is bool boolValue) return boolValue;

        string text = value.ToString()?.Trim() ?? "";
        if (bool.TryParse(text, out bool parsedBool)) return parsedBool;
        if (decimal.TryParse(text, out decimal parsedDecimal)) return parsedDecimal != 0M;

        return false;
    }
}
