using Warehouse.Data;
using Warehouse.Models.Domain;
using Warehouse.Models.Dtos;

namespace Warehouse.Services;

public class CoilService
{
    private readonly WarehouseDbContext _context;

    public CoilService(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task<Coil> CreateAsync(CreateCoilDto coilDto)
    {
        var coil = new Coil
        {
            Id = Guid.NewGuid(),
            Weight = coilDto.Weight,
            Lenght = coilDto.Lenght,
            AddedDate = DateTime.UtcNow
        };
        _context.Coils.Add(coil);
        await _context.SaveChangesAsync();
        
        return coil;
    }

    public async Task<Coil> MarkAsDeletedAsync(Guid id)
    {
        var coil = await _context.Coils.FindAsync(id);
        
        if (coil is null)
            throw new ArgumentException("Coil with given id not found");

        if (coil.RemovedDate.HasValue)
            throw new ArgumentException("Coil already delete");
        
        coil.RemovedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return coil;
    }

    public IEnumerable<Coil> GetFiltered(CoilFilterDto filterDto) 
        => _context.Coils.Where(e => (filterDto.Id.HasValue && e.Id == filterDto.Id) || !filterDto.Id.HasValue)
            .Where(e => (filterDto.Lenght.HasValue && e.Lenght == filterDto.Lenght) || !filterDto.Lenght.HasValue)
            .Where(e => (filterDto.Weight.HasValue && e.Weight == filterDto.Weight) || !filterDto.Weight.HasValue)
            .Where(e => (filterDto.AddedDate.HasValue && e.AddedDate == filterDto.AddedDate) || !filterDto.AddedDate.HasValue)
            .Where(e => (filterDto.RemovedDate.HasValue && e.RemovedDate == filterDto.RemovedDate) || !filterDto.RemovedDate.HasValue);
}