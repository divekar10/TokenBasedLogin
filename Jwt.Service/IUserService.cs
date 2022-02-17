using Jwt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Service
{
    public interface IUserService
    {
        Task<Register> GetUser(string email, string password);
        Task<Register> Add(Register entity);
        Task<IEnumerable<Register>> GetUsers();
    }
}
