using NTierProject.Dal.Repository;
using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Dal.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IGenericRepository<Company> Companies { get; }
        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Branch> Branchs { get; }
        IGenericRepository<JobTitle> JobTitles { get; }
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<City> Cities { get; }

        Task ExecutationTransactionAsync(Func<Task> action);
        Task<int> SaveRepositoryChangesAsync();
    }
}
