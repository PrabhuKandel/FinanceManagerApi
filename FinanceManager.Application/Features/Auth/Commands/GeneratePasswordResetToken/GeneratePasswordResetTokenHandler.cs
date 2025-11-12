using System.Linq.Expressions;
using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.Auth.Commands.GeneratePasswordResetToken
{
    public class GeneratePasswordResetTokenHandler(UserManager<ApplicationUser> _userManager, IEmailService _emailService) : IRequestHandler<GeneratePasswordResetToken, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(GeneratePasswordResetToken request, CancellationToken cancellationToken)
        {
            var user  = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new OperationResult<string>
                {
                    Message = "Password reset link has been sent to your email."
                };
            }

            //generate password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user!);
            Console.WriteLine(token);

            // Build reset link for frontend
            var resetLink = $"{request.ClientURI}?email={user.Email}&token={Uri.EscapeDataString(token)}";

            var message = $@"
        <p>Hello {user.FirstName ?? "User"},</p>
        <p>You requested to reset your password. Click the link below to continue:</p>
        <p>
            <a href='{resetLink}' target='_blank' style='color: #1a73e8; text-decoration: none;'>
                Reset your password
            </a>
        </p>
        <p>If you did not request this, please ignore this email.</p>
        <br />
        <p>Thanks,<br />Finance Manager</p>
    ";

            await _emailService.SendEmailAsync(
                to: user.Email,
                subject: "Password Reset Request",
                body: message
            );

            return new OperationResult<string>
            {
                Message = "Password reset link has been sent to your email."
            };
        }
    }
}
