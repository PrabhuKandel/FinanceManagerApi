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
            var rows = await connection.QueryAsync("usp_GetAllTransactionRecords", commandType: CommandType.StoredProcedure);

            var result = TransactionRecordDapperMapper.MapTransactionRecordResults(rows);

            return new OperationResult<IEnumerable<TransactionRecordResponseDto>>
            {

                Data = result,
                Message = "Transaction records retrieved successfully"

            }; 
        }

       
    }
}
