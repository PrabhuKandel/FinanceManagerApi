using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Features.PaymentMethods.Commands;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.CreatePaymentMethod;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.DeletePaymentMethod;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.UpdatePaymentMethod;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Queries.GellAllPaymentMethod;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Queries.GetPaymentMethodById;
using FinanceManager.IntegrationTest.Shared;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;


namespace FinanceManager.IntegrationTest.Tests.PaymentMethod
{
    public class PaymentMethodIntegrationTests: IClassFixture<FinanceManagerWebApplicationFactory>, IDisposable
    {

        private readonly IMediator _mediator;
        private readonly ITestOutputHelper _output;
        private readonly IServiceScope _scope;

        public PaymentMethodIntegrationTests(FinanceManagerWebApplicationFactory factory, ITestOutputHelper output)
        {
         
            _output = output;
            // Resolve IMediator from the factory's services
            _scope = factory.Services.CreateScope();
            _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();



        }


        [Fact]
        public async Task CreatePaymentMethod_ReturnsCreatedPaymentMethod()
        {
            // Arrange
            var newPaymentMethod = new PaymentMethodCreateDto
            {
                Name = "TestCase1",
                Description = "Test case",
                IsActive = true

            };

            var command = new CreatePaymentMethodCommand(newPaymentMethod);


            // Act
            var createdPaymentMethod = await _mediator.Send(command);



            // Assert

            Assert.NotNull(createdPaymentMethod);
            Assert.Equal(newPaymentMethod.Name, createdPaymentMethod.Data?.Name);
            Assert.Equal(newPaymentMethod.Description, createdPaymentMethod.Data?.Description);
            Assert.Equal(newPaymentMethod.IsActive, createdPaymentMethod.Data?.IsActive);
            Assert.NotEqual(Guid.Empty, createdPaymentMethod.Data?.Id);

            // Assert the message
            Assert.NotNull(createdPaymentMethod.Message); // Ensure message is not null
            Assert.Equal("New payment method added", createdPaymentMethod.Message);

            //output

            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(createdPaymentMethod, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

        }


        [Fact]
        public async Task CreatePaymentMethod_ShouldReturnException_WhenNameAlreadyExist()
        {
            //Arange
            var newPaymentMethod1 = new PaymentMethodCreateDto
            {
                Name = "TestCase2",
                Description = "Test case",
                IsActive = true
            };
            var newPaymentMethod2 = new PaymentMethodCreateDto
            {
                Name = "TestCase2", // Same name to test uniqueness
                Description = "Another test case",
                IsActive = true
            };

            var command1 = new CreatePaymentMethodCommand(newPaymentMethod1);
            var command2 = new CreatePaymentMethodCommand(newPaymentMethod2);


            //Act
            var createdPaymentMethod1 = await _mediator.Send(command1);

            //Assert
            var ex = await Assert.ThrowsAsync<BusinessValidationException>(
            () => _mediator.Send(command2));

            // Assert the exception message
            Assert.Equal(400, ex.StatusCode);
            Assert.Equal("Validation failed", ex.Message);
            // Assert the field-specific error
            Assert.True(ex.Errors?.ContainsKey("PaymentMethod.Name"));
            Assert.Equal("Name already exists", ex.Errors?["PaymentMethod.Name"][0]);


            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(ex.Errors, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

        }




        [Fact]
        public async Task CreatePaymentMethod_WithDapper_ReturnsCreatedPaymentMethod()
        {
            // Arrange
            var command = new CreatePaymentMethodDapperCommand(
                "TestCase3",
                "TestCase3",
                true
            );

            // Act
            var createdPaymentMethod = await _mediator.Send(command);


            // Assert using FluentAssertions
            createdPaymentMethod.Should().NotBeNull();
            createdPaymentMethod.Data.Should().NotBeNull();
            createdPaymentMethod.Data!.Name.Should().Be(command.Name);
            createdPaymentMethod.Data.Description.Should().Be(command.Description);
            createdPaymentMethod.Data.IsActive.Should().Be(command.IsActive);
            createdPaymentMethod.Data.Id.Should().NotBe(Guid.Empty);
            createdPaymentMethod.Message.Should().NotBeNullOrEmpty();
            createdPaymentMethod.Message.Should().Be("New payment method added");


            //output

            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(createdPaymentMethod, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

        }


        [Fact]
        public async Task UpdatePaymentMethod_WithDapper_ReturnsUpdatedPaymentMethod()
        {
            // Arrange: create an initial payment method
            var createCommand = new CreatePaymentMethodDapperCommand(
                "IntegrationTestName",
                "IntegrationTestDescription",
                true
            );

            var createdResult = await _mediator.Send(createCommand);
            createdResult.Should().NotBeNull();
            createdResult.Data.Should().NotBeNull();

            var existingPaymentMethodId = createdResult.Data.Id;

            // Act: update the payment method with new values
            var updateCommand = new UpdatePaymentMethodDapperCommand(
                existingPaymentMethodId,
                "UpdatedIntegrationName",
                "UpdatedIntegrationDescription",
                false
            );

            var updatedResult = await _mediator.Send(updateCommand);

            // Assert: verify result
            updatedResult.Should().NotBeNull();
            updatedResult.Data.Should().NotBeNull();
            updatedResult.Data.Id.Should().Be(existingPaymentMethodId); // same entity
            updatedResult.Data.Name.Should().Be(updateCommand.Name);
            updatedResult.Data.Description.Should().Be(updateCommand.Description);
            updatedResult.Data.IsActive.Should().Be(updateCommand.IsActive);
            updatedResult.Message.Should().Be("Payment method updated");

            // Output for debugging
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                updatedResult,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
            ));

    
        }

        [Theory]
        [InlineData("Card", "Card payment", true)]
        [InlineData("Cash", "Cash payment", true)]
        [InlineData("Voucher", "Voucher payment", false)]
        public async Task GetAllPaymentMethods_WithDapper_ReturnsInsertedPaymentMethods(
        string name,
        string description,
        bool isActive)
        {
            // Arrange: create a payment method with data from InlineData
            var createCommand = new CreatePaymentMethodDapperCommand(
                name,
                description,
                isActive
            );

            await _mediator.Send(createCommand);
  
            // Act: get all payment methods
 
            var paymentMethods = await _mediator.Send(new GetAllPaymentMethodsDapperQuery());

            // Assert
            paymentMethods.Should().NotBeNull();
            paymentMethods.Data.Should().NotBeNull();
            paymentMethods.Data.Should().NotBeEmpty();
            paymentMethods.Message.Should().Be("Payment methods retrieved successfully");

            // Output JSON for debug
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                paymentMethods,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
            ));
        }

