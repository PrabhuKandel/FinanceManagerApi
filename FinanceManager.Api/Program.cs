using System.Text;
using FinanceManager.Api.Middlewares;
using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Services;
using FinanceManager.Domain.Models;
using FinanceManager.Infrastructure.Data;
using FinanceManager.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter your JWT token : Bearer "
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    }

    );

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateLifetime = true,
        RequireExpirationTime = true,
        IssuerSigningKey =
        new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Disable automatic model state validation
//builder.Services.AddControllers()
//    .ConfigureApiBehaviorOptions(options =>
//    {
//        options.SuppressModelStateInvalidFilter = true;
//    });



builder.Services.AddScoped<IAuthService, AuthService>();


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

builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

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
//app.MapGroup("/api")
//    .MapIdentityApi<IdentityUser>();
app.Run();
