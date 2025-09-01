using System.Net;
using System.Reflection.PortableExecutable;
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
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//It makes Swagger aware of JWT authentication and enables you to test secured endpoints directly in Swagger UI.
builder.Services.AddSwaggerGen(
    options =>
    {
        //This adds a security scheme definition to Swagger so it knows your API uses JWT Bearer authentication.
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {

            Name = "Authorization", //The name of the header where the token will be passed (HTTP Authorization header).
            Type = SecuritySchemeType.Http,//We set the scheme type to http since we're using bearer authentication
            Scheme = "Bearer",  //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
            BearerFormat = "JWT",// Indicates that the token format is JSON Web Token (JWT).
            In = ParameterLocation.Header, //The token must be included in the request header.
            Description = "Enter your JWT token : Bearer "//Shown in the Swagger UI
        });

        // This specifies every request in this API must use the Bearer security scheme (JWT in the Authorization header).
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" //The name of the previously defined security scheme.
                }
            },
            //this array represents scopes required for the security scheme.
            //For JWT Bearer tokens, you usually don’t have scopes because its included in payload so the array is empty.
            //For OAuth2, the array lists specific scopes/permissions that the token must have for that endpoint,
            //e.g., "read:users" or "write:orders".
            Array.Empty<string>()
        }
    });
    }

    );

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//This makes IHttpContextAccessor available for dependency injection.
builder.Services.AddHttpContextAccessor(); 


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
builder.Services.AddScoped<IUserContext, UserContext>();


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
// This middleware catches any exceptions thrown by downstream middleware or controllers
// because it wraps the call to _next(context) in a try-catch block.
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//app.MapGroup("/api")
//    .MapIdentityApi<IdentityUser>();
app.Run();
