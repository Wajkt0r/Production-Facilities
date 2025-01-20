using Xunit;
using ProductionFacilities.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProductionFacilities.Domain.Contracts.Repositories;
using AutoMapper;
using ProductionFacilities.Application.DTOs;
using ProductionFacilities.Application.Mapping;
using FluentAssertions;
using ProductionFacilities.Application.Common;
using ProductionFacilities.Application.Validators.Interfaces;
using ProductionFacilities.Domain.Entities;

namespace ProductionFacilities.Application.Services.Tests
{
    public class ContractServiceTests
    {
        [Fact()]
        public async void GetContracts_ShouldReturnMappedContracts()
        {
            var contractRepository = new Mock<IContractRepository>();

            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new ProductionFacilitiesMappingProfile()));

            var mapper = configuration.CreateMapper();

            var contractEntities = new List<Domain.Entities.Contract>
            {
                new Domain.Entities.Contract { ProductionFacilityCode = "F001", EquipmentTypeCode = "E001", UnitsNumber = 10 },
                new Domain.Entities.Contract { ProductionFacilityCode = "F002", EquipmentTypeCode = "E002", UnitsNumber = 20 }
            };

            contractRepository.Setup(repo => repo.GetContracts())
                                .ReturnsAsync(contractEntities);

            var service = new ContractService(contractRepository.Object, null!, null!, null!, mapper);

            var result = await service.GetContracts();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().ProductionFacilityCode.Should().Be("F001");
            result.Last().EquipmentTypeCode.Should().Be("E002");
        }

        [Fact]
        public async Task AddContract_ShouldAddContract_WhenValidationPassesAndSpaceIsAvailable()
        {
            var contractValidator = new Mock<IContractValidator>();
            var contractRepository = new Mock<IContractRepository>();
            var productionFacilitiesRepository = new Mock<IProductionFacilitiesRepository>();
            var equipmentTypeRepository = new Mock<IEquipmentTypeRepository>();
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new ProductionFacilitiesMappingProfile()));
            var mapper = configuration.CreateMapper();

            contractValidator
                .Setup(v => v.ValidateContract(It.IsAny<ContractDto>()))
                .ReturnsAsync(CommandResult.Success());

            productionFacilitiesRepository
                .Setup(repo => repo.GetProductionFacilityArea("F001"))
                .ReturnsAsync(100);

            contractRepository
                .Setup(repo => repo.GetAreasTakenForProductionFacility("F001"))
                .ReturnsAsync(20);

            equipmentTypeRepository
                .Setup(repo => repo.GetByCode("E001"))
                .ReturnsAsync(new EquipmentType { Area = 10 });

            var contractDto = new ContractDto("F001", "E001", 5);            

            var service = new ContractService(
                contractRepository.Object,
                productionFacilitiesRepository.Object,
                equipmentTypeRepository.Object,
                contractValidator.Object,
                mapper
            );

            var result = await service.AddContract(contractDto);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Be("Succesfully added equipment type E001 to the facility F001");
        }

        [Fact]
        public async Task AddContract_ShouldFail_WhenValidationFails()
        {
            var contractValidator = new Mock<IContractValidator>();
            var contractRepository = new Mock<IContractRepository>();
            var productionFacilitiesRepository = new Mock<IProductionFacilitiesRepository>();
            var equipmentTypeRepository = new Mock<IEquipmentTypeRepository>();
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new ProductionFacilitiesMappingProfile()));
            var mapper = configuration.CreateMapper();

            contractValidator
                .Setup(v => v.ValidateContract(It.IsAny<ContractDto>()))
                .ReturnsAsync(CommandResult.Failure("Validation failed"));

            var contractDto = new ContractDto("F001", "E001", 5);

            var service = new ContractService(
                contractRepository.Object,
                productionFacilitiesRepository.Object,
                equipmentTypeRepository.Object,
                contractValidator.Object,
                mapper
            );

            var result = await service.AddContract(contractDto);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Validation failed");
        }

        [Fact]
        public async Task AddContract_ShouldFail_WhenNoSpaceIsAvailable()
        {
            var contractValidator = new Mock<IContractValidator>();
            var contractRepository = new Mock<IContractRepository>();
            var productionFacilitiesRepository = new Mock<IProductionFacilitiesRepository>();
            var equipmentTypeRepository = new Mock<IEquipmentTypeRepository>();
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new ProductionFacilitiesMappingProfile()));
            var mapper = configuration.CreateMapper();

            contractValidator
                .Setup(v => v.ValidateContract(It.IsAny<ContractDto>()))
                .ReturnsAsync(CommandResult.Success());

            productionFacilitiesRepository
                .Setup(repo => repo.GetProductionFacilityArea("F001"))
                .ReturnsAsync(50);

            contractRepository
                .Setup(repo => repo.GetAreasTakenForProductionFacility("F001"))
                .ReturnsAsync(45);

            equipmentTypeRepository
                .Setup(repo => repo.GetByCode("E001"))
                .ReturnsAsync(new EquipmentType { Area = 10 });

            var contractDto = new ContractDto("F001", "E001", 1);

            var service = new ContractService(
                contractRepository.Object,
                productionFacilitiesRepository.Object,
                equipmentTypeRepository.Object,
                contractValidator.Object,
                mapper
            );

            var result = await service.AddContract(contractDto);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("No space for this contract");
        }
    }
}