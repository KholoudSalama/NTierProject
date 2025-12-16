using NTierProject.Bll.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Bll.Interfaces
{
    public interface ICompanyServices
    {
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
        Task<CompanyDto?> GetCompanyByIdAsync(int id);
        Task<int> AddCompanyAsync(CompanyDto companyDto);
        Task<int> UpdateCompanyAsync(CompanyDto companyDto);
        Task DeleteCompanyAsync(int id);


    }
}
