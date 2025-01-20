using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionFacilities.Domain.Entities
{
    public class Contract
    {
        public int Id { get; set; }

        public ProductionFacility ProductionFacility { get; set; } = default!;
        public string ProductionFacilityCode { get; set; } = default!;

        public EquipmentType EquipmentType { get; set; } = default!;
        public string EquipmentTypeCode { get; set; } = default!;

        public int UnitsNumber { get; set; }
    }
}
