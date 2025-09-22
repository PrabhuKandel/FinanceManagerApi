
-- First, make sure the TVP type exists
IF TYPE_ID(N'TransactionPaymentType') IS NULL
BEGIN
    CREATE TYPE TransactionPaymentType AS TABLE
    (
        PaymentMethodId UNIQUEIDENTIFIER,
        Amount DECIMAL(18,2)
    )
END
GO

create or alter procedure usp_CreateTransactionRecord
@TransactionCategoryId uniqueidentifier,
@Amount decimal(18,2),
@Description nvarchar(500) = null,
@TransactionDate datetime,
@CreatedByApplicationUserId nvarchar(450),
@Payments TransactionPaymentType READONLY  -- table-valued parameter
as
begin

  SET NOCOUNT ON;
   DECLARE @Id UNIQUEIDENTIFIER = NEWID();  -- generate new GUID
   BEGIN TRY
     BEGIN TRANSACTION;

       -- Insert main transaction record
	insert into TransactionRecords ( Id, TransactionCategoryId,  Amount, Description, TransactionDate, CreatedByApplicationUserId ,CreatedAt,UpdatedAt)
	values (@Id, @TransactionCategoryId, @Amount, @Description, @TransactionDate, @CreatedByApplicationUserId, SYSUTCDATETIME(), SYSUTCDATETIME());


	    -- Insert payments
    INSERT INTO TransactionPayments (Id,TransactionRecordId, PaymentMethodId, Amount)
    SELECT NEWID(), @Id, PaymentMethodId, Amount
    FROM @Payments;

       COMMIT TRANSACTION;
	--return the newly created row
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
    WHERE tr.Id = @Id;
        END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW; -- propagates the error to the caller (e.g., Dapper)
    END CATCH
end