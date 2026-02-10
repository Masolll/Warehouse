namespace Warehouse.Models.Dtos;

public class CoilFilterDto
{
    public Guid? Id { get; set; }
    public int? Weight { get; set; }
    public int? Lenght { get; set; }
    public DateTime? AddedDate { get; set; }
    public DateTime? RemovedDate { get; set; }
}