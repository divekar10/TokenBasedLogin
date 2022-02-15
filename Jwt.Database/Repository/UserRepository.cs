using Jwt.Database.Infrastructure;
using Jwt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Database.Repository
{
    public interface IUserRepository : IRepository<Register>
    {

    }

    public class UserRepository : Repository<Register>, IUserRepository
    {
        public UserRepository(UserContext userContext) : base(userContext)
        {
              
        }
    }
}
