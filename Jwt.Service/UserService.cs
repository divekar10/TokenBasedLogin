using ClosedXML.Excel;
using Jwt.Database.Infrastructure;
using Jwt.Database.Repository;
using Jwt.Model;
using Jwt.Model.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            return await _userRepository.AddAsync(entity);
        }

        public string Colors()
        {
            var colors = new List<string>();
            var result = "";
            colors.Add("Red");
            colors.Add("Green");
            colors.Add("Pink");
            colors.Add("Orange");
            colors.Add("Purple");
            colors.Add("Yellow");

            foreach (var item in colors)
            {
                result += " "+ item.ToString();
            }

            return result;
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

        public async Task<IEnumerable<Register>> AddUsers(List<Register> entities)
        {
             IEnumerable<Register> register = await _userRepository.AddAsync(entities);
            return register;
        }

        public IEnumerable<Register> AllUsers(PagedParameters pagedParameters)
        {
            return _userRepository.FindAll()
                                  .OrderBy(user => user.Id)
                                  .Skip((pagedParameters.PageNumber - 1) * pagedParameters.PageSize)
                                  .Take(pagedParameters.PageSize)
                                  .ToList();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers(int pageIndex, int pageSize, int recordCount)
        {
            IEnumerable<UserDto> users = await _userRepository.GetUsers(pageIndex, pageSize, recordCount);
            return users;
        }
    }
}
