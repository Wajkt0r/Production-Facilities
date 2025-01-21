using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductionFacilities.Application.Common;
using ProductionFacilities.Application.DTOs;
using ProductionFacilities.Application.Validators.Interfaces;
using ProductionFacilities.Domain.Contracts.Repositories;
using ProductionFacilities.Domain.Contracts.Services;

namespace ProductionFacilities.Application.Services
{
    public class ContractService(IContractRepository contractRepository, IProductionFacilitiesRepository productionFacilitiesRepository, IEquipmentTypeRepository equipmentTypeRepository, IContractValidator contractValidator, IMapper mapper) : IContractService
    {
        public async Task<IEnumerable<ContractDto>> GetContracts()
        {
            var contracts = await contractRepository.GetContracts();
            var contractsDto = mapper.Map<IEnumerable<ContractDto>>(contracts);
            return contractsDto;
        }

        public async Task<CommandResult> AddContract(ContractDto contractDto)
        {        
            var validationResult = await contractValidator.ValidateContract(contractDto);

            if (!validationResult.IsSuccess) return CommandResult.Failure(validationResult.Message);

            if (await CalculateFacilityAreaLeft(contractDto) < 0) return CommandResult.Failure("No space for this contract");

            var contract = new Domain.Entities.Contract()
            {
                ProductionFacilityCode = contractDto.ProductionFacilityCode,
                EquipmentTypeCode = contractDto.EquipmentTypeCode,
                UnitsNumber = contractDto.UnitsNumber,
            };
            await contractRepository.AddContract(contract);

            return CommandResult.Success($"Succesfully added equipment type {contractDto.EquipmentTypeCode} to the facility {contractDto.ProductionFacilityCode}");
        }

        private async Task<double> CalculateFacilityAreaLeft(ContractDto contractDto)
        {
            var productionFacalityArea = await productionFacilitiesRepository.GetProductionFacilityArea(contractDto.ProductionFacilityCode);
            var contractsArea = await contractRepository.GetAreasTakenForProductionFacility(contractDto.ProductionFacilityCode);
            var equipmentArea = (await equipmentTypeRepository.GetByCode(contractDto.EquipmentTypeCode)).Area * contractDto.UnitsNumber;

            return productionFacalityArea - (contractsArea + equipmentArea);
        }
    }
}

