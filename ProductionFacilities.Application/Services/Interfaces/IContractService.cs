using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionFacilities.Application.Common;
using ProductionFacilities.Application.DTOs;

namespace ProductionFacilities.Domain.Contracts.Services
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDto>> GetContracts();
        Task<CommandResult> AddContract(ContractDto contractDto);
    }
}
