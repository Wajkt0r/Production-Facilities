using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionFacilities.Application.DTOs
{
    public record ContractDto(string ProductionFacilityCode, string EquipmentTypeCode, int UnitsNumber);
}
