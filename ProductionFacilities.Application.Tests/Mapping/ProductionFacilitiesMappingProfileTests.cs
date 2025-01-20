using Xunit;
using ProductionFacilities.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductionFacilities.Application.DTOs;
using FluentAssertions;

namespace ProductionFacilities.Application.Mapping.Tests
{
    public class ProductionFacilitiesMappingProfileTests
    {
        [Fact()]
        public void MappingProfile_ShouldMapContractToContractDto()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new ProductionFacilitiesMappingProfile()));

            var mapper = configuration.CreateMapper();

            var contract = new Domain.Entities.Contract { ProductionFacilityCode = "F001", EquipmentTypeCode = "E001", UnitsNumber = 10 };
            var dto = new ContractDto("F001", "E001", 10);

            var result = mapper.Map<ContractDto>(contract);

            result.Should().NotBeNull();
            result.ProductionFacilityCode.Should().Be(dto.ProductionFacilityCode);
            result.EquipmentTypeCode.Should().Be(dto.EquipmentTypeCode);
            result.UnitsNumber.Should().Be(dto.UnitsNumber);
        }

        [Fact()]
        public void MappingProfile_ShouldMapContractDtoToContract()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new ProductionFacilitiesMappingProfile()));

            var mapper = configuration.CreateMapper();

            var dto = new ContractDto("F001", "E001", 10);
            var contract = new Domain.Entities.Contract { ProductionFacilityCode = "F001", EquipmentTypeCode = "E001", UnitsNumber = 10 };

            var result = mapper.Map<Domain.Entities.Contract>(dto);

            result.Should().NotBeNull();
            result.ProductionFacilityCode.Should().Be(contract.ProductionFacilityCode);
            result.EquipmentTypeCode.Should().Be(contract.EquipmentTypeCode);
            result.UnitsNumber.Should().Be(contract.UnitsNumber);
        }
    }
}