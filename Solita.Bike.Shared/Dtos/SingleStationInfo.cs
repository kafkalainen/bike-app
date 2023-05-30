namespace Solita.Bike.Shared.Dtos;

public class SingleStationInfo
{
    public Dictionary<Localization, string?> Name { get; set; } = new ();
    public Dictionary<Localization, string?> Address { get; set; } = new ();
    public ulong StartJourneyTotal { get; set; }
    public ulong EndJourneyTotal { get; set; }
}