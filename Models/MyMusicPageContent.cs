namespace PortfolioApi.Models;

public class MyMusicPageContent
{
    public string Heading {get; set;} = null!;
    public List<DiscographySection> Discography { get; set; } = new();
}   