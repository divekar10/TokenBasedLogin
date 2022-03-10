using Jwt.Model;
using Jwt.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Web.Helpers;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Authorize]
    //[DisableCors]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (ModelState.IsValid)
            {
                await _userService.Add(register);

                return Ok(new Response { Status = "Success", Message = "User Created Successfull..." });
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        //[EnableCors("AllowOrigin")]
        //[DisableCors]

        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userService.GetUser(model.Email, model.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserId", Convert.ToString(user.Id), ClaimValueTypes.Integer),
                    new Claim("Email",user.Email, ClaimValueTypes.String),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("Users")]
        [Produces(typeof(IEnumerable<Register>))]
        public async Task<IEnumerable<Register>> Get()
        {
            return await _userService.GetUsers();
        }

        [HttpPost]
        [Route("Import/Users"), DisableRequestSizeLimit]
        public async Task<IEnumerable<Register>> ImportUsers(IFormFile file)
        {
            try
            {

                if (file == null || file.Length == 0)
                {
                    return null;
                }

                var path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot",
                                file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //var file = HttpContext.Request.Form.Files[0];
                var list = new List<Register>();

            using(var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using(var excel = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets[0];

                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                            list.Add(new Register
                            {
                                UserName = Convert.ToString(worksheet.Cells[row, 1].Value),
                                Email = Convert.ToString(worksheet.Cells[row, 2].Value),
                                Password = Convert.ToString(worksheet.Cells[row, 3].Value)
                            });
                        
                    }
                }
            }
                return await _userService.AddUsers(list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("HangFireJob")]
        public string HangFireJob()
        {
            return _userService.Colors();
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("File not selected..");

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",
                file.FileName);

            using(var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok();
        }

        [HttpGet]
        [Route("PagedResult")]
        public IActionResult GetUsersPagination([FromQuery] PagedParameters pagedParameters)
        {
            var users =  _userService.AllUsers(pagedParameters);
            return Ok(users);
        }

        [HttpGet]
        [Route("PagedResultUsingSP")]
        public async Task<IActionResult> GetAllUsers(int pageIndex, int pageSize, int recordCount)
        {
            var users = await _userService.GetAllUsers(pageIndex, pageSize, recordCount);
            return JsonResponse(users);
        }

        [HttpPost]
        [Route("GetProducts")]
        public List<Product> GetProducts(Product product)
        {
            var result = new List<Product>()
            {
                new Product{ Id = product.Id, Name = product.Name}
            };
            return result;
        }
    }
}
