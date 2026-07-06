using System.Data;
using CPOS.PosApi.Data;
using CPOS.PosApi.Models.Responses;
using Microsoft.Data.SqlClient;

namespace CPOS.PosApi.Services;

public sealed class PosCatalogService
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public PosCatalogService(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<PosBootstrapResponse> GetBootstrapAsync(CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"
SELECT GM_ID, GM_Name, Rank_Num, BK_R, BK_G, BK_B, FK_R, FK_G, FK_B
FROM dbo.General_menu
WHERE ISNULL(POS_isShow, 0) = 1
ORDER BY Rank_Num ASC;

SELECT IM_ID, GM_ID, item_nameSales AS item_name, item_nameSales, isValid,
       CASE WHEN Photo IS NULL THEN 0 ELSE 1 END AS HasPhoto,
       BK_R, BK_G, BK_B, FK_R, FK_G, FK_B
FROM dbo.IM_ActiveList_V
ORDER BY GM_ID ASC, IM_ID ASC;

SELECT U_IM_ID, IM_ID, item_name, U_Name, U_ID, U_Cargo, Price, Min_SP, Min_SP_2, Barcode, isValid, isStore, is_Default
FROM dbo.IM_Menu_Units_V
ORDER BY IM_ID ASC, U_ID ASC;

SELECT ST_ID, ST_name
FROM dbo.STORES
ORDER BY ST_ID ASC;

SELECT B_Type_ID, B_Name
FROM dbo.Sales_Bills_Types
ORDER BY B_Type_ID ASC;

SELECT p.PaymentMethodID AS P_ID, m.PAYMENT_NAME, p.AccountID AS Tr_ID, a.Tr_Name, ISNULL(p.is_Lock, 0) AS is_Lock
FROM dbo.PaymentMethodDefaultAccounts p
INNER JOIN dbo.PAYMENT_METHOD m ON p.PaymentMethodID = m.P_ID
INNER JOIN dbo.TreasuryCard a ON p.AccountID = a.Tr_ID
WHERE ISNULL(p.IsActive, 1) = 1
ORDER BY p.ID ASC;

SELECT TOP 1 Tr_ID, Tr_Name
FROM dbo.TreasuryCard
ORDER BY Tr_ID ASC;

SELECT TOP 1 AG_ID
FROM dbo.AGENTS
ORDER BY CASE WHEN ISNULL(isDefault, 0) = 1 THEN 0 ELSE 1 END, AG_ID ASC;";

        List<PosGroupDto> groups = new();
        List<PosItemDto> items = new();
        List<PosItemUnitDto> units = new();
        List<PosStoreDto> stores = new();
        List<PosBillTypeDto> billTypes = new();
        List<PosPaymentMethodDto> paymentMethods = new();
        int defaultAgentId = 0;

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await dr.ReadAsync(cancellationToken))
        {
            groups.Add(MapGroup(dr));
        }

        if (await dr.NextResultAsync(cancellationToken))
        {
            while (await dr.ReadAsync(cancellationToken))
            {
                items.Add(MapItem(dr));
            }
        }

        if (await dr.NextResultAsync(cancellationToken))
        {
            while (await dr.ReadAsync(cancellationToken))
            {
                units.Add(MapUnit(dr));
            }
        }

        if (await dr.NextResultAsync(cancellationToken))
        {
            while (await dr.ReadAsync(cancellationToken))
            {
                stores.Add(new PosStoreDto
                {
                    StoreId = GetInt(dr, "ST_ID"),
                    StoreName = GetString(dr, "ST_name")
                });
            }
        }

        if (await dr.NextResultAsync(cancellationToken))
        {
            while (await dr.ReadAsync(cancellationToken))
            {
                billTypes.Add(new PosBillTypeDto
                {
                    BillTypeId = GetInt(dr, "B_Type_ID"),
                    BillTypeName = GetString(dr, "B_Name")
                });
            }
        }

        if (await dr.NextResultAsync(cancellationToken))
        {
            while (await dr.ReadAsync(cancellationToken))
            {
                paymentMethods.Add(MapPaymentMethod(dr));
            }
        }

        if (await dr.NextResultAsync(cancellationToken))
        {
            if (paymentMethods.Count == 0 && await dr.ReadAsync(cancellationToken))
            {
                paymentMethods.Add(new PosPaymentMethodDto
                {
                    PaymentId = 1,
                    PaymentName = "نقدا",
                    TreasuryId = GetInt(dr, "Tr_ID"),
                    TreasuryName = GetString(dr, "Tr_Name"),
                    IsLocked = false
                });
            }
        }

        if (await dr.NextResultAsync(cancellationToken))
        {
            if (await dr.ReadAsync(cancellationToken))
            {
                defaultAgentId = GetInt(dr, "AG_ID");
            }
        }

        return new PosBootstrapResponse
        {
            Groups = groups,
            Items = items,
            Units = units,
            Stores = stores,
            BillTypes = billTypes,
            PaymentMethods = paymentMethods,
            DefaultAgentId = defaultAgentId
        };
    }

    public async Task<IReadOnlyList<PosGroupDto>> GetGroupsAsync(CancellationToken cancellationToken)
    {
        List<PosGroupDto> groups = new();

        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"
SELECT GM_ID, GM_Name, Rank_Num, BK_R, BK_G, BK_B, FK_R, FK_G, FK_B
FROM dbo.General_menu
WHERE ISNULL(POS_isShow, 0) = 1
ORDER BY Rank_Num ASC";

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await dr.ReadAsync(cancellationToken))
        {
            groups.Add(MapGroup(dr));
        }

        return groups;
    }

    public async Task<IReadOnlyList<PosItemDto>> GetItemsAsync(int? groupId, string? search, CancellationToken cancellationToken)
    {
        List<PosItemDto> items = new();

        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"
SELECT IM_ID, GM_ID, item_nameSales AS item_name, item_nameSales, isValid,
       CASE WHEN Photo IS NULL THEN 0 ELSE 1 END AS HasPhoto,
       BK_R, BK_G, BK_B, FK_R, FK_G, FK_B
FROM dbo.IM_ActiveList_V
WHERE (@GM_ID IS NULL OR GM_ID = @GM_ID)
  AND (@Search = N'' OR item_nameSales LIKE N'%' + @Search + N'%')
ORDER BY GM_ID ASC, IM_ID ASC";
        cmd.Parameters.Add("@GM_ID", SqlDbType.Int).Value = groupId.HasValue && groupId.Value > 0 ? groupId.Value : DBNull.Value;
        cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = search?.Trim() ?? "";

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await dr.ReadAsync(cancellationToken))
        {
            items.Add(MapItem(dr));
        }

        return items;
    }

    public async Task<IReadOnlyList<PosItemUnitDto>> GetItemUnitsAsync(int itemId, CancellationToken cancellationToken)
    {
        List<PosItemUnitDto> units = new();

        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"
SELECT U_IM_ID, IM_ID, item_name, U_Name, U_ID, U_Cargo, Price, Min_SP, Min_SP_2, Barcode, isValid, isStore, is_Default
FROM dbo.IM_Menu_Units_V
WHERE IM_ID = @IM_ID
ORDER BY U_ID ASC";
        cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = itemId;

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await dr.ReadAsync(cancellationToken))
        {
            units.Add(MapUnit(dr));
        }

        return units;
    }

    public async Task<PosItemUnitDto?> GetItemByBarcodeAsync(string barcode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(barcode)) return null;

        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"
