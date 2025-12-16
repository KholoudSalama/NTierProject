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
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<EmployeeDto>> GetPagedEmployeesAsync(int pageIndex, int pageSize)
        {
            var pagedEmployees = await _unitOfWork.Employees.GetPageAsync(pageIndex, pageSize);
            var mappedEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(pagedEmployees.Result);
            return new PagedResult<EmployeeDto>
            {
                Result = mappedEmployees,
                TotalCount = pagedEmployees.TotalCount,
                PageIndex = pagedEmployees.PageIndex,
                PageSize = pagedEmployees.PageSize
            };
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                return null;
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<int> AddAsync(EmployeeDto dto)
        {
            var existingEmployees = (await _unitOfWork.Employees.FindAsync(e => e.Code == dto.Code)).Any();
            if (existingEmployees)
                throw new Exception("An employee with the same code already exists.");
            var entity =  _mapper.Map<Employee>(dto);
            await _unitOfWork.Employees.AddAsync(entity);
            await _unitOfWork.SaveRepositoryChangesAsync();
            return entity.Id;
        }

        public async Task<int> UpdateAsync(EmployeeDto dto)
        {
            var entity = await _unitOfWork.Employees.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new Exception("Employee not found.");
            _mapper.Map(dto, entity);
            _unitOfWork.Employees.Update(entity);
            await _unitOfWork.SaveRepositoryChangesAsync();
            return entity.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Employees.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("Employee not found.");
            _unitOfWork.Employees.Delete(entity);
            await _unitOfWork.SaveRepositoryChangesAsync();
        }

    }
}
