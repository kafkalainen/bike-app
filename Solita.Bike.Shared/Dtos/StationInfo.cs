namespace Solita.Bike.Shared.Dtos;

public class StationInfo
{
    public string? Id { get; set; }
    public List<Localization>? Name{ get; set; }
    public List<Localization>? Address { get; set; }
    public List<Localization>? City { get; set; }
    public string? Operator { get; set; }
    public int Capacity { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}
