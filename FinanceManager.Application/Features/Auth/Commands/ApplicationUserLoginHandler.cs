using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.Auth.Commands
{
    public class ApplicationUserLoginHandler(UserManager<ApplicationUser> _userManager,ITokenGenerator _tokenGenerator) : IRequestHandler<ApplicationUserLoginCommand, OperationResult<ApplicationUserLoginResponseDto>>
    {
        public async Task<OperationResult<ApplicationUserLoginResponseDto>> Handle(ApplicationUserLoginCommand request, CancellationToken cancellationToken)
        {
             var applicationUser = await _userManager.FindByEmailAsync(request.loginUser.Email);
            if (applicationUser == null)
            {
                throw new AuthenticationException("Invalid Email");
            }
            var result = await _userManager.CheckPasswordAsync(applicationUser, request.loginUser.Password);
            if (!result)
            {
                throw new AuthenticationException("Invalid Credentials");
            }

            var accessToken = await _tokenGenerator.GenerateAccessToken(applicationUser);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            applicationUser.RefreshToken = refreshToken;
            applicationUser.RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(applicationUser);


            return new OperationResult<ApplicationUserLoginResponseDto>
            {

                Message = "Login Successfull!!",
                Data = new ApplicationUserLoginResponseDto
                {
                    UserId = applicationUser.Id,
                    Email = applicationUser.Email,
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                }
            };
        }
    }
}
