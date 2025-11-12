using FinanceManager.Application.Common;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordHandler(UserManager<ApplicationUser> _userManager): IRequestHandler<ResetPasswordCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            //for security generic message
            if (user == null)
            {

                return new OperationResult<string>
                {

                    Message = "Password has been reset successfully."
                };
            }


            //  Reset password using token
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (result.Succeeded)
                await _userManager.UpdateAsync(user);

            return new OperationResult<string>
            {
                Message = result.Succeeded ? "Password has been reset successfully."
                        : "Password reset failed. Please ensure your token is valid and try again.",
            };
        }


    }
}
