using NTierProject.Shared.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Shared.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public List<Company> Companies { get; set; }
    }
}
