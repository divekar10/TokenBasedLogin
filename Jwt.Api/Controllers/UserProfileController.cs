using Jwt.Model;
using Jwt.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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
    //[Authorize]
    public class UserProfileController : BaseController
    {
        private IUserProfileService _userProfileService; 
        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost]
        [Route("UploadUserWithProfile")]
        //[AllowAnonymous]
        public async Task<IActionResult> AddUserWithProfile([FromForm] FileDataDTO file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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

            return Ok(await _userProfileService.AddUserWithProfile(userData));
        }

        [HttpGet,DisableRequestSizeLimit]
        [Route("Download/Profile/{id}")]
        public async Task<IActionResult> DownloadProfilePicture(int id)
        {
            var user = await _userProfileService.GetUserById(id);
            if (user != null)
                return await Download(user.PhotoPath);
            return NotFound();
        }
    }
}
