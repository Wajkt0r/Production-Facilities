using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionFacilities.Domain.Entities;

namespace ProductionFacilities.Domain.Contracts.Repositories
{
    public interface IProductionFacilitiesRepository
    {
        Task<double> GetProductionFacilityArea(string productioNFacilitityCode);
        Task<ProductionFacility> GetByCode(string productionFacilityCode);
    }
}
