namespace Solita.Bike.Shared.Dtos;

public class StationInfo
{
    public string? Id { get; set; }
    public Dictionary<Localization, string?>? Name{ get; set; }
    public Dictionary<Localization, string?>? Address { get; set; }
    public Dictionary<Localization, string?>? City { get; set; }
    public string? Operator { get; set; }
    public int Capacity { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}
