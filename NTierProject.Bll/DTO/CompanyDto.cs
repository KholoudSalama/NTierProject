using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Bll.DTO
{
    public class CompanyDto
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
