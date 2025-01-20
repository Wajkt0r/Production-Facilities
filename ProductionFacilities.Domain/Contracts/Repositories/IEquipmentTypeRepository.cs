using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionFacilities.Domain.Entities;

namespace ProductionFacilities.Domain.Contracts.Repositories
{
    public interface IEquipmentTypeRepository
    {
        Task<double> GetEquipmentTypeArea(string equipmentTypeCode);
        Task<EquipmentType> GetByCode(string equipmentTypeCode);
    }
}
