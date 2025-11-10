using System.Linq.Expressions;
using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.Auth.ForgotPassword.Commands
{
    public class ForgotPasswordHandler(UserManager<ApplicationUser> _userManager, IEmailService _emailService) : IRequestHandler<ForgotPasswordCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
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

            await _emailService.SendEmailAsync(
                to: user.Email,
                subject: "Password Reset Request",
                body: $"Click the link to reset your password: {resetLink}"
            );

            return new OperationResult<string>
            {
                Message = "Password reset link has been sent to your email."
            };
        }
    }
}
