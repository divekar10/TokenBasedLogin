using Jwt.Database.Infrastructure;
using Jwt.Database.Repository;
using Jwt.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jwt.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Register> Add(Register entity)
        {
            var user = new Register();
            user.UserName = entity.UserName;
            user.Email = entity.Email;
            user.Password = entity.Password;
            return await _userRepository.AddAsync(entity);

        }

        public async Task<Register> GetUser(string email, string password)
        {
            try
            {
            return await _userRepository.GetDefault(x => x.Email == email && x.Password == password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Register>> GetUsers()
        {
            return await _userRepository.Get();
        }
    }
}
