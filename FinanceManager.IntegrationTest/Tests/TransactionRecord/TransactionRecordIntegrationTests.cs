using FinanceManager.Application.Dtos.TransactionPayment;
using FinanceManager.Application.Features.TransactionRecords.Commands.Create;
using FinanceManager.Application.Features.TransactionRecords.Commands.PatchApprovalStatus;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using FinanceManager.Infrastructure.Data;
using FinanceManager.IntegrationTest.Shared;
using FinanceManager.IntegrationTest.Tests.Auth;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using TransactionRecordEntity = FinanceManager.Domain.Entities.TransactionRecord;
namespace FinanceManager.IntegrationTest.Tests.TransactionRecord
{
    public class TransactionRecordIntegrationTests : IClassFixture<FinanceManagerWebApplicationFactory>, IDisposable
    {

        private readonly IMediator _mediator;
        private readonly ITestOutputHelper _output;
        private readonly IServiceScope _scope;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly TestUserContext testUserContext;

        public TransactionRecordIntegrationTests(FinanceManagerWebApplicationFactory factory, ITestOutputHelper output)
        {

            _output = output;
            // Resolve IMediator from the factory's services
            _scope = factory.Services.CreateScope();
            var services = _scope.ServiceProvider;
             _context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _mediator = services.GetRequiredService<IMediator>();
            _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            testUserContext = services.GetRequiredService<IUserContext>() as TestUserContext;
            SeedTransactionRecordData.SeedAsync(services).GetAwaiter().GetResult();
            SeedAuthData.SeedAdminUserAsync(services).GetAwaiter().GetResult();



        }
        [Fact]
        public async Task CreateTransactionRecord_AsAdmin_WithValidData_ReturnsCreatedTransactionRecord()
        {
            var adminUser = _context.Users.First(u => u.UserName == "admin@gmail.com");
            var roles = await _userManager.GetRolesAsync(adminUser);
            var role = roles.FirstOrDefault() ?? "User";
            testUserContext.UserId = adminUser.Id;
            testUserContext.Role = role;
            // Arrange
            var transactionCategoryId = _context.TransactionCategories.First().Id;
            var paymentMethodId = _context.PaymentMethods.Take(2).ToList();

            var command = new CreateTransactionRecordCommand(
                TransactionCategoryId: transactionCategoryId,
                Amount: 100m,
                Description: "Test transaction",
                TransactionDate: DateTime.UtcNow,
                Payments: new List<TransactionPaymentDto>
                {
                new TransactionPaymentDto { PaymentMethodId = paymentMethodId[0].Id, Amount = 60m },
                new TransactionPaymentDto { PaymentMethodId = paymentMethodId[1].Id, Amount = 40m }
                },
                TransactionAttachments: null 

            );

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.Data!.Amount.Should().Be(command.Amount);
            result.Data?.TransactionPayments?.Sum(p => p.Amount).Should().Be(command.Amount);
            result.Data?.TransactionCategory?.Id.Should().Be(command.TransactionCategoryId);
            result.Data!.ApprovalStatus.Should().Be(TransactionRecordApprovalStatus.Approved.ToString());
            result.Message.Should().NotBeNullOrEmpty();
            result.Message.Should().Be("New transaction category added");

            // Output for debugging
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }

        [Fact]
        public async Task CreateTransactionRecord_AsNormalUser_WithValidData_ReturnsPendingTransactionRecord()
        {
            // Arrange
            var normalUser = _context.Users.First(u => u.UserName == "user@test.com"); // make sure you seeded a normal user
            var roles = await _userManager.GetRolesAsync(normalUser);
            var role = roles.FirstOrDefault() ?? "User";

            testUserContext.UserId = normalUser.Id;
            testUserContext.Role = role;

            var transactionCategoryId = _context.TransactionCategories.First().Id;
            var paymentMethods = _context.PaymentMethods.Take(2).ToList();

            var command = new CreateTransactionRecordCommand(
                TransactionCategoryId: transactionCategoryId,
                Amount: 100m,
                Description : "Test transaction for normal user",
                TransactionDate : DateTime.UtcNow,
                Payments :new List<TransactionPaymentDto>
                {
            new TransactionPaymentDto { PaymentMethodId = paymentMethods[0].Id, Amount = 60m },
            new TransactionPaymentDto { PaymentMethodId = paymentMethods[1].Id, Amount = 40m }
                },
                TransactionAttachments: null
            );

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.Data!.Amount.Should().Be(command.Amount);
            result.Data?.TransactionPayments?.Sum(p => p.Amount).Should().Be(command.Amount);
            result.Data?.TransactionCategory?.Id.Should().Be(command.TransactionCategoryId);
            result.Data!.ApprovalStatus.Should().Be(TransactionRecordApprovalStatus.Pending.ToString()); // <-- check pending
            result.Message.Should().NotBeNullOrEmpty();
            result.Message.Should().Be("New transaction category added");

            // Output for debugging
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }

        [Fact]
        public async Task PatchApprovalStatus_AsAdmin_UpdatesApprovalStatusSuccessfully()
        {
            // Arrange: create an admin user context
            var adminUser = _context.Users.First(u => u.UserName == "admin@gmail.com");
            var roles = await _userManager.GetRolesAsync(adminUser);
            var role = roles.FirstOrDefault() ?? "Admin";

            testUserContext.UserId = adminUser.Id;
            testUserContext.Role = role;

            // Arrange: create a normal user's transaction (so it's pending initially)
            var normalUser = _context.Users.First(u => u.UserName == "user@test.com");
            var transactionCategoryId = _context.TransactionCategories.First().Id;
            var paymentMethods = _context.PaymentMethods.Take(2).ToList();

            var transaction = new TransactionRecordEntity
            {
                Id = Guid.NewGuid(),
                TransactionCategoryId = transactionCategoryId,
                Amount = 100m,
                ApprovalStatus = TransactionRecordApprovalStatus.Pending, // normal user creates pending
                Description = "Pending transaction for approval test",
                TransactionDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                CreatedByApplicationUserId = normalUser.Id,
                UpdatedByApplicationUserId = normalUser.Id
            };

            _context.TransactionRecords.Add(transaction);

            // Create corresponding payments
            var payments = new List<TransactionPayment>
            {
            new() { Id = Guid.NewGuid(), TransactionRecordId = transaction.Id, PaymentMethodId = paymentMethods[0].Id, Amount = 60m },
            new() { Id = Guid.NewGuid(), TransactionRecordId = transaction.Id, PaymentMethodId = paymentMethods[1].Id, Amount = 40m }
            };
            _context.TransactionPayments.AddRange(payments);

            await _context.SaveChangesAsync();

            var patchCommand = new PatchTransactionRecordApprovalStatusCommand
                (
                    Id: transaction.Id,
                    ApprovalStatus: TransactionRecordApprovalStatus.Approved
                );

            // Act
            var result = await _mediator.Send(patchCommand);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be(" Approval status updated successfully.");


            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

        }




        public void Dispose()
        {
            _scope.Dispose();
        }

    }
    
}
