create or alter procedure usp_GetAllTransactionRecords

as
begin

  SET NOCOUNT ON;
  SELECT
        tr.Id AS TransactionRecordId,
        tr.Amount AS TransactionAmount,
        tr.Description,
        tr.TransactionDate,
        tr.CreatedAt,
        tr.UpdatedAt,

        tc.Id AS TransactionCategoryId,
        tc.Name AS TransactionCategoryName,

        tp.PaymentMethodId,
        pm.Name AS PaymentMethodName,
        tp.Amount AS PaymentAmount,

        cb.Id AS CreatedByUserId,
        cb.Email AS CreatedByEmail,

        ub.Id AS UpdatedByUserId,
        ub.Email AS UpdatedByEmail
    FROM TransactionRecords tr
    JOIN TransactionCategories tc ON tr.TransactionCategoryId = tc.Id
     JOIN TransactionPayments tp ON tr.Id = tp.TransactionRecordId
     JOIN PaymentMethods pm ON tp.PaymentMethodId = pm.Id
     JOIN AspNetUsers cb ON tr.CreatedByApplicationUserId = cb.Id
    LEFT JOIN AspNetUsers ub ON tr.UpdatedByApplicationUserId = ub.Id
end