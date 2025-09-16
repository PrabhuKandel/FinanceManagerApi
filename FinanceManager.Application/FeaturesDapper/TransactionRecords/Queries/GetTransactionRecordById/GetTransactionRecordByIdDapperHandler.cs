using Ardalis.GuardClauses;
using System.Data;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;
using Dapper;
using FinanceManager.Application.Mapping;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetTransactionRecordById
{
    public class GetTransactionRecordByIdDapperHandler(IDbConnection connection) : IRequestHandler<GetTransactionRecordByIdDapperQuery, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(GetTransactionRecordByIdDapperQuery request, CancellationToken cancellationToken)
        {
            var transactionRecord = await connection.QuerySingleOrDefaultAsync<TransactionRecordDapperResult>("usp_GetByIdTransactionRecord", new { request.Id }, commandType: CommandType.StoredProcedure);

            Guard.Against.Null(transactionRecord, nameof(transactionRecord), "Transaction record not found");

            return new OperationResult<TransactionRecordResponseDto>
            {

                Data = transactionRecord.ToResponseDtoFromDapper(),
                Message = "Transaction record retrieved successfully"


            };
        }
    }
}
