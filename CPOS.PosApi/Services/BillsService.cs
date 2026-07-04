using System.Data;
using CPOS.PosApi.Data;
using CPOS.PosApi.Models.Requests;
using CPOS.PosApi.Models.Responses;
using Microsoft.Data.SqlClient;

namespace CPOS.PosApi.Services;

public sealed class BillsService
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public BillsService(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<OpenBillResponse> OpenTableBillAsync(int tableId, OpenTableBillRequest request, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        int periodId = request.PeriodId ?? await GetOpenPeriodIdAsync(cn, request.UserId, cancellationToken);
        int agentId = request.AgentId > 0 ? request.AgentId : await GetDefaultAgentIdAsync(cn, cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Call_New_SalesBill";

        SqlParameter tIdParam = cmd.Parameters.Add("@T_ID", SqlDbType.Int);
        tIdParam.Value = 0;
        tIdParam.Direction = ParameterDirection.Output;

        cmd.Parameters.Add("@AG_ID", SqlDbType.Int).Value = agentId;

        SqlParameter billNumParam = cmd.Parameters.Add("@Bill_Num", SqlDbType.Int);
        billNumParam.Value = 0;
        billNumParam.Direction = ParameterDirection.Output;

        SqlParameter salesBillIdParam = cmd.Parameters.Add("@SB_ID", SqlDbType.Int);
        salesBillIdParam.Value = 0;
        salesBillIdParam.Direction = ParameterDirection.Output;

        SqlParameter isNewParam = cmd.Parameters.Add("@isNew", SqlDbType.Int);
        isNewParam.Value = 0;
        isNewParam.Direction = ParameterDirection.Output;

        cmd.Parameters.Add("@SB_Type", SqlDbType.Int).Value = request.BillTypeId;
        cmd.Parameters.Add("@Pr_ID", SqlDbType.Int).Value = periodId;
        cmd.Parameters.Add("@isPied", SqlDbType.Int).Value = 0;
        cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = request.UserId;
        cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = tableId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);

        int transactionId = GetOutputInt(tIdParam);

        return new OpenBillResponse
        {
            TransactionId = transactionId,
            DailyBillNumber = GetOutputInt(billNumParam),
            SalesBillId = GetOutputInt(salesBillIdParam),
            IsNew = GetOutputInt(isNewParam) != 0,
            Bill = await GetBillAsync(cn, transactionId, cancellationToken)
        };
    }

    public async Task<BillDto?> GetBillAsync(int transactionId, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        return await GetBillAsync(cn, transactionId, cancellationToken);
    }

    public async Task<BillDto?> AddItemAsync(int transactionId, AddBillItemRequest request, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_Contents_INSERT";
        cmd.Parameters.Add("@SB_T_ID", SqlDbType.Int).Value = transactionId;
        cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = request.ItemId;
        cmd.Parameters.Add("@ST_ID", SqlDbType.Int).Value = request.StoreId;
        cmd.Parameters.Add("@U_IM_ID", SqlDbType.Int).Value = request.UnitItemId;
        cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 100).Value = request.Barcode ?? "";

        if (string.IsNullOrWhiteSpace(request.ValidDate) == false)
        {
            cmd.Parameters.Add("@D_Vaild", SqlDbType.NVarChar, 50).Value = request.ValidDate;
        }

        if (request.Quantity.HasValue)
        {
            cmd.Parameters.Add("@QYT", SqlDbType.Decimal).Value = request.Quantity.Value;
        }

        if (request.Price.HasValue)
        {
            cmd.Parameters.Add("@IM_Price", SqlDbType.Decimal).Value = request.Price.Value;
        }

        cmd.Parameters.Add("@On_Update", SqlDbType.Bit).Value = request.OnUpdate;
        cmd.Parameters.Add("@SALES_TYPES", SqlDbType.Int).Value = request.SalesTypeId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await GetBillAsync(cn, transactionId, cancellationToken);
    }

    public async Task<BillDto?> ChangeItemQuantityAsync(int detailId, ChangeBillItemQtyRequest request, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        int transactionId = await GetBillTransactionIdByDetailIdAsync(cn, detailId, cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_Contents_Change_IM_Qty";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = detailId;
        cmd.Parameters.Add("@Def", SqlDbType.Int).Value = request.ChangeBy;
        cmd.Parameters.Add("@On_Update", SqlDbType.Bit).Value = request.OnUpdate;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await GetBillAsync(cn, transactionId, cancellationToken);
    }

    public async Task<BillDto?> UpdateItemDetailsAsync(int detailId, UpdateBillItemDetailsRequest request, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        int transactionId = await GetBillTransactionIdByDetailIdAsync(cn, detailId, cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_IM_Unit_Change";
        cmd.Parameters.Add("@SB_T_ID", SqlDbType.Int).Value = transactionId;
        cmd.Parameters.Add("@IM_T_ID", SqlDbType.Int).Value = detailId;
        cmd.Parameters.Add("@U_ID", SqlDbType.Int).Value = request.UnitId;
        cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = request.Notes ?? "";
        cmd.Parameters.Add("@On_Update", SqlDbType.Bit).Value = request.OnUpdate;
        cmd.Parameters.Add("@Sales_Type", SqlDbType.Int).Value = request.SalesTypeId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await GetBillAsync(cn, transactionId, cancellationToken);
    }

    public async Task<BillDto?> DeleteItemAsync(int detailId, bool onUpdate, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        int transactionId = await GetBillTransactionIdByDetailIdAsync(cn, detailId, cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_Contents_Delete_IM";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = detailId;
        cmd.Parameters.Add("@On_Update", SqlDbType.Bit).Value = onUpdate;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await GetBillAsync(cn, transactionId, cancellationToken);
    }

    public async Task<BillDto?> SendTableOrderAsync(int transactionId, SendOrderRequest request, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "AG_Balance_Update_TB_Ordered";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = transactionId;
        cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = request.TableId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await GetBillAsync(cn, transactionId, cancellationToken);
    }

    private static async Task<BillDto?> GetBillAsync(SqlConnection cn, int transactionId, CancellationToken cancellationToken)
    {
        BillDto? bill = await LoadBillHeaderAsync(cn, transactionId, cancellationToken);
        if (bill is null) return null;

        bill.Items = await LoadBillItemsAsync(cn, transactionId, cancellationToken);
        bill.Total = bill.Items.Sum(x => x.Total);
        if (bill.Pure == 0 && bill.Total > 0)
        {
            bill.Pure = bill.Total - bill.Discount;
        }

        return bill;
    }

    private static async Task<BillDto?> LoadBillHeaderAsync(SqlConnection cn, int transactionId, CancellationToken cancellationToken)
    {
        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_Info_V_SELECT_Bill";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = transactionId;

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken);
        if (await dr.ReadAsync(cancellationToken) == false) return null;

        decimal total = GetDecimal(dr, "Total");
        decimal discount = GetDecimal(dr, "Discount");

        return new BillDto
        {
            TransactionId = transactionId,
            SalesBillId = GetInt(dr, "SB_ID"),
            DailyBillNumber = GetInt(dr, "S_Bill_Pr_ID"),
            BillTypeId = GetInt(dr, "S_Bills_Type"),
            AgentId = GetInt(dr, "AG_ID"),
            AgentName = GetString(dr, "Ag_name"),
            TableId = GetInt(dr, "TB_ID"),
            TableName = GetString(dr, "T_Name"),
            Barcode = GetString(dr, "Barcode"),
            BillDate = GetString(dr, "date"),
            CustomerPhone = GetString(dr, "Cr_Phone"),
            IsDepended = GetBool(dr, "isDepended"),
            IsVoid = GetBool(dr, "isVoid"),
            IsPaid = GetBool(dr, "isPied"),
            IsOrdered = GetBool(dr, "TB_isOrderd"),
            Total = total,
            Discount = discount,
            Pure = total - discount
        };
    }

    private static async Task<IReadOnlyList<BillItemDto>> LoadBillItemsAsync(SqlConnection cn, int transactionId, CancellationToken cancellationToken)
    {
        List<BillItemDto> items = new();

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"
SELECT T_ID, IM_ID, U_ID, QTY, IM_Name, Unit_Name, Price, T_Price, Notes, Ptr_ID
FROM dbo.SB_Contents_V
WHERE Bill_T_ID = @SB_T_ID
ORDER BY T_ID ASC";
        cmd.Parameters.Add("@SB_T_ID", SqlDbType.Int).Value = transactionId;

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await dr.ReadAsync(cancellationToken))
        {
            items.Add(new BillItemDto
            {
                DetailId = GetInt(dr, "T_ID"),
                ItemId = GetInt(dr, "IM_ID"),
                UnitId = GetInt(dr, "U_ID"),
                Quantity = GetDecimal(dr, "QTY"),
                ItemName = GetString(dr, "IM_Name"),
                UnitName = GetString(dr, "Unit_Name"),
                Notes = GetString(dr, "Notes"),
                Price = GetDecimal(dr, "Price"),
                Total = GetDecimal(dr, "T_Price"),
                PtrId = GetInt(dr, "Ptr_ID")
            });
        }

        return items;
    }

    private static async Task<int> GetOpenPeriodIdAsync(SqlConnection cn, int userId, CancellationToken cancellationToken)
    {
        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Check_For_OpenPierod";
        cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = userId;

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken);
        if (await dr.ReadAsync(cancellationToken) == false)
        {
            return 1;
        }

        return GetInt(dr, "Pr_ID");
    }

    private static async Task<int> GetDefaultAgentIdAsync(SqlConnection cn, CancellationToken cancellationToken)
    {
        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT TOP 1 AG_ID FROM dbo.AGENTS WHERE ISNULL(isDefault, 0) = 1 ORDER BY AG_ID ASC";

        object? result;
        try
        {
            result = await cmd.ExecuteScalarAsync(cancellationToken);
        }
        catch (SqlException)
        {
            return 1;
        }

        return result is null || result is DBNull ? 1 : Convert.ToInt32(result);
    }

    private static async Task<int> GetBillTransactionIdByDetailIdAsync(SqlConnection cn, int detailId, CancellationToken cancellationToken)
    {
        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT TOP 1 Bill_T_ID FROM dbo.SB_Contents WHERE T_ID = @T_ID";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = detailId;

        object? result = await cmd.ExecuteScalarAsync(cancellationToken);
        if (result is null || result is DBNull)
        {
            throw new InvalidOperationException("لم يتم العثور على بند الفاتورة المحدد.");
        }

        return Convert.ToInt32(result);
    }

    private static int GetOutputInt(SqlParameter parameter)
    {
        return parameter.Value is DBNull ? 0 : Convert.ToInt32(parameter.Value);
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
        return Convert.ToInt32(value) != 0;
    }
}
