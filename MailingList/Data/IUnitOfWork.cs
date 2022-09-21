using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingList.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, new();
        Task<int> Save();
    }
}
