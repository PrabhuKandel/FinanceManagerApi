using Ardalis.GuardClauses;
using System.Data;
using FinanceManager.Application.Common;
using MediatR;
using Dapper;
using FinanceManager.Application.Mapping;
using FinanceManager.Application.Features.TransactionRecords.Dtos;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetTransactionRecordById
{
    public class GetTransactionRecordByIdDapperHandler(IDbConnection connection) : IRequestHandler<GetTransactionRecordByIdDapperQuery, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(GetTransactionRecordByIdDapperQuery request, CancellationToken cancellationToken)
        {
            var rows = await connection.QueryAsync("usp_GetByIdTransactionRecord", new { request.Id }, commandType: CommandType.StoredProcedure);

            Guard.Against.Null(rows, nameof(rows), "Transaction record not found");

            var result = TransactionRecordDapperMapper.MapTransactionRecordResults(rows);
            return new OperationResult<TransactionRecordResponseDto>
            {

                Data =result.FirstOrDefault(),
                Message = "Transaction record retrieved successfully"


            };
        }
    }
}
