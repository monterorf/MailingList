using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MailingList.Data
{
    public interface IRepository<T> : IDisposable where T : class, new() 
    {
       
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

        Task AddAsync(T entity);
       
    }
}
