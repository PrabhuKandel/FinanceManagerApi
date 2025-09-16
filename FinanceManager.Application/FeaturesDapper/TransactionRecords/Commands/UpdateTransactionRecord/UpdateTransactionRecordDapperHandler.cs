using System.Data;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.UpdateTransactionRecord
{
    public class UpdateTransactionRecordDapperHandler(IDbConnection connection, IUserContext userContext) : IRequestHandler<UpdateTransactionRecordDapperCommand, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(UpdateTransactionRecordDapperCommand request, CancellationToken cancellationToken)
        {

            var transactionRecord = await connection.QuerySingleOrDefaultAsync<TransactionRecordDapperResult>("usp_UpdateTransactionRecord",
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
                Message = "Transaction record updated",
                Data = transactionRecord?.ToResponseDtoFromDapper()
            };

        }

    }
}