        [Fact]
        public async Task GetAllPaymentMethods_WithDapper_WhenNoData_ShouldReturnEmpty()
        {
            // Act:  query without inserting any data

            var paymentMethods = await _mediator.Send(new GetAllPaymentMethodsDapperQuery());

            // Assert
            paymentMethods.Should().NotBeNull();
            paymentMethods.Data.Should().NotBeNull();
            paymentMethods.Data.Should().BeEmpty(); // no rows in DB
            paymentMethods.Message.Should().Be("Payment methods retrieved successfully");

            // Output JSON for debug
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                paymentMethods,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
            ));
        }

        [Fact]
        public async Task GetPaymentMethodById_WithDapper_ReturnsPaymentMethod()
        {
            // Arrange: create a new payment method
            var createCommand = new CreatePaymentMethodDapperCommand(
                "Card",
                "Card Payment",
                true
            );

            var createdPaymentMethod = await _mediator.Send(createCommand);


            var createdId = createdPaymentMethod.Data!.Id;

            // Act: get by id
            var getByIdQuery = new GetPaymentMethodByIdDapperQuery(createdId);
            var paymentMethod = await _mediator.Send(getByIdQuery);

            // Assert
            paymentMethod.Should().NotBeNull();
            paymentMethod.Data.Should().NotBeNull();
            paymentMethod.Data!.Id.Should().Be(createdId);
            paymentMethod.Data.Name.Should().Be(createCommand.Name);
            paymentMethod.Data.Description.Should().Be(createCommand.Description);
            paymentMethod.Data.IsActive.Should().Be(createCommand.IsActive);

            paymentMethod.Message.Should().Be("Payment Method retrieved successfully");

            // Output JSON
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                paymentMethod,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
            ));
        }


        //[Fact]
        //public async Task GetPaymentMethodById_WithDapper_WhenNotFound_ShouldReturnException()
        //{
        //    // Arrange: use a random Guid that doesn’t exist
        //    var nonExistentId = Guid.NewGuid();

        //    // Act
        //    var getByIdQuery = new GetPaymentMethodByIdDapperQuery(nonExistentId);

        //    var ex = await Assert.ThrowsAsync<BusinessValidationException>(
        //    () => _mediator.Send(getByIdQuery));

        //    // Assert the exception message

        //    //sending server error due to unhandled exception
        //    // will use result pattern to throw error later


        //}

        [Fact]
        public async Task DeletePaymentMethod_WithDapper_DeletesSuccessfully()
        {
            // Arrange: create a payment method to delete
            var createCommand = new CreatePaymentMethodDapperCommand(
                "Card",
                "Card Payment",
                true
            );

            var createdPaymentMethod = await _mediator.Send(createCommand);


            var paymentMethodId = createdPaymentMethod.Data!.Id;

            // Act: delete the payment method
            var deleteCommand = new DeletePaymentMethodDapperCommand(paymentMethodId);
            var deletePaymentMethod = await _mediator.Send(deleteCommand);

            // Assert
            deletePaymentMethod.Should().NotBeNull();
            deletePaymentMethod.Message.Should().Be("Payment method deleted");
            deletePaymentMethod.Data.Should().BeNull();


            // Output JSON
            _output.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                deletePaymentMethod,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
            ));
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
