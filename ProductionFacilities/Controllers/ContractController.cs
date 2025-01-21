using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProductionFacilities.Application;
using ProductionFacilities.Application.DTOs;
using ProductionFacilities.Domain.Contracts.Services;
using ProductionFacilities.Domain.Entities;
using ProductionFacilities.Infrastructure.Persistence;

namespace ProductionFacilities.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController(IContractService contractService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await contractService.GetContracts();
            return Ok(data);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]ContractDto entity)
        {
            var result = await contractService.AddContract(entity);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return StatusCode(result.StatusCode, result);
        }
    }

}
