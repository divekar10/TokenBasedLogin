using Jwt.Database.Infrastructure;
using Jwt.Model;
using Jwt.Model.DTOs;
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
        Task<IEnumerable<UserDto>> GetUsers(int pageIndex, int pageSize, int recordCount);
    }

    public class UserRepository : Repository<Register>, IUserRepository
    {
        public UserRepository(UserContext userContext) : base(userContext)
        {
              
        }

        public async Task<IEnumerable<UserDto>> GetUsers(int pageIndex, int pageSize, int recordCount)
        {
            var _param = new List<SqlParameter>
                {
                  new SqlParameter("@PageIndex", pageIndex),
                  new SqlParameter("@PageSize", pageSize),
                  new SqlParameter("@RecordCount", recordCount)
                };

            IEnumerable<UserDto> users = await SQLHelper.CGetData<UserDto>("SP_GetUsers", _param);
            return users;
        }
    }
}
