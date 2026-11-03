Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

' Offline tests only: synthetic DataTables. No connection strings or database calls.
Friend Module AuditInventoryOperationsTests
    Private checks As Integer
    Private ReadOnly utc As New DateTime(2026, 10, 27, 12, 0, 0, DateTimeKind.Utc)
    Private ReadOnly operations As String() = {"Inventory.PurchaseLineDeleted", "Inventory.PurchaseLineChanged", "Manufacturing.AutoProductAdded", "Manufacturing.ManualLineAdded", "Manufacturing.ProductDeleted", "Manufacturing.ProductChanged", "Manufacturing.IngredientDeleted", "Manufacturing.CostRecalculated", "Manufacturing.PurchaseCopied", "Manufacturing.SalesCopied", "Inventory.QuantityRecounted", "Inventory.ItemCostRecounted", "Inventory.AllCostsRecounted"}
    Private Sub Check(ok As Boolean, label As String)
        If Not ok Then Throw New Exception("FAIL " & label)
        checks += 1
    End Sub
    Private Sub Reject(work As Action, label As String)
        Try
            work()
        Catch ex As ArgumentException
            Check(True, label) : Return
        Catch ex As InvalidOperationException
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection: " & label)
    End Sub
    Private Function Screen(op As String) As String
        Select Case op
            Case "Inventory.PurchaseLineDeleted" : Return "Pch"
            Case "Inventory.PurchaseLineChanged", "Manufacturing.ProductChanged" : Return "Change_IM_Details"
            Case "Manufacturing.AutoProductAdded" : Return "Frm_auto_IM_card"
            Case "Manufacturing.ManualLineAdded" : Return "Frm_manuel_IM_card"
            Case "Inventory.ItemCostRecounted" : Return "Recount_IM_Cost"
            Case "Inventory.QuantityRecounted", "Inventory.AllCostsRecounted" : Return "STORES_Explorer"
            Case Else : Return "Format_Items_Manual"
        End Select
    End Function
    Private Function Context(op As String) As AuditEventContext
        Return New AuditEventContext With {.SourceId = Guid.NewGuid(), .SessionId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "اختبار", .ScreenCode = Screen(op), .MachineName = "OfflineTest", .ApplicationVersion = "036"}
    End Function
    Private Function Request(op As String) As AuditOperationRequest
        Dim cmd As New SqlCommand(If(op = "Inventory.AllCostsRecounted", "RECOUNT_IM_ALL_MainCost", AuditContract.InventoryProcedure(op)))
        Dim owner = If(op = "Inventory.AllCostsRecounted", 1, 50)
        Dim kind = If(op.StartsWith("Inventory.Purchase", StringComparison.Ordinal), 7, If(op.StartsWith("Manufacturing.", StringComparison.Ordinal), If(op = "Manufacturing.AutoProductAdded", 13, 18), 0))
        For Each definition In AuditContract.InventoryInputs(op)
            Dim p = definition.Split("|"c)
            If p(1) = "" Then Continue For
            Dim value As Object = Nothing
            Select Case p(2)
                Case "Integer" : value = If(p(0) = "TargetLineId", 71, If(p(0) = "NoStock", 0, 2))
                Case "Boolean" : value = False
                Case "Decimal" : value = 2.345D
                Case "Float" : value = 4.5R
                Case "String" : value = If(p(0) = "CopyTarget", "FRM_Details", "test")
                Case "Date" : value = New DateTime(2026, 10, 27)
            End Select
            cmd.Parameters.AddWithValue("@" & p(1), value)
        Next
        Dim ownerParameter = If(op = "Inventory.PurchaseLineChanged" OrElse op.EndsWith("Added", StringComparison.Ordinal), "Pch_T_ID", If(op.EndsWith("Copied", StringComparison.Ordinal), "FRM_T_ID", If(op = "Manufacturing.CostRecalculated", "T_ID", If(op = "Inventory.QuantityRecounted", "ST_ID", If(op = "Inventory.ItemCostRecounted", "IM_ID", "")))))
        If ownerParameter <> "" Then cmd.Parameters.AddWithValue("@" & ownerParameter, owner)
        Return AuditInventoryOperations.Request(Context(op), cmd, owner, kind)
    End Function
    Private Function MakeTable(entity As String, Optional count As Integer = 1) As DataTable
        Dim dt As New DataTable()
        For Each definition In AuditContract.InventoryFields(entity)
            Dim p = definition.Split("|"c), type As Type = Nothing
            Select Case p(2)
                Case "Integer" : type = If(p(3) = "bigint", GetType(Long), GetType(Integer))
                Case "Decimal" : type = GetType(Decimal)
                Case "Float" : type = GetType(Double)
                Case "Boolean" : type = GetType(Boolean)
                Case "DateTime" : type = GetType(DateTime)
                Case "String" : type = GetType(String)
                Case "Guid" : type = GetType(Guid)
                Case Else : Throw New Exception("Unexpected type")
            End Select
            dt.Columns.Add(p(1), type)
        Next
        For index = 1 To count
            Dim row = dt.NewRow()
            For Each column As DataColumn In dt.Columns
                Select Case Type.GetTypeCode(column.DataType)
                    Case TypeCode.Int32, TypeCode.Int64 : row(column) = 2
                    Case TypeCode.Decimal : row(column) = 2.35D
                    Case TypeCode.Double : row(column) = 2.35R
                    Case TypeCode.Boolean : row(column) = False
                    Case TypeCode.DateTime : row(column) = New DateTime(2026, 10, 27)
                    Case TypeCode.String : row(column) = ""
                    Case Else : row(column) = New Guid("abcdf123-1122-3344-5566-778899aabbcc")
                End Select
            Next
            If entity <> "InvStockBucket" Then
                Dim key = AuditInventoryOperations.TableSpec(entity)(1)
                If dt.Columns(key).DataType IsNot GetType(Guid) Then row(key) = If(entity = "InvDocument", 50, index + 70)
            End If
            dt.Rows.Add(row)
        Next
        Return dt
    End Function
    Private Function Snapshot(op As String) As AuditInventorySnapshot
        Dim result As New AuditInventorySnapshot With {.ItemIds = {2}, .DocumentIds = {50}}
        For Each entity In AuditContract.InventoryEntities(op)
            result.Rows(entity) = MakeTable(entity)
        Next
        Return result
    End Function
    Private Sub SetInput(r As AuditOperationRequest, name As String, value As Object)
        Dim input = r.Inputs.Single(Function(x) x.Name = name)
        input.Value = AuditValue.Capture(input.ValueType, value)
    End Sub
    Public Function Main() As Integer
        Try
            For Each op In operations
                Dim r = Request(op), b = Snapshot(op), a = Snapshot(op)
                AuditContract.Request(r)
                Check(AuditContract.Domain(op) = "Inventory.Operations", "domain " & op)
                Check(AuditCanonical.RequestBytes(r).SequenceEqual(AuditCanonical.RequestBytes(r)), "canonical request " & op)
                Dim ev = AuditInventoryOperations.Events(r, b, a, utc)
                Check(ev.Count > 0, "no-op evidence " & op)
                For Each e In ev
                    Check(e.Changes.Count <= 100, "bounded complete event")
                    Check(AuditMessageFormatter.Format(r, e).Length > 0, "formatter " & op)
                    For Each group In e.Changes.GroupBy(Function(x) x.EntityType & "|" & x.EntityId)
                        Check(group.Count() = AuditContract.InventoryFields(group.First().EntityType).Length, "whole row")
                    Next
                Next
                Dim broken = AuditInventoryOperations.Events(r, b, a, utc)(0)
                broken.Changes.RemoveAt(0)
                Reject(Sub() AuditContract.EventData(r, broken), "missing field " & op)
                r.Context.ScreenCode = "Sales"
                Reject(Sub() AuditContract.Request(r), "wrong screen " & op)
                r.Context.ScreenCode = Screen(op)
                SetInput(r, "DocumentType", 1)
                Reject(Sub() AuditContract.Request(r), "wrong kind " & op)
                r = Request(op) : r.EntityId = "0"
                Reject(Sub() AuditContract.Request(r), "nonpositive owner")
                r = Request(op) : SetInput(r, "ExpectedInventorySnapshot", "bad")
                Reject(Sub() AuditContract.Request(r), "invalid fingerprint")
                r = Request(op) : r.Inputs.RemoveAt(0)
                Reject(Sub() AuditContract.Request(r), "missing request field")
            Next
            Dim rq = Request("Inventory.PurchaseLineChanged")
            SetInput(rq, "RequestedQuantity", 0.004D)
            Reject(Sub() AuditContract.Request(rq), "quantity rounds to zero")
            SetInput(rq, "RequestedQuantity", -1D)
            Reject(Sub() AuditContract.Request(rq), "negative quantity")
            SetInput(rq, "RequestedQuantity", 0.005D) : AuditContract.Request(rq)
            Check(True, "positive rounded quantity")
            Check(AuditInventoryOperations.QuantityMatches(2.35R, "2.345"), "float QYT")
            Check(AuditInventoryOperations.QuantityMatches(2.35D, "2.345"), "decimal QYT")
            Check(AuditInventoryOperations.QuantityMatches(CDbl(99999999999999.35D), "99999999999999.345"), "large float QYT")
            Check(Not AuditInventoryOperations.QuantityMatches(2.34R, "2.345"), "different quantity")
            Check(Not AuditInventoryOperations.QuantityMatches(DBNull.Value, "1"), "null quantity")
            Reject(Sub() AuditInventoryOperations.QuantityMatches(2, "2"), "schema type drift")
            rq = Request("Inventory.ItemCostRecounted")
            SetInput(rq, "FromDate", New DateTime(2026, 10, 28))
            Reject(Sub() AuditContract.Request(rq), "reversed dates")
            rq = Request("Manufacturing.SalesCopied")
            SetInput(rq, "CopyTarget", "SB_Contents")
            Reject(Sub() AuditContract.Request(rq), "dynamic copy target excluded")
            rq = Request("Manufacturing.ManualLineAdded")
            SetInput(rq, "Barcode", New String("x"c, 51))
            Reject(Sub() AuditContract.Request(rq), "barcode overflow")
            Reject(Sub() AuditInventoryOperations.Operation(New SqlCommand("DELETE FROM IM_Menu")), "no arbitrary SQL")
            Reject(Sub() AuditInventoryOperations.Request(Context("Inventory.QuantityRecounted"), New SqlCommand("RECOUNT_IM_ALL_QTY"), 50, 0), "owner parameter missing")

            For Each kind In {7, 9, 13, 18}
                rq = Request(If(kind = 7 OrElse kind = 9, "Inventory.PurchaseLineDeleted", "Manufacturing.ProductDeleted"))
                SetInput(rq, "DocumentType", kind)
                If kind = 7 Then SetInput(rq, "OnUpdate", True)
                Dim header = MakeTable("InvDocument")
                header.Rows(0)("BsType_ID") = kind : header.Rows(0)("isVoid") = 0
                header.Rows(0)("isDepended") = 1 : header.Rows(0)("EditOpenedBy") = 1
                AuditInventoryOperations.ValidateEditableHeader(rq, header)
                Check(True, "approved edited header " & kind)
                header.Rows(0)("EditOpenedBy") = 2
                Reject(Sub() AuditInventoryOperations.ValidateEditableHeader(rq, header), "other user's edit " & kind)
                header.Rows(0)("EditOpenedBy") = 1 : header.Rows(0)("EditOpenedAt") = DBNull.Value
                Reject(Sub() AuditInventoryOperations.ValidateEditableHeader(rq, header), "edit was not opened " & kind)
                header.Rows(0)("isDepended") = 0
                AuditInventoryOperations.ValidateEditableHeader(rq, header)
                Check(True, "unapproved header " & kind)
                header.Rows(0)("isVoid") = 1
                Reject(Sub() AuditInventoryOperations.ValidateEditableHeader(rq, header), "void header " & kind)
            Next

            Dim table = MakeTable("InvCostBatch")
            Dim original = AuditInventoryOperations.TableFingerprint(table)
            table.Rows(0)("BatchId") = Guid.NewGuid()
            Check(original <> AuditInventoryOperations.TableFingerprint(table), "GUID fingerprint")
            table = MakeTable("InvPurchase")
            original = AuditInventoryOperations.TableFingerprint(table)
            table.Rows(0)("Notes") = DBNull.Value
            Check(original <> AuditInventoryOperations.TableFingerprint(table), "SQL NULL differs from empty")
            table = MakeTable("InvStockBucket")
            original = AuditInventoryOperations.TableFingerprint(table)
            table.Rows(0)("Quantity") = 3R
            Check(original <> AuditInventoryOperations.TableFingerprint(table), "derived stock fingerprint")

            rq = Request("Inventory.PurchaseLineDeleted")
            Dim prior = Snapshot(rq.OperationCode), later = Snapshot(rq.OperationCode)
            later.Rows("InvPurchase").Clear()
            Dim deleted = AuditInventoryOperations.Events(rq, prior, later, utc).SelectMany(Function(x) x.Changes).Where(Function(x) x.EntityType = "InvPurchase").ToArray()
            Check(deleted.Length = AuditContract.InventoryFields("InvPurchase").Length, "deleted row complete")
            Check(deleted.All(Function(x) x.After.State = AuditValueState.Absent AndAlso x.Before.State <> AuditValueState.Absent), "deleted row states")
            AuditInventoryOperations.VerifyEffect(rq, prior, later)
            later.Rows("InvPurchase").ImportRow(prior.Rows("InvPurchase").Rows(0))
            Reject(Sub() AuditInventoryOperations.VerifyEffect(rq, prior, later), "delete effect required")
            rq = Request("Inventory.PurchaseLineChanged")
            prior = Snapshot(rq.OperationCode) : later = Snapshot(rq.OperationCode)
            AuditInventoryOperations.VerifyEffect(rq, prior, later)
            later.Rows("InvPurchase").Rows(0)("QYT") = 4R
            Reject(Sub() AuditInventoryOperations.VerifyEffect(rq, prior, later), "change effect required")

            rq = Request("Inventory.QuantityRecounted")
            prior = Snapshot(rq.OperationCode) : later = Snapshot(rq.OperationCode)
            later.Rows("InvStockBucket").Rows(0)("Quantity") = 3R
            Dim bucket = AuditInventoryOperations.Events(rq, prior, later, utc).SelectMany(Function(x) x.Changes).Where(Function(x) x.EntityType = "InvStockBucket").ToArray()
            Check(bucket.Length = 4 AndAlso bucket.All(Function(x) x.ValueOrigin = 3), "derived stock attribution")
            later.Rows("InvPhysicalStock").ImportRow(later.Rows("InvPhysicalStock").Rows(0))
            Reject(Sub() AuditInventoryOperations.Events(rq, prior, later, utc), "duplicate keys cannot disappear")

            rq = Request("Inventory.AllCostsRecounted")
            prior = Snapshot(rq.OperationCode) : later = Snapshot(rq.OperationCode)
            prior.Rows("InvPurchase") = MakeTable("InvPurchase", 300)
            later.Rows("InvPurchase") = prior.Rows("InvPurchase").Copy()
            For Each row As DataRow In later.Rows("InvPurchase").Rows
                row("AVG_Cost") = 9R
            Next
            Check(AuditInventoryOperations.Events(rq, prior, later, utc).Count = 100, "exact 100-event boundary")
            prior.Rows("InvPurchase") = MakeTable("InvPurchase", 301)
            later.Rows("InvPurchase") = prior.Rows("InvPurchase").Copy()
            For Each row As DataRow In later.Rows("InvPurchase").Rows
                row("AVG_Cost") = 9R
            Next
            Reject(Sub() AuditInventoryOperations.Events(rq, prior, later, utc), "101 events reject whole operation, never truncate")
            Console.WriteLine("PASS " & checks & " inventory/manufacturing offline checks.") : Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function
End Module
