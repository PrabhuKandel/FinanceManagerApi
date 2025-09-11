CREATE OR ALTER PROCEDURE usp_UpdateTransactionCategory
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @Description NVARCHAR(500) = NULL,
    @Type INT  -- 0 = Income, 1 = Expense
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate Type
    IF (@Type NOT IN (0,1))
    BEGIN
        RAISERROR('Invalid Type. Allowed values: 0=Income, 1=Expense.', 16, 1);
        RETURN;
    END

    -- Update the row
    UPDATE TransactionCategories
    SET Name = @Name,
        Description = @Description,
        Type = @Type
    WHERE Id = @Id;

    -- Return the updated row
    SELECT Id, Name, Description, Type
    FROM TransactionCategories
    WHERE Id = @Id;
END
