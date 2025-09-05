using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.Auth.Commands
{
    public class ApplicationUserRegisterHandler : IRequestHandler<ApplicationUserRegisterCommand, OperationResult<string>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ApplicationUserRegisterHandler(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager )
        {
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public async Task<OperationResult<string>> Handle(ApplicationUserRegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.registerUser.Email);
            if (existingUser != null)
            {
                throw new BusinessValidationException("Email is already registered.");
            }

            var applicationUser = new ApplicationUser
            {
                FirstName = request.registerUser.FirstName,
                LastName = request.registerUser.LastName,
                UserName = request.registerUser.Email,
                Email = request.registerUser.Email,
                Address = request.registerUser.Address,

            };

            var result = await userManager.CreateAsync(applicationUser, request.registerUser.Password);

            if (!result.Succeeded)
            {
                if (result.Errors.Any())
                {
                    throw new BusinessValidationException(result.Errors.ToDictionary(e => "Error", e => e.Description));
                }
                throw new Exception("Registration failed due to server error.");
            }

            //assingning role based on user input 
            if (!string.IsNullOrEmpty(request.registerUser.RoleId))
            {
                var role = await roleManager.FindByIdAsync(request.registerUser.RoleId);
                if (role == null)
                    throw new BusinessValidationException("Invalid role selected.");

                await userManager.AddToRoleAsync(applicationUser, role.Name);
            }
            else
            {
                await userManager.AddToRoleAsync(applicationUser, RoleConstants.User);
            }
            return new OperationResult<String>
            {

                Data = null,
                Message = "Registration Successfulll!!"


            };

        }
    }
}
