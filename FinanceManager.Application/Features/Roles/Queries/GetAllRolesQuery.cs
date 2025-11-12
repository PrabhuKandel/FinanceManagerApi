using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Roles.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace FinanceManager.Application.Features.Roles.Queries
{
    public record GetAllRolesQuery():IRequest<OperationResult<IEnumerable<RoleResponseDto>>>
    {
    }
}
