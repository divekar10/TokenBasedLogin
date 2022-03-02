using Jwt.Model;
using Jwt.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private IUserProfileService _userProfileService; 
        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost]
        [Route("UploadUserWithProfile")]
        public async Task<Users> AddUserWithProfile([FromForm] FileDataDTO file)
        {
            if (file.file == null || file.file.Length == 0)
                return null;

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot\\Photos",
                file.file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.file.CopyToAsync(stream);
            }

            var userData = new Users();
            userData.FirstName = file.User.FirstName;
            userData.LastName = file.User.LastName;
            userData.DOB = file.User.DOB;
            userData.PhotoPath = path.Trim().ToString();

            return await _userProfileService.AddUserWithProfile(userData);
        }
    }
}
