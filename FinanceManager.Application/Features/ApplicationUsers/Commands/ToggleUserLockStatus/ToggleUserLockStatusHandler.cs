using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.ApplicationUsers.Commands.ToggleUserLockStatus
{
    public class ToggleUserLockStatusHandler (UserManager<ApplicationUser> userManager): IRequestHandler<ToggleUserLockStatusCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(ToggleUserLockStatusCommand request, CancellationToken cancellationToken)
        {
            var user  = await userManager.FindByIdAsync(request.UserId);
            Guard.Against.Null(nameof(user), $"User with Id: {request.UserId} was not found.");

            var isLocked = await userManager.IsLockedOutAsync(user);

            if (isLocked)
                user.LockoutEnd = null;

            else
                user.LockoutEnd = DateTimeOffset.MaxValue;

            var result = await userManager.UpdateAsync(user);

            return new OperationResult<string>
            {
                Message =  isLocked? "User has been unlocked successfully."
                        : "User has been locked successfully."
                    
            };

        }
    }
}
