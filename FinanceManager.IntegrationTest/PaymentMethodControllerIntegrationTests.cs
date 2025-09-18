using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Features.PaymentMethods.Commands;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.CreatePaymentMethod;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;


namespace FinanceManager.IntegrationTest
{
    public class PaymentMethodControllerIntegrationTests: IClassFixture<FinanceManagerWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly IMediator _mediator;
        private readonly ITestOutputHelper _output;

        public PaymentMethodControllerIntegrationTests(FinanceManagerWebApplicationFactory factory, ITestOutputHelper output)
        {
            _client = factory.CreateClient();
            _output = output;
            // Resolve IMediator from the factory's services
            var scope = factory.Services.CreateScope();
            _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();



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


    }
}
