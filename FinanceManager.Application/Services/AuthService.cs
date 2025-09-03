using System.Data;
using System.Security.Authentication;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Services
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public readonly ITokenGenerator _tokenGenerator;
        private readonly IValidator<ApplicationUserLoginDto> _loginValidator;
        private readonly IValidator<ApplicationUserRegisterDto> _registerValidator;
        public AuthService(
            UserManager<ApplicationUser> userManager, 
            ITokenGenerator tokenGenerator, 
            RoleManager<IdentityRole> roleManager,
            IValidator<ApplicationUserLoginDto> loginValidator,
            IValidator<ApplicationUserRegisterDto> registerValidator


            )
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _roleManager = roleManager;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        public async Task<ServiceResponse<string>> RegisterAsync(ApplicationUserRegisterDto registerUser)
        {
            var validationResult = _registerValidator.Validate(registerUser);

            if (!validationResult.IsValid)
            {

                throw new CustomValidationException(validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage));
            }
            var existingUser = await _userManager.FindByEmailAsync(registerUser.Email);
            if (existingUser != null)
            {
                throw new CustomValidationException( "Email is already registered." );
            }

            var applicationUser = new ApplicationUser
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                UserName = registerUser.Email,
                Email = registerUser.Email,
                Address = registerUser.Address,

            };

            var result = await _userManager.CreateAsync(applicationUser, registerUser.Password);

            if (!result.Succeeded)
            {
                if (result.Errors.Any())
                {
                    throw new CustomValidationException(result.Errors.ToDictionary(e => "Error", e => e.Description));
                }
                throw new InvalidOperationException("Registration failed due to server error.");
            }

           //assingning role based on user input 
            if (!string.IsNullOrEmpty(registerUser.RoleId))
            {
                var role = await _roleManager.FindByIdAsync(registerUser.RoleId);
                if (role == null)
                    throw new CustomValidationException("Invalid role selected.");

                await _userManager.AddToRoleAsync(applicationUser, role.Name);
            }
            else
            {
                await _userManager.AddToRoleAsync(applicationUser, RoleConstants.User);
            }

                return new ServiceResponse<String>
                {

                    Data = null,
                    Message = "Registration Successfulll!!"


                };
        }

        public async Task<ServiceResponse<ApplicationUserLoginResponseDto>> LoginAsync(ApplicationUserLoginDto loginUser)
        {
            var validationResult = _loginValidator.Validate(loginUser);

            if (!validationResult.IsValid)
            {

                throw new CustomValidationException(validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage));
            }


            var applicationUser = await _userManager.FindByEmailAsync(loginUser.Email);
            if (applicationUser == null)
            {
                throw new AuthenticationException("Invalid Email");
            }
            var result = await _userManager.CheckPasswordAsync(applicationUser, loginUser.Password);
            if (!result)
            {
                throw new AuthenticationException("Invalid Credentials");
            }

            var accessToken = await _tokenGenerator.GenerateAccessToken(applicationUser);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            applicationUser.RefreshToken = refreshToken;
            applicationUser.RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(applicationUser);



            return new ServiceResponse<ApplicationUserLoginResponseDto>
            {       

                Message = "Login Successfull!!",
                Data = new ApplicationUserLoginResponseDto
                {
                    UserId = applicationUser.Id,
                    Email = applicationUser.Email,
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    AccessToken  = accessToken,
                    RefreshToken = refreshToken,
                }
            };

        }

        public async Task<ServiceResponse<TokenResponseDto>> RefreshTokenAsync(string refreshToken)
        {

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new AuthenticationException("Refresh token is missing ");
            }
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpiresAtUtc < DateTime.UtcNow)
            {
                throw new AuthenticationException("Invalid or expired refresh token ");
              
            }

            var accessToken = await _tokenGenerator.GenerateAccessToken(user);
            var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new ServiceResponse<TokenResponseDto>
            {

                Message = "Token Refreshed!!",
                Data = new TokenResponseDto
                {
                  
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                }
            };

        }

        public async Task<ServiceResponse<string>> LogoutAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {

                throw new  NotFoundException("User doesn't exist");

            }
            user.RefreshToken = null;
            user.RefreshTokenExpiresAtUtc = null;
            await _userManager.UpdateAsync(user);

            return new ServiceResponse<string>
            {
                Message = "Logout Successfull!!",
                Data = null
            };

         }


       

    }


}
