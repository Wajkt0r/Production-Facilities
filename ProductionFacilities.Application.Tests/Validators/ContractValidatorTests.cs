using Xunit;
using ProductionFacilities.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProductionFacilities.Domain.Contracts.Repositories;
using ProductionFacilities.Application.DTOs;
using FluentAssertions;

namespace ProductionFacilities.Application.Validators.Tests
{
    public class ContractValidatorTests
    {
        [Fact()]
        public async Task Validate_WithValidContract_ShouldNotHaveValidationError()
        {
            var productionRepository = new Mock<IProductionFacilitiesRepository>();
            var equipmentRepository = new Mock<IEquipmentTypeRepository>();

            productionRepository
                .Setup(repo => repo.GetByCode("F001"))
                .ReturnsAsync(new Domain.Entities.ProductionFacility() { Code = "F001", Name = "Facility", StandardAreaForEquipment = 10 });

            equipmentRepository
                .Setup(repo => repo.GetByCode("E001"))
                .ReturnsAsync(new Domain.Entities.EquipmentType() { Code = "E001", Name = "Equipment", Area = 10 });

            var validator = new ContractValidator(productionRepository.Object, equipmentRepository.Object);

            var contractDto = new ContractDto("F001", "E001", 10);

            var result = await validator.ValidateContract(contractDto);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Be("Success");
        }

        [Fact()]
        public async Task ValidateContract_WithNonExistentEquipment_ShouldReturnEquipmentCodeError()
        {
            var productionRepository = new Mock<IProductionFacilitiesRepository>();
            var equipmentRepository = new Mock<IEquipmentTypeRepository>();

            productionRepository
                .Setup(repo => repo.GetByCode("F001"))
                .ReturnsAsync(new Domain.Entities.ProductionFacility() { Code = "F001", Name = "Facility", StandardAreaForEquipment = 10 });

            equipmentRepository
                .Setup(repo => repo.GetByCode("E001"))
                .ReturnsAsync((Domain.Entities.EquipmentType?)null);

            var validator = new ContractValidator(productionRepository.Object, equipmentRepository.Object);

            var contractDto = new ContractDto("F001", "E001", 10);

            var result = await validator.ValidateContract(contractDto);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("The equipment with this code doesn't exist");
        }

        [Fact()]
        public async Task ValidateContract_WithNonExistentFacility_ShouldReturnFacilityCodeError()
        {
            var productionRepository = new Mock<IProductionFacilitiesRepository>();
            var equipmentRepository = new Mock<IEquipmentTypeRepository>();

            productionRepository
                .Setup(repo => repo.GetByCode("F001"))
                .ReturnsAsync((Domain.Entities.ProductionFacility?)null);

            equipmentRepository
                .Setup(repo => repo.GetByCode("E001"))
                .ReturnsAsync(new Domain.Entities.EquipmentType() { Code = "E001", Name = "Equipment", Area = 10 });

            var validator = new ContractValidator(productionRepository.Object, equipmentRepository.Object);

            var contractDto = new ContractDto("F001", "E001", 10);

            var result = await validator.ValidateContract(contractDto);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("The facility with this code doesn't exist");
        }
    }
}