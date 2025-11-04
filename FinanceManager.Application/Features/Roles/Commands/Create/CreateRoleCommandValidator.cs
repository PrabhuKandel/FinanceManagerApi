
using FinanceManager.Application.Interfaces.Services;
using FluentValidation;

namespace FinanceManager.Application.Features.Roles.Commands.Create
{
    public class CreateRoleCommandValidator:AbstractValidator<CreateRoleCommand>
    {
        private readonly HashSet<string> validPermissions;
        public CreateRoleCommandValidator(IPermissionService permissionService)
        {
             validPermissions = permissionService.GetAllPermissions().ToHashSet();

            RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Role name is required")
            .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters");

            RuleFor(x => x.Permissions)
                .NotNull().WithMessage("Permissions cannot be null")
                .Must(p => p.Any()).WithMessage("At least one permission must be assigned");

            RuleForEach(x => x.Permissions)
         .Must(IsValidPermission)
        .WithMessage("Permission '{PropertyValue}' is invalid.");

        }

        private bool IsValidPermission(string permission)
        {
            return validPermissions.Contains(permission);
        }
    }
}
