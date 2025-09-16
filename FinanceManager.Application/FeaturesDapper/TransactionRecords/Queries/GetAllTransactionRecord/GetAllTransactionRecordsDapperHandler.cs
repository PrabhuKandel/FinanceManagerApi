using System.Data;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Mapping;
using MediatR;



namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetAllTransactionRecord
{
    public class GetAllTransactionRecordsDapperHandler(IDbConnection connection) : IRequestHandler<GetAllTransactionRecordsDapperQuery, OperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {

      public async  Task<OperationResult<IEnumerable<TransactionRecordResponseDto>>> Handle(GetAllTransactionRecordsDapperQuery request, CancellationToken cancellationToken)
        {
            var transactionRecords = await connection.QueryAsync<TransactionRecordDapperResult>("usp_GetAllTransactionRecords", commandType: CommandType.StoredProcedure);


            return new OperationResult<IEnumerable<TransactionRecordResponseDto>>
            {

                Data = transactionRecords.ToResponseDtoListFromDapper(),
                Message = "Transaction records retrieved successfully"

            };
        }

       
    }
}
