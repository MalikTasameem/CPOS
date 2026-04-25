Imports System.IO
Imports Newtonsoft.Json

Public Class DraftSalesManager

    Private ReadOnly ActiveFolder As String =
        Path.Combine(Application.StartupPath, "DraftSales\Active")

    Private ReadOnly ArchiveFolder As String =
        Path.Combine(Application.StartupPath, "DraftSales\Archived")

    Public Sub New()

        If Not Directory.Exists(ActiveFolder) Then
            Directory.CreateDirectory(ActiveFolder)
        End If

        If Not Directory.Exists(ArchiveFolder) Then
            Directory.CreateDirectory(ArchiveFolder)
        End If

    End Sub

    ' إنشاء مسودة جديدة
    Public Function CreateNewDraft(userId As Integer) As SaleDraftHeader

        Dim d As New SaleDraftHeader With {
            .DraftId = Guid.NewGuid().ToString(),
            .DraftName = "مسودة جديدة",
            .CreatedAt = DateTime.Now,
            .UpdatedAt = DateTime.Now,
            .Date = DateTime.Now,
            .User_ID = userId,
            .Total = 0D,
            .Discount = 0D,
            .Pure = 0D,
            .S_Bill_Pr_ID = Pr_ID
        }

        SaveDraft(d)
        Return d

    End Function

    ' حفظ
    Public Sub SaveDraft(draft As SaleDraftHeader)

        Dim filePath = Path.Combine(ActiveFolder, draft.DraftId & ".json")
        draft.UpdatedAt = DateTime.Now

        Dim json = JsonConvert.SerializeObject(draft, Formatting.Indented)
        File.WriteAllText(filePath, json)

    End Sub

    ' تحميل
    Public Function LoadDraft(draftId As String) As SaleDraftHeader

        Dim filePath = Path.Combine(ActiveFolder, draftId & ".json")
        If Not File.Exists(filePath) Then Return Nothing

        Dim json = File.ReadAllText(filePath)
        Return JsonConvert.DeserializeObject(Of SaleDraftHeader)(json)

    End Function

    ' حذف
    Public Sub DeleteDraft(draftId As String)

        Dim filePath = Path.Combine(ActiveFolder, draftId & ".json")
        If File.Exists(filePath) Then File.Delete(filePath)

    End Sub

    ' جلب كل المسودات
    Public Function GetAllDrafts() As List(Of SaleDraftHeader)

        Dim list As New List(Of SaleDraftHeader)

        For Each filePath As String In Directory.GetFiles(ActiveFolder, "*.json")
            Try
                Dim json As String = File.ReadAllText(filePath)
                Dim d As SaleDraftHeader = JsonConvert.DeserializeObject(Of SaleDraftHeader)(json)

                If d IsNot Nothing Then
                    list.Add(d)
                End If
            Catch
            End Try
        Next

        Return list.OrderByDescending(Function(x) x.UpdatedAt).ToList()

    End Function

    ' أرشفة بعد النجاح
    Public Sub ArchiveDraft(draft As SaleDraftHeader)

        draft.UpdatedAt = DateTime.Now

        Dim source = Path.Combine(ActiveFolder, draft.DraftId & ".json")
        Dim target = Path.Combine(ArchiveFolder, draft.DraftId & ".json")

        Dim json = JsonConvert.SerializeObject(draft, Formatting.Indented)

        File.WriteAllText(source, json)

        If File.Exists(target) Then
            File.Delete(target)
        End If

        File.Move(source, target)


        '----------------------------------------------------------------------------------
        'Dim source = Path.Combine(ActiveFolder, draft.DraftId & ".json")
        'Dim target = Path.Combine(ArchiveFolder, draft.DraftId & ".json")

        'If File.Exists(source) Then
        '    File.Move(source, target)
        'End If

    End Sub

End Class