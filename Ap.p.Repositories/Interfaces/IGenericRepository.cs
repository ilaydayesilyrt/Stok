using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;
namespace App.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> predicate);
        Task<T> LastAsync(Expression<Func<T, bool>> predicate);
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        Task<List<T>> GetPagedAllListAsync(int pageNumber, int pagesize);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        ValueTask<T?> GetByIdAsync(Guid id);
        ValueTask AddAsync(T entity);
        void Update(T entity);
        Task CreateAsync(T entity);
        Task CreateAsync(IEnumerable<T> entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entity);
        Task<int> SumAsync<T>(
             Expression<Func<T, bool>> whereExpression,
             Expression<Func<T, int>> sumExpression) where T : class;
    }
}
