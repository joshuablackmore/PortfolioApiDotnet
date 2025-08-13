using PortfolioApi.Models;
using System.Text.Json;

namespace PortfolioApi.Services;

public class MusicService
{
    private readonly string _jsonPath = Path.Combine("Data", "albums.json");

    public List<DiscographySection> GetDiscographySections()
    {
        var json = File.ReadAllText(_jsonPath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // 👈 THIS is the key fix
        };
        
        var sections = JsonSerializer.Deserialize<List<DiscographySection>>(json, options);

        return sections ?? new List<DiscographySection>();
    }
}