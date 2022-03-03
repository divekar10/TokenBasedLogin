using Jwt.Model;
using Jwt.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        //Task<User> AddUserWithProfile(User entity);

        Task<IEnumerable<Register>> AddUsers(List<Register> entities);
        //Task<IEnumerable<User>> UserWithProfile(List<User> entities);
        Task<IEnumerable<Register>> GetUsers();
        IEnumerable<Register> AllUsers(PagedParameters pagedParameters);
        //FileContentResult Export();
        //public IEnumerable<Register> AllUsers();
        Task<IEnumerable<UserDto>> GetAllUsers(int pageIndex, int pageSize, int recordCount);
        string Colors();
    }
}
