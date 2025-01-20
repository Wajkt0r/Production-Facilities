using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProductionFacilities.Application.Mapping;
using ProductionFacilities.Application.Services;
using ProductionFacilities.Application.Validators;
using ProductionFacilities.Application.Validators.Interfaces;
using ProductionFacilities.Domain.Contracts.Services;

namespace ProductionFacilities.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductionFacilitiesMappingProfile));

            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IContractValidator, ContractValidator>();
        }
    }
}
