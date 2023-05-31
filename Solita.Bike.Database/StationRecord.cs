using CsvHelper.Configuration.Attributes;

namespace Solita.Bike.Database
{
    public class StationRecord
    {
        [Name("FID")]
        public ulong Fid { get; set; }

        [Name("ID")]
        public string? Id { get; set; }

        [Name("Nimi")]
        public string? NameInFinnish { get; set; }

        [Name("Namn")]
        public string? NameInSwedish { get; set; }

        [Name("Name")]
        public string? NameInEnglish { get; set; }

        [Name("Osoite")]
        public string? AddressInFinnish { get; set; }

        [Name("Adress")]
        public string? AddressInSwedish { get; set; }

        [Name("Kaupunki")]
        public string? CityInFinnish { get; set; }

        [Name("Stad")]
        public string? CityInSwedish { get; set; }

        [Name("Operaattor")]
        public string? Operator { get; set; }

        [Name("Kapasiteet")]
        public int Capacity { get; set; }

        [Name("x")]
        public double X { get; set; }

        [Name("y")]
        public double Y { get; set; }
    }
}