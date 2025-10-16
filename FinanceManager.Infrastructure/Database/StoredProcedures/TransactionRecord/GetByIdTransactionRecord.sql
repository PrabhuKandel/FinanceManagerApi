create or alter procedure usp_GetByIdTransactionRecord
 @Id uniqueidentifier
as
begin

  SET NOCOUNT ON;
  SELECT
        tr.Id AS TransactionRecordId,
        tr.Amount AS TransactionAmount,
        tr.Description,
        tr.ApprovalStatus,
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
        ub.Email AS UpdatedByEmail,

        ab.Id AS ActionedByUserId,
        ab.Email AS ActionedByEmail
    FROM TransactionRecords tr
    JOIN TransactionCategories tc ON tr.TransactionCategoryId = tc.Id
     JOIN TransactionPayments tp ON tr.Id = tp.TransactionRecordId
     JOIN PaymentMethods pm ON tp.PaymentMethodId = pm.Id
     JOIN AspNetUsers cb ON tr.CreatedByApplicationUserId = cb.Id
    LEFT JOIN AspNetUsers ub ON tr.UpdatedByApplicationUserId = ub.Id
    LEFT JOIN AspNetUsers ab ON tr.ActionedByApplicationUserId = ab.Id
    
   where tr.Id = @Id;
end