namespace PortfolioApi.Models;

public class Position
{
    public string Period { get; set; } = null!;
    public string? Company { get; set; }
    public string Role { get; set; } = null!;
    public string Responsibilities { get; set; } = null!;
    public string? TechStack { get; set; }
}