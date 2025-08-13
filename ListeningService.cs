using PortfolioApi.Models;
using System.Text.Json;

namespace PortfolioApi.Services;

public class ListeningService
{
    private readonly string _jsonPath = Path.Combine("Data", "listening.json");

    public List<ArtistCard> GetListeningAlbums()
    {
        var json = File.ReadAllText(_jsonPath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var cards = JsonSerializer.Deserialize<List<ArtistCard>>(json, options);

        return cards ?? new List<ArtistCard>();
    }
}   