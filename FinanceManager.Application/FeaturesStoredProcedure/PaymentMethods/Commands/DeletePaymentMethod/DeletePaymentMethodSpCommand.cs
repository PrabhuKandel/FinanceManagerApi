using FinanceManager.Application.Common;
using MediatR;


namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.DeletePaymentMethod
{
    public  record  DeletePaymentMethodSpCommand(Guid Id): IRequest<OperationResult<string>>
    {
    }


}
