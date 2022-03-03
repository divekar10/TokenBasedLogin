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
        //private readonly UserContext _userContext;
        protected UserContext UserContext { get; set; }
        public string connectionString = string.Empty;
        public Repository(UserContext userContext)
        {
            this.UserContext = userContext;
            SQLHelper.ConnectionString = UserContext.Database.GetDbConnection().ConnectionString;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await UserContext.AddAsync<T>(entity);
            await UserContext.SaveChangesAsync();
            return entity;
        }

        public async virtual Task<List<T>> AddAsync(List<T> entity)
        {
            await UserContext.AddRangeAsync(entity);
            await UserContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await UserContext.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await UserContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> FindAll()
        {
            return UserContext.Set<T>();
        }
    }
}
