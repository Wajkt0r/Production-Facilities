using Microsoft.Extensions.DependencyInjection;
using ProductionFacilities.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductionFacilities.Domain.Contracts.Repositories;
using ProductionFacilities.Infrastructure.Repositories;

namespace ProductionFacilities.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProductionFacilities");
            services.AddDbContext<ProductionFacilitiesDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IProductionFacilitiesRepository, ProductionFacilitiesRepository>();
            services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();
        }
    }
}
