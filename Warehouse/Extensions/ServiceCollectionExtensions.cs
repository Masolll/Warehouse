using Microsoft.EntityFrameworkCore;
using Warehouse.Data;

namespace Warehouse.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWarehouseDb(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<WarehouseDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        return serviceCollection;
    }
}