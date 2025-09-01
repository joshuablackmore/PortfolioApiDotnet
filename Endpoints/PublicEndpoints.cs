using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;
using PortfolioApi.Services;

namespace PortfolioApi.Endpoints;

public static class PublicEndpoints
{
    public static RouteGroupBuilder MapPublicEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/home", () =>
        {
            var content = new HomePageContent
            {
                PageTitle = "Home",
                Name = "Joshua Blackmore",
                Tagline = "I build and hit things.",
                Intro = "I’m a full-stack software engineer and professional drummer. During the day I help build scalable, accessible web apps. After hours, I write and perform experimental music.",
                Links = 
                {
                    new Link { Name = "My Music", Url = "/music/my" },
                    new Link { Name = "What I'm Listening To", Url = "/music/listening" },
                    new Link { Name = "Engineering", Url = "/engineering" }
                }
            };

            return Results.Ok(content);
        });

        group.MapGet("navigation", () => 
        {
            var content = new NavigationContent
            {
                Links = 
                {
                    new Link { Name = "Home", Url = "/"}
                }
            };
            return Results.Ok(content);
        });

        group.MapGet("/music/my", (MusicService musicService) =>
        {
            var sections = musicService.GetDiscographySections();
     
            var page = new MyMusicPageContent
            {
                Heading = "My Music",
                Discography = sections
            };
            return Results.Ok(page);
        });

        group.MapGet("/music/listening", (ListeningService listeningService) =>
        {
            var cards = listeningService.GetListeningAlbums();

              var page = new ListeningPageContent
            {
                Heading = "What I'm Listening To",
                ArtistCards = cards
            };
            return Results.Ok(page);
        });

        group.MapGet("/engineering", () =>
        {
            var page = new EngineeringPage
            {
                Heading = "Engineering",
                Positions = new List<Position> 
                {
                    new Position {
                        Period = "2024 - Present",
                        Company = "My Company",
                        Role = "Software Engineer",
                        Responsibilities = "I build and hit things.",
                        TechStack = "C#, .NET, SQL, HTML, CSS, JavaScript"
                    }
                }
            };
            return Results.Ok(page);
        });

        return group;
    }
}