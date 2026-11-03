Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions

' Closed inventory/production contract. Fields are from the reviewed CPOS_ACCOUNTING schema.
Partial Friend NotInheritable Class AuditContract
    Friend Shared Function IsInventoryOperation(operation As String) As Boolean
        Return {"Inventory.PurchaseLineDeleted", "Inventory.PurchaseLineChanged", "Manufacturing.AutoProductAdded", "Manufacturing.ManualLineAdded", "Manufacturing.ProductDeleted", "Manufacturing.ProductChanged", "Manufacturing.IngredientDeleted", "Manufacturing.CostRecalculated", "Manufacturing.PurchaseCopied", "Manufacturing.SalesCopied", "Inventory.QuantityRecounted", "Inventory.ItemCostRecounted", "Inventory.AllCostsRecounted"}.Contains(operation)
    End Function

    Friend Shared Function InventoryProcedure(operation As String) As String
        Select Case operation
            Case "Inventory.PurchaseLineDeleted" : Return "Pch_Details_Delete"
            Case "Inventory.PurchaseLineChanged" : Return "Pch_IM_Unit_Change"
            Case "Manufacturing.AutoProductAdded" : Return "FRM_Details_Insert"
            Case "Manufacturing.ManualLineAdded" : Return "FRM_Manual_Details_Insert"
            Case "Manufacturing.ProductDeleted" : Return "FRM_Details_DELETE"
            Case "Manufacturing.ProductChanged" : Return "Frm_Manual_IM_Unit_Change"
            Case "Manufacturing.IngredientDeleted" : Return "Frm_Contetnts_Manual_Delete_IM"
            Case "Manufacturing.CostRecalculated" : Return "FRM_Manual_Set_Cost"
            Case "Manufacturing.PurchaseCopied" : Return "FRM_Manual_MOVE_FROM _PCH"
            Case "Manufacturing.SalesCopied" : Return "FRM_Manual_MOVE_FROM_SB"
            Case "Inventory.QuantityRecounted" : Return "RECOUNT_IM_ALL_QTY"
            Case "Inventory.ItemCostRecounted" : Return "Recount_IM_Cost"
            Case "Inventory.AllCostsRecounted" : Return "AuditInventory_AllCosts036"
            Case Else : Throw New ArgumentException("عملية مخزنية غير معتمدة.")
        End Select
    End Function

    Friend Shared Function InventoryInputs(operation As String) As String()
        Dim result As New List(Of String) From {"DocumentType||Integer|R", "ExpectedInventorySnapshot||String|R"}
        Select Case operation
            Case "Inventory.PurchaseLineDeleted"
                result.AddRange({"TargetLineId|T_ID|Integer|R", "OnUpdate|On_Update|Boolean|R"})
            Case "Inventory.PurchaseLineChanged"
                result.AddRange({"TargetLineId|IM_T_ID|Integer|R", "SelectedUnitId|U_ID|Integer|R", "RequestedQuantity|QTY|Decimal|R", "RequestedPrice|Price|Float|O", "NewSale|NewSale|Decimal|O", "NewSaleByOne|NewSaleByOne|Decimal|O", "Notes|Notes|String|O", "OnUpdate|On_Update|Boolean|R"})
            Case "Manufacturing.AutoProductAdded"
                result.AddRange({"SelectedItemId|IM_ID|Integer|R", "SelectedUnitId|U_ID|Integer|R", "SelectedStoreId|ST_ID|Integer|R", "RequestedPrice|Price|Float|R", "RequestedExpiry|D_Vaild|Date|O", "RequestedQuantity|QYT|Decimal|R", "RequestedTotal|Total|Decimal|R", "NewSale|NewSale|Decimal|O", "NewSaleByOne|NewSaleByOne|Decimal|O", "MinimumPrice|Min_SP|Decimal|O", "MinimumSinglePrice|Min_SP_ByOne|Decimal|O", "Barcode|Barcode|String|O"})
            Case "Manufacturing.ManualLineAdded"
                result.AddRange({"SelectedItemId|IM_ID|Integer|R", "SelectedUnitId|U_ID|Integer|R", "SelectedStoreId|ST_ID|Integer|R", "RequestedPrice|Price|Float|R", "RequestedExpiry|D_Vaild|Date|O", "RequestedQuantity|QYT|Decimal|R", "RequestedTotal|Total|Decimal|R", "NewSale|NewSale|Decimal|O", "NewSaleByOne|NewSaleByOne|Decimal|O", "MinimumPrice|Min_SP|Decimal|O", "MinimumSinglePrice|Min_SP_ByOne|Decimal|O", "Barcode|Barcode|String|R", "Product|is_Product|Boolean|R", "CurrentQuantity|Current_QTY|Decimal|O", "NoStock|is_Not_Qty|Integer|R"})
            Case "Manufacturing.ProductDeleted"
                result.AddRange({"TargetLineId|T_ID|Integer|R"})
            Case "Manufacturing.ProductChanged"
                result.AddRange({"TargetLineId|IM_T_ID|Integer|R", "SelectedUnitId|U_ID|Integer|R", "RequestedQuantity|QTY|Decimal|R", "RequestedPrice|Price|Float|R", "NewSale|NewSale|Decimal|O", "NewSaleByOne|NewSaleByOne|Decimal|O"})
            Case "Manufacturing.IngredientDeleted"
                result.AddRange({"TargetLineId|T_ID|Integer|R"})
            Case "Manufacturing.CostRecalculated"
            Case "Manufacturing.PurchaseCopied"
                result.AddRange({"SourceDocumentId|PCH_T_ID|Integer|R"})
            Case "Manufacturing.SalesCopied"
                result.AddRange({"SourceDocumentId|SB_T_ID|Integer|R", "NoStock|is_Not_Qty|Integer|R", "CopyTarget|TYPE|String|R"})
            Case "Inventory.QuantityRecounted"
            Case "Inventory.ItemCostRecounted"
                result.AddRange({"NewCost|NewCOST|Float|R", "NewMarketerValue|NewMarkVal|Float|R", "FromDate|D_F|Date|R", "ToDate|D_T|Date|R", "EditCardCost|Edit_On_Card|Boolean|R", "EditCardMarketer|Edit_MarkVal_On_Card|Boolean|R", "CalculateCost|Calc_COST|Boolean|R", "CalculateMarketer|Calc_MarkVal|Boolean|R"})
            Case "Inventory.AllCostsRecounted"
            Case Else : Throw New ArgumentException("عملية مخزنية غير معتمدة.")
        End Select
        Return result.ToArray()
    End Function

    Friend Shared Function InventoryFields(entity As String) As String()
        Select Case entity
            Case "InvDocument"
                Return {"InvDocument_T_ID|T_ID|Integer|int", _
                    "InvDocument_AG_ID|AG_ID|Integer|int", _
                    "InvDocument_Pch_ID|Pch_ID|Integer|int", _
                    "InvDocument_Jrd_ID|Jrd_ID|Integer|int", _
                    "InvDocument_Frm_ID|Frm_ID|Integer|int", _
                    "InvDocument_Frm_ID_M|Frm_ID_M|Integer|int", _
                    "InvDocument_Date|Date|DateTime|datetime", _
                    "InvDocument_Total|Total|Decimal|numeric(18,3)", _
                    "InvDocument_Discount|Discount|Decimal|numeric(18,3)", _
                    "InvDocument_Pure|Pure|Decimal|numeric(18,3)", _
                    "InvDocument_Debit|Debit|Decimal|numeric(18,3)", _
                    "InvDocument_Credit|Credit|Decimal|numeric(18,3)", _
                    "InvDocument_Balance|Balance|Decimal|numeric(18,3)", _
                    "InvDocument_BsType_ID|BsType_ID|Integer|int", _
                    "InvDocument_isDepended|isDepended|Integer|int", _
                    "InvDocument_isVoid|isVoid|Integer|int", _
                    "InvDocument_User_ID|User_ID|Integer|int", _
                    "InvDocument_JournalId|JournalId|Integer|int", _
                    "InvDocument_NeedRepost|NeedRepost|Boolean|bit", _
                    "InvDocument_EditOpenedAt|EditOpenedAt|DateTime|datetime", _
                    "InvDocument_EditOpenedBy|EditOpenedBy|Integer|int", _
                    "InvDocument_NeedCancelReverse|NeedCancelReverse|Boolean|bit"}
            Case "InvPurchase"
                Return {"InvPurchase_T_ID|T_ID|Integer|bigint", _
                    "InvPurchase_Date|Date|DateTime|datetime", _
                    "InvPurchase_Pch_T_ID|Pch_T_ID|Integer|int", _
                    "InvPurchase_IM_ID|IM_ID|Integer|int", _
                    "InvPurchase_U_ID|U_ID|Integer|bigint", _
                    "InvPurchase_ST_ID|ST_ID|Integer|bigint", _
                    "InvPurchase_Price|Price|Float|float", _
                    "InvPurchase_D_Vaild|D_Vaild|String|nvarchar(50)", _
                    "InvPurchase_QYT|QYT|Float|float", _
                    "InvPurchase_Total|Total|Float|float", _
                    "InvPurchase_Notes|Notes|String|nvarchar(max)", _
                    "InvPurchase_Barcode|Barcode|String|nvarchar(max)", _
                    "InvPurchase_NewSale|NewSale|Decimal|numeric(18,2)", _
                    "InvPurchase_NewSaleByOne|NewSaleByOne|Decimal|numeric(18,2)", _
                    "InvPurchase_U_Cargo|U_Cargo|Float|float", _
                    "InvPurchase_ST_QTY|ST_QTY|Decimal|numeric(18,2)", _
                    "InvPurchase_isDepended|isDepended|Boolean|bit", _
                    "InvPurchase_Min_SP|Min_SP|Decimal|numeric(18,2)", _
                    "InvPurchase_Min_SP_ByOne|Min_SP_ByOne|Decimal|numeric(18,2)", _
                    "InvPurchase_AVG_Cost|AVG_Cost|Float|float", _
                    "InvPurchase_Min_SP_2|Min_SP_2|Decimal|numeric(18,2)", _
                    "InvPurchase_Min_SP_2_ByOne|Min_SP_2_ByOne|Decimal|numeric(18,2)", _
                    "InvPurchase_AVG_COSTChange|AVG_COSTChange|Float|float", _
                    "InvPurchase_ST_DIF_QTY|ST_DIF_QTY|Decimal|numeric(18,2)", _
                    "InvPurchase_EXP_VALUE|EXP_VALUE|Float|float", _
                    "InvPurchase_Org_Price|Org_Price|Float|float", _
                    "InvPurchase_Price_By_Foriegn_Cr|Price_By_Foriegn_Cr|Float|float", _
                    "InvPurchase_Price_By_Equal|Price_By_Equal|Float|float", _
                    "InvPurchase_Cr_ID|Cr_ID|Integer|int"}
            Case "InvProduct"
                Return {"InvProduct_T_ID|T_ID|Integer|int", _
                    "InvProduct_Date|Date|DateTime|datetime", _
                    "InvProduct_FRM_T_ID|FRM_T_ID|Integer|int", _
                    "InvProduct_IM_ID|IM_ID|Integer|int", _
                    "InvProduct_U_ID|U_ID|Integer|bigint", _
                    "InvProduct_ST_ID|ST_ID|Integer|bigint", _
                    "InvProduct_Price|Price|Float|float", _
                    "InvProduct_D_Vaild|D_Vaild|String|nvarchar(50)", _
                    "InvProduct_QYT|QYT|Decimal|numeric(18,2)", _
                    "InvProduct_Total|Total|Decimal|numeric(18,2)", _
                    "InvProduct_NewSale|NewSale|Decimal|numeric(18,2)", _
                    "InvProduct_NewSaleByOne|NewSaleByOne|Decimal|numeric(18,2)", _
                    "InvProduct_U_Cargo|U_Cargo|Float|float", _
                    "InvProduct_ST_QTY|ST_QTY|Decimal|numeric(18,2)", _
                    "InvProduct_isDepended|isDepended|Boolean|bit", _
                    "InvProduct_Min_SP|Min_SP|Decimal|numeric(18,2)", _
                    "InvProduct_Min_SP_ByOne|Min_SP_ByOne|Decimal|numeric(18,2)", _
                    "InvProduct_Barcode|Barcode|String|nvarchar(50)"}
            Case "InvIngredient"
                Return {"InvIngredient_T_ID|T_ID|Integer|int", _
                    "InvIngredient_DATE|DATE|DateTime|datetime", _
                    "InvIngredient_Bill_T_ID|Bill_T_ID|Integer|int", _
                    "InvIngredient_FRM_IM_T_ID|FRM_IM_T_ID|Integer|int", _
                    "InvIngredient_IM_ID|IM_ID|Integer|int", _
                    "InvIngredient_U_ID|U_ID|Integer|bigint", _
                    "InvIngredient_ST_ID|ST_ID|Integer|bigint", _
                    "InvIngredient_D_Vaild|D_Vaild|String|nvarchar(50)", _
                    "InvIngredient_Cost|Cost|Float|float", _
                    "InvIngredient_QYT|QYT|Float|float", _
                    "InvIngredient_U_Cargo|U_Cargo|Float|float", _
                    "InvIngredient_ST_QTY|ST_QTY|Decimal|numeric(18,3)", _
                    "InvIngredient_isDepended|isDepended|Integer|int", _
                    "InvIngredient_Barcode|Barcode|String|nvarchar(50)", _
                    "InvIngredient_is_Not_Qty|is_Not_Qty|Integer|int"}
            Case "InvItem"
                Return {"InvItem_IM_ID|IM_ID|Integer|int", _
                    "InvItem_item_name|item_name|String|nvarchar(500)", _
                    "InvItem_item_nameSales|item_nameSales|String|nvarchar(500)", _
                    "InvItem_isStore|isStore|Integer|int", _
                    "InvItem_isValid|isValid|Integer|int", _
                    "InvItem_Cost|Cost|Float|float", _
                    "InvItem_Markter_Val|Markter_Val|Decimal|numeric(18,3)"}
            Case "InvUnit"
                Return {"InvUnit_U_IM_ID|U_IM_ID|Integer|int", _
                    "InvUnit_IM_ID|IM_ID|Integer|int", _
                    "InvUnit_U_ID|U_ID|Integer|int", _
                    "InvUnit_Barcode|Barcode|String|nvarchar(max)", _
                    "InvUnit_Price|Price|Decimal|numeric(18,3)", _
                    "InvUnit_Min_SP|Min_SP|Decimal|numeric(18,3)", _
                    "InvUnit_Min_SP_2|Min_SP_2|Decimal|numeric(18,3)", _
                    "InvUnit_is_Default|is_Default|Integer|int", _
                    "InvUnit_Row_Enabled|Row_Enabled|Boolean|bit", _
                    "InvUnit_User_ID|User_ID|Integer|int", _
                    "InvUnit_OFFER_PRICE|OFFER_PRICE|Decimal|numeric(18,3)"}
            Case "InvPhysicalStock"
                Return {"InvPhysicalStock_T_ID|T_ID|Integer|int", _
                    "InvPhysicalStock_ST_ID|ST_ID|Integer|int", _
                    "InvPhysicalStock_IM_ID|IM_ID|Integer|int", _
                    "InvPhysicalStock_D_Valid|D_Valid|String|nvarchar(50)", _
                    "InvPhysicalStock_ST_QTY|ST_QTY|Decimal|numeric(18,3)"}
            Case "InvExpiry"
                Return {"InvExpiry_T_ID|T_ID|Integer|int", _
                    "InvExpiry_Pch_T_ID|Pch_T_ID|Integer|int", _
                    "InvExpiry_IM_ID|IM_ID|Integer|int", _
                    "InvExpiry_VALID_DATE|VALID_DATE|DateTime|datetime"}
            Case "InvCostBatch"
                Return {"InvCostBatch_BatchId|BatchId|Guid|uniqueidentifier", _
                    "InvCostBatch_IM_ID|IM_ID|Integer|int", _
                    "InvCostBatch_PurchaseT_ID|PurchaseT_ID|Integer|int", _
                    "InvCostBatch_PurchaseDetailId|PurchaseDetailId|Integer|bigint", _
                    "InvCostBatch_RecountDate|RecountDate|DateTime|datetime", _
                    "InvCostBatch_FromDate|FromDate|DateTime|datetime", _
                    "InvCostBatch_OldPurchaseCost|OldPurchaseCost|Decimal|decimal(18,6)", _
                    "InvCostBatch_NewPurchaseCost|NewPurchaseCost|Decimal|decimal(18,6)", _
                    "InvCostBatch_CostDiff|CostDiff|Decimal|decimal(18,6)", _
                    "InvCostBatch_OldItemCost|OldItemCost|Decimal|decimal(18,6)", _
                    "InvCostBatch_NewItemCost|NewItemCost|Decimal|decimal(18,6)", _
                    "InvCostBatch_TotalImpact|TotalImpact|Decimal|decimal(18,6)", _
                    "InvCostBatch_InventoryImpact|InventoryImpact|Decimal|decimal(18,6)", _
                    "InvCostBatch_COGSImpact|COGSImpact|Decimal|decimal(18,6)", _
                    "InvCostBatch_ExpenseImpact|ExpenseImpact|Decimal|decimal(18,6)", _
                    "InvCostBatch_Status|Status|String|nvarchar(20)", _
                    "InvCostBatch_JournalId|JournalId|Integer|int", _
                    "InvCostBatch_CreatedBy|CreatedBy|Integer|int", _
                    "InvCostBatch_CreatedAt|CreatedAt|DateTime|datetime", _
                    "InvCostBatch_PostedBy|PostedBy|Integer|int", _
                    "InvCostBatch_PostedAt|PostedAt|DateTime|datetime", _
                    "InvCostBatch_Notes|Notes|String|nvarchar(500)"}
            Case "InvCostImpact"
                Return {"InvCostImpact_ImpactId|ImpactId|Integer|bigint", _
                    "InvCostImpact_BatchId|BatchId|Guid|uniqueidentifier", _
                    "InvCostImpact_IM_ID|IM_ID|Integer|int", _
                    "InvCostImpact_SourceTable|SourceTable|String|nvarchar(100)", _
                    "InvCostImpact_SourceId|SourceId|Integer|int", _
                    "InvCostImpact_SourceParentId|SourceParentId|Integer|int", _
                    "InvCostImpact_SourceDate|SourceDate|DateTime|datetime", _
                    "InvCostImpact_MovementType|MovementType|String|nvarchar(50)", _
                    "InvCostImpact_StoreId|StoreId|Integer|int", _
                    "InvCostImpact_Qty|Qty|Decimal|decimal(18,6)", _
                    "InvCostImpact_U_Cargo|U_Cargo|Decimal|decimal(18,6)", _
                    "InvCostImpact_OldCost|OldCost|Decimal|decimal(18,6)", _
                    "InvCostImpact_NewCost|NewCost|Decimal|decimal(18,6)", _
                    "InvCostImpact_OldTotalCost|OldTotalCost|Decimal|decimal(18,6)", _
                    "InvCostImpact_NewTotalCost|NewTotalCost|Decimal|decimal(18,6)", _
                    "InvCostImpact_DiffAmount|DiffAmount|Decimal|decimal(19,6)", _
                    "InvCostImpact_PurchaseT_ID|PurchaseT_ID|Integer|int", _
                    "InvCostImpact_PurchaseDate|PurchaseDate|DateTime|datetime", _
                    "InvCostImpact_CreatedAt|CreatedAt|DateTime|datetime", _
                    "InvCostImpact_CreatedBy|CreatedBy|Integer|int", _
                    "InvCostImpact_IsPosted|IsPosted|Boolean|bit", _
                    "InvCostImpact_JournalId|JournalId|Integer|int", _
                    "InvCostImpact_OldAvgCost|OldAvgCost|Decimal|decimal(18,6)", _
                    "InvCostImpact_NewAvgCost|NewAvgCost|Decimal|decimal(18,6)", _
                    "InvCostImpact_OldStockQty|OldStockQty|Decimal|decimal(18,6)", _
                    "InvCostImpact_NewStockQty|NewStockQty|Decimal|decimal(18,6)", _
                    "InvCostImpact_OldStockValue|OldStockValue|Decimal|decimal(18,6)", _
                    "InvCostImpact_NewStockValue|NewStockValue|Decimal|decimal(18,6)", _
                    "InvCostImpact_IsCurrentStockEffect|IsCurrentStockEffect|Boolean|bit", _
                    "InvCostImpact_AccountId|AccountId|Integer|int", _
                    "InvCostImpact_CostCenterId|CostCenterId|Integer|int", _
                    "InvCostImpact_Notes|Notes|String|nvarchar(500)", _
                    "InvCostImpact_IsReversed|IsReversed|Boolean|bit"}
            Case "InvCostJournal"
                Return {"InvCostJournal_DraftId|DraftId|Integer|bigint", _
                    "InvCostJournal_BatchId|BatchId|Guid|uniqueidentifier", _
                    "InvCostJournal_IM_ID|IM_ID|Integer|int", _
                    "InvCostJournal_AccountEffect|AccountEffect|String|nvarchar(100)", _
                    "InvCostJournal_Debit|Debit|Decimal|decimal(18,6)", _
                    "InvCostJournal_Credit|Credit|Decimal|decimal(18,6)", _
                    "InvCostJournal_SourceNote|SourceNote|String|nvarchar(500)", _
                    "InvCostJournal_CreatedAt|CreatedAt|DateTime|datetime", _
                    "InvCostJournal_CreatedBy|CreatedBy|Integer|int", _
                    "InvCostJournal_IsPosted|IsPosted|Boolean|bit", _
                    "InvCostJournal_JournalId|JournalId|Integer|int", _
                    "InvCostJournal_AccountId|AccountId|Integer|int", _
                    "InvCostJournal_CostCenterId|CostCenterId|Integer|int", _
                    "InvCostJournal_MovementType|MovementType|String|nvarchar(50)", _
                    "InvCostJournal_SourceTable|SourceTable|String|nvarchar(100)", _
                    "InvCostJournal_SourceId|SourceId|Integer|int", _
                    "InvCostJournal_LineOrder|LineOrder|Integer|int", _
                    "InvCostJournal_LineNote|LineNote|String|nvarchar(500)"}
            Case "InvSaleCost"
                Return {"InvSaleCost_T_ID|T_ID|Integer|bigint", _
                    "InvSaleCost_Bill_T_ID|Bill_T_ID|Integer|int", _
                    "InvSaleCost_IM_ID|IM_ID|Integer|int", _
                    "InvSaleCost_Date|Date|DateTime|datetime", _
                    "InvSaleCost_Cost|Cost|Float|float", _
                    "InvSaleCost_U_Cargo|U_Cargo|Float|float", _
                    "InvSaleCost_Markter_Val|Markter_Val|Decimal|numeric(18,3)"}
            Case "InvReturnCost"
                Return {"InvReturnCost_T_ID|T_ID|Integer|int", _
                    "InvReturnCost_Date|Date|DateTime|datetime", _
                    "InvReturnCost_Bill_T_ID|Bill_T_ID|Integer|int", _
                    "InvReturnCost_Rtn_Type_ID|Rtn_Type_ID|Integer|int", _
                    "InvReturnCost_IM_ID|IM_ID|Integer|int", _
                    "InvReturnCost_Cost|Cost|Decimal|numeric(18,2)", _
                    "InvReturnCost_U_Cargo|U_Cargo|Float|float"}
            Case "InvIssueCost"
                Return {"InvIssueCost_T_ID|T_ID|Integer|int", _
                    "InvIssueCost_Date|Date|DateTime|datetime", _
                    "InvIssueCost_InSale_T_ID|InSale_T_ID|Integer|int", _
                    "InvIssueCost_IM_ID|IM_ID|Integer|int", _
                    "InvIssueCost_Cost|Cost|Decimal|numeric(18,2)", _
                    "InvIssueCost_U_Cargo|U_Cargo|Float|float"}
            Case "InvReceiptCost"
                Return {"InvReceiptCost_T_ID|T_ID|Integer|int", _
                    "InvReceiptCost_Date|Date|DateTime|datetime", _
                    "InvReceiptCost_Outsale_T_ID|Outsale_T_ID|Integer|int", _
                    "InvReceiptCost_IM_ID|IM_ID|Integer|int", _
                    "InvReceiptCost_Cost|Cost|Decimal|numeric(18,2)", _
                    "InvReceiptCost_U_Cargo|U_Cargo|Float|float"}
            Case "InvTransferCost"
                Return {"InvTransferCost_T_ID|T_ID|Integer|int", _
                    "InvTransferCost_B_T_ID|B_T_ID|Integer|int", _
                    "InvTransferCost_Date|Date|DateTime|datetime", _
                    "InvTransferCost_IM_ID|IM_ID|Integer|int", _
                    "InvTransferCost_Price|Price|Decimal|numeric(18,2)", _
                    "InvTransferCost_U_Cargo|U_Cargo|Float|float"}
            Case "InvStore"
                Return {"InvStore_ST_ID|ST_ID|Integer|bigint", _
                    "InvStore_St_Name|St_Name|String|nvarchar(350)"}
            Case "InvStockBucket"
                Return {"InvStockBucket_IM_ID|IM_ID|Integer|int", _
                    "InvStockBucket_ST_ID|ST_ID|Integer|bigint", _
                    "InvStockBucket_D_Vaild|D_Vaild|String|nvarchar(50)", _
                    "InvStockBucket_Quantity|Quantity|Float|float"}
            Case Else : Throw New ArgumentException("كيان مخزني غير معتمد.")
        End Select
    End Function

    Friend Shared Function InventoryEntities(operation As String) As String()
        Select Case operation
            Case "Inventory.PurchaseLineDeleted", "Inventory.PurchaseLineChanged"
                Return {"InvDocument", "InvPurchase", "InvExpiry", "InvItem", "InvUnit", "InvCostBatch", "InvCostImpact", "InvCostJournal", "InvStockBucket"}
            Case "Inventory.QuantityRecounted"
                Return {"InvStore", "InvItem", "InvUnit", "InvPhysicalStock", "InvStockBucket"}
            Case "Inventory.AllCostsRecounted"
                Return {"InvItem", "InvPurchase"}
            Case "Inventory.ItemCostRecounted"
                Return {"InvItem", "InvSaleCost", "InvReturnCost", "InvIssueCost", "InvReceiptCost", "InvTransferCost"}
            Case Else
                If Not IsInventoryOperation(operation) Then Throw New ArgumentException("عملية مخزنية غير معتمدة.")
                Return {"InvDocument", "InvProduct", "InvIngredient", "InvStockBucket"}
        End Select
    End Function

    Friend Shared Function InventoryFieldType(operation As String, field As String) As String
        For Each d In InventoryInputs(operation).Concat(InventoryEntities(operation).SelectMany(Function(x) InventoryFields(x)))
            Dim p = d.Split("|"c)
            If p(0) = field Then Return p(2)
        Next
        Throw New ArgumentException("حقل مخزني غير معتمد: " & field)
    End Function

    Friend Shared Sub ValidateInventoryRequest(r As AuditOperationRequest)
        Dim id As Integer
        If r.EntityType <> "InventoryOperation" OrElse Not Integer.TryParse(r.EntityId, id) OrElse id <= 0 Then Throw New ArgumentException("هوية عملية مخزنية غير صالحة.")
        Dim screens As String()
        Select Case r.OperationCode
            Case "Inventory.PurchaseLineDeleted" : screens = {"Pch", "Invoice"}
            Case "Inventory.PurchaseLineChanged" : screens = {"Change_IM_Details"}
            Case "Manufacturing.AutoProductAdded" : screens = {"Frm_auto_IM_card", "Frm_manuel_IM_card"}
            Case "Manufacturing.ManualLineAdded" : screens = {"Frm_manuel_IM_card"}
            Case "Manufacturing.ProductDeleted" : screens = {"Format_Items_Auto", "Format_Items_Manual"}
            Case "Manufacturing.ProductChanged" : screens = {"Change_IM_Details"}
            Case "Manufacturing.IngredientDeleted" : screens = {"Format_Items_Manual"}
            Case "Manufacturing.CostRecalculated" : screens = {"Format_Items_Manual"}
            Case "Manufacturing.PurchaseCopied" : screens = {"Format_Items_Manual"}
            Case "Manufacturing.SalesCopied" : screens = {"Format_Items_Manual"}
            Case "Inventory.QuantityRecounted" : screens = {"STORES_Explorer", "IM_CYRCLE"}
            Case "Inventory.ItemCostRecounted" : screens = {"Recount_IM_Cost"}
            Case "Inventory.AllCostsRecounted" : screens = {"STORES_Explorer", "IM_CYRCLE"}
            Case Else : Throw New ArgumentException("عملية مخزنية غير معتمدة.")
        End Select
        If Not screens.Contains(r.Context.ScreenCode) Then Throw New ArgumentException("الشاشة لا تتبع العملية المخزنية.")
        Dim definitions = InventoryInputs(r.OperationCode)
        If r.Inputs.Count <> definitions.Length Then Throw New ArgumentException("مدخلات العملية المخزنية غير مكتملة.")
        For Each d In definitions
            Dim p = d.Split("|"c), x = r.Inputs.SingleOrDefault(Function(v) v.Name = p(0))
            If x Is Nothing OrElse x.ValueType <> p(2) OrElse (p(3) = "R" AndAlso x.Value.State <> AuditValueState.Present) OrElse x.Value.State < AuditValueState.SqlNull Then Throw New ArgumentException("مدخل مخزني غير صالح: " & p(0))
            If x.Value.State = AuditValueState.Present Then
                If p(0).EndsWith("Id", StringComparison.Ordinal) AndAlso Long.Parse(x.Value.Text, CultureInfo.InvariantCulture) <= 0 Then Throw New ArgumentException("معرف مخزني غير موجب.")
                If p(2) = "Decimal" AndAlso Math.Abs(Decimal.Parse(x.Value.Text, CultureInfo.InvariantCulture)) >= 1000000000000000D Then Throw New ArgumentException("تجاوز سعة القيمة المالية.")
                If p(0) = "RequestedQuantity" AndAlso Decimal.Round(Decimal.Parse(x.Value.Text, CultureInfo.InvariantCulture), 2, MidpointRounding.AwayFromZero) <= 0D Then Throw New ArgumentException("الكمية يجب أن تكون موجبة بعد التقريب.")
                If p(0) = "Barcode" AndAlso x.Value.Text.Length > 50 Then Throw New ArgumentException("الباركود يتجاوز 50 حرفًا.")
                If p(0) = "CopyTarget" AndAlso x.Value.Text <> "FRM_Details" Then Throw New ArgumentException("نطاق نسخ التصنيع غير معتمد.")
                If p(0) = "NoStock" AndAlso Not {"0", "1"}.Contains(x.Value.Text) Then Throw New ArgumentException("علم الكمية غير صالح.")
            End If
        Next
        Dim kind = Integer.Parse(r.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text, CultureInfo.InvariantCulture)
        If Not Regex.IsMatch(r.Inputs.Single(Function(x) x.Name = "ExpectedInventorySnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة المخزون غير صالحة.")
        If r.OperationCode.StartsWith("Inventory.Purchase", StringComparison.Ordinal) Then
            If Not {7, 9}.Contains(kind) Then Throw New ArgumentException("نوع فاتورة الشراء/الجرد غير مطابق.")
            If r.Context.ScreenCode = "Pch" AndAlso kind <> 7 OrElse r.Context.ScreenCode = "Invoice" AndAlso kind <> 9 Then Throw New ArgumentException("نوع الفاتورة لا يطابق الشاشة.")
        ElseIf r.OperationCode.StartsWith("Manufacturing.", StringComparison.Ordinal) Then
            If Not {13, 18}.Contains(kind) Then Throw New ArgumentException("نوع التصنيع غير مطابق.")
            If r.Context.ScreenCode = "Format_Items_Auto" OrElse r.Context.ScreenCode = "Frm_auto_IM_card" Then
                If kind <> 13 Then Throw New ArgumentException("نوع التصنيع الآلي غير مطابق.")
            ElseIf kind <> 18 Then
                Throw New ArgumentException("نوع التصنيع اليدوي غير مطابق.")
            End If
        ElseIf kind <> 0 Then
            Throw New ArgumentException("إعادة الاحتساب ليست فاتورة.")
        End If
        If r.OperationCode = "Inventory.AllCostsRecounted" AndAlso r.EntityId <> "1" Then Throw New ArgumentException("النطاق العام ثابت.")
        If r.OperationCode = "Inventory.ItemCostRecounted" Then
            Dim first = DateTime.ParseExact(r.Inputs.Single(Function(x) x.Name = "FromDate").Value.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            Dim last = DateTime.ParseExact(r.Inputs.Single(Function(x) x.Name = "ToDate").Value.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            If first > last Then Throw New ArgumentException("نطاق التاريخ مقلوب.")
        End If
    End Sub

    Friend Shared Sub ValidateInventoryEvent(r As AuditOperationRequest, e As AuditEventRequest)
        If e.EntityType <> r.EntityType OrElse e.EntityId <> r.EntityId OrElse e.RelatedEntities.Count <> 0 Then Throw New ArgumentException("هوية الحدث المخزني لا تطابق الطلب.")
        Dim allowed = InventoryEntities(r.OperationCode)
        For Each g In e.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
            Dim first = g.First()
            If Not allowed.Contains(first.EntityType) Then Throw New ArgumentException("كيان خارج العملية المخزنية.")
            Dim fs = InventoryFields(first.EntityType)
            If g.Count() <> fs.Length OrElse g.Select(Function(x) x.Before.State = AuditValueState.Absent).Distinct().Count() <> 1 OrElse g.Select(Function(x) x.After.State = AuditValueState.Absent).Distinct().Count() <> 1 Then Throw New ArgumentException("صف مخزني غير مكتمل أو غير متجانس.")
            For Each c In g
                If Not fs.Any(Function(d) d.Split("|"c)(0) = c.FieldCode AndAlso d.Split("|"c)(3) = c.SourceSqlType) OrElse c.ValueOrigin <> If(first.EntityType = "InvStockBucket", 3, 1) OrElse c.Before.State = AuditValueState.NotCaptured OrElse c.After.State = AuditValueState.NotCaptured OrElse (c.Before.State = AuditValueState.Absent AndAlso c.After.State = AuditValueState.Absent) Then Throw New ArgumentException("قيمة مخزنية غير فعلية.")
            Next
        Next
    End Sub
End Class
