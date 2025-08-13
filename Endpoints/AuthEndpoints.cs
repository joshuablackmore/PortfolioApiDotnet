using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PortfolioApi.Data;
using PortfolioApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioApi.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group, byte[] keyBytes)
    {
        group.MapPost("/register", async (UserCredentials credentials, AppDbContext db) =>
{
    var u = credentials.Username.ToLower();
    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(credentials.Password);

    if (await db.Users.AnyAsync(x => x.Username == u))
        return Results.BadRequest("User already exists.");


    var user = new User 
    { 
        Username = u,
        Password = hashedPassword
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Ok($"{credentials.Username} registered successfully.");
});

    group.MapPost("/login", async (UserCredentials credentials, AppDbContext db) =>
{
    var u = credentials.Username.ToLower();

    var user = await db.Users.FirstOrDefaultAsync(x => x.Username == u);

    if (user == null || !BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password))
        return Results.Unauthorized();

    var claims = new[] { new Claim(ClaimTypes.Name, user.Username) };

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: new SigningCredentials(
            new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256)
    );

    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    return Results.Ok(new { token = tokenString });
});

    return group;
    }
}