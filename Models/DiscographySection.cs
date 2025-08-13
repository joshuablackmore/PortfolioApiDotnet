namespace PortfolioApi.Models;

public class DiscographySection
{
    public string Heading { get; set; } = null!;
    public List<MusicAlbum> Albums { get; set; } = new();
}