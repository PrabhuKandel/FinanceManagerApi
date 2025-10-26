using System.Data;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Mapping;
using FinanceManager.Application.Interfaces.Services;
using MediatR;




namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetAllTransactionRecord
{
    public class GetAllTransactionRecordsDapperHandler(IDbConnection connection,IUserContext userContext) : IRequestHandler<GetAllTransactionRecordsDapperQuery, PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {

      public async  Task<PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>> Handle(GetAllTransactionRecordsDapperQuery request, CancellationToken cancellationToken)
        {
            var isAdmin = userContext.IsAdmin();

            var parameters = new DynamicParameters();
            // Pagination
            parameters.Add("@PageNumber", request.PageNumber);
            parameters.Add("@PageSize", request.PageSize);

            // Filters
            parameters.Add("@FromDate", request.FromDate);
            parameters.Add("@ToDate", request.ToDate);
            parameters.Add("@CreatedBy", request.CreatedBy);
            parameters.Add("@UpdatedBy", request.UpdatedBy);
            parameters.Add("@ApprovalStatus", request.ApprovalStatus.HasValue ? (int?)request.ApprovalStatus.Value : null);
            parameters.Add("@Search", request.Search);

            // Sorting
            parameters.Add("@SortBy", request.SortBy);
            parameters.Add("@SortDescending", request.SortDescending);

            // Optionally: non-admin filter
            if (!userContext.IsAdmin())
            {
                parameters.Add("@CurrentUserId", userContext.UserId);
            }
             var rows   = await connection.QueryAsync("usp_GetAllTransactionRecordsV2",parameters, commandType: CommandType.StoredProcedure);

            var totalCount = rows.FirstOrDefault()?.TotalCount ?? 0;

            var result = TransactionRecordMappingProfile.MapTransactionRecordResults(rows,isAdmin );

         


            return new PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>
            {

                Data = result,
                Message = "Transaction records retrieved successfully",
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,



            }; 
        }

       
    }
}
