using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CAFM.Core.Interfaces
{
    public interface IBaseRepository<T>
    {

        IReadOnlyList<T> GetAll(
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<IReadOnlyList<T>> GetAllAsync(
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        T? GetById(object id);

        ValueTask<T?> GetByIdAsync(object id);

        T? Find(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool isNoTracking = false);

        Task<T?> FindAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool isNoTracking = false);

        Task<IReadOnlyList<T>> FindAllAsync(
            Expression<Func<T, bool>> filter,
            int? skip = null, int? take = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool isNoTracking = false);

        T Add(T entity);

        ValueTask<T> AddAsync(T entity);

        void AddRange(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);

        T Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        int Count(Expression<Func<T, bool>>? filter = null);

        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);

        bool Exists(Expression<Func<T, bool>> filter);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
    }

}
