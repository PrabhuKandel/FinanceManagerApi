using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Notifications.RegisterNotification;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.Auth.Commands
{
    public class ApplicationUserRegisterHandler : IRequestHandler<ApplicationUserRegisterCommand, OperationResult<string>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMediator _mediator;

        public ApplicationUserRegisterHandler(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager, IMediator mediator)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            _mediator = mediator;
        }

        public async Task<OperationResult<string>> Handle(ApplicationUserRegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.RegisterUser.Email);
            if (existingUser != null)
            {
                throw new BusinessValidationException("Email is already registered.");
            }
            var tempPassword = Guid.NewGuid().ToString("N").Substring(0, 12) + "aA!1";

            var applicationUser = new ApplicationUser
            {
                FirstName = request.RegisterUser.FirstName,
                LastName = request.RegisterUser.LastName,
                UserName = request.RegisterUser.Email,
                Email = request.RegisterUser.Email,
                Address = request.RegisterUser.Address,

            };

            var result = await userManager.CreateAsync(applicationUser, tempPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Any()
                    ? result.Errors.ToDictionary(_ => "Error", e => new[] { e.Description })
                    : new Dictionary<string, string[]> { { "Error", new[] { "Registration failed due to unknown error." } } };

                throw new BusinessValidationException(errors);
            }

            // Assign role — default to "User" if not provided
            var roleId = string.IsNullOrEmpty(request.RegisterUser.RoleId)
                ? null
                : request.RegisterUser.RoleId;

            var role = roleId != null
                ? await roleManager.FindByIdAsync(roleId)
                : await roleManager.FindByNameAsync(RoleConstants.User);

            if (role == null)
                throw new BusinessValidationException("Invalid role selected.");

            await userManager.AddToRoleAsync(applicationUser, role.Name!);

            // Publish notification instead of sending email directly
            await _mediator.Publish(new UserRegisterNotification( applicationUser.Email,applicationUser.FirstName, tempPassword));

            return new OperationResult<String>
            {

                Data = null,
                Message = "Registration Successfulll!!" 


            };

        }
    }
}
