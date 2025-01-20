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
    public class EquipmentTypeRepository(ProductionFacilitiesDbContext dbContext) : IEquipmentTypeRepository
    {
        public async Task<double> GetEquipmentTypeArea(string equipmentTypeCode)
            => await dbContext.EquipmentTypes.Where(e => e.Code == equipmentTypeCode).Select(e => e.Area).FirstOrDefaultAsync();

        public async Task<EquipmentType> GetByCode(string equipmentTypeCode)
            => await dbContext.EquipmentTypes.FirstOrDefaultAsync(e => e.Code == equipmentTypeCode);
    }
}
