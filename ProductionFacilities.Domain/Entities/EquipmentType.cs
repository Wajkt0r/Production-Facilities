using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionFacilities.Domain.Entities
{
    public class EquipmentType
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public double Area { get; set; }
    }
}
