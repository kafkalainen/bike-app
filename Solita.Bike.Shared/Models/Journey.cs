using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solita.Bike.Shared.Models;

public class Journey
{
    [Key]
    public ulong Id { get; set; }

    [Required]
    public DateTime Departure { get; set; }

    [Required]
    public DateTime Return { get; set; }

    [Required]
    public string? DepartureStationId { get; set; }
    
    [ForeignKey(nameof(DepartureStationId))]
    public Station? DepartureStation { get; set; }

    [Required]
    public string? DepartureStationName { get; set; }

    [Required]
    public string? ReturnStationId { get; set; }
    
    [ForeignKey(nameof(ReturnStationId))]
    public Station? ReturnStation { get; set; }

    [Required]
    public string? ReturnStationName { get; set; }

    [Required]
    [Range(10, ulong.MaxValue, ErrorMessage = "Covered distance must be at least 10 meters.")]
    public double? CoveredDistanceInMeters { get; set; }

    [Required]
    [Range(10, ulong.MaxValue, ErrorMessage = "Duration must be at least 10 seconds.")]
    public int DurationInSeconds { get; set; }
}