using System.Text;
using FinanceManager.Api.Filters;
using FinanceManager.Api.Middlewares;
using FinanceManager.Application.DependencyInjection;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using FinanceManager.Infrastructure.DependencyInjection;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
// Add Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();


// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();



//builder.Services.AddFluentValidationAutoValidation();
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


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add Hangfire client.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));





builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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
        ClockSkew = TimeSpan.Zero,

    };
});



builder.Services.AddControllers(options => options.Filters.Add<RequestResponseLoggingFillter>());
builder.Services.AddHangfireServer();

var app = builder.Build();
// Apply migrations and seed data
//using (var scope = app.Services.CreateScope())

//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<ApplicationDbContext>();
//    context.Database.Migrate(); // apply any pending migrations
//    DbSeeder.Seed(context);     // run your manual seeding
//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

//    await RoleSeeder.SeedRolesAsync(roleManager);
//    await RoleSeeder.SeedAdminUserAsync(userManager);

//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// This middleware catches any exceptions thrown by downstream middleware or controllers
// because it wraps the call to _next(context) in a try-catch block.
//  
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
//app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard();
app.MapControllers();
app.Run();
public partial class Program { }