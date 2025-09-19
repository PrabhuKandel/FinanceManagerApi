

using MediatR;

namespace FinanceManager.Application.Notifications.RegisterNotification
{
    public record UserRegisterNotification(string Email, string FirstName, string Password):INotification
    {
    }
}
