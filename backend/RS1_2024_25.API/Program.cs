using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RS1_2024_2025.Domain;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.Classes;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.Interfaces;
using RS1_2024_2025.API.Helper;
using RS1_2024_2025.API.Helper.Auth;
using RS1_2024_2025.Services;
using RS1_2024_2025.Database;


var config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", false)
.Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("db1"),
        sqlOptions => sqlOptions.MigrationsAssembly("RS1_2024_2025.Database")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.OperationFilter<MyAuthorizationSwaggerHeader>());
builder.Services.AddHttpContextAccessor();

//dodajte vaše servise
//builder.Services.AddTransient<MyAuthService>();
//builder.Services.AddTransient<RS1_2024_2025.API.Helper.MyTokenGenerator>();
//builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Images")),
    RequestPath = "/Uploads/Images"
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(
    options => options
        .SetIsOriginAllowed(x => _ = true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
); //This needs to set everything allowed


app.UseAuthorization();

app.MapControllers();

app.Run();

