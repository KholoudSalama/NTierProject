using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Bll.DTO
{
    public class DepartmentDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
