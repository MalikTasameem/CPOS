Public Class DraftCalculator

    Public Shared Sub RecalculateDraft(draft As SaleDraftHeader)

        If draft Is Nothing Then Exit Sub

        ' إعادة حساب كل سطر
        For Each item In draft.Items
            item.T_Price = item.Price * item.QTY
            item.ST_QTY = CDec(item.QTY * CDec(item.U_Cargo))
        Next

        ' إعادة حساب الإجمالي
        draft.Total = draft.Items.Sum(Function(x) x.T_Price)

        ' الصافي
        draft.Pure = draft.Total - draft.Discount

        draft.UpdatedAt = DateTime.Now

    End Sub

End Class