using Jwt.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : BaseController
    {
        private readonly IUserService _userService;
        public ExportController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("Export")]
        public IActionResult ExportFile()
        {
            return  Export();
        }

    }
}
