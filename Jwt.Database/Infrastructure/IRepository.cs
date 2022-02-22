using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Database.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> AddAsync(List<T> entity);
        Task<T> GetDefault(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> Get();
    }
}
