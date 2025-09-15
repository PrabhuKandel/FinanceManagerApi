using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionRecords.Commands.CreateTransactionRecord
{
    public class CreateTransactionRecordSpHandler(IApplicationDbContext context, IUserContext userContext) : IRequestHandler<CreateTransactionRecordSpCommand, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(CreateTransactionRecordSpCommand request, CancellationToken cancellationToken)
        {

            var transactionRecord = context.TransactionRecordSpResults.
                 FromSqlInterpolated(
                $"EXEC dbo.usp_CreateTransactionRecord @TransactionCategoryId = {request.TransactionCategoryId}, @PaymentMethodId ={request.PaymentMethodId},@Amount={request.Amount},@Description={request.Description}, @TransactionDate={request.TransactionDate}, @CreatedByApplicationUserId={userContext.UserId}, @UpdatedByApplicationUserId={userContext.UserId}"
                )
                 .AsNoTracking()
                 .AsEnumerable()
                 .FirstOrDefault();


            return new OperationResult<TransactionRecordResponseDto>
            {

                Message = "New transaction category added",
                Data = transactionRecord?.ToResponseDtoFromSp(userContext.IsAdmin())
            };

        }
    }
}
