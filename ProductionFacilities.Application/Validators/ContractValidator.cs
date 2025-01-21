using ProductionFacilities.Application.Common;
using ProductionFacilities.Application.DTOs;
using ProductionFacilities.Application.Validators.Interfaces;
using ProductionFacilities.Domain.Contracts.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace ProductionFacilities.Application.Validators
{
    public class ContractValidator(IProductionFacilitiesRepository productionFacilitiesRepository, IEquipmentTypeRepository equipmentTypeRepository) : IContractValidator
    {
        public async Task<CommandResult> ValidateContract(ContractDto contractDto)
        {
            if (contractDto.UnitsNumber < 1) return CommandResult.Failure("Units number must be at least 1");

            var facility = await productionFacilitiesRepository.GetByCode(contractDto.ProductionFacilityCode);

            if (facility == null) return CommandResult.Failure("The facility with this code doesn't exist");

            var equipment = await equipmentTypeRepository.GetByCode(contractDto.EquipmentTypeCode);

            if (equipment == null) return CommandResult.Failure("The equipment with this code doesn't exist");

            return CommandResult.Success();
        }
    }
}
