using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models.Dtos;

public class CreateCoilDto
{
    [Range(0, long.MaxValue, ErrorMessage = "Lenght must be non-negative")]
    public required long Lenght { get; set; } 
    
    [Range(0, long.MaxValue, ErrorMessage = "Weight must be non-negative")]
    public required long Weight { get; set; }
}