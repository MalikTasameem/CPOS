Public Class DraftItemService

    Public Shared Sub AddItem(draft As SaleDraftHeader, item As SaleDraftItem)

        Dim existing = draft.Items.FirstOrDefault(
                Function(x) x.IM_ID = item.IM_ID AndAlso
                            x.U_ID = item.U_ID AndAlso
                            x.ST_ID = item.ST_ID AndAlso
                            x.Price = item.Price
            )

        If existing IsNot Nothing Then
            existing.QTY += item.QTY
            existing.Date_ = DateTime.Now
        Else
            item.DraftLineId = Guid.NewGuid().ToString()
            item.Date_ = DateTime.Now
            draft.Items.Add(item)
        End If

        System.Media.SystemSounds.Exclamation.Play()
        DraftCalculator.RecalculateDraft(draft)


    End Sub

    Public Shared Sub RemoveItem(draft As SaleDraftHeader, draftLineId As String)

        Dim item = draft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

        If item IsNot Nothing Then
            draft.Items.Remove(item)
        End If

        DraftCalculator.RecalculateDraft(draft)

    End Sub

End Class
