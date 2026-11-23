-- In SSMS, select a new, empty application database before running this file.
-- Set @InsertSeedUsers to 0 if you will import the Users table from MySQL.
-- This script does not copy existing MySQL rows or set up EF migration history.
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @InsertSeedUsers bit = 1;

IF DB_ID() <= 4
    THROW 50000, 'Select an empty application database, not a system database.', 1;

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.Users') IS NOT NULL
        OR OBJECT_ID(N'dbo.ReimbursementRequests') IS NOT NULL
        OR OBJECT_ID(N'dbo.RequestDecisions') IS NOT NULL
        OR OBJECT_ID(N'dbo.getallrequests') IS NOT NULL
        OR OBJECT_ID(N'dbo.__EFMigrationsHistory') IS NOT NULL
        THROW 50001, 'Application objects already exist. Run this script on a fresh target database.', 1;

    CREATE TABLE dbo.Users (
        Id uniqueidentifier NOT NULL,
        Name nvarchar(100) NOT NULL CONSTRAINT DF_Users_Name DEFAULT (N''),
        Role int NOT NULL,
        Email nvarchar(100) NOT NULL CONSTRAINT DF_Users_Email DEFAULT (N''),
        CONSTRAINT PK_Users PRIMARY KEY (Id)
    );

    CREATE TABLE dbo.ReimbursementRequests (
        RequestId uniqueidentifier NOT NULL,
        EmployeeId uniqueidentifier NOT NULL,
        CourseName nvarchar(100) NOT NULL,
        ClaimAmount decimal(18, 2) NOT NULL,
        RequestStatus int NOT NULL,
        CONSTRAINT PK_ReimbursementRequests PRIMARY KEY (RequestId),
        CONSTRAINT FK_ReimbursementRequests_Users_EmployeeId
            FOREIGN KEY (EmployeeId) REFERENCES dbo.Users (Id) ON DELETE NO ACTION
    );

    CREATE TABLE dbo.RequestDecisions (
        DecisionId uniqueidentifier NOT NULL,
        RequestId uniqueidentifier NOT NULL,
        HrId uniqueidentifier NOT NULL,
        RequestStatus int NOT NULL,
        CONSTRAINT PK_RequestDecisions PRIMARY KEY (DecisionId),
        CONSTRAINT FK_RequestDecisions_ReimbursementRequests_RequestId
            FOREIGN KEY (RequestId) REFERENCES dbo.ReimbursementRequests (RequestId) ON DELETE NO ACTION,
        CONSTRAINT FK_RequestDecisions_Users_HrId
            FOREIGN KEY (HrId) REFERENCES dbo.Users (Id) ON DELETE NO ACTION
    );

    CREATE INDEX IX_ReimbursementRequests_EmployeeId ON dbo.ReimbursementRequests (EmployeeId);
    CREATE INDEX IX_RequestDecisions_HrId ON dbo.RequestDecisions (HrId);
    CREATE UNIQUE INDEX IX_RequestDecisions_RequestId ON dbo.RequestDecisions (RequestId);

    IF @InsertSeedUsers = 1
    BEGIN
        INSERT INTO dbo.Users (Id, Email, Name, Role)
        VALUES
            ('f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e01', N'srinath@example.com', N'Srinath', 0),
            ('f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e02', N'sathvika@example.com', N'Sathvika', 0),
            ('f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e03', N'meghana@example.com', N'Meghana', 1),
            ('f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e04', N'sathya@example.com', N'Sathya', 1);
    END;

    -- CREATE PROCEDURE must be the first statement in its batch.
    EXEC (N'CREATE PROCEDURE dbo.getallrequests
    AS
    BEGIN
        SET NOCOUNT ON;
        SELECT RequestId, EmployeeId, CourseName, ClaimAmount, RequestStatus
        FROM dbo.ReimbursementRequests;
    END;');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
