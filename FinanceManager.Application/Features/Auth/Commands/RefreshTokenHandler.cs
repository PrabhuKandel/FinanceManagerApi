using System.Security.Authentication;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Auth.Commands
{
    public class RefreshTokenHandler(UserManager<ApplicationUser> _userManager, ITokenGenerator _tokenGenerator) : IRequestHandler<RefreshTokenCommand, OperationResult<TokenResponseDto>>
    {
        public async Task<OperationResult<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.refreshToken))
            {
                throw new AuthenticationException("Refresh token is missing ");
            }
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == request.refreshToken);

            if (user == null || user.RefreshTokenExpiresAtUtc < DateTime.UtcNow)
            {
                throw new AuthenticationException("Invalid or expired refresh token ");

            }

            var accessToken = await _tokenGenerator.GenerateAccessToken(user);
            var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new OperationResult<TokenResponseDto>
            {

                Message = "Token Refreshed!!",
                Data = new TokenResponseDto
                {

                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken,
                }
            };

        }
    }
}
