namespace Warehouse.Models.Dtos;

public class CoilStatisticsDto
{
    public long CountAdded { get; set; }
    public long CountDeleted { get; set; }
    public double AverageLenght { get; set; }
    public double AverageWeight { get; set; }
    public long MaxLenght { get; set; }
    public long MinLenght { get; set; }
    public long MaxWeight { get; set; }
    public long MinWeight { get; set; }
    public long TotalWeight { get; set; }
    
    public TimeSpan MaxStorageDuration { get; set; }
    public TimeSpan MinStorageDuration { get; set; }
    
    public DateTime MinCoilsDay { get; set; }
    public DateTime MaxCoilsDay { get; set; }
    
    public DateTime MinWeightDay { get; set; }
    public DateTime MaxWeightDay { get; set; }
}