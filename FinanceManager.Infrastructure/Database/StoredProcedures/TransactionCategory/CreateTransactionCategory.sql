

    CREATE  or ALTER PROCEDURE usp_CreateTransactionCategory
	
	@Name nvarchar(100),
	@Description nvarchar(500) = null,  
	@Type int  --0= Income, 1 = Expense
	AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Id UNIQUEIDENTIFIER = NEWID();  -- generate new GUID


    IF (@Type NOT IN (0,1))
    BEGIN
        RAISERROR('Invalid Type. Allowed values: 0=Income, 1=Expense.', 16, 1);
        RETURN;
    END

    INSERT INTO TransactionCategories ( Id, Name, Description, Type)
    VALUES (@Id , @Name, @Description, @Type);


        -- Return the newly created row
    SELECT Id, Name, Description, Type
    FROM TransactionCategories
    WHERE Id = @Id;

END
