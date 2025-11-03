

using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using FinanceManager.Application.Dtos.Roles;
using FinanceManager.Application.Features.Roles.Queries;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using FinanceManager.IntegrationTest.Shared;
using FinanceManager.IntegrationTest.Tests.Auth;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace FinanceManager.IntegrationTest.Tests.Roles
{
    public class RoleIntegrationTests : IClassFixture<FinanceManagerWebApplicationFactory>, IDisposable
    {
        private readonly IMediator _mediator;
        private readonly ITestOutputHelper _output;
        private readonly IServiceScope _scope;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly TestUserContext testUserContext;
        public RoleIntegrationTests(FinanceManagerWebApplicationFactory factory, ITestOutputHelper output)
        {

            _output = output;
            _scope = factory.Services.CreateScope();
            var services = _scope.ServiceProvider;
            _mediator = services.GetRequiredService<IMediator>();
            _context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            testUserContext = services.GetRequiredService<IUserContext>() as TestUserContext;
            _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            SeedAuthData.SeedAdminUserAsync(services).GetAwaiter().GetResult();
        }

        [Fact]
        public async Task GetAllRoles_AsAdmin_ReturnsListOfRolesSuccessfully()
        {
           
            // Arrange
            var adminUser = _context.Users.First(u => u.UserName == "admin@gmail.com");
            var roles = await _userManager.GetRolesAsync(adminUser);
            var role = roles.FirstOrDefault() ?? "User";
            testUserContext.UserId = adminUser.Id;
            testUserContext.Role = role;




            // Act
            var query = new GetAllRolesQuery();
            var result = await _mediator.Send(query);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.Data.Should().NotBeEmpty();
            result.Message.Should().Be("Roles retrieved successfully.");

        
            result.Data.Select(r => r.Name).Should().Contain(new[] { "Admin", "User" });

            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            }));
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
