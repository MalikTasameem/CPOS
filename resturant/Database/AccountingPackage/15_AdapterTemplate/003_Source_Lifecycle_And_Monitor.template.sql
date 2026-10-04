/* TEMPLATE ONLY - DO NOT EXECUTE AS-IS. */
RAISERROR(N'قالب فقط: استبدل أسماء جداول وحقول المشروع قبل التنفيذ.', 16, 1);
RETURN;
GO

/*
    فتح مستند للتعديل:
    - إذا كان JournalId فارغا فلا توجد إعادة تقييد؛ يبقى الترحيل الأول مطلوبا.
    - إذا كان موجودا تضبط NeedRepost وتزيد النسخة مرة واحدة عند فتح دورة التعديل.
*/
CREATE PROCEDURE dbo.[<PROJECT>_OpenSalesForEdit]
    @DocumentId <DOCUMENT_ID_SQL_TYPE>,
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.[<SALES_HEADER_TABLE>]
    SET
        OriginalJournalId = CASE
            WHEN OriginalJournalId IS NULL THEN JournalId
            ELSE OriginalJournalId END,
        NeedRepost = CASE WHEN JournalId IS NULL THEN 0 ELSE 1 END,
        EditVersion = CASE
            WHEN JournalId IS NULL THEN EditVersion
            ELSE EditVersion + 1 END,
        EditOpenedAt = GETDATE(),
        EditOpenedBy = @UserId
    WHERE [<DOCUMENT_ID_COLUMN>] = @DocumentId
      AND [<VOID_COLUMN>] = 0;

    IF @@ROWCOUNT <> 1
        RAISERROR(N'المستند غير موجود أو ملغي.', 16, 1);
END;
GO

/*
    بعد بناء @Rows من الحالة المعدلة يستدعي Adapter:

    EXEC dbo.ACC_Source_Repost ...

    ثم يحدث رأس المستند داخل نفس المعاملة:
      JournalId = @NewJournalId
      LastReversalJournalId = @ReversalJournalId
      NeedRepost = 0
      LastRepostAt = GETDATE()
      LastRepostBy = @UserId
*/

/*
    الإلغاء:
    1) يقفل رأس المستند.
    2) يضبط IsVoid وسبب الإلغاء.
    3) إذا لم يوجد JournalId فلا يستدعي العكس.
    4) إذا وجد، يستدعي ACC_Source_CancelPosting.
    5) يحفظ VoidReversalJournalId ويصفر NeedCancelReverse.
    كل ذلك داخل معاملة واحدة.
*/

/*
    قالب View المراقبة. كرر SELECT لكل نوع مستند بواسطة UNION ALL.
*/
CREATE VIEW dbo.[<PROJECT>_AccountingPostingMonitor]
AS
    SELECT
        N'<SALES_SOURCE_CODE>' AS SourceTable,
        CONVERT(NVARCHAR(100), H.[<DOCUMENT_ID_COLUMN>]) AS SourceKey,
        TRY_CONVERT(BIGINT, H.[<DOCUMENT_ID_COLUMN>]) AS SourceId,
        H.[<DOCUMENT_NUMBER_COLUMN>] AS DocumentNumber,
        H.[<DOCUMENT_DATE_COLUMN>] AS DocumentDate,
        H.JournalId,
        H.NeedRepost,
        H.NeedCancelReverse,
        H.[<VOID_COLUMN>] AS IsVoid,
        CASE
            WHEN H.[<VOID_COLUMN>] = 1 AND H.JournalId IS NOT NULL
                 AND H.NeedCancelReverse = 1 AND H.VoidReversalJournalId IS NULL
                THEN 'CANCEL_REVERSE'
            WHEN H.[<VOID_COLUMN>] = 1 AND H.VoidReversalJournalId IS NOT NULL
                THEN 'VOID_REVERSED'
            WHEN H.[<VOID_COLUMN>] = 1 AND H.JournalId IS NULL
                THEN 'VOID_NO_ACTION'
            WHEN H.[<VOID_COLUMN>] = 0 AND H.JournalId IS NULL AND H.NeedRepost = 0
                THEN 'POST_FIRST_TIME'
            WHEN H.[<VOID_COLUMN>] = 0 AND H.JournalId IS NOT NULL AND H.NeedRepost = 1
                THEN 'REPOST'
            WHEN H.[<VOID_COLUMN>] = 0 AND H.JournalId IS NOT NULL AND H.NeedRepost = 0
                THEN 'NO_ACTION'
            ELSE 'CHECK'
        END AS PostingAction
    FROM dbo.[<SALES_HEADER_TABLE>] AS H
    WHERE H.[<APPROVED_COLUMN>] = 1;
GO

