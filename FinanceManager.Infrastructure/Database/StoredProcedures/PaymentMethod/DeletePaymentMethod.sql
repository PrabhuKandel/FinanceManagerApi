 CREATE OR ALTER PROCEDURE [dbo].[usp_DeletePaymentMethod]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.PaymentMethods
    WHERE Id = @Id;
END;