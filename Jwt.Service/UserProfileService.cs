using Jwt.Database.Repository;
using Jwt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Service
{
    public interface IUserProfileService 
    {
        Task<Users> AddUserWithProfile(Users entity);
    }
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        public async Task<Users> AddUserWithProfile(Users entity)
        {
                //var user = new User();
                //user.FirstName = entity.FirstName;
                //user.LastName = entity.LastName;
                //user.DOB = entity.DOB;
                //user.PhotoPath = entity.PhotoPath;

                return await _userProfileRepository.AddAsync(entity);
        }
    }
}
