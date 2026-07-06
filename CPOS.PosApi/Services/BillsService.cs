using System.Data;
using CPOS.PosApi.Data;
using CPOS.PosApi.Models.Requests;
using CPOS.PosApi.Models.Responses;
using CPOS.PosApi.Security;
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
        return await OpenSalesBillAsync(tableId, request, cancellationToken);
    }

    public async Task<OpenBillResponse> OpenDirectBillAsync(OpenTableBillRequest request, bool canUseSalesPriceInfo, CancellationToken cancellationToken)
    {
        if (canUseSalesPriceInfo == false)
        {
            throw new InvalidOperationException("ليس لديك صلاحية فتح فواتير بدون طاولة.");
        }

        return await OpenSalesBillAsync(null, request, cancellationToken);
    }

    private async Task<OpenBillResponse> OpenSalesBillAsync(int? tableId, OpenTableBillRequest request, CancellationToken cancellationToken)
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
        if (tableId.HasValue && tableId.Value > 0)
        {
            cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = tableId.Value;
        }

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

        await EnsureBillCanBeChangedAsync(cn, transactionId, cancellationToken);

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
        await EnsureBillCanBeChangedAsync(cn, transactionId, cancellationToken);

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
        await EnsureBillCanBeChangedAsync(cn, transactionId, cancellationToken);

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

    public async Task<IReadOnlyList<PosItemComponentOptionDto>> GetItemComponentOptionsAsync(int detailId, bool isAdd, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        int itemId = await GetItemIdByDetailIdAsync(cn, detailId, cancellationToken);

        List<PosItemComponentOptionDto> options = new();
        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GM_Select_Compon";
        cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = itemId;
        cmd.Parameters.Add("@is_ADD", SqlDbType.Bit).Value = isAdd;

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await dr.ReadAsync(cancellationToken))
        {
            options.Add(new PosItemComponentOptionDto
            {
                ComponentId = GetInt(dr, "Comp_ID"),
                ComponentName = GetString(dr, "Comp_Name"),
                Price = GetDecimal(dr, "Price"),
                IsAdd = isAdd
            });
        }

        return options;
    }

    public async Task<IReadOnlyList<BillItemComponentDto>> GetBillItemComponentsAsync(int detailId, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        return await LoadBillItemComponentsAsync(cn, detailId, cancellationToken);
    }

    public async Task<IReadOnlyList<BillItemComponentDto>> AddBillItemComponentAsync(int detailId, AddBillItemComponentRequest request, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        int transactionId = await GetBillTransactionIdByDetailIdAsync(cn, detailId, cancellationToken);
        await EnsureBillCanBeChangedAsync(cn, transactionId, cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_IM_Compon_INSERT";
        cmd.Parameters.Add("@IM_T_ID", SqlDbType.Int).Value = detailId;
        cmd.Parameters.Add("@Com_ID", SqlDbType.Int).Value = request.ComponentId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await LoadBillItemComponentsAsync(cn, detailId, cancellationToken);
    }

    public async Task<IReadOnlyList<BillItemComponentDto>> ChangeBillItemComponentQuantityAsync(int detailId, int componentLineId, ChangeBillItemComponentQtyRequest request, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        int transactionId = await GetBillTransactionIdByDetailIdAsync(cn, detailId, cancellationToken);
        await EnsureBillCanBeChangedAsync(cn, transactionId, cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_IM_Compon_Update_QTY";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = componentLineId;
        cmd.Parameters.Add("@Def", SqlDbType.Int).Value = request.ChangeBy;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await LoadBillItemComponentsAsync(cn, detailId, cancellationToken);
    }

    public async Task<IReadOnlyList<BillItemComponentDto>> DeleteBillItemComponentAsync(int detailId, int componentLineId, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        int transactionId = await GetBillTransactionIdByDetailIdAsync(cn, detailId, cancellationToken);
        await EnsureBillCanBeChangedAsync(cn, transactionId, cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_IM_Compon_DELETE";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = componentLineId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await LoadBillItemComponentsAsync(cn, detailId, cancellationToken);
    }

    public async Task<IReadOnlyList<BillItemComponentDto>> ClearBillItemComponentsAsync(int detailId, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        int transactionId = await GetBillTransactionIdByDetailIdAsync(cn, detailId, cancellationToken);
        await EnsureBillCanBeChangedAsync(cn, transactionId, cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_IM_Compon_Clear";
        cmd.Parameters.Add("@IM_T_ID", SqlDbType.Int).Value = detailId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await LoadBillItemComponentsAsync(cn, detailId, cancellationToken);
    }

    public async Task<BillDto?> DeleteItemAsync(int detailId, bool onUpdate, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        int transactionId = await GetBillTransactionIdByDetailIdAsync(cn, detailId, cancellationToken);
        await EnsureBillCanBeChangedAsync(cn, transactionId, cancellationToken);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_Contents_Delete_IM";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = detailId;
        cmd.Parameters.Add("@On_Update", SqlDbType.Bit).Value = onUpdate;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await GetBillAsync(cn, transactionId, cancellationToken);
    }

    public async Task<BillDto?> UpdateBillTypeAsync(int transactionId, UpdateBillTypeRequest request, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        BillDto bill = await EnsureBillCanBeChangedAsync(cn, transactionId, cancellationToken);
        if (bill.BillTypeId == request.BillTypeId)
        {
            return await GetBillAsync(cn, transactionId, cancellationToken);
        }

        const int orderBillTypeId = 3;
        bool isMovingToOrFromOrder = bill.BillTypeId == orderBillTypeId || request.BillTypeId == orderBillTypeId;
        if (isMovingToOrFromOrder && await HasBillItemsAsync(cn, transactionId, cancellationToken))
        {
            throw new InvalidOperationException("لتحويل الفاتورة إلى طلبية أو من طلبية يجب حذف كل الأصناف الموجودة بها أولًا.");
        }

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_Update_Bill_Type";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = transactionId;
        cmd.Parameters.Add("@Type_ID", SqlDbType.Int).Value = request.BillTypeId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await GetBillAsync(cn, transactionId, cancellationToken);
    }

    public async Task<BillDto?> SendTableOrderAsync(int transactionId, SendOrderRequest request, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        BillDto billHeader = await EnsureBillCanBeChangedAsync(cn, transactionId, cancellationToken);
        if (billHeader.TableId <= 0)
        {
            throw new InvalidOperationException("هذه الفاتورة ليست مرتبطة بطاولة. استخدم مسار الطلبات الداخلية أو الخارجية.");
        }

        if (request.TableId > 0 && request.TableId != billHeader.TableId)
        {
            throw new InvalidOperationException("رقم الطاولة لا يطابق الفاتورة الحالية.");
        }

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "AG_Balance_Update_TB_Ordered";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = transactionId;
        cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = billHeader.TableId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        return await GetBillAsync(cn, transactionId, cancellationToken);
    }

    public async Task<SaveBillResponse> SaveBillAsync(int transactionId, SaveBillRequest request, ApiUserContext user, CancellationToken cancellationToken)
    {
        await using SqlConnection cn = _connectionFactory.CreateConnection();
        await cn.OpenAsync(cancellationToken);

        BillDto billHeader = await EnsureBillCanBeSavedAsync(cn, transactionId, cancellationToken);
        if (await HasBillItemsAsync(cn, transactionId, cancellationToken) == false)
        {
            throw new InvalidOperationException("لا توجد أصناف في الفاتورة الحالية.");
        }

        if (billHeader.TableId <= 0 && user.CanUseSalesPriceInfo == false)
        {
            throw new InvalidOperationException("ليس لديك صلاحية إنهاء فاتورة بدون طاولة.");
        }

        if (billHeader.TableId > 0)
        {
            bool tableIsCash = await GetTableIsCashAsync(cn, billHeader.TableId, cancellationToken);
            if (tableIsCash == false)
            {
                if (billHeader.IsOrdered == false)
                {
                    await MarkTableOrderAsync(cn, transactionId, billHeader.TableId, cancellationToken);
                }

                return new SaveBillResponse
                {
                    Action = "ordered",
                    Message = billHeader.IsOrdered ? "تم إرسال الطلب مسبقاً." : "تم إرسال الطلب.",
                    Bill = await GetBillAsync(cn, transactionId, cancellationToken)
                };
            }
        }

        int periodId = await GetOpenPeriodIdAsync(cn, user.UserId, cancellationToken);
        if (billHeader.IsOrdered)
        {
            await SwitchBillToCurrentUserAsync(cn, transactionId, user.UserId, periodId, cancellationToken);
        }

        BillDto? bill = await GetBillAsync(cn, transactionId, cancellationToken);
        if (bill is null)
        {
            throw new InvalidOperationException("لم يتم العثور على الفاتورة المحددة.");
        }

        await ConfirmBillAsync(cn, bill, request, user, periodId, cancellationToken);

        return new SaveBillResponse
        {
            Action = "confirmed",
            Message = "تم إنهاء الفاتورة.",
            Bill = null
        };
    }

    private static async Task<BillDto> EnsureBillCanBeSavedAsync(SqlConnection cn, int transactionId, CancellationToken cancellationToken)
    {
        BillDto? bill = await LoadBillHeaderAsync(cn, transactionId, cancellationToken);
        if (bill is null)
        {
            throw new InvalidOperationException("لم يتم العثور على الفاتورة المحددة.");
        }

        if (bill.IsVoid)
        {
            throw new InvalidOperationException("الفاتورة ملغية ولا يمكن حفظها.");
        }

        if (bill.IsPaid)
        {
            throw new InvalidOperationException("الفاتورة مدفوعة مسبقاً.");
        }

        if (bill.IsDepended)
        {
            throw new InvalidOperationException("الفاتورة معتمدة ولا يمكن تعديلها.");
        }

        return bill;
    }

    private static async Task MarkTableOrderAsync(SqlConnection cn, int transactionId, int tableId, CancellationToken cancellationToken)
    {
        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "AG_Balance_Update_TB_Ordered";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = transactionId;
        cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = tableId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task SwitchBillToCurrentUserAsync(SqlConnection cn, int transactionId, int userId, int periodId, CancellationToken cancellationToken)
    {
        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Switch_Bill_To_Current_User";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = transactionId;
        cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = userId;
        cmd.Parameters.Add("@Pr_ID", SqlDbType.Int).Value = periodId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task ConfirmBillAsync(SqlConnection cn, BillDto bill, SaveBillRequest request, ApiUserContext user, int periodId, CancellationToken cancellationToken)
    {
        int agentTypeId = await GetAgentTypeIdAsync(cn, bill.AgentId, cancellationToken);
        int treasuryId = request.TreasuryId ?? user.TreasuryId ?? await GetDefaultSalesTreasuryIdAsync(cn, cancellationToken);
        int payId = request.PayId.GetValueOrDefault(1);

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SB_ConfermBill";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = bill.TransactionId;
        cmd.Parameters.Add("@TOTAL", SqlDbType.Decimal).Value = bill.Total;
        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = bill.Discount;
        cmd.Parameters.Add("@Pure", SqlDbType.Decimal).Value = bill.Pure;

        if (request.PaidAmount.HasValue)
        {
            cmd.Parameters.Add("@Pied", SqlDbType.Decimal).Value = request.PaidAmount.Value;
        }

        cmd.Parameters.Add("@AGType_ID", SqlDbType.Int).Value = agentTypeId;
        cmd.Parameters.Add("@Point_Inc", SqlDbType.Int).Value = 0;
        cmd.Parameters.Add("@Points_Sale", SqlDbType.Int).Value = 0;

        if (bill.BillTypeId == 3)
        {
            if (request.DeliverDate.HasValue)
            {
                cmd.Parameters.Add("@Deliver_date", SqlDbType.DateTime).Value = request.DeliverDate.Value;
            }

            cmd.Parameters.Add("@Order_isDeleverd", SqlDbType.Int).Value = 0;
        }

        cmd.Parameters.Add("@isCostmerScreen", SqlDbType.Int).Value = 0;
        cmd.Parameters.Add("@Tr_ID", SqlDbType.Int).Value = treasuryId;
        cmd.Parameters.Add("@Pr_ID", SqlDbType.Int).Value = periodId;

        if (bill.TableId > 0)
        {
            cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = bill.TableId;
        }

        cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = user.UserId;
        cmd.Parameters.Add("@Pay_ID", SqlDbType.Int).Value = payId;

        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task<BillDto> EnsureBillCanBeChangedAsync(SqlConnection cn, int transactionId, CancellationToken cancellationToken)
    {
        BillDto? bill = await LoadBillHeaderAsync(cn, transactionId, cancellationToken);
        if (bill is null)
        {
            throw new InvalidOperationException("لم يتم العثور على الفاتورة المحددة.");
        }

        if (bill.IsVoid)
        {
            throw new InvalidOperationException("الفاتورة ملغية ولا يمكن تعديلها.");
        }

        if (bill.IsPaid)
        {
            throw new InvalidOperationException("الفاتورة مدفوعة ولا يمكن تعديلها.");
        }

        if (bill.IsDepended || bill.IsOrdered)
        {
            throw new InvalidOperationException("تم إرسال أو اعتماد الطلب ولا يمكن تعديله.");
        }

        return bill;
    }

    private static async Task<BillDto?> GetBillAsync(SqlConnection cn, int transactionId, CancellationToken cancellationToken)
    {
        BillDto? bill = await LoadBillHeaderAsync(cn, transactionId, cancellationToken);
        if (bill is null) return null;

        bill.Items = await LoadBillItemsAsync(cn, transactionId, cancellationToken);
        NormalizeBillTotals(bill);

        return bill;
    }

    private static void NormalizeBillTotals(BillDto bill)
    {
        bill.Total = bill.Items.Sum(x => x.Total);
        bill.Pure = bill.Total - bill.Discount;
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
            TableIsCash = GetBool(dr, "is_Cash"),
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

    private static async Task<IReadOnlyList<BillItemComponentDto>> LoadBillItemComponentsAsync(SqlConnection cn, int detailId, CancellationToken cancellationToken)
    {
        List<BillItemComponentDto> components = new();

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GM_Select_Compon_Details";
        cmd.Parameters.Add("@IM_T_ID", SqlDbType.Int).Value = detailId;

        await using SqlDataReader dr = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await dr.ReadAsync(cancellationToken))
        {
            components.Add(new BillItemComponentDto
            {
                LineId = GetInt(dr, "T_ID"),
                ComponentName = GetString(dr, "Comp_Name"),
                Quantity = GetDecimal(dr, "Qty")
            });
        }

        return components;
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

    private static async Task<int> GetAgentTypeIdAsync(SqlConnection cn, int agentId, CancellationToken cancellationToken)
    {
        if (agentId <= 0) return 1;

        string[] columnNames = { "AG_Type_ID", "AGType_ID", "Type_ID" };
        foreach (string columnName in columnNames)
        {
            await using SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT TOP 1 {columnName} FROM dbo.AGENTS WHERE AG_ID = @AG_ID";
            cmd.Parameters.Add("@AG_ID", SqlDbType.Int).Value = agentId;

            try
            {
                object? result = await cmd.ExecuteScalarAsync(cancellationToken);
                if (result is not null && result is not DBNull)
                {
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException)
            {
                // Some older databases use a different column name for the agent type.
            }
        }

        return 1;
    }

    private static async Task<bool> GetTableIsCashAsync(SqlConnection cn, int tableId, CancellationToken cancellationToken)
    {
        if (tableId <= 0) return false;

        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT TOP 1 is_Cash FROM dbo.TABLES WHERE TB_ID = @TB_ID";
        cmd.Parameters.Add("@TB_ID", SqlDbType.Int).Value = tableId;

        object? result = await cmd.ExecuteScalarAsync(cancellationToken);
        if (result is null || result is DBNull) return false;
        if (result is bool boolValue) return boolValue;
        return Convert.ToInt32(result) != 0;
    }

    private static async Task<int> GetDefaultSalesTreasuryIdAsync(SqlConnection cn, CancellationToken cancellationToken)
    {
        await using SqlCommand settingsCmd = cn.CreateCommand();
        settingsCmd.CommandType = CommandType.Text;
        settingsCmd.CommandText = @"
SELECT TOP 1 SB_TR_ID
FROM dbo.SysSetting
WHERE CP_NAME = @CP_NAME AND ISNULL(SB_TR_ID, 0) > 0";
        settingsCmd.Parameters.Add("@CP_NAME", SqlDbType.NVarChar, 100).Value = Environment.MachineName;

        try
        {
            object? settingsResult = await settingsCmd.ExecuteScalarAsync(cancellationToken);
            if (settingsResult is not null && settingsResult is not DBNull)
            {
                return Convert.ToInt32(settingsResult);
            }
        }
        catch (SqlException)
        {
            // Keep the web POS alive if an older database does not have the local SysSetting row.
        }

        await using SqlCommand treasuryCmd = cn.CreateCommand();
        treasuryCmd.CommandType = CommandType.Text;
        treasuryCmd.CommandText = "SELECT TOP 1 Tr_ID FROM dbo.TreasuryCard ORDER BY Tr_ID ASC";

        try
        {
            object? treasuryResult = await treasuryCmd.ExecuteScalarAsync(cancellationToken);
            return treasuryResult is null || treasuryResult is DBNull ? 1 : Convert.ToInt32(treasuryResult);
        }
        catch (SqlException)
        {
            return 1;
        }
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

    private static async Task<int> GetItemIdByDetailIdAsync(SqlConnection cn, int detailId, CancellationToken cancellationToken)
    {
        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT TOP 1 IM_ID FROM dbo.SB_Contents WHERE T_ID = @T_ID";
        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = detailId;

        object? result = await cmd.ExecuteScalarAsync(cancellationToken);
        if (result is null || result is DBNull)
        {
            throw new InvalidOperationException("لم يتم العثور على بند الفاتورة المحدد.");
        }

        return Convert.ToInt32(result);
    }

    private static async Task<bool> HasBillItemsAsync(SqlConnection cn, int transactionId, CancellationToken cancellationToken)
    {
        await using SqlCommand cmd = cn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT TOP 1 1 FROM dbo.SB_Contents WHERE Bill_T_ID = @SB_T_ID";
        cmd.Parameters.Add("@SB_T_ID", SqlDbType.Int).Value = transactionId;

        object? result = await cmd.ExecuteScalarAsync(cancellationToken);
        return result is not null && result is not DBNull;
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
