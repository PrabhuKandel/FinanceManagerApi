using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordHandler(IUserContext _userContext, UserManager<ApplicationUser> _userManager) : IRequestHandler<ChangePasswordCommand, OperationResult<string>>
    {
        public  async Task<OperationResult<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            string UserId = _userContext.UserId;

            var user = await _userManager.FindByIdAsync(UserId);

            var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new Dictionary<string, string[]>
                        {
                            { "Error", result.Errors.Select(e => e.Description).ToArray() }
                        };
                throw new BusinessValidationException(errors);
            }

            return new OperationResult<string>
            {

                Data = null,
                Message = "Password changed successfully!!"


            };

        }
    }
}
