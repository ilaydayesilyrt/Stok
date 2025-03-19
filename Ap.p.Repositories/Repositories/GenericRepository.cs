using App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace App.Repositories.Repositories
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
        protected AppDbContext Context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();
        public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();
        public async ValueTask<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public void Update(T entity) => _dbSet.Update(entity);
        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();
        public async Task<int> SumAsync<T>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, int>> sumExpression) where T : class
        {
            var query = context.Set<T>().AsQueryable();
            if (whereExpression != null)
            {
                query = query.Where(whereExpression);
            }
            return await query.SumAsync(sumExpression);
        }

        public async Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> LastAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.LastAsync(predicate);
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }
        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstAsync(predicate);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task<List<T>> GetPagedAllListAsync(int pageNumber, int pagesize)
        {
            return await _dbSet.Skip((pageNumber - 1) * pagesize).Take(pagesize).ToListAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task CreateAsync(IEnumerable<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void Delete(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }
    }
}
