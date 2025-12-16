using NTierProject.Dal.Data;
using NTierProject.Dal.Repository;
using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Dal.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IGenericRepository<Company> Companies { get; }
        public IGenericRepository<Employee> Employees { get; }
        public IGenericRepository<Department> Departments { get; }
        public IGenericRepository<Branch> Branchs { get; }
        public IGenericRepository<JobTitle> JobTitles { get; }
        public IGenericRepository<Country> Countries { get; }
        public IGenericRepository<City> Cities { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Companies = new GenericRepository<Company>(_context, _context.Companies);
            Employees = new GenericRepository<Employee>(_context, _context.Employees);
            Departments = new GenericRepository<Department>(_context, _context.Departments);
            Branchs = new GenericRepository<Branch>(_context, _context.Branches);
            JobTitles = new GenericRepository<JobTitle>(_context, _context.JobTitles);
            Countries = new GenericRepository<Country>(_context, _context.Countries);
            Cities = new GenericRepository<City>(_context, _context.Cities);
        }

        public async Task<int> SaveRepositoryChangesAsync()=> await _context.SaveChangesAsync();

        public async Task ExecutationTransactionAsync(Func<Task> action)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

    }
}
