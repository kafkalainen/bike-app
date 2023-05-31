using System.ComponentModel.DataAnnotations;

namespace Solita.Bike.Models
{
    public class Station
    {
        [Key]
        public ulong Fid { get; set; }
        
        [Required]
        public string? Id { get; set; }
        
        [Required]
        public string? NameInFinnish { get; set; }
        
        [Required]
        public string? NameInSwedish { get; set; }
        
        [Required]
        public string? NameInEnglish { get; set; }
        
        [Required]
        public string? AddressInFinnish { get; set; }
        
        [Required]
        public string? AddressInSwedish { get; set; }
        
        [Required]
        public string? CityInFinnish { get; set; }
        
        [Required]
        public string? CityInSwedish { get; set; }
        
        [Required]
        public string? Operator { get; set; }
        
        [Required]
        public int Capacity { get; set; }
        
        [Required]
        public double X { get; set; }
        
        [Required]
        public double Y { get; set; }

        public IEnumerable<Journey> DepartureJourneys { get; set; } = new List<Journey>();
        public IEnumerable<Journey> ReturnJourneys { get; set; } = new List<Journey>();
    }
}
