using Microsoft.EntityFrameworkCore;
using Warehouse.Models.Domain;

namespace Warehouse.Data;

public class WarehouseDbContext : DbContext
{
    public DbSet<Coil> Coils { get; set; }
    
    public WarehouseDbContext(DbContextOptions options) : base(options) { }
}