using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace PortfolioApi.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/education/drumming", [Authorize] () =>
        {
            return Results.Ok("This would return protected educational drum video content.");
        });

        group.MapGet("/me", (HttpContext http) =>
        {
            var username = http.User.Identity?.Name;
            return Results.Ok(new { username });
        });

        return group;
    }
}