namespace PortfolioApi.Models;

public class MusicAlbum
{
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public string Genre { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Artwork { get; set; } = null!;
    public string Role { get; set; } = null!;
    public Dictionary<string, string> Links { get; set; } = new();
}