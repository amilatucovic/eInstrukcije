using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.Classes;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.Interfaces;
using RS1_2024_2025.API.Helper.Auth;
using RS1_2024_2025.Database;
using RS1_2024_2025.Database.DatabaseSeeder;
using RS1_2024_2025.Services;
using RS1_2024_2025.Services.Services;
using RS1_2024_25.API.Endpoints.LoginEndpoint.Interfaces;
using System.Text;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("db1")));



builder.Services.AddTransient<RedundancyChecker>();


// Add JWT Authentication
var jwtSettings = config.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });

// Add application services
builder.Services.AddTransient<MyAuthService>();
builder.Services.AddTransient<MyTokenGenerator>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.OperationFilter<MyAuthorizationSwaggerHeader>());
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    await DatabaseSeeder.SeedAsync(context);
}
using (var scope = app.Services.CreateScope())
{
    var cleanupService = scope.ServiceProvider.GetRequiredService<RedundancyChecker>();
    await cleanupService.RemoveDuplicatesAsync();
}


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(
    options => options
        .SetIsOriginAllowed(x => _ = true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
); // Allow all CORS requests

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization(); // Add authorization middleware

app.MapControllers();

app.Run();