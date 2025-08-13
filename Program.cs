using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioApi.Data;
using System.Text;
using PortfolioApi.Endpoints;
using PortfolioApi.Services;

var builder = WebApplication.CreateBuilder(args);

// --- JWT Config ---
var jwtSecretKey = "this_is_a_super_secure_jwt_key_please_change";
var keyBytes = Encoding.UTF8.GetBytes(jwtSecretKey);

// --- Services ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=portfolio.db"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddSingleton<MusicService>();
builder.Services.AddSingleton<ListeningService>();

var app = builder.Build();

// --- Middleware ---
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// --- Modular Route Groups ---
var publicRoutes = app.MapGroup("/api/public");
var userRoutes = app.MapGroup("/api/user").RequireAuthorization();
var authRoutes = app.MapGroup("/auth");

// --- Register Endpoint Extensions ---
publicRoutes.MapPublicEndpoints();
userRoutes.MapUserEndpoints();
authRoutes.MapAuthEndpoints(keyBytes);

app.Run();