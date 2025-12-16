using NTierProject.Bll.DTO;
using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Bll.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagedResult<EmployeeDto>> GetPagedEmployeesAsync(int pageIndex, int pageSize);
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<int> AddAsync(EmployeeDto dto);
        Task<int> UpdateAsync(EmployeeDto dto);
        Task DeleteAsync(int id);

    }
}
