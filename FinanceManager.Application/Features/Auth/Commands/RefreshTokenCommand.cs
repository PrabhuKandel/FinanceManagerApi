using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using MediatR;

namespace FinanceManager.Application.Features.Auth.Commands
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<OperationResult<TokenResponseDto>>
    {
    }
}
