namespace Warehouse.Models.Domain;

public class Coil
{
    public Guid Id { get; set; }
    public long Lenght { get; set; } 
    public long Weight { get; set; }
    public  DateTime AddedDate { get; set; }
    public DateTime? RemovedDate { get; set; } = null;
}