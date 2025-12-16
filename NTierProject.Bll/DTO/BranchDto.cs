using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Bll.DTO
{
    public class BranchDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public List<Department> Departments { get; set; }
    }
}
