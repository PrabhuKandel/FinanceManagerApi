using FinanceManager.Api.Middlewares;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Services;
using FinanceManager.Application.Validators;
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
builder.Services.AddScoped<ITransactionCategoryService, TransactionCategoryService>();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

// Register repositories if needed
builder.Services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
builder.Services.AddTransient<IValidator<TransactionCategoryCreateDto>, TransactionCategoryCreateDtoValidator>();
builder.Services.AddTransient<IValidator<TransactionCategoryUpdateDto>, TransactionCategoryUpdateDtoValidator>();


var app = builder.Build();


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
