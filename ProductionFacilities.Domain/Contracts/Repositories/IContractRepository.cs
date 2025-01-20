using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionFacilities.Domain.Contracts.Repositories
{
    public interface IContractRepository
    {
        Task<IEnumerable<Domain.Entities.Contract>> GetContracts();
        Task AddContract(Domain.Entities.Contract contract);
        Task<double> GetAreasTakenForProductionFacility(string productionFacilityCode);
    }
}
