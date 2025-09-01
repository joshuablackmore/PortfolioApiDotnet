namespace PortfolioApi.Models;

public class HomePageContent
{
    
    public required string PageTitle { get; set; }
    public required string Name { get; set; }
    public required string Tagline { get; set; }
    public required string Intro { get; set; }
    public List<Link> Links {get; set;} = new List<Link>();
}