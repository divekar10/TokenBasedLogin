using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Database.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UserContext _userContext;
        public Repository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _userContext.AddAsync<T>(entity);
            await _userContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await _userContext.Set<T>().Where(expression).FirstOrDefaultAsync();
        }
    }
}
