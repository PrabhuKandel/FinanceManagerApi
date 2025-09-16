create or alter procedure usp_GetAllTransactionRecords

as
begin

  SET NOCOUNT ON;
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
  on tr.CreatedByApplicationUserId = ub.Id
end