using Ardalis.GuardClauses;
using Dapper;
using FinanceManager.Application.Common;
using MediatR;
using System.Data;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.DeleteTransactionRecord
{
    public class DeleteTransactionRecordDapperHandler(IDbConnection connection) : IRequestHandler<DeleteTransactionRecordDapperCommand, OperationResult<string>>
    {

        public async Task<OperationResult<string>> Handle(DeleteTransactionRecordDapperCommand request, CancellationToken cancellationToken)
        {
            var result = await connection.ExecuteAsync("usp_DeleteTransactionRecord", new { request.Id }, commandType: CommandType.StoredProcedure);
            //If no row matches (Id not found) → returns 0.
            Guard.Against.Zero(result, nameof(result), "Transaction record not found");

            return new OperationResult<string>
            {

                Message = "Transaction record deleted",

            };
        }
    }
}
