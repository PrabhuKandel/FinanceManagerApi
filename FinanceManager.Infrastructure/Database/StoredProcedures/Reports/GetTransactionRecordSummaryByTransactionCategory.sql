CREATE OR ALTER PROCEDURE usp_GetTransactionRecordSummaryByTransactionCategory
    @TransactionCategoryId UNIQUEIDENTIFIER = NULL,
    @FromDate DATETIME = NULL,
    @ToDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        tr.TransactionCategoryId,
        tc.Name AS TransactionCategoryName,
        SUM(tr.Amount) AS TotalAmount,
        COUNT(*) AS TransactionRecordCount
    FROM TransactionRecords tr
    JOIN TransactionCategories tc ON tr.TransactionCategoryId = tc.Id
    WHERE (@TransactionCategoryId IS NULL OR tr.TransactionCategoryId = @TransactionCategoryId)
      AND (@FromDate IS NULL OR tr.TransactionDate >= @FromDate)
      AND (@ToDate IS NULL OR tr.TransactionDate <= @ToDate)
    GROUP BY tr.TransactionCategoryId, tc.Name
END
