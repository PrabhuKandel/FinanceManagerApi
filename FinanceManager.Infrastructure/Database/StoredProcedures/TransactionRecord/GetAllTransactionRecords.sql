create or alter procedure usp_GetAllTransactionRecords
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @FromDate DATETIME = NULL,
    @ToDate DATETIME = NULL,
    @CreatedBy NVARCHAR(450) = NULL,
    @UpdatedBy NVARCHAR(450) = NULL,
    @ApprovalStatus INT = NULL,
    @Search NVARCHAR(500) = NULL,
    @SortBy NVARCHAR(100) = 'TransactionDate',
    @SortDescending BIT = 0,
    @CurrentUserId NVARCHAR(450) = NULL
as
begin

  SET NOCOUNT ON;
;WITH FilteredRecords AS
    (
        SELECT 
            tr.Id AS TransactionRecordId,
            tr.Amount AS TransactionAmount,
            tr.Description,
            tr.TransactionDate,
            tr.ApprovalStatus,
            tr.CreatedAt,
            tr.UpdatedAt,
            tc.Id AS TransactionCategoryId,
            tc.Name AS TransactionCategoryName,
            cb.Id AS CreatedByUserId,
            cb.Email AS CreatedByEmail,
            ub.Id AS UpdatedByUserId,
            ub.Email AS UpdatedByEmail,
            ab.Id AS ActionedByUserId,
            ab.Email AS ActionedByEmail,
            ROW_NUMBER() OVER (
                ORDER BY
                    CASE WHEN LOWER(@SortBy) = 'amount' THEN tr.Amount END ASC,
                    CASE WHEN LOWER(@SortBy) = 'category' THEN tc.Name END ASC,
                    CASE WHEN LOWER(@SortBy) = 'createdby' THEN cb.Email END ASC,
                    CASE WHEN LOWER(@SortBy) = 'updatedby' THEN ub.Email END ASC,
                    CASE WHEN LOWER(@SortBy) NOT IN ('amount','category','createdby','updatedby') THEN tr.TransactionDate END ASC
            ) AS RowNum,
             COUNT(*) OVER() AS TotalCount
        FROM TransactionRecords tr
        JOIN TransactionCategories tc ON tr.TransactionCategoryId = tc.Id
        JOIN AspNetUsers cb ON tr.CreatedByApplicationUserId = cb.Id
        LEFT JOIN AspNetUsers ub ON tr.UpdatedByApplicationUserId = ub.Id
        LEFT JOIN AspNetUsers ab ON tr.ActionedByApplicationUserId = ab.Id
        WHERE
            (@FromDate IS NULL OR tr.TransactionDate >= @FromDate)
            AND (@ToDate IS NULL OR tr.TransactionDate <= @ToDate)
            AND (@CreatedBy IS NULL OR tr.CreatedByApplicationUserId = @CreatedBy)
            AND (@UpdatedBy IS NULL OR tr.UpdatedByApplicationUserId = @UpdatedBy)
            AND (@ApprovalStatus IS NULL OR tr.ApprovalStatus = @ApprovalStatus)
            AND (@CurrentUserId IS NULL OR tr.CreatedByApplicationUserId = @CurrentUserId)
            AND (
                @Search IS NULL OR
                LOWER(tr.Description) LIKE '%' + LOWER(@Search) + '%' OR
                LOWER(tc.Name) LIKE '%' + LOWER(@Search) + '%' OR
				LOWER(cb.Email) LIKE '%' + LOWER(@Search) + '%' OR
				LOWER(ub.Email) LIKE '%' + LOWER(@Search) + '%' 

            )
    )
    SELECT *
    FROM FilteredRecords
    WHERE RowNum BETWEEN ((@PageNumber - 1) * @PageSize + 1)
                    AND (@PageNumber * @PageSize)
    ORDER BY RowNum
 
end