
using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Permissions.Dtos;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Permissions.Queries.GetAll
{
    public class GetAllPermissionsHandler(IApplicationDbContext _context) : IRequestHandler<GetAllPermissionsQuery, OperationResult<IEnumerable<PermissionResponseDto>>>
    {
        public async Task<OperationResult<IEnumerable<PermissionResponseDto>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await  _context.Permissions
                .AsNoTracking()
                .Select(p => new PermissionResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsActive = p.IsActive
                })
                .ToListAsync(cancellationToken);

            return new OperationResult<IEnumerable<PermissionResponseDto>>
            {
                Data = permissions,
                Message = "Permissions retrieved successfully"
            };
        }
    }
}
