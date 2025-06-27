using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.Classes;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.Interfaces;
using RS1_2024_2025.API.Helper.Auth;
using RS1_2024_2025.Database;
using RS1_2024_2025.Services;
using RS1_2024_25.API.Endpoints.LoginEndpoint.Interfaces;
using RS1_2024_25.API.Endpoints.TutorSearch;
using System.Text;

var config = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", false)
	.Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(config.GetConnectionString("db1")));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

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
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, RS1_2024_25.API.SignalR.NameUserIdProvider>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.OperationFilter<MyAuthorizationSwaggerHeader>());
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(
	options => options
		.SetIsOriginAllowed(x => _ = true)
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials()
);

// **Dodano: Omogućeno serviranje statičkih fajlova iz foldera Uploads**
var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(uploadsPath),
	RequestPath = "/Uploads"
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<RS1_2024_25.API.SignalR.ChatHub>("/chatHub"); 


using (var scope = app.Services.CreateScope())
{
	var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	dataContext.Database.Migrate();
}

app.Run();
