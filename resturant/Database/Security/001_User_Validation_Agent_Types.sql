SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.User_Validation_AgentTypes', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.User_Validation_AgentTypes
        (
            Validation_ID INT NOT NULL,
            Agent_Type_ID INT NOT NULL,
            CONSTRAINT PK_User_Validation_AgentTypes PRIMARY KEY CLUSTERED (Validation_ID, Agent_Type_ID),
            CONSTRAINT CK_User_Validation_AgentTypes_AgentType CHECK (Agent_Type_ID > 0),
            CONSTRAINT FK_User_Validation_AgentTypes_Validation FOREIGN KEY (Validation_ID)
                REFERENCES dbo.Users_Validations(T_ID) ON DELETE CASCADE
        );
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.User_Validation_AgentTypes')
          AND name = N'IX_User_Validation_AgentTypes_Type'
    )
    BEGIN
        CREATE NONCLUSTERED INDEX IX_User_Validation_AgentTypes_Type
            ON dbo.User_Validation_AgentTypes(Agent_Type_ID, Validation_ID);
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
