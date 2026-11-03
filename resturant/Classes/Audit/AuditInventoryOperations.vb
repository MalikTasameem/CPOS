Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text

Friend NotInheritable Class AuditInventorySnapshot
    Friend ReadOnly Rows As New SortedDictionary(Of String, DataTable)(StringComparer.Ordinal)
    Friend ReadOnly References As New SortedDictionary(Of String, DataTable)(StringComparer.Ordinal)
    Friend ItemIds As Integer()
    Friend DocumentIds As Integer()
End Class

' No caller-supplied SQL or procedure names are executed. Every mutation and its observed graph share one transaction.
Friend NotInheritable Class AuditInventoryOperations
    Friend Shared Function V(r As AuditOperationRequest, name As String, Optional fallback As String = "") As String
        Dim x = r.Inputs.SingleOrDefault(Function(i) i.Name = name)
        Return If(x Is Nothing OrElse x.Value.State <> AuditValueState.Present, fallback, x.Value.Text)
    End Function

    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "pch_details_delete" : Return "Inventory.PurchaseLineDeleted"
            Case "pch_im_unit_change" : Return "Inventory.PurchaseLineChanged"
            Case "frm_details_insert" : Return "Manufacturing.AutoProductAdded"
            Case "frm_manual_details_insert" : Return "Manufacturing.ManualLineAdded"
            Case "frm_details_delete" : Return "Manufacturing.ProductDeleted"
            Case "frm_manual_im_unit_change" : Return "Manufacturing.ProductChanged"
            Case "frm_contetnts_manual_delete_im" : Return "Manufacturing.IngredientDeleted"
            Case "frm_manual_set_cost" : Return "Manufacturing.CostRecalculated"
            Case "frm_manual_move_from _pch" : Return "Manufacturing.PurchaseCopied"
            Case "frm_manual_move_from_sb" : Return "Manufacturing.SalesCopied"
            Case "recount_im_all_qty" : Return "Inventory.QuantityRecounted"
            Case "recount_im_cost" : Return "Inventory.ItemCostRecounted"
            Case "recount_im_all_maincost" : Return "Inventory.AllCostsRecounted"
            Case Else : Throw New ArgumentException("إجراء مخزني غير معتمد.")
        End Select
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, ownerId As Integer, kind As Integer) As AuditOperationRequest
        Dim op = Operation(command)
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op, .EntityType = "InventoryOperation", .EntityId = ownerId.ToString(CultureInfo.InvariantCulture)}
        Dim ownerParameter = If(op = "Inventory.PurchaseLineChanged" OrElse op.EndsWith("Added", StringComparison.Ordinal), "Pch_T_ID", If(op.EndsWith("Copied", StringComparison.Ordinal), "FRM_T_ID", If(op = "Manufacturing.CostRecalculated", "T_ID", If(op = "Inventory.QuantityRecounted", "ST_ID", If(op = "Inventory.ItemCostRecounted", "IM_ID", "")))))
        If ownerParameter <> "" AndAlso Convert.ToInt32(AuditFinance.Parameter(command, ownerParameter), CultureInfo.CurrentCulture) <> ownerId Then Throw New ArgumentException("معامل المستند/الصنف لا يطابق الطلب.")
        For Each definition In AuditContract.InventoryInputs(op)
            Dim p = definition.Split("|"c), value As Object
            If p(0) = "DocumentType" Then
                value = kind
            ElseIf p(0) = "ExpectedInventorySnapshot" Then
                value = New String("0"c, 64)
            Else
                Dim parameter = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) x.ParameterName.TrimStart("@"c).Equals(p(1), StringComparison.OrdinalIgnoreCase))
                If parameter Is Nothing OrElse parameter.Value Is Nothing OrElse IsDBNull(parameter.Value) Then
                    If p(3) = "R" Then Throw New ArgumentException("معامل مخزني مطلوب: " & p(1))
                    value = DBNull.Value
                Else
                    value = parameter.Value
                    Select Case p(2)
                        Case "Integer" : value = If(TypeOf value Is Boolean, If(CBool(value), 1, 0), Convert.ToInt32(value, CultureInfo.CurrentCulture))
                        Case "Decimal" : value = Convert.ToDecimal(value, CultureInfo.CurrentCulture)
                        Case "Float" : value = Convert.ToDouble(value, CultureInfo.CurrentCulture)
                        Case "Boolean" : value = Convert.ToBoolean(value, CultureInfo.CurrentCulture)
                        Case "Date" : value = Convert.ToDateTime(value, CultureInfo.CurrentCulture).Date
                        Case "String" : value = Convert.ToString(value, CultureInfo.CurrentCulture)
                    End Select
                End If
            End If
            r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = p(2), .Value = AuditValue.Capture(p(2), value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Function Query(cn As SqlConnection, tx As SqlTransaction, sql As String, ParamArray values As Object()) As DataTable
        Using cmd As New SqlCommand(sql, cn, tx)
            cmd.CommandTimeout = 120
            For i = 0 To values.Length - 1
                cmd.Parameters.AddWithValue("@P" & i, If(values(i), DBNull.Value))
            Next
            Using rd = cmd.ExecuteReader()
                Dim dt As New DataTable() : dt.Load(rd) : Return dt
            End Using
        End Using
    End Function

    Friend Shared Function TableSpec(entity As String) As String()
        Select Case entity
            Case "InvDocument" : Return {"Agents_Balance_MV", "T_ID"}
            Case "InvPurchase" : Return {"Pch_Details", "T_ID"}
            Case "InvProduct" : Return {"FRM_Details", "T_ID"}
            Case "InvIngredient" : Return {"FRM_Contents_Details", "T_ID"}
            Case "InvItem" : Return {"IM_Menu", "IM_ID"}
            Case "InvUnit" : Return {"IM_Units", "U_IM_ID"}
            Case "InvPhysicalStock" : Return {"ST_Balance", "T_ID"}
            Case "InvExpiry" : Return {"Valid_Notes", "T_ID"}
            Case "InvCostBatch" : Return {"InventoryCostRecountBatch", "BatchId"}
            Case "InvCostImpact" : Return {"InventoryCostRecountImpact", "ImpactId"}
            Case "InvCostJournal" : Return {"InventoryCostRecountJournalDraft", "DraftId"}
            Case "InvSaleCost" : Return {"SB_Contents", "T_ID"}
            Case "InvReturnCost" : Return {"Rtn_Contents", "T_ID"}
            Case "InvIssueCost" : Return {"InSale_Details", "T_ID"}
            Case "InvReceiptCost" : Return {"Outsale_Details", "T_ID"}
            Case "InvTransferCost" : Return {"Store_Trans_Details", "T_ID"}
            Case "InvStore" : Return {"Stores", "ST_ID"}
            Case Else : Throw New ArgumentException("جدول مخزني غير معتمد.")
        End Select
    End Function

    Private Shared Function ReadEntity(cn As SqlConnection, tx As SqlTransaction, entity As String, where As String, ParamArray values As Object()) As DataTable
        Dim spec = TableSpec(entity)
        Return Query(cn, tx, "SELECT " & String.Join(",", AuditContract.InventoryFields(entity).Select(Function(d) "[" & d.Split("|"c)(1) & "]")) & " FROM dbo.[" & spec(0) & "] WITH(UPDLOCK,HOLDLOCK) WHERE " & where & " ORDER BY [" & spec(1) & "]", values)
    End Function

    Private Shared Function IdList(ids As IEnumerable(Of Integer)) As String
        Dim result = ids.Distinct().OrderBy(Function(x) x).ToArray()
        If result.Any(Function(x) x <= 0) Then Throw New InvalidOperationException("معرف ضمن النطاق المخزني غير موجب.")
        Return If(result.Length = 0, "0", String.Join(",", result.Select(Function(x) x.ToString(CultureInfo.InvariantCulture))))
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest)
        Dim kind = CInt(V(r, "DocumentType")), flag As String
        If kind = 7 OrElse kind = 9 Then
            Dim approvedInventory = kind = 9 AndAlso CInt(Query(cn, tx, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=@P0 AND BsType_ID=9 AND isDepended=1", CInt(r.EntityId)).Rows(0)(0)) = 1
            flag = If(V(r, "OnUpdate") = "1" OrElse approvedInventory, "Pch_Update", "Pch")
        ElseIf kind = 13 OrElse kind = 18 Then
            flag = If(kind = 13, "FRM", "Frm_M")
        Else
            flag = If(r.OperationCode = "Inventory.ItemCostRecounted", "IM", "StExplore")
        End If
        If CInt(Query(cn, tx, "SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON u.User_ID=v.User_ID WHERE u.User_ID=@P0 AND u.is_Allow=1 AND v.[" & flag & "]=1", r.Context.ActorUserId.Value).Rows(0)(0)) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية العملية المخزنية: " & flag)
    End Sub

    Private Shared Sub Guard(cn As SqlConnection, tx As SqlTransaction)
        Dim expected As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) From {
            {"AuditInventory_ItemCost036", "357B8FCF1A0B5E41582DCC5B8655590DE4C86490B4C1FD6E744854B3F5D34D50"},
            {"AuditInventory_AllCosts036", "D3CE333AD7A9B9613D61DACB81A972629176DA94AC0A39979D12EB346A34AB8E"},
            {"AA_GET_MAX_FRM_Details", "33A2237FB530F0BB277119133E3CCA8E77D270E82668846A6479A6839F5D5751"},
            {"AA_GET_MAX_ID", "71826097F08C19B6EC806AC2B253D5E97E088D6E3998608C65BE2CB43FDD8BC6"},
            {"AG_Balance_Update_Pied_On_Update", "67BCD8EDA90891D2785813B4892030F993D5FE121B37892E0AE15F62F1BC9945"},
            {"AG_Balance_Update_Total", "051E94C341671C3A795E59D96EBF248E85381B811EE042B5B3B426577AAFC7B6"},
            {"CALC_IM_QTY_IN_specific_Range_Date", "0CA27741AA94E6D1B73DBB8B551965A8B7C5DCBB86DFDC4560C1DE52C1C51779"},
            {"CHeck_IM_Default_Unit", "9183EA666830FBB96662D89E08C01E5E3715DEAEAD823C06D13C4FFF1F038307"},
            {"CREATE_PCH_TMP_TABLE", "38450ACAD202913A9294F4CE2409E738382613870B0BE8C9048C27347958AD0A"},
            {"FRM_Contents_Manuel_V", "9A1D04E12F5900BEE91B642FCF188279CE252A18E7849A0FAC6EED495232E5F2"},
            {"Frm_Contetnts_Manual_Delete_IM", "6AC6FC22655FB1F0D15C8B3A8DDE4DCF8C7A3CCF3EE59180328DF9D00B5105E7"},
            {"FRM_Details_DELETE", "F8A0DCD7E9F7DC99192A4989039551FE6B2B73883F188D8850FC3034AFC62D26"},
            {"FRM_Details_Insert", "97C70E1F7CCB1E5797915D04C854ABA76EF67B655F4ED81E7E98E8D3981933FC"},
            {"FRM_Manual_Details_Insert", "0F205E8E141A51223CB668B91F1395926D5419307FEF8D15E6821FBCC6B82D2A"},
            {"Frm_Manual_IM_Unit_Change", "6EA20E3582F3887E12A25B045B37950CB61141C65B953786D6582948D4C9ED5C"},
            {"FRM_Manual_MOVE_FROM _PCH", "5210B62750AC9916840DC3C23A052F8BA3E3AD12E95EED9F6FBC27B73B6E7DFD"},
            {"FRM_Manual_MOVE_FROM_SB", "C8DE59F7FB9AB4FA5D6D9B2D7EE9BB46E907EBE450575D651FCFEBF98CB4D9DC"},
            {"FRM_Manual_Set_Cost", "3FF0F0C25ACB98D61972CD6ACA4547FFF75B9F33EECA550FB6E1698B0381A955"},
            {"IM_Formating_Menu_V", "9D39FBBEE7CE7F57B3A18EBED6B4BD20788FAD6C286F45371D8A5634A83B605D"},
            {"Inc_IM_QTY", "E71EAD168BD88A56D598229234CB1ABF4D77A220ED79B6D4CAC4698CE0B30C20"},
            {"InventoryCostRecount_Preview", "61CAD000E1F8428C14090FFF8DD5B42C8368F224E1F94B35ABFD396771E9F6AC"},
            {"Pch_Details_Delete", "C1A676D900A5E1194EE9F770A745288F0AC26A8CBEB319AC6131310D638BFE44"},
            {"Pch_Details_SELECT_Bill", "99280CCE4FCDB565FFCB2C82593DC8B8F5DE040E218E0E3A9BB8D3691B8F5875"},
            {"Pch_IM_Unit_Change", "423502031547CCE1DB840C2688CE8DE8104D0102E90DDF8BEB020AF7A3C701BD"},
            {"Recalculate_Agent_Bill_Total", "826ED8485824D63BC7596DF5D833C8F7D8AFF6ECD3F47C32D59C166BE8EEF095"},
            {"RECOUNT_IM_ALL_MainCost", "71688137DC09B1A5FD3FA5E29A292684E747838A3ABD39A25BEB784414D0DC3A"},
            {"RECOUNT_IM_ALL_QTY", "588CD385E27E2C995414594B03232C89BC2E5C8BEAFE5D349AE829F8B7F33153"},
            {"Recount_IM_Cost", "F6F4363CF23AB80D192C3320566D46AB14FB49FCCEFA266D6BE19DF3A593375A"},
            {"ReCount_IM_Main_COST", "CEFBA6589964DC53D5A91B73232E6B5C640E365AD5D093A5F9DB0E519168E6D7"},
            {"VW_InventoryTransactionFlow", "8DE740DD7F60D35C7C87D947F227C7C9CB898DBBDFFE003C434BD0CEE8F479BE"}
        }
        For Each pair In expected
            Dim definition = Convert.ToString(Query(cn, tx, "SELECT OBJECT_DEFINITION(OBJECT_ID(@P0))", "dbo." & pair.Key).Rows(0)(0), CultureInfo.InvariantCulture)
            If BitConverter.ToString(AuditCanonical.Hash(Encoding.Unicode.GetBytes(definition))).Replace("-", "") <> pair.Value Then Throw New InvalidOperationException("تغير إجراء من تبعيات المخزون؛ يلزم مراجعة العقد: " & pair.Key)
        Next
        Dim tables = AuditContract.InventoryEntities("Inventory.PurchaseLineChanged").Concat(AuditContract.InventoryEntities("Manufacturing.ManualLineAdded")).Concat(AuditContract.InventoryEntities("Inventory.ItemCostRecounted")).Concat({"InvPhysicalStock"}).Where(Function(x) x <> "InvStockBucket").Select(Function(x) TableSpec(x)(0)).Distinct().ToArray()
        For Each table In tables
            If CInt(Query(cn, tx, "SELECT COUNT(*) FROM sys.triggers WHERE parent_id=OBJECT_ID(@P0) AND is_disabled=0", "dbo." & table).Rows(0)(0)) <> 0 Then Throw New InvalidOperationException("يوجد Trigger غير مراجع على جدول المخزون: " & table)
        Next
    End Sub

    Friend Shared Sub ValidateEditableHeader(r As AuditOperationRequest, header As DataTable)
        Dim kind = CInt(V(r, "DocumentType"))
        If header.Rows.Count <> 1 OrElse IsDBNull(header.Rows(0)("BsType_ID")) OrElse CInt(header.Rows(0)("BsType_ID")) <> kind OrElse IsDBNull(header.Rows(0)("isVoid")) OrElse CInt(header.Rows(0)("isVoid")) <> 0 Then Throw New InvalidOperationException("الفاتورة غير موجودة أو ملغاة أو نوعها غير مطابق.")
        Dim row = header.Rows(0)
        If IsDBNull(row("isDepended")) OrElse Not {0, 1}.Contains(CInt(row("isDepended"))) Then Throw New InvalidOperationException("حالة اعتماد الفاتورة غير معروفة.")
        If CInt(row("isDepended")) = 1 Then
            ' OpenForEdit retains isDepended=1. Invoice deletion deliberately passes SQL On_Update=0.
            ' The persisted edit owner is authoritative; do not alter either legacy SQL behavior.
            If (kind = 7 AndAlso V(r, "OnUpdate") <> "1") OrElse IsDBNull(row("EditOpenedAt")) OrElse IsDBNull(row("EditOpenedBy")) OrElse CInt(row("EditOpenedBy")) <> r.Context.ActorUserId.Value Then Throw New InvalidOperationException("افتح تعديل الفاتورة بالمستخدم الحالي أولًا.")
        End If
    End Sub

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, Optional old As AuditInventorySnapshot = Nothing) As AuditInventorySnapshot
        Dim s As New AuditInventorySnapshot, op = r.OperationCode, owner = CInt(r.EntityId), kind = CInt(V(r, "DocumentType"))
        Dim itemIds As New HashSet(Of Integer), documentIds As New HashSet(Of Integer)
        Dim target = CInt(V(r, "TargetLineId", "0"))
        If old IsNot Nothing Then
            itemIds.UnionWith(old.ItemIds) : documentIds.UnionWith(old.DocumentIds)
        End If
        If kind <> 0 Then
            documentIds.Add(owner)
            If old Is Nothing Then
                Dim header = ReadEntity(cn, tx, "InvDocument", "T_ID=@P0", owner)
                ValidateEditableHeader(r, header)
            End If
            If kind = 7 OrElse kind = 9 Then
                s.Rows("InvPurchase") = ReadEntity(cn, tx, "InvPurchase", "Pch_T_ID=@P0", owner)
                If old Is Nothing Then
                    Dim line = s.Rows("InvPurchase").Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = target)
                    If line Is Nothing Then Throw New InvalidOperationException("سطر المشتريات/الجرد لا يتبع الفاتورة الحالية.")
                    itemIds.Add(CInt(line("IM_ID")))
                End If
                s.Rows("InvExpiry") = ReadEntity(cn, tx, "InvExpiry", "Pch_T_ID IN(SELECT T_ID FROM dbo.Pch_Details WITH(HOLDLOCK) WHERE Pch_T_ID=@P0) OR Pch_T_ID=@P1", owner, target)
            Else
                Dim extraBill = 0
                If op = "Manufacturing.IngredientDeleted" AndAlso old Is Nothing Then
                    Dim chosen = ReadEntity(cn, tx, "InvIngredient", "T_ID=@P0 AND Bill_T_ID=@P1", target, owner)
                    If chosen.Rows.Count <> 1 Then Throw New InvalidOperationException("المادة لا تتبع مستند التصنيع الحالي.")
                    ' Preserve the legacy procedure's actual FRM_IM_T_ID target; do not silently repair business behavior.
                    If Not IsDBNull(chosen.Rows(0)("FRM_IM_T_ID")) Then extraBill = CInt(chosen.Rows(0)("FRM_IM_T_ID"))
                    If extraBill > 0 Then documentIds.Add(extraBill)
                End If
                s.Rows("InvProduct") = ReadEntity(cn, tx, "InvProduct", "FRM_T_ID IN(" & IdList(documentIds) & ")")
                s.Rows("InvIngredient") = ReadEntity(cn, tx, "InvIngredient", "Bill_T_ID IN(" & IdList(documentIds) & ") OR FRM_IM_T_ID IN(SELECT T_ID FROM dbo.FRM_Details WITH(HOLDLOCK) WHERE FRM_T_ID=@P0) OR FRM_IM_T_ID=@P1", owner, If(op = "Manufacturing.ProductDeleted", target, 0))
                For Each row As DataRow In s.Rows("InvProduct").Rows
                    itemIds.Add(CInt(row("IM_ID")))
                Next
                For Each row As DataRow In s.Rows("InvIngredient").Rows
                    itemIds.Add(CInt(row("IM_ID")))
                Next
                If old Is Nothing AndAlso {"Manufacturing.ProductDeleted", "Manufacturing.ProductChanged"}.Contains(op) AndAlso Not s.Rows("InvProduct").Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("T_ID")) = target AndAlso CInt(x("FRM_T_ID")) = owner) Then Throw New InvalidOperationException("المنتج لا يتبع مستند التصنيع الحالي.")
                If op.EndsWith("Added", StringComparison.Ordinal) Then
                    itemIds.Add(CInt(V(r, "SelectedItemId")))
                    If op = "Manufacturing.AutoProductAdded" Then
                        s.References("Recipe") = Query(cn, tx, "SELECT * FROM dbo.IM_Formating_Menu_V WITH(HOLDLOCK) WHERE IM_ID=@P0 ORDER BY T_ID,Barcode", CInt(V(r, "SelectedItemId")))
                        For Each row As DataRow In s.References("Recipe").Rows
                            itemIds.Add(CInt(row("FRM_IM_ID")))
                        Next
                    End If
                ElseIf op.EndsWith("Copied", StringComparison.Ordinal) Then
                    Dim sourceKind = If(op = "Manufacturing.PurchaseCopied", 7, 1), sourceTable = If(sourceKind = 7, "Pch_Details", "SB_Contents"), sourceOwner = If(sourceKind = 7, "Pch_T_ID", "Bill_T_ID")
                    s.References("CopyHeader") = ReadEntity(cn, tx, "InvDocument", "T_ID=@P0", CInt(V(r, "SourceDocumentId")))
                    If s.References("CopyHeader").Rows.Count <> 1 OrElse CInt(s.References("CopyHeader").Rows(0)("BsType_ID")) <> sourceKind OrElse CInt(s.References("CopyHeader").Rows(0)("isVoid")) <> 0 Then Throw New InvalidOperationException("المستند المنسوخ غير صالح.")
                    s.References("CopyRows") = Query(cn, tx, "SELECT * FROM dbo.[" & sourceTable & "] WITH(HOLDLOCK) WHERE [" & sourceOwner & "]=@P0 ORDER BY T_ID", CInt(V(r, "SourceDocumentId")))
                    If s.References("CopyRows").Rows.Count = 0 Then Throw New InvalidOperationException("المستند المنسوخ لا يحتوي على أصناف.")
                    For Each row As DataRow In s.References("CopyRows").Rows
                        itemIds.Add(CInt(row("IM_ID")))
                    Next
                End If
            End If
            s.Rows("InvDocument") = ReadEntity(cn, tx, "InvDocument", "T_ID IN(" & IdList(documentIds) & ")")
        ElseIf op = "Inventory.ItemCostRecounted" Then
            itemIds.Add(owner)
            For Each e In {"InvSaleCost", "InvReturnCost", "InvIssueCost", "InvReceiptCost", "InvTransferCost"}
                s.Rows(e) = ReadEntity(cn, tx, e, "IM_ID=@P0 AND CONVERT(date,[Date]) BETWEEN @P1 AND @P2", owner, DateTime.ParseExact(V(r, "FromDate"), "yyyy-MM-dd", CultureInfo.InvariantCulture), DateTime.ParseExact(V(r, "ToDate"), "yyyy-MM-dd", CultureInfo.InvariantCulture))
            Next
        Else
            s.Rows("InvItem") = ReadEntity(cn, tx, "InvItem", "isStore IN(1,2)")
            For Each row As DataRow In s.Rows("InvItem").Rows
                itemIds.Add(CInt(row("IM_ID")))
            Next
            If op = "Inventory.QuantityRecounted" Then
                s.Rows("InvStore") = ReadEntity(cn, tx, "InvStore", "ST_ID=@P0", owner)
                If s.Rows("InvStore").Rows.Count <> 1 Then Throw New InvalidOperationException("المخزن غير موجود.")
                ' Deliberately all stores: legacy expiry deletion is not limited to the selected ST_ID.
                s.Rows("InvPhysicalStock") = ReadEntity(cn, tx, "InvPhysicalStock", "IM_ID IN(" & IdList(itemIds) & ")")
            Else
                s.Rows("InvPurchase") = ReadEntity(cn, tx, "InvPurchase", "IM_ID IN(" & IdList(itemIds) & ")")
            End If
        End If
        Dim items = IdList(itemIds)
        If Not s.Rows.ContainsKey("InvItem") AndAlso AuditContract.InventoryEntities(op).Contains("InvItem") Then
            s.Rows("InvItem") = ReadEntity(cn, tx, "InvItem", "IM_ID IN(" & items & ")")
            If s.Rows("InvItem").Rows.Count <> itemIds.Count Then Throw New InvalidOperationException("مرجع صنف مفقود.")
        End If
        If AuditContract.InventoryEntities(op).Contains("InvUnit") Then s.Rows("InvUnit") = ReadEntity(cn, tx, "InvUnit", "IM_ID IN(" & items & ")")
        If op.StartsWith("Inventory.Purchase", StringComparison.Ordinal) Then
            For Each e In {"InvCostBatch", "InvCostImpact", "InvCostJournal"}
                s.Rows(e) = ReadEntity(cn, tx, e, "IM_ID IN(" & items & ")")
            Next
        End If
        If AuditContract.InventoryEntities(op).Contains("InvStockBucket") Then s.Rows("InvStockBucket") = Query(cn, tx, "SELECT IM_ID,CONVERT(bigint,ST_ID) AS ST_ID,ISNULL(D_Vaild,'') AS D_Vaild,CONVERT(float,ISNULL(SUM(QTY),0)) AS Quantity FROM dbo.ST_Balance_V WITH(HOLDLOCK) WHERE IM_ID IN(" & items & ") GROUP BY IM_ID,ST_ID,ISNULL(D_Vaild,'') ORDER BY IM_ID,ST_ID,ISNULL(D_Vaild,'')")
        s.References("Units") = Query(cn, tx, "SELECT u.U_ID,u.U_Cargo,x.IM_ID,x.U_IM_ID FROM dbo.Units u WITH(HOLDLOCK) JOIN dbo.IM_Units x WITH(HOLDLOCK) ON x.U_ID=u.U_ID WHERE x.IM_ID IN(" & items & ") ORDER BY x.U_IM_ID")
        Dim selectedUnit = CInt(V(r, "SelectedUnitId", "0"))
        Dim selectedItem = CInt(V(r, "SelectedItemId", "0"))
        If op.EndsWith("Changed", StringComparison.Ordinal) Then selectedItem = CInt(s.Rows(If(kind = 7 OrElse kind = 9, "InvPurchase", "InvProduct")).Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = target)("IM_ID"))
        If selectedUnit > 0 AndAlso Not s.References("Units").Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("U_ID")) = selectedUnit AndAlso CInt(x("IM_ID")) = selectedItem AndAlso Not IsDBNull(x("U_Cargo")) AndAlso CDbl(x("U_Cargo")) > 0R) Then Throw New InvalidOperationException("الوحدة غير مرتبطة بالصنف أو سعتها غير صحيحة.")
        If V(r, "SelectedStoreId") <> "" AndAlso CInt(Query(cn, tx, "SELECT COUNT(*) FROM dbo.Stores WITH(HOLDLOCK) WHERE ST_ID=@P0", CInt(V(r, "SelectedStoreId"))).Rows(0)(0)) <> 1 Then Throw New InvalidOperationException("المخزن المحدد غير موجود.")
        ' Locks the cost/quantity dependency inputs, including unapproved/void rows used by legacy calculations.
        If op.StartsWith("Inventory.Purchase", StringComparison.Ordinal) OrElse op = "Inventory.QuantityRecounted" OrElse op = "Inventory.AllCostsRecounted" Then
            For Each table In {"Pch_Details", "SB_Contents", "Rtn_Contents", "Store_Trans_Details", "FRM_Details", "FRM_Contents_Details", "IMEX_Details", "InSale_Details", "Outsale_Details", "ST_QTY_Deferences"}
                s.References("Flow." & table) = Query(cn, tx, "SELECT * FROM dbo.[" & table & "] WITH(HOLDLOCK) WHERE IM_ID IN(" & items & ") ORDER BY T_ID")
            Next
        End If
        s.ItemIds = itemIds.OrderBy(Function(x) x).ToArray() : s.DocumentIds = documentIds.OrderBy(Function(x) x).ToArray()
        If s.Rows.Values.Any(Function(x) x.Rows.Count > 10000) OrElse s.References.Values.Any(Function(x) x.Rows.Count > 30000) Then Throw New InvalidOperationException("نطاق إعادة الاحتساب يتجاوز سعة المراجعة الآمنة؛ لم تحفظ العملية.")
        Return s
    End Function

    Friend Shared Function Fingerprint(s As AuditInventorySnapshot) As String
        Dim text = String.Join("|", s.Rows.Concat(s.References).Select(Function(x) x.Key & ":" & TableFingerprint(x.Value)))
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(text))).Replace("-", "")
    End Function

    Friend Shared Function RowFingerprint(row As DataRow) As String
        Dim pieces As New List(Of String)
        For Each column As DataColumn In row.Table.Columns
            Dim t = column.DataType
            Dim kind = If(t Is GetType(Guid), "Guid", If(t Is GetType(Double), "Float", If(t Is GetType(Decimal), "Decimal", If(t Is GetType(DateTime), "DateTime", If(t Is GetType(Boolean), "Boolean", If(t Is GetType(Integer) OrElse t Is GetType(Long) OrElse t Is GetType(Short) OrElse t Is GetType(Byte), "Integer", "String"))))))
            Dim value = AuditValue.Capture(kind, row(column))
            pieces.Add(column.ColumnName & ":" & CInt(value.State).ToString(CultureInfo.InvariantCulture) & ":" & If(value.Text, "").Length.ToString(CultureInfo.InvariantCulture) & ":" & If(value.Text, ""))
        Next
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(String.Join("|", pieces)))).Replace("-", "")
    End Function

    Friend Shared Function TableFingerprint(table As DataTable) As String
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(String.Join("|", table.Rows.Cast(Of DataRow)().Select(Function(x) RowFingerprint(x)))))).Replace("-", "")
    End Function

    Friend Shared Sub Prepare(connection As String, r As AuditOperationRequest)
        AuditContract.Request(r)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Guard(cn, tx) : Permission(cn, tx, r)
                r.Inputs.Single(Function(x) x.Name = "ExpectedInventorySnapshot").Value = AuditValue.Capture("String", Fingerprint(Capture(cn, tx, r)))
                tx.Commit()
            End Using
        End Using
    End Sub

    Private Shared Sub Bind(command As SqlCommand, r As AuditOperationRequest)
        For Each d In AuditContract.InventoryInputs(r.OperationCode)
            Dim p = d.Split("|"c)
            If p(1) = "" Then Continue For
            Dim x = r.Inputs.Single(Function(i) i.Name = p(0)).Value, value As Object = DBNull.Value
            If x.State = AuditValueState.Present Then
                Select Case p(2)
                    Case "Integer" : value = Integer.Parse(x.Text, CultureInfo.InvariantCulture)
                    Case "Float" : value = Double.Parse(x.Text, CultureInfo.InvariantCulture)
                    Case "Decimal" : value = Decimal.Parse(x.Text, CultureInfo.InvariantCulture)
                    Case "Boolean" : value = x.Text = "1"
                    Case "Date" : value = DateTime.ParseExact(x.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    Case "String" : value = x.Text
                End Select
            End If
            command.Parameters.AddWithValue("@" & p(1), value)
        Next
        Dim op = r.OperationCode, ownerParameter = If(op = "Inventory.PurchaseLineChanged" OrElse op.EndsWith("Added", StringComparison.Ordinal), "Pch_T_ID", If(op.EndsWith("Copied", StringComparison.Ordinal), "FRM_T_ID", If(op = "Manufacturing.CostRecalculated", "T_ID", If(op = "Inventory.QuantityRecounted", "ST_ID", If(op = "Inventory.ItemCostRecounted", "IM_ID", "")))))
        If ownerParameter <> "" Then command.Parameters.AddWithValue("@" & ownerParameter, CInt(r.EntityId))
    End Sub

    Private Shared Sub AllocationLock(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, s As AuditInventorySnapshot)
        If Not (r.OperationCode = "Manufacturing.AutoProductAdded" OrElse (r.OperationCode = "Manufacturing.ManualLineAdded" AndAlso V(r, "Product") = "1") OrElse r.OperationCode = "Manufacturing.SalesCopied") Then Return
        Dim range = Query(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If range.Rows.Count <> 1 OrElse IsDBNull(range.Rows(0)(0)) OrElse IsDBNull(range.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم التصنيع غير صالح.")
        Dim first = CLng(range.Rows(0)(0)), last = CLng(range.Rows(0)(1)), amount = If(r.OperationCode = "Manufacturing.SalesCopied", s.References("CopyRows").Rows.Count, 1)
        Dim maximum = CLng(Query(cn, tx, "SELECT ISNULL(MAX(T_ID),@P0) FROM dbo.FRM_Details WITH(UPDLOCK,HOLDLOCK) WHERE T_ID BETWEEN @P0 AND @P1", first, last).Rows(0)(0))
        If first < 0 OrElse last <= first OrElse maximum + amount > last OrElse maximum + amount > Integer.MaxValue Then Throw New InvalidOperationException("نطاق ترقيم التصنيع ممتلئ.")
    End Sub

    Friend Shared Function Events(r As AuditOperationRequest, before As AuditInventorySnapshot, after As AuditInventorySnapshot, occurred As DateTime) As List(Of AuditEventRequest)
        Dim list As New List(Of AuditEventRequest)
        Dim add As Action(Of String, String, DataRow, DataRow) = Sub(entity, key, b, a)
            Dim fields = AuditContract.InventoryFields(entity)
            If list.Count = 0 OrElse list.Last().Changes.Count + fields.Length > 100 Then list.Add(New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = list.Count + 1, .EventCode = r.OperationCode, .EntityType = r.EntityType, .EntityId = r.EntityId, .OccurredAtUtc = occurred, .Outcome = 1})
            For Each d In fields
                Dim p = d.Split("|"c)
                list.Last().Changes.Add(New AuditFieldChange With {.ChangeNo = list.Last().Changes.Count + 1, .EntityType = entity, .EntityId = key, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = CByte(If(entity = "InvStockBucket", 3, 1)),
                    .Before = If(b Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), b(p(1)))),
                    .After = If(a Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), a(p(1))))})
            Next
        End Sub
        For Each entity In before.Rows.Keys.Union(after.Rows.Keys).OrderBy(Function(x) x, StringComparer.Ordinal)
            Dim prior = before.Rows(entity), current = after.Rows(entity), pairs As New SortedDictionary(Of String, DataRow())(StringComparer.Ordinal)
            For side = 0 To 1
                For Each row As DataRow In If(side = 0, prior, current).Rows
                    Dim key = If(entity = "InvStockBucket", BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(CStr(row("IM_ID")) & "|" & CStr(row("ST_ID")) & "|" & CStr(row("D_Vaild"))))).Replace("-", ""), Convert.ToString(row(TableSpec(entity)(1)), CultureInfo.InvariantCulture))
                    If String.IsNullOrWhiteSpace(key) Then Throw New InvalidOperationException("صف مخزني دون مفتاح.")
                    If Not pairs.ContainsKey(key) Then pairs.Add(key, New DataRow(1) {})
                    If pairs(key)(side) IsNot Nothing Then Throw New InvalidOperationException("مفتاح مخزني مكرر؛ لا يمكن إسقاط الصفوف.")
                    pairs(key)(side) = row
                Next
            Next
            For Each pair In pairs
                Dim b = pair.Value(0), a = pair.Value(1)
                If b Is Nothing OrElse a Is Nothing OrElse RowFingerprint(b) <> RowFingerprint(a) OrElse entity = "InvDocument" OrElse entity = "InvStore" OrElse (r.OperationCode = "Inventory.ItemCostRecounted" AndAlso entity = "InvItem") Then add(entity, pair.Key, b, a)
            Next
        Next
        ' A confirmed no-op remains a real operation; capture the unchanged first eligible card/row.
        If list.Count = 0 Then
            For Each pair In before.Rows
                If pair.Value.Rows.Count > 0 Then
                    Dim row = pair.Value.Rows(0), key = Convert.ToString(row(TableSpec(pair.Key)(1)), CultureInfo.InvariantCulture)
                    add(pair.Key, key, row, after.Rows(pair.Key).Rows(0)) : Exit For
                End If
            Next
        End If
        If list.Count = 0 OrElse list.Count > 100 Then Throw New InvalidOperationException("تفاصيل العملية تتجاوز سعة السجل أو لا توجد بيانات قابلة للمراقبة؛ لم تحفظ العملية.")
        For Each e In list
            AuditContract.EventData(r, e)
        Next
        Return list
    End Function

    Friend Shared Function QuantityMatches(actual As Object, requested As String) As Boolean
        If actual Is Nothing OrElse IsDBNull(actual) Then Return False
        ' All routed procedures accept numeric(18,2), but purchase/ingredient columns store float.
        ' Compare in the physical column type; converting float back to Decimal can lose digits.
        Dim expected = Decimal.Round(Decimal.Parse(requested, CultureInfo.InvariantCulture), 2, MidpointRounding.AwayFromZero)
        If TypeOf actual Is Double Then Return CDbl(actual) = CDbl(expected)
        If TypeOf actual Is Decimal Then Return CDec(actual) = expected
        Throw New InvalidOperationException("تغير نوع عمود الكمية عن النوع الذي تمت مراجعته.")
    End Function

    Friend Shared Sub VerifyEffect(r As AuditOperationRequest, before As AuditInventorySnapshot, after As AuditInventorySnapshot)
        Dim op = r.OperationCode, target = CInt(V(r, "TargetLineId", "0"))
        If op.EndsWith("Deleted", StringComparison.Ordinal) OrElse op.EndsWith("Changed", StringComparison.Ordinal) Then
            Dim entity = If(op.StartsWith("Inventory.Purchase", StringComparison.Ordinal), "InvPurchase", If(op = "Manufacturing.IngredientDeleted", "InvIngredient", "InvProduct"))
            Dim found = after.Rows(entity).Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = target)
            If op.EndsWith("Deleted", StringComparison.Ordinal) Then
                If found IsNot Nothing Then Throw New InvalidOperationException("لم يتأكد حذف السطر.")
            Else
                If found Is Nothing OrElse CInt(found("U_ID")) <> CInt(V(r, "SelectedUnitId")) OrElse Not QuantityMatches(found("QYT"), V(r, "RequestedQuantity")) Then Throw New InvalidOperationException("تعديل الوحدة/الكمية لا يطابق الطلب.")
            End If
        ElseIf op.EndsWith("Added", StringComparison.Ordinal) OrElse op.EndsWith("Copied", StringComparison.Ordinal) Then
            Dim entity = If(op = "Manufacturing.PurchaseCopied" OrElse (op = "Manufacturing.ManualLineAdded" AndAlso V(r, "Product") = "0"), "InvIngredient", "InvProduct")
            Dim oldKeys = New HashSet(Of Integer)(before.Rows(entity).Rows.Cast(Of DataRow)().Select(Function(x) CInt(x("T_ID"))))
            Dim added = after.Rows(entity).Rows.Cast(Of DataRow)().Where(Function(x) Not oldKeys.Contains(CInt(x("T_ID")))).ToArray()
            Dim expected = If(op.EndsWith("Copied", StringComparison.Ordinal), before.References("CopyRows").Rows.Count, 1)
            If added.Length <> expected OrElse after.Rows(entity).Rows.Count <> before.Rows(entity).Rows.Count + expected Then Throw New InvalidOperationException("عدد الصفوف المنشأة غير مطابق.")
            If expected = 1 AndAlso Not op.EndsWith("Copied", StringComparison.Ordinal) Then
                Dim row = added(0)
                If CInt(row("IM_ID")) <> CInt(V(r, "SelectedItemId")) OrElse CInt(row("U_ID")) <> CInt(V(r, "SelectedUnitId")) OrElse CInt(row("ST_ID")) <> CInt(V(r, "SelectedStoreId")) OrElse Not QuantityMatches(row("QYT"), V(r, "RequestedQuantity")) Then Throw New InvalidOperationException("الصف المنشأ لا يطابق الاختيار.")
            End If
        End If
    End Sub

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsInventoryOperation(request.OperationCode) Then Throw New ArgumentException("يلزم عقد المخزون ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request
            Guard(cn, tx) : Permission(cn, tx, r)
            Dim before = Capture(cn, tx, r)
            If Fingerprint(before) <> V(r, "ExpectedInventorySnapshot") Then Throw New InvalidOperationException("تغير المستند أو المخزون أو مراجع التكلفة؛ حدّث العرض قبل إعادة العملية.")
            AllocationLock(cn, tx, r, before)
            Using cmd = AuditRequestCoordinator.Command("dbo.[" & AuditContract.InventoryProcedure(r.OperationCode) & "]", cn, tx)
                cmd.CommandTimeout = 180 : Bind(cmd, r) : cmd.ExecuteNonQuery()
            End Using
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            Dim after = Capture(cn, tx, r, before)
            VerifyEffect(r, before, after)
            For Each e In Events(r, before, after, AuditRequestCoordinator.ServerUtcNow(cn, tx))
                AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

    Friend Shared Function Save(connection As String, request As AuditOperationRequest) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim result = ExecuteInTransaction(cn, tx, request)
                    tx.Commit() : Return result
                Catch
                    Try
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    Catch
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Function
End Class
