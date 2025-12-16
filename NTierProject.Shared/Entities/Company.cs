using NTierProject.Shared.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Shared.Entities
{
   public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

    }
}
