using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.Auth.Commands
{
    public class ApplicationUserLoginHandler : IRequestHandler<ApplicationUserLoginCommand, OperationResult<ApplicationUserLoginResponseDto>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenGenerator tokenGenerator;

        public ApplicationUserLoginHandler(UserManager<ApplicationUser> _userManager,ITokenGenerator _tokenGenerator)
        {
            userManager = _userManager;
            tokenGenerator = _tokenGenerator;
        }

        public async Task<OperationResult<ApplicationUserLoginResponseDto>> Handle(ApplicationUserLoginCommand request, CancellationToken cancellationToken)
        {
             var applicationUser = await userManager.FindByEmailAsync(request.loginUser.Email);
            if (applicationUser == null)
            {
                throw new AuthenticationException("Invalid email");
            }
            var result = await userManager.CheckPasswordAsync(applicationUser, request.loginUser.Password);
            if (!result)
            {
                throw new AuthenticationException("Invalid Credentials");
            }

            var accessToken = await tokenGenerator.GenerateAccessToken(applicationUser);
            var refreshToken = tokenGenerator.GenerateRefreshToken();

            applicationUser.RefreshToken = refreshToken;
            applicationUser.RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7);
            await userManager.UpdateAsync(applicationUser);


            return new OperationResult<ApplicationUserLoginResponseDto>
            {

                Message = "Login Successfull!!",
                Data = new ApplicationUserLoginResponseDto
                {
                    UserId = applicationUser.Id,
                    Email = applicationUser.Email??"",
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                }
            };
        }
    }
}
