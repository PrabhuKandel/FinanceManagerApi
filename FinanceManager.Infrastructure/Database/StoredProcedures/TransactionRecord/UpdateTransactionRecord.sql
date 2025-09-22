CREATE OR ALTER PROCEDURE usp_UpdateTransactionRecord
    @Id UNIQUEIDENTIFIER,
    @TransactionCategoryId UNIQUEIDENTIFIER ,
    @Amount DECIMAL(18,2) ,
    @Description NVARCHAR(500) = NULL,
    @TransactionDate DATETIME ,
    @UpdatedByApplicationUserId UNIQUEIDENTIFIER,
    @Payments TransactionPaymentType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
    UPDATE TransactionRecords
    SET 
        TransactionCategoryId = @TransactionCategoryId,
        Amount = @Amount,
        Description = @Description,
        TransactionDate = @TransactionDate,
        UpdatedByApplicationUserId = @UpdatedByApplicationUserId,
        UpdatedAt = SYSUTCDATETIME()
         WHERE Id = @Id;

      DELETE FROM TransactionPayments WHERE TransactionRecordId = @Id;


    INSERT INTO TransactionPayments (Id,TransactionRecordId, PaymentMethodId, Amount)
    SELECT NewId(), @Id, PaymentMethodId, Amount
    FROM @Payments;

      COMMIT TRANSACTION;
    -- Return the updated record with joins
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
END
