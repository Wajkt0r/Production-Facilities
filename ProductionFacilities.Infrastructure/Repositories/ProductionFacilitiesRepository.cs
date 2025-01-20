using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductionFacilities.Domain.Contracts.Repositories;
using ProductionFacilities.Domain.Entities;
using ProductionFacilities.Infrastructure.Persistence;

namespace ProductionFacilities.Infrastructure.Repositories
{
    public class ProductionFacilitiesRepository(ProductionFacilitiesDbContext dbContext) : IProductionFacilitiesRepository
    {
        public async Task<double> GetProductionFacilityArea(string productioNFacilitityCode)
            => await dbContext.ProductionFacilities.Where(f => f.Code == productioNFacilitityCode).Select(f => f.StandardAreaForEquipment).FirstOrDefaultAsync();

        public async Task<ProductionFacility> GetByCode(string productionFacilityCode)
            => await dbContext.ProductionFacilities.FirstOrDefaultAsync(p => p.Code == productionFacilityCode);
               
    }
}
