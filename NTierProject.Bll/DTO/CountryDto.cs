using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Bll.DTO
{
    public class CountryDto
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public List<City> Cities { get; set; }
    }
}
