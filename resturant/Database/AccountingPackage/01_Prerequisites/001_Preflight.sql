SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @ProductMajorVersion INT;
DECLARE @CompatibilityLevel INT;

SET @ProductMajorVersion = CONVERT(INT, PARSENAME(CONVERT(VARCHAR(50), SERVERPROPERTY('ProductVersion')), 4));
SELECT @CompatibilityLevel = compatibility_level
FROM sys.databases
WHERE name = DB_NAME();

IF @ProductMajorVersion < 12
BEGIN
    RAISERROR(N'تتطلب الحزمة SQL Server 2014 أو أحدث.', 16, 1);
    RETURN;
END;

IF ISNULL(@CompatibilityLevel, 0) < 120
BEGIN
    RAISERROR(N'يجب أن يكون Compatibility Level لقاعدة البيانات 120 أو أحدث.', 16, 1);
    RETURN;
END;

IF DATABASEPROPERTYEX(DB_NAME(), 'Updateability') <> 'READ_WRITE'
BEGIN
    RAISERROR(N'قاعدة البيانات ليست في وضع READ_WRITE.', 16, 1);
    RETURN;
END;

SELECT
    DB_NAME() AS DatabaseName,
    CONVERT(VARCHAR(50), SERVERPROPERTY('ProductVersion')) AS ProductVersion,
    CONVERT(NVARCHAR(100), SERVERPROPERTY('Edition')) AS Edition,
    @CompatibilityLevel AS CompatibilityLevel,
    N'PASS' AS PreflightStatus;

