using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenHandler(IApplicationDbContext context) : IRequestHandler<RevokeTokenCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenFromDb = await context.RefreshTokens
                    .FirstOrDefaultAsync(rt => rt.Token == request.Token );
            Guard.Against.Null(tokenFromDb, nameof(tokenFromDb));

            tokenFromDb.RevokedAt = DateTime.UtcNow;
            tokenFromDb.RevocationReason = request.RevocationReason;

            await context.SaveChangesAsync(cancellationToken);

            return new OperationResult<string>
            {
                Message = "Token revoked successfully"
            };
        }
    }
}
