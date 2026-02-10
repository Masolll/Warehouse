using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models.Dtos;

public class CreateCoilDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Lenght must be non-negative")]
    public required int Lenght { get; set; } 
    
    [Range(0, int.MaxValue, ErrorMessage = "Weight must be non-negative")]
    public required int Weight { get; set; }
}