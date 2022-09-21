using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MailingList.Data
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        #region Fields

        private bool _disposed;
        private readonly DataContext _dbContext;
        private readonly DbSet<T> _dbSet;

        #endregion

        #region Ctor

        public Repository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #endregion

        #region Methods

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _dbContext.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

     

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            query = includeProperties.Split(new[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        #endregion
    }
}