SELECT TOP 1 U_IM_ID, IM_ID, item_name, U_Name, U_ID, U_Cargo, Price, Barcode, isValid, is_Default
FROM dbo.IM_units_Search_V
WHERE Barcode = @Barcode
ORDER BY IM_ID ASC, U_IM_ID ASC";
        cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 100).Value = barcode.Trim();

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken);
        if (await dr.ReadAsync(cancellationToken) == false) return null;

        return MapUnit(dr);
    }

    private static PosGroupDto MapGroup(SqlDataReader dr)
    {
        return new PosGroupDto
        {
            GroupId = GetInt(dr, "GM_ID"),
            GroupName = GetString(dr, "GM_Name"),
            RankNumber = GetInt(dr, "Rank_Num"),
            BackgroundColor = GetColor(dr, "BK_R", "BK_G", "BK_B"),
            ForegroundColor = GetColor(dr, "FK_R", "FK_G", "FK_B")
        };
    }

    private static PosItemDto MapItem(SqlDataReader dr)
    {
        string itemName = GetString(dr, "item_name");
        string salesName = GetString(dr, "item_nameSales");

        return new PosItemDto
        {
            ItemId = GetInt(dr, "IM_ID"),
            GroupId = GetInt(dr, "GM_ID"),
            ItemName = itemName,
            SalesName = string.IsNullOrWhiteSpace(salesName) ? itemName : salesName,
            IsValid = GetBool(dr, "isValid"),
            HasPhoto = GetBool(dr, "HasPhoto"),
            BackgroundColor = GetColor(dr, "BK_R", "BK_G", "BK_B"),
            ForegroundColor = GetColor(dr, "FK_R", "FK_G", "FK_B")
        };
    }

    private static PosItemUnitDto MapUnit(SqlDataReader dr)
    {
        return new PosItemUnitDto
        {
            UnitItemId = GetInt(dr, "U_IM_ID"),
            ItemId = GetInt(dr, "IM_ID"),
            ItemName = GetString(dr, "item_name"),
            UnitId = GetInt(dr, "U_ID"),
            UnitName = GetString(dr, "U_Name"),
            UnitCargo = GetDecimal(dr, "U_Cargo"),
            Price = GetDecimal(dr, "Price"),
            MinSalesPrice = GetDecimal(dr, "Min_SP"),
            MinSalesPrice2 = GetDecimal(dr, "Min_SP_2"),
            Barcode = GetString(dr, "Barcode"),
            IsValid = GetBool(dr, "isValid"),
            IsStore = GetBool(dr, "isStore"),
            IsDefault = GetBool(dr, "is_Default")
        };
    }

    private static PosPaymentMethodDto MapPaymentMethod(SqlDataReader dr)
    {
        return new PosPaymentMethodDto
        {
            PaymentId = GetInt(dr, "P_ID"),
            PaymentName = GetString(dr, "PAYMENT_NAME"),
            TreasuryId = GetInt(dr, "Tr_ID"),
            TreasuryName = GetString(dr, "Tr_Name"),
            IsLocked = GetBool(dr, "is_Lock")
        };
    }

    private static string GetColor(SqlDataReader dr, string redName, string greenName, string blueName)
    {
        int? red = GetNullableInt(dr, redName);
        int? green = GetNullableInt(dr, greenName);
        int? blue = GetNullableInt(dr, blueName);

        if (red.HasValue == false || green.HasValue == false || blue.HasValue == false)
        {
            return "";
        }

        return $"#{ClampColor(red.Value):X2}{ClampColor(green.Value):X2}{ClampColor(blue.Value):X2}";
    }

    private static int ClampColor(int value)
    {
        if (value < 0) return 0;
        if (value > 255) return 255;
        return value;
    }

    private static bool HasColumn(SqlDataReader dr, string name)
    {
        for (int i = 0; i < dr.FieldCount; i++)
        {
            if (string.Equals(dr.GetName(i), name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    private static int GetInt(SqlDataReader dr, string name)
    {
        if (HasColumn(dr, name) == false) return 0;
        object value = dr[name];
        return value is DBNull ? 0 : Convert.ToInt32(value);
    }

    private static int? GetNullableInt(SqlDataReader dr, string name)
    {
        if (HasColumn(dr, name) == false) return null;
        object value = dr[name];
        return value is DBNull ? null : Convert.ToInt32(value);
    }

    private static decimal GetDecimal(SqlDataReader dr, string name)
    {
        if (HasColumn(dr, name) == false) return 0M;
        object value = dr[name];
        return value is DBNull ? 0M : Convert.ToDecimal(value);
    }

    private static string GetString(SqlDataReader dr, string name)
    {
        if (HasColumn(dr, name) == false) return "";
        object value = dr[name];
        return value is DBNull ? "" : value.ToString() ?? "";
    }

    private static bool GetBool(SqlDataReader dr, string name)
    {
        if (HasColumn(dr, name) == false) return false;
        object value = dr[name];
        if (value is DBNull) return false;
        if (value is bool boolValue) return boolValue;

        string text = value.ToString()?.Trim() ?? "";
        if (bool.TryParse(text, out bool parsedBool)) return parsedBool;
        if (decimal.TryParse(text, out decimal parsedDecimal)) return parsedDecimal != 0M;

        return false;
    }
}
