using Microsoft.EntityFrameworkCore;
using NTierProject.Dal.Data;
using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Dal.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly AppDbContext _context;
        public readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<PagedResult<T>> GetPageAsync(int pageindex, int pagesize, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }
            var totalCount =await query.CountAsync();
            if(orderBy is not null)
                query = orderBy(query);

            var result = await query.Skip((pageindex - 1) * pagesize).Take(pagesize).ToListAsync();

            return new PagedResult<T>
            {
                Result = result,
                TotalCount = totalCount,
                PageIndex = pageindex,
                PageSize = pagesize
            };
        }
      
        public void Update(T entity) =>  _dbSet.Update(entity);
       
    }
}
