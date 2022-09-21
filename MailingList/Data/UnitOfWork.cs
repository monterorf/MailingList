using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingList.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private bool _disposed;
        private readonly DataContext _dbContext;

        #endregion

        #region Ctor

        public UnitOfWork(DataContext dbContext) =>
            _dbContext = dbContext;

        #endregion

        #region Methods

        public IRepository<T> GetRepository<T>() where T : class, new() =>
            new Repository<T>(_dbContext);

        public Task<int> Save() =>
            _dbContext.SaveChangesAsync();

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

        #endregion
    }
}
