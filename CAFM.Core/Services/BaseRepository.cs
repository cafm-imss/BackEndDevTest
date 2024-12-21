using CAFM.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CAFM.Database.Models;

namespace CAFM.Core.Services
{
    public class BaseRepository<T> : IBaseRepository<T>, IAsyncDisposable where T : class
    {
        protected readonly CmmsBeTestContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(CmmsBeTestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        // Query Helper
        private IQueryable<T> PrepareQuery(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? skip = null, int? take = null,
            bool isNoTracking = false)
        {
            var query = _dbSet.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (isNoTracking)
                query = query.AsNoTracking();

            return query;
        }

        // Get All
        public IReadOnlyList<T> GetAll(
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            return PrepareQuery(include: include, orderBy: orderBy).ToList();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            return await PrepareQuery(include: include, orderBy: orderBy).ToListAsync().ConfigureAwait(false);
        }

        // Get By ID
        public T? GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public async ValueTask<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id).ConfigureAwait(false);
        }

        // Find
        public T? Find(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool isNoTracking = false)
        {
            return PrepareQuery(filter, include, isNoTracking: isNoTracking).FirstOrDefault();
        }

        public async Task<T?> FindAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool isNoTracking = false)
        {
            return await PrepareQuery(filter, include, isNoTracking: isNoTracking).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        // Find All
        public async Task<IReadOnlyList<T>> FindAllAsync(
            Expression<Func<T, bool>> filter,
            int? skip = null, int? take = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool isNoTracking = false)
        {
            return await PrepareQuery(filter, include, orderBy, skip, take, isNoTracking).ToListAsync().ConfigureAwait(false);
        }

        // Add
        public T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async ValueTask<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity).ConfigureAwait(false);
            return entity;
        }

        // Add Range
        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities).ConfigureAwait(false);
        }

        // Update
        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        // Delete
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        // Count
        public int Count(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? _dbSet.Count() : _dbSet.Count(filter);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? await _dbSet.CountAsync().ConfigureAwait(false) : await _dbSet.CountAsync(filter).ConfigureAwait(false);
        }

        // Exists
        public bool Exists(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter).ConfigureAwait(false);
        }

        // Save Changes
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        // Dispose Async
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }

}
