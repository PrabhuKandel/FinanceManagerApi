CREATE OR ALTER PROCEDURE usp_DeleteTransactionCategory
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Delete the row
    DELETE FROM TransactionCategories
    WHERE Id = @Id;

END
