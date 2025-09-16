CREATE OR ALTER PROCEDURE usp_PatchTransactionRecord
    @Id UNIQUEIDENTIFIER,
    @TransactionCategoryId UNIQUEIDENTIFIER = NULL,
    @PaymentMethodId UNIQUEIDENTIFIER = NULL,
    @Amount DECIMAL(18,2) = NULL,
    @Description NVARCHAR(500) = NULL,
    @TransactionDate DATETIME = NULL,
    @UpdatedByApplicationUserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE TransactionRecords
    SET 
        TransactionCategoryId = COALESCE(@TransactionCategoryId, TransactionCategoryId),
        PaymentMethodId = COALESCE(@PaymentMethodId, PaymentMethodId),
        Amount = COALESCE(@Amount, Amount),
        Description = COALESCE(@Description, Description),
        TransactionDate = COALESCE(@TransactionDate, TransactionDate),
        UpdatedByApplicationUserId = @UpdatedByApplicationUserId,
        UpdatedAt = SYSUTCDATETIME()
         WHERE Id = @Id;

    -- Return the updated record with joins
 	SELECT	
	tr.Id as TransactionRecordId,
	tr.Amount,
	tr.Description,
	tr.TransactionDate,
	tc.Id as TransactionCategoryId,
	tc.Name as TransactionCategoryName,
	pm.Id as PaymentMethodId,
	pm.Name as PaymentMethodName,
	cb.Id as CreatedByUserId,
	cb.FirstName as CreatedByFirstName,
	ub.Id as UpdatedByUserId,
	ub.FirstName as UpdatedByFirstName,
	tr.CreatedAt,
	tr.UpdatedAt
  from TransactionRecords as tr
  join TransactionCategories as tc
  on tr.TransactionCategoryId = tc.Id
  join PaymentMethods as pm
  on tr.PaymentMethodId = pm.Id
  join AspNetUsers as cb
  on tr.CreatedByApplicationUserId = cb.Id
  join AspNetUsers as ub
  on tr.UpdatedByApplicationUserId = ub.Id
    WHERE tr.Id = @Id;
END
