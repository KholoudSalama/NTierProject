using NTierProject.Shared.Entities;
using NTierProject.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Bll.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Code { get; set; }
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }
        public EmploymentStatus Status { get; set; } = EmploymentStatus.Active;
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}
