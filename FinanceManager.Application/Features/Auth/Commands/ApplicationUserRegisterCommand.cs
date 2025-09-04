using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.ApplicationUser;
using MediatR;

namespace FinanceManager.Application.Features.Auth.Commands
{
    public record ApplicationUserRegisterCommand(ApplicationUserRegisterDto registerUser):IRequest<string>
    {
    }
}
