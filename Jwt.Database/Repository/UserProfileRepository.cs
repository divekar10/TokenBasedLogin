using Jwt.Database.Infrastructure;
using Jwt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Database.Repository
{
    public interface IUserProfileRepository : IRepository<Users>
    {

    }

    public class UserProfileRepository : Repository<Users>, IUserProfileRepository
    {
        public UserProfileRepository(UserContext userContext) : base(userContext)
        {

        }
    }
}
