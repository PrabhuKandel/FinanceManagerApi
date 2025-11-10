using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.ApplicationUsers.Commands.ToggleUserLockStatus
{
    public class ToggleUserLockStatusHandler (UserManager<ApplicationUser> userManager): IRequestHandler<ToggleUserLockStatusCommand, OperationResult<ToggleUserLockStatusResponseDto>>
    {
        public async Task<OperationResult<ToggleUserLockStatusResponseDto>> Handle(ToggleUserLockStatusCommand request, CancellationToken cancellationToken)
        {
            var user  = await userManager.FindByIdAsync(request.UserId);
            Guard.Against.Null(nameof(user), $"User with Id: {request.UserId} was not found.");

            var isLocked = await userManager.IsLockedOutAsync(user);

            if (isLocked)
            {
                user.LockoutEnd = null;
                user.IsManuallyLocked = false;
                user.LockReason = null;
            }


            else
            {

                user.LockoutEnd = DateTimeOffset.MaxValue;
                user.IsManuallyLocked = true;
                user.LockReason =  "Manually locked by admin";
            }

            var result = await userManager.UpdateAsync(user);

            var responseDto = new ToggleUserLockStatusResponseDto
            {
                UserId = user.Id,
                IsLocked = !isLocked,
                IsManuallyLocked = user.IsManuallyLocked,
                LockReason = user.LockReason
            };

            return new OperationResult<ToggleUserLockStatusResponseDto>
            {
                Message = isLocked ? "User has been unlocked successfully."
                        : "User has been locked successfully.",
                  Data = responseDto
            };

        }
    }
}
