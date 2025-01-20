using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionFacilities.Application.Common;
using ProductionFacilities.Application.DTOs;

namespace ProductionFacilities.Application.Validators.Interfaces
{
    public interface IContractValidator
    {
        Task<CommandResult> ValidateContract(ContractDto contractDto);
    }
}
