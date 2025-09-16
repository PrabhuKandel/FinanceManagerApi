create or alter procedure usp_CreateTransactionRecord
@TransactionCategoryId uniqueidentifier,
@PaymentMethodId uniqueidentifier,
@Amount decimal(18,2),
@Description nvarchar(500) = null,
@TransactionDate datetime,
@CreatedByApplicationUserId nvarchar(450),
@UpdatedByApplicationUserId nvarchar(450) 	
as
begin

  SET NOCOUNT ON;
   DECLARE @Id UNIQUEIDENTIFIER = NEWID();  -- generate new GUID

	insert into TransactionRecords ( Id, TransactionCategoryId, PaymentMethodId, Amount, Description, TransactionDate, CreatedByApplicationUserId, UpdatedByApplicationUserId,CreatedAt,UpdatedAt)
	values (@Id, @TransactionCategoryId, @PaymentMethodId, @Amount, @Description, @TransactionDate, @CreatedByApplicationUserId, @UpdatedByApplicationUserId, SYSUTCDATETIME(), SYSUTCDATETIME());

	--return the newly created row

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
  FROM [FinanceManagerDb].[dbo].[TransactionRecords] as tr
  join TransactionCategories as tc
  on tr.TransactionCategoryId = tc.Id
  join PaymentMethods as pm
  on tr.PaymentMethodId = pm.Id
  join AspNetUsers as cb
  on tr.CreatedByApplicationUserId = cb.Id
  join AspNetUsers as ub
  on tr.CreatedByApplicationUserId = ub.Id
  where tr.Id = @Id
end