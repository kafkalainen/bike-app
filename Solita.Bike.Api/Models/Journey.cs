using System.ComponentModel.DataAnnotations;

namespace Solita.Bike.Api.Models;

public class Journey
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Departure { get; set; }

    [Required]
    public DateTime Return { get; set; }

    [Required]
    public string? DepartureStationId { get; set; }

    [Required]
    public string? DepartureStationName { get; set; }

    [Required]
    public string? ReturnStationId { get; set; }

    [Required]
    public string? ReturnStationName { get; set; }

    [Required]
    [Range(10, int.MaxValue, ErrorMessage = "Covered distance must be at least 10 meters.")]
    public int CoveredDistanceInMeters { get; set; }

    [Required]
    [Range(10, int.MaxValue, ErrorMessage = "Duration must be at least 10 seconds.")]
    public int DurationInSeconds { get; set; }
}