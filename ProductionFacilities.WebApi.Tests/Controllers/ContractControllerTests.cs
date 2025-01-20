using Xunit;
using ProductionFacilities.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionFacilities.Application.Services;
using Moq;
using ProductionFacilities.Domain.Contracts.Services;
using ProductionFacilities.Application.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ProductionFacilities.Application.Common;

namespace ProductionFacilities.WebApi.Controllers.Tests
{
    public class ContractControllerTests
    {
        [Fact()]
        public async void GetAll_ShouldReturnOkWithContracts()
        {
            var contractService = new Mock<IContractService>();
            var contracts = new List<ContractDto>
            {
                new ContractDto("F001", "E001", 10),
                new ContractDto("F002", "E002", 20)
            };
            contractService
                .Setup(s => s.GetContracts())
                .ReturnsAsync(contracts);

            var controller = new ContractController(contractService.Object);

            var result = await controller.GetAll();


            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            var responseData = okResult.Value as IEnumerable<ContractDto>;
            responseData.Should().NotBeNull();
            responseData.Should().HaveCount(2);
            
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithEmptyList()
        {
            var contractService = new Mock<IContractService>();
            contractService.Setup(service => service.GetContracts())
                           .ReturnsAsync(new List<ContractDto>());

            var controller = new ContractController(contractService.Object);

            var result = await controller.GetAll();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            var responseData = okResult.Value as IEnumerable<ContractDto>;
            responseData.Should().NotBeNull();
            responseData!.Should().BeEmpty();
        }

        [Fact]
        public async Task AddProductionFacility_ShouldReturnOkWhenContractAddedSuccessfully()
        {
            var contractService = new Mock<IContractService>();
            var contractDto = new ContractDto("F001", "E001", 10);

            contractService.Setup(service => service.AddContract(contractDto))
                           .ReturnsAsync(CommandResult.Success("Contract added successfully"));

            var controller = new ContractController(contractService.Object);

            var result = await controller.AddProductionFacility(contractDto);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);

            var responseData = okResult.Value as CommandResult;
            responseData.Should().NotBeNull();
            responseData!.IsSuccess.Should().BeTrue();
            responseData.Message.Should().Be("Contract added successfully");
        }

        [Fact]
        public async Task AddProductionFacility_ShouldReturnBadRequestWhenContractNotAdded()
        {
            var contractService = new Mock<IContractService>();
            var contractDto = new ContractDto("F001", "E001", 10);

            contractService.Setup(service => service.AddContract(contractDto))
                           .ReturnsAsync(CommandResult.Failure("No space for this contract", 400));

            var controller = new ContractController(contractService.Object);

            var result = await controller.AddProductionFacility(contractDto);

            var statusCodeResult = result as ObjectResult;
            statusCodeResult.Should().NotBeNull();
            statusCodeResult!.StatusCode.Should().Be(400);
            statusCodeResult.Value.Should().Be("No space for this contract");
        }

        [Fact]
        public async Task AddProductionFacility_ShouldReturnInternalServerErrorWhenUnexpectedErrorOccurs()
        {
            var contractService = new Mock<IContractService>();
            var contractDto = new ContractDto("F001", "E001", 10);

            contractService.Setup(service => service.AddContract(contractDto))
                           .ReturnsAsync(CommandResult.Failure("An unexpected error occurred", 500));

            var controller = new ContractController(contractService.Object);

            var result = await controller.AddProductionFacility(contractDto);

            var statusCodeResult = result as ObjectResult;
            statusCodeResult.Should().NotBeNull();
            statusCodeResult!.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An unexpected error occurred");
        }
    }
}