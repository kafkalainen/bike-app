namespace Solita.Bike.Database.Data
{
    using CsvHelper.Configuration.Attributes;
    public class JourneyRecord
    {
        [Name("Departure")]
        public DateTime Departure { get; set; }

        [Name("Return")]
        public DateTime Return { get; set; }

        [Name("Departure station id")]
        public string? DepartureStationId { get; set; }

        [Name("Departure station name")]
        public string? DepartureStationName { get; set; }

        [Name("Return station id")]
        public string? ReturnStationId { get; set; }

        [Name("Return station name")]
        public string? ReturnStationName { get; set; }

        [Name("Covered distance (m)")]
        public double? CoveredDistance { get; set; }

        [Name("Duration (sec.)")]
        public int Duration { get; set; }
    }
}