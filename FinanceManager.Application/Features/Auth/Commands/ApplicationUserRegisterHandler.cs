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

            var applicationUser = new ApplicationUser
            {
                FirstName = request.RegisterUser.FirstName,
                LastName = request.RegisterUser.LastName,
                UserName = request.RegisterUser.Email,
                Email = request.RegisterUser.Email,
                Address = request.RegisterUser.Address,

            };

            var result = await userManager.CreateAsync(applicationUser, request.RegisterUser.Password);

            if (!result.Succeeded)
            {
                if (result.Errors.Any())
                {
                    var errors = new Dictionary<string, string[]>
                        {
                            { "Error", result.Errors.Select(e => e.Description).ToArray() }
                        };

                    throw new BusinessValidationException(errors);
                }
                throw new Exception("Registration failed due to server error.");
            }

            //assingning role based on user input 
            if (!string.IsNullOrEmpty(request.RegisterUser.RoleId))
            {
                var role = await roleManager.FindByIdAsync(request.RegisterUser.RoleId);
                if (role == null)
                    throw new BusinessValidationException("Invalid role selected.");

                await userManager.AddToRoleAsync(applicationUser, role.Name);
            }
            else
            {
                await userManager.AddToRoleAsync(applicationUser, RoleConstants.User);
            }

            // Publish notification instead of sending email directly
            await _mediator.Publish(new UserRegisterNotification( applicationUser.Email,applicationUser.FirstName, request.RegisterUser.Password));

            return new OperationResult<String>
            {

                Data = null,
                Message = "Registration Successfulll!!"


            };

        }
    }
}
