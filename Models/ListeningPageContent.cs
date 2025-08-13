namespace PortfolioApi.Models;

public class ListeningPageContent
{
    public string Heading {get; set;} = null!;
    public List<ArtistCard> ArtistCards { get; set; } = new();
}


public class ArtistCard
{
    public required string ArtistName { get; set; }
    public required string Image { get; set; }
    public required string Genre { get; set; }
    public string? Reason { get; set; }
    public Dictionary<string, string>? Links { get; set; }
}
