using FinanceManager.Api.Middlewares;
using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Services;
using FinanceManager.Infrastructure.Data;
using FinanceManager.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
    
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Disable automatic model state validation
//builder.Services.AddControllers()
//    .ConfigureApiBehaviorOptions(options =>
//    {
//        options.SuppressModelStateInvalidFilter = true;
//    });


builder.Services.AddScoped<ITransactionCategoryService, TransactionCategoryService>();
builder.Services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
//builder.Services.AddTransient<IValidator<TransactionCategoryCreateDto>, TransactionCategoryCreateDtoValidator>();
//builder.Services.AddTransient<IValidator<TransactionCategoryUpdateDto>, TransactionCategoryUpdateDtoValidator>();

builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
//builder.Services.AddTransient<IValidator<PaymentMethodCreateDto>, PaymentMethodCreateDtoValidator>();
//builder.Services.AddTransient<IValidator<PaymentMethodUpdateDto>, PaymentMethodUpdateDtoValidator>();

//builder.Services.AddValidatorsFromAssemblyContaining<PaymentMethodCreateDtoValidator>();

builder.Services.AddScoped<ITransactionRecordService, TransactionRecordService>();
builder.Services.AddScoped<ITransactionRecordRepository, TransactionRecordRepository>();

var app = builder.Build();
// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // apply any pending migrations
    DbSeeder.Seed(context);     // run your manual seeding
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
