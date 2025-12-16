using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Dal.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T , bool>> predicate);
        Task<PagedResult<T>> GetPageAsync(int pageindex, int pagesize, 
            Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T> , IOrderedQueryable<T>>? orderBy = null

            );
    }
}
