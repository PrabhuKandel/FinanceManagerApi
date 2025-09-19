using FinanceManager.Application.Interfaces.Services;
using Hangfire;
using MediatR;

namespace FinanceManager.Application.Notifications.RegisterNotification
{
    public class UserRegisterNotificationHandler() : INotificationHandler<UserRegisterNotification>
    {
        public  Task Handle(UserRegisterNotification notification, CancellationToken cancellationToken)
        {
            string to = notification.Email;
            string subject = "Welcome to FinanceManager!";

            string body = $@"
            <h2>Hello {notification.FirstName},</h2>
            <p>Thank you for registering with FinanceManager!</p>
            <p>Here are your login details:</p>
            <ul>
                <li><strong>Email:</strong> {notification.Email}</li>
                <li><strong>Password:</strong> {notification.Password}</li>
            </ul>
            <p>We recommend changing your password after your first login.</p>
            <p>Best regards,<br/>FinanceManager Team</p>";

            // Enqueue email sending with Hangfire
            BackgroundJob.Enqueue<IEmailService>(x =>
                x.SendEmailAsync(to, subject, body));

            return Task.CompletedTask;
        }
    }
}
