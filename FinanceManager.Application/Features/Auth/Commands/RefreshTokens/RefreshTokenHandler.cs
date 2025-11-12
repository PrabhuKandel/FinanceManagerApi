using System.Security.Authentication;
using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Auth.Dtos;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Auth.Commands.RefreshTokens
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, OperationResult<TokenResponseDto>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenGenerator tokenGenerator;
        private readonly IApplicationDbContext context;
        private readonly IUserContext userContext;
        public RefreshTokenHandler(UserManager<ApplicationUser> _userManager, ITokenGenerator _tokenGenerator, IApplicationDbContext _context)
        {
            userManager = _userManager;
            tokenGenerator = _tokenGenerator;
            context = _context;


        }

        public async Task<OperationResult<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
 
            Guard.Against.NullOrEmpty(request.RefreshToken, nameof(request.RefreshToken), "Refresh token is missing");

            var tokenFromDb = await context.RefreshTokens.Include(t=>t.ApplicationUser)
                            .FirstOrDefaultAsync(t => t.Token == request.RefreshToken && t.RevokedAt == null, cancellationToken);

                if (tokenFromDb == null || tokenFromDb.ExpiresAt < DateTime.UtcNow)
            {
                throw new AuthenticationException("Invalid or expired refresh token");
            }
            var user = tokenFromDb.ApplicationUser;

            //if token is present 
            tokenFromDb.RevokedAt = DateTime.UtcNow;
            tokenFromDb.RevocationReason = "Token rotation";

            var accessToken = await tokenGenerator.GenerateAccessToken(user!);
            var newRefreshToken = tokenGenerator.GenerateRefreshToken();

            var newTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                ApplicationUserId = user!.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                DeviceInfo = tokenFromDb.DeviceInfo // keep same device info
            };

            await context.RefreshTokens.AddAsync(newTokenEntity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);




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
