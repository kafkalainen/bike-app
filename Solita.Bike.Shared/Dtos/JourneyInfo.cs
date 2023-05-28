namespace Solita.Bike.Shared.Dtos;

public class JourneyInfo
{
    public string? DepartureStation { get; set; }
    public string? ReturnStation { get; set; }
    public double CoveredDistanceInKilometers { get; set; }
    public int DurationInMinutes { get; set; }
}