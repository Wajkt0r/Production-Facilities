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
    public class ContractRepository(ProductionFacilitiesDbContext dbContext) : IContractRepository
    {
        public async Task AddContract(Contract contract)
        {
            dbContext.Contracts.Add(contract);
            await Commit();
        }

        public async Task<IEnumerable<Domain.Entities.Contract>> GetContracts()
            => await dbContext.Contracts.ToListAsync();
        

        public async Task<double> GetAreasTakenForProductionFacility(string productionFacilityCode)
            => await dbContext.Contracts.Include(c => c.EquipmentType)
            .Where(c => c.ProductionFacilityCode == productionFacilityCode)
            .Select(c => c.EquipmentType.Area * c.UnitsNumber)
            .SumAsync();

        private async Task Commit() => await dbContext.SaveChangesAsync();
    }
}
