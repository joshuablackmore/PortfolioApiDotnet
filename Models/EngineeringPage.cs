namespace PortfolioApi.Models;

public class EngineeringPage
{
    public string Heading {get; set;} = null!;
    public List<Position> Positions { get; set; } = new();
}