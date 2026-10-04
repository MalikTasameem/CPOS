SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_GetNextIdBlock', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_GetNextIdBlock AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_GetNextIdBlock
    @SequenceName SYSNAME,
    @BlockSize    INT = 1,
    @FirstValue   INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @BlockSize IS NULL OR @BlockSize <= 0
    BEGIN
        RAISERROR(N'BlockSize must be greater than zero.', 16, 1);
        RETURN;
    END;

    SET @FirstValue = NULL;

    UPDATE dbo.ACC_INTERNAL_SEQUENCES WITH (UPDLOCK, HOLDLOCK)
    SET
        @FirstValue = NextValue,
        NextValue = NextValue + @BlockSize
    WHERE SequenceName = @SequenceName;

    IF @FirstValue IS NULL
    BEGIN
        RAISERROR(N'Internal sequence is not configured.', 16, 1);
        RETURN;
    END;
END;
GO

