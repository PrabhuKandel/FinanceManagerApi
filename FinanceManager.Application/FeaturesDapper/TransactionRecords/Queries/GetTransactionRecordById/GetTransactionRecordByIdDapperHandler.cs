using Ardalis.GuardClauses;
using System.Data;
using FinanceManager.Application.Common;
using MediatR;
using Dapper;
using FinanceManager.Application.Mapping;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.Interfaces.Services;


namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetTransactionRecordById
{
    public class GetTransactionRecordByIdDapperHandler(IDbConnection connection,IUserContext userContext) : IRequestHandler<GetTransactionRecordByIdDapperQuery, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(GetTransactionRecordByIdDapperQuery request, CancellationToken cancellationToken)
        {
            var isAdmin = userContext.IsAdmin();
            var rows = await connection.QueryAsync("usp_GetByIdTransactionRecord", new { request.Id }, commandType: CommandType.StoredProcedure);

            Guard.Against.Null(rows, nameof(rows), "Transaction record not found");

            var result = TransactionRecordDapperMapper.MapTransactionRecordResults(rows, isAdmin);
            return new OperationResult<TransactionRecordResponseDto>
            {

                Data =result.FirstOrDefault(),
                Message = "Transaction record retrieved successfully"


            };
        }
    }
}
