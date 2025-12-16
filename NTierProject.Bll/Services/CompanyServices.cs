using AutoMapper;
using NTierProject.Bll.DTO;
using NTierProject.Bll.Interfaces;
using NTierProject.Dal.UnitOfWorks;
using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Bll.Services
{
    public class CompanyServices : ICompanyServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CompanyServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _unitOfWork.Companies.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }
        public async Task<CompanyDto?> GetCompanyByIdAsync(int id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            return _mapper.Map<CompanyDto?>(company);
        }
        public async Task<int> AddCompanyAsync(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.SaveRepositoryChangesAsync();
            return company.Id;
        }
        public async Task<int> UpdateCompanyAsync(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            _unitOfWork.Companies.Update(company);
            await _unitOfWork.SaveRepositoryChangesAsync();
            return company.Id;
        }
        public async Task DeleteCompanyAsync(int id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if (company != null)
            {
                _unitOfWork.Companies.Delete(company);
                await _unitOfWork.SaveRepositoryChangesAsync();
            }
        }
    }
}
