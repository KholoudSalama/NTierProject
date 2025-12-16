using NTierProject.Shared.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Shared.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
