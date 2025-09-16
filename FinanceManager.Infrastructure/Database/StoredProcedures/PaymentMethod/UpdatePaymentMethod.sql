CREATE OR ALTER PROCEDURE [dbo].[usp_UpdatePaymentMethod]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @IsActive BIT,
    @Description NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.PaymentMethods
    SET 
        Name = @Name,
        IsActive = @IsActive,
        Description = @Description
	WHERE Id = @Id;

	    -- Return the  updated row
    SELECT Id, Name, IsActive, Description
    FROM dbo.PaymentMethods
    WHERE Id = @Id;
END;