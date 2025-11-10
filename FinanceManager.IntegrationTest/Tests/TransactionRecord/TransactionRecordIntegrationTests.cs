using FinanceManager.Application.Dtos.TransactionPayment;
using FinanceManager.Application.Features.TransactionRecords.Commands.BulkCreate;
using FinanceManager.Application.Features.TransactionRecords.Commands.Create;
using FinanceManager.Application.Features.TransactionRecords.Commands.Delete;
using FinanceManager.Application.Features.TransactionRecords.Commands.PatchApprovalStatus;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.Features.TransactionRecords.Queries.GetById;
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

        [Fact]
        public async Task DeleteTransactionRecord_AsAdmin_WithValidId_ReturnsSuccessMessage()
        {
            // Arrange
            var adminUser = _context.Users.First(u => u.UserName == "admin@gmail.com");
            var roles = await _userManager.GetRolesAsync(adminUser);
            var role = roles.FirstOrDefault() ?? "User";
            testUserContext.UserId = adminUser.Id;
            testUserContext.Role = role;

            // Create a transaction record first (so we have something to delete)
            var transactionCategoryId = _context.TransactionCategories.First().Id;
            var paymentMethodId = _context.PaymentMethods.Take(2).ToList();

            var createCommand = new CreateTransactionRecordCommand(
                TransactionCategoryId: transactionCategoryId,
                Amount: 150m,
                Description: "Transaction to delete",
                TransactionDate: DateTime.UtcNow,
                Payments: new List<TransactionPaymentDto>
                {
            new TransactionPaymentDto { PaymentMethodId = paymentMethodId[0].Id, Amount = 100m },
            new TransactionPaymentDto { PaymentMethodId = paymentMethodId[1].Id, Amount = 50m }
                },
                TransactionAttachments: null
            );

            var createdResult = await _mediator.Send(createCommand);

            createdResult.Should().NotBeNull();
            createdResult.Data.Should().NotBeNull();

            var transactionId = createdResult.Data!.Id;

            // Act
            var deleteCommand = new DeleteTransactionRecordCommand(transactionId);
            var deleteResult = await _mediator.Send(deleteCommand);

            // Assert
            deleteResult.Should().NotBeNull();
            deleteResult.Message.Should().NotBeNullOrEmpty();
            deleteResult.Message.Should().Be("Transaction record deleted");

            // Verify the record no longer exists
            var deletedRecord = _context.TransactionRecords.FirstOrDefault(t => t.Id == transactionId);
            deletedRecord.Should().BeNull();

            // Output for debugging
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(deleteResult, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }

        [Fact]
        public async Task GetTransactionRecordById_AsAdmin_WithValidId_ReturnsTransactionRecord()
        {
            // Arrange
            var adminUser = _context.Users.First(u => u.UserName == "admin@gmail.com");
            var roles = await _userManager.GetRolesAsync(adminUser);
            var role = roles.FirstOrDefault() ?? "User";
            testUserContext.UserId = adminUser.Id;
            testUserContext.Role = role;

            // Create a transaction record first (so we have something to retrieve)
            var transactionCategoryId = _context.TransactionCategories.First().Id;
            var paymentMethodId = _context.PaymentMethods.Take(2).ToList();

            var createCommand = new CreateTransactionRecordCommand(
                TransactionCategoryId: transactionCategoryId,
                Amount: 250m,
                Description: "Transaction to retrieve",
                TransactionDate: DateTime.UtcNow,
                Payments: new List<TransactionPaymentDto>
                {
            new TransactionPaymentDto { PaymentMethodId = paymentMethodId[0].Id, Amount = 150m },
            new TransactionPaymentDto { PaymentMethodId = paymentMethodId[1].Id, Amount = 100m }
                },
                TransactionAttachments: null
            );

            var createdResult = await _mediator.Send(createCommand);

            createdResult.Should().NotBeNull();
            createdResult.Data.Should().NotBeNull();

            var transactionId = createdResult.Data!.Id;

            // Act
            var query = new GetTransactionRecordByIdQuery(transactionId);
            var getResult = await _mediator.Send(query);

            // Assert
            getResult.Should().NotBeNull();
            getResult.Data.Should().NotBeNull();
            getResult.Data!.Id.Should().Be(transactionId);
            getResult.Data!.Amount.Should().Be(createCommand.Amount);
            getResult.Data!.Description.Should().Be(createCommand.Description);
            getResult.Data!.TransactionCategory!.Id.Should().Be(createCommand.TransactionCategoryId);
            getResult.Message.Should().NotBeNullOrEmpty();
            getResult.Message.Should().Be("Transaction record  retrieved successfully");

            // Output for debugging
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(getResult, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }



        [Fact]
        public async Task BulkCreateTransactionRecords_AsAdmin_WithValidData_ShouldCreateMultipleRecords()
        {
            // Arrange
            var adminUser = _context.Users.First(u => u.UserName == "admin@gmail.com");
            var roles = await _userManager.GetRolesAsync(adminUser);
            testUserContext.UserId = adminUser.Id;
            testUserContext.Role = roles.FirstOrDefault() ?? "Admin";

            var categoryId = _context.TransactionCategories.First().Id;
            var paymentMethods = _context.PaymentMethods.Take(2).ToList();

            var transactionList = new List<BulkCreateTransactionRecordDto>
            {
                new BulkCreateTransactionRecordDto
                {
                    TransactionCategoryId = categoryId,
                    Amount = 120m,
                    Description = "Bulk transaction 1",
                    TransactionDate = DateTime.UtcNow,
                    Payments = new List<TransactionPaymentDto>
                    {
                        new TransactionPaymentDto { PaymentMethodId = paymentMethods[0].Id, Amount = 50m },
                        new TransactionPaymentDto { PaymentMethodId = paymentMethods[1].Id, Amount = 70m }
                    }
                },
                new BulkCreateTransactionRecordDto
                {
                    TransactionCategoryId = categoryId,
                    Amount = 80m,
                    Description = "Bulk transaction 2",
                    TransactionDate = DateTime.UtcNow,
                    Payments = new List<TransactionPaymentDto>
                    {
                        new TransactionPaymentDto { PaymentMethodId = paymentMethods[0].Id, Amount = 30m },
                        new TransactionPaymentDto { PaymentMethodId = paymentMethods[1].Id, Amount = 50m }
                    }
                }
            };

            var command = new BulkCreateTransactionRecordCommand(transactionList);

            // Act
            var result = await _mediator.Send(command);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("Bulk transaction records created successfully.");

            // Verify records were inserted into DB
            var createdRecords = _context.TransactionRecords
                .Where(t => transactionList.Select(x => x.Description).Contains(t.Description))
                .ToList();

            createdRecords.Should().HaveCount(transactionList.Count);

 

            // Debug output
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                result,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }


        public void Dispose()
        {
            _scope.Dispose();
        }

    }
    
}
