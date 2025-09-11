CREATE OR ALTER PROCEDURE [dbo].[usp_CreatePaymentMethod]
    @Name NVARCHAR(100),
    @IsActive BIT,
    @Description NVARCHAR(500) = NULL  -- optional, can be null
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id UNIQUEIDENTIFIER = NEWID();  -- generate new GUID

    INSERT INTO dbo.PaymentMethods (Id, Name, IsActive, Description)
    VALUES (@Id, @Name, @IsActive, @Description);

    -- Return the newly created row
    SELECT Id, Name, IsActive, Description
    FROM dbo.PaymentMethods
    WHERE Id = @Id;
END;
