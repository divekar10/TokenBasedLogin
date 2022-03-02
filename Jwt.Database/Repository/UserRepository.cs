using Jwt.Database.Infrastructure;
using Jwt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Database.Repository
{
    public interface IUserRepository : IRepository<Register>
    {
        Task<IEnumerable<Register>> GetUsers(int from, int to);
    }

    public class UserRepository : Repository<Register>, IUserRepository
    {
        public UserRepository(UserContext userContext) : base(userContext)
        {
              
        }

        public async Task<IEnumerable<Register>> GetUsers(int from, int to)
        {
            var _param = new List<SqlParameter>
                {
                  new SqlParameter("@From", from),
                  new SqlParameter("@To", to)
                };

            IEnumerable<Register> users = await SQLHelper.CGetData<Register>("SP_GetUsers", _param);
            return users;
        }
    }
}
