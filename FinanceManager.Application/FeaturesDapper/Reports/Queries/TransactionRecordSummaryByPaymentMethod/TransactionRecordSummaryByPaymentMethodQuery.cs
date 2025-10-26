using FinanceManager.Application.Common;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByPaymentMethod
{
    public record TransactionRecordSummaryByPaymentMethodQuery(
           Guid? PaymentMethodId
        , DateTime? FromDate
        , DateTime? ToDate
        ) : IRequest<OperationResult<IEnumerable<TransactionRecordSummaryByPaymentMethodDto>>>
    {
    }
}
