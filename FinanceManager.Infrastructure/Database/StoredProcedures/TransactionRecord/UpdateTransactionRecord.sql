CREATE OR ALTER PROCEDURE usp_UpdateTransactionRecord
    @Id UNIQUEIDENTIFIER,
    @TransactionCategoryId UNIQUEIDENTIFIER ,
    @PaymentMethodId UNIQUEIDENTIFIER ,
    @Amount DECIMAL(18,2) ,
    @Description NVARCHAR(500) = NULL,
    @TransactionDate DATETIME ,
    @UpdatedByApplicationUserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE TransactionRecords
    SET 
        TransactionCategoryId = @TransactionCategoryId,
        PaymentMethodId = @PaymentMethodId,
        Amount = @Amount,
        Description = @Description,
        TransactionDate = @TransactionDate,
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
