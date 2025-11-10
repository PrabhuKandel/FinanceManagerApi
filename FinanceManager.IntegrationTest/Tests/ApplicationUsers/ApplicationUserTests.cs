
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using FinanceManager.Application.Features.ApplicationUsers.Commands.ToggleUserLockStatus;
using FinanceManager.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers;
using FinanceManager.IntegrationTest.Shared;
using FinanceManager.IntegrationTest.Tests.Auth;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace FinanceManager.IntegrationTest.Tests.ApplicationUsers
{
    public class ApplicationUserTests : IClassFixture<FinanceManagerWebApplicationFactory>, IDisposable
    {
        private readonly IMediator _mediator;
        private readonly ITestOutputHelper _output;
        private readonly IServiceScope _scope;
        public ApplicationUserTests(FinanceManagerWebApplicationFactory factory, ITestOutputHelper output)
        {
            _output = output;

            _scope = factory.Services.CreateScope();
            var services = _scope.ServiceProvider;
            _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
            SeedAuthData.SeedAdminUserAsync(services).GetAwaiter().GetResult();

        }

        [Fact]
        public async Task GetAllApplicationUsers_ShouldReturnListOfUsers()
        {
            // Arrange
            var query = new GetAllApplicationUsersQuery();
            // Act
            var result = await _mediator.Send(query);
            // Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().NotBeEmpty();
            result.Message.Should().Be("Users fetched successfully");
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                     result,
                     new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                 ));
        }

        [Fact]
        public async Task ToggleUserLockStatus_ShouldLockAndUnlockUser()
        {
            // Arrange
            var allUsers = await _mediator.Send(new GetAllApplicationUsersQuery());
            var user = allUsers?.Data?.First(); // pick first user



            // Lock the user 
            var lockCommand = new ToggleUserLockStatusCommand(user.Id);
            var lockResult = await _mediator.Send(lockCommand);

            // Assert lock result
            lockResult.Data.Should().NotBeNull();
            lockResult.Data.IsLocked.Should().BeTrue();
            lockResult.Data.IsManuallyLocked.Should().BeTrue();
            lockResult.Data.LockReason.Should().Be("Manually locked by admin");

            _output.WriteLine($"Lock result: {System.Text.Json.JsonSerializer.Serialize(lockResult, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");

            // Unlock the user 
            var unlockCommand = new ToggleUserLockStatusCommand(user.Id);
            var unlockResult = await _mediator.Send(unlockCommand);

            // Assert unlock result
            unlockResult.Data.Should().NotBeNull();
            unlockResult.Data.IsLocked.Should().BeFalse();
            unlockResult.Data.IsManuallyLocked.Should().BeFalse();
            unlockResult.Data.LockReason.Should().BeNull();

            _output.WriteLine($"Unlock result: {System.Text.Json.JsonSerializer.Serialize(unlockResult, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");
        }
        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
