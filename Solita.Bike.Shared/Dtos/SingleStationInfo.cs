namespace Solita.Bike.Shared.Dtos;

public class SingleStationInfo
{
    public List<Localization> Name { get; set; } = new ();
    public List<Localization> Address { get; set; } = new ();
    public ulong StartJourneyTotal { get; set; }
    public ulong EndJourneyTotal { get; set; }
}