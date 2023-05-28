using System.ComponentModel.DataAnnotations;

namespace Solita.Bike.Shared.Models;

public class DataImport
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? FileName { get; set; }
    [Required]
    public DateTime ImportDate { get; set; } = DateTime.UtcNow;
}