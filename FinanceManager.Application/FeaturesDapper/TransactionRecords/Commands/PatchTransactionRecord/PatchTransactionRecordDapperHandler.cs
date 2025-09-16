using System.Data;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using FinanceManager.Application.Services;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.PatchTransactionRecord
{
    public class PatchTransactionRecordDapperHandler(IDbConnection connection, IUserContext userContext) : IRequestHandler<PatchTransactionRecordDapperCommand, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(PatchTransactionRecordDapperCommand request, CancellationToken cancellationToken)
        {

            var transactionRecord = await connection.QuerySingleOrDefaultAsync<TransactionRecordDapperResult>("usp_PatchTransactionRecord",
                new
                {
                    request.Id,
                    request.TransactionCategoryId,
                    request.PaymentMethodId,
                    request.Amount,
                    request.Description,
                    request.TransactionDate,
                    UpdatedByApplicationUserId = userContext.UserId

                }
                , commandType: CommandType.StoredProcedure);

            return new OperationResult<TransactionRecordResponseDto>
            {
                Message = "Transaction record patched",
                Data = transactionRecord?.ToResponseDtoFromDapper()
            };
        }
    }
}
