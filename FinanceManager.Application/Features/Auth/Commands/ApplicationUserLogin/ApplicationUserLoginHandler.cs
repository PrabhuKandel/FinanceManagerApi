using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using FinanceManager.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FinanceManager.Application.Features.Auth.Dtos;

namespace FinanceManager.Application.Features.Auth.Commands.ApplicationUserLogin
{
    public class ApplicationUserLoginHandler : IRequestHandler<ApplicationUserLoginCommand, OperationResult<ApplicationUserLoginResponseDto>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenGenerator tokenGenerator;
        private readonly IApplicationDbContext context;
        private readonly IHttpContextAccessor contextAccessor; 
        public ApplicationUserLoginHandler(UserManager<ApplicationUser> _userManager,ITokenGenerator _tokenGenerator, IApplicationDbContext _context, IHttpContextAccessor _contextAccessor)
        {
            userManager = _userManager;
            tokenGenerator = _tokenGenerator;
            context = _context;
            contextAccessor = _contextAccessor;
        }


        public async Task<OperationResult<ApplicationUserLoginResponseDto>> Handle(ApplicationUserLoginCommand request, CancellationToken cancellationToken)
        {
             var applicationUser = await userManager.FindByEmailAsync(request.Email);
            if (applicationUser == null)
            {
                throw new AuthenticationException("Invalid email");
            }

            //  Check lockout

            if (await userManager.IsLockedOutAsync(applicationUser))
            {
                if (applicationUser.LockoutEnd == DateTimeOffset.MaxValue)
                    throw new AuthenticationException("Your account is locked. Please contact support or try later.");

                throw new AuthenticationException("Your account is temporarily locked due to multiple failed login attempts. Please try again later.");
            }

            //validate password
            var isPasswordValid = await userManager.CheckPasswordAsync(applicationUser, request.Password);
            if (!isPasswordValid)
            {
                if (userManager.SupportsUserLockout)
                    await userManager.AccessFailedAsync(applicationUser); // increments failed login attempts

                throw new AuthenticationException("Invalid Credentials");
            }

            //  Reset failed attempts on successful login
            await userManager.ResetAccessFailedCountAsync(applicationUser);

            var accessToken = await tokenGenerator.GenerateAccessToken(applicationUser);
            var refreshToken = tokenGenerator.GenerateRefreshToken();

            // Check if a token already exists for this user 
            var existingToken = await context.RefreshTokens
                .FirstOrDefaultAsync(t => t.ApplicationUserId == applicationUser.Id
                                          && t.RevokedAt == null,
                                    cancellationToken);

            if (existingToken != null)
            {
                existingToken.RevokedAt = DateTime.UtcNow;
                existingToken.RevocationReason = "New login from device";
                context.RefreshTokens.Update(existingToken);
            }

            //store refresh token details
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                DeviceInfo = "Unknown",
                ApplicationUserId = applicationUser.Id
            };

            await context.RefreshTokens.AddAsync(refreshTokenEntity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);


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
