using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductionFacilities.Application.DTOs;
using ProductionFacilities.Domain.Entities;

namespace ProductionFacilities.Application.Mapping
{
    public class ProductionFacilitiesMappingProfile : Profile
    {
        public ProductionFacilitiesMappingProfile()
        {
            CreateMap<Domain.Entities.Contract, ContractDto>()
                .ReverseMap();

        }
    }
}
