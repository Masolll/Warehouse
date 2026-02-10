using Warehouse.Data;
using Warehouse.Models.Dtos;
using Warehouse.Models.Domain;

namespace Warehouse.Services;

public class CoilStatisticsService
{
    private readonly WarehouseDbContext _context;

    public CoilStatisticsService(WarehouseDbContext context)
    {
        _context = context;
    }

    public CoilStatisticsDto GetStatistics(PeriodDto period)
    {
        var periodStart = period.PeriodStart;
        var periodEnd = period.PeriodEnd;
        
        var coilsLenghts = _context.Coils.Select(e => e.Lenght);
        var coilsWeights = _context.Coils.Select(e => e.Weight);
        var coilsDurations = GetCoilsDurations(periodStart, periodEnd);
        var (minCoilsDay, maxCoilsDay) = GetDayWithMinAndMaxCoils(period);
        var (minWeightDay, maxWeightDay) = GetDayWithMinAndMaxWeight(period);
        return new CoilStatisticsDto()
        {
            CountAdded = _context.Coils.Count(e => e.AddedDate >= periodStart && e.AddedDate < periodEnd),
            CountDeleted = _context.Coils
                .Count(e => e.RemovedDate != null && e.RemovedDate >= periodStart && e.RemovedDate < periodEnd),
            AverageLenght = coilsLenghts.DefaultIfEmpty().Average(),
            AverageWeight = coilsWeights.DefaultIfEmpty().Average(),
            MaxLenght = coilsLenghts.DefaultIfEmpty().Max(),
            MinLenght = coilsLenghts.DefaultIfEmpty().Min(),
            MaxWeight = coilsWeights.DefaultIfEmpty().Max(),
            MinWeight = coilsWeights.DefaultIfEmpty().Min(),
            TotalWeight = coilsWeights.Sum(),
            MaxStorageDuration = coilsDurations.DefaultIfEmpty().Max(), 
            MinStorageDuration = coilsDurations.DefaultIfEmpty().Min(),
            MinCoilsDay = minCoilsDay,
            MaxCoilsDay = maxCoilsDay,
            MinWeightDay = minWeightDay,
            MaxWeightDay = maxWeightDay
        };
    }
    
    private IEnumerable<TimeSpan> GetCoilsDurations(DateTime periodStart, DateTime periodEnd) 
        => _context.Coils
            .Where(e => e.AddedDate >= periodStart)
            .Where(e => e.RemovedDate != null && e.RemovedDate < periodEnd)
            .Select(e => (TimeSpan)(e.RemovedDate - e.AddedDate));

    private (DateTime MinCoilsDay, DateTime MaxCoilsDay) GetDayWithMinAndMaxCoils(PeriodDto period) 
        => GetDayWithMinAndMaxBySelector(period, coils => coils.LongCount());
    
    private (DateTime MinWeightDay, DateTime MaxWeightDay) GetDayWithMinAndMaxWeight(PeriodDto period) 
        => GetDayWithMinAndMaxBySelector(period, coils => coils.Sum(e => e.Weight));
    
    private (DateTime MinDay, DateTime MaxDay) GetDayWithMinAndMaxBySelector(
        PeriodDto period,
        Func<IEnumerable<Coil>, long> selector)
    {
        var start = period.PeriodStart;
        var end = period.PeriodEnd;
        var valuesPerDay = new Dictionary<DateTime, long>();
        for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
        {
            if (date == start.Date)
                valuesPerDay[date] = selector(GetStoragedCoils(start, date.AddDays(1)));
            else if (date == end.Date)
                valuesPerDay[date] = selector(GetStoragedCoils(date, end));
            else
                valuesPerDay[date] = selector(GetStoragedCoils(date, date.AddDays(1)));
        }
        var maxDay = valuesPerDay.MaxBy(e => e.Value).Key;
        var minDay = valuesPerDay.MinBy(e => e.Value).Key;

        return (minDay, maxDay);
    }
    
    private List<Coil> GetStoragedCoils(DateTime periodStart, DateTime periodEnd) => _context.Coils
            .Where(e => (e.AddedDate >= periodStart && e.AddedDate < periodEnd)
                        || (e.AddedDate < periodStart && (e.RemovedDate == null || e.RemovedDate >= periodEnd))
                        || (e.RemovedDate != null && (e.RemovedDate >= periodStart && e.RemovedDate < periodEnd)))
            .ToList();
}