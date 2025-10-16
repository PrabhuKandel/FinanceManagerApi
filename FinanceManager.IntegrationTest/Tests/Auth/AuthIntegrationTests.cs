using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Features.Auth.Commands;
using FinanceManager.Domain.Entities;
using FinanceManager.IntegrationTest.Shared;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace FinanceManager.IntegrationTest.Tests.Auth
{
    public class AuthIntegrationTests : IClassFixture<FinanceManagerWebApplicationFactory>, IDisposable
    {
        private readonly IServiceScope _scope;
        private readonly IMediator _mediator;
        private readonly ITestOutputHelper _output;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthIntegrationTests(FinanceManagerWebApplicationFactory factory, ITestOutputHelper output)
        {

            _output = output;
            _scope = factory.Services.CreateScope();
            var services = _scope.ServiceProvider;
            _mediator = services.GetRequiredService<IMediator>();
            _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            SeedAuthData.SeedAdminUserAsync(services).GetAwaiter().GetResult();



        }

        [Fact]
        public async Task LoginApplicationUser_WithValidCredentials_ShouldReturnTokenAndLoggedInUser()
        {

            // Arrange
            var loginUser = new ApplicationUserLoginDto
            {
                Email = "admin@gmail.com",
                Password = "Admin@123"
            };

            var command = new ApplicationUserLoginCommand(loginUser);

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.Data.Should().NotBeNull();
            result.Message.Should().Be("Login Successfull!!");
            result.Data?.AccessToken.Should().NotBeNullOrEmpty();
            result.Data?.RefreshToken.Should().NotBeNullOrEmpty();
            result.Data?.UserId.Should().NotBeNullOrEmpty();
            result.Data?.Email.Should().Be(loginUser.Email);

            //output

            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

        }

        [Fact]
        public async Task LoginApplicationUser_WithInvalidPassword_ShouldThrowUnauthorized()
        {
            // Arrange
            var loginUser = new ApplicationUserLoginDto
            {
                Email = "admin@gmail.com",
                Password = "wrongpassword"
            };

            var command = new ApplicationUserLoginCommand(loginUser);

            // Act

            var ex = await Assert.ThrowsAsync<AuthenticationException>(
            () => _mediator.Send(command));

            // Assert the exception message
            Assert.Equal(401, ex.StatusCode);
            Assert.Equal("Invalid Credentials", ex.Message);

            //output
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(ex.Message, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

        }

        [Fact]
        public async Task RegisterApplicationUser_WithValidData_ShouldCreateUser()
        {
            // Arrange
            var role = await _roleManager.FindByNameAsync("Admin");
            var registerUser = new ApplicationUserRegisterDto
            {
                FirstName = "Test",
                LastName = "Test",
                Address = "Test Address",
                Email = "test@gmail.com",
                RoleId = role?.Id
            };

            var command = new ApplicationUserRegisterCommand(registerUser); 

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.Message.Should().Be("Registration Successfulll!!");

            // Optionally verify the user exists in the DB
            var user = await _userManager.FindByEmailAsync(registerUser.Email);
            user.Should().NotBeNull();
            user.FirstName.Should().Be(registerUser.FirstName);


        }

        public void Dispose()
        {
            _scope.Dispose();   
        }
    }

}
