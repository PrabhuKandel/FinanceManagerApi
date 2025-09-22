
using FinanceManager.Application.Features.Auth.Commands;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.AuthValidator.CommandValidator
{
    internal class RevokeTokenCommandVallidator : AbstractValidator<RevokeTokenCommand>
    {
        private readonly IApplicationDbContext _context;

        public RevokeTokenCommandVallidator(IApplicationDbContext context)
        {
            _context = context;
    

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token must not be empty");

            RuleFor(x => x.Token)
                .CustomAsync(async (token, context, cancellationToken) =>
                {
                    var tokenFromDb = await _context.RefreshTokens
                        .FirstOrDefaultAsync(rt => rt.Token == token , cancellationToken);

                    if (tokenFromDb == null)
                        context.AddFailure("Invalid refresh token");

                    if (tokenFromDb != null && tokenFromDb.ExpiresAt <= DateTime.UtcNow)
                        context.AddFailure("Token has already expired");

                    if (tokenFromDb != null && tokenFromDb.RevokedAt != null)
                        context.AddFailure("Refresh token already revoked");
                });
        }
    }
    }
